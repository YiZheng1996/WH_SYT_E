using MainUI.LogicalConfiguration.Instrument.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using ProtocolType = MainUI.LogicalConfiguration.Instrument.Models.ProtocolType;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// TCP/IP 通讯提供者
    /// </summary>
    public class TcpCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private TcpProtocolConfig _config;
        private readonly SemaphoreSlim _connectLock = new(1, 1);   // ← 用 SemaphoreSlim 替代 lock，支持 async

        public ProtocolType ProtocolType => ProtocolType.TcpIp;
        public bool IsConnected => _client?.Connected ?? false;
        public string ConnectionId => _config != null ? $"{_config.IpAddress}:{_config.Port}" : "";

        // ── 连接 ─────────────────────────────────────────────────────────────

        public async Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not TcpProtocolConfig tcpConfig)
            {
                logger?.LogError("无效的TCP配置");
                return false;
            }

            await _connectLock.WaitAsync(cancellationToken);
            try
            {
                // 复用已有连接
                if (_client?.Connected == true
                    && _config?.IpAddress == tcpConfig.IpAddress
                    && _config?.Port == tcpConfig.Port)
                {
                    logger?.LogDebug("复用TCP连接: {ConnectionId}", ConnectionId);
                    return true;
                }

                // 断开旧连接
                DisposeConnection();

                _config = tcpConfig;
                _client = new TcpClient
                {
                    ReceiveTimeout = tcpConfig.ReadTimeout,
                    SendTimeout = 30000,    // 固定值，不再从配置读取
                    ReceiveBufferSize = tcpConfig.ReceiveBufferSize,
                    SendBufferSize = tcpConfig.SendBufferSize,
                    NoDelay = true
                };

                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(tcpConfig.ConnectionTimeout);

                await _client.ConnectAsync(tcpConfig.IpAddress, tcpConfig.Port, cts.Token);
                _stream = _client.GetStream();

                logger?.LogInformation("TCP连接成功: {Address}:{Port}", tcpConfig.IpAddress, tcpConfig.Port);
                return true;
            }
            catch (OperationCanceledException)
            {
                logger?.LogWarning("TCP连接超时: {Address}:{Port}", tcpConfig.IpAddress, tcpConfig.Port);
                DisposeConnection();
                return false;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "TCP连接失败: {Address}:{Port}", tcpConfig.IpAddress, tcpConfig.Port);
                DisposeConnection();
                return false;
            }
            finally
            {
                _connectLock.Release();
            }
        }

        // ── 断开 ─────────────────────────────────────────────────────────────

        public Task DisconnectAsync()
        {
            DisposeConnection();
            logger?.LogInformation("TCP连接已断开: {ConnectionId}", ConnectionId);
            return Task.CompletedTask;
        }

        // ── 发送并接收 ───────────────────────────────────────────────────────

        public async Task<CommunicationResult> SendAndReceiveAsync(
            byte[] data,
            FrameConfig frameConfig,
            int timeout,
            bool waitForResponse,
            CancellationToken cancellationToken = default)
        {
            var result = new CommunicationResult
            {
                SentData = data,
                SentString = EncodingHelper.SmartDecode(data)
            };
            var sw = Stopwatch.StartNew();

            try
            {
                if (!IsConnected)
                    return CommunicationResult.Failed("TCP未连接");

                await _stream.WriteAsync(data, cancellationToken);
                await _stream.FlushAsync(cancellationToken);
                logger?.LogDebug("TCP发送({Bytes}B): {Hex}", data.Length, BitConverter.ToString(data));

                if (!waitForResponse)
                {
                    result.Success = true;
                    result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                    return result;
                }

                var responseData = await ReceiveAsync(frameConfig, timeout, cancellationToken);

                result.RawResponse = responseData;
                result.ResponseString = responseData?.Length > 0 ? EncodingHelper.SmartDecode(responseData) : "";
                result.Success = responseData?.Length > 0;
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;

                if (logger?.IsEnabled(LogLevel.Trace) == true)
                    logger.LogTrace("TCP接收编码诊断:\n{Diagnosis}", EncodingHelper.DiagnoseEncoding(responseData));
                else
                    logger?.LogDebug("TCP接收({Bytes}B): {Text}", responseData?.Length ?? 0, result.ResponseString);

                return result;
            }
            catch (OperationCanceledException)
            {
                result.ErrorMessage = "操作被取消";
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"TCP通讯异常: {ex.Message}";
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                logger?.LogError(ex, "TCP通讯异常");
                return result;
            }
        }

        // ── 仅发送 ───────────────────────────────────────────────────────────

        public async Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!IsConnected) return false;
                await _stream.WriteAsync(data, cancellationToken);
                await _stream.FlushAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "TCP发送失败");
                return false;
            }
        }

        // ── 接收 ─────────────────────────────────────────────────────────────

        public async Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default)
        {
            try
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                var bufferSize = _config?.ReceiveBufferSize ?? 4096;
                var buffer = new byte[bufferSize];
                var receivedData = new List<byte>(bufferSize);

                // 流式读取：每次 ReadAsync 阻塞等待数据，不再轮询 DataAvailable
                while (!cts.Token.IsCancellationRequested)
                {
                    // 设置单次读取超时（避免一直阻塞）
                    _client.ReceiveTimeout = Math.Min(timeout, 200);
                    int bytesRead;
                    try
                    {
                        bytesRead = await _stream.ReadAsync(buffer.AsMemory(0, bufferSize), cts.Token);
                    }
                    catch (IOException) // ReadTimeout 触发 IOException
                    {
                        // 已有数据但无更多 → 认为接收完毕
                        if (receivedData.Count > 0) break;
                        continue;
                    }

                    if (bytesRead == 0) break; // 连接关闭

                    receivedData.AddRange(buffer.Take(bytesRead));

                    if (IsResponseComplete(receivedData.ToArray(), frameConfig))
                        break;
                }

                return receivedData.ToArray();
            }
            catch (OperationCanceledException)
            {
                logger?.LogDebug("TCP接收超时/取消，已收 {Bytes}B", 0);
                return Array.Empty<byte>();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "TCP接收异常");
                return Array.Empty<byte>();
            }
        }

        // ── 帧完整性判断 ─────────────────────────────────────────────────────

        private static bool IsResponseComplete(byte[] data, FrameConfig frameConfig)
        {
            if (data == null || data.Length == 0) return false;
            if (frameConfig == null || !frameConfig.Enabled) return true;

            // 1. 固定长度
            if (frameConfig.FixedResponseLength > 0)
                return data.Length >= frameConfig.FixedResponseLength;

            // 2. 终止符（支持 \r\n 转义）
            if (!string.IsNullOrEmpty(frameConfig.ResponseTerminator))
            {
                var termBytes = Encoding.ASCII.GetBytes(
                    frameConfig.ResponseTerminator.Replace("\\r", "\r").Replace("\\n", "\n"));
                if (data.Length >= termBytes.Length)
                {
                    var tail = data.AsSpan(data.Length - termBytes.Length);
                    if (tail.SequenceEqual(termBytes)) return true;
                }
            }

            // 3. 帧尾 Hex（如 "0D 0A"）
            if (!string.IsNullOrEmpty(frameConfig.FrameFooter))
            {
                try
                {
                    var footer = Convert.FromHexString(frameConfig.FrameFooter.Replace(" ", ""));
                    if (data.Length >= footer.Length)
                    {
                        var tail = data.AsSpan(data.Length - footer.Length);
                        if (tail.SequenceEqual(footer)) return true;
                    }
                }
                catch { /* 格式错误则忽略 */ }
            }

            return false;
        }

        // ── 资源释放 ─────────────────────────────────────────────────────────

        private void DisposeConnection()
        {
            try { _stream?.Dispose(); } catch { }
            try { _client?.Dispose(); } catch { }
            _stream = null;
            _client = null;
        }

        public void Dispose()
        {
            DisposeConnection();
            _connectLock.Dispose();
        }
    }
}
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
        private readonly SemaphoreSlim _connectLock = new(1, 1);

        public ProtocolType ProtocolType => ProtocolType.TcpIp;

        // ★ BUG #3 修复: 增强 IsConnected 判断
        // TcpClient.Connected 只反映最后一次 I/O 操作的结果，
        // 远端断开后该属性仍可能返回 true（半开连接）。
        // 补充检查 _stream 是否为空、socket 是否存活。
        public bool IsConnected
        {
            get
            {
                try
                {
                    if (_client == null || _stream == null)
                        return false;

                    if (!_client.Connected)
                        return false;

                    // 尝试通过 Poll 检测连接是否真的存活
                    // Poll(0, SelectRead) = true + Available == 0 → 远端已关闭
                    var socket = _client.Client;
                    if (socket == null) return false;

                    // 如果 socket 可读但没有数据，说明远端已关闭（FIN 已到达）
                    if (socket.Poll(0, SelectMode.SelectRead))
                    {
                        // 有数据可读或连接已关闭
                        if (socket.Available == 0)
                        {
                            // 远端已关闭连接
                            logger?.LogDebug("TCP连接检测到远端已关闭: {ConnectionId}", ConnectionId);
                            return false;
                        }
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

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
                // 复用已有连接（使用增强后的 IsConnected 判断）
                if (IsConnected
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
                    SendTimeout = 30000,
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
                result.ResponseString = responseData?.Length > 0 ?
                    EncodingHelper.SmartDecode(responseData) : "";
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
                // 检测到 IO/Socket 异常时，主动清理连接
                // 这样下次调用 IsConnected 会返回 false，触发自动重连
                if (ex is IOException or SocketException)
                {
                    logger?.LogWarning("TCP通讯异常，连接可能已断开，清理连接以便下次重连: {Message}", ex.Message);
                    DisposeConnection();
                }

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

                // ★ BUG #3 修复: 发送失败也清理连接
                if (ex is IOException or SocketException)
                {
                    DisposeConnection();
                }

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
                using var ms = new MemoryStream();

                while (!cts.Token.IsCancellationRequested)
                {
                    var bytesRead = await _stream.ReadAsync(buffer.AsMemory(0, bufferSize), cts.Token);
                    if (bytesRead == 0)
                    {
                        // 远端关闭连接时 ReadAsync 返回 0
                        logger?.LogDebug("TCP远端关闭连接");
                        DisposeConnection();
                        break;
                    }

                    ms.Write(buffer, 0, bytesRead);
                    var data = ms.ToArray();

                    // 帧完整性判断
                    if (frameConfig == null || !frameConfig.Enabled || IsFrameComplete(data, frameConfig))
                    {
                        return data;
                    }

                    // 安全上限
                    if (data.Length > bufferSize * 10)
                    {
                        logger?.LogWarning("TCP接收数据超过安全上限，停止接收");
                        return data;
                    }
                }

                return ms.ToArray();
            }
            catch (OperationCanceledException)
            {
                logger?.LogDebug("TCP接收超时");
                return null;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "TCP接收异常");

                // 接收异常也清理连接
                if (ex is IOException or SocketException)
                {
                    DisposeConnection();
                }

                return null;
            }
        }

        // ── 帧完整性判断 ─────────────────────────────────────────────────────

        private static bool IsFrameComplete(byte[] data, FrameConfig frameConfig)
        {
            if (data == null || data.Length == 0) return false;

            // 1. 固定长度
            if (frameConfig.FixedResponseLength > 0)
            {
                return data.Length >= frameConfig.FixedResponseLength;
            }

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

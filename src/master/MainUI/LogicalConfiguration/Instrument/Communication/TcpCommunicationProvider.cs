using MainUI.LogicalConfiguration.Instrument.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Text;
using ProtocolType = MainUI.LogicalConfiguration.Instrument.Models.ProtocolType;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// TCP/IP通讯提供者
    /// </summary>
    public class TcpCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private TcpProtocolConfig _config;
        private readonly object _lockObject = new();

        public ProtocolType ProtocolType => ProtocolType.TcpIp;

        public bool IsConnected => _client?.Connected ?? false;
        public string ConnectionId => _config != null ? $"{_config.IpAddress}:{_config.Port}" : "";

        public async Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not TcpProtocolConfig tcpConfig)
            {
                logger?.LogError("无效的TCP配置");
                return false;
            }

            try
            {
                lock (_lockObject)
                {
                    // 如果已连接到相同地址，复用连接
                    if (_client?.Connected == true && _config?.IpAddress == tcpConfig.IpAddress && _config?.Port == tcpConfig.Port)
                    {
                        return true;
                    }

                    // 断开旧连接
                    _stream?.Dispose();
                    _client?.Dispose();

                    _config = tcpConfig;
                    _client = new TcpClient
                    {
                        ReceiveTimeout = tcpConfig.ReadTimeout,
                        SendTimeout = tcpConfig.WriteTimeout,
                        ReceiveBufferSize = tcpConfig.ReceiveBufferSize,
                        SendBufferSize = tcpConfig.SendBufferSize
                    };
                }

                // 使用超时连接
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
                return false;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "TCP连接失败: {Address}:{Port}", tcpConfig.IpAddress, tcpConfig.Port);
                return false;
            }
        }

        public Task DisconnectAsync()
        {
            lock (_lockObject)
            {
                try
                {
                    _stream?.Dispose();
                    _client?.Dispose();
                    _stream = null;
                    _client = null;
                    logger?.LogInformation("TCP连接已断开: {ConnectionId}", ConnectionId);
                }
                catch (Exception ex)
                {
                    logger?.LogWarning(ex, "断开TCP连接时发生异常");
                }
            }
            return Task.CompletedTask;
        }

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
                SentString = Encoding.ASCII.GetString(data)
            };

            var sw = Stopwatch.StartNew();

            try
            {
                if (!IsConnected)
                {
                    return CommunicationResult.Failed("TCP未连接");
                }

                // 发送数据
                await _stream.WriteAsync(data, cancellationToken);
                await _stream.FlushAsync(cancellationToken);

                logger?.LogDebug("TCP发送: {Data}", BitConverter.ToString(data));

                if (!waitForResponse)
                {
                    result.Success = true;
                    result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                    return result;
                }

                // 接收响应
                var responseData = await ReceiveAsync(frameConfig, timeout, cancellationToken);

                result.RawResponse = responseData;
                result.ResponseString = responseData != null ? Encoding.ASCII.GetString(responseData) : "";
                result.Success = responseData != null && responseData.Length > 0;
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;

                logger?.LogDebug("TCP接收: {Data}", result.ResponseString);

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
                result.ErrorMessage = $"通讯异常: {ex.Message}";
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                logger?.LogError(ex, "TCP通讯异常");
                return result;
            }
        }

        public async Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!IsConnected)
                    return false;

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

        public async Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default)
        {
            try
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                var buffer = new byte[_config?.ReceiveBufferSize ?? 4096];
                var receivedData = new List<byte>();

                // 简化的接收逻辑
                while (!cts.Token.IsCancellationRequested)
                {
                    if (_stream.DataAvailable)
                    {
                        var bytesRead = await _stream.ReadAsync(buffer, cts.Token);
                        if (bytesRead > 0)
                        {
                            receivedData.AddRange(buffer.Take(bytesRead));

                            // 检查是否接收完成
                            if (IsResponseComplete(receivedData.ToArray(), frameConfig))
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        // 如果已有数据且没有更多数据可读，认为接收完成
                        if (receivedData.Count > 0)
                        {
                            await Task.Delay(50, cts.Token);
                            if (!_stream.DataAvailable)
                                break;
                        }
                        else
                        {
                            await Task.Delay(10, cts.Token);
                        }
                    }
                }

                return receivedData.ToArray();
            }
            catch (OperationCanceledException)
            {
                logger?.LogDebug("TCP接收超时");
                return Array.Empty<byte>();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "TCP接收异常");
                return Array.Empty<byte>();
            }
        }

        private bool IsResponseComplete(byte[] data, FrameConfig frameConfig)
        {
            if (data == null || data.Length == 0)
                return false;

            if (frameConfig == null || !frameConfig.Enabled)
                return true;

            // 检查固定长度
            if (frameConfig.FixedResponseLength > 0)
            {
                return data.Length >= frameConfig.FixedResponseLength;
            }

            // 检查结束标记
            if (!string.IsNullOrEmpty(frameConfig.ResponseTerminator))
            {
                var terminator = Encoding.ASCII.GetBytes(frameConfig.ResponseTerminator.Replace("\\n", "\n").Replace("\\r", "\r"));
                if (data.Length >= terminator.Length)
                {
                    var endBytes = data.Skip(data.Length - terminator.Length).ToArray();
                    return endBytes.SequenceEqual(terminator);
                }
            }

            // 检查帧尾
            if (!string.IsNullOrEmpty(frameConfig.FrameFooter))
            {
                var footer = HexStringToBytes(frameConfig.FrameFooter);
                if (footer.Length > 0 && data.Length >= footer.Length)
                {
                    var endBytes = data.Skip(data.Length - footer.Length).ToArray();
                    return endBytes.SequenceEqual(footer);
                }
            }

            return false;
        }

        private static byte[] HexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "");
            if (hex.Length % 2 != 0)
                hex = "0" + hex;

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        public void Dispose()
        {
            DisconnectAsync().Wait();
        }
    }
}

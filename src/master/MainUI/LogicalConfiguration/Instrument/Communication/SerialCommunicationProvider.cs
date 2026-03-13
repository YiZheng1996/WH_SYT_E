using MainUI.LogicalConfiguration.Instrument.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// 串口通讯提供者
    /// ReceiveAsync 返回 null → Array.Empty；IsFrameComplete 支持终止符字符串；
    ///       SendAndReceiveAsync 对空响应安全处理
    /// </summary>
    public class SerialCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private SerialPort _serialPort;
        private SerialProtocolConfig _config;
        private readonly object _lockObject = new();
        private bool _disposed = false;

        public ProtocolType ProtocolType => ProtocolType.Serial;
        public bool IsConnected => _serialPort?.IsOpen ?? false;
        public string ConnectionId => _config?.PortName ?? "";

        // ── 连接 ─────────────────────────────────────────────────────────────

        public Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not SerialProtocolConfig serialConfig)
            {
                logger?.LogError("无效的串口配置");
                return Task.FromResult(false);
            }

            try
            {
                lock (_lockObject)
                {
                    // 复用已有连接
                    if (_serialPort?.IsOpen == true
                        && _config?.PortName == serialConfig.PortName
                        && _config?.BaudRate == serialConfig.BaudRate)
                    {
                        logger?.LogDebug("复用串口连接: {PortName}", serialConfig.PortName);
                        return Task.FromResult(true);
                    }

                    SafeCloseSerialPort();
                    _config = serialConfig;
                    return Task.FromResult(ConnectWithRetry(serialConfig, maxRetries: 3));
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口连接失败: {PortName}", serialConfig.PortName);
                return Task.FromResult(false);
            }
        }

        private bool ConnectWithRetry(SerialProtocolConfig config, int maxRetries = 3)
        {
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    logger?.LogDebug("尝试打开串口 ({Attempt}/{Max}): {PortName}",
                        attempt, maxRetries, config.PortName);

                    _serialPort = new SerialPort
                    {
                        PortName = config.PortName,
                        BaudRate = config.BaudRate,
                        DataBits = config.DataBits,
                        StopBits = ConvertStopBits(config.StopBits),
                        Parity = ConvertParity(config.Parity),
                        ReadTimeout = config.ReadTimeout > 0 ? config.ReadTimeout : 30000,
                        WriteTimeout = 30000,   // 固定值，不再从配置读取
                        ReceivedBytesThreshold = 1
                    };

                    _serialPort.Open();
                    logger?.LogInformation("串口已打开: {PortName} {Baud}bps", config.PortName, config.BaudRate);
                    return true;
                }
                catch (UnauthorizedAccessException) when (attempt < maxRetries)
                {
                    logger?.LogWarning("串口被占用，等待后重试: {PortName}", config.PortName);
                    Thread.Sleep(300 * attempt);
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex, "串口打开失败: {PortName}", config.PortName);
                    if (attempt == maxRetries) return false;
                }
            }
            return false;
        }

        // ── 断开 ─────────────────────────────────────────────────────────────

        public Task DisconnectAsync()
        {
            lock (_lockObject)
            {
                SafeCloseSerialPort();
                logger?.LogInformation("串口已断开: {PortName}", _config?.PortName);
            }
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
                    return CommunicationResult.Failed("串口未打开");

                lock (_lockObject)
                {
                    _serialPort.DiscardInBuffer();
                    _serialPort.DiscardOutBuffer();
                    _serialPort.Write(data, 0, data.Length);
                }
                logger?.LogDebug("串口发送({Bytes}B): {Hex}", data.Length, BitConverter.ToString(data));

                if (!waitForResponse)
                {
                    result.Success = true;
                    result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                    return result;
                }

                var responseData = await ReceiveAsync(frameConfig, timeout, cancellationToken);

                // ← 修复：将 null 视为空数组，避免 NullReferenceException
                responseData ??= Array.Empty<byte>();

                result.RawResponse = responseData;
                result.ResponseString = responseData.Length > 0 ? EncodingHelper.SmartDecode(responseData) : "";
                result.Success = responseData.Length > 0;
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;

                logger?.LogDebug("串口接收({Bytes}B): {Text}", responseData.Length, result.ResponseString);
                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"串口通讯异常: {ex.Message}";
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                logger?.LogError(ex, "串口通讯异常");
                return result;
            }
        }

        // ── 仅发送 ───────────────────────────────────────────────────────────

        public Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!IsConnected) return Task.FromResult(false);
                lock (_lockObject) { _serialPort.Write(data, 0, data.Length); }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口发送失败");
                return Task.FromResult(false);
            }
        }

        // ── 接收 ─────────────────────────────────────────────────────────────

        public async Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default)
        {
            try
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                var receivedData = new List<byte>(1024);
                var buffer = new byte[1024];

                while (!cts.Token.IsCancellationRequested)
                {
                    int available;
                    lock (_lockObject) { available = _serialPort.BytesToRead; }

                    if (available > 0)
                    {
                        int bytesRead;
                        lock (_lockObject)
                        {
                            bytesRead = _serialPort.Read(buffer, 0, Math.Min(buffer.Length, available));
                        }
                        if (bytesRead > 0) receivedData.AddRange(buffer.Take(bytesRead));

                        if (IsFrameComplete(receivedData.ToArray(), frameConfig))
                            break;

                        // 帧未完整，继续等待
                        await Task.Delay(10, cts.Token);
                    }
                    else
                    {
                        if (receivedData.Count > 0 && frameConfig?.Enabled != true)
                        {
                            // 无帧配置 → 等待50ms静默后认为完成
                            await Task.Delay(50, cts.Token);
                            lock (_lockObject) { available = _serialPort.BytesToRead; }
                            if (available == 0) break;
                        }
                        else
                        {
                            await Task.Delay(10, cts.Token);
                        }
                    }
                }

                return receivedData.ToArray();  // ← 始终返回 Array，不返回 null
            }
            catch (OperationCanceledException)
            {
                logger?.LogDebug("串口接收超时/取消");
                return Array.Empty<byte>();     // ← 修复：不返回 null
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口接收异常");
                return Array.Empty<byte>();
            }
        }

        // ── 帧完整性判断 ─────────────────────────────────────────────────────

        private static bool IsFrameComplete(byte[] data, FrameConfig config)
        {
            if (data == null || data.Length == 0) return false;
            if (config == null || !config.Enabled) return false;

            // 1. 固定长度
            if (config.FixedResponseLength > 0)
                return data.Length >= config.FixedResponseLength;

            // 2. 终止符字符串（如 "\r\n"）← 修复：原来只支持帧尾Hex
            if (!string.IsNullOrEmpty(config.ResponseTerminator))
            {
                var termBytes = Encoding.ASCII.GetBytes(
                    config.ResponseTerminator.Replace("\\r", "\r").Replace("\\n", "\n"));
                if (data.Length >= termBytes.Length)
                {
                    var tail = data.AsSpan(data.Length - termBytes.Length);
                    if (tail.SequenceEqual(termBytes)) return true;
                }
            }

            // 3. 帧尾 Hex（如 "0D 0A"）
            if (!string.IsNullOrEmpty(config.FrameFooter))
            {
                try
                {
                    var footer = Convert.FromHexString(config.FrameFooter.Replace(" ", ""));
                    if (data.Length >= footer.Length)
                    {
                        var tail = data.AsSpan(data.Length - footer.Length);
                        if (tail.SequenceEqual(footer)) return true;
                    }
                }
                catch { /* Hex 格式错误则忽略 */ }
            }

            return false;
        }

        // ── 串口安全关闭 ─────────────────────────────────────────────────────

        private void SafeCloseSerialPort()
        {
            if (_serialPort == null) return;
            try
            {
                if (_serialPort.IsOpen) _serialPort.Close();
                _serialPort.Dispose();
            }
            catch (Exception ex)
            {
                logger?.LogDebug("关闭串口时发生异常(可忽略): {Message}", ex.Message);
            }
            finally
            {
                _serialPort = null;
            }
        }

        // ── 强制释放串口 ─────────────────────────────────────────────────────

        public static void ForceRelease(string portName)
        {
            try
            {
                using var sp = new SerialPort(portName);
                if (!sp.IsOpen) sp.Open();
                sp.Close();
            }
            catch { }
        }

        // ── 类型转换 ─────────────────────────────────────────────────────────

        private static StopBits ConvertStopBits(StopBitsType type) => type switch
        {
            StopBitsType.One => StopBits.One,
            StopBitsType.Two => StopBits.Two,
            StopBitsType.OnePointFive => StopBits.OnePointFive,
            _ => StopBits.One
        };

        private static Parity ConvertParity(ParityType type) => type switch
        {
            ParityType.Even => Parity.Even,
            ParityType.Odd => Parity.Odd,
            ParityType.Mark => Parity.Mark,
            ParityType.Space => Parity.Space,
            _ => Parity.None
        };

        // ── 资源释放 ─────────────────────────────────────────────────────────

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            lock (_lockObject) { SafeCloseSerialPort(); }
        }
    }
}
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
    /// SendAndReceiveAsync 对空响应安全处理
    /// </summary>
    public class SerialCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private SerialPort _serialPort;
        private SerialProtocolConfig _config;
        private readonly object _lockObject = new();
        private bool _disposed = false;

        // 新增 SemaphoreSlim，确保发送-接收整个流程是原子操作
        // 原来用 lock(_lockObject) 只锁了发送部分，接收在锁外面，
        // 并发时两个线程可能交叉发送接收，导致数据错乱
        private readonly SemaphoreSlim _sendReceiveLock = new(1, 1);

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
                        WriteTimeout = 30000,
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

        /// <summary>
        /// 整个发送-接收流程用 SemaphoreSlim 包裹，保证原子性
        /// </summary>
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

            // 用 SemaphoreSlim 锁住整个发送+接收流程
            // 确保同一个串口上不会有两个线程交叉操作
            await _sendReceiveLock.WaitAsync(cancellationToken);
            try
            {
                if (!IsConnected)
                    return CommunicationResult.Failed("串口未打开");

                // 清空缓冲区 + 发送（在 lock 内保证串口操作的线程安全）
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

                // 接收现在也在 SemaphoreSlim 保护内
                var responseData = await ReceiveAsync(frameConfig, timeout, cancellationToken);

                // 将 null 视为空数组，避免 NullReferenceException
                responseData ??= Array.Empty<byte>();

                result.RawResponse = responseData;
                result.ResponseString = responseData.Length > 0 ?
                    EncodingHelper.SmartDecode(responseData) : "";
                result.Success = responseData.Length > 0;
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;

                logger?.LogDebug("串口接收({Bytes}B): {Text}", responseData.Length, result.ResponseString);

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
                result.ErrorMessage = $"串口通讯异常: {ex.Message}";
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                logger?.LogError(ex, "串口通讯异常");
                return result;
            }
            finally
            {
                _sendReceiveLock.Release();
            }
        }

        // ── 仅发送 ───────────────────────────────────────────────────────────

        public Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!IsConnected) return Task.FromResult(false);

                lock (_lockObject)
                {
                    _serialPort.Write(data, 0, data.Length);
                }

                logger?.LogDebug("串口发送({Bytes}B)", data.Length);
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

                using var ms = new MemoryStream();
                var buffer = new byte[4096];

                while (!cts.Token.IsCancellationRequested)
                {
                    int bytesAvailable;
                    lock (_lockObject)
                    {
                        bytesAvailable = _serialPort?.BytesToRead ?? 0;
                    }

                    if (bytesAvailable > 0)
                    {
                        int bytesRead;
                        lock (_lockObject)
                        {
                            bytesRead = _serialPort.Read(buffer, 0, Math.Min(bytesAvailable, buffer.Length));
                        }

                        ms.Write(buffer, 0, bytesRead);
                        var data = ms.ToArray();

                        // 帧完整性判断
                        if (frameConfig == null || !frameConfig.Enabled || IsFrameComplete(data, frameConfig))
                        {
                            return data;
                        }
                    }
                    else
                    {
                        // 等待数据到达
                        await Task.Delay(10, cts.Token);
                    }

                    // 如果已有数据但一段时间没有新数据，返回现有数据
                    if (ms.Length > 0 && bytesAvailable == 0)
                    {
                        await Task.Delay(50, cts.Token);
                        lock (_lockObject)
                        {
                            if ((_serialPort?.BytesToRead ?? 0) == 0)
                            {
                                return ms.ToArray();
                            }
                        }
                    }
                }

                return ms.Length > 0 ? ms.ToArray() : null;
            }
            catch (OperationCanceledException)
            {
                logger?.LogDebug("串口接收超时");
                return null;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口接收异常");
                return null;
            }
        }

        // ── 帧完整性判断 ─────────────────────────────────────────────────────

        private static bool IsFrameComplete(byte[] data, FrameConfig config)
        {
            if (data == null || data.Length == 0) return false;

            // 1. 固定长度
            if (config.FixedResponseLength > 0)
            {
                return data.Length >= config.FixedResponseLength;
            }

            // 2. 终止符字符串（如 "\r\n"）
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
            _sendReceiveLock.Dispose();
        }
    }
}

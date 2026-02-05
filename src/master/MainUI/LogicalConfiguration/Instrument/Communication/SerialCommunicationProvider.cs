using MainUI.LogicalConfiguration.Instrument.Models;
using Microsoft.Extensions.Logging;
using System.IO.Ports;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// 串口通讯提供者
    /// 
    /// 增强异常处理，确保串口完全释放
    /// 添加重试机制处理"Access Denied"错误
    /// 增加串口占用检测和强制释放
    /// 优化Dispose模式
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
                    // 如果已连接到相同串口且配置相同，复用连接
                    if (_serialPort?.IsOpen == true &&
                        _config?.PortName == serialConfig.PortName &&
                        _config?.BaudRate == serialConfig.BaudRate)
                    {
                        logger?.LogDebug("复用现有串口连接: {PortName}", serialConfig.PortName);
                        return Task.FromResult(true);
                    }

                    // 安全关闭旧连接
                    SafeCloseSerialPort();

                    _config = serialConfig;

                    // 重试机制
                    return Task.FromResult(ConnectWithRetry(serialConfig, maxRetries: 3));
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口连接失败: {PortName}", serialConfig.PortName);
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// 带重试的连接方法
        /// </summary>
        private bool ConnectWithRetry(SerialProtocolConfig config, int maxRetries = 3)
        {
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    logger?.LogDebug("尝试连接串口 (第{Attempt}/{Max}次): {PortName}",
                        attempt, maxRetries, config.PortName);

                    // 创建新的串口对象
                    _serialPort = new SerialPort
                    {
                        PortName = config.PortName,
                        BaudRate = config.BaudRate,
                        DataBits = config.DataBits,
                        StopBits = ConvertStopBits(config.StopBits),
                        Parity = ConvertParity(config.Parity),
                        Handshake = ConvertFlowControl(config.FlowControl),
                        ReadTimeout = config.ReadTimeout,
                        WriteTimeout = config.WriteTimeout,
                        DtrEnable = config.DtrEnable,
                        RtsEnable = config.RtsEnable
                    };

                    _serialPort.Open();

                    logger?.LogInformation("串口连接成功: {PortName} (第{Attempt}次尝试)",
                        config.PortName, attempt);
                    return true;
                }
                catch (UnauthorizedAccessException ex)
                {
                    logger?.LogWarning("串口访问被拒绝 (第{Attempt}/{Max}次): {PortName} - {Message}",
                        attempt, maxRetries, config.PortName, ex.Message);

                    // 尝试强制释放
                    if (attempt < maxRetries)
                    {
                        ForceReleasePort(config.PortName);
                        Thread.Sleep(200 * attempt); // 递增延时: 200ms, 400ms, 600ms
                    }
                }
                catch (IOException ex)
                {
                    logger?.LogWarning("串口IO错误 (第{Attempt}/{Max}次): {PortName} - {Message}",
                        attempt, maxRetries, config.PortName, ex.Message);

                    if (attempt < maxRetries)
                    {
                        Thread.Sleep(100 * attempt);
                    }
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex, "串口连接异常 (第{Attempt}/{Max}次): {PortName}",
                        attempt, maxRetries, config.PortName);

                    if (attempt >= maxRetries)
                    {
                        break;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 安全关闭串口
        /// </summary>
        private void SafeCloseSerialPort()
        {
            if (_serialPort == null)
                return;

            try
            {
                if (!_serialPort.IsOpen) return;

                logger?.LogDebug("正在关闭串口: {PortName}", _serialPort.PortName);

                // 清空缓冲区
                try
                {
                    _serialPort.DiscardInBuffer();
                    _serialPort.DiscardOutBuffer();
                }
                catch { /* 忽略缓冲区清空错误 */ }

                // 关闭串口
                _serialPort.Close();
                logger?.LogDebug("串口已关闭: {PortName}", _serialPort.PortName);
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "关闭串口时发生异常");
            }
            finally
            {
                // 确保释放资源
                try
                {
                    _serialPort?.Dispose();
                }
                catch { /* 忽略Dispose错误 */ }

                _serialPort = null;
            }
        }

        /// <summary>
        /// 强制释放串口（尝试清除系统锁定）
        /// </summary>
        private void ForceReleasePort(string portName)
        {
            try
            {
                logger?.LogDebug("尝试强制释放串口: {PortName}", portName);

                // 尝试通过创建临时SerialPort对象来释放
                using (var tempPort = new SerialPort(portName))
                {
                    if (tempPort.IsOpen)
                    {
                        tempPort.Close();
                    }
                }

                // 强制GC清理
                GC.Collect();
                GC.WaitForPendingFinalizers();

                logger?.LogDebug("强制释放完成: {PortName}", portName);
            }
            catch (Exception ex)
            {
                logger?.LogDebug("强制释放失败: {PortName} - {Message}", portName, ex.Message);
            }
        }

        public Task DisconnectAsync()
        {
            lock (_lockObject)
            {
                try
                {
                    SafeCloseSerialPort();
                    logger?.LogInformation("串口已断开: {PortName}", _config?.PortName);
                }
                catch (Exception ex)
                {
                    logger?.LogWarning(ex, "断开串口时发生异常");
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
                SentString = EncodingHelper.SmartDecode(data)
            };

            var sw = Stopwatch.StartNew();

            try
            {
                if (!IsConnected)
                {
                    return CommunicationResult.Failed("串口未打开");
                }

                // 清空缓冲区
                _serialPort.DiscardInBuffer();
                _serialPort.DiscardOutBuffer();

                // 发送数据
                _serialPort.Write(data, 0, data.Length);

                logger?.LogDebug("串口发送: {Data}", BitConverter.ToString(data));

                if (!waitForResponse)
                {
                    result.Success = true;
                    result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                    return result;
                }

                // 接收响应
                var responseData = await ReceiveAsync(frameConfig, timeout, cancellationToken);

                result.RawResponse = responseData;
                result.ResponseString = responseData != null ?
                    EncodingHelper.SmartDecode(responseData) : "";
                result.Success = responseData != null && responseData.Length > 0;
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;

                logger?.LogDebug("串口接收: {Data}", result.ResponseString);

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

        public Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!IsConnected)
                    return Task.FromResult(false);

                _serialPort.Write(data, 0, data.Length);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口发送失败");
                return Task.FromResult(false);
            }
        }

        public async Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default)
        {
            try
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                var receivedData = new List<byte>();
                var buffer = new byte[1024];

                while (!cts.Token.IsCancellationRequested)
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        var bytesRead = _serialPort.Read(buffer, 0, Math.Min(buffer.Length, _serialPort.BytesToRead));
                        receivedData.AddRange(buffer.Take(bytesRead));

                        // 检查是否接收完整
                        if (frameConfig?.Enabled == true)
                        {
                            if (IsFrameComplete(receivedData.ToArray(), frameConfig))
                                break;
                        }
                        else
                        {
                            await Task.Delay(50, cts.Token);
                            if (_serialPort.BytesToRead == 0)
                                break;
                        }
                    }
                    else
                    {
                        await Task.Delay(10, cts.Token);
                    }
                }

                return receivedData.ToArray();
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口接收异常");
                return null;
            }
        }

        private bool IsFrameComplete(byte[] data, FrameConfig config)
        {
            if (data.Length == 0)
                return false;

            // 检查帧尾
            if (string.IsNullOrEmpty(config.FrameFooter)) return false;

            var footer = Convert.FromHexString(config.FrameFooter.Replace(" ", ""));

            if (data.Length < footer.Length) return false;

            var endBytes = data.Skip(data.Length - footer.Length).ToArray();

            return endBytes.SequenceEqual(footer);
        }

        #region 类型转换辅助方法

        private static StopBits ConvertStopBits(StopBitsType type)
        {
            return type switch
            {
                StopBitsType.One => StopBits.One,
                StopBitsType.Two => StopBits.Two,
                StopBitsType.OnePointFive => StopBits.OnePointFive,
                _ => StopBits.One
            };
        }

        private static Parity ConvertParity(ParityType type)
        {
            return type switch
            {
                ParityType.None => Parity.None,
                ParityType.Odd => Parity.Odd,
                ParityType.Even => Parity.Even,
                ParityType.Mark => Parity.Mark,
                ParityType.Space => Parity.Space,
                _ => Parity.None
            };
        }

        private static Handshake ConvertFlowControl(FlowControlType type)
        {
            return type switch
            {
                FlowControlType.None => Handshake.None,
                FlowControlType.Hardware => Handshake.RequestToSend,
                FlowControlType.Software => Handshake.XOnXOff,
                _ => Handshake.None
            };
        }

        #endregion

        #region IDisposable实现

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                lock (_lockObject)
                {
                    SafeCloseSerialPort();
                }
            }

            _disposed = true;
        }

        ~SerialCommunicationProvider()
        {
            Dispose(false);
        }

        #endregion
    }
}
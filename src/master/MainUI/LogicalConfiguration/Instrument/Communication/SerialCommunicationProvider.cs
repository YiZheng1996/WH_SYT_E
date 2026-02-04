using MainUI.LogicalConfiguration.Instrument.Models;
using Microsoft.Extensions.Logging;
using System.IO.Ports;
using System.Text;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// 串口通讯提供者
    /// </summary>
    public class SerialCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private SerialPort _serialPort;
        private SerialProtocolConfig _config;
        private readonly object _lockObject = new();

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
                    switch (_serialPort?.IsOpen)
                    {
                        // 如果已连接到相同串口，复用
                        case true when (_config?.PortName == serialConfig.PortName):
                            return Task.FromResult(true);
                        // 关闭旧连接
                        case true:
                            _serialPort.Close();
                            break;
                    }

                    _serialPort?.Dispose();

                    _config = serialConfig;
                    _serialPort = new SerialPort
                    {
                        PortName = serialConfig.PortName,
                        BaudRate = serialConfig.BaudRate,
                        DataBits = serialConfig.DataBits,
                        StopBits = ConvertStopBits(serialConfig.StopBits),
                        Parity = ConvertParity(serialConfig.Parity),
                        Handshake = ConvertFlowControl(serialConfig.FlowControl),
                        ReadTimeout = serialConfig.ReadTimeout,
                        WriteTimeout = serialConfig.WriteTimeout,
                        DtrEnable = serialConfig.DtrEnable,
                        RtsEnable = serialConfig.RtsEnable
                    };

                    _serialPort.Open();
                }

                logger?.LogInformation("串口连接成功: {PortName}", serialConfig.PortName);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口连接失败: {PortName}", serialConfig.PortName);
                return Task.FromResult(false);
            }
        }

        public Task DisconnectAsync()
        {
            lock (_lockObject)
            {
                try
                {
                    if (_serialPort?.IsOpen == true)
                    {
                        _serialPort.Close();
                    }
                    _serialPort?.Dispose();
                    _serialPort = null;
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
                SentString = EncodingHelper.SmartDecode(data)  // 智能解码
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
                result.RawResponse = responseData;
                result.ResponseString = responseData != null ?
                    EncodingHelper.SmartDecode(responseData) : "";  // 智能解码
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
                        if (receivedData.Count > 0)
                        {
                            await Task.Delay(20, cts.Token);
                            if (_serialPort.BytesToRead == 0)
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
                logger?.LogDebug("串口接收超时");
                return Array.Empty<byte>();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口接收异常");
                return Array.Empty<byte>();
            }
        }

        private bool IsResponseComplete(byte[] data, FrameConfig frameConfig)
        {
            if (data == null || data.Length == 0)
                return false;

            if (frameConfig == null || !frameConfig.Enabled)
                return true;

            // 实现与TCP相同的逻辑
            if (frameConfig.FixedResponseLength > 0)
            {
                return data.Length >= frameConfig.FixedResponseLength;
            }

            if (!string.IsNullOrEmpty(frameConfig.ResponseTerminator))
            {
                var terminator = Encoding.ASCII.GetBytes(frameConfig.ResponseTerminator.Replace("\\n", "\n").Replace("\\r", "\r"));
                if (data.Length >= terminator.Length)
                {
                    var endBytes = data.Skip(data.Length - terminator.Length).ToArray();
                    return endBytes.SequenceEqual(terminator);
                }
            }

            return false;
        }

        private static StopBits ConvertStopBits(StopBitsType type)
        {
            return type switch
            {
                StopBitsType.One => StopBits.One,
                StopBitsType.OnePointFive => StopBits.OnePointFive,
                StopBitsType.Two => StopBits.Two,
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

        public void Dispose()
        {
            DisconnectAsync().Wait();
        }
    }
}

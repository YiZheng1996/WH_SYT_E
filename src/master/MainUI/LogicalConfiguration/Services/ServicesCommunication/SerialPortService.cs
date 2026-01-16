using Microsoft.Extensions.Logging;
using System.IO.Ports;
using System.Text;

namespace MainUI.LogicalConfiguration.Services.ServicesCommunication
{
    /// <summary>
    /// 串口通信配置
    /// </summary>
    public class SerialPortConfig
    {
        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;
        public int ReadTimeout { get; set; } = 3000;
        public int WriteTimeout { get; set; } = 3000;
        public Handshake Handshake { get; set; } = Handshake.None;
    }

    /// <summary>
    /// 串口通信服务
    /// </summary>
    public class SerialPortService(SerialPortConfig config, ILogger<SerialPortService> logger = null)
        : ICommunicationService
    {
        private readonly SerialPortConfig _config = config ?? throw new ArgumentNullException(nameof(config));
        private SerialPort _serialPort;
        private bool _disposed;

        public bool IsConnected => _serialPort?.IsOpen == true;

        /// <summary>
        /// 获取可用串口列表
        /// </summary>
        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        public Task<CommunicationResult> ConnectAsync(CancellationToken cancellationToken = default)
        {
            var result = new CommunicationResult();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                if (_serialPort?.IsOpen == true)
                {
                    _serialPort.Close();
                }

                _serialPort = new SerialPort
                {
                    PortName = _config.PortName,
                    BaudRate = _config.BaudRate,
                    Parity = _config.Parity,
                    DataBits = _config.DataBits,
                    StopBits = _config.StopBits,
                    ReadTimeout = _config.ReadTimeout,
                    WriteTimeout = _config.WriteTimeout,
                    Handshake = _config.Handshake
                };

                _serialPort.Open();

                result.Success = true;
                result.Message = $"串口 {_config.PortName} 打开成功";
                logger?.LogInformation("串口连接成功: {PortName}", _config.PortName);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"串口打开失败: {ex.Message}";
                logger?.LogError(ex, "串口连接失败: {PortName}", _config.PortName);
            }

            result.ElapsedTime = stopwatch.Elapsed;
            return Task.FromResult(result);
        }

        public Task DisconnectAsync()
        {
            try
            {
                _serialPort?.Close();
                _serialPort?.Dispose();
                _serialPort = null;

                logger?.LogInformation("串口已关闭");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "关闭串口时发生错误");
            }

            return Task.CompletedTask;
        }

        public Task<CommunicationResult> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            var result = new CommunicationResult();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                if (!IsConnected)
                {
                    var connectResult = ConnectAsync(cancellationToken).Result;
                    if (!connectResult.Success)
                        return Task.FromResult(connectResult);
                }

                _serialPort.Write(data, 0, data.Length);

                result.Success = true;
                result.BytesSent = data.Length;
                result.Message = $"发送成功，共 {data.Length} 字节";
                logger?.LogDebug("串口发送数据成功: {ByteCount} 字节", data.Length);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"发送失败: {ex.Message}";
                logger?.LogError(ex, "串口发送数据失败");
            }

            result.ElapsedTime = stopwatch.Elapsed;
            return Task.FromResult(result);
        }

        public Task<CommunicationResult> SendTextAsync(string text, Encoding encoding = null, CancellationToken cancellationToken = default)
        {
            encoding ??= Encoding.UTF8;
            var data = encoding.GetBytes(text);
            return SendAsync(data, cancellationToken);
        }

        public async Task<CommunicationResult> SendAndReceiveAsync(byte[] data, int timeout = 3000, CancellationToken cancellationToken = default)
        {
            var result = await SendAsync(data, cancellationToken);
            if (!result.Success)
                return result;

            try
            {
                // 等待响应
                await Task.Delay(100, cancellationToken); // 给设备一点响应时间

                var buffer = new byte[4096];
                int totalBytesRead = 0;

                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                // 循环读取直到没有更多数据
                while (!cts.Token.IsCancellationRequested)
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        int bytesRead = _serialPort.Read(buffer, totalBytesRead, Math.Min(_serialPort.BytesToRead, buffer.Length - totalBytesRead));
                        totalBytesRead += bytesRead;
                        await Task.Delay(50, cts.Token); // 等待更多数据
                    }
                    else
                    {
                        break;
                    }
                }

                if (totalBytesRead > 0)
                {
                    result.ResponseData = buffer.Take(totalBytesRead).ToArray();
                    result.ResponseText = Encoding.UTF8.GetString(result.ResponseData);
                    result.BytesReceived = totalBytesRead;
                    result.Message = $"发送 {result.BytesSent} 字节，接收 {totalBytesRead} 字节";
                }
                else
                {
                    result.Message += "（无响应数据）";
                }
            }
            catch (OperationCanceledException)
            {
                result.Message += "（接收超时）";
            }
            catch (Exception ex)
            {
                result.Message += $"（接收失败: {ex.Message}）";
                logger?.LogWarning(ex, "接收串口响应数据失败");
            }

            return result;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _serialPort?.Close();
            _serialPort?.Dispose();
        }
    }
}
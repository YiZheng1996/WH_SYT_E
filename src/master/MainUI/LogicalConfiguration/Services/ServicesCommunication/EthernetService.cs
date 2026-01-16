using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MainUI.LogicalConfiguration.Services.ServicesCommunication
{
    /// <summary>
    /// 以太网通信配置
    /// </summary>
    public class EthernetConfig
    {
        public string IPAddress { get; set; } = "192.168.1.100";
        public int Port { get; set; } = 502;
        public int ConnectTimeout { get; set; } = 5000;
        public int SendTimeout { get; set; } = 3000;
        public int ReceiveTimeout { get; set; } = 3000;
        public ProtocolType Protocol { get; set; } = ProtocolType.Tcp; // TCP/UDP
    }

    /// <summary>
    /// 以太网通信服务
    /// </summary>
    public class EthernetService : ICommunicationService
    {
        private readonly ILogger<EthernetService> _logger;
        private readonly EthernetConfig _config;
        private TcpClient _tcpClient;
        private UdpClient _udpClient;
        private NetworkStream _stream;
        private bool _disposed;

        public bool IsConnected => _tcpClient?.Connected == true || _udpClient != null;

        public EthernetService(EthernetConfig config, ILogger<EthernetService> logger = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _logger = logger;
        }

        public async Task<CommunicationResult> ConnectAsync(CancellationToken cancellationToken = default)
        {
            var result = new CommunicationResult();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                if (_config.Protocol == ProtocolType.Tcp)
                {
                    _tcpClient = new TcpClient
                    {
                        SendTimeout = _config.SendTimeout,
                        ReceiveTimeout = _config.ReceiveTimeout
                    };

                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    cts.CancelAfter(_config.ConnectTimeout);

                    await _tcpClient.ConnectAsync(
                        IPAddress.Parse(_config.IPAddress),
                        _config.Port,
                        cts.Token);

                    _stream = _tcpClient.GetStream();
                    _logger?.LogInformation("TCP连接成功: {IP}:{Port}", _config.IPAddress, _config.Port);
                }
                else
                {
                    _udpClient = new UdpClient();
                    _udpClient.Connect(IPAddress.Parse(_config.IPAddress), _config.Port);
                    _logger?.LogInformation("UDP连接成功: {IP}:{Port}", _config.IPAddress, _config.Port);
                }

                result.Success = true;
                result.Message = "连接成功";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"连接失败: {ex.Message}";
                _logger?.LogError(ex, "以太网连接失败");
            }

            result.ElapsedTime = stopwatch.Elapsed;
            return result;
        }

        public async Task DisconnectAsync()
        {
            try
            {
                _stream?.Close();
                _tcpClient?.Close();
                _udpClient?.Close();

                _stream = null;
                _tcpClient = null;
                _udpClient = null;

                _logger?.LogInformation("以太网连接已断开");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "断开连接时发生错误");
            }

            await Task.CompletedTask;
        }

        public async Task<CommunicationResult> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            var result = new CommunicationResult();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                if (!IsConnected)
                {
                    var connectResult = await ConnectAsync(cancellationToken);
                    if (!connectResult.Success)
                        return connectResult;
                }

                if (_config.Protocol == ProtocolType.Tcp)
                {
                    await _stream.WriteAsync(data, 0, data.Length, cancellationToken);
                    await _stream.FlushAsync(cancellationToken);
                }
                else
                {
                    await _udpClient.SendAsync(data, data.Length);
                }

                result.Success = true;
                result.BytesSent = data.Length;
                result.Message = $"发送成功，共 {data.Length} 字节";
                _logger?.LogDebug("发送数据成功: {ByteCount} 字节", data.Length);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"发送失败: {ex.Message}";
                _logger?.LogError(ex, "以太网发送数据失败");
            }

            result.ElapsedTime = stopwatch.Elapsed;
            return result;
        }

        public async Task<CommunicationResult> SendTextAsync(string text, Encoding encoding = null, CancellationToken cancellationToken = default)
        {
            encoding ??= Encoding.UTF8;
            var data = encoding.GetBytes(text);
            return await SendAsync(data, cancellationToken);
        }

        public async Task<CommunicationResult> SendAndReceiveAsync(byte[] data, int timeout = 3000, CancellationToken cancellationToken = default)
        {
            var result = await SendAsync(data, cancellationToken);
            if (!result.Success)
                return result;

            try
            {
                var buffer = new byte[4096];
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                int bytesRead;
                if (_config.Protocol == ProtocolType.Tcp)
                {
                    bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, cts.Token);
                }
                else
                {
                    var receiveResult = await _udpClient.ReceiveAsync();
                    buffer = receiveResult.Buffer;
                    bytesRead = receiveResult.Buffer.Length;
                }

                result.ResponseData = buffer.Take(bytesRead).ToArray();
                result.ResponseText = Encoding.UTF8.GetString(result.ResponseData);
                result.BytesReceived = bytesRead;
                result.Message = $"发送 {result.BytesSent} 字节，接收 {bytesRead} 字节";
            }
            catch (OperationCanceledException)
            {
                result.Message += "（接收超时）";
            }
            catch (Exception ex)
            {
                result.Message += $"（接收失败: {ex.Message}）";
                _logger?.LogWarning(ex, "接收响应数据失败");
            }

            return result;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _stream?.Dispose();
            _tcpClient?.Dispose();
            _udpClient?.Dispose();
        }
    }
}
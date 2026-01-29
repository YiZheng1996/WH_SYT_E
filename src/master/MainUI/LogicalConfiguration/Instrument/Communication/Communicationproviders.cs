using MainUI.LogicalConfiguration.Instrument.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using ProtocolType = MainUI.LogicalConfiguration.Instrument.Models.ProtocolType;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    #region 通讯提供者接口

    /// <summary>
    /// 通讯提供者接口
    /// </summary>
    public interface ICommunicationProvider : IDisposable
    {
        /// <summary>
        /// 协议类型
        /// </summary>
        ProtocolType ProtocolType { get; }

        /// <summary>
        /// 是否已连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 连接标识
        /// </summary>
        string ConnectionId { get; }

        /// <summary>
        /// 连接设备
        /// </summary>
        Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default);

        /// <summary>
        /// 断开连接
        /// </summary>
        Task DisconnectAsync();

        /// <summary>
        /// 发送数据并接收响应
        /// </summary>
        Task<CommunicationResult> SendAndReceiveAsync(
            byte[] data,
            FrameConfig frameConfig,
            int timeout,
            bool waitForResponse,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 仅发送数据
        /// </summary>
        Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default);

        /// <summary>
        /// 接收数据
        /// </summary>
        Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default);
    }

    #endregion

    #region TCP通讯提供者

    /// <summary>
    /// TCP/IP通讯提供者
    /// </summary>
    public class TcpCommunicationProvider : ICommunicationProvider
    {
        private readonly ILogger _logger;
        private TcpClient _client;
        private NetworkStream _stream;
        private TcpProtocolConfig _config;
        private readonly object _lockObject = new();

        public ProtocolType ProtocolType => ProtocolType.TcpIp;
        public bool IsConnected => _client?.Connected ?? false;
        public string ConnectionId => _config != null ? $"{_config.IpAddress}:{_config.Port}" : "";

        public TcpCommunicationProvider(ILogger logger = null)
        {
            _logger = logger;
        }

        public async Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not TcpProtocolConfig tcpConfig)
            {
                _logger?.LogError("无效的TCP配置");
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

                _logger?.LogInformation("TCP连接成功: {Address}:{Port}", tcpConfig.IpAddress, tcpConfig.Port);
                return true;
            }
            catch (OperationCanceledException)
            {
                _logger?.LogWarning("TCP连接超时: {Address}:{Port}", tcpConfig.IpAddress, tcpConfig.Port);
                return false;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "TCP连接失败: {Address}:{Port}", tcpConfig.IpAddress, tcpConfig.Port);
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
                    _logger?.LogInformation("TCP连接已断开: {ConnectionId}", ConnectionId);
                }
                catch (Exception ex)
                {
                    _logger?.LogWarning(ex, "断开TCP连接时发生异常");
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

                _logger?.LogDebug("TCP发送: {Data}", BitConverter.ToString(data));

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

                _logger?.LogDebug("TCP接收: {Data}", result.ResponseString);

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
                _logger?.LogError(ex, "TCP通讯异常");
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
                _logger?.LogError(ex, "TCP发送失败");
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
                _logger?.LogDebug("TCP接收超时");
                return Array.Empty<byte>();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "TCP接收异常");
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

    #endregion

    #region 串口通讯提供者

    /// <summary>
    /// 串口通讯提供者
    /// </summary>
    public class SerialCommunicationProvider : ICommunicationProvider
    {
        private readonly ILogger _logger;
        private SerialPort _serialPort;
        private SerialProtocolConfig _config;
        private readonly object _lockObject = new();

        public ProtocolType ProtocolType => ProtocolType.Serial;
        public bool IsConnected => _serialPort?.IsOpen ?? false;
        public string ConnectionId => _config?.PortName ?? "";

        public SerialCommunicationProvider(ILogger logger = null)
        {
            _logger = logger;
        }

        public Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not SerialProtocolConfig serialConfig)
            {
                _logger?.LogError("无效的串口配置");
                return Task.FromResult(false);
            }

            try
            {
                lock (_lockObject)
                {
                    // 如果已连接到相同串口，复用
                    if (_serialPort?.IsOpen == true && _config?.PortName == serialConfig.PortName)
                    {
                        return Task.FromResult(true);
                    }

                    // 关闭旧连接
                    if (_serialPort?.IsOpen == true)
                    {
                        _serialPort.Close();
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

                _logger?.LogInformation("串口连接成功: {PortName}", serialConfig.PortName);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "串口连接失败: {PortName}", serialConfig.PortName);
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
                    _logger?.LogInformation("串口已断开: {PortName}", _config?.PortName);
                }
                catch (Exception ex)
                {
                    _logger?.LogWarning(ex, "断开串口时发生异常");
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
                    return CommunicationResult.Failed("串口未打开");
                }

                // 清空缓冲区
                _serialPort.DiscardInBuffer();
                _serialPort.DiscardOutBuffer();

                // 发送数据
                _serialPort.Write(data, 0, data.Length);

                _logger?.LogDebug("串口发送: {Data}", BitConverter.ToString(data));

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

                _logger?.LogDebug("串口接收: {Data}", result.ResponseString);

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"串口通讯异常: {ex.Message}";
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                _logger?.LogError(ex, "串口通讯异常");
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
                _logger?.LogError(ex, "串口发送失败");
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
                _logger?.LogDebug("串口接收超时");
                return Array.Empty<byte>();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "串口接收异常");
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

    #endregion

    #region HTTP通讯提供者

    /// <summary>
    /// HTTP通讯提供者
    /// </summary>
    public class HttpCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private HttpClient _httpClient;
        private HttpProtocolConfig _config;

        public ProtocolType ProtocolType => ProtocolType.Http;
        public bool IsConnected => _httpClient != null;
        public string ConnectionId => _config?.BaseUrl ?? "";

        public Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not HttpProtocolConfig httpConfig)
            {
                logger?.LogError("无效的HTTP配置");
                return Task.FromResult(false);
            }

            try
            {
                _config = httpConfig;
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri(httpConfig.BaseUrl),
                    Timeout = TimeSpan.FromMilliseconds(httpConfig.ConnectionTimeout)
                };

                // 设置认证
                if (httpConfig.AuthType == "Basic" && !string.IsNullOrEmpty(httpConfig.Username))
                {
                    var authBytes = Encoding.ASCII.GetBytes($"{httpConfig.Username}:{httpConfig.Password}");
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
                }
                else if (httpConfig.AuthType == "Bearer" && !string.IsNullOrEmpty(httpConfig.BearerToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", httpConfig.BearerToken);
                }

                // 设置默认请求头
                foreach (var header in httpConfig.DefaultHeaders)
                {
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }

                logger?.LogInformation("HTTP客户端初始化成功: {BaseUrl}", httpConfig.BaseUrl);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "HTTP客户端初始化失败");
                return Task.FromResult(false);
            }
        }

        public Task DisconnectAsync()
        {
            _httpClient?.Dispose();
            _httpClient = null;
            return Task.CompletedTask;
        }

        public async Task<CommunicationResult> SendAndReceiveAsync(
            byte[] data,
            FrameConfig frameConfig,
            int timeout,
            bool waitForResponse,
            CancellationToken cancellationToken = default)
        {
            var result = new CommunicationResult();
            var sw = Stopwatch.StartNew();

            try
            {
                if (_httpClient == null)
                {
                    return CommunicationResult.Failed("HTTP客户端未初始化");
                }

                var requestString = Encoding.UTF8.GetString(data);
                result.SentData = data;
                result.SentString = requestString;

                // 解析请求: "METHOD|URL|BODY" 格式
                var parts = requestString.Split('|');
                var method = parts.Length > 0 ? parts[0].ToUpper() : "GET";
                var url = parts.Length > 1 ? parts[1] : "/";
                var body = parts.Length > 2 ? parts[2] : "";

                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                HttpResponseMessage response;
                switch (method)
                {
                    case "GET":
                        response = await _httpClient.GetAsync(url, cts.Token);
                        break;
                    case "POST":
                        var postContent = new StringContent(body, Encoding.UTF8, _config.ContentType);
                        response = await _httpClient.PostAsync(url, postContent, cts.Token);
                        break;
                    case "PUT":
                        var putContent = new StringContent(body, Encoding.UTF8, _config.ContentType);
                        response = await _httpClient.PutAsync(url, putContent, cts.Token);
                        break;
                    case "DELETE":
                        response = await _httpClient.DeleteAsync(url, cts.Token);
                        break;
                    default:
                        return CommunicationResult.Failed($"不支持的HTTP方法: {method}");
                }

                result.ResponseString = await response.Content.ReadAsStringAsync(cts.Token);
                result.RawResponse = Encoding.UTF8.GetBytes(result.ResponseString);
                result.Success = response.IsSuccessStatusCode;
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;

                if (!result.Success)
                {
                    result.ErrorMessage = $"HTTP错误: {(int)response.StatusCode} {response.ReasonPhrase}";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"HTTP请求异常: {ex.Message}";
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                logger?.LogError(ex, "HTTP请求异常");
                return result;
            }
        }

        public Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            // HTTP不支持仅发送
            return Task.FromResult(false);
        }

        public Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default)
        {
            // HTTP不支持单独接收
            return Task.FromResult(Array.Empty<byte>());
        }

        public void Dispose()
        {
            DisconnectAsync().Wait();
        }
    }

    #endregion

    #region 通讯提供者工厂

    /// <summary>
    /// 通讯提供者工厂
    /// </summary>
    public class CommunicationProviderFactory(ILogger logger = null)
    {
        private readonly ConcurrentDictionary<string, ICommunicationProvider> _providerCache = new();

        /// <summary>
        /// 获取或创建通讯提供者
        /// </summary>
        public ICommunicationProvider GetOrCreateProvider(ProtocolType protocolType, string connectionId = null)
        {
            var key = $"{protocolType}_{connectionId ?? "default"}";

            return _providerCache.GetOrAdd(key, _ => CreateProvider(protocolType));
        }

        /// <summary>
        /// 创建新的通讯提供者
        /// </summary>
        public ICommunicationProvider CreateProvider(ProtocolType protocolType)
        {
            return protocolType switch
            {
                ProtocolType.TcpIp => new TcpCommunicationProvider(logger),
                ProtocolType.Serial => new SerialCommunicationProvider(logger),
                ProtocolType.Http => new HttpCommunicationProvider(logger),
                ProtocolType.ModbusTcp or ProtocolType.ModbusRtu => new ModbusCommunicationProvider(logger),
                _ => throw new NotSupportedException($"不支持的协议类型: {protocolType}")
            };
        }

        /// <summary>
        /// 释放所有提供者
        /// </summary>
        public void DisposeAll()
        {
            foreach (var provider in _providerCache.Values)
            {
                provider.Dispose();
            }
            _providerCache.Clear();
        }

        /// <summary>
        /// 移除并释放指定提供者
        /// </summary>
        public void RemoveProvider(string key)
        {
            if (_providerCache.TryRemove(key, out var provider))
            {
                provider.Dispose();
            }
        }
    }

    #endregion

    #region Modbus通讯提供者(简化版)

    /// <summary>
    /// Modbus通讯提供者(简化实现)
    /// 实际项目中建议使用NModbus等成熟库
    /// </summary>
    public class ModbusCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private ICommunicationProvider _underlyingProvider;
        private ModbusProtocolConfig _config;

        public ProtocolType ProtocolType => _config?.ProtocolType ?? ProtocolType.ModbusTcp;
        public bool IsConnected => _underlyingProvider?.IsConnected ?? false;
        public string ConnectionId => _underlyingProvider?.ConnectionId ?? "";

        public async Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not ModbusProtocolConfig modbusConfig)
            {
                logger?.LogError("无效的Modbus配置");
                return false;
            }

            _config = modbusConfig;

            // 根据Modbus类型创建底层通讯
            if (modbusConfig.ProtocolType == ProtocolType.ModbusTcp)
            {
                _underlyingProvider = new TcpCommunicationProvider(logger);
                var tcpConfig = new TcpProtocolConfig
                {
                    IpAddress = modbusConfig.IpAddress,
                    Port = modbusConfig.Port,
                    ConnectionTimeout = modbusConfig.ConnectionTimeout,
                    ReadTimeout = modbusConfig.ReadTimeout,
                    WriteTimeout = modbusConfig.WriteTimeout
                };
                return await _underlyingProvider.ConnectAsync(tcpConfig, cancellationToken);
            }
            else
            {
                _underlyingProvider = new SerialCommunicationProvider(logger);
                var serialConfig = new SerialProtocolConfig
                {
                    PortName = modbusConfig.PortName,
                    BaudRate = modbusConfig.BaudRate,
                    DataBits = modbusConfig.DataBits,
                    StopBits = modbusConfig.StopBits,
                    Parity = modbusConfig.Parity,
                    ReadTimeout = modbusConfig.ReadTimeout,
                    WriteTimeout = modbusConfig.WriteTimeout
                };
                return await _underlyingProvider.ConnectAsync(serialConfig, cancellationToken);
            }
        }

        public async Task DisconnectAsync()
        {
            if (_underlyingProvider != null)
            {
                await _underlyingProvider.DisconnectAsync();
            }
        }

        public async Task<CommunicationResult> SendAndReceiveAsync(
            byte[] data,
            FrameConfig frameConfig,
            int timeout,
            bool waitForResponse,
            CancellationToken cancellationToken = default)
        {
            // 简化实现：解析Modbus命令并构建报文
            // 格式: "功能码,参数1,参数2,..."
            try
            {
                var commandStr = Encoding.ASCII.GetString(data);
                var parts = commandStr.Split(',');

                if (parts.Length < 1)
                {
                    return CommunicationResult.Failed("无效的Modbus命令格式");
                }

                var functionCode = byte.Parse(parts[0]);
                var modbusRequest = BuildModbusRequest(functionCode, parts.Skip(1).ToArray());

                // 添加Modbus TCP头或RTU CRC
                byte[] finalRequest;
                if (_config.ProtocolType == ProtocolType.ModbusTcp)
                {
                    finalRequest = AddModbusTcpHeader(modbusRequest);
                }
                else
                {
                    finalRequest = AddModbusRtuCrc(modbusRequest);
                }

                // 设置Modbus特定的帧配置
                var modbusFrameConfig = new FrameConfig
                {
                    Enabled = true,
                    FixedResponseLength = CalculateExpectedResponseLength(functionCode, parts)
                };

                return await _underlyingProvider.SendAndReceiveAsync(finalRequest, modbusFrameConfig, timeout, waitForResponse, cancellationToken);
            }
            catch (Exception ex)
            {
                return CommunicationResult.Failed($"Modbus请求构建失败: {ex.Message}");
            }
        }

        private byte[] BuildModbusRequest(byte functionCode, string[] parameters)
        {
            var request = new List<byte> { _config.SlaveAddress, functionCode };

            switch (functionCode)
            {
                case 0x03: // 读取保持寄存器
                case 0x04: // 读取输入寄存器
                    if (parameters.Length >= 2)
                    {
                        var startAddress = ushort.Parse(parameters[0]);
                        var count = ushort.Parse(parameters[1]);
                        request.AddRange(BitConverter.GetBytes(startAddress).Reverse());
                        request.AddRange(BitConverter.GetBytes(count).Reverse());
                    }
                    break;

                case 0x06: // 写入单个寄存器
                    if (parameters.Length >= 2)
                    {
                        var address = ushort.Parse(parameters[0]);
                        var value = ushort.Parse(parameters[1]);
                        request.AddRange(BitConverter.GetBytes(address).Reverse());
                        request.AddRange(BitConverter.GetBytes(value).Reverse());
                    }
                    break;
            }

            return request.ToArray();
        }

        private byte[] AddModbusTcpHeader(byte[] pdu)
        {
            var mbap = new byte[7];
            // Transaction ID (2 bytes)
            mbap[0] = 0x00;
            mbap[1] = 0x01;
            // Protocol ID (2 bytes, always 0 for Modbus)
            mbap[2] = 0x00;
            mbap[3] = 0x00;
            // Length (2 bytes)
            var length = (ushort)(pdu.Length);
            mbap[4] = (byte)(length >> 8);
            mbap[5] = (byte)(length & 0xFF);
            // Unit ID
            mbap[6] = pdu[0];

            return mbap.Concat(pdu.Skip(1)).ToArray();
        }

        private byte[] AddModbusRtuCrc(byte[] pdu)
        {
            var crc = CalculateCrc16(pdu);
            return pdu.Concat(BitConverter.GetBytes(crc)).ToArray();
        }

        private ushort CalculateCrc16(byte[] data)
        {
            ushort crc = 0xFFFF;
            foreach (var b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }

        private int CalculateExpectedResponseLength(byte functionCode, string[] parts)
        {
            return functionCode switch
            {
                0x03 or 0x04 when parts.Length >= 2 => 5 + int.Parse(parts[1]) * 2 + (_config.ProtocolType == ProtocolType.ModbusRtu ? 2 : 0),
                0x06 => 8 + (_config.ProtocolType == ProtocolType.ModbusRtu ? 2 : 0),
                _ => 256
            };
        }

        public Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            return _underlyingProvider?.SendAsync(data, cancellationToken) ?? Task.FromResult(false);
        }

        public Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default)
        {
            return _underlyingProvider?.ReceiveAsync(frameConfig, timeout, cancellationToken) ?? Task.FromResult(Array.Empty<byte>());
        }

        public void Dispose()
        {
            _underlyingProvider?.Dispose();
        }
    }

    #endregion
}
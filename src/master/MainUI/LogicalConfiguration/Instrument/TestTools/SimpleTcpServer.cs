using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MainUI.LogicalConfiguration.Instrument.TestTools
{
    /// <summary>
    /// 简单的TCP测试服务器
    /// </summary>
    public class SimpleTcpServer : IDisposable
    {
        private TcpListener _listener;
        private bool _isRunning;
        private readonly List<TcpClient> _clients = new();
        private readonly object _lockObj = new();

        public event Action<string> OnLog;
        public event Action<byte[]> OnDataReceived;

        /// <summary>
        /// 回显模式：接收到什么就返回什么
        /// </summary>
        public bool EchoMode { get; set; } = true;

        /// <summary>
        /// 自定义响应数据
        /// </summary>
        public byte[] CustomResponse { get; set; }

        /// <summary>
        /// 启动服务器
        /// </summary>
        public void Start(int port = 5025)
        {
            if (_isRunning)
                return;

            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _isRunning = true;

            Log($"TCP服务器已启动，端口: {port}");

            // 接受客户端连接
            Task.Run(AcceptClientsAsync);
        }

        private async Task AcceptClientsAsync()
        {
            while (_isRunning)
            {
                try
                {
                    var client = await _listener.AcceptTcpClientAsync();

                    lock (_lockObj)
                    {
                        _clients.Add(client);
                    }

                    var endpoint = client.Client.RemoteEndPoint;
                    Log($"客户端已连接: {endpoint}");

                    // 处理客户端通讯
                    _ = HandleClientAsync(client);
                }
                catch (Exception ex)
                {
                    if (_isRunning)
                        Log($"接受连接失败: {ex.Message}");
                }
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();
            var buffer = new byte[4096];
            var endpoint = client.Client.RemoteEndPoint;

            try
            {
                while (client.Connected && _isRunning)
                {
                    var bytesRead = await stream.ReadAsync(buffer);

                    if (bytesRead == 0)
                        break;

                    var receivedData = buffer.Take(bytesRead).ToArray();
                    Log($"接收 [{endpoint}]: {FormatBytes(receivedData)}");
                    OnDataReceived?.Invoke(receivedData);

                    // 发送响应
                    byte[] response;
                    if (EchoMode)
                    {
                        response = receivedData; // 回显
                    }
                    else if (CustomResponse != null)
                    {
                        response = CustomResponse; // 自定义响应
                    }
                    else
                    {
                        response = Encoding.ASCII.GetBytes("OK\n"); // 默认响应
                    }

                    await stream.WriteAsync(response);
                    Log($"发送 [{endpoint}]: {FormatBytes(response)}");

                    // 小延迟模拟真实设备
                    await Task.Delay(10);
                }
            }
            catch (Exception ex)
            {
                Log($"处理客户端异常 [{endpoint}]: {ex.Message}");
            }
            finally
            {
                lock (_lockObj)
                {
                    _clients.Remove(client);
                }
                client.Close();
                Log($"客户端已断开: {endpoint}");
            }
        }

        public void Stop()
        {
            if (!_isRunning)
                return;

            _isRunning = false;

            lock (_lockObj)
            {
                foreach (var client in _clients)
                {
                    client.Close();
                }
                _clients.Clear();
            }

            _listener?.Stop();
            Log("TCP服务器已停止");
        }

        private void Log(string message)
        {
            OnLog?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        private string FormatBytes(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", " ");
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
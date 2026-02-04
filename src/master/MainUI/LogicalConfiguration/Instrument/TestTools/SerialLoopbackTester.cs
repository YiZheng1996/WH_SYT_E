using System.IO.Ports;
using System.Text;

namespace MainUI.LogicalConfiguration.Instrument.TestTools
{
    /// <summary>
    /// 串口回环测试工具
    /// </summary>
    public class SerialLoopbackTester : IDisposable
    {
        private SerialPort _port1; // 发送端
        private SerialPort _port2; // 接收端
        private bool _isRunning;

        public event Action<string> OnLog;

        /// <summary>
        /// 回显模式
        /// </summary>
        public bool EchoMode { get; set; } = true;

        /// <summary>
        /// 启动回环测试
        /// </summary>
        public void Start(string port1Name, string port2Name, int baudRate = 9600)
        {
            if (_isRunning)
                return;

            // 配置端口1 (发送端)
            _port1 = new SerialPort
            {
                PortName = port1Name,
                BaudRate = baudRate,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                ReadTimeout = 1000,
                WriteTimeout = 1000
            };

            // 配置端口2 (接收端 - 回环)
            _port2 = new SerialPort
            {
                PortName = port2Name,
                BaudRate = baudRate,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                ReadTimeout = 1000,
                WriteTimeout = 1000
            };

            _port1.Open();
            _port2.Open();

            _isRunning = true;
            Log($"串口回环已启动: {port1Name} <-> {port2Name}, 波特率: {baudRate}");

            // 监听端口2的数据并回显
            if (EchoMode)
            {
                _port2.DataReceived += Port2_DataReceived;
            }
        }

        private void Port2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var bytesToRead = _port2.BytesToRead;
                if (bytesToRead == 0)
                    return;

                var buffer = new byte[bytesToRead];
                _port2.Read(buffer, 0, bytesToRead);

                Log($"接收: {FormatBytes(buffer)}");

                // 回显
                _port2.Write(buffer, 0, buffer.Length);
                Log($"回显: {FormatBytes(buffer)}");
            }
            catch (Exception ex)
            {
                Log($"回显异常: {ex.Message}");
            }
        }

        public void Stop()
        {
            if (!_isRunning)
                return;

            _isRunning = false;

            if (_port2 != null)
            {
                _port2.DataReceived -= Port2_DataReceived;
                _port2.Close();
                _port2.Dispose();
            }

            if (_port1 != null)
            {
                _port1.Close();
                _port1.Dispose();
            }

            Log("串口回环已停止");
        }

        private void Log(string message)
        {
            OnLog?.Invoke($"[{DateTime.Now:HH:mm:ss.fff}] {message}");
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
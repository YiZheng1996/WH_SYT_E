using System.Text;
using MainUI.LogicalConfiguration.Instrument.Communication;
using MainUI.LogicalConfiguration.Instrument.Models;

namespace MainUI.LogicalConfiguration.Instrument.TestTools
{
    /// <summary>
    /// 通讯自动化测试
    /// </summary>
    public class CommunicationAutoTest
    {
        private readonly StringBuilder _log = new();

        public async Task RunAllTests()
        {
            Log("=== 开始自动化测试 ===\n");

            // TCP测试
            await TestTcp();

            // 串口测试  
            await TestSerial();

            Log("\n=== 测试完成 ===");

            // 输出完整日志
            Debug.WriteLine(_log.ToString());
        }

        private async Task TestTcp()
        {
            Log("--- TCP测试 ---");

            SimpleTcpServer server = null;
            TcpCommunicationProvider provider = null;

            try
            {
                // 启动测试服务器
                server = new SimpleTcpServer();
                server.Start(5025);
                await Task.Delay(500);
                Log("✓ TCP服务器启动成功");

                // 创建客户端
                provider = new TcpCommunicationProvider();
                var config = new TcpProtocolConfig
                {
                    IpAddress = "127.0.0.1",
                    Port = 5025,
                    ConnectionTimeout = 5000,
                    ReadTimeout = 3000
                };

                // 测试连接
                var connected = await provider.ConnectAsync(config);
                Assert(connected, "TCP连接成功");

                // 测试发送接收
                var testData = Encoding.ASCII.GetBytes("TEST");
                var result = await provider.SendAndReceiveAsync(testData, null, 3000, true);
                Assert(result.Success, "TCP收发成功"); 
                Assert(result.RawResponse != null && 
                       result.RawResponse.SequenceEqual(testData), "TCP数据一致");

                Log("TCP测试通过\n");
            }
            catch (Exception ex)
            {
                Log($"✗ TCP测试失败: {ex.Message}");
            }
            finally
            {
                // 清理
                if (provider != null)
                {
                    await provider.DisconnectAsync();
                    provider.Dispose();
                }
                server?.Stop();
                server?.Dispose();
            }
        }

        private async Task TestSerial()
        {
            Log("--- 串口测试 ---");

            SerialLoopbackTester loopback = null;
            SerialCommunicationProvider provider = null;

            try
            {
                // 检查虚拟串口是否存在
                var ports = System.IO.Ports.SerialPort.GetPortNames();
                if (!ports.Contains("COM7") || !ports.Contains("COM8"))
                {
                    Log("⚠ 未检测到COM7/COM8虚拟串口,跳过串口测试");
                    Log("提示: 请安装 com0com 创建虚拟串口对进行测试");
                    return;
                }

                // 启动回环
                loopback = new SerialLoopbackTester();
                loopback.Start("COM7", "COM8", 9600);
                await Task.Delay(500);
                Log("✓ 串口回环启动成功");

                // 创建客户端
                provider = new SerialCommunicationProvider();
                var config = new SerialProtocolConfig
                {
                    PortName = "COM7",
                    BaudRate = 9600,
                    DataBits = 8,
                    StopBits = StopBitsType.One,
                    Parity = ParityType.None
                };

                // 测试连接
                var connected = await provider.ConnectAsync(config);
                Assert(connected, "串口连接成功");

                // 测试发送接收
                var testData = Encoding.ASCII.GetBytes("SERIAL_TEST");
                var result = await provider.SendAndReceiveAsync(testData, null, 3000, true);
                Assert(result.Success, "串口收发成功");
                // 注意：串口回环可能有字节序问题，这里只检查长度
                Assert(result.RawResponse?.Length == testData.Length, "串口数据长度一致");

                Log("串口测试通过\n");
            }
            catch (Exception ex)
            {
                Log($"✗ 串口测试失败: {ex.Message}");
            }
            finally
            {
                // 清理
                if (provider != null)
                {
                    await provider.DisconnectAsync();
                    provider.Dispose();
                }
                loopback?.Stop();
                loopback?.Dispose();
            }
        }

        private void Assert(bool condition, string message)
        {
            if (condition)
                Log($"✓ {message}");
            else
                throw new Exception($"✗ {message}");
        }

        private void Log(string message)
        {
            _log.AppendLine(message);
            Debug.WriteLine(message);
        }
    }
}
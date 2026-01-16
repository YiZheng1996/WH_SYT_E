using System.IO.Ports;
using System.Net.Sockets;
using System.Text;

namespace MainUI.LogicalConfiguration.Services.ServicesCommunication
{
    /// <summary>
    /// 通信结果
    /// </summary>
    public class CommunicationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public byte[] ResponseData { get; set; }
        public string ResponseText { get; set; }
        public int BytesSent { get; set; }
        public int BytesReceived { get; set; }
        public TimeSpan ElapsedTime { get; set; }
    }

    /// <summary>
    /// 通信服务接口
    /// </summary>
    public interface ICommunicationService : IDisposable
    {
        /// <summary>
        /// 连接状态
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 连接
        /// </summary>
        Task<CommunicationResult> ConnectAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 断开连接
        /// </summary>
        Task DisconnectAsync();

        /// <summary>
        /// 发送数据
        /// </summary>
        Task<CommunicationResult> SendAsync(byte[] data, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送文本
        /// </summary>
        Task<CommunicationResult> SendTextAsync(string text, Encoding encoding = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送并接收
        /// </summary>
        Task<CommunicationResult> SendAndReceiveAsync(byte[] data, int timeout = 3000, CancellationToken cancellationToken = default);
    }
}
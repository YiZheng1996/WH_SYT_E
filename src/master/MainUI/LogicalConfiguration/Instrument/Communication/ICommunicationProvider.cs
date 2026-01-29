using MainUI.LogicalConfiguration.Instrument.Models;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
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
}

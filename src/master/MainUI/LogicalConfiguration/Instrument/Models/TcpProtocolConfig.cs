namespace MainUI.LogicalConfiguration.Instrument.Models
{
    /// <summary>
    /// TCP/IP协议配置
    /// </summary>
    public class TcpProtocolConfig : ProtocolConfigBase
    {
        public override ProtocolType ProtocolType => ProtocolType.TcpIp;

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; } = "192.168.1.100";

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 5000;

        /// <summary>
        /// 接收缓冲区大小
        /// </summary>
        public int ReceiveBufferSize { get; set; } = 4096;

        /// <summary>
        /// 发送缓冲区大小
        /// </summary>
        public int SendBufferSize { get; set; } = 4096;

        public override ProtocolConfigBase Clone()
        {
            return new TcpProtocolConfig
            {
                IpAddress = this.IpAddress,
                Port = this.Port,
                ConnectionTimeout = this.ConnectionTimeout,
                ReadTimeout = this.ReadTimeout,
                KeepAlive = this.KeepAlive,
                ReceiveBufferSize = this.ReceiveBufferSize,
                SendBufferSize = this.SendBufferSize
            };
        }
    }
}

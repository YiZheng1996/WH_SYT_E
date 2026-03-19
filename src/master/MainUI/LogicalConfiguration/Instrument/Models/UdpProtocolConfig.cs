namespace MainUI.LogicalConfiguration.Instrument.Models
{
    /// <summary>
    /// UDP 协议配置
    /// </summary>
    public class UdpProtocolConfig : ProtocolConfigBase
    {
        public override ProtocolType ProtocolType => ProtocolType.Udp;

        /// <summary>
        /// 远程 IP 地址
        /// </summary>
        public string RemoteIpAddress { get; set; } = "192.168.0.100";

        /// <summary>
        /// 远程端口号
        /// </summary>
        public int RemotePort { get; set; } = 5000;

        /// <summary>
        /// 本地绑定端口（0 表示系统自动分配）
        /// </summary>
        public int LocalPort { get; set; } = 0;

        public override ProtocolConfigBase Clone()
        {
            return new UdpProtocolConfig
            {
                RemoteIpAddress = this.RemoteIpAddress,
                RemotePort = this.RemotePort,
                LocalPort = this.LocalPort,
                ConnectionTimeout = this.ConnectionTimeout,
                ReadTimeout = this.ReadTimeout,
                KeepAlive = this.KeepAlive
            };
        }
    }
}
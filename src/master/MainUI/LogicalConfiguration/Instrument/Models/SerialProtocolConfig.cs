namespace MainUI.LogicalConfiguration.Instrument.Models
{
    /// <summary>
    /// 串口协议配置
    /// </summary>
    public class SerialProtocolConfig : ProtocolConfigBase
    {
        public override ProtocolType ProtocolType => ProtocolType.Serial;

        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName { get; set; } = "COM1";

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; } = 8;

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBitsType StopBits { get; set; } = StopBitsType.One;

        /// <summary>
        /// 校验位
        /// </summary>
        public ParityType Parity { get; set; } = ParityType.None;

        /// <summary>
        /// 流控制
        /// </summary>
        public FlowControlType FlowControl { get; set; } = FlowControlType.None;

        /// <summary>
        /// DTR使能
        /// </summary>
        public bool DtrEnable { get; set; } = false;

        /// <summary>
        /// RTS使能
        /// </summary>
        public bool RtsEnable { get; set; } = false;

        public override ProtocolConfigBase Clone()
        {
            return new SerialProtocolConfig
            {
                PortName = this.PortName,
                BaudRate = this.BaudRate,
                DataBits = this.DataBits,
                StopBits = this.StopBits,
                Parity = this.Parity,
                FlowControl = this.FlowControl,
                ConnectionTimeout = this.ConnectionTimeout,
                ReadTimeout = this.ReadTimeout,
                KeepAlive = this.KeepAlive,
                DtrEnable = this.DtrEnable,
                RtsEnable = this.RtsEnable
            };
        }
    }
}

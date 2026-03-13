namespace MainUI.LogicalConfiguration.Instrument.Models
{
    /// <summary>
    /// Modbus协议配置
    /// </summary>
    public class ModbusProtocolConfig : ProtocolConfigBase
    {
        private ProtocolType _protocolType = ProtocolType.ModbusTcp;

        public override ProtocolType ProtocolType => _protocolType;

        /// <summary>
        /// 设置Modbus类型(TCP或RTU)
        /// </summary>
        public void SetModbusType(bool isTcp)
        {
            _protocolType = isTcp ? ProtocolType.ModbusTcp : ProtocolType.ModbusRtu;
        }

        /// <summary>
        /// 从站地址
        /// </summary>
        public byte SlaveAddress { get; set; } = 1;

        /// <summary>
        /// IP地址(TCP模式)
        /// </summary>
        public string IpAddress { get; set; } = "192.168.1.100";

        /// <summary>
        /// 端口号(TCP模式)
        /// </summary>
        public int Port { get; set; } = 502;

        /// <summary>
        /// 串口名称(RTU模式)
        /// </summary>
        public string PortName { get; set; } = "COM1";

        /// <summary>
        /// 波特率(RTU模式)
        /// </summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        /// 数据位(RTU模式)
        /// </summary>
        public int DataBits { get; set; } = 8;

        /// <summary>
        /// 停止位(RTU模式)
        /// </summary>
        public StopBitsType StopBits { get; set; } = StopBitsType.One;

        /// <summary>
        /// 校验位(RTU模式)
        /// </summary>
        public ParityType Parity { get; set; } = ParityType.None;

        /// <summary>
        /// 字节序
        /// </summary>
        public ByteOrder ByteOrder { get; set; } = ByteOrder.BigEndian;

        /// <summary>
        /// 寄存器字节交换
        /// </summary>
        public bool SwapBytes { get; set; } = false;

        /// <summary>
        /// 寄存器字交换
        /// </summary>
        public bool SwapWords { get; set; } = false;

        public override ProtocolConfigBase Clone()
        {
            var config = new ModbusProtocolConfig
            {
                SlaveAddress = this.SlaveAddress,
                IpAddress = this.IpAddress,
                Port = this.Port,
                PortName = this.PortName,
                BaudRate = this.BaudRate,
                DataBits = this.DataBits,
                StopBits = this.StopBits,
                Parity = this.Parity,
                ByteOrder = this.ByteOrder,
                SwapBytes = this.SwapBytes,
                SwapWords = this.SwapWords,
                ConnectionTimeout = this.ConnectionTimeout,
                ReadTimeout = this.ReadTimeout,
                KeepAlive = this.KeepAlive
            };
            config.SetModbusType(this.ProtocolType == ProtocolType.ModbusTcp);
            return config;
        }
    }
}

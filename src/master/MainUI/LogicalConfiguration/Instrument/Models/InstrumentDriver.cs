using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Instrument.Models
{
    /// <summary>
    /// 仪器驱动配置
    /// </summary>
    public class InstrumentDriver
    {
        /// <summary>
        /// 驱动唯一标识
        /// </summary>
        public string DriverId { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 仪器名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 仪器显示名称
        /// </summary>
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// 仪器类别
        /// </summary>
        public InstrumentCategory Category { get; set; } = InstrumentCategory.Other;

        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer { get; set; } = "";

        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; } = "";

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 协议类型
        /// </summary>
        public ProtocolType ProtocolType { get; set; } = ProtocolType.TcpIp;

        /// <summary>
        /// 协议配置(JSON序列化存储)
        /// </summary>
        [JsonProperty("ProtocolConfig")]
        public string ProtocolConfigJson { get; set; } = "";

        /// <summary>
        /// 数据帧配置
        /// </summary>
        public FrameConfig FrameConfig { get; set; } = new();

        /// <summary>
        /// 命令模板列表
        /// </summary>
        public List<InstrumentCommand> Commands { get; set; } = new();

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; } = "1.0.0";

        /// <summary>
        /// 自定义属性
        /// </summary>
        public Dictionary<string, string> CustomProperties { get; set; } = new();

        /// <summary>
        /// 获取协议配置对象
        /// </summary>
        public ProtocolConfigBase GetProtocolConfig()
        {
            if (string.IsNullOrEmpty(ProtocolConfigJson))
                return CreateDefaultProtocolConfig();

            try
            {
                return ProtocolType switch
                {
                    ProtocolType.TcpIp => JsonConvert.DeserializeObject<TcpProtocolConfig>(ProtocolConfigJson),
                    ProtocolType.Serial => JsonConvert.DeserializeObject<SerialProtocolConfig>(ProtocolConfigJson),
                    ProtocolType.ModbusTcp or ProtocolType.ModbusRtu => JsonConvert.DeserializeObject<ModbusProtocolConfig>(ProtocolConfigJson),
                    ProtocolType.Http => JsonConvert.DeserializeObject<HttpProtocolConfig>(ProtocolConfigJson),
                    _ => CreateDefaultProtocolConfig()
                };
            }
            catch
            {
                return CreateDefaultProtocolConfig();
            }
        }

        /// <summary>
        /// 设置协议配置对象
        /// </summary>
        public void SetProtocolConfig(ProtocolConfigBase config)
        {
            if (config != null)
            {
                ProtocolConfigJson = JsonConvert.SerializeObject(config, Formatting.Indented);
            }
        }

        /// <summary>
        /// 创建默认协议配置
        /// </summary>
        private ProtocolConfigBase CreateDefaultProtocolConfig()
        {
            return ProtocolType switch
            {
                ProtocolType.TcpIp => new TcpProtocolConfig(),
                ProtocolType.Serial => new SerialProtocolConfig(),
                ProtocolType.ModbusTcp => new ModbusProtocolConfig(),
                ProtocolType.ModbusRtu => new ModbusProtocolConfig { },
                ProtocolType.Http => new HttpProtocolConfig(),
                _ => new TcpProtocolConfig()
            };
        }

        /// <summary>
        /// 根据名称获取命令
        /// </summary>
        public InstrumentCommand GetCommand(string commandName)
        {
            return Commands?.Find(c =>
                c.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase) ||
                c.DisplayName.Equals(commandName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 克隆驱动配置
        /// </summary>
        public InstrumentDriver Clone()
        {
            var json = JsonConvert.SerializeObject(this);
            var clone = JsonConvert.DeserializeObject<InstrumentDriver>(json);
            clone.DriverId = Guid.NewGuid().ToString("N");
            clone.Name = $"{this.Name}_Copy";
            clone.DisplayName = $"{this.DisplayName} (副本)";
            return clone;
        }
    }
}

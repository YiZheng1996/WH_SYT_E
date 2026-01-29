using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace MainUI.LogicalConfiguration.Instrument.Models
{
    #region 枚举定义

    /// <summary>
    /// 通讯协议类型
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProtocolType
    {
        [Description("TCP/IP")]
        TcpIp,

        [Description("串口")]
        Serial,

        [Description("Modbus TCP")]
        ModbusTcp,

        [Description("Modbus RTU")]
        ModbusRtu,

        [Description("HTTP/REST")]
        Http,

        [Description("UDP")]
        Udp
    }

    /// <summary>
    /// 仪器类别
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InstrumentCategory
    {
        [Description("电源")]
        PowerSupply,

        [Description("万用表")]
        Multimeter,

        [Description("示波器")]
        Oscilloscope,

        [Description("信号发生器")]
        SignalGenerator,

        [Description("传感器")]
        Sensor,

        [Description("温控器")]
        TemperatureController,

        [Description("流量计")]
        FlowMeter,

        [Description("压力计")]
        PressureGauge,

        [Description("PLC")]
        PLC,

        [Description("其他")]
        Other
    }

    /// <summary>
    /// 命令操作类型
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommandType
    {
        [Description("读取")]
        Read,

        [Description("写入")]
        Write,

        [Description("查询")]
        Query,

        [Description("控制")]
        Control,

        [Description("自定义")]
        Custom
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataType
    {
        [Description("字符串")]
        String,

        [Description("整数")]
        Integer,

        [Description("浮点数")]
        Double,

        [Description("布尔")]
        Boolean,

        [Description("字节数组")]
        ByteArray,

        [Description("十六进制")]
        Hex
    }

    /// <summary>
    /// 校验算法类型
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChecksumType
    {
        [Description("无")]
        None,

        [Description("CRC16")]
        CRC16,

        [Description("CRC32")]
        CRC32,

        [Description("LRC")]
        LRC,

        [Description("异或校验")]
        XOR,

        [Description("累加和")]
        Checksum,

        [Description("Modbus CRC")]
        ModbusCRC
    }

    /// <summary>
    /// 字节序
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ByteOrder
    {
        [Description("大端序")]
        BigEndian,

        [Description("小端序")]
        LittleEndian
    }

    /// <summary>
    /// 串口校验位
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ParityType
    {
        [Description("无")]
        None,

        [Description("奇校验")]
        Odd,

        [Description("偶校验")]
        Even,

        [Description("标记")]
        Mark,

        [Description("空格")]
        Space
    }

    /// <summary>
    /// 停止位
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StopBitsType
    {
        [Description("1位")]
        One,

        [Description("1.5位")]
        OnePointFive,

        [Description("2位")]
        Two
    }

    /// <summary>
    /// 流控制
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FlowControlType
    {
        [Description("无")]
        None,

        [Description("硬件")]
        Hardware,

        [Description("软件(XON/XOFF)")]
        Software
    }

    /// <summary>
    /// 失败处理策略
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FailureStrategy
    {
        [Description("终止流程")]
        Abort,

        [Description("继续执行")]
        Continue,

        [Description("重试")]
        Retry,

        [Description("跳转到指定步骤")]
        JumpToStep
    }

    #endregion

    #region 协议配置类

    /// <summary>
    /// 协议配置基类
    /// </summary>
    public abstract class ProtocolConfigBase
    {
        /// <summary>
        /// 协议类型
        /// </summary>
        public abstract ProtocolType ProtocolType { get; }

        /// <summary>
        /// 连接超时(毫秒)
        /// </summary>
        public int ConnectionTimeout { get; set; } = 5000;

        /// <summary>
        /// 读取超时(毫秒)
        /// </summary>
        public int ReadTimeout { get; set; } = 3000;

        /// <summary>
        /// 写入超时(毫秒)
        /// </summary>
        public int WriteTimeout { get; set; } = 3000;

        /// <summary>
        /// 是否保持连接
        /// </summary>
        public bool KeepAlive { get; set; } = true;

        /// <summary>
        /// 创建配置副本
        /// </summary>
        public abstract ProtocolConfigBase Clone();
    }

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
                WriteTimeout = this.WriteTimeout,
                KeepAlive = this.KeepAlive,
                ReceiveBufferSize = this.ReceiveBufferSize,
                SendBufferSize = this.SendBufferSize
            };
        }
    }

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
                WriteTimeout = this.WriteTimeout,
                KeepAlive = this.KeepAlive,
                DtrEnable = this.DtrEnable,
                RtsEnable = this.RtsEnable
            };
        }
    }

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
                WriteTimeout = this.WriteTimeout,
                KeepAlive = this.KeepAlive
            };
            config.SetModbusType(this.ProtocolType == ProtocolType.ModbusTcp);
            return config;
        }
    }

    /// <summary>
    /// HTTP协议配置
    /// </summary>
    public class HttpProtocolConfig : ProtocolConfigBase
    {
        public override ProtocolType ProtocolType => ProtocolType.Http;

        /// <summary>
        /// 基础URL
        /// </summary>
        public string BaseUrl { get; set; } = "http://192.168.1.100:8080";

        /// <summary>
        /// 认证类型
        /// </summary>
        public string AuthType { get; set; } = "None"; // None, Basic, Bearer

        /// <summary>
        /// 用户名(Basic认证)
        /// </summary>
        public string Username { get; set; } = "";

        /// <summary>
        /// 密码(Basic认证)
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// Token(Bearer认证)
        /// </summary>
        public string BearerToken { get; set; } = "";

        /// <summary>
        /// 默认请求头
        /// </summary>
        public Dictionary<string, string> DefaultHeaders { get; set; } = new();

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; } = "application/json";

        public override ProtocolConfigBase Clone()
        {
            return new HttpProtocolConfig
            {
                BaseUrl = this.BaseUrl,
                AuthType = this.AuthType,
                Username = this.Username,
                Password = this.Password,
                BearerToken = this.BearerToken,
                DefaultHeaders = new Dictionary<string, string>(this.DefaultHeaders),
                ContentType = this.ContentType,
                ConnectionTimeout = this.ConnectionTimeout,
                ReadTimeout = this.ReadTimeout,
                WriteTimeout = this.WriteTimeout,
                KeepAlive = this.KeepAlive
            };
        }
    }

    #endregion

    #region 数据帧配置

    /// <summary>
    /// 数据帧格式配置
    /// </summary>
    public class FrameConfig
    {
        /// <summary>
        /// 是否启用帧配置
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// 帧头(十六进制字符串，如"AA55")
        /// </summary>
        public string FrameHeader { get; set; } = "";

        /// <summary>
        /// 帧尾(十六进制字符串，如"0D0A"表示回车换行)
        /// </summary>
        public string FrameFooter { get; set; } = "";

        /// <summary>
        /// 长度字段位置(从0开始，-1表示无长度字段)
        /// </summary>
        public int LengthFieldPosition { get; set; } = -1;

        /// <summary>
        /// 长度字段字节数
        /// </summary>
        public int LengthFieldSize { get; set; } = 2;

        /// <summary>
        /// 长度是否包含帧头帧尾
        /// </summary>
        public bool LengthIncludesHeaderFooter { get; set; } = false;

        /// <summary>
        /// 校验算法
        /// </summary>
        public ChecksumType ChecksumType { get; set; } = ChecksumType.None;

        /// <summary>
        /// 校验字段位置(-1表示在帧尾之前)
        /// </summary>
        public int ChecksumPosition { get; set; } = -1;

        /// <summary>
        /// 校验字段字节数
        /// </summary>
        public int ChecksumSize { get; set; } = 2;

        /// <summary>
        /// 校验计算起始位置
        /// </summary>
        public int ChecksumStartPosition { get; set; } = 0;

        /// <summary>
        /// 校验计算结束位置(-1表示到校验字段之前)
        /// </summary>
        public int ChecksumEndPosition { get; set; } = -1;

        /// <summary>
        /// 字节序
        /// </summary>
        public ByteOrder ByteOrder { get; set; } = ByteOrder.BigEndian;

        /// <summary>
        /// 数据区起始位置
        /// </summary>
        public int DataStartPosition { get; set; } = 0;

        /// <summary>
        /// 响应结束标记(用于判断接收完成)
        /// </summary>
        public string ResponseTerminator { get; set; } = "";

        /// <summary>
        /// 固定响应长度(-1表示不固定)
        /// </summary>
        public int FixedResponseLength { get; set; } = -1;
    }

    #endregion

    #region 命令模板

    /// <summary>
    /// 命令参数定义
    /// </summary>
    public class CommandParameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 参数显示名称
        /// </summary>
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// 参数数据类型
        /// </summary>
        public DataType DataType { get; set; } = DataType.String;

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; } = "";

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; } = true;

        /// <summary>
        /// 参数说明
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 可选值列表(用于下拉选择)
        /// </summary>
        public List<string> Options { get; set; } = new();

        /// <summary>
        /// 最小值(数值类型)
        /// </summary>
        public double? MinValue { get; set; }

        /// <summary>
        /// 最大值(数值类型)
        /// </summary>
        public double? MaxValue { get; set; }
    }

    /// <summary>
    /// 响应解析规则
    /// </summary>
    public class ResponseParseRule
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 目标变量名(解析结果存储位置)
        /// </summary>
        public string TargetVariable { get; set; } = "";

        /// <summary>
        /// 解析类型
        /// </summary>
        public string ParseType { get; set; } = "Position"; // Position, Delimiter, Regex, Json

        /// <summary>
        /// 起始位置(Position类型)
        /// </summary>
        public int StartPosition { get; set; } = 0;

        /// <summary>
        /// 长度(Position类型)
        /// </summary>
        public int Length { get; set; } = -1;

        /// <summary>
        /// 分隔符(Delimiter类型)
        /// </summary>
        public string Delimiter { get; set; } = ",";

        /// <summary>
        /// 取第几段(Delimiter类型，从0开始)
        /// </summary>
        public int SegmentIndex { get; set; } = 0;

        /// <summary>
        /// 正则表达式(Regex类型)
        /// </summary>
        public string RegexPattern { get; set; } = "";

        /// <summary>
        /// 正则分组索引
        /// </summary>
        public int RegexGroupIndex { get; set; } = 1;

        /// <summary>
        /// JSON路径(Json类型)
        /// </summary>
        public string JsonPath { get; set; } = "";

        /// <summary>
        /// 目标数据类型
        /// </summary>
        public DataType TargetDataType { get; set; } = DataType.String;

        /// <summary>
        /// 数值缩放因子
        /// </summary>
        public double ScaleFactor { get; set; } = 1.0;

        /// <summary>
        /// 数值偏移量
        /// </summary>
        public double Offset { get; set; } = 0;
    }

    /// <summary>
    /// 命令模板
    /// </summary>
    public class InstrumentCommand
    {
        /// <summary>
        /// 命令唯一标识
        /// </summary>
        public string CommandId { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 命令名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 命令显示名称
        /// </summary>
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType CommandType { get; set; } = CommandType.Query;

        /// <summary>
        /// 命令描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 请求报文模板
        /// 支持变量占位符: {参数名}
        /// 支持变量引用: {$变量名}
        /// </summary>
        public string RequestTemplate { get; set; } = "";

        /// <summary>
        /// 请求数据类型
        /// </summary>
        public DataType RequestDataType { get; set; } = DataType.String;

        /// <summary>
        /// 命令参数定义
        /// </summary>
        public List<CommandParameter> Parameters { get; set; } = new();

        /// <summary>
        /// 响应解析规则列表
        /// </summary>
        public List<ResponseParseRule> ParseRules { get; set; } = new();

        /// <summary>
        /// 期望响应格式(用于验证)
        /// </summary>
        public string ExpectedResponsePattern { get; set; } = "";

        /// <summary>
        /// 成功响应标识
        /// </summary>
        public string SuccessIndicator { get; set; } = "";

        /// <summary>
        /// 失败响应标识
        /// </summary>
        public string FailureIndicator { get; set; } = "";

        /// <summary>
        /// 命令专用超时(毫秒，0表示使用默认)
        /// </summary>
        public int Timeout { get; set; } = 0;

        /// <summary>
        /// 发送后延时(毫秒)
        /// </summary>
        public int DelayAfterSend { get; set; } = 0;

        /// <summary>
        /// 是否等待响应
        /// </summary>
        public bool WaitForResponse { get; set; } = true;

        /// <summary>
        /// 排序顺序(用于界面显示)
        /// </summary>
        public int SortOrder { get; set; } = 0;
    }

    #endregion

    #region 仪器驱动配置

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

    #endregion

    #region 通讯结果

    /// <summary>
    /// 通讯执行结果
    /// </summary>
    public class CommunicationResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; } = "";

        /// <summary>
        /// 原始响应数据(字节数组)
        /// </summary>
        public byte[] RawResponse { get; set; }

        /// <summary>
        /// 响应字符串
        /// </summary>
        public string ResponseString { get; set; } = "";

        /// <summary>
        /// 解析后的数据(变量名->值)
        /// </summary>
        public Dictionary<string, object> ParsedData { get; set; } = new();

        /// <summary>
        /// 发送的数据(用于调试)
        /// </summary>
        public byte[] SentData { get; set; }

        /// <summary>
        /// 发送的字符串(用于调试)
        /// </summary>
        public string SentString { get; set; } = "";

        /// <summary>
        /// 执行耗时(毫秒)
        /// </summary>
        public long ElapsedMilliseconds { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建成功结果
        /// </summary>
        public static CommunicationResult Successful(string response = "", Dictionary<string, object> parsedData = null)
        {
            return new CommunicationResult
            {
                Success = true,
                ResponseString = response,
                ParsedData = parsedData ?? new Dictionary<string, object>()
            };
        }

        /// <summary>
        /// 创建失败结果
        /// </summary>
        public static CommunicationResult Failed(string errorMessage)
        {
            return new CommunicationResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }

    #endregion
}
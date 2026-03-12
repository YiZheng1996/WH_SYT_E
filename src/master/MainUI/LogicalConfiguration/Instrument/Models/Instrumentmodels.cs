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

        [Description("电阻测试仪")]
        ResistanceTest,

        [Description("绝缘耐压仪")]
        InsulationVoltage,

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
  
}
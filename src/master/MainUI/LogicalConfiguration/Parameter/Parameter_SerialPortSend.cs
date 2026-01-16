using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO.Ports;

namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 串口发送参数
    /// 用于配置串口数据发送步骤的所有参数
    /// </summary>
    public class Parameter_SerialPortSend
    {
        #region 基本信息

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = "串口数据发送";

        /// <summary>
        /// 是否启用此步骤
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 执行条件（可选，为空时总是执行）
        /// </summary>
        public string Condition { get; set; } = "";

        #endregion

        #region 串口设置

        /// <summary>
        /// 串口名称 (如 COM1, COM2)
        /// </summary>
        public string PortName { get; set; } = "COM1";

        /// <summary>
        /// 波特率
        /// 常用值: 9600, 19200, 38400, 57600, 115200
        /// </summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        /// 校验位
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Parity Parity { get; set; } = Parity.None;

        /// <summary>
        /// 数据位
        /// 常用值: 7, 8
        /// </summary>
        public int DataBits { get; set; } = 8;

        /// <summary>
        /// 停止位
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public StopBits StopBits { get; set; } = StopBits.One;

        /// <summary>
        /// 流控制
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Handshake Handshake { get; set; } = Handshake.None;

        #endregion

        #region 数据设置

        /// <summary>
        /// 数据格式
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Parameter_EthernetSend.DataFormatType DataFormat { get; set; } = Parameter_EthernetSend.DataFormatType.Text;

        /// <summary>
        /// 文本编码
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Parameter_EthernetSend.EncodingType Encoding { get; set; } = Parameter_EthernetSend.EncodingType.UTF8;

        /// <summary>
        /// 发送内容（支持变量引用 {变量名}）
        /// </summary>
        public string SendContent { get; set; } = "";

        /// <summary>
        /// 是否追加换行符
        /// </summary>
        public bool AppendNewLine { get; set; } = false;

        /// <summary>
        /// 换行符类型 (\r\n, \n, \r)
        /// </summary>
        public string NewLineType { get; set; } = "\r\n";

        #endregion

        #region 响应设置

        /// <summary>
        /// 是否等待响应
        /// </summary>
        public bool WaitResponse { get; set; } = false;

        /// <summary>
        /// 响应超时(毫秒)
        /// </summary>
        public int ResponseTimeout { get; set; } = 3000;

        /// <summary>
        /// 响应保存变量名（将响应内容保存到此变量）
        /// </summary>
        public string ResponseVariableName { get; set; } = "";

        #endregion

        #region 其他设置

        /// <summary>
        /// 发送后是否关闭串口
        /// </summary>
        public bool CloseAfterSend { get; set; } = false;

        #endregion

        #region 方法

        /// <summary>
        /// 获取参数的显示描述
        /// </summary>
        public string GetDisplayDescription()
        {
            return $"{PortName} {BaudRate}bps {DataBits}{GetParityChar()}{GetStopBitsChar()}";
        }

        /// <summary>
        /// 获取校验位字符
        /// </summary>
        private char GetParityChar()
        {
            return Parity switch
            {
                Parity.None => 'N',
                Parity.Odd => 'O',
                Parity.Even => 'E',
                Parity.Mark => 'M',
                Parity.Space => 'S',
                _ => 'N'
            };
        }

        /// <summary>
        /// 获取停止位字符
        /// </summary>
        private string GetStopBitsChar()
        {
            return StopBits switch
            {
                StopBits.One => "1",
                StopBits.OnePointFive => "1.5",
                StopBits.Two => "2",
                _ => "1"
            };
        }

        /// <summary>
        /// 验证参数是否有效
        /// </summary>
        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(PortName))
                return (false, "串口名称不能为空");

            if (BaudRate <= 0)
                return (false, "波特率必须大于0");

            if (DataBits < 5 || DataBits > 8)
                return (false, "数据位必须在5-8之间");

            if (string.IsNullOrWhiteSpace(SendContent))
                return (false, "发送内容不能为空");

            return (true, string.Empty);
        }

        /// <summary>
        /// 克隆参数对象
        /// </summary>
        public Parameter_SerialPortSend Clone()
        {
            return new Parameter_SerialPortSend
            {
                Description = this.Description,
                IsEnabled = this.IsEnabled,
                Condition = this.Condition,
                PortName = this.PortName,
                BaudRate = this.BaudRate,
                Parity = this.Parity,
                DataBits = this.DataBits,
                StopBits = this.StopBits,
                Handshake = this.Handshake,
                DataFormat = this.DataFormat,
                Encoding = this.Encoding,
                SendContent = this.SendContent,
                AppendNewLine = this.AppendNewLine,
                NewLineType = this.NewLineType,
                WaitResponse = this.WaitResponse,
                ResponseTimeout = this.ResponseTimeout,
                ResponseVariableName = this.ResponseVariableName,
                CloseAfterSend = this.CloseAfterSend
            };
        }

        /// <summary>
        /// 获取常用波特率列表
        /// </summary>
        public static int[] GetCommonBaudRates()
        {
            return new[] { 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600 };
        }

        /// <summary>
        /// 获取可用串口列表
        /// </summary>
        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        #endregion
    }
}
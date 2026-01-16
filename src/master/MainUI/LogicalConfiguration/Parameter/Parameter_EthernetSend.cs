using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Sockets;

namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 以太网发送参数
    /// 用于配置以太网数据发送步骤的所有参数
    /// </summary>
    public class Parameter_EthernetSend
    {
        #region 基本信息

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = "以太网数据发送";

        /// <summary>
        /// 是否启用此步骤
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 执行条件（可选，为空时总是执行）
        /// </summary>
        public string Condition { get; set; } = "";

        #endregion

        #region 连接设置

        /// <summary>
        /// 目标IP地址
        /// </summary>
        public string IPAddress { get; set; } = "192.168.1.100";

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 502;

        /// <summary>
        /// 协议类型 (TCP/UDP)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ProtocolType Protocol { get; set; } = ProtocolType.Tcp;

        /// <summary>
        /// 连接超时(毫秒)
        /// </summary>
        public int ConnectTimeout { get; set; } = 5000;

        /// <summary>
        /// 发送超时(毫秒)
        /// </summary>
        public int SendTimeout { get; set; } = 3000;

        #endregion

        #region 数据设置

        /// <summary>
        /// 数据格式
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DataFormatType DataFormat { get; set; } = DataFormatType.Text;

        /// <summary>
        /// 文本编码
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EncodingType Encoding { get; set; } = EncodingType.UTF8;

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
        /// 发送后是否断开连接
        /// </summary>
        public bool DisconnectAfterSend { get; set; } = false;

        #endregion

        #region 方法

        /// <summary>
        /// 获取参数的显示描述
        /// </summary>
        public string GetDisplayDescription()
        {
            var protocol = Protocol == ProtocolType.Tcp ? "TCP" : "UDP";
            return $"{protocol} → {IPAddress}:{Port}";
        }

        /// <summary>
        /// 验证参数是否有效
        /// </summary>
        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(IPAddress))
                return (false, "IP地址不能为空");

            if (!System.Net.IPAddress.TryParse(IPAddress, out _))
                return (false, "IP地址格式不正确");

            if (Port < 1 || Port > 65535)
                return (false, "端口号必须在1-65535之间");

            if (string.IsNullOrWhiteSpace(SendContent))
                return (false, "发送内容不能为空");

            return (true, string.Empty);
        }

        /// <summary>
        /// 克隆参数对象
        /// </summary>
        public Parameter_EthernetSend Clone()
        {
            return new Parameter_EthernetSend
            {
                Description = this.Description,
                IsEnabled = this.IsEnabled,
                Condition = this.Condition,
                IPAddress = this.IPAddress,
                Port = this.Port,
                Protocol = this.Protocol,
                ConnectTimeout = this.ConnectTimeout,
                SendTimeout = this.SendTimeout,
                DataFormat = this.DataFormat,
                Encoding = this.Encoding,
                SendContent = this.SendContent,
                AppendNewLine = this.AppendNewLine,
                NewLineType = this.NewLineType,
                WaitResponse = this.WaitResponse,
                ResponseTimeout = this.ResponseTimeout,
                ResponseVariableName = this.ResponseVariableName,
                DisconnectAfterSend = this.DisconnectAfterSend
            };
        }


        /// <summary>
        /// 数据格式类型
        /// </summary>
        public enum DataFormatType
        {
            /// <summary>文本格式</summary>
            Text,
            /// <summary>十六进制格式</summary>
            Hex,
            /// <summary>Base64格式</summary>
            Base64,
            /// <summary>JSON格式</summary>
            Json
        }

        /// <summary>
        /// 编码类型
        /// </summary>
        public enum EncodingType
        {
            /// <summary>UTF-8编码</summary>
            UTF8,
            /// <summary>ASCII编码</summary>
            ASCII,
            /// <summary>GB2312编码</summary>
            GB2312,
            /// <summary>Unicode编码</summary>
            Unicode
        }


        #endregion
    }
}
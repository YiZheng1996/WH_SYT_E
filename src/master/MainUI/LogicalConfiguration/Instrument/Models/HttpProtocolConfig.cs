namespace MainUI.LogicalConfiguration.Instrument.Models
{
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
                KeepAlive = this.KeepAlive
            };
        }
    }
}

namespace MainUI.LogicalConfiguration.Instrument.Models
{
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
        /// 解析结果集合，Key=规则名称，Value=解析后的值
        /// 由调用方（步骤执行层）决定如何使用这些值
        /// </summary>
        public Dictionary<string, object> ParsedValues { get; set; } = [];

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
}

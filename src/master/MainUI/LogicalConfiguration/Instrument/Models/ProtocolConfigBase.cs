namespace MainUI.LogicalConfiguration.Instrument.Models
{
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
}

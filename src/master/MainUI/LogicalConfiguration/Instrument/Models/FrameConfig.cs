namespace MainUI.LogicalConfiguration.Instrument.Models
{
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
}

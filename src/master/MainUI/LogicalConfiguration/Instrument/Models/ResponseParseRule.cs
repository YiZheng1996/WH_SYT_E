namespace MainUI.LogicalConfiguration.Instrument.Models
{
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
}

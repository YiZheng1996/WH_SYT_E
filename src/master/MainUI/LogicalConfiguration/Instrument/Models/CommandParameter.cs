namespace MainUI.LogicalConfiguration.Instrument.Models
{
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

}

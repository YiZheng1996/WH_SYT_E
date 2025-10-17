
namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 读取单元格参数类
    /// 支持:
    /// 1. 批量读取多个单元格
    /// 2. 读取结果保存到变量
    /// 3. 数据类型转换
    /// 4. 默认值设置
    /// </summary>
    public class Parameter_ReadCells
    {
        /// <summary>
        /// 工作表名称(可选,为空则使用第一个工作表)
        /// </summary>
        public string SheetName { get; set; } = "Sheet1";

        /// <summary>
        /// 读取项列表 (批量读取模式)
        /// </summary>
        public List<ReadCellItem> ReadItems { get; set; } = [];

        /// <summary>
        /// 读取失败时的处理策略
        /// </summary>
        public ReadFailureAction FailureAction { get; set; } = ReadFailureAction.UseDefault;
    }

    /// <summary>
    /// 读取单元格项
    /// </summary>
    public class ReadCellItem
    {
        /// <summary>
        /// 单元格地址 (例如: A1, B2, C3)
        /// </summary>
        public string CellAddress { get; set; }

        /// <summary>
        /// 保存到的变量名
        /// </summary>
        public string SaveToVariable { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public CellDataType DataType { get; set; } = CellDataType.String;

        /// <summary>
        /// 默认值（读取失败时使用）
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 格式化字符串（用于日期、数值等）
        /// </summary>
        public string FormatString { get; set; }

        /// <summary>
        /// 描述说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 预览值（用于界面显示,不序列化保存）
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string PreviewValue { get; set; }

        /// <summary>
        /// 是否启用此项读取
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }

    /// <summary>
    /// 单元格数据类型
    /// </summary>
    public enum CellDataType
    {
        /// <summary>
        /// 字符串类型
        /// </summary>
        String = 0,

        /// <summary>
        /// 整数类型
        /// </summary>
        Integer = 1,

        /// <summary>
        /// 小数类型
        /// </summary>
        Decimal = 2,

        /// <summary>
        /// 布尔类型
        /// </summary>
        Boolean = 3,

        /// <summary>
        /// 日期时间类型
        /// </summary>
        DateTime = 4
    }

    /// <summary>
    /// 读取失败时的处理策略
    /// </summary>
    public enum ReadFailureAction
    {
        /// <summary>
        /// 使用默认值
        /// </summary>
        UseDefault = 0,

        /// <summary>
        /// 抛出异常
        /// </summary>
        ThrowError = 1,

        /// <summary>
        /// 跳过该项
        /// </summary>
        Skip = 2,

        /// <summary>
        /// 提示用户
        /// </summary>
        Prompt = 3
    }
}
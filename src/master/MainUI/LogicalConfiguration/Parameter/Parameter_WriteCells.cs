namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 写入单元格参数类
    /// 支持固定值、变量、表达式等多种数据源
    /// </summary>
    public class Parameter_WriteCells
    {
        /// <summary>
        /// 工作表名称(可选,为空则使用第一个工作表)
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 写入项列表
        /// </summary>
        public List<WriteCellItem> Items { get; set; } = [];
    }

    /// <summary>
    /// 单元格写入项 - 灵活配置
    /// </summary>
    public class WriteCellItem
    {
        /// <summary>
        /// 单元格地址 (例如: A1, B2, C3)
        /// </summary>
        public string CellAddress { get; set; }

        /// <summary>
        /// 数据源类型
        /// </summary>
        public CellsDataSourceType SourceType { get; set; }

        /// <summary>
        /// 固定值 (当SourceType为FixedValue时使用)
        /// </summary>
        public string FixedValue { get; set; }

        /// <summary>
        /// 变量名 (当SourceType为Variable时使用)
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 表达式 (当SourceType为Expression时使用)
        /// 支持: {变量名}, 运算符, 函数等
        /// 例如: {Var1} + {Var2}, DateTime.Now.ToString("yyyy-MM-dd")
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 系统属性路径 (当SourceType为SystemProperty时使用)
        /// 例如: NewUsers.NewUserInfo.Username
        ///       VarHelper.TestViewModel.ModelName
        ///       DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        /// </summary>
        public string PropertyPath { get; set; }

        /// <summary>
        /// 格式化字符串 (可选)
        /// 用于数值、日期等的格式化
        /// </summary>
        public string FormatString { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 数据源类型枚举
    /// </summary>
    public enum CellsDataSourceType
    {
        /// <summary>
        /// 固定值 - 用户直接输入的文本
        /// </summary>
        FixedValue = 0,

        /// <summary>
        /// 变量 - 从工作流变量中获取
        /// </summary>
        Variable = 1,

        /// <summary>
        /// 表达式 - 支持运算、函数等
        /// 例如: {Var1} * 2 + 10
        ///       DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")
        /// </summary>
        Expression = 2,

        /// <summary>
        /// 系统属性 - 通过反射获取系统对象的属性
        /// 例如: NewUsers.NewUserInfo.Username
        ///       VarHelper.TestViewModel.ModelName
        /// </summary>
        SystemProperty = 3
    }
}
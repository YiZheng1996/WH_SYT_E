using System.ComponentModel;

namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 项点变量赋值参数
    /// </summary>
    public class Parameter_VariableAssignment
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string TargetVarName { get; set; }

        /// <summary>
        /// 赋值方式
        /// </summary>
        public VariableAssignmentType AssignmentType { get; set; } = VariableAssignmentType.DirectAssignment;

        /// <summary>
        /// 赋值表达式（用于直接赋值、表达式计算、变量复制）
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 数据源配置（用于PLC读取）
        /// </summary>
        public DataSourceConfig DataSource { get; set; } = new DataSourceConfig();

        /// <summary>
        /// 是否赋值
        /// </summary>
        public bool IsAssignment { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 数据源配置
    /// </summary>
    public class DataSourceConfig
    {
        /// <summary>
        /// 数据源类型
        /// </summary>
        public DataSourceType SourceType { get; set; } = DataSourceType.Variable;

        /// <summary>
        /// 变量名（当数据源类型为变量时）
        /// </summary>
        public string VariableName { get; set; } = "";

        /// <summary>
        /// PLC地址配置（当数据源类型为PLC时）
        /// </summary>
        public PlcAddressConfig PlcConfig { get; set; } = new PlcAddressConfig();
    }
}

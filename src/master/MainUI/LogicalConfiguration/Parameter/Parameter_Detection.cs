using System.ComponentModel;

namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 检测工具参数类
    /// </summary>
    public class Parameter_Detection
    {
        /// <summary>
        /// 检测项名称
        /// </summary>
        [DisplayName("检测项名称")]
        [Description("用于标识此检测步骤的名称")]
        public string DetectionName { get; set; } = "";

        /// <summary>
        /// 检测类型
        /// </summary>
        [DisplayName("检测类型")]
        [Description("选择检测的类型")]
        public DetectionType Type { get; set; } = DetectionType.ValueRange;

        /// <summary>
        /// 数据源配置
        /// </summary>
        [DisplayName("数据源")]
        [Description("配置检测数据的来源")]
        public DataSourceConfig DataSource { get; set; } = new DataSourceConfig();

        /// <summary>
        /// 检测条件配置
        /// </summary>
        [DisplayName("检测条件")]
        [Description("配置检测的判断条件")]
        public DetectionCondition Condition { get; set; } = new DetectionCondition();

        /// <summary>
        /// 结果处理配置
        /// </summary>
        [DisplayName("结果处理")]
        [Description("配置检测结果的处理方式")]
        public ResultHandling ResultHandling { get; set; } = new ResultHandling();

        /// <summary>
        /// 刷新频率（毫秒）- 检测间隔时间
        /// 默认100ms，最小10ms，最大10000ms
        /// </summary>
        [DisplayName("刷新频率")]
        [Description("检测间隔时间，单位毫秒。值越小检测越频繁")]
        public int RefreshRateMs { get; set; } = 100;

        /// <summary>
        /// 超时设置（毫秒）
        /// </summary>
        [DisplayName("超时时间")]
        [Description("检测超时时间，单位毫秒，0表示不限制")]
        public int TimeoutMs { get; set; } = 5000;

        /// <summary>
        /// 重试次数
        /// </summary>
        [DisplayName("重试次数")]
        [Description("检测失败时的重试次数")]
        public int RetryCount { get; set; } = 0;

        /// <summary>
        /// 重试间隔（毫秒）
        /// </summary>
        [DisplayName("重试间隔")]
        [Description("重试之间的等待时间，单位毫秒")]
        public int RetryIntervalMs { get; set; } = 1000;
    }

    /// <summary>
    /// 检测类型枚举
    /// </summary>
    public enum DetectionType
    {
        [Description("数值范围检测")]
        ValueRange,

        [Description("相等性检测")]
        Equality,

        [Description("状态检测")]
        Status,

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

    /// <summary>
    /// 数据源类型
    /// </summary>
    public enum DataSourceType
    {
        [Description("系统变量")]
        Variable,

        [Description("PLC地址")]
        PLC,

    }

    /// <summary>
    /// PLC地址配置
    /// </summary>
    public class PlcAddressConfig
    {
        /// <summary>
        /// PLC模块名
        /// </summary>
        public string ModuleName { get; set; } = "";

        /// <summary>
        /// PLC地址/标签名
        /// </summary>
        public string Address { get; set; } = "";

        /// <summary>
        /// 数据类型
        /// </summary>
        //public string DataType { get; set; } = "Float";

        /// <summary>
        /// 转换为PLC读取项（用于读取操作）
        /// </summary>
        /// <returns></returns>
        public PlcReadItem ToPlcReadItem()
        {
            return new PlcReadItem
            {
                PlcModuleName = this.ModuleName,
                PlcKeyName = this.Address
            };
        }
    }

    /// <summary>
    /// 检测条件配置
    /// </summary>
    public class DetectionCondition
    {
        /// <summary>
        /// 最小值（范围检测时使用）
        /// </summary>
        public double MinValue { get; set; } = 0;

        /// <summary>
        /// 最大值（范围检测时使用）
        /// </summary>
        public double MaxValue { get; set; } = 100;

        /// <summary>
        /// 目标值（相等性检测时使用）
        /// </summary>
        public string TargetValue { get; set; } = "";

        /// <summary>
        /// 阈值（阈值检测时使用）
        /// </summary>
        public double ThresholdValue { get; set; } = 0;

        /// <summary>
        /// 比较操作符
        /// </summary>
        public ComparisonOperator Operator { get; set; } = ComparisonOperator.GreaterThan;

        /// <summary>
        /// 自定义判断表达式
        /// </summary>
        //public string CustomExpression { get; set; } = "";

        /// <summary>
        /// 容差值
        /// </summary>
        public double Tolerance { get; set; } = 0.1;
    }

    /// <summary>
    /// 比较操作符
    /// </summary>
    public enum ComparisonOperator
    {
        [Description("大于")]
        GreaterThan,

        [Description("大于等于")]
        GreaterThanOrEqual,

        [Description("小于")]
        LessThan,

        [Description("小于等于")]
        LessThanOrEqual,

        [Description("等于")]
        Equal,

        [Description("不等于")]
        NotEqual
    }

    /// <summary>
    /// 结果处理配置
    /// </summary>
    public class ResultHandling
    {
        /// <summary>
        /// 是否保存检测结果到变量
        /// </summary>
        public bool SaveToVariable { get; set; } = true;

        /// <summary>
        /// 结果变量名
        /// </summary>
        public string ResultVariableName { get; set; } = "";

        /// <summary>
        /// 是否保存检测值到变量
        /// </summary>
        public bool SaveValueToVariable { get; set; } = false;

        /// <summary>
        /// 检测值变量名
        /// </summary>
        public string ValueVariableName { get; set; } = "";

        /// <summary>
        /// 检测失败时的行为
        /// </summary>
        public FailureAction OnFailure { get; set; } = FailureAction.Continue;

        /// <summary>
        /// 失败时跳转的步骤索引
        /// </summary>
        public int FailureStepIndex { get; set; } = -1;

        /// <summary>
        /// 成功时跳转的步骤索引
        /// </summary>
        public int SuccessStepIndex { get; set; } = -1;

        /// <summary>
        /// 是否显示检测结果
        /// </summary>
        public bool ShowResult { get; set; } = true;

        /// <summary>
        /// 自定义消息模板
        /// </summary>
        public string MessageTemplate { get; set; } = "检测项 {DetectionName}: {Result}";
    }

    /// <summary>
    /// 失败处理行为
    /// </summary>
    public enum FailureAction
    {
        [Description("继续执行")]
        Continue,

        [Description("停止流程")]
        Stop,

        [Description("跳转步骤")]
        Jump,

        [Description("显示确认框")]
        Confirm
    }
}

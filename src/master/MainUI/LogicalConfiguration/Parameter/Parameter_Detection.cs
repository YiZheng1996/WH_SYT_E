using System.ComponentModel;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 检测工具参数类 - 表达式化版本
    /// 统一使用表达式进行检测条件配置
    /// </summary>
    public class Parameter_Detection
    {
        #region 基本信息

        /// <summary>
        /// 检测项名称
        /// </summary>
        [DisplayName("检测项名称")]
        [Description("用于标识此检测步骤的名称")]
        public string DetectionName { get; set; } = "";

        #endregion

        #region 检测条件

        /// <summary>
        /// 检测条件表达式
        /// 使用{value}代表数据源的值
        /// 例如: {value} >= 440
        ///      {value} >= 100 && {value} <= 200
        ///      Math.Abs({value} - 440) <= 1.0
        /// </summary>
        [DisplayName("检测条件")]
        [Description("布尔表达式，返回true表示检测成功。使用{value}代表数据源的值")]
        public string ConditionExpression { get; set; } = "{value} >= 0";

        #endregion

        #region 超时和重试

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
        [Description("检测失败后的重试次数")]
        public int RetryCount { get; set; } = 0;

        /// <summary>
        /// 重试间隔（毫秒）
        /// </summary>
        [DisplayName("重试间隔")]
        [Description("每次重试之间的等待时间，单位毫秒")]
        public int RetryIntervalMs { get; set; } = 1000;

        #endregion

        #region 结果处理

        /// <summary>
        /// 结果处理配置
        /// </summary>
        [DisplayName("结果处理")]
        [Description("配置检测结果的处理方式")]
        public ResultHandling ResultHandling { get; set; } = new ResultHandling();

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取表达式的简短描述（用于UI显示）
        /// </summary>
        [JsonIgnore]
        public string ExpressionDescription
        {
            get
            {
                if (string.IsNullOrEmpty(ConditionExpression))
                    return "(未设置)";

                var expr = ConditionExpression;
                if (expr.Length > 50)
                    expr = expr.Substring(0, 47) + "...";

                return expr;
            }
        }

        /// <summary>
        /// 获取表达式类型描述
        /// </summary>
        [JsonIgnore]
        public string ExpressionTypeDescription
        {
            get
            {
                if (string.IsNullOrEmpty(ConditionExpression))
                    return "未配置";

                if (ConditionExpression.Contains(">=") && ConditionExpression.Contains("<=") && ConditionExpression.Contains("&&"))
                    return "范围检测";
                if (ConditionExpression.Contains("Math.Abs"))
                    return "容差检测";
                if (ConditionExpression.Contains("&&"))
                    return "多条件AND";
                if (ConditionExpression.Contains("||"))
                    return "多条件OR";
                if (ConditionExpression.Contains(">="))
                    return "大于等于";
                if (ConditionExpression.Contains("<="))
                    return "小于等于";
                if (ConditionExpression.Contains(">"))
                    return "大于";
                if (ConditionExpression.Contains("<"))
                    return "小于";
                if (ConditionExpression.Contains("=="))
                    return "相等";
                if (ConditionExpression.Contains("!="))
                    return "不等";

                return "自定义";
            }
        }

        #endregion
    }

    #region 数据源相关类

    /// <summary>
    /// 数据源类型
    /// </summary>
    public enum DataSourceType
    {
        [Description("系统变量")]
        Variable,

        [Description("PLC地址")]
        PLC
    }

    /// <summary>
    /// PLC地址配置
    /// </summary>
    public class PlcAddressConfig
    {
        /// <summary>
        /// PLC模块名称
        /// </summary>
        public string ModuleName { get; set; } = "";

        /// <summary>
        /// PLC地址
        /// </summary>
        public string Address { get; set; } = "";
    }

    #endregion

    #region 结果处理相关类

    /// <summary>
    /// 失败后的处理行为
    /// </summary>
    public enum FailureAction
    {
        [Description("继续执行")]
        Continue,

        [Description("停止流程")]
        Stop,

        [Description("跳转到指定步骤")]
        JumpToStep,

        [Description("重试")]
        Retry
    }

    /// <summary>
    /// 结果处理配置
    /// </summary>
    public class ResultHandling
    {
        /// <summary>
        /// 失败后的处理行为
        /// </summary>
        public FailureAction OnFailure { get; set; } = FailureAction.Continue;

        /// <summary>
        /// 是否保存结果到变量
        /// </summary>
        public bool SaveToVariable { get; set; } = false;

        /// <summary>
        /// 结果变量名（保存true/false）
        /// </summary>
        public string ResultVariableName { get; set; } = "";

        /// <summary>
        /// 是否保存检测值到变量
        /// </summary>
        public bool SaveValueToVariable { get; set; } = false;

        /// <summary>
        /// 值变量名（保存实际检测值）
        /// </summary>
        public string ValueVariableName { get; set; } = "";

        /// <summary>
        /// 是否显示结果
        /// </summary>
        public bool ShowResult { get; set; } = true;

        /// <summary>
        /// 消息模板
        /// </summary>
        public string MessageTemplate { get; set; } = "检测项 {DetectionName}: {Result}";

        /// <summary>
        /// 失败后跳转的步骤号（-1表示下一步）
        /// </summary>
        public int FailureJumpStep { get; set; } = -1;

        /// <summary>
        /// 成功后跳转的步骤号（-1表示下一步）
        /// </summary>
        public int SuccessJumpStep { get; set; } = -1;
    }

    #endregion
}
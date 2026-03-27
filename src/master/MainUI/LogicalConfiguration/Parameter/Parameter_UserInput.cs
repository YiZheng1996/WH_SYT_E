using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 用户输入步骤参数
    /// 运行时暂停流程，弹窗让操作员填值后继续执行
    /// </summary>
    public class Parameter_UserInput
    {
        /// <summary>
        /// 弹窗标题，如"请输入零件批次号"
        /// </summary>
        public string Title { get; set; } = "请输入";

        /// <summary>
        /// 提示说明文字，显示在输入框上方，指导用户填写什么值
        /// 例如："请输入当前批次的产品序列号，格式：SN-XXXXXXXX"
        /// </summary>
        public string Prompt { get; set; } = "";

        /// <summary>
        /// 输入类型：文本 / 数值 / 下拉选择
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public UserInputType InputType { get; set; } = UserInputType.Text;

        /// <summary>
        /// 存储结果的变量名（不含花括号）
        /// 例如：BatchNumber、Temperature
        /// </summary>
        public string TargetVariableName { get; set; } = "";

        /// <summary>
        /// 输入框默认值，支持变量引用如 {LastBatch}
        /// </summary>
        public string DefaultValue { get; set; } = "";

        // ─── 数值模式专用 ───────────────────────────

        /// <summary>
        /// 数值模式：最小值（null 表示不限制）
        /// </summary>
        public double? MinValue { get; set; }

        /// <summary>
        /// 数值模式：最大值（null 表示不限制）
        /// </summary>
        public double? MaxValue { get; set; }

        /// <summary>
        /// 数值模式：小数位数（0表示整数）
        /// </summary>
        [DefaultValue(2)]
        public int DecimalPlaces { get; set; } = 2;

        // ─── 下拉选择模式专用 ────────────────────────

        /// <summary>
        /// 下拉选择模式的选项列表，用分号分隔
        /// 例如："合格;不合格;待判断"
        /// </summary>
        public string SelectOptions { get; set; } = "";

        // ─── 通用配置 ────────────────────────────────

        /// <summary>
        /// 是否允许空值提交（false 则不填不允许确认）
        /// </summary>
        [DefaultValue(false)]
        public bool AllowEmpty { get; set; } = false;

        /// <summary>
        /// 超时时间（秒），0 = 无限等待
        /// </summary>
        [DefaultValue(0)]
        public int TimeoutSeconds { get; set; } = 0;

        /// <summary>
        /// 超时后的处理方式
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public InputTimeoutAction OnTimeout { get; set; } = InputTimeoutAction.StopProcedure;

        /// <summary>
        /// 超时时使用的默认值（仅 OnTimeout = UseDefaultValue 时有效）
        /// </summary>
        public string TimeoutDefaultValue { get; set; } = "";

        /// <summary>
        /// 步骤描述（在流程列表中显示）
        /// </summary>
        public string Description { get; set; } = "";
    }

    /// <summary>
    /// 用户输入类型
    /// </summary>
    public enum UserInputType
    {
        /// <summary>自由文本输入</summary>
        [Description("文本输入")]
        Text = 0,

        /// <summary>数值输入（带范围校验）</summary>
        [Description("数值输入")]
        Number = 1,

        /// <summary>下拉选择（从预设选项中选一个）</summary>
        [Description("下拉选择")]
        Select = 2
    }

    /// <summary>
    /// 超时处理方式
    /// </summary>
    public enum InputTimeoutAction
    {
        /// <summary>停止整个流程</summary>
        [Description("停止流程")]
        StopProcedure = 0,

        /// <summary>使用默认值继续执行</summary>
        [Description("使用默认值继续")]
        UseDefaultValue = 1,

        /// <summary>跳过此步骤继续执行</summary>
        [Description("跳过此步骤")]
        SkipStep = 2
    }
}

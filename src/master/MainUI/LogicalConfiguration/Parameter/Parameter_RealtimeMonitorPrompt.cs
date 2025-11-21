using MainUI.LogicalConfiguration.Parameter;

namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 实时监控提示参数类
    /// 用于弹出带有实时数值监控的交互提示窗口
    /// </summary>
    public class Parameter_RealtimeMonitorPrompt
    {
        /// <summary>
        /// 窗体标题
        /// </summary>
        public string Title { get; set; } = "实时监控";

        /// <summary>
        /// 提示文字（支持换行符 \n）
        /// </summary>
        public string PromptMessage { get; set; } = "";

        /// <summary>
        /// 监测源类型
        /// </summary>
        public MonitorSourceType MonitorSourceType { get; set; } = MonitorSourceType.Variable;

        /// <summary>
        /// 监测变量名（当MonitorSourceType为Variable时使用）
        /// </summary>
        public string MonitorVariable { get; set; } = "";

        /// <summary>
        /// PLC模块名（当MonitorSourceType为PLC时使用）
        /// </summary>
        public string PlcModuleName { get; set; } = "";

        /// <summary>
        /// PLC地址（当MonitorSourceType为PLC时使用）
        /// </summary>
        public string PlcAddress { get; set; } = "";

        /// <summary>
        /// 数值单位（显示在数值后面，如 "kPa"、"℃"）
        /// </summary>
        public string Unit { get; set; } = "";

        /// <summary>
        /// 数值显示格式（如 "F1" 表示保留1位小数）
        /// </summary>
        public string DisplayFormat { get; set; } = "F1";

        /// <summary>
        /// 刷新间隔（毫秒）
        /// </summary>
        public int RefreshInterval { get; set; } = 500;

        /// <summary>
        /// 按钮文本
        /// </summary>
        public string ButtonText { get; set; } = "确定";

        /// <summary>
        /// 窗体图标类型
        /// </summary>
        public AntdUI.TType IconType { get; set; } = AntdUI.TType.Info;

        /// <summary>
        /// 是否显示数值标签（在数值下方显示单位说明）
        /// </summary>
        public bool ShowValueLabel { get; set; } = true;

        /// <summary>
        /// 数值标签文本（如 "PE05(kPa)"）
        /// </summary>
        public string ValueLabelText { get; set; } = "";

        /// <summary>
        /// 超时时间（秒，0表示不超时）
        /// </summary>
        public int TimeoutSeconds { get; set; } = 0;

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = "实时监控提示";
    }
}
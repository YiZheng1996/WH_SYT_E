namespace MainUI.LogicalConfiguration.Controls
{
    /// <summary>
    /// 输入模块枚举 - 定义可启用的数据源模块
    /// 使用 Flags 特性支持多选组合
    /// </summary>
    [Flags]
    public enum InputModules
    {
        /// <summary>无模块</summary>
        None = 0,

        /// <summary>PLC地址选择</summary>
        PLC = 1 << 0,

        /// <summary>全局变量选择</summary>
        Variable = 1 << 1,

        /// <summary>表达式模板</summary>
        Expression = 1 << 2,

        /// <summary>系统属性选择</summary>
        System = 1 << 3,

        /// <summary>函数库选择</summary>
        Function = 1 << 4,

        /// <summary>常量输入</summary>
        Constant = 1 << 5,

        /// <summary>全部模块</summary>
        All = PLC | Variable | Expression | System | Function | Constant
    }

    /// <summary>
    /// 输入模式枚举 - 定义面板的工作模式
    /// </summary>
    public enum InputMode
    {
        /// <summary>通用表达式模式 - 支持所有输入类型</summary>
        Expression,

        /// <summary>条件表达式模式 - 主要用于条件判断，返回布尔值</summary>
        Condition,

        /// <summary>数值模式 - 只允许数值输入</summary>
        Numeric,

        /// <summary>变量选择模式 - 只选择变量</summary>
        VariableOnly,

        /// <summary>PLC地址模式 - 只选择PLC地址</summary>
        PLCOnly
    }

    /// <summary>
    /// 表达式输入面板配置选项
    /// </summary>
    public class InputPanelOptions
    {
        /// <summary>
        /// 输入模式
        /// </summary>
        public InputMode Mode { get; set; } = InputMode.Expression;

        /// <summary>
        /// 启用的数据源模块
        /// </summary>
        public InputModules EnabledModules { get; set; } = InputModules.Variable | InputModules.Expression;

        /// <summary>
        /// 期望的返回类型（用于验证）
        /// </summary>
        public Type ExpectedReturnType { get; set; } = typeof(object);

        /// <summary>
        /// 初始表达式值
        /// </summary>
        public string InitialExpression { get; set; } = string.Empty;

        /// <summary>
        /// 是否显示验证状态
        /// </summary>
        public bool ShowValidation { get; set; } = true;

        /// <summary>
        /// 是否显示预览结果
        /// </summary>
        public bool ShowPreview { get; set; } = true;

        /// <summary>
        /// 面板位置（相对于目标控件）
        /// </summary>
        public PanelPosition Position { get; set; } = PanelPosition.Below;

        /// <summary>
        /// 提交时是否自动关闭
        /// </summary>
        public bool CloseOnSubmit { get; set; } = true;

        /// <summary>
        /// 点击外部是否关闭
        /// </summary>
        public bool CloseOnClickOutside { get; set; } = true;

        /// <summary>
        /// 面板宽度（0表示自动）
        /// </summary>
        public int PanelWidth { get; set; } = 720;

        /// <summary>
        /// 面板高度（0表示自动）
        /// </summary>
        public int PanelHeight { get; set; } = 280;

        /// <summary>
        /// 自定义标题（空则使用默认）
        /// </summary>
        public string Title { get; set; } = "参数配置";

        /// <summary>
        /// 根据输入模式获取默认启用的模块
        /// </summary>
        public static InputModules GetDefaultModules(InputMode mode)
        {
            return mode switch
            {
                InputMode.Condition => InputModules.Variable | InputModules.PLC | InputModules.Constant,
                InputMode.Numeric => InputModules.Variable | InputModules.Constant,
                InputMode.VariableOnly => InputModules.Variable,
                InputMode.PLCOnly => InputModules.PLC,
                InputMode.Expression => InputModules.All,
                _ => InputModules.Variable | InputModules.Expression
            };
        }

        /// <summary>
        /// 创建条件输入选项
        /// </summary>
        public static InputPanelOptions ForCondition()
        {
            return new InputPanelOptions
            {
                Mode = InputMode.Condition,
                EnabledModules = InputModules.Variable | InputModules.PLC | InputModules.Constant,
                ExpectedReturnType = typeof(bool),
                Title = "条件表达式"
            };
        }

        /// <summary>
        /// 创建表达式输入选项
        /// </summary>
        public static InputPanelOptions ForExpression()
        {
            return new InputPanelOptions
            {
                Mode = InputMode.Expression,
                EnabledModules = InputModules.All,
                Title = "表达式输入"
            };
        }

        /// <summary>
        /// 创建变量选择选项
        /// </summary>
        public static InputPanelOptions ForVariable()
        {
            return new InputPanelOptions
            {
                Mode = InputMode.VariableOnly,
                EnabledModules = InputModules.Variable,
                Title = "选择变量"
            };
        }

        /// <summary>
        /// 创建PLC地址选择选项
        /// </summary>
        public static InputPanelOptions ForPLC()
        {
            return new InputPanelOptions
            {
                Mode = InputMode.PLCOnly,
                EnabledModules = InputModules.PLC,
                Title = "选择PLC地址"
            };
        }
    }

    /// <summary>
    /// 面板位置枚举
    /// </summary>
    public enum PanelPosition
    {
        /// <summary>目标控件下方</summary>
        Below,

        /// <summary>目标控件上方</summary>
        Above,

        /// <summary>自动选择最佳位置</summary>
        Auto,

        /// <summary>屏幕居中</summary>
        Center
    }

    /// <summary>
    /// 表达式提交事件参数
    /// </summary>
    public class ExpressionSubmitEventArgs : EventArgs
    {
        /// <summary>
        /// 提交的表达式
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 表达式是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 验证错误消息（如果有）
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 预览计算结果（如果可用）
        /// </summary>
        public object PreviewResult { get; set; }

        /// <summary>
        /// 是否取消提交
        /// </summary>
        public bool Cancel { get; set; }
    }

    /// <summary>
    /// 数据源选择事件参数
    /// </summary>
    public class SourceSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// 选择的数据源类型
        /// </summary>
        public InputModules SourceType { get; set; }

        /// <summary>
        /// 选择的值
        /// </summary>
        public string SelectedValue { get; set; }

        /// <summary>
        /// 格式化后的表达式片段
        /// </summary>
        public string FormattedExpression { get; set; }

        /// <summary>
        /// 附加数据（如PLC配置信息）
        /// </summary>
        public object AdditionalData { get; set; }
    }
}
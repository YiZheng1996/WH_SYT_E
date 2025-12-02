using System;
using System.Windows.Forms;

namespace MainUI.LogicalConfiguration.Controls
{
    /// <summary>
    /// UITextBox 扩展方法
    /// 提供便捷的表达式输入面板集成
    /// </summary>
    public static class ExpressionInputExtensions
    {
        #region 附加面板

        /// <summary>
        /// 为UITextBox附加表达式输入面板
        /// </summary>
        /// <param name="textBox">目标UITextBox</param>
        /// <param name="modules">启用的模块</param>
        /// <returns>返回UITextBox本身，支持链式调用</returns>
        public static UITextBox WithExpressionInput(this UITextBox textBox,
            InputModules modules = InputModules.Variable | InputModules.Expression)
        {
            ExpressionInputPanel.AttachTo(textBox, new InputPanelOptions
            {
                Mode = InputMode.Expression,
                EnabledModules = modules
            });
            return textBox;
        }

        /// <summary>
        /// 为UITextBox附加条件输入面板
        /// </summary>
        public static UITextBox WithConditionInput(this UITextBox textBox)
        {
            ExpressionInputPanel.AttachTo(textBox, InputPanelOptions.ForCondition());
            return textBox;
        }

        /// <summary>
        /// 为UITextBox附加变量选择面板
        /// </summary>
        public static UITextBox WithVariableInput(this UITextBox textBox)
        {
            ExpressionInputPanel.AttachTo(textBox, InputPanelOptions.ForVariable());
            return textBox;
        }

        /// <summary>
        /// 为UITextBox附加PLC地址选择面板
        /// </summary>
        public static UITextBox WithPLCInput(this UITextBox textBox)
        {
            ExpressionInputPanel.AttachTo(textBox, InputPanelOptions.ForPLC());
            return textBox;
        }

        /// <summary>
        /// 为UITextBox附加自定义配置的面板
        /// </summary>
        public static UITextBox WithExpressionInput(this UITextBox textBox, InputPanelOptions options)
        {
            ExpressionInputPanel.AttachTo(textBox, options);
            return textBox;
        }

        /// <summary>
        /// 为UITextBox附加自定义配置的面板（使用配置委托）
        /// </summary>
        public static UITextBox WithExpressionInput(this UITextBox textBox, Action<InputPanelOptions> configure)
        {
            var options = new InputPanelOptions();
            configure?.Invoke(options);
            ExpressionInputPanel.AttachTo(textBox, options);
            return textBox;
        }

        #endregion

        #region 移除面板

        /// <summary>
        /// 从UITextBox移除表达式输入面板
        /// </summary>
        public static UITextBox RemoveExpressionInput(this UITextBox textBox)
        {
            ExpressionInputPanel.DetachFrom(textBox);
            return textBox;
        }

        #endregion

        #region 手动显示

        /// <summary>
        /// 手动显示表达式输入面板
        /// </summary>
        public static void ShowExpressionPanel(this UITextBox textBox, InputPanelOptions options = null)
        {
            ExpressionInputPanel.Show(textBox, options);
        }

        /// <summary>
        /// 显示条件输入面板
        /// </summary>
        public static void ShowConditionPanel(this UITextBox textBox)
        {
            ExpressionInputPanel.Show(textBox, InputPanelOptions.ForCondition());
        }

        /// <summary>
        /// 显示变量选择面板
        /// </summary>
        public static void ShowVariablePanel(this UITextBox textBox)
        {
            ExpressionInputPanel.Show(textBox, InputPanelOptions.ForVariable());
        }

        /// <summary>
        /// 显示PLC地址选择面板
        /// </summary>
        public static void ShowPLCPanel(this UITextBox textBox)
        {
            ExpressionInputPanel.Show(textBox, InputPanelOptions.ForPLC());
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查UITextBox是否已附加表达式输入面板
        /// </summary>
        public static bool HasExpressionInput(this UITextBox textBox)
        {
            return textBox?.Tag?.ToString() == "ExpressionInput";
        }

        #endregion
    }

    /// <summary>
    /// 表达式输入面板构建器
    /// 提供流畅的配置API
    /// </summary>
    public class ExpressionInputBuilder
    {
        private readonly InputPanelOptions _options = new();
        private UITextBox _targetTextBox;

        /// <summary>
        /// 创建构建器
        /// </summary>
        public static ExpressionInputBuilder Create()
        {
            return new ExpressionInputBuilder();
        }

        /// <summary>
        /// 设置目标UITextBox
        /// </summary>
        public ExpressionInputBuilder For(UITextBox textBox)
        {
            _targetTextBox = textBox;
            return this;
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public ExpressionInputBuilder WithMode(InputMode mode)
        {
            _options.Mode = mode;
            return this;
        }

        /// <summary>
        /// 设置启用的模块
        /// </summary>
        public ExpressionInputBuilder WithModules(InputModules modules)
        {
            _options.EnabledModules = modules;
            return this;
        }

        /// <summary>
        /// 添加模块
        /// </summary>
        public ExpressionInputBuilder AddModule(InputModules module)
        {
            _options.EnabledModules |= module;
            return this;
        }

        /// <summary>
        /// 移除模块
        /// </summary>
        public ExpressionInputBuilder RemoveModule(InputModules module)
        {
            _options.EnabledModules &= ~module;
            return this;
        }

        /// <summary>
        /// 设置期望返回类型
        /// </summary>
        public ExpressionInputBuilder ExpectType<T>()
        {
            _options.ExpectedReturnType = typeof(T);
            return this;
        }

        /// <summary>
        /// 显示验证状态
        /// </summary>
        public ExpressionInputBuilder ShowValidation(bool show = true)
        {
            _options.ShowValidation = show;
            return this;
        }

        /// <summary>
        /// 显示预览
        /// </summary>
        public ExpressionInputBuilder ShowPreview(bool show = true)
        {
            _options.ShowPreview = show;
            return this;
        }

        /// <summary>
        /// 提交时关闭
        /// </summary>
        public ExpressionInputBuilder CloseOnSubmit(bool close = true)
        {
            _options.CloseOnSubmit = close;
            return this;
        }

        /// <summary>
        /// 点击外部时关闭
        /// </summary>
        public ExpressionInputBuilder CloseOnClickOutside(bool close = true)
        {
            _options.CloseOnClickOutside = close;
            return this;
        }

        /// <summary>
        /// 设置面板尺寸
        /// </summary>
        public ExpressionInputBuilder WithSize(int width, int height)
        {
            _options.PanelWidth = width;
            _options.PanelHeight = height;
            return this;
        }

        /// <summary>
        /// 设置面板位置
        /// </summary>
        public ExpressionInputBuilder AtPosition(PanelPosition position)
        {
            _options.Position = position;
            return this;
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        public ExpressionInputBuilder WithTitle(string title)
        {
            _options.Title = title;
            return this;
        }

        /// <summary>
        /// 设置初始表达式
        /// </summary>
        public ExpressionInputBuilder WithInitialExpression(string expression)
        {
            _options.InitialExpression = expression;
            return this;
        }

        /// <summary>
        /// 附加到目标UITextBox
        /// </summary>
        public UITextBox Attach()
        {
            if (_targetTextBox == null)
                throw new InvalidOperationException("必须先调用 For() 方法指定目标UITextBox");

            ExpressionInputPanel.AttachTo(_targetTextBox, _options);
            return _targetTextBox;
        }

        /// <summary>
        /// 立即显示面板
        /// </summary>
        public void Show()
        {
            if (_targetTextBox == null)
                throw new InvalidOperationException("必须先调用 For() 方法指定目标UITextBox");

            ExpressionInputPanel.Show(_targetTextBox, _options);
        }

        /// <summary>
        /// 获取配置选项
        /// </summary>
        public InputPanelOptions Build()
        {
            return _options;
        }
    }
}
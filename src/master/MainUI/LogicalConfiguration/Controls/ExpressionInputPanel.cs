using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.LogicalConfiguration.Controls
{
    /// <summary>
    /// 通用表达式输入面板
    /// 弹出式设计,可附加到任何UITextBox控件
    /// 支持PLC地址、变量、表达式、系统属性、函数等多种数据源
    /// </summary>
    public partial class ExpressionInputPanel : Form
    {
        #region 静态成员

        /// <summary>
        /// 当前活动的面板实例(确保只有一个面板显示)
        /// </summary>
        private static ExpressionInputPanel _activeInstance;

        /// <summary>
        /// 已附加面板的UITextBox集合
        /// </summary>
        private static readonly Dictionary<UITextBox, InputPanelOptions> _attachedTextBoxes = new();

        #endregion

        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private readonly ExpressionEngine _expressionEngine;
        private readonly IPLCManager _plcManager;

        private UITextBox _targetTextBox;
        private InputPanelOptions _options;
        private bool _isSubmitting;

        // 颜色定义
        private static readonly Color PrimaryColor = Color.FromArgb(65, 100, 204);
        private static readonly Color SuccessColor = Color.FromArgb(40, 167, 69);
        private static readonly Color ErrorColor = Color.FromArgb(220, 53, 69);
        private static readonly Color BackgroundColor = Color.FromArgb(248, 249, 250);
        private static readonly Color ButtonColor = Color.FromArgb(240, 240, 240);
        private static readonly Color ButtonHoverColor = Color.FromArgb(220, 220, 220);

        #endregion

        #region 事件

        /// <summary>
        /// 表达式提交事件
        /// </summary>
        public event EventHandler<ExpressionSubmitEventArgs> ExpressionSubmit;

        /// <summary>
        /// 面板关闭事件
        /// </summary>
        public event EventHandler PanelClosed;

        /// <summary>
        /// 数据源选择事件
        /// </summary>
        public event EventHandler<SourceSelectedEventArgs> SourceSelected;

        #endregion

        #region 属性

        /// <summary>
        /// 当前表达式
        /// </summary>
        public string Expression
        {
            get => _expressionTextBox?.Text ?? string.Empty;
            set
            {
                if (_expressionTextBox != null)
                    _expressionTextBox.Text = value;
            }
        }

        /// <summary>
        /// 表达式是否有效
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        /// 目标输入框
        /// </summary>
        public UITextBox TargetTextBox => _targetTextBox;

        #endregion

        #region 构造函数

        /// <summary>
        /// 私有构造函数 - 通过静态方法创建实例
        /// </summary>
        private ExpressionInputPanel()
        {
            // 尝试从服务容器获取依赖
            _variableManager = Program.ServiceProvider?.GetService<GlobalVariableManager>();
            _expressionEngine = Program.ServiceProvider?.GetService<ExpressionEngine>();
            _plcManager = Program.ServiceProvider?.GetService<IPLCManager>();

            InitializeComponent();
            InitializeAdditionalComponents();
        }

        /// <summary>
        /// 带依赖注入的构造函数
        /// </summary>
        public ExpressionInputPanel(
            GlobalVariableManager variableManager,
            ExpressionEngine expressionEngine,
            IPLCManager plcManager)
        {
            _variableManager = variableManager;
            _expressionEngine = expressionEngine;
            _plcManager = plcManager;

            InitializeComponent();
            InitializeAdditionalComponents();
        }

        #endregion

        #region 静态方法 - 主要入口

        /// <summary>
        /// 附加到UITextBox - 点击时自动弹出面板
        /// </summary>
        /// <param name="textBox">目标UITextBox</param>
        /// <param name="options">配置选项</param>
        public static void AttachTo(UITextBox textBox, InputPanelOptions options = null)
        {
            if (textBox == null) return;

            options ??= new InputPanelOptions();

            // 移除已有的事件处理
            DetachFrom(textBox);

            // 保存选项
            _attachedTextBoxes[textBox] = options;

            // 添加事件处理
            textBox.Click += AttachedTextBox_Click;
            textBox.KeyDown += AttachedTextBox_KeyDown;

            // 设置标记
            textBox.Tag = "ExpressionInput";
        }

        /// <summary>
        /// 从UITextBox分离面板
        /// </summary>
        public static void DetachFrom(UITextBox textBox)
        {
            if (textBox == null) return;

            // 移除事件处理
            textBox.Click -= AttachedTextBox_Click;
            textBox.KeyDown -= AttachedTextBox_KeyDown;

            // 移除选项
            _attachedTextBoxes.Remove(textBox);

            // 清除标记
            if (textBox.Tag?.ToString() == "ExpressionInput")
                textBox.Tag = null;
        }

        /// <summary>
        /// 显示面板(静态方法)
        /// </summary>
        public static void Show(UITextBox textBox, InputPanelOptions options = null)
        {
            if (textBox == null) return;

            options ??= new InputPanelOptions();
            options.InitialExpression = textBox.Text;

            ShowPanel(textBox, options);
        }

        /// <summary>
        /// 关闭当前活动面板
        /// </summary>
        public static void CloseActivePanel()
        {
            _activeInstance?.ClosePanel();
        }

        #endregion

        #region 私有静态方法

        private static void AttachedTextBox_Click(object sender, EventArgs e)
        {
            if (sender is UITextBox textBox && _attachedTextBoxes.TryGetValue(textBox, out var options))
            {
                options.InitialExpression = textBox.Text;
                ShowPanel(textBox, options);
            }
        }

        private static void AttachedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // F2 或 Ctrl+Space 打开面板
            if (e.KeyCode == Keys.F2 || (e.Control && e.KeyCode == Keys.Space))
            {
                if (sender is UITextBox textBox && _attachedTextBoxes.TryGetValue(textBox, out var options))
                {
                    options.InitialExpression = textBox.Text;
                    ShowPanel(textBox, options);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        private static void ShowPanel(UITextBox textBox, InputPanelOptions options)
        {
            // 关闭已有面板
            _activeInstance?.ClosePanel();

            // 创建新面板
            var panel = new ExpressionInputPanel();
            panel.SetTarget(textBox, options);
            panel.ShowPanel();

            _activeInstance = panel;
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化附加组件(Designer.cs 不包含的部分)
        /// </summary>
        private void InitializeAdditionalComponents()
        {
            this.DoubleBuffered = true;
            CreateKeyboardButtons();
        }


        /// <summary>
        /// 创建键盘按钮(动态添加到Designer中的_keyboardGrid)
        /// </summary>
        private void CreateKeyboardButtons()
        {
            // 使用Designer中已定义的 _keyboardGrid

            // 第一行:数字键 7-9, 运算符
            AddKeyButton(_keyboardGrid, "7", 0, 0, () => InsertText("7"));
            AddKeyButton(_keyboardGrid, "8", 0, 1, () => InsertText("8"));
            AddKeyButton(_keyboardGrid, "9", 0, 2, () => InsertText("9"));
            AddKeyButton(_keyboardGrid, "/", 0, 3, () => InsertText(" / "));
            AddKeyButton(_keyboardGrid, "(", 0, 4, () => InsertText("("));
            AddKeyButton(_keyboardGrid, ")", 0, 5, () => InsertText(")"));
            var btnBackspace = AddKeyButton(_keyboardGrid, "←", 0, 6, Backspace);
            var btnClear = AddKeyButton(_keyboardGrid, "清空", 0, 7, Clear);
            var btnSubmit = AddKeyButton(_keyboardGrid, "确定", 0, 8, Submit);
            btnSubmit.BackColor = PrimaryColor;
            btnSubmit.ForeColor = Color.White;
            _keyboardGrid.SetColumnSpan(btnSubmit, 2);

            // 第二行:数字键 4-6, 运算符
            AddKeyButton(_keyboardGrid, "4", 1, 0, () => InsertText("4"));
            AddKeyButton(_keyboardGrid, "5", 1, 1, () => InsertText("5"));
            AddKeyButton(_keyboardGrid, "6", 1, 2, () => InsertText("6"));
            AddKeyButton(_keyboardGrid, "*", 1, 3, () => InsertText(" * "));
            AddKeyButton(_keyboardGrid, "[", 1, 4, () => InsertText("["));
            AddKeyButton(_keyboardGrid, "]", 1, 5, () => InsertText("]"));
            AddKeyButton(_keyboardGrid, "<", 1, 6, () => InsertText(" < "));
            AddKeyButton(_keyboardGrid, ">", 1, 7, () => InsertText(" > "));
            AddKeyButton(_keyboardGrid, "<=", 1, 8, () => InsertText(" <= "));
            AddKeyButton(_keyboardGrid, ">=", 1, 9, () => InsertText(" >= "));

            // 第三行:数字键 1-3, 运算符
            AddKeyButton(_keyboardGrid, "1", 2, 0, () => InsertText("1"));
            AddKeyButton(_keyboardGrid, "2", 2, 1, () => InsertText("2"));
            AddKeyButton(_keyboardGrid, "3", 2, 2, () => InsertText("3"));
            AddKeyButton(_keyboardGrid, "-", 2, 3, () => InsertText(" - "));
            AddKeyButton(_keyboardGrid, "{", 2, 4, () => InsertText("{"));
            AddKeyButton(_keyboardGrid, "}", 2, 5, () => InsertText("}"));
            AddKeyButton(_keyboardGrid, "==", 2, 6, () => InsertText(" == "));
            AddKeyButton(_keyboardGrid, "!=", 2, 7, () => InsertText(" != "));
            AddKeyButton(_keyboardGrid, "&&", 2, 8, () => InsertText(" && "));
            AddKeyButton(_keyboardGrid, "||", 2, 9, () => InsertText(" || "));

            // 第四行:数字键 0, 运算符
            AddKeyButton(_keyboardGrid, "0", 3, 0, () => InsertText("0"));
            _keyboardGrid.SetColumnSpan(_keyboardGrid.GetControlFromPosition(0, 3), 2);
            AddKeyButton(_keyboardGrid, "", 3, 1, null); // 占位
            AddKeyButton(_keyboardGrid, ".", 3, 2, () => InsertText("."));
            AddKeyButton(_keyboardGrid, "+", 3, 3, () => InsertText(" + "));
            AddKeyButton(_keyboardGrid, "%", 3, 4, () => InsertText(" % "));
            AddKeyButton(_keyboardGrid, "!", 3, 5, () => InsertText("!"));
            AddKeyButton(_keyboardGrid, "&", 3, 6, () => InsertText(" & "));
            AddKeyButton(_keyboardGrid, "|", 3, 7, () => InsertText(" | "));
            AddKeyButton(_keyboardGrid, "^", 3, 8, () => InsertText(" ^ "));
            AddKeyButton(_keyboardGrid, "~", 3, 9, () => InsertText("~"));

            // 第五行:特殊功能键
            AddKeyButton(_keyboardGrid, "空格", 4, 0, () => InsertText(" "));
            _keyboardGrid.SetColumnSpan(_keyboardGrid.GetControlFromPosition(0, 4), 3);
            AddKeyButton(_keyboardGrid, "", 4, 1, null); // 占位
            AddKeyButton(_keyboardGrid, "", 4, 2, null); // 占位
            AddKeyButton(_keyboardGrid, "_", 4, 3, () => InsertText("_"));
            AddKeyButton(_keyboardGrid, "\"", 4, 4, () => InsertText("\""));
            AddKeyButton(_keyboardGrid, "'", 4, 5, () => InsertText("'"));
            AddKeyButton(_keyboardGrid, ",", 4, 6, () => InsertText(", "));
            AddKeyButton(_keyboardGrid, ".", 4, 7, () => InsertText("."));
            AddKeyButton(_keyboardGrid, "+/-", 4, 8, ToggleSign);
            var btnClose = AddKeyButton(_keyboardGrid, "关闭", 4, 9, ClosePanel);
            btnClose.BackColor = Color.RebeccaPurple;
            btnClose.ForeColor = Color.White;
        }

        /// <summary>
        /// 添加键盘按钮
        /// </summary>
        private Button AddKeyButton(TableLayoutPanel grid, string text, int row, int col,
            Action onClick = null, Color? backColor = null)
        {
            var btn = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(2),
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor ?? ButtonColor,
                Font = new Font("微软雅黑", 10f),
                Cursor = string.IsNullOrEmpty(text) ? Cursors.Default : Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);

            if (!string.IsNullOrEmpty(text))
            {
                btn.FlatAppearance.MouseOverBackColor = ButtonHoverColor;
                if (onClick != null)
                {
                    btn.Click += (s, e) => onClick();
                }
            }
            else
            {
                btn.Enabled = false;
                btn.BackColor = BackgroundColor;
                btn.FlatAppearance.BorderSize = 0;
            }

            grid.Controls.Add(btn, col, row);
            return btn;
        }

        #endregion

        #region 设置和显示

        /// <summary>
        /// 设置目标UITextBox和配置
        /// </summary>
        private void SetTarget(UITextBox textBox, InputPanelOptions options)
        {
            _targetTextBox = textBox;
            _options = options;

            // 设置初始值
            _expressionTextBox.Text = options.InitialExpression;

            // 根据配置显示/隐藏模块
            UpdateModuleVisibility();

            // 设置标题(如果窗体有标题栏的话)
            this.Text = options.Title;
        }

        /// <summary>
        /// 更新模块可见性
        /// </summary>
        private void UpdateModuleVisibility()
        {
            if (_options == null) return;

            _btnPLC.Visible = _options.EnabledModules.HasFlag(InputModules.PLC);
            _btnVariable.Visible = _options.EnabledModules.HasFlag(InputModules.Variable);
            _btnExpression.Visible = _options.EnabledModules.HasFlag(InputModules.Expression);
            _btnSystem.Visible = _options.EnabledModules.HasFlag(InputModules.System);
            _btnFunction.Visible = _options.EnabledModules.HasFlag(InputModules.Function);
            _btnConstant.Visible = _options.EnabledModules.HasFlag(InputModules.Constant);

            // 重新排列可见按钮
            RearrangeSourceButtons();
        }

        /// <summary>
        /// 重新排列数据源按钮
        /// </summary>
        private void RearrangeSourceButtons()
        {
            var buttons = new[] { _btnPLC, _btnVariable, _btnExpression, _btnSystem, _btnFunction, _btnConstant };
            var visibleButtons = buttons.Where(b => b.Visible).ToList();

            int currentY = 0;
            int buttonSpacing = 4;

            foreach (var btn in visibleButtons)
            {
                btn.Location = new Point(0, currentY);
                currentY += btn.Height + buttonSpacing;
            }
        }

        /// <summary>
        /// 显示面板
        /// </summary>
        private void ShowPanel()
        {
            // 计算位置
            CalculatePosition();

            // 显示
            this.Show();

            // 聚焦到表达式输入框
            _expressionTextBox.Focus();
            _expressionTextBox.SelectionStart = _expressionTextBox.Text.Length;
        }

        /// <summary>
        /// 计算面板位置
        /// </summary>
        private void CalculatePosition()
        {
            if (_targetTextBox == null) return;

            var screenPoint = _targetTextBox.PointToScreen(new Point(0, _targetTextBox.Height));
            var screen = Screen.FromControl(_targetTextBox);

            int x = screenPoint.X;
            int y = screenPoint.Y + 5;

            // 检查是否超出屏幕底部
            if (y + this.Height > screen.WorkingArea.Bottom)
            {
                // 显示在上方
                y = _targetTextBox.PointToScreen(Point.Empty).Y - this.Height - 5;
            }

            // 检查是否超出屏幕右侧
            if (x + this.Width > screen.WorkingArea.Right)
            {
                x = screen.WorkingArea.Right - this.Width - 10;
            }

            // 检查是否超出屏幕左侧
            if (x < screen.WorkingArea.Left)
            {
                x = screen.WorkingArea.Left + 10;
            }

            this.Location = new Point(x, y);
        }

        /// <summary>
        /// 关闭面板
        /// </summary>
        private void ClosePanel()
        {
            try
            {
                PanelClosed?.Invoke(this, EventArgs.Empty);
                this.Close();

                if (_activeInstance == this)
                    _activeInstance = null;
            }
            catch (Exception ex)
            {
                // 静默处理关闭错误
                System.Diagnostics.Debug.WriteLine($"关闭面板错误: {ex.Message}");
            }
        }

        #endregion

        #region 数据源按钮事件 - Designer.cs 定义的事件处理器

        private void BtnPLC_Click(object sender, EventArgs e)
        {
            // TODO: 显示PLC地址选择器
            ShowPLCSelector();
        }

        private void BtnVariable_Click(object sender, EventArgs e)
        {
            // TODO: 显示变量选择器
            ShowVariableSelector();
        }

        private void BtnExpression_Click(object sender, EventArgs e)
        {
            // TODO: 显示表达式模板
            ShowExpressionTemplates();
        }

        private void BtnSystem_Click(object sender, EventArgs e)
        {
            // TODO: 显示系统属性
            ShowSystemProperties();
        }

        private void BtnFunction_Click(object sender, EventArgs e)
        {
            // TODO: 显示函数选择器
            ShowFunctions();
        }

        private void BtnConstant_Click(object sender, EventArgs e)
        {
            // TODO: 显示常量输入
            ShowConstants();
        }

        private void SourceButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = ButtonHoverColor;
            }
        }

        private void SourceButton_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = ButtonColor;
            }
        }

        #endregion

        #region 操作按钮事件 - Designer.cs 定义的事件处理器

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            Submit();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            ClosePanel();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnBackspace_Click(object sender, EventArgs e)
        {
            Backspace();
        }

        #endregion

        #region Designer 事件处理器

        private void ExpressionTextBox_TextChanged(object sender, EventArgs e)
        {
            _validationTimer.Stop();
            _validationTimer.Start();
        }

        private void ValidationTimer_Tick(object sender, EventArgs e)
        {
            _validationTimer.Stop();
            ValidateExpression();
        }

        private void ExpressionInputPanel_Deactivate(object sender, EventArgs e)
        {
            if (_options?.CloseOnClickOutside == true && !_isSubmitting)
            {
                ClosePanel();
            }
        }

        private void ExpressionInputPanel_Paint(object sender, PaintEventArgs e)
        {
            using var pen = new Pen(Color.FromArgb(200, 200, 200), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }

        #endregion

        #region 表达式操作

        /// <summary>
        /// 插入文本到光标位置
        /// </summary>
        private void InsertText(string text)
        {
            if (_expressionTextBox == null) return;

            int selStart = _expressionTextBox.SelectionStart;
            string currentText = _expressionTextBox.Text;

            // 插入文本
            _expressionTextBox.Text = currentText.Insert(selStart, text);

            // 设置光标位置
            _expressionTextBox.SelectionStart = selStart + text.Length;
            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 退格
        /// </summary>
        private void Backspace()
        {
            if (_expressionTextBox == null || _expressionTextBox.Text.Length == 0) return;

            int selStart = _expressionTextBox.SelectionStart;
            if (selStart > 0)
            {
                _expressionTextBox.Text = _expressionTextBox.Text.Remove(selStart - 1, 1);
                _expressionTextBox.SelectionStart = selStart - 1;
            }
            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            if (_expressionTextBox == null) return;

            _expressionTextBox.Text = string.Empty;
            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 切换正负号
        /// </summary>
        private void ToggleSign()
        {
            if (_expressionTextBox == null) return;

            string text = _expressionTextBox.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            if (text.StartsWith("-"))
            {
                _expressionTextBox.Text = text.Substring(1);
            }
            else if (double.TryParse(text, out _))
            {
                _expressionTextBox.Text = "-" + text;
            }

            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 提交表达式
        /// </summary>
        private void Submit()
        {
            try
            {
                _isSubmitting = true;

                var args = new ExpressionSubmitEventArgs
                {
                    Expression = _expressionTextBox.Text,
                    IsValid = IsValid
                };

                // 触发提交事件
                ExpressionSubmit?.Invoke(this, args);

                // 如果没有取消,将值写回目标文本框
                if (!args.Cancel && _targetTextBox != null)
                {
                    _targetTextBox.Text = _expressionTextBox.Text;
                }

                // 根据配置决定是否关闭
                if (_options?.CloseOnSubmit == true && !args.Cancel)
                {
                    ClosePanel();
                }
            }
            finally
            {
                _isSubmitting = false;
            }
        }

        #endregion

        #region 验证

        /// <summary>
        /// 验证表达式
        /// </summary>
        private void ValidateExpression()
        {
            try
            {
                string expression = _expressionTextBox.Text;

                if (string.IsNullOrWhiteSpace(expression))
                {
                    UpdateValidationUI(true, "准备就绪", null);
                    return;
                }

                // 使用表达式引擎验证
                if (_expressionEngine != null)
                {
                    // 预处理：将PLC占位符和变量占位符替换为测试值
                    string testExpression = PreprocessExpressionForValidation(expression);

                    var result = _expressionEngine.EvaluateExpression(testExpression);
                    if (result.Success)
                    {
                        IsValid = true;
                        UpdateValidationUI(true, "表达式语法有效", "（使用测试值验证）");
                    }
                    else
                    {
                        IsValid = false;
                        UpdateValidationUI(false, "表达式无效", result.ErrorMessage);
                    }
                }
                else
                {
                    // 简单验证:检查括号匹配
                    bool bracketsValid = ValidateBrackets(expression);
                    IsValid = bracketsValid;
                    UpdateValidationUI(bracketsValid,
                        bracketsValid ? "语法检查通过" : "括号不匹配",
                        null);
                }
            }
            catch (Exception ex)
            {
                IsValid = false;
                UpdateValidationUI(false, "验证失败", ex.Message);
            }
        }

        /// <summary>
        /// 预处理表达式用于验证（将占位符替换为测试值）
        /// </summary>
        private string PreprocessExpressionForValidation(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return expression;

            // 替换PLC地址占位符：{PLC.xxx} -> 100
            expression = System.Text.RegularExpressions.Regex.Replace(
                expression,
                @"\{PLC\.[^}]+\}",
                "100");

            // 替换变量占位符：{变量名} -> 100
            expression = System.Text.RegularExpressions.Regex.Replace(
                expression,
                @"\{[^}]+\}",
                "100");

            return expression;
        }

        /// <summary>
        /// 更新验证UI
        /// </summary>
        private void UpdateValidationUI(bool isValid, string message, string preview)
        {
            if (_validationLabel != null)
            {
                _validationLabel.Text = $"{(isValid ? "✓" : "✗")} {message}";
                _validationLabel.ForeColor = isValid ? SuccessColor : ErrorColor;
            }

            if (_previewLabel != null)
            {
                _previewLabel.Text = preview ?? string.Empty;
            }
        }

        /// <summary>
        /// 验证括号匹配
        /// </summary>
        private bool ValidateBrackets(string expression)
        {
            var stack = new Stack<char>();
            var pairs = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' } };

            foreach (var c in expression)
            {
                if ("([{".Contains(c))
                {
                    stack.Push(c);
                }
                else if (pairs.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Pop() != pairs[c])
                        return false;
                }
            }

            return stack.Count == 0;
        }

        #endregion

        #region 数据源选择器实现

        /// <summary>
        /// 显示PLC地址选择器
        /// </summary>
        private async void ShowPLCSelector()
        {
            try
            {
                var menu = new ContextMenuStrip
                {
                    Font = new Font("微软雅黑", 9f)
                };

                if (_plcManager != null)
                {
                    var modules = await _plcManager.GetModuleTagsAsync();
                    if (modules != null && modules.Count > 0)
                    {
                        foreach (var module in modules)
                        {
                            var moduleItem = new ToolStripMenuItem(module.Key);

                            // 添加地址子菜单
                            foreach (var address in module.Value)
                            {
                                var addressItem = new ToolStripMenuItem(address);
                                addressItem.Click += (s, e) =>
                                {
                                    var plcExpression = $"PLC.{module.Key}.{address}";
                                    InsertText($"{{{plcExpression}}}");

                                    SourceSelected?.Invoke(this, new SourceSelectedEventArgs
                                    {
                                        SourceType = InputModules.PLC,
                                        SelectedValue = address,
                                        FormattedExpression = $"{{{plcExpression}}}"
                                    });
                                };
                                moduleItem.DropDownItems.Add(addressItem);
                            }

                            menu.Items.Add(moduleItem);
                        }
                    }
                    else
                    {
                        menu.Items.Add(new ToolStripMenuItem("(无可用PLC模块)") { Enabled = false });
                    }
                }
                else
                {
                    menu.Items.Add(new ToolStripMenuItem("(PLC管理器不可用)") { Enabled = false });
                }

                menu.Show(_btnPLC, new Point(0, _btnPLC.Height));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载PLC模块失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 显示变量选择器
        /// </summary>
        private void ShowVariableSelector()
        {
            try
            {
                var menu = new ContextMenuStrip();
                menu.Font = new Font("微软雅黑", 9f);
                menu.MaximumSize = new Size(300, 400);

                if (_variableManager != null)
                {
                    var variables = _variableManager.GetAllVariables()
                        .Where(v => !v.IsSystemVariable)
                        .OrderBy(v => v.VarName)
                        .ToList();

                    if (variables.Count != 0)
                    {
                        // 按类型分组
                        var groups = variables.GroupBy(v => v.VarType);

                        foreach (var group in groups)
                        {
                            var typeItem = new ToolStripMenuItem($"📁 {group.Key}");
                            typeItem.Font = new Font("微软雅黑", 9f, FontStyle.Bold);

                            foreach (var variable in group)
                            {
                                var varItem = new ToolStripMenuItem($"{variable.VarName} = {variable.VarValue}");
                                varItem.Click += (s, e) =>
                                {
                                    InsertText($"{{{variable.VarName}}}");

                                    SourceSelected?.Invoke(this, new SourceSelectedEventArgs
                                    {
                                        SourceType = InputModules.Variable,
                                        SelectedValue = variable.VarName,
                                        FormattedExpression = $"{{{variable.VarName}}}"
                                    });
                                };
                                typeItem.DropDownItems.Add(varItem);
                            }

                            menu.Items.Add(typeItem);
                        }
                    }
                    else
                    {
                        menu.Items.Add(new ToolStripMenuItem("(无可用变量)") { Enabled = false });
                    }
                }
                else
                {
                    menu.Items.Add(new ToolStripMenuItem("(变量管理器不可用)") { Enabled = false });
                }

                menu.Show(_btnVariable, new Point(0, _btnVariable.Height));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载变量列表失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 显示表达式模板
        /// </summary>
        private void ShowExpressionTemplates()
        {
            var menu = new ContextMenuStrip();
            menu.Font = new Font("微软雅黑", 9f);

            var templates = new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "📊 比较运算", new Dictionary<string, string>
                    {
                        { "等于", "{变量} == 值" },
                        { "不等于", "{变量} != 值" },
                        { "大于", "{变量} > 值" },
                        { "小于", "{变量} < 值" },
                        { "大于等于", "{变量} >= 值" },
                        { "小于等于", "{变量} <= 值" },
                        { "范围判断", "{变量} >= 最小 && {变量} <= 最大" }
                    }
                },
                {
                    "🧮 算术运算", new Dictionary<string, string>
                    {
                        { "加法", "{变量1} + {变量2}" },
                        { "减法", "{变量1} - {变量2}" },
                        { "乘法", "{变量1} * {变量2}" },
                        { "除法", "{变量1} / {变量2}" },
                        { "取余", "{变量1} % {变量2}" },
                        { "求平均", "({变量1} + {变量2}) / 2" }
                    }
                },
                {
                    "🔀 逻辑运算", new Dictionary<string, string>
                    {
                        { "与运算", "{条件1} && {条件2}" },
                        { "或运算", "{条件1} || {条件2}" },
                        { "非运算", "!{条件}" },
                        { "异或运算", "{条件1} ^ {条件2}" }
                    }
                },
                {
                    "📝 字符串操作", new Dictionary<string, string>
                    {
                        { "连接字符串", "{字符串1} + {字符串2}" },
                        { "长度 Length", "str.Length" },
                        { "大写 ToUpper", "str.ToUpper()" },
                        { "小写 ToLower", "str.ToLower()" },
                        { "包含 Contains", "str.Contains(value)" }
                    }
                }
            };

            foreach (var category in templates)
            {
                var categoryItem = new ToolStripMenuItem(category.Key);
                categoryItem.Font = new Font("微软雅黑", 9f, FontStyle.Bold);

                foreach (var template in category.Value)
                {
                    var templateItem = new ToolStripMenuItem(template.Key);
                    templateItem.ToolTipText = template.Value;
                    templateItem.Click += (s, e) =>
                    {
                        InsertText(template.Value);

                        SourceSelected?.Invoke(this, new SourceSelectedEventArgs
                        {
                            SourceType = InputModules.Expression,
                            SelectedValue = template.Key,
                            FormattedExpression = template.Value
                        });
                    };
                    categoryItem.DropDownItems.Add(templateItem);
                }

                menu.Items.Add(categoryItem);
            }

            menu.Show(_btnExpression, new Point(0, _btnExpression.Height));
        }

        /// <summary>
        /// 显示系统属性选择器
        /// </summary>
        private void ShowSystemProperties()
        {
            var menu = new ContextMenuStrip();
            menu.Font = new Font("微软雅黑", 9f);

            var properties = new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "👤 用户信息", new Dictionary<string, string>
                    {
                        { "用户名", "NewUsers.NewUserInfo.Username" },
                        { "用户级别", "NewUsers.NewUserInfo.UserLevel" }
                    }
                },
                {
                    "📅 日期时间", new Dictionary<string, string>
                    {
                        { "当前日期", "DateTime.Now.ToString(\"yyyy-MM-dd\")" },
                        { "当前时间", "DateTime.Now.ToString(\"HH:mm:ss\")" },
                        { "当前日期时间", "DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss\")" },
                        { "年份", "DateTime.Now.Year" },
                        { "月份", "DateTime.Now.Month" },
                        { "日期", "DateTime.Now.Day" }
                    }
                },
                {
                    "🔧 系统信息", new Dictionary<string, string>
                    {
                        { "产品类型", "{产品类型}" },
                        { "产品型号", "{产品型号}" },
                        { "试验项点", "{试验项点}" }
                    }
                }
            };

            foreach (var category in properties)
            {
                var categoryItem = new ToolStripMenuItem(category.Key)
                {
                    Font = new Font("微软雅黑", 9f, FontStyle.Bold)
                };

                foreach (var property in category.Value)
                {
                    var propertyItem = new ToolStripMenuItem(property.Key);
                    propertyItem.ToolTipText = property.Value;
                    propertyItem.Click += (s, e) =>
                    {
                        InsertText(property.Value);

                        SourceSelected?.Invoke(this, new SourceSelectedEventArgs
                        {
                            SourceType = InputModules.System,
                            SelectedValue = property.Key,
                            FormattedExpression = property.Value
                        });
                    };
                    categoryItem.DropDownItems.Add(propertyItem);
                }

                menu.Items.Add(categoryItem);
            }

            menu.Show(_btnSystem, new Point(0, _btnSystem.Height));
        }

        /// <summary>
        /// 显示函数选择器
        /// </summary>
        private void ShowFunctions()
        {
            var menu = new ContextMenuStrip();
            menu.Font = new Font("微软雅黑", 9f);

            var functions = new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "📐 数学函数", new Dictionary<string, string>
                    {
                        { "绝对值 Abs", "Math.Abs(x)" },
                        { "四舍五入 Round", "Math.Round(x, 2)" },
                        { "向上取整 Ceiling", "Math.Ceiling(x)" },
                        { "向下取整 Floor", "Math.Floor(x)" },
                        { "最大值 Max", "Math.Max(a, b)" },
                        { "最小值 Min", "Math.Min(a, b)" },
                        { "幂运算 Pow", "Math.Pow(x, y)" },
                        { "平方根 Sqrt", "Math.Sqrt(x)" }
                    }
                },
                {
                    "🔄 转换函数", new Dictionary<string, string>
                    {
                        { "转整数", "Convert.ToInt32(x)" },
                        { "转浮点数", "Convert.ToDouble(x)" },
                        { "转字符串", "Convert.ToString(x)" },
                        { "转布尔值", "Convert.ToBoolean(x)" }
                    }
                },
                {
                    "📊 格式化函数", new Dictionary<string, string>
                    {
                        { "数值格式化", "FORMAT(x, \"0.00\")" },
                        { "日期格式化", "FORMAT(date, \"yyyy-MM-dd\")" },
                        { "时间格式化", "FORMAT(time, \"HH:mm:ss\")" },
                        { "百分比格式化", "FORMAT(x, \"P2\")" }
                    }
                }
            };

            foreach (var category in functions)
            {
                var categoryItem = new ToolStripMenuItem(category.Key);
                categoryItem.Font = new Font("微软雅黑", 9f, FontStyle.Bold);

                foreach (var func in category.Value)
                {
                    var funcItem = new ToolStripMenuItem(func.Key)
                    {
                        ToolTipText = func.Value
                    };
                    funcItem.Click += (s, e) =>
                    {
                        InsertText(func.Value);

                        SourceSelected?.Invoke(this, new SourceSelectedEventArgs
                        {
                            SourceType = InputModules.Function,
                            SelectedValue = func.Key,
                            FormattedExpression = func.Value
                        });
                    };
                    categoryItem.DropDownItems.Add(funcItem);
                }

                menu.Items.Add(categoryItem);
            }

            menu.Show(_btnFunction, new Point(0, _btnFunction.Height));
        }

        /// <summary>
        /// 显示常量输入
        /// </summary>
        private void ShowConstants()
        {
            var menu = new ContextMenuStrip
            {
                Font = new Font("微软雅黑", 9f)
            };

            var constants = new Dictionary<string, string>
            {
                { "true (真)", "true" },
                { "false (假)", "false" },
                { "null (空)", "null" },
                { "π (圆周率)", "Math.PI" },
                { "e (自然常数)", "Math.E" },
                { "空字符串", "\"\"" },
                { "0 (零)", "0" },
                { "1 (一)", "1" },
                { "-1 (负一)", "-1" }
            };

            foreach (var constant in constants)
            {
                var constantItem = new ToolStripMenuItem(constant.Key);
                constantItem.Click += (s, e) =>
                {
                    InsertText(constant.Value);

                    SourceSelected?.Invoke(this, new SourceSelectedEventArgs
                    {
                        SourceType = InputModules.Constant,
                        SelectedValue = constant.Key,
                        FormattedExpression = constant.Value
                    });
                };
                menu.Items.Add(constantItem);
            }

            menu.Show(_btnConstant, new Point(0, _btnConstant.Height));
        }

        #endregion
    }
}
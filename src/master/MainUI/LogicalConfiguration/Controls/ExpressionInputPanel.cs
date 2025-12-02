using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.LogicalConfiguration.Controls
{
    /// <summary>
    /// 通用表达式输入面板
    /// 弹出式设计，可附加到任何UITextBox控件
    /// 支持PLC地址、变量、表达式、系统属性、函数等多种数据源
    /// </summary>
    public partial class ExpressionInputPanel : Form
    {
        #region 静态成员

        /// <summary>
        /// 当前活动的面板实例（确保只有一个面板显示）
        /// </summary>
        private static ExpressionInputPanel _activeInstance;

        /// <summary>
        /// 已附加面板的UITextBox集合
        /// </summary>
        private static readonly Dictionary<UITextBox, InputPanelOptions> _attachedTextBoxes = [];

        #endregion

        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private readonly ExpressionEngine _expressionEngine;
        private readonly IPLCManager _plcManager;

        private UITextBox _targetTextBox;
        private InputPanelOptions _options;
        private bool _isSubmitting;

        // UI 控件
        private Panel _mainPanel;
        private Panel _sourcePanel;        // 左侧数据源选择区
        private Panel _keyboardPanel;      // 右侧键盘区
        private UITextBox _expressionTextBox; // 表达式输入框
        private Label _validationLabel;    // 验证状态标签
        private Label _previewLabel;       // 预览标签

        // 数据源按钮
        private Button _btnPLC;
        private Button _btnVariable;
        private Button _btnExpression;
        private Button _btnSystem;
        private Button _btnFunction;
        private Button _btnConstant;

        // 操作按钮
        private Button _btnSubmit;
        private Button _btnClose;
        private Button _btnClear;
        private Button _btnBackspace;

        // 验证定时器
        private System.Windows.Forms.Timer _validationTimer;

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

            InitializePanel();
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

            InitializePanel();
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

            // 如果已附加，先移除
            if (_attachedTextBoxes.ContainsKey(textBox))
            {
                DetachFrom(textBox);
            }

            // 保存配置
            _attachedTextBoxes[textBox] = options;

            // 添加点击事件
            textBox.Click += AttachedTextBox_Click;
            textBox.KeyDown += AttachedTextBox_KeyDown;

            // 添加视觉提示（可选：添加小图标或边框效果）
            textBox.Tag = "ExpressionInput";
        }

        /// <summary>
        /// 从UITextBox移除附加
        /// </summary>
        public static void DetachFrom(UITextBox textBox)
        {
            if (textBox == null) return;

            if (_attachedTextBoxes.ContainsKey(textBox))
            {
                textBox.Click -= AttachedTextBox_Click;
                textBox.KeyDown -= AttachedTextBox_KeyDown;
                _attachedTextBoxes.Remove(textBox);
                textBox.Tag = null;
            }
        }

        /// <summary>
        /// 显示面板（手动调用）
        /// </summary>
        /// <param name="textBox">目标UITextBox</param>
        /// <param name="options">配置选项</param>
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
        /// 初始化面板
        /// </summary>
        private void InitializePanel()
        {
            // 窗体基本设置
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.BackColor = BackgroundColor;
            this.Size = new Size(720, 280);
            this.Padding = new Padding(1);

            // 添加阴影边框效果
            this.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(200, 200, 200), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            };

            // 创建主面板
            CreateMainPanel();

            // 创建验证定时器
            _validationTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _validationTimer.Tick += (s, e) =>
            {
                _validationTimer.Stop();
                ValidateExpression();
            };

            // 失去焦点时关闭（可选）
            this.Deactivate += (s, e) =>
            {
                if (_options?.CloseOnClickOutside == true && !_isSubmitting)
                {
                    ClosePanel();
                }
            };
        }

        /// <summary>
        /// 创建主面板
        /// </summary>
        private void CreateMainPanel()
        {
            _mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = BackgroundColor,
                Padding = new Padding(8)
            };
            this.Controls.Add(_mainPanel);

            // 顶部：表达式输入区
            CreateExpressionArea();

            // 中部：左右分栏
            var contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 8, 0, 0)
            };
            _mainPanel.Controls.Add(contentPanel);

            // 左侧：数据源选择
            CreateSourcePanel(contentPanel);

            // 右侧：键盘区
            CreateKeyboardPanel(contentPanel);
        }

        /// <summary>
        /// 创建表达式输入区域
        /// </summary>
        private void CreateExpressionArea()
        {
            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                Padding = new Padding(0, 0, 0, 5)
            };
            _mainPanel.Controls.Add(topPanel);

            // 表达式输入框
            _expressionTextBox = new UITextBox
            {
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Consolas", 11f),
                //BorderStyle = BorderStyle.FixedSingle,
            };
            _expressionTextBox.TextChanged += (s, e) =>
            {
                _validationTimer.Stop();
                _validationTimer.Start();
            };
            topPanel.Controls.Add(_expressionTextBox);

            // 验证状态和预览区域
            var statusPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                Padding = new Padding(0, 5, 0, 0)
            };
            topPanel.Controls.Add(statusPanel);
            statusPanel.BringToFront();

            _validationLabel = new Label
            {
                AutoSize = true,
                Location = new Point(0, 5),
                Font = new Font("微软雅黑", 9f),
                ForeColor = Color.Gray,
                Text = "✓ 准备就绪"
            };
            statusPanel.Controls.Add(_validationLabel);

            _previewLabel = new Label
            {
                AutoSize = true,
                Location = new Point(200, 5),
                Font = new Font("微软雅黑", 9f),
                ForeColor = Color.DimGray,
                Text = ""
            };
            statusPanel.Controls.Add(_previewLabel);
        }

        /// <summary>
        /// 创建数据源选择面板
        /// </summary>
        private void CreateSourcePanel(Panel parent)
        {
            _sourcePanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 140,
                Padding = new Padding(0, 0, 8, 0)
            };
            parent.Controls.Add(_sourcePanel);

            var buttonHeight = 32;
            var buttonSpacing = 4;
            var currentY = 0;

            // PLC选择按钮
            _btnPLC = CreateSourceButton("PLC地址 >>", currentY);
            _btnPLC.Click += BtnPLC_Click;
            _sourcePanel.Controls.Add(_btnPLC);
            currentY += buttonHeight + buttonSpacing;

            // 变量选择按钮
            _btnVariable = CreateSourceButton("变量选择 >>", currentY);
            _btnVariable.Click += BtnVariable_Click;
            _sourcePanel.Controls.Add(_btnVariable);
            currentY += buttonHeight + buttonSpacing;

            // 表达式模板按钮
            _btnExpression = CreateSourceButton("表达式 >>", currentY);
            _btnExpression.Click += BtnExpression_Click;
            _sourcePanel.Controls.Add(_btnExpression);
            currentY += buttonHeight + buttonSpacing;

            // 系统属性按钮
            _btnSystem = CreateSourceButton("系统属性 >>", currentY);
            _btnSystem.Click += BtnSystem_Click;
            _sourcePanel.Controls.Add(_btnSystem);
            currentY += buttonHeight + buttonSpacing;

            // 函数选择按钮
            _btnFunction = CreateSourceButton("函数选择 >>", currentY);
            _btnFunction.Click += BtnFunction_Click;
            _sourcePanel.Controls.Add(_btnFunction);
            currentY += buttonHeight + buttonSpacing;

            // 常量输入按钮
            _btnConstant = CreateSourceButton("常量输入 >>", currentY);
            _btnConstant.Click += BtnConstant_Click;
            _sourcePanel.Controls.Add(_btnConstant);
        }

        /// <summary>
        /// 创建数据源按钮
        /// </summary>
        private Button CreateSourceButton(string text, int top)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(0, top),
                Size = new Size(130, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = ButtonColor,
                Font = new Font("微软雅黑", 9f),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btn.FlatAppearance.MouseOverBackColor = ButtonHoverColor;

            return btn;
        }

        /// <summary>
        /// 创建键盘面板
        /// </summary>
        private void CreateKeyboardPanel(Panel parent)
        {
            _keyboardPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8, 0, 0, 0)
            };
            parent.Controls.Add(_keyboardPanel);

            // 使用 TableLayoutPanel 创建按钮网格
            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 11,
                RowCount = 5,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            // 设置列宽
            for (int i = 0; i < 10; i++)
            {
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9f));
            }
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f)); // 最后一列稍宽

            // 设置行高
            for (int i = 0; i < 5; i++)
            {
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
            }

            _keyboardPanel.Controls.Add(grid);

            // 第一行：逻辑运算符
            AddKeyButton(grid, "and", 0, 0, () => InsertText(" and "));
            AddKeyButton(grid, "or", 0, 1, () => InsertText(" or "));
            AddKeyButton(grid, "not", 0, 2, () => InsertText("not "));
            AddKeyButton(grid, "xor", 0, 3, () => InsertText(" xor "));
            AddKeyButton(grid, "true", 0, 4, () => InsertText("true"), SuccessColor);
            AddKeyButton(grid, "false", 0, 5, () => InsertText("false"), ErrorColor);
            AddKeyButton(grid, "", 0, 6); // 空白
            AddKeyButton(grid, "", 0, 7); // 空白
            AddKeyButton(grid, "", 0, 8); // 空白
            AddKeyButton(grid, "", 0, 9); // 空白
            AddKeyButton(grid, "", 0, 10); // 空白

            // 第二行：括号和比较运算符
            AddKeyButton(grid, "(", 1, 0, () => InsertText("("));
            AddKeyButton(grid, ")", 1, 1, () => InsertText(")"));
            AddKeyButton(grid, "<", 1, 2, () => InsertText(" < "));
            AddKeyButton(grid, ">", 1, 3, () => InsertText(" > "));
            AddKeyButton(grid, "+", 1, 4, () => InsertText(" + "));
            AddKeyButton(grid, "-", 1, 5, () => InsertText(" - "));
            AddKeyButton(grid, "7", 1, 6, () => InsertText("7"));
            AddKeyButton(grid, "8", 1, 7, () => InsertText("8"));
            AddKeyButton(grid, "9", 1, 8, () => InsertText("9"));
            _btnBackspace = AddKeyButton(grid, "⌫", 1, 9, DoBackspace);
            AddKeyButton(grid, "", 1, 10); // 空白

            // 第三行
            AddKeyButton(grid, "[", 2, 0, () => InsertText("["));
            AddKeyButton(grid, "]", 2, 1, () => InsertText("]"));
            AddKeyButton(grid, "<=", 2, 2, () => InsertText(" <= "));
            AddKeyButton(grid, ">=", 2, 3, () => InsertText(" >= "));
            AddKeyButton(grid, "*", 2, 4, () => InsertText(" * "));
            AddKeyButton(grid, "/", 2, 5, () => InsertText(" / "));
            AddKeyButton(grid, "4", 2, 6, () => InsertText("4"));
            AddKeyButton(grid, "5", 2, 7, () => InsertText("5"));
            AddKeyButton(grid, "6", 2, 8, () => InsertText("6"));
            // 提交按钮（跨两行）
            _btnSubmit = AddKeyButton(grid, "提交", 2, 9, DoSubmit, SuccessColor);
            grid.SetRowSpan(_btnSubmit, 2);
            _btnSubmit.Font = new Font("微软雅黑", 11f, FontStyle.Bold);
            _btnSubmit.ForeColor = Color.White;

            // 第四行
            AddKeyButton(grid, "{", 3, 0, () => InsertText("{"));
            AddKeyButton(grid, "}", 3, 1, () => InsertText("}"));
            AddKeyButton(grid, "!=", 3, 2, () => InsertText(" != "));
            AddKeyButton(grid, "==", 3, 3, () => InsertText(" == "));
            AddKeyButton(grid, "\"", 3, 4, () => InsertText("\""));
            AddKeyButton(grid, ".", 3, 5, () => InsertText("."));
            AddKeyButton(grid, "1", 3, 6, () => InsertText("1"));
            AddKeyButton(grid, "2", 3, 7, () => InsertText("2"));
            AddKeyButton(grid, "3", 3, 8, () => InsertText("3"));
            // 提交按钮在上一行已添加

            // 第五行
            _btnClear = AddKeyButton(grid, "清空", 4, 0, DoClear);
            _btnClear.BackColor = Color.FromArgb(255, 193, 7);
            AddKeyButton(grid, "空格", 4, 1, () => InsertText(" "));
            AddKeyButton(grid, "<<<", 4, 2, MoveCursorLeft);
            AddKeyButton(grid, ">>>", 4, 3, MoveCursorRight);
            AddKeyButton(grid, "%", 4, 4, () => InsertText(" % "));
            AddKeyButton(grid, "_", 4, 5, () => InsertText("_"));
            AddKeyButton(grid, "0", 4, 6, () => InsertText("0"));
            AddKeyButton(grid, ".", 4, 7, () => InsertText("."));
            AddKeyButton(grid, "+/-", 4, 8, ToggleSign);
            _btnClose = AddKeyButton(grid, "关闭", 4, 9, ClosePanel);
            _btnClose.BackColor = Color.FromArgb(108, 117, 125);
            _btnClose.ForeColor = Color.White;
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

            // 设置标题（如果窗体有标题栏的话）
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
            _validationTimer?.Stop();

            if (_activeInstance == this)
            {
                _activeInstance = null;
            }

            PanelClosed?.Invoke(this, EventArgs.Empty);
            this.Close();
            this.Dispose();
        }

        #endregion

        #region 输入操作

        /// <summary>
        /// 插入文本到表达式
        /// </summary>
        private void InsertText(string text)
        {
            if (_expressionTextBox == null) return;

            int selStart = _expressionTextBox.SelectionStart;
            int selLength = _expressionTextBox.SelectionLength;

            string currentText = _expressionTextBox.Text;

            // 替换选中内容或插入
            string newText = currentText.Substring(0, selStart) +
                             text +
                             currentText.Substring(selStart + selLength);

            _expressionTextBox.Text = newText;
            _expressionTextBox.SelectionStart = selStart + text.Length;
            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 退格
        /// </summary>
        private void DoBackspace()
        {
            if (_expressionTextBox == null) return;

            int selStart = _expressionTextBox.SelectionStart;
            int selLength = _expressionTextBox.SelectionLength;

            if (selLength > 0)
            {
                // 删除选中内容
                _expressionTextBox.Text = _expressionTextBox.Text.Remove(selStart, selLength);
                _expressionTextBox.SelectionStart = selStart;
            }
            else if (selStart > 0)
            {
                // 删除光标前一个字符
                _expressionTextBox.Text = _expressionTextBox.Text.Remove(selStart - 1, 1);
                _expressionTextBox.SelectionStart = selStart - 1;
            }

            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void DoClear()
        {
            _expressionTextBox.Text = string.Empty;
            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 光标左移
        /// </summary>
        private void MoveCursorLeft()
        {
            if (_expressionTextBox.SelectionStart > 0)
            {
                _expressionTextBox.SelectionStart--;
            }
            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 光标右移
        /// </summary>
        private void MoveCursorRight()
        {
            if (_expressionTextBox.SelectionStart < _expressionTextBox.Text.Length)
            {
                _expressionTextBox.SelectionStart++;
            }
            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 切换正负号
        /// </summary>
        private void ToggleSign()
        {
            var text = _expressionTextBox.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            if (text.StartsWith("-"))
            {
                _expressionTextBox.Text = text.Substring(1);
            }
            else if (text.StartsWith("(") && text.EndsWith(")"))
            {
                _expressionTextBox.Text = "-" + text;
            }
            else
            {
                _expressionTextBox.Text = "-(" + text + ")";
            }

            _expressionTextBox.SelectionStart = _expressionTextBox.Text.Length;
            _expressionTextBox.Focus();
        }

        /// <summary>
        /// 提交表达式
        /// </summary>
        private void DoSubmit()
        {
            _isSubmitting = true;

            try
            {
                var expression = _expressionTextBox.Text;

                // 验证
                ValidateExpression();

                // 创建事件参数
                var args = new ExpressionSubmitEventArgs
                {
                    Expression = expression,
                    IsValid = IsValid,
                    ErrorMessage = IsValid ? null : _validationLabel.Text
                };

                // 触发事件
                ExpressionSubmit?.Invoke(this, args);

                // 如果没有取消，更新目标UITextBox
                if (!args.Cancel && _targetTextBox != null)
                {
                    _targetTextBox.Text = expression;
                }

                // 关闭面板
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
            var expression = _expressionTextBox?.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(expression))
            {
                SetValidationStatus(true, "✓ 准备就绪", Color.Gray);
                _previewLabel.Text = "";
                return;
            }

            try
            {
                // 使用表达式引擎验证
                if (_expressionEngine != null)
                {
                    var result = _expressionEngine.EvaluateExpression(expression);
                    IsValid = true;
                    SetValidationStatus(true, "✓ 表达式有效", SuccessColor);
                    _previewLabel.Text = $"预览: {result}";
                    _previewLabel.ForeColor = Color.DimGray;
                }
                else
                {
                    // 简单验证：检查括号匹配
                    IsValid = ValidateBrackets(expression);
                    if (IsValid)
                    {
                        SetValidationStatus(true, "✓ 语法正确", SuccessColor);
                    }
                    else
                    {
                        SetValidationStatus(false, "✗ 括号不匹配", ErrorColor);
                    }
                    _previewLabel.Text = "";
                }
            }
            catch (Exception ex)
            {
                IsValid = false;
                SetValidationStatus(false, $"✗ {ex.Message}", ErrorColor);
                _previewLabel.Text = "";
            }
        }

        /// <summary>
        /// 设置验证状态
        /// </summary>
        private void SetValidationStatus(bool isValid, string message, Color color)
        {
            IsValid = isValid;
            _validationLabel.Text = message;
            _validationLabel.ForeColor = color;
        }

        /// <summary>
        /// 验证括号匹配
        /// </summary>
        private bool ValidateBrackets(string expression)
        {
            var stack = new Stack<char>();
            var pairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };

            foreach (var c in expression)
            {
                if (c == '(' || c == '[' || c == '{')
                {
                    stack.Push(c);
                }
                else if (pairs.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Pop() != pairs[c])
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
        }

        #endregion

        #region 数据源选择事件

        private void BtnPLC_Click(object sender, EventArgs e)
        {
            ShowPLCSelector();
        }

        private void BtnVariable_Click(object sender, EventArgs e)
        {
            ShowVariableSelector();
        }

        private void BtnExpression_Click(object sender, EventArgs e)
        {
            ShowExpressionTemplates();
        }

        private void BtnSystem_Click(object sender, EventArgs e)
        {
            ShowSystemPropertySelector();
        }

        private void BtnFunction_Click(object sender, EventArgs e)
        {
            ShowFunctionSelector();
        }

        private void BtnConstant_Click(object sender, EventArgs e)
        {
            ShowConstantInput();
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

                    if (variables.Any())
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
                        { "范围判断", "{变量} >= 最小值 and {变量} <= 最大值" }
                    }
                },
                {
                    "🔢 数学运算", new Dictionary<string, string>
                    {
                        { "加法", "{变量1} + {变量2}" },
                        { "减法", "{变量1} - {变量2}" },
                        { "乘法", "{变量1} * {变量2}" },
                        { "除法", "{变量1} / {变量2}" },
                        { "取余", "{变量1} % {变量2}" },
                        { "绝对值", "Math.Abs({变量})" },
                        { "四舍五入", "Math.Round({变量}, 2)" }
                    }
                },
                {
                    "🔗 逻辑运算", new Dictionary<string, string>
                    {
                        { "与", "{条件1} and {条件2}" },
                        { "或", "{条件1} or {条件2}" },
                        { "非", "not {条件}" },
                        { "复合条件", "({条件1} and {条件2}) or {条件3}" }
                    }
                },
                {
                    "📝 字符串", new Dictionary<string, string>
                    {
                        { "拼接", "\"{前缀}\" + {变量} + \"{后缀}\"" },
                        { "格式化数值", "FORMAT({变量}, \"0.00\")" },
                        { "当前日期", "FORMAT(NOW(), \"yyyy-MM-dd\")" },
                        { "当前时间", "FORMAT(NOW(), \"HH:mm:ss\")" }
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
        private void ShowSystemPropertySelector()
        {
            var menu = new ContextMenuStrip();
            menu.Font = new Font("微软雅黑", 9f);

            var properties = new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "👤 用户信息", new Dictionary<string, string>
                    {
                        { "用户名", "NewUsers.NewUserInfo.Username" },
                        { "角色名称", "NewUsers.NewUserInfo.RoleName" },
                        { "用户ID", "NewUsers.NewUserInfo.UserId" }
                    }
                },
                {
                    "📋 测试信息", new Dictionary<string, string>
                    {
                        { "型号名称", "VarHelper.TestViewModel.ModelName" },
                        { "图号", "VarHelper.TestViewModel.DrawingNo" },
                        { "序列号", "VarHelper.TestViewModel.SerialNo" },
                        { "测试结果", "VarHelper.TestViewModel.TestResult" }
                    }
                },
                {
                    "🕐 日期时间", new Dictionary<string, string>
                    {
                        { "当前日期时间", "DateTime.Now" },
                        { "年份", "DateTime.Now.Year" },
                        { "月份", "DateTime.Now.Month" },
                        { "日期", "DateTime.Now.Day" },
                        { "格式化日期", "DateTime.Now.ToString(\"yyyy-MM-dd\")" },
                        { "格式化时间", "DateTime.Now.ToString(\"HH:mm:ss\")" }
                    }
                }
            };

            foreach (var category in properties)
            {
                var categoryItem = new ToolStripMenuItem(category.Key);
                categoryItem.Font = new Font("微软雅黑", 9f, FontStyle.Bold);

                foreach (var prop in category.Value)
                {
                    var propItem = new ToolStripMenuItem(prop.Key);
                    propItem.ToolTipText = prop.Value;
                    propItem.Click += (s, e) =>
                    {
                        InsertText(prop.Value);

                        SourceSelected?.Invoke(this, new SourceSelectedEventArgs
                        {
                            SourceType = InputModules.System,
                            SelectedValue = prop.Key,
                            FormattedExpression = prop.Value
                        });
                    };
                    categoryItem.DropDownItems.Add(propItem);
                }

                menu.Items.Add(categoryItem);
            }

            menu.Show(_btnSystem, new Point(0, _btnSystem.Height));
        }

        /// <summary>
        /// 显示函数选择器
        /// </summary>
        private void ShowFunctionSelector()
        {
            var menu = new ContextMenuStrip
            {
                Font = new Font("微软雅黑", 9f)
            };

            var functions = new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "🔢 数学函数", new Dictionary<string, string>
                    {
                        { "绝对值 Abs", "Math.Abs(x)" },
                        { "四舍五入 Round", "Math.Round(x, 2)" },
                        { "向上取整 Ceiling", "Math.Ceiling(x)" },
                        { "向下取整 Floor", "Math.Floor(x)" },
                        { "最大值 Max", "Math.Max(a, b)" },
                        { "最小值 Min", "Math.Min(a, b)" },
                        { "幂运算 Pow", "Math.Pow(x, n)" },
                        { "平方根 Sqrt", "Math.Sqrt(x)" },
                        { "正弦 Sin", "Math.Sin(x)" },
                        { "余弦 Cos", "Math.Cos(x)" }
                    }
                },
                {
                    "📝 字符串函数", new Dictionary<string, string>
                    {
                        { "长度 Length", "str.Length" },
                        { "转大写 ToUpper", "str.ToUpper()" },
                        { "转小写 ToLower", "str.ToLower()" },
                        { "去空格 Trim", "str.Trim()" },
                        { "截取 Substring", "str.Substring(start, length)" },
                        { "替换 Replace", "str.Replace(old, new)" },
                        { "包含 Contains", "str.Contains(value)" }
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
                    var funcItem = new ToolStripMenuItem(func.Key);
                    funcItem.ToolTipText = func.Value;
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
        private void ShowConstantInput()
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
                { "换行符", "\"\\n\"" }
            };

            foreach (var constant in constants)
            {
                var item = new ToolStripMenuItem(constant.Key);
                item.Click += (s, e) =>
                {
                    InsertText(constant.Value);
                };
                menu.Items.Add(item);
            }

            menu.Items.Add(new ToolStripSeparator());

            // 自定义数值输入
            var customItem = new ToolStripMenuItem("输入自定义值...");
            customItem.Click += (s, e) =>
            {
                using var inputForm = new Form
                {
                    Text = "输入常量值",
                    Size = new Size(300, 120),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var txtValue = new UITextBox
                {
                    Location = new Point(20, 20),
                    Size = new Size(240, 25),
                    Font = new Font("微软雅黑", 10f)
                };
                inputForm.Controls.Add(txtValue);

                var btnOK = new Button
                {
                    Text = "确定",
                    Location = new Point(100, 55),
                    Size = new Size(80, 28),
                    DialogResult = DialogResult.OK
                };
                inputForm.Controls.Add(btnOK);
                inputForm.AcceptButton = btnOK;

                if (inputForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(txtValue.Text))
                {
                    InsertText(txtValue.Text);
                }
            };
            menu.Items.Add(customItem);

            menu.Show(_btnConstant, new Point(0, _btnConstant.Height));
        }

        #endregion
    }
}
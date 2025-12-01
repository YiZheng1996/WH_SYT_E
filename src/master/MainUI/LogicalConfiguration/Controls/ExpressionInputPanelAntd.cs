using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.LogicalConfiguration.Controls
{
    /// <summary>
    /// AntdUI 风格的表达式输入面板
    /// 提供更美观的界面和更流畅的交互体验
    /// </summary>
    public partial class ExpressionInputPanelAntd: Form
    {
        #region 静态成员

        private static ExpressionInputPanelAntd _activeInstance;

        #endregion

        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private readonly ExpressionEngine _expressionEngine;
        private readonly IPLCManager _plcManager;

        private TextBox _targetTextBox;
        private InputPanelOptions _options;
        private bool _isSubmitting;

        // UI 控件
        private Panel _mainContainer;
        private Panel _headerPanel;
        private Panel _inputPanel;
        private Panel _contentPanel;
        private Panel _sourcePanel;
        private Panel _keyboardPanel;

        private TextBox _expressionTextBox;
        private Label _titleLabel;
        private Label _validationLabel;
        private Label _previewLabel;
        private PictureBox _closeButton;

        // 验证定时器
        private System.Windows.Forms.Timer _validationTimer;

        // 样式定义
        private static class Styles
        {
            // 主题色
            public static readonly Color Primary = Color.FromArgb(65, 100, 204);
            public static readonly Color PrimaryLight = Color.FromArgb(232, 237, 250);
            public static readonly Color Success = Color.FromArgb(82, 196, 26);
            public static readonly Color Error = Color.FromArgb(245, 34, 45);
            public static readonly Color Warning = Color.FromArgb(250, 173, 20);

            // 背景色
            public static readonly Color Background = Color.White;
            public static readonly Color HeaderBackground = Color.FromArgb(250, 250, 250);
            public static readonly Color ButtonBackground = Color.FromArgb(245, 245, 245);
            public static readonly Color ButtonHover = Color.FromArgb(230, 230, 230);
            public static readonly Color ButtonActive = Color.FromArgb(220, 220, 220);

            // 边框色
            public static readonly Color Border = Color.FromArgb(217, 217, 217);
            public static readonly Color BorderLight = Color.FromArgb(240, 240, 240);

            // 文字色
            public static readonly Color TextPrimary = Color.FromArgb(38, 38, 38);
            public static readonly Color TextSecondary = Color.FromArgb(140, 140, 140);
            public static readonly Color TextDisabled = Color.FromArgb(191, 191, 191);

            // 阴影
            public static readonly Color Shadow = Color.FromArgb(30, 0, 0, 0);

            // 字体
            public static readonly Font TitleFont = new Font("微软雅黑", 11f, FontStyle.Bold);
            public static readonly Font NormalFont = new Font("微软雅黑", 9f);
            public static readonly Font CodeFont = new Font("Consolas", 11f);
            public static readonly Font ButtonFont = new Font("微软雅黑", 10f);
            public static readonly Font SmallFont = new Font("微软雅黑", 8f);

            // 圆角
            public const int BorderRadius = 8;
            public const int ButtonRadius = 4;
        }

        #endregion

        #region 事件

        public event EventHandler<ExpressionSubmitEventArgs> ExpressionSubmit;
        public event EventHandler PanelClosed;

        #endregion

        #region 属性

        public string Expression
        {
            get => _expressionTextBox?.Text ?? string.Empty;
            set { if (_expressionTextBox != null) _expressionTextBox.Text = value; }
        }

        public bool IsValid { get; private set; }

        #endregion

        #region 构造函数

        public ExpressionInputPanelAntd()
        {
            _variableManager = Program.ServiceProvider?.GetService<GlobalVariableManager>();
            _expressionEngine = Program.ServiceProvider?.GetService<ExpressionEngine>();
            _plcManager = Program.ServiceProvider?.GetService<IPLCManager>();

            InitializePanel();
        }

        #endregion

        #region 静态方法

        public static void Show(TextBox textBox, InputPanelOptions options = null)
        {
            if (textBox == null) return;

            _activeInstance?.Close();

            options ??= new InputPanelOptions();
            options.InitialExpression = textBox.Text;

            var panel = new ExpressionInputPanelAntd();
            panel.SetTarget(textBox, options);
            panel.ShowPanel();

            _activeInstance = panel;
        }

        public static void AttachTo(TextBox textBox, InputPanelOptions options = null)
        {
            if (textBox == null) return;

            options ??= new InputPanelOptions();

            textBox.Click += (s, e) =>
            {
                options.InitialExpression = textBox.Text;
                Show(textBox, options);
            };

            textBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.F2 || (e.Control && e.KeyCode == Keys.Space))
                {
                    options.InitialExpression = textBox.Text;
                    Show(textBox, options);
                    e.Handled = true;
                }
            };

            textBox.Tag = "ExpressionInput";
        }

        #endregion

        #region 初始化

        private void InitializePanel()
        {
            // 窗体设置
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.BackColor = Styles.Background;
            this.Size = new Size(750, 320);
            this.DoubleBuffered = true;

            // 启用阴影
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            // 主容器（带圆角和阴影）
            _mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(1),
                BackColor = Styles.Border
            };
            this.Controls.Add(_mainContainer);

            // 内部容器
            var innerContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Styles.Background,
                Padding = new Padding(0)
            };
            _mainContainer.Controls.Add(innerContainer);

            // 创建各区域
            CreateHeader(innerContainer);
            CreateInputArea(innerContainer);
            CreateContentArea(innerContainer);

            // 验证定时器
            _validationTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _validationTimer.Tick += (s, e) =>
            {
                _validationTimer.Stop();
                ValidateExpression();
            };

            // 失焦关闭
            this.Deactivate += (s, e) =>
            {
                if (!_isSubmitting && _options?.CloseOnClickOutside == true)
                {
                    ClosePanel();
                }
            };
        }

        /// <summary>
        /// 创建头部区域
        /// </summary>
        private void CreateHeader(Panel parent)
        {
            _headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Styles.HeaderBackground,
                Padding = new Padding(16, 0, 8, 0)
            };
            parent.Controls.Add(_headerPanel);

            // 分隔线
            var separator = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = Styles.BorderLight
            };
            _headerPanel.Controls.Add(separator);

            // 标题
            _titleLabel = new Label
            {
                Text = "表达式输入",
                AutoSize = true,
                Font = Styles.TitleFont,
                ForeColor = Styles.TextPrimary,
                Location = new Point(16, 10)
            };
            _headerPanel.Controls.Add(_titleLabel);

            // 关闭按钮
            _closeButton = new PictureBox
            {
                Size = new Size(24, 24),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent,
                Location = new Point(_headerPanel.Width - 32, 8),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _closeButton.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var pen = new Pen(Styles.TextSecondary, 2);
                e.Graphics.DrawLine(pen, 6, 6, 18, 18);
                e.Graphics.DrawLine(pen, 18, 6, 6, 18);
            };
            _closeButton.Click += (s, e) => ClosePanel();
            _closeButton.MouseEnter += (s, e) => _closeButton.BackColor = Styles.ButtonHover;
            _closeButton.MouseLeave += (s, e) => _closeButton.BackColor = Color.Transparent;
            _headerPanel.Controls.Add(_closeButton);
        }

        /// <summary>
        /// 创建输入区域
        /// </summary>
        private void CreateInputArea(Panel parent)
        {
            _inputPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Styles.Background,
                Padding = new Padding(16, 12, 16, 8)
            };
            parent.Controls.Add(_inputPanel);
            _inputPanel.BringToFront();

            // 表达式输入框
            _expressionTextBox = new TextBox
            {
                Dock = DockStyle.Top,
                Height = 32,
                Font = Styles.CodeFont,
                BorderStyle = BorderStyle.FixedSingle
            };
            _expressionTextBox.TextChanged += (s, e) =>
            {
                _validationTimer.Stop();
                _validationTimer.Start();
            };
            _expressionTextBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && !e.Shift)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    DoSubmit();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    ClosePanel();
                }
            };
            _inputPanel.Controls.Add(_expressionTextBox);

            // 状态行
            var statusPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 22,
                BackColor = Styles.Background,
                Padding = new Padding(0, 4, 0, 0)
            };
            _inputPanel.Controls.Add(statusPanel);
            statusPanel.BringToFront();

            _validationLabel = new Label
            {
                AutoSize = true,
                Font = Styles.SmallFont,
                ForeColor = Styles.TextSecondary,
                Text = "✓ 准备就绪",
                Location = new Point(0, 2)
            };
            statusPanel.Controls.Add(_validationLabel);

            _previewLabel = new Label
            {
                AutoSize = true,
                Font = Styles.SmallFont,
                ForeColor = Styles.TextSecondary,
                Text = "",
                Location = new Point(150, 2)
            };
            statusPanel.Controls.Add(_previewLabel);
        }

        /// <summary>
        /// 创建内容区域
        /// </summary>
        private void CreateContentArea(Panel parent)
        {
            _contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Styles.Background,
                Padding = new Padding(16, 8, 16, 16)
            };
            parent.Controls.Add(_contentPanel);
            _contentPanel.BringToFront();

            // 左侧数据源面板
            CreateSourcePanel();

            // 右侧键盘面板
            CreateKeyboardPanel();
        }

        /// <summary>
        /// 创建数据源选择面板
        /// </summary>
        private void CreateSourcePanel()
        {
            _sourcePanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 130,
                BackColor = Styles.Background,
                Padding = new Padding(0, 0, 12, 0)
            };
            _contentPanel.Controls.Add(_sourcePanel);

            var sources = new[]
            {
                ("🔌 PLC地址", InputModules.PLC, new Action(() => ShowPLCSelector())),
                ("📋 变量选择", InputModules.Variable, new Action(() => ShowVariableSelector())),
                ("📝 表达式", InputModules.Expression, new Action(() => ShowExpressionTemplates())),
                ("⚙️ 系统属性", InputModules.System, new Action(() => ShowSystemProperties())),
                ("📐 函数", InputModules.Function, new Action(() => ShowFunctions())),
                ("🔢 常量", InputModules.Constant, new Action(() => ShowConstants()))
            };

            int y = 0;
            foreach (var (text, module, action) in sources)
            {
                var btn = CreateSourceButton(text, y, action, module);
                _sourcePanel.Controls.Add(btn);
                y += 34;
            }
        }

        /// <summary>
        /// 创建数据源按钮
        /// </summary>
        private Button CreateSourceButton(string text, int top, Action onClick, InputModules module)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(0, top),
                Size = new Size(115, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Styles.ButtonBackground,
                Font = Styles.NormalFont,
                ForeColor = Styles.TextPrimary,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(6, 0, 0, 0),
                Cursor = Cursors.Hand,
                Tag = module
            };
            btn.FlatAppearance.BorderColor = Styles.Border;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = Styles.ButtonHover;
            btn.FlatAppearance.MouseDownBackColor = Styles.ButtonActive;
            btn.Click += (s, e) => onClick?.Invoke();

            return btn;
        }

        /// <summary>
        /// 创建键盘面板
        /// </summary>
        private void CreateKeyboardPanel()
        {
            _keyboardPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Styles.Background,
                Padding = new Padding(0)
            };
            _contentPanel.Controls.Add(_keyboardPanel);

            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 11,
                RowCount = 5,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                Margin = new Padding(0)
            };

            // 列宽
            for (int i = 0; i < 10; i++)
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9f));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));

            // 行高
            for (int i = 0; i < 5; i++)
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));

            _keyboardPanel.Controls.Add(grid);

            // 按钮定义
            var buttons = new[]
            {
                // Row 0: 逻辑运算符
                ("and", 0, 0, " and "), ("or", 0, 1, " or "), ("not", 0, 2, "not "), ("xor", 0, 3, " xor "),
                ("true", 0, 4, "true"), ("false", 0, 5, "false"),

                // Row 1: 括号和比较
                ("(", 1, 0, "("), (")", 1, 1, ")"), ("<", 1, 2, " < "), (">", 1, 3, " > "),
                ("+", 1, 4, " + "), ("-", 1, 5, " - "),
                ("7", 1, 6, "7"), ("8", 1, 7, "8"), ("9", 1, 8, "9"), ("⌫", 1, 9, "BACKSPACE"),

                // Row 2
                ("[", 2, 0, "["), ("]", 2, 1, "]"), ("<=", 2, 2, " <= "), (">=", 2, 3, " >= "),
                ("*", 2, 4, " * "), ("/", 2, 5, " / "),
                ("4", 2, 6, "4"), ("5", 2, 7, "5"), ("6", 2, 8, "6"),

                // Row 3
                ("{", 3, 0, "{"), ("}", 3, 1, "}"), ("!=", 3, 2, " != "), ("==", 3, 3, " == "),
                ("\"", 3, 4, "\""), (".", 3, 5, "."),
                ("1", 3, 6, "1"), ("2", 3, 7, "2"), ("3", 3, 8, "3"),

                // Row 4
                ("清空", 4, 0, "CLEAR"), ("空格", 4, 1, " "), ("◀", 4, 2, "LEFT"), ("▶", 4, 3, "RIGHT"),
                ("%", 4, 4, " % "), ("_", 4, 5, "_"),
                ("0", 4, 6, "0"), (".", 4, 7, "."), ("±", 4, 8, "SIGN")
            };

            foreach (var (text, row, col, value) in buttons)
            {
                var btn = CreateKeyButton(text, value);
                grid.Controls.Add(btn, col, row);
            }

            // 提交按钮（跨2行）
            var submitBtn = CreateKeyButton("提 交", "SUBMIT");
            submitBtn.BackColor = Styles.Success;
            submitBtn.ForeColor = Color.White;
            submitBtn.Font = new Font("微软雅黑", 11f, FontStyle.Bold);
            grid.Controls.Add(submitBtn, 9, 2);
            grid.SetRowSpan(submitBtn, 2);

            // 关闭按钮
            var closeBtn = CreateKeyButton("关 闭", "CLOSE");
            closeBtn.BackColor = Color.FromArgb(108, 117, 125);
            closeBtn.ForeColor = Color.White;
            grid.Controls.Add(closeBtn, 9, 4);
        }

        /// <summary>
        /// 创建键盘按钮
        /// </summary>
        private Button CreateKeyButton(string text, string value)
        {
            var btn = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Styles.ButtonBackground,
                ForeColor = Styles.TextPrimary,
                Font = Styles.ButtonFont,
                Cursor = Cursors.Hand,
                Tag = value
            };
            btn.FlatAppearance.BorderColor = Styles.Border;
            btn.FlatAppearance.MouseOverBackColor = Styles.ButtonHover;
            btn.FlatAppearance.MouseDownBackColor = Styles.ButtonActive;

            // 特殊按钮颜色
            if (text == "true")
            {
                btn.BackColor = Color.FromArgb(237, 247, 237);
                btn.ForeColor = Styles.Success;
            }
            else if (text == "false")
            {
                btn.BackColor = Color.FromArgb(255, 241, 240);
                btn.ForeColor = Styles.Error;
            }
            else if (text == "清空")
            {
                btn.BackColor = Color.FromArgb(255, 251, 230);
                btn.ForeColor = Styles.Warning;
            }

            btn.Click += KeyButton_Click;

            return btn;
        }

        #endregion

        #region 事件处理

        private void KeyButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string value)
            {
                switch (value)
                {
                    case "BACKSPACE":
                        DoBackspace();
                        break;
                    case "CLEAR":
                        DoClear();
                        break;
                    case "LEFT":
                        MoveCursor(-1);
                        break;
                    case "RIGHT":
                        MoveCursor(1);
                        break;
                    case "SIGN":
                        ToggleSign();
                        break;
                    case "SUBMIT":
                        DoSubmit();
                        break;
                    case "CLOSE":
                        ClosePanel();
                        break;
                    default:
                        InsertText(value);
                        break;
                }
            }
        }

        #endregion

        #region 操作方法

        private void InsertText(string text)
        {
            if (_expressionTextBox == null) return;

            int start = _expressionTextBox.SelectionStart;
            int length = _expressionTextBox.SelectionLength;

            _expressionTextBox.Text = _expressionTextBox.Text
                .Remove(start, length)
                .Insert(start, text);
            _expressionTextBox.SelectionStart = start + text.Length;
            _expressionTextBox.Focus();
        }

        private void DoBackspace()
        {
            if (_expressionTextBox == null) return;

            int start = _expressionTextBox.SelectionStart;
            int length = _expressionTextBox.SelectionLength;

            if (length > 0)
            {
                _expressionTextBox.Text = _expressionTextBox.Text.Remove(start, length);
                _expressionTextBox.SelectionStart = start;
            }
            else if (start > 0)
            {
                _expressionTextBox.Text = _expressionTextBox.Text.Remove(start - 1, 1);
                _expressionTextBox.SelectionStart = start - 1;
            }
            _expressionTextBox.Focus();
        }

        private void DoClear()
        {
            _expressionTextBox.Text = string.Empty;
            _expressionTextBox.Focus();
        }

        private void MoveCursor(int delta)
        {
            int newPos = _expressionTextBox.SelectionStart + delta;
            if (newPos >= 0 && newPos <= _expressionTextBox.Text.Length)
            {
                _expressionTextBox.SelectionStart = newPos;
            }
            _expressionTextBox.Focus();
        }

        private void ToggleSign()
        {
            var text = _expressionTextBox.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            _expressionTextBox.Text = text.StartsWith("-")
                ? text.Substring(1)
                : "-" + text;
            _expressionTextBox.SelectionStart = _expressionTextBox.Text.Length;
            _expressionTextBox.Focus();
        }

        private void DoSubmit()
        {
            _isSubmitting = true;

            try
            {
                var expression = _expressionTextBox.Text;
                ValidateExpression();

                var args = new ExpressionSubmitEventArgs
                {
                    Expression = expression,
                    IsValid = IsValid
                };

                ExpressionSubmit?.Invoke(this, args);

                if (!args.Cancel && _targetTextBox != null)
                {
                    _targetTextBox.Text = expression;
                }

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

        private void ValidateExpression()
        {
            var expression = _expressionTextBox?.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(expression))
            {
                SetStatus("✓ 准备就绪", Styles.TextSecondary);
                _previewLabel.Text = "";
                IsValid = true;
                return;
            }

            try
            {
                if (_expressionEngine != null)
                {
                    var result = _expressionEngine.EvaluateExpression(expression);
                    IsValid = true;
                    SetStatus("✓ 表达式有效", Styles.Success);
                    _previewLabel.Text = $"= {result}";
                    _previewLabel.ForeColor = Styles.Primary;
                }
                else
                {
                    IsValid = ValidateBrackets(expression);
                    SetStatus(IsValid ? "✓ 语法正确" : "✗ 括号不匹配",
                        IsValid ? Styles.Success : Styles.Error);
                    _previewLabel.Text = "";
                }
            }
            catch (Exception ex)
            {
                IsValid = false;
                SetStatus($"✗ {TruncateMessage(ex.Message, 40)}", Styles.Error);
                _previewLabel.Text = "";
            }
        }

        private void SetStatus(string message, Color color)
        {
            _validationLabel.Text = message;
            _validationLabel.ForeColor = color;
        }

        private string TruncateMessage(string message, int maxLength)
        {
            return message.Length <= maxLength ? message : message.Substring(0, maxLength) + "...";
        }

        private bool ValidateBrackets(string expression)
        {
            var stack = new Stack<char>();
            var pairs = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' } };

            foreach (var c in expression)
            {
                if ("([{".Contains(c)) stack.Push(c);
                else if (pairs.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Pop() != pairs[c]) return false;
                }
            }
            return stack.Count == 0;
        }

        #endregion

        #region 数据源选择器

        private void ShowPLCSelector()
        {
            ShowContextMenu(_sourcePanel.Controls.OfType<Button>().First(b => (InputModules)b.Tag == InputModules.PLC),
                BuildPLCMenu());
        }

        private void ShowVariableSelector()
        {
            ShowContextMenu(_sourcePanel.Controls.OfType<Button>().First(b => (InputModules)b.Tag == InputModules.Variable),
                BuildVariableMenu());
        }

        private void ShowExpressionTemplates()
        {
            ShowContextMenu(_sourcePanel.Controls.OfType<Button>().First(b => (InputModules)b.Tag == InputModules.Expression),
                BuildTemplateMenu());
        }

        private void ShowSystemProperties()
        {
            ShowContextMenu(_sourcePanel.Controls.OfType<Button>().First(b => (InputModules)b.Tag == InputModules.System),
                BuildSystemMenu());
        }

        private void ShowFunctions()
        {
            ShowContextMenu(_sourcePanel.Controls.OfType<Button>().First(b => (InputModules)b.Tag == InputModules.Function),
                BuildFunctionMenu());
        }

        private void ShowConstants()
        {
            ShowContextMenu(_sourcePanel.Controls.OfType<Button>().First(b => (InputModules)b.Tag == InputModules.Constant),
                BuildConstantMenu());
        }

        private void ShowContextMenu(Control anchor, ContextMenuStrip menu)
        {
            menu.Show(anchor, new Point(anchor.Width, 0));
        }

        private ContextMenuStrip CreateStyledMenu()
        {
            var menu = new ContextMenuStrip
            {
                Font = Styles.NormalFont,
                BackColor = Styles.Background,
                ShowImageMargin = false
            };
            return menu;
        }

        private ContextMenuStrip BuildPLCMenu()
        {
            var menu = CreateStyledMenu();
            // 这里添加PLC选项，实际实现需要异步加载
            menu.Items.Add(new ToolStripMenuItem("(加载PLC模块...)") { Enabled = false });
            return menu;
        }

        private ContextMenuStrip BuildVariableMenu()
        {
            var menu = CreateStyledMenu();

            if (_variableManager != null)
            {
                var variables = _variableManager.GetAllVariables()
                    .Where(v => !v.IsSystemVariable)
                    .OrderBy(v => v.VarName)
                    .Take(20);

                foreach (var v in variables)
                {
                    var item = new ToolStripMenuItem($"{v.VarName} ({v.VarType})");
                    item.Click += (s, e) => InsertText($"{{{v.VarName}}}");
                    menu.Items.Add(item);
                }

                if (!variables.Any())
                {
                    menu.Items.Add(new ToolStripMenuItem("(无可用变量)") { Enabled = false });
                }
            }

            return menu;
        }

        private ContextMenuStrip BuildTemplateMenu()
        {
            var menu = CreateStyledMenu();

            var templates = new Dictionary<string, string>
            {
                { "等于判断", "{变量} == 值" },
                { "大于判断", "{变量} > 值" },
                { "范围判断", "{变量} >= 最小 and {变量} <= 最大" },
                { "加法运算", "{变量1} + {变量2}" },
                { "格式化数值", "FORMAT({变量}, \"0.00\")" }
            };

            foreach (var t in templates)
            {
                var item = new ToolStripMenuItem(t.Key) { ToolTipText = t.Value };
                item.Click += (s, e) => InsertText(t.Value);
                menu.Items.Add(item);
            }

            return menu;
        }

        private ContextMenuStrip BuildSystemMenu()
        {
            var menu = CreateStyledMenu();

            var props = new Dictionary<string, string>
            {
                { "用户名", "NewUsers.NewUserInfo.Username" },
                { "当前日期", "DateTime.Now.ToString(\"yyyy-MM-dd\")" },
                { "当前时间", "DateTime.Now.ToString(\"HH:mm:ss\")" }
            };

            foreach (var p in props)
            {
                var item = new ToolStripMenuItem(p.Key);
                item.Click += (s, e) => InsertText(p.Value);
                menu.Items.Add(item);
            }

            return menu;
        }

        private ContextMenuStrip BuildFunctionMenu()
        {
            var menu = CreateStyledMenu();

            var funcs = new Dictionary<string, string>
            {
                { "绝对值", "Math.Abs(x)" },
                { "四舍五入", "Math.Round(x, 2)" },
                { "最大值", "Math.Max(a, b)" },
                { "最小值", "Math.Min(a, b)" }
            };

            foreach (var f in funcs)
            {
                var item = new ToolStripMenuItem(f.Key) { ToolTipText = f.Value };
                item.Click += (s, e) => InsertText(f.Value);
                menu.Items.Add(item);
            }

            return menu;
        }

        private ContextMenuStrip BuildConstantMenu()
        {
            var menu = CreateStyledMenu();

            var constants = new[] { ("true", "true"), ("false", "false"), ("null", "null"), ("π", "Math.PI") };

            foreach (var (name, value) in constants)
            {
                var item = new ToolStripMenuItem(name);
                item.Click += (s, e) => InsertText(value);
                menu.Items.Add(item);
            }

            return menu;
        }

        #endregion

        #region 显示和关闭

        private void SetTarget(TextBox textBox, InputPanelOptions options)
        {
            _targetTextBox = textBox;
            _options = options;
            _expressionTextBox.Text = options.InitialExpression;

            if (!string.IsNullOrEmpty(options.Title))
            {
                _titleLabel.Text = options.Title;
            }

            UpdateModuleVisibility();
        }

        private void UpdateModuleVisibility()
        {
            if (_options == null) return;

            foreach (Control ctrl in _sourcePanel.Controls)
            {
                if (ctrl is Button btn && btn.Tag is InputModules module)
                {
                    btn.Visible = _options.EnabledModules.HasFlag(module);
                }
            }

            // 重排按钮
            int y = 0;
            foreach (Control ctrl in _sourcePanel.Controls.OfType<Button>().Where(b => b.Visible))
            {
                ctrl.Location = new Point(0, y);
                y += 34;
            }
        }

        private void ShowPanel()
        {
            CalculatePosition();
            this.Show();
            _expressionTextBox.Focus();
            _expressionTextBox.SelectionStart = _expressionTextBox.Text.Length;
        }

        private void CalculatePosition()
        {
            if (_targetTextBox == null) return;

            var screenPoint = _targetTextBox.PointToScreen(new Point(0, _targetTextBox.Height));
            var screen = Screen.FromControl(_targetTextBox);

            int x = screenPoint.X;
            int y = screenPoint.Y + 5;

            if (y + Height > screen.WorkingArea.Bottom)
                y = _targetTextBox.PointToScreen(Point.Empty).Y - Height - 5;

            if (x + Width > screen.WorkingArea.Right)
                x = screen.WorkingArea.Right - Width - 10;

            if (x < screen.WorkingArea.Left)
                x = screen.WorkingArea.Left + 10;

            Location = new Point(x, y);
        }

        private void ClosePanel()
        {
            _validationTimer?.Stop();
            if (_activeInstance == this) _activeInstance = null;
            PanelClosed?.Invoke(this, EventArgs.Empty);
            Close();
            Dispose();
        }

        #endregion

    }
}
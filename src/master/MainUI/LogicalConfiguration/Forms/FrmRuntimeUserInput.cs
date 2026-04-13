using MainUI.LogicalConfiguration.Helpers;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 运行时用户输入弹窗
    /// 工作流执行到「用户输入」步骤时弹出此窗体，暂停流程等待操作员填值
    /// </summary>
    public partial class FrmRuntimeUserInput : UIForm
    {
        #region 字段

        private readonly Parameter_UserInput _param;
        private readonly GlobalVariableManager _variableManager;
        private int _remainingSeconds;

        #endregion

        #region 属性

        /// <summary>
        /// 操作员输入的值（确认后可读取）
        /// </summary>
        public string InputValue { get; private set; }

        #endregion

        #region 构造函数

        public FrmRuntimeUserInput(Parameter_UserInput param, GlobalVariableManager variableManager = null)
        {
            _param = param ?? throw new ArgumentNullException(nameof(param));
            _variableManager = variableManager;
            InitializeComponent();
            SetupUI();
        }

        #endregion

        #region 初始化

        private void SetupUI()
        {
            // ── 窗口标题 ──────────────────────────────────
            Text = string.IsNullOrWhiteSpace(_param.Title) ? "请输入" : _param.Title;

            // ── 提示文字 ──────────────────────────────────
            var renderedPrompt = PromptTextRenderer.Render(_param.Prompt, _variableManager);
            lblPrompt.Text = string.IsNullOrWhiteSpace(renderedPrompt)
                ? "请填写以下信息后点击【确认】继续执行流程。"
                : renderedPrompt;

            // ── 根据提示文字动态计算 pnlPrompt 高度 ──────
            // 最小高度 44（单行），文字宽度按 pnlPrompt 内容区宽度（460 - padding 20）
            int promptInnerWidth = pnlPrompt.Width - pnlPrompt.Padding.Horizontal - 4;
            using var g = lblPrompt.CreateGraphics();
            var textSize = g.MeasureString(
                lblPrompt.Text,
                lblPrompt.Font,
                promptInnerWidth,
                StringFormat.GenericDefault);
            int promptHeight = Math.Max(44, (int)Math.Ceiling(textSize.Height) + 20);
            pnlPrompt.Size = new Size(pnlPrompt.Width, promptHeight);

            // ── 输入控件 Y 坐标 = pnlPrompt 底部 + 10 ────
            int inputY = pnlPrompt.Bottom + 10;
            txtTextInput.Location = new Point(txtTextInput.Location.X, inputY);
            nudNumInput.Location = new Point(nudNumInput.Location.X, inputY);
            lblUnit.Location = new Point(lblUnit.Location.X, inputY);
            cmbSelectInput.Location = new Point(cmbSelectInput.Location.X, inputY);

            // ── 倒计时标签 Y = 输入控件底部 + 6 ──────────
            lblCountdown.Location = new Point(lblCountdown.Location.X, inputY + 36 + 6);

            // ── 窗体总高度 = 标题栏(35) + pnlPrompt(Y10+高) + 输入控件(36) + 间距 + 底部(60)
            int contentHeight = 10 + promptHeight + 10 + 36 + 14 + 10;
            int formHeight = 35 + contentHeight + 60;
            ClientSize = new Size(ClientSize.Width, formHeight);

            // ── 根据类型初始化输入控件 ────────────────────
            switch (_param.InputType)
            {
                case UserInputType.Number:
                    SetupNumberInput();
                    break;

                case UserInputType.Select:
                    SetupSelectInput();
                    break;

                default:
                    SetupTextInput();
                    break;
            }

            // ── 超时倒计时 ────────────────────────────────
            if (_param.TimeoutSeconds > 0)
            {
                _remainingSeconds = _param.TimeoutSeconds;
                lblCountdown.Visible = true;
                UpdateCountdownLabel();
                timerCountdown.Start();
            }
        }

        private void SetupTextInput()
        {
            txtTextInput.Visible = true;
            nudNumInput.Visible = false;
            cmbSelectInput.Visible = false;
            lblUnit.Visible = false;

            // 设置默认值
            txtTextInput.Text = ResolveDefaultValue();
            txtTextInput.Focus();
            txtTextInput.SelectAll();
        }

        private void SetupNumberInput()
        {
            txtTextInput.Visible = false;
            nudNumInput.Visible = true;
            cmbSelectInput.Visible = false;
            lblUnit.Visible = true;

            // 设置范围
            if (_param.MinValue.HasValue) nudNumInput.Minimum = _param.MinValue.Value;
            if (_param.MaxValue.HasValue) nudNumInput.Maximum = _param.MaxValue.Value;
            nudNumInput.DecimalPlaces = _param.DecimalPlaces;

            // 设置默认值
            var defStr = ResolveDefaultValue();
            if (double.TryParse(defStr, out double defVal))
                nudNumInput.Value = defVal;
            else if (_param.MinValue.HasValue)
                nudNumInput.Value = _param.MinValue.Value;

            // 范围提示
            if (_param.MinValue.HasValue || _param.MaxValue.HasValue)
            {
                var minStr = _param.MinValue.HasValue ? _param.MinValue.Value.ToString() : "-∞";
                var maxStr = _param.MaxValue.HasValue ? _param.MaxValue.Value.ToString() : "+∞";
                lblUnit.Text = $"范围：{minStr} ~ {maxStr}";
            }

            nudNumInput.Focus();
        }

        private void SetupSelectInput()
        {
            txtTextInput.Visible = false;
            nudNumInput.Visible = false;
            cmbSelectInput.Visible = true;
            lblUnit.Visible = false;

            cmbSelectInput.Items.Clear();
            if (!string.IsNullOrWhiteSpace(_param.SelectOptions))
            {
                var options = _param.SelectOptions.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var opt in options)
                    cmbSelectInput.Items.Add(opt.Trim());
            }

            // 设置默认选中项
            var defVal = ResolveDefaultValue();
            if (!string.IsNullOrEmpty(defVal))
            {
                int defIdx = cmbSelectInput.Items.IndexOf(defVal);
                cmbSelectInput.SelectedIndex = defIdx >= 0 ? defIdx : 0;
            }
            else if (cmbSelectInput.Items.Count > 0)
            {
                cmbSelectInput.SelectedIndex = 0;
            }

            cmbSelectInput.Focus();
        }

        /// <summary>
        /// 解析默认值（目前直接返回字符串，后续可扩展变量解析）
        /// </summary>
        private string ResolveDefaultValue() => _param.DefaultValue ?? "";

        #endregion

        #region 倒计时

        private void TimerCountdown_Tick(object sender, EventArgs e)
        {
            _remainingSeconds--;

            if (_remainingSeconds <= 0)
            {
                timerCountdown.Stop();
                HandleTimeout();
            }
            else
            {
                UpdateCountdownLabel();
            }
        }

        private void UpdateCountdownLabel()
        {
            lblCountdown.Text = $"⏱  将在 {_remainingSeconds} 秒后自动处理（{GetTimeoutActionDesc()}）";
        }

        private string GetTimeoutActionDesc() =>
            _param.OnTimeout switch
            {
                InputTimeoutAction.UseDefaultValue => "使用默认值继续",
                InputTimeoutAction.SkipStep => "跳过此步骤",
                _ => "停止流程"
            };

        private void HandleTimeout()
        {
            switch (_param.OnTimeout)
            {
                case InputTimeoutAction.UseDefaultValue:
                    InputValue = _param.TimeoutDefaultValue ?? _param.DefaultValue ?? "";
                    DialogResult = DialogResult.OK;
                    break;

                case InputTimeoutAction.SkipStep:
                    // SkipStep 由 Method 层判断 DialogResult == Ignore 来处理
                    DialogResult = DialogResult.Ignore;
                    break;

                default:
                    // StopProcedure：返回 Cancel，Method 层判断为失败
                    DialogResult = DialogResult.Cancel;
                    break;
            }

            Close();
        }

        #endregion

        #region 按钮事件

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            timerCountdown.Stop();

            var value = GetCurrentInputValue();

            // 空值校验
            if (!_param.AllowEmpty && string.IsNullOrWhiteSpace(value))
            {
                MessageHelper.MessageOK(this, "输入值不能为空，请填写后再确认。",
                    AntdUI.TType.Warn);
                FocusInputControl();
                return;
            }

            InputValue = value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            timerCountdown.Stop();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 文本框按 Enter 键触发确认
        /// </summary>
        private void TxtTextInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnConfirm_Click(sender, EventArgs.Empty);
            }
        }

        #endregion

        #region 辅助

        private string GetCurrentInputValue() =>
            _param.InputType switch
            {
                UserInputType.Number => nudNumInput.Value.ToString(),
                UserInputType.Select => cmbSelectInput.Text,
                _ => txtTextInput.Text.Trim()
            };

        private void FocusInputControl()
        {
            switch (_param.InputType)
            {
                case UserInputType.Number: nudNumInput.Focus(); break;
                case UserInputType.Select: cmbSelectInput.Focus(); break;
                default: txtTextInput.Focus(); break;
            }
        }

        #endregion
    }
}
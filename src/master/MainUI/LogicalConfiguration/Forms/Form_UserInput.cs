using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 用户输入步骤 - 配置窗体（设计时使用）
    /// 让工程师设置运行时弹窗的标题、提示语、输入类型、目标变量等参数
    /// </summary>
    public partial class Form_UserInput : UIForm
    {
        #region 字段

        private readonly IWorkflowStateService _workflowStateService;
        private readonly ILogger<Form_UserInput> _logger;
        private bool _isInitializing = true;

        // 描述行基础 Y（不展开超时使用值时）
        private const int DESC_Y_BASE     = 458;
        // 描述行偏移 Y（展开超时使用值时）
        private const int DESC_Y_EXPANDED = 498;

        #endregion

        #region 构造函数

        public Form_UserInput(
            IWorkflowStateService workflowStateService,
            ILogger<Form_UserInput> logger)
        {
            _workflowStateService = workflowStateService
                ?? throw new ArgumentNullException(nameof(workflowStateService));
            _logger = logger;

            InitializeComponent();
            InitForm();
        }

        #endregion

        #region 初始化

        private void InitForm()
        {
            try
            {
                _isInitializing = true;

                // 下拉框默认值
                cmbInputType.SelectedIndex  = 0;   // 文本输入
                cmbOnTimeout.SelectedIndex  = 0;   // 停止流程

                // 附加变量选择面板到目标变量输入框
                AttachExpressionPanels();

                // 加载已有参数（编辑模式）
                LoadExistingParameter();

                // 根据初始状态刷新联动控件
                UpdateInputTypeControls();
                UpdateTimeoutActionControls();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化用户输入配置窗体失败");
            }
            finally
            {
                _isInitializing = false;
            }
        }

        private void AttachExpressionPanels()
        {
            try
            {
                ExpressionInputPanel.AttachTo(txtTargetVar, new InputPanelOptions
                {
                    Mode           = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title          = "选择目标变量",
                    ShowValidation = false,
                    ShowPreview    = false,
                    CloseOnSubmit  = true
                });
                txtTargetVar.Watermark = "点击选择目标变量 (按F2打开面板)";
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "附加表达式输入面板失败");
            }
        }

        private void LoadExistingParameter()
        {
            try
            {
                var steps = _workflowStateService.GetSteps();
                int idx   = _workflowStateService.StepNum;
                if (steps == null || idx < 0 || idx >= steps.Count) return;

                var paramObj = steps[idx].StepParameter;
                Parameter_UserInput param = null;

                if (paramObj is Parameter_UserInput p)
                    param = p;
                else if (paramObj is not null)
                {
                    try { param = JObject.Parse(paramObj.ToString()).ToObject<Parameter_UserInput>(); }
                    catch (Exception ex) { _logger?.LogWarning(ex, "解析参数失败，使用默认值"); }
                }

                if (param != null)
                    LoadParameterToForm(param);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载已有参数失败");
            }
        }

        private void LoadParameterToForm(Parameter_UserInput param)
        {
            txtTitle.Text        = param.Title        ?? "请输入";
            txtPrompt.Text       = param.Prompt       ?? "";
            txtTargetVar.Text    = string.IsNullOrEmpty(param.TargetVariableName)
                                    ? "" : $"{{{param.TargetVariableName}}}";
            txtDefaultValue.Text  = param.DefaultValue ?? "";
            txtDescription.Text   = param.Description  ?? "";
            txtSelectOptions.Text = param.SelectOptions ?? "";
            txtMinValue.Text      = param.MinValue.HasValue ? param.MinValue.Value.ToString() : "";
            txtMaxValue.Text      = param.MaxValue.HasValue ? param.MaxValue.Value.ToString() : "";
            nudDecimalPlaces.Value = param.DecimalPlaces;
            chkAllowEmpty.Checked  = param.AllowEmpty;
            nudTimeout.Value       = param.TimeoutSeconds;
            txtTimeoutDefault.Text = param.TimeoutDefaultValue ?? "";

            cmbInputType.SelectedIndex = param.InputType switch
            {
                UserInputType.Number => 1,
                UserInputType.Select => 2,
                _                   => 0
            };

            cmbOnTimeout.SelectedIndex = param.OnTimeout switch
            {
                InputTimeoutAction.UseDefaultValue => 1,
                InputTimeoutAction.SkipStep        => 2,
                _                                  => 0
            };
        }

        #endregion

        #region 界面联动

        private void CmbInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            UpdateInputTypeControls();
        }

        private void CmbOnTimeout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            UpdateTimeoutActionControls();
        }

        /// <summary>
        /// 根据输入类型显示/隐藏专属控件
        /// </summary>
        private void UpdateInputTypeControls()
        {
            var type = GetSelectedInputType();
            bool isNumber = type == UserInputType.Number;
            bool isSelect = type == UserInputType.Select;

            // 数值专属
            lblNumRange.Visible       = isNumber;
            txtMinValue.Visible       = isNumber;
            lblRangeSep.Visible       = isNumber;
            txtMaxValue.Visible       = isNumber;
            lblDecimal.Visible        = isNumber;
            nudDecimalPlaces.Visible  = isNumber;

            // 下拉专属
            lblOptions.Visible        = isSelect;
            txtSelectOptions.Visible  = isSelect;

            // 默认值标签文字
            lblDefault.Text = isSelect ? "默认选项:" : "默认值:";
        }

        /// <summary>
        /// 根据超时动作显示/隐藏"超时使用值"行，并调整描述行 Y 坐标
        /// </summary>
        private void UpdateTimeoutActionControls()
        {
            bool showDefRow = cmbOnTimeout.SelectedIndex == 1; // UseDefaultValue
            lblTimeoutDef.Visible     = showDefRow;
            txtTimeoutDefault.Visible = showDefRow;

            // 描述行跟随超时展开状态上下移动
            int descY = showDefRow ? DESC_Y_EXPANDED : DESC_Y_BASE;
            lblDescription.Location  = new Point(lblDescription.Location.X,  descY + 2);
            txtDescription.Location  = new Point(txtDescription.Location.X,  descY);
        }

        #endregion

        #region 验证

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTargetVar.Text))
            {
                MessageHelper.MessageOK(this, "请选择或填写「存入变量」，用于存储操作员填写的值", TType.Warn);
                txtTargetVar.Focus();
                return false;
            }

            if (GetSelectedInputType() == UserInputType.Select
                && string.IsNullOrWhiteSpace(txtSelectOptions.Text))
            {
                MessageHelper.MessageOK(this, "「下拉选择」模式下，必须填写「选项列表」（用分号分隔）", TType.Warn);
                txtSelectOptions.Focus();
                return false;
            }

            if (GetSelectedInputType() == UserInputType.Number)
            {
                bool hasMin = double.TryParse(txtMinValue.Text, out double min);
                bool hasMax = double.TryParse(txtMaxValue.Text, out double max);
                if (hasMin && hasMax && min > max)
                {
                    MessageHelper.MessageOK(this, "最小值不能大于最大值", TType.Warn);
                    txtMinValue.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 保存 / 取消

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                if (!ValidateInput()) return;

                var param = BuildParameter();
                var steps = _workflowStateService.GetSteps();
                int idx   = _workflowStateService.StepNum;

                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    steps[idx].StepParameter = param;
                    _logger?.LogInformation(
                        "用户输入参数已保存: InputType={Type}, TargetVar={Var}",
                        param.InputType, param.TargetVariableName);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存用户输入参数失败");
                MessageHelper.MessageOK(this, $"保存失败：{ex.Message}", TType.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region 辅助

        private Parameter_UserInput BuildParameter()
        {
            // 规范化变量名（去掉花括号）
            var raw     = txtTargetVar.Text.Trim();
            var varName = raw.StartsWith("{") && raw.EndsWith("}") ? raw[1..^1] : raw;

            return new Parameter_UserInput
            {
                Title               = txtTitle.Text.Trim(),
                Prompt              = txtPrompt.Text.Trim(),
                InputType           = GetSelectedInputType(),
                TargetVariableName  = varName,
                DefaultValue        = txtDefaultValue.Text.Trim(),
                SelectOptions       = txtSelectOptions.Text.Trim(),
                MinValue            = double.TryParse(txtMinValue.Text, out var mn) ? mn : null,
                MaxValue            = double.TryParse(txtMaxValue.Text, out var mx) ? mx : null,
                DecimalPlaces       = nudDecimalPlaces.Value,
                AllowEmpty          = chkAllowEmpty.Checked,
                TimeoutSeconds      = nudTimeout.Value,
                OnTimeout           = GetSelectedTimeoutAction(),
                TimeoutDefaultValue = txtTimeoutDefault.Text.Trim(),
                Description         = txtDescription.Text.Trim()
            };
        }

        private UserInputType GetSelectedInputType() =>
            cmbInputType.SelectedIndex switch
            {
                1 => UserInputType.Number,
                2 => UserInputType.Select,
                _ => UserInputType.Text
            };

        private InputTimeoutAction GetSelectedTimeoutAction() =>
            cmbOnTimeout.SelectedIndex switch
            {
                1 => InputTimeoutAction.UseDefaultValue,
                2 => InputTimeoutAction.SkipStep,
                _ => InputTimeoutAction.StopProcedure
            };

        #endregion
    }
}

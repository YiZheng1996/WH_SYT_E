using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 用户输入步骤 - 参数配置窗体
    /// </summary>
    public partial class Form_UserInput : BaseParameterForm
    {
        #region 字段

        private Parameter_UserInput _parameter;
        private bool _isInitializing = true;

        // 描述行动态 Y（根据超时使用值行是否展开而变化）
        private const int DESC_Y_BASE = 496;
        private const int DESC_Y_EXPANDED = 536;

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象 —— 基类通过反射访问此属性完成加载/保存
        /// </summary>
        public Parameter_UserInput Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_UserInput();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                    LoadParameterToForm();
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 设计器构造函数
        /// </summary>
        public Form_UserInput()
        {
            InitializeComponent();
            if (!DesignMode)
                InitializeForm();
        }

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        public Form_UserInput(
            IWorkflowStateService workflowState,
            ILogger<Form_UserInput> logger)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        #endregion

        #region 初始化

        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 下拉框默认值
                cmbInputType.SelectedIndex = 0;
                cmbOnTimeout.SelectedIndex = 0;

                // 附加变量选择面板
                AttachExpressionPanels();

                // 根据初始状态刷新联动控件可见性
                UpdateInputTypeControls();
                UpdateTimeoutActionControls();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化用户输入配置窗体失败");
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
                    Mode = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title = "选择目标变量",
                    ShowValidation = false,
                    ShowPreview = false,
                    CloseOnSubmit = true
                });
                txtTargetVar.Watermark = "点击选择目标变量 (按F2打开面板)";
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "附加表达式输入面板失败");
            }
        }

        #endregion

        #region 重写 BaseParameterForm 虚方法

        /// <summary>
        /// 基类 OnLoad 会调用 LoadParametersFromWorkflow，
        /// 后者调用 ConvertParameter 转换参数类型，再调用此方法填充控件
        /// </summary>
        protected override void LoadParameterToForm()
        {
            if (_parameter == null || _isInitializing) return;

            try
            {
                _isInitializing = true;

                txtTitle.Text = _parameter.Title ?? "请输入";
                txtPrompt.Text = _parameter.Prompt ?? "";
                txtDescription.Text = _parameter.Description ?? "";
                txtDefaultValue.Text = _parameter.DefaultValue ?? "";
                txtSelectOptions.Text = _parameter.SelectOptions ?? "";
                txtMinValue.Text = _parameter.MinValue.HasValue
                                        ? _parameter.MinValue.Value.ToString() : "";
                txtMaxValue.Text = _parameter.MaxValue.HasValue
                                        ? _parameter.MaxValue.Value.ToString() : "";
                nudDecimalPlaces.Value = _parameter.DecimalPlaces;
                chkAllowEmpty.Checked = _parameter.AllowEmpty;
                nudTimeout.Value = _parameter.TimeoutSeconds;
                txtTimeoutDefault.Text = _parameter.TimeoutDefaultValue ?? "";

                // 目标变量——还原花括号用于显示（实际存储不含花括号）
                txtTargetVar.Text = string.IsNullOrEmpty(_parameter.TargetVariableName)
                    ? "" : $"{{{_parameter.TargetVariableName}}}";

                cmbInputType.SelectedIndex = _parameter.InputType switch
                {
                    UserInputType.Number => 1,
                    UserInputType.Select => 2,
                    _ => 0
                };

                cmbOnTimeout.SelectedIndex = _parameter.OnTimeout switch
                {
                    InputTimeoutAction.UseDefaultValue => 1,
                    InputTimeoutAction.SkipStep => 2,
                    _ => 0
                };

                UpdateInputTypeControls();
                UpdateTimeoutActionControls();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数到界面失败");
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 基类 SaveParameters 调用此方法，将控件值写回 _parameter
        /// </summary>
        protected override void SaveFormToParameter()
        {
            _parameter ??= new Parameter_UserInput();

            // 目标变量——去掉花括号后存储（基类 CleanBracketsFromProperties 也会处理 TargetVariableName）
            var raw = txtTargetVar.Text.Trim();
            _parameter.TargetVariableName = raw.StartsWith("{") && raw.EndsWith("}")
                ? raw[1..^1] : raw;

            _parameter.Title = txtTitle.Text.Trim();
            _parameter.Prompt = txtPrompt.Text.Trim();
            _parameter.Description = txtDescription.Text.Trim();
            _parameter.InputType = GetSelectedInputType();
            _parameter.DefaultValue = txtDefaultValue.Text.Trim();
            _parameter.SelectOptions = txtSelectOptions.Text.Trim();
            _parameter.MinValue = double.TryParse(txtMinValue.Text, out var mn) ? mn : null;
            _parameter.MaxValue = double.TryParse(txtMaxValue.Text, out var mx) ? mx : null;
            _parameter.DecimalPlaces = nudDecimalPlaces.Value;
            _parameter.AllowEmpty = chkAllowEmpty.Checked;
            _parameter.TimeoutSeconds = nudTimeout.Value;
            _parameter.OnTimeout = GetSelectedTimeoutAction();
            _parameter.TimeoutDefaultValue = txtTimeoutDefault.Text.Trim();
        }

        /// <summary>
        /// 基类在没有已有参数时调用，设置初始默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            _parameter = new Parameter_UserInput();
            LoadParameterToForm();
        }

        /// <summary>
        /// 基类 SaveParameters 调用，验证通过才执行保存
        /// </summary>
        protected override bool ValidateInput()
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

        /// <summary>
        /// 基类通过此方法将 stepParameter 转换为强类型参数
        /// </summary>
        protected override object ConvertParameter(object stepParameter)
        {
            if (stepParameter is Parameter_UserInput p)
                return p;

            try
            {
                var json = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                return JsonConvert.DeserializeObject<Parameter_UserInput>(json)
                       ?? new Parameter_UserInput();
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(ex, "Parameter_UserInput 反序列化失败，使用默认值");
                return new Parameter_UserInput();
            }
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

        private void UpdateInputTypeControls()
        {
            var type = GetSelectedInputType();
            bool isNumber = type == UserInputType.Number;
            bool isSelect = type == UserInputType.Select;

            lblNumRange.Visible = isNumber;
            txtMinValue.Visible = isNumber;
            lblRangeSep.Visible = isNumber;
            txtMaxValue.Visible = isNumber;
            lblDecimal.Visible = isNumber;
            nudDecimalPlaces.Visible = isNumber;

            lblOptions.Visible = isSelect;
            txtSelectOptions.Visible = isSelect;

            lblDefault.Text = isSelect ? "默认选项:" : "默认值:";
        }

        private void UpdateTimeoutActionControls()
        {
            bool showDef = cmbOnTimeout.SelectedIndex == 1; // UseDefaultValue
            lblTimeoutDef.Visible = showDef;
            txtTimeoutDefault.Visible = showDef;

            int descY = showDef ? DESC_Y_EXPANDED : DESC_Y_BASE;
            lblDescription.Location = new Point(lblDescription.Location.X, descY + 2);
            txtDescription.Location = new Point(txtDescription.Location.X, descY);
        }

        #endregion

        #region 按钮事件

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 调用基类统一保存流程：ValidateInput → SaveFormToParameter → CleanBrackets → UpdateStepParameter
            SaveParameters();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region 辅助

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
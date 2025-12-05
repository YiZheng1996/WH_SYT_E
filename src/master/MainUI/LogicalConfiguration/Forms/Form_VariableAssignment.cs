using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 变量赋值工具窗体 - 优化版
    /// 使用 ExpressionEngine 统一处理表达式验证
    /// </summary>
    public partial class Form_VariableAssignment : BaseParameterForm
    {
        #region 属性

        private Parameter_VariableAssignment _parameter;
        public Parameter_VariableAssignment Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_VariableAssignment();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        #endregion

        #region 私有字段
        private bool _isInitializing = true;
        private bool _hasUnsavedChanges = false;

        #endregion

        #region 构造函数

        public Form_VariableAssignment()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                InitializeForm();
            }
        }

        public Form_VariableAssignment(
            IWorkflowStateService workflowState,
            ILogger<Form_VariableAssignment> logger)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                AttachExpressionPanels();
                BindEvents();

                _isInitializing = false;
                Logger?.LogInformation("变量赋值工具窗体加载完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "变量赋值表单初始化失败");
                MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
            }
        }


        private void AttachExpressionPanels()
        {
            try
            {
                // 目标变量
                ExpressionInputPanel.AttachTo(txtTargetVariable, new InputPanelOptions
                {
                    Mode = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title = "选择目标变量",
                    ShowValidation = false,
                    ShowPreview = false,
                    CloseOnSubmit = true
                });
                txtTargetVariable.Watermark = "点击选择目标变量 (按F2打开面板)";

                // 赋值内容
                ExpressionInputPanel.AttachTo(txtAssignmentContent, new InputPanelOptions
                {
                    Mode = InputMode.Expression,
                    EnabledModules = InputModules.All,
                    Title = "配置赋值内容",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true
                });
                txtAssignmentContent.Watermark = "点击配置赋值内容，支持固定值/变量/表达式/PLC (按F2打开面板)";

                Logger?.LogDebug("表达式输入面板附加完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "附加表达式输入面板失败");
            }
        }

        #endregion

        #region 事件绑定

        private void BindEvents()
        {
            try
            {
                txtTargetVariable.TextChanged += (s, e) =>
                {
                    if (!_isInitializing)
                    {
                        _hasUnsavedChanges = true;
                    }
                };

                txtAssignmentContent.TextChanged += (s, e) =>
                {
                    if (!_isInitializing)
                    {
                        _hasUnsavedChanges = true;
                    }
                };

                txtDescription.TextChanged += (s, e) =>
                {
                    if (!_isInitializing) _hasUnsavedChanges = true;
                };

                chkEnabled.CheckedChanged += (s, e) =>
                {
                    if (!_isInitializing) _hasUnsavedChanges = true;
                };

                btnOK.Click += BtnOK_Click;
                btnCancel.Click += BtnCancel_Click;
                btnHelp.Click += BtnHelp_Click;
                this.FormClosing += Form_VariableAssignment_FormClosing;

                Logger?.LogDebug("事件绑定完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "绑定事件时发生错误");
            }
        }

        #endregion

        #region 参数处理

        protected override void SetDefaultValues()
        {
            _parameter = new Parameter_VariableAssignment
            {
                TargetVarName = "",
                Expression = "",
                Condition = "",
                Description = $"变量赋值步骤 {_workflowState?.StepNum + 1}",
                IsAssignment = true,
                AssignmentType = VariableAssignmentType.ExpressionCalculation,
                DataSource = new DataSourceConfig()
            };

            Logger?.LogDebug("设置变量赋值参数默认值");
            LoadParameterToForm();
        }

        protected override void LoadParameterToForm()
        {
            try
            {
                if (_parameter == null)
                {
                    SetDefaultValues();
                    return;
                }

                // 目标变量：原样显示，不加花括号
                txtTargetVariable.Text = _parameter.TargetVarName ?? "";

                // 根据赋值类型加载内容
                string displayContent = "";
                if (_parameter.AssignmentType == VariableAssignmentType.PLCRead)
                {
                    // PLC模式: 显示为 {PLC.模块名.地址}
                    displayContent = $"{{PLC.{_parameter.DataSource.PlcConfig.ModuleName}.{_parameter.DataSource.PlcConfig.Address}}}";
                }
                else
                {
                    // 其他模式: 原样显示，不处理花括号
                    displayContent = _parameter.Expression ?? "";
                }

                txtAssignmentContent.Text = displayContent;
                txtDescription.Text = _parameter.Description ?? "";
                chkEnabled.Checked = _parameter.IsAssignment;

                Logger?.LogDebug($"参数加载完成: 表达式={displayContent}");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数到界面失败");
            }
        }

        protected override void SaveFormToParameter()
        {
            try
            {
                _parameter ??= new Parameter_VariableAssignment();
                _parameter.DataSource ??= new DataSourceConfig();

                // 目标变量名：直接使用，不处理花括号
                _parameter.TargetVarName = txtTargetVariable?.Text?.Trim() ?? "";

                // 赋值内容：原样保存，不处理花括号
                string expression = txtAssignmentContent?.Text?.Trim() ?? "";

                // 识别PLC格式: PLC.模块名.地址 (可能带花括号)
                // 移除可能的花括号再判断
                string cleanExpression = expression.Trim('{', '}');
                if (cleanExpression.StartsWith("PLC.", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = cleanExpression[4..].Split('.');
                    if (parts.Length == 2)
                    {
                        _parameter.AssignmentType = VariableAssignmentType.PLCRead;
                        _parameter.DataSource.PlcConfig.ModuleName = parts[0];
                        _parameter.DataSource.PlcConfig.Address = parts[1];
                        _parameter.Expression = ""; // PLC模式不保存到Expression

                        Logger?.LogDebug($"PLC模式: {parts[0]}.{parts[1]}");
                    }
                    else
                    {
                        // PLC格式不正确，当作表达式处理
                        _parameter.AssignmentType = VariableAssignmentType.ExpressionCalculation;
                        _parameter.Expression = expression; // 原样保存
                    }
                }
                else
                {
                    // 普通表达式：原样保存
                    _parameter.AssignmentType = VariableAssignmentType.ExpressionCalculation;
                    _parameter.Expression = expression; // 原样保存，包含花括号
                }

                _parameter.Condition = "";
                _parameter.Description = txtDescription?.Text ?? "";
                _parameter.IsAssignment = chkEnabled?.Checked ?? true;

                Logger?.LogDebug($"参数保存完成: 表达式={_parameter.Expression}");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存参数失败");
            }
        }

        #endregion

        #region 验证逻辑

        protected override bool ValidateInput()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTargetVariable?.Text))
                {
                    MessageHelper.MessageOK("请选择目标变量！", TType.Warn);
                    txtTargetVariable?.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtAssignmentContent?.Text))
                {
                    MessageHelper.MessageOK("请配置赋值内容！", TType.Warn);
                    txtAssignmentContent?.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入时发生异常");
                MessageHelper.MessageOK($"验证失败：{ex.Message}", TType.Error);
                return false;
            }
        }
        #endregion

        #region 按钮事件

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                btnOK.Enabled = false;
                btnOK.Text = "处理中...";

                if (!ValidateInput())
                    return;

                SaveFormToParameter();

                SaveParameters();
                _hasUnsavedChanges = false;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "确定按钮处理时发生错误");
                MessageHelper.MessageOK($"操作失败：{ex.Message}", TType.Error);
            }
            finally
            {
                btnOK.Enabled = true;
                btnOK.Text = "确定";
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                var helpText = @"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📖 变量赋值 - 使用说明
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

🔹 目标变量
   点击输入框选择要赋值的变量

🔹 赋值内容
   点击输入框弹出智能输入面板，支持：
   • 固定值：123 或 ""文本""
   • 变量：{变量名}
   • 表达式：{Var1} + {Var2} * 2
   • PLC点位：从PLC读取值

🔹 快捷键
   • F2 或 Ctrl+Space：打开输入面板
   • Enter：提交并关闭
   • Escape：取消

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

💡 提示
   • 面板支持实时预览和验证
   • 变量使用 {变量名} 格式
   • 测试按钮会自动创建不存在的变量

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

                MessageHelper.MessageOK(this, helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助时发生错误");
            }
        }

        #endregion

        #region 窗体生命周期

        private void Form_VariableAssignment_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ExpressionInputPanel.CloseActivePanel();

                if (_hasUnsavedChanges && DialogResult != DialogResult.OK)
                {
                    Logger?.LogDebug("窗体关闭时存在未保存的更改");
                }

                Logger?.LogDebug("变量赋值工具窗体正在关闭");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "窗体关闭事件处理时发生错误");
            }
        }

        #endregion
    }
}
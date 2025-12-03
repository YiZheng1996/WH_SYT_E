using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 循环参数配置表单 - 已集成通用表达式输入面板
    /// 用于配置和管理工作流步骤中的循环操作
    /// </summary>
    public partial class Form_Loop : BaseParameterForm
    {
        #region 属性

        private Parameter_Loop _parameter;
        /// <summary>
        /// 参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_Loop Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_Loop();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }
        #endregion

        #region 私有字段

        /// <summary>
        /// 初始化状态标志
        /// </summary>
        private bool _isInitializing = true;

        /// <summary>
        /// 未保存更改标志
        /// </summary>
        private bool _hasUnsavedChanges = false;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数 - 主要用于设计器
        /// </summary>
        public Form_Loop()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                InitializeForm();
            }
        }

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        public Form_Loop(
            IWorkflowStateService workflowState,
            ILogger<Form_Loop> logger)
        {
            InitializeComponent();
            InitializeForm();

            Logger?.LogDebug("Form_Loop 依赖注入构造函数初始化完成");
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 设置表达式输入面板
                SetupExpressionInputPanels();

                // 绑定事件
                BindEvents();

                // 从工作流状态加载参数
                LoadParameterFromWorkflowState();

                // 更新控件状态
                UpdateControlStates();

                Logger?.LogInformation("Form_Loop 初始化完成，已集成表达式输入面板");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化窗体失败");
                MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// ⭐ 设置表达式输入面板 - 核心集成代码
        /// </summary>
        private void SetupExpressionInputPanels()
        {
            try
            {
               
                // 循环次数输入框 - 支持数值、变量、PLC
                ExpressionInputPanel.AttachTo(txtLoopCount, new InputPanelOptions
                {
                    Mode = InputMode.Expression,
                    EnabledModules = InputModules.Variable | InputModules.PLC | InputModules.Constant,
                    Title = "循环次数",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true
                });

                // 添加视觉提示
                txtLoopCount.Watermark = "点击输入循环次数，支持数值/变量/PLC (按F2打开面板)";

                // 退出条件输入框 - 支持条件表达式
                ExpressionInputPanel.AttachTo(txtExitCondition, new InputPanelOptions
                {
                    Mode = InputMode.Condition,  // 条件模式
                    EnabledModules = InputModules.Variable | InputModules.PLC |
                                     InputModules.Expression | InputModules.Constant,
                    Title = "退出条件表达式",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true
                });

                // 添加视觉提示
                txtExitCondition.Watermark = "点击输入退出条件，如：{压力值} >= 6.0 (按F2打开面板)";

               
                // 计数器变量名输入框 - 仅支持变量选择
                ExpressionInputPanel.AttachTo(txtCounterVariable, new InputPanelOptions
                {
                    Mode = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title = "选择计数器变量",
                    ShowValidation = false,
                    CloseOnSubmit = true
                });

                txtCounterVariable.Watermark = "点击选择计数器变量名";

                Logger?.LogDebug("表达式输入面板设置完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "设置表达式输入面板失败");
            }
        }

        /// <summary>
        /// 绑定事件处理器
        /// </summary>
        private void BindEvents()
        {
            try
            {
                // 文本框改变事件 - 标记未保存
                txtLoopCount.TextChanged += (s, e) => MarkAsChanged();
                txtCounterVariable.TextChanged += (s, e) => MarkAsChanged();
                txtDescription.TextChanged += (s, e) => MarkAsChanged();
                txtExitCondition.TextChanged += (s, e) => MarkAsChanged();

                // 复选框改变事件
                chkEnableCounter.CheckedChanged += (s, e) =>
                {
                    MarkAsChanged();
                    UpdateControlStates();
                };

                chkEnableEarlyExit.CheckedChanged += (s, e) =>
                {
                    MarkAsChanged();
                    UpdateControlStates();
                };

                chkEnabled.CheckedChanged += (s, e) => MarkAsChanged();

                // 按钮点击事件
                btnSave.Click += BtnSave_Click;
                btnCancel.Click += BtnCancel_Click;
                btnHelp.Click += BtnHelp_Click;
                btnConfigChildSteps.Click += BtnConfigChildSteps_Click;

                // 窗体关闭事件
                this.FormClosing += Form_Loop_FormClosing;

                Logger?.LogDebug("事件绑定完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "绑定事件失败");
            }
        }

        /// <summary>
        /// 标记为已更改
        /// </summary>
        private void MarkAsChanged()
        {
            if (!_isInitializing)
            {
                _hasUnsavedChanges = true;
            }
        }

        #endregion

        #region 参数处理

        /// <summary>
        /// 从工作流状态加载参数
        /// </summary>
        private void LoadParameterFromWorkflowState()
        {
            if (!IsServiceAvailable) return;

            try
            {
                var currentStep = GetCurrentStepSafely();
                if (currentStep != null && currentStep.StepParameter != null)
                {
                    var parameter = ConvertParameter(currentStep.StepParameter);
                    Parameter = (Parameter_Loop)parameter;
                }
                else
                {
                    SetDefaultValues();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "从工作流状态加载参数失败");
                SetDefaultValues();
            }
        }

        #endregion

        #region UI更新

        /// <summary>
        /// 更新控件状态
        /// </summary>
        private void UpdateControlStates()
        {
            if (_isInitializing) return;

            try
            {
                // 计数器控件状态
                bool counterEnabled = chkEnableCounter.Checked;
                txtCounterVariable.Enabled = counterEnabled;
                lblCounterVariable.Enabled = counterEnabled;

                // 提前退出控件状态
                bool exitEnabled = chkEnableEarlyExit.Checked;
                lblExitCondition.Enabled = exitEnabled;
                txtExitCondition.Enabled = exitEnabled;
                lblExitConditionHint.Enabled = exitEnabled;

                // 确保文本框始终可见
                txtLoopCount.Visible = true;
                txtExitCondition.Visible = true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "更新控件状态失败");
            }
        }

        #endregion

        #region 事件处理器

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                {
                    return;
                }

                SaveFormToParameter();
                SaveParameters();

                _hasUnsavedChanges = false;
                Logger?.LogInformation("循环配置保存成功");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存循环配置失败");
                MessageHelper.MessageOK($"保存失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "有未保存的更改，确定要放弃吗？",
                    "确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 帮助按钮点击事件
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                var helpText = @"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📖 循环配置 - 使用说明
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

🔹 循环次数配置
   点击输入框会弹出智能输入面板，支持：
   • 固定数值：直接输入数字，如 10
   • 变量引用：选择变量，如 {MaxCount}
   • PLC点位：选择PLC地址读取值
   • 表达式：如 {总数} / 2

🔹 计数器变量
   • 启用后，每次循环会自动更新计数器
   • 可在循环体内使用 {LoopIndex} 获取当前次数
   • 计数从 0 开始

🔹 提前退出条件
   点击输入框会弹出智能输入面板，支持：
   • 条件表达式：{压力值} >= 6.0
   • 复合条件：{压力} > 5 AND {温度} < 80
   • PLC条件：{模块.地址} == true

🔹 快捷键
   • F2 或 Ctrl+Space：打开输入面板
   • Enter：提交并关闭面板
   • Escape：取消并关闭面板

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

💡 提示
   • 变量使用 {变量名} 格式引用
   • 支持的运算符：==, !=, >, <, >=, <=
   • 支持逻辑运算：AND, OR, NOT

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

                MessageHelper.MessageOK(helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助信息失败");
            }
        }

        /// <summary>
        /// 配置子步骤按钮点击事件
        /// </summary>
        private void BtnConfigChildSteps_Click(object sender, EventArgs e)
        {
            try
            {
                // 保存当前界面数据到参数对象
                SaveFormToParameter();

                var configForm = new Form_ChildStepsConfig(Parameter.ChildSteps);
                if (configForm.ShowDialog(this) == DialogResult.OK)
                {
                    // 直接使用Form返回的结果更新Parameter
                    Parameter.ChildSteps = configForm._childSteps;

                    // 更新子步骤数量显示
                    int childStepCount = Parameter.ChildSteps?.Count ?? 0;
                    lblChildStepsCount.Text = $"循环体步骤 ({childStepCount} 个)";

                    _hasUnsavedChanges = true;

                    Logger?.LogDebug("子步骤配置完成，数量: {Count}", childStepCount);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "配置子步骤失败");
                MessageHelper.MessageOK($"配置子步骤失败：{ex.Message}", TType.Error);
            }
        }


        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_Loop_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            // 关闭时清理表达式输入面板
            ExpressionInputPanel.CloseActivePanel();
        }

        #endregion

        #region 验证和保存

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInputs()
        {
            try
            {
                // 验证循环次数
                if (string.IsNullOrWhiteSpace(txtLoopCount.Text))
                {
                    MessageHelper.MessageOK("请输入循环次数！", TType.Warn);
                    txtLoopCount.Focus();
                    return false;
                }

                // 验证计数器变量名
                if (chkEnableCounter.Checked && string.IsNullOrWhiteSpace(txtCounterVariable.Text))
                {
                    MessageHelper.MessageOK("启用计数器时必须指定计数器变量名！", TType.Warn);
                    txtCounterVariable.Focus();
                    return false;
                }

                // 验证退出条件
                if (chkEnableEarlyExit.Checked && string.IsNullOrWhiteSpace(txtExitCondition.Text))
                {
                    MessageHelper.MessageOK("启用提前退出时必须输入退出条件！", TType.Warn);
                    txtExitCondition.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入失败");
                MessageHelper.MessageOK($"验证失败：{ex.Message}", TType.Error);
                return false;
            }
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            Parameter = new Parameter_Loop
            {
                LoopCountExpression = "10",
                CounterVariableName = "LoopIndex",
                EnableCounter = true,
                EnableEarlyExit = false,
                ExitConditionExpression = "",
                ChildSteps = [],
                Description = $"循环步骤 {_workflowState?.StepNum + 1}"
            };

            Logger?.LogDebug("设置循环参数默认值");
            LoadParameterToForm();
        }

        /// <summary>
        /// 从参数对象加载到窗体控件
        /// </summary>
        protected override void LoadParameterToForm()
        {
            try
            {
                _isInitializing = true;

                if (Parameter == null) return;

                // 加载基本信息
                txtDescription.Text = Parameter.Description ?? "";
                chkEnabled.Checked = true; // 默认启用

                // ⭐ 简化：直接加载循环次数表达式
                txtLoopCount.Text = Parameter.LoopCountExpression ?? "10";

                // 加载计数器配置
                chkEnableCounter.Checked = Parameter.EnableCounter;
                txtCounterVariable.Text = Parameter.CounterVariableName ?? "LoopIndex";

                // ⭐ 简化：直接加载退出条件表达式
                chkEnableEarlyExit.Checked = Parameter.EnableEarlyExit;
                txtExitCondition.Text = Parameter.ExitConditionExpression ?? "";

                // 更新子步骤数量显示
                int childStepCount = Parameter.ChildSteps?.Count ?? 0;
                lblChildStepsCount.Text = $"循环体步骤 ({childStepCount} 个)";

                // 更新控件状态
                UpdateControlStates();

                Logger?.LogDebug("参数加载完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数到窗体失败");
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 从窗体控件保存到参数对象
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                if (Parameter == null) return;

                // 保存基本信息
                Parameter.Description = txtDescription.Text;

                // ⭐ 简化：直接保存循环次数表达式
                Parameter.LoopCountExpression = txtLoopCount.Text;

                // 保存计数器配置
                Parameter.EnableCounter = chkEnableCounter.Checked;
                Parameter.CounterVariableName = txtCounterVariable.Text;

                // ⭐ 简化：直接保存退出条件表达式
                Parameter.EnableEarlyExit = chkEnableEarlyExit.Checked;
                Parameter.ExitConditionExpression = txtExitCondition.Text;

                Logger?.LogDebug("参数保存完成: LoopCount={LoopCount}, ExitCondition={ExitCondition}",
                    Parameter.LoopCountExpression, Parameter.ExitConditionExpression);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存参数失败");
                throw;
            }
        }

        #endregion
    }
}
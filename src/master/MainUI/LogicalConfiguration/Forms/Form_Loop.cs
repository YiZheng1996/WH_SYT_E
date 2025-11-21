using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 循环参数配置表单
    /// 用于配置和管理工作流步骤中的循环操作
    /// </summary>
    public partial class Form_Loop : Sunny.UI.UIForm, IParameterForm<Parameter_Loop>
    {
        #region 私有字段

        /// <summary>
        /// 当前参数对象缓存
        /// </summary>
        private Parameter_Loop _parameter;

        /// <summary>
        /// 初始化状态标志
        /// </summary>
        private bool _isInitializing = true;

        /// <summary>
        /// 未保存更改标志
        /// </summary>
        private bool _hasUnsavedChanges = false;

        /// <summary>
        /// 全局变量管理器
        /// </summary>
        private GlobalVariableManager _globalVariable;

        /// <summary>
        /// 工作流状态服务
        /// </summary>
        private readonly IWorkflowStateService _workflowState;

        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly ILogger<Form_Loop> _logger;

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象属性（IParameterForm接口实现）
        /// </summary>
        public Parameter_Loop Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_Loop();

                if (!DesignMode && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        /// <summary>
        /// 日志服务
        /// </summary>
        protected ILogger<Form_Loop> Logger => _logger;

        /// <summary>
        /// 服务是否可用
        /// </summary>
        private bool IsServiceAvailable => _workflowState != null;

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
            _workflowState = workflowState;
            _logger = logger;

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

                // 获取服务实例
                _globalVariable = Program.ServiceProvider?.GetService<GlobalVariableManager>();

                // 加载可用变量
                LoadAvailableVariables();

                // 绑定事件
                BindEvents();

                // 从工作流状态加载参数
                LoadParameterFromWorkflowState();
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
        /// 加载可用变量
        /// </summary>
        private void LoadAvailableVariables()
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (globalVariableManager == null) return;

                var variables = globalVariableManager.GetAllVariables();
                Logger?.LogInformation("成功加载 {Count} 个可用变量", variables.Count);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载可用变量失败");
            }
        }

        /// <summary>
        /// 绑定事件处理器
        /// </summary>
        private void BindEvents()
        {
            try
            {
                // 文本框改变事件
                txtLoopCount.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                txtCounterVariable.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                txtDescription.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };

                // 复选框改变事件
                chkEnableCounter.CheckedChanged += (s, e) =>
                {
                    if (!_isInitializing)
                    {
                        _hasUnsavedChanges = true;
                        UpdateCounterControls();
                    }
                };

                // 按钮点击事件
                btnSave.Click += BtnSave_Click;
                btnCancel.Click += BtnCancel_Click;
                btnHelp.Click += BtnHelp_Click;
                btnSelectVarCount.Click += BtnSelectVarCount_Click;
                btnConfigChildSteps.Click += BtnConfigChildSteps_Click;

                // 窗体关闭事件
                this.FormClosing += Form_Loop_FormClosing;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "绑定事件失败");
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
                if (currentStep != null && currentStep.ChildSteps != null)
                {
                    var parameter = ConvertParameter(currentStep.ChildSteps);
                    Parameter = parameter;
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

        /// <summary>
        /// 获取当前步骤（线程安全）
        /// </summary>
        private Parent GetCurrentStepSafely()
        {
            try
            {
                if (_workflowState == null) return null;

                int stepNum = _workflowState.StepNum;
                var allSteps = _workflowState.GetSteps();

                if (allSteps != null && stepNum >= 0 && stepNum < allSteps.Count)
                {
                    return allSteps[allSteps];
                }

                Logger?.LogWarning("获取当前步骤失败，步骤索引: {StepNum}", stepNum);
                return null;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "获取当前步骤异常");
                return null;
            }
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        private void SetDefaultValues()
        {
            _parameter = new Parameter_Loop
            {
                LoopCountExpression = "10",
                CounterVariableName = "LoopIndex",
                EnableCounter = true,
                ChildSteps = new List<Parent>(),
                Description = $"循环步骤 {_workflowState?.StepNum + 1}"
            };

            Logger?.LogDebug("设置循环参数默认值");
            LoadParameterToForm();
        }

        /// <summary>
        /// 加载参数到界面
        /// </summary>
        private void LoadParameterToForm()
        {
            try
            {
                _isInitializing = true;

                txtLoopCount.Text = _parameter.LoopCountExpression ?? "10";
                txtCounterVariable.Text = _parameter.CounterVariableName ?? "LoopIndex";
                chkEnableCounter.Checked = _parameter.EnableCounter;
                txtDescription.Text = _parameter.Description ?? "";
                chkEnabled.Checked = true; // 默认启用

                // 更新子步骤计数
                lblChildStepsCount.Text = $"循环体步骤 ({_parameter.ChildSteps?.Count ?? 0} 个)";

                // 更新控件状态
                UpdateCounterControls();

                _hasUnsavedChanges = false;
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
        /// 保存界面数据到参数对象
        /// </summary>
        private void SaveFormToParameter()
        {
            _parameter.LoopCountExpression = txtLoopCount.Text?.Trim() ?? "10";
            _parameter.CounterVariableName = txtCounterVariable.Text?.Trim() ?? "LoopIndex";
            _parameter.EnableCounter = chkEnableCounter.Checked;
            _parameter.Description = txtDescription.Text?.Trim() ?? "";
        }

        #endregion

        #region UI更新

        /// <summary>
        /// 更新计数器控件状态
        /// </summary>
        private void UpdateCounterControls()
        {
            bool enabled = chkEnableCounter.Checked;
            txtCounterVariable.Enabled = enabled;
            lblCounterVariable.Enabled = enabled;
        }

        #endregion

        #region 变量选择

        /// <summary>
        /// 选择循环次数变量
        /// </summary>
        private void BtnSelectVarCount_Click(object sender, EventArgs e)
        {
            SelectVariableForTextBox(txtLoopCount);
        }

        /// <summary>
        /// 为文本框选择变量
        /// </summary>
        private void SelectVariableForTextBox(Sunny.UI.UITextBox textBox)
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (globalVariableManager == null)
                {
                    MessageHelper.MessageOK("全局变量管理器不可用", TType.Warn);
                    return;
                }

                var variables = globalVariableManager.GetAllVariables();
                if (variables.Count == 0)
                {
                    MessageHelper.MessageOK("当前没有可用的变量", TType.Info);
                    return;
                }

                // 创建变量选择对话框
                var dialog = new VariableSelectionDialog(variables);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string selectedVar = dialog.SelectedVariable;
                    if (!string.IsNullOrEmpty(selectedVar))
                    {
                        // 在当前光标位置插入变量引用
                        int selectionStart = textBox.SelectionStart;
                        string currentText = textBox.Text ?? "";
                        string varReference = $"{{{selectedVar}}}";

                        textBox.Text = currentText.Insert(selectionStart, varReference);
                        textBox.SelectionStart = selectionStart + varReference.Length;
                        textBox.Focus();

                        _hasUnsavedChanges = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "选择变量失败");
                MessageHelper.MessageOK($"选择变量失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 子步骤配置

        /// <summary>
        /// 配置循环体子步骤
        /// </summary>
        private void BtnConfigChildSteps_Click(object sender, EventArgs e)
        {
            try
            {
                // 这里打开子步骤配置对话框
                // TODO:由于子步骤配置比较复杂，这里简化处理
                // 实际项目中应该有专门的子步骤配置对话框
                MessageHelper.MessageOK($"循环体子步骤配置功能待实现\n当前步骤数: {_parameter.ChildSteps?.Count ?? 0}", TType.Info);
                _hasUnsavedChanges = true;

                // 更新显示
                lblChildStepsCount.Text = $"循环体步骤 ({_parameter.ChildSteps?.Count ?? 0} 个)";
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "配置子步骤失败");
                MessageHelper.MessageOK($"配置子步骤失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证输入
                if (!ValidateInput())
                {
                    return;
                }

                // 获取当前步骤
                var currentStep = GetCurrentStepSafely();
                if (currentStep == null)
                {
                    MessageHelper.MessageOK("当前步骤无效，无法保存数据。", TType.Warn);
                    return;
                }

                // 保存界面数据到参数对象
                SaveFormToParameter();

                // 序列化参数对象并保存到步骤
                currentStep.StepParameter = JsonConvert.SerializeObject(_parameter);

                _hasUnsavedChanges = false;

                Logger?.LogInformation("循环参数保存成功");
                MessageHelper.MessageOK("保存成功！循环配置将在主界面保存时写入配置文件。", TType.Success);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存循环参数失败");
                MessageHelper.MessageOK($"保存失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
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
                string helpText = @"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📖 循环配置 - 使用说明
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

🔹 基本概念
   循环用于重复执行一组步骤指定的次数

🔹 配置说明
   • 循环次数: 设置循环执行的次数
   • 计数器变量: 在子步骤中可使用此变量获取当前循环索引
   • 循环体步骤: 配置每次循环要执行的步骤

🔹 循环次数设置
   • 固定值: 如 10, 100
   • 变量引用: 使用 {变量名} 引用变量值
   • 示例: {MaxRetryCount}

🔹 计数器变量
   • 默认名称: LoopIndex
   • 索引范围: 从 1 开始到循环次数
   • 可在子步骤中使用 {LoopIndex} 引用当前索引
   • 可禁用计数器变量以提升性能

🔹 使用场景
   • 重复执行测试
   • 批量处理数据
   • 失败重试机制
   • 数据采集

⚠️ 注意事项
   1. 循环次数必须大于 0
   2. 避免设置过大的循环次数造成长时间执行
   3. 计数器变量名不能与现有变量重名
   4. 循环内部可使用 Break 和 Continue 控制步骤
   5. 确保循环体内的步骤配置正确

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

                MessageHelper.MessageOK(this, helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助失败");
            }
        }

        #endregion

        #region 验证方法

        /// <summary>
        /// 验证输入数据的有效性
        /// </summary>
        private bool ValidateInput()
        {
            try
            {
                // 收集当前数据
                SaveFormToParameter();

                // 验证循环次数表达式
                if (string.IsNullOrWhiteSpace(_parameter.LoopCountExpression))
                {
                    MessageHelper.MessageOK("请输入循环次数表达式！", TType.Warn);
                    txtLoopCount.Focus();
                    return false;
                }

                // 如果启用计数器，验证计数器变量名
                if (_parameter.EnableCounter)
                {
                    if (string.IsNullOrWhiteSpace(_parameter.CounterVariableName))
                    {
                        MessageHelper.MessageOK("请输入计数器变量名！", TType.Warn);
                        txtCounterVariable.Focus();
                        return false;
                    }

                    // 检查变量名是否合法（只允许字母、数字和下划线）
                    string varName = _parameter.CounterVariableName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(varName, @"^[a-zA-Z_][a-zA-Z0-9_]*$"))
                    {
                        MessageHelper.MessageOK("计数器变量名只能包含字母、数字和下划线，且不能以数字开头！", TType.Warn);
                        txtCounterVariable.Focus();
                        return false;
                    }
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

        #endregion

        #region 窗体事件

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_Loop_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK) return;

            if (_hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes(this, "存在未保存的更改，确定要关闭吗？");
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 填充控件（IParameterForm接口实现）
        /// </summary>
        public void PopulateControls(Parameter_Loop parameter)
        {
            Parameter = parameter;
        }

        /// <summary>
        /// 验证类型化参数（IParameterForm接口实现）
        /// </summary>
        public bool ValidateTypedParameters()
        {
            return ValidateInput();
        }

        /// <summary>
        /// 收集类型化参数（IParameterForm接口实现）
        /// </summary>
        public Parameter_Loop CollectTypedParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        /// <summary>
        /// 转换参数对象（IParameterForm接口实现）
        /// </summary>
        public Parameter_Loop ConvertParameter(object stepParameter)
        {
            if (stepParameter is Parameter_Loop paramObj)
                return paramObj;

            if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Parameter_Loop>(jsonStr)
                        ?? new Parameter_Loop();
                }
                catch (JsonException ex)
                {
                    Logger?.LogWarning(ex, "转换参数失败");
                    return new Parameter_Loop();
                }
            }

            return new Parameter_Loop();
        }

        void IParameterForm<Parameter_Loop>.SetDefaultValues()
        {
            SetDefaultValues();
        }

        #endregion
    }
}
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
                var dialog = new VariableSelectionDialog(globalVariableManager);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string selectedVar = dialog.SelectedVariable.Name;
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
                Debug.WriteLine("========== 开始配置子步骤 ==========");

                // 确保 ChildSteps 列表已初始化
                if (Parameter.ChildSteps == null)
                {
                    Parameter.ChildSteps = [];
                    Logger?.LogDebug("初始化空的子步骤列表");
                }

                // ⭐ 诊断日志1: 配置前的状态
                Debug.WriteLine($"配置前子步骤数量: {Parameter.ChildSteps.Count}");
                if (Parameter.ChildSteps.Count > 0)
                {
                    for (int i = 0; i < Parameter.ChildSteps.Count; i++)
                    {
                        var child = Parameter.ChildSteps[i];
                        Debug.WriteLine($"  [{i}] {child.StepName}: 参数={child.StepParameter}");
                    }
                }

                // 打开子步骤配置窗体
                using var configForm = new Form_ChildStepsConfig(Parameter.ChildSteps);
                var result = configForm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // 获取配置好的子步骤列表
                    var updatedSteps = configForm._childSteps;

                    // 诊断日志2: 配置对话框返回的数据
                    Debug.WriteLine("配置对话框返回:");
                    Debug.WriteLine($"  返回的子步骤数量: {updatedSteps?.Count ?? 0}");
                    if (updatedSteps != null && updatedSteps.Count > 0)
                    {
                        for (int i = 0; i < updatedSteps.Count; i++)
                        {
                            var child = updatedSteps[i];
                            var hasParam = !string.IsNullOrEmpty(child.StepParameter?.ToString());
                            Debug.WriteLine($"    [{i}] {child.StepName}: 参数={hasParam}, 长度={child.StepParameter?.ToString()?.Length ?? 0}");
                        }
                    }

                    // 更新参数对象的子步骤列表
                    Parameter.ChildSteps = updatedSteps;

                    // 诊断日志3: 更新后的状态
                    Debug.WriteLine("更新Parameter.ChildSteps后:");
                    Debug.WriteLine($"  Parameter.ChildSteps 数量: {Parameter.ChildSteps?.Count ?? 0}"
                        );
                    Debug.WriteLine("  引用是否相同: {ReferenceEquals(Parameter.ChildSteps, updatedSteps)}");

                    // 更新显示
                    int stepCount = Parameter.ChildSteps?.Count ?? 0;
                    lblChildStepsCount.Text = $"循环体步骤 ({stepCount} 个)";

                    _hasUnsavedChanges = true;

                    Debug.WriteLine("========== 子步骤配置完成 ==========");

                    if (stepCount > 0)
                    {
                        MessageHelper.MessageOK($"已配置 {stepCount} 个循环体步骤", TType.Success);
                    }
                }
                else
                {
                    Logger?.LogDebug("用户取消了子步骤配置");
                }
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
                Debug.WriteLine("========== 开始保存循环配置 ==========");

                // 验证输入
                if (!ValidateInput())
                {
                    return;
                }

                // 获取当前步骤
                var currentStep = GetCurrentStepSafely();
                if (currentStep == null)
                {
                    MessageHelper.MessageOK("当前步骤无效,无法保存数据。", TType.Warn);
                    return;
                }

                // ⭐ 诊断日志1: 保存前的状态
                Debug.WriteLine("保存前状态:");
                Debug.WriteLine("  循环次数: {Count}", txtLoopCount.Text);
                Debug.WriteLine("  计数器变量: {Var}", txtCounterVariable.Text);
                Debug.WriteLine($"  子步骤数量: {Parameter.ChildSteps?.Count ?? 0}");

                if (Parameter.ChildSteps != null && Parameter.ChildSteps.Count > 0)
                {
                    for (int i = 0; i < Parameter.ChildSteps.Count; i++)
                    {
                        var child = Parameter.ChildSteps[i];
                        var hasParam = !string.IsNullOrEmpty(child.StepParameter?.ToString());
                        Debug.WriteLine($"    [{i}] {child.StepName}: 参数={hasParam}, 长度={child.StepParameter?.ToString()?.Length ?? 0}");
                    }
                }

                // 保存界面数据到参数对象
                SaveFormToParameter();

                // ⭐ 诊断日志2: 保存后的状态
                Debug.WriteLine("SaveFormToParameter 后状态:");
                Debug.WriteLine($"  子步骤数量: {Parameter.ChildSteps?.Count ?? 0}");
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };

                // 序列化参数对象
                var json = JsonConvert.SerializeObject(Parameter, Formatting.None, settings);

                // ⭐ 诊断日志3: 序列化后的JSON
                Logger?.LogDebug("序列化后的JSON: {Json}", json);

                // 保存到步骤
                currentStep.StepParameter = json;

                _hasUnsavedChanges = false;

                Debug.WriteLine("========== 循环配置保存成功 ==========");
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


        #region 基类方法重写

        /// <summary>
        /// 验证输入数据的有效性
        /// </summary>
        protected override bool ValidateInput()
        {
            try
            {
                // 验证循环次数
                if (string.IsNullOrWhiteSpace(txtLoopCount.Text))
                {
                    MessageHelper.MessageOK(this,"请输入循环次数！", TType.Warn);
                    txtLoopCount.Focus();
                    return false;
                }

                // 验证计数器变量名(如果启用)
                if (chkEnableCounter.Checked && string.IsNullOrWhiteSpace(txtCounterVariable.Text))
                {
                    MessageHelper.MessageOK("启用计数器时必须指定变量名！", TType.Warn);
                    txtCounterVariable.Focus();
                    return false;
                }

                // 验证子步骤
                if (Parameter.ChildSteps == null || Parameter.ChildSteps.Count == 0)
                {
                    var result = MessageHelper.MessageYes(this,
                        "尚未配置循环体步骤,是否继续保存?\n(建议至少配置一个步骤)",
                        TType.Warn);

                    if (result != DialogResult.OK)
                    {
                        return false;
                    }
                }
                else
                {
                    // 检查子步骤是否都有参数
                    int emptyParamCount = 0;
                    foreach (var child in Parameter.ChildSteps)
                    {
                        if (string.IsNullOrEmpty(child.StepParameter?.ToString()))
                        {
                            emptyParamCount++;
                        }
                    }

                    if (emptyParamCount > 0)
                    {
                        Logger?.LogWarning("发现 {Count} 个子步骤的参数为空", emptyParamCount);

                        // 可选: 提示用户但允许继续
                        MessageHelper.MessageOK(this,
                            $"警告: 有 {emptyParamCount} 个子步骤尚未配置参数",
                            TType.Warn);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入失败");
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
                ChildSteps = [],
                Description = $"循环步骤 {_workflowState?.StepNum + 1}"
            };

            Logger?.LogDebug("设置循环参数默认值");
            LoadParameterToForm();
        }

        /// <summary>
        /// 加载参数到界面
        /// </summary>
        protected override void LoadParameterToForm()
        {
            try
            {
                _isInitializing = true;

                txtLoopCount.Text = Parameter.LoopCountExpression ?? "10";
                txtCounterVariable.Text = Parameter.CounterVariableName ?? "LoopIndex";
                chkEnableCounter.Checked = Parameter.EnableCounter;
                txtDescription.Text = Parameter.Description ?? "";
                chkEnabled.Checked = true;

                // 更新子步骤计数
                int childStepCount = Parameter.ChildSteps?.Count ?? 0;
                lblChildStepsCount.Text = $"循环体步骤 ({childStepCount} 个)";

                Debug.WriteLine("LoadParameterToForm - 子步骤数量: {Count}", childStepCount);

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
        protected override void SaveFormToParameter()
        {
            try
            {
                // 只更新界面控件对应的属性
                Parameter.LoopCountExpression = txtLoopCount.Text?.Trim() ?? "10";
                Parameter.CounterVariableName = txtCounterVariable.Text?.Trim() ?? "LoopIndex";
                Parameter.EnableCounter = chkEnableCounter.Checked;
                Parameter.Description = txtDescription.Text?.Trim() ?? "";

                // 添加日志记录子步骤信息
                Logger?.LogDebug("SaveFormToParameter - 子步骤数量: {Count}",
                    Parameter.ChildSteps?.Count ?? 0);

                if (Parameter.ChildSteps != null)
                {
                    foreach (var child in Parameter.ChildSteps)
                    {
                        var paramLength = child.StepParameter?.ToString()?.Length ?? 0;
                        Logger?.LogDebug("  子步骤: {StepName}, 参数长度: {Length}",
                            child.StepName, paramLength);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存界面数据到参数对象失败");
                throw;
            }
        }
        #endregion
    }
}
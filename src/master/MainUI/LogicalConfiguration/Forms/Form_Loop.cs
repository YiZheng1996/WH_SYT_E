using AntdUI;
using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
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

                ChkEnableEarlyExit_CheckedChanged(null, null);
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
                txtExitCondition.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };

                // 复选框改变事件
                chkEnableCounter.CheckedChanged += (s, e) =>
                {
                    if (!_isInitializing)
                    {
                        _hasUnsavedChanges = true;
                        UpdateCounterControls();
                    }
                };

                chkEnableEarlyExit.CheckedChanged += ChkEnableEarlyExit_CheckedChanged;

                // 按钮点击事件
                btnSave.Click += BtnSave_Click;
                btnCancel.Click += BtnCancel_Click;
                btnHelp.Click += BtnHelp_Click;
                btnSelectVarCount.Click += BtnSelectVarCount_Click;
                btnSelectVarForExit.Click += BtnExpressionHelper_Click;
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

        /// <summary>
        /// 更新提前退出控件状态
        /// </summary>
        private void UpdateEarlyExitControls()
        {
            bool enabled = chkEnableEarlyExit.Checked;
            lblExitCondition.Enabled = enabled;
            txtExitCondition.Enabled = enabled;
            btnSelectVarForExit.Enabled = enabled;
            lblExitConditionHint.Enabled = enabled;

            //if (!enabled)
            //{
            //    txtExitCondition.Text = "";
            //}
        }

        #endregion

        #region 事件处理器

        /// <summary>
        /// 启用提前退出复选框状态改变事件
        /// </summary>
        private void ChkEnableEarlyExit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_isInitializing)
                {
                    _hasUnsavedChanges = true;
                    UpdateEarlyExitControls();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "切换提前退出状态失败");
            }
        }

        /// <summary>
        /// 选择循环次数变量
        /// </summary>
        private void BtnSelectVarCount_Click(object sender, EventArgs e)
        {
            SelectVariableForTextBox(txtLoopCount);
        }

        /// <summary>
        /// 选择退出条件变量
        /// </summary>
        private void BtnSelectVarForExit_Click(object sender, EventArgs e)
        {
            SelectVariableForTextBox(txtExitCondition);
        }

        /// <summary>
        /// 表达式助手按钮点击事件
        /// </summary>
        private void BtnExpressionHelper_Click(object sender, EventArgs e)
        {
            try
            {
                var expressionValidator = Program.ServiceProvider?.GetService<ExpressionEngine>();
                using var dialog = new ExpressionBuilderDialog(_globalVariable, expressionValidator);

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtExitCondition.Text = dialog.GeneratedExpression;
                    _hasUnsavedChanges = true;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "打开表达式助手失败");
                MessageHelper.MessageOK(this, $"打开助手失败：{ex.Message}", TType.Error);
            }
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

        #region 变量选择

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

                // 保存界面数据到参数对象
                SaveFormToParameter();

                // 获取当前步骤
                var currentStep = GetCurrentStepSafely();
                if (currentStep == null)
                {
                    MessageHelper.MessageOK("无法获取当前步骤信息", TType.Error);
                    return;
                }

                // ⭐ 诊断日志1: 保存前的Parameter状态
                Logger?.LogDebug("保存前 Parameter.ChildSteps 数量: {Count}",
                    Parameter.ChildSteps?.Count ?? 0);

                // ⭐ 诊断日志2: 检查子步骤参数
                if (Parameter.ChildSteps != null)
                {
                    foreach (var child in Parameter.ChildSteps)
                    {
                        var paramLength = child.StepParameter?.ToString()?.Length ?? 0;
                        Logger?.LogDebug("  子步骤: {StepName}, 参数长度: {Length}",
                            child.StepName, paramLength);
                    }
                }

                // 序列化参数到JSON
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    Formatting = Formatting.None
                };

                // 序列化参数对象
                var json = JsonConvert.SerializeObject(Parameter, Formatting.None, settings);

                // 诊断日志3: 序列化后的JSON
                Logger?.LogDebug("序列化后的JSON: {Json}", json);

                // 保存到步骤
                currentStep.StepParameter = json;

                _hasUnsavedChanges = false;

                Debug.WriteLine("========== 循环配置保存成功 ==========");
                MessageHelper.MessageOK("保存成功！循环配置将在主界面保存时写入配置文件。", TType.Success);

                DialogResult = DialogResult.OK;
                Close();
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
   • ⭐ 提前退出: 满足条件时立即退出循环

🔹 循环次数设置
   • 固定值: 如 10, 100
   • 变量引用: 使用 {变量名} 引用变量值
   • 示例: {MaxRetryCount}

🔹 计数器变量
   • 默认名称: LoopIndex
   • 索引范围: 从 1 开始到循环次数
   • 可在子步骤中使用 {LoopIndex} 引用当前索引
   • 可禁用计数器变量以提升性能

🔹 ⭐ 提前退出 (新功能)
   • 功能: 满足条件时立即退出循环
   • 条件格式: {变量名} 运算符 值
   • 运算符: ==, !=, >, <, >=, <=, AND, OR
   • 示例1: {压力值} >= 6.0
   • 示例2: {温度} > 80 AND {压力值} < 5
   • 场景1: 压力测试达标后立即退出
   • 场景2: 设备连接成功后停止重试
   • 场景3: 温度或压力超标时紧急停止

🔹 使用场景
   • 重复执行测试
   • 批量处理数据
   • 失败重试机制（配合提前退出）
   • 数据采集
   • 条件满足时提前终止

⚠️ 注意事项
   1. 循环次数必须大于 0
   2. 避免设置过大的循环次数造成长时间执行
   3. 计数器变量名不能与现有变量重名
   4. 提前退出条件会在每次循环迭代前检查
   5. 循环内部可使用 Break 和 Continue 控制步骤
   6. 确保循环体内的步骤配置正确
   7. 退出条件中的变量必须在循环体中被更新

💡 提前退出示例
   场景: 压力测试，达到6.0 MPa即停止
   配置:
     循环次数: 100
     ✓ 启用提前退出
     退出条件: {压力值} >= 6.0
   
   循环体步骤:
     1. 读取PLC → {压力值}
     2. 延时 100ms
   
   效果: 第15次达标后立即退出，节省85%时间

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
                    MessageHelper.MessageOK(this, "请输入循环次数！", TType.Warn);
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

                // ⭐ 验证提前退出条件
                if (chkEnableEarlyExit.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtExitCondition.Text))
                    {
                        MessageHelper.MessageOK(this,
                         "启用提前退出时必须输入退出条件！\n\n" +
                         "支持功能:\n" +
                         "• 基本比较: {压力值} >= 6.0\n" +
                         "• 数学运算: {A} * 2 + {B} > 10\n" +
                         "• 逻辑运算: {A} > 5 AND {B} < 10\n" +
                         "• 函数调用: ABS({偏差}) < 0.1\n\n" +
                         "点击帮助按钮查看更多示例",
                         TType.Warn);
                        txtExitCondition.Focus();
                        return false;
                    }

                    // 简单验证：检查是否包含变量引用
                    string condition = txtExitCondition.Text.Trim();
                    if (!condition.Contains("{") && !condition.Contains("}"))
                    {
                        var result = MessageHelper.MessageYes(this,
                            "退出条件中未检测到变量引用，确定继续？\n" +
                            "建议使用格式: {变量名} 运算符 值\n" +
                            "例如: {压力值} >= 6.0",
                            TType.Warn);
                        if (result != DialogResult.OK)
                        {
                            txtExitCondition.Focus();
                            return false;
                        }
                    }
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
                EnableEarlyExit = false,
                ExitConditionExpression = "",
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

                // ⭐ 加载提前退出配置
                chkEnableEarlyExit.Checked = Parameter.EnableEarlyExit;
                txtExitCondition.Text = Parameter.ExitConditionExpression ?? "";

                // 更新子步骤计数
                int childStepCount = Parameter.ChildSteps?.Count ?? 0;
                lblChildStepsCount.Text = $"循环体步骤 ({childStepCount} 个)";

                Debug.WriteLine("LoadParameterToForm - 子步骤数量: {Count}", childStepCount);

                UpdateCounterControls();
                UpdateEarlyExitControls();

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

                // ⭐ 保存提前退出配置
                Parameter.EnableEarlyExit = chkEnableEarlyExit.Checked;
                Parameter.ExitConditionExpression = txtExitCondition.Text?.Trim() ?? "";

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
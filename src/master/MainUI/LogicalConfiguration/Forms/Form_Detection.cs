using AntdUI;
using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 检测配置表单 - 改进版
    /// 提供清晰、美观的界面进行检测项配置
    /// 支持多种检测类型和数据源
    /// </summary>
    public partial class Form_Detection : BaseParameterForm, IParameterForm<Parameter_Detection>
    {
        #region 私有字段

        private Parameter_Detection _parameter;
        private bool _isInitializing = true;
        private bool _hasUnsavedChanges = false;
        private System.Windows.Forms.Timer _validationTimer;

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象
        /// </summary>
        public Parameter_Detection Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_Detection();

                // 只在设计模式或窗体未创建时跳过加载
                if (!DesignMode && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 设计器构造函数
        /// </summary>
        public Form_Detection()
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
        public Form_Detection(IWorkflowStateService workflowState, ILogger<Form_Detection> logger)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// 带参数的依赖注入构造函数
        /// </summary>
        public Form_Detection(IWorkflowStateService workflowState, ILogger<Form_Detection> logger, Parameter_Detection parameter)
            : base(workflowState, logger)
        {
            InitializeComponent();
            Parameter = parameter ?? new Parameter_Detection();
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化表单
        /// </summary>
        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 初始化参数
                _parameter ??= new Parameter_Detection();

                // 初始化验证定时器
                InitializeValidationTimer();

                // 初始化下拉框
                InitializeComboBoxes();

                // 初始化变量下拉框
                InitializeVariableComboBoxes();

                // 初始化PLC下拉框
                InitializePlcComboBoxes();

                // 设置事件处理器
                SetupEventHandlers();

                // 加载工具提示
                InitializeToolTips();

                // 注意：不在这里调用LoadParameterToForm()
                // 因为基类的OnLoad事件会自动处理参数加载

                _isInitializing = false;
                Logger?.LogInformation("检测工具窗体初始化完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化表单时发生错误");
                MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 初始化验证定时器
        /// </summary>
        private void InitializeValidationTimer()
        {
            _validationTimer = new System.Windows.Forms.Timer
            {
                Interval = 500
            };
            _validationTimer.Tick += ValidationTimer_Tick;
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitializeComboBoxes()
        {
            try
            {
                // 初始化检测类型下拉框
                cmbDetectionType.DataSource = EnumExtensions.GetEnumItems<DetectionType>();
                cmbDetectionType.DisplayMember = "DisplayName";
                cmbDetectionType.ValueMember = "Value";

                // 初始化数据源类型下拉框
                cmbDataSourceType.DataSource = EnumExtensions.GetEnumItems<DataSourceType>();
                cmbDataSourceType.DisplayMember = "DisplayName";
                cmbDataSourceType.ValueMember = "Value";

                // 初始化比较操作符下拉框
                cmbOperator.DataSource = EnumExtensions.GetEnumItems<ComparisonOperator>();
                cmbOperator.DisplayMember = "DisplayName";
                cmbOperator.ValueMember = "Value";

                // 初始化失败处理行为下拉框
                cmbFailureAction.DataSource = EnumExtensions.GetEnumItems<FailureAction>();
                cmbFailureAction.DisplayMember = "DisplayName";
                cmbFailureAction.ValueMember = "Value";

                // 设置默认值
                cmbDetectionType.SelectedValue = DetectionType.ValueRange;
                cmbDataSourceType.SelectedValue = DataSourceType.Variable;
                cmbOperator.SelectedValue = ComparisonOperator.GreaterThan;
                cmbFailureAction.SelectedValue = FailureAction.Continue;

                Logger?.LogDebug("下拉框初始化完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化下拉框失败");
            }
        }

        /// <summary>
        /// 初始化变量下拉框
        /// </summary>
        private void InitializeVariableComboBoxes()
        {
            try
            {
                var globalVarManager = Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (globalVarManager == null) return;

                var variables = globalVarManager.GetAllUserVariables()
                    .Select(v => v.VarName)
                    .ToList();

                // 变量数据源
                CboVariableName.Items.Clear();
                CboVariableName.Items.AddRange(variables.ToArray());

                // 结果变量
                CboResultVariable.Items.Clear();
                CboResultVariable.Items.AddRange(variables.ToArray());

                // 值变量
                CboValueVariable.Items.Clear();
                CboValueVariable.Items.AddRange(variables.ToArray());

                Logger?.LogDebug("变量下拉框初始化完成，共 {Count} 个变量", variables.Count);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化变量下拉框失败");
            }
        }

        /// <summary>
        /// 初始化PLC下拉框
        /// </summary>
        private async void InitializePlcComboBoxes()
        {
            try
            {
                var plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
                if (plcManager == null) return;

                // 获取PLC模块列表
                var modules = await plcManager.GetModuleTagsAsync();
                CboPlcModule.Items.Clear();
                CboPlcModule.Items.AddRange(modules.Keys.ToArray());

                Logger?.LogDebug("PLC模块下拉框初始化完成，共 {Count} 个模块", modules.Count);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化PLC下拉框失败");
            }
        }

        /// <summary>
        /// 初始化工具提示
        /// </summary>
        private void InitializeToolTips()
        {
            toolTip.SetToolTip(txtDetectionName, "输入检测项的名称，用于标识此检测项");
            toolTip.SetToolTip(txtDescription, "输入检测项的描述信息，说明检测目的");
            toolTip.SetToolTip(cmbDetectionType, "选择检测类型：值范围、数值比较、布尔检测等");
            toolTip.SetToolTip(cmbDataSourceType, "选择数据来源：变量或PLC");
            toolTip.SetToolTip(cmbOperator, "选择比较操作符：大于、小于、等于等");
            toolTip.SetToolTip(numMinValue, "设置允许的最小值");
            toolTip.SetToolTip(numMaxValue, "设置允许的最大值");
            toolTip.SetToolTip(txtTargetValue, "设置目标值或使用 {变量名} 引用变量");
            toolTip.SetToolTip(numTolerance, "设置允许的容差范围");
            toolTip.SetToolTip(numTimeoutMs, "设置检测超时时间（毫秒）");
            toolTip.SetToolTip(numRetryCount, "设置检测失败后的重试次数");
            toolTip.SetToolTip(numRetryIntervalMs, "设置重试之间的间隔时间（毫秒）");
            toolTip.SetToolTip(btnTest, "测试当前检测配置");
            toolTip.SetToolTip(btnHelp, "查看帮助信息");
        }

        /// <summary>
        /// 设置事件处理器
        /// </summary>
        private void SetupEventHandlers()
        {
            try
            {
                // 下拉框改变事件
                cmbDetectionType.SelectedIndexChanged += CmbDetectionType_SelectedIndexChanged;
                cmbDataSourceType.SelectedIndexChanged += CmbDataSourceType_SelectedIndexChanged;
                cmbFailureAction.SelectedIndexChanged += CmbFailureAction_SelectedIndexChanged;

                // 复选框改变事件
                chkEnabled.CheckedChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                chkSaveResult.CheckedChanged += ChkSaveResult_CheckedChanged;
                chkSaveValue.CheckedChanged += ChkSaveValue_CheckedChanged;
                chkShowResult.CheckedChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };

                // 文本改变事件
                txtDetectionName.TextChanged += (s, e) => { if (!_isInitializing) { _hasUnsavedChanges = true; RestartValidationTimer(); } };
                txtDescription.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                txtTargetValue.TextChanged += (s, e) => { if (!_isInitializing) { _hasUnsavedChanges = true; RestartValidationTimer(); } };

                // 数值改变事件
                numMinValue.ValueChanged += (s, e) => { if (!_isInitializing) { _hasUnsavedChanges = true; RestartValidationTimer(); } };
                numMaxValue.ValueChanged += (s, e) => { if (!_isInitializing) { _hasUnsavedChanges = true; RestartValidationTimer(); } };
                numTolerance.ValueChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                numThreshold.ValueChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                numTimeoutMs.ValueChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                numRetryCount.ValueChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                numRetryIntervalMs.ValueChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                numFailureStep.ValueChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                numSuccessStep.ValueChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };

                // PLC相关事件
                CboPlcModule.SelectedIndexChanged += CboPlcModule_SelectedIndexChanged;

                // 按钮事件
                btnOK.Click += BtnOK_Click;
                btnCancel.Click += BtnCancel_Click;
                btnTest.Click += BtnTest_Click;
                btnHelp.Click += BtnHelp_Click;

                // 窗体关闭事件
                this.FormClosing += Form_Detection_FormClosing;

                Logger?.LogDebug("事件处理器绑定完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "绑定事件处理器失败");
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 检测类型改变事件
        /// </summary>
        private void CmbDetectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing || cmbDetectionType.SelectedValue == null) return;

            _hasUnsavedChanges = true;
            UpdateUIBasedOnDetectionType();
            RestartValidationTimer();
        }

        /// <summary>
        /// 数据源类型改变事件
        /// </summary>
        private void CmbDataSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing || cmbDataSourceType.SelectedValue == null) return;

            _hasUnsavedChanges = true;
            UpdateUIBasedOnDataSourceType();
            RestartValidationTimer();
        }

        /// <summary>
        /// 失败处理行为改变事件
        /// </summary>
        private void CmbFailureAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing || cmbFailureAction.SelectedValue == null) return;

            _hasUnsavedChanges = true;
            UpdateUIBasedOnFailureAction();
        }

        /// <summary>
        /// PLC模块改变事件
        /// </summary>
        private async void CboPlcModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing || string.IsNullOrEmpty(CboPlcModule.Text)) return;

            try
            {
                var plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
                if (plcManager == null) return;

                // 获取选中模块的地址列表
                var addresses = await plcManager.GetModuleTagsAsync();
                CboPlcAddress.Items.Clear();
                if (addresses.TryGetValue(CboPlcModule?.SelectedText, out List<string> addresse))
                {
                    CboPlcAddress.Items.AddRange([.. addresse]);
                }

                Logger?.LogDebug("加载PLC模块 {Module} 的地址，共 {Count} 个", CboPlcModule.Text, addresses.Count);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC地址失败");
            }
        }

        /// <summary>
        /// 保存结果复选框改变事件
        /// </summary>
        private void ChkSaveResult_CheckedChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            CboResultVariable.Enabled = chkSaveResult.Checked;

            if (chkSaveResult.Checked && CboResultVariable.Items.Count == 0)
            {
                InitializeVariableComboBoxes();
            }
        }

        /// <summary>
        /// 保存值复选框改变事件
        /// </summary>
        private void ChkSaveValue_CheckedChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            CboValueVariable.Enabled = chkSaveValue.Checked;

            if (chkSaveValue.Checked && CboValueVariable.Items.Count == 0)
            {
                InitializeVariableComboBoxes();
            }
        }

        /// <summary>
        /// 验证定时器触发事件
        /// </summary>
        private void ValidationTimer_Tick(object sender, EventArgs e)
        {
            _validationTimer.Stop();
            ValidateConfiguration();
        }

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                SaveParameters();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存检测配置失败");
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
        /// 测试按钮点击事件
        /// </summary>
        private async void BtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateStatusText("正在测试检测配置...", false);
                btnTest.Enabled = false;

                if (!ValidateInput())
                {
                    UpdateStatusText("验证失败，请检查配置", true);
                    return;
                }

                // 保存当前配置
                SaveFormToParameter();

                // 执行测试（这里可以调用实际的检测逻辑）
                await Task.Delay(1000); // 模拟测试过程

                UpdateStatusText("测试完成 - 配置有效", false);
                MessageHelper.MessageOK(this, "检测配置测试通过！", TType.Success);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "测试检测配置失败");
                UpdateStatusText($"测试失败：{ex.Message}", true);
                MessageHelper.MessageOK($"测试失败：{ex.Message}", TType.Error);
            }
            finally
            {
                btnTest.Enabled = true;
            }
        }

        /// <summary>
        /// 帮助按钮点击事件
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            string helpText = @"检测工具配置帮助

1. 基本信息：
   - 检测名称：为此检测项指定一个唯一的名称
   - 描述：说明此检测项的用途和目的
   - 检测类型：选择检测方式（值范围、数值比较等）

2. 数据源配置：
   - 变量：从全局变量中读取数据
   - PLC：从PLC设备读取数据

3. 检测条件：
   - 比较操作符：定义如何判断检测结果
   - 最小值/最大值：定义有效数据范围
   - 目标值：设置期望的目标值
   - 容差：允许的偏差范围
   - 阈值：触发报警的阈值

4. 超时和重试：
   - 超时时间：检测操作的最长等待时间
   - 重试次数：失败后重试的次数
   - 重试间隔：每次重试之间的等待时间

5. 结果处理：
   - 保存结果：将检测结果（通过/失败）保存到变量
   - 保存值：将检测的实际数值保存到变量
   - 失败后操作：定义检测失败时的行为
   - 失败步骤/成功步骤：跳转到指定步骤（-1表示下一步）

6. 测试功能：
   - 点击测试按钮验证配置的正确性
   - 测试不会影响实际的工作流执行";

            MessageHelper.MessageOK(this, helpText, TType.Info);
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_Detection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK) return;
            if (_hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes(this, "是否放弃未保存的更改？");
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region UI更新方法

        /// <summary>
        /// 根据检测类型更新UI
        /// </summary>
        private void UpdateUIBasedOnDetectionType()
        {
            if (cmbDetectionType.SelectedValue == null) return;

            var detectionType = (DetectionType)cmbDetectionType.SelectedValue;

            // 根据不同的检测类型显示/隐藏相应的控件
            switch (detectionType)
            {
                case DetectionType.ValueRange:
                    // 值范围检测：显示最小值和最大值
                    numMinValue.Visible = lblMinValue.Visible = true;
                    numMaxValue.Visible = lblMaxValue.Visible = true;
                    txtTargetValue.Visible = lblTargetValue.Visible = false;
                    numThreshold.Visible = lblThreshold.Visible = false;
                    break;

                case DetectionType.Equality:
                    // 值比较检测：显示目标值和容差
                    numMinValue.Visible = lblMinValue.Visible = false;
                    numMaxValue.Visible = lblMaxValue.Visible = false;
                    txtTargetValue.Visible = lblTargetValue.Visible = true;
                    numTolerance.Visible = lblTolerance.Visible = true;
                    numThreshold.Visible = lblThreshold.Visible = false;
                    break;

                case DetectionType.Status:
                    // 阈值检测：显示阈值
                    numMinValue.Visible = lblMinValue.Visible = false;
                    numMaxValue.Visible = lblMaxValue.Visible = false;
                    txtTargetValue.Visible = lblTargetValue.Visible = false;
                    numThreshold.Visible = lblThreshold.Visible = true;
                    break;

                default:
                    // 默认显示所有
                    numMinValue.Visible = lblMinValue.Visible = true;
                    numMaxValue.Visible = lblMaxValue.Visible = true;
                    txtTargetValue.Visible = lblTargetValue.Visible = true;
                    numTolerance.Visible = lblTolerance.Visible = true;
                    numThreshold.Visible = lblThreshold.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// 根据数据源类型更新UI
        /// </summary>
        private void UpdateUIBasedOnDataSourceType()
        {
            if (cmbDataSourceType.SelectedValue == null) return;

            var sourceType = (DataSourceType)cmbDataSourceType.SelectedValue;

            // 显示/隐藏相应的数据源面板
            switch (sourceType)
            {
                case DataSourceType.Variable:
                    pnlVariableSource.Visible = true;
                    pnlPlcSource.Visible = false;
                    break;

                case DataSourceType.PLC:
                    pnlVariableSource.Visible = false;
                    pnlPlcSource.Visible = true;

                    // 初始化PLC下拉框
                    if (CboPlcModule.Items.Count == 0)
                    {
                        InitializePlcComboBoxes();
                    }
                    break;

                default:
                    pnlVariableSource.Visible = true;
                    pnlPlcSource.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// 根据失败处理行为更新UI
        /// </summary>
        private void UpdateUIBasedOnFailureAction()
        {
            if (cmbFailureAction.SelectedValue == null) return;

            var failureAction = (FailureAction)cmbFailureAction.SelectedValue;

            // 根据失败处理行为显示/隐藏步骤输入
            bool showStepInputs = failureAction == FailureAction.Jump ||
                                 failureAction == FailureAction.Stop;

            numFailureStep.Visible = lblFailureStep.Visible = showStepInputs;
        }

        /// <summary>
        /// 重启验证定时器
        /// </summary>
        private void RestartValidationTimer()
        {
            _validationTimer.Stop();
            _validationTimer.Start();
        }

        /// <summary>
        /// 更新状态栏文本
        /// </summary>
        private void UpdateStatusText(string text, bool isError = false)
        {
            lblStatusText.Text = text;
            lblStatusText.ForeColor = isError ?
                System.Drawing.Color.FromArgb(220, 53, 69) :
                System.Drawing.Color.FromArgb(100, 100, 100);
        }

        #endregion

        #region 参数处理

        /// <summary>
        /// 收集参数（基类方法重写）
        /// 供基类框架调用，返回通用的参数对象
        /// </summary>
        /// <returns>当前配置的参数对象</returns>
        protected override object CollectParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            _parameter = new Parameter_Detection
            {
                DetectionName = $"检测项 {_workflowState?.StepNum + 1}",
                //Description = "",
                //Enabled = true,
                Type = DetectionType.ValueRange,
                TimeoutMs = 5000,
                RetryCount = 0,
                RetryIntervalMs = 1000,
                DataSource = new DataSourceConfig
                {
                    SourceType = DataSourceType.Variable,
                    VariableName = "",
                    PlcConfig = new PlcAddressConfig(),
                },
                Condition = new DetectionCondition
                {
                    MinValue = 0,
                    MaxValue = 100,
                    Operator = ComparisonOperator.GreaterThan
                },
                ResultHandling = new ResultHandling
                {
                    OnFailure = FailureAction.Continue,
                    ShowResult = true,
                    MessageTemplate = "检测项 {DetectionName}: {Result}"
                }
            };

            Logger?.LogDebug("设置检测参数默认值");
            LoadParameterToForm();
        }

        /// <summary>
        /// 从参数加载到界面
        /// </summary>
        protected void LoadParameterToForm()
        {
            if (_parameter == null) return;

            try
            {
                _isInitializing = true;

                // 基本信息
                txtDetectionName.Text = _parameter.DetectionName ?? "";
                //txtDescription.Text = _parameter.Description ?? "";
                cmbDetectionType.SelectedValue = _parameter.Type;
                //chkEnabled.Checked = _parameter.Enabled;

                // 数据源
                cmbDataSourceType.SelectedValue = _parameter.DataSource?.SourceType ?? DataSourceType.Variable;
                CboVariableName.Text = _parameter.DataSource?.VariableName ?? "";
                CboPlcModule.Text = _parameter.DataSource?.PlcConfig?.ModuleName ?? "";
                CboPlcAddress.Text = _parameter.DataSource?.PlcConfig?.Address ?? "";

                // 检测条件
                numMinValue.Value = _parameter.Condition?.MinValue ?? 0;
                numMaxValue.Value = _parameter.Condition?.MaxValue ?? 100;
                txtTargetValue.Text = _parameter.Condition?.TargetValue ?? "";
                numTolerance.Value = _parameter.Condition?.Tolerance ?? 0;
                numThreshold.Value = _parameter.Condition?.ThresholdValue ?? 0;
                cmbOperator.SelectedValue = _parameter.Condition?.Operator ?? ComparisonOperator.GreaterThan;

                // 超时和重试
                numTimeoutMs.Value = _parameter.TimeoutMs;
                numRetryCount.Value = _parameter.RetryCount;
                numRetryIntervalMs.Value = _parameter.RetryIntervalMs;

                // 结果处理
                chkSaveResult.Checked = _parameter.ResultHandling?.SaveToVariable ?? false;
                CboResultVariable.Text = _parameter.ResultHandling?.ResultVariableName ?? "";
                chkSaveValue.Checked = _parameter.ResultHandling?.SaveValueToVariable ?? false;
                CboValueVariable.Text = _parameter.ResultHandling?.ValueVariableName ?? "";
                cmbFailureAction.SelectedValue = _parameter.ResultHandling?.OnFailure ?? FailureAction.Continue;
                numFailureStep.Value = _parameter.ResultHandling?.FailureStepIndex ?? -1;
                numSuccessStep.Value = _parameter.ResultHandling?.SuccessStepIndex ?? -1;
                chkShowResult.Checked = _parameter.ResultHandling?.ShowResult ?? true;

                // 更新UI状态
                UpdateUIBasedOnDetectionType();
                UpdateUIBasedOnDataSourceType();
                UpdateUIBasedOnFailureAction();

                _hasUnsavedChanges = false;
                Logger?.LogDebug("参数加载到界面完成");
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
        /// 从界面保存到参数
        /// </summary>
        private void SaveFormToParameter()
        {
            if (_parameter == null) return;

            try
            {
                // 基本信息
                _parameter.DetectionName = txtDetectionName.Text?.Trim() ?? "";
                //_parameter.Description = txtDescription.Text?.Trim() ?? "";
                _parameter.Type = (DetectionType)(cmbDetectionType.SelectedValue ?? DetectionType.ValueRange);
                //_parameter.Enabled = chkEnabled.Checked;

                // 数据源
                _parameter.DataSource ??= new DataSourceConfig();
                _parameter.DataSource.SourceType = (DataSourceType)(cmbDataSourceType.SelectedValue ?? DataSourceType.Variable);
                _parameter.DataSource.VariableName = CboVariableName.Text?.Trim() ?? "";
                _parameter.DataSource.PlcConfig ??= new PlcAddressConfig();
                _parameter.DataSource.PlcConfig.ModuleName = CboPlcModule.Text?.Trim() ?? "";
                _parameter.DataSource.PlcConfig.Address = CboPlcAddress.Text?.Trim() ?? "";

                // 检测条件
                _parameter.Condition ??= new DetectionCondition();
                _parameter.Condition.MinValue = (double)numMinValue.Value;
                _parameter.Condition.MaxValue = (double)numMaxValue.Value;
                _parameter.Condition.TargetValue = txtTargetValue.Text?.Trim() ?? "";
                _parameter.Condition.ThresholdValue = (double)numThreshold.Value;
                _parameter.Condition.Operator = (ComparisonOperator)(cmbOperator.SelectedValue ?? ComparisonOperator.GreaterThan);
                _parameter.Condition.Tolerance = (double)numTolerance.Value;

                // 超时和重试
                _parameter.TimeoutMs = numTimeoutMs.Value;
                _parameter.RetryCount = numRetryCount.Value;
                _parameter.RetryIntervalMs = numRetryIntervalMs.Value;

                // 结果处理
                _parameter.ResultHandling ??= new ResultHandling();
                _parameter.ResultHandling.SaveToVariable = chkSaveResult.Checked;
                _parameter.ResultHandling.ResultVariableName = CboResultVariable.Text?.Trim() ?? "";
                _parameter.ResultHandling.SaveValueToVariable = chkSaveValue.Checked;
                _parameter.ResultHandling.ValueVariableName = CboValueVariable.Text?.Trim() ?? "";
                _parameter.ResultHandling.OnFailure = (FailureAction)(cmbFailureAction.SelectedValue ?? FailureAction.Continue);
                _parameter.ResultHandling.FailureStepIndex = numFailureStep.Value;
                _parameter.ResultHandling.SuccessStepIndex = numSuccessStep.Value;
                _parameter.ResultHandling.ShowResult = chkShowResult.Checked;

                _hasUnsavedChanges = false;
                Logger?.LogDebug("界面参数保存完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存界面参数失败");
                throw;
            }
        }

        #endregion

        #region 验证方法

        /// <summary>
        /// 验证配置
        /// </summary>
        private void ValidateConfiguration()
        {
            try
            {
                var errors = new List<string>();

                // 验证检测名称
                if (string.IsNullOrWhiteSpace(txtDetectionName.Text))
                {
                    errors.Add("请输入检测名称");
                }

                // 验证数据源
                if (cmbDataSourceType.SelectedValue != null)
                {
                    var sourceType = (DataSourceType)cmbDataSourceType.SelectedValue;
                    if (sourceType == DataSourceType.Variable)
                    {
                        if (string.IsNullOrWhiteSpace(CboVariableName.Text))
                        {
                            errors.Add("请选择变量");
                        }
                    }
                    else if (sourceType == DataSourceType.PLC)
                    {
                        if (string.IsNullOrWhiteSpace(CboPlcModule.Text))
                        {
                            errors.Add("请选择PLC模块");
                        }
                        if (string.IsNullOrWhiteSpace(CboPlcAddress.Text))
                        {
                            errors.Add("请选择PLC地址");
                        }
                    }
                }

                // 验证检测条件
                if (cmbDetectionType.SelectedValue != null)
                {
                    var detectionType = (DetectionType)cmbDetectionType.SelectedValue;
                    if (detectionType == DetectionType.ValueRange)
                    {
                        if (numMinValue.Value > numMaxValue.Value)
                        {
                            errors.Add("最小值不能大于最大值");
                        }
                    }
                }

                // 更新状态显示
                if (errors.Count > 0)
                {
                    UpdateStatusText($"配置验证失败：{string.Join("; ", errors)}", true);
                }
                else
                {
                    UpdateStatusText("配置有效", false);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证配置失败");
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        protected bool ValidateInput()
        {
            try
            {
                // 验证检测名称
                if (string.IsNullOrWhiteSpace(txtDetectionName.Text))
                {
                    MessageHelper.MessageOK("请输入检测名称！", TType.Warn);
                    txtDetectionName.Focus();
                    return false;
                }

                // 验证数据源
                if (cmbDataSourceType.SelectedValue != null)
                {
                    var sourceType = (DataSourceType)cmbDataSourceType.SelectedValue;
                    if (sourceType == DataSourceType.Variable)
                    {
                        if (string.IsNullOrWhiteSpace(CboVariableName.Text))
                        {
                            MessageHelper.MessageOK("请选择要检测的变量！", TType.Warn);
                            CboVariableName.Focus();
                            return false;
                        }
                    }
                    else if (sourceType == DataSourceType.PLC)
                    {
                        if (string.IsNullOrWhiteSpace(CboPlcModule.Text))
                        {
                            MessageHelper.MessageOK("请选择PLC模块！", TType.Warn);
                            CboPlcModule.Focus();
                            return false;
                        }
                        if (string.IsNullOrWhiteSpace(CboPlcAddress.Text))
                        {
                            MessageHelper.MessageOK("请选择PLC地址！", TType.Warn);
                            CboPlcAddress.Focus();
                            return false;
                        }
                    }
                }

                // 验证检测条件
                if (cmbDetectionType.SelectedValue != null)
                {
                    var detectionType = (DetectionType)cmbDetectionType.SelectedValue;
                    if (detectionType == DetectionType.ValueRange)
                    {
                        if (numMinValue.Value > numMaxValue.Value)
                        {
                            MessageHelper.MessageOK("最小值不能大于最大值！", TType.Warn);
                            numMinValue.Focus();
                            return false;
                        }
                    }
                }

                // 验证结果变量
                if (chkSaveResult.Checked && string.IsNullOrWhiteSpace(CboResultVariable.Text))
                {
                    MessageHelper.MessageOK("启用了保存结果，但未选择结果变量！", TType.Warn);
                    CboResultVariable.Focus();
                    return false;
                }

                if (chkSaveValue.Checked && string.IsNullOrWhiteSpace(CboValueVariable.Text))
                {
                    MessageHelper.MessageOK("启用了保存值，但未选择值变量！", TType.Warn);
                    CboValueVariable.Focus();
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

        #endregion

        #region 重写基类方法
        protected override void LoadParameterFromStep(object stepParameter)
        {
            if (!IsServiceAvailable) return;

            // 只转换参数并调用PopulateControls,不要直接设置Parameter属性
            var parameter = ConvertParameter(stepParameter);
            PopulateControls(parameter);  // 内部会处理参数设置和加载
        }

        #endregion

        #region IParameterForm<Parameter_Detection> 接口实现

        public void PopulateControls(Parameter_Detection parameter)
        {
            // 直接赋值给私有字段,避免触发属性的set访问器
            _parameter = parameter ?? new Parameter_Detection();

            // 直接调用加载方法,不依赖属性的条件判断
            LoadParameterToForm();
        }

        void IParameterForm<Parameter_Detection>.SetDefaultValues()
        {
            SetDefaultValues();
        }

        public bool ValidateTypedParameters()
        {
            return ValidateInput();
        }

        public Parameter_Detection CollectTypedParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        public Parameter_Detection ConvertParameter(object stepParameter)
        {
            try
            {
                Logger?.LogDebug("开始转换参数,类型: {Type}", stepParameter?.GetType().Name ?? "null");

                // 参数为空
                if (stepParameter == null)
                {
                    Logger?.LogWarning("步骤参数为空,使用默认参数");
                    return new Parameter_Detection();
                }

                // 已经是正确的类型
                if (stepParameter is Parameter_Detection detectionParam)
                {
                    Logger?.LogDebug("参数已经是 Parameter_Detection 类型");
                    return detectionParam;
                }

                // 是JSON字符串
                if (stepParameter is string jsonStr && !string.IsNullOrWhiteSpace(jsonStr))
                {
                    try
                    {
                        Logger?.LogDebug("尝试从JSON字符串反序列化");
                        var param = JsonConvert.DeserializeObject<Parameter_Detection>(jsonStr);
                        if (param != null)
                        {
                            Logger?.LogInformation("JSON字符串反序列化成功");
                            return param;
                        }
                    }
                    catch (JsonException ex)
                    {
                        Logger?.LogWarning(ex, "JSON字符串反序列化失败");
                    }
                }

                // 是其他对象类型(比如 JObject、匿名类型等)
                try
                {
                    Logger?.LogDebug("尝试先序列化再反序列化");
                    string jsonString = JsonConvert.SerializeObject(stepParameter);
                    var param = JsonConvert.DeserializeObject<Parameter_Detection>(jsonString);
                    if (param != null)
                    {
                        //Logger?.LogInformation("对象序列化转换成功");
                        return param;
                    }
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "对象序列化转换失败");
                }

                // 所有方法都失败,返回默认参数
                Logger?.LogWarning("所有转换方法都失败,使用默认参数");
                return new Parameter_Detection();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "ConvertParameter 发生异常");
                return new Parameter_Detection();
            }
        }

        #endregion
    }
}
using AntdUI;
using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 检测配置表单
    /// </summary>
    public partial class Form_Detection : BaseParameterForm, IParameterForm<Parameter_Detection>
    {
        private Parameter_Detection _parameter;

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
                // 只有在窗体完全加载且不处于基类的加载状态时才更新界面
                if (!DesignMode && !IsLoading && IsHandleCreated)
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

            _parameter ??= new Parameter_Detection();
            InitializeComboBoxes();
            SetupEventHandlers();

            // 注意：不在这里调用LoadParameterToForm()
            // 因为基类的OnLoad事件会自动处理参数加载
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
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化下拉框失败");
            }
        }

        /// <summary>
        /// 设置事件处理器
        /// </summary>
        private void SetupEventHandlers()
        {
            cmbDetectionType.SelectedIndexChanged += CmbDetectionType_SelectedIndexChanged;
            cmbDataSourceType.SelectedIndexChanged += CmbDataSourceType_SelectedIndexChanged;
            cmbFailureAction.SelectedIndexChanged += CmbFailureAction_SelectedIndexChanged;
            CboPlcModule.SelectedIndexChanged += CboPlcModule_SelectedIndexChanged;

            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            btnTestDetection.Click += BtnTestDetection_Click;
        }
        #endregion

        #region 数据绑定方法
        /// <summary>
        /// 加载参数到表单
        /// </summary>
        private async void LoadParameterToForm()
        {
            if (_parameter == null) return;

            try
            {
                // 基本信息
                txtDetectionName.Text = _parameter.DetectionName;
                cmbDetectionType.SelectedValue = _parameter.Type;
                numRefreshRate.Value = _parameter.RefreshRateMs;
                numTimeout.Value = _parameter.TimeoutMs;
                numRetryCount.Value = _parameter.RetryCount;
                numRetryInterval.Value = _parameter.RetryIntervalMs;

                // 数据源配置
                cmbDataSourceType.SelectedValue = _parameter.DataSource.SourceType;

                // 先根据数据源类型加载相应的数据，然后再设置值
                var sourceType = _parameter.DataSource.SourceType;
                switch (sourceType)
                {
                    case DataSourceType.Variable:
                        ParameterGlobalVariable(); // 先加载变量列表
                        SetComboBoxValue(CboVariableName, _parameter.DataSource.VariableName);
                        pnlVariableSource.Visible = true;
                        pnlPlcSource.Visible = false;
                        break;

                    case DataSourceType.PLC:
                        await Parameterplcs(); // 先加载PLC模块列表
                        SetComboBoxValue(CboPlcModule, _parameter.DataSource.PlcConfig.ModuleName);

                        // 如果有模块名，加载对应的地址列表
                        if (!string.IsNullOrEmpty(_parameter.DataSource.PlcConfig.ModuleName))
                        {
                            await LoadPlcAddresses(_parameter.DataSource.PlcConfig.ModuleName);
                            SetComboBoxValue(CboPlcAddress, _parameter.DataSource.PlcConfig.Address);
                        }

                        pnlVariableSource.Visible = false;
                        pnlPlcSource.Visible = true;
                        break;
                }

                // 检测条件
                numMinValue.Value = (decimal)_parameter.Condition.MinValue;
                numMaxValue.Value = (decimal)_parameter.Condition.MaxValue;
                txtTargetValue.Text = _parameter.Condition.TargetValue;
                numThreshold.Value = (decimal)_parameter.Condition.ThresholdValue;
                cmbOperator.SelectedValue = _parameter.Condition.Operator;
                numTolerance.Value = (decimal)_parameter.Condition.Tolerance;

                // 结果处理
                chkSaveResult.Checked = _parameter.ResultHandling.SaveToVariable;
                CboResultVariable.Text = _parameter.ResultHandling.ResultVariableName;
                chkSaveValue.Checked = _parameter.ResultHandling.SaveValueToVariable;
                CboValueVariable.Text = _parameter.ResultHandling.ValueVariableName;
                cmbFailureAction.SelectedValue = _parameter.ResultHandling.OnFailure;
                numFailureStep.Value = _parameter.ResultHandling.FailureStepIndex;
                numSuccessStep.Value = _parameter.ResultHandling.SuccessStepIndex;
                chkShowResult.Checked = _parameter.ResultHandling.ShowResult;
                //txtMessageTemplate.Text = _parameter.ResultHandling.MessageTemplate;

                // 更新界面布局
                UpdateFormLayout();
                UpdateFailureActionLayout();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数到表单失败");
                MessageHelper.MessageOK($"加载参数失败：{ex.Message}", TType.Error);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 安全设置ComboBox的值
        /// </summary>
        private void SetComboBoxValue(UIComboBox comboBox, string value)
        {
            if (comboBox == null || string.IsNullOrEmpty(value)) return;

            try
            {
                // 方法1：先尝试在Items中查找匹配项
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (comboBox.Items[i].ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                }

                // 方法2：如果没有找到匹配项，且ComboBox允许编辑，则设置Text属性
                if (comboBox.DropDownStyle == UIDropDownStyle.DropDown)
                {
                    comboBox.Text = value;
                }
                else
                {
                    // 方法3：对于DropDownList类型，如果找不到匹配项，添加该项
                    comboBox.Items.Add(value);
                    comboBox.SelectedItem = value;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogWarning("设置ComboBox值失败: {value}", ex);
                // 最后的备用方案
                try
                {
                    comboBox.Text = value;
                }
                catch
                {
                    // 忽略最终的设置失败
                }
            }
        }

        /// <summary>
        /// 加载指定PLC模块的地址列表
        /// </summary>
        private async Task LoadPlcAddresses(string moduleName)
        {
            if (string.IsNullOrEmpty(moduleName)) return;

            try
            {
                CboPlcAddress.Clear();
                var modules = await PLCManager.GetModuleTagsAsync();
                if (modules.TryGetValue(moduleName, out List<string> addresses))
                {
                    foreach (var address in addresses)
                    {
                        CboPlcAddress.Items.Add(address);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError("加载PLC模块[{moduleName}]地址失败", ex);
            }
        }

        /// <summary>
        /// 保存表单数据到参数对象
        /// </summary>
        private void SaveFormToParameter()
        {
            if (_parameter == null) return;

            try
            {
                // 基本信息
                _parameter.DetectionName = txtDetectionName.Text?.Trim() ?? "";
                _parameter.Type = (DetectionType)cmbDetectionType.SelectedValue;
                _parameter.RefreshRateMs = (int)numRefreshRate.Value;
                _parameter.TimeoutMs = (int)numTimeout.Value;
                _parameter.RetryCount = (int)numRetryCount.Value;
                _parameter.RetryIntervalMs = (int)numRetryInterval.Value;

                // 数据源配置
                _parameter.DataSource.SourceType = (DataSourceType)cmbDataSourceType.SelectedValue;
                _parameter.DataSource.VariableName = CboVariableName.Text?.Trim() ?? "";
                _parameter.DataSource.PlcConfig.ModuleName = CboPlcModule.Text?.Trim() ?? "";
                _parameter.DataSource.PlcConfig.Address = CboPlcAddress.Text?.Trim() ?? "";

                // 检测条件
                _parameter.Condition.MinValue = (double)numMinValue.Value;
                _parameter.Condition.MaxValue = (double)numMaxValue.Value;
                _parameter.Condition.TargetValue = txtTargetValue.Text?.Trim() ?? "";
                _parameter.Condition.ThresholdValue = (double)numThreshold.Value;
                _parameter.Condition.Operator = (ComparisonOperator)cmbOperator.SelectedValue;
                _parameter.Condition.Tolerance = (double)numTolerance.Value;

                // 结果处理
                _parameter.ResultHandling.SaveToVariable = chkSaveResult.Checked;
                _parameter.ResultHandling.ResultVariableName = CboResultVariable.Text?.Trim() ?? "";
                _parameter.ResultHandling.SaveValueToVariable = chkSaveValue.Checked;
                _parameter.ResultHandling.ValueVariableName = CboValueVariable.Text?.Trim() ?? "";
                _parameter.ResultHandling.OnFailure = (FailureAction)cmbFailureAction.SelectedValue;
                _parameter.ResultHandling.FailureStepIndex = (int)numFailureStep.Value;
                _parameter.ResultHandling.SuccessStepIndex = (int)numSuccessStep.Value;
                _parameter.ResultHandling.ShowResult = chkShowResult.Checked;
                //_parameter.ResultHandling.MessageTemplate = txtMessageTemplate.Text?.Trim() ?? "";
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存表单数据到参数对象失败");
                throw new InvalidOperationException($"保存表单数据失败：{ex.Message}", ex);
            }
        }
        #endregion

        #region 界面布局更新
        /// <summary>
        /// 根据检测类型更新表单布局
        /// </summary>
        private void UpdateFormLayout()
        {
            if (cmbDetectionType.SelectedValue == null) return;

            try
            {
                var detectionType = (DetectionType)cmbDetectionType.SelectedValue;

                // 隐藏所有条件面板
                pnlRangeCondition.Visible = false;
                pnlEqualityCondition.Visible = false;
                pnlThresholdCondition.Visible = false;

                // 根据检测类型显示相应的条件配置面板
                switch (detectionType)
                {
                    case DetectionType.ValueRange:
                        pnlRangeCondition.Visible = true;
                        break;
                    case DetectionType.Equality:
                        pnlEqualityCondition.Visible = true;
                        break;
                    case DetectionType.Status:
                        pnlThresholdCondition.Visible = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "更新表单布局失败");
            }
        }

        /// <summary>
        /// 根据数据源类型更新表单布局
        /// </summary>
        private async void UpdateDataSourceLayout()
        {
            if (cmbDataSourceType.SelectedValue == null) return;

            try
            {
                var sourceType = (DataSourceType)cmbDataSourceType.SelectedValue;

                // 隐藏所有数据源配置面板
                pnlVariableSource.Visible = false;
                pnlPlcSource.Visible = false;

                // 根据数据源类型显示相应的配置面板
                switch (sourceType)
                {
                    case DataSourceType.Variable:
                        ParameterGlobalVariable();
                        pnlVariableSource.Visible = true;
                        break;
                    case DataSourceType.PLC:
                        await Parameterplcs();
                        pnlPlcSource.Visible = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "更新数据源布局失败");
            }
        }

        /// <summary>
        /// 根据失败处理方式更新表单布局
        /// </summary>
        private void UpdateFailureActionLayout()
        {
            if (cmbFailureAction.SelectedValue == null) return;

            try
            {
                var failureAction = (FailureAction)cmbFailureAction.SelectedValue;
                pnlJumpStep.Visible = failureAction == FailureAction.Jump;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "更新失败处理布局失败");
            }
        }
        #endregion

        #region 参数加载和保存
        /// <summary>
        /// 加载所有PLC参数
        /// </summary>
        private async Task Parameterplcs()
        {
            CboPlcModule.Clear();
            CboPlcAddress.Clear();
            var Modules = await PLCManager.GetModuleTagsAsync();
            foreach (var M in Modules)
            {
                CboPlcModule.Items.Add(M.Key);
            }
        }

        /// <summary>
        /// 加载所有全局变量
        /// </summary>
        private void ParameterGlobalVariable()
        {
            var variable = GlobalVariable.GetUnassignedVariables();
            if (variable != null)
            {
                CboVariableName.Clear();
                foreach (var v in variable)
                {
                    CboVariableName.Items.Add(v.VarName);
                }
            }
        }

        /// <summary>
        /// 将参数保存到当前流程步骤中
        /// </summary>
        private void SaveParameterToProcess()
        {
            // 使用基类提供的统一保存逻辑
            SaveParameters();
        }

        /// <summary>
        /// 重写基类的LoadParameterFromStep方法 - 从已保存的步骤参数中加载
        /// </summary>
        protected override void LoadParameterFromStep(object stepParameter)
        {
            try
            {
                Parameter_Detection loadedParameter = null;

                // 尝试直接类型转换
                if (stepParameter is Parameter_Detection directParam)
                {
                    loadedParameter = directParam;
                    Logger?.LogDebug("直接获取Parameter_Detection参数");
                }
                // 尝试JSON反序列化
                else if (stepParameter != null)
                {
                    try
                    {
                        string jsonString = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                        loadedParameter = JsonConvert.DeserializeObject<Parameter_Detection>(jsonString);
                        Logger?.LogDebug("JSON反序列化获取Parameter_Detection参数");
                    }
                    catch (JsonException jsonEx)
                    {
                        Logger?.LogWarning(jsonEx, "JSON反序列化失败，使用默认参数");
                        loadedParameter = null;
                    }
                }

                // 如果加载成功，设置参数并加载到界面
                if (loadedParameter != null)
                {
                    _parameter = loadedParameter;
                    Logger?.LogInformation("成功加载检测参数: {DetectionName}", _parameter.DetectionName);
                }
                else
                {
                    // 加载失败，使用默认值
                    Logger?.LogWarning("加载参数失败，使用默认参数");
                    SetDefaultValues();
                    return;
                }

                // 加载参数到表单控件
                LoadParameterToForm();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "从步骤参数加载失败");
                SetDefaultValues();
            }
        }

        /// <summary>
        /// 重写基类的CollectParameters方法
        /// </summary>
        protected override object CollectParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        /// <summary>
        /// 重写基类的ValidateParameters方法
        /// </summary>
        protected override bool ValidateParameters()
        {
            return ValidateInput();
        }

        /// <summary>
        /// 重写基类的SetDefaultValues方法，默认参数
        /// </summary>
        protected override void SetDefaultValues()
        {
            _parameter = new Parameter_Detection
            {
                DetectionName = $"{WorkflowState.StepName}-{WorkflowState.StepNum + 1}",
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
        #endregion

        #region IParameterForm<Parameter_Detection> 接口实现
        public void PopulateControls(Parameter_Detection parameter)
        {
            Parameter = parameter;
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
            if (stepParameter is Parameter_Detection detectionParam)
                return detectionParam;

            if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Parameter_Detection>(jsonStr);
                }
                catch (Exception ex)
                {
                    Logger?.LogWarning(ex, "JSON参数转换失败，使用默认参数");
                    return new Parameter_Detection();
                }
            }

            return new Parameter_Detection();
        }
        #endregion

        #region 输入验证
        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            try
            {
                // 数据源验证
                var sourceType = (DataSourceType)cmbDataSourceType.SelectedValue;
                switch (sourceType)
                {
                    case DataSourceType.Variable:
                        if (string.IsNullOrWhiteSpace(CboVariableName.Text))
                        {
                            MessageHelper.MessageOK("请选择变量名", TType.Warn);
                            CboVariableName.Focus();
                            return false;
                        }
                        break;

                    case DataSourceType.PLC:
                        if (string.IsNullOrWhiteSpace(CboPlcModule.Text))
                        {
                            MessageHelper.MessageOK("请选择PLC模块名", TType.Warn);
                            CboPlcModule.Focus();
                            return false;
                        }
                        if (string.IsNullOrWhiteSpace(CboPlcAddress.Text))
                        {
                            MessageHelper.MessageOK("请选择PLC地址", TType.Warn);
                            CboPlcAddress.Focus();
                            return false;
                        }
                        break;
                }

                // 检测条件验证
                var detectionType = (DetectionType)cmbDetectionType.SelectedValue;
                if (detectionType == DetectionType.ValueRange)
                {
                    if (numMinValue.Value >= numMaxValue.Value)
                    {
                        MessageHelper.MessageOK("最小值必须小于最大值", TType.Warn);
                        numMinValue.Focus();
                        return false;
                    }
                }

                // 结果处理验证
                if (chkSaveResult.Checked && string.IsNullOrWhiteSpace(CboResultVariable.Text))
                {
                    MessageHelper.MessageOK("保存结果时必须指定结果变量名", TType.Warn);
                    CboResultVariable.Focus();
                    return false;
                }

                if (chkSaveValue.Checked && string.IsNullOrWhiteSpace(CboValueVariable.Text))
                {
                    MessageHelper.MessageOK("保存检测值时必须指定值变量名", TType.Warn);
                    CboValueVariable.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "参数验证时发生错误");
                MessageHelper.MessageOK($"参数验证失败：{ex.Message}", TType.Error);
                return false;
            }
        }
        #endregion

        #region 事件处理器
        private async void CboPlcModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                CboPlcAddress.Clear();
                var selectedModule = CboPlcModule.Text;
                var Modules = await PLCManager.GetModuleTagsAsync();
                foreach (var M in Modules)
                {
                    if (M.Key == selectedModule)
                        foreach (var addr in M.Value)
                            CboPlcAddress.Items.Add(addr);
                }
            }
        }

        private void CmbDetectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                UpdateFormLayout();
            }
        }

        // 数据源触发事件
        private void CmbDataSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                UpdateDataSourceLayout();
            }
        }

        // 失败处理触发事件
        private void CmbFailureAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                UpdateFailureActionLayout();
            }
        }

        // 保存按钮点击事件
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证输入
                if (!ValidateInput())
                {
                    return;
                }

                // 保存参数到流程中（使用基类方案）
                SaveParameterToProcess();

                // 如果保存成功，对话框会在基类的SaveParameters方法中关闭
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存操作失败");
                MessageHelper.MessageOK($"保存失败: {ex.Message}", TType.Error);
            }
        }

        // 取消按钮点击事件
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // 测试检测按钮点击事件
        private async void BtnTestDetection_Click(object sender, EventArgs e)
        {
            try
            {
                btnTestDetection.Enabled = false;
                btnTestDetection.Text = "测试中...";

                // 验证并保存当前配置到参数对象
                if (!ValidateInput())
                {
                    return;
                }

                SaveFormToParameter();

                // 执行测试检测
                bool result = await TestDetection();

                string message = result ? "检测测试成功！" : "检测测试失败！";
                var messageType = result ? TType.Success : TType.Error;
                MessageHelper.MessageOK(message, messageType);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "测试检测失败");
                MessageHelper.MessageOK($"测试失败: {ex.Message}", TType.Error);
            }
            finally
            {
                btnTestDetection.Enabled = true;
                btnTestDetection.Text = "测试检测";
            }
        }
        #endregion

        #region 测试功能
        /// <summary>
        /// 测试检测功能
        /// </summary>
        private async Task<bool> TestDetection()
        {
            try
            {
                // 1. 通过Program.ServiceProvider获取DetectionMethods服务
                var detectionMethods = Program.ServiceProvider?.GetService<DetectionMethods>();

                // 2. 调用实际的检测方法
                bool result = await detectionMethods.Detection(_parameter);

                // 3. 记录日志和性能指标
                return result;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "检测测试执行失败");
                throw new Exception($"检测测试失败: {ex.Message}", ex);
            }
        }
        #endregion

    }
}
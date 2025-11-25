using AntdUI;
using Google.Protobuf.WellKnownTypes;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sunny.UI;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 等待变量稳定配置窗体
    /// 用于监测变量或PLC地址的值，判断其是否达到稳定状态
    /// 支持：
    /// 1. 变量监测 - 监测全局变量的稳定性
    /// 2. PLC监测 - 监测PLC地址的稳定性
    /// 3. 灵活的稳定判据配置（阈值、采样间隔、连续次数）
    /// 4. 超时处理（继续/停止/跳转）
    /// 5. 稳定值自动赋值到变量
    /// </summary>
    public partial class Form_WaitForStable : BaseParameterForm
    {
        #region 属性

        private Parameter_WaitForStable _parameter;
        /// <summary>
        /// 参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_WaitForStable Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_WaitForStable();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }
        #endregion

        #region 私有字段

        /// <summary>
        /// 初始化状态标志 - 防止在窗体初始化过程中触发不必要的事件
        /// </summary>
        private bool _isInitializing = true;

        /// <summary>
        /// 未保存更改标志 - 跟踪用户是否做了未保存的修改
        /// </summary>
        private bool _hasUnsavedChanges = false;

        /// <summary>
        /// 验证定时器 - 延迟触发配置验证
        /// </summary>
        private System.Windows.Forms.Timer _validationTimer;

        #endregion

        #region 构造函数

        /// <summary>
        /// 设计器构造函数
        /// </summary>
        public Form_WaitForStable()
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
        public Form_WaitForStable(
            IWorkflowStateService workflowState,
            ILogger<Form_WaitForStable> logger)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// 带参数的依赖注入构造函数
        /// </summary>
        public Form_WaitForStable(
            IWorkflowStateService workflowState,
            ILogger<Form_WaitForStable> logger,
            Parameter_WaitForStable parameter)
            : base(workflowState, logger)
        {
            InitializeComponent();
            Parameter = parameter ?? new Parameter_WaitForStable();
            InitializeForm();
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

                // 初始化窗体样式
                InitializeFormStyle();

                // 初始化控件内容
                InitializeControlData();

                // 初始化定时器
                InitializeTimers();

                // 注册事件
                RegisterEvents();

                // 加载参数到界面
                //if (!IsLoading)
                {
                    LoadParameterFromWorkflowState();
                }

                Logger?.LogDebug("Form_WaitForStable 初始化完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化窗体时发生错误");
                MessageHelper.MessageOK(this, $"初始化失败: {ex.Message}", TType.Error);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 初始化窗体样式
        /// </summary>
        private void InitializeFormStyle()
        {
            this.Text = "等待变量稳定配置";
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;

            // 设置窗体颜色
            this.TitleColor = Color.FromArgb(65, 100, 204);
            this.RectColor = Color.FromArgb(65, 100, 204);
        }

        /// <summary>
        /// 初始化控件数据
        /// </summary>
        private void InitializeControlData()
        {
            // 加载监测源类型
            cmbMonitorSourceType.Items.Clear();
            cmbMonitorSourceType.Items.Add(new ComboItem { Text = "全局变量", Value = "Variable" });
            cmbMonitorSourceType.Items.Add(new ComboItem { Text = "PLC地址", Value = "PLC" });
            cmbMonitorSourceType.SelectedIndex = 0;

            // 加载可用变量到下拉框
            LoadAvailableVariables();

            // 初始化PLC模块列表
            LoadPlcModulesAsync();

            // 初始化超时动作下拉框
            InitializeTimeoutActionComboBox();

            // 设置默认值
            numStabilityThreshold.Value = 0.1m;
            numSamplingInterval.Value = 1;
            numStableCount.Value = 3;
            numTimeout.Value = 60;
        }

        /// <summary>
        /// 加载可用变量
        /// </summary>
        private void LoadAvailableVariables()
        {
            try
            {
                var variableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (variableManager == null)
                {
                    Logger?.LogWarning("无法获取 GlobalVariableManager 实例");
                    return;
                }

                var variables = variableManager.GetAllVariables()
                    .Where(v => !v.IsSystemVariable)
                    .Select(v => v.VarName)
                    .ToList();

                cmbMonitorVariable.Items.Clear();
                cmbMonitorVariable.Items.AddRange([.. variables]);

                cmbAssignToVariable.Items.Clear();
                cmbAssignToVariable.Items.Add(""); // 添加空选项表示不赋值
                cmbAssignToVariable.Items.AddRange([.. variables]);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载变量列表失败");
            }
        }

        /// <summary>
        /// 异步加载PLC模块列表
        /// </summary>
        private async void LoadPlcModulesAsync()
        {
            try
            {
                var plcManager = _plcManager ?? Program.ServiceProvider?.GetService<IPLCManager>();
                if (plcManager == null)
                {
                    Logger?.LogWarning("无法获取 PLCManager 实例");
                    return;
                }

                var modules = await plcManager.GetModuleTagsAsync();
                if (modules != null && modules.Count != 0)
                {
                    cmbPlcModule.Items.Clear();
                    cmbPlcModule.Items.AddRange([.. modules.Keys]);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC模块列表失败");
            }
        }

        /// <summary>
        /// 异步加载PLC地址列表
        /// </summary>
        private async Task LoadPlcAddresses(string moduleName)
        {
            try
            {
                var plcManager = _plcManager ?? Program.ServiceProvider?.GetService<IPLCManager>();
                if (plcManager == null || string.IsNullOrEmpty(moduleName))
                    return;

                var modules = await PLCManager.GetModuleTagsAsync();
                if (modules.TryGetValue(moduleName, out List<string> addresses))
                {
                    foreach (var address in addresses)
                    {
                        cmbPlcAddress.Items.Add(address);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC地址列表失败");
            }
        }

        /// <summary>
        /// 初始化超时动作下拉框
        /// </summary>
        private void InitializeTimeoutActionComboBox()
        {
            cmbTimeoutAction.Items.Clear();
            cmbTimeoutAction.Items.Add(new ComboItem
            {
                Text = "继续执行并记录日志",
                Value = TimeoutAction.ContinueAndLog
            });
            cmbTimeoutAction.Items.Add(new ComboItem
            {
                Text = "停止整个流程",
                Value = TimeoutAction.StopProcedure
            });
            cmbTimeoutAction.Items.Add(new ComboItem
            {
                Text = "跳转到指定步骤",
                Value = TimeoutAction.JumpToStep
            });

            cmbTimeoutAction.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitializeTimers()
        {
            // 验证定时器
            _validationTimer = new System.Windows.Forms.Timer
            {
                Interval = 500
            };
            _validationTimer.Tick += (s, e) =>
            {
                _validationTimer.Stop();
                ValidateConfigurationAsync();
            };
        }

        #endregion

        #region 事件注册

        /// <summary>
        /// 注册控件事件
        /// </summary>
        private void RegisterEvents()
        {
            // 文本变更事件
            txtDescription.TextChanged += (s, e) => OnParameterChanged();

            // 监测源类型变更
            cmbMonitorSourceType.SelectedIndexChanged += CmbMonitorSourceType_SelectedIndexChanged;

            // 变量/PLC选择变更
            cmbMonitorVariable.SelectedIndexChanged += (s, e) => OnParameterChanged();
            cmbPlcModule.SelectedIndexChanged += CmbPlcModule_SelectedIndexChanged;
            cmbPlcAddress.SelectedIndexChanged += (s, e) => OnParameterChanged();

            // 数值变更事件
            numStabilityThreshold.ValueChanged += (s, v) => OnParameterChanged();
            numSamplingInterval.ValueChanged += (s, v) => OnParameterChanged();
            numStableCount.ValueChanged += (s, v) => OnParameterChanged();
            numTimeout.ValueChanged += (s, v) => OnParameterChanged();

            // 赋值目标变更
            cmbAssignToVariable.SelectedIndexChanged += (s, e) => OnParameterChanged();

            // 超时动作变更
            cmbTimeoutAction.SelectedIndexChanged += CmbTimeoutAction_SelectedIndexChanged;
            numTimeoutJumpStep.ValueChanged += (s, v) => OnParameterChanged();

            // 按钮事件
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            btnTest.Click += BtnTest_Click;
            btnHelp.Click += BtnHelp_Click;
        }

        /// <summary>
        /// 监测源类型变更事件
        /// </summary>
        private void CmbMonitorSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            UpdateMonitorSourceVisibility();
            OnParameterChanged();
        }

        /// <summary>
        /// PLC模块选择变更事件
        /// </summary>
        private async void CmbPlcModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            var selectedModule = cmbPlcModule?.Text;
            if (!string.IsNullOrEmpty(selectedModule))
            {
                await LoadPlcAddresses(selectedModule);
            }

            OnParameterChanged();
        }

        /// <summary>
        /// 超时动作变更事件
        /// </summary>
        private void CmbTimeoutAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            UpdateTimeoutJumpStepVisibility();
            OnParameterChanged();
        }

        /// <summary>
        /// 参数变更事件 - 标记有未保存的修改
        /// </summary>
        private void OnParameterChanged()
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            RestartValidationTimer();
        }

        #endregion

        #region 界面更新方法

        /// <summary>
        /// 更新监测源显示
        /// </summary>
        private void UpdateMonitorSourceVisibility()
        {
            var selectedItem = cmbMonitorSourceType.SelectedItem as ComboItem;
            bool isVariable = selectedItem?.Value?.ToString() == "Variable";

            // 变量监测
            lblMonitorVariable.Visible = isVariable;
            cmbMonitorVariable.Visible = isVariable;

            // PLC监测
            lblPlcModule.Visible = !isVariable;
            cmbPlcModule.Visible = !isVariable;
            lblPlcAddress.Visible = !isVariable;
            cmbPlcAddress.Visible = !isVariable;
        }

        /// <summary>
        /// 更新超时跳转步骤号显示
        /// </summary>
        private void UpdateTimeoutJumpStepVisibility()
        {
            var selectedItem = cmbTimeoutAction.SelectedItem as ComboItem;
            bool showJumpStep = selectedItem?.Value is TimeoutAction action &&
                              action == TimeoutAction.JumpToStep;

            lblTimeoutJumpStep.Visible = showJumpStep;
            numTimeoutJumpStep.Visible = showJumpStep;
        }

        #endregion

        #region 参数加载和保存

        /// <summary>
        /// 确保PLC模块列表已加载
        /// </summary>
        private async Task EnsurePlcModulesLoaded()
        {
            // 如果列表为空，等待加载
            if (cmbPlcModule.Items.Count == 0)
            {
                try
                {
                    var plcManager = _plcManager ?? Program.ServiceProvider?.GetService<IPLCManager>();
                    if (plcManager != null)
                    {
                        var modules = await plcManager.GetModuleTagsAsync();
                        if (modules != null && modules.Count != 0)
                        {
                            cmbPlcModule.Items.Clear();
                            cmbPlcModule.Items.AddRange([.. modules.Keys]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "加载PLC模块列表失败");
                }
            }
        }

        private void LoadParameterFromWorkflowState()
        {
            try
            {
                if (!IsServiceAvailable)
                {
                    Logger?.LogWarning("服务不可用，无法加载PLC参数");
                    SetDefaultValues();
                    return;
                }

                var steps = _workflowState.GetSteps();
                int idx = _workflowState.StepNum;

                if (steps == null || idx < 0 || idx >= steps.Count)
                {
                    Logger?.LogWarning("步骤索引无效: Index={Index}, Count={Count}", idx, steps?.Count ?? 0);
                    SetDefaultValues();
                    return;
                }

                var currentStep = steps[idx];
                var paramObj = currentStep.StepParameter;

                // 解析参数
                if (paramObj is Parameter_WaitForStable directParam)
                {
                    Parameter = directParam;
                    Logger?.LogDebug("直接获取Parameter_WaitForStable参数");
                }
                else if (paramObj != null)
                {
                    try
                    {
                        string jsonString = paramObj is string s ? s : JsonConvert.SerializeObject(paramObj);
                        Parameter = JsonConvert.DeserializeObject<Parameter_WaitForStable>(jsonString);
                        Logger?.LogDebug("JSON反序列化获取Parameter_WritePLC参数");
                    }
                    catch (JsonException jsonEx)
                    {
                        Logger?.LogWarning(jsonEx, "JSON反序列化失败，使用默认参数");
                        Parameter = new Parameter_WaitForStable();
                    }
                }
                else
                {
                    Parameter = new Parameter_WaitForStable();
                    Logger?.LogDebug("参数为空，创建默认Parameter_WritePLC参数");
                }

                Parameter ??= new Parameter_WaitForStable();

                // 加载到界面
                LoadParameterToForm();

            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC写入参数时发生异常");
                Parameter = new Parameter_WaitForStable();
                MessageHelper.MessageOK($"加载PLC参数失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 验证方法

        /// <summary>
        /// 重启验证定时器
        /// </summary>
        private void RestartValidationTimer()
        {
            _validationTimer?.Stop();
            _validationTimer?.Start();
        }

        /// <summary>
        /// 异步验证配置
        /// </summary>
        private async void ValidateConfigurationAsync()
        {
            await Task.Run(() => ValidateConfiguration());
        }

        /// <summary>
        /// 验证配置
        /// </summary>
        private bool ValidateConfiguration()
        {
            try
            {
                var errors = new List<string>();

                // 验证监测源
                var selectedSourceType = cmbMonitorSourceType.SelectedItem as ComboItem;
                if (selectedSourceType?.Value?.ToString() == "Variable")
                {
                    if (string.IsNullOrWhiteSpace(cmbMonitorVariable.Text))
                    {
                        errors.Add("请选择监测变量");
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(cmbPlcModule.Text))
                    {
                        errors.Add("请选择PLC模块");
                    }
                    if (string.IsNullOrWhiteSpace(cmbPlcAddress.Text))
                    {
                        errors.Add("请选择PLC地址");
                    }
                }

                // 验证稳定判据
                if (numStabilityThreshold.Value < 0)
                {
                    errors.Add("稳定阈值不能为负数");
                }

                if (numSamplingInterval.Value < 1)
                {
                    errors.Add("采样间隔必须大于0秒");
                }

                if (numStableCount.Value < 1)
                {
                    errors.Add("连续稳定次数必须大于0");
                }

                // 验证超时配置
                var selectedAction = cmbTimeoutAction.SelectedItem as ComboItem;
                if (selectedAction?.Value is TimeoutAction action &&
                    action == TimeoutAction.JumpToStep)
                {
                    if (numTimeoutJumpStep.Value < 1)
                    {
                        errors.Add("跳转步骤号必须大于0");
                    }
                }

                // 更新验证状态（在UI线程）
                if (InvokeRequired)
                {
                    Invoke(new Action(() => UpdateValidationStatus(errors)));
                }
                else
                {
                    UpdateValidationStatus(errors);
                }

                return errors.Count == 0;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证配置时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 更新验证状态显示
        /// </summary>
        private void UpdateValidationStatus(List<string> errors)
        {
            if (errors.Count > 0)
            {
                // 可以在界面上显示错误提示
                lblValidationStatus.Text = "⚠ " + string.Join("; ", errors);
                lblValidationStatus.ForeColor = Color.FromArgb(220, 38, 38);
                lblValidationStatus.Visible = true;
            }
            else
            {
                lblValidationStatus.Text = "✓ 配置有效";
                lblValidationStatus.ForeColor = Color.FromArgb(34, 197, 94);
                lblValidationStatus.Visible = true;
            }
        }

        #endregion

        #region 按钮事件处理

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证输入
                if (!ValidateConfiguration())
                {
                    MessageHelper.MessageOK(this, "请检查并修正配置错误", TType.Warn);
                    return;
                }

                // 保存参数
                SaveFormToParameter();

                // 通知基类保存成功
                SaveParameters();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存配置时发生错误");
                MessageHelper.MessageOK(this, $"保存失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes(this, "配置已修改但未保存，确定要取消吗？");
                if (result != DialogResult.OK)
                {
                    return;
                }
            }

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
                // 验证配置
                if (!ValidateConfiguration())
                {
                    MessageHelper.MessageOK(this, "请先完成配置", TType.Warn);
                    return;
                }

                // 保存当前配置
                SaveFormToParameter();

                // 获取当前监测值
                double currentValue = 0;
                var selectedSourceType = cmbMonitorSourceType.SelectedItem as ComboItem;

                if (selectedSourceType?.Value?.ToString() == "Variable")
                {
                    // 从变量获取值
                    var variableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                    var variable = variableManager?.FindVariableByName(Parameter.MonitorVariable);
                    if (variable != null)
                    {
                        currentValue = Convert.ToDouble(variable.VarValue);
                    }
                }
                else
                {
                    // 从PLC获取值
                    var plcManager = _plcManager ?? Program.ServiceProvider?.GetService<IPLCManager>();
                    if (plcManager != null)
                    {
                        var plcValue = await Task.Run(() =>
                            plcManager.ReadPLCValueAsync(Parameter.PlcModuleName, Parameter.PlcAddress));
                        currentValue = Convert.ToDouble(plcValue);
                    }
                }

                var info = $"当前监测值: {currentValue:F4}\n\n" +
                          $"稳定判据:\n" +
                          $"- 阈值: {Parameter.StabilityThreshold:F4} (变化率)\n" +
                          $"- 采样间隔: {Parameter.SamplingInterval} 秒\n" +
                          $"- 连续次数: {Parameter.StableCount} 次\n\n" +
                          $"超时配置:\n" +
                          $"- 超时时间: {Parameter.TimeoutSeconds} 秒\n" +
                          $"- 超时动作: {cmbTimeoutAction.Text}";

                MessageHelper.MessageOK(this, info, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "测试配置时发生错误");
                MessageHelper.MessageOK(this, $"测试失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 帮助按钮点击事件
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            var helpText = @"等待变量稳定 - 使用说明

功能概述:
监测指定的变量或PLC地址的值，当其变化率满足稳定条件且持续指定次数后，认为已达到稳定状态。

配置说明:

1. 基本配置:
   - 步骤描述: 为此步骤指定一个说明
   - 监测源类型: 选择监测全局变量还是PLC地址

2. 监测源配置:
   - 全局变量: 选择要监测的数值类型变量
   - PLC地址: 选择PLC模块和地址

3. 稳定判据:
   - 稳定阈值: 变化率阈值（单位/秒）
     计算公式: |当前值 - 上次值| / 采样间隔 ≤ 阈值
   - 采样间隔: 每隔多少秒采样一次（建议1-5秒）
   - 连续稳定次数: 连续多少次采样满足条件才算真正稳定
     用于过滤偶然的波动

4. 超时配置:
   - 超时时间: 最长等待时间（秒），0表示无限等待
   - 超时动作: 
     * 继续执行并记录日志 - 超时后继续下一步
     * 停止整个流程 - 超时后终止执行
     * 跳转到指定步骤 - 超时后跳转到指定步骤号

5. 结果处理:
   - 赋值目标变量: 稳定后将当前值赋给此变量（可选）

使用场景:
- 等待气压下降至稳定后记录压力值
- 等待温度稳定后进行测量
- 等待流量稳定后计算流量值
- 监测PLC设备数值变化趋势

注意事项:
- 监测的变量或PLC地址必须是数值类型
- 采样间隔不宜过短，避免频繁采样
- 连续稳定次数建议3-5次
- 稳定阈值需根据实际场景调整
- 超时时间要考虑实际稳定所需时间";

            MessageHelper.MessageOK(this, helpText, TType.Info);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 设置ComboBox选中值
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
            catch (Exception)
            {
                Logger?.LogWarning("设置ComboBox值失败: {value}", value);
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
        #endregion


        #region 基类方法重写

        /// <summary>
        /// 加载参数到表单控件
        /// </summary>
        protected override async void LoadParameterToForm()
        {
            try
            {
                _isInitializing = true;

                // 基本信息
                txtDescription.Text = Parameter.Description ?? "";

                // 监测源配置
                if (Parameter.MonitorSourceType == MonitorSourceType.Variable)
                {
                    // 确保选中"全局变量"
                    SetComboBoxValue(cmbMonitorSourceType, "Variable");
                    cmbMonitorVariable.Text = Parameter.MonitorVariable ?? "";
                }
                else // MonitorSourceType.PLC
                {
                    SetComboBoxValue(cmbMonitorSourceType, "PLC");

                    // 先等待PLC模块列表加载完成
                    await EnsurePlcModulesLoaded();
                    cmbPlcModule.Text = Parameter.PlcModuleName ?? "";

                    // 如果有模块名，加载对应地址列表
                    if (!string.IsNullOrEmpty(Parameter.PlcModuleName))
                    {
                        await LoadPlcAddresses(Parameter.PlcModuleName);
                        cmbPlcAddress.Text = Parameter.PlcAddress ?? "";
                    }
                }

                // 更新界面显示
                UpdateMonitorSourceVisibility();

                // 稳定判据
                numStabilityThreshold.Value = (decimal)Parameter.StabilityThreshold;
                numSamplingInterval.Value = Parameter.SamplingInterval;
                numStableCount.Value = Parameter.StableCount;

                // 超时配置
                numTimeout.Value = Parameter.TimeoutSeconds;
                SetComboBoxValue(cmbTimeoutAction, Parameter.OnTimeout.ToString());
                numTimeoutJumpStep.Value = Parameter.TimeoutJumpToStep;

                // 结果处理
                cmbAssignToVariable.Text = Parameter.AssignToVariable ?? "";

                // 更新界面显示
                UpdateMonitorSourceVisibility();
                UpdateTimeoutJumpStepVisibility();

                _hasUnsavedChanges = false;

                Logger?.LogDebug("参数加载完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数时发生错误");
                MessageHelper.MessageOK(this, $"加载参数失败：{ex.Message}", TType.Error);
            }
            finally
            {
                _isInitializing = false;

                // 触发验证
                await Task.Delay(100).ContinueWith(_ => ValidateConfigurationAsync());
            }
        }

        /// <summary>
        /// 保存表单到参数
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                // 基本信息
                Parameter.Description = txtDescription.Text;

                // 监测源配置
                var selectedSourceType = cmbMonitorSourceType.SelectedItem as ComboItem;
                if (selectedSourceType?.Value?.ToString() == "Variable")
                {
                    Parameter.MonitorSourceType = MonitorSourceType.Variable;
                    Parameter.MonitorVariable = cmbMonitorVariable.Text;
                    Parameter.PlcModuleName = "";
                    Parameter.PlcAddress = "";
                }
                else
                {
                    Parameter.MonitorSourceType = MonitorSourceType.PLC;
                    Parameter.MonitorVariable = "";
                    Parameter.PlcModuleName = cmbPlcModule.Text;
                    Parameter.PlcAddress = cmbPlcAddress.Text;
                }

                // 稳定判据
                Parameter.StabilityThreshold = (double)numStabilityThreshold.Value;
                Parameter.SamplingInterval = (int)numSamplingInterval.Value;
                Parameter.StableCount = (int)numStableCount.Value;

                // 超时配置
                Parameter.TimeoutSeconds = (int)numTimeout.Value;
                var selectedAction = cmbTimeoutAction.SelectedItem as ComboItem;
                if (selectedAction?.Value is TimeoutAction action)
                {
                    Parameter.OnTimeout = action;
                }
                Parameter.TimeoutJumpToStep = (int)numTimeoutJumpStep.Value;

                // 结果处理
                Parameter.AssignToVariable = cmbAssignToVariable.Text;

                _hasUnsavedChanges = false;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存参数时发生错误");
                MessageHelper.MessageOK(this, $"保存参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 设置默认值（基类方法）
        /// </summary>
        protected override void SetDefaultValues()
        {
            Parameter = new Parameter_WaitForStable
            {
                Description = $"等待变量稳定步骤 {_workflowState?.StepNum + 1}",
                MonitorSourceType = MonitorSourceType.Variable,
                MonitorVariable = "",
                PlcModuleName = "",
                PlcAddress = "",
                StabilityThreshold = 0.1,
                SamplingInterval = 1,
                StableCount = 3,
                TimeoutSeconds = 60,
                OnTimeout = TimeoutAction.ContinueAndLog,
                TimeoutJumpToStep = -1,
                AssignToVariable = ""
            };

            Logger?.LogDebug("设置等待变量稳定参数默认值");
            LoadParameterToForm();
        }

        /// <summary>
        /// 验证输入（基类方法）
        /// </summary>
        protected override bool ValidateInput()
        {
            return ValidateConfiguration();
        }

        #endregion

        #region ComboItem 类

        /// <summary>
        /// ComboBox数据项
        /// </summary>
        private class ComboItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString() => Text;
        }

        #endregion
    }
}
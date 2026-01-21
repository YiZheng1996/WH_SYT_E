using AntdUI;
using MainUI.LogicalConfiguration.Helpers;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 实时监控提示配置窗体
    /// </summary>
    public partial class Form_RealtimeMonitorPromptConfig : BaseParameterForm
    {
        #region 属性
        private bool _isInitializing = true;

        private Parameter_RealtimeMonitorPrompt _parameter;
        /// <summary>
        /// 参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_RealtimeMonitorPrompt Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_RealtimeMonitorPrompt();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }
        #endregion

        public Form_RealtimeMonitorPromptConfig()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                InitializeForm();
            }
        }

        public Form_RealtimeMonitorPromptConfig(
            IWorkflowStateService workflowState,
            ILogger<Form_RealtimeMonitorPromptConfig> logger)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {

            // 初始化下拉框数据
            InitializeComboBoxes();

            // 加载变量列表
            LoadAvailableVariables();

            // 加载PLC模块
            _ = LoadPlcModulesAsync();

            // 加载参数
            LoadParameterFromWorkflowState();

            _isInitializing = false;
        }

        private void InitializeComboBoxes()
        {
            // 监测源类型
            cmbMonitorSourceType.Items.Clear();
            cmbMonitorSourceType.Items.AddRange(new[] { "全局变量", "PLC点位" });

            // 图标类型
            cmbIconType.Items.Clear();
            cmbIconType.Items.AddRange(new[] { "Info", "Success", "Warn", "Error" });
            cmbIconType.SelectedIndex = 0;
        }

        private void LoadAvailableVariables()
        {
            try
            {
                var variableManager = Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (variableManager != null)
                {
                    var variables = variableManager.GetAllVariables()
                        .Select(v => v.VarName)
                        .ToArray();

                    cmbMonitorVariable.Items.Clear();
                    cmbMonitorVariable.Items.AddRange(variables);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载变量列表失败");
            }
        }

        private async Task LoadPlcModulesAsync()
        {
            try
            {
                if (_plcManager != null)
                {
                    var modules = await _plcManager.GetModuleTagsAsync();
                    if (modules != null)
                    {
                        cmbPlcModule.Items.Clear();
                        cmbPlcModule.Items.AddRange(modules.Keys.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC模块列表失败");
            }
        }

        /// <summary>
        /// 设置ComboBox选中值(支持通过Value或Text匹配)
        /// </summary>
        private void SetComboBoxValue(UIComboBox comboBox, string value)
        {
            if (comboBox == null || string.IsNullOrEmpty(value)) return;

            try
            {
                // 优先通过 ComboItem.Value 查找匹配项
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (comboBox.Items[i] is ComboItem item)
                    {
                        // 先比较 Value
                        if (item.Value?.ToString().Equals(value, StringComparison.OrdinalIgnoreCase) == true)
                        {
                            comboBox.SelectedIndex = i;
                            return;
                        }
                        // 再比较 Text
                        if (item.Text?.Equals(value, StringComparison.OrdinalIgnoreCase) == true)
                        {
                            comboBox.SelectedIndex = i;
                            return;
                        }
                    }
                    // 兼容普通字符串项
                    else if (comboBox.Items[i]?.ToString().Equals(value, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                }

                // 如果没有找到匹配项,且ComboBox允许编辑,则设置Text属性
                if (comboBox.DropDownStyle == UIDropDownStyle.DropDown)
                {
                    comboBox.Text = value;
                }
                else
                {
                    Logger?.LogWarning($"ComboBox中找不到匹配项: {value}");
                }
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(ex, $"设置ComboBox值失败: {value}");
            }
        }

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
                    if (_plcManager != null)
                    {
                        var modules = await _plcManager.GetModuleTagsAsync();
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

        /// <summary>
        /// 异步加载PLC地址列表
        /// </summary>
        private async Task LoadPlcAddresses(string moduleName)
        {
            try
            {
                 
                if (_plcManager == null || string.IsNullOrEmpty(moduleName))
                    return;

                var modules = await PLCManager.GetModuleTagsAsync();
                if (modules.TryGetValue(moduleName, out List<string> addresses))
                {
                    cmbPlcAddress.Items.Clear();
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

        private async void CmbPlcModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            try
            {
                string moduleName = cmbPlcModule.Text;
                if (string.IsNullOrEmpty(moduleName)) return;
                var modules = await PLCManager.GetModuleTagsAsync();
                if (modules.TryGetValue(moduleName, out List<string> addresses))
                {
                    cmbPlcAddress.Items.Clear();
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

        private void CmbMonitorSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMonitorSourceVisibility();
        }

        private void UpdateMonitorSourceVisibility()
        {
            bool isVariable = cmbMonitorSourceType.SelectedIndex == 0;
            pnlVariableSource.Visible = isVariable;
            pnlPlcSource.Visible = !isVariable;
        }

        
        private void LoadParameterFromWorkflowState()
        {
            try
            {
                if (!IsServiceAvailable) return;

                var steps = _workflowState.GetSteps();
                int idx = _workflowState.StepNum;

                if (steps == null || idx < 0 || idx >= steps.Count) return;

                var currentStep = steps[idx];
                var paramObj = currentStep.StepParameter;

                if (paramObj is Parameter_RealtimeMonitorPrompt directParam)
                {
                    Parameter = directParam;
                }
                else if (paramObj != null)
                {
                    string jsonString = paramObj is string s ? s : JsonConvert.SerializeObject(paramObj);
                    Parameter = JsonConvert.DeserializeObject<Parameter_RealtimeMonitorPrompt>(jsonString);
                }
                else
                {
                    Parameter = new Parameter_RealtimeMonitorPrompt();
                }

                Parameter ??= new Parameter_RealtimeMonitorPrompt();
                LoadParameterToForm();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数失败");
                Parameter = new Parameter_RealtimeMonitorPrompt();
            }
        }


        private void BtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFormToParameter();

                var variableManager = Program.ServiceProvider?.GetService<GlobalVariableManager>();
                using var dialog = new Form_RealtimeMonitorPrompt(
                    Parameter,
                    variableManager,
                    _plcManager);

                dialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(this, $"测试失败: {ex.Message}", TType.Error);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateConfiguration())
            {
                DialogResult = DialogResult.None;
                return;
            }

            SaveFormToParameter();
            SaveParameters();
        }

        private bool ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageHelper.MessageOK(this, "请输入窗体标题", TType.Warn);
                return false;
            }

            if (cmbMonitorSourceType.SelectedIndex == 0)
            {
                if (string.IsNullOrWhiteSpace(cmbMonitorVariable.Text))
                {
                    MessageHelper.MessageOK(this, "请选择监测变量", TType.Warn);
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(cmbPlcModule.Text))
                {
                    MessageHelper.MessageOK(this, "请选择PLC模块", TType.Warn);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(cmbPlcAddress.Text))
                {
                    MessageHelper.MessageOK(this, "请选择PLC地址", TType.Warn);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtPromptMessage.Text))
            {
                MessageHelper.MessageOK(this, "请输入提示信息", TType.Warn);
                return false;
            }

            return true;
        }

        #region 重写基类方法
        /// <summary>
        /// 加载参数
        /// </summary>
        protected override async void LoadParameterToForm()
        {
            _isInitializing = true;

            txtTitle.Text = Parameter.Title;
            txtDescription.Text = Parameter.Description;
            txtPromptMessage.Text = Parameter.PromptMessage;
            txtUnit.Text = Parameter.Unit;
            txtDisplayFormat.Text = Parameter.DisplayFormat;
            numRefreshInterval.Value = Parameter.RefreshInterval;
            txtButtonText.Text = Parameter.ButtonText;
            txtValueLabelText.Text = Parameter.ValueLabelText;
            chkShowValueLabel.Checked = Parameter.ShowValueLabel;

            cmbMonitorSourceType.SelectedIndex = Parameter.MonitorSourceType == MonitorSourceType.Variable ? 0 : 1;
            cmbMonitorVariable.Text = Parameter.MonitorVariable;
            cmbPlcModule.Text = Parameter.PlcModuleName;
            cmbPlcAddress.Text = Parameter.PlcAddress;

            cmbIconType.SelectedIndex = (int)Parameter.IconType;

            UpdateMonitorSourceVisibility();

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

            _isInitializing = false;
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        protected override void SaveFormToParameter()
        {
            Parameter.Title = txtTitle.Text;
            Parameter.Description = txtDescription.Text;
            Parameter.PromptMessage = txtPromptMessage.Text;
            Parameter.MonitorSourceType = cmbMonitorSourceType.SelectedIndex == 0
                ? MonitorSourceType.Variable
                : MonitorSourceType.PLC;
            Parameter.MonitorVariable = cmbMonitorVariable.Text;
            Parameter.PlcModuleName = cmbPlcModule.Text;
            Parameter.PlcAddress = cmbPlcAddress.Text;
            Parameter.Unit = txtUnit.Text;
            Parameter.DisplayFormat = txtDisplayFormat.Text;
            Parameter.RefreshInterval = numRefreshInterval.Value;
            Parameter.ButtonText = txtButtonText.Text;
            Parameter.IconType = (TType)cmbIconType.SelectedIndex;
            Parameter.ValueLabelText = txtValueLabelText.Text;
            Parameter.ShowValueLabel = chkShowValueLabel.Checked;

            // 规范化监测变量名
            //var monitorVarName = cmbMonitorVariable.Text?.Trim();
            //Parameter.MonitorVariable = VariableNameHelper.NormalizeVariableName(monitorVarName) ?? monitorVarName;
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
using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Panel = System.Windows.Forms.Panel;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 实时监控提示配置窗体
    /// </summary>
    public partial class Form_RealtimeMonitorPromptConfig : BaseParameterForm, IParameterForm<Parameter_RealtimeMonitorPrompt>
    {
        private Parameter_RealtimeMonitorPrompt _parameter;
        private bool _isInitializing = true;
        private IPLCManager _plcManager;

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

        private void InitializeComponent2()
        {
            //Size = new Size(700, 650);
            Text = "实时监控提示配置";

            int yPos = 20;
            int labelWidth = 120;
            int controlWidth = 500;

            // 标题
            AddLabel("窗体标题:", 20, yPos, labelWidth);
            txtTitle = AddTextBox(140, yPos, controlWidth);
            yPos += 40;

            // 描述
            AddLabel("步骤描述:", 20, yPos, labelWidth);
            txtDescription = AddTextBox(140, yPos, controlWidth);
            yPos += 40;

            // 监测源类型
            AddLabel("*监测源类型:", 20, yPos, labelWidth);
            cmbMonitorSourceType = AddComboBox(140, yPos, 200);
            cmbMonitorSourceType.Items.AddRange(["全局变量", "PLC点位"]);
            cmbMonitorSourceType.SelectedIndexChanged += CmbMonitorSourceType_SelectedIndexChanged;
            yPos += 40;

            // 变量监测面板
            pnlVariableSource = new Panel
            {
                Location = new Point(20, yPos),
                Size = new Size(650, 40),
                Visible = false
            };
            AddLabel("*监测变量:", 0, 0, labelWidth, pnlVariableSource);
            cmbMonitorVariable = AddComboBox(120, 0, 300, pnlVariableSource);
            Controls.Add(pnlVariableSource);

            // PLC监测面板
            pnlPlcSource = new Panel
            {
                Location = new Point(20, yPos),
                Size = new Size(650, 80),
                Visible = false
            };
            AddLabel("*PLC模块:", 0, 0, labelWidth, pnlPlcSource);
            cmbPlcModule = AddComboBox(120, 0, 300, pnlPlcSource);
            cmbPlcModule.SelectedIndexChanged += CmbPlcModule_SelectedIndexChanged;

            AddLabel("*PLC地址:", 0, 40, labelWidth, pnlPlcSource);
            cmbPlcAddress = AddComboBox(120, 40, 300, pnlPlcSource);
            Controls.Add(pnlPlcSource);

            yPos += 90;

            // 提示信息
            AddLabel("*提示信息:", 20, yPos, labelWidth);
            yPos += 25;
            txtPromptMessage = new UIRichTextBox
            {
                Location = new Point(20, yPos),
                Size = new Size(650, 80),
                Multiline = true,
                //ShowScrollBar = true
            };
            Controls.Add(txtPromptMessage);
            yPos += 90;

            // 数值单位
            AddLabel("数值单位:", 20, yPos, labelWidth);
            txtUnit = AddTextBox(140, yPos, 200);
            //txtUnit.PlaceholderText = "如: kPa, ℃, MPa";
            yPos += 40;

            // 显示格式
            AddLabel("显示格式:", 20, yPos, labelWidth);
            txtDisplayFormat = AddTextBox(140, yPos, 200);
            txtDisplayFormat.Text = "F1";
            //txtDisplayFormat.PlaceholderText = "F1=1位小数, F2=2位小数";
            yPos += 40;

            // 数值标签
            chkShowValueLabel = new UICheckBox
            {
                Location = new Point(20, yPos),
                Text = "显示数值标签",
                Checked = true
            };
            Controls.Add(chkShowValueLabel);
            yPos += 30;

            AddLabel("数值标签文本:", 20, yPos, labelWidth);
            txtValueLabelText = AddTextBox(140, yPos, 300);
            //txtValueLabelText.PlaceholderText = "如: PE05(kPa)";
            yPos += 40;

            // 刷新间隔
            AddLabel("刷新间隔(ms):", 20, yPos, labelWidth);
            numRefreshInterval = new Sunny.UI.UIIntegerUpDown
            {
                Location = new Point(140, yPos),
                Size = new Size(200, 30),
                Minimum = 100,
                Maximum = 5000,
                Value = 500
            };
            Controls.Add(numRefreshInterval);
            yPos += 40;

            // 按钮文本
            AddLabel("按钮文本:", 20, yPos, labelWidth);
            txtButtonText = AddTextBox(140, yPos, 200);
            txtButtonText.Text = "确定";
            yPos += 40;

            // 窗体图标
            AddLabel("窗体图标:", 20, yPos, labelWidth);
            cmbIconType = AddComboBox(140, yPos, 200);
            cmbIconType.Items.AddRange(["Info", "Success", "Warn", "Error"]);
            cmbIconType.SelectedIndex = 0;
            yPos += 50;

            // 底部按钮
            btnTest = new UIButton
            {
                Location = new Point(350, yPos),
                Size = new Size(80, 35),
                Text = "测试"
            };
            btnTest.Click += BtnTest_Click;
            Controls.Add(btnTest);

            btnOK = new UIButton
            {
                Location = new Point(450, yPos),
                Size = new Size(80, 35),
                Text = "确定",
                DialogResult = DialogResult.OK
            };
            btnOK.Click += BtnOK_Click;
            Controls.Add(btnOK);

            btnCancel = new UIButton
            {
                Location = new Point(550, yPos),
                Size = new Size(80, 35),
                Text = "取消",
                DialogResult = DialogResult.Cancel
            };
            Controls.Add(btnCancel);
        }

        private void InitializeForm()
        {
            _plcManager = Program.ServiceProvider?.GetService<IPLCManager>();

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

        private async void CmbPlcModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            try
            {
                string moduleName = cmbPlcModule.Text;
                if (string.IsNullOrEmpty(moduleName)) return;

                var addresses = await _plcManager.GetModuleTagsAsync(moduleName);
                if (addresses != null)
                {
                    cmbPlcAddress.Items.Clear();
                    cmbPlcAddress.Items.AddRange(addresses.ToArray());
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
                    _parameter = directParam;
                }
                else if (paramObj != null)
                {
                    string jsonString = paramObj is string s ? s : JsonConvert.SerializeObject(paramObj);
                    _parameter = JsonConvert.DeserializeObject<Parameter_RealtimeMonitorPrompt>(jsonString);
                }
                else
                {
                    _parameter = new Parameter_RealtimeMonitorPrompt();
                }

                _parameter ??= new Parameter_RealtimeMonitorPrompt();
                LoadParameterToForm();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数失败");
                _parameter = new Parameter_RealtimeMonitorPrompt();
            }
        }

        private void LoadParameterToForm()
        {
            _isInitializing = true;

            txtTitle.Text = _parameter.Title;
            txtDescription.Text = _parameter.Description;
            txtPromptMessage.Text = _parameter.PromptMessage;
            txtUnit.Text = _parameter.Unit;
            txtDisplayFormat.Text = _parameter.DisplayFormat;
            numRefreshInterval.Value = _parameter.RefreshInterval;
            txtButtonText.Text = _parameter.ButtonText;
            txtValueLabelText.Text = _parameter.ValueLabelText;
            chkShowValueLabel.Checked = _parameter.ShowValueLabel;

            cmbMonitorSourceType.SelectedIndex = _parameter.MonitorSourceType == MonitorSourceType.Variable ? 0 : 1;
            cmbMonitorVariable.Text = _parameter.MonitorVariable;
            cmbPlcModule.Text = _parameter.PlcModuleName;
            cmbPlcAddress.Text = _parameter.PlcAddress;

            cmbIconType.SelectedIndex = (int)_parameter.IconType;

            UpdateMonitorSourceVisibility();

            _isInitializing = false;
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFormToParameter();

                var variableManager = Program.ServiceProvider?.GetService<GlobalVariableManager>();
                using var dialog = new Form_RealtimeMonitorPrompt(
                    _parameter,
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

        private void SaveFormToParameter()
        {
            _parameter.Title = txtTitle.Text;
            _parameter.Description = txtDescription.Text;
            _parameter.PromptMessage = txtPromptMessage.Text;
            _parameter.MonitorSourceType = cmbMonitorSourceType.SelectedIndex == 0
                ? MonitorSourceType.Variable
                : MonitorSourceType.PLC;
            _parameter.MonitorVariable = cmbMonitorVariable.Text;
            _parameter.PlcModuleName = cmbPlcModule.Text;
            _parameter.PlcAddress = cmbPlcAddress.Text;
            _parameter.Unit = txtUnit.Text;
            _parameter.DisplayFormat = txtDisplayFormat.Text;
            _parameter.RefreshInterval = numRefreshInterval.Value;
            _parameter.ButtonText = txtButtonText.Text;
            _parameter.IconType = (TType)cmbIconType.SelectedIndex;
            _parameter.ValueLabelText = txtValueLabelText.Text;
            _parameter.ShowValueLabel = chkShowValueLabel.Checked;
        }

        #region IParameterForm 实现

        public void PopulateControls(Parameter_RealtimeMonitorPrompt parameter) => Parameter = parameter;

        void IParameterForm<Parameter_RealtimeMonitorPrompt>.SetDefaultValues() => SetDefaultValues();

        public bool ValidateTypedParameters() => ValidateConfiguration();

        public Parameter_RealtimeMonitorPrompt CollectTypedParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        public Parameter_RealtimeMonitorPrompt ConvertParameter(object stepParameter)
        {
            if (stepParameter is Parameter_RealtimeMonitorPrompt param)
                return param;

            if (stepParameter is string json && !string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<Parameter_RealtimeMonitorPrompt>(json);
            }

            return new Parameter_RealtimeMonitorPrompt();
        }

        protected override void SetDefaultValues()
        {
            _parameter = new Parameter_RealtimeMonitorPrompt
            {
                Title = "实时监控",
                Description = "实时监控提示",
                PromptMessage = "请根据提示进行操作",
                RefreshInterval = 500,
                ButtonText = "确定",
                DisplayFormat = "F1",
                ShowValueLabel = true
            };
            LoadParameterToForm();
        }

        protected override object CollectParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        #endregion

        #region 辅助方法

        private System.Windows.Forms.Label AddLabel(string text, int x, int y, int width, Control parent = null)
        {
            var label = new System.Windows.Forms.Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            (parent ?? this).Controls.Add(label);
            return label;
        }

        private UITextBox AddTextBox(int x, int y, int width, Control parent = null)
        {
            var textBox = new UITextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 30)
            };
            (parent ?? this).Controls.Add(textBox);
            return textBox;
        }

        private UIComboBox AddComboBox(int x, int y, int width, Control parent = null)
        {
            var comboBox = new UIComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 30),
                DropDownStyle = UIDropDownStyle.DropDownList
            };
            (parent ?? this).Controls.Add(comboBox);
            return comboBox;
        }

        #endregion
    }
}
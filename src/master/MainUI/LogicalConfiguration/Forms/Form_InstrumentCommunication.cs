using MainUI.LogicalConfiguration.Instrument.Models;
using MainUI.LogicalConfiguration.Instrument.Services;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Forms
{
    public partial class Form_InstrumentCommunication : BaseParameterForm
    {
        private Parameter_InstrumentCommunication _parameter;
        private readonly IInstrumentDriverService _driverService;
        private readonly GlobalVariableManager _globalVariable;
        private List<InstrumentDriver> _drivers;
        private InstrumentDriver _selectedDriver;
        private InstrumentCommand _selectedCommand;
        private readonly Dictionary<string, Control> _paramControls = new();

        public Form_InstrumentCommunication(
            IWorkflowStateService workflowState,
            GlobalVariableManager globalVariable,
            IInstrumentDriverService driverService,
            ILogger<Form_InstrumentCommunication> logger)
            : base(workflowState, logger)
        {
            _globalVariable = globalVariable ?? throw new ArgumentNullException(nameof(globalVariable));
            _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
            _parameter = new Parameter_InstrumentCommunication();

            InitializeComponent();
            InitializeFormData();
            BindEvents();
            _ = LoadDriversAsync();
        }

        private void InitializeFormData()
        {
            foreach (DataType dt in Enum.GetValues(typeof(DataType)))
                cboCustomDataType.Items.Add(dt);
            cboCustomDataType.SelectedItem = DataType.String;

            foreach (FailureStrategy fs in Enum.GetValues(typeof(FailureStrategy)))
                cboFailureStrategy.Items.Add(fs);
            cboFailureStrategy.SelectedItem = FailureStrategy.Abort;

            InitializeParseRulesGrid();
        }

        private void InitializeParseRulesGrid()
        {
            dgvParseRules.Columns.Clear();
            dgvParseRules.Columns.Add("Name", "规则名称");
            dgvParseRules.Columns.Add("TargetVariable", "目标变量");

            var parseTypeColumn = new DataGridViewComboBoxColumn
            {
                Name = "ParseType",
                HeaderText = "解析方式",
                DataPropertyName = "ParseType"
            };
            parseTypeColumn.Items.AddRange("Position", "Delimiter", "Regex", "Json");
            dgvParseRules.Columns.Add(parseTypeColumn);
            dgvParseRules.Columns.Add("Pattern", "解析参数");
        }

        private void BindEvents()
        {
            cboInstrument.SelectedIndexChanged += CboInstrument_SelectedIndexChanged;
            cboCommand.SelectedIndexChanged += CboCommand_SelectedIndexChanged;
            chkCustomCommand.CheckedChanged += ChkCustomCommand_CheckedChanged;
            chkOverrideTimeout.CheckedChanged += ChkOverrideTimeout_CheckedChanged;
            cboFailureStrategy.SelectedIndexChanged += CboFailureStrategy_SelectedIndexChanged;
            btnManageDrivers.Click += BtnManageDrivers_Click;
            btnTestConnection.Click += BtnTestConnection_Click;
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private async Task LoadDriversAsync()
        {
            try
            {
                _drivers = await _driverService.GetAllDriversAsync();
                if (this.InvokeRequired)
                    this.BeginInvoke(new Action(PopulateInstrumentComboBox));
                else
                    PopulateInstrumentComboBox();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载仪器驱动列表失败");
            }
        }

        private void PopulateInstrumentComboBox()
        {
            cboInstrument.Items.Clear();
            foreach (var driver in _drivers)
            {
                cboInstrument.Items.Add(new ComboBoxItem
                {
                    Text = $"{driver.DisplayName} ({driver.Category})",
                    Value = driver
                });
            }

            if (string.IsNullOrEmpty(_parameter?.DriverId)) return;

            var item = cboInstrument.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(i => ((InstrumentDriver)i.Value).DriverId == _parameter.DriverId);
            if (item != null) cboInstrument.SelectedItem = item;
        }

        private void LoadCommands()
        {
            cboCommand.Items.Clear();
            _paramControls.Clear();
            flowParams.Controls.Clear();

            if (_selectedDriver == null) return;

            foreach (var cmd in _selectedDriver.Commands.OrderBy(c => c.SortOrder))
            {
                cboCommand.Items.Add(new ComboBoxItem
                {
                    Text = $"{cmd.DisplayName} ({cmd.CommandType})",
                    Value = cmd
                });
            }

            if (string.IsNullOrEmpty(_parameter?.CommandId)) return;
            var item = cboCommand.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(i => ((InstrumentCommand)i.Value).CommandId == _parameter.CommandId);
            if (item != null) cboCommand.SelectedItem = item;
        }

        private void LoadCommandParameters()
        {
            _paramControls.Clear();
            flowParams.Controls.Clear();
            if (_selectedCommand == null) return;

            foreach (var param in _selectedCommand.Parameters)
            {
                var panel = new Panel { Width = flowParams.Width - 30, Height = 35, Margin = new Padding(0, 0, 0, 5) };
                var label = new Label
                {
                    Text = $"{param.DisplayName}:",
                    Width = 120,
                    TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                    Location = new System.Drawing.Point(0, 5)
                };

                Control inputControl;
                if (param.Options?.Count > 0)
                {
                    var combo = new UIComboBox
                    {
                        Width = 200,
                        Location = new System.Drawing.Point(125, 0),
                        DropDownStyle = UIDropDownStyle.DropDown
                    };
                    foreach (var opt in param.Options) combo.Items.Add(opt);
                    combo.Text = param.DefaultValue;
                    inputControl = combo;
                }
                else
                {
                    inputControl = new UITextBox
                    {
                        Width = 200,
                        Location = new System.Drawing.Point(125, 0),
                        Text = param.DefaultValue,
                        Watermark = param.Description
                    };
                }

                if (_parameter?.CommandParameters?.ContainsKey(param.Name) == true)
                {
                    if (inputControl is UIComboBox combo)
                        combo.Text = _parameter.CommandParameters[param.Name];
                    else if (inputControl is UITextBox textBox)
                        textBox.Text = _parameter.CommandParameters[param.Name];
                }

                _paramControls[param.Name] = inputControl;
                panel.Controls.Add(label);
                panel.Controls.Add(inputControl);
                flowParams.Controls.Add(panel);
            }
        }

        private void CboInstrument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInstrument.SelectedItem is ComboBoxItem item)
            {
                _selectedDriver = item.Value as InstrumentDriver;
                LoadCommands();
            }
        }

        private void CboCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCommand.SelectedItem is ComboBoxItem item)
            {
                _selectedCommand = item.Value as InstrumentCommand;
                LoadCommandParameters();
            }
        }

        private void ChkCustomCommand_CheckedChanged(object sender, EventArgs e)
        {
            var useCustom = chkCustomCommand.Checked;
            txtCustomCommand.Enabled = useCustom;
            cboCustomDataType.Enabled = useCustom;
            cboCommand.Enabled = !useCustom;
            panelCommandParams.Enabled = !useCustom;
        }

        private void ChkOverrideTimeout_CheckedChanged(object sender, EventArgs e)
        {
            txtTimeout.Enabled = chkOverrideTimeout.Checked;
        }

        private void CboFailureStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtJumpStep.Enabled = cboFailureStrategy.SelectedItem is FailureStrategy fs && fs == FailureStrategy.JumpToStep;
        }

        private async void BtnTestConnection_Click(object sender, EventArgs e)
        {
            if (_selectedDriver == null)
            {
                UIMessageTip.ShowWarning("请先选择仪器");
                return;
            }

            btnTestConnection.Enabled = false;
            btnTestConnection.Text = "测试中...";

            try
            {
                var factory = new Instrument.Communication.CommunicationProviderFactory();
                var provider = factory.CreateProvider(_selectedDriver.ProtocolType);
                var connected = await provider.ConnectAsync(_selectedDriver.GetProtocolConfig());
                await provider.DisconnectAsync();
                provider.Dispose();

                if (connected)
                    UIMessageTip.ShowOk("连接测试成功！");
                else
                    UIMessageTip.ShowError("连接测试失败");
            }
            catch (Exception ex)
            {
                UIMessageTip.ShowError($"测试异常: {ex.Message}");
            }
            finally
            {
                btnTestConnection.Enabled = true;
                btnTestConnection.Text = "测试";
            }
        }

        private void BtnManageDrivers_Click(object sender, EventArgs e)
        {
            using var managerForm = new FrmInstrumentDriverManager(_driverService, Logger as ILogger<FrmInstrumentDriverManager>);
            if (managerForm.ShowDialog() == DialogResult.OK)
                _ = LoadDriversAsync();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            SaveFormToParameter();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void LoadParameterToForm()
        {
            if (_parameter == null) return;

            txtDescription.Text = _parameter.Description;
            chkCustomCommand.Checked = _parameter.UseCustomCommand;
            txtCustomCommand.Text = _parameter.CustomCommand;
            if (Enum.TryParse<DataType>(_parameter.CustomCommandDataType.ToString(), out var dt))
                cboCustomDataType.SelectedItem = dt;

            txtResponseVariable.Text = _parameter.ResponseVariable;
            txtStatusVariable.Text = _parameter.StatusVariable;
            txtErrorVariable.Text = _parameter.ErrorVariable;
            chkOverrideTimeout.Checked = _parameter.OverrideTimeout;
            txtTimeout.Text = _parameter.CustomTimeout.ToString();
            txtRetryCount.Text = _parameter.RetryCount.ToString();
            txtRetryInterval.Text = _parameter.RetryInterval.ToString();
            cboFailureStrategy.SelectedItem = _parameter.FailureStrategy;
            txtJumpStep.Text = _parameter.JumpToStepNumber.ToString();
            txtDelayBefore.Text = _parameter.DelayBeforeSend.ToString();
            txtDelayAfter.Text = _parameter.DelayAfterSend.ToString();
            chkEnableLogging.Checked = _parameter.EnableLogging;
            txtExecuteCondition.Text = _parameter.ExecuteCondition;
        }

        protected override void SaveFormToParameter()
        {
            _parameter.Description = txtDescription.Text;

            if (cboInstrument.SelectedItem is ComboBoxItem instrumentItem)
            {
                var driver = instrumentItem.Value as InstrumentDriver;
                _parameter.DriverId = driver.DriverId;
                _parameter.InstrumentName = driver.DisplayName;
            }

            if (cboCommand.SelectedItem is ComboBoxItem commandItem)
            {
                var command = commandItem.Value as InstrumentCommand;
                _parameter.CommandId = command.CommandId;
                _parameter.CommandName = command.DisplayName;
            }

            _parameter.UseCustomCommand = chkCustomCommand.Checked;
            _parameter.CustomCommand = txtCustomCommand.Text;
            _parameter.CustomCommandDataType = cboCustomDataType.SelectedItem is DataType dataType ? dataType : DataType.String;

            _parameter.CommandParameters.Clear();
            foreach (var kvp in _paramControls)
            {
                var value = kvp.Value switch
                {
                    UIComboBox combo => combo.Text,
                    UITextBox textBox => textBox.Text,
                    _ => ""
                };
                _parameter.CommandParameters[kvp.Key] = value;
            }

            _parameter.ResponseVariable = txtResponseVariable.Text;
            _parameter.StatusVariable = txtStatusVariable.Text;
            _parameter.ErrorVariable = txtErrorVariable.Text;
            _parameter.OverrideTimeout = chkOverrideTimeout.Checked;
            int.TryParse(txtTimeout.Text, out var timeout);
            _parameter.CustomTimeout = timeout;
            int.TryParse(txtRetryCount.Text, out var retryCount);
            _parameter.RetryCount = retryCount;
            int.TryParse(txtRetryInterval.Text, out var retryInterval);
            _parameter.RetryInterval = retryInterval;
            _parameter.FailureStrategy = cboFailureStrategy.SelectedItem is FailureStrategy fs ? fs : FailureStrategy.Abort;
            int.TryParse(txtJumpStep.Text, out var jumpStep);
            _parameter.JumpToStepNumber = jumpStep;
            int.TryParse(txtDelayBefore.Text, out var delayBefore);
            _parameter.DelayBeforeSend = delayBefore;
            int.TryParse(txtDelayAfter.Text, out var delayAfter);
            _parameter.DelayAfterSend = delayAfter;
            _parameter.EnableLogging = chkEnableLogging.Checked;
            _parameter.ExecuteCondition = txtExecuteCondition.Text;
        }

        protected override bool ValidateInput()
        {
            if (!chkCustomCommand.Checked && cboInstrument.SelectedItem == null)
            {
                UIMessageTip.ShowWarning("请选择仪器");
                return false;
            }
            if (!chkCustomCommand.Checked && cboCommand.SelectedItem == null)
            {
                UIMessageTip.ShowWarning("请选择命令");
                return false;
            }
            if (chkCustomCommand.Checked && string.IsNullOrWhiteSpace(txtCustomCommand.Text))
            {
                UIMessageTip.ShowWarning("请输入自定义命令内容");
                return false;
            }
            return true;
        }

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }
    }
}
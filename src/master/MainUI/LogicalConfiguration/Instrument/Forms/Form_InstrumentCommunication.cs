using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.Instrument.Models;
using MainUI.LogicalConfiguration.Instrument.Parameter;
using MainUI.LogicalConfiguration.Instrument.Services;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    public partial class Form_InstrumentCommunication : BaseParameterForm
    {
        private Parameter_InstrumentCommunication _parameter;
        private readonly IInstrumentDriverService _driverService;
        private List<InstrumentDriver> _drivers;
        private InstrumentDriver _selectedDriver;
        private InstrumentCommand _selectedCommand;
        private readonly Dictionary<string, Control> _paramControls = new();

        public Form_InstrumentCommunication(
            IWorkflowStateService workflowState,
            IInstrumentDriverService driverService,
            ILogger<Form_InstrumentCommunication> logger)
            : base(workflowState, logger)
        {
            _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
            _parameter = new Parameter_InstrumentCommunication();

            InitializeComponent();
            InitializeFormData();
            BindEvents();
            _ = LoadDriversAsync();
        }

        public Parameter_InstrumentCommunication Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_InstrumentCommunication();
                // 当参数设置时，自动加载到界面
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        private void InitializeFormData()
        {
            // 初始化数据类型下拉框 - 显示Description
            cboCustomDataType.DataSource = EnumExtensions.GetEnumItems<DataType>();
            cboCustomDataType.DisplayMember = "DisplayName";
            cboCustomDataType.ValueMember = "Value";
            cboCustomDataType.SelectedValue = DataType.String;

            // 初始化失败策略下拉框 - 显示Description
            cboFailureStrategy.DataSource = EnumExtensions.GetEnumItems<FailureStrategy>();
            cboFailureStrategy.DisplayMember = "DisplayName";
            cboFailureStrategy.ValueMember = "Value";
            cboFailureStrategy.SelectedValue = FailureStrategy.Abort;

            InitializeParseRulesGrid();

            // 隐藏解析规则面板
            grpParseRules.Visible = false;
            lblParseRules.Visible = false;
        }

        /// <summary>
        /// 初始化解析规则网格
        /// </summary>
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

        /// <summary>
        /// 事件注册
        /// </summary>
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

        /// <summary>
        /// 异步加载驱动程序
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 填充仪器组合框
        /// </summary>
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

            // 驱动加载完成后，重新加载仪器选择
            LoadInstrumentSelection();

            if (string.IsNullOrEmpty(_parameter?.DriverId)) return;

            var item = cboInstrument.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(i => ((InstrumentDriver)i.Value).DriverId == _parameter.DriverId);
            if (item != null) cboInstrument.SelectedItem = item;
        }

        /// <summary>
        /// 加载命令列表
        /// </summary>
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
        }

        private void LoadCommandParameters()
        {
            // 这个方法现在只在用户手动切换命令时调用
            // 参数加载时使用LoadCommandParametersWithValues
            LoadCommandParametersWithValues();
        }

        /// <summary>
        /// 加载命令参数
        /// </summary>
        //private void LoadCommandParameters()
        //{
        //    _paramControls.Clear();
        //    flowParams.Controls.Clear();
        //    if (_selectedCommand == null) return;

        //    foreach (var param in _selectedCommand.Parameters)
        //    {
        //        var panel = new Panel { Width = flowParams.Width - 30, Height = 35, Margin = new Padding(0, 0, 0, 5) };
        //        var label = new Label
        //        {
        //            Text = $"{param.DisplayName}:",
        //            Width = 120,
        //            TextAlign = System.Drawing.ContentAlignment.MiddleRight,
        //            Location = new System.Drawing.Point(0, 5)
        //        };

        //        Control inputControl;
        //        if (param.Options?.Count > 0)
        //        {
        //            var combo = new UIComboBox
        //            {
        //                Width = 200,
        //                Location = new System.Drawing.Point(125, 0),
        //                DropDownStyle = UIDropDownStyle.DropDown
        //            };
        //            foreach (var opt in param.Options) combo.Items.Add(opt);
        //            combo.Text = param.DefaultValue;
        //            inputControl = combo;
        //        }
        //        else
        //        {
        //            inputControl = new UITextBox
        //            {
        //                Width = 200,
        //                Location = new System.Drawing.Point(125, 0),
        //                Text = param.DefaultValue,
        //                Watermark = param.Description
        //            };
        //        }

        //        if (_parameter?.CommandParameters?.ContainsKey(param.Name) == true)
        //        {
        //            if (inputControl is UIComboBox combo)
        //                combo.Text = _parameter.CommandParameters[param.Name];
        //            else if (inputControl is UITextBox textBox)
        //                textBox.Text = _parameter.CommandParameters[param.Name];
        //        }

        //        _paramControls[param.Name] = inputControl;
        //        panel.Controls.Add(label);
        //        panel.Controls.Add(inputControl);
        //        flowParams.Controls.Add(panel);
        //    }
        //}

        private void CboInstrument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInstrument.SelectedItem is not ComboBoxItem item) return;

            _selectedDriver = item.Value as InstrumentDriver;
            LoadCommands();
        }

        private void CboCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCommand.SelectedItem is not ComboBoxItem item) return;

            _selectedCommand = item.Value as InstrumentCommand;
            LoadCommandParameters();
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
            var fs = (FailureStrategy)(cboFailureStrategy.SelectedValue ?? FailureStrategy.Abort);
            txtJumpStep.Enabled = fs == FailureStrategy.JumpToStep;
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

            if (VarHelper.ShowDialogWithOverlayEx(this, managerForm) == DialogResult.OK)
                _ = LoadDriversAsync();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            SaveFormToParameter();
            SaveParameters();
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

            // 基础信息
            txtDescription.Text = _parameter.Description;
            chkCustomCommand.Checked = _parameter.UseCustomCommand;
            txtCustomCommand.Text = _parameter.CustomCommand;
            cboCustomDataType.SelectedValue = _parameter.CustomCommandDataType;

            // 响应处理
            txtResponseVariable.Text = _parameter.ResponseVariable;
            txtStatusVariable.Text = _parameter.StatusVariable;
            txtErrorVariable.Text = _parameter.ErrorVariable;

            // 超时和重试
            chkOverrideTimeout.Checked = _parameter.OverrideTimeout;
            txtTimeout.Text = _parameter.CustomTimeout.ToString();
            txtRetryCount.Text = _parameter.RetryCount.ToString();
            txtRetryInterval.Text = _parameter.RetryInterval.ToString();

            // 错误处理
            cboFailureStrategy.SelectedValue = _parameter.FailureStrategy;
            txtJumpStep.Text = _parameter.JumpToStepNumber.ToString();

            // 高级选项
            txtDelayBefore.Text = _parameter.DelayBeforeSend.ToString();
            txtDelayAfter.Text = _parameter.DelayAfterSend.ToString();
            chkEnableLogging.Checked = _parameter.EnableLogging;
            txtExecuteCondition.Text = _parameter.ExecuteCondition;

            // 加载仪器选择
            LoadInstrumentSelection();

            // 加载解析规则
            LoadParseRules();
        }

        /// <summary>
        /// 加载仪器和命令选择（处理异步加载问题）
        /// </summary>
        private void LoadInstrumentSelection()
        {
            if (_drivers == null || _drivers.Count == 0)
            {
                // 驱动还未加载，等待加载完成后再设置
                Logger?.LogDebug("驱动列表未加载，将在加载完成后自动设置选中项");
                return;
            }

            // 设置选中的仪器
            if (string.IsNullOrEmpty(_parameter.DriverId)) return;
            var driverItem = cboInstrument.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(i => ((InstrumentDriver)i.Value).DriverId == _parameter.DriverId);

            if (driverItem != null)
            {
                cboInstrument.SelectedItem = driverItem;
                _selectedDriver = driverItem.Value as InstrumentDriver;

                // 加载该仪器的命令列表
                LoadCommands();

                // 设置选中的命令
                if (string.IsNullOrEmpty(_parameter.CommandId)) return;

                var commandItem = cboCommand.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(i => ((InstrumentCommand)i.Value).CommandId == _parameter.CommandId);

                if (commandItem == null) return;

                cboCommand.SelectedItem = commandItem;
                _selectedCommand = commandItem.Value as InstrumentCommand;

                // 加载命令参数
                LoadCommandParametersWithValues();
            }
            else
            {
                Logger?.LogWarning("未找到驱动ID: {DriverId}", _parameter.DriverId);
            }
        }

        /// <summary>
        /// 加载命令参数
        /// </summary>
        private void LoadCommandParametersWithValues()
        {
            _paramControls.Clear();
            flowParams.Controls.Clear();

            if (_selectedCommand == null) return;

            foreach (var param in _selectedCommand.Parameters)
            {
                var panel = new Panel
                {
                    Width = flowParams.Width - 30,
                    Height = 35,
                    Margin = new Padding(0, 0, 0, 5)
                };

                var label = new Label
                {
                    Text = $"{param.DisplayName}:",
                    Width = 120,
                    TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                    Location = new System.Drawing.Point(0, 5)
                };

                Control inputControl;

                // 获取已保存的值
                string savedValue = _parameter?.CommandParameters?.ContainsKey(param.Name) == true
                    ? _parameter.CommandParameters[param.Name]
                    : param.DefaultValue;

                if (param.Options?.Count > 0)
                {
                    var combo = new UIComboBox
                    {
                        Width = 200,
                        Location = new System.Drawing.Point(125, 0),
                        DropDownStyle = UIDropDownStyle.DropDown
                    };
                    foreach (var opt in param.Options)
                        combo.Items.Add(opt);
                    combo.Text = savedValue;
                    inputControl = combo;
                }
                else
                {
                    inputControl = new UITextBox
                    {
                        Width = 200,
                        Location = new System.Drawing.Point(125, 0),
                        Text = savedValue,
                        Watermark = param.Description
                    };
                }

                _paramControls[param.Name] = inputControl;
                panel.Controls.Add(label);
                panel.Controls.Add(inputControl);
                flowParams.Controls.Add(panel);
            }
        }

        /// <summary>
        /// 加载解析规则到DataGridView
        /// </summary>
        private void LoadParseRules()
        {
            dgvParseRules.Rows.Clear();

            if (_parameter.CustomParseRules == null || _parameter.CustomParseRules.Count == 0)
                return;

            foreach (var rule in _parameter.CustomParseRules)
            {
                int rowIndex = dgvParseRules.Rows.Add();
                var row = dgvParseRules.Rows[rowIndex];

                row.Cells["Name"].Value = rule.Name;
                row.Cells["TargetVariable"].Value = rule.TargetVariable;
                row.Cells["ParseType"].Value = rule.ParseType;

                // 根据ParseType组合不同的参数
                row.Cells["Pattern"].Value = GetParseRulePattern(rule);

                // 保存完整的rule对象到Tag，方便后续编辑
                row.Tag = rule;
            }
        }

        /// <summary>
        /// 根据解析类型获取参数字符串
        /// </summary>
        private string GetParseRulePattern(ResponseParseRule rule)
        {
            return rule.ParseType switch
            {
                "Position" => $"起始:{rule.StartPosition}, 长度:{rule.Length}",
                "Delimiter" => $"分隔符:{rule.Delimiter}, 索引:{rule.SegmentIndex}",
                "Regex" => rule.RegexPattern,
                "Json" => rule.JsonPath,
                _ => ""
            };
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
            _parameter.CustomCommandDataType = (DataType)(cboCustomDataType.SelectedValue ?? DataType.String);

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
            _parameter.FailureStrategy = (FailureStrategy)(cboFailureStrategy.SelectedValue ?? FailureStrategy.Abort);
            int.TryParse(txtJumpStep.Text, out var jumpStep);
            _parameter.JumpToStepNumber = jumpStep;
            int.TryParse(txtDelayBefore.Text, out var delayBefore);
            _parameter.DelayBeforeSend = delayBefore;
            int.TryParse(txtDelayAfter.Text, out var delayAfter);
            _parameter.DelayAfterSend = delayAfter;
            _parameter.EnableLogging = chkEnableLogging.Checked;
            _parameter.ExecuteCondition = txtExecuteCondition.Text;
            //_parameter.OverrideConnectionParams = chkOverrideConnection?.Checked ?? false;
            // _parameter.OverrideParamsJson = txtOverrideParamsJson?.Text ?? "";
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
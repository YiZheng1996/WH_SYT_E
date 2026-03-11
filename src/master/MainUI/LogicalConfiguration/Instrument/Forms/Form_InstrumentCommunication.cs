using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.Instrument.Communication;
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
            AttachExpressionPanels();
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

        private void AttachExpressionPanels()
        {
            try
            {
                // 目标变量
                ExpressionInputPanel.AttachTo(txtResponseVariable, new InputPanelOptions
                {
                    Mode = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title = "选择目标变量",
                    ShowValidation = false,
                    ShowPreview = false,
                    CloseOnSubmit = true
                });
                txtResponseVariable.Watermark = "点击选择目标变量 (按F2打开面板)";

                // 目标变量
                ExpressionInputPanel.AttachTo(txtStatusVariable, new InputPanelOptions
                {
                    Mode = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title = "选择目标变量",
                    ShowValidation = false,
                    ShowPreview = false,
                    CloseOnSubmit = true
                });
                txtStatusVariable.Watermark = "点击选择目标变量 (按F2打开面板)";

                // 目标变量
                ExpressionInputPanel.AttachTo(txtErrorVariable, new InputPanelOptions
                {
                    Mode = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title = "选择目标变量",
                    ShowValidation = false,
                    ShowPreview = false,
                    CloseOnSubmit = true
                });
                txtErrorVariable.Watermark = "点击选择目标变量 (按F2打开面板)";

                Logger?.LogDebug("表达式输入面板附加完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "附加表达式输入面板失败");
            }
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
            _driverService.DriversChanged += OnDriversChanged;
            cboInstrument.SelectedIndexChanged += CboInstrument_SelectedIndexChanged;
            cboCommand.SelectedIndexChanged += CboCommand_SelectedIndexChanged;
            chkCustomCommand.CheckedChanged += ChkCustomCommand_CheckedChanged;
            chkOverrideTimeout.CheckedChanged += ChkOverrideTimeout_CheckedChanged;
            cboFailureStrategy.SelectedIndexChanged += CboFailureStrategy_SelectedIndexChanged;
            btnManageDrivers.Click += BtnManageDrivers_Click;
            btnTestConnection.Click += BtnTestConnection_Click;
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
            FormClosing += Form_InstrumentCommunication_FormClosing;
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

            //if (string.IsNullOrEmpty(_parameter?.DriverId)) return;

            //var item = cboInstrument.Items.Cast<ComboBoxItem>()
            //    .FirstOrDefault(i => ((InstrumentDriver)i.Value).DriverId == _parameter.DriverId);
            //if (item != null) cboInstrument.SelectedItem = item;
        }

        /// <summary>
        /// 加载命令列表
        /// </summary>
        private void LoadCommands()
        {
            // 先 Detach 旧的输入面板
            foreach (var ctrl in _paramControls.Values)
            {
                if (ctrl is UITextBox txt)
                    ExpressionInputPanel.DetachFrom(txt);
            }
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

        private void OnDriversChanged()
        {
            if (IsDisposed || !IsHandleCreated) return;

            if (InvokeRequired)
                BeginInvoke((async void () => await LoadDriversAsync()));
            else
                _ = LoadDriversAsync();
        }

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
                // ← 修复：使用单例工厂，connectionId 传入驱动ID避免与运行时连接混用
                var testConnectionId = $"TEST_{_selectedDriver.DriverId}";
                var provider = CommunicationProviderFactory.Instance.CreateProvider(_selectedDriver.ProtocolType);

                var config = _selectedDriver.GetProtocolConfig();
                var connected = await provider.ConnectAsync(config);

                await provider.DisconnectAsync();
                provider.Dispose();

                if (connected)
                    UIMessageTip.ShowOk("连接测试成功！");
                else
                    UIMessageTip.ShowError("连接测试失败，请检查连接配置");
            }
            catch (Exception ex)
            {
                UIMessageTip.ShowError($"测试异常: {ex.Message}");
                Logger?.LogError(ex, "连接测试异常");
            }
            finally
            {
                btnTestConnection.Enabled = true;
                btnTestConnection.Text = "测试连接";
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

        private void Form_InstrumentCommunication_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExpressionInputPanel.CloseActivePanel();
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
                MessageHelper.MessageOK(this, "请选择仪器");
                return false;
            }

            if (!chkCustomCommand.Checked && cboCommand.SelectedItem == null)
            {
                MessageHelper.MessageOK(this, "请选择命令");
                return false;
            }

            if (chkCustomCommand.Checked && string.IsNullOrWhiteSpace(txtCustomCommand.Text))
            {
                MessageHelper.MessageOK(this, "请输入自定义命令内容");
                return false;
            }

            // ── 必填参数校验 ──────────────────────────
            if (!chkCustomCommand.Checked && _selectedCommand != null)
            {
                foreach (var param in _selectedCommand.Parameters.Where(p => p.Required))
                {
                    if (!_paramControls.TryGetValue(param.Name, out var ctrl)) continue;

                    var val = ctrl switch
                    {
                        UIComboBox combo => combo.Text,
                        UITextBox txt => txt.Text,
                        _ => ""
                    };

                    if (string.IsNullOrWhiteSpace(val))
                    {
                        MessageHelper.MessageOK(this, $"参数「{param.DisplayName}」为必填项，请填写");
                        ctrl.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_driverService != null)
                _driverService.DriversChanged -= OnDriversChanged;
            base.OnFormClosed(e);
        }

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }

        #region 命令参数动态渲染

        /// <summary>
        /// 根据选中的命令动态渲染参数输入行（含已保存的值回填）
        /// </summary>
        private void LoadCommandParametersWithValues()
        {
            // 清理旧控件：先 Detach 所有 ExpressionInputPanel，再清除
            foreach (var ctrl in _paramControls.Values)
            {
                if (ctrl is UITextBox txt)
                    ExpressionInputPanel.DetachFrom(txt);
            }
            _paramControls.Clear();
            flowParams.Controls.Clear();

            if (_selectedCommand == null) return;

            // 没有参数时显示提示
            if (_selectedCommand.Parameters == null || _selectedCommand.Parameters.Count == 0)
            {
                flowParams.Controls.Add(MakeNoParamHint());
                return;
            }

            foreach (var param in _selectedCommand.Parameters)
            {
                // 取已保存的值，回退到参数默认值
                string savedValue = _parameter?.CommandParameters?.TryGetValue(param.Name, out var sv) == true
                    ? sv
                    : param.DefaultValue ?? "";

                var row = BuildParamRow(param, savedValue);
                flowParams.Controls.Add(row);
            }
        }

        /// <summary>
        /// 构建单个参数的输入行 Panel
        /// </summary>
        private Panel BuildParamRow(CommandParameter param, string savedValue)
        {
            // ── 外层行容器 ────────────────────────────────
            var row = new Panel
            {
                Width = Math.Max(flowParams.Width > 0 ? flowParams.Width - 8 : 400, 300),
                Height = 42,
                Margin = new Padding(0, 0, 0, 6),
                Tag = param.Name
            };

            // ── 必填红星 ──────────────────────────────────
            if (param.Required)
            {
                var star = new Label
                {
                    Text = "*",
                    ForeColor = Color.Red,
                    Font = new Font("微软雅黑", 12F, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(0, 10)
                };
                row.Controls.Add(star);
            }

            // ── 标签（显示名称 + 类型提示）────────────────
            var lbl = new UILabel
            {
                Text = $"{param.DisplayName}:",
                Font = new Font("微软雅黑", 11F),
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(10, 8),
                Size = new Size(130, 26),
                ForeColor = Color.FromArgb(48, 48, 48)
            };
            row.Controls.Add(lbl);

            // ── 类型角标 ──────────────────────────────────
            var typeTag = new Label
            {
                Text = GetDataTypeTag(param.DataType),
                Font = new Font("微软雅黑", 9F),
                ForeColor = Color.FromArgb(120, 120, 180),
                AutoSize = true,
                Location = new Point(146, 14)
            };
            row.Controls.Add(typeTag);

            // ── 输入控件 ──────────────────────────────────
            Control inputCtrl;

            if (param.Options?.Count > 0)
            {
                // 有预定义选项 → 下拉框
                inputCtrl = BuildComboInput(param, savedValue);
            }
            else
            {
                // 自由输入 → UITextBox + ExpressionInputPanel
                inputCtrl = BuildTextInput(param, savedValue);
            }

            inputCtrl.Location = new Point(196, 6);
            inputCtrl.Width = Math.Max(row.Width - 310, 160);
            row.Controls.Add(inputCtrl);

            // ── 范围提示标签 ──────────────────────────────
            if (param.MinValue.HasValue || param.MaxValue.HasValue)
            {
                var rangeLbl = new Label
                {
                    Text = BuildRangeHint(param),
                    Font = new Font("微软雅黑", 9F),
                    ForeColor = Color.FromArgb(100, 100, 200),
                    AutoSize = true,
                    Location = new Point(inputCtrl.Right + 6, 14)
                };
                row.Controls.Add(rangeLbl);
            }
            else if (!string.IsNullOrEmpty(param.Description))
            {
                // 无范围但有描述 → 小提示
                var descLbl = new Label
                {
                    Text = param.Description,
                    Font = new Font("微软雅黑", 9F),
                    ForeColor = Color.FromArgb(150, 150, 150),
                    AutoSize = true,
                    Location = new Point(inputCtrl.Right + 6, 14)
                };
                row.Controls.Add(descLbl);
            }

            // 存入字典，供保存时收集
            _paramControls[param.Name] = inputCtrl;

            return row;
        }

        /// <summary>
        /// 构建下拉选项输入控件（参数定义了 Options 时）
        /// </summary>
        private UIComboBox BuildComboInput(CommandParameter param, string savedValue)
        {
            var combo = new UIComboBox
            {
                DropDownStyle = UIDropDownStyle.DropDownList,
                Font = new Font("微软雅黑", 12F),
                Height = 30,
                FillColor = Color.White,
                ItemHoverColor = Color.FromArgb(155, 200, 255),
                ItemSelectForeColor = Color.FromArgb(235, 243, 255),
                Padding = new Padding(0, 0, 30, 2),
                SymbolSize = 24
            };

            foreach (var opt in param.Options)
                combo.Items.Add(opt);

            // 回填已保存的值
            int idx = combo.Items.IndexOf(savedValue);
            combo.SelectedIndex = idx >= 0 ? idx : (combo.Items.Count > 0 ? 0 : -1);

            return combo;
        }

        /// <summary>
        /// 构建文本输入控件，附加 ExpressionInputPanel
        /// 支持直接输入固定值，也支持 {变量名} 引用变量
        /// </summary>
        private UITextBox BuildTextInput(CommandParameter param, string savedValue)
        {
            var txt = new UITextBox
            {
                Font = new Font("微软雅黑", 12F),
                Height = 30,
                Padding = new Padding(5),
                ShowText = false,
                TextAlignment = ContentAlignment.MiddleLeft,
                Text = savedValue,
                Watermark = BuildWatermark(param)
            };

            // 根据数据类型决定开放哪些输入模式
            var modules = param.DataType == DataType.Boolean
                ? InputModules.Variable | InputModules.Constant
                : InputModules.Variable | InputModules.Expression | InputModules.Constant;

            ExpressionInputPanel.AttachTo(txt, new InputPanelOptions
            {
                Mode = InputMode.Expression,
                EnabledModules = modules,
                Title = $"输入参数值：{param.DisplayName}",
                ShowValidation = false,
                ShowPreview = true,
                CloseOnSubmit = true
            });

            return txt;
        }

        /// <summary>
        /// 无参数时显示的提示控件
        /// </summary>
        private static Label MakeNoParamHint() => new Label
        {
            Text = "该命令无需参数",
            Font = new Font("微软雅黑", 11F),
            ForeColor = Color.FromArgb(160, 160, 160),
            AutoSize = true,
            Margin = new Padding(4, 8, 0, 0)
        };

        /// <summary>
        /// 数据类型短标签，显示在参数名旁边
        /// </summary>
        private static string GetDataTypeTag(DataType type) => type switch
        {
            DataType.Integer => "[整数]",
            DataType.Double => "[小数]",
            DataType.Boolean => "[布尔]",
            DataType.String => "[文本]",
            _ => $"[{type}]"
        };

        /// <summary>
        /// 根据参数定义构造输入框水印提示文字
        /// </summary>
        private static string BuildWatermark(CommandParameter param)
        {
            var parts = new List<string>();

            if (!string.IsNullOrEmpty(param.DefaultValue))
                parts.Add($"默认 {param.DefaultValue}");

            if (param.MinValue.HasValue || param.MaxValue.HasValue)
                parts.Add(BuildRangeHint(param));

            parts.Add("支持 {变量名} 引用变量");

            return string.Join("，", parts);
        }

        /// <summary>
        /// 构建范围提示文字，如 [0 ~ 300]
        /// </summary>
        private static string BuildRangeHint(CommandParameter param)
        {
            if (param.MinValue.HasValue && param.MaxValue.HasValue)
                return $"[{param.MinValue} ~ {param.MaxValue}]";
            if (param.MinValue.HasValue)
                return $"[≥ {param.MinValue}]";
            if (param.MaxValue.HasValue)
                return $"[≤ {param.MaxValue}]";
            return "";
        }

        #endregion
    }
}
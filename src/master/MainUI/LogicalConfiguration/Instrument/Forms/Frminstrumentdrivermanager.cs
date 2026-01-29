using MainUI.LogicalConfiguration.Instrument.Models;
using MainUI.LogicalConfiguration.Instrument.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 仪器驱动管理窗体
    /// </summary>
    public partial class FrmInstrumentDriverManager : UIForm
    {
        #region 字段

        private readonly IInstrumentDriverService _driverService;
        private readonly ILogger _logger;
        private List<InstrumentDriver> _drivers;
        private InstrumentDriver _selectedDriver;
        private Dictionary<string, Control> _protocolControls = new();

        #endregion

        #region 构造函数

        public FrmInstrumentDriverManager(
            IInstrumentDriverService driverService,
            ILogger<FrmInstrumentDriverManager> logger)
        {
            _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
            _logger = logger;

            InitializeComponent();
            InitializeFormData();
            BindEvents();
            InitializeGridColumns();
            _ = LoadDriversAsync();
        }

        #endregion

        #region 初始化

        private void InitializeFormData()
        {
            // 初始化设备类型下拉框
            foreach (InstrumentCategory cat in Enum.GetValues(typeof(InstrumentCategory)))
            {
                cboCategory.Items.Add(cat);
            }
            cboCategory.SelectedIndex = 0;

            // 初始化协议类型下拉框
            foreach (ProtocolType pt in Enum.GetValues(typeof(ProtocolType)))
            {
                cboProtocolType.Items.Add(pt);
            }
            cboProtocolType.SelectedIndex = 0;

            // 初始化校验类型下拉框
            foreach (ChecksumType ct in Enum.GetValues(typeof(ChecksumType)))
            {
                cboChecksumType.Items.Add(ct);
            }
            cboChecksumType.SelectedIndex = 0;
        }

        private void BindEvents()
        {
            // 驱动列表事件
            dgvDrivers.SelectionChanged += DgvDrivers_SelectionChanged;
            dgvDrivers.CellDoubleClick += DgvDrivers_CellDoubleClick;

            // 工具栏按钮事件
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClone.Click += BtnClone_Click;
            btnImport.Click += BtnImport_Click;
            btnExport.Click += BtnExport_Click;

            // 协议类型变更
            cboProtocolType.SelectedIndexChanged += CboProtocolType_SelectedIndexChanged;

            // 命令模板按钮事件
            btnAddCommand.Click += BtnAddCommand_Click;
            btnEditCommand.Click += BtnEditCommand_Click;
            btnDeleteCommand.Click += BtnDeleteCommand_Click;
            dgvCommands.CellDoubleClick += DgvCommands_CellDoubleClick;

            // 底部按钮事件
            btnTestConnection.Click += BtnTestConnection_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void InitializeGridColumns()
        {
            // 驱动列表列
            dgvDrivers.Columns.Clear();
            dgvDrivers.Columns.Add("DisplayName", "显示名称");
            dgvDrivers.Columns.Add("Category", "类别");
            dgvDrivers.Columns.Add("ProtocolType", "协议");
            dgvDrivers.Columns.Add("Enabled", "状态");

            // 命令列表列
            dgvCommands.Columns.Clear();
            dgvCommands.Columns.Add("Name", "命令名称");
            dgvCommands.Columns.Add("DisplayName", "显示名称");
            dgvCommands.Columns.Add("CommandType", "类型");
            dgvCommands.Columns.Add("RequestTemplate", "请求模板");
        }

        #endregion

        #region 数据加载

        private async Task LoadDriversAsync()
        {
            try
            {
                _drivers = (await _driverService.GetAllDriversAsync()).ToList();
                RefreshDriverList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载驱动列表失败");
                UIMessageTip.ShowError($"加载驱动列表失败: {ex.Message}");
            }
        }

        private void RefreshDriverList()
        {
            dgvDrivers.Rows.Clear();

            foreach (var driver in _drivers)
            {
                var rowIndex = dgvDrivers.Rows.Add(
                    driver.DisplayName,
                    GetCategoryDisplayName(driver.Category),
                    driver.ProtocolType.ToString(),
                    driver.Enabled ? "启用" : "禁用"
                );
                dgvDrivers.Rows[rowIndex].Tag = driver;
            }

            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Rows[0].Selected = true;
            }
        }

        private void LoadDriverToForm(InstrumentDriver driver)
        {
            if (driver == null)
            {
                ClearForm();
                return;
            }

            _selectedDriver = driver;

            // 基本信息
            txtName.Text = driver.Name;
            txtDisplayName.Text = driver.DisplayName;
            cboCategory.SelectedItem = driver.Category;
            cboProtocolType.SelectedItem = driver.ProtocolType;
            txtManufacturer.Text = driver.Manufacturer;
            txtModel.Text = driver.Model;
            chkEnabled.Checked = driver.Enabled;
            txtDescription.Text = driver.Description;

            // 协议配置
            LoadProtocolConfig(driver);

            // 帧配置
            LoadFrameConfig(driver.FrameConfig);

            // 命令列表
            LoadCommands(driver.Commands);
        }

        /// <summary>
        /// 协议配置
        /// </summary>
        /// <param name="driver"></param>
        private void LoadProtocolConfig(InstrumentDriver driver)
        {
            var config = driver.GetProtocolConfig();

            switch (driver.ProtocolType)
            {
                case ProtocolType.TcpIp:
                    CreateTcpConfigControls();
                    if (config is TcpProtocolConfig tcp)
                    {
                        SetControlValue("IpAddress", tcp.IpAddress);
                        SetControlValue("Port", tcp.Port.ToString());
                        SetControlValue("ConnectionTimeout", tcp.ConnectionTimeout.ToString());
                        SetControlValue("ReadTimeout", tcp.ReadTimeout.ToString());
                        SetControlValue("KeepAlive", tcp.KeepAlive);
                    }
                    break;

                case ProtocolType.Serial:
                    CreateSerialConfigControls();
                    if (config is SerialProtocolConfig serial)
                    {
                        SetControlValue("PortName", serial.PortName);
                        SetControlValue("BaudRate", serial.BaudRate.ToString());
                        SetControlValue("DataBits", serial.DataBits);
                        SetControlValue("StopBits", serial.StopBits);
                        SetControlValue("Parity", serial.Parity);
                        SetControlValue("FlowControl", serial.FlowControl);
                        SetControlValue("ReadTimeout", serial.ReadTimeout.ToString());
                    }
                    break;

                case ProtocolType.ModbusTcp:
                case ProtocolType.ModbusRtu:
                    CreateModbusConfigControls();
                    if (config is ModbusProtocolConfig modbus)
                    {
                        SetControlValue("SlaveAddress", modbus.SlaveAddress.ToString());
                        if (driver.ProtocolType == ProtocolType.ModbusTcp)
                        {
                            SetControlValue("IpAddress", modbus.IpAddress);
                            SetControlValue("Port", modbus.Port.ToString());
                        }
                        else
                        {
                            SetControlValue("PortName", modbus.PortName);
                            SetControlValue("BaudRate", modbus.BaudRate.ToString());
                        }
                        SetControlValue("ByteOrder", modbus.ByteOrder);
                        SetControlValue("ReadTimeout", modbus.ReadTimeout.ToString());
                    }
                    break;
            }
        }


        private void LoadFrameConfig(FrameConfig config)
        {
            if (config == null)
            {
                chkFrameEnabled.Checked = false;
                txtFrameHeader.Text = "";
                txtFrameFooter.Text = "";
                txtResponseTerminator.Text = "";
                cboChecksumType.SelectedItem = ChecksumType.None;
                return;
            }

            chkFrameEnabled.Checked = config.Enabled;
            txtFrameHeader.Text = config.FrameHeader;
            txtFrameFooter.Text = config.FrameFooter;
            txtResponseTerminator.Text = config.ResponseTerminator;
            cboChecksumType.SelectedItem = config.ChecksumType;
        }

        private void LoadCommands(List<InstrumentCommand> commands)
        {
            dgvCommands.Rows.Clear();

            if (commands == null)
                return;

            foreach (var cmd in commands)
            {
                var rowIndex = dgvCommands.Rows.Add(
                    cmd.Name,
                    cmd.DisplayName,
                    cmd.CommandType.ToString(),
                    cmd.RequestTemplate
                );
                dgvCommands.Rows[rowIndex].Tag = cmd;
            }
        }

        private void ClearForm()
        {
            _selectedDriver = null;
            txtName.Text = "";
            txtDisplayName.Text = "";
            cboCategory.SelectedIndex = 0;
            cboProtocolType.SelectedIndex = 0;
            txtManufacturer.Text = "";
            txtModel.Text = "";
            chkEnabled.Checked = true;
            txtDescription.Text = "";
            chkFrameEnabled.Checked = false;
            txtFrameHeader.Text = "";
            txtFrameFooter.Text = "";
            txtResponseTerminator.Text = "";
            cboChecksumType.SelectedIndex = 0;
            dgvCommands.Rows.Clear();
        }

        #endregion

        #region 协议配置面板

        private void CboProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProtocolConfigPanel();
        }

        private void UpdateProtocolConfigPanel()
        {
            panelProtocolConfig.Controls.Clear();
            _protocolControls.Clear();

            var protocolType = cboProtocolType.SelectedItem is ProtocolType pt ? pt : ProtocolType.Serial;

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 5,
                Padding = new Padding(10)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            switch (protocolType)
            {
                case ProtocolType.Serial:
                    CreateSerialConfigControls(layout);
                    break;

                case ProtocolType.TcpClient:
                case ProtocolType.TcpServer:
                    CreateTcpConfigControls(layout);
                    break;

                case ProtocolType.Modbus_RTU:
                    CreateModbusRtuConfigControls(layout);
                    break;

                case ProtocolType.Modbus_TCP:
                    CreateModbusTcpConfigControls(layout);
                    break;

                case ProtocolType.VISA:
                    CreateVisaConfigControls(layout);
                    break;
            }

            panelProtocolConfig.Controls.Add(layout);
        }

        private void CreateSerialConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            // 串口名称
            AddConfigRow(layout, row++, "串口:", "PortName", "COM1");

            // 波特率
            AddConfigRow(layout, row, "波特率:", "BaudRate", "9600", 0);
            AddConfigRow(layout, row++, "数据位:", "DataBits", "8", 2);

            // 停止位和校验
            AddConfigRow(layout, row, "停止位:", "StopBits", "1", 0);
            AddConfigRow(layout, row++, "校验位:", "Parity", "None", 2);
        }

        private void CreateTcpConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            AddConfigRow(layout, row, "IP地址:", "IpAddress", "192.168.1.1", 0);
            AddConfigRow(layout, row++, "端口:", "Port", "5000", 2);
        }

        private void CreateModbusRtuConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            AddConfigRow(layout, row, "从站地址:", "SlaveAddress", "1", 0);
            AddConfigRow(layout, row++, "串口:", "PortName", "COM1", 2);

            AddConfigRow(layout, row, "波特率:", "BaudRate", "9600", 0);
            AddConfigRow(layout, row++, "数据位:", "DataBits", "8", 2);
        }

        private void CreateModbusTcpConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            AddConfigRow(layout, row, "从站地址:", "SlaveAddress", "1", 0);
            AddConfigRow(layout, row++, "IP地址:", "IpAddress", "192.168.1.1", 2);

            AddConfigRow(layout, row, "端口:", "Port", "502", 0);
        }

        private void CreateVisaConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            AddConfigRow(layout, row++, "资源名:", "ResourceName", "TCPIP::192.168.1.1::INSTR");
        }

        private void AddConfigRow(TableLayoutPanel layout, int row, string label, string name, string defaultValue, int col = 0)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("微软雅黑", 9F)
            };

            var txt = new UITextBox
            {
                Dock = DockStyle.Fill,
                Text = defaultValue,
                Name = name
            };

            layout.Controls.Add(lbl, col, row);
            layout.Controls.Add(txt, col + 1, row);

            _protocolControls[name] = txt;
        }

        private void SetControlValue(string key, object value)
        {
            if (!_protocolControls.TryGetValue(key, out var control))
                return;

            switch (control)
            {
                case UITextBox txt:
                    txt.Text = value?.ToString() ?? "";
                    break;
                case UIComboBox cbo:
                    if (value is Enum)
                        cbo.SelectedItem = value;
                    else
                        cbo.Text = value?.ToString() ?? "";
                    break;
                case UICheckBox chk:
                    chk.Checked = value is bool b && b;
                    break;
            }
        }

        private T GetControlValue<T>(string name, T defaultValue = default)
        {
            if (!_protocolControls.TryGetValue(name, out var control))
                return defaultValue;

            var text = control is UITextBox txt ? txt.Text :
                       control is UIComboBox cbo ? cbo.Text : "";

            try
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)text;
                if (typeof(T) == typeof(int))
                    return int.TryParse(text, out var i) ? (T)(object)i : defaultValue;
                if (typeof(T) == typeof(byte))
                    return byte.TryParse(text, out var b) ? (T)(object)b : defaultValue;
            }
            catch { }

            return defaultValue;
        }

        #endregion

        #region 数据收集

        private InstrumentDriver CollectDriverFromForm()
        {
            var driver = _selectedDriver ?? new InstrumentDriver();

            driver.Name = txtName.Text.Trim();
            driver.DisplayName = txtDisplayName.Text.Trim();
            driver.Category = (InstrumentCategory)cboCategory.SelectedItem;
            driver.ProtocolType = (ProtocolType)cboProtocolType.SelectedItem;
            driver.Manufacturer = txtManufacturer.Text.Trim();
            driver.Model = txtModel.Text.Trim();
            driver.Enabled = chkEnabled.Checked;
            driver.Description = txtDescription.Text.Trim();

            // 协议配置
            driver.SetProtocolConfig(CollectProtocolConfig());

            // 帧配置
            driver.FrameConfig = new FrameConfig
            {
                Enabled = chkFrameEnabled.Checked,
                FrameHeader = txtFrameHeader.Text,
                FrameFooter = txtFrameFooter.Text,
                ResponseTerminator = txtResponseTerminator.Text,
                ChecksumType = (ChecksumType)cboChecksumType.SelectedItem
            };

            // 命令列表
            driver.Commands.Clear();
            foreach (DataGridViewRow row in dgvCommands.Rows)
            {
                if (row.Tag is InstrumentCommand cmd)
                {
                    driver.Commands.Add(cmd);
                }
            }

            return driver;
        }

        private ProtocolConfigBase CollectProtocolConfig()
        {
            var protocolType = (ProtocolType)cboProtocolType.SelectedItem;

            switch (protocolType)
            {
                case ProtocolType.TcpIp:
                    return new TcpProtocolConfig
                    {
                        IpAddress = GetControlValue<string>("IpAddress"),
                        Port = GetControlValue<int>("Port", 5000),
                        ConnectionTimeout = GetControlValue<int>("ConnectionTimeout", 5000),
                        ReadTimeout = GetControlValue<int>("ReadTimeout", 3000),
                        KeepAlive = GetControlValue<bool>("KeepAlive", true)
                    };

                case ProtocolType.Serial:
                    return new SerialProtocolConfig
                    {
                        PortName = GetControlValue<string>("PortName"),
                        BaudRate = GetControlValue<int>("BaudRate", 9600),
                        DataBits = GetControlValue<int>("DataBits", 8),
                        StopBits = GetControlValue<StopBitsType>("StopBits", StopBitsType.One),
                        Parity = GetControlValue<ParityType>("Parity", ParityType.None),
                        FlowControl = GetControlValue<FlowControlType>("FlowControl", FlowControlType.None),
                        ReadTimeout = GetControlValue<int>("ReadTimeout", 3000)
                    };

                case ProtocolType.ModbusTcp:
                    var modbusTcp = new ModbusProtocolConfig
                    {
                        SlaveAddress = GetControlValue<byte>("SlaveAddress", 1),
                        IpAddress = GetControlValue<string>("IpAddress"),
                        Port = GetControlValue<int>("Port", 502),
                        ByteOrder = GetControlValue<ByteOrder>("ByteOrder", ByteOrder.BigEndian),
                        ReadTimeout = GetControlValue<int>("ReadTimeout", 3000)
                    };
                    modbusTcp.SetModbusType(true);
                    return modbusTcp;

                case ProtocolType.ModbusRtu:
                    var modbusRtu = new ModbusProtocolConfig
                    {
                        SlaveAddress = GetControlValue<byte>("SlaveAddress", 1),
                        PortName = GetControlValue<string>("PortName"),
                        BaudRate = GetControlValue<int>("BaudRate", 9600),
                        ByteOrder = GetControlValue<ByteOrder>("ByteOrder", ByteOrder.BigEndian),
                        ReadTimeout = GetControlValue<int>("ReadTimeout", 3000)
                    };
                    modbusRtu.SetModbusType(false);
                    return modbusRtu;

                case ProtocolType.Http:
                case ProtocolType.Udp:
                default:
                    return new TcpProtocolConfig();
            }
        }

        #endregion

        #region 驱动列表事件

        private void DgvDrivers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDrivers.SelectedRows.Count > 0)
            {
                var driver = dgvDrivers.SelectedRows[0].Tag as InstrumentDriver;
                LoadDriverToForm(driver);
            }
        }

        private void DgvDrivers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEdit_Click(sender, e);
            }
        }

        #endregion

        #region 工具栏按钮事件

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            _selectedDriver = new InstrumentDriver();
            txtName.Focus();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedDriver == null)
            {
                UIMessageTip.ShowWarning("请先选择要编辑的驱动");
                return;
            }
            txtName.Focus();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedDriver == null)
            {
                UIMessageTip.ShowWarning("请先选择要删除的驱动");
                return;
            }

            if (MessageHelper.MessageYes($"确定要删除驱动 [{_selectedDriver.DisplayName}] 吗？") != DialogResult.OK)
                return;

            try
            {
                _driverService.DeleteDriverAsync(_selectedDriver.Name).Wait();
                _drivers.Remove(_selectedDriver);
                RefreshDriverList();
                ClearForm();
                UIMessageTip.ShowOk("删除成功");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除驱动失败");
                UIMessageTip.ShowError($"删除失败: {ex.Message}");
            }
        }

        private void BtnClone_Click(object sender, EventArgs e)
        {
            if (_selectedDriver == null)
            {
                UIMessageTip.ShowWarning("请先选择要复制的驱动");
                return;
            }

            var cloned = _selectedDriver.Clone();
            cloned.Name = $"{_selectedDriver.Name}_Copy";
            cloned.DisplayName = $"{_selectedDriver.DisplayName} (副本)";

            _selectedDriver = cloned;
            LoadDriverToForm(cloned);
            UIMessageTip.ShowOk("已复制，请修改后保存");
        }

        private async void BtnImport_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "JSON文件|*.json",
                Title = "导入仪器驱动配置"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var imported = await _driverService.ImportDriverAsync(ofd.FileName);
            if (imported != null)
            {
                UIMessageTip.ShowOk($"导入成功: {imported.DisplayName}");
                await LoadDriversAsync();
            }
            else
            {
                UIMessageTip.ShowError("导入失败");
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (_drivers == null || _drivers.Count == 0)
            {
                UIMessageTip.ShowWarning("没有可导出的驱动配置");
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "JSON文件|*.json",
                Title = "导出仪器驱动配置",
                FileName = $"InstrumentDrivers_{DateTime.Now:yyyyMMdd}.json"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var json = JsonConvert.SerializeObject(_drivers, Formatting.Indented);
                File.WriteAllText(dialog.FileName, json);
                UIMessageTip.ShowOk("导出成功");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导出驱动配置失败");
                UIMessageTip.ShowError($"导出失败: {ex.Message}");
            }
        }

        #endregion

        #region 命令模板事件

        private void BtnAddCommand_Click(object sender, EventArgs e)
        {
            using var form = new FrmCommandEditor(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var cmd = form.Command;
                var rowIndex = dgvCommands.Rows.Add(
                    cmd.Name,
                    cmd.DisplayName,
                    cmd.CommandType.ToString(),
                    cmd.RequestTemplate
                );
                dgvCommands.Rows[rowIndex].Tag = cmd;
            }
        }

        private void BtnEditCommand_Click(object sender, EventArgs e)
        {
            if (dgvCommands.SelectedRows.Count == 0)
            {
                UIMessageTip.ShowWarning("请选择要编辑的命令");
                return;
            }

            var cmd = dgvCommands.SelectedRows[0].Tag as InstrumentCommand;
            using var form = new FrmCommandEditor(cmd);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var updated = form.Command;
                var row = dgvCommands.SelectedRows[0];
                row.Cells["Name"].Value = updated.Name;
                row.Cells["DisplayName"].Value = updated.DisplayName;
                row.Cells["CommandType"].Value = updated.CommandType.ToString();
                row.Cells["RequestTemplate"].Value = updated.RequestTemplate;
                row.Tag = updated;
            }
        }

        private void BtnDeleteCommand_Click(object sender, EventArgs e)
        {
            if (dgvCommands.SelectedRows.Count == 0)
            {
                UIMessageTip.ShowWarning("请选择要删除的命令");
                return;
            }

            if (MessageHelper.MessageYes("确定要删除选中的命令吗？") != DialogResult.OK)
                return;

            dgvCommands.Rows.Remove(dgvCommands.SelectedRows[0]);
        }

        private void DgvCommands_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEditCommand_Click(sender, e);
            }
        }

        #endregion

        #region 底部按钮事件

        private void BtnTestConnection_Click(object sender, EventArgs e)
        {
            UIMessageTip.ShowWarning("测试连接功能需要配置实际的通讯参数后使用");
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                var driver = CollectDriverFromForm();

                bool result;
                if (_selectedDriver == null || string.IsNullOrEmpty(_selectedDriver.DriverId))
                {
                    // 新建
                    result = await _driverService.AddDriverAsync(driver);
                }
                else
                {
                    // 更新
                    driver.DriverId = _selectedDriver.DriverId;
                    result = await _driverService.UpdateDriverAsync(driver);
                }

                if (result)
                {
                    MessageHelper.MessageOK("保存成功");
                    await LoadDriversAsync();

                    // 选中刚保存的驱动
                    foreach (DataGridViewRow row in dgvDrivers.Rows)
                    {
                        if (row.Tag is InstrumentDriver d && d.Name == driver.Name)
                        {
                            row.Selected = true;
                            break;
                        }
                    }
                }
                else
                {
                    MessageHelper.MessageOK(("保存失败"));
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存驱动失败");
                MessageHelper.MessageOK(($"保存失败: {ex.Message}"));
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                UIMessageTip.ShowWarning("请输入驱动名称");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                UIMessageTip.ShowWarning("请输入显示名称");
                txtDisplayName.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region 辅助方法

        private string GetCategoryDisplayName(InstrumentCategory category)
        {
            return category switch
            {
                InstrumentCategory.Multimeter => "万用表",
                InstrumentCategory.Oscilloscope => "示波器",
                InstrumentCategory.PowerSupply => "电源",
                InstrumentCategory.SignalGenerator => "信号发生器",
                InstrumentCategory.Sensor => "传感器",
                InstrumentCategory.PLC => "PLC",
                InstrumentCategory.Other => "其他",
                _ => category.ToString()
            };
        }

        #endregion
    }
}
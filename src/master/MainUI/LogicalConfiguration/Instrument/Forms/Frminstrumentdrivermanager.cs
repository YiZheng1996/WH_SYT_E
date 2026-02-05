using AntdUI;
using MainUI.LogicalConfiguration.Instrument.Models;
using MainUI.LogicalConfiguration.Instrument.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Label = System.Windows.Forms.Label;

namespace MainUI.LogicalConfiguration.Instrument.Forms
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
            cboCategory.DataSource = EnumExtensions.GetEnumItems<InstrumentCategory>();
            cboCategory.DisplayMember = "DisplayName";
            cboCategory.ValueMember = "Value";
            cboCategory.SelectedIndex = 0;

            // 初始化协议类型下拉框 - 显示Description
            cboProtocolType.DataSource = EnumExtensions.GetEnumItems<ProtocolType>();
            cboProtocolType.DisplayMember = "DisplayName";
            cboProtocolType.ValueMember = "Value";
            cboProtocolType.SelectedIndex = 0;

            // 初始化校验类型下拉框 - 显示Description
            cboChecksumType.DataSource = EnumExtensions.GetEnumItems<ChecksumType>();
            cboChecksumType.DisplayMember = "DisplayName";
            cboChecksumType.ValueMember = "Value";
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
                _drivers = (await _driverService.GetAllDriversIncludingDisabledAsync()).ToList();
                RefreshDriverList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载驱动列表失败");
                MessageHelper.MessageOK(this, $"加载驱动列表失败: {ex.Message}");
            }
        }

        private void RefreshDriverList()
        {
            dgvDrivers.Rows.Clear();

            foreach (var driver in _drivers)
            {
                var rowIndex = dgvDrivers.Rows.Add(
                    driver.DisplayName,
                    driver.Category.GetDescription(),
                    driver.ProtocolType.GetDescription(),
                    driver.Enabled ? "启用" : "禁用"
                );
                dgvDrivers.Rows[rowIndex].Tag = driver;

                // 为禁用的驱动添加视觉提示
                if (driver.Enabled) continue;

                var row = dgvDrivers.Rows[rowIndex];

                // 灰色文字
                row.DefaultCellStyle.ForeColor = Color.Gray;

                // 斜体字
                row.DefaultCellStyle.Font = new Font(dgvDrivers.Font, FontStyle.Italic);

                // 浅灰色背景
                row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
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
            cboCategory.SelectedValue = driver.Category;
            cboProtocolType.SelectedValue = driver.ProtocolType;
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

            // 更新协议配置面板
            UpdateProtocolConfigPanel();

            // 填充数据
            switch (driver.ProtocolType)
            {
                case ProtocolType.TcpIp:
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

                case ProtocolType.Http:
                    if (config is HttpProtocolConfig http)
                    {
                        SetControlValue("BaseUrl", http.BaseUrl);
                        SetControlValue("AuthType", http.AuthType);
                        SetControlValue("Username", http.Username);
                        SetControlValue("Password", http.Password);
                        SetControlValue("ContentType", http.ContentType);
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
                cboChecksumType.SelectedValue = ChecksumType.None;
                return;
            }

            chkFrameEnabled.Checked = config.Enabled;
            txtFrameHeader.Text = config.FrameHeader;
            txtFrameFooter.Text = config.FrameFooter;
            txtResponseTerminator.Text = config.ResponseTerminator;
            cboChecksumType.SelectedValue = config.ChecksumType;
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
                    cmd.CommandType.GetDescription(),
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

        /// <summary>
        /// 根据协议类型更新配置面板
        /// </summary>
        private void UpdateProtocolConfigPanel()
        {
            panelProtocolConfig.Controls.Clear();
            _protocolControls.Clear();

            var protocolType = cboProtocolType.SelectedValue is ProtocolType pt
                ? pt
                : ProtocolType.TcpIp;

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

            // 使用正确的枚举值,并传递layout参数
            switch (protocolType)
            {
                case ProtocolType.TcpIp:
                    CreateTcpConfigControls(layout);
                    break;

                case ProtocolType.Serial:
                    CreateSerialConfigControls(layout);
                    break;

                case ProtocolType.ModbusRtu:
                    CreateModbusRtuConfigControls(layout);
                    break;

                case ProtocolType.ModbusTcp:
                    CreateModbusTcpConfigControls(layout);
                    break;

                case ProtocolType.Http:
                    CreateHttpConfigControls(layout);
                    break;

                case ProtocolType.Udp:
                    CreateUdpConfigControls(layout);
                    break;

                default:
                    // 默认显示TCP配置
                    CreateTcpConfigControls(layout);
                    break;
            }

            panelProtocolConfig.Controls.Add(layout);
        }

        #region 协议配置控件创建方法

        private void CreateTcpConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            // IP地址 - 添加IP地址验证
            AddConfigRow(layout, row, "IP地址:", "IpAddress", "192.168.1.100", 0, ValidationType.IpAddress);
            // 端口 - 添加端口号验证
            AddConfigRow(layout, row++, "端口:", "Port", "5000", 2, ValidationType.Port);

            // 连接超时 - 添加超时验证
            AddConfigRow(layout, row, "连接超时:", "ConnectionTimeout", "5000", 0, ValidationType.Timeout);
            // 读取超时 - 添加超时验证
            AddConfigRow(layout, row++, "读取超时:", "ReadTimeout", "3000", 2, ValidationType.Timeout);

            AddConfigRow(layout, row, "保持连接:", "KeepAlive", "true", 0);
        }

        private void CreateSerialConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            // 串口名称 - 允许手动输入（用于特殊串口如 \\.\COM15）
            AddConfigComboBox(layout, row++, "串口:", "PortName",
                GetAvailablePorts(), "COM1", 0, allowCustomInput: true);

            // 波特率 - 允许手动输入（用于特殊波特率）
            AddConfigComboBox(layout, row, "波特率:", "BaudRate",
                BaudRates, 9600, 0, allowCustomInput: true);
            // 数据位 - 不允许手动输入（固定选项）
            AddConfigComboBox(layout, row++, "数据位:", "DataBits",
                DataBitsList, 8, 2, allowCustomInput: false);

            // 停止位 - 使用枚举下拉框
            AddConfigComboBox(layout, row, "停止位:", "StopBits",
                Enum.GetValues(typeof(StopBitsType)), StopBitsType.One, 0);
            // 校验位 - 使用枚举下拉框
            AddConfigComboBox(layout, row++, "校验位:", "Parity",
                Enum.GetValues(typeof(ParityType)), ParityType.None, 2);

            // 流控制 - 使用枚举下拉框
            AddConfigComboBox(layout, row, "流控制:", "FlowControl",
                Enum.GetValues(typeof(FlowControlType)), FlowControlType.None, 0);
            // 读取超时 - 保持文本框
            AddConfigRow(layout, row++, "读取超时:", "ReadTimeout", "3000", 2);
        }

        private void CreateModbusRtuConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            // 从站地址 - 添加正整数验证
            AddConfigRow(layout, row, "从站地址:", "SlaveAddress", "1", 0, ValidationType.PositiveInteger);
            AddConfigComboBox(layout, row++, "串口:", "PortName",
                GetAvailablePorts(), "COM1", 2, allowCustomInput: true);

            AddConfigComboBox(layout, row, "波特率:", "BaudRate",
                BaudRates, 9600, 0, allowCustomInput: true);
            AddConfigComboBox(layout, row++, "数据位:", "DataBits", DataBitsList, 8, 2);

            AddConfigComboBox(layout, row, "停止位:", "StopBits",
                Enum.GetValues(typeof(StopBitsType)), StopBitsType.One, 0);
            AddConfigComboBox(layout, row++, "校验位:", "Parity",
                Enum.GetValues(typeof(ParityType)), ParityType.None, 2);

            AddConfigComboBox(layout, row, "字节序:", "ByteOrder", ByteOrders, "BigEndian", 0);
            // 读取超时 - 添加超时验证
            AddConfigRow(layout, row++, "读取超时:", "ReadTimeout", "3000", 2, ValidationType.Timeout);
        }

        private void CreateModbusTcpConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            AddConfigRow(layout, row, "从站地址:", "SlaveAddress", "1", 0);
            AddConfigRow(layout, row++, "IP地址:", "IpAddress", "192.168.1.100", 2);

            AddConfigRow(layout, row, "端口:", "Port", "502", 0);
            AddConfigComboBox(layout, row++, "字节序:", "ByteOrder", ByteOrders, "BigEndian", 2);

            AddConfigRow(layout, row, "连接超时:", "ConnectionTimeout", "5000", 0);
            AddConfigRow(layout, row++, "读取超时:", "ReadTimeout", "3000", 2);
        }

        private void CreateHttpConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            // 基础URL - 添加URL验证
            AddConfigRow(layout, row++, "基础URL:", "BaseUrl",
                "http://192.168.0.100", 0, ValidationType.Url);

            AddConfigComboBox(layout, row, "认证类型:", "AuthType",
                AuthTypes, "None", 0, allowCustomInput: false);
            AddConfigComboBox(layout, row++, "内容类型:", "ContentType",
                ContentTypes, "application/json", 2, allowCustomInput: true);

            AddConfigRow(layout, row, "用户名:", "Username", "", 0);
            AddConfigRow(layout, row++, "密码:", "Password", "", 2);
        }

        private void CreateUdpConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            AddConfigRow(layout, row, "远程IP:", "RemoteIpAddress", "192.168.1.100", 0);
            AddConfigRow(layout, row++, "远程端口:", "RemotePort", "5000", 2);

            AddConfigRow(layout, row, "本地端口:", "LocalPort", "0", 0);
            AddConfigRow(layout, row++, "读取超时:", "ReadTimeout", "3000", 2);
        }

        #endregion

        /// <summary>
        /// 添加文本框配置行（支持验证）
        /// </summary>
        private void AddConfigRow(TableLayoutPanel layout, int row, string label,
            string name, string defaultValue, int col = 0, ValidationType? validationType = null)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("微软雅黑", 11F)
            };

            var txt = new UITextBox
            {
                Dock = DockStyle.Fill,
                Text = defaultValue,
                Name = name
            };

            // 添加验证
            if (validationType.HasValue)
            {
                AddValidationToTextBox(txt, validationType.Value);
            }

            layout.Controls.Add(lbl, col, row);
            layout.Controls.Add(txt, col + 1, row);

            _protocolControls[name] = txt;
        }

        // 支持下拉框的重载方法
        /// <summary>
        /// 添加下拉框配置行
        /// </summary>
        /// <param name="allowCustomInput">是否允许用户手动输入（默认false只能选择）</param>
        private void AddConfigComboBox(TableLayoutPanel layout, int row, string label,
            string name, object dataSource, object defaultValue, int col = 0, bool allowCustomInput = false)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("微软雅黑", 11F)
            };

            var cbo = new UIComboBox
            {
                Dock = DockStyle.Fill,
                Name = name,
                // 根据参数决定是否允许手动输入
                DropDownStyle = allowCustomInput ? UIDropDownStyle.DropDown : UIDropDownStyle.DropDownList
            };

            switch (dataSource)
            {
                // 根据数据源类型设置下拉框
                // 枚举类型
                case Array enumArray:
                {
                    cbo.DataSource = enumArray;
                    if (defaultValue != null)
                        cbo.SelectedItem = defaultValue;
                    break;
                }
                // 字符串列表
                case IEnumerable<string> stringList:
                {
                    cbo.DataSource = new List<string>(stringList);
                    if (defaultValue != null)
                    {
                        var index = ((List<string>)cbo.DataSource).IndexOf(defaultValue.ToString());
                        if (index >= 0)
                            cbo.SelectedIndex = index;
                        else if (allowCustomInput)
                            cbo.Text = defaultValue.ToString(); // 允许输入时直接设置文本
                    }

                    break;
                }
                // 整数列表
                case IEnumerable<int> intList:
                {
                    cbo.DataSource = new List<int>(intList);
                    if (defaultValue != null)
                    {
                        var value = Convert.ToInt32(defaultValue);
                        var index = ((List<int>)cbo.DataSource).IndexOf(value);
                        if (index >= 0)
                            cbo.SelectedIndex = index;
                        else if (allowCustomInput)
                            cbo.Text = value.ToString(); // 允许输入时直接设置文本
                    }

                    break;
                }
            }

            layout.Controls.Add(lbl, col, row);
            layout.Controls.Add(cbo, col + 1, row);

            _protocolControls[name] = cbo;
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
                    if (value is Enum enumValue)
                    {
                        // 枚举类型直接设置
                        cbo.SelectedItem = enumValue;
                    }
                    else if (value is int intValue && cbo.DataSource is List<int>)
                    {
                        // 整数类型（如波特率）
                        cbo.SelectedItem = intValue;
                    }
                    else if (value is string strValue)
                    {
                        // 字符串类型尝试匹配
                        cbo.SelectedItem = strValue;

                        // 如果没找到匹配项，尝试解析枚举
                        if (cbo.SelectedItem == null && cbo.DataSource is Array)
                        {
                            foreach (var item in (Array)cbo.DataSource)
                            {
                                if (item.ToString() == strValue)
                                {
                                    cbo.SelectedItem = item;
                                    break;
                                }
                            }
                        }
                    }
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

            try
            {
                switch (control)
                {
                    case UITextBox txt:
                        return ConvertValue<T>(txt.Text, defaultValue);

                    case UIComboBox cbo:
                        if (cbo.SelectedItem == null)
                            return defaultValue;

                        // 如果目标类型是枚举且选中项也是枚举
                        if (typeof(T).IsEnum && cbo.SelectedItem is Enum)
                            return (T)cbo.SelectedItem;

                        // 如果目标类型是 int 且选中项是 int
                        if (typeof(T) == typeof(int) && cbo.SelectedItem is int)
                            return (T)cbo.SelectedItem;

                        // 如果目标类型是 string
                        if (typeof(T) == typeof(string))
                            return (T)(object)cbo.SelectedItem.ToString();

                        // 尝试转换选中项的字符串表示
                        return ConvertValue<T>(cbo.SelectedItem.ToString(), defaultValue);

                    case UICheckBox chk:
                        if (typeof(T) == typeof(bool))
                            return (T)(object)chk.Checked;
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, $"获取控件 {name} 的值失败，使用默认值");
            }

            return defaultValue;
        }

        // 辅助转换方法
        private T ConvertValue<T>(string text, T defaultValue)
        {
            if (string.IsNullOrWhiteSpace(text))
                return defaultValue;

            try
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)text;

                if (typeof(T) == typeof(int))
                    return int.TryParse(text, out var i) ? (T)(object)i : defaultValue;

                if (typeof(T) == typeof(byte))
                    return byte.TryParse(text, out var b) ? (T)(object)b : defaultValue;

                if (typeof(T).IsEnum)
                    return Enum.TryParse(typeof(T), text, true, out var enumValue)
                        ? (T)enumValue : defaultValue;
            }
            catch
            {
                // 转换失败返回默认值
            }

            return defaultValue;
        }

        #endregion

        #region 数据收集

        private InstrumentDriver CollectDriverFromForm()
        {
            var driver = _selectedDriver ?? new InstrumentDriver();

            // 如果是更新操作，保留原 DriverId
            if (_selectedDriver != null && !string.IsNullOrEmpty(_selectedDriver.DriverId))
            {
                driver.DriverId = _selectedDriver.DriverId;
                driver.CreatedTime = _selectedDriver.CreatedTime; // 保留创建时间
            }

            driver.Name = txtName.Text.Trim();
            driver.DisplayName = txtDisplayName.Text.Trim();
            driver.Category = (InstrumentCategory)(cboCategory.SelectedValue ?? InstrumentCategory.Other);
            driver.ProtocolType = (ProtocolType)(cboProtocolType.SelectedValue ?? ProtocolType.TcpIp);
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
                ChecksumType = (ChecksumType)(cboChecksumType.SelectedValue ?? ChecksumType.None)
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
            var protocolType = (ProtocolType)(cboProtocolType.SelectedValue ?? ProtocolType.TcpIp);

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
                    var modbusTcp = new ModbusProtocolConfig();
                    modbusTcp.SetModbusType(true); // TCP模式
                    modbusTcp.SlaveAddress = GetControlValue<byte>("SlaveAddress", 1);
                    modbusTcp.IpAddress = GetControlValue<string>("IpAddress");
                    modbusTcp.Port = GetControlValue<int>("Port", 502);
                    modbusTcp.ByteOrder = GetControlValue<ByteOrder>("ByteOrder", ByteOrder.BigEndian);
                    modbusTcp.ReadTimeout = GetControlValue<int>("ReadTimeout", 3000);
                    return modbusTcp;

                case ProtocolType.ModbusRtu:
                    var modbusRtu = new ModbusProtocolConfig();
                    modbusRtu.SetModbusType(false); // RTU模式
                    modbusRtu.SlaveAddress = GetControlValue<byte>("SlaveAddress", 1);
                    modbusRtu.PortName = GetControlValue<string>("PortName");
                    modbusRtu.BaudRate = GetControlValue<int>("BaudRate", 9600);
                    modbusRtu.DataBits = GetControlValue<int>("DataBits", 8);
                    modbusRtu.StopBits = GetControlValue<StopBitsType>("StopBits", StopBitsType.One);
                    modbusRtu.Parity = GetControlValue<ParityType>("Parity", ParityType.None);
                    modbusRtu.ByteOrder = GetControlValue<ByteOrder>("ByteOrder", ByteOrder.BigEndian);
                    modbusRtu.ReadTimeout = GetControlValue<int>("ReadTimeout", 3000);
                    return modbusRtu;

                case ProtocolType.Http:
                    return new HttpProtocolConfig
                    {
                        BaseUrl = GetControlValue<string>("BaseUrl"),
                        //AuthType = GetControlValue<HttpAuthType>("AuthType", HttpAuthType.None),
                        Username = GetControlValue<string>("Username"),
                        Password = GetControlValue<string>("Password"),
                        ContentType = GetControlValue<string>("ContentType", "application/json")
                    };

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
                MessageHelper.MessageOK(this, "请先选择要编辑的驱动");
                return;
            }
            txtName.Focus();
        }

        /// <summary>
        /// 删除驱动按钮点击事件
        /// </summary>
        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedDriver == null)
            {
                MessageHelper.MessageOK(this, "请先选择要删除的驱动");
                return;
            }

            if (MessageHelper.MessageYes(this, $"确定要删除驱动 [{_selectedDriver.DisplayName}] 吗？") != DialogResult.OK)
                return;

            try
            {
                var result = await _driverService.DeleteDriverAsync(_selectedDriver.DriverId);

                // 根据返回值判断是否真正删除成功
                if (result)
                {
                    // 从内存列表中移除
                    _drivers.Remove(_selectedDriver);

                    // 刷新界面
                    RefreshDriverList();
                    ClearForm();

                    MessageHelper.MessageOK(this, "删除成功");
                    _logger?.LogInformation("删除驱动成功: {DisplayName} (ID: {DriverId})",
                        _selectedDriver.DisplayName,
                        _selectedDriver.DriverId);
                }
                else
                {
                    // 删除失败，可能是驱动不存在或已被删除
                    MessageHelper.MessageOK(this, "删除失败：驱动不存在或已被删除");
                    _logger?.LogWarning("删除驱动失败: {DisplayName} (ID: {DriverId})",
                        _selectedDriver.DisplayName,
                        _selectedDriver.DriverId);

                    // 刷新列表，确保界面数据与实际一致
                    await LoadDriversAsync();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除驱动时发生异常: {DisplayName}", _selectedDriver.DisplayName);
                MessageHelper.MessageOK(this, $"删除失败: {ex.Message}");
            }
        }

        private void BtnClone_Click(object sender, EventArgs e)
        {
            if (_selectedDriver == null)
            {
                MessageHelper.MessageOK(this, "请先选择要复制的驱动");
                return;
            }

            var cloned = _selectedDriver.Clone();
            cloned.Name = $"{_selectedDriver.Name}_Copy";
            cloned.DisplayName = $"{_selectedDriver.DisplayName} (副本)";

            // 清空 DriverId，表示这是新驱动
            cloned.DriverId = null;  // 或 string.Empty

            _selectedDriver = cloned;
            LoadDriverToForm(cloned);
            MessageHelper.MessageOK(this, "已复制，请修改后保存");
        }

        private async void BtnImport_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "JSON文件|*.json",
                Title = "导入仪器驱动配置",
                Multiselect = false  // 单个文件，但文件内可以是数组
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var json = await File.ReadAllTextAsync(ofd.FileName);

                List<InstrumentDriver> driversToImport = new();

                // 尝试解析为数组
                try
                {
                    var array = JsonConvert.DeserializeObject<List<InstrumentDriver>>(json);
                    if (array != null && array.Count > 0)
                    {
                        driversToImport = array;
                    }
                }
                catch
                {
                    // 尝试解析为单个对象
                    var single = JsonConvert.DeserializeObject<InstrumentDriver>(json);
                    if (single != null)
                    {
                        driversToImport.Add(single);
                    }
                }

                if (driversToImport.Count == 0)
                {
                    MessageHelper.MessageOK(this, "文件格式无效或无有效数据", TType.Warn);
                    return;
                }

                // 批量导入
                int successCount = 0;
                int skipCount = 0;
                List<string> skippedNames = new();

                foreach (var driver in driversToImport)
                {
                    // 生成新ID
                    driver.DriverId = Guid.NewGuid().ToString("N");

                    // 检查名称冲突
                    var existing = _drivers.FirstOrDefault(d =>
                        d.Name.Equals(driver.Name, StringComparison.OrdinalIgnoreCase));

                    if (existing != null)
                    {
                        driver.Name = $"{driver.Name}_{DateTime.Now:yyyyMMddHHmmss}";
                        driver.DisplayName = $"{driver.DisplayName} (导入)";
                    }

                    if (await _driverService.AddDriverAsync(driver))
                    {
                        successCount++;
                    }
                    else
                    {
                        skipCount++;
                        skippedNames.Add(driver.DisplayName);
                    }
                }

                await LoadDriversAsync();

                // 显示导入结果
                var message = $"导入完成!\n\n成功: {successCount} 个\n失败: {skipCount} 个";
                if (skippedNames.Count > 0)
                {
                    message += $"\n\n失败列表:\n{string.Join("\n", skippedNames)}";
                }

                MessageHelper.MessageOK(this, message,
                    skipCount == 0 ? TType.Success : TType.Warn);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导入失败");
                MessageHelper.MessageOK(this, $"导入失败: {ex.Message}", TType.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (_drivers == null || _drivers.Count == 0)
            {
                MessageHelper.MessageOK(this, "没有可导出的驱动配置");
                return;
            }

            // 询问导出范围
            bool exportAll;
            if (dgvDrivers.SelectedRows.Count == 0)
            {
                exportAll = true; // 无选中则导出全部
            }
            else
            {
                var result = MessageHelper.MessageYes(this,
                    $"当前选中 {dgvDrivers.SelectedRows.Count} 个驱动\n\n是否导出【全部】驱动?\n\n选择'否'将只导出选中的驱动");
                exportAll = (result == DialogResult.OK);
            }

            List<InstrumentDriver> driversToExport;
            string defaultFileName;

            if (exportAll)
            {
                // 导出全部
                driversToExport = _drivers;
                defaultFileName = $"InstrumentDrivers_All_{DateTime.Now:yyyyMMdd}.json";
            }
            else
            {
                // 导出选中
                driversToExport = dgvDrivers.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Select(r => r.Tag as InstrumentDriver)
                    .Where(d => d != null)
                    .ToList();

                if (driversToExport.Count == 0)
                {
                    MessageHelper.MessageOK(this, "请先选择要导出的驱动");
                    return;
                }

                defaultFileName = driversToExport.Count == 1
                    ? $"{driversToExport[0].Name}_{DateTime.Now:yyyyMMdd}.json"
                    : $"InstrumentDrivers_Selected{driversToExport.Count}_{DateTime.Now:yyyyMMdd}.json";
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "JSON文件|*.json",
                Title = "导出仪器驱动配置",
                FileName = defaultFileName
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var json = JsonConvert.SerializeObject(
                    driversToExport.Count == 1 ? (object)driversToExport[0] : driversToExport,
                    Formatting.Indented);

                File.WriteAllText(dialog.FileName, json);

                MessageHelper.MessageOK(this,
                    $"导出成功!\n\n数量: {driversToExport.Count} 个驱动\n文件: {Path.GetFileName(dialog.FileName)}",
                    TType.Success);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导出驱动配置失败");
                MessageHelper.MessageOK(this, $"导出失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 命令模板事件

        private void BtnAddCommand_Click(object sender, EventArgs e)
        {
            using var form = new FrmCommandEditor(null);
            if (VarHelper.ShowDialogWithOverlayEx(this, form) != DialogResult.OK) return;

            var cmd = form.Command;
            var rowIndex = dgvCommands.Rows.Add(
                cmd.Name,
                cmd.DisplayName,
                cmd.CommandType.ToString(),
                cmd.RequestTemplate
            );
            dgvCommands.Rows[rowIndex].Tag = cmd;
        }

        private void BtnEditCommand_Click(object sender, EventArgs e)
        {
            if (dgvCommands.SelectedRows.Count == 0)
            {
                MessageHelper.MessageOK(this, "请选择要编辑的命令");
                return;
            }

            var cmd = dgvCommands.SelectedRows[0].Tag as InstrumentCommand;
            using var form = new FrmCommandEditor(cmd);
            if (VarHelper.ShowDialogWithOverlayEx(this, form) != DialogResult.OK) return;

            var updated = form.Command;
            var row = dgvCommands.SelectedRows[0];
            row.Cells["Name"].Value = updated.Name;
            row.Cells["DisplayName"].Value = updated.DisplayName;
            row.Cells["CommandType"].Value = updated.CommandType.GetDescription();
            row.Cells["RequestTemplate"].Value = updated.RequestTemplate;
            row.Tag = updated;
        }

        private void BtnDeleteCommand_Click(object sender, EventArgs e)
        {
            if (dgvCommands.SelectedRows.Count == 0)
            {
                MessageHelper.MessageOK(this, "请选择要删除的命令");
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
                    // 通过 DriverId 或 Name 选中
                    foreach (DataGridViewRow row in dgvDrivers.Rows)
                    {
                        if (row.Tag is not InstrumentDriver d ||
                            (d.DriverId != driver.DriverId && d.Name != driver.Name)) continue;

                        row.Selected = true;
                        dgvDrivers.FirstDisplayedScrollingRowIndex = row.Index; // 滚动到可见位置
                        break;
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
            // 驱动名称检查
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageHelper.MessageOK(this, "请输入驱动名称");
                txtName.Focus();
                return false;
            }

            // 显示名称检查
            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageHelper.MessageOK(this, "请输入显示名称");
                txtDisplayName.Focus();
                return false;
            }

            // 驱动名称唯一性检查（新建或修改名称时）
            if (_selectedDriver == null ||
                string.IsNullOrEmpty(_selectedDriver.DriverId) ||
                _selectedDriver.Name != txtName.Text.Trim())
            {
                var existingDriver = _drivers.FirstOrDefault(d =>
                    d.Name.Equals(txtName.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    d.DriverId != _selectedDriver?.DriverId);

                if (existingDriver != null)
                {
                    MessageHelper.MessageOK(this, "驱动名称已存在，请使用其他名称");
                    txtName.Focus();
                    return false;
                }
            }

            // 协议配置检查
            var protocolType = (ProtocolType)(cboProtocolType.SelectedValue ?? ProtocolType.TcpIp);
            switch (protocolType)
            {
                case ProtocolType.TcpIp:
                    if (string.IsNullOrWhiteSpace(GetControlValue<string>("IpAddress")))
                    {
                        MessageHelper.MessageOK(this, "请输入 IP 地址");
                        return false;
                    }
                    break;
                case ProtocolType.Serial:
                    if (string.IsNullOrWhiteSpace(GetControlValue<string>("PortName")))
                    {
                        MessageHelper.MessageOK(this, "请选择串口");
                        return false;
                    }
                    break;
                case ProtocolType.ModbusTcp:
                case ProtocolType.ModbusRtu:
                case ProtocolType.Http:
                case ProtocolType.Udp:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        #endregion

        #region 协议配置下拉框选项

        // 串口号列表
        private static readonly List<string> PortNames = new()
        {
            "COM1", "COM2", "COM3", "COM4", "COM5",
            "COM6", "COM7", "COM8", "COM9", "COM10"
        };

        // 波特率列表
        private static readonly List<int> BaudRates = new()
        {
            1200, 2400, 4800, 9600, 14400, 19200,
            38400, 57600, 115200, 128000, 256000
        };

        // 数据位列表
        private static readonly List<int> DataBitsList = new() { 5, 6, 7, 8 };

        // 字节序选项
        private static readonly List<string> ByteOrders = new()
        {
            "BigEndian", "LittleEndian"
        };

        // HTTP认证类型
        private static readonly List<string> AuthTypes = new()
        {
            "None", "Basic", "Bearer", "ApiKey"
        };

        // HTTP内容类型
        private static readonly List<string> ContentTypes = new()
        {
            "application/json",
            "application/xml",
            "text/plain",
            "application/x-www-form-urlencoded"
        };

        /// <summary>
        /// 获取系统可用串口列表（动态检测）
        /// </summary>
        private static List<string> GetAvailablePorts()
        {
            try
            {
                var ports = System.IO.Ports.SerialPort.GetPortNames()
                    .OrderBy(p => p)
                    .ToList();

                // 如果没有检测到串口，至少提供一个默认选项
                if (!ports.Any())
                {
                    ports.Add("COM1");
                }

                return ports;
            }
            catch (Exception)
            {
                // 检测失败时返回默认串口列表
                return new List<string>
                {
                    "COM1", "COM2", "COM3", "COM4", "COM5",
                    "COM6", "COM7", "COM8", "COM9", "COM10"
                };
            }
        }

        #endregion

        #region 输入验证

        /// <summary>
        /// 为文本框添加实时验证
        /// </summary>
        private void AddValidationToTextBox(UITextBox textBox, ValidationType type)
        {
            textBox.TextChanged += (s, e) => ValidateTextBox(textBox, type);
            textBox.Leave += (s, e) => ValidateTextBox(textBox, type);
        }

        /// <summary>
        /// 验证类型枚举
        /// </summary>
        private enum ValidationType
        {
            IpAddress,      // IP地址
            Port,           // 端口号
            Timeout,        // 超时时间
            PositiveInteger,// 正整数
            Url,            // URL地址
            NotEmpty        // 非空
        }

        /// <summary>
        /// 验证文本框内容
        /// </summary>
        private void ValidateTextBox(UITextBox textBox, ValidationType type)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                // 空值处理
                if (type != ValidationType.NotEmpty)
                {
                    ResetTextBoxStyle(textBox);
                    return;
                }
            }

            bool isValid = type switch
            {
                ValidationType.IpAddress => ValidateIpAddress(textBox.Text),
                ValidationType.Port => ValidatePort(textBox.Text),
                ValidationType.Timeout => ValidateTimeout(textBox.Text),
                ValidationType.PositiveInteger => ValidatePositiveInteger(textBox.Text),
                ValidationType.Url => ValidateUrl(textBox.Text),
                ValidationType.NotEmpty => !string.IsNullOrWhiteSpace(textBox.Text),
                _ => true
            };

            if (isValid)
            {
                ResetTextBoxStyle(textBox);
                textBox.Watermark = null;
            }
            else
            {
                SetErrorStyle(textBox);
                textBox.Watermark = GetValidationMessage(type);
            }
        }

        /// <summary>
        /// 设置错误样式
        /// </summary>
        private void SetErrorStyle(UITextBox textBox)
        {
            textBox.RectColor = Color.Red;
            textBox.ForeColor = Color.Red;
        }

        /// <summary>
        /// 重置文本框样式
        /// </summary>
        private void ResetTextBoxStyle(UITextBox textBox)
        {
            textBox.RectColor = Color.FromArgb(220, 220, 220);
            textBox.ForeColor = Color.Black;
        }

        /// <summary>
        /// 获取验证提示消息
        /// </summary>
        private string GetValidationMessage(ValidationType type)
        {
            return type switch
            {
                ValidationType.IpAddress => "请输入有效的IP地址（例如：192.168.1.100）",
                ValidationType.Port => "请输入有效的端口号（1-65535）",
                ValidationType.Timeout => "请输入有效的超时时间（毫秒，0-300000）",
                ValidationType.PositiveInteger => "请输入正整数",
                ValidationType.Url => "请输入有效的URL地址",
                ValidationType.NotEmpty => "此字段不能为空",
                _ => "输入格式不正确"
            };
        }

        #region 具体验证方法

        private bool ValidateIpAddress(string input)
        {
            return System.Net.IPAddress.TryParse(input, out _);
        }

        private bool ValidatePort(string input)
        {
            return int.TryParse(input, out var port) && port > 0 && port <= 65535;
        }

        private bool ValidateTimeout(string input)
        {
            return int.TryParse(input, out var timeout) && timeout >= 0 && timeout <= 300000;
        }

        private bool ValidatePositiveInteger(string input)
        {
            return int.TryParse(input, out var value) && value > 0;
        }

        private bool ValidateUrl(string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out var uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

        #endregion

        #endregion
    }
}
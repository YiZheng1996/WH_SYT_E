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
        private List<InstrumentDriver> _drivers = [];
        private InstrumentDriver _selectedDriver;
        private Dictionary<string, Control> _protocolControls = [];

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

                if (driver.Enabled) continue;
                var row = dgvDrivers.Rows[rowIndex];
                row.DefaultCellStyle.ForeColor = Color.Gray;
                row.DefaultCellStyle.Font = new Font(dgvDrivers.Font, FontStyle.Italic);
                row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            }

            // 只有当前不是新建状态时，才自动选中第一行
            // 新建状态的判断：_selectedDriver 不为null 但 DriverId 为空
            bool isAddingNew = _selectedDriver != null && string.IsNullOrEmpty(_selectedDriver.DriverId);
            if (!isAddingNew && dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.SelectionChanged -= DgvDrivers_SelectionChanged; // 防止触发覆盖
                dgvDrivers.Rows[0].Selected = true;
                dgvDrivers.SelectionChanged += DgvDrivers_SelectionChanged;
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
                        SetControlValue("ConnectionTimeout", serial.ConnectionTimeout.ToString());
                        SetControlValue("ReadTimeout", serial.ReadTimeout.ToString());
                    }
                    break;

                case ProtocolType.ModbusTcp:
                    if (config is ModbusProtocolConfig modbusTcp)
                    {
                        SetControlValue("SlaveAddress", modbusTcp.SlaveAddress.ToString());
                        SetControlValue("IpAddress", modbusTcp.IpAddress);
                        SetControlValue("Port", modbusTcp.Port.ToString());
                        SetControlValue("ByteOrder", modbusTcp.ByteOrder);
                        SetControlValue("ConnectionTimeout", modbusTcp.ConnectionTimeout.ToString());
                        SetControlValue("ReadTimeout", modbusTcp.ReadTimeout.ToString());
                        SetControlValue("SwapBytes", modbusTcp.SwapBytes);
                        SetControlValue("SwapWords", modbusTcp.SwapWords);
                    }
                    break;

                case ProtocolType.ModbusRtu:
                    if (config is ModbusProtocolConfig modbusRtu)
                    {
                        SetControlValue("SlaveAddress", modbusRtu.SlaveAddress.ToString());
                        SetControlValue("PortName", modbusRtu.PortName);
                        SetControlValue("BaudRate", modbusRtu.BaudRate.ToString());
                        SetControlValue("DataBits", modbusRtu.DataBits);
                        SetControlValue("StopBits", modbusRtu.StopBits);
                        SetControlValue("Parity", modbusRtu.Parity);
                        SetControlValue("ByteOrder", modbusRtu.ByteOrder);
                        SetControlValue("ReadTimeout", modbusRtu.ReadTimeout.ToString());
                        SetControlValue("SwapBytes", modbusRtu.SwapBytes);
                        SetControlValue("SwapWords", modbusRtu.SwapWords);
                    }
                    break;

                case ProtocolType.Http:
                    if (config is HttpProtocolConfig http)
                    {
                        SetControlValue("BaseUrl", http.BaseUrl);
                        SetControlValue("ContentType", http.ContentType);
                        // AuthType 赋值后手动触发联动（SelectedIndexChanged 可能未触发）
                        SetControlValue("AuthType", http.AuthType);
                        TriggerHttpAuthTypeChange(http.AuthType);
                        // 按认证类型填充对应字段
                        SetControlValue("Username", http.Username);
                        SetControlValue("Password", http.Password);
                        SetControlValue("BearerToken", http.BearerToken);
                    }
                    break;

                case ProtocolType.Udp:
                    if (config is UdpProtocolConfig udp)
                    {
                        SetControlValue("RemoteIpAddress", udp.RemoteIpAddress);
                        SetControlValue("RemotePort", udp.RemotePort.ToString());
                        SetControlValue("LocalPort", udp.LocalPort.ToString());
                        SetControlValue("ConnectionTimeout", udp.ConnectionTimeout.ToString());
                        SetControlValue("ReadTimeout", udp.ReadTimeout.ToString());
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

            // 解绑，防止触发 UpdateProtocolConfigPanel 和 _protocolControls 状态异常
            cboProtocolType.SelectedIndexChanged -= CboProtocolType_SelectedIndexChanged;
            cboProtocolType.SelectedIndex = 0;
            cboProtocolType.SelectedIndexChanged += CboProtocolType_SelectedIndexChanged;

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

            // 清表单后主动重建协议面板，确保 _protocolControls 状态正确
            UpdateProtocolConfigPanel();
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

            // 改为 CheckBox，和 SetControlValue/GetControlValue 的 bool 分支匹配
            AddConfigCheckBox(layout, row, "保持连接:", "KeepAlive", true, 0);
        }

        private void CreateSerialConfigControls(TableLayoutPanel layout)
        {
            // ── 重置行配置，避免默认行高撑大 
            layout.RowCount = 6;
            layout.RowStyles.Clear();
            for (int i = 0; i < 6; i++)
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            int row = 0;

            // ── 第0行：串口下拉（col0~1）+ 刷新按钮（col2）
            AddConfigComboBox(layout, row, "串口:", "PortName",
                GetAvailablePorts(), "COM1", 0, allowCustomInput: true);

            var btnRefreshPort = new UIButton
            {
                Text = "刷新",
                Dock = DockStyle.Fill,
                FillColor = Color.FromArgb(65, 100, 204),
                Font = new Font("微软雅黑", 10F),
                Cursor = Cursors.Hand
            };
            btnRefreshPort.Click += (s, e) =>
            {
                if (!_protocolControls.TryGetValue("PortName", out var ctrl) || ctrl is not UIComboBox cbo)
                    return;
                var current = cbo.Text;
                var ports = GetAvailablePorts();
                cbo.DataSource = new List<string>(ports);
                if (ports.Contains(current))
                    cbo.Text = current;
                else if (ports.Count > 0)
                    cbo.SelectedIndex = 0;
            };
            layout.Controls.Add(btnRefreshPort, 2, row);
            row++;

            // ── 第1行：波特率 + 数据位
            AddConfigComboBox(layout, row, "波特率:", "BaudRate",
                BaudRates, 9600, 0, allowCustomInput: true);
            AddConfigComboBox(layout, row++, "数据位:", "DataBits",
                DataBitsList, 8, 2, allowCustomInput: false);

            // ── 第2行：停止位 + 校验位
            AddConfigComboBox(layout, row, "停止位:", "StopBits",
                Enum.GetValues(typeof(StopBitsType)), StopBitsType.One, 0);
            AddConfigComboBox(layout, row++, "校验位:", "Parity",
                Enum.GetValues(typeof(ParityType)), ParityType.None, 2);

            // ── 第3行：流控制 + 连接超时
            AddConfigComboBox(layout, row, "流控制:", "FlowControl",
                Enum.GetValues(typeof(FlowControlType)), FlowControlType.None, 0);
            AddConfigRow(layout, row++, "连接超时:", "ConnectionTimeout", "5000", 2, ValidationType.Timeout);

            // ── 第4行：读取超时
            AddConfigRow(layout, row, "读取超时:", "ReadTimeout", "3000", 0, ValidationType.Timeout);
        }

        private void CreateModbusRtuConfigControls(TableLayoutPanel layout)
        {
            // ── 重置行配置，5行内容每行固定40px
            layout.RowCount = 6;
            layout.RowStyles.Clear();
            for (int i = 0; i < layout.RowCount; i++)
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            int row = 0;

            // ── 第0行：从站地址 + 串口
            AddConfigRow(layout, row, "从站地址:", "SlaveAddress", "1", 0, ValidationType.PositiveInteger);
            AddConfigComboBox(layout, row++, "串口:", "PortName",
                GetAvailablePorts(), "COM1", 2, allowCustomInput: true);

            // ── 第1行：波特率 + 数据位
            AddConfigComboBox(layout, row, "波特率:", "BaudRate",
                BaudRates, 9600, 0, allowCustomInput: true);
            AddConfigComboBox(layout, row++, "数据位:", "DataBits", DataBitsList, 8, 2);

            // ── 第2行：停止位 + 校验位
            AddConfigComboBox(layout, row, "停止位:", "StopBits",
                Enum.GetValues(typeof(StopBitsType)), StopBitsType.One, 0);
            AddConfigComboBox(layout, row++, "校验位:", "Parity",
                Enum.GetValues(typeof(ParityType)), ParityType.None, 2);

            // ── 第3行：字节序 + 读取超时
            AddConfigComboBox(layout, row, "字节序:", "ByteOrder",
                Enum.GetValues(typeof(ByteOrder)), ByteOrder.BigEndian, 0);
            AddConfigRow(layout, row++, "读取超时:", "ReadTimeout", "3000", 2, ValidationType.Timeout);

            // ── 第4行：字节交换 + 字交换
            AddConfigCheckBox(layout, row, "字节交换:", "SwapBytes", false, 0);
            AddConfigCheckBox(layout, row++, "字交换:", "SwapWords", false, 2);
        }

        private void CreateModbusTcpConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            AddConfigRow(layout, row, "从站地址:", "SlaveAddress", "1", 0, ValidationType.PositiveInteger);
            // 加 IP 地址格式验证
            AddConfigRow(layout, row++, "IP地址:", "IpAddress", "192.168.1.100", 2, ValidationType.IpAddress);

            // 加端口号验证
            AddConfigRow(layout, row, "端口:", "Port", "502", 0, ValidationType.Port);
            // ByteOrder 使用枚举
            AddConfigComboBox(layout, row++, "字节序:", "ByteOrder",
                Enum.GetValues(typeof(ByteOrder)), ByteOrder.BigEndian, 2);

            AddConfigRow(layout, row, "连接超时:", "ConnectionTimeout", "5000", 0, ValidationType.Timeout);
            AddConfigRow(layout, row++, "读取超时:", "ReadTimeout", "3000", 2, ValidationType.Timeout);

            AddConfigCheckBox(layout, row, "字节交换:", "SwapBytes", false, 0);
            AddConfigCheckBox(layout, row++, "字交换:", "SwapWords", false, 2);
        }


        private void CreateHttpConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            // 基础 URL
            AddConfigRow(layout, row++, "基础URL:", "BaseUrl",
                "http://192.168.0.100", 0, ValidationType.Url);

            // 认证类型 + 内容类型
            AddConfigComboBox(layout, row, "认证类型:", "AuthType",
                AuthTypes, "None", 0, allowCustomInput: false);
            AddConfigComboBox(layout, row++, "内容类型:", "ContentType",
                ContentTypes, "application/json", 2, allowCustomInput: true);

            // ── 动态认证区域（Basic：用户名/密码；Bearer：Token）────────────────
            // 使用一个跨 4 列的 Panel 容器承载动态内容
            _httpAuthPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                Height = 40,
                Padding = new Padding(0)
            };

            // Basic 认证控件（默认隐藏）
            var basicLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Visible = false,
                Name = "basicLayout"
            };
            basicLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            basicLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            basicLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            basicLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // 用户名
            var lblUser = new Label { Text = "用户名:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Font = new Font("微软雅黑", 11F) };
            var txtUser = new UITextBox { Dock = DockStyle.Fill, Name = "Username" };
            _protocolControls["Username"] = txtUser;

            // 密码
            var lblPwd = new Label { Text = "密码:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Font = new Font("微软雅黑", 11F) };
            var txtPwd = new UITextBox { Dock = DockStyle.Fill, Name = "Password", PasswordChar = '●' };
            _protocolControls["Password"] = txtPwd;

            basicLayout.Controls.Add(lblUser, 0, 0);
            basicLayout.Controls.Add(txtUser, 1, 0);
            basicLayout.Controls.Add(lblPwd, 2, 0);
            basicLayout.Controls.Add(txtPwd, 3, 0);

            // Bearer 认证控件（默认隐藏）
            var bearerLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Visible = false,
                Name = "bearerLayout"
            };
            bearerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            bearerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var lblToken = new Label { Text = "Token:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, Font = new Font("微软雅黑", 11F) };
            var txtToken = new UITextBox { Dock = DockStyle.Fill, Name = "BearerToken" };
            _protocolControls["BearerToken"] = txtToken;

            bearerLayout.Controls.Add(lblToken, 0, 0);
            bearerLayout.Controls.Add(txtToken, 1, 0);

            _httpAuthPanel.Controls.Add(basicLayout);
            _httpAuthPanel.Controls.Add(bearerLayout);

            // 跨 4 列放入动态认证 Panel
            layout.SetColumnSpan(_httpAuthPanel, 4);
            layout.Controls.Add(_httpAuthPanel, 0, row++);

            // ── AuthType 变化时联动切换认证区域
            if (_protocolControls.TryGetValue("AuthType", out var authCtrl) && authCtrl is UIComboBox cboAuth)
            {
                cboAuth.SelectedIndexChanged += (s, e) =>
                {
                    var selected = cboAuth.SelectedItem?.ToString() ?? "None";
                    basicLayout.Visible = selected == "Basic";
                    bearerLayout.Visible = selected == "Bearer";
                };
            }
        }

        private void CreateUdpConfigControls(TableLayoutPanel layout)
        {
            int row = 0;

            AddConfigRow(layout, row, "远程IP:", "RemoteIpAddress", "192.168.1.100", 0, ValidationType.IpAddress);
            AddConfigRow(layout, row++, "远程端口:", "RemotePort", "5000", 2, ValidationType.Port);

            AddConfigRow(layout, row, "本地端口:", "LocalPort", "0", 0);
            AddConfigRow(layout, row++, "读取超时:", "ReadTimeout", "3000", 2, ValidationType.Timeout);

            // ConnectionTimeout
            AddConfigRow(layout, row, "连接超时:", "ConnectionTimeout", "5000", 0, ValidationType.Timeout);
        }

        /// <summary>
        /// 加载数据后手动同步认证区域可见性
        /// </summary>
        /// <param name="authType"></param>
        private void TriggerHttpAuthTypeChange(string authType)
        {
            if (_httpAuthPanel == null) return;

            var basicLayout = _httpAuthPanel.Controls.Find("basicLayout", false).FirstOrDefault();
            var bearerLayout = _httpAuthPanel.Controls.Find("bearerLayout", false).FirstOrDefault();

            if (basicLayout != null) basicLayout.Visible = authType == "Basic";
            if (bearerLayout != null) bearerLayout.Visible = authType == "Bearer";
        }

        private void AddConfigCheckBox(TableLayoutPanel layout, int row, string label,
                                string name, bool defaultValue, int col = 0)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("微软雅黑", 11F)
            };

            var chk = new UICheckBox
            {
                Name = name,
                CheckBoxSize = 20,
                Checked = defaultValue,
                Text = "",
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom,
                Margin = new Padding(3, 0, 0, 0),   // 左边距与文本框对齐
                AutoSize = false,
                Width = 24,
                Height = 24
            };

            layout.Controls.Add(lbl, col, row);
            layout.Controls.Add(chk, col + 1, row);

            _protocolControls[name] = chk;
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

        /// <summary>
        /// 添加下拉框配置行,支持下拉框的重载方法
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
                DropDownStyle = allowCustomInput ? UIDropDownStyle.DropDown : UIDropDownStyle.DropDownList
            };

            switch (dataSource)
            {
                case Array enumArray:
                    {
                        // 枚举数组：包装成 DisplayItem 列表，显示 Description，值保留枚举
                        var items = enumArray.Cast<object>()
                            .Select(e => new EnumDisplayItem(e))
                            .ToList();
                        cbo.DataSource = items;
                        cbo.DisplayMember = nameof(EnumDisplayItem.DisplayName);
                        cbo.ValueMember = nameof(EnumDisplayItem.Value);

                        if (defaultValue != null)
                        {
                            var match = items.FirstOrDefault(i => i.Value.Equals(defaultValue));
                            if (match != null)
                                cbo.SelectedItem = match;
                        }
                        break;
                    }

                case IEnumerable<string> stringList:
                    {
                        cbo.DataSource = new List<string>(stringList);
                        if (defaultValue != null)
                        {
                            var index = ((List<string>)cbo.DataSource).IndexOf(defaultValue.ToString());
                            if (index >= 0)
                                cbo.SelectedIndex = index;
                            else if (allowCustomInput)
                                cbo.Text = defaultValue.ToString();
                        }
                        break;
                    }

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
                                cbo.Text = value.ToString();
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
                        // 数据源是 EnumDisplayItem 时，按 Value 匹配
                        if (cbo.DataSource is List<EnumDisplayItem> items)
                        {
                            var match = items.FirstOrDefault(i => i.Value.Equals(enumValue));
                            if (match != null)
                                cbo.SelectedItem = match;
                        }
                        else
                        {
                            // 原来的逻辑，数据源是原始枚举数组时
                            cbo.SelectedItem = enumValue;
                        }
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

        /// <summary>
        /// 从协议配置控件字典中读取控件的值并转换为目标类型
        /// 支持：UITextBox、UIComboBox、UICheckBox
        /// </summary>
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

                        // 数据源是 EnumDisplayItem（枚举中文显示）时，从 Value 取枚举
                        if (cbo.SelectedItem is EnumDisplayItem displayItem)
                        {
                            if (typeof(T).IsEnum)
                                return (T)displayItem.Value;
                            // 非枚举目标类型，走文本转换
                            return ConvertValue<T>(displayItem.DisplayName, defaultValue);
                        }

                        // 目标类型是枚举且选中项也是枚举
                        if (typeof(T).IsEnum && cbo.SelectedItem is Enum)
                            return (T)cbo.SelectedItem;

                        // 目标类型是枚举但选中项是字符串（兼容旧字符串数据源）
                        if (typeof(T).IsEnum && cbo.SelectedItem is string enumStr)
                        {
                            if (Enum.TryParse(typeof(T), enumStr, true, out var parsed))
                                return (T)parsed;
                            return defaultValue;
                        }

                        // 目标类型是 int 且选中项是 int
                        if (typeof(T) == typeof(int) && cbo.SelectedItem is int intItem)
                            return (T)(object)intItem;

                        // 其他情况走文本转换
                        return ConvertValue<T>(cbo.SelectedItem?.ToString(), defaultValue);

                    // UICheckBox 支持
                    case UICheckBox chk:
                        if (typeof(T) == typeof(bool))
                            return (T)(object)chk.Checked;
                        return defaultValue;

                    default:
                        return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
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

        // 参数由外部传入，内部不再自己判断 isNew
        private InstrumentDriver CollectDriverFromForm(bool isNew, string existingId, DateTime existingCreatedTime)
        {
            var driver = new InstrumentDriver(); // 始终新建对象，不复用引用

            if (!isNew)
            {
                driver.DriverId = existingId;           // 从外部传入，不从 _selectedDriver 读
                driver.CreatedTime = existingCreatedTime;
            }

            driver.Name = txtName.Text.Trim();
            driver.DisplayName = txtDisplayName.Text.Trim();
            driver.Category = (InstrumentCategory)(cboCategory.SelectedValue ?? InstrumentCategory.Other);
            driver.ProtocolType = (ProtocolType)(cboProtocolType.SelectedValue ?? ProtocolType.TcpIp);
            driver.Manufacturer = txtManufacturer.Text.Trim();
            driver.Model = txtModel.Text.Trim();
            driver.Enabled = chkEnabled.Checked;
            driver.Description = txtDescription.Text.Trim();

            driver.SetProtocolConfig(CollectProtocolConfig());

            driver.FrameConfig = new FrameConfig
            {
                Enabled = chkFrameEnabled.Checked,
                FrameHeader = txtFrameHeader.Text,
                FrameFooter = txtFrameFooter.Text,
                ResponseTerminator = txtResponseTerminator.Text,
                ChecksumType = (ChecksumType)(cboChecksumType.SelectedValue ?? ChecksumType.None)
            };

            driver.Commands.Clear();
            foreach (DataGridViewRow row in dgvCommands.Rows)
            {
                if (row.Tag is InstrumentCommand cmd)
                    driver.Commands.Add(cmd);
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
                        ConnectionTimeout = GetControlValue<int>("ConnectionTimeout", 5000),
                        ReadTimeout = GetControlValue<int>("ReadTimeout", 3000)
                    };

                case ProtocolType.ModbusTcp:
                    {
                        var cfg = new ModbusProtocolConfig();
                        cfg.SetModbusType(true);
                        cfg.SlaveAddress = GetControlValue<byte>("SlaveAddress", 1);
                        cfg.IpAddress = GetControlValue<string>("IpAddress");
                        cfg.Port = GetControlValue<int>("Port", 502);
                        cfg.ByteOrder = GetControlValue<ByteOrder>("ByteOrder", ByteOrder.BigEndian);
                        cfg.ConnectionTimeout = GetControlValue<int>("ConnectionTimeout", 5000);
                        cfg.ReadTimeout = GetControlValue<int>("ReadTimeout", 3000);
                        cfg.SwapBytes = GetControlValue<bool>("SwapBytes", false);
                        cfg.SwapWords = GetControlValue<bool>("SwapWords", false);
                        return cfg;
                    }

                case ProtocolType.ModbusRtu:
                    {
                        var cfg = new ModbusProtocolConfig();
                        cfg.SetModbusType(false);
                        cfg.SlaveAddress = GetControlValue<byte>("SlaveAddress", 1);
                        cfg.PortName = GetControlValue<string>("PortName");
                        cfg.BaudRate = GetControlValue<int>("BaudRate", 9600);
                        cfg.DataBits = GetControlValue<int>("DataBits", 8);
                        cfg.StopBits = GetControlValue<StopBitsType>("StopBits", StopBitsType.One);
                        cfg.Parity = GetControlValue<ParityType>("Parity", ParityType.None);
                        cfg.ByteOrder = GetControlValue<ByteOrder>("ByteOrder", ByteOrder.BigEndian);
                        cfg.ReadTimeout = GetControlValue<int>("ReadTimeout", 3000);
                        cfg.SwapBytes = GetControlValue<bool>("SwapBytes", false);
                        cfg.SwapWords = GetControlValue<bool>("SwapWords", false);
                        return cfg;
                    }

                case ProtocolType.Http:
                    return new HttpProtocolConfig
                    {
                        BaseUrl = GetControlValue<string>("BaseUrl"),
                        AuthType = GetControlValue<string>("AuthType", "None"),
                        Username = GetControlValue<string>("Username"),
                        Password = GetControlValue<string>("Password"),
                        BearerToken = GetControlValue<string>("BearerToken"),
                        ContentType = GetControlValue<string>("ContentType", "application/json")
                    };

                case ProtocolType.Udp:
                    return new UdpProtocolConfig
                    {
                        RemoteIpAddress = GetControlValue<string>("RemoteIpAddress"),
                        RemotePort = GetControlValue<int>("RemotePort", 5000),
                        LocalPort = GetControlValue<int>("LocalPort", 0),
                        ConnectionTimeout = GetControlValue<int>("ConnectionTimeout", 5000),
                        ReadTimeout = GetControlValue<int>("ReadTimeout", 3000)
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
            // 先解绑，防止 ClearForm 过程中 SelectionChanged 覆盖新建状态
            dgvDrivers.SelectionChanged -= DgvDrivers_SelectionChanged;

            // 取消列表选中
            dgvDrivers.ClearSelection();

            // 清空表单，设置新建标记
            ClearForm();
            _selectedDriver = new InstrumentDriver(); // DriverId 为空，标记为新建

            // 重新绑定事件
            dgvDrivers.SelectionChanged += DgvDrivers_SelectionChanged;

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

            // await 前保存，后续不再依赖 _selectedDriver
            var driverToDelete = _selectedDriver;

            if (MessageHelper.MessageYes(this, $"确定要删除驱动 [{driverToDelete.DisplayName}] 吗？") != DialogResult.OK)
                return;

            try
            {
                var result = await _driverService.DeleteDriverAsync(driverToDelete.DriverId);

                if (result)
                {
                    _drivers.Remove(driverToDelete);
                    RefreshDriverList();
                    ClearForm();
                    MessageHelper.MessageOK(this, "删除成功");
                    _logger?.LogInformation("删除驱动成功: {DisplayName} (ID: {DriverId})",
                        driverToDelete.DisplayName, driverToDelete.DriverId);  // 用本地变量
                }
                else
                {
                    MessageHelper.MessageOK(this, "删除失败：驱动不存在或已被删除");
                    _logger?.LogWarning("删除驱动失败: {DisplayName} (ID: {DriverId})",
                        driverToDelete.DisplayName, driverToDelete.DriverId);
                    await LoadDriversAsync();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除驱动时发生异常: {DisplayName}", driverToDelete.DisplayName);
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
                    //driver.DriverId = Guid.NewGuid().ToString("N");

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
                // 唯一的判断点，await 之前确定，全程不变
                bool isNew = _selectedDriver == null || string.IsNullOrEmpty(_selectedDriver.DriverId);
                string existingId = isNew ? null : _selectedDriver.DriverId;
                DateTime existingCreatedTime = isNew ? DateTime.Now : _selectedDriver.CreatedTime;

                // CollectDriverFromForm 不再自己判断，直接从外部接收
                var driver = CollectDriverFromForm(isNew, existingId, existingCreatedTime);

                bool result;
                if (isNew)
                {
                    result = await _driverService.AddDriverAsync(driver);
                }
                else
                {
                    result = await _driverService.UpdateDriverAsync(driver);
                }

                if (result)
                {
                    MessageHelper.MessageOK(this, "保存成功");
                    var savedId = driver.DriverId;
                    var savedName = driver.Name;

                    await LoadDriversAsync();

                    dgvDrivers.SelectionChanged -= DgvDrivers_SelectionChanged;
                    try
                    {
                        foreach (DataGridViewRow row in dgvDrivers.Rows)
                        {
                            if (row.Tag is not InstrumentDriver d) continue;
                            if (d.DriverId != savedId && d.Name != savedName) continue;
                            row.Selected = true;
                            dgvDrivers.FirstDisplayedScrollingRowIndex = row.Index;
                            _selectedDriver = d;
                            break;
                        }
                    }
                    finally
                    {
                        dgvDrivers.SelectionChanged += DgvDrivers_SelectionChanged;
                    }
                }
                else
                {
                    MessageHelper.MessageOK(this, isNew
                        ? "保存失败（驱动名称已存在）"
                        : "保存失败（未找到要更新的驱动）");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存驱动失败");
                MessageHelper.MessageOK(this, $"保存失败: {ex.Message}");
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

            // 直接查重，排除自身即可
            var duplicate = _drivers?.FirstOrDefault(d =>
                d.Name.Equals(txtName.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
                d.DriverId != _selectedDriver?.DriverId);

            if (duplicate != null)
            {
                MessageHelper.MessageOK(this, "驱动名称已存在，请使用其他名称");
                txtName.Focus();
                return false;
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
                    if (string.IsNullOrWhiteSpace(GetControlValue<string>("IpAddress")))
                    {
                        MessageHelper.MessageOK(this, "请输入 Modbus TCP 的 IP 地址");
                        return false;
                    }
                    break;

                case ProtocolType.ModbusRtu:
                    if (string.IsNullOrWhiteSpace(GetControlValue<string>("PortName")))
                    {
                        MessageHelper.MessageOK(this, "请选择 Modbus RTU 的串口");
                        return false;
                    }
                    break;

                case ProtocolType.Http:
                    if (string.IsNullOrWhiteSpace(GetControlValue<string>("BaseUrl")))
                    {
                        MessageHelper.MessageOK(this, "请输入基础 URL");
                        return false;
                    }
                    // Bearer 认证时 Token 不能为空
                    if (GetControlValue<string>("AuthType") == "Bearer" &&
                        string.IsNullOrWhiteSpace(GetControlValue<string>("BearerToken")))
                    {
                        MessageHelper.MessageOK(this, "Bearer 认证需要填写 Token");
                        return false;
                    }
                    break;

                case ProtocolType.Udp:
                    if (string.IsNullOrWhiteSpace(GetControlValue<string>("RemoteIpAddress")))
                    {
                        MessageHelper.MessageOK(this, "请输入远程 IP 地址");
                        return false;
                    }
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

    /// <summary>
    /// 枚举下拉框显示项，将枚举的 Description 作为显示文本
    /// </summary>
    internal class EnumDisplayItem
    {
        public object Value { get; }
        public string DisplayName { get; }

        public EnumDisplayItem(object enumValue)
        {
            Value = enumValue;
            // 读取 [Description] 特性，没有则 fallback 到枚举名
            var fi = enumValue.GetType().GetField(enumValue.ToString());
            var attr = fi?.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                           .FirstOrDefault() as System.ComponentModel.DescriptionAttribute;
            DisplayName = attr?.Description ?? enumValue.ToString();
        }
    }
}
using MainUI.LogicalConfiguration.Instrument.Communication;
using MainUI.LogicalConfiguration.Instrument.Models;
using Sunny.UI;
using System.Text;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    public partial class FrmCommunicationTest : UIForm
    {
        private ICommunicationProvider _provider;
        private CancellationTokenSource _cts;
        private bool _isConnected;

        public FrmCommunicationTest()
        {
            InitializeComponent();
            InitializeUI();
        }


        private void InitializeUI()
        {
            // 协议类型下拉
            cboProtocolType.DataSource = new List<ProtocolTypeOption>
            {
                new ProtocolTypeOption { Text = "TCP/IP", Value = ProtocolType.TcpIp },
                new ProtocolTypeOption { Text = "串口", Value = ProtocolType.Serial }
            };
            cboProtocolType.DisplayMember = "Text";
            cboProtocolType.ValueMember = "Value";
            cboProtocolType.SelectedIndex = 0;

            // 数据格式
            cboDataFormat.Items.AddRange(new[] { "ASCII", "HEX" });
            cboDataFormat.SelectedIndex = 0;

            // 初始化配置面板
            UpdateConfigPanel();

            // 初始化日志
            rtbLog.ReadOnly = true;
            rtbLog.Font = new Font("Consolas", 9);

            _cts = new CancellationTokenSource();
        }

        private void cboProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateConfigPanel();
        }

        // 获取当前选择的协议类型
        private ProtocolType GetSelectedProtocolType()
        {
            if (cboProtocolType.SelectedValue is ProtocolType pt)
                return pt;

            // 默认返回TcpIp
            return ProtocolType.TcpIp;
        }

        private void UpdateConfigPanel()
        {
            pnlConfig.Controls.Clear();

            // 根据选择的文本判断协议类型
            var protocolType = cboProtocolType.SelectedIndex == 0
                ? ProtocolType.TcpIp
                : ProtocolType.Serial;

            if (protocolType == ProtocolType.TcpIp)
            {
                CreateTcpConfigPanel();
            }
            else if (protocolType == ProtocolType.Serial)
            {
                CreateSerialConfigPanel();
            }
        }


        private void CreateTcpConfigPanel()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 3,
                Padding = new Padding(10)
            };

            // 设置列宽
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            int row = 0;

            // IP地址
            AddConfigControl(layout, row, 0, "IP地址:", "txtIpAddress", "127.0.0.1");
            // 端口
            AddConfigControl(layout, row, 2, "端口:", "txtPort", "5025");
            row++;

            // 连接超时
            AddConfigControl(layout, row, 0, "连接超时(ms):", "txtConnectionTimeout", "5000");
            // 读取超时
            AddConfigControl(layout, row, 2, "读取超时(ms):", "txtReadTimeout", "3000");
            row++;

            // 保持连接
            var lblKeepAlive = new UILabel { Text = "保持连接:", TextAlign = ContentAlignment.MiddleRight };
            var chkKeepAlive = new UICheckBox { Name = "chkKeepAlive", Checked = true };
            layout.Controls.Add(lblKeepAlive, 0, row);
            layout.Controls.Add(chkKeepAlive, 1, row);

            pnlConfig.Controls.Add(layout);
        }

        private void CreateSerialConfigPanel()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 4,
                Padding = new Padding(10)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            int row = 0;

            // 串口号
            var lblPort = new UILabel { Text = "串口:", TextAlign = ContentAlignment.MiddleRight };
            var cboPortName = new UIComboBox { Name = "cboPortName", DropDownStyle = (UIDropDownStyle)ComboBoxStyle.DropDownList };
            cboPortName.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (cboPortName.Items.Count > 0) cboPortName.SelectedIndex = 0;
            layout.Controls.Add(lblPort, 0, row);
            layout.Controls.Add(cboPortName, 1, row);

            // 波特率
            var lblBaudRate = new UILabel { Text = "波特率:", TextAlign = ContentAlignment.MiddleRight };
            var cboBaudRate = new UIComboBox { Name = "cboBaudRate", DropDownStyle = (UIDropDownStyle)ComboBoxStyle.DropDownList };
            cboBaudRate.Items.AddRange(new object[] { 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 });
            cboBaudRate.SelectedItem = 9600;
            layout.Controls.Add(lblBaudRate, 2, row);
            layout.Controls.Add(cboBaudRate, 3, row);
            row++;

            // 数据位
            var lblDataBits = new UILabel { Text = "数据位:", TextAlign = ContentAlignment.MiddleRight };
            var cboDataBits = new UIComboBox { Name = "cboDataBits", DropDownStyle = (UIDropDownStyle)ComboBoxStyle.DropDownList };
            cboDataBits.Items.AddRange(new object[] { 5, 6, 7, 8 });
            cboDataBits.SelectedItem = 8;
            layout.Controls.Add(lblDataBits, 0, row);
            layout.Controls.Add(cboDataBits, 1, row);

            // 停止位
            var lblStopBits = new UILabel { Text = "停止位:", TextAlign = ContentAlignment.MiddleRight };
            var cboStopBits = new UIComboBox { Name = "cboStopBits", DropDownStyle = (UIDropDownStyle)ComboBoxStyle.DropDownList };
            cboStopBits.DataSource = Enum.GetValues(typeof(StopBitsType));
            cboStopBits.SelectedItem = StopBitsType.One;
            layout.Controls.Add(lblStopBits, 2, row);
            layout.Controls.Add(cboStopBits, 3, row);
            row++;

            // 校验位
            var lblParity = new UILabel
            {
                Text = "校验位:",
                TextAlign = ContentAlignment.MiddleRight,
                //AutoSize = true
            };
            var cboParity = new UIComboBox { Name = "cboParity", DropDownStyle = (UIDropDownStyle)ComboBoxStyle.DropDownList };
            cboParity.DataSource = Enum.GetValues(typeof(ParityType));
            cboParity.SelectedItem = ParityType.None;
            layout.Controls.Add(lblParity, 0, row);
            layout.Controls.Add(cboParity, 1, row);

            // 读取超时
            AddConfigControl(layout, row, 2, "读取超时(ms):", "txtReadTimeout", "3000");

            pnlConfig.Controls.Add(layout);
        }

        private void AddConfigControl(TableLayoutPanel layout, int row, int col, string label, string name, string defaultValue)
        {
            var lbl = new UILabel { Text = label, TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
            var txt = new UITextBox { Name = name, Text = defaultValue };
            layout.Controls.Add(lbl, col, row);
            layout.Controls.Add(txt, col + 1, row);
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isConnected)
                {
                    // 断开连接
                    await DisconnectAsync();
                }
                else
                {
                    // 连接
                    await ConnectAsync();
                }
            }
            catch (Exception ex)
            {
                AppendLog("错误", $"连接操作失败: {ex.Message}", Color.Red);
            }
        }

        private async Task ConnectAsync()
        {
            // 根据选择的文本判断协议类型
            var protocolType = cboProtocolType.SelectedIndex == 0
                ? ProtocolType.TcpIp
                : ProtocolType.Serial;

            ProtocolConfigBase config;

            if (protocolType == ProtocolType.TcpIp)
            {
                config = new TcpProtocolConfig
                {
                    IpAddress = GetControlValue<string>("txtIpAddress"),
                    Port = GetControlValue<int>("txtPort"),
                    ConnectionTimeout = GetControlValue<int>("txtConnectionTimeout"),
                    ReadTimeout = GetControlValue<int>("txtReadTimeout"),
                    KeepAlive = GetControlValue<bool>("chkKeepAlive")
                };
                _provider = new TcpCommunicationProvider();
            }
            else // Serial
            {
                config = new SerialProtocolConfig
                {
                    PortName = GetControlValue<string>("cboPortName"),
                    BaudRate = GetControlValue<int>("cboBaudRate"),
                    DataBits = GetControlValue<int>("cboDataBits"),
                    StopBits = GetControlValue<StopBitsType>("cboStopBits"),
                    Parity = GetControlValue<ParityType>("cboParity"),
                    ReadTimeout = GetControlValue<int>("txtReadTimeout")
                };
                _provider = new SerialCommunicationProvider();
            }

            btnConnect.Enabled = false;
            AppendLog("系统", $"正在连接 {protocolType}...", Color.Gray);

            var success = await _provider.ConnectAsync(config, _cts.Token);

            if (success)
            {
                _isConnected = true;
                btnConnect.Text = "断开";
                btnSend.Enabled = true;
                btnReceive.Enabled = true;
                btnClearLog.Enabled = true;
                pnlConfig.Enabled = false;
                cboProtocolType.Enabled = false;

                AppendLog("系统", "连接成功", Color.Green);
            }
            else
            {
                AppendLog("错误", "连接失败", Color.Red);
            }

            btnConnect.Enabled = true;
        }

        private async Task DisconnectAsync()
        {
            if (_provider != null)
            {
                await _provider.DisconnectAsync();
                _provider.Dispose();
                _provider = null;
            }

            _isConnected = false;
            btnConnect.Text = "连接";
            btnSend.Enabled = false;
            btnReceive.Enabled = false;
            pnlConfig.Enabled = true;
            cboProtocolType.Enabled = true;

            AppendLog("系统", "已断开连接", Color.Gray);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (!_isConnected || _provider == null)
            {
                UIMessageBox.ShowWarning("请先连接设备");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSendData.Text))
            {
                UIMessageBox.ShowWarning("请输入要发送的数据");
                return;
            }

            try
            {
                byte[] data = ParseSendData();

                AppendLog("发送", FormatData(data), Color.Blue);

                bool waitResponse = chkWaitResponse.Checked;
                int timeout = (int)nudTimeout.Value;

                var sw = System.Diagnostics.Stopwatch.StartNew();

                if (waitResponse)
                {
                    var result = await _provider.SendAndReceiveAsync(
                        data,
                        null,  // 简单测试不使用FrameConfig
                        timeout,
                        true,
                        _cts.Token);

                    sw.Stop();

                    if (result.Success)
                    {
                        AppendLog("接收", FormatData(result.RawResponse), Color.Green);
                        AppendLog("统计", $"耗时: {sw.ElapsedMilliseconds}ms, 接收字节: {result.RawResponse?.Length ?? 0}", Color.Gray);
                    }
                    else
                    {
                        AppendLog("错误", result.ErrorMessage ?? "接收失败", Color.Red);
                    }
                }
                else
                {
                    var success = await _provider.SendAsync(data, _cts.Token);
                    sw.Stop();

                    if (success)
                    {
                        AppendLog("统计", $"发送成功, 耗时: {sw.ElapsedMilliseconds}ms", Color.Gray);
                    }
                    else
                    {
                        AppendLog("错误", "发送失败", Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                AppendLog("异常", ex.Message, Color.Red);
            }
        }

        private async void btnReceive_Click(object sender, EventArgs e)
        {
            if (!_isConnected || _provider == null)
            {
                UIMessageBox.ShowWarning("请先连接设备");
                return;
            }

            try
            {
                AppendLog("系统", "等待接收数据...", Color.Gray);

                int timeout = (int)nudTimeout.Value;
                var data = await _provider.ReceiveAsync(null, timeout, _cts.Token);

                if (data != null && data.Length > 0)
                {
                    AppendLog("接收", FormatData(data), Color.Green);
                    AppendLog("统计", $"接收字节: {data.Length}", Color.Gray);
                }
                else
                {
                    AppendLog("提示", "未接收到数据", Color.Orange);
                }
            }
            catch (Exception ex)
            {
                AppendLog("异常", ex.Message, Color.Red);
            }
        }

        private byte[] ParseSendData()
        {
            string text = txtSendData.Text.Trim();
            bool isHex = cboDataFormat.SelectedItem.ToString() == "HEX";

            if (isHex)
            {
                // HEX格式: "01 03 00 00 00 0A" 或 "010300000A"
                text = text.Replace(" ", "").Replace("-", "");

                if (text.Length % 2 != 0)
                    throw new ArgumentException("HEX数据长度必须为偶数");

                return Enumerable.Range(0, text.Length / 2)
                    .Select(x => Convert.ToByte(text.Substring(x * 2, 2), 16))
                    .ToArray();
            }
            else
            {
                // ASCII格式
                return Encoding.ASCII.GetBytes(text);
            }
        }

        private string FormatData(byte[] data)
        {
            if (data == null || data.Length == 0)
                return "[空]";

            bool isHex = cboDataFormat.SelectedItem.ToString() == "HEX";

            if (isHex)
            {
                return BitConverter.ToString(data).Replace("-", " ");
            }
            else
            {
                // ASCII 显示,不可见字符用HEX表示
                var sb = new StringBuilder();
                foreach (byte b in data)
                {
                    if (b >= 32 && b <= 126)
                        sb.Append((char)b);
                    else
                        sb.Append($"<{b:X2}>");
                }
                return sb.ToString();
            }
        }

        private void AppendLog(string type, string message, Color color)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(() => AppendLog(type, message, color));
                return;
            }

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;

            // 时间戳
            rtbLog.SelectionColor = Color.Gray;
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] ");

            // 类型标签
            rtbLog.SelectionColor = GetTypeColor(type);
            rtbLog.SelectionFont = new Font(rtbLog.Font, FontStyle.Bold);
            rtbLog.AppendText($"[{type}] ");
            rtbLog.SelectionFont = rtbLog.Font;

            // 消息内容
            rtbLog.SelectionColor = color;
            rtbLog.AppendText(message + "\n");

            rtbLog.SelectionColor = rtbLog.ForeColor;
            rtbLog.ScrollToCaret();
        }

        private Color GetTypeColor(string type)
        {
            return type switch
            {
                "发送" => Color.DodgerBlue,
                "接收" => Color.ForestGreen,
                "错误" or "异常" => Color.Red,
                "系统" or "统计" => Color.Gray,
                _ => Color.Black
            };
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
        }

        private T GetControlValue<T>(string controlName)
        {
            var control = pnlConfig.Controls.Find(controlName, true).FirstOrDefault();
            if (control == null)
                return default(T);

            if (control is UITextBox txt)
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)txt.Text;
                if (typeof(T) == typeof(int))
                    return (T)(object)int.Parse(txt.Text);
            }
            else if (control is UIComboBox cbo)
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)cbo.Text;
                if (typeof(T) == typeof(int))
                    return (T)(object)int.Parse(cbo.Text);
                if (typeof(T).IsEnum)
                    return (T)cbo.SelectedItem;
            }
            else if (control is UICheckBox chk)
            {
                return (T)(object)chk.Checked;
            }

            return default(T);
        }

        private void FrmCommunicationTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts?.Cancel();
            _provider?.Dispose();
        }
    }


    /// <summary>
    /// 协议类型选项
    /// </summary>
    public class ProtocolTypeOption
    {
        public string Text { get; set; }
        public ProtocolType Value { get; set; }
    }
}
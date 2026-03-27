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

        // 切换协议时先断开当前连接
        private async void cboProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 如果当前已连接，先断开再切换UI
            if (_isConnected)
            {
                AppendLog("系统", "切换协议类型，正在断开当前连接...", Color.Gray);
                await DisconnectAsync();
            }

            UpdateConfigPanel();
        }

        // 获取当前选择的协议类型
        private ProtocolType GetSelectedProtocolType()
        {
            if (cboProtocolType.SelectedValue is ProtocolType pt)
                return pt;
            return ProtocolType.TcpIp;
        }

        private void UpdateConfigPanel()
        {
            pnlConfig.Controls.Clear();

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

            // 串口名
            var lblPort = new UILabel { Text = "串口:", TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
            var cboPort = new UIComboBox { Name = "cboPortName", DropDownStyle = (UIDropDownStyle)ComboBoxStyle.DropDownList };
            cboPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (cboPort.Items.Count > 0) cboPort.SelectedIndex = 0;
            layout.Controls.Add(lblPort, 0, row);
            layout.Controls.Add(cboPort, 1, row);

            // 波特率
            var lblBaud = new UILabel { Text = "波特率:", TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
            var cboBaud = new UIComboBox { Name = "cboBaudRate", DropDownStyle = (UIDropDownStyle)ComboBoxStyle.DropDownList };
            cboBaud.Items.AddRange(new object[] { 9600, 19200, 38400, 57600, 115200 });
            cboBaud.SelectedItem = 9600;
            layout.Controls.Add(lblBaud, 2, row);
            layout.Controls.Add(cboBaud, 3, row);

            row++;

            // 数据位
            var lblDataBits = new UILabel { Text = "数据位:", TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
            var cboDataBits = new UIComboBox { Name = "cboDataBits", DropDownStyle = (UIDropDownStyle)ComboBoxStyle.DropDownList };
            cboDataBits.Items.AddRange(new object[] { 7, 8 });
            cboDataBits.SelectedItem = 8;
            layout.Controls.Add(lblDataBits, 0, row);
            layout.Controls.Add(cboDataBits, 1, row);

            // 停止位
            var lblStopBits = new UILabel { Text = "停止位:", TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
            var cboStopBits = new UIComboBox { Name = "cboStopBits", DropDownStyle = (UIDropDownStyle)ComboBoxStyle.DropDownList };
            cboStopBits.DataSource = Enum.GetValues(typeof(StopBitsType));
            cboStopBits.SelectedItem = StopBitsType.One;
            layout.Controls.Add(lblStopBits, 2, row);
            layout.Controls.Add(cboStopBits, 3, row);

            row++;

            // 校验位
            var lblParity = new UILabel { Text = "校验位:", TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
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
                    await DisconnectAsync();
                }
                else
                {
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
            else
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
                // 连接失败时也要清理 provider
                _provider?.Dispose();
                _provider = null;
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
                        null,
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
                        AppendLog("错误", result.ErrorMessage ?? "未知错误", Color.Red);
                    }
                }
                else
                {
                    var success = await _provider.SendAsync(data, _cts.Token);
                    sw.Stop();
                    AppendLog(success ? "系统" : "错误",
                        success ? $"发送成功, 耗时: {sw.ElapsedMilliseconds}ms" : "发送失败",
                        success ? Color.Gray : Color.Red);
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
                int timeout = (int)nudTimeout.Value;
                var data = await _provider.ReceiveAsync(null, timeout, _cts.Token);

                if (data != null && data.Length > 0)
                {
                    AppendLog("接收", FormatData(data), Color.Green);
                }
                else
                {
                    AppendLog("系统", "未接收到数据", Color.Gray);
                }
            }
            catch (Exception ex)
            {
                AppendLog("异常", ex.Message, Color.Red);
            }
        }

        private byte[] ParseSendData()
        {
            string text = txtSendData.Text;

            if (cboDataFormat.SelectedIndex == 1) // HEX
            {
                text = text.Replace(" ", "").Replace("-", "");
                var bytes = new byte[text.Length / 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
                }
                return bytes;
            }
            else // ASCII
            {
                return Encoding.ASCII.GetBytes(text);
            }
        }

        private string FormatData(byte[] data)
        {
            if (data == null || data.Length == 0)
                return "(空)";

            if (cboDataFormat.SelectedIndex == 1) // HEX
            {
                return BitConverter.ToString(data).Replace("-", " ");
            }
            else // ASCII
            {
                return Encoding.ASCII.GetString(data);
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
            rtbLog.SelectionColor = Color.Gray;
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] ");

            rtbLog.SelectionColor = GetTypeColor(type);
            rtbLog.AppendText($"[{type}] ");

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

        // FormClosing 正确 await 断开连接
        // 原代码直接 _provider?.Dispose()，没有先断开，可能导致:
        // 1. TCP 没有优雅断开（发送 FIN），远端感知为异常
        // 2. 如果有正在进行的 async 操作，Dispose 和 async 操作竞态
        private async void FrmCommunicationTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 先取消所有正在进行的操作
            _cts?.Cancel();

            // 等待断开连接
            if (_provider != null)
            {
                try
                {
                    await _provider.DisconnectAsync();
                }
                catch
                {
                    // 关闭时忽略断开异常
                }
                finally
                {
                    _provider.Dispose();
                    _provider = null;
                }
            }
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

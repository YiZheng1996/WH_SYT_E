using MainUI.LogicalConfiguration.Instrument.TestTools;
using Sunny.UI;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    public partial class FrmSerialLoopbackTest : UIForm
    {
        private SerialLoopbackTester _tester;

        public FrmSerialLoopbackTest()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            _tester = new SerialLoopbackTester();
            _tester.OnLog += msg => AppendLog(msg);

            // 加载可用串口
            var ports = System.IO.Ports.SerialPort.GetPortNames();
            cboPort1.Items.AddRange(ports);
            cboPort2.Items.AddRange(ports);

            if (cboPort1.Items.Count > 0) cboPort1.SelectedIndex = 0;
            if (cboPort2.Items.Count > 1) cboPort2.SelectedIndex = 1;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cboPort1.SelectedItem == null || cboPort2.SelectedItem == null)
            {
                UIMessageBox.ShowWarning("请选择两个不同的串口!");
                return;
            }

            if (cboPort1.Text == cboPort2.Text)
            {
                UIMessageBox.ShowWarning("两个串口不能相同!");
                return;
            }

            try
            {
                _tester.Start(
                    cboPort1.Text,
                    cboPort2.Text,
                    (int)nudBaudRate.Value);

                btnStart.Enabled = false;
                btnStop.Enabled = true;
                gbSettings.Enabled = false;
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"启动失败: {ex.Message}");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _tester.Stop();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            gbSettings.Enabled = true;
        }

        private void AppendLog(string message)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(() => AppendLog(message));
                return;
            }

            rtbLog.AppendText(message + "\n");
            rtbLog.ScrollToCaret();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
        }

        private void FrmSerialLoopbackTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tester?.Dispose();
        }
    }
}
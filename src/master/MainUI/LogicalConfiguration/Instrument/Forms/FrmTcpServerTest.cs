using MainUI.LogicalConfiguration.Instrument.TestTools;
using System.Text;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    public partial class FrmTcpServerTest : UIForm
    {
        private SimpleTcpServer _server;

        public FrmTcpServerTest()
        {
            InitializeComponent();
            _server = new SimpleTcpServer();
            _server.OnLog += msg => AppendLog(msg);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int port = (int)nudPort.Value;

            if (rbEcho.Checked)
            {
                _server.EchoMode = true;
            }
            else if (rbCustom.Checked)
            {
                _server.EchoMode = false;
                _server.CustomResponse = Encoding.ASCII.GetBytes(txtCustomResponse.Text);
            }

            _server.Start(port);

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            gbSettings.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _server.Stop();

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

        private void FrmTcpServerTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            _server?.Dispose();
        }
    }
}

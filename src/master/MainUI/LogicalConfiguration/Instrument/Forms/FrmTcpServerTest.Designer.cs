namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    partial class FrmTcpServerTest
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            uiPanel1 = new UIPanel();
            btnStop = new UIButton();
            btnStart = new UIButton();
            gbSettings = new UIGroupBox();
            txtCustomResponse = new UITextBox();
            rbCustom = new UIRadioButton();
            rbEcho = new UIRadioButton();
            uiLabel2 = new UILabel();
            nudPort = new UIIntegerUpDown();
            uiLabel1 = new UILabel();
            uiGroupBox1 = new UIGroupBox();
            btnClearLog = new UIButton();
            rtbLog = new RichTextBox();
            uiPanel1.SuspendLayout();
            gbSettings.SuspendLayout();
            uiGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(btnStop);
            uiPanel1.Controls.Add(btnStart);
            uiPanel1.Controls.Add(gbSettings);
            uiPanel1.Dock = DockStyle.Top;
            uiPanel1.Font = new Font("微软雅黑", 12F);
            uiPanel1.Location = new Point(0, 35);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Padding = new Padding(10);
            uiPanel1.Size = new Size(800, 180);
            uiPanel1.TabIndex = 0;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStop.Enabled = false;
            btnStop.Font = new Font("微软雅黑", 12F);
            btnStop.Location = new Point(670, 140);
            btnStop.MinimumSize = new Size(1, 1);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(100, 35);
            btnStop.TabIndex = 2;
            btnStop.Text = "停止服务器";
            btnStop.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStart.Font = new Font("微软雅黑", 12F);
            btnStart.Location = new Point(560, 140);
            btnStart.MinimumSize = new Size(1, 1);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(100, 35);
            btnStart.TabIndex = 1;
            btnStart.Text = "启动服务器";
            btnStart.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStart.Click += btnStart_Click;
            // 
            // gbSettings
            // 
            gbSettings.Controls.Add(txtCustomResponse);
            gbSettings.Controls.Add(rbCustom);
            gbSettings.Controls.Add(rbEcho);
            gbSettings.Controls.Add(uiLabel2);
            gbSettings.Controls.Add(nudPort);
            gbSettings.Controls.Add(uiLabel1);
            gbSettings.Dock = DockStyle.Top;
            gbSettings.Font = new Font("微软雅黑", 12F);
            gbSettings.Location = new Point(10, 10);
            gbSettings.Margin = new Padding(4, 5, 4, 5);
            gbSettings.MinimumSize = new Size(1, 1);
            gbSettings.Name = "gbSettings";
            gbSettings.Padding = new Padding(10, 32, 10, 10);
            gbSettings.Size = new Size(780, 125);
            gbSettings.TabIndex = 0;
            gbSettings.Text = "服务器设置";
            gbSettings.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // txtCustomResponse
            // 
            txtCustomResponse.Font = new Font("微软雅黑", 12F);
            txtCustomResponse.Location = new Point(360, 75);
            txtCustomResponse.Margin = new Padding(4, 5, 4, 5);
            txtCustomResponse.MinimumSize = new Size(1, 16);
            txtCustomResponse.Name = "txtCustomResponse";
            txtCustomResponse.Padding = new Padding(5);
            txtCustomResponse.ShowText = false;
            txtCustomResponse.Size = new Size(400, 29);
            txtCustomResponse.TabIndex = 5;
            txtCustomResponse.TextAlignment = ContentAlignment.MiddleLeft;
            txtCustomResponse.Watermark = "输入自定义响应内容...";
            // 
            // rbCustom
            // 
            rbCustom.Font = new Font("微软雅黑", 12F);
            rbCustom.Location = new Point(230, 75);
            rbCustom.MinimumSize = new Size(1, 1);
            rbCustom.Name = "rbCustom";
            rbCustom.Size = new Size(120, 29);
            rbCustom.TabIndex = 4;
            rbCustom.Text = "自定义响应";
            // 
            // rbEcho
            // 
            rbEcho.Checked = true;
            rbEcho.Font = new Font("微软雅黑", 12F);
            rbEcho.Location = new Point(100, 75);
            rbEcho.MinimumSize = new Size(1, 1);
            rbEcho.Name = "rbEcho";
            rbEcho.Size = new Size(120, 29);
            rbEcho.TabIndex = 3;
            rbEcho.Text = "回显模式";
            // 
            // uiLabel2
            // 
            uiLabel2.Font = new Font("微软雅黑", 12F);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(15, 75);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(80, 29);
            uiLabel2.TabIndex = 2;
            uiLabel2.Text = "响应模式:";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // nudPort
            // 
            nudPort.Font = new Font("微软雅黑", 12F);
            nudPort.Location = new Point(100, 35);
            nudPort.Margin = new Padding(4, 5, 4, 5);
            nudPort.Maximum = 65535D;
            nudPort.Minimum = 1D;
            nudPort.MinimumSize = new Size(1, 16);
            nudPort.Name = "nudPort";
            nudPort.Padding = new Padding(5);
            nudPort.ShowText = false;
            nudPort.Size = new Size(120, 29);
            nudPort.TabIndex = 1;
            nudPort.Text = "5025";
            nudPort.TextAlignment = ContentAlignment.MiddleCenter;
            nudPort.Value = 5025;
            // 
            // uiLabel1
            // 
            uiLabel1.Font = new Font("微软雅黑", 12F);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(15, 35);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(80, 29);
            uiLabel1.TabIndex = 0;
            uiLabel1.Text = "监听端口:";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(btnClearLog);
            uiGroupBox1.Controls.Add(rtbLog);
            uiGroupBox1.Dock = DockStyle.Fill;
            uiGroupBox1.Font = new Font("微软雅黑", 12F);
            uiGroupBox1.Location = new Point(0, 215);
            uiGroupBox1.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new Padding(10, 32, 10, 10);
            uiGroupBox1.Size = new Size(800, 348);
            uiGroupBox1.TabIndex = 1;
            uiGroupBox1.Text = "服务器日志";
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnClearLog
            // 
            btnClearLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearLog.Font = new Font("微软雅黑", 9F);
            btnClearLog.Location = new Point(710, 24);
            btnClearLog.MinimumSize = new Size(1, 1);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(75, 28);
            btnClearLog.TabIndex = 1;
            btnClearLog.Text = "清空";
            btnClearLog.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // rtbLog
            // 
            rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbLog.BackColor = Color.FromArgb(243, 249, 255);
            rtbLog.BorderStyle = BorderStyle.FixedSingle;
            rtbLog.Font = new Font("Consolas", 9F);
            rtbLog.Location = new Point(15, 56);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(770, 277);
            rtbLog.TabIndex = 0;
            rtbLog.Text = "";
            // 
            // FrmTcpServerTest
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 563);
            Controls.Add(uiGroupBox1);
            Controls.Add(uiPanel1);
            Name = "FrmTcpServerTest";
            ShowIcon = false;
            Text = "TCP测试服务器";
            ZoomScaleRect = new Rectangle(15, 15, 800, 550);
            FormClosing += FrmTcpServerTest_FormClosing;
            uiPanel1.ResumeLayout(false);
            gbSettings.ResumeLayout(false);
            uiGroupBox1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIGroupBox gbSettings;
        private Sunny.UI.UITextBox txtCustomResponse;
        private Sunny.UI.UIRadioButton rbCustom;
        private Sunny.UI.UIRadioButton rbEcho;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIIntegerUpDown nudPort;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIButton btnStop;
        private Sunny.UI.UIButton btnStart;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIButton btnClearLog;
        private System.Windows.Forms.RichTextBox rtbLog;
    }
}
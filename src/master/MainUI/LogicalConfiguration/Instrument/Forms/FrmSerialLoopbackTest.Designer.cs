namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    partial class FrmSerialLoopbackTest
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
            nudBaudRate = new UIIntegerUpDown();
            uiLabel3 = new UILabel();
            cboPort2 = new UIComboBox();
            uiLabel2 = new UILabel();
            cboPort1 = new UIComboBox();
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
            uiPanel1.Size = new Size(700, 150);
            uiPanel1.TabIndex = 0;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStop.Enabled = false;
            btnStop.Font = new Font("微软雅黑", 12F);
            btnStop.Location = new Point(570, 110);
            btnStop.MinimumSize = new Size(1, 1);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(100, 35);
            btnStop.TabIndex = 2;
            btnStop.Text = "停止回环";
            btnStop.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStart.Font = new Font("微软雅黑", 12F);
            btnStart.Location = new Point(460, 110);
            btnStart.MinimumSize = new Size(1, 1);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(100, 35);
            btnStart.TabIndex = 1;
            btnStart.Text = "启动回环";
            btnStart.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStart.Click += btnStart_Click;
            // 
            // gbSettings
            // 
            gbSettings.Controls.Add(nudBaudRate);
            gbSettings.Controls.Add(uiLabel3);
            gbSettings.Controls.Add(cboPort2);
            gbSettings.Controls.Add(uiLabel2);
            gbSettings.Controls.Add(cboPort1);
            gbSettings.Controls.Add(uiLabel1);
            gbSettings.Dock = DockStyle.Top;
            gbSettings.Font = new Font("微软雅黑", 12F);
            gbSettings.Location = new Point(10, 10);
            gbSettings.Margin = new Padding(4, 5, 4, 5);
            gbSettings.MinimumSize = new Size(1, 1);
            gbSettings.Name = "gbSettings";
            gbSettings.Padding = new Padding(10, 32, 10, 10);
            gbSettings.Size = new Size(680, 95);
            gbSettings.TabIndex = 0;
            gbSettings.Text = "回环配置";
            gbSettings.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // nudBaudRate
            // 
            nudBaudRate.Font = new Font("微软雅黑", 12F);
            nudBaudRate.Location = new Point(550, 35);
            nudBaudRate.Margin = new Padding(4, 5, 4, 5);
            nudBaudRate.Maximum = 115200D;
            nudBaudRate.Minimum = 1200D;
            nudBaudRate.MinimumSize = new Size(1, 16);
            nudBaudRate.Name = "nudBaudRate";
            nudBaudRate.Padding = new Padding(5);
            nudBaudRate.ShowText = false;
            nudBaudRate.Size = new Size(110, 29);
            nudBaudRate.TabIndex = 5;
            nudBaudRate.Text = "9600";
            nudBaudRate.TextAlignment = ContentAlignment.MiddleCenter;
            nudBaudRate.Value = 9600;
            // 
            // uiLabel3
            // 
            uiLabel3.Font = new Font("微软雅黑", 12F);
            uiLabel3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel3.Location = new Point(465, 35);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(80, 29);
            uiLabel3.TabIndex = 4;
            uiLabel3.Text = "波特率:";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboPort2
            // 
            cboPort2.DataSource = null;
            cboPort2.FillColor = Color.White;
            cboPort2.Font = new Font("微软雅黑", 12F);
            cboPort2.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboPort2.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboPort2.Location = new Point(325, 35);
            cboPort2.Margin = new Padding(4, 5, 4, 5);
            cboPort2.MinimumSize = new Size(63, 0);
            cboPort2.Name = "cboPort2";
            cboPort2.Padding = new Padding(0, 0, 30, 2);
            cboPort2.Size = new Size(120, 29);
            cboPort2.SymbolSize = 24;
            cboPort2.TabIndex = 3;
            cboPort2.TextAlignment = ContentAlignment.MiddleLeft;
            cboPort2.Watermark = "";
            // 
            // uiLabel2
            // 
            uiLabel2.Font = new Font("微软雅黑", 12F);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(240, 35);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(80, 29);
            uiLabel2.TabIndex = 2;
            uiLabel2.Text = "串口2:";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboPort1
            // 
            cboPort1.DataSource = null;
            cboPort1.FillColor = Color.White;
            cboPort1.Font = new Font("微软雅黑", 12F);
            cboPort1.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboPort1.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboPort1.Location = new Point(100, 35);
            cboPort1.Margin = new Padding(4, 5, 4, 5);
            cboPort1.MinimumSize = new Size(63, 0);
            cboPort1.Name = "cboPort1";
            cboPort1.Padding = new Padding(0, 0, 30, 2);
            cboPort1.Size = new Size(120, 29);
            cboPort1.SymbolSize = 24;
            cboPort1.TabIndex = 1;
            cboPort1.TextAlignment = ContentAlignment.MiddleLeft;
            cboPort1.Watermark = "";
            // 
            // uiLabel1
            // 
            uiLabel1.Font = new Font("微软雅黑", 12F);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(15, 35);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(80, 29);
            uiLabel1.TabIndex = 0;
            uiLabel1.Text = "串口1:";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(btnClearLog);
            uiGroupBox1.Controls.Add(rtbLog);
            uiGroupBox1.Dock = DockStyle.Fill;
            uiGroupBox1.Font = new Font("微软雅黑", 12F);
            uiGroupBox1.Location = new Point(0, 185);
            uiGroupBox1.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new Padding(10, 32, 10, 10);
            uiGroupBox1.Size = new Size(700, 334);
            uiGroupBox1.TabIndex = 1;
            uiGroupBox1.Text = "回环日志";
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnClearLog
            // 
            btnClearLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearLog.Font = new Font("微软雅黑", 9F);
            btnClearLog.Location = new Point(610, 30);
            btnClearLog.MinimumSize = new Size(1, 1);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(75, 28);
            btnClearLog.TabIndex = 1;
            btnClearLog.Text = "清空";
            btnClearLog.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnClearLog.Click += btnClearLog_Click;
            // 
            // rtbLog
            // 
            rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbLog.BackColor = Color.FromArgb(243, 249, 255);
            rtbLog.BorderStyle = BorderStyle.FixedSingle;
            rtbLog.Font = new Font("Consolas", 9F);
            rtbLog.Location = new Point(15, 64);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(670, 255);
            rtbLog.TabIndex = 0;
            rtbLog.Text = "";
            // 
            // FrmSerialLoopbackTest
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(700, 519);
            Controls.Add(uiGroupBox1);
            Controls.Add(uiPanel1);
            Name = "FrmSerialLoopbackTest";
            ShowIcon = false;
            Text = "串口回环测试工具";
            ZoomScaleRect = new Rectangle(15, 15, 700, 500);
            FormClosing += FrmSerialLoopbackTest_FormClosing;
            uiPanel1.ResumeLayout(false);
            gbSettings.ResumeLayout(false);
            uiGroupBox1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIButton btnStop;
        private Sunny.UI.UIButton btnStart;
        private Sunny.UI.UIGroupBox gbSettings;
        private Sunny.UI.UIIntegerUpDown nudBaudRate;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIComboBox cboPort2;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIComboBox cboPort1;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIButton btnClearLog;
        private System.Windows.Forms.RichTextBox rtbLog;
    }
}
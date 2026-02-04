namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    partial class FrmCommunicationTest
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
            btnConnect = new UIButton();
            pnlConfig = new UIPanel();
            uiLabel1 = new UILabel();
            cboProtocolType = new UIComboBox();
            uiPanel2 = new UIPanel();
            uiGroupBox2 = new UIGroupBox();
            rtbLog = new RichTextBox();
            btnClearLog = new UIButton();
            uiGroupBox1 = new UIGroupBox();
            txtSendData = new UITextBox();
            btnReceive = new UIButton();
            btnSend = new UIButton();
            uiLabel2 = new UILabel();
            cboDataFormat = new UIComboBox();
            uiLabel3 = new UILabel();
            nudTimeout = new UIIntegerUpDown();
            chkWaitResponse = new UICheckBox();
            uiPanel1.SuspendLayout();
            uiPanel2.SuspendLayout();
            uiGroupBox2.SuspendLayout();
            uiGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(btnConnect);
            uiPanel1.Controls.Add(pnlConfig);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(cboProtocolType);
            uiPanel1.Dock = DockStyle.Top;
            uiPanel1.Font = new Font("微软雅黑", 12F);
            uiPanel1.Location = new Point(0, 35);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Size = new Size(1000, 255);
            uiPanel1.TabIndex = 0;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnConnect
            // 
            btnConnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConnect.Font = new Font("微软雅黑", 12F);
            btnConnect.Location = new Point(870, 215);
            btnConnect.MinimumSize = new Size(1, 1);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(115, 35);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "连接";
            btnConnect.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnConnect.Click += btnConnect_Click;
            // 
            // pnlConfig
            // 
            pnlConfig.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlConfig.Font = new Font("微软雅黑", 12F);
            pnlConfig.Location = new Point(15, 50);
            pnlConfig.Margin = new Padding(4, 5, 4, 5);
            pnlConfig.MinimumSize = new Size(1, 1);
            pnlConfig.Name = "pnlConfig";
            pnlConfig.Size = new Size(970, 155);
            pnlConfig.TabIndex = 2;
            pnlConfig.Text = null;
            pnlConfig.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel1
            // 
            uiLabel1.Font = new Font("微软雅黑", 12F);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(15, 15);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(80, 29);
            uiLabel1.TabIndex = 1;
            uiLabel1.Text = "协议类型:";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboProtocolType
            // 
            cboProtocolType.DataSource = null;
            cboProtocolType.FillColor = Color.White;
            cboProtocolType.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboProtocolType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboProtocolType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboProtocolType.Location = new Point(100, 15);
            cboProtocolType.Margin = new Padding(4, 5, 4, 5);
            cboProtocolType.MinimumSize = new Size(63, 0);
            cboProtocolType.Name = "cboProtocolType";
            cboProtocolType.Padding = new Padding(0, 0, 30, 2);
            cboProtocolType.Size = new Size(150, 29);
            cboProtocolType.SymbolSize = 24;
            cboProtocolType.TabIndex = 0;
            cboProtocolType.TextAlignment = ContentAlignment.MiddleLeft;
            cboProtocolType.Watermark = "";
            cboProtocolType.SelectedIndexChanged += cboProtocolType_SelectedIndexChanged;
            // 
            // uiPanel2
            // 
            uiPanel2.Controls.Add(uiGroupBox2);
            uiPanel2.Controls.Add(uiGroupBox1);
            uiPanel2.Dock = DockStyle.Fill;
            uiPanel2.Font = new Font("微软雅黑", 12F);
            uiPanel2.Location = new Point(0, 290);
            uiPanel2.Margin = new Padding(4, 5, 4, 5);
            uiPanel2.MinimumSize = new Size(1, 1);
            uiPanel2.Name = "uiPanel2";
            uiPanel2.Padding = new Padding(5);
            uiPanel2.Size = new Size(1000, 508);
            uiPanel2.TabIndex = 1;
            uiPanel2.Text = null;
            uiPanel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiGroupBox2
            // 
            uiGroupBox2.Controls.Add(rtbLog);
            uiGroupBox2.Controls.Add(btnClearLog);
            uiGroupBox2.Dock = DockStyle.Fill;
            uiGroupBox2.Font = new Font("微软雅黑", 12F);
            uiGroupBox2.Location = new Point(5, 155);
            uiGroupBox2.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox2.MinimumSize = new Size(1, 1);
            uiGroupBox2.Name = "uiGroupBox2";
            uiGroupBox2.Padding = new Padding(10, 32, 10, 10);
            uiGroupBox2.Size = new Size(990, 348);
            uiGroupBox2.TabIndex = 1;
            uiGroupBox2.Text = "通讯日志";
            uiGroupBox2.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // rtbLog
            // 
            rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbLog.BackColor = Color.FromArgb(243, 249, 255);
            rtbLog.BorderStyle = BorderStyle.FixedSingle;
            rtbLog.Location = new Point(15, 64);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(960, 269);
            rtbLog.TabIndex = 1;
            rtbLog.Text = "";
            // 
            // btnClearLog
            // 
            btnClearLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearLog.Enabled = false;
            btnClearLog.Font = new Font("微软雅黑", 9F);
            btnClearLog.Location = new Point(900, 30);
            btnClearLog.MinimumSize = new Size(1, 1);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(75, 28);
            btnClearLog.TabIndex = 0;
            btnClearLog.Text = "清空";
            btnClearLog.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnClearLog.Click += btnClearLog_Click;
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(txtSendData);
            uiGroupBox1.Controls.Add(btnReceive);
            uiGroupBox1.Controls.Add(btnSend);
            uiGroupBox1.Controls.Add(uiLabel2);
            uiGroupBox1.Controls.Add(cboDataFormat);
            uiGroupBox1.Controls.Add(uiLabel3);
            uiGroupBox1.Controls.Add(nudTimeout);
            uiGroupBox1.Controls.Add(chkWaitResponse);
            uiGroupBox1.Dock = DockStyle.Top;
            uiGroupBox1.Font = new Font("微软雅黑", 12F);
            uiGroupBox1.Location = new Point(5, 5);
            uiGroupBox1.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new Padding(10, 32, 10, 10);
            uiGroupBox1.Size = new Size(990, 150);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = "数据收发";
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // txtSendData
            // 
            txtSendData.Font = new Font("微软雅黑", 12F);
            txtSendData.Location = new Point(15, 75);
            txtSendData.Margin = new Padding(4, 5, 4, 5);
            txtSendData.MinimumSize = new Size(1, 16);
            txtSendData.Multiline = true;
            txtSendData.Name = "txtSendData";
            txtSendData.Padding = new Padding(5);
            txtSendData.ShowText = false;
            txtSendData.Size = new Size(735, 65);
            txtSendData.TabIndex = 7;
            txtSendData.TextAlignment = ContentAlignment.MiddleLeft;
            txtSendData.Watermark = "输入要发送的数据...";
            // 
            // btnReceive
            // 
            btnReceive.Enabled = false;
            btnReceive.Font = new Font("微软雅黑", 12F);
            btnReceive.Location = new Point(870, 105);
            btnReceive.MinimumSize = new Size(1, 1);
            btnReceive.Name = "btnReceive";
            btnReceive.Size = new Size(100, 35);
            btnReceive.TabIndex = 6;
            btnReceive.Text = "接收";
            btnReceive.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnReceive.Click += btnReceive_Click;
            // 
            // btnSend
            // 
            btnSend.Enabled = false;
            btnSend.Font = new Font("微软雅黑", 12F);
            btnSend.Location = new Point(760, 105);
            btnSend.MinimumSize = new Size(1, 1);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(100, 35);
            btnSend.TabIndex = 5;
            btnSend.Text = "发送";
            btnSend.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSend.Click += btnSend_Click;
            // 
            // uiLabel2
            // 
            uiLabel2.Font = new Font("微软雅黑", 12F);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(330, 40);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(85, 29);
            uiLabel2.TabIndex = 4;
            uiLabel2.Text = "数据格式:";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboDataFormat
            // 
            cboDataFormat.DataSource = null;
            cboDataFormat.FillColor = Color.White;
            cboDataFormat.Font = new Font("微软雅黑", 12F);
            cboDataFormat.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboDataFormat.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboDataFormat.Location = new Point(420, 40);
            cboDataFormat.Margin = new Padding(4, 5, 4, 5);
            cboDataFormat.MinimumSize = new Size(63, 0);
            cboDataFormat.Name = "cboDataFormat";
            cboDataFormat.Padding = new Padding(0, 0, 30, 2);
            cboDataFormat.Size = new Size(100, 29);
            cboDataFormat.SymbolSize = 24;
            cboDataFormat.TabIndex = 3;
            cboDataFormat.TextAlignment = ContentAlignment.MiddleLeft;
            cboDataFormat.Watermark = "";
            // 
            // uiLabel3
            // 
            uiLabel3.Font = new Font("微软雅黑", 12F);
            uiLabel3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel3.Location = new Point(114, 40);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(101, 29);
            uiLabel3.TabIndex = 2;
            uiLabel3.Text = "超时(ms):";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // nudTimeout
            // 
            nudTimeout.Font = new Font("微软雅黑", 12F);
            nudTimeout.Location = new Point(220, 40);
            nudTimeout.Margin = new Padding(4, 5, 4, 5);
            nudTimeout.Maximum = 30000D;
            nudTimeout.Minimum = 100D;
            nudTimeout.MinimumSize = new Size(1, 16);
            nudTimeout.Name = "nudTimeout";
            nudTimeout.Padding = new Padding(5);
            nudTimeout.ShowText = false;
            nudTimeout.Size = new Size(109, 29);
            nudTimeout.TabIndex = 1;
            nudTimeout.Text = "3000";
            nudTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            nudTimeout.Value = 3000;
            // 
            // chkWaitResponse
            // 
            chkWaitResponse.Checked = true;
            chkWaitResponse.Font = new Font("微软雅黑", 12F);
            chkWaitResponse.ForeColor = Color.FromArgb(48, 48, 48);
            chkWaitResponse.Location = new Point(15, 40);
            chkWaitResponse.MinimumSize = new Size(1, 1);
            chkWaitResponse.Name = "chkWaitResponse";
            chkWaitResponse.Size = new Size(120, 29);
            chkWaitResponse.TabIndex = 0;
            chkWaitResponse.Text = "等待响应";
            // 
            // FrmCommunicationTest
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1000, 798);
            Controls.Add(uiPanel2);
            Controls.Add(uiPanel1);
            Name = "FrmCommunicationTest";
            ShowIcon = false;
            Text = "通讯测试工具";
            ZoomScaleRect = new Rectangle(15, 15, 1000, 700);
            FormClosing += FrmCommunicationTest_FormClosing;
            uiPanel1.ResumeLayout(false);
            uiPanel2.ResumeLayout(false);
            uiGroupBox2.ResumeLayout(false);
            uiGroupBox1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIButton btnConnect;
        private Sunny.UI.UIPanel pnlConfig;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIComboBox cboProtocolType;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.RichTextBox rtbLog;
        private Sunny.UI.UIButton btnClearLog;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UITextBox txtSendData;
        private Sunny.UI.UIButton btnReceive;
        private Sunny.UI.UIButton btnSend;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIComboBox cboDataFormat;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIIntegerUpDown nudTimeout;
        private Sunny.UI.UICheckBox chkWaitResponse;
    }
}
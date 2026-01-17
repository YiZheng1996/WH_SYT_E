namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_EthernetSend
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelDescription = new Panel();
            lblDescription = new UILabel();
            txtDescription = new UITextBox();
            chkEnabled = new UICheckBox();
            panelMain = new Panel();
            grpConnection = new UIGroupBox();
            lblIPAddress = new UILabel();
            txtIPAddress = new UITextBox();
            lblPort = new UILabel();
            txtPort = new UITextBox();
            lblProtocol = new UILabel();
            cmbProtocol = new UIComboBox();
            btnTestConnection = new UISymbolButton();
            lblConnectionStatus = new UILabel();
            grpTimeout = new UIGroupBox();
            lblConnectTimeout = new UILabel();
            numConnectTimeout = new UIIntegerUpDown();
            lblSendTimeout = new UILabel();
            numSendTimeout = new UIIntegerUpDown();
            lblReceiveTimeout = new UILabel();
            numReceiveTimeout = new UIIntegerUpDown();
            grpDataSettings = new UIGroupBox();
            lblDataFormat = new UILabel();
            cmbDataFormat = new UIComboBox();
            lblEncoding = new UILabel();
            cmbEncoding = new UIComboBox();
            lblSendContent = new UILabel();
            txtSendContent = new UITextBox();
            btnInsertVariable = new UISymbolButton();
            chkAppendNewLine = new UICheckBox();
            lblNewLineType = new UILabel();
            cmbNewLineType = new UIComboBox();
            grpResponseSettings = new UIGroupBox();
            chkWaitResponse = new UICheckBox();
            lblResponseTimeout = new UILabel();
            numResponseTimeout = new UIIntegerUpDown();
            lblResponseVariable = new UILabel();
            cmbResponseVariable = new UIComboBox();
            btnCreateVariable = new UISymbolButton();
            chkDisconnectAfterSend = new UICheckBox();
            lblCondition = new UILabel();
            txtCondition = new UITextBox();
            panelBottom = new Panel();
            btnTestSend = new UIButton();
            btnHelp = new UIButton();
            btnCancel = new UIButton();
            btnSave = new UIButton();
            panelDescription.SuspendLayout();
            panelMain.SuspendLayout();
            grpConnection.SuspendLayout();
            grpTimeout.SuspendLayout();
            grpDataSettings.SuspendLayout();
            grpResponseSettings.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelDescription
            // 
            panelDescription.BackColor = Color.White;
            panelDescription.BorderStyle = BorderStyle.FixedSingle;
            panelDescription.Controls.Add(lblDescription);
            panelDescription.Controls.Add(txtDescription);
            panelDescription.Controls.Add(chkEnabled);
            panelDescription.Dock = DockStyle.Top;
            panelDescription.Location = new Point(0, 35);
            panelDescription.Name = "panelDescription";
            panelDescription.Padding = new Padding(15, 10, 15, 10);
            panelDescription.Size = new Size(800, 70);
            panelDescription.TabIndex = 0;
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("微软雅黑", 10F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(18, 20);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(100, 25);
            lblDescription.TabIndex = 0;
            lblDescription.Text = "步骤描述:";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            txtDescription.Cursor = Cursors.IBeam;
            txtDescription.Font = new Font("微软雅黑", 10F);
            txtDescription.Location = new Point(124, 20);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.RectColor = Color.FromArgb(65, 100, 204);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(500, 30);
            txtDescription.TabIndex = 1;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "请输入步骤描述信息";
            // 
            // chkEnabled
            // 
            chkEnabled.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkEnabled.CheckBoxSize = 18;
            chkEnabled.Checked = true;
            chkEnabled.Font = new Font("微软雅黑", 10F);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(650, 20);
            chkEnabled.MinimumSize = new Size(1, 1);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(120, 30);
            chkEnabled.TabIndex = 2;
            chkEnabled.Text = "启用此步骤";
            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.BackColor = Color.FromArgb(248, 249, 250);
            panelMain.Controls.Add(grpConnection);
            panelMain.Controls.Add(grpTimeout);
            panelMain.Controls.Add(grpDataSettings);
            panelMain.Controls.Add(grpResponseSettings);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 105);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15);
            panelMain.Size = new Size(800, 594);
            panelMain.TabIndex = 1;
            // 
            // grpConnection
            // 
            grpConnection.Controls.Add(lblIPAddress);
            grpConnection.Controls.Add(txtIPAddress);
            grpConnection.Controls.Add(lblPort);
            grpConnection.Controls.Add(txtPort);
            grpConnection.Controls.Add(lblProtocol);
            grpConnection.Controls.Add(cmbProtocol);
            grpConnection.Controls.Add(btnTestConnection);
            grpConnection.Controls.Add(lblConnectionStatus);
            grpConnection.Dock = DockStyle.Top;
            grpConnection.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpConnection.ForeColor = Color.FromArgb(65, 100, 204);
            grpConnection.Location = new Point(15, 446);
            grpConnection.Margin = new Padding(4, 5, 4, 5);
            grpConnection.MinimumSize = new Size(1, 1);
            grpConnection.Name = "grpConnection";
            grpConnection.Padding = new Padding(0, 32, 0, 0);
            grpConnection.RectColor = Color.FromArgb(65, 100, 204);
            grpConnection.Size = new Size(770, 130);
            grpConnection.TabIndex = 0;
            grpConnection.Text = "连接设置";
            grpConnection.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblIPAddress
            // 
            lblIPAddress.AutoSize = true;
            lblIPAddress.Font = new Font("微软雅黑", 9F);
            lblIPAddress.ForeColor = Color.FromArgb(48, 48, 48);
            lblIPAddress.Location = new Point(20, 45);
            lblIPAddress.Name = "lblIPAddress";
            lblIPAddress.Size = new Size(46, 17);
            lblIPAddress.TabIndex = 0;
            lblIPAddress.Text = "IP地址:";
            // 
            // txtIPAddress
            // 
            txtIPAddress.Cursor = Cursors.IBeam;
            txtIPAddress.Font = new Font("微软雅黑", 9F);
            txtIPAddress.Location = new Point(90, 42);
            txtIPAddress.Margin = new Padding(4, 5, 4, 5);
            txtIPAddress.MinimumSize = new Size(1, 16);
            txtIPAddress.Name = "txtIPAddress";
            txtIPAddress.Padding = new Padding(5);
            txtIPAddress.RectColor = Color.FromArgb(65, 100, 204);
            txtIPAddress.ShowText = false;
            txtIPAddress.Size = new Size(150, 29);
            txtIPAddress.TabIndex = 1;
            txtIPAddress.TextAlignment = ContentAlignment.MiddleLeft;
            txtIPAddress.Watermark = "192.168.1.100";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("微软雅黑", 9F);
            lblPort.ForeColor = Color.FromArgb(48, 48, 48);
            lblPort.Location = new Point(260, 45);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(35, 17);
            lblPort.TabIndex = 2;
            lblPort.Text = "端口:";
            // 
            // txtPort
            // 
            txtPort.Cursor = Cursors.IBeam;
            txtPort.Font = new Font("微软雅黑", 9F);
            txtPort.Location = new Point(310, 42);
            txtPort.Margin = new Padding(4, 5, 4, 5);
            txtPort.MinimumSize = new Size(1, 16);
            txtPort.Name = "txtPort";
            txtPort.Padding = new Padding(5);
            txtPort.RectColor = Color.FromArgb(65, 100, 204);
            txtPort.ShowText = false;
            txtPort.Size = new Size(100, 29);
            txtPort.TabIndex = 3;
            txtPort.TextAlignment = ContentAlignment.MiddleLeft;
            txtPort.Watermark = "8080";
            // 
            // lblProtocol
            // 
            lblProtocol.AutoSize = true;
            lblProtocol.Font = new Font("微软雅黑", 9F);
            lblProtocol.ForeColor = Color.FromArgb(48, 48, 48);
            lblProtocol.Location = new Point(430, 45);
            lblProtocol.Name = "lblProtocol";
            lblProtocol.Size = new Size(35, 17);
            lblProtocol.TabIndex = 4;
            lblProtocol.Text = "协议:";
            // 
            // cmbProtocol
            // 
            cmbProtocol.DataSource = null;
            cmbProtocol.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbProtocol.FillColor = Color.White;
            cmbProtocol.Font = new Font("微软雅黑", 9F);
            cmbProtocol.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbProtocol.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbProtocol.Location = new Point(480, 42);
            cmbProtocol.Margin = new Padding(4, 5, 4, 5);
            cmbProtocol.MinimumSize = new Size(63, 0);
            cmbProtocol.Name = "cmbProtocol";
            cmbProtocol.Padding = new Padding(0, 0, 30, 2);
            cmbProtocol.RectColor = Color.FromArgb(65, 100, 204);
            cmbProtocol.Size = new Size(100, 29);
            cmbProtocol.SymbolSize = 24;
            cmbProtocol.TabIndex = 5;
            cmbProtocol.TextAlignment = ContentAlignment.MiddleLeft;
            cmbProtocol.Watermark = "";
            // 
            // btnTestConnection
            // 
            btnTestConnection.Cursor = Cursors.Hand;
            btnTestConnection.FillColor = Color.FromArgb(40, 167, 69);
            btnTestConnection.Font = new Font("微软雅黑", 9F);
            btnTestConnection.Location = new Point(20, 85);
            btnTestConnection.MinimumSize = new Size(1, 1);
            btnTestConnection.Name = "btnTestConnection";
            btnTestConnection.Size = new Size(100, 32);
            btnTestConnection.Symbol = 61714;
            btnTestConnection.TabIndex = 6;
            btnTestConnection.Text = "测试连接";
            btnTestConnection.TipsFont = new Font("微软雅黑", 9F);
            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.Font = new Font("微软雅黑", 9F);
            lblConnectionStatus.ForeColor = Color.Gray;
            lblConnectionStatus.Location = new Point(130, 92);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new Size(44, 17);
            lblConnectionStatus.TabIndex = 7;
            lblConnectionStatus.Text = "未测试";
            // 
            // grpTimeout
            // 
            grpTimeout.Controls.Add(lblConnectTimeout);
            grpTimeout.Controls.Add(numConnectTimeout);
            grpTimeout.Controls.Add(lblSendTimeout);
            grpTimeout.Controls.Add(numSendTimeout);
            grpTimeout.Controls.Add(lblReceiveTimeout);
            grpTimeout.Controls.Add(numReceiveTimeout);
            grpTimeout.Dock = DockStyle.Top;
            grpTimeout.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpTimeout.ForeColor = Color.FromArgb(65, 100, 204);
            grpTimeout.Location = new Point(15, 356);
            grpTimeout.Margin = new Padding(4, 5, 4, 5);
            grpTimeout.MinimumSize = new Size(1, 1);
            grpTimeout.Name = "grpTimeout";
            grpTimeout.Padding = new Padding(0, 32, 0, 0);
            grpTimeout.RectColor = Color.FromArgb(65, 100, 204);
            grpTimeout.Size = new Size(770, 90);
            grpTimeout.TabIndex = 1;
            grpTimeout.Text = "超时设置";
            grpTimeout.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblConnectTimeout
            // 
            lblConnectTimeout.AutoSize = true;
            lblConnectTimeout.Font = new Font("微软雅黑", 9F);
            lblConnectTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblConnectTimeout.Location = new Point(20, 45);
            lblConnectTimeout.Name = "lblConnectTimeout";
            lblConnectTimeout.Size = new Size(79, 17);
            lblConnectTimeout.TabIndex = 0;
            lblConnectTimeout.Text = "连接超时(秒):";
            // 
            // numConnectTimeout
            // 
            numConnectTimeout.Font = new Font("微软雅黑", 9F);
            numConnectTimeout.Location = new Point(115, 42);
            numConnectTimeout.Margin = new Padding(4, 5, 4, 5);
            numConnectTimeout.Maximum = 300D;
            numConnectTimeout.Minimum = 1D;
            numConnectTimeout.MinimumSize = new Size(100, 0);
            numConnectTimeout.Name = "numConnectTimeout";
            numConnectTimeout.Padding = new Padding(5);
            numConnectTimeout.RectColor = Color.FromArgb(65, 100, 204);
            numConnectTimeout.ShowText = false;
            numConnectTimeout.Size = new Size(100, 29);
            numConnectTimeout.TabIndex = 1;
            numConnectTimeout.Text = "5";
            numConnectTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numConnectTimeout.Value = 5;
            // 
            // lblSendTimeout
            // 
            lblSendTimeout.AutoSize = true;
            lblSendTimeout.Font = new Font("微软雅黑", 9F);
            lblSendTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblSendTimeout.Location = new Point(250, 45);
            lblSendTimeout.Name = "lblSendTimeout";
            lblSendTimeout.Size = new Size(79, 17);
            lblSendTimeout.TabIndex = 2;
            lblSendTimeout.Text = "发送超时(秒):";
            // 
            // numSendTimeout
            // 
            numSendTimeout.Font = new Font("微软雅黑", 9F);
            numSendTimeout.Location = new Point(345, 42);
            numSendTimeout.Margin = new Padding(4, 5, 4, 5);
            numSendTimeout.Maximum = 300D;
            numSendTimeout.Minimum = 1D;
            numSendTimeout.MinimumSize = new Size(100, 0);
            numSendTimeout.Name = "numSendTimeout";
            numSendTimeout.Padding = new Padding(5);
            numSendTimeout.RectColor = Color.FromArgb(65, 100, 204);
            numSendTimeout.ShowText = false;
            numSendTimeout.Size = new Size(100, 29);
            numSendTimeout.TabIndex = 3;
            numSendTimeout.Text = "3";
            numSendTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numSendTimeout.Value = 3;
            // 
            // lblReceiveTimeout
            // 
            lblReceiveTimeout.AutoSize = true;
            lblReceiveTimeout.Font = new Font("微软雅黑", 9F);
            lblReceiveTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblReceiveTimeout.Location = new Point(480, 45);
            lblReceiveTimeout.Name = "lblReceiveTimeout";
            lblReceiveTimeout.Size = new Size(79, 17);
            lblReceiveTimeout.TabIndex = 4;
            lblReceiveTimeout.Text = "接收超时(秒):";
            // 
            // numReceiveTimeout
            // 
            numReceiveTimeout.Font = new Font("微软雅黑", 9F);
            numReceiveTimeout.Location = new Point(575, 42);
            numReceiveTimeout.Margin = new Padding(4, 5, 4, 5);
            numReceiveTimeout.Maximum = 300D;
            numReceiveTimeout.Minimum = 1D;
            numReceiveTimeout.MinimumSize = new Size(100, 0);
            numReceiveTimeout.Name = "numReceiveTimeout";
            numReceiveTimeout.Padding = new Padding(5);
            numReceiveTimeout.RectColor = Color.FromArgb(65, 100, 204);
            numReceiveTimeout.ShowText = false;
            numReceiveTimeout.Size = new Size(100, 29);
            numReceiveTimeout.TabIndex = 5;
            numReceiveTimeout.Text = "5";
            numReceiveTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numReceiveTimeout.Value = 5;
            // 
            // grpDataSettings
            // 
            grpDataSettings.Controls.Add(lblDataFormat);
            grpDataSettings.Controls.Add(cmbDataFormat);
            grpDataSettings.Controls.Add(lblEncoding);
            grpDataSettings.Controls.Add(cmbEncoding);
            grpDataSettings.Controls.Add(lblSendContent);
            grpDataSettings.Controls.Add(txtSendContent);
            grpDataSettings.Controls.Add(btnInsertVariable);
            grpDataSettings.Controls.Add(chkAppendNewLine);
            grpDataSettings.Controls.Add(lblNewLineType);
            grpDataSettings.Controls.Add(cmbNewLineType);
            grpDataSettings.Dock = DockStyle.Top;
            grpDataSettings.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpDataSettings.ForeColor = Color.FromArgb(65, 100, 204);
            grpDataSettings.Location = new Point(15, 155);
            grpDataSettings.Margin = new Padding(4, 5, 4, 5);
            grpDataSettings.MinimumSize = new Size(1, 1);
            grpDataSettings.Name = "grpDataSettings";
            grpDataSettings.Padding = new Padding(0, 32, 0, 0);
            grpDataSettings.RectColor = Color.FromArgb(65, 100, 204);
            grpDataSettings.Size = new Size(770, 201);
            grpDataSettings.TabIndex = 2;
            grpDataSettings.Text = "数据设置";
            grpDataSettings.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblDataFormat
            // 
            lblDataFormat.AutoSize = true;
            lblDataFormat.Font = new Font("微软雅黑", 9F);
            lblDataFormat.ForeColor = Color.FromArgb(48, 48, 48);
            lblDataFormat.Location = new Point(20, 45);
            lblDataFormat.Name = "lblDataFormat";
            lblDataFormat.Size = new Size(59, 17);
            lblDataFormat.TabIndex = 0;
            lblDataFormat.Text = "数据格式:";
            // 
            // cmbDataFormat
            // 
            cmbDataFormat.DataSource = null;
            cmbDataFormat.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbDataFormat.FillColor = Color.White;
            cmbDataFormat.Font = new Font("微软雅黑", 9F);
            cmbDataFormat.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDataFormat.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDataFormat.Location = new Point(95, 42);
            cmbDataFormat.Margin = new Padding(4, 5, 4, 5);
            cmbDataFormat.MinimumSize = new Size(63, 0);
            cmbDataFormat.Name = "cmbDataFormat";
            cmbDataFormat.Padding = new Padding(0, 0, 30, 2);
            cmbDataFormat.RectColor = Color.FromArgb(65, 100, 204);
            cmbDataFormat.Size = new Size(120, 29);
            cmbDataFormat.SymbolSize = 24;
            cmbDataFormat.TabIndex = 1;
            cmbDataFormat.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDataFormat.Watermark = "";
            // 
            // lblEncoding
            // 
            lblEncoding.AutoSize = true;
            lblEncoding.Font = new Font("微软雅黑", 9F);
            lblEncoding.ForeColor = Color.FromArgb(48, 48, 48);
            lblEncoding.Location = new Point(250, 45);
            lblEncoding.Name = "lblEncoding";
            lblEncoding.Size = new Size(59, 17);
            lblEncoding.TabIndex = 2;
            lblEncoding.Text = "文本编码:";
            // 
            // cmbEncoding
            // 
            cmbEncoding.DataSource = null;
            cmbEncoding.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbEncoding.FillColor = Color.White;
            cmbEncoding.Font = new Font("微软雅黑", 9F);
            cmbEncoding.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbEncoding.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbEncoding.Location = new Point(325, 42);
            cmbEncoding.Margin = new Padding(4, 5, 4, 5);
            cmbEncoding.MinimumSize = new Size(63, 0);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Padding = new Padding(0, 0, 30, 2);
            cmbEncoding.RectColor = Color.FromArgb(65, 100, 204);
            cmbEncoding.Size = new Size(120, 29);
            cmbEncoding.SymbolSize = 24;
            cmbEncoding.TabIndex = 3;
            cmbEncoding.TextAlignment = ContentAlignment.MiddleLeft;
            cmbEncoding.Watermark = "";
            // 
            // lblSendContent
            // 
            lblSendContent.AutoSize = true;
            lblSendContent.Font = new Font("微软雅黑", 9F);
            lblSendContent.ForeColor = Color.FromArgb(48, 48, 48);
            lblSendContent.Location = new Point(20, 85);
            lblSendContent.Name = "lblSendContent";
            lblSendContent.Size = new Size(59, 17);
            lblSendContent.TabIndex = 4;
            lblSendContent.Text = "发送内容:";
            // 
            // txtSendContent
            // 
            txtSendContent.Cursor = Cursors.IBeam;
            txtSendContent.Font = new Font("微软雅黑", 9F);
            txtSendContent.Location = new Point(95, 82);
            txtSendContent.Margin = new Padding(4, 5, 4, 5);
            txtSendContent.MinimumSize = new Size(1, 16);
            txtSendContent.Multiline = true;
            txtSendContent.Name = "txtSendContent";
            txtSendContent.Padding = new Padding(5);
            txtSendContent.RectColor = Color.FromArgb(65, 100, 204);
            txtSendContent.ShowText = false;
            txtSendContent.Size = new Size(520, 60);
            txtSendContent.TabIndex = 5;
            txtSendContent.TextAlignment = ContentAlignment.TopLeft;
            txtSendContent.Watermark = "输入发送内容,支持 {变量名} 格式引用变量";
            // 
            // btnInsertVariable
            // 
            btnInsertVariable.Cursor = Cursors.Hand;
            btnInsertVariable.FillColor = Color.FromArgb(65, 100, 204);
            btnInsertVariable.Font = new Font("微软雅黑", 9F);
            btnInsertVariable.Location = new Point(625, 82);
            btnInsertVariable.MinimumSize = new Size(1, 1);
            btnInsertVariable.Name = "btnInsertVariable";
            btnInsertVariable.Size = new Size(100, 32);
            btnInsertVariable.Symbol = 61618;
            btnInsertVariable.TabIndex = 6;
            btnInsertVariable.Text = "插入变量";
            btnInsertVariable.TipsFont = new Font("微软雅黑", 9F);
            // 
            // chkAppendNewLine
            // 
            chkAppendNewLine.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkAppendNewLine.Font = new Font("微软雅黑", 9F);
            chkAppendNewLine.ForeColor = Color.FromArgb(48, 48, 48);
            chkAppendNewLine.Location = new Point(95, 155);
            chkAppendNewLine.MinimumSize = new Size(1, 1);
            chkAppendNewLine.Name = "chkAppendNewLine";
            chkAppendNewLine.Size = new Size(100, 24);
            chkAppendNewLine.TabIndex = 7;
            chkAppendNewLine.Text = "追加换行符";
            // 
            // lblNewLineType
            // 
            lblNewLineType.AutoSize = true;
            lblNewLineType.Font = new Font("微软雅黑", 9F);
            lblNewLineType.ForeColor = Color.FromArgb(48, 48, 48);
            lblNewLineType.Location = new Point(210, 157);
            lblNewLineType.Name = "lblNewLineType";
            lblNewLineType.Size = new Size(59, 17);
            lblNewLineType.TabIndex = 8;
            lblNewLineType.Text = "换行类型:";
            // 
            // cmbNewLineType
            // 
            cmbNewLineType.DataSource = null;
            cmbNewLineType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbNewLineType.Enabled = false;
            cmbNewLineType.FillColor = Color.White;
            cmbNewLineType.Font = new Font("微软雅黑", 9F);
            cmbNewLineType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbNewLineType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbNewLineType.Location = new Point(285, 152);
            cmbNewLineType.Margin = new Padding(4, 5, 4, 5);
            cmbNewLineType.MinimumSize = new Size(63, 0);
            cmbNewLineType.Name = "cmbNewLineType";
            cmbNewLineType.Padding = new Padding(0, 0, 30, 2);
            cmbNewLineType.RectColor = Color.FromArgb(65, 100, 204);
            cmbNewLineType.Size = new Size(140, 29);
            cmbNewLineType.SymbolSize = 24;
            cmbNewLineType.TabIndex = 9;
            cmbNewLineType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbNewLineType.Watermark = "";
            // 
            // grpResponseSettings
            // 
            grpResponseSettings.Controls.Add(chkWaitResponse);
            grpResponseSettings.Controls.Add(lblResponseTimeout);
            grpResponseSettings.Controls.Add(numResponseTimeout);
            grpResponseSettings.Controls.Add(lblResponseVariable);
            grpResponseSettings.Controls.Add(cmbResponseVariable);
            grpResponseSettings.Controls.Add(btnCreateVariable);
            grpResponseSettings.Controls.Add(chkDisconnectAfterSend);
            grpResponseSettings.Controls.Add(lblCondition);
            grpResponseSettings.Controls.Add(txtCondition);
            grpResponseSettings.Dock = DockStyle.Top;
            grpResponseSettings.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpResponseSettings.ForeColor = Color.FromArgb(65, 100, 204);
            grpResponseSettings.Location = new Point(15, 15);
            grpResponseSettings.Margin = new Padding(4, 5, 4, 5);
            grpResponseSettings.MinimumSize = new Size(1, 1);
            grpResponseSettings.Name = "grpResponseSettings";
            grpResponseSettings.Padding = new Padding(0, 32, 0, 0);
            grpResponseSettings.RectColor = Color.FromArgb(65, 100, 204);
            grpResponseSettings.Size = new Size(770, 140);
            grpResponseSettings.TabIndex = 3;
            grpResponseSettings.Text = "响应与条件设置";
            grpResponseSettings.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // chkWaitResponse
            // 
            chkWaitResponse.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkWaitResponse.Font = new Font("微软雅黑", 9F);
            chkWaitResponse.ForeColor = Color.FromArgb(48, 48, 48);
            chkWaitResponse.Location = new Point(20, 46);
            chkWaitResponse.MinimumSize = new Size(1, 1);
            chkWaitResponse.Name = "chkWaitResponse";
            chkWaitResponse.Size = new Size(90, 24);
            chkWaitResponse.TabIndex = 0;
            chkWaitResponse.Text = "等待响应";
            // 
            // lblResponseTimeout
            // 
            lblResponseTimeout.AutoSize = true;
            lblResponseTimeout.Font = new Font("微软雅黑", 9F);
            lblResponseTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblResponseTimeout.Location = new Point(125, 50);
            lblResponseTimeout.Name = "lblResponseTimeout";
            lblResponseTimeout.Size = new Size(55, 17);
            lblResponseTimeout.TabIndex = 1;
            lblResponseTimeout.Text = "超时(秒):";
            // 
            // numResponseTimeout
            // 
            numResponseTimeout.Enabled = false;
            numResponseTimeout.Font = new Font("微软雅黑", 9F);
            numResponseTimeout.Location = new Point(195, 45);
            numResponseTimeout.Margin = new Padding(4, 5, 4, 5);
            numResponseTimeout.Maximum = 300D;
            numResponseTimeout.Minimum = 1D;
            numResponseTimeout.MinimumSize = new Size(100, 0);
            numResponseTimeout.Name = "numResponseTimeout";
            numResponseTimeout.Padding = new Padding(5);
            numResponseTimeout.RectColor = Color.FromArgb(65, 100, 204);
            numResponseTimeout.ShowText = false;
            numResponseTimeout.Size = new Size(100, 29);
            numResponseTimeout.TabIndex = 2;
            numResponseTimeout.Text = "5";
            numResponseTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numResponseTimeout.Value = 5;
            // 
            // lblResponseVariable
            // 
            lblResponseVariable.AutoSize = true;
            lblResponseVariable.Font = new Font("微软雅黑", 9F);
            lblResponseVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblResponseVariable.Location = new Point(315, 48);
            lblResponseVariable.Name = "lblResponseVariable";
            lblResponseVariable.Size = new Size(59, 17);
            lblResponseVariable.TabIndex = 3;
            lblResponseVariable.Text = "响应变量:";
            // 
            // cmbResponseVariable
            // 
            cmbResponseVariable.DataSource = null;
            cmbResponseVariable.Enabled = false;
            cmbResponseVariable.FillColor = Color.White;
            cmbResponseVariable.Font = new Font("微软雅黑", 9F);
            cmbResponseVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbResponseVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbResponseVariable.Location = new Point(380, 44);
            cmbResponseVariable.Margin = new Padding(4, 5, 4, 5);
            cmbResponseVariable.MinimumSize = new Size(63, 0);
            cmbResponseVariable.Name = "cmbResponseVariable";
            cmbResponseVariable.Padding = new Padding(0, 0, 30, 2);
            cmbResponseVariable.RectColor = Color.FromArgb(65, 100, 204);
            cmbResponseVariable.Size = new Size(180, 29);
            cmbResponseVariable.SymbolSize = 24;
            cmbResponseVariable.TabIndex = 4;
            cmbResponseVariable.TextAlignment = ContentAlignment.MiddleLeft;
            cmbResponseVariable.Watermark = "输入或选择变量名";
            // 
            // btnCreateVariable
            // 
            btnCreateVariable.Cursor = Cursors.Hand;
            btnCreateVariable.Enabled = false;
            btnCreateVariable.FillColor = Color.FromArgb(65, 100, 204);
            btnCreateVariable.Font = new Font("微软雅黑", 9F);
            btnCreateVariable.Location = new Point(573, 45);
            btnCreateVariable.MinimumSize = new Size(1, 1);
            btnCreateVariable.Name = "btnCreateVariable";
            btnCreateVariable.Size = new Size(80, 29);
            btnCreateVariable.Symbol = 61543;
            btnCreateVariable.TabIndex = 5;
            btnCreateVariable.Text = "新建";
            btnCreateVariable.TipsFont = new Font("微软雅黑", 9F);
            // 
            // chkDisconnectAfterSend
            // 
            chkDisconnectAfterSend.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkDisconnectAfterSend.Checked = true;
            chkDisconnectAfterSend.Font = new Font("微软雅黑", 9F);
            chkDisconnectAfterSend.ForeColor = Color.FromArgb(48, 48, 48);
            chkDisconnectAfterSend.Location = new Point(20, 90);
            chkDisconnectAfterSend.MinimumSize = new Size(1, 1);
            chkDisconnectAfterSend.Name = "chkDisconnectAfterSend";
            chkDisconnectAfterSend.Size = new Size(150, 24);
            chkDisconnectAfterSend.TabIndex = 6;
            chkDisconnectAfterSend.Text = "发送后断开连接";
            // 
            // lblCondition
            // 
            lblCondition.AutoSize = true;
            lblCondition.Font = new Font("微软雅黑", 9F);
            lblCondition.ForeColor = Color.FromArgb(48, 48, 48);
            lblCondition.Location = new Point(240, 92);
            lblCondition.Name = "lblCondition";
            lblCondition.Size = new Size(59, 17);
            lblCondition.TabIndex = 7;
            lblCondition.Text = "执行条件:";
            // 
            // txtCondition
            // 
            txtCondition.Cursor = Cursors.IBeam;
            txtCondition.Font = new Font("微软雅黑", 9F);
            txtCondition.Location = new Point(315, 85);
            txtCondition.Margin = new Padding(4, 5, 4, 5);
            txtCondition.MinimumSize = new Size(1, 16);
            txtCondition.Name = "txtCondition";
            txtCondition.Padding = new Padding(5);
            txtCondition.RectColor = Color.FromArgb(65, 100, 204);
            txtCondition.ShowText = false;
            txtCondition.Size = new Size(410, 30);
            txtCondition.TabIndex = 8;
            txtCondition.TextAlignment = ContentAlignment.MiddleLeft;
            txtCondition.Watermark = "可选,为空时总是执行";
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(248, 249, 250);
            panelBottom.Controls.Add(btnTestSend);
            panelBottom.Controls.Add(btnHelp);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 699);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(15, 10, 15, 10);
            panelBottom.Size = new Size(800, 70);
            panelBottom.TabIndex = 2;
            // 
            // btnTestSend
            // 
            btnTestSend.Cursor = Cursors.Hand;
            btnTestSend.FillColor = Color.FromArgb(40, 167, 69);
            btnTestSend.Font = new Font("微软雅黑", 11F);
            btnTestSend.Location = new Point(20, 15);
            btnTestSend.MinimumSize = new Size(1, 1);
            btnTestSend.Name = "btnTestSend";
            btnTestSend.Size = new Size(100, 40);
            btnTestSend.TabIndex = 0;
            btnTestSend.Text = "测试发送";
            btnTestSend.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.FillColor = Color.White;
            btnHelp.Font = new Font("微软雅黑", 11F);
            btnHelp.ForeColor = Color.FromArgb(65, 100, 204);
            btnHelp.Location = new Point(140, 15);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.RectColor = Color.FromArgb(65, 100, 204);
            btnHelp.Size = new Size(80, 40);
            btnHelp.Style = UIStyle.Custom;
            btnHelp.StyleCustomMode = true;
            btnHelp.TabIndex = 1;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("微软雅黑", 11F);
            btnCancel.ForeColor = Color.FromArgb(48, 48, 48);
            btnCancel.Location = new Point(680, 15);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(180, 180, 180);
            btnCancel.Size = new Size(100, 40);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.StyleCustomMode = true;
            btnCancel.TabIndex = 3;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.FromArgb(65, 100, 204);
            btnSave.Font = new Font("微软雅黑", 11F);
            btnSave.Location = new Point(560, 15);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 2;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            // 
            // Form_EthernetSend
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(800, 769);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelDescription);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_EthernetSend";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Text = "以太网发送配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 700);
            panelDescription.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            grpConnection.ResumeLayout(false);
            grpConnection.PerformLayout();
            grpTimeout.ResumeLayout(false);
            grpTimeout.PerformLayout();
            grpDataSettings.ResumeLayout(false);
            grpDataSettings.PerformLayout();
            grpResponseSettings.ResumeLayout(false);
            grpResponseSettings.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // 面板
        private System.Windows.Forms.Panel panelDescription;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelBottom;

        // 顶部描述区
        private Sunny.UI.UILabel lblDescription;
        private Sunny.UI.UITextBox txtDescription;
        private Sunny.UI.UICheckBox chkEnabled;

        // 连接设置组
        private Sunny.UI.UIGroupBox grpConnection;
        private Sunny.UI.UILabel lblIPAddress;
        private Sunny.UI.UITextBox txtIPAddress;
        private Sunny.UI.UILabel lblPort;
        private Sunny.UI.UITextBox txtPort;
        private Sunny.UI.UILabel lblProtocol;
        private Sunny.UI.UIComboBox cmbProtocol;
        private Sunny.UI.UISymbolButton btnTestConnection;
        private Sunny.UI.UILabel lblConnectionStatus;

        // 超时设置组
        private Sunny.UI.UIGroupBox grpTimeout;
        private Sunny.UI.UILabel lblConnectTimeout;
        private Sunny.UI.UIIntegerUpDown numConnectTimeout;
        private Sunny.UI.UILabel lblSendTimeout;
        private Sunny.UI.UIIntegerUpDown numSendTimeout;
        private Sunny.UI.UILabel lblReceiveTimeout;
        private Sunny.UI.UIIntegerUpDown numReceiveTimeout;

        // 数据设置组
        private Sunny.UI.UIGroupBox grpDataSettings;
        private Sunny.UI.UILabel lblDataFormat;
        private Sunny.UI.UIComboBox cmbDataFormat;
        private Sunny.UI.UILabel lblEncoding;
        private Sunny.UI.UIComboBox cmbEncoding;
        private Sunny.UI.UILabel lblSendContent;
        private Sunny.UI.UITextBox txtSendContent;
        private Sunny.UI.UISymbolButton btnInsertVariable;
        private Sunny.UI.UICheckBox chkAppendNewLine;
        private Sunny.UI.UILabel lblNewLineType;
        private Sunny.UI.UIComboBox cmbNewLineType;

        // 响应设置组
        private Sunny.UI.UIGroupBox grpResponseSettings;
        private Sunny.UI.UICheckBox chkWaitResponse;
        private Sunny.UI.UILabel lblResponseTimeout;
        private Sunny.UI.UIIntegerUpDown numResponseTimeout;
        private Sunny.UI.UILabel lblResponseVariable;
        private Sunny.UI.UIComboBox cmbResponseVariable;
        private Sunny.UI.UISymbolButton btnCreateVariable;
        private Sunny.UI.UICheckBox chkDisconnectAfterSend;
        private Sunny.UI.UILabel lblCondition;
        private Sunny.UI.UITextBox txtCondition;

        // 底部按钮
        private Sunny.UI.UIButton btnTestSend;
        private Sunny.UI.UIButton btnHelp;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSave;
    }
}
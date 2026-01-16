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
            panelMain = new Panel();
            grpOtherSettings = new UIGroupBox();
            chkEnabled = new UICheckBox();
            txtDescription = new UITextBox();
            lblDescription = new UILabel();
            btnConditionHelper = new UISymbolButton();
            txtCondition = new UITextBox();
            lblCondition = new UILabel();
            chkDisconnectAfterSend = new UICheckBox();
            grpResponseSettings = new UIGroupBox();
            btnCreateVariable = new UISymbolButton();
            cmbResponseVariable = new UIComboBox();
            lblResponseVariable = new UILabel();
            numResponseTimeout = new UIIntegerUpDown();
            lblResponseTimeout = new UILabel();
            chkWaitResponse = new UICheckBox();
            grpDataSettings = new UIGroupBox();
            cmbNewLineType = new UIComboBox();
            lblNewLineType = new UILabel();
            chkAppendNewLine = new UICheckBox();
            btnInsertVariable = new UISymbolButton();
            txtSendContent = new UITextBox();
            lblSendContent = new UILabel();
            cmbEncoding = new UIComboBox();
            lblEncoding = new UILabel();
            cmbDataFormat = new UIComboBox();
            lblDataFormat = new UILabel();
            grpConnection = new UIGroupBox();
            lblConnectionStatus = new UILabel();
            btnTestConnection = new UISymbolButton();
            numReceiveTimeout = new UIIntegerUpDown();
            lblReceiveTimeout = new UILabel();
            numSendTimeout = new UIIntegerUpDown();
            lblSendTimeout = new UILabel();
            numConnectTimeout = new UIIntegerUpDown();
            lblConnectTimeout = new UILabel();
            cmbProtocol = new UIComboBox();
            lblProtocol = new UILabel();
            txtPort = new UITextBox();
            lblPort = new UILabel();
            txtIPAddress = new UITextBox();
            lblIPAddress = new UILabel();
            panelBottom = new Panel();
            btnTestSend = new UISymbolButton();
            btnOK = new UISymbolButton();
            btnCancel = new UISymbolButton();
            panelMain.SuspendLayout();
            grpOtherSettings.SuspendLayout();
            grpResponseSettings.SuspendLayout();
            grpDataSettings.SuspendLayout();
            grpConnection.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.Controls.Add(grpOtherSettings);
            panelMain.Controls.Add(grpResponseSettings);
            panelMain.Controls.Add(grpDataSettings);
            panelMain.Controls.Add(grpConnection);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15);
            panelMain.Size = new Size(700, 555);
            panelMain.TabIndex = 0;
            // 
            // grpOtherSettings
            // 
            grpOtherSettings.Controls.Add(chkEnabled);
            grpOtherSettings.Controls.Add(txtDescription);
            grpOtherSettings.Controls.Add(lblDescription);
            grpOtherSettings.Controls.Add(btnConditionHelper);
            grpOtherSettings.Controls.Add(txtCondition);
            grpOtherSettings.Controls.Add(lblCondition);
            grpOtherSettings.Controls.Add(chkDisconnectAfterSend);
            grpOtherSettings.Dock = DockStyle.Top;
            grpOtherSettings.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpOtherSettings.Location = new Point(15, 415);
            grpOtherSettings.Margin = new Padding(4, 5, 4, 5);
            grpOtherSettings.MinimumSize = new Size(1, 1);
            grpOtherSettings.Name = "grpOtherSettings";
            grpOtherSettings.Padding = new Padding(0, 32, 0, 0);
            grpOtherSettings.Size = new Size(670, 130);
            grpOtherSettings.TabIndex = 3;
            grpOtherSettings.Text = "其他设置";
            grpOtherSettings.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // chkEnabled
            // 
            chkEnabled.Checked = true;
            chkEnabled.Cursor = Cursors.Hand;
            chkEnabled.Font = new Font("微软雅黑", 9F);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(555, 88);
            chkEnabled.MinimumSize = new Size(1, 1);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(100, 24);
            chkEnabled.TabIndex = 6;
            chkEnabled.Text = "启用此步骤";
            // 
            // txtDescription
            // 
            txtDescription.Cursor = Cursors.IBeam;
            txtDescription.Font = new Font("微软雅黑", 9F);
            txtDescription.Location = new Point(70, 85);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(475, 29);
            txtDescription.TabIndex = 5;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "步骤描述信息";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("微软雅黑", 9F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(20, 88);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(35, 17);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "描述:";
            // 
            // btnConditionHelper
            // 
            btnConditionHelper.Cursor = Cursors.Hand;
            btnConditionHelper.Font = new Font("微软雅黑", 9F);
            btnConditionHelper.Location = new Point(555, 45);
            btnConditionHelper.MinimumSize = new Size(1, 1);
            btnConditionHelper.Name = "btnConditionHelper";
            btnConditionHelper.Size = new Size(100, 29);
            btnConditionHelper.Symbol = 61736;
            btnConditionHelper.TabIndex = 3;
            btnConditionHelper.Text = "条件助手";
            btnConditionHelper.TipsFont = new Font("微软雅黑", 9F);
            // 
            // txtCondition
            // 
            txtCondition.Cursor = Cursors.IBeam;
            txtCondition.Font = new Font("微软雅黑", 9F);
            txtCondition.Location = new Point(235, 45);
            txtCondition.Margin = new Padding(4, 5, 4, 5);
            txtCondition.MinimumSize = new Size(1, 16);
            txtCondition.Name = "txtCondition";
            txtCondition.Padding = new Padding(5);
            txtCondition.ShowText = false;
            txtCondition.Size = new Size(310, 29);
            txtCondition.TabIndex = 2;
            txtCondition.TextAlignment = ContentAlignment.MiddleLeft;
            txtCondition.Watermark = "留空表示始终执行";
            // 
            // lblCondition
            // 
            lblCondition.AutoSize = true;
            lblCondition.Font = new Font("微软雅黑", 9F);
            lblCondition.ForeColor = Color.FromArgb(48, 48, 48);
            lblCondition.Location = new Point(160, 48);
            lblCondition.Name = "lblCondition";
            lblCondition.Size = new Size(59, 17);
            lblCondition.TabIndex = 1;
            lblCondition.Text = "执行条件:";
            // 
            // chkDisconnectAfterSend
            // 
            chkDisconnectAfterSend.Cursor = Cursors.Hand;
            chkDisconnectAfterSend.Font = new Font("微软雅黑", 9F);
            chkDisconnectAfterSend.ForeColor = Color.FromArgb(48, 48, 48);
            chkDisconnectAfterSend.Location = new Point(20, 45);
            chkDisconnectAfterSend.MinimumSize = new Size(1, 1);
            chkDisconnectAfterSend.Name = "chkDisconnectAfterSend";
            chkDisconnectAfterSend.Size = new Size(120, 24);
            chkDisconnectAfterSend.TabIndex = 0;
            chkDisconnectAfterSend.Text = "发送后断开连接";
            // 
            // grpResponseSettings
            // 
            grpResponseSettings.Controls.Add(btnCreateVariable);
            grpResponseSettings.Controls.Add(cmbResponseVariable);
            grpResponseSettings.Controls.Add(lblResponseVariable);
            grpResponseSettings.Controls.Add(numResponseTimeout);
            grpResponseSettings.Controls.Add(lblResponseTimeout);
            grpResponseSettings.Controls.Add(chkWaitResponse);
            grpResponseSettings.Dock = DockStyle.Top;
            grpResponseSettings.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpResponseSettings.Location = new Point(15, 330);
            grpResponseSettings.Margin = new Padding(4, 5, 4, 5);
            grpResponseSettings.MinimumSize = new Size(1, 1);
            grpResponseSettings.Name = "grpResponseSettings";
            grpResponseSettings.Padding = new Padding(0, 32, 0, 0);
            grpResponseSettings.Size = new Size(670, 85);
            grpResponseSettings.TabIndex = 2;
            grpResponseSettings.Text = "响应设置";
            grpResponseSettings.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnCreateVariable
            // 
            btnCreateVariable.Cursor = Cursors.Hand;
            btnCreateVariable.Font = new Font("微软雅黑", 9F);
            btnCreateVariable.Location = new Point(555, 45);
            btnCreateVariable.MinimumSize = new Size(1, 1);
            btnCreateVariable.Name = "btnCreateVariable";
            btnCreateVariable.Size = new Size(100, 29);
            btnCreateVariable.Symbol = 61543;
            btnCreateVariable.TabIndex = 5;
            btnCreateVariable.Text = "新建变量";
            btnCreateVariable.TipsFont = new Font("微软雅黑", 9F);
            // 
            // cmbResponseVariable
            // 
            cmbResponseVariable.DataSource = null;
            cmbResponseVariable.FillColor = Color.White;
            cmbResponseVariable.Font = new Font("微软雅黑", 9F);
            cmbResponseVariable.ItemHoverColor = Color.FromArgb(65, 100, 204);
            cmbResponseVariable.ItemSelectForeColor = Color.FromArgb(65, 100, 204);
            cmbResponseVariable.Location = new Point(375, 45);
            cmbResponseVariable.Margin = new Padding(4, 5, 4, 5);
            cmbResponseVariable.MinimumSize = new Size(63, 0);
            cmbResponseVariable.Name = "cmbResponseVariable";
            cmbResponseVariable.Padding = new Padding(0, 0, 30, 2);
            cmbResponseVariable.Size = new Size(170, 29);
            cmbResponseVariable.SymbolSize = 24;
            cmbResponseVariable.TabIndex = 4;
            cmbResponseVariable.TextAlignment = ContentAlignment.MiddleLeft;
            cmbResponseVariable.Watermark = "选择或输入变量名";
            // 
            // lblResponseVariable
            // 
            lblResponseVariable.AutoSize = true;
            lblResponseVariable.Font = new Font("微软雅黑", 9F);
            lblResponseVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblResponseVariable.Location = new Point(290, 48);
            lblResponseVariable.Name = "lblResponseVariable";
            lblResponseVariable.Size = new Size(71, 17);
            lblResponseVariable.TabIndex = 3;
            lblResponseVariable.Text = "保存到变量:";
            // 
            // numResponseTimeout
            // 
            numResponseTimeout.Font = new Font("微软雅黑", 9F);
            numResponseTimeout.Location = new Point(215, 45);
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
            numResponseTimeout.Text = "10";
            numResponseTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numResponseTimeout.Value = 10;
            // 
            // lblResponseTimeout
            // 
            lblResponseTimeout.AutoSize = true;
            lblResponseTimeout.Font = new Font("微软雅黑", 9F);
            lblResponseTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblResponseTimeout.Location = new Point(130, 48);
            lblResponseTimeout.Name = "lblResponseTimeout";
            lblResponseTimeout.Size = new Size(73, 17);
            lblResponseTimeout.TabIndex = 1;
            lblResponseTimeout.Text = "响应超时(s):";
            // 
            // chkWaitResponse
            // 
            chkWaitResponse.Cursor = Cursors.Hand;
            chkWaitResponse.Font = new Font("微软雅黑", 9F);
            chkWaitResponse.ForeColor = Color.FromArgb(48, 48, 48);
            chkWaitResponse.Location = new Point(20, 45);
            chkWaitResponse.MinimumSize = new Size(1, 1);
            chkWaitResponse.Name = "chkWaitResponse";
            chkWaitResponse.Size = new Size(100, 24);
            chkWaitResponse.TabIndex = 0;
            chkWaitResponse.Text = "等待响应";
            // 
            // grpDataSettings
            // 
            grpDataSettings.Controls.Add(cmbNewLineType);
            grpDataSettings.Controls.Add(lblNewLineType);
            grpDataSettings.Controls.Add(chkAppendNewLine);
            grpDataSettings.Controls.Add(btnInsertVariable);
            grpDataSettings.Controls.Add(txtSendContent);
            grpDataSettings.Controls.Add(lblSendContent);
            grpDataSettings.Controls.Add(cmbEncoding);
            grpDataSettings.Controls.Add(lblEncoding);
            grpDataSettings.Controls.Add(cmbDataFormat);
            grpDataSettings.Controls.Add(lblDataFormat);
            grpDataSettings.Dock = DockStyle.Top;
            grpDataSettings.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpDataSettings.Location = new Point(15, 155);
            grpDataSettings.Margin = new Padding(4, 5, 4, 5);
            grpDataSettings.MinimumSize = new Size(1, 1);
            grpDataSettings.Name = "grpDataSettings";
            grpDataSettings.Padding = new Padding(0, 32, 0, 0);
            grpDataSettings.Size = new Size(670, 175);
            grpDataSettings.TabIndex = 1;
            grpDataSettings.Text = "数据设置";
            grpDataSettings.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // cmbNewLineType
            // 
            cmbNewLineType.DataSource = null;
            cmbNewLineType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbNewLineType.FillColor = Color.White;
            cmbNewLineType.Font = new Font("微软雅黑", 9F);
            cmbNewLineType.ItemHoverColor = Color.FromArgb(65, 100, 204);
            cmbNewLineType.ItemSelectForeColor = Color.FromArgb(65, 100, 204);
            cmbNewLineType.Location = new Point(205, 142);
            cmbNewLineType.Margin = new Padding(4, 5, 4, 5);
            cmbNewLineType.MinimumSize = new Size(63, 0);
            cmbNewLineType.Name = "cmbNewLineType";
            cmbNewLineType.Padding = new Padding(0, 0, 30, 2);
            cmbNewLineType.Size = new Size(100, 29);
            cmbNewLineType.SymbolSize = 24;
            cmbNewLineType.TabIndex = 9;
            cmbNewLineType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbNewLineType.Watermark = "";
            // 
            // lblNewLineType
            // 
            lblNewLineType.AutoSize = true;
            lblNewLineType.Font = new Font("微软雅黑", 9F);
            lblNewLineType.ForeColor = Color.FromArgb(48, 48, 48);
            lblNewLineType.Location = new Point(130, 145);
            lblNewLineType.Name = "lblNewLineType";
            lblNewLineType.Size = new Size(59, 17);
            lblNewLineType.TabIndex = 8;
            lblNewLineType.Text = "换行类型:";
            // 
            // chkAppendNewLine
            // 
            chkAppendNewLine.Cursor = Cursors.Hand;
            chkAppendNewLine.Font = new Font("微软雅黑", 9F);
            chkAppendNewLine.ForeColor = Color.FromArgb(48, 48, 48);
            chkAppendNewLine.Location = new Point(20, 142);
            chkAppendNewLine.MinimumSize = new Size(1, 1);
            chkAppendNewLine.Name = "chkAppendNewLine";
            chkAppendNewLine.Size = new Size(100, 24);
            chkAppendNewLine.TabIndex = 7;
            chkAppendNewLine.Text = "追加换行符";
            // 
            // btnInsertVariable
            // 
            btnInsertVariable.Cursor = Cursors.Hand;
            btnInsertVariable.Font = new Font("微软雅黑", 9F);
            btnInsertVariable.Location = new Point(555, 79);
            btnInsertVariable.MinimumSize = new Size(1, 1);
            btnInsertVariable.Name = "btnInsertVariable";
            btnInsertVariable.Size = new Size(100, 29);
            btnInsertVariable.Symbol = 61618;
            btnInsertVariable.TabIndex = 6;
            btnInsertVariable.Text = "插入变量";
            btnInsertVariable.TipsFont = new Font("微软雅黑", 9F);
            // 
            // txtSendContent
            // 
            txtSendContent.Cursor = Cursors.IBeam;
            txtSendContent.Font = new Font("微软雅黑", 9F);
            txtSendContent.Location = new Point(95, 79);
            txtSendContent.Margin = new Padding(4, 5, 4, 5);
            txtSendContent.MinimumSize = new Size(1, 16);
            txtSendContent.Multiline = true;
            txtSendContent.Name = "txtSendContent";
            txtSendContent.Padding = new Padding(5);
            txtSendContent.ShowText = false;
            txtSendContent.Size = new Size(450, 55);
            txtSendContent.TabIndex = 5;
            txtSendContent.TextAlignment = ContentAlignment.TopLeft;
            txtSendContent.Watermark = "输入发送内容，支持 {变量名} 格式引用变量";
            // 
            // lblSendContent
            // 
            lblSendContent.AutoSize = true;
            lblSendContent.Font = new Font("微软雅黑", 9F);
            lblSendContent.ForeColor = Color.FromArgb(48, 48, 48);
            lblSendContent.Location = new Point(20, 82);
            lblSendContent.Name = "lblSendContent";
            lblSendContent.Size = new Size(59, 17);
            lblSendContent.TabIndex = 4;
            lblSendContent.Text = "发送内容:";
            // 
            // cmbEncoding
            // 
            cmbEncoding.DataSource = null;
            cmbEncoding.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbEncoding.FillColor = Color.White;
            cmbEncoding.Font = new Font("微软雅黑", 9F);
            cmbEncoding.ItemHoverColor = Color.FromArgb(65, 100, 204);
            cmbEncoding.ItemSelectForeColor = Color.FromArgb(65, 100, 204);
            cmbEncoding.Location = new Point(285, 42);
            cmbEncoding.Margin = new Padding(4, 5, 4, 5);
            cmbEncoding.MinimumSize = new Size(63, 0);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Padding = new Padding(0, 0, 30, 2);
            cmbEncoding.Size = new Size(100, 29);
            cmbEncoding.SymbolSize = 24;
            cmbEncoding.TabIndex = 3;
            cmbEncoding.TextAlignment = ContentAlignment.MiddleLeft;
            cmbEncoding.Watermark = "";
            // 
            // lblEncoding
            // 
            lblEncoding.AutoSize = true;
            lblEncoding.Font = new Font("微软雅黑", 9F);
            lblEncoding.ForeColor = Color.FromArgb(48, 48, 48);
            lblEncoding.Location = new Point(210, 45);
            lblEncoding.Name = "lblEncoding";
            lblEncoding.Size = new Size(59, 17);
            lblEncoding.TabIndex = 2;
            lblEncoding.Text = "字符编码:";
            // 
            // cmbDataFormat
            // 
            cmbDataFormat.DataSource = null;
            cmbDataFormat.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbDataFormat.FillColor = Color.White;
            cmbDataFormat.Font = new Font("微软雅黑", 9F);
            cmbDataFormat.ItemHoverColor = Color.FromArgb(65, 100, 204);
            cmbDataFormat.ItemSelectForeColor = Color.FromArgb(65, 100, 204);
            cmbDataFormat.Location = new Point(95, 42);
            cmbDataFormat.Margin = new Padding(4, 5, 4, 5);
            cmbDataFormat.MinimumSize = new Size(63, 0);
            cmbDataFormat.Name = "cmbDataFormat";
            cmbDataFormat.Padding = new Padding(0, 0, 30, 2);
            cmbDataFormat.Size = new Size(100, 29);
            cmbDataFormat.SymbolSize = 24;
            cmbDataFormat.TabIndex = 1;
            cmbDataFormat.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDataFormat.Watermark = "";
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
            // grpConnection
            // 
            grpConnection.Controls.Add(lblConnectionStatus);
            grpConnection.Controls.Add(btnTestConnection);
            grpConnection.Controls.Add(numReceiveTimeout);
            grpConnection.Controls.Add(lblReceiveTimeout);
            grpConnection.Controls.Add(numSendTimeout);
            grpConnection.Controls.Add(lblSendTimeout);
            grpConnection.Controls.Add(numConnectTimeout);
            grpConnection.Controls.Add(lblConnectTimeout);
            grpConnection.Controls.Add(cmbProtocol);
            grpConnection.Controls.Add(lblProtocol);
            grpConnection.Controls.Add(txtPort);
            grpConnection.Controls.Add(lblPort);
            grpConnection.Controls.Add(txtIPAddress);
            grpConnection.Controls.Add(lblIPAddress);
            grpConnection.Dock = DockStyle.Top;
            grpConnection.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpConnection.Location = new Point(15, 15);
            grpConnection.Margin = new Padding(4, 5, 4, 5);
            grpConnection.MinimumSize = new Size(1, 1);
            grpConnection.Name = "grpConnection";
            grpConnection.Padding = new Padding(0, 32, 0, 0);
            grpConnection.Size = new Size(670, 140);
            grpConnection.TabIndex = 0;
            grpConnection.Text = "连接设置";
            grpConnection.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.Font = new Font("微软雅黑", 9F);
            lblConnectionStatus.ForeColor = Color.Gray;
            lblConnectionStatus.Location = new Point(512, 85);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new Size(44, 17);
            lblConnectionStatus.TabIndex = 13;
            lblConnectionStatus.Text = "未测试";
            // 
            // btnTestConnection
            // 
            btnTestConnection.Cursor = Cursors.Hand;
            btnTestConnection.Font = new Font("微软雅黑", 9F);
            btnTestConnection.Location = new Point(402, 82);
            btnTestConnection.MinimumSize = new Size(1, 1);
            btnTestConnection.Name = "btnTestConnection";
            btnTestConnection.Size = new Size(100, 29);
            btnTestConnection.Symbol = 61728;
            btnTestConnection.TabIndex = 12;
            btnTestConnection.Text = "测试连接";
            btnTestConnection.TipsFont = new Font("微软雅黑", 9F);
            // 
            // numReceiveTimeout
            // 
            numReceiveTimeout.Font = new Font("微软雅黑", 9F);
            numReceiveTimeout.Location = new Point(317, 82);
            numReceiveTimeout.Margin = new Padding(4, 5, 4, 5);
            numReceiveTimeout.Maximum = 300D;
            numReceiveTimeout.Minimum = 1D;
            numReceiveTimeout.MinimumSize = new Size(100, 0);
            numReceiveTimeout.Name = "numReceiveTimeout";
            numReceiveTimeout.Padding = new Padding(5);
            numReceiveTimeout.RectColor = Color.FromArgb(65, 100, 204);
            numReceiveTimeout.ShowText = false;
            numReceiveTimeout.Size = new Size(100, 29);
            numReceiveTimeout.TabIndex = 11;
            numReceiveTimeout.Text = "5";
            numReceiveTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numReceiveTimeout.Value = 5;
            // 
            // lblReceiveTimeout
            // 
            lblReceiveTimeout.AutoSize = true;
            lblReceiveTimeout.Font = new Font("微软雅黑", 9F);
            lblReceiveTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblReceiveTimeout.Location = new Point(232, 85);
            lblReceiveTimeout.Name = "lblReceiveTimeout";
            lblReceiveTimeout.Size = new Size(73, 17);
            lblReceiveTimeout.TabIndex = 10;
            lblReceiveTimeout.Text = "接收超时(s):";
            // 
            // numSendTimeout
            // 
            numSendTimeout.Font = new Font("微软雅黑", 9F);
            numSendTimeout.Location = new Point(105, 82);
            numSendTimeout.Margin = new Padding(4, 5, 4, 5);
            numSendTimeout.Maximum = 300D;
            numSendTimeout.Minimum = 1D;
            numSendTimeout.MinimumSize = new Size(100, 0);
            numSendTimeout.Name = "numSendTimeout";
            numSendTimeout.Padding = new Padding(5);
            numSendTimeout.RectColor = Color.FromArgb(65, 100, 204);
            numSendTimeout.ShowText = false;
            numSendTimeout.Size = new Size(100, 29);
            numSendTimeout.TabIndex = 9;
            numSendTimeout.Text = "5";
            numSendTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numSendTimeout.Value = 5;
            // 
            // lblSendTimeout
            // 
            lblSendTimeout.AutoSize = true;
            lblSendTimeout.Font = new Font("微软雅黑", 9F);
            lblSendTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblSendTimeout.Location = new Point(20, 85);
            lblSendTimeout.Name = "lblSendTimeout";
            lblSendTimeout.Size = new Size(73, 17);
            lblSendTimeout.TabIndex = 8;
            lblSendTimeout.Text = "发送超时(s):";
            // 
            // numConnectTimeout
            // 
            numConnectTimeout.Font = new Font("微软雅黑", 9F);
            numConnectTimeout.Location = new Point(595, 42);
            numConnectTimeout.Margin = new Padding(4, 5, 4, 5);
            numConnectTimeout.Maximum = 300D;
            numConnectTimeout.Minimum = 1D;
            numConnectTimeout.MinimumSize = new Size(100, 0);
            numConnectTimeout.Name = "numConnectTimeout";
            numConnectTimeout.Padding = new Padding(5);
            numConnectTimeout.RectColor = Color.FromArgb(65, 100, 204);
            numConnectTimeout.ShowText = false;
            numConnectTimeout.Size = new Size(100, 29);
            numConnectTimeout.TabIndex = 7;
            numConnectTimeout.Text = "5";
            numConnectTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numConnectTimeout.Value = 5;
            // 
            // lblConnectTimeout
            // 
            lblConnectTimeout.AutoSize = true;
            lblConnectTimeout.Font = new Font("微软雅黑", 9F);
            lblConnectTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblConnectTimeout.Location = new Point(510, 45);
            lblConnectTimeout.Name = "lblConnectTimeout";
            lblConnectTimeout.Size = new Size(73, 17);
            lblConnectTimeout.TabIndex = 6;
            lblConnectTimeout.Text = "连接超时(s):";
            // 
            // cmbProtocol
            // 
            cmbProtocol.DataSource = null;
            cmbProtocol.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbProtocol.FillColor = Color.White;
            cmbProtocol.Font = new Font("微软雅黑", 9F);
            cmbProtocol.ItemHoverColor = Color.FromArgb(65, 100, 204);
            cmbProtocol.ItemSelectForeColor = Color.FromArgb(65, 100, 204);
            cmbProtocol.Location = new Point(420, 42);
            cmbProtocol.Margin = new Padding(4, 5, 4, 5);
            cmbProtocol.MinimumSize = new Size(63, 0);
            cmbProtocol.Name = "cmbProtocol";
            cmbProtocol.Padding = new Padding(0, 0, 30, 2);
            cmbProtocol.Size = new Size(80, 29);
            cmbProtocol.SymbolSize = 24;
            cmbProtocol.TabIndex = 5;
            cmbProtocol.TextAlignment = ContentAlignment.MiddleLeft;
            cmbProtocol.Watermark = "";
            // 
            // lblProtocol
            // 
            lblProtocol.AutoSize = true;
            lblProtocol.Font = new Font("微软雅黑", 9F);
            lblProtocol.ForeColor = Color.FromArgb(48, 48, 48);
            lblProtocol.Location = new Point(370, 45);
            lblProtocol.Name = "lblProtocol";
            lblProtocol.Size = new Size(35, 17);
            lblProtocol.TabIndex = 4;
            lblProtocol.Text = "协议:";
            // 
            // txtPort
            // 
            txtPort.Cursor = Cursors.IBeam;
            txtPort.Font = new Font("微软雅黑", 9F);
            txtPort.Location = new Point(280, 42);
            txtPort.Margin = new Padding(4, 5, 4, 5);
            txtPort.MinimumSize = new Size(1, 16);
            txtPort.Name = "txtPort";
            txtPort.Padding = new Padding(5);
            txtPort.ShowText = false;
            txtPort.Size = new Size(80, 29);
            txtPort.TabIndex = 3;
            txtPort.TextAlignment = ContentAlignment.MiddleLeft;
            txtPort.Watermark = "8080";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("微软雅黑", 9F);
            lblPort.ForeColor = Color.FromArgb(48, 48, 48);
            lblPort.Location = new Point(230, 45);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(35, 17);
            lblPort.TabIndex = 2;
            lblPort.Text = "端口:";
            // 
            // txtIPAddress
            // 
            txtIPAddress.Cursor = Cursors.IBeam;
            txtIPAddress.Font = new Font("微软雅黑", 9F);
            txtIPAddress.Location = new Point(80, 42);
            txtIPAddress.Margin = new Padding(4, 5, 4, 5);
            txtIPAddress.MinimumSize = new Size(1, 16);
            txtIPAddress.Name = "txtIPAddress";
            txtIPAddress.Padding = new Padding(5);
            txtIPAddress.ShowText = false;
            txtIPAddress.Size = new Size(140, 29);
            txtIPAddress.TabIndex = 1;
            txtIPAddress.TextAlignment = ContentAlignment.MiddleLeft;
            txtIPAddress.Watermark = "192.168.1.100";
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
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(248, 249, 250);
            panelBottom.Controls.Add(btnTestSend);
            panelBottom.Controls.Add(btnOK);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 590);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(700, 60);
            panelBottom.TabIndex = 1;
            // 
            // btnTestSend
            // 
            btnTestSend.Cursor = Cursors.Hand;
            btnTestSend.FillColor = Color.FromArgb(0, 150, 136);
            btnTestSend.FillHoverColor = Color.FromArgb(0, 170, 156);
            btnTestSend.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnTestSend.Location = new Point(15, 15);
            btnTestSend.MinimumSize = new Size(1, 1);
            btnTestSend.Name = "btnTestSend";
            btnTestSend.Size = new Size(110, 35);
            btnTestSend.Symbol = 61544;
            btnTestSend.TabIndex = 0;
            btnTestSend.Text = "测试发送";
            btnTestSend.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.FillColor = Color.FromArgb(65, 100, 204);
            btnOK.FillHoverColor = Color.FromArgb(85, 120, 224);
            btnOK.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnOK.Location = new Point(465, 15);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(110, 35);
            btnOK.Symbol = 61694;
            btnOK.TabIndex = 1;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(110, 110, 110);
            btnCancel.FillHoverColor = Color.FromArgb(130, 130, 130);
            btnCancel.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnCancel.Location = new Point(585, 15);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.Symbol = 61527;
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            // 
            // Form_EthernetSend
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(700, 650);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_EthernetSend";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "以太网发送配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 700, 650);
            panelMain.ResumeLayout(false);
            grpOtherSettings.ResumeLayout(false);
            grpOtherSettings.PerformLayout();
            grpResponseSettings.ResumeLayout(false);
            grpResponseSettings.PerformLayout();
            grpDataSettings.ResumeLayout(false);
            grpDataSettings.PerformLayout();
            grpConnection.ResumeLayout(false);
            grpConnection.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // 主面板
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelBottom;

        // 连接设置组
        private Sunny.UI.UIGroupBox grpConnection;
        private Sunny.UI.UILabel lblIPAddress;
        private Sunny.UI.UITextBox txtIPAddress;
        private Sunny.UI.UILabel lblPort;
        private Sunny.UI.UITextBox txtPort;
        private Sunny.UI.UILabel lblProtocol;
        private Sunny.UI.UIComboBox cmbProtocol;
        private Sunny.UI.UILabel lblConnectTimeout;
        private Sunny.UI.UIIntegerUpDown numConnectTimeout;
        private Sunny.UI.UILabel lblSendTimeout;
        private Sunny.UI.UIIntegerUpDown numSendTimeout;
        private Sunny.UI.UILabel lblReceiveTimeout;
        private Sunny.UI.UIIntegerUpDown numReceiveTimeout;
        private Sunny.UI.UISymbolButton btnTestConnection;
        private Sunny.UI.UILabel lblConnectionStatus;

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

        // 其他设置组
        private Sunny.UI.UIGroupBox grpOtherSettings;
        private Sunny.UI.UICheckBox chkDisconnectAfterSend;
        private Sunny.UI.UILabel lblCondition;
        private Sunny.UI.UITextBox txtCondition;
        private Sunny.UI.UISymbolButton btnConditionHelper;
        private Sunny.UI.UILabel lblDescription;
        private Sunny.UI.UITextBox txtDescription;
        private Sunny.UI.UICheckBox chkEnabled;

        // 底部按钮
        private Sunny.UI.UISymbolButton btnTestSend;
        private Sunny.UI.UISymbolButton btnOK;
        private Sunny.UI.UISymbolButton btnCancel;
    }
}
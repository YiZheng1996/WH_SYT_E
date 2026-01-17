namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_SerialPortSend
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
            grpSerialPort = new UIGroupBox();
            lblPortName = new UILabel();
            cmbPortName = new UIComboBox();
            btnRefreshPorts = new UISymbolButton();
            lblBaudRate = new UILabel();
            cmbBaudRate = new UIComboBox();
            lblDataBits = new UILabel();
            cmbDataBits = new UIComboBox();
            lblParity = new UILabel();
            cmbParity = new UIComboBox();
            lblStopBits = new UILabel();
            cmbStopBits = new UIComboBox();
            lblHandshake = new UILabel();
            cmbHandshake = new UIComboBox();
            btnTestPort = new UISymbolButton();
            lblPortStatus = new UILabel();
            grpTimeout = new UIGroupBox();
            lblReadTimeout = new UILabel();
            numReadTimeout = new UIIntegerUpDown();
            lblWriteTimeout = new UILabel();
            numWriteTimeout = new UIIntegerUpDown();
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
            chkCloseAfterSend = new UICheckBox();
            lblCondition = new UILabel();
            txtCondition = new UITextBox();
            panelBottom = new Panel();
            btnTestSend = new UIButton();
            btnHelp = new UIButton();
            btnCancel = new UIButton();
            btnSave = new UIButton();
            panelDescription.SuspendLayout();
            panelMain.SuspendLayout();
            grpSerialPort.SuspendLayout();
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
            panelMain.Controls.Add(grpSerialPort);
            panelMain.Controls.Add(grpTimeout);
            panelMain.Controls.Add(grpDataSettings);
            panelMain.Controls.Add(grpResponseSettings);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 105);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15);
            panelMain.Size = new Size(800, 637);
            panelMain.TabIndex = 1;
            // 
            // grpSerialPort
            // 
            grpSerialPort.Controls.Add(lblPortName);
            grpSerialPort.Controls.Add(cmbPortName);
            grpSerialPort.Controls.Add(btnRefreshPorts);
            grpSerialPort.Controls.Add(lblBaudRate);
            grpSerialPort.Controls.Add(cmbBaudRate);
            grpSerialPort.Controls.Add(lblDataBits);
            grpSerialPort.Controls.Add(cmbDataBits);
            grpSerialPort.Controls.Add(lblParity);
            grpSerialPort.Controls.Add(cmbParity);
            grpSerialPort.Controls.Add(lblStopBits);
            grpSerialPort.Controls.Add(cmbStopBits);
            grpSerialPort.Controls.Add(lblHandshake);
            grpSerialPort.Controls.Add(cmbHandshake);
            grpSerialPort.Controls.Add(btnTestPort);
            grpSerialPort.Controls.Add(lblPortStatus);
            grpSerialPort.Dock = DockStyle.Top;
            grpSerialPort.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpSerialPort.ForeColor = Color.FromArgb(65, 100, 204);
            grpSerialPort.Location = new Point(15, 446);
            grpSerialPort.Margin = new Padding(4, 5, 4, 5);
            grpSerialPort.MinimumSize = new Size(1, 1);
            grpSerialPort.Name = "grpSerialPort";
            grpSerialPort.Padding = new Padding(0, 32, 0, 0);
            grpSerialPort.RectColor = Color.FromArgb(65, 100, 204);
            grpSerialPort.Size = new Size(770, 180);
            grpSerialPort.TabIndex = 0;
            grpSerialPort.Text = "串口设置";
            grpSerialPort.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblPortName
            // 
            lblPortName.AutoSize = true;
            lblPortName.Font = new Font("微软雅黑", 9F);
            lblPortName.ForeColor = Color.FromArgb(48, 48, 48);
            lblPortName.Location = new Point(20, 45);
            lblPortName.Name = "lblPortName";
            lblPortName.Size = new Size(47, 17);
            lblPortName.TabIndex = 0;
            lblPortName.Text = "串口号:";
            // 
            // cmbPortName
            // 
            cmbPortName.DataSource = null;
            cmbPortName.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbPortName.FillColor = Color.White;
            cmbPortName.Font = new Font("微软雅黑", 9F);
            cmbPortName.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbPortName.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbPortName.Location = new Point(80, 42);
            cmbPortName.Margin = new Padding(4, 5, 4, 5);
            cmbPortName.MinimumSize = new Size(63, 0);
            cmbPortName.Name = "cmbPortName";
            cmbPortName.Padding = new Padding(0, 0, 30, 2);
            cmbPortName.RectColor = Color.FromArgb(65, 100, 204);
            cmbPortName.Size = new Size(100, 29);
            cmbPortName.SymbolSize = 24;
            cmbPortName.TabIndex = 1;
            cmbPortName.TextAlignment = ContentAlignment.MiddleLeft;
            cmbPortName.Watermark = "";
            // 
            // btnRefreshPorts
            // 
            btnRefreshPorts.Cursor = Cursors.Hand;
            btnRefreshPorts.FillColor = Color.FromArgb(65, 100, 204);
            btnRefreshPorts.Font = new Font("微软雅黑", 9F);
            btnRefreshPorts.Location = new Point(190, 42);
            btnRefreshPorts.MinimumSize = new Size(1, 1);
            btnRefreshPorts.Name = "btnRefreshPorts";
            btnRefreshPorts.Size = new Size(35, 29);
            btnRefreshPorts.Symbol = 61473;
            btnRefreshPorts.TabIndex = 2;
            btnRefreshPorts.TipsFont = new Font("微软雅黑", 9F);
            // 
            // lblBaudRate
            // 
            lblBaudRate.AutoSize = true;
            lblBaudRate.Font = new Font("微软雅黑", 9F);
            lblBaudRate.ForeColor = Color.FromArgb(48, 48, 48);
            lblBaudRate.Location = new Point(250, 45);
            lblBaudRate.Name = "lblBaudRate";
            lblBaudRate.Size = new Size(47, 17);
            lblBaudRate.TabIndex = 3;
            lblBaudRate.Text = "波特率:";
            // 
            // cmbBaudRate
            // 
            cmbBaudRate.DataSource = null;
            cmbBaudRate.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbBaudRate.FillColor = Color.White;
            cmbBaudRate.Font = new Font("微软雅黑", 9F);
            cmbBaudRate.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbBaudRate.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbBaudRate.Location = new Point(310, 42);
            cmbBaudRate.Margin = new Padding(4, 5, 4, 5);
            cmbBaudRate.MinimumSize = new Size(63, 0);
            cmbBaudRate.Name = "cmbBaudRate";
            cmbBaudRate.Padding = new Padding(0, 0, 30, 2);
            cmbBaudRate.RectColor = Color.FromArgb(65, 100, 204);
            cmbBaudRate.Size = new Size(100, 29);
            cmbBaudRate.SymbolSize = 24;
            cmbBaudRate.TabIndex = 4;
            cmbBaudRate.TextAlignment = ContentAlignment.MiddleLeft;
            cmbBaudRate.Watermark = "";
            // 
            // lblDataBits
            // 
            lblDataBits.AutoSize = true;
            lblDataBits.Font = new Font("微软雅黑", 9F);
            lblDataBits.ForeColor = Color.FromArgb(48, 48, 48);
            lblDataBits.Location = new Point(430, 45);
            lblDataBits.Name = "lblDataBits";
            lblDataBits.Size = new Size(47, 17);
            lblDataBits.TabIndex = 5;
            lblDataBits.Text = "数据位:";
            // 
            // cmbDataBits
            // 
            cmbDataBits.DataSource = null;
            cmbDataBits.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbDataBits.FillColor = Color.White;
            cmbDataBits.Font = new Font("微软雅黑", 9F);
            cmbDataBits.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDataBits.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDataBits.Location = new Point(490, 42);
            cmbDataBits.Margin = new Padding(4, 5, 4, 5);
            cmbDataBits.MinimumSize = new Size(63, 0);
            cmbDataBits.Name = "cmbDataBits";
            cmbDataBits.Padding = new Padding(0, 0, 30, 2);
            cmbDataBits.RectColor = Color.FromArgb(65, 100, 204);
            cmbDataBits.Size = new Size(80, 29);
            cmbDataBits.SymbolSize = 24;
            cmbDataBits.TabIndex = 6;
            cmbDataBits.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDataBits.Watermark = "";
            // 
            // lblParity
            // 
            lblParity.AutoSize = true;
            lblParity.Font = new Font("微软雅黑", 9F);
            lblParity.ForeColor = Color.FromArgb(48, 48, 48);
            lblParity.Location = new Point(20, 85);
            lblParity.Name = "lblParity";
            lblParity.Size = new Size(47, 17);
            lblParity.TabIndex = 7;
            lblParity.Text = "校验位:";
            // 
            // cmbParity
            // 
            cmbParity.DataSource = null;
            cmbParity.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbParity.FillColor = Color.White;
            cmbParity.Font = new Font("微软雅黑", 9F);
            cmbParity.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbParity.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbParity.Location = new Point(80, 82);
            cmbParity.Margin = new Padding(4, 5, 4, 5);
            cmbParity.MinimumSize = new Size(63, 0);
            cmbParity.Name = "cmbParity";
            cmbParity.Padding = new Padding(0, 0, 30, 2);
            cmbParity.RectColor = Color.FromArgb(65, 100, 204);
            cmbParity.Size = new Size(100, 29);
            cmbParity.SymbolSize = 24;
            cmbParity.TabIndex = 8;
            cmbParity.TextAlignment = ContentAlignment.MiddleLeft;
            cmbParity.Watermark = "";
            // 
            // lblStopBits
            // 
            lblStopBits.AutoSize = true;
            lblStopBits.Font = new Font("微软雅黑", 9F);
            lblStopBits.ForeColor = Color.FromArgb(48, 48, 48);
            lblStopBits.Location = new Point(250, 85);
            lblStopBits.Name = "lblStopBits";
            lblStopBits.Size = new Size(47, 17);
            lblStopBits.TabIndex = 9;
            lblStopBits.Text = "停止位:";
            // 
            // cmbStopBits
            // 
            cmbStopBits.DataSource = null;
            cmbStopBits.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbStopBits.FillColor = Color.White;
            cmbStopBits.Font = new Font("微软雅黑", 9F);
            cmbStopBits.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbStopBits.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbStopBits.Location = new Point(310, 82);
            cmbStopBits.Margin = new Padding(4, 5, 4, 5);
            cmbStopBits.MinimumSize = new Size(63, 0);
            cmbStopBits.Name = "cmbStopBits";
            cmbStopBits.Padding = new Padding(0, 0, 30, 2);
            cmbStopBits.RectColor = Color.FromArgb(65, 100, 204);
            cmbStopBits.Size = new Size(140, 29);
            cmbStopBits.SymbolSize = 24;
            cmbStopBits.TabIndex = 10;
            cmbStopBits.TextAlignment = ContentAlignment.MiddleLeft;
            cmbStopBits.Watermark = "";
            // 
            // lblHandshake
            // 
            lblHandshake.AutoSize = true;
            lblHandshake.Font = new Font("微软雅黑", 9F);
            lblHandshake.ForeColor = Color.FromArgb(48, 48, 48);
            lblHandshake.Location = new Point(470, 85);
            lblHandshake.Name = "lblHandshake";
            lblHandshake.Size = new Size(47, 17);
            lblHandshake.TabIndex = 11;
            lblHandshake.Text = "流控制:";
            // 
            // cmbHandshake
            // 
            cmbHandshake.DataSource = null;
            cmbHandshake.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbHandshake.FillColor = Color.White;
            cmbHandshake.Font = new Font("微软雅黑", 9F);
            cmbHandshake.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbHandshake.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbHandshake.Location = new Point(530, 82);
            cmbHandshake.Margin = new Padding(4, 5, 4, 5);
            cmbHandshake.MinimumSize = new Size(63, 0);
            cmbHandshake.Name = "cmbHandshake";
            cmbHandshake.Padding = new Padding(0, 0, 30, 2);
            cmbHandshake.RectColor = Color.FromArgb(65, 100, 204);
            cmbHandshake.Size = new Size(200, 29);
            cmbHandshake.SymbolSize = 24;
            cmbHandshake.TabIndex = 12;
            cmbHandshake.TextAlignment = ContentAlignment.MiddleLeft;
            cmbHandshake.Watermark = "";
            // 
            // btnTestPort
            // 
            btnTestPort.Cursor = Cursors.Hand;
            btnTestPort.FillColor = Color.FromArgb(40, 167, 69);
            btnTestPort.Font = new Font("微软雅黑", 9F);
            btnTestPort.Location = new Point(20, 130);
            btnTestPort.MinimumSize = new Size(1, 1);
            btnTestPort.Name = "btnTestPort";
            btnTestPort.Size = new Size(100, 32);
            btnTestPort.Symbol = 61714;
            btnTestPort.TabIndex = 13;
            btnTestPort.Text = "测试串口";
            btnTestPort.TipsFont = new Font("微软雅黑", 9F);
            // 
            // lblPortStatus
            // 
            lblPortStatus.AutoSize = true;
            lblPortStatus.Font = new Font("微软雅黑", 9F);
            lblPortStatus.ForeColor = Color.Gray;
            lblPortStatus.Location = new Point(130, 137);
            lblPortStatus.Name = "lblPortStatus";
            lblPortStatus.Size = new Size(44, 17);
            lblPortStatus.TabIndex = 14;
            lblPortStatus.Text = "未测试";
            // 
            // grpTimeout
            // 
            grpTimeout.Controls.Add(lblReadTimeout);
            grpTimeout.Controls.Add(numReadTimeout);
            grpTimeout.Controls.Add(lblWriteTimeout);
            grpTimeout.Controls.Add(numWriteTimeout);
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
            // lblReadTimeout
            // 
            lblReadTimeout.AutoSize = true;
            lblReadTimeout.Font = new Font("微软雅黑", 9F);
            lblReadTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblReadTimeout.Location = new Point(20, 45);
            lblReadTimeout.Name = "lblReadTimeout";
            lblReadTimeout.Size = new Size(79, 17);
            lblReadTimeout.TabIndex = 0;
            lblReadTimeout.Text = "读取超时(秒):";
            // 
            // numReadTimeout
            // 
            numReadTimeout.Font = new Font("微软雅黑", 9F);
            numReadTimeout.Location = new Point(115, 42);
            numReadTimeout.Margin = new Padding(4, 5, 4, 5);
            numReadTimeout.Maximum = 300D;
            numReadTimeout.Minimum = 1D;
            numReadTimeout.MinimumSize = new Size(100, 0);
            numReadTimeout.Name = "numReadTimeout";
            numReadTimeout.Padding = new Padding(5);
            numReadTimeout.RectColor = Color.FromArgb(65, 100, 204);
            numReadTimeout.ShowText = false;
            numReadTimeout.Size = new Size(100, 29);
            numReadTimeout.TabIndex = 1;
            numReadTimeout.Text = "3";
            numReadTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numReadTimeout.Value = 3;
            // 
            // lblWriteTimeout
            // 
            lblWriteTimeout.AutoSize = true;
            lblWriteTimeout.Font = new Font("微软雅黑", 9F);
            lblWriteTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblWriteTimeout.Location = new Point(250, 45);
            lblWriteTimeout.Name = "lblWriteTimeout";
            lblWriteTimeout.Size = new Size(79, 17);
            lblWriteTimeout.TabIndex = 2;
            lblWriteTimeout.Text = "写入超时(秒):";
            // 
            // numWriteTimeout
            // 
            numWriteTimeout.Font = new Font("微软雅黑", 9F);
            numWriteTimeout.Location = new Point(345, 42);
            numWriteTimeout.Margin = new Padding(4, 5, 4, 5);
            numWriteTimeout.Maximum = 300D;
            numWriteTimeout.Minimum = 1D;
            numWriteTimeout.MinimumSize = new Size(100, 0);
            numWriteTimeout.Name = "numWriteTimeout";
            numWriteTimeout.Padding = new Padding(5);
            numWriteTimeout.RectColor = Color.FromArgb(65, 100, 204);
            numWriteTimeout.ShowText = false;
            numWriteTimeout.Size = new Size(100, 29);
            numWriteTimeout.TabIndex = 3;
            numWriteTimeout.Text = "3";
            numWriteTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numWriteTimeout.Value = 3;
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
            lblNewLineType.Location = new Point(210, 160);
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
            grpResponseSettings.Controls.Add(chkCloseAfterSend);
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
            chkWaitResponse.Location = new Point(20, 45);
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
            lblResponseTimeout.Location = new Point(125, 48);
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
            numResponseTimeout.Text = "3";
            numResponseTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            numResponseTimeout.Value = 3;
            // 
            // lblResponseVariable
            // 
            lblResponseVariable.AutoSize = true;
            lblResponseVariable.Font = new Font("微软雅黑", 9F);
            lblResponseVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblResponseVariable.Location = new Point(295, 48);
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
            cmbResponseVariable.Location = new Point(370, 45);
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
            btnCreateVariable.Location = new Point(560, 45);
            btnCreateVariable.MinimumSize = new Size(1, 1);
            btnCreateVariable.Name = "btnCreateVariable";
            btnCreateVariable.Size = new Size(80, 29);
            btnCreateVariable.Symbol = 61543;
            btnCreateVariable.TabIndex = 5;
            btnCreateVariable.Text = "新建";
            btnCreateVariable.TipsFont = new Font("微软雅黑", 9F);
            // 
            // chkCloseAfterSend
            // 
            chkCloseAfterSend.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkCloseAfterSend.Checked = true;
            chkCloseAfterSend.Font = new Font("微软雅黑", 9F);
            chkCloseAfterSend.ForeColor = Color.FromArgb(48, 48, 48);
            chkCloseAfterSend.Location = new Point(20, 85);
            chkCloseAfterSend.MinimumSize = new Size(1, 1);
            chkCloseAfterSend.Name = "chkCloseAfterSend";
            chkCloseAfterSend.Size = new Size(150, 24);
            chkCloseAfterSend.TabIndex = 6;
            chkCloseAfterSend.Text = "发送后关闭串口";
            // 
            // lblCondition
            // 
            lblCondition.AutoSize = true;
            lblCondition.Font = new Font("微软雅黑", 9F);
            lblCondition.ForeColor = Color.FromArgb(48, 48, 48);
            lblCondition.Location = new Point(240, 88);
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
            panelBottom.Location = new Point(0, 742);
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
            // Form_SerialPortSend
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(800, 812);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelDescription);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_SerialPortSend";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Text = "串口发送配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 700);
            panelDescription.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            grpSerialPort.ResumeLayout(false);
            grpSerialPort.PerformLayout();
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

        // 串口设置组
        private Sunny.UI.UIGroupBox grpSerialPort;
        private Sunny.UI.UILabel lblPortName;
        private Sunny.UI.UIComboBox cmbPortName;
        private Sunny.UI.UISymbolButton btnRefreshPorts;
        private Sunny.UI.UILabel lblBaudRate;
        private Sunny.UI.UIComboBox cmbBaudRate;
        private Sunny.UI.UILabel lblDataBits;
        private Sunny.UI.UIComboBox cmbDataBits;
        private Sunny.UI.UILabel lblParity;
        private Sunny.UI.UIComboBox cmbParity;
        private Sunny.UI.UILabel lblStopBits;
        private Sunny.UI.UIComboBox cmbStopBits;
        private Sunny.UI.UILabel lblHandshake;
        private Sunny.UI.UIComboBox cmbHandshake;
        private Sunny.UI.UISymbolButton btnTestPort;
        private Sunny.UI.UILabel lblPortStatus;

        // 超时设置组
        private Sunny.UI.UIGroupBox grpTimeout;
        private Sunny.UI.UILabel lblReadTimeout;
        private Sunny.UI.UIIntegerUpDown numReadTimeout;
        private Sunny.UI.UILabel lblWriteTimeout;
        private Sunny.UI.UIIntegerUpDown numWriteTimeout;

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
        private Sunny.UI.UICheckBox chkCloseAfterSend;
        private Sunny.UI.UILabel lblCondition;
        private Sunny.UI.UITextBox txtCondition;

        // 底部按钮
        private Sunny.UI.UIButton btnTestSend;
        private Sunny.UI.UIButton btnHelp;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSave;
    }
}
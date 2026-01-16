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
            components = new System.ComponentModel.Container();

            // 主面板
            panelMain = new System.Windows.Forms.Panel();
            panelBottom = new System.Windows.Forms.Panel();

            // 串口设置组
            grpSerialPort = new Sunny.UI.UIGroupBox();
            lblPortName = new Sunny.UI.UILabel();
            cmbPortName = new Sunny.UI.UIComboBox();
            btnRefreshPorts = new Sunny.UI.UISymbolButton();
            lblBaudRate = new Sunny.UI.UILabel();
            cmbBaudRate = new Sunny.UI.UIComboBox();
            lblDataBits = new Sunny.UI.UILabel();
            cmbDataBits = new Sunny.UI.UIComboBox();
            lblParity = new Sunny.UI.UILabel();
            cmbParity = new Sunny.UI.UIComboBox();
            lblStopBits = new Sunny.UI.UILabel();
            cmbStopBits = new Sunny.UI.UIComboBox();
            lblHandshake = new Sunny.UI.UILabel();
            cmbHandshake = new Sunny.UI.UIComboBox();
            btnTestPort = new Sunny.UI.UISymbolButton();
            lblPortStatus = new Sunny.UI.UILabel();

            // 超时设置组
            grpTimeout = new Sunny.UI.UIGroupBox();
            lblReadTimeout = new Sunny.UI.UILabel();
            numReadTimeout = new Sunny.UI.UIIntegerUpDown();
            lblWriteTimeout = new Sunny.UI.UILabel();
            numWriteTimeout = new Sunny.UI.UIIntegerUpDown();

            // 数据设置组
            grpDataSettings = new Sunny.UI.UIGroupBox();
            lblDataFormat = new Sunny.UI.UILabel();
            cmbDataFormat = new Sunny.UI.UIComboBox();
            lblEncoding = new Sunny.UI.UILabel();
            cmbEncoding = new Sunny.UI.UIComboBox();
            lblSendContent = new Sunny.UI.UILabel();
            txtSendContent = new Sunny.UI.UITextBox();
            btnInsertVariable = new Sunny.UI.UISymbolButton();
            chkAppendNewLine = new Sunny.UI.UICheckBox();
            lblNewLineType = new Sunny.UI.UILabel();
            cmbNewLineType = new Sunny.UI.UIComboBox();

            // 响应设置组
            grpResponseSettings = new Sunny.UI.UIGroupBox();
            chkWaitResponse = new Sunny.UI.UICheckBox();
            lblResponseTimeout = new Sunny.UI.UILabel();
            numResponseTimeout = new Sunny.UI.UIIntegerUpDown();
            lblResponseVariable = new Sunny.UI.UILabel();
            cmbResponseVariable = new Sunny.UI.UIComboBox();
            btnCreateVariable = new Sunny.UI.UISymbolButton();

            // 其他设置组
            grpOtherSettings = new Sunny.UI.UIGroupBox();
            chkCloseAfterSend = new Sunny.UI.UICheckBox();
            lblCondition = new Sunny.UI.UILabel();
            txtCondition = new Sunny.UI.UITextBox();
            btnConditionHelper = new Sunny.UI.UISymbolButton();
            lblDescription = new Sunny.UI.UILabel();
            txtDescription = new Sunny.UI.UITextBox();
            chkEnabled = new Sunny.UI.UICheckBox();

            // 底部按钮
            btnTestSend = new Sunny.UI.UISymbolButton();
            btnOK = new Sunny.UI.UISymbolButton();
            btnCancel = new Sunny.UI.UISymbolButton();

            panelMain.SuspendLayout();
            panelBottom.SuspendLayout();
            grpSerialPort.SuspendLayout();
            grpTimeout.SuspendLayout();
            grpDataSettings.SuspendLayout();
            grpResponseSettings.SuspendLayout();
            grpOtherSettings.SuspendLayout();
            SuspendLayout();

            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.Controls.Add(grpOtherSettings);
            panelMain.Controls.Add(grpResponseSettings);
            panelMain.Controls.Add(grpDataSettings);
            panelMain.Controls.Add(grpTimeout);
            panelMain.Controls.Add(grpSerialPort);
            panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMain.Location = new System.Drawing.Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Padding = new System.Windows.Forms.Padding(15);
            panelMain.Size = new System.Drawing.Size(720, 665);
            panelMain.TabIndex = 0;

            // 
            // panelBottom
            // 
            panelBottom.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelBottom.Controls.Add(btnTestSend);
            panelBottom.Controls.Add(btnOK);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelBottom.Location = new System.Drawing.Point(0, 700);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new System.Drawing.Size(720, 60);
            panelBottom.TabIndex = 1;

            // ============================================
            // grpSerialPort - 串口设置
            // ============================================
            grpSerialPort.Controls.Add(lblPortStatus);
            grpSerialPort.Controls.Add(btnTestPort);
            grpSerialPort.Controls.Add(cmbHandshake);
            grpSerialPort.Controls.Add(lblHandshake);
            grpSerialPort.Controls.Add(cmbStopBits);
            grpSerialPort.Controls.Add(lblStopBits);
            grpSerialPort.Controls.Add(cmbParity);
            grpSerialPort.Controls.Add(lblParity);
            grpSerialPort.Controls.Add(cmbDataBits);
            grpSerialPort.Controls.Add(lblDataBits);
            grpSerialPort.Controls.Add(cmbBaudRate);
            grpSerialPort.Controls.Add(lblBaudRate);
            grpSerialPort.Controls.Add(btnRefreshPorts);
            grpSerialPort.Controls.Add(cmbPortName);
            grpSerialPort.Controls.Add(lblPortName);
            grpSerialPort.Dock = System.Windows.Forms.DockStyle.Top;
            grpSerialPort.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            grpSerialPort.Location = new System.Drawing.Point(15, 15);
            grpSerialPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpSerialPort.MinimumSize = new System.Drawing.Size(1, 1);
            grpSerialPort.Name = "grpSerialPort";
            grpSerialPort.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpSerialPort.Size = new System.Drawing.Size(690, 140);
            grpSerialPort.TabIndex = 0;
            grpSerialPort.Text = "串口设置";
            grpSerialPort.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            //grpSerialPort.TitleColor = System.Drawing.Color.FromArgb(65, 100, 204);

            // 
            // lblPortName
            // 
            lblPortName.AutoSize = true;
            lblPortName.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblPortName.Location = new System.Drawing.Point(20, 45);
            lblPortName.Name = "lblPortName";
            lblPortName.Size = new System.Drawing.Size(56, 17);
            lblPortName.TabIndex = 0;
            lblPortName.Text = "串口号:";

            // 
            // cmbPortName
            // 
            cmbPortName.DataSource = null;
            cmbPortName.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbPortName.FillColor = System.Drawing.Color.White;
            cmbPortName.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbPortName.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbPortName.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbPortName.Location = new System.Drawing.Point(80, 42);
            cmbPortName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbPortName.MinimumSize = new System.Drawing.Size(63, 0);
            cmbPortName.Name = "cmbPortName";
            cmbPortName.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbPortName.Radius = 5;
            cmbPortName.Size = new System.Drawing.Size(100, 29);
            cmbPortName.TabIndex = 1;
            cmbPortName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbPortName.Watermark = "";

            // 
            // btnRefreshPorts
            // 
            btnRefreshPorts.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRefreshPorts.Font = new System.Drawing.Font("微软雅黑", 9F);
            btnRefreshPorts.Location = new System.Drawing.Point(185, 42);
            btnRefreshPorts.MinimumSize = new System.Drawing.Size(1, 1);
            btnRefreshPorts.Name = "btnRefreshPorts";
            btnRefreshPorts.Radius = 5;
            btnRefreshPorts.Size = new System.Drawing.Size(35, 29);
            btnRefreshPorts.Symbol = 61473;
            btnRefreshPorts.TabIndex = 2;
            btnRefreshPorts.TipsFont = new System.Drawing.Font("微软雅黑", 9F);

            // 
            // lblBaudRate
            // 
            lblBaudRate.AutoSize = true;
            lblBaudRate.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblBaudRate.Location = new System.Drawing.Point(230, 45);
            lblBaudRate.Name = "lblBaudRate";
            lblBaudRate.Size = new System.Drawing.Size(56, 17);
            lblBaudRate.TabIndex = 3;
            lblBaudRate.Text = "波特率:";

            // 
            // cmbBaudRate
            // 
            cmbBaudRate.DataSource = null;
            cmbBaudRate.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbBaudRate.FillColor = System.Drawing.Color.White;
            cmbBaudRate.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbBaudRate.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbBaudRate.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbBaudRate.Location = new System.Drawing.Point(290, 42);
            cmbBaudRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbBaudRate.MinimumSize = new System.Drawing.Size(63, 0);
            cmbBaudRate.Name = "cmbBaudRate";
            cmbBaudRate.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbBaudRate.Radius = 5;
            cmbBaudRate.Size = new System.Drawing.Size(100, 29);
            cmbBaudRate.TabIndex = 4;
            cmbBaudRate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbBaudRate.Watermark = "";

            // 
            // lblDataBits
            // 
            lblDataBits.AutoSize = true;
            lblDataBits.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblDataBits.Location = new System.Drawing.Point(400, 45);
            lblDataBits.Name = "lblDataBits";
            lblDataBits.Size = new System.Drawing.Size(56, 17);
            lblDataBits.TabIndex = 5;
            lblDataBits.Text = "数据位:";

            // 
            // cmbDataBits
            // 
            cmbDataBits.DataSource = null;
            cmbDataBits.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbDataBits.FillColor = System.Drawing.Color.White;
            cmbDataBits.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbDataBits.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbDataBits.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbDataBits.Location = new System.Drawing.Point(460, 42);
            cmbDataBits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbDataBits.MinimumSize = new System.Drawing.Size(63, 0);
            cmbDataBits.Name = "cmbDataBits";
            cmbDataBits.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbDataBits.Radius = 5;
            cmbDataBits.Size = new System.Drawing.Size(70, 29);
            cmbDataBits.TabIndex = 6;
            cmbDataBits.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbDataBits.Watermark = "";

            // 
            // lblParity
            // 
            lblParity.AutoSize = true;
            lblParity.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblParity.Location = new System.Drawing.Point(540, 45);
            lblParity.Name = "lblParity";
            lblParity.Size = new System.Drawing.Size(56, 17);
            lblParity.TabIndex = 7;
            lblParity.Text = "校验位:";

            // 
            // cmbParity
            // 
            cmbParity.DataSource = null;
            cmbParity.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbParity.FillColor = System.Drawing.Color.White;
            cmbParity.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbParity.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbParity.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbParity.Location = new System.Drawing.Point(600, 42);
            cmbParity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbParity.MinimumSize = new System.Drawing.Size(63, 0);
            cmbParity.Name = "cmbParity";
            cmbParity.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbParity.Radius = 5;
            cmbParity.Size = new System.Drawing.Size(75, 29);
            cmbParity.TabIndex = 8;
            cmbParity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbParity.Watermark = "";

            // 
            // lblStopBits
            // 
            lblStopBits.AutoSize = true;
            lblStopBits.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblStopBits.Location = new System.Drawing.Point(20, 85);
            lblStopBits.Name = "lblStopBits";
            lblStopBits.Size = new System.Drawing.Size(56, 17);
            lblStopBits.TabIndex = 9;
            lblStopBits.Text = "停止位:";

            // 
            // cmbStopBits
            // 
            cmbStopBits.DataSource = null;
            cmbStopBits.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbStopBits.FillColor = System.Drawing.Color.White;
            cmbStopBits.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbStopBits.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbStopBits.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbStopBits.Location = new System.Drawing.Point(80, 82);
            cmbStopBits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbStopBits.MinimumSize = new System.Drawing.Size(63, 0);
            cmbStopBits.Name = "cmbStopBits";
            cmbStopBits.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbStopBits.Radius = 5;
            cmbStopBits.Size = new System.Drawing.Size(80, 29);
            cmbStopBits.TabIndex = 10;
            cmbStopBits.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbStopBits.Watermark = "";

            // 
            // lblHandshake
            // 
            lblHandshake.AutoSize = true;
            lblHandshake.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblHandshake.Location = new System.Drawing.Point(175, 85);
            lblHandshake.Name = "lblHandshake";
            lblHandshake.Size = new System.Drawing.Size(68, 17);
            lblHandshake.TabIndex = 11;
            lblHandshake.Text = "流控方式:";

            // 
            // cmbHandshake
            // 
            cmbHandshake.DataSource = null;
            cmbHandshake.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbHandshake.FillColor = System.Drawing.Color.White;
            cmbHandshake.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbHandshake.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbHandshake.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbHandshake.Location = new System.Drawing.Point(250, 82);
            cmbHandshake.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbHandshake.MinimumSize = new System.Drawing.Size(63, 0);
            cmbHandshake.Name = "cmbHandshake";
            cmbHandshake.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbHandshake.Radius = 5;
            cmbHandshake.Size = new System.Drawing.Size(120, 29);
            cmbHandshake.TabIndex = 12;
            cmbHandshake.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbHandshake.Watermark = "";

            // 
            // btnTestPort
            // 
            btnTestPort.Cursor = System.Windows.Forms.Cursors.Hand;
            btnTestPort.Font = new System.Drawing.Font("微软雅黑", 9F);
            btnTestPort.Location = new System.Drawing.Point(390, 82);
            btnTestPort.MinimumSize = new System.Drawing.Size(1, 1);
            btnTestPort.Name = "btnTestPort";
            btnTestPort.Radius = 5;
            btnTestPort.Size = new System.Drawing.Size(100, 29);
            btnTestPort.Symbol = 61728;
            btnTestPort.TabIndex = 13;
            btnTestPort.Text = "测试串口";
            btnTestPort.TipsFont = new System.Drawing.Font("微软雅黑", 9F);

            // 
            // lblPortStatus
            // 
            lblPortStatus.AutoSize = true;
            lblPortStatus.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblPortStatus.ForeColor = System.Drawing.Color.Gray;
            lblPortStatus.Location = new System.Drawing.Point(500, 85);
            lblPortStatus.Name = "lblPortStatus";
            lblPortStatus.Size = new System.Drawing.Size(56, 17);
            lblPortStatus.TabIndex = 14;
            lblPortStatus.Text = "未测试";

            // ============================================
            // grpTimeout - 超时设置
            // ============================================
            grpTimeout.Controls.Add(numWriteTimeout);
            grpTimeout.Controls.Add(lblWriteTimeout);
            grpTimeout.Controls.Add(numReadTimeout);
            grpTimeout.Controls.Add(lblReadTimeout);
            grpTimeout.Dock = System.Windows.Forms.DockStyle.Top;
            grpTimeout.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            grpTimeout.Location = new System.Drawing.Point(15, 155);
            grpTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpTimeout.MinimumSize = new System.Drawing.Size(1, 1);
            grpTimeout.Name = "grpTimeout";
            grpTimeout.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpTimeout.Size = new System.Drawing.Size(690, 75);
            grpTimeout.TabIndex = 1;
            grpTimeout.Text = "超时设置";
            grpTimeout.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            //grpTimeout.TitleColor = System.Drawing.Color.FromArgb(65, 100, 204);

            // 
            // lblReadTimeout
            // 
            lblReadTimeout.AutoSize = true;
            lblReadTimeout.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblReadTimeout.Location = new System.Drawing.Point(20, 42);
            lblReadTimeout.Name = "lblReadTimeout";
            lblReadTimeout.Size = new System.Drawing.Size(92, 17);
            lblReadTimeout.TabIndex = 0;
            lblReadTimeout.Text = "读取超时(ms):";

            // 
            // numReadTimeout
            // 
            numReadTimeout.Font = new System.Drawing.Font("微软雅黑", 9F);
            numReadTimeout.Location = new System.Drawing.Point(120, 39);
            numReadTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            numReadTimeout.Maximum = 60000;
            numReadTimeout.Minimum = 100;
            numReadTimeout.MinimumSize = new System.Drawing.Size(100, 0);
            numReadTimeout.Name = "numReadTimeout";
            numReadTimeout.Radius = 5;
            numReadTimeout.RectColor = System.Drawing.Color.FromArgb(65, 100, 204);
            numReadTimeout.Size = new System.Drawing.Size(100, 29);
            numReadTimeout.Step = 100;
            numReadTimeout.TabIndex = 1;
            numReadTimeout.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            numReadTimeout.Value = 5000;

            // 
            // lblWriteTimeout
            // 
            lblWriteTimeout.AutoSize = true;
            lblWriteTimeout.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblWriteTimeout.Location = new System.Drawing.Point(250, 42);
            lblWriteTimeout.Name = "lblWriteTimeout";
            lblWriteTimeout.Size = new System.Drawing.Size(92, 17);
            lblWriteTimeout.TabIndex = 2;
            lblWriteTimeout.Text = "写入超时(ms):";

            // 
            // numWriteTimeout
            // 
            numWriteTimeout.Font = new System.Drawing.Font("微软雅黑", 9F);
            numWriteTimeout.Location = new System.Drawing.Point(350, 39);
            numWriteTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            numWriteTimeout.Maximum = 60000;
            numWriteTimeout.Minimum = 100;
            numWriteTimeout.MinimumSize = new System.Drawing.Size(100, 0);
            numWriteTimeout.Name = "numWriteTimeout";
            numWriteTimeout.Radius = 5;
            numWriteTimeout.RectColor = System.Drawing.Color.FromArgb(65, 100, 204);
            numWriteTimeout.Size = new System.Drawing.Size(100, 29);
            numWriteTimeout.Step = 100;
            numWriteTimeout.TabIndex = 3;
            numWriteTimeout.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            numWriteTimeout.Value = 5000;

            // ============================================
            // grpDataSettings - 数据设置
            // ============================================
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
            grpDataSettings.Dock = System.Windows.Forms.DockStyle.Top;
            grpDataSettings.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            grpDataSettings.Location = new System.Drawing.Point(15, 230);
            grpDataSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpDataSettings.MinimumSize = new System.Drawing.Size(1, 1);
            grpDataSettings.Name = "grpDataSettings";
            grpDataSettings.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpDataSettings.Size = new System.Drawing.Size(690, 175);
            grpDataSettings.TabIndex = 2;
            grpDataSettings.Text = "数据设置";
            grpDataSettings.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            //grpDataSettings.TitleColor = System.Drawing.Color.FromArgb(65, 100, 204);

            // 
            // lblDataFormat
            // 
            lblDataFormat.AutoSize = true;
            lblDataFormat.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblDataFormat.Location = new System.Drawing.Point(20, 45);
            lblDataFormat.Name = "lblDataFormat";
            lblDataFormat.Size = new System.Drawing.Size(68, 17);
            lblDataFormat.TabIndex = 0;
            lblDataFormat.Text = "数据格式:";

            // 
            // cmbDataFormat
            // 
            cmbDataFormat.DataSource = null;
            cmbDataFormat.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbDataFormat.FillColor = System.Drawing.Color.White;
            cmbDataFormat.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbDataFormat.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbDataFormat.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbDataFormat.Location = new System.Drawing.Point(95, 42);
            cmbDataFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbDataFormat.MinimumSize = new System.Drawing.Size(63, 0);
            cmbDataFormat.Name = "cmbDataFormat";
            cmbDataFormat.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbDataFormat.Radius = 5;
            cmbDataFormat.Size = new System.Drawing.Size(100, 29);
            cmbDataFormat.TabIndex = 1;
            cmbDataFormat.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbDataFormat.Watermark = "";

            // 
            // lblEncoding
            // 
            lblEncoding.AutoSize = true;
            lblEncoding.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblEncoding.Location = new System.Drawing.Point(210, 45);
            lblEncoding.Name = "lblEncoding";
            lblEncoding.Size = new System.Drawing.Size(68, 17);
            lblEncoding.TabIndex = 2;
            lblEncoding.Text = "字符编码:";

            // 
            // cmbEncoding
            // 
            cmbEncoding.DataSource = null;
            cmbEncoding.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbEncoding.FillColor = System.Drawing.Color.White;
            cmbEncoding.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbEncoding.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbEncoding.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbEncoding.Location = new System.Drawing.Point(285, 42);
            cmbEncoding.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbEncoding.MinimumSize = new System.Drawing.Size(63, 0);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbEncoding.Radius = 5;
            cmbEncoding.Size = new System.Drawing.Size(100, 29);
            cmbEncoding.TabIndex = 3;
            cmbEncoding.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbEncoding.Watermark = "";

            // 
            // lblSendContent
            // 
            lblSendContent.AutoSize = true;
            lblSendContent.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblSendContent.Location = new System.Drawing.Point(20, 82);
            lblSendContent.Name = "lblSendContent";
            lblSendContent.Size = new System.Drawing.Size(68, 17);
            lblSendContent.TabIndex = 4;
            lblSendContent.Text = "发送内容:";

            // 
            // txtSendContent
            // 
            txtSendContent.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtSendContent.Font = new System.Drawing.Font("微软雅黑", 9F);
            txtSendContent.Location = new System.Drawing.Point(95, 79);
            txtSendContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtSendContent.MinimumSize = new System.Drawing.Size(1, 16);
            txtSendContent.Multiline = true;
            txtSendContent.Name = "txtSendContent";
            txtSendContent.Padding = new System.Windows.Forms.Padding(5);
            txtSendContent.Radius = 5;
            //txtSendContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtSendContent.ShowText = false;
            txtSendContent.Size = new System.Drawing.Size(470, 55);
            txtSendContent.TabIndex = 5;
            txtSendContent.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            txtSendContent.Watermark = "输入发送内容，支持 {变量名} 格式引用变量";

            // 
            // btnInsertVariable
            // 
            btnInsertVariable.Cursor = System.Windows.Forms.Cursors.Hand;
            btnInsertVariable.Font = new System.Drawing.Font("微软雅黑", 9F);
            btnInsertVariable.Location = new System.Drawing.Point(575, 79);
            btnInsertVariable.MinimumSize = new System.Drawing.Size(1, 1);
            btnInsertVariable.Name = "btnInsertVariable";
            btnInsertVariable.Radius = 5;
            btnInsertVariable.Size = new System.Drawing.Size(100, 29);
            btnInsertVariable.Symbol = 61618;
            btnInsertVariable.TabIndex = 6;
            btnInsertVariable.Text = "插入变量";
            btnInsertVariable.TipsFont = new System.Drawing.Font("微软雅黑", 9F);

            // 
            // chkAppendNewLine
            // 
            chkAppendNewLine.Cursor = System.Windows.Forms.Cursors.Hand;
            chkAppendNewLine.Font = new System.Drawing.Font("微软雅黑", 9F);
            chkAppendNewLine.Location = new System.Drawing.Point(20, 142);
            chkAppendNewLine.MinimumSize = new System.Drawing.Size(1, 1);
            chkAppendNewLine.Name = "chkAppendNewLine";
            chkAppendNewLine.Size = new System.Drawing.Size(100, 24);
            chkAppendNewLine.TabIndex = 7;
            chkAppendNewLine.Text = "追加换行符";

            // 
            // lblNewLineType
            // 
            lblNewLineType.AutoSize = true;
            lblNewLineType.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblNewLineType.Location = new System.Drawing.Point(130, 145);
            lblNewLineType.Name = "lblNewLineType";
            lblNewLineType.Size = new System.Drawing.Size(68, 17);
            lblNewLineType.TabIndex = 8;
            lblNewLineType.Text = "换行类型:";

            // 
            // cmbNewLineType
            // 
            cmbNewLineType.DataSource = null;
            cmbNewLineType.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbNewLineType.FillColor = System.Drawing.Color.White;
            cmbNewLineType.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbNewLineType.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbNewLineType.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbNewLineType.Location = new System.Drawing.Point(205, 142);
            cmbNewLineType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbNewLineType.MinimumSize = new System.Drawing.Size(63, 0);
            cmbNewLineType.Name = "cmbNewLineType";
            cmbNewLineType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbNewLineType.Radius = 5;
            cmbNewLineType.Size = new System.Drawing.Size(100, 29);
            cmbNewLineType.TabIndex = 9;
            cmbNewLineType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbNewLineType.Watermark = "";

            // ============================================
            // grpResponseSettings - 响应设置
            // ============================================
            grpResponseSettings.Controls.Add(btnCreateVariable);
            grpResponseSettings.Controls.Add(cmbResponseVariable);
            grpResponseSettings.Controls.Add(lblResponseVariable);
            grpResponseSettings.Controls.Add(numResponseTimeout);
            grpResponseSettings.Controls.Add(lblResponseTimeout);
            grpResponseSettings.Controls.Add(chkWaitResponse);
            grpResponseSettings.Dock = System.Windows.Forms.DockStyle.Top;
            grpResponseSettings.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            grpResponseSettings.Location = new System.Drawing.Point(15, 405);
            grpResponseSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpResponseSettings.MinimumSize = new System.Drawing.Size(1, 1);
            grpResponseSettings.Name = "grpResponseSettings";
            grpResponseSettings.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpResponseSettings.Size = new System.Drawing.Size(690, 85);
            grpResponseSettings.TabIndex = 3;
            grpResponseSettings.Text = "响应设置";
            grpResponseSettings.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            //grpResponseSettings.TitleColor = System.Drawing.Color.FromArgb(65, 100, 204);

            // 
            // chkWaitResponse
            // 
            chkWaitResponse.Cursor = System.Windows.Forms.Cursors.Hand;
            chkWaitResponse.Font = new System.Drawing.Font("微软雅黑", 9F);
            chkWaitResponse.Location = new System.Drawing.Point(20, 45);
            chkWaitResponse.MinimumSize = new System.Drawing.Size(1, 1);
            chkWaitResponse.Name = "chkWaitResponse";
            chkWaitResponse.Size = new System.Drawing.Size(100, 24);
            chkWaitResponse.TabIndex = 0;
            chkWaitResponse.Text = "等待响应";

            // 
            // lblResponseTimeout
            // 
            lblResponseTimeout.AutoSize = true;
            lblResponseTimeout.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblResponseTimeout.Location = new System.Drawing.Point(130, 48);
            lblResponseTimeout.Name = "lblResponseTimeout";
            lblResponseTimeout.Size = new System.Drawing.Size(92, 17);
            lblResponseTimeout.TabIndex = 1;
            lblResponseTimeout.Text = "响应超时(ms):";

            // 
            // numResponseTimeout
            // 
            numResponseTimeout.Font = new System.Drawing.Font("微软雅黑", 9F);
            numResponseTimeout.Location = new System.Drawing.Point(230, 45);
            numResponseTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            numResponseTimeout.Maximum = 60000;
            numResponseTimeout.Minimum = 100;
            numResponseTimeout.MinimumSize = new System.Drawing.Size(100, 0);
            numResponseTimeout.Name = "numResponseTimeout";
            numResponseTimeout.Radius = 5;
            numResponseTimeout.RectColor = System.Drawing.Color.FromArgb(65, 100, 204);
            numResponseTimeout.Size = new System.Drawing.Size(100, 29);
            numResponseTimeout.Step = 100;
            numResponseTimeout.TabIndex = 2;
            numResponseTimeout.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            numResponseTimeout.Value = 5000;

            // 
            // lblResponseVariable
            // 
            lblResponseVariable.AutoSize = true;
            lblResponseVariable.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblResponseVariable.Location = new System.Drawing.Point(350, 48);
            lblResponseVariable.Name = "lblResponseVariable";
            lblResponseVariable.Size = new System.Drawing.Size(80, 17);
            lblResponseVariable.TabIndex = 3;
            lblResponseVariable.Text = "保存到变量:";

            // 
            // cmbResponseVariable
            // 
            cmbResponseVariable.DataSource = null;
            cmbResponseVariable.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDown;
            cmbResponseVariable.FillColor = System.Drawing.Color.White;
            cmbResponseVariable.Font = new System.Drawing.Font("微软雅黑", 9F);
            cmbResponseVariable.ItemHoverColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbResponseVariable.ItemSelectForeColor = System.Drawing.Color.FromArgb(65, 100, 204);
            cmbResponseVariable.Location = new System.Drawing.Point(435, 45);
            cmbResponseVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbResponseVariable.MinimumSize = new System.Drawing.Size(63, 0);
            cmbResponseVariable.Name = "cmbResponseVariable";
            cmbResponseVariable.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbResponseVariable.Radius = 5;
            cmbResponseVariable.Size = new System.Drawing.Size(140, 29);
            cmbResponseVariable.TabIndex = 4;
            cmbResponseVariable.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cmbResponseVariable.Watermark = "选择或输入变量名";

            // 
            // btnCreateVariable
            // 
            btnCreateVariable.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCreateVariable.Font = new System.Drawing.Font("微软雅黑", 9F);
            btnCreateVariable.Location = new System.Drawing.Point(585, 45);
            btnCreateVariable.MinimumSize = new System.Drawing.Size(1, 1);
            btnCreateVariable.Name = "btnCreateVariable";
            btnCreateVariable.Radius = 5;
            btnCreateVariable.Size = new System.Drawing.Size(90, 29);
            btnCreateVariable.Symbol = 61543;
            btnCreateVariable.TabIndex = 5;
            btnCreateVariable.Text = "新建变量";
            btnCreateVariable.TipsFont = new System.Drawing.Font("微软雅黑", 9F);

            // ============================================
            // grpOtherSettings - 其他设置
            // ============================================
            grpOtherSettings.Controls.Add(chkEnabled);
            grpOtherSettings.Controls.Add(txtDescription);
            grpOtherSettings.Controls.Add(lblDescription);
            grpOtherSettings.Controls.Add(btnConditionHelper);
            grpOtherSettings.Controls.Add(txtCondition);
            grpOtherSettings.Controls.Add(lblCondition);
            grpOtherSettings.Controls.Add(chkCloseAfterSend);
            grpOtherSettings.Dock = System.Windows.Forms.DockStyle.Top;
            grpOtherSettings.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            grpOtherSettings.Location = new System.Drawing.Point(15, 490);
            grpOtherSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpOtherSettings.MinimumSize = new System.Drawing.Size(1, 1);
            grpOtherSettings.Name = "grpOtherSettings";
            grpOtherSettings.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpOtherSettings.Size = new System.Drawing.Size(690, 130);
            grpOtherSettings.TabIndex = 4;
            grpOtherSettings.Text = "其他设置";
            grpOtherSettings.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            //grpOtherSettings.TitleColor = System.Drawing.Color.FromArgb(65, 100, 204);

            // 
            // chkCloseAfterSend
            // 
            chkCloseAfterSend.Cursor = System.Windows.Forms.Cursors.Hand;
            chkCloseAfterSend.Font = new System.Drawing.Font("微软雅黑", 9F);
            chkCloseAfterSend.Location = new System.Drawing.Point(20, 45);
            chkCloseAfterSend.MinimumSize = new System.Drawing.Size(1, 1);
            chkCloseAfterSend.Name = "chkCloseAfterSend";
            chkCloseAfterSend.Size = new System.Drawing.Size(120, 24);
            chkCloseAfterSend.TabIndex = 0;
            chkCloseAfterSend.Text = "发送后关闭串口";

            // 
            // lblCondition
            // 
            lblCondition.AutoSize = true;
            lblCondition.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblCondition.Location = new System.Drawing.Point(160, 48);
            lblCondition.Name = "lblCondition";
            lblCondition.Size = new System.Drawing.Size(68, 17);
            lblCondition.TabIndex = 1;
            lblCondition.Text = "执行条件:";

            // 
            // txtCondition
            // 
            txtCondition.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtCondition.Font = new System.Drawing.Font("微软雅黑", 9F);
            txtCondition.Location = new System.Drawing.Point(235, 45);
            txtCondition.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtCondition.MinimumSize = new System.Drawing.Size(1, 16);
            txtCondition.Name = "txtCondition";
            txtCondition.Padding = new System.Windows.Forms.Padding(5);
            txtCondition.Radius = 5;
            txtCondition.ShowText = false;
            txtCondition.Size = new System.Drawing.Size(340, 29);
            txtCondition.TabIndex = 2;
            txtCondition.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            txtCondition.Watermark = "留空表示始终执行";

            // 
            // btnConditionHelper
            // 
            btnConditionHelper.Cursor = System.Windows.Forms.Cursors.Hand;
            btnConditionHelper.Font = new System.Drawing.Font("微软雅黑", 9F);
            btnConditionHelper.Location = new System.Drawing.Point(585, 45);
            btnConditionHelper.MinimumSize = new System.Drawing.Size(1, 1);
            btnConditionHelper.Name = "btnConditionHelper";
            btnConditionHelper.Radius = 5;
            btnConditionHelper.Size = new System.Drawing.Size(90, 29);
            btnConditionHelper.Symbol = 61736;
            btnConditionHelper.TabIndex = 3;
            btnConditionHelper.Text = "条件助手";
            btnConditionHelper.TipsFont = new System.Drawing.Font("微软雅黑", 9F);

            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new System.Drawing.Font("微软雅黑", 9F);
            lblDescription.Location = new System.Drawing.Point(20, 88);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(44, 17);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "描述:";

            // 
            // txtDescription
            // 
            txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtDescription.Font = new System.Drawing.Font("微软雅黑", 9F);
            txtDescription.Location = new System.Drawing.Point(70, 85);
            txtDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new System.Drawing.Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new System.Windows.Forms.Padding(5);
            txtDescription.Radius = 5;
            txtDescription.ShowText = false;
            txtDescription.Size = new System.Drawing.Size(505, 29);
            txtDescription.TabIndex = 5;
            txtDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "步骤描述信息";

            // 
            // chkEnabled
            // 
            chkEnabled.Checked = true;
            chkEnabled.Cursor = System.Windows.Forms.Cursors.Hand;
            chkEnabled.Font = new System.Drawing.Font("微软雅黑", 9F);
            chkEnabled.Location = new System.Drawing.Point(585, 88);
            chkEnabled.MinimumSize = new System.Drawing.Size(1, 1);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new System.Drawing.Size(90, 24);
            chkEnabled.TabIndex = 6;
            chkEnabled.Text = "启用此步骤";

            // ============================================
            // 底部按钮
            // ============================================
            // 
            // btnTestSend
            // 
            btnTestSend.Cursor = System.Windows.Forms.Cursors.Hand;
            btnTestSend.FillColor = System.Drawing.Color.FromArgb(0, 150, 136);
            btnTestSend.FillHoverColor = System.Drawing.Color.FromArgb(0, 170, 156);
            btnTestSend.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            btnTestSend.Location = new System.Drawing.Point(15, 15);
            btnTestSend.MinimumSize = new System.Drawing.Size(1, 1);
            btnTestSend.Name = "btnTestSend";
            btnTestSend.Radius = 5;
            btnTestSend.Size = new System.Drawing.Size(110, 35);
            btnTestSend.Symbol = 61544;
            btnTestSend.TabIndex = 0;
            btnTestSend.Text = "测试发送";
            btnTestSend.TipsFont = new System.Drawing.Font("微软雅黑", 9F);

            // 
            // btnOK
            // 
            btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOK.FillColor = System.Drawing.Color.FromArgb(65, 100, 204);
            btnOK.FillHoverColor = System.Drawing.Color.FromArgb(85, 120, 224);
            btnOK.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            btnOK.Location = new System.Drawing.Point(495, 15);
            btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Radius = 5;
            btnOK.Size = new System.Drawing.Size(110, 35);
            btnOK.Symbol = 61694;
            btnOK.TabIndex = 1;
            btnOK.Text = "确定";
            btnOK.TipsFont = new System.Drawing.Font("微软雅黑", 9F);

            // 
            // btnCancel
            // 
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.FillColor = System.Drawing.Color.FromArgb(110, 110, 110);
            btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(130, 130, 130);
            btnCancel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.Location = new System.Drawing.Point(615, 15);
            btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 5;
            btnCancel.Size = new System.Drawing.Size(90, 35);
            btnCancel.Symbol = 61527;
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new System.Drawing.Font("微软雅黑", 9F);

            // ============================================
            // Form_SerialPortSend
            // ============================================
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(720, 760);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_SerialPortSend";
            RectColor = System.Drawing.Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "串口发送配置";
            TitleColor = System.Drawing.Color.FromArgb(65, 100, 204);
            TitleFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 720, 760);

            panelMain.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            grpSerialPort.ResumeLayout(false);
            grpSerialPort.PerformLayout();
            grpTimeout.ResumeLayout(false);
            grpTimeout.PerformLayout();
            grpDataSettings.ResumeLayout(false);
            grpDataSettings.PerformLayout();
            grpResponseSettings.ResumeLayout(false);
            grpResponseSettings.PerformLayout();
            grpOtherSettings.ResumeLayout(false);
            grpOtherSettings.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // 主面板
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelBottom;

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

        // 其他设置组
        private Sunny.UI.UIGroupBox grpOtherSettings;
        private Sunny.UI.UICheckBox chkCloseAfterSend;
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
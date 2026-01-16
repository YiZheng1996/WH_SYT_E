namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_RealtimeMonitorPromptConfig
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
            panelMain = new UIPanel();
            grpButtonConfig = new UIGroupBox();
            cmbIconType = new UIComboBox();
            lblIconType = new UILabel();
            txtButtonText = new UITextBox();
            lblButtonText = new UILabel();
            grpDisplayConfig = new UIGroupBox();
            numRefreshInterval = new UIIntegerUpDown();
            lblRefreshInterval = new UILabel();
            txtValueLabelText = new UITextBox();
            lblValueLabelText = new UILabel();
            chkShowValueLabel = new UICheckBox();
            txtDisplayFormat = new UITextBox();
            lblDisplayFormat = new UILabel();
            txtUnit = new UITextBox();
            lblUnit = new UILabel();
            grpPromptConfig = new UIGroupBox();
            txtPromptMessage = new UIRichTextBox();
            lblPromptMessage = new UILabel();
            grpMonitorSource = new UIGroupBox();
            pnlPlcSource = new Panel();
            cmbPlcAddress = new UIComboBox();
            lblPlcAddress = new UILabel();
            cmbPlcModule = new UIComboBox();
            lblPlcModule = new UILabel();
            pnlVariableSource = new Panel();
            cmbMonitorVariable = new UIComboBox();
            lblMonitorVariable = new UILabel();
            cmbMonitorSourceType = new UIComboBox();
            lblMonitorSourceType = new UILabel();
            grpBasicConfig = new UIGroupBox();
            txtDescription = new UITextBox();
            lblDescription = new UILabel();
            txtTitle = new UITextBox();
            lblTitle = new UILabel();
            panelBottom = new Panel();
            btnCancel = new UIButton();
            btnSave = new UIButton();
            btnTest = new UIButton();
            panelMain.SuspendLayout();
            grpButtonConfig.SuspendLayout();
            grpDisplayConfig.SuspendLayout();
            grpPromptConfig.SuspendLayout();
            grpMonitorSource.SuspendLayout();
            pnlPlcSource.SuspendLayout();
            pnlVariableSource.SuspendLayout();
            grpBasicConfig.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.BackColor = Color.FromArgb(248, 249, 250);
            panelMain.Controls.Add(grpButtonConfig);
            panelMain.Controls.Add(grpDisplayConfig);
            panelMain.Controls.Add(grpPromptConfig);
            panelMain.Controls.Add(grpMonitorSource);
            panelMain.Controls.Add(grpBasicConfig);
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.FromArgb(248, 249, 250);
            panelMain.FillColor2 = Color.FromArgb(248, 249, 250);
            panelMain.Font = new Font("微软雅黑", 12F);
            panelMain.Location = new Point(0, 35);
            panelMain.Margin = new Padding(4, 5, 4, 5);
            panelMain.MinimumSize = new Size(1, 1);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15, 10, 15, 10);
            panelMain.RectColor = Color.FromArgb(220, 220, 220);
            panelMain.Size = new Size(700, 776);
            panelMain.TabIndex = 0;
            panelMain.Text = null;
            panelMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // grpButtonConfig
            // 
            grpButtonConfig.Controls.Add(cmbIconType);
            grpButtonConfig.Controls.Add(lblIconType);
            grpButtonConfig.Controls.Add(txtButtonText);
            grpButtonConfig.Controls.Add(lblButtonText);
            grpButtonConfig.FillColor = Color.White;
            grpButtonConfig.FillColor2 = Color.White;
            grpButtonConfig.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            grpButtonConfig.Location = new Point(20, 655);
            grpButtonConfig.Margin = new Padding(4, 5, 4, 5);
            grpButtonConfig.MinimumSize = new Size(1, 1);
            grpButtonConfig.Name = "grpButtonConfig";
            grpButtonConfig.Padding = new Padding(0, 32, 0, 0);
            grpButtonConfig.Size = new Size(655, 110);
            grpButtonConfig.TabIndex = 4;
            grpButtonConfig.Text = "按钮配置";
            grpButtonConfig.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // cmbIconType
            // 
            cmbIconType.DataSource = null;
            cmbIconType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbIconType.FillColor = Color.White;
            cmbIconType.Font = new Font("微软雅黑", 10F);
            cmbIconType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbIconType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbIconType.Location = new Point(140, 73);
            cmbIconType.Margin = new Padding(4, 5, 4, 5);
            cmbIconType.MinimumSize = new Size(63, 0);
            cmbIconType.Name = "cmbIconType";
            cmbIconType.Padding = new Padding(0, 0, 30, 2);
            cmbIconType.Size = new Size(200, 29);
            cmbIconType.SymbolSize = 24;
            cmbIconType.TabIndex = 3;
            cmbIconType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbIconType.Watermark = "";
            // 
            // lblIconType
            // 
            lblIconType.Font = new Font("微软雅黑", 10F);
            lblIconType.ForeColor = Color.FromArgb(48, 48, 48);
            lblIconType.Location = new Point(15, 75);
            lblIconType.Name = "lblIconType";
            lblIconType.Size = new Size(120, 23);
            lblIconType.TabIndex = 2;
            lblIconType.Text = "窗体图标:";
            lblIconType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtButtonText
            // 
            txtButtonText.Cursor = Cursors.IBeam;
            txtButtonText.Font = new Font("微软雅黑", 10F);
            txtButtonText.Location = new Point(140, 38);
            txtButtonText.Margin = new Padding(4, 5, 4, 5);
            txtButtonText.MinimumSize = new Size(1, 16);
            txtButtonText.Name = "txtButtonText";
            txtButtonText.Padding = new Padding(5);
            txtButtonText.ShowText = false;
            txtButtonText.Size = new Size(200, 29);
            txtButtonText.TabIndex = 1;
            txtButtonText.Text = "确定";
            txtButtonText.TextAlignment = ContentAlignment.MiddleLeft;
            txtButtonText.Watermark = "";
            // 
            // lblButtonText
            // 
            lblButtonText.Font = new Font("微软雅黑", 10F);
            lblButtonText.ForeColor = Color.FromArgb(48, 48, 48);
            lblButtonText.Location = new Point(15, 40);
            lblButtonText.Name = "lblButtonText";
            lblButtonText.Size = new Size(120, 23);
            lblButtonText.TabIndex = 0;
            lblButtonText.Text = "按钮文本:";
            lblButtonText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpDisplayConfig
            // 
            grpDisplayConfig.Controls.Add(numRefreshInterval);
            grpDisplayConfig.Controls.Add(lblRefreshInterval);
            grpDisplayConfig.Controls.Add(txtValueLabelText);
            grpDisplayConfig.Controls.Add(lblValueLabelText);
            grpDisplayConfig.Controls.Add(chkShowValueLabel);
            grpDisplayConfig.Controls.Add(txtDisplayFormat);
            grpDisplayConfig.Controls.Add(lblDisplayFormat);
            grpDisplayConfig.Controls.Add(txtUnit);
            grpDisplayConfig.Controls.Add(lblUnit);
            grpDisplayConfig.FillColor = Color.White;
            grpDisplayConfig.FillColor2 = Color.White;
            grpDisplayConfig.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            grpDisplayConfig.Location = new Point(20, 455);
            grpDisplayConfig.Margin = new Padding(4, 5, 4, 5);
            grpDisplayConfig.MinimumSize = new Size(1, 1);
            grpDisplayConfig.Name = "grpDisplayConfig";
            grpDisplayConfig.Padding = new Padding(0, 32, 0, 0);
            grpDisplayConfig.Size = new Size(655, 190);
            grpDisplayConfig.TabIndex = 3;
            grpDisplayConfig.Text = "显示配置";
            grpDisplayConfig.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // numRefreshInterval
            // 
            numRefreshInterval.Font = new Font("微软雅黑", 10F);
            numRefreshInterval.Location = new Point(140, 146);
            numRefreshInterval.Margin = new Padding(4, 5, 4, 5);
            numRefreshInterval.Maximum = 5000D;
            numRefreshInterval.Minimum = 100D;
            numRefreshInterval.MinimumSize = new Size(100, 0);
            numRefreshInterval.Name = "numRefreshInterval";
            numRefreshInterval.Padding = new Padding(5);
            numRefreshInterval.ShowText = false;
            numRefreshInterval.Size = new Size(200, 29);
            numRefreshInterval.TabIndex = 8;
            numRefreshInterval.Text = "500";
            numRefreshInterval.TextAlignment = ContentAlignment.MiddleCenter;
            numRefreshInterval.Value = 500;
            // 
            // lblRefreshInterval
            // 
            lblRefreshInterval.Font = new Font("微软雅黑", 10F);
            lblRefreshInterval.ForeColor = Color.FromArgb(48, 48, 48);
            lblRefreshInterval.Location = new Point(15, 148);
            lblRefreshInterval.Name = "lblRefreshInterval";
            lblRefreshInterval.Size = new Size(120, 23);
            lblRefreshInterval.TabIndex = 7;
            lblRefreshInterval.Text = "刷新间隔(ms):";
            lblRefreshInterval.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtValueLabelText
            // 
            txtValueLabelText.Cursor = Cursors.IBeam;
            txtValueLabelText.Font = new Font("微软雅黑", 10F);
            txtValueLabelText.Location = new Point(305, 111);
            txtValueLabelText.Margin = new Padding(4, 5, 4, 5);
            txtValueLabelText.MinimumSize = new Size(1, 16);
            txtValueLabelText.Name = "txtValueLabelText";
            txtValueLabelText.Padding = new Padding(5);
            txtValueLabelText.ShowText = false;
            txtValueLabelText.Size = new Size(330, 29);
            txtValueLabelText.TabIndex = 6;
            txtValueLabelText.TextAlignment = ContentAlignment.MiddleLeft;
            txtValueLabelText.Watermark = "如: PE05(kPa)";
            // 
            // lblValueLabelText
            // 
            lblValueLabelText.Font = new Font("微软雅黑", 10F);
            lblValueLabelText.ForeColor = Color.FromArgb(48, 48, 48);
            lblValueLabelText.Location = new Point(200, 113);
            lblValueLabelText.Name = "lblValueLabelText";
            lblValueLabelText.Size = new Size(100, 23);
            lblValueLabelText.TabIndex = 5;
            lblValueLabelText.Text = "标签文本:";
            lblValueLabelText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkShowValueLabel
            // 
            chkShowValueLabel.Checked = true;
            chkShowValueLabel.Font = new Font("微软雅黑", 10F);
            chkShowValueLabel.ForeColor = Color.FromArgb(48, 48, 48);
            chkShowValueLabel.Location = new Point(15, 110);
            chkShowValueLabel.MinimumSize = new Size(1, 1);
            chkShowValueLabel.Name = "chkShowValueLabel";
            chkShowValueLabel.Padding = new Padding(22, 0, 0, 0);
            chkShowValueLabel.Size = new Size(150, 29);
            chkShowValueLabel.TabIndex = 4;
            chkShowValueLabel.Text = "显示数值标签";
            // 
            // txtDisplayFormat
            // 
            txtDisplayFormat.Cursor = Cursors.IBeam;
            txtDisplayFormat.Font = new Font("微软雅黑", 10F);
            txtDisplayFormat.Location = new Point(140, 73);
            txtDisplayFormat.Margin = new Padding(4, 5, 4, 5);
            txtDisplayFormat.MinimumSize = new Size(1, 16);
            txtDisplayFormat.Name = "txtDisplayFormat";
            txtDisplayFormat.Padding = new Padding(5);
            txtDisplayFormat.ShowText = false;
            txtDisplayFormat.Size = new Size(200, 29);
            txtDisplayFormat.TabIndex = 3;
            txtDisplayFormat.Text = "F1";
            txtDisplayFormat.TextAlignment = ContentAlignment.MiddleLeft;
            txtDisplayFormat.Watermark = "F1=1位小数";
            // 
            // lblDisplayFormat
            // 
            lblDisplayFormat.Font = new Font("微软雅黑", 10F);
            lblDisplayFormat.ForeColor = Color.FromArgb(48, 48, 48);
            lblDisplayFormat.Location = new Point(15, 75);
            lblDisplayFormat.Name = "lblDisplayFormat";
            lblDisplayFormat.Size = new Size(120, 23);
            lblDisplayFormat.TabIndex = 2;
            lblDisplayFormat.Text = "显示格式:";
            lblDisplayFormat.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtUnit
            // 
            txtUnit.Cursor = Cursors.IBeam;
            txtUnit.Font = new Font("微软雅黑", 10F);
            txtUnit.Location = new Point(140, 38);
            txtUnit.Margin = new Padding(4, 5, 4, 5);
            txtUnit.MinimumSize = new Size(1, 16);
            txtUnit.Name = "txtUnit";
            txtUnit.Padding = new Padding(5);
            txtUnit.ShowText = false;
            txtUnit.Size = new Size(200, 29);
            txtUnit.TabIndex = 1;
            txtUnit.TextAlignment = ContentAlignment.MiddleLeft;
            txtUnit.Watermark = "如: kPa, ℃, MPa";
            // 
            // lblUnit
            // 
            lblUnit.Font = new Font("微软雅黑", 10F);
            lblUnit.ForeColor = Color.FromArgb(48, 48, 48);
            lblUnit.Location = new Point(15, 40);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(120, 23);
            lblUnit.TabIndex = 0;
            lblUnit.Text = "数值单位:";
            lblUnit.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpPromptConfig
            // 
            grpPromptConfig.Controls.Add(txtPromptMessage);
            grpPromptConfig.Controls.Add(lblPromptMessage);
            grpPromptConfig.FillColor = Color.White;
            grpPromptConfig.FillColor2 = Color.White;
            grpPromptConfig.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            grpPromptConfig.Location = new Point(20, 305);
            grpPromptConfig.Margin = new Padding(4, 5, 4, 5);
            grpPromptConfig.MinimumSize = new Size(1, 1);
            grpPromptConfig.Name = "grpPromptConfig";
            grpPromptConfig.Padding = new Padding(0, 32, 0, 0);
            grpPromptConfig.Size = new Size(655, 140);
            grpPromptConfig.TabIndex = 2;
            grpPromptConfig.Text = "提示信息";
            grpPromptConfig.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // txtPromptMessage
            // 
            txtPromptMessage.FillColor = Color.White;
            txtPromptMessage.Font = new Font("微软雅黑", 10F);
            txtPromptMessage.Location = new Point(15, 68);
            txtPromptMessage.Margin = new Padding(4, 5, 4, 5);
            txtPromptMessage.MinimumSize = new Size(1, 1);
            txtPromptMessage.Name = "txtPromptMessage";
            txtPromptMessage.Padding = new Padding(2);
            txtPromptMessage.ShowText = false;
            txtPromptMessage.Size = new Size(620, 60);
            txtPromptMessage.TabIndex = 1;
            txtPromptMessage.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblPromptMessage
            // 
            lblPromptMessage.Font = new Font("微软雅黑", 10F);
            lblPromptMessage.ForeColor = Color.FromArgb(48, 48, 48);
            lblPromptMessage.Location = new Point(15, 40);
            lblPromptMessage.Name = "lblPromptMessage";
            lblPromptMessage.Size = new Size(120, 23);
            lblPromptMessage.TabIndex = 0;
            lblPromptMessage.Text = "*提示信息:";
            lblPromptMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpMonitorSource
            // 
            grpMonitorSource.Controls.Add(pnlPlcSource);
            grpMonitorSource.Controls.Add(pnlVariableSource);
            grpMonitorSource.Controls.Add(cmbMonitorSourceType);
            grpMonitorSource.Controls.Add(lblMonitorSourceType);
            grpMonitorSource.FillColor = Color.White;
            grpMonitorSource.FillColor2 = Color.White;
            grpMonitorSource.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            grpMonitorSource.Location = new Point(20, 145);
            grpMonitorSource.Margin = new Padding(4, 5, 4, 5);
            grpMonitorSource.MinimumSize = new Size(1, 1);
            grpMonitorSource.Name = "grpMonitorSource";
            grpMonitorSource.Padding = new Padding(0, 32, 0, 0);
            grpMonitorSource.Size = new Size(655, 150);
            grpMonitorSource.TabIndex = 1;
            grpMonitorSource.Text = "监测源配置";
            grpMonitorSource.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // pnlPlcSource
            // 
            pnlPlcSource.Controls.Add(cmbPlcAddress);
            pnlPlcSource.Controls.Add(lblPlcAddress);
            pnlPlcSource.Controls.Add(cmbPlcModule);
            pnlPlcSource.Controls.Add(lblPlcModule);
            pnlPlcSource.Location = new Point(15, 80);
            pnlPlcSource.Name = "pnlPlcSource";
            pnlPlcSource.Size = new Size(625, 65);
            pnlPlcSource.TabIndex = 3;
            pnlPlcSource.Visible = false;
            // 
            // cmbPlcAddress
            // 
            cmbPlcAddress.DataSource = null;
            cmbPlcAddress.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbPlcAddress.FillColor = Color.White;
            cmbPlcAddress.Font = new Font("微软雅黑", 10F);
            cmbPlcAddress.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbPlcAddress.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbPlcAddress.Location = new Point(125, 34);
            cmbPlcAddress.Margin = new Padding(4, 5, 4, 5);
            cmbPlcAddress.MinimumSize = new Size(63, 0);
            cmbPlcAddress.Name = "cmbPlcAddress";
            cmbPlcAddress.Padding = new Padding(0, 0, 30, 2);
            cmbPlcAddress.Size = new Size(480, 29);
            cmbPlcAddress.SymbolSize = 24;
            cmbPlcAddress.TabIndex = 3;
            cmbPlcAddress.TextAlignment = ContentAlignment.MiddleLeft;
            cmbPlcAddress.Watermark = "";
            // 
            // lblPlcAddress
            // 
            lblPlcAddress.Font = new Font("微软雅黑", 10F);
            lblPlcAddress.ForeColor = Color.FromArgb(48, 48, 48);
            lblPlcAddress.Location = new Point(3, 36);
            lblPlcAddress.Name = "lblPlcAddress";
            lblPlcAddress.Size = new Size(103, 23);
            lblPlcAddress.TabIndex = 2;
            lblPlcAddress.Text = "*PLC地址:";
            lblPlcAddress.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbPlcModule
            // 
            cmbPlcModule.DataSource = null;
            cmbPlcModule.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbPlcModule.FillColor = Color.White;
            cmbPlcModule.Font = new Font("微软雅黑", 10F);
            cmbPlcModule.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbPlcModule.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbPlcModule.Location = new Point(125, 3);
            cmbPlcModule.Margin = new Padding(4, 5, 4, 5);
            cmbPlcModule.MinimumSize = new Size(63, 0);
            cmbPlcModule.Name = "cmbPlcModule";
            cmbPlcModule.Padding = new Padding(0, 0, 30, 2);
            cmbPlcModule.Size = new Size(480, 29);
            cmbPlcModule.SymbolSize = 24;
            cmbPlcModule.TabIndex = 1;
            cmbPlcModule.TextAlignment = ContentAlignment.MiddleLeft;
            cmbPlcModule.Watermark = "";
            cmbPlcModule.SelectedIndexChanged += CmbPlcModule_SelectedIndexChanged;
            // 
            // lblPlcModule
            // 
            lblPlcModule.Font = new Font("微软雅黑", 10F);
            lblPlcModule.ForeColor = Color.FromArgb(48, 48, 48);
            lblPlcModule.Location = new Point(3, 5);
            lblPlcModule.Name = "lblPlcModule";
            lblPlcModule.Size = new Size(100, 23);
            lblPlcModule.TabIndex = 0;
            lblPlcModule.Text = "*PLC模块:";
            lblPlcModule.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlVariableSource
            // 
            pnlVariableSource.Controls.Add(cmbMonitorVariable);
            pnlVariableSource.Controls.Add(lblMonitorVariable);
            pnlVariableSource.Location = new Point(15, 80);
            pnlVariableSource.Name = "pnlVariableSource";
            pnlVariableSource.Size = new Size(625, 40);
            pnlVariableSource.TabIndex = 2;
            pnlVariableSource.Visible = false;
            // 
            // cmbMonitorVariable
            // 
            cmbMonitorVariable.DataSource = null;
            cmbMonitorVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbMonitorVariable.FillColor = Color.White;
            cmbMonitorVariable.Font = new Font("微软雅黑", 10F);
            cmbMonitorVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbMonitorVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbMonitorVariable.Location = new Point(125, 6);
            cmbMonitorVariable.Margin = new Padding(4, 5, 4, 5);
            cmbMonitorVariable.MinimumSize = new Size(63, 0);
            cmbMonitorVariable.Name = "cmbMonitorVariable";
            cmbMonitorVariable.Padding = new Padding(0, 0, 30, 2);
            cmbMonitorVariable.Size = new Size(480, 29);
            cmbMonitorVariable.SymbolSize = 24;
            cmbMonitorVariable.TabIndex = 1;
            cmbMonitorVariable.TextAlignment = ContentAlignment.MiddleLeft;
            cmbMonitorVariable.Watermark = "";
            // 
            // lblMonitorVariable
            // 
            lblMonitorVariable.Font = new Font("微软雅黑", 10F);
            lblMonitorVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblMonitorVariable.Location = new Point(0, 8);
            lblMonitorVariable.Name = "lblMonitorVariable";
            lblMonitorVariable.Size = new Size(120, 23);
            lblMonitorVariable.TabIndex = 0;
            lblMonitorVariable.Text = "*监测变量:";
            lblMonitorVariable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbMonitorSourceType
            // 
            cmbMonitorSourceType.DataSource = null;
            cmbMonitorSourceType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbMonitorSourceType.FillColor = Color.White;
            cmbMonitorSourceType.Font = new Font("微软雅黑", 10F);
            cmbMonitorSourceType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbMonitorSourceType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbMonitorSourceType.Location = new Point(140, 38);
            cmbMonitorSourceType.Margin = new Padding(4, 5, 4, 5);
            cmbMonitorSourceType.MinimumSize = new Size(63, 0);
            cmbMonitorSourceType.Name = "cmbMonitorSourceType";
            cmbMonitorSourceType.Padding = new Padding(0, 0, 30, 2);
            cmbMonitorSourceType.Size = new Size(200, 29);
            cmbMonitorSourceType.SymbolSize = 24;
            cmbMonitorSourceType.TabIndex = 1;
            cmbMonitorSourceType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbMonitorSourceType.Watermark = "";
            cmbMonitorSourceType.SelectedIndexChanged += CmbMonitorSourceType_SelectedIndexChanged;
            // 
            // lblMonitorSourceType
            // 
            lblMonitorSourceType.Font = new Font("微软雅黑", 10F);
            lblMonitorSourceType.ForeColor = Color.FromArgb(48, 48, 48);
            lblMonitorSourceType.Location = new Point(15, 40);
            lblMonitorSourceType.Name = "lblMonitorSourceType";
            lblMonitorSourceType.Size = new Size(120, 23);
            lblMonitorSourceType.TabIndex = 0;
            lblMonitorSourceType.Text = "*监测源类型:";
            lblMonitorSourceType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpBasicConfig
            // 
            grpBasicConfig.Controls.Add(txtDescription);
            grpBasicConfig.Controls.Add(lblDescription);
            grpBasicConfig.Controls.Add(txtTitle);
            grpBasicConfig.Controls.Add(lblTitle);
            grpBasicConfig.FillColor = Color.White;
            grpBasicConfig.FillColor2 = Color.White;
            grpBasicConfig.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            grpBasicConfig.Location = new Point(20, 15);
            grpBasicConfig.Margin = new Padding(4, 5, 4, 5);
            grpBasicConfig.MinimumSize = new Size(1, 1);
            grpBasicConfig.Name = "grpBasicConfig";
            grpBasicConfig.Padding = new Padding(0, 32, 0, 0);
            grpBasicConfig.Size = new Size(655, 120);
            grpBasicConfig.TabIndex = 0;
            grpBasicConfig.Text = "基本配置";
            grpBasicConfig.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            txtDescription.Cursor = Cursors.IBeam;
            txtDescription.Font = new Font("微软雅黑", 10F);
            txtDescription.Location = new Point(120, 75);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(515, 29);
            txtDescription.TabIndex = 3;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "";
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("微软雅黑", 10F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(15, 77);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(100, 23);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "步骤描述:";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTitle
            // 
            txtTitle.Cursor = Cursors.IBeam;
            txtTitle.Font = new Font("微软雅黑", 10F);
            txtTitle.Location = new Point(120, 38);
            txtTitle.Margin = new Padding(4, 5, 4, 5);
            txtTitle.MinimumSize = new Size(1, 16);
            txtTitle.Name = "txtTitle";
            txtTitle.Padding = new Padding(5);
            txtTitle.ShowText = false;
            txtTitle.Size = new Size(515, 29);
            txtTitle.TabIndex = 1;
            txtTitle.TextAlignment = ContentAlignment.MiddleLeft;
            txtTitle.Watermark = "";
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("微软雅黑", 10F);
            lblTitle.ForeColor = Color.FromArgb(48, 48, 48);
            lblTitle.Location = new Point(15, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(100, 23);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "*窗体标题:";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Controls.Add(btnTest);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 811);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(15, 10, 15, 15);
            panelBottom.Size = new Size(700, 75);
            panelBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(570, 18);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 40);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            btnCancel.DialogResult = DialogResult.Cancel;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.Font = new Font("微软雅黑", 10F);
            btnSave.Location = new Point(460, 18);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 40);
            btnSave.TabIndex = 1;
            btnSave.Text = "确定";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            btnSave.Click += BtnOK_Click;
            // 
            // btnTest
            // 
            btnTest.Cursor = Cursors.Hand;
            btnTest.Font = new Font("微软雅黑", 10F);
            btnTest.Location = new Point(350, 18);
            btnTest.MinimumSize = new Size(1, 1);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(90, 40);
            btnTest.TabIndex = 0;
            btnTest.Text = "测试";
            btnTest.TipsFont = new Font("微软雅黑", 9F);
            btnTest.Click += BtnTest_Click;
            // 
            // Form_RealtimeMonitorPromptConfig
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(700, 886);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_RealtimeMonitorPromptConfig";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Text = "实时监控提示配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 700, 840);
            panelMain.ResumeLayout(false);
            grpButtonConfig.ResumeLayout(false);
            grpDisplayConfig.ResumeLayout(false);
            grpPromptConfig.ResumeLayout(false);
            grpMonitorSource.ResumeLayout(false);
            pnlPlcSource.ResumeLayout(false);
            pnlVariableSource.ResumeLayout(false);
            grpBasicConfig.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        //private UIButton btnOK;
        private Sunny.UI.UIPanel panelMain;
        private Sunny.UI.UIGroupBox grpBasicConfig;
        private Sunny.UI.UITextBox txtDescription;
        private Sunny.UI.UILabel lblDescription;
        private Sunny.UI.UITextBox txtTitle;
        private Sunny.UI.UILabel lblTitle;
        private Sunny.UI.UIGroupBox grpMonitorSource;
        private System.Windows.Forms.Panel pnlPlcSource;
        private Sunny.UI.UIComboBox cmbPlcAddress;
        private Sunny.UI.UILabel lblPlcAddress;
        private Sunny.UI.UIComboBox cmbPlcModule;
        private Sunny.UI.UILabel lblPlcModule;
        private System.Windows.Forms.Panel pnlVariableSource;
        private Sunny.UI.UIComboBox cmbMonitorVariable;
        private Sunny.UI.UILabel lblMonitorVariable;
        private Sunny.UI.UIComboBox cmbMonitorSourceType;
        private Sunny.UI.UILabel lblMonitorSourceType;
        private Sunny.UI.UIGroupBox grpPromptConfig;
        private Sunny.UI.UIRichTextBox txtPromptMessage;
        private Sunny.UI.UILabel lblPromptMessage;
        private Sunny.UI.UIGroupBox grpDisplayConfig;
        private Sunny.UI.UIIntegerUpDown numRefreshInterval;
        private Sunny.UI.UILabel lblRefreshInterval;
        private Sunny.UI.UITextBox txtValueLabelText;
        private Sunny.UI.UILabel lblValueLabelText;
        private Sunny.UI.UICheckBox chkShowValueLabel;
        private Sunny.UI.UITextBox txtDisplayFormat;
        private Sunny.UI.UILabel lblDisplayFormat;
        private Sunny.UI.UITextBox txtUnit;
        private Sunny.UI.UILabel lblUnit;
        private Sunny.UI.UIGroupBox grpButtonConfig;
        private Sunny.UI.UIComboBox cmbIconType;
        private Sunny.UI.UILabel lblIconType;
        private Sunny.UI.UITextBox txtButtonText;
        private Sunny.UI.UILabel lblButtonText;
        private System.Windows.Forms.Panel panelBottom;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UIButton btnTest;
    }
}
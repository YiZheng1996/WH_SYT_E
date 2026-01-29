namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    partial class Form_InstrumentCommunication
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            mainPanel = new TableLayoutPanel();
            tabControl = new UITabControl();
            tabBasic = new TabPage();
            layoutBasic = new TableLayoutPanel();
            lblDescription = new Label();
            txtDescription = new UITextBox();
            lblInstrument = new Label();
            panelInstrument = new FlowLayoutPanel();
            cboInstrument = new UIComboBox();
            btnManageDrivers = new UISymbolButton();
            btnTestConnection = new UISymbolButton();
            lblCommand = new Label();
            cboCommand = new UIComboBox();
            lblCustom = new Label();
            chkCustomCommand = new UICheckBox();
            lblCommandContent = new Label();
            panelCustomCommand = new FlowLayoutPanel();
            txtCustomCommand = new UITextBox();
            cboCustomDataType = new UIComboBox();
            lblParams = new Label();
            panelCommandParams = new UIPanel();
            flowParams = new FlowLayoutPanel();
            tabResponse = new TabPage();
            layoutResponse = new TableLayoutPanel();
            lblResponseVar = new Label();
            txtResponseVariable = new UITextBox();
            lblStatusVar = new Label();
            txtStatusVariable = new UITextBox();
            lblErrorVar = new Label();
            txtErrorVariable = new UITextBox();
            lblParseRules = new Label();
            grpParseRules = new UIGroupBox();
            dgvParseRules = new UIDataGridView();
            tabAdvanced = new TabPage();
            layoutAdvanced = new TableLayoutPanel();
            lblTimeout = new Label();
            panelTimeout = new FlowLayoutPanel();
            chkOverrideTimeout = new UICheckBox();
            txtTimeout = new UITextBox();
            lblTimeoutMs = new Label();
            lblRetryCount = new Label();
            txtRetryCount = new UITextBox();
            lblRetryInterval = new Label();
            panelRetryInterval = new FlowLayoutPanel();
            txtRetryInterval = new UITextBox();
            lblRetryMs = new Label();
            lblDelayBefore = new Label();
            panelDelayBefore = new FlowLayoutPanel();
            txtDelayBefore = new UITextBox();
            lblDelayBeforeMs = new Label();
            lblDelayAfter = new Label();
            panelDelayAfter = new FlowLayoutPanel();
            txtDelayAfter = new UITextBox();
            lblDelayAfterMs = new Label();
            lblFailureStrategy = new Label();
            cboFailureStrategy = new UIComboBox();
            lblJumpStep = new Label();
            txtJumpStep = new UITextBox();
            lblLogging = new Label();
            chkEnableLogging = new UICheckBox();
            lblCondition = new Label();
            txtExecuteCondition = new UITextBox();
            panelButtons = new Panel();
            btnOk = new UISymbolButton();
            btnCancel = new UISymbolButton();
            mainPanel.SuspendLayout();
            tabControl.SuspendLayout();
            tabBasic.SuspendLayout();
            layoutBasic.SuspendLayout();
            panelInstrument.SuspendLayout();
            panelCustomCommand.SuspendLayout();
            panelCommandParams.SuspendLayout();
            tabResponse.SuspendLayout();
            layoutResponse.SuspendLayout();
            grpParseRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvParseRules).BeginInit();
            tabAdvanced.SuspendLayout();
            layoutAdvanced.SuspendLayout();
            panelTimeout.SuspendLayout();
            panelRetryInterval.SuspendLayout();
            panelDelayBefore.SuspendLayout();
            panelDelayAfter.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.ColumnCount = 1;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainPanel.Controls.Add(tabControl, 0, 0);
            mainPanel.Controls.Add(panelButtons, 0, 1);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 35);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(10);
            mainPanel.RowCount = 2;
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            mainPanel.Size = new Size(870, 745);
            mainPanel.TabIndex = 0;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabBasic);
            tabControl.Controls.Add(tabResponse);
            tabControl.Controls.Add(tabAdvanced);
            tabControl.Dock = DockStyle.Fill;
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.Font = new Font("微软雅黑", 10F);
            tabControl.ItemSize = new Size(150, 40);
            tabControl.Location = new Point(13, 13);
            tabControl.MainPage = "";
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(844, 669);
            tabControl.SizeMode = TabSizeMode.Fixed;
            tabControl.TabIndex = 0;
            tabControl.TabUnSelectedForeColor = Color.FromArgb(240, 240, 240);
            tabControl.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // tabBasic
            // 
            tabBasic.Controls.Add(layoutBasic);
            tabBasic.Location = new Point(0, 40);
            tabBasic.Name = "tabBasic";
            tabBasic.Size = new Size(844, 629);
            tabBasic.TabIndex = 0;
            tabBasic.Text = "基本配置";
            // 
            // layoutBasic
            // 
            layoutBasic.ColumnCount = 2;
            layoutBasic.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutBasic.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBasic.Controls.Add(lblDescription, 0, 0);
            layoutBasic.Controls.Add(txtDescription, 1, 0);
            layoutBasic.Controls.Add(lblInstrument, 0, 1);
            layoutBasic.Controls.Add(panelInstrument, 1, 1);
            layoutBasic.Controls.Add(lblCommand, 0, 2);
            layoutBasic.Controls.Add(cboCommand, 1, 2);
            layoutBasic.Controls.Add(lblCustom, 0, 3);
            layoutBasic.Controls.Add(chkCustomCommand, 1, 3);
            layoutBasic.Controls.Add(lblCommandContent, 0, 4);
            layoutBasic.Controls.Add(panelCustomCommand, 1, 4);
            layoutBasic.Controls.Add(lblParams, 0, 5);
            layoutBasic.Controls.Add(panelCommandParams, 1, 5);
            layoutBasic.Dock = DockStyle.Fill;
            layoutBasic.Location = new Point(0, 0);
            layoutBasic.Name = "layoutBasic";
            layoutBasic.RowCount = 6;
            layoutBasic.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutBasic.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutBasic.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutBasic.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            layoutBasic.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutBasic.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBasic.Size = new Size(844, 629);
            layoutBasic.TabIndex = 0;
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Font = new Font("微软雅黑", 9F);
            lblDescription.Location = new Point(3, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(94, 38);
            lblDescription.TabIndex = 0;
            lblDescription.Text = "描述:";
            lblDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtDescription.Location = new Point(104, 5);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(736, 28);
            txtDescription.TabIndex = 1;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "请输入步骤描述";
            // 
            // lblInstrument
            // 
            lblInstrument.Dock = DockStyle.Fill;
            lblInstrument.Font = new Font("微软雅黑", 9F);
            lblInstrument.Location = new Point(3, 38);
            lblInstrument.Name = "lblInstrument";
            lblInstrument.Size = new Size(94, 45);
            lblInstrument.TabIndex = 2;
            lblInstrument.Text = "选择仪器:";
            lblInstrument.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelInstrument
            // 
            panelInstrument.Controls.Add(cboInstrument);
            panelInstrument.Controls.Add(btnManageDrivers);
            panelInstrument.Controls.Add(btnTestConnection);
            panelInstrument.Dock = DockStyle.Fill;
            panelInstrument.Location = new Point(103, 41);
            panelInstrument.Name = "panelInstrument";
            panelInstrument.Size = new Size(738, 39);
            panelInstrument.TabIndex = 3;
            // 
            // cboInstrument
            // 
            cboInstrument.DataSource = null;
            cboInstrument.DropDownStyle = UIDropDownStyle.DropDownList;
            cboInstrument.FillColor = Color.White;
            cboInstrument.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboInstrument.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboInstrument.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboInstrument.Location = new Point(4, 5);
            cboInstrument.Margin = new Padding(4, 5, 4, 5);
            cboInstrument.MinimumSize = new Size(63, 0);
            cboInstrument.Name = "cboInstrument";
            cboInstrument.Padding = new Padding(0, 0, 30, 2);
            cboInstrument.Size = new Size(350, 29);
            cboInstrument.SymbolSize = 24;
            cboInstrument.TabIndex = 0;
            cboInstrument.TextAlignment = ContentAlignment.MiddleLeft;
            cboInstrument.Watermark = "请选择已配置的仪器";
            // 
            // btnManageDrivers
            // 
            btnManageDrivers.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnManageDrivers.Location = new Point(361, 3);
            btnManageDrivers.MinimumSize = new Size(1, 1);
            btnManageDrivers.Name = "btnManageDrivers";
            btnManageDrivers.Size = new Size(80, 29);
            btnManageDrivers.Symbol = 61459;
            btnManageDrivers.TabIndex = 1;
            btnManageDrivers.Text = "管理";
            btnManageDrivers.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnTestConnection
            // 
            btnTestConnection.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnTestConnection.Location = new Point(447, 3);
            btnTestConnection.MinimumSize = new Size(1, 1);
            btnTestConnection.Name = "btnTestConnection";
            btnTestConnection.Size = new Size(80, 29);
            btnTestConnection.Symbol = 61931;
            btnTestConnection.TabIndex = 2;
            btnTestConnection.Text = "测试";
            btnTestConnection.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // lblCommand
            // 
            lblCommand.Dock = DockStyle.Fill;
            lblCommand.Font = new Font("微软雅黑", 9F);
            lblCommand.Location = new Point(3, 83);
            lblCommand.Name = "lblCommand";
            lblCommand.Size = new Size(94, 40);
            lblCommand.TabIndex = 4;
            lblCommand.Text = "选择命令:";
            lblCommand.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboCommand
            // 
            cboCommand.DataSource = null;
            cboCommand.Dock = DockStyle.Fill;
            cboCommand.DropDownStyle = UIDropDownStyle.DropDownList;
            cboCommand.FillColor = Color.White;
            cboCommand.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboCommand.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboCommand.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboCommand.Location = new Point(104, 88);
            cboCommand.Margin = new Padding(4, 5, 4, 5);
            cboCommand.MinimumSize = new Size(63, 0);
            cboCommand.Name = "cboCommand";
            cboCommand.Padding = new Padding(0, 0, 30, 2);
            cboCommand.Size = new Size(736, 30);
            cboCommand.SymbolSize = 24;
            cboCommand.TabIndex = 5;
            cboCommand.TextAlignment = ContentAlignment.MiddleLeft;
            cboCommand.Watermark = "请先选择仪器";
            // 
            // lblCustom
            // 
            lblCustom.Dock = DockStyle.Fill;
            lblCustom.Font = new Font("微软雅黑", 9F);
            lblCustom.Location = new Point(3, 123);
            lblCustom.Name = "lblCustom";
            lblCustom.Size = new Size(94, 22);
            lblCustom.TabIndex = 6;
            lblCustom.Text = "自定义:";
            lblCustom.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkCustomCommand
            // 
            chkCustomCommand.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            chkCustomCommand.ForeColor = Color.FromArgb(48, 48, 48);
            chkCustomCommand.Location = new Point(103, 126);
            chkCustomCommand.MinimumSize = new Size(1, 1);
            chkCustomCommand.Name = "chkCustomCommand";
            chkCustomCommand.Size = new Size(150, 16);
            chkCustomCommand.TabIndex = 7;
            chkCustomCommand.Text = "使用自定义命令";
            // 
            // lblCommandContent
            // 
            lblCommandContent.Dock = DockStyle.Fill;
            lblCommandContent.Font = new Font("微软雅黑", 9F);
            lblCommandContent.Location = new Point(3, 145);
            lblCommandContent.Name = "lblCommandContent";
            lblCommandContent.Size = new Size(94, 44);
            lblCommandContent.TabIndex = 8;
            lblCommandContent.Text = "命令内容:";
            lblCommandContent.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelCustomCommand
            // 
            panelCustomCommand.Controls.Add(txtCustomCommand);
            panelCustomCommand.Controls.Add(cboCustomDataType);
            panelCustomCommand.Dock = DockStyle.Fill;
            panelCustomCommand.Location = new Point(103, 148);
            panelCustomCommand.Name = "panelCustomCommand";
            panelCustomCommand.Size = new Size(738, 38);
            panelCustomCommand.TabIndex = 9;
            // 
            // txtCustomCommand
            // 
            txtCustomCommand.Enabled = false;
            txtCustomCommand.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtCustomCommand.Location = new Point(4, 5);
            txtCustomCommand.Margin = new Padding(4, 5, 4, 5);
            txtCustomCommand.MinimumSize = new Size(1, 16);
            txtCustomCommand.Name = "txtCustomCommand";
            txtCustomCommand.Padding = new Padding(5);
            txtCustomCommand.ShowText = false;
            txtCustomCommand.Size = new Size(400, 29);
            txtCustomCommand.TabIndex = 0;
            txtCustomCommand.TextAlignment = ContentAlignment.MiddleLeft;
            txtCustomCommand.Watermark = "输入自定义命令内容";
            // 
            // cboCustomDataType
            // 
            cboCustomDataType.DataSource = null;
            cboCustomDataType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboCustomDataType.Enabled = false;
            cboCustomDataType.FillColor = Color.White;
            cboCustomDataType.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboCustomDataType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboCustomDataType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboCustomDataType.Location = new Point(412, 5);
            cboCustomDataType.Margin = new Padding(4, 5, 4, 5);
            cboCustomDataType.MinimumSize = new Size(63, 0);
            cboCustomDataType.Name = "cboCustomDataType";
            cboCustomDataType.Padding = new Padding(0, 0, 30, 2);
            cboCustomDataType.Size = new Size(100, 29);
            cboCustomDataType.SymbolSize = 24;
            cboCustomDataType.TabIndex = 1;
            cboCustomDataType.TextAlignment = ContentAlignment.MiddleLeft;
            cboCustomDataType.Watermark = "";
            // 
            // lblParams
            // 
            lblParams.Dock = DockStyle.Fill;
            lblParams.Font = new Font("微软雅黑", 9F);
            lblParams.Location = new Point(3, 189);
            lblParams.Name = "lblParams";
            lblParams.Padding = new Padding(0, 10, 0, 0);
            lblParams.Size = new Size(94, 440);
            lblParams.TabIndex = 10;
            lblParams.Text = "命令参数:";
            lblParams.TextAlign = ContentAlignment.TopRight;
            // 
            // panelCommandParams
            // 
            panelCommandParams.Controls.Add(flowParams);
            panelCommandParams.Dock = DockStyle.Fill;
            panelCommandParams.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelCommandParams.Location = new Point(104, 194);
            panelCommandParams.Margin = new Padding(4, 5, 4, 5);
            panelCommandParams.MinimumSize = new Size(1, 1);
            panelCommandParams.Name = "panelCommandParams";
            panelCommandParams.Size = new Size(736, 430);
            panelCommandParams.TabIndex = 11;
            panelCommandParams.Text = "命令参数";
            panelCommandParams.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // flowParams
            // 
            flowParams.AutoScroll = true;
            flowParams.Dock = DockStyle.Fill;
            flowParams.FlowDirection = FlowDirection.TopDown;
            flowParams.Location = new Point(0, 0);
            flowParams.Name = "flowParams";
            flowParams.Padding = new Padding(10);
            flowParams.Size = new Size(736, 430);
            flowParams.TabIndex = 0;
            flowParams.WrapContents = false;
            // 
            // tabResponse
            // 
            tabResponse.Controls.Add(layoutResponse);
            tabResponse.Location = new Point(0, 40);
            tabResponse.Name = "tabResponse";
            tabResponse.Size = new Size(200, 60);
            tabResponse.TabIndex = 1;
            tabResponse.Text = "响应处理";
            // 
            // layoutResponse
            // 
            layoutResponse.ColumnCount = 2;
            layoutResponse.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutResponse.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutResponse.Controls.Add(lblResponseVar, 0, 0);
            layoutResponse.Controls.Add(txtResponseVariable, 1, 0);
            layoutResponse.Controls.Add(lblStatusVar, 0, 1);
            layoutResponse.Controls.Add(txtStatusVariable, 1, 1);
            layoutResponse.Controls.Add(lblErrorVar, 0, 2);
            layoutResponse.Controls.Add(txtErrorVariable, 1, 2);
            layoutResponse.Controls.Add(lblParseRules, 0, 3);
            layoutResponse.Controls.Add(grpParseRules, 1, 3);
            layoutResponse.Dock = DockStyle.Fill;
            layoutResponse.Location = new Point(0, 0);
            layoutResponse.Name = "layoutResponse";
            layoutResponse.RowCount = 4;
            layoutResponse.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutResponse.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            layoutResponse.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutResponse.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutResponse.Size = new Size(200, 60);
            layoutResponse.TabIndex = 0;
            // 
            // lblResponseVar
            // 
            lblResponseVar.Dock = DockStyle.Fill;
            lblResponseVar.Font = new Font("微软雅黑", 9F);
            lblResponseVar.Location = new Point(3, 0);
            lblResponseVar.Name = "lblResponseVar";
            lblResponseVar.Size = new Size(114, 38);
            lblResponseVar.TabIndex = 0;
            lblResponseVar.Text = "响应存储变量:";
            lblResponseVar.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtResponseVariable
            // 
            txtResponseVariable.Dock = DockStyle.Fill;
            txtResponseVariable.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtResponseVariable.Location = new Point(124, 5);
            txtResponseVariable.Margin = new Padding(4, 5, 4, 5);
            txtResponseVariable.MinimumSize = new Size(1, 16);
            txtResponseVariable.Name = "txtResponseVariable";
            txtResponseVariable.Padding = new Padding(5);
            txtResponseVariable.ShowText = false;
            txtResponseVariable.Size = new Size(72, 28);
            txtResponseVariable.TabIndex = 1;
            txtResponseVariable.TextAlignment = ContentAlignment.MiddleLeft;
            txtResponseVariable.Watermark = "将原始响应存储到此变量";
            // 
            // lblStatusVar
            // 
            lblStatusVar.Dock = DockStyle.Fill;
            lblStatusVar.Font = new Font("微软雅黑", 9F);
            lblStatusVar.Location = new Point(3, 38);
            lblStatusVar.Name = "lblStatusVar";
            lblStatusVar.Size = new Size(114, 39);
            lblStatusVar.TabIndex = 2;
            lblStatusVar.Text = "状态存储变量:";
            lblStatusVar.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtStatusVariable
            // 
            txtStatusVariable.Dock = DockStyle.Fill;
            txtStatusVariable.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtStatusVariable.Location = new Point(124, 43);
            txtStatusVariable.Margin = new Padding(4, 5, 4, 5);
            txtStatusVariable.MinimumSize = new Size(1, 16);
            txtStatusVariable.Name = "txtStatusVariable";
            txtStatusVariable.Padding = new Padding(5);
            txtStatusVariable.ShowText = false;
            txtStatusVariable.Size = new Size(72, 29);
            txtStatusVariable.TabIndex = 3;
            txtStatusVariable.TextAlignment = ContentAlignment.MiddleLeft;
            txtStatusVariable.Watermark = "存储执行结果(true/false)";
            // 
            // lblErrorVar
            // 
            lblErrorVar.Dock = DockStyle.Fill;
            lblErrorVar.Font = new Font("微软雅黑", 9F);
            lblErrorVar.Location = new Point(3, 77);
            lblErrorVar.Name = "lblErrorVar";
            lblErrorVar.Size = new Size(114, 40);
            lblErrorVar.TabIndex = 4;
            lblErrorVar.Text = "错误存储变量:";
            lblErrorVar.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtErrorVariable
            // 
            txtErrorVariable.Dock = DockStyle.Fill;
            txtErrorVariable.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtErrorVariable.Location = new Point(124, 82);
            txtErrorVariable.Margin = new Padding(4, 5, 4, 5);
            txtErrorVariable.MinimumSize = new Size(1, 16);
            txtErrorVariable.Name = "txtErrorVariable";
            txtErrorVariable.Padding = new Padding(5);
            txtErrorVariable.ShowText = false;
            txtErrorVariable.Size = new Size(72, 30);
            txtErrorVariable.TabIndex = 5;
            txtErrorVariable.TextAlignment = ContentAlignment.MiddleLeft;
            txtErrorVariable.Watermark = "失败时存储错误信息";
            // 
            // lblParseRules
            // 
            lblParseRules.Dock = DockStyle.Fill;
            lblParseRules.Font = new Font("微软雅黑", 9F);
            lblParseRules.Location = new Point(3, 117);
            lblParseRules.Name = "lblParseRules";
            lblParseRules.Padding = new Padding(0, 10, 0, 0);
            lblParseRules.Size = new Size(114, 1);
            lblParseRules.TabIndex = 6;
            lblParseRules.Text = "解析规则:";
            lblParseRules.TextAlign = ContentAlignment.TopRight;
            // 
            // grpParseRules
            // 
            grpParseRules.Controls.Add(dgvParseRules);
            grpParseRules.Dock = DockStyle.Fill;
            grpParseRules.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpParseRules.Location = new Point(124, 122);
            grpParseRules.Margin = new Padding(4, 5, 4, 5);
            grpParseRules.MinimumSize = new Size(1, 1);
            grpParseRules.Name = "grpParseRules";
            grpParseRules.Padding = new Padding(0, 32, 0, 0);
            grpParseRules.Size = new Size(72, 1);
            grpParseRules.TabIndex = 7;
            grpParseRules.Text = "响应数据解析规则";
            grpParseRules.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // dgvParseRules
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dgvParseRules.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvParseRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvParseRules.BackgroundColor = Color.White;
            dgvParseRules.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvParseRules.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvParseRules.ColumnHeadersHeight = 32;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvParseRules.DefaultCellStyle = dataGridViewCellStyle3;
            dgvParseRules.Dock = DockStyle.Fill;
            dgvParseRules.EnableHeadersVisualStyles = false;
            dgvParseRules.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvParseRules.GridColor = Color.FromArgb(80, 160, 255);
            dgvParseRules.Location = new Point(0, 32);
            dgvParseRules.Name = "dgvParseRules";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvParseRules.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvParseRules.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvParseRules.SelectedIndex = -1;
            dgvParseRules.Size = new Size(72, 0);
            dgvParseRules.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvParseRules.TabIndex = 0;
            // 
            // tabAdvanced
            // 
            tabAdvanced.Controls.Add(layoutAdvanced);
            tabAdvanced.Location = new Point(0, 40);
            tabAdvanced.Name = "tabAdvanced";
            tabAdvanced.Size = new Size(844, 629);
            tabAdvanced.TabIndex = 2;
            tabAdvanced.Text = "高级选项";
            // 
            // layoutAdvanced
            // 
            layoutAdvanced.ColumnCount = 4;
            layoutAdvanced.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutAdvanced.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutAdvanced.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutAdvanced.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutAdvanced.Controls.Add(lblTimeout, 0, 0);
            layoutAdvanced.Controls.Add(panelTimeout, 1, 0);
            layoutAdvanced.Controls.Add(lblRetryCount, 2, 0);
            layoutAdvanced.Controls.Add(txtRetryCount, 3, 0);
            layoutAdvanced.Controls.Add(lblRetryInterval, 0, 1);
            layoutAdvanced.Controls.Add(panelRetryInterval, 1, 1);
            layoutAdvanced.Controls.Add(lblDelayBefore, 0, 2);
            layoutAdvanced.Controls.Add(panelDelayBefore, 1, 2);
            layoutAdvanced.Controls.Add(lblDelayAfter, 2, 2);
            layoutAdvanced.Controls.Add(panelDelayAfter, 3, 2);
            layoutAdvanced.Controls.Add(lblFailureStrategy, 0, 3);
            layoutAdvanced.Controls.Add(cboFailureStrategy, 1, 3);
            layoutAdvanced.Controls.Add(lblJumpStep, 2, 3);
            layoutAdvanced.Controls.Add(txtJumpStep, 3, 3);
            layoutAdvanced.Controls.Add(lblLogging, 0, 4);
            layoutAdvanced.Controls.Add(chkEnableLogging, 1, 4);
            layoutAdvanced.Controls.Add(lblCondition, 0, 5);
            layoutAdvanced.Controls.Add(txtExecuteCondition, 1, 5);
            layoutAdvanced.Dock = DockStyle.Fill;
            layoutAdvanced.Location = new Point(0, 0);
            layoutAdvanced.Name = "layoutAdvanced";
            layoutAdvanced.RowCount = 7;
            layoutAdvanced.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutAdvanced.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutAdvanced.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            layoutAdvanced.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layoutAdvanced.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            layoutAdvanced.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutAdvanced.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutAdvanced.Size = new Size(844, 629);
            layoutAdvanced.TabIndex = 0;
            // 
            // lblTimeout
            // 
            lblTimeout.Dock = DockStyle.Fill;
            lblTimeout.Font = new Font("微软雅黑", 9F);
            lblTimeout.Location = new Point(3, 0);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(94, 40);
            lblTimeout.TabIndex = 0;
            lblTimeout.Text = "超时设置:";
            lblTimeout.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelTimeout
            // 
            panelTimeout.Controls.Add(chkOverrideTimeout);
            panelTimeout.Controls.Add(txtTimeout);
            panelTimeout.Controls.Add(lblTimeoutMs);
            panelTimeout.Dock = DockStyle.Fill;
            panelTimeout.Location = new Point(103, 3);
            panelTimeout.Name = "panelTimeout";
            panelTimeout.Size = new Size(316, 34);
            panelTimeout.TabIndex = 1;
            // 
            // chkOverrideTimeout
            // 
            chkOverrideTimeout.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            chkOverrideTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            chkOverrideTimeout.Location = new Point(3, 3);
            chkOverrideTimeout.MinimumSize = new Size(1, 1);
            chkOverrideTimeout.Name = "chkOverrideTimeout";
            chkOverrideTimeout.Size = new Size(100, 29);
            chkOverrideTimeout.TabIndex = 0;
            chkOverrideTimeout.Text = "自定义超时";
            // 
            // txtTimeout
            // 
            txtTimeout.DoubleValue = 3000D;
            txtTimeout.Enabled = false;
            txtTimeout.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtTimeout.IntValue = 3000;
            txtTimeout.Location = new Point(110, 5);
            txtTimeout.Margin = new Padding(4, 5, 4, 5);
            txtTimeout.MinimumSize = new Size(1, 16);
            txtTimeout.Name = "txtTimeout";
            txtTimeout.Padding = new Padding(5);
            txtTimeout.ShowText = false;
            txtTimeout.Size = new Size(80, 29);
            txtTimeout.TabIndex = 1;
            txtTimeout.Text = "3000";
            txtTimeout.TextAlignment = ContentAlignment.MiddleLeft;
            txtTimeout.Watermark = "";
            // 
            // lblTimeoutMs
            // 
            lblTimeoutMs.AutoSize = true;
            lblTimeoutMs.Location = new Point(197, 0);
            lblTimeoutMs.Name = "lblTimeoutMs";
            lblTimeoutMs.Padding = new Padding(0, 8, 0, 0);
            lblTimeoutMs.Size = new Size(28, 28);
            lblTimeoutMs.TabIndex = 2;
            lblTimeoutMs.Text = "ms";
            // 
            // lblRetryCount
            // 
            lblRetryCount.Dock = DockStyle.Fill;
            lblRetryCount.Font = new Font("微软雅黑", 9F);
            lblRetryCount.Location = new Point(425, 0);
            lblRetryCount.Name = "lblRetryCount";
            lblRetryCount.Size = new Size(94, 40);
            lblRetryCount.TabIndex = 2;
            lblRetryCount.Text = "重试次数:";
            lblRetryCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtRetryCount
            // 
            txtRetryCount.Dock = DockStyle.Fill;
            txtRetryCount.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtRetryCount.Location = new Point(526, 5);
            txtRetryCount.Margin = new Padding(4, 5, 4, 5);
            txtRetryCount.MinimumSize = new Size(1, 16);
            txtRetryCount.Name = "txtRetryCount";
            txtRetryCount.Padding = new Padding(5);
            txtRetryCount.ShowText = false;
            txtRetryCount.Size = new Size(314, 30);
            txtRetryCount.TabIndex = 3;
            txtRetryCount.Text = "0";
            txtRetryCount.TextAlignment = ContentAlignment.MiddleLeft;
            txtRetryCount.Watermark = "";
            // 
            // lblRetryInterval
            // 
            lblRetryInterval.Dock = DockStyle.Fill;
            lblRetryInterval.Font = new Font("微软雅黑", 9F);
            lblRetryInterval.Location = new Point(3, 40);
            lblRetryInterval.Name = "lblRetryInterval";
            lblRetryInterval.Size = new Size(94, 40);
            lblRetryInterval.TabIndex = 4;
            lblRetryInterval.Text = "重试间隔:";
            lblRetryInterval.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelRetryInterval
            // 
            panelRetryInterval.Controls.Add(txtRetryInterval);
            panelRetryInterval.Controls.Add(lblRetryMs);
            panelRetryInterval.Dock = DockStyle.Fill;
            panelRetryInterval.Location = new Point(103, 43);
            panelRetryInterval.Name = "panelRetryInterval";
            panelRetryInterval.Size = new Size(316, 34);
            panelRetryInterval.TabIndex = 5;
            // 
            // txtRetryInterval
            // 
            txtRetryInterval.DoubleValue = 500D;
            txtRetryInterval.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtRetryInterval.IntValue = 500;
            txtRetryInterval.Location = new Point(4, 5);
            txtRetryInterval.Margin = new Padding(4, 5, 4, 5);
            txtRetryInterval.MinimumSize = new Size(1, 16);
            txtRetryInterval.Name = "txtRetryInterval";
            txtRetryInterval.Padding = new Padding(5);
            txtRetryInterval.ShowText = false;
            txtRetryInterval.Size = new Size(80, 29);
            txtRetryInterval.TabIndex = 0;
            txtRetryInterval.Text = "500";
            txtRetryInterval.TextAlignment = ContentAlignment.MiddleLeft;
            txtRetryInterval.Watermark = "";
            // 
            // lblRetryMs
            // 
            lblRetryMs.AutoSize = true;
            lblRetryMs.Location = new Point(91, 0);
            lblRetryMs.Name = "lblRetryMs";
            lblRetryMs.Padding = new Padding(0, 8, 0, 0);
            lblRetryMs.Size = new Size(28, 28);
            lblRetryMs.TabIndex = 1;
            lblRetryMs.Text = "ms";
            // 
            // lblDelayBefore
            // 
            lblDelayBefore.Dock = DockStyle.Fill;
            lblDelayBefore.Font = new Font("微软雅黑", 9F);
            lblDelayBefore.Location = new Point(3, 80);
            lblDelayBefore.Name = "lblDelayBefore";
            lblDelayBefore.Size = new Size(94, 47);
            lblDelayBefore.TabIndex = 6;
            lblDelayBefore.Text = "发送前延时:";
            lblDelayBefore.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelDelayBefore
            // 
            panelDelayBefore.Controls.Add(txtDelayBefore);
            panelDelayBefore.Controls.Add(lblDelayBeforeMs);
            panelDelayBefore.Dock = DockStyle.Fill;
            panelDelayBefore.Location = new Point(103, 83);
            panelDelayBefore.Name = "panelDelayBefore";
            panelDelayBefore.Size = new Size(316, 41);
            panelDelayBefore.TabIndex = 7;
            // 
            // txtDelayBefore
            // 
            txtDelayBefore.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtDelayBefore.Location = new Point(4, 5);
            txtDelayBefore.Margin = new Padding(4, 5, 4, 5);
            txtDelayBefore.MinimumSize = new Size(1, 16);
            txtDelayBefore.Name = "txtDelayBefore";
            txtDelayBefore.Padding = new Padding(5);
            txtDelayBefore.ShowText = false;
            txtDelayBefore.Size = new Size(80, 29);
            txtDelayBefore.TabIndex = 0;
            txtDelayBefore.Text = "0";
            txtDelayBefore.TextAlignment = ContentAlignment.MiddleLeft;
            txtDelayBefore.Watermark = "";
            // 
            // lblDelayBeforeMs
            // 
            lblDelayBeforeMs.AutoSize = true;
            lblDelayBeforeMs.Location = new Point(91, 0);
            lblDelayBeforeMs.Name = "lblDelayBeforeMs";
            lblDelayBeforeMs.Padding = new Padding(0, 8, 0, 0);
            lblDelayBeforeMs.Size = new Size(28, 28);
            lblDelayBeforeMs.TabIndex = 1;
            lblDelayBeforeMs.Text = "ms";
            // 
            // lblDelayAfter
            // 
            lblDelayAfter.Dock = DockStyle.Fill;
            lblDelayAfter.Font = new Font("微软雅黑", 9F);
            lblDelayAfter.Location = new Point(425, 80);
            lblDelayAfter.Name = "lblDelayAfter";
            lblDelayAfter.Size = new Size(94, 47);
            lblDelayAfter.TabIndex = 8;
            lblDelayAfter.Text = "发送后延时:";
            lblDelayAfter.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelDelayAfter
            // 
            panelDelayAfter.Controls.Add(txtDelayAfter);
            panelDelayAfter.Controls.Add(lblDelayAfterMs);
            panelDelayAfter.Dock = DockStyle.Fill;
            panelDelayAfter.Location = new Point(525, 83);
            panelDelayAfter.Name = "panelDelayAfter";
            panelDelayAfter.Size = new Size(316, 41);
            panelDelayAfter.TabIndex = 9;
            // 
            // txtDelayAfter
            // 
            txtDelayAfter.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtDelayAfter.Location = new Point(4, 5);
            txtDelayAfter.Margin = new Padding(4, 5, 4, 5);
            txtDelayAfter.MinimumSize = new Size(1, 16);
            txtDelayAfter.Name = "txtDelayAfter";
            txtDelayAfter.Padding = new Padding(5);
            txtDelayAfter.ShowText = false;
            txtDelayAfter.Size = new Size(80, 29);
            txtDelayAfter.TabIndex = 0;
            txtDelayAfter.Text = "0";
            txtDelayAfter.TextAlignment = ContentAlignment.MiddleLeft;
            txtDelayAfter.Watermark = "";
            // 
            // lblDelayAfterMs
            // 
            lblDelayAfterMs.AutoSize = true;
            lblDelayAfterMs.Location = new Point(91, 0);
            lblDelayAfterMs.Name = "lblDelayAfterMs";
            lblDelayAfterMs.Padding = new Padding(0, 8, 0, 0);
            lblDelayAfterMs.Size = new Size(28, 28);
            lblDelayAfterMs.TabIndex = 1;
            lblDelayAfterMs.Text = "ms";
            // 
            // lblFailureStrategy
            // 
            lblFailureStrategy.Dock = DockStyle.Fill;
            lblFailureStrategy.Font = new Font("微软雅黑", 9F);
            lblFailureStrategy.Location = new Point(3, 127);
            lblFailureStrategy.Name = "lblFailureStrategy";
            lblFailureStrategy.Size = new Size(94, 42);
            lblFailureStrategy.TabIndex = 10;
            lblFailureStrategy.Text = "失败处理:";
            lblFailureStrategy.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboFailureStrategy
            // 
            cboFailureStrategy.DataSource = null;
            cboFailureStrategy.Dock = DockStyle.Fill;
            cboFailureStrategy.DropDownStyle = UIDropDownStyle.DropDownList;
            cboFailureStrategy.FillColor = Color.White;
            cboFailureStrategy.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboFailureStrategy.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboFailureStrategy.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboFailureStrategy.Location = new Point(104, 132);
            cboFailureStrategy.Margin = new Padding(4, 5, 4, 5);
            cboFailureStrategy.MinimumSize = new Size(63, 0);
            cboFailureStrategy.Name = "cboFailureStrategy";
            cboFailureStrategy.Padding = new Padding(0, 0, 30, 2);
            cboFailureStrategy.Size = new Size(314, 32);
            cboFailureStrategy.SymbolSize = 24;
            cboFailureStrategy.TabIndex = 11;
            cboFailureStrategy.TextAlignment = ContentAlignment.MiddleLeft;
            cboFailureStrategy.Watermark = "";
            // 
            // lblJumpStep
            // 
            lblJumpStep.Dock = DockStyle.Fill;
            lblJumpStep.Font = new Font("微软雅黑", 9F);
            lblJumpStep.Location = new Point(425, 127);
            lblJumpStep.Name = "lblJumpStep";
            lblJumpStep.Size = new Size(94, 42);
            lblJumpStep.TabIndex = 12;
            lblJumpStep.Text = "跳转步骤:";
            lblJumpStep.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtJumpStep
            // 
            txtJumpStep.Dock = DockStyle.Fill;
            txtJumpStep.Enabled = false;
            txtJumpStep.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtJumpStep.Location = new Point(526, 132);
            txtJumpStep.Margin = new Padding(4, 5, 4, 5);
            txtJumpStep.MinimumSize = new Size(1, 16);
            txtJumpStep.Name = "txtJumpStep";
            txtJumpStep.Padding = new Padding(5);
            txtJumpStep.ShowText = false;
            txtJumpStep.Size = new Size(314, 32);
            txtJumpStep.TabIndex = 13;
            txtJumpStep.Text = "0";
            txtJumpStep.TextAlignment = ContentAlignment.MiddleLeft;
            txtJumpStep.Watermark = "";
            // 
            // lblLogging
            // 
            lblLogging.Dock = DockStyle.Fill;
            lblLogging.Font = new Font("微软雅黑", 9F);
            lblLogging.Location = new Point(3, 169);
            lblLogging.Name = "lblLogging";
            lblLogging.Size = new Size(94, 31);
            lblLogging.TabIndex = 14;
            lblLogging.Text = "记录日志:";
            lblLogging.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkEnableLogging
            // 
            chkEnableLogging.Checked = true;
            chkEnableLogging.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            chkEnableLogging.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnableLogging.Location = new Point(103, 172);
            chkEnableLogging.MinimumSize = new Size(1, 1);
            chkEnableLogging.Name = "chkEnableLogging";
            chkEnableLogging.Size = new Size(150, 25);
            chkEnableLogging.TabIndex = 15;
            chkEnableLogging.Text = "启用通讯日志";
            // 
            // lblCondition
            // 
            lblCondition.Dock = DockStyle.Fill;
            lblCondition.Font = new Font("微软雅黑", 9F);
            lblCondition.Location = new Point(3, 200);
            lblCondition.Name = "lblCondition";
            lblCondition.Size = new Size(94, 40);
            lblCondition.TabIndex = 16;
            lblCondition.Text = "执行条件:";
            lblCondition.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtExecuteCondition
            // 
            layoutAdvanced.SetColumnSpan(txtExecuteCondition, 3);
            txtExecuteCondition.Dock = DockStyle.Fill;
            txtExecuteCondition.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtExecuteCondition.Location = new Point(104, 205);
            txtExecuteCondition.Margin = new Padding(4, 5, 4, 5);
            txtExecuteCondition.MinimumSize = new Size(1, 16);
            txtExecuteCondition.Name = "txtExecuteCondition";
            txtExecuteCondition.Padding = new Padding(5);
            txtExecuteCondition.ShowText = false;
            txtExecuteCondition.Size = new Size(736, 30);
            txtExecuteCondition.TabIndex = 17;
            txtExecuteCondition.TextAlignment = ContentAlignment.MiddleLeft;
            txtExecuteCondition.Watermark = "为空时总是执行，如: {Var1} > 0";
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnOk);
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Location = new Point(13, 688);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(844, 44);
            panelButtons.TabIndex = 1;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Right;
            btnOk.FillColor = Color.FromArgb(0, 150, 136);
            btnOk.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOk.Location = new Point(604, 5);
            btnOk.MinimumSize = new Size(1, 1);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 35);
            btnOk.Symbol = 61452;
            btnOk.TabIndex = 0;
            btnOk.Text = "确定";
            btnOk.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Right;
            btnCancel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Location = new Point(710, 5);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // Form_InstrumentCommunication
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(870, 780);
            Controls.Add(mainPanel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_InstrumentCommunication";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "仪器通讯配置";
            ZoomScaleRect = new Rectangle(15, 15, 730, 650);
            mainPanel.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabBasic.ResumeLayout(false);
            layoutBasic.ResumeLayout(false);
            panelInstrument.ResumeLayout(false);
            panelCustomCommand.ResumeLayout(false);
            panelCommandParams.ResumeLayout(false);
            tabResponse.ResumeLayout(false);
            layoutResponse.ResumeLayout(false);
            grpParseRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvParseRules).EndInit();
            tabAdvanced.ResumeLayout(false);
            layoutAdvanced.ResumeLayout(false);
            panelTimeout.ResumeLayout(false);
            panelTimeout.PerformLayout();
            panelRetryInterval.ResumeLayout(false);
            panelRetryInterval.PerformLayout();
            panelDelayBefore.ResumeLayout(false);
            panelDelayBefore.PerformLayout();
            panelDelayAfter.ResumeLayout(false);
            panelDelayAfter.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region 控件声明

        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private Sunny.UI.UITabControl tabControl;
        private System.Windows.Forms.TabPage tabBasic;
        private System.Windows.Forms.TabPage tabResponse;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.Panel panelButtons;

        private System.Windows.Forms.TableLayoutPanel layoutBasic;
        private System.Windows.Forms.Label lblDescription;
        private Sunny.UI.UITextBox txtDescription;
        private System.Windows.Forms.Label lblInstrument;
        private System.Windows.Forms.FlowLayoutPanel panelInstrument;
        private Sunny.UI.UIComboBox cboInstrument;
        private Sunny.UI.UISymbolButton btnManageDrivers;
        private Sunny.UI.UISymbolButton btnTestConnection;
        private System.Windows.Forms.Label lblCommand;
        private Sunny.UI.UIComboBox cboCommand;
        private System.Windows.Forms.Label lblCustom;
        private Sunny.UI.UICheckBox chkCustomCommand;
        private System.Windows.Forms.Label lblCommandContent;
        private System.Windows.Forms.FlowLayoutPanel panelCustomCommand;
        private Sunny.UI.UITextBox txtCustomCommand;
        private Sunny.UI.UIComboBox cboCustomDataType;
        private System.Windows.Forms.Label lblParams;
        private Sunny.UI.UIPanel panelCommandParams;
        private System.Windows.Forms.FlowLayoutPanel flowParams;

        private System.Windows.Forms.TableLayoutPanel layoutResponse;
        private System.Windows.Forms.Label lblResponseVar;
        private Sunny.UI.UITextBox txtResponseVariable;
        private System.Windows.Forms.Label lblStatusVar;
        private Sunny.UI.UITextBox txtStatusVariable;
        private System.Windows.Forms.Label lblErrorVar;
        private Sunny.UI.UITextBox txtErrorVariable;
        private System.Windows.Forms.Label lblParseRules;
        private Sunny.UI.UIGroupBox grpParseRules;
        private Sunny.UI.UIDataGridView dgvParseRules;

        private System.Windows.Forms.TableLayoutPanel layoutAdvanced;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.FlowLayoutPanel panelTimeout;
        private Sunny.UI.UICheckBox chkOverrideTimeout;
        private Sunny.UI.UITextBox txtTimeout;
        private System.Windows.Forms.Label lblTimeoutMs;
        private System.Windows.Forms.Label lblRetryCount;
        private Sunny.UI.UITextBox txtRetryCount;
        private System.Windows.Forms.Label lblRetryInterval;
        private System.Windows.Forms.FlowLayoutPanel panelRetryInterval;
        private Sunny.UI.UITextBox txtRetryInterval;
        private System.Windows.Forms.Label lblRetryMs;
        private System.Windows.Forms.Label lblDelayBefore;
        private System.Windows.Forms.FlowLayoutPanel panelDelayBefore;
        private Sunny.UI.UITextBox txtDelayBefore;
        private System.Windows.Forms.Label lblDelayBeforeMs;
        private System.Windows.Forms.Label lblDelayAfter;
        private System.Windows.Forms.FlowLayoutPanel panelDelayAfter;
        private Sunny.UI.UITextBox txtDelayAfter;
        private System.Windows.Forms.Label lblDelayAfterMs;
        private System.Windows.Forms.Label lblFailureStrategy;
        private Sunny.UI.UIComboBox cboFailureStrategy;
        private System.Windows.Forms.Label lblJumpStep;
        private Sunny.UI.UITextBox txtJumpStep;
        private System.Windows.Forms.Label lblLogging;
        private Sunny.UI.UICheckBox chkEnableLogging;
        private System.Windows.Forms.Label lblCondition;
        private Sunny.UI.UITextBox txtExecuteCondition;

        private Sunny.UI.UISymbolButton btnOk;
        private Sunny.UI.UISymbolButton btnCancel;

        #endregion
    }
}
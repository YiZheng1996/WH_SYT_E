namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_WritePLC_ConfigDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.components = new System.ComponentModel.Container();
            this.grpBasicInfo = new System.Windows.Forms.GroupBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.grpPLCConfig = new System.Windows.Forms.GroupBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.lblDataType = new System.Windows.Forms.Label();
            this.cmbPLCAddress = new System.Windows.Forms.ComboBox();
            this.lblPLCAddress = new System.Windows.Forms.Label();
            this.cmbPLCModule = new System.Windows.Forms.ComboBox();
            this.lblPLCModule = new System.Windows.Forms.Label();
            this.grpDataSource = new System.Windows.Forms.GroupBox();
            this.pnlDataSourceContent = new System.Windows.Forms.Panel();
            this.pnlPLCRead = new System.Windows.Forms.Panel();
            this.chkEnableConversion = new System.Windows.Forms.CheckBox();
            this.txtConversionExpression = new System.Windows.Forms.TextBox();
            this.lblConversionExpression = new System.Windows.Forms.Label();
            this.btnTestRead = new System.Windows.Forms.Button();
            this.lblSourceConnectionStatus = new System.Windows.Forms.Label();
            this.cmbSourceDataType = new System.Windows.Forms.ComboBox();
            this.lblSourceDataType = new System.Windows.Forms.Label();
            this.cmbSourceAddress = new System.Windows.Forms.ComboBox();
            this.lblSourceAddress = new System.Windows.Forms.Label();
            this.cmbSourceModule = new System.Windows.Forms.ComboBox();
            this.lblSourceModule = new System.Windows.Forms.Label();
            this.pnlExpression = new System.Windows.Forms.Panel();
            this.lstAvailableVariables = new System.Windows.Forms.ListBox();
            this.lblAvailableVariables = new System.Windows.Forms.Label();
            this.txtExpressionPreview = new System.Windows.Forms.TextBox();
            this.lblExpressionPreview = new System.Windows.Forms.Label();
            this.btnInsertVariable = new System.Windows.Forms.Button();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.lblExpression = new System.Windows.Forms.Label();
            this.pnlVariableRef = new System.Windows.Forms.Panel();
            this.lblVariableInfo = new System.Windows.Forms.Label();
            this.txtVariableInfo = new System.Windows.Forms.TextBox();
            this.cmbVariable = new System.Windows.Forms.ComboBox();
            this.lblVariable = new System.Windows.Forms.Label();
            this.pnlFixedValue = new System.Windows.Forms.Panel();
            this.txtFixedValueHint = new System.Windows.Forms.TextBox();
            this.txtFixedValue = new System.Windows.Forms.TextBox();
            this.lblFixedValue = new System.Windows.Forms.Label();
            this.pnlDataSourceType = new System.Windows.Forms.Panel();
            this.rbPLCRead = new System.Windows.Forms.RadioButton();
            this.rbExpression = new System.Windows.Forms.RadioButton();
            this.rbVariableRef = new System.Windows.Forms.RadioButton();
            this.rbFixedValue = new System.Windows.Forms.RadioButton();
            this.grpAdvanced = new System.Windows.Forms.GroupBox();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.numRetryDelay = new System.Windows.Forms.NumericUpDown();
            this.lblRetryDelay = new System.Windows.Forms.Label();
            this.numRetryCount = new System.Windows.Forms.NumericUpDown();
            this.lblRetryCount = new System.Windows.Forms.Label();
            this.txtExecutionCondition = new System.Windows.Forms.TextBox();
            this.lblExecutionCondition = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.validationTimer = new System.Windows.Forms.Timer(this.components);
            this.grpBasicInfo.SuspendLayout();
            this.grpPLCConfig.SuspendLayout();
            this.grpDataSource.SuspendLayout();
            this.pnlDataSourceContent.SuspendLayout();
            this.pnlPLCRead.SuspendLayout();
            this.pnlExpression.SuspendLayout();
            this.pnlVariableRef.SuspendLayout();
            this.pnlFixedValue.SuspendLayout();
            this.pnlDataSourceType.SuspendLayout();
            this.grpAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryCount)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBasicInfo
            // 
            this.grpBasicInfo.Controls.Add(this.chkEnabled);
            this.grpBasicInfo.Controls.Add(this.txtDescription);
            this.grpBasicInfo.Controls.Add(this.lblDescription);
            this.grpBasicInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBasicInfo.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.grpBasicInfo.Location = new System.Drawing.Point(10, 10);
            this.grpBasicInfo.Name = "grpBasicInfo";
            this.grpBasicInfo.Padding = new System.Windows.Forms.Padding(10);
            this.grpBasicInfo.Size = new System.Drawing.Size(880, 100);
            this.grpBasicInfo.TabIndex = 0;
            this.grpBasicInfo.TabStop = false;
            this.grpBasicInfo.Text = "基本信息";
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.chkEnabled.Location = new System.Drawing.Point(100, 65);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(99, 21);
            this.chkEnabled.TabIndex = 2;
            this.chkEnabled.Text = "☑ 启用此配置项";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtDescription.Location = new System.Drawing.Point(100, 30);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(760, 23);
            this.txtDescription.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblDescription.Location = new System.Drawing.Point(20, 33);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(68, 17);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "描述(必填):";
            // 
            // grpPLCConfig
            // 
            this.grpPLCConfig.Controls.Add(this.btnTestConnection);
            this.grpPLCConfig.Controls.Add(this.lblConnectionStatus);
            this.grpPLCConfig.Controls.Add(this.cmbDataType);
            this.grpPLCConfig.Controls.Add(this.lblDataType);
            this.grpPLCConfig.Controls.Add(this.cmbPLCAddress);
            this.grpPLCConfig.Controls.Add(this.lblPLCAddress);
            this.grpPLCConfig.Controls.Add(this.cmbPLCModule);
            this.grpPLCConfig.Controls.Add(this.lblPLCModule);
            this.grpPLCConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpPLCConfig.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.grpPLCConfig.Location = new System.Drawing.Point(10, 110);
            this.grpPLCConfig.Name = "grpPLCConfig";
            this.grpPLCConfig.Padding = new System.Windows.Forms.Padding(10);
            this.grpPLCConfig.Size = new System.Drawing.Size(880, 180);
            this.grpPLCConfig.TabIndex = 1;
            this.grpPLCConfig.TabStop = false;
            this.grpPLCConfig.Text = "PLC配置";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnTestConnection.FlatAppearance.BorderSize = 0;
            this.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConnection.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.btnTestConnection.ForeColor = System.Drawing.Color.White;
            this.btnTestConnection.Location = new System.Drawing.Point(100, 135);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(100, 30);
            this.btnTestConnection.TabIndex = 7;
            this.btnTestConnection.Text = "🔌 测试连接";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblConnectionStatus.Location = new System.Drawing.Point(210, 142);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(80, 17);
            this.lblConnectionStatus.TabIndex = 6;
            this.lblConnectionStatus.Text = "未测试连接";
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(100, 100);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(250, 25);
            this.cmbDataType.TabIndex = 5;
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblDataType.Location = new System.Drawing.Point(20, 103);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(68, 17);
            this.lblDataType.TabIndex = 4;
            this.lblDataType.Text = "数据类型:";
            // 
            // cmbPLCAddress
            // 
            this.cmbPLCAddress.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cmbPLCAddress.FormattingEnabled = true;
            this.cmbPLCAddress.Location = new System.Drawing.Point(100, 65);
            this.cmbPLCAddress.Name = "cmbPLCAddress";
            this.cmbPLCAddress.Size = new System.Drawing.Size(250, 25);
            this.cmbPLCAddress.TabIndex = 3;
            // 
            // lblPLCAddress
            // 
            this.lblPLCAddress.AutoSize = true;
            this.lblPLCAddress.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblPLCAddress.Location = new System.Drawing.Point(20, 68);
            this.lblPLCAddress.Name = "lblPLCAddress";
            this.lblPLCAddress.Size = new System.Drawing.Size(68, 17);
            this.lblPLCAddress.TabIndex = 2;
            this.lblPLCAddress.Text = "点位地址:";
            // 
            // cmbPLCModule
            // 
            this.cmbPLCModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPLCModule.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cmbPLCModule.FormattingEnabled = true;
            this.cmbPLCModule.Location = new System.Drawing.Point(100, 30);
            this.cmbPLCModule.Name = "cmbPLCModule";
            this.cmbPLCModule.Size = new System.Drawing.Size(250, 25);
            this.cmbPLCModule.TabIndex = 1;
            // 
            // lblPLCModule
            // 
            this.lblPLCModule.AutoSize = true;
            this.lblPLCModule.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblPLCModule.Location = new System.Drawing.Point(20, 33);
            this.lblPLCModule.Name = "lblPLCModule";
            this.lblPLCModule.Size = new System.Drawing.Size(68, 17);
            this.lblPLCModule.TabIndex = 0;
            this.lblPLCModule.Text = "PLC模块:";
            // 
            // grpDataSource
            // 
            this.grpDataSource.Controls.Add(this.pnlDataSourceContent);
            this.grpDataSource.Controls.Add(this.pnlDataSourceType);
            this.grpDataSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDataSource.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.grpDataSource.Location = new System.Drawing.Point(10, 290);
            this.grpDataSource.Name = "grpDataSource";
            this.grpDataSource.Padding = new System.Windows.Forms.Padding(10);
            this.grpDataSource.Size = new System.Drawing.Size(880, 400);
            this.grpDataSource.TabIndex = 2;
            this.grpDataSource.TabStop = false;
            this.grpDataSource.Text = "写入数据源";
            // 
            // pnlDataSourceContent
            // 
            this.pnlDataSourceContent.Controls.Add(this.pnlPLCRead);
            this.pnlDataSourceContent.Controls.Add(this.pnlExpression);
            this.pnlDataSourceContent.Controls.Add(this.pnlVariableRef);
            this.pnlDataSourceContent.Controls.Add(this.pnlFixedValue);
            this.pnlDataSourceContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDataSourceContent.Location = new System.Drawing.Point(10, 65);
            this.pnlDataSourceContent.Name = "pnlDataSourceContent";
            this.pnlDataSourceContent.Size = new System.Drawing.Size(860, 325);
            this.pnlDataSourceContent.TabIndex = 1;
            // 
            // pnlPLCRead
            // 
            this.pnlPLCRead.Controls.Add(this.chkEnableConversion);
            this.pnlPLCRead.Controls.Add(this.txtConversionExpression);
            this.pnlPLCRead.Controls.Add(this.lblConversionExpression);
            this.pnlPLCRead.Controls.Add(this.btnTestRead);
            this.pnlPLCRead.Controls.Add(this.lblSourceConnectionStatus);
            this.pnlPLCRead.Controls.Add(this.cmbSourceDataType);
            this.pnlPLCRead.Controls.Add(this.lblSourceDataType);
            this.pnlPLCRead.Controls.Add(this.cmbSourceAddress);
            this.pnlPLCRead.Controls.Add(this.lblSourceAddress);
            this.pnlPLCRead.Controls.Add(this.cmbSourceModule);
            this.pnlPLCRead.Controls.Add(this.lblSourceModule);
            this.pnlPLCRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPLCRead.Location = new System.Drawing.Point(0, 0);
            this.pnlPLCRead.Name = "pnlPLCRead";
            this.pnlPLCRead.Size = new System.Drawing.Size(860, 325);
            this.pnlPLCRead.TabIndex = 3;
            this.pnlPLCRead.Visible = false;
            // 
            // chkEnableConversion
            // 
            this.chkEnableConversion.AutoSize = true;
            this.chkEnableConversion.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.chkEnableConversion.Location = new System.Drawing.Point(90, 150);
            this.chkEnableConversion.Name = "chkEnableConversion";
            this.chkEnableConversion.Size = new System.Drawing.Size(111, 21);
            this.chkEnableConversion.TabIndex = 10;
            this.chkEnableConversion.Text = "☐ 启用数据转换";
            this.chkEnableConversion.UseVisualStyleBackColor = true;
            // 
            // txtConversionExpression
            // 
            this.txtConversionExpression.Enabled = false;
            this.txtConversionExpression.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtConversionExpression.Location = new System.Drawing.Point(90, 185);
            this.txtConversionExpression.Name = "txtConversionExpression";
            this.txtConversionExpression.Size = new System.Drawing.Size(700, 23);
            this.txtConversionExpression.TabIndex = 9;
            // 
            // lblConversionExpression
            // 
            this.lblConversionExpression.AutoSize = true;
            this.lblConversionExpression.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblConversionExpression.Location = new System.Drawing.Point(10, 188);
            this.lblConversionExpression.Name = "lblConversionExpression";
            this.lblConversionExpression.Size = new System.Drawing.Size(68, 17);
            this.lblConversionExpression.TabIndex = 8;
            this.lblConversionExpression.Text = "转换公式:";
            // 
            // btnTestRead
            // 
            this.btnTestRead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnTestRead.FlatAppearance.BorderSize = 0;
            this.btnTestRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestRead.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.btnTestRead.ForeColor = System.Drawing.Color.White;
            this.btnTestRead.Location = new System.Drawing.Point(90, 110);
            this.btnTestRead.Name = "btnTestRead";
            this.btnTestRead.Size = new System.Drawing.Size(100, 30);
            this.btnTestRead.TabIndex = 7;
            this.btnTestRead.Text = "📖 测试读取";
            this.btnTestRead.UseVisualStyleBackColor = false;
            // 
            // lblSourceConnectionStatus
            // 
            this.lblSourceConnectionStatus.AutoSize = true;
            this.lblSourceConnectionStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblSourceConnectionStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblSourceConnectionStatus.Location = new System.Drawing.Point(200, 117);
            this.lblSourceConnectionStatus.Name = "lblSourceConnectionStatus";
            this.lblSourceConnectionStatus.Size = new System.Drawing.Size(80, 17);
            this.lblSourceConnectionStatus.TabIndex = 6;
            this.lblSourceConnectionStatus.Text = "未测试连接";
            // 
            // cmbSourceDataType
            // 
            this.cmbSourceDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceDataType.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cmbSourceDataType.FormattingEnabled = true;
            this.cmbSourceDataType.Location = new System.Drawing.Point(90, 75);
            this.cmbSourceDataType.Name = "cmbSourceDataType";
            this.cmbSourceDataType.Size = new System.Drawing.Size(250, 25);
            this.cmbSourceDataType.TabIndex = 5;
            // 
            // lblSourceDataType
            // 
            this.lblSourceDataType.AutoSize = true;
            this.lblSourceDataType.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblSourceDataType.Location = new System.Drawing.Point(10, 78);
            this.lblSourceDataType.Name = "lblSourceDataType";
            this.lblSourceDataType.Size = new System.Drawing.Size(68, 17);
            this.lblSourceDataType.TabIndex = 4;
            this.lblSourceDataType.Text = "数据类型:";
            // 
            // cmbSourceAddress
            // 
            this.cmbSourceAddress.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cmbSourceAddress.FormattingEnabled = true;
            this.cmbSourceAddress.Location = new System.Drawing.Point(90, 40);
            this.cmbSourceAddress.Name = "cmbSourceAddress";
            this.cmbSourceAddress.Size = new System.Drawing.Size(250, 25);
            this.cmbSourceAddress.TabIndex = 3;
            // 
            // lblSourceAddress
            // 
            this.lblSourceAddress.AutoSize = true;
            this.lblSourceAddress.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblSourceAddress.Location = new System.Drawing.Point(10, 43);
            this.lblSourceAddress.Name = "lblSourceAddress";
            this.lblSourceAddress.Size = new System.Drawing.Size(68, 17);
            this.lblSourceAddress.TabIndex = 2;
            this.lblSourceAddress.Text = "点位地址:";
            // 
            // cmbSourceModule
            // 
            this.cmbSourceModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceModule.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cmbSourceModule.FormattingEnabled = true;
            this.cmbSourceModule.Location = new System.Drawing.Point(90, 5);
            this.cmbSourceModule.Name = "cmbSourceModule";
            this.cmbSourceModule.Size = new System.Drawing.Size(250, 25);
            this.cmbSourceModule.TabIndex = 1;
            // 
            // lblSourceModule
            // 
            this.lblSourceModule.AutoSize = true;
            this.lblSourceModule.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblSourceModule.Location = new System.Drawing.Point(10, 8);
            this.lblSourceModule.Name = "lblSourceModule";
            this.lblSourceModule.Size = new System.Drawing.Size(56, 17);
            this.lblSourceModule.TabIndex = 0;
            this.lblSourceModule.Text = "源模块:";
            // 
            // pnlExpression
            // 
            this.pnlExpression.Controls.Add(this.lstAvailableVariables);
            this.pnlExpression.Controls.Add(this.lblAvailableVariables);
            this.pnlExpression.Controls.Add(this.txtExpressionPreview);
            this.pnlExpression.Controls.Add(this.lblExpressionPreview);
            this.pnlExpression.Controls.Add(this.btnInsertVariable);
            this.pnlExpression.Controls.Add(this.txtExpression);
            this.pnlExpression.Controls.Add(this.lblExpression);
            this.pnlExpression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlExpression.Location = new System.Drawing.Point(0, 0);
            this.pnlExpression.Name = "pnlExpression";
            this.pnlExpression.Size = new System.Drawing.Size(860, 325);
            this.pnlExpression.TabIndex = 2;
            this.pnlExpression.Visible = false;
            // 
            // lstAvailableVariables
            // 
            this.lstAvailableVariables.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lstAvailableVariables.FormattingEnabled = true;
            this.lstAvailableVariables.ItemHeight = 17;
            this.lstAvailableVariables.Location = new System.Drawing.Point(90, 185);
            this.lstAvailableVariables.Name = "lstAvailableVariables";
            this.lstAvailableVariables.Size = new System.Drawing.Size(700, 123);
            this.lstAvailableVariables.TabIndex = 6;
            // 
            // lblAvailableVariables
            // 
            this.lblAvailableVariables.AutoSize = true;
            this.lblAvailableVariables.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblAvailableVariables.Location = new System.Drawing.Point(10, 185);
            this.lblAvailableVariables.Name = "lblAvailableVariables";
            this.lblAvailableVariables.Size = new System.Drawing.Size(68, 17);
            this.lblAvailableVariables.TabIndex = 5;
            this.lblAvailableVariables.Text = "可用变量:";
            // 
            // txtExpressionPreview
            // 
            this.txtExpressionPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.txtExpressionPreview.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtExpressionPreview.Location = new System.Drawing.Point(90, 105);
            this.txtExpressionPreview.Multiline = true;
            this.txtExpressionPreview.Name = "txtExpressionPreview";
            this.txtExpressionPreview.ReadOnly = true;
            this.txtExpressionPreview.Size = new System.Drawing.Size(700, 70);
            this.txtExpressionPreview.TabIndex = 4;
            // 
            // lblExpressionPreview
            // 
            this.lblExpressionPreview.AutoSize = true;
            this.lblExpressionPreview.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblExpressionPreview.Location = new System.Drawing.Point(10, 108);
            this.lblExpressionPreview.Name = "lblExpressionPreview";
            this.lblExpressionPreview.Size = new System.Drawing.Size(68, 17);
            this.lblExpressionPreview.TabIndex = 3;
            this.lblExpressionPreview.Text = "实时预览:";
            // 
            // btnInsertVariable
            // 
            this.btnInsertVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnInsertVariable.FlatAppearance.BorderSize = 0;
            this.btnInsertVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertVariable.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.btnInsertVariable.ForeColor = System.Drawing.Color.White;
            this.btnInsertVariable.Location = new System.Drawing.Point(90, 65);
            this.btnInsertVariable.Name = "btnInsertVariable";
            this.btnInsertVariable.Size = new System.Drawing.Size(120, 30);
            this.btnInsertVariable.TabIndex = 2;
            this.btnInsertVariable.Text = "📋 插入变量";
            this.btnInsertVariable.UseVisualStyleBackColor = false;
            // 
            // txtExpression
            // 
            this.txtExpression.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtExpression.Location = new System.Drawing.Point(90, 5);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(700, 50);
            this.txtExpression.TabIndex = 1;
            // 
            // lblExpression
            // 
            this.lblExpression.AutoSize = true;
            this.lblExpression.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblExpression.Location = new System.Drawing.Point(10, 8);
            this.lblExpression.Name = "lblExpression";
            this.lblExpression.Size = new System.Drawing.Size(56, 17);
            this.lblExpression.TabIndex = 0;
            this.lblExpression.Text = "表达式:";
            // 
            // pnlVariableRef
            // 
            this.pnlVariableRef.Controls.Add(this.lblVariableInfo);
            this.pnlVariableRef.Controls.Add(this.txtVariableInfo);
            this.pnlVariableRef.Controls.Add(this.cmbVariable);
            this.pnlVariableRef.Controls.Add(this.lblVariable);
            this.pnlVariableRef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVariableRef.Location = new System.Drawing.Point(0, 0);
            this.pnlVariableRef.Name = "pnlVariableRef";
            this.pnlVariableRef.Size = new System.Drawing.Size(860, 325);
            this.pnlVariableRef.TabIndex = 1;
            this.pnlVariableRef.Visible = false;
            // 
            // lblVariableInfo
            // 
            this.lblVariableInfo.AutoSize = true;
            this.lblVariableInfo.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblVariableInfo.Location = new System.Drawing.Point(10, 43);
            this.lblVariableInfo.Name = "lblVariableInfo";
            this.lblVariableInfo.Size = new System.Drawing.Size(80, 17);
            this.lblVariableInfo.TabIndex = 3;
            this.lblVariableInfo.Text = "变量详细信息:";
            // 
            // txtVariableInfo
            // 
            this.txtVariableInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.txtVariableInfo.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtVariableInfo.Location = new System.Drawing.Point(90, 40);
            this.txtVariableInfo.Multiline = true;
            this.txtVariableInfo.Name = "txtVariableInfo";
            this.txtVariableInfo.ReadOnly = true;
            this.txtVariableInfo.Size = new System.Drawing.Size(700, 150);
            this.txtVariableInfo.TabIndex = 2;
            // 
            // cmbVariable
            // 
            this.cmbVariable.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cmbVariable.FormattingEnabled = true;
            this.cmbVariable.Location = new System.Drawing.Point(90, 5);
            this.cmbVariable.Name = "cmbVariable";
            this.cmbVariable.Size = new System.Drawing.Size(250, 25);
            this.cmbVariable.TabIndex = 1;
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblVariable.Location = new System.Drawing.Point(10, 8);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(68, 17);
            this.lblVariable.TabIndex = 0;
            this.lblVariable.Text = "选择变量:";
            // 
            // pnlFixedValue
            // 
            this.pnlFixedValue.Controls.Add(this.txtFixedValueHint);
            this.pnlFixedValue.Controls.Add(this.txtFixedValue);
            this.pnlFixedValue.Controls.Add(this.lblFixedValue);
            this.pnlFixedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFixedValue.Location = new System.Drawing.Point(0, 0);
            this.pnlFixedValue.Name = "pnlFixedValue";
            this.pnlFixedValue.Size = new System.Drawing.Size(860, 325);
            this.pnlFixedValue.TabIndex = 0;
            // 
            // txtFixedValueHint
            // 
            this.txtFixedValueHint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(205)))));
            this.txtFixedValueHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFixedValueHint.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtFixedValueHint.Location = new System.Drawing.Point(90, 40);
            this.txtFixedValueHint.Multiline = true;
            this.txtFixedValueHint.Name = "txtFixedValueHint";
            this.txtFixedValueHint.ReadOnly = true;
            this.txtFixedValueHint.Size = new System.Drawing.Size(700, 120);
            this.txtFixedValueHint.TabIndex = 2;
            this.txtFixedValueHint.Text = "💡 不同数据类型的输入示例:\r\n• Bool: True/False 或 1/0\r\n• Int: -32768~32767 的整数\r\n• Real: 浮点数，如 25.5\r\n• String: 文本内容";
            // 
            // txtFixedValue
            // 
            this.txtFixedValue.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtFixedValue.Location = new System.Drawing.Point(90, 5);
            this.txtFixedValue.Name = "txtFixedValue";
            this.txtFixedValue.Size = new System.Drawing.Size(700, 23);
            this.txtFixedValue.TabIndex = 1;
            // 
            // lblFixedValue
            // 
            this.lblFixedValue.AutoSize = true;
            this.lblFixedValue.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblFixedValue.Location = new System.Drawing.Point(10, 8);
            this.lblFixedValue.Name = "lblFixedValue";
            this.lblFixedValue.Size = new System.Drawing.Size(56, 17);
            this.lblFixedValue.TabIndex = 0;
            this.lblFixedValue.Text = "写入值:";
            // 
            // pnlDataSourceType
            // 
            this.pnlDataSourceType.Controls.Add(this.rbPLCRead);
            this.pnlDataSourceType.Controls.Add(this.rbExpression);
            this.pnlDataSourceType.Controls.Add(this.rbVariableRef);
            this.pnlDataSourceType.Controls.Add(this.rbFixedValue);
            this.pnlDataSourceType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDataSourceType.Location = new System.Drawing.Point(10, 25);
            this.pnlDataSourceType.Name = "pnlDataSourceType";
            this.pnlDataSourceType.Size = new System.Drawing.Size(860, 40);
            this.pnlDataSourceType.TabIndex = 0;
            // 
            // rbPLCRead
            // 
            this.rbPLCRead.AutoSize = true;
            this.rbPLCRead.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.rbPLCRead.Location = new System.Drawing.Point(330, 10);
            this.rbPLCRead.Name = "rbPLCRead";
            this.rbPLCRead.Size = new System.Drawing.Size(86, 21);
            this.rbPLCRead.TabIndex = 3;
            this.rbPLCRead.Text = "○ PLC读取";
            this.rbPLCRead.UseVisualStyleBackColor = true;
            // 
            // rbExpression
            // 
            this.rbExpression.AutoSize = true;
            this.rbExpression.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.rbExpression.Location = new System.Drawing.Point(230, 10);
            this.rbExpression.Name = "rbExpression";
            this.rbExpression.Size = new System.Drawing.Size(74, 21);
            this.rbExpression.TabIndex = 2;
            this.rbExpression.Text = "○ 表达式";
            this.rbExpression.UseVisualStyleBackColor = true;
            // 
            // rbVariableRef
            // 
            this.rbVariableRef.AutoSize = true;
            this.rbVariableRef.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.rbVariableRef.Location = new System.Drawing.Point(120, 10);
            this.rbVariableRef.Name = "rbVariableRef";
            this.rbVariableRef.Size = new System.Drawing.Size(86, 21);
            this.rbVariableRef.TabIndex = 1;
            this.rbVariableRef.Text = "○ 变量引用";
            this.rbVariableRef.UseVisualStyleBackColor = true;
            // 
            // rbFixedValue
            // 
            this.rbFixedValue.AutoSize = true;
            this.rbFixedValue.Checked = true;
            this.rbFixedValue.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.rbFixedValue.Location = new System.Drawing.Point(20, 10);
            this.rbFixedValue.Name = "rbFixedValue";
            this.rbFixedValue.Size = new System.Drawing.Size(74, 21);
            this.rbFixedValue.TabIndex = 0;
            this.rbFixedValue.TabStop = true;
            this.rbFixedValue.Text = "⦿ 固定值";
            this.rbFixedValue.UseVisualStyleBackColor = true;
            // 
            // grpAdvanced
            // 
            this.grpAdvanced.Controls.Add(this.numTimeout);
            this.grpAdvanced.Controls.Add(this.lblTimeout);
            this.grpAdvanced.Controls.Add(this.numRetryDelay);
            this.grpAdvanced.Controls.Add(this.lblRetryDelay);
            this.grpAdvanced.Controls.Add(this.numRetryCount);
            this.grpAdvanced.Controls.Add(this.lblRetryCount);
            this.grpAdvanced.Controls.Add(this.txtExecutionCondition);
            this.grpAdvanced.Controls.Add(this.lblExecutionCondition);
            this.grpAdvanced.Controls.Add(this.txtRemarks);
            this.grpAdvanced.Controls.Add(this.lblRemarks);
            this.grpAdvanced.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAdvanced.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.grpAdvanced.Location = new System.Drawing.Point(10, 690);
            this.grpAdvanced.Name = "grpAdvanced";
            this.grpAdvanced.Padding = new System.Windows.Forms.Padding(10);
            this.grpAdvanced.Size = new System.Drawing.Size(880, 240);
            this.grpAdvanced.TabIndex = 3;
            this.grpAdvanced.TabStop = false;
            this.grpAdvanced.Text = "高级选项 (可选)";
            // 
            // numTimeout
            // 
            this.numTimeout.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.numTimeout.Location = new System.Drawing.Point(580, 115);
            this.numTimeout.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(120, 23);
            this.numTimeout.TabIndex = 9;
            this.numTimeout.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblTimeout.Location = new System.Drawing.Point(480, 118);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(92, 17);
            this.lblTimeout.TabIndex = 8;
            this.lblTimeout.Text = "超时时间(ms):";
            // 
            // numRetryDelay
            // 
            this.numRetryDelay.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.numRetryDelay.Location = new System.Drawing.Point(340, 115);
            this.numRetryDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRetryDelay.Name = "numRetryDelay";
            this.numRetryDelay.Size = new System.Drawing.Size(120, 23);
            this.numRetryDelay.TabIndex = 7;
            this.numRetryDelay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblRetryDelay
            // 
            this.lblRetryDelay.AutoSize = true;
            this.lblRetryDelay.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblRetryDelay.Location = new System.Drawing.Point(240, 118);
            this.lblRetryDelay.Name = "lblRetryDelay";
            this.lblRetryDelay.Size = new System.Drawing.Size(92, 17);
            this.lblRetryDelay.TabIndex = 6;
            this.lblRetryDelay.Text = "重试延迟(ms):";
            // 
            // numRetryCount
            // 
            this.numRetryCount.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.numRetryCount.Location = new System.Drawing.Point(100, 115);
            this.numRetryCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRetryCount.Name = "numRetryCount";
            this.numRetryCount.Size = new System.Drawing.Size(120, 23);
            this.numRetryCount.TabIndex = 5;
            this.numRetryCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblRetryCount
            // 
            this.lblRetryCount.AutoSize = true;
            this.lblRetryCount.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblRetryCount.Location = new System.Drawing.Point(20, 118);
            this.lblRetryCount.Name = "lblRetryCount";
            this.lblRetryCount.Size = new System.Drawing.Size(68, 17);
            this.lblRetryCount.TabIndex = 4;
            this.lblRetryCount.Text = "失败重试:";
            // 
            // txtExecutionCondition
            // 
            this.txtExecutionCondition.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtExecutionCondition.Location = new System.Drawing.Point(100, 30);
            this.txtExecutionCondition.Multiline = true;
            this.txtExecutionCondition.Name = "txtExecutionCondition";
            this.txtExecutionCondition.Size = new System.Drawing.Size(760, 50);
            this.txtExecutionCondition.TabIndex = 3;
            // 
            // lblExecutionCondition
            // 
            this.lblExecutionCondition.AutoSize = true;
            this.lblExecutionCondition.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblExecutionCondition.Location = new System.Drawing.Point(20, 33);
            this.lblExecutionCondition.Name = "lblExecutionCondition";
            this.lblExecutionCondition.Size = new System.Drawing.Size(68, 17);
            this.lblExecutionCondition.TabIndex = 2;
            this.lblExecutionCondition.Text = "执行条件:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtRemarks.Location = new System.Drawing.Point(100, 155);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(760, 70);
            this.txtRemarks.TabIndex = 1;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblRemarks.Location = new System.Drawing.Point(20, 158);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(44, 17);
            this.lblRemarks.TabIndex = 0;
            this.lblRemarks.Text = "备注:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnOK);
            this.pnlButtons.Controls.Add(this.btnHelp);
            this.pnlButtons.Controls.Add(this.btnPreview);
            this.pnlButtons.Controls.Add(this.btnTest);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(10, 940);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlButtons.Size = new System.Drawing.Size(880, 60);
            this.pnlButtons.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(770, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "✗ 取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(660, 13);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 35);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "✓ 确定";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.btnHelp.ForeColor = System.Drawing.Color.White;
            this.btnHelp.Location = new System.Drawing.Point(230, 13);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(100, 35);
            this.btnHelp.TabIndex = 2;
            this.btnHelp.Text = "❓ 帮助";
            this.btnHelp.UseVisualStyleBackColor = false;
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnPreview.FlatAppearance.BorderSize = 0;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Location = new System.Drawing.Point(120, 13);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(100, 35);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "👁️ 预览";
            this.btnPreview.UseVisualStyleBackColor = false;
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnTest.FlatAppearance.BorderSize = 0;
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.btnTest.ForeColor = System.Drawing.Color.White;
            this.btnTest.Location = new System.Drawing.Point(10, 13);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(100, 35);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "🧪 测试";
            this.btnTest.UseVisualStyleBackColor = false;
            // 
            // validationTimer
            // 
            this.validationTimer.Interval = 500;
            // 
            // Form_WritePLC_ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(900, 1000);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpAdvanced);
            this.Controls.Add(this.grpDataSource);
            this.Controls.Add(this.grpPLCConfig);
            this.Controls.Add(this.grpBasicInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_WritePLC_ConfigDialog";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PLC写入配置 - 详细设置";
            this.grpBasicInfo.ResumeLayout(false);
            this.grpBasicInfo.PerformLayout();
            this.grpPLCConfig.ResumeLayout(false);
            this.grpPLCConfig.PerformLayout();
            this.grpDataSource.ResumeLayout(false);
            this.pnlDataSourceContent.ResumeLayout(false);
            this.pnlPLCRead.ResumeLayout(false);
            this.pnlPLCRead.PerformLayout();
            this.pnlExpression.ResumeLayout(false);
            this.pnlExpression.PerformLayout();
            this.pnlVariableRef.ResumeLayout(false);
            this.pnlVariableRef.PerformLayout();
            this.pnlFixedValue.ResumeLayout(false);
            this.pnlFixedValue.PerformLayout();
            this.pnlDataSourceType.ResumeLayout(false);
            this.pnlDataSourceType.PerformLayout();
            this.grpAdvanced.ResumeLayout(false);
            this.grpAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryCount)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBasicInfo;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.GroupBox grpPLCConfig;
        private System.Windows.Forms.ComboBox cmbPLCModule;
        private System.Windows.Forms.Label lblPLCModule;
        private System.Windows.Forms.ComboBox cmbPLCAddress;
        private System.Windows.Forms.Label lblPLCAddress;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.GroupBox grpDataSource;
        private System.Windows.Forms.Panel pnlDataSourceType;
        private System.Windows.Forms.RadioButton rbFixedValue;
        private System.Windows.Forms.RadioButton rbVariableRef;
        private System.Windows.Forms.RadioButton rbExpression;
        private System.Windows.Forms.RadioButton rbPLCRead;
        private System.Windows.Forms.Panel pnlDataSourceContent;
        private System.Windows.Forms.Panel pnlFixedValue;
        private System.Windows.Forms.TextBox txtFixedValue;
        private System.Windows.Forms.Label lblFixedValue;
        private System.Windows.Forms.TextBox txtFixedValueHint;
        private System.Windows.Forms.Panel pnlVariableRef;
        private System.Windows.Forms.ComboBox cmbVariable;
        private System.Windows.Forms.Label lblVariable;
        private System.Windows.Forms.TextBox txtVariableInfo;
        private System.Windows.Forms.Label lblVariableInfo;
        private System.Windows.Forms.Panel pnlExpression;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Label lblExpression;
        private System.Windows.Forms.Button btnInsertVariable;
        private System.Windows.Forms.TextBox txtExpressionPreview;
        private System.Windows.Forms.Label lblExpressionPreview;
        private System.Windows.Forms.ListBox lstAvailableVariables;
        private System.Windows.Forms.Label lblAvailableVariables;
        private System.Windows.Forms.Panel pnlPLCRead;
        private System.Windows.Forms.ComboBox cmbSourceModule;
        private System.Windows.Forms.Label lblSourceModule;
        private System.Windows.Forms.ComboBox cmbSourceAddress;
        private System.Windows.Forms.Label lblSourceAddress;
        private System.Windows.Forms.ComboBox cmbSourceDataType;
        private System.Windows.Forms.Label lblSourceDataType;
        private System.Windows.Forms.Label lblSourceConnectionStatus;
        private System.Windows.Forms.Button btnTestRead;
        private System.Windows.Forms.CheckBox chkEnableConversion;
        private System.Windows.Forms.TextBox txtConversionExpression;
        private System.Windows.Forms.Label lblConversionExpression;
        private System.Windows.Forms.GroupBox grpAdvanced;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtExecutionCondition;
        private System.Windows.Forms.Label lblExecutionCondition;
        private System.Windows.Forms.NumericUpDown numRetryCount;
        private System.Windows.Forms.Label lblRetryCount;
        private System.Windows.Forms.NumericUpDown numRetryDelay;
        private System.Windows.Forms.Label lblRetryDelay;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Timer validationTimer;
    }
}
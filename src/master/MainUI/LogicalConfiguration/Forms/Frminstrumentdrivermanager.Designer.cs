namespace MainUI.LogicalConfiguration.Forms
{
    partial class FrmInstrumentDriverManager
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            splitMain = new UISplitContainer();
            panelLeft = new UIPanel();
            dgvDrivers = new UIDataGridView();
            toolbarLeft = new FlowLayoutPanel();
            btnAdd = new UISymbolButton();
            btnEdit = new UISymbolButton();
            btnDelete = new UISymbolButton();
            btnClone = new UISymbolButton();
            btnImport = new UISymbolButton();
            btnExport = new UISymbolButton();
            panelRight = new UIPanel();
            tabDetails = new UITabControl();
            tabBasicInfo = new TabPage();
            layoutBasicInfo = new TableLayoutPanel();
            lblName = new Label();
            txtName = new UITextBox();
            lblDisplayName = new Label();
            txtDisplayName = new UITextBox();
            lblCategory = new Label();
            cboCategory = new UIComboBox();
            lblProtocolType = new Label();
            cboProtocolType = new UIComboBox();
            lblManufacturer = new Label();
            txtManufacturer = new UITextBox();
            lblModel = new Label();
            txtModel = new UITextBox();
            lblEnabled = new Label();
            chkEnabled = new UICheckBox();
            lblDescription = new Label();
            txtDescription = new UITextBox();
            tabProtocol = new TabPage();
            panelProtocolConfig = new UIPanel();
            tabFrame = new TabPage();
            layoutFrame = new TableLayoutPanel();
            lblFrameEnabled = new Label();
            chkFrameEnabled = new UICheckBox();
            lblFrameHeader = new Label();
            txtFrameHeader = new UITextBox();
            lblFrameFooter = new Label();
            txtFrameFooter = new UITextBox();
            lblResponseTerminator = new Label();
            txtResponseTerminator = new UITextBox();
            lblChecksumType = new Label();
            cboChecksumType = new UIComboBox();
            lblFrameTip = new Label();
            tabCommands = new TabPage();
            layoutCommands = new TableLayoutPanel();
            toolbarCommands = new FlowLayoutPanel();
            btnAddCommand = new UISymbolButton();
            btnEditCommand = new UISymbolButton();
            btnDeleteCommand = new UISymbolButton();
            dgvCommands = new UIDataGridView();
            panelBottom = new Panel();
            btnTestConnection = new UISymbolButton();
            btnSave = new UISymbolButton();
            btnCancel = new UISymbolButton();
            (splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDrivers).BeginInit();
            toolbarLeft.SuspendLayout();
            panelRight.SuspendLayout();
            tabDetails.SuspendLayout();
            tabBasicInfo.SuspendLayout();
            layoutBasicInfo.SuspendLayout();
            tabProtocol.SuspendLayout();
            tabFrame.SuspendLayout();
            layoutFrame.SuspendLayout();
            tabCommands.SuspendLayout();
            layoutCommands.SuspendLayout();
            toolbarCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCommands).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 35);
            splitMain.MinimumSize = new Size(20, 20);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(panelLeft);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(panelRight);
            splitMain.Size = new Size(1100, 665);
            splitMain.SplitterDistance = 350;
            splitMain.SplitterWidth = 11;
            splitMain.TabIndex = 0;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(dgvDrivers);
            panelLeft.Controls.Add(toolbarLeft);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelLeft.Location = new Point(0, 0);
            panelLeft.Margin = new Padding(4, 5, 4, 5);
            panelLeft.MinimumSize = new Size(1, 1);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(350, 665);
            panelLeft.TabIndex = 0;
            panelLeft.Text = "仪器列表";
            panelLeft.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // dgvDrivers
            // 
            dgvDrivers.AllowUserToAddRows = false;
            dgvDrivers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dgvDrivers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvDrivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDrivers.BackgroundColor = Color.White;
            dgvDrivers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvDrivers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvDrivers.ColumnHeadersHeight = 32;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvDrivers.DefaultCellStyle = dataGridViewCellStyle3;
            dgvDrivers.Dock = DockStyle.Fill;
            dgvDrivers.EnableHeadersVisualStyles = false;
            dgvDrivers.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvDrivers.GridColor = Color.FromArgb(80, 160, 255);
            dgvDrivers.Location = new Point(0, 40);
            dgvDrivers.MultiSelect = false;
            dgvDrivers.Name = "dgvDrivers";
            dgvDrivers.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvDrivers.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvDrivers.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvDrivers.SelectedIndex = -1;
            dgvDrivers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDrivers.Size = new Size(350, 625);
            dgvDrivers.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvDrivers.TabIndex = 0;
            // 
            // toolbarLeft
            // 
            toolbarLeft.Controls.Add(btnAdd);
            toolbarLeft.Controls.Add(btnEdit);
            toolbarLeft.Controls.Add(btnDelete);
            toolbarLeft.Controls.Add(btnClone);
            toolbarLeft.Controls.Add(btnImport);
            toolbarLeft.Controls.Add(btnExport);
            toolbarLeft.Dock = DockStyle.Top;
            toolbarLeft.Location = new Point(0, 0);
            toolbarLeft.Name = "toolbarLeft";
            toolbarLeft.Padding = new Padding(5);
            toolbarLeft.Size = new Size(350, 40);
            toolbarLeft.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAdd.Location = new Point(7, 7);
            btnAdd.Margin = new Padding(2);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 30);
            btnAdd.Symbol = 61543;
            btnAdd.TabIndex = 0;
            btnAdd.Text = "新建";
            btnAdd.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEdit.Location = new Point(91, 7);
            btnEdit.Margin = new Padding(2);
            btnEdit.MinimumSize = new Size(1, 1);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 30);
            btnEdit.Symbol = 61508;
            btnEdit.TabIndex = 1;
            btnEdit.Text = "编辑";
            btnEdit.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.Location = new Point(175, 7);
            btnDelete.Margin = new Padding(2);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
            btnDelete.Symbol = 61460;
            btnDelete.TabIndex = 2;
            btnDelete.Text = "删除";
            btnDelete.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnClone
            // 
            btnClone.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnClone.Location = new Point(259, 7);
            btnClone.Margin = new Padding(2);
            btnClone.MinimumSize = new Size(1, 1);
            btnClone.Name = "btnClone";
            btnClone.Size = new Size(80, 30);
            btnClone.Symbol = 61637;
            btnClone.TabIndex = 3;
            btnClone.Text = "复制";
            btnClone.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnImport
            // 
            btnImport.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnImport.Location = new Point(7, 41);
            btnImport.Margin = new Padding(2);
            btnImport.MinimumSize = new Size(1, 1);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(80, 30);
            btnImport.Symbol = 61573;
            btnImport.TabIndex = 4;
            btnImport.Text = "导入";
            btnImport.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnExport
            // 
            btnExport.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnExport.Location = new Point(91, 41);
            btnExport.Margin = new Padding(2);
            btnExport.MinimumSize = new Size(1, 1);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(80, 30);
            btnExport.Symbol = 61574;
            btnExport.TabIndex = 5;
            btnExport.Text = "导出";
            btnExport.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // panelRight
            // 
            panelRight.Controls.Add(tabDetails);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelRight.Location = new Point(0, 0);
            panelRight.Margin = new Padding(4, 5, 4, 5);
            panelRight.MinimumSize = new Size(1, 1);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(739, 665);
            panelRight.TabIndex = 0;
            panelRight.Text = "驱动配置";
            panelRight.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // tabDetails
            // 
            tabDetails.Controls.Add(tabBasicInfo);
            tabDetails.Controls.Add(tabProtocol);
            tabDetails.Controls.Add(tabFrame);
            tabDetails.Controls.Add(tabCommands);
            tabDetails.Dock = DockStyle.Fill;
            tabDetails.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabDetails.Font = new Font("微软雅黑", 9F);
            tabDetails.ItemSize = new Size(150, 40);
            tabDetails.Location = new Point(0, 0);
            tabDetails.MainPage = "";
            tabDetails.Name = "tabDetails";
            tabDetails.SelectedIndex = 0;
            tabDetails.Size = new Size(739, 665);
            tabDetails.SizeMode = TabSizeMode.Fixed;
            tabDetails.TabIndex = 0;
            tabDetails.TabUnSelectedForeColor = Color.FromArgb(240, 240, 240);
            tabDetails.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // tabBasicInfo
            // 
            tabBasicInfo.Controls.Add(layoutBasicInfo);
            tabBasicInfo.Location = new Point(0, 40);
            tabBasicInfo.Name = "tabBasicInfo";
            tabBasicInfo.Size = new Size(739, 625);
            tabBasicInfo.TabIndex = 0;
            tabBasicInfo.Text = "基本信息";
            // 
            // layoutBasicInfo
            // 
            layoutBasicInfo.ColumnCount = 4;
            layoutBasicInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutBasicInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBasicInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutBasicInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBasicInfo.Controls.Add(lblName, 0, 0);
            layoutBasicInfo.Controls.Add(txtName, 1, 0);
            layoutBasicInfo.Controls.Add(lblDisplayName, 2, 0);
            layoutBasicInfo.Controls.Add(txtDisplayName, 3, 0);
            layoutBasicInfo.Controls.Add(lblCategory, 0, 1);
            layoutBasicInfo.Controls.Add(cboCategory, 1, 1);
            layoutBasicInfo.Controls.Add(lblProtocolType, 2, 1);
            layoutBasicInfo.Controls.Add(cboProtocolType, 3, 1);
            layoutBasicInfo.Controls.Add(lblManufacturer, 0, 2);
            layoutBasicInfo.Controls.Add(txtManufacturer, 1, 2);
            layoutBasicInfo.Controls.Add(lblModel, 2, 2);
            layoutBasicInfo.Controls.Add(txtModel, 3, 2);
            layoutBasicInfo.Controls.Add(lblEnabled, 0, 3);
            layoutBasicInfo.Controls.Add(chkEnabled, 1, 3);
            layoutBasicInfo.Controls.Add(lblDescription, 0, 4);
            layoutBasicInfo.Controls.Add(txtDescription, 1, 4);
            layoutBasicInfo.Dock = DockStyle.Fill;
            layoutBasicInfo.Location = new Point(0, 0);
            layoutBasicInfo.Name = "layoutBasicInfo";
            layoutBasicInfo.RowCount = 5;
            layoutBasicInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutBasicInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutBasicInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutBasicInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutBasicInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBasicInfo.Size = new Size(739, 625);
            layoutBasicInfo.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("微软雅黑", 9F);
            lblName.Location = new Point(3, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(74, 40);
            lblName.TabIndex = 0;
            lblName.Text = "名称*:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Fill;
            txtName.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtName.Location = new Point(84, 5);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.MinimumSize = new Size(1, 16);
            txtName.Name = "txtName";
            txtName.Padding = new Padding(5);
            txtName.ShowText = false;
            txtName.Size = new Size(281, 30);
            txtName.TabIndex = 1;
            txtName.TextAlignment = ContentAlignment.MiddleLeft;
            txtName.Watermark = "英文标识符";
            // 
            // lblDisplayName
            // 
            lblDisplayName.Dock = DockStyle.Fill;
            lblDisplayName.Font = new Font("微软雅黑", 9F);
            lblDisplayName.Location = new Point(372, 0);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(74, 40);
            lblDisplayName.TabIndex = 2;
            lblDisplayName.Text = "显示名称*:";
            lblDisplayName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDisplayName
            // 
            txtDisplayName.Dock = DockStyle.Fill;
            txtDisplayName.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtDisplayName.Location = new Point(453, 5);
            txtDisplayName.Margin = new Padding(4, 5, 4, 5);
            txtDisplayName.MinimumSize = new Size(1, 16);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Padding = new Padding(5);
            txtDisplayName.ShowText = false;
            txtDisplayName.Size = new Size(282, 30);
            txtDisplayName.TabIndex = 3;
            txtDisplayName.TextAlignment = ContentAlignment.MiddleLeft;
            txtDisplayName.Watermark = "中文显示名称";
            // 
            // lblCategory
            // 
            lblCategory.Dock = DockStyle.Fill;
            lblCategory.Font = new Font("微软雅黑", 9F);
            lblCategory.Location = new Point(3, 40);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(74, 40);
            lblCategory.TabIndex = 4;
            lblCategory.Text = "类别:";
            lblCategory.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboCategory
            // 
            cboCategory.DataSource = null;
            cboCategory.Dock = DockStyle.Fill;
            cboCategory.DropDownStyle = UIDropDownStyle.DropDownList;
            cboCategory.FillColor = Color.White;
            cboCategory.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboCategory.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboCategory.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboCategory.Location = new Point(84, 45);
            cboCategory.Margin = new Padding(4, 5, 4, 5);
            cboCategory.MinimumSize = new Size(63, 0);
            cboCategory.Name = "cboCategory";
            cboCategory.Padding = new Padding(0, 0, 30, 2);
            cboCategory.Size = new Size(281, 30);
            cboCategory.SymbolSize = 24;
            cboCategory.TabIndex = 5;
            cboCategory.TextAlignment = ContentAlignment.MiddleLeft;
            cboCategory.Watermark = "";
            // 
            // lblProtocolType
            // 
            lblProtocolType.Dock = DockStyle.Fill;
            lblProtocolType.Font = new Font("微软雅黑", 9F);
            lblProtocolType.Location = new Point(372, 40);
            lblProtocolType.Name = "lblProtocolType";
            lblProtocolType.Size = new Size(74, 40);
            lblProtocolType.TabIndex = 6;
            lblProtocolType.Text = "协议类型*:";
            lblProtocolType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboProtocolType
            // 
            cboProtocolType.DataSource = null;
            cboProtocolType.Dock = DockStyle.Fill;
            cboProtocolType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboProtocolType.FillColor = Color.White;
            cboProtocolType.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboProtocolType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboProtocolType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboProtocolType.Location = new Point(453, 45);
            cboProtocolType.Margin = new Padding(4, 5, 4, 5);
            cboProtocolType.MinimumSize = new Size(63, 0);
            cboProtocolType.Name = "cboProtocolType";
            cboProtocolType.Padding = new Padding(0, 0, 30, 2);
            cboProtocolType.Size = new Size(282, 30);
            cboProtocolType.SymbolSize = 24;
            cboProtocolType.TabIndex = 7;
            cboProtocolType.TextAlignment = ContentAlignment.MiddleLeft;
            cboProtocolType.Watermark = "";
            // 
            // lblManufacturer
            // 
            lblManufacturer.Dock = DockStyle.Fill;
            lblManufacturer.Font = new Font("微软雅黑", 9F);
            lblManufacturer.Location = new Point(3, 80);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(74, 40);
            lblManufacturer.TabIndex = 8;
            lblManufacturer.Text = "制造商:";
            lblManufacturer.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtManufacturer
            // 
            txtManufacturer.Dock = DockStyle.Fill;
            txtManufacturer.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtManufacturer.Location = new Point(84, 85);
            txtManufacturer.Margin = new Padding(4, 5, 4, 5);
            txtManufacturer.MinimumSize = new Size(1, 16);
            txtManufacturer.Name = "txtManufacturer";
            txtManufacturer.Padding = new Padding(5);
            txtManufacturer.ShowText = false;
            txtManufacturer.Size = new Size(281, 30);
            txtManufacturer.TabIndex = 9;
            txtManufacturer.TextAlignment = ContentAlignment.MiddleLeft;
            txtManufacturer.Watermark = "";
            // 
            // lblModel
            // 
            lblModel.Dock = DockStyle.Fill;
            lblModel.Font = new Font("微软雅黑", 9F);
            lblModel.Location = new Point(372, 80);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(74, 40);
            lblModel.TabIndex = 10;
            lblModel.Text = "型号:";
            lblModel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtModel
            // 
            txtModel.Dock = DockStyle.Fill;
            txtModel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtModel.Location = new Point(453, 85);
            txtModel.Margin = new Padding(4, 5, 4, 5);
            txtModel.MinimumSize = new Size(1, 16);
            txtModel.Name = "txtModel";
            txtModel.Padding = new Padding(5);
            txtModel.ShowText = false;
            txtModel.Size = new Size(282, 30);
            txtModel.TabIndex = 11;
            txtModel.TextAlignment = ContentAlignment.MiddleLeft;
            txtModel.Watermark = "";
            // 
            // lblEnabled
            // 
            lblEnabled.Dock = DockStyle.Fill;
            lblEnabled.Font = new Font("微软雅黑", 9F);
            lblEnabled.Location = new Point(3, 120);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(74, 40);
            lblEnabled.TabIndex = 12;
            lblEnabled.Text = "状态:";
            lblEnabled.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkEnabled
            // 
            chkEnabled.Checked = true;
            chkEnabled.Dock = DockStyle.Fill;
            chkEnabled.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(83, 123);
            chkEnabled.MinimumSize = new Size(1, 1);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(283, 34);
            chkEnabled.TabIndex = 13;
            chkEnabled.Text = "启用此仪器驱动";
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Font = new Font("微软雅黑", 9F);
            lblDescription.Location = new Point(3, 160);
            lblDescription.Name = "lblDescription";
            lblDescription.Padding = new Padding(0, 10, 0, 0);
            lblDescription.Size = new Size(74, 465);
            lblDescription.TabIndex = 14;
            lblDescription.Text = "描述:";
            lblDescription.TextAlign = ContentAlignment.TopRight;
            // 
            // txtDescription
            // 
            layoutBasicInfo.SetColumnSpan(txtDescription, 3);
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtDescription.Location = new Point(84, 165);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(651, 455);
            txtDescription.TabIndex = 15;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "";
            // 
            // tabProtocol
            // 
            tabProtocol.Controls.Add(panelProtocolConfig);
            tabProtocol.Location = new Point(0, 40);
            tabProtocol.Name = "tabProtocol";
            tabProtocol.Size = new Size(739, 625);
            tabProtocol.TabIndex = 1;
            tabProtocol.Text = "协议配置";
            // 
            // panelProtocolConfig
            // 
            panelProtocolConfig.AutoScroll = true;
            panelProtocolConfig.Dock = DockStyle.Fill;
            panelProtocolConfig.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelProtocolConfig.Location = new Point(0, 0);
            panelProtocolConfig.Margin = new Padding(4, 5, 4, 5);
            panelProtocolConfig.MinimumSize = new Size(1, 1);
            panelProtocolConfig.Name = "panelProtocolConfig";
            panelProtocolConfig.Size = new Size(739, 625);
            panelProtocolConfig.TabIndex = 0;
            panelProtocolConfig.Text = null;
            panelProtocolConfig.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // tabFrame
            // 
            tabFrame.Controls.Add(layoutFrame);
            tabFrame.Location = new Point(0, 40);
            tabFrame.Name = "tabFrame";
            tabFrame.Size = new Size(739, 625);
            tabFrame.TabIndex = 2;
            tabFrame.Text = "帧配置";
            // 
            // layoutFrame
            // 
            layoutFrame.ColumnCount = 4;
            layoutFrame.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutFrame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutFrame.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutFrame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutFrame.Controls.Add(lblFrameEnabled, 0, 0);
            layoutFrame.Controls.Add(chkFrameEnabled, 1, 0);
            layoutFrame.Controls.Add(lblFrameHeader, 0, 1);
            layoutFrame.Controls.Add(txtFrameHeader, 1, 1);
            layoutFrame.Controls.Add(lblFrameFooter, 2, 1);
            layoutFrame.Controls.Add(txtFrameFooter, 3, 1);
            layoutFrame.Controls.Add(lblResponseTerminator, 0, 2);
            layoutFrame.Controls.Add(txtResponseTerminator, 1, 2);
            layoutFrame.Controls.Add(lblChecksumType, 2, 2);
            layoutFrame.Controls.Add(cboChecksumType, 3, 2);
            layoutFrame.Controls.Add(lblFrameTip, 0, 3);
            layoutFrame.Dock = DockStyle.Top;
            layoutFrame.Location = new Point(0, 0);
            layoutFrame.Name = "layoutFrame";
            layoutFrame.RowCount = 5;
            layoutFrame.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutFrame.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutFrame.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutFrame.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutFrame.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutFrame.Size = new Size(739, 250);
            layoutFrame.TabIndex = 0;
            // 
            // lblFrameEnabled
            // 
            lblFrameEnabled.Dock = DockStyle.Fill;
            lblFrameEnabled.Font = new Font("微软雅黑", 9F);
            lblFrameEnabled.Location = new Point(3, 0);
            lblFrameEnabled.Name = "lblFrameEnabled";
            lblFrameEnabled.Size = new Size(94, 40);
            lblFrameEnabled.TabIndex = 0;
            lblFrameEnabled.Text = "帧配置:";
            lblFrameEnabled.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkFrameEnabled
            // 
            chkFrameEnabled.Dock = DockStyle.Fill;
            chkFrameEnabled.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            chkFrameEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkFrameEnabled.Location = new Point(103, 3);
            chkFrameEnabled.MinimumSize = new Size(1, 1);
            chkFrameEnabled.Name = "chkFrameEnabled";
            chkFrameEnabled.Size = new Size(263, 34);
            chkFrameEnabled.TabIndex = 1;
            chkFrameEnabled.Text = "启用帧格式配置";
            // 
            // lblFrameHeader
            // 
            lblFrameHeader.Dock = DockStyle.Fill;
            lblFrameHeader.Font = new Font("微软雅黑", 9F);
            lblFrameHeader.Location = new Point(3, 40);
            lblFrameHeader.Name = "lblFrameHeader";
            lblFrameHeader.Size = new Size(94, 40);
            lblFrameHeader.TabIndex = 2;
            lblFrameHeader.Text = "帧头(Hex):";
            lblFrameHeader.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtFrameHeader
            // 
            txtFrameHeader.Dock = DockStyle.Fill;
            txtFrameHeader.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtFrameHeader.Location = new Point(104, 45);
            txtFrameHeader.Margin = new Padding(4, 5, 4, 5);
            txtFrameHeader.MinimumSize = new Size(1, 16);
            txtFrameHeader.Name = "txtFrameHeader";
            txtFrameHeader.Padding = new Padding(5);
            txtFrameHeader.ShowText = false;
            txtFrameHeader.Size = new Size(261, 30);
            txtFrameHeader.TabIndex = 3;
            txtFrameHeader.TextAlignment = ContentAlignment.MiddleLeft;
            txtFrameHeader.Watermark = "如: AA55";
            // 
            // lblFrameFooter
            // 
            lblFrameFooter.Dock = DockStyle.Fill;
            lblFrameFooter.Font = new Font("微软雅黑", 9F);
            lblFrameFooter.Location = new Point(372, 40);
            lblFrameFooter.Name = "lblFrameFooter";
            lblFrameFooter.Size = new Size(94, 40);
            lblFrameFooter.TabIndex = 4;
            lblFrameFooter.Text = "帧尾(Hex):";
            lblFrameFooter.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtFrameFooter
            // 
            txtFrameFooter.Dock = DockStyle.Fill;
            txtFrameFooter.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtFrameFooter.Location = new Point(473, 45);
            txtFrameFooter.Margin = new Padding(4, 5, 4, 5);
            txtFrameFooter.MinimumSize = new Size(1, 16);
            txtFrameFooter.Name = "txtFrameFooter";
            txtFrameFooter.Padding = new Padding(5);
            txtFrameFooter.ShowText = false;
            txtFrameFooter.Size = new Size(262, 30);
            txtFrameFooter.TabIndex = 5;
            txtFrameFooter.TextAlignment = ContentAlignment.MiddleLeft;
            txtFrameFooter.Watermark = "如: 0D0A";
            // 
            // lblResponseTerminator
            // 
            lblResponseTerminator.Dock = DockStyle.Fill;
            lblResponseTerminator.Font = new Font("微软雅黑", 9F);
            lblResponseTerminator.Location = new Point(3, 80);
            lblResponseTerminator.Name = "lblResponseTerminator";
            lblResponseTerminator.Size = new Size(94, 40);
            lblResponseTerminator.TabIndex = 6;
            lblResponseTerminator.Text = "结束符:";
            lblResponseTerminator.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtResponseTerminator
            // 
            txtResponseTerminator.Dock = DockStyle.Fill;
            txtResponseTerminator.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtResponseTerminator.Location = new Point(104, 85);
            txtResponseTerminator.Margin = new Padding(4, 5, 4, 5);
            txtResponseTerminator.MinimumSize = new Size(1, 16);
            txtResponseTerminator.Name = "txtResponseTerminator";
            txtResponseTerminator.Padding = new Padding(5);
            txtResponseTerminator.ShowText = false;
            txtResponseTerminator.Size = new Size(261, 30);
            txtResponseTerminator.TabIndex = 7;
            txtResponseTerminator.TextAlignment = ContentAlignment.MiddleLeft;
            txtResponseTerminator.Watermark = "如: \\n 或 \\r\\n";
            // 
            // lblChecksumType
            // 
            lblChecksumType.Dock = DockStyle.Fill;
            lblChecksumType.Font = new Font("微软雅黑", 9F);
            lblChecksumType.Location = new Point(372, 80);
            lblChecksumType.Name = "lblChecksumType";
            lblChecksumType.Size = new Size(94, 40);
            lblChecksumType.TabIndex = 8;
            lblChecksumType.Text = "校验类型:";
            lblChecksumType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboChecksumType
            // 
            cboChecksumType.DataSource = null;
            cboChecksumType.Dock = DockStyle.Fill;
            cboChecksumType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboChecksumType.FillColor = Color.White;
            cboChecksumType.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboChecksumType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboChecksumType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboChecksumType.Location = new Point(473, 85);
            cboChecksumType.Margin = new Padding(4, 5, 4, 5);
            cboChecksumType.MinimumSize = new Size(63, 0);
            cboChecksumType.Name = "cboChecksumType";
            cboChecksumType.Padding = new Padding(0, 0, 30, 2);
            cboChecksumType.Size = new Size(262, 30);
            cboChecksumType.SymbolSize = 24;
            cboChecksumType.TabIndex = 9;
            cboChecksumType.TextAlignment = ContentAlignment.MiddleLeft;
            cboChecksumType.Watermark = "";
            // 
            // lblFrameTip
            // 
            layoutFrame.SetColumnSpan(lblFrameTip, 4);
            lblFrameTip.Dock = DockStyle.Fill;
            lblFrameTip.ForeColor = Color.Gray;
            lblFrameTip.Location = new Point(3, 120);
            lblFrameTip.Name = "lblFrameTip";
            lblFrameTip.Size = new Size(733, 40);
            lblFrameTip.TabIndex = 10;
            lblFrameTip.Text = "提示：帧头帧尾使用十六进制表示，如 AA55 表示 0xAA 0x55。结束符使用转义字符，如 \\n 表示换行符。";
            // 
            // tabCommands
            // 
            tabCommands.Controls.Add(layoutCommands);
            tabCommands.Location = new Point(0, 40);
            tabCommands.Name = "tabCommands";
            tabCommands.Size = new Size(739, 625);
            tabCommands.TabIndex = 3;
            tabCommands.Text = "命令模板";
            // 
            // layoutCommands
            // 
            layoutCommands.ColumnCount = 1;
            layoutCommands.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCommands.Controls.Add(toolbarCommands, 0, 0);
            layoutCommands.Controls.Add(dgvCommands, 0, 1);
            layoutCommands.Dock = DockStyle.Fill;
            layoutCommands.Location = new Point(0, 0);
            layoutCommands.Name = "layoutCommands";
            layoutCommands.RowCount = 2;
            layoutCommands.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutCommands.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCommands.Size = new Size(739, 625);
            layoutCommands.TabIndex = 0;
            // 
            // toolbarCommands
            // 
            toolbarCommands.Controls.Add(btnAddCommand);
            toolbarCommands.Controls.Add(btnEditCommand);
            toolbarCommands.Controls.Add(btnDeleteCommand);
            toolbarCommands.Dock = DockStyle.Fill;
            toolbarCommands.Location = new Point(3, 3);
            toolbarCommands.Name = "toolbarCommands";
            toolbarCommands.Size = new Size(733, 34);
            toolbarCommands.TabIndex = 0;
            // 
            // btnAddCommand
            // 
            btnAddCommand.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddCommand.Location = new Point(2, 2);
            btnAddCommand.Margin = new Padding(2);
            btnAddCommand.MinimumSize = new Size(1, 1);
            btnAddCommand.Name = "btnAddCommand";
            btnAddCommand.Size = new Size(100, 30);
            btnAddCommand.Symbol = 61543;
            btnAddCommand.TabIndex = 0;
            btnAddCommand.Text = "添加命令";
            btnAddCommand.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnEditCommand
            // 
            btnEditCommand.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEditCommand.Location = new Point(106, 2);
            btnEditCommand.Margin = new Padding(2);
            btnEditCommand.MinimumSize = new Size(1, 1);
            btnEditCommand.Name = "btnEditCommand";
            btnEditCommand.Size = new Size(80, 30);
            btnEditCommand.Symbol = 61508;
            btnEditCommand.TabIndex = 1;
            btnEditCommand.Text = "编辑";
            btnEditCommand.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnDeleteCommand
            // 
            btnDeleteCommand.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDeleteCommand.Location = new Point(190, 2);
            btnDeleteCommand.Margin = new Padding(2);
            btnDeleteCommand.MinimumSize = new Size(1, 1);
            btnDeleteCommand.Name = "btnDeleteCommand";
            btnDeleteCommand.Size = new Size(80, 30);
            btnDeleteCommand.Symbol = 61460;
            btnDeleteCommand.TabIndex = 2;
            btnDeleteCommand.Text = "删除";
            btnDeleteCommand.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // dgvCommands
            // 
            dgvCommands.AllowUserToAddRows = false;
            dgvCommands.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(235, 243, 255);
            dgvCommands.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            dgvCommands.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCommands.BackgroundColor = Color.White;
            dgvCommands.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle7.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dgvCommands.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dgvCommands.ColumnHeadersHeight = 32;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvCommands.DefaultCellStyle = dataGridViewCellStyle8;
            dgvCommands.Dock = DockStyle.Fill;
            dgvCommands.EnableHeadersVisualStyles = false;
            dgvCommands.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvCommands.GridColor = Color.FromArgb(80, 160, 255);
            dgvCommands.Location = new Point(3, 43);
            dgvCommands.MultiSelect = false;
            dgvCommands.Name = "dgvCommands";
            dgvCommands.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle9.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dgvCommands.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvCommands.RowsDefaultCellStyle = dataGridViewCellStyle10;
            dgvCommands.SelectedIndex = -1;
            dgvCommands.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCommands.Size = new Size(733, 579);
            dgvCommands.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvCommands.TabIndex = 1;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btnTestConnection);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 700);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1100, 50);
            panelBottom.TabIndex = 1;
            // 
            // btnTestConnection
            // 
            btnTestConnection.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnTestConnection.Location = new Point(15, 8);
            btnTestConnection.MinimumSize = new Size(1, 1);
            btnTestConnection.Name = "btnTestConnection";
            btnTestConnection.Size = new Size(100, 35);
            btnTestConnection.Symbol = 61931;
            btnTestConnection.TabIndex = 0;
            btnTestConnection.Text = "测试连接";
            btnTestConnection.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Right;
            btnSave.FillColor = Color.FromArgb(0, 150, 136);
            btnSave.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSave.Location = new Point(1770, 8);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 35);
            btnSave.Symbol = 61639;
            btnSave.TabIndex = 1;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Right;
            btnCancel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Location = new Point(1885, 8);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 2;
            btnCancel.Text = "关闭";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // FrmInstrumentDriverManager
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1100, 750);
            Controls.Add(splitMain);
            Controls.Add(panelBottom);
            Name = "FrmInstrumentDriverManager";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "仪器驱动管理";
            ZoomScaleRect = new Rectangle(15, 15, 1100, 750);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            (splitMain).EndInit();
            splitMain.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDrivers).EndInit();
            toolbarLeft.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            tabDetails.ResumeLayout(false);
            tabBasicInfo.ResumeLayout(false);
            layoutBasicInfo.ResumeLayout(false);
            tabProtocol.ResumeLayout(false);
            tabFrame.ResumeLayout(false);
            layoutFrame.ResumeLayout(false);
            tabCommands.ResumeLayout(false);
            layoutCommands.ResumeLayout(false);
            toolbarCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCommands).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region 控件声明

        private Sunny.UI.UISplitContainer splitMain;
        private Sunny.UI.UIPanel panelLeft;
        private Sunny.UI.UIPanel panelRight;
        private System.Windows.Forms.Panel panelBottom;

        // 左侧工具栏和列表
        private System.Windows.Forms.FlowLayoutPanel toolbarLeft;
        private Sunny.UI.UISymbolButton btnAdd;
        private Sunny.UI.UISymbolButton btnEdit;
        private Sunny.UI.UISymbolButton btnDelete;
        private Sunny.UI.UISymbolButton btnClone;
        private Sunny.UI.UISymbolButton btnImport;
        private Sunny.UI.UISymbolButton btnExport;
        private Sunny.UI.UIDataGridView dgvDrivers;

        // 右侧Tab
        private Sunny.UI.UITabControl tabDetails;
        private System.Windows.Forms.TabPage tabBasicInfo;
        private System.Windows.Forms.TabPage tabProtocol;
        private System.Windows.Forms.TabPage tabFrame;
        private System.Windows.Forms.TabPage tabCommands;

        // 基本信息Tab
        private System.Windows.Forms.TableLayoutPanel layoutBasicInfo;
        private System.Windows.Forms.Label lblName;
        private Sunny.UI.UITextBox txtName;
        private System.Windows.Forms.Label lblDisplayName;
        private Sunny.UI.UITextBox txtDisplayName;
        private System.Windows.Forms.Label lblCategory;
        private Sunny.UI.UIComboBox cboCategory;
        private System.Windows.Forms.Label lblProtocolType;
        private Sunny.UI.UIComboBox cboProtocolType;
        private System.Windows.Forms.Label lblManufacturer;
        private Sunny.UI.UITextBox txtManufacturer;
        private System.Windows.Forms.Label lblModel;
        private Sunny.UI.UITextBox txtModel;
        private System.Windows.Forms.Label lblEnabled;
        private Sunny.UI.UICheckBox chkEnabled;
        private System.Windows.Forms.Label lblDescription;
        private Sunny.UI.UITextBox txtDescription;

        // 协议配置Tab
        private Sunny.UI.UIPanel panelProtocolConfig;

        // 帧配置Tab
        private System.Windows.Forms.TableLayoutPanel layoutFrame;
        private System.Windows.Forms.Label lblFrameEnabled;
        private Sunny.UI.UICheckBox chkFrameEnabled;
        private System.Windows.Forms.Label lblFrameHeader;
        private Sunny.UI.UITextBox txtFrameHeader;
        private System.Windows.Forms.Label lblFrameFooter;
        private Sunny.UI.UITextBox txtFrameFooter;
        private System.Windows.Forms.Label lblResponseTerminator;
        private Sunny.UI.UITextBox txtResponseTerminator;
        private System.Windows.Forms.Label lblChecksumType;
        private Sunny.UI.UIComboBox cboChecksumType;
        private System.Windows.Forms.Label lblFrameTip;

        // 命令Tab
        private System.Windows.Forms.TableLayoutPanel layoutCommands;
        private System.Windows.Forms.FlowLayoutPanel toolbarCommands;
        private Sunny.UI.UISymbolButton btnAddCommand;
        private Sunny.UI.UISymbolButton btnEditCommand;
        private Sunny.UI.UISymbolButton btnDeleteCommand;
        private Sunny.UI.UIDataGridView dgvCommands;

        // 底部按钮
        private Sunny.UI.UISymbolButton btnTestConnection;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UISymbolButton btnCancel;

        #endregion
    }
}
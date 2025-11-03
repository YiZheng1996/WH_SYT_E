namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_DefinePoint
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
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            pnlLeft = new UIPanel();
            grpModules = new UIGroupBox();
            lstModules = new UIListBox();
            pnlModuleButtons = new UIPanel();
            btnAddModule = new UISymbolButton();
            btnDeleteModule = new UISymbolButton();
            btnRenameModule = new UISymbolButton();
            BtnDownloadTemplate = new UISymbolButton();
            pnlRight = new UIPanel();
            grpPoints = new UIGroupBox();
            dgvPoints = new UIDataGridView();
            ColPointName = new DataGridViewTextBoxColumn();
            ColPointAddress = new DataGridViewTextBoxColumn();
            pnlPointButtons = new UIPanel();
            btnAddPoint = new UISymbolButton();
            btnDeletePoint = new UISymbolButton();
            btnImportExcel = new UISymbolButton();
            btnExport = new UISymbolButton();
            btnClearPoints = new UISymbolButton();
            pnlServerName = new UIPanel();
            lblServerName = new UILabel();
            txtServerName = new UITextBox();
            btnSetServerName = new UISymbolButton();
            pnlBottom = new UIPanel();
            lblStatus = new UILabel();
            btnHelp = new UISymbolButton();
            btnSave = new UISymbolButton();
            btnCancel = new UISymbolButton();
            pnlLeft.SuspendLayout();
            grpModules.SuspendLayout();
            pnlModuleButtons.SuspendLayout();
            pnlRight.SuspendLayout();
            grpPoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPoints).BeginInit();
            pnlPointButtons.SuspendLayout();
            pnlServerName.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.Controls.Add(grpModules);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.FillColor = Color.White;
            pnlLeft.FillColor2 = Color.White;
            pnlLeft.Font = new Font("微软雅黑", 12F);
            pnlLeft.Location = new Point(0, 35);
            pnlLeft.Margin = new Padding(4, 5, 4, 5);
            pnlLeft.MinimumSize = new Size(1, 1);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(10);
            pnlLeft.Radius = 0;
            pnlLeft.RectColor = Color.FromArgb(233, 236, 239);
            pnlLeft.Size = new Size(300, 665);
            pnlLeft.Style = UIStyle.Custom;
            pnlLeft.TabIndex = 0;
            pnlLeft.Text = null;
            pnlLeft.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // grpModules
            // 
            grpModules.Controls.Add(lstModules);
            grpModules.Controls.Add(pnlModuleButtons);
            grpModules.Dock = DockStyle.Fill;
            grpModules.FillColor = Color.White;
            grpModules.FillColor2 = Color.White;
            grpModules.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            grpModules.Location = new Point(10, 10);
            grpModules.Margin = new Padding(4, 5, 4, 5);
            grpModules.MinimumSize = new Size(1, 1);
            grpModules.Name = "grpModules";
            grpModules.Padding = new Padding(10, 35, 10, 10);
            grpModules.RectColor = Color.FromArgb(65, 100, 204);
            grpModules.Size = new Size(280, 645);
            grpModules.Style = UIStyle.Custom;
            grpModules.TabIndex = 0;
            grpModules.Text = "📦 PLC模块列表";
            grpModules.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lstModules
            // 
            lstModules.Dock = DockStyle.Fill;
            lstModules.FillColor = Color.White;
            lstModules.Font = new Font("微软雅黑", 10F);
            lstModules.HoverColor = Color.FromArgb(155, 200, 255);
            lstModules.ItemSelectBackColor = Color.FromArgb(65, 100, 204);
            lstModules.ItemSelectForeColor = Color.White;
            lstModules.Location = new Point(10, 35);
            lstModules.Margin = new Padding(4, 5, 4, 5);
            lstModules.MinimumSize = new Size(1, 1);
            lstModules.Name = "lstModules";
            lstModules.Padding = new Padding(2);
            lstModules.RectColor = Color.FromArgb(233, 236, 239);
            lstModules.ShowText = false;
            lstModules.Size = new Size(260, 550);
            lstModules.Style = UIStyle.Custom;
            lstModules.TabIndex = 0;
            lstModules.Text = null;
            lstModules.SelectedIndexChanged += LstModules_SelectedIndexChanged;
            // 
            // pnlModuleButtons
            // 
            pnlModuleButtons.Controls.Add(btnAddModule);
            pnlModuleButtons.Controls.Add(btnDeleteModule);
            pnlModuleButtons.Controls.Add(btnRenameModule);
            pnlModuleButtons.Dock = DockStyle.Bottom;
            pnlModuleButtons.FillColor = Color.White;
            pnlModuleButtons.FillColor2 = Color.White;
            pnlModuleButtons.Font = new Font("微软雅黑", 12F);
            pnlModuleButtons.Location = new Point(10, 585);
            pnlModuleButtons.Margin = new Padding(4, 5, 4, 5);
            pnlModuleButtons.MinimumSize = new Size(1, 1);
            pnlModuleButtons.Name = "pnlModuleButtons";
            pnlModuleButtons.Padding = new Padding(5);
            pnlModuleButtons.Radius = 0;
            pnlModuleButtons.RectColor = Color.FromArgb(233, 236, 239);
            pnlModuleButtons.Size = new Size(260, 50);
            pnlModuleButtons.Style = UIStyle.Custom;
            pnlModuleButtons.TabIndex = 1;
            pnlModuleButtons.Text = null;
            pnlModuleButtons.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnAddModule
            // 
            btnAddModule.Cursor = Cursors.Hand;
            btnAddModule.FillColor = Color.FromArgb(40, 167, 69);
            btnAddModule.FillColor2 = Color.FromArgb(34, 139, 34);
            btnAddModule.FillColorGradient = true;
            btnAddModule.Font = new Font("微软雅黑", 9F);
            btnAddModule.Location = new Point(5, 10);
            btnAddModule.MinimumSize = new Size(1, 1);
            btnAddModule.Name = "btnAddModule";
            btnAddModule.RectColor = Color.FromArgb(40, 167, 69);
            btnAddModule.Size = new Size(75, 32);
            btnAddModule.Style = UIStyle.Custom;
            btnAddModule.Symbol = 61543;
            btnAddModule.SymbolSize = 20;
            btnAddModule.TabIndex = 0;
            btnAddModule.Text = "添加";
            btnAddModule.TipsFont = new Font("微软雅黑", 9F);
            btnAddModule.Click += BtnAddModule_Click;
            // 
            // btnDeleteModule
            // 
            btnDeleteModule.Cursor = Cursors.Hand;
            btnDeleteModule.FillColor = Color.FromArgb(239, 68, 68);
            btnDeleteModule.FillColor2 = Color.FromArgb(220, 38, 38);
            btnDeleteModule.FillColorGradient = true;
            btnDeleteModule.Font = new Font("微软雅黑", 9F);
            btnDeleteModule.Location = new Point(90, 10);
            btnDeleteModule.MinimumSize = new Size(1, 1);
            btnDeleteModule.Name = "btnDeleteModule";
            btnDeleteModule.RectColor = Color.FromArgb(239, 68, 68);
            btnDeleteModule.Size = new Size(75, 32);
            btnDeleteModule.Style = UIStyle.Custom;
            btnDeleteModule.Symbol = 61460;
            btnDeleteModule.SymbolSize = 20;
            btnDeleteModule.TabIndex = 1;
            btnDeleteModule.Text = "删除";
            btnDeleteModule.TipsFont = new Font("微软雅黑", 9F);
            btnDeleteModule.Click += BtnDeleteModule_Click;
            // 
            // btnRenameModule
            // 
            btnRenameModule.Cursor = Cursors.Hand;
            btnRenameModule.FillColor = Color.FromArgb(255, 193, 7);
            btnRenameModule.FillColor2 = Color.FromArgb(245, 166, 35);
            btnRenameModule.FillColorGradient = true;
            btnRenameModule.Font = new Font("微软雅黑", 9F);
            btnRenameModule.Location = new Point(175, 10);
            btnRenameModule.MinimumSize = new Size(1, 1);
            btnRenameModule.Name = "btnRenameModule";
            btnRenameModule.RectColor = Color.FromArgb(255, 193, 7);
            btnRenameModule.Size = new Size(80, 32);
            btnRenameModule.Style = UIStyle.Custom;
            btnRenameModule.Symbol = 61508;
            btnRenameModule.SymbolSize = 20;
            btnRenameModule.TabIndex = 2;
            btnRenameModule.Text = "重命名";
            btnRenameModule.TipsFont = new Font("微软雅黑", 9F);
            btnRenameModule.Click += BtnRenameModule_Click;
            // 
            // BtnDownloadTemplate
            // 
            BtnDownloadTemplate.Cursor = Cursors.Hand;
            BtnDownloadTemplate.FillColor = Color.FromArgb(16, 185, 129);
            BtnDownloadTemplate.FillColor2 = Color.FromArgb(16, 185, 129);
            BtnDownloadTemplate.FillColorGradient = true;
            BtnDownloadTemplate.FillHoverColor = Color.FromArgb(13, 148, 103);
            BtnDownloadTemplate.FillPressColor = Color.FromArgb(10, 111, 77);
            BtnDownloadTemplate.Font = new Font("微软雅黑", 10F);
            BtnDownloadTemplate.Location = new Point(565, 10);
            BtnDownloadTemplate.MinimumSize = new Size(1, 1);
            BtnDownloadTemplate.Name = "BtnDownloadTemplate";
            BtnDownloadTemplate.RectColor = Color.FromArgb(16, 185, 129);
            BtnDownloadTemplate.Size = new Size(100, 32);
            BtnDownloadTemplate.Symbol = 61549;
            BtnDownloadTemplate.TabIndex = 25;
            BtnDownloadTemplate.Text = "下载模板";
            BtnDownloadTemplate.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnDownloadTemplate.Click += BtnDownloadTemplate_Click;
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(grpPoints);
            pnlRight.Controls.Add(pnlServerName);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.FillColor = Color.White;
            pnlRight.FillColor2 = Color.White;
            pnlRight.Font = new Font("微软雅黑", 12F);
            pnlRight.Location = new Point(300, 35);
            pnlRight.Margin = new Padding(4, 5, 4, 5);
            pnlRight.MinimumSize = new Size(1, 1);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(10);
            pnlRight.Radius = 0;
            pnlRight.RectColor = Color.FromArgb(233, 236, 239);
            pnlRight.Size = new Size(900, 665);
            pnlRight.Style = UIStyle.Custom;
            pnlRight.TabIndex = 1;
            pnlRight.Text = null;
            pnlRight.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // grpPoints
            // 
            grpPoints.Controls.Add(dgvPoints);
            grpPoints.Controls.Add(pnlPointButtons);
            grpPoints.Dock = DockStyle.Fill;
            grpPoints.FillColor = Color.White;
            grpPoints.FillColor2 = Color.White;
            grpPoints.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            grpPoints.Location = new Point(10, 80);
            grpPoints.Margin = new Padding(4, 5, 4, 5);
            grpPoints.MinimumSize = new Size(1, 1);
            grpPoints.Name = "grpPoints";
            grpPoints.Padding = new Padding(10, 35, 10, 10);
            grpPoints.RectColor = Color.FromArgb(65, 100, 204);
            grpPoints.Size = new Size(880, 575);
            grpPoints.Style = UIStyle.Custom;
            grpPoints.TabIndex = 1;
            grpPoints.Text = "📍 点位定义列表";
            grpPoints.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // dgvPoints
            // 
            dataGridViewCellStyle6.BackColor = Color.FromArgb(250, 250, 252);
            dgvPoints.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            dgvPoints.BackgroundColor = Color.White;
            dgvPoints.BorderStyle = BorderStyle.None;
            dgvPoints.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle7.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle7.SelectionForeColor = Color.White;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dgvPoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dgvPoints.ColumnHeadersHeight = 40;
            dgvPoints.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPoints.Columns.AddRange(new DataGridViewColumn[] { ColPointName, ColPointAddress });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.Font = new Font("微软雅黑", 10F);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle8.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvPoints.DefaultCellStyle = dataGridViewCellStyle8;
            dgvPoints.Dock = DockStyle.Fill;
            dgvPoints.EnableHeadersVisualStyles = false;
            dgvPoints.Font = new Font("微软雅黑", 10F);
            dgvPoints.GridColor = Color.FromArgb(233, 236, 239);
            dgvPoints.Location = new Point(10, 35);
            dgvPoints.Name = "dgvPoints";
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle9.Font = new Font("微软雅黑", 10F);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dgvPoints.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("微软雅黑", 10F);
            dgvPoints.RowsDefaultCellStyle = dataGridViewCellStyle10;
            dgvPoints.RowTemplate.Height = 35;
            dgvPoints.ScrollBars = ScrollBars.Vertical;
            dgvPoints.SelectedIndex = -1;
            dgvPoints.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPoints.Size = new Size(860, 480);
            dgvPoints.StripeOddColor = Color.FromArgb(250, 250, 252);
            dgvPoints.Style = UIStyle.Custom;
            dgvPoints.TabIndex = 0;
            dgvPoints.CellValueChanged += DgvPoints_CellValueChanged;
            dgvPoints.UserDeletedRow += DgvPoints_UserDeletedRow;
            // 
            // ColPointName
            // 
            ColPointName.HeaderText = "点位名称";
            ColPointName.Name = "ColPointName";
            ColPointName.Width = 300;
            // 
            // ColPointAddress
            // 
            ColPointAddress.HeaderText = "点位地址";
            ColPointAddress.Name = "ColPointAddress";
            ColPointAddress.Width = 520;
            // 
            // pnlPointButtons
            // 
            pnlPointButtons.Controls.Add(btnAddPoint);
            pnlPointButtons.Controls.Add(btnDeletePoint);
            pnlPointButtons.Controls.Add(BtnDownloadTemplate);
            pnlPointButtons.Controls.Add(btnImportExcel);
            pnlPointButtons.Controls.Add(btnExport);
            pnlPointButtons.Controls.Add(btnClearPoints);
            pnlPointButtons.Dock = DockStyle.Bottom;
            pnlPointButtons.FillColor = Color.White;
            pnlPointButtons.FillColor2 = Color.White;
            pnlPointButtons.Font = new Font("微软雅黑", 12F);
            pnlPointButtons.Location = new Point(10, 515);
            pnlPointButtons.Margin = new Padding(4, 5, 4, 5);
            pnlPointButtons.MinimumSize = new Size(1, 1);
            pnlPointButtons.Name = "pnlPointButtons";
            pnlPointButtons.Padding = new Padding(5);
            pnlPointButtons.Radius = 0;
            pnlPointButtons.RectColor = Color.FromArgb(233, 236, 239);
            pnlPointButtons.Size = new Size(860, 50);
            pnlPointButtons.Style = UIStyle.Custom;
            pnlPointButtons.TabIndex = 1;
            pnlPointButtons.Text = null;
            pnlPointButtons.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnAddPoint
            // 
            btnAddPoint.Cursor = Cursors.Hand;
            btnAddPoint.FillColor = Color.FromArgb(40, 167, 69);
            btnAddPoint.FillColor2 = Color.FromArgb(34, 139, 34);
            btnAddPoint.FillColorGradient = true;
            btnAddPoint.Font = new Font("微软雅黑", 9F);
            btnAddPoint.Location = new Point(5, 10);
            btnAddPoint.MinimumSize = new Size(1, 1);
            btnAddPoint.Name = "btnAddPoint";
            btnAddPoint.RectColor = Color.FromArgb(40, 167, 69);
            btnAddPoint.Size = new Size(90, 32);
            btnAddPoint.Style = UIStyle.Custom;
            btnAddPoint.Symbol = 61543;
            btnAddPoint.SymbolSize = 20;
            btnAddPoint.TabIndex = 0;
            btnAddPoint.Text = "添加点位";
            btnAddPoint.TipsFont = new Font("微软雅黑", 9F);
            btnAddPoint.Click += BtnAddPoint_Click;
            // 
            // btnDeletePoint
            // 
            btnDeletePoint.Cursor = Cursors.Hand;
            btnDeletePoint.FillColor = Color.FromArgb(239, 68, 68);
            btnDeletePoint.FillColor2 = Color.FromArgb(220, 38, 38);
            btnDeletePoint.FillColorGradient = true;
            btnDeletePoint.Font = new Font("微软雅黑", 9F);
            btnDeletePoint.Location = new Point(105, 10);
            btnDeletePoint.MinimumSize = new Size(1, 1);
            btnDeletePoint.Name = "btnDeletePoint";
            btnDeletePoint.RectColor = Color.FromArgb(239, 68, 68);
            btnDeletePoint.Size = new Size(90, 32);
            btnDeletePoint.Style = UIStyle.Custom;
            btnDeletePoint.Symbol = 61460;
            btnDeletePoint.SymbolSize = 20;
            btnDeletePoint.TabIndex = 1;
            btnDeletePoint.Text = "删除点位";
            btnDeletePoint.TipsFont = new Font("微软雅黑", 9F);
            btnDeletePoint.Click += BtnDeletePoint_Click;
            // 
            // btnImportExcel
            // 
            btnImportExcel.Cursor = Cursors.Hand;
            btnImportExcel.FillColor = Color.FromArgb(16, 185, 129);
            btnImportExcel.FillColor2 = Color.FromArgb(5, 150, 105);
            btnImportExcel.FillColorGradient = true;
            btnImportExcel.Font = new Font("微软雅黑", 9F);
            btnImportExcel.Location = new Point(335, 10);
            btnImportExcel.MinimumSize = new Size(1, 1);
            btnImportExcel.Name = "btnImportExcel";
            btnImportExcel.RectColor = Color.FromArgb(16, 185, 129);
            btnImportExcel.Size = new Size(110, 32);
            btnImportExcel.Style = UIStyle.Custom;
            btnImportExcel.Symbol = 61564;
            btnImportExcel.SymbolSize = 20;
            btnImportExcel.TabIndex = 2;
            btnImportExcel.Text = "导入Excel";
            btnImportExcel.TipsFont = new Font("微软雅黑", 9F);
            btnImportExcel.Click += BtnImportExcel_Click;
            // 
            // btnExport
            // 
            btnExport.Cursor = Cursors.Hand;
            btnExport.FillColor = Color.FromArgb(59, 130, 246);
            btnExport.FillColor2 = Color.FromArgb(29, 78, 216);
            btnExport.FillColorGradient = true;
            btnExport.Font = new Font("微软雅黑", 9F);
            btnExport.Location = new Point(455, 10);
            btnExport.MinimumSize = new Size(1, 1);
            btnExport.Name = "btnExport";
            btnExport.RectColor = Color.FromArgb(59, 130, 246);
            btnExport.Size = new Size(100, 32);
            btnExport.Style = UIStyle.Custom;
            btnExport.Symbol = 61639;
            btnExport.SymbolSize = 20;
            btnExport.TabIndex = 4;
            btnExport.Text = "导出Excel";
            btnExport.TipsFont = new Font("微软雅黑", 9F);
            btnExport.Click += BtnExport_Click;
            // 
            // btnClearPoints
            // 
            btnClearPoints.Cursor = Cursors.Hand;
            btnClearPoints.FillColor = Color.FromArgb(255, 193, 7);
            btnClearPoints.FillColor2 = Color.FromArgb(245, 166, 35);
            btnClearPoints.FillColorGradient = true;
            btnClearPoints.Font = new Font("微软雅黑", 9F);
            btnClearPoints.Location = new Point(205, 10);
            btnClearPoints.MinimumSize = new Size(1, 1);
            btnClearPoints.Name = "btnClearPoints";
            btnClearPoints.RectColor = Color.FromArgb(255, 193, 7);
            btnClearPoints.Size = new Size(120, 32);
            btnClearPoints.Style = UIStyle.Custom;
            btnClearPoints.Symbol = 61552;
            btnClearPoints.SymbolSize = 20;
            btnClearPoints.TabIndex = 5;
            btnClearPoints.Text = "清空当前模块";
            btnClearPoints.TipsFont = new Font("微软雅黑", 9F);
            btnClearPoints.Click += BtnClearPoints_Click;
            // 
            // pnlServerName
            // 
            pnlServerName.Controls.Add(lblServerName);
            pnlServerName.Controls.Add(txtServerName);
            pnlServerName.Controls.Add(btnSetServerName);
            pnlServerName.Dock = DockStyle.Top;
            pnlServerName.FillColor = Color.White;
            pnlServerName.FillColor2 = Color.FromArgb(248, 249, 250);
            pnlServerName.Font = new Font("微软雅黑", 12F);
            pnlServerName.Location = new Point(10, 10);
            pnlServerName.Margin = new Padding(4, 5, 4, 5);
            pnlServerName.MinimumSize = new Size(1, 1);
            pnlServerName.Name = "pnlServerName";
            pnlServerName.Padding = new Padding(10);
            pnlServerName.RectColor = Color.FromArgb(65, 100, 204);
            pnlServerName.Size = new Size(880, 70);
            pnlServerName.Style = UIStyle.Custom;
            pnlServerName.TabIndex = 0;
            pnlServerName.Text = null;
            pnlServerName.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblServerName
            // 
            lblServerName.BackColor = Color.Transparent;
            lblServerName.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblServerName.ForeColor = Color.FromArgb(48, 48, 48);
            lblServerName.Location = new Point(15, 20);
            lblServerName.Name = "lblServerName";
            lblServerName.Size = new Size(100, 30);
            lblServerName.Style = UIStyle.Custom;
            lblServerName.TabIndex = 0;
            lblServerName.Text = "ServerName:";
            lblServerName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtServerName
            // 
            txtServerName.Cursor = Cursors.IBeam;
            txtServerName.Font = new Font("微软雅黑", 10F);
            txtServerName.Location = new Point(120, 20);
            txtServerName.Margin = new Padding(4, 5, 4, 5);
            txtServerName.MinimumSize = new Size(1, 16);
            txtServerName.Name = "txtServerName";
            txtServerName.Padding = new Padding(5);
            txtServerName.RectColor = Color.FromArgb(65, 100, 204);
            txtServerName.ShowText = false;
            txtServerName.Size = new Size(550, 30);
            txtServerName.Style = UIStyle.Custom;
            txtServerName.TabIndex = 1;
            txtServerName.TextAlignment = ContentAlignment.MiddleLeft;
            txtServerName.Watermark = "例如: KEPware.KEPServerEx.V4";
            // 
            // btnSetServerName
            // 
            btnSetServerName.Cursor = Cursors.Hand;
            btnSetServerName.FillColor = Color.FromArgb(59, 130, 246);
            btnSetServerName.FillColor2 = Color.FromArgb(29, 78, 216);
            btnSetServerName.FillColorGradient = true;
            btnSetServerName.Font = new Font("微软雅黑", 10F);
            btnSetServerName.Location = new Point(680, 20);
            btnSetServerName.MinimumSize = new Size(1, 1);
            btnSetServerName.Name = "btnSetServerName";
            btnSetServerName.Size = new Size(180, 30);
            btnSetServerName.Style = UIStyle.Custom;
            btnSetServerName.Symbol = 61639;
            btnSetServerName.SymbolSize = 20;
            btnSetServerName.TabIndex = 2;
            btnSetServerName.Text = "应用到当前模块";
            btnSetServerName.TipsFont = new Font("微软雅黑", 9F);
            btnSetServerName.Click += BtnSetServerName_Click;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(lblStatus);
            pnlBottom.Controls.Add(btnHelp);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.FillColor = Color.FromArgb(248, 249, 250);
            pnlBottom.FillColor2 = Color.FromArgb(248, 249, 250);
            pnlBottom.Font = new Font("微软雅黑", 12F);
            pnlBottom.Location = new Point(0, 700);
            pnlBottom.Margin = new Padding(4, 5, 4, 5);
            pnlBottom.MinimumSize = new Size(1, 1);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(15, 10, 15, 10);
            pnlBottom.Radius = 0;
            pnlBottom.RectColor = Color.FromArgb(233, 236, 239);
            pnlBottom.Size = new Size(1200, 70);
            pnlBottom.Style = UIStyle.Custom;
            pnlBottom.TabIndex = 2;
            pnlBottom.Text = null;
            pnlBottom.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("微软雅黑", 9F);
            lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatus.Location = new Point(15, 18);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(750, 34);
            lblStatus.Style = UIStyle.Custom;
            lblStatus.TabIndex = 0;
            lblStatus.Text = "提示: 选择模块后可以编辑点位,支持Excel/CSV批量导入";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.FillColor = Color.FromArgb(100, 116, 139);
            btnHelp.FillColor2 = Color.FromArgb(71, 85, 105);
            btnHelp.FillColorGradient = true;
            btnHelp.Font = new Font("微软雅黑", 11F);
            btnHelp.Location = new Point(780, 16);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.Radius = 8;
            btnHelp.RectColor = Color.FromArgb(100, 116, 139);
            btnHelp.Size = new Size(110, 38);
            btnHelp.Style = UIStyle.Custom;
            btnHelp.Symbol = 61736;
            btnHelp.TabIndex = 1;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("微软雅黑", 9F);
            btnHelp.Click += BtnHelp_Click;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.FromArgb(40, 167, 69);
            btnSave.FillColor2 = Color.FromArgb(34, 139, 34);
            btnSave.FillColorGradient = true;
            btnSave.Font = new Font("微软雅黑", 11F);
            btnSave.Location = new Point(900, 16);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.Radius = 8;
            btnSave.RectColor = Color.FromArgb(40, 167, 69);
            btnSave.Size = new Size(140, 38);
            btnSave.Style = UIStyle.Custom;
            btnSave.Symbol = 61639;
            btnSave.TabIndex = 2;
            btnSave.Text = "保存到配置";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(239, 68, 68);
            btnCancel.FillColor2 = Color.FromArgb(220, 38, 38);
            btnCancel.FillColorGradient = true;
            btnCancel.Font = new Font("微软雅黑", 11F);
            btnCancel.Location = new Point(1050, 16);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 8;
            btnCancel.RectColor = Color.FromArgb(239, 68, 68);
            btnCancel.Size = new Size(135, 38);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 3;
            btnCancel.Text = "关闭";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            btnCancel.Click += BtnCancel_Click;
            // 
            // Form_DefinePoint
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1200, 770);
            ControlBox = false;
            ControlBoxFillHoverColor = Color.FromArgb(163, 163, 163);
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            Controls.Add(pnlBottom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_DefinePoint";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Style = UIStyle.Custom;
            Text = "点位定义工具";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1200, 770);
            Load += Form_DefinePoint_Load;
            pnlLeft.ResumeLayout(false);
            grpModules.ResumeLayout(false);
            pnlModuleButtons.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            grpPoints.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPoints).EndInit();
            pnlPointButtons.ResumeLayout(false);
            pnlServerName.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIPanel pnlLeft;
        private Sunny.UI.UIGroupBox grpModules;
        private Sunny.UI.UIListBox lstModules;
        private Sunny.UI.UIPanel pnlModuleButtons;
        private Sunny.UI.UISymbolButton btnAddModule;
        private Sunny.UI.UISymbolButton btnDeleteModule;
        private Sunny.UI.UISymbolButton btnRenameModule;
        private Sunny.UI.UISymbolButton BtnDownloadTemplate;

        private Sunny.UI.UIPanel pnlRight;
        private Sunny.UI.UIPanel pnlServerName;
        private Sunny.UI.UILabel lblServerName;
        private Sunny.UI.UITextBox txtServerName;
        private Sunny.UI.UISymbolButton btnSetServerName;

        private Sunny.UI.UIGroupBox grpPoints;
        private Sunny.UI.UIDataGridView dgvPoints;
        private DataGridViewTextBoxColumn ColPointName;
        private DataGridViewTextBoxColumn ColPointAddress;

        private Sunny.UI.UIPanel pnlPointButtons;
        private Sunny.UI.UISymbolButton btnAddPoint;
        private Sunny.UI.UISymbolButton btnDeletePoint;
        private Sunny.UI.UISymbolButton btnImportExcel;
        private Sunny.UI.UISymbolButton btnExport;
        private Sunny.UI.UISymbolButton btnClearPoints;

        private Sunny.UI.UIPanel pnlBottom;
        private Sunny.UI.UILabel lblStatus;
        private Sunny.UI.UISymbolButton btnHelp;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UISymbolButton btnCancel;
    }
}
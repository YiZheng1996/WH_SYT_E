namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_VariableMonitor
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            pnlToolbar = new UIPanel();
            txtSearch = new UITextBox();
            lblRefreshInterval = new UILabel();
            cmbRefreshInterval = new UIComboBox();
            btnHistory = new UISymbolButton();
            btnExport = new UISymbolButton();
            chkOnlyAssigned = new UICheckBox();
            chkHighlightChanges = new UICheckBox();
            btnPauseResume = new UISymbolButton();
            pnlMain = new UIPanel();
            dgvVariables = new UIDataGridView();
            ColVarName = new DataGridViewTextBoxColumn();
            ColVarType = new DataGridViewTextBoxColumn();
            ColCurrentValue = new DataGridViewTextBoxColumn();
            ColLastUpdated = new DataGridViewTextBoxColumn();
            ColAssignedBy = new DataGridViewTextBoxColumn();
            pnlDetails = new UIPanel();
            grpDetails = new UIGroupBox();
            lblDetailVarName = new UILabel();
            lblDetailVarType = new UILabel();
            lblDetailCurrentValue = new UILabel();
            lblDetailLastUpdated = new UILabel();
            lblDetailIsAssigned = new UILabel();
            lblDetailSource = new UILabel();
            uiLine1 = new UILine();
            lblHistoryTitle = new UILabel();
            lstHistory = new UIListBox();
            btnModifyValue = new UISymbolButton();
            btnViewFullHistory = new UISymbolButton();
            btnClearHistory = new UISymbolButton();
            pnlStatus = new UIPanel();
            lblStats = new UILabel();
            pnlBottom = new UIPanel();
            btnHelp = new UISymbolButton();
            btnRefresh = new UISymbolButton();
            btnModify = new UISymbolButton();
            btnClose = new UISymbolButton();
            refreshTimer = new System.Windows.Forms.Timer(components);
            pnlToolbar.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVariables).BeginInit();
            pnlDetails.SuspendLayout();
            grpDetails.SuspendLayout();
            pnlStatus.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlToolbar
            // 
            pnlToolbar.Controls.Add(txtSearch);
            pnlToolbar.Controls.Add(lblRefreshInterval);
            pnlToolbar.Controls.Add(cmbRefreshInterval);
            pnlToolbar.Controls.Add(btnHistory);
            pnlToolbar.Controls.Add(btnExport);
            pnlToolbar.Controls.Add(chkOnlyAssigned);
            pnlToolbar.Controls.Add(chkHighlightChanges);
            pnlToolbar.Controls.Add(btnPauseResume);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.FillColor = Color.White;
            pnlToolbar.FillColor2 = Color.White;
            pnlToolbar.Font = new Font("微软雅黑", 12F);
            pnlToolbar.Location = new Point(0, 35);
            pnlToolbar.Margin = new Padding(4, 5, 4, 5);
            pnlToolbar.MinimumSize = new Size(1, 1);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new Padding(10);
            pnlToolbar.Radius = 0;
            pnlToolbar.RectColor = Color.FromArgb(233, 236, 239);
            pnlToolbar.Size = new Size(1000, 97);
            pnlToolbar.Style = UIStyle.Custom;
            pnlToolbar.TabIndex = 0;
            pnlToolbar.Text = null;
            pnlToolbar.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtSearch
            // 
            txtSearch.Cursor = Cursors.IBeam;
            txtSearch.Font = new Font("微软雅黑", 10F);
            txtSearch.Location = new Point(15, 15);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.MinimumSize = new Size(1, 16);
            txtSearch.Name = "txtSearch";
            txtSearch.Padding = new Padding(5);
            txtSearch.RectColor = Color.FromArgb(65, 100, 204);
            txtSearch.ShowText = false;
            txtSearch.Size = new Size(200, 30);
            txtSearch.Style = UIStyle.Custom;
            txtSearch.Symbol = 61442;
            txtSearch.SymbolSize = 20;
            txtSearch.TabIndex = 0;
            txtSearch.TextAlignment = ContentAlignment.MiddleLeft;
            txtSearch.Watermark = "搜索变量名...";
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // lblRefreshInterval
            // 
            lblRefreshInterval.BackColor = Color.Transparent;
            lblRefreshInterval.Font = new Font("微软雅黑", 10F);
            lblRefreshInterval.ForeColor = Color.FromArgb(48, 48, 48);
            lblRefreshInterval.Location = new Point(230, 15);
            lblRefreshInterval.Name = "lblRefreshInterval";
            lblRefreshInterval.Size = new Size(80, 30);
            lblRefreshInterval.Style = UIStyle.Custom;
            lblRefreshInterval.TabIndex = 1;
            lblRefreshInterval.Text = "刷新间隔:";
            lblRefreshInterval.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbRefreshInterval
            // 
            cmbRefreshInterval.DataSource = null;
            cmbRefreshInterval.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbRefreshInterval.FillColor = Color.White;
            cmbRefreshInterval.Font = new Font("微软雅黑", 10F);
            cmbRefreshInterval.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbRefreshInterval.Items.AddRange(new object[] { "100ms", "500ms", "1秒", "2秒", "5秒", "手动" });
            cmbRefreshInterval.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbRefreshInterval.Location = new Point(310, 15);
            cmbRefreshInterval.Margin = new Padding(4, 5, 4, 5);
            cmbRefreshInterval.MinimumSize = new Size(63, 0);
            cmbRefreshInterval.Name = "cmbRefreshInterval";
            cmbRefreshInterval.Padding = new Padding(0, 0, 30, 2);
            cmbRefreshInterval.RectColor = Color.FromArgb(65, 100, 204);
            cmbRefreshInterval.Size = new Size(100, 30);
            cmbRefreshInterval.SymbolSize = 24;
            cmbRefreshInterval.TabIndex = 2;
            cmbRefreshInterval.Text = "500ms";
            cmbRefreshInterval.TextAlignment = ContentAlignment.MiddleLeft;
            cmbRefreshInterval.Watermark = "";
            cmbRefreshInterval.SelectedIndexChanged += CmbRefreshInterval_SelectedIndexChanged;
            // 
            // btnHistory
            // 
            btnHistory.Cursor = Cursors.Hand;
            btnHistory.FillColor = Color.FromArgb(65, 100, 204);
            btnHistory.FillColor2 = Color.FromArgb(55, 90, 194);
            btnHistory.FillColorGradient = true;
            btnHistory.FillHoverColor = Color.FromArgb(75, 110, 214);
            btnHistory.Font = new Font("微软雅黑", 10F);
            btnHistory.Location = new Point(515, 15);
            btnHistory.MinimumSize = new Size(1, 1);
            btnHistory.Name = "btnHistory";
            btnHistory.RectColor = Color.FromArgb(65, 100, 204);
            btnHistory.Size = new Size(80, 30);
            btnHistory.Style = UIStyle.Custom;
            btnHistory.Symbol = 61555;
            btnHistory.SymbolSize = 20;
            btnHistory.TabIndex = 4;
            btnHistory.Text = "历史";
            btnHistory.TipsFont = new Font("微软雅黑", 9F);
            btnHistory.Click += BtnHistory_Click;
            // 
            // btnExport
            // 
            btnExport.Cursor = Cursors.Hand;
            btnExport.FillColor = Color.FromArgb(16, 185, 129);
            btnExport.FillColor2 = Color.FromArgb(5, 150, 105);
            btnExport.FillColorGradient = true;
            btnExport.FillHoverColor = Color.FromArgb(20, 184, 166);
            btnExport.Font = new Font("微软雅黑", 10F);
            btnExport.Location = new Point(605, 15);
            btnExport.MinimumSize = new Size(1, 1);
            btnExport.Name = "btnExport";
            btnExport.RectColor = Color.FromArgb(16, 185, 129);
            btnExport.Size = new Size(80, 30);
            btnExport.Style = UIStyle.Custom;
            btnExport.Symbol = 61639;
            btnExport.SymbolSize = 20;
            btnExport.TabIndex = 5;
            btnExport.Text = "导出";
            btnExport.TipsFont = new Font("微软雅黑", 9F);
            btnExport.Click += BtnExport_Click;
            // 
            // chkOnlyAssigned
            // 
            chkOnlyAssigned.BackColor = Color.Transparent;
            chkOnlyAssigned.Cursor = Cursors.Hand;
            chkOnlyAssigned.Font = new Font("微软雅黑", 10F);
            chkOnlyAssigned.ForeColor = Color.FromArgb(48, 48, 48);
            chkOnlyAssigned.Location = new Point(15, 61);
            chkOnlyAssigned.MinimumSize = new Size(1, 1);
            chkOnlyAssigned.Name = "chkOnlyAssigned";
            chkOnlyAssigned.Size = new Size(130, 23);
            chkOnlyAssigned.Style = UIStyle.Custom;
            chkOnlyAssigned.TabIndex = 6;
            chkOnlyAssigned.Text = "只显示已赋值";
            chkOnlyAssigned.CheckedChanged += ChkOnlyAssigned_CheckedChanged;
            // 
            // chkHighlightChanges
            // 
            chkHighlightChanges.BackColor = Color.Transparent;
            chkHighlightChanges.Checked = true;
            chkHighlightChanges.Cursor = Cursors.Hand;
            chkHighlightChanges.Font = new Font("微软雅黑", 10F);
            chkHighlightChanges.ForeColor = Color.FromArgb(48, 48, 48);
            chkHighlightChanges.Location = new Point(155, 61);
            chkHighlightChanges.MinimumSize = new Size(1, 1);
            chkHighlightChanges.Name = "chkHighlightChanges";
            chkHighlightChanges.Size = new Size(130, 23);
            chkHighlightChanges.Style = UIStyle.Custom;
            chkHighlightChanges.TabIndex = 7;
            chkHighlightChanges.Text = "值变化高亮";
            // 
            // btnPauseResume
            // 
            btnPauseResume.Cursor = Cursors.Hand;
            btnPauseResume.FillColor = Color.FromArgb(255, 193, 7);
            btnPauseResume.FillColor2 = Color.FromArgb(245, 166, 35);
            btnPauseResume.FillColorGradient = true;
            btnPauseResume.FillHoverColor = Color.FromArgb(255, 213, 79);
            btnPauseResume.Font = new Font("微软雅黑", 10F);
            btnPauseResume.Location = new Point(295, 59);
            btnPauseResume.MinimumSize = new Size(1, 1);
            btnPauseResume.Name = "btnPauseResume";
            btnPauseResume.RectColor = Color.FromArgb(255, 193, 7);
            btnPauseResume.Size = new Size(100, 28);
            btnPauseResume.Style = UIStyle.Custom;
            btnPauseResume.Symbol = 61516;
            btnPauseResume.SymbolSize = 20;
            btnPauseResume.TabIndex = 8;
            btnPauseResume.Text = "暂停刷新";
            btnPauseResume.TipsFont = new Font("微软雅黑", 9F);
            btnPauseResume.Click += BtnPauseResume_Click;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(dgvVariables);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.White;
            pnlMain.FillColor2 = Color.White;
            pnlMain.Font = new Font("微软雅黑", 12F);
            pnlMain.Location = new Point(0, 132);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.MinimumSize = new Size(1, 1);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(10, 10, 10, 5);
            pnlMain.Radius = 0;
            pnlMain.RectColor = Color.FromArgb(233, 236, 239);
            pnlMain.Size = new Size(1000, 313);
            pnlMain.Style = UIStyle.Custom;
            pnlMain.TabIndex = 1;
            pnlMain.Text = null;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // dgvVariables
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 250, 252);
            dgvVariables.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvVariables.BackgroundColor = Color.White;
            dgvVariables.BorderStyle = BorderStyle.None;
            dgvVariables.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvVariables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvVariables.ColumnHeadersHeight = 40;
            dgvVariables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvVariables.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColVarType, ColCurrentValue, ColLastUpdated, ColAssignedBy });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvVariables.DefaultCellStyle = dataGridViewCellStyle3;
            dgvVariables.Dock = DockStyle.Fill;
            dgvVariables.EnableHeadersVisualStyles = false;
            dgvVariables.Font = new Font("微软雅黑", 10F);
            dgvVariables.GridColor = Color.FromArgb(233, 236, 239);
            dgvVariables.Location = new Point(10, 10);
            dgvVariables.MultiSelect = false;
            dgvVariables.Name = "dgvVariables";
            dgvVariables.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(243, 249, 255);
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 10F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvVariables.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvVariables.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("微软雅黑", 10F);
            dgvVariables.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvVariables.RowTemplate.Height = 35;
            dgvVariables.ScrollBars = ScrollBars.Vertical;
            dgvVariables.SelectedIndex = -1;
            dgvVariables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVariables.Size = new Size(980, 298);
            dgvVariables.StripeOddColor = Color.FromArgb(250, 250, 252);
            dgvVariables.Style = UIStyle.Custom;
            dgvVariables.TabIndex = 0;
            dgvVariables.SelectionChanged += DgvVariables_SelectionChanged;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "变量名";
            ColVarName.Name = "ColVarName";
            ColVarName.ReadOnly = true;
            ColVarName.Width = 200;
            // 
            // ColVarType
            // 
            ColVarType.HeaderText = "类型";
            ColVarType.Name = "ColVarType";
            ColVarType.ReadOnly = true;
            // 
            // ColCurrentValue
            // 
            ColCurrentValue.HeaderText = "当前值";
            ColCurrentValue.Name = "ColCurrentValue";
            ColCurrentValue.ReadOnly = true;
            ColCurrentValue.Width = 200;
            // 
            // ColLastUpdated
            // 
            ColLastUpdated.HeaderText = "上次更新";
            ColLastUpdated.Name = "ColLastUpdated";
            ColLastUpdated.ReadOnly = true;
            ColLastUpdated.Width = 200;
            // 
            // ColAssignedBy
            // 
            ColAssignedBy.HeaderText = "赋值来源";
            ColAssignedBy.Name = "ColAssignedBy";
            ColAssignedBy.ReadOnly = true;
            ColAssignedBy.Width = 180;
            // 
            // pnlDetails
            // 
            pnlDetails.Controls.Add(grpDetails);
            pnlDetails.Dock = DockStyle.Bottom;
            pnlDetails.FillColor = Color.FromArgb(248, 249, 250);
            pnlDetails.FillColor2 = Color.FromArgb(248, 249, 250);
            pnlDetails.Font = new Font("微软雅黑", 12F);
            pnlDetails.Location = new Point(0, 445);
            pnlDetails.Margin = new Padding(4, 5, 4, 5);
            pnlDetails.MinimumSize = new Size(1, 1);
            pnlDetails.Name = "pnlDetails";
            pnlDetails.Padding = new Padding(10);
            pnlDetails.Radius = 0;
            pnlDetails.RectColor = Color.FromArgb(233, 236, 239);
            pnlDetails.Size = new Size(1000, 230);
            pnlDetails.Style = UIStyle.Custom;
            pnlDetails.TabIndex = 2;
            pnlDetails.Text = null;
            pnlDetails.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // grpDetails
            // 
            grpDetails.Controls.Add(lblDetailVarName);
            grpDetails.Controls.Add(lblDetailVarType);
            grpDetails.Controls.Add(lblDetailCurrentValue);
            grpDetails.Controls.Add(lblDetailLastUpdated);
            grpDetails.Controls.Add(lblDetailIsAssigned);
            grpDetails.Controls.Add(lblDetailSource);
            grpDetails.Controls.Add(uiLine1);
            grpDetails.Controls.Add(lblHistoryTitle);
            grpDetails.Controls.Add(lstHistory);
            grpDetails.Controls.Add(btnModifyValue);
            grpDetails.Controls.Add(btnViewFullHistory);
            grpDetails.Controls.Add(btnClearHistory);
            grpDetails.Dock = DockStyle.Fill;
            grpDetails.FillColor = Color.White;
            grpDetails.FillColor2 = Color.White;
            grpDetails.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            grpDetails.Location = new Point(10, 10);
            grpDetails.Margin = new Padding(4, 5, 4, 5);
            grpDetails.MinimumSize = new Size(1, 1);
            grpDetails.Name = "grpDetails";
            grpDetails.Padding = new Padding(10, 35, 10, 10);
            grpDetails.RectColor = Color.FromArgb(65, 100, 204);
            grpDetails.Size = new Size(980, 210);
            grpDetails.Style = UIStyle.Custom;
            grpDetails.TabIndex = 0;
            grpDetails.Text = "📊 变量详情";
            grpDetails.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblDetailVarName
            // 
            lblDetailVarName.BackColor = Color.Transparent;
            lblDetailVarName.Font = new Font("微软雅黑", 10F);
            lblDetailVarName.ForeColor = Color.FromArgb(48, 48, 48);
            lblDetailVarName.Location = new Point(15, 40);
            lblDetailVarName.Name = "lblDetailVarName";
            lblDetailVarName.Size = new Size(450, 25);
            lblDetailVarName.Style = UIStyle.Custom;
            lblDetailVarName.TabIndex = 0;
            lblDetailVarName.Text = "变量名: 未选择";
            lblDetailVarName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailVarType
            // 
            lblDetailVarType.BackColor = Color.Transparent;
            lblDetailVarType.Font = new Font("微软雅黑", 10F);
            lblDetailVarType.ForeColor = Color.FromArgb(48, 48, 48);
            lblDetailVarType.Location = new Point(15, 70);
            lblDetailVarType.Name = "lblDetailVarType";
            lblDetailVarType.Size = new Size(450, 25);
            lblDetailVarType.Style = UIStyle.Custom;
            lblDetailVarType.TabIndex = 1;
            lblDetailVarType.Text = "类型: -";
            lblDetailVarType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailCurrentValue
            // 
            lblDetailCurrentValue.BackColor = Color.Transparent;
            lblDetailCurrentValue.Font = new Font("微软雅黑", 10F);
            lblDetailCurrentValue.ForeColor = Color.FromArgb(48, 48, 48);
            lblDetailCurrentValue.Location = new Point(15, 100);
            lblDetailCurrentValue.Name = "lblDetailCurrentValue";
            lblDetailCurrentValue.Size = new Size(450, 25);
            lblDetailCurrentValue.Style = UIStyle.Custom;
            lblDetailCurrentValue.TabIndex = 2;
            lblDetailCurrentValue.Text = "当前值: -";
            lblDetailCurrentValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailLastUpdated
            // 
            lblDetailLastUpdated.BackColor = Color.Transparent;
            lblDetailLastUpdated.Font = new Font("微软雅黑", 10F);
            lblDetailLastUpdated.ForeColor = Color.FromArgb(48, 48, 48);
            lblDetailLastUpdated.Location = new Point(15, 130);
            lblDetailLastUpdated.Name = "lblDetailLastUpdated";
            lblDetailLastUpdated.Size = new Size(450, 25);
            lblDetailLastUpdated.Style = UIStyle.Custom;
            lblDetailLastUpdated.TabIndex = 3;
            lblDetailLastUpdated.Text = "上次更新: -";
            lblDetailLastUpdated.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailIsAssigned
            // 
            lblDetailIsAssigned.BackColor = Color.Transparent;
            lblDetailIsAssigned.Font = new Font("微软雅黑", 10F);
            lblDetailIsAssigned.ForeColor = Color.FromArgb(48, 48, 48);
            lblDetailIsAssigned.Location = new Point(475, 40);
            lblDetailIsAssigned.Name = "lblDetailIsAssigned";
            lblDetailIsAssigned.Size = new Size(250, 25);
            lblDetailIsAssigned.Style = UIStyle.Custom;
            lblDetailIsAssigned.TabIndex = 4;
            lblDetailIsAssigned.Text = "是否被步骤赋值: -";
            lblDetailIsAssigned.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailSource
            // 
            lblDetailSource.BackColor = Color.Transparent;
            lblDetailSource.Font = new Font("微软雅黑", 10F);
            lblDetailSource.ForeColor = Color.FromArgb(48, 48, 48);
            lblDetailSource.Location = new Point(475, 70);
            lblDetailSource.Name = "lblDetailSource";
            lblDetailSource.Size = new Size(450, 25);
            lblDetailSource.Style = UIStyle.Custom;
            lblDetailSource.TabIndex = 5;
            lblDetailSource.Text = "赋值来源: -";
            lblDetailSource.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("微软雅黑", 10F);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.FromArgb(233, 236, 239);
            uiLine1.Location = new Point(475, 100);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(490, 3);
            uiLine1.TabIndex = 6;
            // 
            // lblHistoryTitle
            // 
            lblHistoryTitle.BackColor = Color.Transparent;
            lblHistoryTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblHistoryTitle.ForeColor = Color.FromArgb(48, 48, 48);
            lblHistoryTitle.Location = new Point(475, 105);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Size = new Size(200, 25);
            lblHistoryTitle.Style = UIStyle.Custom;
            lblHistoryTitle.TabIndex = 7;
            lblHistoryTitle.Text = "值变化历史 (最近10次)";
            lblHistoryTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstHistory
            // 
            lstHistory.FillColor = Color.White;
            lstHistory.Font = new Font("Consolas", 9F);
            lstHistory.HoverColor = Color.FromArgb(155, 200, 255);
            lstHistory.ItemSelectBackColor = Color.FromArgb(235, 243, 255);
            lstHistory.ItemSelectForeColor = Color.FromArgb(48, 48, 48);
            lstHistory.Location = new Point(475, 135);
            lstHistory.Margin = new Padding(4, 5, 4, 5);
            lstHistory.MinimumSize = new Size(1, 1);
            lstHistory.Name = "lstHistory";
            lstHistory.Padding = new Padding(2);
            lstHistory.RectColor = Color.FromArgb(233, 236, 239);
            lstHistory.ShowText = false;
            lstHistory.Size = new Size(350, 60);
            lstHistory.Style = UIStyle.Custom;
            lstHistory.TabIndex = 8;
            lstHistory.Text = null;
            // 
            // btnModifyValue
            // 
            btnModifyValue.Cursor = Cursors.Hand;
            btnModifyValue.FillColor = Color.FromArgb(255, 193, 7);
            btnModifyValue.FillColor2 = Color.FromArgb(245, 166, 35);
            btnModifyValue.FillColorGradient = true;
            btnModifyValue.Font = new Font("微软雅黑", 9F);
            btnModifyValue.Location = new Point(835, 135);
            btnModifyValue.MinimumSize = new Size(1, 1);
            btnModifyValue.Name = "btnModifyValue";
            btnModifyValue.RectColor = Color.FromArgb(255, 193, 7);
            btnModifyValue.Size = new Size(130, 28);
            btnModifyValue.Style = UIStyle.Custom;
            btnModifyValue.Symbol = 61508;
            btnModifyValue.SymbolSize = 18;
            btnModifyValue.TabIndex = 9;
            btnModifyValue.Text = "手动修改值";
            btnModifyValue.TipsFont = new Font("微软雅黑", 9F);
            btnModifyValue.Click += BtnModifyValue_Click;
            // 
            // btnViewFullHistory
            // 
            btnViewFullHistory.Cursor = Cursors.Hand;
            btnViewFullHistory.FillColor = Color.FromArgb(65, 100, 204);
            btnViewFullHistory.FillColor2 = Color.FromArgb(55, 90, 194);
            btnViewFullHistory.FillColorGradient = true;
            btnViewFullHistory.Font = new Font("微软雅黑", 9F);
            btnViewFullHistory.Location = new Point(835, 167);
            btnViewFullHistory.MinimumSize = new Size(1, 1);
            btnViewFullHistory.Name = "btnViewFullHistory";
            btnViewFullHistory.RectColor = Color.FromArgb(65, 100, 204);
            btnViewFullHistory.Size = new Size(130, 28);
            btnViewFullHistory.Style = UIStyle.Custom;
            btnViewFullHistory.Symbol = 61555;
            btnViewFullHistory.SymbolSize = 18;
            btnViewFullHistory.TabIndex = 10;
            btnViewFullHistory.Text = "完整历史";
            btnViewFullHistory.TipsFont = new Font("微软雅黑", 9F);
            btnViewFullHistory.Click += BtnViewFullHistory_Click;
            // 
            // btnClearHistory
            // 
            btnClearHistory.Cursor = Cursors.Hand;
            btnClearHistory.FillColor = Color.FromArgb(239, 68, 68);
            btnClearHistory.FillColor2 = Color.FromArgb(220, 38, 38);
            btnClearHistory.FillColorGradient = true;
            btnClearHistory.Font = new Font("微软雅黑", 9F);
            btnClearHistory.Location = new Point(15, 165);
            btnClearHistory.MinimumSize = new Size(1, 1);
            btnClearHistory.Name = "btnClearHistory";
            btnClearHistory.RectColor = Color.FromArgb(239, 68, 68);
            btnClearHistory.Size = new Size(110, 28);
            btnClearHistory.Style = UIStyle.Custom;
            btnClearHistory.Symbol = 61460;
            btnClearHistory.SymbolSize = 18;
            btnClearHistory.TabIndex = 11;
            btnClearHistory.Text = "清除历史";
            btnClearHistory.TipsFont = new Font("微软雅黑", 9F);
            btnClearHistory.Click += BtnClearHistory_Click;
            // 
            // pnlStatus
            // 
            pnlStatus.Controls.Add(lblStats);
            pnlStatus.Dock = DockStyle.Bottom;
            pnlStatus.FillColor = Color.FromArgb(240, 242, 245);
            pnlStatus.FillColor2 = Color.FromArgb(240, 242, 245);
            pnlStatus.Font = new Font("微软雅黑", 12F);
            pnlStatus.Location = new Point(0, 675);
            pnlStatus.Margin = new Padding(4, 5, 4, 5);
            pnlStatus.MinimumSize = new Size(1, 1);
            pnlStatus.Name = "pnlStatus";
            pnlStatus.Padding = new Padding(10, 5, 10, 5);
            pnlStatus.Radius = 0;
            pnlStatus.RectColor = Color.FromArgb(233, 236, 239);
            pnlStatus.Size = new Size(1000, 35);
            pnlStatus.Style = UIStyle.Custom;
            pnlStatus.TabIndex = 3;
            pnlStatus.Text = null;
            pnlStatus.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblStats
            // 
            lblStats.BackColor = Color.Transparent;
            lblStats.Dock = DockStyle.Fill;
            lblStats.Font = new Font("微软雅黑", 9F);
            lblStats.ForeColor = Color.FromArgb(100, 116, 139);
            lblStats.Location = new Point(10, 5);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(980, 25);
            lblStats.Style = UIStyle.Custom;
            lblStats.TabIndex = 0;
            lblStats.Text = "总变量数: 0  已赋值: 0  未赋值: 0  刷新频率: 500ms  最后刷新: - ";
            lblStats.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnHelp);
            pnlBottom.Controls.Add(btnRefresh);
            pnlBottom.Controls.Add(btnModify);
            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.FillColor = Color.FromArgb(248, 249, 250);
            pnlBottom.FillColor2 = Color.FromArgb(248, 249, 250);
            pnlBottom.Font = new Font("微软雅黑", 12F);
            pnlBottom.Location = new Point(0, 710);
            pnlBottom.Margin = new Padding(4, 5, 4, 5);
            pnlBottom.MinimumSize = new Size(1, 1);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(15, 10, 15, 10);
            pnlBottom.Radius = 0;
            pnlBottom.RectColor = Color.FromArgb(233, 236, 239);
            pnlBottom.Size = new Size(1000, 70);
            pnlBottom.Style = UIStyle.Custom;
            pnlBottom.TabIndex = 4;
            pnlBottom.Text = null;
            pnlBottom.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.FillColor = Color.FromArgb(100, 116, 139);
            btnHelp.FillColor2 = Color.FromArgb(71, 85, 105);
            btnHelp.FillColorGradient = true;
            btnHelp.Font = new Font("微软雅黑", 11F);
            btnHelp.Location = new Point(15, 16);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.Radius = 8;
            btnHelp.RectColor = Color.FromArgb(100, 116, 139);
            btnHelp.Size = new Size(110, 38);
            btnHelp.Style = UIStyle.Custom;
            btnHelp.Symbol = 61736;
            btnHelp.TabIndex = 0;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("微软雅黑", 9F);
            btnHelp.Click += BtnHelp_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FillColor = Color.FromArgb(59, 130, 246);
            btnRefresh.FillColor2 = Color.FromArgb(29, 78, 216);
            btnRefresh.FillColorGradient = true;
            btnRefresh.Font = new Font("微软雅黑", 11F);
            btnRefresh.Location = new Point(644, 16);
            btnRefresh.MinimumSize = new Size(1, 1);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Radius = 8;
            btnRefresh.RectColor = Color.FromArgb(59, 130, 246);
            btnRefresh.Size = new Size(110, 38);
            btnRefresh.Style = UIStyle.Custom;
            btnRefresh.Symbol = 61473;
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "刷新数据";
            btnRefresh.TipsFont = new Font("微软雅黑", 9F);
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // btnModify
            // 
            btnModify.Cursor = Cursors.Hand;
            btnModify.FillColor = Color.FromArgb(255, 193, 7);
            btnModify.FillColor2 = Color.FromArgb(245, 166, 35);
            btnModify.FillColorGradient = true;
            btnModify.Font = new Font("微软雅黑", 11F);
            btnModify.Location = new Point(760, 16);
            btnModify.MinimumSize = new Size(1, 1);
            btnModify.Name = "btnModify";
            btnModify.Radius = 8;
            btnModify.RectColor = Color.FromArgb(255, 193, 7);
            btnModify.Size = new Size(110, 38);
            btnModify.Style = UIStyle.Custom;
            btnModify.Symbol = 61508;
            btnModify.TabIndex = 2;
            btnModify.Text = "修改变量";
            btnModify.TipsFont = new Font("微软雅黑", 9F);
            btnModify.Click += BtnModify_Click;
            // 
            // btnClose
            // 
            btnClose.Cursor = Cursors.Hand;
            btnClose.FillColor = Color.FromArgb(239, 68, 68);
            btnClose.FillColor2 = Color.FromArgb(220, 38, 38);
            btnClose.FillColorGradient = true;
            btnClose.Font = new Font("微软雅黑", 11F);
            btnClose.Location = new Point(878, 16);
            btnClose.MinimumSize = new Size(1, 1);
            btnClose.Name = "btnClose";
            btnClose.Radius = 8;
            btnClose.RectColor = Color.FromArgb(239, 68, 68);
            btnClose.Size = new Size(110, 38);
            btnClose.Style = UIStyle.Custom;
            btnClose.Symbol = 61453;
            btnClose.TabIndex = 3;
            btnClose.Text = "关闭";
            btnClose.TipsFont = new Font("微软雅黑", 9F);
            btnClose.Click += BtnClose_Click;
            // 
            // refreshTimer
            // 
            refreshTimer.Interval = 500;
            refreshTimer.Tick += RefreshTimer_Tick;
            // 
            // Form_VariableMonitor
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1000, 780);
            ControlBoxFillHoverColor = Color.FromArgb(163, 163, 163);
            Controls.Add(pnlMain);
            Controls.Add(pnlDetails);
            Controls.Add(pnlStatus);
            Controls.Add(pnlBottom);
            Controls.Add(pnlToolbar);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_VariableMonitor";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Style = UIStyle.Custom;
            Text = "变量监控";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1000, 750);
            FormClosing += Form_VariableMonitor_FormClosing;
            Load += Form_VariableMonitor_Load;
            pnlToolbar.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVariables).EndInit();
            pnlDetails.ResumeLayout(false);
            grpDetails.ResumeLayout(false);
            pnlStatus.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIPanel pnlToolbar;
        private Sunny.UI.UITextBox txtSearch;
        private Sunny.UI.UILabel lblRefreshInterval;
        private Sunny.UI.UIComboBox cmbRefreshInterval;
        private Sunny.UI.UISymbolButton btnHistory;
        private Sunny.UI.UISymbolButton btnExport;
        private Sunny.UI.UICheckBox chkOnlyAssigned;
        private Sunny.UI.UICheckBox chkHighlightChanges;
        private Sunny.UI.UISymbolButton btnPauseResume;

        private Sunny.UI.UIPanel pnlMain;
        private Sunny.UI.UIDataGridView dgvVariables;
        private DataGridViewTextBoxColumn ColVarName;
        private DataGridViewTextBoxColumn ColVarType;
        private DataGridViewTextBoxColumn ColCurrentValue;
        private DataGridViewTextBoxColumn ColLastUpdated;
        private DataGridViewTextBoxColumn ColAssignedBy;

        private Sunny.UI.UIPanel pnlDetails;
        private Sunny.UI.UIGroupBox grpDetails;
        private Sunny.UI.UILabel lblDetailVarName;
        private Sunny.UI.UILabel lblDetailVarType;
        private Sunny.UI.UILabel lblDetailCurrentValue;
        private Sunny.UI.UILabel lblDetailLastUpdated;
        private Sunny.UI.UILabel lblDetailIsAssigned;
        private Sunny.UI.UILabel lblDetailSource;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UILabel lblHistoryTitle;
        private Sunny.UI.UIListBox lstHistory;
        private Sunny.UI.UISymbolButton btnModifyValue;
        private Sunny.UI.UISymbolButton btnViewFullHistory;
        private Sunny.UI.UISymbolButton btnClearHistory;

        private Sunny.UI.UIPanel pnlStatus;
        private Sunny.UI.UILabel lblStats;

        private Sunny.UI.UIPanel pnlBottom;
        private Sunny.UI.UISymbolButton btnHelp;
        private Sunny.UI.UISymbolButton btnRefresh;
        private Sunny.UI.UISymbolButton btnModify;
        private Sunny.UI.UISymbolButton btnClose;

        private System.Windows.Forms.Timer refreshTimer;
    }
}
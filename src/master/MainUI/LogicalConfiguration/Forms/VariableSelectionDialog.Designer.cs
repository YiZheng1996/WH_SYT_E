namespace MainUI.LogicalConfiguration.Forms
{
    partial class VariableSelectionDialog
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panelSearch = new Panel();
            txtSearch = new UITextBox();
            cmbFilter = new UIComboBox();
            lblFilter = new Label();
            panelMain = new Panel();
            dgvVariables = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colType = new DataGridViewTextBoxColumn();
            colValue = new DataGridViewTextBoxColumn();
            panelStats = new Panel();
            lblStats = new Label();
            panelDetails = new Panel();
            lblDetailsTitle = new Label();
            txtDetails = new RichTextBox();
            panelBottom = new Panel();
            btnOK = new UISymbolButton();
            BtnCancel = new UISymbolButton();
            panelSearch.SuspendLayout();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVariables).BeginInit();
            panelStats.SuspendLayout();
            panelDetails.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelSearch
            // 
            panelSearch.BackColor = Color.White;
            panelSearch.Controls.Add(txtSearch);
            panelSearch.Controls.Add(cmbFilter);
            panelSearch.Controls.Add(lblFilter);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(0, 35);
            panelSearch.Name = "panelSearch";
            panelSearch.Padding = new Padding(20, 15, 20, 15);
            panelSearch.Size = new Size(900, 80);
            panelSearch.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Cursor = Cursors.IBeam;
            txtSearch.Font = new Font("微软雅黑", 10F);
            txtSearch.Location = new Point(20, 20);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.MinimumSize = new Size(1, 16);
            txtSearch.Name = "txtSearch";
            txtSearch.Padding = new Padding(5);
            txtSearch.RectColor = Color.FromArgb(220, 220, 220);
            txtSearch.ShowText = false;
            txtSearch.Size = new Size(440, 36);
            txtSearch.TabIndex = 0;
            txtSearch.TextAlignment = ContentAlignment.MiddleLeft;
            txtSearch.Watermark = "🔍 输入变量名称搜索...";
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // cmbFilter
            // 
            cmbFilter.DataSource = null;
            cmbFilter.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbFilter.FillColor = Color.White;
            cmbFilter.Font = new Font("微软雅黑", 10F);
            cmbFilter.ItemHoverColor = Color.FromArgb(230, 240, 255);
            cmbFilter.Items.AddRange(new object[] { "全部变量", "字符串", "数字", "日期时间", "布尔值", "其他" });
            cmbFilter.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbFilter.Location = new Point(560, 20);
            cmbFilter.Margin = new Padding(4, 5, 4, 5);
            cmbFilter.MinimumSize = new Size(63, 0);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Padding = new Padding(0, 0, 30, 2);
            cmbFilter.RectColor = Color.FromArgb(220, 220, 220);
            cmbFilter.Size = new Size(150, 36);
            cmbFilter.SymbolSize = 24;
            cmbFilter.TabIndex = 2;
            cmbFilter.Text = "全部变量";
            cmbFilter.TextAlignment = ContentAlignment.MiddleLeft;
            cmbFilter.Watermark = "";
            cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
            // 
            // lblFilter
            // 
            lblFilter.AutoSize = true;
            lblFilter.Font = new Font("微软雅黑", 10F);
            lblFilter.ForeColor = Color.FromArgb(80, 80, 80);
            lblFilter.Location = new Point(480, 28);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(68, 20);
            lblFilter.TabIndex = 3;
            lblFilter.Text = "类型筛选:";
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(dgvVariables);
            panelMain.Controls.Add(panelStats);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 115);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20, 10, 20, 10);
            panelMain.Size = new Size(900, 425);
            panelMain.TabIndex = 2;
            // 
            // dgvVariables
            // 
            dgvVariables.AllowUserToAddRows = false;
            dgvVariables.AllowUserToDeleteRows = false;
            dgvVariables.AllowUserToResizeRows = false;
            dgvVariables.BackgroundColor = Color.White;
            dgvVariables.BorderStyle = BorderStyle.None;
            dgvVariables.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVariables.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvVariables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvVariables.ColumnHeadersHeight = 40;
            dgvVariables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvVariables.Columns.AddRange(new DataGridViewColumn[] { colName, colType, colValue });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("微软雅黑", 9.5F);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dataGridViewCellStyle5.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvVariables.DefaultCellStyle = dataGridViewCellStyle5;
            dgvVariables.Dock = DockStyle.Fill;
            dgvVariables.EnableHeadersVisualStyles = false;
            dgvVariables.GridColor = Color.FromArgb(240, 240, 240);
            dgvVariables.Location = new Point(20, 10);
            dgvVariables.MultiSelect = false;
            dgvVariables.Name = "dgvVariables";
            dgvVariables.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("微软雅黑", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvVariables.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvVariables.RowHeadersVisible = false;
            dgvVariables.RowHeadersWidth = 51;
            dgvVariables.RowTemplate.Height = 35;
            dgvVariables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVariables.Size = new Size(860, 375);
            dgvVariables.TabIndex = 0;
            dgvVariables.CellDoubleClick += DgvVariables_CellDoubleClick;
            dgvVariables.SelectionChanged += DgvVariables_SelectionChanged;
            dgvVariables.KeyDown += DgvVariables_KeyDown;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.HeaderText = "变量名称";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.Width = 250;
            // 
            // colType
            // 
            colType.DataPropertyName = "Type";
            colType.HeaderText = "数据类型";
            colType.MinimumWidth = 6;
            colType.Name = "colType";
            colType.ReadOnly = true;
            colType.Width = 150;
            // 
            // colValue
            // 
            colValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colValue.DataPropertyName = "ValuePreview";
            colValue.HeaderText = "当前值";
            colValue.MinimumWidth = 6;
            colValue.Name = "colValue";
            colValue.ReadOnly = true;
            // 
            // panelStats
            // 
            panelStats.BackColor = Color.FromArgb(250, 250, 250);
            panelStats.Controls.Add(lblStats);
            panelStats.Dock = DockStyle.Bottom;
            panelStats.Location = new Point(20, 385);
            panelStats.Name = "panelStats";
            panelStats.Size = new Size(860, 30);
            panelStats.TabIndex = 1;
            // 
            // lblStats
            // 
            lblStats.Dock = DockStyle.Fill;
            lblStats.Font = new Font("微软雅黑", 9F);
            lblStats.ForeColor = Color.Gray;
            lblStats.Location = new Point(0, 0);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(860, 30);
            lblStats.TabIndex = 0;
            lblStats.Text = "共 0 个变量";
            lblStats.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelDetails
            // 
            panelDetails.BackColor = Color.FromArgb(248, 250, 252);
            panelDetails.Controls.Add(lblDetailsTitle);
            panelDetails.Controls.Add(txtDetails);
            panelDetails.Dock = DockStyle.Bottom;
            panelDetails.Location = new Point(0, 540);
            panelDetails.Name = "panelDetails";
            panelDetails.Padding = new Padding(20, 10, 20, 10);
            panelDetails.Size = new Size(900, 142);
            panelDetails.TabIndex = 3;
            // 
            // lblDetailsTitle
            // 
            lblDetailsTitle.AutoSize = true;
            lblDetailsTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblDetailsTitle.ForeColor = Color.FromArgb(80, 80, 80);
            lblDetailsTitle.Location = new Point(20, 12);
            lblDetailsTitle.Name = "lblDetailsTitle";
            lblDetailsTitle.Size = new Size(93, 19);
            lblDetailsTitle.TabIndex = 0;
            lblDetailsTitle.Text = "变量详细信息";
            // 
            // txtDetails
            // 
            txtDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDetails.BackColor = Color.White;
            txtDetails.BorderStyle = BorderStyle.FixedSingle;
            txtDetails.Font = new Font("微软雅黑", 9F);
            txtDetails.ForeColor = Color.FromArgb(80, 80, 80);
            txtDetails.Location = new Point(20, 40);
            txtDetails.Name = "txtDetails";
            txtDetails.ReadOnly = true;
            txtDetails.Size = new Size(860, 92);
            txtDetails.TabIndex = 1;
            txtDetails.Text = "请选择一个变量查看详细信息";
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(BtnCancel);
            panelBottom.Controls.Add(btnOK);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 682);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(900, 70);
            panelBottom.TabIndex = 4;
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.FillColor = Color.FromArgb(65, 100, 204);
            btnOK.FillHoverColor = Color.FromArgb(88, 165, 49);
            btnOK.FillPressColor = Color.FromArgb(56, 106, 32);
            btnOK.FillSelectedColor = Color.FromArgb(56, 106, 32);
            btnOK.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnOK.Location = new Point(598, 16);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.RectColor = Color.FromArgb(65, 100, 204);
            btnOK.RectHoverColor = Color.FromArgb(88, 165, 49);
            btnOK.Size = new Size(130, 38);
            btnOK.Symbol = 61639;
            btnOK.SymbolSize = 28;
            btnOK.TabIndex = 6;
            btnOK.Text = "保存";
            btnOK.TipsFont = new Font("微软雅黑", 9F);
            btnOK.Click += BtnOK_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Cursor = Cursors.Hand;
            BtnCancel.FillColor = Color.FromArgb(230, 80, 80);
            BtnCancel.FillHoverColor = Color.FromArgb(232, 127, 128);
            BtnCancel.FillPressColor = Color.FromArgb(202, 87, 89);
            BtnCancel.FillSelectedColor = Color.FromArgb(202, 87, 89);
            BtnCancel.Font = new Font("微软雅黑", 10F);
            BtnCancel.Location = new Point(750, 16);
            BtnCancel.MinimumSize = new Size(1, 1);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.RectColor = Color.FromArgb(230, 80, 80);
            BtnCancel.RectHoverColor = Color.FromArgb(232, 127, 128);
            BtnCancel.Size = new Size(130, 38);
            BtnCancel.Symbol = 61453;
            BtnCancel.TabIndex = 7;
            BtnCancel.Text = "取消";
            BtnCancel.TipsFont = new Font("微软雅黑", 9F);
            BtnCancel.Click += BtnCancel_Click;
            // 
            // VariableSelectionDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(900, 752);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelDetails);
            Controls.Add(panelBottom);
            Controls.Add(panelSearch);
            Font = new Font("微软雅黑", 9F);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(900, 700);
            Name = "VariableSelectionDialog";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "选择工作流变量";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 13F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 900, 700);
            Load += VariableSelectionDialog_Load;
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVariables).EndInit();
            panelStats.ResumeLayout(false);
            panelDetails.ResumeLayout(false);
            panelDetails.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSearch;
        private Sunny.UI.UITextBox txtSearch;
        private Sunny.UI.UIComboBox cmbFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView dgvVariables;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.RichTextBox txtDetails;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private UISymbolButton btnOK;
        private UISymbolButton BtnCancel;
    }
}
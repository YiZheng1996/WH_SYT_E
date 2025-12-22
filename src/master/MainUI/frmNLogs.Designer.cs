namespace MainUI
{
    partial class frmNLogs
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
            pnlTop = new Panel();
            btnClose = new AntdUI.Button();
            btnExport = new AntdUI.Button();
            btnToday = new AntdUI.Button();
            txtSearch = new UITextBox();
            lblSearchKeyword = new AntdUI.Label();
            btnSearch = new AntdUI.Button();
            dtpEndBig = new UIDatePicker();
            lblEndDate = new AntdUI.Label();
            dtpStartBig = new UIDatePicker();
            lblStartDate = new AntdUI.Label();
            cboType = new UIComboBox();
            lblLogType = new AntdUI.Label();
            splitContainer = new SplitContainer();
            dgvLogs = new UIDataGridView();
            pnlDetail = new Panel();
            txtDetail = new TextBox();
            pnlDetailTop = new Panel();
            btnCopy = new AntdUI.Button();
            lblDetailTitle = new AntdUI.Label();
            pnlBottom = new Panel();
            lblStatus = new AntdUI.Label();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            pnlDetail.SuspendLayout();
            pnlDetailTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(btnClose);
            pnlTop.Controls.Add(btnExport);
            pnlTop.Controls.Add(btnToday);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Controls.Add(lblSearchKeyword);
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Controls.Add(dtpEndBig);
            pnlTop.Controls.Add(lblEndDate);
            pnlTop.Controls.Add(dtpStartBig);
            pnlTop.Controls.Add(lblStartDate);
            pnlTop.Controls.Add(cboType);
            pnlTop.Controls.Add(lblLogType);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 35);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1109, 100);
            pnlTop.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(984, 15);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 37);
            btnClose.TabIndex = 14;
            btnClose.Text = "关闭";
            btnClose.Type = AntdUI.TTypeMini.Error;
            btnClose.Click += btnClose_Click;
            // 
            // btnExport
            // 
            btnExport.Font = new Font("微软雅黑", 12F);
            btnExport.Location = new Point(760, 15);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(100, 37);
            btnExport.TabIndex = 12;
            btnExport.Text = "导出Excel";
            btnExport.Type = AntdUI.TTypeMini.Primary;
            btnExport.Click += btnExport_Click;
            // 
            // btnToday
            // 
            btnToday.Font = new Font("微软雅黑", 12F);
            btnToday.Location = new Point(872, 15);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(100, 37);
            btnToday.TabIndex = 11;
            btnToday.Text = "今天";
            btnToday.Type = AntdUI.TTypeMini.Warn;
            btnToday.Click += btnToday_Click;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("微软雅黑", 11F);
            txtSearch.Location = new Point(459, 60);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.MinimumSize = new Size(1, 16);
            txtSearch.Name = "txtSearch";
            txtSearch.Padding = new Padding(5);
            txtSearch.RectColor = Color.FromArgb(65, 100, 204);
            txtSearch.ShowText = false;
            txtSearch.Size = new Size(626, 30);
            txtSearch.TabIndex = 8;
            txtSearch.TextAlignment = ContentAlignment.MiddleLeft;
            txtSearch.Watermark = "输入关键字搜索...";
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearchKeyword
            // 
            lblSearchKeyword.Font = new Font("微软雅黑", 12F);
            lblSearchKeyword.Location = new Point(370, 57);
            lblSearchKeyword.Name = "lblSearchKeyword";
            lblSearchKeyword.Size = new Size(70, 30);
            lblSearchKeyword.TabIndex = 7;
            lblSearchKeyword.Text = "关键字:";
            lblSearchKeyword.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("微软雅黑", 12F);
            btnSearch.Location = new Point(648, 15);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 37);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "查询";
            btnSearch.Type = AntdUI.TTypeMini.Primary;
            btnSearch.Click += btnSearch_Click;
            // 
            // dtpEndBig
            // 
            dtpEndBig.FillColor = Color.White;
            dtpEndBig.Font = new Font("微软雅黑", 12F);
            dtpEndBig.Location = new Point(459, 18);
            dtpEndBig.Margin = new Padding(4, 5, 4, 5);
            dtpEndBig.MaxLength = 10;
            dtpEndBig.MinimumSize = new Size(63, 0);
            dtpEndBig.Name = "dtpEndBig";
            dtpEndBig.Padding = new Padding(0, 0, 30, 2);
            dtpEndBig.RectColor = Color.FromArgb(65, 100, 204);
            dtpEndBig.Size = new Size(176, 30);
            dtpEndBig.SymbolDropDown = 61555;
            dtpEndBig.SymbolNormal = 61555;
            dtpEndBig.SymbolSize = 24;
            dtpEndBig.TabIndex = 5;
            dtpEndBig.Text = "2025-12-22";
            dtpEndBig.TextAlignment = ContentAlignment.MiddleLeft;
            dtpEndBig.Value = new DateTime(2025, 12, 22, 11, 25, 5, 151);
            dtpEndBig.Watermark = "";
            // 
            // lblEndDate
            // 
            lblEndDate.Font = new Font("微软雅黑", 12F);
            lblEndDate.Location = new Point(350, 18);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(90, 30);
            lblEndDate.TabIndex = 4;
            lblEndDate.Text = "结束日期:";
            lblEndDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpStartBig
            // 
            dtpStartBig.FillColor = Color.White;
            dtpStartBig.Font = new Font("微软雅黑", 12F);
            dtpStartBig.Location = new Point(141, 17);
            dtpStartBig.Margin = new Padding(4, 5, 4, 5);
            dtpStartBig.MaxLength = 10;
            dtpStartBig.MinimumSize = new Size(63, 0);
            dtpStartBig.Name = "dtpStartBig";
            dtpStartBig.Padding = new Padding(0, 0, 30, 2);
            dtpStartBig.RectColor = Color.FromArgb(65, 100, 204);
            dtpStartBig.Size = new Size(176, 30);
            dtpStartBig.SymbolDropDown = 61555;
            dtpStartBig.SymbolNormal = 61555;
            dtpStartBig.SymbolSize = 24;
            dtpStartBig.TabIndex = 3;
            dtpStartBig.Text = "2025-12-22";
            dtpStartBig.TextAlignment = ContentAlignment.MiddleLeft;
            dtpStartBig.Value = new DateTime(2025, 12, 22, 11, 25, 5, 185);
            dtpStartBig.Watermark = "";
            // 
            // lblStartDate
            // 
            lblStartDate.Font = new Font("微软雅黑", 12F);
            lblStartDate.Location = new Point(19, 17);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(90, 30);
            lblStartDate.TabIndex = 2;
            lblStartDate.Text = "开始日期:";
            lblStartDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboType
            // 
            cboType.DataSource = null;
            cboType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboType.FillColor = Color.White;
            cboType.Font = new Font("微软雅黑", 12F);
            cboType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboType.Location = new Point(141, 59);
            cboType.Margin = new Padding(4, 5, 4, 5);
            cboType.MinimumSize = new Size(63, 0);
            cboType.Name = "cboType";
            cboType.Padding = new Padding(0, 0, 30, 2);
            cboType.RectColor = Color.FromArgb(65, 100, 204);
            cboType.Size = new Size(176, 30);
            cboType.SymbolSize = 24;
            cboType.TabIndex = 1;
            cboType.TextAlignment = ContentAlignment.MiddleLeft;
            cboType.Watermark = "";
            cboType.SelectedIndexChanged += cboType_SelectedIndexChanged;
            // 
            // lblLogType
            // 
            lblLogType.Font = new Font("微软雅黑", 12F);
            lblLogType.Location = new Point(19, 58);
            lblLogType.Name = "lblLogType";
            lblLogType.Size = new Size(90, 30);
            lblLogType.TabIndex = 0;
            lblLogType.Text = "日志类型:";
            lblLogType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 135);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(dgvLogs);
            splitContainer.Panel1.Padding = new Padding(5);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(pnlDetail);
            splitContainer.Size = new Size(1109, 664);
            splitContainer.SplitterDistance = 421;
            splitContainer.TabIndex = 1;
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.AllowUserToResizeColumns = false;
            dgvLogs.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dgvLogs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvLogs.BackgroundColor = Color.White;
            dgvLogs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvLogs.DefaultCellStyle = dataGridViewCellStyle3;
            dgvLogs.Dock = DockStyle.Fill;
            dgvLogs.EnableHeadersVisualStyles = false;
            dgvLogs.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvLogs.GridColor = Color.FromArgb(80, 160, 255);
            dgvLogs.Location = new Point(5, 5);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvLogs.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvLogs.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvLogs.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvLogs.RowTemplate.Height = 30;
            dgvLogs.SelectedIndex = -1;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.Size = new Size(1099, 411);
            dgvLogs.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvLogs.TabIndex = 0;
            // 
            // pnlDetail
            // 
            pnlDetail.Controls.Add(txtDetail);
            pnlDetail.Controls.Add(pnlDetailTop);
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.Location = new Point(0, 0);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Size = new Size(1109, 239);
            pnlDetail.TabIndex = 0;
            // 
            // txtDetail
            // 
            txtDetail.BackColor = Color.FromArgb(250, 250, 250);
            txtDetail.Dock = DockStyle.Fill;
            txtDetail.Font = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtDetail.Location = new Point(0, 40);
            txtDetail.Multiline = true;
            txtDetail.Name = "txtDetail";
            txtDetail.ReadOnly = true;
            txtDetail.ScrollBars = ScrollBars.Vertical;
            txtDetail.Size = new Size(1109, 199);
            txtDetail.TabIndex = 1;
            // 
            // pnlDetailTop
            // 
            pnlDetailTop.BackColor = Color.FromArgb(240, 240, 240);
            pnlDetailTop.Controls.Add(btnCopy);
            pnlDetailTop.Controls.Add(lblDetailTitle);
            pnlDetailTop.Dock = DockStyle.Top;
            pnlDetailTop.Location = new Point(0, 0);
            pnlDetailTop.Name = "pnlDetailTop";
            pnlDetailTop.Size = new Size(1109, 40);
            pnlDetailTop.TabIndex = 0;
            // 
            // btnCopy
            // 
            btnCopy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopy.Location = new Point(983, 2);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(116, 35);
            btnCopy.TabIndex = 1;
            btnCopy.Text = "复制详情";
            btnCopy.Click += btnCopy_Click;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblDetailTitle.Location = new Point(10, 5);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(200, 30);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "详细信息";
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.FromArgb(240, 240, 240);
            pnlBottom.Controls.Add(lblStatus);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 799);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(1109, 30);
            pnlBottom.TabIndex = 2;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(0, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(10, 0, 0, 0);
            lblStatus.Size = new Size(1109, 30);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "就绪";
            // 
            // frmNLogs
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1109, 829);
            ControlBox = false;
            Controls.Add(splitContainer);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmNLogs";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "日志查询";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1200, 680);
            Load += frmNLogs_Load;
            pnlTop.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            pnlDetailTop.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private AntdUI.Label lblLogType;
        private Sunny.UI.UIComboBox cboType;
        private Sunny.UI.UIDatePicker dtpStartBig;
        private AntdUI.Label lblStartDate;
        private Sunny.UI.UIDatePicker dtpEndBig;
        private AntdUI.Label lblEndDate;
        private AntdUI.Button btnSearch;
        private AntdUI.Label lblSearchKeyword;
        private UITextBox txtSearch;
        private AntdUI.Button btnToday;
        private AntdUI.Button btnExport;
        private AntdUI.Button btnClose;
        private System.Windows.Forms.SplitContainer splitContainer;
        private UIDataGridView dgvLogs;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Panel pnlDetailTop;
        private AntdUI.Button btnCopy;
        private AntdUI.Label lblDetailTitle;
        private System.Windows.Forms.Panel pnlBottom;
        private AntdUI.Label lblStatus;
    }
}
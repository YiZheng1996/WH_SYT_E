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
            txtSearch = new UITextBox();
            lblFilter = new Label();
            cmbFilter = new UIComboBox();
            lblStats = new Label();
            lstVariables = new UIListBox();
            panelDetails = new Panel();
            lblDetails = new Label();
            btnOK = new UIButton();
            btnCancel = new UIButton();
            panelDetails.SuspendLayout();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Cursor = Cursors.IBeam;
            txtSearch.Font = new Font("微软雅黑", 10F);
            txtSearch.Location = new Point(20, 52);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.MinimumSize = new Size(1, 16);
            txtSearch.Name = "txtSearch";
            txtSearch.Padding = new Padding(5);
            txtSearch.ShowText = false;
            txtSearch.Size = new Size(540, 30);
            txtSearch.TabIndex = 0;
            txtSearch.TextAlignment = ContentAlignment.MiddleLeft;
            txtSearch.Watermark = "🔍 输入变量名称搜索...";
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // lblFilter
            // 
            lblFilter.Font = new Font("微软雅黑", 9F);
            lblFilter.Location = new Point(20, 87);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(80, 29);
            lblFilter.TabIndex = 1;
            lblFilter.Text = "变量类型:";
            lblFilter.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbFilter
            // 
            cmbFilter.DataSource = null;
            cmbFilter.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbFilter.FillColor = Color.White;
            cmbFilter.Font = new Font("微软雅黑", 9F);
            cmbFilter.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbFilter.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbFilter.Location = new Point(100, 88);
            cmbFilter.Margin = new Padding(4, 5, 4, 5);
            cmbFilter.MinimumSize = new Size(63, 0);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Padding = new Padding(0, 0, 30, 2);
            cmbFilter.Size = new Size(150, 29);
            cmbFilter.SymbolSize = 24;
            cmbFilter.TabIndex = 2;
            cmbFilter.TextAlignment = ContentAlignment.MiddleLeft;
            cmbFilter.Watermark = "";
            cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
            // 
            // lblStats
            // 
            lblStats.Font = new Font("微软雅黑", 9F);
            lblStats.ForeColor = Color.Gray;
            lblStats.Location = new Point(270, 88);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(290, 29);
            lblStats.TabIndex = 3;
            lblStats.Text = "共 0 个变量";
            lblStats.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstVariables
            // 
            lstVariables.Font = new Font("Consolas", 10F);
            lstVariables.HoverColor = Color.FromArgb(155, 200, 255);
            lstVariables.ItemSelectForeColor = Color.White;
            lstVariables.Location = new Point(20, 124);
            lstVariables.Margin = new Padding(4, 5, 4, 5);
            lstVariables.MinimumSize = new Size(1, 1);
            lstVariables.Name = "lstVariables";
            lstVariables.Padding = new Padding(2);
            lstVariables.ShowText = false;
            lstVariables.Size = new Size(540, 300);
            lstVariables.TabIndex = 4;
            lstVariables.Text = null;
            lstVariables.KeyDown += LstVariables_KeyDown;
            lstVariables.DoubleClick += LstVariables_DoubleClick;
            lstVariables.SelectedIndexChanged += LstVariables_SelectedIndexChanged;
            // 
            // panelDetails
            // 
            panelDetails.BackColor = Color.FromArgb(240, 248, 255);
            panelDetails.BorderStyle = BorderStyle.FixedSingle;
            panelDetails.Controls.Add(lblDetails);
            panelDetails.Location = new Point(20, 427);
            panelDetails.Name = "panelDetails";
            panelDetails.Size = new Size(540, 50);
            panelDetails.TabIndex = 5;
            // 
            // lblDetails
            // 
            lblDetails.Dock = DockStyle.Fill;
            lblDetails.Font = new Font("微软雅黑", 9F);
            lblDetails.ForeColor = Color.Gray;
            lblDetails.Location = new Point(0, 0);
            lblDetails.Name = "lblDetails";
            lblDetails.Padding = new Padding(10, 5, 10, 5);
            lblDetails.Size = new Size(538, 48);
            lblDetails.TabIndex = 0;
            lblDetails.Text = "请选择一个变量";
            lblDetails.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.Font = new Font("微软雅黑", 10F);
            btnOK.Location = new Point(340, 492);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(100, 35);
            btnOK.TabIndex = 6;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("微软雅黑", 9F);
            btnOK.Click += BtnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(450, 492);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            // 
            // VariableSelectionDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = btnCancel;
            ClientSize = new Size(580, 542);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(panelDetails);
            Controls.Add(lstVariables);
            Controls.Add(lblStats);
            Controls.Add(cmbFilter);
            Controls.Add(lblFilter);
            Controls.Add(txtSearch);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "VariableSelectionDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "选择变量";
            ZoomScaleRect = new Rectangle(15, 15, 580, 525);
            panelDetails.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox txtSearch;
        private System.Windows.Forms.Label lblFilter;
        private Sunny.UI.UIComboBox cmbFilter;
        private System.Windows.Forms.Label lblStats;
        private Sunny.UI.UIListBox lstVariables;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblDetails;
        private Sunny.UI.UIButton btnOK;
        private Sunny.UI.UIButton btnCancel;
    }
}

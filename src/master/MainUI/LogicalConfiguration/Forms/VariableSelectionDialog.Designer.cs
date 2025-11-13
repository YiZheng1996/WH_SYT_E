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
            uiGroupBox1 = new UIPanel();
            cmbFilter = new UIComboBox();
            lblStats = new UILabel();
            lblFilter = new UILabel();
            txtSearch = new UITextBox();
            uiGroupBox2 = new UIPanel();
            lstVariables = new UIListBox();
            uiLabel1 = new UILabel();
            uiGroupBox3 = new UIPanel();
            lblDetails = new UILabel();
            uiLabel2 = new UILabel();
            btnCancel = new UISymbolButton();
            btnOK = new UISymbolButton();
            uiLine1 = new UILine();
            uiLine2 = new UILine();
            uiLine3 = new UILine();
            uiGroupBox1.SuspendLayout();
            uiGroupBox2.SuspendLayout();
            uiGroupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(cmbFilter);
            uiGroupBox1.Controls.Add(lblStats);
            uiGroupBox1.Controls.Add(lblFilter);
            uiGroupBox1.Controls.Add(txtSearch);
            uiGroupBox1.FillColor = Color.White;
            uiGroupBox1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox1.Location = new Point(15, 78);
            uiGroupBox1.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox1.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox1.Size = new Size(550, 119);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = null;
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // cmbFilter
            // 
            cmbFilter.DataSource = null;
            cmbFilter.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbFilter.FillColor = Color.White;
            cmbFilter.Font = new Font("微软雅黑", 10F);
            cmbFilter.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbFilter.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbFilter.Location = new Point(100, 69);
            cmbFilter.Margin = new Padding(4, 5, 4, 5);
            cmbFilter.MinimumSize = new Size(63, 0);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Padding = new Padding(0, 0, 30, 2);
            cmbFilter.RectColor = Color.FromArgb(65, 100, 204);
            cmbFilter.Size = new Size(150, 29);
            cmbFilter.SymbolSize = 24;
            cmbFilter.TabIndex = 3;
            cmbFilter.TextAlignment = ContentAlignment.MiddleLeft;
            cmbFilter.Watermark = "";
            cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
            // 
            // lblStats
            // 
            lblStats.BackColor = Color.Transparent;
            lblStats.Font = new Font("微软雅黑", 9F);
            lblStats.ForeColor = Color.Gray;
            lblStats.Location = new Point(270, 69);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(260, 29);
            lblStats.TabIndex = 2;
            lblStats.Text = "共 0 个变量";
            lblStats.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFilter
            // 
            lblFilter.BackColor = Color.Transparent;
            lblFilter.Font = new Font("微软雅黑", 10F);
            lblFilter.ForeColor = Color.FromArgb(48, 48, 48);
            lblFilter.Location = new Point(20, 69);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(80, 29);
            lblFilter.TabIndex = 1;
            lblFilter.Text = "变量类型:";
            lblFilter.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSearch
            // 
            txtSearch.Cursor = Cursors.IBeam;
            txtSearch.Font = new Font("微软雅黑", 10F);
            txtSearch.Location = new Point(20, 27);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.MinimumSize = new Size(1, 16);
            txtSearch.Name = "txtSearch";
            txtSearch.Padding = new Padding(5);
            txtSearch.RectColor = Color.FromArgb(65, 100, 204);
            txtSearch.ShowText = false;
            txtSearch.Size = new Size(510, 29);
            txtSearch.TabIndex = 0;
            txtSearch.TextAlignment = ContentAlignment.MiddleLeft;
            txtSearch.Watermark = "🔍 输入变量名称搜索...";
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // uiGroupBox2
            // 
            uiGroupBox2.Controls.Add(lstVariables);
            uiGroupBox2.Controls.Add(uiLabel1);
            uiGroupBox2.FillColor = Color.White;
            uiGroupBox2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox2.Location = new Point(15, 241);
            uiGroupBox2.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox2.MinimumSize = new Size(1, 1);
            uiGroupBox2.Name = "uiGroupBox2";
            uiGroupBox2.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox2.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox2.Size = new Size(550, 260);
            uiGroupBox2.TabIndex = 1;
            uiGroupBox2.Text = null;
            uiGroupBox2.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lstVariables
            // 
            lstVariables.Font = new Font("Consolas", 10F);
            lstVariables.HoverColor = Color.FromArgb(155, 200, 255);
            lstVariables.ItemSelectBackColor = Color.FromArgb(80, 160, 255);
            lstVariables.ItemSelectForeColor = Color.White;
            lstVariables.Location = new Point(15, 70);
            lstVariables.Margin = new Padding(4, 5, 4, 5);
            lstVariables.MinimumSize = new Size(1, 1);
            lstVariables.Name = "lstVariables";
            lstVariables.Padding = new Padding(2);
            lstVariables.ShowText = false;
            lstVariables.Size = new Size(520, 175);
            lstVariables.TabIndex = 1;
            lstVariables.Text = null;
            lstVariables.KeyDown += LstVariables_KeyDown;
            lstVariables.DoubleClick += LstVariables_DoubleClick;
            lstVariables.SelectedIndexChanged += LstVariables_SelectedIndexChanged;
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("微软雅黑", 9F);
            uiLabel1.ForeColor = Color.Gray;
            uiLabel1.Location = new Point(15, 35);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(520, 29);
            uiLabel1.TabIndex = 0;
            uiLabel1.Text = "💡 提示:双击或按回车键选择变量";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiGroupBox3
            // 
            uiGroupBox3.Controls.Add(lblDetails);
            uiGroupBox3.Controls.Add(uiLabel2);
            uiGroupBox3.FillColor = Color.White;
            uiGroupBox3.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox3.Location = new Point(15, 545);
            uiGroupBox3.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox3.MinimumSize = new Size(1, 1);
            uiGroupBox3.Name = "uiGroupBox3";
            uiGroupBox3.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox3.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox3.Size = new Size(550, 110);
            uiGroupBox3.TabIndex = 2;
            uiGroupBox3.Text = null;
            uiGroupBox3.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblDetails
            // 
            lblDetails.BackColor = Color.Transparent;
            lblDetails.Font = new Font("微软雅黑", 9F);
            lblDetails.ForeColor = Color.Gray;
            lblDetails.Location = new Point(15, 35);
            lblDetails.Name = "lblDetails";
            lblDetails.Padding = new Padding(10, 5, 10, 5);
            lblDetails.Size = new Size(520, 65);
            lblDetails.TabIndex = 1;
            lblDetails.Text = "请选择一个变量";
            lblDetails.TextAlign = ContentAlignment.TopLeft;
            // 
            // uiLabel2
            // 
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 10F);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(15, 8);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(100, 25);
            uiLabel2.TabIndex = 0;
            uiLabel2.Text = "变量详情";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FillColor = Color.FromArgb(230, 80, 80);
            btnCancel.FillHoverColor = Color.FromArgb(232, 127, 128);
            btnCancel.FillPressColor = Color.FromArgb(202, 87, 89);
            btnCancel.FillSelectedColor = Color.FromArgb(202, 87, 89);
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(450, 670);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(230, 80, 80);
            btnCancel.RectHoverColor = Color.FromArgb(232, 127, 128);
            btnCancel.Size = new Size(115, 38);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 4;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.FillColor = Color.FromArgb(65, 100, 204);
            btnOK.FillHoverColor = Color.FromArgb(88, 165, 49);
            btnOK.FillPressColor = Color.FromArgb(56, 106, 32);
            btnOK.FillSelectedColor = Color.FromArgb(56, 106, 32);
            btnOK.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnOK.Location = new Point(320, 670);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.RectColor = Color.FromArgb(65, 100, 204);
            btnOK.RectHoverColor = Color.FromArgb(88, 165, 49);
            btnOK.Size = new Size(115, 38);
            btnOK.Symbol = 61528;
            btnOK.SymbolSize = 28;
            btnOK.TabIndex = 3;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("微软雅黑", 9F);
            btnOK.Click += BtnOK_Click;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.EndCap = UILineCap.Circle;
            uiLine1.Font = new Font("微软雅黑", 11F);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.White;
            uiLine1.Location = new Point(15, 43);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(550, 29);
            uiLine1.TabIndex = 5;
            uiLine1.Text = "🔍 搜索与筛选";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 11F);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(15, 205);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(550, 29);
            uiLine2.TabIndex = 6;
            uiLine2.Text = "📋 可用变量列表";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine3
            // 
            uiLine3.BackColor = Color.Transparent;
            uiLine3.EndCap = UILineCap.Circle;
            uiLine3.Font = new Font("微软雅黑", 11F);
            uiLine3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine3.LineColor = Color.White;
            uiLine3.Location = new Point(15, 509);
            uiLine3.MinimumSize = new Size(1, 1);
            uiLine3.Name = "uiLine3";
            uiLine3.Size = new Size(550, 29);
            uiLine3.TabIndex = 7;
            uiLine3.Text = "ℹ️ 变量详情";
            uiLine3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VariableSelectionDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            CancelButton = btnCancel;
            ClientSize = new Size(580, 723);
            Controls.Add(uiLine3);
            Controls.Add(uiLine2);
            Controls.Add(uiLine1);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(uiGroupBox3);
            Controls.Add(uiGroupBox2);
            Controls.Add(uiGroupBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "VariableSelectionDialog";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "🔧 选择变量";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 580, 723);
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox2.ResumeLayout(false);
            uiGroupBox3.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiGroupBox1;
        private Sunny.UI.UITextBox txtSearch;
        private Sunny.UI.UILabel lblFilter;
        private Sunny.UI.UIComboBox cmbFilter;
        private Sunny.UI.UILabel lblStats;
        private Sunny.UI.UIPanel uiGroupBox2;
        private Sunny.UI.UIListBox lstVariables;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIPanel uiGroupBox3;
        private Sunny.UI.UILabel lblDetails;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UISymbolButton btnOK;
        private Sunny.UI.UISymbolButton btnCancel;
        private UILine uiLine1;
        private UILine uiLine2;
        private UILine uiLine3;
    }
}
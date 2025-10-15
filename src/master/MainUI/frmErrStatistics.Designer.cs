namespace MainUI
{
    partial class frmErrStatistics
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
            btnEdit = new UIButton();
            btnDelete = new UIButton();
            btnAdd = new UIButton();
            btnClose = new UIButton();
            Tables = new AntdUI.Table();
            SuspendLayout();
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.Transparent;
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnEdit.ForeDisableColor = Color.White;
            btnEdit.Location = new Point(379, 581);
            btnEdit.MinimumSize = new Size(1, 1);
            btnEdit.Name = "btnEdit";
            btnEdit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnEdit.Size = new Size(147, 37);
            btnEdit.TabIndex = 402;
            btnEdit.Text = "更改";
            btnEdit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEdit.TipsText = "1";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Transparent;
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnDelete.ForeDisableColor = Color.White;
            btnDelete.Location = new Point(198, 581);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnDelete.Size = new Size(147, 37);
            btnDelete.TabIndex = 401;
            btnDelete.Text = "删除";
            btnDelete.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.TipsText = "1";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.Transparent;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnAdd.ForeDisableColor = Color.White;
            btnAdd.Location = new Point(17, 581);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnAdd.Size = new Size(147, 37);
            btnAdd.TabIndex = 400;
            btnAdd.Text = "添加";
            btnAdd.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAdd.TipsText = "1";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Transparent;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnClose.ForeDisableColor = Color.White;
            btnClose.Location = new Point(895, 581);
            btnClose.MinimumSize = new Size(1, 1);
            btnClose.Name = "btnClose";
            btnClose.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnClose.Size = new Size(147, 37);
            btnClose.TabIndex = 404;
            btnClose.Text = "退出";
            btnClose.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnClose.TipsText = "1";
            btnClose.Click += btnClose_Click;
            // 
            // Tables
            // 
            Tables.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            Tables.BackColor = Color.White;
            Tables.BackgroundImageLayout = ImageLayout.None;
            Tables.BorderColor = Color.Black;
            Tables.Bordered = true;
            Tables.CheckSize = 20;
            Tables.ClipboardCopy = false;
            Tables.ColumnBack = Color.FromArgb(44, 62, 80);
            Tables.ColumnFont = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            Tables.ColumnFore = Color.White;
            Tables.DefaultExpand = true;
            Tables.Font = new Font("微软雅黑", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Tables.ImeMode = ImeMode.NoControl;
            Tables.Location = new Point(17, 53);
            Tables.Name = "Tables";
            Tables.RightToLeft = RightToLeft.No;
            Tables.RowHeight = 50;
            Tables.RowHeightHeader = 40;
            Tables.Size = new Size(1025, 516);
            Tables.SwitchSize = 25;
            Tables.TabIndex = 406;
            Tables.CellClick += Tables_CellClick;
            Tables.CellDoubleClick += Tables_CellDoubleClick;
            // 
            // frmErrStatistics
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(1059, 630);
            ControlBox = false;
            Controls.Add(Tables);
            Controls.Add(btnClose);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmErrStatistics";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "问题统计";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            Load += frmMeteringRemind_Load;
            ResumeLayout(false);
        }

        #endregion
        private UIButton btnEdit;
        private UIButton btnDelete;
        private UIButton btnAdd;
        private UIButton btnClose;
        private AntdUI.Table Tables;
    }
}
namespace MainUI.Procedure.User
{
    partial class ucRole
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRole));
            btnAdd = new UIButton();
            btnDelete = new UIButton();
            btnEdit = new UIButton();
            grpUser = new UIGroupBox();
            Tables = new AntdUI.Table();
            grpUser.SuspendLayout();
            SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.Cursor = Cursors.Hand;
            resources.ApplyResources(btnAdd, "btnAdd");
            btnAdd.ForeDisableColor = Color.White;
            btnAdd.Name = "btnAdd";
            btnAdd.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnAdd.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAdd.TipsText = "1";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Cursor = Cursors.Hand;
            resources.ApplyResources(btnDelete, "btnDelete");
            btnDelete.ForeDisableColor = Color.White;
            btnDelete.Name = "btnDelete";
            btnDelete.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnDelete.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.TipsText = "1";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Cursor = Cursors.Hand;
            resources.ApplyResources(btnEdit, "btnEdit");
            btnEdit.ForeDisableColor = Color.White;
            btnEdit.Name = "btnEdit";
            btnEdit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnEdit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEdit.TipsText = "1";
            btnEdit.Click += btnEdit_Click;
            // 
            // grpUser
            // 
            grpUser.BackColor = Color.FromArgb(236, 236, 237);
            grpUser.Controls.Add(Tables);
            grpUser.Controls.Add(btnEdit);
            grpUser.Controls.Add(btnDelete);
            grpUser.Controls.Add(btnAdd);
            grpUser.FillColor = Color.FromArgb(236, 236, 237);
            grpUser.FillColor2 = Color.FromArgb(236, 236, 237);
            grpUser.FillDisableColor = Color.FromArgb(236, 236, 237);
            resources.ApplyResources(grpUser, "grpUser");
            grpUser.ForeColor = Color.Black;
            grpUser.ForeDisableColor = Color.Black;
            grpUser.Name = "grpUser";
            grpUser.RectColor = Color.FromArgb(236, 236, 237);
            grpUser.RectDisableColor = Color.FromArgb(236, 236, 237);
            grpUser.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // Tables
            // 
            Tables.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            Tables.BackColor = Color.White;
            resources.ApplyResources(Tables, "Tables");
            Tables.BorderColor = Color.Black;
            Tables.Bordered = true;
            Tables.CheckSize = 20;
            Tables.ClipboardCopy = false;
            Tables.ColumnBack = Color.FromArgb(44, 62, 80);
            Tables.ColumnFont = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            Tables.ColumnFore = Color.White;
            Tables.DefaultExpand = true;
            Tables.Gap = 12;
            Tables.Name = "Tables";
            Tables.RowHeight = 50;
            Tables.RowHeightHeader = 40;
            Tables.SwitchSize = 25;
            Tables.CellClick += Tables_CellClick;
            Tables.CellDoubleClick += Tables_CellDoubleClick;
            // 
            // ucRole
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 237);
            Controls.Add(grpUser);
            resources.ApplyResources(this, "$this");
            Name = "ucRole";
            Load += ucRole_Load;
            grpUser.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UIButton btnEdit;
        private Sunny.UI.UIButton btnDelete;
        private Sunny.UI.UIButton btnAdd;
        private UIGroupBox grpUser;
        private AntdUI.Table Tables;
    }
}

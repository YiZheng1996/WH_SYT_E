namespace MainUI.Procedure
{
    partial class ucModelManage
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            grpUser = new UIGroupBox();
            cboModelType = new UIComboBox();
            btnEdit = new UIButton();
            btnDelete = new UIButton();
            btnAdd = new UIButton();
            Tables = new AntdUI.Table();
            uiLine1 = new UILine();
            uiLabel2 = new UILabel();
            grpUser.SuspendLayout();
            SuspendLayout();
            // 
            // grpUser
            // 
            grpUser.BackColor = Color.FromArgb(236, 236, 237);
            grpUser.Controls.Add(cboModelType);
            grpUser.Controls.Add(btnEdit);
            grpUser.Controls.Add(btnDelete);
            grpUser.Controls.Add(btnAdd);
            grpUser.Controls.Add(Tables);
            grpUser.Controls.Add(uiLine1);
            grpUser.Controls.Add(uiLabel2);
            grpUser.Dock = DockStyle.Top;
            grpUser.FillColor = Color.FromArgb(236, 236, 237);
            grpUser.FillColor2 = Color.FromArgb(236, 236, 237);
            grpUser.FillDisableColor = Color.FromArgb(42, 47, 55);
            grpUser.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            grpUser.ForeColor = Color.Black;
            grpUser.ForeDisableColor = Color.Black;
            grpUser.Location = new Point(0, 0);
            grpUser.Margin = new Padding(4, 5, 4, 5);
            grpUser.MinimumSize = new Size(1, 1);
            grpUser.Name = "grpUser";
            grpUser.Padding = new Padding(0, 32, 0, 0);
            grpUser.RectColor = Color.FromArgb(236, 236, 237);
            grpUser.RectDisableColor = Color.FromArgb(236, 236, 237);
            grpUser.Size = new Size(792, 787);
            grpUser.TabIndex = 400;
            grpUser.Text = "产品型号列表";
            grpUser.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // cboModelType
            // 
            cboModelType.BackColor = Color.Transparent;
            cboModelType.DataSource = null;
            cboModelType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboModelType.FillColor = Color.White;
            cboModelType.FillColor2 = Color.White;
            cboModelType.FillDisableColor = Color.White;
            cboModelType.FilterMaxCount = 50;
            cboModelType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            cboModelType.ForeColor = Color.Black;
            cboModelType.ForeDisableColor = Color.Black;
            cboModelType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboModelType.Items.AddRange(new object[] { "系统管理员" });
            cboModelType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboModelType.Location = new Point(327, 56);
            cboModelType.Margin = new Padding(4, 5, 4, 5);
            cboModelType.MinimumSize = new Size(63, 0);
            cboModelType.Name = "cboModelType";
            cboModelType.Padding = new Padding(0, 0, 30, 2);
            cboModelType.Radius = 10;
            cboModelType.RectColor = Color.Gray;
            cboModelType.RectDisableColor = Color.Gray;
            cboModelType.RectSides = ToolStripStatusLabelBorderSides.None;
            cboModelType.Size = new Size(223, 29);
            cboModelType.SymbolSize = 24;
            cboModelType.TabIndex = 396;
            cboModelType.TextAlignment = ContentAlignment.MiddleLeft;
            cboModelType.Watermark = "请选择";
            // 
            // btnEdit
            // 
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnEdit.ForeDisableColor = Color.White;
            btnEdit.Location = new Point(364, 747);
            btnEdit.MinimumSize = new Size(1, 1);
            btnEdit.Name = "btnEdit";
            btnEdit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnEdit.Size = new Size(147, 37);
            btnEdit.TabIndex = 414;
            btnEdit.Text = "更改";
            btnEdit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEdit.TipsText = "1";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnDelete.ForeDisableColor = Color.White;
            btnDelete.Location = new Point(183, 747);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnDelete.Size = new Size(147, 37);
            btnDelete.TabIndex = 413;
            btnDelete.Text = "删除";
            btnDelete.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.TipsText = "1";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnAdd.ForeDisableColor = Color.White;
            btnAdd.Location = new Point(2, 747);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnAdd.Size = new Size(147, 37);
            btnAdd.TabIndex = 412;
            btnAdd.Text = "添加";
            btnAdd.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAdd.TipsText = "1";
            btnAdd.Click += btnAdd_Click;
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
            Tables.Font = new Font("微软雅黑", 14.25F);
            Tables.Gap = 12;
            Tables.ImeMode = ImeMode.NoControl;
            Tables.Location = new Point(0, 96);
            Tables.Name = "Tables";
            Tables.RightToLeft = RightToLeft.No;
            Tables.RowHeight = 50;
            Tables.RowHeightHeader = 40;
            Tables.Size = new Size(792, 645);
            Tables.SwitchSize = 25;
            Tables.TabIndex = 411;
            Tables.CellClick += Tables_CellClick;
            Tables.CellButtonClick += Tables_CellButtonClick;
            Tables.CellDoubleClick += Tables_CellDoubleClick;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLine1.ForeColor = Color.White;
            uiLine1.LineColor = Color.White;
            uiLine1.LineColor2 = Color.White;
            uiLine1.Location = new Point(5, 25);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(787, 29);
            uiLine1.StartCap = UILineCap.Circle;
            uiLine1.TabIndex = 410;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel2.ForeColor = Color.Black;
            uiLabel2.Location = new Point(242, 58);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(78, 24);
            uiLabel2.TabIndex = 397;
            uiLabel2.Text = "产品类型";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ucModelManage
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            Controls.Add(grpUser);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Margin = new Padding(3, 6, 3, 6);
            Name = "ucModelManage";
            Size = new Size(792, 787);
            Load += ucModelManage_Load;
            grpUser.ResumeLayout(false);
            grpUser.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private UIGroupBox grpUser;
        private UILine uiLine1;
        private UILabel uiLabel2;
        private UIComboBox cboModelType;
        private AntdUI.Table Tables;
        private UIButton btnEdit;
        private UIButton btnDelete;
        private UIButton btnAdd;
    }
}

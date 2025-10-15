namespace MainUI.Procedure.User
{
    partial class frmUserEdit
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserEdit));
            uiLabel2 = new UILabel();
            CboRole = new UIComboBox();
            btnCancel = new UIButton();
            btnSave = new UIButton();
            uiPanel1 = new UIPanel();
            uiLabel1 = new UILabel();
            txtUserName = new UITextBox();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiLabel2
            // 
            uiLabel2.BackColor = Color.Transparent;
            resources.ApplyResources(uiLabel2, "uiLabel2");
            uiLabel2.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel2.Name = "uiLabel2";
            // 
            // CboRole
            // 
            CboRole.BackColor = Color.Transparent;
            CboRole.DataSource = null;
            CboRole.DropDownStyle = UIDropDownStyle.DropDownList;
            CboRole.FillColor = Color.FromArgb(218, 220, 230);
            CboRole.FillColor2 = Color.FromArgb(218, 220, 230);
            CboRole.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboRole.FilterMaxCount = 50;
            resources.ApplyResources(CboRole, "CboRole");
            CboRole.ForeColor = Color.FromArgb(46, 46, 46);
            CboRole.ForeDisableColor = Color.FromArgb(46, 46, 46);
            CboRole.ItemForeColor = Color.FromArgb(46, 46, 46);
            CboRole.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboRole.Items.AddRange(new object[] { resources.GetString("CboRole.Items"), resources.GetString("CboRole.Items1"), resources.GetString("CboRole.Items2"), resources.GetString("CboRole.Items3"), resources.GetString("CboRole.Items4"), resources.GetString("CboRole.Items5"), resources.GetString("CboRole.Items6"), resources.GetString("CboRole.Items7"), resources.GetString("CboRole.Items8"), resources.GetString("CboRole.Items9"), resources.GetString("CboRole.Items10"), resources.GetString("CboRole.Items11") });
            CboRole.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboRole.Name = "CboRole";
            CboRole.Radius = 10;
            CboRole.RectColor = Color.Gray;
            CboRole.RectDisableColor = Color.Gray;
            CboRole.RectSides = ToolStripStatusLabelBorderSides.None;
            CboRole.SymbolSize = 24;
            CboRole.TextAlignment = ContentAlignment.MiddleLeft;
            CboRole.Watermark = "请选择权限等级";
            CboRole.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            CboRole.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.ForeDisableColor = Color.White;
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 10;
            btnCancel.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.TipsText = "1";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Transparent;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillDisableColor = Color.FromArgb(80, 160, 255);
            resources.ApplyResources(btnSave, "btnSave");
            btnSave.ForeDisableColor = Color.White;
            btnSave.Name = "btnSave";
            btnSave.Radius = 10;
            btnSave.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSave.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSave.TipsText = "1";
            btnSave.Click += btnSave_Click;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(btnCancel);
            uiPanel1.Controls.Add(CboRole);
            uiPanel1.Controls.Add(txtUserName);
            uiPanel1.Controls.Add(btnSave);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.White;
            resources.ApplyResources(uiPanel1, "uiPanel1");
            uiPanel1.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel1.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.Transparent;
            resources.ApplyResources(uiLabel1, "uiLabel1");
            uiLabel1.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel1.Name = "uiLabel1";
            // 
            // txtUserName
            // 
            txtUserName.BackColor = Color.Transparent;
            txtUserName.FillColor = Color.FromArgb(218, 220, 230);
            txtUserName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtUserName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtUserName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            resources.ApplyResources(txtUserName, "txtUserName");
            txtUserName.ForeColor = Color.FromArgb(46, 46, 46);
            txtUserName.ForeDisableColor = Color.FromArgb(46, 46, 46);
            txtUserName.ForeReadOnlyColor = Color.FromArgb(46, 46, 46);
            txtUserName.Name = "txtUserName";
            txtUserName.Radius = 10;
            txtUserName.RectColor = Color.FromArgb(218, 220, 230);
            txtUserName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtUserName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtUserName.ShowText = false;
            txtUserName.TextAlignment = ContentAlignment.MiddleLeft;
            txtUserName.Watermark = "请输入用户名";
            txtUserName.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            txtUserName.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // frmUserEdit
            // 
            AutoScaleMode = AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            BackColor = Color.FromArgb(224, 224, 224);
            Controls.Add(uiPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmUserEdit";
            RectColor = Color.FromArgb(49, 54, 64);
            ShowIcon = false;
            ShowInTaskbar = false;
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 280, 234);
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIComboBox CboRole;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSave;
        private UIPanel uiPanel1;
        private UITextBox txtUserName;
        private UILabel uiLabel1;
    }
}
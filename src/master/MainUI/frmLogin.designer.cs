namespace MainUI
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            lblSoftName = new Label();
            lblMessage = new Label();
            uiLabel3 = new UILabel();
            uiLabel1 = new UILabel();
            Logo = new PictureBox();
            uiPanel1 = new UIPanel();
            cboUserName = new UIComboBox();
            txtPassword = new UITextBox();
            btnExit = new UISymbolButton();
            btnSignIn = new UISymbolButton();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblSoftName
            // 
            resources.ApplyResources(lblSoftName, "lblSoftName");
            lblSoftName.ForeColor = Color.FromArgb(44, 62, 80);
            lblSoftName.Name = "lblSoftName";
            // 
            // lblMessage
            // 
            resources.ApplyResources(lblMessage, "lblMessage");
            lblMessage.BackColor = Color.Transparent;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Name = "lblMessage";
            // 
            // uiLabel3
            // 
            uiLabel3.BackColor = Color.Transparent;
            resources.ApplyResources(uiLabel3, "uiLabel3");
            uiLabel3.ForeColor = Color.FromArgb(64, 64, 64);
            uiLabel3.Name = "uiLabel3";
            // 
            // uiLabel1
            // 
            resources.ApplyResources(uiLabel1, "uiLabel1");
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.ForeColor = Color.FromArgb(64, 64, 64);
            uiLabel1.Name = "uiLabel1";
            // 
            // Logo
            // 
            Logo.BackColor = Color.White;
            Logo.Image = Resources.CRRC_FULL_noback;
            resources.ApplyResources(Logo, "Logo");
            Logo.Name = "Logo";
            Logo.TabStop = false;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(cboUserName);
            uiPanel1.Controls.Add(txtPassword);
            uiPanel1.Controls.Add(btnExit);
            uiPanel1.Controls.Add(btnSignIn);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(Logo);
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.Controls.Add(lblMessage);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.FromArgb(49, 54, 64);
            resources.ApplyResources(uiPanel1, "uiPanel1");
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.FromArgb(224, 224, 224);
            uiPanel1.RectDisableColor = Color.FromArgb(224, 224, 224);
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // cboUserName
            // 
            cboUserName.BackColor = Color.Transparent;
            cboUserName.DataSource = null;
            cboUserName.FillColor = Color.FromArgb(218, 220, 230);
            cboUserName.FillColor2 = Color.FromArgb(218, 220, 230);
            cboUserName.FillDisableColor = Color.FromArgb(218, 220, 230);
            cboUserName.FilterMaxCount = 50;
            resources.ApplyResources(cboUserName, "cboUserName");
            cboUserName.ForeColor = Color.FromArgb(64, 64, 64);
            cboUserName.ForeDisableColor = Color.FromArgb(64, 64, 64);
            cboUserName.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboUserName.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboUserName.Name = "cboUserName";
            cboUserName.Radius = 10;
            cboUserName.RectColor = Color.Gray;
            cboUserName.RectDisableColor = Color.Gray;
            cboUserName.RectSides = ToolStripStatusLabelBorderSides.None;
            cboUserName.SymbolSize = 24;
            cboUserName.TextAlignment = ContentAlignment.MiddleLeft;
            cboUserName.Watermark = "请选择";
            cboUserName.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            cboUserName.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.Transparent;
            txtPassword.FillColor = Color.FromArgb(218, 220, 230);
            txtPassword.FillColor2 = Color.FromArgb(218, 220, 230);
            txtPassword.FillDisableColor = Color.FromArgb(43, 46, 57);
            txtPassword.FillReadOnlyColor = Color.FromArgb(43, 46, 57);
            resources.ApplyResources(txtPassword, "txtPassword");
            txtPassword.ForeColor = Color.FromArgb(64, 64, 64);
            txtPassword.ForeDisableColor = Color.FromArgb(64, 64, 64);
            txtPassword.ForeReadOnlyColor = Color.FromArgb(64, 64, 64);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Radius = 10;
            txtPassword.RectColor = Color.FromArgb(218, 220, 230);
            txtPassword.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtPassword.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPassword.ShowText = false;
            txtPassword.TextAlignment = ContentAlignment.MiddleLeft;
            txtPassword.Watermark = "请输入密码";
            txtPassword.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            txtPassword.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // btnExit
            // 
            btnExit.FillColor = Color.FromArgb(44, 62, 80);
            btnExit.FillColor2 = Color.DodgerBlue;
            btnExit.FillDisableColor = Color.FromArgb(70, 75, 85);
            resources.ApplyResources(btnExit, "btnExit");
            btnExit.ForeDisableColor = Color.White;
            btnExit.Name = "btnExit";
            btnExit.RectColor = Color.FromArgb(44, 62, 80);
            btnExit.RectDisableColor = Color.DodgerBlue;
            btnExit.Symbol = 0;
            btnExit.TipsFont = new Font("宋体", 11F);
            btnExit.Click += btnExit_Click;
            // 
            // btnSignIn
            // 
            btnSignIn.FillColor = Color.FromArgb(44, 62, 80);
            btnSignIn.FillColor2 = Color.FromArgb(44, 62, 80);
            btnSignIn.FillDisableColor = Color.FromArgb(70, 75, 85);
            resources.ApplyResources(btnSignIn, "btnSignIn");
            btnSignIn.ForeDisableColor = Color.White;
            btnSignIn.Name = "btnSignIn";
            btnSignIn.RectColor = Color.FromArgb(44, 62, 80);
            btnSignIn.RectDisableColor = Color.FromArgb(44, 62, 80);
            btnSignIn.Symbol = 0;
            btnSignIn.TipsFont = new Font("宋体", 11F);
            btnSignIn.Click += btnSignIn_Click;
            // 
            // frmLogin
            // 
            AcceptButton = btnSignIn;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(lblSoftName);
            Controls.Add(uiPanel1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLogin";
            Load += frmLogin_Load;
            MouseDown += frmLogin_MouseDown;
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            uiPanel1.ResumeLayout(false);
            uiPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Label lblMessage;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel1;
        public System.Windows.Forms.PictureBox Logo;
        public System.Windows.Forms.Label lblSoftName;
        private UITextBox txtPassword;
        private UIPanel uiPanel1;
        private UISymbolButton btnSignIn;
        private UISymbolButton btnExit;
        private UIComboBox cboUserName;
    }
}
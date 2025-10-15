namespace MainUI
{
    partial class frmRemindEdit
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
            uiLabel1 = new UILabel();
            txtUsername = new UITextBox();
            btnCancel = new UIButton();
            btnSubmit = new UIButton();
            uiPanel1 = new UIPanel();
            txtPassword = new UITextBox();
            txtNewPwd1 = new UITextBox();
            txtNewPwd2 = new UITextBox();
            uiLabel4 = new UILabel();
            uiLabel2 = new UILabel();
            uiLabel3 = new UILabel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel1.Location = new Point(99, 54);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(88, 23);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "用户名";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.Transparent;
            txtUsername.Enabled = false;
            txtUsername.FillColor = Color.FromArgb(218, 220, 230);
            txtUsername.FillColor2 = Color.FromArgb(218, 220, 230);
            txtUsername.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtUsername.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtUsername.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtUsername.ForeDisableColor = Color.FromArgb(218, 220, 230);
            txtUsername.ForeReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtUsername.Location = new Point(200, 54);
            txtUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtUsername.MinimumSize = new Size(1, 16);
            txtUsername.Name = "txtUsername";
            txtUsername.Padding = new System.Windows.Forms.Padding(5);
            txtUsername.Radius = 10;
            txtUsername.ReadOnly = true;
            txtUsername.RectColor = Color.FromArgb(218, 220, 230);
            txtUsername.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtUsername.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtUsername.ShowText = false;
            txtUsername.Size = new Size(210, 29);
            txtUsername.TabIndex = 70;
            txtUsername.TextAlignment = ContentAlignment.MiddleLeft;
            txtUsername.Watermark = "用户名";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCancel.Location = new Point(284, 272);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 10;
            btnCancel.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Size = new Size(126, 37);
            btnCancel.TabIndex = 398;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.TipsText = "1";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.Transparent;
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnSubmit.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSubmit.Location = new Point(120, 272);
            btnSubmit.MinimumSize = new Size(1, 1);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Radius = 10;
            btnSubmit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSubmit.Size = new Size(126, 37);
            btnSubmit.TabIndex = 397;
            btnSubmit.Text = "确定";
            btnSubmit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSubmit.TipsText = "1";
            btnSubmit.Click += BtnSubmit_Click;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(txtPassword);
            uiPanel1.Controls.Add(txtUsername);
            uiPanel1.Controls.Add(btnCancel);
            uiPanel1.Controls.Add(txtNewPwd1);
            uiPanel1.Controls.Add(btnSubmit);
            uiPanel1.Controls.Add(txtNewPwd2);
            uiPanel1.Controls.Add(uiLabel4);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Font = new Font("宋体", 12F);
            uiPanel1.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel1.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Location = new Point(73, 77);
            uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(519, 333);
            uiPanel1.TabIndex = 408;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.Transparent;
            txtPassword.Cursor = Cursors.IBeam;
            txtPassword.FillColor = Color.FromArgb(218, 220, 230);
            txtPassword.FillColor2 = Color.FromArgb(218, 220, 230);
            txtPassword.FillDisableColor = Color.FromArgb(42, 47, 55);
            txtPassword.FillReadOnlyColor = Color.FromArgb(42, 47, 55);
            txtPassword.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtPassword.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtPassword.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtPassword.Location = new Point(200, 102);
            txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtPassword.MinimumSize = new Size(1, 16);
            txtPassword.Name = "txtPassword";
            txtPassword.Padding = new System.Windows.Forms.Padding(5);
            txtPassword.PasswordChar = '*';
            txtPassword.Radius = 10;
            txtPassword.RectColor = Color.FromArgb(218, 220, 230);
            txtPassword.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtPassword.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPassword.ShowText = false;
            txtPassword.Size = new Size(210, 29);
            txtPassword.TabIndex = 69;
            txtPassword.TextAlignment = ContentAlignment.MiddleLeft;
            txtPassword.Watermark = "请输入";
            // 
            // txtNewPwd1
            // 
            txtNewPwd1.BackColor = Color.Transparent;
            txtNewPwd1.Cursor = Cursors.IBeam;
            txtNewPwd1.FillColor = Color.FromArgb(218, 220, 230);
            txtNewPwd1.FillColor2 = Color.FromArgb(218, 220, 230);
            txtNewPwd1.FillDisableColor = Color.FromArgb(42, 47, 55);
            txtNewPwd1.FillReadOnlyColor = Color.FromArgb(42, 47, 55);
            txtNewPwd1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtNewPwd1.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtNewPwd1.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtNewPwd1.Location = new Point(200, 150);
            txtNewPwd1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtNewPwd1.MinimumSize = new Size(1, 16);
            txtNewPwd1.Name = "txtNewPwd1";
            txtNewPwd1.Padding = new System.Windows.Forms.Padding(5);
            txtNewPwd1.PasswordChar = '*';
            txtNewPwd1.Radius = 10;
            txtNewPwd1.RectColor = Color.FromArgb(218, 220, 230);
            txtNewPwd1.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtNewPwd1.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtNewPwd1.ShowText = false;
            txtNewPwd1.Size = new Size(210, 29);
            txtNewPwd1.TabIndex = 71;
            txtNewPwd1.TextAlignment = ContentAlignment.MiddleLeft;
            txtNewPwd1.Watermark = "请输入";
            // 
            // txtNewPwd2
            // 
            txtNewPwd2.BackColor = Color.Transparent;
            txtNewPwd2.Cursor = Cursors.IBeam;
            txtNewPwd2.FillColor = Color.FromArgb(218, 220, 230);
            txtNewPwd2.FillColor2 = Color.FromArgb(218, 220, 230);
            txtNewPwd2.FillDisableColor = Color.FromArgb(42, 47, 55);
            txtNewPwd2.FillReadOnlyColor = Color.FromArgb(42, 47, 55);
            txtNewPwd2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtNewPwd2.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtNewPwd2.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtNewPwd2.Location = new Point(200, 198);
            txtNewPwd2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtNewPwd2.MinimumSize = new Size(1, 16);
            txtNewPwd2.Name = "txtNewPwd2";
            txtNewPwd2.Padding = new System.Windows.Forms.Padding(5);
            txtNewPwd2.PasswordChar = '*';
            txtNewPwd2.Radius = 10;
            txtNewPwd2.RectColor = Color.FromArgb(218, 220, 230);
            txtNewPwd2.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtNewPwd2.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtNewPwd2.ShowText = false;
            txtNewPwd2.Size = new Size(210, 29);
            txtNewPwd2.TabIndex = 72;
            txtNewPwd2.TextAlignment = ContentAlignment.MiddleLeft;
            txtNewPwd2.Watermark = "请输入";
            // 
            // uiLabel4
            // 
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel4.ImeMode = ImeMode.NoControl;
            uiLabel4.Location = new Point(99, 198);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(88, 23);
            uiLabel4.TabIndex = 75;
            uiLabel4.Text = "确认新密码";
            uiLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiLabel2
            // 
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel2.ImeMode = ImeMode.NoControl;
            uiLabel2.Location = new Point(99, 102);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(88, 23);
            uiLabel2.TabIndex = 73;
            uiLabel2.Text = "原始密码";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiLabel3
            // 
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(99, 150);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(88, 23);
            uiLabel3.TabIndex = 74;
            uiLabel3.Text = "新密码";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmRemindEdit
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(665, 450);
            Controls.Add(uiPanel1);
            Font = new Font("微软雅黑", 11F);
            ForeColor = Color.FromArgb(236, 236, 236);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(0, 0);
            MinimizeBox = false;
            Name = "frmRemindEdit";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "修改密码";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold);
            TitleForeColor = Color.FromArgb(236, 236, 236);
            ZoomScaleRect = new Rectangle(15, 15, 294, 282);
            Load += FrmChangePwd_Load;
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txtUsername;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSubmit;
        private UIPanel uiPanel1;
        private UITextBox txtPassword;
        private UITextBox txtNewPwd1;
        private UITextBox txtNewPwd2;
        private UILabel uiLabel4;
        private UILabel uiLabel2;
        private UILabel uiLabel3;
    }
}
using System.Drawing;
using System.Windows.Forms;

namespace CRRC.LoginUI
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private Panel leftPanel;
        private Panel rightPanel;
        private Label lblClose;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblUser;
        private Label lblPwd;
        private ComboBox cmbUserName;
        private TextBox txtPassword;
        private Panel userLine;
        private Panel pwdLine;
        private CheckBox chkRemember;
        private LinkLabel lnkForget;
        private Button btnLogin;
        private Button btnExit;

        private void InitializeComponent()
        {
            leftPanel = new Panel();
            rightPanel = new Panel();
            lblClose = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblUser = new Label();
            cmbUserName = new ComboBox();
            userLine = new Panel();
            lblPwd = new Label();
            txtPassword = new TextBox();
            pwdLine = new Panel();
            chkRemember = new CheckBox();
            lnkForget = new LinkLabel();
            btnLogin = new Button();
            btnExit = new Button();
            rightPanel.SuspendLayout();
            SuspendLayout();
            // 
            // leftPanel
            // 
            leftPanel.BackColor = Color.FromArgb(45, 55, 72);
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Location = new Point(0, 0);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(380, 560);
            leftPanel.TabIndex = 0;
            leftPanel.Paint += LeftPanel_Paint;
            // 
            // rightPanel
            // 
            rightPanel.BackColor = Color.White;
            rightPanel.Controls.Add(lblClose);
            rightPanel.Controls.Add(lblTitle);
            rightPanel.Controls.Add(lblSubtitle);
            rightPanel.Controls.Add(lblUser);
            rightPanel.Controls.Add(cmbUserName);
            rightPanel.Controls.Add(userLine);
            rightPanel.Controls.Add(lblPwd);
            rightPanel.Controls.Add(txtPassword);
            rightPanel.Controls.Add(pwdLine);
            rightPanel.Controls.Add(chkRemember);
            rightPanel.Controls.Add(lnkForget);
            rightPanel.Controls.Add(btnLogin);
            rightPanel.Controls.Add(btnExit);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(380, 0);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(520, 560);
            rightPanel.TabIndex = 1;
            // 
            // lblClose
            // 
            lblClose.Cursor = Cursors.Hand;
            lblClose.Font = new Font("Microsoft YaHei UI", 12F);
            lblClose.ForeColor = Color.FromArgb(74, 85, 104);
            lblClose.Location = new Point(478, 10);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(30, 30);
            lblClose.TabIndex = 0;
            lblClose.Text = "✕";
            lblClose.TextAlign = ContentAlignment.MiddleCenter;
            lblClose.Click += LblClose_Click;
            lblClose.MouseEnter += LblClose_MouseEnter;
            lblClose.MouseLeave += LblClose_MouseLeave;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft YaHei UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 55, 72);
            lblTitle.Location = new Point(60, 60);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(137, 40);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "欢迎登录";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Microsoft YaHei UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(160, 174, 192);
            lblSubtitle.Location = new Point(62, 105);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(195, 17);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "软件通用平台 · Software Platform";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Microsoft YaHei UI", 9F);
            lblUser.ForeColor = Color.FromArgb(74, 85, 104);
            lblUser.Location = new Point(60, 160);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(44, 17);
            lblUser.TabIndex = 3;
            lblUser.Text = "用户名";
            // 
            // cmbUserName
            // 
            cmbUserName.FlatStyle = FlatStyle.Flat;
            cmbUserName.Font = new Font("Microsoft YaHei UI", 11F);
            cmbUserName.Location = new Point(60, 185);
            cmbUserName.Name = "cmbUserName";
            cmbUserName.Size = new Size(360, 28);
            cmbUserName.TabIndex = 1;
            // 
            // userLine
            // 
            userLine.BackColor = Color.FromArgb(226, 232, 240);
            userLine.Location = new Point(60, 220);
            userLine.Name = "userLine";
            userLine.Size = new Size(360, 1);
            userLine.TabIndex = 4;
            // 
            // lblPwd
            // 
            lblPwd.AutoSize = true;
            lblPwd.Font = new Font("Microsoft YaHei UI", 9F);
            lblPwd.ForeColor = Color.FromArgb(74, 85, 104);
            lblPwd.Location = new Point(60, 245);
            lblPwd.Name = "lblPwd";
            lblPwd.Size = new Size(40, 17);
            lblPwd.TabIndex = 5;
            lblPwd.Text = "密  码";
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Microsoft YaHei UI", 11F);
            txtPassword.ForeColor = Color.FromArgb(160, 174, 192);
            txtPassword.Location = new Point(60, 275);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(360, 19);
            txtPassword.TabIndex = 2;
            txtPassword.Text = "请输入密码";
            txtPassword.GotFocus += TxtPassword_GotFocus;
            txtPassword.LostFocus += TxtPassword_LostFocus;
            // 
            // pwdLine
            // 
            pwdLine.BackColor = Color.FromArgb(226, 232, 240);
            pwdLine.Location = new Point(60, 305);
            pwdLine.Name = "pwdLine";
            pwdLine.Size = new Size(360, 1);
            pwdLine.TabIndex = 6;
            // 
            // chkRemember
            // 
            chkRemember.AutoSize = true;
            chkRemember.Font = new Font("Microsoft YaHei UI", 9F);
            chkRemember.ForeColor = Color.FromArgb(74, 85, 104);
            chkRemember.Location = new Point(60, 325);
            chkRemember.Name = "chkRemember";
            chkRemember.Size = new Size(75, 21);
            chkRemember.TabIndex = 7;
            chkRemember.Text = "记住密码";
            // 
            // lnkForget
            // 
            lnkForget.ActiveLinkColor = Color.FromArgb(196, 30, 35);
            lnkForget.AutoSize = true;
            lnkForget.Font = new Font("Microsoft YaHei UI", 9F);
            lnkForget.LinkColor = Color.FromArgb(196, 30, 35);
            lnkForget.Location = new Point(355, 325);
            lnkForget.Name = "lnkForget";
            lnkForget.Size = new Size(62, 17);
            lnkForget.TabIndex = 8;
            lnkForget.TabStop = true;
            lnkForget.Text = "忘记密码?";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(196, 30, 35);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(60, 365);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(360, 44);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "登 录";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.White;
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Microsoft YaHei UI", 11F);
            btnExit.ForeColor = Color.FromArgb(74, 85, 104);
            btnExit.Location = new Point(60, 420);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(360, 36);
            btnExit.TabIndex = 4;
            btnExit.Text = "退出系统";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += BtnExit_Click;
            // 
            // LoginForm
            // 
            AllowShowTitle = false;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(900, 560);
            ControlBox = false;
            Controls.Add(rightPanel);
            Controls.Add(leftPanel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            Padding = new Padding(0);
            ShowIcon = false;
            ShowTitle = false;
            Text = "";
            ZoomScaleRect = new Rectangle(15, 15, 900, 560);
            Load += LoginForm_Load;
            rightPanel.ResumeLayout(false);
            rightPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
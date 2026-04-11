using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace CRRC.LoginUI
{
    public partial class LoginForm : UIForm
    {
        // 颜色常量 - 中车品牌色系
        private readonly Color PrimaryColor = Color.FromArgb(196, 30, 35);   // 中车红
        private readonly Color DarkColor = Color.FromArgb(45, 55, 72);       // 深蓝灰
        private readonly Color TextColor = Color.FromArgb(74, 85, 104);      // 文字色
        private readonly Color PlaceholderColor = Color.FromArgb(160, 174, 192);

        private const string PWD_PLACEHOLDER = "请输入密码";

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int w, int h);

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 窗体圆角
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 15, 15));

            // 按钮圆角
            btnLogin.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnLogin.Width, btnLogin.Height, 8, 8));
            btnExit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnExit.Width, btnExit.Height, 8, 8));

            // 初始化用户名下拉项
            cmbUserName.Items.AddRange(new object[] { "admin", "user01", "tester" });
            cmbUserName.SelectedIndex = 0;
        }

        #region 左侧品牌面板绘制

        private void LeftPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 渐变背景
            using (LinearGradientBrush brush = new LinearGradientBrush(
                leftPanel.ClientRectangle,
                Color.FromArgb(45, 55, 72),
                Color.FromArgb(26, 32, 44),
                LinearGradientMode.ForwardDiagonal))
            {
                g.FillRectangle(brush, leftPanel.ClientRectangle);
            }

            // 装饰圆形
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(20, 255, 255, 255)))
            {
                g.FillEllipse(brush, -80, -80, 250, 250);
                g.FillEllipse(brush, 280, 380, 200, 200);
            }

            // 中车 LOGO（实际可替换为 PictureBox 加载真实图片）
            using (SolidBrush redBrush = new SolidBrush(PrimaryColor))
            {
                Rectangle logoRect = new Rectangle(140, 150, 100, 100);
                g.FillRectangle(redBrush, logoRect);
                using (Font logoFont = new Font("Microsoft YaHei UI", 22F, FontStyle.Bold))
                using (StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    g.DrawString("中车", logoFont, Brushes.White, logoRect, sf);
                }
            }

            // 标题文字
            using (Font titleFont = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold))
            using (Font subFont = new Font("Microsoft YaHei UI", 10F))
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center })
            {
                g.DrawString("中国中车 CRRC", titleFont, Brushes.White,
                    new RectangleF(0, 280, leftPanel.Width, 30), sf);

                using (SolidBrush subBrush = new SolidBrush(Color.FromArgb(180, 255, 255, 255)))
                {
                    g.DrawString("软 件 通 用 平 台", subFont, subBrush,
                        new RectangleF(0, 320, leftPanel.Width, 20), sf);
                }

                // 底部版权
                using (Font copyFont = new Font("Microsoft YaHei UI", 8F))
                using (SolidBrush copyBrush = new SolidBrush(Color.FromArgb(120, 255, 255, 255)))
                {
                    g.DrawString("© 2026 CRRC Corporation Limited", copyFont, copyBrush,
                        new RectangleF(0, leftPanel.Height - 40, leftPanel.Width, 20), sf);
                }
            }
        }

        #endregion

        #region 控件事件

        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LblClose_MouseEnter(object sender, EventArgs e)
        {
            lblClose.ForeColor = PrimaryColor;
        }

        private void LblClose_MouseLeave(object sender, EventArgs e)
        {
            lblClose.ForeColor = TextColor;
        }

        private void TxtPassword_GotFocus(object sender, EventArgs e)
        {
            if (txtPassword.Text == PWD_PLACEHOLDER)
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = DarkColor;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void TxtPassword_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = PWD_PLACEHOLDER;
                txtPassword.ForeColor = PlaceholderColor;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string userName = cmbUserName.Text.Trim();
            string password = txtPassword.Text == PWD_PLACEHOLDER ? "" : txtPassword.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入用户名和密码", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: 此处添加你的实际登录验证逻辑
            // 例如：调用数据库 / WebAPI 校验用户名密码
            MessageBox.Show($"登录成功！欢迎 {userName}", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region 无边框窗体拖动

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 1;
            const int HTCAPTION = 2;

            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
            {
                m.Result = (IntPtr)HTCAPTION;
            }
        }

        #endregion
    }
}
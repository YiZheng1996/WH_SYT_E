using System.Runtime.InteropServices;

namespace MainUI
{
    public partial class frmLogin : Form
    {
        private readonly OperateUserBLL _userBLL;

        public frmLogin()
        {
            InitializeComponent();
            _userBLL = new OperateUserBLL();
        }

        #region 窗体拖动相关
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
        }
        #endregion

        private void frmLogin_Load(object sender, EventArgs e)
        {
            InitializeUserComboBox();
            txtPassword.Focus();
        }

        /// <summary>
        /// 初始化用户下拉列表
        /// </summary>
        private void InitializeUserComboBox()
        {
            try
            {
                var users = _userBLL.GetUsers()
                    .Where(u => u.Username != "admin")
                    .ToList();

                cboUserName.DataSource = users;
                cboUserName.DisplayMember = nameof(OperateUserNewModel.Username);
                cboUserName.ValueMember = nameof(OperateUserNewModel.ID);
            }
            catch (Exception ex)
            {
                ShowError("初始化用户列表失败", ex);
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e) => AttemptLogin();

        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();

        /// <summary>
        /// 执行登录验证
        /// </summary>
        private void AttemptLogin()
        {
            try
            {
                string username = cboUserName.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (!ValidateLoginInput(username, password))
                    return;

                // 安全的用户获取：区分选择用户和手动输入
                var user = GetUserForAuthentication(username);
                if (user == null)
                {
                    ShowLoginError("当前用户名不存在!");
                    return;
                }

                if (AuthenticateUser(user, password))
                {
                    CompleteSuccessfulLogin(user);
                }
            }
            catch (Exception ex)
            {
                ShowLoginError("登录失败，请检查用户名和密码");
                NlogHelper.Default.Error("登录过程发生错误", ex);
            }
        }

        private bool ValidateLoginInput(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                ShowLoginError("账号不能为空，请重新输入!");
                cboUserName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowLoginError("密码不能为空，请重新输入!");
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private OperateUserNewModel GetUserForAuthentication(string username)
        {
            try
            {
                // 如果是从下拉框选择的用户
                if (IsSelectedFromDropDown(username))
                {
                    return GetSelectedDropDownUser();
                }

                // 如果是手动输入的用户名（主要是admin）
                return GetManualInputUser(username);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取用户信息失败: {username}", ex);
                return null;
            }
        }

        /// <summary>
        /// 判断是否从下拉框选择的用户
        /// </summary>
        private bool IsSelectedFromDropDown(string username)
        {
            // 检查当前选中的项是否与输入的用户名匹配
            if (cboUserName.SelectedItem is OperateUserNewModel selectedUser)
            {
                return selectedUser.Username.Equals(username, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        /// <summary>
        /// 获取下拉框选中的用户
        /// </summary>
        private OperateUserNewModel GetSelectedDropDownUser()
        {
            if (cboUserName.SelectedValue == null)
            {
                return null;
            }

            if (!int.TryParse(cboUserName.SelectedValue.ToString(), out int userId))
            {
                NlogHelper.Default.Warn($"无效的用户ID: {cboUserName.SelectedValue}");
                return null;
            }

            // 从数据库重新获取用户信息
            var user = _userBLL.SelectUser(new OperateUserNewModel { ID = userId });

            if (user == null)
            {
                NlogHelper.Default.Warn($"数据库中未找到用户ID: {userId}");
            }

            return user;
        }

        /// <summary>
        /// 获取手动输入的用户
        /// </summary>
        private OperateUserNewModel GetManualInputUser(string username)
        {
            // 只允许特定的手动输入用户
            var allowedManualUsers = new[] { "admin" }; // 可以扩展允许手动输入的用户列表

            if (!allowedManualUsers.Contains(username.ToLower()))
            {
                NlogHelper.Default.Warn($"不允许手动输入的用户名: {username}");
                return null;
            }

            // 从数据库获取用户信息
            var user = _userBLL.SelectUser(username);

            if (user == null)
            {
                NlogHelper.Default.Warn($"数据库中未找到手动输入的用户: {username}");
            }
            else
            {
                NlogHelper.Default.Info($"检测到手动输入用户: {username}");
            }

            return user;
        }


        private bool AuthenticateUser(OperateUserNewModel user, string password)
        {
            if (user.Password != password)
            {
                ShowLoginError("密码错误，请重新输入!");
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        private void CompleteSuccessfulLogin(OperateUserNewModel user)
        {
            NewUsers.NewUserInfo.InitUser(user);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ShowLoginError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "错误",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            NlogHelper.Default.Error(message, ex);
        }
    }
}
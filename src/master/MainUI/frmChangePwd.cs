namespace MainUI
{
    public partial class frmRemindEdit : UIForm
    {
        public frmRemindEdit()
        {
            InitializeComponent();
        }

        private void FrmChangePwd_Load(object sender, EventArgs e)
        {
            txtUsername.Text = NewUsers.NewUserInfo.Username;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string password = txtPassword.Text.Trim();
                string newPwd1 = txtNewPwd1.Text.Trim();
                string newPwd2 = txtNewPwd2.Text.Trim();

                if (string.IsNullOrEmpty(password))
                {
                    MessageHelper.MessageOK(this, "密码不能为空，请重新输入！");
                    txtPassword.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(newPwd1))
                {
                    MessageHelper.MessageOK(this, "新密码不能为空，请重新输入");
                    txtNewPwd1.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(newPwd2))
                {
                    MessageHelper.MessageOK(this, "确认密码不能为空，请重新输入！");
                    txtNewPwd2.Focus();
                    return;
                }
                if (newPwd1 != newPwd2)
                {
                    MessageHelper.MessageOK(this, "两次输入的密码不正确，请重新输入！");
                    txtNewPwd1.Focus();
                    return;
                }
                if (password != NewUsers.NewUserInfo.Password)
                {
                    MessageHelper.MessageOK(this, "原始密码不正确，请重新输入！");
                    txtPassword.Focus();
                    return;
                }

                OperateUserBLL bllUser = new();
                if (bllUser.UpdateUserPwd(NewUsers.NewUserInfo.ID, newPwd1))
                {
                    MessageHelper.MessageOK(this, "密码修改成功，重新登录后即可生效！");
                    NewUsers.NewUserInfo.Password = newPwd1;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageHelper.MessageOK(this, "修改失败！数据库操作失败！");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("密码修改失败：", ex);
                MessageHelper.MessageOK(this, "密码修改失败：" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
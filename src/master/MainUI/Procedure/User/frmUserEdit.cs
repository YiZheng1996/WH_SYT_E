namespace MainUI.Procedure.User
{
    public partial class frmUserEdit : UIForm
    {
        public frmUserEdit()
        {
            InitializeComponent();
            InitRadioButton();
        }

        private OperateUserNewModel User;
        public frmUserEdit(OperateUserNewModel user)
        {
            InitializeComponent();
            InitRadioButton();
            User = user;
            if (User != null)
            {
                txtUserName.Text = user.Username;
                CboRole.SelectedValue = user.Role_ID;
            }
        }

        void InitRadioButton()
        {
            RoleBLL roleBLL = new();
            CboRole.DataSource = roleBLL.GetRoles();
            CboRole.DisplayMember = "RoleName";
            CboRole.ValueMember = "ID";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OperateUserBLL bLL = new();
            User ??= new OperateUserNewModel();
            User.Username = txtUserName.Text;
            User.Role_ID = CboRole.SelectedValue.ToInt32();
            User.Password = "1";
            try
            {
                bool count = bLL.HandMovementUserTable(User);
                if (count)
                {
                    MessageBox.Show("保存成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("保存失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
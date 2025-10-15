namespace MainUI
{
    public partial class frmRoleEdit : UIForm
    {
        public frmRoleEdit() => InitializeComponent();

        private readonly RoleBLL roleBLL = new();
        private readonly RoleModel roleModel;
        public frmRoleEdit(RoleModel model)
        {
            InitializeComponent();
            roleModel = model;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string RoleName = txtRoleName.Text.Trim();
                string Describe = txtDescribe.Text.Trim();
                if (string.IsNullOrEmpty(RoleName))
                {
                    MessageHelper.MessageOK(this, "未输入角色名称！");
                    txtRoleName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(Describe))
                {
                    MessageHelper.MessageOK(this, "未输入角色描述！");
                    txtDescribe.Focus();
                    return;
                }

                bool result = false;
                if (roleModel == null)
                {
                    var newModel = new RoleModel
                    {
                        RoleName = txtRoleName.Text,
                        Describe = txtDescribe.Text,
                    };
                    result = roleBLL.AddRole(newModel);
                }
                else
                {
                    roleModel.RoleName = RoleName;
                    roleModel.Describe = Describe;
                    result = roleBLL.UpdateRole(roleModel);
                }

                if (result)
                {
                    MessageHelper.MessageOK(this, "保存成功！");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageHelper.MessageOK(this, "保存失败！");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存失败：", ex);
                MessageHelper.MessageOK(this, "保存失败：" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMeteringEdit_Load(object sender, EventArgs e)
        {
            if (roleModel != null)
            {
                txtRoleName.Text = roleModel.RoleName;
                txtDescribe.Text = roleModel.Describe;
            }
        }
    }
}
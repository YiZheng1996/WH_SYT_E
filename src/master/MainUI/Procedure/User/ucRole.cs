using AntdUI;

namespace MainUI.Procedure.User
{
    public partial class ucRole : ucBaseManagerUI
    {
        private RoleModel roleModel = new();
        private readonly RoleBLL roleBLL = new();
        public ucRole() => InitializeComponent();

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("RoleName","角色名称"){ Align = ColumnAlign.Center  },
                new Column("Describe","角色描述"){ Align = ColumnAlign.Center  },
           ];

            // 获取所有角色并过滤系统管理员（ID=1）
            var allRoles = roleBLL.GetRoles();
            var filteredRoles = allRoles.Where(r => r.ID != 1).ToList();
            Tables.DataSource = filteredRoles;
        }

        private void LoadData(RoleModel model)
        {
            using frmRoleEdit edit = new(model);
            edit.ShowDialog();
            if (edit.DialogResult == DialogResult.OK)
            {
                DataChangedEventManager.NotifyDataChanged(DataChangeType.Permission);
            }
        }

        private void ucRole_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 系统管理员角色保护
            if (roleModel.ID == 1)
            {
                MessageHelper.MessageOK("系统管理员角色不能删除！", TType.Error);
                return;
            }

            var DialogResult = MessageHelper.MessageYes("是否删除选中记录？", TType.Warn);
            if (DialogResult == DialogResult.OK)
            {
                if (roleBLL.DelRole(roleModel.ID))
                {
                    MessageHelper.MessageOK("删除成功！");
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.Permission);
                }
                else
                {
                    MessageHelper.MessageOK("删除失败！");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData(roleModel);
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is RoleModel model)
            {
                roleModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(roleModel);
        }
    }
}

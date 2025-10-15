using AntdUI;
using MainUI.Procedure.Edit;

namespace MainUI.Procedure.User
{
    public partial class ucPermission : ucBaseManagerUI
    {
        private PermissionModel PermissionModel = new();
        private readonly PermissionBLL permissionBLL = new();
        private readonly PermissionAllocationBLL allocationBLL = new();
        public ucPermission()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载权限列表数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                Tables.Columns = [
                    new Column("ID","ID"){ Align = ColumnAlign.Center, Visible = false },
                    new Column("PermissionName","权限名称"){ Align = ColumnAlign.Center },
                    new Column("PermissionCode","权限代码"){ Align = ColumnAlign.Center ,Visible=false},
                    new Column("ControlName","控件名称"){ Align = ColumnAlign.Center },
                    new Column("PermissionNotes","备注"){ Align = ColumnAlign.Center },
                ];

                Tables.DataSource = permissionBLL.GetPermissions();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"加载权限列表失败：{ex.Message}", ex);
                MessageHelper.MessageOK($"加载数据失败：{ex.Message}", TType.Error);
            }
        }


        /// <summary>
        /// 打开权限编辑窗体
        /// </summary>
        private void LoadData(PermissionModel model)
        {
            try
            {
                using frmPermissionEdit edit = new(model);

                if (edit.ShowDialog() == DialogResult.OK)
                {
                    // 刷新列表
                    LoadData();

                    // 通知权限数据已变更（触发ucPermissionAllocation刷新）
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.Permission);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"打开权限编辑窗体失败：{ex.Message}", ex);
                MessageHelper.MessageOK($"操作失败：{ex.Message}", TType.Error);
            }
        }

        private void ucPermission_Load(object sender, EventArgs e)
        {
            // 二次验证: 确保只有超级管理员能访问
            if (NewUsers.NewUserInfo.ID != 1)
            {
                MessageHelper.MessageOK("您没有权限访问此功能!", TType.Error);
                this.Parent?.Controls.Remove(this);
                return;
            }

            // 通过验证,加载数据
            LoadData();
        }

        /// <summary>
        /// 新增按钮点击事件
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (PermissionModel == null || PermissionModel.ID == 0)
                {
                    MessageHelper.MessageOK("请先选择要删除的权限！", TType.Warn);
                    return;
                }

                var dialogResult = MessageHelper.MessageYes(
                    $"确定要删除权限 '{PermissionModel.PermissionName}' 吗？\n\n注意：删除后将同时删除该权限的所有分配记录！",
                    TType.Warn);

                if (dialogResult == DialogResult.OK)
                {
                    // 先删除权限分配记录
                    bool delAllocation = allocationBLL.DelUserPermission(PermissionModel.ID);

                    // 再删除权限本身
                    bool delPermission = permissionBLL.DelPermission(PermissionModel.ID);

                    if (delPermission)
                    {
                        MessageHelper.MessageOK("删除成功！", TType.Success);

                        // 刷新列表
                        LoadData();

                        // 通知权限数据已变更（触发ucPermissionAllocation刷新）
                        DataChangedEventManager.NotifyDataChanged(DataChangeType.Permission);

                        // 清空当前选中
                        PermissionModel = new PermissionModel();
                    }
                    else
                    {
                        MessageHelper.MessageOK("删除失败！", TType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"删除权限失败：{ex.Message}", ex);
                MessageHelper.MessageOK($"删除失败：{ex.Message}", TType.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (PermissionModel == null || PermissionModel.ID == 0)
            {
                MessageHelper.MessageOK("请先选择要编辑的权限！", TType.Warn);
                return;
            }
            LoadData(PermissionModel);
        }

        /// <summary>
        /// 表格单击事件
        /// </summary>
        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is PermissionModel model)
            {
                PermissionModel = model;
            }
        }

        /// <summary>
        /// 表格双击事件
        /// </summary>
        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is PermissionModel model)
            {
                PermissionModel = model;
                LoadData(PermissionModel);
            }
        }
    }
}

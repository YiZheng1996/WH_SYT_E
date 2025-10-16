using AntdUI;
using System.Diagnostics;

namespace MainUI.Procedure.User
{
    public partial class ucPermissionAllocation : ucBaseManagerUI
    {
        private Dictionary<int, UICheckBox> DicChecks = [];

        public ucPermissionAllocation() => InitializeComponent();

        private void ucPermissionAllocation_Load(object sender, EventArgs e)
        {
            // 二次验证: 确保只有超级管理员能访问
            if (NewUsers.NewUserInfo.ID != 1)
            {
                MessageHelper.MessageOK("您没有权限访问此功能!", TType.Error);
                Parent?.Controls.Remove(this);
                return;
            }

            try
            {
                GetCboRoleData();
                GetPermissions();
                GetPermissionChecks();
                cboRole.SelectedValueChanged += cboRole_SelectedValueChanged;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"权限设定界面发生错误：{ex.Message}");
            }
        }

        // 获取角色数据
        private void GetCboRoleData()
        {
            try
            {
                RoleBLL roleBLL = new();
                var allRoles = roleBLL.GetRoles();

                // 过滤掉系统管理员角色（ID=1或名称为"系统管理员"）
                var filteredRoles = allRoles
                    .Where(r => r.ID != 1 && r.RoleName != "系统管理员")
                    .ToList();

                cboRole.DataSource = filteredRoles;
                cboRole.DisplayMember = "RoleName";
                cboRole.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取角色数据错误：{0}", ex);
                MessageHelper.MessageOK($"获取角色数据错误：{ex.Message}");
            }
        }

        // 获取权限数据
        private void GetPermissions()
        {
            try
            {
                // 先清空现有的控件和字典
                PanelPermissions.Controls.Clear();
                DicChecks.Clear();

                // 设置Panel的Padding（只需要设置一次，在循环外）
                PanelPermissions.Padding = new System.Windows.Forms.Padding(15);

                PermissionBLL permissionBLL = new();
                var permissions = permissionBLL.GetPermissions();

                foreach (var item in permissions)
                {
                    UICheckBox checkBox = new()
                    {
                        Text = item.PermissionName,
                        Tag = item.ID,
                        AutoSize = true,
                        CheckBoxSize = 18,
                        Font = new Font("微软雅黑", 13),
                        Margin = new System.Windows.Forms.Padding(20)
                    };

                    DicChecks.TryAdd(checkBox.Tag.ToInt32(), checkBox);
                    PanelPermissions.Controls.Add(checkBox);
                }

                // 强制刷新布局
                PanelPermissions.PerformLayout();
                PanelPermissions.Refresh();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载权限列表错误：{0}", ex);
                MessageHelper.MessageOK($"加载权限列表错误：{ex.Message}");
            }
        }

        // 获取权限Check数据
        private void GetPermissionChecks()
        {
            try
            {
                var roleID = cboRole.SelectedValue.ToInt32();
                if (roleID == 0 || DicChecks.Count == 0) return;

                PermissionAllocationBLL rolePermissionBLL = new();
                var permissions = rolePermissionBLL.GetPermissionChecks(roleID);

                foreach (var permission in permissions)
                {
                    int permissionID = permission.Permission_id;
                    if (DicChecks.TryGetValue(permissionID, out UICheckBox value))
                    {
                        value.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取权限Check发生错误：{0}", ex);
                MessageHelper.MessageOK($"获取权限Check发生错误：{ex.Message}");
            }
        }

        // 取消所有Check控件选择
        private void ClearChecks()
        {
            foreach (var item in DicChecks)
            {
                item.Value.Checked = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboRole.SelectedValue == null)
                {
                    MessageHelper.MessageOK("请先选择一个角色。");
                    return;
                }

                int roleId = Convert.ToInt32(cboRole.SelectedValue);
                List<int> selectedPermissionIds = [];

                foreach (var pair in DicChecks)
                {
                    UICheckBox checkBox = pair.Value;
                    if (checkBox.Checked)
                    {
                        if (checkBox.Tag is int permissionId)
                        {
                            selectedPermissionIds.Add(permissionId);
                        }
                    }
                }

                if (selectedPermissionIds.Count == 0)
                {
                    MessageHelper.MessageOK("请至少选择一个权限。");
                    return;
                }

                PermissionAllocationBLL rolePermissionBLL = new();
                bool result = rolePermissionBLL.SetRolePermissions(roleId, selectedPermissionIds);

                if (result)
                {
                    MessageHelper.MessageOK("权限分配成功！");
                }
                else
                {
                    MessageHelper.MessageOK("权限分配失败，请重试。");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("权限分配发生错误：{0}", ex);
                MessageHelper.MessageOK($"权限分配发生错误：{ex.Message}");
            }
        }

        private void cboRole_SelectedValueChanged(object sender, EventArgs e)
        {
            ClearChecks();
            GetPermissionChecks();
        }

        #region 数据自动刷新
        /// <summary>
        /// 重写Reload方法，实现数据刷新逻辑
        /// 当数据变更事件触发时，会自动调用此方法
        /// </summary>
        public override void Reload()
        {
            try
            {
                Debug.WriteLine("开始刷新 ucPermissionAllocation");

                // 保存当前选中的值
                int? currentSelectedValue = cboRole.SelectedValue?.ToInt32();

                GetCboRoleData();
                GetPermissions();

                // 恢复之前选中的值
                if (currentSelectedValue.HasValue && currentSelectedValue.Value > 0)
                {
                    cboRole.SelectedValue = currentSelectedValue.Value;
                }

                GetPermissionChecks();

                Debug.WriteLine("ucPermissionAllocation 权限设定 数据已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("ucPermissionAllocation 权限设定 重新加载数据失败", ex);
            }
        }

        /// <summary>
        /// 重写带类型的数据变更处理
        /// 只在权限数据变更时才刷新，提高性能
        /// </summary>
        protected override void OnDataChangedWithType(DataChangeType changeType)
        {
            // 只处理权限相关的数据变更
            if (changeType == DataChangeType.Permission || changeType == DataChangeType.All)
            {
                base.OnDataChangedWithType(changeType);
            }
        }
        #endregion
    }
}
namespace MainUI.CurrencyHelper
{
    public static class PermissionHelper
    {
        private static readonly PermissionAllocationBLL _permissionBLL = new();
        private static readonly PermissionBLL _permissionConfigBLL = new();
        private static readonly Dictionary<int, HashSet<string>> _userPermissions = [];
        private static readonly Dictionary<string, string> _controlPermissions = [];

        /// <summary>
        /// 初始化系统权限
        /// </summary>
        public static void Initialize(int userId, int roleId)
        {
            try
            {
                // 获取并缓存用户权限
                _userPermissions[userId] = [.. _permissionBLL.GetUserPermissionCodes(roleId)];

                // 获取并缓存控件权限配置
                _controlPermissions.Clear();
                var permissions = _permissionConfigBLL.GetPermissions()
                    .Where(p => !string.IsNullOrEmpty(p.ControlName) &&
                               !string.IsNullOrEmpty(p.PermissionCode));

                // 获取全部权限列表
                foreach (var permission in permissions)
                {
                    _controlPermissions[permission.ControlName] = permission.PermissionCode;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"初始化权限系统失败：{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 应用权限到控件
        /// </summary>
        public static void ApplyPermissionToControl(Control control, int userId)
        {
            if (control == null) return;

            try
            {
                // 获取权限代码
                var permissionCode = GetControlPermissionCode(control);
                if (!string.IsNullOrEmpty(permissionCode))
                {
                    control.Visible = HasPermission(userId, permissionCode);
                }

                // 递归处理子控件
                foreach (Control child in control.Controls)
                {
                    ApplyPermissionToControl(child, userId);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"应用权限到控件[{control.Name}]失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取控件的权限代码
        /// </summary>
        private static string GetControlPermissionCode(Control control)
        {
            // 从配置获取权限代码
            if (_controlPermissions.TryGetValue(control.Name, out string configCode))
            {
                control.Tag = $"{configCode}";
                return configCode;
            }

            // 从Tag获取权限代码
            if (control.Tag?.ToString() is string tag && tag.StartsWith("Permission:"))
            {
                return tag.Replace("Permission:", "");
            }

            return null;
        }

        /// <summary>
        /// 验证用户是否拥有指定权限
        /// </summary>
        public static bool HasPermission(int userId, string permissionCode)
        {
            try
            {
                if (userId == 1) return true; // 超级管理员

                if (_userPermissions.TryGetValue(userId, out var permissions))
                {
                    // 直接检查
                    if (permissions.Contains(permissionCode))
                        return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"验证权限失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 刷新权限配置
        /// </summary>
        public static void RefreshPermissions(int userId, int roleId)
        {
            _userPermissions.Clear();
            _controlPermissions.Clear();
            Initialize(userId, roleId);
        }
    }
}
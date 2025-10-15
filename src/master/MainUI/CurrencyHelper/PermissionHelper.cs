namespace MainUI.CurrencyHelper
{
    public static class PermissionHelper
    {
        private static readonly PermissionAllocationBLL _permissionBLL = new();
        private static readonly PermissionBLL _permissionConfigBLL = new();
        private static readonly Dictionary<int, HashSet<string>> _userPermissions = [];
        private static readonly Dictionary<string, string> _controlPermissions = [];

        /// <summary>
        /// ��ʼ��ϵͳȨ��
        /// </summary>
        public static void Initialize(int userId, int roleId)
        {
            try
            {
                // ��ȡ�������û�Ȩ��
                _userPermissions[userId] = [.. _permissionBLL.GetUserPermissionCodes(roleId)];

                // ��ȡ������ؼ�Ȩ������
                _controlPermissions.Clear();
                var permissions = _permissionConfigBLL.GetPermissions()
                    .Where(p => !string.IsNullOrEmpty(p.ControlName) &&
                               !string.IsNullOrEmpty(p.PermissionCode));

                // ��ȡȫ��Ȩ���б�
                foreach (var permission in permissions)
                {
                    _controlPermissions[permission.ControlName] = permission.PermissionCode;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"��ʼ��Ȩ��ϵͳʧ�ܣ�{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Ӧ��Ȩ�޵��ؼ�
        /// </summary>
        public static void ApplyPermissionToControl(Control control, int userId)
        {
            if (control == null) return;

            try
            {
                // ��ȡȨ�޴���
                var permissionCode = GetControlPermissionCode(control);
                if (!string.IsNullOrEmpty(permissionCode))
                {
                    control.Visible = HasPermission(userId, permissionCode);
                }

                // �ݹ鴦���ӿؼ�
                foreach (Control child in control.Controls)
                {
                    ApplyPermissionToControl(child, userId);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"Ӧ��Ȩ�޵��ؼ�[{control.Name}]ʧ�ܣ�{ex.Message}");
            }
        }

        /// <summary>
        /// ��ȡ�ؼ���Ȩ�޴���
        /// </summary>
        private static string GetControlPermissionCode(Control control)
        {
            // �����û�ȡȨ�޴���
            if (_controlPermissions.TryGetValue(control.Name, out string configCode))
            {
                control.Tag = $"{configCode}";
                return configCode;
            }

            // ��Tag��ȡȨ�޴���
            if (control.Tag?.ToString() is string tag && tag.StartsWith("Permission:"))
            {
                return tag.Replace("Permission:", "");
            }

            return null;
        }

        /// <summary>
        /// ��֤�û��Ƿ�ӵ��ָ��Ȩ��
        /// </summary>
        public static bool HasPermission(int userId, string permissionCode)
        {
            try
            {
                if (userId == 1) return true; // ��������Ա

                if (_userPermissions.TryGetValue(userId, out var permissions))
                {
                    // ֱ�Ӽ��
                    if (permissions.Contains(permissionCode))
                        return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"��֤Ȩ��ʧ�ܣ�{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// ˢ��Ȩ������
        /// </summary>
        public static void RefreshPermissions(int userId, int roleId)
        {
            _userPermissions.Clear();
            _controlPermissions.Clear();
            Initialize(userId, roleId);
        }
    }
}
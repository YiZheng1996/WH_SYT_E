namespace MainUI.BLL
{
    /// <summary>
    /// 权限分配业务逻辑类
    /// </summary>
    public class PermissionAllocationBLL
    {
        private readonly IFreeSql _fsql = VarHelper.fsql;

        /// <summary>
        /// 为指定角色分配权限
        /// </summary>
        public bool SetRolePermissions(int roleId, List<int> permissionIds)
        {
            try
            {
                // 检查角色是否存在且未被删除
                if (!_fsql.Select<RoleModel>()
                    .Where(r => r.ID == roleId && r.IsDelete == 0)
                    .Any())
                {
                    return false;
                }

                _fsql.Transaction(() =>
               {
                   // 删除已有的角色-权限关联
                   _fsql.Delete<User_permissionModel>()
                       .Where(rp => rp.User_id == roleId)
                       .ExecuteAffrows();

                   // 添加新的角色-权限关联
                   if (permissionIds?.Count > 0)
                   {
                       var rolePermissions = permissionIds.Select(pid => new User_permissionModel
                       {
                           User_id = roleId,
                           Permission_id = pid,
                           IsDelete = 0
                       });

                       _fsql.Insert<User_permissionModel>()
                           .AppendData(rolePermissions)
                           .ExecuteAffrows();
                   }
               });
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"分配角色权限失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取用户权限信息
        /// </summary>
        public List<string> GetUserPermissionCodes(int roleId)
        {
            try
            {
                return _fsql.Select<User_permissionModel, PermissionModel>()
                    .LeftJoin((u, p) => u.Permission_id == p.ID)
                    .Where((u, p) => u.User_id == roleId &&
                                   u.IsDelete == 0 &&
                                   p.IsDelete == 0)
                    .ToList((u, p) => p.PermissionCode);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取用户权限失败：{ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// 获取角色绑定的权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<User_permissionModel> GetPermissionChecks(int UserId) =>
           _fsql.Select<User_permissionModel>()
                .Where(x => x.User_id == UserId && x.IsDelete == 0)
                .ToList();

        /// <summary>
        /// 当删除权限时，删除对应的用户权限关联
        /// </summary>
        /// <param name="Permission_id"></param>
        /// <returns></returns>
        public bool DelUserPermission(int Permission_id) =>
            _fsql.Delete<User_permissionModel>()
                 .Where(x => x.Permission_id == Permission_id)
                 .ExecuteAffrows() > 0;
    }

    /// <summary>
    /// 权限管理业务逻辑类
    /// </summary>
    public class PermissionBLL
    {
        private readonly IFreeSql _fsql = VarHelper.fsql;

        #region 基础权限管理

        /// <summary>
        /// 获取所有权限
        /// </summary>
        public List<PermissionModel> GetPermissions() => 
            VarHelper.fsql.Select<PermissionModel>().ToList();

        /// <summary>
        /// 检查控件名称是否已存在
        /// </summary>
        /// <param name="controlName">控件名称</param>
        /// <param name="excludeId">排除的ID（用于编辑时排除自己）</param>
        /// <returns>true=已存在，false=不存在</returns>
        public bool IsControlNameExists(string controlName, int excludeId = 0)
        {
            // 如果控件名称为空，返回false（允许为空）
            if (string.IsNullOrWhiteSpace(controlName))
                return false;

            try
            {
                // 查询是否存在相同的控件名称
                var query = VarHelper.fsql.Select<PermissionModel>()
                    .Where(p => p.ControlName == controlName.Trim());

                // 如果是编辑操作，排除当前记录自己
                if (excludeId > 0)
                {
                    query = query.Where(p => p.ID != excludeId);
                }

                return query.Any();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"检查控件名称是否存在时发生错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 检查权限代码是否已存在
        /// </summary>
        /// <param name="permissionCode">权限代码</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns></returns>
        public bool IsPermissionCodeExists(string permissionCode, int excludeId = 0)
        {
            if (string.IsNullOrWhiteSpace(permissionCode))
                return false;

            try
            {
                var query = VarHelper.fsql.Select<PermissionModel>()
                    .Where(p => p.PermissionCode == permissionCode.Trim());

                if (excludeId > 0)
                {
                    query = query.Where(p => p.ID != excludeId);
                }

                return query.Any();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"检查权限代码是否存在时发生错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 添加权限（带重复检查）
        /// </summary>
        public bool AddPermission(PermissionModel model)
        {
            try
            {
                // 验证必填字段
                if (string.IsNullOrWhiteSpace(model.PermissionName))
                {
                    throw new Exception("权限名称不能为空！");
                }

                if (string.IsNullOrWhiteSpace(model.PermissionCode))
                {
                    throw new Exception("权限代码不能为空！");
                }

                // 检查权限代码是否重复
                if (IsPermissionCodeExists(model.PermissionCode))
                {
                    throw new Exception($"权限代码 '{model.PermissionCode}' 已存在，请使用其他代码！");
                }

                // 检查控件名称是否重复（如果填写了控件名称）
                if (!string.IsNullOrWhiteSpace(model.ControlName) &&
                    IsControlNameExists(model.ControlName))
                {
                    throw new Exception($"控件名称 '{model.ControlName}' 已存在，请使用其他名称！");
                }

                // 插入数据
                int affectedRows = VarHelper.fsql.Insert(model).ExecuteAffrows();

                if (affectedRows > 0)
                {
                    NlogHelper.Default.Info($"成功添加权限：{model.PermissionName}");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"添加权限失败：{ex.Message}", ex);
                throw; // 重新抛出异常，让调用方处理
            }
        }

        /// <summary>
        /// 执行权限操作
        /// </summary>
        private bool ExecutePermissionOperation(Func<IFreeSql, bool> operation)
        {
            try
            {
                return operation(_fsql);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"执行权限操作失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 更新权限（带重复检查）
        /// </summary>
        public bool UpdatePermission(PermissionModel model)
        {
            try
            {
                // 验证必填字段
                if (string.IsNullOrWhiteSpace(model.PermissionName))
                {
                    throw new Exception("权限名称不能为空！");
                }

                if (string.IsNullOrWhiteSpace(model.PermissionCode))
                {
                    throw new Exception("权限代码不能为空！");
                }

                // 检查权限代码是否重复（排除自己）
                if (IsPermissionCodeExists(model.PermissionCode, model.ID))
                {
                    throw new Exception($"权限代码 '{model.PermissionCode}' 已存在，请使用其他代码！");
                }

                // 检查控件名称是否重复（排除自己）
                if (!string.IsNullOrWhiteSpace(model.ControlName) &&
                    IsControlNameExists(model.ControlName, model.ID))
                {
                    throw new Exception($"控件名称 '{model.ControlName}' 已存在，请使用其他名称！");
                }

                // 更新数据
                int affectedRows = VarHelper.fsql.Update<PermissionModel>()
                    .SetSource(model)
                    .ExecuteAffrows();

                if (affectedRows > 0)
                {
                    NlogHelper.Default.Info($"成功更新权限：{model.PermissionName}");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"更新权限失败：{ex.Message}", ex);
                throw; // 重新抛出异常，让调用方处理
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        public bool DelPermission(int id)
        {
            try
            {
                int affectedRows = VarHelper.fsql.Delete<PermissionModel>()
                    .Where(p => p.ID == id)
                    .ExecuteAffrows();

                if (affectedRows > 0)
                {
                    NlogHelper.Default.Info($"成功删除权限 ID：{id}");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"删除权限失败：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 根据ID获取权限
        /// </summary>
        public PermissionModel GetPermissionById(int id)
        {
            try
            {
                return VarHelper.fsql.Select<PermissionModel>()
                    .Where(p => p.ID == id)
                    .First();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取权限失败：{ex.Message}", ex);
                return null;
            }

        }
        #endregion
    }
}

/// <summary>
/// 角色管理业务逻辑类
/// </summary>
public class RoleBLL
{
    private readonly IFreeSql _fsql = VarHelper.fsql;

    /// <summary>
    /// 执行角色操作
    /// </summary>
    private bool ExecuteRoleOperation(Func<IFreeSql, bool> operation)
    {
        try
        {
            return operation(_fsql);
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"执行角色操作失败：{ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// 获取所有角色
    /// </summary>
    public List<RoleModel> GetRoles() =>
        _fsql.Select<RoleModel>()
            .Where(x => x.IsDelete == 0)
            .ToList();

    /// <summary>
    /// 新增角色
    /// </summary>
    public bool AddRole(RoleModel model) =>
        ExecuteRoleOperation(db =>
            db.Insert(model).ExecuteAffrows() > 0);

    /// <summary>
    /// 更新角色
    /// </summary>
    public bool UpdateRole(RoleModel model) =>
        ExecuteRoleOperation(db =>
            db.Update<RoleModel>()
              .SetSource(model)
              .Where(x => x.ID == model.ID)
              .ExecuteAffrows() > 0);

    /// <summary>
    /// 删除角色
    /// </summary>
    public bool DelRole(int id) =>
        ExecuteRoleOperation(db =>
            db.Update<RoleModel>()
              .Set(x => x.IsDelete, 1)
              .Set(x => x.DeleteTime, DateTime.Now)
              .Where(x => x.ID == id)
              .ExecuteAffrows() > 0);
}

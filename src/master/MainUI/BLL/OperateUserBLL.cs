namespace MainUI.BLL
{
    internal class OperateUserBLL
    {
        // 更新用户密码
        public bool UpdateUserPwd(int ID, string pwd) =>
            VarHelper.fsql.Update<OperateUserModel>()
                  .Set(o => o.Password, pwd)
                  .Where(o => o.ID == ID)
                  .ExecuteAffrows() > 0;

        // 根据ID获取用户信息
        public OperateUserNewModel SelectUser(string username)
        {
            return VarHelper.fsql.Select<OperateUserModel, RoleModel>()
                .LeftJoin((m, t) => m.Role_ID == t.ID)
                .Where((m, t) => m.Username == username)
                .ToList((m, t) => new OperateUserNewModel())
                .FirstOrDefault();
        }

        // 根据ID获取用户信息
        public OperateUserNewModel SelectUser(OperateUserNewModel model)
        {
            return VarHelper.fsql.Select<OperateUserModel, RoleModel>()
                .LeftJoin((m, t) => m.Role_ID == t.ID)
                .Where((m, t) => m.ID == model.ID)
                .ToList((m, t) => new OperateUserNewModel())
                .FirstOrDefault();
        }

        // 更新用户信息
        public bool UpdateUser(OperateUserNewModel model) => VarHelper.fsql.Update<OperateUserNewModel>()
                   .Set(a => a.Username, model.Username)
                   .Set(a => a.Password, model.Password)
                   .Where(a => a.ID == model.ID)
                   .ExecuteAffrows() > 0;

        // 添加用户信息
        public bool AddUser(OperateUserNewModel model) =>
            VarHelper.fsql.Insert(model).ExecuteAffrows() > 0;

        // 根据工号获取用户信息
        public OperateUserModel GetJobNumber(OperateUserNewModel model) => VarHelper.fsql
            .Select<OperateUserNewModel>()
            .Where(a => a.ID == model.ID)
            .First();

        // 获取所有用户信息
        public List<OperateUserNewModel> GetUsers() =>
            VarHelper.fsql.Select<OperateUserModel, RoleModel>()
                .LeftJoin((m, t) => m.Role_ID == t.ID)
                .ToList((m, t) => new OperateUserNewModel());

        // 删除用户信息
        public int RemoveByUserJob(OperateUserModel model) => VarHelper.fsql.Delete<OperateUserModel>()
            .Where(a => a.ID == model.ID)
            .ExecuteAffrows();

        // 更新用户信息
        public int UpdateUserID(OperateUserModel model) => VarHelper.fsql.Update<OperateUserModel>()
                   .Set(a => a.Username, model.Username)
                   .Where(a => a.ID == model.ID)
                   .ExecuteAffrows();

        // 手动添加时
        public bool HandMovementUserTable(OperateUserNewModel model)
        {
            var data = GetJobNumber(model);
            return data != null ? HandUpdateUser(model) : AddUser(model);
        }

        // 手动更新用户信息
        public bool HandUpdateUser(OperateUserNewModel model) =>
            VarHelper.fsql.Update<OperateUserNewModel>()
               .Set(a => a.Username, model.Username)
               .Set(a => a.Role_ID, model.Role_ID)
               .Where(a => a.ID == model.ID)
               .ExecuteAffrows() > 0;

    }
}

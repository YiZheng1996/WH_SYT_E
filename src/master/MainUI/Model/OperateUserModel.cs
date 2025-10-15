using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace MainUI.Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table(Name = "Users")]
    public class OperateUserModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int Role_ID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 试验台ID
        /// </summary>
        public int TestBenchID { get; set; }

    }

    [Table(Name = "Users")]
    public class OperateUserNewModel : OperateUserModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Column(IsIgnore = true)]
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [Column(IsIgnore = true)]
        public string Describe { get; set; }

        public void InitUser(OperateUserNewModel user)
        {
            Username = user.Username;
            Password = user.Password;
            Role_ID = user.Role_ID;
            ID = user.ID;
            Sort = user.Sort;
            RoleName = user.RoleName;
            Describe = user.Describe;
        }
    }

    /// <summary>
    /// 角色表
    /// </summary>
    [Table(Name = "Role")]
    public class RoleModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(0)]
        public int IsDelete { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime DeleteTime { get; set; }
    }


    /// <summary>
    /// 权限表
    /// </summary>
    [Table(Name = "Permission")]
    public class PermissionModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// 控件名称
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string PermissionNotes { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(0)]
        public int IsDelete { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime DeleteTime { get; set; }
    }


    /// <summary>
    /// 角色权限关联表
    /// </summary>
    [Table(Name = "User_permission")]
    public class User_permissionModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int User_id { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int Permission_id { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(0)]
        public int IsDelete { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime DeleteTime { get; set; }
    }
}

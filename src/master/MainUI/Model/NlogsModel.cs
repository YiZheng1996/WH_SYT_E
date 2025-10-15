using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    [Table(Name = "LoggerInfo")]
    public class NlogsModel
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; } 

        /// <summary>
        /// 记录时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime MessTime { get; set; }

        /// <summary>
        /// 错误等级
        /// </summary>
        [Column(StringLength = 100)]
        public string Level { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [Column(DbType = "TEXT")]
        public string Message { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        [Column(StringLength = 100)]
        public string UserName { get; set; }

        /// <summary>
        /// 标题信息
        /// </summary>
        [Column(StringLength = 100)]
        public string MessageName { get; set; }

        /// <summary>
        /// 错误来源
        /// </summary>
        [Column(StringLength = 200)]
        public string Source { get; set; }

        /// <summary>
        /// 试验台ID
        /// </summary>
        public int TestBenchID { get; set; }
    }
}

using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    [Table(Name = "testbench")]
    public class TestBenchModel
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 实验台编号
        /// </summary>
        [Column(StringLength = 50)]
        public string BenchCode { get; set; }

        /// <summary>
        /// 实验台名称
        /// </summary>
        [Column(StringLength = 100)]
        public string BenchName { get; set; }

        /// <summary>
        /// 实验台位置
        /// </summary>
        [Column(StringLength = 200)]
        public string Location { get; set; }

        /// <summary>
        /// 状态:1启用 0停用
        /// </summary>
        [Column(MapType = typeof(bool))]
        public bool Status { get; set; }

        /// <summary>
        /// 实验台IP
        /// </summary>
        [Column(StringLength = 100)]
        public string BenchIP { get; set; }

        /// <summary>
        /// 数据查看范围 0=仅本实验台,1=所有实验台
        /// </summary>
        public int DataScope { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
    }
}

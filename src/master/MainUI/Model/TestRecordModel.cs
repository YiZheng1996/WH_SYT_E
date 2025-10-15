using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    [Table(Name = "Record")]
    public class TestRecordModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 类型名称ID
        /// </summary>
        public int KindID { get; set; }

        /// <summary>
        /// 型号名称ID
        /// </summary>
        public int ModelID { get; set; }

        /// <summary>
        /// 车型车号编号
        /// </summary>
        [Column(StringLength = 200)]
        public string TestID { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        [Column(StringLength = 100)]
        public string Tester { get; set; }

        /// <summary>
        /// 保存时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime TestTime { get; set; }

        /// <summary>
        /// 保存报告路径
        /// </summary>
        [Column(StringLength = 500)]
        public string ReportPath { get; set; }

        /// <summary>
        /// 试验台ID
        /// </summary>
        public int TestBenchID { get; set; }
    }

    public class TestRecordModelDto : TestRecordModel
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Column(StringLength = 100)]
        public string ModelTypeName { get; set; }

        /// <summary>
        /// 型号名称
        /// </summary>
        [Column(StringLength = 100)]
        public string ModelName { get; set; }

        /// <summary>
        /// 试验台名称 - 新增字段
        /// </summary>
        [Column(StringLength = 100)]
        public string TestBenchName { get; set; }
    }
}

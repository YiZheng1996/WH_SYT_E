using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    /// <summary>
    /// 试验步骤实体类
    /// </summary>
    [Table(Name = "TestStep")]
    public class TestStepModel
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 步骤号
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        [Column(StringLength = 100)]
        public string ProcessName { get; set; }

        /// <summary>
        /// 产品型号ID
        /// </summary>
        public int ModelID { get; set; }

        /// <summary>
        /// 测试过程ID
        /// </summary>
        public int TestProcessID { get; set; }

        /// <summary>
        /// 试验台ID
        /// </summary>
        public int TestBenchID { get; set; }
    }

    public class TestStepNewModel : TestStepModel
    {
        /// <summary>
        /// 获取或设置测试进程的名称
        /// </summary>
        [Column(StringLength = 100)]
        public string TestProcessName { get; set; }

        /// <summary>
        /// 是否在测试中可见
        /// </summary>
        [Column(MapType = typeof(bool))]
        public bool IsVisible { get; set; }
    }
}

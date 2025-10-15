using AntdUI;
using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    /// <summary>
    /// 测试流程实体类
    /// </summary>
    [Table(Name = "TestProcess")]
    public class TestProcessModel : NotifyProperty
    {
        /// <summary>
        /// ID 主键，自增
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 流程项点名称
        /// </summary>
        [Column(StringLength = 100)]
        public string ProcessName { get; set; }

        /// <summary>
        /// 实体类名称，用于反射创建实例
        /// </summary>
        [Column(StringLength = 100)]
        public string EntityClassName { get; set; }

        /// <summary>
        /// 型号类型ID
        /// </summary>
        public int ModelTypeID { get; set; }

        /// <summary>
        /// 试验台ID
        /// </summary>
        public int TestBenchID { get; set; }

        /// <summary>
        /// 是否在测试中可见
        /// </summary>
        [Column(MapType = typeof(bool))]
        public bool IsVisible { get; set; }

        private bool _enable;
        [Column(IsIgnore = true)]
        public bool Enable
        {
            get => _enable;
            set
            {
                if (_enable == value) return;
                _enable = value;
                OnPropertyChanged(nameof(Enable));
            }
        }


        /// <summary>
        /// 生成的类文件路径（仅用于显示，不存储到数据库）
        /// </summary>
        [Column(IsIgnore = true)]
        public string ClassFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(EntityClassName))
                    return "";
                return Path.Combine("MainUI", "Procedure", "Test", $"{EntityClassName}.cs");
            }
        }

        /// <summary>
        /// 类文件是否存在（仅用于显示，不存储到数据库）
        /// </summary>
        [Column(IsIgnore = true)]
        public bool ClassFileExists
        {
            get
            {
                if (string.IsNullOrEmpty(EntityClassName))
                    return false;

                string fullPath = Path.Combine(Application.StartupPath, "MainUI", "Procedure", "Test", $"{EntityClassName}.cs");
                return File.Exists(fullPath);
            }
        }

    }
}

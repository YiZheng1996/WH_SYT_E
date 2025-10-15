using AntdUI;
using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace MainUI.Model
{
    /// <summary>
    /// 维保计量提醒模型
    /// </summary>
    [Table(Name = "MeteringRemind")]
    public class MeteringRemindModel : NotifyProperty
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 检查类型
        /// </summary>
        [Column(StringLength = 100)]
        public string InspectType { get; set; }

        /// <summary>
        /// 检查名称
        /// </summary>
        [Column(StringLength = 100)]
        public string InspectName { get; set; }

        /// <summary>
        /// 当前检查时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime CurrentTime { get; set; }
        public string CurrenDate => CurrentTime.ToString("yyyy-MM-dd");

        /// <summary>
        /// 下次检查时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime NextTime { get; set; }
        public string NextDate => NextTime.ToString("yyyy-MM-dd");

        /// <summary>
        /// 检查说明
        /// </summary>
        [Column(StringLength = 500)]
        public string InspectDescribe { get; set; }

        /// <summary>
        /// 周期/天
        /// </summary>
        public int Cycle { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(0)]
        [Column(MapType = typeof(bool))]
        public int IsDelete { get; set; }

        /// <summary>
        /// 试验台ID
        /// </summary>
        [DefaultValue(0)]
        public int TestBenchID { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime DeleteTime { get; set; }

        CellLink[] _Buttns =
            [new CellButton("Save", "检查完成", TTypeMini.Warn)
                     {
                         BorderWidth = 1,
                         Back = Color.FromArgb(80, 160, 255),
                         BackHover = Color.FromArgb(115, 179, 255),
                         BackActive = Color.FromArgb(80, 160, 255),
                         DefaultBack = Color.FromArgb(80, 160, 255),
                     },
             ];

        [Column(IsIgnore = true)]
        public CellLink[] Buttns
        {
            get => _Buttns;
            set
            {
                _Buttns = value;
                OnPropertyChanged(nameof(Buttns));
            }
        }
    }
}

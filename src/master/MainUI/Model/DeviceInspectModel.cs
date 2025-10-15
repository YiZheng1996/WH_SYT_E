using AntdUI;
using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace MainUI.Model
{
    /// <summary>
    /// 设备检查模型
    /// </summary>
    [Table(Name = "DeviceInspect")]
    public class DeviceInspectModel : NotifyProperty
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 部件类型
        /// </summary>
        [Column(StringLength = 100)]
        public string PartType { get; set; }

        /// <summary>
        /// 部件名称
        /// </summary>
        [Column(StringLength = 200)]
        public string PartName { get; set; }

        /// <summary>
        /// 使用时长(天)
        /// </summary>
        [DefaultValue(0)]
        public int UseDuration { get; set; }

        /// <summary>
        /// 动作次数
        /// </summary>
        [DefaultValue(0)]
        public int ActionNumber { get; set; }
        
        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(0)]
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
           [
            new CellButton("NumberClearing", "次数清零", TTypeMini.Warn)
                     {
                         BorderWidth = 1,
                         Back = Color.FromArgb(80, 160, 255),
                         BackHover = Color.FromArgb(115, 179, 255),
                         BackActive = Color.FromArgb(80, 160, 255),
                         DefaultBack = Color.FromArgb(80, 160, 255),
                     },
            new CellButton("TimeClearing", "时间清零", TTypeMini.Warn)
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

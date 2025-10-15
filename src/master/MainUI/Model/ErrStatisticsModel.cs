using AntdUI;
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MainUI.Model
{
    /// <summary>
    /// 问题统计模型
    /// </summary>
    [Table(Name = "ErrStatistics")]
    public class ErrStatisticsModel : NotifyProperty
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 问题类型
        /// </summary>
        [Column(StringLength = 100)]
        public string ErrType { get; set; }

        /// <summary>
        /// 问题详细内容
        /// </summary>
        [Column(StringLength = 500)]
        public string ErrContent { get; set; }

        /// <summary>
        /// 问题记录时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime ErrTime { get; set; }
        public string ErrDate => ErrTime.ToString("yyyy-MM-dd");

        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(0)]
        [Column(MapType = typeof(bool))]
        public int IsDelete { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime DeleteTime { get; set; }

        /// <summary>
        /// 试验台ID
        /// </summary>
        [DefaultValue(0)]
        public int TestBenchID { get; set; }
    }
}

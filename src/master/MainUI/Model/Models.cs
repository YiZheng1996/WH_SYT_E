using AntdUI;
using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace MainUI.Model
{
    /// <summary>
    /// 模型类型表
    /// </summary>
    [Table(Name = "ModelTypeTable")]
    public class ModelsType : NotifyProperty
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Column(StringLength = 100)]
        public string ModelTypeName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(StringLength = 500)]
        public string Mark { get; set; }

        /// <summary>
        /// 试验台ID - 新增字段
        /// </summary>
        [DefaultValue(0)]
        public int TestBenchID { get; set; }

        /// <summary>
        /// 产品型号集合
        /// </summary>
        private NewModels[] _newmodels;
        [Column(IsIgnore = true)]
        public NewModels[] NewModels
        {
            get => _newmodels;
            set
            {
                _newmodels = value;
                OnPropertyChanged(nameof(NewModels));
            }
        }
    }

    /// <summary>
    /// 产品型号表
    /// </summary>
    [Table(Name = "ModelsTable")]
    public class Models : NotifyProperty
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 型号名称
        /// </summary>
        [Column(StringLength = 100)]
        public string ModelName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(StringLength = 500)]
        public string Mark { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(StringLength = 100)]
        public string CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column(StringLength = 200)]
        public string UpdateTime { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        [Column(MapType = typeof(bool))]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 图号
        /// </summary>
        [Column(StringLength = 200)]
        public string DrawingNo { get; set; }

        /// <summary>
        /// 公司项目编号
        /// </summary>
        [Column(StringLength = 200)]
        public string CompanyProjectNo { get; set; }

        /// <summary>
        /// 客户项目编号
        /// </summary>
        [Column(StringLength = 200)]
        public string CustomerProjectNo { get; set; }

        /// <summary>
        /// 是否是发布
        /// </summary>
        [DefaultValue(0)]
        [Column(MapType = typeof(bool))]
        public bool IsRelease { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [DefaultValue("未发布")]
        public string ReleaseTime { get; set; }

        CellLink[] _Buttns =
         [new CellButton("Release", "发布", TTypeMini.Primary)
            {
                BorderWidth = 1,
                Fore = Color.FromArgb(236, 236, 236),
                Back = Color.FromArgb(90, 124, 236),
                BackHover = Color.FromArgb(90, 124, 236),
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

    public class NewModels : Models
    {
        public int ModelTypeID { get; set; }

        public int ModelID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Column(StringLength = 100)]
        public string ModelTypeName { get; set; }

        /// <summary>
        /// 产品摘要（用于按钮显示文字）
        /// 格式: 型号名 | 图号 | 公司编号 | 客户编号
        /// 空字段自动跳过
        /// </summary>
        [Column(IsIgnore = true)]
        public string ProductSummary
        {
            get
            {
                var parts = new List<string>();

                if (!string.IsNullOrWhiteSpace(ModelName))
                    parts.Add(ModelName);

                if (!string.IsNullOrWhiteSpace(DrawingNo))
                    parts.Add(DrawingNo);

                if (!string.IsNullOrWhiteSpace(CompanyProjectNo))
                    parts.Add(CompanyProjectNo);

                if (!string.IsNullOrWhiteSpace(CustomerProjectNo))
                    parts.Add(CustomerProjectNo);

                return parts.Count > 0 ? string.Join(" | ", parts) : "未命名产品";
            }
        }

        /// <summary>
        /// 产品文件夹名称（用于路径构建）
        /// 格式: {ID}_{ModelName}
        /// </summary>
        [Column(IsIgnore = true)]
        public string ProductFolderName => $"{ID}_{ModelName}";
    }
}
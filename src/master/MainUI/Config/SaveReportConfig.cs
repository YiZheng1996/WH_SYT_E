namespace MainUI.Config
{
    /// <summary>
    /// 保存报表配置类
    /// </summary>
    public class SaveReportConfig : IniConfig
    {
        public SaveReportConfig()
            : base(Application.StartupPath + "config\\SaveReportPath.ini")
        {
            Load();
        }

        public SaveReportConfig(string sectionName)
            : base(Application.StartupPath + "config\\SaveReportPath.ini")
        {
            SetSectionName(sectionName);
            Load();
        }

        /// <summary>
        /// 报表保存路径
        /// </summary>
        [IniKeyName("报表保存路径")]
        public string RptSaveFile { get; set; }

        /// <summary>
        /// 是否同时保存PDF
        /// </summary>
        [IniKeyName("保存PDF")]
        public bool SavePDF { get; set; }

        /// <summary>
        /// 文件名包含产品型号
        /// </summary>
        [IniKeyName("文件名包含产品型号")]
        public bool IncludeModelName { get; set; } = true;

        /// <summary>
        /// 文件名包含产品编号
        /// </summary>
        [IniKeyName("文件名包含产品编号")]
        public bool IncludeProductNo { get; set; } = true;

        /// <summary>
        /// 文件名包含综合判定
        /// </summary>
        [IniKeyName("文件名包含综合判定")]
        public bool IncludeTestResult { get; set; } = true;

        /// <summary>
        /// 文件名包含保存时间
        /// </summary>
        [IniKeyName("文件名包含保存时间")]
        public bool IncludeSaveTime { get; set; } = true;

        /// <summary>
        /// Excel保护密码
        /// </summary>
        [IniKeyName("Excel保护密码")]
        public string ExcelPassword { get; set; }
    }
}

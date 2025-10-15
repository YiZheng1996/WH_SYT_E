namespace MainUI.Config
{
    /// <summary>
    /// 保存报表配置类
    /// </summary>
    internal class SaveReportConfig : IniConfig
    {
        public SaveReportConfig()
         : base(Application.StartupPath + "config\\SaveReportPath.ini") { }

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
    }
}

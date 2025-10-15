namespace MainUI.Config
{
    public class ParaConfig : IniConfig
    {
        public ParaConfig()
          : base(Application.StartupPath + "config\\Para.ini")
        {
        }
        public ParaConfig(string sectionName)
            : base(Application.StartupPath + "config\\Para.ini")
        {
            this.SetSectionName(sectionName);
            Load();
        }
        /// <summary>
        /// 报表名称
        /// </summary>
        [IniKeyName("报表名称")]
        public string RptFile { get; set; }

        /// <summary>
        /// 报表保存路径
        /// </summary>
        [IniKeyName("报表保存路径")]
        public string RptSaveFile { get; set; }
    }
}

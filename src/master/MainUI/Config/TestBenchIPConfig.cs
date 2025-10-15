namespace MainUI.Config
{
    /// <summary>
    /// 试验台IP配置类
    /// </summary>
    public class TestBenchIPConfig : IniConfig
    {
        public TestBenchIPConfig()
            : base(Application.StartupPath + "\\config\\TestBenchIP.ini")
        {
            Load();
        }

        public TestBenchIPConfig(string sectionName)
            : base(Application.StartupPath + "\\config\\TestBenchIP.ini")
        {
            SetSectionName(sectionName);
            Load();
        }

        /// <summary>
        /// 已选择的IP地址
        /// </summary>
        [IniKeyName("已选择IP")]
        public string SelectedIP { get; set; }
    }
}
namespace MainUI.Config
{
    /// <summary>
    /// 部署配置 - 控制本机的运行模式与路径
    /// </summary>
    public class DeploymentConfig
    {
        /// <summary>共享目录根路径（如 \\Server\MainUI）</summary>
        public string BasePath { get; set; } = string.Empty;

        /// <summary>本地缓存路径（执行端测试时优先读这里）</summary>
        public string LocalCachePath { get; set; } = string.Empty;

        /// <summary>是否为编辑模式（true=编辑端A, false=执行端B/C/D/E）</summary>
        public bool IsEditMode { get; set; }

        /// <summary>本机标识</summary>
        public string MachineName { get; set; } = Environment.MachineName;

        /// <summary>配置同步轮询间隔（秒）</summary>
        public int ConfigSyncIntervalSeconds { get; set; } = 5;

        /// <summary>是否启用Git版本管理</summary>
        public bool EnableGitVersioning { get; set; } = true;

        /// <summary>
        /// 执行端真正读配置的根路径 = LocalCachePath
        /// 编辑端读写都走 BasePath
        /// </summary>
        public string EffectiveReadPath => IsEditMode ? BasePath : LocalCachePath;

        /// <summary>写入路径永远是BasePath（只有编辑端能写）</summary>
        public string WritePath => BasePath;
    }
}
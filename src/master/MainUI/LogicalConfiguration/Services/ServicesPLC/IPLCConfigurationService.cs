namespace MainUI.LogicalConfiguration.Services.ServicesPLC
{
    /// <summary>
    /// PLC配置服务接口 - 专门处理配置文件读写
    /// 
    /// 职责：
    /// 1. 配置文件的读取和解析
    /// 2. 配置验证和缓存
    /// 3. 配置变更监控
    /// </summary>
    public interface IPLCConfigurationService
    {
        /// <summary>
        /// 异步加载配置
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>配置内容字典</returns>
        Task<Dictionary<string, Dictionary<string, string>>> LoadConfigurationAsync(string configPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取配置内容（只读）
        /// </summary>
        IReadOnlyDictionary<string, Dictionary<string, string>> Configuration { get; }

        /// <summary>
        /// 刷新配置
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        Task RefreshConfigurationAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 配置变更事件
        /// </summary>
        event EventHandler<ConfigurationChangedEventArgs> ConfigurationChanged;
    }

    /// <summary>
    /// 配置变更事件参数
    /// </summary>
    public class ConfigurationChangedEventArgs : EventArgs
    {
        public string ModuleName { get; init; } = string.Empty;
        public Dictionary<string, string> NewConfiguration { get; init; } = new();
        public DateTime ChangedAt { get; init; } = DateTime.Now;
    }
}

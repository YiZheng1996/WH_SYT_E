using Microsoft.Extensions.Logging;
using System.Text;

namespace MainUI.LogicalConfiguration.Services.ServicesPLC
{
    /// <summary>
    /// PLC点位配置管理，实现IPLCConfigurationService接口
    /// 支持依赖注入和异步操作，默认地址为 Bin\Modules\MyModules.ini
    /// </summary>
    public sealed class PLCConfigurationService : IniConfig, IPLCConfigurationService, IDisposable
    {
        private const string MODULE_FILE_PATH = "Modules\\MyModules.ini";
        private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        private readonly ILogger<PLCConfigurationService> _logger;
        private readonly ReaderWriterLockSlim _configLock = new();
        private readonly FileSystemWatcher _fileWatcher;
        private readonly string _configurationPath;
        private bool _disposed = false;

        /// <summary>
        /// 存储所有模块配置内容的字典
        /// Key: 区段名称, Value: 该区段下的所有配置项
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, string>> _dicModelsContent;

        /// <summary>
        /// 获取模块配置内容字典 - IPLCConfigurationService接口实现
        /// </summary>
        public IReadOnlyDictionary<string, Dictionary<string, string>> Configuration
        {
            get
            {
                _configLock.EnterReadLock();
                try
                {
                    // 返回深拷贝以确保线程安全和不可变性
                    return _dicModelsContent.ToDictionary(
                        kvp => kvp.Key,
                        kvp => new Dictionary<string, string>(kvp.Value)
                    );
                }
                finally
                {
                    _configLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// 配置变更事件 - IPLCConfigurationService接口实现
        /// </summary>
        public event EventHandler<ConfigurationChangedEventArgs> ConfigurationChanged;

        #region 构造函数

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="configPath">可选的配置文件路径，为空时使用默认路径</param>
        public PLCConfigurationService(ILogger<PLCConfigurationService> logger, string configPath = null)
            : base(configPath ?? AppPath + MODULE_FILE_PATH)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configurationPath = configPath ?? AppPath + MODULE_FILE_PATH;
            _dicModelsContent = new Dictionary<string, Dictionary<string, string>>();

            // 设置文件监控
            _fileWatcher = CreateFileWatcher(_configurationPath);

            // 异步初始化
            _ = Task.Run(InitializeAsync);
        }

        #endregion

        #region IPLCConfigurationService接口实现

        /// <summary>
        /// 异步加载配置 - IPLCConfigurationService接口实现
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>配置内容字典</returns>
        public async Task<Dictionary<string, Dictionary<string, string>>> LoadConfigurationAsync(
            string configPath,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("开始异步加载PLC配置文件: {ConfigPath}", configPath);

                // 检查文件是否存在
                if (!File.Exists(configPath))
                {
                    _logger.LogWarning("配置文件不存在: {ConfigPath}", configPath);
                    return [];
                }

                // 在后台线程中执行配置加载
                var newConfiguration = await Task.Run(() =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    // 允许使用GB2312编码格式
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    // 创建临时IniConfig实例来读取指定路径的配置
                    var tempConfig = new IniConfig(configPath);
                    return LoadConfigurationFromIni(tempConfig, cancellationToken);
                }, cancellationToken);

                // 线程安全地更新配置
                _configLock.EnterWriteLock();
                try
                {
                    _dicModelsContent.Clear();
                    foreach (var kvp in newConfiguration)
                    {
                        _dicModelsContent[kvp.Key] = kvp.Value;
                    }
                }
                finally
                {
                    _configLock.ExitWriteLock();
                }

                _logger.LogInformation("成功加载PLC配置，共 {Count} 个模块", newConfiguration.Count);

                // 触发配置变更事件
                OnConfigurationChanged(newConfiguration);

                return newConfiguration;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("加载PLC配置操作被取消: {ConfigPath}", configPath);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载PLC配置文件失败: {ConfigPath}", configPath);
                throw new InvalidOperationException($"加载PLC配置失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 刷新配置 - IPLCConfigurationService接口实现
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task RefreshConfigurationAsync(CancellationToken cancellationToken = default)
        {
            await LoadConfigurationAsync(_configurationPath, cancellationToken);
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取所有配置区段
        /// </summary>
        public string[] GetSections() => Config.GetSections();

        /// <summary>
        /// 获取指定区段的配置内容
        /// </summary>
        /// <param name="section">区段名称</param>
        /// <returns>区段配置字典</returns>
        public Dictionary<string, string> GetSectionContent(string section)
        {
            _configLock.EnterReadLock();
            try
            {
                return _dicModelsContent.TryGetValue(section, out var content)
                    ? new Dictionary<string, string>(content)
                    : null;
            }
            finally
            {
                _configLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取指定区段和键的值
        /// </summary>
        /// <param name="section">区段名称</param>
        /// <param name="key">键名</param>
        /// <returns>配置值</returns>
        public string GetValue(string section, string key)
        {
            _configLock.EnterReadLock();
            try
            {
                if (_dicModelsContent.TryGetValue(section, out var sectionContent))
                {
                    return sectionContent.TryGetValue(key, out var value) ? value : null;
                }
                return null;
            }
            finally
            {
                _configLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 重新加载配置内容
        /// </summary>
        public void Reload()
        {
            try
            {
                _ = Task.Run(async () => await RefreshConfigurationAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "同步重新加载配置时发生错误");
                throw;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 异步初始化
        /// </summary>
        private async Task InitializeAsync()
        {
            try
            {
                await LoadConfigurationAsync(_configurationPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化PLC点位配置时发生错误");
                // 不抛出异常，避免阻塞构造函数
            }
        }

        /// <summary>
        /// 从IniConfig加载配置内容
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> LoadConfigurationFromIni(
            IniConfig iniConfig,
            CancellationToken cancellationToken)
        {
            var configuration = new Dictionary<string, Dictionary<string, string>>();

            // 获取所有配置区段
            var sections = iniConfig.Config.GetSections();

            // 如果没有找到任何区段，则返回空字典
            if (sections == null || sections.Length == 0)
            {
                _logger.LogWarning("没有找到任何配置区段");
                return configuration;
            }

            // 遍历所有区段并获取其键值对
            foreach (string section in sections)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // 跳过空区段
                if (string.IsNullOrWhiteSpace(section)) continue;

                // 获取当前区段的所有键
                var sectionKeys = iniConfig.Config.GetKeys(section);

                // 跳过没有键的区段
                if (sectionKeys == null || sectionKeys.Length == 0) continue;

                // 创建一个字典来存储当前区段的键值对
                var sectionContent = new Dictionary<string, string>(sectionKeys.Length);

                // 遍历当前区段的所有键，并获取对应的值
                foreach (var key in sectionKeys)
                {
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        // 获取键对应的值，并添加到当前区段的字典中
                        var value = iniConfig.Config.GetString(section, key);
                        sectionContent[key] = value;
                    }
                }

                // 如果当前区段有内容，则添加到总字典中
                if (sectionContent.Count > 0)
                {
                    configuration[section] = sectionContent;
                }
            }

            return configuration;
        }

        /// <summary>
        /// 创建文件监控器
        /// </summary>
        private FileSystemWatcher CreateFileWatcher(string configPath)
        {
            try
            {
                var directory = Path.GetDirectoryName(configPath);
                var fileName = Path.GetFileName(configPath);

                if (string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(fileName))
                {
                    _logger.LogWarning("无法创建文件监控器，路径无效: {ConfigPath}", configPath);
                    return null;
                }

                var watcher = new FileSystemWatcher(directory, fileName)
                {
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                    EnableRaisingEvents = true
                };

                watcher.Changed += async (sender, e) =>
                {
                    try
                    {
                        _logger.LogInformation("检测到配置文件变更: {FilePath}", e.FullPath);
                        // 延迟一点时间，避免文件被占用
                        await Task.Delay(500);
                        await RefreshConfigurationAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "处理配置文件变更时发生错误");
                    }
                };

                _logger.LogInformation("已启用配置文件监控: {ConfigPath}", configPath);
                return watcher;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "创建文件监控器失败: {ConfigPath}", configPath);
                return null;
            }
        }

        /// <summary>
        /// 触发配置变更事件
        /// </summary>
        private void OnConfigurationChanged(Dictionary<string, Dictionary<string, string>> newConfiguration)
        {
            if (ConfigurationChanged != null)
            {
                foreach (var module in newConfiguration)
                {
                    var args = new ConfigurationChangedEventArgs
                    {
                        ModuleName = module.Key,
                        NewConfiguration = module.Value,
                        ChangedAt = DateTime.Now
                    };
                    ConfigurationChanged.Invoke(this, args);
                }
            }
        }

        #endregion

        #region IDisposable实现

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                try
                {
                    _fileWatcher?.Dispose();
                    _configLock?.Dispose();
                    _disposed = true;
                    _logger.LogInformation("TestAA资源已释放");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "释放TestAA资源时发生异常");
                }
            }
        }

        #endregion
    }
    /// <summary>
    /// PLC配置选项
    /// </summary>
    public class PLCConfigurationOptions
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string ConfigurationPath { get; set; } = "Modules\\MyModules.ini";

        /// <summary>
        /// 是否启用文件监控
        /// </summary>
        public bool EnableFileWatching { get; set; } = true;

        /// <summary>
        /// 配置缓存超时时间（分钟）
        /// </summary>
        public int CacheTimeoutMinutes { get; set; } = 30;
    }
}
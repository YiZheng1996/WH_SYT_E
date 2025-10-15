using Microsoft.Extensions.Logging;
using RW.Modules;
using System.Collections.Concurrent;

namespace MainUI.LogicalConfiguration.Services.ServicesPLC
{
    /// <summary>
    /// PLC模块提供者实现
    /// 
    /// 职责：
    /// 1. 管理BaseModule实例的生命周期
    /// 2. 提供模块查找和验证功能
    /// 3. 处理模块初始化异常
    /// </summary>
    public class PLCModuleProvider(ILogger<PLCModuleProvider> logger) : IPLCModuleProvider, IDisposable
    {
        private readonly ILogger<PLCModuleProvider> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private ConcurrentDictionary<string, BaseModule> _modules = new();
        private readonly ReaderWriterLockSlim _modulesLock = new();
        private bool _disposed = false;

        /// <summary>
        /// 异步初始化PLC模块
        /// </summary>
        public async Task<Dictionary<string, BaseModule>> InitializePLCModulesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("开始初始化PLC模块集合");

                // 异步初始化ModuleComponent
                await Task.Run(() => ModuleComponent.Instance.Init(), cancellationToken);

                // 获取模块列表
                var moduleList = ModuleComponent.Instance.GetList();

                if (moduleList == null || moduleList.Count == 0)
                {
                    _logger.LogWarning("未找到任何PLC模块，返回空字典");
                    return [];
                }

                // 更新内部缓存
                _modulesLock.EnterWriteLock();
                try
                {
                    _modules.Clear();
                    foreach (var kvp in moduleList)
                    {
                        _modules.TryAdd(kvp.Key, kvp.Value);
                    }
                }
                finally
                {
                    _modulesLock.ExitWriteLock();
                }

                _logger.LogInformation("成功初始化 {Count} 个PLC模块: {Modules}",
                    moduleList.Count, string.Join(", ", moduleList.Keys));

                return new Dictionary<string, BaseModule>(moduleList);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("PLC模块初始化被取消");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PLC模块初始化失败");
                return [];
            }
        }

        /// <summary>
        /// 获取指定模块
        /// </summary>
        public BaseModule GetModule(string moduleName)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
                return null;

            _modulesLock.EnterReadLock();
            try
            {
                return _modules.TryGetValue(moduleName, out var module) ? module : null;
            }
            finally
            {
                _modulesLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取所有模块名称
        /// </summary>
        public IEnumerable<string> GetModuleNames()
        {
            _modulesLock.EnterReadLock();
            try
            {
                return _modules.Keys.ToList(); // 返回副本避免并发问题
            }
            finally
            {
                _modulesLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 检查模块是否存在
        /// </summary>
        public bool ModuleExists(string moduleName)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
                return false;

            _modulesLock.EnterReadLock();
            try
            {
                return _modules.ContainsKey(moduleName);
            }
            finally
            {
                _modulesLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _modulesLock.Dispose();
                // 注意：不释放BaseModule实例，因为它们可能被其他服务使用
                _disposed = true;
            }
        }
    }
}

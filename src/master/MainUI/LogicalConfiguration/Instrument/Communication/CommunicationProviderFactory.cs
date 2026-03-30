using MainUI.LogicalConfiguration.Instrument.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// 通讯提供者工厂 - 单例模式版本
    ///
    /// 使用单例模式确保全局只有一个Factory实例
    /// 所有串口连接通过同一个Factory管理，避免重复打开
    /// 增加线程安全的资源清理机制
    /// </summary>
    public class CommunicationProviderFactory
    {
        private static readonly Lazy<CommunicationProviderFactory> _instance =
            new(() => new CommunicationProviderFactory());

        private readonly ConcurrentDictionary<string, ICommunicationProvider> _providerCache = new();
        private readonly SemaphoreSlim _cleanupLock = new(1, 1);
        private ILogger _logger;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static CommunicationProviderFactory Instance => _instance.Value;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunicationProviderFactory()
        {
        }

        /// <summary>
        /// 设置日志记录器
        /// </summary>
        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取或创建通讯提供者
        /// </summary>
        public ICommunicationProvider GetOrCreateProvider(ProtocolType protocolType, string connectionId = null)
        {
            var key = $"{protocolType}_{connectionId ?? "default"}";

            return _providerCache.GetOrAdd(key, _ =>
            {
                _logger?.LogDebug("创建新的通讯提供者: {Key}", key);
                return CreateProvider(protocolType);
            });
        }

        /// <summary>
        /// 创建新的通讯提供者（不放入缓存，用于测试连接等临时场景）
        /// </summary>
        public ICommunicationProvider CreateProvider(ProtocolType protocolType)
        {
            return protocolType switch
            {
                ProtocolType.TcpIp => new TcpCommunicationProvider(_logger),
                ProtocolType.Serial => new SerialCommunicationProvider(_logger),
                ProtocolType.Http => new HttpCommunicationProvider(_logger),
                ProtocolType.ModbusTcp or ProtocolType.ModbusRtu => new ModbusCommunicationProvider(_logger),
                ProtocolType.Udp => new UdpCommunicationProvider(_logger),
                _ => throw new NotSupportedException($"不支持的协议类型: {protocolType}")
            };
        }

        /// <summary>
        /// 释放所有提供者
        /// 提供异步版本，避免 UI 线程调用 .Wait() 死锁
        /// </summary>
        public async Task DisposeAllAsync()
        {
            await _cleanupLock.WaitAsync();
            try
            {
                _logger?.LogInformation("开始释放所有通讯提供者，总数: {Count}", _providerCache.Count);

                foreach (var kvp in _providerCache)
                {
                    try
                    {
                        _logger?.LogDebug("释放通讯提供者: {Key}", kvp.Key);
                        // 使用 await 替代 .Wait()，避免死锁 
                        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
                        try
                        {
                            await kvp.Value.DisconnectAsync().WaitAsync(cts.Token);
                        }
                        catch (OperationCanceledException)
                        {
                            _logger?.LogWarning("断开提供者超时: {Key}", kvp.Key);
                        }
                        kvp.Value.Dispose();
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogWarning(ex, "释放提供者时发生异常: {Key}", kvp.Key);
                    }
                }

                _providerCache.Clear();
                _logger?.LogInformation("所有通讯提供者已释放");
            }
            finally
            {
                _cleanupLock.Release();
            }
        }

        /// <summary>
        /// 释放所有提供者（同步版本，仅在非 UI 线程或应用退出时使用）
        /// 保留同步版本但用 Task.Run 包裹避免死锁
        /// </summary>
        public void DisposeAll()
        {
            // 如果当前在 UI 线程（SynchronizationContext 不为 null），
            // 使用 Task.Run 避免死锁
            if (SynchronizationContext.Current != null)
            {
                Task.Run(async () => await DisposeAllAsync()).GetAwaiter().GetResult();
            }
            else
            {
                // 非 UI 线程可以安全同步等待
                DisposeAllAsync().GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// 移除并释放指定提供者
        /// </summary>
        public async Task<bool> RemoveProviderAsync(string key)
        {
            if (!_providerCache.TryRemove(key, out var provider)) return false;

            try
            {
                _logger?.LogDebug("移除并释放提供者: {Key}", key);
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
                try
                {
                    await provider.DisconnectAsync().WaitAsync(cts.Token);
                }
                catch (OperationCanceledException)
                {
                    _logger?.LogWarning("断开提供者超时: {Key}", key);
                }
                provider.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "移除提供者时发生异常: {Key}", key);
                return false;
            }
        }

        /// <summary>
        /// 移除并释放指定提供者（同步版本，保持向后兼容）
        /// 内部改用安全方式
        /// </summary>
        public bool RemoveProvider(string key)
        {
            if (!_providerCache.TryRemove(key, out var provider)) return false;

            try
            {
                _logger?.LogDebug("移除并释放提供者: {Key}", key);

                // 用 Task.Run 包裹防止在 UI 线程死锁
                Task.Run(async () =>
                {
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
                    try
                    {
                        await provider.DisconnectAsync().WaitAsync(cts.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        _logger?.LogWarning("断开提供者超时: {Key}", key);
                    }
                }).GetAwaiter().GetResult();

                provider.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "移除提供者时发生异常: {Key}", key);
                // 即使断开失败，也尝试 Dispose
                try { provider.Dispose(); } catch { }
                return false;
            }
        }

        /// <summary>
        /// 强制释放指定串口连接
        /// </summary>
        public bool ForceReleaseSerialPort(string portName)
        {
            var key = $"Serial_{portName}";
            _logger?.LogInformation("强制释放串口连接: {PortName}", portName);
            return RemoveProvider(key);
        }

        /// <summary>
        /// 强制释放指定串口连接（异步版本）
        /// </summary>
        public async Task<bool> ForceReleaseSerialPortAsync(string portName)
        {
            var key = $"Serial_{portName}";
            _logger?.LogInformation("强制释放串口连接: {PortName}", portName);
            return await RemoveProviderAsync(key);
        }

        /// <summary>
        /// 获取当前活动连接数
        /// </summary>
        public int GetActiveConnectionCount()
        {
            return _providerCache.Count(kvp => kvp.Value.IsConnected);
        }

        /// <summary>
        /// 获取所有活动连接信息
        /// </summary>
        public Dictionary<string, bool> GetConnectionStatus()
        {
            return _providerCache.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.IsConnected
            );
        }
    }
}
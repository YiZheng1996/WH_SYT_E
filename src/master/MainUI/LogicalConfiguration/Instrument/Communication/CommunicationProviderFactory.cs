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
            new Lazy<CommunicationProviderFactory>(() => new CommunicationProviderFactory());

        private readonly ConcurrentDictionary<string, ICommunicationProvider> _providerCache = new();
        private readonly object _cleanupLock = new object();
        private ILogger _logger;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static CommunicationProviderFactory Instance => _instance.Value;

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private CommunicationProviderFactory()
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
        /// 创建新的通讯提供者
        /// </summary>
        private ICommunicationProvider CreateProvider(ProtocolType protocolType)
        {
            return protocolType switch
            {
                ProtocolType.TcpIp => new TcpCommunicationProvider(_logger),
                ProtocolType.Serial => new SerialCommunicationProvider(_logger),
                ProtocolType.Http => new HttpCommunicationProvider(_logger),
                ProtocolType.ModbusTcp or ProtocolType.ModbusRtu => new ModbusCommunicationProvider(_logger),
                _ => throw new NotSupportedException($"不支持的协议类型: {protocolType}")
            };
        }

        /// <summary>
        /// 释放所有提供者
        /// </summary>
        public void DisposeAll()
        {
            lock (_cleanupLock)
            {
                _logger?.LogInformation("开始释放所有通讯提供者，总数: {Count}", _providerCache.Count);

                foreach (var kvp in _providerCache)
                {
                    try
                    {
                        _logger?.LogDebug("释放通讯提供者: {Key}", kvp.Key);
                        kvp.Value.DisconnectAsync().Wait(TimeSpan.FromSeconds(2));
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
        }

        /// <summary>
        /// 移除并释放指定提供者
        /// </summary>
        public bool RemoveProvider(string key)
        {
            if (!_providerCache.TryRemove(key, out var provider)) return false;

            try
            {
                _logger?.LogDebug("移除并释放提供者: {Key}", key);
                provider.DisconnectAsync().Wait(TimeSpan.FromSeconds(2));
                provider.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "移除提供者时发生异常: {Key}", key);
                return false;
            }
            return false;
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
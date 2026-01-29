using MainUI.LogicalConfiguration.Instrument.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using System.Collections.Concurrent;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// 通讯提供者工厂
    /// </summary>
    public class CommunicationProviderFactory(ILogger logger = null)
    {
        private readonly ConcurrentDictionary<string, ICommunicationProvider> _providerCache = new();

        /// <summary>
        /// 获取或创建通讯提供者
        /// </summary>
        public ICommunicationProvider GetOrCreateProvider(ProtocolType protocolType, string connectionId = null)
        {
            var key = $"{protocolType}_{connectionId ?? "default"}";

            return _providerCache.GetOrAdd(key, _ => CreateProvider(protocolType));
        }

        /// <summary>
        /// 创建新的通讯提供者
        /// </summary>
        public ICommunicationProvider CreateProvider(ProtocolType protocolType)
        {
            return protocolType switch
            {
                ProtocolType.TcpIp => new TcpCommunicationProvider(logger),
                ProtocolType.Serial => new SerialCommunicationProvider(logger),
                ProtocolType.Http => new HttpCommunicationProvider(logger),
                ProtocolType.ModbusTcp or ProtocolType.ModbusRtu => new ModbusCommunicationProvider(logger),
                _ => throw new NotSupportedException($"不支持的协议类型: {protocolType}")
            };
        }

        /// <summary>
        /// 释放所有提供者
        /// </summary>
        public void DisposeAll()
        {
            foreach (var provider in _providerCache.Values)
            {
                provider.Dispose();
            }
            _providerCache.Clear();
        }

        /// <summary>
        /// 移除并释放指定提供者
        /// </summary>
        public void RemoveProvider(string key)
        {
            if (_providerCache.TryRemove(key, out var provider))
            {
                provider.Dispose();
            }
        }
    }
}

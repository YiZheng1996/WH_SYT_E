using MainUI.LogicalConfiguration.Parameter;
using static MainUI.LogicalConfiguration.Parameter.Parameter_WritePLC;

namespace MainUI.LogicalConfiguration.Services.ServicesPLC
{
    /// <summary>
    /// PLC管理器接口 - 定义所有PLC操作的契约
    /// 
    /// 设计原则：
    /// 1. 接口分离原则 - 分离读写、配置、监控功能
    /// 2. 依赖倒置原则 - 高层模块不依赖低层模块
    /// 3. 单一职责原则 - 每个方法职责明确
    /// </summary>
    public interface IPLCManager
    {
        #region 基础属性和状态

        /// <summary>
        /// 检查PLC模块是否已成功初始化
        /// </summary>
        bool IsPLCInitialized { get; }

        #endregion

        #region 异步PLC操作接口

        /// <summary>
        /// 获取所有模块的点位信息
        /// </summary>
        /// <returns>模块点位字典，Key为模块名，Value为点位列表</returns>
        Task<Dictionary<string, List<string>>> GetModuleTagsAsync();

        /// <summary>
        /// 获取指定模块的点位信息
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>点位列表</returns>
        Task<List<string>> GetModuleTagsAsync(string moduleName);

        /// <summary>
        /// 异步读取单个PLC点位值
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="address">点位地址</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>读取的值</returns>
        Task<object> ReadPLCValueAsync(string moduleName, string address, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步写入单个PLC点位值
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="address">点位地址</param>
        /// <param name="value">要写入的值</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>是否写入成功</returns>
        Task<bool> WritePLCValueAsync(string moduleName, string address, object value, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步批量读取PLC点位值
        /// </summary>
        /// <param name="readItems">读取项列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>读取结果字典</returns>
        Task<Dictionary<string, object>> BatchReadPLCAsync(IEnumerable<PlcReadItem> readItems, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步批量写入PLC点位值
        /// </summary>
        /// <param name="writeItems">写入项列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>成功写入的数量</returns>
        Task<int> BatchWritePLCAsync(IEnumerable<PLCWriteItem> writeItems, CancellationToken cancellationToken = default);

        #endregion

        #region 模块管理接口

        /// <summary>
        /// 检查指定模块是否在线
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>是否在线</returns>
        Task<bool> IsModuleOnlineAsync(string moduleName);

        /// <summary>
        /// 获取PLC操作统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<PLCOperationStats> GetOperationStatsAsync();

        /// <summary>
        /// 验证PLC配置是否有效
        /// </summary>
        /// <param name="config">PLC配置</param>
        /// <returns>是否有效</returns>
        bool ValidatePlcConfig(PlcAddressConfig config);

        #endregion

        #region 专用业务接口

        /// <summary>
        /// 检测工具专用的PLC读取方法
        /// </summary>
        /// <param name="plcConfig">PLC配置</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>读取的值</returns>
        Task<object> ReadPLCForDetectionAsync(PlcAddressConfig plcConfig, CancellationToken cancellationToken = default);

        #endregion
    }
}

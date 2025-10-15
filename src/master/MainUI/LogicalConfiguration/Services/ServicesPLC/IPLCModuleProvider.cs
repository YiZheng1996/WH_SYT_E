using RW.Modules;

namespace MainUI.LogicalConfiguration.Services.ServicesPLC
{
    /// <summary>
    /// PLC模块提供者接口 - 负责模块的创建和管理
    /// 
    /// 职责分离：
    /// 1. 与IPLCManager分离，单一职责
    /// 2. 专注于模块的生命周期管理
    /// 3. 便于替换不同的模块提供者实现
    /// </summary>
    public interface IPLCModuleProvider
    {
        /// <summary>
        /// 异步初始化所有PLC模块
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>初始化的模块字典</returns>
        Task<Dictionary<string, BaseModule>> InitializePLCModulesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取指定名称的模块
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>模块实例，如果不存在返回null</returns>
        BaseModule GetModule(string moduleName);

        /// <summary>
        /// 获取所有模块名称
        /// </summary>
        /// <returns>模块名称集合</returns>
        IEnumerable<string> GetModuleNames();

        /// <summary>
        /// 检查模块是否存在
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>是否存在</returns>
        bool ModuleExists(string moduleName);
    }
}

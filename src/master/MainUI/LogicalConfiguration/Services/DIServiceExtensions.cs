using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.LogicalConfiguration.Services
{
    /// <summary>
    /// 依赖注入服务扩展类
    /// 
    /// 这个类提供了便捷的扩展方法来注册我们的服务
    /// 使用扩展方法的好处：
    /// 1. 集中管理服务注册逻辑
    /// 2. 提供清晰的 API
    /// 3. 易于维护和修改
    /// </summary>
    public static class DIServiceExtensions
    {
        /// <summary>
        /// 注册工作流状态服务到依赖注入容器
        /// 
        /// 注册策略说明：
        /// - 使用 AddSingleton：整个应用程序生命周期内只有一个实例
        /// - 这符合原来 SingletonStatus 的行为
        /// - 确保状态在整个应用程序中保持一致
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合（支持链式调用）</returns>
        public static IServiceCollection AddWorkflowServices(this IServiceCollection services)
        {
            // 注册接口和实现的映射
            // 这样可以通过接口进行依赖注入，提高可测试性
            services.AddSingleton<IWorkflowStateService, WorkflowStateService>();

            // 可以在这里注册其他相关服务
            // services.AddScoped<IWorkflowManager, WorkflowManager>();
            // services.AddTransient<IWorkflowValidator, WorkflowValidator>();

            return services;
        }

        /// <summary>
        /// 注册工作流服务的重载方法，支持自定义配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configureOptions">配置选项的委托</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddWorkflowServices(
            this IServiceCollection services,
            Action<WorkflowServiceOptions> configureOptions = null,
            Action<PLCConfigurationOptions> configureConfigOptions = null)
        {
            // 配置选项
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }
            if (configureConfigOptions != null)
            {
                services.Configure(configureConfigOptions);
            }

            // 注册主要服务及PLC相关服务
            services.AddSingleton<IWorkflowStateService, WorkflowStateService>();
            services.AddSingleton<IPLCConfigurationService, PLCConfigurationService>();
            services.AddSingleton<IPLCModuleProvider, PLCModuleProvider>();
            //services.AddSingleton<IPLCManager, PLCManager>();
            services.AddScoped<IPLCManager, PLCManager>();

            return services;
        }
    }

    /// <summary>
    /// 工作流服务配置选项
    /// 
    /// 可以用来配置服务的行为，例如：
    /// - 是否启用事件日志
    /// - 性能监控设置
    /// - 缓存策略等
    /// </summary>
    public class WorkflowServiceOptions
    {
        /// <summary>
        /// 是否启用事件日志记录
        /// 默认为 false，避免性能影响
        /// </summary>
        public bool EnableEventLogging { get; set; } = false;

        /// <summary>
        /// 是否启用性能监控
        /// 默认为 false，生产环境可以开启
        /// </summary>
        public bool EnablePerformanceMonitoring { get; set; } = false;

        /// <summary>
        /// 变量缓存的最大大小
        /// 用于控制内存使用
        /// </summary>
        public int MaxVariableCacheSize { get; set; } = 1000;

        /// <summary>
        /// 步骤缓存的最大大小
        /// </summary>
        public int MaxStepCacheSize { get; set; } = 500;
    }
}

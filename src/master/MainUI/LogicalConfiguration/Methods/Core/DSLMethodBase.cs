using MainUI.LogicalConfiguration.Infrastructure;
using System.Runtime.CompilerServices;

namespace MainUI.LogicalConfiguration.Methods.Core
{
    /// <summary>
    /// DSL方法基类 - 统一错误处理
    /// </summary>
    public abstract class DSLMethodBase : IDSLMethod
    {
        public abstract string Category { get; }
        public abstract string Description { get; }

        /// <summary>
        /// 执行方法并记录日志（带返回值）
        /// </summary>
        protected async Task<T> ExecuteWithLogging<T>(
            object parameter,
            Func<Task<T>> action,
            T defaultValue = default,
            [CallerMemberName] string methodName = "")
        {
            return await MethodExecutor.ExecuteAsync(methodName, parameter, action, defaultValue);
        }

        /// <summary>
        /// 执行方法并返回详细结果（无返回值）
        /// </summary>
        protected async Task<DetailedResult> ExecuteWithDetailedResult(
            object parameter,
            Func<Task> action,
            [CallerMemberName] string methodName = "")
        {
            return await MethodExecutor.ExecuteWithDetailAsync(methodName, parameter, action);
        }

        /// <summary>
        /// 执行方法并返回详细结果（带返回值）
        /// </summary>
        protected async Task<DetailedResult> ExecuteWithDetailedResult<T>(
            object parameter,
            Func<Task<T>> action,
            [CallerMemberName] string methodName = "")
        {
            return await MethodExecutor.ExecuteWithDetailAsync(methodName, parameter, action);
        }

    }

    /// <summary>
    /// DSL方法接口
    /// </summary>
    public interface IDSLMethod
    {
        /// <summary>
        /// 方法类别
        /// </summary>
        string Category { get; }

        /// <summary>
        /// 方法描述
        /// </summary>
        string Description { get; }
    }

}

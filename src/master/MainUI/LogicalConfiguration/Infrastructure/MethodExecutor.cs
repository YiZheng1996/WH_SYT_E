namespace MainUI.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 方法执行器
    /// </summary>
    public static class MethodExecutor
    {
        /// <summary>
        /// 执行异步方法并统一处理日志和异常
        /// </summary>
        public static async Task<T> ExecuteAsync<T>(
            string methodName,
            object parameter,
            Func<Task<T>> action,
            T defaultValue = default)
        {
            try
            {
                LogMethodStart(methodName, parameter);

                var result = await action();

                LogMethodSuccess(methodName, result);
                return result;
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);

                // 根据返回类型决定是抛出异常还是返回默认值
                if (typeof(T) == typeof(bool))
                {
                    return (T)(object)false; // bool类型返回false
                }

                return defaultValue; // 其他类型返回默认值或抛出异常
            }
        }

        /// <summary>
        /// 执行异步方法并返回详细结果
        /// </summary>
        public static async Task<DetailedResult> ExecuteWithDetailAsync(
            string methodName,
            object parameter,
            Func<Task> action)
        {
            try
            {
                LogMethodStart(methodName, parameter);

                await action();

                LogMethodSuccess(methodName, "执行完成");
                return DetailedResult.Successful();
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
                return DetailedResult.Failed(ex.Message);
            }
        }

        /// <summary>
        /// 执行异步方法并返回详细结果（带返回值）
        /// </summary>
        public static async Task<DetailedResult> ExecuteWithDetailAsync<T>(
            string methodName,
            object parameter,
            Func<Task<T>> action)
        {
            try
            {
                LogMethodStart(methodName, parameter);

                var result = await action();

                LogMethodSuccess(methodName, result);
                return DetailedResult.Successful(result);
            }
            catch (Exception ex)
            {
                LogMethodError(methodName, ex);
                return DetailedResult.Failed(ex.Message);
            }
        }

        /// <summary>
        /// 记录方法开始执行
        /// </summary>
        private static void LogMethodStart(string methodName, object parameter)
        {
            NlogHelper.Default.Info($"开始执行 {methodName}，参数: {parameter?.ToString() ?? "null"}");
        }

        /// <summary>
        /// 记录方法执行成功
        /// </summary>
        private static void LogMethodSuccess(string methodName, object result)
        {
            NlogHelper.Default.Info($"{methodName} 执行成功，结果: {result?.ToString() ?? "无返回值"}");
        }

        /// <summary>
        /// 记录方法执行失败
        /// </summary>
        private static void LogMethodError(string methodName, Exception ex)
        {
            NlogHelper.Default.Error($"{methodName} 执行失败: {ex.Message}", ex);
        }
    }
}

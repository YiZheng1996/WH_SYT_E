using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 参数管理器 - 提供从步骤参数中获取指定类型参数的功能
    /// </summary>
    internal class ParameterManager
    {
        /// <summary>
        /// 尝试从步骤参数中获取指定类型的参数对象
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="stepParameter">步骤参数对象</param>
        /// <param name="parameter">输出的参数对象</param>
        /// <returns>是否成功获取</returns>
        public static bool TryGetParameter<T>(object stepParameter, out T parameter) where T : class
        {
            parameter = null;

            if (stepParameter == null)
                return false;

            try
            {
                // 如果直接是目标类型
                if (stepParameter is T directParam)
                {
                    parameter = directParam;
                    return true;
                }

                // 如果是JSON字符串，尝试反序列化
                if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
                {
                    parameter = JsonConvert.DeserializeObject<T>(jsonStr);
                    return parameter != null;
                }

                // 如果是其他对象，尝试先序列化再反序列化
                if (stepParameter != null)
                {
                    var jsonString = JsonConvert.SerializeObject(stepParameter);
                    parameter = JsonConvert.DeserializeObject<T>(jsonString);
                    return parameter != null;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Debug($"参数转换失败 {typeof(T).Name}: {ex.Message}");
            }

            return false;
        }
    }
}

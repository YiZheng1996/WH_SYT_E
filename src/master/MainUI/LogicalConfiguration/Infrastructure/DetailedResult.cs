
namespace MainUI.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 方法执行详细结果
    /// </summary>
    public class DetailedResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息（失败时提供详细说明）
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 返回值（可选）
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        public static DetailedResult Successful(object value = null) => new()
        {
            Success = true,
            Value = value
        };

        /// <summary>
        /// 创建失败结果
        /// </summary>
        public static DetailedResult Failed(string errorMessage) => new()
        {
            Success = false,
            ErrorMessage = errorMessage
        };

        /// <summary>
        /// 隐式转换为 bool (向后兼容)
        /// </summary>
        public static implicit operator bool(DetailedResult result) => result.Success;

        /// <summary>
        /// 解构支持：var (success, error) = result;
        /// </summary>
        public void Deconstruct(out bool success, out string errorMessage)
        {
            success = Success;
            errorMessage = ErrorMessage;
        }
    }
}
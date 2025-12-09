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
        /// 下一步步骤索引（用于流程跳转）
        /// </summary>
        public int? NextStepIndex { get; set; }

        /// <summary>
        /// 消息（兼容 ExecutionResult.Message）
        /// </summary>
        public string Message
        {
            get => ErrorMessage;
            set => ErrorMessage = value;
        }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        public static DetailedResult Successful(object value = null, string message = "") => new()
        {
            Success = true,
            Value = value,
            ErrorMessage = message
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
        /// 创建跳转结果（用于流程控制）
        /// </summary>
        public static DetailedResult Jump(int stepIndex) => new()
        {
            Success = true,
            NextStepIndex = stepIndex
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

        /// <summary>
        /// 解构支持（包含跳转）：var (success, error, nextStep) = result;
        /// </summary>
        public void Deconstruct(out bool success, out string errorMessage, out int? nextStepIndex)
        {
            success = Success;
            errorMessage = ErrorMessage;
            nextStepIndex = NextStepIndex;
        }
    }
}
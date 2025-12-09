namespace MainUI.LogicalConfiguration.Engine
{
    #region 赋值结果类

    /// <summary>
    /// 赋值结果
    /// 统一的赋值操作结果类，包含执行信息和状态
    /// </summary>
    public class AssignmentResult
    {
        /// <summary>
        /// 目标变量名称
        /// </summary>
        public string TargetVariableName { get; set; }

        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 新赋的值
        /// </summary>
        public object NewValue { get; set; }

        /// <summary>
        /// 赋值前的旧值
        /// </summary>
        public object OldValue { get; set; }

        /// <summary>
        /// 执行耗时
        /// </summary>
        public TimeSpan ExecutionTime { get; set; }

        /// <summary>
        /// 验证错误列表
        /// </summary>
        public List<string> ValidationErrors { get; set; } = new();

        /// <summary>
        /// 创建成功结果
        /// </summary>
        public static AssignmentResult Succes(object newValue, object oldValue) =>
            new() { Success = true, NewValue = newValue, OldValue = oldValue };

        /// <summary>
        /// 创建失败结果
        /// </summary>
        public static AssignmentResult Error(string message) =>
            new() { Success = false, ErrorMessage = message };
    }

    #endregion

    #region 结果类定义

    /// <summary>
    /// 求值结果
    /// </summary>
    public class EvaluationResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 求值结果
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        public static EvaluationResult Succes(object result) =>
            new() { Success = true, Result = result };

        /// <summary>
        /// 创建失败结果
        /// </summary>
        public static EvaluationResult Error(string message) =>
            new() { Success = false, ErrorMessage = message };
    }

    /// <summary>
    /// 验证结果
    /// 包含错误和警告信息
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// 是否验证通过
        /// 注意：即使有警告，只要没有错误，IsValid 仍然为 true
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 验证消息
        /// 简短的验证结果描述
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误列表
        /// 导致验证失败的错误
        /// </summary>
        public List<string> Errors { get; set; } = new();

        /// <summary>
        /// 警告列表
        /// 不影响验证结果，但需要用户注意的问题
        /// 例如：类型转换警告、性能警告等
        /// </summary>
        public List<string> Warnings { get; set; } = new();

        /// <summary>
        /// 有错误
        /// </summary>
        public bool HasErrors => Errors?.Any() == true;

        /// <summary>
        /// 有警告
        /// </summary>
        public bool HasWarnings => Warnings?.Any() == true;

        /// <summary>
        /// 创建成功的验证结果
        /// </summary>
        public static ValidationResult Succes(string message = "验证通过")
        {
            return new ValidationResult
            {
                IsValid = true,
                Message = message
            };
        }

        /// <summary>
        /// 创建失败的验证结果
        /// </summary>
        public static ValidationResult Error(string message, params string[] errors)
        {
            var result = new ValidationResult
            {
                IsValid = false,
                Message = message
            };

            if (errors?.Length > 0)
            {
                result.Errors.AddRange(errors);
            }

            return result;
        }

        /// <summary>
        /// 添加警告
        /// </summary>
        public void AddWarning(string warning)
        {
            if (!string.IsNullOrWhiteSpace(warning))
            {
                Warnings.Add(warning);
            }
        }

        /// <summary>
        /// 添加错误
        /// </summary>
        public void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                IsValid = false;
                Errors.Add(error);
            }
        }

        /// <summary>
        /// 获取所有问题（错误+警告）
        /// </summary>
        public List<string> GetAllIssues()
        {
            var issues = new List<string>();
            if (HasErrors)
            {
                issues.AddRange(Errors.Select(e => $"错误: {e}"));
            }
            if (HasWarnings)
            {
                issues.AddRange(Warnings.Select(w => $"警告: {w}"));
            }
            return issues;
        }
    }

    #endregion

    #region 表达式验证
    /// <summary>
    /// 表达式验证上下文
    /// 为表达式验证提供额外的上下文信息，如目标变量名称和类型
    /// </summary>
    public class ValidationContext
    {
        /// <summary>
        /// 目标变量名称
        /// 用于在验证时提供更具体的错误信息
        /// </summary>
        public string TargetVariableName { get; set; }

        /// <summary>
        /// 目标变量类型
        /// 用于类型兼容性检查和警告
        /// </summary>
        public string TargetVariableType { get; set; }

        /// <summary>
        /// 是否严格模式
        /// true: 类型不匹配时报错; false: 类型不匹配时只警告
        /// </summary>
        public bool StrictMode { get; set; } = false;

        /// <summary>
        /// 是否允许PLC引用
        /// 在某些场景下可能需要禁止PLC引用
        /// </summary>
        public bool AllowPlcReferences { get; set; } = true;

        /// <summary>
        /// 是否允许函数调用
        /// 在某些简单场景下可能只需要基本的算术运算
        /// </summary>
        public bool AllowFunctionCalls { get; set; } = true;

        /// <summary>
        /// 自定义验证标签（用于日志和错误追踪）
        /// </summary>
        public string ValidationLabel { get; set; }

        /// <summary>
        /// 获取允许在应用程序中使用的运行时变量名的列表
        /// </summary>
        public List<string> RuntimeVariableWhitelist { get; internal set; }
    }
    #endregion
}

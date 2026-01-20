using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 条件判断执行方法
    /// 直接使用 ConditionExpression 进行条件判断
    /// TrueSteps 和 FalseSteps 类型统一为 List&lt;ChildModel&gt;
    /// </summary>
    public class ConditionMethods(
        ExpressionEngine expressionEngine,
        ILogger<ConditionMethods> logger) : DSLMethodBase
    {
        public override string Category => "条件判断工具";
        public override string Description => "条件判断";

        /// <summary>
        /// 异步执行条件判断 - 推荐使用此方法
        /// 避免UI线程死锁问题
        /// </summary>
        public async Task<ConditionEvaluationResult> EvaluateConditionAsync(Parameter_Condition parameter)
        {
            try
            {
                // 检查是否启用
                if (!parameter.IsEnabled)
                {
                    logger.LogInformation("条件判断已禁用，跳过执行: {Description}", parameter.Description);
                    return new ConditionEvaluationResult
                    {
                        ConditionMet = true,
                        StepsToExecute = null,
                        Description = parameter.Description,
                        Skipped = true
                    };
                }

                logger.LogInformation("开始条件判断: {Description}", parameter.Description);
                logger.LogDebug("条件表达式: {Expression}", parameter.ConditionExpression);

                // 使用异步方法计算条件结果
                var conditionResult = await EvaluateConditionExpressionAsync(parameter.ConditionExpression);

                logger.LogInformation("条件判断结果: {Result} ({Expression})",
                    conditionResult ? "满足条件" : "不满足条件",
                    parameter.ConditionExpression);

                // 根据结果选择执行分支
                var stepsToExecute = conditionResult ? parameter.TrueSteps : parameter.FalseSteps;

                return new ConditionEvaluationResult
                {
                    ConditionMet = conditionResult,
                    StepsToExecute = stepsToExecute,
                    Description = parameter.Description,
                    EvaluatedExpression = parameter.ConditionExpression
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "条件判断异常: {Message}", ex.Message);
                return new ConditionEvaluationResult
                {
                    ConditionMet = false,
                    StepsToExecute = null,
                    ErrorMessage = ex.Message,
                    EvaluatedExpression = parameter.ConditionExpression
                };
            }
        }

        /// <summary>
        /// 异步计算条件表达式结果
        /// </summary>
        private async Task<bool> EvaluateConditionExpressionAsync(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                logger.LogWarning("条件表达式为空，默认返回 false");
                return false;
            }

            try
            {
                // 使用异步版本的表达式求值
                var result = await expressionEngine.EvaluateExpressionAsync(expression);

                if (!result.Success)
                {
                    logger.LogError("条件表达式计算失败: {Expression}, 错误: {Error}",
                        expression, result.ErrorMessage);
                    return false;
                }

                // 处理结果
                if (result.Result is bool boolValue)
                {
                    return boolValue;
                }

                // 尝试转换为布尔值
                if (result.Result != null)
                {
                    return result.Result switch
                    {
                        // 数值类型：非零为true
                        int intValue => intValue != 0,
                        double doubleValue => Math.Abs(doubleValue) > double.Epsilon,
                        decimal decimalValue => decimalValue != 0,
                        // 字符串类型：非空为true
                        string strValue => !string.IsNullOrWhiteSpace(strValue) &&
                                           !strValue.Equals("false", StringComparison.OrdinalIgnoreCase) &&
                                           strValue != "0",
                        _ => Convert.ToBoolean(result.Result)
                    };
                }

                logger.LogWarning("条件表达式结果为 null，默认返回 false");
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "计算条件表达式时发生异常: {Expression}", expression);
                return false;
            }
        }
    }


    /// <summary>
    /// 条件判断结果
    /// </summary>
    public class ConditionEvaluationResult
    {
        /// <summary>
        /// 条件是否满足
        /// </summary>
        public bool ConditionMet { get; set; }

        /// <summary>
        /// 需要执行的步骤列表
        /// </summary>
        public List<ChildModel> StepsToExecute { get; set; }

        /// <summary>
        /// 条件描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 错误消息（如果有）
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否被跳过（禁用时）
        /// </summary>
        public bool Skipped { get; set; }

        /// <summary>
        /// 计算的表达式
        /// </summary>
        public string EvaluatedExpression { get; set; }

        /// <summary>
        /// 是否成功（无错误）
        /// </summary>
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
    }
}
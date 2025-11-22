using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 条件判断执行方法 - 只负责条件逻辑判断
    /// </summary>
    public class ConditionMethods(
        ExpressionEngine expressionEngine,
        ILogger<ConditionMethods> logger) : DSLMethodBase
    {
        private readonly ExpressionEngine _expressionEngine = expressionEngine;
        private readonly ILogger<ConditionMethods> _logger = logger;

        public override string Category => "条件判断工具";
        public override string Description => "条件判断";

        /// <summary>
        /// 执行条件判断 - 返回判断结果和需要执行的步骤列表
        /// </summary>
        public ConditionEvaluationResult EvaluateCondition(Parameter_Condition parameter)
        {
            try
            {
                _logger.LogInformation("开始条件判断: {Description}", parameter.Description);

                // 计算条件结果
                bool conditionResult = EvaluateConditionLogic(parameter);

                _logger.LogInformation($"条件判断结果: {(conditionResult ? "满足条件" : "不满足条件")}");

                // 根据结果选择执行分支
                var stepsToExecute = conditionResult ? parameter.TrueSteps : parameter.FalseSteps;

                return new ConditionEvaluationResult
                {
                    ConditionMet = conditionResult,
                    StepsToExecute = stepsToExecute,
                    Description = parameter.Description
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "条件判断异常: {Message}", ex.Message);
                return new ConditionEvaluationResult
                {
                    ConditionMet = false,
                    StepsToExecute = null,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// 计算条件结果
        /// </summary>
        private bool EvaluateConditionLogic(Parameter_Condition parameter)
        {
            try
            {
                // 计算左值
                var leftResult = _expressionEngine.EvaluateExpression(parameter.LeftExpression);
                if (!leftResult.Success)
                {
                    _logger.LogError($"左值表达式计算失败: {parameter.LeftExpression}");
                    return false;
                }

                // 处理布尔类型直接返回
                if (leftResult.Result is bool boolValue)
                {
                    _logger.LogInformation($"左值是布尔类型: {boolValue}");
                    return boolValue;
                }

                double leftValue = Convert.ToDouble(leftResult.Result);

                // 根据运算符类型进行判断
                switch (parameter.Operator)
                {
                    case ConditionOperator.在范围内:
                    case ConditionOperator.不在范围内:
                        return EvaluateRangeCondition(leftValue, parameter);

                    default:
                        return EvaluateComparisonCondition(leftValue, parameter);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"条件计算异常: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 计算范围条件
        /// </summary>
        private bool EvaluateRangeCondition(double leftValue, Parameter_Condition parameter)
        {
            var minResult = _expressionEngine.EvaluateExpression(parameter.RangeMin);
            var maxResult = _expressionEngine.EvaluateExpression(parameter.RangeMax);

            if (!minResult.Success || !maxResult.Success)
            {
                _logger.LogError("范围值计算失败");
                return false;
            }

            double minValue = Convert.ToDouble(minResult.Result);
            double maxValue = Convert.ToDouble(maxResult.Result);

            bool inRange = leftValue >= minValue && leftValue <= maxValue;

            _logger.LogInformation($"范围判断: {leftValue} 在 [{minValue}, {maxValue}] 之间 = {inRange}");

            return parameter.Operator == ConditionOperator.在范围内 ? inRange : !inRange;
        }

        /// <summary>
        /// 计算比较条件
        /// </summary>
        private bool EvaluateComparisonCondition(double leftValue, Parameter_Condition parameter)
        {
            var rightResult = _expressionEngine.EvaluateExpression(parameter.RightExpression);
            if (!rightResult.Success)
            {
                _logger.LogError($"右值表达式计算失败: {parameter.RightExpression}");
                return false;
            }

            double rightValue = Convert.ToDouble(rightResult.Result);

            bool result = parameter.Operator switch
            {
                ConditionOperator.等于 => Math.Abs(leftValue - rightValue) < 0.0001,
                ConditionOperator.不等于 => Math.Abs(leftValue - rightValue) >= 0.0001,
                ConditionOperator.大于 => leftValue > rightValue,
                ConditionOperator.大于等于 => leftValue >= rightValue,
                ConditionOperator.小于 => leftValue < rightValue,
                ConditionOperator.小于等于 => leftValue <= rightValue,
                _ => false
            };

            _logger.LogInformation($"比较判断: {leftValue} {parameter.Operator} {rightValue} = {result}");

            return result;
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
        public List<Parent> StepsToExecute { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 错误信息（如果有）
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
    }
}
using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 循环执行方法 - 只负责循环参数计算和计数器管理
    /// </summary>
    public class LoopMethods(
        ExpressionEngine expressionEngine,
        GlobalVariableManager globalVariableManager,
        ILogger<LoopMethods> logger) : DSLMethodBase
    {
        private readonly ExpressionEngine _expressionEngine = expressionEngine;
        private readonly GlobalVariableManager _globalVariableManager = globalVariableManager;
        private readonly ILogger<LoopMethods> _logger = logger;

        public override string Category => "循环执行工具";
        public override string Description => "循环执行工具";

        /// <summary>
        /// 计算循环参数 - 返回循环配置信息
        /// </summary>
        public LoopInfo EvaluateLoop(Parameter_Loop parameter)
        {
            try
            {
                int loopCount = EvaluateLoopCount(parameter.LoopCountExpression);

                if (loopCount <= 0)
                {
                    _logger.LogInformation($"循环次数为 {loopCount}，跳过循环");
                    return new LoopInfo { LoopCount = 0 };
                }

                _logger.LogInformation($"计算循环参数成功，共 {loopCount} 次 - {parameter.Description}");

                return new LoopInfo
                {
                    LoopCount = loopCount,
                    EnableCounter = parameter.EnableCounter,
                    CounterVariableName = parameter.CounterVariableName,
                    ChildSteps = parameter.ChildSteps,
                    Description = parameter.Description
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "循环参数计算异常: {Message}", ex.Message);
                return new LoopInfo
                {
                    LoopCount = 0,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// 更新循环计数器变量
        /// </summary>
        public void UpdateLoopCounter(string counterVariableName, int currentIndex)
        {
            if (string.IsNullOrWhiteSpace(counterVariableName)) return;

            try
            {
                var variable = _globalVariableManager.GetAllVariables()
                    .FirstOrDefault(v => v.VarName == counterVariableName);

                if (variable != null)
                {
                    variable.UpdateValue(currentIndex, $"循环计数器更新: {currentIndex}");
                }
                else
                {
                    // 如果变量不存在，创建一个临时变量
                    _globalVariableManager.AddOrUpdateVariable(new VarItem_Enhanced
                    {
                        VarName = counterVariableName,
                        VarValue = currentIndex,
                        VarType = "int",
                        LastUpdated = DateTime.Now,
                        IsAssignedByStep = true,
                        AssignmentType = VariableAssignmentType.DirectAssignment
                    });
                }

                _logger.LogDebug($"循环计数器已更新: {counterVariableName} = {currentIndex}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新循环计数器失败: {Variable}", counterVariableName);
            }
        }

        /// <summary>
        /// 计算循环次数
        /// </summary>
        private int EvaluateLoopCount(string expression)
        {
            try
            {
                var result = _expressionEngine.EvaluateExpression(expression);

                if (result.Success && result.Result != null)
                {
                    if (int.TryParse(result.Result.ToString(), out int count))
                    {
                        return count;
                    }
                }

                _logger.LogError($"无法计算循环次数: {expression}");
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "计算循环次数异常: {Expression}", expression);
                return 0;
            }
        }
    }

    /// <summary>
    /// 循环信息
    /// </summary>
    public class LoopInfo
    {
        /// <summary>
        /// 循环次数
        /// </summary>
        public int LoopCount { get; set; }

        /// <summary>
        /// 是否启用计数器
        /// </summary>
        public bool EnableCounter { get; set; }

        /// <summary>
        /// 计数器变量名
        /// </summary>
        public string CounterVariableName { get; set; }

        /// <summary>
        /// 子步骤列表
        /// </summary>
        public List<ChildModel> ChildSteps { get; set; }

        /// <summary>
        /// 描述
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
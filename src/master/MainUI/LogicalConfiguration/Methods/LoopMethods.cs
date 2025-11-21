using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 循环执行方法（包含循环控制）
    /// </summary>
    public class LoopMethods(
        IWorkflowStateService workflowStateService,
        ExpressionEngine expressionEngine,
        StepExecutionManager stepExecutionManager,
        ILogger<LoopMethods> logger) : DSLMethodBase()
    {
        private readonly IWorkflowStateService _workflowStateService = workflowStateService;
        private readonly ExpressionEngine _expressionEngine = expressionEngine;
        private readonly StepExecutionManager _stepExecutionManager = stepExecutionManager;
        private readonly ILogger<LoopMethods> _logger = logger;

        public override string Category => "循环执行执行工具";

        public override string Description => "循环执行执行工具";

        /// <summary>
        /// 执行循环
        /// </summary>
        public async Task ExecuteLoop(Parameter_Loop parameter, CancellationToken cancellationToken)
        {
            try
            {
                int loopCount = EvaluateLoopCount(parameter.LoopCountExpression);

                if (loopCount <= 0)
                {
                    _logger.LogInformation($"循环次数为 {loopCount}，跳过循环");
                    return;
                }

                _logger.LogInformation($"开始循环，共 {loopCount} 次 - {parameter.Description}");

                for (int i = 1; i <= loopCount; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    _logger.LogInformation($"========== 第 {i}/{loopCount} 次循环 ==========");

                    if (parameter.EnableCounter && !string.IsNullOrWhiteSpace(parameter.CounterVariableName))
                    {
                        GlobalVariableManager.SetVariable(parameter.CounterVariableName, i.ToString());
                    }

                    bool shouldBreak = false;
                    foreach (var childStep in parameter.ChildSteps)
                    {
                        try
                        {
                            await _stepExecutionManager.ExecuteStepAsync(childStep, cancellationToken);

                            if (_workflowStateService.ShouldBreakLoop)
                            {
                                _logger.LogError($"收到 Break 指令，跳出循环");
                                shouldBreak = true;
                                _workflowStateService.ShouldBreakLoop = false;
                                break;
                            }

                            if (_workflowStateService.ShouldContinueLoop)
                            {
                                _logger.LogInformation($"收到 Continue 指令，跳过本次循环剩余步骤");
                                _workflowStateService.ShouldContinueLoop = false;
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"子步骤执行失败: {ex.Message}");
                            throw;
                        }
                    }

                    if (shouldBreak) break;
                }

                _logger.LogInformation($"循环执行完成");
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("循环被取消");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"循环执行异常: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 执行循环控制（Break/Continue）
        /// </summary>
        public Task ExecuteLoopControl(Parameter_LoopControl parameter, CancellationToken cancellationToken)
        {
            try
            {
                if (parameter.ControlType == LoopControlType.跳出循环)
                {
                    _logger.LogInformation($"执行跳出循环（Break） - {parameter.Description}");
                    _workflowStateService.ShouldBreakLoop = true;
                }
                else
                {
                    _logger.LogInformation($"执行继续循环（Continue） - {parameter.Description}");
                    _workflowStateService.ShouldContinueLoop = true;
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"循环控制执行异常: {ex.Message}");
                throw;
            }
        }

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
                _logger.LogError($"计算循环次数异常: {ex.Message}");
                return 0;
            }
        }
    }
}
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 等待变量稳定方法类
    /// 提供监测变量变化并判断稳定性的功能
    /// </summary>
    public class WaitForStableMethods
    {
        private readonly GlobalVariableManager _globalVariableManager;
        private readonly ILogger<WaitForStableMethods> _logger;

        public WaitForStableMethods(
            GlobalVariableManager globalVariableManager,
            ILogger<WaitForStableMethods> logger)
        {
            _globalVariableManager = globalVariableManager ?? throw new ArgumentNullException(nameof(globalVariableManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 执行等待变量稳定逻辑
        /// </summary>
        /// <param name="param">参数配置</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>执行结果(成功/失败/超时)</returns>
        public async Task<WaitForStableResult> ExecuteWaitForStable(
            Parameter_WaitForStable param,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("开始等待变量稳定: {Variable}, 阈值: {Threshold}, 间隔: {Interval}秒",
                    param.MonitorVariable, param.StabilityThreshold, param.SamplingInterval);

                var startTime = DateTime.Now;
                int stableCountAchieved = 0;
                double? previousValue = null;
                double currentValue = 0;

                while (true)
                {
                    // 检查取消
                    if (cancellationToken.IsCancellationRequested)
                    {
                        _logger.LogWarning("等待变量稳定被取消");
                        return WaitForStableResult.Cancelled();
                    }

                    // 检查超时
                    if (param.TimeoutSeconds > 0)
                    {
                        var elapsed = (DateTime.Now - startTime).TotalSeconds;
                        if (elapsed > param.TimeoutSeconds)
                        {
                            _logger.LogWarning("等待变量稳定超时: {Elapsed}秒", elapsed);
                            return HandleTimeout(param, currentValue);
                        }
                    }

                    // 获取当前变量值
                    var variable = _globalVariableManager.GetAllVariables()
                        .FirstOrDefault(v => v.VarName == param.MonitorVariable);

                    if (variable == null)
                    {
                        _logger.LogError("监测变量不存在: {Variable}", param.MonitorVariable);
                        return WaitForStableResult.Failed($"变量 {param.MonitorVariable} 不存在");
                    }

                    // 尝试转换为数值
                    if (!double.TryParse(variable.VarValue?.ToString(), out currentValue))
                    {
                        _logger.LogError("变量值无法转换为数值: {Variable} = {Value}",
                            param.MonitorVariable, variable.VarValue);
                        return WaitForStableResult.Failed($"变量 {param.MonitorVariable} 的值无法转换为数值");
                    }

                    // 第一次采样,记录初始值
                    if (previousValue == null)
                    {
                        previousValue = currentValue;
                        _logger.LogDebug("首次采样: {Variable} = {Value}", param.MonitorVariable, currentValue);
                        await Task.Delay(param.SamplingInterval * 1000, cancellationToken);
                        continue;
                    }

                    // 计算变化率(绝对值)
                    double changeRate = Math.Abs(currentValue - previousValue.Value) / param.SamplingInterval;

                    _logger.LogDebug("采样: {Variable} = {Value}, 变化率 = {Rate:F4}",
                        param.MonitorVariable, currentValue, changeRate);

                    // 判断是否稳定
                    if (changeRate <= param.StabilityThreshold)
                    {
                        stableCountAchieved++;
                        _logger.LogDebug("稳定计数: {Count}/{Required}", stableCountAchieved, param.StableCount);

                        // 达到连续稳定次数要求
                        if (stableCountAchieved >= param.StableCount)
                        {
                            _logger.LogInformation("变量已稳定: {Variable} = {Value}", param.MonitorVariable, currentValue);

                            // 稳定后赋值
                            if (!string.IsNullOrWhiteSpace(param.AssignToVariable))
                            {
                                var targetVariable = _globalVariableManager.GetAllVariables()
                                    .FirstOrDefault(v => v.VarName == param.AssignToVariable);

                                if (targetVariable != null)
                                {
                                    targetVariable.VarValue = currentValue;
                                    _logger.LogInformation("已将稳定值赋给变量: {Target} = {Value}",
                                        param.AssignToVariable, currentValue);
                                }
                                else
                                {
                                    _logger.LogWarning("目标变量不存在: {Variable}", param.AssignToVariable);
                                }
                            }

                            return WaitForStableResult.Success(
                                $"变量 {param.MonitorVariable} 已稳定,当前值: {currentValue:F2}",
                                currentValue);
                        }
                    }
                    else
                    {
                        // 未稳定,重置计数
                        if (stableCountAchieved > 0)
                        {
                            _logger.LogDebug("变化率超过阈值,重置稳定计数");
                            stableCountAchieved = 0;
                        }
                    }

                    previousValue = currentValue;
                    await Task.Delay(param.SamplingInterval * 1000, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("等待变量稳定被取消");
                return WaitForStableResult.Cancelled();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "等待变量稳定时发生错误");
                return WaitForStableResult.Failed($"执行异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 处理超时情况
        /// </summary>
        private WaitForStableResult HandleTimeout(Parameter_WaitForStable param, double currentValue)
        {
            switch (param.OnTimeout)
            {
                case TimeoutAction.ContinueAndLog:
                    _logger.LogWarning("等待变量稳定超时,继续执行");
                    return WaitForStableResult.Timeout(
                        $"变量 {param.MonitorVariable} 等待超时,继续执行。当前值: {currentValue:F2}",
                        shouldContinue: true);

                case TimeoutAction.StopProcedure:
                    _logger.LogError("等待变量稳定超时,停止流程");
                    return WaitForStableResult.Timeout(
                        $"变量 {param.MonitorVariable} 等待超时,停止流程。当前值: {currentValue:F2}",
                        shouldContinue: false);

                case TimeoutAction.JumpToStep:
                    _logger.LogWarning("等待变量稳定超时,跳转到步骤 {Step}", param.TimeoutJumpToStep);
                    return WaitForStableResult.TimeoutWithJump(
                        $"变量 {param.MonitorVariable} 等待超时,跳转到步骤 {param.TimeoutJumpToStep}。当前值: {currentValue:F2}",
                        param.TimeoutJumpToStep);

                default:
                    return WaitForStableResult.Timeout(
                        $"变量 {param.MonitorVariable} 等待超时。当前值: {currentValue:F2}",
                        shouldContinue: true);
            }
        }
    }

    /// <summary>
    /// 等待变量稳定执行结果
    /// </summary>
    public class WaitForStableResult
    {
        public bool IsSuccess { get; set; }
        public bool IsTimeout { get; set; }
        public bool IsCancelled { get; set; }
        public bool ShouldContinue { get; set; } = true;
        public int? JumpToStep { get; set; }
        public string Message { get; set; }
        public double? StableValue { get; set; }

        public static WaitForStableResult Success(string message, double stableValue)
        {
            return new WaitForStableResult
            {
                IsSuccess = true,
                Message = message,
                StableValue = stableValue
            };
        }

        public static WaitForStableResult Failed(string message)
        {
            return new WaitForStableResult
            {
                IsSuccess = false,
                Message = message,
                ShouldContinue = false
            };
        }

        public static WaitForStableResult Timeout(string message, bool shouldContinue)
        {
            return new WaitForStableResult
            {
                IsTimeout = true,
                Message = message,
                ShouldContinue = shouldContinue
            };
        }

        public static WaitForStableResult TimeoutWithJump(string message, int jumpToStep)
        {
            return new WaitForStableResult
            {
                IsTimeout = true,
                Message = message,
                JumpToStep = jumpToStep,
                ShouldContinue = true
            };
        }

        public static WaitForStableResult Cancelled()
        {
            return new WaitForStableResult
            {
                IsCancelled = true,
                Message = "操作已取消",
                ShouldContinue = false
            };
        }
    }
}
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 等待变量稳定方法类
    /// 提供监测变量变化并判断稳定性的功能
    /// 支持监测全局变量和 PLC 点位
    /// </summary>
    public class WaitForStableMethods
    {
        private readonly GlobalVariableManager _globalVariableManager;
        private readonly IPLCManager _plcManager;
        private readonly ILogger<WaitForStableMethods> _logger;

        public WaitForStableMethods(
            GlobalVariableManager globalVariableManager,
            IPLCManager plcManager,
            ILogger<WaitForStableMethods> logger)
        {
            _globalVariableManager = globalVariableManager ?? throw new ArgumentNullException(nameof(globalVariableManager));
            _plcManager = plcManager ?? throw new ArgumentNullException(nameof(plcManager));
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
                // 根据监测源类型记录日志
                string monitorSource = param.MonitorSourceType == MonitorSourceType.Variable
                    ? $"变量: {param.MonitorVariable}"
                    : $"PLC: {param.PlcModuleName}.{param.PlcAddress}";

                _logger.LogInformation("开始等待稳定 - 监测源: {Source}, 阈值: {Threshold}, 间隔: {Interval}秒",
                    monitorSource, param.StabilityThreshold, param.SamplingInterval);

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
                            _logger.LogWarning("等待稳定超时: {Elapsed}秒", elapsed);
                            return HandleTimeout(param, currentValue, monitorSource);
                        }
                    }

                    // 获取当前监测值
                    try
                    {
                        currentValue = await GetCurrentMonitorValue(param, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "获取监测值失败: {Source}", monitorSource);
                        return WaitForStableResult.Failed($"获取监测值失败: {ex.Message}");
                    }

                    // 第一次采样,记录初始值
                    if (previousValue == null)
                    {
                        previousValue = currentValue;
                        _logger.LogDebug("首次采样: {Source} = {Value}", monitorSource, currentValue);
                        await Task.Delay(param.SamplingInterval * 1000, cancellationToken);
                        continue;
                    }

                    // 计算变化率(绝对值)
                    double changeRate = Math.Abs(currentValue - previousValue.Value) / param.SamplingInterval;

                    _logger.LogDebug("采样: {Source} = {Value}, 变化率 = {Rate:F4}",
                        monitorSource, currentValue, changeRate);

                    // 判断是否稳定
                    if (changeRate <= param.StabilityThreshold)
                    {
                        stableCountAchieved++;
                        _logger.LogDebug("稳定计数: {Count}/{Required}", stableCountAchieved, param.StableCount);

                        // 达到连续稳定次数要求
                        if (stableCountAchieved >= param.StableCount)
                        {
                            _logger.LogInformation("监测源已稳定: {Source} = {Value}", monitorSource, currentValue);

                            // 稳定后赋值
                            if (!string.IsNullOrWhiteSpace(param.AssignToVariable))
                            {
                                await AssignStableValueToVariable(param.AssignToVariable, currentValue);
                            }

                            return WaitForStableResult.Success(
                                $"{monitorSource} 已稳定,当前值: {currentValue:F2}",
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
        /// 获取当前监测值
        /// </summary>
        /// <param name="param">参数配置</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>当前数值</returns>
        private async Task<double> GetCurrentMonitorValue(
            Parameter_WaitForStable param,
            CancellationToken cancellationToken)
        {
            if (param.MonitorSourceType == MonitorSourceType.Variable)
            {
                // 从全局变量获取值
                var variable = _globalVariableManager.GetAllVariables()
                    .FirstOrDefault(v => v.VarName == param.MonitorVariable);

                if (variable == null)
                {
                    throw new InvalidOperationException($"监测变量不存在: {param.MonitorVariable}");
                }

                // 尝试转换为数值
                if (!double.TryParse(variable.VarValue?.ToString(), out double value))
                {
                    throw new InvalidOperationException(
                        $"变量值无法转换为数值: {param.MonitorVariable} = {variable.VarValue}");
                }

                return value;
            }
            else // MonitorSourceType.PLC
            {
                // 验证 PLC 配置
                if (string.IsNullOrWhiteSpace(param.PlcModuleName))
                {
                    throw new InvalidOperationException("PLC 模块名不能为空");
                }

                if (string.IsNullOrWhiteSpace(param.PlcAddress))
                {
                    throw new InvalidOperationException("PLC 地址不能为空");
                }

                // 从 PLC 读取值
                var plcValue = await _plcManager.ReadPLCValueAsync(
                    param.PlcModuleName,
                    param.PlcAddress,
                    cancellationToken);

                if (plcValue == null)
                {
                    throw new InvalidOperationException(
                        $"无法从 PLC 读取值: {param.PlcModuleName}.{param.PlcAddress}");
                }

                // 尝试转换为数值
                if (!double.TryParse(plcValue.ToString(), out double value))
                {
                    throw new InvalidOperationException(
                        $"PLC 值无法转换为数值: {param.PlcModuleName}.{param.PlcAddress} = {plcValue}");
                }

                return value;
            }
        }

        /// <summary>
        /// 将稳定值赋给目标变量
        /// </summary>
        /// <param name="targetVarName">目标变量名</param>
        /// <param name="stableValue">稳定值</param>
        private async Task AssignStableValueToVariable(string targetVarName, double stableValue)
        {
            try
            {
                var targetVariable = _globalVariableManager.GetAllVariables()
                    .FirstOrDefault(v => v.VarName == targetVarName);

                if (targetVariable != null)
                {
                    targetVariable.UpdateValue(stableValue, "等待稳定后赋值");
                    _logger.LogInformation("已将稳定值赋给变量: {Target} = {Value}",
                        targetVarName, stableValue);
                }
                else
                {
                    _logger.LogWarning("目标变量不存在: {Variable}", targetVarName);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "赋值稳定值到变量失败: {Variable}", targetVarName);
            }
        }

        /// <summary>
        /// 处理超时情况
        /// </summary>
        private WaitForStableResult HandleTimeout(
            Parameter_WaitForStable param,
            double currentValue,
            string monitorSource)
        {
            switch (param.OnTimeout)
            {
                case TimeoutAction.ContinueAndLog:
                    _logger.LogWarning("等待稳定超时,继续执行");
                    return WaitForStableResult.Timeout(
                        $"{monitorSource} 等待超时,继续执行。当前值: {currentValue:F2}",
                        shouldContinue: true);

                case TimeoutAction.StopProcedure:
                    _logger.LogError("等待稳定超时,停止流程");
                    return WaitForStableResult.Timeout(
                        $"{monitorSource} 等待超时,停止流程。当前值: {currentValue:F2}",
                        shouldContinue: false);

                case TimeoutAction.JumpToStep:
                    _logger.LogWarning("等待稳定超时,跳转到步骤 {Step}", param.TimeoutJumpToStep);
                    return WaitForStableResult.TimeoutWithJump(
                        $"{monitorSource} 等待超时,跳转到步骤 {param.TimeoutJumpToStep}。当前值: {currentValue:F2}",
                        param.TimeoutJumpToStep);

                default:
                    return WaitForStableResult.Timeout(
                        $"{monitorSource} 等待超时。当前值: {currentValue:F2}",
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
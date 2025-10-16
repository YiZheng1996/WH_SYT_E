using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 等待检测工具方法集合
    /// 默认等待直到条件为真，支持超时控制防止程序卡死
    /// </summary>
    public class DetectionMethods(
        IWorkflowStateService workflowState,
        ILogger<DetectionMethods> logger,
        GlobalVariableManager variableManager,
        IPLCManager plcManager) : DSLMethodBase()
    {
        #region 私有字段
        private readonly IWorkflowStateService _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
        private readonly ILogger<DetectionMethods> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly GlobalVariableManager _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
        private readonly IPLCManager _plcManager = plcManager ?? throw new ArgumentNullException(nameof(plcManager));

        // 默认检测间隔100ms
        private const int DEFAULT_CHECK_INTERVAL_MS = 100;
        #endregion

        #region 基类属性实现
        public override string Category => "等待检测工具";
        public override string Description => "等待直到检测条件满足，支持超时控制";
        #endregion

        #region 主要检测方法

        /// <summary>
        /// 等待检测方法 - 等待直到条件为真
        /// </summary>
        /// <param name="param">检测参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>检测是否成功</returns>
        public async Task<bool> Detection(Parameter_Detection param, CancellationToken cancellationToken = default)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                // 参数验证
                ValidateParameter(param);

                var result = new DetectionResult
                {
                    DetectionName = param.DetectionName,
                    StartTime = DateTime.Now
                };

                _logger.LogInformation("开始等待检测: {DetectionName}, 超时时间: {Timeout}ms, 刷新频率: {RefreshRate}ms",
                param.DetectionName, param.TimeoutMs, param.RefreshRateMs);

                try
                {
                    // 执行等待检测
                    bool success = await ExecuteWaitUntilTrue(param, result, cancellationToken);

                    result.IsSuccess = success;
                    result.EndTime = DateTime.Now;

                    // 处理检测结果
                    await ProcessDetectionResult(result, param);

                    _logger.LogInformation("等待检测完成: {DetectionName}, 结果: {Result}, 总耗时: {Duration}ms, 检测次数: {Attempts}, 平均间隔: {AvgInterval}ms",
                   param.DetectionName, success, result.Duration.TotalMilliseconds, result.DetectionAttempts,
                   result.DetectionAttempts > 0 ? result.Duration.TotalMilliseconds / result.DetectionAttempts : 0);

                    return success;
                }
                catch (OperationCanceledException)
                {
                    result.IsSuccess = false;
                    result.EndTime = DateTime.Now;
                    result.ErrorMessage = "检测被取消";
                    _logger.LogWarning("等待检测被取消: {DetectionName}", param.DetectionName);
                    throw;
                }
                catch (TimeoutException)
                {
                    result.IsSuccess = false;
                    result.EndTime = DateTime.Now;
                    result.ErrorMessage = "检测超时";
                    _logger.LogWarning("等待检测超时: {DetectionName}, 超时时间: {Timeout}ms, 检测次数: {Attempts}, 刷新频率: {RefreshRate}ms",
                        param.DetectionName, param.TimeoutMs, result.DetectionAttempts, param.RefreshRateMs);

                    // 处理超时结果
                    await ProcessDetectionResult(result, param);
                    return false;
                }
            }, false, Category);
        }

        #endregion

        #region 等待检测核心实现

        /// <summary>
        /// 等待直到条件为真
        /// </summary>
        private async Task<bool> ExecuteWaitUntilTrue(Parameter_Detection param, DetectionResult result, CancellationToken cancellationToken)
        {
            // 设置超时
            using var timeoutCts = new CancellationTokenSource();
            if (param.TimeoutMs > 0)
            {
                // 仅当设置了正超时时间时启用超时取消
                timeoutCts.CancelAfter(param.TimeoutMs);
            }

            // 组合取消令牌，支持外部取消和超时取消
            using var combinedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            var startTime = DateTime.Now; // 项点开始时间
            int attemptCount = 0; // 检测尝试次数
            object lastValue = null; // 最后一次检测值
            int refreshRate = param.RefreshRateMs; // 使用配置的刷新频率

            _logger.LogDebug("开始等待循环，刷新频率: {RefreshRate}ms", refreshRate);
            try
            {
                // 循环检测直到条件满足或取消
                while (!combinedCts.Token.IsCancellationRequested)
                {
                    attemptCount++;

                    // 执行单次检测
                    var (success, value) = await PerformSingleDetection(param, combinedCts.Token);
                    lastValue = value;

                    _logger.LogDebug("等待检测尝试 #{Attempt}: 结果={Result}, 值={Value}",
                        attemptCount, success, value);

                    // 如果条件满足，返回成功
                    if (success)
                    {
                        result.DetectionAttempts = attemptCount;
                        result.DetectedValue = lastValue;

                        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
                        _logger.LogInformation("等待检测成功: 条件在第{Attempt}次尝试时满足, 耗时: {Duration}ms, 刷新频率: {RefreshRate}ms",
                         attemptCount, elapsed, refreshRate);
                        return true;
                    }

                    // 重试机制
                    if (attemptCount <= param.RetryCount)
                    {
                        _logger.LogDebug("条件未满足，{Interval}ms后重试", param.RetryIntervalMs);
                        await Task.Delay(param.RetryIntervalMs, combinedCts.Token);
                    }
                    else
                    {
                        // 使用默认检测间隔继续等待
                        await Task.Delay(refreshRate, combinedCts.Token);
                    }
                }
            }
            catch (OperationCanceledException) when (timeoutCts.Token.IsCancellationRequested)
            {
                result.DetectionAttempts = attemptCount;
                result.DetectedValue = lastValue;

                _logger.LogWarning("等待检测超时: {Timeout}ms, 尝试次数: {Attempts}, 最后检测值: {LastValue}",
                    param.TimeoutMs, attemptCount, lastValue);
                throw new TimeoutException($"等待检测超时: {param.TimeoutMs}ms");
            }

            result.DetectionAttempts = attemptCount;
            result.DetectedValue = lastValue;
            return false;
        }

        /// <summary>
        /// 执行单次检测
        /// </summary>
        private async Task<(bool success, object value)> PerformSingleDetection(Parameter_Detection param, CancellationToken cancellationToken)
        {
            try
            {
                // 获取检测数据
                object detectionValue = await GetDetectionValue(param.DataSource, cancellationToken);

                // 执行检测判断
                bool success = await EvaluateDetection(detectionValue, param);

                return (success, detectionValue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "单次检测执行失败");
                throw;
            }
        }

        #endregion

        #region 数据获取方法

        /// <summary>
        /// 获取检测数据值
        /// </summary>
        private async Task<object> GetDetectionValue(DataSourceConfig dataSource, CancellationToken cancellationToken = default)
        {
            return dataSource.SourceType switch
            {
                DataSourceType.Variable => await GetVariableValue(dataSource.VariableName),
                DataSourceType.PLC => await GetPlcValue(dataSource.PlcConfig, cancellationToken),
                _ => throw new NotSupportedException($"不支持的数据源类型: {dataSource.SourceType}"),
            };
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        private async Task<object> GetVariableValue(string variableName)
        {
            if (string.IsNullOrEmpty(variableName))
                throw new ArgumentException("变量名不能为空", nameof(variableName));

            var variables = _variableManager.GetAllVariables();
            var variable = variables.FirstOrDefault(v => v.VarName.Equals(variableName, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentException($"变量 '{variableName}' 不存在");
            await Task.CompletedTask;

            _logger.LogDebug("获取变量值: {VariableName} = {Value}", variableName, variable.VarValue);
            return variable.VarValue;
        }

        /// <summary>
        /// 获取PLC值
        /// </summary>
        private async Task<object> GetPlcValue(PlcAddressConfig plcConfig, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(plcConfig);

            if (string.IsNullOrEmpty(plcConfig.ModuleName))
                throw new ArgumentException("PLC模块名不能为空");

            if (string.IsNullOrEmpty(plcConfig.Address))
                throw new ArgumentException("PLC地址不能为空");

            _logger.LogDebug("读取PLC值: {ModuleName}.{Address}", plcConfig.ModuleName, plcConfig.Address);

            try
            {
                var result = await _plcManager.ReadPLCForDetectionAsync(plcConfig, cancellationToken);
                _logger.LogDebug("PLC读取成功: {ModuleName}.{Address} = {Value}",
                    plcConfig.ModuleName, plcConfig.Address, result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PLC读取失败: {ModuleName}.{Address}",
                    plcConfig.ModuleName, plcConfig.Address);
                throw new InvalidOperationException($"PLC读取失败: {plcConfig.ModuleName}.{plcConfig.Address} - {ex.Message}", ex);
            }
        }

        #endregion

        #region 检测判断方法

        /// <summary>
        /// 执行检测判断 - 支持BOOL类型参与条件计算
        /// </summary>
        private async Task<bool> EvaluateDetection(object value, Parameter_Detection param)
        {
            try
            {
                _logger.LogDebug("执行检测判断: 类型={DetectionType}, 值={Value}", param.Type, value);

                var result = param.Type switch
                {
                    DetectionType.ValueRange => await EvaluateRangeDetection(value, param.Condition),
                    DetectionType.Equality => await EvaluateEqualityDetection(value, param.Condition),
                    DetectionType.Status => await EvaluateStatusDetection(value, param.Condition),
                    _ => throw new NotSupportedException($"不支持的检测类型: {param.Type}"),
                };

                _logger.LogDebug("检测判断完成: {Result}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "检测判断失败");
                return false;
            }
        }

        /// <summary>
        /// 范围检测
        /// </summary>
        private async Task<bool> EvaluateRangeDetection(object value, DetectionCondition condition)
        {
            if (!TryConvertToNumericValue(value, out double numValue))
            {
                _logger.LogWarning("范围检测: 无法将值 '{Value}' 转换为数值", value);
                return false;
            }

            await Task.CompletedTask;

            bool result = numValue >= condition.MinValue && numValue <= condition.MaxValue;

            _logger.LogDebug("范围检测: {Value}({NumValue}) 在 [{MinValue}, {MaxValue}] 范围内: {Result}",
                value, numValue, condition.MinValue, condition.MaxValue, result);

            return result;
        }

        /// <summary>
        /// 相等性检测
        /// </summary>
        private async Task<bool> EvaluateEqualityDetection(object value, DetectionCondition condition)
        {
            await Task.CompletedTask;

            // 数值比较（包括BOOL转数值）
            if (TryConvertToNumericValue(value, out double numValue) &&
                TryConvertToNumericValue(condition.TargetValue, out double targetNum))
            {
                bool result = Math.Abs(numValue - targetNum) <= condition.Tolerance;
                _logger.LogDebug("数值相等性检测: |{Value}({NumValue}) - {Target}({TargetNum})| <= {Tolerance}: {Result}",
                    value, numValue, condition.TargetValue, targetNum, condition.Tolerance, result);
                return result;
            }

            // 字符串比较
            var stringResult = string.Equals(value?.ToString(), condition.TargetValue, StringComparison.OrdinalIgnoreCase);
            _logger.LogDebug("字符串相等性检测: '{Value}' == '{Target}': {Result}",
                value, condition.TargetValue, stringResult);

            return stringResult;
        }

        /// <summary>
        /// 状态检测/阈值检测 - 支持BOOL类型参与条件计算
        /// </summary>
        private async Task<bool> EvaluateStatusDetection(object value, DetectionCondition condition)
        {
            await Task.CompletedTask;

            // 统一转换为数值进行条件计算（包括BOOL类型）
            if (TryConvertToNumericValue(value, out double numValue))
            {
                bool result = condition.Operator switch
                {
                    ComparisonOperator.GreaterThan => numValue > condition.ThresholdValue,
                    ComparisonOperator.GreaterThanOrEqual => numValue >= condition.ThresholdValue,
                    ComparisonOperator.LessThan => numValue < condition.ThresholdValue,
                    ComparisonOperator.LessThanOrEqual => numValue <= condition.ThresholdValue,
                    ComparisonOperator.Equal => Math.Abs(numValue - condition.ThresholdValue) <= condition.Tolerance,
                    ComparisonOperator.NotEqual => Math.Abs(numValue - condition.ThresholdValue) > condition.Tolerance,
                    _ => false
                };

                _logger.LogDebug("数值阈值检测: {Value}({NumValue}) {Operator} {Threshold}: {Result}",
                    value, numValue, condition.Operator, condition.ThresholdValue, result);
                return result;
            }

            // 字符串状态检测
            var stringResult = string.Equals(value?.ToString(), condition.TargetValue, StringComparison.OrdinalIgnoreCase);
            _logger.LogDebug("字符串状态检测: '{Value}' == '{Target}': {Result}",
                value, condition.TargetValue, stringResult);

            return stringResult;
        }

        #endregion

        #region 结果处理方法

        /// <summary>
        /// 处理检测结果
        /// </summary>
        private async Task ProcessDetectionResult(DetectionResult result, Parameter_Detection param)
        {
            try
            {
                _logger.LogInformation("处理等待检测结果: {DetectionName}, 成功: {IsSuccess}, 刷新频率: {RefreshRate}ms",
               result.DetectionName, result.IsSuccess, param.RefreshRateMs);

                // 保存检测结果到变量
                if (param.ResultHandling.SaveToVariable && !string.IsNullOrEmpty(param.ResultHandling.ResultVariableName))
                {
                    await SaveOrUpdateVariable(param.ResultHandling.ResultVariableName, result.IsSuccess);
                }

                // 保存检测值到变量
                if (param.ResultHandling.SaveValueToVariable && !string.IsNullOrEmpty(param.ResultHandling.ValueVariableName))
                {
                    await SaveOrUpdateVariable(param.ResultHandling.ValueVariableName, result.DetectedValue);
                }

                // 显示检测结果
                if (param.ResultHandling.ShowResult)
                {
                    string message = FormatResultMessage(param.ResultHandling.MessageTemplate, result, param);
                    _logger.LogInformation("等待检测结果消息: {Message}", message);
                }

                // 处理失败情况
                if (!result.IsSuccess)
                {
                    await HandleDetectionFailure(param.ResultHandling, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理检测结果失败");
            }
        }

        /// <summary>
        /// 格式化结果消息
        /// </summary>
        private static string FormatResultMessage(string template, DetectionResult result, Parameter_Detection param)
        {
            if (string.IsNullOrEmpty(template))
                template = "等待检测 {DetectionName}: {Result}";

            return template
                .Replace("{DetectionName}", result.DetectionName ?? "未知")
                .Replace("{Result}", result.IsSuccess ? "成功" : "失败")
                .Replace("{Value}", result.DetectedValue?.ToString() ?? "无")
                .Replace("{Duration}", $"{result.Duration.TotalMilliseconds:F0}ms")
                .Replace("{Attempts}", result.DetectionAttempts.ToString())
                .Replace("{RefreshRate}", param.RefreshRateMs.ToString())
                .Replace("{AvgInterval}", result.DetectionAttempts > 0 ?
                    $"{result.Duration.TotalMilliseconds / result.DetectionAttempts:F1}ms" : "0ms")
                .Replace("{StartTime}", result.StartTime.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{EndTime}", result.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 处理检测失败
        /// </summary>
        private async Task HandleDetectionFailure(ResultHandling resultHandling, DetectionResult result)
        {
            _logger.LogWarning("等待检测失败处理: {FailureAction}", resultHandling.OnFailure);

            switch (resultHandling.OnFailure)
            {
                case FailureAction.Continue:
                    _logger.LogInformation("等待检测失败但继续执行流程");
                    break;

                case FailureAction.Stop:
                    _logger.LogError("等待检测失败，停止流程执行");
                    throw new InvalidOperationException($"等待检测失败，流程已停止: {result.DetectionName}");

                case FailureAction.Jump:
                    _logger.LogInformation("等待检测失败，准备跳转到步骤: {StepIndex}", resultHandling.FailureStepIndex);
                    break;

                case FailureAction.Confirm:
                    _logger.LogInformation("等待检测失败，需要用户确认");
                    break;
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 保存或更新变量
        /// </summary>
        private async Task SaveOrUpdateVariable(string variableName, object value)
        {
            if (string.IsNullOrEmpty(variableName))
                return;

            try
            {
                var variables = _variableManager.GetAllVariables();
                var existingVar = variables.FirstOrDefault(v => v.VarName.Equals(variableName, StringComparison.OrdinalIgnoreCase));

                if (existingVar != null)
                {
                    existingVar.VarValue = value?.ToString() ?? "";
                    _logger.LogDebug("更新变量: {VariableName} = {Value}", variableName, value);
                }
                else
                {
                    var newVar = new VarItem_Enhanced
                    {
                        VarName = variableName,
                        VarValue = value?.ToString() ?? "",
                        VarType = value?.GetType().Name ?? "String",
                    };

                    _variableManager.AddOrUpdateVariable(newVar);
                    _logger.LogDebug("创建新变量: {VariableName} = {Value}", variableName, value);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存变量失败: {VariableName}", variableName);
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 验证参数
        /// </summary>
        private void ValidateParameter(Parameter_Detection param)
        {
            ArgumentNullException.ThrowIfNull(param);

            if (string.IsNullOrEmpty(param.DetectionName))
                throw new ArgumentException("检测名称不能为空");

            if (param.TimeoutMs <= 0)
            {
                _logger.LogWarning("超时时间设置为{Timeout}ms，将使用无限等待模式", param.TimeoutMs);
            }

            // 合理性检查：刷新频率不应该太接近超时时间
            if (param.TimeoutMs > 0 && param.RefreshRateMs > param.TimeoutMs / 2)
            {
                _logger.LogWarning("刷新频率({RefreshRate}ms)相对于超时时间({Timeout}ms)较长，可能影响检测精度",
                    param.RefreshRateMs, param.TimeoutMs);
            }

            if (param.DataSource == null)
                throw new ArgumentException("数据源配置不能为空");

            if (param.Condition == null)
                throw new ArgumentException("检测条件不能为空");

            if (param.ResultHandling == null)
                throw new ArgumentException("结果处理配置不能为空");
        }

        /// <summary>
        /// 尝试转换为double - 支持BOOL类型
        /// </summary>
        private static bool TryConvertToNumericValue(object value, out double result)
        {
            result = 0;

            if (value == null)
                return false;

            // 直接的数值类型
            if (value is double d) { result = d; return true; }
            if (value is float f) { result = f; return true; }
            if (value is int i) { result = i; return true; }
            if (value is decimal dec) { result = (double)dec; return true; }
            if (value is long l) { result = l; return true; }
            if (value is short s) { result = s; return true; }

            // 布尔类型转换 - 关键支持
            if (value is bool boolValue)
            {
                result = boolValue ? 1.0 : 0.0;
                return true;
            }

            // 字符串转换
            if (value is string str)
            {
                if (double.TryParse(str, out result))
                    return true;

                if (TryParseBooleanString(str, out bool boolFromString))
                {
                    result = boolFromString ? 1.0 : 0.0;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 解析布尔字符串
        /// </summary>
        private static bool TryParseBooleanString(string str, out bool result)
        {
            result = false;

            if (string.IsNullOrEmpty(str))
                return false;

            if (bool.TryParse(str, out result))
                return true;

            str = str.ToLowerInvariant().Trim();
            switch (str)
            {
                case "1":
                case "on":
                case "yes":
                case "enable":
                case "enabled":
                case "active":
                case "high":
                    result = true;
                    return true;
                case "0":
                case "off":
                case "no":
                case "disable":
                case "disabled":
                case "inactive":
                case "low":
                    result = false;
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }

    #region 扩展的检测结果类

    /// <summary>
    /// 扩展的检测结果 - 包含等待相关信息
    /// </summary>
    public class DetectionResult
    {
        public string DetectionName { get; set; } = "";
        public bool IsSuccess { get; set; }
        public object DetectedValue { get; set; }
        public string ErrorMessage { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;

        // 等待相关属性
        public int DetectionAttempts { get; set; } = 0;
    }

    #endregion
}
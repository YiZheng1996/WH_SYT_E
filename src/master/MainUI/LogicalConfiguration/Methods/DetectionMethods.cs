using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 检测方法类 - 表达式化版本
    /// 使用表达式引擎执行检测判断
    /// </summary>
    public class DetectionMethods : DSLMethodBase
    {
        #region 私有字段

        private readonly IWorkflowStateService _workflowState;
        private readonly ILogger<DetectionMethods> _logger;
        private readonly GlobalVariableManager _variableManager;
        private readonly IPLCManager _plcManager;
        private readonly ExpressionEngine _expressionEngine;

        // 默认检测间隔100ms
        private const int DEFAULT_CHECK_INTERVAL_MS = 100;

        #endregion

        #region 构造函数

        public DetectionMethods(
            IWorkflowStateService workflowState,
            ILogger<DetectionMethods> logger,
            GlobalVariableManager variableManager,
            IPLCManager plcManager,
            ExpressionEngine expressionEngine = null)
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _plcManager = plcManager ?? throw new ArgumentNullException(nameof(plcManager));

            // 如果没有提供表达式引擎，创建一个新的
            _expressionEngine = expressionEngine ?? new ExpressionEngine(variableManager, plcManager, null);
        }

        #endregion

        #region 基类属性实现

        public override string Category => "等待检测工具";
        public override string Description => "等待直到检测条件满足，支持表达式条件和超时控制";

        #endregion

        #region 主要检测方法

        /// <summary>
        /// 等待检测方法 - 等待直到条件表达式为真
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

                _logger.LogInformation(
                    "开始等待检测: {DetectionName}, 表达式: {Expression}, 超时: {Timeout}ms, 刷新频率: {RefreshRate}ms",
                    param.DetectionName, param.ConditionExpression, param.TimeoutMs, param.RefreshRateMs);

                try
                {
                    // 执行等待检测
                    bool success = await ExecuteWaitUntilTrue(param, result, cancellationToken);

                    result.IsSuccess = success;
                    result.EndTime = DateTime.Now;

                    // 处理检测结果
                    await ProcessDetectionResult(result, param);

                    _logger.LogInformation(
                        "等待检测完成: {DetectionName}, 结果: {Result}, 总耗时: {Duration}ms, 检测次数: {Attempts}",
                        param.DetectionName, success, result.Duration.TotalMilliseconds, result.DetectionAttempts);

                    return success;
                }
                catch (OperationCanceledException)
                {
                    _logger.LogWarning("检测被取消: {DetectionName}", param.DetectionName);
                    result.IsSuccess = false;
                    result.EndTime = DateTime.Now;
                    result.ErrorMessage = "检测被取消";
                    await ProcessDetectionResult(result, param);
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "检测执行失败: {DetectionName}", param.DetectionName);
                    result.IsSuccess = false;
                    result.EndTime = DateTime.Now;
                    result.ErrorMessage = ex.Message;
                    await ProcessDetectionResult(result, param);
                    throw;
                }
            });
        }

        #endregion

        #region 核心检测循环

        /// <summary>
        /// 执行等待检测循环
        /// </summary>
        private async Task<bool> ExecuteWaitUntilTrue(
            Parameter_Detection param,
            DetectionResult result,
            CancellationToken cancellationToken)
        {
            var checkInterval = param.RefreshRateMs > 0 ? param.RefreshRateMs : DEFAULT_CHECK_INTERVAL_MS;
            var timeout = param.TimeoutMs > 0 ? TimeSpan.FromMilliseconds(param.TimeoutMs) : TimeSpan.MaxValue;
            var startTime = DateTime.Now;
            var retryCount = 0;
            var maxRetries = param.RetryCount;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // 检查超时
                var elapsed = DateTime.Now - startTime;
                if (elapsed >= timeout)
                {
                    _logger.LogWarning("检测超时: {DetectionName}, 已等待 {Elapsed}ms",
                        param.DetectionName, elapsed.TotalMilliseconds);
                    result.TimeoutOccurred = true;
                    return false;
                }

                result.DetectionAttempts++;

                try
                {
                    // 直接使用表达式引擎计算
                    // 表达式引擎会自动解析并读取其中的变量和PLC地址
                    var evalResult = await _expressionEngine.EvaluateExpressionAsync(
                        param.ConditionExpression);

                    if (evalResult.Result is bool conditionMet && conditionMet)
                    {
                        _logger.LogDebug("检测条件满足: {DetectionName}", param.DetectionName);
                        result.FinalValue = evalResult.Result;
                        return true;
                    }

                    // 记录当前表达式计算值用于调试
                    result.LastReadValue = evalResult.Result;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "检测读取失败，尝试次数: {Attempts}/{MaxRetries}",
                        retryCount + 1, maxRetries);

                    retryCount++;
                    if (retryCount > maxRetries)
                    {
                        _logger.LogError("检测重试次数已用尽: {DetectionName}", param.DetectionName);
                        result.ErrorMessage = $"检测失败，重试次数已用尽: {ex.Message}";
                        return false;
                    }

                    // 等待重试间隔
                    await Task.Delay(param.RetryIntervalMs, cancellationToken);
                    continue;
                }

                // 等待下一次检测
                var remainingTime = timeout - elapsed;
                var waitTime = Math.Min(checkInterval, (int)remainingTime.TotalMilliseconds);

                if (waitTime > 0)
                {
                    await Task.Delay(waitTime, cancellationToken);
                }
            }
        }

        #endregion

        #region 表达式计算

        /// <summary>
        /// 计算条件表达式
        /// </summary>
        private bool EvaluateConditionExpression(object value, string expression)
        {
            try
            {
                // 将值转换为字符串
                string valueStr = ConvertValueToString(value);

                // 替换{value}占位符
                string evaluateExpression = expression.Replace("{value}", valueStr);

                // 使用表达式引擎计算
                var result = _expressionEngine.EvaluateExpression(evaluateExpression);

                // 转换为布尔值
                return ConvertToBoolean(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "表达式计算失败: {Expression}, Value: {Value}",
                    expression, value);
                throw;
            }
        }

        /// <summary>
        /// 将值转换为字符串（用于表达式替换）
        /// </summary>
        private string ConvertValueToString(object value)
        {
            if (value == null) return "null";

            // 布尔值
            if (value is bool boolVal)
            {
                return boolVal ? "true" : "false";
            }

            // 数值类型
            if (value is IConvertible convertible)
            {
                try
                {
                    double numVal = Convert.ToDouble(value);
                    return numVal.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    // 非数值，继续处理
                }
            }

            // 字符串需要加引号
            if (value is string strVal)
            {
                return $"\"{strVal}\"";
            }

            return value.ToString();
        }

        /// <summary>
        /// 将表达式结果转换为布尔值
        /// </summary>
        private bool ConvertToBoolean(object result)
        {
            if (result == null) return false;

            if (result is bool boolResult)
            {
                return boolResult;
            }

            if (result is string strResult)
            {
                return strResult.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                       strResult.Equals("1", StringComparison.OrdinalIgnoreCase);
            }

            try
            {
                return Convert.ToBoolean(result);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 结果处理

        /// <summary>
        /// 处理检测结果
        /// </summary>
        private async Task ProcessDetectionResult(DetectionResult result, Parameter_Detection param)
        {
            try
            {
                var handling = param.ResultHandling ?? new ResultHandling();

                // 保存结果到变量
                if (handling.SaveToVariable && !string.IsNullOrEmpty(handling.ResultVariableName))
                {
                    await SaveToVariable(handling.ResultVariableName, result.IsSuccess);
                    _logger.LogDebug("保存结果到变量: {VarName} = {Value}",
                        handling.ResultVariableName, result.IsSuccess);
                }

                // 保存检测值到变量
                if (handling.SaveValueToVariable && !string.IsNullOrEmpty(handling.ValueVariableName))
                {
                    var valueToSave = result.FinalValue ?? result.LastReadValue;
                    await SaveToVariable(handling.ValueVariableName, valueToSave);
                    _logger.LogDebug("保存值到变量: {VarName} = {Value}",
                        handling.ValueVariableName, valueToSave);
                }

                // 显示结果消息
                if (handling.ShowResult)
                {
                    string message = FormatResultMessage(handling.MessageTemplate, param, result);
                    _logger.LogInformation("检测结果: {Message}", message);
                }

                // 处理失败情况
                if (!result.IsSuccess)
                {
                    await HandleFailure(param, result, handling);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理检测结果失败");
            }
        }

        /// <summary>
        /// 保存值到变量
        /// </summary>
        private Task SaveToVariable(string variableName, object value)
        {
            try
            {
                var variable = _variableManager.TryFindVariableByName(variableName);
                if (variable != null)
                {
                    variable.VarValue = value;
                    _logger.LogDebug("变量 '{VarName}' 已更新为: {Value}", variableName, value);
                }
                else
                {
                    _logger.LogWarning("变量 '{VarName}' 不存在，无法保存值", variableName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存值到变量失败: {VarName}", variableName);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 格式化结果消息
        /// </summary>
        private string FormatResultMessage(string template, Parameter_Detection param, DetectionResult result)
        {
            if (string.IsNullOrEmpty(template))
            {
                template = "检测项 {DetectionName}: {Result}";
            }

            return template
                .Replace("{DetectionName}", param.DetectionName ?? "")
                .Replace("{Result}", result.IsSuccess ? "通过" : "未通过")
                .Replace("{Value}", result.FinalValue?.ToString() ?? result.LastReadValue?.ToString() ?? "N/A")
                .Replace("{Duration}", result.Duration.TotalMilliseconds.ToString("F0"))
                .Replace("{Attempts}", result.DetectionAttempts.ToString());
        }

        /// <summary>
        /// 处理检测失败
        /// </summary>
        private Task HandleFailure(Parameter_Detection param, DetectionResult result, ResultHandling handling)
        {
            switch (handling.OnFailure)
            {
                case FailureAction.Continue:
                    _logger.LogDebug("检测失败，继续执行");
                    break;

                case FailureAction.Stop:
                    _logger.LogWarning("检测失败，停止流程");
                    // _workflowState?.RequestStop("检测失败");
                    break;

                case FailureAction.JumpToStep:
                    if (handling.FailureJumpStep >= 0)
                    {
                        _logger.LogDebug("检测失败，跳转到步骤: {Step}", handling.FailureJumpStep);
                        // _workflowState?.SetNextStep(handling.FailureJumpStep);
                    }
                    break;

                case FailureAction.Retry:
                    _logger.LogDebug("检测失败，请求重试");
                    // 重试逻辑已在主循环中处理
                    break;
            }

            return Task.CompletedTask;
        }

        #endregion

        #region 参数验证

        /// <summary>
        /// 验证参数
        /// </summary>
        private void ValidateParameter(Parameter_Detection param)
        {
            ArgumentNullException.ThrowIfNull(param);

            // 验证表达式
            if (string.IsNullOrWhiteSpace(param.ConditionExpression))
            {
                throw new InvalidOperationException("条件表达式不能为空");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 带日志记录的执行包装
        /// </summary>
        private async Task<T> ExecuteWithLogging<T>(Parameter_Detection param, Func<Task<T>> action)
        {
            var startTime = DateTime.Now;

            try
            {
                return await action();
            }
            finally
            {
                var duration = DateTime.Now - startTime;
                _logger.LogDebug("检测 '{DetectionName}' 执行完成，耗时: {Duration}ms",
                    param?.DetectionName ?? "未知", duration.TotalMilliseconds);
            }
        }

        #endregion
    }

    #region 检测结果类

    /// <summary>
    /// 检测结果
    /// </summary>
    public class DetectionResult
    {
        /// <summary>
        /// 检测项名称
        /// </summary>
        public string DetectionName { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan Duration => EndTime - StartTime;

        /// <summary>
        /// 检测尝试次数
        /// </summary>
        public int DetectionAttempts { get; set; }

        /// <summary>
        /// 最后读取的值
        /// </summary>
        public object LastReadValue { get; set; }

        /// <summary>
        /// 最终值（条件满足时的值）
        /// </summary>
        public object FinalValue { get; set; }

        /// <summary>
        /// 是否发生超时
        /// </summary>
        public bool TimeoutOccurred { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    #endregion
}
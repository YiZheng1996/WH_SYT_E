using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.Methods;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 步骤执行管理器
    /// </summary>
    /// <remarks>
    /// 构造函数 - 获取所有方法实例
    /// </remarks>
    public class StepExecutionManager(
    List<ChildModel> steps,
    SystemMethods systemMethods,
    VariableMethods variableMethods,
    PLCMethods plcMethods,
    DetectionMethods detectionMethods,
    ReportMethods reportMethods,
    WaitForStableMethods waitForStableMethods,
    RealtimeMonitorPromptMethods realtimeMonitorPromptMethods,
    ConditionMethods conditionMethods,
    LoopMethods loopMethods,
    IWorkflowStateService workflowState,
    ExpressionEngine expressionEngine,
    GlobalVariableManager globalVariableManager)
    {
        #region 字段和属性

        private readonly List<ChildModel> _steps = steps ?? throw new ArgumentNullException(nameof(steps));
        private readonly SystemMethods _systemMethods = systemMethods ?? throw new ArgumentNullException(nameof(systemMethods));
        private readonly VariableMethods _variableMethods = variableMethods ?? throw new ArgumentNullException(nameof(variableMethods));
        private readonly PLCMethods _plcMethods = plcMethods ?? throw new ArgumentNullException(nameof(plcMethods));
        private readonly DetectionMethods _detectionMethods = detectionMethods ?? throw new ArgumentNullException(nameof(detectionMethods));
        private readonly ReportMethods _reportMethods = reportMethods ?? throw new ArgumentNullException(nameof(reportMethods));
        private readonly WaitForStableMethods _waitForStableMethods = waitForStableMethods ?? throw new ArgumentNullException(nameof(waitForStableMethods));
        private readonly RealtimeMonitorPromptMethods _realtimeMonitorPromptMethods =
       realtimeMonitorPromptMethods ?? throw new ArgumentNullException(nameof(realtimeMonitorPromptMethods));
        private readonly ConditionMethods _conditionMethods = conditionMethods;
        private readonly LoopMethods _loopMethods = loopMethods;
        private readonly GlobalVariableManager _globalVariableManager = globalVariableManager
       ?? throw new ArgumentNullException(nameof(globalVariableManager));
        private readonly IWorkflowStateService _workflowStateService = workflowState;
        private readonly ExpressionEngine _expressionEngine = expressionEngine;

        public event Action<ChildModel, int> StepStatusChanged;

        private bool _isExecuting;
        private int _currentStepIndex;

        // 取消令牌源
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// 当前步骤索引
        /// </summary>
        public int CurrentStepIndex => _currentStepIndex;

        /// <summary>
        /// 是否正在执行
        /// </summary>
        public bool IsExecuting => _isExecuting;

        /// <summary>
        /// 总步骤数
        /// </summary>
        public int TotalSteps => _steps.Count;

        #endregion

        #region 主要执行方法

        /// <summary>
        /// 开始执行所有步骤
        /// </summary>
        public async Task StartExecutionAsync()
        {
            _isExecuting = true;
            _currentStepIndex = 0;

            // 创建新的取消令牌源
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                NlogHelper.Default.Info($"开始执行步骤序列,共 {_steps.Count} 个步骤");

                // 初始化测试信息变量
                TestInfoVariableHelper.UpdateTestInfoVariables(_globalVariableManager);

                while (_isExecuting && _currentStepIndex < _steps.Count)
                {
                    // 检查是否已取消
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        NlogHelper.Default.Info("步骤执行已被取消");
                        break;
                    }

                    var step = _steps[_currentStepIndex];

                    // 更新步骤状态为执行中
                    step.Status = 1; // 执行中
                    StepStatusChanged?.Invoke(step, _currentStepIndex);

                    try
                    {
                        NlogHelper.Default.Info($"执行步骤 [{_currentStepIndex + 1}/{_steps.Count}]: {step.StepName}");

                        // 执行步骤,传递取消令牌
                        var result = await ExecuteStepAsync(step, _cancellationTokenSource.Token);

                        if (result.Success)
                        {
                            step.Status = 2; // 成功
                            step.ErrorMessage = null; // 清空错误信息
                            NlogHelper.Default.Info($"步骤执行成功: {step.StepName}");

                            // 检查是否需要跳转
                            if (result.NextStepIndex.HasValue)
                            {
                                _currentStepIndex = result.NextStepIndex.Value;
                                NlogHelper.Default.Info($"条件跳转到步骤: {_currentStepIndex + 1}");
                                continue;
                            }
                        }
                        else
                        {
                            step.Status = 3; // 失败
                            step.ErrorMessage = result.Message; // 记录错误信息
                            NlogHelper.Default.Error($"步骤执行失败: {step.StepName}, 原因: {result.Message}");
                            break;
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // 操作被取消
                        step.Status = 3; // 标记为失败
                        step.ErrorMessage = $"执行异常:操作被取消";
                        NlogHelper.Default.Info($"步骤 {step.StepName} 执行被取消");
                        break;
                    }
                    catch (Exception ex)
                    {
                        step.Status = 3; // 失败
                        step.ErrorMessage = $"执行异常: {ex.Message}"; // ⭐ 记录异常信息
                        NlogHelper.Default.Error($"步骤执行异常: {step.StepName}", ex);
                        break;
                    }
                    finally
                    {
                        StepStatusChanged?.Invoke(step, _currentStepIndex);
                    }

                    _currentStepIndex++;

                    // 步骤间延时(可选),使用取消令牌
                    if (_isExecuting)
                    {
                        try
                        {
                            await Task.Delay(10, _cancellationTokenSource.Token);
                        }
                        catch (OperationCanceledException)
                        {
                            // 延时被取消,直接退出
                            break;
                        }
                    }
                }

                NlogHelper.Default.Info("步骤序列执行完成");
            }
            finally
            {
                _isExecuting = false;
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }

        /// <summary>
        /// 停止执行 - 立即取消正在执行的步骤
        /// </summary>
        public void Stop()
        {
            _isExecuting = false;

            // 请求取消正在执行的操作
            _cancellationTokenSource?.Cancel();

            NlogHelper.Default.Info("步骤执行已请求停止");
        }

        #endregion

        #region 步骤执行核心逻辑

        /// <summary>
        /// 执行单个步骤 - 使用 switch-case 替代策略模式
        /// </summary>
        private async Task<DetailedResult> ExecuteStepAsync(ChildModel step, CancellationToken cancellationToken)
        {
            // 在执行前检查取消
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return step.StepName switch
                {
                    // 系统工具
                    "延时等待" => await ExecuteDelayTime(step, cancellationToken),
                    "消息通知" => await ExecuteSystemPrompt(step, cancellationToken),

                    // 变量管理
                    "变量定义" => await ExecuteDefineVar(step, cancellationToken),
                    "变量赋值" => await ExecuteVariableAssignment(step, cancellationToken),

                    // PLC通信
                    "读取PLC" => await ExecuteReadPLC(step, cancellationToken),
                    "写入PLC" => await ExecuteWritePLC(step, cancellationToken),

                    // 检测工具
                    "条件判断" => await ExecuteDetection(step, cancellationToken),
                    "等待稳定" => await ExecuteWaitForStable(step, cancellationToken),
                    "实时监控" => await ExecuteRealtimeMonitorPrompt(step, cancellationToken),
                    //"检测工具" => await ExecuteCondition(step, cancellationToken),
                    "循环工具" => await ExecuteLoop(step, cancellationToken),

                    // 报表工具
                    "读取单元格" => await ExecuteReadCells(step, cancellationToken),
                    "写入单元格" => await ExecuteWriteCells(step, cancellationToken),

                    // 未知步骤类型
                    _ => DetailedResult.Failed($"不支持的步骤类型: {step.StepName}")
                };
            }
            catch (OperationCanceledException)
            {
                NlogHelper.Default.Info($"步骤 {step.StepName} 被取消");
                throw; // 向上传播取消异常
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"执行步骤失败: {step.StepName}", ex);
                return DetailedResult.Failed($"执行异常: {ex.Message}");
            }
        }

        #endregion

        #region 具体步骤执行方法
        /// <summary>
        /// 延时等待
        /// </summary>
        /// <param name="step">当前步骤信息</param>
        /// <returns></returns>
        private async Task<DetailedResult> ExecuteDelayTime(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_DelayTime>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            // 将取消令牌传递给延时方法
            var result = await _systemMethods.DelayTime(param, cancellationToken);
            return result ? DetailedResult.Successful() : DetailedResult.Failed("延时执行失败");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="step">当前步骤信息</param>
        private async Task<DetailedResult> ExecuteSystemPrompt(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_SystemPrompt>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _systemMethods.SystemPrompt(param);
            return result ? DetailedResult.Successful() : DetailedResult.Failed("提示执行失败");
        }

        /// <summary>
        /// 变量定义
        /// </summary>
        private async Task<DetailedResult> ExecuteDefineVar(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_DefineVar>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            cancellationToken.ThrowIfCancellationRequested();
            var result = await _variableMethods.DefineVar(param);
            return result ? DetailedResult.Successful() : DetailedResult.Failed("变量定义失败");
        }

        /// <summary>
        /// 变量赋值
        /// </summary>
        private async Task<DetailedResult> ExecuteVariableAssignment(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_VariableAssignment>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            cancellationToken.ThrowIfCancellationRequested();
            // 使用详细结果
            var result = await _variableMethods.VariableAssignment(param);

            return result.Success
                ? DetailedResult.Successful()
                : DetailedResult.Failed(result.ErrorMessage);
        }

        /// <summary>
        /// 读取PLC
        /// </summary>
        private async Task<DetailedResult> ExecuteReadPLC(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_ReadPLC>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            cancellationToken.ThrowIfCancellationRequested();
            var result = await _plcMethods.ReadPLC(param, cancellationToken);
            return result ? DetailedResult.Successful() : DetailedResult.Failed(result.ErrorMessage);
        }

        /// <summary>
        /// 写入PLC
        /// </summary>
        private async Task<DetailedResult> ExecuteWritePLC(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_WritePLC>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            cancellationToken.ThrowIfCancellationRequested();
            var result = await _plcMethods.WritePLC(param, cancellationToken);
            return result ? DetailedResult.Successful() : DetailedResult.Failed(result.ErrorMessage);
        }

        /// <summary>
        /// 条件判断 - 支持跳转逻辑
        /// </summary>
        /// <param name="step">当前步骤信息</param>
        private async Task<DetailedResult> ExecuteDetection(ChildModel step, CancellationToken cancellationToken)
        {
            try
            {
                var param = ConvertParameter<Parameter_Detection>(step.StepParameter);
                if (param == null)
                {
                    NlogHelper.Default.Error("检测/条件判断参数转换失败");
                    return DetailedResult.Failed("参数转换失败");
                }
                cancellationToken.ThrowIfCancellationRequested();

                NlogHelper.Default.Info($"执行检测: {param.DetectionName}, 类型: {step.StepName}");

                // 获取当前步骤索引（用于防死循环检查）
                int currentStepIndex = _currentStepIndex;

                // 防死循环检查（针对条件判断）
                if (param.ResultHandling.OnFailure == FailureAction.JumpToStep)
                {
                    int failureStep = param.ResultHandling.FailureJumpStep;
                    int successStep = param.ResultHandling.SuccessJumpStep;

                    if (failureStep == currentStepIndex && successStep == currentStepIndex)
                    {
                        NlogHelper.Default.Error($"配置错误：成功和失败都跳转到当前步骤({currentStepIndex})，会导致死循环！");
                        return DetailedResult.Failed("配置错误：会导致死循环");
                    }

                    // 警告：如果失败跳转到当前步骤
                    if (failureStep == currentStepIndex)
                    {
                        NlogHelper.Default.Warn($"警告：失败时跳转到当前步骤({currentStepIndex})，可能导致死循环");
                    }
                }

                // 执行检测
                var result = await _detectionMethods.Detection(param, cancellationToken);

                if (result)
                {
                    // 检测成功
                    NlogHelper.Default.Info($"检测成功: {param.DetectionName}");

                    // 检查是否需要跳转
                    int successStep = param.ResultHandling.SuccessJumpStep;
                    if (successStep >= 0)
                    {
                        NlogHelper.Default.Info($"成功跳转到步骤: {successStep}");
                        return DetailedResult.Jump(successStep);
                    }

                    return DetailedResult.Successful();
                }
                else
                {
                    // 检测失败
                    NlogHelper.Default.Info($"检测失败: {param.DetectionName}");

                    var failureAction = param.ResultHandling.OnFailure;

                    switch (failureAction)
                    {
                        case FailureAction.Stop:
                            NlogHelper.Default.Error("检测失败：停止流程执行");
                            return DetailedResult.Failed("检测失败，流程已停止");

                        case FailureAction.JumpToStep:
                            int failureStep = param.ResultHandling.FailureJumpStep;
                            NlogHelper.Default.Info($"失败跳转到步骤: {failureStep}");
                            return DetailedResult.Jump(failureStep);

                        case FailureAction.Continue:
                            NlogHelper.Default.Info("检测失败：继续执行下一步");
                            return DetailedResult.Successful();

                        case FailureAction.Retry:
                            NlogHelper.Default.Info("检测失败：需要用户确认");
                            // TODO: 实现用户确认对话框
                            return DetailedResult.Successful();

                        default:
                            NlogHelper.Default.Info("检测失败：默认继续执行");
                            return DetailedResult.Successful();
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"检测/条件判断异常: {ex.Message}", ex);
                return DetailedResult.Failed($"执行异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 保存报表
        /// </summary>
        /// <param name="step">当前步骤信息</param>
        private async Task<DetailedResult> ExecuteSaveReport(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_SaveReport>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _reportMethods.SaveReport(param);
            return result ? DetailedResult.Successful() : DetailedResult.Failed("报表保存失败");
        }

        /// <summary>
        /// 读取报表单元格
        /// </summary>
        private async Task<DetailedResult> ExecuteReadCells(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_ReadCells>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var result = await _reportMethods.ReadCells(param);

                // 判断结果不为 null 即为成功
                if (result != null)
                {
                    // 如果有读取项且需要保存到变量
                    if (param.ReadItems != null && param.ReadItems.Count > 0)
                    {
                        // 单个值的情况
                        if (result is not Dictionary<string, object> && param.ReadItems.Count == 1)
                        {
                            var item = param.ReadItems[0];
                            if (!string.IsNullOrEmpty(item.SaveToVariable))
                            {
                                // 使用正确的方法保存变量值
                                var variable = _globalVariableManager?.FindVariableByName(item.SaveToVariable);
                                if (variable != null)
                                {
                                    variable.UpdateValue(result, $"从单元格 {item.CellAddress} 读取");
                                    NlogHelper.Default.Info($"单元格 {item.CellAddress} 值已保存到变量 {item.SaveToVariable}: {result}");
                                }
                                else
                                {
                                    NlogHelper.Default.Warn($"变量 {item.SaveToVariable} 不存在");
                                }
                            }
                        }
                        // 多个值的情况 (Dictionary)
                        else if (result is Dictionary<string, object> results)
                        {
                            foreach (var item in param.ReadItems)
                            {
                                if (results.TryGetValue(item.CellAddress, out var value) &&
                                    !string.IsNullOrEmpty(item.SaveToVariable))
                                {
                                    var variable = _globalVariableManager?.FindVariableByName(item.SaveToVariable);
                                    if (variable != null)
                                    {
                                        variable.UpdateValue(value, $"从单元格 {item.CellAddress} 读取");
                                        NlogHelper.Default.Info($"单元格 {item.CellAddress} 值已保存到变量 {item.SaveToVariable}: {value}");
                                    }
                                    else
                                    {
                                        NlogHelper.Default.Warn($"变量 {item.SaveToVariable} 不存在");
                                    }
                                }
                            }
                        }
                    }
                    return DetailedResult.Successful($"成功读取 {param.ReadItems?.Count ?? 0} 个单元格");
                }

                return DetailedResult.Failed("单元格读取失败: 返回值为空");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"读取单元格异常: {ex.Message}", ex);
                return DetailedResult.Failed($"读取单元格异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 写报表单元格
        /// </summary>
        /// <param name="step">当前步骤信息</param>
        private async Task<DetailedResult> ExecuteWriteCells(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_WriteCells>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            cancellationToken.ThrowIfCancellationRequested();
            var result = await _reportMethods.WriteCells(param);
            return result ? DetailedResult.Successful() : DetailedResult.Failed(result.ErrorMessage);
        }

        /// <summary>
        /// 等待稳定
        /// </summary>
        /// <param name="step">当前步骤信息</param>
        private async Task<DetailedResult> ExecuteWaitForStable(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_WaitForStable>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            cancellationToken.ThrowIfCancellationRequested();
            var result = await _waitForStableMethods.ExecuteWaitForStable(param, cancellationToken);
            return result.IsSuccess ? DetailedResult.Successful() : DetailedResult.Failed("等待稳定失败");
        }

        /// <summary>
        /// 实时监控提示
        /// </summary>
        private async Task<DetailedResult> ExecuteRealtimeMonitorPrompt(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_RealtimeMonitorPrompt>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            cancellationToken.ThrowIfCancellationRequested();
            var result = await _realtimeMonitorPromptMethods.ShowRealtimeMonitorPrompt(param, cancellationToken);
            return result ? DetailedResult.Successful() : DetailedResult.Failed("用户取消操作");
        }

        /// <summary>
        /// 执行条件判断
        /// </summary>
        private async Task<DetailedResult> ExecuteCondition(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_Condition>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            try
            {
                // 调用 ConditionMethods 进行条件判断
                var result = _conditionMethods.EvaluateCondition(param);

                if (!result.IsSuccess)
                {
                    return DetailedResult.Failed($"条件判断失败: {result.ErrorMessage}");
                }

                if (result.StepsToExecute != null && result.StepsToExecute.Count > 0)
                {
                    NlogHelper.Default.Info($"执行{(result.ConditionMet ? "满足" : "不满足")}条件的步骤，共 {result.StepsToExecute.Count} 个分支");

                    // 执行子步骤
                    foreach (var parentStep in result.StepsToExecute)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        if (parentStep.ChildSteps != null && parentStep.ChildSteps.Count > 0)
                        {
                            NlogHelper.Default.Info($"执行分支 [{parentStep.ItemName}] 中的 {parentStep.ChildSteps.Count} 个步骤");

                            foreach (var childStep in parentStep.ChildSteps)
                            {
                                cancellationToken.ThrowIfCancellationRequested();

                                // 递归调用执行子步骤
                                var childResult = await ExecuteStepAsync(childStep, cancellationToken);

                                if (!childResult.Success)
                                {
                                    NlogHelper.Default.Warn($"条件分支子步骤执行失败: {childStep.StepName}, 原因: {childResult.Message}");
                                    // 根据策略决定是否继续（这里选择继续）
                                }
                            }
                        }
                    }

                    NlogHelper.Default.Info($"{(result.ConditionMet ? "满足" : "不满足")}条件的步骤执行完成");
                }
                else
                {
                    NlogHelper.Default.Info($"{(result.ConditionMet ? "满足" : "不满足")}条件时无需执行子步骤");
                }

                return DetailedResult.Successful();
            }
            catch (OperationCanceledException)
            {
                NlogHelper.Default.Info("条件判断被取消");
                throw;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"条件判断执行异常", ex);
                return DetailedResult.Failed($"条件判断异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 执行循环
        /// </summary>
        private async Task<DetailedResult> ExecuteLoop(ChildModel step, CancellationToken cancellationToken)
        {
            var param = ConvertParameter<Parameter_Loop>(step.StepParameter);
            if (param == null) return DetailedResult.Failed("参数转换失败");

            try
            {
                // 调用 LoopMethods 计算循环参数
                var loopInfo = _loopMethods.EvaluateLoop(param);

                if (!loopInfo.IsSuccess)
                {
                    return DetailedResult.Failed($"循环参数计算失败: {loopInfo.ErrorMessage}");
                }

                if (loopInfo.LoopCount <= 0)
                {
                    NlogHelper.Default.Info("循环次数为 0，跳过循环");
                    return DetailedResult.Successful();
                }

                NlogHelper.Default.Info($"开始循环执行，共 {loopInfo.LoopCount} 次 - {loopInfo.Description}");

                // 检查是否启用提前退出
                bool earlyExitEnabled = param.EnableEarlyExit &&
                                        !string.IsNullOrWhiteSpace(param.ExitConditionExpression);

                if (earlyExitEnabled)
                {
                    NlogHelper.Default.Info($"✓ 已启用提前退出，退出条件: {param.ExitConditionExpression}");
                }

                // 执行循环
                for (int i = 1; i <= loopInfo.LoopCount; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    NlogHelper.Default.Info($"========== 第 {i}/{loopInfo.LoopCount} 次循环 ==========");

                    if (earlyExitEnabled)
                    {
                        try
                        {
                            // 使用表达式引擎计算退出条件
                            var exitResult = await _expressionEngine.EvaluateExpressionAsync(param.ExitConditionExpression);

                            // 添加详细日志
                            NlogHelper.Default.Info($"退出条件计算结果: Success={exitResult.Success}, Result={exitResult.Result}, ResultType={exitResult.Result?.GetType().Name ?? "null"}");

                            // 检查计算是否成功
                            if (!exitResult.Success)
                            {
                                NlogHelper.Default.Warn($"退出条件计算失败: {exitResult.ErrorMessage}");
                            }
                            else
                            {
                                // 尝试转换为bool
                                bool shouldExit = false;

                                if (exitResult.Result is bool boolResult)
                                {
                                    shouldExit = boolResult;
                                    NlogHelper.Default.Info($"条件结果为布尔值: {shouldExit}");
                                }
                                else
                                {
                                    // 尝试强制转换
                                    try
                                    {
                                        shouldExit = Convert.ToBoolean(exitResult.Result);
                                        NlogHelper.Default.Info($"条件结果已转换为布尔值: {shouldExit} (原始类型: {exitResult.Result?.GetType().Name})");
                                    }
                                    catch
                                    {
                                        NlogHelper.Default.Warn($"无法将条件结果转换为布尔值: {exitResult.Result}");
                                    }
                                }

                                // 判断是否应该退出
                                if (shouldExit)
                                {
                                    NlogHelper.Default.Info(
                                        $"满足退出条件 [{param.ExitConditionExpression}]，" +
                                        $"在第 {i}/{loopInfo.LoopCount} 次循环提前退出");

                                    // 立即退出循环
                                    break;
                                }
                                else
                                {
                                    NlogHelper.Default.Info($"未满足退出条件，继续循环");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // 条件计算出错，记录日志但继续执行
                            NlogHelper.Default.Error(
                                $"计算退出条件出错: {ex.Message}，条件: {param.ExitConditionExpression}");
                        }
                    }


                    // 更新计数器变量
                    if (loopInfo.EnableCounter && !string.IsNullOrWhiteSpace(loopInfo.CounterVariableName))
                    {
                        _loopMethods.UpdateLoopCounter(loopInfo.CounterVariableName, i);
                    }

                    // 重置循环控制标志（保留原有的Break/Continue支持）
                    _workflowStateService.ShouldBreakLoop = false;
                    _workflowStateService.ShouldContinueLoop = false;

                    // 执行循环体子步骤
                    bool shouldBreak = false;
                    if (loopInfo.ChildSteps != null && loopInfo.ChildSteps.Count > 0)
                    {
                        foreach (var childSteps in loopInfo.ChildSteps)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            if (childSteps != null)
                            {
                                try
                                {
                                    // 递归调用执行子步骤
                                    var childResult = await ExecuteStepAsync(childSteps, cancellationToken);

                                    if (!childResult.Success)
                                    {
                                        NlogHelper.Default.Warn(
                                            $"循环子步骤执行失败: {childSteps.StepName}, 原因: {childResult.Message}");
                                    }

                                    // 检查循环控制指令（保留原有功能）
                                    if (_workflowStateService.ShouldBreakLoop)
                                    {
                                        NlogHelper.Default.Info("收到 Break 指令，跳出循环");
                                        shouldBreak = true;
                                        _workflowStateService.ShouldBreakLoop = false;
                                        break;
                                    }

                                    if (_workflowStateService.ShouldContinueLoop)
                                    {
                                        NlogHelper.Default.Info("收到 Continue 指令，继续下一次循环");
                                        _workflowStateService.ShouldContinueLoop = false;
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    NlogHelper.Default.Error($"执行循环子步骤异常: {childSteps.StepName}", ex);
                                }
                            }
                        }
                    }

                    // 如果收到 Break 指令，退出整个循环
                    if (shouldBreak)
                    {
                        break;
                    }

                    // 循环迭代之间的延时（可选）
                    await Task.Delay(10, cancellationToken);
                }

                NlogHelper.Default.Info("循环执行完成");
                return DetailedResult.Successful();
            }
            catch (OperationCanceledException)
            {
                NlogHelper.Default.Info("循环执行被取消");
                throw;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"循环执行异常", ex);
                return DetailedResult.Failed($"循环执行异常: {ex.Message}");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 转换步骤参数
        /// </summary>
        private static T ConvertParameter<T>(object stepParameter) where T : class
        {
            if (stepParameter == null) return null;

            if (stepParameter is T directParam)
                return directParam;

            try
            {
                var json = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"参数转换失败: {typeof(T).Name}", ex);
                return null;
            }
        }

        #endregion
    }
}

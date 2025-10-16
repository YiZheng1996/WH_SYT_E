using MainUI.LogicalConfiguration.Methods;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods;
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
    FlowControlMethods flowControlMethods,
    ReportMethods reportMethods)
    {
        #region 字段和属性

        private readonly List<ChildModel> _steps = steps ?? throw new ArgumentNullException(nameof(steps));
        private readonly SystemMethods _systemMethods = systemMethods ?? throw new ArgumentNullException(nameof(systemMethods));
        private readonly VariableMethods _variableMethods = variableMethods ?? throw new ArgumentNullException(nameof(variableMethods));
        private readonly PLCMethods _plcMethods = plcMethods ?? throw new ArgumentNullException(nameof(plcMethods));
        private readonly DetectionMethods _detectionMethods = detectionMethods ?? throw new ArgumentNullException(nameof(detectionMethods));
        private readonly FlowControlMethods _flowControlMethods = flowControlMethods ?? throw new ArgumentNullException(nameof(flowControlMethods));
        private readonly ReportMethods _reportMethods = reportMethods ?? throw new ArgumentNullException(nameof(reportMethods));

        public event Action<ChildModel, int> StepStatusChanged;

        private bool _isExecuting;
        private int _currentStepIndex;
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

            try
            {
                NlogHelper.Default.Info($"开始执行步骤序列，共 {_steps.Count} 个步骤");

                while (_isExecuting && _currentStepIndex < _steps.Count)
                {
                    var step = _steps[_currentStepIndex];

                    // 更新步骤状态为执行中
                    step.Status = 1; // 执行中
                    StepStatusChanged?.Invoke(step, _currentStepIndex);

                    try
                    {
                        NlogHelper.Default.Info($"执行步骤 [{_currentStepIndex + 1}/{_steps.Count}]: {step.StepName}");

                        // 执行步骤
                        var result = await ExecuteStepAsync(step);

                        if (result.Succes)
                        {
                            step.Status = 2; // 成功
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
                            NlogHelper.Default.Error($"步骤执行失败: {step.StepName}, 原因: {result.Message}");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        step.Status = 3; // 失败
                        NlogHelper.Default.Error($"步骤执行异常: {step.StepName}", ex);
                        break;
                    }
                    finally
                    {
                        StepStatusChanged?.Invoke(step, _currentStepIndex);
                    }

                    _currentStepIndex++;

                    // 步骤间延时（可选）
                    if (_isExecuting)
                    {
                        await Task.Delay(50); // 50ms延时，可以配置
                    }
                }

                NlogHelper.Default.Info("步骤序列执行完成");
            }
            finally
            {
                _isExecuting = false;
            }
        }

        /// <summary>
        /// 停止执行
        /// </summary>
        public void Stop()
        {
            _isExecuting = false;
            NlogHelper.Default.Info("步骤执行已停止");
        }

        #endregion

        #region 步骤执行核心逻辑

        /// <summary>
        /// 执行单个步骤 - 使用 switch-case 替代策略模式
        /// </summary>
        private async Task<ExecutionResult> ExecuteStepAsync(ChildModel step)
        {
            try
            {
                return step.StepName switch
                {
                    // 系统工具
                    "延时等待" => await ExecuteDelayTime(step),
                    "消息通知" => await ExecuteSystemPrompt(step),

                    // 变量管理
                    "变量定义" or "试验参数" => await ExecuteDefineVar(step),
                    "变量赋值" => await ExecuteVariableAssignment(step),

                    // PLC通信
                    "读取PLC" => await ExecuteReadPLC(step),
                    "写入PLC" => await ExecuteWritePLC(step),

                    // 检测工具
                    "检测工具" => await ExecuteDetection(step),

                    // 流程控制
                    "条件判断" => await ExecuteCondition(step),

                    // 报表工具
                    //"保存报表" => await ExecuteSaveReport(step),
                    "读取单元格" => await ExecuteReadCells(step),
                    "写入单元格" => await ExecuteWriteCells(step),

                    // 未知步骤类型
                    _ => ExecutionResult.Failed($"不支持的步骤类型: {step.StepName}")
                };
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"执行步骤失败: {step.StepName}", ex);
                return ExecutionResult.Failed($"执行异常: {ex.Message}");
            }
        }

        #endregion

        #region 具体步骤执行方法

        private async Task<ExecutionResult> ExecuteDelayTime(ChildModel step)
        {
            var param = ConvertParameter<Parameter_DelayTime>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _systemMethods.DelayTime(param);
            return result ? ExecutionResult.Success() : ExecutionResult.Failed("延时执行失败");
        }

        private async Task<ExecutionResult> ExecuteSystemPrompt(ChildModel step)
        {
            var param = ConvertParameter<Parameter_SystemPrompt>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _systemMethods.SystemPrompt(param);
            return result ? ExecutionResult.Success() : ExecutionResult.Failed("提示执行失败");
        }

        private async Task<ExecutionResult> ExecuteDefineVar(ChildModel step)
        {
            var param = ConvertParameter<Parameter_DefineVar>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _variableMethods.DefineVar(param);
            return result ? ExecutionResult.Success() : ExecutionResult.Failed("变量定义失败");
        }

        private async Task<ExecutionResult> ExecuteVariableAssignment(ChildModel step)
        {
            var param = ConvertParameter<Parameter_VariableAssignment>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _variableMethods.VariableAssignment(param);
            return result ? ExecutionResult.Success() : ExecutionResult.Failed("变量赋值失败");
        }

        private async Task<ExecutionResult> ExecuteReadPLC(ChildModel step)
        {
            var param = ConvertParameter<Parameter_ReadPLC>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _plcMethods.ReadPLC(param);
            return result ? ExecutionResult.Success() : ExecutionResult.Failed("PLC读取失败");
        }

        private async Task<ExecutionResult> ExecuteWritePLC(ChildModel step)
        {
            var param = ConvertParameter<Parameter_WritePLC>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _plcMethods.WritePLC(param);
            return result ? ExecutionResult.Success() : ExecutionResult.Failed("PLC写入失败");
        }

        private async Task<ExecutionResult> ExecuteDetection(ChildModel step)
        {
            var param = ConvertParameter<Parameter_Detection>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _detectionMethods.Detection(param);
            return result ? ExecutionResult.Success() : ExecutionResult.Failed("检测执行失败");
        }

        private async Task<ExecutionResult> ExecuteCondition(ChildModel step)
        {
            var param = ConvertParameter<Parameter_Condition>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _flowControlMethods.EvaluateCondition(param);

            // 条件判断可能返回跳转索引
            if (result >= 0)
            {
                return ExecutionResult.Jump(result);
            }

            return result == 0 ? ExecutionResult.Success() : ExecutionResult.Failed("条件判断失败");
        }

        private async Task<ExecutionResult> ExecuteSaveReport(ChildModel step)
        {
            var param = ConvertParameter<Parameter_SaveReport>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _reportMethods.SaveReport(param);
            return result ? ExecutionResult.Success() : ExecutionResult.Failed("报表保存失败");
        }

        private async Task<ExecutionResult> ExecuteReadCells(ChildModel step)
        {
            var param = ConvertParameter<Parameter_ReadCells>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _reportMethods.ReadCells(param);
            return (bool)result ? ExecutionResult.Success() : ExecutionResult.Failed("单元格读取失败");
        }

        private async Task<ExecutionResult> ExecuteWriteCells(ChildModel step)
        {
            var param = ConvertParameter<Parameter_WriteCells>(step.StepParameter);
            if (param == null) return ExecutionResult.Failed("参数转换失败");

            var result = await _reportMethods.WriteCells(param);
            return result ? ExecutionResult.Success() : ExecutionResult.Failed("单元格写入失败");
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

    #region 执行结果类

    /// <summary>
    /// 执行结果
    /// </summary>
    public class ExecutionResult
    {
        public bool Succes { get; private set; }
        public string Message { get; private set; }
        public int? NextStepIndex { get; private set; }

        private ExecutionResult(bool success, string message = "", int? nextStepIndex = null)
        {
            Succes = success;
            Message = message ?? "";
            NextStepIndex = nextStepIndex;
        }

        public static ExecutionResult Success(string message = "") => new(true, message);
        public static ExecutionResult Failed(string message = "") => new(false, message);
        public static ExecutionResult Jump(int stepIndex) => new(true, "", stepIndex);
    }

    #endregion
}

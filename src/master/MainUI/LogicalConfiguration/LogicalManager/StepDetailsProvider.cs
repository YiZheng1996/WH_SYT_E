using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 步骤详情提供器 - 负责生成步骤配置的预览文本
    /// 用于在步骤表格中显示每个步骤的配置摘要
    /// </summary>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    public class StepDetailsProvider(ILogger logger = null)
    {

        /// <summary>
        /// 获取步骤详情预览文本
        /// </summary>
        /// <param name="step">步骤对象</param>
        /// <returns>详情预览文本</returns>
        public string GetStepDetailsPreview(ChildModel step)
        {
            if (step == null)
                return "无效步骤";

            try
            {
                // 根据步骤名称分发到不同的处理方法
                return step.StepName switch
                {
                    "变量赋值" => GetVariableAssignmentPreview(step),
                    "延时等待" => GetDelayPreview(step),
                    "等待稳定" => GetWaitForStablePreview(step),
                    "条件判断" => GetConditionPreview(step),
                    "循环工具" => GetLoopStartPreview(step),
                    "数据读取" => GetDataReadPreview(step),
                    "数据计算" => GetDataCalculationPreview(step),
                    "消息通知" => GetMessageNotificationPreview(step),
                    "读取PLC" => GetReadPLCPreview(step),
                    "写入PLC" => GetWritePLCPreview(step),
                    "读取单元格" => GetReadCellsPreview(step),
                    "写入单元格" => GetWriteCellsPreview(step),
                    _ => "双击查看详情"
                };
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "生成步骤详情预览失败: {StepName}", step.StepName);
                return "配置数据异常";
            }
        }

        #region 各步骤类型的预览生成方法

        /// <summary>
        /// 获取变量赋值步骤的预览
        /// </summary>
        private string GetVariableAssignmentPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_VariableAssignment>(step.StepParameter, out var param))
                return "未配置";

            // 获取目标变量和赋值类型
            var targetVar = param.TargetVarName ?? "未指定";
            var assignType = GetAssignmentTypeDisplay(param.AssignmentType);

            // 根据赋值类型生成不同的预览
            return param.AssignmentType switch
            {
                VariableAssignmentType.DirectAssignment =>
                    $"{targetVar} = {TruncateText(param.Expression, 40)}",

                VariableAssignmentType.ExpressionCalculation =>
                    $"{targetVar} = [{TruncateText(param.Expression, 35)}]",

                VariableAssignmentType.VariableCopy =>
                    $"{targetVar} ← @{param.Expression}",

                VariableAssignmentType.PLCRead =>
                    $"{targetVar} ← PLC[{param.DataSource?.PlcConfig?.ModuleName}.{param.DataSource?.PlcConfig?.Address}]",

                _ => $"{targetVar} = {assignType}"
            };
        }

        /// <summary>
        /// 获取延时等待步骤的预览
        /// </summary>
        private string GetDelayPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_DelayTime>(step.StepParameter, out var param))
                return "未配置";

            if (param.T < 1000)
                return $"等待 {param.T:F0} 毫秒";
            else if (param.T < 60000)
                return $"等待 {param.T / 1000:F1} 秒";
            else
                return $"等待 {param.T / 60000:F1} 分钟";
        }

        /// <summary>
        /// 获取等待稳定步骤的预览
        /// </summary>
        private string GetWaitForStablePreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_WaitForStable>(step.StepParameter, out var param))
                return "未配置";

            // 构建监测源描述
            string monitorSource;
            if (param.MonitorSourceType == MonitorSourceType.Variable)
            {
                monitorSource = string.IsNullOrEmpty(param.MonitorVariable)
                    ? "未指定变量"
                    : $"@{param.MonitorVariable}";
            }
            else // PLC
            {
                if (string.IsNullOrEmpty(param.PlcModuleName) || string.IsNullOrEmpty(param.PlcAddress))
                {
                    monitorSource = "未指定PLC点位";
                }
                else
                {
                    monitorSource = $"PLC[{param.PlcModuleName}.{param.PlcAddress}]";
                }
            }

            // 构建稳定条件描述
            string stabilityCondition = $"阈值≤{param.StabilityThreshold:F2}, 连续{param.StableCount}次";

            // 构建超时描述
            string timeoutDesc = param.TimeoutSeconds == 0
                ? "无限等待"
                : $"{param.TimeoutSeconds}秒超时";

            // 构建完整预览文本
            var previewParts = new List<string>
            {
                $"监测 {monitorSource}",
                stabilityCondition,
                $"间隔{param.SamplingInterval}秒"
            };

            // 添加赋值信息
            if (!string.IsNullOrWhiteSpace(param.AssignToVariable))
            {
                previewParts.Add($"→ @{param.AssignToVariable}");
            }

            // 添加超时信息
            previewParts.Add($"[{timeoutDesc}]");

            return string.Join(", ", previewParts);
        }

        /// <summary>
        /// 获取条件判断步骤的预览
        /// </summary>
        private string GetConditionPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_Detection>(step.StepParameter, out var param))
                return "未配置";

            // 添加失败处理信息
            var failureInfo = param.ResultHandling?.OnFailure switch
            {
                FailureAction.JumpToStep => $" [失败→步骤{param.ResultHandling.FailureJumpStep}]",
                FailureAction.Stop => " [失败→停止]",
                _ => ""
            };

            return $"判断: {param.ConditionExpression}";
        }

        /// <summary>
        /// 获取循环开始步骤的预览
        /// </summary>
        private string GetLoopStartPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_Loop>(step.StepParameter, out var param))
                return "未配置";

            // 获取循环次数表达式
            var loopCount = param.LoopCountExpression ?? "10";

            // 获取子步骤数量
            var childCount = param.ChildSteps?.Count ?? 0;

            // 构建预览文本
            var preview = $"循环 {loopCount} 次";

            // 添加子步骤计数
            if (childCount > 0)
            {
                preview += $", 包含 {childCount} 个步骤";
            }

            // 添加计数器信息
            if (param.EnableCounter && !string.IsNullOrWhiteSpace(param.CounterVariableName))
            {
                preview += $", 计数器: @{param.CounterVariableName}";
            }

            // 添加提前退出条件
            if (param.EnableEarlyExit && !string.IsNullOrWhiteSpace(param.ExitConditionExpression))
            {
                preview += $" [退出: {TruncateText(param.ExitConditionExpression, 30)}]";
            }

            return preview;
        }

        /// <summary>
        /// 获取数据读取步骤的预览
        /// </summary>
        private string GetDataReadPreview(ChildModel step)
        {
            // 根据实际的参数结构调整
            return "数据读取操作";
        }

        /// <summary>
        /// 获取数据计算步骤的预览
        /// </summary>
        private string GetDataCalculationPreview(ChildModel step)
        {
            // 根据实际的参数结构调整
            return "数据计算操作";
        }

        /// <summary>
        /// 获取消息通知步骤的预览
        /// </summary>
        private string GetMessageNotificationPreview(ChildModel step)
        {
            // 根据实际的参数结构调整
            return "消息通知";
        }

        /// <summary>
        /// 获取读取PLC步骤的预览
        /// </summary>
        private string GetReadPLCPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_ReadPLC>(step.StepParameter, out var param))
                return "未配置";

            if (param.Items == null || param.Items.Count == 0)
                return "未配置PLC点位";

            // 显示前2个PLC点位
            var preview = string.Join("; ", param.Items.Take(2).Select(item =>
                $"{item.PlcModuleName}.{item.PlcKeyName} → @{item.TargetVarName}"));

            if (param.Items.Count > 2)
                preview += $" ...等{param.Items.Count}个点位";

            return preview;
        }

        /// <summary>
        /// 获取写入PLC步骤的预览
        /// </summary>
        private string GetWritePLCPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_WritePLC>(step.StepParameter, out var param))
                return "未配置";

            if (param.Items == null || param.Items.Count == 0)
                return "未配置PLC点位";

            // 显示前2个PLC点位
            var preview = string.Join("; ", param.Items.Take(2).Select(item =>
            {
                var value = TruncateText(item.PlcValue, 15);
                return $"{item.PlcModuleName}.{item.PlcKeyName} ← {value}";
            }));

            if (param.Items.Count > 2)
                preview += $" ...等{param.Items.Count}个点位";

            return preview;
        }

        /// <summary>
        /// 获取读取单元格步骤的预览
        /// </summary>
        private string GetReadCellsPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_ReadCells>(step.StepParameter, out var param))
                return "未配置";

            if (param.ReadItems == null || param.ReadItems.Count == 0)
                return "未配置单元格";

            var sheetName = string.IsNullOrEmpty(param.SheetName) ? "Sheet1" : param.SheetName;

            // 显示前2个单元格
            var preview = string.Join("; ", param.ReadItems.Take(2).Select(item =>
                $"{item.CellAddress} → @{item.SaveToVariable}"));

            if (param.ReadItems.Count > 2)
                preview += $" ...等{param.ReadItems.Count}项";

            return $"[{sheetName}] {preview}";
        }

        /// <summary>
        /// 获取写入单元格步骤的预览
        /// </summary>
        private string GetWriteCellsPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_WriteCells>(step.StepParameter, out var param))
                return "未配置";

            if (param.Items == null || param.Items.Count == 0)
                return "未配置单元格";

            var sheetName = string.IsNullOrEmpty(param.SheetName) ? "Sheet1" : param.SheetName;

            // 显示前2个单元格
            var preview = string.Join("; ", param.Items.Take(2).Select(item =>
                $"{item.CellAddress} ← {GetCellValuePreview(item)}"));

            if (param.Items.Count > 2)
                preview += $" ...等{param.Items.Count}项";

            return $"[{sheetName}] {preview}";
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取赋值类型的显示文本
        /// </summary>
        private string GetAssignmentTypeDisplay(VariableAssignmentType type)
        {
            return type switch
            {
                VariableAssignmentType.DirectAssignment => "直接赋值",
                VariableAssignmentType.ExpressionCalculation => "表达式计算",
                VariableAssignmentType.VariableCopy => "变量复制",
                VariableAssignmentType.PLCRead => "PLC读取",
                _ => "未知类型"
            };
        }

        /// <summary>
        /// 获取单元格写入值的预览
        /// </summary>
        private string GetCellValuePreview(WriteCellItem item)
        {
            if (item == null) return "";

            return item.SourceType switch
            {
                CellsDataSourceType.FixedValue => TruncateText(item.FixedValue, 15),
                CellsDataSourceType.Variable => $"@{item.VariableName}",
                CellsDataSourceType.Expression => $"[{TruncateText(item.Expression, 20)}]",
                CellsDataSourceType.SystemProperty => $"${item.PropertyPath}",
                _ => TruncateText(item.FixedValue, 15)
            };
        }

        /// <summary>
        /// 截断文本到指定长度
        /// </summary>
        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            if (text.Length <= maxLength)
                return text;

            return string.Concat(text.AsSpan(0, maxLength), "...");
        }

        /// <summary>
        /// 尝试获取并解析参数
        /// </summary>
        private bool TryGetParameter<T>(object stepParameter, out T parameter) where T : class
        {
            parameter = null;

            if (stepParameter == null)
                return false;

            // ⭐ 关键修复：处理数值类型（0, -1等初始值）
            if (stepParameter is int ||
                stepParameter is long ||
                stepParameter is decimal ||
                stepParameter is double ||
                stepParameter is float ||
                stepParameter is short ||
                stepParameter is byte)
            {
                logger?.LogDebug("参数为数值类型({Value})，跳过解析", stepParameter);
                return false; // 返回false，显示"未配置"
            }

            try
            {
                // 直接类型转换
                if (stepParameter is T directParam)
                {
                    parameter = directParam;
                    return true;
                }

                // JSON字符串反序列化
                string jsonString = stepParameter is string str
                    ? str
                    : JsonConvert.SerializeObject(stepParameter);

                parameter = JsonConvert.DeserializeObject<T>(jsonString);
                return parameter != null;
            }
            catch (Exception ex)
            {
                logger?.LogDebug(ex, "参数解析失败: {ParameterType}", typeof(T).Name);
                return false;
            }
        }
        #endregion
    }
}
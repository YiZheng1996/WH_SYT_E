using MainUI.LogicalConfiguration.Instrument.Parameter;
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
                    "消息通知" => GetMessageNotificationPreview(step),
                    "实时监控" => GetRealtimeMonitorPreview(step),
                    "用户输入" => GetUserInputPreview(step),
                    "检测工具" => GetDetectionToolPreview(step),
                    "读取PLC" => GetReadPLCPreview(step),
                    "写入PLC" => GetWritePLCPreview(step),
                    "仪器通讯" => GetInstrumentCommunicationPreview(step),
                    "以太网发送" => GetEthernetSendPreview(step),
                    "串口发送" => GetSerialPortSendPreview(step),
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

            // 检查是否使用了变量表达式
            if (!string.IsNullOrEmpty(param.DelayValue) && param.DelayValue.Contains("{"))
            {
                // 使用变量的情况，显示变量表达式
                return $"延时: {param.DelayValue}";
            }

            return param.T switch
            {
                // 直接使用数值的情况
                < 1000 => $"等待 {param.T:F0} 毫秒",
                < 60000 => $"等待 {param.T / 1000:F1} 秒",
                _ => $"等待 {param.T / 60000:F1} 分钟"
            };
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
        /// 消息通知步骤的预览
        /// </summary>
        private string GetMessageNotificationPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_SystemPrompt>(step.StepParameter, out var param))
                return "未配置";

            // 根据提示等级选择图标
            var levelIcon = param.MessageLevel switch
            {
                MessageLevel.Info => "信息",
                MessageLevel.Warning => "警告",
                MessageLevel.Error => "错误",
                MessageLevel.Question => "询问",
                _ => "💬"
            };

            // 消息内容（截断显示）
            string message = string.IsNullOrEmpty(param.Message)
                ? "（空消息）"
                : TruncateText(param.Message, 30);

            // 构建基础预览
            string preview = $"{levelIcon} {message}";

            // 添加对话框类型（仅非OK类型）
            if (param.DialogType != DialogType.OK)
            {
                string dialogTypeText = param.DialogType switch
                {
                    DialogType.YesNo => "是/否",
                    DialogType.OKCancel => "确认/取消",
                    _ => ""
                };

                if (!string.IsNullOrEmpty(dialogTypeText))
                {
                    preview += $" [{dialogTypeText}";

                    // 如果有结果变量，添加到预览中
                    if (!string.IsNullOrEmpty(param.ResultVariable))
                    {
                        preview += $"→@{param.ResultVariable}";
                    }

                    preview += "]";
                }
            }

            return preview;
        }

        /// <summary>
        /// 实时监控步骤的预览
        /// </summary>
        private string GetRealtimeMonitorPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_RealtimeMonitorPrompt>(step.StepParameter, out var param))
                return "未配置";

            // 监测源
            string source = param.MonitorSourceType == MonitorSourceType.Variable
                ? $"变量:{param.MonitorVariable}"
                : $"PLC:{param.PlcModuleName}.{param.PlcAddress}";

            // 提示内容截断
            string prompt = string.IsNullOrEmpty(param.PromptMessage)
                ? "无提示"
                : TruncateText(param.PromptMessage, 20);

            // 单位
            string unit = string.IsNullOrEmpty(param.Unit) ? "" : $" ({param.Unit})";

            return $"{prompt} | 监测:{source}{unit}";
        }

        /// <summary>
        /// 用户输入步骤的预览
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        private string GetUserInputPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_UserInput>(step.StepParameter, out var param))
                return "未配置";

            string typeText = param.InputType switch
            {
                UserInputType.Number => "数值",
                UserInputType.Select => "下拉",
                _ => "文本"
            };
            string varPart = string.IsNullOrEmpty(param.TargetVariableName)
                ? "（未设置变量）"
                : $"→ @{param.TargetVariableName}";
            string timeout = param.TimeoutSeconds > 0 ? $" [{param.TimeoutSeconds}s]" : "";

            return $"输入{typeText} {varPart}{timeout}";
        }


        /// <summary>
        /// 检测工具步骤的预览
        /// </summary>
        private string GetDetectionToolPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_Detection>(step.StepParameter, out var param))
                return "未配置";

            // 构建条件表达式预览
            string expression = string.IsNullOrEmpty(param.ConditionExpression)
                ? "未设置条件"
                : TruncateText(param.ConditionExpression, 35);

            // 构建超时信息
            string timeout = "";
            if (param.TimeoutMs > 0)
            {
                timeout = param.TimeoutMs >= 1000
                    ? $" [{param.TimeoutMs / 1000}s]"
                    : $" [{param.TimeoutMs}ms]";
            }

            return $"{expression}{timeout}";
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

        /// <summary>
        /// 获取以太网发送步骤的预览
        /// </summary>
        private string GetEthernetSendPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_EthernetSend>(step.StepParameter, out var param))
                return "未配置";

            // 构建连接信息
            var connectionInfo = $"{param.IPAddress}:{param.Port}";

            // 协议类型
            var protocol = param.Protocol == System.Net.Sockets.ProtocolType.Tcp ? "TCP" : "UDP";

            // 数据格式
            var format = param.DataFormat switch
            {
                Parameter_EthernetSend.DataFormatType.Text => "文本",
                Parameter_EthernetSend.DataFormatType.Hex => "HEX",
                Parameter_EthernetSend.DataFormatType.Base64 => "Base64",
                _ => "文本"
            };

            // 发送内容预览
            var contentPreview = string.IsNullOrEmpty(param.SendContent)
                ? "无内容"
                : TruncateText(param.SendContent, 20);

            // 响应设置
            var responseInfo = param.WaitResponse
                ? $", 等待响应→@{param.ResponseVariableName}"
                : "";

            return $"[{protocol}] {connectionInfo} | {format}: {contentPreview}{responseInfo}";
        }

        /// <summary>
        /// 获取串口发送步骤的预览
        /// </summary>
        private string GetSerialPortSendPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_SerialPortSend>(step.StepParameter, out var param))
                return "未配置";

            // 串口配置信息
            var portInfo = $"{param.PortName} ({param.BaudRate})";

            // 数据格式
            var format = param.DataFormat switch
            {
                Parameter_EthernetSend.DataFormatType.Text => "文本",
                Parameter_EthernetSend.DataFormatType.Hex => "HEX",
                Parameter_EthernetSend.DataFormatType.Base64 => "Base64",
                _ => "文本"
            };

            // 发送内容预览
            var contentPreview = string.IsNullOrEmpty(param.SendContent)
                ? "无内容"
                : TruncateText(param.SendContent, 20);

            // 响应设置
            var responseInfo = param.WaitResponse
                ? $", 等待响应→@{param.ResponseVariableName}"
                : "";

            return $"[{portInfo}] {format}: {contentPreview}{responseInfo}";
        }

        // 添加对应的预览方法
        private string GetInstrumentCommunicationPreview(ChildModel step)
        {
            if (!TryGetParameter<Parameter_InstrumentCommunication>(step.StepParameter, out var param))
                return "未配置";

            return param.UseCustomCommand
                ? $"[{param.InstrumentName}] 自定义命令"
                : $"[{param.InstrumentName}] {param.CommandName}";
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
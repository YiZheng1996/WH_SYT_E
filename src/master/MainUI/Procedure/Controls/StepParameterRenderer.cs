using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Label = AntdUI.Label;
using Panel = Sunny.UI.UIPanel;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 步骤参数渲染器
    /// 负责将各类步骤参数渲染为控件，添加到 detailsPanel 中
    /// </summary>
    public class StepParameterRenderer(Panel detailsPanel)
    {
        private readonly Panel _detailsPanel = detailsPanel ?? throw new ArgumentNullException(nameof(detailsPanel));
        private GlobalVariableManager _variableManager = Program.ServiceProvider?.GetService<GlobalVariableManager>();

        // 共享颜色定义（由 StepStatusControl 传入）
        private static class StatusColors
        {
            public static readonly Color Waiting = ColorTranslator.FromHtml("#C4C7CC");
            public static readonly Color Running = ColorTranslator.FromHtml("#1890FF");
            public static readonly Color Success = ColorTranslator.FromHtml("#52C41A");
            public static readonly Color Failed = ColorTranslator.FromHtml("#E73624");
            public static readonly Color Skipped = ColorTranslator.FromHtml("#FAAD14");
        }

        #region 入口分发

        public int Render(string stepType, object stepParameter, int yPosition)
        {
            return stepType switch
            {
                "写入单元格" or "WriteCells" => DisplayWriteCells(stepParameter, yPosition),
                "变量赋值" or "VariableAssignment" => DisplayVariableAssignment(stepParameter, yPosition),
                "读取单元格" or "ReadCells" => DisplayReadCells(stepParameter, yPosition),
                "监测工具" or "Condition" => DisplayCondition(stepParameter, yPosition),
                "条件判断" => DisplayConditionTool(stepParameter, yPosition),
                "延时等待" or "Delay" => DisplayDelay(stepParameter, yPosition),
                "写入PLC" or "WritePLC" => DisplayWritePLC(stepParameter, yPosition),
                "读取PLC" or "ReadPLC" => DisplayReadPLC(stepParameter, yPosition),
                "以太网发送" or "EthernetSend" => DisplayEthernetSend(stepParameter, yPosition),
                "串口发送" or "SerialPortSend" => DisplaySerialPortSend(stepParameter, yPosition),
                "等待稳定" or "WaitForStable" => DisplayWaitForStable(stepParameter, yPosition),
                "实时监控" => DisplayRealtimeMonitorPrompt(stepParameter, yPosition),
                "循环工具" => DisplayLoop(stepParameter, yPosition),
                "检测工具" => DisplayDetectionTool(stepParameter, yPosition),
                "消息通知" => DisplayMessageNotification(stepParameter, yPosition),
                "用户输入" => DisplayUserInput(stepParameter, yPosition),
                "仪器通讯" or "InstrumentCommunication" => DisplayInstrumentCommunication(stepParameter, yPosition),
                _ => DisplayGeneric(stepParameter, yPosition)
            };
        }

        #endregion

        #region 写入单元格

        private int DisplayWriteCells(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_WriteCells>(stepParameter);
                if (param == null) return DisplayGeneric(stepParameter, yPosition);

                yPosition = AddSubTitle("报表配置", yPosition);
                yPosition = AddLine("工作表", param.SheetName ?? "Sheet1", yPosition);
                yPosition += 10;

                if (param.Items?.Count > 0)
                {
                    yPosition = AddSubTitle("写入明细", yPosition);
                    int c1 = 100, c2 = 100, c3 = _detailsPanel.Width - 220;
                    AddCell("单元格地址", yPosition, 0, c1, true);
                    AddCell("数据来源", yPosition, c1, c2, true);
                    AddCell("内容", yPosition, c1 + c2, c3, true);
                    yPosition += 25;

                    foreach (var item in param.Items)
                    {
                        string src = item.SourceType switch
                        {
                            CellsDataSourceType.FixedValue => "固定值",
                            CellsDataSourceType.Variable => "变量",
                            CellsDataSourceType.Expression => "表达式",
                            CellsDataSourceType.SystemProperty => "系统属性",
                            _ => "未知"
                        };
                        string content = item.SourceType switch
                        {
                            CellsDataSourceType.FixedValue => item.FixedValue ?? "",
                            CellsDataSourceType.Variable => item.VariableName ?? "",
                            CellsDataSourceType.Expression => item.Expression ?? "",
                            CellsDataSourceType.SystemProperty => item.PropertyPath ?? "",
                            _ => ""
                        };
                        AddCell(item.CellAddress, yPosition, 0, c1, false);
                        AddCell(src, yPosition, c1, c2, false);
                        AddCell(content, yPosition, c1 + c2, c3, false);
                        yPosition += 22;
                    }
                }
                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayWriteCells 错误: {ex}");
                return DisplayGeneric(stepParameter, yPosition);
            }
        }

        #endregion

        #region 变量赋值

        private int DisplayVariableAssignment(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_VariableAssignment>(stepParameter);
                if (param == null) return DisplayGeneric(stepParameter, yPosition);

                yPosition = AddSubTitle("赋值配置", yPosition);
                var (c1, c2, y) = BeginTable(yPosition);
                yPosition = y;

                AddCell("目标变量", yPosition, 0, c1, false);
                AddCell(param.TargetVarName ?? "未指定", yPosition, c1, c2, false);
                yPosition += 22;

                string typeName = param.AssignmentType switch
                {
                    VariableAssignmentType.DirectAssignment => "直接赋值",
                    VariableAssignmentType.ExpressionCalculation => "表达式计算",
                    VariableAssignmentType.VariableCopy => "复制变量",
                    VariableAssignmentType.PLCRead => "PLC读取",
                    _ => "未知"
                };
                AddCell("赋值方式", yPosition, 0, c1, false);
                AddCell(typeName, yPosition, c1, c2, false);
                yPosition += 22;

                if (!string.IsNullOrEmpty(param.Expression))
                {
                    AddCell("表达式/值", yPosition, 0, c1, false);
                    AddCell(param.Expression, yPosition, c1, c2, false);
                    yPosition += 22;
                }
                if (!string.IsNullOrEmpty(param.Condition))
                {
                    AddCell("执行条件", yPosition, 0, c1, false);
                    AddCell(param.Condition, yPosition, c1, c2, false);
                    yPosition += 22;
                }

                AddCell("是否启用", yPosition, 0, c1, false);
                AddCell(param.IsAssignment ? "是" : "否", yPosition, c1, c2, false,
                    param.IsAssignment ? StatusColors.Success : StatusColors.Waiting);
                yPosition += 22;

                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayVariableAssignment 错误: {ex}");
                return DisplayGeneric(stepParameter, yPosition);
            }
        }

        #endregion

        #region 读取单元格

        private int DisplayReadCells(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_ReadCells>(stepParameter);
                if (param == null) return DisplayGeneric(stepParameter, yPosition);

                yPosition = AddSubTitle("报表读取配置", yPosition);
                var (c1, c2, y) = BeginTable(yPosition);
                yPosition = y;

                AddCell("工作表", yPosition, 0, c1, false);
                AddCell(param.SheetName ?? "Sheet1", yPosition, c1, c2, false);
                yPosition += 22;

                if (param.ReadItems == null || param.ReadItems.Count == 0)
                {
                    AddCell("读取项", yPosition, 0, c1, false);
                    AddCell("未配置单元格", yPosition, c1, c2, false, StatusColors.Failed);
                    return yPosition + 22;
                }

                yPosition = AddSeparator(yPosition);
                yPosition = AddSubTitle("读取项列表", yPosition);

                int cellCol = 100, varCol = 120, typeCol = _detailsPanel.Width - 240;
                AddCell("单元格", yPosition, 0, cellCol, true);
                AddCell("目标变量", yPosition, cellCol, varCol, true);
                AddCell("数据类型", yPosition, cellCol + varCol, typeCol, true);
                yPosition += 25;

                int max = Math.Min(param.ReadItems.Count, 10);
                for (int i = 0; i < max; i++)
                {
                    var item = param.ReadItems[i];
                    string typeName = item.DataType switch
                    {
                        CellDataType.String => "字符串",
                        CellDataType.Integer => "整数",
                        CellDataType.Decimal => "小数",
                        CellDataType.Boolean => "布尔",
                        CellDataType.DateTime => "日期时间",
                        _ => "字符串"
                    };
                    Color typeColor = item.DataType switch
                    {
                        CellDataType.Integer => Color.FromArgb(0, 102, 204),
                        CellDataType.Decimal => Color.FromArgb(204, 102, 0),
                        CellDataType.Boolean => Color.FromArgb(102, 0, 204),
                        CellDataType.DateTime => Color.FromArgb(0, 153, 76),
                        _ => Color.FromArgb(96, 96, 96)
                    };
                    AddCell(item.CellAddress ?? "", yPosition, 0, cellCol, false);
                    AddCell(item.SaveToVariable ?? "", yPosition, cellCol, varCol, false, Color.FromArgb(0, 102, 204));
                    AddCell(typeName, yPosition, cellCol + varCol, typeCol, false, typeColor);
                    yPosition += 22;
                }

                if (param.ReadItems.Count > max)
                {
                    AddCell("", yPosition, 0, cellCol, false);
                    AddCell($"...还有 {param.ReadItems.Count - max} 项", yPosition, cellCol, varCol + typeCol, false, Color.FromArgb(150, 150, 150));
                    yPosition += 22;
                }
                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayReadCells 错误: {ex}");
                return DisplayGeneric(stepParameter, yPosition);
            }
        }

        #endregion

        #region 监测工具（Parameter_Detection）

        private int DisplayCondition(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_Detection>(stepParameter);
                if (param == null) return DisplayGeneric(stepParameter, yPosition);

                yPosition = AddSubTitle("检测条件配置", yPosition);
                var (c1, c2, y) = BeginTable(yPosition);
                yPosition = y;

                if (!string.IsNullOrEmpty(param.DetectionName))
                {
                    AddCell("检测名称", yPosition, 0, c1, false);
                    AddCell(param.DetectionName, yPosition, c1, c2, false, Color.FromArgb(0, 102, 204));
                    yPosition += 22;
                }

                yPosition = AddSubTitle("检测条件", yPosition);
                string expr = param.ConditionExpression ?? "{value} >= 0";
                AddCell("条件表达式", yPosition, 0, c1, false);
                if (expr.Length > 40)
                {
                    AddCell(string.Concat(expr.AsSpan(0, 40), "..."), yPosition, c1, c2, false, Color.FromArgb(102, 51, 153));
                    yPosition += 22;
                    AddCell("", yPosition, 0, c1, false);
                    AddCell($"完整: {expr}", yPosition, c1, c2, false, Color.FromArgb(100, 100, 100));
                }
                else
                {
                    AddCell(expr, yPosition, c1, c2, false, Color.FromArgb(102, 51, 153));
                }
                yPosition += 22;

                string exprDesc = GetExpressionDescription(expr);
                if (!string.IsNullOrEmpty(exprDesc))
                {
                    AddCell("条件说明", yPosition, 0, c1, false);
                    AddCell(exprDesc, yPosition, c1, c2, false, Color.FromArgb(40, 167, 69));
                    yPosition += 22;
                }

                yPosition = AddSubTitle("超时和重试", yPosition);
                AddCell("超时时间", yPosition, 0, c1, false);
                AddCell($"{param.TimeoutMs} 毫秒 ({param.TimeoutMs / 1000.0:F1} 秒)", yPosition, c1, c2, false);
                yPosition += 22;

                if (param.RetryCount > 0)
                {
                    AddCell("重试次数", yPosition, 0, c1, false);
                    AddCell($"{param.RetryCount} 次", yPosition, c1, c2, false);
                    yPosition += 22;
                    AddCell("重试间隔", yPosition, 0, c1, false);
                    AddCell($"{param.RetryIntervalMs} 毫秒", yPosition, c1, c2, false);
                    yPosition += 22;
                }
                if (param.RefreshRateMs > 0)
                {
                    AddCell("刷新频率", yPosition, 0, c1, false);
                    AddCell($"{param.RefreshRateMs} 毫秒", yPosition, c1, c2, false);
                    yPosition += 22;
                }

                // 结果处理
                yPosition = AddSubTitle("结果处理", yPosition);
                string failAction = param.ResultHandling?.OnFailure switch
                {
                    FailureAction.Continue => "继续执行",
                    FailureAction.Stop => "停止流程",
                    FailureAction.JumpToStep => $"跳转到步骤 {param.ResultHandling.FailureJumpStep}",
                    _ => "未知"
                };
                Color failColor = param.ResultHandling?.OnFailure == FailureAction.Stop ? StatusColors.Failed : StatusColors.Waiting;
                AddCell("失败时", yPosition, 0, c1, false);
                AddCell(failAction, yPosition, c1, c2, false, failColor);
                yPosition += 22;

                if (param.ResultHandling?.SuccessJumpStep > 0)
                {
                    AddCell("成功时", yPosition, 0, c1, false);
                    AddCell($"跳转到步骤 {param.ResultHandling.SuccessJumpStep}", yPosition, c1, c2, false, StatusColors.Success);
                    yPosition += 22;
                }
                if (param.ResultHandling?.SaveToVariable == true)
                {
                    AddCell("保存结果到", yPosition, 0, c1, false);
                    AddCell(param.ResultHandling.ResultVariableName ?? "(未指定)", yPosition, c1, c2, false);
                    yPosition += 22;
                }
                if (param.ResultHandling?.SaveValueToVariable == true)
                {
                    AddCell("保存数值到", yPosition, 0, c1, false);
                    AddCell(param.ResultHandling.ValueVariableName ?? "(未指定)", yPosition, c1, c2, false);
                    yPosition += 22;
                }
                if (param.ResultHandling?.ShowResult == true)
                {
                    AddCell("显示结果", yPosition, 0, c1, false);
                    AddCell("✓ 是", yPosition, c1, c2, false, StatusColors.Success);
                    yPosition += 22;
                }

                return yPosition + 5;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayCondition 错误: {ex}");
                return DisplayGeneric(stepParameter, yPosition);
            }
        }

        #endregion

        #region 条件判断（Parameter_Condition）

        private int DisplayConditionTool(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_Condition>(stepParameter);
                if (param == null)
                {
                    var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                    return DisplayConditionToolFromJson(JObject.Parse(jsonStr), yPosition);
                }

                var (c1, c2, y) = BeginTable(yPosition, "检测工具配置");
                yPosition = y;

                if (!string.IsNullOrEmpty(param.Description))
                {
                    AddCell("描述", yPosition, 0, c1, false);
                    AddCell(param.Description, yPosition, c1, c2, false, Color.FromArgb(0, 102, 204));
                    yPosition += 22;
                }
                AddCell("启用状态", yPosition, 0, c1, false);
                AddCell(param.IsEnabled ? "✓ 已启用" : "✗ 已禁用", yPosition, c1, c2, false,
                    param.IsEnabled ? StatusColors.Success : StatusColors.Skipped);
                yPosition += 22;

                yPosition = AddSeparator(yPosition);
                yPosition = AddSubTitle("条件表达式", yPosition);
                string expr = param.ConditionExpression ?? "(未设置)";
                AddCell("表达式", yPosition, 0, c1, false);
                AddCell(expr, yPosition, c1, c2, false,
                    string.IsNullOrEmpty(param.ConditionExpression) ? StatusColors.Failed : StatusColors.Waiting);
                yPosition += 22;

                string desc = GetExpressionDescription(param.ConditionExpression);
                if (!string.IsNullOrEmpty(desc))
                {
                    AddCell("类型", yPosition, 0, c1, false);
                    AddCell(desc, yPosition, c1, c2, false);
                    yPosition += 22;
                }

                yPosition = AddSeparator(yPosition);
                yPosition = AddSubTitle("执行分支", yPosition);

                int trueCount = param.TrueSteps?.Count ?? 0;
                AddCell("满足条件时", yPosition, 0, c1, false);
                AddCell($"{trueCount} 个子步骤", yPosition, c1, c2, false,
                    trueCount > 0 ? StatusColors.Success : StatusColors.Waiting);
                yPosition += 22;
                if (trueCount > 0) yPosition = DisplayChildStepsList(param.TrueSteps, "  → ", yPosition, c1, c2);

                int falseCount = param.FalseSteps?.Count ?? 0;
                AddCell("不满足条件时", yPosition, 0, c1, false);
                AddCell($"{falseCount} 个子步骤", yPosition, c1, c2, false,
                    falseCount > 0 ? StatusColors.Skipped : StatusColors.Waiting);
                yPosition += 22;
                if (falseCount > 0) yPosition = DisplayChildStepsList(param.FalseSteps, "  → ", yPosition, c1, c2);

                return yPosition + 5;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayConditionTool 错误: {ex}");
                return DisplayGeneric(stepParameter, yPosition);
            }
        }

        private int DisplayConditionToolFromJson(JObject json, int yPosition)
        {
            var (c1, c2, y) = BeginTable(yPosition, "检测工具配置");
            yPosition = y;

            string description = json["Description"]?.ToString();
            if (!string.IsNullOrEmpty(description))
            {
                AddCell("描述", yPosition, 0, c1, false);
                AddCell(description, yPosition, c1, c2, false, Color.FromArgb(0, 102, 204));
                yPosition += 22;
            }

            bool isEnabled = json["IsEnabled"]?.ToObject<bool>() ?? true;
            AddCell("启用状态", yPosition, 0, c1, false);
            AddCell(isEnabled ? "✓ 已启用" : "✗ 已禁用", yPosition, c1, c2, false,
                isEnabled ? StatusColors.Success : StatusColors.Skipped);
            yPosition += 22;

            yPosition = AddSeparator(yPosition);
            yPosition = AddSubTitle("条件表达式", yPosition);
            AddCell("表达式", yPosition, 0, c1, false);
            AddCell(json["ConditionExpression"]?.ToString() ?? "(未设置)", yPosition, c1, c2, false);
            yPosition += 22;

            yPosition = AddSeparator(yPosition);
            yPosition = AddSubTitle("执行分支", yPosition);

            int trueCount = (json["TrueSteps"] as JArray)?.Count ?? 0;
            int falseCount = (json["FalseSteps"] as JArray)?.Count ?? 0;
            AddCell("满足条件时", yPosition, 0, c1, false);
            AddCell($"{trueCount} 个子步骤", yPosition, c1, c2, false, trueCount > 0 ? StatusColors.Success : StatusColors.Waiting);
            yPosition += 22;
            AddCell("不满足条件时", yPosition, 0, c1, false);
            AddCell($"{falseCount} 个子步骤", yPosition, c1, c2, false, falseCount > 0 ? StatusColors.Skipped : StatusColors.Waiting);
            yPosition += 22;

            return yPosition + 5;
        }

        private int DisplayChildStepsList(List<ChildModel> steps, string prefix, int yPosition, int c1, int c2)
        {
            int max = Math.Min(steps.Count, 5);
            for (int i = 0; i < max; i++)
            {
                AddCell($"{prefix}步骤{i + 1}", yPosition, 0, c1, false);
                AddCell($"[{steps[i].StepName}] {steps[i].Remark ?? ""}", yPosition, c1, c2, false, Color.FromArgb(100, 100, 100));
                yPosition += 20;
            }
            if (steps.Count > 5)
            {
                AddCell("", yPosition, 0, c1, false);
                AddCell($"... 还有 {steps.Count - 5} 个步骤", yPosition, c1, c2, false, Color.FromArgb(150, 150, 150));
                yPosition += 20;
            }
            return yPosition;
        }

        #endregion

        #region 延时等待

        private int DisplayDelay(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_DelayTime>(stepParameter);
                if (param == null) return DisplayGeneric(stepParameter, yPosition);

                var (c1, c2, y) = BeginTable(yPosition, "延时配置");
                yPosition = y;

                bool isExpression = !string.IsNullOrEmpty(param.DelayValue) && param.DelayValue.Contains("{");
                string unitName = Parameter_DelayTime.GetUnitDisplayName(param.Unit);

                if (isExpression)
                {
                    AddCell("延时表达式", yPosition, 0, c1, false);
                    AddCell(param.DelayValue, yPosition, c1, c2, false, Color.FromArgb(0, 102, 204));
                    yPosition += 22;

                    // 从变量管理器读取当前实际值
                    string currentValueDisplay = GetVariableCurrentValue(param.DelayValue, param.Unit);
                    AddCell("当前值", yPosition, 0, c1, false);
                    AddCell(currentValueDisplay + unitName, yPosition, c1, c2, false, Color.FromArgb(100, 100, 100));
                    yPosition += 22;
                }
                else
                {
                    double.TryParse(param.DelayValue, out double rawValue);
                    double ms = param.ConvertToMilliseconds(rawValue);

                    AddCell("延时时长", yPosition, 0, c1, false);
                    AddCell($"{rawValue:G} {unitName}  ({ms:F0} ms)", yPosition, c1, c2, false);
                    yPosition += 22;
                }

                return yPosition;
            }
            catch { return DisplayGeneric(stepParameter, yPosition); }
        }

        /// <summary>
        /// 从变量管理器读取变量当前值，拼接单位显示
        /// </summary>
        private string GetVariableCurrentValue(string delayValue, TimeUnit unit)
        {
            try
            {
                // 提取变量名，去掉花括号
                var match = System.Text.RegularExpressions.Regex.Match(delayValue, @"\{(.+?)\}");
                if (!match.Success) return "未知";

                string varName = match.Groups[1].Value;
                var varValue = _variableManager?.FindVariableByName(varName);

                if (varValue == null) return "(变量未赋值)";

                if (double.TryParse(varValue.ToString(), out double numVal))
                {
                    string unitName = Parameter_DelayTime.GetUnitDisplayName(unit);
                    double ms = unit switch
                    {
                        TimeUnit.Milliseconds => numVal,
                        TimeUnit.Seconds => numVal * 1000,
                        TimeUnit.Minutes => numVal * 60000,
                        _ => numVal
                    };
                    return $"{numVal:G} {unitName}  ({ms:F0} ms)";
                }

                return varValue.VarValue.ToString();
            }
            catch
            {
                return "(读取失败)";
            }
        }

        #endregion

        #region 读写PLC

        private int DisplayReadPLC(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);
                yPosition = AddSubTitle("PLC读取配置", yPosition);

                var items = json["Items"];
                if (items != null && items.Type == JTokenType.Array && items.HasValues)
                {
                    int c1 = 100, c2 = 120, c3 = _detailsPanel.Width - 240;
                    AddCell("模块名称", yPosition, 0, c1, true);
                    AddCell("点位地址", yPosition, c1, c2, true);
                    AddCell("目标变量", yPosition, c1 + c2, c3, true);
                    yPosition += 25;
                    foreach (var item in items)
                    {
                        AddCell(item["ModuleName"]?.ToString() ?? "", yPosition, 0, c1, false);
                        AddCell(item["Address"]?.ToString() ?? "", yPosition, c1, c2, false);
                        AddCell(item["TargetVariable"]?.ToString() ?? "", yPosition, c1 + c2, c3, false);
                        yPosition += 22;
                    }
                }
                else
                {
                    var (c1, c2, y) = BeginTable(yPosition);
                    yPosition = y;
                    AddCell("模块名称", yPosition, 0, c1, false); AddCell(json["ModuleName"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                    AddCell("点位地址", yPosition, 0, c1, false); AddCell(json["Address"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                    AddCell("目标变量", yPosition, 0, c1, false); AddCell(json["TargetVariable"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                }
                return yPosition;
            }
            catch { return DisplayGeneric(stepParameter, yPosition); }
        }

        private int DisplayWritePLC(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);
                yPosition = AddSubTitle("PLC写入配置", yPosition);

                var items = json["Items"];
                if (items != null && items.Type == JTokenType.Array && items.HasValues)
                {
                    int c1 = 100, c2 = 120, c3 = _detailsPanel.Width - 240;
                    AddCell("模块名称", yPosition, 0, c1, true);
                    AddCell("点位地址", yPosition, c1, c2, true);
                    AddCell("写入值", yPosition, c1 + c2, c3, true);
                    yPosition += 25;
                    foreach (var item in items)
                    {
                        AddCell(item["PlcModuleName"]?.ToString() ?? "", yPosition, 0, c1, false);
                        AddCell(item["PlcKeyName"]?.ToString() ?? "", yPosition, c1, c2, false);
                        AddCell(item["PlcValue"]?.ToString() ?? "", yPosition, c1 + c2, c3, false);
                        yPosition += 22;
                    }
                }
                else
                {
                    var (c1, c2, y) = BeginTable(yPosition);
                    yPosition = y;
                    AddCell("模块名称", yPosition, 0, c1, false); AddCell(json["ModuleName"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                    AddCell("点位地址", yPosition, 0, c1, false); AddCell(json["Address"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                    AddCell("写入值", yPosition, 0, c1, false); AddCell(json["Value"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                }
                return yPosition;
            }
            catch { return DisplayGeneric(stepParameter, yPosition); }
        }

        #endregion

        #region 以太网 / 串口发送

        private int DisplayEthernetSend(object stepParameter, int yPosition)
        {
            try
            {
                var json = ParseJson(stepParameter);
                var (c1, c2, y) = BeginTable(yPosition, "以太网发送配置");
                yPosition = y;

                AddCell("IP地址", yPosition, 0, c1, false); AddCell(json["IPAddress"]?.ToString() ?? "192.168.1.100", yPosition, c1, c2, false); yPosition += 22;
                AddCell("端口", yPosition, 0, c1, false); AddCell(json["Port"]?.ToString() ?? "502", yPosition, c1, c2, false); yPosition += 22;

                string proto = (json["Protocol"]?.ToString() ?? "Tcp").Contains("Udp", StringComparison.OrdinalIgnoreCase) ? "UDP" : "TCP";
                AddCell("协议类型", yPosition, 0, c1, false);
                AddCell(proto, yPosition, c1, c2, false, proto == "TCP" ? StatusColors.Success : StatusColors.Running);
                yPosition += 22;

                AddCell("数据格式", yPosition, 0, c1, false); AddCell(json["DataFormat"]?.ToString() ?? "Text", yPosition, c1, c2, false); yPosition += 22;

                string content = json["SendContent"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(content))
                {
                    AddCell("发送内容", yPosition, 0, c1, false);
                    AddCell(content.Length > 50 ? content[..50] + "..." : content, yPosition, c1, c2, false);
                    yPosition += 22;
                }

                bool wait = json["WaitResponse"]?.ToObject<bool>() ?? false;
                AddCell("等待响应", yPosition, 0, c1, false);
                AddCell(wait ? "是" : "否", yPosition, c1, c2, false, wait ? StatusColors.Success : Color.Gray);
                yPosition += 22;

                if (wait)
                {
                    string rv = json["ResponseVariableName"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(rv)) { AddCell("响应变量", yPosition, 0, c1, false); AddCell($"@{rv}", yPosition, c1, c2, false, StatusColors.Success); yPosition += 22; }
                }
                return yPosition;
            }
            catch { return DisplayGeneric(stepParameter, yPosition); }
        }

        private int DisplaySerialPortSend(object stepParameter, int yPosition)
        {
            try
            {
                var json = ParseJson(stepParameter);
                var (c1, c2, y) = BeginTable(yPosition, "串口发送配置");
                yPosition = y;

                AddCell("串口", yPosition, 0, c1, false); AddCell(json["PortName"]?.ToString() ?? "COM1", yPosition, c1, c2, false); yPosition += 22;
                AddCell("波特率", yPosition, 0, c1, false); AddCell(json["BaudRate"]?.ToString() ?? "9600", yPosition, c1, c2, false); yPosition += 22;
                AddCell("数据位", yPosition, 0, c1, false); AddCell(json["DataBits"]?.ToString() ?? "8", yPosition, c1, c2, false); yPosition += 22;
                AddCell("校验位", yPosition, 0, c1, false); AddCell(json["Parity"]?.ToString() ?? "None", yPosition, c1, c2, false); yPosition += 22;
                AddCell("停止位", yPosition, 0, c1, false); AddCell(json["StopBits"]?.ToString() ?? "One", yPosition, c1, c2, false); yPosition += 22;
                AddCell("数据格式", yPosition, 0, c1, false); AddCell(json["DataFormat"]?.ToString() ?? "Text", yPosition, c1, c2, false); yPosition += 22;

                string content = json["SendContent"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(content))
                {
                    AddCell("发送内容", yPosition, 0, c1, false);
                    AddCell(content.Length > 50 ? content[..50] + "..." : content, yPosition, c1, c2, false);
                    yPosition += 22;
                }

                bool wait = json["WaitResponse"]?.ToObject<bool>() ?? false;
                AddCell("等待响应", yPosition, 0, c1, false);
                AddCell(wait ? "是" : "否", yPosition, c1, c2, false, wait ? StatusColors.Success : Color.Gray);
                yPosition += 22;

                if (wait)
                {
                    string rv = json["ResponseVariableName"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(rv)) { AddCell("响应变量", yPosition, 0, c1, false); AddCell($"@{rv}", yPosition, c1, c2, false, StatusColors.Success); yPosition += 22; }
                }
                return yPosition;
            }
            catch { return DisplayGeneric(stepParameter, yPosition); }
        }

        #endregion

        #region 等待稳定

        private int DisplayWaitForStable(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_WaitForStable>(stepParameter);
                if (param == null) return DisplayGeneric(stepParameter, yPosition);

                var (c1, c2, y) = BeginTable(yPosition, "等待稳定配置");
                yPosition = y;

                if (!string.IsNullOrEmpty(param.Description))
                {
                    AddCell("步骤描述", yPosition, 0, c1, false); AddCell(param.Description, yPosition, c1, c2, false); yPosition += 22;
                }

                bool isVar = param.MonitorSourceType == MonitorSourceType.Variable;
                AddCell("监测源类型", yPosition, 0, c1, false);
                AddCell(isVar ? "全局变量" : "PLC点位", yPosition, c1, c2, false, isVar ? StatusColors.Success : StatusColors.Running);
                yPosition += 22;

                if (isVar)
                {
                    AddCell("监测变量", yPosition, 0, c1, false); AddCell(param.MonitorVariable ?? "(未指定)", yPosition, c1, c2, false); yPosition += 22;
                }
                else
                {
                    AddCell("PLC模块", yPosition, 0, c1, false); AddCell(param.PlcModuleName ?? "(未指定)", yPosition, c1, c2, false); yPosition += 22;
                    AddCell("PLC地址", yPosition, 0, c1, false); AddCell(param.PlcAddress ?? "(未指定)", yPosition, c1, c2, false); yPosition += 22;
                }

                yPosition = AddSeparator(yPosition);
                yPosition = AddSubTitle("稳定判据", yPosition);
                AddCell("稳定阈值", yPosition, 0, c1, false); AddCell($"{param.StabilityThreshold:F4} (单位/秒)", yPosition, c1, c2, false); yPosition += 22;
                AddCell("采样间隔", yPosition, 0, c1, false); AddCell($"{param.SamplingInterval} 秒", yPosition, c1, c2, false); yPosition += 22;
                AddCell("连续稳定次数", yPosition, 0, c1, false); AddCell($"{param.StableCount} 次", yPosition, c1, c2, false); yPosition += 22;

                yPosition = AddSeparator(yPosition);
                yPosition = AddSubTitle("超时配置", yPosition);
                string timeoutDisplay = param.TimeoutSeconds > 0 ? $"{param.TimeoutSeconds} 秒" : "无限等待";
                AddCell("超时时间", yPosition, 0, c1, false);
                AddCell(timeoutDisplay, yPosition, c1, c2, false, param.TimeoutSeconds > 0 ? Color.FromArgb(100, 100, 100) : StatusColors.Waiting);
                yPosition += 22;

                string timeoutAction = param.OnTimeout switch
                {
                    TimeoutAction.ContinueAndLog => "继续执行并记录日志",
                    TimeoutAction.StopProcedure => $"停止整个流程",
                    TimeoutAction.JumpToStep => $"跳转到步骤 {param.TimeoutJumpToStep}",
                    _ => "未知"
                };
                Color actionColor = param.OnTimeout switch
                {
                    TimeoutAction.ContinueAndLog => StatusColors.Success,
                    TimeoutAction.StopProcedure => StatusColors.Failed,
                    TimeoutAction.JumpToStep => StatusColors.Skipped,
                    _ => StatusColors.Waiting
                };
                AddCell("超时动作", yPosition, 0, c1, false);
                AddCell(timeoutAction, yPosition, c1, c2, false, actionColor);
                yPosition += 22;

                if (!string.IsNullOrEmpty(param.AssignToVariable))
                {
                    yPosition = AddSeparator(yPosition);
                    yPosition = AddSubTitle("结果处理", yPosition);
                    AddCell("赋值目标变量", yPosition, 0, c1, false);
                    AddCell(param.AssignToVariable, yPosition, c1, c2, false, StatusColors.Success);
                    yPosition += 22;
                }
                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayWaitForStable 错误: {ex}");
                return DisplayGeneric(stepParameter, yPosition);
            }
        }

        #endregion

        #region 实时监控

        private int DisplayRealtimeMonitorPrompt(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_RealtimeMonitorPrompt>(stepParameter);
                if (param == null) return DisplayGeneric(stepParameter, yPosition);

                var (c1, c2, y) = BeginTable(yPosition, "实时监控提示配置");
                yPosition = y;

                bool isVar = param.MonitorSourceType == MonitorSourceType.Variable;
                AddCell("窗体标题", yPosition, 0, c1, false); AddCell(param.Title, yPosition, c1, c2, false); yPosition += 22;
                AddCell("监测源类型", yPosition, 0, c1, false); AddCell(isVar ? "全局变量" : "PLC点位", yPosition, c1, c2, false); yPosition += 22;
                AddCell("监测源", yPosition, 0, c1, false); AddCell(isVar ? param.MonitorVariable : $"{param.PlcModuleName}.{param.PlcAddress}", yPosition, c1, c2, false); yPosition += 22;
                AddCell("提示信息", yPosition, 0, c1, false); AddCell(param.PromptMessage.Replace("\n", " "), yPosition, c1, c2, false); yPosition += 22;

                if (!string.IsNullOrEmpty(param.Unit)) { AddCell("数值单位", yPosition, 0, c1, false); AddCell(param.Unit, yPosition, c1, c2, false); yPosition += 22; }
                AddCell("显示格式", yPosition, 0, c1, false); AddCell(param.DisplayFormat ?? "F1", yPosition, c1, c2, false); yPosition += 22;
                AddCell("刷新间隔", yPosition, 0, c1, false); AddCell($"{param.RefreshInterval} 毫秒", yPosition, c1, c2, false); yPosition += 22;
                AddCell("按钮文本", yPosition, 0, c1, false); AddCell(param.ButtonText ?? "确定", yPosition, c1, c2, false); yPosition += 22;

                if (param.TimeoutSeconds > 0) { AddCell("超时时间", yPosition, 0, c1, false); AddCell($"{param.TimeoutSeconds} 秒", yPosition, c1, c2, false, Color.FromArgb(255, 165, 0)); yPosition += 22; }
                if (param.ShowValueLabel && !string.IsNullOrEmpty(param.ValueLabelText)) { AddCell("数值标签", yPosition, 0, c1, false); AddCell(param.ValueLabelText, yPosition, c1, c2, false); yPosition += 22; }

                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayRealtimeMonitorPrompt 错误: {ex}");
                return DisplayGeneric(stepParameter, yPosition);
            }
        }

        #endregion

        #region 循环工具

        private int DisplayLoop(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_Loop>(stepParameter);

                int c1 = 120, c2 = _detailsPanel.Width - c1 - 10;
                yPosition = AddSubTitle("循环配置", yPosition);
                AddCell("配置项", yPosition, 0, c1, true);
                AddCell("配置值", yPosition, c1, c2, true);
                yPosition += 25;

                if (param != null)
                {
                    AddCell("循环次数", yPosition, 0, c1, false); AddCell(param.LoopCountExpression ?? "10", yPosition, c1, c2, false); yPosition += 22;
                    if (param.EnableCounter) { AddCell("计数器变量", yPosition, 0, c1, false); AddCell(param.CounterVariableName ?? "LoopIndex", yPosition, c1, c2, false); yPosition += 22; }
                    AddCell("子步骤数量", yPosition, 0, c1, false); AddCell($"{param.ChildSteps?.Count ?? 0} 个", yPosition, c1, c2, false); yPosition += 22;

                    if (param.EnableEarlyExit)
                    {
                        yPosition = AddSeparator(yPosition);
                        yPosition = AddSubTitle("提前退出配置", yPosition);
                        AddCell("退出条件", yPosition, 0, c1, false); AddCell(param.ExitConditionExpression ?? "", yPosition, c1, c2, false, StatusColors.Skipped); yPosition += 22;
                        if (!string.IsNullOrEmpty(param.ExitConditionDescription)) { AddCell("条件说明", yPosition, 0, c1, false); AddCell(param.ExitConditionDescription, yPosition, c1, c2, false); yPosition += 22; }
                    }
                }
                else
                {
                    // JSON 回退
                    var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                    var json = JObject.Parse(jsonStr);
                    AddCell("循环次数", yPosition, 0, c1, false); AddCell(json["LoopCountExpression"]?.ToString() ?? "10", yPosition, c1, c2, false); yPosition += 22;
                    bool enableCounter = json["EnableCounter"]?.ToObject<bool>() ?? true;
                    if (enableCounter) { AddCell("计数器变量", yPosition, 0, c1, false); AddCell(json["CounterVariableName"]?.ToString() ?? "LoopIndex", yPosition, c1, c2, false); yPosition += 22; }
                    int childCount = (json["ChildSteps"] as JArray)?.Count ?? 0;
                    AddCell("子步骤数量", yPosition, 0, c1, false); AddCell($"{childCount} 个", yPosition, c1, c2, false); yPosition += 22;
                    bool earlyExit = json["EnableEarlyExit"]?.ToObject<bool>() ?? false;
                    if (earlyExit)
                    {
                        yPosition = AddSeparator(yPosition);
                        yPosition = AddSubTitle("提前退出配置", yPosition);
                        AddCell("退出条件", yPosition, 0, c1, false); AddCell(json["ExitConditionExpression"]?.ToString() ?? "", yPosition, c1, c2, false, StatusColors.Skipped); yPosition += 22;
                    }
                }
                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayLoop 错误: {ex}");
                return DisplayGeneric(stepParameter, yPosition);
            }
        }

        #endregion

        #region 消息通知

        private int DisplayMessageNotification(object stepParameter, int yPosition)
        {
            try
            {
                var json = ParseJson(stepParameter);
                var (c1, c2, y) = BeginTable(yPosition, "消息通知配置");
                yPosition = y;

                AddCell("标题", yPosition, 0, c1, false); AddCell(json["Title"]?.ToString() ?? "提示", yPosition, c1, c2, false); yPosition += 22;
                AddCell("消息内容", yPosition, 0, c1, false); AddCell(json["Message"]?.ToString() ?? "", yPosition, c1, c2, false, Color.FromArgb(0, 102, 204)); yPosition += 22;

                string level = json["MessageLevel"]?.ToString() ?? "0";
                AddCell("提示等级", yPosition, 0, c1, false);
                AddCell(level switch { "0" or "Info" => "ℹ️ 信息", "1" or "Warning" => "⚠️ 警告", "2" or "Error" => "❌ 错误", "3" or "Question" => "❓ 询问", _ => "信息" },
                    yPosition, c1, c2, false,
                    level switch { "1" or "Warning" => StatusColors.Skipped, "2" or "Error" => StatusColors.Failed, "3" or "Question" => Color.FromArgb(102, 102, 255), _ => StatusColors.Running });
                yPosition += 22;

                string dlg = json["DialogType"]?.ToString() ?? "0";
                AddCell("对话框类型", yPosition, 0, c1, false);
                AddCell(dlg switch { "0" or "OK" => "确认", "1" or "YesNo" => "是/否", "2" or "OKCancel" => "确认/取消", _ => "确认" }, yPosition, c1, c2, false);
                yPosition += 22;

                if (dlg is "1" or "YesNo" or "2" or "OKCancel")
                {
                    string rv = json["ResultVariable"]?.ToString();
                    if (!string.IsNullOrEmpty(rv)) { AddCell("结果变量", yPosition, 0, c1, false); AddCell(rv, yPosition, c1, c2, false, Color.FromArgb(0, 102, 204)); yPosition += 22; }
                }

                string resp = json["UserResponse"]?.ToString();
                if (!string.IsNullOrEmpty(resp))
                {
                    yPosition = AddSeparator(yPosition);
                    yPosition = AddSubTitle("运行详情", yPosition);
                    AddCell("配置项", yPosition, 0, c1, true); AddCell("实际值", yPosition, c1, c2, true); yPosition += 25;
                    string respText = resp switch { "1" or "OK" => "✓ 确认", "6" or "Yes" => "✓ 是", "2" or "Cancel" => "✗ 取消", "7" or "No" => "✗ 否", _ => resp };
                    bool ok = resp is "1" or "OK" or "6" or "Yes";
                    AddCell("用户选择", yPosition, 0, c1, false); AddCell(respText, yPosition, c1, c2, false, ok ? StatusColors.Success : StatusColors.Failed); yPosition += 22;
                }
                return yPosition;
            }
            catch { return DisplayGeneric(stepParameter, yPosition); }
        }

        #endregion

        #region 用户输入

        private int DisplayUserInput(object stepParameter, int yPosition)
        {
            try
            {
                var param = Convert<Parameter_UserInput>(stepParameter);
                if (param == null) return DisplayGeneric(stepParameter, yPosition);

                var (c1, c2, y) = BeginTable(yPosition, "用户输入配置");
                yPosition = y;

                AddCell("弹窗标题", yPosition, 0, c1, false); AddCell(param.Title ?? "", yPosition, c1, c2, false); yPosition += 22;

                if (!string.IsNullOrEmpty(param.Prompt))
                {
                    AddCell("提示说明", yPosition, 0, c1, false); AddCell(param.Prompt, yPosition, c1, c2, false); yPosition += 22;
                }

                string inputType = param.InputType switch { UserInputType.Number => "数值", UserInputType.Select => "下拉选择", _ => "文本" };
                AddCell("输入类型", yPosition, 0, c1, false); AddCell(inputType, yPosition, c1, c2, false); yPosition += 22;

                if (!string.IsNullOrEmpty(param.TargetVariableName))
                {
                    AddCell("存入变量", yPosition, 0, c1, false); AddCell($"{{{param.TargetVariableName}}}", yPosition, c1, c2, false, Color.FromArgb(0, 102, 204)); yPosition += 22;
                }

                if (param.InputType == UserInputType.Number && (param.MinValue.HasValue || param.MaxValue.HasValue))
                {
                    string range = $"{(param.MinValue.HasValue ? param.MinValue.Value.ToString() : "不限")} ~ {(param.MaxValue.HasValue ? param.MaxValue.Value.ToString() : "不限")}";
                    AddCell("数值范围", yPosition, 0, c1, false); AddCell(range, yPosition, c1, c2, false); yPosition += 22;
                }

                if (param.TimeoutSeconds > 0)
                {
                    AddCell("超时时间", yPosition, 0, c1, false); AddCell($"{param.TimeoutSeconds} 秒", yPosition, c1, c2, false); yPosition += 22;
                }
                return yPosition;
            }
            catch { return DisplayGeneric(stepParameter, yPosition); }
        }

        #endregion

        #region 检测工具（JSON）

        private int DisplayDetectionTool(object stepParameter, int yPosition)
        {
            try
            {
                var json = ParseJson(stepParameter);
                var (c1, c2, y) = BeginTable(yPosition, "检测工具配置");
                yPosition = y;

                AddCell("检测名称", yPosition, 0, c1, false); AddCell(json["DetectionName"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                AddCell("条件表达式", yPosition, 0, c1, false); AddCell(json["ConditionExpression"]?.ToString() ?? "", yPosition, c1, c2, false, Color.FromArgb(0, 102, 204)); yPosition += 22;

                var ds = json["DataSource"];
                if (ds != null)
                {
                    string srcType = ds["SourceType"]?.ToString() ?? "0";
                    bool isVar = srcType == "0" || srcType.Contains("Variable");
                    AddCell("数据源类型", yPosition, 0, c1, false); AddCell(isVar ? "全局变量" : "PLC地址", yPosition, c1, c2, false); yPosition += 22;
                    if (isVar)
                    {
                        AddCell("变量名称", yPosition, 0, c1, false); AddCell(ds["VariableName"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                    }
                    else
                    {
                        var plc = ds["PlcConfig"];
                        if (plc != null)
                        {
                            AddCell("PLC模块", yPosition, 0, c1, false); AddCell(plc["ModuleName"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                            AddCell("点位地址", yPosition, 0, c1, false); AddCell(plc["Address"]?.ToString() ?? "", yPosition, c1, c2, false); yPosition += 22;
                        }
                    }
                }

                string timeout = json["TimeoutMs"]?.ToString() ?? "0";
                AddCell("超时时间", yPosition, 0, c1, false); AddCell(timeout == "0" ? "不限制" : $"{timeout} ms", yPosition, c1, c2, false); yPosition += 22;
                AddCell("刷新频率", yPosition, 0, c1, false); AddCell($"{json["RefreshRateMs"]?.ToString() ?? "100"} ms", yPosition, c1, c2, false); yPosition += 22;

                string retry = json["RetryCount"]?.ToString() ?? "0";
                if (retry != "0") { AddCell("重试次数", yPosition, 0, c1, false); AddCell($"{retry} 次", yPosition, c1, c2, false); yPosition += 22; }

                return yPosition;
            }
            catch { return DisplayGeneric(stepParameter, yPosition); }
        }

        #endregion

        #region 仪器通讯

        private int DisplayInstrumentCommunication(object stepParameter, int yPosition)
        {
            try
            {
                var json = ParseJson(stepParameter);
                int c1 = 110, c2 = _detailsPanel.Width - c1 - 10;

                yPosition = AddSubTitle("仪表通讯配置", yPosition);
                AddCell("配置项", yPosition, 0, c1, true); AddCell("配置值", yPosition, c1, c2, true); yPosition += 25;

                AddCell("仪器名称", yPosition, 0, c1, false);
                AddCell(json["InstrumentName"]?.ToString() ?? "(未选择)", yPosition, c1, c2, false, Color.FromArgb(65, 100, 204));
                yPosition += 22;

                bool useCustom = json["UseCustomCommand"]?.Value<bool>() ?? false;
                if (useCustom)
                {
                    AddCell("命令类型", yPosition, 0, c1, false); AddCell("自定义命令", yPosition, c1, c2, false); yPosition += 22;
                    string cmd = json["CustomCommand"]?.ToString() ?? "", dtype = json["CustomCommandDataType"]?.ToString() ?? "String";
                    AddCell("命令内容", yPosition, 0, c1, false); AddCell($"{cmd} ({dtype})", yPosition, c1, c2, false, Color.FromArgb(100, 100, 100)); yPosition += 22;
                }
                else
                {
                    AddCell("预定义命令", yPosition, 0, c1, false); AddCell(json["CommandName"]?.ToString() ?? "(未选择)", yPosition, c1, c2, false, Color.FromArgb(0, 102, 204)); yPosition += 22;
                }

                yPosition = AddSubTitle("通讯参数", yPosition);
                int timeout = json["Timeout"]?.Value<int>() ?? 5000;
                AddCell("超时时间", yPosition, 0, c1, false); AddCell($"{timeout} ms", yPosition, c1, c2, false); yPosition += 22;
                int retry = json["RetryCount"]?.Value<int>() ?? 0;
                if (retry > 0) { AddCell("重试次数", yPosition, 0, c1, false); AddCell($"{retry} 次", yPosition, c1, c2, false); yPosition += 22; }
                string strategy = json["FailureStrategy"]?.ToString() ?? "Abort";
                AddCell("失败策略", yPosition, 0, c1, false);
                AddCell(strategy switch { "Abort" => "终止测试", "Continue" => "继续执行", "Retry" => "自动重试", _ => strategy }, yPosition, c1, c2, false);
                yPosition += 22;

                string responseVar = json["ResponseVariable"]?.ToString();
                string statusVar = json["StatusVariable"]?.ToString();
                string errorVar = json["ErrorVariable"]?.ToString();
                if (!string.IsNullOrEmpty(responseVar) || !string.IsNullOrEmpty(statusVar) || !string.IsNullOrEmpty(errorVar))
                {
                    yPosition = AddSubTitle("数据保存", yPosition);
                    if (!string.IsNullOrEmpty(responseVar)) { AddCell("响应数据→", yPosition, 0, c1, false); AddCell(responseVar, yPosition, c1, c2, false, Color.FromArgb(40, 167, 69)); yPosition += 22; }
                    if (!string.IsNullOrEmpty(statusVar)) { AddCell("执行状态→", yPosition, 0, c1, false); AddCell(statusVar, yPosition, c1, c2, false, Color.FromArgb(40, 167, 69)); yPosition += 22; }
                    if (!string.IsNullOrEmpty(errorVar)) { AddCell("错误信息→", yPosition, 0, c1, false); AddCell(errorVar, yPosition, c1, c2, false, Color.FromArgb(220, 53, 69)); yPosition += 22; }
                }

                string actualResp = json["ActualResponse"]?.ToString();
                int? execTime = json["ExecutionTime"]?.Value<int>();
                string actualErr = json["ActualError"]?.ToString();
                if (!string.IsNullOrEmpty(actualResp) || execTime.HasValue || !string.IsNullOrEmpty(actualErr))
                {
                    yPosition = AddSeparator(yPosition);
                    yPosition = AddSubTitle("运行详情", yPosition);
                    AddCell("实际情况", yPosition, 0, c1, true); AddCell("实际值", yPosition, c1, c2, true); yPosition += 25;
                    if (!string.IsNullOrEmpty(actualResp)) { AddCell("返回数据", yPosition, 0, c1, false); AddCell(actualResp.Length > 100 ? actualResp[..100] + "..." : actualResp, yPosition, c1, c2, false, StatusColors.Success); yPosition += 22; }
                    if (execTime.HasValue) { AddCell("执行耗时", yPosition, 0, c1, false); AddCell($"{execTime.Value} ms", yPosition, c1, c2, false); yPosition += 22; }
                    if (!string.IsNullOrEmpty(actualErr)) { AddCell("错误详情", yPosition, 0, c1, false); AddCell(actualErr, yPosition, c1, c2, false, StatusColors.Failed); yPosition += 22; }
                }
                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayInstrumentCommunication 错误: {ex}");
                return DisplayGeneric(stepParameter, yPosition);
            }
        }

        #endregion

        #region 通用展示

        public int DisplayGeneric(object stepParameter, int yPosition)
        {
            try
            {
                yPosition = AddSubTitle("参数详情", yPosition);
                string jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                int c1 = 150, c2 = _detailsPanel.Width - c1 - 10;
                AddCell("参数名", yPosition, 0, c1, true); AddCell("参数值", yPosition, c1, c2, true); yPosition += 25;

                foreach (var prop in json.Properties())
                {
                    AddCell(GetChineseName(prop.Name), yPosition, 0, c1, false);
                    AddCell(prop.Value?.ToString() ?? "", yPosition, c1, c2, false);
                    yPosition += 22;
                }
                if (!json.Properties().Any())
                {
                    AddCell("", yPosition, 0, c1, false); AddCell("空参数", yPosition, c1, c2, false, Color.FromArgb(150, 150, 150)); yPosition += 22;
                }
                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayGeneric 错误: {ex}");
                return AddLine("解析错误", ex.Message, yPosition, StatusColors.Failed);
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 开始一个标准两列表格，返回列宽和新 yPosition
        /// </summary>
        private (int c1, int c2, int y) BeginTable(int yPosition, string subtitle = null, int col1Width = 120)
        {
            if (!string.IsNullOrEmpty(subtitle)) yPosition = AddSubTitle(subtitle, yPosition);
            int c2 = _detailsPanel.Width - col1Width - 10;
            AddCell("配置项", yPosition, 0, col1Width, true);
            AddCell("配置值", yPosition, col1Width, c2, true);
            return (col1Width, c2, yPosition + 25);
        }

        private int AddSubTitle(string title, int yPosition)
        {
            var lbl = new Label
            {
                Text = title,
                Font = new Font("微软雅黑", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(0, yPosition),
                Padding = new Padding(5, 3, 0, 3),
            };
            _detailsPanel.Controls.Add(lbl);
            return yPosition + 22;
        }

        public int AddSectionTitle(string title, int yPosition)
        {
            var lbl = new Label
            {
                Text = title,
                Font = new Font("微软雅黑", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 144, 255),
                AutoSize = true,
                Location = new Point(0, yPosition),
                Padding = new Padding(5, 5, 0, 5),
            };
            _detailsPanel.Controls.Add(lbl);
            return yPosition + 26;
        }

        public int AddLine(string label, string value, int yPosition, Color? valueColor = null)
        {
            var lbl = new Label
            {
                Text = string.IsNullOrEmpty(label) ? value : $"{label}: {value}",
                Font = new Font("微软雅黑", 8.5F),
                ForeColor = valueColor ?? Color.FromArgb(96, 96, 96),
                Location = new Point(0, yPosition),
                MaximumSize = new Size(_detailsPanel.Width, 0),
                AutoSize = true,
                Padding = new Padding(5, 2, 0, 2),
            };
            _detailsPanel.Controls.Add(lbl);
            return yPosition + lbl.Height + 2;
        }

        public int AddMultilineBlock(string label, string content, int yPosition, Color textColor)
        {
            var lblLabel = new Label { Text = $"{label}:", Font = new Font("微软雅黑", 8.5F, FontStyle.Bold), ForeColor = textColor, AutoSize = true, Location = new Point(5, yPosition) };
            _detailsPanel.Controls.Add(lblLabel);
            yPosition += 20;

            var lblContent = new Label
            {
                Text = content,
                Font = new Font("微软雅黑", 8.5F),
                ForeColor = textColor,
                AutoSize = true,
                Location = new Point(5, yPosition),
                Size = new Size(_detailsPanel.Width - 10, 0),
                MaximumSize = new Size(_detailsPanel.Width - 10, 0),
                Padding = new Padding(8, 6, 8, 6),
                BackColor = Color.Transparent,
            };
            _detailsPanel.Controls.Add(lblContent);
            return yPosition + lblContent.Height + 10;
        }

        public int AddSeparator(int yPosition)
        {
            yPosition += 5;
            var sep = new System.Windows.Forms.Panel
            {
                Location = new Point(10, yPosition),
                Size = new Size(_detailsPanel.Width - 20, 1),
                BackColor = Color.FromArgb(230, 230, 230)
            };
            _detailsPanel.Controls.Add(sep);
            return yPosition + 6;
        }

        private void AddCell(string text, int y, int x, int width, bool isHeader, Color? textColor = null)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("微软雅黑", isHeader ? 9F : 8.5F, isHeader ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = textColor ?? (isHeader ? Color.FromArgb(24, 144, 255) : Color.FromArgb(80, 80, 80)),
                Location = new Point(x, y),
                Size = new Size(width, isHeader ? 22 : 20),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 5, 0),
                AutoEllipsis = true
            };
            _detailsPanel.Controls.Add(lbl);
        }

        private T Convert<T>(object stepParameter) where T : class
        {
            if (stepParameter == null) return null;
            if (stepParameter is T direct) return direct;
            try
            {
                string json = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"参数转换失败: {typeof(T).Name}, {ex.Message}");
                return null;
            }
        }

        private JObject ParseJson(object stepParameter)
        {
            string jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
            return JObject.Parse(jsonStr);
        }

        private string GetExpressionDescription(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return string.Empty;
            try
            {
                if (expression.Contains("Math.Abs")) return "容差检测";
                if (expression.Contains("&&")) return "多条件AND";
                if (expression.Contains("||")) return "多条件OR";
                if (expression.Contains(">=") && expression.Contains("<=")) return "范围检测";
                if (expression.Contains(">=")) return "大于等于";
                if (expression.Contains("<=")) return "小于等于";
                if (expression.Contains(">")) return "大于";
                if (expression.Contains("<")) return "小于";
                if (expression.Contains("==")) return "等于";
                if (expression.Contains("!=")) return "不等于";
                return "自定义表达式";
            }
            catch { return string.Empty; }
        }

        private string GetChineseName(string name) => name switch
        {
            "SheetName" => "工作表",
            "CellAddress" => "单元格地址",
            "ReportName" => "报表名称",
            "TargetVariable" => "目标变量",
            "TargetVarName" => "目标变量",
            "VariableName" => "变量名",
            "Variable" => "变量",
            "VarName" => "变量名",
            "AssignmentType" => "赋值方式",
            "Expression" => "表达式",
            "Value" => "值",
            "FixedValue" => "固定值",
            "Condition" => "条件",
            "TrueStepIndex" => "为真跳转",
            "FalseStepIndex" => "为假跳转",
            "ModuleName" => "模块名称",
            "Address" => "点位地址",
            "Duration" => "时长",
            "Timeout" => "超时时间",
            "Description" => "说明",
            "IsEnabled" => "是否启用",
            "Source" => "数据源",
            "SourceType" => "数据源类型",
            _ => name
        };

        #endregion
    }
}
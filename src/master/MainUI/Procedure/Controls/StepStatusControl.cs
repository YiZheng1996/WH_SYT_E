using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.Parameter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Label = AntdUI.Label;
using Panel = Sunny.UI.UIPanel;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 步骤状态控件
    /// 支持根据步骤类型动态显示参数和结果信息
    /// </summary>
    public class StepStatusControl : Panel
    {
        #region 私有字段

        private readonly Panel statusIndicator;        // 左侧状态条
        private readonly Panel contentPanel;           // 内容面板
        private readonly Panel circlePanel;            // 圆形序号面板
        private readonly Label lblStepName;            // 步骤名称
        private readonly Label lblStepStatus;          // 步骤状态
        private readonly Label lblStepTime;            // 执行时间
        private readonly Panel separatorLine;          // 分隔线
        private readonly AntdUI.Progress progressBar;  // 进度条(延时步骤用)
        private readonly Panel detailsPanel;           // 详情面板

        private int stepNumber;
        private string currentStatus = "waiting";
        private ChildModel currentStepData;            // 当前步骤数据

        #endregion

        #region 颜色定义

        private static class StatusColors
        {
            public static readonly Color Waiting = ColorTranslator.FromHtml("#C4C7CC");
            public static readonly Color Running = ColorTranslator.FromHtml("#1890FF");
            public static readonly Color Success = ColorTranslator.FromHtml("#52C41A");
            public static readonly Color Failed = ColorTranslator.FromHtml("#E73624");
            public static readonly Color Skipped = ColorTranslator.FromHtml("#FAAD14");
        }

        private static class BackgroundColors
        {
            public static readonly Color Waiting = ColorTranslator.FromHtml("#FAFAFA");
            public static readonly Color Running = ColorTranslator.FromHtml("#E6F4FF");
            public static readonly Color Success = ColorTranslator.FromHtml("#F0FFF4");
            public static readonly Color Failed = ColorTranslator.FromHtml("#FFF1F0");
            public static readonly Color Skipped = ColorTranslator.FromHtml("#FFFBE6");
        }

        #endregion

        #region 构造函数

        public StepStatusControl(int stepNumber, string stepName)
        {
            this.stepNumber = stepNumber;

            // 主面板设置
            Height = 85;
            Width = 860;
            BackColor = BackgroundColors.Waiting;
            Margin = new Padding(0, 0, 0, 12);

            // 状态指示条(左侧5px)
            statusIndicator = new Panel
            {
                Width = 5,
                Dock = DockStyle.Left,
                FillColor = StatusColors.Waiting,
                RectColor = StatusColors.Waiting,
                BackColor = StatusColors.Waiting
            };
            Controls.Add(statusIndicator);

            // 内容面板
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(15, 12, 15, 12),
                FillColor = BackgroundColors.Waiting,
                RectColor = StatusColors.Waiting,
            };
            Controls.Add(contentPanel);

            // 圆形序号徽章
            circlePanel = new Panel
            {
                Size = new Size(32, 32),
                Location = new Point(15, 12),
                BackColor = Color.Transparent,
                RectColor = Color.Transparent
            };
            circlePanel.Paint += CirclePanel_Paint;
            contentPanel.Controls.Add(circlePanel);

            // 步骤名称
            lblStepName = new Label
            {
                Text = stepName,
                Font = new Font("微软雅黑", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(60, 10)
            };
            contentPanel.Controls.Add(lblStepName);

            // 步骤状态
            lblStepStatus = new Label
            {
                Text = "● 等待中",
                Font = new Font("微软雅黑", 9F),
                ForeColor = StatusColors.Waiting,
                AutoSize = true,
                Location = new Point(60, 35)
            };
            contentPanel.Controls.Add(lblStepStatus);

            // 执行时间(右上角)
            lblStepTime = new Label
            {
                Text = "⏱ 00:00:00",
                Font = new Font("微软雅黑", 10F),
                ForeColor = Color.FromArgb(140, 140, 140),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            contentPanel.Controls.Add(lblStepTime);
            UpdateTimePosition();

            // 分隔线(默认隐藏)
            separatorLine = new Panel
            {
                Height = 1,
                BackColor = StatusColors.Waiting,
                Location = new Point(15, 65),
                Visible = false
            };
            contentPanel.Controls.Add(separatorLine);

            // 详情面板(默认隐藏)
            detailsPanel = new Panel
            {
                Location = new Point(15, 73),
                BackColor = Color.Transparent,
                //Margin = new Padding(5),
                Padding = new Padding(5),
                AutoSize = false,
                Visible = false
            };
            contentPanel.Controls.Add(detailsPanel);

            // 进度条(延时步骤用,默认隐藏)
            progressBar = new AntdUI.Progress
            {
                Location = new Point(15, 0),
                Height = 15,
                Visible = false,
                ForeColor = StatusColors.Running,
                Radius = 3
            };
            contentPanel.Controls.Add(progressBar);

            // 监听尺寸变化
            contentPanel.Resize += (s, e) => UpdateTimePosition();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 更新步骤状态
        /// </summary>
        public void UpdateStatus(string status, ChildModel stepData = null, string message = "")
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, ChildModel, string>(UpdateStatus), status, stepData, message);
                return;
            }

            currentStatus = status.ToLower();
            currentStepData = stepData;

            Debug.WriteLine($"UpdateStatus - Status: {status}, StepData: {stepData?.StepName}");

            Color statusColor;
            Color bgColor;
            string statusText;
            bool showDetails = false;

            switch (currentStatus)
            {
                case "running":
                case "执行中":
                    statusColor = StatusColors.Running;
                    bgColor = BackgroundColors.Running;
                    statusText = string.IsNullOrEmpty(message) ? "▶ 执行中" : $"▶ 执行中 - {message}";
                    showDetails = true;
                    break;

                case "success":
                case "completed":
                case "成功":
                    statusColor = StatusColors.Success;
                    bgColor = BackgroundColors.Success;
                    statusText = "✓ 已完成";
                    showDetails = true;
                    break;

                case "failed":
                case "error":
                case "失败":
                    statusColor = StatusColors.Failed;
                    bgColor = BackgroundColors.Failed;
                    statusText = string.IsNullOrEmpty(message) ? "✕ 失败" : $"✕ 失败 - {message}";
                    showDetails = true;
                    break;

                case "skipped":
                case "跳过":
                    statusColor = StatusColors.Skipped;
                    bgColor = BackgroundColors.Skipped;
                    statusText = string.IsNullOrEmpty(message) ? "⊘ 已跳过" : $"⊘ 已跳过 - {message}";
                    showDetails = true;
                    break;

                default:
                    statusColor = StatusColors.Waiting;
                    bgColor = BackgroundColors.Waiting;
                    statusText = "● 等待中";
                    showDetails = false;
                    break;
            }


            detailsPanel.RectColor = statusColor;
            detailsPanel.FillColor = bgColor;
            SetPanelColor(statusIndicator, statusColor);
            SetPanelColor(separatorLine, statusColor);
            SetPanelColor(contentPanel, bgColor);
            // 更新UI
            separatorLine.BackColor = statusColor;
            statusIndicator.FillColor = statusColor;
            statusIndicator.RectColor = statusColor;
            statusIndicator.BackColor = statusColor;
            BackColor = bgColor;
            contentPanel.FillColor = bgColor;
            contentPanel.RectColor = statusColor;
            lblStepStatus.Text = statusText;
            lblStepStatus.ForeColor = statusColor;
            circlePanel.Invalidate();

            // 显示或隐藏详情
            if (showDetails && stepData != null)
            {
                ShowDetails(stepData);
            }
            else
            {
                HideDetails();
            }
        }

        /// <summary>
        /// 颜色状态更新
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="color"></param>
        private void SetPanelColor(UIPanel panel, Color color)
        {
            panel.BackColor = Color.Transparent;
            panel.FillColor = color;
            panel.FillColor2 = color;
            panel.RectColor = color;
            panel.RectDisableColor = color;
        }

        /// <summary>
        /// 更新执行时间
        /// </summary>
        public void UpdateTime(TimeSpan elapsed)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<TimeSpan>(UpdateTime), elapsed);
                return;
            }

            lblStepTime.Text = $"⏱ {elapsed:hh\\:mm\\:ss}";
            UpdateTimePosition();
        }

        /// <summary>
        /// 更新进度(延时步骤专用)
        /// </summary>
        public void UpdateProgress(int current, int total)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, int>(UpdateProgress), current, total);
                return;
            }

            if (total > 0 && progressBar.Visible)
            {
                float percentage = Math.Min(1.0f, (float)current / total);
                progressBar.Value = percentage;
                int percentageDisplay = (int)(percentage * 100);
                progressBar.Text = $"{percentageDisplay}% ({current}/{total}秒)";
            }
        }

        #endregion

        #region 详情展示 - 核心方法

        /// <summary>
        /// 显示详情信息
        /// </summary>
        private void ShowDetails(ChildModel stepData)
        {
            detailsPanel.Controls.Clear();

            separatorLine.Visible = true;
            separatorLine.Width = contentPanel.Width - 30;
            detailsPanel.Visible = true;
            detailsPanel.Width = contentPanel.Width - 30;

            int yPosition = 10;

            // 配置参数
            yPosition = ShowConfigurationParameters(stepData, yPosition);
            yPosition += 8;

            // 运行时信息
            if (currentStatus != "waiting")
            {
                yPosition = ShowRuntimeInfo(stepData, yPosition);
            }

            detailsPanel.Height = yPosition + 10;
            Height = 70 + detailsPanel.Height + 15;

            // 调整进度条位置(如果有)
            if (progressBar.Visible)
            {
                Height += 15;
                progressBar.Location = new Point(15, 73 + detailsPanel.Height + 8);
                progressBar.Width = contentPanel.Width - 30;
            }
        }

        /// <summary>
        /// 隐藏详情
        /// </summary>
        private void HideDetails()
        {
            separatorLine.Visible = false;
            detailsPanel.Visible = false;
            progressBar.Visible = false;
            Height = 85;
        }

        #endregion

        #region 配置参数

        private int ShowConfigurationParameters(ChildModel stepData, int yPosition)
        {
            yPosition = AddSectionTitle("配置参数", yPosition, 0);

            if (stepData?.StepParameter == null)
            {
                yPosition = AddDetailLine("参数状态", "未配置参数", yPosition, 0,
                    detailsPanel.Width, Color.FromArgb(150, 150, 150));
                return yPosition;
            }

            try
            {
                //string stepType = stepData.StepType ?? stepData.StepName ?? "Unknown";
                string stepType = stepData.StepName ?? stepData.StepName ?? "Unknown";
                yPosition = ParseAndDisplayParameters(stepType, stepData.StepParameter, yPosition);
            }
            catch (Exception ex)
            {
                yPosition = AddDetailLine("参数解析", $"解析失败: {ex.Message}", yPosition,
                    0, detailsPanel.Width, StatusColors.Failed);
                Debug.WriteLine($"参数解析异常: {ex}");
            }

            return yPosition;
        }

        private int ParseAndDisplayParameters(string stepType, object stepParameter, int yPosition)
        {
            return stepType switch
            {
                "写入单元格" or "WriteCells" => DisplayWriteCellsParameters(stepParameter, yPosition),
                "变量赋值" or "VariableAssignment" => DisplayVariableAssignmentParameters(stepParameter, yPosition),
                "读取单元格" or "ReadCells" => DisplayReadCellsParameters(stepParameter, yPosition),
                "条件判断" or "Condition" => DisplayConditionParameters(stepParameter, yPosition),
                "延时等待" or "Delay" => DisplayDelayParameters(stepParameter, yPosition),
                "写入PLC" or "WritePLC" => DisplayWritePLCParameters(stepParameter, yPosition),
                "读取PLC" or "ReadPLC" => DisplayReadPLCParameters(stepParameter, yPosition),
                "等待稳定" or "WaitForStable" => DisplayWaitForStableParameters(stepParameter, yPosition),
                "实时监控" => DisplayRealtimeMonitorPromptParameters(stepParameter, yPosition),
                "循环工具" => DisplayLoopParameters(stepParameter, yPosition),
                _ => DisplayGenericParameters(stepParameter, yPosition)
            };
        }

        #endregion

        #region 表格式展示方法

        /// <summary>
        /// 写入单元格参数展示 - 表格式
        /// </summary>
        private int DisplayWriteCellsParameters(object stepParameter, int yPosition)
        {
            try
            {
                var param = ConvertToParameter<Parameter_WriteCells>(stepParameter);
                if (param == null) return DisplayGenericParameters(stepParameter, yPosition);

                yPosition = AddSubSectionTitle("Excel 配置", yPosition);
                yPosition = AddDetailLine("工作表", param.SheetName ?? "Sheet1", yPosition, 0, detailsPanel.Width);
                yPosition += 10;

                if (param.Items?.Count > 0)
                {
                    yPosition = AddSubSectionTitle("写入明细", yPosition);

                    // 定义列宽
                    int col1Width = 100;  // 单元格地址
                    int col2Width = 100;  // 数据来源
                    int col3Width = detailsPanel.Width - col1Width - col2Width - 20;

                    // 表头
                    AddTableCell("单元格地址", yPosition, 0, col1Width, true);
                    AddTableCell("数据来源", yPosition, col1Width, col2Width, true);
                    AddTableCell("内容(根据来源填写)", yPosition, col1Width + col2Width, col3Width, true);
                    yPosition += 25;

                    // 数据行
                    foreach (var item in param.Items)
                    {
                        string sourceTypeName = item.SourceType switch
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

                        AddTableCell(item.CellAddress, yPosition, 0, col1Width, false);
                        AddTableCell(sourceTypeName, yPosition, col1Width, col2Width, false);
                        AddTableCell(content, yPosition, col1Width + col2Width, col3Width, false);
                        yPosition += 22;
                    }
                }

                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayWriteCellsParameters 错误: {ex}");
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 变量赋值参数展示 - 表格式
        /// </summary>
        private int DisplayVariableAssignmentParameters(object stepParameter, int yPosition)
        {
            try
            {
                var param = ConvertToParameter<Parameter_VariableAssignment>(stepParameter);
                if (param == null) return DisplayGenericParameters(stepParameter, yPosition);

                yPosition = AddSubSectionTitle("赋值配置", yPosition);

                // 定义列宽
                int col1Width = 120;  // 配置项
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 目标变量
                AddTableCell("目标变量", yPosition, 0, col1Width, false);
                AddTableCell(param.TargetVarName ?? "未指定", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 赋值方式
                string assignmentTypeName = param.AssignmentType switch
                {
                    VariableAssignmentType.DirectAssignment => "直接赋值",
                    VariableAssignmentType.ExpressionCalculation => "表达式计算",
                    VariableAssignmentType.VariableCopy => "复制变量",
                    VariableAssignmentType.PLCRead => "PLC读取",
                    _ => "未知"
                };
                AddTableCell("赋值方式", yPosition, 0, col1Width, false);
                AddTableCell(assignmentTypeName, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 表达式/值
                if (!string.IsNullOrEmpty(param.Expression))
                {
                    AddTableCell("表达式/值", yPosition, 0, col1Width, false);
                    AddTableCell(param.Expression, yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 执行条件
                if (!string.IsNullOrEmpty(param.Condition))
                {
                    AddTableCell("执行条件", yPosition, 0, col1Width, false);
                    AddTableCell(param.Condition, yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 是否启用
                AddTableCell("是否启用", yPosition, 0, col1Width, false);
                AddTableCell(param.IsAssignment ? "是" : "否", yPosition, col1Width, col2Width, false,
                    param.IsAssignment ? StatusColors.Success : StatusColors.Waiting);
                yPosition += 22;

                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayVariableAssignmentParameters 错误: {ex}");
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 读取单元格参数展示 - 表格式
        /// </summary>
        private int DisplayReadCellsParameters(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                yPosition = AddSubSectionTitle("📊 读取配置", yPosition);

                // 定义列宽
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 工作表
                AddTableCell("工作表", yPosition, 0, col1Width, false);
                AddTableCell(json["SheetName"]?.ToString() ?? "Sheet1", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 单元格地址
                AddTableCell("单元格地址", yPosition, 0, col1Width, false);
                AddTableCell(json["CellAddress"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 目标变量
                AddTableCell("保存到变量", yPosition, 0, col1Width, false);
                AddTableCell(json["TargetVariable"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                return yPosition;
            }
            catch
            {
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        #region 条件判断显示

        #region 条件判断

        /// <summary>
        /// 条件判断参数展示
        /// </summary>
        private int DisplayConditionParameters(object stepParameter, int yPosition)
        {
            try
            {
                Parameter_Detection param = null;

                // 尝试转换参数
                if (stepParameter is Parameter_Detection directParam)
                {
                    param = directParam;
                }
                else if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
                {
                    try
                    {
                        param = JsonConvert.DeserializeObject<Parameter_Detection>(jsonStr);
                    }
                    catch (JsonException)
                    {
                        var json = JObject.Parse(jsonStr);
                        return DisplayConditionParametersFromJson(json, yPosition);
                    }
                }
                else if (stepParameter != null)
                {
                    var jsonStr2 = JsonConvert.SerializeObject(stepParameter);
                    param = JsonConvert.DeserializeObject<Parameter_Detection>(jsonStr2);
                }

                if (param == null)
                {
                    return DisplayGenericParameters(stepParameter, yPosition);
                }

                // 开始显示
                yPosition = AddSubSectionTitle("🔍 检测条件配置", yPosition);

                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 检测名称
                if (!string.IsNullOrEmpty(param.DetectionName))
                {
                    AddTableCell("检测名称", yPosition, 0, col1Width, false);
                    AddTableCell(param.DetectionName, yPosition, col1Width, col2Width, false,
                        Color.FromArgb(0, 102, 204));
                    yPosition += 22;
                }

                // 检测条件（显示表达式）
                yPosition = AddSubSectionTitle("检测条件", yPosition);

                // 显示条件表达式 - 修复之前被截断的代码
                string expression = param.ConditionExpression ?? "{value} >= 0";
                AddTableCell("条件表达式", yPosition, 0, col1Width, false);

                // 表达式可能较长,需要特殊处理
                if (expression.Length > 40)
                {
                    // 长表达式分行显示
                    AddTableCell(string.Concat(expression.AsSpan(0, 40), "..."), yPosition, col1Width, col2Width, false,
                        Color.FromArgb(102, 51, 153));
                    yPosition += 22;

                    // 完整表达式作为附加信息
                    AddTableCell("", yPosition, 0, col1Width, false);
                    AddTableCell($"完整: {expression}", yPosition, col1Width, col2Width, false,
                        Color.FromArgb(100, 100, 100));
                    yPosition += 22;
                }
                else
                {
                    AddTableCell(expression, yPosition, col1Width, col2Width, false,
                        Color.FromArgb(102, 51, 153));
                    yPosition += 22;
                }

                // 显示条件说明 - 添加辅助方法调用
                string expressionDesc = GetExpressionDescription(param.ConditionExpression);
                if (!string.IsNullOrEmpty(expressionDesc))
                {
                    AddTableCell("条件说明", yPosition, 0, col1Width, false);
                    AddTableCell(expressionDesc, yPosition, col1Width, col2Width, false,
                        Color.FromArgb(40, 167, 69));
                    yPosition += 22;
                }
          
                // 超时和重试
                yPosition = AddSubSectionTitle("超时和重试", yPosition);

                AddTableCell("超时时间", yPosition, 0, col1Width, false);
                AddTableCell($"{param.TimeoutMs} 毫秒 ({param.TimeoutMs / 1000.0:F1} 秒)",
                    yPosition, col1Width, col2Width, false);
                yPosition += 22;

                if (param.RetryCount > 0)
                {
                    AddTableCell("重试次数", yPosition, 0, col1Width, false);
                    AddTableCell($"{param.RetryCount} 次", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    AddTableCell("重试间隔", yPosition, 0, col1Width, false);
                    AddTableCell($"{param.RetryIntervalMs} 毫秒", yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                if (param.RefreshRateMs > 0)
                {
                    AddTableCell("刷新频率", yPosition, 0, col1Width, false);
                    AddTableCell($"{param.RefreshRateMs} 毫秒", yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 结果处理
                yPosition = AddSubSectionTitle("结果处理", yPosition);

                // 失败处理
                string failureActionName = param.ResultHandling?.OnFailure switch
                {
                    FailureAction.Continue => "继续执行",
                    FailureAction.Stop => "停止流程",
                    FailureAction.JumpToStep => $"跳转到步骤 {param.ResultHandling.FailureJumpStep}",
                    //FailureAction.Confirm => "等待确认",
                    _ => "未知"
                };
                AddTableCell("失败时", yPosition, 0, col1Width, false);
                Color failureColor = param.ResultHandling?.OnFailure == FailureAction.Stop
                    ? StatusColors.Failed
                    : StatusColors.Waiting;
                AddTableCell(failureActionName, yPosition, col1Width, col2Width, false, failureColor);
                yPosition += 22;

                // 成功处理
                if (param.ResultHandling?.SuccessJumpStep != null && param.ResultHandling.SuccessJumpStep > 0)
                {
                    AddTableCell("成功时", yPosition, 0, col1Width, false);
                    AddTableCell($"跳转到步骤 {param.ResultHandling.SuccessJumpStep}",
                        yPosition, col1Width, col2Width, false, StatusColors.Success);
                    yPosition += 22;
                }

                // 结果保存
                if (param.ResultHandling?.SaveToVariable == true)
                {
                    AddTableCell("保存结果到", yPosition, 0, col1Width, false);
                    AddTableCell(param.ResultHandling.ResultVariableName ?? "(未指定)",
                        yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                if (param.ResultHandling?.SaveValueToVariable == true)
                {
                    AddTableCell("保存数值到", yPosition, 0, col1Width, false);
                    AddTableCell(param.ResultHandling.ValueVariableName ?? "(未指定)",
                        yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 是否显示结果
                if (param.ResultHandling?.ShowResult == true)
                {
                    AddTableCell("显示结果", yPosition, 0, col1Width, false);
                    AddTableCell("✓ 是", yPosition, col1Width, col2Width, false, StatusColors.Success);
                    yPosition += 22;
                }

                yPosition += 5;
                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayConditionParameters 错误: {ex}");
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 获取表达式的简要描述
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns>中文描述</returns>
        private string GetExpressionDescription(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return string.Empty;

            try
            {
                // 容差检测
                if (expression.Contains("Math.Abs"))
                    return "容差检测";

                // 多条件检测
                if (expression.Contains("&&"))
                    return "多条件AND";
                if (expression.Contains("||"))
                    return "多条件OR";

                // 范围检测
                if (expression.Contains(">=") && expression.Contains("<="))
                    return "范围检测";

                // 单一比较
                if (expression.Contains(">="))
                    return "大于等于";
                if (expression.Contains("<="))
                    return "小于等于";
                if (expression.Contains(">"))
                    return "大于";
                if (expression.Contains("<"))
                    return "小于";
                if (expression.Contains("=="))
                    return "等于";
                if (expression.Contains("!="))
                    return "不等于";

                return "自定义表达式";
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        /// <summary>
        /// 显示结果处理配置
        /// </summary>
        private int DisplayResultHandling(ResultHandling handling, int yPosition, int col1Width, int col2Width)
        {
            if (handling == null)
                return yPosition;

            yPosition = AddSubSectionTitle("结果处理", yPosition);

            // 失败处理
            string failureActionName = handling.OnFailure switch
            {
                FailureAction.Continue => "继续执行",
                FailureAction.Stop => "停止流程",
                FailureAction.JumpToStep => "跳转到指定步骤",
                FailureAction.Retry => "重试",
                _ => "未知"
            };
            AddTableCell("失败处理", yPosition, 0, col1Width, false);
            AddTableCell(failureActionName, yPosition, col1Width, col2Width, false);
            yPosition += 22;

            // 保存结果
            if (handling.SaveToVariable)
            {
                AddTableCell("保存结果", yPosition, 0, col1Width, false);
                AddTableCell($"是 → {handling.ResultVariableName}", yPosition, col1Width, col2Width, false);
                yPosition += 22;
            }

            // 保存值
            if (handling.SaveValueToVariable)
            {
                AddTableCell("保存值", yPosition, 0, col1Width, false);
                AddTableCell($"是 → {handling.ValueVariableName}", yPosition, col1Width, col2Width, false);
                yPosition += 22;
            }

            // 跳转步骤
            if (handling.OnFailure == FailureAction.JumpToStep && handling.FailureJumpStep >= 0)
            {
                AddTableCell("失败跳转", yPosition, 0, col1Width, false);
                AddTableCell($"步骤 {handling.FailureJumpStep}", yPosition, col1Width, col2Width, false);
                yPosition += 22;
            }

            if (handling.SuccessJumpStep >= 0)
            {
                AddTableCell("成功跳转", yPosition, 0, col1Width, false);
                AddTableCell($"步骤 {handling.SuccessJumpStep}", yPosition, col1Width, col2Width, false);
                yPosition += 22;
            }

            return yPosition;
        }

        /// <summary>
        /// 从JSON对象显示检测参数（兼容旧格式）
        /// </summary>
        private int DisplayConditionParametersFromJson(JObject json, int yPosition)
        {
            try
            {
                yPosition = AddSubSectionTitle("🔍 检测条件配置", yPosition);

                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 检测名称
                string detectionName = json["DetectionName"]?.ToString();
                if (!string.IsNullOrEmpty(detectionName))
                {
                    AddTableCell("检测名称", yPosition, 0, col1Width, false);
                    AddTableCell(detectionName, yPosition, col1Width, col2Width, false,
                        Color.FromArgb(0, 102, 204));
                    yPosition += 22;
                }

                // 优先显示表达式（新格式）
                string conditionExpression = json["ConditionExpression"]?.ToString();
                if (!string.IsNullOrEmpty(conditionExpression))
                {
                    AddTableCell("条件表达式", yPosition, 0, col1Width, false);
                    AddTableCell(conditionExpression, yPosition, col1Width, col2Width, false,
                        Color.FromArgb(102, 51, 153));
                    yPosition += 22;
                }
                else
                {
                    // 显示旧格式数据
                    string detectionType = json["Type"]?.ToString();
                    if (!string.IsNullOrEmpty(detectionType))
                    {
                        AddTableCell("检测类型(旧)", yPosition, 0, col1Width, false);
                        AddTableCell(detectionType, yPosition, col1Width, col2Width, false,
                            Color.FromArgb(255, 165, 0)); // 橙色表示旧格式
                        yPosition += 22;
                    }

                    var condition = json["Condition"];
                    if (condition != null)
                    {
                        AddTableCell("条件配置(旧)", yPosition, 0, col1Width, false);
                        AddTableCell(condition.ToString(Formatting.None), yPosition, col1Width, col2Width, false,
                            Color.FromArgb(255, 165, 0));
                        yPosition += 22;
                    }
                }

                return yPosition + 5;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DisplayConditionParametersFromJson 错误: {ex}");
                return yPosition;
            }
        }
        #endregion

        /// <summary>
        /// 延时等待参数展示 - 表格式
        /// </summary>
        private int DisplayDelayParameters(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                yPosition = AddSubSectionTitle("延时配置", yPosition);

                // 定义列宽
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 延时时长
                string duration = json["T"]?.ToString() ?? "0";
                AddTableCell("延时时长", yPosition, 0, col1Width, false);
                AddTableCell($"{(duration.ToDouble() / 1000)} 秒", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                return yPosition;
            }
            catch
            {
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 读取PLC参数展示 - 表格式
        /// </summary>
        private int DisplayReadPLCParameters(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                yPosition = AddSubSectionTitle("PLC读取配置", yPosition);

                // 检查是否有Items数组(多项读取)
                var items = json["Items"];
                if (items != null && items.Type == JTokenType.Array && items.HasValues)
                {
                    // 多项读取 - 表格展示
                    int col1Width = 100;  // 模块名称
                    int col2Width = 120;  // 点位地址
                    int col3Width = detailsPanel.Width - col1Width - col2Width - 20;  // 目标变量

                    // 表头
                    AddTableCell("模块名称", yPosition, 0, col1Width, true);
                    AddTableCell("点位地址", yPosition, col1Width, col2Width, true);
                    AddTableCell("目标变量", yPosition, col1Width + col2Width, col3Width, true);
                    yPosition += 25;

                    // 数据行
                    foreach (var item in items)
                    {
                        string moduleName = item["ModuleName"]?.ToString() ?? "";
                        string address = item["Address"]?.ToString() ?? "";
                        string variable = item["TargetVariable"]?.ToString() ?? "";

                        AddTableCell(moduleName, yPosition, 0, col1Width, false);
                        AddTableCell(address, yPosition, col1Width, col2Width, false);
                        AddTableCell(variable, yPosition, col1Width + col2Width, col3Width, false);
                        yPosition += 22;
                    }
                }
                else
                {
                    // 单项读取 - 键值对展示
                    int col1Width = 120;
                    int col2Width = detailsPanel.Width - col1Width - 10;

                    AddTableCell("配置项", yPosition, 0, col1Width, true);
                    AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                    yPosition += 25;

                    AddTableCell("模块名称", yPosition, 0, col1Width, false);
                    AddTableCell(json["ModuleName"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    AddTableCell("点位地址", yPosition, 0, col1Width, false);
                    AddTableCell(json["Address"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    AddTableCell("目标变量", yPosition, 0, col1Width, false);
                    AddTableCell(json["TargetVariable"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                return yPosition;
            }
            catch
            {
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 写入PLC参数展示 - 表格式
        /// </summary>
        private int DisplayWritePLCParameters(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                yPosition = AddSubSectionTitle("PLC写入配置", yPosition);

                // 检查是否有Items数组
                var items = json["Items"];
                if (items != null && items.Type == JTokenType.Array && items.HasValues)
                {
                    // 多项写入
                    int col1Width = 100;  // 模块名称
                    int col2Width = 120;  // 点位地址
                    int col3Width = detailsPanel.Width - col1Width - col2Width - 20;  // 写入值

                    AddTableCell("模块名称", yPosition, 0, col1Width, true);
                    AddTableCell("点位地址", yPosition, col1Width, col2Width, true);
                    AddTableCell("写入值", yPosition, col1Width + col2Width, col3Width, true);
                    yPosition += 25;

                    foreach (var item in items)
                    {
                        string moduleName = item["PlcModuleName"]?.ToString() ?? "";
                        string address = item["PlcKeyName"]?.ToString() ?? "";
                        string value = item["PlcValue"]?.ToString() ?? "";

                        AddTableCell(moduleName, yPosition, 0, col1Width, false);
                        AddTableCell(address, yPosition, col1Width, col2Width, false);
                        AddTableCell(value, yPosition, col1Width + col2Width, col3Width, false);
                        yPosition += 22;
                    }
                }
                else
                {
                    // 单项写入
                    int col1Width = 120;
                    int col2Width = detailsPanel.Width - col1Width - 10;

                    AddTableCell("配置项", yPosition, 0, col1Width, true);
                    AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                    yPosition += 25;

                    AddTableCell("模块名称", yPosition, 0, col1Width, false);
                    AddTableCell(json["ModuleName"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    AddTableCell("点位地址", yPosition, 0, col1Width, false);
                    AddTableCell(json["Address"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    AddTableCell("写入值", yPosition, 0, col1Width, false);
                    AddTableCell(json["Value"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                return yPosition;
            }
            catch
            {
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 等待稳定参数展示 - 表格式
        /// </summary>
        private int DisplayWaitForStableParameters(object stepParameter, int yPosition)
        {
            try
            {
                var param = ConvertToParameter<Parameter_WaitForStable>(stepParameter);
                if (param == null) return DisplayGenericParameters(stepParameter, yPosition);

                yPosition = AddSubSectionTitle("等待稳定配置", yPosition);

                // 定义列宽
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 步骤描述
                if (!string.IsNullOrEmpty(param.Description))
                {
                    AddTableCell("步骤描述", yPosition, 0, col1Width, false);
                    AddTableCell(param.Description, yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 监测源类型
                string monitorSourceType = param.MonitorSourceType == MonitorSourceType.Variable
                    ? "全局变量"
                    : "PLC点位";
                AddTableCell("监测源类型", yPosition, 0, col1Width, false);
                AddTableCell(monitorSourceType, yPosition, col1Width, col2Width, false,
                    param.MonitorSourceType == MonitorSourceType.Variable ? StatusColors.Success : StatusColors.Running);
                yPosition += 22;

                // 监测源详情
                if (param.MonitorSourceType == MonitorSourceType.Variable)
                {
                    // 显示变量名
                    AddTableCell("监测变量", yPosition, 0, col1Width, false);
                    AddTableCell(param.MonitorVariable ?? "(未指定)", yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }
                else
                {
                    // 显示PLC模块和地址
                    AddTableCell("PLC模块", yPosition, 0, col1Width, false);
                    AddTableCell(param.PlcModuleName ?? "(未指定)", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    AddTableCell("PLC地址", yPosition, 0, col1Width, false);
                    AddTableCell(param.PlcAddress ?? "(未指定)", yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 分隔线
                yPosition = AddSeparatorLine(yPosition);

                // 稳定判据小标题
                yPosition = AddSubSectionTitle("稳定判据", yPosition);

                // 稳定阈值
                AddTableCell("稳定阈值", yPosition, 0, col1Width, false);
                AddTableCell($"{param.StabilityThreshold:F4} (单位/秒)", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 采样间隔
                AddTableCell("采样间隔", yPosition, 0, col1Width, false);
                AddTableCell($"{param.SamplingInterval} 秒", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 连续稳定次数
                AddTableCell("连续稳定次数", yPosition, 0, col1Width, false);
                AddTableCell($"{param.StableCount} 次", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 分隔线
                yPosition = AddSeparatorLine(yPosition);

                // 超时配置小标题
                yPosition = AddSubSectionTitle("超时配置", yPosition);

                // 超时时间
                string timeoutDisplay = param.TimeoutSeconds > 0
                    ? $"{param.TimeoutSeconds} 秒"
                    : "无限等待";
                AddTableCell("超时时间", yPosition, 0, col1Width, false);
                AddTableCell(timeoutDisplay, yPosition, col1Width, col2Width, false,
                    param.TimeoutSeconds > 0 ? Color.FromArgb(100, 100, 100) : StatusColors.Waiting);
                yPosition += 22;

                // 超时动作
                string timeoutAction = param.OnTimeout switch
                {
                    TimeoutAction.ContinueAndLog => "继续执行并记录日志",
                    TimeoutAction.StopProcedure => "停止整个流程",
                    TimeoutAction.JumpToStep => $"跳转到步骤 {param.TimeoutJumpToStep}",
                    _ => "未知"
                };
                AddTableCell("超时动作", yPosition, 0, col1Width, false);
                Color actionColor = param.OnTimeout switch
                {
                    TimeoutAction.ContinueAndLog => StatusColors.Success,
                    TimeoutAction.StopProcedure => StatusColors.Failed,
                    TimeoutAction.JumpToStep => StatusColors.Skipped,
                    _ => StatusColors.Waiting
                };
                AddTableCell(timeoutAction, yPosition, col1Width, col2Width, false, actionColor);
                yPosition += 22;

                // 结果处理
                if (!string.IsNullOrEmpty(param.AssignToVariable))
                {
                    yPosition = AddSeparatorLine(yPosition);
                    yPosition = AddSubSectionTitle("结果处理", yPosition);

                    AddTableCell("赋值目标变量", yPosition, 0, col1Width, false);
                    AddTableCell(param.AssignToVariable, yPosition, col1Width, col2Width, false, StatusColors.Success);
                    yPosition += 22;
                }

                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayWaitForStableParameters 错误: {ex}");
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 实时监控提示参数展示
        /// </summary>
        private int DisplayRealtimeMonitorPromptParameters(object stepParameter, int yPosition)
        {
            try
            {
                var param = ConvertToParameter<Parameter_RealtimeMonitorPrompt>(stepParameter);
                if (param == null) return DisplayGenericParameters(stepParameter, yPosition);

                yPosition = AddSubSectionTitle("📺 实时监控提示配置", yPosition);

                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 窗体标题
                AddTableCell("窗体标题", yPosition, 0, col1Width, false);
                AddTableCell(param.Title, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 监测源类型
                string sourceType = param.MonitorSourceType == MonitorSourceType.Variable ? "全局变量" : "PLC点位";
                AddTableCell("监测源类型", yPosition, 0, col1Width, false);
                AddTableCell(sourceType, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 监测源
                string source = param.MonitorSourceType == MonitorSourceType.Variable
                    ? param.MonitorVariable
                    : $"{param.PlcModuleName}.{param.PlcAddress}";
                AddTableCell("监测源", yPosition, 0, col1Width, false);
                AddTableCell(source, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 提示信息
                AddTableCell("提示信息", yPosition, 0, col1Width, false);
                AddTableCell(param.PromptMessage.Replace("\n", " "), yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // === 新增显示 ===

                // 数值单位
                if (!string.IsNullOrEmpty(param.Unit))
                {
                    AddTableCell("数值单位", yPosition, 0, col1Width, false);
                    AddTableCell(param.Unit, yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 显示格式
                AddTableCell("显示格式", yPosition, 0, col1Width, false);
                AddTableCell(param.DisplayFormat ?? "F1", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 刷新间隔
                AddTableCell("刷新间隔", yPosition, 0, col1Width, false);
                AddTableCell($"{param.RefreshInterval} 毫秒", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 按钮文本
                AddTableCell("按钮文本", yPosition, 0, col1Width, false);
                AddTableCell(param.ButtonText ?? "确定", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 超时设置
                if (param.TimeoutSeconds > 0)
                {
                    AddTableCell("超时时间", yPosition, 0, col1Width, false);
                    AddTableCell($"{param.TimeoutSeconds} 秒", yPosition, col1Width, col2Width, false,
                        Color.FromArgb(255, 165, 0));
                    yPosition += 22;
                }

                // 数值标签
                if (param.ShowValueLabel && !string.IsNullOrEmpty(param.ValueLabelText))
                {
                    AddTableCell("数值标签", yPosition, 0, col1Width, false);
                    AddTableCell(param.ValueLabelText, yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayRealtimeMonitorPromptParameters 错误: {ex}");
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 循环参数展示 - 表格式
        /// </summary>
        private int DisplayLoopParameters(object stepParameter, int yPosition)
        {
            try
            {
                // 尝试转换为强类型参数
                var param = ConvertToParameter<Parameter_Loop>(stepParameter);

                if (param == null)
                {
                    // 如果转换失败,尝试JSON解析
                    var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                    var json = JObject.Parse(jsonStr);

                    yPosition = AddSubSectionTitle("循环配置", yPosition);

                    // 定义列宽
                    int col1Width = 120;
                    int col2Width = detailsPanel.Width - col1Width - 10;

                    // 表头
                    AddTableCell("配置项", yPosition, 0, col1Width, true);
                    AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                    yPosition += 25;

                    // 循环次数表达式
                    AddTableCell("循环次数", yPosition, 0, col1Width, false);
                    AddTableCell(json["LoopCountExpression"]?.ToString() ?? "10", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    // 计数器变量
                    bool enableCounter = json["EnableCounter"]?.ToObject<bool>() ?? true;
                    if (enableCounter)
                    {
                        AddTableCell("计数器变量", yPosition, 0, col1Width, false);
                        AddTableCell(json["CounterVariableName"]?.ToString() ?? "LoopIndex", yPosition, col1Width, col2Width, false);
                        yPosition += 22;
                    }

                    // 子步骤数量
                    var childSteps = json["ChildSteps"] as JArray;
                    int childCount = childSteps?.Count ?? 0;
                    AddTableCell("子步骤数量", yPosition, 0, col1Width, false);
                    AddTableCell($"{childCount} 个", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    // 提前退出条件
                    bool enableEarlyExit = json["EnableEarlyExit"]?.ToObject<bool>() ?? false;
                    if (enableEarlyExit)
                    {
                        yPosition = AddSeparatorLine(yPosition);
                        yPosition = AddSubSectionTitle("提前退出配置", yPosition);

                        string exitCondition = json["ExitConditionExpression"]?.ToString() ?? "";
                        AddTableCell("退出条件", yPosition, 0, col1Width, false);
                        AddTableCell(exitCondition, yPosition, col1Width, col2Width, false, StatusColors.Skipped);
                        yPosition += 22;
                    }

                    // 描述信息
                    string description = json["Description"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(description))
                    {
                        yPosition = AddSeparatorLine(yPosition);
                        //yPosition = AddTextContent("描述", description, yPosition);
                    }

                    return yPosition;
                }
                else
                {
                    // 使用强类型参数显示
                    yPosition = AddSubSectionTitle("循环配置", yPosition);

                    int col1Width = 120;
                    int col2Width = detailsPanel.Width - col1Width - 10;

                    // 表头
                    AddTableCell("配置项", yPosition, 0, col1Width, true);
                    AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                    yPosition += 25;

                    // 循环次数表达式
                    AddTableCell("循环次数", yPosition, 0, col1Width, false);
                    AddTableCell(param.LoopCountExpression ?? "10", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    // 计数器变量
                    if (param.EnableCounter)
                    {
                        AddTableCell("计数器变量", yPosition, 0, col1Width, false);
                        AddTableCell(param.CounterVariableName ?? "LoopIndex", yPosition, col1Width, col2Width, false);
                        yPosition += 22;
                    }

                    // 子步骤数量
                    int childCount = param.ChildSteps?.Count ?? 0;
                    AddTableCell("子步骤数量", yPosition, 0, col1Width, false);
                    AddTableCell($"{childCount} 个", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    // 提前退出条件
                    if (param.EnableEarlyExit)
                    {
                        yPosition = AddSeparatorLine(yPosition);
                        yPosition = AddSubSectionTitle("提前退出配置", yPosition);

                        AddTableCell("退出条件", yPosition, 0, col1Width, false);
                        AddTableCell(param.ExitConditionExpression ?? "", yPosition, col1Width, col2Width, false, StatusColors.Skipped);
                        yPosition += 22;

                        // 退出条件说明
                        if (!string.IsNullOrEmpty(param.ExitConditionDescription))
                        {
                            AddTableCell("条件说明", yPosition, 0, col1Width, false);
                            AddTableCell(param.ExitConditionDescription, yPosition, col1Width, col2Width, false);
                            yPosition += 22;
                        }
                    }

                    // 描述信息
                    if (!string.IsNullOrEmpty(param.Description))
                    {
                        yPosition = AddSeparatorLine(yPosition);
                        //yPosition = AddTextContent("描述", param.Description, yPosition);
                    }

                    return yPosition;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayLoopParameters 错误: {ex}");
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 添加分隔线（辅助方法）
        /// </summary>
        private int AddSeparatorLine(int yPosition)
        {
            yPosition += 5; // 上边距

            var separator = new Panel
            {
                Location = new Point(10, yPosition),
                Size = new Size(detailsPanel.Width - 20, 1),
                BackColor = Color.FromArgb(230, 230, 230)
            };
            detailsPanel.Controls.Add(separator);

            yPosition += 6; // 下边距
            return yPosition;
        }

        /// <summary>
        /// 通用参数展示 - 表格式
        /// </summary>
        private int DisplayGenericParameters(object stepParameter, int yPosition)
        {
            try
            {
                yPosition = AddSubSectionTitle("参数详情", yPosition);

                string jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                // 定义列宽
                int col1Width = 150;  // 参数名
                int col2Width = detailsPanel.Width - col1Width - 10;  // 参数值

                // 表头
                AddTableCell("参数名", yPosition, 0, col1Width, true);
                AddTableCell("参数值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 数据行 - 将英文键名转为中文
                foreach (var property in json.Properties())
                {
                    string chineseName = GetChinesePropertyName(property.Name);
                    string value = property.Value?.ToString() ?? "";

                    AddTableCell(chineseName, yPosition, 0, col1Width, false);
                    AddTableCell(value, yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                if (!json.Properties().Any())
                {
                    AddTableCell("", yPosition, 0, col1Width, false);
                    AddTableCell("空参数", yPosition, col1Width, col2Width, false, Color.FromArgb(150, 150, 150));
                    yPosition += 22;
                }

                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayGenericParameters 错误: {ex}");
                return AddDetailLine("解析错误", ex.Message, yPosition, 0, detailsPanel.Width, StatusColors.Failed);
            }
        }

        /// <summary>
        /// 表格单元格
        /// </summary>
        private void AddTableCell(string text, int y, int x, int width, bool isHeader, Color? textColor = null)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("微软雅黑", isHeader ? 9F : 8.5F, isHeader ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = textColor ?? (isHeader ? Color.FromArgb(24, 144, 255) : Color.FromArgb(80, 80, 80)),
                Location = new Point(x, y),
                Size = new Size(width, isHeader ? 22 : 20),
                BackColor = Color.Transparent, //Color.FromArgb(24, 144, 255),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 5, 0),
                AutoEllipsis = true  // 超长文本显示省略号
            };
            detailsPanel.Controls.Add(lbl);
        }

        /// <summary>
        /// 英文参数名转中文
        /// </summary>
        private string GetChinesePropertyName(string englishName)
        {
            return englishName switch
            {
                // Excel相关
                "SheetName" => "工作表",
                "CellAddress" => "单元格地址",
                "Cell" => "单元格",
                "ReportName" => "报表名称",

                // 变量相关
                "TargetVariable" => "目标变量",
                "TargetVarName" => "目标变量",
                "VariableName" => "变量名",
                "Variable" => "变量",
                "VarName" => "变量名",

                // 赋值相关
                "AssignmentType" => "赋值方式",
                "Expression" => "表达式",
                "Value" => "值",
                "FixedValue" => "固定值",

                // 条件相关
                "Condition" => "条件",
                "TrueStepIndex" => "为真跳转",
                "FalseStepIndex" => "为假跳转",

                // PLC相关
                "ModuleName" => "模块名称",
                "Address" => "点位地址",

                // 其他
                "Duration" => "时长",
                "Timeout" => "超时时间",
                "Description" => "说明",
                "IsEnabled" => "是否启用",
                "Source" => "数据源",
                "SourceType" => "数据源类型",

                _ => englishName  // 找不到对应翻译就用英文
            };
        }

        #endregion

        #region 第三层: 运行时信息

        private int ShowRuntimeInfo(ChildModel stepData, int yPosition)
        {
            yPosition = AddSectionTitle("运行时信息", yPosition, 0);

            // 状态信息
            string statusInfo = currentStatus switch
            {
                "running" => "步骤正在执行中...",
                "success" => "执行成功",
                "failed" => "执行失败",
                _ => "等待执行"
            };

            Color statusColor = currentStatus switch
            {
                "success" => StatusColors.Success,
                "failed" => StatusColors.Failed,
                _ => Color.FromArgb(96, 96, 96)
            };

            yPosition = AddDetailLine("状态", statusInfo, yPosition, 0, detailsPanel.Width, statusColor);

            // 显示错误信息（失败时）
            if (currentStatus == "failed" && !string.IsNullOrEmpty(stepData?.ErrorMessage))
            {
                yPosition = AddMultilineDetailBlock("错误信息", stepData.ErrorMessage, yPosition,
                    StatusColors.Failed);
            }

            // 显示备注
            if (!string.IsNullOrEmpty(stepData?.Remark))
            {
                yPosition = AddMultilineDetailBlock("备注", stepData.Remark, yPosition,
                    Color.FromArgb(96, 96, 96));
            }

            return yPosition;
        }

        #endregion

        #region 辅助方法
        /// <summary>
        /// 添加多行文本块（用于显示较长的文本内容）
        /// </summary>
        private int AddMultilineDetailBlock(string label, string content, int yPosition, Color textColor)
        {
            // 标签
            var lblLabel = new Label
            {
                Text = $"{label}:",
                Font = new Font("微软雅黑", 8.5F, FontStyle.Bold),
                ForeColor = textColor,
                AutoSize = true,
                Location = new Point(5, yPosition)
            };
            detailsPanel.Controls.Add(lblLabel);
            yPosition += 20;

            // 内容（多行显示，带边框）
            var lblContent = new Label
            {
                Text = content,
                Font = new Font("微软雅黑", 8.5F),
                ForeColor = textColor,
                AutoSize = true, // 设置为true让高度自适应
                Location = new Point(5, yPosition),
                Size = new Size(detailsPanel.Width - 10, 0), // 宽度固定，高度自动
                MaximumSize = new Size(detailsPanel.Width - 10, 0),
                Padding = new Padding(8, 6, 8, 6),
                BackColor = Color.Transparent,
            };
            detailsPanel.Controls.Add(lblContent);

            // 返回新的Y坐标（标签高度 + 内边距）
            return yPosition + lblContent.Height + 10;
        }

        // 大表头参数配置等
        private int AddSectionTitle(string title, int yPosition, int xPosition)
        {
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("微软雅黑", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 144, 255),
                AutoSize = true,
                Location = new Point(xPosition, yPosition),
                Padding = new Padding(5, 5, 0, 5),
                //BackColor = Color.Black
            };
            detailsPanel.Controls.Add(lblTitle);
            return yPosition + 26;
        }

        /// <summary>
        /// 表头
        /// </summary>
        /// <returns></returns>
        private int AddSubSectionTitle(string title, int yPosition)
        {
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("微软雅黑", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(0, yPosition),
                Padding = new Padding(5, 3, 0, 3),
            };
            detailsPanel.Controls.Add(lblTitle);
            return yPosition + 22;
        }

        // 内容及运行状态
        private int AddDetailLine(string label, string value, int yPosition, int xPosition,
            int maxWidth, Color? valueColor = null)
        {
            var lblLine = new Label
            {
                Text = string.IsNullOrEmpty(label) ? value : $"{label}: {value}",
                Font = new Font("微软雅黑", 8.5F),
                ForeColor = valueColor ?? Color.FromArgb(96, 96, 96),
                Location = new Point(xPosition, yPosition),
                MaximumSize = new Size(maxWidth, 0),
                AutoSize = true,
                Padding = new Padding(5, 2, 0, 2),
            };
            detailsPanel.Controls.Add(lblLine);
            return yPosition + lblLine.Height + 2;
        }

        private T ConvertToParameter<T>(object stepParameter) where T : class
        {
            if (stepParameter == null) return null;
            if (stepParameter is T directParam) return directParam;

            try
            {
                string jsonString = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"参数转换失败: {typeof(T).Name}, {ex.Message}");
                return null;
            }
        }

        private void UpdateTimePosition()
        {
            if (lblStepTime != null && lblStepTime.Width > 0)
            {
                lblStepTime.Location = new Point(contentPanel.Width - lblStepTime.Width - 15, 10);
            }
        }

        private void InitializeComponent()
        {

        }

        private void CirclePanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Color circleColor = currentStatus switch
            {
                "success" => StatusColors.Success,
                "failed" => StatusColors.Failed,
                "running" => StatusColors.Running,
                "skipped" => StatusColors.Skipped,
                _ => StatusColors.Waiting
            };

            using (var brush = new SolidBrush(circleColor))
            {
                g.FillEllipse(brush, 0, 0, 32, 32);
            }

            string numberText = stepNumber.ToString();
            using (var font = new Font("微软雅黑", 10F, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
            {
                var size = g.MeasureString(numberText, font);
                var x = (32 - size.Width) / 2;
                var y = (32 - size.Height) / 2;
                g.DrawString(numberText, font, brush, x, y);
            }
        }

        #endregion
    }
}
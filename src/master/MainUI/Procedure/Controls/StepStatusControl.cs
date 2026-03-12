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
            Width = 470;
            BackColor = BackgroundColors.Waiting;
            Margin = new Padding(0, 0, 12, 12);

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

            Debug.WriteLine($"更新状态 - 状态: {status}, 步骤数据: {stepData?.StepName}");

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
            if (!progressBar.Visible) return;

            Height += 15;
            progressBar.Location = new Point(15, 73 + detailsPanel.Height + 8);
            progressBar.Width = contentPanel.Width - 30;
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
                "监测工具" or "Condition" => DisplayConditionParameters(stepParameter, yPosition),
                "条件判断" => DisplayConditionToolParameters(stepParameter, yPosition),
                "延时等待" or "Delay" => DisplayDelayParameters(stepParameter, yPosition),
                "写入PLC" or "WritePLC" => DisplayWritePLCParameters(stepParameter, yPosition),
                "读取PLC" or "ReadPLC" => DisplayReadPLCParameters(stepParameter, yPosition),
                "以太网发送" or "EthernetSend" => DisplayEthernetSendParameters(stepParameter, yPosition),
                "串口发送" or "SerialPortSend" => DisplaySerialPortSendParameters(stepParameter, yPosition),
                "等待稳定" or "WaitForStable" => DisplayWaitForStableParameters(stepParameter, yPosition),
                "实时监控" => DisplayRealtimeMonitorPromptParameters(stepParameter, yPosition),
                "循环工具" => DisplayLoopParameters(stepParameter, yPosition),
                "检测工具" => DisplayDetectionToolParameters(stepParameter, yPosition),
                "消息通知" => DisplayMessageNotificationParameters(stepParameter, yPosition),
                "仪器通讯" or "InstrumentCommunication" => DisplayInstrumentCommunicationParameters(stepParameter, yPosition),
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

                yPosition = AddSubSectionTitle("报表配置", yPosition);
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
                var param = ConvertToParameter<Parameter_ReadCells>(stepParameter);
                if (param == null) return DisplayGenericParameters(stepParameter, yPosition);

                yPosition = AddSubSectionTitle("报表读取配置", yPosition);

                // 工作表名称
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                AddTableCell("工作表", yPosition, 0, col1Width, false);
                AddTableCell(param.SheetName ?? "Sheet1", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 检查是否有ReadItems数据
                if (param.ReadItems == null || param.ReadItems.Count == 0)
                {
                    AddTableCell("读取项", yPosition, 0, col1Width, false);
                    AddTableCell("未配置单元格", yPosition, col1Width, col2Width, false,
                        StatusColors.Failed);
                    yPosition += 22;
                    return yPosition;
                }

                // 分隔线
                yPosition = AddSeparatorLine(yPosition);

                // 显示读取项列表 - 三列表格
                yPosition = AddSubSectionTitle("读取项列表", yPosition);

                int cellCol = 100;   // 单元格地址
                int varCol = 120;    // 目标变量
                int typeCol = detailsPanel.Width - cellCol - varCol - 20;  // 数据类型

                // 表头
                AddTableCell("单元格", yPosition, 0, cellCol, true);
                AddTableCell("目标变量", yPosition, cellCol, varCol, true);
                AddTableCell("数据类型", yPosition, cellCol + varCol, typeCol, true);
                yPosition += 25;

                // 数据行 - 显示所有项（如果太多可以限制显示前几项）
                int displayCount = Math.Min(param.ReadItems.Count, 10); // 最多显示10项
                for (int i = 0; i < displayCount; i++)
                {
                    var item = param.ReadItems[i];

                    // 数据类型名称
                    var dataTypeName = item.DataType switch
                    {
                        CellDataType.String => "字符串",
                        CellDataType.Integer => "整数",
                        CellDataType.Decimal => "小数",
                        CellDataType.Boolean => "布尔",
                        CellDataType.DateTime => "日期时间",
                        _ => "字符串"
                    };

                    // 数据类型的颜色
                    var typeColor = item.DataType switch
                    {
                        CellDataType.Integer => Color.FromArgb(0, 102, 204),
                        CellDataType.Decimal => Color.FromArgb(204, 102, 0),
                        CellDataType.Boolean => Color.FromArgb(102, 0, 204),
                        CellDataType.DateTime => Color.FromArgb(0, 153, 76),
                        _ => Color.FromArgb(96, 96, 96)
                    };

                    AddTableCell(item.CellAddress ?? "", yPosition, 0, cellCol, false);
                    AddTableCell(item.SaveToVariable ?? "", yPosition, cellCol, varCol, false,
                        Color.FromArgb(0, 102, 204));
                    AddTableCell(dataTypeName, yPosition, cellCol + varCol, typeCol, false, typeColor);
                    yPosition += 22;
                }

                // 如果有更多项，显示提示
                if (param.ReadItems.Count <= displayCount) return yPosition;

                AddTableCell("", yPosition, 0, cellCol, false);
                AddTableCell($"...还有 {param.ReadItems.Count - displayCount} 项",
                    yPosition, cellCol, varCol + typeCol, false,
                    Color.FromArgb(150, 150, 150));
                yPosition += 22;

                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayReadCellsParameters 错误: {ex}");
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        #region 条件判断显示

        #region 监测工具

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
                yPosition = AddSubSectionTitle("检测条件配置", yPosition);

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

        #region 条件判断显示（Parameter_Condition）

        /// <summary>
        /// 条件判断参数展示 - 使用 Parameter_Condition
        /// 显示条件表达式、满足/不满足条件时的子步骤等信息
        /// </summary>
        private int DisplayConditionToolParameters(object stepParameter, int yPosition)
        {
            try
            {
                // 尝试转换为强类型参数
                var param = ConvertToParameter<Parameter_Condition>(stepParameter);

                if (param == null)
                {
                    // 如果转换失败，尝试JSON解析
                    var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                    var json = JObject.Parse(jsonStr);
                    return DisplayConditionToolParametersFromJson(json, yPosition);
                }

                // 定义列宽
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // ===== 基本信息 =====
                yPosition = AddSubSectionTitle("检测工具配置", yPosition);

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 描述
                if (!string.IsNullOrEmpty(param.Description))
                {
                    AddTableCell("描述", yPosition, 0, col1Width, false);
                    AddTableCell(param.Description, yPosition, col1Width, col2Width, false,
                        Color.FromArgb(0, 102, 204));
                    yPosition += 22;
                }

                // 启用状态
                AddTableCell("启用状态", yPosition, 0, col1Width, false);
                AddTableCell(param.IsEnabled ? "✓ 已启用" : "✗ 已禁用", yPosition, col1Width, col2Width, false,
                    param.IsEnabled ? StatusColors.Success : StatusColors.Skipped);
                yPosition += 22;

                // ===== 条件表达式 =====
                yPosition = AddSeparatorLine(yPosition);
                yPosition = AddSubSectionTitle("条件表达式", yPosition);

                // 条件表达式
                AddTableCell("表达式", yPosition, 0, col1Width, false);
                string expression = param.ConditionExpression ?? "(未设置)";
                AddTableCell(expression, yPosition, col1Width, col2Width, false,
                    string.IsNullOrEmpty(param.ConditionExpression) ? StatusColors.Failed : StatusColors.Waiting);
                yPosition += 22;

                // 表达式说明
                string expressionDesc = GetExpressionDescription(param.ConditionExpression);
                if (!string.IsNullOrEmpty(expressionDesc))
                {
                    AddTableCell("类型", yPosition, 0, col1Width, false);
                    AddTableCell(expressionDesc, yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // ===== 分支配置 =====
                yPosition = AddSeparatorLine(yPosition);
                yPosition = AddSubSectionTitle("执行分支", yPosition);

                // 满足条件时的步骤
                int trueStepsCount = param.TrueSteps?.Count ?? 0;
                AddTableCell("满足条件时", yPosition, 0, col1Width, false);
                AddTableCell($"{trueStepsCount} 个子步骤", yPosition, col1Width, col2Width, false,
                    trueStepsCount > 0 ? StatusColors.Success : StatusColors.Waiting);
                yPosition += 22;

                // 显示满足条件的子步骤名称列表
                if (trueStepsCount > 0)
                {
                    yPosition = DisplayChildStepsList(param.TrueSteps, "  → ", yPosition, col1Width, col2Width);
                }

                // 不满足条件时的步骤
                int falseStepsCount = param.FalseSteps?.Count ?? 0;
                AddTableCell("不满足条件时", yPosition, 0, col1Width, false);
                AddTableCell($"{falseStepsCount} 个子步骤", yPosition, col1Width, col2Width, false,
                    falseStepsCount > 0 ? StatusColors.Skipped : StatusColors.Waiting);
                yPosition += 22;

                // 显示不满足条件的子步骤名称列表
                if (falseStepsCount > 0)
                {
                    yPosition = DisplayChildStepsList(param.FalseSteps, "  → ", yPosition, col1Width, col2Width);
                }

                yPosition += 5;
                return yPosition;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DisplayConditionToolParameters 错误: {ex}");
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 显示子步骤列表
        /// </summary>
        private int DisplayChildStepsList(List<ChildModel> steps, string prefix, int yPosition, int col1Width, int col2Width)
        {
            if (steps == null || steps.Count == 0)
                return yPosition;

            // 最多显示5个子步骤，超过则显示省略
            int displayCount = Math.Min(steps.Count, 5);

            for (int i = 0; i < displayCount; i++)
            {
                var step = steps[i];
                AddTableCell($"{prefix}步骤{i + 1}", yPosition, 0, col1Width, false);
                AddTableCell($"[{step.StepName}] {step.Remark ?? ""}", yPosition, col1Width, col2Width, false,
                    Color.FromArgb(100, 100, 100));
                yPosition += 20;
            }

            if (steps.Count > 5)
            {
                AddTableCell("", yPosition, 0, col1Width, false);
                AddTableCell($"... 还有 {steps.Count - 5} 个步骤", yPosition, col1Width, col2Width, false,
                    Color.FromArgb(150, 150, 150));
                yPosition += 20;
            }

            return yPosition;
        }

        /// <summary>
        /// 从JSON显示检测工具参数（备用方案）
        /// </summary>
        private int DisplayConditionToolParametersFromJson(JObject json, int yPosition)
        {
            // 定义列宽
            int col1Width = 120;
            int col2Width = detailsPanel.Width - col1Width - 10;

            yPosition = AddSubSectionTitle("检测工具配置", yPosition);

            // 表头
            AddTableCell("配置项", yPosition, 0, col1Width, true);
            AddTableCell("配置值", yPosition, col1Width, col2Width, true);
            yPosition += 25;

            // 描述
            string description = json["Description"]?.ToString();
            if (!string.IsNullOrEmpty(description))
            {
                AddTableCell("描述", yPosition, 0, col1Width, false);
                AddTableCell(description, yPosition, col1Width, col2Width, false, Color.FromArgb(0, 102, 204));
                yPosition += 22;
            }

            // 启用状态
            bool isEnabled = json["IsEnabled"]?.ToObject<bool>() ?? true;
            AddTableCell("启用状态", yPosition, 0, col1Width, false);
            AddTableCell(isEnabled ? "✓ 已启用" : "✗ 已禁用", yPosition, col1Width, col2Width, false,
                isEnabled ? StatusColors.Success : StatusColors.Skipped);
            yPosition += 22;

            // 条件表达式
            yPosition = AddSeparatorLine(yPosition);
            yPosition = AddSubSectionTitle("条件表达式", yPosition);

            string expression = json["ConditionExpression"]?.ToString() ?? "(未设置)";
            AddTableCell("表达式", yPosition, 0, col1Width, false);
            AddTableCell(expression, yPosition, col1Width, col2Width, false);
            yPosition += 22;

            // 分支信息
            yPosition = AddSeparatorLine(yPosition);
            yPosition = AddSubSectionTitle("执行分支", yPosition);

            // 满足条件步骤数
            var trueSteps = json["TrueSteps"] as JArray;
            int trueCount = trueSteps?.Count ?? 0;
            AddTableCell("满足条件时", yPosition, 0, col1Width, false);
            AddTableCell($"{trueCount} 个子步骤", yPosition, col1Width, col2Width, false,
                trueCount > 0 ? StatusColors.Success : StatusColors.Waiting);
            yPosition += 22;

            // 不满足条件步骤数
            var falseSteps = json["FalseSteps"] as JArray;
            int falseCount = falseSteps?.Count ?? 0;
            AddTableCell("不满足条件时", yPosition, 0, col1Width, false);
            AddTableCell($"{falseCount} 个子步骤", yPosition, col1Width, col2Width, false,
                falseCount > 0 ? StatusColors.Skipped : StatusColors.Waiting);
            yPosition += 22;

            return yPosition + 5;
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
                var param = ConvertToParameter<Parameter_DelayTime>(stepParameter);
                if (param == null) return DisplayGenericParameters(stepParameter, yPosition);

                yPosition = AddSubSectionTitle("延时配置", yPosition);

                // 定义列宽
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 检查是否使用变量表达式
                if (!string.IsNullOrEmpty(param.DelayValue) && param.DelayValue.Contains("{"))
                {
                    // 变量模式
                    AddTableCell("延时表达式", yPosition, 0, col1Width, false);
                    AddTableCell(param.DelayValue, yPosition, col1Width, col2Width, false,
                        Color.FromArgb(0, 102, 204));
                    yPosition += 22;

                    AddTableCell("当前值", yPosition, 0, col1Width, false);
                    AddTableCell($"{param.T / 1000.0:F1} 秒 ({param.T} ms)", yPosition, col1Width, col2Width, false,
                        Color.FromArgb(100, 100, 100));
                    yPosition += 22;
                }
                else
                {
                    // 固定值模式
                    AddTableCell("延时时长", yPosition, 0, col1Width, false);
                    AddTableCell($"{param.T / 1000.0:F1} 秒 ({param.T} ms)", yPosition, col1Width, col2Width, false);
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
        /// 以太网发送参数展示 - 表格式
        /// </summary>
        private int DisplayEthernetSendParameters(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                yPosition = AddSubSectionTitle("以太网发送配置", yPosition);

                // 定义列宽
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // IP地址
                AddTableCell("IP地址", yPosition, 0, col1Width, false);
                AddTableCell(json["IPAddress"]?.ToString() ?? "192.168.1.100", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 端口
                AddTableCell("端口", yPosition, 0, col1Width, false);
                AddTableCell(json["Port"]?.ToString() ?? "502", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 协议类型
                var protocolValue = json["Protocol"]?.ToString() ?? "Tcp";
                var protocolDisplay = protocolValue.Contains("Udp", StringComparison.OrdinalIgnoreCase) ? "UDP" : "TCP";
                AddTableCell("协议类型", yPosition, 0, col1Width, false);
                AddTableCell(protocolDisplay, yPosition, col1Width, col2Width, false,
                    protocolDisplay == "TCP" ? StatusColors.Success : StatusColors.Running);
                yPosition += 22;

                // 数据格式
                var formatValue = json["DataFormat"]?.ToString() ?? "Text";
                AddTableCell("数据格式", yPosition, 0, col1Width, false);
                AddTableCell(formatValue, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 发送内容
                var content = json["SendContent"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(content))
                {
                    AddTableCell("发送内容", yPosition, 0, col1Width, false);
                    AddTableCell(content.Length > 50 ? content.Substring(0, 50) + "..." : content,
                        yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 等待响应
                var waitResponse = json["WaitResponse"]?.ToObject<bool>() ?? false;
                AddTableCell("等待响应", yPosition, 0, col1Width, false);
                AddTableCell(waitResponse ? "是" : "否", yPosition, col1Width, col2Width, false,
                    waitResponse ? StatusColors.Success : Color.Gray);
                yPosition += 22;

                // 响应变量
                if (waitResponse)
                {
                    var responseVar = json["ResponseVariableName"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(responseVar))
                    {
                        AddTableCell("响应变量", yPosition, 0, col1Width, false);
                        AddTableCell($"@{responseVar}", yPosition, col1Width, col2Width, false, StatusColors.Success);
                        yPosition += 22;
                    }
                }

                return yPosition;
            }
            catch
            {
                return DisplayGenericParameters(stepParameter, yPosition);
            }
        }

        /// <summary>
        /// 串口发送参数展示 - 表格式
        /// </summary>
        private int DisplaySerialPortSendParameters(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                yPosition = AddSubSectionTitle("串口发送配置", yPosition);

                // 定义列宽
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 串口名称
                AddTableCell("串口", yPosition, 0, col1Width, false);
                AddTableCell(json["PortName"]?.ToString() ?? "COM1", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 波特率
                AddTableCell("波特率", yPosition, 0, col1Width, false);
                AddTableCell(json["BaudRate"]?.ToString() ?? "9600", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 数据位
                AddTableCell("数据位", yPosition, 0, col1Width, false);
                AddTableCell(json["DataBits"]?.ToString() ?? "8", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 校验位
                var parityValue = json["Parity"]?.ToString() ?? "None";
                AddTableCell("校验位", yPosition, 0, col1Width, false);
                AddTableCell(parityValue, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 停止位
                var stopBitsValue = json["StopBits"]?.ToString() ?? "One";
                AddTableCell("停止位", yPosition, 0, col1Width, false);
                AddTableCell(stopBitsValue, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 数据格式
                var formatValue = json["DataFormat"]?.ToString() ?? "Text";
                AddTableCell("数据格式", yPosition, 0, col1Width, false);
                AddTableCell(formatValue, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 发送内容
                var content = json["SendContent"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(content))
                {
                    AddTableCell("发送内容", yPosition, 0, col1Width, false);
                    AddTableCell(content.Length > 50 ? content.Substring(0, 50) + "..." : content,
                        yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 等待响应
                var waitResponse = json["WaitResponse"]?.ToObject<bool>() ?? false;
                AddTableCell("等待响应", yPosition, 0, col1Width, false);
                AddTableCell(waitResponse ? "是" : "否", yPosition, col1Width, col2Width, false,
                    waitResponse ? StatusColors.Success : Color.Gray);
                yPosition += 22;

                // 响应变量
                if (waitResponse)
                {
                    var responseVar = json["ResponseVariableName"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(responseVar))
                    {
                        AddTableCell("响应变量", yPosition, 0, col1Width, false);
                        AddTableCell($"@{responseVar}", yPosition, col1Width, col2Width, false, StatusColors.Success);
                        yPosition += 22;
                    }
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
        /// 消息通知参数展示 - 表格式
        /// </summary>
        private int DisplayMessageNotificationParameters(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                yPosition = AddSubSectionTitle("💬 消息通知配置", yPosition);

                // 定义列宽
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 提示标题
                AddTableCell("标题", yPosition, 0, col1Width, false);
                AddTableCell(json["Title"]?.ToString() ?? "提示", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 消息内容
                AddTableCell("消息内容", yPosition, 0, col1Width, false);
                AddTableCell(json["Message"]?.ToString() ?? "", yPosition, col1Width, col2Width, false,
                    Color.FromArgb(0, 102, 204));
                yPosition += 22;

                // 提示等级
                var messageLevel = json["MessageLevel"]?.ToString() ?? "0";
                var levelText = messageLevel switch
                {
                    "0" or "Info" => "ℹ️ 信息",
                    "1" or "Warning" => "⚠️ 警告",
                    "2" or "Error" => "❌ 错误",
                    "3" or "Question" => "❓ 询问",
                    _ => "信息"
                };
                var levelColor = messageLevel switch
                {
                    "1" or "Warning" => StatusColors.Skipped,
                    "2" or "Error" => StatusColors.Failed,
                    "0" or "Info" => StatusColors.Running,
                    "3" or "Question" => Color.FromArgb(102, 102, 255),
                    _ => Color.FromArgb(96, 96, 96)
                };
                AddTableCell("提示等级", yPosition, 0, col1Width, false);
                AddTableCell(levelText, yPosition, col1Width, col2Width, false, levelColor);
                yPosition += 22;

                // 对话框类型
                var dialogType = json["DialogType"]?.ToString() ?? "0";
                var dialogTypeText = dialogType switch
                {
                    "0" or "OK" => "确认",
                    "1" or "YesNo" => "是/否",
                    "2" or "OKCancel" => "确认/取消",
                    _ => "确认"
                };
                AddTableCell("对话框类型", yPosition, 0, col1Width, false);
                AddTableCell(dialogTypeText, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 结果变量（仅在YesNo或OKCancel类型时显示）
                if (dialogType == "1" || dialogType == "YesNo" || dialogType == "2" || dialogType == "OKCancel")
                {
                    var resultVar = json["ResultVariable"]?.ToString();
                    if (!string.IsNullOrEmpty(resultVar))
                    {
                        AddTableCell("结果变量", yPosition, 0, col1Width, false);
                        AddTableCell(resultVar, yPosition, col1Width, col2Width, false,
                            Color.FromArgb(0, 102, 204));
                        yPosition += 22;
                    }
                }

                // 运行时信息：用户响应
                var userResponse = json["UserResponse"]?.ToString();
                if (!string.IsNullOrEmpty(userResponse))
                {
                    yPosition = AddSeparatorLine(yPosition);
                    yPosition = AddSubSectionTitle("📊 运行详情", yPosition);

                    AddTableCell("配置项", yPosition, 0, col1Width, true);
                    AddTableCell("实际值", yPosition, col1Width, col2Width, true);
                    yPosition += 25;

                    // 用户选择结果
                    var responseText = userResponse switch
                    {
                        "1" or "OK" => "✓ 确认",
                        "6" or "Yes" => "✓ 是",
                        "2" or "Cancel" => "✗ 取消",
                        "7" or "No" => "✗ 否",
                        _ => userResponse
                    };
                    var responseColor = (userResponse == "1" || userResponse == "OK" || userResponse == "6" || userResponse == "Yes")
                        ? StatusColors.Success
                        : StatusColors.Failed;

                    AddTableCell("用户选择", yPosition, 0, col1Width, false);
                    AddTableCell(responseText, yPosition, col1Width, col2Width, false, responseColor);
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
        /// 检测工具参数展示 - 表格式
        /// </summary>
        private int DisplayDetectionToolParameters(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                yPosition = AddSubSectionTitle("检测工具配置", yPosition);

                // 定义列宽
                int col1Width = 120;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 检测名称
                AddTableCell("检测名称", yPosition, 0, col1Width, false);
                AddTableCell(json["DetectionName"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 条件表达式
                AddTableCell("条件表达式", yPosition, 0, col1Width, false);
                AddTableCell(json["ConditionExpression"]?.ToString() ?? "", yPosition, col1Width, col2Width, false,
                    Color.FromArgb(0, 102, 204));
                yPosition += 22;

                // 数据源类型
                var dataSource = json["DataSource"];
                if (dataSource != null)
                {
                    var sourceType = dataSource["SourceType"]?.ToString() ?? "0";
                    var sourceTypeName = sourceType == "0" || sourceType.Contains("Variable") ? "全局变量" : "PLC地址";

                    AddTableCell("数据源类型", yPosition, 0, col1Width, false);
                    AddTableCell(sourceTypeName, yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    // 根据数据源类型显示具体信息
                    if (sourceType == "0" || sourceType.Contains("Variable"))
                    {
                        // 变量数据源
                        AddTableCell("变量名称", yPosition, 0, col1Width, false);
                        AddTableCell(dataSource["VariableName"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                        yPosition += 22;
                    }
                    else
                    {
                        // PLC数据源
                        var plcConfig = dataSource["PlcConfig"];
                        if (plcConfig != null)
                        {
                            AddTableCell("PLC模块", yPosition, 0, col1Width, false);
                            AddTableCell(plcConfig["ModuleName"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                            yPosition += 22;

                            AddTableCell("点位地址", yPosition, 0, col1Width, false);
                            AddTableCell(plcConfig["Address"]?.ToString() ?? "", yPosition, col1Width, col2Width, false);
                            yPosition += 22;
                        }
                    }
                }

                // 超时时间
                var timeout = json["TimeoutMs"]?.ToString() ?? "0";
                var timeoutText = timeout == "0" ? "不限制" : $"{timeout} ms";
                AddTableCell("超时时间", yPosition, 0, col1Width, false);
                AddTableCell(timeoutText, yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 刷新频率
                AddTableCell("刷新频率", yPosition, 0, col1Width, false);
                AddTableCell($"{json["RefreshRateMs"]?.ToString() ?? "100"} ms", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 重试次数（如果大于0才显示）
                var retryCount = json["RetryCount"]?.ToString() ?? "0";
                if (retryCount != "0")
                {
                    AddTableCell("重试次数", yPosition, 0, col1Width, false);
                    AddTableCell($"{retryCount} 次", yPosition, col1Width, col2Width, false);
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
        /// 仪表通讯参数展示 - 表格式
        /// 精简显示核心配置和运行时信息
        /// 
        /// 显示策略:
        /// 1. 核心配置: 仪器名称、命令类型、命令内容
        /// 2. 通讯参数: 超时时间、重试次数、失败策略
        /// 3. 数据保存: 响应/状态/错误变量(有值时显示)
        /// 4. 运行详情: 实际响应、执行耗时、错误信息(执行后显示)
        /// </summary>
        private int DisplayInstrumentCommunicationParameters(object stepParameter, int yPosition)
        {
            try
            {
                var jsonStr = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                var json = JObject.Parse(jsonStr);

                // 定义列宽
                int col1Width = 110;
                int col2Width = detailsPanel.Width - col1Width - 10;

                // 第一部分: 仪表通讯配置
                yPosition = AddSubSectionTitle("仪表通讯配置", yPosition);

                // 表头
                AddTableCell("配置项", yPosition, 0, col1Width, true);
                AddTableCell("配置值", yPosition, col1Width, col2Width, true);
                yPosition += 25;

                // 仪器名称 - 主题蓝色突出显示
                var instrumentName = json["InstrumentName"]?.ToString() ?? "(未选择)";
                AddTableCell("仪器名称", yPosition, 0, col1Width, false);
                AddTableCell(instrumentName, yPosition, col1Width, col2Width, false,
                    Color.FromArgb(65, 100, 204)); // 主题蓝色
                yPosition += 22;

                // 命令配置 - 区分自定义命令和预定义命令
                var useCustomCommand = json["UseCustomCommand"]?.Value<bool>() ?? false;
                if (useCustomCommand)
                {
                    // 自定义命令模式
                    AddTableCell("命令类型", yPosition, 0, col1Width, false);
                    AddTableCell("自定义命令", yPosition, col1Width, col2Width, false);
                    yPosition += 22;

                    var customCommand = json["CustomCommand"]?.ToString() ?? "";
                    var customDataType = json["CustomCommandDataType"]?.ToString() ?? "String";

                    // 显示命令内容和数据类型
                    AddTableCell("命令内容", yPosition, 0, col1Width, false);
                    AddTableCell($"{customCommand} ({customDataType})", yPosition, col1Width, col2Width, false,
                        Color.FromArgb(100, 100, 100)); // 灰色
                    yPosition += 22;
                }
                else
                {
                    // 预定义命令模式
                    var commandName = json["CommandName"]?.ToString() ?? "(未选择)";
                    AddTableCell("预定义命令", yPosition, 0, col1Width, false);
                    AddTableCell(commandName, yPosition, col1Width, col2Width, false,
                        Color.FromArgb(0, 102, 204)); // 深蓝色
                    yPosition += 22;
                }

                // 第二部分: 通讯参数
                yPosition = AddSubSectionTitle("通讯参数", yPosition);

                // 超时时间 - 必显示
                var timeout = json["Timeout"]?.Value<int>() ?? 5000;
                AddTableCell("超时时间", yPosition, 0, col1Width, false);
                AddTableCell($"{timeout} ms", yPosition, col1Width, col2Width, false);
                yPosition += 22;

                // 重试次数 - 仅在大于0时显示
                var retryCount = json["RetryCount"]?.Value<int>() ?? 0;
                if (retryCount > 0)
                {
                    AddTableCell("重试次数", yPosition, 0, col1Width, false);
                    AddTableCell($"{retryCount} 次", yPosition, col1Width, col2Width, false);
                    yPosition += 22;
                }

                // 失败策略 - 转换为中文显示
                var failureStrategy = json["FailureStrategy"]?.ToString() ?? "Abort";
                var strategyText = failureStrategy switch
                {
                    "Abort" => "终止测试",
                    "Continue" => "继续执行",
                    "Retry" => "自动重试",
                    _ => failureStrategy
                };
                AddTableCell("失败策略", yPosition, 0, col1Width, false);
                AddTableCell(strategyText, yPosition, col1Width, col2Width, false);
                yPosition += 22;


                // 第三部分: 数据保存(仅在有配置时显示)
                var responseVar = json["ResponseVariable"]?.ToString();
                var statusVar = json["StatusVariable"]?.ToString();
                var errorVar = json["ErrorVariable"]?.ToString();

                if (!string.IsNullOrEmpty(responseVar) || !string.IsNullOrEmpty(statusVar) || !string.IsNullOrEmpty(errorVar))
                {
                    yPosition = AddSubSectionTitle("数据保存", yPosition);

                    // 响应数据保存变量 - 绿色
                    if (!string.IsNullOrEmpty(responseVar))
                    {
                        AddTableCell("响应数据→", yPosition, 0, col1Width, false);
                        AddTableCell(responseVar, yPosition, col1Width, col2Width, false,
                            Color.FromArgb(40, 167, 69)); // 成功绿
                        yPosition += 22;
                    }

                    // 执行状态保存变量 - 绿色
                    if (!string.IsNullOrEmpty(statusVar))
                    {
                        AddTableCell("执行状态→", yPosition, 0, col1Width, false);
                        AddTableCell(statusVar, yPosition, col1Width, col2Width, false,
                            Color.FromArgb(40, 167, 69)); // 成功绿
                        yPosition += 22;
                    }

                    // 错误信息保存变量 - 红色
                    if (!string.IsNullOrEmpty(errorVar))
                    {
                        AddTableCell("错误信息→", yPosition, 0, col1Width, false);
                        AddTableCell(errorVar, yPosition, col1Width, col2Width, false,
                            Color.FromArgb(220, 53, 69)); // 错误红
                        yPosition += 22;
                    }
                }

                // 第四部分: 运行时信息(仅在执行后显示)
                // 注意: 这些字段需要在工具执行时动态添加到参数对象中
                var actualResponse = json["ActualResponse"]?.ToString();
                var executionTime = json["ExecutionTime"]?.Value<int>();
                var actualError = json["ActualError"]?.ToString();

                if (!string.IsNullOrEmpty(actualResponse) || executionTime.HasValue || !string.IsNullOrEmpty(actualError))
                {
                    yPosition = AddSeparatorLine(yPosition);
                    yPosition = AddSubSectionTitle("运行详情", yPosition);

                    AddTableCell("实际情况", yPosition, 0, col1Width, true);
                    AddTableCell("实际值", yPosition, col1Width, col2Width, true);
                    yPosition += 25;

                    // 实际响应数据 - 过长则截断
                    if (!string.IsNullOrEmpty(actualResponse))
                    {
                        // 截断过长的响应数据,避免界面过长
                        var displayResponse = actualResponse.Length > 100
                            ? actualResponse.Substring(0, 100) + "..."
                            : actualResponse;

                        AddTableCell("返回数据", yPosition, 0, col1Width, false);
                        AddTableCell(displayResponse, yPosition, col1Width, col2Width, false,
                            StatusColors.Success); // 成功绿色
                        yPosition += 22;
                    }

                    // 实际执行时间
                    if (executionTime.HasValue)
                    {
                        AddTableCell("执行耗时", yPosition, 0, col1Width, false);
                        AddTableCell($"{executionTime.Value} ms", yPosition, col1Width, col2Width, false);
                        yPosition += 22;
                    }

                    // 错误详情(如果有错误)
                    if (!string.IsNullOrEmpty(actualError))
                    {
                        AddTableCell("错误详情", yPosition, 0, col1Width, false);
                        AddTableCell(actualError, yPosition, col1Width, col2Width, false,
                            StatusColors.Failed); // 失败红色
                        yPosition += 22;
                    }
                }

                return yPosition;
            }
            catch (Exception ex)
            {
                // 解析失败时回退到通用参数显示
                Debug.WriteLine($"DisplayInstrumentCommunicationParameters 错误: {ex}");
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
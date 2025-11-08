using MainUI.LogicalConfiguration;
using Newtonsoft.Json.Linq;
using Label = AntdUI.Label;
using Panel = Sunny.UI.UIPanel;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 步骤状态控件
    /// 支持根据步骤状态动态显示参数和结果信息
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
        private readonly Panel detailsPanel;           // 详情面板
        private readonly AntdUI.Progress progressBar;  // 进度条（延时步骤用）

        private int stepNumber;
        private string currentStatus = "waiting";
        private ChildModel currentStepData;            // 当前步骤数据

        // 状态颜色定义
        private static class StatusColors
        {
            public static readonly Color Waiting = ColorTranslator.FromHtml("#C4C7CC");  // 灰色
            public static readonly Color Running = ColorTranslator.FromHtml("#1890FF");  // 蓝色
            public static readonly Color Success = ColorTranslator.FromHtml("#52C41A");  // 绿色
            public static readonly Color Failed = ColorTranslator.FromHtml("#E73624");   // 红色
            public static readonly Color Skipped = ColorTranslator.FromHtml("#FAAD14");  // 橙色
        }

        // 背景颜色定义
        private static class BackgroundColors
        {
            public static readonly Color Waiting = ColorTranslator.FromHtml("#FAFAFA");  // 灰色浅背景
            public static readonly Color Running = ColorTranslator.FromHtml("#E6F4FF");  // 蓝色浅背景
            public static readonly Color Success = ColorTranslator.FromHtml("#F0FFF4");  // 绿色浅背景
            public static readonly Color Failed = ColorTranslator.FromHtml("#FFF1F0");   // 红色浅背景
            public static readonly Color Skipped = ColorTranslator.FromHtml("#FFFBE6");  // 橙色浅背景
        }

        #endregion

        #region 构造函数

        public StepStatusControl(int stepNumber, string stepName)
        {
            this.stepNumber = stepNumber;

            // 主面板设置（简洁模式高度）
            Height = 85;
            Width = 860;
            BackColor = BackgroundColors.Waiting;
            Margin = new Padding(0, 0, 0, 12);

            // 状态指示条（左侧5px）
            statusIndicator = new Panel
            {
                Width = 5,
                Dock = DockStyle.Left,
                BackColor = StatusColors.Waiting
            };
            Controls.Add(statusIndicator);

            // 内容面板
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(15, 12, 15, 12),
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

            // 执行时间（右上角）
            lblStepTime = new Label
            {
                Text = "",
                Font = new Font("微软雅黑", 9F),
                ForeColor = Color.FromArgb(140, 140, 140),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            contentPanel.Controls.Add(lblStepTime);
            UpdateTimePosition();

            // 分隔线（默认隐藏）
            separatorLine = new Panel
            {
                Height = 1,
                BackColor = Color.FromArgb(232, 232, 232),
                Location = new Point(15, 65),
                Visible = false
            };
            contentPanel.Controls.Add(separatorLine);

            // 详情面板（默认隐藏）
            detailsPanel = new Panel
            {
                Location = new Point(15, 73),
                BackColor = Color.Transparent,
                AutoSize = false,
                Visible = false
            };
            contentPanel.Controls.Add(detailsPanel);

            // 进度条（延时步骤用，默认隐藏）
            progressBar = new AntdUI.Progress
            {
                Location = new Point(15, 0),
                Height = 6,
                Visible = false,
                ForeColor = StatusColors.Running,
                Radius = 3
            };
            contentPanel.Controls.Add(progressBar);

            // 监听尺寸变化以更新时间位置
            contentPanel.Resize += (s, e) => UpdateTimePosition();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 更新步骤状态
        /// </summary>
        public void UpdateStatus(string status, string message = "", ChildModel stepData = null)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, string, ChildModel>(UpdateStatus), status, message, stepData);
                return;
            }

            currentStatus = status.ToLower();
            currentStepData = stepData;

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
                    statusText = string.IsNullOrEmpty(message) ? "⊘ 跳过" : $"⊘ 跳过 - {message}";
                    showDetails = true;
                    break;

                default:
                    statusColor = StatusColors.Waiting;
                    bgColor = BackgroundColors.Waiting;
                    statusText = "● 等待中";
                    showDetails = false;
                    break;
            }

            // 更新UI元素
            statusIndicator.BackColor = statusColor;
            BackColor = bgColor;
            lblStepStatus.ForeColor = statusColor;
            lblStepStatus.Text = statusText;
            circlePanel.Invalidate(); // 触发圆形重绘

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
        /// 更新进度（延时步骤专用）
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
                int percentage = (int)((double)current / total * 100);
                progressBar.Value = percentage;
                progressBar.Text = $"{percentage}% ({current}/{total}秒)";
            }
        }

        #endregion

        #region 私有方法 - 详情显示

        /// <summary>
        /// 显示详情信息
        /// </summary>
        private void ShowDetails(ChildModel stepData)
        {
            // 清空详情面板
            detailsPanel.Controls.Clear();

            // 显示分隔线和详情面板
            separatorLine.Visible = true;
            separatorLine.Width = contentPanel.Width - 30;
            detailsPanel.Visible = true;
            detailsPanel.Width = contentPanel.Width - 30;

            int yPosition = 0;

            // 根据步骤类型显示不同的详情
            string stepType = stepData.StepName ?? "未知";

            yPosition = stepType switch
            {
                "读取PLC" => ShowReadPLCDetails(stepData, yPosition),
                "写入PLC" => ShowWritePLCDetails(stepData, yPosition),
                "延时等待" => ShowDelayDetails(stepData, yPosition),
                "变量赋值" => ShowVariableAssignDetails(stepData, yPosition),
                "条件判断" => ShowConditionDetails(stepData, yPosition),
                "消息通知" => ShowMessageDetails(stepData, yPosition),
                "读取单元格" or "写入单元格" => ShowCellOperationDetails(stepData, yPosition),
                _ => ShowDefaultDetails(stepData, yPosition),
            };

            // 设置详情面板高度
            detailsPanel.Height = yPosition;

            // 调整整体卡片高度
            int newHeight = 85 + yPosition + 20; // 基础高度 + 详情高度 + 底部边距

            // 如果是延时步骤且正在执行，需要额外空间显示进度条
            if (stepType == "延时等待" && currentStatus == "running" && progressBar.Visible)
            {
                newHeight += 20; // 进度条高度 + 间距
            }

            Height = newHeight;

            // 更新进度条位置（如果显示）
            if (progressBar.Visible)
            {
                progressBar.Location = new Point(15, 73 + yPosition + 8);
                progressBar.Width = contentPanel.Width - 30;
            }
        }

        /// <summary>
        /// 隐藏详情信息
        /// </summary>
        private void HideDetails()
        {
            separatorLine.Visible = false;
            detailsPanel.Visible = false;
            progressBar.Visible = false;
            Height = 85; // 恢复简洁模式高度
        }

        /// <summary>
        /// 显示读取PLC详情
        /// </summary>
        private int ShowReadPLCDetails(ChildModel stepData, int yPosition)
        {
            var parameters = ParseStepParameters(stepData);
            var results = ParseStepResults(stepData);

            // 计算双栏宽度
            int leftWidth = (detailsPanel.Width - 20) / 2;

            // 左栏：执行参数
            int leftY = yPosition;
            leftY = AddSectionTitle(" 执行参数", leftY, 0);
            leftY = AddDetailLine("模块名称", parameters.GetValueOrDefault("ModuleName", "N/A"), leftY, 0, leftWidth);
            leftY = AddDetailLine("点位地址", parameters.GetValueOrDefault("Address", "N/A"), leftY, 0, leftWidth);
            leftY = AddDetailLine("目标变量", parameters.GetValueOrDefault("Variable", "N/A"), leftY, 0, leftWidth);

            // 右栏：执行结果（仅完成或失败时显示）
            int rightY = yPosition;
            if (currentStatus == "success" || currentStatus == "failed")
            {
                rightY = AddSectionTitle(" 执行结果", rightY, leftWidth + 10);

                if (currentStatus == "success")
                {
                    rightY = AddDetailLine("读取值", results.GetValueOrDefault("Value", "N/A"), rightY, leftWidth + 10, leftWidth);
                    rightY = AddDetailLine("数据类型", results.GetValueOrDefault("DataType", "REAL"), rightY, leftWidth + 10, leftWidth);
                    rightY = AddDetailLine("状态", "成功 ✓", rightY, leftWidth + 10, leftWidth, StatusColors.Success);
                }
                else
                {
                    rightY = AddDetailLine("错误码", results.GetValueOrDefault("ErrorCode", "E1001"), rightY, leftWidth + 10, leftWidth);
                    rightY = AddDetailLine("错误描述", results.GetValueOrDefault("ErrorMessage", "通信超时"), rightY, leftWidth + 10, leftWidth, StatusColors.Failed);
                }

                return Math.Max(leftY, rightY);
            }

            return leftY;
        }

        /// <summary>
        /// 显示写入PLC详情
        /// </summary>
        private int ShowWritePLCDetails(ChildModel stepData, int yPosition)
        {
            var parameters = ParseStepParameters(stepData);
            var results = ParseStepResults(stepData);

            int leftWidth = (detailsPanel.Width - 20) / 2;

            // 左栏：执行参数
            int leftY = yPosition;
            leftY = AddSectionTitle(" 执行参数", leftY, 0);
            leftY = AddDetailLine("模块名称", parameters.GetValueOrDefault("ModuleName", "N/A"), leftY, 0, leftWidth);
            leftY = AddDetailLine("点位地址", parameters.GetValueOrDefault("Address", "N/A"), leftY, 0, leftWidth);

            string writeValue = parameters.GetValueOrDefault("WriteValue", "N/A");
            string valueSource = parameters.GetValueOrDefault("ValueSource", "");
            if (!string.IsNullOrEmpty(valueSource))
            {
                writeValue = $"{writeValue} (来自: {valueSource})";
            }
            leftY = AddDetailLine("写入值", writeValue, leftY, 0, leftWidth);

            // 右栏：执行结果
            int rightY = yPosition;
            if (currentStatus == "success" || currentStatus == "failed")
            {
                rightY = AddSectionTitle(" 写入结果", rightY, leftWidth + 10);

                if (currentStatus == "success")
                {
                    rightY = AddDetailLine("写入值", results.GetValueOrDefault("WriteValue", writeValue), rightY, leftWidth + 10, leftWidth);
                    rightY = AddDetailLine("确认值", results.GetValueOrDefault("ConfirmValue", writeValue) + " ✓", rightY, leftWidth + 10, leftWidth);
                    rightY = AddDetailLine("状态", "成功", rightY, leftWidth + 10, leftWidth, StatusColors.Success);
                }
                else
                {
                    rightY = AddDetailLine("错误信息", results.GetValueOrDefault("ErrorMessage", "写入失败"), rightY, leftWidth + 10, leftWidth, StatusColors.Failed);
                }

                return Math.Max(leftY, rightY);
            }

            return leftY;
        }

        /// <summary>
        /// 显示延时等待详情
        /// </summary>
        private int ShowDelayDetails(ChildModel stepData, int yPosition)
        {
            var parameters = ParseStepParameters(stepData);
            var results = ParseStepResults(stepData);

            int leftWidth = (detailsPanel.Width - 20) / 2;

            int leftY = yPosition;
            leftY = AddSectionTitle(" 等待参数", leftY, 0);

            string delayTime = parameters.GetValueOrDefault("DelayTime", "30");
            leftY = AddDetailLine("延时时长", delayTime + "秒", leftY, 0, leftWidth);

            if (currentStatus == "running")
            {
                string elapsed = results.GetValueOrDefault("Elapsed", "0");
                string remaining = results.GetValueOrDefault("Remaining", delayTime);
                leftY = AddDetailLine("已等待", elapsed + "秒", leftY, 0, leftWidth);
                leftY = AddDetailLine("剩余时间", remaining + "秒", leftY, 0, leftWidth);

                // 显示进度条
                progressBar.Visible = true;
            }
            else if (currentStatus == "success")
            {
                string reason = parameters.GetValueOrDefault("Reason", "等待设备稳定");
                leftY = AddDetailLine("等待原因", reason, leftY, 0, leftWidth);

                // 右栏：执行结果
                int rightY = yPosition;
                rightY = AddSectionTitle(" 执行结果", rightY, leftWidth + 10);
                string actualTime = results.GetValueOrDefault("ActualTime", delayTime);
                rightY = AddDetailLine("实际耗时", actualTime + "秒", rightY, leftWidth + 10, leftWidth);
                rightY = AddDetailLine("状态", "正常完成", rightY, leftWidth + 10, leftWidth, StatusColors.Success);

                return Math.Max(leftY, rightY);
            }

            return leftY;
        }

        /// <summary>
        /// 显示变量赋值详情
        /// </summary>
        private int ShowVariableAssignDetails(ChildModel stepData, int yPosition)
        {
            var parameters = ParseStepParameters(stepData);
            var results = ParseStepResults(stepData);

            int leftWidth = (detailsPanel.Width - 20) / 2;

            // 左栏：赋值参数
            int leftY = yPosition;
            leftY = AddSectionTitle(" 赋值参数", leftY, 0);
            leftY = AddDetailLine("目标变量", parameters.GetValueOrDefault("Variable", "N/A"), leftY, 0, leftWidth);
            leftY = AddDetailLine("赋值方式", parameters.GetValueOrDefault("Method", "直接赋值"), leftY, 0, leftWidth);

            string expression = parameters.GetValueOrDefault("Expression", "");
            if (!string.IsNullOrEmpty(expression))
            {
                leftY = AddDetailLine("表达式", expression, leftY, 0, leftWidth);
            }

            // 右栏：赋值结果
            int rightY = yPosition;
            if (currentStatus == "success")
            {
                rightY = AddSectionTitle(" 赋值结果", rightY, leftWidth + 10);
                rightY = AddDetailLine("赋值前", results.GetValueOrDefault("OldValue", "null"), rightY, leftWidth + 10, leftWidth);
                rightY = AddDetailLine("赋值后", results.GetValueOrDefault("NewValue", "N/A"), rightY, leftWidth + 10, leftWidth);

                string calculation = results.GetValueOrDefault("Calculation", "");
                if (!string.IsNullOrEmpty(calculation))
                {
                    rightY = AddDetailLine("计算过程", calculation, rightY, leftWidth + 10, leftWidth);
                }

                return Math.Max(leftY, rightY);
            }

            return leftY;
        }

        /// <summary>
        /// 显示条件判断详情
        /// </summary>
        private int ShowConditionDetails(ChildModel stepData, int yPosition)
        {
            var parameters = ParseStepParameters(stepData);
            var results = ParseStepResults(stepData);

            int leftWidth = (detailsPanel.Width - 20) / 2;

            // 左栏：判断条件
            int leftY = yPosition;
            leftY = AddSectionTitle(" 判断条件", leftY, 0);
            leftY = AddDetailLine("判断条件", parameters.GetValueOrDefault("Condition", "N/A"), leftY, 0, leftWidth);
            leftY = AddDetailLine("真分支", parameters.GetValueOrDefault("TrueBranch", "继续执行"), leftY, 0, leftWidth);
            leftY = AddDetailLine("假分支", parameters.GetValueOrDefault("FalseBranch", "继续执行"), leftY, 0, leftWidth);

            // 右栏：判断结果
            int rightY = yPosition;
            if (currentStatus == "success" || currentStatus == "skipped")
            {
                rightY = AddSectionTitle(" 判断结果", rightY, leftWidth + 10);
                rightY = AddDetailLine("变量值", results.GetValueOrDefault("Value", "N/A"), rightY, leftWidth + 10, leftWidth);

                string result = results.GetValueOrDefault("Result", "N/A");
                Color resultColor = result == "True" ? StatusColors.Success : StatusColors.Skipped;
                rightY = AddDetailLine("结果", result, rightY, leftWidth + 10, leftWidth, resultColor);
                rightY = AddDetailLine("执行", results.GetValueOrDefault("Action", "N/A"), rightY, leftWidth + 10, leftWidth);

                return Math.Max(leftY, rightY);
            }

            return leftY;
        }

        /// <summary>
        /// 显示消息通知详情
        /// </summary>
        private int ShowMessageDetails(ChildModel stepData, int yPosition)
        {
            var parameters = ParseStepParameters(stepData);

            yPosition = AddSectionTitle(" 消息内容", yPosition, 0);
            yPosition = AddDetailLine("消息类型", parameters.GetValueOrDefault("MessageType", "提示框"), yPosition, 0, detailsPanel.Width);
            yPosition = AddDetailLine("标题", parameters.GetValueOrDefault("Title", "N/A"), yPosition, 0, detailsPanel.Width);
            yPosition = AddDetailLine("内容", parameters.GetValueOrDefault("Content", "N/A"), yPosition, 0, detailsPanel.Width);

            string buttons = parameters.GetValueOrDefault("Buttons", "");
            if (!string.IsNullOrEmpty(buttons))
            {
                yPosition = AddDetailLine("按钮", buttons, yPosition, 0, detailsPanel.Width);
            }

            return yPosition;
        }

        /// <summary>
        /// 显示单元格操作详情
        /// </summary>
        private int ShowCellOperationDetails(ChildModel stepData, int yPosition)
        {
            var parameters = ParseStepParameters(stepData);
            var results = ParseStepResults(stepData);
            string stepType = stepData.StepName ?? "未知";

            int leftWidth = (detailsPanel.Width - 20) / 2;

            // 左栏：操作参数
            int leftY = yPosition;
            leftY = AddSectionTitle(stepType == "读取单元格" ? " 读取参数" : " 写入参数", leftY, 0);
            leftY = AddDetailLine("报表名称", parameters.GetValueOrDefault("ReportName", "N/A"), leftY, 0, leftWidth);
            leftY = AddDetailLine("工作表", parameters.GetValueOrDefault("SheetName", "Sheet1"), leftY, 0, leftWidth);
            leftY = AddDetailLine("单元格", parameters.GetValueOrDefault("Cell", "N/A"), leftY, 0, leftWidth);

            if (stepType == "写入单元格")
            {
                leftY = AddDetailLine("写入来源", parameters.GetValueOrDefault("Source", "N/A"), leftY, 0, leftWidth);
            }
            else
            {
                leftY = AddDetailLine("目标变量", parameters.GetValueOrDefault("Variable", "N/A"), leftY, 0, leftWidth);
            }

            // 右栏：操作结果
            int rightY = yPosition;
            if (currentStatus == "success")
            {
                rightY = AddSectionTitle(stepType == "读取单元格" ? " 读取结果" : " 写入结果", rightY, leftWidth + 10);

                if (stepType == "读取单元格")
                {
                    rightY = AddDetailLine("读取值", results.GetValueOrDefault("Value", "N/A"), rightY, leftWidth + 10, leftWidth);
                }
                else
                {
                    rightY = AddDetailLine("写入值", results.GetValueOrDefault("WriteValue", "N/A"), rightY, leftWidth + 10, leftWidth);
                }

                rightY = AddDetailLine("状态", "成功 ✓", rightY, leftWidth + 10, leftWidth, StatusColors.Success);

                return Math.Max(leftY, rightY);
            }

            return leftY;
        }

        /// <summary>
        /// 显示默认详情
        /// </summary>
        private int ShowDefaultDetails(ChildModel stepData, int yPosition)
        {
            var parameters = ParseStepParameters(stepData);

            if (parameters.Count > 0)
            {
                yPosition = AddSectionTitle(" 执行参数", yPosition, 0);

                int count = 0;
                foreach (var kvp in parameters)
                {
                    if (count >= 5) break; // 最多显示5个参数
                    yPosition = AddDetailLine(kvp.Key, kvp.Value, yPosition, 0, detailsPanel.Width);
                    count++;
                }
            }

            return yPosition;
        }

        /// <summary>
        /// 添加区块标题
        /// </summary>
        private int AddSectionTitle(string title, int yPosition, int xPosition)
        {
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("微软雅黑", 9F, FontStyle.Bold),
                ForeColor = StatusColors.Running,
                AutoSize = true,
                Location = new Point(xPosition, yPosition),
                Padding = new Padding(10, 5, 5, 5)
            };
            detailsPanel.Controls.Add(lblTitle);

            return yPosition + 22;
        }

        /// <summary>
        /// 添加详情行
        /// </summary>
        private int AddDetailLine(string label, string value, int yPosition, int xPosition, int maxWidth, Color? valueColor = null)
        {
            var lblLine = new Label
            {
                Text = $"{label}: {value}",
                Font = new Font("微软雅黑", 9F),
                ForeColor = valueColor ?? Color.FromArgb(128, 128, 128),
                Location = new Point(xPosition, yPosition),
                MaximumSize = new Size(maxWidth, 0),
                AutoSize = true,
                Padding = new Padding(10, 5, 5, 5)
            };

            detailsPanel.Controls.Add(lblLine);

            return yPosition + Math.Max(lblLine.Height, 22);
        }

        #endregion

        #region 私有方法 - 辅助功能

        /// <summary>
        /// 绘制圆形序号
        /// </summary>
        private void CirclePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 根据当前状态选择背景色
            Color circleColor = currentStatus switch
            {
                "running" => StatusColors.Running,
                "success" => StatusColors.Success,
                "failed" => StatusColors.Failed,
                "skipped" => StatusColors.Skipped,
                _ => StatusColors.Waiting
            };

            using (var brush = new SolidBrush(circleColor))
            {
                e.Graphics.FillEllipse(brush, 0, 0, 31, 31);
            }

            TextRenderer.DrawText(e.Graphics, stepNumber.ToString(),
                new Font("微软雅黑", 10F, FontStyle.Bold),
                new Rectangle(0, 0, 32, 32), Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// 更新时间标签位置
        /// </summary>
        private void UpdateTimePosition()
        {
            if (lblStepTime != null && contentPanel != null && lblStepTime.Width > 0)
            {
                lblStepTime.Location = new Point(contentPanel.Width - lblStepTime.Width - 15, 10);
            }
        }

        /// <summary>
        /// 解析步骤参数
        /// </summary>
        private Dictionary<string, string> ParseStepParameters(ChildModel stepData)
        {
            var parameters = new Dictionary<string, string>();

            if (stepData?.StepParameter == null) return parameters;

            try
            {
                // 尝试解析 JSON 格式的参数
                string paramStr = stepData.StepParameter.ToString();

                if (paramStr.StartsWith("{"))
                {
                    var json = JObject.Parse(paramStr);
                    foreach (var prop in json.Properties())
                    {
                        parameters[prop.Name] = prop.Value.ToString();
                    }
                }
                else
                {
                    // 如果不是JSON，提供默认示例数据
                    FillDefaultParameters(stepData.StepName, parameters);
                }
            }
            catch
            {
                // 解析失败，使用默认数据
                FillDefaultParameters(stepData.StepName, parameters);
            }

            return parameters;
        }

        /// <summary>
        /// 填充默认参数（示例数据）
        /// </summary>
        private void FillDefaultParameters(string stepName, Dictionary<string, string> parameters)
        {
            switch (stepName)
            {
                case "读取PLC":
                case "写入PLC":
                    parameters["ModuleName"] = "PLC_Module_1";
                    parameters["Address"] = "DB1.DBW100";
                    if (stepName == "读取PLC")
                    {
                        parameters["Variable"] = "Temperature";
                    }
                    else
                    {
                        parameters["WriteValue"] = "100";
                        parameters["ValueSource"] = "SetPoint";
                    }
                    break;

                case "延时等待":
                    parameters["DelayTime"] = "30";
                    parameters["Reason"] = "等待设备稳定";
                    break;

                case "变量赋值":
                    parameters["Variable"] = "TestResult";
                    parameters["Method"] = "表达式计算";
                    parameters["Expression"] = "{ValueA} * 2 + 10";
                    break;

                case "条件判断":
                    parameters["Condition"] = "{Temperature} > 30";
                    parameters["TrueBranch"] = "继续执行";
                    parameters["FalseBranch"] = "跳转到步骤8";
                    break;

                case "消息通知":
                    parameters["MessageType"] = "确认对话框";
                    parameters["Title"] = "请确认";
                    parameters["Content"] = "请确认设备已准备就绪";
                    parameters["Buttons"] = "[确定] [取消]";
                    break;

                case "读取单元格":
                case "写入单元格":
                    parameters["ReportName"] = "测试报告";
                    parameters["SheetName"] = "Sheet1";
                    parameters["Cell"] = stepName == "读取单元格" ? "B5" : "C10";
                    if (stepName == "读取单元格")
                    {
                        parameters["Variable"] = "Score";
                    }
                    else
                    {
                        parameters["Source"] = "{Result}";
                    }
                    break;
            }
        }

        /// <summary>
        /// 解析步骤结果
        /// </summary>
        private Dictionary<string, string> ParseStepResults(ChildModel stepData)
        {
            var results = new Dictionary<string, string>();

            if (stepData == null) return results;

            try
            {
                // 这里应该从stepData中获取实际的执行结果
                // 如果有ExecutionResult字段，可以解析它
                // 目前提供示例数据

                if (currentStatus == "success")
                {
                    FillDefaultResults(stepData.StepName, results);
                }
                else if (currentStatus == "failed")
                {
                    results["ErrorCode"] = "E1001";
                    results["ErrorMessage"] = "通信超时";
                }
                else if (currentStatus == "running")
                {
                    // 对于延时步骤，可以提供实时进度
                    if (stepData.StepName == "延时等待")
                    {
                        results["Elapsed"] = "15";
                        results["Remaining"] = "15";
                    }
                }
            }
            catch
            {
                // 忽略错误
            }

            return results;
        }

        /// <summary>
        /// 填充默认结果（示例数据）
        /// </summary>
        private void FillDefaultResults(string stepName, Dictionary<string, string> results)
        {
            switch (stepName)
            {
                case "读取PLC":
                    results["Value"] = "25.6°C";
                    results["DataType"] = "REAL";
                    break;

                case "写入PLC":
                    results["WriteValue"] = "100";
                    results["ConfirmValue"] = "100";
                    break;

                case "延时等待":
                    results["ActualTime"] = "30";
                    break;

                case "变量赋值":
                    results["OldValue"] = "null";
                    results["NewValue"] = "30";
                    results["Calculation"] = "10 * 2 + 10 = 30";
                    break;

                case "条件判断":
                    results["Value"] = "35.2°C";
                    results["Result"] = "True";
                    results["Action"] = "继续执行";
                    break;

                case "读取单元格":
                    results["Value"] = "98.5";
                    break;

                case "写入单元格":
                    results["WriteValue"] = "合格";
                    break;
            }
        }

        #endregion
    }
}
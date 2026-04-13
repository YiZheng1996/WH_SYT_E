using MainUI.LogicalConfiguration;
using Label = AntdUI.Label;
using Panel = Sunny.UI.UIPanel;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 步骤状态控件
    /// 负责步骤的状态显示、布局和时间/进度更新
    /// 参数渲染委托给 <see cref="StepParameterRenderer"/>
    /// </summary>
    public class StepStatusControl : Panel
    {
        #region 私有字段

        private readonly Panel statusIndicator;
        private readonly Panel contentPanel;
        private readonly Panel circlePanel;
        private readonly Label lblStepName;
        private readonly Label lblStepStatus;
        private readonly Label lblStepTime;
        private readonly Panel separatorLine;
        private readonly AntdUI.Progress progressBar;
        private readonly Panel detailsPanel;

        private readonly StepParameterRenderer _renderer;

        private int stepNumber;
        private string currentStatus = "waiting";
        private ChildModel currentStepData;

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

            Height = 85;
            Width = 470;
            BackColor = BackgroundColors.Waiting;
            Margin = new Padding(0, 0, 12, 12);

            statusIndicator = new Panel
            {
                Width = 5,
                Dock = DockStyle.Left,
                FillColor = StatusColors.Waiting,
                RectColor = StatusColors.Waiting,
                BackColor = StatusColors.Waiting
            };
            Controls.Add(statusIndicator);

            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(15, 12, 15, 12),
                FillColor = BackgroundColors.Waiting,
                RectColor = StatusColors.Waiting,
            };
            Controls.Add(contentPanel);

            circlePanel = new Panel
            {
                Size = new Size(32, 32),
                Location = new Point(15, 12),
                BackColor = Color.Transparent,
                RectColor = Color.Transparent
            };
            circlePanel.Paint += CirclePanel_Paint;
            contentPanel.Controls.Add(circlePanel);

            lblStepName = new Label
            {
                Text = stepName,
                Font = new Font("微软雅黑", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(60, 10)
            };
            contentPanel.Controls.Add(lblStepName);

            lblStepStatus = new Label
            {
                Text = "● 等待中",
                Font = new Font("微软雅黑", 9F),
                ForeColor = StatusColors.Waiting,
                AutoSize = true,
                Location = new Point(60, 35)
            };
            contentPanel.Controls.Add(lblStepStatus);

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

            separatorLine = new Panel
            {
                Height = 1,
                BackColor = StatusColors.Waiting,
                Location = new Point(15, 65),
                Visible = false
            };
            contentPanel.Controls.Add(separatorLine);

            detailsPanel = new Panel
            {
                Location = new Point(15, 73),
                BackColor = Color.Transparent,
                Padding = new Padding(5),
                AutoSize = false,
                Visible = false
            };
            contentPanel.Controls.Add(detailsPanel);

            progressBar = new AntdUI.Progress
            {
                Location = new Point(15, 0),
                Height = 15,
                Visible = false,
                ForeColor = StatusColors.Running,
                Radius = 3
            };
            contentPanel.Controls.Add(progressBar);

            contentPanel.Resize += (s, e) => UpdateTimePosition();

            // 初始化渲染器
            _renderer = new StepParameterRenderer(detailsPanel);
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

            Color statusColor, bgColor;
            string statusText;
            bool showDetails;

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

            ApplyColors(statusColor, bgColor);
            lblStepStatus.Text = statusText;
            lblStepStatus.ForeColor = statusColor;
            circlePanel.Invalidate();

            if (showDetails && stepData != null)
                ShowDetails(stepData);
            else
                HideDetails();
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
                float pct = Math.Min(1.0f, (float)current / total);
                progressBar.Value = pct;
                progressBar.Text = $"{(int)(pct * 100)}% ({current}/{total}秒)";
            }
        }

        #endregion

        #region 详情展示

        private void ShowDetails(ChildModel stepData)
        {
            detailsPanel.Controls.Clear();

            separatorLine.Visible = true;
            separatorLine.Width = contentPanel.Width - 30;
            detailsPanel.Visible = true;
            detailsPanel.Width = contentPanel.Width - 30;

            int y = 10;
            y = ShowConfigurationParameters(stepData, y);
            y += 8;

            if (currentStatus != "waiting")
                y = ShowRuntimeInfo(stepData, y);

            detailsPanel.Height = y + 10;
            Height = 70 + detailsPanel.Height + 15;

            if (!progressBar.Visible) return;
            Height += 15;
            progressBar.Location = new Point(15, 73 + detailsPanel.Height + 8);
            progressBar.Width = contentPanel.Width - 30;
        }

        private void HideDetails()
        {
            separatorLine.Visible = false;
            detailsPanel.Visible = false;
            progressBar.Visible = false;
            Height = 85;
        }

        private int ShowConfigurationParameters(ChildModel stepData, int yPosition)
        {
            yPosition = _renderer.AddSectionTitle("配置参数", yPosition);

            if (stepData?.StepParameter == null)
                return _renderer.AddLine("参数状态", "未配置参数", yPosition, Color.FromArgb(150, 150, 150));

            try
            {
                string stepType = stepData.StepName ?? "Unknown";
                yPosition = _renderer.Render(stepType, stepData.StepParameter, yPosition);
            }
            catch (Exception ex)
            {
                yPosition = _renderer.AddLine("参数解析", $"解析失败: {ex.Message}", yPosition, Color.FromArgb(220, 53, 69));
                Debug.WriteLine($"参数解析异常: {ex}");
            }
            return yPosition;
        }

        private int ShowRuntimeInfo(ChildModel stepData, int yPosition)
        {
            yPosition = _renderer.AddSectionTitle("运行时信息", yPosition);

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
            yPosition = _renderer.AddLine("状态", statusInfo, yPosition, statusColor);

            if (currentStatus == "failed" && !string.IsNullOrEmpty(stepData?.ErrorMessage))
                yPosition = _renderer.AddMultilineBlock("错误信息", stepData.ErrorMessage, yPosition, StatusColors.Failed);

            if (!string.IsNullOrEmpty(stepData?.Remark))
                yPosition = _renderer.AddMultilineBlock("备注", stepData.Remark, yPosition, Color.FromArgb(96, 96, 96));

            return yPosition;
        }

        #endregion

        #region 私有辅助

        private void ApplyColors(Color statusColor, Color bgColor)
        {
            SetPanelColor(statusIndicator, statusColor);
            SetPanelColor(separatorLine, statusColor);
            SetPanelColor(contentPanel, bgColor);

            detailsPanel.RectColor = statusColor;
            detailsPanel.FillColor = bgColor;
            separatorLine.BackColor = statusColor;
            BackColor = bgColor;
            contentPanel.FillColor = bgColor;
            contentPanel.RectColor = statusColor;
        }

        private void SetPanelColor(Panel panel, Color color)
        {
            panel.BackColor = Color.Transparent;
            panel.FillColor = color;
            panel.FillColor2 = color;
            panel.RectColor = color;
            panel.RectDisableColor = color;
        }

        private void UpdateTimePosition()
        {
            if (lblStepTime != null && lblStepTime.Width > 0)
                lblStepTime.Location = new Point(contentPanel.Width - lblStepTime.Width - 15, 10);
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
                g.FillEllipse(brush, 0, 0, 32, 32);

            string num = stepNumber.ToString();
            using (var font = new Font("微软雅黑", 10F, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
            {
                var size = g.MeasureString(num, font);
                g.DrawString(num, font, brush, (32 - size.Width) / 2, (32 - size.Height) / 2);
            }
        }

        #endregion
    }
}
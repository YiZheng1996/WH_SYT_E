using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainUI.Procedure.Controls.UcTestDetails;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 步骤状态显示控件
    /// </summary>
    public class StepStatusControl : AntdUI.Panel
    {
        #region 私有字段

        private AntdUI.Panel indicatorPanel;
        private Label lblNumber;
        private Label lblName;
        private Label lblDescription;
        private Label lblStatus;
        private Label lblElapsedTime;
        private AntdUI.Panel contentPanel;

        private StepStatus _status = StepStatus.Pending;
        private DateTime _startTime;
        private DateTime _endTime;

        #endregion

        #region 构造函数

        public StepStatusControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 初始化

        private void InitializeComponent()
        {
            // 主容器样式
            BackColor = Color.White;
            Radius = 8;
            BorderWidth = 1;
            BorderColor = Color.FromArgb(229, 230, 235);
            Shadow = 4;
            ShadowOpacity = 0.04f;

            // 左侧状态指示器
            indicatorPanel = new AntdUI.Panel
            {
                Size = new Size(5, Height),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(229, 230, 235),
                Radius = 0,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom
            };
            Controls.Add(indicatorPanel);

            // 内容容器
            contentPanel = new AntdUI.Panel
            {
                Location = new Point(20, 12),
                Size = new Size(Width - 40, Height - 24),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom
            };
            Controls.Add(contentPanel);

            // 步骤序号
            lblNumber = new Label
            {
                Location = new Point(0, 0),
                Size = new Size(50, 30),
                Font = new Font("微软雅黑", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(140, 140, 140),
                TextAlign = ContentAlignment.MiddleLeft
            };
            contentPanel.Controls.Add(lblNumber);

            // 步骤名称
            lblName = new Label
            {
                Location = new Point(55, 2),
                AutoSize = true,
                Font = new Font("微软雅黑", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 35, 41)
            };
            contentPanel.Controls.Add(lblName);

            // 步骤描述
            lblDescription = new Label
            {
                Location = new Point(55, 26),
                AutoSize = true,
                MaximumSize = new Size(contentPanel.Width - 160, 0),
                Font = new Font("微软雅黑", 8.5F),
                ForeColor = Color.FromArgb(140, 146, 153)
            };
            contentPanel.Controls.Add(lblDescription);

            // 耗时标签
            lblElapsedTime = new Label
            {
                Location = new Point(55, 48),
                AutoSize = true,
                Font = new Font("Consolas", 8F),
                ForeColor = Color.FromArgb(140, 146, 153),
                Visible = false
            };
            contentPanel.Controls.Add(lblElapsedTime);

            // 状态标签
            lblStatus = new Label
            {
                Location = new Point(contentPanel.Width - 100, 8),
                Size = new Size(100, 24),
                Font = new Font("微软雅黑", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            contentPanel.Controls.Add(lblStatus);

            UpdateDisplay();
        }

        #endregion

        #region 属性

        public int StepNumber
        {
            get => int.TryParse(lblNumber.Text.Replace("步骤 ", ""), out int num) ? num : 0;
            set
            {
                lblNumber.Text = $"步骤 {value}";
                UpdateDisplay();
            }
        }

        public string StepName
        {
            get => lblName.Text;
            set
            {
                lblName.Text = value;
                UpdateDisplay();
            }
        }

        public string StepDescription
        {
            get => lblDescription.Text;
            set
            {
                lblDescription.Text = value;
                lblDescription.Visible = !string.IsNullOrEmpty(value);
            }
        }

        public StepStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                UpdateDisplay();
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                UpdateElapsedTime();
            }
        }

        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                UpdateElapsedTime();
            }
        }

        #endregion

        #region 私有方法

        private void UpdateDisplay()
        {
            switch (_status)
            {
                case StepStatus.Pending:
                    // 灰色主题 - 等待状态
                    indicatorPanel.BackColor = Color.FromArgb(229, 230, 235);
                    lblNumber.ForeColor = Color.FromArgb(140, 146, 153);
                    lblStatus.Text = "⏳ 等待中";
                    lblStatus.ForeColor = Color.FromArgb(140, 146, 153);
                    BackColor = Color.White;
                    BorderColor = Color.FromArgb(229, 230, 235);
                    break;

                case StepStatus.Running:
                    // 蓝色主题 - 执行中
                    indicatorPanel.BackColor = Color.FromArgb(24, 144, 255);
                    lblNumber.ForeColor = Color.FromArgb(24, 144, 255);
                    lblStatus.Text = "▶ 执行中";
                    lblStatus.ForeColor = Color.FromArgb(24, 144, 255);
                    BackColor = Color.FromArgb(240, 248, 255);
                    BorderColor = Color.FromArgb(145, 200, 255);
                    break;

                case StepStatus.Success:
                    // 绿色主题 - 成功
                    indicatorPanel.BackColor = Color.FromArgb(82, 196, 26);
                    lblNumber.ForeColor = Color.FromArgb(82, 196, 26);
                    lblStatus.Text = "✓ 已完成";
                    lblStatus.ForeColor = Color.FromArgb(82, 196, 26);
                    BackColor = Color.FromArgb(246, 255, 237);
                    BorderColor = Color.FromArgb(183, 235, 143);
                    break;

                case StepStatus.Failed:
                    // 红色主题 - 失败
                    indicatorPanel.BackColor = Color.FromArgb(245, 63, 63);
                    lblNumber.ForeColor = Color.FromArgb(245, 63, 63);
                    lblStatus.Text = "✗ 失败";
                    lblStatus.ForeColor = Color.FromArgb(245, 63, 63);
                    BackColor = Color.FromArgb(255, 241, 240);
                    BorderColor = Color.FromArgb(255, 163, 158);
                    break;
            }
        }

        private void UpdateElapsedTime()
        {
            if (_startTime == DateTime.MinValue)
            {
                lblElapsedTime.Visible = false;
                return;
            }

            TimeSpan elapsed;
            if (_endTime == DateTime.MinValue)
            {
                // 正在执行中，显示实时耗时
                elapsed = DateTime.Now - _startTime;
            }
            else
            {
                // 已完成，显示总耗时
                elapsed = _endTime - _startTime;
            }

            lblElapsedTime.Text = $"⏱ 耗时: {elapsed.TotalSeconds:F2}s";
            lblElapsedTime.Visible = true;
        }

        #endregion
    }



    /// <summary>
    /// 步骤状态枚举
    /// </summary>
    public enum StepStatus
    {
        Pending,   // 待执行
        Running,   // 执行中
        Success,   // 成功
        Failed     // 失败
    }

}

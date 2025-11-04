using AntdUI;
using MainUI.LogicalConfiguration;
using System.Drawing.Drawing2D;
using Panel = AntdUI.Panel;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 试验详情用户控件
    /// 用于显示工作流执行的实时状态和详细信息
    /// </summary>
    public partial class UcTestDetails : UserControl
    {
        #region 私有字段

        private Panel panelSteps;
        private Panel panelInfo;
        private AntdUI.Label lblCurrentTest;
        private AntdUI.Label lblTestStatus;
        private AntdUI.Label lblElapsedTime;
        private AntdUI.Label lblCurrentStep;
        private Progress progressBar;
        private Panel panelStepList;
        private System.Windows.Forms.Timer updateTimer;

        // 步骤状态字典
        private Dictionary<int, StepStatusControl> _stepControls = [];

        #endregion

        #region 构造函数

        public UcTestDetails()
        {
            InitializeComponent();
            InitializeComponent2();
            InitializeCustomControls();
            InitializeTimer();
        }

        #endregion

        #region 初始化方法

        private void InitializeComponent2()
        {
            SuspendLayout();

            // 设置控件基本属性
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Name = "UcTestDetails";
            Size = new Size(1000, 700);

            ResumeLayout(false);
        }

        /// <summary>
        /// 初始化自定义控件
        /// </summary>
        private void InitializeCustomControls()
        {
            // 1. 创建顶部信息面板
            panelInfo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            Controls.Add(panelInfo);

            // 当前测试项
            lblCurrentTest = new AntdUI.Label
            {
                Text = "当前测试项：未开始",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("微软雅黑", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64)
            };
            panelInfo.Controls.Add(lblCurrentTest);

            // 测试状态
            lblTestStatus = new AntdUI.Label
            {
                Text = "状态：待机",
                Location = new Point(20, 50),
                AutoSize = true,
                Font = new Font("微软雅黑", 10F),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            panelInfo.Controls.Add(lblTestStatus);

            // 已用时间
            lblElapsedTime = new AntdUI.Label
            {
                Text = "已用时间：00:00:00",
                Location = new Point(250, 50),
                AutoSize = true,
                Font = new Font("微软雅黑", 10F),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            panelInfo.Controls.Add(lblElapsedTime);

            // 当前步骤
            lblCurrentStep = new AntdUI.Label
            {
                Text = "当前步骤：-",
                Location = new Point(20, 80),
                AutoSize = true,
                Font = new Font("微软雅黑", 10F),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            panelInfo.Controls.Add(lblCurrentStep);

            // 进度条
            progressBar = new Progress
            {
                Location = new Point(20, 85),
                Size = new Size(panelInfo.Width - 40, 20),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Value = 0,
                Shape = (TShapeProgress)TShape.Round
            };
            panelInfo.Controls.Add(progressBar);

            // 2. 创建步骤列表面板
            panelSteps = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20),
                //AutoScroll = true
            };
            Controls.Add(panelSteps);

            // 步骤列表容器
            panelStepList = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Radius = 8,
                Shadow = 5,
                Padding = new Padding(15)
            };
            panelSteps.Controls.Add(panelStepList);
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitializeTimer()
        {
            updateTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // 每秒更新一次
            };
            updateTimer.Tick += UpdateTimer_Tick;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 开始测试
        /// </summary>
        public void StartTest(string testName, List<ChildModel> steps)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, List<ChildModel>>(StartTest), testName, steps);
                return;
            }

            try
            {
                lblCurrentTest.Text = $"当前测试项：{testName}";
                lblTestStatus.Text = "状态：执行中";
                lblTestStatus.ForeColor = Color.FromArgb(24, 144, 255);
                lblElapsedTime.Text = "已用时间：00:00:00";
                lblCurrentStep.Text = "当前步骤：准备中...";
                progressBar.Value = 0;

                // 清空并创建步骤控件
                _stepControls.Clear();
                panelStepList.Controls.Clear();

                int yPosition = 10;
                for (int i = 0; i < steps.Count; i++)
                {
                    var stepControl = new StepStatusControl
                    {
                        StepNumber = i + 1,
                        StepName = steps[i].StepName,
                        Status = StepStatus.Pending,
                        Location = new Point(10, yPosition),
                        Size = new Size(panelStepList.Width - 40, 60),
                        Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
                    };

                    panelStepList.Controls.Add(stepControl);
                    _stepControls[i] = stepControl;

                    yPosition += 70;
                }

                // 启动定时器
                updateTimer.Start();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("启动测试详情显示失败", ex);
            }
        }

        /// <summary>
        /// 更新步骤状态
        /// </summary>
        public void UpdateStepStatus(int stepIndex, ChildModel step)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, ChildModel>(UpdateStepStatus), stepIndex, step);
                return;
            }

            try
            {
                if (_stepControls.TryGetValue(stepIndex, out var stepControl))
                {
                    // 更新步骤状态
                    stepControl.Status = step.Status switch
                    {
                        1 => StepStatus.Running,
                        2 => StepStatus.Success,
                        3 => StepStatus.Failed,
                        _ => StepStatus.Pending
                    };

                    // 更新当前步骤显示
                    if (step.Status == 1) // 执行中
                    {
                        lblCurrentStep.Text = $"当前步骤：[{stepIndex + 1}] {step.StepName}";
                    }

                    // 更新进度条
                    float progress = (float)(stepIndex + 1) / _stepControls.Count * 100;
                    progressBar.Value = progress;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("更新步骤状态失败", ex);
            }
        }

        /// <summary>
        /// 测试完成
        /// </summary>
        public void TestCompleted(bool success)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(TestCompleted), success);
                return;
            }

            try
            {
                updateTimer.Stop();

                lblTestStatus.Text = success ? "状态：✓ 测试完成" : "状态：✗ 测试失败";
                lblTestStatus.ForeColor = success
                    ? Color.FromArgb(82, 196, 26)
                    : Color.FromArgb(255, 77, 79);

                lblCurrentStep.Text = success ? "所有步骤已完成" : "测试中断";
                progressBar.Value = 100;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("测试完成处理失败", ex);
            }
        }

        /// <summary>
        /// 重置显示
        /// </summary>
        public void Reset()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(Reset));
                return;
            }

            try
            {
                updateTimer.Stop();

                lblCurrentTest.Text = "当前测试项：未开始";
                lblTestStatus.Text = "状态：待机";
                lblTestStatus.ForeColor = Color.FromArgb(100, 100, 100);
                lblElapsedTime.Text = "已用时间：00:00:00";
                lblCurrentStep.Text = "当前步骤：-";
                progressBar.Value = 0;

                _stepControls.Clear();
                panelStepList.Controls.Clear();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("重置显示失败", ex);
            }
        }

        #endregion

        #region 私有方法

        private DateTime testStartTime;

        /// <summary>
        /// 定时器更新
        /// </summary>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (testStartTime == DateTime.MinValue)
                    testStartTime = DateTime.Now;

                TimeSpan elapsed = DateTime.Now - testStartTime;
                lblElapsedTime.Text = $"已用时间：{elapsed:hh\\:mm\\:ss}";
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("更新时间显示失败", ex);
            }
        }

        #endregion
    }

    #region 步骤状态控件

    /// <summary>
    /// 步骤状态
    /// </summary>
    public enum StepStatus
    {
        Pending,   // 待执行
        Running,   // 执行中
        Success,   // 成功
        Failed     // 失败
    }

    /// <summary>
    /// 步骤状态显示控件
    /// </summary>
    public class StepStatusControl : Panel
    {
        private AntdUI.Label lblNumber;
        private AntdUI.Label lblName;
        private AntdUI.Label lblStatus;
        private Panel statusIndicator;

        private int _stepNumber;
        private string _stepName;
        private StepStatus _status;

        public int StepNumber
        {
            get => _stepNumber;
            set
            {
                _stepNumber = value;
                UpdateDisplay();
            }
        }

        public string StepName
        {
            get => _stepName;
            set
            {
                _stepName = value;
                UpdateDisplay();
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

        public StepStatusControl()
        {
            InitializeControl();
        }

        private void InitializeControl()
        {
            BackColor = Color.FromArgb(250, 250, 250);
            BorderStyle = DashStyle.Dash;
            Padding = new Padding(10);

            // 状态指示器
            statusIndicator = new Panel
            {
                Location = new Point(10, 20),
                Size = new Size(20, 20),
                BackColor = Color.Gray
            };
            Controls.Add(statusIndicator);

            // 步骤编号
            lblNumber = new AntdUI.Label
            {
                Location = new Point(40, 10),
                AutoSize = true,
                Font = new Font("微软雅黑", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64)
            };
            Controls.Add(lblNumber);

            // 步骤名称
            lblName = new AntdUI.Label
            {
                Location = new Point(40, 32),
                AutoSize = true,
                Font = new Font("微软雅黑", 9F),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            Controls.Add(lblName);

            // 状态文本
            lblStatus = new AntdUI.Label
            {
                Location = new Point(Width - 100, 20),
                AutoSize = true,
                Font = new Font("微软雅黑", 9F),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            Controls.Add(lblStatus);
        }

        private void UpdateDisplay()
        {
            lblNumber.Text = $"步骤 {StepNumber}";
            lblName.Text = StepName;

            switch (Status)
            {
                case StepStatus.Pending:
                    statusIndicator.BackColor = Color.Gray;
                    lblStatus.Text = "⏳ 待执行";
                    lblStatus.ForeColor = Color.Gray;
                    BackColor = Color.FromArgb(250, 250, 250);
                    break;

                case StepStatus.Running:
                    statusIndicator.BackColor = Color.FromArgb(24, 144, 255);
                    lblStatus.Text = "▶ 执行中";
                    lblStatus.ForeColor = Color.FromArgb(24, 144, 255);
                    BackColor = Color.FromArgb(230, 244, 255);
                    break;

                case StepStatus.Success:
                    statusIndicator.BackColor = Color.FromArgb(82, 196, 26);
                    lblStatus.Text = "✓ 完成";
                    lblStatus.ForeColor = Color.FromArgb(82, 196, 26);
                    BackColor = Color.FromArgb(246, 255, 237);
                    break;

                case StepStatus.Failed:
                    statusIndicator.BackColor = Color.FromArgb(255, 77, 79);
                    lblStatus.Text = "✗ 失败";
                    lblStatus.ForeColor = Color.FromArgb(255, 77, 79);
                    BackColor = Color.FromArgb(255, 241, 240);
                    break;
            }
        }
    }

    #endregion
}
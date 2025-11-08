using MainUI.LogicalConfiguration;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 试验详情用户控件
    /// 用于显示工作流执行的实时状态和详细信息
    /// </summary>
    public partial class UcTestDetails : UserControl
    {
        #region 私有字段

        // 步骤状态字典
        private Dictionary<int, StepStatusControl> _stepControls = [];

        // 测试开始时间
        private DateTime testStartTime;

        #endregion

        #region 构造函数

        public UcTestDetails()
        {
            InitializeComponent();
            InitializeTimer();
        }


        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitializeTimer()
        {
            updateTimer = new System.Windows.Forms.Timer()
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
        /// <param name="testName">测试名称</param>
        /// <param name="steps">步骤列表</param>
        public void StartTest(string testName, List<ChildModel> steps)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, List<ChildModel>>(StartTest), testName, steps);
                return;
            }

            try
            {
                // 更新顶部信息
                lblCurrentTest.Text = $"当前测试项: {testName}";
                lblTestStatus.Text = "▶ 执行中";
                lblTestStatus.ForeColor = Color.FromArgb(24, 144, 255);
                lblElapsedTime.Text = "⏱ 已用时间: 00:00:00";
                lblCurrentStep.Text = "📍 当前步骤: 准备中...";
                progressBar.Value = 0;

                // 清空并创建步骤控件
                _stepControls.Clear();
                panelStepList.Controls.Clear();

                int yPosition = 0;
                for (int i = 0; i < steps.Count; i++)
                {
                    var stepControl = new StepStatusControl
                    {
                        StepNumber = i + 1,
                        StepName = steps[i].StepName,
                        StepDescription = GetStepDescription(steps[i]),
                        Status = StepStatus.Pending,
                        Location = new Point(0, yPosition),
                        Size = new Size(820, 90),
                        Anchor = AnchorStyles.Left | AnchorStyles.Top
                    };

                    panelStepList.Controls.Add(stepControl);
                    _stepControls[i] = stepControl;

                    yPosition += 100; // 步骤间距
                }

                // 调整容器高度以容纳所有步骤
                //panelStepList.Height = yPosition;
                panelStepList.Width = 670;

                // 重置并启动定时器
                testStartTime = DateTime.Now;
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
        /// <param name="stepIndex">步骤索引</param>
        /// <param name="step">步骤信息</param>
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
                    // Status 值: 0=待执行, 1=执行中, 2=成功, 3=失败
                    var newStatus = step.Status switch
                    {
                        1 => StepStatus.Running,
                        2 => StepStatus.Success,
                        3 => StepStatus.Failed,
                        _ => StepStatus.Pending
                    };

                    // 如果状态从其他变为 Running，记录开始时间
                    if (newStatus == StepStatus.Running && stepControl.Status != StepStatus.Running)
                    {
                        stepControl.StartTime = DateTime.Now;
                    }
                    // 如果状态从 Running 变为完成/失败，记录结束时间并计算耗时
                    else if (stepControl.Status == StepStatus.Running &&
                            (newStatus == StepStatus.Success || newStatus == StepStatus.Failed))
                    {
                        stepControl.EndTime = DateTime.Now;
                    }

                    stepControl.Status = newStatus;

                    // 更新当前步骤显示
                    if (step.Status == 1) // 执行中
                    {
                        lblCurrentStep.Text = $"📍 当前步骤: [{stepIndex + 1}/{_stepControls.Count}] {step.StepName}";
                    }

                    // 更新进度条
                    int completedSteps = _stepControls.Values.Count(s =>
                        s.Status == StepStatus.Success || s.Status == StepStatus.Failed);
                    progressBar.Value = (int)((double)completedSteps / _stepControls.Count * 100);
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
        /// <param name="success">是否成功</param>
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

                lblTestStatus.Text = success
                    ? "✓ 测试完成" : "✗ 测试失败";
                lblTestStatus.ForeColor = success
                    ? Color.FromArgb(82, 196, 26)
                    : Color.FromArgb(245, 63, 63);

                lblCurrentStep.Text = success ? "📍 所有步骤已完成" : "📍 测试中断";
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

                lblCurrentTest.Text = "当前测试项:未开始";
                lblTestStatus.Text = "⏸ 待机";
                lblTestStatus.ForeColor = Color.FromArgb(82, 86, 89);
                lblElapsedTime.Text = "⏱ 已用时间: 00:00:00";
                lblCurrentStep.Text = "📍 当前步骤: 等待开始...";
                progressBar.Value = 0;

                _stepControls.Clear();
                panelStepList.Controls.Clear();

                testStartTime = DateTime.MinValue;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("重置显示失败", ex);
            }
        }

        #endregion

        #region 私有方法 - 事件处理

        /// <summary>
        /// 定时器更新事件
        /// </summary>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (testStartTime == DateTime.MinValue)
                    testStartTime = DateTime.Now;

                TimeSpan elapsed = DateTime.Now - testStartTime;
                lblElapsedTime.Text = $"⏱ 已用时间: {elapsed:hh\\:mm\\:ss}";
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("更新时间显示失败", ex);
            }
        }

        #endregion

        #region 私有方法 - 辅助功能

        /// <summary>
        /// 获取步骤描述信息
        /// </summary>
        /// <param name="step">步骤模型</param>
        /// <returns>步骤描述文字</returns>
        private string GetStepDescription(ChildModel step)
        {
            try
            {
                // 根据步骤类型返回不同的描述
                if (step.StepParameter != null)
                {
                    return step.StepName switch
                    {
                        "延时等待" => "暂停执行,等待指定时间",
                        "消息通知" => "显示消息提示框",
                        "变量定义" => "定义全局变量",
                        "试验参数" => "定义试验参数",
                        "变量赋值" => "为变量赋值",
                        "读取PLC" => "从PLC读取数据",
                        "写入PLC" => "向PLC写入数据",
                        "条件判断" => "根据条件决定流程",
                        "读取单元格" => "读取报表单元格数据",
                        "写入单元格" => "写入数据到报表单元格",
                        _ => "执行自定义操作"
                    };
                }
                return "正在执行...";
            }
            catch
            {
                return "";
            }
        }

        #endregion
    }
}
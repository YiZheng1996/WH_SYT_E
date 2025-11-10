using MainUI.LogicalConfiguration;
using System.Net.NetworkInformation;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 试验详情用户控件
    /// 用于显示工作流执行的实时状态和详细信息
    /// 支持步骤参数和结果的详细展示
    /// </summary>
    public partial class UcTestDetails : UserControl
    {
        #region 私有字段

        // 步骤状态字典
        private Dictionary<int, StepStatusControl> _stepControls = [];

        // 测试开始时间
        private DateTime testStartTime;

        // 步骤开始时间字典
        private Dictionary<int, DateTime> _stepStartTimes = [];

        // 步骤数据字典（保存完整的步骤信息）
        private Dictionary<int, ChildModel> _stepDataDict = [];

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
                lblCurrentStep.Text = "  当前步骤: 准备中...";
                progressBar.Value = 0;

                // 清空并创建步骤控件
                _stepControls.Clear();
                _stepStartTimes.Clear();
                _stepDataDict.Clear();
                panelStepList.Controls.Clear();

                // 创建步骤控件
                for (int i = 0; i < steps.Count; i++)
                {
                    var stepControl = new StepStatusControl(i + 1, steps[i].StepName);

                    panelStepList.Controls.Add(stepControl);
                    _stepControls[i] = stepControl;
                    _stepDataDict[i] = steps[i]; // 保存步骤数据
                }

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
                // 调试输出
                Debug.WriteLine($"========== 步骤 {stepIndex} 状态更新 ==========");
                Debug.WriteLine($"Status: {step.Status}, StepName: {step.StepName}");
                Debug.WriteLine($"当前 _stepStartTimes 字典内容:");
                foreach (var kvp in _stepStartTimes)
                {
                    Debug.WriteLine($"  步骤 {kvp.Key}: {kvp.Value:HH:mm:ss}");
                }
                if (_stepControls.TryGetValue(stepIndex, out var stepControl))
                {
                    // 更新步骤数据
                    _stepDataDict[stepIndex] = step;

                    // 根据 step.Status 更新步骤状态
                    // Status 值: 0=待执行, 1=执行中, 2=成功, 3=失败
                    string statusText;
                    string message = "";

                    switch (step.Status)
                    {
                        case 0:
                            statusText = "waiting";
                            Debug.WriteLine($"步骤 {stepIndex} → 等待中");
                            break;

                        case 1:
                            statusText = "running";
                            message = GetStepRunningMessage(step);

                            // 记录开始时间
                            if (!_stepStartTimes.ContainsKey(stepIndex))
                            {
                                _stepStartTimes[stepIndex] = DateTime.Now;
                                Debug.WriteLine($"✅ 步骤 {stepIndex} 开始计时: {DateTime.Now:HH:mm:ss.fff}");
                            }
                            else
                            {
                                Debug.WriteLine($"⚠️ 步骤 {stepIndex} 已有开始时间，不重复记录");
                            }
                            break;

                        case 2:
                            statusText = "success";
                            // 步骤完成时移除开始时间
                            if (_stepStartTimes.ContainsKey(stepIndex))
                            {
                                var elapsed = DateTime.Now - _stepStartTimes[stepIndex];
                                Debug.WriteLine($"✅ 步骤 {stepIndex} 完成，用时: {elapsed:hh\\:mm\\:ss}");
                                _stepStartTimes.Remove(stepIndex);
                                Debug.WriteLine($"✅ 步骤 {stepIndex} 开始时间已清除");
                            }
                            break;

                        case 3:
                            statusText = "failed";
                            message = GetStepErrorMessage(step);
                            // 步骤失败时移除开始时间
                            if (_stepStartTimes.ContainsKey(stepIndex))
                            {
                                var elapsed = DateTime.Now - _stepStartTimes[stepIndex];
                                Debug.WriteLine($"❌ 步骤 {stepIndex} 失败，用时: {elapsed:hh\\:mm\\:ss}");
                                _stepStartTimes.Remove(stepIndex);
                                Debug.WriteLine($"✅ 步骤 {stepIndex} 开始时间已清除");
                            }
                            break;

                        default:
                            statusText = "waiting";
                            break;
                    }

                    // 更新步骤控件状态（传递完整的步骤数据以显示详情）
                    stepControl.UpdateStatus(statusText, message, step);

                    // 更新当前步骤显示
                    if (step.Status == 1) // 执行中
                    {
                        lblCurrentStep.Text = $"  当前步骤: [{stepIndex + 1}/{_stepControls.Count}] {step.StepName}";
                    }

                    // 更新进度条
                    UpdateProgressBar();
                }
                 Debug.WriteLine($"========== 步骤 {stepIndex} 状态更新完成 ==========\n");
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

                lblTestStatus.Text = success ? "✓ 测试完成" : "✕ 测试失败";
                lblTestStatus.ForeColor = success
                    ? Color.FromArgb(82, 196, 26)   // 成功绿
                    : Color.FromArgb(231, 54, 36);   // 失败红

                lblCurrentStep.Text = success ? "  所有步骤已完成" : "  测试中断";
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

                lblCurrentTest.Text = "当前测试项: 未开始";
                lblTestStatus.Text = "● 待机中";
                lblTestStatus.ForeColor = Color.FromArgb(196, 199, 204);  // 等待灰
                lblElapsedTime.Text = "⏱ 已用时间: 00:00:00";
                lblCurrentStep.Text = "  当前步骤: 等待开始...";
                progressBar.Value = 0;

                _stepControls.Clear();
                _stepStartTimes.Clear();
                _stepDataDict.Clear();
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

                // 更新总时间
                TimeSpan elapsed = DateTime.Now - testStartTime;
                lblElapsedTime.Text = $"⏱ 已用时间: {elapsed:hh\\:mm\\:ss}";

                // 更新正在执行步骤的时间
                foreach (var kvp in _stepStartTimes.ToList())
                {
                    int stepIndex = kvp.Key;
                    Debug.WriteLine($"🕐 Timer Tick - 检查步骤 {stepIndex}, 开始时间: {kvp.Value:HH:mm:ss}");
                    // 检查步骤是否仍在执行中
                    if (_stepDataDict.TryGetValue(stepIndex, out var stepData) && stepData.Status == 1)
                    {
                        if (_stepControls.TryGetValue(stepIndex, out var stepControl))
                        {
                            TimeSpan stepElapsed = DateTime.Now - kvp.Value;
                            Debug.WriteLine($"   → 更新时间: {stepElapsed:hh\\:mm\\:ss}");
                            stepControl.UpdateTime(stepElapsed);

                            // 如果是延时步骤，更新进度
                            if (stepData.StepName == "延时等待")
                            {
                                UpdateDelayProgress(stepControl, stepData, stepElapsed);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("更新时间显示失败", ex);
            }
        }

        #endregion

        #region 私有方法 - 辅助功能

        /// <summary>
        /// 更新进度条
        /// </summary>
        private void UpdateProgressBar()
        {
            try
            {
                int completedSteps = 0;

                foreach (var kvp in _stepDataDict)
                {
                    var stepData = kvp.Value;
                    // 状态为2(成功)或3(失败)都算完成
                    if (stepData.Status == 2 || stepData.Status == 3)
                    {
                        completedSteps++;
                    }
                }

                if (_stepControls.Count > 0)
                {
                    progressBar.Value = (int)((double)completedSteps / _stepControls.Count * 100);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("更新进度条失败", ex);
            }
        }

        /// <summary>
        /// 获取步骤执行中的消息
        /// </summary>
        private string GetStepRunningMessage(ChildModel step)
        {
            try
            {
                return step.StepName switch
                {
                    "延时等待" => "等待中...",
                    "读取PLC" => "正在从PLC读取数据...",
                    "写入PLC" => "正在向PLC写入数据...",
                    "变量赋值" => "正在计算表达式...",
                    "条件判断" => "正在评估条件...",
                    "消息通知" => "等待用户确认...",
                    "读取单元格" => "正在读取报表数据...",
                    "写入单元格" => "正在写入报表数据...",
                    _ => "执行中..."
                };
            }
            catch
            {
                return "执行中...";
            }
        }

        /// <summary>
        /// 获取步骤错误消息
        /// </summary>
        private string GetStepErrorMessage(ChildModel step)
        {
            try
            {
                // 这里可以从 step 中获取实际的错误信息
                // 如果有 ErrorMessage 字段可以使用
                return "执行失败";
            }
            catch
            {
                return "执行失败";
            }
        }

        /// <summary>
        /// 更新延时步骤的进度
        /// </summary>
        private void UpdateDelayProgress(StepStatusControl stepControl, ChildModel stepData, TimeSpan elapsed)
        {
            try
            {
                // 从步骤参数中获取延时时长
                // 这里需要根据实际的参数结构来解析
                int totalSeconds = 30; // 默认30秒

                // 尝试解析参数
                if (stepData.StepParameter != null)
                {
                    string paramStr = stepData.StepParameter.ToString();
                    // 这里应该根据实际的参数格式来解析
                    // 示例：假设参数中有 DelayTime 字段

                    // 简化处理：如果能解析出数字就使用，否则使用默认值
                    if (int.TryParse(paramStr, out int parsedValue))
                    {
                        totalSeconds = parsedValue;
                    }
                }

                int currentSeconds = (int)elapsed.TotalSeconds;
                if (currentSeconds <= totalSeconds)
                {
                    stepControl.UpdateProgress(currentSeconds, totalSeconds);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("更新延时进度失败", ex);
            }
        }

        #endregion
    }
}
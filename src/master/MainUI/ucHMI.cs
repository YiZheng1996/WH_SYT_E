using AntdUI;
using MainUI.LogicalConfiguration;
using MainUI.Procedure.Controls;
using MainUI.Service;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Relational;
using System.Threading.Tasks;
using Label = System.Windows.Forms.Label;

namespace MainUI
{
    public partial class UcHMI : UserControl
    {
        #region 全局变量
        private RW.UI.Controls.Report.RWReport rWReport = new();
        private readonly frmMainMenu frm = new();
        Dictionary<TaskInfo, BaseTest> DicTestItems = [];
        public delegate void RunStatusHandler(bool obj);
        public event RunStatusHandler EmergencyStatusChanged;
        private static ParaConfig paraconfig;
        private List<ItemPointModel> _itemPoints = [];
        private readonly ControlMappings controls = new();
        private readonly ControlInitializationService _controlInitService;
        public delegate void TestStateHandler(bool isTesting, bool Exit = false);
        public event TestStateHandler TestStateChanged;
        private readonly string reportPath;
        private readonly OPCEventRegistration _opcEventRegistration;
        private readonly ReportService _reportService;
        private readonly TableService _tableService;
        private readonly CountdownService _countdownService;
        private bool _isTestActuallyStarted = false; //  试验开始标志位
        // 添加依赖注入的字段
        private readonly WorkflowExecutionService _workflowService;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private UcTestDetails ucTestDetails;// 试验详情控件
        #endregion
        public UcHMI(WorkflowExecutionService workflowService, ILogger<UcHMI> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _workflowService = workflowService;
            InitializeComponent();
            _opcEventRegistration = new OPCEventRegistration(this);
            reportPath = Path.Combine(Application.StartupPath, Constants.ReportsPath);
            _reportService = new ReportService(reportPath, rWReport);
            _tableService = new TableService(TableItemPoint, _itemPoints);
            _countdownService = new CountdownService(LabTestTime);
            _controlInitService = new ControlInitializationService(controls);

            if (_workflowService != null)
            {
                // 订阅工作流服务的事件
                _workflowService.ProgressMessage += OnWorkflowProgressMessage;
                _workflowService.ErrorOccurred += OnWorkflowError;
                _workflowService.ConfigurationLoaded += OnConfigurationLoaded;
                _workflowService.WorkflowStarted += OnWorkflowStarted;
                _workflowService.WorkflowCompleted += OnWorkflowCompleted;
                _workflowService.StepStatusChanged += OnWorkflowStepStatusChanged;
            }
        }

        #region 工作流事件处理
        /// <summary>
        /// 工作流进度消息处理
        /// </summary>
        private void OnWorkflowProgressMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(OnWorkflowProgressMessage), message);
                return;
            }
            AppendText(message);
        }

        /// <summary>
        /// 工作流错误处理
        /// </summary>
        private void OnWorkflowError(string message, Exception ex)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, Exception>(OnWorkflowError), message, ex);
                return;
            }
            AppendText($"✗ {message}: {ex.Message}");
            NlogHelper.Default.Error(message, ex);
        }

        /// <summary>
        /// 配置加载完成处理
        /// </summary>
        private void OnConfigurationLoaded(int configCount, int totalSteps)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, int>(OnConfigurationLoaded), configCount, totalSteps);
                return;
            }
            // 可以在这里更新UI，比如显示已加载的配置数量
        }

        /// <summary>
        /// 工作流开始事件
        /// </summary>
        private void OnWorkflowStarted(string itemName, int stepCount)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, int>(OnWorkflowStarted), itemName, stepCount);
                return;
            }

            try
            {
                // 更新TableItemPoint颜色状态为黄色(进行中)
                UpdateItemPointColor(itemName, 1);

                // 获取步骤列表
                var steps = _workflowService.GetConfiguration(itemName);
                if (steps != null)
                {
                    // 更新试验详情显示
                    ucTestDetails.StartTest(itemName, steps);

                    // 自动切换到试验详情页面
                    tabs1.SelectedIndex = 2;
                    NavigationButtonStyles.UpdateNavigationButtons(2, controls);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("处理工作流开始事件失败", ex);
            }
        }

        /// <summary>
        /// 工作流完成事件
        /// </summary>
        private void OnWorkflowCompleted(string itemName, bool success)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, bool>(OnWorkflowCompleted), itemName, success);
                return;
            }

            try
            {
                // 根据成功/失败状态更新颜色
                if (success)
                {
                    // 注释说 2=绿色
                    UpdateItemPointColor(itemName, 2);
                    _logger?.LogInformation("工作流完成成功: {ItemName}, 颜色设置为绿色", itemName);
                }
                else
                {
                    UpdateItemPointColor(itemName, 3); // 3 = 红色(失败)
                    _logger?.LogWarning("工作流完成失败: {ItemName}, 颜色设置为红色", itemName);
                }

                ucTestDetails.TestCompleted(success);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("处理工作流完成事件失败", ex);
            }
        }

        /// <summary>
        /// 步骤状态变化事件
        /// </summary>
        private void OnWorkflowStepStatusChanged(ChildModel step, int stepIndex)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChildModel, int>(OnWorkflowStepStatusChanged), step, stepIndex);
                return;
            }

            try
            {
                // 更新试验详情显示
                ucTestDetails.UpdateStepStatus(stepIndex, step);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("处理步骤状态变化失败", ex);
            }
        }
        #endregion

        #region 项点表格颜色更新
        /// <summary>
        /// 更新TableItemPoint中指定项目的颜色状态
        /// </summary>
        /// <param name="itemName">项目名称</param>
        /// <param name="colorState">颜色状态: 0=默认, 1=绿色(成功), 2=黄色(进行中), 3=红色(失败)</param>
        private void UpdateItemPointColor(string itemName, int colorState)
        {
            try
            {
                // 在数据源中查找对应的项目
                var itemPoint = _itemPoints.FirstOrDefault(item =>
                    item.ItemName == itemName);

                if (itemPoint != null)
                {
                    // 更新颜色状态
                    itemPoint.ColorState = colorState;

                    // 刷新TableItemPoint显示
                    _tableService.Refreshtable();
                }
                else
                {
                    _logger?.LogWarning("未找到项目: {ItemName}, 无法更新颜色", itemName);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新项目颜色失败: {ItemName}", itemName);
            }
        }
        #endregion

        #region 初始化
        public void Init()
        {
            try
            {
                InitializeOPC();  //初始化OPC连接和组
                InitializeControls();  //初始化控件和数据
                RegisterOPCHandlers();  //注册OPC组事件处理程序
                RegisterTestEventHandlers();  //注册测试状态和提示事件处理程序
                SetInitialState();  //设置初始状态
                InitializePermissions(); //初始化权限
                InitializeProcessInterface(); //加载工艺界面
                EnsureFrmHandleCreated(); //确保主窗体句柄已创建
                InitializeTestDetailsPage();
                OPCHelper.TestCongrp[0] = true; //默认自动模式
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 初始化试验详情页面
        /// </summary>
        private void InitializeTestDetailsPage()
        {
            try
            {
                // 创建试验详情控件
                ucTestDetails = new UcTestDetails
                {
                    Dock = DockStyle.Fill
                };

                // 添加到 tabs1 的第3个页面（索引2）
                // 确保 tabs1.TabPages 中已有 tabPageTestDetails
                if (tabs1.Pages.Count > 2)
                {
                    tabs1.Pages[2].Controls.Add(ucTestDetails);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化试验详情页面失败", ex);
            }
        }


        /// <summary>
        /// 加载工艺界面
        /// </summary>
        private void InitializeProcessInterface()
        {
            UcHMI_FLE ucHMI_PBU = new(frm);
            grpRainy.Controls.Add(ucHMI_PBU);
        }

        /// <summary>
        /// ===== 确保主窗体句柄已创建 =====
        /// 这个方法必须在 Init() 的最后调用
        /// </summary>
        private void EnsureFrmHandleCreated()
        {
            try
            {
                // 1. 检查 frm 是否为 null
                if (frm == null)
                {
                    string error = "frmMainMenu 实例为 null，无法初始化窗体句柄";
                    NlogHelper.Default.Error(error);
                    throw new InvalidOperationException(error);
                }

                // 2. 检查句柄是否已创建
                if (!frm.IsHandleCreated)
                {
                    // 访问 Handle 属性会强制创建窗口句柄
                    var handle = frm.Handle;
                    NlogHelper.Default.Info($"主窗体句柄已创建: {handle}");
                }
                else
                {
                    NlogHelper.Default.Info($"主窗体句柄已存在: {frm.Handle}");
                }

                // 3. 验证句柄确实已创建
                if (!frm.IsHandleCreated)
                {
                    string error = "主窗体句柄创建失败";
                    NlogHelper.Default.Error(error);
                    throw new InvalidOperationException(error);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"确保主窗体句柄创建失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 初始化权限
        /// </summary>
        private void InitializePermissions()
        {
            try
            {
                var currentUser = NewUsers.NewUserInfo;
                PermissionHelper.Initialize(currentUser.ID, currentUser.Role_ID);
                PermissionHelper.ApplyPermissionToControl(this, currentUser.ID);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"初始化权限失败：{ex.Message}");
                throw;
            }
        }

        // 初始化OPC连接和组
        private void InitializeOPC()
        {
            OPCHelper.Init();
        }

        // 初始化控件和数据
        private void InitializeControls()
        {
            _controlInitService.InitializeAllControls(this);
            _tableService.InittializeColumns(); // 初始化表格列
        }

        // 标记是否允许勾选
        private bool _allowCheckChange = true;
        private void TableItemPoint_CheckedChanged(object sender, TableCheckEventArgs e)
        {
            if (!_allowCheckChange) return;

            _tableService.HandleCheckedChanged(e);
        }

        // 注册OPC组事件处理程序
        private void RegisterOPCHandlers()
        {
            _opcEventRegistration.RegisterAll();
        }

        // 注册测试状态和提示事件处理程序
        private void RegisterTestEventHandlers()
        {
            BaseTest.TestStateChanged += BaseTest_TestStateChanged;
            BaseTest.TipsChanged += BaseTest_TipsChanged;
            BaseTest.WaitTick += BaseTest_WaitTick;
            BaseTest.WaitStepTick += BaseTest_WaitStepTick;
            BaseTest.TimingChanged += BaseTest_TimingChanged;

            // 订阅 CountdownService 的时间更新事件
            _countdownService.TimingUpdated += CountdownService_TimingUpdated;
        }

        // CountdownService 时间更新事件处理
        private void CountdownService_TimingUpdated(int seconds)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(() => CountdownService_TimingUpdated(seconds));
                    return;
                }

                TimeSpan elapsed = TimeSpan.FromSeconds(seconds);

                // 更新 UcTestDetails 的总时间显示
                ucTestDetails?.UpdateElapsedTime(elapsed);

                // 更新 UcTestDetails 的步骤时间
                ucTestDetails?.UpdateStepTimes();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("时间同步更新失败", ex);
            }
        }

        // 测试计时变更事件
        private void BaseTest_TimingChanged(object sender, int e) { }

        // 测试等待步骤进度事件，使用等待的毫秒数及步骤名称
        private void BaseTest_WaitStepTick(int arg1, string arg2)
        {
            // 可用于显示等待步骤的进度
            // 使用UI更新逻辑，例如进度条或标签
            // 禁止使用LabTestTime控件进行刷新，此控件只做试验总时间用
            // uiPanel2.Text = $"{arg2}:{arg1} 秒";  // 显示具体时间
        }

        // 测试等待进度事件，使用等待的毫秒数
        private void BaseTest_WaitTick(int obj)
        {
            // 可用于显示等待的进度
            // 使用UI更新逻辑，例如进度条或标签
            // 禁止使用LabTestTime控件进行刷新，此控件只做试验总时间用
            // uiPanel2.Text = $"{arg2}:{arg1} 秒";  // 显示具体时间
        }

        // 设置初始状态
        private void SetInitialState()
        {
            btnStopTest.Enabled = false;
        }

        // 测试提示信息变更事件处理程序
        private void BaseTest_TipsChanged(object sender, object info)
        {
            AppendText(info.ToString());
        }

        // 测试状态变更事件处理程序
        private void BaseTest_TestStateChanged(bool isTesting)
        {
            Disable(isTesting);
        }

        /// <summary>
        /// 根据测试状态启用或禁用控件
        /// </summary>
        /// <param name="isTesting">是否正在测试中</param>
        private void Disable(bool isTesting)
        {
            // 确保在UI线程执行
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(Disable), isTesting);
                return;
            }

            btnStopTest.Enabled = isTesting;  // 停止按钮在测试时启用
            btnStartTest.Enabled = !isTesting; // 开始按钮在测试时禁用
            _allowCheckChange = !isTesting; // 测试时禁止更改勾选状态

            //  TableService 处理表格列的状态
            //_tableService.SetCheckColumnAutoCheck(!isTesting);

            // 在测试进行时禁用的控件组
            var controlsToDisable = new Control[]
            {
                btnProductSelection, // 产品选择按钮
                panelHand         // 手动控制面板
            };

            // 批量设置控件状态
            foreach (var control in controlsToDisable)
            {
                control.Enabled = !isTesting;
            }

            Refresh(); // 触发UI更新
        }

        /// <summary>
        /// 初始化报表
        /// </summary>
        /// <param name="reportFileName">报表文件名</param>
        private void InitializeReport(string reportFileName)
        {
            try
            {
                if (string.IsNullOrEmpty(reportFileName))
                    return;

                reportFileName = ReportService.GetDefaultReportPath() + reportFileName;

                // 检查文件是否存在
                if (!_reportService.FileExists(reportFileName))
                {
                    MessageHelper.MessageOK($"报表文件不存在：{reportFileName}", TType.Error);
                    return;
                }

                string workingPath = ReportService.GetWorkingReportPath();

                // 如果当前加载的不是这个文件，则重新加载
                //if (rWReport.Filename != workingPath)
                {
                    // 准备报表控件
                    rWReport.Dock = DockStyle.Fill;
                    if (!panelReport.Controls.Contains(rWReport))
                        panelReport.Controls.Add(rWReport);

                    // 关闭Excel进程
                    ProcessHelper.KillProcess("EXCEL");
                    Thread.Sleep(200);

                    // 关闭WPS进程
                    ProcessHelper.KillProcess("et");      // WPS表格
                    //ProcessHelper.KillProcess("wps");     // WPS文字  
                    //ProcessHelper.KillProcess("wpp");     // WPS演示
                    Thread.Sleep(200);

                    // 复制文件到工作目录
                    _reportService.CopyReportFile(reportFileName, workingPath);

                    // 初始化报表控件
                    BaseTest.Frm = frm;
                    BaseTest.Report = rWReport;
                    rWReport.Filename = workingPath;
                    rWReport.Init();

                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("报表加载错误：", ex);
                MessageHelper.MessageOK($"报表加载错误：{ex.Message}", TType.Error);
            }
        }
        #endregion

        #region 值改变事件
        public void TestCongrp_TestConGroupChanged(object sender, int index, object value)
        {
            if (controls.DigitalOutputs.TryGetValue(index, out UISwitch iSwitch))
            {
                iSwitch.Active = value.ToBool();
            }
            if (controls.DigitalOutputButtons.TryGetValue(index, out UIButton btn))
            {
                NavigationButtonStyles.BtnColor(btn, value.ToBool());
            }
        }

        public void Currentgrp_CurrentvalueGrpChaned(object sender, int index, double value)
        {
            if (controls.TemperaturePoints.TryGetValue(index, out Label label))
            {
                label.Text = value.ToString("f1");
            }
        }
        public void AIgrp_AIvalueGrpChanged(object sender, int index, double value)
        {
            if (controls.AnalogInputs.TryGetValue(index, out Label label))
            {
                label.Text = value.ToString("f1");
            }
        }

        public void AOgrp_AOvalueGrpChanged(object sender, int index, double value)
        {
            switch (index)
            {
                default:
                    break;
            }
        }

        public async void DIgrp_DIGroupChanged(object sender, int index, bool value)
        {
            try
            {
                if (index == 0)
                {
                    if (!value) await IsTestEndAsync();
                    EmergencyStatusChanged?.Invoke(value);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("DI模块事件报错：" + ex.Message);
            }
        }

        public void DOgrp_DOgrpChanged(object sender, int index, bool value) { }

        public void WSDCongrp_WSDConGroupChanged(object sender, int index, object value)
        {
            if (controls.DigitalOutputs.TryGetValue(index, out UISwitch iSwitch))
            {
                iSwitch.Active = value.ToBool();
            }
            if (controls.DigitalOutputButtons.TryGetValue(index, out UIButton btn))
            {
                NavigationButtonStyles.BtnColor(btn, value.ToBool());
            }
        }

        /// <summary>
        /// 手自动切换
        /// </summary>
        private void RadioAuto_CheckedChanged(object sender, EventArgs e)
        {
            OPCHelper.TestCongrp[0] = RadioAuto.Checked;
        }
        #endregion

        #region 参数
        //private void InitParaConfig()
        //{
        //    try
        //    {
        //        if (VarHelper.TestViewModel == null) return;

        //        // 初始化和加载参数配置
        //        paraconfig = new ParaConfig();
        //        paraconfig.SetSectionName(VarHelper.ModelTypeName);
        //        paraconfig.Load();
        //        BaseTest.ParaConfig = paraconfig;

        //        // 初始化测试项
        //        _tableService.LoadTestItems();

        //        // 初始化报表
        //        InitializeReport(paraconfig.RptFile);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageHelper.MessageOK($"加载参数错误：{ex.Message}");
        //        NlogHelper.Default.Error($"加载参数错误", ex);
        //    }
        //}

        ////刷新型号
        //public async void ParaRefresh()
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(txtModel.Text)) return;
        //        // 加载参数
        //        InitParaConfig();
        //        // 加载工作流配置
        //        if (_workflowService != null)
        //        {
        //            await _workflowService.LoadConfigurationsAsync(
        //                VarHelper.TestViewModel.ModelTypeName,
        //                VarHelper.TestViewModel.ModelName);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageHelper.MessageYes("刷新型号错误：" + ex.Message);
        //    }
        //}
        #endregion

        #region 刷新方法拆分

        /// <summary>
        /// 刷新参数配置
        /// </summary>
        public void RefreshParaConfig()
        {
            try
            {
                if (VarHelper.TestViewModel == null) return;

                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(VarHelper.ModelTypeName);
                paraconfig.Load();
                BaseTest.ParaConfig = paraconfig;

                NlogHelper.Default.Debug("参数配置已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"刷新参数配置失败: {ex.Message}", ex);
                MessageHelper.MessageOK($"刷新参数配置失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 刷新测试项列表
        /// </summary>
        public void RefreshTestItems()
        {
            try
            {
                _tableService.LoadTestItems();
                NlogHelper.Default.Debug("测试项列表已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"刷新测试项失败: {ex.Message}", ex);
                MessageHelper.MessageOK($"刷新测试项失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 刷新工作流配置
        /// </summary>
        public async Task RefreshWorkflowConfigAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(txtModel.Text)) return;

                if (_workflowService != null)
                {
                    await _workflowService.LoadConfigurationsAsync(
                        VarHelper.TestViewModel.ModelTypeName,
                        VarHelper.TestViewModel.ModelName);

                    NlogHelper.Default.Debug("工作流配置已刷新");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"刷新工作流配置失败: {ex.Message}", ex);
                MessageHelper.MessageOK($"刷新工作流配置失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 刷新报表
        /// </summary>
        public void RefreshReport()
        {
            try
            {
                if (VarHelper.TestViewModel == null) return;

                // 重新加载参数以获取最新的报表文件名
                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(VarHelper.ModelTypeName);
                paraconfig.Load();

                InitializeReport(paraconfig.RptFile);
                NlogHelper.Default.Debug("报表已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"刷新报表失败: {ex.Message}", ex);
                MessageHelper.MessageOK($"刷新报表失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 完整刷新（原 InitParaConfig 方法）
        /// </summary>
        private void InitParaConfig()
        {
            try
            {
                if (VarHelper.TestViewModel == null) return;

                // 刷新参数配置
                RefreshParaConfig();

                // 刷新测试项
                RefreshTestItems();

                // ❌ 移除报表刷新 - 只在特定场景下刷新
                // InitializeReport(paraconfig.RptFile);
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载参数错误：{ex.Message}");
                NlogHelper.Default.Error($"加载参数错误", ex);
            }
        }

        /// <summary>
        /// 刷新型号相关配置（原 ParaRefresh 方法）
        /// </summary>
        /// <param name="includeReport">是否同时刷新报表</param>
        public async Task ParaRefreshAsync(bool includeReport = false)
        {
            try
            {
                if (string.IsNullOrEmpty(txtModel.Text)) return;

                // 刷新参数配置
                RefreshParaConfig();

                // 刷新测试项
                RefreshTestItems();

                // 刷新工作流配置
                await RefreshWorkflowConfigAsync();

                // 可选：刷新报表
                if (includeReport)
                {
                    RefreshReport();
                }

                NlogHelper.Default.Info($"型号刷新完成（报表刷新: {includeReport}）");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"刷新型号错误: {ex.Message}", ex);
                MessageHelper.MessageYes($"刷新型号错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 向后兼容的无参方法
        /// </summary>
        public async void ParaRefresh()
        {
            await ParaRefreshAsync(includeReport: false);
        }

        #endregion

        #region 自动试验
        private CancellationTokenSource _cancellationTokenSource = new();
        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            _isTestActuallyStarted = false; // 初始化为false
            try
            {
                //  检查前置条件
                (bool Result, string txt) = FrmText();
                if (!Result)
                {
                    MessageHelper.MessageOK(txt, TType.Error);
                    return;
                }

                // 前置条件满足，标记试验开始
                _isTestActuallyStarted = true;

                //  设置UI状态
                Disable(true);
                TestStateChanged?.Invoke(true, true);

                // 重置所有项目的颜色状态
                _tableService.ResetAllItemPointColors();

                //  初始化取消令牌
                _cancellationTokenSource = new CancellationTokenSource();

                //  启动计时器
                _ = _countdownService.StartCountup(_cancellationTokenSource.Token);

                //  获取要执行的测试项
                var checkedItems = GetCheckedTestItemNames();

                // 6. 批量执行工作流
                if (_workflowService != null && checkedItems.Count > 0)
                {
                    // 试验前排空所有气压
                    bool exhaustSuccess = await ExhaustkPaAsync(_cancellationTokenSource.Token);

                    var results = await _workflowService.ExecuteMultipleWorkflowsAsync(
                        checkedItems,
                        VarHelper.TestViewModel.ModelTypeName,
                        VarHelper.TestViewModel.ModelName);

                    // 显示执行结果摘要
                    int successCount = results.Count(r => r.Value);
                    AppendText($"\n========== 执行完成: {successCount}/{results.Count} 成功 ==========");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"测试执行错误：{ex.Message}", ex);
                MessageHelper.MessageOK($"试验开始错误：{ex.Message}");
            }
            finally
            {
                // 只有试验真正开始了才执行结束逻辑
                if (_isTestActuallyStarted)
                {
                    await IsTestEndAsync();
                }
            }
        }

        #region 试验排空气压
        // 开启或关闭所有阀门
        private void ValveType(bool value)
        {
            OPCHelper.AOgrp.CA00 = 0.0;
            OPCHelper.DOgrp[1] = value;
            OPCHelper.DOgrp[2] = value;
            OPCHelper.DOgrp[3] = value;
            OPCHelper.DOgrp[4] = value;
            OPCHelper.DOgrp[5] = value;
            OPCHelper.DOgrp[8] = value;
            OPCHelper.DOgrp[9] = value;
            OPCHelper.DOgrp[10] = value;
            OPCHelper.DOgrp[11] = value;
            OPCHelper.DOgrp[12] = value;
            OPCHelper.DOgrp[13] = value;
        }

        /// <summary>
        /// 异步排空所有气压 - 不阻塞UI线程
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>true=排空成功, false=超时或取消</returns>
        private async Task<bool> ExhaustkPaAsync(CancellationToken cancellationToken)
        {
            try
            {
                // 打开所有阀门
                ValveType(true);

                AppendText("正在排空气压,请稍候...");

                // 使用异步等待,不阻塞UI线程
                bool isTimeout = await DelayAsync(
                    seconds: 30,
                    interval: 100,
                    wait: () =>
                    {
                        // 等待所有气压排空完成
                        return (
                            OPCHelper.AIgrp[1] < 5 &&
                            OPCHelper.AIgrp[2] < 5 &&
                            OPCHelper.AIgrp[3] < 5 &&
                            OPCHelper.AIgrp[4] < 5 &&
                            OPCHelper.AIgrp[5] < 5 &&
                            OPCHelper.AIgrp[6] < 5
                        );
                    },
                    cancellationToken
                );

                // 关闭所有阀门
                ValveType(false);

                if (isTimeout)
                {
                    AppendText("气压排空超时(30秒),但已关闭阀门");
                    return false;
                }
                else
                {
                    AppendText("气压排空完成");
                    return true;
                }
            }
            catch (OperationCanceledException)
            {
                // 取消操作时也要关闭阀门
                ValveType(false);
                AppendText("气压排空被取消");
                return false;
            }
            catch (Exception ex)
            {
                // 异常时确保关闭阀门
                ValveType(false);
                AppendText($"排空气压异常:{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 异步延时方法 - 带条件检查,不阻塞UI线程
        /// </summary>
        /// <param name="seconds">延时秒数</param>
        /// <param name="interval">检查间隔(毫秒)</param>
        /// <param name="wait">等待条件(返回false时继续等待,返回true时退出)</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>是否超时(true=超时,false=条件满足提前退出)</returns>
        private async Task<bool> DelayAsync(double seconds, int interval, Func<bool> wait, CancellationToken cancellationToken)
        {
            var elapsed = 0;
            var timeout = seconds * 1000;

            while (elapsed <= timeout && !wait() && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(interval, cancellationToken);
                elapsed += interval;
            }

            return elapsed > timeout; // true=超时, false=条件满足
        }
        #endregion

        /// <summary>
        /// 获取选中的测试项名称列表
        /// </summary>
        private List<string> GetCheckedTestItemNames()
        {
            var items = new List<string>();

            try
            {
                // 从数据源 _itemPoints 中筛选选中的项
                foreach (var itemPoint in _itemPoints)
                {
                    if (itemPoint.Check) // Check 属性表示是否选中
                    {
                        if (!string.IsNullOrEmpty(itemPoint.ItemName))
                        {
                            items.Add(itemPoint.ItemName);
                        }
                    }
                }

                _logger?.LogInformation("找到 {Count} 个选中的测试项", items.Count);

                if (items.Count == 0)
                {
                    _logger?.LogWarning("没有选中任何测试项");
                }

                return items;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "获取选中测试项失败");
                NlogHelper.Default.Error("获取选中测试项失败", ex);
                return [];
            }
        }

        private async void btnStopTest_Click(object sender, EventArgs e) => await IsTestEndAsync();

        private (bool Result, string txt) FrmText()
        {
            if (!OPCHelper.DIgrp[0])
            {
                return (false, "请注意，急停情况下无法启动自动试验!");
            }
            if (string.IsNullOrEmpty(VarHelper.TestViewModel.ModelName))
            {
                return (false, "未选择型号，无法启动自动试验!");
            }
            if (!RadioAuto.Checked)
            {
                return (false, "请注意，手动情况下无法启动自动试验!");
            }
            if (string.IsNullOrEmpty(paraconfig.RptFile))
            {
                return (false, "请注意，该型号[试验报表模板]未设置，无法启动自动试验!\n请请往 [参数管理] ➡️[试验参数] ➡️[报表模板]中设置！");
            }
            if (_itemPoints == null || !_itemPoints.Any(item => item.Check))
            {
                return (false, "请至少选择一个测试项!");
            }
            return (true, "");
        }

        // 结束试验操作
        private async Task IsTestEndAsync()
        {
            try
            {
                AppendText("试验结束");

                // 停止工作流执行
                _cancellationTokenSource.Cancel();
                _workflowService?.StopExecution();
                _countdownService.StopCountdown();

                await ExhaustkPaAsync(CancellationToken.None);

                // 恢复UI状态 - 确保在UI线程
                if (InvokeRequired)
                {
                    Invoke(new Action(() => Disable(false)));
                }
                else
                {
                    Disable(false);
                }

                TestStateChanged?.Invoke(false, false);
                _isTestActuallyStarted = false;
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine($"Task被取消: {ex}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"结束试验错误：{ex.Message}", ex);
                MessageHelper.MessageOK(frm, $"结束试验错误：{ex.Message}");
            }
        }

        #endregion

        #region 模拟量设置
        private void btnNozzleMotor_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as UIButton;
                using frmSetOutValue fs = new(OPCHelper.TestCongrp[btn.Tag.ToInt32()].ToDouble(), btn.Text, 10000);
                VarHelper.ShowDialogWithOverlay(frm, fs);
                if (fs.DialogResult == DialogResult.OK)
                {
                    ControlHelper.ButtonClickAsync(sender, () =>
                    {
                        //LabAO01.Text = fs.OutValue.ToString();
                        OPCHelper.TestCongrp[btn.Tag.ToInt32()] = fs.OutValue;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(frm, $"模拟量设定错误：{ex.Message}");
            }
        }
        #endregion

        #region 报表控件

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateSaveParameters()) return;// 参数校验

                if (!ConfirmSaveReport()) return;  // 提示确认

                string saveFilePath = ReportService.BuildSaveFilePath(
                    VarHelper.TestViewModel.ModelTypeName,
                    VarHelper.TestViewModel.ModelName); // 保存路径

                var testRecord = new TestRecordModel
                {
                    KindID = VarHelper.TestViewModel.ModelTypeID,
                    ModelID = VarHelper.TestViewModel.ID,
                    TestID = VarHelper.TestViewModel.DrawingNo,
                    Tester = NewUsers.NewUserInfo.Username,
                    TestTime = DateTime.Now,
                    ReportPath = saveFilePath,
                    TestBenchID = TestBenchService.CurrentTestBenchID // 使用当前工位的ID
                };

                if (ReportService.SaveTestRecord(testRecord))
                {
                    rWReport.SaveAS(saveFilePath);
                    MessageHelper.MessageOK("保存成功", TType.Success);
                }
                else
                {
                    MessageHelper.MessageOK("保存失败", TType.Error);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"保存失败: {ex.Message}", TType.Error);
            }
        }

        // 保存试验报告前参数校验
        private static bool ValidateSaveParameters()
        {
            if (string.IsNullOrEmpty(paraconfig?.RptFile))
            {
                MessageHelper.MessageOK("报表模板未设置，无法保存！", TType.Warn);
                return false;
            }

            if (string.IsNullOrEmpty(VarHelper.TestViewModel.ModelName))
            {
                MessageHelper.MessageOK("型号未选择！", TType.Warn);
                return false;
            }
            return true;
        }

        // 确认保存报表
        private static bool ConfirmSaveReport() =>
            MessageHelper.MessageYes("是否保存试验结果？") == DialogResult.OK;

        public int curRows = 1; //当前行数
        public int MaxcurRows = 1000; //默认最大行数
        /// <summary>
        /// 向上翻页
        /// </summary>
        private void BtnPageUp_Click(object sender, EventArgs e)
        {
            try
            {
                int pageSize = Convert.ToInt32(inputNumber.Value);
                var (currentRows, upEnabled, downEnabled) = _reportService.PageUp(pageSize);

                // 更新按钮状态
                btnPageUp.Enabled = upEnabled;
                btnPageDown.Enabled = downEnabled;

                // 显示当前页信息
                // XX.Text = $"第 {currentRows} 行";
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"向上翻页失败：{ex.Message}", TType.Error);
                NlogHelper.Default.Error("报表向上翻页失败", ex);
            }
        }

        /// <summary>
        /// 向下翻页
        /// </summary>
        private void BtnPageDown_Click(object sender, EventArgs e)
        {
            try
            {
                int pageSize = Convert.ToInt32(inputNumber.Value);
                var (currentRows, upEnabled, downEnabled) = _reportService.PageDown(pageSize);

                // 更新按钮状态
                btnPageUp.Enabled = upEnabled;
                btnPageDown.Enabled = downEnabled;

                // 显示当前页信息
                // XX.Text = $"第 {currentRows} 行";
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"向下翻页失败：{ex.Message}", TType.Error);
                NlogHelper.Default.Error("报表向下翻页失败", ex);
            }
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            rWReport.Print();
        }
        #endregion

        #region 其他
        private void AppendText(string text)
        {
            // 由于分辨率过小原因，暂时没有使用试验消息提示

            //txtTestRecord.AppendText($"{DateTime.Now:HH:mm:ss}：{text}\n");
            //txtTestRecord.ScrollToCaret();
        }

        private void btnTechnology_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 0;
            NavigationButtonStyles.UpdateNavigationButtons
                (tabs1.SelectedIndex, controls);
        }

        private void btnCurve_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 1;
            NavigationButtonStyles.UpdateNavigationButtons
                (tabs1.SelectedIndex, controls);
        }

        /// <summary>
        /// 试验详情按钮点击事件
        /// </summary>
        private void btnTestDetails_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 2; // 切换到试验详情页面
            NavigationButtonStyles.UpdateNavigationButtons(
                tabs1.SelectedIndex, controls);
        }

        private async void btnProductSelection_Click(object sender, EventArgs e)
        {
            using FrmSpec frmSpec = new();
            VarHelper.ShowDialogWithOverlay(frm, frmSpec);
            if (frmSpec.DialogResult == DialogResult.OK)
            {
                txtModel.Text = VarHelper.TestViewModel.ModelName;
                txtType.Text = VarHelper.TestViewModel.ModelTypeName;
                txtDrawingNo.Text = VarHelper.TestViewModel.DrawingNo;

                //  选择新型号后，完整刷新（包括报表）
                await ParaRefreshAsync(includeReport: true);
            }
        }

        private void btnWaterPumpStart_Click(object sender, EventArgs e)
        {
            var btn = sender as UIButton;
            OPCHelper.TestCongrp[btn.Tag.ToInt32()] =
                !OPCHelper.TestCongrp[btn.Tag.ToInt32()].ToBool();
        }

        private async void btnFaultRemoval_Click(object sender, EventArgs e)
        {
            var btn = sender as UIButton;
            await FaultClearingAsync(btn);
        }
        private async Task FaultClearingAsync(UIButton btn)
        {
            OPCHelper.TestCongrp[btn.Tag.ToInt32()] = true;
            await Task.Delay(1000);
            OPCHelper.TestCongrp[btn.Tag.ToInt32()] = false;
        }

        private void uiSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            var sder = sender as UISwitch;
            OPCHelper.TestCongrp[sder.Tag.ToInt32()] = true;
        }

        private void uiSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            var sder = sender as UISwitch;
            OPCHelper.TestCongrp[sder.Tag.ToInt32()] = false;
        }

        #endregion
    }
}
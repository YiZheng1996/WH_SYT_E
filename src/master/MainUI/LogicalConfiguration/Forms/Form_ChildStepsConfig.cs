using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 循环体子步骤配置窗体
    /// </summary>
    public partial class Form_ChildStepsConfig : UIForm
    {
        #region 字段

        // UI控件
        private ToolTreeViewControl _toolTreeControl;
        private ProcessDataGridViewControl _processGridControl;
        private UISymbolButton btnSave;
        private UISymbolButton btnCancel;

        // 服务依赖
        private readonly ILogger<Form_ChildStepsConfig> _logger;
        private readonly IFormService _formService;
        private readonly IWorkflowStateService _workflowState; // 子步骤专用的工作流状态

        // 数据
        public List<ChildModel> _childSteps;
        private readonly List<ChildModel> _originalSteps;
        private bool _hasUnsavedChanges = false;

        // 状态颜色
        private static readonly Color PrimaryBlue = Color.FromArgb(65, 100, 204);

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="childSteps">要编辑的子步骤列表</param>
        /// <param name="logger">日志服务(可选)</param>
        public Form_ChildStepsConfig(
            List<ChildModel> childSteps,
            ILogger<Form_ChildStepsConfig> logger = null)
        {
            _logger = logger;
            _originalSteps = childSteps;

            // ⭐ 直接使用传入的列表，不深拷贝
            _childSteps = childSteps ?? [];

            // 获取服务
            _formService = Program.ServiceProvider?.GetService<IFormService>();

            // 创建独立的本地工作流状态服务实例
            _workflowState = new WorkflowStateService();

            // 初始化子步骤到本地工作流状态（不影响全局）
            foreach (var step in _childSteps)
            {
                _workflowState.AddStep(step);
            }

            InitializeComponent();
            InitializeCustomUI();
            RegisterEventHandlers();

            _processGridControl.RefreshGrid();
            _logger?.LogDebug("循环体子步骤配置窗体已创建,步骤数量: {Count}", _childSteps.Count);
        }

        #endregion

        #region 初始化UI

        /// <summary>
        /// 初始化自定义UI
        /// </summary>
        private void InitializeCustomUI()
        {
            try
            {
                // 创建工具箱控件(不允许循环控制)
                var toolLogger = Program.ServiceProvider?.GetService<ILogger<ToolTreeViewControl>>();
                _toolTreeControl = new ToolTreeViewControl(toolLogger)
                {
                    Dock = DockStyle.Fill,
                    Title = "工具箱"
                };

                // 添加到左侧容器
                panelToolBox.Controls.Clear();
                panelToolBox.Controls.Add(_toolTreeControl);

                // 初始化工具箱(过滤掉循环控制相关步骤)
                InitializeToolBox();

                // 创建流程表格控件
                var gridLogger = Program.ServiceProvider?.GetService<ILogger<ProcessDataGridViewControl>>();
                _processGridControl = new ProcessDataGridViewControl(_workflowState, gridLogger)
                {
                    Dock = DockStyle.Fill
                };

                // 添加到中间容器
                panelProcess.Controls.Clear();
                panelProcess.Controls.Add(_processGridControl);

                // 创建底部按钮
                CreateButtons();

                // 设置窗体样式
                Text = "循环体子步骤配置";
                TitleColor = PrimaryBlue;
                ShowRadius = false;
                Size = new Size(1200, 700);
                StartPosition = FormStartPosition.CenterParent;

                _logger?.LogDebug("自定义UI初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化自定义UI失败");
                throw;
            }
        }

        /// <summary>
        /// 初始化工具箱(过滤循环控制步骤)
        /// </summary>
        private void InitializeToolBox()
        {
            try
            {
                // 禁止的步骤类型(避免嵌套循环)
                var disallowedSteps = new HashSet<string>
        {
            "LoopControlStart",  // 循环开始
            "LoopControlStop",   // 循环结束
            "Loop",              // 循环(如果有的话)
        };

                // 清空工具箱
                _toolTreeControl.ClearTools();

                // 逻辑控制组(排除循环相关)
                TreeNode logicNode = _toolTreeControl.AddToolNode(
                    "逻辑控制",
                    "LogicControl",
                    "文件夹.png"
                );
                _toolTreeControl.AddToolNode("延时等待", "DelayWait", "延时等待.png", logicNode);
                _toolTreeControl.AddToolNode("条件判断", "ConditionJudge", "条件判断.png", logicNode);
                _toolTreeControl.AddToolNode("等待稳定", "Waitingforstability", "等待稳定.png", logicNode);
                _toolTreeControl.AddToolNode("循环工具", "CycleBegins", "等待稳定.png", logicNode);
                _toolTreeControl.AddToolNode("实时监控", "MonitorTool", "检测工具.png", logicNode);

                // 数据操作组
                TreeNode dataNode = _toolTreeControl.AddToolNode(
                    "数据操作",
                    "DataOperation",
                    "文件夹.png"
                );
                _toolTreeControl.AddToolNode("变量赋值", "VariableAssign", "变量赋值.png", dataNode);
                _toolTreeControl.AddToolNode("消息通知", "", "消息通知.png", dataNode);

                // PLC通信组
                TreeNode plcNode = _toolTreeControl.AddToolNode(
                    "通信操作",
                    "PLCCommunication",
                    "文件夹.png"
                );
                _toolTreeControl.AddToolNode("读取PLC", "PLCRead", "读取PLC.png", plcNode);
                _toolTreeControl.AddToolNode("写入PLC", "PLCWrite", "写入PLC.png", plcNode);

                // 检测工具组
                TreeNode detectionNode = _toolTreeControl.AddToolNode(
                    "检测工具",
                    "DetectionTools",
                    "文件夹.png"
                );
                _toolTreeControl.AddToolNode("AI检测", "AIDetection", "检测工具.png", detectionNode);

                // 报表操作组
                TreeNode reportNode = _toolTreeControl.AddToolNode(
                    "报表工具",
                    "ReportTools",
                    "文件夹.png"
                );
                _toolTreeControl.AddToolNode("读取单元格", "ReadExcelCell", "报表读取.png", reportNode);
                _toolTreeControl.AddToolNode("写入单元格", "WriteExcelCell", "报表写入.png", reportNode);

                // 展开所有节点
                _toolTreeControl.ExpandAll();

                _logger?.LogDebug("工具箱初始化完成(已过滤循环控制步骤)");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化工具箱失败");
                throw;
            }
        }

        /// <summary>
        /// 创建底部按钮
        /// </summary>
        private void CreateButtons()
        {
            // 保存按钮
            btnSave = new UISymbolButton
            {
                Name = "btnSave",
                Text = "保存",
                Symbol = 61639,
                SymbolSize = 24,
                FillColor = PrimaryBlue,
                ForeColor = Color.White,
                Size = new Size(120, 40),
                Radius = 5,
                Cursor = Cursors.Hand,
                Location = new Point(panelButtons.Width - 260, 10)
            };
            btnSave.Click += BtnSave_Click;

            // 取消按钮
            btnCancel = new UISymbolButton
            {
                Name = "btnCancel",
                Text = "取消",
                Symbol = 61453,
                SymbolSize = 24,
                FillColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Size = new Size(120, 40),
                Radius = 5,
                Cursor = Cursors.Hand,
                Location = new Point(panelButtons.Width - 130, 10)
            };
            btnCancel.Click += BtnCancel_Click;

            panelButtons.Controls.AddRange([btnSave, btnCancel]);
        }

        #endregion

        #region 事件注册
        /// <summary>
        /// 注册事件处理程序
        /// </summary>
        private void RegisterEventHandlers()
        {
            try
            {
                // 工具箱事件
                _toolTreeControl.ToolSelected += OnToolSelected;

                // 流程表格事件
                _processGridControl.StepConfigRequested += OnStepConfigRequested;
                _processGridControl.DragDropEvent += OnProcessGridDragDrop;
                _processGridControl.SelectionChangedEvent += OnGridSelectionChanged;
                _processGridControl.StepsChanged += OnStepsChanged;

                _logger?.LogDebug("事件处理程序已注册");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "注册事件处理程序失败");
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 工具选择事件(拖拽)
        /// </summary>
        private void OnToolSelected(object sender, ToolSelectedEventArgs e)
        {
            _logger?.LogDebug("工具被选择: {ToolName}", e.ToolName);
            _hasUnsavedChanges = true;
        }

        /// <summary>
        /// 步骤配置请求事件(双击行)
        /// </summary>
        private void OnStepConfigRequested(object sender, StepConfigEventArgs e)
        {
            try
            {
                if (_formService == null)
                {
                    MessageHelper.MessageOK("无法打开配置窗体,服务未初始化", TType.Error);
                    return;
                }

                var globalWorkflowState = Program.ServiceProvider?.GetService<IWorkflowStateService>();
                if (globalWorkflowState == null)
                {
                    MessageHelper.MessageOK("系统错误：无法获取工作流服务", TType.Error);
                    return;
                }

                // 从工作流状态获取实际的步骤对象(基于当前表格位置)
                var localStep = _workflowState.GetStep(e.RowIndex);
                if (localStep == null)
                {
                    _logger?.LogWarning("无法获取步骤: RowIndex={RowIndex}", e.RowIndex);
                    return;
                }

                _logger?.LogDebug("准备打开配置: StepNum={StepNum}, StepName={StepName}, RowIndex={RowIndex}",
                    localStep.StepNum, localStep.StepName, e.RowIndex);

                // 保存全局状态的原始值
                var originalStepNum = globalWorkflowState.StepNum;
                var originalStepName = globalWorkflowState.StepName;
                var originalSteps = globalWorkflowState.GetSteps(); // 备份全局步骤列表

                try
                {
                    // 临时替换：清空全局状态，只添加当前要配置的子步骤
                    globalWorkflowState.ClearSteps();
                    globalWorkflowState.AddStep(localStep);
                    globalWorkflowState.StepNum = 0;  // 配置窗体使用索引0
                    globalWorkflowState.StepName = e.Step.StepName;

                    _logger?.LogDebug("已设置全局状态为子步骤配置模式");

                    // 打开配置窗体（会修改全局状态索引0的步骤）
                    _formService.OpenFormByName(this, e.Step.StepName, this);

                    // 从全局状态获取修改后的参数
                    var updatedStep = globalWorkflowState.GetStep(0);
                    if (updatedStep != null && updatedStep.StepParameter != null)
                    {
                        //var settings = new JsonSerializerSettings
                        //{
                        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        //    NullValueHandling = NullValueHandling.Ignore
                        //};

                        // 序列化为JSON字符串,切断引用链
                        //string paramJson = JsonConvert.SerializeObject(updatedStep.StepParameter);

                        //_logger?.LogDebug("参数已序列化: Length={Length}", paramJson?.Length ?? 0);

                        // 关键修改: 使用对象引用而非索引来更新参数
                        // 方法1: 直接更新 localStep 对象(因为它是从 _workflowState 获取的引用)
                        //localStep.StepParameter = paramJson;
                        _logger?.LogDebug("已更新 localStep.StepParameter");

                        // 方法2: 同时在 _childSteps 中查找并更新对应的步骤
                        // 通过 StepNum 和 StepName 双重匹配确保准确性
                        var matchingStep = _childSteps.FirstOrDefault(s =>
                            s.StepNum == localStep.StepNum &&
                            s.StepName == localStep.StepName);

                        if (matchingStep != null)
                        {
                            //matchingStep.StepParameter = paramJson;
                            _logger?.LogDebug("已更新 _childSteps 中的匹配步骤: StepNum={StepNum}, StepName={StepName}",
                                matchingStep.StepNum, matchingStep.StepName);
                        }
                        else
                        {
                            _logger?.LogWarning("在 _childSteps 中未找到匹配的步骤: StepNum={StepNum}, StepName={StepName}",
                                localStep.StepNum, localStep.StepName);
                        }

                        // 方法3: 额外的安全措施 - 确保 _originalSteps 也同步
                        // (因为保存时会同步 _originalSteps 和 _childSteps)
                        var originalMatchingStep = _originalSteps.FirstOrDefault(s =>
                            s.StepNum == localStep.StepNum &&
                            s.StepName == localStep.StepName);

                        if (originalMatchingStep != null)
                        {
                            //originalMatchingStep.StepParameter = paramJson;
                            _logger?.LogDebug("已更新 _originalSteps 中的匹配步骤");
                        }
                    }
                    else
                    {
                        _logger?.LogDebug("配置窗体返回的步骤参数为空");
                    }
                }
                finally
                {
                    // ⭐ 恢复全局状态到原始状态
                    globalWorkflowState.ClearSteps();
                    if (originalSteps != null)
                    {
                        foreach (var step in originalSteps)
                        {
                            globalWorkflowState.AddStep(step);
                        }
                    }
                    globalWorkflowState.StepNum = originalStepNum;
                    globalWorkflowState.StepName = originalStepName;

                    _logger?.LogDebug("已恢复全局状态");
                }

                _hasUnsavedChanges = true;
                _processGridControl?.RefreshGrid();

                _logger?.LogInformation("步骤配置完成: StepName={StepName}", localStep.StepName);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "打开步骤配置失败");
                MessageHelper.MessageOK($"打开步骤配置失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 流程表格拖放事件
        /// </summary>
        private void OnProcessGridDragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        // 创建新步骤
                        var newStep = new ChildModel
                        {
                            StepNum = _workflowState.GetStepCount() + 1,
                            StepName = node.Text,
                            StepParameter = null,
                            Remark = string.Empty
                        };

                        // 添加到工作流状态
                        _workflowState.AddStep(newStep);

                        // 刷新表格
                        _processGridControl.RefreshGrid();

                        _hasUnsavedChanges = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "拖拽步骤错误");
            }
        }

        /// <summary>
        /// 表格选择改变事件
        /// </summary>
        private void OnGridSelectionChanged(object sender, EventArgs e)
        {
            // 可以在这里添加选择改变的逻辑
            _logger?.LogDebug("表格选择已改变");
        }

        /// <summary>
        /// 步骤列表改变事件
        /// </summary>
        private void OnStepsChanged(object sender, EventArgs e)
        {
            _logger?.LogDebug("步骤列表已改变");
            _hasUnsavedChanges = true;
        }

        /// <summary>
        /// 保存按钮点击
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var currentSteps = _workflowState.GetSteps();

                if (currentSteps == null || currentSteps.Count == 0)
                {
                    var result = MessageHelper.MessageYes(
                        "当前没有配置任何子步骤，确定要保存空配置吗？");
                    if (result != DialogResult.OK)
                        return;
                }

                // 直接清空并同步，不深拷贝（因为最终会序列化成字符串）
                _originalSteps.Clear();
                _originalSteps.AddRange(currentSteps);

                _logger?.LogInformation("成功保存 {Count} 个子步骤", _originalSteps.Count);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存子步骤失败");
                MessageHelper.MessageOK($"保存失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes("有未保存的更改,确定要取消吗?");
                if (result != DialogResult.OK)
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
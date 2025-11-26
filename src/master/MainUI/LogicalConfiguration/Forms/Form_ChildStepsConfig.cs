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
                    "循环开始",
                    "循环结束",
                    "For循环",
                    "While循环"
                };

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

                var localStep = _workflowState.GetStep(e.RowIndex);
                if (localStep == null) return;

                // ⭐ 保存全局状态的原始值
                var originalStepNum = globalWorkflowState.StepNum;
                var originalStepName = globalWorkflowState.StepName;
                var originalSteps = globalWorkflowState.GetSteps(); // 备份全局步骤列表

                try
                {
                    // ⭐ 临时替换：清空全局状态，只添加当前要配置的子步骤
                    globalWorkflowState.ClearSteps();
                    globalWorkflowState.AddStep(localStep);
                    globalWorkflowState.StepNum = 0;  // 配置窗体使用索引0
                    globalWorkflowState.StepName = e.Step.StepName;

                    // 打开配置窗体（会修改全局状态索引0的步骤）
                    _formService.OpenFormByName(this, e.Step.StepName, this);

                    // ⭐ 从全局状态获取修改后的参数
                    var updatedStep = globalWorkflowState.GetStep(0);
                    if (updatedStep != null)
                    {
                        // ⭐ 先序列化再反序列化，切断引用链
                        if (updatedStep.StepParameter != null)
                        {
                            var settings = new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            };

                            var paramJson = JsonConvert.SerializeObject(updatedStep.StepParameter, settings);
                            var paramCopy = JsonConvert.DeserializeObject(paramJson);

                            localStep.StepParameter = paramCopy;
                            _workflowState.UpdateStepParameter(e.RowIndex, localStep);

                            if (e.RowIndex >= 0 && e.RowIndex < _childSteps.Count)
                            {
                                _childSteps[e.RowIndex].StepParameter = paramCopy;
                            }
                        }

                    _logger?.LogDebug("已同步子步骤参数: {StepName}", e.Step.StepName);
                    }
                }
                finally
                {
                    // ⭐ 恢复全局状态
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
                }

                _hasUnsavedChanges = true;
                _processGridControl?.RefreshGrid();
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
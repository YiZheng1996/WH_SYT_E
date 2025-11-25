using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.LogicalManager;
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
        private readonly IWorkflowStateService _childWorkflowState; // 子步骤专用的工作流状态

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

            // 深拷贝子步骤列表
            _childSteps = childSteps != null
                ? JsonConvert.DeserializeObject<List<ChildModel>>(JsonConvert.SerializeObject(childSteps))
                : [];

            // 获取服务
            _formService = Program.ServiceProvider?.GetService<IFormService>();

            // 创建子步骤专用的工作流状态服务(独立实例)
            _childWorkflowState = new WorkflowStateService();

            // 初始化子步骤到工作流状态
            foreach (var step in _childSteps)
            {
                _childWorkflowState.AddStep(step);
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
                _processGridControl = new ProcessDataGridViewControl(_childWorkflowState, gridLogger)
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

                // 窗体关闭事件
                this.FormClosing += Form_ChildStepsConfig_FormClosing;

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
        /// 步骤配置请求事件(双击行) - 修复版本
        /// </summary>
        private void OnStepConfigRequested(object sender, StepConfigEventArgs e)
        {
            try
            {
                _logger?.LogDebug("打开步骤配置: {StepName}, 行索引: {RowIndex}",
                    e.Step.StepName, e.RowIndex);

                if (_formService == null)
                {
                    _logger?.LogWarning("FormService未初始化,无法打开配置窗体");
                    MessageHelper.MessageOK("无法打开配置窗体,服务未初始化", TType.Error);
                    return;
                }

                // ✅ 获取全局工作流状态服务
                var globalWorkflowState = Program.ServiceProvider?.GetService<IWorkflowStateService>();
                if (globalWorkflowState == null)
                {
                    _logger?.LogError("无法获取全局工作流状态服务");
                    MessageHelper.MessageOK("系统错误：无法获取工作流服务", TType.Error);
                    return;
                }

                // ✅ 设置全局工作流状态（配置窗体会使用）
                globalWorkflowState.StepNum = e.RowIndex;
                globalWorkflowState.StepName = e.Step.StepName;

                // ✅ 同时设置子工作流状态
                _childWorkflowState.StepNum = e.RowIndex;
                _childWorkflowState.StepName = e.Step.StepName;

                // ✅ 打开配置窗体
                _formService.OpenFormByName(this, e.Step.StepName, this);

                // ✅ 关键修复：配置完成后同步参数
                var globalStep = globalWorkflowState.GetStep(e.RowIndex);
                if (globalStep != null)
                {
                    var childStep = _childWorkflowState.GetStep(e.RowIndex);
                    if (childStep != null)
                    {
                        // 同步参数
                        childStep.StepParameter = globalStep.StepParameter;
                        _childWorkflowState.UpdateStepParameter(childStep.StepNum, childStep);

                        _logger?.LogDebug("已同步步骤参数: {StepName}, 参数长度: {Length}",
                            childStep.StepName,
                            globalStep.StepParameter?.ToString()?.Length ?? 0);
                    }
                }

                _hasUnsavedChanges = true;
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
                            StepNum = _childWorkflowState.GetStepCount() + 1,
                            StepName = node.Text,
                            StepParameter = null,
                            Remark = string.Empty
                        };

                        // 添加到工作流状态
                        _childWorkflowState.AddStep(newStep);

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
                // 1. 从工作流状态获取最新步骤
                var currentSteps = _childWorkflowState.GetSteps();

                if (currentSteps == null || currentSteps.Count == 0)
                {
                    var result = MessageHelper.MessageYes(
                        "当前没有配置任何子步骤，确定要保存空配置吗？");
                    if (result != DialogResult.OK)
                        return;
                }

                // 2. 深拷贝数据
                var deepCopiedSteps = JsonConvert.DeserializeObject<List<ChildModel>>(
                    JsonConvert.SerializeObject(currentSteps));

                // 同时更新两个列表
                _childSteps = deepCopiedSteps; // 供Form_Loop访问
                _originalSteps.Clear();
                _originalSteps.AddRange(deepCopiedSteps);   // 更新原始引用

                _logger?.LogInformation("子步骤配置已保存，步骤数: {Count}", _childSteps.Count);
                MessageHelper.MessageOK(
                    $"保存成功！共配置 {_childSteps.Count} 个子步骤",
                    TType.Success);

                _hasUnsavedChanges = false;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存子步骤配置失败");
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

        /// <summary>
        /// 窗体关闭前检查
        /// </summary>
        private void Form_ChildStepsConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK && _hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes("有未保存的更改,确定要关闭吗?");
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion


        #region 步骤操作

        /// <summary>
        /// 添加步骤到表单
        /// </summary>
        private void AddStepToForm(int stepNumber, string stepName)
        {
            var newStep = new ChildModel
            {
                StepName = stepName,
                Status = 0,
                StepNum = stepNumber,
                StepParameter = 0
            };

            // 可以通过控件添加
            _processGridControl.AddStep(newStep);
        }
        #endregion
    }
}
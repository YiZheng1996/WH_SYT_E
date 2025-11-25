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

        // 菜单管理器
        private StepContextMenuManager _menuManager;
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

                // 刷新表格数据
                _processGridControl.RefreshGrid();

                // 初始化右键菜单
                InitializeContextMenu();

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
        /// 初始化右键菜单
        /// </summary>
        private void InitializeContextMenu()
        {
            try
            {
                // 获取ProcessDataGridViewControl内部的DataGridView
                var dataGridView = _processGridControl.DataGridView;

                // 创建DataGridViewManager(用于菜单管理器)
                var gridManager = new DataGridViewManager(dataGridView);

                // 创建菜单管理器
                var menuLogger = Program.ServiceProvider?.GetService<ILogger<StepContextMenuManager>>();
                _menuManager = new StepContextMenuManager(
                    dataGridView,
                    _childWorkflowState,
                    gridManager,
                    menuLogger,
                    this);

                _logger?.LogDebug("右键菜单已初始化");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化右键菜单失败");
                // 菜单初始化失败不影响主功能,记录日志继续
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
        /// 步骤配置请求事件(双击行)
        /// </summary>
        private void OnStepConfigRequested(object sender, StepConfigEventArgs e)
        {
            try
            {
                _logger?.LogDebug("打开步骤配置: {StepName}, 行索引: {RowIndex}", e.Step.StepName, e.RowIndex);

                if (_formService != null)
                {
                    // 设置子工作流状态
                    _childWorkflowState.StepNum = e.RowIndex;
                    _childWorkflowState.StepName = e.Step.StepName;

                    // 打开配置窗体
                    _formService.OpenFormByName(this, e.Step.StepName, this);

                    _hasUnsavedChanges = true;
                }
                else
                {
                    _logger?.LogWarning("FormService未初始化,无法打开配置窗体");
                    MessageHelper.MessageOK("无法打开配置窗体,服务未初始化", TType.Error);
                }
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
                // 从工作流状态获取最新的步骤列表
                var currentSteps = _childWorkflowState.GetSteps();

                if (currentSteps == null || currentSteps.Count == 0)
                {
                    var result = MessageHelper.MessageYes("子步骤列表为空,确定要保存吗?");
                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                }

                // 更新原始引用(深拷贝回去)
                _originalSteps.Clear();
                _originalSteps.AddRange(
                    JsonConvert.DeserializeObject<List<ChildModel>>(
                        JsonConvert.SerializeObject(currentSteps)));

                _logger?.LogInformation("子步骤配置已保存,步骤数: {Count}", _originalSteps.Count);
                MessageHelper.MessageOK($"保存成功!共配置 {_originalSteps.Count} 个子步骤", TType.Success);

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
                if (result!= DialogResult.OK)
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
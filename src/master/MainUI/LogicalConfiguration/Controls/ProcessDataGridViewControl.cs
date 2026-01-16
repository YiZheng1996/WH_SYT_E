using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows.Forms;

namespace MainUI.LogicalConfiguration.Controls
{
    /// <summary>
    /// 流程配置表格用户控件
    /// </summary>
    public partial class ProcessDataGridViewControl : UserControl
    {
        #region 字段
        private DataGridView _dataGridView;
        private readonly ILogger<ProcessDataGridViewControl> _logger;
        private readonly IWorkflowStateService _workflowState;
        private DataGridViewManager _gridManager;
        private StepContextMenuManager _menuManager;
        private bool _isDisposed;
        private bool _isInitialized;

        #endregion

        #region 配置属性

        /// <summary>
        /// 是否启用右键菜单（默认启用）
        /// </summary>
        public bool EnableContextMenu { get; set; } = true;

        /// <summary>
        /// 是否自动刷新UI（默认启用）
        /// </summary>
        public bool AutoRefresh { get; set; } = true;

        /// <summary>
        /// 是否允许拖放（默认启用）
        /// </summary>
        public bool AllowDragDrop { get; set; } = true;

        #endregion

        #region 事件定义

        /// <summary>
        /// 当需要配置步骤时触发(双击行)
        /// </summary>
        public event EventHandler<StepConfigEventArgs> StepConfigRequested;

        /// <summary>
        /// 当步骤被删除时触发
        /// </summary>
        public event EventHandler<StepEventArgs> StepDeleted;

        /// <summary>
        /// 当步骤被添加时触发
        /// </summary>
        public event EventHandler<StepEventArgs> StepAdded;

        /// <summary>
        /// 当步骤列表改变时触发
        /// </summary>
        public event EventHandler StepsChanged;

        /// <summary>
        /// 当拖拽进入时触发
        /// </summary>
        public event DragEventHandler DragEnterEvent;

        /// <summary>
        /// 当拖拽放下时触发
        /// </summary>
        public event DragEventHandler DragDropEvent;

        /// <summary>
        /// 选择改变事件
        /// </summary>
        public event EventHandler SelectionChangedEvent;

        /// <summary>
        /// 单元格编辑结束事件
        /// </summary>
        public event DataGridViewCellEventHandler CellEndEditEvent;

        /// <summary>
        /// 单元格开始编辑事件
        /// </summary>
        public event DataGridViewCellCancelEventHandler CellBeginEditEvent;

        #endregion

        #region 构造函数

        /// <summary>
        /// 设计时构造函数
        /// </summary>
        public ProcessDataGridViewControl()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        /// <summary>
        /// 运行时构造函数
        /// </summary>
        public ProcessDataGridViewControl(
            IWorkflowStateService workflowState,
            ILogger<ProcessDataGridViewControl> logger) : this()
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _logger = logger;

            // 创建管理器
            _gridManager = new DataGridViewManager(_dataGridView);

            // 延迟初始化菜单和事件订阅（等待 ParentForm 可用）
            Load += OnControlLoad;

            _logger?.LogDebug("流程配置表格控件已创建");
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 控件加载完成事件
        /// </summary>
        private void OnControlLoad(object sender, EventArgs e)
        {
            if (_isInitialized) return;

            try
            {
                // 初始化右键菜单
                if (EnableContextMenu)
                {
                    InitializeContextMenu();
                }

                // 订阅工作流状态事件
                SubscribeToWorkflowEvents();

                _isInitialized = true;
                _logger?.LogDebug("流程配置表格控件初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化流程配置表格控件时发生错误");
            }
        }

        /// <summary>
        /// 初始化DataGridView控件
        /// </summary>
        private void InitializeDataGridView()
        {
            _dataGridView = new UIDataGridView
            {
                Name = "ProcessDataGridView",
                Dock = DockStyle.Fill,
                Location = new Point(8, 8),
                Size = new Size(983, 588),
                TabIndex = 0,

                // 基本设置
                AllowDrop = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                MultiSelect = true,

                // 背景和边框
                BackgroundColor = Color.White,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,

                // 字体
                Font = new Font("微软雅黑", 9F),

                // 网格线
                GridColor = Color.FromArgb(233, 236, 239),

                // 边框
                RectColor = Color.White,

                // 行头
                RowHeadersVisible = false,
                RowHeadersWidth = 35,

                // 选择
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                SelectedIndex = -1,

                // 交替行颜色
                StripeOddColor = Color.White,

                // 禁用系统样式
                EnableHeadersVisualStyles = false,
                // 交替行样式
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White
                },

                // 列头样式 - 蓝色背景,白色粗体文字
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(80, 160, 255),         // ← 蓝色!
                    Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134), // ← 12F 粗体!
                    ForeColor = Color.White,
                    SelectionBackColor = Color.FromArgb(80, 160, 255),
                    SelectionForeColor = Color.White,
                    WrapMode = DataGridViewTriState.True
                },
                ColumnHeadersHeight = 40,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,

                // 默认单元格样式
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.White,
                    Font = new Font("微软雅黑", 9F),                  // ← 9F!
                    ForeColor = Color.FromArgb(48, 48, 48),
                    SelectionBackColor = Color.FromArgb(227, 242, 253), // ← 淡蓝色!
                    SelectionForeColor = Color.Black,                  // ← 黑色!
                    WrapMode = DataGridViewTriState.False
                },

                // 行头样式
                RowHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    BackColor = SystemColors.Control,
                    Font = new Font("微软雅黑", 12F),
                    ForeColor = SystemColors.WindowText,
                    SelectionBackColor = SystemColors.Highlight,
                    SelectionForeColor = SystemColors.HighlightText,
                    WrapMode = DataGridViewTriState.True
                },

                // 行样式
                RowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134)
                }
            };

            // 行模板
            _dataGridView.RowTemplate.Height = 35;
            _dataGridView.RowTemplate.DefaultCellStyle.Font = new Font("微软雅黑", 12F);

            // 初始化列
            InitializeColumns();

            // 注册事件
            _dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            _dataGridView.DragEnter += DataGridView_DragEnter;
            _dataGridView.DragDrop += DataGridView_DragDrop;
            _dataGridView.SelectionChanged += DataGridView_SelectionChanged;
            _dataGridView.CellBeginEdit += DataGridView_CellBeginEdit;
            _dataGridView.CellEndEdit += DataGridView_CellEndEdit;


            Controls.Add(_dataGridView);
        }

        /// <summary>
        /// 初始化列
        /// </summary>
        private void InitializeColumns()
        {
            _dataGridView.Columns.Clear();

            // 第1列: StepNum - 步骤号
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColStepNumber",
                HeaderText = "步骤",
                Width = 60,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // 第2列: StepName - 步骤名称
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColStepName",
                HeaderText = "操作名称",
                Width = 150,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // 第3列: StepType - 步骤类型 (由GetStepTypeName生成)
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColStepType",
                HeaderText = "类型",
                Width = 120,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // 第4列: StepDetails - 步骤详情 (由StepDetailsProvider生成) ← 关键!
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColStepDetails",
                HeaderText = "详情",
                Width = 300,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // 第5列: Remark - 备注 (可编辑)
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColRemark",
                HeaderText = "备注",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = false,  // 允许编辑
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // 第6列: Status - 状态
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColStatus",
                HeaderText = "状态",
                Width = 100,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                Visible = false
            });

            // 第7列: ExecutionTime - 执行时间
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColExecutionTime",
                HeaderText = "执行时间",
                Width = 100,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(108, 117, 125)
                },
                Visible = false
            });
        }

        /// <summary>
        /// 初始化右键菜单管理器
        /// </summary>
        private void InitializeContextMenu()
        {
            try
            {
                if (_menuManager != null)
                {
                    _logger?.LogDebug("右键菜单管理器已存在，跳过初始化");
                    return;
                }

                // 获取父窗体
                var parentForm = FindForm();
                if (parentForm == null)
                {
                    _logger?.LogWarning("无法找到父窗体，延迟初始化右键菜单");
                    return;
                }

                // 创建菜单管理器
                var menuLogger = Program.ServiceProvider?.GetService<ILogger<StepContextMenuManager>>();
                _menuManager = new StepContextMenuManager(
                    _dataGridView,
                    _workflowState,
                    _gridManager,
                    menuLogger ?? _logger as Microsoft.Extensions.Logging.ILogger,
                    parentForm);

                _logger?.LogDebug("右键菜单管理器已初始化");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化右键菜单管理器失败");
            }
        }

        /// <summary>
        /// 订阅工作流状态服务的事件
        /// </summary>
        private void SubscribeToWorkflowEvents()
        {
            if (_workflowState == null) return;

            try
            {
                _workflowState.StepAdded += OnWorkflowStepAdded;
                _workflowState.StepRemoved += OnWorkflowStepRemoved;
                _workflowState.StepsChanged += OnWorkflowStepsChanged;

                _logger?.LogDebug("已订阅工作流状态事件");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "订阅工作流状态事件失败");
            }
        }

        /// <summary>
        /// 取消订阅工作流状态服务的事件
        /// </summary>
        private void UnsubscribeFromWorkflowEvents()
        {
            if (_workflowState == null) return;

            try
            {
                _workflowState.StepAdded -= OnWorkflowStepAdded;
                _workflowState.StepRemoved -= OnWorkflowStepRemoved;
                _workflowState.StepsChanged -= OnWorkflowStepsChanged;

                _logger?.LogDebug("已取消订阅工作流状态事件");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "取消订阅工作流状态事件失败");
            }
        }

        #endregion

        #region 工作流事件处理（内部自动刷新）

        /// <summary>
        /// 工作流步骤添加事件 - 自动刷新UI
        /// </summary>
        private void OnWorkflowStepAdded(ChildModel step)
        {
            try
            {
                _logger?.LogDebug("步骤已添加到工作流: {StepName}", step.StepName);

                // 自动刷新UI
                if (AutoRefresh)
                {
                    RefreshGridInternal();
                }

                // 触发外部事件
                StepAdded?.Invoke(this, new StepEventArgs(step));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "处理步骤添加事件时发生错误");
            }
        }

        /// <summary>
        /// 工作流步骤移除事件 - 自动刷新UI
        /// </summary>
        private void OnWorkflowStepRemoved(ChildModel step)
        {
            try
            {
                _logger?.LogDebug("步骤已从工作流移除: {StepName}", step.StepName);

                // 自动刷新UI
                if (AutoRefresh)
                {
                    RefreshGridInternal();
                }

                // 触发外部事件
                StepDeleted?.Invoke(this, new StepEventArgs(step));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "处理步骤移除事件时发生错误");
            }
        }

        /// <summary>
        /// 工作流步骤集合变更事件 - 自动刷新UI
        /// </summary>
        private void OnWorkflowStepsChanged()
        {
            try
            {
                _logger?.LogDebug("工作流步骤集合已变更");

                // 自动刷新UI
                if (AutoRefresh)
                {
                    RefreshGridInternal();
                }

                // 触发外部事件
                StepsChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "处理步骤集合变更事件时发生错误");
            }
        }

        /// <summary>
        /// 内部刷新方法（支持线程安全）
        /// </summary>
        private void RefreshGridInternal()
        {
            if (_gridManager == null || _workflowState == null) return;

            try
            {
                // 确保在UI线程上执行
                if (InvokeRequired)
                {
                    Invoke(new Action(RefreshGridInternal));
                    return;
                }

                _gridManager.RefreshFromDataSource(_workflowState.GetSteps());
                _logger?.LogDebug("表格已刷新，步骤数: {Count}", _workflowState.GetSteps().Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "刷新表格时发生错误");
            }
        }

        #endregion

        #region 公开属性

        /// <summary>
        /// 获取内部DataGridView控件
        /// </summary>
        public DataGridView DataGridView => _dataGridView;

        /// <summary>
        /// 获取或设置步骤列表
        /// </summary>
        public List<ChildModel> Steps
        {
            get
            {
                if (_workflowState != null)
                {
                    return _workflowState.GetSteps();
                }
                return [];
            }
            set
            {
                if (_gridManager != null)
                {
                    _gridManager.RefreshFromDataSource(value);
                    StepsChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 获取选中的步骤索引
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (_dataGridView.SelectedRows.Count > 0)
                {
                    return _dataGridView.SelectedRows[0].Index;
                }
                return -1;
            }
        }

        /// <summary>
        /// 获取步骤总数
        /// </summary>
        public int StepCount => _dataGridView.Rows.Count;

        #endregion

        #region 公开方法

        /// <summary>
        /// 添加步骤
        /// </summary>
        public void AddStep(ChildModel step)
        {
            if (_workflowState != null)
            {
                _workflowState.AddStep(step);
                // 事件订阅会自动刷新UI，无需手动刷新
                _logger?.LogDebug("步骤已添加: {StepName}", step.StepName);
            }
        }

        /// <summary>
        /// 删除选中的步骤
        /// </summary>
        public void RemoveSelectedStep()
        {
            if (SelectedIndex >= 0)
            {
                var step = Steps[SelectedIndex];
                if (_workflowState != null)
                {
                    _workflowState.RemoveStep(step);
                    // 事件订阅会自动刷新UI，无需手动刷新
                    _logger?.LogDebug("步骤已删除: {StepName}", step.StepName);
                }
            }
        }

        /// <summary>
        /// 获取选中的步骤
        /// </summary>
        public ChildModel GetSelectedStep()
        {
            if (SelectedIndex >= 0 && SelectedIndex < Steps.Count)
            {
                return Steps[SelectedIndex];
            }
            return null;
        }

        /// <summary>
        /// 刷新表格显示（手动刷新）
        /// </summary>
        public void RefreshGrid()
        {
            RefreshGridInternal();
        }

        /// <summary>
        /// 清空所有步骤
        /// </summary>
        public void ClearAllSteps()
        {
            if (_workflowState != null)
            {
                _workflowState.ClearSteps();
                // 事件订阅会自动刷新UI，无需手动刷新
                _logger?.LogDebug("所有步骤已清空");
            }
        }

        /// <summary>
        /// 更新步骤状态
        /// </summary>
        public void UpdateStepStatus(int index, int status)
        {
            if (_gridManager != null && index >= 0 && index < _dataGridView.Rows.Count)
            {
                _gridManager.UpdateRowStatus(index, status);
                _logger?.LogDebug("步骤状态已更新: Index={Index}, Status={Status}", index, status);
            }
        }

        /// <summary>
        /// 选中指定行
        /// </summary>
        public void SelectRow(int index)
        {
            if (index >= 0 && index < _dataGridView.Rows.Count)
            {
                _dataGridView.ClearSelection();
                _dataGridView.Rows[index].Selected = true;
                _dataGridView.CurrentCell = _dataGridView.Rows[index].Cells[0];
            }
        }

        /// <summary>
        /// 滚动到指定行
        /// </summary>
        public void ScrollToRow(int index)
        {
            if (index >= 0 && index < _dataGridView.Rows.Count)
            {
                _dataGridView.FirstDisplayedScrollingRowIndex = index;
            }
        }

        #endregion

        #region DataGridView事件处理
        /// <summary>
        /// 双击单元格打开配置
        /// </summary>
        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var step = Steps[e.RowIndex];
                StepConfigRequested?.Invoke(this, new StepConfigEventArgs(step, e.RowIndex));
                _logger?.LogDebug("请求配置步骤: {StepName}, Index={Index}", step.StepName, e.RowIndex);
            }
        }

        /// <summary>
        /// 拖拽进入
        /// </summary>
        private void DataGridView_DragEnter(object sender, DragEventArgs e)
        {
            if (!AllowDragDrop) return;

            e.Effect = e.Data.GetDataPresent(typeof(TreeNode))
                ? DragDropEffects.Copy
                : DragDropEffects.None;

            DragEnterEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// 拖拽放下
        /// </summary>
        private void DataGridView_DragDrop(object sender, DragEventArgs e)
        {
            if (!AllowDragDrop) return;

            DragDropEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// 选择改变
        /// </summary>
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            SelectionChangedEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// 单元格开始编辑
        /// </summary>
        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CellBeginEditEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// 单元格编辑结束
        /// </summary>
        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CellEndEditEvent?.Invoke(sender, e);
        }

        #endregion

        #region 按钮事件处理

        private void BtnInsertBefore_Click(object sender, EventArgs e)
        {
            _menuManager?.InsertBefore();
            UpdateButtonStates(null, EventArgs.Empty);
        }

        private void BtnInsertAfter_Click(object sender, EventArgs e)
        {
            _menuManager?.InsertAfter();
            UpdateButtonStates(null, EventArgs.Empty);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            _menuManager?.Delete();
            UpdateButtonStates(null, EventArgs.Empty);
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            _menuManager?.MoveUp();
            UpdateButtonStates(null, EventArgs.Empty);
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            _menuManager?.MoveDown();
            UpdateButtonStates(null, EventArgs.Empty);
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            _menuManager?.Copy();
            UpdateButtonStates(null, EventArgs.Empty);
        }

        private void BtnCut_Click(object sender, EventArgs e)
        {
            _menuManager?.Cut();
            UpdateButtonStates(null, EventArgs.Empty);
        }

        private void BtnPaste_Click(object sender, EventArgs e)
        {
            _menuManager?.Paste();
            UpdateButtonStates(null, EventArgs.Empty);
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            _menuManager?.SelectAll();
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            _menuManager?.ClearAll();
            UpdateButtonStates(null, EventArgs.Empty);
        }

        /// <summary>
        /// 更新按钮可用状态
        /// </summary>
        private void UpdateButtonStates(object sender, EventArgs e)
        {
            if (_menuManager == null) return;

            try
            {
                int selectedCount = _gridManager.GetSelectedRowCount();
                int selectedIndex = _gridManager.GetSelectedRowIndex();
                int totalRows = _dataGridView.Rows.Count;
                bool hasSelection = selectedCount > 0;
                bool isSingleSelection = selectedCount == 1;
                bool hasCopiedData = _menuManager.HasCopiedData();

                // 插入操作仅单选时可用
                btnInsertBefore.Enabled = isSingleSelection;
                btnInsertAfter.Enabled = isSingleSelection;

                // 删除/复制/剪切支持多选
                btnDelete.Enabled = hasSelection;
                btnCopy.Enabled = hasSelection;
                btnCut.Enabled = hasSelection;
                btnPaste.Enabled = hasCopiedData;

                // 上移/下移仅单选时可用
                btnMoveUp.Enabled = isSingleSelection && selectedIndex > 0;
                btnMoveDown.Enabled = isSingleSelection && selectedIndex < totalRows - 1;

                // 全选/清空
                btnSelectAll.Enabled = totalRows > 0;
                btnClearAll.Enabled = totalRows > 0;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新按钮状态时发生错误");
            }
        }

        /// <summary>
        /// 键盘按键处理(支持快捷键)
        /// </summary>
        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (_menuManager != null && _menuManager.HandleKeyDown(e.KeyData))
            {
                e.Handled = true;
                UpdateButtonStates(null, EventArgs.Empty);
            }
        }

        #endregion
    }

    #region 事件参数类

    /// <summary>
    /// 步骤事件参数
    /// </summary>
    public class StepEventArgs(ChildModel step) : EventArgs
    {
        public ChildModel Step { get; } = step;
    }

    /// <summary>
    /// 步骤配置事件参数
    /// </summary>
    public class StepConfigEventArgs(ChildModel step, int rowIndex) : EventArgs
    {
        public ChildModel Step { get; } = step;
        public int RowIndex { get; } = rowIndex;
    }

    #endregion
}
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Controls
{
    /// <summary>
    /// 流程配置表格用户控件 - 封装DataGridView流程步骤管理功能
    /// </summary>
    public partial class ProcessDataGridViewControl : UserControl
    {
        #region 字段

        private DataGridView _dataGridView;
        private readonly ILogger<ProcessDataGridViewControl> _logger;
        private readonly IWorkflowStateService _workflowState;
        private DataGridViewManager _gridManager;

        // 状态颜色
        private static readonly Color PrimaryBlue = Color.FromArgb(65, 100, 204);
        private static readonly Color SuccessGreen = Color.FromArgb(40, 167, 69);
        private static readonly Color ErrorRed = Color.FromArgb(220, 53, 69);

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
            InitializeComponent2();
            InitializeDataGridView();
        }

        /// <summary>
        /// 运行时构造函数
        /// </summary>
        public ProcessDataGridViewControl(
            IWorkflowStateService workflowState,
            ILogger<ProcessDataGridViewControl> logger) : this()
        {
            _workflowState = workflowState;
            _logger = logger;

            // 创建管理器
            _gridManager = new DataGridViewManager(_dataGridView);

            _logger?.LogDebug("流程配置表格控件已创建");
        }

        #endregion

        #region 初始化

        private void InitializeComponent2()
        {

        }

        /// <summary>
        /// 初始化DataGridView控件 - 与原始样式完全一致
        /// </summary>
        private void InitializeDataGridView()
        {
            _dataGridView = new Sunny.UI.UIDataGridView  // 或 AntdUI.DataGridView
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
                MultiSelect = false,

                // 背景和边框
                BackgroundColor = Color.White,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,

                // 字体
                Font = new Font("微软雅黑", 9F),

                // 网格线
                GridColor = Color.FromArgb(233, 236, 239),

                // 边框 (Sunny.UI/AntdUI 特有)
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
                EnableHeadersVisualStyles = false
            };

            // ★★★ 交替行样式
            _dataGridView.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White
            };

            // ★★★ 列头样式 - 蓝色背景,白色粗体文字
            _dataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(80, 160, 255),         // ← 蓝色!
                Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134), // ← 12F 粗体!
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(80, 160, 255),
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };
            _dataGridView.ColumnHeadersHeight = 40;
            _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // ★★★ 默认单元格样式
            _dataGridView.DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.White,
                Font = new Font("微软雅黑", 9F),                  // ← 9F!
                ForeColor = Color.FromArgb(48, 48, 48),
                SelectionBackColor = Color.FromArgb(227, 242, 253), // ← 淡蓝色!
                SelectionForeColor = Color.Black,                  // ← 黑色!
                WrapMode = DataGridViewTriState.False
            };

            // ★★★ 行头样式
            _dataGridView.RowHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = SystemColors.Control,
                Font = new Font("微软雅黑", 12F),
                ForeColor = SystemColors.WindowText,
                SelectionBackColor = SystemColors.Highlight,
                SelectionForeColor = SystemColors.HighlightText,
                WrapMode = DataGridViewTriState.True
            };

            // ★★★ 行样式
            _dataGridView.RowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134)
            };

            // ★★★ 行模板
            _dataGridView.RowTemplate.Height = 35;
            _dataGridView.RowTemplate.DefaultCellStyle.Font = new Font("微软雅黑", 12F);

            // 初始化列
            InitializeColumns();

            // 注册事件
            _dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            _dataGridView.DragEnter += DataGridView_DragEnter;
            _dataGridView.DragDrop += DataGridView_DragDrop;
            _dataGridView.SelectionChanged += DataGridView_SelectionChanged;
            _dataGridView.CellEndEdit += DataGridView_CellEndEdit;
            _dataGridView.CellBeginEdit += DataGridView_CellBeginEdit;

            this.Controls.Add(_dataGridView);
        }

        /// <summary>
        /// 初始化列 - 与DataGridViewManager.RefreshFromDataSource()的数据顺序完全匹配
        /// </summary>
        private void InitializeColumns()
        {
            _dataGridView.Columns.Clear();

            // ★ 第1列: StepNum - 步骤号
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColStepNumber",
                HeaderText = "步骤",
                Width = 60,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // ★ 第2列: StepName - 步骤名称
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColStepName",
                HeaderText = "操作名称",
                Width = 150,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // ★ 第3列: StepType - 步骤类型 (由GetStepTypeName生成)
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColStepType",
                HeaderText = "类型",
                Width = 120,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // ★ 第4列: StepDetails - 步骤详情 (由StepDetailsProvider生成) ← 关键!
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColStepDetails",
                HeaderText = "详情",
                Width = 300,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // ★ 第5列: Remark - 备注 (可编辑)
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColRemark",
                HeaderText = "备注",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = false,  // 允许编辑
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // ★ 第6列: Status - 状态
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

            // ★ 第7列: ExecutionTime - 执行时间
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
                StepAdded?.Invoke(this, new StepEventArgs(step));
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
                    StepDeleted?.Invoke(this, new StepEventArgs(step));
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
        /// 刷新表格显示
        /// </summary>
        public void RefreshGrid()
        {
            if (_gridManager != null && _workflowState != null)
            {
                _gridManager.RefreshFromDataSource(_workflowState.GetSteps());
                _logger?.LogDebug("表格已刷新");
            }
        }

        /// <summary>
        /// 清空所有步骤
        /// </summary>
        public void ClearAllSteps()
        {
            if (_workflowState != null)
            {
                _workflowState.ClearSteps();
                StepsChanged?.Invoke(this, EventArgs.Empty);
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

        /// <summary>
        /// 设置右键菜单
        /// </summary>
        public void SetContextMenu(System.Windows.Forms.ContextMenuStrip contextMenu)
        {
            _dataGridView.ContextMenuStrip = contextMenu;
        }

        #endregion

        #region 事件处理

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
            //DragDropEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// 选择改变
        /// </summary>
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            SelectionChangedEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// 单元格编辑结束
        /// </summary>
        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CellEndEditEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// 单元格开始编辑
        /// </summary>
        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CellBeginEditEvent?.Invoke(sender, e);
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
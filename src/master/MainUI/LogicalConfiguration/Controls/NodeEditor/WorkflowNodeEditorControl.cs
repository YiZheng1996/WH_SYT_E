using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Controls.NodeEditor
{
    /// <summary>
    /// 工作流节点编辑器控件 - 可视化流程图设计器
    /// 支持节点拖拽、连线、缩放、选择等操作
    /// </summary>
    public partial class WorkflowNodeEditorControl : UserControl
    {
        #region 字段

        // 服务依赖
        private readonly IWorkflowStateService _workflowState;
        private readonly ILogger<WorkflowNodeEditorControl> _logger;

        // 渲染器
        private readonly NodeRenderer _nodeRenderer;

        // 数据
        private List<WorkflowNode> _nodes = new List<WorkflowNode>();
        private List<NodeConnection> _connections = new List<NodeConnection>();

        // 视图状态
        private PointF _viewOffset = PointF.Empty;
        private float _zoomLevel = 1.0f;
        private const float MIN_ZOOM = 0.3f;
        private const float MAX_ZOOM = 2.0f;

        // 交互状态
        private WorkflowNode _selectedNode;
        private WorkflowNode _hoveredNode;
        private NodeConnection _hoveredConnection;
        private WorkflowNode _dragNode;
        private PointF _dragStartPos;
        private PointF _dragNodeStartPos;
        private bool _isPanning;
        private PointF _panStartPos;
        private PointF _panStartOffset;

        // 连线状态
        private bool _isConnecting;
        private WorkflowNode _connectionSource;
        private PointF _connectionEndPoint;

        // 多选状态
        private List<WorkflowNode> _selectedNodes = new List<WorkflowNode>();
        private bool _isSelecting;
        private RectangleF _selectionRect;
        private PointF _selectionStart;

        // 布局配置
        private const float NODE_SPACING_X = 250f;
        private const float NODE_SPACING_Y = 100f;
        private const float START_X = 50f;
        private const float START_Y = 50f;

        // 双缓冲
        private BufferedGraphicsContext _bufferContext;
        private BufferedGraphics _bufferedGraphics;

        #endregion

        #region 事件定义

        /// <summary>
        /// 节点被选中时触发
        /// </summary>
        public event EventHandler<NodeSelectedEventArgs> NodeSelected;

        /// <summary>
        /// 节点被双击时触发（用于打开配置）
        /// </summary>
        public event EventHandler<NodeSelectedEventArgs> NodeDoubleClicked;

        /// <summary>
        /// 节点位置改变时触发
        /// </summary>
        public event EventHandler<NodeMovedEventArgs> NodeMoved;

        /// <summary>
        /// 请求添加节点时触发
        /// </summary>
        public event EventHandler<AddNodeRequestEventArgs> AddNodeRequested;

        /// <summary>
        /// 缩放级别改变时触发
        /// </summary>
        public event EventHandler<float> ZoomChanged;

        #endregion

        #region 属性

        /// <summary>
        /// 当前缩放级别
        /// </summary>
        [Browsable(false)]
        public float ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                _zoomLevel = Math.Max(MIN_ZOOM, Math.Min(MAX_ZOOM, value));
                ZoomChanged?.Invoke(this, _zoomLevel);
                Invalidate();
            }
        }

        /// <summary>
        /// 当前选中的节点
        /// </summary>
        [Browsable(false)]
        public WorkflowNode SelectedNode => _selectedNode;

        /// <summary>
        /// 是否显示网格
        /// </summary>
        [DefaultValue(true)]
        public bool ShowGrid { get; set; } = true;

        /// <summary>
        /// 网格大小
        /// </summary>
        [DefaultValue(20)]
        public int GridSize { get; set; } = 20;

        /// <summary>
        /// 是否启用对齐到网格
        /// </summary>
        [DefaultValue(true)]
        public bool SnapToGrid { get; set; } = true;

        /// <summary>
        /// 是否显示小地图
        /// </summary>
        [DefaultValue(true)]
        public bool ShowMinimap { get; set; } = true;

        /// <summary>
        /// 自动布局模式
        /// </summary>
        [DefaultValue(LayoutMode.Horizontal)]
        public LayoutMode AutoLayoutMode { get; set; } = LayoutMode.Horizontal;

        #endregion

        #region 构造函数

        /// <summary>
        /// 设计时构造函数
        /// </summary>
        public WorkflowNodeEditorControl()
        {
            InitializeComponent();
            SetupControl();
            _nodeRenderer = new NodeRenderer();
        }

        /// <summary>
        /// 运行时构造函数（带依赖注入）
        /// </summary>
        public WorkflowNodeEditorControl(
            IWorkflowStateService workflowState,
            ILogger<WorkflowNodeEditorControl> logger) : this()
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _logger = logger;

            // 订阅工作流状态变更
            SubscribeToWorkflowEvents();

            _logger?.LogDebug("WorkflowNodeEditorControl 初始化完成");
        }

        /// <summary>
        /// 初始化控件设置
        /// </summary>
        private void SetupControl()
        {
            // 启用双缓冲
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            // 设置背景色
            BackColor = Color.FromArgb(248, 249, 250);

            // 允许拖放
            AllowDrop = true;

            // 初始化缓冲
            _bufferContext = BufferedGraphicsManager.Current;
        }

        /// <summary>
        /// 订阅工作流状态事件
        /// </summary>
        private void SubscribeToWorkflowEvents()
        {
            if (_workflowState == null) return;

            _workflowState.StepAdded += OnWorkflowStepAdded;
            _workflowState.StepRemoved += OnWorkflowStepRemoved;
            _workflowState.StepsChanged += OnWorkflowStepsChanged;
        }

        #endregion

        #region 初始化组件

        private void InitializeComponent()
        {
            SuspendLayout();

            Name = "WorkflowNodeEditorControl";
            Size = new Size(800, 600);

            ResumeLayout(false);
        }

        #endregion

        #region 数据同步方法

        /// <summary>
        /// 从工作流状态同步节点
        /// </summary>
        public void SyncFromWorkflowState()
        {
            if (_workflowState == null) return;

            try
            {
                var steps = _workflowState.GetSteps();
                _nodes.Clear();
                _connections.Clear();

                // 创建节点
                for (int i = 0; i < steps.Count; i++)
                {
                    var node = WorkflowNode.FromChildModel(steps[i], i);
                    _nodes.Add(node);
                }

                // 自动布局
                AutoLayoutNodes();

                // 创建连接
                CreateDefaultConnections();

                Invalidate();

                _logger?.LogDebug("同步了 {Count} 个节点", _nodes.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "同步工作流状态失败");
            }
        }

        /// <summary>
        /// 自动布局节点
        /// </summary>
        public void AutoLayoutNodes()
        {
            if (_nodes.Count == 0) return;

            switch (AutoLayoutMode)
            {
                case LayoutMode.Horizontal:
                    LayoutHorizontal();
                    break;
                case LayoutMode.Vertical:
                    LayoutVertical();
                    break;
                case LayoutMode.Tree:
                    LayoutTree();
                    break;
            }

            Invalidate();
        }

        /// <summary>
        /// 水平布局
        /// </summary>
        private void LayoutHorizontal()
        {
            float x = START_X;
            float y = START_Y;
            int nodesPerRow = 4;
            int currentIndex = 0;

            foreach (var node in _nodes)
            {
                node.Position = new PointF(x, y);

                currentIndex++;
                if (currentIndex % nodesPerRow == 0)
                {
                    x = START_X;
                    y += NODE_SPACING_Y;
                }
                else
                {
                    x += NODE_SPACING_X;
                }
            }
        }

        /// <summary>
        /// 垂直布局
        /// </summary>
        private void LayoutVertical()
        {
            float x = START_X + 200;
            float y = START_Y;

            foreach (var node in _nodes)
            {
                node.Position = new PointF(x, y);
                y += NODE_SPACING_Y;
            }
        }

        /// <summary>
        /// 树形布局（考虑分支）
        /// </summary>
        private void LayoutTree()
        {
            // 简化版本：按步骤顺序排列，条件分支向下偏移
            float x = START_X;
            float y = START_Y;
            float branchOffset = 0;

            for (int i = 0; i < _nodes.Count; i++)
            {
                var node = _nodes[i];

                // 条件节点后的节点向下偏移
                if (i > 0 && _nodes[i - 1].Type == NodeType.Condition)
                {
                    branchOffset += NODE_SPACING_Y * 0.5f;
                }

                node.Position = new PointF(x, y + branchOffset);
                x += NODE_SPACING_X;

                // 每4个节点换行
                if ((i + 1) % 4 == 0)
                {
                    x = START_X;
                    y += NODE_SPACING_Y + branchOffset;
                    branchOffset = 0;
                }
            }
        }

        /// <summary>
        /// 创建默认连接线
        /// </summary>
        private void CreateDefaultConnections()
        {
            _connections.Clear();

            for (int i = 0; i < _nodes.Count - 1; i++)
            {
                var connection = new NodeConnection
                {
                    SourceNode = _nodes[i],
                    TargetNode = _nodes[i + 1],
                    Type = ConnectionType.Normal
                };

                // 条件节点特殊处理
                if (_nodes[i].Type == NodeType.Condition)
                {
                    connection.Label = "是";
                    connection.Type = ConnectionType.ConditionTrue;
                }

                _connections.Add(connection);
            }
        }

        #endregion

        #region 工作流事件处理

        private void OnWorkflowStepAdded(ChildModel step)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChildModel>(OnWorkflowStepAdded), step);
                return;
            }

            SyncFromWorkflowState();
        }

        private void OnWorkflowStepRemoved(ChildModel step)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChildModel>(OnWorkflowStepRemoved), step);
                return;
            }

            SyncFromWorkflowState();
        }

        private void OnWorkflowStepsChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnWorkflowStepsChanged));
                return;
            }

            SyncFromWorkflowState();
        }

        #endregion

        #region 绘制方法

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 应用视图变换
            g.TranslateTransform(_viewOffset.X, _viewOffset.Y);
            g.ScaleTransform(_zoomLevel, _zoomLevel);

            // 1. 绘制网格
            if (ShowGrid)
            {
                DrawGrid(g);
            }

            // 2. 绘制连接线
            foreach (var connection in _connections)
            {
                connection.Draw(g);
            }

            // 3. 绘制正在创建的连接线
            if (_isConnecting && _connectionSource != null)
            {
                DrawTempConnection(g);
            }

            // 4. 绘制节点
            foreach (var node in _nodes)
            {
                _nodeRenderer.DrawNode(g, node);
            }

            // 5. 绘制选择框
            if (_isSelecting)
            {
                DrawSelectionRect(g);
            }

            // 重置变换
            g.ResetTransform();

            // 6. 绘制小地图（不受缩放影响）
            if (ShowMinimap)
            {
                DrawMinimap(g);
            }

            // 7. 绘制缩放信息
            DrawZoomInfo(g);
        }

        /// <summary>
        /// 绘制网格
        /// </summary>
        private void DrawGrid(Graphics g)
        {
            var visibleRect = GetVisibleRect();
            int gridSize = GridSize;

            using (var pen = new Pen(Color.FromArgb(30, 0, 0, 0), 1))
            {
                // 垂直线
                float startX = (float)Math.Floor(visibleRect.Left / gridSize) * gridSize;
                for (float x = startX; x < visibleRect.Right; x += gridSize)
                {
                    g.DrawLine(pen, x, visibleRect.Top, x, visibleRect.Bottom);
                }

                // 水平线
                float startY = (float)Math.Floor(visibleRect.Top / gridSize) * gridSize;
                for (float y = startY; y < visibleRect.Bottom; y += gridSize)
                {
                    g.DrawLine(pen, visibleRect.Left, y, visibleRect.Right, y);
                }
            }
        }

        /// <summary>
        /// 绘制临时连接线
        /// </summary>
        private void DrawTempConnection(Graphics g)
        {
            var start = _connectionSource.OutputConnector;
            var end = _connectionEndPoint;

            using (var pen = new Pen(Color.FromArgb(65, 100, 204), 2))
            {
                pen.DashStyle = DashStyle.Dash;

                // 简单贝塞尔曲线
                float offset = Math.Max(50, Math.Abs(end.X - start.X) * 0.3f);
                var cp1 = new PointF(start.X + offset, start.Y);
                var cp2 = new PointF(end.X - offset, end.Y);

                g.DrawBezier(pen, start, cp1, cp2, end);
            }
        }

        /// <summary>
        /// 绘制选择框
        /// </summary>
        private void DrawSelectionRect(Graphics g)
        {
            using (var pen = new Pen(Color.FromArgb(65, 100, 204), 1))
            using (var brush = new SolidBrush(Color.FromArgb(30, 65, 100, 204)))
            {
                pen.DashStyle = DashStyle.Dash;
                g.FillRectangle(brush, _selectionRect);
                g.DrawRectangle(pen, _selectionRect.X, _selectionRect.Y,
                    _selectionRect.Width, _selectionRect.Height);
            }
        }

        /// <summary>
        /// 绘制小地图
        /// </summary>
        private void DrawMinimap(Graphics g)
        {
            if (_nodes.Count == 0) return;

            // 小地图区域
            var minimapRect = new RectangleF(Width - 160, Height - 120, 150, 110);

            // 背景
            using (var bgBrush = new SolidBrush(Color.FromArgb(200, 255, 255, 255)))
            using (var borderPen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                g.FillRectangle(bgBrush, minimapRect);
                g.DrawRectangle(borderPen, minimapRect.X, minimapRect.Y,
                    minimapRect.Width, minimapRect.Height);
            }

            // 计算缩放比例
            var bounds = GetNodesBounds();
            float scaleX = (minimapRect.Width - 10) / Math.Max(bounds.Width, 1);
            float scaleY = (minimapRect.Height - 10) / Math.Max(bounds.Height, 1);
            float scale = Math.Min(scaleX, scaleY);

            // 绘制节点缩略图
            foreach (var node in _nodes)
            {
                float x = minimapRect.X + 5 + (node.Position.X - bounds.X) * scale;
                float y = minimapRect.Y + 5 + (node.Position.Y - bounds.Y) * scale;
                float w = node.Width * scale;
                float h = node.Height * scale;

                using (var brush = new SolidBrush(node.GetPrimaryColor()))
                {
                    g.FillRectangle(brush, x, y, Math.Max(w, 3), Math.Max(h, 2));
                }
            }

            // 绘制当前视口
            var viewRect = GetVisibleRect();
            float vx = minimapRect.X + 5 + (viewRect.X - bounds.X) * scale;
            float vy = minimapRect.Y + 5 + (viewRect.Y - bounds.Y) * scale;
            float vw = viewRect.Width * scale;
            float vh = viewRect.Height * scale;

            using (var pen = new Pen(Color.FromArgb(65, 100, 204), 1))
            {
                g.DrawRectangle(pen, vx, vy, vw, vh);
            }
        }

        /// <summary>
        /// 绘制缩放信息
        /// </summary>
        private void DrawZoomInfo(Graphics g)
        {
            string zoomText = $"缩放: {_zoomLevel:P0}";
            using (var font = new Font("微软雅黑", 9f))
            using (var brush = new SolidBrush(Color.FromArgb(108, 117, 125)))
            {
                g.DrawString(zoomText, font, brush, 10, Height - 25);
            }
        }

        #endregion

        #region 鼠标事件处理

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();

            var worldPos = ScreenToWorld(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                // 检测节点点击
                var hitNode = HitTestNode(worldPos);

                if (hitNode != null)
                {
                    // 检测是否点击连接点
                    if (hitNode.HitTestOutputConnector(worldPos))
                    {
                        // 开始创建连接
                        _isConnecting = true;
                        _connectionSource = hitNode;
                        _connectionEndPoint = worldPos;
                    }
                    else
                    {
                        // 选中节点
                        SelectNode(hitNode);

                        // 准备拖拽
                        _dragNode = hitNode;
                        _dragStartPos = worldPos;
                        _dragNodeStartPos = hitNode.Position;
                        hitNode.IsDragging = true;
                    }
                }
                else
                {
                    // 点击空白处 - 开始框选
                    _selectedNode = null;
                    _selectedNodes.Clear();
                    _isSelecting = true;
                    _selectionStart = worldPos;
                    _selectionRect = new RectangleF(worldPos.X, worldPos.Y, 0, 0);
                    NodeSelected?.Invoke(this, new NodeSelectedEventArgs(null));
                }
            }
            else if (e.Button == MouseButtons.Middle || e.Button == MouseButtons.Right)
            {
                // 开始平移视图
                _isPanning = true;
                _panStartPos = e.Location;
                _panStartOffset = _viewOffset;
                Cursor = Cursors.SizeAll;
            }

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var worldPos = ScreenToWorld(e.Location);

            if (_isPanning)
            {
                // 平移视图
                _viewOffset = new PointF(
                    _panStartOffset.X + (e.X - _panStartPos.X),
                    _panStartOffset.Y + (e.Y - _panStartPos.Y));
                Invalidate();
            }
            else if (_dragNode != null)
            {
                // 拖拽节点
                float dx = worldPos.X - _dragStartPos.X;
                float dy = worldPos.Y - _dragStartPos.Y;

                var newPos = new PointF(_dragNodeStartPos.X + dx, _dragNodeStartPos.Y + dy);

                // 对齐到网格
                if (SnapToGrid)
                {
                    newPos.X = (float)Math.Round(newPos.X / GridSize) * GridSize;
                    newPos.Y = (float)Math.Round(newPos.Y / GridSize) * GridSize;
                }

                _dragNode.Position = newPos;
                Invalidate();
            }
            else if (_isConnecting)
            {
                // 更新连接终点
                _connectionEndPoint = worldPos;
                Invalidate();
            }
            else if (_isSelecting)
            {
                // 更新选择框
                _selectionRect = CreateRectFromPoints(_selectionStart, worldPos);

                // 更新框选的节点
                _selectedNodes.Clear();
                foreach (var node in _nodes)
                {
                    if (_selectionRect.IntersectsWith(node.Bounds))
                    {
                        node.IsSelected = true;
                        _selectedNodes.Add(node);
                    }
                    else
                    {
                        node.IsSelected = false;
                    }
                }

                Invalidate();
            }
            else
            {
                // 悬停检测
                var hitNode = HitTestNode(worldPos);
                var hitConnection = HitTestConnection(worldPos);

                // 更新悬停状态
                if (_hoveredNode != hitNode)
                {
                    if (_hoveredNode != null) _hoveredNode.IsHovered = false;
                    _hoveredNode = hitNode;
                    if (_hoveredNode != null) _hoveredNode.IsHovered = true;
                    Invalidate();
                }

                if (_hoveredConnection != hitConnection)
                {
                    if (_hoveredConnection != null) _hoveredConnection.IsHovered = false;
                    _hoveredConnection = hitConnection;
                    if (_hoveredConnection != null) _hoveredConnection.IsHovered = true;
                    Invalidate();
                }

                // 更新光标
                if (hitNode != null && hitNode.HitTestOutputConnector(worldPos))
                    Cursor = Cursors.Cross;
                else if (hitNode != null)
                    Cursor = Cursors.Hand;
                else
                    Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            var worldPos = ScreenToWorld(e.Location);

            if (_dragNode != null)
            {
                _dragNode.IsDragging = false;

                // 触发节点移动事件
                NodeMoved?.Invoke(this, new NodeMovedEventArgs(_dragNode, _dragNodeStartPos, _dragNode.Position));

                _dragNode = null;
            }

            if (_isConnecting)
            {
                // 检测是否连接到目标节点
                var targetNode = HitTestNode(worldPos);
                if (targetNode != null && targetNode != _connectionSource)
                {
                    // 创建新连接
                    var connection = new NodeConnection
                    {
                        SourceNode = _connectionSource,
                        TargetNode = targetNode,
                        Type = ConnectionType.Normal
                    };
                    _connections.Add(connection);
                }

                _isConnecting = false;
                _connectionSource = null;
            }

            if (_isPanning)
            {
                _isPanning = false;
                Cursor = Cursors.Default;
            }

            if (_isSelecting)
            {
                _isSelecting = false;
            }

            Invalidate();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            var worldPos = ScreenToWorld(e.Location);
            var hitNode = HitTestNode(worldPos);

            if (hitNode != null)
            {
                NodeDoubleClicked?.Invoke(this, new NodeSelectedEventArgs(hitNode));
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            // 缩放
            float delta = e.Delta > 0 ? 0.1f : -0.1f;
            float newZoom = _zoomLevel + delta;
            newZoom = Math.Max(MIN_ZOOM, Math.Min(MAX_ZOOM, newZoom));

            if (Math.Abs(newZoom - _zoomLevel) > 0.001f)
            {
                // 以鼠标位置为中心缩放
                var mouseWorld = ScreenToWorld(e.Location);

                _zoomLevel = newZoom;

                // 调整偏移以保持鼠标位置不变
                var newMouseWorld = ScreenToWorld(e.Location);
                _viewOffset.X += (newMouseWorld.X - mouseWorld.X) * _zoomLevel;
                _viewOffset.Y += (newMouseWorld.Y - mouseWorld.Y) * _zoomLevel;

                ZoomChanged?.Invoke(this, _zoomLevel);
                Invalidate();
            }
        }

        #endregion

        #region 键盘事件处理

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteSelectedNodes();
                    break;

                case Keys.A when e.Control:
                    SelectAllNodes();
                    break;

                case Keys.D0 when e.Control:
                case Keys.NumPad0 when e.Control:
                    ResetView();
                    break;

                case Keys.Add when e.Control:
                case Keys.Oemplus when e.Control:
                    ZoomLevel += 0.1f;
                    break;

                case Keys.Subtract when e.Control:
                case Keys.OemMinus when e.Control:
                    ZoomLevel -= 0.1f;
                    break;
            }
        }

        #endregion

        #region 拖放事件处理

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

            if (drgevent.Data.GetDataPresent(typeof(TreeNode)))
            {
                drgevent.Effect = DragDropEffects.Copy;
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            if (drgevent.Data.GetData(typeof(TreeNode)) is TreeNode treeNode)
            {
                var screenPos = new Point(drgevent.X, drgevent.Y);
                var clientPos = PointToClient(screenPos);
                var worldPos = ScreenToWorld(clientPos);

                // 触发添加节点事件
                AddNodeRequested?.Invoke(this, new AddNodeRequestEventArgs(
                    treeNode.Text,
                    treeNode.Tag?.ToString(),
                    worldPos));
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 选中指定节点
        /// </summary>
        public void SelectNode(WorkflowNode node)
        {
            // 取消之前的选中
            if (_selectedNode != null)
                _selectedNode.IsSelected = false;

            _selectedNode = node;

            if (node != null)
            {
                node.IsSelected = true;
                NodeSelected?.Invoke(this, new NodeSelectedEventArgs(node));
            }

            Invalidate();
        }

        /// <summary>
        /// 选中所有节点
        /// </summary>
        public void SelectAllNodes()
        {
            _selectedNodes.Clear();
            foreach (var node in _nodes)
            {
                node.IsSelected = true;
                _selectedNodes.Add(node);
            }
            Invalidate();
        }

        /// <summary>
        /// 删除选中的节点
        /// </summary>
        public void DeleteSelectedNodes()
        {
            var nodesToDelete = _selectedNodes.Count > 0 ? _selectedNodes :
                (_selectedNode != null ? new List<WorkflowNode> { _selectedNode } : new List<WorkflowNode>());

            if (nodesToDelete.Count == 0) return;

            foreach (var node in nodesToDelete.ToList())
            {
                // 删除相关连接
                _connections.RemoveAll(c => c.SourceNode == node || c.TargetNode == node);

                // 删除节点
                _nodes.Remove(node);

                // 同步到工作流状态
                if (_workflowState != null && node.StepModel != null)
                {
                    _workflowState.RemoveStep(node.StepModel);
                }
            }

            _selectedNode = null;
            _selectedNodes.Clear();
            Invalidate();
        }

        /// <summary>
        /// 重置视图
        /// </summary>
        public void ResetView()
        {
            _viewOffset = PointF.Empty;
            _zoomLevel = 1.0f;
            Invalidate();
        }

        /// <summary>
        /// 适应内容到视图
        /// </summary>
        public void FitToView()
        {
            if (_nodes.Count == 0) return;

            var bounds = GetNodesBounds();
            float padding = 50;

            float scaleX = (Width - padding * 2) / bounds.Width;
            float scaleY = (Height - padding * 2) / bounds.Height;
            _zoomLevel = Math.Min(scaleX, scaleY);
            _zoomLevel = Math.Max(MIN_ZOOM, Math.Min(MAX_ZOOM, _zoomLevel));

            _viewOffset = new PointF(
                padding - bounds.X * _zoomLevel,
                padding - bounds.Y * _zoomLevel);

            Invalidate();
        }

        /// <summary>
        /// 更新节点执行状态
        /// </summary>
        public void UpdateNodeState(int stepIndex, ExecutionState state)
        {
            var node = _nodes.FirstOrDefault(n => n.StepIndex == stepIndex);
            if (node != null)
            {
                node.State = state;
                Invalidate();
            }
        }

        /// <summary>
        /// 高亮当前执行节点
        /// </summary>
        public void HighlightExecutingNode(int stepIndex)
        {
            foreach (var node in _nodes)
            {
                if (node.StepIndex == stepIndex)
                {
                    node.State = ExecutionState.Running;
                    SelectNode(node);
                    ScrollToNode(node);
                }
                else if (node.State == ExecutionState.Running)
                {
                    node.State = ExecutionState.Pending;
                }
            }
            Invalidate();
        }

        /// <summary>
        /// 滚动视图以显示指定节点
        /// </summary>
        public void ScrollToNode(WorkflowNode node)
        {
            if (node == null) return;

            var nodeCenter = node.Center;
            var viewCenter = new PointF(Width / 2f, Height / 2f);

            _viewOffset = new PointF(
                viewCenter.X - nodeCenter.X * _zoomLevel,
                viewCenter.Y - nodeCenter.Y * _zoomLevel);

            Invalidate();
        }

        #endregion

        #region 私有辅助方法

        /// <summary>
        /// 屏幕坐标转世界坐标
        /// </summary>
        private PointF ScreenToWorld(Point screenPoint)
        {
            return new PointF(
                (screenPoint.X - _viewOffset.X) / _zoomLevel,
                (screenPoint.Y - _viewOffset.Y) / _zoomLevel);
        }

        /// <summary>
        /// 世界坐标转屏幕坐标
        /// </summary>
        private Point WorldToScreen(PointF worldPoint)
        {
            return new Point(
                (int)(worldPoint.X * _zoomLevel + _viewOffset.X),
                (int)(worldPoint.Y * _zoomLevel + _viewOffset.Y));
        }

        /// <summary>
        /// 获取可见区域（世界坐标）
        /// </summary>
        private RectangleF GetVisibleRect()
        {
            var topLeft = ScreenToWorld(Point.Empty);
            var bottomRight = ScreenToWorld(new Point(Width, Height));
            return new RectangleF(topLeft.X, topLeft.Y,
                bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }

        /// <summary>
        /// 获取所有节点的边界
        /// </summary>
        private RectangleF GetNodesBounds()
        {
            if (_nodes.Count == 0)
                return new RectangleF(0, 0, Width, Height);

            float minX = _nodes.Min(n => n.Position.X);
            float minY = _nodes.Min(n => n.Position.Y);
            float maxX = _nodes.Max(n => n.Position.X + n.Width);
            float maxY = _nodes.Max(n => n.Position.Y + n.Height);

            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// 节点命中测试
        /// </summary>
        private WorkflowNode HitTestNode(PointF point)
        {
            // 从后往前遍历，优先选中上层节点
            for (int i = _nodes.Count - 1; i >= 0; i--)
            {
                if (_nodes[i].HitTest(point))
                    return _nodes[i];
            }
            return null;
        }

        /// <summary>
        /// 连接线命中测试
        /// </summary>
        private NodeConnection HitTestConnection(PointF point)
        {
            foreach (var connection in _connections)
            {
                if (connection.HitTest(point))
                    return connection;
            }
            return null;
        }

        /// <summary>
        /// 从两点创建矩形
        /// </summary>
        private RectangleF CreateRectFromPoints(PointF p1, PointF p2)
        {
            float x = Math.Min(p1.X, p2.X);
            float y = Math.Min(p1.Y, p2.Y);
            float w = Math.Abs(p2.X - p1.X);
            float h = Math.Abs(p2.Y - p1.Y);
            return new RectangleF(x, y, w, h);
        }

        #endregion

        #region 资源释放

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _nodeRenderer?.Dispose();
                _bufferedGraphics?.Dispose();

                if (_workflowState != null)
                {
                    _workflowState.StepAdded -= OnWorkflowStepAdded;
                    _workflowState.StepRemoved -= OnWorkflowStepRemoved;
                    _workflowState.StepsChanged -= OnWorkflowStepsChanged;
                }
            }

            base.Dispose(disposing);
        }

        #endregion
    }

    #region 枚举和事件参数

    /// <summary>
    /// 布局模式
    /// </summary>
    public enum LayoutMode
    {
        /// <summary>水平布局</summary>
        Horizontal,
        /// <summary>垂直布局</summary>
        Vertical,
        /// <summary>树形布局</summary>
        Tree
    }

    /// <summary>
    /// 节点选中事件参数
    /// </summary>
    public class NodeSelectedEventArgs : EventArgs
    {
        public WorkflowNode Node { get; }
        public NodeSelectedEventArgs(WorkflowNode node) => Node = node;
    }

    /// <summary>
    /// 节点移动事件参数
    /// </summary>
    public class NodeMovedEventArgs : EventArgs
    {
        public WorkflowNode Node { get; }
        public PointF OldPosition { get; }
        public PointF NewPosition { get; }

        public NodeMovedEventArgs(WorkflowNode node, PointF oldPos, PointF newPos)
        {
            Node = node;
            OldPosition = oldPos;
            NewPosition = newPos;
        }
    }

    /// <summary>
    /// 添加节点请求事件参数
    /// </summary>
    public class AddNodeRequestEventArgs : EventArgs
    {
        public string ToolName { get; }
        public string ToolTag { get; }
        public PointF Position { get; }

        public AddNodeRequestEventArgs(string toolName, string toolTag, PointF position)
        {
            ToolName = toolName;
            ToolTag = toolTag;
            Position = position;
        }
    }

    #endregion
}

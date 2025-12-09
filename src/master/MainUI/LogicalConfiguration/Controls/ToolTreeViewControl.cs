using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Controls
{
    /// <summary>
    /// 工具箱用户控件
    /// </summary>
    public partial class ToolTreeViewControl : UserControl
    {
        #region 字段

        private UITreeView _treeView;
        private readonly ILogger<ToolTreeViewControl> _logger;

        #endregion

        #region 事件定义

        /// <summary>
        /// 当工具被选择时触发(通过拖拽或双击)
        /// </summary>
        public event EventHandler<ToolSelectedEventArgs> ToolSelected;

        /// <summary>
        /// 当工具被拖拽时触发
        /// </summary>
        public event EventHandler<ItemDragEventArgs> ToolDragging;

        /// <summary>
        /// TreeView节点选择改变事件
        /// </summary>
        public event TreeViewEventHandler AfterSelect;

        #endregion

        #region 构造函数

        /// <summary>
        /// 设计时构造函数
        /// </summary>
        public ToolTreeViewControl()
        {
            InitializeComponent();
            InitializeTreeView();
        }

        /// <summary>
        /// 运行时构造函数(带日志)
        /// </summary>
        public ToolTreeViewControl(ILogger<ToolTreeViewControl> logger) : this()
        {
            _logger = logger;
            _logger?.LogDebug("工具箱控件已创建");
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化TreeView控件 - 与原始样式完全一致
        /// </summary>
        private void InitializeTreeView()
        {
            _treeView = new UITreeView  // 或者 AntdUI.TreeView，根据你的项目使用的库
            {
                Dock = DockStyle.Fill,
                Name = "ToolTreeView",

                // 背景填充色
                FillColor = Color.FromArgb(248, 249, 250),
                FillColor2 = Color.FromArgb(248, 249, 250),

                // 字体
                Font = new Font("微软雅黑", 12F),

                // 交互颜色
                HoverColor = Color.FromArgb(227, 242, 253),      // 鼠标悬停颜色 - 淡蓝色
                SelectedColor = Color.FromArgb(25, 118, 210),    // 选中颜色 - 深蓝色
                LineColor = Color.White,                          // 连接线颜色

                // 图片列表
                ImageKey = "文件夹.png",
                SelectedImageIndex = 0,

                // ★ 布局设置
                Indent = 23,
                ItemHeight = 35,

                // 显示设置
                ShowPlusMinus = false,
                ShowText = false,

                // 边框颜色
                RectColor = Color.FromArgb(248, 249, 250),
                RectDisableColor = Color.FromArgb(248, 249, 250),

                // 滚动条样式
                ScrollBarStyleInherited = false,

                // 其他属性
                Location = new Point(8, 8),
                MinimumSize = new Size(1, 1),
                TabIndex = 0,
                Text = null,
                TextAlignment = ContentAlignment.MiddleCenter,
                ImageList = imageList1
            };

            // 注册事件
            _treeView.ItemDrag += TreeView_ItemDrag;
            _treeView.AfterSelect += TreeView_AfterSelect;
            _treeView.NodeMouseDoubleClick += TreeView_NodeMouseDoubleClick;

            Controls.Add(_treeView);

            // 加载默认工具
            LoadDefaultTools();
        }


        #endregion

        #region 公开属性

        /// <summary>
        /// 获取内部TreeView控件(用于高级自定义)
        /// </summary>
        public UITreeView TreeView => _treeView;

        /// <summary>
        /// 工具箱标题
        /// </summary>
        public string Title
        {
            get => this.Text;
            set => this.Text = value;
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 加载默认工具箱数据
        /// </summary>
        public void LoadDefaultTools()
        {
            try
            {
                _treeView.Nodes.Clear();

                // 逻辑控制组
                TreeNode logicNode = new("逻辑控制")
                {
                    Tag = "LogicControl",
                    ImageKey = "文件夹.png",
                    ForeColor = Color.FromArgb(52, 58, 64)
                };
                logicNode.Nodes.Add(new TreeNode("延时等待") { Tag = "DelayWait", ImageKey = "延时等待.png" });
                logicNode.Nodes.Add(new TreeNode("条件判断") { Tag = "ConditionJudge", ImageKey = "条件判断.png" });
                logicNode.Nodes.Add(new TreeNode("等待稳定") { Tag = "Waitingforstability", ImageKey = "等待稳定.png" });
                //logicNode.Nodes.Add(new TreeNode("检测工具") { Tag = "DetectionTool", ImageKey = "检测工具.png" });
                logicNode.Nodes.Add(new TreeNode("实时监控") { Tag = "MonitorTool", ImageKey = "检测工具.png" });
                logicNode.Nodes.Add(new TreeNode("循环工具") { Tag = "CycleBegins", ImageKey = "循环工具.png" });
                _treeView.Nodes.Add(logicNode);

                // 数据操作组
                TreeNode dataNode = new("数据操作")
                {
                    Tag = "DataOperation",
                    ImageKey = "文件夹.png",
                    ForeColor = Color.FromArgb(40, 167, 69)
                };
                dataNode.Nodes.Add(new TreeNode("变量赋值") { Tag = "VariableAssign", ImageKey = "变量赋值.png" });
                dataNode.Nodes.Add(new TreeNode("消息通知") { Tag = "MessageNotify", ImageKey = "消息通知.png" });
                _treeView.Nodes.Add(dataNode);

                // PLC通信组
                TreeNode plcNode = new("通信操作")
                {
                    Tag = "PLCCommunication",
                    ImageKey = "文件夹.png",
                    ForeColor = Color.FromArgb(13, 110, 253)
                };
                plcNode.Nodes.Add(new TreeNode("读取PLC") { Tag = "PLCRead", ImageKey = "读取PLC.png" });
                plcNode.Nodes.Add(new TreeNode("写入PLC") { Tag = "PLCWrite", ImageKey = "写入PLC.png" });
                _treeView.Nodes.Add(plcNode);

                // 报表操作组
                TreeNode reportNode = new("报表工具")
                {
                    Tag = "ReportTools",
                    ImageKey = "文件夹.png",
                    ForeColor = Color.FromArgb(255, 140, 0)
                };
                reportNode.Nodes.Add(new TreeNode("读取单元格") { Tag = "ReadExcelCell", ImageKey = "报表读取.png" });
                reportNode.Nodes.Add(new TreeNode("写入单元格") { Tag = "WriteExcelCell", ImageKey = "报表写入.png" });
                _treeView.Nodes.Add(reportNode);

                // 展开所有节点
                _treeView.ExpandAll();

                _logger?.LogDebug("工具箱数据加载完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载工具箱数据时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 刷新工具箱
        /// </summary>
        public void RefreshToolTree()
        {
            LoadDefaultTools();
        }

        /// <summary>
        /// 清空工具箱
        /// </summary>
        public void ClearTools()
        {
            _treeView.Nodes.Clear();
        }

        /// <summary>
        /// 添加自定义工具节点
        /// </summary>
        public TreeNode AddToolNode(string text, string tag, string imageKey = null, TreeNode parent = null)
        {
            var node = new TreeNode(text)
            {
                Tag = tag,
                ImageKey = imageKey
            };

            if (parent != null)
            {
                parent.Nodes.Add(node);
            }
            else
            {
                _treeView.Nodes.Add(node);
            }

            return node;
        }

        /// <summary>
        /// 展开所有节点
        /// </summary>
        public void ExpandAll()
        {
            _treeView.ExpandAll();
        }

        /// <summary>
        /// 折叠所有节点
        /// </summary>
        public void CollapseAll()
        {
            _treeView.CollapseAll();
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// TreeView拖拽事件
        /// </summary>
        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode node && node.Parent != null)
            {
                // 触发外部事件
                ToolDragging?.Invoke(this, e);

                // 触发选择事件
                ToolSelected?.Invoke(this, new ToolSelectedEventArgs(node.Text, node.Tag?.ToString()));

                // 开始拖拽
                _treeView.DoDragDrop(e.Item, DragDropEffects.Copy);

                _logger?.LogDebug("工具开始拖拽: {ToolName}", node.Text);
            }
        }

        /// <summary>
        /// TreeView节点选择改变
        /// </summary>
        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelect?.Invoke(this, e);
        }

        /// <summary>
        /// TreeView节点双击事件
        /// </summary>
        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if (e.Node?.Parent != null)
            //{
            //    // 双击也触发选择事件
            //    ToolSelected?.Invoke(this, new ToolSelectedEventArgs(e.Node.Text, e.Node.Tag?.ToString()));
            //    _logger?.LogDebug("工具双击: {ToolName}", e.Node.Text);
            //}
        }

        #endregion

    }

    #region 事件参数类

    /// <summary>
    /// 工具选择事件参数
    /// </summary>
    public class ToolSelectedEventArgs(string toolName, string toolTag) : EventArgs
    {
        public string ToolName { get; } = toolName;

        public string ToolTag { get; } = toolTag;
    }

    #endregion
}
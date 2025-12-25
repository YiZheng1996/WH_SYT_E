using System;
using System.Drawing;

namespace MainUI.LogicalConfiguration.Controls.NodeEditor
{
    /// <summary>
    /// 工作流节点 - 代表流程图中的一个步骤
    /// </summary>
    public class WorkflowNode
    {
        #region 基础属性

        /// <summary>
        /// 节点唯一标识
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 对应的步骤索引（与ChildModel.StepNum对应）
        /// </summary>
        public int StepIndex { get; set; }

        /// <summary>
        /// 节点标题（步骤名称）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 节点类型（用于确定颜色和图标）
        /// </summary>
        public NodeType Type { get; set; }

        /// <summary>
        /// 节点描述/预览文本
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 关联的ChildModel引用
        /// </summary>
        public ChildModel StepModel { get; set; }

        #endregion

        #region 位置和尺寸

        /// <summary>
        /// 节点位置（左上角）
        /// </summary>
        public PointF Position { get; set; }

        /// <summary>
        /// 节点宽度
        /// </summary>
        public float Width { get; set; } = 180;

        /// <summary>
        /// 节点高度
        /// </summary>
        public float Height { get; set; } = 70;

        /// <summary>
        /// 获取节点边界矩形
        /// </summary>
        public RectangleF Bounds => new RectangleF(Position.X, Position.Y, Width, Height);

        /// <summary>
        /// 获取节点中心点
        /// </summary>
        public PointF Center => new PointF(Position.X + Width / 2, Position.Y + Height / 2);

        /// <summary>
        /// 输入连接点位置（左侧中间）
        /// </summary>
        public PointF InputConnector => new PointF(Position.X, Position.Y + Height / 2);

        /// <summary>
        /// 输出连接点位置（右侧中间）
        /// </summary>
        public PointF OutputConnector => new PointF(Position.X + Width, Position.Y + Height / 2);

        /// <summary>
        /// 底部连接点（用于循环/条件的"否"分支）
        /// </summary>
        public PointF BottomConnector => new PointF(Position.X + Width / 2, Position.Y + Height);

        #endregion

        #region 状态属性

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 是否悬停
        /// </summary>
        public bool IsHovered { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public ExecutionState State { get; set; } = ExecutionState.Pending;

        /// <summary>
        /// 是否正在拖拽
        /// </summary>
        public bool IsDragging { get; set; }

        /// <summary>
        /// 是否折叠（用于循环/条件节点）
        /// </summary>
        public bool IsCollapsed { get; set; }

        #endregion

        #region 颜色配置

        /// <summary>
        /// 获取节点主色调（根据类型）
        /// </summary>
        public Color GetPrimaryColor()
        {
            return Type switch
            {
                NodeType.Start => Color.FromArgb(40, 167, 69),      // 绿色
                NodeType.End => Color.FromArgb(220, 53, 69),        // 红色
                NodeType.Logic => Color.FromArgb(255, 193, 7),      // 黄色
                NodeType.Communication => Color.FromArgb(0, 123, 255), // 蓝色
                NodeType.Data => Color.FromArgb(111, 66, 193),      // 紫色
                NodeType.Loop => Color.FromArgb(253, 126, 20),      // 橙色
                NodeType.Condition => Color.FromArgb(23, 162, 184), // 青色
                NodeType.Monitor => Color.FromArgb(102, 16, 242),   // 深紫
                _ => Color.FromArgb(108, 117, 125)                  // 灰色
            };
        }

        /// <summary>
        /// 获取状态指示颜色
        /// </summary>
        public Color GetStateColor()
        {
            return State switch
            {
                ExecutionState.Pending => Color.FromArgb(200, 200, 200),
                ExecutionState.Running => Color.FromArgb(255, 193, 7),
                ExecutionState.Success => Color.FromArgb(40, 167, 69),
                ExecutionState.Failed => Color.FromArgb(220, 53, 69),
                ExecutionState.Skipped => Color.FromArgb(108, 117, 125),
                _ => Color.Gray
            };
        }

        #endregion

        #region 方法

        /// <summary>
        /// 检测点是否在节点范围内
        /// </summary>
        public bool HitTest(PointF point)
        {
            return Bounds.Contains(point);
        }

        /// <summary>
        /// 检测点是否在输入连接点附近
        /// </summary>
        public bool HitTestInputConnector(PointF point, float tolerance = 10)
        {
            return Distance(point, InputConnector) <= tolerance;
        }

        /// <summary>
        /// 检测点是否在输出连接点附近
        /// </summary>
        public bool HitTestOutputConnector(PointF point, float tolerance = 10)
        {
            return Distance(point, OutputConnector) <= tolerance;
        }

        /// <summary>
        /// 计算两点距离
        /// </summary>
        private static float Distance(PointF p1, PointF p2)
        {
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 从ChildModel创建节点
        /// </summary>
        public static WorkflowNode FromChildModel(ChildModel model, int index)
        {
            return new WorkflowNode
            {
                StepIndex = index,
                Title = model.StepName,
                Type = GetNodeType(model.StepName),
                Description = model.Remark ?? "",
                StepModel = model,
                State = model.Status switch
                {
                    0 => ExecutionState.Pending,
                    1 => ExecutionState.Running,
                    2 => ExecutionState.Success,
                    3 => ExecutionState.Failed,
                    _ => ExecutionState.Pending
                }
            };
        }

        /// <summary>
        /// 根据步骤名称确定节点类型
        /// </summary>
        private static NodeType GetNodeType(string stepName)
        {
            return stepName switch
            {
                "延时等待" or "等待稳定" => NodeType.Logic,
                "条件判断" => NodeType.Condition,
                "循环工具" => NodeType.Loop,
                "变量赋值" or "数据计算" => NodeType.Data,
                "读取PLC" or "写入PLC" => NodeType.Communication,
                "读取单元格" or "写入单元格" => NodeType.Data,
                "消息通知" => NodeType.Logic,
                "实时监控" => NodeType.Monitor,
                _ => NodeType.Generic
            };
        }

        #endregion
    }

    /// <summary>
    /// 节点类型枚举
    /// </summary>
    public enum NodeType
    {
        /// <summary>通用节点</summary>
        Generic,
        /// <summary>开始节点</summary>
        Start,
        /// <summary>结束节点</summary>
        End,
        /// <summary>逻辑控制</summary>
        Logic,
        /// <summary>通信操作</summary>
        Communication,
        /// <summary>数据操作</summary>
        Data,
        /// <summary>循环节点</summary>
        Loop,
        /// <summary>条件判断</summary>
        Condition,
        /// <summary>监控节点</summary>
        Monitor
    }

    /// <summary>
    /// 执行状态枚举
    /// </summary>
    public enum ExecutionState
    {
        /// <summary>等待执行</summary>
        Pending,
        /// <summary>正在执行</summary>
        Running,
        /// <summary>执行成功</summary>
        Success,
        /// <summary>执行失败</summary>
        Failed,
        /// <summary>已跳过</summary>
        Skipped
    }
}

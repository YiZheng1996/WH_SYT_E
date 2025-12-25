using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MainUI.LogicalConfiguration.Controls.NodeEditor
{
    /// <summary>
    /// 节点连接线 - 连接两个节点的线条
    /// </summary>
    public class NodeConnection
    {
        #region 属性

        /// <summary>
        /// 连接唯一标识
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 源节点
        /// </summary>
        public WorkflowNode SourceNode { get; set; }

        /// <summary>
        /// 目标节点
        /// </summary>
        public WorkflowNode TargetNode { get; set; }

        /// <summary>
        /// 连接类型
        /// </summary>
        public ConnectionType Type { get; set; } = ConnectionType.Normal;

        /// <summary>
        /// 连接标签（如"是"、"否"）
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 是否悬停
        /// </summary>
        public bool IsHovered { get; set; }

        /// <summary>
        /// 线条颜色
        /// </summary>
        public Color LineColor { get; set; } = Color.FromArgb(150, 150, 150);

        /// <summary>
        /// 线条宽度
        /// </summary>
        public float LineWidth { get; set; } = 2f;

        #endregion

        #region 计算属性

        /// <summary>
        /// 获取起点
        /// </summary>
        public PointF StartPoint
        {
            get
            {
                if (SourceNode == null) return PointF.Empty;

                return Type switch
                {
                    ConnectionType.LoopBack => SourceNode.BottomConnector,
                    ConnectionType.ConditionFalse => SourceNode.BottomConnector,
                    _ => SourceNode.OutputConnector
                };
            }
        }

        /// <summary>
        /// 获取终点
        /// </summary>
        public PointF EndPoint
        {
            get
            {
                if (TargetNode == null) return PointF.Empty;
                return TargetNode.InputConnector;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取贝塞尔曲线的控制点
        /// </summary>
        public (PointF cp1, PointF cp2) GetControlPoints()
        {
            var start = StartPoint;
            var end = EndPoint;

            float dx = Math.Abs(end.X - start.X);
            float dy = Math.Abs(end.Y - start.Y);

            // 根据连接类型调整控制点
            switch (Type)
            {
                case ConnectionType.LoopBack:
                    // 循环回退连接 - 向下再向左
                    float loopOffset = Math.Max(50, dy * 0.5f);
                    return (
                        new PointF(start.X, start.Y + loopOffset),
                        new PointF(end.X - loopOffset, end.Y)
                    );

                case ConnectionType.ConditionFalse:
                    // 条件"否"分支 - 向下
                    float condOffset = Math.Max(30, dy * 0.3f);
                    return (
                        new PointF(start.X, start.Y + condOffset),
                        new PointF(end.X, end.Y - condOffset)
                    );

                default:
                    // 正常连接 - 水平方向优先
                    float offset = Math.Max(50, dx * 0.3f);
                    return (
                        new PointF(start.X + offset, start.Y),
                        new PointF(end.X - offset, end.Y)
                    );
            }
        }

        /// <summary>
        /// 获取绘制路径
        /// </summary>
        public GraphicsPath GetPath()
        {
            var path = new GraphicsPath();
            var start = StartPoint;
            var end = EndPoint;
            var (cp1, cp2) = GetControlPoints();

            path.AddBezier(start, cp1, cp2, end);
            return path;
        }

        /// <summary>
        /// 检测点是否在连接线附近
        /// </summary>
        public bool HitTest(PointF point, float tolerance = 8)
        {
            using (var path = GetPath())
            using (var pen = new Pen(Color.Black, tolerance * 2))
            {
                return path.IsOutlineVisible(point, pen);
            }
        }

        /// <summary>
        /// 获取标签绘制位置（曲线中点）
        /// </summary>
        public PointF GetLabelPosition()
        {
            var start = StartPoint;
            var end = EndPoint;
            var (cp1, cp2) = GetControlPoints();

            // 使用贝塞尔曲线的中点公式 (t=0.5)
            float t = 0.5f;
            float u = 1 - t;

            float x = u * u * u * start.X +
                      3 * u * u * t * cp1.X +
                      3 * u * t * t * cp2.X +
                      t * t * t * end.X;

            float y = u * u * u * start.Y +
                      3 * u * u * t * cp1.Y +
                      3 * u * t * t * cp2.Y +
                      t * t * t * end.Y;

            return new PointF(x, y);
        }

        /// <summary>
        /// 绘制连接线
        /// </summary>
        public void Draw(Graphics g)
        {
            if (SourceNode == null || TargetNode == null) return;

            var start = StartPoint;
            var end = EndPoint;
            var (cp1, cp2) = GetControlPoints();

            // 确定线条颜色
            Color color = LineColor;
            if (IsSelected)
                color = Color.FromArgb(65, 100, 204);
            else if (IsHovered)
                color = Color.FromArgb(100, 100, 100);

            // 绘制曲线
            using (var pen = new Pen(color, LineWidth))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Custom;
                pen.CustomEndCap = new AdjustableArrowCap(4, 6);

                g.DrawBezier(pen, start, cp1, cp2, end);
            }

            // 绘制标签
            if (!string.IsNullOrEmpty(Label))
            {
                var labelPos = GetLabelPosition();
                using (var font = new Font("微软雅黑", 9f))
                using (var brush = new SolidBrush(color))
                using (var format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    // 绘制标签背景
                    var labelSize = g.MeasureString(Label, font);
                    var labelRect = new RectangleF(
                        labelPos.X - labelSize.Width / 2 - 4,
                        labelPos.Y - labelSize.Height / 2 - 2,
                        labelSize.Width + 8,
                        labelSize.Height + 4);

                    using (var bgBrush = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(bgBrush, labelRect);
                    }

                    g.DrawString(Label, font, brush, labelPos, format);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 连接类型枚举
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>正常顺序连接</summary>
        Normal,
        /// <summary>条件"是"分支</summary>
        ConditionTrue,
        /// <summary>条件"否"分支</summary>
        ConditionFalse,
        /// <summary>循环回退</summary>
        LoopBack,
        /// <summary>循环退出</summary>
        LoopExit
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MainUI.LogicalConfiguration.Controls.NodeEditor
{
    /// <summary>
    /// 节点渲染器 - 负责绘制工作流节点
    /// </summary>
    public class NodeRenderer
    {
        #region 配置常量

        // 尺寸配置
        private const float CORNER_RADIUS = 8f;
        private const float CONNECTOR_RADIUS = 6f;
        private const float SHADOW_OFFSET = 3f;
        private const float ICON_SIZE = 24f;
        private const float PADDING = 10f;

        // 字体配置
        private readonly Font _titleFont = new Font("微软雅黑", 11f, FontStyle.Bold);
        private readonly Font _descFont = new Font("微软雅黑", 9f);
        private readonly Font _indexFont = new Font("微软雅黑", 8f, FontStyle.Bold);

        #endregion

        #region 主绘制方法

        /// <summary>
        /// 绘制单个节点
        /// </summary>
        public void DrawNode(Graphics g, WorkflowNode node)
        {
            if (node == null) return;

            var bounds = node.Bounds;
            var primaryColor = node.GetPrimaryColor();
            var stateColor = node.GetStateColor();

            // 1. 绘制阴影
            DrawShadow(g, bounds);

            // 2. 绘制节点主体
            DrawNodeBody(g, bounds, primaryColor, node.IsSelected, node.IsHovered);

            // 3. 绘制状态指示条
            DrawStateIndicator(g, bounds, stateColor);

            // 4. 绘制图标
            DrawNodeIcon(g, bounds, node.Type, primaryColor);

            // 5. 绘制标题和描述
            DrawNodeText(g, bounds, node);

            // 6. 绘制步骤序号
            DrawStepIndex(g, bounds, node.StepIndex);

            // 7. 绘制连接点
            DrawConnectors(g, node);

            // 8. 如果是特殊节点，绘制额外标记
            DrawSpecialMarkers(g, node);
        }

        #endregion

        #region 私有绘制方法

        /// <summary>
        /// 绘制阴影
        /// </summary>
        private void DrawShadow(Graphics g, RectangleF bounds)
        {
            var shadowRect = new RectangleF(
                bounds.X + SHADOW_OFFSET,
                bounds.Y + SHADOW_OFFSET,
                bounds.Width,
                bounds.Height);

            using (var shadowPath = CreateRoundedRectPath(shadowRect, CORNER_RADIUS))
            using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
            {
                g.FillPath(shadowBrush, shadowPath);
            }
        }

        /// <summary>
        /// 绘制节点主体
        /// </summary>
        private void DrawNodeBody(Graphics g, RectangleF bounds, Color primaryColor, bool isSelected, bool isHovered)
        {
            using (var path = CreateRoundedRectPath(bounds, CORNER_RADIUS))
            {
                // 填充背景
                Color bgColor = Color.White;
                if (isHovered)
                    bgColor = Color.FromArgb(250, 252, 255);

                using (var bgBrush = new SolidBrush(bgColor))
                {
                    g.FillPath(bgBrush, path);
                }

                // 绘制顶部色带
                var headerRect = new RectangleF(bounds.X, bounds.Y, bounds.Width, 6);
                using (var headerPath = CreateTopRoundedRectPath(headerRect, CORNER_RADIUS))
                using (var headerBrush = new SolidBrush(primaryColor))
                {
                    g.FillPath(headerBrush, headerPath);
                }

                // 绘制边框
                Color borderColor = isSelected
                    ? Color.FromArgb(65, 100, 204)
                    : (isHovered ? Color.FromArgb(150, 150, 150) : Color.FromArgb(220, 220, 220));

                float borderWidth = isSelected ? 2.5f : 1f;

                using (var borderPen = new Pen(borderColor, borderWidth))
                {
                    g.DrawPath(borderPen, path);
                }
            }
        }

        /// <summary>
        /// 绘制状态指示条
        /// </summary>
        private void DrawStateIndicator(Graphics g, RectangleF bounds, Color stateColor)
        {
            var indicatorRect = new RectangleF(
                bounds.X + 4,
                bounds.Y + bounds.Height - 8,
                bounds.Width - 8,
                4);

            using (var indicatorPath = CreateRoundedRectPath(indicatorRect, 2))
            using (var indicatorBrush = new SolidBrush(stateColor))
            {
                g.FillPath(indicatorBrush, indicatorPath);
            }
        }

        /// <summary>
        /// 绘制节点图标
        /// </summary>
        private void DrawNodeIcon(Graphics g, RectangleF bounds, NodeType type, Color color)
        {
            var iconRect = new RectangleF(
                bounds.X + PADDING,
                bounds.Y + 12,
                ICON_SIZE,
                ICON_SIZE);

            // 绘制图标背景圆
            using (var iconBgBrush = new SolidBrush(Color.FromArgb(30, color)))
            {
                g.FillEllipse(iconBgBrush, iconRect);
            }

            // 绘制图标符号
            string symbol = GetNodeSymbol(type);
            using (var symbolFont = new Font("Segoe UI Symbol", 12f))
            using (var symbolBrush = new SolidBrush(color))
            using (var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                g.DrawString(symbol, symbolFont, symbolBrush,
                    new PointF(iconRect.X + iconRect.Width / 2, iconRect.Y + iconRect.Height / 2),
                    format);
            }
        }

        /// <summary>
        /// 获取节点符号
        /// </summary>
        private string GetNodeSymbol(NodeType type)
        {
            return type switch
            {
                NodeType.Start => "▶",
                NodeType.End => "■",
                NodeType.Logic => "⏱",
                NodeType.Communication => "⇄",
                NodeType.Data => "≡",
                NodeType.Loop => "↻",
                NodeType.Condition => "◇",
                NodeType.Monitor => "◎",
                _ => "●"
            };
        }

        /// <summary>
        /// 绘制节点文本
        /// </summary>
        private void DrawNodeText(Graphics g, RectangleF bounds, WorkflowNode node)
        {
            float textX = bounds.X + PADDING + ICON_SIZE + 8;
            float textWidth = bounds.Width - PADDING - ICON_SIZE - 16;

            // 绘制标题
            var titleRect = new RectangleF(textX, bounds.Y + 10, textWidth, 22);
            using (var titleBrush = new SolidBrush(Color.FromArgb(33, 37, 41)))
            using (var format = new StringFormat
            {
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap
            })
            {
                g.DrawString(node.Title, _titleFont, titleBrush, titleRect, format);
            }

            // 绘制描述（如果有）
            if (!string.IsNullOrEmpty(node.Description))
            {
                var descRect = new RectangleF(textX, bounds.Y + 34, textWidth, 20);
                using (var descBrush = new SolidBrush(Color.FromArgb(108, 117, 125)))
                using (var format = new StringFormat
                {
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.NoWrap
                })
                {
                    g.DrawString(node.Description, _descFont, descBrush, descRect, format);
                }
            }
        }

        /// <summary>
        /// 绘制步骤序号
        /// </summary>
        private void DrawStepIndex(Graphics g, RectangleF bounds, int index)
        {
            var indexRect = new RectangleF(
                bounds.X + bounds.Width - 28,
                bounds.Y + 8,
                22,
                16);

            // 绘制序号背景
            using (var bgBrush = new SolidBrush(Color.FromArgb(65, 100, 204)))
            {
                g.FillEllipse(bgBrush, indexRect);
            }

            // 绘制序号文本
            string indexText = (index + 1).ToString();
            using (var textBrush = new SolidBrush(Color.White))
            using (var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                g.DrawString(indexText, _indexFont, textBrush,
                    new PointF(indexRect.X + indexRect.Width / 2, indexRect.Y + indexRect.Height / 2),
                    format);
            }
        }

        /// <summary>
        /// 绘制连接点
        /// </summary>
        private void DrawConnectors(Graphics g, WorkflowNode node)
        {
            // 输入连接点（左侧）
            DrawConnector(g, node.InputConnector, node.IsHovered);

            // 输出连接点（右侧）
            DrawConnector(g, node.OutputConnector, node.IsHovered);

            // 特殊节点的额外连接点
            if (node.Type == NodeType.Condition || node.Type == NodeType.Loop)
            {
                DrawConnector(g, node.BottomConnector, node.IsHovered, Color.FromArgb(255, 193, 7));
            }
        }

        /// <summary>
        /// 绘制单个连接点
        /// </summary>
        private void DrawConnector(Graphics g, PointF center, bool isHovered, Color? customColor = null)
        {
            var rect = new RectangleF(
                center.X - CONNECTOR_RADIUS,
                center.Y - CONNECTOR_RADIUS,
                CONNECTOR_RADIUS * 2,
                CONNECTOR_RADIUS * 2);

            Color fillColor = customColor ?? Color.White;
            Color borderColor = customColor ?? (isHovered ? Color.FromArgb(65, 100, 204) : Color.FromArgb(150, 150, 150));

            using (var fillBrush = new SolidBrush(fillColor))
            using (var borderPen = new Pen(borderColor, 1.5f))
            {
                g.FillEllipse(fillBrush, rect);
                g.DrawEllipse(borderPen, rect);
            }
        }

        /// <summary>
        /// 绘制特殊标记（循环、条件等）
        /// </summary>
        private void DrawSpecialMarkers(Graphics g, WorkflowNode node)
        {
            if (node.Type == NodeType.Loop)
            {
                // 循环标记 - 右下角小图标
                var markerPos = new PointF(
                    node.Position.X + node.Width - 16,
                    node.Position.Y + node.Height - 20);

                using (var font = new Font("Segoe UI Symbol", 10f))
                using (var brush = new SolidBrush(Color.FromArgb(253, 126, 20)))
                {
                    g.DrawString("↻", font, brush, markerPos);
                }
            }
            else if (node.Type == NodeType.Condition)
            {
                // 条件标记 - 显示分支标签
                var yesPos = new PointF(
                    node.Position.X + node.Width + 5,
                    node.Position.Y + node.Height / 2 - 8);

                var noPos = new PointF(
                    node.Position.X + node.Width / 2 + 5,
                    node.Position.Y + node.Height + 2);

                using (var font = new Font("微软雅黑", 8f))
                {
                    using (var yesBrush = new SolidBrush(Color.FromArgb(40, 167, 69)))
                        g.DrawString("是", font, yesBrush, yesPos);

                    using (var noBrush = new SolidBrush(Color.FromArgb(220, 53, 69)))
                        g.DrawString("否", font, noBrush, noPos);
                }
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 创建圆角矩形路径
        /// </summary>
        private GraphicsPath CreateRoundedRectPath(RectangleF rect, float radius)
        {
            var path = new GraphicsPath();
            float diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// 创建顶部圆角矩形路径
        /// </summary>
        private GraphicsPath CreateTopRoundedRectPath(RectangleF rect, float radius)
        {
            var path = new GraphicsPath();
            float diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            path.CloseFigure();

            return path;
        }

        #endregion

        #region 资源释放

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _titleFont?.Dispose();
            _descFont?.Dispose();
            _indexFont?.Dispose();
        }

        #endregion
    }
}

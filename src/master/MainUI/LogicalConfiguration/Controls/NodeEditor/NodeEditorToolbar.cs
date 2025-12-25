using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainUI.LogicalConfiguration.Controls.NodeEditor
{
    /// <summary>
    /// 节点编辑器工具栏 - 提供视图控制和布局选项
    /// </summary>
    public class NodeEditorToolbar : UserControl
    {
        #region 控件

        private Button btnZoomIn;
        private Button btnZoomOut;
        private Button btnFitView;
        private Button btnResetView;
        private ComboBox cmbLayout;
        private CheckBox chkShowGrid;
        private CheckBox chkSnapToGrid;
        private CheckBox chkShowMinimap;
        private Label lblZoom;
        private TrackBar trackZoom;

        #endregion

        #region 事件

        public event EventHandler ZoomInClicked;
        public event EventHandler ZoomOutClicked;
        public event EventHandler FitViewClicked;
        public event EventHandler ResetViewClicked;
        public event EventHandler<LayoutMode> LayoutModeChanged;
        public event EventHandler<bool> ShowGridChanged;
        public event EventHandler<bool> SnapToGridChanged;
        public event EventHandler<bool> ShowMinimapChanged;
        public event EventHandler<float> ZoomLevelChanged;

        #endregion

        #region 属性

        /// <summary>
        /// 当前缩放级别（用于显示）
        /// </summary>
        public float ZoomLevel
        {
            set
            {
                lblZoom.Text = $"{value:P0}";
                trackZoom.Value = (int)(value * 100);
            }
        }

        #endregion

        #region 构造函数

        public NodeEditorToolbar()
        {
            InitializeComponents();
            BindEvents();
        }

        #endregion

        #region 初始化

        private void InitializeComponents()
        {
            Height = 40;
            BackColor = Color.FromArgb(248, 249, 250);
            Padding = new Padding(5);

            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = false
            };

            // 缩放按钮组
            btnZoomIn = CreateButton("➕", "放大 (Ctrl++)");
            btnZoomOut = CreateButton("➖", "缩小 (Ctrl+-)");
            btnFitView = CreateButton("⛶", "适应视图");
            btnResetView = CreateButton("🔄", "重置视图 (Ctrl+0)");

            // 缩放滑块
            trackZoom = new TrackBar
            {
                Minimum = 30,
                Maximum = 200,
                Value = 100,
                Width = 120,
                Height = 30,
                TickFrequency = 10,
                SmallChange = 10,
                LargeChange = 25
            };

            lblZoom = new Label
            {
                Text = "100%",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(5, 8, 5, 0)
            };

            // 分隔符
            var separator1 = CreateSeparator();

            // 布局选择
            var lblLayout = new Label
            {
                Text = "布局:",
                AutoSize = true,
                Padding = new Padding(5, 8, 0, 0)
            };

            cmbLayout = new ComboBox
            {
                Width = 90,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(3, 5, 3, 3)
            };
            cmbLayout.Items.AddRange(new object[] { "水平", "垂直", "树形" });
            cmbLayout.SelectedIndex = 0;

            // 分隔符
            var separator2 = CreateSeparator();

            // 显示选项
            chkShowGrid = new CheckBox
            {
                Text = "网格",
                Checked = true,
                AutoSize = true,
                Padding = new Padding(5, 5, 0, 0)
            };

            chkSnapToGrid = new CheckBox
            {
                Text = "对齐",
                Checked = true,
                AutoSize = true,
                Padding = new Padding(5, 5, 0, 0)
            };

            chkShowMinimap = new CheckBox
            {
                Text = "小地图",
                Checked = true,
                AutoSize = true,
                Padding = new Padding(5, 5, 0, 0)
            };

            // 添加控件
            flowPanel.Controls.Add(btnZoomOut);
            flowPanel.Controls.Add(trackZoom);
            flowPanel.Controls.Add(btnZoomIn);
            flowPanel.Controls.Add(lblZoom);
            flowPanel.Controls.Add(btnFitView);
            flowPanel.Controls.Add(btnResetView);
            flowPanel.Controls.Add(separator1);
            flowPanel.Controls.Add(lblLayout);
            flowPanel.Controls.Add(cmbLayout);
            flowPanel.Controls.Add(separator2);
            flowPanel.Controls.Add(chkShowGrid);
            flowPanel.Controls.Add(chkSnapToGrid);
            flowPanel.Controls.Add(chkShowMinimap);

            Controls.Add(flowPanel);
        }

        private Button CreateButton(string text, string tooltip)
        {
            var btn = new Button
            {
                Text = text,
                Width = 32,
                Height = 28,
                Margin = new Padding(2),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);

            var toolTip = new ToolTip();
            toolTip.SetToolTip(btn, tooltip);

            return btn;
        }

        private Panel CreateSeparator()
        {
            return new Panel
            {
                Width = 2,
                Height = 28,
                Margin = new Padding(8, 5, 8, 5),
                BackColor = Color.FromArgb(200, 200, 200)
            };
        }

        private void BindEvents()
        {
            btnZoomIn.Click += (s, e) => ZoomInClicked?.Invoke(this, EventArgs.Empty);
            btnZoomOut.Click += (s, e) => ZoomOutClicked?.Invoke(this, EventArgs.Empty);
            btnFitView.Click += (s, e) => FitViewClicked?.Invoke(this, EventArgs.Empty);
            btnResetView.Click += (s, e) => ResetViewClicked?.Invoke(this, EventArgs.Empty);

            trackZoom.ValueChanged += (s, e) =>
            {
                float zoom = trackZoom.Value / 100f;
                lblZoom.Text = $"{zoom:P0}";
                ZoomLevelChanged?.Invoke(this, zoom);
            };

            cmbLayout.SelectedIndexChanged += (s, e) =>
            {
                var mode = (LayoutMode)cmbLayout.SelectedIndex;
                LayoutModeChanged?.Invoke(this, mode);
            };

            chkShowGrid.CheckedChanged += (s, e) =>
                ShowGridChanged?.Invoke(this, chkShowGrid.Checked);

            chkSnapToGrid.CheckedChanged += (s, e) =>
                SnapToGridChanged?.Invoke(this, chkSnapToGrid.Checked);

            chkShowMinimap.CheckedChanged += (s, e) =>
                ShowMinimapChanged?.Invoke(this, chkShowMinimap.Checked);
        }

        #endregion
    }
}

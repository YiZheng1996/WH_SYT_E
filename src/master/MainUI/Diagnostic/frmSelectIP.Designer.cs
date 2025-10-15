using Sunny.UI;

namespace MainUI.Diagnostic
{
    partial class frmSelectIP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            uiPanel1 = new UIPanel();
            lblTitle = new UILabel();
            uiPanel2 = new UIPanel();
            listBoxIP = new UIListBox();
            uiPanel3 = new UIPanel();
            lblSelectedInfo = new UILabel();
            uiPanel4 = new UIPanel();
            btnCancel = new UISymbolButton();
            btnRefresh = new UISymbolButton();
            btnConfirm = new UISymbolButton();
            uiPanel1.SuspendLayout();
            uiPanel2.SuspendLayout();
            uiPanel3.SuspendLayout();
            uiPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(lblTitle);
            uiPanel1.Dock = DockStyle.Top;
            uiPanel1.Font = new Font("微软雅黑", 12F);
            uiPanel1.Location = new Point(0, 35);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Size = new Size(600, 80);
            uiPanel1.TabIndex = 0;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("微软雅黑", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(48, 48, 48);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(600, 80);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "检测到以下IP地址\r\n请选择试验台使用的网络IP";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // uiPanel2
            // 
            uiPanel2.Controls.Add(listBoxIP);
            uiPanel2.Dock = DockStyle.Top;
            uiPanel2.Font = new Font("微软雅黑", 12F);
            uiPanel2.Location = new Point(0, 115);
            uiPanel2.Margin = new Padding(4, 5, 4, 5);
            uiPanel2.MinimumSize = new Size(1, 1);
            uiPanel2.Name = "uiPanel2";
            uiPanel2.Padding = new Padding(20, 10, 20, 10);
            uiPanel2.Radius = 0;
            uiPanel2.RadiusSides = UICornerRadiusSides.None;
            uiPanel2.RectColor = Color.FromArgb(65, 100, 204);
            uiPanel2.RectDisableColor = Color.FromArgb(65, 100, 204);
            uiPanel2.Size = new Size(600, 200);
            uiPanel2.TabIndex = 1;
            uiPanel2.Text = null;
            uiPanel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // listBoxIP
            // 
            listBoxIP.Dock = DockStyle.Left;
            listBoxIP.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            listBoxIP.HoverColor = Color.FromArgb(155, 200, 255);
            listBoxIP.ItemSelectForeColor = Color.White;
            listBoxIP.Location = new Point(20, 10);
            listBoxIP.Margin = new Padding(4, 5, 4, 5);
            listBoxIP.MinimumSize = new Size(1, 1);
            listBoxIP.Name = "listBoxIP";
            listBoxIP.Padding = new Padding(2);
            listBoxIP.Radius = 0;
            listBoxIP.RectColor = Color.FromArgb(243, 249, 255);
            listBoxIP.RectDisableColor = Color.FromArgb(243, 249, 255);
            listBoxIP.ShowText = false;
            listBoxIP.Size = new Size(560, 180);
            listBoxIP.TabIndex = 0;
            listBoxIP.Text = null;
            listBoxIP.SelectedIndexChanged += listBoxIP_SelectedIndexChanged;
            // 
            // uiPanel3
            // 
            uiPanel3.Controls.Add(lblSelectedInfo);
            uiPanel3.Dock = DockStyle.Top;
            uiPanel3.Font = new Font("微软雅黑", 12F);
            uiPanel3.Location = new Point(0, 315);
            uiPanel3.Margin = new Padding(4, 5, 4, 5);
            uiPanel3.MinimumSize = new Size(1, 1);
            uiPanel3.Name = "uiPanel3";
            uiPanel3.Padding = new Padding(20, 10, 20, 10);
            uiPanel3.Radius = 0;
            uiPanel3.RectSides = ToolStripStatusLabelBorderSides.None;
            uiPanel3.Size = new Size(600, 100);
            uiPanel3.TabIndex = 2;
            uiPanel3.Text = null;
            uiPanel3.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblSelectedInfo
            // 
            lblSelectedInfo.Dock = DockStyle.Fill;
            lblSelectedInfo.Font = new Font("微软雅黑", 11F);
            lblSelectedInfo.ForeColor = Color.Gray;
            lblSelectedInfo.Location = new Point(20, 10);
            lblSelectedInfo.Name = "lblSelectedInfo";
            lblSelectedInfo.Size = new Size(560, 80);
            lblSelectedInfo.TabIndex = 0;
            lblSelectedInfo.Text = "请选择一个IP地址";
            lblSelectedInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiPanel4
            // 
            uiPanel4.Controls.Add(btnCancel);
            uiPanel4.Controls.Add(btnRefresh);
            uiPanel4.Controls.Add(btnConfirm);
            uiPanel4.Dock = DockStyle.Fill;
            uiPanel4.Font = new Font("微软雅黑", 12F);
            uiPanel4.Location = new Point(0, 415);
            uiPanel4.Margin = new Padding(4, 5, 4, 5);
            uiPanel4.MinimumSize = new Size(1, 1);
            uiPanel4.Name = "uiPanel4";
            uiPanel4.Padding = new Padding(20);
            uiPanel4.Radius = 0;
            uiPanel4.RectColor = Color.FromArgb(65, 100, 204);
            uiPanel4.RectDisableColor = Color.FromArgb(65, 100, 204);
            uiPanel4.Size = new Size(600, 85);
            uiPanel4.TabIndex = 3;
            uiPanel4.Text = null;
            uiPanel4.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(230, 80, 80);
            btnCancel.FillColor2 = Color.FromArgb(230, 80, 80);
            btnCancel.FillHoverColor = Color.FromArgb(235, 115, 115);
            btnCancel.FillPressColor = Color.FromArgb(184, 64, 64);
            btnCancel.FillSelectedColor = Color.FromArgb(184, 64, 64);
            btnCancel.Font = new Font("微软雅黑", 12F);
            btnCancel.Location = new Point(457, 25);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(230, 80, 80);
            btnCancel.RectHoverColor = Color.FromArgb(235, 115, 115);
            btnCancel.RectPressColor = Color.FromArgb(184, 64, 64);
            btnCancel.RectSelectedColor = Color.FromArgb(184, 64, 64);
            btnCancel.Size = new Size(120, 40);
            btnCancel.Symbol = 61453;
            btnCancel.SymbolSize = 30;
            btnCancel.TabIndex = 6;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            btnCancel.Click += btnCancel_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Font = new Font("微软雅黑", 12F);
            btnRefresh.Location = new Point(40, 25);
            btnRefresh.MinimumSize = new Size(1, 1);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.Symbol = 61473;
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "刷新";
            btnRefresh.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.FillColor = Color.FromArgb(65, 100, 204);
            btnConfirm.FillColor2 = Color.FromArgb(65, 100, 204);
            btnConfirm.FillHoverColor = Color.FromArgb(80, 126, 164);
            btnConfirm.FillPressColor = Color.FromArgb(74, 131, 229);
            btnConfirm.FillSelectedColor = Color.FromArgb(74, 131, 229);
            btnConfirm.Font = new Font("微软雅黑", 12F);
            btnConfirm.Location = new Point(314, 25);
            btnConfirm.MinimumSize = new Size(1, 1);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.RectColor = Color.FromArgb(65, 100, 204);
            btnConfirm.RectHoverColor = Color.FromArgb(80, 126, 164);
            btnConfirm.RectPressColor = Color.FromArgb(74, 131, 229);
            btnConfirm.RectSelectedColor = Color.FromArgb(74, 131, 229);
            btnConfirm.Size = new Size(120, 40);
            btnConfirm.Symbol = 61452;
            btnConfirm.SymbolSize = 30;
            btnConfirm.TabIndex = 5;
            btnConfirm.Text = "确定";
            btnConfirm.TipsFont = new Font("微软雅黑", 9F);
            btnConfirm.Click += btnConfirm_Click;
            // 
            // frmSelectIP
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(600, 500);
            ControlBox = false;
            Controls.Add(uiPanel4);
            Controls.Add(uiPanel3);
            Controls.Add(uiPanel2);
            Controls.Add(uiPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSelectIP";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "选择试验台IP地址";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 600, 500);
            uiPanel1.ResumeLayout(false);
            uiPanel2.ResumeLayout(false);
            uiPanel3.ResumeLayout(false);
            uiPanel4.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private UIPanel uiPanel1;
        private UILabel lblTitle;
        private UIPanel uiPanel2;
        private UIListBox listBoxIP;
        private UIPanel uiPanel3;
        private UILabel lblSelectedInfo;
        private UIPanel uiPanel4;
        private UISymbolButton btnRefresh;
        private UISymbolButton btnCancel;
        private UISymbolButton btnConfirm;
    }
}
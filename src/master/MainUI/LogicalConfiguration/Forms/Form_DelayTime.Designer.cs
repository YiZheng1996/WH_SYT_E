namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_DelayTime
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
            components = new System.ComponentModel.Container();
            pnlMain = new UIPanel();
            pnlContent = new UIPanel();
            lblPreviewValue = new UILabel();
            lblPreview = new UILabel();
            cmbTimeUnit = new UIComboBox();
            lblTimeUnit = new UILabel();
            txtDelayValue = new UITextBox();
            lblDelayTime = new UILabel();
            pnlBottom = new UIPanel();
            btnCancel = new UISymbolButton();
            BtnSave = new UISymbolButton();
            pnlHeader = new UIPanel();
            lblDescription = new UILabel();
            toolTip = new ToolTip(components);
            pnlMain.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlBottom.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlBottom);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.White;
            pnlMain.FillColor2 = Color.White;
            pnlMain.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlMain.Location = new Point(0, 35);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.MinimumSize = new Size(1, 1);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(15);
            pnlMain.Size = new Size(450, 265);
            pnlMain.TabIndex = 0;
            pnlMain.Text = null;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(lblPreviewValue);
            pnlContent.Controls.Add(lblPreview);
            pnlContent.Controls.Add(cmbTimeUnit);
            pnlContent.Controls.Add(lblTimeUnit);
            pnlContent.Controls.Add(txtDelayValue);
            pnlContent.Controls.Add(lblDelayTime);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.FillColor = Color.White;
            pnlContent.FillColor2 = Color.White;
            pnlContent.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlContent.Location = new Point(15, 65);
            pnlContent.Margin = new Padding(4, 5, 4, 5);
            pnlContent.MinimumSize = new Size(1, 1);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(420, 125);
            pnlContent.TabIndex = 1;
            pnlContent.Text = null;
            pnlContent.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblPreviewValue
            // 
            lblPreviewValue.BackColor = Color.FromArgb(246, 255, 237);
            lblPreviewValue.Font = new Font("微软雅黑", 10F);
            lblPreviewValue.ForeColor = Color.FromArgb(82, 196, 26);
            lblPreviewValue.Location = new Point(95, 55);
            lblPreviewValue.Name = "lblPreviewValue";
            lblPreviewValue.Padding = new Padding(10, 0, 10, 0);
            lblPreviewValue.Size = new Size(320, 32);
            lblPreviewValue.TabIndex = 5;
            lblPreviewValue.Text = "1000 毫秒 (1 秒)";
            lblPreviewValue.TextAlign = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblPreviewValue, "显示实际等待时间的计算结果");
            // 
            // lblPreview
            // 
            lblPreview.BackColor = Color.Transparent;
            lblPreview.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblPreview.ForeColor = Color.FromArgb(48, 48, 48);
            lblPreview.Location = new Point(10, 60);
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new Size(80, 25);
            lblPreview.TabIndex = 4;
            lblPreview.Text = "实际等待:";
            lblPreview.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbTimeUnit
            // 
            cmbTimeUnit.DataSource = null;
            cmbTimeUnit.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbTimeUnit.FillColor = Color.White;
            cmbTimeUnit.Font = new Font("微软雅黑", 10F);
            cmbTimeUnit.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbTimeUnit.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbTimeUnit.Location = new Point(301, 12);
            cmbTimeUnit.Margin = new Padding(4, 5, 4, 5);
            cmbTimeUnit.MinimumSize = new Size(63, 0);
            cmbTimeUnit.Name = "cmbTimeUnit";
            cmbTimeUnit.Padding = new Padding(0, 0, 30, 2);
            cmbTimeUnit.Size = new Size(110, 32);
            cmbTimeUnit.SymbolSize = 24;
            cmbTimeUnit.TabIndex = 3;
            cmbTimeUnit.TextAlignment = ContentAlignment.MiddleCenter;
            toolTip.SetToolTip(cmbTimeUnit, "选择时间单位：毫秒、秒、分钟");
            cmbTimeUnit.Watermark = "";
            // 
            // lblTimeUnit
            // 
            lblTimeUnit.BackColor = Color.Transparent;
            lblTimeUnit.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblTimeUnit.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeUnit.Location = new Point(255, 15);
            lblTimeUnit.Name = "lblTimeUnit";
            lblTimeUnit.Size = new Size(50, 25);
            lblTimeUnit.TabIndex = 2;
            lblTimeUnit.Text = "单位:";
            lblTimeUnit.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDelayValue
            // 
            txtDelayValue.DoubleValue = 1000D;
            txtDelayValue.Font = new Font("微软雅黑", 10F);
            txtDelayValue.IntValue = 1000;
            txtDelayValue.Location = new Point(95, 12);
            txtDelayValue.Margin = new Padding(4, 5, 4, 5);
            txtDelayValue.MinimumSize = new Size(1, 16);
            txtDelayValue.Name = "txtDelayValue";
            txtDelayValue.Padding = new Padding(5);
            txtDelayValue.ShowText = false;
            txtDelayValue.Size = new Size(150, 32);
            txtDelayValue.TabIndex = 1;
            txtDelayValue.Text = "1000";
            txtDelayValue.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtDelayValue, "输入延时数值或使用变量，如 {DelayTime}");
            txtDelayValue.Watermark = "点击输入延时值 (按F2打开面板)";
            // 
            // lblDelayTime
            // 
            lblDelayTime.BackColor = Color.Transparent;
            lblDelayTime.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblDelayTime.ForeColor = Color.FromArgb(48, 48, 48);
            lblDelayTime.Location = new Point(10, 15);
            lblDelayTime.Name = "lblDelayTime";
            lblDelayTime.Size = new Size(80, 25);
            lblDelayTime.TabIndex = 0;
            lblDelayTime.Text = "延时时间:";
            lblDelayTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(BtnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.FillColor = Color.FromArgb(245, 247, 250);
            pnlBottom.FillColor2 = Color.FromArgb(245, 247, 250);
            pnlBottom.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlBottom.Location = new Point(15, 190);
            pnlBottom.Margin = new Padding(4, 5, 4, 5);
            pnlBottom.MinimumSize = new Size(1, 1);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(10);
            pnlBottom.Size = new Size(420, 60);
            pnlBottom.TabIndex = 2;
            pnlBottom.Text = null;
            pnlBottom.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(65, 100, 204);
            btnCancel.FillColor2 = Color.FromArgb(65, 100, 204);
            btnCancel.FillHoverColor = Color.FromArgb(80, 120, 220);
            btnCancel.FillPressColor = Color.FromArgb(50, 80, 180);
            btnCancel.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            btnCancel.Location = new Point(235, 10);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 6;
            btnCancel.RectColor = Color.FromArgb(65, 100, 204);
            btnCancel.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnCancel.Size = new Size(119, 40);
            btnCancel.Symbol = 61453;
            btnCancel.SymbolSize = 20;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取  消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            // 
            // BtnSave
            // 
            BtnSave.Anchor = AnchorStyles.None;
            BtnSave.Cursor = Cursors.Hand;
            BtnSave.FillColor = Color.FromArgb(65, 100, 204);
            BtnSave.FillColor2 = Color.FromArgb(65, 100, 204);
            BtnSave.FillHoverColor = Color.FromArgb(80, 120, 220);
            BtnSave.FillPressColor = Color.FromArgb(50, 80, 180);
            BtnSave.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            BtnSave.Location = new Point(67, 10);
            BtnSave.MinimumSize = new Size(1, 1);
            BtnSave.Name = "BtnSave";
            BtnSave.Radius = 6;
            BtnSave.RectColor = Color.FromArgb(65, 100, 204);
            BtnSave.RectDisableColor = Color.FromArgb(65, 100, 204);
            BtnSave.Size = new Size(119, 40);
            BtnSave.Symbol = 61639;
            BtnSave.SymbolSize = 20;
            BtnSave.TabIndex = 0;
            BtnSave.Text = "保存配置";
            BtnSave.TipsFont = new Font("微软雅黑", 9F);
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblDescription);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.FillColor = Color.FromArgb(230, 244, 255);
            pnlHeader.FillColor2 = Color.FromArgb(230, 244, 255);
            pnlHeader.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlHeader.Location = new Point(15, 15);
            pnlHeader.Margin = new Padding(0, 0, 0, 10);
            pnlHeader.MinimumSize = new Size(1, 1);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(12);
            pnlHeader.Radius = 8;
            pnlHeader.RectColor = Color.FromArgb(65, 100, 204);
            pnlHeader.Size = new Size(420, 50);
            pnlHeader.TabIndex = 0;
            pnlHeader.Text = null;
            pnlHeader.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblDescription
            // 
            lblDescription.BackColor = Color.Transparent;
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Font = new Font("微软雅黑", 9F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(12, 12);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(396, 26);
            lblDescription.TabIndex = 0;
            lblDescription.Text = "设置工作流暂停等待的时间，支持使用变量";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_DelayTime
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(450, 300);
            ControlBox = false;
            ControlBoxFillHoverColor = Color.FromArgb(163, 163, 163);
            Controls.Add(pnlMain);
            Font = new Font("微软雅黑", 10F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_DelayTime";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            Text = "延时设置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            pnlMain.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region 控件声明

        private Sunny.UI.UIPanel pnlMain;
        private Sunny.UI.UIPanel pnlHeader;
        private Sunny.UI.UILabel lblDescription;
        private Sunny.UI.UIPanel pnlContent;
        private Sunny.UI.UILabel lblDelayTime;
        private Sunny.UI.UITextBox txtDelayValue;
        private Sunny.UI.UILabel lblTimeUnit;
        private Sunny.UI.UIComboBox cmbTimeUnit;
        private Sunny.UI.UILabel lblPreview;
        private Sunny.UI.UILabel lblPreviewValue;
        private Sunny.UI.UIPanel pnlBottom;
        private Sunny.UI.UISymbolButton BtnSave;
        private System.Windows.Forms.ToolTip toolTip;

        #endregion

        private UISymbolButton btnCancel;
    }
}
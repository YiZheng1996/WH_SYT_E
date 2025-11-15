using Padding = System.Windows.Forms.Padding;

namespace MainUI.Procedure
{
    partial class ucItemConfiguration
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            uiPanel1 = new UIPanel();
            lstAllPoint = new UIListBox();
            lstTestPoint = new UIListBox();
            uiLabel20 = new UILabel();
            uiLabel1 = new UILabel();
            cboType = new UIComboBox();
            uiLabel9 = new UILabel();
            uiLabel2 = new UILabel();
            cboModel = new UIComboBox();
            btnRight = new UIButton();
            btnLeft = new UIButton();
            btnSave = new UIButton();
            uiLine1 = new UILine();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.BackColor = Color.FromArgb(236, 236, 236);
            uiPanel1.Controls.Add(lstAllPoint);
            uiPanel1.Controls.Add(lstTestPoint);
            uiPanel1.Controls.Add(uiLabel20);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(cboType);
            uiPanel1.Controls.Add(uiLabel9);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(cboModel);
            uiPanel1.Controls.Add(btnRight);
            uiPanel1.Controls.Add(btnLeft);
            uiPanel1.Controls.Add(btnSave);
            uiPanel1.Controls.Add(uiLine1);
            uiPanel1.Dock = DockStyle.Fill;
            uiPanel1.FillColor = Color.FromArgb(236, 236, 236);
            uiPanel1.FillColor2 = Color.FromArgb(236, 236, 236);
            uiPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel1.Location = new Point(0, 0);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.RectColor = Color.FromArgb(236, 236, 236);
            uiPanel1.RectDisableColor = Color.FromArgb(236, 236, 236);
            uiPanel1.Size = new Size(792, 787);
            uiPanel1.TabIndex = 1;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lstAllPoint
            // 
            lstAllPoint.BackColor = Color.Transparent;
            lstAllPoint.FillColor = Color.White;
            lstAllPoint.FillColor2 = Color.White;
            lstAllPoint.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            lstAllPoint.ForeColor = Color.Black;
            lstAllPoint.ForeDisableColor = Color.Black;
            lstAllPoint.HoverColor = Color.FromArgb(155, 200, 255);
            lstAllPoint.ItemSelectBackColor = Color.FromArgb(189, 179, 172);
            lstAllPoint.ItemSelectForeColor = Color.White;
            lstAllPoint.Location = new Point(444, 109);
            lstAllPoint.Margin = new Padding(4, 5, 4, 5);
            lstAllPoint.MinimumSize = new Size(1, 1);
            lstAllPoint.Name = "lstAllPoint";
            lstAllPoint.Padding = new Padding(5);
            lstAllPoint.Radius = 10;
            lstAllPoint.RectColor = Color.White;
            lstAllPoint.RectDisableColor = Color.White;
            lstAllPoint.ShowText = false;
            lstAllPoint.Size = new Size(325, 630);
            lstAllPoint.TabIndex = 1;
            lstAllPoint.Text = null;
            // 
            // lstTestPoint
            // 
            lstTestPoint.FillColor = Color.White;
            lstTestPoint.FillColor2 = Color.White;
            lstTestPoint.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lstTestPoint.ForeColor = Color.Black;
            lstTestPoint.ForeDisableColor = Color.Black;
            lstTestPoint.HoverColor = Color.FromArgb(155, 200, 255);
            lstTestPoint.ItemHeight = 30;
            lstTestPoint.ItemSelectBackColor = Color.FromArgb(189, 179, 172);
            lstTestPoint.ItemSelectForeColor = Color.White;
            lstTestPoint.Location = new Point(23, 109);
            lstTestPoint.Margin = new Padding(4, 5, 4, 5);
            lstTestPoint.MinimumSize = new Size(1, 1);
            lstTestPoint.Name = "lstTestPoint";
            lstTestPoint.Padding = new Padding(5);
            lstTestPoint.Radius = 10;
            lstTestPoint.RectColor = Color.White;
            lstTestPoint.RectDisableColor = Color.White;
            lstTestPoint.ShowText = false;
            lstTestPoint.Size = new Size(325, 630);
            lstTestPoint.TabIndex = 0;
            lstTestPoint.Text = null;
            lstTestPoint.MouseDoubleClick += lstTestPoint_MouseDoubleClick;
            // 
            // uiLabel20
            // 
            uiLabel20.AutoSize = true;
            uiLabel20.BackColor = Color.Transparent;
            uiLabel20.Font = new Font("思源黑体 CN Bold", 15F, FontStyle.Bold);
            uiLabel20.ForeColor = Color.Black;
            uiLabel20.Location = new Point(24, 76);
            uiLabel20.Name = "uiLabel20";
            uiLabel20.Size = new Size(93, 29);
            uiLabel20.TabIndex = 432;
            uiLabel20.Text = "试验项点";
            uiLabel20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 15F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.Black;
            uiLabel1.Location = new Point(442, 76);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(133, 29);
            uiLabel1.TabIndex = 433;
            uiLabel1.Text = "可选试验项点";
            uiLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cboType
            // 
            cboType.BackColor = Color.Transparent;
            cboType.DataSource = null;
            cboType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboType.FillColor = Color.FromArgb(218, 220, 230);
            cboType.FillColor2 = Color.FromArgb(218, 220, 230);
            cboType.FillDisableColor = Color.FromArgb(218, 220, 230);
            cboType.FilterMaxCount = 50;
            cboType.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            cboType.ForeColor = Color.FromArgb(46, 46, 46);
            cboType.ForeDisableColor = Color.FromArgb(46, 46, 46);
            cboType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboType.Location = new Point(126, 15);
            cboType.Margin = new Padding(4, 5, 4, 5);
            cboType.MinimumSize = new Size(63, 0);
            cboType.Name = "cboType";
            cboType.Padding = new Padding(0, 0, 30, 2);
            cboType.Radius = 10;
            cboType.RectColor = Color.Gray;
            cboType.RectDisableColor = Color.Gray;
            cboType.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            cboType.Size = new Size(200, 29);
            cboType.SymbolSize = 24;
            cboType.TabIndex = 440;
            cboType.TextAlignment = ContentAlignment.MiddleLeft;
            cboType.Watermark = "请选择";
            cboType.SelectedIndexChanged += cboType_SelectedIndexChanged;
            // 
            // uiLabel9
            // 
            uiLabel9.AutoSize = true;
            uiLabel9.BackColor = Color.Transparent;
            uiLabel9.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel9.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel9.Location = new Point(33, 19);
            uiLabel9.Name = "uiLabel9";
            uiLabel9.Size = new Size(95, 24);
            uiLabel9.TabIndex = 439;
            uiLabel9.Text = "产品类型：";
            uiLabel9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel2.ForeColor = Color.Black;
            uiLabel2.Location = new Point(447, 19);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(83, 24);
            uiLabel2.TabIndex = 438;
            uiLabel2.Text = "产品型号:";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboModel
            // 
            cboModel.BackColor = Color.Transparent;
            cboModel.DataSource = null;
            cboModel.DropDownStyle = UIDropDownStyle.DropDownList;
            cboModel.FillColor = Color.FromArgb(218, 220, 230);
            cboModel.FillColor2 = Color.FromArgb(218, 220, 230);
            cboModel.FillDisableColor = Color.FromArgb(218, 220, 230);
            cboModel.FilterMaxCount = 50;
            cboModel.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            cboModel.ForeColor = Color.Black;
            cboModel.ForeDisableColor = Color.Black;
            cboModel.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboModel.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboModel.Location = new Point(539, 15);
            cboModel.Margin = new Padding(4, 5, 4, 5);
            cboModel.MinimumSize = new Size(63, 0);
            cboModel.Name = "cboModel";
            cboModel.Padding = new Padding(0, 0, 30, 2);
            cboModel.Radius = 10;
            cboModel.RectColor = Color.Gray;
            cboModel.RectDisableColor = Color.Gray;
            cboModel.Size = new Size(200, 29);
            cboModel.SymbolSize = 24;
            cboModel.TabIndex = 437;
            cboModel.TextAlignment = ContentAlignment.MiddleLeft;
            cboModel.Watermark = "请选择";
            // 
            // btnRight
            // 
            btnRight.Cursor = Cursors.Hand;
            btnRight.Font = new Font("微软雅黑", 15F, FontStyle.Bold);
            btnRight.Location = new Point(376, 425);
            btnRight.MinimumSize = new Size(1, 1);
            btnRight.Name = "btnRight";
            btnRight.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnRight.Size = new Size(40, 40);
            btnRight.TabIndex = 436;
            btnRight.Text = "→";
            btnRight.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRight.TipsText = "1";
            btnRight.Click += btnRight_Click;
            // 
            // btnLeft
            // 
            btnLeft.Cursor = Cursors.Hand;
            btnLeft.Font = new Font("微软雅黑", 15F, FontStyle.Bold);
            btnLeft.Location = new Point(376, 321);
            btnLeft.MinimumSize = new Size(1, 1);
            btnLeft.Name = "btnLeft";
            btnLeft.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnLeft.Size = new Size(40, 40);
            btnLeft.TabIndex = 435;
            btnLeft.Text = "←";
            btnLeft.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnLeft.TipsText = "1";
            btnLeft.Click += btnLeft_Click;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnSave.Location = new Point(607, 747);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSave.Size = new Size(162, 37);
            btnSave.TabIndex = 434;
            btnSave.Text = "保 存";
            btnSave.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSave.TipsText = "1";
            btnSave.Click += btnSave_Click;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLine1.ForeColor = Color.White;
            uiLine1.LineColor = Color.White;
            uiLine1.LineColor2 = Color.White;
            uiLine1.Location = new Point(3, 46);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(789, 29);
            uiLine1.StartCap = UILineCap.Circle;
            uiLine1.TabIndex = 441;
            // 
            // ucItemConfiguration
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 236, 236);
            Controls.Add(uiPanel1);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Margin = new Padding(3, 6, 3, 6);
            Name = "ucItemConfiguration";
            Size = new Size(792, 787);
            uiPanel1.ResumeLayout(false);
            uiPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private UIPanel uiPanel1;
        private UILabel uiLabel2;
        private UIComboBox cboModel;
        private UIButton btnRight;
        private UIButton btnLeft;
        private UIButton btnSave;
        private UILabel uiLabel1;
        private UILabel uiLabel20;
        private UIListBox lstAllPoint;
        private UIListBox lstTestPoint;
        private UILabel uiLabel9;
        private UIComboBox cboType;
        private UILine uiLine1;
    }
}

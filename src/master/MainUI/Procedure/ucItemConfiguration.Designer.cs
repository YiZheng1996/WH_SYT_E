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
            _btnSelectProduct = new MainUI.Procedure.Controls.ProductSelectButton();
            btnDown = new UIButton();
            btnUp = new UIButton();
            btnCopyItem = new UIButton();
            lstAllPoint = new UIListBox();
            lstTestPoint = new UIListBox();
            uiLabel20 = new UILabel();
            uiLabel1 = new UILabel();
            uiLabel9 = new UILabel();
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
            uiPanel1.Controls.Add(_btnSelectProduct);
            uiPanel1.Controls.Add(btnDown);
            uiPanel1.Controls.Add(btnUp);
            uiPanel1.Controls.Add(btnCopyItem);
            uiPanel1.Controls.Add(lstAllPoint);
            uiPanel1.Controls.Add(lstTestPoint);
            uiPanel1.Controls.Add(uiLabel20);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(uiLabel9);
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
            // _btnSelectProduct
            // 
            _btnSelectProduct.FillColor = Color.FromArgb(218, 220, 230);
            _btnSelectProduct.FillColor2 = Color.FromArgb(218, 220, 230);
            _btnSelectProduct.Font = new Font("微软雅黑", 12F);
            _btnSelectProduct.ForeColor = Color.Black;
            _btnSelectProduct.Location = new Point(134, 13);
            _btnSelectProduct.MinimumSize = new Size(1, 1);
            _btnSelectProduct.Name = "_btnSelectProduct";
            _btnSelectProduct.PlaceholderText = "点击选择产品型号...";
            _btnSelectProduct.Radius = 10;
            _btnSelectProduct.RectColor = Color.Gray;
            _btnSelectProduct.Size = new Size(635, 35);
            _btnSelectProduct.TabIndex = 444;
            _btnSelectProduct.Text = "点击选择产品型号...";
            _btnSelectProduct.TextAlign = ContentAlignment.MiddleLeft;
            _btnSelectProduct.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            _btnSelectProduct.ProductSelected += productSelectButton1_ProductSelected;
            // 
            // btnDown
            // 
            btnDown.Cursor = Cursors.Hand;
            btnDown.Font = new Font("微软雅黑", 15F, FontStyle.Bold);
            btnDown.Location = new Point(376, 237);
            btnDown.MinimumSize = new Size(1, 1);
            btnDown.Name = "btnDown";
            btnDown.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnDown.Size = new Size(40, 40);
            btnDown.TabIndex = 443;
            btnDown.Text = "⬇️";
            btnDown.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDown.TipsText = "1";
            btnDown.Click += btnDown_Click;
            // 
            // btnUp
            // 
            btnUp.Cursor = Cursors.Hand;
            btnUp.Font = new Font("微软雅黑", 15F, FontStyle.Bold);
            btnUp.Location = new Point(376, 180);
            btnUp.MinimumSize = new Size(1, 1);
            btnUp.Name = "btnUp";
            btnUp.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnUp.Size = new Size(40, 40);
            btnUp.TabIndex = 442;
            btnUp.Text = "⬆️";
            btnUp.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnUp.TipsText = "1";
            btnUp.Click += btnUp_Click;
            // 
            // btnCopyItem
            // 
            btnCopyItem.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnCopyItem.Location = new Point(497, 747);
            btnCopyItem.MinimumSize = new Size(1, 1);
            btnCopyItem.Name = "btnCopyItem";
            btnCopyItem.Size = new Size(132, 37);
            btnCopyItem.TabIndex = 0;
            btnCopyItem.Text = "复制项点";
            btnCopyItem.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCopyItem.Click += btnCopyItem_Click;
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
            btnLeft.Location = new Point(376, 368);
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
            btnSave.Location = new Point(637, 747);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSave.Size = new Size(132, 37);
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
        private UIButton btnRight;
        private UIButton btnLeft;
        private UIButton btnSave;
        private UILabel uiLabel1;
        private UILabel uiLabel20;
        private UIListBox lstAllPoint;
        private UIListBox lstTestPoint;
        private UILabel uiLabel9;
        private UILine uiLine1;
        private UIButton btnCopyItem;
        private UIButton btnDown;
        private UIButton btnUp;
        private Controls.ProductSelectButton _btnSelectProduct;
    }
}

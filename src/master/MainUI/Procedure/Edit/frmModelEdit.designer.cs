namespace MainUI
{
    partial class frmModelEdit
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            uiLabel1 = new UILabel();
            btnCancel = new UIButton();
            btnSubmit = new UIButton();
            uiPanel1 = new UIPanel();
            txtDrawingNo = new UITextBox();
            uiLabel4 = new UILabel();
            uiLabel2 = new UILabel();
            cboModelType = new UIComboBox();
            txtModelType = new UITextBox();
            txtMark = new UITextBox();
            uiLabel3 = new UILabel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel1.Location = new Point(111, 91);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(84, 25);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "型号名称";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCancel.Location = new Point(281, 305);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 10;
            btnCancel.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Size = new Size(126, 37);
            btnCancel.TabIndex = 398;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.TipsText = "1";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.Transparent;
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnSubmit.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSubmit.Location = new Point(117, 305);
            btnSubmit.MinimumSize = new Size(1, 1);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Radius = 10;
            btnSubmit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSubmit.Size = new Size(126, 37);
            btnSubmit.TabIndex = 397;
            btnSubmit.Text = "确定";
            btnSubmit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSubmit.TipsText = "1";
            btnSubmit.Click += BtnSubmit_Click;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(txtDrawingNo);
            uiPanel1.Controls.Add(uiLabel4);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(cboModelType);
            uiPanel1.Controls.Add(txtModelType);
            uiPanel1.Controls.Add(txtMark);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(btnCancel);
            uiPanel1.Controls.Add(btnSubmit);
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Font = new Font("宋体", 12F);
            uiPanel1.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel1.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Location = new Point(33, 61);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(519, 375);
            uiPanel1.TabIndex = 408;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtDrawingNo
            // 
            txtDrawingNo.BackColor = Color.Transparent;
            txtDrawingNo.Cursor = Cursors.IBeam;
            txtDrawingNo.FillColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.FillColor2 = Color.FromArgb(218, 220, 230);
            txtDrawingNo.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtDrawingNo.ForeColor = Color.Black;
            txtDrawingNo.ForeDisableColor = Color.Black;
            txtDrawingNo.ForeReadOnlyColor = Color.Black;
            txtDrawingNo.Location = new Point(202, 144);
            txtDrawingNo.Margin = new Padding(4, 5, 4, 5);
            txtDrawingNo.MinimumSize = new Size(1, 16);
            txtDrawingNo.Name = "txtDrawingNo";
            txtDrawingNo.Padding = new Padding(5);
            txtDrawingNo.Radius = 10;
            txtDrawingNo.RectColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.ShowText = false;
            txtDrawingNo.Size = new Size(210, 30);
            txtDrawingNo.TabIndex = 413;
            txtDrawingNo.TextAlignment = ContentAlignment.MiddleLeft;
            txtDrawingNo.Watermark = "请输入";
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel4.Location = new Point(111, 145);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(84, 25);
            uiLabel4.TabIndex = 412;
            uiLabel4.Text = "产品图号";
            uiLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.Black;
            uiLabel2.Location = new Point(111, 36);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(84, 25);
            uiLabel2.TabIndex = 411;
            uiLabel2.Text = "产品类型";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboModelType
            // 
            cboModelType.BackColor = Color.Transparent;
            cboModelType.DataSource = null;
            cboModelType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboModelType.FillColor = Color.FromArgb(218, 220, 230);
            cboModelType.FillColor2 = Color.FromArgb(218, 220, 230);
            cboModelType.FillDisableColor = Color.White;
            cboModelType.FilterMaxCount = 50;
            cboModelType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            cboModelType.ForeColor = Color.Black;
            cboModelType.ForeDisableColor = Color.Black;
            cboModelType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboModelType.Items.AddRange(new object[] { "系统管理员" });
            cboModelType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboModelType.Location = new Point(202, 34);
            cboModelType.Margin = new Padding(4, 5, 4, 5);
            cboModelType.MinimumSize = new Size(63, 0);
            cboModelType.Name = "cboModelType";
            cboModelType.Padding = new Padding(0, 0, 30, 2);
            cboModelType.Radius = 10;
            cboModelType.RectColor = Color.Gray;
            cboModelType.RectDisableColor = Color.Gray;
            cboModelType.RectSides = ToolStripStatusLabelBorderSides.None;
            cboModelType.Size = new Size(210, 30);
            cboModelType.SymbolSize = 24;
            cboModelType.TabIndex = 410;
            cboModelType.TextAlignment = ContentAlignment.MiddleLeft;
            cboModelType.Watermark = "请选择";
            // 
            // txtModelType
            // 
            txtModelType.BackColor = Color.Transparent;
            txtModelType.Cursor = Cursors.IBeam;
            txtModelType.FillColor = Color.FromArgb(218, 220, 230);
            txtModelType.FillColor2 = Color.FromArgb(218, 220, 230);
            txtModelType.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtModelType.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtModelType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtModelType.ForeColor = Color.Black;
            txtModelType.ForeDisableColor = Color.Black;
            txtModelType.ForeReadOnlyColor = Color.Black;
            txtModelType.Location = new Point(202, 89);
            txtModelType.Margin = new Padding(4, 5, 4, 5);
            txtModelType.MinimumSize = new Size(1, 16);
            txtModelType.Name = "txtModelType";
            txtModelType.Padding = new Padding(5);
            txtModelType.Radius = 10;
            txtModelType.RectColor = Color.FromArgb(218, 220, 230);
            txtModelType.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtModelType.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtModelType.ShowText = false;
            txtModelType.Size = new Size(210, 30);
            txtModelType.TabIndex = 409;
            txtModelType.TextAlignment = ContentAlignment.MiddleLeft;
            txtModelType.Watermark = "请输入";
            // 
            // txtMark
            // 
            txtMark.BackColor = Color.Transparent;
            txtMark.Cursor = Cursors.IBeam;
            txtMark.FillColor = Color.FromArgb(218, 220, 230);
            txtMark.FillColor2 = Color.FromArgb(218, 220, 230);
            txtMark.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtMark.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtMark.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtMark.ForeColor = Color.Black;
            txtMark.ForeDisableColor = Color.Black;
            txtMark.ForeReadOnlyColor = Color.Black;
            txtMark.Location = new Point(202, 199);
            txtMark.Margin = new Padding(4, 5, 4, 5);
            txtMark.MinimumSize = new Size(1, 16);
            txtMark.Name = "txtMark";
            txtMark.Padding = new Padding(5);
            txtMark.Radius = 10;
            txtMark.RectColor = Color.FromArgb(218, 220, 230);
            txtMark.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtMark.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtMark.ShowText = false;
            txtMark.Size = new Size(210, 81);
            txtMark.TabIndex = 408;
            txtMark.TextAlignment = ContentAlignment.MiddleLeft;
            txtMark.Watermark = "请输入";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(111, 199);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(84, 25);
            uiLabel3.TabIndex = 74;
            uiLabel3.Text = "型号备注";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmModelEdit
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(585, 467);
            ControlBox = false;
            Controls.Add(uiPanel1);
            Font = new Font("微软雅黑", 11F);
            ForeColor = Color.FromArgb(236, 236, 236);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(0, 0);
            MinimizeBox = false;
            Name = "frmModelEdit";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "产品型号参数修改";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold);
            TitleForeColor = Color.FromArgb(236, 236, 236);
            ZoomScaleRect = new Rectangle(15, 15, 294, 282);
            Load += frmMeteringEdit_Load;
            uiPanel1.ResumeLayout(false);
            uiPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSubmit;
        private UIPanel uiPanel1;
        private UITextBox txtMark;
        private UILabel uiLabel3;
        private UITextBox txtModelType;
        private UILabel uiLabel2;
        private UIComboBox cboModelType;
        private UITextBox txtDrawingNo;
        private UILabel uiLabel4;
    }
}
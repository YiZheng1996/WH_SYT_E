namespace MainUI
{
    partial class frmErrStatisticsEdit
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
            labPrompt = new UILabel();
            txtDescribe = new UIRichTextBox();
            uiLabel5 = new UILabel();
            dateRecord = new UIDatePicker();
            CboErrType = new UIComboBox();
            uiLabel3 = new UILabel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel1.Location = new Point(101, 56);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(78, 24);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "问题类型";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCancel.Location = new Point(292, 374);
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
            btnSubmit.Location = new Point(128, 374);
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
            uiPanel1.Controls.Add(labPrompt);
            uiPanel1.Controls.Add(txtDescribe);
            uiPanel1.Controls.Add(uiLabel5);
            uiPanel1.Controls.Add(dateRecord);
            uiPanel1.Controls.Add(CboErrType);
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
            uiPanel1.Location = new Point(73, 68);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(519, 438);
            uiPanel1.TabIndex = 408;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // labPrompt
            // 
            labPrompt.BackColor = Color.Transparent;
            labPrompt.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            labPrompt.ForeColor = Color.Red;
            labPrompt.Location = new Point(37, 5);
            labPrompt.Name = "labPrompt";
            labPrompt.Size = new Size(445, 37);
            labPrompt.TabIndex = 407;
            labPrompt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtDescribe
            // 
            txtDescribe.BackColor = Color.Transparent;
            txtDescribe.FillColor = Color.FromArgb(218, 220, 230);
            txtDescribe.FillColor2 = Color.FromArgb(218, 220, 230);
            txtDescribe.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtDescribe.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtDescribe.ForeColor = Color.Black;
            txtDescribe.ForeDisableColor = Color.Black;
            txtDescribe.Location = new Point(202, 164);
            txtDescribe.Margin = new Padding(4, 5, 4, 5);
            txtDescribe.MinimumSize = new Size(1, 1);
            txtDescribe.Name = "txtDescribe";
            txtDescribe.Padding = new Padding(2);
            txtDescribe.Radius = 10;
            txtDescribe.RectColor = Color.FromArgb(218, 220, 230);
            txtDescribe.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtDescribe.ShowText = false;
            txtDescribe.Size = new Size(210, 180);
            txtDescribe.TabIndex = 405;
            txtDescribe.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel5
            // 
            uiLabel5.AutoSize = true;
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel5.ImeMode = ImeMode.NoControl;
            uiLabel5.Location = new Point(101, 162);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(78, 24);
            uiLabel5.TabIndex = 403;
            uiLabel5.Text = "详细内容";
            uiLabel5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dateRecord
            // 
            dateRecord.BackColor = Color.Transparent;
            dateRecord.FillColor = Color.FromArgb(218, 220, 230);
            dateRecord.FillColor2 = Color.FromArgb(218, 220, 230);
            dateRecord.FillDisableColor = Color.FromArgb(218, 220, 230);
            dateRecord.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dateRecord.ForeColor = Color.Black;
            dateRecord.Location = new Point(202, 109);
            dateRecord.Margin = new Padding(4, 5, 4, 5);
            dateRecord.MaxLength = 10;
            dateRecord.MinimumSize = new Size(63, 0);
            dateRecord.Name = "dateRecord";
            dateRecord.Padding = new Padding(0, 0, 30, 2);
            dateRecord.RectColor = Color.Gray;
            dateRecord.RectDisableColor = Color.Gray;
            dateRecord.RectSides = ToolStripStatusLabelBorderSides.None;
            dateRecord.Size = new Size(210, 31);
            dateRecord.SymbolDropDown = 61555;
            dateRecord.SymbolNormal = 61555;
            dateRecord.SymbolSize = 24;
            dateRecord.TabIndex = 400;
            dateRecord.Text = "2025-05-26";
            dateRecord.TextAlignment = ContentAlignment.MiddleLeft;
            dateRecord.Value = new DateTime(2025, 5, 26, 11, 37, 21, 220);
            dateRecord.Watermark = "";
            // 
            // CboErrType
            // 
            CboErrType.BackColor = Color.Transparent;
            CboErrType.DataSource = null;
            CboErrType.FillColor = Color.FromArgb(218, 220, 230);
            CboErrType.FillColor2 = Color.FromArgb(218, 220, 230);
            CboErrType.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboErrType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            CboErrType.ForeColor = Color.Black;
            CboErrType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboErrType.Items.AddRange(new object[] { "机械故障", "电气故障", "控制系统故障" });
            CboErrType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboErrType.Location = new Point(202, 54);
            CboErrType.Margin = new Padding(4, 5, 4, 5);
            CboErrType.MinimumSize = new Size(63, 0);
            CboErrType.Name = "CboErrType";
            CboErrType.Padding = new Padding(0, 0, 30, 2);
            CboErrType.Radius = 10;
            CboErrType.RectColor = Color.Gray;
            CboErrType.RectDisableColor = Color.Gray;
            CboErrType.RectSides = ToolStripStatusLabelBorderSides.None;
            CboErrType.Size = new Size(210, 31);
            CboErrType.SymbolSize = 24;
            CboErrType.TabIndex = 399;
            CboErrType.TextAlignment = ContentAlignment.MiddleLeft;
            CboErrType.Watermark = "请选择";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(101, 109);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(78, 24);
            uiLabel3.TabIndex = 74;
            uiLabel3.Text = "记录日期";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmErrStatisticsEdit
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(665, 541);
            ControlBox = false;
            Controls.Add(uiPanel1);
            Font = new Font("微软雅黑", 11F);
            ForeColor = Color.FromArgb(236, 236, 236);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(0, 0);
            MinimizeBox = false;
            Name = "frmErrStatisticsEdit";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "问题记录修改";
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
        private UILabel uiLabel3;
        private UIComboBox CboErrType;
        private UIDatePicker dateRecord;
        private UILabel uiLabel5;
        private UIRichTextBox txtDescribe;
        private UILabel labPrompt;
    }
}
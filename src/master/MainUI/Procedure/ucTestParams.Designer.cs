namespace MainUI.Procedure
{
    partial class UcTestParams
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
            AntdUI.Tabs.StyleCard2 styleCard21 = new AntdUI.Tabs.StyleCard2();
            openFileDialog1 = new OpenFileDialog();
            uiGroupBox1 = new UIGroupBox();
            uiLabel2 = new UILabel();
            txtType = new UITextBox();
            uiLabel9 = new UILabel();
            btnGet = new UIButton();
            txtModel = new UITextBox();
            btnBrowse = new UIButton();
            btnDelete = new UIButton();
            txtTemplateRpt = new UITextBox();
            tabs1 = new AntdUI.Tabs();
            tabPage2 = new AntdUI.TabPage();
            uiLabel1 = new UILabel();
            btnSaveBrowse = new UIButton();
            txtSaveReport = new UITextBox();
            uiLabel3 = new UILabel();
            tabPage1 = new AntdUI.TabPage();
            btnReport = new AntdUI.Button();
            btnParameter = new AntdUI.Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            uiLine1 = new UILine();
            uiLine2 = new UILine();
            tabs1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.BackColor = Color.FromArgb(224, 224, 224);
            uiGroupBox1.FillColor = Color.FromArgb(224, 224, 224);
            uiGroupBox1.FillColor2 = Color.FromArgb(224, 224, 224);
            uiGroupBox1.FillDisableColor = Color.FromArgb(42, 47, 55);
            uiGroupBox1.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            uiGroupBox1.ForeColor = Color.FromArgb(46, 46, 46);
            uiGroupBox1.ForeDisableColor = Color.FromArgb(235, 227, 221);
            uiGroupBox1.Location = new Point(0, 0);
            uiGroupBox1.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox1.Radius = 15;
            uiGroupBox1.RectColor = Color.FromArgb(224, 224, 224);
            uiGroupBox1.RectDisableColor = Color.FromArgb(224, 224, 224);
            uiGroupBox1.Size = new Size(792, 29);
            uiGroupBox1.TabIndex = 400;
            uiGroupBox1.Text = "参数设置";
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel2.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel2.Location = new Point(361, 58);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(79, 22);
            uiLabel2.TabIndex = 82;
            uiLabel2.Text = "产品型号:";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtType
            // 
            txtType.BackColor = Color.Transparent;
            txtType.Enabled = false;
            txtType.FillColor2 = Color.White;
            txtType.FillDisableColor = Color.White;
            txtType.FillReadOnlyColor = Color.White;
            txtType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtType.ForeColor = Color.Black;
            txtType.ForeDisableColor = Color.Black;
            txtType.ForeReadOnlyColor = Color.Black;
            txtType.Location = new Point(125, 55);
            txtType.Margin = new Padding(4, 5, 4, 5);
            txtType.MinimumSize = new Size(1, 16);
            txtType.Name = "txtType";
            txtType.Padding = new Padding(5);
            txtType.Radius = 10;
            txtType.ReadOnly = true;
            txtType.RectColor = Color.White;
            txtType.RectDisableColor = Color.White;
            txtType.RectReadOnlyColor = Color.White;
            txtType.ShowText = false;
            txtType.Size = new Size(198, 29);
            txtType.TabIndex = 397;
            txtType.TextAlignment = ContentAlignment.MiddleLeft;
            txtType.Watermark = "请选择";
            // 
            // uiLabel9
            // 
            uiLabel9.AutoSize = true;
            uiLabel9.BackColor = Color.Transparent;
            uiLabel9.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel9.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel9.Location = new Point(19, 58);
            uiLabel9.Name = "uiLabel9";
            uiLabel9.Size = new Size(90, 22);
            uiLabel9.TabIndex = 396;
            uiLabel9.Text = "产品类型：";
            uiLabel9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnGet
            // 
            btnGet.Cursor = Cursors.Hand;
            btnGet.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnGet.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            btnGet.ForeDisableColor = Color.White;
            btnGet.Location = new Point(675, 50);
            btnGet.MinimumSize = new Size(1, 1);
            btnGet.Name = "btnGet";
            btnGet.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnGet.Size = new Size(105, 40);
            btnGet.TabIndex = 389;
            btnGet.Text = "产品选择";
            btnGet.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnGet.TipsText = "1";
            btnGet.Click += btnProductSelection_Click;
            // 
            // txtModel
            // 
            txtModel.BackColor = Color.Transparent;
            txtModel.Enabled = false;
            txtModel.FillColor2 = Color.White;
            txtModel.FillDisableColor = Color.White;
            txtModel.FillReadOnlyColor = Color.White;
            txtModel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtModel.ForeColor = Color.FromArgb(46, 46, 46);
            txtModel.ForeDisableColor = Color.White;
            txtModel.ForeReadOnlyColor = Color.White;
            txtModel.Location = new Point(446, 55);
            txtModel.Margin = new Padding(4, 5, 4, 5);
            txtModel.MinimumSize = new Size(1, 16);
            txtModel.Name = "txtModel";
            txtModel.Padding = new Padding(5);
            txtModel.Radius = 10;
            txtModel.ReadOnly = true;
            txtModel.RectColor = Color.White;
            txtModel.RectDisableColor = Color.White;
            txtModel.RectReadOnlyColor = Color.White;
            txtModel.ShowText = false;
            txtModel.Size = new Size(198, 29);
            txtModel.TabIndex = 390;
            txtModel.TextAlignment = ContentAlignment.MiddleLeft;
            txtModel.Watermark = "请选择";
            // 
            // btnBrowse
            // 
            btnBrowse.Cursor = Cursors.Hand;
            btnBrowse.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnBrowse.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            btnBrowse.ForeDisableColor = Color.White;
            btnBrowse.Location = new Point(665, 228);
            btnBrowse.MinimumSize = new Size(1, 1);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnBrowse.Size = new Size(82, 30);
            btnBrowse.TabIndex = 394;
            btnBrowse.Text = "浏览";
            btnBrowse.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnBrowse.TipsText = "1";
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Transparent;
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnDelete.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            btnDelete.ForeDisableColor = Color.White;
            btnDelete.Location = new Point(606, 744);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnDelete.Size = new Size(183, 40);
            btnDelete.TabIndex = 396;
            btnDelete.Text = "保存";
            btnDelete.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.TipsText = "1";
            btnDelete.Click += btnOK_Click;
            // 
            // txtTemplateRpt
            // 
            txtTemplateRpt.Enabled = false;
            txtTemplateRpt.FillColor = Color.FromArgb(218, 220, 230);
            txtTemplateRpt.FillColor2 = Color.FromArgb(218, 220, 230);
            txtTemplateRpt.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtTemplateRpt.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtTemplateRpt.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtTemplateRpt.ForeColor = Color.FromArgb(46, 46, 46);
            txtTemplateRpt.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtTemplateRpt.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtTemplateRpt.Location = new Point(45, 229);
            txtTemplateRpt.Margin = new Padding(4, 5, 4, 5);
            txtTemplateRpt.MinimumSize = new Size(1, 16);
            txtTemplateRpt.Name = "txtTemplateRpt";
            txtTemplateRpt.Padding = new Padding(5);
            txtTemplateRpt.ReadOnly = true;
            txtTemplateRpt.RectColor = Color.FromArgb(218, 220, 230);
            txtTemplateRpt.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtTemplateRpt.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtTemplateRpt.ShowText = false;
            txtTemplateRpt.Size = new Size(603, 29);
            txtTemplateRpt.TabIndex = 393;
            txtTemplateRpt.TextAlignment = ContentAlignment.MiddleLeft;
            txtTemplateRpt.Watermark = "请选择";
            // 
            // tabs1
            // 
            tabs1.BackColor = Color.White;
            tabs1.Controls.Add(tabPage1);
            tabs1.Controls.Add(tabPage2);
            tabs1.Location = new Point(0, 151);
            tabs1.Name = "tabs1";
            tabs1.Pages.Add(tabPage1);
            tabs1.Pages.Add(tabPage2);
            tabs1.Size = new Size(792, 587);
            styleCard21.Closable = AntdUI.Tabs.StyleCard2.CloseType.none;
            tabs1.Style = styleCard21;
            tabs1.TabIndex = 401;
            tabs1.TabMenuVisible = false;
            tabs1.Text = "tabs1";
            tabs1.Type = AntdUI.TabType.Card2;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.White;
            tabPage2.Controls.Add(uiLabel1);
            tabPage2.Controls.Add(btnSaveBrowse);
            tabPage2.Controls.Add(txtSaveReport);
            tabPage2.Controls.Add(uiLabel3);
            tabPage2.Controls.Add(btnBrowse);
            tabPage2.Controls.Add(txtTemplateRpt);
            tabPage2.Dock = DockStyle.Fill;
            tabPage2.Location = new Point(0, 0);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(792, 587);
            tabPage2.TabIndex = 0;
            tabPage2.Text = "报表模板";
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel1.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel1.Location = new Point(45, 333);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(122, 22);
            uiLabel1.TabIndex = 400;
            uiLabel1.Text = "报表保存路径：";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSaveBrowse
            // 
            btnSaveBrowse.Cursor = Cursors.Hand;
            btnSaveBrowse.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnSaveBrowse.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            btnSaveBrowse.ForeDisableColor = Color.White;
            btnSaveBrowse.Location = new Point(665, 364);
            btnSaveBrowse.MinimumSize = new Size(1, 1);
            btnSaveBrowse.Name = "btnSaveBrowse";
            btnSaveBrowse.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSaveBrowse.Size = new Size(82, 30);
            btnSaveBrowse.TabIndex = 399;
            btnSaveBrowse.Text = "浏览";
            btnSaveBrowse.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSaveBrowse.TipsText = "1";
            btnSaveBrowse.Click += btnSaveBrowse_Click;
            // 
            // txtSaveReport
            // 
            txtSaveReport.Enabled = false;
            txtSaveReport.FillColor = Color.FromArgb(218, 220, 230);
            txtSaveReport.FillColor2 = Color.FromArgb(218, 220, 230);
            txtSaveReport.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtSaveReport.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtSaveReport.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtSaveReport.ForeColor = Color.FromArgb(46, 46, 46);
            txtSaveReport.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtSaveReport.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtSaveReport.Location = new Point(45, 364);
            txtSaveReport.Margin = new Padding(4, 5, 4, 5);
            txtSaveReport.MinimumSize = new Size(1, 16);
            txtSaveReport.Name = "txtSaveReport";
            txtSaveReport.Padding = new Padding(5);
            txtSaveReport.ReadOnly = true;
            txtSaveReport.RectColor = Color.FromArgb(218, 220, 230);
            txtSaveReport.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtSaveReport.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtSaveReport.ShowText = false;
            txtSaveReport.Size = new Size(603, 29);
            txtSaveReport.TabIndex = 398;
            txtSaveReport.TextAlignment = ContentAlignment.MiddleLeft;
            txtSaveReport.Watermark = "请选择";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel3.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel3.Location = new Point(45, 198);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(154, 22);
            uiLabel3.TabIndex = 397;
            uiLabel3.Text = "报表模板打开路径：";
            uiLabel3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.White;
            tabPage1.Dock = DockStyle.Fill;
            tabPage1.Location = new Point(0, 0);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(792, 587);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "试验参数";
            // 
            // btnReport
            // 
            btnReport.BackActive = Color.FromArgb(196, 199, 204);
            btnReport.BackColor = Color.FromArgb(196, 199, 204);
            btnReport.BorderWidth = 1F;
            btnReport.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnReport.ForeColor = Color.White;
            btnReport.JoinMode = AntdUI.TJoinMode.Right;
            btnReport.Location = new Point(117, 117);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(124, 35);
            btnReport.TabIndex = 496;
            btnReport.Text = "报表模板";
            btnReport.Type = AntdUI.TTypeMini.Primary;
            btnReport.WaveSize = 1;
            btnReport.Click += btnReport_Click;
            // 
            // btnParameter
            // 
            btnParameter.BackActive = Color.FromArgb(49, 54, 64);
            btnParameter.BackColor = Color.White;
            btnParameter.BorderWidth = 1F;
            btnParameter.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnParameter.ForeColor = Color.Black;
            btnParameter.JoinMode = AntdUI.TJoinMode.Left;
            btnParameter.Location = new Point(-3, 117);
            btnParameter.Name = "btnParameter";
            btnParameter.Size = new Size(119, 35);
            btnParameter.TabIndex = 495;
            btnParameter.Text = "参数界面";
            btnParameter.Type = AntdUI.TTypeMini.Primary;
            btnParameter.WaveSize = 1;
            btnParameter.Click += btnParameter_Click;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLine1.ForeColor = Color.White;
            uiLine1.LineColor = Color.White;
            uiLine1.LineColor2 = Color.White;
            uiLine1.Location = new Point(2, 88);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(787, 29);
            uiLine1.TabIndex = 497;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLine2.ForeColor = Color.White;
            uiLine2.LineColor = Color.White;
            uiLine2.LineColor2 = Color.White;
            uiLine2.Location = new Point(2, 21);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(787, 29);
            uiLine2.StartCap = UILineCap.Circle;
            uiLine2.TabIndex = 498;
            // 
            // UcTestParams
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            Controls.Add(uiGroupBox1);
            Controls.Add(uiLabel2);
            Controls.Add(uiLine2);
            Controls.Add(txtType);
            Controls.Add(tabs1);
            Controls.Add(txtModel);
            Controls.Add(btnReport);
            Controls.Add(uiLabel9);
            Controls.Add(btnGet);
            Controls.Add(btnParameter);
            Controls.Add(btnDelete);
            Controls.Add(uiLine1);
            Name = "UcTestParams";
            Size = new Size(792, 787);
            tabs1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIButton btnGet;
        private Sunny.UI.UITextBox txtModel;
        private Sunny.UI.UIButton btnDelete;
        private Sunny.UI.UIButton btnBrowse;
        private Sunny.UI.UITextBox txtTemplateRpt;
        private AntdUI.Tabs tabs1;
        private AntdUI.TabPage tabPage1;
        private AntdUI.TabPage tabPage2;
        private UILabel uiLabel3;
        private UILabel uiLabel9;
        private UITextBox txtType;
        private AntdUI.Button btnReport;
        private AntdUI.Button btnParameter;
        private UILabel uiLabel1;
        private UIButton btnSaveBrowse;
        private UITextBox txtSaveReport;
        private FolderBrowserDialog folderBrowserDialog1;
        private UILine uiLine1;
        private UILine uiLine2;
    }
}

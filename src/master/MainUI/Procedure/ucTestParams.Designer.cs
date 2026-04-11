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
            btnBrowse = new UIButton();
            btnDelete = new UIButton();
            txtTemplateRpt = new UITextBox();
            tabs1 = new AntdUI.Tabs();
            tabPage1 = new AntdUI.TabPage();
            txtForemanCellName = new UITextBox();
            txtForemanName = new UITextBox();
            uiLabel5 = new UILabel();
            uiLabel4 = new UILabel();
            tabPage2 = new AntdUI.TabPage();
            uiLabel1 = new UILabel();
            btnSaveBrowse = new UIButton();
            txtSaveReport = new UITextBox();
            uiLabel3 = new UILabel();
            chkSavePDF = new UICheckBox();
            lblFileNameConfig = new UILabel();
            chkIncludeModelName = new UICheckBox();
            chkIncludeProductNo = new UICheckBox();
            chkIncludeTestResult = new UICheckBox();
            chkIncludeSaveTime = new UICheckBox();
            lblExcelPassword = new UILabel();
            txtExcelPassword = new UITextBox();
            btnReport = new AntdUI.Button();
            btnParameter = new AntdUI.Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            uiLine1 = new UILine();
            uiLine2 = new UILine();
            productSelectButton = new MainUI.Procedure.Controls.ProductSelectButton();
            uiLabel2 = new UILabel();
            uiLabel6 = new UILabel();
            txtOverallJudgment = new UITextBox();
            tabs1.SuspendLayout();
            tabPage1.SuspendLayout();
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
            // btnBrowse
            // 
            btnBrowse.Cursor = Cursors.Hand;
            btnBrowse.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnBrowse.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            btnBrowse.ForeDisableColor = Color.White;
            btnBrowse.Location = new Point(665, 144);
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
            txtTemplateRpt.Location = new Point(45, 145);
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
            // tabPage1
            // 
            tabPage1.BackColor = Color.White;
            tabPage1.Controls.Add(txtOverallJudgment);
            tabPage1.Controls.Add(uiLabel6);
            tabPage1.Controls.Add(txtForemanCellName);
            tabPage1.Controls.Add(txtForemanName);
            tabPage1.Controls.Add(uiLabel5);
            tabPage1.Controls.Add(uiLabel4);
            tabPage1.Dock = DockStyle.Fill;
            tabPage1.Location = new Point(0, 0);
            tabPage1.Name = "tabPage1";
            tabPage1.Showed = true;
            tabPage1.Size = new Size(792, 587);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "试验参数";
            // 
            // txtForemanCellName
            // 
            txtForemanCellName.FillColor = Color.FromArgb(218, 220, 230);
            txtForemanCellName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtForemanCellName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtForemanCellName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtForemanCellName.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtForemanCellName.ForeColor = Color.FromArgb(46, 46, 46);
            txtForemanCellName.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtForemanCellName.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtForemanCellName.Location = new Point(520, 88);
            txtForemanCellName.Margin = new Padding(4, 5, 4, 5);
            txtForemanCellName.MinimumSize = new Size(1, 16);
            txtForemanCellName.Name = "txtForemanCellName";
            txtForemanCellName.Padding = new Padding(5);
            txtForemanCellName.RectColor = Color.FromArgb(218, 220, 230);
            txtForemanCellName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtForemanCellName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtForemanCellName.ShowText = false;
            txtForemanCellName.Size = new Size(179, 29);
            txtForemanCellName.TabIndex = 423;
            txtForemanCellName.TextAlignment = ContentAlignment.MiddleLeft;
            txtForemanCellName.Watermark = "请输入";
            // 
            // txtForemanName
            // 
            txtForemanName.FillColor = Color.FromArgb(218, 220, 230);
            txtForemanName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtForemanName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtForemanName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtForemanName.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtForemanName.ForeColor = Color.FromArgb(46, 46, 46);
            txtForemanName.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtForemanName.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtForemanName.Location = new Point(134, 88);
            txtForemanName.Margin = new Padding(4, 5, 4, 5);
            txtForemanName.MinimumSize = new Size(1, 16);
            txtForemanName.Name = "txtForemanName";
            txtForemanName.Padding = new Padding(5);
            txtForemanName.RectColor = Color.FromArgb(218, 220, 230);
            txtForemanName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtForemanName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtForemanName.ShowText = false;
            txtForemanName.Size = new Size(179, 29);
            txtForemanName.TabIndex = 422;
            txtForemanName.TextAlignment = ContentAlignment.MiddleLeft;
            txtForemanName.Watermark = "请输入";
            // 
            // uiLabel5
            // 
            uiLabel5.AutoSize = true;
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel5.Location = new Point(382, 90);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(138, 22);
            uiLabel5.TabIndex = 420;
            uiLabel5.Text = "写入单元格名称：";
            uiLabel5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel4.Location = new Point(45, 90);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(90, 22);
            uiLabel4.TabIndex = 418;
            uiLabel4.Text = "工长姓名：";
            uiLabel4.TextAlign = ContentAlignment.MiddleLeft;
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
            tabPage2.Controls.Add(chkSavePDF);
            tabPage2.Controls.Add(lblFileNameConfig);
            tabPage2.Controls.Add(chkIncludeModelName);
            tabPage2.Controls.Add(chkIncludeProductNo);
            tabPage2.Controls.Add(chkIncludeTestResult);
            tabPage2.Controls.Add(chkIncludeSaveTime);
            tabPage2.Controls.Add(lblExcelPassword);
            tabPage2.Controls.Add(txtExcelPassword);
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
            uiLabel1.Location = new Point(45, 249);
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
            btnSaveBrowse.Location = new Point(665, 280);
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
            txtSaveReport.Location = new Point(45, 280);
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
            uiLabel3.Location = new Point(45, 114);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(154, 22);
            uiLabel3.TabIndex = 397;
            uiLabel3.Text = "报表模板打开路径：";
            uiLabel3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkSavePDF
            // 
            chkSavePDF.Cursor = Cursors.Hand;
            chkSavePDF.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            chkSavePDF.ForeColor = Color.FromArgb(46, 46, 46);
            chkSavePDF.Location = new Point(45, 333);
            chkSavePDF.MinimumSize = new Size(1, 1);
            chkSavePDF.Name = "chkSavePDF";
            chkSavePDF.Size = new Size(200, 29);
            chkSavePDF.TabIndex = 410;
            chkSavePDF.Text = "同时保存PDF文件";
            // 
            // lblFileNameConfig
            // 
            lblFileNameConfig.AutoSize = true;
            lblFileNameConfig.BackColor = Color.Transparent;
            lblFileNameConfig.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            lblFileNameConfig.ForeColor = Color.FromArgb(46, 46, 46);
            lblFileNameConfig.Location = new Point(45, 373);
            lblFileNameConfig.Name = "lblFileNameConfig";
            lblFileNameConfig.Size = new Size(218, 22);
            lblFileNameConfig.TabIndex = 411;
            lblFileNameConfig.Text = "文件名构建（按勾选顺序）：";
            lblFileNameConfig.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkIncludeModelName
            // 
            chkIncludeModelName.Checked = true;
            chkIncludeModelName.Cursor = Cursors.Hand;
            chkIncludeModelName.Font = new Font("微软雅黑", 11F);
            chkIncludeModelName.ForeColor = Color.FromArgb(46, 46, 46);
            chkIncludeModelName.Location = new Point(70, 403);
            chkIncludeModelName.MinimumSize = new Size(1, 1);
            chkIncludeModelName.Name = "chkIncludeModelName";
            chkIncludeModelName.Size = new Size(120, 29);
            chkIncludeModelName.TabIndex = 412;
            chkIncludeModelName.Text = "产品型号";
            // 
            // chkIncludeProductNo
            // 
            chkIncludeProductNo.Checked = true;
            chkIncludeProductNo.Cursor = Cursors.Hand;
            chkIncludeProductNo.Font = new Font("微软雅黑", 11F);
            chkIncludeProductNo.ForeColor = Color.FromArgb(46, 46, 46);
            chkIncludeProductNo.Location = new Point(200, 403);
            chkIncludeProductNo.MinimumSize = new Size(1, 1);
            chkIncludeProductNo.Name = "chkIncludeProductNo";
            chkIncludeProductNo.Size = new Size(120, 29);
            chkIncludeProductNo.TabIndex = 413;
            chkIncludeProductNo.Text = "产品编号";
            // 
            // chkIncludeTestResult
            // 
            chkIncludeTestResult.Checked = true;
            chkIncludeTestResult.Cursor = Cursors.Hand;
            chkIncludeTestResult.Font = new Font("微软雅黑", 11F);
            chkIncludeTestResult.ForeColor = Color.FromArgb(46, 46, 46);
            chkIncludeTestResult.Location = new Point(330, 403);
            chkIncludeTestResult.MinimumSize = new Size(1, 1);
            chkIncludeTestResult.Name = "chkIncludeTestResult";
            chkIncludeTestResult.Size = new Size(120, 29);
            chkIncludeTestResult.TabIndex = 414;
            chkIncludeTestResult.Text = "综合判定";
            // 
            // chkIncludeSaveTime
            // 
            chkIncludeSaveTime.Checked = true;
            chkIncludeSaveTime.Cursor = Cursors.Hand;
            chkIncludeSaveTime.Font = new Font("微软雅黑", 11F);
            chkIncludeSaveTime.ForeColor = Color.FromArgb(46, 46, 46);
            chkIncludeSaveTime.Location = new Point(460, 403);
            chkIncludeSaveTime.MinimumSize = new Size(1, 1);
            chkIncludeSaveTime.Name = "chkIncludeSaveTime";
            chkIncludeSaveTime.Size = new Size(120, 29);
            chkIncludeSaveTime.TabIndex = 415;
            chkIncludeSaveTime.Text = "保存时间";
            // 
            // lblExcelPassword
            // 
            lblExcelPassword.AutoSize = true;
            lblExcelPassword.BackColor = Color.Transparent;
            lblExcelPassword.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            lblExcelPassword.ForeColor = Color.FromArgb(46, 46, 46);
            lblExcelPassword.Location = new Point(45, 448);
            lblExcelPassword.Name = "lblExcelPassword";
            lblExcelPassword.Size = new Size(130, 22);
            lblExcelPassword.TabIndex = 416;
            lblExcelPassword.Text = "Excel保护密码：";
            lblExcelPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtExcelPassword
            // 
            txtExcelPassword.ButtonFillHoverColor = Color.FromArgb(215, 218, 226);
            txtExcelPassword.ButtonStyleInherited = false;
            txtExcelPassword.Cursor = Cursors.IBeam;
            txtExcelPassword.FillColor = Color.FromArgb(242, 243, 245);
            txtExcelPassword.FillColor2 = Color.FromArgb(238, 239, 241);
            txtExcelPassword.FillReadOnlyColor = Color.FromArgb(242, 243, 245);
            txtExcelPassword.Font = new Font("微软雅黑", 12F);
            txtExcelPassword.Location = new Point(177, 444);
            txtExcelPassword.Margin = new Padding(4, 5, 4, 5);
            txtExcelPassword.MinimumSize = new Size(1, 16);
            txtExcelPassword.Name = "txtExcelPassword";
            txtExcelPassword.Padding = new Padding(5);
            txtExcelPassword.PasswordChar = '*';
            txtExcelPassword.Radius = 4;
            txtExcelPassword.RectColor = Color.FromArgb(218, 220, 230);
            txtExcelPassword.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtExcelPassword.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtExcelPassword.ShowText = false;
            txtExcelPassword.Size = new Size(372, 29);
            txtExcelPassword.TabIndex = 417;
            txtExcelPassword.TextAlignment = ContentAlignment.MiddleLeft;
            txtExcelPassword.Watermark = "留空则不加密";
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
            // productSelectButton
            // 
            productSelectButton.FillColor = Color.FromArgb(218, 220, 230);
            productSelectButton.FillColor2 = Color.FromArgb(218, 220, 230);
            productSelectButton.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            productSelectButton.ForeColor = Color.Black;
            productSelectButton.Location = new Point(117, 53);
            productSelectButton.MinimumSize = new Size(1, 1);
            productSelectButton.Name = "productSelectButton";
            productSelectButton.PlaceholderText = "点击选择产品型号...";
            productSelectButton.Radius = 10;
            productSelectButton.RectColor = Color.Gray;
            productSelectButton.Size = new Size(652, 35);
            productSelectButton.TabIndex = 499;
            productSelectButton.Text = "点击选择产品型号...";
            productSelectButton.TextAlign = ContentAlignment.MiddleLeft;
            productSelectButton.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            productSelectButton.ProductSelected += productSelectButton_ProductSelected;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel2.Location = new Point(26, 56);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(90, 22);
            uiLabel2.TabIndex = 500;
            uiLabel2.Text = "产品详情：";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel6
            // 
            uiLabel6.AutoSize = true;
            uiLabel6.BackColor = Color.Transparent;
            uiLabel6.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLabel6.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel6.Location = new Point(45, 150);
            uiLabel6.Name = "uiLabel6";
            uiLabel6.Size = new Size(90, 22);
            uiLabel6.TabIndex = 424;
            uiLabel6.Text = "综合判断：";
            uiLabel6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtOverallJudgment
            // 
            txtOverallJudgment.FillColor = Color.FromArgb(218, 220, 230);
            txtOverallJudgment.FillColor2 = Color.FromArgb(218, 220, 230);
            txtOverallJudgment.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtOverallJudgment.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtOverallJudgment.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtOverallJudgment.ForeColor = Color.FromArgb(46, 46, 46);
            txtOverallJudgment.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtOverallJudgment.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtOverallJudgment.Location = new Point(134, 145);
            txtOverallJudgment.Margin = new Padding(4, 5, 4, 5);
            txtOverallJudgment.MinimumSize = new Size(1, 16);
            txtOverallJudgment.Name = "txtOverallJudgment";
            txtOverallJudgment.Padding = new Padding(5);
            txtOverallJudgment.RectColor = Color.FromArgb(218, 220, 230);
            txtOverallJudgment.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtOverallJudgment.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtOverallJudgment.ShowText = false;
            txtOverallJudgment.Size = new Size(179, 29);
            txtOverallJudgment.TabIndex = 423;
            txtOverallJudgment.TextAlignment = ContentAlignment.MiddleLeft;
            txtOverallJudgment.Watermark = "请输入";
            // 
            // UcTestParams
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            Controls.Add(uiLabel2);
            Controls.Add(productSelectButton);
            Controls.Add(uiGroupBox1);
            Controls.Add(uiLine2);
            Controls.Add(tabs1);
            Controls.Add(btnReport);
            Controls.Add(btnParameter);
            Controls.Add(btnDelete);
            Controls.Add(uiLine1);
            Name = "UcTestParams";
            Size = new Size(792, 787);
            tabs1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIButton btnDelete;
        private Sunny.UI.UIButton btnBrowse;
        private Sunny.UI.UITextBox txtTemplateRpt;
        private AntdUI.Tabs tabs1;
        private AntdUI.TabPage tabPage1;
        private AntdUI.TabPage tabPage2;
        private UILabel uiLabel3;
        private AntdUI.Button btnReport;
        private AntdUI.Button btnParameter;
        private UILabel uiLabel1;
        private UIButton btnSaveBrowse;
        private UITextBox txtSaveReport;
        private FolderBrowserDialog folderBrowserDialog1;
        private UILine uiLine1;
        private UILine uiLine2;
        private Sunny.UI.UICheckBox chkSavePDF;
        private Sunny.UI.UILabel lblFileNameConfig;
        private Sunny.UI.UICheckBox chkIncludeModelName;
        private Sunny.UI.UICheckBox chkIncludeProductNo;
        private Sunny.UI.UICheckBox chkIncludeTestResult;
        private Sunny.UI.UICheckBox chkIncludeSaveTime;
        private Sunny.UI.UILabel lblExcelPassword;
        private Sunny.UI.UITextBox txtExcelPassword;
        private UILabel uiLabel5;
        private UILabel uiLabel4;
        private UITextBox txtForemanName;
        private UITextBox txtForemanCellName;
        private Controls.ProductSelectButton productSelectButton;
        private UILabel uiLabel2;
        private UITextBox txtOverallJudgment;
        private UILabel uiLabel6;
    }
}

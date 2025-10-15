namespace MainUI
{
    partial class frmNLogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNLogs));
            lstViewNlg = new ListView();
            imageList1 = new ImageList(components);
            uiLabel1 = new UILabel();
            cboType = new UIComboBox();
            dtpStartBig = new UIDatePicker();
            uiLabel4 = new UILabel();
            btnSearch = new UIButton();
            btnClose = new UIButton();
            SuspendLayout();
            // 
            // lstViewNlg
            // 
            lstViewNlg.BackColor = Color.White;
            lstViewNlg.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            lstViewNlg.ForeColor = Color.FromArgb(46, 46, 46);
            lstViewNlg.FullRowSelect = true;
            lstViewNlg.GridLines = true;
            lstViewNlg.Location = new Point(3, 92);
            lstViewNlg.Name = "lstViewNlg";
            lstViewNlg.Size = new Size(984, 474);
            lstViewNlg.SmallImageList = imageList1;
            lstViewNlg.TabIndex = 0;
            lstViewNlg.UseCompatibleStateImageBehavior = false;
            lstViewNlg.View = View.Details;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "提示-trace.png");
            imageList1.Images.SetKeyName(1, "搜索-DEBUG.png");
            imageList1.Images.SetKeyName(2, "info-fill.png");
            imageList1.Images.SetKeyName(3, "黄色提示-WARN.png");
            imageList1.Images.SetKeyName(4, "红色感叹号-ERROR.png");
            imageList1.Images.SetKeyName(5, "红色停止-FATAL.png");
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel1.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel1.Location = new Point(23, 48);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(83, 24);
            uiLabel1.TabIndex = 74;
            uiLabel1.Text = "日志等级:";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboType
            // 
            cboType.BackColor = Color.Transparent;
            cboType.DataSource = null;
            cboType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboType.FillColor = Color.White;
            cboType.FillColor2 = Color.White;
            cboType.FillDisableColor = Color.FromArgb(218, 220, 230);
            cboType.FilterMaxCount = 50;
            cboType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            cboType.ForeColor = Color.FromArgb(46, 46, 46);
            cboType.ForeDisableColor = Color.FromArgb(46, 46, 46);
            cboType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboType.Items.AddRange(new object[] { "Trace", "Debug", "Info ", "Warn ", "Error", "Fatal" });
            cboType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboType.Location = new Point(112, 48);
            cboType.Margin = new Padding(4, 5, 4, 5);
            cboType.MinimumSize = new Size(63, 0);
            cboType.Name = "cboType";
            cboType.Padding = new Padding(0, 0, 30, 2);
            cboType.RectColor = Color.Gray;
            cboType.RectDisableColor = Color.Gray;
            cboType.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            cboType.Size = new Size(165, 29);
            cboType.SymbolSize = 24;
            cboType.TabIndex = 73;
            cboType.TextAlignment = ContentAlignment.MiddleLeft;
            cboType.Watermark = "请选择";
            cboType.SelectedIndexChanged += cboType_SelectedIndexChanged;
            // 
            // dtpStartBig
            // 
            dtpStartBig.BackColor = Color.Transparent;
            dtpStartBig.FillColor = Color.White;
            dtpStartBig.FillDisableColor = Color.FromArgb(243, 249, 255);
            dtpStartBig.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dtpStartBig.ForeColor = Color.FromArgb(46, 46, 46);
            dtpStartBig.Location = new Point(406, 48);
            dtpStartBig.Margin = new Padding(4, 5, 4, 5);
            dtpStartBig.MaxLength = 10;
            dtpStartBig.MinimumSize = new Size(63, 0);
            dtpStartBig.Name = "dtpStartBig";
            dtpStartBig.Padding = new Padding(0, 0, 30, 2);
            dtpStartBig.RectColor = Color.Gray;
            dtpStartBig.RectDisableColor = Color.Gray;
            dtpStartBig.Size = new Size(165, 29);
            dtpStartBig.SymbolDropDown = 61555;
            dtpStartBig.SymbolNormal = 61555;
            dtpStartBig.SymbolSize = 24;
            dtpStartBig.TabIndex = 392;
            dtpStartBig.Text = "2024-07-19";
            dtpStartBig.TextAlignment = ContentAlignment.MiddleLeft;
            dtpStartBig.Value = new DateTime(2024, 7, 19, 0, 0, 0, 0);
            dtpStartBig.Watermark = "";
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel4.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel4.Location = new Point(316, 50);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(83, 24);
            uiLabel4.TabIndex = 391;
            uiLabel4.Text = "记录时间:";
            uiLabel4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSearch.ForeDisableColor = Color.White;
            btnSearch.Location = new Point(635, 44);
            btnSearch.MinimumSize = new Size(1, 1);
            btnSearch.Name = "btnSearch";
            btnSearch.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSearch.Size = new Size(120, 40);
            btnSearch.TabIndex = 393;
            btnSearch.Text = "搜索";
            btnSearch.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSearch.TipsText = "1";
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClose
            // 
            btnClose.Cursor = Cursors.Hand;
            btnClose.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnClose.ForeDisableColor = Color.White;
            btnClose.Location = new Point(415, 572);
            btnClose.MinimumSize = new Size(1, 1);
            btnClose.Name = "btnClose";
            btnClose.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnClose.Size = new Size(160, 35);
            btnClose.TabIndex = 394;
            btnClose.Text = "退  出";
            btnClose.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnClose.TipsText = "1";
            btnClose.Click += btnClose_Click;
            // 
            // frmNLogs
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(990, 614);
            ControlBox = false;
            Controls.Add(btnClose);
            Controls.Add(btnSearch);
            Controls.Add(dtpStartBig);
            Controls.Add(uiLabel4);
            Controls.Add(uiLabel1);
            Controls.Add(cboType);
            Controls.Add(lstViewNlg);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmNLogs";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "日志显示";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            Load += frmNLogs_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListView lstViewNlg;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIComboBox cboType;
        private Sunny.UI.UIDatePicker dtpStartBig;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIButton btnSearch;
        private Sunny.UI.UIButton btnClose;
        private System.Windows.Forms.ImageList imageList1;
    }
}
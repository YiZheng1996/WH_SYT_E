namespace MainUI
{
    partial class FrmDataManager
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
            panel2 = new Panel();
            btnExit = new UIButton();
            btnRemove = new UIButton();
            btnView = new UIButton();
            cboType = new UIComboBox();
            grpDI = new UIGroupBox();
            CboWorkstations = new UIComboBox();
            uiLabel6 = new UILabel();
            dtpStartBig = new UIDatePicker();
            cboModel = new UIComboBox();
            uiLabel5 = new UILabel();
            dtpStartEnd = new UIDatePicker();
            uiLabel4 = new UILabel();
            txtNumber = new UITextBox();
            btnSearch = new UIButton();
            uiLabel3 = new UILabel();
            uiLabel2 = new UILabel();
            uiLabel1 = new UILabel();
            Tables = new AntdUI.Table();
            panel2.SuspendLayout();
            grpDI.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(btnExit);
            panel2.Controls.Add(btnRemove);
            panel2.Controls.Add(btnView);
            panel2.Dock = DockStyle.Bottom;
            panel2.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panel2.Location = new Point(0, 752);
            panel2.Margin = new Padding(3, 6, 3, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(1096, 62);
            panel2.TabIndex = 3;
            // 
            // btnExit
            // 
            btnExit.Cursor = Cursors.Hand;
            btnExit.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnExit.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnExit.Location = new Point(933, 12);
            btnExit.MinimumSize = new Size(1, 1);
            btnExit.Name = "btnExit";
            btnExit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnExit.Size = new Size(136, 40);
            btnExit.TabIndex = 390;
            btnExit.Text = "退出";
            btnExit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnExit.TipsText = "1";
            btnExit.Click += btnExit_Click;
            // 
            // btnRemove
            // 
            btnRemove.Cursor = Cursors.Hand;
            btnRemove.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnRemove.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnRemove.Location = new Point(478, 12);
            btnRemove.MinimumSize = new Size(1, 1);
            btnRemove.Name = "btnRemove";
            btnRemove.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnRemove.Size = new Size(136, 40);
            btnRemove.TabIndex = 389;
            btnRemove.Text = "删除";
            btnRemove.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRemove.TipsText = "1";
            btnRemove.Click += btnRemove_Click;
            // 
            // btnView
            // 
            btnView.Cursor = Cursors.Hand;
            btnView.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnView.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnView.Location = new Point(29, 12);
            btnView.MinimumSize = new Size(1, 1);
            btnView.Name = "btnView";
            btnView.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnView.Size = new Size(136, 40);
            btnView.TabIndex = 388;
            btnView.Text = "查看报表";
            btnView.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnView.TipsText = "1";
            btnView.Click += btnView_Click;
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
            cboType.Location = new Point(107, 23);
            cboType.Margin = new Padding(4, 5, 4, 5);
            cboType.MinimumSize = new Size(63, 0);
            cboType.Name = "cboType";
            cboType.Padding = new Padding(0, 0, 30, 2);
            cboType.Radius = 10;
            cboType.RectColor = Color.Gray;
            cboType.RectDisableColor = Color.Gray;
            cboType.RectSides = ToolStripStatusLabelBorderSides.None;
            cboType.Size = new Size(180, 29);
            cboType.SymbolSize = 24;
            cboType.TabIndex = 71;
            cboType.TextAlignment = ContentAlignment.MiddleLeft;
            cboType.Watermark = "请选择";
            cboType.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            cboType.WatermarkColor = Color.FromArgb(46, 46, 46);
            cboType.SelectedIndexChanged += CboType_SelectedIndexChanged;
            // 
            // grpDI
            // 
            grpDI.BackColor = Color.Transparent;
            grpDI.Controls.Add(CboWorkstations);
            grpDI.Controls.Add(uiLabel6);
            grpDI.Controls.Add(cboType);
            grpDI.Controls.Add(dtpStartBig);
            grpDI.Controls.Add(cboModel);
            grpDI.Controls.Add(uiLabel5);
            grpDI.Controls.Add(dtpStartEnd);
            grpDI.Controls.Add(uiLabel4);
            grpDI.Controls.Add(txtNumber);
            grpDI.Controls.Add(btnSearch);
            grpDI.Controls.Add(uiLabel3);
            grpDI.Controls.Add(uiLabel2);
            grpDI.Controls.Add(uiLabel1);
            grpDI.FillColor = Color.White;
            grpDI.FillColor2 = Color.White;
            grpDI.FillDisableColor = Color.White;
            grpDI.Font = new Font("微软雅黑", 11F);
            grpDI.ForeColor = Color.White;
            grpDI.ForeDisableColor = Color.White;
            grpDI.Location = new Point(16, 60);
            grpDI.Margin = new Padding(4, 5, 4, 5);
            grpDI.MinimumSize = new Size(1, 1);
            grpDI.Name = "grpDI";
            grpDI.Padding = new Padding(0, 32, 0, 0);
            grpDI.Radius = 10;
            grpDI.RectColor = Color.White;
            grpDI.RectDisableColor = Color.White;
            grpDI.Size = new Size(1064, 127);
            grpDI.TabIndex = 390;
            grpDI.Text = null;
            grpDI.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // CboWorkstations
            // 
            CboWorkstations.BackColor = Color.Transparent;
            CboWorkstations.DataSource = null;
            CboWorkstations.DropDownStyle = UIDropDownStyle.DropDownList;
            CboWorkstations.FillColor = Color.FromArgb(218, 220, 230);
            CboWorkstations.FillColor2 = Color.FromArgb(218, 220, 230);
            CboWorkstations.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboWorkstations.FilterMaxCount = 50;
            CboWorkstations.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            CboWorkstations.ForeColor = Color.FromArgb(46, 46, 46);
            CboWorkstations.ForeDisableColor = Color.FromArgb(46, 46, 46);
            CboWorkstations.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboWorkstations.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboWorkstations.Location = new Point(107, 71);
            CboWorkstations.Margin = new Padding(4, 5, 4, 5);
            CboWorkstations.MinimumSize = new Size(63, 0);
            CboWorkstations.Name = "CboWorkstations";
            CboWorkstations.Padding = new Padding(0, 0, 30, 2);
            CboWorkstations.Radius = 10;
            CboWorkstations.RectColor = Color.Gray;
            CboWorkstations.RectDisableColor = Color.Gray;
            CboWorkstations.RectSides = ToolStripStatusLabelBorderSides.None;
            CboWorkstations.Size = new Size(180, 29);
            CboWorkstations.SymbolSize = 24;
            CboWorkstations.TabIndex = 393;
            CboWorkstations.TextAlignment = ContentAlignment.MiddleLeft;
            CboWorkstations.Watermark = "请选择";
            CboWorkstations.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            CboWorkstations.WatermarkColor = Color.FromArgb(46, 46, 46);
            CboWorkstations.SelectedIndexChanged += CboWorkstations_SelectedIndexChanged;
            // 
            // uiLabel6
            // 
            uiLabel6.AutoSize = true;
            uiLabel6.BackColor = Color.Transparent;
            uiLabel6.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel6.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel6.Location = new Point(22, 74);
            uiLabel6.Name = "uiLabel6";
            uiLabel6.Size = new Size(84, 26);
            uiLabel6.TabIndex = 394;
            uiLabel6.Text = "试验工位";
            uiLabel6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpStartBig
            // 
            dtpStartBig.BackColor = Color.Transparent;
            dtpStartBig.FillColor = Color.FromArgb(218, 220, 230);
            dtpStartBig.FillColor2 = Color.FromArgb(218, 220, 230);
            dtpStartBig.FillDisableColor = Color.FromArgb(43, 46, 57);
            dtpStartBig.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dtpStartBig.ForeColor = Color.FromArgb(46, 46, 46);
            dtpStartBig.ForeDisableColor = Color.FromArgb(235, 227, 221);
            dtpStartBig.Location = new Point(399, 71);
            dtpStartBig.Margin = new Padding(4, 5, 4, 5);
            dtpStartBig.MaxLength = 10;
            dtpStartBig.MinimumSize = new Size(63, 0);
            dtpStartBig.Name = "dtpStartBig";
            dtpStartBig.Padding = new Padding(0, 0, 30, 2);
            dtpStartBig.Radius = 10;
            dtpStartBig.RectColor = Color.Gray;
            dtpStartBig.RectDisableColor = Color.Gray;
            dtpStartBig.RectSides = ToolStripStatusLabelBorderSides.None;
            dtpStartBig.Size = new Size(174, 29);
            dtpStartBig.SymbolDropDown = 61555;
            dtpStartBig.SymbolNormal = 61555;
            dtpStartBig.SymbolSize = 24;
            dtpStartBig.TabIndex = 390;
            dtpStartBig.Text = "2023-02-01";
            dtpStartBig.TextAlignment = ContentAlignment.MiddleLeft;
            dtpStartBig.Value = new DateTime(2023, 2, 1, 16, 20, 20, 721);
            dtpStartBig.Watermark = "";
            dtpStartBig.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            dtpStartBig.WatermarkColor = Color.FromArgb(46, 46, 46);
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
            cboModel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            cboModel.ForeColor = Color.FromArgb(46, 46, 46);
            cboModel.ForeDisableColor = Color.FromArgb(235, 227, 221);
            cboModel.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboModel.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboModel.Location = new Point(399, 23);
            cboModel.Margin = new Padding(4, 5, 4, 5);
            cboModel.MinimumSize = new Size(63, 0);
            cboModel.Name = "cboModel";
            cboModel.Padding = new Padding(0, 0, 30, 2);
            cboModel.Radius = 10;
            cboModel.RectColor = Color.Gray;
            cboModel.RectDisableColor = Color.Gray;
            cboModel.RectSides = ToolStripStatusLabelBorderSides.None;
            cboModel.Size = new Size(174, 29);
            cboModel.SymbolSize = 24;
            cboModel.TabIndex = 73;
            cboModel.TextAlignment = ContentAlignment.MiddleLeft;
            cboModel.Watermark = "请选择";
            cboModel.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            cboModel.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // uiLabel5
            // 
            uiLabel5.AutoSize = true;
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel5.Location = new Point(634, 74);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(30, 26);
            uiLabel5.TabIndex = 392;
            uiLabel5.Text = "至";
            uiLabel5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpStartEnd
            // 
            dtpStartEnd.BackColor = Color.Transparent;
            dtpStartEnd.FillColor = Color.FromArgb(218, 220, 230);
            dtpStartEnd.FillColor2 = Color.FromArgb(218, 220, 230);
            dtpStartEnd.FillDisableColor = Color.FromArgb(43, 46, 57);
            dtpStartEnd.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dtpStartEnd.ForeColor = Color.FromArgb(46, 46, 46);
            dtpStartEnd.ForeDisableColor = Color.FromArgb(235, 227, 221);
            dtpStartEnd.Location = new Point(706, 71);
            dtpStartEnd.Margin = new Padding(4, 5, 4, 5);
            dtpStartEnd.MaxLength = 10;
            dtpStartEnd.MinimumSize = new Size(63, 0);
            dtpStartEnd.Name = "dtpStartEnd";
            dtpStartEnd.Padding = new Padding(0, 0, 30, 2);
            dtpStartEnd.Radius = 10;
            dtpStartEnd.RectColor = Color.Gray;
            dtpStartEnd.RectDisableColor = Color.Gray;
            dtpStartEnd.RectSides = ToolStripStatusLabelBorderSides.None;
            dtpStartEnd.Size = new Size(174, 29);
            dtpStartEnd.SymbolDropDown = 61555;
            dtpStartEnd.SymbolNormal = 61555;
            dtpStartEnd.SymbolSize = 24;
            dtpStartEnd.TabIndex = 391;
            dtpStartEnd.Text = "2023-02-01";
            dtpStartEnd.TextAlignment = ContentAlignment.MiddleLeft;
            dtpStartEnd.Value = new DateTime(2023, 2, 1, 16, 20, 20, 721);
            dtpStartEnd.Watermark = "";
            dtpStartEnd.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            dtpStartEnd.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel4.Location = new Point(309, 74);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(84, 26);
            uiLabel4.TabIndex = 389;
            uiLabel4.Text = "测试时间";
            uiLabel4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNumber
            // 
            txtNumber.BackColor = Color.Transparent;
            txtNumber.Cursor = Cursors.IBeam;
            txtNumber.FillColor = Color.FromArgb(218, 220, 230);
            txtNumber.FillColor2 = Color.FromArgb(218, 220, 230);
            txtNumber.FillDisableColor = Color.FromArgb(43, 46, 57);
            txtNumber.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtNumber.ForeColor = Color.FromArgb(46, 46, 46);
            txtNumber.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtNumber.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtNumber.Location = new Point(706, 23);
            txtNumber.Margin = new Padding(4, 5, 4, 5);
            txtNumber.MinimumSize = new Size(1, 16);
            txtNumber.Name = "txtNumber";
            txtNumber.Padding = new Padding(5);
            txtNumber.Radius = 10;
            txtNumber.RectColor = Color.FromArgb(218, 220, 230);
            txtNumber.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtNumber.RectSides = ToolStripStatusLabelBorderSides.None;
            txtNumber.ShowText = false;
            txtNumber.Size = new Size(174, 29);
            txtNumber.TabIndex = 388;
            txtNumber.TextAlignment = ContentAlignment.MiddleLeft;
            txtNumber.Watermark = "请输入";
            txtNumber.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            txtNumber.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // btnSearch
            // 
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnSearch.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSearch.ForeDisableColor = Color.White;
            btnSearch.Location = new Point(917, 35);
            btnSearch.MinimumSize = new Size(1, 1);
            btnSearch.Name = "btnSearch";
            btnSearch.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSearch.Size = new Size(120, 40);
            btnSearch.TabIndex = 387;
            btnSearch.Text = "搜索";
            btnSearch.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSearch.TipsText = "1";
            btnSearch.Click += btnSearch_Click;
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel3.Location = new Point(618, 26);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(84, 26);
            uiLabel3.TabIndex = 75;
            uiLabel3.Text = "产品图号";
            uiLabel3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel2.Location = new Point(309, 26);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(84, 26);
            uiLabel2.TabIndex = 74;
            uiLabel2.Text = "产品型号";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel1.Location = new Point(22, 26);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(84, 26);
            uiLabel1.TabIndex = 72;
            uiLabel1.Text = "产品类型";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Tables
            // 
            Tables.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            Tables.BackColor = Color.White;
            Tables.BackgroundImageLayout = ImageLayout.None;
            Tables.BorderColor = Color.Black;
            Tables.Bordered = true;
            Tables.CheckSize = 20;
            Tables.ClipboardCopy = false;
            Tables.ColumnBack = Color.FromArgb(44, 62, 80);
            Tables.ColumnFont = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            Tables.ColumnFore = Color.White;
            Tables.DefaultExpand = true;
            Tables.Font = new Font("微软雅黑", 14.25F);
            Tables.Gap = 12;
            Tables.ImeMode = ImeMode.NoControl;
            Tables.Location = new Point(16, 195);
            Tables.Name = "Tables";
            Tables.RightToLeft = RightToLeft.No;
            Tables.RowHeight = 50;
            Tables.RowHeightHeader = 40;
            Tables.Size = new Size(1064, 548);
            Tables.SwitchSize = 25;
            Tables.TabIndex = 407;
            Tables.CellClick += Tables_CellClick;
            Tables.CellDoubleClick += Tables_CellDoubleClick;
            // 
            // FrmDataManager
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(1096, 814);
            Controls.Add(Tables);
            Controls.Add(grpDI);
            Controls.Add(panel2);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            ForeColor = Color.White;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmDataManager";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "试验报表管理";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1015, 646);
            Load += frmDataManager_Load;
            panel2.ResumeLayout(false);
            grpDI.ResumeLayout(false);
            grpDI.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private Sunny.UI.UIComboBox cboType;
        private Sunny.UI.UIGroupBox grpDI;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIComboBox cboModel;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIButton btnSearch;
        private Sunny.UI.UITextBox txtNumber;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIDatePicker dtpStartBig;
        private Sunny.UI.UIDatePicker dtpStartEnd;
        private Sunny.UI.UIButton btnExit;
        private Sunny.UI.UIButton btnRemove;
        private Sunny.UI.UIButton btnView;
        private Sunny.UI.UILabel uiLabel5;
        private AntdUI.Table Tables;
        private UILabel uiLabel6;
        private UIComboBox CboWorkstations;
    }
}
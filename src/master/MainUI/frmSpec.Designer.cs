namespace MainUI
{
    partial class FrmSpec
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
            btnSelectRow = new UIButton();
            cboType = new UIComboBox();
            cboModel = new UIComboBox();
            uiLabel2 = new UILabel();
            uiLabel1 = new UILabel();
            Tables = new AntdUI.Table();
            uiLine1 = new UILine();
            SuspendLayout();
            // 
            // btnSelectRow
            // 
            btnSelectRow.Cursor = Cursors.Hand;
            btnSelectRow.DialogResult = DialogResult.OK;
            btnSelectRow.FillColor = Color.DodgerBlue;
            btnSelectRow.FillColor2 = Color.DodgerBlue;
            btnSelectRow.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            btnSelectRow.Location = new Point(651, 673);
            btnSelectRow.MinimumSize = new Size(1, 1);
            btnSelectRow.Name = "btnSelectRow";
            btnSelectRow.RectColor = Color.DodgerBlue;
            btnSelectRow.RectDisableColor = Color.DodgerBlue;
            btnSelectRow.Size = new Size(120, 40);
            btnSelectRow.TabIndex = 391;
            btnSelectRow.Text = "选择实行";
            btnSelectRow.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSelectRow.TipsForeColor = Color.Transparent;
            btnSelectRow.TipsText = "1";
            btnSelectRow.Click += btnSelectRow_Click;
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
            cboType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboType.Location = new Point(319, 48);
            cboType.Margin = new Padding(4, 5, 4, 5);
            cboType.MinimumSize = new Size(63, 0);
            cboType.Name = "cboType";
            cboType.Padding = new Padding(0, 0, 30, 2);
            cboType.Radius = 10;
            cboType.RectColor = Color.Gray;
            cboType.RectDisableColor = Color.Gray;
            cboType.RectSides = ToolStripStatusLabelBorderSides.None;
            cboType.Size = new Size(244, 29);
            cboType.SymbolSize = 24;
            cboType.TabIndex = 393;
            cboType.TextAlignment = ContentAlignment.MiddleLeft;
            cboType.Watermark = "请选择";
            cboType.SelectedIndexChanged += cboType_SelectedIndexChanged;
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
            cboModel.Location = new Point(301, 679);
            cboModel.Margin = new Padding(4, 5, 4, 5);
            cboModel.MinimumSize = new Size(63, 0);
            cboModel.Name = "cboModel";
            cboModel.Padding = new Padding(0, 0, 30, 2);
            cboModel.Radius = 10;
            cboModel.RectColor = Color.FromArgb(218, 220, 230);
            cboModel.RectDisableColor = Color.FromArgb(218, 220, 230);
            cboModel.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            cboModel.Size = new Size(172, 29);
            cboModel.SymbolSize = 24;
            cboModel.TabIndex = 395;
            cboModel.TextAlignment = ContentAlignment.MiddleLeft;
            cboModel.Visible = false;
            cboModel.Watermark = "请选择";
            cboModel.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            cboModel.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel2.Location = new Point(211, 681);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(102, 26);
            uiLabel2.TabIndex = 396;
            uiLabel2.Text = "产品型号：";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            uiLabel2.Visible = false;
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel1.Location = new Point(223, 50);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(102, 26);
            uiLabel1.TabIndex = 394;
            uiLabel1.Text = "产品类型：";
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
            Tables.Location = new Point(15, 109);
            Tables.Name = "Tables";
            Tables.RightToLeft = RightToLeft.No;
            Tables.RowHeight = 50;
            Tables.RowHeightHeader = 40;
            Tables.Size = new Size(756, 558);
            Tables.SwitchSize = 25;
            Tables.TabIndex = 411;
            Tables.CellClick += Tables_CellClick;
            Tables.CellDoubleClick += Tables_CellDoubleClick;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.White;
            uiLine1.LineColor2 = Color.White;
            uiLine1.Location = new Point(15, 74);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(756, 29);
            uiLine1.StartCap = UILineCap.Circle;
            uiLine1.TabIndex = 412;
            // 
            // frmSpec
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(786, 722);
            Controls.Add(cboType);
            Controls.Add(uiLine1);
            Controls.Add(Tables);
            Controls.Add(cboModel);
            Controls.Add(uiLabel2);
            Controls.Add(uiLabel1);
            Controls.Add(btnSelectRow);
            Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            ForeColor = Color.FromArgb(235, 227, 221);
            Margin = new Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSpec";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "产品选择";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ZoomScaleRect = new Rectangle(15, 15, 786, 678);
            Load += frmSpec_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Sunny.UI.UIButton btnSelectRow;
        private UIComboBox cboType;
        private UIComboBox cboModel;
        private UILabel uiLabel2;
        private UILabel uiLabel1;
        private AntdUI.Table Tables;
        private UILine uiLine1;
    }
}
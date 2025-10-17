namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_WriteCells
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
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            BtnDelete = new UISymbolButton();
            btnAddRow = new UISymbolButton();
            DataGridViewDefineVar = new UIDataGridView();
            ColVarName = new DataGridViewTextBoxColumn();
            ColVarType = new DataGridViewComboBoxColumn();
            ColVarText = new DataGridViewTextBoxColumn();
            uiGroupBox1 = new UIPanel();
            txtFilePath = new UITextBox();
            btnBrowse = new UISymbolButton();
            uiLabel1 = new UILabel();
            txtSheetName = new UITextBox();
            uiLabel2 = new UILabel();
            uiGroupBox2 = new UIPanel();
            uiLabel3 = new UILabel();
            uiLine2 = new UILine();
            uiLine1 = new UILine();
            btnSave = new UISymbolButton();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            uiGroupBox1.SuspendLayout();
            uiGroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // BtnDelete
            // 
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.FillColor = Color.FromArgb(245, 108, 108);
            BtnDelete.FillColor2 = Color.FromArgb(245, 108, 108);
            BtnDelete.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            BtnDelete.LightColor = Color.FromArgb(248, 248, 248);
            BtnDelete.Location = new Point(640, 370);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.RectColor = Color.FromArgb(245, 108, 108);
            BtnDelete.RectDisableColor = Color.FromArgb(245, 108, 108);
            BtnDelete.RectHoverColor = Color.FromArgb(230, 80, 80);
            BtnDelete.Size = new Size(115, 38);
            BtnDelete.Style = UIStyle.Custom;
            BtnDelete.Symbol = 61683;
            BtnDelete.SymbolSize = 28;
            BtnDelete.TabIndex = 3;
            BtnDelete.Text = "删除";
            BtnDelete.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnAddRow
            // 
            btnAddRow.Cursor = Cursors.Hand;
            btnAddRow.FillColor = Color.FromArgb(103, 194, 58);
            btnAddRow.FillColor2 = Color.FromArgb(103, 194, 58);
            btnAddRow.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnAddRow.LightColor = Color.FromArgb(248, 248, 248);
            btnAddRow.Location = new Point(504, 370);
            btnAddRow.MinimumSize = new Size(1, 1);
            btnAddRow.Name = "btnAddRow";
            btnAddRow.RectColor = Color.FromArgb(103, 194, 58);
            btnAddRow.RectDisableColor = Color.FromArgb(103, 194, 58);
            btnAddRow.RectHoverColor = Color.FromArgb(88, 165, 49);
            btnAddRow.Size = new Size(120, 38);
            btnAddRow.Style = UIStyle.Custom;
            btnAddRow.Symbol = 61694;
            btnAddRow.SymbolSize = 28;
            btnAddRow.TabIndex = 2;
            btnAddRow.Text = "添加";
            btnAddRow.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // DataGridViewDefineVar
            // 
            DataGridViewDefineVar.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.BorderStyle = BorderStyle.Fixed3D;
            DataGridViewDefineVar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle7.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            DataGridViewDefineVar.ColumnHeadersHeight = 36;
            DataGridViewDefineVar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColVarType, ColVarText });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("微软雅黑", 10F);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(180, 200, 230);
            dataGridViewCellStyle8.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            DataGridViewDefineVar.DefaultCellStyle = dataGridViewCellStyle8;
            DataGridViewDefineVar.EditMode = DataGridViewEditMode.EditOnEnter;
            DataGridViewDefineVar.EnableHeadersVisualStyles = false;
            DataGridViewDefineVar.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            DataGridViewDefineVar.GridColor = Color.FromArgb(200, 200, 200);
            DataGridViewDefineVar.Location = new Point(15, 40);
            DataGridViewDefineVar.MultiSelect = false;
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            DataGridViewDefineVar.RectColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle9.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(180, 200, 230);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            DataGridViewDefineVar.RowHeadersVisible = false;
            DataGridViewDefineVar.RowHeadersWidth = 51;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle10;
            DataGridViewDefineVar.RowTemplate.Height = 32;
            DataGridViewDefineVar.ScrollBarRectColor = Color.FromArgb(65, 100, 204);
            DataGridViewDefineVar.ScrollBarStyleInherited = false;
            DataGridViewDefineVar.SelectedIndex = -1;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDefineVar.Size = new Size(740, 321);
            DataGridViewDefineVar.StripeOddColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.Style = UIStyle.Custom;
            DataGridViewDefineVar.TabIndex = 1;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "单元格地址";
            ColVarName.Name = "ColVarName";
            ColVarName.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColVarName.Width = 150;
            // 
            // ColVarType
            // 
            ColVarType.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            ColVarType.HeaderText = "填写类型";
            ColVarType.Items.AddRange(new object[] { "自定义", "操作员", "产品类型", "产品型号", "产品图号", "当前时间", "自定义变量" });
            ColVarType.Name = "ColVarType";
            ColVarType.Resizable = DataGridViewTriState.True;
            ColVarType.SortMode = DataGridViewColumnSortMode.Automatic;
            ColVarType.Width = 180;
            // 
            // ColVarText
            // 
            ColVarText.HeaderText = "填写内容/变量名";
            ColVarText.Name = "ColVarText";
            ColVarText.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColVarText.Width = 390;
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.BackColor = Color.Transparent;
            uiGroupBox1.Controls.Add(txtFilePath);
            uiGroupBox1.Controls.Add(btnBrowse);
            uiGroupBox1.Controls.Add(uiLabel1);
            uiGroupBox1.Controls.Add(txtSheetName);
            uiGroupBox1.Controls.Add(uiLabel2);
            uiGroupBox1.FillColor = Color.White;
            uiGroupBox1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox1.Location = new Point(15, 76);
            uiGroupBox1.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox1.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox1.Size = new Size(770, 120);
            uiGroupBox1.Style = UIStyle.Custom;
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = null;
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // txtFilePath
            // 
            txtFilePath.ButtonSymbol = 61451;
            txtFilePath.Cursor = Cursors.IBeam;
            txtFilePath.Font = new Font("微软雅黑", 10F);
            txtFilePath.Location = new Point(100, 29);
            txtFilePath.Margin = new Padding(4, 5, 4, 5);
            txtFilePath.MinimumSize = new Size(1, 16);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Padding = new Padding(5);
            txtFilePath.RectColor = Color.FromArgb(65, 100, 204);
            txtFilePath.ShowText = false;
            txtFilePath.Size = new Size(530, 29);
            txtFilePath.Style = UIStyle.Custom;
            txtFilePath.TabIndex = 1;
            txtFilePath.TextAlignment = ContentAlignment.MiddleLeft;
            txtFilePath.Watermark = "请选择Excel文件路径...";
            // 
            // btnBrowse
            // 
            btnBrowse.Cursor = Cursors.Hand;
            btnBrowse.FillColor = Color.FromArgb(64, 158, 255);
            btnBrowse.FillColor2 = Color.FromArgb(64, 158, 255);
            btnBrowse.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnBrowse.Location = new Point(640, 26);
            btnBrowse.MinimumSize = new Size(1, 1);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.RectColor = Color.FromArgb(64, 158, 255);
            btnBrowse.Size = new Size(110, 35);
            btnBrowse.Style = UIStyle.Custom;
            btnBrowse.Symbol = 61461;
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "浏览";
            btnBrowse.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.Font = new Font("微软雅黑", 10F);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(15, 31);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(68, 20);
            uiLabel1.TabIndex = 0;
            uiLabel1.Text = "文件路径:";
            // 
            // txtSheetName
            // 
            txtSheetName.ButtonSymbol = 61451;
            txtSheetName.Cursor = Cursors.IBeam;
            txtSheetName.Font = new Font("微软雅黑", 10F);
            txtSheetName.Location = new Point(100, 72);
            txtSheetName.Margin = new Padding(4, 5, 4, 5);
            txtSheetName.MinimumSize = new Size(1, 16);
            txtSheetName.Name = "txtSheetName";
            txtSheetName.Padding = new Padding(5);
            txtSheetName.RectColor = Color.FromArgb(65, 100, 204);
            txtSheetName.ShowText = false;
            txtSheetName.Size = new Size(530, 29);
            txtSheetName.Style = UIStyle.Custom;
            txtSheetName.TabIndex = 4;
            txtSheetName.Text = "Sheet1";
            txtSheetName.TextAlignment = ContentAlignment.MiddleLeft;
            txtSheetName.Watermark = "输入工作表名称,留空使用第一个工作表";
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.Font = new Font("微软雅黑", 10F);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(15, 75);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(68, 20);
            uiLabel2.TabIndex = 3;
            uiLabel2.Text = "工作表名:";
            // 
            // uiGroupBox2
            // 
            uiGroupBox2.Controls.Add(btnSave);
            uiGroupBox2.Controls.Add(uiLabel3);
            uiGroupBox2.Controls.Add(btnAddRow);
            uiGroupBox2.Controls.Add(BtnDelete);
            uiGroupBox2.Controls.Add(DataGridViewDefineVar);
            uiGroupBox2.FillColor = Color.White;
            uiGroupBox2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox2.Location = new Point(15, 240);
            uiGroupBox2.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox2.MinimumSize = new Size(1, 1);
            uiGroupBox2.Name = "uiGroupBox2";
            uiGroupBox2.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox2.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox2.Size = new Size(770, 417);
            uiGroupBox2.Style = UIStyle.Custom;
            uiGroupBox2.TabIndex = 5;
            uiGroupBox2.Text = null;
            uiGroupBox2.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            uiLabel3.Font = new Font("微软雅黑", 9F);
            uiLabel3.ForeColor = Color.Gray;
            uiLabel3.Location = new Point(15, 17);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(450, 20);
            uiLabel3.TabIndex = 0;
            uiLabel3.Text = "💡 提示: 单元格地址格式如 A1, B2, C3...";
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 11F);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(15, 42);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(770, 29);
            uiLine2.TabIndex = 444;
            uiLine2.Text = "📄 文件配置";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.EndCap = UILineCap.Circle;
            uiLine1.Font = new Font("微软雅黑", 11F);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.White;
            uiLine1.Location = new Point(15, 207);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(770, 29);
            uiLine1.TabIndex = 445;
            uiLine1.Text = "📝 单元格写入配置";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.DodgerBlue;
            btnSave.FillColor2 = Color.DodgerBlue;
            btnSave.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnSave.LightColor = Color.FromArgb(248, 248, 248);
            btnSave.Location = new Point(368, 370);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectColor = Color.DodgerBlue;
            btnSave.RectDisableColor = Color.DodgerBlue;
            btnSave.RectHoverColor = Color.FromArgb(88, 165, 49);
            btnSave.Size = new Size(120, 38);
            btnSave.Style = UIStyle.Custom;
            btnSave.Symbol = 61639;
            btnSave.SymbolSize = 28;
            btnSave.TabIndex = 5;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // Form_WriteCells
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(800, 667);
            Controls.Add(uiLine1);
            Controls.Add(uiLine2);
            Controls.Add(uiGroupBox2);
            Controls.Add(uiGroupBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_WriteCells";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "写入单元格配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 600);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox1.PerformLayout();
            uiGroupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UISymbolButton BtnDelete;
        private Sunny.UI.UISymbolButton btnAddRow;
        private Sunny.UI.UIDataGridView DataGridViewDefineVar;
        private DataGridViewTextBoxColumn ColVarName;
        private DataGridViewComboBoxColumn ColVarType;
        private DataGridViewTextBoxColumn ColVarText;
        private Sunny.UI.UIPanel uiGroupBox1;
        private Sunny.UI.UITextBox txtFilePath;
        private Sunny.UI.UISymbolButton btnBrowse;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txtSheetName;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIPanel uiGroupBox2;
        private Sunny.UI.UILabel uiLabel3;
        private UILine uiLine2;
        private UILine uiLine1;
        private UISymbolButton btnSave;
    }
}
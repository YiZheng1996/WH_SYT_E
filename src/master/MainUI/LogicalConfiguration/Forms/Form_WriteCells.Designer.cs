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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            BtnDelete = new UISymbolButton();
            btnAddRow = new UISymbolButton();
            DataGridViewDefineVar = new UIDataGridView();
            ColVarName = new DataGridViewTextBoxColumn();
            ColVarType = new DataGridViewComboBoxColumn();
            ColVarText = new DataGridViewTextBoxColumn();
            uiGroupBox1 = new UIPanel();
            txtSheetName = new UITextBox();
            uiLabel2 = new UILabel();
            uiGroupBox2 = new UIPanel();
            btnSave = new UISymbolButton();
            uiLabel3 = new UILabel();
            uiLine2 = new UILine();
            uiLine1 = new UILine();
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
            BtnDelete.Location = new Point(640, 403);
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
            btnAddRow.Location = new Point(504, 403);
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
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.BorderStyle = BorderStyle.Fixed3D;
            DataGridViewDefineVar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewDefineVar.ColumnHeadersHeight = 36;
            DataGridViewDefineVar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColVarType, ColVarText });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(180, 200, 230);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewDefineVar.DefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewDefineVar.EditMode = DataGridViewEditMode.EditOnEnter;
            DataGridViewDefineVar.EnableHeadersVisualStyles = false;
            DataGridViewDefineVar.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            DataGridViewDefineVar.GridColor = Color.FromArgb(200, 200, 200);
            DataGridViewDefineVar.Location = new Point(15, 40);
            DataGridViewDefineVar.MultiSelect = false;
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            DataGridViewDefineVar.RectColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(180, 200, 230);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewDefineVar.RowHeadersVisible = false;
            DataGridViewDefineVar.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle5;
            DataGridViewDefineVar.RowTemplate.Height = 32;
            DataGridViewDefineVar.ScrollBarRectColor = Color.FromArgb(65, 100, 204);
            DataGridViewDefineVar.ScrollBarStyleInherited = false;
            DataGridViewDefineVar.SelectedIndex = -1;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDefineVar.Size = new Size(740, 353);
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
            uiGroupBox1.Size = new Size(770, 81);
            uiGroupBox1.Style = UIStyle.Custom;
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = null;
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // txtSheetName
            // 
            txtSheetName.ButtonSymbol = 61451;
            txtSheetName.Cursor = Cursors.IBeam;
            txtSheetName.Font = new Font("微软雅黑", 10F);
            txtSheetName.Location = new Point(121, 23);
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
            uiLabel2.Location = new Point(30, 26);
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
            uiGroupBox2.Location = new Point(15, 207);
            uiGroupBox2.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox2.MinimumSize = new Size(1, 1);
            uiGroupBox2.Name = "uiGroupBox2";
            uiGroupBox2.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox2.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox2.Size = new Size(770, 450);
            uiGroupBox2.Style = UIStyle.Custom;
            uiGroupBox2.TabIndex = 5;
            uiGroupBox2.Text = null;
            uiGroupBox2.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.DodgerBlue;
            btnSave.FillColor2 = Color.DodgerBlue;
            btnSave.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnSave.LightColor = Color.FromArgb(248, 248, 248);
            btnSave.Location = new Point(368, 403);
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
            uiLine1.Location = new Point(15, 172);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(770, 29);
            uiLine1.TabIndex = 445;
            uiLine1.Text = "📝 单元格写入配置";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
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
        private Sunny.UI.UITextBox txtSheetName;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIPanel uiGroupBox2;
        private Sunny.UI.UILabel uiLabel3;
        private UILine uiLine2;
        private UILine uiLine1;
        private UISymbolButton btnSave;
    }
}
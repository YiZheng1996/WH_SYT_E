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

            // 释放定时器
            _previewTimer?.Dispose();

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
            uiGroupBox1 = new UIPanel();
            txtSheetName = new UITextBox();
            uiLabel1 = new UILabel();
            uiGroupBox2 = new UIPanel();
            btnSave = new UISymbolButton();
            BtnDelete = new UISymbolButton();
            btnAddRow = new UISymbolButton();
            uiLabel2 = new UILabel();
            DataGridViewDefineVar = new UIDataGridView();
            ColVarName = new DataGridViewTextBoxColumn();
            ColVarType = new DataGridViewComboBoxColumn();
            ColVarText = new DataGridViewTextBoxColumn();
            uiGroupBox3 = new UIPanel();
            uiLabel3 = new UILabel();
            lblPreviewContent = new UILabel();
            uiLine1 = new UILine();
            uiLine2 = new UILine();
            uiLine3 = new UILine();
            uiGroupBox1.SuspendLayout();
            uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            uiGroupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(txtSheetName);
            uiGroupBox1.Controls.Add(uiLabel1);
            uiGroupBox1.FillColor = Color.White;
            uiGroupBox1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox1.Location = new Point(15, 78);
            uiGroupBox1.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox1.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox1.Size = new Size(770, 66);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = null;
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // txtSheetName
            // 
            txtSheetName.Cursor = Cursors.IBeam;
            txtSheetName.Font = new Font("微软雅黑", 10F);
            txtSheetName.Location = new Point(117, 19);
            txtSheetName.Margin = new Padding(4, 5, 4, 5);
            txtSheetName.MinimumSize = new Size(1, 16);
            txtSheetName.Name = "txtSheetName";
            txtSheetName.Padding = new Padding(5);
            txtSheetName.RectColor = Color.FromArgb(65, 100, 204);
            txtSheetName.ShowText = false;
            txtSheetName.Size = new Size(621, 29);
            txtSheetName.TabIndex = 1;
            txtSheetName.Text = "Sheet1";
            txtSheetName.TextAlignment = ContentAlignment.MiddleLeft;
            txtSheetName.Watermark = "输入工作表名称...";
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("微软雅黑", 10F);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(32, 18);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(80, 29);
            uiLabel1.TabIndex = 0;
            uiLabel1.Text = "工作表名:";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiGroupBox2
            // 
            uiGroupBox2.Controls.Add(btnSave);
            uiGroupBox2.Controls.Add(BtnDelete);
            uiGroupBox2.Controls.Add(btnAddRow);
            uiGroupBox2.Controls.Add(uiLabel2);
            uiGroupBox2.Controls.Add(DataGridViewDefineVar);
            uiGroupBox2.FillColor = Color.White;
            uiGroupBox2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox2.Location = new Point(15, 187);
            uiGroupBox2.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox2.MinimumSize = new Size(1, 1);
            uiGroupBox2.Name = "uiGroupBox2";
            uiGroupBox2.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox2.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox2.Size = new Size(770, 412);
            uiGroupBox2.TabIndex = 1;
            uiGroupBox2.Text = null;
            uiGroupBox2.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.FromArgb(65, 100, 204);
            btnSave.FillHoverColor = Color.FromArgb(88, 165, 49);
            btnSave.FillPressColor = Color.FromArgb(56, 106, 32);
            btnSave.FillSelectedColor = Color.FromArgb(56, 106, 32);
            btnSave.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnSave.Location = new Point(635, 365);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectColor = Color.FromArgb(65, 100, 204);
            btnSave.RectHoverColor = Color.FromArgb(88, 165, 49);
            btnSave.Size = new Size(120, 38);
            btnSave.Symbol = 61639;
            btnSave.SymbolSize = 28;
            btnSave.TabIndex = 4;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            // 
            // BtnDelete
            // 
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.FillColor = Color.FromArgb(230, 80, 80);
            BtnDelete.FillHoverColor = Color.FromArgb(232, 127, 128);
            BtnDelete.FillPressColor = Color.FromArgb(202, 87, 89);
            BtnDelete.FillSelectedColor = Color.FromArgb(202, 87, 89);
            BtnDelete.Font = new Font("微软雅黑", 10F);
            BtnDelete.Location = new Point(135, 365);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.RectColor = Color.FromArgb(230, 80, 80);
            BtnDelete.RectHoverColor = Color.FromArgb(232, 127, 128);
            BtnDelete.Size = new Size(110, 38);
            BtnDelete.Symbol = 61460;
            BtnDelete.TabIndex = 3;
            BtnDelete.Text = "删除";
            BtnDelete.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnAddRow
            // 
            btnAddRow.Cursor = Cursors.Hand;
            btnAddRow.FillColor = Color.FromArgb(110, 190, 40);
            btnAddRow.FillHoverColor = Color.FromArgb(139, 203, 83);
            btnAddRow.FillPressColor = Color.FromArgb(88, 152, 32);
            btnAddRow.FillSelectedColor = Color.FromArgb(88, 152, 32);
            btnAddRow.Font = new Font("微软雅黑", 10F);
            btnAddRow.Location = new Point(15, 365);
            btnAddRow.MinimumSize = new Size(1, 1);
            btnAddRow.Name = "btnAddRow";
            btnAddRow.RectColor = Color.FromArgb(110, 190, 40);
            btnAddRow.RectHoverColor = Color.FromArgb(139, 203, 83);
            btnAddRow.Size = new Size(110, 38);
            btnAddRow.Symbol = 61543;
            btnAddRow.TabIndex = 2;
            btnAddRow.Text = "添加";
            btnAddRow.TipsFont = new Font("微软雅黑", 9F);
            // 
            // uiLabel2
            // 
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 9F);
            uiLabel2.ForeColor = Color.Gray;
            uiLabel2.Location = new Point(15, 35);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(740, 29);
            uiLabel2.TabIndex = 1;
            uiLabel2.Text = "💡 提示:配置单元格地址、数据源类型和内容,支持变量和表达式";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DataGridViewDefineVar
            // 
            DataGridViewDefineVar.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(243, 249, 255);
            DataGridViewDefineVar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewDefineVar.ColumnHeadersHeight = 40;
            DataGridViewDefineVar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColVarType, ColVarText });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(155, 200, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewDefineVar.DefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewDefineVar.EnableHeadersVisualStyles = false;
            DataGridViewDefineVar.Font = new Font("微软雅黑", 10F);
            DataGridViewDefineVar.GridColor = Color.FromArgb(80, 160, 255);
            DataGridViewDefineVar.Location = new Point(15, 70);
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(243, 249, 255);
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewDefineVar.RowHeadersVisible = false;
            DataGridViewDefineVar.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle5;
            DataGridViewDefineVar.RowTemplate.Height = 35;
            DataGridViewDefineVar.SelectedIndex = -1;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDefineVar.Size = new Size(740, 289);
            DataGridViewDefineVar.StripeEvenColor = Color.Empty;
            DataGridViewDefineVar.TabIndex = 0;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "单元格地址";
            ColVarName.MinimumWidth = 6;
            ColVarName.Name = "ColVarName";
            ColVarName.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColVarName.Width = 120;
            // 
            // ColVarType
            // 
            ColVarType.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            ColVarType.HeaderText = "数据源类型";
            ColVarType.MinimumWidth = 6;
            ColVarType.Name = "ColVarType";
            ColVarType.Resizable = DataGridViewTriState.True;
            ColVarType.SortMode = DataGridViewColumnSortMode.Automatic;
            ColVarType.Width = 125;
            // 
            // ColVarText
            // 
            ColVarText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColVarText.HeaderText = "内容";
            ColVarText.MinimumWidth = 6;
            ColVarText.Name = "ColVarText";
            ColVarText.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // uiGroupBox3
            // 
            uiGroupBox3.Controls.Add(uiLabel3);
            uiGroupBox3.Controls.Add(lblPreviewContent);
            uiGroupBox3.FillColor = Color.White;
            uiGroupBox3.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox3.Location = new Point(15, 643);
            uiGroupBox3.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox3.MinimumSize = new Size(1, 1);
            uiGroupBox3.Name = "uiGroupBox3";
            uiGroupBox3.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox3.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox3.Size = new Size(770, 138);
            uiGroupBox3.TabIndex = 2;
            uiGroupBox3.Text = null;
            uiGroupBox3.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("微软雅黑", 10F);
            uiLabel3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel3.Location = new Point(15, 11);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(65, 20);
            uiLabel3.TabIndex = 0;
            uiLabel3.Text = "实时预览";
            uiLabel3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPreviewContent
            // 
            lblPreviewContent.BackColor = Color.Transparent;
            lblPreviewContent.Font = new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPreviewContent.ForeColor = Color.Gray;
            lblPreviewContent.Location = new Point(15, 35);
            lblPreviewContent.Name = "lblPreviewContent";
            lblPreviewContent.Padding = new Padding(15, 10, 15, 10);
            lblPreviewContent.Size = new Size(740, 76);
            lblPreviewContent.TabIndex = 1;
            lblPreviewContent.Text = "请选择一行查看预览";
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.EndCap = UILineCap.Circle;
            uiLine1.Font = new Font("微软雅黑", 11F);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.White;
            uiLine1.Location = new Point(15, 43);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(770, 29);
            uiLine1.TabIndex = 3;
            uiLine1.Text = "📄 Excel文件配置";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 11F);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(15, 150);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(770, 29);
            uiLine2.TabIndex = 4;
            uiLine2.Text = "📝 单元格写入配置";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine3
            // 
            uiLine3.BackColor = Color.Transparent;
            uiLine3.EndCap = UILineCap.Circle;
            uiLine3.Font = new Font("微软雅黑", 11F);
            uiLine3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine3.LineColor = Color.White;
            uiLine3.Location = new Point(15, 607);
            uiLine3.MinimumSize = new Size(1, 1);
            uiLine3.Name = "uiLine3";
            uiLine3.Size = new Size(770, 29);
            uiLine3.TabIndex = 5;
            uiLine3.Text = "📋 实时预览";
            uiLine3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_WriteCells
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(800, 789);
            Controls.Add(uiLine3);
            Controls.Add(uiLine2);
            Controls.Add(uiLine1);
            Controls.Add(uiGroupBox3);
            Controls.Add(uiGroupBox2);
            Controls.Add(uiGroupBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_WriteCells";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "✍️ 写入报表单元格";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 710);
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            uiGroupBox3.ResumeLayout(false);
            uiGroupBox3.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiGroupBox1;
        private Sunny.UI.UITextBox txtSheetName;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIPanel uiGroupBox2;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIDataGridView DataGridViewDefineVar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVarName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColVarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVarText;
        private Sunny.UI.UISymbolButton btnAddRow;
        private Sunny.UI.UISymbolButton BtnDelete;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UIPanel uiGroupBox3;
        private Sunny.UI.UILabel lblPreviewContent;
        private Sunny.UI.UILabel uiLabel3;
        private UILine uiLine1;
        private UILine uiLine2;
        private UILine uiLine3;
    }
}
namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_ReadCells
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
            uiGroupBox1 = new UIPanel();
            txtSheetName = new UITextBox();
            uiLabel2 = new UILabel();
            uiGroupBox2 = new UIPanel();
            btnCancel = new UISymbolButton();
            btnSave = new UISymbolButton();
            btnPreview = new UISymbolButton();
            uiLabel3 = new UILabel();
            btnAddRow = new UISymbolButton();
            BtnDelete = new UISymbolButton();
            DataGridViewDefineVar = new UIDataGridView();
            ColCellAddress = new DataGridViewTextBoxColumn();
            ColSaveToVar = new DataGridViewComboBoxColumn();
            ColDataType = new DataGridViewComboBoxColumn();
            ColDefaultValue = new DataGridViewTextBoxColumn();
            ColPreview = new DataGridViewTextBoxColumn();
            uiLine2 = new UILine();
            uiLine1 = new UILine();
            uiGroupBox1.SuspendLayout();
            uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(txtSheetName);
            uiGroupBox1.Controls.Add(uiLabel2);
            uiGroupBox1.FillColor = Color.White;
            uiGroupBox1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox1.Location = new Point(15, 78);
            uiGroupBox1.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox1.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox1.Size = new Size(820, 83);
            uiGroupBox1.TabIndex = 5;
            uiGroupBox1.Text = null;
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // txtSheetName
            // 
            txtSheetName.Cursor = Cursors.IBeam;
            txtSheetName.Font = new Font("微软雅黑", 10F);
            txtSheetName.Location = new Point(120, 27);
            txtSheetName.Margin = new Padding(4, 5, 4, 5);
            txtSheetName.MinimumSize = new Size(1, 16);
            txtSheetName.Name = "txtSheetName";
            txtSheetName.Padding = new Padding(5);
            txtSheetName.RectColor = Color.FromArgb(65, 100, 204);
            txtSheetName.ShowText = false;
            txtSheetName.Size = new Size(621, 29);
            txtSheetName.TabIndex = 4;
            txtSheetName.Text = "Sheet1";
            txtSheetName.TextAlignment = ContentAlignment.MiddleLeft;
            txtSheetName.Watermark = "输入工作表名称,留空使用第一个工作表";
            // 
            // uiLabel2
            // 
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 10F);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(35, 26);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(80, 29);
            uiLabel2.TabIndex = 3;
            uiLabel2.Text = "工作表名:";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiGroupBox2
            // 
            uiGroupBox2.Controls.Add(btnCancel);
            uiGroupBox2.Controls.Add(btnSave);
            uiGroupBox2.Controls.Add(btnPreview);
            uiGroupBox2.Controls.Add(uiLabel3);
            uiGroupBox2.Controls.Add(btnAddRow);
            uiGroupBox2.Controls.Add(BtnDelete);
            uiGroupBox2.Controls.Add(DataGridViewDefineVar);
            uiGroupBox2.FillColor = Color.White;
            uiGroupBox2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox2.Location = new Point(15, 205);
            uiGroupBox2.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox2.MinimumSize = new Size(1, 1);
            uiGroupBox2.Name = "uiGroupBox2";
            uiGroupBox2.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox2.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox2.Size = new Size(820, 490);
            uiGroupBox2.TabIndex = 6;
            uiGroupBox2.Text = null;
            uiGroupBox2.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(230, 80, 80);
            btnCancel.FillHoverColor = Color.FromArgb(232, 127, 128);
            btnCancel.FillPressColor = Color.FromArgb(202, 87, 89);
            btnCancel.FillSelectedColor = Color.FromArgb(202, 87, 89);
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(675, 439);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(230, 80, 80);
            btnCancel.RectHoverColor = Color.FromArgb(232, 127, 128);
            btnCancel.Size = new Size(130, 38);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 6;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.FromArgb(65, 100, 204);
            btnSave.FillHoverColor = Color.FromArgb(88, 165, 49);
            btnSave.FillPressColor = Color.FromArgb(56, 106, 32);
            btnSave.FillSelectedColor = Color.FromArgb(56, 106, 32);
            btnSave.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnSave.Location = new Point(530, 439);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectColor = Color.FromArgb(65, 100, 204);
            btnSave.RectHoverColor = Color.FromArgb(88, 165, 49);
            btnSave.Size = new Size(130, 38);
            btnSave.Symbol = 61639;
            btnSave.SymbolSize = 28;
            btnSave.TabIndex = 5;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnPreview
            // 
            btnPreview.Cursor = Cursors.Hand;
            btnPreview.FillColor = Color.FromArgb(220, 155, 40);
            btnPreview.FillHoverColor = Color.FromArgb(227, 175, 83);
            btnPreview.FillPressColor = Color.FromArgb(176, 124, 32);
            btnPreview.FillSelectedColor = Color.FromArgb(176, 124, 32);
            btnPreview.Font = new Font("微软雅黑", 9F);
            btnPreview.Location = new Point(275, 439);
            btnPreview.MinimumSize = new Size(1, 1);
            btnPreview.Name = "btnPreview";
            btnPreview.RectColor = Color.FromArgb(220, 155, 40);
            btnPreview.RectHoverColor = Color.FromArgb(227, 175, 83);
            btnPreview.Size = new Size(110, 38);
            btnPreview.Symbol = 61550;
            btnPreview.TabIndex = 4;
            btnPreview.Text = "预览读取";
            btnPreview.TipsFont = new Font("微软雅黑", 9F);
            // 
            // uiLabel3
            // 
            uiLabel3.Font = new Font("微软雅黑", 9F);
            uiLabel3.ForeColor = Color.Gray;
            uiLabel3.Location = new Point(15, 19);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(790, 20);
            uiLabel3.TabIndex = 0;
            uiLabel3.Text = "💡 提示: 单元格地址格式如 A1, B2, C3... 读取的值将保存到指定的变量中";
            uiLabel3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnAddRow
            // 
            btnAddRow.Cursor = Cursors.Hand;
            btnAddRow.FillColor = Color.FromArgb(88, 165, 49);
            btnAddRow.FillHoverColor = Color.FromArgb(111, 191, 77);
            btnAddRow.FillPressColor = Color.FromArgb(56, 106, 32);
            btnAddRow.FillSelectedColor = Color.FromArgb(56, 106, 32);
            btnAddRow.Font = new Font("微软雅黑", 9F);
            btnAddRow.Location = new Point(15, 439);
            btnAddRow.MinimumSize = new Size(1, 1);
            btnAddRow.Name = "btnAddRow";
            btnAddRow.RectColor = Color.FromArgb(88, 165, 49);
            btnAddRow.RectHoverColor = Color.FromArgb(111, 191, 77);
            btnAddRow.Size = new Size(110, 38);
            btnAddRow.Symbol = 61543;
            btnAddRow.TabIndex = 2;
            btnAddRow.Text = "添加行";
            btnAddRow.TipsFont = new Font("微软雅黑", 9F);
            // 
            // BtnDelete
            // 
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.FillColor = Color.FromArgb(230, 80, 80);
            BtnDelete.FillHoverColor = Color.FromArgb(232, 127, 128);
            BtnDelete.FillPressColor = Color.FromArgb(202, 87, 89);
            BtnDelete.FillSelectedColor = Color.FromArgb(202, 87, 89);
            BtnDelete.Font = new Font("微软雅黑", 9F);
            BtnDelete.Location = new Point(145, 439);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.RectColor = Color.FromArgb(230, 80, 80);
            BtnDelete.RectHoverColor = Color.FromArgb(232, 127, 128);
            BtnDelete.Size = new Size(110, 38);
            BtnDelete.Symbol = 61544;
            BtnDelete.TabIndex = 3;
            BtnDelete.Text = "删除行";
            BtnDelete.TipsFont = new Font("微软雅黑", 9F);
            // 
            // DataGridViewDefineVar
            // 
            DataGridViewDefineVar.AllowUserToAddRows = false;
            DataGridViewDefineVar.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(243, 249, 255);
            DataGridViewDefineVar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.BorderStyle = BorderStyle.Fixed3D;
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
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColCellAddress, ColSaveToVar, ColDataType, ColDefaultValue, ColPreview });
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
            dataGridViewCellStyle5.Font = new Font("微软雅黑", 9.75F);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle5;
            DataGridViewDefineVar.RowTemplate.Height = 35;
            DataGridViewDefineVar.SelectedIndex = -1;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDefineVar.Size = new Size(790, 330);
            DataGridViewDefineVar.StripeEvenColor = Color.Empty;
            DataGridViewDefineVar.TabIndex = 1;
            // 
            // ColCellAddress
            // 
            ColCellAddress.HeaderText = "单元格地址";
            ColCellAddress.MinimumWidth = 100;
            ColCellAddress.Name = "ColCellAddress";
            ColCellAddress.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColCellAddress.Width = 120;
            // 
            // ColSaveToVar
            // 
            ColSaveToVar.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            ColSaveToVar.HeaderText = "保存到变量";
            ColSaveToVar.MinimumWidth = 150;
            ColSaveToVar.Name = "ColSaveToVar";
            ColSaveToVar.Resizable = DataGridViewTriState.True;
            ColSaveToVar.SortMode = DataGridViewColumnSortMode.Automatic;
            ColSaveToVar.Width = 200;
            // 
            // ColDataType
            // 
            ColDataType.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            ColDataType.HeaderText = "数据类型";
            ColDataType.Items.AddRange(new object[] { "字符串", "整数", "小数", "布尔", "日期时间" });
            ColDataType.MinimumWidth = 100;
            ColDataType.Name = "ColDataType";
            ColDataType.Resizable = DataGridViewTriState.True;
            ColDataType.SortMode = DataGridViewColumnSortMode.Automatic;
            ColDataType.Width = 120;
            // 
            // ColDefaultValue
            // 
            ColDefaultValue.HeaderText = "默认值";
            ColDefaultValue.MinimumWidth = 120;
            ColDefaultValue.Name = "ColDefaultValue";
            ColDefaultValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColDefaultValue.Width = 170;
            // 
            // ColPreview
            // 
            ColPreview.HeaderText = "预览值";
            ColPreview.MinimumWidth = 120;
            ColPreview.Name = "ColPreview";
            ColPreview.Width = 170;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 11F);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(15, 169);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(820, 29);
            uiLine2.TabIndex = 445;
            uiLine2.Text = "📖 单元格读取配置";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
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
            uiLine1.Size = new Size(820, 29);
            uiLine1.TabIndex = 446;
            uiLine1.Text = "📄 Excel文件配置";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_ReadCells
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(850, 710);
            Controls.Add(uiLine1);
            Controls.Add(uiLine2);
            Controls.Add(uiGroupBox2);
            Controls.Add(uiGroupBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_ReadCells";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "📖 读取报表单元格";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 850, 710);
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIPanel uiGroupBox1;
        private Sunny.UI.UIPanel uiGroupBox2;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UITextBox txtSheetName;
        private Sunny.UI.UIDataGridView DataGridViewDefineVar;
        private Sunny.UI.UISymbolButton btnAddRow;
        private Sunny.UI.UISymbolButton BtnDelete;
        private Sunny.UI.UISymbolButton btnPreview;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UISymbolButton btnCancel;
        private UILine uiLine2;
        private UILine uiLine1;
        private DataGridViewTextBoxColumn ColCellAddress;
        private DataGridViewComboBoxColumn ColSaveToVar;
        private DataGridViewComboBoxColumn ColDataType;
        private DataGridViewTextBoxColumn ColDefaultValue;
        private DataGridViewTextBoxColumn ColPreview;
    }
}
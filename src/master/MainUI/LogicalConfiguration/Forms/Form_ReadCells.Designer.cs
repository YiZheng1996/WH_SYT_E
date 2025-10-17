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
            uiGroupBox1 = new UIGroupBox();
            uiLabel2 = new AntdUI.Label();
            txtSheetName = new AntdUI.Input();
            uiLine1 = new AntdUI.Divider();
            uiGroupBox2 = new UIGroupBox();
            btnPreview = new AntdUI.Button();
            btnSave = new AntdUI.Button();
            btnCancel = new AntdUI.Button();
            uiLabel3 = new AntdUI.Label();
            btnAddRow = new AntdUI.Button();
            BtnDelete = new AntdUI.Button();
            DataGridViewDefineVar = new UIDataGridView();
            uiLine2 = new AntdUI.Divider();
            uiGroupBox1.SuspendLayout();
            uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(uiLabel2);
            uiGroupBox1.Controls.Add(txtSheetName);
            uiGroupBox1.Controls.Add(uiLine1);
            uiGroupBox1.FillColor = Color.White;
            uiGroupBox1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiGroupBox1.Location = new Point(15, 35);
            uiGroupBox1.Margin = new Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new Padding(0, 32, 0, 0);
            uiGroupBox1.RectColor = Color.FromArgb(65, 100, 204);
            uiGroupBox1.Size = new Size(820, 180);
            uiGroupBox1.TabIndex = 1;
            uiGroupBox1.Text = null;
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSizeMode = AntdUI.TAutoSize.Auto;
            uiLabel2.Font = new Font("微软雅黑", 10F);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(15, 100);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(57, 18);
            uiLabel2.TabIndex = 3;
            uiLabel2.Text = "工作表名:";
            // 
            // txtSheetName
            // 
            txtSheetName.Font = new Font("微软雅黑", 10F);
            txtSheetName.Location = new Point(100, 97);
            txtSheetName.Margin = new Padding(4, 5, 4, 5);
            txtSheetName.MinimumSize = new Size(1, 16);
            txtSheetName.Name = "txtSheetName";
            txtSheetName.Padding = new Padding(5);
            txtSheetName.PlaceholderText = "输入工作表名称,留空使用第一个工作表";
            txtSheetName.Size = new Size(540, 29);
            txtSheetName.TabIndex = 4;
            txtSheetName.Text = "Sheet1";
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.Location = new Point(15, 15);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(790, 29);
            uiLine1.TabIndex = 444;
            uiLine1.Text = "📄 Excel文件配置";
            // 
            // uiGroupBox2
            // 
            uiGroupBox2.Controls.Add(btnPreview);
            uiGroupBox2.Controls.Add(btnSave);
            uiGroupBox2.Controls.Add(btnCancel);
            uiGroupBox2.Controls.Add(uiLabel3);
            uiGroupBox2.Controls.Add(btnAddRow);
            uiGroupBox2.Controls.Add(BtnDelete);
            uiGroupBox2.Controls.Add(DataGridViewDefineVar);
            uiGroupBox2.Controls.Add(uiLine2);
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
            // btnPreview
            // 
            btnPreview.Font = new Font("微软雅黑", 9F);
            btnPreview.Location = new Point(275, 415);
            btnPreview.MinimumSize = new Size(1, 1);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(140, 32);
            btnPreview.TabIndex = 4;
            btnPreview.Text = "👁️ 预览读取";
            btnPreview.Type = AntdUI.TTypeMini.Warn;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnSave.Location = new Point(530, 455);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(130, 38);
            btnSave.TabIndex = 5;
            btnSave.Text = "💾 保存";
            btnSave.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(675, 455);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 38);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "❌ 取消";
            // 
            // uiLabel3
            // 
            uiLabel3.Font = new Font("微软雅黑", 9F);
            uiLabel3.ForeColor = Color.Gray;
            uiLabel3.Location = new Point(15, 45);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(790, 20);
            uiLabel3.TabIndex = 0;
            uiLabel3.Text = "💡 提示: 单元格地址格式如 A1, B2, C3... 读取的值将保存到指定的变量中";
            // 
            // btnAddRow
            // 
            btnAddRow.Font = new Font("微软雅黑", 9F);
            btnAddRow.Location = new Point(15, 415);
            btnAddRow.MinimumSize = new Size(1, 1);
            btnAddRow.Name = "btnAddRow";
            btnAddRow.Size = new Size(120, 32);
            btnAddRow.TabIndex = 2;
            btnAddRow.Text = "➕ 添加行";
            btnAddRow.Type = AntdUI.TTypeMini.Success;
            // 
            // BtnDelete
            // 
            BtnDelete.Font = new Font("微软雅黑", 9F);
            BtnDelete.Location = new Point(145, 415);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(120, 32);
            BtnDelete.TabIndex = 3;
            BtnDelete.Text = "🗑️ 删除行";
            BtnDelete.Type = AntdUI.TTypeMini.Error;
            // 
            // DataGridViewDefineVar
            // 
            DataGridViewDefineVar.AllowUserToAddRows = false;
            DataGridViewDefineVar.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(243, 249, 255);
            DataGridViewDefineVar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.BorderStyle = BorderStyle.None;
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
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 236, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewDefineVar.DefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewDefineVar.EnableHeadersVisualStyles = false;
            DataGridViewDefineVar.Font = new Font("宋体", 12F);
            DataGridViewDefineVar.GridColor = Color.FromArgb(224, 224, 224);
            DataGridViewDefineVar.Location = new Point(15, 75);
            DataGridViewDefineVar.MultiSelect = false;
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            DataGridViewDefineVar.RectColor = Color.FromArgb(128, 128, 255);
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(243, 249, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewDefineVar.RowHeadersVisible = false;
            DataGridViewDefineVar.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("微软雅黑", 9F);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle5;
            DataGridViewDefineVar.RowTemplate.Height = 35;
            DataGridViewDefineVar.SelectedIndex = -1;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDefineVar.Size = new Size(790, 330);
            DataGridViewDefineVar.TabIndex = 1;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.Location = new Point(15, 10);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(790, 29);
            uiLine2.TabIndex = 445;
            uiLine2.Text = "📖 单元格读取配置";
            // 
            // Form_ReadCells
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(850, 710);
            Controls.Add(uiGroupBox2);
            Controls.Add(uiGroupBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_ReadCells";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "📖 读取报表单元格";
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 850, 710);
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox1.PerformLayout();
            uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private AntdUI.Label uiLabel2;
        private AntdUI.Label uiLabel3;
        private AntdUI.Input txtSheetName;
        private Sunny.UI.UIDataGridView DataGridViewDefineVar;
        private AntdUI.Button btnAddRow;
        private AntdUI.Button BtnDelete;
        private AntdUI.Button btnPreview;
        private AntdUI.Button btnSave;
        private AntdUI.Button btnCancel;
        private AntdUI.Divider uiLine1;
        private AntdUI.Divider uiLine2;
    }
}
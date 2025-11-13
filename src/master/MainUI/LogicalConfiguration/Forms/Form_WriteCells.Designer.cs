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
            txtSheetName = new UITextBox();
            lblSheetName = new Label();
            DataGridViewDefineVar = new DataGridView();
            ColVarName = new DataGridViewTextBoxColumn();
            ColVarType = new DataGridViewComboBoxColumn();
            ColVarText = new DataGridViewTextBoxColumn();
            btnAddRow = new UIButton();
            BtnDelete = new UIButton();
            btnSave = new UIButton();
            panelPreview = new Panel();
            lblPreviewContent = new Label();
            lblPreviewTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            panelPreview.SuspendLayout();
            SuspendLayout();
            // 
            // txtSheetName
            // 
            txtSheetName.Cursor = Cursors.IBeam;
            txtSheetName.Font = new Font("微软雅黑", 10F);
            txtSheetName.Location = new Point(100, 42);
            txtSheetName.Margin = new Padding(4, 5, 4, 5);
            txtSheetName.MinimumSize = new Size(1, 16);
            txtSheetName.Name = "txtSheetName";
            txtSheetName.Padding = new Padding(5);
            txtSheetName.ShowText = false;
            txtSheetName.Size = new Size(250, 30);
            txtSheetName.TabIndex = 0;
            txtSheetName.Text = "Sheet1";
            txtSheetName.TextAlignment = ContentAlignment.MiddleLeft;
            txtSheetName.Watermark = "输入工作表名称...";
            // 
            // lblSheetName
            // 
            lblSheetName.AutoSize = true;
            lblSheetName.Font = new Font("微软雅黑", 10F);
            lblSheetName.Location = new Point(20, 42);
            lblSheetName.Name = "lblSheetName";
            lblSheetName.Size = new Size(68, 20);
            lblSheetName.TabIndex = 1;
            lblSheetName.Text = "工作表名:";
            // 
            // DataGridViewDefineVar
            // 
            DataGridViewDefineVar.AllowUserToResizeRows = false;
            DataGridViewDefineVar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColVarType, ColVarText });
            DataGridViewDefineVar.Location = new Point(20, 78);
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            DataGridViewDefineVar.RowHeadersWidth = 51;
            DataGridViewDefineVar.RowTemplate.Height = 27;
            DataGridViewDefineVar.Size = new Size(760, 390);
            DataGridViewDefineVar.TabIndex = 2;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "单元格地址";
            ColVarName.MinimumWidth = 6;
            ColVarName.Name = "ColVarName";
            ColVarName.Width = 120;
            // 
            // ColVarType
            // 
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
            // 
            // btnAddRow
            // 
            btnAddRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddRow.Cursor = Cursors.Hand;
            btnAddRow.Font = new Font("微软雅黑", 10F);
            btnAddRow.Location = new Point(20, 578);
            btnAddRow.MinimumSize = new Size(1, 1);
            btnAddRow.Name = "btnAddRow";
            btnAddRow.Size = new Size(100, 35);
            btnAddRow.TabIndex = 6;
            btnAddRow.Text = "➕ 添加";
            btnAddRow.TipsFont = new Font("微软雅黑", 9F);
            // 
            // BtnDelete
            // 
            BtnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.Font = new Font("微软雅黑", 10F);
            BtnDelete.Location = new Point(130, 578);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(100, 35);
            BtnDelete.TabIndex = 7;
            BtnDelete.Text = "🗑️ 删除";
            BtnDelete.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Cursor = Cursors.Hand;
            btnSave.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnSave.Location = new Point(680, 578);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 8;
            btnSave.Text = "💾 保存";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            // 
            // panelPreview
            // 
            panelPreview.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelPreview.BackColor = Color.FromArgb(245, 250, 255);
            panelPreview.BorderStyle = BorderStyle.FixedSingle;
            panelPreview.Controls.Add(lblPreviewContent);
            panelPreview.Controls.Add(lblPreviewTitle);
            panelPreview.Location = new Point(20, 473);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(760, 100);
            panelPreview.TabIndex = 9;
            // 
            // lblPreviewContent
            // 
            lblPreviewContent.Dock = DockStyle.Fill;
            lblPreviewContent.Font = new Font("Consolas", 10F);
            lblPreviewContent.ForeColor = Color.Gray;
            lblPreviewContent.Location = new Point(0, 30);
            lblPreviewContent.Name = "lblPreviewContent";
            lblPreviewContent.Padding = new Padding(15, 10, 15, 10);
            lblPreviewContent.Size = new Size(758, 68);
            lblPreviewContent.TabIndex = 1;
            lblPreviewContent.Text = "请选择一行查看预览";
            // 
            // lblPreviewTitle
            // 
            lblPreviewTitle.BackColor = Color.FromArgb(80, 160, 255);
            lblPreviewTitle.Dock = DockStyle.Top;
            lblPreviewTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblPreviewTitle.ForeColor = Color.White;
            lblPreviewTitle.Location = new Point(0, 0);
            lblPreviewTitle.Name = "lblPreviewTitle";
            lblPreviewTitle.Padding = new Padding(10, 5, 10, 5);
            lblPreviewTitle.Size = new Size(758, 30);
            lblPreviewTitle.TabIndex = 0;
            lblPreviewTitle.Text = "📋 实时预览";
            lblPreviewTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_WriteCells
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 620);
            Controls.Add(panelPreview);
            Controls.Add(btnSave);
            Controls.Add(BtnDelete);
            Controls.Add(btnAddRow);
            Controls.Add(DataGridViewDefineVar);
            Controls.Add(lblSheetName);
            Controls.Add(txtSheetName);
            Name = "Form_WriteCells";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = " 写入单元格配置 - 增强版";
            ZoomScaleRect = new Rectangle(15, 15, 800, 530);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            panelPreview.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Sunny.UI.UITextBox txtSheetName;
        private System.Windows.Forms.Label lblSheetName;
        private System.Windows.Forms.DataGridView DataGridViewDefineVar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVarName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColVarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVarText;
        private Sunny.UI.UIButton btnAddRow;
        private Sunny.UI.UIButton BtnDelete;
        private Sunny.UI.UIButton btnSave;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Label lblPreviewTitle;
        private System.Windows.Forms.Label lblPreviewContent;
    }
}

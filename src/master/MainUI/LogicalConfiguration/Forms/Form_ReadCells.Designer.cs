namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
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
            DataGridViewDefineVar = new UIDataGridView();
            ColVarName = new DataGridViewTextBoxColumn();
            ColVarType = new DataGridViewComboBoxColumn();
            ColVarText = new DataGridViewTextBoxColumn();
            BtnDelete = new UISymbolButton();
            uiSymbolButton1 = new UISymbolButton();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            SuspendLayout();
            // 
            // DataGridViewDefineVar
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewDefineVar.ColumnHeadersHeight = 35;
            DataGridViewDefineVar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColVarType, ColVarText });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("宋体", 13F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewDefineVar.DefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewDefineVar.EnableHeadersVisualStyles = false;
            DataGridViewDefineVar.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            DataGridViewDefineVar.GridColor = Color.FromArgb(64, 64, 64);
            DataGridViewDefineVar.Location = new Point(21, 49);
            DataGridViewDefineVar.MultiSelect = false;
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            DataGridViewDefineVar.RectColor = Color.White;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(248, 248, 248);
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 13F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewDefineVar.RowHeadersWidth = 28;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("微软雅黑", 13F);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle5;
            DataGridViewDefineVar.RowTemplate.Height = 30;
            DataGridViewDefineVar.SelectedIndex = -1;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DataGridViewDefineVar.Size = new Size(817, 445);
            DataGridViewDefineVar.StripeOddColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.Style = UIStyle.Custom;
            DataGridViewDefineVar.TabIndex = 12;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "单元格名称";
            ColVarName.Name = "ColVarName";
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
            ColVarType.Width = 150;
            // 
            // ColVarText
            // 
            ColVarText.HeaderText = "自定义填写内容";
            ColVarText.Name = "ColVarText";
            ColVarText.Width = 260;
            // 
            // BtnDelete
            // 
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.FillColor = Color.DodgerBlue;
            BtnDelete.FillColor2 = Color.DodgerBlue;
            BtnDelete.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnDelete.LightColor = Color.FromArgb(248, 248, 248);
            BtnDelete.Location = new Point(246, 506);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.RectColor = Color.DodgerBlue;
            BtnDelete.RectDisableColor = Color.DodgerBlue;
            BtnDelete.RectHoverColor = Color.FromArgb(64, 128, 204);
            BtnDelete.Size = new Size(132, 39);
            BtnDelete.Style = UIStyle.Custom;
            BtnDelete.Symbol = 561695;
            BtnDelete.SymbolSize = 32;
            BtnDelete.TabIndex = 15;
            BtnDelete.Text = "删除";
            BtnDelete.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // uiSymbolButton1
            // 
            uiSymbolButton1.Cursor = Cursors.Hand;
            uiSymbolButton1.FillColor = Color.DodgerBlue;
            uiSymbolButton1.FillColor2 = Color.DodgerBlue;
            uiSymbolButton1.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiSymbolButton1.LightColor = Color.FromArgb(248, 248, 248);
            uiSymbolButton1.Location = new Point(480, 506);
            uiSymbolButton1.MinimumSize = new Size(1, 1);
            uiSymbolButton1.Name = "uiSymbolButton1";
            uiSymbolButton1.RectColor = Color.DodgerBlue;
            uiSymbolButton1.RectDisableColor = Color.DodgerBlue;
            uiSymbolButton1.Size = new Size(132, 39);
            uiSymbolButton1.Style = UIStyle.Custom;
            uiSymbolButton1.Symbol = 61639;
            uiSymbolButton1.SymbolSize = 32;
            uiSymbolButton1.TabIndex = 14;
            uiSymbolButton1.Text = "保存";
            uiSymbolButton1.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // Form_ReadCells
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(859, 556);
            Controls.Add(BtnDelete);
            Controls.Add(uiSymbolButton1);
            Controls.Add(DataGridViewDefineVar);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_ReadCells";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "读取单元格";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private UIDataGridView DataGridViewDefineVar;
        private DataGridViewTextBoxColumn ColVarName;
        private DataGridViewComboBoxColumn ColVarType;
        private DataGridViewTextBoxColumn ColVarText;
        private UISymbolButton BtnDelete;
        private UISymbolButton uiSymbolButton1;
    }
}
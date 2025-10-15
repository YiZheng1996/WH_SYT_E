namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_Condition
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
            ColOperator = new DataGridViewComboBoxColumn();
            ColValue = new DataGridViewTextBoxColumn();
            ColTrueStepIndex = new DataGridViewTextBoxColumn();
            ColFalseStepIndex = new DataGridViewTextBoxColumn();
            BtnDelete = new UISymbolButton();
            BtnSave = new UISymbolButton();
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
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColOperator, ColValue, ColTrueStepIndex, ColFalseStepIndex });
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
            DataGridViewDefineVar.Size = new Size(918, 527);
            DataGridViewDefineVar.StripeOddColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.Style = UIStyle.Custom;
            DataGridViewDefineVar.TabIndex = 6;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "变量名称";
            ColVarName.Name = "ColVarName";
            ColVarName.Width = 150;
            // 
            // ColOperator
            // 
            ColOperator.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            ColOperator.HeaderText = "比较操作符";
            ColOperator.Items.AddRange(new object[] { "==", ">", "<", "≤", "≥" });
            ColOperator.Name = "ColOperator";
            ColOperator.Resizable = DataGridViewTriState.True;
            ColOperator.SortMode = DataGridViewColumnSortMode.Automatic;
            ColOperator.Width = 150;
            // 
            // ColValue
            // 
            ColValue.HeaderText = "比较值";
            ColValue.Name = "ColValue";
            ColValue.Width = 130;
            // 
            // ColTrueStepIndex
            // 
            ColTrueStepIndex.HeaderText = "True步骤号";
            ColTrueStepIndex.Name = "ColTrueStepIndex";
            ColTrueStepIndex.Width = 130;
            // 
            // ColFalseStepIndex
            // 
            ColFalseStepIndex.HeaderText = "False步骤号";
            ColFalseStepIndex.Name = "ColFalseStepIndex";
            ColFalseStepIndex.Width = 130;
            // 
            // BtnDelete
            // 
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.FillColor = Color.DodgerBlue;
            BtnDelete.FillColor2 = Color.DodgerBlue;
            BtnDelete.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnDelete.LightColor = Color.FromArgb(248, 248, 248);
            BtnDelete.Location = new Point(297, 588);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.RectColor = Color.DodgerBlue;
            BtnDelete.RectDisableColor = Color.DodgerBlue;
            BtnDelete.RectHoverColor = Color.FromArgb(64, 128, 204);
            BtnDelete.Size = new Size(132, 39);
            BtnDelete.Style = UIStyle.Custom;
            BtnDelete.Symbol = 561695;
            BtnDelete.SymbolSize = 32;
            BtnDelete.TabIndex = 9;
            BtnDelete.Text = "删除";
            BtnDelete.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnDelete.Click += BtnDelete_Click;
            // 
            // BtnSave
            // 
            BtnSave.Cursor = Cursors.Hand;
            BtnSave.FillColor = Color.DodgerBlue;
            BtnSave.FillColor2 = Color.DodgerBlue;
            BtnSave.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnSave.LightColor = Color.FromArgb(248, 248, 248);
            BtnSave.Location = new Point(531, 588);
            BtnSave.MinimumSize = new Size(1, 1);
            BtnSave.Name = "BtnSave";
            BtnSave.RectColor = Color.DodgerBlue;
            BtnSave.RectDisableColor = Color.DodgerBlue;
            BtnSave.Size = new Size(132, 39);
            BtnSave.Style = UIStyle.Custom;
            BtnSave.Symbol = 61639;
            BtnSave.SymbolSize = 32;
            BtnSave.TabIndex = 8;
            BtnSave.Text = "保存";
            BtnSave.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnSave.Click += BtnSave_Click;
            // 
            // Form_Condition
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(960, 638);
            Controls.Add(BtnDelete);
            Controls.Add(BtnSave);
            Controls.Add(DataGridViewDefineVar);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Condition";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "条件判断";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private UIDataGridView DataGridViewDefineVar;
        private UISymbolButton BtnDelete;
        private UISymbolButton BtnSave;
        private DataGridViewTextBoxColumn ColVarName;
        private DataGridViewComboBoxColumn ColOperator;
        private DataGridViewTextBoxColumn ColValue;
        private DataGridViewTextBoxColumn ColTrueStepIndex;
        private DataGridViewTextBoxColumn ColFalseStepIndex;
    }
}
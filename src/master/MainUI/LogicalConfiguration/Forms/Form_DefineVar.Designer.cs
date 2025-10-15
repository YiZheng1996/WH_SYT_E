namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_DefineVar
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
            BtnSave = new UISymbolButton();
            BtnDelete = new UISymbolButton();
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
            DataGridViewDefineVar.Location = new Point(29, 52);
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
            DataGridViewDefineVar.Size = new Size(529, 345);
            DataGridViewDefineVar.StripeOddColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.Style = UIStyle.Custom;
            DataGridViewDefineVar.TabIndex = 5;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "变量名称";
            ColVarName.Name = "ColVarName";
            ColVarName.Width = 150;
            // 
            // ColVarType
            // 
            ColVarType.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            ColVarType.HeaderText = "数据类型";
            ColVarType.Items.AddRange(new object[] { "int", "double", "bool" });
            ColVarType.Name = "ColVarType";
            ColVarType.Resizable = DataGridViewTriState.True;
            ColVarType.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // ColVarText
            // 
            ColVarText.HeaderText = "备注";
            ColVarText.Name = "ColVarText";
            ColVarText.Width = 245;
            // 
            // BtnSave
            // 
            BtnSave.Cursor = Cursors.Hand;
            BtnSave.FillColor = Color.DodgerBlue;
            BtnSave.FillColor2 = Color.DodgerBlue;
            BtnSave.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnSave.LightColor = Color.FromArgb(248, 248, 248);
            BtnSave.Location = new Point(342, 408);
            BtnSave.MinimumSize = new Size(1, 1);
            BtnSave.Name = "BtnSave";
            BtnSave.RectColor = Color.DodgerBlue;
            BtnSave.RectDisableColor = Color.DodgerBlue;
            BtnSave.Size = new Size(132, 39);
            BtnSave.Style = UIStyle.Custom;
            BtnSave.Symbol = 61639;
            BtnSave.SymbolSize = 32;
            BtnSave.TabIndex = 6;
            BtnSave.Text = "保存";
            BtnSave.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnSave.Click += Save_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.FillColor = Color.DodgerBlue;
            BtnDelete.FillColor2 = Color.DodgerBlue;
            BtnDelete.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnDelete.LightColor = Color.FromArgb(248, 248, 248);
            BtnDelete.Location = new Point(108, 408);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.RectColor = Color.DodgerBlue;
            BtnDelete.RectDisableColor = Color.DodgerBlue;
            BtnDelete.RectHoverColor = Color.FromArgb(64, 128, 204);
            BtnDelete.Size = new Size(132, 39);
            BtnDelete.Style = UIStyle.Custom;
            BtnDelete.Symbol = 561695;
            BtnDelete.SymbolSize = 32;
            BtnDelete.TabIndex = 7;
            BtnDelete.Text = "删除";
            BtnDelete.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnDelete.Click += Clean_Click;
            // 
            // Form_DefineVar
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(587, 460);
            ControlBoxFillHoverColor = Color.FromArgb(163, 163, 163);
            Controls.Add(BtnDelete);
            Controls.Add(BtnSave);
            Controls.Add(DataGridViewDefineVar);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_DefineVar";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Style = UIStyle.Custom;
            Text = "变量定义";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIDataGridView DataGridViewDefineVar;
        private Sunny.UI.UISymbolButton BtnSave;
        private Sunny.UI.UISymbolButton BtnDelete;
        private DataGridViewTextBoxColumn ColVarName;
        private DataGridViewComboBoxColumn ColVarType;
        private DataGridViewTextBoxColumn ColVarText;
    }
}
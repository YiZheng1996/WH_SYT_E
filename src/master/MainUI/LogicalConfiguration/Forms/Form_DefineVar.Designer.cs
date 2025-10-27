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
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewDefineVar = new UIDataGridView();
            ColVarName = new DataGridViewTextBoxColumn();
            ColVarType = new DataGridViewComboBoxColumn();
            ColVarText = new DataGridViewTextBoxColumn();
            BtnSave = new UISymbolButton();
            BtnDelete = new UISymbolButton();
            BtnHelp = new UISymbolButton();
            lblTip = new UILabel();
            pnlMain = new UIPanel();
            pnlBottom = new UIPanel();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            pnlMain.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // DataGridViewDefineVar
            // 
            dataGridViewCellStyle6.BackColor = Color.FromArgb(250, 250, 252);
            DataGridViewDefineVar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.BorderStyle = BorderStyle.None;
            DataGridViewDefineVar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle7.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle7.SelectionForeColor = Color.White;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            DataGridViewDefineVar.ColumnHeadersHeight = 45;
            DataGridViewDefineVar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColVarType, ColVarText });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.Font = new Font("微软雅黑", 10F);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle8.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            DataGridViewDefineVar.DefaultCellStyle = dataGridViewCellStyle8;
            DataGridViewDefineVar.EnableHeadersVisualStyles = false;
            DataGridViewDefineVar.Font = new Font("微软雅黑", 10F);
            DataGridViewDefineVar.GridColor = Color.FromArgb(233, 236, 239);
            DataGridViewDefineVar.Location = new Point(15, 45);
            DataGridViewDefineVar.MultiSelect = false;
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(243, 249, 255);
            dataGridViewCellStyle9.Font = new Font("微软雅黑", 10F);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            DataGridViewDefineVar.RowHeadersWidth = 51;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("微软雅黑", 10F);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle10;
            DataGridViewDefineVar.RowTemplate.Height = 40;
            DataGridViewDefineVar.ScrollBars = ScrollBars.Vertical;
            DataGridViewDefineVar.SelectedIndex = -1;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDefineVar.Size = new Size(590, 335);
            DataGridViewDefineVar.StripeOddColor = Color.FromArgb(250, 250, 252);
            DataGridViewDefineVar.Style = UIStyle.Custom;
            DataGridViewDefineVar.TabIndex = 1;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "变量名称";
            ColVarName.Name = "ColVarName";
            ColVarName.Width = 200;
            // 
            // ColVarType
            // 
            ColVarType.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            ColVarType.HeaderText = "数据类型";
            ColVarType.Items.AddRange(new object[] { "int", "double", "bool", "string" });
            ColVarType.Name = "ColVarType";
            ColVarType.Resizable = DataGridViewTriState.True;
            ColVarType.SortMode = DataGridViewColumnSortMode.Automatic;
            ColVarType.Width = 150;
            // 
            // ColVarText
            // 
            ColVarText.HeaderText = "备注说明";
            ColVarText.Name = "ColVarText";
            ColVarText.Width = 240;
            // 
            // BtnSave
            // 
            BtnSave.Cursor = Cursors.Hand;
            BtnSave.FillColor = Color.FromArgb(16, 185, 129);
            BtnSave.FillColor2 = Color.FromArgb(5, 150, 105);
            BtnSave.FillColorGradient = true;
            BtnSave.FillColorGradientDirection = FlowDirection.LeftToRight;
            BtnSave.FillDisableColor = Color.FromArgb(149, 154, 164);
            BtnSave.FillHoverColor = Color.FromArgb(20, 184, 166);
            BtnSave.FillPressColor = Color.FromArgb(13, 148, 136);
            BtnSave.FillSelectedColor = Color.FromArgb(13, 148, 136);
            BtnSave.Font = new Font("微软雅黑", 12F);
            BtnSave.LightColor = Color.FromArgb(248, 248, 248);
            BtnSave.Location = new Point(525, 18);
            BtnSave.MinimumSize = new Size(1, 1);
            BtnSave.Name = "BtnSave";
            BtnSave.Radius = 8;
            BtnSave.RectColor = Color.FromArgb(16, 185, 129);
            BtnSave.RectDisableColor = Color.FromArgb(149, 154, 164);
            BtnSave.RectHoverColor = Color.FromArgb(20, 184, 166);
            BtnSave.RectPressColor = Color.FromArgb(13, 148, 136);
            BtnSave.RectSelectedColor = Color.FromArgb(13, 148, 136);
            BtnSave.Size = new Size(110, 38);
            BtnSave.Style = UIStyle.Custom;
            BtnSave.Symbol = 61639;
            BtnSave.TabIndex = 3;
            BtnSave.Text = "保存";
            BtnSave.TipsFont = new Font("微软雅黑", 9F);
            BtnSave.Click += Save_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.FillColor = Color.FromArgb(239, 68, 68);
            BtnDelete.FillColor2 = Color.FromArgb(220, 38, 38);
            BtnDelete.FillColorGradient = true;
            BtnDelete.FillColorGradientDirection = FlowDirection.LeftToRight;
            BtnDelete.FillDisableColor = Color.FromArgb(149, 154, 164);
            BtnDelete.FillHoverColor = Color.FromArgb(248, 113, 113);
            BtnDelete.FillPressColor = Color.FromArgb(220, 38, 38);
            BtnDelete.FillSelectedColor = Color.FromArgb(220, 38, 38);
            BtnDelete.Font = new Font("微软雅黑", 12F);
            BtnDelete.LightColor = Color.FromArgb(248, 248, 248);
            BtnDelete.Location = new Point(409, 18);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Radius = 8;
            BtnDelete.RectColor = Color.FromArgb(239, 68, 68);
            BtnDelete.RectDisableColor = Color.FromArgb(149, 154, 164);
            BtnDelete.RectHoverColor = Color.FromArgb(248, 113, 113);
            BtnDelete.RectPressColor = Color.FromArgb(220, 38, 38);
            BtnDelete.RectSelectedColor = Color.FromArgb(220, 38, 38);
            BtnDelete.Size = new Size(110, 38);
            BtnDelete.Style = UIStyle.Custom;
            BtnDelete.Symbol = 61453;
            BtnDelete.TabIndex = 4;
            BtnDelete.Text = "删除";
            BtnDelete.TipsFont = new Font("微软雅黑", 9F);
            BtnDelete.Click += Clean_Click;
            // 
            // BtnHelp
            // 
            BtnHelp.Cursor = Cursors.Hand;
            BtnHelp.FillColor = Color.FromArgb(100, 116, 139);
            BtnHelp.FillColor2 = Color.FromArgb(71, 85, 105);
            BtnHelp.FillColorGradient = true;
            BtnHelp.FillColorGradientDirection = FlowDirection.LeftToRight;
            BtnHelp.FillDisableColor = Color.FromArgb(149, 154, 164);
            BtnHelp.FillHoverColor = Color.FromArgb(125, 144, 178);
            BtnHelp.FillPressColor = Color.FromArgb(71, 85, 105);
            BtnHelp.FillSelectedColor = Color.FromArgb(71, 85, 105);
            BtnHelp.Font = new Font("微软雅黑", 12F);
            BtnHelp.LightColor = Color.FromArgb(248, 248, 248);
            BtnHelp.Location = new Point(15, 18);
            BtnHelp.MinimumSize = new Size(1, 1);
            BtnHelp.Name = "BtnHelp";
            BtnHelp.Radius = 8;
            BtnHelp.RectColor = Color.FromArgb(100, 116, 139);
            BtnHelp.RectDisableColor = Color.FromArgb(149, 154, 164);
            BtnHelp.RectHoverColor = Color.FromArgb(125, 144, 178);
            BtnHelp.RectPressColor = Color.FromArgb(71, 85, 105);
            BtnHelp.RectSelectedColor = Color.FromArgb(71, 85, 105);
            BtnHelp.Size = new Size(110, 38);
            BtnHelp.Style = UIStyle.Custom;
            BtnHelp.Symbol = 61736;
            BtnHelp.TabIndex = 2;
            BtnHelp.Text = "使用说明";
            BtnHelp.TipsFont = new Font("微软雅黑", 9F);
            BtnHelp.Click += BtnHelp_Click;
            // 
            // lblTip
            // 
            lblTip.BackColor = Color.Transparent;
            lblTip.Font = new Font("微软雅黑", 10F);
            lblTip.ForeColor = Color.FromArgb(100, 116, 139);
            lblTip.Location = new Point(15, 10);
            lblTip.Name = "lblTip";
            lblTip.Size = new Size(590, 25);
            lblTip.Style = UIStyle.Custom;
            lblTip.TabIndex = 0;
            lblTip.Text = "提示: 双击单元格可编辑,支持添加、删除和修改变量";
            lblTip.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(DataGridViewDefineVar);
            pnlMain.Controls.Add(lblTip);
            pnlMain.FillColor = Color.White;
            pnlMain.FillColor2 = Color.White;
            pnlMain.Font = new Font("微软雅黑", 12F);
            pnlMain.Location = new Point(15, 60);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.MinimumSize = new Size(1, 1);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(15);
            pnlMain.Radius = 8;
            pnlMain.RectColor = Color.FromArgb(233, 236, 239);
            pnlMain.Size = new Size(620, 395);
            pnlMain.Style = UIStyle.Custom;
            pnlMain.TabIndex = 0;
            pnlMain.Text = null;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(BtnHelp);
            pnlBottom.Controls.Add(BtnDelete);
            pnlBottom.Controls.Add(BtnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.FillColor = Color.FromArgb(248, 249, 250);
            pnlBottom.FillColor2 = Color.FromArgb(248, 249, 250);
            pnlBottom.Font = new Font("微软雅黑", 12F);
            pnlBottom.Location = new Point(0, 465);
            pnlBottom.Margin = new Padding(4, 5, 4, 5);
            pnlBottom.MinimumSize = new Size(1, 1);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(15, 10, 15, 10);
            pnlBottom.Radius = 0;
            pnlBottom.RectColor = Color.FromArgb(233, 236, 239);
            pnlBottom.Size = new Size(650, 75);
            pnlBottom.Style = UIStyle.Custom;
            pnlBottom.TabIndex = 1;
            pnlBottom.Text = null;
            pnlBottom.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // Form_DefineVar
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(650, 540);
            ControlBoxFillHoverColor = Color.FromArgb(163, 163, 163);
            Controls.Add(pnlBottom);
            Controls.Add(pnlMain);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_DefineVar";
            Padding = new Padding(0);
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Style = UIStyle.Custom;
            Text = "变量定义";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 650, 540);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            pnlMain.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIDataGridView DataGridViewDefineVar;
        private Sunny.UI.UISymbolButton BtnSave;
        private Sunny.UI.UISymbolButton BtnDelete;
        private Sunny.UI.UISymbolButton BtnHelp;
        private Sunny.UI.UILabel lblTip;
        private Sunny.UI.UIPanel pnlMain;
        private Sunny.UI.UIPanel pnlBottom;
        private DataGridViewTextBoxColumn ColVarName;
        private DataGridViewComboBoxColumn ColVarType;
        private DataGridViewTextBoxColumn ColVarText;
    }
}
namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class Form_WritePLC
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
            DataGridViewPLCList = new UIDataGridView();
            TreeViewPLC = new UITreeView();
            BtnDelete = new UISymbolButton();
            BtnSave = new UISymbolButton();
            uiPanel1 = new UIPanel();
            uiLine2 = new UILine();
            uiLine1 = new UILine();
            ColPCLModelName = new DataGridViewTextBoxColumn();
            ColPCLKeyName = new DataGridViewTextBoxColumn();
            ColConstant = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)DataGridViewPLCList).BeginInit();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // DataGridViewPLCList
            // 
            DataGridViewPLCList.AllowDrop = true;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            DataGridViewPLCList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewPLCList.BackgroundColor = Color.White;
            DataGridViewPLCList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataGridViewPLCList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewPLCList.ColumnHeadersHeight = 35;
            DataGridViewPLCList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewPLCList.Columns.AddRange(new DataGridViewColumn[] { ColPCLModelName, ColPCLKeyName, ColConstant });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("宋体", 13F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewPLCList.DefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewPLCList.EnableHeadersVisualStyles = false;
            DataGridViewPLCList.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            DataGridViewPLCList.GridColor = Color.FromArgb(64, 64, 64);
            DataGridViewPLCList.Location = new Point(303, 80);
            DataGridViewPLCList.MultiSelect = false;
            DataGridViewPLCList.Name = "DataGridViewPLCList";
            DataGridViewPLCList.RectColor = Color.White;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(248, 248, 248);
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 13F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            DataGridViewPLCList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewPLCList.RowHeadersWidth = 28;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("微软雅黑", 13F);
            DataGridViewPLCList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            DataGridViewPLCList.RowTemplate.Height = 30;
            DataGridViewPLCList.SelectedIndex = -1;
            DataGridViewPLCList.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DataGridViewPLCList.Size = new Size(640, 534);
            DataGridViewPLCList.StripeOddColor = Color.FromArgb(235, 243, 255);
            DataGridViewPLCList.Style = UIStyle.Custom;
            DataGridViewPLCList.TabIndex = 12;
            DataGridViewPLCList.DragDrop += DataGridViewPLCList_DragDrop;
            DataGridViewPLCList.DragEnter += DataGridViewPLCList_DragEnter;
            // 
            // TreeViewPLC
            // 
            TreeViewPLC.BackColor = Color.Transparent;
            TreeViewPLC.FillColor = Color.White;
            TreeViewPLC.FillColor2 = Color.White;
            TreeViewPLC.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            TreeViewPLC.Location = new Point(17, 80);
            TreeViewPLC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TreeViewPLC.MinimumSize = new Size(1, 1);
            TreeViewPLC.Name = "TreeViewPLC";
            TreeViewPLC.Radius = 10;
            TreeViewPLC.RectColor = Color.White;
            TreeViewPLC.RectDisableColor = Color.White;
            TreeViewPLC.ScrollBarStyleInherited = false;
            TreeViewPLC.ShowLines = true;
            TreeViewPLC.ShowNodeToolTips = true;
            TreeViewPLC.ShowText = false;
            TreeViewPLC.Size = new Size(279, 534);
            TreeViewPLC.TabIndex = 439;
            TreeViewPLC.Text = null;
            TreeViewPLC.TextAlignment = ContentAlignment.MiddleCenter;
            TreeViewPLC.ItemDrag += TreeViewPLC_ItemDrag;
            // 
            // BtnDelete
            // 
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.FillColor = Color.DodgerBlue;
            BtnDelete.FillColor2 = Color.DodgerBlue;
            BtnDelete.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnDelete.LightColor = Color.FromArgb(248, 248, 248);
            BtnDelete.Location = new Point(303, 9);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.RectColor = Color.DodgerBlue;
            BtnDelete.RectDisableColor = Color.DodgerBlue;
            BtnDelete.RectHoverColor = Color.FromArgb(64, 128, 204);
            BtnDelete.Size = new Size(132, 39);
            BtnDelete.Style = UIStyle.Custom;
            BtnDelete.Symbol = 561695;
            BtnDelete.SymbolSize = 32;
            BtnDelete.TabIndex = 441;
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
            BtnSave.Location = new Point(492, 9);
            BtnSave.MinimumSize = new Size(1, 1);
            BtnSave.Name = "BtnSave";
            BtnSave.RectColor = Color.DodgerBlue;
            BtnSave.RectDisableColor = Color.DodgerBlue;
            BtnSave.Size = new Size(132, 39);
            BtnSave.Style = UIStyle.Custom;
            BtnSave.Symbol = 61639;
            BtnSave.SymbolSize = 32;
            BtnSave.TabIndex = 440;
            BtnSave.Text = "保存";
            BtnSave.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnSave.Click += BtnSave_Click;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(BtnDelete);
            uiPanel1.Controls.Add(BtnSave);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel1.Location = new Point(17, 622);
            uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 10;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(926, 57);
            uiPanel1.TabIndex = 442;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(17, 43);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(279, 29);
            uiLine2.TabIndex = 442;
            uiLine2.Text = "选择PLC";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.EndCap = UILineCap.Circle;
            uiLine1.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.White;
            uiLine1.Location = new Point(303, 43);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(640, 29);
            uiLine1.TabIndex = 443;
            uiLine1.Text = "写入数据";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ColPCLModelName
            // 
            ColPCLModelName.HeaderText = "PLC模块名称";
            ColPCLModelName.Name = "ColPCLModelName";
            ColPCLModelName.Width = 240;
            // 
            // ColPCLKeyName
            // 
            ColPCLKeyName.HeaderText = "PLC点位名称";
            ColPCLKeyName.Name = "ColPCLKeyName";
            ColPCLKeyName.Width = 240;
            // 
            // ColConstant
            // 
            ColConstant.HeaderText = "常数";
            ColConstant.Name = "ColConstant";
            ColConstant.Width = 130;
            // 
            // Form_WritePLC
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(956, 690);
            ControlBoxFillHoverColor = Color.FromArgb(163, 163, 163);
            Controls.Add(uiLine1);
            Controls.Add(uiLine2);
            Controls.Add(uiPanel1);
            Controls.Add(TreeViewPLC);
            Controls.Add(DataGridViewPLCList);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_WritePLC";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Style = UIStyle.Custom;
            Text = "PLC写入";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ((System.ComponentModel.ISupportInitialize)DataGridViewPLCList).EndInit();
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private UIDataGridView DataGridViewPLCList;
        private UITreeView TreeViewPLC;
        private UISymbolButton BtnDelete;
        private UISymbolButton BtnSave;
        private UIPanel uiPanel1;
        private UILine uiLine2;
        private UILine uiLine1;
        private DataGridViewTextBoxColumn ColPCLModelName;
        private DataGridViewTextBoxColumn ColPCLKeyName;
        private DataGridViewTextBoxColumn ColConstant;
    }
}
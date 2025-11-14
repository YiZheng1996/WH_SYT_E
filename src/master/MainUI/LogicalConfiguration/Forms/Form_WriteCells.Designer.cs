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
            panelFileConfig = new UIPanel();
            txtSheetName = new UITextBox();
            lblSheetName = new Label();
            lblFileConfigTitle = new Label();
            panelMain = new Panel();
            DataGridViewDefineVar = new UIDataGridView();
            ColVarName = new DataGridViewTextBoxColumn();
            ColVarType = new DataGridViewComboBoxColumn();
            ColVarText = new DataGridViewTextBoxColumn();
            panelToolbar = new Panel();
            lblHelpText = new Label();
            btnAddRow = new UISymbolButton();
            BtnDelete = new UISymbolButton();
            lblToolbarTitle = new Label();
            lblCellConfigTitle = new Label();
            panelPreview = new Panel();
            lblPreviewTitle = new Label();
            txtPreviewContent = new RichTextBox();
            panelBottom = new Panel();
            btnCancel = new UISymbolButton();
            btnSave = new UISymbolButton();
            panelFileConfig.SuspendLayout();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            panelToolbar.SuspendLayout();
            panelPreview.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelFileConfig
            // 
            panelFileConfig.BackColor = Color.FromArgb(248, 250, 252);
            panelFileConfig.Controls.Add(txtSheetName);
            panelFileConfig.Controls.Add(lblSheetName);
            panelFileConfig.Controls.Add(lblFileConfigTitle);
            panelFileConfig.Dock = DockStyle.Top;
            panelFileConfig.FillColor = Color.FromArgb(248, 250, 252);
            panelFileConfig.FillColor2 = Color.FromArgb(248, 250, 252);
            panelFileConfig.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelFileConfig.Location = new Point(0, 35);
            panelFileConfig.Margin = new Padding(5);
            panelFileConfig.MinimumSize = new Size(1, 1);
            panelFileConfig.Name = "panelFileConfig";
            panelFileConfig.Padding = new Padding(20, 15, 20, 15);
            panelFileConfig.Radius = 0;
            panelFileConfig.RectColor = Color.FromArgb(65, 100, 204);
            panelFileConfig.RectDisableColor = Color.FromArgb(65, 100, 204);
            panelFileConfig.Size = new Size(1000, 100);
            panelFileConfig.TabIndex = 1;
            panelFileConfig.Text = null;
            panelFileConfig.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtSheetName
            // 
            txtSheetName.Cursor = Cursors.IBeam;
            txtSheetName.Font = new Font("微软雅黑", 10F);
            txtSheetName.Location = new Point(135, 47);
            txtSheetName.Margin = new Padding(4, 5, 4, 5);
            txtSheetName.MinimumSize = new Size(1, 16);
            txtSheetName.Name = "txtSheetName";
            txtSheetName.Padding = new Padding(5);
            txtSheetName.RectColor = Color.FromArgb(220, 220, 220);
            txtSheetName.ShowText = false;
            txtSheetName.Size = new Size(401, 36);
            txtSheetName.TabIndex = 0;
            txtSheetName.TextAlignment = ContentAlignment.MiddleLeft;
            txtSheetName.Watermark = "请输入工作表名称,如: Sheet1";
            // 
            // lblSheetName
            // 
            lblSheetName.AutoSize = true;
            lblSheetName.Font = new Font("微软雅黑", 10F);
            lblSheetName.ForeColor = Color.FromArgb(80, 80, 80);
            lblSheetName.Location = new Point(55, 53);
            lblSheetName.Name = "lblSheetName";
            lblSheetName.Size = new Size(65, 20);
            lblSheetName.TabIndex = 2;
            lblSheetName.Text = "工作表名";
            // 
            // lblFileConfigTitle
            // 
            lblFileConfigTitle.AutoSize = true;
            lblFileConfigTitle.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            lblFileConfigTitle.ForeColor = Color.FromArgb(80, 80, 80);
            lblFileConfigTitle.Location = new Point(20, 15);
            lblFileConfigTitle.Name = "lblFileConfigTitle";
            lblFileConfigTitle.Size = new Size(108, 19);
            lblFileConfigTitle.TabIndex = 0;
            lblFileConfigTitle.Text = "Excel文件配置";
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(248, 250, 252);
            panelMain.Controls.Add(DataGridViewDefineVar);
            panelMain.Controls.Add(panelToolbar);
            panelMain.Controls.Add(lblCellConfigTitle);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 135);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20, 10, 20, 10);
            panelMain.Size = new Size(1000, 465);
            panelMain.TabIndex = 2;
            // 
            // DataGridViewDefineVar
            // 
            DataGridViewDefineVar.AllowUserToAddRows = false;
            DataGridViewDefineVar.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.BorderStyle = BorderStyle.Fixed3D;
            DataGridViewDefineVar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewDefineVar.ColumnHeadersHeight = 40;
            DataGridViewDefineVar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColVarType, ColVarText });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewDefineVar.DefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewDefineVar.Dock = DockStyle.Fill;
            DataGridViewDefineVar.EnableHeadersVisualStyles = false;
            DataGridViewDefineVar.Font = new Font("微软雅黑", 9F);
            DataGridViewDefineVar.GridColor = Color.FromArgb(65, 100, 204);
            DataGridViewDefineVar.Location = new Point(20, 74);
            DataGridViewDefineVar.MultiSelect = false;
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            DataGridViewDefineVar.RectColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewDefineVar.RowHeadersVisible = false;
            DataGridViewDefineVar.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle5;
            DataGridViewDefineVar.RowTemplate.Height = 35;
            DataGridViewDefineVar.SelectedIndex = -1;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDefineVar.Size = new Size(960, 381);
            DataGridViewDefineVar.StripeOddColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.TabIndex = 2;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "单元格地址";
            ColVarName.MinimumWidth = 6;
            ColVarName.Name = "ColVarName";
            ColVarName.Width = 180;
            // 
            // ColVarType
            // 
            ColVarType.HeaderText = "数据来源";
            ColVarType.Items.AddRange(new object[] { "固定值", "变量", "表达式", "系统属性" });
            ColVarType.MinimumWidth = 6;
            ColVarType.Name = "ColVarType";
            ColVarType.Resizable = DataGridViewTriState.True;
            ColVarType.SortMode = DataGridViewColumnSortMode.Automatic;
            ColVarType.Width = 150;
            // 
            // ColVarText
            // 
            ColVarText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColVarText.HeaderText = "内容 (根据类型填写)";
            ColVarText.MinimumWidth = 6;
            ColVarText.Name = "ColVarText";
            // 
            // panelToolbar
            // 
            panelToolbar.BackColor = Color.FromArgb(250, 250, 250);
            panelToolbar.Controls.Add(lblHelpText);
            panelToolbar.Controls.Add(btnAddRow);
            panelToolbar.Controls.Add(BtnDelete);
            panelToolbar.Controls.Add(lblToolbarTitle);
            panelToolbar.Dock = DockStyle.Top;
            panelToolbar.Location = new Point(20, 10);
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Size = new Size(960, 64);
            panelToolbar.TabIndex = 1;
            // 
            // lblHelpText
            // 
            lblHelpText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblHelpText.Font = new Font("微软雅黑", 9F);
            lblHelpText.ForeColor = Color.FromArgb(100, 100, 100);
            lblHelpText.Location = new Point(600, 8);
            lblHelpText.Name = "lblHelpText";
            lblHelpText.Size = new Size(350, 40);
            lblHelpText.TabIndex = 2;
            lblHelpText.Text = "类型说明: 固定值-直接输入 | 变量-点击选择 | 表达式-点击构建 | 系统属性-点击浏览";
            lblHelpText.TextAlign = ContentAlignment.TopRight;
            // 
            // btnAddRow
            // 
            btnAddRow.Cursor = Cursors.Hand;
            btnAddRow.FillColor = Color.FromArgb(88, 165, 49);
            btnAddRow.FillHoverColor = Color.FromArgb(111, 191, 77);
            btnAddRow.FillPressColor = Color.FromArgb(56, 106, 32);
            btnAddRow.FillSelectedColor = Color.FromArgb(56, 106, 32);
            btnAddRow.Font = new Font("微软雅黑", 9F);
            btnAddRow.Location = new Point(13, 31);
            btnAddRow.MinimumSize = new Size(1, 1);
            btnAddRow.Name = "btnAddRow";
            btnAddRow.RectColor = Color.FromArgb(88, 165, 49);
            btnAddRow.RectHoverColor = Color.FromArgb(111, 191, 77);
            btnAddRow.Size = new Size(100, 28);
            btnAddRow.Symbol = 61543;
            btnAddRow.TabIndex = 0;
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
            BtnDelete.Location = new Point(125, 31);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.RectColor = Color.FromArgb(230, 80, 80);
            BtnDelete.RectHoverColor = Color.FromArgb(232, 127, 128);
            BtnDelete.Size = new Size(100, 28);
            BtnDelete.Symbol = 61460;
            BtnDelete.TabIndex = 1;
            BtnDelete.Text = "删除行";
            BtnDelete.TipsFont = new Font("微软雅黑", 9F);
            // 
            // lblToolbarTitle
            // 
            lblToolbarTitle.AutoSize = true;
            lblToolbarTitle.Font = new Font("微软雅黑", 9F);
            lblToolbarTitle.ForeColor = Color.Gray;
            lblToolbarTitle.Location = new Point(10, 8);
            lblToolbarTitle.Name = "lblToolbarTitle";
            lblToolbarTitle.Size = new Size(234, 17);
            lblToolbarTitle.TabIndex = 0;
            lblToolbarTitle.Text = "💡 提示: 单元格地址格式如 A1, B2, C3 等";
            // 
            // lblCellConfigTitle
            // 
            lblCellConfigTitle.AutoSize = true;
            lblCellConfigTitle.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            lblCellConfigTitle.ForeColor = Color.FromArgb(80, 80, 80);
            lblCellConfigTitle.Location = new Point(20, 10);
            lblCellConfigTitle.Name = "lblCellConfigTitle";
            lblCellConfigTitle.Size = new Size(134, 19);
            lblCellConfigTitle.TabIndex = 0;
            lblCellConfigTitle.Text = "📝 单元格写入配置";
            // 
            // panelPreview
            // 
            panelPreview.BackColor = Color.FromArgb(248, 250, 252);
            panelPreview.Controls.Add(lblPreviewTitle);
            panelPreview.Controls.Add(txtPreviewContent);
            panelPreview.Dock = DockStyle.Bottom;
            panelPreview.Location = new Point(0, 600);
            panelPreview.Name = "panelPreview";
            panelPreview.Padding = new Padding(20, 10, 20, 10);
            panelPreview.Size = new Size(1000, 120);
            panelPreview.TabIndex = 3;
            // 
            // lblPreviewTitle
            // 
            lblPreviewTitle.AutoSize = true;
            lblPreviewTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblPreviewTitle.ForeColor = Color.FromArgb(80, 80, 80);
            lblPreviewTitle.Location = new Point(20, 12);
            lblPreviewTitle.Name = "lblPreviewTitle";
            lblPreviewTitle.Size = new Size(93, 19);
            lblPreviewTitle.TabIndex = 0;
            lblPreviewTitle.Text = "写入预览信息";
            // 
            // txtPreviewContent
            // 
            txtPreviewContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtPreviewContent.BackColor = Color.White;
            txtPreviewContent.BorderStyle = BorderStyle.FixedSingle;
            txtPreviewContent.Font = new Font("Consolas", 9F);
            txtPreviewContent.ForeColor = Color.FromArgb(80, 80, 80);
            txtPreviewContent.Location = new Point(20, 40);
            txtPreviewContent.Name = "txtPreviewContent";
            txtPreviewContent.ReadOnly = true;
            txtPreviewContent.Size = new Size(960, 70);
            txtPreviewContent.TabIndex = 1;
            txtPreviewContent.Text = "请选择一行查看预览信息";
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(248, 250, 252);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 720);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1000, 70);
            panelBottom.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(230, 80, 80);
            btnCancel.FillHoverColor = Color.FromArgb(232, 127, 128);
            btnCancel.FillPressColor = Color.FromArgb(202, 87, 89);
            btnCancel.FillSelectedColor = Color.FromArgb(202, 87, 89);
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(861, 16);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(230, 80, 80);
            btnCancel.RectHoverColor = Color.FromArgb(232, 127, 128);
            btnCancel.Size = new Size(130, 38);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 8;
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
            btnSave.Location = new Point(716, 16);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectColor = Color.FromArgb(65, 100, 204);
            btnSave.RectHoverColor = Color.FromArgb(88, 165, 49);
            btnSave.Size = new Size(130, 38);
            btnSave.Symbol = 61639;
            btnSave.SymbolSize = 28;
            btnSave.TabIndex = 7;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            // 
            // Form_WriteCells
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1000, 790);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelPreview);
            Controls.Add(panelBottom);
            Controls.Add(panelFileConfig);
            Font = new Font("微软雅黑", 9F);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(1000, 790);
            Name = "Form_WriteCells";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "写入报表单元格";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 13F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1000, 790);
            panelFileConfig.ResumeLayout(false);
            panelFileConfig.PerformLayout();
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            panelToolbar.ResumeLayout(false);
            panelToolbar.PerformLayout();
            panelPreview.ResumeLayout(false);
            panelPreview.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIPanel panelFileConfig;
        private System.Windows.Forms.Label lblFileConfigTitle;
        private System.Windows.Forms.Label lblSheetName;
        private Sunny.UI.UITextBox txtSheetName;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblCellConfigTitle;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Label lblToolbarTitle;
        private Sunny.UI.UISymbolButton btnAddRow;
        private Sunny.UI.UISymbolButton BtnDelete;
        private System.Windows.Forms.Label lblHelpText;
        private Sunny.UI.UIDataGridView DataGridViewDefineVar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVarName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColVarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVarText;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Label lblPreviewTitle;
        private System.Windows.Forms.RichTextBox txtPreviewContent;
        private System.Windows.Forms.Panel panelBottom;
        private UISymbolButton btnCancel;
        private UISymbolButton btnSave;
    }
}
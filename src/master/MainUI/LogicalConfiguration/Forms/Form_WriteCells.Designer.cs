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
            panelFileConfig = new Panel();
            txtSheetName = new UITextBox();
            lblSheetName = new UILabel();
            panelMain = new Panel();
            DataGridViewDefineVar = new DataGridView();
            ColIndex = new DataGridViewTextBoxColumn();
            ColCellAddress = new DataGridViewTextBoxColumn();
            ColVarText = new DataGridViewTextBoxColumn();
            panelButtons = new Panel();
            btnMoveDown = new UISymbolButton();
            btnMoveUp = new UISymbolButton();
            btnDelete = new UISymbolButton();
            btnAdd = new UISymbolButton();
            panelBottom = new Panel();
            btnCancel = new UIButton();
            btnSave = new UIButton();
            panelFileConfig.SuspendLayout();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            panelButtons.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelFileConfig
            // 
            panelFileConfig.BackColor = Color.White;
            panelFileConfig.Controls.Add(txtSheetName);
            panelFileConfig.Controls.Add(lblSheetName);
            panelFileConfig.Dock = DockStyle.Top;
            panelFileConfig.Location = new Point(0, 35);
            panelFileConfig.Name = "panelFileConfig";
            panelFileConfig.Padding = new Padding(15, 10, 15, 10);
            panelFileConfig.Size = new Size(888, 70);
            panelFileConfig.TabIndex = 1;
            // 
            // txtSheetName
            // 
            txtSheetName.Cursor = Cursors.IBeam;
            txtSheetName.Font = new Font("微软雅黑", 10F);
            txtSheetName.Location = new Point(124, 15);
            txtSheetName.Margin = new Padding(4, 5, 4, 5);
            txtSheetName.MinimumSize = new Size(1, 16);
            txtSheetName.Name = "txtSheetName";
            txtSheetName.Padding = new Padding(5);
            txtSheetName.RectColor = Color.FromArgb(65, 100, 204);
            txtSheetName.ShowText = false;
            txtSheetName.Size = new Size(650, 30);
            txtSheetName.TabIndex = 1;
            txtSheetName.Text = "Sheet1";
            txtSheetName.TextAlignment = ContentAlignment.MiddleLeft;
            txtSheetName.Watermark = "请输入工作表名称";
            // 
            // lblSheetName
            // 
            lblSheetName.Font = new Font("微软雅黑", 10F);
            lblSheetName.ForeColor = Color.FromArgb(48, 48, 48);
            lblSheetName.Location = new Point(18, 15);
            lblSheetName.Name = "lblSheetName";
            lblSheetName.Size = new Size(100, 25);
            lblSheetName.TabIndex = 0;
            lblSheetName.Text = "工作表名称:";
            lblSheetName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.BorderStyle = BorderStyle.FixedSingle;
            panelMain.Controls.Add(DataGridViewDefineVar);
            panelMain.Controls.Add(panelButtons);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 105);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15, 10, 15, 10);
            panelMain.Size = new Size(888, 379);
            panelMain.TabIndex = 2;
            // 
            // DataGridViewDefineVar
            // 
            DataGridViewDefineVar.AllowUserToResizeRows = false;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewDefineVar.ColumnHeadersHeight = 40;
            DataGridViewDefineVar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColIndex, ColCellAddress, ColVarText });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 236, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DataGridViewDefineVar.DefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewDefineVar.Dock = DockStyle.Fill;
            DataGridViewDefineVar.EnableHeadersVisualStyles = false;
            DataGridViewDefineVar.GridColor = Color.FromArgb(230, 230, 230);
            DataGridViewDefineVar.Location = new Point(15, 10);
            DataGridViewDefineVar.MultiSelect = false;
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(243, 249, 255);
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewDefineVar.RowHeadersWidth = 45;
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 11F);
            dataGridViewCellStyle4.Padding = new Padding(3);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewDefineVar.RowTemplate.Height = 32;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDefineVar.Size = new Size(742, 357);
            DataGridViewDefineVar.TabIndex = 1;
            // 
            // ColIndex
            // 
            ColIndex.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ColIndex.HeaderText = "序号";
            ColIndex.Name = "ColIndex";
            ColIndex.ReadOnly = true;
            // 
            // ColCellAddress
            // 
            ColCellAddress.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ColCellAddress.HeaderText = "单元格地址";
            ColCellAddress.Name = "ColCellAddress";
            ColCellAddress.Width = 150;
            // 
            // ColVarText
            // 
            ColVarText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColVarText.HeaderText = "内容 (点击配置)";
            ColVarText.Name = "ColVarText";
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(248, 249, 250);
            panelButtons.Controls.Add(btnMoveDown);
            panelButtons.Controls.Add(btnMoveUp);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnAdd);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(757, 10);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(10, 5, 10, 5);
            panelButtons.Size = new Size(114, 357);
            panelButtons.TabIndex = 0;
            // 
            // btnMoveDown
            // 
            btnMoveDown.Cursor = Cursors.Hand;
            btnMoveDown.FillColor = Color.FromArgb(40, 167, 69);
            btnMoveDown.FillColor2 = Color.FromArgb(40, 167, 69);
            btnMoveDown.Font = new Font("微软雅黑", 11F);
            btnMoveDown.Location = new Point(13, 172);
            btnMoveDown.MinimumSize = new Size(1, 1);
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.RectColor = Color.FromArgb(40, 167, 69);
            btnMoveDown.RectDisableColor = Color.FromArgb(40, 167, 69);
            btnMoveDown.Size = new Size(90, 38);
            btnMoveDown.Style = UIStyle.Custom;
            btnMoveDown.Symbol = 61703;
            btnMoveDown.SymbolSize = 20;
            btnMoveDown.TabIndex = 7;
            btnMoveDown.Text = "下移";
            btnMoveDown.TipsFont = new Font("微软雅黑", 11F);
            // 
            // btnMoveUp
            // 
            btnMoveUp.BackColor = Color.Transparent;
            btnMoveUp.Cursor = Cursors.Hand;
            btnMoveUp.FillColor = Color.FromArgb(40, 167, 69);
            btnMoveUp.FillColor2 = Color.FromArgb(40, 167, 69);
            btnMoveUp.Font = new Font("微软雅黑", 11F);
            btnMoveUp.Location = new Point(13, 122);
            btnMoveUp.MinimumSize = new Size(1, 1);
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.RectColor = Color.FromArgb(40, 167, 69);
            btnMoveUp.RectDisableColor = Color.FromArgb(40, 167, 69);
            btnMoveUp.Size = new Size(90, 38);
            btnMoveUp.Style = UIStyle.Custom;
            btnMoveUp.Symbol = 61702;
            btnMoveUp.SymbolSize = 20;
            btnMoveUp.TabIndex = 6;
            btnMoveUp.Text = "上移";
            btnMoveUp.TipsFont = new Font("微软雅黑", 11F);
            // 
            // btnDelete
            // 
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FillColor = Color.FromArgb(220, 53, 69);
            btnDelete.FillColor2 = Color.FromArgb(220, 53, 69);
            btnDelete.FillDisableColor = Color.FromArgb(220, 53, 69);
            btnDelete.Font = new Font("微软雅黑", 11F);
            btnDelete.Location = new Point(13, 72);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectColor = Color.FromArgb(220, 53, 69);
            btnDelete.RectDisableColor = Color.FromArgb(220, 53, 69);
            btnDelete.Size = new Size(90, 38);
            btnDelete.Style = UIStyle.Custom;
            btnDelete.Symbol = 61460;
            btnDelete.SymbolSize = 20;
            btnDelete.TabIndex = 5;
            btnDelete.Text = "删除";
            btnDelete.TipsFont = new Font("微软雅黑", 11F);
            // 
            // btnAdd
            // 
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FillColor = Color.DodgerBlue;
            btnAdd.FillColor2 = Color.DodgerBlue;
            btnAdd.Font = new Font("微软雅黑", 11F);
            btnAdd.Location = new Point(13, 22);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.RectColor = Color.DodgerBlue;
            btnAdd.RectDisableColor = Color.DodgerBlue;
            btnAdd.Size = new Size(90, 38);
            btnAdd.Symbol = 61543;
            btnAdd.SymbolSize = 20;
            btnAdd.TabIndex = 4;
            btnAdd.Text = "添加";
            btnAdd.TipsFont = new Font("微软雅黑", 11F);
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(248, 249, 250);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 484);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(15, 10, 15, 10);
            panelBottom.Size = new Size(888, 70);
            panelBottom.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.White;
            btnCancel.FillHoverColor = Color.FromArgb(235, 243, 255);
            btnCancel.FillPressColor = Color.FromArgb(235, 243, 255);
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.ForeColor = Color.FromArgb(65, 100, 204);
            btnCancel.Location = new Point(758, 18);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(65, 100, 204);
            btnCancel.RectHoverColor = Color.FromArgb(65, 100, 204);
            btnCancel.RectPressColor = Color.FromArgb(65, 100, 204);
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.FromArgb(65, 100, 204);
            btnSave.FillHoverColor = Color.FromArgb(80, 120, 220);
            btnSave.FillPressColor = Color.FromArgb(50, 80, 180);
            btnSave.Font = new Font("微软雅黑", 10F);
            btnSave.Location = new Point(638, 18);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectColor = Color.FromArgb(65, 100, 204);
            btnSave.RectHoverColor = Color.FromArgb(80, 120, 220);
            btnSave.RectPressColor = Color.FromArgb(50, 80, 180);
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 0;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            // 
            // Form_WriteCells
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(888, 554);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelFileConfig);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_WriteCells";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Text = "写入报表单元格";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1000, 680);
            panelFileConfig.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            panelButtons.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFileConfig;
        private Sunny.UI.UILabel lblSheetName;
        private Sunny.UI.UITextBox txtSheetName;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView DataGridViewDefineVar;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelBottom;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UIButton btnCancel;
        private UISymbolButton btnMoveDown;
        private UISymbolButton btnMoveUp;
        private UISymbolButton btnDelete;
        private UISymbolButton btnAdd;
        private DataGridViewTextBoxColumn ColIndex;
        private DataGridViewTextBoxColumn ColCellAddress;
        private DataGridViewTextBoxColumn ColVarText;
    }
}
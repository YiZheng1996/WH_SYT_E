namespace MainUI.LogicalConfiguration.Forms
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panelDescription = new Panel();
            chkEnabled = new UICheckBox();
            txtDescription = new UITextBox();
            lblDescription = new UILabel();
            panelMain = new Panel();
            DataGridViewPLCList = new DataGridView();
            ColIndex = new DataGridViewTextBoxColumn();
            ColPLCModule = new DataGridViewComboBoxColumn();
            ColPLCAddress = new DataGridViewComboBoxColumn();
            ColWriteValue = new DataGridViewTextBoxColumn();
            ColDescription = new DataGridViewTextBoxColumn();
            panelButtons = new Panel();
            btnMoveDown = new UISymbolButton();
            btnMoveUp = new UISymbolButton();
            btnDelete = new UISymbolButton();
            btnAdd = new UISymbolButton();
            panelBottom = new Panel();
            btnHelp = new UIButton();
            btnCancel = new UIButton();
            btnSave = new UIButton();
            panelDescription.SuspendLayout();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewPLCList).BeginInit();
            panelButtons.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelDescription
            // 
            panelDescription.BackColor = Color.White;
            panelDescription.Controls.Add(chkEnabled);
            panelDescription.Controls.Add(txtDescription);
            panelDescription.Controls.Add(lblDescription);
            panelDescription.Dock = DockStyle.Top;
            panelDescription.Location = new Point(0, 35);
            panelDescription.Name = "panelDescription";
            panelDescription.Padding = new Padding(15, 10, 15, 10);
            panelDescription.Size = new Size(1000, 70);
            panelDescription.TabIndex = 1;
            // 
            // chkEnabled
            // 
            chkEnabled.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkEnabled.CheckBoxSize = 18;
            chkEnabled.Checked = true;
            chkEnabled.Font = new Font("微软雅黑", 10F);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(800, 15);
            chkEnabled.MinimumSize = new Size(1, 1);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(150, 30);
            chkEnabled.TabIndex = 2;
            chkEnabled.Text = "启用此步骤";
            // 
            // txtDescription
            // 
            txtDescription.Cursor = Cursors.IBeam;
            txtDescription.Font = new Font("微软雅黑", 10F);
            txtDescription.Location = new Point(124, 15);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.RectColor = Color.FromArgb(65, 100, 204);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(650, 30);
            txtDescription.TabIndex = 1;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "请输入步骤描述信息";
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("微软雅黑", 10F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(18, 15);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(100, 25);
            lblDescription.TabIndex = 0;
            lblDescription.Text = "步骤描述:";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.BorderStyle = BorderStyle.FixedSingle;
            panelMain.Controls.Add(DataGridViewPLCList);
            panelMain.Controls.Add(panelButtons);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 105);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15, 10, 15, 10);
            panelMain.Size = new Size(1000, 515);
            panelMain.TabIndex = 2;
            // 
            // DataGridViewPLCList
            // 
            DataGridViewPLCList.AllowDrop = true;
            DataGridViewPLCList.AllowUserToResizeRows = false;
            DataGridViewPLCList.BackgroundColor = Color.White;
            DataGridViewPLCList.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(65, 100, 204);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            DataGridViewPLCList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewPLCList.ColumnHeadersHeight = 40;
            DataGridViewPLCList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewPLCList.Columns.AddRange(new DataGridViewColumn[] { ColIndex, ColPLCModule, ColPLCAddress, ColWriteValue, ColDescription });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 236, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewPLCList.DefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewPLCList.Dock = DockStyle.Fill;
            DataGridViewPLCList.EnableHeadersVisualStyles = false;
            DataGridViewPLCList.GridColor = Color.FromArgb(230, 230, 230);
            DataGridViewPLCList.Location = new Point(15, 10);
            DataGridViewPLCList.MultiSelect = false;
            DataGridViewPLCList.Name = "DataGridViewPLCList";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(243, 249, 255);
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 9F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            DataGridViewPLCList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewPLCList.RowHeadersWidth = 45;
            dataGridViewCellStyle5.Font = new Font("微软雅黑", 11F);
            dataGridViewCellStyle5.Padding = new Padding(3);
            DataGridViewPLCList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            DataGridViewPLCList.RowTemplate.Height = 32;
            DataGridViewPLCList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewPLCList.Size = new Size(853, 493);
            DataGridViewPLCList.TabIndex = 0;
            // 
            // ColIndex
            // 
            ColIndex.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(248, 249, 250);
            ColIndex.DefaultCellStyle = dataGridViewCellStyle2;
            ColIndex.HeaderText = "序号";
            ColIndex.Name = "ColIndex";
            ColIndex.ReadOnly = true;
            ColIndex.Width = 60;
            // 
            // ColPLCModule
            // 
            ColPLCModule.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColPLCModule.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            ColPLCModule.FlatStyle = FlatStyle.Flat;
            ColPLCModule.HeaderText = "PLC模块";
            ColPLCModule.Name = "ColPLCModule";
            // 
            // ColPLCAddress
            // 
            ColPLCAddress.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColPLCAddress.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            ColPLCAddress.FillWeight = 120F;
            ColPLCAddress.FlatStyle = FlatStyle.Flat;
            ColPLCAddress.HeaderText = "PLC地址";
            ColPLCAddress.Name = "ColPLCAddress";
            // 
            // ColWriteValue
            // 
            ColWriteValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColWriteValue.FillWeight = 120F;
            ColWriteValue.HeaderText = "写入值（支持 {变量名}）";
            ColWriteValue.Name = "ColWriteValue";
            // 
            // ColDescription
            // 
            ColDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColDescription.HeaderText = "描述";
            ColDescription.Name = "ColDescription";
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(248, 249, 250);
            panelButtons.BorderStyle = BorderStyle.FixedSingle;
            panelButtons.Controls.Add(btnMoveDown);
            panelButtons.Controls.Add(btnMoveUp);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnAdd);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(868, 10);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(10, 15, 10, 15);
            panelButtons.Size = new Size(115, 493);
            panelButtons.TabIndex = 1;
            // 
            // btnMoveDown
            // 
            btnMoveDown.Cursor = Cursors.Hand;
            btnMoveDown.FillColor = Color.FromArgb(40, 167, 69);
            btnMoveDown.FillColor2 = Color.FromArgb(40, 167, 69);
            btnMoveDown.Font = new Font("微软雅黑", 11F);
            btnMoveDown.Location = new Point(12, 170);
            btnMoveDown.MinimumSize = new Size(1, 1);
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.RectColor = Color.FromArgb(40, 167, 69);
            btnMoveDown.RectDisableColor = Color.FromArgb(40, 167, 69);
            btnMoveDown.Size = new Size(90, 38);
            btnMoveDown.Style = UIStyle.Custom;
            btnMoveDown.Symbol = 61703;
            btnMoveDown.SymbolSize = 20;
            btnMoveDown.TabIndex = 3;
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
            btnMoveUp.Location = new Point(12, 120);
            btnMoveUp.MinimumSize = new Size(1, 1);
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.RectColor = Color.FromArgb(40, 167, 69);
            btnMoveUp.RectDisableColor = Color.FromArgb(40, 167, 69);
            btnMoveUp.Size = new Size(90, 38);
            btnMoveUp.Style = UIStyle.Custom;
            btnMoveUp.Symbol = 61702;
            btnMoveUp.SymbolSize = 20;
            btnMoveUp.TabIndex = 2;
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
            btnDelete.Location = new Point(12, 70);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectColor = Color.FromArgb(220, 53, 69);
            btnDelete.RectDisableColor = Color.FromArgb(220, 53, 69);
            btnDelete.Size = new Size(90, 38);
            btnDelete.Style = UIStyle.Custom;
            btnDelete.Symbol = 61460;
            btnDelete.SymbolSize = 20;
            btnDelete.TabIndex = 1;
            btnDelete.Text = "删除";
            btnDelete.TipsFont = new Font("微软雅黑", 11F);
            // 
            // btnAdd
            // 
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FillColor = Color.DodgerBlue;
            btnAdd.FillColor2 = Color.DodgerBlue;
            btnAdd.Font = new Font("微软雅黑", 11F);
            btnAdd.Location = new Point(12, 20);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.RectColor = Color.DodgerBlue;
            btnAdd.RectDisableColor = Color.DodgerBlue;
            btnAdd.Size = new Size(90, 38);
            btnAdd.Symbol = 61543;
            btnAdd.SymbolSize = 20;
            btnAdd.TabIndex = 0;
            btnAdd.Text = "添加";
            btnAdd.TipsFont = new Font("微软雅黑", 11F);
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(248, 249, 250);
            panelBottom.Controls.Add(btnHelp);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 620);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(15, 10, 15, 10);
            panelBottom.Size = new Size(1000, 70);
            panelBottom.TabIndex = 3;
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.FillColor = Color.White;
            btnHelp.Font = new Font("微软雅黑", 11F);
            btnHelp.ForeColor = Color.FromArgb(65, 100, 204);
            btnHelp.Location = new Point(20, 15);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.RectColor = Color.FromArgb(65, 100, 204);
            btnHelp.Size = new Size(80, 40);
            btnHelp.Style = UIStyle.Custom;
            btnHelp.StyleCustomMode = true;
            btnHelp.TabIndex = 3;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("微软雅黑", 11F);
            btnCancel.ForeColor = Color.FromArgb(48, 48, 48);
            btnCancel.Location = new Point(866, 15);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(220, 220, 220);
            btnCancel.Size = new Size(100, 40);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.StyleCustomMode = true;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.Font = new Font("微软雅黑", 11F);
            btnSave.Location = new Point(746, 15);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // Form_WritePLC
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1000, 690);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelDescription);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_WritePLC";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Text = "PLC 写入配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1000, 690);
            panelDescription.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewPLCList).EndInit();
            panelButtons.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panelDescription;
        private Sunny.UI.UILabel lblDescription;
        private Sunny.UI.UITextBox txtDescription;
        private Sunny.UI.UICheckBox chkEnabled;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView DataGridViewPLCList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColIndex;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColPLCModule;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColPLCAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColWriteValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
        private System.Windows.Forms.Panel panelButtons;
        private Sunny.UI.UISymbolButton btnAdd;
        private Sunny.UI.UISymbolButton btnDelete;
        private Sunny.UI.UISymbolButton btnMoveUp;
        private Sunny.UI.UISymbolButton btnMoveDown;
        private System.Windows.Forms.Panel panelBottom;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnHelp;
    }
}
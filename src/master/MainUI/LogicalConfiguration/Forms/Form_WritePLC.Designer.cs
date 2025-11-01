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
                _validationTimer?.Stop();
                _validationTimer?.Dispose();
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
            pnlMain = new Panel();
            pnlContent = new Panel();
            DataGridViewPLCList = new Sunny.UI.UIDataGridView();
            pnlToolbar = new Panel();
            btnHelp = new Button();
            btnTest = new Button();
            btnMoveDown = new Button();
            btnMoveUp = new Button();
            btnDelete = new Button();
            btnAdd = new Button();
            pnlHeader = new Panel();
            chkEnabled = new CheckBox();
            txtDescription = new TextBox();
            lblDescription = new Label();
            pnlBottom = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            lblInfo = new Label();
            pnlMain.SuspendLayout();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewPLCList).BeginInit();
            pnlToolbar.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlToolbar);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Controls.Add(pnlBottom);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 35);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(10);
            pnlMain.Size = new Size(900, 565);
            pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(DataGridViewPLCList);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(10, 130);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(0, 5, 0, 5);
            pnlContent.Size = new Size(880, 355);
            pnlContent.TabIndex = 2;
            // 
            // DataGridViewPLCList
            // 
            DataGridViewPLCList.AllowDrop = true;
            DataGridViewPLCList.BackgroundColor = Color.White;
            DataGridViewPLCList.BorderStyle = BorderStyle.Fixed3D;
            DataGridViewPLCList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewPLCList.Dock = DockStyle.Fill;
            DataGridViewPLCList.Location = new Point(0, 5);
            DataGridViewPLCList.MultiSelect = false;
            DataGridViewPLCList.Name = "DataGridViewPLCList";
            DataGridViewPLCList.RowHeadersWidth = 51;
            DataGridViewPLCList.RowTemplate.Height = 27;
            DataGridViewPLCList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewPLCList.Size = new Size(880, 345);
            DataGridViewPLCList.TabIndex = 0;
            // 
            // pnlToolbar
            // 
            pnlToolbar.Controls.Add(btnHelp);
            pnlToolbar.Controls.Add(btnTest);
            pnlToolbar.Controls.Add(btnMoveDown);
            pnlToolbar.Controls.Add(btnMoveUp);
            pnlToolbar.Controls.Add(btnDelete);
            pnlToolbar.Controls.Add(btnAdd);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(10, 80);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new Size(880, 50);
            pnlToolbar.TabIndex = 1;
            // 
            // btnHelp
            // 
            btnHelp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHelp.BackColor = Color.FromArgb(100, 149, 237);
            btnHelp.FlatAppearance.BorderSize = 0;
            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.Font = new Font("微软雅黑", 9F);
            btnHelp.ForeColor = Color.White;
            btnHelp.Location = new Point(785, 10);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(90, 32);
            btnHelp.TabIndex = 5;
            btnHelp.Text = "帮助";
            btnHelp.UseVisualStyleBackColor = false;
            // 
            // btnTest
            // 
            btnTest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTest.BackColor = Color.FromArgb(255, 140, 0);
            btnTest.FlatAppearance.BorderSize = 0;
            btnTest.FlatStyle = FlatStyle.Flat;
            btnTest.Font = new Font("微软雅黑", 9F);
            btnTest.ForeColor = Color.White;
            btnTest.Location = new Point(685, 10);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(90, 32);
            btnTest.TabIndex = 4;
            btnTest.Text = "测试连接";
            btnTest.UseVisualStyleBackColor = false;
            // 
            // btnMoveDown
            // 
            btnMoveDown.BackColor = Color.FromArgb(70, 130, 180);
            btnMoveDown.FlatAppearance.BorderSize = 0;
            btnMoveDown.FlatStyle = FlatStyle.Flat;
            btnMoveDown.Font = new Font("微软雅黑", 9F);
            btnMoveDown.ForeColor = Color.White;
            btnMoveDown.Location = new Point(310, 10);
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.Size = new Size(90, 32);
            btnMoveDown.TabIndex = 3;
            btnMoveDown.Text = "下移 ↓";
            btnMoveDown.UseVisualStyleBackColor = false;
            // 
            // btnMoveUp
            // 
            btnMoveUp.BackColor = Color.FromArgb(70, 130, 180);
            btnMoveUp.FlatAppearance.BorderSize = 0;
            btnMoveUp.FlatStyle = FlatStyle.Flat;
            btnMoveUp.Font = new Font("微软雅黑", 9F);
            btnMoveUp.ForeColor = Color.White;
            btnMoveUp.Location = new Point(210, 10);
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.Size = new Size(90, 32);
            btnMoveUp.TabIndex = 2;
            btnMoveUp.Text = "上移 ↑";
            btnMoveUp.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("微软雅黑", 9F);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(110, 10);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 32);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "删除";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(40, 167, 69);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("微软雅黑", 9F);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(10, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 32);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "添加";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(chkEnabled);
            pnlHeader.Controls.Add(txtDescription);
            pnlHeader.Controls.Add(lblDescription);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(10, 10);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(880, 70);
            pnlHeader.TabIndex = 0;
            // 
            // chkEnabled
            // 
            chkEnabled.AutoSize = true;
            chkEnabled.Checked = true;
            chkEnabled.CheckState = CheckState.Checked;
            chkEnabled.Font = new Font("微软雅黑", 9F);
            chkEnabled.Location = new Point(10, 42);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(87, 21);
            chkEnabled.TabIndex = 2;
            chkEnabled.Text = "启用此步骤";
            chkEnabled.UseVisualStyleBackColor = true;
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Font = new Font("微软雅黑", 9F);
            txtDescription.Location = new Point(100, 8);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(770, 23);
            txtDescription.TabIndex = 1;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("微软雅黑", 9F);
            lblDescription.Location = new Point(10, 11);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(68, 17);
            lblDescription.TabIndex = 0;
            lblDescription.Text = "步骤描述：";
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Controls.Add(lblInfo);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(10, 485);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(880, 70);
            pnlBottom.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(760, 20);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 40);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.BackColor = Color.FromArgb(0, 123, 255);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("微软雅黑", 10F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(630, 20);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 40);
            btnSave.TabIndex = 1;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("微软雅黑", 9F);
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(10, 30);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(324, 17);
            lblInfo.TabIndex = 0;
            lblInfo.Text = "提示：支持使用 {变量名} 引用全局变量，支持拖拽调整顺序";
            // 
            // Form_WritePLC
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(900, 600);
            Controls.Add(pnlMain);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_WritePLC";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "PLC写入配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 900, 600);
            pnlMain.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewPLCList).EndInit();
            pnlToolbar.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Panel pnlContent;
        private Sunny.UI.UIDataGridView DataGridViewPLCList;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
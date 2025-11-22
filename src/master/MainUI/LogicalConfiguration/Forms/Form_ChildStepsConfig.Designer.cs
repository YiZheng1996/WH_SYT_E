namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_ChildStepsConfig
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ChildStepsConfig));

            this.pnlMain = new Sunny.UI.UIPanel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlToolBox = new Sunny.UI.UIPanel();
            this.lblToolBoxTitle = new Sunny.UI.UILabel();
            this.treeViewTools = new System.Windows.Forms.TreeView();
            this.pnlStepList = new Sunny.UI.UIPanel();
            this.dgvSteps = new Sunny.UI.UIDataGridView();
            this.ColIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStepName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlStepButtons = new Sunny.UI.UIPanel();
            this.btnDelete = new Sunny.UI.UISymbolButton();
            this.btnMoveDown = new Sunny.UI.UISymbolButton();
            this.btnMoveUp = new Sunny.UI.UISymbolButton();
            this.btnEdit = new Sunny.UI.UISymbolButton();
            this.btnAdd = new Sunny.UI.UISymbolButton();
            this.lblStepListTitle = new Sunny.UI.UILabel();
            this.pnlButtons = new Sunny.UI.UIPanel();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnSave = new Sunny.UI.UISymbolButton();

            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlToolBox.SuspendLayout();
            this.pnlStepList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSteps)).BeginInit();
            this.pnlStepButtons.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.pnlMain.Location = new System.Drawing.Point(0, 35);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMain.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Radius = 0;
            this.pnlMain.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.pnlMain.Size = new System.Drawing.Size(1200, 585);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.Text = null;
            this.pnlMain.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(10, 10);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pnlToolBox);
            this.splitContainer.Panel1MinSize = 250;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlStepList);
            this.splitContainer.Panel2MinSize = 600;
            this.splitContainer.Size = new System.Drawing.Size(1180, 565);
            this.splitContainer.SplitterDistance = 280;
            this.splitContainer.TabIndex = 0;

            // 
            // pnlToolBox
            // 
            this.pnlToolBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlToolBox.Controls.Add(this.treeViewTools);
            this.pnlToolBox.Controls.Add(this.lblToolBoxTitle);
            this.pnlToolBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolBox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.pnlToolBox.Location = new System.Drawing.Point(0, 0);
            this.pnlToolBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlToolBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlToolBox.Name = "pnlToolBox";
            this.pnlToolBox.Padding = new System.Windows.Forms.Padding(5);
            this.pnlToolBox.Radius = 5;
            this.pnlToolBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.pnlToolBox.Size = new System.Drawing.Size(280, 565);
            this.pnlToolBox.TabIndex = 0;
            this.pnlToolBox.Text = null;
            this.pnlToolBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblToolBoxTitle
            // 
            this.lblToolBoxTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblToolBoxTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblToolBoxTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.lblToolBoxTitle.Location = new System.Drawing.Point(5, 5);
            this.lblToolBoxTitle.Name = "lblToolBoxTitle";
            this.lblToolBoxTitle.Size = new System.Drawing.Size(270, 35);
            this.lblToolBoxTitle.TabIndex = 0;
            this.lblToolBoxTitle.Text = "工具箱";
            this.lblToolBoxTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // treeViewTools
            // 
            this.treeViewTools.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTools.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.treeViewTools.ItemHeight = 28;
            this.treeViewTools.Location = new System.Drawing.Point(5, 40);
            this.treeViewTools.Name = "treeViewTools";
            this.treeViewTools.Size = new System.Drawing.Size(270, 520);
            this.treeViewTools.TabIndex = 1;
            this.treeViewTools.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeViewTools_ItemDrag);

            // 
            // pnlStepList
            // 
            this.pnlStepList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlStepList.Controls.Add(this.dgvSteps);
            this.pnlStepList.Controls.Add(this.pnlStepButtons);
            this.pnlStepList.Controls.Add(this.lblStepListTitle);
            this.pnlStepList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStepList.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.pnlStepList.Location = new System.Drawing.Point(0, 0);
            this.pnlStepList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlStepList.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlStepList.Name = "pnlStepList";
            this.pnlStepList.Padding = new System.Windows.Forms.Padding(5);
            this.pnlStepList.Radius = 5;
            this.pnlStepList.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.pnlStepList.Size = new System.Drawing.Size(896, 565);
            this.pnlStepList.TabIndex = 0;
            this.pnlStepList.Text = null;
            this.pnlStepList.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblStepListTitle
            // 
            this.lblStepListTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStepListTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblStepListTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.lblStepListTitle.Location = new System.Drawing.Point(5, 5);
            this.lblStepListTitle.Name = "lblStepListTitle";
            this.lblStepListTitle.Size = new System.Drawing.Size(886, 35);
            this.lblStepListTitle.TabIndex = 0;
            this.lblStepListTitle.Text = "循环体步骤列表";
            this.lblStepListTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // dgvSteps
            // 
            this.dgvSteps.AllowUserToAddRows = false;
            this.dgvSteps.AllowUserToDeleteRows = false;
            this.dgvSteps.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dgvSteps.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSteps.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dgvSteps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSteps.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSteps.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSteps.ColumnHeadersHeight = 35;
            this.dgvSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSteps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColIndex,
            this.ColStepName,
            this.ColRemark});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSteps.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSteps.EnableHeadersVisualStyles = false;
            this.dgvSteps.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dgvSteps.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(173)))), ((int)(((byte)(255)))));
            this.dgvSteps.Location = new System.Drawing.Point(5, 40);
            this.dgvSteps.MultiSelect = false;
            this.dgvSteps.Name = "dgvSteps";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSteps.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSteps.RowHeadersVisible = false;
            this.dgvSteps.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dgvSteps.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSteps.RowTemplate.Height = 29;
            this.dgvSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSteps.Size = new System.Drawing.Size(886, 465);
            this.dgvSteps.TabIndex = 1;
            this.dgvSteps.AllowDrop = true;
            this.dgvSteps.DragDrop += new System.Windows.Forms.DragEventHandler(this.DgvSteps_DragDrop);
            this.dgvSteps.DragEnter += new System.Windows.Forms.DragEventHandler(this.DgvSteps_DragEnter);
            this.dgvSteps.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSteps_CellDoubleClick);
            this.dgvSteps.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvSteps_CellBeginEdit);
            this.dgvSteps.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSteps_CellEndEdit);

            // 
            // ColIndex
            // 
            this.ColIndex.HeaderText = "序号";
            this.ColIndex.MinimumWidth = 6;
            this.ColIndex.Name = "ColIndex";
            this.ColIndex.ReadOnly = true;
            this.ColIndex.Width = 80;

            // 
            // ColStepName
            // 
            this.ColStepName.HeaderText = "步骤名称";
            this.ColStepName.MinimumWidth = 6;
            this.ColStepName.Name = "ColStepName";
            this.ColStepName.ReadOnly = true;
            this.ColStepName.Width = 250;

            // 
            // ColRemark
            // 
            this.ColRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColRemark.HeaderText = "备注";
            this.ColRemark.MinimumWidth = 6;
            this.ColRemark.Name = "ColRemark";

            // 
            // pnlStepButtons
            // 
            this.pnlStepButtons.Controls.Add(this.btnDelete);
            this.pnlStepButtons.Controls.Add(this.btnMoveDown);
            this.pnlStepButtons.Controls.Add(this.btnMoveUp);
            this.pnlStepButtons.Controls.Add(this.btnEdit);
            this.pnlStepButtons.Controls.Add(this.btnAdd);
            this.pnlStepButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStepButtons.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.pnlStepButtons.Location = new System.Drawing.Point(5, 505);
            this.pnlStepButtons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlStepButtons.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlStepButtons.Name = "pnlStepButtons";
            this.pnlStepButtons.Padding = new System.Windows.Forms.Padding(5);
            this.pnlStepButtons.Radius = 0;
            this.pnlStepButtons.RectColor = System.Drawing.Color.Transparent;
            this.pnlStepButtons.Size = new System.Drawing.Size(886, 55);
            this.pnlStepButtons.TabIndex = 2;
            this.pnlStepButtons.Text = null;
            this.pnlStepButtons.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnAdd.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnAdd.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnAdd.Location = new System.Drawing.Point(10, 10);
            this.btnAdd.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Radius = 5;
            this.btnAdd.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnAdd.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnAdd.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.Symbol = 61543;
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "添加";
            this.btnAdd.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            // 
            // btnEdit
            // 
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnEdit.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnEdit.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnEdit.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnEdit.Location = new System.Drawing.Point(120, 10);
            this.btnEdit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Radius = 5;
            this.btnEdit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnEdit.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnEdit.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnEdit.Size = new System.Drawing.Size(100, 35);
            this.btnEdit.Symbol = 61508;
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "编辑";
            this.btnEdit.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);

            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoveUp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnMoveUp.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnMoveUp.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnMoveUp.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnMoveUp.Location = new System.Drawing.Point(340, 10);
            this.btnMoveUp.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Radius = 5;
            this.btnMoveUp.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnMoveUp.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnMoveUp.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnMoveUp.Size = new System.Drawing.Size(100, 35);
            this.btnMoveUp.Symbol = 61537;
            this.btnMoveUp.TabIndex = 2;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMoveUp.Click += new System.EventHandler(this.BtnMoveUp_Click);

            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoveDown.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnMoveDown.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnMoveDown.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnMoveDown.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnMoveDown.Location = new System.Drawing.Point(450, 10);
            this.btnMoveDown.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Radius = 5;
            this.btnMoveDown.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnMoveDown.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnMoveDown.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnMoveDown.Size = new System.Drawing.Size(100, 35);
            this.btnMoveDown.Symbol = 61539;
            this.btnMoveDown.TabIndex = 3;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMoveDown.Click += new System.EventHandler(this.BtnMoveDown_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnDelete.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.btnDelete.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnDelete.Location = new System.Drawing.Point(230, 10);
            this.btnDelete.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Radius = 5;
            this.btnDelete.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnDelete.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.Symbol = 61460;
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.pnlButtons.Location = new System.Drawing.Point(0, 620);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlButtons.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(10);
            this.pnlButtons.Radius = 0;
            this.pnlButtons.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.pnlButtons.Size = new System.Drawing.Size(1200, 60);
            this.pnlButtons.TabIndex = 1;
            this.pnlButtons.Text = null;
            this.pnlButtons.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnSave.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnSave.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnSave.Location = new System.Drawing.Point(820, 12);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Radius = 5;
            this.btnSave.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnSave.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnSave.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.btnSave.Size = new System.Drawing.Size(150, 36);
            this.btnSave.Symbol = 61639;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "确定";
            this.btnSave.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.btnCancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnCancel.Location = new System.Drawing.Point(1020, 12);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Radius = 5;
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.btnCancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            this.btnCancel.Size = new System.Drawing.Size(150, 36);
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // 
            // Form_ChildStepsConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 680);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_ChildStepsConfig";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "循环体步骤配置";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 1200, 680);

            this.pnlMain.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlToolBox.ResumeLayout(false);
            this.pnlStepList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSteps)).EndInit();
            this.pnlStepButtons.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        #region 控件声明

        private Sunny.UI.UIPanel pnlMain;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Sunny.UI.UIPanel pnlToolBox;
        private Sunny.UI.UILabel lblToolBoxTitle;
        private System.Windows.Forms.TreeView treeViewTools;
        private Sunny.UI.UIPanel pnlStepList;
        private Sunny.UI.UILabel lblStepListTitle;
        private Sunny.UI.UIDataGridView dgvSteps;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStepName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRemark;
        private Sunny.UI.UIPanel pnlStepButtons;
        private Sunny.UI.UISymbolButton btnAdd;
        private Sunny.UI.UISymbolButton btnEdit;
        private Sunny.UI.UISymbolButton btnMoveUp;
        private Sunny.UI.UISymbolButton btnMoveDown;
        private Sunny.UI.UISymbolButton btnDelete;
        private Sunny.UI.UIPanel pnlButtons;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UISymbolButton btnCancel;

        #endregion
    }
}
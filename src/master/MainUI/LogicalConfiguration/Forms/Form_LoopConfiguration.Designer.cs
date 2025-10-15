namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class Form_LoopConfiguration
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
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
            pnlMain = new UIPanel();
            grpLoopType = new UIGroupBox();
            rbForLoop = new UIRadioButton();
            rbWhileLoop = new UIRadioButton();
            rbForeachLoop = new UIRadioButton();
            pnlForLoop = new UIPanel();
            lblForVar = new UILabel();
            txtForVar = new UITextBox();
            lblForStart = new UILabel();
            txtForStart = new UITextBox();
            lblForEnd = new UILabel();
            txtForEnd = new UITextBox();
            lblForStep = new UILabel();
            txtForStep = new UITextBox();
            pnlWhileLoop = new UIPanel();
            lblWhileCondition = new UILabel();
            cmbWhileVar = new UIComboBox();
            cmbWhileOperator = new UIComboBox();
            txtWhileValue = new UITextBox();
            lblMaxIterations = new UILabel();
            nudMaxIterations = new UIIntegerUpDown();
            pnlForeachLoop = new UIPanel();
            lblForeachArray = new UILabel();
            cmbForeachArray = new UIComboBox();
            lblForeachItem = new UILabel();
            txtForeachItem = new UITextBox();
            grpLoopBody = new UIGroupBox();
            dgvLoopSteps = new UIDataGridView();
            btnAddStep = new UIButton();
            btnRemoveStep = new UIButton();
            btnEditStep = new UIButton();
            btnMoveUp = new UIButton();
            btnMoveDown = new UIButton();
            pnlButtons = new UIPanel();
            btnTest = new UIButton();
            btnOK = new UIButton();
            btnCancel = new UIButton();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            pnlMain.SuspendLayout();
            grpLoopType.SuspendLayout();
            pnlForLoop.SuspendLayout();
            pnlWhileLoop.SuspendLayout();
            pnlForeachLoop.SuspendLayout();
            grpLoopBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLoopSteps).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(grpLoopType);
            pnlMain.Controls.Add(pnlForLoop);
            pnlMain.Controls.Add(pnlWhileLoop);
            pnlMain.Controls.Add(pnlForeachLoop);
            pnlMain.Controls.Add(grpLoopBody);
            pnlMain.Controls.Add(pnlButtons);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.White;
            pnlMain.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlMain.Location = new Point(0, 35);
            pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pnlMain.MinimumSize = new Size(1, 1);
            pnlMain.Name = "pnlMain";
            pnlMain.Radius = 0;
            pnlMain.Size = new Size(800, 585);
            pnlMain.TabIndex = 0;
            pnlMain.Text = null;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // grpLoopType
            // 
            grpLoopType.Controls.Add(rbForLoop);
            grpLoopType.Controls.Add(rbWhileLoop);
            grpLoopType.Controls.Add(rbForeachLoop);
            grpLoopType.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpLoopType.Location = new Point(12, 12);
            grpLoopType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpLoopType.MinimumSize = new Size(1, 1);
            grpLoopType.Name = "grpLoopType";
            grpLoopType.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpLoopType.Size = new Size(760, 80);
            grpLoopType.TabIndex = 0;
            grpLoopType.Text = "循环类型";
            grpLoopType.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // rbForLoop
            // 
            rbForLoop.Checked = true;
            rbForLoop.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            rbForLoop.Location = new Point(20, 30);
            rbForLoop.MinimumSize = new Size(1, 1);
            rbForLoop.Name = "rbForLoop";
            rbForLoop.Size = new Size(120, 30);
            rbForLoop.TabIndex = 0;
            rbForLoop.Text = "For循环";
            rbForLoop.CheckedChanged += RbLoopType_CheckedChanged;
            // 
            // rbWhileLoop
            // 
            rbWhileLoop.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            rbWhileLoop.Location = new Point(160, 30);
            rbWhileLoop.MinimumSize = new Size(1, 1);
            rbWhileLoop.Name = "rbWhileLoop";
            rbWhileLoop.Size = new Size(120, 30);
            rbWhileLoop.TabIndex = 1;
            rbWhileLoop.Text = "While循环";
            rbWhileLoop.CheckedChanged += RbLoopType_CheckedChanged;
            // 
            // rbForeachLoop
            // 
            rbForeachLoop.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            rbForeachLoop.Location = new Point(300, 30);
            rbForeachLoop.MinimumSize = new Size(1, 1);
            rbForeachLoop.Name = "rbForeachLoop";
            rbForeachLoop.Size = new Size(120, 30);
            rbForeachLoop.TabIndex = 2;
            rbForeachLoop.Text = "Foreach循环";
            rbForeachLoop.CheckedChanged += RbLoopType_CheckedChanged;
            // 
            // pnlForLoop
            // 
            pnlForLoop.Controls.Add(lblForVar);
            pnlForLoop.Controls.Add(txtForVar);
            pnlForLoop.Controls.Add(lblForStart);
            pnlForLoop.Controls.Add(txtForStart);
            pnlForLoop.Controls.Add(lblForEnd);
            pnlForLoop.Controls.Add(txtForEnd);
            pnlForLoop.Controls.Add(lblForStep);
            pnlForLoop.Controls.Add(txtForStep);
            pnlForLoop.FillColor = Color.FromArgb(248, 248, 248);
            pnlForLoop.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlForLoop.Location = new Point(12, 100);
            pnlForLoop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pnlForLoop.MinimumSize = new Size(1, 1);
            pnlForLoop.Name = "pnlForLoop";
            pnlForLoop.Size = new Size(760, 100);
            pnlForLoop.TabIndex = 1;
            pnlForLoop.Text = null;
            pnlForLoop.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblForVar
            // 
            lblForVar.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblForVar.ForeColor = Color.FromArgb(48, 48, 48);
            lblForVar.Location = new Point(10, 15);
            lblForVar.Name = "lblForVar";
            lblForVar.Size = new Size(80, 25);
            lblForVar.TabIndex = 0;
            lblForVar.Text = "循环变量:";
            // 
            // txtForVar
            // 
            txtForVar.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtForVar.Location = new Point(100, 12);
            txtForVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtForVar.MinimumSize = new Size(1, 16);
            txtForVar.Name = "txtForVar";
            txtForVar.Padding = new System.Windows.Forms.Padding(5);
            txtForVar.ShowText = false;
            txtForVar.Size = new Size(120, 30);
            txtForVar.TabIndex = 1;
            txtForVar.Text = "i";
            txtForVar.TextAlignment = ContentAlignment.MiddleLeft;
            txtForVar.Watermark = "";
            // 
            // lblForStart
            // 
            lblForStart.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblForStart.ForeColor = Color.FromArgb(48, 48, 48);
            lblForStart.Location = new Point(240, 15);
            lblForStart.Name = "lblForStart";
            lblForStart.Size = new Size(60, 25);
            lblForStart.TabIndex = 2;
            lblForStart.Text = "起始值:";
            // 
            // txtForStart
            // 
            txtForStart.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtForStart.Location = new Point(310, 12);
            txtForStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtForStart.MinimumSize = new Size(1, 16);
            txtForStart.Name = "txtForStart";
            txtForStart.Padding = new System.Windows.Forms.Padding(5);
            txtForStart.ShowText = false;
            txtForStart.Size = new Size(80, 30);
            txtForStart.TabIndex = 3;
            txtForStart.Text = "0";
            txtForStart.TextAlignment = ContentAlignment.MiddleLeft;
            txtForStart.Watermark = "";
            // 
            // lblForEnd
            // 
            lblForEnd.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblForEnd.ForeColor = Color.FromArgb(48, 48, 48);
            lblForEnd.Location = new Point(410, 15);
            lblForEnd.Name = "lblForEnd";
            lblForEnd.Size = new Size(60, 25);
            lblForEnd.TabIndex = 4;
            lblForEnd.Text = "结束值:";
            // 
            // txtForEnd
            // 
            txtForEnd.DoubleValue = 10D;
            txtForEnd.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtForEnd.IntValue = 10;
            txtForEnd.Location = new Point(480, 12);
            txtForEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtForEnd.MinimumSize = new Size(1, 16);
            txtForEnd.Name = "txtForEnd";
            txtForEnd.Padding = new System.Windows.Forms.Padding(5);
            txtForEnd.ShowText = false;
            txtForEnd.Size = new Size(80, 30);
            txtForEnd.TabIndex = 5;
            txtForEnd.Text = "10";
            txtForEnd.TextAlignment = ContentAlignment.MiddleLeft;
            txtForEnd.Watermark = "";
            // 
            // lblForStep
            // 
            lblForStep.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblForStep.ForeColor = Color.FromArgb(48, 48, 48);
            lblForStep.Location = new Point(580, 15);
            lblForStep.Name = "lblForStep";
            lblForStep.Size = new Size(60, 25);
            lblForStep.TabIndex = 6;
            lblForStep.Text = "步长:";
            // 
            // txtForStep
            // 
            txtForStep.DoubleValue = 1D;
            txtForStep.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtForStep.IntValue = 1;
            txtForStep.Location = new Point(650, 12);
            txtForStep.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtForStep.MinimumSize = new Size(1, 16);
            txtForStep.Name = "txtForStep";
            txtForStep.Padding = new System.Windows.Forms.Padding(5);
            txtForStep.ShowText = false;
            txtForStep.Size = new Size(80, 30);
            txtForStep.TabIndex = 7;
            txtForStep.Text = "1";
            txtForStep.TextAlignment = ContentAlignment.MiddleLeft;
            txtForStep.Watermark = "";
            // 
            // pnlWhileLoop
            // 
            pnlWhileLoop.Controls.Add(lblWhileCondition);
            pnlWhileLoop.Controls.Add(cmbWhileVar);
            pnlWhileLoop.Controls.Add(cmbWhileOperator);
            pnlWhileLoop.Controls.Add(txtWhileValue);
            pnlWhileLoop.Controls.Add(lblMaxIterations);
            pnlWhileLoop.Controls.Add(nudMaxIterations);
            pnlWhileLoop.FillColor = Color.FromArgb(248, 248, 248);
            pnlWhileLoop.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlWhileLoop.Location = new Point(12, 100);
            pnlWhileLoop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pnlWhileLoop.MinimumSize = new Size(1, 1);
            pnlWhileLoop.Name = "pnlWhileLoop";
            pnlWhileLoop.Size = new Size(760, 100);
            pnlWhileLoop.TabIndex = 2;
            pnlWhileLoop.Text = null;
            pnlWhileLoop.TextAlignment = ContentAlignment.MiddleCenter;
            pnlWhileLoop.Visible = false;
            // 
            // lblWhileCondition
            // 
            lblWhileCondition.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblWhileCondition.ForeColor = Color.FromArgb(48, 48, 48);
            lblWhileCondition.Location = new Point(10, 15);
            lblWhileCondition.Name = "lblWhileCondition";
            lblWhileCondition.Size = new Size(80, 25);
            lblWhileCondition.TabIndex = 0;
            lblWhileCondition.Text = "循环条件:";
            // 
            // cmbWhileVar
            // 
            cmbWhileVar.DataSource = null;
            cmbWhileVar.FillColor = Color.White;
            cmbWhileVar.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbWhileVar.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbWhileVar.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbWhileVar.Location = new Point(100, 12);
            cmbWhileVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbWhileVar.MinimumSize = new Size(63, 0);
            cmbWhileVar.Name = "cmbWhileVar";
            cmbWhileVar.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbWhileVar.Size = new Size(120, 30);
            cmbWhileVar.SymbolSize = 24;
            cmbWhileVar.TabIndex = 1;
            cmbWhileVar.TextAlignment = ContentAlignment.MiddleLeft;
            cmbWhileVar.Watermark = "";
            // 
            // cmbWhileOperator
            // 
            cmbWhileOperator.DataSource = null;
            cmbWhileOperator.FillColor = Color.White;
            cmbWhileOperator.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbWhileOperator.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbWhileOperator.Items.AddRange(new object[] { "==", "!=", ">", "<", ">=", "<=" });
            cmbWhileOperator.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbWhileOperator.Location = new Point(230, 12);
            cmbWhileOperator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbWhileOperator.MinimumSize = new Size(63, 0);
            cmbWhileOperator.Name = "cmbWhileOperator";
            cmbWhileOperator.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbWhileOperator.Size = new Size(80, 30);
            cmbWhileOperator.SymbolSize = 24;
            cmbWhileOperator.TabIndex = 2;
            cmbWhileOperator.Text = "==";
            cmbWhileOperator.TextAlignment = ContentAlignment.MiddleLeft;
            cmbWhileOperator.Watermark = "";
            // 
            // txtWhileValue
            // 
            txtWhileValue.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtWhileValue.Location = new Point(320, 12);
            txtWhileValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtWhileValue.MinimumSize = new Size(1, 16);
            txtWhileValue.Name = "txtWhileValue";
            txtWhileValue.Padding = new System.Windows.Forms.Padding(5);
            txtWhileValue.ShowText = false;
            txtWhileValue.Size = new Size(120, 30);
            txtWhileValue.TabIndex = 3;
            txtWhileValue.TextAlignment = ContentAlignment.MiddleLeft;
            txtWhileValue.Watermark = "";
            // 
            // lblMaxIterations
            // 
            lblMaxIterations.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblMaxIterations.ForeColor = Color.FromArgb(48, 48, 48);
            lblMaxIterations.Location = new Point(460, 15);
            lblMaxIterations.Name = "lblMaxIterations";
            lblMaxIterations.Size = new Size(100, 25);
            lblMaxIterations.TabIndex = 4;
            lblMaxIterations.Text = "最大迭代次数:";
            // 
            // nudMaxIterations
            // 
            nudMaxIterations.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            nudMaxIterations.Location = new Point(570, 12);
            nudMaxIterations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            nudMaxIterations.Maximum = 10000D;
            nudMaxIterations.MinimumSize = new Size(1, 16);
            nudMaxIterations.Name = "nudMaxIterations";
            nudMaxIterations.Padding = new System.Windows.Forms.Padding(5);
            nudMaxIterations.ShowText = false;
            nudMaxIterations.Size = new Size(80, 30);
            nudMaxIterations.TabIndex = 5;
            nudMaxIterations.Text = "1000";
            nudMaxIterations.TextAlignment = ContentAlignment.MiddleCenter;
            nudMaxIterations.Value = 1000;
            // 
            // pnlForeachLoop
            // 
            pnlForeachLoop.Controls.Add(lblForeachArray);
            pnlForeachLoop.Controls.Add(cmbForeachArray);
            pnlForeachLoop.Controls.Add(lblForeachItem);
            pnlForeachLoop.Controls.Add(txtForeachItem);
            pnlForeachLoop.FillColor = Color.FromArgb(248, 248, 248);
            pnlForeachLoop.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlForeachLoop.Location = new Point(12, 100);
            pnlForeachLoop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pnlForeachLoop.MinimumSize = new Size(1, 1);
            pnlForeachLoop.Name = "pnlForeachLoop";
            pnlForeachLoop.Size = new Size(760, 100);
            pnlForeachLoop.TabIndex = 3;
            pnlForeachLoop.Text = null;
            pnlForeachLoop.TextAlignment = ContentAlignment.MiddleCenter;
            pnlForeachLoop.Visible = false;
            // 
            // lblForeachArray
            // 
            lblForeachArray.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblForeachArray.ForeColor = Color.FromArgb(48, 48, 48);
            lblForeachArray.Location = new Point(10, 15);
            lblForeachArray.Name = "lblForeachArray";
            lblForeachArray.Size = new Size(80, 25);
            lblForeachArray.TabIndex = 0;
            lblForeachArray.Text = "数组变量:";
            // 
            // cmbForeachArray
            // 
            cmbForeachArray.DataSource = null;
            cmbForeachArray.FillColor = Color.White;
            cmbForeachArray.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbForeachArray.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbForeachArray.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbForeachArray.Location = new Point(100, 12);
            cmbForeachArray.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbForeachArray.MinimumSize = new Size(63, 0);
            cmbForeachArray.Name = "cmbForeachArray";
            cmbForeachArray.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cmbForeachArray.Size = new Size(200, 30);
            cmbForeachArray.SymbolSize = 24;
            cmbForeachArray.TabIndex = 1;
            cmbForeachArray.TextAlignment = ContentAlignment.MiddleLeft;
            cmbForeachArray.Watermark = "";
            // 
            // lblForeachItem
            // 
            lblForeachItem.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblForeachItem.ForeColor = Color.FromArgb(48, 48, 48);
            lblForeachItem.Location = new Point(320, 15);
            lblForeachItem.Name = "lblForeachItem";
            lblForeachItem.Size = new Size(80, 25);
            lblForeachItem.TabIndex = 2;
            lblForeachItem.Text = "项目变量:";
            // 
            // txtForeachItem
            // 
            txtForeachItem.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtForeachItem.Location = new Point(410, 12);
            txtForeachItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtForeachItem.MinimumSize = new Size(1, 16);
            txtForeachItem.Name = "txtForeachItem";
            txtForeachItem.Padding = new System.Windows.Forms.Padding(5);
            txtForeachItem.ShowText = false;
            txtForeachItem.Size = new Size(120, 30);
            txtForeachItem.TabIndex = 3;
            txtForeachItem.Text = "item";
            txtForeachItem.TextAlignment = ContentAlignment.MiddleLeft;
            txtForeachItem.Watermark = "";
            // 
            // grpLoopBody
            // 
            grpLoopBody.Controls.Add(dgvLoopSteps);
            grpLoopBody.Controls.Add(btnAddStep);
            grpLoopBody.Controls.Add(btnRemoveStep);
            grpLoopBody.Controls.Add(btnEditStep);
            grpLoopBody.Controls.Add(btnMoveUp);
            grpLoopBody.Controls.Add(btnMoveDown);
            grpLoopBody.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpLoopBody.Location = new Point(12, 210);
            grpLoopBody.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpLoopBody.MinimumSize = new Size(1, 1);
            grpLoopBody.Name = "grpLoopBody";
            grpLoopBody.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpLoopBody.Size = new Size(760, 300);
            grpLoopBody.TabIndex = 4;
            grpLoopBody.Text = "循环体步骤";
            grpLoopBody.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // dgvLoopSteps
            // 
            dgvLoopSteps.AllowUserToAddRows = false;
            dataGridViewCellStyle16.BackColor = Color.FromArgb(235, 243, 255);
            dgvLoopSteps.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            dgvLoopSteps.BackgroundColor = Color.White;
            dgvLoopSteps.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle17.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle17.ForeColor = Color.White;
            dataGridViewCellStyle17.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = DataGridViewTriState.True;
            dgvLoopSteps.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            dgvLoopSteps.ColumnHeadersHeight = 32;
            dgvLoopSteps.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = SystemColors.Window;
            dataGridViewCellStyle18.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle18.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle18.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = DataGridViewTriState.False;
            dgvLoopSteps.DefaultCellStyle = dataGridViewCellStyle18;
            dgvLoopSteps.EnableHeadersVisualStyles = false;
            dgvLoopSteps.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvLoopSteps.GridColor = Color.FromArgb(80, 160, 255);
            dgvLoopSteps.Location = new Point(10, 30);
            dgvLoopSteps.Name = "dgvLoopSteps";
            dataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle19.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle19.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle19.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle19.SelectionForeColor = Color.White;
            dataGridViewCellStyle19.WrapMode = DataGridViewTriState.True;
            dgvLoopSteps.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            dataGridViewCellStyle20.BackColor = Color.White;
            dataGridViewCellStyle20.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvLoopSteps.RowsDefaultCellStyle = dataGridViewCellStyle20;
            dgvLoopSteps.SelectedIndex = -1;
            dgvLoopSteps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoopSteps.Size = new Size(600, 250);
            dgvLoopSteps.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvLoopSteps.TabIndex = 0;
            // 
            // btnAddStep
            // 
            btnAddStep.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddStep.Location = new Point(620, 30);
            btnAddStep.MinimumSize = new Size(1, 1);
            btnAddStep.Name = "btnAddStep";
            btnAddStep.Size = new Size(80, 35);
            btnAddStep.TabIndex = 1;
            btnAddStep.Text = "添加步骤";
            btnAddStep.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddStep.Click += BtnAddStep_Click;
            // 
            // btnRemoveStep
            // 
            btnRemoveStep.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRemoveStep.Location = new Point(620, 75);
            btnRemoveStep.MinimumSize = new Size(1, 1);
            btnRemoveStep.Name = "btnRemoveStep";
            btnRemoveStep.Size = new Size(80, 35);
            btnRemoveStep.TabIndex = 2;
            btnRemoveStep.Text = "删除步骤";
            btnRemoveStep.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRemoveStep.Click += BtnRemoveStep_Click;
            // 
            // btnEditStep
            // 
            btnEditStep.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEditStep.Location = new Point(620, 120);
            btnEditStep.MinimumSize = new Size(1, 1);
            btnEditStep.Name = "btnEditStep";
            btnEditStep.Size = new Size(80, 35);
            btnEditStep.TabIndex = 3;
            btnEditStep.Text = "编辑步骤";
            btnEditStep.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEditStep.Click += BtnEditStep_Click;
            // 
            // btnMoveUp
            // 
            btnMoveUp.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnMoveUp.Location = new Point(620, 165);
            btnMoveUp.MinimumSize = new Size(1, 1);
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.Size = new Size(80, 35);
            btnMoveUp.TabIndex = 4;
            btnMoveUp.Text = "上移";
            btnMoveUp.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnMoveUp.Click += BtnMoveUp_Click;
            // 
            // btnMoveDown
            // 
            btnMoveDown.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnMoveDown.Location = new Point(620, 210);
            btnMoveDown.MinimumSize = new Size(1, 1);
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.Size = new Size(80, 35);
            btnMoveDown.TabIndex = 5;
            btnMoveDown.Text = "下移";
            btnMoveDown.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnMoveDown.Click += BtnMoveDown_Click;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnTest);
            pnlButtons.Controls.Add(btnOK);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.FillColor = Color.White;
            pnlButtons.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlButtons.Location = new Point(12, 520);
            pnlButtons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pnlButtons.MinimumSize = new Size(1, 1);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(760, 50);
            pnlButtons.TabIndex = 5;
            pnlButtons.Text = null;
            pnlButtons.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnTest
            // 
            btnTest.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnTest.Location = new Point(400, 10);
            btnTest.MinimumSize = new Size(1, 1);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(80, 35);
            btnTest.TabIndex = 0;
            btnTest.Text = "测试运行";
            btnTest.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnTest.Click += BtnTest_Click;
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOK.Location = new Point(500, 10);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(80, 35);
            btnOK.TabIndex = 1;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOK.Click += BtnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Location = new Point(600, 10);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 35);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // frmLoopConfiguration
            // 
            ClientSize = new Size(800, 620);
            ControlBox = false;
            Controls.Add(pnlMain);
            MaximizeBox = false;
            Name = "frmLoopConfiguration";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "循环工具配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 620);
            pnlMain.ResumeLayout(false);
            grpLoopType.ResumeLayout(false);
            pnlForLoop.ResumeLayout(false);
            pnlWhileLoop.ResumeLayout(false);
            pnlForeachLoop.ResumeLayout(false);
            grpLoopBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLoopSteps).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }


        #region 控件声明
        private UIPanel pnlMain;
        private UIGroupBox grpLoopType;
        private UIRadioButton rbForLoop;
        private UIRadioButton rbWhileLoop;
        private UIRadioButton rbForeachLoop;

        // For循环控件
        private UIPanel pnlForLoop;
        private UILabel lblForVar;
        private UITextBox txtForVar;
        private UILabel lblForStart;
        private UITextBox txtForStart;
        private UILabel lblForEnd;
        private UITextBox txtForEnd;
        private UILabel lblForStep;
        private UITextBox txtForStep;

        // While循环控件
        private UIPanel pnlWhileLoop;
        private UILabel lblWhileCondition;
        private UIComboBox cmbWhileVar;
        private UIComboBox cmbWhileOperator;
        private UITextBox txtWhileValue;
        private UILabel lblMaxIterations;
        private UIIntegerUpDown nudMaxIterations;

        // Foreach循环控件
        private UIPanel pnlForeachLoop;
        private UILabel lblForeachArray;
        private UIComboBox cmbForeachArray;
        private UILabel lblForeachItem;
        private UITextBox txtForeachItem;

        // 循环体配置控件
        private UIGroupBox grpLoopBody;
        private UIDataGridView dgvLoopSteps;
        private UIButton btnAddStep;
        private UIButton btnRemoveStep;
        private UIButton btnEditStep;
        private UIButton btnMoveUp;
        private UIButton btnMoveDown;

        // 按钮控件
        private UIPanel pnlButtons;
        private UIButton btnOK;
        private UIButton btnCancel;
        private UIButton btnTest;
        #endregion

        #endregion

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}
using Sunny.UI;

namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_Loop
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
            panelDescription = new Panel();
            chkEnabled = new UICheckBox();
            txtDescription = new UITextBox();
            lblDescription = new UILabel();
            panelMain = new Panel();
            panelChildSteps = new UIPanel();
            btnConfigChildSteps = new UIButton();
            lblChildStepsCount = new UILabel();
            lblChildStepsTitle = new UILabel();
            panelEarlyExit = new UIPanel();
            lblExitConditionHint = new UILabel();
            txtExitCondition = new UITextBox();
            lblExitCondition = new UILabel();
            chkEnableEarlyExit = new UICheckBox();
            lblEarlyExitTitle = new UILabel();
            panelLoopConfig = new UIPanel();
            chkEnableCounter = new UICheckBox();
            txtCounterVariable = new UITextBox();
            lblCounterVariable = new UILabel();
            txtLoopCount = new UITextBox();
            lblLoopCount = new UILabel();
            lblLoopConfigTitle = new UILabel();
            panelBottom = new Panel();
            btnHelp = new UISymbolButton();
            btnCancel = new UISymbolButton();
            btnSave = new UISymbolButton();
            panelDescription.SuspendLayout();
            panelMain.SuspendLayout();
            panelChildSteps.SuspendLayout();
            panelEarlyExit.SuspendLayout();
            panelLoopConfig.SuspendLayout();
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
            panelDescription.Size = new Size(800, 70);
            panelDescription.TabIndex = 1;
            // 
            // chkEnabled
            // 
            chkEnabled.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkEnabled.CheckBoxSize = 18;
            chkEnabled.Checked = true;
            chkEnabled.Font = new Font("微软雅黑", 10F);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(650, 15);
            chkEnabled.MinimumSize = new Size(1, 1);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(130, 30);
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
            txtDescription.Size = new Size(500, 30);
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
            panelMain.Controls.Add(panelChildSteps);
            panelMain.Controls.Add(panelEarlyExit);
            panelMain.Controls.Add(panelLoopConfig);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 105);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15, 10, 15, 10);
            panelMain.Size = new Size(800, 515);
            panelMain.TabIndex = 2;
            // 
            // panelChildSteps
            // 
            panelChildSteps.BackColor = Color.FromArgb(250, 250, 250);
            panelChildSteps.Controls.Add(btnConfigChildSteps);
            panelChildSteps.Controls.Add(lblChildStepsCount);
            panelChildSteps.Controls.Add(lblChildStepsTitle);
            panelChildSteps.Dock = DockStyle.Fill;
            panelChildSteps.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelChildSteps.Location = new Point(15, 320);
            panelChildSteps.Margin = new Padding(5);
            panelChildSteps.MinimumSize = new Size(1, 1);
            panelChildSteps.Name = "panelChildSteps";
            panelChildSteps.Padding = new Padding(15);
            panelChildSteps.Radius = 8;
            panelChildSteps.RectColor = Color.FromArgb(65, 100, 204);
            panelChildSteps.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            panelChildSteps.Size = new Size(770, 185);
            panelChildSteps.TabIndex = 2;
            panelChildSteps.Text = null;
            panelChildSteps.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnConfigChildSteps
            // 
            btnConfigChildSteps.Cursor = Cursors.Hand;
            btnConfigChildSteps.Font = new Font("微软雅黑", 10F);
            btnConfigChildSteps.Location = new Point(18, 60);
            btnConfigChildSteps.MinimumSize = new Size(1, 1);
            btnConfigChildSteps.Name = "btnConfigChildSteps";
            btnConfigChildSteps.Size = new Size(150, 35);
            btnConfigChildSteps.TabIndex = 2;
            btnConfigChildSteps.Text = "配置循环体步骤...";
            btnConfigChildSteps.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // lblChildStepsCount
            // 
            lblChildStepsCount.BackColor = Color.Transparent;
            lblChildStepsCount.Font = new Font("微软雅黑", 10F);
            lblChildStepsCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblChildStepsCount.Location = new Point(18, 30);
            lblChildStepsCount.Name = "lblChildStepsCount";
            lblChildStepsCount.Size = new Size(500, 25);
            lblChildStepsCount.TabIndex = 1;
            lblChildStepsCount.Text = "循环体步骤 (0 个)";
            lblChildStepsCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblChildStepsTitle
            // 
            lblChildStepsTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblChildStepsTitle.ForeColor = Color.FromArgb(65, 100, 204);
            lblChildStepsTitle.Location = new Point(18, 5);
            lblChildStepsTitle.Name = "lblChildStepsTitle";
            lblChildStepsTitle.Size = new Size(200, 25);
            lblChildStepsTitle.TabIndex = 0;
            lblChildStepsTitle.Text = "循环体步骤配置";
            lblChildStepsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelEarlyExit
            // 
            panelEarlyExit.BackColor = Color.FromArgb(250, 250, 250);
            panelEarlyExit.Controls.Add(lblExitConditionHint);
            panelEarlyExit.Controls.Add(txtExitCondition);
            panelEarlyExit.Controls.Add(lblExitCondition);
            panelEarlyExit.Controls.Add(chkEnableEarlyExit);
            panelEarlyExit.Controls.Add(lblEarlyExitTitle);
            panelEarlyExit.Dock = DockStyle.Top;
            panelEarlyExit.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelEarlyExit.Location = new Point(15, 170);
            panelEarlyExit.Margin = new Padding(5);
            panelEarlyExit.MinimumSize = new Size(1, 1);
            panelEarlyExit.Name = "panelEarlyExit";
            panelEarlyExit.Padding = new Padding(15);
            panelEarlyExit.Radius = 8;
            panelEarlyExit.RectColor = Color.FromArgb(65, 100, 204);
            panelEarlyExit.RectSides = ToolStripStatusLabelBorderSides.None;
            panelEarlyExit.Size = new Size(770, 150);
            panelEarlyExit.TabIndex = 1;
            panelEarlyExit.Text = null;
            panelEarlyExit.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblExitConditionHint
            // 
            lblExitConditionHint.BackColor = Color.Transparent;
            lblExitConditionHint.Font = new Font("微软雅黑", 9F);
            lblExitConditionHint.ForeColor = Color.Gray;
            lblExitConditionHint.Location = new Point(124, 106);
            lblExitConditionHint.Name = "lblExitConditionHint";
            lblExitConditionHint.Size = new Size(586, 35);
            lblExitConditionHint.TabIndex = 9;
            lblExitConditionHint.Text = "点击输入框打开智能面板，支持变量、PLC、表达式等多种输入方式";
            // 
            // txtExitCondition
            // 
            txtExitCondition.Cursor = Cursors.IBeam;
            txtExitCondition.Font = new Font("微软雅黑", 10F);
            txtExitCondition.Location = new Point(124, 66);
            txtExitCondition.Margin = new Padding(4, 5, 4, 5);
            txtExitCondition.MinimumSize = new Size(1, 16);
            txtExitCondition.Name = "txtExitCondition";
            txtExitCondition.Padding = new Padding(5);
            txtExitCondition.RectColor = Color.FromArgb(65, 100, 204);
            txtExitCondition.ShowText = false;
            txtExitCondition.Size = new Size(620, 35);
            txtExitCondition.TabIndex = 6;
            txtExitCondition.TextAlignment = ContentAlignment.MiddleLeft;
            txtExitCondition.Watermark = "点击输入退出条件，如：{压力值} >= 6.0 (按F2打开面板)";
            // 
            // lblExitCondition
            // 
            lblExitCondition.BackColor = Color.Transparent;
            lblExitCondition.Font = new Font("微软雅黑", 10F);
            lblExitCondition.ForeColor = Color.FromArgb(48, 48, 48);
            lblExitCondition.Location = new Point(18, 70);
            lblExitCondition.Name = "lblExitCondition";
            lblExitCondition.Size = new Size(100, 25);
            lblExitCondition.TabIndex = 5;
            lblExitCondition.Text = "退出条件:";
            lblExitCondition.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkEnableEarlyExit
            // 
            chkEnableEarlyExit.BackColor = Color.Transparent;
            chkEnableEarlyExit.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkEnableEarlyExit.CheckBoxSize = 18;
            chkEnableEarlyExit.Font = new Font("微软雅黑", 10F);
            chkEnableEarlyExit.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnableEarlyExit.Location = new Point(18, 36);
            chkEnableEarlyExit.MinimumSize = new Size(1, 1);
            chkEnableEarlyExit.Name = "chkEnableEarlyExit";
            chkEnableEarlyExit.Size = new Size(200, 30);
            chkEnableEarlyExit.TabIndex = 4;
            chkEnableEarlyExit.Text = "启用提前退出";
            // 
            // lblEarlyExitTitle
            // 
            lblEarlyExitTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblEarlyExitTitle.ForeColor = Color.FromArgb(65, 100, 204);
            lblEarlyExitTitle.Location = new Point(18, 5);
            lblEarlyExitTitle.Name = "lblEarlyExitTitle";
            lblEarlyExitTitle.Size = new Size(200, 25);
            lblEarlyExitTitle.TabIndex = 3;
            lblEarlyExitTitle.Text = "提前退出配置";
            lblEarlyExitTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelLoopConfig
            // 
            panelLoopConfig.BackColor = Color.FromArgb(250, 250, 250);
            panelLoopConfig.Controls.Add(chkEnableCounter);
            panelLoopConfig.Controls.Add(txtCounterVariable);
            panelLoopConfig.Controls.Add(lblCounterVariable);
            panelLoopConfig.Controls.Add(txtLoopCount);
            panelLoopConfig.Controls.Add(lblLoopCount);
            panelLoopConfig.Controls.Add(lblLoopConfigTitle);
            panelLoopConfig.Dock = DockStyle.Top;
            panelLoopConfig.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelLoopConfig.Location = new Point(15, 10);
            panelLoopConfig.Margin = new Padding(5);
            panelLoopConfig.MinimumSize = new Size(1, 1);
            panelLoopConfig.Name = "panelLoopConfig";
            panelLoopConfig.Padding = new Padding(15);
            panelLoopConfig.Radius = 8;
            panelLoopConfig.RectColor = Color.FromArgb(65, 100, 204);
            panelLoopConfig.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right;
            panelLoopConfig.Size = new Size(770, 160);
            panelLoopConfig.TabIndex = 0;
            panelLoopConfig.Text = null;
            panelLoopConfig.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // chkEnableCounter
            // 
            chkEnableCounter.BackColor = Color.Transparent;
            chkEnableCounter.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkEnableCounter.CheckBoxSize = 18;
            chkEnableCounter.Checked = true;
            chkEnableCounter.Font = new Font("微软雅黑", 10F);
            chkEnableCounter.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnableCounter.Location = new Point(18, 80);
            chkEnableCounter.MinimumSize = new Size(1, 1);
            chkEnableCounter.Name = "chkEnableCounter";
            chkEnableCounter.Size = new Size(130, 30);
            chkEnableCounter.TabIndex = 8;
            chkEnableCounter.Text = "启用计数器";
            // 
            // txtCounterVariable
            // 
            txtCounterVariable.Cursor = Cursors.IBeam;
            txtCounterVariable.Font = new Font("微软雅黑", 10F);
            txtCounterVariable.Location = new Point(280, 80);
            txtCounterVariable.Margin = new Padding(4, 5, 4, 5);
            txtCounterVariable.MinimumSize = new Size(1, 16);
            txtCounterVariable.Name = "txtCounterVariable";
            txtCounterVariable.Padding = new Padding(5);
            txtCounterVariable.RectColor = Color.FromArgb(65, 100, 204);
            txtCounterVariable.ShowText = false;
            txtCounterVariable.Size = new Size(250, 30);
            txtCounterVariable.TabIndex = 10;
            txtCounterVariable.Text = "LoopIndex";
            txtCounterVariable.TextAlignment = ContentAlignment.MiddleLeft;
            txtCounterVariable.Watermark = "计数器变量名";
            // 
            // lblCounterVariable
            // 
            lblCounterVariable.BackColor = Color.Transparent;
            lblCounterVariable.Font = new Font("微软雅黑", 10F);
            lblCounterVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblCounterVariable.Location = new Point(160, 80);
            lblCounterVariable.Name = "lblCounterVariable";
            lblCounterVariable.Size = new Size(115, 25);
            lblCounterVariable.TabIndex = 9;
            lblCounterVariable.Text = "计数器变量:";
            lblCounterVariable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtLoopCount
            // 
            txtLoopCount.Cursor = Cursors.IBeam;
            txtLoopCount.DoubleValue = 10D;
            txtLoopCount.Font = new Font("微软雅黑", 10F);
            txtLoopCount.IntValue = 10;
            txtLoopCount.Location = new Point(124, 36);
            txtLoopCount.Margin = new Padding(4, 5, 4, 5);
            txtLoopCount.MinimumSize = new Size(1, 16);
            txtLoopCount.Name = "txtLoopCount";
            txtLoopCount.Padding = new Padding(5);
            txtLoopCount.RectColor = Color.FromArgb(65, 100, 204);
            txtLoopCount.ShowText = false;
            txtLoopCount.Size = new Size(620, 35);
            txtLoopCount.TabIndex = 4;
            txtLoopCount.Text = "10";
            txtLoopCount.TextAlignment = ContentAlignment.MiddleLeft;
            txtLoopCount.Watermark = "点击输入循环次数，支持数值/变量/PLC (按F2打开面板)";
            // 
            // lblLoopCount
            // 
            lblLoopCount.BackColor = Color.Transparent;
            lblLoopCount.Font = new Font("微软雅黑", 10F);
            lblLoopCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblLoopCount.Location = new Point(18, 40);
            lblLoopCount.Name = "lblLoopCount";
            lblLoopCount.Size = new Size(100, 25);
            lblLoopCount.TabIndex = 3;
            lblLoopCount.Text = "循环次数:";
            lblLoopCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLoopConfigTitle
            // 
            lblLoopConfigTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblLoopConfigTitle.ForeColor = Color.FromArgb(65, 100, 204);
            lblLoopConfigTitle.Location = new Point(18, 5);
            lblLoopConfigTitle.Name = "lblLoopConfigTitle";
            lblLoopConfigTitle.Size = new Size(200, 25);
            lblLoopConfigTitle.TabIndex = 0;
            lblLoopConfigTitle.Text = "循环配置";
            lblLoopConfigTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(btnHelp);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 620);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(15, 10, 15, 10);
            panelBottom.Size = new Size(800, 60);
            panelBottom.TabIndex = 3;
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.Font = new Font("微软雅黑", 10F);
            btnHelp.Location = new Point(18, 13);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(100, 35);
            btnHelp.Symbol = 61529;
            btnHelp.TabIndex = 2;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(230, 80, 80);
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(572, 13);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(230, 80, 80);
            btnCancel.RectHoverColor = Color.FromArgb(235, 115, 115);
            btnCancel.RectPressColor = Color.FromArgb(184, 64, 64);
            btnCancel.RectSelectedColor = Color.FromArgb(184, 64, 64);
            btnCancel.Size = new Size(100, 35);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.FromArgb(65, 100, 204);
            btnSave.Font = new Font("微软雅黑", 10F);
            btnSave.Location = new Point(682, 13);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectColor = Color.FromArgb(65, 100, 204);
            btnSave.RectHoverColor = Color.FromArgb(80, 126, 164);
            btnSave.RectPressColor = Color.FromArgb(52, 80, 163);
            btnSave.RectSelectedColor = Color.FromArgb(52, 80, 163);
            btnSave.Size = new Size(100, 35);
            btnSave.Symbol = 61639;
            btnSave.TabIndex = 0;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // Form_Loop
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 680);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelDescription);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Loop";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "循环配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 680);
            panelDescription.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            panelChildSteps.ResumeLayout(false);
            panelEarlyExit.ResumeLayout(false);
            panelLoopConfig.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // ====== 可见控件 ======
        private Panel panelDescription;
        private UICheckBox chkEnabled;
        private UITextBox txtDescription;
        private UILabel lblDescription;
        private Panel panelMain;
        private UIPanel panelLoopConfig;
        private UILabel lblLoopConfigTitle;
        private UITextBox txtLoopCount;
        private UILabel lblLoopCount;
        private UICheckBox chkEnableCounter;
        private UITextBox txtCounterVariable;
        private UILabel lblCounterVariable;
        private UIPanel panelEarlyExit;
        private UILabel lblEarlyExitTitle;
        private UICheckBox chkEnableEarlyExit;
        private UILabel lblExitCondition;
        private UITextBox txtExitCondition;
        private UILabel lblExitConditionHint;
        private UIPanel panelChildSteps;
        private UIButton btnConfigChildSteps;
        private UILabel lblChildStepsCount;
        private UILabel lblChildStepsTitle;
        private Panel panelBottom;
        private UISymbolButton btnHelp;
        private UISymbolButton btnCancel;
        private UISymbolButton btnSave;
    }
}
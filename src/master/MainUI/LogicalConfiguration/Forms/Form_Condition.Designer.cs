using MainUI.LogicalConfiguration.Parameter;
using Sunny.UI;

namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_Condition
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
            btnConfigFalseSteps = new UIButton();
            lblFalseStepsCount = new UILabel();
            btnConfigTrueSteps = new UIButton();
            lblTrueStepsCount = new UILabel();
            lblChildStepsTitle = new UILabel();
            panelCondition = new UIPanel();
            panelRangeValue = new Panel();
            btnSelectVarRangeMax = new UISymbolButton();
            txtRangeMax = new UITextBox();
            lblRangeMax = new UILabel();
            btnSelectVarRangeMin = new UISymbolButton();
            txtRangeMin = new UITextBox();
            lblRangeMin = new UILabel();
            panelSingleValue = new Panel();
            btnSelectVarRight = new UISymbolButton();
            txtRightExpression = new UITextBox();
            lblRightExpression = new UILabel();
            cmbOperator = new UIComboBox();
            lblOperator = new UILabel();
            btnSelectVarLeft = new UISymbolButton();
            txtLeftExpression = new UITextBox();
            lblLeftExpression = new UILabel();
            lblConditionTitle = new UILabel();
            panelBottom = new Panel();
            btnHelp = new UISymbolButton();
            btnCancel = new UISymbolButton();
            btnSave = new UISymbolButton();
            panelDescription.SuspendLayout();
            panelMain.SuspendLayout();
            panelChildSteps.SuspendLayout();
            panelCondition.SuspendLayout();
            panelRangeValue.SuspendLayout();
            panelSingleValue.SuspendLayout();
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
            panelDescription.Size = new Size(900, 70);
            panelDescription.TabIndex = 1;
            // 
            // chkEnabled
            // 
            chkEnabled.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkEnabled.CheckBoxSize = 18;
            chkEnabled.Checked = true;
            chkEnabled.Font = new Font("微软雅黑", 10F);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(750, 15);
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
            txtDescription.Size = new Size(600, 30);
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
            panelMain.Controls.Add(panelCondition);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 105);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15, 10, 15, 10);
            panelMain.Size = new Size(900, 495);
            panelMain.TabIndex = 2;
            // 
            // panelChildSteps
            // 
            panelChildSteps.BackColor = Color.FromArgb(250, 250, 250);
            panelChildSteps.Controls.Add(btnConfigFalseSteps);
            panelChildSteps.Controls.Add(lblFalseStepsCount);
            panelChildSteps.Controls.Add(btnConfigTrueSteps);
            panelChildSteps.Controls.Add(lblTrueStepsCount);
            panelChildSteps.Controls.Add(lblChildStepsTitle);
            panelChildSteps.Dock = DockStyle.Fill;
            panelChildSteps.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelChildSteps.Location = new Point(15, 275);
            panelChildSteps.Margin = new Padding(4, 5, 4, 5);
            panelChildSteps.MinimumSize = new Size(1, 1);
            panelChildSteps.Name = "panelChildSteps";
            panelChildSteps.Padding = new Padding(15);
            panelChildSteps.Radius = 8;
            panelChildSteps.RectColor = Color.FromArgb(65, 100, 204);
            panelChildSteps.Size = new Size(870, 210);
            panelChildSteps.TabIndex = 1;
            panelChildSteps.Text = null;
            panelChildSteps.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnConfigFalseSteps
            // 
            btnConfigFalseSteps.Cursor = Cursors.Hand;
            btnConfigFalseSteps.Font = new Font("微软雅黑", 10F);
            btnConfigFalseSteps.Location = new Point(18, 135);
            btnConfigFalseSteps.MinimumSize = new Size(1, 1);
            btnConfigFalseSteps.Name = "btnConfigFalseSteps";
            btnConfigFalseSteps.Size = new Size(150, 35);
            btnConfigFalseSteps.TabIndex = 4;
            btnConfigFalseSteps.Text = "配置步骤...";
            btnConfigFalseSteps.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // lblFalseStepsCount
            // 
            lblFalseStepsCount.Font = new Font("微软雅黑", 10F);
            lblFalseStepsCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblFalseStepsCount.Location = new Point(18, 105);
            lblFalseStepsCount.Name = "lblFalseStepsCount";
            lblFalseStepsCount.Size = new Size(350, 25);
            lblFalseStepsCount.TabIndex = 3;
            lblFalseStepsCount.Text = "不满足条件时执行的步骤 (0 个)";
            lblFalseStepsCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnConfigTrueSteps
            // 
            btnConfigTrueSteps.Cursor = Cursors.Hand;
            btnConfigTrueSteps.Font = new Font("微软雅黑", 10F);
            btnConfigTrueSteps.Location = new Point(18, 60);
            btnConfigTrueSteps.MinimumSize = new Size(1, 1);
            btnConfigTrueSteps.Name = "btnConfigTrueSteps";
            btnConfigTrueSteps.Size = new Size(150, 35);
            btnConfigTrueSteps.TabIndex = 2;
            btnConfigTrueSteps.Text = "配置步骤...";
            btnConfigTrueSteps.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // lblTrueStepsCount
            // 
            lblTrueStepsCount.Font = new Font("微软雅黑", 10F);
            lblTrueStepsCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblTrueStepsCount.Location = new Point(18, 30);
            lblTrueStepsCount.Name = "lblTrueStepsCount";
            lblTrueStepsCount.Size = new Size(350, 25);
            lblTrueStepsCount.TabIndex = 1;
            lblTrueStepsCount.Text = "满足条件时执行的步骤 (0 个)";
            lblTrueStepsCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblChildStepsTitle
            // 
            lblChildStepsTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblChildStepsTitle.ForeColor = Color.FromArgb(65, 100, 204);
            lblChildStepsTitle.Location = new Point(18, 5);
            lblChildStepsTitle.Name = "lblChildStepsTitle";
            lblChildStepsTitle.Size = new Size(200, 25);
            lblChildStepsTitle.TabIndex = 0;
            lblChildStepsTitle.Text = "执行分支配置";
            lblChildStepsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelCondition
            // 
            panelCondition.BackColor = Color.FromArgb(250, 250, 250);
            panelCondition.Controls.Add(panelRangeValue);
            panelCondition.Controls.Add(panelSingleValue);
            panelCondition.Controls.Add(cmbOperator);
            panelCondition.Controls.Add(lblOperator);
            panelCondition.Controls.Add(btnSelectVarLeft);
            panelCondition.Controls.Add(txtLeftExpression);
            panelCondition.Controls.Add(lblLeftExpression);
            panelCondition.Controls.Add(lblConditionTitle);
            panelCondition.Dock = DockStyle.Top;
            panelCondition.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelCondition.Location = new Point(15, 10);
            panelCondition.Margin = new Padding(4, 5, 4, 5);
            panelCondition.MinimumSize = new Size(1, 1);
            panelCondition.Name = "panelCondition";
            panelCondition.Padding = new Padding(15);
            panelCondition.Radius = 8;
            panelCondition.RectColor = Color.FromArgb(65, 100, 204);
            panelCondition.Size = new Size(870, 265);
            panelCondition.TabIndex = 0;
            panelCondition.Text = null;
            panelCondition.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // panelRangeValue
            // 
            panelRangeValue.BackColor = Color.Transparent;
            panelRangeValue.Controls.Add(btnSelectVarRangeMax);
            panelRangeValue.Controls.Add(txtRangeMax);
            panelRangeValue.Controls.Add(lblRangeMax);
            panelRangeValue.Controls.Add(btnSelectVarRangeMin);
            panelRangeValue.Controls.Add(txtRangeMin);
            panelRangeValue.Controls.Add(lblRangeMin);
            panelRangeValue.Location = new Point(18, 135);
            panelRangeValue.Name = "panelRangeValue";
            panelRangeValue.Size = new Size(830, 110);
            panelRangeValue.TabIndex = 7;
            panelRangeValue.Visible = false;
            // 
            // btnSelectVarRangeMax
            // 
            btnSelectVarRangeMax.Cursor = Cursors.Hand;
            btnSelectVarRangeMax.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSelectVarRangeMax.Location = new Point(690, 55);
            btnSelectVarRangeMax.MinimumSize = new Size(1, 1);
            btnSelectVarRangeMax.Name = "btnSelectVarRangeMax";
            btnSelectVarRangeMax.Size = new Size(120, 30);
            btnSelectVarRangeMax.Symbol = 361697;
            btnSelectVarRangeMax.TabIndex = 5;
            btnSelectVarRangeMax.Text = "选择变量";
            btnSelectVarRangeMax.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // txtRangeMax
            // 
            txtRangeMax.Cursor = Cursors.IBeam;
            txtRangeMax.Font = new Font("微软雅黑", 10F);
            txtRangeMax.Location = new Point(106, 55);
            txtRangeMax.Margin = new Padding(4, 5, 4, 5);
            txtRangeMax.MinimumSize = new Size(1, 16);
            txtRangeMax.Name = "txtRangeMax";
            txtRangeMax.Padding = new Padding(5);
            txtRangeMax.RectColor = Color.FromArgb(65, 100, 204);
            txtRangeMax.ShowText = false;
            txtRangeMax.Size = new Size(570, 30);
            txtRangeMax.TabIndex = 4;
            txtRangeMax.TextAlignment = ContentAlignment.MiddleLeft;
            txtRangeMax.Watermark = "输入范围最大值或变量，如：100 或 {MaxValue}";
            // 
            // lblRangeMax
            // 
            lblRangeMax.Font = new Font("微软雅黑", 10F);
            lblRangeMax.ForeColor = Color.FromArgb(48, 48, 48);
            lblRangeMax.Location = new Point(7, 55);
            lblRangeMax.Name = "lblRangeMax";
            lblRangeMax.Size = new Size(100, 25);
            lblRangeMax.TabIndex = 3;
            lblRangeMax.Text = "范围最大值:";
            lblRangeMax.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSelectVarRangeMin
            // 
            btnSelectVarRangeMin.Cursor = Cursors.Hand;
            btnSelectVarRangeMin.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSelectVarRangeMin.Location = new Point(690, 10);
            btnSelectVarRangeMin.MinimumSize = new Size(1, 1);
            btnSelectVarRangeMin.Name = "btnSelectVarRangeMin";
            btnSelectVarRangeMin.Size = new Size(120, 30);
            btnSelectVarRangeMin.Symbol = 361697;
            btnSelectVarRangeMin.TabIndex = 2;
            btnSelectVarRangeMin.Text = "选择变量";
            btnSelectVarRangeMin.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // txtRangeMin
            // 
            txtRangeMin.Cursor = Cursors.IBeam;
            txtRangeMin.Font = new Font("微软雅黑", 10F);
            txtRangeMin.Location = new Point(106, 10);
            txtRangeMin.Margin = new Padding(4, 5, 4, 5);
            txtRangeMin.MinimumSize = new Size(1, 16);
            txtRangeMin.Name = "txtRangeMin";
            txtRangeMin.Padding = new Padding(5);
            txtRangeMin.RectColor = Color.FromArgb(65, 100, 204);
            txtRangeMin.ShowText = false;
            txtRangeMin.Size = new Size(570, 30);
            txtRangeMin.TabIndex = 1;
            txtRangeMin.TextAlignment = ContentAlignment.MiddleLeft;
            txtRangeMin.Watermark = "输入范围最小值或变量，如：0 或 {MinValue}";
            // 
            // lblRangeMin
            // 
            lblRangeMin.Font = new Font("微软雅黑", 10F);
            lblRangeMin.ForeColor = Color.FromArgb(48, 48, 48);
            lblRangeMin.Location = new Point(6, 10);
            lblRangeMin.Name = "lblRangeMin";
            lblRangeMin.Size = new Size(100, 25);
            lblRangeMin.TabIndex = 0;
            lblRangeMin.Text = "范围最小值:";
            lblRangeMin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelSingleValue
            // 
            panelSingleValue.BackColor = Color.Transparent;
            panelSingleValue.Controls.Add(btnSelectVarRight);
            panelSingleValue.Controls.Add(txtRightExpression);
            panelSingleValue.Controls.Add(lblRightExpression);
            panelSingleValue.Location = new Point(18, 135);
            panelSingleValue.Name = "panelSingleValue";
            panelSingleValue.Size = new Size(830, 45);
            panelSingleValue.TabIndex = 6;
            // 
            // btnSelectVarRight
            // 
            btnSelectVarRight.Cursor = Cursors.Hand;
            btnSelectVarRight.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSelectVarRight.Location = new Point(690, 5);
            btnSelectVarRight.MinimumSize = new Size(1, 1);
            btnSelectVarRight.Name = "btnSelectVarRight";
            btnSelectVarRight.Size = new Size(120, 30);
            btnSelectVarRight.Symbol = 361697;
            btnSelectVarRight.TabIndex = 2;
            btnSelectVarRight.Text = "选择变量";
            btnSelectVarRight.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // txtRightExpression
            // 
            txtRightExpression.Cursor = Cursors.IBeam;
            txtRightExpression.Font = new Font("微软雅黑", 10F);
            txtRightExpression.Location = new Point(106, 5);
            txtRightExpression.Margin = new Padding(4, 5, 4, 5);
            txtRightExpression.MinimumSize = new Size(1, 16);
            txtRightExpression.Name = "txtRightExpression";
            txtRightExpression.Padding = new Padding(5);
            txtRightExpression.RectColor = Color.FromArgb(65, 100, 204);
            txtRightExpression.ShowText = false;
            txtRightExpression.Size = new Size(570, 30);
            txtRightExpression.TabIndex = 1;
            txtRightExpression.TextAlignment = ContentAlignment.MiddleLeft;
            txtRightExpression.Watermark = "输入右值表达式或变量，如：100 或 {Target}";
            // 
            // lblRightExpression
            // 
            lblRightExpression.Font = new Font("微软雅黑", 10F);
            lblRightExpression.ForeColor = Color.FromArgb(48, 48, 48);
            lblRightExpression.Location = new Point(0, 5);
            lblRightExpression.Name = "lblRightExpression";
            lblRightExpression.Size = new Size(100, 25);
            lblRightExpression.TabIndex = 0;
            lblRightExpression.Text = "右值表达式:";
            lblRightExpression.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbOperator
            // 
            cmbOperator.DataSource = null;
            cmbOperator.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbOperator.FillColor = Color.White;
            cmbOperator.Font = new Font("微软雅黑", 10F);
            cmbOperator.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbOperator.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbOperator.Location = new Point(124, 90);
            cmbOperator.Margin = new Padding(4, 5, 4, 5);
            cmbOperator.MinimumSize = new Size(63, 0);
            cmbOperator.Name = "cmbOperator";
            cmbOperator.Padding = new Padding(0, 0, 30, 2);
            cmbOperator.RectColor = Color.FromArgb(65, 100, 204);
            cmbOperator.Size = new Size(200, 30);
            cmbOperator.SymbolSize = 24;
            cmbOperator.TabIndex = 5;
            cmbOperator.TextAlignment = ContentAlignment.MiddleLeft;
            cmbOperator.Watermark = "";
            // 
            // lblOperator
            // 
            lblOperator.Font = new Font("微软雅黑", 10F);
            lblOperator.ForeColor = Color.FromArgb(48, 48, 48);
            lblOperator.Location = new Point(18, 90);
            lblOperator.Name = "lblOperator";
            lblOperator.Size = new Size(100, 25);
            lblOperator.TabIndex = 4;
            lblOperator.Text = "运算符:";
            lblOperator.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSelectVarLeft
            // 
            btnSelectVarLeft.Cursor = Cursors.Hand;
            btnSelectVarLeft.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSelectVarLeft.Location = new Point(708, 45);
            btnSelectVarLeft.MinimumSize = new Size(1, 1);
            btnSelectVarLeft.Name = "btnSelectVarLeft";
            btnSelectVarLeft.Size = new Size(120, 30);
            btnSelectVarLeft.Symbol = 361697;
            btnSelectVarLeft.TabIndex = 3;
            btnSelectVarLeft.Text = "选择变量";
            btnSelectVarLeft.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // txtLeftExpression
            // 
            txtLeftExpression.Cursor = Cursors.IBeam;
            txtLeftExpression.Font = new Font("微软雅黑", 10F);
            txtLeftExpression.Location = new Point(124, 45);
            txtLeftExpression.Margin = new Padding(4, 5, 4, 5);
            txtLeftExpression.MinimumSize = new Size(1, 16);
            txtLeftExpression.Name = "txtLeftExpression";
            txtLeftExpression.Padding = new Padding(5);
            txtLeftExpression.RectColor = Color.FromArgb(65, 100, 204);
            txtLeftExpression.ShowText = false;
            txtLeftExpression.Size = new Size(570, 30);
            txtLeftExpression.TabIndex = 2;
            txtLeftExpression.TextAlignment = ContentAlignment.MiddleLeft;
            txtLeftExpression.Watermark = "输入左值表达式或变量，如：{Temperature} 或 {Value1} + 10";
            // 
            // lblLeftExpression
            // 
            lblLeftExpression.Font = new Font("微软雅黑", 10F);
            lblLeftExpression.ForeColor = Color.FromArgb(48, 48, 48);
            lblLeftExpression.Location = new Point(18, 45);
            lblLeftExpression.Name = "lblLeftExpression";
            lblLeftExpression.Size = new Size(100, 25);
            lblLeftExpression.TabIndex = 1;
            lblLeftExpression.Text = "左值表达式:";
            lblLeftExpression.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblConditionTitle
            // 
            lblConditionTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblConditionTitle.ForeColor = Color.FromArgb(65, 100, 204);
            lblConditionTitle.Location = new Point(18, 5);
            lblConditionTitle.Name = "lblConditionTitle";
            lblConditionTitle.Size = new Size(200, 25);
            lblConditionTitle.TabIndex = 0;
            lblConditionTitle.Text = "条件表达式配置";
            lblConditionTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(btnHelp);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 600);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(15, 10, 15, 10);
            panelBottom.Size = new Size(900, 60);
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
            btnCancel.Location = new Point(672, 13);
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
            btnSave.Location = new Point(782, 13);
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
            // Form_Condition
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(900, 660);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelDescription);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Condition";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "条件判断配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            ZoomScaleRect = new Rectangle(15, 15, 900, 660);
            panelDescription.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            panelChildSteps.ResumeLayout(false);
            panelCondition.ResumeLayout(false);
            panelRangeValue.ResumeLayout(false);
            panelSingleValue.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelDescription;
        private UICheckBox chkEnabled;
        private UITextBox txtDescription;
        private UILabel lblDescription;
        private Panel panelMain;
        private UIPanel panelCondition;
        private UISymbolButton btnSelectVarLeft;
        private UITextBox txtLeftExpression;
        private UILabel lblLeftExpression;
        private UILabel lblConditionTitle;
        private UIComboBox cmbOperator;
        private UILabel lblOperator;
        private Panel panelSingleValue;
        private UISymbolButton btnSelectVarRight;
        private UITextBox txtRightExpression;
        private UILabel lblRightExpression;
        private Panel panelRangeValue;
        private UISymbolButton btnSelectVarRangeMax;
        private UITextBox txtRangeMax;
        private UILabel lblRangeMax;
        private UISymbolButton btnSelectVarRangeMin;
        private UITextBox txtRangeMin;
        private UILabel lblRangeMin;
        private UIPanel panelChildSteps;
        private UILabel lblChildStepsTitle;
        private UIButton btnConfigTrueSteps;
        private UILabel lblTrueStepsCount;
        private UIButton btnConfigFalseSteps;
        private UILabel lblFalseStepsCount;
        private Panel panelBottom;
        private UISymbolButton btnHelp;
        private UISymbolButton btnCancel;
        private UISymbolButton btnSave;
    }
}
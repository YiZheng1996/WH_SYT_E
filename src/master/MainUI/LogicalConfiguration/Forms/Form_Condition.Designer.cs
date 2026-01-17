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
            lblValidationStatus = new AntdUI.Label();
            txtConditionExpression = new UITextBox();
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
            panelDescription.Size = new Size(805, 70);
            panelDescription.TabIndex = 1;
            // 
            // chkEnabled
            // 
            chkEnabled.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkEnabled.CheckBoxSize = 18;
            chkEnabled.Checked = true;
            chkEnabled.Font = new Font("微软雅黑", 10F);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(665, 14);
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
            txtDescription.Size = new Size(532, 30);
            txtDescription.TabIndex = 1;
            txtDescription.Text = "条件判断工具\r\n根据条件表达式的结果决定执行不同的步骤分支\r\n";
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
            panelMain.Size = new Size(805, 437);
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
            panelChildSteps.Size = new Size(775, 152);
            panelChildSteps.TabIndex = 1;
            panelChildSteps.Text = null;
            panelChildSteps.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnConfigFalseSteps
            // 
            btnConfigFalseSteps.Cursor = Cursors.Hand;
            btnConfigFalseSteps.Font = new Font("微软雅黑", 10F);
            btnConfigFalseSteps.Location = new Point(428, 93);
            btnConfigFalseSteps.MinimumSize = new Size(1, 1);
            btnConfigFalseSteps.Name = "btnConfigFalseSteps";
            btnConfigFalseSteps.Size = new Size(150, 35);
            btnConfigFalseSteps.TabIndex = 4;
            btnConfigFalseSteps.Text = "配置步骤...";
            btnConfigFalseSteps.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // lblFalseStepsCount
            // 
            lblFalseStepsCount.BackColor = Color.FromArgb(255, 245, 245);
            lblFalseStepsCount.Font = new Font("微软雅黑", 10F);
            lblFalseStepsCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblFalseStepsCount.Location = new Point(18, 96);
            lblFalseStepsCount.Name = "lblFalseStepsCount";
            lblFalseStepsCount.Size = new Size(390, 25);
            lblFalseStepsCount.TabIndex = 3;
            lblFalseStepsCount.Text = "不满足条件时执行的步骤 (0 个)";
            lblFalseStepsCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnConfigTrueSteps
            // 
            btnConfigTrueSteps.Cursor = Cursors.Hand;
            btnConfigTrueSteps.Font = new Font("微软雅黑", 10F);
            btnConfigTrueSteps.Location = new Point(428, 36);
            btnConfigTrueSteps.MinimumSize = new Size(1, 1);
            btnConfigTrueSteps.Name = "btnConfigTrueSteps";
            btnConfigTrueSteps.Size = new Size(150, 35);
            btnConfigTrueSteps.TabIndex = 2;
            btnConfigTrueSteps.Text = "配置步骤...";
            btnConfigTrueSteps.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // lblTrueStepsCount
            // 
            lblTrueStepsCount.BackColor = Color.Honeydew;
            lblTrueStepsCount.Font = new Font("微软雅黑", 10F);
            lblTrueStepsCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblTrueStepsCount.Location = new Point(18, 40);
            lblTrueStepsCount.Name = "lblTrueStepsCount";
            lblTrueStepsCount.Size = new Size(390, 25);
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
            panelCondition.Controls.Add(lblValidationStatus);
            panelCondition.Controls.Add(txtConditionExpression);
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
            panelCondition.Size = new Size(775, 265);
            panelCondition.TabIndex = 0;
            panelCondition.Text = null;
            panelCondition.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblValidationStatus
            // 
            lblValidationStatus.Font = new Font("微软雅黑", 8.5F);
            lblValidationStatus.ForeColor = Color.Gray;
            lblValidationStatus.Location = new Point(124, 227);
            lblValidationStatus.Name = "lblValidationStatus";
            lblValidationStatus.Size = new Size(617, 20);
            lblValidationStatus.TabIndex = 3;
            lblValidationStatus.Text = "准备就绪";
            // 
            // txtConditionExpression
            // 
            txtConditionExpression.Cursor = Cursors.IBeam;
            txtConditionExpression.Font = new Font("微软雅黑", 10F);
            txtConditionExpression.Location = new Point(124, 45);
            txtConditionExpression.Margin = new Padding(4, 5, 4, 5);
            txtConditionExpression.MinimumSize = new Size(1, 16);
            txtConditionExpression.Multiline = true;
            txtConditionExpression.Name = "txtConditionExpression";
            txtConditionExpression.Padding = new Padding(5);
            txtConditionExpression.RectColor = Color.FromArgb(65, 100, 204);
            txtConditionExpression.ShowText = false;
            txtConditionExpression.Size = new Size(617, 170);
            txtConditionExpression.TabIndex = 2;
            txtConditionExpression.TextAlignment = ContentAlignment.MiddleLeft;
            txtConditionExpression.Watermark = "点击输入条件表达式，如：{温度} > 100 (按F2打开面板)";
            // 
            // lblLeftExpression
            // 
            lblLeftExpression.Font = new Font("微软雅黑", 10F);
            lblLeftExpression.ForeColor = Color.FromArgb(48, 48, 48);
            lblLeftExpression.Location = new Point(18, 45);
            lblLeftExpression.Name = "lblLeftExpression";
            lblLeftExpression.Size = new Size(100, 25);
            lblLeftExpression.TabIndex = 1;
            lblLeftExpression.Text = "条件表达式:";
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
            panelBottom.Location = new Point(0, 542);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(15, 10, 15, 10);
            panelBottom.Size = new Size(805, 65);
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
            btnCancel.Location = new Point(677, 13);
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
            btnSave.Location = new Point(556, 13);
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
            ClientSize = new Size(805, 607);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelDescription);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Condition";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "条件判断配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 900, 660);
            panelDescription.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            panelChildSteps.ResumeLayout(false);
            panelCondition.ResumeLayout(false);
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
        private UITextBox txtConditionExpression;
        private UILabel lblLeftExpression;
        private UILabel lblConditionTitle;
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
        private AntdUI.Label lblValidationStatus;
    }
}
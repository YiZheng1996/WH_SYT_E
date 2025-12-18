using MainUI.LogicalConfiguration.Parameter;
using Sunny.UI;

namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_VariableAssignment
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
            components = new System.ComponentModel.Container();
            grpBasicConfig = new UIPanel();
            txtDescription = new UITextBox();
            lblDescription = new UILabel();
            txtAssignmentContent = new UITextBox();
            lblAssignmentContent = new UILabel();
            txtTargetVariable = new UITextBox();
            lblTargetVariable = new UILabel();
            chkEnabled = new UICheckBox();
            uiLine1 = new UILine();
            pnlButtons = new UIPanel();
            btnHelp = new UISymbolButton();
            btnCancel = new UISymbolButton();
            btnOK = new UISymbolButton();
            toolTip = new ToolTip(components);
            grpBasicConfig.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // grpBasicConfig
            // 
            grpBasicConfig.Controls.Add(txtDescription);
            grpBasicConfig.Controls.Add(lblDescription);
            grpBasicConfig.Controls.Add(txtAssignmentContent);
            grpBasicConfig.Controls.Add(lblAssignmentContent);
            grpBasicConfig.Controls.Add(txtTargetVariable);
            grpBasicConfig.Controls.Add(lblTargetVariable);
            grpBasicConfig.Controls.Add(chkEnabled);
            grpBasicConfig.Controls.Add(uiLine1);
            grpBasicConfig.FillColor = Color.White;
            grpBasicConfig.FillColor2 = Color.White;
            grpBasicConfig.Font = new Font("微软雅黑", 9F);
            grpBasicConfig.Location = new Point(15, 45);
            grpBasicConfig.Margin = new Padding(4, 5, 4, 5);
            grpBasicConfig.MinimumSize = new Size(1, 1);
            grpBasicConfig.Name = "grpBasicConfig";
            grpBasicConfig.Radius = 8;
            grpBasicConfig.RectColor = Color.FromArgb(65, 100, 204);
            grpBasicConfig.Size = new Size(770, 420);
            grpBasicConfig.TabIndex = 0;
            grpBasicConfig.Text = null;
            grpBasicConfig.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("微软雅黑", 10F);
            txtDescription.Location = new Point(130, 280);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.RectColor = Color.FromArgb(65, 100, 204);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(600, 90);
            txtDescription.TabIndex = 7;
            txtDescription.TextAlignment = ContentAlignment.TopLeft;
            toolTip.SetToolTip(txtDescription, "输入对该赋值操作的说明");
            txtDescription.Watermark = "输入步骤描述（可选）";
            // 
            // lblDescription
            // 
            lblDescription.BackColor = Color.Transparent;
            lblDescription.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(30, 280);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(90, 30);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "步骤描述";
            lblDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtAssignmentContent
            // 
            txtAssignmentContent.Font = new Font("微软雅黑", 10F);
            txtAssignmentContent.Location = new Point(130, 140);
            txtAssignmentContent.Margin = new Padding(4, 5, 4, 5);
            txtAssignmentContent.MinimumSize = new Size(1, 16);
            txtAssignmentContent.Multiline = true;
            txtAssignmentContent.Name = "txtAssignmentContent";
            txtAssignmentContent.Padding = new Padding(5);
            txtAssignmentContent.RectColor = Color.FromArgb(65, 100, 204);
            txtAssignmentContent.ShowText = false;
            txtAssignmentContent.Size = new Size(600, 120);
            txtAssignmentContent.TabIndex = 5;
            txtAssignmentContent.TextAlignment = ContentAlignment.TopLeft;
            toolTip.SetToolTip(txtAssignmentContent, "点击打开智能输入面板，支持多种数据源");
            txtAssignmentContent.Watermark = "点击配置赋值内容，支持固定值/变量/表达式/PLC (按F2打开面板)";
            // 
            // lblAssignmentContent
            // 
            lblAssignmentContent.BackColor = Color.Transparent;
            lblAssignmentContent.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblAssignmentContent.ForeColor = Color.FromArgb(48, 48, 48);
            lblAssignmentContent.Location = new Point(30, 140);
            lblAssignmentContent.Name = "lblAssignmentContent";
            lblAssignmentContent.Size = new Size(90, 30);
            lblAssignmentContent.TabIndex = 4;
            lblAssignmentContent.Text = "赋值内容";
            lblAssignmentContent.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTargetVariable
            // 
            txtTargetVariable.Font = new Font("微软雅黑", 10F);
            txtTargetVariable.Location = new Point(130, 70);
            txtTargetVariable.Margin = new Padding(4, 5, 4, 5);
            txtTargetVariable.MinimumSize = new Size(1, 16);
            txtTargetVariable.Name = "txtTargetVariable";
            txtTargetVariable.Padding = new Padding(5);
            txtTargetVariable.RectColor = Color.FromArgb(65, 100, 204);
            txtTargetVariable.ShowText = false;
            txtTargetVariable.Size = new Size(600, 40);
            txtTargetVariable.TabIndex = 3;
            txtTargetVariable.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtTargetVariable, "选择要赋值的目标变量");
            txtTargetVariable.Watermark = "点击选择目标变量 (按F2打开面板)";
            // 
            // lblTargetVariable
            // 
            lblTargetVariable.BackColor = Color.Transparent;
            lblTargetVariable.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblTargetVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblTargetVariable.Location = new Point(30, 70);
            lblTargetVariable.Name = "lblTargetVariable";
            lblTargetVariable.Size = new Size(90, 30);
            lblTargetVariable.TabIndex = 2;
            lblTargetVariable.Text = "目标变量";
            lblTargetVariable.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkEnabled
            // 
            chkEnabled.BackColor = Color.Transparent;
            chkEnabled.Checked = true;
            chkEnabled.Font = new Font("微软雅黑", 10F);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(130, 385);
            chkEnabled.MinimumSize = new Size(1, 1);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(150, 23);
            chkEnabled.TabIndex = 8;
            chkEnabled.Text = "启用该步骤";
            toolTip.SetToolTip(chkEnabled, "取消勾选将跳过该步骤的执行");
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(65, 100, 204);
            uiLine1.LineColor = Color.FromArgb(65, 100, 204);
            uiLine1.Location = new Point(20, 15);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(730, 35);
            uiLine1.TabIndex = 1;
            uiLine1.Text = "变量赋值配置";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnHelp);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnOK);
            pnlButtons.FillColor = Color.White;
            pnlButtons.FillColor2 = Color.White;
            pnlButtons.Font = new Font("微软雅黑", 9F);
            pnlButtons.Location = new Point(15, 475);
            pnlButtons.Margin = new Padding(4, 5, 4, 5);
            pnlButtons.MinimumSize = new Size(1, 1);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Radius = 8;
            pnlButtons.RectColor = Color.FromArgb(65, 100, 204);
            pnlButtons.Size = new Size(770, 70);
            pnlButtons.TabIndex = 1;
            pnlButtons.Text = null;
            pnlButtons.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.FillColor = Color.FromArgb(23, 162, 184);
            btnHelp.FillColor2 = Color.FromArgb(23, 162, 184);
            btnHelp.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnHelp.Location = new Point(30, 17);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.RectColor = Color.FromArgb(23, 162, 184);
            btnHelp.RectDisableColor = Color.FromArgb(23, 162, 184);
            btnHelp.Size = new Size(130, 35);
            btnHelp.Symbol = 61529;
            btnHelp.TabIndex = 3;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnHelp, "查看变量赋值工具的使用帮助");
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(108, 117, 125);
            btnCancel.FillColor2 = Color.FromArgb(108, 117, 125);
            btnCancel.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnCancel.Location = new Point(610, 17);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(108, 117, 125);
            btnCancel.RectDisableColor = Color.FromArgb(108, 117, 125);
            btnCancel.Size = new Size(130, 35);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnCancel, "取消操作并关闭窗口");
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.FillColor = Color.FromArgb(40, 167, 69);
            btnOK.FillColor2 = Color.FromArgb(40, 167, 69);
            btnOK.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnOK.Location = new Point(460, 17);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.RectColor = Color.FromArgb(40, 167, 69);
            btnOK.RectDisableColor = Color.FromArgb(40, 167, 69);
            btnOK.Size = new Size(130, 35);
            btnOK.Symbol = 61528;
            btnOK.TabIndex = 0;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnOK, "确认并应用变量赋值配置");
            // 
            // Form_VariableAssignment
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 560);
            ControlBox = false;
            Controls.Add(pnlButtons);
            Controls.Add(grpBasicConfig);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_VariableAssignment";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "变量赋值工具";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 560);
            grpBasicConfig.ResumeLayout(false);
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private UIPanel grpBasicConfig;
        private UILine uiLine1;
        private UILabel lblTargetVariable;
        private UITextBox txtTargetVariable;
        private UILabel lblAssignmentContent;
        private UITextBox txtAssignmentContent;
        private UILabel lblDescription;
        private UITextBox txtDescription;
        private UICheckBox chkEnabled;

        private UIPanel pnlButtons;
        private UISymbolButton btnOK;
        private UISymbolButton btnCancel;
        private UISymbolButton btnHelp;

        private System.Windows.Forms.ToolTip toolTip;
    }
}
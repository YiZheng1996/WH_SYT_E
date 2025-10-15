using MainUI.LogicalConfiguration.Parameter;

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
                _validationTimer?.Dispose();
                _previewTimer?.Dispose();
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
            pnlPlcSource = new UIPanel();
            CboPlcAddress = new UIComboBox();
            CboPlcModule = new UIComboBox();
            lblPlcModule = new Label();
            lblPlcAddress = new Label();
            pnlVoluationSource = new UIPanel();
            txtAssignmentContent = new UITextBox();
            btnExpressionBuilder = new UISymbolButton();
            uiLine1 = new UILine();
            lblTargetVariable = new UILabel();
            cmbTargetVariable = new UIComboBox();
            lblAssignmentType = new UILabel();
            cmbAssignmentType = new UIComboBox();
            lblAssignmentContent = new UILabel();
            grpAdvancedConfig = new UIPanel();
            uiLine2 = new UILine();
            lblCondition = new UILabel();
            txtCondition = new UITextBox();
            lblDescription = new UILabel();
            txtDescription = new UITextBox();
            chkEnabled = new UICheckBox();
            grpPreview = new UIPanel();
            uiLine3 = new UILine();
            lblValidation = new UILabel();
            rtbValidationResult = new UIRichTextBox();
            lblPreview = new UILabel();
            rtbPreviewResult = new UIRichTextBox();
            pnlButtons = new UIPanel();
            btnOK = new UISymbolButton();
            btnCancel = new UISymbolButton();
            btnTest = new UISymbolButton();
            btnHelp = new UISymbolButton();
            toolTip = new ToolTip(components);
            grpBasicConfig.SuspendLayout();
            pnlPlcSource.SuspendLayout();
            pnlVoluationSource.SuspendLayout();
            grpAdvancedConfig.SuspendLayout();
            grpPreview.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // grpBasicConfig
            // 
            grpBasicConfig.Controls.Add(pnlPlcSource);
            grpBasicConfig.Controls.Add(pnlVoluationSource);
            grpBasicConfig.Controls.Add(uiLine1);
            grpBasicConfig.Controls.Add(lblTargetVariable);
            grpBasicConfig.Controls.Add(cmbTargetVariable);
            grpBasicConfig.Controls.Add(lblAssignmentType);
            grpBasicConfig.Controls.Add(cmbAssignmentType);
            grpBasicConfig.Controls.Add(lblAssignmentContent);
            grpBasicConfig.FillColor = Color.White;
            grpBasicConfig.FillColor2 = Color.White;
            grpBasicConfig.Font = new Font("微软雅黑", 9F);
            grpBasicConfig.Location = new Point(15, 45);
            grpBasicConfig.Margin = new Padding(4, 5, 4, 5);
            grpBasicConfig.MinimumSize = new Size(1, 1);
            grpBasicConfig.Name = "grpBasicConfig";
            grpBasicConfig.Radius = 8;
            grpBasicConfig.RectColor = Color.FromArgb(65, 100, 204);
            grpBasicConfig.Size = new Size(454, 280);
            grpBasicConfig.TabIndex = 0;
            grpBasicConfig.Text = null;
            grpBasicConfig.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pnlPlcSource
            // 
            pnlPlcSource.BackColor = Color.Transparent;
            pnlPlcSource.Controls.Add(CboPlcAddress);
            pnlPlcSource.Controls.Add(CboPlcModule);
            pnlPlcSource.Controls.Add(lblPlcModule);
            pnlPlcSource.Controls.Add(lblPlcAddress);
            pnlPlcSource.FillColor = Color.White;
            pnlPlcSource.FillColor2 = Color.White;
            pnlPlcSource.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlPlcSource.Location = new Point(117, 140);
            pnlPlcSource.Margin = new Padding(4, 5, 4, 5);
            pnlPlcSource.MinimumSize = new Size(1, 1);
            pnlPlcSource.Name = "pnlPlcSource";
            pnlPlcSource.RectColor = Color.White;
            pnlPlcSource.RectDisableColor = Color.White;
            pnlPlcSource.Size = new Size(325, 130);
            pnlPlcSource.TabIndex = 4;
            pnlPlcSource.Text = null;
            pnlPlcSource.TextAlignment = ContentAlignment.MiddleCenter;
            pnlPlcSource.Visible = false;
            // 
            // CboPlcAddress
            // 
            CboPlcAddress.BackColor = Color.Transparent;
            CboPlcAddress.DataSource = null;
            CboPlcAddress.DropDownStyle = UIDropDownStyle.DropDownList;
            CboPlcAddress.FillColor = Color.White;
            CboPlcAddress.FillColor2 = Color.White;
            CboPlcAddress.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboPlcAddress.FilterMaxCount = 50;
            CboPlcAddress.Font = new Font("微软雅黑", 10F);
            CboPlcAddress.ForeDisableColor = Color.FromArgb(48, 48, 48);
            CboPlcAddress.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboPlcAddress.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboPlcAddress.Location = new Point(91, 70);
            CboPlcAddress.Margin = new Padding(4, 5, 4, 5);
            CboPlcAddress.MinimumSize = new Size(63, 0);
            CboPlcAddress.Name = "CboPlcAddress";
            CboPlcAddress.Padding = new Padding(0, 0, 30, 2);
            CboPlcAddress.RectColor = Color.FromArgb(65, 100, 204);
            CboPlcAddress.RectDisableColor = Color.Gray;
            CboPlcAddress.Size = new Size(222, 30);
            CboPlcAddress.SymbolSize = 24;
            CboPlcAddress.TabIndex = 125;
            CboPlcAddress.TextAlignment = ContentAlignment.MiddleLeft;
            CboPlcAddress.Watermark = "请选择目标地址";
            CboPlcAddress.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            CboPlcAddress.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // CboPlcModule
            // 
            CboPlcModule.BackColor = Color.Transparent;
            CboPlcModule.DataSource = null;
            CboPlcModule.DropDownStyle = UIDropDownStyle.DropDownList;
            CboPlcModule.FillColor = Color.White;
            CboPlcModule.FillColor2 = Color.White;
            CboPlcModule.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboPlcModule.FilterMaxCount = 50;
            CboPlcModule.Font = new Font("微软雅黑", 10F);
            CboPlcModule.ForeDisableColor = Color.FromArgb(48, 48, 48);
            CboPlcModule.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboPlcModule.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboPlcModule.Location = new Point(91, 18);
            CboPlcModule.Margin = new Padding(4, 5, 4, 5);
            CboPlcModule.MinimumSize = new Size(63, 0);
            CboPlcModule.Name = "CboPlcModule";
            CboPlcModule.Padding = new Padding(0, 0, 30, 2);
            CboPlcModule.RectColor = Color.FromArgb(65, 100, 204);
            CboPlcModule.RectDisableColor = Color.Gray;
            CboPlcModule.Size = new Size(222, 30);
            CboPlcModule.SymbolSize = 24;
            CboPlcModule.TabIndex = 124;
            CboPlcModule.TextAlignment = ContentAlignment.MiddleLeft;
            CboPlcModule.Watermark = "请选择目标模块";
            CboPlcModule.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            CboPlcModule.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // lblPlcModule
            // 
            lblPlcModule.AutoSize = true;
            lblPlcModule.Font = new Font("微软雅黑", 10F);
            lblPlcModule.Location = new Point(18, 21);
            lblPlcModule.Name = "lblPlcModule";
            lblPlcModule.Size = new Size(65, 20);
            lblPlcModule.TabIndex = 0;
            lblPlcModule.Text = "PLC模块:";
            // 
            // lblPlcAddress
            // 
            lblPlcAddress.AutoSize = true;
            lblPlcAddress.Font = new Font("微软雅黑", 10F);
            lblPlcAddress.Location = new Point(18, 73);
            lblPlcAddress.Name = "lblPlcAddress";
            lblPlcAddress.Size = new Size(65, 20);
            lblPlcAddress.TabIndex = 2;
            lblPlcAddress.Text = "PLC地址:";
            // 
            // pnlVoluationSource
            // 
            pnlVoluationSource.BackColor = Color.Transparent;
            pnlVoluationSource.Controls.Add(txtAssignmentContent);
            pnlVoluationSource.Controls.Add(btnExpressionBuilder);
            pnlVoluationSource.FillColor = Color.White;
            pnlVoluationSource.FillColor2 = Color.White;
            pnlVoluationSource.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlVoluationSource.Location = new Point(117, 140);
            pnlVoluationSource.Margin = new Padding(4, 5, 4, 5);
            pnlVoluationSource.MinimumSize = new Size(1, 1);
            pnlVoluationSource.Name = "pnlVoluationSource";
            pnlVoluationSource.RectColor = Color.White;
            pnlVoluationSource.RectDisableColor = Color.White;
            pnlVoluationSource.Size = new Size(325, 130);
            pnlVoluationSource.TabIndex = 5;
            pnlVoluationSource.Text = null;
            pnlVoluationSource.TextAlignment = ContentAlignment.MiddleCenter;
            pnlVoluationSource.Visible = false;
            // 
            // txtAssignmentContent
            // 
            txtAssignmentContent.AutoScroll = true;
            txtAssignmentContent.Cursor = Cursors.IBeam;
            txtAssignmentContent.Font = new Font("微软雅黑", 10F);
            txtAssignmentContent.Location = new Point(13, 5);
            txtAssignmentContent.Margin = new Padding(4, 5, 4, 5);
            txtAssignmentContent.MinimumSize = new Size(1, 16);
            txtAssignmentContent.Multiline = true;
            txtAssignmentContent.Name = "txtAssignmentContent";
            txtAssignmentContent.Padding = new Padding(5);
            txtAssignmentContent.RectColor = Color.FromArgb(65, 100, 204);
            txtAssignmentContent.ShowText = false;
            txtAssignmentContent.Size = new Size(269, 121);
            txtAssignmentContent.TabIndex = 6;
            txtAssignmentContent.TextAlignment = ContentAlignment.TopLeft;
            toolTip.SetToolTip(txtAssignmentContent, "输入要赋给目标变量的值或表达式");
            txtAssignmentContent.Watermark = "输入赋值内容或表达式...";
            // 
            // btnExpressionBuilder
            // 
            btnExpressionBuilder.Cursor = Cursors.Hand;
            btnExpressionBuilder.FillColor = Color.FromArgb(65, 100, 204);
            btnExpressionBuilder.FillColor2 = Color.FromArgb(65, 100, 204);
            btnExpressionBuilder.Font = new Font("微软雅黑", 9F);
            btnExpressionBuilder.Location = new Point(289, 5);
            btnExpressionBuilder.MinimumSize = new Size(1, 1);
            btnExpressionBuilder.Name = "btnExpressionBuilder";
            btnExpressionBuilder.Size = new Size(27, 25);
            btnExpressionBuilder.Symbol = 61729;
            btnExpressionBuilder.SymbolSize = 16;
            btnExpressionBuilder.TabIndex = 7;
            btnExpressionBuilder.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnExpressionBuilder, "打开表达式构建器");
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.FromArgb(65, 100, 204);
            uiLine1.Location = new Point(15, 10);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(418, 29);
            uiLine1.TabIndex = 0;
            uiLine1.Text = "基础配置";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTargetVariable
            // 
            lblTargetVariable.BackColor = Color.Transparent;
            lblTargetVariable.Font = new Font("微软雅黑", 10F);
            lblTargetVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblTargetVariable.Location = new Point(15, 50);
            lblTargetVariable.Name = "lblTargetVariable";
            lblTargetVariable.Size = new Size(100, 23);
            lblTargetVariable.TabIndex = 1;
            lblTargetVariable.Text = "目标变量 *";
            lblTargetVariable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbTargetVariable
            // 
            cmbTargetVariable.DataSource = null;
            cmbTargetVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbTargetVariable.FillColor = Color.White;
            cmbTargetVariable.Font = new Font("微软雅黑", 10F);
            cmbTargetVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbTargetVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbTargetVariable.Location = new Point(130, 50);
            cmbTargetVariable.Margin = new Padding(4, 5, 4, 5);
            cmbTargetVariable.MinimumSize = new Size(63, 0);
            cmbTargetVariable.Name = "cmbTargetVariable";
            cmbTargetVariable.Padding = new Padding(0, 0, 30, 2);
            cmbTargetVariable.RectColor = Color.FromArgb(65, 100, 204);
            cmbTargetVariable.Size = new Size(303, 29);
            cmbTargetVariable.SymbolSize = 24;
            cmbTargetVariable.TabIndex = 2;
            cmbTargetVariable.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbTargetVariable, "选择要进行赋值操作的目标变量");
            cmbTargetVariable.Watermark = "请选择目标变量...";
            // 
            // lblAssignmentType
            // 
            lblAssignmentType.BackColor = Color.Transparent;
            lblAssignmentType.Font = new Font("微软雅黑", 10F);
            lblAssignmentType.ForeColor = Color.FromArgb(48, 48, 48);
            lblAssignmentType.Location = new Point(15, 95);
            lblAssignmentType.Name = "lblAssignmentType";
            lblAssignmentType.Size = new Size(100, 23);
            lblAssignmentType.TabIndex = 3;
            lblAssignmentType.Text = "赋值方式";
            lblAssignmentType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbAssignmentType
            // 
            cmbAssignmentType.DataSource = null;
            cmbAssignmentType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbAssignmentType.FillColor = Color.White;
            cmbAssignmentType.Font = new Font("微软雅黑", 10F);
            cmbAssignmentType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbAssignmentType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbAssignmentType.Location = new Point(130, 95);
            cmbAssignmentType.Margin = new Padding(4, 5, 4, 5);
            cmbAssignmentType.MinimumSize = new Size(63, 0);
            cmbAssignmentType.Name = "cmbAssignmentType";
            cmbAssignmentType.Padding = new Padding(0, 0, 30, 2);
            cmbAssignmentType.RectColor = Color.FromArgb(65, 100, 204);
            cmbAssignmentType.Size = new Size(303, 29);
            cmbAssignmentType.SymbolSize = 24;
            cmbAssignmentType.TabIndex = 4;
            cmbAssignmentType.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbAssignmentType, "选择变量赋值的方式");
            cmbAssignmentType.Watermark = "";
            // 
            // lblAssignmentContent
            // 
            lblAssignmentContent.BackColor = Color.Transparent;
            lblAssignmentContent.Font = new Font("微软雅黑", 10F);
            lblAssignmentContent.ForeColor = Color.FromArgb(48, 48, 48);
            lblAssignmentContent.Location = new Point(15, 140);
            lblAssignmentContent.Name = "lblAssignmentContent";
            lblAssignmentContent.Size = new Size(100, 23);
            lblAssignmentContent.TabIndex = 5;
            lblAssignmentContent.Text = "赋值内容 *";
            lblAssignmentContent.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpAdvancedConfig
            // 
            grpAdvancedConfig.Controls.Add(uiLine2);
            grpAdvancedConfig.Controls.Add(lblCondition);
            grpAdvancedConfig.Controls.Add(txtCondition);
            grpAdvancedConfig.Controls.Add(lblDescription);
            grpAdvancedConfig.Controls.Add(txtDescription);
            grpAdvancedConfig.Controls.Add(chkEnabled);
            grpAdvancedConfig.FillColor = Color.White;
            grpAdvancedConfig.FillColor2 = Color.White;
            grpAdvancedConfig.Font = new Font("微软雅黑", 9F);
            grpAdvancedConfig.Location = new Point(15, 335);
            grpAdvancedConfig.Margin = new Padding(4, 5, 4, 5);
            grpAdvancedConfig.MinimumSize = new Size(1, 1);
            grpAdvancedConfig.Name = "grpAdvancedConfig";
            grpAdvancedConfig.Radius = 8;
            grpAdvancedConfig.RectColor = Color.FromArgb(65, 100, 204);
            grpAdvancedConfig.Size = new Size(454, 265);
            grpAdvancedConfig.TabIndex = 1;
            grpAdvancedConfig.Text = null;
            grpAdvancedConfig.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.FromArgb(65, 100, 204);
            uiLine2.Location = new Point(15, 10);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(370, 29);
            uiLine2.TabIndex = 0;
            uiLine2.Text = "高级配置";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCondition
            // 
            lblCondition.BackColor = Color.Transparent;
            lblCondition.Font = new Font("微软雅黑", 10F);
            lblCondition.ForeColor = Color.FromArgb(48, 48, 48);
            lblCondition.Location = new Point(15, 50);
            lblCondition.Name = "lblCondition";
            lblCondition.Size = new Size(100, 23);
            lblCondition.TabIndex = 1;
            lblCondition.Text = "执行条件";
            lblCondition.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCondition
            // 
            txtCondition.Cursor = Cursors.IBeam;
            txtCondition.Font = new Font("微软雅黑", 10F);
            txtCondition.Location = new Point(130, 50);
            txtCondition.Margin = new Padding(4, 5, 4, 5);
            txtCondition.MinimumSize = new Size(1, 16);
            txtCondition.Name = "txtCondition";
            txtCondition.Padding = new Padding(5);
            txtCondition.RectColor = Color.FromArgb(65, 100, 204);
            txtCondition.ShowText = false;
            txtCondition.Size = new Size(255, 29);
            txtCondition.TabIndex = 2;
            txtCondition.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtCondition, "设置赋值操作的执行条件，为空则总是执行");
            txtCondition.Watermark = "可选：执行条件表达式";
            // 
            // lblDescription
            // 
            lblDescription.BackColor = Color.Transparent;
            lblDescription.Font = new Font("微软雅黑", 10F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(15, 95);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(100, 23);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "描述说明";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            txtDescription.AutoScroll = true;
            txtDescription.Cursor = Cursors.IBeam;
            txtDescription.Font = new Font("微软雅黑", 10F);
            txtDescription.Location = new Point(130, 95);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.RectColor = Color.FromArgb(65, 100, 204);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(255, 119);
            txtDescription.TabIndex = 4;
            txtDescription.TextAlignment = ContentAlignment.TopLeft;
            toolTip.SetToolTip(txtDescription, "对此赋值操作进行详细描述，便于后期维护");
            txtDescription.Watermark = "可选：描述此赋值操作的用途";
            // 
            // chkEnabled
            // 
            chkEnabled.BackColor = Color.Transparent;
            chkEnabled.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkEnabled.Checked = true;
            chkEnabled.Font = new Font("微软雅黑", 10F);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(130, 222);
            chkEnabled.MinimumSize = new Size(1, 1);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(150, 29);
            chkEnabled.TabIndex = 5;
            chkEnabled.Text = "启用此赋值操作";
            toolTip.SetToolTip(chkEnabled, "勾选启用，取消勾选则跳过此赋值操作");
            // 
            // grpPreview
            // 
            grpPreview.Controls.Add(uiLine3);
            grpPreview.Controls.Add(lblValidation);
            grpPreview.Controls.Add(rtbValidationResult);
            grpPreview.Controls.Add(lblPreview);
            grpPreview.Controls.Add(rtbPreviewResult);
            grpPreview.FillColor = Color.White;
            grpPreview.FillColor2 = Color.White;
            grpPreview.Font = new Font("微软雅黑", 9F);
            grpPreview.Location = new Point(477, 45);
            grpPreview.Margin = new Padding(4, 5, 4, 5);
            grpPreview.MinimumSize = new Size(1, 1);
            grpPreview.Name = "grpPreview";
            grpPreview.Radius = 8;
            grpPreview.RectColor = Color.FromArgb(65, 100, 204);
            grpPreview.Size = new Size(579, 555);
            grpPreview.TabIndex = 2;
            grpPreview.Text = null;
            grpPreview.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine3
            // 
            uiLine3.BackColor = Color.Transparent;
            uiLine3.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLine3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine3.LineColor = Color.FromArgb(65, 100, 204);
            uiLine3.Location = new Point(15, 10);
            uiLine3.MinimumSize = new Size(1, 1);
            uiLine3.Name = "uiLine3";
            uiLine3.Size = new Size(551, 29);
            uiLine3.TabIndex = 0;
            uiLine3.Text = "实时预览与验证";
            uiLine3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblValidation
            // 
            lblValidation.BackColor = Color.Transparent;
            lblValidation.Font = new Font("微软雅黑", 10F);
            lblValidation.ForeColor = Color.FromArgb(48, 48, 48);
            lblValidation.Location = new Point(15, 50);
            lblValidation.Name = "lblValidation";
            lblValidation.Size = new Size(100, 23);
            lblValidation.TabIndex = 1;
            lblValidation.Text = "验证结果";
            lblValidation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // rtbValidationResult
            // 
            rtbValidationResult.FillColor = Color.FromArgb(248, 248, 248);
            rtbValidationResult.Font = new Font("微软雅黑", 9F);
            rtbValidationResult.Location = new Point(15, 75);
            rtbValidationResult.Margin = new Padding(4, 5, 4, 5);
            rtbValidationResult.MinimumSize = new Size(1, 1);
            rtbValidationResult.Name = "rtbValidationResult";
            rtbValidationResult.Padding = new Padding(2);
            rtbValidationResult.ReadOnly = true;
            rtbValidationResult.RectColor = Color.FromArgb(65, 100, 204);
            rtbValidationResult.ShowText = false;
            rtbValidationResult.Size = new Size(551, 205);
            rtbValidationResult.TabIndex = 2;
            rtbValidationResult.TextAlignment = ContentAlignment.MiddleCenter;
            toolTip.SetToolTip(rtbValidationResult, "显示配置验证的结果");
            // 
            // lblPreview
            // 
            lblPreview.BackColor = Color.Transparent;
            lblPreview.Font = new Font("微软雅黑", 10F);
            lblPreview.ForeColor = Color.FromArgb(48, 48, 48);
            lblPreview.Location = new Point(15, 290);
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new Size(100, 23);
            lblPreview.TabIndex = 3;
            lblPreview.Text = "预览结果";
            lblPreview.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // rtbPreviewResult
            // 
            rtbPreviewResult.FillColor = Color.FromArgb(248, 248, 248);
            rtbPreviewResult.Font = new Font("微软雅黑", 9F);
            rtbPreviewResult.Location = new Point(15, 315);
            rtbPreviewResult.Margin = new Padding(4, 5, 4, 5);
            rtbPreviewResult.MinimumSize = new Size(1, 1);
            rtbPreviewResult.Name = "rtbPreviewResult";
            rtbPreviewResult.Padding = new Padding(2);
            rtbPreviewResult.ReadOnly = true;
            rtbPreviewResult.RectColor = Color.FromArgb(65, 100, 204);
            rtbPreviewResult.ShowText = false;
            rtbPreviewResult.Size = new Size(551, 226);
            rtbPreviewResult.TabIndex = 4;
            rtbPreviewResult.TextAlignment = ContentAlignment.MiddleCenter;
            toolTip.SetToolTip(rtbPreviewResult, "显示赋值操作的预期结果");
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnOK);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnTest);
            pnlButtons.Controls.Add(btnHelp);
            pnlButtons.FillColor = Color.White;
            pnlButtons.FillColor2 = Color.White;
            pnlButtons.Font = new Font("微软雅黑", 9F);
            pnlButtons.Location = new Point(15, 610);
            pnlButtons.Margin = new Padding(4, 5, 4, 5);
            pnlButtons.MinimumSize = new Size(1, 1);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Radius = 8;
            pnlButtons.RectColor = Color.FromArgb(65, 100, 204);
            pnlButtons.Size = new Size(1041, 70);
            pnlButtons.TabIndex = 3;
            pnlButtons.Text = null;
            pnlButtons.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.FillColor = Color.FromArgb(40, 167, 69);
            btnOK.FillColor2 = Color.FromArgb(40, 167, 69);
            btnOK.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnOK.Location = new Point(753, 20);
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
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(108, 117, 125);
            btnCancel.FillColor2 = Color.FromArgb(108, 117, 125);
            btnCancel.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnCancel.Location = new Point(893, 20);
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
            // btnTest
            // 
            btnTest.Cursor = Cursors.Hand;
            btnTest.FillColor = Color.FromArgb(255, 193, 7);
            btnTest.FillColor2 = Color.FromArgb(255, 193, 7);
            btnTest.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnTest.ForeColor = Color.FromArgb(33, 37, 41);
            btnTest.Location = new Point(280, 20);
            btnTest.MinimumSize = new Size(1, 1);
            btnTest.Name = "btnTest";
            btnTest.RectColor = Color.FromArgb(255, 193, 7);
            btnTest.RectDisableColor = Color.FromArgb(255, 193, 7);
            btnTest.Size = new Size(130, 35);
            btnTest.Symbol = 61515;
            btnTest.TabIndex = 2;
            btnTest.Text = "测试";
            btnTest.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnTest, "测试赋值操作是否能够正确执行");
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.FillColor = Color.FromArgb(23, 162, 184);
            btnHelp.FillColor2 = Color.FromArgb(23, 162, 184);
            btnHelp.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnHelp.Location = new Point(140, 20);
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
            // Form_VariableAssignment
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1071, 694);
            Controls.Add(pnlButtons);
            Controls.Add(grpPreview);
            Controls.Add(grpAdvancedConfig);
            Controls.Add(grpBasicConfig);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_VariableAssignment";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "变量赋值工具";
            ZoomScaleRect = new Rectangle(15, 15, 865, 605);
            grpBasicConfig.ResumeLayout(false);
            pnlPlcSource.ResumeLayout(false);
            pnlPlcSource.PerformLayout();
            pnlVoluationSource.ResumeLayout(false);
            grpAdvancedConfig.ResumeLayout(false);
            grpPreview.ResumeLayout(false);
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIPanel grpBasicConfig;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UILabel lblTargetVariable;
        private Sunny.UI.UIComboBox cmbTargetVariable;
        private Sunny.UI.UILabel lblAssignmentType;
        private Sunny.UI.UIComboBox cmbAssignmentType;
        private Sunny.UI.UILabel lblAssignmentContent;
        private Sunny.UI.UITextBox txtAssignmentContent;
        private Sunny.UI.UISymbolButton btnExpressionBuilder;

        private Sunny.UI.UIPanel grpAdvancedConfig;
        private Sunny.UI.UILine uiLine2;
        private Sunny.UI.UILabel lblCondition;
        private Sunny.UI.UITextBox txtCondition;
        private Sunny.UI.UILabel lblDescription;
        private Sunny.UI.UITextBox txtDescription;
        private Sunny.UI.UICheckBox chkEnabled;

        private Sunny.UI.UIPanel grpPreview;
        private Sunny.UI.UILine uiLine3;
        private Sunny.UI.UILabel lblValidation;
        private Sunny.UI.UIRichTextBox rtbValidationResult;
        private Sunny.UI.UILabel lblPreview;
        private Sunny.UI.UIRichTextBox rtbPreviewResult;

        private Sunny.UI.UIPanel pnlButtons;
        private Sunny.UI.UISymbolButton btnOK;
        private Sunny.UI.UISymbolButton btnCancel;
        private Sunny.UI.UISymbolButton btnTest;
        private Sunny.UI.UISymbolButton btnHelp;

        private System.Windows.Forms.ToolTip toolTip;
        private UIPanel pnlPlcSource;
        private UIComboBox CboPlcAddress;
        private UIComboBox CboPlcModule;
        private Label lblPlcModule;
        private Label lblPlcAddress;
        private UIPanel pnlVoluationSource;
    }
}
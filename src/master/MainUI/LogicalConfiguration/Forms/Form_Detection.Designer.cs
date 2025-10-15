using Sunny.UI;
namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class Form_Detection
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
            lblDetectionName = new Label();
            txtDetectionName = new UITextBox();
            lblDetectionType = new Label();
            lblTimeout = new Label();
            lblRetryCount = new Label();
            lblRetryInterval = new Label();
            pnlPlcSource = new UIPanel();
            CboPlcAddress = new UIComboBox();
            CboPlcModule = new UIComboBox();
            lblPlcModule = new Label();
            lblPlcAddress = new Label();
            lblDataSourceType = new Label();
            pnlVariableSource = new UIPanel();
            CboVariableName = new UIComboBox();
            lblVariableName = new Label();
            pnlRangeCondition = new UIPanel();
            numMaxValue = new AntdUI.InputNumber();
            numMinValue = new AntdUI.InputNumber();
            lblMinValue = new Label();
            lblMaxValue = new Label();
            pnlEqualityCondition = new UIPanel();
            numTolerance = new AntdUI.InputNumber();
            txtTargetValue = new UITextBox();
            lblTargetValue = new Label();
            lblTolerance = new Label();
            pnlThresholdCondition = new UIPanel();
            numThreshold = new AntdUI.InputNumber();
            lblOperator = new Label();
            cmbOperator = new UIComboBox();
            lblThreshold = new Label();
            lblResultVariable = new Label();
            grpBasicInfo = new UIPanel();
            numRefreshRate = new AntdUI.InputNumber();
            label1 = new Label();
            numRetryInterval = new AntdUI.InputNumber();
            numRetryCount = new AntdUI.InputNumber();
            numTimeout = new AntdUI.InputNumber();
            cmbDetectionType = new UIComboBox();
            uiLine2 = new UILine();
            uiLine1 = new UILine();
            grpDataSource = new UIPanel();
            cmbDataSourceType = new UIComboBox();
            uiLine3 = new UILine();
            grpCondition = new UIPanel();
            uiLine4 = new UILine();
            grpResultHandling = new UIPanel();
            chkShowResult = new UICheckBox();
            pnlJumpStep = new UIPanel();
            numSuccessStep = new AntdUI.InputNumber();
            numFailureStep = new AntdUI.InputNumber();
            label4 = new Label();
            label5 = new Label();
            cmbFailureAction = new UIComboBox();
            label3 = new Label();
            CboValueVariable = new UITextBox();
            label2 = new Label();
            chkSaveValue = new UICheckBox();
            CboResultVariable = new UIComboBox();
            chkSaveResult = new UICheckBox();
            btnOK = new UISymbolButton();
            btnCancel = new UISymbolButton();
            btnTestDetection = new UISymbolButton();
            pnlPlcSource.SuspendLayout();
            pnlVariableSource.SuspendLayout();
            pnlRangeCondition.SuspendLayout();
            pnlEqualityCondition.SuspendLayout();
            pnlThresholdCondition.SuspendLayout();
            grpBasicInfo.SuspendLayout();
            grpDataSource.SuspendLayout();
            grpCondition.SuspendLayout();
            grpResultHandling.SuspendLayout();
            pnlJumpStep.SuspendLayout();
            SuspendLayout();
            // 
            // lblDetectionName
            // 
            lblDetectionName.AutoSize = true;
            lblDetectionName.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblDetectionName.Location = new Point(401, 0);
            lblDetectionName.Name = "lblDetectionName";
            lblDetectionName.Size = new Size(82, 23);
            lblDetectionName.TabIndex = 0;
            lblDetectionName.Text = "检测名称:";
            lblDetectionName.Visible = false;
            // 
            // txtDetectionName
            // 
            txtDetectionName.FillColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtDetectionName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.Font = new Font("微软雅黑", 12.75F);
            txtDetectionName.Location = new Point(491, -2);
            txtDetectionName.Margin = new Padding(4, 5, 4, 5);
            txtDetectionName.MinimumSize = new Size(1, 16);
            txtDetectionName.Name = "txtDetectionName";
            txtDetectionName.Padding = new Padding(5);
            txtDetectionName.RectColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.ShowText = false;
            txtDetectionName.Size = new Size(120, 30);
            txtDetectionName.TabIndex = 1;
            txtDetectionName.TextAlignment = ContentAlignment.MiddleLeft;
            txtDetectionName.Visible = false;
            txtDetectionName.Watermark = "请输入";
            txtDetectionName.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            txtDetectionName.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // lblDetectionType
            // 
            lblDetectionType.AutoSize = true;
            lblDetectionType.Font = new Font("微软雅黑", 12.75F);
            lblDetectionType.Location = new Point(57, 16);
            lblDetectionType.Name = "lblDetectionType";
            lblDetectionType.Size = new Size(82, 23);
            lblDetectionType.TabIndex = 2;
            lblDetectionType.Text = "检测类型:";
            // 
            // lblTimeout
            // 
            lblTimeout.AutoSize = true;
            lblTimeout.Font = new Font("微软雅黑", 12.75F);
            lblTimeout.Location = new Point(21, 73);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(118, 23);
            lblTimeout.TabIndex = 4;
            lblTimeout.Text = "超时时间(ms):";
            // 
            // lblRetryCount
            // 
            lblRetryCount.AutoSize = true;
            lblRetryCount.Font = new Font("微软雅黑", 12.75F);
            lblRetryCount.Location = new Point(270, 73);
            lblRetryCount.Name = "lblRetryCount";
            lblRetryCount.Size = new Size(82, 23);
            lblRetryCount.TabIndex = 6;
            lblRetryCount.Text = "重试次数:";
            // 
            // lblRetryInterval
            // 
            lblRetryInterval.AutoSize = true;
            lblRetryInterval.Font = new Font("微软雅黑", 12.75F);
            lblRetryInterval.Location = new Point(464, 73);
            lblRetryInterval.Name = "lblRetryInterval";
            lblRetryInterval.Size = new Size(118, 23);
            lblRetryInterval.TabIndex = 8;
            lblRetryInterval.Text = "重试间隔(ms):";
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
            pnlPlcSource.Location = new Point(14, 54);
            pnlPlcSource.Margin = new Padding(4, 5, 4, 5);
            pnlPlcSource.MinimumSize = new Size(1, 1);
            pnlPlcSource.Name = "pnlPlcSource";
            pnlPlcSource.RectColor = Color.White;
            pnlPlcSource.RectDisableColor = Color.White;
            pnlPlcSource.Size = new Size(679, 80);
            pnlPlcSource.TabIndex = 3;
            pnlPlcSource.Text = null;
            pnlPlcSource.TextAlignment = ContentAlignment.MiddleCenter;
            pnlPlcSource.Visible = false;
            // 
            // CboPlcAddress
            // 
            CboPlcAddress.BackColor = Color.Transparent;
            CboPlcAddress.DataSource = null;
            CboPlcAddress.DropDownStyle = UIDropDownStyle.DropDownList;
            CboPlcAddress.FillColor = Color.FromArgb(218, 220, 230);
            CboPlcAddress.FillColor2 = Color.FromArgb(218, 220, 230);
            CboPlcAddress.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboPlcAddress.FilterMaxCount = 50;
            CboPlcAddress.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            CboPlcAddress.ForeDisableColor = Color.FromArgb(48, 48, 48);
            CboPlcAddress.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboPlcAddress.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboPlcAddress.Location = new Point(420, 10);
            CboPlcAddress.Margin = new Padding(4, 5, 4, 5);
            CboPlcAddress.MinimumSize = new Size(63, 0);
            CboPlcAddress.Name = "CboPlcAddress";
            CboPlcAddress.Padding = new Padding(0, 0, 30, 2);
            CboPlcAddress.Radius = 10;
            CboPlcAddress.RectColor = Color.Gray;
            CboPlcAddress.RectDisableColor = Color.Gray;
            CboPlcAddress.RectSides = ToolStripStatusLabelBorderSides.None;
            CboPlcAddress.Size = new Size(222, 30);
            CboPlcAddress.SymbolSize = 24;
            CboPlcAddress.TabIndex = 125;
            CboPlcAddress.TextAlignment = ContentAlignment.MiddleLeft;
            CboPlcAddress.Watermark = "请选择";
            CboPlcAddress.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            CboPlcAddress.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // CboPlcModule
            // 
            CboPlcModule.BackColor = Color.Transparent;
            CboPlcModule.DataSource = null;
            CboPlcModule.DropDownStyle = UIDropDownStyle.DropDownList;
            CboPlcModule.FillColor = Color.FromArgb(218, 220, 230);
            CboPlcModule.FillColor2 = Color.FromArgb(218, 220, 230);
            CboPlcModule.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboPlcModule.FilterMaxCount = 50;
            CboPlcModule.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            CboPlcModule.ForeDisableColor = Color.FromArgb(48, 48, 48);
            CboPlcModule.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboPlcModule.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboPlcModule.Location = new Point(91, 10);
            CboPlcModule.Margin = new Padding(4, 5, 4, 5);
            CboPlcModule.MinimumSize = new Size(63, 0);
            CboPlcModule.Name = "CboPlcModule";
            CboPlcModule.Padding = new Padding(0, 0, 30, 2);
            CboPlcModule.Radius = 10;
            CboPlcModule.RectColor = Color.Gray;
            CboPlcModule.RectDisableColor = Color.Gray;
            CboPlcModule.RectSides = ToolStripStatusLabelBorderSides.None;
            CboPlcModule.Size = new Size(222, 30);
            CboPlcModule.SymbolSize = 24;
            CboPlcModule.TabIndex = 124;
            CboPlcModule.TextAlignment = ContentAlignment.MiddleLeft;
            CboPlcModule.Watermark = "请选择";
            CboPlcModule.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            CboPlcModule.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // lblPlcModule
            // 
            lblPlcModule.AutoSize = true;
            lblPlcModule.Font = new Font("微软雅黑", 12.75F);
            lblPlcModule.Location = new Point(6, 13);
            lblPlcModule.Name = "lblPlcModule";
            lblPlcModule.Size = new Size(78, 23);
            lblPlcModule.TabIndex = 0;
            lblPlcModule.Text = "PLC模块:";
            // 
            // lblPlcAddress
            // 
            lblPlcAddress.AutoSize = true;
            lblPlcAddress.Font = new Font("微软雅黑", 12.75F);
            lblPlcAddress.Location = new Point(339, 13);
            lblPlcAddress.Name = "lblPlcAddress";
            lblPlcAddress.Size = new Size(78, 23);
            lblPlcAddress.TabIndex = 2;
            lblPlcAddress.Text = "PLC地址:";
            // 
            // lblDataSourceType
            // 
            lblDataSourceType.AutoSize = true;
            lblDataSourceType.Font = new Font("微软雅黑", 12.75F);
            lblDataSourceType.Location = new Point(28, 16);
            lblDataSourceType.Name = "lblDataSourceType";
            lblDataSourceType.Size = new Size(99, 23);
            lblDataSourceType.TabIndex = 0;
            lblDataSourceType.Text = "数据源类型:";
            // 
            // pnlVariableSource
            // 
            pnlVariableSource.BackColor = Color.Transparent;
            pnlVariableSource.Controls.Add(CboVariableName);
            pnlVariableSource.Controls.Add(lblVariableName);
            pnlVariableSource.Controls.Add(lblDetectionName);
            pnlVariableSource.Controls.Add(txtDetectionName);
            pnlVariableSource.FillColor = Color.White;
            pnlVariableSource.FillColor2 = Color.White;
            pnlVariableSource.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlVariableSource.Location = new Point(14, 54);
            pnlVariableSource.Margin = new Padding(4, 5, 4, 5);
            pnlVariableSource.MinimumSize = new Size(1, 1);
            pnlVariableSource.Name = "pnlVariableSource";
            pnlVariableSource.RectColor = Color.White;
            pnlVariableSource.RectDisableColor = Color.White;
            pnlVariableSource.Size = new Size(679, 80);
            pnlVariableSource.TabIndex = 2;
            pnlVariableSource.Text = null;
            pnlVariableSource.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // CboVariableName
            // 
            CboVariableName.BackColor = Color.Transparent;
            CboVariableName.DataSource = null;
            CboVariableName.DropDownStyle = UIDropDownStyle.DropDownList;
            CboVariableName.FillColor = Color.FromArgb(218, 220, 230);
            CboVariableName.FillColor2 = Color.FromArgb(218, 220, 230);
            CboVariableName.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboVariableName.FilterMaxCount = 50;
            CboVariableName.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            CboVariableName.ForeDisableColor = Color.FromArgb(48, 48, 48);
            CboVariableName.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboVariableName.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboVariableName.Location = new Point(91, 10);
            CboVariableName.Margin = new Padding(4, 5, 4, 5);
            CboVariableName.MinimumSize = new Size(63, 0);
            CboVariableName.Name = "CboVariableName";
            CboVariableName.Padding = new Padding(0, 0, 30, 2);
            CboVariableName.Radius = 10;
            CboVariableName.RectColor = Color.Gray;
            CboVariableName.RectDisableColor = Color.Gray;
            CboVariableName.RectSides = ToolStripStatusLabelBorderSides.None;
            CboVariableName.Size = new Size(222, 30);
            CboVariableName.SymbolSize = 24;
            CboVariableName.TabIndex = 125;
            CboVariableName.TextAlignment = ContentAlignment.MiddleLeft;
            CboVariableName.Watermark = "请选择";
            CboVariableName.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            CboVariableName.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // lblVariableName
            // 
            lblVariableName.AutoSize = true;
            lblVariableName.Font = new Font("微软雅黑", 12.75F);
            lblVariableName.Location = new Point(6, 13);
            lblVariableName.Name = "lblVariableName";
            lblVariableName.Size = new Size(65, 23);
            lblVariableName.TabIndex = 0;
            lblVariableName.Text = "变量名:";
            // 
            // pnlRangeCondition
            // 
            pnlRangeCondition.BackColor = Color.Transparent;
            pnlRangeCondition.Controls.Add(numMaxValue);
            pnlRangeCondition.Controls.Add(numMinValue);
            pnlRangeCondition.Controls.Add(lblMinValue);
            pnlRangeCondition.Controls.Add(lblMaxValue);
            pnlRangeCondition.FillColor = Color.White;
            pnlRangeCondition.FillColor2 = Color.White;
            pnlRangeCondition.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlRangeCondition.Location = new Point(19, 11);
            pnlRangeCondition.Margin = new Padding(4, 5, 4, 5);
            pnlRangeCondition.MinimumSize = new Size(1, 1);
            pnlRangeCondition.Name = "pnlRangeCondition";
            pnlRangeCondition.RectColor = Color.White;
            pnlRangeCondition.RectDisableColor = Color.White;
            pnlRangeCondition.Size = new Size(679, 110);
            pnlRangeCondition.TabIndex = 0;
            pnlRangeCondition.Text = null;
            pnlRangeCondition.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // numMaxValue
            // 
            numMaxValue.BackColor = Color.FromArgb(218, 220, 230);
            numMaxValue.Font = new Font("微软雅黑", 12.75F);
            numMaxValue.Location = new Point(288, 5);
            numMaxValue.Name = "numMaxValue";
            numMaxValue.SelectionStart = 1;
            numMaxValue.Size = new Size(109, 42);
            numMaxValue.TabIndex = 446;
            numMaxValue.Text = "0";
            // 
            // numMinValue
            // 
            numMinValue.BackColor = Color.FromArgb(218, 220, 230);
            numMinValue.Font = new Font("微软雅黑", 12.75F);
            numMinValue.Location = new Point(88, 5);
            numMinValue.Name = "numMinValue";
            numMinValue.SelectionStart = 1;
            numMinValue.Size = new Size(109, 42);
            numMinValue.TabIndex = 445;
            numMinValue.Text = "0";
            // 
            // lblMinValue
            // 
            lblMinValue.AutoSize = true;
            lblMinValue.Font = new Font("微软雅黑", 12.75F);
            lblMinValue.Location = new Point(15, 14);
            lblMinValue.Name = "lblMinValue";
            lblMinValue.Size = new Size(65, 23);
            lblMinValue.TabIndex = 0;
            lblMinValue.Text = "最小值:";
            // 
            // lblMaxValue
            // 
            lblMaxValue.AutoSize = true;
            lblMaxValue.Font = new Font("微软雅黑", 12.75F);
            lblMaxValue.Location = new Point(215, 14);
            lblMaxValue.Name = "lblMaxValue";
            lblMaxValue.Size = new Size(65, 23);
            lblMaxValue.TabIndex = 2;
            lblMaxValue.Text = "最大值:";
            // 
            // pnlEqualityCondition
            // 
            pnlEqualityCondition.BackColor = Color.Transparent;
            pnlEqualityCondition.Controls.Add(numTolerance);
            pnlEqualityCondition.Controls.Add(txtTargetValue);
            pnlEqualityCondition.Controls.Add(lblTargetValue);
            pnlEqualityCondition.Controls.Add(lblTolerance);
            pnlEqualityCondition.FillColor = Color.White;
            pnlEqualityCondition.FillColor2 = Color.White;
            pnlEqualityCondition.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlEqualityCondition.Location = new Point(19, 11);
            pnlEqualityCondition.Margin = new Padding(4, 5, 4, 5);
            pnlEqualityCondition.MinimumSize = new Size(1, 1);
            pnlEqualityCondition.Name = "pnlEqualityCondition";
            pnlEqualityCondition.RectColor = Color.White;
            pnlEqualityCondition.RectDisableColor = Color.White;
            pnlEqualityCondition.Size = new Size(679, 110);
            pnlEqualityCondition.TabIndex = 1;
            pnlEqualityCondition.Text = null;
            pnlEqualityCondition.TextAlignment = ContentAlignment.MiddleCenter;
            pnlEqualityCondition.Visible = false;
            // 
            // numTolerance
            // 
            numTolerance.BackColor = Color.FromArgb(218, 220, 230);
            numTolerance.Font = new Font("微软雅黑", 12.75F);
            numTolerance.Location = new Point(331, 5);
            numTolerance.Name = "numTolerance";
            numTolerance.SelectionStart = 1;
            numTolerance.Size = new Size(91, 42);
            numTolerance.TabIndex = 446;
            numTolerance.Text = "0";
            numTolerance.Value = new decimal(new int[] { 100, 0, 0, 196608 });
            // 
            // txtTargetValue
            // 
            txtTargetValue.FillColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.FillColor2 = Color.FromArgb(218, 220, 230);
            txtTargetValue.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.Font = new Font("微软雅黑", 12.75F);
            txtTargetValue.Location = new Point(87, 10);
            txtTargetValue.Margin = new Padding(4, 5, 4, 5);
            txtTargetValue.MinimumSize = new Size(1, 16);
            txtTargetValue.Name = "txtTargetValue";
            txtTargetValue.Padding = new Padding(5);
            txtTargetValue.RectColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.ShowText = false;
            txtTargetValue.Size = new Size(163, 30);
            txtTargetValue.TabIndex = 4;
            txtTargetValue.TextAlignment = ContentAlignment.MiddleLeft;
            txtTargetValue.Watermark = "请输入";
            txtTargetValue.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            txtTargetValue.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // lblTargetValue
            // 
            lblTargetValue.AutoSize = true;
            lblTargetValue.Font = new Font("微软雅黑", 12.75F);
            lblTargetValue.Location = new Point(16, 13);
            lblTargetValue.Name = "lblTargetValue";
            lblTargetValue.Size = new Size(65, 23);
            lblTargetValue.TabIndex = 0;
            lblTargetValue.Text = "目标值:";
            // 
            // lblTolerance
            // 
            lblTolerance.AutoSize = true;
            lblTolerance.Font = new Font("微软雅黑", 12.75F);
            lblTolerance.Location = new Point(273, 13);
            lblTolerance.Name = "lblTolerance";
            lblTolerance.Size = new Size(48, 23);
            lblTolerance.TabIndex = 2;
            lblTolerance.Text = "容差:";
            // 
            // pnlThresholdCondition
            // 
            pnlThresholdCondition.Controls.Add(numThreshold);
            pnlThresholdCondition.Controls.Add(lblOperator);
            pnlThresholdCondition.Controls.Add(cmbOperator);
            pnlThresholdCondition.Controls.Add(lblThreshold);
            pnlThresholdCondition.FillColor = Color.White;
            pnlThresholdCondition.FillColor2 = Color.White;
            pnlThresholdCondition.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlThresholdCondition.Location = new Point(19, 11);
            pnlThresholdCondition.Margin = new Padding(4, 5, 4, 5);
            pnlThresholdCondition.MinimumSize = new Size(1, 1);
            pnlThresholdCondition.Name = "pnlThresholdCondition";
            pnlThresholdCondition.RectColor = Color.White;
            pnlThresholdCondition.RectDisableColor = Color.White;
            pnlThresholdCondition.Size = new Size(679, 110);
            pnlThresholdCondition.TabIndex = 2;
            pnlThresholdCondition.Text = null;
            pnlThresholdCondition.TextAlignment = ContentAlignment.MiddleCenter;
            pnlThresholdCondition.Visible = false;
            // 
            // numThreshold
            // 
            numThreshold.BackColor = Color.FromArgb(218, 220, 230);
            numThreshold.Font = new Font("微软雅黑", 12.75F);
            numThreshold.Location = new Point(327, 5);
            numThreshold.Name = "numThreshold";
            numThreshold.SelectionStart = 1;
            numThreshold.Size = new Size(123, 42);
            numThreshold.TabIndex = 446;
            numThreshold.Text = "0";
            // 
            // lblOperator
            // 
            lblOperator.AutoSize = true;
            lblOperator.Font = new Font("微软雅黑", 12.75F);
            lblOperator.Location = new Point(12, 13);
            lblOperator.Name = "lblOperator";
            lblOperator.Size = new Size(82, 23);
            lblOperator.TabIndex = 0;
            lblOperator.Text = "比较操作:";
            // 
            // cmbOperator
            // 
            cmbOperator.BackColor = Color.Transparent;
            cmbOperator.DataSource = null;
            cmbOperator.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbOperator.FillColor = Color.FromArgb(218, 220, 230);
            cmbOperator.FillColor2 = Color.FromArgb(218, 220, 230);
            cmbOperator.FillDisableColor = Color.FromArgb(218, 220, 230);
            cmbOperator.FilterMaxCount = 50;
            cmbOperator.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbOperator.ForeDisableColor = Color.FromArgb(48, 48, 48);
            cmbOperator.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbOperator.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbOperator.Location = new Point(101, 10);
            cmbOperator.Margin = new Padding(4, 5, 4, 5);
            cmbOperator.MinimumSize = new Size(63, 0);
            cmbOperator.Name = "cmbOperator";
            cmbOperator.Padding = new Padding(0, 0, 30, 2);
            cmbOperator.Radius = 10;
            cmbOperator.RectColor = Color.Gray;
            cmbOperator.RectDisableColor = Color.Gray;
            cmbOperator.RectSides = ToolStripStatusLabelBorderSides.None;
            cmbOperator.Size = new Size(140, 30);
            cmbOperator.SymbolSize = 24;
            cmbOperator.TabIndex = 125;
            cmbOperator.TextAlignment = ContentAlignment.MiddleLeft;
            cmbOperator.Watermark = "请选择";
            cmbOperator.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            cmbOperator.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // lblThreshold
            // 
            lblThreshold.AutoSize = true;
            lblThreshold.Font = new Font("微软雅黑", 12.75F);
            lblThreshold.Location = new Point(274, 14);
            lblThreshold.Name = "lblThreshold";
            lblThreshold.Size = new Size(48, 23);
            lblThreshold.TabIndex = 2;
            lblThreshold.Text = "阈值:";
            // 
            // lblResultVariable
            // 
            lblResultVariable.AutoSize = true;
            lblResultVariable.Font = new Font("微软雅黑", 12.75F);
            lblResultVariable.Location = new Point(191, 17);
            lblResultVariable.Name = "lblResultVariable";
            lblResultVariable.Size = new Size(82, 23);
            lblResultVariable.TabIndex = 1;
            lblResultVariable.Text = "结果变量:";
            // 
            // grpBasicInfo
            // 
            grpBasicInfo.BackColor = Color.Transparent;
            grpBasicInfo.Controls.Add(numRefreshRate);
            grpBasicInfo.Controls.Add(label1);
            grpBasicInfo.Controls.Add(numRetryInterval);
            grpBasicInfo.Controls.Add(numRetryCount);
            grpBasicInfo.Controls.Add(numTimeout);
            grpBasicInfo.Controls.Add(cmbDetectionType);
            grpBasicInfo.Controls.Add(lblRetryInterval);
            grpBasicInfo.Controls.Add(lblDetectionType);
            grpBasicInfo.Controls.Add(lblRetryCount);
            grpBasicInfo.Controls.Add(lblTimeout);
            grpBasicInfo.FillColor = Color.White;
            grpBasicInfo.FillColor2 = Color.White;
            grpBasicInfo.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpBasicInfo.Location = new Point(13, 74);
            grpBasicInfo.Margin = new Padding(4, 5, 4, 5);
            grpBasicInfo.MinimumSize = new Size(1, 1);
            grpBasicInfo.Name = "grpBasicInfo";
            grpBasicInfo.Radius = 10;
            grpBasicInfo.RectColor = Color.White;
            grpBasicInfo.RectDisableColor = Color.White;
            grpBasicInfo.Size = new Size(707, 119);
            grpBasicInfo.TabIndex = 43;
            grpBasicInfo.Text = null;
            grpBasicInfo.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // numRefreshRate
            // 
            numRefreshRate.BackColor = Color.FromArgb(218, 220, 230);
            numRefreshRate.Font = new Font("微软雅黑", 12.75F);
            numRefreshRate.Location = new Point(588, 12);
            numRefreshRate.Name = "numRefreshRate";
            numRefreshRate.SelectionStart = 5;
            numRefreshRate.Size = new Size(85, 42);
            numRefreshRate.TabIndex = 448;
            numRefreshRate.Text = "30000";
            numRefreshRate.Value = new decimal(new int[] { 30000, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("微软雅黑", 12.75F);
            label1.Location = new Point(464, 19);
            label1.Name = "label1";
            label1.Size = new Size(118, 23);
            label1.TabIndex = 447;
            label1.Text = "刷新频率(ms):";
            // 
            // numRetryInterval
            // 
            numRetryInterval.BackColor = Color.FromArgb(218, 220, 230);
            numRetryInterval.Font = new Font("微软雅黑", 12.75F);
            numRetryInterval.Location = new Point(588, 66);
            numRetryInterval.Name = "numRetryInterval";
            numRetryInterval.SelectionStart = 4;
            numRetryInterval.Size = new Size(85, 42);
            numRetryInterval.TabIndex = 446;
            numRetryInterval.Text = "5000";
            numRetryInterval.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // numRetryCount
            // 
            numRetryCount.BackColor = Color.FromArgb(218, 220, 230);
            numRetryCount.Font = new Font("微软雅黑", 12.75F);
            numRetryCount.Location = new Point(358, 66);
            numRetryCount.Name = "numRetryCount";
            numRetryCount.SelectionStart = 1;
            numRetryCount.Size = new Size(83, 42);
            numRetryCount.TabIndex = 445;
            numRetryCount.Text = "0";
            // 
            // numTimeout
            // 
            numTimeout.BackColor = Color.FromArgb(218, 220, 230);
            numTimeout.Font = new Font("微软雅黑", 12.75F);
            numTimeout.Location = new Point(146, 66);
            numTimeout.Name = "numTimeout";
            numTimeout.SelectionStart = 5;
            numTimeout.Size = new Size(109, 42);
            numTimeout.TabIndex = 444;
            numTimeout.Text = "30000";
            numTimeout.Value = new decimal(new int[] { 30000, 0, 0, 0 });
            // 
            // cmbDetectionType
            // 
            cmbDetectionType.BackColor = Color.Transparent;
            cmbDetectionType.DataSource = null;
            cmbDetectionType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbDetectionType.FillColor = Color.FromArgb(218, 220, 230);
            cmbDetectionType.FillColor2 = Color.FromArgb(218, 220, 230);
            cmbDetectionType.FillDisableColor = Color.FromArgb(218, 220, 230);
            cmbDetectionType.FilterMaxCount = 50;
            cmbDetectionType.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbDetectionType.ForeDisableColor = Color.FromArgb(48, 48, 48);
            cmbDetectionType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDetectionType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDetectionType.Location = new Point(144, 12);
            cmbDetectionType.Margin = new Padding(4, 5, 4, 5);
            cmbDetectionType.MinimumSize = new Size(63, 0);
            cmbDetectionType.Name = "cmbDetectionType";
            cmbDetectionType.Padding = new Padding(0, 0, 30, 2);
            cmbDetectionType.Radius = 10;
            cmbDetectionType.RectColor = Color.Gray;
            cmbDetectionType.RectDisableColor = Color.Gray;
            cmbDetectionType.RectSides = ToolStripStatusLabelBorderSides.None;
            cmbDetectionType.Size = new Size(208, 30);
            cmbDetectionType.SymbolSize = 24;
            cmbDetectionType.TabIndex = 123;
            cmbDetectionType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDetectionType.Watermark = "请选择";
            cmbDetectionType.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            cmbDetectionType.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(3, 38);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(717, 29);
            uiLine2.TabIndex = 443;
            uiLine2.Text = "检测设置";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.EndCap = UILineCap.Circle;
            uiLine1.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.White;
            uiLine1.Location = new Point(3, 200);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(717, 29);
            uiLine1.TabIndex = 445;
            uiLine1.Text = "数据源配置";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpDataSource
            // 
            grpDataSource.BackColor = Color.Transparent;
            grpDataSource.Controls.Add(cmbDataSourceType);
            grpDataSource.Controls.Add(lblDataSourceType);
            grpDataSource.Controls.Add(pnlVariableSource);
            grpDataSource.Controls.Add(pnlPlcSource);
            grpDataSource.FillColor = Color.White;
            grpDataSource.FillColor2 = Color.White;
            grpDataSource.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpDataSource.Location = new Point(13, 236);
            grpDataSource.Margin = new Padding(4, 5, 4, 5);
            grpDataSource.MinimumSize = new Size(1, 1);
            grpDataSource.Name = "grpDataSource";
            grpDataSource.Radius = 10;
            grpDataSource.RectColor = Color.White;
            grpDataSource.RectDisableColor = Color.White;
            grpDataSource.Size = new Size(707, 151);
            grpDataSource.TabIndex = 444;
            grpDataSource.Text = null;
            grpDataSource.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // cmbDataSourceType
            // 
            cmbDataSourceType.BackColor = Color.Transparent;
            cmbDataSourceType.DataSource = null;
            cmbDataSourceType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbDataSourceType.FillColor = Color.FromArgb(218, 220, 230);
            cmbDataSourceType.FillColor2 = Color.FromArgb(218, 220, 230);
            cmbDataSourceType.FillDisableColor = Color.FromArgb(218, 220, 230);
            cmbDataSourceType.FilterMaxCount = 50;
            cmbDataSourceType.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbDataSourceType.ForeDisableColor = Color.FromArgb(48, 48, 48);
            cmbDataSourceType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDataSourceType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDataSourceType.Location = new Point(135, 11);
            cmbDataSourceType.Margin = new Padding(4, 5, 4, 5);
            cmbDataSourceType.MinimumSize = new Size(63, 0);
            cmbDataSourceType.Name = "cmbDataSourceType";
            cmbDataSourceType.Padding = new Padding(0, 0, 30, 2);
            cmbDataSourceType.Radius = 10;
            cmbDataSourceType.RectColor = Color.Gray;
            cmbDataSourceType.RectDisableColor = Color.Gray;
            cmbDataSourceType.RectSides = ToolStripStatusLabelBorderSides.None;
            cmbDataSourceType.Size = new Size(184, 30);
            cmbDataSourceType.SymbolSize = 24;
            cmbDataSourceType.TabIndex = 124;
            cmbDataSourceType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDataSourceType.Watermark = "请选择";
            cmbDataSourceType.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            cmbDataSourceType.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // uiLine3
            // 
            uiLine3.BackColor = Color.Transparent;
            uiLine3.EndCap = UILineCap.Circle;
            uiLine3.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine3.LineColor = Color.White;
            uiLine3.Location = new Point(3, 391);
            uiLine3.MinimumSize = new Size(1, 1);
            uiLine3.Name = "uiLine3";
            uiLine3.Size = new Size(717, 29);
            uiLine3.TabIndex = 447;
            uiLine3.Text = "检测条件配置";
            uiLine3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpCondition
            // 
            grpCondition.BackColor = Color.Transparent;
            grpCondition.Controls.Add(pnlRangeCondition);
            grpCondition.Controls.Add(pnlThresholdCondition);
            grpCondition.Controls.Add(pnlEqualityCondition);
            grpCondition.FillColor = Color.White;
            grpCondition.FillColor2 = Color.White;
            grpCondition.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpCondition.Location = new Point(13, 427);
            grpCondition.Margin = new Padding(4, 5, 4, 5);
            grpCondition.MinimumSize = new Size(1, 1);
            grpCondition.Name = "grpCondition";
            grpCondition.Radius = 10;
            grpCondition.RectColor = Color.White;
            grpCondition.RectDisableColor = Color.White;
            grpCondition.Size = new Size(707, 127);
            grpCondition.TabIndex = 446;
            grpCondition.Text = null;
            grpCondition.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine4
            // 
            uiLine4.BackColor = Color.Transparent;
            uiLine4.EndCap = UILineCap.Circle;
            uiLine4.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine4.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine4.LineColor = Color.White;
            uiLine4.Location = new Point(3, 561);
            uiLine4.MinimumSize = new Size(1, 1);
            uiLine4.Name = "uiLine4";
            uiLine4.Size = new Size(717, 29);
            uiLine4.TabIndex = 448;
            uiLine4.Text = "结果处理配置";
            uiLine4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpResultHandling
            // 
            grpResultHandling.BackColor = Color.Transparent;
            grpResultHandling.Controls.Add(lblResultVariable);
            grpResultHandling.Controls.Add(chkShowResult);
            grpResultHandling.Controls.Add(pnlJumpStep);
            grpResultHandling.Controls.Add(cmbFailureAction);
            grpResultHandling.Controls.Add(label3);
            grpResultHandling.Controls.Add(CboValueVariable);
            grpResultHandling.Controls.Add(label2);
            grpResultHandling.Controls.Add(chkSaveValue);
            grpResultHandling.Controls.Add(CboResultVariable);
            grpResultHandling.Controls.Add(chkSaveResult);
            grpResultHandling.FillColor = Color.White;
            grpResultHandling.FillColor2 = Color.White;
            grpResultHandling.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpResultHandling.Location = new Point(13, 593);
            grpResultHandling.Margin = new Padding(4, 5, 4, 5);
            grpResultHandling.MinimumSize = new Size(1, 1);
            grpResultHandling.Name = "grpResultHandling";
            grpResultHandling.Radius = 10;
            grpResultHandling.RectColor = Color.White;
            grpResultHandling.RectDisableColor = Color.White;
            grpResultHandling.Size = new Size(707, 226);
            grpResultHandling.TabIndex = 449;
            grpResultHandling.Text = null;
            grpResultHandling.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // chkShowResult
            // 
            chkShowResult.CheckBoxSize = 20;
            chkShowResult.Checked = true;
            chkShowResult.Font = new Font("微软雅黑", 12.75F);
            chkShowResult.ForeColor = Color.FromArgb(48, 48, 48);
            chkShowResult.Location = new Point(21, 146);
            chkShowResult.MinimumSize = new Size(1, 1);
            chkShowResult.Name = "chkShowResult";
            chkShowResult.Size = new Size(150, 29);
            chkShowResult.TabIndex = 451;
            chkShowResult.Text = "显示检测结果";
            // 
            // pnlJumpStep
            // 
            pnlJumpStep.BackColor = Color.Transparent;
            pnlJumpStep.Controls.Add(numSuccessStep);
            pnlJumpStep.Controls.Add(numFailureStep);
            pnlJumpStep.Controls.Add(label4);
            pnlJumpStep.Controls.Add(label5);
            pnlJumpStep.FillColor = Color.White;
            pnlJumpStep.FillColor2 = Color.White;
            pnlJumpStep.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlJumpStep.Location = new Point(320, 100);
            pnlJumpStep.Margin = new Padding(4, 5, 4, 5);
            pnlJumpStep.MinimumSize = new Size(1, 1);
            pnlJumpStep.Name = "pnlJumpStep";
            pnlJumpStep.RectColor = Color.White;
            pnlJumpStep.RectDisableColor = Color.White;
            pnlJumpStep.Size = new Size(376, 55);
            pnlJumpStep.TabIndex = 450;
            pnlJumpStep.Text = null;
            pnlJumpStep.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // numSuccessStep
            // 
            numSuccessStep.BackColor = Color.FromArgb(218, 220, 230);
            numSuccessStep.Font = new Font("微软雅黑", 12.75F);
            numSuccessStep.Location = new Point(277, 5);
            numSuccessStep.Name = "numSuccessStep";
            numSuccessStep.SelectionStart = 1;
            numSuccessStep.Size = new Size(86, 42);
            numSuccessStep.TabIndex = 446;
            numSuccessStep.Text = "0";
            // 
            // numFailureStep
            // 
            numFailureStep.BackColor = Color.FromArgb(218, 220, 230);
            numFailureStep.Font = new Font("微软雅黑", 12.75F);
            numFailureStep.Location = new Point(91, 5);
            numFailureStep.Name = "numFailureStep";
            numFailureStep.SelectionStart = 1;
            numFailureStep.Size = new Size(86, 42);
            numFailureStep.TabIndex = 445;
            numFailureStep.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("微软雅黑", 12.75F);
            label4.Location = new Point(11, 14);
            label4.Name = "label4";
            label4.Size = new Size(82, 23);
            label4.TabIndex = 0;
            label4.Text = "失败跳转:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("微软雅黑", 12.75F);
            label5.Location = new Point(199, 14);
            label5.Name = "label5";
            label5.Size = new Size(82, 23);
            label5.TabIndex = 2;
            label5.Text = "成功跳转:";
            // 
            // cmbFailureAction
            // 
            cmbFailureAction.BackColor = Color.Transparent;
            cmbFailureAction.DataSource = null;
            cmbFailureAction.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbFailureAction.FillColor = Color.FromArgb(218, 220, 230);
            cmbFailureAction.FillColor2 = Color.FromArgb(218, 220, 230);
            cmbFailureAction.FillDisableColor = Color.FromArgb(218, 220, 230);
            cmbFailureAction.FilterMaxCount = 50;
            cmbFailureAction.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbFailureAction.ForeDisableColor = Color.FromArgb(48, 48, 48);
            cmbFailureAction.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbFailureAction.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbFailureAction.Location = new Point(120, 100);
            cmbFailureAction.Margin = new Padding(4, 5, 4, 5);
            cmbFailureAction.MinimumSize = new Size(63, 0);
            cmbFailureAction.Name = "cmbFailureAction";
            cmbFailureAction.Padding = new Padding(0, 0, 30, 2);
            cmbFailureAction.Radius = 10;
            cmbFailureAction.RectColor = Color.Gray;
            cmbFailureAction.RectDisableColor = Color.Gray;
            cmbFailureAction.RectSides = ToolStripStatusLabelBorderSides.None;
            cmbFailureAction.Size = new Size(184, 30);
            cmbFailureAction.SymbolSize = 24;
            cmbFailureAction.TabIndex = 450;
            cmbFailureAction.TextAlignment = ContentAlignment.MiddleLeft;
            cmbFailureAction.Watermark = "请选择";
            cmbFailureAction.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            cmbFailureAction.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label3.Location = new Point(28, 103);
            label3.Name = "label3";
            label3.Size = new Size(82, 23);
            label3.TabIndex = 128;
            label3.Text = "失败处理:";
            // 
            // CboValueVariable
            // 
            CboValueVariable.FillColor = Color.FromArgb(218, 220, 230);
            CboValueVariable.FillColor2 = Color.FromArgb(218, 220, 230);
            CboValueVariable.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboValueVariable.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            CboValueVariable.Font = new Font("微软雅黑", 12.75F);
            CboValueVariable.Location = new Point(280, 54);
            CboValueVariable.Margin = new Padding(4, 5, 4, 5);
            CboValueVariable.MinimumSize = new Size(1, 16);
            CboValueVariable.Name = "CboValueVariable";
            CboValueVariable.Padding = new Padding(5);
            CboValueVariable.RectColor = Color.FromArgb(218, 220, 230);
            CboValueVariable.RectDisableColor = Color.FromArgb(218, 220, 230);
            CboValueVariable.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            CboValueVariable.ShowText = false;
            CboValueVariable.Size = new Size(184, 30);
            CboValueVariable.TabIndex = 127;
            CboValueVariable.TextAlignment = ContentAlignment.MiddleLeft;
            CboValueVariable.Watermark = "请输入";
            CboValueVariable.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            CboValueVariable.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label2.Location = new Point(208, 57);
            label2.Name = "label2";
            label2.Size = new Size(65, 23);
            label2.TabIndex = 126;
            label2.Text = "值变量:";
            // 
            // chkSaveValue
            // 
            chkSaveValue.CheckBoxSize = 20;
            chkSaveValue.Checked = true;
            chkSaveValue.Font = new Font("微软雅黑", 12.75F);
            chkSaveValue.ForeColor = Color.FromArgb(48, 48, 48);
            chkSaveValue.Location = new Point(21, 54);
            chkSaveValue.MinimumSize = new Size(1, 1);
            chkSaveValue.Name = "chkSaveValue";
            chkSaveValue.Size = new Size(150, 29);
            chkSaveValue.TabIndex = 125;
            chkSaveValue.Text = "保存检测值";
            // 
            // CboResultVariable
            // 
            CboResultVariable.BackColor = Color.Transparent;
            CboResultVariable.DataSource = null;
            CboResultVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            CboResultVariable.FillColor = Color.FromArgb(218, 220, 230);
            CboResultVariable.FillColor2 = Color.FromArgb(218, 220, 230);
            CboResultVariable.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboResultVariable.FilterMaxCount = 50;
            CboResultVariable.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            CboResultVariable.ForeDisableColor = Color.FromArgb(48, 48, 48);
            CboResultVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboResultVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboResultVariable.Location = new Point(280, 12);
            CboResultVariable.Margin = new Padding(4, 5, 4, 5);
            CboResultVariable.MinimumSize = new Size(63, 0);
            CboResultVariable.Name = "CboResultVariable";
            CboResultVariable.Padding = new Padding(0, 0, 30, 2);
            CboResultVariable.Radius = 10;
            CboResultVariable.RectColor = Color.Gray;
            CboResultVariable.RectDisableColor = Color.Gray;
            CboResultVariable.RectSides = ToolStripStatusLabelBorderSides.None;
            CboResultVariable.Size = new Size(184, 30);
            CboResultVariable.SymbolSize = 24;
            CboResultVariable.TabIndex = 124;
            CboResultVariable.TextAlignment = ContentAlignment.MiddleLeft;
            CboResultVariable.Watermark = "请选择";
            CboResultVariable.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            CboResultVariable.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // chkSaveResult
            // 
            chkSaveResult.CheckBoxSize = 20;
            chkSaveResult.Checked = true;
            chkSaveResult.Font = new Font("微软雅黑", 12.75F);
            chkSaveResult.ForeColor = Color.FromArgb(48, 48, 48);
            chkSaveResult.Location = new Point(21, 12);
            chkSaveResult.MinimumSize = new Size(1, 1);
            chkSaveResult.Name = "chkSaveResult";
            chkSaveResult.Size = new Size(150, 29);
            chkSaveResult.TabIndex = 0;
            chkSaveResult.Text = "保存检测结果";
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnOK.LightColor = Color.FromArgb(248, 248, 248);
            btnOK.Location = new Point(441, 828);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnOK.Size = new Size(132, 39);
            btnOK.Style = UIStyle.Custom;
            btnOK.Symbol = 61639;
            btnOK.SymbolSize = 30;
            btnOK.TabIndex = 450;
            btnOK.Text = "保 存";
            btnOK.TipsFont = new Font("微软雅黑", 12F);
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(230, 80, 80);
            btnCancel.FillColor2 = Color.FromArgb(230, 80, 80);
            btnCancel.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnCancel.LightColor = Color.FromArgb(248, 248, 248);
            btnCancel.Location = new Point(588, 828);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(230, 80, 80);
            btnCancel.RectDisableColor = Color.FromArgb(230, 80, 80);
            btnCancel.Size = new Size(132, 39);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.Symbol = 361457;
            btnCancel.SymbolSize = 30;
            btnCancel.TabIndex = 451;
            btnCancel.Text = "退 出";
            btnCancel.TipsFont = new Font("微软雅黑", 12F);
            // 
            // btnTestDetection
            // 
            btnTestDetection.Cursor = Cursors.Hand;
            btnTestDetection.FillColor = Color.FromArgb(110, 190, 40);
            btnTestDetection.FillColor2 = Color.FromArgb(110, 190, 40);
            btnTestDetection.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnTestDetection.LightColor = Color.FromArgb(248, 248, 248);
            btnTestDetection.Location = new Point(294, 828);
            btnTestDetection.MinimumSize = new Size(1, 1);
            btnTestDetection.Name = "btnTestDetection";
            btnTestDetection.RectColor = Color.FromArgb(110, 190, 40);
            btnTestDetection.RectDisableColor = Color.FromArgb(110, 190, 40);
            btnTestDetection.Size = new Size(132, 39);
            btnTestDetection.Style = UIStyle.Custom;
            btnTestDetection.Symbol = 358555;
            btnTestDetection.SymbolSize = 30;
            btnTestDetection.TabIndex = 452;
            btnTestDetection.Text = "测 试";
            btnTestDetection.TipsFont = new Font("微软雅黑", 12F);
            // 
            // Form_Detection
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(736, 875);
            ControlBox = false;
            Controls.Add(btnTestDetection);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(grpResultHandling);
            Controls.Add(uiLine4);
            Controls.Add(uiLine3);
            Controls.Add(grpCondition);
            Controls.Add(uiLine1);
            Controls.Add(grpDataSource);
            Controls.Add(uiLine2);
            Controls.Add(grpBasicInfo);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Detection";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "检测工具配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 784, 691);
            pnlPlcSource.ResumeLayout(false);
            pnlPlcSource.PerformLayout();
            pnlVariableSource.ResumeLayout(false);
            pnlVariableSource.PerformLayout();
            pnlRangeCondition.ResumeLayout(false);
            pnlRangeCondition.PerformLayout();
            pnlEqualityCondition.ResumeLayout(false);
            pnlEqualityCondition.PerformLayout();
            pnlThresholdCondition.ResumeLayout(false);
            pnlThresholdCondition.PerformLayout();
            grpBasicInfo.ResumeLayout(false);
            grpBasicInfo.PerformLayout();
            grpDataSource.ResumeLayout(false);
            grpDataSource.PerformLayout();
            grpCondition.ResumeLayout(false);
            grpResultHandling.ResumeLayout(false);
            grpResultHandling.PerformLayout();
            pnlJumpStep.ResumeLayout(false);
            pnlJumpStep.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblDetectionName;
        private UITextBox txtDetectionName;
        private System.Windows.Forms.Label lblDetectionType;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.Label lblRetryCount;
        private System.Windows.Forms.Label lblRetryInterval;
        private System.Windows.Forms.Label lblDataSourceType;
        private Sunny.UI.UIPanel pnlVariableSource;
        private System.Windows.Forms.Label lblVariableName;
        private Sunny.UI.UIPanel pnlPlcSource;
        private System.Windows.Forms.Label lblPlcModule;
        private System.Windows.Forms.Label lblPlcAddress;
        private Sunny.UI.UIPanel pnlRangeCondition;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.Label lblMaxValue;
        private Sunny.UI.UIPanel pnlEqualityCondition;
        private System.Windows.Forms.Label lblTargetValue;
        private System.Windows.Forms.Label lblTolerance;
        private Sunny.UI.UIPanel pnlThresholdCondition;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Label lblThreshold;
        private UIPanel grpBasicInfo;
        private UIComboBox cmbDetectionType;
        private UILine uiLine2;
        private AntdUI.InputNumber numTimeout;
        private AntdUI.InputNumber numRetryCount;
        private AntdUI.InputNumber numRetryInterval;
        private UILine uiLine1;
        private UIPanel grpDataSource;
        private UIComboBox cmbDataSourceType;
        private AntdUI.InputNumber numMinValue;
        private UILine uiLine3;
        private UIPanel grpCondition;
        private AntdUI.InputNumber numMaxValue;
        private UITextBox txtTargetValue;
        private AntdUI.InputNumber numTolerance;
        private UIComboBox cmbOperator;
        private AntdUI.InputNumber numThreshold;
        private UIComboBox CboPlcModule;
        private UIComboBox CboPlcAddress;
        private UIComboBox CboVariableName;
        private UILine uiLine4;
        private UIPanel grpResultHandling;
        private Label label2;
        private UICheckBox chkSaveValue;
        private UIComboBox CboResultVariable;
        private UICheckBox chkSaveResult;
        private UICheckBox chkShowResult;
        private UIPanel pnlJumpStep;
        private AntdUI.InputNumber numSuccessStep;
        private AntdUI.InputNumber numFailureStep;
        private Label label4;
        private Label label5;
        private UIComboBox cmbFailureAction;
        private Label label3;
        private UITextBox CboValueVariable;
        private Label lblResultVariable;
        private UISymbolButton btnOK;
        private UISymbolButton btnCancel;
        private UISymbolButton btnTestDetection;
        private AntdUI.InputNumber numRefreshRate;
        private Label label1;
    }
}
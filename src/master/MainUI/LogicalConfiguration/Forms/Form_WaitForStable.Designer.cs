namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_WaitForStable
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
            pnlMain = new UIPanel();
            grpBasicConfig = new UIPanel();
            uiLine1 = new UILine();
            lblDescription = new UILabel();
            txtDescription = new UITextBox();
            lblMonitorSourceType = new UILabel();
            cmbMonitorSourceType = new UIComboBox();
            grpMonitorSource = new UIPanel();
            uiLine2 = new UILine();
            lblMonitorVariable = new UILabel();
            cmbMonitorVariable = new UIComboBox();
            lblPlcModule = new UILabel();
            cmbPlcModule = new UIComboBox();
            lblPlcAddress = new UILabel();
            cmbPlcAddress = new UIComboBox();
            grpStabilityCriteria = new UIPanel();
            uiLine3 = new UILine();
            lblStabilityThreshold = new UILabel();
            numStabilityThreshold = new AntdUI.InputNumber();
            lblSamplingInterval = new UILabel();
            numSamplingInterval = new AntdUI.InputNumber();
            lblStableCount = new UILabel();
            numStableCount = new AntdUI.InputNumber();
            grpTimeoutConfig = new UIPanel();
            uiLine4 = new UILine();
            lblTimeout = new UILabel();
            numTimeout = new AntdUI.InputNumber();
            lblTimeoutAction = new UILabel();
            cmbTimeoutAction = new UIComboBox();
            lblTimeoutJumpStep = new UILabel();
            numTimeoutJumpStep = new AntdUI.InputNumber();
            grpResultHandling = new UIPanel();
            uiLine5 = new UILine();
            lblAssignToVariable = new UILabel();
            cmbAssignToVariable = new UIComboBox();
            lblValidationStatus = new UILabel();
            btnOK = new UISymbolButton();
            btnCancel = new UISymbolButton();
            btnTest = new UISymbolButton();
            btnHelp = new UISymbolButton();
            toolTip = new ToolTip(components);
            pnlMain.SuspendLayout();
            grpBasicConfig.SuspendLayout();
            grpMonitorSource.SuspendLayout();
            grpStabilityCriteria.SuspendLayout();
            grpTimeoutConfig.SuspendLayout();
            grpResultHandling.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(grpBasicConfig);
            pnlMain.Controls.Add(grpMonitorSource);
            pnlMain.Controls.Add(grpStabilityCriteria);
            pnlMain.Controls.Add(grpTimeoutConfig);
            pnlMain.Controls.Add(grpResultHandling);
            pnlMain.Controls.Add(lblValidationStatus);
            pnlMain.Dock = DockStyle.Top;
            pnlMain.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlMain.Location = new Point(0, 35);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.MinimumSize = new Size(1, 1);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(10);
            pnlMain.Radius = 0;
            pnlMain.RectColor = Color.FromArgb(65, 100, 204);
            pnlMain.Size = new Size(700, 700);
            pnlMain.TabIndex = 0;
            pnlMain.Text = null;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // grpBasicConfig
            // 
            grpBasicConfig.Controls.Add(uiLine1);
            grpBasicConfig.Controls.Add(lblDescription);
            grpBasicConfig.Controls.Add(txtDescription);
            grpBasicConfig.Controls.Add(lblMonitorSourceType);
            grpBasicConfig.Controls.Add(cmbMonitorSourceType);
            grpBasicConfig.FillColor = Color.White;
            grpBasicConfig.FillColor2 = Color.White;
            grpBasicConfig.Font = new Font("微软雅黑", 9F);
            grpBasicConfig.Location = new Point(15, 15);
            grpBasicConfig.Margin = new Padding(4, 5, 4, 5);
            grpBasicConfig.MinimumSize = new Size(1, 1);
            grpBasicConfig.Name = "grpBasicConfig";
            grpBasicConfig.Padding = new Padding(10, 32, 10, 10);
            grpBasicConfig.Radius = 8;
            grpBasicConfig.RectColor = Color.FromArgb(65, 100, 204);
            grpBasicConfig.Size = new Size(670, 110);
            grpBasicConfig.TabIndex = 0;
            grpBasicConfig.Text = null;
            grpBasicConfig.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.FromArgb(65, 100, 204);
            uiLine1.Location = new Point(15, 6);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(642, 25);
            uiLine1.TabIndex = 0;
            uiLine1.Text = "基本配置";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDescription
            // 
            lblDescription.BackColor = Color.Transparent;
            lblDescription.Font = new Font("微软雅黑", 10F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(15, 39);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(100, 23);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "步骤描述";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            txtDescription.Cursor = Cursors.IBeam;
            txtDescription.Font = new Font("微软雅黑", 10F);
            txtDescription.Location = new Point(130, 39);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.RectColor = Color.FromArgb(65, 100, 204);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(525, 29);
            txtDescription.TabIndex = 2;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtDescription, "对此步骤进行简要说明");
            txtDescription.Watermark = "请输入步骤描述...";
            // 
            // lblMonitorSourceType
            // 
            lblMonitorSourceType.BackColor = Color.Transparent;
            lblMonitorSourceType.Font = new Font("微软雅黑", 10F);
            lblMonitorSourceType.ForeColor = Color.Red;
            lblMonitorSourceType.Location = new Point(15, 74);
            lblMonitorSourceType.Name = "lblMonitorSourceType";
            lblMonitorSourceType.Size = new Size(100, 23);
            lblMonitorSourceType.TabIndex = 3;
            lblMonitorSourceType.Text = "监测源类型 *";
            lblMonitorSourceType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbMonitorSourceType
            // 
            cmbMonitorSourceType.DataSource = null;
            cmbMonitorSourceType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbMonitorSourceType.FillColor = Color.White;
            cmbMonitorSourceType.Font = new Font("微软雅黑", 10F);
            cmbMonitorSourceType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbMonitorSourceType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbMonitorSourceType.Location = new Point(130, 74);
            cmbMonitorSourceType.Margin = new Padding(4, 5, 4, 5);
            cmbMonitorSourceType.MinimumSize = new Size(63, 0);
            cmbMonitorSourceType.Name = "cmbMonitorSourceType";
            cmbMonitorSourceType.Padding = new Padding(0, 0, 30, 2);
            cmbMonitorSourceType.RectColor = Color.FromArgb(65, 100, 204);
            cmbMonitorSourceType.Size = new Size(200, 29);
            cmbMonitorSourceType.SymbolSize = 24;
            cmbMonitorSourceType.TabIndex = 4;
            cmbMonitorSourceType.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbMonitorSourceType, "选择监测全局变量还是PLC地址");
            cmbMonitorSourceType.Watermark = "";
            // 
            // grpMonitorSource
            // 
            grpMonitorSource.Controls.Add(uiLine2);
            grpMonitorSource.Controls.Add(lblMonitorVariable);
            grpMonitorSource.Controls.Add(cmbMonitorVariable);
            grpMonitorSource.Controls.Add(lblPlcModule);
            grpMonitorSource.Controls.Add(cmbPlcModule);
            grpMonitorSource.Controls.Add(lblPlcAddress);
            grpMonitorSource.Controls.Add(cmbPlcAddress);
            grpMonitorSource.FillColor = Color.White;
            grpMonitorSource.FillColor2 = Color.White;
            grpMonitorSource.Font = new Font("微软雅黑", 9F);
            grpMonitorSource.Location = new Point(15, 130);
            grpMonitorSource.Margin = new Padding(4, 5, 4, 5);
            grpMonitorSource.MinimumSize = new Size(1, 1);
            grpMonitorSource.Name = "grpMonitorSource";
            grpMonitorSource.Padding = new Padding(10, 32, 10, 10);
            grpMonitorSource.Radius = 8;
            grpMonitorSource.RectColor = Color.FromArgb(65, 100, 204);
            grpMonitorSource.Size = new Size(670, 110);
            grpMonitorSource.TabIndex = 1;
            grpMonitorSource.Text = null;
            grpMonitorSource.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.FromArgb(65, 100, 204);
            uiLine2.Location = new Point(15, 5);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(640, 25);
            uiLine2.TabIndex = 0;
            uiLine2.Text = "🎯 监测源配置";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMonitorVariable
            // 
            lblMonitorVariable.BackColor = Color.Transparent;
            lblMonitorVariable.Font = new Font("微软雅黑", 10F);
            lblMonitorVariable.ForeColor = Color.Red;
            lblMonitorVariable.Location = new Point(15, 40);
            lblMonitorVariable.Name = "lblMonitorVariable";
            lblMonitorVariable.Size = new Size(100, 23);
            lblMonitorVariable.TabIndex = 1;
            lblMonitorVariable.Text = "监测变量 *";
            lblMonitorVariable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbMonitorVariable
            // 
            cmbMonitorVariable.DataSource = null;
            cmbMonitorVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbMonitorVariable.FillColor = Color.White;
            cmbMonitorVariable.Font = new Font("微软雅黑", 10F);
            cmbMonitorVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbMonitorVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbMonitorVariable.Location = new Point(130, 40);
            cmbMonitorVariable.Margin = new Padding(4, 5, 4, 5);
            cmbMonitorVariable.MinimumSize = new Size(63, 0);
            cmbMonitorVariable.Name = "cmbMonitorVariable";
            cmbMonitorVariable.Padding = new Padding(0, 0, 30, 2);
            cmbMonitorVariable.RectColor = Color.FromArgb(65, 100, 204);
            cmbMonitorVariable.Size = new Size(525, 29);
            cmbMonitorVariable.SymbolSize = 24;
            cmbMonitorVariable.TabIndex = 2;
            cmbMonitorVariable.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbMonitorVariable, "选择要监测的全局变量（仅数值类型）");
            cmbMonitorVariable.Watermark = "请选择监测变量...";
            // 
            // lblPlcModule
            // 
            lblPlcModule.BackColor = Color.Transparent;
            lblPlcModule.Font = new Font("微软雅黑", 10F);
            lblPlcModule.ForeColor = Color.Red;
            lblPlcModule.Location = new Point(15, 40);
            lblPlcModule.Name = "lblPlcModule";
            lblPlcModule.Size = new Size(100, 23);
            lblPlcModule.TabIndex = 3;
            lblPlcModule.Text = "PLC模块 *";
            lblPlcModule.TextAlign = ContentAlignment.MiddleLeft;
            lblPlcModule.Visible = false;
            // 
            // cmbPlcModule
            // 
            cmbPlcModule.DataSource = null;
            cmbPlcModule.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbPlcModule.FillColor = Color.White;
            cmbPlcModule.Font = new Font("微软雅黑", 10F);
            cmbPlcModule.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbPlcModule.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbPlcModule.Location = new Point(130, 40);
            cmbPlcModule.Margin = new Padding(4, 5, 4, 5);
            cmbPlcModule.MinimumSize = new Size(63, 0);
            cmbPlcModule.Name = "cmbPlcModule";
            cmbPlcModule.Padding = new Padding(0, 0, 30, 2);
            cmbPlcModule.RectColor = Color.FromArgb(65, 100, 204);
            cmbPlcModule.Size = new Size(525, 29);
            cmbPlcModule.SymbolSize = 24;
            cmbPlcModule.TabIndex = 4;
            cmbPlcModule.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbPlcModule, "选择PLC模块");
            cmbPlcModule.Visible = false;
            cmbPlcModule.Watermark = "请选择PLC模块...";
            // 
            // lblPlcAddress
            // 
            lblPlcAddress.BackColor = Color.Transparent;
            lblPlcAddress.Font = new Font("微软雅黑", 10F);
            lblPlcAddress.ForeColor = Color.Red;
            lblPlcAddress.Location = new Point(15, 75);
            lblPlcAddress.Name = "lblPlcAddress";
            lblPlcAddress.Size = new Size(100, 23);
            lblPlcAddress.TabIndex = 5;
            lblPlcAddress.Text = "PLC地址 *";
            lblPlcAddress.TextAlign = ContentAlignment.MiddleLeft;
            lblPlcAddress.Visible = false;
            // 
            // cmbPlcAddress
            // 
            cmbPlcAddress.DataSource = null;
            cmbPlcAddress.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbPlcAddress.FillColor = Color.White;
            cmbPlcAddress.Font = new Font("微软雅黑", 10F);
            cmbPlcAddress.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbPlcAddress.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbPlcAddress.Location = new Point(130, 75);
            cmbPlcAddress.Margin = new Padding(4, 5, 4, 5);
            cmbPlcAddress.MinimumSize = new Size(63, 0);
            cmbPlcAddress.Name = "cmbPlcAddress";
            cmbPlcAddress.Padding = new Padding(0, 0, 30, 2);
            cmbPlcAddress.RectColor = Color.FromArgb(65, 100, 204);
            cmbPlcAddress.Size = new Size(525, 29);
            cmbPlcAddress.SymbolSize = 24;
            cmbPlcAddress.TabIndex = 6;
            cmbPlcAddress.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbPlcAddress, "选择PLC地址");
            cmbPlcAddress.Visible = false;
            cmbPlcAddress.Watermark = "请选择PLC地址...";
            // 
            // grpStabilityCriteria
            // 
            grpStabilityCriteria.Controls.Add(uiLine3);
            grpStabilityCriteria.Controls.Add(lblStabilityThreshold);
            grpStabilityCriteria.Controls.Add(numStabilityThreshold);
            grpStabilityCriteria.Controls.Add(lblSamplingInterval);
            grpStabilityCriteria.Controls.Add(numSamplingInterval);
            grpStabilityCriteria.Controls.Add(lblStableCount);
            grpStabilityCriteria.Controls.Add(numStableCount);
            grpStabilityCriteria.FillColor = Color.White;
            grpStabilityCriteria.FillColor2 = Color.White;
            grpStabilityCriteria.Font = new Font("微软雅黑", 9F);
            grpStabilityCriteria.Location = new Point(15, 245);
            grpStabilityCriteria.Margin = new Padding(4, 5, 4, 5);
            grpStabilityCriteria.MinimumSize = new Size(1, 1);
            grpStabilityCriteria.Name = "grpStabilityCriteria";
            grpStabilityCriteria.Padding = new Padding(10, 32, 10, 10);
            grpStabilityCriteria.Radius = 8;
            grpStabilityCriteria.RectColor = Color.FromArgb(65, 100, 204);
            grpStabilityCriteria.Size = new Size(670, 145);
            grpStabilityCriteria.TabIndex = 2;
            grpStabilityCriteria.Text = null;
            grpStabilityCriteria.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine3
            // 
            uiLine3.BackColor = Color.Transparent;
            uiLine3.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiLine3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine3.LineColor = Color.FromArgb(65, 100, 204);
            uiLine3.Location = new Point(15, 5);
            uiLine3.MinimumSize = new Size(1, 1);
            uiLine3.Name = "uiLine3";
            uiLine3.Size = new Size(640, 25);
            uiLine3.TabIndex = 0;
            uiLine3.Text = "⚙️ 稳定判据";
            uiLine3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStabilityThreshold
            // 
            lblStabilityThreshold.BackColor = Color.Transparent;
            lblStabilityThreshold.Font = new Font("微软雅黑", 10F);
            lblStabilityThreshold.ForeColor = Color.FromArgb(48, 48, 48);
            lblStabilityThreshold.Location = new Point(15, 40);
            lblStabilityThreshold.Name = "lblStabilityThreshold";
            lblStabilityThreshold.Size = new Size(150, 23);
            lblStabilityThreshold.TabIndex = 1;
            lblStabilityThreshold.Text = "稳定阈值(变化率):";
            lblStabilityThreshold.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numStabilityThreshold
            // 
            numStabilityThreshold.DecimalPlaces = 4;
            numStabilityThreshold.Font = new Font("微软雅黑", 10F);
            numStabilityThreshold.Location = new Point(170, 40);
            numStabilityThreshold.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numStabilityThreshold.Name = "numStabilityThreshold";
            numStabilityThreshold.Size = new Size(200, 29);
            numStabilityThreshold.TabIndex = 2;
            numStabilityThreshold.Text = "0.1000";
            toolTip.SetToolTip(numStabilityThreshold, "变化率阈值，|当前值-上次值|/采样间隔≤此值时认为稳定");
            numStabilityThreshold.Value = new decimal(new int[] { 1000, 0, 0, 262144 });
            // 
            // lblSamplingInterval
            // 
            lblSamplingInterval.BackColor = Color.Transparent;
            lblSamplingInterval.Font = new Font("微软雅黑", 10F);
            lblSamplingInterval.ForeColor = Color.FromArgb(48, 48, 48);
            lblSamplingInterval.Location = new Point(15, 75);
            lblSamplingInterval.Name = "lblSamplingInterval";
            lblSamplingInterval.Size = new Size(150, 23);
            lblSamplingInterval.TabIndex = 3;
            lblSamplingInterval.Text = "采样间隔(秒):";
            lblSamplingInterval.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numSamplingInterval
            // 
            numSamplingInterval.Font = new Font("微软雅黑", 10F);
            numSamplingInterval.Location = new Point(170, 75);
            numSamplingInterval.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            numSamplingInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSamplingInterval.Name = "numSamplingInterval";
            numSamplingInterval.Size = new Size(200, 29);
            numSamplingInterval.TabIndex = 4;
            numSamplingInterval.Text = "1";
            toolTip.SetToolTip(numSamplingInterval, "每隔多少秒采样一次（建议1-5秒）");
            numSamplingInterval.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblStableCount
            // 
            lblStableCount.BackColor = Color.Transparent;
            lblStableCount.Font = new Font("微软雅黑", 10F);
            lblStableCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblStableCount.Location = new Point(15, 110);
            lblStableCount.Name = "lblStableCount";
            lblStableCount.Size = new Size(150, 23);
            lblStableCount.TabIndex = 5;
            lblStableCount.Text = "连续稳定次数:";
            lblStableCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numStableCount
            // 
            numStableCount.Font = new Font("微软雅黑", 10F);
            numStableCount.Location = new Point(170, 110);
            numStableCount.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numStableCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numStableCount.Name = "numStableCount";
            numStableCount.Size = new Size(200, 29);
            numStableCount.TabIndex = 6;
            numStableCount.Text = "3";
            toolTip.SetToolTip(numStableCount, "连续多少次采样满足条件才算真正稳定");
            numStableCount.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // grpTimeoutConfig
            // 
            grpTimeoutConfig.Controls.Add(uiLine4);
            grpTimeoutConfig.Controls.Add(lblTimeout);
            grpTimeoutConfig.Controls.Add(numTimeout);
            grpTimeoutConfig.Controls.Add(lblTimeoutAction);
            grpTimeoutConfig.Controls.Add(cmbTimeoutAction);
            grpTimeoutConfig.Controls.Add(lblTimeoutJumpStep);
            grpTimeoutConfig.Controls.Add(numTimeoutJumpStep);
            grpTimeoutConfig.FillColor = Color.White;
            grpTimeoutConfig.FillColor2 = Color.White;
            grpTimeoutConfig.Font = new Font("微软雅黑", 9F);
            grpTimeoutConfig.Location = new Point(15, 395);
            grpTimeoutConfig.Margin = new Padding(4, 5, 4, 5);
            grpTimeoutConfig.MinimumSize = new Size(1, 1);
            grpTimeoutConfig.Name = "grpTimeoutConfig";
            grpTimeoutConfig.Padding = new Padding(10, 32, 10, 10);
            grpTimeoutConfig.Radius = 8;
            grpTimeoutConfig.RectColor = Color.FromArgb(65, 100, 204);
            grpTimeoutConfig.Size = new Size(670, 145);
            grpTimeoutConfig.TabIndex = 3;
            grpTimeoutConfig.Text = null;
            grpTimeoutConfig.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine4
            // 
            uiLine4.BackColor = Color.Transparent;
            uiLine4.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiLine4.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine4.LineColor = Color.FromArgb(65, 100, 204);
            uiLine4.Location = new Point(15, 5);
            uiLine4.MinimumSize = new Size(1, 1);
            uiLine4.Name = "uiLine4";
            uiLine4.Size = new Size(640, 25);
            uiLine4.TabIndex = 0;
            uiLine4.Text = "⏱️ 超时配置";
            uiLine4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTimeout
            // 
            lblTimeout.BackColor = Color.Transparent;
            lblTimeout.Font = new Font("微软雅黑", 10F);
            lblTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeout.Location = new Point(15, 40);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(150, 23);
            lblTimeout.TabIndex = 1;
            lblTimeout.Text = "超时时间(秒):";
            lblTimeout.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numTimeout
            // 
            numTimeout.Font = new Font("微软雅黑", 10F);
            numTimeout.Location = new Point(170, 40);
            numTimeout.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numTimeout.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numTimeout.Name = "numTimeout";
            numTimeout.Size = new Size(200, 29);
            numTimeout.TabIndex = 2;
            numTimeout.Text = "60";
            toolTip.SetToolTip(numTimeout, "最长等待时间（秒），0表示无限等待");
            numTimeout.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // lblTimeoutAction
            // 
            lblTimeoutAction.BackColor = Color.Transparent;
            lblTimeoutAction.Font = new Font("微软雅黑", 10F);
            lblTimeoutAction.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeoutAction.Location = new Point(15, 75);
            lblTimeoutAction.Name = "lblTimeoutAction";
            lblTimeoutAction.Size = new Size(150, 23);
            lblTimeoutAction.TabIndex = 3;
            lblTimeoutAction.Text = "超时后的动作:";
            lblTimeoutAction.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbTimeoutAction
            // 
            cmbTimeoutAction.DataSource = null;
            cmbTimeoutAction.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbTimeoutAction.FillColor = Color.White;
            cmbTimeoutAction.Font = new Font("微软雅黑", 10F);
            cmbTimeoutAction.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbTimeoutAction.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbTimeoutAction.Location = new Point(170, 75);
            cmbTimeoutAction.Margin = new Padding(4, 5, 4, 5);
            cmbTimeoutAction.MinimumSize = new Size(63, 0);
            cmbTimeoutAction.Name = "cmbTimeoutAction";
            cmbTimeoutAction.Padding = new Padding(0, 0, 30, 2);
            cmbTimeoutAction.RectColor = Color.FromArgb(65, 100, 204);
            cmbTimeoutAction.Size = new Size(485, 29);
            cmbTimeoutAction.SymbolSize = 24;
            cmbTimeoutAction.TabIndex = 4;
            cmbTimeoutAction.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbTimeoutAction, "选择超时后执行的动作");
            cmbTimeoutAction.Watermark = "";
            // 
            // lblTimeoutJumpStep
            // 
            lblTimeoutJumpStep.BackColor = Color.Transparent;
            lblTimeoutJumpStep.Font = new Font("微软雅黑", 10F);
            lblTimeoutJumpStep.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeoutJumpStep.Location = new Point(15, 110);
            lblTimeoutJumpStep.Name = "lblTimeoutJumpStep";
            lblTimeoutJumpStep.Size = new Size(150, 23);
            lblTimeoutJumpStep.TabIndex = 5;
            lblTimeoutJumpStep.Text = "跳转步骤号:";
            lblTimeoutJumpStep.TextAlign = ContentAlignment.MiddleLeft;
            lblTimeoutJumpStep.Visible = false;
            // 
            // numTimeoutJumpStep
            // 
            numTimeoutJumpStep.Font = new Font("微软雅黑", 10F);
            numTimeoutJumpStep.Location = new Point(170, 110);
            numTimeoutJumpStep.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numTimeoutJumpStep.Name = "numTimeoutJumpStep";
            numTimeoutJumpStep.Size = new Size(200, 29);
            numTimeoutJumpStep.TabIndex = 6;
            numTimeoutJumpStep.Text = "-1";
            toolTip.SetToolTip(numTimeoutJumpStep, "超时后跳转的步骤号，-1表示下一步");
            numTimeoutJumpStep.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numTimeoutJumpStep.Visible = false;
            // 
            // grpResultHandling
            // 
            grpResultHandling.Controls.Add(uiLine5);
            grpResultHandling.Controls.Add(lblAssignToVariable);
            grpResultHandling.Controls.Add(cmbAssignToVariable);
            grpResultHandling.FillColor = Color.White;
            grpResultHandling.FillColor2 = Color.White;
            grpResultHandling.Font = new Font("微软雅黑", 9F);
            grpResultHandling.Location = new Point(15, 545);
            grpResultHandling.Margin = new Padding(4, 5, 4, 5);
            grpResultHandling.MinimumSize = new Size(1, 1);
            grpResultHandling.Name = "grpResultHandling";
            grpResultHandling.Padding = new Padding(10, 32, 10, 10);
            grpResultHandling.Radius = 8;
            grpResultHandling.RectColor = Color.FromArgb(65, 100, 204);
            grpResultHandling.Size = new Size(670, 75);
            grpResultHandling.TabIndex = 4;
            grpResultHandling.Text = null;
            grpResultHandling.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine5
            // 
            uiLine5.BackColor = Color.Transparent;
            uiLine5.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiLine5.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine5.LineColor = Color.FromArgb(65, 100, 204);
            uiLine5.Location = new Point(15, 5);
            uiLine5.MinimumSize = new Size(1, 1);
            uiLine5.Name = "uiLine5";
            uiLine5.Size = new Size(640, 25);
            uiLine5.TabIndex = 0;
            uiLine5.Text = "📊 结果处理";
            uiLine5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAssignToVariable
            // 
            lblAssignToVariable.BackColor = Color.Transparent;
            lblAssignToVariable.Font = new Font("微软雅黑", 10F);
            lblAssignToVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblAssignToVariable.Location = new Point(15, 40);
            lblAssignToVariable.Name = "lblAssignToVariable";
            lblAssignToVariable.Size = new Size(150, 23);
            lblAssignToVariable.TabIndex = 1;
            lblAssignToVariable.Text = "赋值到变量:";
            lblAssignToVariable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbAssignToVariable
            // 
            cmbAssignToVariable.DataSource = null;
            cmbAssignToVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbAssignToVariable.FillColor = Color.White;
            cmbAssignToVariable.Font = new Font("微软雅黑", 10F);
            cmbAssignToVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbAssignToVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbAssignToVariable.Location = new Point(170, 40);
            cmbAssignToVariable.Margin = new Padding(4, 5, 4, 5);
            cmbAssignToVariable.MinimumSize = new Size(63, 0);
            cmbAssignToVariable.Name = "cmbAssignToVariable";
            cmbAssignToVariable.Padding = new Padding(0, 0, 30, 2);
            cmbAssignToVariable.RectColor = Color.FromArgb(65, 100, 204);
            cmbAssignToVariable.Size = new Size(485, 29);
            cmbAssignToVariable.SymbolSize = 24;
            cmbAssignToVariable.TabIndex = 2;
            cmbAssignToVariable.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbAssignToVariable, "稳定后将当前值赋给此变量（可选）");
            cmbAssignToVariable.Watermark = "可选：选择要赋值的变量...";
            // 
            // lblValidationStatus
            // 
            lblValidationStatus.BackColor = Color.Transparent;
            lblValidationStatus.Font = new Font("微软雅黑", 9F);
            lblValidationStatus.ForeColor = Color.FromArgb(34, 197, 94);
            lblValidationStatus.Location = new Point(15, 630);
            lblValidationStatus.Name = "lblValidationStatus";
            lblValidationStatus.Size = new Size(670, 20);
            lblValidationStatus.TabIndex = 5;
            lblValidationStatus.Text = "✓ 配置有效";
            lblValidationStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblValidationStatus.Visible = false;
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.Font = new Font("微软雅黑", 10F);
            btnOK.Location = new Point(320, 745);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Radius = 8;
            btnOK.Size = new Size(80, 35);
            btnOK.Symbol = 61528;
            btnOK.TabIndex = 1;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(410, 745);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 8;
            btnCancel.Size = new Size(80, 35);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnTest
            // 
            btnTest.Cursor = Cursors.Hand;
            btnTest.Font = new Font("微软雅黑", 10F);
            btnTest.Location = new Point(500, 745);
            btnTest.MinimumSize = new Size(1, 1);
            btnTest.Name = "btnTest";
            btnTest.Radius = 8;
            btnTest.Size = new Size(80, 35);
            btnTest.Symbol = 61590;
            btnTest.TabIndex = 3;
            btnTest.Text = "测试";
            btnTest.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.Font = new Font("微软雅黑", 10F);
            btnHelp.Location = new Point(590, 745);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.Radius = 8;
            btnHelp.Size = new Size(80, 35);
            btnHelp.Symbol = 61534;
            btnHelp.TabIndex = 4;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // Form_WaitForStable
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(700, 790);
            Controls.Add(btnHelp);
            Controls.Add(btnTest);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(pnlMain);
            Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_WaitForStable";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "等待变量稳定配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            ZoomScaleRect = new Rectangle(15, 15, 700, 790);
            pnlMain.ResumeLayout(false);
            grpBasicConfig.ResumeLayout(false);
            grpMonitorSource.ResumeLayout(false);
            grpStabilityCriteria.ResumeLayout(false);
            grpTimeoutConfig.ResumeLayout(false);
            grpResultHandling.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel pnlMain;
        private Sunny.UI.UIPanel grpBasicConfig;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UILabel lblDescription;
        private Sunny.UI.UITextBox txtDescription;
        private Sunny.UI.UILabel lblMonitorSourceType;
        private Sunny.UI.UIComboBox cmbMonitorSourceType;
        private Sunny.UI.UIPanel grpMonitorSource;
        private Sunny.UI.UILine uiLine2;
        private Sunny.UI.UILabel lblMonitorVariable;
        private Sunny.UI.UIComboBox cmbMonitorVariable;
        private Sunny.UI.UILabel lblPlcModule;
        private Sunny.UI.UIComboBox cmbPlcModule;
        private Sunny.UI.UILabel lblPlcAddress;
        private Sunny.UI.UIComboBox cmbPlcAddress;
        private Sunny.UI.UIPanel grpStabilityCriteria;
        private Sunny.UI.UILine uiLine3;
        private Sunny.UI.UILabel lblStabilityThreshold;
        private AntdUI.InputNumber numStabilityThreshold;
        private Sunny.UI.UILabel lblSamplingInterval;
        private AntdUI.InputNumber numSamplingInterval;
        private Sunny.UI.UILabel lblStableCount;
        private AntdUI.InputNumber numStableCount;
        private Sunny.UI.UIPanel grpTimeoutConfig;
        private Sunny.UI.UILine uiLine4;
        private Sunny.UI.UILabel lblTimeout;
        private AntdUI.InputNumber numTimeout;
        private Sunny.UI.UILabel lblTimeoutAction;
        private Sunny.UI.UIComboBox cmbTimeoutAction;
        private Sunny.UI.UILabel lblTimeoutJumpStep;
        private AntdUI.InputNumber numTimeoutJumpStep;
        private Sunny.UI.UIPanel grpResultHandling;
        private Sunny.UI.UILine uiLine5;
        private Sunny.UI.UILabel lblAssignToVariable;
        private Sunny.UI.UIComboBox cmbAssignToVariable;
        private Sunny.UI.UILabel lblValidationStatus;
        private Sunny.UI.UISymbolButton btnOK;
        private Sunny.UI.UISymbolButton btnCancel;
        private Sunny.UI.UISymbolButton btnTest;
        private Sunny.UI.UISymbolButton btnHelp;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
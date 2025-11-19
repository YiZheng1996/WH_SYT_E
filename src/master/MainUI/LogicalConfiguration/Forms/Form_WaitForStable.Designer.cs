namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_WaitForStable
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlMain = new UIPanel();
            lineSection1 = new Line();
            lblDescription = new UILabel();
            txtDescription = new TextBox();
            lblMonitorVariable = new UILabel();
            cmbMonitorVariable = new UIComboBox();
            lineSection2 = new UILine();
            lblStabilityThreshold = new UILabel();
            numStabilityThreshold = new AntdUI.InputNumber();
            lblSamplingInterval = new UILabel();
            numSamplingInterval = new UINumPadTextBox();
            lblStableCount = new UILabel();
            numStableCount = new AntdUI.InputNumber();
            lineSection3 = new UILine();
            lblTimeout = new UILabel();
            numTimeout = new AntdUI.InputNumber();
            lblTimeoutAction = new UILabel();
            cmbTimeoutAction = new UIComboBox();
            lblTimeoutJumpStep = new UILabel();
            numTimeoutJumpStep = new AntdUI.InputNumber();
            lineSection4 = new UILine();
            lblAssignToVariable = new UILabel();
            cmbAssignToVariable = new UIComboBox();
            btnOK = new UISymbolButton();
            btnCancel = new UISymbolButton();
            btnTest = new UISymbolButton();
            btnHelp = new UISymbolButton();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(lineSection1);
            pnlMain.Controls.Add(lblDescription);
            pnlMain.Controls.Add(txtDescription);
            pnlMain.Controls.Add(lblMonitorVariable);
            pnlMain.Controls.Add(cmbMonitorVariable);
            pnlMain.Controls.Add(lineSection2);
            pnlMain.Controls.Add(lblStabilityThreshold);
            pnlMain.Controls.Add(numStabilityThreshold);
            pnlMain.Controls.Add(lblSamplingInterval);
            pnlMain.Controls.Add(numSamplingInterval);
            pnlMain.Controls.Add(lblStableCount);
            pnlMain.Controls.Add(numStableCount);
            pnlMain.Controls.Add(lineSection3);
            pnlMain.Controls.Add(lblTimeout);
            pnlMain.Controls.Add(numTimeout);
            pnlMain.Controls.Add(lblTimeoutAction);
            pnlMain.Controls.Add(cmbTimeoutAction);
            pnlMain.Controls.Add(lblTimeoutJumpStep);
            pnlMain.Controls.Add(numTimeoutJumpStep);
            pnlMain.Controls.Add(lineSection4);
            pnlMain.Controls.Add(lblAssignToVariable);
            pnlMain.Controls.Add(cmbAssignToVariable);
            pnlMain.Dock = DockStyle.Top;
            pnlMain.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlMain.Location = new Point(0, 35);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.MinimumSize = new Size(1, 1);
            pnlMain.Name = "pnlMain";
            pnlMain.Radius = 0;
            pnlMain.Size = new Size(600, 541);
            pnlMain.TabIndex = 0;
            pnlMain.Text = null;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lineSection1
            // 
            lineSection1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lineSection1.Location = new Point(20, 20);
            lineSection1.MinimumSize = new Size(10, 10);
            lineSection1.Name = "lineSection1";
            lineSection1.Size = new Size(560, 10);
            lineSection1.TabIndex = 0;
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("微软雅黑", 10F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(40, 60);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(120, 30);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "步骤描述:";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("微软雅黑", 10F);
            txtDescription.Location = new Point(160, 60);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(400, 25);
            txtDescription.TabIndex = 2;
            // 
            // lblMonitorVariable
            // 
            lblMonitorVariable.Font = new Font("微软雅黑", 10F);
            lblMonitorVariable.ForeColor = Color.Red;
            lblMonitorVariable.Location = new Point(40, 100);
            lblMonitorVariable.Name = "lblMonitorVariable";
            lblMonitorVariable.Size = new Size(120, 30);
            lblMonitorVariable.TabIndex = 3;
            lblMonitorVariable.Text = "*监测变量:";
            lblMonitorVariable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbMonitorVariable
            // 
            cmbMonitorVariable.DataSource = null;
            cmbMonitorVariable.FillColor = Color.White;
            cmbMonitorVariable.Font = new Font("微软雅黑", 10F);
            cmbMonitorVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbMonitorVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbMonitorVariable.Location = new Point(160, 100);
            cmbMonitorVariable.Margin = new Padding(4, 5, 4, 5);
            cmbMonitorVariable.MinimumSize = new Size(63, 0);
            cmbMonitorVariable.Name = "cmbMonitorVariable";
            cmbMonitorVariable.Padding = new Padding(0, 0, 30, 2);
            cmbMonitorVariable.Size = new Size(400, 30);
            cmbMonitorVariable.SymbolSize = 24;
            cmbMonitorVariable.TabIndex = 4;
            cmbMonitorVariable.TextAlignment = ContentAlignment.MiddleLeft;
            cmbMonitorVariable.Watermark = "";
            // 
            // lineSection2
            // 
            lineSection2.BackColor = Color.Transparent;
            lineSection2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lineSection2.ForeColor = Color.FromArgb(48, 48, 48);
            lineSection2.Location = new Point(20, 150);
            lineSection2.MinimumSize = new Size(1, 1);
            lineSection2.Name = "lineSection2";
            lineSection2.Size = new Size(560, 30);
            lineSection2.TabIndex = 5;
            lineSection2.Text = "⚙️ 稳定判据";
            // 
            // lblStabilityThreshold
            // 
            lblStabilityThreshold.Font = new Font("微软雅黑", 10F);
            lblStabilityThreshold.ForeColor = Color.FromArgb(48, 48, 48);
            lblStabilityThreshold.Location = new Point(40, 190);
            lblStabilityThreshold.Name = "lblStabilityThreshold";
            lblStabilityThreshold.Size = new Size(120, 30);
            lblStabilityThreshold.TabIndex = 6;
            lblStabilityThreshold.Text = "稳定阈值:";
            lblStabilityThreshold.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numStabilityThreshold
            // 
            numStabilityThreshold.DecimalPlaces = 2;
            numStabilityThreshold.Font = new Font("微软雅黑", 10F);
            numStabilityThreshold.Location = new Point(160, 190);
            numStabilityThreshold.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numStabilityThreshold.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numStabilityThreshold.Name = "numStabilityThreshold";
            numStabilityThreshold.Size = new Size(150, 30);
            numStabilityThreshold.TabIndex = 7;
            numStabilityThreshold.Text = "0.10";
            numStabilityThreshold.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // lblSamplingInterval
            // 
            lblSamplingInterval.Font = new Font("微软雅黑", 10F);
            lblSamplingInterval.ForeColor = Color.FromArgb(48, 48, 48);
            lblSamplingInterval.Location = new Point(40, 230);
            lblSamplingInterval.Name = "lblSamplingInterval";
            lblSamplingInterval.Size = new Size(120, 30);
            lblSamplingInterval.TabIndex = 8;
            lblSamplingInterval.Text = "采样间隔(秒):";
            lblSamplingInterval.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numSamplingInterval
            // 
            numSamplingInterval.FillColor = Color.White;
            numSamplingInterval.Font = new Font("微软雅黑", 10F);
            numSamplingInterval.Location = new Point(160, 230);
            numSamplingInterval.Margin = new Padding(4, 5, 4, 5);
            numSamplingInterval.MinimumSize = new Size(63, 0);
            numSamplingInterval.Name = "numSamplingInterval";
            numSamplingInterval.Padding = new Padding(0, 0, 30, 2);
            numSamplingInterval.Size = new Size(150, 30);
            numSamplingInterval.SymbolSize = 24;
            numSamplingInterval.TabIndex = 9;
            numSamplingInterval.TextAlignment = ContentAlignment.MiddleLeft;
            numSamplingInterval.Watermark = "";
            // 
            // lblStableCount
            // 
            lblStableCount.Font = new Font("微软雅黑", 10F);
            lblStableCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblStableCount.Location = new Point(40, 270);
            lblStableCount.Name = "lblStableCount";
            lblStableCount.Size = new Size(120, 30);
            lblStableCount.TabIndex = 10;
            lblStableCount.Text = "连续稳定次数:";
            lblStableCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numStableCount
            // 
            numStableCount.Font = new Font("微软雅黑", 10F);
            numStableCount.Location = new Point(160, 270);
            numStableCount.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numStableCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numStableCount.Name = "numStableCount";
            numStableCount.Size = new Size(150, 30);
            numStableCount.TabIndex = 11;
            numStableCount.Text = "3";
            numStableCount.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // lineSection3
            // 
            lineSection3.BackColor = Color.Transparent;
            lineSection3.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lineSection3.ForeColor = Color.FromArgb(48, 48, 48);
            lineSection3.Location = new Point(20, 320);
            lineSection3.MinimumSize = new Size(1, 1);
            lineSection3.Name = "lineSection3";
            lineSection3.Size = new Size(560, 30);
            lineSection3.TabIndex = 12;
            lineSection3.Text = "⏱️ 超时配置";
            // 
            // lblTimeout
            // 
            lblTimeout.Font = new Font("微软雅黑", 10F);
            lblTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeout.Location = new Point(40, 360);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(120, 30);
            lblTimeout.TabIndex = 13;
            lblTimeout.Text = "超时时间(秒):";
            lblTimeout.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numTimeout
            // 
            numTimeout.Font = new Font("微软雅黑", 10F);
            numTimeout.Location = new Point(160, 360);
            numTimeout.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numTimeout.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numTimeout.Name = "numTimeout";
            numTimeout.Size = new Size(150, 30);
            numTimeout.TabIndex = 14;
            numTimeout.Text = "60";
            numTimeout.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // lblTimeoutAction
            // 
            lblTimeoutAction.Font = new Font("微软雅黑", 10F);
            lblTimeoutAction.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeoutAction.Location = new Point(40, 400);
            lblTimeoutAction.Name = "lblTimeoutAction";
            lblTimeoutAction.Size = new Size(120, 30);
            lblTimeoutAction.TabIndex = 15;
            lblTimeoutAction.Text = "超时动作:";
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
            cmbTimeoutAction.Location = new Point(160, 400);
            cmbTimeoutAction.Margin = new Padding(4, 5, 4, 5);
            cmbTimeoutAction.MinimumSize = new Size(63, 0);
            cmbTimeoutAction.Name = "cmbTimeoutAction";
            cmbTimeoutAction.Padding = new Padding(0, 0, 30, 2);
            cmbTimeoutAction.Size = new Size(200, 30);
            cmbTimeoutAction.SymbolSize = 24;
            cmbTimeoutAction.TabIndex = 16;
            cmbTimeoutAction.TextAlignment = ContentAlignment.MiddleLeft;
            cmbTimeoutAction.Watermark = "";
            // 
            // lblTimeoutJumpStep
            // 
            lblTimeoutJumpStep.Font = new Font("微软雅黑", 10F);
            lblTimeoutJumpStep.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeoutJumpStep.Location = new Point(370, 400);
            lblTimeoutJumpStep.Name = "lblTimeoutJumpStep";
            lblTimeoutJumpStep.Size = new Size(100, 30);
            lblTimeoutJumpStep.TabIndex = 17;
            lblTimeoutJumpStep.Text = "跳转步骤号:";
            lblTimeoutJumpStep.TextAlign = ContentAlignment.MiddleLeft;
            lblTimeoutJumpStep.Visible = false;
            // 
            // numTimeoutJumpStep
            // 
            numTimeoutJumpStep.Font = new Font("微软雅黑", 10F);
            numTimeoutJumpStep.Location = new Point(470, 400);
            numTimeoutJumpStep.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numTimeoutJumpStep.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numTimeoutJumpStep.Name = "numTimeoutJumpStep";
            numTimeoutJumpStep.Size = new Size(90, 30);
            numTimeoutJumpStep.TabIndex = 18;
            numTimeoutJumpStep.Text = "-1";
            numTimeoutJumpStep.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numTimeoutJumpStep.Visible = false;
            // 
            // lineSection4
            // 
            lineSection4.BackColor = Color.Transparent;
            lineSection4.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lineSection4.ForeColor = Color.FromArgb(48, 48, 48);
            lineSection4.Location = new Point(20, 450);
            lineSection4.MinimumSize = new Size(1, 1);
            lineSection4.Name = "lineSection4";
            lineSection4.Size = new Size(560, 30);
            lineSection4.TabIndex = 19;
            lineSection4.Text = "✅ 结果处理";
            // 
            // lblAssignToVariable
            // 
            lblAssignToVariable.Font = new Font("微软雅黑", 10F);
            lblAssignToVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblAssignToVariable.Location = new Point(40, 490);
            lblAssignToVariable.Name = "lblAssignToVariable";
            lblAssignToVariable.Size = new Size(120, 30);
            lblAssignToVariable.TabIndex = 20;
            lblAssignToVariable.Text = "赋值目标变量:";
            lblAssignToVariable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbAssignToVariable
            // 
            cmbAssignToVariable.DataSource = null;
            cmbAssignToVariable.FillColor = Color.White;
            cmbAssignToVariable.Font = new Font("微软雅黑", 10F);
            cmbAssignToVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbAssignToVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbAssignToVariable.Location = new Point(160, 490);
            cmbAssignToVariable.Margin = new Padding(4, 5, 4, 5);
            cmbAssignToVariable.MinimumSize = new Size(63, 0);
            cmbAssignToVariable.Name = "cmbAssignToVariable";
            cmbAssignToVariable.Padding = new Padding(0, 0, 30, 2);
            cmbAssignToVariable.Size = new Size(400, 30);
            cmbAssignToVariable.SymbolSize = 24;
            cmbAssignToVariable.TabIndex = 21;
            cmbAssignToVariable.TextAlignment = ContentAlignment.MiddleLeft;
            cmbAssignToVariable.Watermark = "";
            // 
            // btnOK
            // 
            btnOK.Font = new Font("微软雅黑", 10F);
            btnOK.Location = new Point(209, 590);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(80, 35);
            btnOK.TabIndex = 22;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(309, 590);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 35);
            btnCancel.TabIndex = 23;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnTest
            // 
            btnTest.Font = new Font("微软雅黑", 10F);
            btnTest.Location = new Point(409, 590);
            btnTest.MinimumSize = new Size(1, 1);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(80, 35);
            btnTest.TabIndex = 24;
            btnTest.Text = "测试";
            btnTest.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnHelp
            // 
            btnHelp.Font = new Font("微软雅黑", 10F);
            btnHelp.Location = new Point(509, 590);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(80, 35);
            btnHelp.TabIndex = 25;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // Form_WaitForStable
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(600, 644);
            Controls.Add(btnHelp);
            Controls.Add(btnTest);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(pnlMain);
            Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_WaitForStable";
            RectColor = Color.FromArgb(65, 100, 204);
            StartPosition = FormStartPosition.CenterParent;
            Text = "等待变量稳定配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            ZoomScaleRect = new Rectangle(15, 15, 600, 600);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIPanel  pnlMain;
        private Line lineSection1;
        private TextBox txtDescription;
        private UIComboBox cmbMonitorVariable;
        private UILine lineSection2;
        private UILabel lblStabilityThreshold;
        private AntdUI.InputNumber numStabilityThreshold;
        private UILabel lblSamplingInterval;
        private UINumPadTextBox numSamplingInterval;
        private UILabel lblStableCount;
        private AntdUI.InputNumber numStableCount;
        private UILine lineSection3;
        private UILabel lblTimeout;
        private AntdUI.InputNumber numTimeout;
        private UILabel lblTimeoutAction;
        private UIComboBox cmbTimeoutAction;
        private UILabel lblTimeoutJumpStep;
        private AntdUI.InputNumber numTimeoutJumpStep;
        private UILine lineSection4;
        private UILabel lblAssignToVariable;
        private UIComboBox cmbAssignToVariable;
        private UISymbolButton btnOK;
        private UISymbolButton btnCancel;
        private UISymbolButton btnTest;
        private UISymbolButton btnHelp;
        private UILabel lblDescription;
        private UILabel lblMonitorVariable;
    }
}
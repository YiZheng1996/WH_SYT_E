using Sunny.UI;

namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_Detection
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing)
            {
                _validationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelMain = new Panel();
            panelResult = new Panel();
            cmbFailureAction = new UIComboBox();
            lblFailureAction = new UILabel();
            cmbValueVariable = new UIComboBox();
            chkSaveValue = new UICheckBox();
            cmbResultVariable = new UIComboBox();
            chkSaveResult = new UICheckBox();
            lblResultTitle = new UILabel();
            panelTimeout = new Panel();
            numRetryIntervalMs = new UIIntegerUpDown();
            lblRetryIntervalMs = new UILabel();
            numRetryCount = new UIIntegerUpDown();
            lblRetryCount = new UILabel();
            numRefreshRateMs = new UIIntegerUpDown();
            lblRefreshRateMs = new UILabel();
            numTimeoutMs = new UIIntegerUpDown();
            lblTimeoutMs = new UILabel();
            lblTimeoutTitle = new UILabel();
            panelCondition = new Panel();
            lblValidationStatus = new UILabel();
            txtConditionExpression = new UITextBox();
            lblConditionExpression = new UILabel();
            lblConditionTitle = new UILabel();
            panelBasicInfo = new Panel();
            lblTip = new UILabel();
            txtDetectionName = new UITextBox();
            lblDetectionName = new UILabel();
            panelBottom = new Panel();
            btnCancel = new UIButton();
            btnSave = new UIButton();
            panelMain.SuspendLayout();
            panelResult.SuspendLayout();
            panelTimeout.SuspendLayout();
            panelCondition.SuspendLayout();
            panelBasicInfo.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.BackColor = Color.FromArgb(248, 249, 250);
            panelMain.Controls.Add(panelResult);
            panelMain.Controls.Add(panelTimeout);
            panelMain.Controls.Add(panelCondition);
            panelMain.Controls.Add(panelBasicInfo);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15, 10, 15, 10);
            panelMain.Size = new Size(718, 562);
            panelMain.TabIndex = 0;
            // 
            // panelResult
            // 
            panelResult.BackColor = Color.White;
            panelResult.Controls.Add(cmbFailureAction);
            panelResult.Controls.Add(lblFailureAction);
            panelResult.Controls.Add(cmbValueVariable);
            panelResult.Controls.Add(chkSaveValue);
            panelResult.Controls.Add(cmbResultVariable);
            panelResult.Controls.Add(chkSaveResult);
            panelResult.Controls.Add(lblResultTitle);
            panelResult.Dock = DockStyle.Top;
            panelResult.Location = new Point(15, 430);
            panelResult.Margin = new Padding(0, 10, 0, 10);
            panelResult.Name = "panelResult";
            panelResult.Padding = new Padding(15);
            panelResult.Size = new Size(688, 121);
            panelResult.TabIndex = 4;
            // 
            // cmbFailureAction
            // 
            cmbFailureAction.DataSource = null;
            cmbFailureAction.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbFailureAction.FillColor = Color.White;
            cmbFailureAction.Font = new Font("微软雅黑", 9F);
            cmbFailureAction.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbFailureAction.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbFailureAction.Location = new Point(441, 63);
            cmbFailureAction.Margin = new Padding(4, 5, 4, 5);
            cmbFailureAction.MinimumSize = new Size(63, 0);
            cmbFailureAction.Name = "cmbFailureAction";
            cmbFailureAction.Padding = new Padding(0, 0, 30, 2);
            cmbFailureAction.Size = new Size(193, 28);
            cmbFailureAction.SymbolSize = 24;
            cmbFailureAction.TabIndex = 6;
            cmbFailureAction.TextAlignment = ContentAlignment.MiddleLeft;
            cmbFailureAction.Watermark = "";
            // 
            // lblFailureAction
            // 
            lblFailureAction.Font = new Font("微软雅黑", 9F);
            lblFailureAction.ForeColor = Color.FromArgb(48, 48, 48);
            lblFailureAction.Location = new Point(366, 63);
            lblFailureAction.Name = "lblFailureAction";
            lblFailureAction.Size = new Size(70, 25);
            lblFailureAction.TabIndex = 5;
            lblFailureAction.Text = "失败处理:";
            lblFailureAction.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbValueVariable
            // 
            cmbValueVariable.DataSource = null;
            cmbValueVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbValueVariable.Enabled = false;
            cmbValueVariable.FillColor = Color.White;
            cmbValueVariable.Font = new Font("微软雅黑", 9F);
            cmbValueVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbValueVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbValueVariable.Location = new Point(431, 148);
            cmbValueVariable.Margin = new Padding(4, 5, 4, 5);
            cmbValueVariable.MinimumSize = new Size(63, 0);
            cmbValueVariable.Name = "cmbValueVariable";
            cmbValueVariable.Padding = new Padding(0, 0, 30, 2);
            cmbValueVariable.Size = new Size(150, 28);
            cmbValueVariable.SymbolSize = 24;
            cmbValueVariable.TabIndex = 4;
            cmbValueVariable.TextAlignment = ContentAlignment.MiddleLeft;
            cmbValueVariable.Visible = false;
            cmbValueVariable.Watermark = "选择变量...";
            // 
            // chkSaveValue
            // 
            chkSaveValue.Font = new Font("微软雅黑", 9F);
            chkSaveValue.ForeColor = Color.FromArgb(48, 48, 48);
            chkSaveValue.Location = new Point(324, 151);
            chkSaveValue.MinimumSize = new Size(1, 1);
            chkSaveValue.Name = "chkSaveValue";
            chkSaveValue.Size = new Size(100, 25);
            chkSaveValue.TabIndex = 3;
            chkSaveValue.Text = "保存检测值:";
            chkSaveValue.Visible = false;
            // 
            // cmbResultVariable
            // 
            cmbResultVariable.DataSource = null;
            cmbResultVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbResultVariable.Enabled = false;
            cmbResultVariable.FillColor = Color.White;
            cmbResultVariable.Font = new Font("微软雅黑", 9F);
            cmbResultVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbResultVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbResultVariable.Location = new Point(118, 63);
            cmbResultVariable.Margin = new Padding(4, 5, 4, 5);
            cmbResultVariable.MinimumSize = new Size(63, 0);
            cmbResultVariable.Name = "cmbResultVariable";
            cmbResultVariable.Padding = new Padding(0, 0, 30, 2);
            cmbResultVariable.Size = new Size(210, 28);
            cmbResultVariable.SymbolSize = 24;
            cmbResultVariable.TabIndex = 2;
            cmbResultVariable.TextAlignment = ContentAlignment.MiddleLeft;
            cmbResultVariable.Watermark = "选择变量...";
            // 
            // chkSaveResult
            // 
            chkSaveResult.Font = new Font("微软雅黑", 9F);
            chkSaveResult.ForeColor = Color.FromArgb(48, 48, 48);
            chkSaveResult.Location = new Point(18, 63);
            chkSaveResult.MinimumSize = new Size(1, 1);
            chkSaveResult.Name = "chkSaveResult";
            chkSaveResult.Size = new Size(100, 25);
            chkSaveResult.TabIndex = 1;
            chkSaveResult.Text = "保存结果:";
            // 
            // lblResultTitle
            // 
            lblResultTitle.BackColor = Color.FromArgb(65, 100, 204);
            lblResultTitle.Dock = DockStyle.Top;
            lblResultTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblResultTitle.ForeColor = Color.White;
            lblResultTitle.Location = new Point(15, 15);
            lblResultTitle.Name = "lblResultTitle";
            lblResultTitle.Padding = new Padding(10, 0, 0, 0);
            lblResultTitle.Size = new Size(658, 28);
            lblResultTitle.TabIndex = 0;
            lblResultTitle.Text = "结果处理";
            lblResultTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelTimeout
            // 
            panelTimeout.BackColor = Color.White;
            panelTimeout.Controls.Add(numRetryIntervalMs);
            panelTimeout.Controls.Add(lblRetryIntervalMs);
            panelTimeout.Controls.Add(numRetryCount);
            panelTimeout.Controls.Add(lblRetryCount);
            panelTimeout.Controls.Add(numRefreshRateMs);
            panelTimeout.Controls.Add(lblRefreshRateMs);
            panelTimeout.Controls.Add(numTimeoutMs);
            panelTimeout.Controls.Add(lblTimeoutMs);
            panelTimeout.Controls.Add(lblTimeoutTitle);
            panelTimeout.Dock = DockStyle.Top;
            panelTimeout.Location = new Point(15, 298);
            panelTimeout.Margin = new Padding(0, 10, 0, 10);
            panelTimeout.Name = "panelTimeout";
            panelTimeout.Padding = new Padding(15);
            panelTimeout.Size = new Size(688, 132);
            panelTimeout.TabIndex = 3;
            // 
            // numRetryIntervalMs
            // 
            numRetryIntervalMs.Font = new Font("微软雅黑", 9F);
            numRetryIntervalMs.Location = new Point(466, 93);
            numRetryIntervalMs.Margin = new Padding(4, 5, 4, 5);
            numRetryIntervalMs.Maximum = 60000D;
            numRetryIntervalMs.Minimum = 100D;
            numRetryIntervalMs.MinimumSize = new Size(1, 16);
            numRetryIntervalMs.Name = "numRetryIntervalMs";
            numRetryIntervalMs.Padding = new Padding(5);
            numRetryIntervalMs.ShowText = false;
            numRetryIntervalMs.Size = new Size(145, 28);
            numRetryIntervalMs.Step = 100;
            numRetryIntervalMs.TabIndex = 8;
            numRetryIntervalMs.Text = "1000";
            numRetryIntervalMs.TextAlignment = ContentAlignment.MiddleCenter;
            numRetryIntervalMs.Value = 1000;
            // 
            // lblRetryIntervalMs
            // 
            lblRetryIntervalMs.Font = new Font("微软雅黑", 9F);
            lblRetryIntervalMs.ForeColor = Color.FromArgb(48, 48, 48);
            lblRetryIntervalMs.Location = new Point(366, 92);
            lblRetryIntervalMs.Name = "lblRetryIntervalMs";
            lblRetryIntervalMs.Size = new Size(85, 25);
            lblRetryIntervalMs.TabIndex = 7;
            lblRetryIntervalMs.Text = "重试间隔(ms):";
            lblRetryIntervalMs.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numRetryCount
            // 
            numRetryCount.Font = new Font("微软雅黑", 9F);
            numRetryCount.Location = new Point(137, 93);
            numRetryCount.Margin = new Padding(4, 5, 4, 5);
            numRetryCount.Maximum = 100D;
            numRetryCount.Minimum = 0D;
            numRetryCount.MinimumSize = new Size(1, 16);
            numRetryCount.Name = "numRetryCount";
            numRetryCount.Padding = new Padding(5);
            numRetryCount.ShowText = false;
            numRetryCount.Size = new Size(145, 28);
            numRetryCount.TabIndex = 6;
            numRetryCount.Text = "0";
            numRetryCount.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblRetryCount
            // 
            lblRetryCount.Font = new Font("微软雅黑", 9F);
            lblRetryCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblRetryCount.Location = new Point(24, 92);
            lblRetryCount.Name = "lblRetryCount";
            lblRetryCount.Size = new Size(70, 25);
            lblRetryCount.TabIndex = 5;
            lblRetryCount.Text = "重试次数:";
            lblRetryCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numRefreshRateMs
            // 
            numRefreshRateMs.Font = new Font("微软雅黑", 9F);
            numRefreshRateMs.Location = new Point(466, 55);
            numRefreshRateMs.Margin = new Padding(4, 5, 4, 5);
            numRefreshRateMs.Maximum = 10000D;
            numRefreshRateMs.Minimum = 10D;
            numRefreshRateMs.MinimumSize = new Size(1, 16);
            numRefreshRateMs.Name = "numRefreshRateMs";
            numRefreshRateMs.Padding = new Padding(5);
            numRefreshRateMs.ShowText = false;
            numRefreshRateMs.Size = new Size(145, 28);
            numRefreshRateMs.Step = 10;
            numRefreshRateMs.TabIndex = 4;
            numRefreshRateMs.Text = "100";
            numRefreshRateMs.TextAlignment = ContentAlignment.MiddleCenter;
            numRefreshRateMs.Value = 100;
            // 
            // lblRefreshRateMs
            // 
            lblRefreshRateMs.Font = new Font("微软雅黑", 9F);
            lblRefreshRateMs.ForeColor = Color.FromArgb(48, 48, 48);
            lblRefreshRateMs.Location = new Point(366, 58);
            lblRefreshRateMs.Name = "lblRefreshRateMs";
            lblRefreshRateMs.Size = new Size(85, 25);
            lblRefreshRateMs.TabIndex = 3;
            lblRefreshRateMs.Text = "刷新频率(ms):";
            lblRefreshRateMs.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numTimeoutMs
            // 
            numTimeoutMs.Font = new Font("微软雅黑", 9F);
            numTimeoutMs.Location = new Point(137, 55);
            numTimeoutMs.Margin = new Padding(4, 5, 4, 5);
            numTimeoutMs.Maximum = 600000D;
            numTimeoutMs.Minimum = 0D;
            numTimeoutMs.MinimumSize = new Size(1, 16);
            numTimeoutMs.Name = "numTimeoutMs";
            numTimeoutMs.Padding = new Padding(5);
            numTimeoutMs.ShowText = false;
            numTimeoutMs.Size = new Size(145, 28);
            numTimeoutMs.Step = 100;
            numTimeoutMs.TabIndex = 2;
            numTimeoutMs.Text = "5000";
            numTimeoutMs.TextAlignment = ContentAlignment.MiddleCenter;
            numTimeoutMs.Value = 5000;
            // 
            // lblTimeoutMs
            // 
            lblTimeoutMs.Font = new Font("微软雅黑", 9F);
            lblTimeoutMs.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeoutMs.Location = new Point(24, 58);
            lblTimeoutMs.Name = "lblTimeoutMs";
            lblTimeoutMs.Size = new Size(94, 25);
            lblTimeoutMs.TabIndex = 1;
            lblTimeoutMs.Text = "超时时间(ms):";
            lblTimeoutMs.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTimeoutTitle
            // 
            lblTimeoutTitle.BackColor = Color.FromArgb(65, 100, 204);
            lblTimeoutTitle.Dock = DockStyle.Top;
            lblTimeoutTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblTimeoutTitle.ForeColor = Color.White;
            lblTimeoutTitle.Location = new Point(15, 15);
            lblTimeoutTitle.Name = "lblTimeoutTitle";
            lblTimeoutTitle.Padding = new Padding(10, 0, 0, 0);
            lblTimeoutTitle.Size = new Size(658, 28);
            lblTimeoutTitle.TabIndex = 0;
            lblTimeoutTitle.Text = "超时和重试";
            lblTimeoutTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelCondition
            // 
            panelCondition.BackColor = Color.White;
            panelCondition.Controls.Add(lblValidationStatus);
            panelCondition.Controls.Add(txtConditionExpression);
            panelCondition.Controls.Add(lblConditionExpression);
            panelCondition.Controls.Add(lblConditionTitle);
            panelCondition.Dock = DockStyle.Top;
            panelCondition.Location = new Point(15, 90);
            panelCondition.Margin = new Padding(0, 10, 0, 10);
            panelCondition.Name = "panelCondition";
            panelCondition.Padding = new Padding(15);
            panelCondition.Size = new Size(688, 208);
            panelCondition.TabIndex = 2;
            // 
            // lblValidationStatus
            // 
            lblValidationStatus.Font = new Font("微软雅黑", 10F);
            lblValidationStatus.ForeColor = Color.Gray;
            lblValidationStatus.Location = new Point(100, 53);
            lblValidationStatus.Name = "lblValidationStatus";
            lblValidationStatus.Size = new Size(556, 25);
            lblValidationStatus.TabIndex = 5;
            lblValidationStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtConditionExpression
            // 
            txtConditionExpression.Cursor = Cursors.IBeam;
            txtConditionExpression.Font = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtConditionExpression.Location = new Point(15, 80);
            txtConditionExpression.Margin = new Padding(4, 5, 4, 5);
            txtConditionExpression.MinimumSize = new Size(1, 16);
            txtConditionExpression.Multiline = true;
            txtConditionExpression.Name = "txtConditionExpression";
            txtConditionExpression.Padding = new Padding(5);
            txtConditionExpression.ShowText = false;
            txtConditionExpression.Size = new Size(654, 113);
            txtConditionExpression.TabIndex = 2;
            txtConditionExpression.TextAlignment = ContentAlignment.TopLeft;
            txtConditionExpression.Watermark = "点击配置检测条件表达式... (按F2打开面板)";
            // 
            // lblConditionExpression
            // 
            lblConditionExpression.Font = new Font("微软雅黑", 10F);
            lblConditionExpression.ForeColor = Color.FromArgb(48, 48, 48);
            lblConditionExpression.Location = new Point(15, 53);
            lblConditionExpression.Name = "lblConditionExpression";
            lblConditionExpression.Size = new Size(80, 25);
            lblConditionExpression.TabIndex = 1;
            lblConditionExpression.Text = "条件表达式:";
            lblConditionExpression.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblConditionTitle
            // 
            lblConditionTitle.BackColor = Color.FromArgb(65, 100, 204);
            lblConditionTitle.Dock = DockStyle.Top;
            lblConditionTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblConditionTitle.ForeColor = Color.White;
            lblConditionTitle.Location = new Point(15, 15);
            lblConditionTitle.Name = "lblConditionTitle";
            lblConditionTitle.Padding = new Padding(10, 0, 0, 0);
            lblConditionTitle.Size = new Size(658, 28);
            lblConditionTitle.TabIndex = 0;
            lblConditionTitle.Text = "检测条件 (使用{value}代表数据源的值)";
            lblConditionTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBasicInfo
            // 
            panelBasicInfo.BackColor = Color.White;
            panelBasicInfo.Controls.Add(lblTip);
            panelBasicInfo.Controls.Add(txtDetectionName);
            panelBasicInfo.Controls.Add(lblDetectionName);
            panelBasicInfo.Dock = DockStyle.Top;
            panelBasicInfo.Location = new Point(15, 10);
            panelBasicInfo.Margin = new Padding(0, 0, 0, 10);
            panelBasicInfo.Name = "panelBasicInfo";
            panelBasicInfo.Padding = new Padding(15);
            panelBasicInfo.Size = new Size(688, 80);
            panelBasicInfo.TabIndex = 0;
            // 
            // lblTip
            // 
            lblTip.Font = new Font("微软雅黑", 9F);
            lblTip.ForeColor = Color.Gray;
            lblTip.Location = new Point(15, 10);
            lblTip.Name = "lblTip";
            lblTip.Size = new Size(540, 20);
            lblTip.TabIndex = 0;
            lblTip.Text = "配置检测条件，等待数据源满足表达式后继续执行";
            lblTip.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDetectionName
            // 
            txtDetectionName.Cursor = Cursors.IBeam;
            txtDetectionName.Font = new Font("微软雅黑", 10F);
            txtDetectionName.Location = new Point(100, 38);
            txtDetectionName.Margin = new Padding(4, 5, 4, 5);
            txtDetectionName.MinimumSize = new Size(1, 16);
            txtDetectionName.Name = "txtDetectionName";
            txtDetectionName.Padding = new Padding(5);
            txtDetectionName.ShowText = false;
            txtDetectionName.Size = new Size(569, 30);
            txtDetectionName.TabIndex = 2;
            txtDetectionName.TextAlignment = ContentAlignment.MiddleLeft;
            txtDetectionName.Watermark = "输入检测项名称...";
            // 
            // lblDetectionName
            // 
            lblDetectionName.Font = new Font("微软雅黑", 10F);
            lblDetectionName.ForeColor = Color.FromArgb(48, 48, 48);
            lblDetectionName.Location = new Point(15, 40);
            lblDetectionName.Name = "lblDetectionName";
            lblDetectionName.Size = new Size(80, 25);
            lblDetectionName.TabIndex = 1;
            lblDetectionName.Text = "检测名称:";
            lblDetectionName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(240, 240, 240);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 597);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(718, 55);
            panelBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(180, 180, 180);
            btnCancel.FillColor2 = SystemColors.ActiveBorder;
            btnCancel.FillHoverColor = Color.FromArgb(200, 200, 200);
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(608, 12);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = SystemColors.ActiveBorder;
            btnCancel.RectHoverColor = SystemColors.ActiveBorder;
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.FromArgb(65, 100, 204);
            btnSave.FillDisableColor = Color.FromArgb(65, 100, 204);
            btnSave.FillHoverColor = Color.FromArgb(85, 120, 224);
            btnSave.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnSave.Location = new Point(498, 12);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectColor = Color.FromArgb(65, 100, 204);
            btnSave.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 保存";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            // 
            // Form_Detection
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(718, 652);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Detection";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Text = "检测工具配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 600, 610);
            panelMain.ResumeLayout(false);
            panelResult.ResumeLayout(false);
            panelTimeout.ResumeLayout(false);
            panelCondition.ResumeLayout(false);
            panelBasicInfo.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // 主区域面板
        private Panel panelMain;
        private Panel panelBottom;

        // 基本信息区域
        private Panel panelBasicInfo;
        private UILabel lblDetectionName;
        private UITextBox txtDetectionName;
        private UILabel lblTip;

        // 检测条件区域
        private Panel panelCondition;
        private UILabel lblConditionTitle;
        private UILabel lblConditionExpression;
        private UITextBox txtConditionExpression;
        private UILabel lblValidationStatus;

        // 超时重试区域
        private Panel panelTimeout;
        private UILabel lblTimeoutTitle;
        private UILabel lblTimeoutMs;
        private UIIntegerUpDown numTimeoutMs;
        private UILabel lblRefreshRateMs;
        private UIIntegerUpDown numRefreshRateMs;
        private UILabel lblRetryCount;
        private UIIntegerUpDown numRetryCount;
        private UILabel lblRetryIntervalMs;
        private UIIntegerUpDown numRetryIntervalMs;

        // 结果处理区域
        private Panel panelResult;
        private UILabel lblResultTitle;
        private UICheckBox chkSaveResult;
        private UIComboBox cmbResultVariable;
        private UICheckBox chkSaveValue;
        private UIComboBox cmbValueVariable;
        private UILabel lblFailureAction;
        private UIComboBox cmbFailureAction;

        // 底部按钮
        private UIButton btnSave;
        private UIButton btnCancel;
    }
}
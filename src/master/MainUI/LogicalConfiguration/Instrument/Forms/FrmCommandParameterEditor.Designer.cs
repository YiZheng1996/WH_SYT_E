using Sunny.UI;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    partial class FrmCommandParameterEditor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelMain = new Panel();
            grpBasic = new UIGroupBox();
            lblName = new UILabel();
            txtName = new UITextBox();
            lblDisplayName = new UILabel();
            txtDisplayName = new UITextBox();
            lblDataType = new UILabel();
            cboDataType = new UIComboBox();
            lblDefault = new UILabel();
            txtDefaultValue = new UITextBox();
            chkRequired = new UICheckBox();
            lblDescription = new UILabel();
            txtDescription = new UITextBox();
            grpRange = new UIGroupBox();
            chkHasRange = new UICheckBox();
            panelRange = new Panel();
            lblMin = new UILabel();
            numMin = new UIDoubleUpDown();
            lblMax = new UILabel();
            numMax = new UIDoubleUpDown();
            panelBottom = new Panel();
            btnCancel = new UIButton();
            btnOk = new UIButton();
            panelMain.SuspendLayout();
            grpBasic.SuspendLayout();
            grpRange.SuspendLayout();
            panelRange.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(248, 249, 250);
            panelMain.Controls.Add(grpBasic);
            panelMain.Controls.Add(grpRange);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(12);
            panelMain.Size = new Size(500, 381);
            panelMain.TabIndex = 0;
            // 
            // grpBasic
            // 
            grpBasic.Controls.Add(lblName);
            grpBasic.Controls.Add(txtName);
            grpBasic.Controls.Add(lblDisplayName);
            grpBasic.Controls.Add(txtDisplayName);
            grpBasic.Controls.Add(lblDataType);
            grpBasic.Controls.Add(cboDataType);
            grpBasic.Controls.Add(lblDefault);
            grpBasic.Controls.Add(txtDefaultValue);
            grpBasic.Controls.Add(chkRequired);
            grpBasic.Controls.Add(lblDescription);
            grpBasic.Controls.Add(txtDescription);
            grpBasic.Dock = DockStyle.Top;
            grpBasic.Font = new Font("微软雅黑", 12F);
            grpBasic.Location = new Point(12, 107);
            grpBasic.Margin = new Padding(4, 5, 4, 5);
            grpBasic.MinimumSize = new Size(1, 1);
            grpBasic.Name = "grpBasic";
            grpBasic.Padding = new Padding(10, 32, 10, 10);
            grpBasic.Size = new Size(476, 265);
            grpBasic.TabIndex = 0;
            grpBasic.Text = "参数基本信息";
            grpBasic.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("微软雅黑", 11F);
            lblName.ForeColor = Color.FromArgb(48, 48, 48);
            lblName.Location = new Point(14, 42);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 20);
            lblName.TabIndex = 0;
            lblName.Text = "参数名称*:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Font = new Font("微软雅黑", 12F);
            txtName.Location = new Point(104, 38);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.MinimumSize = new Size(1, 16);
            txtName.Name = "txtName";
            txtName.Padding = new Padding(5);
            txtName.ShowText = false;
            txtName.Size = new Size(310, 30);
            txtName.TabIndex = 1;
            txtName.TextAlignment = ContentAlignment.MiddleLeft;
            txtName.Watermark = "英文标识符，如 Temperature";
            // 
            // lblDisplayName
            // 
            lblDisplayName.AutoSize = true;
            lblDisplayName.Font = new Font("微软雅黑", 11F);
            lblDisplayName.ForeColor = Color.FromArgb(48, 48, 48);
            lblDisplayName.Location = new Point(14, 84);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(80, 20);
            lblDisplayName.TabIndex = 2;
            lblDisplayName.Text = "显示名称*:";
            lblDisplayName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDisplayName
            // 
            txtDisplayName.Font = new Font("微软雅黑", 12F);
            txtDisplayName.Location = new Point(104, 80);
            txtDisplayName.Margin = new Padding(4, 5, 4, 5);
            txtDisplayName.MinimumSize = new Size(1, 16);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Padding = new Padding(5);
            txtDisplayName.ShowText = false;
            txtDisplayName.Size = new Size(310, 30);
            txtDisplayName.TabIndex = 3;
            txtDisplayName.TextAlignment = ContentAlignment.MiddleLeft;
            txtDisplayName.Watermark = "中文名称，如 目标温度";
            // 
            // lblDataType
            // 
            lblDataType.AutoSize = true;
            lblDataType.Font = new Font("微软雅黑", 11F);
            lblDataType.ForeColor = Color.FromArgb(48, 48, 48);
            lblDataType.Location = new Point(14, 126);
            lblDataType.Name = "lblDataType";
            lblDataType.Size = new Size(80, 20);
            lblDataType.TabIndex = 4;
            lblDataType.Text = "数据类型*:";
            lblDataType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboDataType
            // 
            cboDataType.DataSource = null;
            cboDataType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboDataType.FillColor = Color.White;
            cboDataType.Font = new Font("微软雅黑", 12F);
            cboDataType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboDataType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboDataType.Location = new Point(104, 122);
            cboDataType.Margin = new Padding(4, 5, 4, 5);
            cboDataType.MinimumSize = new Size(63, 0);
            cboDataType.Name = "cboDataType";
            cboDataType.Padding = new Padding(0, 0, 30, 2);
            cboDataType.Size = new Size(180, 30);
            cboDataType.SymbolSize = 24;
            cboDataType.TabIndex = 5;
            cboDataType.TextAlignment = ContentAlignment.MiddleLeft;
            cboDataType.Watermark = "请选择";
            // 
            // lblDefault
            // 
            lblDefault.AutoSize = true;
            lblDefault.Font = new Font("微软雅黑", 11F);
            lblDefault.ForeColor = Color.FromArgb(48, 48, 48);
            lblDefault.Location = new Point(14, 168);
            lblDefault.Name = "lblDefault";
            lblDefault.Size = new Size(58, 20);
            lblDefault.TabIndex = 6;
            lblDefault.Text = "默认值:";
            lblDefault.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDefaultValue
            // 
            txtDefaultValue.Font = new Font("微软雅黑", 12F);
            txtDefaultValue.Location = new Point(104, 164);
            txtDefaultValue.Margin = new Padding(4, 5, 4, 5);
            txtDefaultValue.MinimumSize = new Size(1, 16);
            txtDefaultValue.Name = "txtDefaultValue";
            txtDefaultValue.Padding = new Padding(5);
            txtDefaultValue.ShowText = false;
            txtDefaultValue.Size = new Size(180, 30);
            txtDefaultValue.TabIndex = 7;
            txtDefaultValue.TextAlignment = ContentAlignment.MiddleLeft;
            txtDefaultValue.Watermark = "可为空";
            // 
            // chkRequired
            // 
            chkRequired.Font = new Font("微软雅黑", 11F);
            chkRequired.ForeColor = Color.FromArgb(48, 48, 48);
            chkRequired.Location = new Point(300, 164);
            chkRequired.MinimumSize = new Size(1, 1);
            chkRequired.Name = "chkRequired";
            chkRequired.Size = new Size(120, 30);
            chkRequired.TabIndex = 8;
            chkRequired.Text = "必填参数";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("微软雅黑", 11F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(14, 210);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(73, 20);
            lblDescription.TabIndex = 9;
            lblDescription.Text = "参数说明:";
            lblDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("微软雅黑", 12F);
            txtDescription.Location = new Point(104, 206);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(310, 30);
            txtDescription.TabIndex = 10;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "可选，填写参数用途说明";
            // 
            // grpRange
            // 
            grpRange.Controls.Add(chkHasRange);
            grpRange.Controls.Add(panelRange);
            grpRange.Dock = DockStyle.Top;
            grpRange.Font = new Font("微软雅黑", 12F);
            grpRange.Location = new Point(12, 12);
            grpRange.Margin = new Padding(4, 8, 4, 5);
            grpRange.MinimumSize = new Size(1, 1);
            grpRange.Name = "grpRange";
            grpRange.Padding = new Padding(10, 32, 10, 10);
            grpRange.Size = new Size(476, 95);
            grpRange.TabIndex = 1;
            grpRange.Text = "数值范围限制（可选）";
            grpRange.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // chkHasRange
            // 
            chkHasRange.Font = new Font("微软雅黑", 11F);
            chkHasRange.ForeColor = Color.FromArgb(48, 48, 48);
            chkHasRange.Location = new Point(14, 38);
            chkHasRange.MinimumSize = new Size(1, 1);
            chkHasRange.Name = "chkHasRange";
            chkHasRange.Size = new Size(100, 28);
            chkHasRange.TabIndex = 0;
            chkHasRange.Text = "启用范围";
            chkHasRange.CheckedChanged += ChkHasRange_CheckedChanged;
            // 
            // panelRange
            // 
            panelRange.Controls.Add(numMax);
            panelRange.Controls.Add(numMin);
            panelRange.Controls.Add(lblMin);
            panelRange.Controls.Add(lblMax);
            panelRange.Location = new Point(120, 36);
            panelRange.Name = "panelRange";
            panelRange.Size = new Size(343, 35);
            panelRange.TabIndex = 1;
            panelRange.Visible = false;
            // 
            // lblMin
            // 
            lblMin.AutoSize = true;
            lblMin.Font = new Font("微软雅黑", 11F);
            lblMin.ForeColor = Color.FromArgb(48, 48, 48);
            lblMin.Location = new Point(2, 6);
            lblMin.Name = "lblMin";
            lblMin.Size = new Size(43, 20);
            lblMin.TabIndex = 0;
            lblMin.Text = "最小:";
            // 
            // numMin
            // 
            numMin.Font = new Font("微软雅黑", 12F);
            numMin.Location = new Point(44, 2);
            numMin.Margin = new Padding(4, 5, 4, 5);
            numMin.Maximum = 999999D;
            numMin.Minimum = -999999D;
            numMin.MinimumSize = new Size(1, 16);
            numMin.Name = "numMin";
            numMin.Padding = new Padding(5);
            numMin.ShowText = false;
            numMin.Size = new Size(124, 30);
            numMin.Step = 1D;
            numMin.TabIndex = 0;
            numMin.Text = "0.00";
            numMin.TextAlignment = ContentAlignment.MiddleCenter;
            numMin.Value = 0D;
            // 
            // lblMax
            // 
            lblMax.AutoSize = true;
            lblMax.Font = new Font("微软雅黑", 11F);
            lblMax.ForeColor = Color.FromArgb(48, 48, 48);
            lblMax.Location = new Point(171, 6);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(43, 20);
            lblMax.TabIndex = 1;
            lblMax.Text = "最大:";
            // 
            // numMax
            // 
            numMax.Font = new Font("微软雅黑", 12F);
            numMax.Location = new Point(214, 2);
            numMax.Margin = new Padding(4, 5, 4, 5);
            numMax.Maximum = 999999D;
            numMax.Minimum = -999999D;
            numMax.MinimumSize = new Size(1, 16);
            numMax.Name = "numMax";
            numMax.Padding = new Padding(5);
            numMax.ShowText = false;
            numMax.Size = new Size(124, 30);
            numMax.Step = 1D;
            numMax.TabIndex = 1;
            numMax.Text = "9999.00";
            numMax.TextAlignment = ContentAlignment.MiddleCenter;
            numMax.Value = 9999D;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnOk);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 416);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(0, 8, 16, 8);
            panelBottom.Size = new Size(500, 52);
            panelBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("微软雅黑", 12F);
            btnCancel.Location = new Point(398, 6);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 36);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            // 
            // btnOk
            // 
            btnOk.FillColor = Color.FromArgb(65, 100, 204);
            btnOk.Font = new Font("微软雅黑", 12F);
            btnOk.Location = new Point(302, 6);
            btnOk.MinimumSize = new Size(1, 1);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(88, 36);
            btnOk.TabIndex = 0;
            btnOk.Text = "确定";
            // 
            // FrmCommandParameterEditor
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 468);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Font = new Font("微软雅黑", 12F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCommandParameterEditor";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "参数编辑";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 13F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 460, 477);
            panelMain.ResumeLayout(false);
            grpBasic.ResumeLayout(false);
            grpBasic.PerformLayout();
            grpRange.ResumeLayout(false);
            panelRange.ResumeLayout(false);
            panelRange.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private UIGroupBox grpBasic;
        private UILabel lblName;
        private UITextBox txtName;
        private UILabel lblDisplayName;
        private UITextBox txtDisplayName;
        private UILabel lblDataType;
        private UIComboBox cboDataType;
        private UILabel lblDefault;
        private UITextBox txtDefaultValue;
        private UICheckBox chkRequired;
        private UILabel lblDescription;
        private UITextBox txtDescription;
        private UIGroupBox grpRange;
        private UICheckBox chkHasRange;
        private Panel panelRange;
        private UILabel lblMin;
        private UIDoubleUpDown numMin;
        private UILabel lblMax;
        private UIDoubleUpDown numMax;
        private Panel panelBottom;
        private UIButton btnOk;
        private UIButton btnCancel;
    }
}
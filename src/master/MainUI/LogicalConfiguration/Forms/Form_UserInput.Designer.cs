namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_UserInput
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
            components = new System.ComponentModel.Container();
            pnlMain = new UIPanel();
            pnlContent = new UIPanel();
            lblTitle = new UILabel();
            txtTitle = new UITextBox();
            lblPrompt = new UILabel();
            txtPrompt = new UITextBox();
            uiLine1 = new UILine();
            lblInputType = new UILabel();
            cmbInputType = new UIComboBox();
            lblTargetVar = new UILabel();
            txtTargetVar = new UITextBox();
            lblDefault = new UILabel();
            txtDefaultValue = new UITextBox();
            lblOptions = new UILabel();
            txtSelectOptions = new UITextBox();
            lblNumRange = new UILabel();
            txtMinValue = new UITextBox();
            lblRangeSep = new UILabel();
            txtMaxValue = new UITextBox();
            lblDecimal = new UILabel();
            nudDecimalPlaces = new UIIntegerUpDown();
            chkAllowEmpty = new UICheckBox();
            uiLine2 = new UILine();
            lblTimeout = new UILabel();
            nudTimeout = new UIIntegerUpDown();
            lblTimeoutUnit = new UILabel();
            lblOnTimeout = new UILabel();
            cmbOnTimeout = new UIComboBox();
            lblTimeoutDef = new UILabel();
            txtTimeoutDefault = new UITextBox();
            lblDescription = new UILabel();
            txtDescription = new UITextBox();
            pnlBottom = new UIPanel();
            btnCancel = new UISymbolButton();
            btnSave = new UISymbolButton();
            pnlHeader = new UIPanel();
            lblHeaderDesc = new UILabel();
            toolTip = new ToolTip(components);
            pnlMain.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlBottom.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlBottom);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.White;
            pnlMain.FillColor2 = Color.White;
            pnlMain.Font = new Font("微软雅黑", 10F);
            pnlMain.Location = new Point(0, 35);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.MinimumSize = new Size(1, 1);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(15);
            pnlMain.Size = new Size(540, 670);
            pnlMain.TabIndex = 0;
            pnlMain.Text = null;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(lblTitle);
            pnlContent.Controls.Add(txtTitle);
            pnlContent.Controls.Add(lblPrompt);
            pnlContent.Controls.Add(txtPrompt);
            pnlContent.Controls.Add(uiLine1);
            pnlContent.Controls.Add(lblInputType);
            pnlContent.Controls.Add(cmbInputType);
            pnlContent.Controls.Add(lblTargetVar);
            pnlContent.Controls.Add(txtTargetVar);
            pnlContent.Controls.Add(lblDefault);
            pnlContent.Controls.Add(txtDefaultValue);
            pnlContent.Controls.Add(lblOptions);
            pnlContent.Controls.Add(txtSelectOptions);
            pnlContent.Controls.Add(lblNumRange);
            pnlContent.Controls.Add(txtMinValue);
            pnlContent.Controls.Add(lblRangeSep);
            pnlContent.Controls.Add(txtMaxValue);
            pnlContent.Controls.Add(lblDecimal);
            pnlContent.Controls.Add(nudDecimalPlaces);
            pnlContent.Controls.Add(chkAllowEmpty);
            pnlContent.Controls.Add(uiLine2);
            pnlContent.Controls.Add(lblTimeout);
            pnlContent.Controls.Add(nudTimeout);
            pnlContent.Controls.Add(lblTimeoutUnit);
            pnlContent.Controls.Add(lblOnTimeout);
            pnlContent.Controls.Add(cmbOnTimeout);
            pnlContent.Controls.Add(lblTimeoutDef);
            pnlContent.Controls.Add(txtTimeoutDefault);
            pnlContent.Controls.Add(lblDescription);
            pnlContent.Controls.Add(txtDescription);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.FillColor = Color.White;
            pnlContent.FillColor2 = Color.White;
            pnlContent.Font = new Font("微软雅黑", 10F);
            pnlContent.Location = new Point(15, 59);
            pnlContent.Margin = new Padding(4, 5, 4, 5);
            pnlContent.MinimumSize = new Size(1, 1);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(510, 536);
            pnlContent.TabIndex = 1;
            pnlContent.Text = null;
            pnlContent.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(48, 48, 48);
            lblTitle.Location = new Point(10, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(80, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "窗口标题:";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTitle
            // 
            txtTitle.Font = new Font("微软雅黑", 10F);
            txtTitle.Location = new Point(96, 12);
            txtTitle.Margin = new Padding(4, 5, 4, 5);
            txtTitle.MinimumSize = new Size(1, 16);
            txtTitle.Name = "txtTitle";
            txtTitle.Padding = new Padding(5);
            txtTitle.ShowText = false;
            txtTitle.Size = new Size(400, 30);
            txtTitle.TabIndex = 1;
            txtTitle.Text = "请输入";
            txtTitle.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtTitle, "运行时弹窗的标题文字");
            txtTitle.Watermark = "如：请输入产品批次号";
            // 
            // lblPrompt
            // 
            lblPrompt.BackColor = Color.Transparent;
            lblPrompt.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblPrompt.ForeColor = Color.FromArgb(48, 48, 48);
            lblPrompt.Location = new Point(10, 54);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(80, 25);
            lblPrompt.TabIndex = 2;
            lblPrompt.Text = "提示说明:";
            lblPrompt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPrompt
            // 
            txtPrompt.Font = new Font("微软雅黑", 10F);
            txtPrompt.Location = new Point(96, 52);
            txtPrompt.Margin = new Padding(4, 5, 4, 5);
            txtPrompt.MinimumSize = new Size(1, 16);
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Padding = new Padding(5);
            txtPrompt.ShowText = false;
            txtPrompt.Size = new Size(400, 30);
            txtPrompt.TabIndex = 3;
            txtPrompt.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtPrompt, "显示在弹窗中的说明文字");
            txtPrompt.Watermark = "向操作员说明需要填写什么值";
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(65, 100, 204);
            uiLine1.LineColor = Color.FromArgb(65, 100, 204);
            uiLine1.Location = new Point(10, 92);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(490, 14);
            uiLine1.TabIndex = 4;
            uiLine1.Text = "输入配置";
            // 
            // lblInputType
            // 
            lblInputType.BackColor = Color.Transparent;
            lblInputType.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblInputType.ForeColor = Color.FromArgb(48, 48, 48);
            lblInputType.Location = new Point(10, 116);
            lblInputType.Name = "lblInputType";
            lblInputType.Size = new Size(80, 25);
            lblInputType.TabIndex = 5;
            lblInputType.Text = "输入类型:";
            lblInputType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbInputType
            // 
            cmbInputType.DataSource = null;
            cmbInputType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbInputType.FillColor = Color.White;
            cmbInputType.Font = new Font("微软雅黑", 10F);
            cmbInputType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbInputType.Items.AddRange(new object[] { "文本输入", "数值输入", "下拉选择" });
            cmbInputType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbInputType.Location = new Point(96, 114);
            cmbInputType.Margin = new Padding(4, 5, 4, 5);
            cmbInputType.MinimumSize = new Size(63, 0);
            cmbInputType.Name = "cmbInputType";
            cmbInputType.Padding = new Padding(0, 0, 30, 2);
            cmbInputType.Size = new Size(160, 30);
            cmbInputType.SymbolSize = 24;
            cmbInputType.TabIndex = 6;
            cmbInputType.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbInputType, "选择操作员填写的数据类型");
            cmbInputType.Watermark = "";
            cmbInputType.SelectedIndexChanged += CmbInputType_SelectedIndexChanged;
            // 
            // lblTargetVar
            // 
            lblTargetVar.BackColor = Color.Transparent;
            lblTargetVar.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblTargetVar.ForeColor = Color.FromArgb(48, 48, 48);
            lblTargetVar.Location = new Point(10, 156);
            lblTargetVar.Name = "lblTargetVar";
            lblTargetVar.Size = new Size(80, 25);
            lblTargetVar.TabIndex = 7;
            lblTargetVar.Text = "存入变量:";
            lblTargetVar.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTargetVar
            // 
            txtTargetVar.Font = new Font("微软雅黑", 10F);
            txtTargetVar.Location = new Point(96, 154);
            txtTargetVar.Margin = new Padding(4, 5, 4, 5);
            txtTargetVar.MinimumSize = new Size(1, 16);
            txtTargetVar.Name = "txtTargetVar";
            txtTargetVar.Padding = new Padding(5);
            txtTargetVar.ShowText = false;
            txtTargetVar.Size = new Size(400, 30);
            txtTargetVar.TabIndex = 8;
            txtTargetVar.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtTargetVar, "操作员填写的值将保存到这个变量");
            txtTargetVar.Watermark = "点击选择目标变量 (按F2打开面板)";
            // 
            // lblDefault
            // 
            lblDefault.BackColor = Color.Transparent;
            lblDefault.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblDefault.ForeColor = Color.FromArgb(48, 48, 48);
            lblDefault.Location = new Point(10, 196);
            lblDefault.Name = "lblDefault";
            lblDefault.Size = new Size(80, 25);
            lblDefault.TabIndex = 9;
            lblDefault.Text = "默认值:";
            lblDefault.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDefaultValue
            // 
            txtDefaultValue.Font = new Font("微软雅黑", 10F);
            txtDefaultValue.Location = new Point(96, 194);
            txtDefaultValue.Margin = new Padding(4, 5, 4, 5);
            txtDefaultValue.MinimumSize = new Size(1, 16);
            txtDefaultValue.Name = "txtDefaultValue";
            txtDefaultValue.Padding = new Padding(5);
            txtDefaultValue.ShowText = false;
            txtDefaultValue.Size = new Size(400, 30);
            txtDefaultValue.TabIndex = 10;
            txtDefaultValue.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtDefaultValue, "弹窗打开时输入框的初始值");
            txtDefaultValue.Watermark = "可选，支持 {变量名} 引用";
            // 
            // lblOptions
            // 
            lblOptions.BackColor = Color.Transparent;
            lblOptions.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblOptions.ForeColor = Color.FromArgb(48, 48, 48);
            lblOptions.Location = new Point(10, 236);
            lblOptions.Name = "lblOptions";
            lblOptions.Size = new Size(80, 25);
            lblOptions.TabIndex = 11;
            lblOptions.Text = "选项列表:";
            lblOptions.TextAlign = ContentAlignment.MiddleLeft;
            lblOptions.Visible = false;
            // 
            // txtSelectOptions
            // 
            txtSelectOptions.Font = new Font("微软雅黑", 10F);
            txtSelectOptions.Location = new Point(96, 234);
            txtSelectOptions.Margin = new Padding(4, 5, 4, 5);
            txtSelectOptions.MinimumSize = new Size(1, 16);
            txtSelectOptions.Name = "txtSelectOptions";
            txtSelectOptions.Padding = new Padding(5);
            txtSelectOptions.ShowText = false;
            txtSelectOptions.Size = new Size(400, 30);
            txtSelectOptions.TabIndex = 12;
            txtSelectOptions.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtSelectOptions, "下拉列表的选项，用英文分号 ; 分隔");
            txtSelectOptions.Visible = false;
            txtSelectOptions.Watermark = "选项用分号分隔，如：合格;不合格;待判断";
            // 
            // lblNumRange
            // 
            lblNumRange.BackColor = Color.Transparent;
            lblNumRange.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblNumRange.ForeColor = Color.FromArgb(48, 48, 48);
            lblNumRange.Location = new Point(10, 236);
            lblNumRange.Name = "lblNumRange";
            lblNumRange.Size = new Size(80, 25);
            lblNumRange.TabIndex = 13;
            lblNumRange.Text = "数值范围:";
            lblNumRange.TextAlign = ContentAlignment.MiddleLeft;
            lblNumRange.Visible = false;
            // 
            // txtMinValue
            // 
            txtMinValue.Font = new Font("微软雅黑", 10F);
            txtMinValue.Location = new Point(96, 234);
            txtMinValue.Margin = new Padding(4, 5, 4, 5);
            txtMinValue.MinimumSize = new Size(1, 16);
            txtMinValue.Name = "txtMinValue";
            txtMinValue.Padding = new Padding(5);
            txtMinValue.ShowText = false;
            txtMinValue.Size = new Size(120, 30);
            txtMinValue.TabIndex = 14;
            txtMinValue.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtMinValue, "数值最小值（留空表示不限制）");
            txtMinValue.Visible = false;
            txtMinValue.Watermark = "最小值";
            // 
            // lblRangeSep
            // 
            lblRangeSep.BackColor = Color.Transparent;
            lblRangeSep.Font = new Font("微软雅黑", 12F);
            lblRangeSep.ForeColor = Color.FromArgb(80, 80, 80);
            lblRangeSep.Location = new Point(222, 234);
            lblRangeSep.Name = "lblRangeSep";
            lblRangeSep.Size = new Size(20, 30);
            lblRangeSep.TabIndex = 15;
            lblRangeSep.Text = "~";
            lblRangeSep.TextAlign = ContentAlignment.MiddleCenter;
            lblRangeSep.Visible = false;
            // 
            // txtMaxValue
            // 
            txtMaxValue.Font = new Font("微软雅黑", 10F);
            txtMaxValue.Location = new Point(248, 234);
            txtMaxValue.Margin = new Padding(4, 5, 4, 5);
            txtMaxValue.MinimumSize = new Size(1, 16);
            txtMaxValue.Name = "txtMaxValue";
            txtMaxValue.Padding = new Padding(5);
            txtMaxValue.ShowText = false;
            txtMaxValue.Size = new Size(120, 30);
            txtMaxValue.TabIndex = 16;
            txtMaxValue.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtMaxValue, "数值最大值（留空表示不限制）");
            txtMaxValue.Visible = false;
            txtMaxValue.Watermark = "最大值";
            // 
            // lblDecimal
            // 
            lblDecimal.BackColor = Color.Transparent;
            lblDecimal.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblDecimal.ForeColor = Color.FromArgb(48, 48, 48);
            lblDecimal.Location = new Point(10, 276);
            lblDecimal.Name = "lblDecimal";
            lblDecimal.Size = new Size(80, 25);
            lblDecimal.TabIndex = 17;
            lblDecimal.Text = "小数位数:";
            lblDecimal.TextAlign = ContentAlignment.MiddleLeft;
            lblDecimal.Visible = false;
            // 
            // nudDecimalPlaces
            // 
            nudDecimalPlaces.Font = new Font("微软雅黑", 10F);
            nudDecimalPlaces.Location = new Point(96, 274);
            nudDecimalPlaces.Margin = new Padding(4, 5, 4, 5);
            nudDecimalPlaces.Maximum = 6D;
            nudDecimalPlaces.Minimum = 0D;
            nudDecimalPlaces.MinimumSize = new Size(1, 16);
            nudDecimalPlaces.Name = "nudDecimalPlaces";
            nudDecimalPlaces.Padding = new Padding(5);
            nudDecimalPlaces.ShowText = false;
            nudDecimalPlaces.Size = new Size(100, 30);
            nudDecimalPlaces.TabIndex = 18;
            nudDecimalPlaces.Text = "2";
            nudDecimalPlaces.TextAlignment = ContentAlignment.MiddleCenter;
            toolTip.SetToolTip(nudDecimalPlaces, "保留小数位数，0 表示整数");
            nudDecimalPlaces.Value = 2;
            nudDecimalPlaces.Visible = false;
            // 
            // chkAllowEmpty
            // 
            chkAllowEmpty.BackColor = Color.Transparent;
            chkAllowEmpty.Font = new Font("微软雅黑", 10F);
            chkAllowEmpty.ForeColor = Color.FromArgb(80, 80, 80);
            chkAllowEmpty.Location = new Point(96, 316);
            chkAllowEmpty.MinimumSize = new Size(1, 1);
            chkAllowEmpty.Name = "chkAllowEmpty";
            chkAllowEmpty.Size = new Size(240, 28);
            chkAllowEmpty.TabIndex = 19;
            chkAllowEmpty.Text = "允许空值（不填也可以确认）";
            toolTip.SetToolTip(chkAllowEmpty, "勾选后，操作员可以不填直接点击确认");
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(65, 100, 204);
            uiLine2.LineColor = Color.FromArgb(65, 100, 204);
            uiLine2.Location = new Point(10, 354);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(490, 14);
            uiLine2.TabIndex = 20;
            uiLine2.Text = "超时配置";
            // 
            // lblTimeout
            // 
            lblTimeout.BackColor = Color.Transparent;
            lblTimeout.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeout.Location = new Point(10, 378);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(80, 25);
            lblTimeout.TabIndex = 21;
            lblTimeout.Text = "超时时间:";
            lblTimeout.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudTimeout
            // 
            nudTimeout.Font = new Font("微软雅黑", 10F);
            nudTimeout.Location = new Point(96, 376);
            nudTimeout.Margin = new Padding(4, 5, 4, 5);
            nudTimeout.Maximum = 3600D;
            nudTimeout.Minimum = 0D;
            nudTimeout.MinimumSize = new Size(1, 16);
            nudTimeout.Name = "nudTimeout";
            nudTimeout.Padding = new Padding(5);
            nudTimeout.ShowText = false;
            nudTimeout.Size = new Size(100, 30);
            nudTimeout.TabIndex = 22;
            nudTimeout.Text = "0";
            nudTimeout.TextAlignment = ContentAlignment.MiddleCenter;
            toolTip.SetToolTip(nudTimeout, "等待操作员输入的最长秒数，0 = 无限等待");
            // 
            // lblTimeoutUnit
            // 
            lblTimeoutUnit.BackColor = Color.Transparent;
            lblTimeoutUnit.Font = new Font("微软雅黑", 10F);
            lblTimeoutUnit.ForeColor = Color.FromArgb(120, 120, 120);
            lblTimeoutUnit.Location = new Point(202, 380);
            lblTimeoutUnit.Name = "lblTimeoutUnit";
            lblTimeoutUnit.Size = new Size(140, 22);
            lblTimeoutUnit.TabIndex = 23;
            lblTimeoutUnit.Text = "秒（0 = 无限等待）";
            lblTimeoutUnit.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOnTimeout
            // 
            lblOnTimeout.BackColor = Color.Transparent;
            lblOnTimeout.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblOnTimeout.ForeColor = Color.FromArgb(48, 48, 48);
            lblOnTimeout.Location = new Point(10, 418);
            lblOnTimeout.Name = "lblOnTimeout";
            lblOnTimeout.Size = new Size(80, 25);
            lblOnTimeout.TabIndex = 24;
            lblOnTimeout.Text = "超时动作:";
            lblOnTimeout.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbOnTimeout
            // 
            cmbOnTimeout.DataSource = null;
            cmbOnTimeout.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbOnTimeout.FillColor = Color.White;
            cmbOnTimeout.Font = new Font("微软雅黑", 10F);
            cmbOnTimeout.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbOnTimeout.Items.AddRange(new object[] { "停止流程", "使用默认值继续", "跳过此步骤" });
            cmbOnTimeout.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbOnTimeout.Location = new Point(96, 416);
            cmbOnTimeout.Margin = new Padding(4, 5, 4, 5);
            cmbOnTimeout.MinimumSize = new Size(63, 0);
            cmbOnTimeout.Name = "cmbOnTimeout";
            cmbOnTimeout.Padding = new Padding(0, 0, 30, 2);
            cmbOnTimeout.Size = new Size(200, 30);
            cmbOnTimeout.SymbolSize = 24;
            cmbOnTimeout.TabIndex = 25;
            cmbOnTimeout.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbOnTimeout, "超时后的处理方式");
            cmbOnTimeout.Watermark = "";
            cmbOnTimeout.SelectedIndexChanged += CmbOnTimeout_SelectedIndexChanged;
            // 
            // lblTimeoutDef
            // 
            lblTimeoutDef.BackColor = Color.Transparent;
            lblTimeoutDef.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblTimeoutDef.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeoutDef.Location = new Point(10, 458);
            lblTimeoutDef.Name = "lblTimeoutDef";
            lblTimeoutDef.Size = new Size(80, 25);
            lblTimeoutDef.TabIndex = 26;
            lblTimeoutDef.Text = "超时使用值:";
            lblTimeoutDef.TextAlign = ContentAlignment.MiddleLeft;
            lblTimeoutDef.Visible = false;
            // 
            // txtTimeoutDefault
            // 
            txtTimeoutDefault.Font = new Font("微软雅黑", 10F);
            txtTimeoutDefault.Location = new Point(96, 456);
            txtTimeoutDefault.Margin = new Padding(4, 5, 4, 5);
            txtTimeoutDefault.MinimumSize = new Size(1, 16);
            txtTimeoutDefault.Name = "txtTimeoutDefault";
            txtTimeoutDefault.Padding = new Padding(5);
            txtTimeoutDefault.ShowText = false;
            txtTimeoutDefault.Size = new Size(400, 30);
            txtTimeoutDefault.TabIndex = 27;
            txtTimeoutDefault.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtTimeoutDefault, "仅「使用默认值继续」时生效");
            txtTimeoutDefault.Visible = false;
            txtTimeoutDefault.Watermark = "超时时写入目标变量的值";
            // 
            // lblDescription
            // 
            lblDescription.BackColor = Color.Transparent;
            lblDescription.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(10, 418);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(80, 25);
            lblDescription.TabIndex = 28;
            lblDescription.Text = "步骤描述:";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("微软雅黑", 10F);
            txtDescription.Location = new Point(96, 416);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(400, 30);
            txtDescription.TabIndex = 29;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtDescription, "步骤描述，不影响执行逻辑");
            txtDescription.Watermark = "可选，在流程列表中显示的备注说明";
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.FillColor = Color.FromArgb(245, 247, 250);
            pnlBottom.FillColor2 = Color.FromArgb(245, 247, 250);
            pnlBottom.Font = new Font("微软雅黑", 10F);
            pnlBottom.Location = new Point(15, 595);
            pnlBottom.Margin = new Padding(4, 5, 4, 5);
            pnlBottom.MinimumSize = new Size(1, 1);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(10);
            pnlBottom.Size = new Size(510, 60);
            pnlBottom.TabIndex = 2;
            pnlBottom.Text = null;
            pnlBottom.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(248, 248, 248);
            btnCancel.FillColor2 = Color.FromArgb(248, 248, 248);
            btnCancel.FillHoverColor = Color.FromArgb(230, 230, 230);
            btnCancel.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(80, 80, 80);
            btnCancel.Location = new Point(392, 10);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 6;
            btnCancel.RectColor = Color.FromArgb(200, 200, 200);
            btnCancel.Size = new Size(110, 40);
            btnCancel.Symbol = 61453;
            btnCancel.SymbolSize = 20;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取  消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.None;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.FromArgb(65, 100, 204);
            btnSave.FillColor2 = Color.FromArgb(65, 100, 204);
            btnSave.FillHoverColor = Color.FromArgb(80, 120, 220);
            btnSave.FillPressColor = Color.FromArgb(50, 80, 180);
            btnSave.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            btnSave.Location = new Point(272, 10);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.Radius = 6;
            btnSave.RectColor = Color.FromArgb(65, 100, 204);
            btnSave.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnSave.Size = new Size(110, 40);
            btnSave.Symbol = 61639;
            btnSave.SymbolSize = 20;
            btnSave.TabIndex = 0;
            btnSave.Text = "确  定";
            btnSave.TipsFont = new Font("微软雅黑", 9F);
            btnSave.Click += BtnSave_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblHeaderDesc);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.FillColor = Color.FromArgb(230, 244, 255);
            pnlHeader.FillColor2 = Color.FromArgb(230, 244, 255);
            pnlHeader.Font = new Font("微软雅黑", 10F);
            pnlHeader.Location = new Point(15, 15);
            pnlHeader.Margin = new Padding(4, 5, 4, 5);
            pnlHeader.MinimumSize = new Size(1, 1);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(12);
            pnlHeader.Radius = 8;
            pnlHeader.RectColor = Color.FromArgb(65, 100, 204);
            pnlHeader.Size = new Size(510, 44);
            pnlHeader.TabIndex = 0;
            pnlHeader.Text = null;
            pnlHeader.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblHeaderDesc
            // 
            lblHeaderDesc.BackColor = Color.Transparent;
            lblHeaderDesc.Dock = DockStyle.Fill;
            lblHeaderDesc.Font = new Font("微软雅黑", 9F);
            lblHeaderDesc.ForeColor = Color.FromArgb(48, 48, 48);
            lblHeaderDesc.Location = new Point(12, 12);
            lblHeaderDesc.Name = "lblHeaderDesc";
            lblHeaderDesc.Size = new Size(486, 20);
            lblHeaderDesc.TabIndex = 0;
            lblHeaderDesc.Text = "运行时暂停流程，弹窗让操作员填值，结果存入指定变量后继续执行";
            lblHeaderDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_UserInput
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(540, 705);
            ControlBox = false;
            ControlBoxFillHoverColor = Color.FromArgb(163, 163, 163);
            Controls.Add(pnlMain);
            Font = new Font("微软雅黑", 10F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_UserInput";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            Text = "用户输入 - 参数配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 540, 610);
            pnlMain.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region 控件声明

        private Sunny.UI.UIPanel pnlMain;
        private Sunny.UI.UIPanel pnlHeader;
        private Sunny.UI.UILabel lblHeaderDesc;
        private Sunny.UI.UIPanel pnlContent;
        private Sunny.UI.UILabel lblTitle;
        private Sunny.UI.UITextBox txtTitle;
        private Sunny.UI.UILabel lblPrompt;
        private Sunny.UI.UITextBox txtPrompt;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UILabel lblInputType;
        private Sunny.UI.UIComboBox cmbInputType;
        private Sunny.UI.UILabel lblTargetVar;
        private Sunny.UI.UITextBox txtTargetVar;
        private Sunny.UI.UILabel lblDefault;
        private Sunny.UI.UITextBox txtDefaultValue;
        private Sunny.UI.UILabel lblOptions;
        private Sunny.UI.UITextBox txtSelectOptions;
        private Sunny.UI.UILabel lblNumRange;
        private Sunny.UI.UITextBox txtMinValue;
        private Sunny.UI.UILabel lblRangeSep;
        private Sunny.UI.UITextBox txtMaxValue;
        private Sunny.UI.UILabel lblDecimal;
        private Sunny.UI.UIIntegerUpDown nudDecimalPlaces;
        private Sunny.UI.UICheckBox chkAllowEmpty;
        private Sunny.UI.UILine uiLine2;
        private Sunny.UI.UILabel lblTimeout;
        private Sunny.UI.UIIntegerUpDown nudTimeout;
        private Sunny.UI.UILabel lblTimeoutUnit;
        private Sunny.UI.UILabel lblOnTimeout;
        private Sunny.UI.UIComboBox cmbOnTimeout;
        private Sunny.UI.UILabel lblTimeoutDef;
        private Sunny.UI.UITextBox txtTimeoutDefault;
        private Sunny.UI.UILabel lblDescription;
        private Sunny.UI.UITextBox txtDescription;
        private Sunny.UI.UIPanel pnlBottom;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UISymbolButton btnCancel;
        private ToolTip toolTip;

        #endregion
    }
}

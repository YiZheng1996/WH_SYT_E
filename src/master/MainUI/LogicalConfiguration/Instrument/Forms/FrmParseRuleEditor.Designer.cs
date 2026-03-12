using Sunny.UI;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    partial class FrmParseRuleEditor
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
            lblTargetVariable = new UILabel();
            txtTargetVariable = new UITextBox();
            lblParseType = new UILabel();
            cboParseType = new UIComboBox();
            lblTargetDataType = new UILabel();
            cboTargetDataType = new UIComboBox();
            grpParseParams = new UIGroupBox();
            panelJson = new Panel();
            lblJsonPath = new UILabel();
            txtJsonPath = new UITextBox();
            lblJsonHint = new UILabel();
            panelDelimiter = new Panel();
            numSegmentIndex = new UIIntegerUpDown();
            lblDelimiter = new UILabel();
            txtDelimiter = new UITextBox();
            lblSegmentIndex = new UILabel();
            lblDelimiterHint = new UILabel();
            panelRegex = new Panel();
            lblRegexTemplate = new UILabel();
            cboRegexTemplate = new UIComboBox();
            lblAdvancedHint = new UILabel();
            txtRegexPattern = new UITextBox();
            lblRegexPattern = new UILabel();
            btnTestRegex = new UIButton();
            lblRegexGroup = new UILabel();
            numRegexGroup = new UIIntegerUpDown();
            lblRegexHint = new UILabel();
            panelPosition = new Panel();
            lblStartPos = new UILabel();
            numStartPosition = new UIIntegerUpDown();
            lblLength = new UILabel();
            numLength = new UIIntegerUpDown();
            lblPositionHint = new UILabel();
            grpConvert = new UIGroupBox();
            lblScaleFactor = new UILabel();
            numScaleFactor = new UIDoubleUpDown();
            lblOffset = new UILabel();
            numOffset = new UIDoubleUpDown();
            lblConvertHint = new UILabel();
            panelBottom = new Panel();
            chkAdvancedMode = new UICheckBox();
            btnCancel = new UIButton();
            btnOk = new UIButton();
            panelMain.SuspendLayout();
            grpBasic.SuspendLayout();
            grpParseParams.SuspendLayout();
            panelJson.SuspendLayout();
            panelDelimiter.SuspendLayout();
            panelRegex.SuspendLayout();
            panelPosition.SuspendLayout();
            grpConvert.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.BackColor = Color.FromArgb(248, 249, 250);
            panelMain.Controls.Add(grpBasic);
            panelMain.Controls.Add(grpParseParams);
            panelMain.Controls.Add(grpConvert);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(12, 12, 12, 4);
            panelMain.Size = new Size(588, 499);
            panelMain.TabIndex = 0;
            // 
            // grpBasic
            // 
            grpBasic.Controls.Add(lblName);
            grpBasic.Controls.Add(txtName);
            grpBasic.Controls.Add(lblTargetVariable);
            grpBasic.Controls.Add(txtTargetVariable);
            grpBasic.Controls.Add(lblParseType);
            grpBasic.Controls.Add(cboParseType);
            grpBasic.Controls.Add(lblTargetDataType);
            grpBasic.Controls.Add(cboTargetDataType);
            grpBasic.Dock = DockStyle.Top;
            grpBasic.Font = new Font("微软雅黑", 12F);
            grpBasic.Location = new Point(12, 120);
            grpBasic.Margin = new Padding(4, 5, 4, 8);
            grpBasic.MinimumSize = new Size(1, 1);
            grpBasic.Name = "grpBasic";
            grpBasic.Padding = new Padding(10, 32, 10, 10);
            grpBasic.Size = new Size(564, 178);
            grpBasic.TabIndex = 0;
            grpBasic.Text = "基本信息";
            grpBasic.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("微软雅黑", 11F);
            lblName.ForeColor = Color.FromArgb(48, 48, 48);
            lblName.Location = new Point(4, 42);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 20);
            lblName.TabIndex = 0;
            lblName.Text = "规则名称*:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Font = new Font("微软雅黑", 12F);
            txtName.Location = new Point(94, 38);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.MinimumSize = new Size(1, 16);
            txtName.Name = "txtName";
            txtName.Padding = new Padding(5);
            txtName.ShowText = false;
            txtName.Size = new Size(231, 30);
            txtName.TabIndex = 1;
            txtName.TextAlignment = ContentAlignment.MiddleLeft;
            txtName.Watermark = "规则标识，如 Temperature";
            // 
            // lblTargetVariable
            // 
            lblTargetVariable.AutoSize = true;
            lblTargetVariable.Font = new Font("微软雅黑", 11F);
            lblTargetVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblTargetVariable.Location = new Point(4, 84);
            lblTargetVariable.Name = "lblTargetVariable";
            lblTargetVariable.Size = new Size(80, 20);
            lblTargetVariable.TabIndex = 2;
            lblTargetVariable.Text = "目标变量*:";
            lblTargetVariable.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTargetVariable
            // 
            txtTargetVariable.Font = new Font("微软雅黑", 12F);
            txtTargetVariable.Location = new Point(94, 80);
            txtTargetVariable.Margin = new Padding(4, 5, 4, 5);
            txtTargetVariable.MinimumSize = new Size(1, 16);
            txtTargetVariable.Name = "txtTargetVariable";
            txtTargetVariable.Padding = new Padding(5);
            txtTargetVariable.ShowText = false;
            txtTargetVariable.Size = new Size(420, 30);
            txtTargetVariable.TabIndex = 3;
            txtTargetVariable.TextAlignment = ContentAlignment.MiddleLeft;
            txtTargetVariable.Watermark = "解析结果写入的变量名，如 CurrentTemperature";
            // 
            // lblParseType
            // 
            lblParseType.AutoSize = true;
            lblParseType.Font = new Font("微软雅黑", 11F);
            lblParseType.ForeColor = Color.FromArgb(48, 48, 48);
            lblParseType.Location = new Point(4, 126);
            lblParseType.Name = "lblParseType";
            lblParseType.Size = new Size(80, 20);
            lblParseType.TabIndex = 4;
            lblParseType.Text = "解析方式*:";
            lblParseType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboParseType
            // 
            cboParseType.DataSource = null;
            cboParseType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboParseType.FillColor = Color.White;
            cboParseType.Font = new Font("微软雅黑", 12F);
            cboParseType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboParseType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboParseType.Location = new Point(94, 122);
            cboParseType.Margin = new Padding(4, 5, 4, 5);
            cboParseType.MinimumSize = new Size(63, 0);
            cboParseType.Name = "cboParseType";
            cboParseType.Padding = new Padding(0, 0, 30, 2);
            cboParseType.Size = new Size(420, 30);
            cboParseType.SymbolSize = 24;
            cboParseType.TabIndex = 5;
            cboParseType.TextAlignment = ContentAlignment.MiddleLeft;
            cboParseType.Watermark = "请选择解析方式";
            // 
            // lblTargetDataType
            // 
            lblTargetDataType.AutoSize = true;
            lblTargetDataType.Font = new Font("微软雅黑", 11F);
            lblTargetDataType.ForeColor = Color.FromArgb(48, 48, 48);
            lblTargetDataType.Location = new Point(332, 43);
            lblTargetDataType.Name = "lblTargetDataType";
            lblTargetDataType.Size = new Size(73, 20);
            lblTargetDataType.TabIndex = 6;
            lblTargetDataType.Text = "结果类型:";
            lblTargetDataType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboTargetDataType
            // 
            cboTargetDataType.DataSource = null;
            cboTargetDataType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboTargetDataType.FillColor = Color.White;
            cboTargetDataType.Font = new Font("微软雅黑", 12F);
            cboTargetDataType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboTargetDataType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboTargetDataType.Location = new Point(410, 39);
            cboTargetDataType.Margin = new Padding(4, 5, 4, 5);
            cboTargetDataType.MinimumSize = new Size(63, 0);
            cboTargetDataType.Name = "cboTargetDataType";
            cboTargetDataType.Padding = new Padding(0, 0, 30, 2);
            cboTargetDataType.Size = new Size(104, 30);
            cboTargetDataType.SymbolSize = 24;
            cboTargetDataType.TabIndex = 7;
            cboTargetDataType.TextAlignment = ContentAlignment.MiddleLeft;
            cboTargetDataType.Watermark = "";
            // 
            // grpParseParams
            // 
            grpParseParams.Controls.Add(panelRegex);
            grpParseParams.Controls.Add(panelJson);
            grpParseParams.Controls.Add(panelDelimiter);
            grpParseParams.Controls.Add(panelPosition);
            grpParseParams.Dock = DockStyle.Bottom;
            grpParseParams.Font = new Font("微软雅黑", 12F);
            grpParseParams.Location = new Point(12, 296);
            grpParseParams.Margin = new Padding(4, 8, 4, 8);
            grpParseParams.MinimumSize = new Size(1, 1);
            grpParseParams.Name = "grpParseParams";
            grpParseParams.Padding = new Padding(10, 32, 10, 10);
            grpParseParams.Size = new Size(564, 199);
            grpParseParams.TabIndex = 1;
            grpParseParams.Text = "解析参数配置";
            grpParseParams.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // panelJson
            // 
            panelJson.Controls.Add(lblJsonPath);
            panelJson.Controls.Add(txtJsonPath);
            panelJson.Controls.Add(lblJsonHint);
            panelJson.Location = new Point(10, 36);
            panelJson.Name = "panelJson";
            panelJson.Size = new Size(541, 96);
            panelJson.TabIndex = 3;
            panelJson.Visible = false;
            // 
            // lblJsonPath
            // 
            lblJsonPath.AutoSize = true;
            lblJsonPath.Font = new Font("微软雅黑", 11F);
            lblJsonPath.ForeColor = Color.FromArgb(48, 48, 48);
            lblJsonPath.Location = new Point(5, 6);
            lblJsonPath.Name = "lblJsonPath";
            lblJsonPath.Size = new Size(82, 20);
            lblJsonPath.TabIndex = 0;
            lblJsonPath.Text = "JSON路径:";
            lblJsonPath.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtJsonPath
            // 
            txtJsonPath.Font = new Font("微软雅黑", 12F);
            txtJsonPath.Location = new Point(93, 2);
            txtJsonPath.Margin = new Padding(4, 5, 4, 5);
            txtJsonPath.MinimumSize = new Size(1, 16);
            txtJsonPath.Name = "txtJsonPath";
            txtJsonPath.Padding = new Padding(5);
            txtJsonPath.ShowText = false;
            txtJsonPath.Size = new Size(380, 30);
            txtJsonPath.TabIndex = 0;
            txtJsonPath.TextAlignment = ContentAlignment.MiddleLeft;
            txtJsonPath.Watermark = "如 data.temp";
            // 
            // lblJsonHint
            // 
            lblJsonHint.Font = new Font("微软雅黑", 10F);
            lblJsonHint.ForeColor = Color.FromArgb(100, 100, 200);
            lblJsonHint.Location = new Point(4, 44);
            lblJsonHint.Name = "lblJsonHint";
            lblJsonHint.Size = new Size(468, 44);
            lblJsonHint.TabIndex = 1;
            lblJsonHint.Text = "   提示：用\"点\"分隔层级路径。\r\n   示例：响应={\"data\":{\"temp\":25.6}}，路径=\"data.temp\" → 25.6";
            lblJsonHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelDelimiter
            // 
            panelDelimiter.Controls.Add(numSegmentIndex);
            panelDelimiter.Controls.Add(lblDelimiter);
            panelDelimiter.Controls.Add(txtDelimiter);
            panelDelimiter.Controls.Add(lblSegmentIndex);
            panelDelimiter.Controls.Add(lblDelimiterHint);
            panelDelimiter.Location = new Point(10, 36);
            panelDelimiter.Name = "panelDelimiter";
            panelDelimiter.Size = new Size(541, 96);
            panelDelimiter.TabIndex = 1;
            panelDelimiter.Visible = false;
            // 
            // numSegmentIndex
            // 
            numSegmentIndex.Font = new Font("微软雅黑", 12F);
            numSegmentIndex.Location = new Point(381, 2);
            numSegmentIndex.Margin = new Padding(4, 5, 4, 5);
            numSegmentIndex.Maximum = 999D;
            numSegmentIndex.Minimum = 0D;
            numSegmentIndex.MinimumSize = new Size(1, 16);
            numSegmentIndex.Name = "numSegmentIndex";
            numSegmentIndex.Padding = new Padding(5);
            numSegmentIndex.ShowText = false;
            numSegmentIndex.Size = new Size(121, 30);
            numSegmentIndex.TabIndex = 1;
            numSegmentIndex.Text = "0";
            numSegmentIndex.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblDelimiter
            // 
            lblDelimiter.AutoSize = true;
            lblDelimiter.Font = new Font("微软雅黑", 11F);
            lblDelimiter.ForeColor = Color.FromArgb(48, 48, 48);
            lblDelimiter.Location = new Point(9, 6);
            lblDelimiter.Name = "lblDelimiter";
            lblDelimiter.Size = new Size(58, 20);
            lblDelimiter.TabIndex = 0;
            lblDelimiter.Text = "分隔符:";
            lblDelimiter.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDelimiter
            // 
            txtDelimiter.Font = new Font("微软雅黑", 12F);
            txtDelimiter.Location = new Point(75, 2);
            txtDelimiter.Margin = new Padding(4, 5, 4, 5);
            txtDelimiter.MinimumSize = new Size(1, 16);
            txtDelimiter.Name = "txtDelimiter";
            txtDelimiter.Padding = new Padding(5);
            txtDelimiter.ShowText = false;
            txtDelimiter.Size = new Size(176, 30);
            txtDelimiter.TabIndex = 0;
            txtDelimiter.TextAlignment = ContentAlignment.MiddleLeft;
            txtDelimiter.Watermark = "如 ,";
            // 
            // lblSegmentIndex
            // 
            lblSegmentIndex.AutoSize = true;
            lblSegmentIndex.Font = new Font("微软雅黑", 11F);
            lblSegmentIndex.ForeColor = Color.FromArgb(48, 48, 48);
            lblSegmentIndex.Location = new Point(258, 6);
            lblSegmentIndex.Name = "lblSegmentIndex";
            lblSegmentIndex.Size = new Size(122, 20);
            lblSegmentIndex.TabIndex = 1;
            lblSegmentIndex.Text = "取第几段(从0起):";
            lblSegmentIndex.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDelimiterHint
            // 
            lblDelimiterHint.Font = new Font("微软雅黑", 10F);
            lblDelimiterHint.ForeColor = Color.FromArgb(100, 100, 200);
            lblDelimiterHint.Location = new Point(4, 44);
            lblDelimiterHint.Name = "lblDelimiterHint";
            lblDelimiterHint.Size = new Size(468, 44);
            lblDelimiterHint.TabIndex = 2;
            lblDelimiterHint.Text = "   提示：用分隔符拆分响应，取指定序号的片段。\r\n   示例：响应=\"10.5,20.3,30.1\"，分隔符=\",\"，取第0段 → \"10.5\"";
            lblDelimiterHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelRegex
            // 
            panelRegex.Controls.Add(lblRegexTemplate);
            panelRegex.Controls.Add(cboRegexTemplate);
            panelRegex.Controls.Add(txtRegexPattern);
            panelRegex.Controls.Add(lblRegexPattern);
            panelRegex.Controls.Add(lblAdvancedHint);
            panelRegex.Controls.Add(btnTestRegex);
            panelRegex.Controls.Add(lblRegexGroup);
            panelRegex.Controls.Add(numRegexGroup);
            panelRegex.Controls.Add(lblRegexHint);
            panelRegex.Location = new Point(10, 30);
            panelRegex.Name = "panelRegex";
            panelRegex.Size = new Size(541, 163);
            panelRegex.TabIndex = 2;
            panelRegex.Visible = false;
            // 
            // lblRegexTemplate
            // 
            lblRegexTemplate.AutoSize = true;
            lblRegexTemplate.Font = new Font("微软雅黑", 11F);
            lblRegexTemplate.ForeColor = Color.FromArgb(48, 48, 48);
            lblRegexTemplate.Location = new Point(19, 35);
            lblRegexTemplate.Name = "lblRegexTemplate";
            lblRegexTemplate.Size = new Size(73, 20);
            lblRegexTemplate.TabIndex = 0;
            lblRegexTemplate.Text = "常用模板:";
            // 
            // cboRegexTemplate
            // 
            cboRegexTemplate.DataSource = null;
            cboRegexTemplate.DropDownStyle = UIDropDownStyle.DropDownList;
            cboRegexTemplate.FillColor = Color.White;
            cboRegexTemplate.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboRegexTemplate.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboRegexTemplate.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboRegexTemplate.Location = new Point(92, 31);
            cboRegexTemplate.Margin = new Padding(4, 5, 4, 5);
            cboRegexTemplate.MinimumSize = new Size(63, 0);
            cboRegexTemplate.Name = "cboRegexTemplate";
            cboRegexTemplate.Padding = new Padding(0, 0, 30, 2);
            cboRegexTemplate.Size = new Size(442, 29);
            cboRegexTemplate.SymbolSize = 24;
            cboRegexTemplate.TabIndex = 1;
            cboRegexTemplate.TextAlignment = ContentAlignment.MiddleLeft;
            cboRegexTemplate.Watermark = "";
            // 
            // lblAdvancedHint
            // 
            lblAdvancedHint.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblAdvancedHint.ForeColor = Color.FromArgb(180, 100, 0);
            lblAdvancedHint.Location = new Point(5, 2);
            lblAdvancedHint.Name = "lblAdvancedHint";
            lblAdvancedHint.Size = new Size(420, 20);
            lblAdvancedHint.TabIndex = 2;
            lblAdvancedHint.Text = "⚠ 正则表达式由工程师配置，普通用户请使用常用模板";
            lblAdvancedHint.Visible = false;
            // 
            // txtRegexPattern
            // 
            txtRegexPattern.Font = new Font("微软雅黑", 12F);
            txtRegexPattern.Location = new Point(92, 72);
            txtRegexPattern.Margin = new Padding(4, 5, 4, 5);
            txtRegexPattern.MinimumSize = new Size(1, 16);
            txtRegexPattern.Name = "txtRegexPattern";
            txtRegexPattern.Padding = new Padding(5);
            txtRegexPattern.ShowText = false;
            txtRegexPattern.Size = new Size(200, 30);
            txtRegexPattern.TabIndex = 0;
            txtRegexPattern.TextAlignment = ContentAlignment.MiddleLeft;
            txtRegexPattern.Watermark = "如 ([\\d.]+)";
            // 
            // lblRegexPattern
            // 
            lblRegexPattern.AutoSize = true;
            lblRegexPattern.Font = new Font("微软雅黑", 11F);
            lblRegexPattern.ForeColor = Color.FromArgb(48, 48, 48);
            lblRegexPattern.Location = new Point(4, 76);
            lblRegexPattern.Name = "lblRegexPattern";
            lblRegexPattern.Size = new Size(88, 20);
            lblRegexPattern.TabIndex = 0;
            lblRegexPattern.Text = "正则表达式:";
            lblRegexPattern.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnTestRegex
            // 
            btnTestRegex.FillColor = Color.FromArgb(40, 167, 69);
            btnTestRegex.Font = new Font("微软雅黑", 11F);
            btnTestRegex.Location = new Point(296, 71);
            btnTestRegex.MinimumSize = new Size(1, 1);
            btnTestRegex.Name = "btnTestRegex";
            btnTestRegex.Size = new Size(80, 30);
            btnTestRegex.TabIndex = 1;
            btnTestRegex.Text = "🔍 测试";
            btnTestRegex.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // lblRegexGroup
            // 
            lblRegexGroup.AutoSize = true;
            lblRegexGroup.Font = new Font("微软雅黑", 11F);
            lblRegexGroup.ForeColor = Color.FromArgb(48, 48, 48);
            lblRegexGroup.Location = new Point(376, 76);
            lblRegexGroup.Name = "lblRegexGroup";
            lblRegexGroup.Size = new Size(43, 20);
            lblRegexGroup.TabIndex = 2;
            lblRegexGroup.Text = "分组:";
            lblRegexGroup.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numRegexGroup
            // 
            numRegexGroup.Font = new Font("微软雅黑", 12F);
            numRegexGroup.Location = new Point(423, 72);
            numRegexGroup.Margin = new Padding(4, 5, 4, 5);
            numRegexGroup.Maximum = 99D;
            numRegexGroup.Minimum = 0D;
            numRegexGroup.MinimumSize = new Size(1, 16);
            numRegexGroup.Name = "numRegexGroup";
            numRegexGroup.Padding = new Padding(5);
            numRegexGroup.ShowText = false;
            numRegexGroup.Size = new Size(111, 30);
            numRegexGroup.TabIndex = 2;
            numRegexGroup.Text = "1";
            numRegexGroup.TextAlignment = ContentAlignment.MiddleCenter;
            numRegexGroup.Value = 1;
            // 
            // lblRegexHint
            // 
            lblRegexHint.Font = new Font("微软雅黑", 10F);
            lblRegexHint.ForeColor = Color.FromArgb(100, 100, 200);
            lblRegexHint.Location = new Point(5, 110);
            lblRegexHint.Name = "lblRegexHint";
            lblRegexHint.Size = new Size(468, 44);
            lblRegexHint.TabIndex = 3;
            lblRegexHint.Text = "   提示：分组=0取整体匹配，分组=1取第一个括号内容。\r\n   示例：响应=\"TEMP=25.6\"，表达式=\"TEMP=([\\d.]+)\"，分组=1 → \"25.6\"";
            lblRegexHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelPosition
            // 
            panelPosition.Controls.Add(lblStartPos);
            panelPosition.Controls.Add(numStartPosition);
            panelPosition.Controls.Add(lblLength);
            panelPosition.Controls.Add(numLength);
            panelPosition.Controls.Add(lblPositionHint);
            panelPosition.Location = new Point(10, 36);
            panelPosition.Name = "panelPosition";
            panelPosition.Size = new Size(541, 96);
            panelPosition.TabIndex = 0;
            panelPosition.Visible = false;
            // 
            // lblStartPos
            // 
            lblStartPos.AutoSize = true;
            lblStartPos.Font = new Font("微软雅黑", 11F);
            lblStartPos.ForeColor = Color.FromArgb(48, 48, 48);
            lblStartPos.Location = new Point(5, 6);
            lblStartPos.Name = "lblStartPos";
            lblStartPos.Size = new Size(73, 20);
            lblStartPos.TabIndex = 0;
            lblStartPos.Text = "起始位置:";
            lblStartPos.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numStartPosition
            // 
            numStartPosition.Font = new Font("微软雅黑", 12F);
            numStartPosition.Location = new Point(83, 2);
            numStartPosition.Margin = new Padding(4, 5, 4, 5);
            numStartPosition.Maximum = 9999D;
            numStartPosition.Minimum = 0D;
            numStartPosition.MinimumSize = new Size(1, 16);
            numStartPosition.Name = "numStartPosition";
            numStartPosition.Padding = new Padding(5);
            numStartPosition.ShowText = false;
            numStartPosition.Size = new Size(143, 30);
            numStartPosition.TabIndex = 0;
            numStartPosition.Text = "0";
            numStartPosition.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblLength
            // 
            lblLength.AutoSize = true;
            lblLength.Font = new Font("微软雅黑", 11F);
            lblLength.ForeColor = Color.FromArgb(48, 48, 48);
            lblLength.Location = new Point(233, 6);
            lblLength.Name = "lblLength";
            lblLength.Size = new Size(73, 20);
            lblLength.TabIndex = 1;
            lblLength.Text = "截取长度:";
            lblLength.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numLength
            // 
            numLength.Font = new Font("微软雅黑", 12F);
            numLength.Location = new Point(311, 2);
            numLength.Margin = new Padding(4, 5, 4, 5);
            numLength.Maximum = 9999D;
            numLength.Minimum = -1D;
            numLength.MinimumSize = new Size(1, 16);
            numLength.Name = "numLength";
            numLength.Padding = new Padding(5);
            numLength.ShowText = false;
            numLength.Size = new Size(143, 30);
            numLength.TabIndex = 1;
            numLength.Text = "-1";
            numLength.TextAlignment = ContentAlignment.MiddleCenter;
            numLength.Value = -1;
            // 
            // lblPositionHint
            // 
            lblPositionHint.Font = new Font("微软雅黑", 10F);
            lblPositionHint.ForeColor = Color.FromArgb(100, 100, 200);
            lblPositionHint.Location = new Point(10, 44);
            lblPositionHint.Name = "lblPositionHint";
            lblPositionHint.Size = new Size(468, 44);
            lblPositionHint.TabIndex = 2;
            lblPositionHint.Text = "   提示：从第0位开始计数。长度=-1 表示截取到末尾。\r\n   示例：响应=\"OK25.6END\"，起始=2，长度=4 → \"25.6\"";
            lblPositionHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpConvert
            // 
            grpConvert.Controls.Add(lblScaleFactor);
            grpConvert.Controls.Add(numScaleFactor);
            grpConvert.Controls.Add(lblOffset);
            grpConvert.Controls.Add(numOffset);
            grpConvert.Controls.Add(lblConvertHint);
            grpConvert.Dock = DockStyle.Top;
            grpConvert.Font = new Font("微软雅黑", 12F);
            grpConvert.Location = new Point(12, 12);
            grpConvert.Margin = new Padding(4, 8, 4, 4);
            grpConvert.MinimumSize = new Size(1, 1);
            grpConvert.Name = "grpConvert";
            grpConvert.Padding = new Padding(10, 32, 10, 10);
            grpConvert.Size = new Size(564, 108);
            grpConvert.TabIndex = 2;
            grpConvert.Text = "数值转换（可选，最终结果 = 原始值 × 倍数 + 偏移）";
            grpConvert.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblScaleFactor
            // 
            lblScaleFactor.AutoSize = true;
            lblScaleFactor.Font = new Font("微软雅黑", 11F);
            lblScaleFactor.ForeColor = Color.FromArgb(48, 48, 48);
            lblScaleFactor.Location = new Point(4, 44);
            lblScaleFactor.Name = "lblScaleFactor";
            lblScaleFactor.Size = new Size(43, 20);
            lblScaleFactor.TabIndex = 0;
            lblScaleFactor.Text = "倍数:";
            lblScaleFactor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numScaleFactor
            // 
            numScaleFactor.DecimalPlaces = 4;
            numScaleFactor.Font = new Font("微软雅黑", 12F);
            numScaleFactor.Location = new Point(70, 40);
            numScaleFactor.Margin = new Padding(4, 5, 4, 5);
            numScaleFactor.Maximum = 99999.9999D;
            numScaleFactor.Minimum = -99999.9999D;
            numScaleFactor.MinimumSize = new Size(1, 16);
            numScaleFactor.Name = "numScaleFactor";
            numScaleFactor.Padding = new Padding(5);
            numScaleFactor.ShowText = false;
            numScaleFactor.Size = new Size(143, 30);
            numScaleFactor.Step = 1D;
            numScaleFactor.TabIndex = 0;
            numScaleFactor.Text = "1.0000";
            numScaleFactor.TextAlignment = ContentAlignment.MiddleCenter;
            numScaleFactor.Value = 1D;
            // 
            // lblOffset
            // 
            lblOffset.AutoSize = true;
            lblOffset.Font = new Font("微软雅黑", 11F);
            lblOffset.ForeColor = Color.FromArgb(48, 48, 48);
            lblOffset.Location = new Point(268, 44);
            lblOffset.Name = "lblOffset";
            lblOffset.Size = new Size(43, 20);
            lblOffset.TabIndex = 1;
            lblOffset.Text = "偏移:";
            lblOffset.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numOffset
            // 
            numOffset.DecimalPlaces = 4;
            numOffset.Font = new Font("微软雅黑", 12F);
            numOffset.Location = new Point(322, 40);
            numOffset.Margin = new Padding(4, 5, 4, 5);
            numOffset.Maximum = 99999.9999D;
            numOffset.Minimum = -99999.9999D;
            numOffset.MinimumSize = new Size(1, 16);
            numOffset.Name = "numOffset";
            numOffset.Padding = new Padding(5);
            numOffset.ShowText = false;
            numOffset.Size = new Size(143, 30);
            numOffset.Step = 1D;
            numOffset.TabIndex = 1;
            numOffset.Text = "0.0000";
            numOffset.TextAlignment = ContentAlignment.MiddleCenter;
            numOffset.Value = 0D;
            // 
            // lblConvertHint
            // 
            lblConvertHint.Font = new Font("微软雅黑", 10F);
            lblConvertHint.ForeColor = Color.FromArgb(100, 100, 200);
            lblConvertHint.Location = new Point(4, 76);
            lblConvertHint.Name = "lblConvertHint";
            lblConvertHint.Size = new Size(480, 22);
            lblConvertHint.TabIndex = 2;
            lblConvertHint.Text = "示例：仪器返回\"256\"表示25.6℃，则设置 倍数=0.1，偏移=0";
            lblConvertHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(chkAdvancedMode);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnOk);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 534);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(0, 8, 16, 8);
            panelBottom.Size = new Size(588, 52);
            panelBottom.TabIndex = 1;
            // 
            // chkAdvancedMode
            // 
            chkAdvancedMode.CheckBoxColor = Color.FromArgb(65, 100, 204);
            chkAdvancedMode.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            chkAdvancedMode.ForeColor = Color.FromArgb(48, 48, 48);
            chkAdvancedMode.Location = new Point(15, 11);
            chkAdvancedMode.MinimumSize = new Size(1, 1);
            chkAdvancedMode.Name = "chkAdvancedMode";
            chkAdvancedMode.Size = new Size(258, 30);
            chkAdvancedMode.TabIndex = 0;
            chkAdvancedMode.Text = "高级模式（启用正则表达式）";
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("微软雅黑", 12F);
            btnCancel.Location = new Point(484, 9);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 36);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnOk
            // 
            btnOk.FillColor = Color.FromArgb(65, 100, 204);
            btnOk.Font = new Font("微软雅黑", 12F);
            btnOk.Location = new Point(388, 9);
            btnOk.MinimumSize = new Size(1, 1);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(88, 36);
            btnOk.TabIndex = 0;
            btnOk.Text = "确定";
            btnOk.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // FrmParseRuleEditor
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(588, 586);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Font = new Font("微软雅黑", 12F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmParseRuleEditor";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "解析规则编辑";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 13F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 520, 607);
            panelMain.ResumeLayout(false);
            grpBasic.ResumeLayout(false);
            grpBasic.PerformLayout();
            grpParseParams.ResumeLayout(false);
            panelJson.ResumeLayout(false);
            panelJson.PerformLayout();
            panelDelimiter.ResumeLayout(false);
            panelDelimiter.PerformLayout();
            panelRegex.ResumeLayout(false);
            panelRegex.PerformLayout();
            panelPosition.ResumeLayout(false);
            panelPosition.PerformLayout();
            grpConvert.ResumeLayout(false);
            grpConvert.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // 基本信息
        private Panel panelMain;
        private UIGroupBox grpBasic;
        private UILabel lblName;
        private UITextBox txtName;
        private UILabel lblTargetVariable;
        private UITextBox txtTargetVariable;
        private UILabel lblParseType;
        private UIComboBox cboParseType;
        private UILabel lblTargetDataType;
        private UIComboBox cboTargetDataType;
        // 解析参数区
        private UIGroupBox grpParseParams;
        private Panel panelPosition;
        private UILabel lblStartPos;
        private UIIntegerUpDown numStartPosition;
        private UILabel lblLength;
        private UIIntegerUpDown numLength;
        private UILabel lblPositionHint;
        private Panel panelDelimiter;
        private UILabel lblDelimiter;
        private UITextBox txtDelimiter;
        private UILabel lblSegmentIndex;
        private UIIntegerUpDown numSegmentIndex;
        private UILabel lblDelimiterHint;
        private Panel panelRegex;
        private UILabel lblRegexPattern;
        private UITextBox txtRegexPattern;
        private UIButton btnTestRegex;
        private UILabel lblRegexGroup;
        private UIIntegerUpDown numRegexGroup;
        private UILabel lblRegexHint;
        private Panel panelJson;
        private UILabel lblJsonPath;
        private UITextBox txtJsonPath;
        private UILabel lblJsonHint;
        // 数值转换
        private UIGroupBox grpConvert;
        private UILabel lblScaleFactor;
        private UIDoubleUpDown numScaleFactor;
        private UILabel lblOffset;
        private UIDoubleUpDown numOffset;
        private UILabel lblConvertHint;
        // 底部
        private Panel panelBottom;
        private UIButton btnOk;
        private UIButton btnCancel;

        // ── 控件声明区追加 ──
        private UILabel lblRegexTemplate;
        private UIComboBox cboRegexTemplate;
        private UILabel lblAdvancedHint;
        private UICheckBox chkAdvancedMode;
    }
}
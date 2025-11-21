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
            components = new System.ComponentModel.Container();
            pnlMain = new UIPanel();
            pnlBasicInfo = new UIGroupBox();
            lblDetectionName = new UILabel();
            txtDetectionName = new UITextBox();
            lblDescription = new UILabel();
            txtDescription = new UITextBox();
            lblDetectionType = new UILabel();
            cmbDetectionType = new UIComboBox();
            chkEnabled = new UICheckBox();
            pnlDataSource = new UIGroupBox();
            lblDataSourceType = new UILabel();
            cmbDataSourceType = new UIComboBox();
            pnlVariableSource = new UIPanel();
            lblVariableName = new UILabel();
            CboVariableName = new UIComboBox();
            pnlPlcSource = new UIPanel();
            lblPlcModule = new UILabel();
            CboPlcModule = new UIComboBox();
            lblPlcAddress = new UILabel();
            CboPlcAddress = new UIComboBox();
            pnlDetectionCondition = new UIGroupBox();
            lblOperator = new UILabel();
            cmbOperator = new UIComboBox();
            lblMinValue = new UILabel();
            numMinValue = new UIDoubleUpDown();
            lblMaxValue = new UILabel();
            numMaxValue = new UIDoubleUpDown();
            lblTargetValue = new UILabel();
            txtTargetValue = new UITextBox();
            lblTolerance = new UILabel();
            numTolerance = new UIDoubleUpDown();
            lblThreshold = new UILabel();
            numThreshold = new UIDoubleUpDown();
            pnlTimeout = new UIGroupBox();
            lblTimeoutMs = new UILabel();
            numTimeoutMs = new UIIntegerUpDown();
            lblRetryCount = new UILabel();
            numRetryCount = new UIIntegerUpDown();
            lblRetryInterval = new UILabel();
            numRetryIntervalMs = new UIIntegerUpDown();
            pnlResultHandling = new UIGroupBox();
            chkSaveResult = new UICheckBox();
            lblResultVariable = new UILabel();
            CboResultVariable = new UIComboBox();
            chkSaveValue = new UICheckBox();
            lblValueVariable = new UILabel();
            CboValueVariable = new UIComboBox();
            lblFailureAction = new UILabel();
            cmbFailureAction = new UIComboBox();
            lblFailureStep = new UILabel();
            numFailureStep = new UIIntegerUpDown();
            lblSuccessStep = new UILabel();
            numSuccessStep = new UIIntegerUpDown();
            chkShowResult = new UICheckBox();
            pnlButtons = new UIPanel();
            btnOK = new UIButton();
            btnCancel = new UIButton();
            btnTest = new UIButton();
            btnHelp = new UIButton();
            pnlStatus = new UIPanel();
            lblStatus = new UILabel();
            lblStatusText = new UILabel();
            toolTip = new ToolTip(components);
            pnlMain.SuspendLayout();
            pnlBasicInfo.SuspendLayout();
            pnlDataSource.SuspendLayout();
            pnlVariableSource.SuspendLayout();
            pnlPlcSource.SuspendLayout();
            pnlDetectionCondition.SuspendLayout();
            pnlTimeout.SuspendLayout();
            pnlResultHandling.SuspendLayout();
            pnlButtons.SuspendLayout();
            pnlStatus.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlBasicInfo);
            pnlMain.Controls.Add(pnlDataSource);
            pnlMain.Controls.Add(pnlDetectionCondition);
            pnlMain.Controls.Add(pnlTimeout);
            pnlMain.Controls.Add(pnlResultHandling);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.FromArgb(248, 249, 250);
            pnlMain.Font = new Font("微软雅黑", 12F);
            pnlMain.Location = new Point(0, 35);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.MinimumSize = new Size(1, 1);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(10);
            pnlMain.Radius = 0;
            pnlMain.RectColor = Color.FromArgb(216, 219, 227);
            pnlMain.RectSides = ToolStripStatusLabelBorderSides.None;
            pnlMain.Size = new Size(900, 783);
            pnlMain.Style = UIStyle.Custom;
            pnlMain.StyleCustomMode = true;
            pnlMain.TabIndex = 0;
            pnlMain.Text = null;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pnlBasicInfo
            // 
            pnlBasicInfo.Controls.Add(lblDetectionName);
            pnlBasicInfo.Controls.Add(txtDetectionName);
            pnlBasicInfo.Controls.Add(lblDescription);
            pnlBasicInfo.Controls.Add(txtDescription);
            pnlBasicInfo.Controls.Add(lblDetectionType);
            pnlBasicInfo.Controls.Add(cmbDetectionType);
            pnlBasicInfo.Controls.Add(chkEnabled);
            pnlBasicInfo.FillColor = Color.White;
            pnlBasicInfo.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            pnlBasicInfo.Location = new Point(15, 15);
            pnlBasicInfo.Margin = new Padding(4, 5, 4, 5);
            pnlBasicInfo.MinimumSize = new Size(1, 1);
            pnlBasicInfo.Name = "pnlBasicInfo";
            pnlBasicInfo.Padding = new Padding(10, 35, 10, 10);
            pnlBasicInfo.RectColor = Color.FromArgb(216, 219, 227);
            pnlBasicInfo.Size = new Size(870, 165);
            pnlBasicInfo.Style = UIStyle.Custom;
            pnlBasicInfo.TabIndex = 0;
            pnlBasicInfo.Text = "基本信息";
            pnlBasicInfo.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblDetectionName
            // 
            lblDetectionName.BackColor = Color.Transparent;
            lblDetectionName.Font = new Font("微软雅黑", 10F);
            lblDetectionName.ForeColor = Color.FromArgb(48, 48, 48);
            lblDetectionName.Location = new Point(20, 40);
            lblDetectionName.Name = "lblDetectionName";
            lblDetectionName.Size = new Size(100, 23);
            lblDetectionName.TabIndex = 0;
            lblDetectionName.Text = "检测名称:";
            lblDetectionName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDetectionName
            // 
            txtDetectionName.Cursor = Cursors.IBeam;
            txtDetectionName.Font = new Font("微软雅黑", 10F);
            txtDetectionName.Location = new Point(130, 38);
            txtDetectionName.Margin = new Padding(4, 5, 4, 5);
            txtDetectionName.MinimumSize = new Size(1, 16);
            txtDetectionName.Name = "txtDetectionName";
            txtDetectionName.Padding = new Padding(5);
            txtDetectionName.Radius = 3;
            txtDetectionName.RectColor = Color.FromArgb(216, 219, 227);
            txtDetectionName.ShowText = false;
            txtDetectionName.Size = new Size(280, 29);
            txtDetectionName.Style = UIStyle.Custom;
            txtDetectionName.TabIndex = 1;
            txtDetectionName.TextAlignment = ContentAlignment.MiddleLeft;
            txtDetectionName.Watermark = "输入检测项名称";
            // 
            // lblDescription
            // 
            lblDescription.BackColor = Color.Transparent;
            lblDescription.Font = new Font("微软雅黑", 10F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(430, 40);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(80, 23);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "描述:";
            lblDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            txtDescription.Cursor = Cursors.IBeam;
            txtDescription.Font = new Font("微软雅黑", 10F);
            txtDescription.Location = new Point(520, 38);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.Radius = 3;
            txtDescription.RectColor = Color.FromArgb(216, 219, 227);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(330, 29);
            txtDescription.Style = UIStyle.Custom;
            txtDescription.TabIndex = 3;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "输入检测描述信息";
            // 
            // lblDetectionType
            // 
            lblDetectionType.BackColor = Color.Transparent;
            lblDetectionType.Font = new Font("微软雅黑", 10F);
            lblDetectionType.ForeColor = Color.FromArgb(48, 48, 48);
            lblDetectionType.Location = new Point(20, 85);
            lblDetectionType.Name = "lblDetectionType";
            lblDetectionType.Size = new Size(100, 23);
            lblDetectionType.TabIndex = 4;
            lblDetectionType.Text = "检测类型:";
            lblDetectionType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbDetectionType
            // 
            cmbDetectionType.DataSource = null;
            cmbDetectionType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbDetectionType.FillColor = Color.White;
            cmbDetectionType.Font = new Font("微软雅黑", 10F);
            cmbDetectionType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDetectionType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDetectionType.Location = new Point(130, 83);
            cmbDetectionType.Margin = new Padding(4, 5, 4, 5);
            cmbDetectionType.MinimumSize = new Size(63, 0);
            cmbDetectionType.Name = "cmbDetectionType";
            cmbDetectionType.Padding = new Padding(0, 0, 30, 2);
            cmbDetectionType.Radius = 3;
            cmbDetectionType.RectColor = Color.FromArgb(216, 219, 227);
            cmbDetectionType.Size = new Size(280, 29);
            cmbDetectionType.Style = UIStyle.Custom;
            cmbDetectionType.SymbolSize = 24;
            cmbDetectionType.TabIndex = 5;
            cmbDetectionType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDetectionType.Watermark = "";
            // 
            // chkEnabled
            // 
            chkEnabled.BackColor = Color.Transparent;
            chkEnabled.Checked = true;
            chkEnabled.Cursor = Cursors.Hand;
            chkEnabled.Font = new Font("微软雅黑", 10F);
            chkEnabled.ForeColor = Color.FromArgb(48, 48, 48);
            chkEnabled.Location = new Point(520, 85);
            chkEnabled.MinimumSize = new Size(1, 1);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Padding = new Padding(22, 0, 0, 0);
            chkEnabled.Size = new Size(150, 29);
            chkEnabled.Style = UIStyle.Custom;
            chkEnabled.TabIndex = 6;
            chkEnabled.Text = "启用此检测项";
            // 
            // pnlDataSource
            // 
            pnlDataSource.Controls.Add(lblDataSourceType);
            pnlDataSource.Controls.Add(cmbDataSourceType);
            pnlDataSource.Controls.Add(pnlVariableSource);
            pnlDataSource.Controls.Add(pnlPlcSource);
            pnlDataSource.FillColor = Color.White;
            pnlDataSource.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            pnlDataSource.Location = new Point(15, 190);
            pnlDataSource.Margin = new Padding(4, 5, 4, 5);
            pnlDataSource.MinimumSize = new Size(1, 1);
            pnlDataSource.Name = "pnlDataSource";
            pnlDataSource.Padding = new Padding(10, 35, 10, 10);
            pnlDataSource.RectColor = Color.FromArgb(216, 219, 227);
            pnlDataSource.Size = new Size(870, 150);
            pnlDataSource.Style = UIStyle.Custom;
            pnlDataSource.TabIndex = 1;
            pnlDataSource.Text = "数据源配置";
            pnlDataSource.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblDataSourceType
            // 
            lblDataSourceType.BackColor = Color.Transparent;
            lblDataSourceType.Font = new Font("微软雅黑", 10F);
            lblDataSourceType.ForeColor = Color.FromArgb(48, 48, 48);
            lblDataSourceType.Location = new Point(20, 40);
            lblDataSourceType.Name = "lblDataSourceType";
            lblDataSourceType.Size = new Size(100, 23);
            lblDataSourceType.TabIndex = 0;
            lblDataSourceType.Text = "数据源类型:";
            lblDataSourceType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbDataSourceType
            // 
            cmbDataSourceType.DataSource = null;
            cmbDataSourceType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbDataSourceType.FillColor = Color.White;
            cmbDataSourceType.Font = new Font("微软雅黑", 10F);
            cmbDataSourceType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDataSourceType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDataSourceType.Location = new Point(130, 38);
            cmbDataSourceType.Margin = new Padding(4, 5, 4, 5);
            cmbDataSourceType.MinimumSize = new Size(63, 0);
            cmbDataSourceType.Name = "cmbDataSourceType";
            cmbDataSourceType.Padding = new Padding(0, 0, 30, 2);
            cmbDataSourceType.Radius = 3;
            cmbDataSourceType.RectColor = Color.FromArgb(216, 219, 227);
            cmbDataSourceType.Size = new Size(280, 29);
            cmbDataSourceType.Style = UIStyle.Custom;
            cmbDataSourceType.SymbolSize = 24;
            cmbDataSourceType.TabIndex = 1;
            cmbDataSourceType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDataSourceType.Watermark = "";
            // 
            // pnlVariableSource
            // 
            pnlVariableSource.Controls.Add(lblVariableName);
            pnlVariableSource.Controls.Add(CboVariableName);
            pnlVariableSource.FillColor = Color.FromArgb(248, 249, 250);
            pnlVariableSource.Font = new Font("微软雅黑", 10F);
            pnlVariableSource.Location = new Point(20, 80);
            pnlVariableSource.Margin = new Padding(4, 5, 4, 5);
            pnlVariableSource.MinimumSize = new Size(1, 1);
            pnlVariableSource.Name = "pnlVariableSource";
            pnlVariableSource.Padding = new Padding(5);
            pnlVariableSource.Radius = 3;
            pnlVariableSource.RectColor = Color.FromArgb(216, 219, 227);
            pnlVariableSource.Size = new Size(830, 55);
            pnlVariableSource.Style = UIStyle.Custom;
            pnlVariableSource.TabIndex = 2;
            pnlVariableSource.Text = null;
            pnlVariableSource.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblVariableName
            // 
            lblVariableName.BackColor = Color.Transparent;
            lblVariableName.Font = new Font("微软雅黑", 10F);
            lblVariableName.ForeColor = Color.FromArgb(48, 48, 48);
            lblVariableName.Location = new Point(10, 15);
            lblVariableName.Name = "lblVariableName";
            lblVariableName.Size = new Size(100, 23);
            lblVariableName.TabIndex = 0;
            lblVariableName.Text = "变量名称:";
            lblVariableName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CboVariableName
            // 
            CboVariableName.DataSource = null;
            CboVariableName.DropDownStyle = UIDropDownStyle.DropDownList;
            CboVariableName.FillColor = Color.White;
            CboVariableName.Font = new Font("微软雅黑", 10F);
            CboVariableName.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboVariableName.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboVariableName.Location = new Point(120, 13);
            CboVariableName.Margin = new Padding(4, 5, 4, 5);
            CboVariableName.MinimumSize = new Size(63, 0);
            CboVariableName.Name = "CboVariableName";
            CboVariableName.Padding = new Padding(0, 0, 30, 2);
            CboVariableName.Radius = 3;
            CboVariableName.RectColor = Color.FromArgb(216, 219, 227);
            CboVariableName.Size = new Size(690, 29);
            CboVariableName.Style = UIStyle.Custom;
            CboVariableName.SymbolSize = 24;
            CboVariableName.TabIndex = 1;
            CboVariableName.TextAlignment = ContentAlignment.MiddleLeft;
            CboVariableName.Watermark = "选择要检测的变量";
            // 
            // pnlPlcSource
            // 
            pnlPlcSource.Controls.Add(lblPlcModule);
            pnlPlcSource.Controls.Add(CboPlcModule);
            pnlPlcSource.Controls.Add(lblPlcAddress);
            pnlPlcSource.Controls.Add(CboPlcAddress);
            pnlPlcSource.FillColor = Color.FromArgb(248, 249, 250);
            pnlPlcSource.Font = new Font("微软雅黑", 10F);
            pnlPlcSource.Location = new Point(20, 80);
            pnlPlcSource.Margin = new Padding(4, 5, 4, 5);
            pnlPlcSource.MinimumSize = new Size(1, 1);
            pnlPlcSource.Name = "pnlPlcSource";
            pnlPlcSource.Padding = new Padding(5);
            pnlPlcSource.Radius = 3;
            pnlPlcSource.RectColor = Color.FromArgb(216, 219, 227);
            pnlPlcSource.Size = new Size(830, 55);
            pnlPlcSource.Style = UIStyle.Custom;
            pnlPlcSource.TabIndex = 3;
            pnlPlcSource.Text = null;
            pnlPlcSource.TextAlignment = ContentAlignment.MiddleCenter;
            pnlPlcSource.Visible = false;
            // 
            // lblPlcModule
            // 
            lblPlcModule.Font = new Font("微软雅黑", 10F);
            lblPlcModule.ForeColor = Color.FromArgb(48, 48, 48);
            lblPlcModule.Location = new Point(10, 15);
            lblPlcModule.Name = "lblPlcModule";
            lblPlcModule.Size = new Size(100, 23);
            lblPlcModule.TabIndex = 0;
            lblPlcModule.Text = "PLC模块:";
            lblPlcModule.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CboPlcModule
            // 
            CboPlcModule.DataSource = null;
            CboPlcModule.DropDownStyle = UIDropDownStyle.DropDownList;
            CboPlcModule.FillColor = Color.White;
            CboPlcModule.Font = new Font("微软雅黑", 10F);
            CboPlcModule.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboPlcModule.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboPlcModule.Location = new Point(120, 13);
            CboPlcModule.Margin = new Padding(4, 5, 4, 5);
            CboPlcModule.MinimumSize = new Size(63, 0);
            CboPlcModule.Name = "CboPlcModule";
            CboPlcModule.Padding = new Padding(0, 0, 30, 2);
            CboPlcModule.Radius = 3;
            CboPlcModule.RectColor = Color.FromArgb(216, 219, 227);
            CboPlcModule.Size = new Size(280, 29);
            CboPlcModule.Style = UIStyle.Custom;
            CboPlcModule.SymbolSize = 24;
            CboPlcModule.TabIndex = 1;
            CboPlcModule.TextAlignment = ContentAlignment.MiddleLeft;
            CboPlcModule.Watermark = "选择PLC模块";
            // 
            // lblPlcAddress
            // 
            lblPlcAddress.Font = new Font("微软雅黑", 10F);
            lblPlcAddress.ForeColor = Color.FromArgb(48, 48, 48);
            lblPlcAddress.Location = new Point(420, 15);
            lblPlcAddress.Name = "lblPlcAddress";
            lblPlcAddress.Size = new Size(100, 23);
            lblPlcAddress.TabIndex = 2;
            lblPlcAddress.Text = "PLC地址:";
            lblPlcAddress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CboPlcAddress
            // 
            CboPlcAddress.DataSource = null;
            CboPlcAddress.DropDownStyle = UIDropDownStyle.DropDownList;
            CboPlcAddress.FillColor = Color.White;
            CboPlcAddress.Font = new Font("微软雅黑", 10F);
            CboPlcAddress.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboPlcAddress.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboPlcAddress.Location = new Point(530, 13);
            CboPlcAddress.Margin = new Padding(4, 5, 4, 5);
            CboPlcAddress.MinimumSize = new Size(63, 0);
            CboPlcAddress.Name = "CboPlcAddress";
            CboPlcAddress.Padding = new Padding(0, 0, 30, 2);
            CboPlcAddress.Radius = 3;
            CboPlcAddress.RectColor = Color.FromArgb(216, 219, 227);
            CboPlcAddress.Size = new Size(280, 29);
            CboPlcAddress.Style = UIStyle.Custom;
            CboPlcAddress.SymbolSize = 24;
            CboPlcAddress.TabIndex = 3;
            CboPlcAddress.TextAlignment = ContentAlignment.MiddleLeft;
            CboPlcAddress.Watermark = "选择PLC地址";
            // 
            // pnlDetectionCondition
            // 
            pnlDetectionCondition.Controls.Add(lblOperator);
            pnlDetectionCondition.Controls.Add(cmbOperator);
            pnlDetectionCondition.Controls.Add(lblMinValue);
            pnlDetectionCondition.Controls.Add(numMinValue);
            pnlDetectionCondition.Controls.Add(lblMaxValue);
            pnlDetectionCondition.Controls.Add(numMaxValue);
            pnlDetectionCondition.Controls.Add(lblTargetValue);
            pnlDetectionCondition.Controls.Add(txtTargetValue);
            pnlDetectionCondition.Controls.Add(lblTolerance);
            pnlDetectionCondition.Controls.Add(numTolerance);
            pnlDetectionCondition.Controls.Add(lblThreshold);
            pnlDetectionCondition.Controls.Add(numThreshold);
            pnlDetectionCondition.FillColor = Color.White;
            pnlDetectionCondition.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            pnlDetectionCondition.Location = new Point(15, 350);
            pnlDetectionCondition.Margin = new Padding(4, 5, 4, 5);
            pnlDetectionCondition.MinimumSize = new Size(1, 1);
            pnlDetectionCondition.Name = "pnlDetectionCondition";
            pnlDetectionCondition.Padding = new Padding(10, 35, 10, 10);
            pnlDetectionCondition.RectColor = Color.FromArgb(216, 219, 227);
            pnlDetectionCondition.Size = new Size(870, 135);
            pnlDetectionCondition.Style = UIStyle.Custom;
            pnlDetectionCondition.TabIndex = 2;
            pnlDetectionCondition.Text = "检测条件";
            pnlDetectionCondition.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblOperator
            // 
            lblOperator.BackColor = Color.Transparent;
            lblOperator.Font = new Font("微软雅黑", 10F);
            lblOperator.ForeColor = Color.FromArgb(48, 48, 48);
            lblOperator.Location = new Point(20, 40);
            lblOperator.Name = "lblOperator";
            lblOperator.Size = new Size(100, 23);
            lblOperator.TabIndex = 0;
            lblOperator.Text = "比较操作符:";
            lblOperator.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbOperator
            // 
            cmbOperator.DataSource = null;
            cmbOperator.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbOperator.FillColor = Color.White;
            cmbOperator.Font = new Font("微软雅黑", 10F);
            cmbOperator.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbOperator.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbOperator.Location = new Point(130, 38);
            cmbOperator.Margin = new Padding(4, 5, 4, 5);
            cmbOperator.MinimumSize = new Size(63, 0);
            cmbOperator.Name = "cmbOperator";
            cmbOperator.Padding = new Padding(0, 0, 30, 2);
            cmbOperator.Radius = 3;
            cmbOperator.RectColor = Color.FromArgb(216, 219, 227);
            cmbOperator.Size = new Size(200, 29);
            cmbOperator.Style = UIStyle.Custom;
            cmbOperator.SymbolSize = 24;
            cmbOperator.TabIndex = 1;
            cmbOperator.TextAlignment = ContentAlignment.MiddleLeft;
            cmbOperator.Watermark = "";
            // 
            // lblMinValue
            // 
            lblMinValue.BackColor = Color.Transparent;
            lblMinValue.Font = new Font("微软雅黑", 10F);
            lblMinValue.ForeColor = Color.FromArgb(48, 48, 48);
            lblMinValue.Location = new Point(360, 40);
            lblMinValue.Name = "lblMinValue";
            lblMinValue.Size = new Size(80, 23);
            lblMinValue.TabIndex = 2;
            lblMinValue.Text = "最小值:";
            lblMinValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numMinValue
            // 
            numMinValue.Font = new Font("微软雅黑", 10F);
            numMinValue.Location = new Point(450, 38);
            numMinValue.Margin = new Padding(4, 5, 4, 5);
            numMinValue.Maximum = 1000000D;
            numMinValue.Minimum = -1000000D;
            numMinValue.MinimumSize = new Size(100, 0);
            numMinValue.Name = "numMinValue";
            numMinValue.Padding = new Padding(5);
            numMinValue.Radius = 3;
            numMinValue.RectColor = Color.FromArgb(216, 219, 227);
            numMinValue.ShowText = false;
            numMinValue.Size = new Size(150, 29);
            numMinValue.Step = 1D;
            numMinValue.Style = UIStyle.Custom;
            numMinValue.TabIndex = 3;
            numMinValue.Text = "0.00";
            numMinValue.TextAlignment = ContentAlignment.MiddleCenter;
            numMinValue.Value = 0D;
            // 
            // lblMaxValue
            // 
            lblMaxValue.BackColor = Color.Transparent;
            lblMaxValue.Font = new Font("微软雅黑", 10F);
            lblMaxValue.ForeColor = Color.FromArgb(48, 48, 48);
            lblMaxValue.Location = new Point(630, 40);
            lblMaxValue.Name = "lblMaxValue";
            lblMaxValue.Size = new Size(80, 23);
            lblMaxValue.TabIndex = 4;
            lblMaxValue.Text = "最大值:";
            lblMaxValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numMaxValue
            // 
            numMaxValue.Font = new Font("微软雅黑", 10F);
            numMaxValue.Location = new Point(720, 38);
            numMaxValue.Margin = new Padding(4, 5, 4, 5);
            numMaxValue.Maximum = 1000000D;
            numMaxValue.Minimum = -1000000D;
            numMaxValue.MinimumSize = new Size(100, 0);
            numMaxValue.Name = "numMaxValue";
            numMaxValue.Padding = new Padding(5);
            numMaxValue.Radius = 3;
            numMaxValue.RectColor = Color.FromArgb(216, 219, 227);
            numMaxValue.ShowText = false;
            numMaxValue.Size = new Size(130, 29);
            numMaxValue.Step = 1D;
            numMaxValue.Style = UIStyle.Custom;
            numMaxValue.TabIndex = 5;
            numMaxValue.Text = "100.00";
            numMaxValue.TextAlignment = ContentAlignment.MiddleCenter;
            numMaxValue.Value = 100D;
            // 
            // lblTargetValue
            // 
            lblTargetValue.BackColor = Color.Transparent;
            lblTargetValue.Font = new Font("微软雅黑", 10F);
            lblTargetValue.ForeColor = Color.FromArgb(48, 48, 48);
            lblTargetValue.Location = new Point(20, 85);
            lblTargetValue.Name = "lblTargetValue";
            lblTargetValue.Size = new Size(100, 23);
            lblTargetValue.TabIndex = 6;
            lblTargetValue.Text = "目标值:";
            lblTargetValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTargetValue
            // 
            txtTargetValue.Cursor = Cursors.IBeam;
            txtTargetValue.Font = new Font("微软雅黑", 10F);
            txtTargetValue.Location = new Point(130, 83);
            txtTargetValue.Margin = new Padding(4, 5, 4, 5);
            txtTargetValue.MinimumSize = new Size(1, 16);
            txtTargetValue.Name = "txtTargetValue";
            txtTargetValue.Padding = new Padding(5);
            txtTargetValue.Radius = 3;
            txtTargetValue.RectColor = Color.FromArgb(216, 219, 227);
            txtTargetValue.ShowText = false;
            txtTargetValue.Size = new Size(200, 29);
            txtTargetValue.Style = UIStyle.Custom;
            txtTargetValue.TabIndex = 7;
            txtTargetValue.TextAlignment = ContentAlignment.MiddleLeft;
            txtTargetValue.Watermark = "输入目标值或变量";
            // 
            // lblTolerance
            // 
            lblTolerance.BackColor = Color.Transparent;
            lblTolerance.Font = new Font("微软雅黑", 10F);
            lblTolerance.ForeColor = Color.FromArgb(48, 48, 48);
            lblTolerance.Location = new Point(360, 85);
            lblTolerance.Name = "lblTolerance";
            lblTolerance.Size = new Size(80, 23);
            lblTolerance.TabIndex = 8;
            lblTolerance.Text = "容差:";
            lblTolerance.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numTolerance
            // 
            numTolerance.Font = new Font("微软雅黑", 10F);
            numTolerance.Location = new Point(450, 83);
            numTolerance.Margin = new Padding(4, 5, 4, 5);
            numTolerance.Maximum = 1000000D;
            numTolerance.Minimum = 0D;
            numTolerance.MinimumSize = new Size(100, 0);
            numTolerance.Name = "numTolerance";
            numTolerance.Padding = new Padding(5);
            numTolerance.Radius = 3;
            numTolerance.RectColor = Color.FromArgb(216, 219, 227);
            numTolerance.ShowText = false;
            numTolerance.Size = new Size(150, 29);
            numTolerance.Step = 1D;
            numTolerance.Style = UIStyle.Custom;
            numTolerance.TabIndex = 9;
            numTolerance.Text = "0.00";
            numTolerance.TextAlignment = ContentAlignment.MiddleCenter;
            numTolerance.Value = 0D;
            // 
            // lblThreshold
            // 
            lblThreshold.BackColor = Color.Transparent;
            lblThreshold.Font = new Font("微软雅黑", 10F);
            lblThreshold.ForeColor = Color.FromArgb(48, 48, 48);
            lblThreshold.Location = new Point(630, 85);
            lblThreshold.Name = "lblThreshold";
            lblThreshold.Size = new Size(80, 23);
            lblThreshold.TabIndex = 10;
            lblThreshold.Text = "阈值:";
            lblThreshold.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numThreshold
            // 
            numThreshold.Font = new Font("微软雅黑", 10F);
            numThreshold.Location = new Point(720, 83);
            numThreshold.Margin = new Padding(4, 5, 4, 5);
            numThreshold.Maximum = 1000000D;
            numThreshold.Minimum = 0D;
            numThreshold.MinimumSize = new Size(100, 0);
            numThreshold.Name = "numThreshold";
            numThreshold.Padding = new Padding(5);
            numThreshold.Radius = 3;
            numThreshold.RectColor = Color.FromArgb(216, 219, 227);
            numThreshold.ShowText = false;
            numThreshold.Size = new Size(130, 29);
            numThreshold.Step = 1D;
            numThreshold.Style = UIStyle.Custom;
            numThreshold.TabIndex = 11;
            numThreshold.Text = "0.00";
            numThreshold.TextAlignment = ContentAlignment.MiddleCenter;
            numThreshold.Value = 0D;
            // 
            // pnlTimeout
            // 
            pnlTimeout.Controls.Add(lblTimeoutMs);
            pnlTimeout.Controls.Add(numTimeoutMs);
            pnlTimeout.Controls.Add(lblRetryCount);
            pnlTimeout.Controls.Add(numRetryCount);
            pnlTimeout.Controls.Add(lblRetryInterval);
            pnlTimeout.Controls.Add(numRetryIntervalMs);
            pnlTimeout.FillColor = Color.White;
            pnlTimeout.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            pnlTimeout.Location = new Point(15, 495);
            pnlTimeout.Margin = new Padding(4, 5, 4, 5);
            pnlTimeout.MinimumSize = new Size(1, 1);
            pnlTimeout.Name = "pnlTimeout";
            pnlTimeout.Padding = new Padding(10, 35, 10, 10);
            pnlTimeout.RectColor = Color.FromArgb(216, 219, 227);
            pnlTimeout.Size = new Size(870, 90);
            pnlTimeout.Style = UIStyle.Custom;
            pnlTimeout.TabIndex = 3;
            pnlTimeout.Text = "超时和重试设置";
            pnlTimeout.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblTimeoutMs
            // 
            lblTimeoutMs.BackColor = Color.Transparent;
            lblTimeoutMs.Font = new Font("微软雅黑", 10F);
            lblTimeoutMs.ForeColor = Color.FromArgb(48, 48, 48);
            lblTimeoutMs.Location = new Point(20, 40);
            lblTimeoutMs.Name = "lblTimeoutMs";
            lblTimeoutMs.Size = new Size(100, 23);
            lblTimeoutMs.TabIndex = 0;
            lblTimeoutMs.Text = "超时(ms):";
            lblTimeoutMs.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numTimeoutMs
            // 
            numTimeoutMs.Font = new Font("微软雅黑", 10F);
            numTimeoutMs.Location = new Point(130, 38);
            numTimeoutMs.Margin = new Padding(4, 5, 4, 5);
            numTimeoutMs.Maximum = 300000D;
            numTimeoutMs.Minimum = 100D;
            numTimeoutMs.MinimumSize = new Size(100, 0);
            numTimeoutMs.Name = "numTimeoutMs";
            numTimeoutMs.Padding = new Padding(5);
            numTimeoutMs.Radius = 3;
            numTimeoutMs.RectColor = Color.FromArgb(216, 219, 227);
            numTimeoutMs.ShowText = false;
            numTimeoutMs.Size = new Size(150, 29);
            numTimeoutMs.Style = UIStyle.Custom;
            numTimeoutMs.TabIndex = 1;
            numTimeoutMs.Text = "5000";
            numTimeoutMs.TextAlignment = ContentAlignment.MiddleCenter;
            numTimeoutMs.Value = 5000;
            // 
            // lblRetryCount
            // 
            lblRetryCount.BackColor = Color.Transparent;
            lblRetryCount.Font = new Font("微软雅黑", 10F);
            lblRetryCount.ForeColor = Color.FromArgb(48, 48, 48);
            lblRetryCount.Location = new Point(310, 40);
            lblRetryCount.Name = "lblRetryCount";
            lblRetryCount.Size = new Size(100, 23);
            lblRetryCount.TabIndex = 2;
            lblRetryCount.Text = "重试次数:";
            lblRetryCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numRetryCount
            // 
            numRetryCount.Font = new Font("微软雅黑", 10F);
            numRetryCount.Location = new Point(420, 38);
            numRetryCount.Margin = new Padding(4, 5, 4, 5);
            numRetryCount.Maximum = 10D;
            numRetryCount.Minimum = 0D;
            numRetryCount.MinimumSize = new Size(100, 0);
            numRetryCount.Name = "numRetryCount";
            numRetryCount.Padding = new Padding(5);
            numRetryCount.Radius = 3;
            numRetryCount.RectColor = Color.FromArgb(216, 219, 227);
            numRetryCount.ShowText = false;
            numRetryCount.Size = new Size(150, 29);
            numRetryCount.Style = UIStyle.Custom;
            numRetryCount.TabIndex = 3;
            numRetryCount.Text = "0";
            numRetryCount.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblRetryInterval
            // 
            lblRetryInterval.BackColor = Color.Transparent;
            lblRetryInterval.Font = new Font("微软雅黑", 10F);
            lblRetryInterval.ForeColor = Color.FromArgb(48, 48, 48);
            lblRetryInterval.Location = new Point(600, 40);
            lblRetryInterval.Name = "lblRetryInterval";
            lblRetryInterval.Size = new Size(110, 23);
            lblRetryInterval.TabIndex = 4;
            lblRetryInterval.Text = "重试间隔(ms):";
            lblRetryInterval.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numRetryIntervalMs
            // 
            numRetryIntervalMs.Font = new Font("微软雅黑", 10F);
            numRetryIntervalMs.Location = new Point(720, 38);
            numRetryIntervalMs.Margin = new Padding(4, 5, 4, 5);
            numRetryIntervalMs.Maximum = 10000D;
            numRetryIntervalMs.Minimum = 100D;
            numRetryIntervalMs.MinimumSize = new Size(100, 0);
            numRetryIntervalMs.Name = "numRetryIntervalMs";
            numRetryIntervalMs.Padding = new Padding(5);
            numRetryIntervalMs.Radius = 3;
            numRetryIntervalMs.RectColor = Color.FromArgb(216, 219, 227);
            numRetryIntervalMs.ShowText = false;
            numRetryIntervalMs.Size = new Size(130, 29);
            numRetryIntervalMs.Style = UIStyle.Custom;
            numRetryIntervalMs.TabIndex = 5;
            numRetryIntervalMs.Text = "1000";
            numRetryIntervalMs.TextAlignment = ContentAlignment.MiddleCenter;
            numRetryIntervalMs.Value = 1000;
            // 
            // pnlResultHandling
            // 
            pnlResultHandling.Controls.Add(chkSaveResult);
            pnlResultHandling.Controls.Add(lblResultVariable);
            pnlResultHandling.Controls.Add(CboResultVariable);
            pnlResultHandling.Controls.Add(chkSaveValue);
            pnlResultHandling.Controls.Add(lblValueVariable);
            pnlResultHandling.Controls.Add(CboValueVariable);
            pnlResultHandling.Controls.Add(lblFailureAction);
            pnlResultHandling.Controls.Add(cmbFailureAction);
            pnlResultHandling.Controls.Add(lblFailureStep);
            pnlResultHandling.Controls.Add(numFailureStep);
            pnlResultHandling.Controls.Add(lblSuccessStep);
            pnlResultHandling.Controls.Add(numSuccessStep);
            pnlResultHandling.Controls.Add(chkShowResult);
            pnlResultHandling.FillColor = Color.White;
            pnlResultHandling.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            pnlResultHandling.Location = new Point(15, 595);
            pnlResultHandling.Margin = new Padding(4, 5, 4, 5);
            pnlResultHandling.MinimumSize = new Size(1, 1);
            pnlResultHandling.Name = "pnlResultHandling";
            pnlResultHandling.Padding = new Padding(10, 35, 10, 10);
            pnlResultHandling.RectColor = Color.FromArgb(216, 219, 227);
            pnlResultHandling.Size = new Size(870, 180);
            pnlResultHandling.Style = UIStyle.Custom;
            pnlResultHandling.TabIndex = 4;
            pnlResultHandling.Text = "结果处理";
            pnlResultHandling.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // chkSaveResult
            // 
            chkSaveResult.BackColor = Color.Transparent;
            chkSaveResult.Cursor = Cursors.Hand;
            chkSaveResult.Font = new Font("微软雅黑", 10F);
            chkSaveResult.ForeColor = Color.FromArgb(48, 48, 48);
            chkSaveResult.Location = new Point(20, 40);
            chkSaveResult.MinimumSize = new Size(1, 1);
            chkSaveResult.Name = "chkSaveResult";
            chkSaveResult.Padding = new Padding(22, 0, 0, 0);
            chkSaveResult.Size = new Size(150, 29);
            chkSaveResult.Style = UIStyle.Custom;
            chkSaveResult.TabIndex = 0;
            chkSaveResult.Text = "保存检测结果";
            // 
            // lblResultVariable
            // 
            lblResultVariable.BackColor = Color.Transparent;
            lblResultVariable.Font = new Font("微软雅黑", 10F);
            lblResultVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblResultVariable.Location = new Point(180, 40);
            lblResultVariable.Name = "lblResultVariable";
            lblResultVariable.Size = new Size(80, 23);
            lblResultVariable.TabIndex = 1;
            lblResultVariable.Text = "结果变量:";
            lblResultVariable.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CboResultVariable
            // 
            CboResultVariable.DataSource = null;
            CboResultVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            CboResultVariable.Enabled = false;
            CboResultVariable.FillColor = Color.White;
            CboResultVariable.Font = new Font("微软雅黑", 10F);
            CboResultVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboResultVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboResultVariable.Location = new Point(270, 38);
            CboResultVariable.Margin = new Padding(4, 5, 4, 5);
            CboResultVariable.MinimumSize = new Size(63, 0);
            CboResultVariable.Name = "CboResultVariable";
            CboResultVariable.Padding = new Padding(0, 0, 30, 2);
            CboResultVariable.Radius = 3;
            CboResultVariable.RectColor = Color.FromArgb(216, 219, 227);
            CboResultVariable.Size = new Size(250, 29);
            CboResultVariable.Style = UIStyle.Custom;
            CboResultVariable.SymbolSize = 24;
            CboResultVariable.TabIndex = 2;
            CboResultVariable.TextAlignment = ContentAlignment.MiddleLeft;
            CboResultVariable.Watermark = "选择保存结果的变量";
            // 
            // chkSaveValue
            // 
            chkSaveValue.BackColor = Color.Transparent;
            chkSaveValue.Cursor = Cursors.Hand;
            chkSaveValue.Font = new Font("微软雅黑", 10F);
            chkSaveValue.ForeColor = Color.FromArgb(48, 48, 48);
            chkSaveValue.Location = new Point(550, 40);
            chkSaveValue.MinimumSize = new Size(1, 1);
            chkSaveValue.Name = "chkSaveValue";
            chkSaveValue.Padding = new Padding(22, 0, 0, 0);
            chkSaveValue.Size = new Size(130, 29);
            chkSaveValue.Style = UIStyle.Custom;
            chkSaveValue.TabIndex = 3;
            chkSaveValue.Text = "保存检测值";
            // 
            // lblValueVariable
            // 
            lblValueVariable.Font = new Font("微软雅黑", 10F);
            lblValueVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblValueVariable.Location = new Point(690, 40);
            lblValueVariable.Name = "lblValueVariable";
            lblValueVariable.Size = new Size(10, 23);
            lblValueVariable.TabIndex = 4;
            lblValueVariable.Text = ":";
            lblValueVariable.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CboValueVariable
            // 
            CboValueVariable.DataSource = null;
            CboValueVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            CboValueVariable.Enabled = false;
            CboValueVariable.FillColor = Color.White;
            CboValueVariable.Font = new Font("微软雅黑", 10F);
            CboValueVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboValueVariable.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboValueVariable.Location = new Point(710, 38);
            CboValueVariable.Margin = new Padding(4, 5, 4, 5);
            CboValueVariable.MinimumSize = new Size(63, 0);
            CboValueVariable.Name = "CboValueVariable";
            CboValueVariable.Padding = new Padding(0, 0, 30, 2);
            CboValueVariable.Radius = 3;
            CboValueVariable.RectColor = Color.FromArgb(216, 219, 227);
            CboValueVariable.Size = new Size(140, 29);
            CboValueVariable.Style = UIStyle.Custom;
            CboValueVariable.SymbolSize = 24;
            CboValueVariable.TabIndex = 5;
            CboValueVariable.TextAlignment = ContentAlignment.MiddleLeft;
            CboValueVariable.Watermark = "值变量";
            // 
            // lblFailureAction
            // 
            lblFailureAction.BackColor = Color.Transparent;
            lblFailureAction.Font = new Font("微软雅黑", 10F);
            lblFailureAction.ForeColor = Color.FromArgb(48, 48, 48);
            lblFailureAction.Location = new Point(20, 85);
            lblFailureAction.Name = "lblFailureAction";
            lblFailureAction.Size = new Size(100, 23);
            lblFailureAction.TabIndex = 6;
            lblFailureAction.Text = "失败后操作:";
            lblFailureAction.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbFailureAction
            // 
            cmbFailureAction.DataSource = null;
            cmbFailureAction.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbFailureAction.FillColor = Color.White;
            cmbFailureAction.Font = new Font("微软雅黑", 10F);
            cmbFailureAction.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbFailureAction.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbFailureAction.Location = new Point(130, 83);
            cmbFailureAction.Margin = new Padding(4, 5, 4, 5);
            cmbFailureAction.MinimumSize = new Size(63, 0);
            cmbFailureAction.Name = "cmbFailureAction";
            cmbFailureAction.Padding = new Padding(0, 0, 30, 2);
            cmbFailureAction.Radius = 3;
            cmbFailureAction.RectColor = Color.FromArgb(216, 219, 227);
            cmbFailureAction.Size = new Size(200, 29);
            cmbFailureAction.Style = UIStyle.Custom;
            cmbFailureAction.SymbolSize = 24;
            cmbFailureAction.TabIndex = 7;
            cmbFailureAction.TextAlignment = ContentAlignment.MiddleLeft;
            cmbFailureAction.Watermark = "";
            // 
            // lblFailureStep
            // 
            lblFailureStep.BackColor = Color.Transparent;
            lblFailureStep.Font = new Font("微软雅黑", 10F);
            lblFailureStep.ForeColor = Color.FromArgb(48, 48, 48);
            lblFailureStep.Location = new Point(360, 85);
            lblFailureStep.Name = "lblFailureStep";
            lblFailureStep.Size = new Size(80, 23);
            lblFailureStep.TabIndex = 8;
            lblFailureStep.Text = "失败步骤:";
            lblFailureStep.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numFailureStep
            // 
            numFailureStep.Font = new Font("微软雅黑", 10F);
            numFailureStep.Location = new Point(450, 83);
            numFailureStep.Margin = new Padding(4, 5, 4, 5);
            numFailureStep.Maximum = 1000D;
            numFailureStep.Minimum = -1D;
            numFailureStep.MinimumSize = new Size(100, 0);
            numFailureStep.Name = "numFailureStep";
            numFailureStep.Padding = new Padding(5);
            numFailureStep.Radius = 3;
            numFailureStep.RectColor = Color.FromArgb(216, 219, 227);
            numFailureStep.ShowText = false;
            numFailureStep.Size = new Size(150, 29);
            numFailureStep.Style = UIStyle.Custom;
            numFailureStep.TabIndex = 9;
            numFailureStep.Text = "-1";
            numFailureStep.TextAlignment = ContentAlignment.MiddleCenter;
            numFailureStep.Value = -1;
            // 
            // lblSuccessStep
            // 
            lblSuccessStep.BackColor = Color.Transparent;
            lblSuccessStep.Font = new Font("微软雅黑", 10F);
            lblSuccessStep.ForeColor = Color.FromArgb(48, 48, 48);
            lblSuccessStep.Location = new Point(630, 85);
            lblSuccessStep.Name = "lblSuccessStep";
            lblSuccessStep.Size = new Size(80, 23);
            lblSuccessStep.TabIndex = 10;
            lblSuccessStep.Text = "成功步骤:";
            lblSuccessStep.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numSuccessStep
            // 
            numSuccessStep.Font = new Font("微软雅黑", 10F);
            numSuccessStep.Location = new Point(720, 83);
            numSuccessStep.Margin = new Padding(4, 5, 4, 5);
            numSuccessStep.Maximum = 1000D;
            numSuccessStep.Minimum = -1D;
            numSuccessStep.MinimumSize = new Size(100, 0);
            numSuccessStep.Name = "numSuccessStep";
            numSuccessStep.Padding = new Padding(5);
            numSuccessStep.Radius = 3;
            numSuccessStep.RectColor = Color.FromArgb(216, 219, 227);
            numSuccessStep.ShowText = false;
            numSuccessStep.Size = new Size(130, 29);
            numSuccessStep.Style = UIStyle.Custom;
            numSuccessStep.TabIndex = 11;
            numSuccessStep.Text = "-1";
            numSuccessStep.TextAlignment = ContentAlignment.MiddleCenter;
            numSuccessStep.Value = -1;
            // 
            // chkShowResult
            // 
            chkShowResult.Checked = true;
            chkShowResult.Cursor = Cursors.Hand;
            chkShowResult.Font = new Font("微软雅黑", 10F);
            chkShowResult.ForeColor = Color.FromArgb(48, 48, 48);
            chkShowResult.Location = new Point(20, 130);
            chkShowResult.MinimumSize = new Size(1, 1);
            chkShowResult.Name = "chkShowResult";
            chkShowResult.Padding = new Padding(22, 0, 0, 0);
            chkShowResult.Size = new Size(200, 29);
            chkShowResult.Style = UIStyle.Custom;
            chkShowResult.TabIndex = 12;
            chkShowResult.Text = "显示检测结果消息";
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnOK);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnTest);
            pnlButtons.Controls.Add(btnHelp);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.FillColor = Color.FromArgb(248, 249, 250);
            pnlButtons.Font = new Font("微软雅黑", 12F);
            pnlButtons.Location = new Point(0, 818);
            pnlButtons.Margin = new Padding(4, 5, 4, 5);
            pnlButtons.MinimumSize = new Size(1, 1);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Padding = new Padding(10);
            pnlButtons.Radius = 0;
            pnlButtons.RectColor = Color.FromArgb(216, 219, 227);
            pnlButtons.RectSides = ToolStripStatusLabelBorderSides.Top;
            pnlButtons.Size = new Size(900, 60);
            pnlButtons.Style = UIStyle.Custom;
            pnlButtons.StyleCustomMode = true;
            pnlButtons.TabIndex = 1;
            pnlButtons.Text = null;
            pnlButtons.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.FillColor = Color.FromArgb(65, 100, 204);
            btnOK.Font = new Font("微软雅黑", 10F);
            btnOK.Location = new Point(480, 15);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.RectColor = Color.FromArgb(65, 100, 204);
            btnOK.Size = new Size(100, 35);
            btnOK.Style = UIStyle.Custom;
            btnOK.StyleCustomMode = true;
            btnOK.TabIndex = 0;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.ForeColor = Color.FromArgb(48, 48, 48);
            btnCancel.Location = new Point(590, 15);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(216, 219, 227);
            btnCancel.Size = new Size(100, 35);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.StyleCustomMode = true;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnTest
            // 
            btnTest.Cursor = Cursors.Hand;
            btnTest.FillColor = Color.FromArgb(52, 168, 83);
            btnTest.Font = new Font("微软雅黑", 10F);
            btnTest.Location = new Point(700, 15);
            btnTest.MinimumSize = new Size(1, 1);
            btnTest.Name = "btnTest";
            btnTest.RectColor = Color.FromArgb(52, 168, 83);
            btnTest.Size = new Size(90, 35);
            btnTest.Style = UIStyle.Custom;
            btnTest.StyleCustomMode = true;
            btnTest.TabIndex = 2;
            btnTest.Text = "测试";
            btnTest.TipsFont = new Font("微软雅黑", 9F);
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.FillColor = Color.White;
            btnHelp.Font = new Font("微软雅黑", 10F);
            btnHelp.ForeColor = Color.FromArgb(48, 48, 48);
            btnHelp.Location = new Point(800, 15);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.RectColor = Color.FromArgb(216, 219, 227);
            btnHelp.Size = new Size(90, 35);
            btnHelp.Style = UIStyle.Custom;
            btnHelp.StyleCustomMode = true;
            btnHelp.TabIndex = 3;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("微软雅黑", 9F);
            // 
            // pnlStatus
            // 
            pnlStatus.Controls.Add(lblStatus);
            pnlStatus.Controls.Add(lblStatusText);
            pnlStatus.Dock = DockStyle.Bottom;
            pnlStatus.FillColor = Color.White;
            pnlStatus.Font = new Font("微软雅黑", 12F);
            pnlStatus.Location = new Point(0, 878);
            pnlStatus.Margin = new Padding(4, 5, 4, 5);
            pnlStatus.MinimumSize = new Size(1, 1);
            pnlStatus.Name = "pnlStatus";
            pnlStatus.Padding = new Padding(10, 5, 10, 5);
            pnlStatus.Radius = 0;
            pnlStatus.RectColor = Color.FromArgb(216, 219, 227);
            pnlStatus.RectSides = ToolStripStatusLabelBorderSides.Top;
            pnlStatus.Size = new Size(900, 35);
            pnlStatus.Style = UIStyle.Custom;
            pnlStatus.StyleCustomMode = true;
            pnlStatus.TabIndex = 2;
            pnlStatus.Text = null;
            pnlStatus.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(48, 48, 48);
            lblStatus.Location = new Point(10, 7);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(60, 20);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "状态:";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStatusText
            // 
            lblStatusText.BackColor = Color.Transparent;
            lblStatusText.Font = new Font("微软雅黑", 9F);
            lblStatusText.ForeColor = Color.FromArgb(100, 100, 100);
            lblStatusText.Location = new Point(75, 7);
            lblStatusText.Name = "lblStatusText";
            lblStatusText.Size = new Size(815, 20);
            lblStatusText.TabIndex = 1;
            lblStatusText.Text = "就绪";
            lblStatusText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_Detection
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(900, 913);
            Controls.Add(pnlMain);
            Controls.Add(pnlButtons);
            Controls.Add(pnlStatus);
            Font = new Font("微软雅黑", 12F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Detection";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Text = "条件判断配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 900, 820);
            pnlMain.ResumeLayout(false);
            pnlBasicInfo.ResumeLayout(false);
            pnlDataSource.ResumeLayout(false);
            pnlVariableSource.ResumeLayout(false);
            pnlPlcSource.ResumeLayout(false);
            pnlDetectionCondition.ResumeLayout(false);
            pnlTimeout.ResumeLayout(false);
            pnlResultHandling.ResumeLayout(false);
            pnlButtons.ResumeLayout(false);
            pnlStatus.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private UIPanel pnlMain;
        private UIGroupBox pnlBasicInfo;
        private UILabel lblDetectionName;
        private UITextBox txtDetectionName;
        private UILabel lblDescription;
        private UITextBox txtDescription;
        private UILabel lblDetectionType;
        private UIComboBox cmbDetectionType;
        private UICheckBox chkEnabled;
        
        private UIGroupBox pnlDataSource;
        private UILabel lblDataSourceType;
        private UIComboBox cmbDataSourceType;
        private UIPanel pnlVariableSource;
        private UILabel lblVariableName;
        private UIComboBox CboVariableName;
        private UIPanel pnlPlcSource;
        private UILabel lblPlcModule;
        private UIComboBox CboPlcModule;
        private UILabel lblPlcAddress;
        private UIComboBox CboPlcAddress;
        
        private UIGroupBox pnlDetectionCondition;
        private UILabel lblOperator;
        private UIComboBox cmbOperator;
        private UILabel lblMinValue;
        private UIDoubleUpDown numMinValue;
        private UILabel lblMaxValue;
        private UIDoubleUpDown numMaxValue;
        private UILabel lblTargetValue;
        private UITextBox txtTargetValue;
        private UILabel lblTolerance;
        private UIDoubleUpDown numTolerance;
        private UILabel lblThreshold;
        private UIDoubleUpDown numThreshold;
        
        private UIGroupBox pnlTimeout;
        private UILabel lblTimeoutMs;
        private UIIntegerUpDown numTimeoutMs;
        private UILabel lblRetryCount;
        private UIIntegerUpDown numRetryCount;
        private UILabel lblRetryInterval;
        private UIIntegerUpDown numRetryIntervalMs;
        
        private UIGroupBox pnlResultHandling;
        private UICheckBox chkSaveResult;
        private UILabel lblResultVariable;
        private UIComboBox CboResultVariable;
        private UICheckBox chkSaveValue;
        private UILabel lblValueVariable;
        private UIComboBox CboValueVariable;
        private UILabel lblFailureAction;
        private UIComboBox cmbFailureAction;
        private UILabel lblFailureStep;
        private UIIntegerUpDown numFailureStep;
        private UILabel lblSuccessStep;
        private UIIntegerUpDown numSuccessStep;
        private UICheckBox chkShowResult;
        
        private UIPanel pnlButtons;
        private UIButton btnOK;
        private UIButton btnCancel;
        private UIButton btnTest;
        private UIButton btnHelp;
        
        private UIPanel pnlStatus;
        private UILabel lblStatus;
        private UILabel lblStatusText;
        
        private System.Windows.Forms.ToolTip toolTip;
    }
}
namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    partial class FrmCommandEditor : UIForm
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
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
            mainLayout = new TableLayoutPanel();
            lblName = new Label();
            txtName = new UITextBox();
            lblDisplayName = new Label();
            txtDisplayName = new UITextBox();
            lblCommandType = new Label();
            cboCommandType = new UIComboBox();
            lblDataType = new Label();
            cboDataType = new UIComboBox();
            lblTimeout = new Label();
            txtTimeout = new UITextBox();
            lblWaitResponse = new Label();
            chkWaitForResponse = new UICheckBox();
            lblRequestTemplate = new Label();
            txtRequestTemplate = new UITextBox();
            lblSuccessIndicator = new Label();
            txtSuccessIndicator = new UITextBox();
            lblFailureIndicator = new Label();
            txtFailureIndicator = new UITextBox();
            lblDescription = new Label();
            txtDescription = new UITextBox();
            lblParameters = new Label();
            grpParameters = new UILine();
            dgvParameters = new UIDataGridView();
            toolbarParams = new FlowLayoutPanel();
            btnAddParam = new UISymbolButton();
            btnDeleteParam = new UISymbolButton();
            lblParseRules = new Label();
            grpParseRules = new UILine();
            dgvParseRules = new UIDataGridView();
            toolbarRules = new FlowLayoutPanel();
            btnAddRule = new UISymbolButton();
            btnDeleteRule = new UISymbolButton();
            panelButtons = new Panel();
            btnOk = new UISymbolButton();
            btnCancel = new UISymbolButton();
            mainLayout.SuspendLayout();
            grpParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvParameters).BeginInit();
            toolbarParams.SuspendLayout();
            grpParseRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvParseRules).BeginInit();
            toolbarRules.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 4;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayout.Controls.Add(lblName, 0, 0);
            mainLayout.Controls.Add(txtName, 1, 0);
            mainLayout.Controls.Add(lblDisplayName, 2, 0);
            mainLayout.Controls.Add(txtDisplayName, 3, 0);
            mainLayout.Controls.Add(lblCommandType, 0, 1);
            mainLayout.Controls.Add(cboCommandType, 1, 1);
            mainLayout.Controls.Add(lblDataType, 2, 1);
            mainLayout.Controls.Add(cboDataType, 3, 1);
            mainLayout.Controls.Add(lblTimeout, 0, 2);
            mainLayout.Controls.Add(txtTimeout, 1, 2);
            mainLayout.Controls.Add(lblWaitResponse, 2, 2);
            mainLayout.Controls.Add(chkWaitForResponse, 3, 2);
            mainLayout.Controls.Add(lblRequestTemplate, 0, 3);
            mainLayout.Controls.Add(txtRequestTemplate, 1, 3);
            mainLayout.Controls.Add(lblSuccessIndicator, 0, 4);
            mainLayout.Controls.Add(txtSuccessIndicator, 1, 4);
            mainLayout.Controls.Add(lblFailureIndicator, 2, 4);
            mainLayout.Controls.Add(txtFailureIndicator, 3, 4);
            mainLayout.Controls.Add(lblDescription, 0, 5);
            mainLayout.Controls.Add(txtDescription, 1, 5);
            mainLayout.Controls.Add(lblParameters, 0, 6);
            mainLayout.Controls.Add(grpParameters, 1, 6);
            mainLayout.Controls.Add(lblParseRules, 0, 7);
            mainLayout.Controls.Add(grpParseRules, 1, 7);
            mainLayout.Controls.Add(panelButtons, 0, 8);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 35);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(15);
            mainLayout.RowCount = 9;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            mainLayout.Size = new Size(848, 750);
            mainLayout.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("微软雅黑", 11F);
            lblName.Location = new Point(18, 15);
            lblName.Name = "lblName";
            lblName.Size = new Size(94, 40);
            lblName.TabIndex = 0;
            lblName.Text = "命令名称*:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Fill;
            txtName.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtName.Location = new Point(119, 20);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.MinimumSize = new Size(1, 16);
            txtName.Name = "txtName";
            txtName.Padding = new Padding(5);
            txtName.ShowText = false;
            txtName.Size = new Size(301, 30);
            txtName.TabIndex = 1;
            txtName.TextAlignment = ContentAlignment.MiddleLeft;
            txtName.Watermark = "英文标识符";
            // 
            // lblDisplayName
            // 
            lblDisplayName.Dock = DockStyle.Fill;
            lblDisplayName.Font = new Font("微软雅黑", 11F);
            lblDisplayName.Location = new Point(427, 15);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(94, 40);
            lblDisplayName.TabIndex = 2;
            lblDisplayName.Text = "显示名称*:";
            lblDisplayName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDisplayName
            // 
            txtDisplayName.Dock = DockStyle.Fill;
            txtDisplayName.Font = new Font("微软雅黑", 12F);
            txtDisplayName.Location = new Point(528, 20);
            txtDisplayName.Margin = new Padding(4, 5, 4, 5);
            txtDisplayName.MinimumSize = new Size(1, 16);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Padding = new Padding(5);
            txtDisplayName.ShowText = false;
            txtDisplayName.Size = new Size(301, 30);
            txtDisplayName.TabIndex = 3;
            txtDisplayName.TextAlignment = ContentAlignment.MiddleLeft;
            txtDisplayName.Watermark = "中文显示名称";
            // 
            // lblCommandType
            // 
            lblCommandType.Dock = DockStyle.Fill;
            lblCommandType.Font = new Font("微软雅黑", 11F);
            lblCommandType.Location = new Point(18, 55);
            lblCommandType.Name = "lblCommandType";
            lblCommandType.Size = new Size(94, 40);
            lblCommandType.TabIndex = 4;
            lblCommandType.Text = "命令类型:";
            lblCommandType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboCommandType
            // 
            cboCommandType.DataSource = null;
            cboCommandType.Dock = DockStyle.Fill;
            cboCommandType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboCommandType.FillColor = Color.White;
            cboCommandType.Font = new Font("微软雅黑", 12F);
            cboCommandType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboCommandType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboCommandType.Location = new Point(119, 60);
            cboCommandType.Margin = new Padding(4, 5, 4, 5);
            cboCommandType.MinimumSize = new Size(63, 0);
            cboCommandType.Name = "cboCommandType";
            cboCommandType.Padding = new Padding(0, 0, 30, 2);
            cboCommandType.Size = new Size(301, 30);
            cboCommandType.SymbolSize = 24;
            cboCommandType.TabIndex = 5;
            cboCommandType.TextAlignment = ContentAlignment.MiddleLeft;
            cboCommandType.Watermark = "";
            // 
            // lblDataType
            // 
            lblDataType.Dock = DockStyle.Fill;
            lblDataType.Font = new Font("微软雅黑", 11F);
            lblDataType.Location = new Point(427, 55);
            lblDataType.Name = "lblDataType";
            lblDataType.Size = new Size(94, 40);
            lblDataType.TabIndex = 6;
            lblDataType.Text = "数据类型:";
            lblDataType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboDataType
            // 
            cboDataType.DataSource = null;
            cboDataType.Dock = DockStyle.Fill;
            cboDataType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboDataType.FillColor = Color.White;
            cboDataType.Font = new Font("微软雅黑", 12F);
            cboDataType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboDataType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboDataType.Location = new Point(528, 60);
            cboDataType.Margin = new Padding(4, 5, 4, 5);
            cboDataType.MinimumSize = new Size(63, 0);
            cboDataType.Name = "cboDataType";
            cboDataType.Padding = new Padding(0, 0, 30, 2);
            cboDataType.Size = new Size(301, 30);
            cboDataType.SymbolSize = 24;
            cboDataType.TabIndex = 7;
            cboDataType.TextAlignment = ContentAlignment.MiddleLeft;
            cboDataType.Watermark = "";
            // 
            // lblTimeout
            // 
            lblTimeout.Dock = DockStyle.Fill;
            lblTimeout.Font = new Font("微软雅黑", 11F);
            lblTimeout.Location = new Point(18, 95);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(94, 40);
            lblTimeout.TabIndex = 8;
            lblTimeout.Text = "超时(ms):";
            lblTimeout.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTimeout
            // 
            txtTimeout.Dock = DockStyle.Fill;
            txtTimeout.DoubleValue = 3000D;
            txtTimeout.Font = new Font("微软雅黑", 12F);
            txtTimeout.IntValue = 3000;
            txtTimeout.Location = new Point(119, 100);
            txtTimeout.Margin = new Padding(4, 5, 4, 5);
            txtTimeout.MinimumSize = new Size(1, 16);
            txtTimeout.Name = "txtTimeout";
            txtTimeout.Padding = new Padding(5);
            txtTimeout.ShowText = false;
            txtTimeout.Size = new Size(301, 30);
            txtTimeout.TabIndex = 9;
            txtTimeout.Text = "3000";
            txtTimeout.TextAlignment = ContentAlignment.MiddleLeft;
            txtTimeout.Watermark = "";
            // 
            // lblWaitResponse
            // 
            lblWaitResponse.Dock = DockStyle.Fill;
            lblWaitResponse.Font = new Font("微软雅黑", 11F);
            lblWaitResponse.Location = new Point(427, 95);
            lblWaitResponse.Name = "lblWaitResponse";
            lblWaitResponse.Size = new Size(94, 40);
            lblWaitResponse.TabIndex = 10;
            lblWaitResponse.Text = "等待响应:";
            lblWaitResponse.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkWaitForResponse
            // 
            chkWaitForResponse.Checked = true;
            chkWaitForResponse.Dock = DockStyle.Fill;
            chkWaitForResponse.Font = new Font("微软雅黑", 12F);
            chkWaitForResponse.ForeColor = Color.FromArgb(48, 48, 48);
            chkWaitForResponse.Location = new Point(527, 98);
            chkWaitForResponse.MinimumSize = new Size(1, 1);
            chkWaitForResponse.Name = "chkWaitForResponse";
            chkWaitForResponse.Size = new Size(303, 34);
            chkWaitForResponse.TabIndex = 11;
            chkWaitForResponse.Text = "等待仪器响应";
            // 
            // lblRequestTemplate
            // 
            lblRequestTemplate.Dock = DockStyle.Fill;
            lblRequestTemplate.Font = new Font("微软雅黑", 11F);
            lblRequestTemplate.Location = new Point(18, 135);
            lblRequestTemplate.Name = "lblRequestTemplate";
            lblRequestTemplate.Padding = new Padding(0, 10, 0, 0);
            lblRequestTemplate.Size = new Size(94, 80);
            lblRequestTemplate.TabIndex = 12;
            lblRequestTemplate.Text = "请求模板*:";
            lblRequestTemplate.TextAlign = ContentAlignment.TopRight;
            // 
            // txtRequestTemplate
            // 
            mainLayout.SetColumnSpan(txtRequestTemplate, 3);
            txtRequestTemplate.Dock = DockStyle.Fill;
            txtRequestTemplate.Font = new Font("微软雅黑", 12F);
            txtRequestTemplate.Location = new Point(119, 140);
            txtRequestTemplate.Margin = new Padding(4, 5, 4, 5);
            txtRequestTemplate.MinimumSize = new Size(1, 16);
            txtRequestTemplate.Multiline = true;
            txtRequestTemplate.Name = "txtRequestTemplate";
            txtRequestTemplate.Padding = new Padding(5);
            txtRequestTemplate.ShowText = false;
            txtRequestTemplate.Size = new Size(710, 70);
            txtRequestTemplate.TabIndex = 13;
            txtRequestTemplate.TextAlignment = ContentAlignment.MiddleLeft;
            txtRequestTemplate.Watermark = "如: MEAS:VOLT:DC? 或带参数: APPL {Channel},{Voltage}";
            // 
            // lblSuccessIndicator
            // 
            lblSuccessIndicator.Dock = DockStyle.Fill;
            lblSuccessIndicator.Font = new Font("微软雅黑", 11F);
            lblSuccessIndicator.Location = new Point(18, 215);
            lblSuccessIndicator.Name = "lblSuccessIndicator";
            lblSuccessIndicator.Size = new Size(94, 40);
            lblSuccessIndicator.TabIndex = 14;
            lblSuccessIndicator.Text = "成功标志:";
            lblSuccessIndicator.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtSuccessIndicator
            // 
            txtSuccessIndicator.Dock = DockStyle.Fill;
            txtSuccessIndicator.Font = new Font("微软雅黑", 12F);
            txtSuccessIndicator.Location = new Point(119, 220);
            txtSuccessIndicator.Margin = new Padding(4, 5, 4, 5);
            txtSuccessIndicator.MinimumSize = new Size(1, 16);
            txtSuccessIndicator.Name = "txtSuccessIndicator";
            txtSuccessIndicator.Padding = new Padding(5);
            txtSuccessIndicator.ShowText = false;
            txtSuccessIndicator.Size = new Size(301, 30);
            txtSuccessIndicator.TabIndex = 15;
            txtSuccessIndicator.TextAlignment = ContentAlignment.MiddleLeft;
            txtSuccessIndicator.Watermark = "响应中包含此内容表示成功";
            // 
            // lblFailureIndicator
            // 
            lblFailureIndicator.Dock = DockStyle.Fill;
            lblFailureIndicator.Font = new Font("微软雅黑", 11F);
            lblFailureIndicator.Location = new Point(427, 215);
            lblFailureIndicator.Name = "lblFailureIndicator";
            lblFailureIndicator.Size = new Size(94, 40);
            lblFailureIndicator.TabIndex = 16;
            lblFailureIndicator.Text = "失败标志:";
            lblFailureIndicator.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtFailureIndicator
            // 
            txtFailureIndicator.Dock = DockStyle.Fill;
            txtFailureIndicator.Font = new Font("微软雅黑", 12F);
            txtFailureIndicator.Location = new Point(528, 220);
            txtFailureIndicator.Margin = new Padding(4, 5, 4, 5);
            txtFailureIndicator.MinimumSize = new Size(1, 16);
            txtFailureIndicator.Name = "txtFailureIndicator";
            txtFailureIndicator.Padding = new Padding(5);
            txtFailureIndicator.ShowText = false;
            txtFailureIndicator.Size = new Size(301, 30);
            txtFailureIndicator.TabIndex = 17;
            txtFailureIndicator.TextAlignment = ContentAlignment.MiddleLeft;
            txtFailureIndicator.Watermark = "响应中包含此内容表示失败";
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Font = new Font("微软雅黑", 11F);
            lblDescription.Location = new Point(18, 255);
            lblDescription.Name = "lblDescription";
            lblDescription.Padding = new Padding(0, 10, 0, 0);
            lblDescription.Size = new Size(94, 60);
            lblDescription.TabIndex = 18;
            lblDescription.Text = "描述:";
            lblDescription.TextAlign = ContentAlignment.TopRight;
            // 
            // txtDescription
            // 
            mainLayout.SetColumnSpan(txtDescription, 3);
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Font = new Font("微软雅黑", 12F);
            txtDescription.Location = new Point(119, 260);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.MinimumSize = new Size(1, 16);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Padding = new Padding(5);
            txtDescription.ShowText = false;
            txtDescription.Size = new Size(710, 50);
            txtDescription.TabIndex = 19;
            txtDescription.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescription.Watermark = "";
            // 
            // lblParameters
            // 
            lblParameters.Dock = DockStyle.Fill;
            lblParameters.Font = new Font("微软雅黑", 11F);
            lblParameters.Location = new Point(18, 315);
            lblParameters.Name = "lblParameters";
            lblParameters.Padding = new Padding(0, 10, 0, 0);
            lblParameters.Size = new Size(94, 185);
            lblParameters.TabIndex = 20;
            lblParameters.Text = "参数定义:";
            lblParameters.TextAlign = ContentAlignment.TopRight;
            // 
            // grpParameters
            // 
            grpParameters.BackColor = Color.Transparent;
            mainLayout.SetColumnSpan(grpParameters, 3);
            grpParameters.Controls.Add(dgvParameters);
            grpParameters.Controls.Add(toolbarParams);
            grpParameters.Dock = DockStyle.Fill;
            grpParameters.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpParameters.ForeColor = Color.FromArgb(48, 48, 48);
            grpParameters.Location = new Point(119, 320);
            grpParameters.Margin = new Padding(4, 5, 4, 5);
            grpParameters.MinimumSize = new Size(1, 1);
            grpParameters.Name = "grpParameters";
            grpParameters.Padding = new Padding(0, 32, 0, 0);
            grpParameters.Size = new Size(710, 175);
            grpParameters.TabIndex = 21;
            grpParameters.Text = "命令参数列表";
            // 
            // dgvParameters
            // 
            dgvParameters.AllowUserToAddRows = false;
            dgvParameters.AllowUserToDeleteRows = false;
            dataGridViewCellStyle11.BackColor = Color.FromArgb(235, 243, 255);
            dgvParameters.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            dgvParameters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvParameters.BackgroundColor = Color.White;
            dgvParameters.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle12.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle12.ForeColor = Color.White;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
            dgvParameters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            dgvParameters.ColumnHeadersHeight = 32;
            dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = SystemColors.Window;
            dataGridViewCellStyle13.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle13.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle13.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = DataGridViewTriState.False;
            dgvParameters.DefaultCellStyle = dataGridViewCellStyle13;
            dgvParameters.Dock = DockStyle.Fill;
            dgvParameters.EnableHeadersVisualStyles = false;
            dgvParameters.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvParameters.GridColor = Color.FromArgb(80, 160, 255);
            dgvParameters.Location = new Point(0, 35);
            dgvParameters.Name = "dgvParameters";
            dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle14.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle14.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle14.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle14.SelectionForeColor = Color.White;
            dataGridViewCellStyle14.WrapMode = DataGridViewTriState.True;
            dgvParameters.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            dataGridViewCellStyle15.BackColor = Color.White;
            dataGridViewCellStyle15.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvParameters.RowsDefaultCellStyle = dataGridViewCellStyle15;
            dgvParameters.SelectedIndex = -1;
            dgvParameters.Size = new Size(710, 140);
            dgvParameters.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvParameters.TabIndex = 0;
            // 
            // toolbarParams
            // 
            toolbarParams.Controls.Add(btnAddParam);
            toolbarParams.Controls.Add(btnDeleteParam);
            toolbarParams.Dock = DockStyle.Top;
            toolbarParams.Location = new Point(0, 0);
            toolbarParams.Name = "toolbarParams";
            toolbarParams.Size = new Size(710, 35);
            toolbarParams.TabIndex = 1;
            // 
            // btnAddParam
            // 
            btnAddParam.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddParam.Location = new Point(2, 2);
            btnAddParam.Margin = new Padding(2);
            btnAddParam.MinimumSize = new Size(1, 1);
            btnAddParam.Name = "btnAddParam";
            btnAddParam.Size = new Size(80, 28);
            btnAddParam.Symbol = 61543;
            btnAddParam.TabIndex = 0;
            btnAddParam.Text = "添加";
            btnAddParam.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnDeleteParam
            // 
            btnDeleteParam.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDeleteParam.Location = new Point(86, 2);
            btnDeleteParam.Margin = new Padding(2);
            btnDeleteParam.MinimumSize = new Size(1, 1);
            btnDeleteParam.Name = "btnDeleteParam";
            btnDeleteParam.Size = new Size(80, 28);
            btnDeleteParam.Symbol = 61460;
            btnDeleteParam.TabIndex = 1;
            btnDeleteParam.Text = "删除";
            btnDeleteParam.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // lblParseRules
            // 
            lblParseRules.Dock = DockStyle.Fill;
            lblParseRules.Font = new Font("微软雅黑", 11F);
            lblParseRules.Location = new Point(18, 500);
            lblParseRules.Name = "lblParseRules";
            lblParseRules.Padding = new Padding(0, 10, 0, 0);
            lblParseRules.Size = new Size(94, 185);
            lblParseRules.TabIndex = 22;
            lblParseRules.Text = "解析规则:";
            lblParseRules.TextAlign = ContentAlignment.TopRight;
            // 
            // grpParseRules
            // 
            mainLayout.SetColumnSpan(grpParseRules, 3);
            grpParseRules.Controls.Add(dgvParseRules);
            grpParseRules.Controls.Add(toolbarRules);
            grpParseRules.Dock = DockStyle.Fill;
            grpParseRules.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpParseRules.Location = new Point(119, 505);
            grpParseRules.Margin = new Padding(4, 5, 4, 5);
            grpParseRules.MinimumSize = new Size(1, 1);
            grpParseRules.Name = "grpParseRules";
            grpParseRules.Padding = new Padding(0, 32, 0, 0);
            grpParseRules.Size = new Size(710, 175);
            grpParseRules.TabIndex = 23;
            grpParseRules.Text = "响应解析规则";
            // 
            // dgvParseRules
            // 
            dgvParseRules.AllowUserToAddRows = false;
            dgvParseRules.AllowUserToDeleteRows = false;
            dataGridViewCellStyle16.BackColor = Color.FromArgb(235, 243, 255);
            dgvParseRules.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            dgvParseRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvParseRules.BackgroundColor = Color.White;
            dgvParseRules.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle17.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle17.ForeColor = Color.White;
            dataGridViewCellStyle17.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = DataGridViewTriState.True;
            dgvParseRules.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            dgvParseRules.ColumnHeadersHeight = 32;
            dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = SystemColors.Window;
            dataGridViewCellStyle18.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle18.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle18.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = DataGridViewTriState.False;
            dgvParseRules.DefaultCellStyle = dataGridViewCellStyle18;
            dgvParseRules.Dock = DockStyle.Fill;
            dgvParseRules.EnableHeadersVisualStyles = false;
            dgvParseRules.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvParseRules.GridColor = Color.FromArgb(80, 160, 255);
            dgvParseRules.Location = new Point(0, 67);
            dgvParseRules.Name = "dgvParseRules";
            dataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle19.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle19.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle19.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle19.SelectionForeColor = Color.White;
            dataGridViewCellStyle19.WrapMode = DataGridViewTriState.True;
            dgvParseRules.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            dataGridViewCellStyle20.BackColor = Color.White;
            dataGridViewCellStyle20.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvParseRules.RowsDefaultCellStyle = dataGridViewCellStyle20;
            dgvParseRules.SelectedIndex = -1;
            dgvParseRules.Size = new Size(710, 108);
            dgvParseRules.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvParseRules.TabIndex = 0;
            // 
            // toolbarRules
            // 
            toolbarRules.Controls.Add(btnAddRule);
            toolbarRules.Controls.Add(btnDeleteRule);
            toolbarRules.Dock = DockStyle.Top;
            toolbarRules.Location = new Point(0, 32);
            toolbarRules.Name = "toolbarRules";
            toolbarRules.Size = new Size(710, 35);
            toolbarRules.TabIndex = 1;
            // 
            // btnAddRule
            // 
            btnAddRule.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddRule.Location = new Point(2, 2);
            btnAddRule.Margin = new Padding(2);
            btnAddRule.MinimumSize = new Size(1, 1);
            btnAddRule.Name = "btnAddRule";
            btnAddRule.Size = new Size(80, 28);
            btnAddRule.Symbol = 61543;
            btnAddRule.TabIndex = 0;
            btnAddRule.Text = "添加";
            btnAddRule.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnDeleteRule
            // 
            btnDeleteRule.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDeleteRule.Location = new Point(86, 2);
            btnDeleteRule.Margin = new Padding(2);
            btnDeleteRule.MinimumSize = new Size(1, 1);
            btnDeleteRule.Name = "btnDeleteRule";
            btnDeleteRule.Size = new Size(80, 28);
            btnDeleteRule.Symbol = 61460;
            btnDeleteRule.TabIndex = 1;
            btnDeleteRule.Text = "删除";
            btnDeleteRule.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // panelButtons
            // 
            mainLayout.SetColumnSpan(panelButtons, 4);
            panelButtons.Controls.Add(btnOk);
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Location = new Point(18, 688);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(812, 44);
            panelButtons.TabIndex = 24;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Right;
            btnOk.FillColor = Color.FromArgb(0, 150, 136);
            btnOk.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOk.Location = new Point(590, 6);
            btnOk.MinimumSize = new Size(1, 1);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 35);
            btnOk.Symbol = 61452;
            btnOk.TabIndex = 0;
            btnOk.Text = "确定";
            btnOk.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Right;
            btnCancel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Location = new Point(696, 6);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // FrmCommandEditor
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(848, 785);
            Controls.Add(mainLayout);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCommandEditor";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "命令模板编辑";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 700, 650);
            mainLayout.ResumeLayout(false);
            grpParameters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvParameters).EndInit();
            toolbarParams.ResumeLayout(false);
            grpParseRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvParseRules).EndInit();
            toolbarRules.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region 控件声明

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel panelButtons;

        // 基本信息
        private System.Windows.Forms.Label lblName;
        private Sunny.UI.UITextBox txtName;
        private System.Windows.Forms.Label lblDisplayName;
        private Sunny.UI.UITextBox txtDisplayName;
        private System.Windows.Forms.Label lblCommandType;
        private Sunny.UI.UIComboBox cboCommandType;
        private System.Windows.Forms.Label lblDataType;
        private Sunny.UI.UIComboBox cboDataType;
        private System.Windows.Forms.Label lblTimeout;
        private Sunny.UI.UITextBox txtTimeout;
        private System.Windows.Forms.Label lblWaitResponse;
        private Sunny.UI.UICheckBox chkWaitForResponse;
        private System.Windows.Forms.Label lblRequestTemplate;
        private Sunny.UI.UITextBox txtRequestTemplate;
        private System.Windows.Forms.Label lblSuccessIndicator;
        private Sunny.UI.UITextBox txtSuccessIndicator;
        private System.Windows.Forms.Label lblFailureIndicator;
        private Sunny.UI.UITextBox txtFailureIndicator;
        private System.Windows.Forms.Label lblDescription;
        private Sunny.UI.UITextBox txtDescription;

        // 参数配置
        private System.Windows.Forms.Label lblParameters;
        private Sunny.UI.UILine grpParameters;
        private Sunny.UI.UIDataGridView dgvParameters;
        private System.Windows.Forms.FlowLayoutPanel toolbarParams;
        private Sunny.UI.UISymbolButton btnAddParam;
        private Sunny.UI.UISymbolButton btnDeleteParam;

        // 解析规则
        private System.Windows.Forms.Label lblParseRules;
        private Sunny.UI.UILine grpParseRules;
        private Sunny.UI.UIDataGridView dgvParseRules;
        private System.Windows.Forms.FlowLayoutPanel toolbarRules;
        private Sunny.UI.UISymbolButton btnAddRule;
        private Sunny.UI.UISymbolButton btnDeleteRule;

        // 按钮
        private Sunny.UI.UISymbolButton btnOk;
        private Sunny.UI.UISymbolButton btnCancel;

        #endregion
    }
}
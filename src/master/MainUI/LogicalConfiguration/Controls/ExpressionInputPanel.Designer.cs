namespace MainUI.LogicalConfiguration.Controls
{
    partial class ExpressionInputPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _validationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _mainPanel = new Panel();
            _contentPanel = new Panel();
            _keyboardPanel = new Panel();
            _keyboardGrid = new TableLayoutPanel();
            _sourcePanel = new Panel();
            _btnConstant = new Button();
            _btnFunction = new Button();
            _btnSystem = new Button();
            _btnExpression = new Button();
            _btnVariable = new Button();
            _btnPLC = new Button();
            _topPanel = new Panel();
            _statusPanel = new Panel();
            _previewLabel = new Label();
            _validationLabel = new Label();
            _expressionTextBox = new UITextBox();
            _validationTimer = new System.Windows.Forms.Timer(components);
            _mainPanel.SuspendLayout();
            _contentPanel.SuspendLayout();
            _keyboardPanel.SuspendLayout();
            _sourcePanel.SuspendLayout();
            _topPanel.SuspendLayout();
            _statusPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _mainPanel
            // 
            _mainPanel.BackColor = Color.White;
            _mainPanel.Controls.Add(_contentPanel);
            _mainPanel.Controls.Add(_topPanel);
            _mainPanel.Dock = DockStyle.Fill;
            _mainPanel.Location = new Point(1, 1);
            _mainPanel.Name = "_mainPanel";
            _mainPanel.Padding = new Padding(12);
            _mainPanel.Size = new Size(845, 396);
            _mainPanel.TabIndex = 0;
            // 
            // _contentPanel
            // 
            _contentPanel.BackColor = Color.White;
            _contentPanel.Controls.Add(_keyboardPanel);
            _contentPanel.Controls.Add(_sourcePanel);
            _contentPanel.Dock = DockStyle.Fill;
            _contentPanel.Location = new Point(12, 80);
            _contentPanel.Name = "_contentPanel";
            _contentPanel.Padding = new Padding(0, 10, 0, 0);
            _contentPanel.Size = new Size(821, 304);
            _contentPanel.TabIndex = 1;
            // 
            // _keyboardPanel
            // 
            _keyboardPanel.BackColor = Color.White;
            _keyboardPanel.Controls.Add(_keyboardGrid);
            _keyboardPanel.Dock = DockStyle.Fill;
            _keyboardPanel.Location = new Point(152, 10);
            _keyboardPanel.Name = "_keyboardPanel";
            _keyboardPanel.Padding = new Padding(8, 0, 0, 0);
            _keyboardPanel.Size = new Size(669, 294);
            _keyboardPanel.TabIndex = 1;
            // 
            // _keyboardGrid
            // 
            _keyboardGrid.BackColor = Color.White;
            _keyboardGrid.ColumnCount = 10;
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            _keyboardGrid.Dock = DockStyle.Fill;
            _keyboardGrid.Location = new Point(8, 0);
            _keyboardGrid.Name = "_keyboardGrid";
            _keyboardGrid.Padding = new Padding(4);
            _keyboardGrid.RowCount = 5;
            _keyboardGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            _keyboardGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            _keyboardGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            _keyboardGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            _keyboardGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            _keyboardGrid.Size = new Size(661, 294);
            _keyboardGrid.TabIndex = 0;
            // 
            // _sourcePanel
            // 
            _sourcePanel.BackColor = Color.FromArgb(250, 250, 250);
            _sourcePanel.Controls.Add(_btnConstant);
            _sourcePanel.Controls.Add(_btnFunction);
            _sourcePanel.Controls.Add(_btnSystem);
            _sourcePanel.Controls.Add(_btnExpression);
            _sourcePanel.Controls.Add(_btnVariable);
            _sourcePanel.Controls.Add(_btnPLC);
            _sourcePanel.Dock = DockStyle.Left;
            _sourcePanel.Location = new Point(0, 10);
            _sourcePanel.Name = "_sourcePanel";
            _sourcePanel.Padding = new Padding(8);
            _sourcePanel.Size = new Size(152, 294);
            _sourcePanel.TabIndex = 0;
            // 
            // _btnConstant
            // 
            _btnConstant.BackColor = Color.White;
            _btnConstant.Cursor = Cursors.Hand;
            _btnConstant.FlatAppearance.BorderColor = Color.FromArgb(217, 217, 217);
            _btnConstant.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 225, 235);
            _btnConstant.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 235, 241);
            _btnConstant.FlatStyle = FlatStyle.Flat;
            _btnConstant.Font = new Font("微软雅黑", 9.5F);
            _btnConstant.ForeColor = Color.FromArgb(38, 38, 38);
            _btnConstant.Location = new Point(8, 244);
            _btnConstant.Name = "_btnConstant";
            _btnConstant.Padding = new Padding(10, 0, 8, 0);
            _btnConstant.Size = new Size(136, 36);
            _btnConstant.TabIndex = 5;
            _btnConstant.Text = "»  常量输入";
            _btnConstant.TextAlign = ContentAlignment.MiddleLeft;
            _btnConstant.UseVisualStyleBackColor = false;
            _btnConstant.Click += BtnConstant_Click;
            _btnConstant.MouseEnter += SourceButton_MouseEnter;
            _btnConstant.MouseLeave += SourceButton_MouseLeave;
            // 
            // _btnFunction
            // 
            _btnFunction.BackColor = Color.White;
            _btnFunction.Cursor = Cursors.Hand;
            _btnFunction.FlatAppearance.BorderColor = Color.FromArgb(217, 217, 217);
            _btnFunction.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 225, 235);
            _btnFunction.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 235, 241);
            _btnFunction.FlatStyle = FlatStyle.Flat;
            _btnFunction.Font = new Font("微软雅黑", 9.5F);
            _btnFunction.ForeColor = Color.FromArgb(38, 38, 38);
            _btnFunction.Location = new Point(8, 198);
            _btnFunction.Name = "_btnFunction";
            _btnFunction.Padding = new Padding(10, 0, 8, 0);
            _btnFunction.Size = new Size(136, 36);
            _btnFunction.TabIndex = 4;
            _btnFunction.Text = "»  函数选择";
            _btnFunction.TextAlign = ContentAlignment.MiddleLeft;
            _btnFunction.UseVisualStyleBackColor = false;
            _btnFunction.Click += BtnFunction_Click;
            _btnFunction.MouseEnter += SourceButton_MouseEnter;
            _btnFunction.MouseLeave += SourceButton_MouseLeave;
            // 
            // _btnSystem
            // 
            _btnSystem.BackColor = Color.White;
            _btnSystem.Cursor = Cursors.Hand;
            _btnSystem.FlatAppearance.BorderColor = Color.FromArgb(217, 217, 217);
            _btnSystem.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 225, 235);
            _btnSystem.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 235, 241);
            _btnSystem.FlatStyle = FlatStyle.Flat;
            _btnSystem.Font = new Font("微软雅黑", 9.5F);
            _btnSystem.ForeColor = Color.FromArgb(38, 38, 38);
            _btnSystem.Location = new Point(8, 152);
            _btnSystem.Name = "_btnSystem";
            _btnSystem.Padding = new Padding(10, 0, 8, 0);
            _btnSystem.Size = new Size(136, 36);
            _btnSystem.TabIndex = 3;
            _btnSystem.Text = "»  系统属性";
            _btnSystem.TextAlign = ContentAlignment.MiddleLeft;
            _btnSystem.UseVisualStyleBackColor = false;
            _btnSystem.Click += BtnSystem_Click;
            _btnSystem.MouseEnter += SourceButton_MouseEnter;
            _btnSystem.MouseLeave += SourceButton_MouseLeave;
            // 
            // _btnExpression
            // 
            _btnExpression.BackColor = Color.White;
            _btnExpression.Cursor = Cursors.Hand;
            _btnExpression.FlatAppearance.BorderColor = Color.FromArgb(217, 217, 217);
            _btnExpression.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 225, 235);
            _btnExpression.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 235, 241);
            _btnExpression.FlatStyle = FlatStyle.Flat;
            _btnExpression.Font = new Font("微软雅黑", 9.5F);
            _btnExpression.ForeColor = Color.FromArgb(38, 38, 38);
            _btnExpression.Location = new Point(8, 106);
            _btnExpression.Name = "_btnExpression";
            _btnExpression.Padding = new Padding(10, 0, 8, 0);
            _btnExpression.Size = new Size(136, 36);
            _btnExpression.TabIndex = 2;
            _btnExpression.Text = "»  表达式";
            _btnExpression.TextAlign = ContentAlignment.MiddleLeft;
            _btnExpression.UseVisualStyleBackColor = false;
            _btnExpression.Click += BtnExpression_Click;
            _btnExpression.MouseEnter += SourceButton_MouseEnter;
            _btnExpression.MouseLeave += SourceButton_MouseLeave;
            // 
            // _btnVariable
            // 
            _btnVariable.BackColor = Color.White;
            _btnVariable.Cursor = Cursors.Hand;
            _btnVariable.FlatAppearance.BorderColor = Color.FromArgb(217, 217, 217);
            _btnVariable.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 225, 235);
            _btnVariable.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 235, 241);
            _btnVariable.FlatStyle = FlatStyle.Flat;
            _btnVariable.Font = new Font("微软雅黑", 9.5F);
            _btnVariable.ForeColor = Color.FromArgb(38, 38, 38);
            _btnVariable.Location = new Point(8, 60);
            _btnVariable.Name = "_btnVariable";
            _btnVariable.Padding = new Padding(10, 0, 8, 0);
            _btnVariable.Size = new Size(136, 36);
            _btnVariable.TabIndex = 1;
            _btnVariable.Text = "»  变量选择";
            _btnVariable.TextAlign = ContentAlignment.MiddleLeft;
            _btnVariable.UseVisualStyleBackColor = false;
            _btnVariable.Click += BtnVariable_Click;
            _btnVariable.MouseEnter += SourceButton_MouseEnter;
            _btnVariable.MouseLeave += SourceButton_MouseLeave;
            // 
            // _btnPLC
            // 
            _btnPLC.BackColor = Color.White;
            _btnPLC.Cursor = Cursors.Hand;
            _btnPLC.FlatAppearance.BorderColor = Color.FromArgb(217, 217, 217);
            _btnPLC.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 225, 235);
            _btnPLC.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 235, 241);
            _btnPLC.FlatStyle = FlatStyle.Flat;
            _btnPLC.Font = new Font("微软雅黑", 9.5F);
            _btnPLC.ForeColor = Color.FromArgb(38, 38, 38);
            _btnPLC.Location = new Point(8, 14);
            _btnPLC.Name = "_btnPLC";
            _btnPLC.Padding = new Padding(10, 0, 8, 0);
            _btnPLC.Size = new Size(136, 36);
            _btnPLC.TabIndex = 0;
            _btnPLC.Text = "»  PLC地址";
            _btnPLC.TextAlign = ContentAlignment.MiddleLeft;
            _btnPLC.UseVisualStyleBackColor = false;
            _btnPLC.Click += BtnPLC_Click;
            _btnPLC.MouseEnter += SourceButton_MouseEnter;
            _btnPLC.MouseLeave += SourceButton_MouseLeave;
            // 
            // _topPanel
            // 
            _topPanel.BackColor = Color.White;
            _topPanel.Controls.Add(_statusPanel);
            _topPanel.Controls.Add(_expressionTextBox);
            _topPanel.Dock = DockStyle.Top;
            _topPanel.Location = new Point(12, 12);
            _topPanel.Name = "_topPanel";
            _topPanel.Padding = new Padding(0, 0, 0, 8);
            _topPanel.Size = new Size(821, 68);
            _topPanel.TabIndex = 0;
            // 
            // _statusPanel
            // 
            _statusPanel.BackColor = Color.FromArgb(250, 250, 250);
            _statusPanel.Controls.Add(_previewLabel);
            _statusPanel.Controls.Add(_validationLabel);
            _statusPanel.Dock = DockStyle.Top;
            _statusPanel.Location = new Point(0, 32);
            _statusPanel.Name = "_statusPanel";
            _statusPanel.Padding = new Padding(8, 6, 8, 6);
            _statusPanel.Size = new Size(821, 30);
            _statusPanel.TabIndex = 1;
            // 
            // _previewLabel
            // 
            _previewLabel.AutoSize = true;
            _previewLabel.Font = new Font("微软雅黑", 9F);
            _previewLabel.ForeColor = Color.FromArgb(115, 115, 115);
            _previewLabel.Location = new Point(220, 6);
            _previewLabel.Name = "_previewLabel";
            _previewLabel.Size = new Size(0, 17);
            _previewLabel.TabIndex = 1;
            // 
            // _validationLabel
            // 
            _validationLabel.AutoSize = true;
            _validationLabel.Font = new Font("微软雅黑", 10F);
            _validationLabel.ForeColor = Color.FromArgb(115, 115, 115);
            _validationLabel.Location = new Point(8, 5);
            _validationLabel.Name = "_validationLabel";
            _validationLabel.Size = new Size(79, 20);
            _validationLabel.TabIndex = 0;
            _validationLabel.Text = "✓ 准备就绪";
            // 
            // _expressionTextBox
            // 
            _expressionTextBox.BackColor = Color.White;
            _expressionTextBox.Dock = DockStyle.Top;
            _expressionTextBox.Font = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _expressionTextBox.ForeColor = Color.FromArgb(38, 38, 38);
            _expressionTextBox.Location = new Point(0, 0);
            _expressionTextBox.Margin = new Padding(4, 5, 4, 5);
            _expressionTextBox.MinimumSize = new Size(1, 16);
            _expressionTextBox.Name = "_expressionTextBox";
            _expressionTextBox.Padding = new Padding(8, 5, 8, 5);
            _expressionTextBox.ShowText = false;
            _expressionTextBox.Size = new Size(821, 32);
            _expressionTextBox.TabIndex = 0;
            _expressionTextBox.TextAlignment = ContentAlignment.MiddleLeft;
            _expressionTextBox.Watermark = "请输入表达式...";
            _expressionTextBox.TextChanged += ExpressionTextBox_TextChanged;
            // 
            // _validationTimer
            // 
            _validationTimer.Interval = 300;
            _validationTimer.Tick += ValidationTimer_Tick;
            // 
            // ExpressionInputPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(847, 398);
            Controls.Add(_mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ExpressionInputPanel";
            Padding = new Padding(1);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            Deactivate += ExpressionInputPanel_Deactivate;
            Paint += ExpressionInputPanel_Paint;
            _mainPanel.ResumeLayout(false);
            _contentPanel.ResumeLayout(false);
            _keyboardPanel.ResumeLayout(false);
            _sourcePanel.ResumeLayout(false);
            _topPanel.ResumeLayout(false);
            _statusPanel.ResumeLayout(false);
            _statusPanel.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _mainPanel;
        private System.Windows.Forms.Panel _topPanel;
        private System.Windows.Forms.Panel _statusPanel;
        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel _sourcePanel;
        private System.Windows.Forms.Panel _keyboardPanel;
        private System.Windows.Forms.TableLayoutPanel _keyboardGrid;
        private Sunny.UI.UITextBox _expressionTextBox;
        private System.Windows.Forms.Label _validationLabel;
        private System.Windows.Forms.Label _previewLabel;
        private System.Windows.Forms.Button _btnPLC;
        private System.Windows.Forms.Button _btnVariable;
        private System.Windows.Forms.Button _btnExpression;
        private System.Windows.Forms.Button _btnSystem;
        private System.Windows.Forms.Button _btnFunction;
        private System.Windows.Forms.Button _btnConstant;
        private System.Windows.Forms.Timer _validationTimer;
    }
}
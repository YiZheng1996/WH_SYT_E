namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class ExpressionBuilderDialog
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
            toolTip = new ToolTip(components);
            txtExpression = new UITextBox();
            cmbTemplates = new UIComboBox();
            lstVariables = new UIListBox();
            lstFunctions = new UIListBox();
            lstOperators = new UIListBox();
            btnValidate = new UIButton();
            btnClear = new UISymbolButton();
            btnUndo = new UISymbolButton();
            btnRedo = new UISymbolButton();
            btnHelp = new UISymbolButton();
            btnOK = new UISymbolButton();
            btnCancel = new UISymbolButton();
            grpExpression = new UIPanel();
            uiLine1 = new UILine();
            rtbPreview = new UIRichTextBox();
            lblPreview = new UILabel();
            lblValidationResult = new UILabel();
            lblExpression = new UILabel();
            pnlTools = new UIPanel();
            lblTemplates = new UILabel();
            grpLists = new UIPanel();
            uiLine2 = new UILine();
            pnlVariables = new UIPanel();
            lblFunctions = new UILabel();
            lblVariables = new UILabel();
            lblOperators = new UILabel();
            grpButtons = new UIGroupBox();
            _validationTimer = new System.Windows.Forms.Timer(components);
            grpExpression.SuspendLayout();
            pnlTools.SuspendLayout();
            grpLists.SuspendLayout();
            pnlVariables.SuspendLayout();
            grpButtons.SuspendLayout();
            SuspendLayout();
            // 
            // txtExpression
            // 
            txtExpression.FillColor2 = Color.White;
            txtExpression.Font = new Font("微软雅黑", 10F);
            txtExpression.Location = new Point(15, 60);
            txtExpression.Margin = new Padding(4, 5, 4, 5);
            txtExpression.MinimumSize = new Size(1, 16);
            txtExpression.Multiline = true;
            txtExpression.Name = "txtExpression";
            txtExpression.Padding = new Padding(5);
            txtExpression.RectColor = Color.FromArgb(65, 100, 204);
            txtExpression.RectDisableColor = Color.FromArgb(65, 100, 204);
            txtExpression.RectReadOnlyColor = Color.FromArgb(65, 100, 204);
            txtExpression.ShowText = false;
            txtExpression.Size = new Size(463, 205);
            txtExpression.TabIndex = 1;
            txtExpression.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(txtExpression, "输入或编辑表达式,支持 Ctrl+Z 撤销, Ctrl+Y 重做");
            txtExpression.Watermark = "在此输入表达式,或从下方列表选择插入...";
            // 
            // cmbTemplates
            // 
            cmbTemplates.DataSource = null;
            cmbTemplates.FillColor = Color.White;
            cmbTemplates.Font = new Font("微软雅黑", 10F);
            cmbTemplates.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbTemplates.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbTemplates.Location = new Point(110, 10);
            cmbTemplates.Margin = new Padding(4, 5, 4, 5);
            cmbTemplates.MinimumSize = new Size(63, 0);
            cmbTemplates.Name = "cmbTemplates";
            cmbTemplates.Padding = new Padding(0, 0, 30, 2);
            cmbTemplates.RectColor = Color.FromArgb(65, 100, 204);
            cmbTemplates.RectDisableColor = Color.FromArgb(65, 100, 204);
            cmbTemplates.Size = new Size(850, 30);
            cmbTemplates.SymbolSize = 24;
            cmbTemplates.TabIndex = 1;
            cmbTemplates.TextAlignment = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(cmbTemplates, "选择常用表达式模板快速开始");
            cmbTemplates.Watermark = "";
            // 
            // lstVariables
            // 
            lstVariables.FillColor = Color.White;
            lstVariables.FillColor2 = Color.White;
            lstVariables.Font = new Font("微软雅黑", 10F);
            lstVariables.HoverColor = Color.FromArgb(155, 200, 255);
            lstVariables.ItemHeight = 20;
            lstVariables.ItemSelectForeColor = Color.White;
            lstVariables.Location = new Point(5, 30);
            lstVariables.Margin = new Padding(4, 5, 4, 5);
            lstVariables.MinimumSize = new Size(1, 1);
            lstVariables.Name = "lstVariables";
            lstVariables.Padding = new Padding(2);
            lstVariables.RectColor = Color.FromArgb(65, 100, 204);
            lstVariables.RectDisableColor = Color.FromArgb(65, 100, 204);
            lstVariables.ShowText = false;
            lstVariables.Size = new Size(300, 289);
            lstVariables.TabIndex = 1;
            lstVariables.Text = null;
            toolTip.SetToolTip(lstVariables, "双击变量名插入到表达式");
            // 
            // lstFunctions
            // 
            lstFunctions.FillColor = Color.White;
            lstFunctions.FillColor2 = Color.White;
            lstFunctions.Font = new Font("微软雅黑", 10F);
            lstFunctions.HoverColor = Color.FromArgb(155, 200, 255);
            lstFunctions.ItemHeight = 20;
            lstFunctions.ItemSelectForeColor = Color.White;
            lstFunctions.Location = new Point(550, 30);
            lstFunctions.Margin = new Padding(4, 5, 4, 5);
            lstFunctions.MinimumSize = new Size(1, 1);
            lstFunctions.Name = "lstFunctions";
            lstFunctions.Padding = new Padding(2);
            lstFunctions.RectColor = Color.FromArgb(65, 100, 204);
            lstFunctions.RectDisableColor = Color.FromArgb(65, 100, 204);
            lstFunctions.ShowText = false;
            lstFunctions.Size = new Size(399, 289);
            lstFunctions.TabIndex = 1;
            lstFunctions.Text = null;
            toolTip.SetToolTip(lstFunctions, "双击函数名插入到表达式");
            // 
            // lstOperators
            // 
            lstOperators.FillColor = Color.White;
            lstOperators.FillColor2 = Color.White;
            lstOperators.Font = new Font("微软雅黑", 10F);
            lstOperators.HoverColor = Color.FromArgb(155, 200, 255);
            lstOperators.ItemHeight = 20;
            lstOperators.ItemSelectForeColor = Color.White;
            lstOperators.Location = new Point(312, 30);
            lstOperators.Margin = new Padding(4, 5, 4, 5);
            lstOperators.MinimumSize = new Size(1, 1);
            lstOperators.Name = "lstOperators";
            lstOperators.Padding = new Padding(2);
            lstOperators.RectColor = Color.FromArgb(65, 100, 204);
            lstOperators.RectDisableColor = Color.FromArgb(65, 100, 204);
            lstOperators.ShowText = false;
            lstOperators.Size = new Size(231, 289);
            lstOperators.TabIndex = 1;
            lstOperators.Text = null;
            toolTip.SetToolTip(lstOperators, "双击运算符插入到表达式");
            // 
            // btnValidate
            // 
            btnValidate.Cursor = Cursors.Hand;
            btnValidate.FillColor = Color.FromArgb(23, 162, 184);
            btnValidate.Font = new Font("微软雅黑", 9F);
            btnValidate.Location = new Point(20, 26);
            btnValidate.MinimumSize = new Size(1, 1);
            btnValidate.Name = "btnValidate";
            btnValidate.RectColor = Color.FromArgb(23, 162, 184);
            btnValidate.Size = new Size(100, 38);
            btnValidate.TabIndex = 0;
            btnValidate.Text = "🔍 验证";
            btnValidate.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnValidate, "验证表达式语法 (Ctrl+Enter)");
            // 
            // btnClear
            // 
            btnClear.Cursor = Cursors.Hand;
            btnClear.FillColor = Color.FromArgb(255, 193, 7);
            btnClear.Font = new Font("微软雅黑", 9F);
            btnClear.Location = new Point(140, 26);
            btnClear.MinimumSize = new Size(1, 1);
            btnClear.Name = "btnClear";
            btnClear.RectColor = Color.FromArgb(255, 193, 7);
            btnClear.Size = new Size(100, 38);
            btnClear.Symbol = 61714;
            btnClear.TabIndex = 1;
            btnClear.Text = "清除";
            btnClear.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnClear, "清除当前表达式");
            // 
            // btnUndo
            // 
            btnUndo.Cursor = Cursors.Hand;
            btnUndo.FillColor = Color.FromArgb(108, 117, 125);
            btnUndo.Font = new Font("微软雅黑", 9F);
            btnUndo.Location = new Point(260, 26);
            btnUndo.MinimumSize = new Size(1, 1);
            btnUndo.Name = "btnUndo";
            btnUndo.RectColor = Color.FromArgb(108, 117, 125);
            btnUndo.Size = new Size(90, 38);
            btnUndo.Symbol = 61666;
            btnUndo.TabIndex = 2;
            btnUndo.Text = "撤销";
            btnUndo.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnUndo, "撤销 (Ctrl+Z)");
            // 
            // btnRedo
            // 
            btnRedo.Cursor = Cursors.Hand;
            btnRedo.FillColor = Color.FromArgb(108, 117, 125);
            btnRedo.Font = new Font("微软雅黑", 9F);
            btnRedo.Location = new Point(370, 26);
            btnRedo.MinimumSize = new Size(1, 1);
            btnRedo.Name = "btnRedo";
            btnRedo.RectColor = Color.FromArgb(108, 117, 125);
            btnRedo.Size = new Size(90, 38);
            btnRedo.Symbol = 61667;
            btnRedo.TabIndex = 3;
            btnRedo.Text = "重做";
            btnRedo.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnRedo, "重做 (Ctrl+Y)");
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.FillColor = Color.FromArgb(102, 126, 234);
            btnHelp.Font = new Font("微软雅黑", 9F);
            btnHelp.Location = new Point(650, 26);
            btnHelp.MinimumSize = new Size(1, 1);
            btnHelp.Name = "btnHelp";
            btnHelp.RectColor = Color.FromArgb(102, 126, 234);
            btnHelp.Size = new Size(90, 38);
            btnHelp.Symbol = 61736;
            btnHelp.TabIndex = 4;
            btnHelp.Text = "帮助";
            btnHelp.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnHelp, "显示使用帮助");
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.FillColor = Color.FromArgb(65, 100, 204);
            btnOK.Font = new Font("微软雅黑", 10F);
            btnOK.Location = new Point(760, 26);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.RectColor = Color.FromArgb(65, 100, 204);
            btnOK.Size = new Size(100, 38);
            btnOK.Symbol = 61528;
            btnOK.TabIndex = 5;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnOK, "应用当前表达式");
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(870, 26);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(220, 53, 69);
            btnCancel.Size = new Size(100, 38);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 6;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolTip.SetToolTip(btnCancel, "取消并关闭");
            // 
            // grpExpression
            // 
            grpExpression.BackColor = Color.Transparent;
            grpExpression.Controls.Add(uiLine1);
            grpExpression.Controls.Add(rtbPreview);
            grpExpression.Controls.Add(lblPreview);
            grpExpression.Controls.Add(lblValidationResult);
            grpExpression.Controls.Add(txtExpression);
            grpExpression.Controls.Add(lblExpression);
            grpExpression.FillColor = Color.White;
            grpExpression.FillColor2 = Color.White;
            grpExpression.Font = new Font("微软雅黑", 10F);
            grpExpression.Location = new Point(10, 41);
            grpExpression.Margin = new Padding(4, 5, 4, 5);
            grpExpression.MinimumSize = new Size(1, 1);
            grpExpression.Name = "grpExpression";
            grpExpression.Padding = new Padding(10, 32, 10, 10);
            grpExpression.RectColor = Color.FromArgb(65, 100, 204);
            grpExpression.Size = new Size(980, 280);
            grpExpression.TabIndex = 0;
            grpExpression.Text = null;
            grpExpression.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.FromArgb(65, 100, 204);
            uiLine1.Location = new Point(13, 6);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(952, 29);
            uiLine1.TabIndex = 5;
            uiLine1.Text = "📝 表达式编辑区";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // rtbPreview
            // 
            rtbPreview.BackColor = Color.FromArgb(250, 250, 250);
            rtbPreview.FillColor = Color.White;
            rtbPreview.FillColor2 = Color.White;
            rtbPreview.Font = new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            rtbPreview.Location = new Point(486, 60);
            rtbPreview.Margin = new Padding(4, 5, 4, 5);
            rtbPreview.MinimumSize = new Size(1, 1);
            rtbPreview.Name = "rtbPreview";
            rtbPreview.Padding = new Padding(2);
            rtbPreview.ReadOnly = true;
            rtbPreview.RectColor = Color.FromArgb(65, 100, 204);
            rtbPreview.RectDisableColor = Color.FromArgb(65, 100, 204);
            rtbPreview.ShowText = false;
            rtbPreview.Size = new Size(484, 205);
            rtbPreview.TabIndex = 4;
            rtbPreview.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblPreview
            // 
            lblPreview.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblPreview.ForeColor = Color.FromArgb(48, 48, 48);
            lblPreview.Location = new Point(486, 32);
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new Size(100, 25);
            lblPreview.TabIndex = 3;
            lblPreview.Text = "预览:";
            lblPreview.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblValidationResult
            // 
            lblValidationResult.Font = new Font("微软雅黑", 10F);
            lblValidationResult.ForeColor = Color.Gray;
            lblValidationResult.Location = new Point(73, 30);
            lblValidationResult.Name = "lblValidationResult";
            lblValidationResult.Size = new Size(406, 25);
            lblValidationResult.TabIndex = 2;
            lblValidationResult.Text = "验证结果: 等待输入";
            lblValidationResult.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblExpression
            // 
            lblExpression.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblExpression.ForeColor = Color.FromArgb(48, 48, 48);
            lblExpression.Location = new Point(15, 31);
            lblExpression.Name = "lblExpression";
            lblExpression.Size = new Size(100, 25);
            lblExpression.TabIndex = 0;
            lblExpression.Text = "表达式:";
            lblExpression.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlTools
            // 
            pnlTools.BackColor = Color.White;
            pnlTools.Controls.Add(cmbTemplates);
            pnlTools.Controls.Add(lblTemplates);
            pnlTools.FillColor = Color.White;
            pnlTools.FillColor2 = Color.White;
            pnlTools.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlTools.Location = new Point(10, 331);
            pnlTools.Margin = new Padding(4, 5, 4, 5);
            pnlTools.MinimumSize = new Size(1, 1);
            pnlTools.Name = "pnlTools";
            pnlTools.Padding = new Padding(5);
            pnlTools.RectColor = Color.FromArgb(65, 100, 204);
            pnlTools.RectDisableColor = Color.FromArgb(65, 100, 204);
            pnlTools.Size = new Size(980, 50);
            pnlTools.TabIndex = 1;
            pnlTools.Text = null;
            pnlTools.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblTemplates
            // 
            lblTemplates.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblTemplates.ForeColor = Color.FromArgb(48, 48, 48);
            lblTemplates.Location = new Point(10, 10);
            lblTemplates.Name = "lblTemplates";
            lblTemplates.Size = new Size(100, 30);
            lblTemplates.TabIndex = 0;
            lblTemplates.Text = " 模板:";
            lblTemplates.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpLists
            // 
            grpLists.Controls.Add(uiLine2);
            grpLists.Controls.Add(pnlVariables);
            grpLists.Font = new Font("微软雅黑", 10F);
            grpLists.Location = new Point(10, 392);
            grpLists.Margin = new Padding(5);
            grpLists.MinimumSize = new Size(1, 1);
            grpLists.Name = "grpLists";
            grpLists.Padding = new Padding(10, 32, 10, 10);
            grpLists.RectColor = Color.FromArgb(65, 100, 204);
            grpLists.RectDisableColor = Color.FromArgb(65, 100, 204);
            grpLists.Size = new Size(980, 366);
            grpLists.TabIndex = 2;
            grpLists.Text = null;
            grpLists.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.FromArgb(65, 100, 204);
            uiLine2.Location = new Point(13, 3);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(952, 29);
            uiLine2.TabIndex = 6;
            uiLine2.Text = "📚 可用元素 (双击或选中后按Enter插入)";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlVariables
            // 
            pnlVariables.Controls.Add(lstFunctions);
            pnlVariables.Controls.Add(lstOperators);
            pnlVariables.Controls.Add(lblFunctions);
            pnlVariables.Controls.Add(lstVariables);
            pnlVariables.Controls.Add(lblVariables);
            pnlVariables.Controls.Add(lblOperators);
            pnlVariables.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlVariables.Location = new Point(10, 32);
            pnlVariables.Margin = new Padding(5);
            pnlVariables.MinimumSize = new Size(1, 1);
            pnlVariables.Name = "pnlVariables";
            pnlVariables.Padding = new Padding(5);
            pnlVariables.RectColor = Color.FromArgb(65, 100, 204);
            pnlVariables.RectDisableColor = Color.FromArgb(65, 100, 204);
            pnlVariables.Size = new Size(960, 324);
            pnlVariables.TabIndex = 0;
            pnlVariables.Text = null;
            pnlVariables.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblFunctions
            // 
            lblFunctions.BackColor = Color.Transparent;
            lblFunctions.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblFunctions.ForeColor = Color.FromArgb(48, 48, 48);
            lblFunctions.Location = new Point(550, 5);
            lblFunctions.Name = "lblFunctions";
            lblFunctions.Size = new Size(399, 25);
            lblFunctions.TabIndex = 0;
            lblFunctions.Text = " 函数库";
            lblFunctions.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblVariables
            // 
            lblVariables.BackColor = Color.Transparent;
            lblVariables.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblVariables.ForeColor = Color.FromArgb(48, 48, 48);
            lblVariables.Location = new Point(5, 5);
            lblVariables.Name = "lblVariables";
            lblVariables.Size = new Size(300, 25);
            lblVariables.TabIndex = 0;
            lblVariables.Text = " 变量列表";
            lblVariables.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOperators
            // 
            lblOperators.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblOperators.ForeColor = Color.FromArgb(48, 48, 48);
            lblOperators.Location = new Point(312, 5);
            lblOperators.Name = "lblOperators";
            lblOperators.Size = new Size(231, 25);
            lblOperators.TabIndex = 0;
            lblOperators.Text = " 运算符";
            lblOperators.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpButtons
            // 
            grpButtons.BackColor = Color.White;
            grpButtons.Controls.Add(btnValidate);
            grpButtons.Controls.Add(btnCancel);
            grpButtons.Controls.Add(btnOK);
            grpButtons.Controls.Add(btnHelp);
            grpButtons.Controls.Add(btnRedo);
            grpButtons.Controls.Add(btnUndo);
            grpButtons.Controls.Add(btnClear);
            grpButtons.Dock = DockStyle.Bottom;
            grpButtons.FillColor = Color.White;
            grpButtons.FillColor2 = Color.White;
            grpButtons.Font = new Font("微软雅黑", 10F);
            grpButtons.Location = new Point(10, 752);
            grpButtons.Margin = new Padding(4, 5, 4, 5);
            grpButtons.MinimumSize = new Size(1, 1);
            grpButtons.Name = "grpButtons";
            grpButtons.Padding = new Padding(10, 32, 10, 10);
            grpButtons.RectColor = Color.FromArgb(65, 100, 204);
            grpButtons.RectDisableColor = Color.FromArgb(65, 100, 204);
            grpButtons.Size = new Size(980, 70);
            grpButtons.TabIndex = 3;
            grpButtons.Text = null;
            grpButtons.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // _validationTimer
            // 
            _validationTimer.Interval = 500;
            _validationTimer.Tick += _validationTimer_Tick;
            // 
            // ExpressionBuilderDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1000, 832);
            ControlBox = false;
            Controls.Add(grpLists);
            Controls.Add(grpButtons);
            Controls.Add(pnlTools);
            Controls.Add(grpExpression);
            Font = new Font("微软雅黑", 9F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExpressionBuilderDialog";
            Padding = new Padding(10);
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "表达式构建器";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1000, 620);
            grpExpression.ResumeLayout(false);
            pnlTools.ResumeLayout(false);
            grpLists.ResumeLayout(false);
            pnlVariables.ResumeLayout(false);
            grpButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void _validationTimer_Tick(object sender, EventArgs e)
        {
            _validationTimer.Stop();
            ValidateExpression();
        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private Sunny.UI.UIPanel grpExpression;
        private Sunny.UI.UILabel lblExpression;
        private Sunny.UI.UITextBox txtExpression;
        private Sunny.UI.UILabel lblValidationResult;
        private Sunny.UI.UILabel lblPreview;
        private Sunny.UI.UIRichTextBox rtbPreview;
        private Sunny.UI.UIPanel pnlTools;
        private Sunny.UI.UILabel lblTemplates;
        private Sunny.UI.UIComboBox cmbTemplates;
        private Sunny.UI.UIPanel grpLists;
        private Sunny.UI.UIPanel pnlVariables;
        private Sunny.UI.UILabel lblVariables;
        private Sunny.UI.UIListBox lstVariables;
        private Sunny.UI.UILabel lblFunctions;
        private Sunny.UI.UIListBox lstFunctions;
        private Sunny.UI.UILabel lblOperators;
        private Sunny.UI.UIListBox lstOperators;
        private Sunny.UI.UIGroupBox grpButtons;
        private Sunny.UI.UIButton btnValidate;
        private Sunny.UI.UISymbolButton btnClear;
        private Sunny.UI.UISymbolButton btnUndo;
        private Sunny.UI.UISymbolButton btnRedo;
        private Sunny.UI.UISymbolButton btnHelp;
        private Sunny.UI.UISymbolButton btnOK;
        private Sunny.UI.UISymbolButton btnCancel;
        private System.Windows.Forms.Timer _validationTimer;
        private UILine uiLine1;
        private UILine uiLine2;
    }
}
using Sunny.UI;

namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_ExpressionHelper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            panelMain = new Panel();
            grpTemplate = new UIGroupBox();
            btnTemplateLogic = new UIButton();
            btnTemplateError = new UIButton();
            btnTemplateRange = new UIButton();
            btnTemplateBasic = new UIButton();
            grpExpression = new UIGroupBox();
            btnClear = new UISymbolButton();
            txtPreview = new UITextBox();
            lblPreview = new UILabel();
            grpRight = new UIGroupBox();
            rdoRightExpr = new UIRadioButton();
            rdoRightVar = new UIRadioButton();
            rdoRightValue = new UIRadioButton();
            cmbRightVariable = new UIComboBox();
            txtRightValue = new UITextBox();
            txtRightExpression = new UITextBox();
            grpOperator = new UIGroupBox();
            rdoOpOr = new UIRadioButton();
            rdoOpAnd = new UIRadioButton();
            rdoOpNotEqual = new UIRadioButton();
            rdoOpEqual = new UIRadioButton();
            rdoOpLessEqual = new UIRadioButton();
            rdoOpGreaterEqual = new UIRadioButton();
            rdoOpLess = new UIRadioButton();
            rdoOpGreater = new UIRadioButton();
            grpLeft = new UIGroupBox();
            cmbFunction = new UIComboBox();
            lblFunction = new UILabel();
            rdoLeftExpr = new UIRadioButton();
            rdoLeftVar = new UIRadioButton();
            cmbLeftVariable = new UIComboBox();
            txtLeftExpression = new UITextBox();
            panelBottom = new Panel();
            btnCancel = new UISymbolButton();
            btnOK = new UISymbolButton();
            panelMain.SuspendLayout();
            grpTemplate.SuspendLayout();
            grpExpression.SuspendLayout();
            grpRight.SuspendLayout();
            grpOperator.SuspendLayout();
            grpLeft.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(grpTemplate);
            panelMain.Controls.Add(grpExpression);
            panelMain.Controls.Add(grpRight);
            panelMain.Controls.Add(grpOperator);
            panelMain.Controls.Add(grpLeft);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15);
            panelMain.Size = new Size(680, 545);
            panelMain.TabIndex = 0;
            // 
            // grpTemplate
            // 
            grpTemplate.Controls.Add(btnTemplateLogic);
            grpTemplate.Controls.Add(btnTemplateError);
            grpTemplate.Controls.Add(btnTemplateRange);
            grpTemplate.Controls.Add(btnTemplateBasic);
            grpTemplate.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpTemplate.Location = new Point(18, 390);
            grpTemplate.Margin = new Padding(4, 5, 4, 5);
            grpTemplate.MinimumSize = new Size(1, 1);
            grpTemplate.Name = "grpTemplate";
            grpTemplate.Padding = new Padding(0, 32, 0, 0);
            grpTemplate.Size = new Size(644, 135);
            grpTemplate.TabIndex = 4;
            grpTemplate.Text = "快速模板";
            grpTemplate.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnTemplateLogic
            // 
            btnTemplateLogic.Cursor = Cursors.Hand;
            btnTemplateLogic.Font = new Font("微软雅黑", 9F);
            btnTemplateLogic.Location = new Point(340, 80);
            btnTemplateLogic.MinimumSize = new Size(1, 1);
            btnTemplateLogic.Name = "btnTemplateLogic";
            btnTemplateLogic.Size = new Size(140, 35);
            btnTemplateLogic.TabIndex = 3;
            btnTemplateLogic.Text = "逻辑组合";
            btnTemplateLogic.TipsFont = new Font("宋体", 9F);
            // 
            // btnTemplateError
            // 
            btnTemplateError.Cursor = Cursors.Hand;
            btnTemplateError.Font = new Font("微软雅黑", 9F);
            btnTemplateError.Location = new Point(340, 40);
            btnTemplateError.MinimumSize = new Size(1, 1);
            btnTemplateError.Name = "btnTemplateError";
            btnTemplateError.Size = new Size(140, 35);
            btnTemplateError.TabIndex = 2;
            btnTemplateError.Text = "误差容忍";
            btnTemplateError.TipsFont = new Font("宋体", 9F);
            // 
            // btnTemplateRange
            // 
            btnTemplateRange.Cursor = Cursors.Hand;
            btnTemplateRange.Font = new Font("微软雅黑", 9F);
            btnTemplateRange.Location = new Point(180, 80);
            btnTemplateRange.MinimumSize = new Size(1, 1);
            btnTemplateRange.Name = "btnTemplateRange";
            btnTemplateRange.Size = new Size(140, 35);
            btnTemplateRange.TabIndex = 1;
            btnTemplateRange.Text = "范围判断";
            btnTemplateRange.TipsFont = new Font("宋体", 9F);
            // 
            // btnTemplateBasic
            // 
            btnTemplateBasic.Cursor = Cursors.Hand;
            btnTemplateBasic.Font = new Font("微软雅黑", 9F);
            btnTemplateBasic.Location = new Point(180, 40);
            btnTemplateBasic.MinimumSize = new Size(1, 1);
            btnTemplateBasic.Name = "btnTemplateBasic";
            btnTemplateBasic.Size = new Size(140, 35);
            btnTemplateBasic.TabIndex = 0;
            btnTemplateBasic.Text = "基本比较";
            btnTemplateBasic.TipsFont = new Font("宋体", 9F);
            // 
            // grpExpression
            // 
            grpExpression.Controls.Add(btnClear);
            grpExpression.Controls.Add(txtPreview);
            grpExpression.Controls.Add(lblPreview);
            grpExpression.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpExpression.Location = new Point(18, 285);
            grpExpression.Margin = new Padding(4, 5, 4, 5);
            grpExpression.MinimumSize = new Size(1, 1);
            grpExpression.Name = "grpExpression";
            grpExpression.Padding = new Padding(0, 32, 0, 0);
            grpExpression.Size = new Size(644, 95);
            grpExpression.TabIndex = 3;
            grpExpression.Text = "表达式预览";
            grpExpression.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnClear
            // 
            btnClear.Cursor = Cursors.Hand;
            btnClear.Font = new Font("宋体", 12F);
            btnClear.Location = new Point(550, 40);
            btnClear.MinimumSize = new Size(1, 1);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(80, 35);
            btnClear.Symbol = 61757;
            btnClear.TabIndex = 2;
            btnClear.Text = "清空";
            btnClear.TipsFont = new Font("宋体", 9F);
            // 
            // txtPreview
            // 
            txtPreview.Cursor = Cursors.IBeam;
            txtPreview.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            txtPreview.ForeColor = Color.FromArgb(65, 100, 204);
            txtPreview.Location = new Point(90, 40);
            txtPreview.Margin = new Padding(4, 5, 4, 5);
            txtPreview.MinimumSize = new Size(1, 16);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.Padding = new Padding(5);
            txtPreview.ReadOnly = true;
            txtPreview.RectColor = Color.FromArgb(65, 100, 204);
            txtPreview.ShowText = false;
            txtPreview.Size = new Size(450, 35);
            txtPreview.TabIndex = 1;
            txtPreview.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // lblPreview
            // 
            lblPreview.Font = new Font("微软雅黑", 10F);
            lblPreview.ForeColor = Color.FromArgb(48, 48, 48);
            lblPreview.Location = new Point(15, 40);
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new Size(70, 35);
            lblPreview.TabIndex = 0;
            lblPreview.Text = "结果:";
            lblPreview.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpRight
            // 
            grpRight.Controls.Add(rdoRightExpr);
            grpRight.Controls.Add(rdoRightVar);
            grpRight.Controls.Add(rdoRightValue);
            grpRight.Controls.Add(cmbRightVariable);
            grpRight.Controls.Add(txtRightValue);
            grpRight.Controls.Add(txtRightExpression);
            grpRight.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpRight.Location = new Point(458, 15);
            grpRight.Margin = new Padding(4, 5, 4, 5);
            grpRight.MinimumSize = new Size(1, 1);
            grpRight.Name = "grpRight";
            grpRight.Padding = new Padding(0, 32, 0, 0);
            grpRight.Size = new Size(204, 170);
            grpRight.TabIndex = 2;
            grpRight.Text = "右侧操作数";
            grpRight.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // rdoRightExpr
            // 
            rdoRightExpr.Cursor = Cursors.Hand;
            rdoRightExpr.Font = new Font("微软雅黑", 9F);
            rdoRightExpr.Location = new Point(130, 35);
            rdoRightExpr.MinimumSize = new Size(1, 1);
            rdoRightExpr.Name = "rdoRightExpr";
            rdoRightExpr.Size = new Size(60, 25);
            rdoRightExpr.TabIndex = 5;
            rdoRightExpr.Text = "表达式";
            // 
            // rdoRightVar
            // 
            rdoRightVar.Cursor = Cursors.Hand;
            rdoRightVar.Font = new Font("微软雅黑", 9F);
            rdoRightVar.Location = new Point(70, 35);
            rdoRightVar.MinimumSize = new Size(1, 1);
            rdoRightVar.Name = "rdoRightVar";
            rdoRightVar.Size = new Size(60, 25);
            rdoRightVar.TabIndex = 4;
            rdoRightVar.Text = "变量";
            // 
            // rdoRightValue
            // 
            rdoRightValue.Checked = true;
            rdoRightValue.Cursor = Cursors.Hand;
            rdoRightValue.Font = new Font("微软雅黑", 9F);
            rdoRightValue.Location = new Point(10, 35);
            rdoRightValue.MinimumSize = new Size(1, 1);
            rdoRightValue.Name = "rdoRightValue";
            rdoRightValue.Size = new Size(60, 25);
            rdoRightValue.TabIndex = 3;
            rdoRightValue.Text = "数值";
            // 
            // cmbRightVariable
            // 
            cmbRightVariable.DataSource = null;
            cmbRightVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbRightVariable.Enabled = false;
            cmbRightVariable.FillColor = Color.White;
            cmbRightVariable.Font = new Font("微软雅黑", 10F);
            cmbRightVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbRightVariable.Location = new Point(10, 95);
            cmbRightVariable.Margin = new Padding(4, 5, 4, 5);
            cmbRightVariable.MinimumSize = new Size(63, 0);
            cmbRightVariable.Name = "cmbRightVariable";
            cmbRightVariable.Padding = new Padding(0, 0, 30, 2);
            cmbRightVariable.RectColor = Color.FromArgb(65, 100, 204);
            cmbRightVariable.Size = new Size(184, 29);
            cmbRightVariable.SymbolSize = 24;
            cmbRightVariable.TabIndex = 2;
            cmbRightVariable.TextAlignment = ContentAlignment.MiddleLeft;
            cmbRightVariable.Watermark = "选择变量";
            // 
            // txtRightValue
            // 
            txtRightValue.Cursor = Cursors.IBeam;
            txtRightValue.Font = new Font("微软雅黑", 10F);
            txtRightValue.Location = new Point(10, 65);
            txtRightValue.Margin = new Padding(4, 5, 4, 5);
            txtRightValue.MinimumSize = new Size(1, 16);
            txtRightValue.Name = "txtRightValue";
            txtRightValue.Padding = new Padding(5);
            txtRightValue.RectColor = Color.FromArgb(65, 100, 204);
            txtRightValue.ShowText = false;
            txtRightValue.Size = new Size(184, 29);
            txtRightValue.TabIndex = 1;
            txtRightValue.TextAlignment = ContentAlignment.MiddleLeft;
            txtRightValue.Watermark = "输入数值";
            // 
            // txtRightExpression
            // 
            txtRightExpression.Cursor = Cursors.IBeam;
            txtRightExpression.Enabled = false;
            txtRightExpression.Font = new Font("微软雅黑", 10F);
            txtRightExpression.Location = new Point(10, 130);
            txtRightExpression.Margin = new Padding(4, 5, 4, 5);
            txtRightExpression.MinimumSize = new Size(1, 16);
            txtRightExpression.Name = "txtRightExpression";
            txtRightExpression.Padding = new Padding(5);
            txtRightExpression.RectColor = Color.FromArgb(65, 100, 204);
            txtRightExpression.ShowText = false;
            txtRightExpression.Size = new Size(184, 29);
            txtRightExpression.TabIndex = 0;
            txtRightExpression.TextAlignment = ContentAlignment.MiddleLeft;
            txtRightExpression.Watermark = "输入表达式";
            // 
            // grpOperator
            // 
            grpOperator.Controls.Add(rdoOpOr);
            grpOperator.Controls.Add(rdoOpAnd);
            grpOperator.Controls.Add(rdoOpNotEqual);
            grpOperator.Controls.Add(rdoOpEqual);
            grpOperator.Controls.Add(rdoOpLessEqual);
            grpOperator.Controls.Add(rdoOpGreaterEqual);
            grpOperator.Controls.Add(rdoOpLess);
            grpOperator.Controls.Add(rdoOpGreater);
            grpOperator.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpOperator.Location = new Point(238, 15);
            grpOperator.Margin = new Padding(4, 5, 4, 5);
            grpOperator.MinimumSize = new Size(1, 1);
            grpOperator.Name = "grpOperator";
            grpOperator.Padding = new Padding(0, 32, 0, 0);
            grpOperator.Size = new Size(210, 260);
            grpOperator.TabIndex = 1;
            grpOperator.Text = "运算符";
            grpOperator.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // rdoOpOr
            // 
            rdoOpOr.Cursor = Cursors.Hand;
            rdoOpOr.Font = new Font("微软雅黑", 10F);
            rdoOpOr.Location = new Point(110, 215);
            rdoOpOr.MinimumSize = new Size(1, 1);
            rdoOpOr.Name = "rdoOpOr";
            rdoOpOr.Size = new Size(80, 29);
            rdoOpOr.TabIndex = 7;
            rdoOpOr.Text = "OR";
            // 
            // rdoOpAnd
            // 
            rdoOpAnd.Cursor = Cursors.Hand;
            rdoOpAnd.Font = new Font("微软雅黑", 10F);
            rdoOpAnd.Location = new Point(15, 215);
            rdoOpAnd.MinimumSize = new Size(1, 1);
            rdoOpAnd.Name = "rdoOpAnd";
            rdoOpAnd.Size = new Size(80, 29);
            rdoOpAnd.TabIndex = 6;
            rdoOpAnd.Text = "AND";
            // 
            // rdoOpNotEqual
            // 
            rdoOpNotEqual.Cursor = Cursors.Hand;
            rdoOpNotEqual.Font = new Font("微软雅黑", 10F);
            rdoOpNotEqual.Location = new Point(110, 145);
            rdoOpNotEqual.MinimumSize = new Size(1, 1);
            rdoOpNotEqual.Name = "rdoOpNotEqual";
            rdoOpNotEqual.Size = new Size(80, 29);
            rdoOpNotEqual.TabIndex = 5;
            rdoOpNotEqual.Text = "!=";
            // 
            // rdoOpEqual
            // 
            rdoOpEqual.Cursor = Cursors.Hand;
            rdoOpEqual.Font = new Font("微软雅黑", 10F);
            rdoOpEqual.Location = new Point(15, 145);
            rdoOpEqual.MinimumSize = new Size(1, 1);
            rdoOpEqual.Name = "rdoOpEqual";
            rdoOpEqual.Size = new Size(80, 29);
            rdoOpEqual.TabIndex = 4;
            rdoOpEqual.Text = "==";
            // 
            // rdoOpLessEqual
            // 
            rdoOpLessEqual.Cursor = Cursors.Hand;
            rdoOpLessEqual.Font = new Font("微软雅黑", 10F);
            rdoOpLessEqual.Location = new Point(110, 110);
            rdoOpLessEqual.MinimumSize = new Size(1, 1);
            rdoOpLessEqual.Name = "rdoOpLessEqual";
            rdoOpLessEqual.Size = new Size(80, 29);
            rdoOpLessEqual.TabIndex = 3;
            rdoOpLessEqual.Text = "<=";
            // 
            // rdoOpGreaterEqual
            // 
            rdoOpGreaterEqual.Checked = true;
            rdoOpGreaterEqual.Cursor = Cursors.Hand;
            rdoOpGreaterEqual.Font = new Font("微软雅黑", 10F);
            rdoOpGreaterEqual.Location = new Point(15, 110);
            rdoOpGreaterEqual.MinimumSize = new Size(1, 1);
            rdoOpGreaterEqual.Name = "rdoOpGreaterEqual";
            rdoOpGreaterEqual.Size = new Size(80, 29);
            rdoOpGreaterEqual.TabIndex = 2;
            rdoOpGreaterEqual.Text = ">=";
            // 
            // rdoOpLess
            // 
            rdoOpLess.Cursor = Cursors.Hand;
            rdoOpLess.Font = new Font("微软雅黑", 10F);
            rdoOpLess.Location = new Point(110, 40);
            rdoOpLess.MinimumSize = new Size(1, 1);
            rdoOpLess.Name = "rdoOpLess";
            rdoOpLess.Size = new Size(80, 29);
            rdoOpLess.TabIndex = 1;
            rdoOpLess.Text = "<";
            // 
            // rdoOpGreater
            // 
            rdoOpGreater.Cursor = Cursors.Hand;
            rdoOpGreater.Font = new Font("微软雅黑", 10F);
            rdoOpGreater.Location = new Point(15, 40);
            rdoOpGreater.MinimumSize = new Size(1, 1);
            rdoOpGreater.Name = "rdoOpGreater";
            rdoOpGreater.Size = new Size(80, 29);
            rdoOpGreater.TabIndex = 0;
            rdoOpGreater.Text = ">";
            // 
            // grpLeft
            // 
            grpLeft.Controls.Add(cmbFunction);
            grpLeft.Controls.Add(lblFunction);
            grpLeft.Controls.Add(rdoLeftExpr);
            grpLeft.Controls.Add(rdoLeftVar);
            grpLeft.Controls.Add(cmbLeftVariable);
            grpLeft.Controls.Add(txtLeftExpression);
            grpLeft.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpLeft.Location = new Point(18, 15);
            grpLeft.Margin = new Padding(4, 5, 4, 5);
            grpLeft.MinimumSize = new Size(1, 1);
            grpLeft.Name = "grpLeft";
            grpLeft.Padding = new Padding(0, 32, 0, 0);
            grpLeft.Size = new Size(210, 260);
            grpLeft.TabIndex = 0;
            grpLeft.Text = "左侧操作数";
            grpLeft.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // cmbFunction
            // 
            cmbFunction.DataSource = null;
            cmbFunction.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbFunction.FillColor = Color.White;
            cmbFunction.Font = new Font("微软雅黑", 10F);
            cmbFunction.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbFunction.Location = new Point(80, 215);
            cmbFunction.Margin = new Padding(4, 5, 4, 5);
            cmbFunction.MinimumSize = new Size(63, 0);
            cmbFunction.Name = "cmbFunction";
            cmbFunction.Padding = new Padding(0, 0, 30, 2);
            cmbFunction.RectColor = Color.FromArgb(65, 100, 204);
            cmbFunction.Size = new Size(120, 29);
            cmbFunction.SymbolSize = 24;
            cmbFunction.TabIndex = 5;
            cmbFunction.TextAlignment = ContentAlignment.MiddleLeft;
            cmbFunction.Watermark = "无";
            // 
            // lblFunction
            // 
            lblFunction.Font = new Font("微软雅黑", 9F);
            lblFunction.ForeColor = Color.FromArgb(48, 48, 48);
            lblFunction.Location = new Point(10, 215);
            lblFunction.Name = "lblFunction";
            lblFunction.Size = new Size(70, 29);
            lblFunction.TabIndex = 4;
            lblFunction.Text = "函数:";
            lblFunction.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // rdoLeftExpr
            // 
            rdoLeftExpr.Cursor = Cursors.Hand;
            rdoLeftExpr.Font = new Font("微软雅黑", 9F);
            rdoLeftExpr.Location = new Point(130, 35);
            rdoLeftExpr.MinimumSize = new Size(1, 1);
            rdoLeftExpr.Name = "rdoLeftExpr";
            rdoLeftExpr.Size = new Size(70, 25);
            rdoLeftExpr.TabIndex = 3;
            rdoLeftExpr.Text = "表达式";
            // 
            // rdoLeftVar
            // 
            rdoLeftVar.Checked = true;
            rdoLeftVar.Cursor = Cursors.Hand;
            rdoLeftVar.Font = new Font("微软雅黑", 9F);
            rdoLeftVar.Location = new Point(10, 35);
            rdoLeftVar.MinimumSize = new Size(1, 1);
            rdoLeftVar.Name = "rdoLeftVar";
            rdoLeftVar.Size = new Size(120, 25);
            rdoLeftVar.TabIndex = 2;
            rdoLeftVar.Text = "变量";
            // 
            // cmbLeftVariable
            // 
            cmbLeftVariable.DataSource = null;
            cmbLeftVariable.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbLeftVariable.FillColor = Color.White;
            cmbLeftVariable.Font = new Font("微软雅黑", 10F);
            cmbLeftVariable.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbLeftVariable.Location = new Point(10, 65);
            cmbLeftVariable.Margin = new Padding(4, 5, 4, 5);
            cmbLeftVariable.MinimumSize = new Size(63, 0);
            cmbLeftVariable.Name = "cmbLeftVariable";
            cmbLeftVariable.Padding = new Padding(0, 0, 30, 2);
            cmbLeftVariable.RectColor = Color.FromArgb(65, 100, 204);
            cmbLeftVariable.Size = new Size(190, 29);
            cmbLeftVariable.SymbolSize = 24;
            cmbLeftVariable.TabIndex = 1;
            cmbLeftVariable.TextAlignment = ContentAlignment.MiddleLeft;
            cmbLeftVariable.Watermark = "选择变量";
            // 
            // txtLeftExpression
            // 
            txtLeftExpression.Cursor = Cursors.IBeam;
            txtLeftExpression.Enabled = false;
            txtLeftExpression.Font = new Font("微软雅黑", 10F);
            txtLeftExpression.Location = new Point(10, 100);
            txtLeftExpression.Margin = new Padding(4, 5, 4, 5);
            txtLeftExpression.MinimumSize = new Size(1, 16);
            txtLeftExpression.Multiline = true;
            txtLeftExpression.Name = "txtLeftExpression";
            txtLeftExpression.Padding = new Padding(5);
            txtLeftExpression.RectColor = Color.FromArgb(65, 100, 204);
            txtLeftExpression.ShowText = false;
            txtLeftExpression.Size = new Size(190, 100);
            txtLeftExpression.TabIndex = 0;
            txtLeftExpression.TextAlignment = ContentAlignment.TopLeft;
            txtLeftExpression.Watermark = "输入表达式";
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnOK);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 580);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(15, 10, 15, 10);
            panelBottom.Size = new Size(680, 60);
            panelBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(230, 80, 80);
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(462, 13);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(230, 80, 80);
            btnCancel.Size = new Size(100, 35);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F);
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.FillColor = Color.FromArgb(65, 100, 204);
            btnOK.Font = new Font("微软雅黑", 10F);
            btnOK.Location = new Point(572, 13);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.RectColor = Color.FromArgb(65, 100, 204);
            btnOK.Size = new Size(100, 35);
            btnOK.Symbol = 61639;
            btnOK.TabIndex = 0;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("宋体", 9F);
            // 
            // Form_ExpressionHelper
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(680, 640);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_ExpressionHelper";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "表达式助手";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 680, 640);
            panelMain.ResumeLayout(false);
            grpTemplate.ResumeLayout(false);
            grpExpression.ResumeLayout(false);
            grpRight.ResumeLayout(false);
            grpOperator.ResumeLayout(false);
            grpLeft.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private UIGroupBox grpLeft;
        private UIComboBox cmbLeftVariable;
        private UITextBox txtLeftExpression;
        private UIRadioButton rdoLeftVar;
        private UIRadioButton rdoLeftExpr;
        private UIGroupBox grpOperator;
        private UIRadioButton rdoOpGreater;
        private UIRadioButton rdoOpLess;
        private UIRadioButton rdoOpGreaterEqual;
        private UIRadioButton rdoOpLessEqual;
        private UIRadioButton rdoOpEqual;
        private UIRadioButton rdoOpNotEqual;
        private UIRadioButton rdoOpAnd;
        private UIRadioButton rdoOpOr;
        private UIGroupBox grpRight;
        private UITextBox txtRightExpression;
        private UITextBox txtRightValue;
        private UIComboBox cmbRightVariable;
        private UIRadioButton rdoRightValue;
        private UIRadioButton rdoRightVar;
        private UIRadioButton rdoRightExpr;
        private UIGroupBox grpExpression;
        private UILabel lblPreview;
        private UITextBox txtPreview;
        private Panel panelBottom;
        private UISymbolButton btnOK;
        private UISymbolButton btnCancel;
        private UISymbolButton btnClear;
        private UIGroupBox grpTemplate;
        private UIButton btnTemplateBasic;
        private UIButton btnTemplateRange;
        private UIButton btnTemplateError;
        private UIButton btnTemplateLogic;
        private UIComboBox cmbFunction;
        private UILabel lblFunction;
    }
}
using AntdUI;

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
            grpExpression = new UIGroupBox();
            btnValidate = new UIButton();
            lblValidationResult = new UILabel();
            rtbPreview = new UIRichTextBox();
            lblPreview = new UILabel();
            lstOperators = new UIListBox();
            lblOperators = new UILabel();
            lstFunctions = new UIListBox();
            lblFunctions = new UILabel();
            lstVariables = new UIListBox();
            lblVariables = new UILabel();
            txtExpression = new UITextBox();
            lblExpression = new UILabel();
            grpButtons = new UIGroupBox();
            btnCancel = new UISymbolButton();
            btnOK = new UISymbolButton();
            toolTip = new ToolTip(components);
            grpExpression.SuspendLayout();
            grpButtons.SuspendLayout();
            SuspendLayout();
            // 
            // grpExpression
            // 
            grpExpression.BackColor = Color.Transparent;
            grpExpression.Controls.Add(btnValidate);
            grpExpression.Controls.Add(lblValidationResult);
            grpExpression.Controls.Add(rtbPreview);
            grpExpression.Controls.Add(lblPreview);
            grpExpression.Controls.Add(lstOperators);
            grpExpression.Controls.Add(lblOperators);
            grpExpression.Controls.Add(lstFunctions);
            grpExpression.Controls.Add(lblFunctions);
            grpExpression.Controls.Add(lstVariables);
            grpExpression.Controls.Add(lblVariables);
            grpExpression.Controls.Add(txtExpression);
            grpExpression.Controls.Add(lblExpression);
            grpExpression.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            grpExpression.Location = new Point(10, 35);
            grpExpression.Margin = new Padding(4, 5, 4, 5);
            grpExpression.MinimumSize = new Size(1, 1);
            grpExpression.Name = "grpExpression";
            grpExpression.Padding = new Padding(0, 32, 0, 0);
            grpExpression.RectColor = Color.FromArgb(65, 100, 204);
            grpExpression.Size = new Size(870, 420);
            grpExpression.TabIndex = 0;
            grpExpression.Text = "表达式编辑";
            grpExpression.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnValidate
            // 
            btnValidate.Cursor = Cursors.Hand;
            btnValidate.FillColor = Color.FromArgb(65, 100, 204);
            btnValidate.FillColor2 = Color.FromArgb(65, 100, 204);
            btnValidate.Font = new Font("微软雅黑", 9F);
            btnValidate.Location = new Point(650, 210);
            btnValidate.MinimumSize = new Size(1, 1);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new Size(100, 35);
            btnValidate.TabIndex = 11;
            btnValidate.Text = "验证表达式";
            btnValidate.TipsFont = new Font("微软雅黑", 9F);
            toolTip.SetToolTip(btnValidate, "手动验证当前表达式的语法和逻辑");
            // 
            // lblValidationResult
            // 
            lblValidationResult.BackColor = Color.Transparent;
            lblValidationResult.Font = new Font("微软雅黑", 9F);
            lblValidationResult.ForeColor = Color.Gray;
            lblValidationResult.Location = new Point(10, 400);
            lblValidationResult.Name = "lblValidationResult";
            lblValidationResult.Size = new Size(620, 25);
            lblValidationResult.TabIndex = 10;
            lblValidationResult.Text = "验证结果: 等待输入";
            lblValidationResult.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // rtbPreview
            // 
            rtbPreview.FillColor = Color.White;
            rtbPreview.Font = new Font("微软雅黑", 9F);
            rtbPreview.Location = new Point(650, 60);
            rtbPreview.Margin = new Padding(4, 5, 4, 5);
            rtbPreview.MinimumSize = new Size(1, 1);
            rtbPreview.Name = "rtbPreview";
            rtbPreview.Padding = new Padding(2);
            rtbPreview.ReadOnly = true;
            rtbPreview.RectColor = Color.FromArgb(65, 100, 204);
            rtbPreview.ShowText = false;
            rtbPreview.Size = new Size(200, 130);
            rtbPreview.TabIndex = 9;
            rtbPreview.TextAlignment = ContentAlignment.TopLeft;
            toolTip.SetToolTip(rtbPreview, "显示表达式的预期计算结果");
            // 
            // lblPreview
            // 
            lblPreview.BackColor = Color.Transparent;
            lblPreview.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblPreview.ForeColor = Color.FromArgb(48, 48, 48);
            lblPreview.Location = new Point(650, 30);
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new Size(100, 25);
            lblPreview.TabIndex = 8;
            lblPreview.Text = "实时预览:";
            lblPreview.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstOperators
            // 
            lstOperators.Font = new Font("微软雅黑", 9F);
            lstOperators.HoverColor = Color.FromArgb(155, 200, 255);
            lstOperators.ItemSelectForeColor = Color.White;
            lstOperators.Location = new Point(450, 185);
            lstOperators.Margin = new Padding(4, 5, 4, 5);
            lstOperators.MinimumSize = new Size(1, 1);
            lstOperators.Name = "lstOperators";
            lstOperators.Padding = new Padding(2);
            lstOperators.RectColor = Color.FromArgb(65, 100, 204);
            lstOperators.ShowText = false;
            lstOperators.Size = new Size(180, 200);
            lstOperators.TabIndex = 7;
            lstOperators.Text = null;
            toolTip.SetToolTip(lstOperators, "双击运算符插入到表达式中");
            // 
            // lblOperators
            // 
            lblOperators.BackColor = Color.Transparent;
            lblOperators.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblOperators.ForeColor = Color.FromArgb(48, 48, 48);
            lblOperators.Location = new Point(450, 160);
            lblOperators.Name = "lblOperators";
            lblOperators.Size = new Size(100, 25);
            lblOperators.TabIndex = 6;
            lblOperators.Text = "运算符:";
            lblOperators.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstFunctions
            // 
            lstFunctions.Font = new Font("微软雅黑", 9F);
            lstFunctions.HoverColor = Color.FromArgb(155, 200, 255);
            lstFunctions.ItemSelectForeColor = Color.White;
            lstFunctions.Location = new Point(230, 185);
            lstFunctions.Margin = new Padding(4, 5, 4, 5);
            lstFunctions.MinimumSize = new Size(1, 1);
            lstFunctions.Name = "lstFunctions";
            lstFunctions.Padding = new Padding(2);
            lstFunctions.RectColor = Color.FromArgb(65, 100, 204);
            lstFunctions.ShowText = false;
            lstFunctions.Size = new Size(200, 200);
            lstFunctions.TabIndex = 5;
            lstFunctions.Text = null;
            toolTip.SetToolTip(lstFunctions, "双击函数名插入到表达式中");
            // 
            // lblFunctions
            // 
            lblFunctions.BackColor = Color.Transparent;
            lblFunctions.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblFunctions.ForeColor = Color.FromArgb(48, 48, 48);
            lblFunctions.Location = new Point(230, 160);
            lblFunctions.Name = "lblFunctions";
            lblFunctions.Size = new Size(100, 25);
            lblFunctions.TabIndex = 4;
            lblFunctions.Text = "内置函数:";
            lblFunctions.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstVariables
            // 
            lstVariables.Font = new Font("微软雅黑", 9F);
            lstVariables.HoverColor = Color.FromArgb(155, 200, 255);
            lstVariables.ItemSelectForeColor = Color.White;
            lstVariables.Location = new Point(10, 185);
            lstVariables.Margin = new Padding(4, 5, 4, 5);
            lstVariables.MinimumSize = new Size(1, 1);
            lstVariables.Name = "lstVariables";
            lstVariables.Padding = new Padding(2);
            lstVariables.RectColor = Color.FromArgb(65, 100, 204);
            lstVariables.ShowText = false;
            lstVariables.Size = new Size(200, 200);
            lstVariables.TabIndex = 3;
            lstVariables.Text = null;
            toolTip.SetToolTip(lstVariables, "双击变量名插入到表达式中");
            // 
            // lblVariables
            // 
            lblVariables.BackColor = Color.Transparent;
            lblVariables.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblVariables.ForeColor = Color.FromArgb(48, 48, 48);
            lblVariables.Location = new Point(10, 160);
            lblVariables.Name = "lblVariables";
            lblVariables.Size = new Size(100, 25);
            lblVariables.TabIndex = 2;
            lblVariables.Text = "可用变量:";
            lblVariables.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtExpression
            // 
            txtExpression.Cursor = Cursors.IBeam;
            txtExpression.Font = new Font("Consolas", 10F);
            txtExpression.Location = new Point(10, 60);
            txtExpression.Margin = new Padding(4, 5, 4, 5);
            txtExpression.MinimumSize = new Size(1, 16);
            txtExpression.Multiline = true;
            txtExpression.Name = "txtExpression";
            txtExpression.Padding = new Padding(5);
            txtExpression.RectColor = Color.FromArgb(65, 100, 204);
            txtExpression.ShowScrollBar = true;
            txtExpression.ShowText = false;
            txtExpression.Size = new Size(620, 80);
            txtExpression.TabIndex = 1;
            txtExpression.TextAlignment = ContentAlignment.TopLeft;
            toolTip.SetToolTip(txtExpression, "在此输入表达式，例如: {Variable1} + {Variable2} * 10");
            txtExpression.Watermark = "在此输入表达式，例如: {Variable1} + {Variable2} * 10";
            // 
            // lblExpression
            // 
            lblExpression.BackColor = Color.Transparent;
            lblExpression.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            lblExpression.ForeColor = Color.FromArgb(48, 48, 48);
            lblExpression.Location = new Point(10, 30);
            lblExpression.Name = "lblExpression";
            lblExpression.Size = new Size(100, 25);
            lblExpression.TabIndex = 0;
            lblExpression.Text = "表达式:";
            lblExpression.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpButtons
            // 
            grpButtons.BackColor = Color.Transparent;
            grpButtons.Controls.Add(btnCancel);
            grpButtons.Controls.Add(btnOK);
            grpButtons.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpButtons.Location = new Point(10, 440);
            grpButtons.Margin = new Padding(4, 5, 4, 5);
            grpButtons.MinimumSize = new Size(1, 1);
            grpButtons.Name = "grpButtons";
            grpButtons.Padding = new Padding(0, 32, 0, 0);
            grpButtons.RectColor = Color.Transparent;
            grpButtons.Size = new Size(870, 80);
            grpButtons.TabIndex = 1;
            grpButtons.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(230, 80, 80);
            btnCancel.FillColor2 = Color.FromArgb(230, 80, 80);
            btnCancel.Font = new Font("微软雅黑", 10F);
            btnCancel.Location = new Point(640, 20);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 12;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            toolTip.SetToolTip(btnCancel, "取消编辑并关闭对话框");
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.FillColor = Color.FromArgb(65, 100, 204);
            btnOK.FillColor2 = Color.FromArgb(65, 100, 204);
            btnOK.Font = new Font("微软雅黑", 10F);
            btnOK.Location = new Point(750, 20);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(100, 40);
            btnOK.Symbol = 61528;
            btnOK.TabIndex = 13;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("微软雅黑", 9F);
            toolTip.SetToolTip(btnOK, "应用当前表达式并关闭对话框");
            // 
            // ExpressionBuilderDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(890, 530);
            Controls.Add(grpButtons);
            Controls.Add(grpExpression);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExpressionBuilderDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "表达式构建器";
            ZoomScaleRect = new Rectangle(15, 15, 890, 530);
            grpExpression.ResumeLayout(false);
            grpButtons.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private UIGroupBox grpExpression;
        private UIGroupBox grpButtons;
        private UILabel lblExpression;
        private UITextBox txtExpression;
        private UILabel lblVariables;
        private UIListBox lstVariables;
        private UILabel lblFunctions;
        private UIListBox lstFunctions;
        private UILabel lblOperators;
        private UIListBox lstOperators;
        private UILabel lblPreview;
        private UIRichTextBox rtbPreview;
        private UILabel lblValidationResult;
        private UIButton btnValidate;
        private UISymbolButton btnOK;
        private UISymbolButton btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
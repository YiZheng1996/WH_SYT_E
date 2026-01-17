using Font = System.Drawing.Font;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_SystemPrompt
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            BtnSave = new UISymbolButton();
            txtPromptContent = new UIRichTextBox();
            lblPromptContent = new UILine();
            lblDialogType = new UILine();
            cmbDialogType = new UIComboBox();
            lblMessageLevel = new UILine();
            cmbMessageLevel = new UIComboBox();
            lblResultVariable = new UILine();
            txtResultVariable = new UITextBox();
            pnlResultVariable = new Panel();
            btnCancel = new UISymbolButton();
            pnlResultVariable.SuspendLayout();
            SuspendLayout();
            // 
            // BtnSave
            // 
            BtnSave.Cursor = Cursors.Hand;
            BtnSave.Font = new Font("微软雅黑", 12F);
            BtnSave.Location = new Point(60, 418);
            BtnSave.MinimumSize = new Size(1, 1);
            BtnSave.Name = "BtnSave";
            BtnSave.Radius = 20;
            BtnSave.Size = new Size(123, 41);
            BtnSave.Symbol = 61530;
            BtnSave.TabIndex = 7;
            BtnSave.Text = "保存";
            BtnSave.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnSave.Click += BtnSave_Click;
            // 
            // txtPromptContent
            // 
            txtPromptContent.FillColor = Color.White;
            txtPromptContent.Font = new Font("微软雅黑", 12F);
            txtPromptContent.Location = new Point(37, 80);
            txtPromptContent.Margin = new Padding(4, 5, 4, 5);
            txtPromptContent.MinimumSize = new Size(1, 1);
            txtPromptContent.Name = "txtPromptContent";
            txtPromptContent.Padding = new Padding(2);
            txtPromptContent.Radius = 10;
            txtPromptContent.RectColor = Color.White;
            txtPromptContent.ShowText = false;
            txtPromptContent.Size = new Size(352, 160);
            txtPromptContent.TabIndex = 1;
            txtPromptContent.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblPromptContent
            // 
            lblPromptContent.BackColor = Color.Transparent;
            lblPromptContent.EndCap = UILineCap.Circle;
            lblPromptContent.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            lblPromptContent.ForeColor = Color.FromArgb(48, 48, 48);
            lblPromptContent.LineColor = Color.White;
            lblPromptContent.Location = new Point(37, 48);
            lblPromptContent.MinimumSize = new Size(1, 1);
            lblPromptContent.Name = "lblPromptContent";
            lblPromptContent.Size = new Size(352, 29);
            lblPromptContent.TabIndex = 0;
            lblPromptContent.Text = "提示内容";
            lblPromptContent.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDialogType
            // 
            lblDialogType.BackColor = Color.Transparent;
            lblDialogType.EndCap = UILineCap.Circle;
            lblDialogType.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            lblDialogType.ForeColor = Color.FromArgb(48, 48, 48);
            lblDialogType.LineColor = Color.White;
            lblDialogType.Location = new Point(229, 248);
            lblDialogType.MinimumSize = new Size(1, 1);
            lblDialogType.Name = "lblDialogType";
            lblDialogType.Size = new Size(160, 29);
            lblDialogType.TabIndex = 4;
            lblDialogType.Text = "对话框类型";
            lblDialogType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbDialogType
            // 
            cmbDialogType.BackColor = Color.Transparent;
            cmbDialogType.DataSource = null;
            cmbDialogType.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbDialogType.FillColor = Color.White;
            cmbDialogType.Font = new Font("微软雅黑", 12F);
            cmbDialogType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDialogType.Items.AddRange(new object[] { "确认", "是/否", "确认/取消" });
            cmbDialogType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDialogType.Location = new Point(229, 280);
            cmbDialogType.Margin = new Padding(4, 5, 4, 5);
            cmbDialogType.MinimumSize = new Size(63, 0);
            cmbDialogType.Name = "cmbDialogType";
            cmbDialogType.Padding = new Padding(0, 0, 30, 2);
            cmbDialogType.Radius = 10;
            cmbDialogType.RectColor = Color.Gainsboro;
            cmbDialogType.Size = new Size(160, 36);
            cmbDialogType.SymbolSize = 24;
            cmbDialogType.TabIndex = 5;
            cmbDialogType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDialogType.Watermark = "";
            cmbDialogType.SelectedIndexChanged += CmbDialogType_SelectedIndexChanged;
            // 
            // lblMessageLevel
            // 
            lblMessageLevel.BackColor = Color.Transparent;
            lblMessageLevel.EndCap = UILineCap.Circle;
            lblMessageLevel.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            lblMessageLevel.ForeColor = Color.FromArgb(48, 48, 48);
            lblMessageLevel.LineColor = Color.White;
            lblMessageLevel.Location = new Point(37, 248);
            lblMessageLevel.MinimumSize = new Size(1, 1);
            lblMessageLevel.Name = "lblMessageLevel";
            lblMessageLevel.Size = new Size(160, 29);
            lblMessageLevel.TabIndex = 2;
            lblMessageLevel.Text = "提示等级";
            lblMessageLevel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbMessageLevel
            // 
            cmbMessageLevel.BackColor = Color.Transparent;
            cmbMessageLevel.DataSource = null;
            cmbMessageLevel.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbMessageLevel.FillColor = Color.White;
            cmbMessageLevel.Font = new Font("微软雅黑", 12F);
            cmbMessageLevel.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbMessageLevel.Items.AddRange(new object[] { "信息", "警告", "错误", "询问" });
            cmbMessageLevel.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbMessageLevel.Location = new Point(37, 280);
            cmbMessageLevel.Margin = new Padding(4, 5, 4, 5);
            cmbMessageLevel.MinimumSize = new Size(63, 0);
            cmbMessageLevel.Name = "cmbMessageLevel";
            cmbMessageLevel.Padding = new Padding(0, 0, 30, 2);
            cmbMessageLevel.Radius = 10;
            cmbMessageLevel.RectColor = Color.Gainsboro;
            cmbMessageLevel.Size = new Size(160, 36);
            cmbMessageLevel.SymbolSize = 24;
            cmbMessageLevel.TabIndex = 3;
            cmbMessageLevel.TextAlignment = ContentAlignment.MiddleLeft;
            cmbMessageLevel.Watermark = "";
            // 
            // lblResultVariable
            // 
            lblResultVariable.BackColor = Color.Transparent;
            lblResultVariable.Dock = DockStyle.Top;
            lblResultVariable.EndCap = UILineCap.Circle;
            lblResultVariable.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            lblResultVariable.ForeColor = Color.FromArgb(48, 48, 48);
            lblResultVariable.LineColor = Color.White;
            lblResultVariable.Location = new Point(0, 0);
            lblResultVariable.MinimumSize = new Size(1, 1);
            lblResultVariable.Name = "lblResultVariable";
            lblResultVariable.Size = new Size(352, 29);
            lblResultVariable.TabIndex = 0;
            lblResultVariable.Text = "结果保存变量（true/false）";
            lblResultVariable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtResultVariable
            // 
            txtResultVariable.Cursor = Cursors.IBeam;
            txtResultVariable.Font = new Font("微软雅黑", 12F);
            txtResultVariable.Location = new Point(0, 32);
            txtResultVariable.Margin = new Padding(4, 5, 4, 5);
            txtResultVariable.MinimumSize = new Size(1, 16);
            txtResultVariable.Name = "txtResultVariable";
            txtResultVariable.Padding = new Padding(5);
            txtResultVariable.Radius = 10;
            txtResultVariable.RectColor = Color.White;
            txtResultVariable.ShowText = false;
            txtResultVariable.Size = new Size(352, 36);
            txtResultVariable.TabIndex = 1;
            txtResultVariable.TextAlignment = ContentAlignment.MiddleLeft;
            txtResultVariable.Watermark = "请输入变量名，如：UserChoice";
            // 
            // pnlResultVariable
            // 
            pnlResultVariable.BackColor = Color.Transparent;
            pnlResultVariable.Controls.Add(lblResultVariable);
            pnlResultVariable.Controls.Add(txtResultVariable);
            pnlResultVariable.Location = new Point(37, 328);
            pnlResultVariable.Name = "pnlResultVariable";
            pnlResultVariable.Size = new Size(352, 70);
            pnlResultVariable.TabIndex = 6;
            pnlResultVariable.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Font = new Font("微软雅黑", 12F);
            btnCancel.Location = new Point(229, 418);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 20;
            btnCancel.Size = new Size(123, 41);
            btnCancel.Symbol = 61530;
            btnCancel.TabIndex = 8;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Click += btnCancel_Click;
            // 
            // Form_SystemPrompt
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(427, 472);
            ControlBox = false;
            Controls.Add(btnCancel);
            Controls.Add(pnlResultVariable);
            Controls.Add(cmbDialogType);
            Controls.Add(lblDialogType);
            Controls.Add(cmbMessageLevel);
            Controls.Add(lblMessageLevel);
            Controls.Add(lblPromptContent);
            Controls.Add(txtPromptContent);
            Controls.Add(BtnSave);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_SystemPrompt";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "系统提示";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            FormClosed += Form_SystemPrompt_FormClosed;
            pnlResultVariable.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Sunny.UI.UISymbolButton BtnSave;
        private Sunny.UI.UIRichTextBox txtPromptContent;
        private Sunny.UI.UILine lblPromptContent;
        private Sunny.UI.UILine lblDialogType;
        private Sunny.UI.UIComboBox cmbDialogType;
        private Sunny.UI.UILine lblMessageLevel;
        private Sunny.UI.UIComboBox cmbMessageLevel;
        private Sunny.UI.UILine lblResultVariable;
        private Sunny.UI.UITextBox txtResultVariable;
        private Panel pnlResultVariable;
        private UISymbolButton btnCancel;
    }
}
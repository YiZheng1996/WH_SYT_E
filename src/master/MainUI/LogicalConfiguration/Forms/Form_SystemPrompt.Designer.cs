using Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;
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
            BtnSave = new Sunny.UI.UISymbolButton();
            txtPromptContent = new Sunny.UI.UIRichTextBox();
            uiLine2 = new Sunny.UI.UILine();
            uiLine1 = new Sunny.UI.UILine();
            cmbDialogType = new Sunny.UI.UIComboBox();
            chkWaitResponse = new Sunny.UI.UICheckBox();
            SuspendLayout();
            // 
            // BtnSave
            // 
            BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            BtnSave.Font = new Font("微软雅黑", 12F);
            BtnSave.Location = new Point(152, 385);
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
            txtPromptContent.Font = new Font("微软雅黑", 12.75F);
            txtPromptContent.Location = new Point(37, 85);
            txtPromptContent.Margin = new Padding(4, 5, 4, 5);
            txtPromptContent.MinimumSize = new Size(1, 1);
            txtPromptContent.Name = "txtPromptContent";
            txtPromptContent.Padding = new Padding(2);
            txtPromptContent.Radius = 10;
            txtPromptContent.RectColor = Color.White;
            txtPromptContent.ShowText = false;
            txtPromptContent.Size = new Size(352, 150);
            txtPromptContent.TabIndex = 8;
            txtPromptContent.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = Sunny.UI.UILineCap.Circle;
            uiLine2.Font = new System.Drawing.Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new System.Drawing.Point(37, 48);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(352, 29);
            uiLine2.TabIndex = 9;
            uiLine2.Text = "提示内容";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.EndCap = Sunny.UI.UILineCap.Circle;
            uiLine1.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.White;
            uiLine1.Location = new Point(37, 245);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(352, 29);
            uiLine1.TabIndex = 10;
            uiLine1.Text = "对话框类型";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbDialogType
            // 
            cmbDialogType.DataSource = null;
            cmbDialogType.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cmbDialogType.FillColor = Color.White;
            cmbDialogType.Font = new Font("微软雅黑", 12F);
            cmbDialogType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDialogType.Items.AddRange(new object[] { "仅消息提示", "是/否", "是/否/取消", "确定/取消", "仅确定" });
            cmbDialogType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDialogType.Location = new Point(37, 282);
            cmbDialogType.Margin = new Padding(4, 5, 4, 5);
            cmbDialogType.MinimumSize = new Size(63, 0);
            cmbDialogType.Name = "cmbDialogType";
            cmbDialogType.Padding = new Padding(0, 0, 30, 2);
            cmbDialogType.Radius = 10;
            cmbDialogType.Size = new Size(352, 36);
            cmbDialogType.SymbolSize = 24;
            cmbDialogType.TabIndex = 11;
            cmbDialogType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDialogType.Watermark = "";
            // 
            // chkWaitResponse
            // 
            chkWaitResponse.Checked = true;
            chkWaitResponse.Font = new Font("微软雅黑", 12F);
            chkWaitResponse.Location = new Point(37, 335);
            chkWaitResponse.MinimumSize = new Size(1, 1);
            chkWaitResponse.Name = "chkWaitResponse";
            chkWaitResponse.Size = new Size(200, 29);
            chkWaitResponse.TabIndex = 12;
            chkWaitResponse.Text = "等待用户响应";
            // 
            // Form_SystemPrompt
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(427, 450);
            Controls.Add(chkWaitResponse);
            Controls.Add(cmbDialogType);
            Controls.Add(uiLine1);
            Controls.Add(uiLine2);
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
            ResumeLayout(false);
        }

        private Sunny.UI.UISymbolButton BtnSave;
        private Sunny.UI.UIRichTextBox txtPromptContent;
        private Sunny.UI.UILine uiLine2;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UIComboBox cmbDialogType;
        private Sunny.UI.UICheckBox chkWaitResponse;
    }
}
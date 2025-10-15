namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class Form_SystemPrompt
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
            BtnSave = new UISymbolButton();
            txtPromptContent = new UIRichTextBox();
            uiLine2 = new UILine();
            SuspendLayout();
            // 
            // BtnSave
            // 
            BtnSave.Cursor = Cursors.Hand;
            BtnSave.FillColor = Color.DodgerBlue;
            BtnSave.FillColor2 = Color.DodgerBlue;
            BtnSave.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnSave.LightColor = Color.FromArgb(248, 248, 248);
            BtnSave.Location = new Point(147, 284);
            BtnSave.MinimumSize = new Size(1, 1);
            BtnSave.Name = "BtnSave";
            BtnSave.RectColor = Color.DodgerBlue;
            BtnSave.RectDisableColor = Color.DodgerBlue;
            BtnSave.Size = new Size(132, 39);
            BtnSave.Style = UIStyle.Custom;
            BtnSave.Symbol = 61639;
            BtnSave.SymbolSize = 32;
            BtnSave.TabIndex = 7;
            BtnSave.Text = "保存";
            BtnSave.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnSave.Click += BtnSave_Click;
            // 
            // txtPromptContent
            // 
            txtPromptContent.FillColor = Color.White;
            txtPromptContent.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtPromptContent.Location = new Point(37, 85);
            txtPromptContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtPromptContent.MinimumSize = new Size(1, 1);
            txtPromptContent.Name = "txtPromptContent";
            txtPromptContent.Padding = new System.Windows.Forms.Padding(2);
            txtPromptContent.Radius = 10;
            txtPromptContent.RectColor = Color.White;
            txtPromptContent.ShowText = false;
            txtPromptContent.Size = new Size(352, 186);
            txtPromptContent.TabIndex = 8;
            txtPromptContent.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(37, 48);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(352, 29);
            uiLine2.TabIndex = 9;
            uiLine2.Text = "提示内容";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_SystemPrompt
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(427, 335);
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

        #endregion

        private UISymbolButton BtnSave;
        private UIRichTextBox txtPromptContent;
        private UILine uiLine2;
    }
}
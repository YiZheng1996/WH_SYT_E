namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class Form_SaveReport
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
            uiLine2 = new UILine();
            txtRePortPath = new UIRichTextBox();
            BtnSave = new UISymbolButton();
            BtnOpenPath = new UISymbolButton();
            SuspendLayout();
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(47, 50);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(352, 29);
            uiLine2.TabIndex = 12;
            uiLine2.Text = "报表路径";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtRePortPath
            // 
            txtRePortPath.FillColor = Color.White;
            txtRePortPath.FillColor2 = Color.White;
            txtRePortPath.Font = new Font("微软雅黑", 13F);
            txtRePortPath.Location = new Point(47, 87);
            txtRePortPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtRePortPath.MinimumSize = new Size(1, 1);
            txtRePortPath.Name = "txtRePortPath";
            txtRePortPath.Padding = new System.Windows.Forms.Padding(2);
            txtRePortPath.Radius = 10;
            txtRePortPath.ReadOnly = true;
            txtRePortPath.RectColor = Color.White;
            txtRePortPath.RectDisableColor = Color.White;
            txtRePortPath.ShowText = false;
            txtRePortPath.Size = new Size(352, 186);
            txtRePortPath.TabIndex = 11;
            txtRePortPath.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // BtnSave
            // 
            BtnSave.Cursor = Cursors.Hand;
            BtnSave.FillColor = Color.DodgerBlue;
            BtnSave.FillColor2 = Color.DodgerBlue;
            BtnSave.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnSave.LightColor = Color.FromArgb(248, 248, 248);
            BtnSave.Location = new Point(258, 299);
            BtnSave.MinimumSize = new Size(1, 1);
            BtnSave.Name = "BtnSave";
            BtnSave.RectColor = Color.DodgerBlue;
            BtnSave.RectDisableColor = Color.DodgerBlue;
            BtnSave.Size = new Size(132, 39);
            BtnSave.Style = UIStyle.Custom;
            BtnSave.Symbol = 61639;
            BtnSave.SymbolSize = 32;
            BtnSave.TabIndex = 10;
            BtnSave.Text = "保存";
            BtnSave.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnOpenPath
            // 
            BtnOpenPath.Cursor = Cursors.Hand;
            BtnOpenPath.FillColor = Color.DodgerBlue;
            BtnOpenPath.FillColor2 = Color.DodgerBlue;
            BtnOpenPath.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnOpenPath.LightColor = Color.FromArgb(248, 248, 248);
            BtnOpenPath.Location = new Point(57, 299);
            BtnOpenPath.MinimumSize = new Size(1, 1);
            BtnOpenPath.Name = "BtnOpenPath";
            BtnOpenPath.RectColor = Color.DodgerBlue;
            BtnOpenPath.RectDisableColor = Color.DodgerBlue;
            BtnOpenPath.Size = new Size(132, 39);
            BtnOpenPath.Style = UIStyle.Custom;
            BtnOpenPath.Symbol = 257733;
            BtnOpenPath.SymbolSize = 32;
            BtnOpenPath.TabIndex = 13;
            BtnOpenPath.Text = "选择";
            BtnOpenPath.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnOpenPath.Click += BtnOpenPath_Click;
            // 
            // Form_SaveReport
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(447, 357);
            Controls.Add(BtnOpenPath);
            Controls.Add(uiLine2);
            Controls.Add(txtRePortPath);
            Controls.Add(BtnSave);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_SaveReport";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "保存报表";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ResumeLayout(false);
        }

        #endregion

        private UILine uiLine2;
        private UIRichTextBox txtRePortPath;
        private UISymbolButton BtnSave;
        private UISymbolButton BtnOpenPath;
    }
}
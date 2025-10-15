namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class Form_DelayTime
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
            txtTime = new UITextBox();
            uiLabel2 = new UILabel();
            uiLabel1 = new UILabel();
            SuspendLayout();
            // 
            // BtnSave
            // 
            BtnSave.Cursor = Cursors.Hand;
            BtnSave.FillColor = Color.DodgerBlue;
            BtnSave.FillColor2 = Color.DodgerBlue;
            BtnSave.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            BtnSave.LightColor = Color.FromArgb(248, 248, 248);
            BtnSave.Location = new Point(110, 150);
            BtnSave.MinimumSize = new Size(1, 1);
            BtnSave.Name = "BtnSave";
            BtnSave.RectColor = Color.DodgerBlue;
            BtnSave.RectDisableColor = Color.DodgerBlue;
            BtnSave.Size = new Size(132, 39);
            BtnSave.Style = UIStyle.Custom;
            BtnSave.Symbol = 61639;
            BtnSave.SymbolSize = 36;
            BtnSave.TabIndex = 7;
            BtnSave.Text = "保存";
            BtnSave.TipsFont = new Font("微软雅黑", 12F);
            //BtnSave.Click += BtnSave_Click;
            // 
            // txtTime
            // 
            txtTime.ButtonFillColor = Color.FromArgb(140, 140, 140);
            txtTime.ButtonFillHoverColor = Color.FromArgb(163, 163, 163);
            txtTime.ButtonFillPressColor = Color.FromArgb(112, 112, 112);
            txtTime.ButtonRectColor = Color.FromArgb(140, 140, 140);
            txtTime.ButtonRectHoverColor = Color.FromArgb(163, 163, 163);
            txtTime.ButtonRectPressColor = Color.FromArgb(112, 112, 112);
            txtTime.ButtonStyleInherited = false;
            txtTime.Cursor = Cursors.IBeam;
            txtTime.DoubleValue = 200D;
            txtTime.FillColor2 = Color.FromArgb(248, 248, 248);
            txtTime.Font = new Font("宋体", 15F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtTime.IntValue = 200;
            txtTime.Location = new Point(110, 77);
            txtTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtTime.MinimumSize = new Size(1, 16);
            txtTime.Name = "txtTime";
            txtTime.Padding = new System.Windows.Forms.Padding(5);
            txtTime.RectColor = Color.White;
            txtTime.ScrollBarColor = Color.FromArgb(140, 140, 140);
            txtTime.ScrollBarStyleInherited = false;
            txtTime.ShowText = false;
            txtTime.Size = new Size(150, 29);
            txtTime.Style = UIStyle.Custom;
            txtTime.TabIndex = 6;
            txtTime.Text = "200";
            txtTime.TextAlignment = ContentAlignment.MiddleLeft;
            txtTime.Type = UITextBox.UIEditType.Integer;
            txtTime.Watermark = "";
            // 
            // uiLabel2
            // 
            uiLabel2.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(267, 67);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(57, 49);
            uiLabel2.TabIndex = 4;
            uiLabel2.Text = "ms";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            uiLabel1.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(32, 67);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(71, 49);
            uiLabel1.TabIndex = 5;
            uiLabel1.Text = "延时";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_DelayTime
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(378, 210);
            ControlBoxFillHoverColor = Color.FromArgb(163, 163, 163);
            Controls.Add(BtnSave);
            Controls.Add(txtTime);
            Controls.Add(uiLabel2);
            Controls.Add(uiLabel1);
            Font = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_DelayTime";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Style = UIStyle.Custom;
            Text = "延时设置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UISymbolButton BtnSave;
        private Sunny.UI.UITextBox txtTime;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel1;
    }
}
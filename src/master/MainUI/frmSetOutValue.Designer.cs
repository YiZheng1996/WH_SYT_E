namespace MainUI
{
    partial class frmSetOutValue
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
            Btn_Ok = new UIButton();
            Btn_Exit = new UIButton();
            grpDI = new UIGroupBox();
            nudOutputValue = new UITextBox();
            grpDI.SuspendLayout();
            SuspendLayout();
            // 
            // Btn_Ok
            // 
            Btn_Ok.Cursor = Cursors.Hand;
            Btn_Ok.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            Btn_Ok.ForeDisableColor = Color.White;
            Btn_Ok.Location = new Point(225, 177);
            Btn_Ok.MinimumSize = new Size(1, 1);
            Btn_Ok.Name = "Btn_Ok";
            Btn_Ok.RectDisableColor = Color.FromArgb(80, 160, 255);
            Btn_Ok.Size = new Size(120, 40);
            Btn_Ok.TabIndex = 389;
            Btn_Ok.Text = "确定";
            Btn_Ok.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Btn_Ok.TipsText = "1";
            Btn_Ok.Click += Btn_Ok_Click;
            // 
            // Btn_Exit
            // 
            Btn_Exit.Cursor = Cursors.Hand;
            Btn_Exit.DialogResult = DialogResult.Cancel;
            Btn_Exit.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            Btn_Exit.ForeDisableColor = Color.White;
            Btn_Exit.Location = new Point(366, 177);
            Btn_Exit.MinimumSize = new Size(1, 1);
            Btn_Exit.Name = "Btn_Exit";
            Btn_Exit.RectDisableColor = Color.FromArgb(80, 160, 255);
            Btn_Exit.Size = new Size(120, 40);
            Btn_Exit.TabIndex = 390;
            Btn_Exit.Text = "取消";
            Btn_Exit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Btn_Exit.TipsText = "1";
            Btn_Exit.Click += Btn_Exit_Click;
            // 
            // grpDI
            // 
            grpDI.BackColor = Color.White;
            grpDI.Controls.Add(nudOutputValue);
            grpDI.FillColor = Color.White;
            grpDI.FillColor2 = Color.White;
            grpDI.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            grpDI.ForeColor = Color.FromArgb(46, 46, 46);
            grpDI.ForeDisableColor = Color.FromArgb(46, 46, 46);
            grpDI.Location = new Point(12, 48);
            grpDI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpDI.MinimumSize = new Size(1, 1);
            grpDI.Name = "grpDI";
            grpDI.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpDI.RectColor = Color.FromArgb(49, 54, 64);
            grpDI.RectDisableColor = Color.FromArgb(49, 54, 64);
            grpDI.Size = new Size(474, 108);
            grpDI.TabIndex = 391;
            grpDI.Text = "请输入数值";
            grpDI.TextAlignment = ContentAlignment.MiddleCenter;
            grpDI.TitleAlignment = HorizontalAlignment.Center;
            // 
            // nudOutputValue
            // 
            nudOutputValue.ButtonWidth = 100;
            nudOutputValue.CanEmpty = true;
            nudOutputValue.Cursor = Cursors.IBeam;
            nudOutputValue.FillColor = Color.FromArgb(218, 220, 230);
            nudOutputValue.FillColor2 = Color.FromArgb(218, 220, 230);
            nudOutputValue.FillDisableColor = Color.FromArgb(218, 220, 230);
            nudOutputValue.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            nudOutputValue.FocusedSelectAll = true;
            nudOutputValue.Font = new Font("思源黑体 CN Bold", 30F, FontStyle.Bold);
            nudOutputValue.ForeColor = Color.FromArgb(46, 46, 46);
            nudOutputValue.ForeDisableColor = Color.FromArgb(46, 46, 46);
            nudOutputValue.ForeReadOnlyColor = Color.FromArgb(46, 46, 46);
            nudOutputValue.Location = new Point(69, 39);
            nudOutputValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            nudOutputValue.MinimumSize = new Size(1, 1);
            nudOutputValue.Name = "nudOutputValue";
            nudOutputValue.Padding = new System.Windows.Forms.Padding(5);
            nudOutputValue.RectColor = Color.FromArgb(218, 220, 230);
            nudOutputValue.RectDisableColor = Color.FromArgb(218, 220, 230);
            nudOutputValue.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            nudOutputValue.ShowText = false;
            nudOutputValue.Size = new Size(354, 54);
            nudOutputValue.TabIndex = 5;
            nudOutputValue.Text = "0.00";
            nudOutputValue.TextAlignment = ContentAlignment.MiddleCenter;
            nudOutputValue.Type = UITextBox.UIEditType.Double;
            nudOutputValue.Watermark = "请输入";
            nudOutputValue.KeyPress += nudOutputValue_KeyPress;
            // 
            // frmSetOutValue
            // 
            AcceptButton = Btn_Ok;
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = Btn_Exit;
            ClientSize = new Size(501, 233);
            ControlBox = false;
            Controls.Add(grpDI);
            Controls.Add(Btn_Exit);
            Controls.Add(Btn_Ok);
            Font = new Font("宋体", 10F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSetOutValue";
            RectColor = Color.FromArgb(47, 55, 64);
            Text = "frmSetOutValue";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 501, 201);
            grpDI.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UIButton Btn_Ok;
        private Sunny.UI.UIButton Btn_Exit;
        private Sunny.UI.UIGroupBox grpDI;
        private Sunny.UI.UITextBox nudOutputValue;
    }
}
using Report;

namespace MainUI
{
    partial class frmDispReport
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
            BtnPrint = new UIButton();
            BtnClose = new UIButton();
            btnPageUp = new UIButton();
            btnPageDown = new UIButton();
            inputNumber = new NumericUpDown();
            grpReport = new UIPanel();
            ((System.ComponentModel.ISupportInitialize)inputNumber).BeginInit();
            SuspendLayout();
            // 
            // BtnPrint
            // 
            BtnPrint.BackColor = Color.Transparent;
            BtnPrint.Cursor = Cursors.Hand;
            BtnPrint.FillDisableColor = Color.FromArgb(70, 75, 85);
            BtnPrint.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            BtnPrint.ForeDisableColor = Color.White;
            BtnPrint.Location = new Point(588, 762);
            BtnPrint.MinimumSize = new Size(1, 1);
            BtnPrint.Name = "BtnPrint";
            BtnPrint.Radius = 10;
            BtnPrint.RectDisableColor = Color.FromArgb(80, 160, 255);
            BtnPrint.Size = new Size(120, 40);
            BtnPrint.TabIndex = 392;
            BtnPrint.Text = "打  印";
            BtnPrint.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnPrint.TipsText = "1";
            BtnPrint.Click += BtnPrint_Click;
            // 
            // BtnClose
            // 
            BtnClose.BackColor = Color.Transparent;
            BtnClose.Cursor = Cursors.Hand;
            BtnClose.FillDisableColor = Color.FromArgb(70, 75, 85);
            BtnClose.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            BtnClose.ForeDisableColor = Color.White;
            BtnClose.Location = new Point(731, 762);
            BtnClose.MinimumSize = new Size(1, 1);
            BtnClose.Name = "BtnClose";
            BtnClose.Radius = 10;
            BtnClose.RectDisableColor = Color.FromArgb(80, 160, 255);
            BtnClose.Size = new Size(120, 40);
            BtnClose.TabIndex = 393;
            BtnClose.Text = "退  出";
            BtnClose.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnClose.TipsText = "1";
            BtnClose.Click += BtnClose_Click;
            // 
            // btnPageUp
            // 
            btnPageUp.BackColor = Color.Transparent;
            btnPageUp.Cursor = Cursors.Hand;
            btnPageUp.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnPageUp.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnPageUp.ForeDisableColor = Color.White;
            btnPageUp.Location = new Point(113, 762);
            btnPageUp.MinimumSize = new Size(1, 1);
            btnPageUp.Name = "btnPageUp";
            btnPageUp.Radius = 10;
            btnPageUp.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnPageUp.Size = new Size(120, 40);
            btnPageUp.TabIndex = 400;
            btnPageUp.Text = "上翻";
            btnPageUp.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnPageUp.TipsText = "1";
            btnPageUp.Click += btnPageUp_Click;
            // 
            // btnPageDown
            // 
            btnPageDown.BackColor = Color.Transparent;
            btnPageDown.Cursor = Cursors.Hand;
            btnPageDown.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnPageDown.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnPageDown.ForeDisableColor = Color.White;
            btnPageDown.Location = new Point(309, 762);
            btnPageDown.MinimumSize = new Size(1, 1);
            btnPageDown.Name = "btnPageDown";
            btnPageDown.Radius = 10;
            btnPageDown.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnPageDown.Size = new Size(120, 40);
            btnPageDown.TabIndex = 401;
            btnPageDown.Text = "下翻";
            btnPageDown.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnPageDown.TipsText = "1";
            btnPageDown.Click += btnPageDown_Click;
            // 
            // inputNumber
            // 
            inputNumber.Font = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            inputNumber.Location = new Point(241, 768);
            inputNumber.Name = "inputNumber";
            inputNumber.Size = new Size(55, 27);
            inputNumber.TabIndex = 402;
            inputNumber.TextAlign = HorizontalAlignment.Center;
            inputNumber.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // grpReport
            // 
            grpReport.BackColor = Color.Transparent;
            grpReport.FillColor = Color.White;
            grpReport.FillColor2 = Color.White;
            grpReport.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpReport.Location = new Point(19, 49);
            grpReport.Margin = new Padding(4, 5, 4, 5);
            grpReport.MinimumSize = new Size(1, 1);
            grpReport.Name = "grpReport";
            grpReport.Radius = 10;
            grpReport.RectColor = Color.White;
            grpReport.RectDisableColor = Color.White;
            grpReport.Size = new Size(918, 702);
            grpReport.TabIndex = 403;
            grpReport.Text = null;
            grpReport.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // frmDispReport
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(956, 813);
            Controls.Add(grpReport);
            Controls.Add(inputNumber);
            Controls.Add(btnPageUp);
            Controls.Add(btnPageDown);
            Controls.Add(BtnClose);
            Controls.Add(BtnPrint);
            Font = new Font("微软雅黑", 9F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmDispReport";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "试验结果";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            TopMost = true;
            ZoomScaleRect = new Rectangle(15, 15, 903, 724);
            Load += frmDispReport_Load;
            ((System.ComponentModel.ISupportInitialize)inputNumber).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UIButton BtnPrint;
        private Sunny.UI.UIButton BtnClose;
        private Sunny.UI.UIButton btnPageUp;
        private Sunny.UI.UIButton btnPageDown;
        private System.Windows.Forms.NumericUpDown inputNumber;
        private UIPanel grpReport;
    }
}
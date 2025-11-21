namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_RealtimeMonitorPrompt
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
            panelMain = new Panel();
            panelMessage = new UIPanel();
            lblMessage = new UILabel();
            panelValueDisplay = new UIPanel();
            lblValueLabel = new UILabel();
            lblValue = new UILabel();
            panelBottom = new Panel();
            btnConfirm = new UIButton();
            panelMain.SuspendLayout();
            panelMessage.SuspendLayout();
            panelValueDisplay.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(248, 249, 250);
            panelMain.Controls.Add(panelMessage);
            panelMain.Controls.Add(panelValueDisplay);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15, 20, 15, 20);
            panelMain.Size = new Size(600, 290);
            panelMain.TabIndex = 0;
            // 
            // panelMessage
            // 
            panelMessage.BackColor = Color.White;
            panelMessage.Controls.Add(lblMessage);
            panelMessage.FillColor = Color.White;
            panelMessage.Font = new Font("微软雅黑", 12F);
            panelMessage.Location = new Point(250, 30);
            panelMessage.Margin = new Padding(4, 5, 4, 5);
            panelMessage.MinimumSize = new Size(1, 1);
            panelMessage.Name = "panelMessage";
            panelMessage.Padding = new Padding(15);
            panelMessage.RectColor = Color.FromArgb(220, 220, 220);
            panelMessage.Size = new Size(320, 180);
            panelMessage.TabIndex = 1;
            panelMessage.Text = null;
            panelMessage.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            lblMessage.Dock = DockStyle.Fill;
            lblMessage.Font = new Font("微软雅黑", 14F);
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(15, 15);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(290, 150);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "转动高压调阀螺钉(下调)\r\n来进行调整";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelValueDisplay
            // 
            panelValueDisplay.BackColor = Color.White;
            panelValueDisplay.Controls.Add(lblValueLabel);
            panelValueDisplay.Controls.Add(lblValue);
            panelValueDisplay.FillColor = Color.White;
            panelValueDisplay.Font = new Font("微软雅黑", 12F);
            panelValueDisplay.Location = new Point(30, 30);
            panelValueDisplay.Margin = new Padding(4, 5, 4, 5);
            panelValueDisplay.MinimumSize = new Size(1, 1);
            panelValueDisplay.Name = "panelValueDisplay";
            panelValueDisplay.Padding = new Padding(0, 10, 0, 10);
            panelValueDisplay.RectColor = Color.FromArgb(220, 220, 220);
            panelValueDisplay.Size = new Size(200, 180);
            panelValueDisplay.TabIndex = 0;
            panelValueDisplay.Text = null;
            panelValueDisplay.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblValueLabel
            // 
            lblValueLabel.Font = new Font("微软雅黑", 11F);
            lblValueLabel.ForeColor = Color.Gray;
            lblValueLabel.Location = new Point(2, 140);
            lblValueLabel.Name = "lblValueLabel";
            lblValueLabel.Size = new Size(195, 30);
            lblValueLabel.TabIndex = 1;
            lblValueLabel.Text = "PE05(kPa)";
            lblValueLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblValue
            // 
            lblValue.Dock = DockStyle.Top;
            lblValue.Font = new Font("Arial", 48F, FontStyle.Bold);
            lblValue.ForeColor = Color.FromArgb(24, 144, 255);
            lblValue.Location = new Point(0, 10);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(200, 110);
            lblValue.TabIndex = 0;
            lblValue.Text = "0.0";
            lblValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(btnConfirm);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 325);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(15, 10, 15, 15);
            panelBottom.Size = new Size(600, 75);
            panelBottom.TabIndex = 1;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.None;
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.Font = new Font("微软雅黑", 11F);
            btnConfirm.Location = new Point(250, 17);
            btnConfirm.MinimumSize = new Size(1, 1);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(100, 40);
            btnConfirm.TabIndex = 0;
            btnConfirm.Text = "确定";
            btnConfirm.TipsFont = new Font("微软雅黑", 9F);
            btnConfirm.Click += BtnConfirm_Click;
            // 
            // Form_RealtimeMonitorPrompt
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(600, 400);
            ControlBox = false;
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_RealtimeMonitorPrompt";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Text = "实时监控提示";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 600, 400);
            FormClosing += Form_RealtimeMonitorPrompt_FormClosing;
            Load += Form_RealtimeMonitorPrompt_Load;
            panelMain.ResumeLayout(false);
            panelMessage.ResumeLayout(false);
            panelValueDisplay.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private Sunny.UI.UIPanel panelValueDisplay;
        private Sunny.UI.UILabel lblValue;
        private Sunny.UI.UILabel lblValueLabel;
        private Sunny.UI.UIPanel panelMessage;
        private Sunny.UI.UILabel lblMessage;
        private System.Windows.Forms.Panel panelBottom;
        private Sunny.UI.UIButton btnConfirm;
    }
}
namespace MainUI
{
    partial class frmHardWare
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
            panel1 = new Panel();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            label12 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            grpAI = new UIGroupBox();
            panel3 = new Panel();
            panel2 = new Panel();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            grpAO = new UIGroupBox();
            panel1.SuspendLayout();
            grpAI.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            grpAO.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(44, 62, 80);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Font = new Font("微软雅黑", 15F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(650, 38);
            panel1.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label2.ImeMode = ImeMode.NoControl;
            label2.Location = new Point(340, 7);
            label2.Name = "label2";
            label2.Size = new Size(66, 26);
            label2.TabIndex = 7;
            label2.Text = "增益值";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label3.ImeMode = ImeMode.NoControl;
            label3.Location = new Point(154, 7);
            label3.Name = "label3";
            label3.Size = new Size(66, 26);
            label3.TabIndex = 7;
            label3.Text = "测定值";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(250, 7);
            label1.Name = "label1";
            label1.Size = new Size(66, 26);
            label1.TabIndex = 7;
            label1.Text = "零点值";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("思源黑体 CN Bold", 17F, FontStyle.Bold);
            label12.ForeColor = Color.FromArgb(46, 46, 46);
            label12.ImeMode = ImeMode.NoControl;
            label12.Location = new Point(19, 615);
            label12.Name = "label12";
            label12.Size = new Size(481, 33);
            label12.TabIndex = 54;
            label12.Text = "计算公式：测定值 = 工程值 * 增益值 - 零点值  ";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // grpAI
            // 
            grpAI.BackColor = Color.White;
            grpAI.Controls.Add(panel3);
            grpAI.FillColor = Color.White;
            grpAI.FillColor2 = Color.White;
            grpAI.FillDisableColor = Color.FromArgb(49, 54, 64);
            grpAI.Font = new Font("思源黑体 CN Bold", 17F, FontStyle.Bold);
            grpAI.ForeColor = Color.FromArgb(46, 46, 46);
            grpAI.ForeDisableColor = Color.FromArgb(235, 227, 221);
            grpAI.Location = new Point(19, 65);
            grpAI.Margin = new Padding(4, 5, 4, 5);
            grpAI.MinimumSize = new Size(1, 1);
            grpAI.Name = "grpAI";
            grpAI.Padding = new Padding(0, 32, 0, 0);
            grpAI.Radius = 15;
            grpAI.RectColor = Color.White;
            grpAI.RectDisableColor = Color.White;
            grpAI.Size = new Size(650, 541);
            grpAI.TabIndex = 382;
            grpAI.Text = "输入检测";
            grpAI.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.BackColor = Color.White;
            panel3.Controls.Add(panel1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 32);
            panel3.Name = "panel3";
            panel3.Size = new Size(650, 509);
            panel3.TabIndex = 17;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(239, 154, 78);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label6);
            panel2.Dock = DockStyle.Top;
            panel2.Font = new Font("微软雅黑", 15F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panel2.Location = new Point(0, 32);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(42, 38);
            panel2.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(235, 227, 221);
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(340, 7);
            label4.Name = "label4";
            label4.Size = new Size(66, 26);
            label4.TabIndex = 7;
            label4.Text = "增益值";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(235, 227, 221);
            label5.ImeMode = ImeMode.NoControl;
            label5.Location = new Point(154, 7);
            label5.Name = "label5";
            label5.Size = new Size(66, 26);
            label5.TabIndex = 7;
            label5.Text = "测定值";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(235, 227, 221);
            label6.ImeMode = ImeMode.NoControl;
            label6.Location = new Point(250, 7);
            label6.Name = "label6";
            label6.Size = new Size(66, 26);
            label6.TabIndex = 7;
            label6.Text = "零点值";
            // 
            // grpAO
            // 
            grpAO.BackColor = Color.FromArgb(49, 54, 64);
            grpAO.Controls.Add(panel2);
            grpAO.FillColor = Color.FromArgb(49, 54, 64);
            grpAO.FillColor2 = Color.FromArgb(49, 54, 64);
            grpAO.FillDisableColor = Color.FromArgb(49, 54, 64);
            grpAO.Font = new Font("思源黑体 CN Bold", 17F, FontStyle.Bold);
            grpAO.ForeColor = Color.FromArgb(235, 227, 221);
            grpAO.Location = new Point(544, 619);
            grpAO.Margin = new Padding(4, 5, 4, 5);
            grpAO.MinimumSize = new Size(1, 1);
            grpAO.Name = "grpAO";
            grpAO.Padding = new Padding(0, 32, 0, 0);
            grpAO.RectColor = Color.FromArgb(49, 54, 64);
            grpAO.RectDisableColor = Color.FromArgb(49, 54, 64);
            grpAO.Size = new Size(42, 29);
            grpAO.TabIndex = 383;
            grpAO.Text = "输出检测";
            grpAO.TextAlignment = ContentAlignment.MiddleCenter;
            grpAO.Visible = false;
            // 
            // frmHardWare
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(696, 664);
            Controls.Add(grpAO);
            Controls.Add(grpAI);
            Controls.Add(label12);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmHardWare";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "硬件校准";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1270, 771);
            Load += frmHardWare_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            grpAI.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            grpAO.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.Timer timer1;
        private Sunny.UI.UIGroupBox grpAI;
        private System.Windows.Forms.Panel panel3;
        private Panel panel2;
        private Label label4;
        private Label label5;
        private Label label6;
        private UIGroupBox grpAO;
    }
}
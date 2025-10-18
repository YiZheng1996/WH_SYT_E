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
            ucCalibration6 = new MainUI.Procedure.UCCalibration();
            ucCalibration7 = new MainUI.Procedure.UCCalibration();
            ucCalibration5 = new MainUI.Procedure.UCCalibration();
            ucCalibration4 = new MainUI.Procedure.UCCalibration();
            ucCalibration3 = new MainUI.Procedure.UCCalibration();
            ucCalibration2 = new MainUI.Procedure.UCCalibration();
            ucCalibration1 = new MainUI.Procedure.UCCalibration();
            grpAO = new UIGroupBox();
            panel4 = new Panel();
            ucCalibration9 = new MainUI.Procedure.UCCalibration();
            ucCalibration10 = new MainUI.Procedure.UCCalibration();
            ucCalibration11 = new MainUI.Procedure.UCCalibration();
            ucCalibration12 = new MainUI.Procedure.UCCalibration();
            ucCalibration13 = new MainUI.Procedure.UCCalibration();
            ucCalibration14 = new MainUI.Procedure.UCCalibration();
            panel5 = new Panel();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            panel1.SuspendLayout();
            grpAI.SuspendLayout();
            panel3.SuspendLayout();
            grpAO.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
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
            panel1.Size = new Size(599, 38);
            panel1.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Bold);
            label2.ImeMode = ImeMode.NoControl;
            label2.Location = new Point(340, 7);
            label2.Name = "label2";
            label2.Size = new Size(67, 22);
            label2.TabIndex = 7;
            label2.Text = "增益值";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Bold);
            label3.ImeMode = ImeMode.NoControl;
            label3.Location = new Point(154, 7);
            label3.Name = "label3";
            label3.Size = new Size(67, 22);
            label3.TabIndex = 7;
            label3.Text = "测定值";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Bold);
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(250, 7);
            label1.Name = "label1";
            label1.Size = new Size(67, 22);
            label1.TabIndex = 7;
            label1.Text = "零点值";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 17F, FontStyle.Bold);
            label12.ForeColor = Color.FromArgb(46, 46, 46);
            label12.ImeMode = ImeMode.NoControl;
            label12.Location = new Point(385, 622);
            label12.Name = "label12";
            label12.Size = new Size(512, 29);
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
            grpAI.Font = new Font("Microsoft Sans Serif", 17F, FontStyle.Bold);
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
            grpAI.Size = new Size(599, 541);
            grpAI.TabIndex = 382;
            grpAI.Text = "输入检测";
            grpAI.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.BackColor = Color.White;
            panel3.Controls.Add(ucCalibration6);
            panel3.Controls.Add(ucCalibration7);
            panel3.Controls.Add(ucCalibration5);
            panel3.Controls.Add(ucCalibration4);
            panel3.Controls.Add(ucCalibration3);
            panel3.Controls.Add(ucCalibration2);
            panel3.Controls.Add(ucCalibration1);
            panel3.Controls.Add(panel1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 32);
            panel3.Name = "panel3";
            panel3.Size = new Size(599, 509);
            panel3.TabIndex = 17;
            // 
            // ucCalibration6
            // 
            ucCalibration6.Font = new Font("微软雅黑", 12F);
            ucCalibration6.GainValue = 0D;
            ucCalibration6.Index = 6;
            ucCalibration6.Location = new Point(7, 300);
            ucCalibration6.Margin = new Padding(4, 5, 4, 5);
            ucCalibration6.Name = "ucCalibration6";
            ucCalibration6.Size = new Size(567, 38);
            ucCalibration6.TabIndex = 17;
            ucCalibration6.Text = "PE07(kPa)";
            ucCalibration6.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration7
            // 
            ucCalibration7.Font = new Font("微软雅黑", 12F);
            ucCalibration7.GainValue = 0D;
            ucCalibration7.Index = 5;
            ucCalibration7.Location = new Point(7, 258);
            ucCalibration7.Margin = new Padding(4, 5, 4, 5);
            ucCalibration7.Name = "ucCalibration7";
            ucCalibration7.Size = new Size(567, 38);
            ucCalibration7.TabIndex = 16;
            ucCalibration7.Text = "PE06(kPa)";
            ucCalibration7.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration5
            // 
            ucCalibration5.Font = new Font("微软雅黑", 12F);
            ucCalibration5.GainValue = 0D;
            ucCalibration5.Index = 4;
            ucCalibration5.Location = new Point(7, 216);
            ucCalibration5.Margin = new Padding(4, 5, 4, 5);
            ucCalibration5.Name = "ucCalibration5";
            ucCalibration5.Size = new Size(567, 38);
            ucCalibration5.TabIndex = 15;
            ucCalibration5.Text = "PE05(kPa)";
            ucCalibration5.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration4
            // 
            ucCalibration4.Font = new Font("微软雅黑", 12F);
            ucCalibration4.GainValue = 0D;
            ucCalibration4.Index = 3;
            ucCalibration4.Location = new Point(7, 174);
            ucCalibration4.Margin = new Padding(4, 5, 4, 5);
            ucCalibration4.Name = "ucCalibration4";
            ucCalibration4.Size = new Size(567, 38);
            ucCalibration4.TabIndex = 14;
            ucCalibration4.Text = "PE04(kPa)";
            ucCalibration4.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration3
            // 
            ucCalibration3.Font = new Font("微软雅黑", 12F);
            ucCalibration3.GainValue = 0D;
            ucCalibration3.Index = 2;
            ucCalibration3.Location = new Point(7, 132);
            ucCalibration3.Margin = new Padding(4, 5, 4, 5);
            ucCalibration3.Name = "ucCalibration3";
            ucCalibration3.Size = new Size(567, 38);
            ucCalibration3.TabIndex = 13;
            ucCalibration3.Text = "PE03(kPa)";
            ucCalibration3.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration2
            // 
            ucCalibration2.Font = new Font("微软雅黑", 12F);
            ucCalibration2.GainValue = 0D;
            ucCalibration2.Index = 1;
            ucCalibration2.Location = new Point(7, 90);
            ucCalibration2.Margin = new Padding(4, 5, 4, 5);
            ucCalibration2.Name = "ucCalibration2";
            ucCalibration2.Size = new Size(567, 38);
            ucCalibration2.TabIndex = 12;
            ucCalibration2.Text = "PE02(kPa)";
            ucCalibration2.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration1
            // 
            ucCalibration1.Font = new Font("微软雅黑", 12F);
            ucCalibration1.GainValue = 0D;
            ucCalibration1.Location = new Point(7, 48);
            ucCalibration1.Margin = new Padding(4, 5, 4, 5);
            ucCalibration1.Name = "ucCalibration1";
            ucCalibration1.Size = new Size(567, 38);
            ucCalibration1.TabIndex = 11;
            ucCalibration1.Text = "PE01(kPa)";
            ucCalibration1.Submited += ucCalibration_AI_Submited;
            // 
            // grpAO
            // 
            grpAO.BackColor = Color.White;
            grpAO.Controls.Add(panel4);
            grpAO.FillColor = Color.White;
            grpAO.FillColor2 = Color.White;
            grpAO.FillDisableColor = Color.FromArgb(49, 54, 64);
            grpAO.Font = new Font("Microsoft Sans Serif", 17F, FontStyle.Bold);
            grpAO.ForeColor = Color.FromArgb(46, 46, 46);
            grpAO.ForeDisableColor = Color.FromArgb(235, 227, 221);
            grpAO.Location = new Point(656, 65);
            grpAO.Margin = new Padding(4, 5, 4, 5);
            grpAO.MinimumSize = new Size(1, 1);
            grpAO.Name = "grpAO";
            grpAO.Padding = new Padding(0, 32, 0, 0);
            grpAO.Radius = 15;
            grpAO.RectColor = Color.White;
            grpAO.RectDisableColor = Color.White;
            grpAO.Size = new Size(599, 541);
            grpAO.TabIndex = 382;
            grpAO.Text = "输出检测";
            grpAO.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.AutoScroll = true;
            panel4.BackColor = Color.White;
            panel4.Controls.Add(ucCalibration9);
            panel4.Controls.Add(ucCalibration10);
            panel4.Controls.Add(ucCalibration11);
            panel4.Controls.Add(ucCalibration12);
            panel4.Controls.Add(ucCalibration13);
            panel4.Controls.Add(ucCalibration14);
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 32);
            panel4.Name = "panel4";
            panel4.Size = new Size(599, 509);
            panel4.TabIndex = 17;
            // 
            // ucCalibration9
            // 
            ucCalibration9.Font = new Font("微软雅黑", 12F);
            ucCalibration9.GainValue = 0D;
            ucCalibration9.Index = 5;
            ucCalibration9.Location = new Point(23, 326);
            ucCalibration9.Margin = new Padding(4, 5, 4, 5);
            ucCalibration9.Name = "ucCalibration9";
            ucCalibration9.Size = new Size(567, 38);
            ucCalibration9.TabIndex = 16;
            ucCalibration9.Text = "备用";
            ucCalibration9.Submited += ucCalibration_AO_Submited;
            // 
            // ucCalibration10
            // 
            ucCalibration10.Font = new Font("微软雅黑", 12F);
            ucCalibration10.GainValue = 0D;
            ucCalibration10.Index = 4;
            ucCalibration10.Location = new Point(23, 271);
            ucCalibration10.Margin = new Padding(4, 5, 4, 5);
            ucCalibration10.Name = "ucCalibration10";
            ucCalibration10.Size = new Size(567, 38);
            ucCalibration10.TabIndex = 15;
            ucCalibration10.Text = "备用";
            ucCalibration10.Submited += ucCalibration_AO_Submited;
            // 
            // ucCalibration11
            // 
            ucCalibration11.Font = new Font("微软雅黑", 12F);
            ucCalibration11.GainValue = 0D;
            ucCalibration11.Index = 3;
            ucCalibration11.Location = new Point(23, 216);
            ucCalibration11.Margin = new Padding(4, 5, 4, 5);
            ucCalibration11.Name = "ucCalibration11";
            ucCalibration11.Size = new Size(567, 38);
            ucCalibration11.TabIndex = 14;
            ucCalibration11.Text = "备用";
            ucCalibration11.Submited += ucCalibration_AO_Submited;
            // 
            // ucCalibration12
            // 
            ucCalibration12.Font = new Font("微软雅黑", 12F);
            ucCalibration12.GainValue = 0D;
            ucCalibration12.Index = 2;
            ucCalibration12.Location = new Point(23, 161);
            ucCalibration12.Margin = new Padding(4, 5, 4, 5);
            ucCalibration12.Name = "ucCalibration12";
            ucCalibration12.Size = new Size(567, 38);
            ucCalibration12.TabIndex = 13;
            ucCalibration12.Text = "备用";
            ucCalibration12.Submited += ucCalibration_AO_Submited;
            // 
            // ucCalibration13
            // 
            ucCalibration13.Font = new Font("微软雅黑", 12F);
            ucCalibration13.GainValue = 0D;
            ucCalibration13.Index = 1;
            ucCalibration13.Location = new Point(23, 106);
            ucCalibration13.Margin = new Padding(4, 5, 4, 5);
            ucCalibration13.Name = "ucCalibration13";
            ucCalibration13.Size = new Size(567, 38);
            ucCalibration13.TabIndex = 12;
            ucCalibration13.Text = "备用";
            ucCalibration13.Submited += ucCalibration_AO_Submited;
            // 
            // ucCalibration14
            // 
            ucCalibration14.Font = new Font("微软雅黑", 12F);
            ucCalibration14.GainValue = 0D;
            ucCalibration14.Location = new Point(23, 51);
            ucCalibration14.Margin = new Padding(4, 5, 4, 5);
            ucCalibration14.Name = "ucCalibration14";
            ucCalibration14.Size = new Size(567, 38);
            ucCalibration14.TabIndex = 11;
            ucCalibration14.Text = "EP01";
            ucCalibration14.Submited += ucCalibration_AO_Submited;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(44, 62, 80);
            panel5.Controls.Add(label7);
            panel5.Controls.Add(label8);
            panel5.Controls.Add(label9);
            panel5.Dock = DockStyle.Top;
            panel5.Font = new Font("微软雅黑", 15F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panel5.ForeColor = Color.White;
            panel5.Location = new Point(0, 0);
            panel5.Margin = new Padding(3, 4, 3, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(599, 38);
            panel5.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Bold);
            label7.ImeMode = ImeMode.NoControl;
            label7.Location = new Point(340, 7);
            label7.Name = "label7";
            label7.Size = new Size(67, 22);
            label7.TabIndex = 7;
            label7.Text = "增益值";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Bold);
            label8.ImeMode = ImeMode.NoControl;
            label8.Location = new Point(154, 7);
            label8.Name = "label8";
            label8.Size = new Size(67, 22);
            label8.TabIndex = 7;
            label8.Text = "测定值";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Bold);
            label9.ImeMode = ImeMode.NoControl;
            label9.Location = new Point(250, 7);
            label9.Name = "label9";
            label9.Size = new Size(67, 22);
            label9.TabIndex = 7;
            label9.Text = "零点值";
            // 
            // frmHardWare
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(1280, 664);
            Controls.Add(grpAO);
            Controls.Add(grpAI);
            Controls.Add(label12);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmHardWare";
            RectColor = Color.FromArgb(49, 54, 64);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "硬件校准";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1270, 771);
            Load += frmHardWare_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            grpAI.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            grpAO.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
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
        private Procedure.UCCalibration ucCalibration1;
        private Procedure.UCCalibration ucCalibration5;
        private Procedure.UCCalibration ucCalibration4;
        private Procedure.UCCalibration ucCalibration3;
        private Procedure.UCCalibration ucCalibration2;
        private Procedure.UCCalibration ucCalibration6;
        private Procedure.UCCalibration ucCalibration7;
        private UIGroupBox grpAO;
        private Panel panel4;
        private Procedure.UCCalibration ucCalibration9;
        private Procedure.UCCalibration ucCalibration10;
        private Procedure.UCCalibration ucCalibration11;
        private Procedure.UCCalibration ucCalibration12;
        private Procedure.UCCalibration ucCalibration13;
        private Procedure.UCCalibration ucCalibration14;
        private Panel panel5;
        private Label label7;
        private Label label8;
        private Label label9;
    }
}
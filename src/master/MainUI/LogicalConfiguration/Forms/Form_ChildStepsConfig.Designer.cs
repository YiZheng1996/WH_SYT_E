namespace MainUI.LogicalConfiguration.Forms
{
    partial class Form_ChildStepsConfig
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
                _toolTreeControl?.Dispose();
                _processGridControl?.Dispose();
                btnSave?.Dispose();
                btnCancel?.Dispose();
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
            panelToolBox = new Panel();
            panelProcess = new Panel();
            panelButtons = new Panel();
            SuspendLayout();
            // 
            // panelToolBox
            // 
            panelToolBox.BackColor = Color.FromArgb(248, 249, 250);
            panelToolBox.Dock = DockStyle.Left;
            panelToolBox.Location = new Point(0, 35);
            panelToolBox.Name = "panelToolBox";
            panelToolBox.Size = new Size(250, 605);
            panelToolBox.TabIndex = 0;
            // 
            // panelProcess
            // 
            panelProcess.BackColor = Color.White;
            panelProcess.Dock = DockStyle.Fill;
            panelProcess.Location = new Point(250, 35);
            panelProcess.Name = "panelProcess";
            panelProcess.Padding = new Padding(10);
            panelProcess.Size = new Size(950, 605);
            panelProcess.TabIndex = 1;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(248, 249, 250);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(0, 640);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1200, 60);
            panelButtons.TabIndex = 2;
            // 
            // Form_ChildStepsConfig
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1200, 700);
            ControlBox = false;
            Controls.Add(panelProcess);
            Controls.Add(panelToolBox);
            Controls.Add(panelButtons);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_ChildStepsConfig";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "循环体子步骤配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1200, 700);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolBox;
        private System.Windows.Forms.Panel panelProcess;
        private System.Windows.Forms.Panel panelButtons;
    }
}
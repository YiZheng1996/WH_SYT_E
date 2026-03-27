namespace MainUI.LogicalConfiguration.Forms
{
    partial class FrmRuntimeUserInput
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlMain = new UIPanel();
            pnlInput = new UIPanel();
            txtTextInput = new UITextBox();
            nudNumInput = new UIDoubleUpDown();
            lblUnit = new UILabel();
            cmbSelectInput = new UIComboBox();
            lblCountdown = new UILabel();
            pnlBottom = new UIPanel();
            btnCancel = new UISymbolButton();
            btnConfirm = new UISymbolButton();
            pnlPrompt = new UIPanel();
            lblPrompt = new UILabel();
            timerCountdown = new System.Windows.Forms.Timer(components);
            pnlMain.SuspendLayout();
            pnlInput.SuspendLayout();
            pnlBottom.SuspendLayout();
            pnlPrompt.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlInput);
            pnlMain.Controls.Add(lblCountdown);
            pnlMain.Controls.Add(pnlBottom);
            pnlMain.Controls.Add(pnlPrompt);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.White;
            pnlMain.FillColor2 = Color.White;
            pnlMain.Font = new Font("微软雅黑", 10F);
            pnlMain.Location = new Point(0, 35);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.MinimumSize = new Size(1, 1);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(20, 10, 20, 0);
            pnlMain.Size = new Size(500, 225);
            pnlMain.TabIndex = 0;
            pnlMain.Text = null;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pnlInput
            // 
            pnlInput.Controls.Add(txtTextInput);
            pnlInput.Controls.Add(nudNumInput);
            pnlInput.Controls.Add(lblUnit);
            pnlInput.Controls.Add(cmbSelectInput);
            pnlInput.Dock = DockStyle.Top;
            pnlInput.FillColor = Color.White;
            pnlInput.FillColor2 = Color.White;
            pnlInput.Font = new Font("微软雅黑", 10F);
            pnlInput.Location = new Point(20, 60);
            pnlInput.Margin = new Padding(4, 5, 4, 5);
            pnlInput.MinimumSize = new Size(1, 1);
            pnlInput.Name = "pnlInput";
            pnlInput.Padding = new Padding(0, 8, 0, 0);
            pnlInput.Size = new Size(460, 56);
            pnlInput.TabIndex = 1;
            pnlInput.Text = null;
            pnlInput.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtTextInput
            // 
            txtTextInput.Font = new Font("微软雅黑", 13F);
            txtTextInput.Location = new Point(0, 8);
            txtTextInput.Margin = new Padding(4, 5, 4, 5);
            txtTextInput.MinimumSize = new Size(1, 16);
            txtTextInput.Name = "txtTextInput";
            txtTextInput.Padding = new Padding(5);
            txtTextInput.ShowText = false;
            txtTextInput.Size = new Size(460, 38);
            txtTextInput.TabIndex = 0;
            txtTextInput.TextAlignment = ContentAlignment.MiddleLeft;
            txtTextInput.Watermark = "请在此处输入...";
            txtTextInput.KeyDown += TxtTextInput_KeyDown;
            // 
            // nudNumInput
            // 
            nudNumInput.Font = new Font("微软雅黑", 13F);
            nudNumInput.Location = new Point(0, 8);
            nudNumInput.Margin = new Padding(4, 5, 4, 5);
            nudNumInput.Maximum = 999999999D;
            nudNumInput.Minimum = -999999999D;
            nudNumInput.MinimumSize = new Size(1, 16);
            nudNumInput.Name = "nudNumInput";
            nudNumInput.Padding = new Padding(5);
            nudNumInput.ShowText = false;
            nudNumInput.Size = new Size(300, 38);
            nudNumInput.Step = 1D;
            nudNumInput.TabIndex = 1;
            nudNumInput.Text = "0.00";
            nudNumInput.TextAlignment = ContentAlignment.MiddleCenter;
            nudNumInput.Value = 0D;
            nudNumInput.Visible = false;
            // 
            // lblUnit
            // 
            lblUnit.BackColor = Color.Transparent;
            lblUnit.Font = new Font("微软雅黑", 9F);
            lblUnit.ForeColor = Color.FromArgb(120, 120, 120);
            lblUnit.Location = new Point(308, 8);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(152, 38);
            lblUnit.TabIndex = 3;
            lblUnit.TextAlign = ContentAlignment.MiddleLeft;
            lblUnit.Visible = false;
            // 
            // cmbSelectInput
            // 
            cmbSelectInput.DataSource = null;
            cmbSelectInput.DropDownStyle = UIDropDownStyle.DropDownList;
            cmbSelectInput.FillColor = Color.White;
            cmbSelectInput.Font = new Font("微软雅黑", 13F);
            cmbSelectInput.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbSelectInput.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbSelectInput.Location = new Point(0, 8);
            cmbSelectInput.Margin = new Padding(4, 5, 4, 5);
            cmbSelectInput.MinimumSize = new Size(63, 0);
            cmbSelectInput.Name = "cmbSelectInput";
            cmbSelectInput.Padding = new Padding(0, 0, 30, 2);
            cmbSelectInput.RectColor = Color.FromArgb(65, 100, 204);
            cmbSelectInput.Size = new Size(460, 38);
            cmbSelectInput.SymbolSize = 24;
            cmbSelectInput.TabIndex = 2;
            cmbSelectInput.TextAlignment = ContentAlignment.MiddleLeft;
            cmbSelectInput.Visible = false;
            cmbSelectInput.Watermark = "";
            // 
            // lblCountdown
            // 
            lblCountdown.BackColor = Color.Transparent;
            lblCountdown.Font = new Font("微软雅黑", 9F);
            lblCountdown.ForeColor = Color.FromArgb(180, 60, 60);
            lblCountdown.Location = new Point(20, 122);
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Size = new Size(460, 20);
            lblCountdown.TabIndex = 2;
            lblCountdown.TextAlign = ContentAlignment.MiddleLeft;
            lblCountdown.Visible = false;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnConfirm);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.FillColor = Color.FromArgb(245, 247, 250);
            pnlBottom.FillColor2 = Color.FromArgb(245, 247, 250);
            pnlBottom.Font = new Font("微软雅黑", 10F);
            pnlBottom.Location = new Point(20, 165);
            pnlBottom.Margin = new Padding(4, 5, 4, 5);
            pnlBottom.MinimumSize = new Size(1, 1);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(10);
            pnlBottom.Size = new Size(460, 60);
            pnlBottom.TabIndex = 3;
            pnlBottom.Text = null;
            pnlBottom.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(248, 248, 248);
            btnCancel.FillColor2 = Color.FromArgb(248, 248, 248);
            btnCancel.FillHoverColor = Color.FromArgb(230, 230, 230);
            btnCancel.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(80, 80, 80);
            btnCancel.Location = new Point(330, 13);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 6;
            btnCancel.RectColor = Color.FromArgb(200, 200, 200);
            btnCancel.Size = new Size(110, 40);
            btnCancel.Symbol = 61453;
            btnCancel.SymbolSize = 20;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取  消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.None;
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.FillColor = Color.FromArgb(65, 100, 204);
            btnConfirm.FillColor2 = Color.FromArgb(65, 100, 204);
            btnConfirm.FillHoverColor = Color.FromArgb(80, 120, 220);
            btnConfirm.FillPressColor = Color.FromArgb(50, 80, 180);
            btnConfirm.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnConfirm.Location = new Point(210, 13);
            btnConfirm.MinimumSize = new Size(1, 1);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Radius = 6;
            btnConfirm.RectColor = Color.FromArgb(65, 100, 204);
            btnConfirm.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnConfirm.Size = new Size(110, 40);
            btnConfirm.Symbol = 61639;
            btnConfirm.SymbolSize = 20;
            btnConfirm.TabIndex = 0;
            btnConfirm.Text = "确  认";
            btnConfirm.TipsFont = new Font("微软雅黑", 9F);
            btnConfirm.Click += BtnConfirm_Click;
            // 
            // pnlPrompt
            // 
            pnlPrompt.Controls.Add(lblPrompt);
            pnlPrompt.Dock = DockStyle.Top;
            pnlPrompt.FillColor = Color.FromArgb(230, 244, 255);
            pnlPrompt.FillColor2 = Color.FromArgb(230, 244, 255);
            pnlPrompt.Font = new Font("微软雅黑", 10F);
            pnlPrompt.Location = new Point(20, 10);
            pnlPrompt.Margin = new Padding(4, 5, 4, 5);
            pnlPrompt.MinimumSize = new Size(1, 1);
            pnlPrompt.Name = "pnlPrompt";
            pnlPrompt.Padding = new Padding(12, 8, 12, 8);
            pnlPrompt.Radius = 8;
            pnlPrompt.RectColor = Color.FromArgb(65, 100, 204);
            pnlPrompt.Size = new Size(460, 50);
            pnlPrompt.TabIndex = 0;
            pnlPrompt.Text = null;
            pnlPrompt.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblPrompt
            // 
            lblPrompt.BackColor = Color.Transparent;
            lblPrompt.Dock = DockStyle.Fill;
            lblPrompt.Font = new Font("微软雅黑", 11F);
            lblPrompt.ForeColor = Color.FromArgb(30, 30, 30);
            lblPrompt.Location = new Point(12, 8);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(436, 34);
            lblPrompt.TabIndex = 0;
            lblPrompt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // timerCountdown
            // 
            timerCountdown.Interval = 1000;
            timerCountdown.Tick += TimerCountdown_Tick;
            // 
            // FrmRuntimeUserInput
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(500, 260);
            ControlBox = false;
            ControlBoxFillHoverColor = Color.FromArgb(163, 163, 163);
            Controls.Add(pnlMain);
            Font = new Font("微软雅黑", 10F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRuntimeUserInput";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Style = UIStyle.Custom;
            Text = "请输入";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            TopMost = true;
            ZoomScaleRect = new Rectangle(15, 15, 500, 260);
            pnlMain.ResumeLayout(false);
            pnlInput.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            pnlPrompt.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region 控件声明

        private Sunny.UI.UIPanel pnlMain;
        private Sunny.UI.UIPanel pnlPrompt;
        private Sunny.UI.UILabel lblPrompt;
        private Sunny.UI.UIPanel pnlInput;
        private Sunny.UI.UITextBox txtTextInput;
        private Sunny.UI.UIDoubleUpDown nudNumInput;
        private Sunny.UI.UILabel lblUnit;
        private Sunny.UI.UIComboBox cmbSelectInput;
        private Sunny.UI.UILabel lblCountdown;
        private Sunny.UI.UIPanel pnlBottom;
        private Sunny.UI.UISymbolButton btnConfirm;
        private Sunny.UI.UISymbolButton btnCancel;
        private System.Windows.Forms.Timer timerCountdown;

        #endregion
    }
}
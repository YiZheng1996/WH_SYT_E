// MainUI/frmTestBenchSelect.Designer.cs
namespace MainUI
{
    partial class frmTestBenchSelect
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
            uiPanel1 = new UIPanel();
            lblBenchInfo = new UILabel();
            uiLabel2 = new UILabel();
            btnCancel = new UISymbolButton();
            btnConfirm = new UISymbolButton();
            lblDescription = new UILabel();
            cboTestBench = new UIComboBox();
            uiLabel1 = new UILabel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(lblBenchInfo);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(btnCancel);
            uiPanel1.Controls.Add(btnConfirm);
            uiPanel1.Controls.Add(lblDescription);
            uiPanel1.Controls.Add(cboTestBench);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Dock = DockStyle.Fill;
            uiPanel1.Font = new Font("微软雅黑", 12F);
            uiPanel1.Location = new Point(0, 35);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 0;
            uiPanel1.RectColor = Color.FromArgb(65, 100, 204);
            uiPanel1.Size = new Size(500, 315);
            uiPanel1.TabIndex = 0;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblBenchInfo
            // 
            lblBenchInfo.Font = new Font("微软雅黑", 10F);
            lblBenchInfo.ForeColor = Color.FromArgb(100, 100, 100);
            lblBenchInfo.Location = new Point(30, 170);
            lblBenchInfo.Name = "lblBenchInfo";
            lblBenchInfo.Size = new Size(440, 50);
            lblBenchInfo.TabIndex = 6;
            lblBenchInfo.Text = "请选择试验台查看详细信息";
            // 
            // uiLabel2
            // 
            uiLabel2.Font = new Font("微软雅黑", 10F);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(30, 140);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(100, 30);
            uiLabel2.TabIndex = 5;
            uiLabel2.Text = "试验台信息：";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = Color.FromArgb(230, 80, 80);
            btnCancel.FillColor2 = Color.FromArgb(230, 80, 80);
            btnCancel.FillHoverColor = Color.FromArgb(235, 115, 115);
            btnCancel.FillPressColor = Color.FromArgb(184, 64, 64);
            btnCancel.FillSelectedColor = Color.FromArgb(184, 64, 64);
            btnCancel.Font = new Font("微软雅黑", 12F);
            btnCancel.Location = new Point(270, 240);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.FromArgb(230, 80, 80);
            btnCancel.RectHoverColor = Color.FromArgb(235, 115, 115);
            btnCancel.RectPressColor = Color.FromArgb(184, 64, 64);
            btnCancel.RectSelectedColor = Color.FromArgb(184, 64, 64);
            btnCancel.Size = new Size(120, 45);
            btnCancel.Symbol = 61453;
            btnCancel.SymbolSize = 30;
            btnCancel.TabIndex = 4;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F);
            btnCancel.Click += btnCancel_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.FillColor = Color.FromArgb(65, 100, 204);
            btnConfirm.FillColor2 = Color.FromArgb(65, 100, 204);
            btnConfirm.FillHoverColor = Color.FromArgb(80, 126, 164);
            btnConfirm.FillPressColor = Color.FromArgb(74, 131, 229);
            btnConfirm.FillSelectedColor = Color.FromArgb(74, 131, 229);
            btnConfirm.Font = new Font("微软雅黑", 12F);
            btnConfirm.Location = new Point(130, 240);
            btnConfirm.MinimumSize = new Size(1, 1);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.RectColor = Color.FromArgb(65, 100, 204);
            btnConfirm.RectHoverColor = Color.FromArgb(80, 126, 164);
            btnConfirm.RectPressColor = Color.FromArgb(74, 131, 229);
            btnConfirm.RectSelectedColor = Color.FromArgb(74, 131, 229);
            btnConfirm.Size = new Size(120, 45);
            btnConfirm.Symbol = 61452;
            btnConfirm.SymbolSize = 30;
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "确定";
            btnConfirm.TipsFont = new Font("微软雅黑", 9F);
            btnConfirm.Click += btnConfirm_Click;
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("微软雅黑", 11F);
            lblDescription.ForeColor = Color.FromArgb(48, 48, 48);
            lblDescription.Location = new Point(30, 20);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(440, 50);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "系统未能自动识别试验台，请手动选择当前试验台：";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboTestBench
            // 
            cboTestBench.DataSource = null;
            cboTestBench.DropDownStyle = UIDropDownStyle.DropDownList;
            cboTestBench.FillColor = Color.White;
            cboTestBench.Font = new Font("微软雅黑", 12F);
            cboTestBench.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboTestBench.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboTestBench.Location = new Point(130, 85);
            cboTestBench.Margin = new Padding(4, 5, 4, 5);
            cboTestBench.MinimumSize = new Size(63, 0);
            cboTestBench.Name = "cboTestBench";
            cboTestBench.Padding = new Padding(0, 0, 30, 2);
            cboTestBench.RectColor = Color.FromArgb(65, 100, 204);
            cboTestBench.Size = new Size(340, 35);
            cboTestBench.SymbolSize = 24;
            cboTestBench.TabIndex = 1;
            cboTestBench.TextAlignment = ContentAlignment.MiddleLeft;
            cboTestBench.Watermark = "请选择试验台";
            cboTestBench.SelectedIndexChanged += cboTestBench_SelectedIndexChanged;
            // 
            // uiLabel1
            // 
            uiLabel1.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(30, 85);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(100, 35);
            uiLabel1.TabIndex = 0;
            uiLabel1.Text = "试验台：";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmTestBenchSelect
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 350);
            Controls.Add(uiPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmTestBenchSelect";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "选择试验台";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 500, 350);
            Load += frmTestBenchSelect_Load;
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIComboBox cboTestBench;
        private Sunny.UI.UILabel lblDescription;
        private Sunny.UI.UISymbolButton btnConfirm;
        private Sunny.UI.UISymbolButton btnCancel;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel lblBenchInfo;
    }
}
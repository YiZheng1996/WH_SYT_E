namespace MainUI
{
    partial class frmDeviceInspectEdit
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            uiLabel1 = new UILabel();
            btnCancel = new UIButton();
            btnSubmit = new UIButton();
            uiPanel1 = new UIPanel();
            txtPartName = new UITextBox();
            CboPartType = new UIComboBox();
            uiLabel3 = new UILabel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel1.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel1.Location = new Point(110, 51);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(78, 24);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "部件类型";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCancel.Location = new Point(281, 175);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 10;
            btnCancel.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Size = new Size(126, 37);
            btnCancel.TabIndex = 398;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.TipsText = "1";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.Transparent;
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnSubmit.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSubmit.Location = new Point(117, 175);
            btnSubmit.MinimumSize = new Size(1, 1);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Radius = 10;
            btnSubmit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSubmit.Size = new Size(126, 37);
            btnSubmit.TabIndex = 397;
            btnSubmit.Text = "确定";
            btnSubmit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSubmit.TipsText = "1";
            btnSubmit.Click += BtnSubmit_Click;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(txtPartName);
            uiPanel1.Controls.Add(CboPartType);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(btnCancel);
            uiPanel1.Controls.Add(btnSubmit);
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Font = new Font("宋体", 12F);
            uiPanel1.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel1.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Location = new Point(33, 61);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(519, 250);
            uiPanel1.TabIndex = 408;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtPartName
            // 
            txtPartName.BackColor = Color.Transparent;
            txtPartName.Cursor = Cursors.IBeam;
            txtPartName.FillColor = Color.FromArgb(218, 220, 230);
            txtPartName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtPartName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtPartName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPartName.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtPartName.ForeColor = Color.Black;
            txtPartName.ForeDisableColor = Color.Black;
            txtPartName.ForeReadOnlyColor = Color.Black;
            txtPartName.Location = new Point(202, 104);
            txtPartName.Margin = new Padding(4, 5, 4, 5);
            txtPartName.MinimumSize = new Size(1, 16);
            txtPartName.Name = "txtPartName";
            txtPartName.Padding = new Padding(5);
            txtPartName.Radius = 10;
            txtPartName.RectColor = Color.FromArgb(218, 220, 230);
            txtPartName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtPartName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPartName.ShowText = false;
            txtPartName.Size = new Size(210, 30);
            txtPartName.TabIndex = 408;
            txtPartName.TextAlignment = ContentAlignment.MiddleLeft;
            txtPartName.Watermark = "请输入";
            // 
            // CboPartType
            // 
            CboPartType.BackColor = Color.Transparent;
            CboPartType.DataSource = null;
            CboPartType.FillColor = Color.FromArgb(218, 220, 230);
            CboPartType.FillColor2 = Color.FromArgb(218, 220, 230);
            CboPartType.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboPartType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            CboPartType.ForeColor = Color.Black;
            CboPartType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboPartType.Items.AddRange(new object[] { "模拟量输出", "模拟量输入", "数字量输出", "数字量输入" });
            CboPartType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboPartType.Location = new Point(202, 49);
            CboPartType.Margin = new Padding(4, 5, 4, 5);
            CboPartType.MinimumSize = new Size(63, 0);
            CboPartType.Name = "CboPartType";
            CboPartType.Padding = new Padding(0, 0, 30, 2);
            CboPartType.Radius = 10;
            CboPartType.RectColor = Color.Gray;
            CboPartType.RectDisableColor = Color.Gray;
            CboPartType.RectSides = ToolStripStatusLabelBorderSides.None;
            CboPartType.Size = new Size(210, 31);
            CboPartType.SymbolSize = 24;
            CboPartType.TabIndex = 399;
            CboPartType.TextAlignment = ContentAlignment.MiddleLeft;
            CboPartType.Watermark = "请选择";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(104, 104);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(84, 26);
            uiLabel3.TabIndex = 74;
            uiLabel3.Text = "部件名称";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmDeviceInspectEdit
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(585, 341);
            ControlBox = false;
            Controls.Add(uiPanel1);
            Font = new Font("微软雅黑", 11F);
            ForeColor = Color.FromArgb(236, 236, 236);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(0, 0);
            MinimizeBox = false;
            Name = "frmDeviceInspectEdit";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "设备检查参数修改";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold);
            TitleForeColor = Color.FromArgb(236, 236, 236);
            ZoomScaleRect = new Rectangle(15, 15, 294, 282);
            Load += frmMeteringEdit_Load;
            uiPanel1.ResumeLayout(false);
            uiPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSubmit;
        private UIPanel uiPanel1;
        private UIComboBox CboPartType;
        private UITextBox txtPartName;
        private UILabel uiLabel3;
    }
}
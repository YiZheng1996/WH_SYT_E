namespace MainUI
{
    partial class frmModelTypeEdit
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
            txtModelType = new UITextBox();
            txtMark = new UITextBox();
            uiLabel3 = new UILabel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel1.Location = new Point(107, 43);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(78, 24);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "类型名称";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCancel.Location = new Point(281, 198);
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
            btnSubmit.Location = new Point(117, 198);
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
            uiPanel1.Controls.Add(txtModelType);
            uiPanel1.Controls.Add(txtMark);
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
            uiPanel1.Size = new Size(519, 268);
            uiPanel1.TabIndex = 408;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtModelType
            // 
            txtModelType.BackColor = Color.Transparent;
            txtModelType.Cursor = Cursors.IBeam;
            txtModelType.FillColor = Color.FromArgb(218, 220, 230);
            txtModelType.FillColor2 = Color.FromArgb(218, 220, 230);
            txtModelType.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtModelType.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtModelType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtModelType.ForeColor = Color.Black;
            txtModelType.ForeDisableColor = Color.Black;
            txtModelType.ForeReadOnlyColor = Color.Black;
            txtModelType.Location = new Point(202, 40);
            txtModelType.Margin = new Padding(4, 5, 4, 5);
            txtModelType.MinimumSize = new Size(1, 16);
            txtModelType.Name = "txtModelType";
            txtModelType.Padding = new Padding(5);
            txtModelType.Radius = 10;
            txtModelType.RectColor = Color.FromArgb(218, 220, 230);
            txtModelType.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtModelType.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtModelType.ShowText = false;
            txtModelType.Size = new Size(210, 30);
            txtModelType.TabIndex = 409;
            txtModelType.TextAlignment = ContentAlignment.MiddleLeft;
            txtModelType.Watermark = "请输入";
            // 
            // txtMark
            // 
            txtMark.BackColor = Color.Transparent;
            txtMark.Cursor = Cursors.IBeam;
            txtMark.FillColor = Color.FromArgb(218, 220, 230);
            txtMark.FillColor2 = Color.FromArgb(218, 220, 230);
            txtMark.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtMark.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtMark.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtMark.ForeColor = Color.Black;
            txtMark.ForeDisableColor = Color.Black;
            txtMark.ForeReadOnlyColor = Color.Black;
            txtMark.Location = new Point(202, 96);
            txtMark.Margin = new Padding(4, 5, 4, 5);
            txtMark.MinimumSize = new Size(1, 16);
            txtMark.Name = "txtMark";
            txtMark.Padding = new Padding(5);
            txtMark.Radius = 10;
            txtMark.RectColor = Color.FromArgb(218, 220, 230);
            txtMark.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtMark.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtMark.ShowText = false;
            txtMark.Size = new Size(210, 81);
            txtMark.TabIndex = 408;
            txtMark.TextAlignment = ContentAlignment.MiddleLeft;
            txtMark.Watermark = "请输入";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(107, 96);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(78, 24);
            uiLabel3.TabIndex = 74;
            uiLabel3.Text = "类型备注";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmModelTypeEdit
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(585, 360);
            ControlBox = false;
            Controls.Add(uiPanel1);
            Font = new Font("微软雅黑", 11F);
            ForeColor = Color.FromArgb(236, 236, 236);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(0, 0);
            MinimizeBox = false;
            Name = "frmModelTypeEdit";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "类型管理参数修改";
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
        private UITextBox txtMark;
        private UILabel uiLabel3;
        private UITextBox txtModelType;
    }
}
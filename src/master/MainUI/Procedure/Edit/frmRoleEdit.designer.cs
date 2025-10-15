namespace MainUI
{
    partial class frmRoleEdit
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
            txtRoleName = new UITextBox();
            txtDescribe = new UITextBox();
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
            uiLabel1.Location = new Point(108, 51);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(78, 24);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "角色名称";
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
            uiPanel1.Controls.Add(txtRoleName);
            uiPanel1.Controls.Add(txtDescribe);
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
            // txtRoleName
            // 
            txtRoleName.BackColor = Color.Transparent;
            txtRoleName.Cursor = Cursors.IBeam;
            txtRoleName.FillColor = Color.FromArgb(218, 220, 230);
            txtRoleName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtRoleName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtRoleName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtRoleName.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtRoleName.ForeColor = Color.Black;
            txtRoleName.ForeDisableColor = Color.Black;
            txtRoleName.ForeReadOnlyColor = Color.Black;
            txtRoleName.Location = new Point(202, 48);
            txtRoleName.Margin = new Padding(4, 5, 4, 5);
            txtRoleName.MinimumSize = new Size(1, 16);
            txtRoleName.Name = "txtRoleName";
            txtRoleName.Padding = new Padding(5);
            txtRoleName.Radius = 10;
            txtRoleName.RectColor = Color.FromArgb(218, 220, 230);
            txtRoleName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtRoleName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtRoleName.ShowText = false;
            txtRoleName.Size = new Size(210, 30);
            txtRoleName.TabIndex = 409;
            txtRoleName.TextAlignment = ContentAlignment.MiddleLeft;
            txtRoleName.Watermark = "请输入";
            // 
            // txtDescribe
            // 
            txtDescribe.BackColor = Color.Transparent;
            txtDescribe.Cursor = Cursors.IBeam;
            txtDescribe.FillColor = Color.FromArgb(218, 220, 230);
            txtDescribe.FillColor2 = Color.FromArgb(218, 220, 230);
            txtDescribe.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtDescribe.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDescribe.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtDescribe.ForeColor = Color.Black;
            txtDescribe.ForeDisableColor = Color.Black;
            txtDescribe.ForeReadOnlyColor = Color.Black;
            txtDescribe.Location = new Point(202, 104);
            txtDescribe.Margin = new Padding(4, 5, 4, 5);
            txtDescribe.MinimumSize = new Size(1, 16);
            txtDescribe.Name = "txtDescribe";
            txtDescribe.Padding = new Padding(5);
            txtDescribe.Radius = 10;
            txtDescribe.RectColor = Color.FromArgb(218, 220, 230);
            txtDescribe.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtDescribe.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDescribe.ShowText = false;
            txtDescribe.Size = new Size(210, 30);
            txtDescribe.TabIndex = 408;
            txtDescribe.TextAlignment = ContentAlignment.MiddleLeft;
            txtDescribe.Watermark = "请输入";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(108, 104);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(78, 24);
            uiLabel3.TabIndex = 74;
            uiLabel3.Text = "角色描述";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmRoleEdit
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
            Name = "frmRoleEdit";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "角色管理参数修改";
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
        private UITextBox txtDescribe;
        private UILabel uiLabel3;
        private UITextBox txtRoleName;
    }
}
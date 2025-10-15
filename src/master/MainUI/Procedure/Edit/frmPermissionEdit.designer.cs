namespace MainUI.Procedure.Edit
{
    partial class frmPermissionEdit
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
            txtControlName = new UITextBox();
            uiLabel4 = new UILabel();
            txtPermissionCode = new UITextBox();
            uiLabel2 = new UILabel();
            txtPermissionNotes = new UIRichTextBox();
            uiLabel5 = new UILabel();
            txtPermissionName = new UITextBox();
            uiLabel3 = new UILabel();
            CboPermissionType = new UIComboBox();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel1.Location = new Point(112, 457);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(88, 23);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "权限类型";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            uiLabel1.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCancel.Location = new Point(293, 331);
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
            btnSubmit.Location = new Point(129, 331);
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
            uiPanel1.Controls.Add(txtControlName);
            uiPanel1.Controls.Add(uiLabel4);
            uiPanel1.Controls.Add(txtPermissionCode);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(txtPermissionNotes);
            uiPanel1.Controls.Add(uiLabel5);
            uiPanel1.Controls.Add(txtPermissionName);
            uiPanel1.Controls.Add(btnCancel);
            uiPanel1.Controls.Add(btnSubmit);
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Font = new Font("宋体", 12F);
            uiPanel1.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel1.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Location = new Point(33, 60);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(519, 406);
            uiPanel1.TabIndex = 408;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtControlName
            // 
            txtControlName.BackColor = Color.Transparent;
            txtControlName.Cursor = Cursors.IBeam;
            txtControlName.FillColor = Color.FromArgb(218, 220, 230);
            txtControlName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtControlName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtControlName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtControlName.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtControlName.ForeColor = Color.Black;
            txtControlName.ForeDisableColor = Color.Black;
            txtControlName.ForeReadOnlyColor = Color.Black;
            txtControlName.Location = new Point(211, 145);
            txtControlName.Margin = new Padding(4, 5, 4, 5);
            txtControlName.MinimumSize = new Size(1, 16);
            txtControlName.Name = "txtControlName";
            txtControlName.Padding = new Padding(5);
            txtControlName.Radius = 10;
            txtControlName.RectColor = Color.FromArgb(218, 220, 230);
            txtControlName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtControlName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtControlName.ShowText = false;
            txtControlName.Size = new Size(210, 30);
            txtControlName.TabIndex = 414;
            txtControlName.TextAlignment = ContentAlignment.MiddleLeft;
            txtControlName.Watermark = "请输入";
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel4.ImeMode = ImeMode.NoControl;
            uiLabel4.Location = new Point(118, 147);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(78, 24);
            uiLabel4.TabIndex = 413;
            uiLabel4.Text = "控件名称";
            uiLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPermissionCode
            // 
            txtPermissionCode.BackColor = Color.Transparent;
            txtPermissionCode.Cursor = Cursors.IBeam;
            txtPermissionCode.FillColor = Color.FromArgb(218, 220, 230);
            txtPermissionCode.FillColor2 = Color.FromArgb(218, 220, 230);
            txtPermissionCode.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtPermissionCode.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPermissionCode.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtPermissionCode.ForeColor = Color.Black;
            txtPermissionCode.ForeDisableColor = Color.Black;
            txtPermissionCode.ForeReadOnlyColor = Color.Black;
            txtPermissionCode.Location = new Point(211, 92);
            txtPermissionCode.Margin = new Padding(4, 5, 4, 5);
            txtPermissionCode.MinimumSize = new Size(1, 16);
            txtPermissionCode.Name = "txtPermissionCode";
            txtPermissionCode.Padding = new Padding(5);
            txtPermissionCode.Radius = 10;
            txtPermissionCode.RectColor = Color.FromArgb(218, 220, 230);
            txtPermissionCode.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtPermissionCode.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPermissionCode.ShowText = false;
            txtPermissionCode.Size = new Size(210, 30);
            txtPermissionCode.TabIndex = 412;
            txtPermissionCode.TextAlignment = ContentAlignment.MiddleLeft;
            txtPermissionCode.Watermark = "请输入";
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel2.ImeMode = ImeMode.NoControl;
            uiLabel2.Location = new Point(118, 95);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(78, 24);
            uiLabel2.TabIndex = 411;
            uiLabel2.Text = "权限代码";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPermissionNotes
            // 
            txtPermissionNotes.BackColor = Color.Transparent;
            txtPermissionNotes.FillColor = Color.FromArgb(218, 220, 230);
            txtPermissionNotes.FillColor2 = Color.FromArgb(218, 220, 230);
            txtPermissionNotes.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtPermissionNotes.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtPermissionNotes.ForeColor = Color.Black;
            txtPermissionNotes.ForeDisableColor = Color.Black;
            txtPermissionNotes.Location = new Point(211, 198);
            txtPermissionNotes.Margin = new Padding(4, 5, 4, 5);
            txtPermissionNotes.MinimumSize = new Size(1, 1);
            txtPermissionNotes.Name = "txtPermissionNotes";
            txtPermissionNotes.Padding = new Padding(2);
            txtPermissionNotes.Radius = 10;
            txtPermissionNotes.RectColor = Color.FromArgb(218, 220, 230);
            txtPermissionNotes.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtPermissionNotes.ShowText = false;
            txtPermissionNotes.Size = new Size(210, 102);
            txtPermissionNotes.TabIndex = 410;
            txtPermissionNotes.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel5
            // 
            uiLabel5.AutoSize = true;
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel5.ImeMode = ImeMode.NoControl;
            uiLabel5.Location = new Point(146, 199);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(54, 24);
            uiLabel5.TabIndex = 409;
            uiLabel5.Text = "备  注";
            uiLabel5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPermissionName
            // 
            txtPermissionName.BackColor = Color.Transparent;
            txtPermissionName.Cursor = Cursors.IBeam;
            txtPermissionName.FillColor = Color.FromArgb(218, 220, 230);
            txtPermissionName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtPermissionName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtPermissionName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPermissionName.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtPermissionName.ForeColor = Color.Black;
            txtPermissionName.ForeDisableColor = Color.Black;
            txtPermissionName.ForeReadOnlyColor = Color.Black;
            txtPermissionName.Location = new Point(211, 39);
            txtPermissionName.Margin = new Padding(4, 5, 4, 5);
            txtPermissionName.MinimumSize = new Size(1, 16);
            txtPermissionName.Name = "txtPermissionName";
            txtPermissionName.Padding = new Padding(5);
            txtPermissionName.Radius = 10;
            txtPermissionName.RectColor = Color.FromArgb(218, 220, 230);
            txtPermissionName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtPermissionName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPermissionName.ShowText = false;
            txtPermissionName.Size = new Size(210, 30);
            txtPermissionName.TabIndex = 408;
            txtPermissionName.TextAlignment = ContentAlignment.MiddleLeft;
            txtPermissionName.Watermark = "请输入";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(118, 43);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(78, 24);
            uiLabel3.TabIndex = 74;
            uiLabel3.Text = "权限名称";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CboPermissionType
            // 
            CboPermissionType.BackColor = Color.Transparent;
            CboPermissionType.DataSource = null;
            CboPermissionType.FillColor = Color.FromArgb(218, 220, 230);
            CboPermissionType.FillColor2 = Color.FromArgb(218, 220, 230);
            CboPermissionType.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboPermissionType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            CboPermissionType.ForeColor = Color.Black;
            CboPermissionType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboPermissionType.Items.AddRange(new object[] { "查看", "删除", "编辑", "新增" });
            CboPermissionType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboPermissionType.Location = new Point(213, 454);
            CboPermissionType.Margin = new Padding(4, 5, 4, 5);
            CboPermissionType.MinimumSize = new Size(63, 0);
            CboPermissionType.Name = "CboPermissionType";
            CboPermissionType.Padding = new Padding(0, 0, 30, 2);
            CboPermissionType.Radius = 10;
            CboPermissionType.RectColor = Color.Gray;
            CboPermissionType.RectDisableColor = Color.Gray;
            CboPermissionType.RectSides = ToolStripStatusLabelBorderSides.None;
            CboPermissionType.Size = new Size(210, 31);
            CboPermissionType.SymbolSize = 24;
            CboPermissionType.TabIndex = 399;
            CboPermissionType.TextAlignment = ContentAlignment.MiddleLeft;
            CboPermissionType.Visible = false;
            CboPermissionType.Watermark = "请选择";
            // 
            // frmPermissionEdit
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(585, 494);
            ControlBox = false;
            Controls.Add(uiPanel1);
            Controls.Add(CboPermissionType);
            Controls.Add(uiLabel1);
            Font = new Font("微软雅黑", 11F);
            ForeColor = Color.FromArgb(236, 236, 236);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(0, 0);
            MinimizeBox = false;
            Name = "frmPermissionEdit";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "权限管理参数修改";
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
        private UIComboBox CboPermissionType;
        private UITextBox txtPermissionName;
        private UILabel uiLabel3;
        private UIRichTextBox txtPermissionNotes;
        private UILabel uiLabel5;
        private UITextBox txtControlName;
        private UILabel uiLabel4;
        private UITextBox txtPermissionCode;
        private UILabel uiLabel2;
    }
}
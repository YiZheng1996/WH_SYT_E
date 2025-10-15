namespace MainUI
{
    partial class FrmTestProcess
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
            uiLabel4 = new UILabel();
            cboModelType = new UIComboBox();
            txtEntityClassName = new UITextBox();
            uiLabel2 = new UILabel();
            RadioIsVisible2 = new UIRadioButton();
            RadioIsVisible = new UIRadioButton();
            txtProcessName = new UITextBox();
            uiLabel3 = new UILabel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel1.Location = new Point(105, 92);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(84, 26);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "项点名称";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCancel.Location = new Point(281, 249);
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
            btnSubmit.Location = new Point(117, 249);
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
            uiPanel1.Controls.Add(uiLabel4);
            uiPanel1.Controls.Add(cboModelType);
            uiPanel1.Controls.Add(txtEntityClassName);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(RadioIsVisible2);
            uiPanel1.Controls.Add(RadioIsVisible);
            uiPanel1.Controls.Add(txtProcessName);
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
            uiPanel1.Size = new Size(519, 321);
            uiPanel1.TabIndex = 408;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel4.ForeColor = Color.Black;
            uiLabel4.Location = new Point(111, 39);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(78, 24);
            uiLabel4.TabIndex = 415;
            uiLabel4.Text = "产品类型";
            uiLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboModelType
            // 
            cboModelType.BackColor = Color.Transparent;
            cboModelType.DataSource = null;
            cboModelType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboModelType.FillColor = Color.FromArgb(218, 220, 230);
            cboModelType.FillColor2 = Color.FromArgb(218, 220, 230);
            cboModelType.FillDisableColor = Color.White;
            cboModelType.FilterMaxCount = 50;
            cboModelType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            cboModelType.ForeColor = Color.Black;
            cboModelType.ForeDisableColor = Color.Black;
            cboModelType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboModelType.Items.AddRange(new object[] { "系统管理员" });
            cboModelType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboModelType.Location = new Point(202, 37);
            cboModelType.Margin = new Padding(4, 5, 4, 5);
            cboModelType.MinimumSize = new Size(63, 0);
            cboModelType.Name = "cboModelType";
            cboModelType.Padding = new Padding(0, 0, 30, 2);
            cboModelType.Radius = 10;
            cboModelType.RectColor = Color.Gray;
            cboModelType.RectDisableColor = Color.Gray;
            cboModelType.RectSides = ToolStripStatusLabelBorderSides.None;
            cboModelType.Size = new Size(210, 30);
            cboModelType.SymbolSize = 24;
            cboModelType.TabIndex = 414;
            cboModelType.TextAlignment = ContentAlignment.MiddleLeft;
            cboModelType.Watermark = "请选择";
            // 
            // txtEntityClassName
            // 
            txtEntityClassName.BackColor = Color.Transparent;
            txtEntityClassName.Cursor = Cursors.IBeam;
            txtEntityClassName.FillColor = Color.FromArgb(218, 220, 230);
            txtEntityClassName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtEntityClassName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtEntityClassName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtEntityClassName.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtEntityClassName.ForeColor = Color.Black;
            txtEntityClassName.ForeDisableColor = Color.Black;
            txtEntityClassName.ForeReadOnlyColor = Color.Black;
            txtEntityClassName.Location = new Point(202, 143);
            txtEntityClassName.Margin = new Padding(4, 5, 4, 5);
            txtEntityClassName.MinimumSize = new Size(1, 16);
            txtEntityClassName.Name = "txtEntityClassName";
            txtEntityClassName.Padding = new Padding(5);
            txtEntityClassName.Radius = 10;
            txtEntityClassName.RectColor = Color.FromArgb(218, 220, 230);
            txtEntityClassName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtEntityClassName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtEntityClassName.ShowText = false;
            txtEntityClassName.Size = new Size(210, 30);
            txtEntityClassName.TabIndex = 413;
            txtEntityClassName.TextAlignment = ContentAlignment.MiddleLeft;
            txtEntityClassName.Watermark = "请输入";
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel2.Location = new Point(105, 144);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(84, 26);
            uiLabel2.TabIndex = 412;
            uiLabel2.Text = "关联名称";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // RadioIsVisible2
            // 
            RadioIsVisible2.BackColor = Color.Transparent;
            RadioIsVisible2.Font = new Font("微软雅黑", 12.5F);
            RadioIsVisible2.Location = new Point(311, 194);
            RadioIsVisible2.MinimumSize = new Size(1, 1);
            RadioIsVisible2.Name = "RadioIsVisible2";
            RadioIsVisible2.RadioButtonColor = Color.FromArgb(65, 100, 204);
            RadioIsVisible2.Size = new Size(97, 29);
            RadioIsVisible2.TabIndex = 411;
            RadioIsVisible2.Text = "禁用";
            // 
            // RadioIsVisible
            // 
            RadioIsVisible.BackColor = Color.Transparent;
            RadioIsVisible.Checked = true;
            RadioIsVisible.Font = new Font("微软雅黑", 12.5F);
            RadioIsVisible.Location = new Point(223, 194);
            RadioIsVisible.MinimumSize = new Size(1, 1);
            RadioIsVisible.Name = "RadioIsVisible";
            RadioIsVisible.RadioButtonColor = Color.FromArgb(65, 100, 204);
            RadioIsVisible.Size = new Size(97, 29);
            RadioIsVisible.TabIndex = 410;
            RadioIsVisible.Text = "启用";
            // 
            // txtProcessName
            // 
            txtProcessName.BackColor = Color.Transparent;
            txtProcessName.Cursor = Cursors.IBeam;
            txtProcessName.FillColor = Color.FromArgb(218, 220, 230);
            txtProcessName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtProcessName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtProcessName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtProcessName.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtProcessName.ForeColor = Color.Black;
            txtProcessName.ForeDisableColor = Color.Black;
            txtProcessName.ForeReadOnlyColor = Color.Black;
            txtProcessName.Location = new Point(202, 90);
            txtProcessName.Margin = new Padding(4, 5, 4, 5);
            txtProcessName.MinimumSize = new Size(1, 16);
            txtProcessName.Name = "txtProcessName";
            txtProcessName.Padding = new Padding(5);
            txtProcessName.Radius = 10;
            txtProcessName.RectColor = Color.FromArgb(218, 220, 230);
            txtProcessName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtProcessName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtProcessName.ShowText = false;
            txtProcessName.Size = new Size(210, 30);
            txtProcessName.TabIndex = 409;
            txtProcessName.TextAlignment = ContentAlignment.MiddleLeft;
            txtProcessName.Watermark = "请输入";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(105, 195);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(84, 26);
            uiLabel3.TabIndex = 74;
            uiLabel3.Text = "是否启用";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FrmTestProcess
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(585, 404);
            ControlBox = false;
            Controls.Add(uiPanel1);
            Font = new Font("微软雅黑", 11F);
            ForeColor = Color.FromArgb(236, 236, 236);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(0, 0);
            MinimizeBox = false;
            Name = "FrmTestProcess";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "试验项点修改";
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
        private UILabel uiLabel3;
        private UITextBox txtProcessName;
        private UIRadioButton RadioIsVisible2;
        private UIRadioButton RadioIsVisible;
        private UITextBox txtEntityClassName;
        private UILabel uiLabel2;
        private UILabel uiLabel4;
        private UIComboBox cboModelType;
    }
}
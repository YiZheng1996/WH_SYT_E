namespace MainUI
{
    partial class frmMeteringEdit
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
            labPrompt = new UILabel();
            dateNext = new UIDatePicker();
            txtDescribe = new UIRichTextBox();
            uiLabel5 = new UILabel();
            dateCurrent = new UIDatePicker();
            CboInspectType = new UIComboBox();
            txtInspectName = new UITextBox();
            uiLabel4 = new UILabel();
            uiLabel2 = new UILabel();
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
            uiLabel1.Location = new Point(142, 54);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(49, 24);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "类 型";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCancel.Location = new Point(280, 483);
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
            btnSubmit.Location = new Point(116, 483);
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
            uiPanel1.Controls.Add(labPrompt);
            uiPanel1.Controls.Add(dateNext);
            uiPanel1.Controls.Add(txtDescribe);
            uiPanel1.Controls.Add(uiLabel5);
            uiPanel1.Controls.Add(dateCurrent);
            uiPanel1.Controls.Add(CboInspectType);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(txtInspectName);
            uiPanel1.Controls.Add(btnCancel);
            uiPanel1.Controls.Add(btnSubmit);
            uiPanel1.Controls.Add(uiLabel4);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Font = new Font("宋体", 12F);
            uiPanel1.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel1.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Location = new Point(73, 68);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(519, 556);
            uiPanel1.TabIndex = 408;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // labPrompt
            // 
            labPrompt.BackColor = Color.Transparent;
            labPrompt.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            labPrompt.ForeColor = Color.Red;
            labPrompt.Location = new Point(37, 5);
            labPrompt.Name = "labPrompt";
            labPrompt.Size = new Size(445, 37);
            labPrompt.TabIndex = 407;
            labPrompt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dateNext
            // 
            dateNext.BackColor = Color.Transparent;
            dateNext.FillColor = Color.FromArgb(218, 220, 230);
            dateNext.FillColor2 = Color.FromArgb(218, 220, 230);
            dateNext.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dateNext.ForeColor = Color.Black;
            dateNext.Location = new Point(202, 211);
            dateNext.Margin = new Padding(4, 5, 4, 5);
            dateNext.MaxLength = 10;
            dateNext.MinimumSize = new Size(63, 0);
            dateNext.Name = "dateNext";
            dateNext.Padding = new Padding(0, 0, 30, 2);
            dateNext.RectColor = Color.Gray;
            dateNext.RectDisableColor = Color.Gray;
            dateNext.RectSides = ToolStripStatusLabelBorderSides.None;
            dateNext.Size = new Size(210, 31);
            dateNext.SymbolDropDown = 61555;
            dateNext.SymbolNormal = 61555;
            dateNext.SymbolSize = 24;
            dateNext.TabIndex = 406;
            dateNext.Text = "2025-05-26";
            dateNext.TextAlignment = ContentAlignment.MiddleLeft;
            dateNext.Value = new DateTime(2025, 5, 26, 11, 37, 21, 220);
            dateNext.Watermark = "";
            // 
            // txtDescribe
            // 
            txtDescribe.BackColor = Color.Transparent;
            txtDescribe.FillColor = Color.FromArgb(218, 220, 230);
            txtDescribe.FillColor2 = Color.FromArgb(218, 220, 230);
            txtDescribe.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtDescribe.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtDescribe.ForeColor = Color.Black;
            txtDescribe.ForeDisableColor = Color.Black;
            txtDescribe.Location = new Point(202, 265);
            txtDescribe.Margin = new Padding(4, 5, 4, 5);
            txtDescribe.MinimumSize = new Size(1, 1);
            txtDescribe.Name = "txtDescribe";
            txtDescribe.Padding = new Padding(2);
            txtDescribe.Radius = 10;
            txtDescribe.RectColor = Color.FromArgb(218, 220, 230);
            txtDescribe.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtDescribe.ShowText = false;
            txtDescribe.Size = new Size(210, 180);
            txtDescribe.TabIndex = 405;
            txtDescribe.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel5
            // 
            uiLabel5.AutoSize = true;
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel5.ImeMode = ImeMode.NoControl;
            uiLabel5.Location = new Point(113, 262);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(78, 24);
            uiLabel5.TabIndex = 403;
            uiLabel5.Text = "检查说明";
            uiLabel5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dateCurrent
            // 
            dateCurrent.BackColor = Color.Transparent;
            dateCurrent.FillColor = Color.FromArgb(218, 220, 230);
            dateCurrent.FillColor2 = Color.FromArgb(218, 220, 230);
            dateCurrent.FillDisableColor = Color.FromArgb(218, 220, 230);
            dateCurrent.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dateCurrent.ForeColor = Color.Black;
            dateCurrent.Location = new Point(202, 157);
            dateCurrent.Margin = new Padding(4, 5, 4, 5);
            dateCurrent.MaxLength = 10;
            dateCurrent.MinimumSize = new Size(63, 0);
            dateCurrent.Name = "dateCurrent";
            dateCurrent.Padding = new Padding(0, 0, 30, 2);
            dateCurrent.RectColor = Color.Gray;
            dateCurrent.RectDisableColor = Color.Gray;
            dateCurrent.RectSides = ToolStripStatusLabelBorderSides.None;
            dateCurrent.Size = new Size(210, 31);
            dateCurrent.SymbolDropDown = 61555;
            dateCurrent.SymbolNormal = 61555;
            dateCurrent.SymbolSize = 24;
            dateCurrent.TabIndex = 400;
            dateCurrent.Text = "2025-05-26";
            dateCurrent.TextAlignment = ContentAlignment.MiddleLeft;
            dateCurrent.Value = new DateTime(2025, 5, 26, 11, 37, 21, 220);
            dateCurrent.Watermark = "";
            // 
            // CboInspectType
            // 
            CboInspectType.BackColor = Color.Transparent;
            CboInspectType.DataSource = null;
            CboInspectType.DropDownStyle = UIDropDownStyle.DropDownList;
            CboInspectType.FillColor = Color.FromArgb(218, 220, 230);
            CboInspectType.FillColor2 = Color.FromArgb(218, 220, 230);
            CboInspectType.FillDisableColor = Color.FromArgb(218, 220, 230);
            CboInspectType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            CboInspectType.ForeColor = Color.Black;
            CboInspectType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            CboInspectType.Items.AddRange(new object[] { "计量", "维保" });
            CboInspectType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            CboInspectType.Location = new Point(202, 54);
            CboInspectType.Margin = new Padding(4, 5, 4, 5);
            CboInspectType.MinimumSize = new Size(63, 0);
            CboInspectType.Name = "CboInspectType";
            CboInspectType.Padding = new Padding(0, 0, 30, 2);
            CboInspectType.Radius = 10;
            CboInspectType.RectColor = Color.Gray;
            CboInspectType.RectDisableColor = Color.Gray;
            CboInspectType.RectSides = ToolStripStatusLabelBorderSides.None;
            CboInspectType.Size = new Size(210, 31);
            CboInspectType.SymbolSize = 24;
            CboInspectType.TabIndex = 399;
            CboInspectType.TextAlignment = ContentAlignment.MiddleLeft;
            CboInspectType.Watermark = "请选择";
            // 
            // txtInspectName
            // 
            txtInspectName.BackColor = Color.Transparent;
            txtInspectName.Cursor = Cursors.IBeam;
            txtInspectName.FillColor = Color.FromArgb(218, 220, 230);
            txtInspectName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtInspectName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtInspectName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtInspectName.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtInspectName.ForeColor = Color.Black;
            txtInspectName.ForeDisableColor = Color.Black;
            txtInspectName.ForeReadOnlyColor = Color.Black;
            txtInspectName.Location = new Point(202, 106);
            txtInspectName.Margin = new Padding(4, 5, 4, 5);
            txtInspectName.MinimumSize = new Size(1, 16);
            txtInspectName.Name = "txtInspectName";
            txtInspectName.Padding = new Padding(5);
            txtInspectName.Radius = 10;
            txtInspectName.RectColor = Color.FromArgb(218, 220, 230);
            txtInspectName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtInspectName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtInspectName.ShowText = false;
            txtInspectName.Size = new Size(210, 30);
            txtInspectName.TabIndex = 69;
            txtInspectName.TextAlignment = ContentAlignment.MiddleLeft;
            txtInspectName.Watermark = "请输入";
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel4.ImeMode = ImeMode.NoControl;
            uiLabel4.Location = new Point(54, 212);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(137, 24);
            uiLabel4.TabIndex = 75;
            uiLabel4.Text = "下次检查周期/天";
            uiLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel2.ImeMode = ImeMode.NoControl;
            uiLabel2.Location = new Point(142, 106);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(49, 24);
            uiLabel2.TabIndex = 73;
            uiLabel2.Text = "名 称";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(43, 46, 57);
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(79, 160);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(112, 24);
            uiLabel3.TabIndex = 74;
            uiLabel3.Text = "当前检查日期";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmMeteringEdit
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(665, 655);
            Controls.Add(uiPanel1);
            Font = new Font("微软雅黑", 11F);
            ForeColor = Color.FromArgb(236, 236, 236);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(0, 0);
            MinimizeBox = false;
            Name = "frmMeteringEdit";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "维保计量数据修改";
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
        private UITextBox txtInspectName;
        private UILabel uiLabel4;
        private UILabel uiLabel2;
        private UILabel uiLabel3;
        private UIComboBox CboInspectType;
        private UIDatePicker dateCurrent;
        private UILabel uiLabel5;
        private UIRichTextBox txtDescribe;
        private UIDatePicker dateNext;
        private UILabel labPrompt;
    }
}
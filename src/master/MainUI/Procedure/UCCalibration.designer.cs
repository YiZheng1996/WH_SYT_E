namespace MainUI.Procedure
{
    partial class UCCalibration
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCalibration));
            uiPanel1 = new UIPanel();
            lblText = new UILabel();
            nudGainValue = new UITextBox();
            nudZeroValue = new UITextBox();
            btnReset = new UIButton();
            btnOK = new UIButton();
            txtValue = new UITextBox();
            cboChannel = new ComboBox();
            uiPanel1.SuspendLayout();
            txtValue.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.BackColor = Color.Transparent;
            uiPanel1.Controls.Add(txtValue);
            uiPanel1.Controls.Add(lblText);
            uiPanel1.Controls.Add(nudGainValue);
            uiPanel1.Controls.Add(nudZeroValue);
            uiPanel1.Controls.Add(btnReset);
            uiPanel1.Controls.Add(btnOK);
            resources.ApplyResources(uiPanel1, "uiPanel1");
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblText
            // 
            lblText.BackColor = Color.Transparent;
            resources.ApplyResources(lblText, "lblText");
            lblText.ForeColor = Color.FromArgb(46, 46, 46);
            lblText.Name = "lblText";
            // 
            // nudGainValue
            // 
            nudGainValue.Cursor = Cursors.IBeam;
            nudGainValue.DecimalPlaces = 3;
            nudGainValue.FillColor = Color.FromArgb(218, 220, 230);
            nudGainValue.FillColor2 = Color.FromArgb(218, 220, 230);
            nudGainValue.FillDisableColor = Color.FromArgb(218, 220, 230);
            nudGainValue.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            resources.ApplyResources(nudGainValue, "nudGainValue");
            nudGainValue.ForeColor = Color.FromArgb(46, 46, 46);
            nudGainValue.ForeDisableColor = Color.FromArgb(46, 46, 46);
            nudGainValue.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            nudGainValue.Name = "nudGainValue";
            nudGainValue.RectColor = Color.FromArgb(218, 220, 230);
            nudGainValue.RectDisableColor = Color.FromArgb(218, 220, 230);
            nudGainValue.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            nudGainValue.ShowText = false;
            nudGainValue.TextAlignment = ContentAlignment.MiddleLeft;
            nudGainValue.Type = UITextBox.UIEditType.Double;
            nudGainValue.Watermark = "请输入";
            nudGainValue.Click += cboChannel_Click;
            nudGainValue.TextChanged += nudGainValue_ValueChanged;
            // 
            // nudZeroValue
            // 
            nudZeroValue.Cursor = Cursors.IBeam;
            nudZeroValue.FillColor = Color.FromArgb(218, 220, 230);
            nudZeroValue.FillColor2 = Color.FromArgb(218, 220, 230);
            nudZeroValue.FillDisableColor = Color.FromArgb(218, 220, 230);
            nudZeroValue.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            resources.ApplyResources(nudZeroValue, "nudZeroValue");
            nudZeroValue.ForeColor = Color.FromArgb(46, 46, 46);
            nudZeroValue.ForeDisableColor = Color.FromArgb(46, 46, 46);
            nudZeroValue.Name = "nudZeroValue";
            nudZeroValue.RectColor = Color.FromArgb(218, 220, 230);
            nudZeroValue.RectDisableColor = Color.FromArgb(218, 220, 230);
            nudZeroValue.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            nudZeroValue.ShowText = false;
            nudZeroValue.TextAlignment = ContentAlignment.MiddleLeft;
            nudZeroValue.Type = UITextBox.UIEditType.Double;
            nudZeroValue.Watermark = "请输入";
            nudZeroValue.Click += cboChannel_Click;
            nudZeroValue.TextChanged += nudZeroDisplacement_ValueChanged;
            // 
            // btnReset
            // 
            btnReset.Cursor = Cursors.Hand;
            btnReset.FillDisableColor = Color.FromArgb(80, 160, 255);
            resources.ApplyResources(btnReset, "btnReset");
            btnReset.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnReset.Name = "btnReset";
            btnReset.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnReset.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnReset.TipsText = "1";
            btnReset.Click += btnClear_Click;
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.FillDisableColor = Color.FromArgb(80, 160, 255);
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnOK.Name = "btnOK";
            btnOK.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnOK.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOK.TipsText = "1";
            btnOK.Click += btnOK_Click;
            // 
            // txtValue
            // 
            txtValue.Controls.Add(cboChannel);
            resources.ApplyResources(txtValue, "txtValue");
            txtValue.FillColor = Color.FromArgb(218, 220, 230);
            txtValue.FillColor2 = Color.FromArgb(218, 220, 230);
            txtValue.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtValue.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtValue.ForeColor = Color.FromArgb(46, 46, 46);
            txtValue.ForeDisableColor = Color.FromArgb(46, 46, 46);
            txtValue.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.RectColor = Color.FromArgb(218, 220, 230);
            txtValue.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtValue.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtValue.ShowText = false;
            txtValue.TextAlignment = ContentAlignment.MiddleLeft;
            txtValue.Watermark = "请选择";
            txtValue.TextChanged += txtValue_TextChanged;
            // 
            // cboChannel
            // 
            cboChannel.ForeColor = Color.FromArgb(46, 46, 46);
            resources.ApplyResources(cboChannel, "cboChannel");
            cboChannel.Name = "cboChannel";
            cboChannel.Click += cboChannel_Click;
            // 
            // UCCalibration
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(uiPanel1);
            Name = "UCCalibration";
            Load += UCCalibration_Load;
            uiPanel1.ResumeLayout(false);
            txtValue.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UILabel lblText;
        private Sunny.UI.UITextBox txtValue;
        protected System.Windows.Forms.ComboBox cboChannel;
        private Sunny.UI.UITextBox nudGainValue;
        private Sunny.UI.UITextBox nudZeroValue;
        private Sunny.UI.UIButton btnReset;
        private Sunny.UI.UIButton btnOK;
    }
}

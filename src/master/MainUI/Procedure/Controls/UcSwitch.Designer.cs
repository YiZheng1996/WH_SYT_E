namespace MainUI.Procedure.Controls
{
    partial class UcSwitch
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            uiSwitch3 = new UISwitch();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("宋体", 13F);
            label1.ForeColor = Color.FromArgb(64, 64, 64);
            label1.Location = new Point(18, 42);
            label1.Name = "label1";
            label1.Size = new Size(116, 18);
            label1.TabIndex = 8;
            label1.Tag = "";
            label1.Text = "水泵电机供电";
            // 
            // uiSwitch3
            // 
            uiSwitch3.ActiveColor = Color.FromArgb(193, 218, 93);
            uiSwitch3.BackColor = Color.Transparent;
            uiSwitch3.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            uiSwitch3.ForeColor = Color.FromArgb(235, 227, 221);
            uiSwitch3.InActiveColor = Color.FromArgb(231, 54, 36);
            uiSwitch3.Location = new Point(37, 3);
            uiSwitch3.MinimumSize = new Size(1, 1);
            uiSwitch3.MultiLanguageSupport = false;
            uiSwitch3.Name = "uiSwitch3";
            uiSwitch3.Radius = 30;
            uiSwitch3.Size = new Size(79, 33);
            uiSwitch3.SwitchShape = UISwitch.UISwitchShape.Square;
            uiSwitch3.TabIndex = 7;
            uiSwitch3.Tag = "2";
            uiSwitch3.TagString = "";
            uiSwitch3.Text = "123";
            // 
            // UcSwitch
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(label1);
            Controls.Add(uiSwitch3);
            Name = "UcSwitch";
            Size = new Size(150, 64);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private UISwitch uiSwitch3;
    }
}

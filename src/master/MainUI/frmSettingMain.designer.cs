namespace MainUI
{
    partial class frmSettingMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettingMain));
            treeView1 = new UITreeView();
            imageList1 = new ImageList(components);
            pnlMain = new UIPanel();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.BackColor = Color.Transparent;
            treeView1.FillColor = Color.White;
            treeView1.FillColor2 = Color.White;
            treeView1.FillDisableColor = Color.White;
            resources.ApplyResources(treeView1, "treeView1");
            treeView1.ForeColor = Color.FromArgb(64, 64, 64);
            treeView1.ForeDisableColor = Color.FromArgb(64, 64, 64);
            treeView1.HoverColor = Color.FromArgb(80, 160, 255);
            treeView1.ImageList = imageList1;
            treeView1.ItemHeight = 50;
            treeView1.Name = "treeView1";
            treeView1.RectColor = Color.White;
            treeView1.RectDisableColor = Color.White;
            treeView1.ScrollBarColor = Color.FromArgb(239, 154, 78);
            treeView1.ScrollBarRectColor = Color.FromArgb(239, 154, 78);
            treeView1.ScrollBarStyleInherited = false;
            treeView1.ShowText = false;
            treeView1.Style = UIStyle.Custom;
            treeView1.TextAlignment = ContentAlignment.MiddleCenter;
            treeView1.BeforeSelect += treeView1_BeforeSelect;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "用户管理-25+25.png");
            imageList1.Images.SetKeyName(1, "型号管理-25+25.png");
            imageList1.Images.SetKeyName(2, "参数设置-25+25.png");
            imageList1.Images.SetKeyName(3, "模块.png");
            imageList1.Images.SetKeyName(4, "农事采集项_点击状态.png");
            imageList1.Images.SetKeyName(5, "配置-分区配置.png");
            imageList1.Images.SetKeyName(6, "角色管理.png");
            imageList1.Images.SetKeyName(7, "账号权限管理.png");
            imageList1.Images.SetKeyName(8, "权限分配.png");
            imageList1.Images.SetKeyName(9, "类型管理.png");
            // 
            // pnlMain
            // 
            pnlMain.FillColor = Color.White;
            pnlMain.FillColor2 = Color.White;
            resources.ApplyResources(pnlMain, "pnlMain");
            pnlMain.ForeColor = Color.FromArgb(46, 46, 46);
            pnlMain.ForeDisableColor = Color.FromArgb(46, 46, 46);
            pnlMain.Name = "pnlMain";
            pnlMain.RectColor = Color.White;
            pnlMain.RectDisableColor = Color.White;
            pnlMain.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // frmSettingMain
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(224, 224, 224);
            resources.ApplyResources(this, "$this");
            ControlBoxFillHoverColor = Color.FromArgb(42, 47, 55);
            Controls.Add(pnlMain);
            Controls.Add(treeView1);
            ForeColor = Color.FromArgb(235, 227, 221);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSettingMain";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ZoomScaleRect = new Rectangle(15, 15, 854, 666);
            Load += frmMain_Load;
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UITreeView treeView1;
        private UIPanel pnlMain;
        private ImageList imageList1;
    }
}
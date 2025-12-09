namespace MainUI.LogicalConfiguration.Controls
{
    partial class ToolTreeViewControl
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
                _treeView?.Dispose();
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolTreeViewControl));
            imageList1 = new ImageList(components);
            SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "文件夹.png");
            imageList1.Images.SetKeyName(1, "条件判断.png");
            imageList1.Images.SetKeyName(2, "延时等待.png");
            imageList1.Images.SetKeyName(3, "数据读取.png");
            imageList1.Images.SetKeyName(4, "变量赋值.png");
            imageList1.Images.SetKeyName(5, "数据计算.png");
            imageList1.Images.SetKeyName(6, "消息通知.png");
            imageList1.Images.SetKeyName(7, "循环工具.png");
            imageList1.Images.SetKeyName(8, "报表读取.png");
            imageList1.Images.SetKeyName(9, "报表写入.png");
            imageList1.Images.SetKeyName(10, "读取PLC.png");
            imageList1.Images.SetKeyName(11, "写入PLC.png");
            imageList1.Images.SetKeyName(12, "等待稳定.png");
            imageList1.Images.SetKeyName(13, "检测工具.png");
            // 
            // ToolTreeViewControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Name = "ToolTreeViewControl";
            Size = new Size(250, 500);
            ResumeLayout(false);
        }

        #endregion

        private ImageList imageList1;
    }
}

namespace MainUI
{
    partial class FrmCommunicationTestSelector
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            uiPanel1 = new UIPanel();
            uiLabel5 = new UILabel();
            uiLabel4 = new UILabel();
            uiLabel3 = new UILabel();
            uiLabel2 = new UILabel();
            btnAutoTest = new UISymbolButton();
            btnSerialLoopbackTest = new UISymbolButton();
            btnCommunicationTest = new UISymbolButton();
            btnTcpServerTest = new UISymbolButton();
            uiLabel1 = new UILabel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(uiLabel5);
            uiPanel1.Controls.Add(uiLabel4);
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(btnAutoTest);
            uiPanel1.Controls.Add(btnSerialLoopbackTest);
            uiPanel1.Controls.Add(btnCommunicationTest);
            uiPanel1.Controls.Add(btnTcpServerTest);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Dock = DockStyle.Fill;
            uiPanel1.Font = new Font("微软雅黑", 12F);
            uiPanel1.Location = new Point(0, 35);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Padding = new Padding(20);
            uiPanel1.Size = new Size(500, 365);
            uiPanel1.TabIndex = 0;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel5
            // 
            uiLabel5.Font = new Font("微软雅黑", 10F);
            uiLabel5.ForeColor = Color.Gray;
            uiLabel5.Location = new Point(240, 280);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(240, 45);
            uiLabel5.TabIndex = 8;
            uiLabel5.Text = "自动执行所有测试用例并生成报告";
            uiLabel5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel4
            // 
            uiLabel4.Font = new Font("微软雅黑", 10F);
            uiLabel4.ForeColor = Color.Gray;
            uiLabel4.Location = new Point(240, 210);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(240, 45);
            uiLabel4.TabIndex = 7;
            uiLabel4.Text = "启动虚拟串口回环用于串口测试";
            uiLabel4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            uiLabel3.Font = new Font("微软雅黑", 10F);
            uiLabel3.ForeColor = Color.Gray;
            uiLabel3.Location = new Point(240, 140);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(240, 45);
            uiLabel3.TabIndex = 6;
            uiLabel3.Text = "手动测试TCP/串口通讯收发功能";
            uiLabel3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            uiLabel2.Font = new Font("微软雅黑", 10F);
            uiLabel2.ForeColor = Color.Gray;
            uiLabel2.Location = new Point(240, 70);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(240, 45);
            uiLabel2.TabIndex = 5;
            uiLabel2.Text = "启动本地TCP服务器用于测试客户端连接";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnAutoTest
            // 
            btnAutoTest.Font = new Font("微软雅黑", 12F);
            btnAutoTest.Location = new Point(40, 280);
            btnAutoTest.MinimumSize = new Size(1, 1);
            btnAutoTest.Name = "btnAutoTest";
            btnAutoTest.Size = new Size(180, 45);
            btnAutoTest.Symbol = 61762;
            btnAutoTest.TabIndex = 4;
            btnAutoTest.Text = "运行自动化测试";
            btnAutoTest.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAutoTest.Click += btnAutoTest_Click;
            // 
            // btnSerialLoopbackTest
            // 
            btnSerialLoopbackTest.Font = new Font("微软雅黑", 12F);
            btnSerialLoopbackTest.Location = new Point(40, 210);
            btnSerialLoopbackTest.MinimumSize = new Size(1, 1);
            btnSerialLoopbackTest.Name = "btnSerialLoopbackTest";
            btnSerialLoopbackTest.Size = new Size(180, 45);
            btnSerialLoopbackTest.Symbol = 61726;
            btnSerialLoopbackTest.TabIndex = 3;
            btnSerialLoopbackTest.Text = "串口回环测试";
            btnSerialLoopbackTest.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSerialLoopbackTest.Click += btnSerialLoopbackTest_Click;
            // 
            // btnCommunicationTest
            // 
            btnCommunicationTest.Font = new Font("微软雅黑", 12F);
            btnCommunicationTest.Location = new Point(40, 140);
            btnCommunicationTest.MinimumSize = new Size(1, 1);
            btnCommunicationTest.Name = "btnCommunicationTest";
            btnCommunicationTest.Size = new Size(180, 45);
            btnCommunicationTest.Symbol = 61641;
            btnCommunicationTest.TabIndex = 2;
            btnCommunicationTest.Text = "通讯测试工具";
            btnCommunicationTest.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCommunicationTest.Click += btnCommunicationTest_Click;
            // 
            // btnTcpServerTest
            // 
            btnTcpServerTest.Font = new Font("微软雅黑", 12F);
            btnTcpServerTest.Location = new Point(40, 70);
            btnTcpServerTest.MinimumSize = new Size(1, 1);
            btnTcpServerTest.Name = "btnTcpServerTest";
            btnTcpServerTest.Size = new Size(180, 45);
            btnTcpServerTest.Symbol = 61530;
            btnTcpServerTest.TabIndex = 1;
            btnTcpServerTest.Text = "TCP测试服务器";
            btnTcpServerTest.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnTcpServerTest.Click += btnTcpServerTest_Click;
            // 
            // uiLabel1
            // 
            uiLabel1.Font = new Font("微软雅黑", 14F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(20, 20);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(460, 35);
            uiLabel1.TabIndex = 0;
            uiLabel1.Text = "请选择测试工具";
            uiLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmCommunicationTestSelector
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 400);
            Controls.Add(uiPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCommunicationTestSelector";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "通讯测试工具";
            ZoomScaleRect = new Rectangle(15, 15, 500, 400);
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UISymbolButton btnAutoTest;
        private Sunny.UI.UISymbolButton btnSerialLoopbackTest;
        private Sunny.UI.UISymbolButton btnCommunicationTest;
        private Sunny.UI.UISymbolButton btnTcpServerTest;
        private Sunny.UI.UILabel uiLabel1;
    }
}
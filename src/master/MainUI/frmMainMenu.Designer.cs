
namespace MainUI
{
    partial class frmMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainMenu));
            panel1 = new Panel();
            lblTitle = new UIPanel();
            lblDateTime = new Label();
            picRunStatus = new PictureBox();
            uiPanel2 = new UIPanel();
            Logo = new PictureBox();
            btnExit = new UISymbolButton();
            btnChangePwd = new UISymbolButton();
            timerPLC = new System.Windows.Forms.Timer(components);
            statusStrip1 = new StatusStrip();
            tslblUser = new ToolStripStatusLabel();
            tslblPLC = new ToolStripStatusLabel();
            tslblWorkStation = new ToolStripStatusLabel();
            timerHeartbeat = new System.Windows.Forms.Timer(components);
            panelmue = new UIPanel();
            btnDeviceDetection = new UISymbolButton();
            btnErrStatistics = new UISymbolButton();
            btnMeteringRemind = new UISymbolButton();
            btnNLog = new UISymbolButton();
            btnHardwareTest = new UISymbolButton();
            btnReports = new UISymbolButton();
            btnMainData = new UISymbolButton();
            PanelHmi = new UIPanel();
            label5 = new AntdUI.Label();
            panel1.SuspendLayout();
            lblTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picRunStatus).BeginInit();
            uiPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            statusStrip1.SuspendLayout();
            panelmue.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(236, 236, 237);
            panel1.Controls.Add(lblTitle);
            panel1.Controls.Add(uiPanel2);
            panel1.Dock = DockStyle.Top;
            panel1.Font = new Font("宋体", 10.5F);
            panel1.ForeColor = Color.FromArgb(235, 227, 221);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1280, 53);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Controls.Add(lblDateTime);
            lblTitle.Controls.Add(picRunStatus);
            lblTitle.FillColor = Color.FromArgb(65, 100, 204);
            lblTitle.FillColor2 = Color.FromArgb(65, 100, 204);
            lblTitle.Font = new Font("微软雅黑", 19F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.ForeDisableColor = Color.White;
            lblTitle.Location = new Point(217, 0);
            lblTitle.Margin = new Padding(4, 5, 4, 5);
            lblTitle.MinimumSize = new Size(1, 1);
            lblTitle.Name = "lblTitle";
            lblTitle.Radius = 40;
            lblTitle.RectColor = Color.FromArgb(65, 100, 204);
            lblTitle.RectDisableColor = Color.FromArgb(65, 100, 204);
            lblTitle.Size = new Size(1063, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "试验台名称";
            lblTitle.TextAlignment = ContentAlignment.MiddleLeft;
            lblTitle.MouseDown += lblTitle_MouseDown;
            // 
            // lblDateTime
            // 
            lblDateTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDateTime.AutoSize = true;
            lblDateTime.BackColor = Color.Transparent;
            lblDateTime.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblDateTime.ForeColor = Color.White;
            lblDateTime.Location = new Point(807, 14);
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(179, 24);
            lblDateTime.TabIndex = 4;
            lblDateTime.Text = "2016-09-13 00:00:00";
            // 
            // picRunStatus
            // 
            picRunStatus.BackColor = Color.Transparent;
            picRunStatus.Image = (Image)resources.GetObject("picRunStatus.Image");
            picRunStatus.Location = new Point(995, 2);
            picRunStatus.Name = "picRunStatus";
            picRunStatus.Size = new Size(47, 43);
            picRunStatus.SizeMode = PictureBoxSizeMode.CenterImage;
            picRunStatus.TabIndex = 1;
            picRunStatus.TabStop = false;
            // 
            // uiPanel2
            // 
            uiPanel2.Controls.Add(Logo);
            uiPanel2.FillColor = Color.FromArgb(65, 100, 204);
            uiPanel2.FillColor2 = Color.FromArgb(65, 100, 204);
            uiPanel2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel2.Location = new Point(7, 0);
            uiPanel2.Margin = new Padding(4, 5, 4, 5);
            uiPanel2.MinimumSize = new Size(1, 1);
            uiPanel2.Name = "uiPanel2";
            uiPanel2.Radius = 30;
            uiPanel2.RectColor = Color.FromArgb(65, 100, 204);
            uiPanel2.RectDisableColor = Color.FromArgb(65, 100, 204);
            uiPanel2.Size = new Size(204, 50);
            uiPanel2.TabIndex = 403;
            uiPanel2.Text = "uiPanel2";
            uiPanel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // Logo
            // 
            Logo.BackColor = Color.FromArgb(65, 100, 204);
            Logo.Image = (Image)resources.GetObject("Logo.Image");
            Logo.Location = new Point(36, 3);
            Logo.Name = "Logo";
            Logo.Size = new Size(133, 44);
            Logo.SizeMode = PictureBoxSizeMode.StretchImage;
            Logo.TabIndex = 2;
            Logo.TabStop = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Transparent;
            btnExit.FillColor = Color.FromArgb(65, 100, 204);
            btnExit.FillColor2 = Color.FromArgb(65, 100, 204);
            btnExit.FillDisableColor = Color.FromArgb(113, 143, 226);
            btnExit.FillHoverColor = Color.FromArgb(113, 143, 226);
            btnExit.FillPressColor = Color.FromArgb(65, 100, 204);
            btnExit.FillSelectedColor = Color.FromArgb(113, 143, 226);
            btnExit.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnExit.ForeColor = Color.FromArgb(235, 227, 221);
            btnExit.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnExit.Image = (Image)resources.GetObject("btnExit.Image");
            btnExit.Location = new Point(3, 834);
            btnExit.MinimumSize = new Size(1, 1);
            btnExit.Name = "btnExit";
            btnExit.Radius = 30;
            btnExit.RectColor = Color.FromArgb(65, 100, 204);
            btnExit.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnExit.RectHoverColor = Color.FromArgb(113, 143, 226);
            btnExit.RectPressColor = Color.FromArgb(65, 100, 204);
            btnExit.RectSelectedColor = Color.FromArgb(113, 143, 226);
            btnExit.Size = new Size(107, 49);
            btnExit.Symbol = 0;
            btnExit.SymbolDisableColor = Color.FromArgb(90, 95, 102);
            btnExit.SymbolSize = 25;
            btnExit.TabIndex = 119;
            btnExit.Text = "退 出    ";
            btnExit.TipsFont = new Font("宋体", 11F);
            btnExit.Click += btnExit_Click;
            // 
            // btnChangePwd
            // 
            btnChangePwd.BackColor = Color.Transparent;
            btnChangePwd.FillColor = Color.FromArgb(65, 100, 204);
            btnChangePwd.FillColor2 = Color.FromArgb(65, 100, 204);
            btnChangePwd.FillDisableColor = Color.FromArgb(113, 143, 226);
            btnChangePwd.FillHoverColor = Color.FromArgb(113, 143, 226);
            btnChangePwd.FillPressColor = Color.FromArgb(65, 100, 204);
            btnChangePwd.FillSelectedColor = Color.FromArgb(113, 143, 226);
            btnChangePwd.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnChangePwd.ForeColor = Color.FromArgb(235, 227, 221);
            btnChangePwd.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnChangePwd.Image = (Image)resources.GetObject("btnChangePwd.Image");
            btnChangePwd.Location = new Point(3, 785);
            btnChangePwd.MinimumSize = new Size(1, 1);
            btnChangePwd.Name = "btnChangePwd";
            btnChangePwd.Radius = 30;
            btnChangePwd.RectColor = Color.FromArgb(65, 100, 204);
            btnChangePwd.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnChangePwd.RectHoverColor = Color.FromArgb(113, 143, 226);
            btnChangePwd.RectPressColor = Color.FromArgb(65, 100, 204);
            btnChangePwd.RectSelectedColor = Color.FromArgb(113, 143, 226);
            btnChangePwd.Size = new Size(107, 49);
            btnChangePwd.Symbol = 0;
            btnChangePwd.SymbolDisableColor = Color.FromArgb(90, 95, 102);
            btnChangePwd.SymbolSize = 25;
            btnChangePwd.TabIndex = 118;
            btnChangePwd.Text = "更改密码";
            btnChangePwd.TipsFont = new Font("宋体", 11F);
            btnChangePwd.Click += btnChangePwd_Click;
            // 
            // timerPLC
            // 
            timerPLC.Interval = 1000;
            timerPLC.Tick += timerPLC_Tick;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(227, 200, 227);
            statusStrip1.Font = new Font("思源黑体 CN Bold", 9F, FontStyle.Bold);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tslblUser, tslblWorkStation, tslblPLC });
            statusStrip1.Location = new Point(0, 954);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1280, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // tslblUser
            // 
            tslblUser.Font = new Font("微软雅黑", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            tslblUser.ForeColor = Color.FromArgb(64, 64, 64);
            tslblUser.Name = "tslblUser";
            tslblUser.Size = new Size(56, 17);
            tslblUser.Text = "用户名称";
            // 
            // tslblPLC
            // 
            tslblPLC.Font = new Font("微软雅黑", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            tslblPLC.ForeColor = Color.FromArgb(64, 64, 64);
            tslblPLC.Name = "tslblPLC";
            tslblPLC.Size = new Size(55, 17);
            tslblPLC.Text = "PLC状态";
            // 
            // tslblWorkStation
            // 
            tslblWorkStation.Font = new Font("微软雅黑", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            tslblWorkStation.ForeColor = Color.FromArgb(64, 64, 64);
            tslblWorkStation.Name = "tslblWorkStation";
            tslblWorkStation.Size = new Size(56, 17);
            tslblWorkStation.Text = "工位信息";
            // 
            // timerHeartbeat
            // 
            timerHeartbeat.Interval = 5000;
            // 
            // panelmue
            // 
            panelmue.Controls.Add(btnDeviceDetection);
            panelmue.Controls.Add(btnErrStatistics);
            panelmue.Controls.Add(btnMeteringRemind);
            panelmue.Controls.Add(btnNLog);
            panelmue.Controls.Add(btnHardwareTest);
            panelmue.Controls.Add(btnReports);
            panelmue.Controls.Add(btnExit);
            panelmue.Controls.Add(btnChangePwd);
            panelmue.Controls.Add(btnMainData);
            panelmue.FillColor = Color.FromArgb(65, 100, 204);
            panelmue.FillColor2 = Color.FromArgb(65, 100, 204);
            panelmue.FillDisableColor = Color.FromArgb(65, 100, 204);
            panelmue.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelmue.Location = new Point(6, 56);
            panelmue.Margin = new Padding(4, 5, 4, 5);
            panelmue.MinimumSize = new Size(1, 1);
            panelmue.Name = "panelmue";
            panelmue.Radius = 20;
            panelmue.RectColor = Color.FromArgb(65, 100, 204);
            panelmue.RectDisableColor = Color.FromArgb(65, 100, 204);
            panelmue.Size = new Size(113, 889);
            panelmue.TabIndex = 5;
            panelmue.Text = null;
            panelmue.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnDeviceDetection
            // 
            btnDeviceDetection.BackColor = Color.Transparent;
            btnDeviceDetection.FillColor = Color.FromArgb(65, 100, 204);
            btnDeviceDetection.FillColor2 = Color.FromArgb(65, 100, 204);
            btnDeviceDetection.FillDisableColor = Color.FromArgb(113, 143, 226);
            btnDeviceDetection.FillHoverColor = Color.FromArgb(113, 143, 226);
            btnDeviceDetection.FillPressColor = Color.FromArgb(65, 100, 204);
            btnDeviceDetection.FillSelectedColor = Color.FromArgb(113, 143, 226);
            btnDeviceDetection.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnDeviceDetection.ForeColor = Color.FromArgb(235, 227, 221);
            btnDeviceDetection.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnDeviceDetection.Image = (Image)resources.GetObject("btnDeviceDetection.Image");
            btnDeviceDetection.LightColor = Color.Transparent;
            btnDeviceDetection.Location = new Point(4, 609);
            btnDeviceDetection.MinimumSize = new Size(1, 1);
            btnDeviceDetection.Name = "btnDeviceDetection";
            btnDeviceDetection.Radius = 10;
            btnDeviceDetection.RectColor = Color.FromArgb(65, 100, 204);
            btnDeviceDetection.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnDeviceDetection.RectHoverColor = Color.FromArgb(113, 143, 226);
            btnDeviceDetection.RectPressColor = Color.FromArgb(65, 100, 204);
            btnDeviceDetection.RectSelectedColor = Color.FromArgb(113, 143, 226);
            btnDeviceDetection.Size = new Size(107, 52);
            btnDeviceDetection.Symbol = 0;
            btnDeviceDetection.SymbolDisableColor = Color.FromArgb(90, 95, 102);
            btnDeviceDetection.SymbolSize = 25;
            btnDeviceDetection.TabIndex = 125;
            btnDeviceDetection.Text = "设备检查";
            btnDeviceDetection.TipsFont = new Font("宋体", 11F);
            btnDeviceDetection.TipsForeColor = Color.Transparent;
            btnDeviceDetection.Click += btnDeviceDetection_Click;
            // 
            // btnErrStatistics
            // 
            btnErrStatistics.BackColor = Color.Transparent;
            btnErrStatistics.FillColor = Color.FromArgb(65, 100, 204);
            btnErrStatistics.FillColor2 = Color.FromArgb(65, 100, 204);
            btnErrStatistics.FillDisableColor = Color.FromArgb(113, 143, 226);
            btnErrStatistics.FillHoverColor = Color.FromArgb(113, 143, 226);
            btnErrStatistics.FillPressColor = Color.FromArgb(65, 100, 204);
            btnErrStatistics.FillSelectedColor = Color.FromArgb(113, 143, 226);
            btnErrStatistics.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnErrStatistics.ForeColor = Color.FromArgb(235, 227, 221);
            btnErrStatistics.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnErrStatistics.Image = (Image)resources.GetObject("btnErrStatistics.Image");
            btnErrStatistics.LightColor = Color.Transparent;
            btnErrStatistics.Location = new Point(4, 516);
            btnErrStatistics.MinimumSize = new Size(1, 1);
            btnErrStatistics.Name = "btnErrStatistics";
            btnErrStatistics.Radius = 10;
            btnErrStatistics.RectColor = Color.FromArgb(65, 100, 204);
            btnErrStatistics.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnErrStatistics.RectHoverColor = Color.FromArgb(113, 143, 226);
            btnErrStatistics.RectPressColor = Color.FromArgb(65, 100, 204);
            btnErrStatistics.RectSelectedColor = Color.FromArgb(113, 143, 226);
            btnErrStatistics.Size = new Size(107, 52);
            btnErrStatistics.Symbol = 0;
            btnErrStatistics.SymbolDisableColor = Color.FromArgb(90, 95, 102);
            btnErrStatistics.SymbolSize = 25;
            btnErrStatistics.TabIndex = 124;
            btnErrStatistics.Text = "问题统计";
            btnErrStatistics.TipsFont = new Font("宋体", 11F);
            btnErrStatistics.TipsForeColor = Color.Transparent;
            btnErrStatistics.Click += btnErrStatistics_Click;
            // 
            // btnMeteringRemind
            // 
            btnMeteringRemind.BackColor = Color.Transparent;
            btnMeteringRemind.FillColor = Color.FromArgb(65, 100, 204);
            btnMeteringRemind.FillColor2 = Color.FromArgb(65, 100, 204);
            btnMeteringRemind.FillDisableColor = Color.FromArgb(113, 143, 226);
            btnMeteringRemind.FillHoverColor = Color.FromArgb(113, 143, 226);
            btnMeteringRemind.FillPressColor = Color.FromArgb(65, 100, 204);
            btnMeteringRemind.FillSelectedColor = Color.FromArgb(113, 143, 226);
            btnMeteringRemind.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnMeteringRemind.ForeColor = Color.FromArgb(235, 227, 221);
            btnMeteringRemind.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnMeteringRemind.Image = (Image)resources.GetObject("btnMeteringRemind.Image");
            btnMeteringRemind.LightColor = Color.Transparent;
            btnMeteringRemind.Location = new Point(4, 423);
            btnMeteringRemind.MinimumSize = new Size(1, 1);
            btnMeteringRemind.Name = "btnMeteringRemind";
            btnMeteringRemind.Radius = 49;
            btnMeteringRemind.RectColor = Color.FromArgb(65, 100, 204);
            btnMeteringRemind.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnMeteringRemind.RectHoverColor = Color.FromArgb(113, 143, 226);
            btnMeteringRemind.RectPressColor = Color.FromArgb(65, 100, 204);
            btnMeteringRemind.RectSelectedColor = Color.FromArgb(113, 143, 226);
            btnMeteringRemind.Size = new Size(107, 52);
            btnMeteringRemind.Symbol = 0;
            btnMeteringRemind.SymbolDisableColor = Color.FromArgb(90, 95, 102);
            btnMeteringRemind.SymbolSize = 25;
            btnMeteringRemind.TabIndex = 122;
            btnMeteringRemind.Text = "维保计量";
            btnMeteringRemind.TipsFont = new Font("宋体", 11F);
            btnMeteringRemind.TipsForeColor = Color.Transparent;
            btnMeteringRemind.Click += btnMeteringRemind_Click;
            // 
            // btnNLog
            // 
            btnNLog.BackColor = Color.Transparent;
            btnNLog.FillColor = Color.FromArgb(65, 100, 204);
            btnNLog.FillColor2 = Color.FromArgb(65, 100, 204);
            btnNLog.FillDisableColor = Color.FromArgb(113, 143, 226);
            btnNLog.FillHoverColor = Color.FromArgb(113, 143, 226);
            btnNLog.FillPressColor = Color.FromArgb(65, 100, 204);
            btnNLog.FillSelectedColor = Color.FromArgb(113, 143, 226);
            btnNLog.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnNLog.ForeColor = Color.FromArgb(235, 227, 221);
            btnNLog.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnNLog.Image = (Image)resources.GetObject("btnNLog.Image");
            btnNLog.LightColor = Color.Transparent;
            btnNLog.Location = new Point(4, 330);
            btnNLog.MinimumSize = new Size(1, 1);
            btnNLog.Name = "btnNLog";
            btnNLog.Radius = 49;
            btnNLog.RectColor = Color.FromArgb(65, 100, 204);
            btnNLog.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnNLog.RectHoverColor = Color.FromArgb(113, 143, 226);
            btnNLog.RectPressColor = Color.FromArgb(65, 100, 204);
            btnNLog.RectSelectedColor = Color.FromArgb(113, 143, 226);
            btnNLog.Size = new Size(107, 52);
            btnNLog.Symbol = 0;
            btnNLog.SymbolDisableColor = Color.FromArgb(90, 95, 102);
            btnNLog.SymbolSize = 25;
            btnNLog.TabIndex = 120;
            btnNLog.Text = "日志管理";
            btnNLog.TipsFont = new Font("宋体", 11F);
            btnNLog.TipsForeColor = Color.Transparent;
            btnNLog.Click += btnNLog_Click;
            // 
            // btnHardwareTest
            // 
            btnHardwareTest.BackColor = Color.Transparent;
            btnHardwareTest.FillColor = Color.FromArgb(65, 100, 204);
            btnHardwareTest.FillColor2 = Color.FromArgb(65, 100, 204);
            btnHardwareTest.FillDisableColor = Color.FromArgb(113, 143, 226);
            btnHardwareTest.FillHoverColor = Color.FromArgb(113, 143, 226);
            btnHardwareTest.FillPressColor = Color.FromArgb(65, 100, 204);
            btnHardwareTest.FillSelectedColor = Color.FromArgb(113, 143, 226);
            btnHardwareTest.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnHardwareTest.ForeColor = Color.FromArgb(235, 227, 221);
            btnHardwareTest.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnHardwareTest.Image = (Image)resources.GetObject("btnHardwareTest.Image");
            btnHardwareTest.LightColor = Color.Transparent;
            btnHardwareTest.Location = new Point(4, 144);
            btnHardwareTest.MinimumSize = new Size(1, 1);
            btnHardwareTest.Name = "btnHardwareTest";
            btnHardwareTest.Radius = 49;
            btnHardwareTest.RectColor = Color.FromArgb(65, 100, 204);
            btnHardwareTest.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnHardwareTest.RectHoverColor = Color.FromArgb(113, 143, 226);
            btnHardwareTest.RectPressColor = Color.FromArgb(65, 100, 204);
            btnHardwareTest.RectSelectedColor = Color.FromArgb(113, 143, 226);
            btnHardwareTest.Size = new Size(107, 52);
            btnHardwareTest.Symbol = 0;
            btnHardwareTest.SymbolDisableColor = Color.FromArgb(90, 95, 102);
            btnHardwareTest.SymbolSize = 25;
            btnHardwareTest.TabIndex = 121;
            btnHardwareTest.Tag = "";
            btnHardwareTest.Text = "硬件校准";
            btnHardwareTest.TipsFont = new Font("宋体", 11F);
            btnHardwareTest.TipsForeColor = Color.Transparent;
            btnHardwareTest.Click += btnHardwareTest_Click;
            // 
            // btnReports
            // 
            btnReports.BackColor = Color.Transparent;
            btnReports.FillColor = Color.FromArgb(65, 100, 204);
            btnReports.FillColor2 = Color.FromArgb(65, 100, 204);
            btnReports.FillDisableColor = Color.FromArgb(113, 143, 226);
            btnReports.FillHoverColor = Color.FromArgb(113, 143, 226);
            btnReports.FillPressColor = Color.FromArgb(65, 100, 204);
            btnReports.FillSelectedColor = Color.FromArgb(113, 143, 226);
            btnReports.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnReports.ForeColor = Color.FromArgb(235, 227, 221);
            btnReports.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnReports.Image = (Image)resources.GetObject("btnReports.Image");
            btnReports.LightColor = Color.Transparent;
            btnReports.Location = new Point(4, 51);
            btnReports.MinimumSize = new Size(1, 1);
            btnReports.Name = "btnReports";
            btnReports.Radius = 41;
            btnReports.RectColor = Color.FromArgb(65, 100, 204);
            btnReports.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnReports.RectHoverColor = Color.FromArgb(113, 143, 226);
            btnReports.RectPressColor = Color.FromArgb(65, 100, 204);
            btnReports.RectSelectedColor = Color.FromArgb(113, 143, 226);
            btnReports.Size = new Size(107, 52);
            btnReports.Symbol = 0;
            btnReports.SymbolDisableColor = Color.FromArgb(90, 95, 102);
            btnReports.SymbolSize = 50;
            btnReports.TabIndex = 120;
            btnReports.Tag = "";
            btnReports.Text = "数据查询";
            btnReports.TipsFont = new Font("宋体", 12F);
            btnReports.TipsForeColor = Color.Transparent;
            btnReports.Click += btnReports_Click;
            // 
            // btnMainData
            // 
            btnMainData.BackColor = Color.Transparent;
            btnMainData.FillColor = Color.FromArgb(65, 100, 204);
            btnMainData.FillColor2 = Color.FromArgb(65, 100, 204);
            btnMainData.FillDisableColor = Color.FromArgb(113, 143, 226);
            btnMainData.FillHoverColor = Color.FromArgb(113, 143, 226);
            btnMainData.FillPressColor = Color.FromArgb(65, 100, 204);
            btnMainData.FillSelectedColor = Color.FromArgb(113, 143, 226);
            btnMainData.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnMainData.ForeColor = Color.FromArgb(235, 227, 221);
            btnMainData.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnMainData.Image = (Image)resources.GetObject("btnMainData.Image");
            btnMainData.LightColor = Color.Transparent;
            btnMainData.Location = new Point(4, 237);
            btnMainData.MinimumSize = new Size(1, 1);
            btnMainData.Name = "btnMainData";
            btnMainData.Radius = 49;
            btnMainData.RectColor = Color.FromArgb(65, 100, 204);
            btnMainData.RectDisableColor = Color.FromArgb(65, 100, 204);
            btnMainData.RectHoverColor = Color.FromArgb(113, 143, 226);
            btnMainData.RectPressColor = Color.FromArgb(65, 100, 204);
            btnMainData.RectSelectedColor = Color.FromArgb(113, 143, 226);
            btnMainData.Size = new Size(107, 52);
            btnMainData.Symbol = 0;
            btnMainData.SymbolDisableColor = Color.FromArgb(90, 95, 102);
            btnMainData.SymbolSize = 25;
            btnMainData.TabIndex = 119;
            btnMainData.Text = "参数管理";
            btnMainData.TipsFont = new Font("宋体", 11F);
            btnMainData.TipsForeColor = Color.Transparent;
            btnMainData.Click += BtnMainData_Click;
            // 
            // PanelHmi
            // 
            PanelHmi.FillColor = Color.FromArgb(236, 236, 237);
            PanelHmi.FillColor2 = Color.FromArgb(236, 236, 237);
            PanelHmi.FillDisableColor = Color.FromArgb(236, 236, 237);
            PanelHmi.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            PanelHmi.Location = new Point(125, 56);
            PanelHmi.Margin = new Padding(4, 5, 4, 5);
            PanelHmi.MinimumSize = new Size(1, 1);
            PanelHmi.Name = "PanelHmi";
            PanelHmi.RectColor = Color.FromArgb(236, 236, 237);
            PanelHmi.RectDisableColor = Color.FromArgb(236, 236, 237);
            PanelHmi.Size = new Size(1155, 889);
            PanelHmi.TabIndex = 6;
            PanelHmi.Text = null;
            PanelHmi.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.BackColor = Color.FromArgb(65, 100, 204);
            label5.Dock = DockStyle.Bottom;
            label5.Location = new Point(0, 950);
            label5.Name = "label5";
            label5.Size = new Size(1280, 4);
            label5.TabIndex = 496;
            label5.Text = "";
            // 
            // frmMainMenu
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 237);
            ClientSize = new Size(1280, 976);
            ControlBox = false;
            Controls.Add(label5);
            Controls.Add(PanelHmi);
            Controls.Add(panelmue);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += frmMainMenu_FormClosing;
            panel1.ResumeLayout(false);
            lblTitle.ResumeLayout(false);
            lblTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picRunStatus).EndInit();
            uiPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panelmue.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picRunStatus;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer timerPLC;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblUser;
        private System.Windows.Forms.ToolStripStatusLabel tslblPLC;
        private System.Windows.Forms.Timer timerHeartbeat;
        private UIPanel uiPanel2;
        public PictureBox Logo;
        private UIPanel lblTitle;
        private UISymbolButton btnExit;
        private UISymbolButton btnChangePwd;
        private UIPanel panelmue;
        private UIPanel PanelHmi;
        private UISymbolButton btnMainData;
        private AntdUI.Label label5;
        private UISymbolButton btnReports;
        private UISymbolButton btnHardwareTest;
        private UISymbolButton btnNLog;
        private UISymbolButton btnMeteringRemind;
        private UISymbolButton btnErrStatistics;
        private UISymbolButton btnDeviceDetection;
        private ToolStripStatusLabel tslblWorkStation;
    }
}
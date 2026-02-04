namespace MainUI.LogicalConfiguration
{
    partial class FrmLogicalConfiguration
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
            if (disposing && (components != null) && _workflowState != null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogicalConfiguration));
            splitContainerMain = new SplitContainer();
            panelToolBox = new UIPanel();
            pnlToolsTop = new UIPanel();
            splitContainerRight = new SplitContainer();
            panelProcess = new UIPanel();
            palProcessTop = new UIPanel();
            splitContainerBottom = new SplitContainer();
            palStepDetails = new UIPanel();
            tblStepDetails = new TableLayoutPanel();
            lblStepNumberTitle = new Label();
            lblStepNumber = new Label();
            lblStepNameTitle = new Label();
            lblStepName = new Label();
            lblStepTypeTitle = new Label();
            lblStepType = new Label();
            lblStepStatusTitle = new Label();
            lblStepStatus = new Label();
            lblExecutionTimeTitle = new Label();
            lblExecutionTime = new Label();
            palStepDetailsTop = new UIPanel();
            palExecutionLog = new UIPanel();
            txtLog = new TextBox();
            palExecutionLogTop = new UIPanel();
            imageList1 = new ImageList(components);
            pnlButtons = new UIPanel();
            BtnSystemParams = new UISymbolButton();
            BtnVariableMonitor = new UISymbolButton();
            BtnPointDefine = new UISymbolButton();
            btnVariableDefine = new UISymbolButton();
            btnClose = new UISymbolButton();
            btnExecute = new UISymbolButton();
            btnSave = new UISymbolButton();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerRight).BeginInit();
            splitContainerRight.Panel1.SuspendLayout();
            splitContainerRight.Panel2.SuspendLayout();
            splitContainerRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerBottom).BeginInit();
            splitContainerBottom.Panel1.SuspendLayout();
            splitContainerBottom.Panel2.SuspendLayout();
            splitContainerBottom.SuspendLayout();
            palStepDetails.SuspendLayout();
            tblStepDetails.SuspendLayout();
            palExecutionLog.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.BackColor = Color.FromArgb(215, 215, 215);
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.FixedPanel = FixedPanel.Panel1;
            splitContainerMain.Location = new Point(0, 35);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.BackColor = Color.FromArgb(248, 249, 250);
            splitContainerMain.Panel1.Controls.Add(panelToolBox);
            splitContainerMain.Panel1.Controls.Add(pnlToolsTop);
            splitContainerMain.Panel1MinSize = 240;
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.BackColor = Color.White;
            splitContainerMain.Panel2.Controls.Add(splitContainerRight);
            splitContainerMain.Size = new Size(1280, 887);
            splitContainerMain.SplitterDistance = 280;
            splitContainerMain.SplitterWidth = 1;
            splitContainerMain.TabIndex = 0;
            // 
            // panelToolBox
            // 
            panelToolBox.BackColor = Color.FromArgb(248, 249, 250);
            panelToolBox.Dock = DockStyle.Fill;
            panelToolBox.FillColor = Color.FromArgb(248, 249, 250);
            panelToolBox.FillColor2 = Color.FromArgb(248, 249, 250);
            panelToolBox.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            panelToolBox.Location = new Point(0, 40);
            panelToolBox.Margin = new Padding(4, 5, 4, 5);
            panelToolBox.MinimumSize = new Size(1, 1);
            panelToolBox.Name = "panelToolBox";
            panelToolBox.Padding = new Padding(8);
            panelToolBox.RectColor = Color.FromArgb(248, 249, 250);
            panelToolBox.RectDisableColor = Color.FromArgb(248, 249, 250);
            panelToolBox.Size = new Size(280, 847);
            panelToolBox.TabIndex = 0;
            panelToolBox.TabStop = false;
            panelToolBox.Text = "工具组件库";
            panelToolBox.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pnlToolsTop
            // 
            pnlToolsTop.BackColor = Color.FromArgb(248, 249, 250);
            pnlToolsTop.Dock = DockStyle.Top;
            pnlToolsTop.FillColor = Color.White;
            pnlToolsTop.FillColor2 = Color.White;
            pnlToolsTop.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            pnlToolsTop.ForeColor = Color.FromArgb(65, 100, 204);
            pnlToolsTop.Location = new Point(0, 0);
            pnlToolsTop.Margin = new Padding(4, 5, 4, 5);
            pnlToolsTop.MinimumSize = new Size(1, 1);
            pnlToolsTop.Name = "pnlToolsTop";
            pnlToolsTop.Radius = 0;
            pnlToolsTop.RectColor = Color.FromArgb(233, 236, 239);
            pnlToolsTop.RectDisableColor = Color.FromArgb(233, 236, 239);
            pnlToolsTop.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            pnlToolsTop.Size = new Size(280, 40);
            pnlToolsTop.TabIndex = 1;
            pnlToolsTop.Text = " 工具组件库";
            pnlToolsTop.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // splitContainerRight
            // 
            splitContainerRight.BackColor = Color.FromArgb(200, 200, 200);
            splitContainerRight.Dock = DockStyle.Fill;
            splitContainerRight.Location = new Point(0, 0);
            splitContainerRight.Name = "splitContainerRight";
            splitContainerRight.Orientation = Orientation.Horizontal;
            // 
            // splitContainerRight.Panel1
            // 
            splitContainerRight.Panel1.BackColor = Color.FromArgb(248, 249, 250);
            splitContainerRight.Panel1.Controls.Add(panelProcess);
            splitContainerRight.Panel1.Controls.Add(palProcessTop);
            // 
            // splitContainerRight.Panel2
            // 
            splitContainerRight.Panel2.Controls.Add(splitContainerBottom);
            splitContainerRight.Size = new Size(999, 887);
            splitContainerRight.SplitterDistance = 644;
            splitContainerRight.SplitterWidth = 1;
            splitContainerRight.TabIndex = 1;
            // 
            // panelProcess
            // 
            panelProcess.BackColor = Color.White;
            panelProcess.Dock = DockStyle.Fill;
            panelProcess.FillColor = Color.White;
            panelProcess.FillColor2 = Color.White;
            panelProcess.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            panelProcess.Location = new Point(0, 40);
            panelProcess.Margin = new Padding(4, 5, 4, 5);
            panelProcess.MinimumSize = new Size(1, 1);
            panelProcess.Name = "panelProcess";
            panelProcess.Padding = new Padding(8);
            panelProcess.RectColor = Color.White;
            panelProcess.RectDisableColor = Color.White;
            panelProcess.Size = new Size(999, 604);
            panelProcess.TabIndex = 0;
            panelProcess.TabStop = false;
            panelProcess.Text = "流程配置 - 拖拽组件到此处构建逻辑流程";
            panelProcess.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // palProcessTop
            // 
            palProcessTop.BackColor = Color.FromArgb(248, 249, 250);
            palProcessTop.Dock = DockStyle.Top;
            palProcessTop.FillColor = Color.White;
            palProcessTop.FillColor2 = Color.White;
            palProcessTop.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            palProcessTop.ForeColor = Color.FromArgb(65, 100, 204);
            palProcessTop.Location = new Point(0, 0);
            palProcessTop.Margin = new Padding(4, 5, 4, 5);
            palProcessTop.MinimumSize = new Size(1, 1);
            palProcessTop.Name = "palProcessTop";
            palProcessTop.Radius = 0;
            palProcessTop.RectColor = Color.FromArgb(233, 236, 239);
            palProcessTop.RectDisableColor = Color.FromArgb(233, 236, 239);
            palProcessTop.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            palProcessTop.Size = new Size(999, 40);
            palProcessTop.TabIndex = 1;
            palProcessTop.Text = " 流程配置 - 拖拽组件到此处构建逻辑流程";
            palProcessTop.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // splitContainerBottom
            // 
            splitContainerBottom.BackColor = Color.FromArgb(200, 200, 200);
            splitContainerBottom.Dock = DockStyle.Fill;
            splitContainerBottom.Location = new Point(0, 0);
            splitContainerBottom.Name = "splitContainerBottom";
            // 
            // splitContainerBottom.Panel1
            // 
            splitContainerBottom.Panel1.Controls.Add(palStepDetails);
            splitContainerBottom.Panel1.Controls.Add(palStepDetailsTop);
            // 
            // splitContainerBottom.Panel2
            // 
            splitContainerBottom.Panel2.Controls.Add(palExecutionLog);
            splitContainerBottom.Panel2.Controls.Add(palExecutionLogTop);
            splitContainerBottom.Size = new Size(999, 242);
            splitContainerBottom.SplitterDistance = 377;
            splitContainerBottom.SplitterWidth = 1;
            splitContainerBottom.TabIndex = 0;
            // 
            // palStepDetails
            // 
            palStepDetails.BackColor = Color.FromArgb(250, 251, 252);
            palStepDetails.Controls.Add(tblStepDetails);
            palStepDetails.Dock = DockStyle.Fill;
            palStepDetails.FillColor = Color.FromArgb(250, 251, 252);
            palStepDetails.FillColor2 = Color.FromArgb(250, 251, 252);
            palStepDetails.Font = new Font("微软雅黑", 12F);
            palStepDetails.Location = new Point(0, 35);
            palStepDetails.Margin = new Padding(4, 5, 4, 5);
            palStepDetails.MinimumSize = new Size(1, 1);
            palStepDetails.Name = "palStepDetails";
            palStepDetails.Padding = new Padding(8);
            palStepDetails.RectColor = Color.FromArgb(250, 251, 252);
            palStepDetails.RectDisableColor = Color.FromArgb(250, 251, 252);
            palStepDetails.Size = new Size(377, 207);
            palStepDetails.TabIndex = 0;
            palStepDetails.TabStop = false;
            palStepDetails.Text = "步骤详情信息";
            palStepDetails.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // tblStepDetails
            // 
            tblStepDetails.BackColor = Color.White;
            tblStepDetails.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tblStepDetails.ColumnCount = 2;
            tblStepDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tblStepDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblStepDetails.Controls.Add(lblStepNumberTitle, 0, 0);
            tblStepDetails.Controls.Add(lblStepNumber, 1, 0);
            tblStepDetails.Controls.Add(lblStepNameTitle, 0, 1);
            tblStepDetails.Controls.Add(lblStepName, 1, 1);
            tblStepDetails.Controls.Add(lblStepTypeTitle, 0, 2);
            tblStepDetails.Controls.Add(lblStepType, 1, 2);
            tblStepDetails.Controls.Add(lblStepStatusTitle, 0, 3);
            tblStepDetails.Controls.Add(lblStepStatus, 1, 3);
            tblStepDetails.Controls.Add(lblExecutionTimeTitle, 0, 4);
            tblStepDetails.Controls.Add(lblExecutionTime, 1, 4);
            tblStepDetails.Dock = DockStyle.Fill;
            tblStepDetails.Font = new Font("微软雅黑", 12F);
            tblStepDetails.Location = new Point(8, 8);
            tblStepDetails.Name = "tblStepDetails";
            tblStepDetails.RowCount = 5;
            tblStepDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblStepDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblStepDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblStepDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblStepDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblStepDetails.Size = new Size(361, 191);
            tblStepDetails.TabIndex = 0;
            // 
            // lblStepNumberTitle
            // 
            lblStepNumberTitle.AutoSize = true;
            lblStepNumberTitle.BackColor = Color.FromArgb(240, 242, 245);
            lblStepNumberTitle.Dock = DockStyle.Fill;
            lblStepNumberTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblStepNumberTitle.Location = new Point(4, 1);
            lblStepNumberTitle.Name = "lblStepNumberTitle";
            lblStepNumberTitle.Size = new Size(94, 35);
            lblStepNumberTitle.TabIndex = 0;
            lblStepNumberTitle.Text = "步骤编号:";
            lblStepNumberTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStepNumber
            // 
            lblStepNumber.Anchor = AnchorStyles.Left;
            lblStepNumber.AutoSize = true;
            lblStepNumber.Location = new Point(105, 8);
            lblStepNumber.Name = "lblStepNumber";
            lblStepNumber.Size = new Size(19, 21);
            lblStepNumber.TabIndex = 1;
            lblStepNumber.Text = "1";
            // 
            // lblStepNameTitle
            // 
            lblStepNameTitle.AutoSize = true;
            lblStepNameTitle.BackColor = Color.FromArgb(240, 242, 245);
            lblStepNameTitle.Dock = DockStyle.Fill;
            lblStepNameTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblStepNameTitle.Location = new Point(4, 37);
            lblStepNameTitle.Name = "lblStepNameTitle";
            lblStepNameTitle.Size = new Size(94, 35);
            lblStepNameTitle.TabIndex = 2;
            lblStepNameTitle.Text = "操作名称:";
            lblStepNameTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStepName
            // 
            lblStepName.Anchor = AnchorStyles.Left;
            lblStepName.AutoSize = true;
            lblStepName.Location = new Point(105, 44);
            lblStepName.Name = "lblStepName";
            lblStepName.Size = new Size(90, 21);
            lblStepName.TabIndex = 3;
            lblStepName.Text = "初始化变量";
            // 
            // lblStepTypeTitle
            // 
            lblStepTypeTitle.AutoSize = true;
            lblStepTypeTitle.BackColor = Color.FromArgb(240, 242, 245);
            lblStepTypeTitle.Dock = DockStyle.Fill;
            lblStepTypeTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblStepTypeTitle.Location = new Point(4, 73);
            lblStepTypeTitle.Name = "lblStepTypeTitle";
            lblStepTypeTitle.Size = new Size(94, 35);
            lblStepTypeTitle.TabIndex = 4;
            lblStepTypeTitle.Text = "操作类型:";
            lblStepTypeTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStepType
            // 
            lblStepType.Anchor = AnchorStyles.Left;
            lblStepType.AutoSize = true;
            lblStepType.Location = new Point(105, 80);
            lblStepType.Name = "lblStepType";
            lblStepType.Size = new Size(74, 21);
            lblStepType.TabIndex = 5;
            lblStepType.Text = "变量赋值";
            // 
            // lblStepStatusTitle
            // 
            lblStepStatusTitle.AutoSize = true;
            lblStepStatusTitle.BackColor = Color.FromArgb(240, 242, 245);
            lblStepStatusTitle.Dock = DockStyle.Fill;
            lblStepStatusTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblStepStatusTitle.Location = new Point(4, 109);
            lblStepStatusTitle.Name = "lblStepStatusTitle";
            lblStepStatusTitle.Size = new Size(94, 35);
            lblStepStatusTitle.TabIndex = 6;
            lblStepStatusTitle.Text = "执行状态:";
            lblStepStatusTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStepStatus
            // 
            lblStepStatus.Anchor = AnchorStyles.Left;
            lblStepStatus.AutoSize = true;
            lblStepStatus.Location = new Point(105, 116);
            lblStepStatus.Name = "lblStepStatus";
            lblStepStatus.Size = new Size(59, 21);
            lblStepStatus.TabIndex = 7;
            lblStepStatus.Text = "✓ 完成";
            // 
            // lblExecutionTimeTitle
            // 
            lblExecutionTimeTitle.AutoSize = true;
            lblExecutionTimeTitle.BackColor = Color.FromArgb(240, 242, 245);
            lblExecutionTimeTitle.Dock = DockStyle.Fill;
            lblExecutionTimeTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            lblExecutionTimeTitle.Location = new Point(4, 145);
            lblExecutionTimeTitle.Name = "lblExecutionTimeTitle";
            lblExecutionTimeTitle.Size = new Size(94, 45);
            lblExecutionTimeTitle.TabIndex = 8;
            lblExecutionTimeTitle.Text = "执行时间:";
            lblExecutionTimeTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblExecutionTime
            // 
            lblExecutionTime.Anchor = AnchorStyles.Left;
            lblExecutionTime.AutoSize = true;
            lblExecutionTime.Location = new Point(105, 157);
            lblExecutionTime.Name = "lblExecutionTime";
            lblExecutionTime.Size = new Size(48, 21);
            lblExecutionTime.TabIndex = 9;
            lblExecutionTime.Text = "0.05s";
            // 
            // palStepDetailsTop
            // 
            palStepDetailsTop.BackColor = Color.FromArgb(248, 249, 250);
            palStepDetailsTop.Dock = DockStyle.Top;
            palStepDetailsTop.FillColor = Color.White;
            palStepDetailsTop.FillColor2 = Color.White;
            palStepDetailsTop.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            palStepDetailsTop.ForeColor = Color.FromArgb(65, 100, 204);
            palStepDetailsTop.Location = new Point(0, 0);
            palStepDetailsTop.Margin = new Padding(4, 5, 4, 5);
            palStepDetailsTop.MinimumSize = new Size(1, 1);
            palStepDetailsTop.Name = "palStepDetailsTop";
            palStepDetailsTop.Radius = 0;
            palStepDetailsTop.RectColor = Color.FromArgb(233, 236, 239);
            palStepDetailsTop.RectDisableColor = Color.FromArgb(233, 236, 239);
            palStepDetailsTop.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            palStepDetailsTop.Size = new Size(377, 35);
            palStepDetailsTop.TabIndex = 1;
            palStepDetailsTop.Text = " 步骤详情信息";
            palStepDetailsTop.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // palExecutionLog
            // 
            palExecutionLog.BackColor = Color.White;
            palExecutionLog.Controls.Add(txtLog);
            palExecutionLog.Dock = DockStyle.Fill;
            palExecutionLog.FillColor = Color.White;
            palExecutionLog.FillColor2 = Color.White;
            palExecutionLog.Font = new Font("微软雅黑", 12F);
            palExecutionLog.Location = new Point(0, 35);
            palExecutionLog.Margin = new Padding(4, 5, 4, 5);
            palExecutionLog.MinimumSize = new Size(1, 1);
            palExecutionLog.Name = "palExecutionLog";
            palExecutionLog.Padding = new Padding(8);
            palExecutionLog.RectColor = Color.FromArgb(250, 251, 252);
            palExecutionLog.RectDisableColor = Color.FromArgb(250, 251, 252);
            palExecutionLog.Size = new Size(621, 207);
            palExecutionLog.TabIndex = 0;
            palExecutionLog.TabStop = false;
            palExecutionLog.Text = "执行日志";
            palExecutionLog.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.FromArgb(250, 251, 252);
            txtLog.BorderStyle = BorderStyle.FixedSingle;
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("微软雅黑", 12F);
            txtLog.ForeColor = Color.FromArgb(46, 46, 46);
            txtLog.Location = new Point(8, 8);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(605, 191);
            txtLog.TabIndex = 0;
            // 
            // palExecutionLogTop
            // 
            palExecutionLogTop.BackColor = Color.FromArgb(248, 249, 250);
            palExecutionLogTop.Dock = DockStyle.Top;
            palExecutionLogTop.FillColor = Color.White;
            palExecutionLogTop.FillColor2 = Color.White;
            palExecutionLogTop.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            palExecutionLogTop.ForeColor = Color.FromArgb(65, 100, 204);
            palExecutionLogTop.Location = new Point(0, 0);
            palExecutionLogTop.Margin = new Padding(4, 5, 4, 5);
            palExecutionLogTop.MinimumSize = new Size(1, 1);
            palExecutionLogTop.Name = "palExecutionLogTop";
            palExecutionLogTop.Radius = 0;
            palExecutionLogTop.RectColor = Color.FromArgb(233, 236, 239);
            palExecutionLogTop.RectDisableColor = Color.FromArgb(233, 236, 239);
            palExecutionLogTop.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            palExecutionLogTop.Size = new Size(621, 35);
            palExecutionLogTop.TabIndex = 1;
            palExecutionLogTop.Text = " 执行日志";
            palExecutionLogTop.TextAlignment = ContentAlignment.MiddleLeft;
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
            imageList1.Images.SetKeyName(7, "循环开始.png");
            imageList1.Images.SetKeyName(8, "循环结束.png");
            imageList1.Images.SetKeyName(9, "报表读取.png");
            imageList1.Images.SetKeyName(10, "报表写入.png");
            imageList1.Images.SetKeyName(11, "读取PLC.png");
            imageList1.Images.SetKeyName(12, "写入PLC.png");
            imageList1.Images.SetKeyName(13, "等待稳定.png");
            imageList1.Images.SetKeyName(14, "检测工具.png");
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.White;
            pnlButtons.Controls.Add(BtnSystemParams);
            pnlButtons.Controls.Add(BtnVariableMonitor);
            pnlButtons.Controls.Add(BtnPointDefine);
            pnlButtons.Controls.Add(btnVariableDefine);
            pnlButtons.Controls.Add(btnClose);
            pnlButtons.Controls.Add(btnExecute);
            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.FillColor = Color.FromArgb(248, 249, 250);
            pnlButtons.FillColor2 = Color.FromArgb(248, 249, 250);
            pnlButtons.Font = new Font("微软雅黑", 12F);
            pnlButtons.Location = new Point(0, 922);
            pnlButtons.Margin = new Padding(4, 5, 4, 5);
            pnlButtons.MinimumSize = new Size(1, 1);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.RectColor = Color.FromArgb(233, 236, 239);
            pnlButtons.RectDisableColor = Color.FromArgb(233, 236, 239);
            pnlButtons.Size = new Size(1280, 54);
            pnlButtons.TabIndex = 0;
            pnlButtons.Text = null;
            pnlButtons.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // BtnSystemParams
            // 
            BtnSystemParams.Cursor = Cursors.Hand;
            BtnSystemParams.FillColor = Color.FromArgb(55, 71, 79);
            BtnSystemParams.FillColor2 = Color.FromArgb(55, 71, 79);
            BtnSystemParams.FillColorGradient = true;
            BtnSystemParams.FillColorGradientDirection = FlowDirection.LeftToRight;
            BtnSystemParams.FillDisableColor = Color.FromArgb(149, 154, 164);
            BtnSystemParams.FillHoverColor = Color.FromArgb(149, 154, 164);
            BtnSystemParams.FillPressColor = Color.FromArgb(149, 154, 164);
            BtnSystemParams.FillSelectedColor = Color.FromArgb(149, 154, 164);
            BtnSystemParams.Font = new Font("微软雅黑", 12F);
            BtnSystemParams.Image = (Image)resources.GetObject("BtnSystemParams.Image");
            BtnSystemParams.Location = new Point(373, 10);
            BtnSystemParams.MinimumSize = new Size(1, 1);
            BtnSystemParams.Name = "BtnSystemParams";
            BtnSystemParams.RectColor = Color.FromArgb(55, 71, 79);
            BtnSystemParams.RectDisableColor = Color.FromArgb(55, 71, 79);
            BtnSystemParams.RectHoverColor = Color.FromArgb(149, 154, 164);
            BtnSystemParams.RectPressColor = Color.FromArgb(149, 154, 164);
            BtnSystemParams.RectSelectedColor = Color.FromArgb(149, 154, 164);
            BtnSystemParams.Size = new Size(110, 35);
            BtnSystemParams.Symbol = 0;
            BtnSystemParams.TabIndex = 6;
            BtnSystemParams.Text = "系统参数";
            BtnSystemParams.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // BtnVariableMonitor
            // 
            BtnVariableMonitor.Cursor = Cursors.Hand;
            BtnVariableMonitor.FillColor = Color.FromArgb(55, 71, 79);
            BtnVariableMonitor.FillColor2 = Color.FromArgb(55, 71, 79);
            BtnVariableMonitor.FillColorGradient = true;
            BtnVariableMonitor.FillColorGradientDirection = FlowDirection.LeftToRight;
            BtnVariableMonitor.FillDisableColor = Color.FromArgb(149, 154, 164);
            BtnVariableMonitor.FillHoverColor = Color.FromArgb(149, 154, 164);
            BtnVariableMonitor.FillPressColor = Color.FromArgb(149, 154, 164);
            BtnVariableMonitor.FillSelectedColor = Color.FromArgb(149, 154, 164);
            BtnVariableMonitor.Font = new Font("微软雅黑", 12F);
            BtnVariableMonitor.Image = (Image)resources.GetObject("BtnVariableMonitor.Image");
            BtnVariableMonitor.Location = new Point(129, 10);
            BtnVariableMonitor.MinimumSize = new Size(1, 1);
            BtnVariableMonitor.Name = "BtnVariableMonitor";
            BtnVariableMonitor.RectColor = Color.FromArgb(55, 71, 79);
            BtnVariableMonitor.RectDisableColor = Color.FromArgb(55, 71, 79);
            BtnVariableMonitor.RectHoverColor = Color.FromArgb(149, 154, 164);
            BtnVariableMonitor.RectPressColor = Color.FromArgb(149, 154, 164);
            BtnVariableMonitor.RectSelectedColor = Color.FromArgb(149, 154, 164);
            BtnVariableMonitor.Size = new Size(110, 35);
            BtnVariableMonitor.Symbol = 0;
            BtnVariableMonitor.TabIndex = 5;
            BtnVariableMonitor.Text = "变量监控";
            BtnVariableMonitor.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // BtnPointDefine
            // 
            BtnPointDefine.Cursor = Cursors.Hand;
            BtnPointDefine.FillColor = Color.FromArgb(55, 71, 79);
            BtnPointDefine.FillColor2 = Color.FromArgb(55, 71, 79);
            BtnPointDefine.FillColorGradient = true;
            BtnPointDefine.FillColorGradientDirection = FlowDirection.LeftToRight;
            BtnPointDefine.FillDisableColor = Color.FromArgb(149, 154, 164);
            BtnPointDefine.FillHoverColor = Color.FromArgb(149, 154, 164);
            BtnPointDefine.FillPressColor = Color.FromArgb(149, 154, 164);
            BtnPointDefine.FillSelectedColor = Color.FromArgb(149, 154, 164);
            BtnPointDefine.Font = new Font("微软雅黑", 12F);
            BtnPointDefine.Image = (Image)resources.GetObject("BtnPointDefine.Image");
            BtnPointDefine.Location = new Point(251, 10);
            BtnPointDefine.MinimumSize = new Size(1, 1);
            BtnPointDefine.Name = "BtnPointDefine";
            BtnPointDefine.RectColor = Color.FromArgb(55, 71, 79);
            BtnPointDefine.RectDisableColor = Color.FromArgb(55, 71, 79);
            BtnPointDefine.RectHoverColor = Color.FromArgb(149, 154, 164);
            BtnPointDefine.RectPressColor = Color.FromArgb(149, 154, 164);
            BtnPointDefine.RectSelectedColor = Color.FromArgb(149, 154, 164);
            BtnPointDefine.Size = new Size(110, 35);
            BtnPointDefine.Symbol = 0;
            BtnPointDefine.TabIndex = 4;
            BtnPointDefine.Text = "点位定义";
            BtnPointDefine.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnVariableDefine
            // 
            btnVariableDefine.Cursor = Cursors.Hand;
            btnVariableDefine.FillColor = Color.FromArgb(69, 90, 100);
            btnVariableDefine.FillColor2 = Color.FromArgb(55, 71, 79);
            btnVariableDefine.FillColorGradient = true;
            btnVariableDefine.FillColorGradientDirection = FlowDirection.LeftToRight;
            btnVariableDefine.FillDisableColor = Color.FromArgb(149, 154, 164);
            btnVariableDefine.FillHoverColor = Color.FromArgb(149, 154, 164);
            btnVariableDefine.FillPressColor = Color.FromArgb(149, 154, 164);
            btnVariableDefine.FillSelectedColor = Color.FromArgb(149, 154, 164);
            btnVariableDefine.Font = new Font("微软雅黑", 12F);
            btnVariableDefine.ForeDisableColor = Color.White;
            btnVariableDefine.Image = (Image)resources.GetObject("btnVariableDefine.Image");
            btnVariableDefine.Location = new Point(7, 10);
            btnVariableDefine.MinimumSize = new Size(1, 1);
            btnVariableDefine.Name = "btnVariableDefine";
            btnVariableDefine.RectColor = Color.FromArgb(55, 71, 79);
            btnVariableDefine.RectDisableColor = Color.FromArgb(55, 71, 79);
            btnVariableDefine.RectHoverColor = Color.FromArgb(149, 154, 164);
            btnVariableDefine.RectPressColor = Color.FromArgb(149, 154, 164);
            btnVariableDefine.RectSelectedColor = Color.FromArgb(149, 154, 164);
            btnVariableDefine.Size = new Size(110, 35);
            btnVariableDefine.Symbol = 0;
            btnVariableDefine.TabIndex = 3;
            btnVariableDefine.Text = "变量定义";
            btnVariableDefine.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Transparent;
            btnClose.FillColor = Color.FromArgb(239, 68, 68);
            btnClose.FillColor2 = Color.FromArgb(220, 38, 38);
            btnClose.FillColorGradient = true;
            btnClose.FillColorGradientDirection = FlowDirection.LeftToRight;
            btnClose.FillDisableColor = Color.FromArgb(239, 68, 68);
            btnClose.FillHoverColor = Color.FromArgb(242, 122, 122);
            btnClose.FillPressColor = Color.FromArgb(242, 122, 122);
            btnClose.FillSelectedColor = Color.FromArgb(242, 122, 122);
            btnClose.Font = new Font("微软雅黑", 12F);
            btnClose.Image = (Image)resources.GetObject("btnClose.Image");
            btnClose.Location = new Point(1164, 9);
            btnClose.MinimumSize = new Size(1, 1);
            btnClose.Name = "btnClose";
            btnClose.Radius = 10;
            btnClose.RectColor = Color.FromArgb(239, 68, 68);
            btnClose.RectDisableColor = Color.FromArgb(239, 68, 68);
            btnClose.RectHoverColor = Color.FromArgb(242, 122, 122);
            btnClose.RectPressColor = Color.FromArgb(242, 122, 122);
            btnClose.RectSelectedColor = Color.FromArgb(242, 122, 122);
            btnClose.Size = new Size(110, 37);
            btnClose.TabIndex = 2;
            btnClose.Text = "关闭";
            btnClose.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // btnExecute
            // 
            btnExecute.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExecute.BackColor = Color.Transparent;
            btnExecute.FillColor = Color.FromArgb(59, 130, 246);
            btnExecute.FillColor2 = Color.FromArgb(29, 78, 216);
            btnExecute.FillColorGradient = true;
            btnExecute.FillColorGradientDirection = FlowDirection.LeftToRight;
            btnExecute.Font = new Font("微软雅黑", 12F);
            btnExecute.Image = (Image)resources.GetObject("btnExecute.Image");
            btnExecute.Location = new Point(919, 10);
            btnExecute.MinimumSize = new Size(1, 1);
            btnExecute.Name = "btnExecute";
            btnExecute.Radius = 10;
            btnExecute.Size = new Size(110, 37);
            btnExecute.Style = UIStyle.Custom;
            btnExecute.Symbol = 0;
            btnExecute.TabIndex = 1;
            btnExecute.Text = "执行流程";
            btnExecute.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnExecute.Visible = false;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.BackColor = Color.Transparent;
            btnSave.FillColor = Color.FromArgb(16, 185, 129);
            btnSave.FillColor2 = Color.FromArgb(5, 150, 105);
            btnSave.FillColorGradient = true;
            btnSave.FillColorGradientDirection = FlowDirection.LeftToRight;
            btnSave.FillDisableColor = Color.FromArgb(16, 185, 129);
            btnSave.FillHoverColor = Color.FromArgb(87, 203, 164);
            btnSave.FillPressColor = Color.FromArgb(87, 203, 164);
            btnSave.FillSelectedColor = Color.FromArgb(87, 203, 164);
            btnSave.Font = new Font("微软雅黑", 12F);
            btnSave.Image = (Image)resources.GetObject("btnSave.Image");
            btnSave.Location = new Point(1045, 10);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.Radius = 10;
            btnSave.RectColor = Color.FromArgb(40, 167, 69);
            btnSave.RectDisableColor = Color.FromArgb(40, 167, 69);
            btnSave.RectHoverColor = Color.FromArgb(87, 203, 164);
            btnSave.RectPressColor = Color.FromArgb(87, 203, 164);
            btnSave.RectSelectedColor = Color.FromArgb(87, 203, 164);
            btnSave.Size = new Size(110, 37);
            btnSave.TabIndex = 0;
            btnSave.Text = "保存配置";
            btnSave.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // FrmLogicalConfiguration
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 237);
            ClientSize = new Size(1280, 976);
            ControlBox = false;
            Controls.Add(splitContainerMain);
            Controls.Add(pnlButtons);
            Font = new Font("微软雅黑", 13F);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(1000, 600);
            Name = "FrmLogicalConfiguration";
            RectColor = Color.Transparent;
            ShowIcon = false;
            Text = "试验逻辑配置";
            TitleColor = Color.FromArgb(81, 114, 221);
            TitleFont = new Font("微软雅黑", 14F, FontStyle.Bold);
            WindowState = FormWindowState.Maximized;
            ZoomScaleRect = new Rectangle(15, 15, 1200, 700);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            splitContainerRight.Panel1.ResumeLayout(false);
            splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerRight).EndInit();
            splitContainerRight.ResumeLayout(false);
            splitContainerBottom.Panel1.ResumeLayout(false);
            splitContainerBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerBottom).EndInit();
            splitContainerBottom.ResumeLayout(false);
            palStepDetails.ResumeLayout(false);
            tblStepDetails.ResumeLayout(false);
            tblStepDetails.PerformLayout();
            palExecutionLog.ResumeLayout(false);
            palExecutionLog.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private Sunny.UI.UIPanel panelToolBox;
        private Sunny.UI.UIPanel pnlToolsTop;
        private System.Windows.Forms.SplitContainer splitContainerRight;
        private Sunny.UI.UIPanel panelProcess;
        private Sunny.UI.UIPanel palProcessTop;
        private System.Windows.Forms.SplitContainer splitContainerBottom;
        private Sunny.UI.UIPanel palStepDetails;
        private Sunny.UI.UIPanel palStepDetailsTop;
        private System.Windows.Forms.TableLayoutPanel tblStepDetails;
        private System.Windows.Forms.Label lblStepNumberTitle;
        private System.Windows.Forms.Label lblStepNumber;
        private System.Windows.Forms.Label lblStepNameTitle;
        private System.Windows.Forms.Label lblStepName;
        private System.Windows.Forms.Label lblStepTypeTitle;
        private System.Windows.Forms.Label lblStepType;
        private System.Windows.Forms.Label lblStepStatusTitle;
        private System.Windows.Forms.Label lblStepStatus;
        private System.Windows.Forms.Label lblExecutionTimeTitle;
        private System.Windows.Forms.Label lblExecutionTime;
        private Sunny.UI.UIPanel palExecutionLog;
        private Sunny.UI.UIPanel palExecutionLogTop;
        private System.Windows.Forms.TextBox txtLog;
        private Sunny.UI.UIPanel pnlButtons;
        private Sunny.UI.UISymbolButton btnExecute;
        private ImageList imageList1;
        private UISymbolButton btnVariableDefine;
        private UISymbolButton btnClose;
        private UISymbolButton btnSave;
        private UISymbolButton BtnSystemParams;
        private UISymbolButton BtnVariableMonitor;
        private UISymbolButton BtnPointDefine;
    }
}

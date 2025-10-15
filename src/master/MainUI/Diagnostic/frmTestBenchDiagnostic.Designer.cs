namespace MainUI
{
    partial class frmTestBenchDiagnostic
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
            this.tabControl = new AntdUI.Tabs();
            this.tabPageDiagnostic = new AntdUI.TabPage();
            this.panelDiagnostic = new System.Windows.Forms.Panel();
            this.txtDiagnosticReport = new System.Windows.Forms.TextBox();
            this.panelDiagnosticButtons = new System.Windows.Forms.Panel();
            this.lblHealthStatus = new System.Windows.Forms.Label();
            this.btnRefresh = new AntdUI.Button();
            this.btnSaveReport = new AntdUI.Button();
            this.btnRunDiagnostic = new AntdUI.Button();
            this.tabPageSQL = new AntdUI.TabPage();
            this.panelSQL = new System.Windows.Forms.Panel();
            this.txtSQLScript = new System.Windows.Forms.TextBox();
            this.panelSQLConfig = new System.Windows.Forms.Panel();
            this.btnCopySQL = new AntdUI.Button();
            this.btnGenerateSQL = new AntdUI.Button();
            this.txtLocation = new AntdUI.Input();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtBenchCode = new AntdUI.Input();
            this.lblBenchCode = new System.Windows.Forms.Label();
            this.txtBenchName = new AntdUI.Input();
            this.lblBenchName = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnClose = new AntdUI.Button();
            this.tabControl.SuspendLayout();
            this.tabPageDiagnostic.SuspendLayout();
            this.panelDiagnostic.SuspendLayout();
            this.panelDiagnosticButtons.SuspendLayout();
            this.tabPageSQL.SuspendLayout();
            this.panelSQL.SuspendLayout();
            this.panelSQLConfig.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Size = new System.Drawing.Size(900, 600);
            this.tabControl.TabIndex = 0;
            this.tabControl.Controls.Add(this.tabPageDiagnostic);
            this.tabControl.Controls.Add(this.tabPageSQL);
            // 
            // tabPageDiagnostic
            // 
            this.tabPageDiagnostic.Text = "诊断报告";
            this.tabPageDiagnostic.Controls.Add(this.panelDiagnostic);
            this.tabPageDiagnostic.Controls.Add(this.panelDiagnosticButtons);
            this.tabPageDiagnostic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageDiagnostic.Location = new System.Drawing.Point(0, 40);
            this.tabPageDiagnostic.Name = "tabPageDiagnostic";
            this.tabPageDiagnostic.Size = new System.Drawing.Size(900, 560);
            this.tabPageDiagnostic.TabIndex = 0;
            // 
            // panelDiagnostic
            // 
            this.panelDiagnostic.Controls.Add(this.txtDiagnosticReport);
            this.panelDiagnostic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDiagnostic.Location = new System.Drawing.Point(0, 80);
            this.panelDiagnostic.Name = "panelDiagnostic";
            this.panelDiagnostic.Padding = new System.Windows.Forms.Padding(10);
            this.panelDiagnostic.Size = new System.Drawing.Size(900, 480);
            this.panelDiagnostic.TabIndex = 1;
            // 
            // txtDiagnosticReport
            // 
            this.txtDiagnosticReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiagnosticReport.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtDiagnosticReport.Location = new System.Drawing.Point(10, 10);
            this.txtDiagnosticReport.Multiline = true;
            this.txtDiagnosticReport.Name = "txtDiagnosticReport";
            this.txtDiagnosticReport.ReadOnly = true;
            this.txtDiagnosticReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDiagnosticReport.Size = new System.Drawing.Size(880, 460);
            this.txtDiagnosticReport.TabIndex = 0;
            this.txtDiagnosticReport.WordWrap = false;
            // 
            // panelDiagnosticButtons
            // 
            this.panelDiagnosticButtons.Controls.Add(this.lblHealthStatus);
            this.panelDiagnosticButtons.Controls.Add(this.btnRefresh);
            this.panelDiagnosticButtons.Controls.Add(this.btnSaveReport);
            this.panelDiagnosticButtons.Controls.Add(this.btnRunDiagnostic);
            this.panelDiagnosticButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDiagnosticButtons.Location = new System.Drawing.Point(0, 0);
            this.panelDiagnosticButtons.Name = "panelDiagnosticButtons";
            this.panelDiagnosticButtons.Padding = new System.Windows.Forms.Padding(10);
            this.panelDiagnosticButtons.Size = new System.Drawing.Size(900, 80);
            this.panelDiagnosticButtons.TabIndex = 0;
            // 
            // lblHealthStatus
            // 
            this.lblHealthStatus.AutoSize = true;
            this.lblHealthStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHealthStatus.Location = new System.Drawing.Point(380, 25);
            this.lblHealthStatus.Name = "lblHealthStatus";
            this.lblHealthStatus.Size = new System.Drawing.Size(120, 20);
            this.lblHealthStatus.TabIndex = 3;
            this.lblHealthStatus.Text = "等待诊断...";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(260, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 36);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新配置";
            this.btnRefresh.Type = AntdUI.TTypeMini.Primary;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSaveReport
            // 
            this.btnSaveReport.Location = new System.Drawing.Point(140, 20);
            this.btnSaveReport.Name = "btnSaveReport";
            this.btnSaveReport.Size = new System.Drawing.Size(100, 36);
            this.btnSaveReport.TabIndex = 1;
            this.btnSaveReport.Text = "保存报告";
            this.btnSaveReport.Type = AntdUI.TTypeMini.Success;
            this.btnSaveReport.Click += new System.EventHandler(this.btnSaveReport_Click);
            // 
            // btnRunDiagnostic
            // 
            this.btnRunDiagnostic.Location = new System.Drawing.Point(20, 20);
            this.btnRunDiagnostic.Name = "btnRunDiagnostic";
            this.btnRunDiagnostic.Size = new System.Drawing.Size(100, 36);
            this.btnRunDiagnostic.TabIndex = 0;
            this.btnRunDiagnostic.Text = "重新诊断";
            this.btnRunDiagnostic.Type = AntdUI.TTypeMini.Primary;
            this.btnRunDiagnostic.Click += new System.EventHandler(this.btnRunDiagnostic_Click);
            // 
            // tabPageSQL
            // 
            this.tabPageSQL.Text = "SQL配置";
            this.tabPageSQL.Controls.Add(this.panelSQL);
            this.tabPageSQL.Controls.Add(this.panelSQLConfig);
            this.tabPageSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageSQL.Location = new System.Drawing.Point(0, 40);
            this.tabPageSQL.Name = "tabPageSQL";
            this.tabPageSQL.Size = new System.Drawing.Size(900, 560);
            this.tabPageSQL.TabIndex = 1;
            // 
            // panelSQL
            // 
            this.panelSQL.Controls.Add(this.txtSQLScript);
            this.panelSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSQL.Location = new System.Drawing.Point(0, 180);
            this.panelSQL.Name = "panelSQL";
            this.panelSQL.Padding = new System.Windows.Forms.Padding(10);
            this.panelSQL.Size = new System.Drawing.Size(900, 380);
            this.panelSQL.TabIndex = 1;
            // 
            // txtSQLScript
            // 
            this.txtSQLScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLScript.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtSQLScript.Location = new System.Drawing.Point(10, 10);
            this.txtSQLScript.Multiline = true;
            this.txtSQLScript.Name = "txtSQLScript";
            this.txtSQLScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQLScript.Size = new System.Drawing.Size(880, 360);
            this.txtSQLScript.TabIndex = 0;
            this.txtSQLScript.WordWrap = false;
            // 
            // panelSQLConfig
            // 
            this.panelSQLConfig.Controls.Add(this.btnCopySQL);
            this.panelSQLConfig.Controls.Add(this.btnGenerateSQL);
            this.panelSQLConfig.Controls.Add(this.txtLocation);
            this.panelSQLConfig.Controls.Add(this.lblLocation);
            this.panelSQLConfig.Controls.Add(this.txtBenchCode);
            this.panelSQLConfig.Controls.Add(this.lblBenchCode);
            this.panelSQLConfig.Controls.Add(this.txtBenchName);
            this.panelSQLConfig.Controls.Add(this.lblBenchName);
            this.panelSQLConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSQLConfig.Location = new System.Drawing.Point(0, 0);
            this.panelSQLConfig.Name = "panelSQLConfig";
            this.panelSQLConfig.Padding = new System.Windows.Forms.Padding(10);
            this.panelSQLConfig.Size = new System.Drawing.Size(900, 180);
            this.panelSQLConfig.TabIndex = 0;
            // 
            // btnCopySQL
            // 
            this.btnCopySQL.Location = new System.Drawing.Point(140, 130);
            this.btnCopySQL.Name = "btnCopySQL";
            this.btnCopySQL.Size = new System.Drawing.Size(100, 36);
            this.btnCopySQL.TabIndex = 7;
            this.btnCopySQL.Text = "复制SQL";
            this.btnCopySQL.Type = AntdUI.TTypeMini.Success;
            this.btnCopySQL.Click += new System.EventHandler(this.btnCopySQL_Click);
            // 
            // btnGenerateSQL
            // 
            this.btnGenerateSQL.Location = new System.Drawing.Point(20, 130);
            this.btnGenerateSQL.Name = "btnGenerateSQL";
            this.btnGenerateSQL.Size = new System.Drawing.Size(100, 36);
            this.btnGenerateSQL.TabIndex = 6;
            this.btnGenerateSQL.Text = "生成SQL";
            this.btnGenerateSQL.Type = AntdUI.TTypeMini.Primary;
            this.btnGenerateSQL.Click += new System.EventHandler(this.btnGenerateSQL_Click);
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(120, 85);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.PlaceholderText = "请输入试验台位置";
            this.txtLocation.Size = new System.Drawing.Size(300, 32);
            this.txtLocation.TabIndex = 5;
            this.txtLocation.Text = "实验室";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(20, 92);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(80, 17);
            this.lblLocation.TabIndex = 4;
            this.lblLocation.Text = "试验台位置:";
            // 
            // txtBenchCode
            // 
            this.txtBenchCode.Location = new System.Drawing.Point(120, 47);
            this.txtBenchCode.Name = "txtBenchCode";
            this.txtBenchCode.PlaceholderText = "请输入试验台编号";
            this.txtBenchCode.Size = new System.Drawing.Size(300, 32);
            this.txtBenchCode.TabIndex = 3;
            this.txtBenchCode.Text = "BENCH-001";
            // 
            // lblBenchCode
            // 
            this.lblBenchCode.AutoSize = true;
            this.lblBenchCode.Location = new System.Drawing.Point(20, 54);
            this.lblBenchCode.Name = "lblBenchCode";
            this.lblBenchCode.Size = new System.Drawing.Size(80, 17);
            this.lblBenchCode.TabIndex = 2;
            this.lblBenchCode.Text = "试验台编号:";
            // 
            // txtBenchName
            // 
            this.txtBenchName.Location = new System.Drawing.Point(120, 9);
            this.txtBenchName.Name = "txtBenchName";
            this.txtBenchName.PlaceholderText = "请输入试验台名称";
            this.txtBenchName.Size = new System.Drawing.Size(300, 32);
            this.txtBenchName.TabIndex = 1;
            this.txtBenchName.Text = "默认试验台";
            // 
            // lblBenchName
            // 
            this.lblBenchName.AutoSize = true;
            this.lblBenchName.Location = new System.Drawing.Point(20, 16);
            this.lblBenchName.Name = "lblBenchName";
            this.lblBenchName.Size = new System.Drawing.Size(80, 17);
            this.lblBenchName.TabIndex = 0;
            this.lblBenchName.Text = "试验台名称:";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 600);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(10);
            this.panelBottom.Size = new System.Drawing.Size(900, 60);
            this.panelBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(780, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmTestBenchDiagnostic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 660);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelBottom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTestBenchDiagnostic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试验台IP诊断工具";
            this.Load += new System.EventHandler(this.frmTestBenchDiagnostic_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageDiagnostic.ResumeLayout(false);
            this.panelDiagnostic.ResumeLayout(false);
            this.panelDiagnostic.PerformLayout();
            this.panelDiagnosticButtons.ResumeLayout(false);
            this.panelDiagnosticButtons.PerformLayout();
            this.tabPageSQL.ResumeLayout(false);
            this.panelSQL.ResumeLayout(false);
            this.panelSQL.PerformLayout();
            this.panelSQLConfig.ResumeLayout(false);
            this.panelSQLConfig.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private AntdUI.Tabs tabControl;
        private AntdUI.TabPage tabPageDiagnostic;
        private AntdUI.TabPage tabPageSQL;
        private System.Windows.Forms.Panel panelDiagnostic;
        private System.Windows.Forms.TextBox txtDiagnosticReport;
        private System.Windows.Forms.Panel panelDiagnosticButtons;
        private AntdUI.Button btnRunDiagnostic;
        private AntdUI.Button btnSaveReport;
        private AntdUI.Button btnRefresh;
        private System.Windows.Forms.Label lblHealthStatus;
        private System.Windows.Forms.Panel panelSQL;
        private System.Windows.Forms.TextBox txtSQLScript;
        private System.Windows.Forms.Panel panelSQLConfig;
        private System.Windows.Forms.Label lblBenchName;
        private AntdUI.Input txtBenchName;
        private AntdUI.Input txtBenchCode;
        private System.Windows.Forms.Label lblBenchCode;
        private AntdUI.Input txtLocation;
        private System.Windows.Forms.Label lblLocation;
        private AntdUI.Button btnGenerateSQL;
        private AntdUI.Button btnCopySQL;
        private System.Windows.Forms.Panel panelBottom;
        private AntdUI.Button btnClose;
    }
}
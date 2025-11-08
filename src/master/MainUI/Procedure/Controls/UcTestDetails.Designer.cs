using System.Drawing;
using System.Windows.Forms;
using static FreeSql.DatabaseModel.DbTypeInfo;

namespace MainUI.Procedure.Controls
{
    partial class UcTestDetails
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源,为 true;否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing)
            {
                updateTimer?.Stop();
                updateTimer?.Dispose();
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
            panelInfo = new AntdUI.Panel();
            lblCurrentTest = new AntdUI.Label();
            statusPanel = new AntdUI.Panel();
            statusBadge = new AntdUI.Panel();
            lblTestStatus = new AntdUI.Label();
            lblElapsedTime = new AntdUI.Label();
            lblCurrentStep = new AntdUI.Label();
            progressBar = new AntdUI.Progress();
            panelSteps = new AntdUI.Panel();
            panelStepList = new AntdUI.Panel();
            panelInfo.SuspendLayout();
            statusPanel.SuspendLayout();
            statusBadge.SuspendLayout();
            panelSteps.SuspendLayout();
            SuspendLayout();
            // 
            // panelInfo
            // 
            panelInfo.BackColor = Color.FromArgb(255, 255, 255);
            panelInfo.Controls.Add(lblCurrentTest);
            panelInfo.Controls.Add(statusPanel);
            panelInfo.Controls.Add(progressBar);
            panelInfo.Controls.Add(lblCurrentStep);
            panelInfo.Dock = DockStyle.Top;
            panelInfo.Location = new Point(0, 0);
            panelInfo.Name = "panelInfo";
            panelInfo.Padding = new Padding(24);
            panelInfo.Radius = 0;
            panelInfo.Shadow = 8;
            panelInfo.ShadowOpacity = 0.08F;
            panelInfo.Size = new Size(900, 150);
            panelInfo.TabIndex = 0;
            // 
            // lblCurrentTest
            // 
            lblCurrentTest.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblCurrentTest.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            lblCurrentTest.ForeColor = Color.FromArgb(31, 35, 41);
            lblCurrentTest.Location = new Point(24, 20);
            lblCurrentTest.Name = "lblCurrentTest";
            lblCurrentTest.Size = new Size(144, 23);
            lblCurrentTest.TabIndex = 0;
            lblCurrentTest.Text = "当前测试项:未开始";
            // 
            // statusPanel
            // 
            statusPanel.BackColor = Color.Transparent;
            statusPanel.Controls.Add(statusBadge);
            statusPanel.Controls.Add(lblElapsedTime);
            statusPanel.Location = new Point(24, 55);
            statusPanel.Name = "statusPanel";
            statusPanel.Size = new Size(858, 28);
            statusPanel.TabIndex = 1;
            // 
            // statusBadge
            // 
            statusBadge.Controls.Add(lblTestStatus);
            statusBadge.Location = new Point(0, 0);
            statusBadge.Name = "statusBadge";
            statusBadge.Size = new Size(0, 0);
            statusBadge.TabIndex = 0;
            // 
            // lblTestStatus
            // 
            lblTestStatus.Location = new Point(0, 0);
            lblTestStatus.Name = "lblTestStatus";
            lblTestStatus.Size = new Size(0, 0);
            lblTestStatus.TabIndex = 0;
            // 
            // lblElapsedTime
            // 
            lblElapsedTime.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblElapsedTime.Font = new Font("Consolas", 10F);
            lblElapsedTime.ForeColor = Color.FromArgb(82, 86, 89);
            lblElapsedTime.Location = new Point(160, 4);
            lblElapsedTime.Name = "lblElapsedTime";
            lblElapsedTime.Size = new Size(152, 16);
            lblElapsedTime.TabIndex = 1;
            lblElapsedTime.Text = "⏱ 已用时间: 00:00:00";
            // 
            // lblCurrentStep
            // 
            lblCurrentStep.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblCurrentStep.Font = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblCurrentStep.ForeColor = Color.FromArgb(82, 86, 89);
            lblCurrentStep.Location = new Point(24, 95);
            lblCurrentStep.Name = "lblCurrentStep";
            lblCurrentStep.Size = new Size(144, 20);
            lblCurrentStep.TabIndex = 2;
            lblCurrentStep.Text = " 当前步骤: 等待开始...";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(24, 120);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(849, 16);
            progressBar.TabIndex = 3;
            // 
            // panelSteps
            // 
            panelSteps.BackColor = Color.FromArgb(240, 242, 245);
            panelSteps.Controls.Add(panelStepList);
            panelSteps.Dock = DockStyle.Fill;
            panelSteps.Location = new Point(0, 0);
            panelSteps.Name = "panelSteps";
            panelSteps.Padding = new Padding(24);
            panelSteps.Size = new Size(900, 850);
            panelSteps.TabIndex = 1;
            // 
            // panelStepList
            // 
            panelStepList.BackColor = Color.Transparent;
            panelStepList.Dock = DockStyle.Bottom;
            panelStepList.Location = new Point(24, 156);
            panelStepList.Name = "panelStepList";
            panelStepList.Size = new Size(852, 670);
            panelStepList.TabIndex = 0;
            panelStepList.AutoScrollOffset = new Point(900, 500);
            // 
            // UcTestDetails
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(240, 242, 245);
            Controls.Add(panelInfo);
            Controls.Add(panelSteps);
            Font = new Font("微软雅黑", 10F);
            ForeColor = Color.FromArgb(82, 86, 89);
            Location = new Point(12, 4);
            Name = "UcTestDetails";
            Size = new Size(900, 850);
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            statusPanel.ResumeLayout(false);
            statusPanel.PerformLayout();
            statusBadge.ResumeLayout(false);
            panelSteps.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region 组件设计器生成的字段

        private AntdUI.Panel panelSteps;
        private AntdUI.Panel panelInfo;
        private AntdUI.Label lblCurrentTest;
        private AntdUI.Label lblTestStatus;
        private AntdUI.Label lblElapsedTime;
        private AntdUI.Label lblCurrentStep;
        private AntdUI.Progress progressBar;
        private AntdUI.Panel panelStepList;
        private System.Windows.Forms.Timer updateTimer;
        private AntdUI.Panel statusBadge;
        AntdUI.Panel statusPanel;
        #endregion

    }
}
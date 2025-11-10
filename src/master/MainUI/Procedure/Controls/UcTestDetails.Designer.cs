using System.Drawing;
using System.Windows.Forms;

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
            statusBadge = new UIPanel();
            lblTestStatus = new AntdUI.Label();
            lblElapsedTime = new AntdUI.Label();
            lblCurrentStep = new AntdUI.Label();
            progressBar = new AntdUI.Progress();
            panelSteps = new AntdUI.Panel();
            panelStepList = new FlowLayoutPanel();
            panelInfo.SuspendLayout();
            statusPanel.SuspendLayout();
            statusBadge.SuspendLayout();
            panelSteps.SuspendLayout();
            SuspendLayout();
            // 
            // panelInfo
            // 
            panelInfo.BackColor = Color.FromArgb(245, 247, 250);
            panelInfo.Controls.Add(lblCurrentTest);
            panelInfo.Controls.Add(statusPanel);
            panelInfo.Controls.Add(lblElapsedTime);
            panelInfo.Controls.Add(lblCurrentStep);
            panelInfo.Controls.Add(progressBar);
            panelInfo.Dock = DockStyle.Top;
            panelInfo.Location = new Point(0, 0);
            panelInfo.Name = "panelInfo";
            panelInfo.Padding = new Padding(20, 15, 20, 15);
            panelInfo.Radius = 8;
            panelInfo.Shadow = 8;
            panelInfo.ShadowOpacity = 1F;
            panelInfo.Size = new Size(900, 170);
            panelInfo.TabIndex = 0;
            // 
            // lblCurrentTest
            // 
            lblCurrentTest.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblCurrentTest.BackColor = Color.Transparent;
            lblCurrentTest.Font = new Font("微软雅黑", 14F, FontStyle.Bold);
            lblCurrentTest.ForeColor = Color.FromArgb(64, 64, 64);
            lblCurrentTest.Location = new Point(20, 20);
            lblCurrentTest.Name = "lblCurrentTest";
            lblCurrentTest.Size = new Size(161, 25);
            lblCurrentTest.TabIndex = 0;
            lblCurrentTest.Text = "当前测试项: 未开始";
            // 
            // statusPanel
            // 
            statusPanel.BackColor = Color.Transparent;
            statusPanel.Controls.Add(statusBadge);
            statusPanel.Location = new Point(700, 20);
            statusPanel.Name = "statusPanel";
            statusPanel.Size = new Size(180, 36);
            statusPanel.TabIndex = 4;
            // 
            // statusBadge
            // 
            statusBadge.BackColor = Color.Transparent;
            statusBadge.Controls.Add(lblTestStatus);
            statusBadge.Dock = DockStyle.Fill;
            statusBadge.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            statusBadge.Location = new Point(0, 0);
            statusBadge.Margin = new Padding(4, 5, 4, 5);
            statusBadge.MinimumSize = new Size(1, 1);
            statusBadge.Name = "statusBadge";
            statusBadge.Padding = new Padding(15, 5, 15, 5);
            statusBadge.Radius = 18;
            statusBadge.Size = new Size(180, 36);
            statusBadge.TabIndex = 0;
            statusBadge.Text = null;
            statusBadge.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblTestStatus
            // 
            lblTestStatus.Dock = DockStyle.Fill;
            lblTestStatus.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            lblTestStatus.ForeColor = Color.FromArgb(24, 144, 255);
            lblTestStatus.Location = new Point(15, 5);
            lblTestStatus.Name = "lblTestStatus";
            lblTestStatus.Size = new Size(150, 26);
            lblTestStatus.TabIndex = 1;
            lblTestStatus.Text = "● 待机中";
            // 
            // lblElapsedTime
            // 
            lblElapsedTime.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblElapsedTime.BackColor = Color.Transparent;
            lblElapsedTime.Font = new Font("微软雅黑", 10F);
            lblElapsedTime.ForeColor = Color.FromArgb(128, 128, 128);
            lblElapsedTime.Location = new Point(20, 60);
            lblElapsedTime.Name = "lblElapsedTime";
            lblElapsedTime.Size = new Size(130, 18);
            lblElapsedTime.TabIndex = 2;
            lblElapsedTime.Text = "⏱ 已用时间: 00:00:00";
            // 
            // lblCurrentStep
            // 
            lblCurrentStep.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblCurrentStep.BackColor = Color.Transparent;
            lblCurrentStep.Font = new Font("微软雅黑", 10F);
            lblCurrentStep.ForeColor = Color.FromArgb(128, 128, 128);
            lblCurrentStep.Location = new Point(250, 60);
            lblCurrentStep.Name = "lblCurrentStep";
            lblCurrentStep.Size = new Size(115, 18);
            lblCurrentStep.TabIndex = 3;
            lblCurrentStep.Text = " 当前步骤: 准备中...";
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.Transparent;
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Font = new Font("微软雅黑", 9F);
            progressBar.ForeColor = Color.FromArgb(24, 144, 255);
            progressBar.Location = new Point(28, 117);
            progressBar.Name = "progressBar";
            progressBar.Radius = 4;
            progressBar.Size = new Size(844, 30);
            progressBar.TabIndex = 5;
            progressBar.Value = 0.1F;
            progressBar.ValueRatio = 0.8F;
            // 
            // panelSteps
            // 
            panelSteps.Back = Color.FromArgb(245, 247, 250);
            panelSteps.BackColor = Color.FromArgb(245, 247, 250);
            panelSteps.Controls.Add(panelStepList);
            panelSteps.Dock = DockStyle.Fill;
            panelSteps.Location = new Point(0, 170);
            panelSteps.Name = "panelSteps";
            panelSteps.Padding = new Padding(0, 15, 0, 0);
            panelSteps.Size = new Size(900, 680);
            panelSteps.TabIndex = 1;
            // 
            // panelStepList
            // 
            panelStepList.AutoScroll = true;
            panelStepList.AutoScrollMargin = new Size(10, 10);
            panelStepList.AutoScrollMinSize = new Size(10, 10);
            panelStepList.AutoSize = true;
            panelStepList.BackColor = Color.FromArgb(245, 247, 250);
            panelStepList.Dock = DockStyle.Fill;
            panelStepList.FlowDirection = FlowDirection.TopDown;
            panelStepList.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelStepList.Location = new Point(0, 15);
            panelStepList.Margin = new Padding(4, 5, 4, 5);
            panelStepList.MinimumSize = new Size(1, 1);
            panelStepList.Name = "panelStepList";
            panelStepList.Padding = new Padding(15, 0, 15, 12);
            panelStepList.Size = new Size(900, 665);
            panelStepList.TabIndex = 0;
            panelStepList.WrapContents = false;
            // 
            // UcTestDetails
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(panelSteps);
            Controls.Add(panelInfo);
            Font = new Font("微软雅黑", 10F);
            ForeColor = Color.FromArgb(82, 86, 89);
            Location = new Point(12, 4);
            Name = "UcTestDetails";
            Size = new Size(900, 850);
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            statusPanel.ResumeLayout(false);
            statusBadge.ResumeLayout(false);
            panelSteps.ResumeLayout(false);
            panelSteps.PerformLayout();
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
        private FlowLayoutPanel panelStepList;
        private Sunny.UI.UIPanel statusBadge;
        private AntdUI.Panel statusPanel;

        #endregion
    }
}
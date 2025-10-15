using AntdUI;
using MainUI.Diagnostic;
using MainUI.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI
{
    public partial class frmTestBenchDiagnostic : UIForm
    {
        public frmTestBenchDiagnostic()
        {
            InitializeComponent();
        }

        private void frmTestBenchDiagnostic_Load(object sender, EventArgs e)
        {
            // 自动运行诊断
            RunDiagnostic();
        }

        /// <summary>
        /// 运行诊断
        /// </summary>
        private void RunDiagnostic()
        {
            try
            {
                btnRunDiagnostic.Enabled = false;
                txtDiagnosticReport.Text = "正在诊断,请稍候...";
                txtDiagnosticReport.Refresh();

                // 异步执行诊断
                Task.Run(() =>
                {
                    var report = TestBenchIPDiagnostic.RunFullDiagnostic();

                    this.Invoke(new Action(() =>
                    {
                        txtDiagnosticReport.Text = report;
                        btnRunDiagnostic.Enabled = true;

                        // 显示健康状态
                        var (isHealthy, message) = TestBenchIPDiagnostic.QuickHealthCheck();
                        lblHealthStatus.Text = message;
                        lblHealthStatus.ForeColor = isHealthy ? Color.Green : Color.Red;
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(this, $"诊断失败: {ex.Message}", TType.Error);
                btnRunDiagnostic.Enabled = true;
            }
        }

        /// <summary>
        /// 重新运行诊断
        /// </summary>
        private void btnRunDiagnostic_Click(object sender, EventArgs e)
        {
            RunDiagnostic();
        }

        /// <summary>
        /// 保存诊断报告
        /// </summary>
        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog sfd = new()
                {
                    Filter = "文本文件|*.txt|所有文件|*.*",
                    FileName = $"TestBench_Diagnostic_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
                    DefaultExt = "txt"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, txtDiagnosticReport.Text);
                    MessageHelper.MessageOK(this, "诊断报告保存成功!", TType.Success);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(this, $"保存失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 生成配置SQL
        /// </summary>
        private void btnGenerateSQL_Click(object sender, EventArgs e)
        {
            try
            {
                var sql = TestBenchIPDiagnostic.GenerateConfigSQL(
                    txtBenchName.Text.Trim(),
                    txtBenchCode.Text.Trim(),
                    txtLocation.Text.Trim()
                );

                txtSQLScript.Text = sql;
                tabControl.SelectedIndex = 1; // 切换到SQL标签页
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(this, $"生成SQL失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 复制SQL到剪贴板
        /// </summary>
        private void btnCopySQL_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSQLScript.Text))
                {
                    Clipboard.SetText(txtSQLScript.Text);
                    MessageHelper.MessageOK(this, "SQL已复制到剪贴板", TType.Success);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(this, $"复制失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 刷新试验台信息
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                TestBenchService.Reinitialize();
                MessageHelper.MessageOK(this, "试验台信息已刷新", TType.Success);
                RunDiagnostic();
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(this, $"刷新失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

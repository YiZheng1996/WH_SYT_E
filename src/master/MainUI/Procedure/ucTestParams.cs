using Sunny.UI;
using System.Windows.Forms;

namespace MainUI.Procedure
{
    public partial class UcTestParams : ucBaseManagerUI
    {
        // 模型名称
        private string ModelName => $"{txtType.Text}_{txtModel.Text}";
        ParaConfig paraconfig = new(); //参数配置类
        SaveReportConfig SaveRptconfig = new(); //报表保存配置类

        public UcTestParams() => InitializeComponent();

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void LoadConfig()
        {
            try
            {
                // 试验参数配置
                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(ModelName);
                paraconfig.Load();
                txtTemplateRpt.Text = paraconfig.RptFile;

                // 报表保存路径
                SaveRptconfig = new SaveReportConfig();
                SaveRptconfig.Load();
                txtSaveReport.Text = SaveRptconfig.RptSaveFile;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(ex.Message);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtModel.Text))
                {
                    MessageHelper.MessageOK($"型号未选择！");
                    return;
                }

                // 保存前的报表模板路径
                string oldRptFile = paraconfig.RptFile;

                paraconfig.SetSectionName(ModelName);
                paraconfig.RptFile = txtTemplateRpt.Text;
                paraconfig.Save();

                SaveRptconfig.RptSaveFile = txtSaveReport.Text;
                SaveRptconfig.Save();

                MessageHelper.MessageOK("保存成功。");

                // 检查报表模板是否变更
                if (oldRptFile != txtTemplateRpt.Text)
                {
                    // 报表模板已变更，触发报表刷新事件
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.ReportTemplate);
                    NlogHelper.Default.Info($"报表模板已变更: {oldRptFile} → {txtTemplateRpt.Text}");
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("保存失败。" + ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtType.Text))
            {
                MessageHelper.MessageOK("型号未选择!", AntdUI.TType.Error);
                return;
            }
            OpenFileDialog openFile = new()
            {
                InitialDirectory = Application.StartupPath + "reports\\",
                Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx"
            };
            openFile.ShowDialog();
            if (string.IsNullOrEmpty(openFile.FileName) == false)
            {
                string path = openFile.FileName;
                string[] str = path.Split('\\');
                txtTemplateRpt.Text = str[^1];
            }
        }

        //产品选择
        private void btnProductSelection_Click(object sender, EventArgs e)
        {
            FrmSpec fs = new();
            fs.ShowDialog();
            if (fs.DialogResult == DialogResult.OK)
            {
                txtType.Text = VarHelper.TestViewModel.ModelTypeName;
                txtModel.Text = VarHelper.TestViewModel.ModelName;
                LoadConfig();
            }
        }

        private void btnParameter_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 0;
            UpdateNavigationButtons(0, btnParameter);
            UpdateNavigationButtons(1, btnReport);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 1;
            UpdateNavigationButtons(0, btnReport);
            UpdateNavigationButtons(1, btnParameter);
        }

        private void btnSaveBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new()
            {
                InitialDirectory = "D:\\",
            };
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                txtSaveReport.Text = path;
            }
        }

        public void UpdateNavigationButtons(int selectedIndex, AntdUI.Button button)
        {
            switch (selectedIndex)
            {
                case 0:
                    button.ForeColor = Color.FromArgb(64, 64, 64);
                    button.BackColor = Color.White;
                    break;
                case 1:
                    button.ForeColor = Color.White;
                    button.BackColor = Color.FromArgb(196, 199, 204);
                    break;
                default:
                    break;
            }
        }
    }
}

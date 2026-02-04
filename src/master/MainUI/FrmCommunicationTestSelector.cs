using Sunny.UI;
using MainUI.LogicalConfiguration.Instrument.Forms;
using MainUI.LogicalConfiguration.Instrument.TestTools;

namespace MainUI
{
    public partial class FrmCommunicationTestSelector : UIForm
    {
        public FrmCommunicationTestSelector()
        {
            InitializeComponent();
        }

        private void btnTcpServerTest_Click(object sender, EventArgs e)
        {
            using var frmTcpServer = new FrmTcpServerTest();
            frmTcpServer.ShowDialog();
        }

        private void btnCommunicationTest_Click(object sender, EventArgs e)
        {
            using var frmCommTest = new FrmCommunicationTest();
            frmCommTest.ShowDialog();
        }

        private void btnSerialLoopbackTest_Click(object sender, EventArgs e)
        {
            using var frmSerialTest = new FrmSerialLoopbackTest();
            frmSerialTest.ShowDialog();
        }

        private void btnAutoTest_Click(object sender, EventArgs e)
        {
            // 运行自动化测试脚本
            RunAutoTest();
            this.Close();
        }

        private async void RunAutoTest()
        {
            try
            {
                var autoTest = new CommunicationAutoTest();

                // 在后台线程运行
                await Task.Run(async () =>
                {
                    await autoTest.RunAllTests();
                });

                UIMessageBox.ShowSuccess("自动化测试完成!");
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"自动化测试失败: {ex.Message}");
            }
        }
    }
}
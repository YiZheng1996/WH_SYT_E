using MainUI.Service;

namespace MainUI
{
    public partial class frmDispReport : UIForm
    {
        private readonly string filename;
        private RW.UI.Controls.Report.RWReport rWReport;
        public frmDispReport(string file)
        {
            InitializeComponent();
            filename = file;
        }
        private void frmDispReport_Load(object sender, EventArgs e)
        {
            try
            {
                rWReport = new()
                {
                    Dock = DockStyle.Fill,
                    Enabled = false
                };
                grpReport.Controls.Add(rWReport);
                rWReport.Filename = filename;
                rWReport.Init();

                // 获取当前试验台信息
                var currentTestBench = TestBenchService.CurrentTestBench;
                if (currentTestBench.DataScope == 1)
                {
                    BtnPrint.Visible = true;
                }
                else
                {
                    BtnPrint.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(this, $"加载报表出现错误：{ex.Message}");
            }
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            rWReport.Print();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public int CurrentRows { get; private set; } = 1;
        private void btnPageUp_Click(object sender, EventArgs e)
        {
            int pageSize = Convert.ToInt32(inputNumber.Value);
            CurrentRows -= pageSize;
            if (CurrentRows < 1)
            {
                CurrentRows = 1;
            }

            // 执行报表滚动
            rWReport?.ScrollIndex(CurrentRows);
        }

        private void btnPageDown_Click(object sender, EventArgs e)
        {
            int pageSize = Convert.ToInt32(inputNumber.Value);
            CurrentRows += pageSize;

            if (CurrentRows > 1000)
            {
                CurrentRows = 1; // 循环到第一页
            }
            // 执行报表滚动
            rWReport?.ScrollIndex(CurrentRows);
        }
    }
}

// MainUI/frmTestBenchSelect.cs
namespace MainUI
{
    public partial class frmTestBenchSelect : Sunny.UI.UIForm
    {
        private readonly TestBenchBLL _testBenchBLL = new();
        public TestBenchModel SelectedTestBench { get; private set; }

        public frmTestBenchSelect()
        {
            InitializeComponent();
        }

        private void frmTestBenchSelect_Load(object sender, EventArgs e)
        {
            LoadTestBenches();
        }

        private void LoadTestBenches()
        {
            try
            {
                var testBenches = _testBenchBLL.GetActiveTestBenches();

                if (testBenches == null || testBenches.Count == 0)
                {
                    MessageHelper.MessageOK(this, "系统中没有可用的试验台，请联系管理员配置。", AntdUI.TType.Error);
                    btnConfirm.Enabled = false;
                    return;
                }

                // 绑定到ComboBox
                cboTestBench.DataSource = testBenches;
                cboTestBench.DisplayMember = "BenchName";
                cboTestBench.ValueMember = "ID";

                // 默认选中第一项
                if (testBenches.Count > 0)
                {
                    cboTestBench.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载试验台列表失败", ex);
                MessageHelper.MessageOK(this, $"加载试验台列表失败: {ex.Message}", AntdUI.TType.Error);
            }
        }

        private void cboTestBench_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTestBench.SelectedItem is TestBenchModel bench)
                {
                    // 显示试验台详细信息
                    lblBenchInfo.Text = $"编号: {bench.BenchCode}\n" +
                                       $"位置: {bench.Location}\n" +
                                       $"IP地址: {bench.BenchIP}";
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("显示试验台信息失败", ex);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTestBench.SelectedValue == null)
                {
                    MessageHelper.MessageOK(this, "请选择试验台", AntdUI.TType.Warn);
                    cboTestBench.Focus();
                    return;
                }

                SelectedTestBench = cboTestBench.SelectedItem as TestBenchModel;

                if (SelectedTestBench == null)
                {
                    MessageHelper.MessageOK(this, "获取试验台信息失败", AntdUI.TType.Error);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("确认选择试验台失败", ex);
                MessageHelper.MessageOK(this, $"确认失败: {ex.Message}", AntdUI.TType.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
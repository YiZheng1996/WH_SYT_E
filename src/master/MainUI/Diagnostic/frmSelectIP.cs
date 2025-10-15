using MainUI.Service;

namespace MainUI.Diagnostic
{
    /// <summary>
    /// IP选择窗体
    /// </summary>
    public partial class frmSelectIP : UIForm
    {
        private string _selectedIP;
        private readonly List<string> _availableIPs;
        private readonly Dictionary<string, TestBenchModel> _ipTestBenchMap;

        /// <summary>
        /// 用户选择的IP地址
        /// </summary>
        public string SelectedIP => _selectedIP;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="availableIPs">可用的IP列表</param>
        /// <param name="ipTestBenchMap">IP与试验台的映射关系</param>
        public frmSelectIP(List<string> availableIPs, Dictionary<string, TestBenchModel> ipTestBenchMap)
        {
            InitializeComponent();
            _availableIPs = availableIPs ?? [];
            _ipTestBenchMap = ipTestBenchMap ?? [];

            InitializeData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitializeData()
        {
            // 清空列表
            listBoxIP.Items.Clear();

            // 添加IP到列表,并标注是否已注册
            foreach (var ip in _availableIPs)
            {
                var isRegistered = _ipTestBenchMap.ContainsKey(ip);
                var displayText = isRegistered
                    ? $"{ip}  ✅ ({_ipTestBenchMap[ip].BenchName})"
                    : $"{ip}  ❌ (未注册)";

                listBoxIP.Items.Add(displayText);

                // 如果已注册,设置为默认选中第一个
                if (isRegistered && listBoxIP.SelectedIndex == -1)
                {
                    listBoxIP.SelectedIndex = listBoxIP.Items.Count - 1;
                }
            }

            // 更新选中信息
            UpdateSelectionInfo();
        }

        /// <summary>
        /// 列表选择改变事件
        /// </summary>
        private void listBoxIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectionInfo();
        }

        /// <summary>
        /// 更新选中信息显示
        /// </summary>
        private void UpdateSelectionInfo()
        {
            if (listBoxIP.SelectedIndex == -1)
            {
                lblSelectedInfo.Text = "请选择一个IP地址";
                lblSelectedInfo.ForeColor = Color.Gray;
                btnConfirm.Enabled = false;
                return;
            }

            var selectedIP = _availableIPs[listBoxIP.SelectedIndex];

            if (_ipTestBenchMap.ContainsKey(selectedIP))
            {
                var testBench = _ipTestBenchMap[selectedIP];
                lblSelectedInfo.Text = $"✅ 已选择: {selectedIP}\n试验台: {testBench.BenchName}\n位置: {testBench.Location}";
                lblSelectedInfo.ForeColor = Color.Green;
                btnConfirm.Enabled = true;
            }
            else
            {
                lblSelectedInfo.Text = $"❌ 该IP地址未在数据库中注册!\n\n请联系管理员在数据库中添加:\nBenchIP = '{selectedIP}'";
                lblSelectedInfo.ForeColor = Color.Red;
                btnConfirm.Enabled = false;
            }
        }

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (listBoxIP.SelectedIndex == -1)
            {
                MessageHelper.MessageOK(this, "请选择一个IP地址!", AntdUI.TType.Info);
                return;
            }

            var selectedIP = _availableIPs[listBoxIP.SelectedIndex];

            if (!_ipTestBenchMap.ContainsKey(selectedIP))
            {
                MessageHelper.MessageOK(this, "该IP地址未在数据库中注册,无法使用!", AntdUI.TType.Error);
                return;
            }

            _selectedIP = selectedIP;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 确认退出
            if (MessageHelper.MessageYes(this, "确定要退出程序吗?") == DialogResult.OK)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        /// <summary>
        /// 刷新按钮点击事件
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // 重新获取IP列表
                _availableIPs.Clear();
                var newIPs = TestBenchService.GetAllLocalIPAddresses();
                _availableIPs.AddRange(newIPs);

                if (newIPs.Count == 0)
                {
                    MessageHelper.MessageOK(this, "未检测到有效的IP地址!", AntdUI.TType.Error);
                    return;
                }

                // 重新获取数据库中的试验台映射
                _ipTestBenchMap.Clear();
                var newMapping = TestBenchService.GetIPTestBenchMapping();
                foreach (var item in newMapping)
                {
                    _ipTestBenchMap[item.Key] = item.Value;
                }

                // 清除已保存的IP配置(强制用户重新选择)
                TestBenchService.ClearSavedIP();

                // 刷新界面数据
                InitializeData();

                MessageHelper.MessageOK(this, "刷新成功!请重新选择IP地址。", AntdUI.TType.Success);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("刷新IP列表失败", ex);
                MessageHelper.MessageOK(this, $"刷新失败: {ex.Message}", AntdUI.TType.Error);
            }
        }

    }
}
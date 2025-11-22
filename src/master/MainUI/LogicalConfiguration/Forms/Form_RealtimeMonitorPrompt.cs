using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services.ServicesPLC;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 实时监控提示窗体
    /// 显示实时更新的数值和提示信息
    /// 继承自 Sunny.UI.UIForm 以保持项目统一风格
    /// </summary>
    public partial class Form_RealtimeMonitorPrompt : UIForm
    {
        #region 私有字段

        private readonly Parameter_RealtimeMonitorPrompt _parameter;
        private readonly GlobalVariableManager _variableManager;
        private readonly IPLCManager _plcManager;
        private System.Windows.Forms.Timer _refreshTimer;

        #endregion

        #region 构造函数

        public Form_RealtimeMonitorPrompt(
            Parameter_RealtimeMonitorPrompt parameter,
            GlobalVariableManager variableManager,
            IPLCManager plcManager)
        {
            _parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _plcManager = plcManager ?? throw new ArgumentNullException(nameof(plcManager));

            InitializeComponent();
            InitializeCustomSettings();
            InitializeRefreshTimer();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化自定义设置
        /// </summary>
        private void InitializeCustomSettings()
        {
            // 设置窗体标题
            Text = _parameter.Title;

            // 设置数值标签文本
            if (_parameter.ShowValueLabel)
            {
                lblValueLabel.Text = string.IsNullOrEmpty(_parameter.ValueLabelText)
                    ? _parameter.Unit
                    : _parameter.ValueLabelText;
                lblValueLabel.Visible = true;
            }
            else
            {
                lblValueLabel.Visible = false;
            }

            // 设置提示信息
            lblMessage.Text = _parameter.PromptMessage.Replace("\\n", Environment.NewLine);

            // 设置按钮文本
            btnConfirm.Text = _parameter.ButtonText;

            // 初始化数值显示
            lblValue.Text = "0.0";
        }

        /// <summary>
        /// 初始化刷新定时器
        /// </summary>
        private void InitializeRefreshTimer()
        {
            _refreshTimer = new System.Windows.Forms.Timer
            {
                Interval = _parameter.RefreshInterval
            };
            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();

            // 立即刷新一次
            _ = UpdateValueAsync();
        }

        #endregion

        #region 数据刷新

        /// <summary>
        /// 定时器触发事件
        /// </summary>
        private async void RefreshTimer_Tick(object sender, EventArgs e)
        {
            await UpdateValueAsync();
        }

        /// <summary>
        /// 更新显示的数值
        /// </summary>
        private async Task UpdateValueAsync()
        {
            if (lblValue == null || lblValue.IsDisposed) return;

            try
            {
                object value = await GetMonitorValueAsync();

                if (value != null)
                {
                    // 格式化显示
                    string displayText = FormatValue(value);

                    // 在UI线程更新
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            lblValue.Text = displayText;
                            lblValue.ForeColor = Color.FromArgb(24, 144, 255);
                        }));
                    }
                    else
                    {
                        lblValue.Text = displayText;
                        lblValue.ForeColor = Color.FromArgb(24, 144, 255);
                    }
                }
                else
                {
                    UpdateValueDisplay("N/A", Color.Gray);
                }
            }
            catch (Exception ex)
            {
                UpdateValueDisplay("错误", Color.Red);
                NlogHelper.Default.Error($"实时监控提示更新数值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取监测值
        /// </summary>
        private async Task<object> GetMonitorValueAsync()
        {
            if (_parameter.MonitorSourceType == MonitorSourceType.Variable)
            {
                // 从全局变量获取值
                var variable = _variableManager.GetAllVariables()
                    .FirstOrDefault(v => v.VarName == _parameter.MonitorVariable);

                return variable?.VarValue;
            }
            else // PLC
            {
                // 从PLC读取值
                return await _plcManager.ReadPLCValueAsync(
                    _parameter.PlcModuleName,
                    _parameter.PlcAddress);
            }
        }

        /// <summary>
        /// 格式化数值显示
        /// </summary>
        private string FormatValue(object value)
        {
            if (value == null) return "N/A";

            // 尝试转换为数值并格式化
            if (double.TryParse(value.ToString(), out double numValue))
            {
                return numValue.ToString(_parameter.DisplayFormat);
            }

            return value.ToString();
        }

        /// <summary>
        /// 更新数值显示（线程安全）
        /// </summary>
        private void UpdateValueDisplay(string text, Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateValueDisplay(text, color)));
                return;
            }

            if (lblValue != null && !lblValue.IsDisposed)
            {
                lblValue.Text = text;
                lblValue.ForeColor = color;
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void Form_RealtimeMonitorPrompt_Load(object sender, EventArgs e)
        {
            // 窗体加载完成后的初始化
            CenterToScreen();
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_RealtimeMonitorPrompt_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 停止并释放定时器
            if (_refreshTimer != null)
            {
                _refreshTimer.Stop();
                _refreshTimer.Dispose();
                _refreshTimer = null;
            }
        }

        #endregion
    }
}
using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 延时参数配置表单
    /// 功能：
    /// 1. 支持毫秒、秒、分钟三种时间单位
    /// 2. 支持使用变量/表达式作为延时值
    /// 3. 实时预览计算结果
    /// </summary>
    public partial class Form_DelayTime : BaseParameterForm
    {
        #region 常量

        /// <summary>
        /// 默认延时时间（毫秒）
        /// </summary>
        private const double DEFAULT_DELAY_TIME = 1000.0;

        /// <summary>
        /// 时间单位选项
        /// </summary>
        private static readonly string[] TIME_UNIT_OPTIONS = { "毫秒", "秒", "分钟" };

        #endregion

        #region 私有字段

        private Parameter_DelayTime _parameter;
        private bool _isInitializing = false;

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_DelayTime Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_DelayTime();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 无参构造函数 - 供设计器使用
        /// </summary>
        public Form_DelayTime()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                InitializeForm();
            }
        }

        /// <summary>
        /// 依赖注入构造函数 - 推荐在运行时使用
        /// </summary>
        /// <param name="workflowState">工作流状态服务</param>
        /// <param name="logger">日志记录器</param>
        public Form_DelayTime(IWorkflowStateService workflowState, ILogger<Form_DelayTime> logger)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化表单
        /// </summary>
        private void InitializeForm()
        {
            if (DesignMode) return;

            _isInitializing = true;

            try
            {
                InitializeTimeUnitComboBox();
                SetupExpressionInputPanel();
                BindEvents();
                LoadParameterToForm();
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 初始化时间单位下拉框
        /// </summary>
        private void InitializeTimeUnitComboBox()
        {
            if (cmbTimeUnit == null) return;

            cmbTimeUnit.Items.Clear();
            cmbTimeUnit.Items.AddRange(TIME_UNIT_OPTIONS);
            cmbTimeUnit.SelectedIndex = 0; // 默认毫秒
        }

        /// <summary>
        /// 设置表达式输入面板
        /// </summary>
        private void SetupExpressionInputPanel()
        {
            try
            {
                if (txtDelayValue == null) return;

                // 附加表达式输入面板 - 支持变量和常量
                ExpressionInputPanel.AttachTo(txtDelayValue, new InputPanelOptions
                {
                    Mode = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title = "延时时间",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true
                });

                txtDelayValue.Watermark = "点击输入延时值，支持数值或 {变量名} (按F2打开面板)";

                Logger?.LogDebug("延时表达式输入面板设置完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "设置表达式输入面板失败");
            }
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            if (BtnSave != null)
            {
                BtnSave.Click += OnSaveClick;
            }

            if (btnCancel != null)
            {
                btnCancel.Click += BtnCancel_Click;
            }

            if (txtDelayValue != null)
            {
                txtDelayValue.TextChanged += OnDelayValueChanged;
                txtDelayValue.Leave += OnDelayValueLeave;
            }

            if (cmbTimeUnit != null)
            {
                cmbTimeUnit.SelectedIndexChanged += OnTimeUnitChanged;
            }
        }

        #endregion

        #region 重写基类方法

        /// <summary>
        /// 保存界面数据到参数对象
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                _parameter ??= new Parameter_DelayTime();

                // 保存延时值（支持表达式）
                _parameter.DelayValue = txtDelayValue?.Text?.Trim() ?? "1000";

                // 保存时间单位
                if (cmbTimeUnit?.SelectedItem != null)
                {
                    _parameter.Unit = Parameter_DelayTime.GetUnitFromDisplayName(
                        cmbTimeUnit.SelectedItem.ToString());
                }

                // 如果是纯数值，同步更新 T 属性（向后兼容）
                if (double.TryParse(_parameter.DelayValue, out double value))
                {
                    // T 存储的是毫秒数
                    _parameter.T = _parameter.ConvertToMilliseconds(value);
                }

                Logger?.LogDebug($"保存延时参数: {_parameter.DelayValue} {_parameter.Unit}");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存界面数据到参数对象失败");
                throw;
            }
        }

        /// <summary>
        /// 加载参数到界面
        /// </summary>
        protected override void LoadParameterToForm()
        {
            try
            {
                _isInitializing = true;

                Parameter ??= new Parameter_DelayTime();

                // 加载延时值
                if (txtDelayValue != null)
                {
                    // 优先使用 DelayValue
                    if (!string.IsNullOrEmpty(Parameter.DelayValue))
                    {
                        string displayValue = Parameter.DelayValue;

                        // 检测是否需要反向换算（DelayValue 被 T 覆盖的情况）
                        // 如果 DelayValue 是纯数值，且等于 T，且单位不是毫秒，则需要反算
                        if (double.TryParse(displayValue, out double numValue)
                            && Math.Abs(numValue - Parameter.T) < 0.001
                            && Parameter.Unit != TimeUnit.Milliseconds)
                        {
                            // 反向换算：从毫秒转回原始单位
                            double originalValue = Parameter.Unit switch
                            {
                                TimeUnit.Seconds => numValue / 1000,
                                TimeUnit.Minutes => numValue / 60000,
                                _ => numValue
                            };
                            displayValue = originalValue.ToString("G");
                        }

                        txtDelayValue.Text = displayValue;
                    }
                    else if (Parameter.T > 0)
                    {
                        txtDelayValue.Text = Parameter.T.ToString();
                    }
                    else
                    {
                        txtDelayValue.Text = DEFAULT_DELAY_TIME.ToString();
                    }
                }

                // 加载时间单位
                if (cmbTimeUnit != null)
                {
                    string unitDisplay = Parameter_DelayTime.GetUnitDisplayName(Parameter.Unit);
                    int index = Array.IndexOf(TIME_UNIT_OPTIONS, unitDisplay);
                    cmbTimeUnit.SelectedIndex = index >= 0 ? index : 0;
                }

                // 更新预览
                UpdatePreview();

                Logger?.LogDebug($"加载延时参数: {Parameter.DelayValue} {Parameter.Unit}");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数到界面失败");
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 验证输入数据的有效性
        /// </summary>
        protected override bool ValidateInput()
        {
            try
            {
                // 检查延时值是否为空
                if (string.IsNullOrWhiteSpace(txtDelayValue?.Text))
                {
                    MessageHelper.MessageOK("请输入需要延时的时间！", TType.Warn);
                    txtDelayValue?.Focus();
                    return false;
                }

                string delayValue = txtDelayValue.Text.Trim();

                // 检查是否包含变量引用
                bool containsVariables = delayValue.Contains("{") && delayValue.Contains("}");

                if (!containsVariables)
                {
                    // 如果不是变量表达式，则必须是有效的数值
                    if (!double.TryParse(delayValue, out double value))
                    {
                        MessageHelper.MessageOK("延时值必须是有效的数值或变量表达式！", TType.Warn);
                        txtDelayValue.Focus();
                        return false;
                    }

                    // 检查数值范围
                    if (value < 0)
                    {
                        MessageHelper.MessageOK("延时值不能为负数！", TType.Warn);
                        txtDelayValue.Focus();
                        return false;
                    }

                    // 计算实际毫秒数，检查是否过大
                    TimeUnit unit = GetSelectedTimeUnit();
                    double milliseconds = ConvertToMilliseconds(value, unit);

                    if (milliseconds > 86400000) // 超过24小时
                    {
                        var result = MessageHelper.MessageYes(this,
                            $"延时时间超过24小时 ({milliseconds / 3600000:F1} 小时)，确定要继续吗？",
                            TType.Warn);
                        if (result != DialogResult.Yes)
                        {
                            txtDelayValue.Focus();
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入失败");
                return false;
            }
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            Parameter = Parameter_DelayTime.CreateDefault();
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void OnSaveClick(object sender, EventArgs e)
        {
            if (DesignMode) return;
            SaveParameters();
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 延时值改变事件
        /// </summary>
        private void OnDelayValueChanged(object sender, EventArgs e)
        {
            if (DesignMode || _isInitializing) return;
            UpdatePreview();
        }

        /// <summary>
        /// 延时值输入框失去焦点
        /// </summary>
        private void OnDelayValueLeave(object sender, EventArgs e)
        {
            if (DesignMode || _isInitializing || !IsServiceAvailable) return;

            // 更新预览
            UpdatePreview();
        }

        /// <summary>
        /// 时间单位改变事件
        /// </summary>
        private void OnTimeUnitChanged(object sender, EventArgs e)
        {
            if (DesignMode || _isInitializing) return;
            UpdatePreview();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取当前选择的时间单位
        /// </summary>
        private TimeUnit GetSelectedTimeUnit()
        {
            if (cmbTimeUnit?.SelectedItem == null)
                return TimeUnit.Milliseconds;

            return Parameter_DelayTime.GetUnitFromDisplayName(
                cmbTimeUnit.SelectedItem.ToString());
        }

        /// <summary>
        /// 将时间值转换为毫秒
        /// </summary>
        private double ConvertToMilliseconds(double value, TimeUnit unit)
        {
            return unit switch
            {
                TimeUnit.Milliseconds => value,
                TimeUnit.Seconds => value * 1000,
                TimeUnit.Minutes => value * 60000,
                _ => value
            };
        }

        /// <summary>
        /// 更新预览显示
        /// </summary>
        private void UpdatePreview()
        {
            if (lblPreviewValue == null) return;

            try
            {
                string delayValue = txtDelayValue?.Text?.Trim() ?? "0";
                TimeUnit unit = GetSelectedTimeUnit();

                // 检查是否包含变量
                bool containsVariables = delayValue.Contains("{") && delayValue.Contains("}");

                if (containsVariables)
                {
                    // 变量表达式 - 显示为表达式
                    lblPreviewValue.Text = $"⚡ 运行时计算: {delayValue}";
                    lblPreviewValue.ForeColor = Color.FromArgb(24, 144, 255); // 蓝色
                    lblPreviewValue.BackColor = Color.FromArgb(230, 244, 255);
                }
                else if (double.TryParse(delayValue, out double value))
                {
                    // 数值 - 计算实际毫秒数
                    double milliseconds = ConvertToMilliseconds(value, unit);
                    string displayText = FormatMillisecondsDisplay(milliseconds);

                    lblPreviewValue.Text = $"✓ {displayText}";
                    lblPreviewValue.ForeColor = Color.FromArgb(82, 196, 26); // 绿色
                    lblPreviewValue.BackColor = Color.FromArgb(246, 255, 237);
                }
                else
                {
                    // 无效输入
                    lblPreviewValue.Text = "⚠ 请输入有效的数值或变量";
                    lblPreviewValue.ForeColor = Color.FromArgb(255, 77, 79); // 红色
                    lblPreviewValue.BackColor = Color.FromArgb(255, 241, 240);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "更新预览失败");
                lblPreviewValue.Text = "⚠ 预览失败";
                lblPreviewValue.ForeColor = Color.FromArgb(255, 77, 79);
                lblPreviewValue.BackColor = Color.FromArgb(255, 241, 240);
            }
        }

        /// <summary>
        /// 格式化毫秒数的显示文本
        /// </summary>
        private string FormatMillisecondsDisplay(double milliseconds)
        {
            if (milliseconds >= 3600000)
            {
                // 超过1小时
                double hours = milliseconds / 3600000;
                return $"{hours:F2} 小时 ({milliseconds:N0} 毫秒)";
            }
            else if (milliseconds >= 60000)
            {
                // 超过1分钟
                double minutes = milliseconds / 60000;
                return $"{minutes:F2} 分钟 ({milliseconds:N0} 毫秒)";
            }
            else if (milliseconds >= 1000)
            {
                // 超过1秒
                double seconds = milliseconds / 1000;
                return $"{seconds:F2} 秒 ({milliseconds:N0} 毫秒)";
            }
            else
            {
                return $"{milliseconds:N0} 毫秒";
            }
        }

        #endregion
    }
}
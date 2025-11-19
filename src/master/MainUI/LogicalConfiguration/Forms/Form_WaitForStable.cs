using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 等待变量稳定配置窗体
    /// </summary>
    public partial class Form_WaitForStable : UIForm
    {
        #region 字段

        private readonly IWorkflowStateService _workflowState;
        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger<Form_WaitForStable> _logger;
        private int _currentStepIndex;
        private Parameter_WaitForStable _parameter;
        private bool _hasUnsavedChanges = false;

        #endregion

        #region 构造函数

        public Form_WaitForStable(
            IWorkflowStateService workflowState,
            GlobalVariableManager variableManager,
            ILogger<Form_WaitForStable> logger)
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            InitializeComponent();
            InitializeForm();

            _logger.LogDebug("Form_WaitForStable 初始化完成");
        }

        #endregion

        #region 初始化方法

        private void InitializeForm()
        {
            try
            {
                // 获取当前步骤信息
                _currentStepIndex = _workflowState.StepNum;

                // 加载当前步骤的参数
                LoadCurrentStepParameter();

                // 加载可用变量到下拉框
                LoadAvailableVariables();

                // 初始化超时动作下拉框
                InitializeTimeoutActionComboBox();

                // 注册控件事件
                RegisterControlEvents();

                _logger.LogDebug("窗体初始化完成，当前步骤: {StepIndex}", _currentStepIndex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化窗体时发生错误");
                MessageHelper.MessageOK(this, $"初始化失败: {ex.Message}", TType.Error);
            }
        }

        private void LoadCurrentStepParameter()
        {
            try
            {
                var currentStep = _workflowState.GetStep(_currentStepIndex);
                if (currentStep?.StepParameter != null)
                {
                    if (currentStep.StepParameter is Parameter_WaitForStable param)
                    {
                        _parameter = param;
                    }
                    else
                    {
                        var jsonString = currentStep.StepParameter.ToString();
                        _parameter = JsonConvert.DeserializeObject<Parameter_WaitForStable>(jsonString)
                            ?? new Parameter_WaitForStable();
                    }
                }
                else
                {
                    _parameter = new Parameter_WaitForStable();
                }

                LoadParameterToForm();
                _logger.LogDebug("加载步骤参数完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载步骤参数时发生错误");
                _parameter = new Parameter_WaitForStable();
            }
        }

        private void LoadAvailableVariables()
        {
            try
            {
                var variables = _variableManager.GetAllVariables()
                    .Select(v => v.VarName)
                    .ToList();

                cmbMonitorVariable.Items.Clear();
                cmbMonitorVariable.Items.AddRange(variables.ToArray());

                cmbAssignToVariable.Items.Clear();
                cmbAssignToVariable.Items.Add(""); // 空选项表示不赋值
                cmbAssignToVariable.Items.AddRange(variables.ToArray());

                _logger.LogDebug("加载了 {Count} 个变量", variables.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载变量列表时发生错误");
            }
        }

        private void InitializeTimeoutActionComboBox()
        {
            cmbTimeoutAction.Items.Clear();
            cmbTimeoutAction.Items.Add(new { Text = "继续执行并记录日志", Value = TimeoutAction.ContinueAndLog });
            cmbTimeoutAction.Items.Add(new { Text = "停止整个流程", Value = TimeoutAction.StopProcedure });
            cmbTimeoutAction.Items.Add(new { Text = "跳转到指定步骤", Value = TimeoutAction.JumpToStep });
            cmbTimeoutAction.DisplayMember = "Text";
            cmbTimeoutAction.ValueMember = "Value";
            cmbTimeoutAction.SelectedIndex = 0;
        }

        private void RegisterControlEvents()
        {
            // 文本变更事件
            txtDescription.TextChanged += OnParameterChanged;
            cmbMonitorVariable.SelectedIndexChanged += OnParameterChanged;
            numStabilityThreshold.ValueChanged += OnParameterChanged;
            numSamplingInterval.ValueChanged += OnParameterChanged;
            numStableCount.ValueChanged += OnParameterChanged;
            numTimeout.ValueChanged += OnParameterChanged;
            cmbAssignToVariable.SelectedIndexChanged += OnParameterChanged;
            cmbTimeoutAction.SelectedIndexChanged += OnTimeoutActionChanged;
            numTimeoutJumpStep.ValueChanged += OnParameterChanged;

            // 按钮事件
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            btnTest.Click += BtnTest_Click;
            btnHelp.Click += BtnHelp_Click;
        }

        private void OnParameterChanged(object sender, string value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 参数加载和保存

        private void LoadParameterToForm()
        {
            txtDescription.Text = _parameter.Description;
            cmbMonitorVariable.Text = _parameter.MonitorVariable;
            numStabilityThreshold.Value = (decimal)_parameter.StabilityThreshold;
            numSamplingInterval.Text = _parameter.SamplingInterval.ToString();
            numStableCount.Value = _parameter.StableCount;
            numTimeout.Value = _parameter.TimeoutSeconds;
            cmbAssignToVariable.Text = _parameter.AssignToVariable;
            cmbTimeoutAction.SelectedValue = _parameter.OnTimeout;
            numTimeoutJumpStep.Value = _parameter.TimeoutJumpToStep;

            // 根据超时动作显示/隐藏跳转步骤号控件
            UpdateTimeoutJumpStepVisibility();

            _hasUnsavedChanges = false;
        }

        private bool SaveParameterFromForm()
        {
            try
            {
                // 验证必填项
                if (string.IsNullOrWhiteSpace(cmbMonitorVariable.Text))
                {
                    MessageHelper.MessageOK(this, "请选择要监测的变量", TType.Warn);
                    cmbMonitorVariable.Focus();
                    return false;
                }

                // 验证变量是否存在
                if (!_variableManager.GetAllVariables().Any(v => v.VarName == cmbMonitorVariable.Text))
                {
                    MessageHelper.MessageOK(this, $"变量 '{cmbMonitorVariable.Text}' 不存在", TType.Warn);
                    return false;
                }

                // 验证赋值目标变量
                if (!string.IsNullOrWhiteSpace(cmbAssignToVariable.Text))
                {
                    if (!_variableManager.GetAllVariables().Any(v => v.VarName == cmbAssignToVariable.Text))
                    {
                        MessageHelper.MessageOK(this, $"目标变量 '{cmbAssignToVariable.Text}' 不存在", TType.Warn);
                        return false;
                    }
                }

                // 保存参数
                _parameter.Description = txtDescription.Text.Trim();
                _parameter.MonitorVariable = cmbMonitorVariable.Text.Trim();
                _parameter.StabilityThreshold = (double)numStabilityThreshold.Value;
                _parameter.SamplingInterval = numSamplingInterval.Text.ToInt();
                _parameter.StableCount = (int)numStableCount.Value;
                _parameter.TimeoutSeconds = (int)numTimeout.Value;
                _parameter.AssignToVariable = cmbAssignToVariable.Text.Trim();
                _parameter.OnTimeout = (TimeoutAction)cmbTimeoutAction.SelectedValue;
                _parameter.TimeoutJumpToStep = (int)numTimeoutJumpStep.Value;

                // 保存到步骤
                var currentStep = _workflowState.GetStep(_currentStepIndex);
                if (currentStep != null)
                {
                    currentStep.StepParameter = _parameter;
                    _hasUnsavedChanges = false;
                    _logger.LogInformation("参数已保存");
                    return true;
                }
                else
                {
                    _logger.LogError("无法找到当前步骤");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存参数时发生错误");
                MessageHelper.MessageOK(this, $"保存失败: {ex.Message}", TType.Error);
                return false;
            }
        }

        #endregion

        #region 事件处理

        private void OnParameterChanged(object sender, EventArgs e)
        {
            _hasUnsavedChanges = true;
        }

        private void OnTimeoutActionChanged(object sender, EventArgs e)
        {
            UpdateTimeoutJumpStepVisibility();
            _hasUnsavedChanges = true;
        }

        private void UpdateTimeoutJumpStepVisibility()
        {
            bool showJumpStep = cmbTimeoutAction.SelectedValue != null &&
                                (TimeoutAction)cmbTimeoutAction.SelectedValue == TimeoutAction.JumpToStep;

            lblTimeoutJumpStep.Visible = showJumpStep;
            numTimeoutJumpStep.Visible = showJumpStep;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (SaveParameterFromForm())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes(this, "有未保存的更改，确定要放弃吗?");
                if (result != DialogResult.OK)
                {
                    return;
                }
            }

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证配置
                if (string.IsNullOrWhiteSpace(cmbMonitorVariable.Text))
                {
                    MessageHelper.MessageOK(this, "请先选择要监测的变量", TType.Warn);
                    return;
                }

                // 获取当前变量值
                var variable = _variableManager.GetAllVariables()
                    .FirstOrDefault(v => v.VarName == cmbMonitorVariable.Text);

                if (variable == null)
                {
                    MessageHelper.MessageOK(this, "变量不存在", TType.Error);
                    return;
                }

                // 尝试转换为数值
                if (double.TryParse(variable.VarValue?.ToString(), out double currentValue))
                {
                    string testInfo = $"当前配置测试:\n\n" +
                                    $"监测变量: {cmbMonitorVariable.Text}\n" +
                                    $"当前值: {currentValue:F2}\n" +
                                    $"稳定阈值: {numStabilityThreshold.Value} (单位/秒)\n" +
                                    $"采样间隔: {numSamplingInterval.Text} 秒\n" +
                                    $"连续稳定次数: {numStableCount.Value} 次\n" +
                                    $"超时时间: {(numTimeout.Value == 0 ? "无限等待" : $"{numTimeout.Value} 秒")}\n" +
                                    $"赋值目标: {(string.IsNullOrWhiteSpace(cmbAssignToVariable.Text) ? "不赋值" : cmbAssignToVariable.Text)}\n\n" +
                                    $"说明: 当变化率 ≤ {numStabilityThreshold.Value} 且持续 {numStableCount.Value} 次时判定为稳定";

                    MessageHelper.MessageOK(this, testInfo, TType.Info);
                }
                else
                {
                    MessageHelper.MessageOK(this, "变量值无法转换为数值类型", TType.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "测试配置时发生错误");
                MessageHelper.MessageOK(this, $"测试失败: {ex.Message}", TType.Error);
            }
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            string helpText = @"等待变量稳定 - 使用说明
                本步骤用于监测变量值的变化，当变化率小于设定阈值且持续一定次数后，认为变量已稳定。
                配置说明:
                
                1. 基本配置:
                   - 步骤描述: 为此步骤指定一个说明
                   - 监测变量: 选择要监测的变量（必填）
                
                2. 稳定判据:
                   - 稳定阈值: 变化率阈值（单位/秒），当 |当前值-上次值|/采样间隔 ≤ 此值时认为稳定
                   - 采样间隔: 每隔多少秒采样一次（建议1-5秒）
                   - 连续稳定次数: 连续多少次采样满足条件才算真正稳定（用于过滤偶然波动）
                
                3. 超时配置:
                   - 超时时间: 最长等待时间（秒），0表示无限等待
                   - 超时动作: 
                     * 继续执行并记录日志 - 超时后继续下一步
                     * 停止整个流程 - 超时后终止执行
                     * 跳转到指定步骤 - 超时后跳转到指定步骤号
                
                4. 结果处理:
                   - 赋值目标变量: 稳定后将当前值赋给此变量（可选）
                
                使用场景:
                - 等待压力稳定后记录压力值
                - 等待温度稳定后进行测量
                - 等待流量稳定后计算流量值
                
                注意事项:
                - 监测的变量必须是数值类型
                - 采样间隔不宜过短，避免频繁采样
                - 连续稳定次数建议3-5次";

            MessageHelper.MessageOK(this, helpText, TType.Info);
        }

        #endregion
    }
}
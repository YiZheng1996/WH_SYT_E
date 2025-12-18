using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 检测配置表单 - 表达式化版本
    /// 使用统一的表达式进行检测条件配置
    /// </summary>
    public partial class Form_Detection : BaseParameterForm
    {
        #region 私有字段

        private Parameter_Detection _parameter;
        private ExpressionEngine _expressionEngine;
        private bool _isInitializing = true;
        private bool _hasUnsavedChanges = false;
        private System.Windows.Forms.Timer _validationTimer;

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象
        /// </summary>
        public Parameter_Detection Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_Detection();
                if (!DesignMode && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 设计器构造函数
        /// </summary>
        public Form_Detection()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                InitializeForm();
            }
        }

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        public Form_Detection(ILogger<Form_Detection> logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// 带参数的依赖注入构造函数
        /// </summary>
        public Form_Detection(Parameter_Detection parameter)
        {
            InitializeComponent();
            _parameter = parameter ?? new Parameter_Detection();
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化表单
        /// </summary>
        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 获取表达式引擎（用于统一验证）
                _expressionEngine = Program.ServiceProvider?.GetService<ExpressionEngine>();

                // 初始化参数
                _parameter ??= new Parameter_Detection();

                // 初始化验证定时器
                InitializeValidationTimer();

                // 初始化下拉框
                InitializeComboBoxes();

                // 初始化表达式输入面板
                InitializeExpressionPanel();

                // 设置事件处理器
                SetupEventHandlers();

                _isInitializing = false;
                _logger?.LogInformation("检测工具窗体初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化表单时发生错误");
                MessageHelper.MessageOK(this, $"初始化失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 初始化验证定时器
        /// </summary>
        private void InitializeValidationTimer()
        {
            _validationTimer = new System.Windows.Forms.Timer
            {
                Interval = 500
            };
            _validationTimer.Tick += ValidationTimer_Tick;
        }

        /// <summary>
        /// 加载全局变量到下拉框
        /// </summary>
        private void LoadVariables()
        {
            if (_globalVariable == null) return;

            var variables = _globalVariable.GetAllUserVariables()
                .Select(v => v.VarName)
                .OrderBy(n => n)
                .ToList();

            cmbResultVariable.Items.Clear();
            cmbResultVariable.Items.AddRange([.. variables]);
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitializeComboBoxes()
        {
            try
            {
                // 失败处理
                cmbFailureAction.Items.Clear();
                cmbFailureAction.Items.Add("继续执行");
                cmbFailureAction.Items.Add("停止流程");
                cmbFailureAction.Items.Add("跳转到指定步骤");
                //cmbFailureAction.Items.Add("重试");
                cmbFailureAction.SelectedIndex = 0;

                LoadVariables();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化下拉框失败");
            }
        }

        /// <summary>
        /// 初始化表达式输入面板
        /// </summary>
        private void InitializeExpressionPanel()
        {
            try
            {
                // 为条件表达式文本框附加ExpressionInputPanel
                ExpressionInputPanel.AttachTo(txtConditionExpression, new InputPanelOptions
                {
                    Mode = InputMode.Expression,
                    EnabledModules = InputModules.All,
                    Title = "配置检测条件表达式",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true
                });

                _logger?.LogDebug("表达式输入面板初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化表达式输入面板失败");
            }
        }

        /// <summary>
        /// 设置事件处理器
        /// </summary>
        private void SetupEventHandlers()
        {
            // 表达式变化
            txtConditionExpression.TextChanged += (s, e) =>
            {
                if (!_isInitializing)
                {
                    _validationTimer.Stop();
                    _validationTimer.Start();
                    MarkAsChanged();
                }
            };

            // 保存结果复选框
            chkSaveResult.CheckedChanged += (s, e) =>
            {
                cmbResultVariable.Enabled = chkSaveResult.Checked;
                MarkAsChanged();
            };

            // 保存值复选框
            chkSaveValue.CheckedChanged += (s, e) =>
            {
                cmbValueVariable.Enabled = chkSaveValue.Checked;
                MarkAsChanged();
            };

            // 保存按钮
            btnSave.Click += BtnSave_Click;

            // 取消按钮
            btnCancel.Click += BtnCancel_Click;
        }

        #endregion

        #region UI更新方法

        /// <summary>
        /// 更新验证状态显示 - 使用ExpressionEngine统一验证
        /// </summary>
        private void UpdateValidationStatus()
        {
            string expression = txtConditionExpression.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(expression))
            {
                lblValidationStatus.Text = "准备就绪";
                lblValidationStatus.ForeColor = Color.Gray;
                return;
            }

            try
            {
                if (_expressionEngine != null)
                {
                    // 创建验证上下文（与ExpressionInputPanel使用相同的验证方式）
                    var validationContext = new ValidationContext
                    {
                        ValidationLabel = "检测条件",
                        AllowFunctionCalls = true,
                        AllowPlcReferences = true,
                        StrictMode = false,
                        RuntimeVariableWhitelist = GetRuntimeVariableWhitelist()
                    };

                    // 使用ExpressionEngine进行验证
                    var result = _expressionEngine.ValidateExpression(expression, validationContext);

                    if (result.IsValid)
                    {
                        string message = "✓ 表达式语法有效";

                        // 如果有警告，显示警告信息
                        if (result.HasWarnings)
                        {
                            message = $"✓ 有效 (警告: {string.Join("; ", result.Warnings)})";
                            lblValidationStatus.ForeColor = Color.FromArgb(255, 165, 0); // 橙色
                        }
                        else
                        {
                            lblValidationStatus.ForeColor = Color.FromArgb(40, 167, 69); // 绿色
                        }

                        lblValidationStatus.Text = message;
                    }
                    else
                    {
                        string errorDetail = result.Errors.Count != 0
                            ? string.Join("; ", result.Errors)
                            : result.Message;
                        lblValidationStatus.Text = $"✗ {errorDetail}";
                        lblValidationStatus.ForeColor = Color.FromArgb(220, 53, 69); // 红色
                    }
                }
                else
                {
                    // 降级方案：没有表达式引擎时使用简单验证
                    bool bracketsValid = ValidateBrackets(expression);
                    if (bracketsValid)
                    {
                        lblValidationStatus.Text = "✓ 语法检查通过";
                        lblValidationStatus.ForeColor = Color.FromArgb(40, 167, 69);
                    }
                    else
                    {
                        lblValidationStatus.Text = "✗ 括号不匹配";
                        lblValidationStatus.ForeColor = Color.FromArgb(220, 53, 69);
                    }
                }
            }
            catch (Exception ex)
            {
                lblValidationStatus.Text = $"✗ 验证失败: {ex.Message}";
                lblValidationStatus.ForeColor = Color.FromArgb(220, 53, 69);
            }
        }

        /// <summary>
        /// 获取运行时变量白名单（与ExpressionInputPanel保持一致）
        /// </summary>
        private List<string> GetRuntimeVariableWhitelist()
        {
            return
            [
                // 系统日期时间
                "DateTime.Now",
                "DateTime.Today",
                "DateTime.UtcNow",
                
                // 系统环境信息
                "Environment.MachineName",
                "Environment.UserName",
                "Environment.OSVersion",
                "Environment.ProcessorCount",
                
                // 数学常量
                "Math.PI",
                "Math.E",
                
                // 检测专用
                "value"
            ];
        }

        /// <summary>
        /// 简单括号验证（降级方案）
        /// </summary>
        private bool ValidateBrackets(string expression)
        {
            int braceCount = 0;
            int parenCount = 0;

            foreach (char c in expression)
            {
                switch (c)
                {
                    case '{': braceCount++; break;
                    case '}': braceCount--; break;
                    case '(': parenCount++; break;
                    case ')': parenCount--; break;
                }

                if (braceCount < 0 || parenCount < 0)
                    return false;
            }

            return braceCount == 0 && parenCount == 0;
        }

        /// <summary>
        /// 标记为已修改
        /// </summary>
        private void MarkAsChanged()
        {
            if (!_isInitializing)
            {
                _hasUnsavedChanges = true;
            }
        }

        #endregion

        #region 事件处理器

        /// <summary>
        /// 验证定时器触发
        /// </summary>
        private void ValidationTimer_Tick(object sender, EventArgs e)
        {
            _validationTimer.Stop();
            UpdateValidationStatus();
        }

        /// <summary>
        /// 保存按钮点击
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                btnSave.Text = "处理中...";

                if (!ValidateInput())
                    return;

                SaveFormToParameter();

                SaveParameters();
                _hasUnsavedChanges = false;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "确定按钮处理时发生错误");
                MessageHelper.MessageOK($"操作失败：{ex.Message}", TType.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "确定";
            }
        }

        /// <summary>
        /// 取消按钮点击
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes(this, ("有未保存的更改，确定要取消吗？"));
                if (result != DialogResult.OK)
                {
                    return;
                }
            }
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion


        #region 参数加载/保存

        /// <summary>
        /// 加载参数到界面
        /// </summary>
        protected override void LoadParameterToForm()
        {
            if (_parameter == null) return;

            try
            {
                _isInitializing = true;

                // 基本信息
                txtDetectionName.Text = _parameter.DetectionName ?? "";

                // 检测条件
                txtConditionExpression.Text = _parameter.ConditionExpression ?? "{value} >= 0";

                // 超时重试
                numTimeoutMs.Value = _parameter.TimeoutMs;
                numRefreshRateMs.Value = _parameter.RefreshRateMs;
                numRetryCount.Value = _parameter.RetryCount;
                numRetryIntervalMs.Value = _parameter.RetryIntervalMs;

                // 结果处理
                chkSaveResult.Checked = _parameter.ResultHandling?.SaveToVariable ?? false;
                cmbResultVariable.Text = _parameter.ResultHandling?.ResultVariableName ?? "";
                cmbResultVariable.Enabled = chkSaveResult.Checked;

                chkSaveValue.Checked = _parameter.ResultHandling?.SaveValueToVariable ?? false;
                cmbValueVariable.Text = _parameter.ResultHandling?.ValueVariableName ?? "";
                cmbValueVariable.Enabled = chkSaveValue.Checked;

                cmbFailureAction.SelectedIndex = (int)(_parameter.ResultHandling?.OnFailure ?? FailureAction.Continue);

                UpdateValidationStatus();

                _hasUnsavedChanges = false;
                _logger?.LogDebug("参数加载到界面完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载参数到界面失败");
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 保存界面到参数
        /// </summary>
        protected override void SaveFormToParameter()
        {
            if (_parameter == null) return;

            try
            {
                // 基本信息
                _parameter.DetectionName = txtDetectionName.Text?.Trim() ?? "";

                // 检测条件
                _parameter.ConditionExpression = txtConditionExpression.Text?.Trim() ?? "{value} >= 0";

                // 超时重试
                _parameter.TimeoutMs = numTimeoutMs.Value;
                _parameter.RefreshRateMs = numRefreshRateMs.Value;
                _parameter.RetryCount = numRetryCount.Value;
                _parameter.RetryIntervalMs = numRetryIntervalMs.Value;

                // 结果处理
                _parameter.ResultHandling ??= new ResultHandling();
                _parameter.ResultHandling.SaveToVariable = chkSaveResult.Checked;
                _parameter.ResultHandling.ResultVariableName = cmbResultVariable.Text?.Trim() ?? "";
                _parameter.ResultHandling.SaveValueToVariable = chkSaveValue.Checked;
                _parameter.ResultHandling.ValueVariableName = cmbValueVariable.Text?.Trim() ?? "";
                _parameter.ResultHandling.OnFailure = (FailureAction)cmbFailureAction.SelectedIndex;

                _logger?.LogDebug("界面保存到参数完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存界面到参数失败");
            }
        }

        #endregion

        #region 验证

        /// <summary>
        /// 验证输入
        /// </summary>
        protected override bool ValidateInput()
        {
            try
            {
                // 验证检测名称
                if (string.IsNullOrWhiteSpace(txtDetectionName.Text))
                {
                    MessageHelper.MessageOK(this, "请输入检测项名称");
                    txtDetectionName.Focus();
                    return false;
                }

                // 验证表达式
                string expression = txtConditionExpression.Text?.Trim() ?? "";
                if (string.IsNullOrEmpty(expression))
                {
                    MessageHelper.MessageOK(this, "请输入检测条件表达式");
                    txtConditionExpression.Focus();
                    return false;
                }

                // 使用ExpressionEngine验证（与UpdateValidationStatus保持一致）
                if (_expressionEngine != null)
                {
                    var validationContext = new ValidationContext
                    {
                        ValidationLabel = "检测条件",
                        AllowFunctionCalls = true,
                        AllowPlcReferences = true,
                        StrictMode = false,
                        RuntimeVariableWhitelist = GetRuntimeVariableWhitelist()
                    };

                    var result = _expressionEngine.ValidateExpression(expression, validationContext);
                    if (!result.IsValid)
                    {
                        string errorMsg = result.Errors.Count != 0
                            ? string.Join("; ", result.Errors)
                            : result.Message;
                        MessageHelper.MessageOK(this, $"表达式无效：{errorMsg}");
                        txtConditionExpression.Focus();
                        return false;
                    }
                }

                // 验证结果处理
                if (chkSaveResult.Checked && string.IsNullOrWhiteSpace(cmbResultVariable.Text))
                {
                    MessageHelper.MessageOK(this, "启用了保存结果，但未选择结果变量");
                    cmbResultVariable.Focus();
                    return false;
                }

                if (chkSaveValue.Checked && string.IsNullOrWhiteSpace(cmbValueVariable.Text))
                {
                    MessageHelper.MessageOK(this, "启用了保存值，但未选择值变量");
                    cmbValueVariable.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证输入失败");
                MessageHelper.MessageOK(this, $"验证失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            Parameter = new Parameter_Detection
            {
                DetectionName = $"条件判断步骤 {_workflowState?.StepNum + 1}"
            };

            Logger?.LogDebug("设置条件判断参数默认值");
            LoadParameterToForm();
        }

        #endregion
    }
}
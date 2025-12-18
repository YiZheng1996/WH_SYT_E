using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 检测配置表单 - 表达式化版本
    /// 使用统一的表达式进行检测条件配置
    /// </summary>
    public partial class Form_Detection : UIForm
    {
        #region 私有字段

        private Parameter_Detection _parameter;
        private ExpressionEngine _expressionEngine;
        private GlobalVariableManager _variableManager;
        private IWorkflowStateService _workflowState;
        private ILogger<Form_Detection> _logger;
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
        public Form_Detection(IWorkflowStateService workflowState, ILogger<Form_Detection> logger)
        {
            _workflowState = workflowState;
            _logger = logger;
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// 带参数的依赖注入构造函数
        /// </summary>
        public Form_Detection(IWorkflowStateService workflowState, ILogger<Form_Detection> logger, Parameter_Detection parameter)
        {
            _workflowState = workflowState;
            _logger = logger;
            InitializeComponent();
            _parameter = parameter ?? new Parameter_Detection();
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化表单
        /// </summary>
        private async void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 获取服务
                _expressionEngine = Program.ServiceProvider?.GetService<ExpressionEngine>();
                _variableManager = Program.ServiceProvider?.GetService<GlobalVariableManager>();
                _workflowState ??= Program.ServiceProvider?.GetService<IWorkflowStateService>();

                // 初始化参数
                _parameter ??= new Parameter_Detection();

                // 初始化验证定时器
                InitializeValidationTimer();

                // 初始化下拉框
                InitializeComboBoxes();

                // 初始化变量下拉框
                await InitializeVariableComboBoxes();

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
                UIMessageBox.ShowError($"初始化失败：{ex.Message}");
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
        /// 初始化下拉框
        /// </summary>
        private void InitializeComboBoxes()
        {
            try
            {
                // 数据源类型
                cmbDataSourceType.Items.Clear();
                cmbDataSourceType.Items.Add("系统变量");
                cmbDataSourceType.Items.Add("PLC地址");
                cmbDataSourceType.SelectedIndex = 0;

                // 失败处理
                cmbFailureAction.Items.Clear();
                cmbFailureAction.Items.Add("继续执行");
                cmbFailureAction.Items.Add("停止流程");
                cmbFailureAction.Items.Add("跳转到指定步骤");
                cmbFailureAction.Items.Add("重试");
                cmbFailureAction.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化下拉框失败");
            }
        }

        /// <summary>
        /// 初始化变量下拉框
        /// </summary>
        private async Task InitializeVariableComboBoxes()
        {
            try
            {
                if (_variableManager == null) return;

                // 获取所有变量名
                var variableNames = _variableManager.GetAllVariables()
                    .Select(v => v.VarName)
                    .OrderBy(n => n)
                    .ToList();

                // 数据源变量
                cmbVariableName.Items.Clear();
                foreach (var name in variableNames)
                {
                    cmbVariableName.Items.Add(name);
                }

                // 结果变量
                cmbResultVariable.Items.Clear();
                foreach (var name in variableNames)
                {
                    cmbResultVariable.Items.Add(name);
                }

                // 值变量
                cmbValueVariable.Items.Clear();
                foreach (var name in variableNames)
                {
                    cmbValueVariable.Items.Add(name);
                }

                // PLC模块
                var plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
                if (plcManager != null)
                {
                    var modules = await plcManager.GetModuleTagsAsync();
                    cmbPlcModule.Items.Clear();
                    foreach (var module in modules.Keys)
                    {
                        cmbPlcModule.Items.Add(module);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化变量下拉框失败");
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
            // 数据源类型变化
            cmbDataSourceType.SelectedIndexChanged += (s, e) =>
            {
                if (!_isInitializing)
                {
                    UpdateDataSourceUI();
                    MarkAsChanged();
                }
            };

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

            // 模板按钮
            btnSelectTemplate.Click += BtnSelectTemplate_Click;

            // 测试按钮
            btnTestExpression.Click += BtnTestExpression_Click;

            // 保存按钮
            btnSave.Click += BtnSave_Click;

            // 取消按钮
            btnCancel.Click += BtnCancel_Click;

            // PLC模块选择变化时更新地址列表
            cmbPlcModule.SelectedIndexChanged += async (s, e) =>
            {
                if (!_isInitializing && cmbPlcModule.SelectedItem != null)
                {
                    await UpdatePlcAddresses();
                    MarkAsChanged();
                }
            };
        }

        #endregion

        #region UI更新方法

        /// <summary>
        /// 根据数据源类型更新UI
        /// </summary>
        private void UpdateDataSourceUI()
        {
            bool isVariable = cmbDataSourceType.SelectedIndex == 0;

            // 变量相关控件
            lblVariableName.Visible = isVariable;
            cmbVariableName.Visible = isVariable;

            // PLC相关控件
            lblPlcModule.Visible = !isVariable;
            cmbPlcModule.Visible = !isVariable;
            lblPlcAddress.Visible = !isVariable;
            cmbPlcAddress.Visible = !isVariable;

            // 调整面板高度
            panelDataSource.Height = isVariable ? 85 : 125;
        }

        /// <summary>
        /// 更新PLC地址列表
        /// </summary>
        private async Task UpdatePlcAddresses()
        {
            try
            {
                var plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
                if (plcManager == null || cmbPlcModule.SelectedItem == null) return;

                string moduleName = cmbPlcModule.SelectedItem.ToString();
                var modules = await plcManager.GetModuleTagsAsync();

                cmbPlcAddress.Items.Clear();
                if (modules.TryGetValue(moduleName, out var tags))
                {
                    foreach (var tag in tags)
                    {
                        cmbPlcAddress.Items.Add(tag);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新PLC地址列表失败");
            }
        }

        /// <summary>
        /// 更新验证状态显示
        /// </summary>
        private void UpdateValidationStatus()
        {
            string expression = txtConditionExpression.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(expression))
            {
                lblValidationStatus.Text = "";
                lblValidationStatus.ForeColor = Color.Gray;
                return;
            }

            var (isValid, message) = DetectionExpressionHelper.ValidateConditionExpression(expression);

            if (isValid)
            {
                string typeDesc = DetectionExpressionHelper.GetExpressionTypeDescription(expression);
                lblValidationStatus.Text = $"✅ {typeDesc}";
                lblValidationStatus.ForeColor = Color.FromArgb(40, 167, 69);
            }
            else
            {
                lblValidationStatus.Text = $"❌ {message}";
                lblValidationStatus.ForeColor = Color.FromArgb(220, 53, 69);
            }
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
        /// 选择模板按钮点击
        /// </summary>
        private void BtnSelectTemplate_Click(object sender, EventArgs e)
        {
            ShowTemplateMenu();
        }

        /// <summary>
        /// 测试表达式按钮点击
        /// </summary>
        private async void BtnTestExpression_Click(object sender, EventArgs e)
        {
            await TestExpression();
        }

        /// <summary>
        /// 保存按钮点击
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                SaveFormToParameter();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// 取消按钮点击
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = UIMessageBox.ShowAsk("有未保存的更改，确定要取消吗？");
                if (!result)
                {
                    return;
                }
            }
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region 模板功能

        /// <summary>
        /// 显示模板选择菜单
        /// </summary>
        private void ShowTemplateMenu()
        {
            var contextMenu = new ContextMenuStrip();
            var templatesByCategory = ExpressionTemplates.GetTemplatesByCategory();

            foreach (var category in templatesByCategory)
            {
                // 添加分类标题
                var categoryItem = new ToolStripMenuItem(category.Key)
                {
                    Enabled = false,
                    Font = new Font("微软雅黑", 9F, FontStyle.Bold)
                };
                contextMenu.Items.Add(categoryItem);

                // 添加该分类下的模板
                foreach (var template in category.Value)
                {
                    var item = new ToolStripMenuItem($"{template.Icon} {template.Name}")
                    {
                        ToolTipText = $"{template.Description}\n表达式: {template.Expression}",
                        Tag = template
                    };
                    item.Click += (s, e) => ApplyTemplate(template);
                    contextMenu.Items.Add(item);
                }

                contextMenu.Items.Add(new ToolStripSeparator());
            }

            contextMenu.Show(btnSelectTemplate, new Point(0, btnSelectTemplate.Height));
        }

        /// <summary>
        /// 应用模板
        /// </summary>
        private void ApplyTemplate(ExpressionTemplate template)
        {
            if (template.Placeholders.Length > 0)
            {
                // 有占位符，提示用户填写
                string message = $"模板: {template.Name}\n" +
                                 $"表达式: {template.Expression}\n\n" +
                                 $"需要替换的占位符:\n{string.Join("\n", template.Placeholders)}\n\n" +
                                 "表达式已填入，请手动修改占位符的值。";

                txtConditionExpression.Text = template.Expression;
                UIMessageBox.ShowInfo(message);
            }
            else
            {
                txtConditionExpression.Text = template.Expression;
            }
        }

        #endregion

        #region 测试功能

        /// <summary>
        /// 测试表达式
        /// </summary>
        private async Task TestExpression()
        {
            try
            {
                string expression = txtConditionExpression.Text?.Trim() ?? "";

                if (string.IsNullOrEmpty(expression))
                {
                    UIMessageBox.ShowWarning("请先输入检测条件表达式");
                    return;
                }

                // 验证表达式
                var (isValid, message) = DetectionExpressionHelper.ValidateConditionExpression(expression);
                if (!isValid)
                {
                    UIMessageBox.ShowWarning($"表达式无效：{message}");
                    return;
                }

                // 读取数据源值
                object value = await ReadDataSourceValue();
                if (value == null)
                {
                    UIMessageBox.ShowWarning("无法读取数据源的值，请检查数据源配置");
                    return;
                }

                // 替换{value}并计算
                string evaluateExpression = expression.Replace("{value}", value.ToString());

                try
                {
                    if (_expressionEngine != null)
                    {
                        var result = await _expressionEngine.EvaluateExpressionAsync(evaluateExpression);
                        bool boolResult = Convert.ToBoolean(result);

                        string resultText = boolResult ? "✅ 检测通过" : "❌ 检测未通过";
                        UIMessageBox.ShowInfo(
                            $"数据源值: {value}\n" +
                            $"表达式: {expression}\n" +
                            $"计算式: {evaluateExpression}\n" +
                            $"结果: {resultText}", boolResult);
                    }
                    else
                    {
                        UIMessageBox.ShowWarning("表达式引擎不可用");
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError($"表达式计算失败：{ex.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "测试表达式失败");
                UIMessageBox.ShowError($"测试失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 读取数据源的值
        /// </summary>
        private async Task<object> ReadDataSourceValue()
        {
            try
            {
                bool isVariable = cmbDataSourceType.SelectedIndex == 0;

                if (isVariable)
                {
                    string varName = cmbVariableName.Text?.Trim();
                    if (string.IsNullOrEmpty(varName)) return null;

                    var variable = _variableManager?.TryFindVariableByName(varName);
                    return variable?.VarValue;
                }
                else
                {
                    string moduleName = cmbPlcModule.Text?.Trim();
                    string address = cmbPlcAddress.Text?.Trim();

                    if (string.IsNullOrEmpty(moduleName) || string.IsNullOrEmpty(address))
                        return null;

                    var plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
                    if (plcManager != null)
                    {
                        var plcConfig = new PlcAddressConfig
                        {
                            ModuleName = moduleName,
                            Address = address
                        };
                        return await plcManager.ReadPLCForDetectionAsync(plcConfig);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "读取数据源值失败");
                return null;
            }
        }

        #endregion

        #region 参数加载/保存

        /// <summary>
        /// 加载参数到界面
        /// </summary>
        private void LoadParameterToForm()
        {
            if (_parameter == null) return;

            try
            {
                _isInitializing = true;

                // 基本信息
                txtDetectionName.Text = _parameter.DetectionName ?? "";

                // 数据源
                cmbDataSourceType.SelectedIndex = _parameter.DataSource?.SourceType == DataSourceType.PLC ? 1 : 0;
                cmbVariableName.Text = _parameter.DataSource?.VariableName ?? "";
                cmbPlcModule.Text = _parameter.DataSource?.PlcConfig?.ModuleName ?? "";
                cmbPlcAddress.Text = _parameter.DataSource?.PlcConfig?.Address ?? "";

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

                // 更新UI
                UpdateDataSourceUI();
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
        private void SaveFormToParameter()
        {
            if (_parameter == null) return;

            try
            {
                // 基本信息
                _parameter.DetectionName = txtDetectionName.Text?.Trim() ?? "";

                // 数据源
                _parameter.DataSource ??= new DataSourceConfig();
                _parameter.DataSource.SourceType = cmbDataSourceType.SelectedIndex == 0 ? DataSourceType.Variable : DataSourceType.PLC;
                _parameter.DataSource.VariableName = cmbVariableName.Text?.Trim() ?? "";
                _parameter.DataSource.PlcConfig ??= new PlcAddressConfig();
                _parameter.DataSource.PlcConfig.ModuleName = cmbPlcModule.Text?.Trim() ?? "";
                _parameter.DataSource.PlcConfig.Address = cmbPlcAddress.Text?.Trim() ?? "";

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
        private bool ValidateInput()
        {
            try
            {
                // 验证检测名称
                if (string.IsNullOrWhiteSpace(txtDetectionName.Text))
                {
                    UIMessageBox.ShowWarning("请输入检测项名称");
                    txtDetectionName.Focus();
                    return false;
                }

                // 验证数据源
                bool isVariable = cmbDataSourceType.SelectedIndex == 0;
                if (isVariable)
                {
                    if (string.IsNullOrWhiteSpace(cmbVariableName.Text))
                    {
                        UIMessageBox.ShowWarning("请选择数据源变量");
                        cmbVariableName.Focus();
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(cmbPlcModule.Text))
                    {
                        UIMessageBox.ShowWarning("请选择PLC模块");
                        cmbPlcModule.Focus();
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(cmbPlcAddress.Text))
                    {
                        UIMessageBox.ShowWarning("请选择PLC地址");
                        cmbPlcAddress.Focus();
                        return false;
                    }
                }

                // 验证表达式
                string expression = txtConditionExpression.Text?.Trim() ?? "";
                if (string.IsNullOrEmpty(expression))
                {
                    UIMessageBox.ShowWarning("请输入检测条件表达式");
                    txtConditionExpression.Focus();
                    return false;
                }

                var (isValid, message) = DetectionExpressionHelper.ValidateConditionExpression(expression);
                if (!isValid)
                {
                    UIMessageBox.ShowWarning($"表达式无效：{message}");
                    txtConditionExpression.Focus();
                    return false;
                }

                // 验证结果处理
                if (chkSaveResult.Checked && string.IsNullOrWhiteSpace(cmbResultVariable.Text))
                {
                    UIMessageBox.ShowWarning("启用了保存结果，但未选择结果变量");
                    cmbResultVariable.Focus();
                    return false;
                }

                if (chkSaveValue.Checked && string.IsNullOrWhiteSpace(cmbValueVariable.Text))
                {
                    UIMessageBox.ShowWarning("启用了保存值，但未选择值变量");
                    cmbValueVariable.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证输入失败");
                UIMessageBox.ShowError($"验证失败：{ex.Message}");
                return false;
            }
        }

        #endregion

        #region IParameterForm接口实现

        public void SetDefaultValues()
        {
            _parameter = new Parameter_Detection
            {
                DetectionName = $"检测项 {_workflowState?.StepNum + 1}",
                TimeoutMs = 5000,
                RefreshRateMs = 100,
                RetryCount = 0,
                RetryIntervalMs = 1000,
                ConditionExpression = "{value} >= 0",
                DataSource = new DataSourceConfig
                {
                    SourceType = DataSourceType.Variable,
                    VariableName = "",
                    PlcConfig = new PlcAddressConfig()
                },
                ResultHandling = new ResultHandling
                {
                    OnFailure = FailureAction.Continue,
                    ShowResult = true,
                    MessageTemplate = "检测项 {DetectionName}: {Result}"
                }
            };

            _logger?.LogDebug("设置检测参数默认值");
            LoadParameterToForm();
        }

        public void PopulateControls(Parameter_Detection parameter)
        {
            Parameter = parameter;
        }

        public bool ValidateTypedParameters()
        {
            return ValidateInput();
        }

        public Parameter_Detection CollectTypedParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        public Parameter_Detection ConvertParameter(object stepParameter)
        {
            if (stepParameter is Parameter_Detection detectionParam)
            {
                return detectionParam;
            }

            if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Parameter_Detection>(jsonStr);
                }
                catch
                {
                    // 解析失败，返回默认参数
                }
            }

            return new Parameter_Detection();
        }

        #endregion
    }
}
using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 条件判断参数配置表单
    /// 使用ExpressionInputPanel实现灵活的条件表达式输入
    /// 支持完整的条件表达式，如：{温度} > 100 && {压力} <= 6.0
    /// </summary>
    public partial class Form_Condition : BaseParameterForm
    {
        #region 私有字段

        private Parameter_Condition _parameter;
        private readonly ExpressionEngine _expressionEngine;
        private System.Windows.Forms.Timer _validationTimer;
        private bool _isInitializing = true;
        private bool _hasUnsavedChanges = false;

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_Condition Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_Condition();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数 - 设计器使用
        /// </summary>
        public Form_Condition()
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
        public Form_Condition(
            IWorkflowStateService workflowState,
            ILogger<Form_Condition> logger)
            : base(workflowState, logger)
        {

            // 获取表达式引擎
            _expressionEngine = ServiceLocator.Current?.GetService<ExpressionEngine>();

            InitializeComponent();
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 初始化验证定时器
                InitializeValidationTimer();

                // 附加表达式输入面板
                AttachExpressionPanels();

                // 绑定事件
                BindEvents();

                _isInitializing = false;
                Logger?.LogInformation("条件判断工具窗体加载完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "条件判断表单初始化失败");
                MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
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
            _validationTimer.Tick += (s, e) =>
            {
                _validationTimer.Stop();
                UpdateValidationStatus();
            };
        }

        /// <summary>
        /// 附加表达式输入面板
        /// </summary>
        private void AttachExpressionPanels()
        {
            try
            {
                // 为条件表达式文本框附加ExpressionInputPanel
                ExpressionInputPanel.AttachTo(txtConditionExpression, new InputPanelOptions
                {
                    Mode = InputMode.Condition,
                    EnabledModules = InputModules.Variable | InputModules.PLC |
                                     InputModules.Expression | InputModules.Constant,
                    Title = "配置条件表达式",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true,
                    ExpectedReturnType = typeof(bool)
                });

                // 设置水印提示
                txtConditionExpression.Watermark = "点击输入条件表达式，如：{温度} > 100 (按F2打开面板)";

                Logger?.LogDebug("表达式输入面板附加完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "附加表达式输入面板失败");
            }
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            try
            {
                // 条件表达式变化
                txtConditionExpression.TextChanged += (s, e) =>
                {
                    if (!_isInitializing)
                    {
                        _validationTimer.Stop();
                        _validationTimer.Start();
                        MarkAsChanged();
                    }
                };

                // 描述变化
                txtDescription.TextChanged += (s, e) =>
                {
                    if (!_isInitializing)
                    {
                        MarkAsChanged();
                    }
                };

                // 启用复选框
                chkEnabled.CheckedChanged += (s, e) =>
                {
                    if (!_isInitializing)
                    {
                        MarkAsChanged();
                    }
                };

                // 配置满足条件步骤
                btnConfigTrueSteps.Click += BtnConfigTrueSteps_Click;

                // 配置不满足条件步骤
                btnConfigFalseSteps.Click += BtnConfigFalseSteps_Click;

                // 保存按钮
                btnSave.Click += BtnSave_Click;

                // 取消按钮
                btnCancel.Click += BtnCancel_Click;

                // 帮助按钮
                btnHelp.Click += BtnHelp_Click;

                // 窗体关闭
                this.FormClosing += Form_Condition_FormClosing;

                Logger?.LogDebug("事件绑定完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "绑定事件失败");
            }
        }

        /// <summary>
        /// 标记为已修改
        /// </summary>
        private void MarkAsChanged()
        {
            _hasUnsavedChanges = true;
        }

        #endregion

        #region 验证方法

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
                        ValidationLabel = "条件表达式",
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
                    // 降级方案：没有表达式引擎时，使用简单验证
                    bool isValid = ValidateBasicSyntax(expression);
                    lblValidationStatus.Text = isValid ? "✓ 基本语法检查通过" : "✗ 语法错误";
                    lblValidationStatus.ForeColor = isValid
                        ? Color.FromArgb(40, 167, 69)
                        : Color.FromArgb(220, 53, 69);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证表达式时发生错误");
                lblValidationStatus.Text = $"✗ 验证错误: {ex.Message}";
                lblValidationStatus.ForeColor = Color.FromArgb(220, 53, 69);
            }
        }

        /// <summary>
        /// 获取运行时变量白名单
        /// </summary>
        private HashSet<string> GetRuntimeVariableWhitelist()
        {
            return new HashSet<string>
            {
                "CurrentDateTime", "CurrentDate", "CurrentTime",
                "LoopIndex", "LoopCount", "StepIndex", "StepName"
            };
        }

        /// <summary>
        /// 基本语法验证（降级方案）
        /// </summary>
        private bool ValidateBasicSyntax(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return false;

            // 检查括号匹配
            int braceCount = 0;
            int parenCount = 0;
            foreach (char c in expression)
            {
                if (c == '{') braceCount++;
                else if (c == '}') braceCount--;
                else if (c == '(') parenCount++;
                else if (c == ')') parenCount--;

                if (braceCount < 0 || parenCount < 0)
                    return false;
            }

            return braceCount == 0 && parenCount == 0;
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        protected override bool ValidateInput()
        {
            try
            {
                // 验证条件表达式
                string expression = txtConditionExpression.Text?.Trim() ?? "";
                if (string.IsNullOrEmpty(expression))
                {
                    MessageHelper.MessageOK(this, "请输入条件表达式");
                    txtConditionExpression.Focus();
                    return false;
                }

                // 使用ExpressionEngine验证
                if (_expressionEngine != null)
                {
                    var validationContext = new ValidationContext
                    {
                        ValidationLabel = "条件表达式",
                        AllowFunctionCalls = true,
                        AllowPlcReferences = true,
                        StrictMode = false,
                        RuntimeVariableWhitelist = GetRuntimeVariableWhitelist().ToList()
                    };

                    var result = _expressionEngine.ValidateExpression(expression, validationContext);
                    if (result.IsValid) return true;
                    string errorMsg = result.Errors.Count != 0
                        ? string.Join("; ", result.Errors)
                        : result.Message;
                    MessageHelper.MessageOK(this, $"表达式无效：{errorMsg}");
                    txtConditionExpression.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入失败");
                MessageHelper.MessageOK(this, $"验证失败：{ex.Message}");
                return false;
            }
        }

        #endregion

        #region 子步骤配置

        /// <summary>
        /// 配置满足条件时的步骤
        /// </summary>
        private void BtnConfigTrueSteps_Click(object sender, EventArgs e)
        {
            try
            {
                var trueSteps = Parameter.TrueSteps ?? new List<Parent>();
                ConfigureChildSteps(ref trueSteps, "满足条件时执行", true);
                Parameter.TrueSteps = trueSteps;
                UpdateStepsCount();
                MarkAsChanged();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "配置满足条件步骤失败");
                MessageHelper.MessageOK($"配置失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 配置不满足条件时的步骤
        /// </summary>
        private void BtnConfigFalseSteps_Click(object sender, EventArgs e)
        {
            try
            {
                var falseSteps = Parameter.FalseSteps ?? new List<Parent>();
                ConfigureChildSteps(ref falseSteps, "不满足条件时执行", false);
                Parameter.FalseSteps = falseSteps;
                UpdateStepsCount();
                MarkAsChanged();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "配置不满足条件步骤失败");
                MessageHelper.MessageOK($"配置失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 配置子步骤
        /// </summary>
        private void ConfigureChildSteps(ref List<Parent> steps, string title, bool isTrueBranch)
        {
            try
            {
                // 尝试使用子步骤配置窗体
                var formService = ServiceLocator.Current?.GetService<FormService>();
                if (formService != null)
                {
                    // 创建子步骤配置窗体
                    using var configForm = ServiceLocator.Current?.GetService<Form_ChildStepsConfig>();
                    if (configForm != null)
                    {
                        configForm.Text = title;
                        configForm.Steps = steps ?? new List<Parent>();

                        VarHelper.ShowDialogWithOverlay(this, configForm);

                        if (configForm.DialogResult == DialogResult.OK)
                        {
                            steps = configForm.Steps;
                            Logger?.LogDebug("{Title} 步骤配置完成，共 {Count} 个步骤", title, steps?.Count ?? 0);
                        }
                        return;
                    }
                }

                // 降级方案：显示提示信息
                string branchType = isTrueBranch ? "满足条件" : "不满足条件";
                string message = $"【{title}】的子步骤配置\n\n" +
                                 $"当前已配置: {steps?.Count ?? 0} 个步骤\n\n" +
                                 $"提示：请确保子步骤配置窗体已正确注册到依赖注入容器";
                MessageHelper.MessageOK(this, message, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "配置子步骤失败: {Title}", title);
                MessageHelper.MessageOK($"配置子步骤失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 更新步骤计数显示
        /// </summary>
        private void UpdateStepsCount()
        {
            int trueCount = Parameter?.TrueSteps?.Count ?? 0;
            int falseCount = Parameter?.FalseSteps?.Count ?? 0;

            lblTrueStepsCount.Text = $"当条件为 True 时执行的步骤 ({trueCount} 个)";
            lblFalseStepsCount.Text = $"当条件为 False 时执行的步骤 ({falseCount} 个)";
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 保存按钮点击事件
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
                Logger?.LogError(ex, "保存条件判断参数失败");
                MessageHelper.MessageOK($"保存失败：{ex.Message}", TType.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "确定";
            }
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
        /// 帮助按钮点击事件
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string helpText = @"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📖 条件判断配置 - 使用说明
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

🔹 基本概念
   条件判断用于根据表达式的计算结果来决定执行哪些步骤

🔹 条件表达式
   点击输入框或按 F2 打开智能输入面板，支持：
   • 变量引用：{变量名}
   • PLC地址：{模块名.地址}
   • 比较运算：>, <, >=, <=, ==, !=
   • 逻辑运算：&&（与）, ||（或）, !（非）

🔹 表达式示例
   • 单一条件：{温度} > 100
   • 范围判断：{压力} >= 5.0 && {压力} <= 6.0
   • 多条件组合：{状态} == ""OK"" && {计数} > 0
   • PLC条件：{AI检测.PE01} > 4.5

🔹 分支配置
   • 满足条件：条件为 True 时执行的步骤
   • 不满足条件：条件为 False 时执行的步骤

🔹 快捷键
   • F2 或 Ctrl+Space：打开输入面板
   • Enter：提交并关闭
   • Escape：取消

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

                MessageHelper.MessageOK(this, helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助时发生错误");
            }
        }

        #endregion

        #region 参数加载与保存

        /// <summary>
        /// 加载参数到界面
        /// </summary>
        protected override void LoadParameterToForm()
        {
            if (Parameter == null || _isInitializing) return;

            try
            {
                _isInitializing = true;

                // 尝试迁移旧版本参数
                Parameter.MigrateFromLegacy();

                // 加载基本信息
                txtDescription.Text = Parameter.Description ?? "";
                chkEnabled.Checked = Parameter.IsEnabled;

                // 加载条件表达式
                txtConditionExpression.Text = Parameter.ConditionExpression ?? "";

                // 更新步骤计数
                UpdateStepsCount();

                // 更新验证状态
                UpdateValidationStatus();

                _hasUnsavedChanges = false;
                Logger?.LogDebug("参数加载到界面完成");
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
        /// 保存界面数据到参数对象
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                Parameter.Description = txtDescription.Text?.Trim() ?? "";
                Parameter.IsEnabled = chkEnabled.Checked;
                Parameter.ConditionExpression = txtConditionExpression.Text?.Trim() ?? "";

                Logger?.LogDebug("参数保存完成: 条件表达式={Expression}", Parameter.ConditionExpression);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存参数失败");
            }
        }
 
        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            Parameter = new Parameter_Condition
            {
                ConditionExpression = "",
                TrueSteps = [],
                FalseSteps = [],
                IsEnabled = true,
                Description = $"条件判断步骤 {_workflowState?.StepNum + 1}"
            };

            Logger?.LogDebug("设置条件判断参数默认值");
            LoadParameterToForm();
        }

        #endregion

        #region 窗体生命周期

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_Condition_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 关闭活动的表达式面板
                ExpressionInputPanel.CloseActivePanel();

                // 停止验证定时器
                _validationTimer?.Stop();
                _validationTimer?.Dispose();

                if (_hasUnsavedChanges && DialogResult != DialogResult.OK)
                {
                    Logger?.LogDebug("窗体关闭时存在未保存的更改");
                }

                Logger?.LogDebug("条件判断工具窗体正在关闭");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "窗体关闭事件处理时发生错误");
            }
        }

        #endregion
    }
}
using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 条件判断参数配置表单
    /// 用于配置和管理工作流步骤中的条件判断操作
    /// </summary>
    public partial class Form_Condition : BaseParameterForm
    {
        #region 属性

        private Parameter_Condition _parameter;
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

        #region 私有字段

        /// <summary>
        /// 初始化状态标志
        /// </summary>
        private bool _isInitializing = true;

        /// <summary>
        /// 未保存更改标志
        /// </summary>
        private bool _hasUnsavedChanges = false;

        #endregion


        #region 构造函数

        /// <summary>
        /// 默认构造函数 - 主要用于设计器
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
        {
            InitializeComponent();
            InitializeForm();

            Logger?.LogDebug("Form_Condition 依赖注入构造函数初始化完成");
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

                // 初始化运算符下拉框
                InitializeOperatorComboBox();

                // 加载可用变量
                LoadAvailableVariables();

                // 绑定事件
                BindEvents();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化窗体失败");
                MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 初始化运算符下拉框
        /// </summary>
        private void InitializeOperatorComboBox()
        {
            cmbOperator.Items.Clear();
            foreach (ConditionOperator op in Enum.GetValues(typeof(ConditionOperator)))
            {
                cmbOperator.Items.Add(op.ToString());
            }
            cmbOperator.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载可用变量
        /// </summary>
        private void LoadAvailableVariables()
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (globalVariableManager == null) return;

                var variables = globalVariableManager.GetAllVariables();
                Logger?.LogInformation("成功加载 {Count} 个可用变量", variables.Count);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载可用变量失败");
            }
        }

        /// <summary>
        /// 绑定事件处理器
        /// </summary>
        private void BindEvents()
        {
            try
            {
                // 运算符选择改变事件
                cmbOperator.SelectedIndexChanged += CmbOperator_SelectedIndexChanged;

                // 文本框改变事件
                txtLeftExpression.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                txtRightExpression.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                txtRangeMin.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                txtRangeMax.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };
                txtDescription.TextChanged += (s, e) => { if (!_isInitializing) _hasUnsavedChanges = true; };

                // 按钮点击事件
                btnSave.Click += BtnSave_Click;
                btnCancel.Click += BtnCancel_Click;
                btnHelp.Click += BtnHelp_Click;
                btnSelectVarLeft.Click += BtnSelectVarLeft_Click;
                btnSelectVarRight.Click += BtnSelectVarRight_Click;
                btnSelectVarRangeMin.Click += BtnSelectVarRangeMin_Click;
                btnSelectVarRangeMax.Click += BtnSelectVarRangeMax_Click;
                btnConfigTrueSteps.Click += BtnConfigTrueSteps_Click;
                btnConfigFalseSteps.Click += BtnConfigFalseSteps_Click;

                // 窗体关闭事件
                this.FormClosing += Form_Condition_FormClosing;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "绑定事件失败");
            }
        }

        #endregion


        #region UI更新

        /// <summary>
        /// 根据运算符更新界面
        /// </summary>
        private void UpdateUIForOperator()
        {
            var selectedOperator = (ConditionOperator)Enum.Parse(typeof(ConditionOperator), cmbOperator.SelectedItem?.ToString() ?? "等于");

            bool isRangeOperator = selectedOperator == ConditionOperator.在范围内 || selectedOperator == ConditionOperator.不在范围内;

            // 显示/隐藏相应控件
            panelSingleValue.Visible = !isRangeOperator;
            panelRangeValue.Visible = isRangeOperator;
        }

        /// <summary>
        /// 运算符选择改变事件
        /// </summary>
        private void CmbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isInitializing)
            {
                UpdateUIForOperator();
                _hasUnsavedChanges = true;
            }
        }

        #endregion

        #region 变量选择

        /// <summary>
        /// 选择左值变量
        /// </summary>
        private void BtnSelectVarLeft_Click(object sender, EventArgs e)
        {
            SelectVariableForTextBox(txtLeftExpression);
        }

        /// <summary>
        /// 选择右值变量
        /// </summary>
        private void BtnSelectVarRight_Click(object sender, EventArgs e)
        {
            SelectVariableForTextBox(txtRightExpression);
        }

        /// <summary>
        /// 选择范围最小值变量
        /// </summary>
        private void BtnSelectVarRangeMin_Click(object sender, EventArgs e)
        {
            SelectVariableForTextBox(txtRangeMin);
        }

        /// <summary>
        /// 选择范围最大值变量
        /// </summary>
        private void BtnSelectVarRangeMax_Click(object sender, EventArgs e)
        {
            SelectVariableForTextBox(txtRangeMax);
        }

        /// <summary>
        /// 为文本框选择变量
        /// </summary>
        private void SelectVariableForTextBox(Sunny.UI.UITextBox textBox)
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (globalVariableManager == null)
                {
                    MessageHelper.MessageOK("全局变量管理器不可用", TType.Warn);
                    return;
                }

                var variables = globalVariableManager.GetAllVariables();
                if (variables.Count == 0)
                {
                    MessageHelper.MessageOK("当前没有可用的变量", TType.Info);
                    return;
                }

                // 创建变量选择对话框
                var dialog = new VariableSelectionDialog(globalVariableManager);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string selectedVar = dialog.SelectedVariable.Name;
                    if (!string.IsNullOrEmpty(selectedVar))
                    {
                        // 在当前光标位置插入变量引用
                        int selectionStart = textBox.SelectionStart;
                        string currentText = textBox.Text ?? "";
                        string varReference = $"{{{selectedVar}}}";

                        textBox.Text = currentText.Insert(selectionStart, varReference);
                        textBox.SelectionStart = selectionStart + varReference.Length;
                        textBox.Focus();

                        _hasUnsavedChanges = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "选择变量失败");
                MessageHelper.MessageOK($"选择变量失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 子步骤配置

        /// <summary>
        /// 配置满足条件时的步骤
        /// </summary>
        private void BtnConfigTrueSteps_Click(object sender, EventArgs e)
        {
            var TrueSteps = Parameter.TrueSteps;
            ConfigureChildSteps(ref TrueSteps, "满足条件时执行");
            lblTrueStepsCount.Text = $"满足条件时执行的步骤 ({TrueSteps?.Count ?? 0} 个)";
        }

        /// <summary>
        /// 配置不满足条件时的步骤
        /// </summary>
        private void BtnConfigFalseSteps_Click(object sender, EventArgs e)
        {
            var FalseSteps = Parameter.FalseSteps;
            ConfigureChildSteps(ref FalseSteps, "不满足条件时执行");
            lblFalseStepsCount.Text = $"不满足条件时执行的步骤 ({FalseSteps?.Count ?? 0} 个)";
        }

        /// <summary>
        /// 配置子步骤
        /// </summary>
        private void ConfigureChildSteps(ref List<Parent> steps, string title)
        {
            try
            {
                // 这里打开子步骤配置对话框
                // 由于子步骤配置比较复杂，这里简化处理
                // 实际项目中应该有专门的子步骤配置对话框
                MessageHelper.MessageOK($"{title}的子步骤配置功能待实现\n当前步骤数: {steps?.Count ?? 0}", TType.Info);
                _hasUnsavedChanges = true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "配置子步骤失败");
                MessageHelper.MessageOK($"配置子步骤失败：{ex.Message}", TType.Error);
            }
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
                // 统一保存方法
                SaveParameters();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存条件判断参数失败");
                MessageHelper.MessageOK($"保存失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

🔹 配置说明
   • 左值表达式: 要进行判断的值或变量
   • 运算符: 选择比较方式（等于、大于等）
   • 右值/范围: 比较的目标值
   • 满足条件: 条件为真时执行的步骤
   • 不满足条件: 条件为假时执行的步骤

🔹 变量引用
   • 使用 {变量名} 引用全局变量
   • 示例: {Temperature} > 100
   • 点击 [...] 按钮快速选择变量

🔹 运算符说明
   • 等于/不等于: 精确匹配
   • 大于/小于: 数值比较
   • 在范围内: 检查值是否在指定范围内
   • 不在范围内: 检查值是否不在指定范围内

⚠️ 注意事项
   1. 确保表达式语法正确
   2. 变量引用必须用 {} 包裹
   3. 范围判断时需要同时设置最小值和最大值
   4. 可以不配置子步骤，但建议至少配置一个分支

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

                MessageHelper.MessageOK(this, helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助失败");
            }
        }

        #endregion

        #region 窗体事件

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_Condition_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK) return;

            if (_hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes(this, "存在未保存的更改，确定要关闭吗？");
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region 基类方法重写
        /// <summary>
        /// 验证输入数据的有效性
        /// </summary>
        protected override bool ValidateInput()
        {
            try
            {
                // 验证左值表达式
                if (string.IsNullOrWhiteSpace(Parameter.LeftExpression))
                {
                    MessageHelper.MessageOK("请输入左值表达式！", TType.Warn);
                    txtLeftExpression.Focus();
                    return false;
                }

                var selectedOperator = Parameter.Operator;
                bool isRangeOperator = selectedOperator == ConditionOperator.在范围内 || selectedOperator == ConditionOperator.不在范围内;

                if (isRangeOperator)
                {
                    // 验证范围值
                    if (string.IsNullOrWhiteSpace(Parameter.RangeMin))
                    {
                        MessageHelper.MessageOK("请输入范围最小值！", TType.Warn);
                        txtRangeMin.Focus();
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(Parameter.RangeMax))
                    {
                        MessageHelper.MessageOK("请输入范围最大值！", TType.Warn);
                        txtRangeMax.Focus();
                        return false;
                    }
                }
                else
                {
                    // 验证右值表达式
                    if (string.IsNullOrWhiteSpace(Parameter.RightExpression))
                    {
                        MessageHelper.MessageOK("请输入右值表达式！", TType.Warn);
                        txtRightExpression.Focus();
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入失败");
                MessageHelper.MessageOK($"验证失败：{ex.Message}", TType.Error);
                return false;
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

                txtLeftExpression.Text = Parameter.LeftExpression ?? "";
                cmbOperator.SelectedItem = Parameter.Operator.ToString();
                txtRightExpression.Text = Parameter.RightExpression ?? "";
                txtRangeMin.Text = Parameter.RangeMin ?? "";
                txtRangeMax.Text = Parameter.RangeMax ?? "";
                txtDescription.Text = Parameter.Description ?? "";
                chkEnabled.Checked = true; // 默认启用

                // 更新子步骤计数
                lblTrueStepsCount.Text = $"满足条件时执行的步骤 ({Parameter.TrueSteps?.Count ?? 0} 个)";
                lblFalseStepsCount.Text = $"不满足条件时执行的步骤 ({Parameter.FalseSteps?.Count ?? 0} 个)";

                // 根据运算符更新界面
                UpdateUIForOperator();

                _hasUnsavedChanges = false;
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
            Parameter.LeftExpression = txtLeftExpression.Text?.Trim() ?? "";
            Parameter.Operator = (ConditionOperator)Enum.Parse(typeof(ConditionOperator), cmbOperator.SelectedItem?.ToString() ?? "等于");
            Parameter.RightExpression = txtRightExpression.Text?.Trim() ?? "";
            Parameter.RangeMin = txtRangeMin.Text?.Trim() ?? "";
            Parameter.RangeMax = txtRangeMax.Text?.Trim() ?? "";
            Parameter.Description = txtDescription.Text?.Trim() ?? "";
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            Parameter = new Parameter_Condition
            {
                LeftExpression = "",
                Operator = ConditionOperator.等于,
                RightExpression = "",
                RangeMin = "",
                RangeMax = "",
                TrueSteps = [],
                FalseSteps = [],
                Description = $"条件判断步骤 {_workflowState?.StepNum + 1}"
            };

            Logger?.LogDebug("设置条件判断参数默认值");
            LoadParameterToForm();
        }
        #endregion

    }
}
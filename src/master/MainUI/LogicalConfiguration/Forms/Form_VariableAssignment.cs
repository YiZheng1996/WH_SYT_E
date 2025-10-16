using AntdUI;
using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 变量赋值工具窗体
    /// 提供变量赋值配置的用户界面，支持多种赋值方式：
    /// 1. 直接赋值 - 将固定值赋给变量
    /// 2. 表达式计算 - 通过表达式计算结果赋值
    /// 3. 变量复制 - 从其他变量复制值
    /// 4. PLC读取 - 从PLC设备读取值
    /// </summary>
    public partial class Form_VariableAssignment : BaseParameterForm, IParameterForm<Parameter_VariableAssignment>
    {
        #region 私有字段

        /// <summary>
        /// 表达式验证器 - 用于验证和计算表达式的正确性
        /// 提供表达式语法检查和预期值计算功能
        /// </summary>
        private readonly ExpressionValidator _expressionValidator;

        /// <summary>
        /// 变量赋值引擎 - 负责执行实际的变量赋值操作
        /// 支持各种赋值方式的具体执行逻辑
        /// </summary>
        private readonly VariableAssignmentEngine _assignmentEngine;

        /// <summary>
        /// 验证定时器 - 延迟触发配置验证，避免用户输入时频繁验证
        /// 设置500ms延迟，提供良好的用户体验
        /// </summary>
        private System.Windows.Forms.Timer _validationTimer;

        /// <summary>
        /// 预览定时器 - 延迟更新预览结果，减少不必要的计算
        /// 设置500ms延迟，在用户输入完成后更新预览
        /// </summary>
        private System.Windows.Forms.Timer _previewTimer;

        /// <summary>
        /// 初始化状态标志 - 防止在窗体初始化过程中触发不必要的事件
        /// true: 正在初始化，忽略控件事件; false: 初始化完成，正常处理事件
        /// </summary>
        private bool _isInitializing = true;

        /// <summary>
        /// 未保存更改标志 - 跟踪用户是否做了未保存的修改
        /// 用于提示用户保存更改或在关闭时警告
        /// </summary>
        private bool _hasUnsavedChanges = false;

        /// <summary>
        /// 当前的参数对象 - 存储窗体的所有配置数据
        /// 包含目标变量、赋值方式、表达式等所有必要信息
        /// </summary>
        private Parameter_VariableAssignment _parameter;

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象属性
        /// 获取或设置当前的变量赋值参数
        /// 设置时会自动加载参数到界面控件（仅在窗体完全加载后）
        /// </summary>
        public Parameter_VariableAssignment Parameter
        {
            get => _parameter;
            set
            {
                // 确保参数不为null，提供默认值
                _parameter = value ?? new Parameter_VariableAssignment();

                // 只有在非设计模式、非基类加载状态且窗体句柄已创建时才加载到界面
                // 这样避免在窗体初始化过程中的无效操作
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// 主要供Visual Studio设计器使用，也支持运行时无依赖注入的场景
        /// 会尝试从全局服务容器获取所需的服务实例
        /// </summary>
        public Form_VariableAssignment()
        {
            InitializeComponent(); // 初始化设计器生成的组件

            // 非设计模式下进行实际的初始化
            if (!DesignMode)
            {
                InitializeForm();
            }
        }

        /// <summary>
        /// 依赖注入构造函数
        /// 推荐在生产环境中使用，通过依赖注入容器提供所需服务
        /// </summary>
        /// <param name="workflowState">工作流状态服务 - 提供当前步骤信息</param>
        /// <param name="logger">日志服务 - 记录操作日志和错误信息</param>
        /// <param name="pLcManager">PLC管理器 - 处理PLC相关操作</param>
        public Form_VariableAssignment(
            IWorkflowStateService workflowState,
            ILogger<Form_VariableAssignment> logger,
            IPLCManager pLcManager)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化窗体
        /// 按顺序执行各项初始化任务，确保窗体处于可用状态
        /// </summary>
        private void InitializeForm()
        {
            // 设计模式下不执行初始化，避免影响设计器
            if (DesignMode) return;

            try
            {
                _isInitializing = true; // 设置初始化状态，暂时忽略控件事件

                // 获取服务（优先使用基类提供的服务，再尝试从ServiceProvider获取）
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                var plcManager = _plcManager ?? Program.ServiceProvider?.GetService<IPLCManager>();

                if (globalVariableManager == null)
                {
                    Logger?.LogWarning("无法获取GlobalVariableManager服务");
                }

                // 初始化核心引擎
                InitializeEngines(globalVariableManager, plcManager);

                // 设置窗体样式
                InitializeFormStyle();

                // 初始化定时器
                InitializeTimers();

                // 初始化界面数据
                InitializeVariableComboBox();

                // 初始化赋值类型下拉框
                InitializeAssignmentType();

                // 绑定事件
                BindEvents();

                // 初始验证
                ValidateConfigurationAsync();

                _isInitializing = false; // 初始化完成，开始响应用户操作
                Logger?.LogInformation("变量赋值工具窗体加载完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "变量赋值表单初始化失败");
                MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 初始化核心引擎
        /// 创建表达式验证器和变量赋值引擎实例
        /// 使用反射设置readonly字段，确保服务实例在对象生命周期内不变
        /// </summary>
        /// <param name="globalVariableManager">全局变量管理器</param>
        /// <param name="plcManager">PLC管理器</param>
        private void InitializeEngines(GlobalVariableManager globalVariableManager, IPLCManager plcManager)
        {
            try
            {
                if (globalVariableManager != null)
                {
                    // 初始化核心引擎 
                    var expressionValidator = new ExpressionValidator(globalVariableManager);
                    var assignmentEngine = new VariableAssignmentEngine(globalVariableManager, plcManager, expressionValidator);

                    // 使用反射设置私有字段（保持兼容性）
                    var expressionField = typeof(Form_VariableAssignment).GetField("_expressionValidator",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    expressionField?.SetValue(this, expressionValidator);

                    var assignmentField = typeof(Form_VariableAssignment).GetField("_assignmentEngine",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    assignmentField?.SetValue(this, assignmentEngine);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化引擎失败");
            }
        }

        /// <summary>
        /// 初始化窗体样式
        /// 设置窗体的外观、大小、位置等视觉属性
        /// 使用Sunny.UI的自定义样式以保持界面一致性
        /// </summary>
        private void InitializeFormStyle()
        {
            // 设置窗体属性
            this.Text = "变量赋值工具";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;

            // 设置Sunny.UI主题
            this.Style = UIStyle.Custom;
            this.StyleCustomMode = true;
            this.TitleColor = Color.FromArgb(65, 100, 204);
            this.TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            this.RectColor = Color.FromArgb(65, 100, 204);
        }

        /// <summary>
        /// 初始化定时器
        /// 创建验证和预览定时器，设置合适的延迟时间以平衡响应性和性能
        /// </summary>
        private void InitializeTimers()
        {
            // 验证定时器 - 延迟验证以避免频繁触发
            _validationTimer = new System.Windows.Forms.Timer
            {
                Interval = 500 // 500ms延迟
            };
            _validationTimer.Tick += (s, e) =>
            {
                _validationTimer.Stop();
                ValidateConfigurationAsync();
            };

            // 预览定时器，默认刷新速率500ms - 延迟预览更新
            _previewTimer = new System.Windows.Forms.Timer
            {
                Interval = 500
            };
            _previewTimer.Tick += (s, e) =>
            {
                _previewTimer.Stop();
                RefreshPreviewAsync();
            };
        }

        /// <summary>
        /// 初始化赋值类型下拉框
        /// 将AssignmentTypeEnum枚举值绑定到下拉框
        /// 使用EnumHelper获取枚举的显示名称和值
        /// </summary>
        private void InitializeAssignmentType()
        {
            if (cmbAssignmentType != null)
            {
                cmbAssignmentType.DataSource = EnumExtensions.GetEnumItems<AssignmentTypeEnum>();
                cmbAssignmentType.DisplayMember = "DisplayName";
                cmbAssignmentType.ValueMember = "Value";
                cmbAssignmentType.SelectedValue = AssignmentTypeEnum.DirectAssignment;
            }
        }

        /// <summary>
        /// 初始化变量下拉框
        /// 从全局变量管理器获取所有可用变量，并绑定到下拉框
        /// 提供变量名的自动完成和选择功能
        /// </summary>
        private void InitializeVariableComboBox()
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (globalVariableManager != null && cmbTargetVariable != null)
                {
                    var variables = globalVariableManager.GetAllVariables();
                    var variableItems = variables.Select(v => new
                    {
                        Text = $"{v.VarName}",
                        Value = v.VarName
                    }).ToList();

                    cmbTargetVariable.DisplayMember = "Text";
                    cmbTargetVariable.ValueMember = "Value";
                    cmbTargetVariable.DataSource = variableItems;

                    if (variableItems.Count > 0 && cmbTargetVariable.SelectedIndex == -1)
                    {
                        cmbTargetVariable.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化变量下拉框失败");
            }
        }

        /// <summary>
        /// 绑定事件处理器
        /// 将用户界面控件的事件与相应的处理方法关联
        /// 为各个控件绑定独立的事件处理方法
        /// </summary>
        private void BindEvents()
        {
            try
            {
                // 目标变量改变事件
                cmbTargetVariable.SelectedIndexChanged += CmbTargetVariable_SelectedIndexChanged;

                // 赋值方式改变事件
                cmbAssignmentType.SelectedIndexChanged += CmbAssignmentType_SelectedIndexChanged;

                // 内容改变事件
                txtAssignmentContent.TextChanged += TxtAssignmentContent_TextChanged;
                txtCondition.TextChanged += TxtCondition_TextChanged;
                txtDescription.TextChanged += TxtDescription_TextChanged;
                chkEnabled.CheckedChanged += ChkEnabled_CheckedChanged;

                // PLC相关事件
                CboPlcModule.SelectedIndexChanged += CboPlcModule_SelectedIndexChanged;
                CboPlcAddress.SelectedIndexChanged += CboPlcAddress_SelectedIndexChanged;

                // 按钮事件
                btnOK.Click += BtnOK_Click;
                btnCancel.Click += BtnCancel_Click;
                btnTest.Click += BtnTest_Click;
                btnExpressionBuilder.Click += BtnExpressionBuilder_Click;
                btnHelp.Click += BtnHelp_Click;

                // 窗体事件
                FormClosing += Form_VariableAssignment_FormClosing;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "绑定事件时发生错误");
            }
        }

        #endregion

        #region 参数处理

        /// <summary>
        /// 设置默认值
        /// 当需要重置参数或创建新配置时调用
        /// 设置合理的默认值以提供良好的用户体验
        /// </summary>
        protected override void SetDefaultValues()
        {
            _parameter = new Parameter_VariableAssignment
            {
                TargetVarName = "",
                Expression = "",
                Condition = "",
                Description = $"变量赋值步骤 {_workflowState?.StepNum + 1}",
                IsAssignment = true,
                AssignmentType = AssignmentTypeEnum.DirectAssignment,
                DataSource = new DataSourceConfig()
            };

            Logger?.LogDebug("设置变量赋值参数默认值");
            LoadParameterToForm();
        }

        /// <summary>
        /// 加载参数到界面
        /// 根据参数的赋值类型显示相应的配置界面
        /// 包含异步的PLC模块和地址加载
        /// </summary>
        /// <param name="parameter">要加载的参数对象</param>
        public async void LoadParameter(Parameter_VariableAssignment parameter)
        {
            _parameter = parameter ?? new Parameter_VariableAssignment();

            try
            {
                // 基本信息加载
                cmbTargetVariable.Text = _parameter.TargetVarName ?? "";
                cmbAssignmentType.SelectedValue = _parameter.AssignmentType;
                txtCondition.Text = _parameter.Condition ?? "";
                txtDescription.Text = _parameter.Description ?? "";
                chkEnabled.Checked = _parameter.IsAssignment;

                // 根据赋值类型加载相应数据
                switch (_parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.PLCRead:
                        await Parameterplcs();// 先加载PLC模块列表
                        SetComboBoxValue(CboPlcModule, _parameter.DataSource.PlcConfig.ModuleName);// 设置PLC配置

                        // 如果有模块名，加载对应的地址列表
                        if (!string.IsNullOrEmpty(_parameter.DataSource.PlcConfig.ModuleName))
                        {
                            await LoadPlcAddresses(_parameter.DataSource.PlcConfig.ModuleName);
                            SetComboBoxValue(CboPlcAddress, _parameter.DataSource.PlcConfig.Address);
                        }
                        break;

                    default:
                        // 其他类型使用Expression字段
                        if (txtAssignmentContent != null)
                            txtAssignmentContent.Text = _parameter.Expression ?? "";
                        break;
                }

                // 更新UI布局
                UpdateUIBasedOnAssignmentType();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数时发生错误");
                MessageHelper.MessageOK($"加载参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 从表单加载到Parameter属性（基类兼容）
        /// 调用LoadParameter方法，保持与基类的兼容性
        /// </summary>
        private void LoadParameterToForm()
        {
            LoadParameter(_parameter);
        }

        /// <summary>
        /// 获取当前配置的参数对象
        /// 根据界面当前状态创建一个新的参数对象，不修改内部_parameter
        /// 主要用于验证和测试场景
        /// </summary>
        /// <returns>根据当前界面状态创建的参数对象</returns>
        private Parameter_VariableAssignment GetParameter()
        {
            var parameter = new Parameter_VariableAssignment
            {
                TargetVarName = cmbTargetVariable?.Text ?? "",
                AssignmentType = (AssignmentTypeEnum)cmbAssignmentType.SelectedValue,
                Condition = txtCondition?.Text ?? "",
                Description = txtDescription?.Text ?? "",
                IsAssignment = chkEnabled?.Checked ?? false,
                DataSource = new DataSourceConfig()
            };

            // 根据赋值类型设置数据
            switch (parameter.AssignmentType)
            {
                case AssignmentTypeEnum.PLCRead:
                    parameter.DataSource.SourceType = DataSourceType.PLC;
                    parameter.DataSource.PlcConfig.ModuleName = CboPlcModule?.Text ?? "";
                    parameter.DataSource.PlcConfig.Address = CboPlcAddress?.Text ?? "";
                    parameter.Expression = ""; // PLC读取时Expression为空
                    break;

                default:
                    parameter.Expression = txtAssignmentContent?.Text ?? "";
                    parameter.DataSource.SourceType = DataSourceType.Variable;
                    break;
            }

            return parameter;
        }

        /// <summary>
        /// 将界面控件的值保存到参数对象
        /// 在用户点击确定或需要获取当前配置时调用
        /// </summary>
        private void SaveFormToParameter()
        {
            if (_parameter == null) return;

            try
            {
                _parameter.TargetVarName = cmbTargetVariable?.Text ?? "";
                _parameter.AssignmentType = (AssignmentTypeEnum)cmbAssignmentType.SelectedValue;
                _parameter.Condition = txtCondition?.Text ?? "";
                _parameter.Description = txtDescription?.Text ?? "";
                _parameter.IsAssignment = chkEnabled?.Checked ?? false;

                // 根据赋值类型保存数据
                switch (_parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.PLCRead:
                        _parameter.DataSource.SourceType = DataSourceType.PLC;
                        _parameter.DataSource.PlcConfig.ModuleName = CboPlcModule?.Text ?? "";
                        _parameter.DataSource.PlcConfig.Address = CboPlcAddress?.Text ?? "";
                        _parameter.Expression = ""; // PLC读取时Expression为空
                        break;

                    default:
                        _parameter.Expression = txtAssignmentContent?.Text ?? "";
                        _parameter.DataSource.SourceType = DataSourceType.Variable; // 默认
                        break;
                }

                _hasUnsavedChanges = false;
                Logger?.LogDebug("表单数据已保存到参数对象");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存表单数据失败");
                throw;
            }
        }
        #endregion

        #region PLC功能支持

        /// <summary>
        /// 加载所有PLC参数
        /// 从PLCManager获取所有可用的PLC模块
        /// </summary>
        private async Task Parameterplcs()
        {
            if (CboPlcModule.Items.Count > 0) return; // 避免重复加载

            CboPlcModule.Clear();
            CboPlcAddress.Clear();
            try
            {
                var modules = await PLCManager.GetModuleTagsAsync();
                foreach (var module in modules)
                {
                    CboPlcModule.Items.Add(module.Key);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC模块失败");
            }
        }

        /// <summary>
        /// 加载指定PLC模块的地址列表
        /// 根据选中的模块名异步获取该模块的所有可用地址
        /// </summary>
        /// <param name="moduleName">PLC模块名称</param>
        private async Task LoadPlcAddresses(string moduleName)
        {
            if (string.IsNullOrEmpty(moduleName) || CboPlcAddress == null) return;

            try
            {
                CboPlcAddress.Clear();
                var modules = await PLCManager.GetModuleTagsAsync();
                if (modules.TryGetValue(moduleName, out List<string> addresses))
                {
                    foreach (var address in addresses)
                    {
                        CboPlcAddress.Items.Add(address);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC模块[{moduleName}]地址失败", moduleName);
            }
        }

        /// <summary>
        /// 安全设置ComboBox的值
        /// 处理各种ComboBox类型的值设置，包含容错机制
        /// </summary>
        /// <param name="comboBox">要设置的ComboBox控件</param>
        /// <param name="value">要设置的值</param>
        private void SetComboBoxValue(UIComboBox comboBox, string value)
        {
            if (comboBox == null || string.IsNullOrEmpty(value)) return;

            try
            {
                // 方法1：先尝试在Items中查找匹配项
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (comboBox.Items[i].ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                }

                // 方法2：如果没有找到匹配项，且ComboBox允许编辑，则设置Text属性
                if (comboBox.DropDownStyle == UIDropDownStyle.DropDown)
                {
                    comboBox.Text = value;
                }
                else
                {
                    // 方法3：对于DropDownList类型，如果找不到匹配项，添加该项
                    comboBox.Items.Add(value);
                    comboBox.SelectedItem = value;
                }
            }
            catch (Exception)
            {
                Logger?.LogWarning("设置ComboBox值失败: {value}", value);
                // 最后的备用方案
                try
                {
                    comboBox.Text = value;
                }
                catch
                {
                    // 忽略最终的设置失败
                }
            }
        }

        #endregion

        #region 验证逻辑

        /// <summary>
        /// 验证参数（基类方法实现）
        /// 调用内部的验证逻辑，供基类框架使用
        /// </summary>
        /// <returns>true: 验证通过; false: 验证失败</returns>
        protected override bool ValidateParameters() => ValidateInput();

        /// <summary>
        /// 验证用户输入的完整性和正确性
        /// 执行全面的输入验证，包括必填项检查和业务逻辑验证
        /// </summary>
        /// <returns>true: 所有验证通过; false: 存在验证错误</returns>
        private bool ValidateInput()
        {
            try
            {
                // 验证目标变量名
                if (string.IsNullOrWhiteSpace(cmbTargetVariable?.Text))
                {
                    MessageHelper.MessageOK("请选择或输入目标变量名", TType.Warn);
                    cmbTargetVariable?.Focus();
                    return false;
                }

                // 验证目标变量是否存在
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                var targetVar = globalVariableManager?.GetAllVariables();
                if (targetVar == null)
                {
                    MessageHelper.MessageOK($"目标变量 '{cmbTargetVariable.Text}' 不存在", TType.Warn);
                    cmbTargetVariable?.Focus();
                    return false;
                }

                // 根据赋值类型进行特定验证
                var assignmentType = (AssignmentTypeEnum)cmbAssignmentType.SelectedValue;
                switch (assignmentType)
                {
                    case AssignmentTypeEnum.DirectAssignment:
                    case AssignmentTypeEnum.ExpressionCalculation:
                        if (string.IsNullOrWhiteSpace(txtAssignmentContent?.Text))
                        {
                            MessageHelper.MessageOK("赋值内容不能为空", TType.Warn);
                            txtAssignmentContent?.Focus();
                            return false;
                        }

                        // 验证表达式语法
                        if (_expressionValidator != null)
                        {
                            var context = new ValidationContext
                            {
                                TargetVariableName = cmbTargetVariable.Text,
                                //TargetVariableType = targetVar.VarType
                            };

                            var expressionResult = _expressionValidator.ValidateExpression(txtAssignmentContent.Text, context);
                            if (!expressionResult.IsValid)
                            {
                                var errorMsg = string.Join("\n", expressionResult.Errors);
                                MessageHelper.MessageOK($"表达式语法错误：\n{errorMsg}", TType.Error);
                                txtAssignmentContent?.Focus();
                                return false;
                            }
                        }
                        break;

                    case AssignmentTypeEnum.VariableCopy:
                        if (string.IsNullOrWhiteSpace(txtAssignmentContent?.Text))
                        {
                            MessageHelper.MessageOK("请输入源变量名", TType.Warn);
                            txtAssignmentContent?.Focus();
                            return false;
                        }

                        // 验证源变量是否存在
                        var sourceVarName = txtAssignmentContent.Text.Trim();
                        if (sourceVarName.StartsWith("{") && sourceVarName.EndsWith("}"))
                        {
                            sourceVarName = sourceVarName.Substring(1, sourceVarName.Length - 2);
                        }

                        var sourceVar = globalVariableManager?.GetAllVariables();
                        if (sourceVar == null)
                        {
                            MessageHelper.MessageOK($"源变量 '{sourceVarName}' 不存在", TType.Warn);
                            txtAssignmentContent?.Focus();
                            return false;
                        }
                        break;

                    case AssignmentTypeEnum.PLCRead:
                        if (string.IsNullOrWhiteSpace(CboPlcModule?.Text))
                        {
                            MessageHelper.MessageOK("请选择PLC模块", TType.Warn);
                            CboPlcModule?.Focus();
                            return false;
                        }

                        if (string.IsNullOrWhiteSpace(CboPlcAddress?.Text))
                        {
                            MessageHelper.MessageOK("请选择或输入PLC地址", TType.Warn);
                            CboPlcAddress?.Focus();
                            return false;
                        }
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入时发生异常");
                MessageHelper.MessageOK($"验证失败：{ex.Message}", TType.Error);
                return false;
            }
        }

        /// <summary>
        /// 异步验证配置的完整性
        /// 提供更全面的配置验证，返回详细的验证结果
        /// 主要用于实时验证和状态显示，同时更新验证结果界面
        /// </summary>
        /// <returns>包含验证结果和错误信息的ValidationResult对象</returns>
        private ValidationResult ValidateConfigurationAsync()
        {
            try
            {
                var parameter = GetParameter();
                var errors = new List<string>();
                var warnings = new List<string>();

                // 基本验证
                if (string.IsNullOrWhiteSpace(parameter.TargetVarName))
                {
                    errors.Add("目标变量名不能为空");
                }
                else
                {
                    // 验证目标变量是否存在
                    var targetVar = GetTargetVariableType();
                    if (targetVar == null)
                    {
                        errors.Add($"目标变量 '{parameter.TargetVarName}' 不存在");
                    }
                }

                // 根据赋值类型进行不同的验证
                switch (parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.DirectAssignment:
                        if (string.IsNullOrWhiteSpace(parameter.Expression))
                        {
                            errors.Add("直接赋值的值不能为空");
                        }
                        break;

                    case AssignmentTypeEnum.ExpressionCalculation:
                        if (string.IsNullOrWhiteSpace(parameter.Expression))
                        {
                            errors.Add("表达式不能为空");
                        }
                        else
                        {
                            // 验证表达式语法
                            var context = new ValidationContext
                            {
                                TargetVariableName = parameter.TargetVarName,
                                TargetVariableType = GetTargetVariableType()
                            };

                            var expressionResult = _expressionValidator?.ValidateExpression(parameter.Expression, context);
                            if (expressionResult != null)
                            {
                                if (!expressionResult.IsValid)
                                    errors.AddRange(expressionResult.Errors);
                                warnings.AddRange(expressionResult.Warnings);
                            }
                        }
                        break;

                    case AssignmentTypeEnum.VariableCopy:
                        if (string.IsNullOrWhiteSpace(parameter.Expression))
                        {
                            errors.Add("源变量名不能为空");
                        }
                        else
                        {
                            // 验证源变量是否存在
                            var sourceVarName = parameter.Expression.Trim();
                            if (sourceVarName.StartsWith("{") && sourceVarName.EndsWith("}"))
                            {
                                sourceVarName = sourceVarName.Substring(1, sourceVarName.Length - 2);
                            }

                            var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                            var sourceVar = globalVariableManager?.FindVariableByName(sourceVarName);
                            if (sourceVar == null)
                            {
                                errors.Add($"源变量 '{sourceVarName}' 不存在");
                            }
                        }
                        break;

                    case AssignmentTypeEnum.PLCRead:
                        // PLC读取验证
                        if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.ModuleName))
                        {
                            errors.Add("请选择PLC模块");
                        }
                        if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.Address))
                        {
                            errors.Add("请选择PLC地址");
                        }
                        break;
                }

                // 条件验证（可选）
                if (!string.IsNullOrWhiteSpace(parameter.Condition))
                {
                    var conditionResult = _expressionValidator?.ValidateExpression(parameter.Condition, new ValidationContext());
                    if (conditionResult != null && !conditionResult.IsValid)
                    {
                        errors.Add("执行条件表达式错误");
                        errors.AddRange(conditionResult.Errors.Select(e => $"条件错误: {e}"));
                    }
                }

                var combinedResult = new ValidationResult
                {
                    IsValid = errors.Count == 0,
                    Errors = errors,
                    Warnings = warnings,
                    Message = GenerateValidationMessage(new ValidationResult
                    {
                        IsValid = errors.Count == 0,
                        Errors = errors,
                        Warnings = warnings
                    })
                };

                // 更新UI显示
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => UpdateValidationUI(combinedResult)));
                }
                else
                {
                    UpdateValidationUI(combinedResult);
                }

                return combinedResult;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "异步验证配置时发生错误");
                var errorResult = ValidationResult.Error($"验证过程发生错误: {ex.Message}");
                UpdateValidationUI(errorResult);
                return errorResult;
            }
        }

        /// <summary>
        /// 生成验证消息
        /// 根据验证结果创建格式化的消息文本
        /// </summary>
        /// <param name="result">验证结果</param>
        /// <returns>格式化的验证消息</returns>
        private string GenerateValidationMessage(ValidationResult result)
        {
            var messages = new List<string>();

            if (result.IsValid)
            {
                messages.Add("✓ 配置验证通过");

                // 根据赋值类型添加特定提示
                var parameter = GetParameter();
                switch (parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.PLCRead:
                        messages.Add($"  PLC模块: {parameter.DataSource.PlcConfig.ModuleName}");
                        messages.Add($"  PLC地址: {parameter.DataSource.PlcConfig.Address}");
                        break;
                    case AssignmentTypeEnum.ExpressionCalculation:
                        messages.Add("  表达式语法正确");
                        break;
                    case AssignmentTypeEnum.VariableCopy:
                        messages.Add("  源变量存在且可访问");
                        break;
                }
            }

            if (result.Errors?.Any() == true)
            {
                messages.Add("❌ 发现错误：");
                messages.AddRange(result.Errors.Select(e => $"  • {e}"));
            }

            if (result.Warnings?.Any() == true)
            {
                messages.Add("⚠️ 警告信息：");
                messages.AddRange(result.Warnings.Select(w => $"  • {w}"));
            }

            return string.Join("\n", messages);
        }

        /// <summary>
        /// 获取目标变量类型
        /// 从全局变量管理器中查找目标变量并返回其类型
        /// </summary>
        /// <returns>变量类型字符串，未找到时返回null</returns>
        private string GetTargetVariableType()
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                var targetVar = globalVariableManager?.FindVariableByName(cmbTargetVariable?.Text);
                return targetVar?.VarType;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// PLC模块选择改变事件
        /// 当用户选择不同PLC模块时，重新加载该模块的地址列表
        /// </summary>
        private async void CboPlcModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            var selectedModule = CboPlcModule?.Text;
            if (!string.IsNullOrEmpty(selectedModule))
            {
                await LoadPlcAddresses(selectedModule);
            }

            _hasUnsavedChanges = true;

            // 触发验证更新
            RestartValidationTimer();
        }

        /// <summary>
        /// PLC地址选择改变事件
        /// 当用户选择PLC地址时触发验证更新
        /// </summary>
        private void CboPlcAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            _hasUnsavedChanges = true;

            // 触发验证更新
            RestartValidationTimer();
        }

        /// <summary>
        /// 目标变量选择改变事件
        /// 当用户选择不同目标变量时，触发验证和预览更新
        /// </summary>
        private void CmbTargetVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            RestartValidationTimer();
            RestartPreviewTimer();
        }

        /// <summary>
        /// 赋值方式选择改变事件
        /// 当用户选择不同赋值方式时，更新界面布局和清空内容
        /// </summary>
        private void CmbAssignmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            txtAssignmentContent.Text = string.Empty;
            UpdateUIBasedOnAssignmentType();
            _hasUnsavedChanges = true;
            RestartValidationTimer();
            RestartPreviewTimer();
        }

        /// <summary>
        /// 赋值内容改变事件
        /// 当用户修改赋值内容时，触发验证和预览更新
        /// </summary>
        private void TxtAssignmentContent_TextChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            RestartValidationTimer();
            RestartPreviewTimer();
        }

        /// <summary>
        /// 执行条件改变事件
        /// 当用户修改执行条件时，触发验证更新
        /// </summary>
        private void TxtCondition_TextChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            RestartValidationTimer();
        }

        /// <summary>
        /// 描述改变事件
        /// 当用户修改描述信息时，标记有未保存更改
        /// </summary>
        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
        }

        /// <summary>
        /// 启用状态改变事件
        /// 当用户切换启用/禁用状态时，触发验证更新
        /// </summary>
        private void ChkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            RestartValidationTimer();
        }

        #endregion

        #region UI更新方法

        /// <summary>
        /// 根据赋值类型更新UI
        /// 不同的赋值类型需要显示不同的配置控件和提示信息
        /// </summary>
        private async void UpdateUIBasedOnAssignmentType()
        {
            if (cmbAssignmentType?.SelectedValue == null) return;

            var selectedType = (AssignmentTypeEnum)cmbAssignmentType.SelectedValue;

            // 显示/隐藏相应的面板
            pnlVoluationSource.Visible = true;
            pnlPlcSource.Visible = false;

            switch (selectedType)
            {
                case AssignmentTypeEnum.ExpressionCalculation:
                    txtAssignmentContent.Watermark = "输入计算表达式，如：{Var1} + {Var2} * 2";
                    btnExpressionBuilder.Visible = true;
                    break;
                case AssignmentTypeEnum.VariableCopy:
                    txtAssignmentContent.Watermark = "输入源变量名或使用 {变量名} 格式";
                    btnExpressionBuilder.Visible = false;
                    break;
                case AssignmentTypeEnum.PLCRead:
                    txtAssignmentContent.Watermark = "输入PLC地址";
                    btnExpressionBuilder.Visible = false;
                    pnlVoluationSource.Visible = false;
                    pnlPlcSource.Visible = true;
                    if (CboPlcModule.Items.Count == 0)
                        await Parameterplcs();
                    break;
                case AssignmentTypeEnum.DirectAssignment:
                    txtAssignmentContent.Watermark = "输入要赋的值，字符串请用引号包围";
                    btnExpressionBuilder.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// 更新验证UI
        /// 根据验证结果更新验证结果显示区域的内容和样式
        /// </summary>
        /// <param name="result">验证结果</param>
        private void UpdateValidationUI(ValidationResult result)
        {
            try
            {
                if (rtbValidationResult != null)
                {
                    rtbValidationResult.Clear();
                    rtbValidationResult.Text = result.Message;

                    // 根据验证结果设置颜色和按钮状态
                    if (result.IsValid)
                    {
                        rtbValidationResult.ForeColor = Color.FromArgb(40, 167, 69); // 绿色

                        // 有警告时使用橙色
                        if (result.Warnings?.Any() == true)
                        {
                            rtbValidationResult.ForeColor = Color.FromArgb(255, 193, 7); // 橙色
                        }

                        if (btnOK != null) btnOK.Enabled = true;
                        if (btnTest != null) btnTest.Enabled = true;
                    }
                    else
                    {
                        rtbValidationResult.ForeColor = Color.FromArgb(220, 53, 69); // 红色
                        if (btnOK != null) btnOK.Enabled = false;
                        if (btnTest != null) btnTest.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "更新验证UI失败");
            }
        }

        #endregion

        #region 定时器方法

        /// <summary>
        /// 重启验证定时器
        /// 停止当前定时器并重新开始计时，实现延迟验证效果
        /// </summary>
        private void RestartValidationTimer()
        {
            _validationTimer?.Stop();
            _validationTimer?.Start();
        }

        /// <summary>
        /// 重启预览定时器
        /// 停止当前定时器并重新开始计时，实现延迟预览更新效果
        /// </summary>
        private void RestartPreviewTimer()
        {
            _previewTimer?.Stop();
            _previewTimer?.Start();
        }

        #endregion

        #region 预览功能

        /// <summary>
        /// 异步刷新预览
        /// 根据当前配置实时显示预览结果
        /// 包括目标变量信息、当前值和预期值等
        /// </summary>
        private void RefreshPreviewAsync()
        {
            try
            {
                var previewLines = new List<string>();
                var parameter = GetParameter();

                // 基本信息
                previewLines.Add($"目标变量：{parameter.TargetVarName}");
                previewLines.Add(item: $"赋值方式：{parameter.AssignmentType.GetDescription()}");

                // 根据赋值类型显示内容
                switch (parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.PLCRead:
                        previewLines.Add($"PLC模块：{parameter.DataSource.PlcConfig.ModuleName}");
                        previewLines.Add($"PLC地址：{parameter.DataSource.PlcConfig.Address}");
                        break;
                    default:
                        previewLines.Add($"赋值内容：{parameter.Expression}");
                        break;
                }

                if (!string.IsNullOrWhiteSpace(parameter.Condition))
                {
                    previewLines.Add($"执行条件：{parameter.Condition}");
                }

                previewLines.Add($"状态：{(parameter.IsAssignment ? "启用" : "禁用")}");
                previewLines.Add("");

                // 计算预期结果
                try
                {
                    var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                    var targetVar = globalVariableManager?.FindVariableByName(parameter.TargetVarName);
                    if (targetVar != null)
                    {
                        previewLines.Add($"当前值：{targetVar.VarValue ?? "null"}");

                        // 使用表达式验证器计算预期值
                        var calculationResult = _expressionValidator?.CalculateExpectedValue(parameter.Expression);
                        if (calculationResult?.Success == true)
                        {
                            previewLines.Add($"预期值：{calculationResult.Value ?? "null"}");
                        }
                        else
                        {
                            previewLines.Add($"预期值：计算失败 - {calculationResult?.ErrorMessage}");
                        }
                    }
                    else
                    {
                        previewLines.Add("变量不存在或未找到");
                    }
                }
                catch (Exception ex)
                {
                    previewLines.Add($"预览计算异常：{ex.Message}");
                }

                // 更新预览界面
                var previewText = string.Join("\n", previewLines);
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (rtbPreviewResult != null)
                            rtbPreviewResult.Text = previewText;
                    }));
                }
                else
                {
                    if (rtbPreviewResult != null)
                        rtbPreviewResult.Text = previewText;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "刷新预览时发生错误");
            }
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 确定按钮点击事件处理器
        /// 执行最终验证，保存参数，并关闭窗体
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                btnOK.Enabled = false;
                btnOK.Text = "处理中...";

                if (!ValidateInput())
                    return;

                SaveFormToParameter();

                // 使用基类方法，保存参数到流程中
                SaveParameters();

                _hasUnsavedChanges = false;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "确定按钮处理时发生错误");
                MessageHelper.MessageOK($"操作失败：{ex.Message}", TType.Error);
            }
            finally
            {
                btnOK.Enabled = true;
                btnOK.Text = "确定";
            }
        }

        /// <summary>
        /// 取消按钮点击事件处理器
        /// 不保存任何更改，直接关闭窗体
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 测试按钮点击事件处理器
        /// 在不保存配置的情况下测试当前的赋值配置是否能够正常执行
        /// </summary>
        private async void BtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                btnTest.Enabled = false;
                btnTest.Text = "测试中...";

                // 先验证配置
                var validationResult = ValidateConfigurationAsync();
                if (!validationResult.IsValid)
                {
                    MessageHelper.MessageOK($"配置验证失败，无法测试：\n{string.Join("\n", validationResult.Errors)}", TType.Warn);
                    return;
                }

                // 获取当前参数
                var parameter = GetParameter();
                parameter.Condition = txtCondition?.Text?.Trim(); // 确保包含条件

                // 执行测试赋值
                if (_assignmentEngine != null)
                {
                    var testResult = await _assignmentEngine.ExecuteAssignmentAsync(parameter);

                    if (testResult.Success)
                    {
                        if (testResult.Skipped)
                        {
                            MessageHelper.MessageOK($"测试完成（已跳过）：\n{testResult.SkipReason}", TType.Info);
                        }
                        else
                        {
                            var message = $"测试成功！\n" +
                                        $"变量：{testResult.TargetVariableName}\n" +
                                        $"原值：{testResult.OldValue ?? "null"}\n" +
                                        $"新值：{testResult.NewValue ?? "null"}";

                            MessageHelper.MessageOK(message, TType.Success);

                            // 刷新预览以显示新值
                            RestartPreviewTimer();
                        }
                    }
                    else
                    {
                        MessageHelper.MessageOK($"测试失败：\n{testResult.ErrorMessage}", TType.Error);
                    }
                }
                else
                {
                    MessageHelper.MessageOK("赋值引擎不可用，无法执行测试", TType.Error);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "测试执行失败");
                MessageHelper.MessageOK($"测试失败：{ex.Message}", TType.Error);
            }
            finally
            {
                btnTest.Enabled = true;
                btnTest.Text = "测试";
            }
        }

        /// <summary>
        /// 表达式构建器按钮点击事件处理器
        /// 打开表达式构建器窗体，帮助用户构建复杂表达式
        /// </summary>
        private void BtnExpressionBuilder_Click(object sender, EventArgs e)
        {
            try
            {
                // 创建表达式构建器对话框
                using var builder = new ExpressionBuilderDialog(GlobalVariable, _expressionValidator)
                {
                    InitialExpression = txtAssignmentContent?.Text ?? "",
                    TargetVariableType = GetTargetVariableType(),
                    StartPosition = FormStartPosition.CenterParent
                };

                // 显示对话框
                if (builder.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(builder.GeneratedExpression))
                {
                    // 将生成的表达式设置到文本框
                    txtAssignmentContent.Text = builder.GeneratedExpression;

                    // 标记有未保存的更改
                    _hasUnsavedChanges = true;

                    // 重启验证和预览定时器
                    RestartValidationTimer();
                    RestartPreviewTimer();

                    Logger?.LogDebug("表达式构建器生成表达式: {Expression}", builder.GeneratedExpression);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "打开表达式构建器时发生错误");
                MessageHelper.MessageOK($"打开表达式构建器失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 帮助按钮点击事件处理器
        /// 显示帮助信息，指导用户如何使用变量赋值功能
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                var helpText = @"变量赋值工具使用说明：

1. 赋值方式：
   - 直接赋值：直接设置变量的值
   - 表达式计算：使用数学表达式计算结果
   - 从其他变量复制：复制其他变量的值
   - 从PLC读取：从PLC地址读取值

2. 表达式语法：
   - 使用 {变量名} 引用变量
   - 支持四则运算：+、-、*、/
   - 支持函数：Math.Sin、Math.Cos等

3. 执行条件：
   - 可选设置，为空时总是执行
   - 使用布尔表达式，如：{Var1} > 10

4. 测试功能：
   - 点击测试按钮可以验证配置
   - 不会影响实际流程执行";

                MessageHelper.MessageOK(helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助时发生错误");
            }
        }

        #endregion

        #region 基类重写和接口实现

        /// <summary>
        /// 从步骤参数加载（基类方法重写）
        /// 处理从工作流步骤中加载参数的逻辑
        /// </summary>
        /// <param name="stepParameter">步骤参数对象</param>
        protected override void LoadParameterFromStep(object stepParameter)
        {
            try
            {
                Parameter_VariableAssignment loadedParameter = null;

                // 尝试直接类型转换
                if (stepParameter is Parameter_VariableAssignment directParam)
                {
                    loadedParameter = directParam;
                    Logger?.LogDebug("直接获取Parameter_VariableAssignment参数");
                }
                // 尝试JSON反序列化
                else if (stepParameter != null)
                {
                    try
                    {
                        string jsonString = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                        loadedParameter = JsonConvert.DeserializeObject<Parameter_VariableAssignment>(jsonString);
                        Logger?.LogDebug("JSON反序列化获取Parameter_VariableAssignment参数");
                    }
                    catch (JsonException jsonEx)
                    {
                        Logger?.LogWarning(jsonEx, "JSON反序列化失败，使用默认参数");
                        loadedParameter = null;
                    }
                }

                // 如果加载成功，设置参数并加载到界面
                if (loadedParameter != null)
                {
                    _parameter = loadedParameter;
                    Logger?.LogInformation("成功加载变量赋值参数: {Description}", _parameter.Description);
                }
                else
                {
                    // 加载失败，使用默认值
                    Logger?.LogWarning("加载参数失败，使用默认参数");
                    SetDefaultValues();
                    return;
                }

                // 加载参数到表单控件
                LoadParameterToForm();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数时发生错误");
                MessageHelper.MessageOK($"加载参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 收集参数（基类方法重写）
        /// 供基类框架调用，返回通用的参数对象
        /// </summary>
        /// <returns>当前配置的参数对象</returns>
        protected override object CollectParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        /// <summary>
        /// 填充控件（IParameterForm接口实现）
        /// 将参数对象的值填充到界面控件中
        /// </summary>
        /// <param name="parameter">要填充的参数对象</param>
        public void PopulateControls(Parameter_VariableAssignment parameter) => Parameter = parameter;

        /// <summary>
        /// 设置默认值（IParameterForm接口实现）
        /// 调用内部的默认值设置方法
        /// </summary>
        void IParameterForm<Parameter_VariableAssignment>.SetDefaultValues() => SetDefaultValues();

        /// <summary>
        /// 验证类型化参数（IParameterForm接口实现）
        /// 验证当前参数配置的有效性
        /// </summary>
        /// <returns>true: 验证通过; false: 验证失败</returns>
        public bool ValidateTypedParameters() => ValidateInput();

        /// <summary>
        /// 收集类型化参数（IParameterForm接口实现）
        /// 从界面控件收集数据并返回参数对象
        /// </summary>
        /// <returns>当前配置的参数对象</returns>
        public Parameter_VariableAssignment CollectTypedParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        /// <summary>
        /// 转换参数对象（IParameterForm接口实现）
        /// 将通用对象转换为特定的参数类型
        /// 支持直接转换和JSON反序列化两种方式
        /// </summary>
        /// <param name="stepParameter">要转换的参数对象</param>
        /// <returns>转换后的Parameter_VariableAssignment对象</returns>
        public Parameter_VariableAssignment ConvertParameter(object stepParameter)
        {
            if (stepParameter is Parameter_VariableAssignment paramObj)
                return paramObj;

            if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Parameter_VariableAssignment>(jsonStr)
                        ?? new Parameter_VariableAssignment();
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "反序列化参数失败");
                    return new Parameter_VariableAssignment();
                }
            }

            return new Parameter_VariableAssignment();
        }

        #endregion

        #region 窗体生命周期

        /// <summary>
        /// 窗体关闭事件处理器
        /// 执行必要的清理工作，检查未保存的更改
        /// </summary>
        /// <param name="sender">窗体对象</param>
        /// <param name="e">窗体关闭事件参数</param>
        private void Form_VariableAssignment_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 停止定时器以防止在关闭过程中触发
                _validationTimer?.Stop();
                _previewTimer?.Stop();

                // 如果有未保存的更改且用户点击了取消，可以选择提示用户
                if (_hasUnsavedChanges && DialogResult != DialogResult.OK)
                {
                    // 这里可以添加未保存更改的提示逻辑
                    // 根据实际需求决定是否实现
                    Logger?.LogDebug("窗体关闭时存在未保存的更改");
                }

                Logger?.LogDebug("变量赋值工具窗体正在关闭");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "窗体关闭事件处理时发生错误");
            }
        }

        #endregion
    }

    #region 辅助枚举

    /// <summary>
    /// 赋值方式枚举
    /// 定义变量赋值支持的各种方式
    /// </summary>
    public enum AssignmentTypeEnum
    {
        /// <summary>
        /// 直接赋值 - 将固定值直接赋给目标变量
        /// </summary>
        [Description("直接赋值")]
        DirectAssignment,

        /// <summary>
        /// 表达式计算 - 通过数学表达式计算结果后赋值
        /// </summary>
        [Description("表达式计算")]
        ExpressionCalculation,

        /// <summary>
        /// 从其他变量复制 - 将其他变量的值复制到目标变量
        /// </summary>
        [Description("从其他变量复制")]
        VariableCopy,

        /// <summary>
        /// 从PLC读取 - 从指定的PLC模块和地址读取值
        /// </summary>
        [Description("从PLC读取")]
        PLCRead
    }

    #endregion
}
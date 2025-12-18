using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 参数表单基类 - 非泛型版本，完全兼容设计器
    /// 提供统一的参数管理、加载、保存逻辑
    /// 子类只需定义 Parameter 属性并重写虚方法即可
    /// </summary>
    public class BaseParameterForm : UIForm
    {
        #region 私有字段

        private bool _isLoading = true;

        // 依赖注入的服务
        protected readonly IPLCManager _plcManager;
        protected readonly IWorkflowStateService _workflowState;
        protected readonly GlobalVariableManager _globalVariable;
        protected readonly Microsoft.Extensions.Logging.ILogger _logger;

        #endregion

        #region 属性

        /// <summary>
        /// 是否正在加载中
        /// </summary>
        protected bool IsLoading => _isLoading;

        #endregion

        #region 构造函数

        /// <summary>
        /// 无参构造函数 - 供设计器使用
        /// </summary>
        public BaseParameterForm()
        {
            if (DesignMode) return;

            // 运行时从全局服务提供者获取服务
            try
            {
                _plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
                _workflowState = Program.ServiceProvider?.GetService<IWorkflowStateService>();
                _logger = Program.ServiceProvider?.GetService<ILogger<BaseParameterForm>>();
                _globalVariable = Program.ServiceProvider?.GetService<GlobalVariableManager>();

                if (_workflowState == null || _logger == null)
                {
                    throw new InvalidOperationException(
                        "无法获取必需的服务。请确保已正确配置服务提供者。");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BaseParameterForm构造函数警告: {ex.Message}");
            }
        }

        /// <summary>
        /// 依赖注入构造函数（推荐）
        /// </summary>
        protected BaseParameterForm(IWorkflowStateService workflowState, Microsoft.Extensions.Logging.ILogger logger)
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
            _globalVariable = Program.ServiceProvider?.GetService<GlobalVariableManager>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region 生命周期方法

        /// <summary>
        /// 窗体加载事件 - 自动从工作流加载参数
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;

            try
            {
                LoadParametersFromWorkflow();
            }
            finally
            {
                _isLoading = false;
            }
        }

        #endregion

        #region 参数管理核心方法

        /// <summary>
        /// 从工作流加载参数 - 统一逻辑
        /// </summary>
        protected virtual void LoadParametersFromWorkflow()
        {
            if (DesignMode || WorkflowState == null) return;

            var currentStep = GetCurrentStepSafely();
            if (currentStep?.StepParameter != null)
            {
                try
                {
                    // 调用子类的参数转换方法
                    var convertedParameter = ConvertParameter(currentStep.StepParameter);

                    // 设置参数（通过反射访问子类的 Parameter 属性）
                    SetParameterValue(convertedParameter);

                    Logger?.LogInformation("成功加载参数: {FormType}", GetType().Name);

                    // 调用子类的加载方法
                    LoadParameterToForm();
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "参数转换失败: {FormType}", GetType().Name);
                    SetDefaultValues();
                }
            }
            else
            {
                SetDefaultValues();
            }
        }

        /// <summary>
        /// 保存参数到工作流 - 统一的保存逻辑
        /// </summary>
        protected virtual void SaveParameters()
        {
            if (DesignMode || WorkflowState == null) return;

            try
            {
                var currentStep = GetCurrentStepSafely();
                if (currentStep == null)
                {
                    Logger?.LogWarning("步骤索引无效，无法保存参数: StepNum={StepNum}", WorkflowState.StepNum);
                    MessageHelper.MessageOK("步骤索引无效，无法保存参数。", TType.Error);
                    return;
                }

                if (!ValidateInput())
                {
                    Logger?.LogWarning("参数验证失败: {FormType}", GetType().Name);
                    return;
                }

                // 调用子类方法将界面数据保存到参数对象
                SaveFormToParameter();

                // 获取参数对象（通过反射访问子类的 Parameter 属性）
                var parameter = GetParameterValue();

                // 清理花括号
                CleanBracketsFromProperties(parameter);

                // 更新到工作流
                WorkflowState.UpdateStepParameter(WorkflowState.StepNum, parameter);

                Logger?.LogInformation("参数保存成功: {FormType}, StepNum={StepNum}",
                    GetType().Name, WorkflowState.StepNum);

                MessageHelper.MessageOK("参数已暂存，主界面点击保存后才会写入文件。", TType.Info);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存参数失败: {FormType}", GetType().Name);
                MessageHelper.MessageOK($"保存参数失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 反射辅助方法

        /// <summary>
        /// 通过反射获取子类的 Parameter 属性值
        /// </summary>
        protected virtual object GetParameterValue()
        {
            var parameterProperty = GetType().GetProperty("Parameter");
            if (parameterProperty != null && parameterProperty.CanRead)
            {
                return parameterProperty.GetValue(this);
            }

            Logger?.LogWarning("未找到 Parameter 属性: {FormType}", GetType().Name);
            return null;
        }

        /// <summary>
        /// 通过反射设置子类的 Parameter 属性值
        /// </summary>
        protected virtual void SetParameterValue(object parameter)
        {
            if (parameter == null) return;

            // 加载时还原花括号
            RestoreBracketsToProperties(parameter);

            var parameterProperty = GetType().GetProperty("Parameter");
            if (parameterProperty != null && parameterProperty.CanWrite)
            {
                parameterProperty.SetValue(this, parameter);
            }
        }

        /// <summary>
        /// 获取子类 Parameter 属性的类型
        /// </summary>
        protected virtual Type GetParameterType()
        {
            var parameterProperty = GetType().GetProperty("Parameter");
            return parameterProperty?.PropertyType;
        }

        /// <summary>
        /// 清理参数对象中特定属性的花括号
        /// </summary>
        private void CleanBracketsFromProperties(object parameter)
        {
            if (parameter == null) return;

            var type = parameter.GetType();
            var properties = type.GetProperties();

            foreach (var prop in properties)
            {
                // 需要清理花括号的属性名模式
                if (prop.Name.EndsWith("VarName") ||
                    prop.Name == "TargetVariable" ||
                    prop.Name == "SourceVariable")
                {
                    if (prop.PropertyType == typeof(string) && prop.CanWrite)
                    {
                        var value = prop.GetValue(parameter) as string;
                        if (!string.IsNullOrEmpty(value))
                        {
                            var cleaned = value.Trim().Trim('{', '}');
                            prop.SetValue(parameter, cleaned);
                        }
                    }
                }
            }
        }

        #endregion

        #region 参数转换

        /// <summary>
        /// 还原参数对象中特定属性的花括号（用于界面显示和验证）
        /// </summary>
        private void RestoreBracketsToProperties(object parameter)
        {
            if (parameter == null) return;

            var type = parameter.GetType();
            var properties = type.GetProperties();

            foreach (var prop in properties)
            {
                // 需要还原花括号的属性名模式
                if (prop.Name.EndsWith("VarName") ||
                    prop.Name == "TargetVariable" ||
                    prop.Name == "SourceVariable")
                {
                    if (prop.PropertyType == typeof(string) && prop.CanWrite)
                    {
                        var value = prop.GetValue(parameter) as string;
                        if (!string.IsNullOrEmpty(value) && !value.StartsWith("{"))
                        {
                            prop.SetValue(parameter, $"{{{value}}}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 统一的参数转换逻辑 - 支持直接转换和JSON反序列化
        /// 子类可以重写此方法以提供特定类型的转换
        /// </summary>
        protected virtual object ConvertParameter(object stepParameter)
        {
            var parameterType = GetParameterType();
            if (parameterType == null)
                return stepParameter;

            // 处理 null 或数值类型（0, -1等初始值）
            if (stepParameter == null ||
                stepParameter is int ||
                stepParameter is long ||
                stepParameter is decimal ||
                stepParameter is double ||
                stepParameter is float ||
                stepParameter is short ||
                stepParameter is byte)
            {
                Logger?.LogDebug("参数为初始值({Value})，创建默认参数: {Type}",
                    stepParameter, parameterType.Name);
                try
                {
                    return Activator.CreateInstance(parameterType);
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "创建默认参数失败: {Type}", parameterType.Name);
                    return null;
                }
            }

            // 1. 尝试直接类型转换
            if (parameterType.IsInstanceOfType(stepParameter))
            {
                return stepParameter;
            }

            // 2. 尝试 JSON 字符串反序列化
            if (stepParameter is string json && !string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    return JsonConvert.DeserializeObject(json, parameterType);
                }
                catch (JsonException ex)
                {
                    Logger?.LogWarning(ex, "JSON反序列化失败，使用默认参数");
                }
            }

            // 3. 尝试序列化再反序列化（处理匿名对象）
            try
            {
                string jsonString = JsonConvert.SerializeObject(stepParameter);
                return JsonConvert.DeserializeObject(jsonString, parameterType);
            }
            catch (JsonException ex)
            {
                Logger?.LogWarning(ex, "对象转换失败，使用默认参数");
            }

            // 4. 最终兜底 - 创建默认实例
            Logger?.LogDebug("所有转换方法失败，返回默认参数实例");
            try
            {
                return Activator.CreateInstance(parameterType);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "创建默认参数实例失败");
                return null;
            }
        }

        #endregion

        #region 虚方法 - 子类按需重写

        /// <summary>
        /// 加载参数到界面控件
        /// 子类必须重写此方法，将参数的值填充到界面控件
        /// </summary>
        protected virtual void LoadParameterToForm()
        {
            // 子类实现：从 Parameter 读取数据并填充到控件
        }

        /// <summary>
        /// 从界面控件保存到参数
        /// 子类必须重写此方法，将界面控件的值保存到 Parameter
        /// </summary>
        protected virtual void SaveFormToParameter()
        {
            // 子类实现：从控件读取数据并保存到 Parameter
        }

        /// <summary>
        /// 设置默认值
        /// 子类可以重写此方法，设置参数的默认值
        /// </summary>
        protected virtual void SetDefaultValues()
        {
            // 尝试创建默认的参数对象
            var parameterType = GetParameterType();
            if (parameterType != null)
            {
                try
                {
                    var defaultParameter = Activator.CreateInstance(parameterType);
                    SetParameterValue(defaultParameter);
                    Logger?.LogDebug("使用默认参数: {ParameterType}", parameterType.Name);
                }
                catch (Exception ex)
                {
                    Logger?.LogWarning(ex, "创建默认参数失败");
                }
            }

            LoadParameterToForm();
        }

        /// <summary>
        /// 验证输入
        /// 子类可以重写此方法，实现自定义验证逻辑
        /// </summary>
        protected virtual bool ValidateInput()
        {
            return true;
        }

        #endregion

        #region 按钮事件处理

        /// <summary>
        /// 确定按钮点击事件 - 保存参数并关闭窗体
        /// 子类可以在按钮事件中调用 SaveParameters()
        /// </summary>
        protected virtual void OnOkButtonClick(object sender, EventArgs e)
        {
            SaveParameters();
        }

        /// <summary>
        /// 取消按钮点击事件 - 直接关闭窗体不保存
        /// </summary>
        protected virtual void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 安全获取当前步骤 - 防止索引越界异常
        /// </summary>
        protected ChildModel GetCurrentStepSafely()
        {
            if (_workflowState == null) return null;

            try
            {
                var steps = _workflowState.GetSteps();
                int idx = _workflowState.StepNum;

                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    return steps[idx];
                }

                _logger?.LogWarning("步骤索引超出范围: Index={Index}, Count={Count}", idx, steps?.Count ?? 0);
                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "获取当前步骤失败");
                return null;
            }
        }

        #endregion

        #region 受保护的辅助属性

        /// <summary>
        /// 获取工作流状态服务
        /// </summary>
        protected IWorkflowStateService WorkflowState => _workflowState;

        /// <summary>
        /// 获取 PLC 管理器
        /// </summary>
        protected IPLCManager PLCManager => _plcManager;

        /// <summary>
        /// 获取全局变量管理器
        /// </summary>
        protected GlobalVariableManager GlobalVariable => _globalVariable;

        /// <summary>
        /// 获取日志服务
        /// </summary>
        protected Microsoft.Extensions.Logging.ILogger Logger => _logger;

        /// <summary>
        /// 检查服务是否可用
        /// </summary>
        protected bool IsServiceAvailable => _workflowState != null && _logger != null;

        #endregion
    }
}
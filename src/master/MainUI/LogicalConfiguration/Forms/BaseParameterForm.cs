using AntdUI;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 参数表单基类 - 非泛型，供设计器使用
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
                Debug.WriteLine($"BaseParameterForm构造函数警告: {ex.Message}");
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
        /// 窗体加载事件
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;

            // 运行时检查服务是否可用
            if (_workflowState == null || _logger == null)
            {
                MessageBox.Show("窗体初始化失败：缺少必需的服务。请检查依赖注入配置。",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _isLoading = true;
                _logger.LogDebug("开始加载参数: {FormType}", GetType().Name);

                LoadParametersFromWorkflow();

                _isLoading = false;
                _logger.LogDebug("参数加载完成: {FormType}", GetType().Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载参数失败: {FormType}", GetType().Name);
                MessageHelper.MessageOK($"加载参数失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 虚方法 - 供子类重写

        /// <summary>
        /// 从工作流加载参数
        /// </summary>
        protected virtual void LoadParametersFromWorkflow()
        {
            // 泛型子类重写
        }

        /// <summary>
        /// 保存参数到工作流
        /// </summary>
        protected virtual void SaveParameters()
        {
            // 泛型子类重写
        }

        #endregion

        #region 通用方法

        /// <summary>
        /// 安全获取当前步骤
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

    /// <summary>
    /// 参数表单泛型基类
    /// 提供统一的参数管理、加载、保存逻辑
    /// 子类只需重写业务逻辑方法
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    public class BaseParameterForm<T> : BaseParameterForm where T : class, new()
    {
        #region 私有字段

        private T _parameter;

        #endregion

        #region 核心属性

        /// <summary>
        /// 参数对象 - FormService 通过反射访问
        /// </summary>
        public virtual T Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new T();
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
        public BaseParameterForm() : base()
        {
        }

        /// <summary>
        /// 依赖注入构造函数（推荐）
        /// </summary>
        protected BaseParameterForm(IWorkflowStateService workflowState, Microsoft.Extensions.Logging.ILogger logger)
            : base(workflowState, logger)
        {
        }

        #endregion

        #region 重写基类方法

        /// <summary>
        /// 从工作流加载参数 - 统一逻辑
        /// </summary>
        protected override void LoadParametersFromWorkflow()
        {
            if (DesignMode || WorkflowState == null) return;

            var currentStep = GetCurrentStepSafely();
            if (currentStep?.StepParameter != null)
            {
                try
                {
                    Parameter = ConvertParameter(currentStep.StepParameter);
                    Logger?.LogInformation("成功加载参数: {ParameterType}", typeof(T).Name);
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
        /// 保存参数 - 统一的保存逻辑
        /// </summary>
        protected override void SaveParameters()
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

                // 调用子类方法将界面数据保存到 Parameter
                SaveFormToParameter();

                // 更新到工作流
                WorkflowState.UpdateStepParameter(WorkflowState.StepNum, _parameter);

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

        #region 参数转换

        /// <summary>
        /// 统一的参数转换逻辑 - 支持直接转换和JSON反序列化
        /// </summary>
        protected virtual T ConvertParameter(object stepParameter)
        {
            // 1. 尝试直接类型转换
            if (stepParameter is T typed)
            {
                return typed;
            }

            // 2. 尝试 JSON 反序列化
            if (stepParameter is string json && !string.IsNullOrEmpty(json))
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(json) ?? new T();
                }
                catch (JsonException ex)
                {
                    Logger?.LogWarning(ex, "JSON反序列化失败，使用默认参数");
                }
            }

            // 3. 尝试序列化再反序列化（处理匿名对象）
            if (stepParameter != null)
            {
                try
                {
                    string jsonString = JsonConvert.SerializeObject(stepParameter);
                    return JsonConvert.DeserializeObject<T>(jsonString) ?? new T();
                }
                catch (JsonException ex)
                {
                    Logger?.LogWarning(ex, "对象转换失败，使用默认参数");
                }
            }

            return new T();
        }

        #endregion

        #region 虚方法 - 子类按需重写

        /// <summary>
        /// 加载参数到界面控件
        /// 子类必须重写此方法，将 Parameter 的值填充到界面控件
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
            _parameter = new T();
            Logger?.LogDebug("使用默认参数: {ParameterType}", typeof(T).Name);
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
    }
}
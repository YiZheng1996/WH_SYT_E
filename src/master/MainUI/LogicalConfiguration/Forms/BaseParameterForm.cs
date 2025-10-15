using AntdUI;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 参数表单基类
    /// </summary>
    public class BaseParameterForm : UIForm
    {
        private bool _isLoading = true;

        // 依赖注入的服务
        protected readonly IPLCManager _plcManager;
        protected readonly IWorkflowStateService _workflowState;
        protected readonly GlobalVariableManager _globalVariable;
        protected readonly Microsoft.Extensions.Logging.ILogger _logger;

        #region 构造函数和生命周期

        // 保留无参构造函数供设计器使用
        public BaseParameterForm()
        {
            // 设计时模式检查
            if (DesignMode)
            {
                // 设计时不需要实际的服务
                return;
            }

            // 运行时如果没有通过DI构造函数创建，尝试从全局服务提供者获取
            try
            {
                _plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
                _workflowState = Program.ServiceProvider?.GetService<IWorkflowStateService>();
                _logger = Program.ServiceProvider?.GetService<ILogger<BaseParameterForm>>();
                _globalVariable = Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (_workflowState == null || _logger == null)
                {
                    throw new InvalidOperationException(
                        "无法获取必需的服务。请使用依赖注入构造函数创建此窗体，或确保已正确配置服务提供者。");
                }
            }
            catch (Exception ex)
            {
                // 如果在构造函数中无法获取服务，记录错误但不抛出异常
                // 这样至少可以让设计器工作
                Debug.WriteLine($"BaseParameterForm构造函数警告: {ex.Message}");
            }
        }

        // 依赖注入构造函数（推荐在运行时使用）
        protected BaseParameterForm(IWorkflowStateService workflowState,
            Microsoft.Extensions.Logging.ILogger logger,
            IPLCManager plcManager = null)
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _plcManager = plcManager ?? throw new ArgumentNullException(nameof(plcManager));
            _globalVariable = Program.ServiceProvider?.GetService<GlobalVariableManager>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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

                LoadParameters();
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

        #region 虚方法 - 子类重写实现

        /// <summary>
        /// 加载参数 - 统一的加载逻辑
        /// </summary>
        protected virtual void LoadParameters()
        {
            if (DesignMode || _workflowState == null) return;

            var currentStep = GetCurrentStepSafely();
            if (currentStep?.StepParameter != null)
            {
                try
                {
                    LoadParameterFromStep(currentStep.StepParameter);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "参数转换失败: {FormType}", GetType().Name);
                    SetDefaultValues();
                }
            }
            else
            {
                SetDefaultValues();
            }
        }

        /// <summary>
        /// 从步骤参数加载 - 子类重写
        /// </summary>
        protected virtual void LoadParameterFromStep(object stepParameter) { }

        /// <summary>
        /// 设置默认值 - 子类重写
        /// </summary>
        protected virtual void SetDefaultValues() { }

        /// <summary>
        /// 验证参数 - 基类方法
        /// </summary>
        protected virtual bool ValidateParameters()
        {
            return true;
        }

        /// <summary>
        /// 收集参数 - 子类重写
        /// </summary>
        protected virtual object CollectParameters()
        {
            return null;
        }

        #endregion

        #region 统一的通用逻辑

        /// <summary>
        /// 保存参数 - 统一的保存逻辑
        /// </summary>
        protected virtual void SaveParameters()
        {
            if (DesignMode || _workflowState == null) return;

            try
            {
                var currentStep = GetCurrentStepSafely();
                if (currentStep == null)
                {
                    _logger?.LogWarning("步骤索引无效，无法保存参数: StepNum={StepNum}", _workflowState.StepNum);
                    MessageHelper.MessageOK("步骤索引无效，无法保存参数。", TType.Error);
                    return;
                }

                if (!ValidateParameters())
                {
                    _logger?.LogWarning("参数验证失败: {FormType}", GetType().Name);
                    return;
                }

                var parameter = CollectParameters();
                if (parameter != null)
                {
                    _workflowState.UpdateStepParameter(_workflowState.StepNum, parameter);

                    _logger?.LogInformation("参数保存成功: {FormType}, StepNum={StepNum}",
                        GetType().Name, _workflowState.StepNum);

                    MessageHelper.MessageOK("参数已暂存，主界面点击保存后才会写入文件。", TType.Info);
                    Close();
                }
                else
                {
                    _logger?.LogError("收集参数失败: {FormType}", GetType().Name);
                    MessageHelper.MessageOK("收集参数失败。", TType.Error);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存参数失败: {FormType}", GetType().Name);
                MessageHelper.MessageOK($"保存参数失败：{ex.Message}", TType.Error);
            }
        }

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

        #region 受保护的辅助方法

        /// <summary>
        /// 检查是否正在加载中
        /// </summary>
        protected bool IsLoading => _isLoading;

        /// <summary>
        /// 获取工作流状态服务（供子类使用）
        /// </summary>
        protected IWorkflowStateService WorkflowState => _workflowState;

        /// <summary>
        /// 获取工作流PLC服务（供子类使用）
        /// </summary>
        protected IPLCManager PLCManager => _plcManager;

        /// <summary>
        /// 获取工作流PLC服务（供子类使用）
        /// </summary>
        protected GlobalVariableManager GlobalVariable => _globalVariable;

        /// <summary>
        /// 获取日志服务（供子类使用）
        /// </summary>
        protected Microsoft.Extensions.Logging.ILogger Logger => _logger;

        /// <summary>
        /// 检查服务是否可用
        /// </summary>
        protected bool IsServiceAvailable => _workflowState != null && _logger != null;

        #endregion
    }
}
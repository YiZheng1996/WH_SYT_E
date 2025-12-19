using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Services
{
    /// <summary>
    /// 窗体服务实现，提供更清晰的窗体管理机制
    /// </summary>
    public class FormService : IFormService
    {
        private readonly ILogger<FormService> _logger;
        private readonly IServiceProvider _serviceProvider;
        // 这些服务在构造函数中一次性获取，后续直接使用
        private readonly IPLCManager _plcManager;
        private readonly IWorkflowStateService _workflowState;
        private readonly GlobalVariableManager _variableManager;
        private readonly IPLCConfigurationService _plcConfigService;
        private readonly IFormService _selfReference; // 用于避免循环引用

        public FormService(IServiceProvider serviceProvider, ILogger<FormService> logger)
        {
            // 在构造函数中预加载常用服务
            // 避免每个方法都重复获取相同的服务实例
            // 提高性能，减少代码重复，降低出错概率
            try
            {
                _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _plcManager = _serviceProvider.GetRequiredService<IPLCManager>();
                _workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                _plcConfigService = _serviceProvider.GetRequiredService<IPLCConfigurationService>();
                _variableManager = _serviceProvider.GetRequiredService<GlobalVariableManager>();
                _selfReference = this; // 自引用，避免循环依赖
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FormService 预加载服务失败");
                throw; // 构造函数失败时必须抛出异常
            }
        }


        /// <summary>
        /// 根据名称打开窗体
        /// </summary>
        public void OpenFormByName(Form parentform, string formName, Form parent = null)
        {
            try
            {
                _logger.LogDebug("打开窗体: {FormName}", formName);

                Form form = null;

                // 根据窗体名称创建对应的窗体
                switch (formName.ToUpperInvariant())
                {
                    case "变量定义":
                        form = CreateForm<Form_DefineVar>();
                        break;
                    case "变量赋值":
                        form = CreateForm<Form_VariableAssignment>();
                        break;
                    case "读取PLC":
                        form = CreateForm<Form_ReadPLC>();
                        break;
                    case "写入PLC":
                        form = CreateForm<Form_WritePLC>();
                        break;
                    case "延时等待":
                        form = CreateForm<Form_DelayTime>();
                        break;
                    case "读取单元格":
                        form = CreateForm<Form_ReadCells>();
                        break;
                    case "写入单元格":
                        form = CreateForm<Form_WriteCells>();
                        break;
                    case "保存报表":
                        form = CreateForm<Form_SaveReport>();
                        break;
                    case "消息通知":
                        form = CreateForm<Form_SystemPrompt>();
                        break;
                    case "条件判断":
                        form = CreateForm<Form_Detection>();
                        break;
                    case "变量监控":
                        form = CreateForm<Form_VariableMonitor>();
                        break;
                    case "点位定义":
                        form = CreateForm<Form_DefinePoint>();
                        break;
                    case "等待稳定":
                        form = CreateForm<Form_WaitForStable>();
                        break;
                    case "实时监控":
                        form = CreateForm<Form_RealtimeMonitorPromptConfig>();
                        break;
                    case "检测工具":
                        form = CreateForm<Form_Condition>();
                        break;
                    case "循环工具":
                        form = CreateForm<Form_Loop>();
                        break;
                    default:
                        _logger.LogWarning("未知的窗体类型: {FormName}", formName);
                        MessageBox.Show($"未知的窗体类型: {formName}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                if (form != null)
                {
                    // 设置父窗体关系
                    if (parent != null && !parent.IsDisposed)
                    {
                        form.Owner = parent;
                        form.StartPosition = FormStartPosition.CenterParent;
                    }

                    // 显示窗体
                    VarHelper.ShowDialogWithOverlay(parentform, form);
                    _logger.LogInformation("窗体打开成功: {FormName}", formName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "打开窗体时发生错误: {FormName}", formName);
                MessageBox.Show($"打开窗体时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 创建指定类型的窗体
        /// </summary>
        public T CreateForm<T>() where T : Form
        {
            try
            {
                _logger.LogDebug("创建窗体: {FormType}", typeof(T).Name);

                // 避免每个窗体类型都重复获取服务，减少代码重复
                var form = CreateFormByType<T>();

                _logger.LogInformation("窗体创建成功: {FormType}", typeof(T).Name);
                return form;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建窗体时发生错误: {FormType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// 根据类型创建窗体的内部方法
        /// 集中管理所有窗体的创建逻辑，避免服务获取重复
        /// </summary>
        private T CreateFormByType<T>() where T : Form
        {
            // 所有窗体创建都使用预加载的服务，避免重复获取
            return typeof(T).Name switch
            {
                // 变量定义窗体
                nameof(Form_DefineVar) => (T)(object)new Form_DefineVar(
                    _variableManager),

                // 变量赋值窗体
                nameof(Form_VariableAssignment) => (T)(object)new Form_VariableAssignment(
                    _workflowState,
                    GetSpecificLogger<Form_VariableAssignment>()
                    ),

                // PLC读取窗体
                nameof(Form_ReadPLC) => (T)(object)new Form_ReadPLC(
                    GetSpecificLogger<Form_ReadPLC>()),

                // PLC写入窗体
                nameof(Form_WritePLC) => (T)(object)new Form_WritePLC(
                    _workflowState,
                    GetSpecificLogger<Form_WritePLC>()),

                // 延时配置窗体
                nameof(Form_DelayTime) => (T)(object)new Form_DelayTime(
                    _workflowState,
                    GetSpecificLogger<Form_DelayTime>()),

                // 读取单元格 配置窗体
                nameof(Form_ReadCells) => (T)(object)new Form_ReadCells(),

                // 写入单元格 配置窗体
                nameof(Form_WriteCells) => (T)(object)new Form_WriteCells(),

                // 保存报表 配置窗体
                nameof(Form_SaveReport) => (T)(object)new Form_SaveReport(_workflowState,
                    GetSpecificLogger<Form_SaveReport>()),

                // 系统提示窗体
                nameof(Form_SystemPrompt) => (T)(object)new Form_SystemPrompt(
                    _workflowState,  // 预加载的服务
                    GetSpecificLogger<Form_SystemPrompt>()),

                // 检测工具窗体
                nameof(Form_Detection) => (T)(object)new Form_Detection(),

                // 变量监控
                nameof(Form_VariableMonitor) => (T)(object)new Form_VariableMonitor(
                    _workflowState,
                    _variableManager,
                    GetSpecificLogger<Form_VariableMonitor>()),

                // 点位定义
                nameof(Form_DefinePoint) => (T)(object)new Form_DefinePoint(
                    _plcConfigService,
                    GetSpecificLogger<Form_DefinePoint>()),

                // 等待稳定
                nameof(Form_WaitForStable) => (T)(object)new Form_WaitForStable(
                    _workflowState,
                    GetSpecificLogger<Form_WaitForStable>()),

                // 等待稳定
                nameof(Form_RealtimeMonitorPromptConfig) => (T)(object)new Form_RealtimeMonitorPromptConfig(
                    _workflowState,
                    GetSpecificLogger<Form_RealtimeMonitorPromptConfig>()),

                // 等待稳定
                nameof(Form_Condition) => (T)(object)new Form_Condition(
                    _workflowState,
                    GetSpecificLogger<Form_Condition>()),

                // 等待稳定
                nameof(Form_Loop) => (T)(object)new Form_Loop(
                    _workflowState,
                    GetSpecificLogger<Form_Loop>()),

                // 未知窗体类型
                _ => throw new NotSupportedException($"不支持的窗体类型: {typeof(T).Name}")
            };
        }

        /// <summary>
        /// 创建逻辑配置窗体
        /// </summary>
        public FrmLogicalConfiguration CreateLogicalConfigurationForm(
            string path, string modelType, string modelName, string processName)
        {
            try
            {
                _logger.LogDebug("创建逻辑配置窗体: {ModelType}/{ModelName}/{ProcessName}",
                    modelType, modelName, processName);

                // 使用预加载的服务
                var form = new FrmLogicalConfiguration(
                    _workflowState,
                    _variableManager,
                    GetSpecificLogger<FrmLogicalConfiguration>(),
                    _selfReference,
                    path, modelType, modelName, processName);

                _logger.LogInformation("逻辑配置窗体创建成功");
                return form;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建逻辑配置窗体时发生错误");
                throw; // 重新抛出，让调用方处理
            }
        }


        /// <summary>
        /// 获取特定类型的日志器
        /// 集中管理日志器获取，避免在每个方法中重复代码
        /// </summary>
        private ILogger<T> GetSpecificLogger<T>()
        {
            return _serviceProvider.GetRequiredService<ILogger<T>>();
        }

        /// <summary>
        /// 根据名称打开窗体并返回配置结果(用于BaseStepConfigForm内部打开子步骤配置)
        /// </summary>
        public (DialogResult result, object parameter) OpenFormByNameWithResult(
            Form parentForm, string formName, object currentParameter)
        {
            try
            {
                _logger.LogDebug("打开配置窗体: {FormName}", formName);

                Form form = null;

                // 根据窗体名称创建对应的窗体
                switch (formName.ToUpperInvariant())
                {
                    case "变量定义":
                        form = CreateForm<Form_DefineVar>();
                        break;
                    case "变量赋值":
                        form = CreateForm<Form_VariableAssignment>();
                        break;
                    case "读取PLC":
                        form = CreateForm<Form_ReadPLC>();
                        break;
                    case "写入PLC":
                        form = CreateForm<Form_WritePLC>();
                        break;
                    case "延时等待":
                        form = CreateForm<Form_DelayTime>();
                        break;
                    case "读取单元格":
                        form = CreateForm<Form_ReadCells>();
                        break;
                    case "写入单元格":
                        form = CreateForm<Form_WriteCells>();
                        break;
                    case "保存报表":
                        form = CreateForm<Form_SaveReport>();
                        break;
                    case "消息通知":
                        form = CreateForm<Form_SystemPrompt>();
                        break;
                    case "条件判断":
                        form = CreateForm<Form_Detection>();
                        break;
                    case "变量监控":
                        form = CreateForm<Form_VariableMonitor>();
                        break;
                    case "点位定义":
                        form = CreateForm<Form_DefinePoint>();
                        break;
                    case "等待稳定":
                        form = CreateForm<Form_WaitForStable>();
                        break;
                    case "实时监控":
                        form = CreateForm<Form_RealtimeMonitorPromptConfig>();
                        break;
                    case "检测工具":
                        form = CreateForm<Form_Condition>();
                        break;
                    case "循环工具":
                        form = CreateForm<Form_Loop>();
                        break;
                    default:
                        _logger.LogWarning("未知的窗体类型: {FormName}", formName);
                        return (DialogResult.Cancel, null);
                }

                // 订阅 Shown 事件，在窗体显示后再加载参数
                void shownHandler(object s, EventArgs e)
                {
                    form.Shown -= shownHandler;  // 取消订阅
                    if (currentParameter != null)
                    {
                        SetFormParameter(form, currentParameter);
                    }
                }

                form.Shown += shownHandler;

                // 显示窗体
                DialogResult result = form.ShowDialog(parentForm);

                // 获取返回参数
                object returnParameter = null;
                if (result == DialogResult.OK)
                {
                    returnParameter = GetParameterFrom(form);
                }

                form.Dispose();
                return (result, returnParameter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "打开配置窗体失败");
                return (DialogResult.Cancel, null);
            }
        }

        /// <summary>
        /// 设置窗体的 Parameter 属性
        /// </summary>
        private void SetFormParameter(Form form, object parameter)
        {
            try
            {
                var paramProp = form.GetType().GetProperty("Parameter");
                if (paramProp != null && paramProp.CanWrite)
                {
                    // 如果参数是 JSON 字符串，需要反序列化
                    object paramValue = parameter;
                    if (parameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
                    {
                        try
                        {
                            paramValue = JsonConvert.DeserializeObject(jsonStr, paramProp.PropertyType);
                        }
                        catch
                        {
                            _logger?.LogWarning("JSON 反序列化失败，使用原始参数");
                        }
                    }

                    paramProp.SetValue(form, paramValue);
                    _logger?.LogDebug("成功设置窗体参数: {FormType}", form.GetType().Name);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "设置窗体参数失败");
            }
        }

        /// <summary>
        /// 加载参数到配置窗体
        /// </summary>
        private void LoadParameterToForm(Form form, object stepParameter)
        {
            try
            {
                if (stepParameter == null) return;

                var parameterProperty = form.GetType().GetProperty("Parameter");
                if (parameterProperty == null || !parameterProperty.CanWrite)
                {
                    _logger?.LogWarning("窗体没有可写的Parameter属性");
                    return;
                }

                // 如果是JSON字符串,反序列化
                if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
                {
                    var paramType = parameterProperty.PropertyType;
                    var deserializedParam = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonStr, paramType);
                    parameterProperty.SetValue(form, deserializedParam);
                }
                else
                {
                    parameterProperty.SetValue(form, stepParameter);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载参数到窗体失败");
            }
        }

        /// <summary>
        /// 从配置窗体获取参数
        /// </summary>
        private object GetParameterFrom(Form form)
        {
            try
            {
                // 1. 查找Parameter属性
                var paramProp = form.GetType().GetProperty("Parameter");
                if (paramProp != null && paramProp.CanRead)
                {
                    return paramProp.GetValue(form);
                }

                // 2. 查找GetParameter方法
                var getParamMethod = form.GetType().GetMethod("GetParameter");
                if (getParamMethod != null)
                {
                    return getParamMethod.Invoke(form, null);
                }

                _logger?.LogWarning("无法从窗体获取参数: {FormType}", form.GetType().Name);
                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "从窗体获取参数失败");
                return null;
            }
        }
    }
}

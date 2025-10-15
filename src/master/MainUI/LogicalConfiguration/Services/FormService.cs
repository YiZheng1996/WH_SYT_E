using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using MainUI.LogicalConfiguration.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MainUI.Procedure.DSL.LogicalConfiguration.Forms;

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
                switch (formName.ToLowerInvariant())
                {
                    case "变量定义":
                        form = CreateForm<Form_DefineVar>();
                        break;
                    case "变量赋值":
                        form = CreateForm<Form_VariableAssignment>();
                        break;
                    case "plc读取":
                        form = CreateForm<Form_ReadPLC>();
                        break;
                    case "plc写入":
                        form = CreateForm<Form_WritePLC>();
                        break;
                    case "延时工具":
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
                    case "系统提示":
                        form = CreateForm<Form_SystemPrompt>();
                        break;
                    case "检测工具":
                        form = CreateForm<Form_Detection>();
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
                    GetSpecificLogger<Form_VariableAssignment>(),
                    _plcManager
                    ),

                // PLC读取窗体
                nameof(Form_ReadPLC) => (T)(object)new Form_ReadPLC(
                    _workflowState,
                    _variableManager,
                    GetSpecificLogger<Form_ReadPLC>(),
                    _plcManager),

                // PLC写入窗体
                nameof(Form_WritePLC) => (T)(object)new Form_WritePLC(
                    _plcManager),

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
    }
}

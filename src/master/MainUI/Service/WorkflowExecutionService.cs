using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.Service
{
    /// <summary>
    /// 工作流执行服务 - 通用工作流管理类
    /// 负责加载、管理和执行工作流配置
    /// </summary>
    public class WorkflowExecutionService(
        IWorkflowStateService workflowState,
        Func<List<ChildModel>, StepExecutionManager> executionFactory,
        ILogger<WorkflowExecutionService> logger) : IDisposable
    {
        #region 私有字段

        private readonly IWorkflowStateService _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
        private readonly Func<List<ChildModel>, StepExecutionManager> _executionFactory = executionFactory ?? throw new ArgumentNullException(nameof(executionFactory));
        private readonly ILogger<WorkflowExecutionService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // 存储加载的工作流配置：项目名称 -> 步骤列表
        private Dictionary<string, List<ChildModel>> _workflowConfigurations = [];

        // 当前执行的工作流管理器
        private StepExecutionManager _currentExecutionManager;

        private bool _disposed = false;

        #endregion

        #region 事件

        /// <summary>
        /// 配置加载完成事件
        /// 参数：(配置数量, 总步骤数)
        /// </summary>
        public event Action<int, int> ConfigurationLoaded;

        /// <summary>
        /// 工作流开始执行事件
        /// 参数：(项目名称, 步骤数量)
        /// </summary>
        public event Action<string, int> WorkflowStarted;

        /// <summary>
        /// 工作流执行完成事件
        /// 参数：(项目名称, 是否成功)
        /// </summary>
        public event Action<string, bool> WorkflowCompleted;

        /// <summary>
        /// 步骤状态变化事件
        /// 参数：(步骤, 索引)
        /// </summary>
        public event Action<ChildModel, int> StepStatusChanged;

        /// <summary>
        /// 错误发生事件
        /// 参数：(错误消息, 异常对象)
        /// </summary>
        public event Action<string, Exception> ErrorOccurred;

        /// <summary>
        /// 进度消息事件
        /// 参数：(消息内容)
        /// </summary>
        public event Action<string> ProgressMessage;

        #endregion

        #region 公共属性
        /// <summary>
        /// 是否已加载配置
        /// </summary>
        public bool IsConfigurationLoaded => _workflowConfigurations.Count > 0;

        /// <summary>
        /// 已加载的配置数量
        /// </summary>
        public int ConfigurationCount => _workflowConfigurations.Count;

        /// <summary>
        /// 是否正在执行
        /// </summary>
        public bool IsExecuting => _currentExecutionManager?.IsExecuting ?? false;

        #endregion

        #region 配置加载

        /// <summary>
        /// 加载指定产品的所有工作流配置
        /// </summary>
        /// <param name="modelType">产品类型</param>
        /// <param name="modelName">产品型号</param>
        /// <returns>加载的配置数量</returns>
        public async Task<int> LoadConfigurationsAsync(string modelType, string modelName)
        {
            try
            {
                _logger.LogInformation("开始加载工作流配置: {ModelType}/{ModelName}", modelType, modelName);
                ProgressMessage?.Invoke("正在加载工作流配置...");

                if (string.IsNullOrEmpty(modelType) || string.IsNullOrEmpty(modelName))
                {
                    _logger.LogWarning("产品型号信息不完整");
                    return 0;
                }

                // 清空之前的配置
                _workflowConfigurations.Clear();

                // 获取配置文件目录
                string configDir = Path.Combine(
                    Application.StartupPath,
                    "Procedure",
                    modelType,
                    modelName
                );

                // 检查目录是否存在
                if (!Directory.Exists(configDir))
                {
                    _logger.LogInformation("工作流配置目录不存在: {ConfigDir}", configDir);
                    ProgressMessage?.Invoke("未找到工作流配置文件");
                    return 0;
                }

                // 扫描所有 JSON 配置文件
                var jsonFiles = Directory.GetFiles(configDir, "*.json");

                if (jsonFiles.Length == 0)
                {
                    _logger.LogInformation("配置目录中没有JSON文件");
                    ProgressMessage?.Invoke("未找到工作流配置文件");
                    return 0;
                }

                int loadedCount = 0;
                int totalSteps = 0;

                foreach (var jsonPath in jsonFiles)
                {
                    try
                    {
                        // 从文件名获取项目名称
                        string itemName = Path.GetFileNameWithoutExtension(jsonPath);

                        // 加载单个配置
                        var steps = await LoadSingleConfigurationAsync(jsonPath, modelType, modelName, itemName);

                        if (steps != null && steps.Count > 0)
                        {
                            _workflowConfigurations[itemName] = steps;
                            loadedCount++;
                            totalSteps += steps.Count;

                            _logger.LogInformation(
                                "加载工作流配置 [{ItemName}]: {StepCount} 个步骤",
                                itemName, steps.Count);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "加载配置文件失败: {JsonPath}", jsonPath);
                        ErrorOccurred?.Invoke($"加载配置文件失败: {Path.GetFileName(jsonPath)}", ex);
                    }
                }

                if (loadedCount > 0)
                {
                    string message = $"✓ 成功加载 {loadedCount} 个工作流配置，共 {totalSteps} 个步骤";
                    ProgressMessage?.Invoke(message);
                    ConfigurationLoaded?.Invoke(loadedCount, totalSteps);

                    _logger.LogInformation(
                        "工作流配置加载完成: {LoadedCount} 个配置, {TotalSteps} 个步骤",
                        loadedCount, totalSteps);
                }
                else
                {
                    ProgressMessage?.Invoke("未找到有效的工作流配置");
                }

                return loadedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载工作流配置时发生错误");
                ErrorOccurred?.Invoke("加载工作流配置失败", ex);
                return 0;
            }
        }

        /// <summary>
        /// 加载单个配置文件
        /// </summary>
        private async Task<List<ChildModel>> LoadSingleConfigurationAsync(
            string jsonPath,
            string modelType,
            string modelName,
            string itemName)
        {
            try
            {
                // 设置 JsonManager 的文件路径
                JsonManager.FilePath = jsonPath;

                // 加载配置
                var config = await JsonManager.GetOrCreateConfigAsync();

                // 查找匹配的配置
                var parent = config.Form.FirstOrDefault(p =>
                    p.ModelTypeName == modelType &&
                    p.ModelName == modelName &&
                    p.ItemName == itemName);

                if (parent?.ChildSteps != null && parent.ChildSteps.Count > 0)
                {
                    // 创建步骤副本
                    return [.. parent.ChildSteps.Select(s => new ChildModel
                    {
                        StepName = s.StepName,
                        Status = s.Status,
                        StepNum = s.StepNum,
                        StepParameter = s.StepParameter
                    })];
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载配置文件失败: {JsonPath}", jsonPath);
                throw;
            }
        }

        #endregion

        #region 配置查询

        /// <summary>
        /// 获取指定项目的工作流配置
        /// </summary>
        /// <param name="itemName">项目名称</param>
        /// <returns>步骤列表，如果不存在返回 null</returns>
        public List<ChildModel> GetConfiguration(string itemName)
        {
            if (_workflowConfigurations.TryGetValue(itemName, out var steps))
            {
                return steps;
            }
            return null;
        }

        /// <summary>
        /// 检查指定项目是否有工作流配置
        /// </summary>
        /// <param name="itemName">项目名称</param>
        public bool HasConfiguration(string itemName)
        {
            return _workflowConfigurations.ContainsKey(itemName);
        }

        /// <summary>
        /// 获取所有已加载的项目名称
        /// </summary>
        public List<string> GetAllItemNames()
        {
            return _workflowConfigurations.Keys.ToList();
        }

        /// <summary>
        /// 获取指定项目的步骤数量
        /// </summary>
        public int GetStepCount(string itemName)
        {
            return GetConfiguration(itemName)?.Count ?? 0;
        }

        #endregion

        #region 工作流执行

        /// <summary>
        /// 执行指定项目的工作流
        /// </summary>
        /// <param name="itemName">项目名称</param>
        /// <param name="modelType">产品类型</param>
        /// <param name="modelName">产品型号</param>
        /// <returns>是否执行成功</returns>
        public async Task<bool> ExecuteWorkflowAsync(string itemName, string modelType, string modelName)
        {
            try
            {
                // 检查配置是否存在
                var steps = GetConfiguration(itemName);
                if (steps == null || steps.Count == 0)
                {
                    _logger.LogWarning("测试项 {ItemName} 没有工作流配置", itemName);
                    ProgressMessage?.Invoke($"⚠ 测试项 {itemName} 没有工作流配置");
                    return false;
                }

                _logger.LogInformation("开始执行工作流: {ItemName}, {StepCount} 个步骤",
                    itemName, steps.Count);

                // 触发开始事件
                WorkflowStarted?.Invoke(itemName, steps.Count);
                ProgressMessage?.Invoke($"开始执行工作流: {itemName} ({steps.Count} 个步骤)");

                // 在工作流执行前调用
                await EnsureVariablesLoadedAsync();

                // 更新工作流状态配置
                _workflowState.UpdateConfiguration(modelType, modelName, itemName);

                // 清空现有步骤并加载新步骤
                _workflowState.ClearSteps();
                foreach (var step in steps)
                {
                    _workflowState.AddStep(new ChildModel
                    {
                        StepName = step.StepName,
                        Status = step.Status,
                        StepNum = step.StepNum,
                        StepParameter = step.StepParameter
                    });
                }

                // 创建执行管理器
                _currentExecutionManager = _executionFactory(_workflowState.GetSteps());

                // 订阅步骤状态变化事件
                _currentExecutionManager.StepStatusChanged += OnStepStatusChanged;

                try
                {
                    // 开始执行工作流
                    await _currentExecutionManager.StartExecutionAsync();

                    _logger.LogInformation("工作流执行完成: {ItemName}", itemName);
                    ProgressMessage?.Invoke($"✓ 工作流执行完成: {itemName}");

                    // 触发完成事件
                    WorkflowCompleted?.Invoke(itemName, true);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "工作流执行失败: {ItemName}", itemName);
                    ProgressMessage?.Invoke($"✗ 工作流执行失败: {itemName}");
                    ErrorOccurred?.Invoke($"工作流执行失败: {itemName}", ex);

                    // 触发完成事件（失败）
                    WorkflowCompleted?.Invoke(itemName, false);

                    return false;
                }
                finally
                {
                    // 清理事件订阅
                    if (_currentExecutionManager != null)
                    {
                        _currentExecutionManager.StepStatusChanged -= OnStepStatusChanged;
                        _currentExecutionManager = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "执行工作流时发生错误: {ItemName}", itemName);
                ErrorOccurred?.Invoke($"执行工作流时发生错误: {itemName}", ex);
                WorkflowCompleted?.Invoke(itemName, false);
                return false;
            }
        }

        /// <summary>
        /// 批量执行多个项目的工作流
        /// </summary>
        /// <param name="itemNames">项目名称列表</param>
        /// <param name="modelType">产品类型</param>
        /// <param name="modelName">产品型号</param>
        /// <returns>执行结果字典：项目名称 -> 是否成功</returns>
        public async Task<Dictionary<string, bool>> ExecuteMultipleWorkflowsAsync(
            List<string> itemNames,
            string modelType,
            string modelName)
        {
            var results = new Dictionary<string, bool>();

            foreach (var itemName in itemNames)
            {
                try
                {
                    ProgressMessage?.Invoke($"\n========== 开始测试项: {itemName} ==========");

                    bool success = await ExecuteWorkflowAsync(itemName, modelType, modelName);
                    results[itemName] = success;

                    if (success)
                    {
                        ProgressMessage?.Invoke($"✓ 完成测试项: {itemName}");
                    }
                    else
                    {
                        ProgressMessage?.Invoke($"✗ 测试项执行失败: {itemName}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "执行测试项失败: {ItemName}", itemName);
                    results[itemName] = false;
                    ProgressMessage?.Invoke($"✗ 测试项 {itemName} 执行异常");
                }
            }

            return results;
        }

        /// <summary>
        /// 停止当前正在执行的工作流
        /// </summary>
        public void StopExecution()
        {
            try
            {
                if (_currentExecutionManager != null && _currentExecutionManager.IsExecuting)
                {
                    _logger.LogInformation("停止工作流执行");
                    _currentExecutionManager.Stop();
                    ProgressMessage?.Invoke("⏹ 工作流执行已停止");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "停止工作流执行时发生错误");
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 步骤状态变化处理
        /// </summary>
        private void OnStepStatusChanged(ChildModel step, int stepIndex)
        {
            try
            {
                // 转发事件
                StepStatusChanged?.Invoke(step, stepIndex);

                // 生成进度消息
                string statusText = step.Status switch
                {
                    1 => "▶ 执行中",
                    2 => "✓ 完成",
                    3 => "✗ 失败",
                    _ => "⏳ 待执行"
                };

                int totalSteps = _workflowState.GetStepCount();
                string message = $"  [{stepIndex + 1}/{totalSteps}] {step.StepName} - {statusText}";
                ProgressMessage?.Invoke(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理步骤状态变化时发生错误");
            }
        }

        /// <summary>
        /// 在工作流执行前调用,否则无法获取变量集合
        /// </summary>
        /// <returns></returns>
        public async Task EnsureVariablesLoadedAsync()
        {
            try
            {
                var workflowState = Program.ServiceProvider.GetRequiredService<IWorkflowStateService>();
                var config = await JsonManager.GetOrCreateConfigAsync();

                // 清空并重新加载
                workflowState.ClearVariables();
                foreach (var varItem in config.Variable)
                {
                    var enhancedVar = new VarItem_Enhanced
                    {
                        VarName = varItem.VarName,
                        VarType = varItem.VarType,
                        VarValue = varItem.VarValue,
                        VarText = varItem.VarText,
                        LastUpdated = DateTime.Now,
                        IsAssignedByStep = false,
                        AssignmentType = VariableAssignmentType.None
                    };

                    workflowState.AddVariable(enhancedVar);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载变量失败", ex);
                throw;
            }
        }

        #endregion

        #region 资源释放

        /// <summary>
        /// 清空所有配置
        /// </summary>
        public void ClearConfigurations()
        {
            _workflowConfigurations.Clear();
            _logger.LogInformation("已清空所有工作流配置");
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                StopExecution();
                ClearConfigurations();
                _disposed = true;
            }
        }

        #endregion
    }
}
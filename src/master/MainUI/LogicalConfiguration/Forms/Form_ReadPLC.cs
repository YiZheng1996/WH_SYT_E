using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static MainUI.LogicalConfiguration.LogicalManager.GlobalVariableManager;

namespace MainUI.LogicalConfiguration.Forms
{
    public partial class Form_ReadPLC : UIForm
    {
        #region 私有字段

        // 通过依赖注入获取的服务
        private readonly IWorkflowStateService _workflowState;
        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger<Form_ReadPLC> _logger;
        private readonly IPLCManager _pLCManager;
        private bool _isLoading = false;

        // 窗体私有字段
        private Parameter_ReadPLC _currentParameter;
        private int _currentStepIndex = -1;

        #endregion

        #region 构造函数

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        /// <param name="workflowState">工作流状态服务</param>
        /// <param name="variableManager">变量管理器</param>
        /// <param name="logger">日志服务</param>
        public Form_ReadPLC(
            IWorkflowStateService workflowState,
            GlobalVariableManager variableManager,
            ILogger<Form_ReadPLC> logger,
            IPLCManager pLCManager)
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pLCManager = pLCManager ?? throw new ArgumentNullException(nameof(pLCManager));
            InitializeComponent();
            InitializeForm();
            _logger.LogDebug("Form_ReadPLC 初始化完成");
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitializeForm()
        {
            try
            {
                _isLoading = true;

                // 获取当前步骤信息
                _currentStepIndex = _workflowState.StepNum;

                // 加载当前步骤的参数
                LoadCurrentStepParameter();

                // 加载可用变量到下拉框
                LoadAvailableVariables();

                // 加载写入PLC参数
                LoadWritePLCParameters();

                // 加载PLC点位
                InitializePointLocationPLC();

                _logger.LogDebug("窗体初始化完成，当前步骤: {StepIndex}", _currentStepIndex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化窗体时发生错误");
                MessageHelper.MessageOK($"初始化失败: {ex.Message}", TType.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        /// <summary>
        /// 加载当前步骤的参数
        /// </summary>
        private void LoadCurrentStepParameter()
        {
            try
            {
                var currentStep = _workflowState.GetStep(_currentStepIndex);
                if (currentStep?.StepParameter != null)
                {
                    // 尝试解析参数
                    if (currentStep.StepParameter is Parameter_ReadPLC param)
                    {
                        _currentParameter = param;
                    }
                    else
                    {
                        // 如果是JSON字符串，尝试反序列化
                        var jsonString = currentStep.StepParameter.ToString();
                        _currentParameter = JsonConvert.DeserializeObject<Parameter_ReadPLC>(jsonString)
                            ?? new Parameter_ReadPLC();
                    }
                }
                else
                {
                    _currentParameter = new Parameter_ReadPLC();
                }

                _logger.LogDebug("加载步骤参数完成，包含 {ItemCount} 个PLC项", _currentParameter.Items?.Count ?? 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载步骤参数时发生错误");
                _currentParameter = new Parameter_ReadPLC();
            }
        }

        /// <summary>
        /// 加载可用变量到下拉框
        /// </summary>
        private void LoadAvailableVariables()
        {
            try
            {
                // 获取所有可用变量
                var allVariables = _variableManager.GetAllVariables();
                var variableNames = allVariables.Select(v => v.VarName).ToArray();

                // 设置目标变量下拉框
                if (DataGridViewPLCList.Columns["TargetVarName"] is DataGridViewComboBoxColumn varColumn)
                {
                    varColumn.Items.Clear();
                    varColumn.Items.AddRange(variableNames);
                }

                _logger.LogDebug("加载了 {Count} 个可用变量", variableNames.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载可用变量时发生错误");
            }
        }
        #endregion

        #region 数据操作方法

        ///// <summary>
        ///// 添加PLC项目到表格
        ///// </summary>
        ///// <param name="item">PLC读取项</param>
        //private void AddPLCItemToGrid(PLCReadItem item)
        //{
        //    try
        //    {
        //        var rowIndex = DataGridViewPLCList.Rows.Add(
        //            item.PLCAddress,
        //            item.DataType,
        //            item.TargetVarName,
        //            item.Description
        //        );

        //        // 检查新添加项的变量冲突
        //        if (!string.IsNullOrEmpty(item.TargetVarName))
        //        {
        //            CheckSingleVariableConflict(item.TargetVarName, rowIndex);
        //        }

        //        _logger.LogDebug("添加PLC项到表格: {Address} -> {VarName}", item.PLCAddress, item.TargetVarName);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "添加PLC项到表格时发生错误");
        //    }
        //}

        /// <summary>
        /// 验证所有数据
        /// </summary>
        /// <returns>验证是否通过</returns>
        private bool ValidateAllData()
        {
            try
            {
                var errors = new List<string>();

                // 验证PLC项目
                var hasValidItems = false;
                foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var plcAddr = row.Cells["PLCAddress"]?.Value?.ToString()?.Trim();
                        var varName = row.Cells["TargetVarName"]?.Value?.ToString()?.Trim();

                        if (!string.IsNullOrEmpty(plcAddr) && !string.IsNullOrEmpty(varName))
                        {
                            hasValidItems = true;

                            // 检查变量是否存在
                            var variable = _variableManager.TryFindVariableByName(varName);
                            if (variable == null)
                            {
                                errors.Add($"变量 '{varName}' 不存在");
                            }
                        }
                    }
                }

                if (!hasValidItems)
                {
                    errors.Add("至少需要配置一个有效的PLC读取项");
                }

                // 显示验证错误
                if (errors.Count != 0)
                {
                    var errorMessage = string.Join("\n", errors);
                    MessageHelper.MessageOK($"数据验证失败:\n{errorMessage}", TType.Error);
                    return false;
                }

                _logger.LogDebug("数据验证通过");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "验证数据时发生错误");
                MessageHelper.MessageOK($"验证失败: {ex.Message}", TType.Error);
                return false;
            }
        }

        /// <summary>
        /// 更新步骤参数
        /// </summary>
        private async Task UpdateStepParameter()
        {
            try
            {
                var currentStep = _workflowState.GetStep(_currentStepIndex);
                if (currentStep != null)
                {
                    currentStep.StepParameter = _currentParameter;
                    _logger.LogDebug("更新步骤参数完成");
                }
                else
                {
                    throw new InvalidOperationException($"无法找到步骤索引 {_currentStepIndex}");
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新步骤参数时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 更新变量赋值状态
        /// </summary>
        private void UpdateVariableAssignmentStatus()
        {
            try
            {
                foreach (var item in _currentParameter.Items)
                {
                    var variable = _variableManager.TryFindVariableByName(item.TargetVarName);
                    if (variable != null)
                    {
                        // 标记变量为已被当前步骤赋值
                        variable.IsAssignedByStep = true;
                        variable.AssignedByStepIndex = _currentStepIndex;
                        variable.AssignedByStepInfo = $"PLC读取:{_workflowState.StepName}";
                        variable.AssignmentType = VariableAssignmentType.PLCRead;
                        variable.LastUpdated = DateTime.Now;

                        _logger.LogDebug("更新变量赋值状态: {VarName} -> 步骤{StepIndex}",
                            item.TargetVarName, _currentStepIndex);
                    }
                }

                _logger.LogDebug("变量赋值状态更新完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新变量赋值状态时发生错误");
            }
        }

        #endregion

        #region 变量冲突检查方法

        /// <summary>
        /// 检查所有变量冲突
        /// </summary>
        private void CheckAllVariableConflicts()
        {
            try
            {
                var variableNames = GetAllSelectedVariableNames();
                if (variableNames.Count == 0)
                {
                    return;
                }

                //// 使用变量管理器检查冲突
                //var conflicts = _variableManager.CheckVariableConflicts(
                //    variableNames,
                //    _currentStepIndex,
                //    VariableAssignmentType.PLCRead);

                //HandleConflictResults(conflicts);

                _logger.LogDebug("变量冲突检查完成，检查了 {Count} 个变量", variableNames.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "检查变量冲突时发生错误");
                MessageHelper.MessageOK($"检查变量冲突失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 获取所有选中的变量名
        /// </summary>
        /// <returns>变量名列表</returns>
        private List<string> GetAllSelectedVariableNames()
        {
            var variableNames = new List<string>();

            try
            {
                foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var varName = row.Cells["TargetVarName"]?.Value?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(varName))
                        {
                            variableNames.Add(varName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取变量名列表时发生错误");
            }

            return variableNames;
        }

        #endregion

        /// <summary>
        /// 加载写入PLC参数
        /// </summary>
        private void LoadWritePLCParameters()
        {
            try
            {
                var steps = _workflowState.GetSteps();
                int idx = _workflowState.StepNum;
                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    var paramObj = steps[idx].StepParameter;
                    Parameter_ReadPLC param = null;

                    if (paramObj is Parameter_ReadPLC directParam)
                    {
                        param = directParam;
                    }
                    else if (paramObj != null)
                    {
                        try
                        {
                            param = JsonConvert.DeserializeObject<Parameter_ReadPLC>(
                                paramObj is string s ? s : JsonConvert.SerializeObject(paramObj)
                            );
                        }
                        catch (Exception ex)
                        {
                            NlogHelper.Default.Error("参数反序列化失败", ex);
                            param = new Parameter_ReadPLC();
                        }
                    }

                    // 加载所有行
                    if (param != null && param.Items != null)
                    {
                        foreach (var item in param.Items)
                        {
                            if (!string.IsNullOrWhiteSpace(item.PlcKeyName))
                            {
                                DataGridViewPLCList.Rows.Add(item.PlcModuleName, item.PlcKeyName, item.TargetVarName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载PLC参数错误", ex);
                MessageHelper.MessageOK("加载PLC参数错误：" + ex.Message, TType.Error);
            }
        }

        /// <summary>
        /// 初始化变量下拉框
        /// </summary>
        private void InitializeVariableComboBox()
        {
            try
            {
                try
                {
                    var variables = _variableManager.GetAllVariables();

                    // 清空并重新加载
                    ColVariable.Items.Clear();

                    // 添加空选项
                    ColVariable.Items.Add("");

                    foreach (var variable in variables)
                    {
                        ColVariable.Items.Add(variable.VarName);
                    }
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error("初始化变量下拉框失败", ex);
                    MessageHelper.MessageOK("初始化变量下拉框失败：" + ex.Message, TType.Error);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化变量下拉框失败", ex);
            }
        }

        /// <summary>
        /// 加载全部PLC点位
        /// </summary>
        private async void InitializePointLocationPLC()
        {
            try
            {
                TreeViewPLC.Nodes.Clear();
                var configs = await _pLCManager.GetModuleTagsAsync();
                foreach (var kvp in configs)
                {
                    // 创建主节点(Key)
                    TreeNode parentNode = new(kvp.Key);

                    // 添加子节点(Value)
                    foreach (var value in kvp.Value)
                    {
                        parentNode.Nodes.Add(value);
                    }
                    TreeViewPLC.Nodes.Add(parentNode);
                }
                // 默认全部展开
                TreeViewPLC.ExpandAll();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载全部PLC点位错误", ex);
                MessageHelper.MessageOK($"加载全部PLC点位错误：{ex.Message}", TType.Error);
            }
        }

        private void TreeViewPLC_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeViewPLC.DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        // 拖拽添加操作
        private void DataGridViewPLCList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        DataGridViewPLCList.Rows.Add($"{node?.Parent.Text}", $"{node.Text}");
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("拖拽步骤错误", ex);
                MessageHelper.MessageOK($"拖拽步骤错误：{ex.Message}", TType.Error);
            }
        }

        // 拖拽完成
        private void DataGridViewPLCList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(TreeNode)) ?
               DragDropEffects.Copy : DragDropEffects.None;
        }

        // 保存数据到当前步骤
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation("开始保存PLC读取参数");

                // 保存前进行最终验证
                if (!ValidateAllData())
                {
                    return;
                }

                // 检查变量冲突
                CheckAllVariableConflicts();

                //// 如果有冲突，询问用户是否继续
                //var conflicts = _variableManager.CheckVariableConflicts(
                //    GetAllSelectedVariableNames(),
                //    _currentStepIndex,
                //    VariableAssignmentType.PLCRead);

                //if (conflicts.Any(c => c.HasConflict))
                //{
                //    var result = MessageHelper.MessageYes(this, "检测到变量冲突，是否仍要保存？");
                //    if (result != DialogResult.OK)
                //    {
                //        return;
                //    }
                //}

                // 更新步骤参数
                await UpdateStepParameter();

                // 更新变量赋值状态
                UpdateVariableAssignmentStatus();

                MessageHelper.MessageOK("保存成功！", TType.Success);
                _logger.LogInformation("PLC读取参数保存成功");

                Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存PLC读取参数时发生错误");
                MessageHelper.MessageOK($"保存失败: {ex.Message}", TType.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // 获取要删除的行
            int rowIndex = GetSelectedRowIndex();
            if (rowIndex < 0)
            {
                MessageHelper.MessageOK("请选择要删除的PLC行！", TType.Warn);
                return;
            }

            if (MessageHelper.MessageYes(this, "确定要删除选中的PLC行吗？") == DialogResult.OK)
            {
                try
                {
                    var stepInfo = _workflowState.GetStep(_currentStepIndex); ;

                    // 1. 获取要删除的变量名
                    string targetVarName = DataGridViewPLCList.Rows[rowIndex].Cells["ColVariable"].Value?.ToString();

                    //// 2. 清除该变量的赋值状态
                    //if (!string.IsNullOrEmpty(targetVarName))
                    //{
                    //    GlobalVariableManager.ClearSpecificVariableAssignment(targetVarName, stepInfo.StepIndex);
                    //}

                    // TODO:从参数和UI中移除
                    DataGridViewPLCList.Rows.RemoveAt(rowIndex);

                    MessageHelper.MessageOK("删除成功！", TType.Success);
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error("删除PLC行失败", ex);
                    MessageHelper.MessageOK($"删除失败：{ex.Message}", TType.Error);
                }
            }
        }

        // 获取行索引
        private int GetSelectedRowIndex()
        {
            if (DataGridViewPLCList.SelectedRows.Count > 0)
                return DataGridViewPLCList.SelectedRows[0].Index;
            else if (DataGridViewPLCList.CurrentCell != null)
                return DataGridViewPLCList.CurrentCell.RowIndex;
            return -1;
        }

        private void RemoveItemFromParameter(CurrentStepInfo stepInfo, int rowIndex)
        {
            //string plcModuleName = DataGridViewPLCList.Rows[rowIndex].Cells["ColPCLModelName"].Value?.ToString();
            //string plcKeyName = DataGridViewPLCList.Rows[rowIndex].Cells["ColPCLKeyName"].Value?.ToString();

            //if (GlobalVariableManager.TryGetParameter<Parameter_ReadPLC>(stepInfo.Step.StepParameter, out var param))
            //{
            //    var toRemove = param.Items.FirstOrDefault(x =>
            //        x.PlcModuleName == plcModuleName && x.PlcKeyName == plcKeyName);

            //    if (toRemove != null)
            //    {
            //        param.Items.Remove(toRemove);
            //        stepInfo.Step.StepParameter = JsonConvert.SerializeObject(param);
            //    }
            //}
        }

        /// <summary>
        /// 收集变量赋值信息
        /// </summary>
        private List<VariableAssignment> CollectVariableAssignments()
        {
            var assignments = new List<VariableAssignment>();

            foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
            {
                if (row.IsNewRow) continue;

                string plcModuleName = row.Cells["ColPCLModelName"].Value?.ToString();
                string plcKeyName = row.Cells["ColPCLKeyName"].Value?.ToString();
                string varName = row.Cells["ColVariable"].Value?.ToString();

                if (!string.IsNullOrEmpty(varName) && !string.IsNullOrEmpty(plcModuleName) && !string.IsNullOrEmpty(plcKeyName))
                {
                    assignments.Add(new VariableAssignment
                    {
                        VariableName = varName,
                        AssignmentDescription = $"PLC读取({plcModuleName}.{plcKeyName})"
                    });
                }
            }

            return assignments;
        }

        /// <summary>
        /// 收集PLC项
        /// </summary>
        private List<PlcReadItem> CollectPlcItems()
        {
            var plcItems = new List<PlcReadItem>();

            foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
            {
                if (row.IsNewRow) continue;

                string plcModuleName = row.Cells["ColPCLModelName"].Value?.ToString();
                string plcKeyName = row.Cells["ColPCLKeyName"].Value?.ToString();
                string varName = row.Cells["ColVariable"].Value?.ToString();

                if (!string.IsNullOrEmpty(plcModuleName) && !string.IsNullOrEmpty(plcKeyName))
                {
                    plcItems.Add(new PlcReadItem
                    {
                        PlcModuleName = plcModuleName,
                        PlcKeyName = plcKeyName,
                        TargetVarName = varName
                    });
                }
            }

            return plcItems;
        }
    }
}

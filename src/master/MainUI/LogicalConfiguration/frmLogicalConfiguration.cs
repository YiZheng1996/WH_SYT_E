using AntdUI;
using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.Procedure
{
    public partial class FrmLogicalConfiguration : UIForm
    {
        #region 字段和属性

        // 状态颜色定义
        private static readonly Color PrimaryBlue = Color.FromArgb(65, 100, 204);
        private static readonly Color SuccessGreen = Color.FromArgb(40, 167, 69);
        private static readonly Color ErrorRed = Color.FromArgb(220, 53, 69);

        // 通过依赖注入获取的服务
        private readonly IWorkflowStateService _workflowState;
        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger<FrmLogicalConfiguration> _logger;
        private readonly IFormService _formService;

        // UI管理器
        private readonly DataGridViewManager _gridManager;
        private StepExecutionManager _executionManager;
        private bool _isExecuting = false;
        #endregion

        #region 构造函数
        public FrmLogicalConfiguration(
            IWorkflowStateService workflowState,
            GlobalVariableManager variableManager,
            ILogger<FrmLogicalConfiguration> logger,
            IFormService formService,
            string path,
            string modelType,
            string modelName,
            string processName)
        {
            // 依赖验证
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));

            try
            {
                InitializeComponent();

                _logger.LogDebug("开始初始化配置: {ModelType}/{ModelName}/{ProcessName}",
                    modelType, modelName, processName);

                // 设置JSON文件路径
                JsonManager.FilePath = path;

                // 更新窗体标题
                Text = $"产品类型：{modelType}，产品型号：{modelName}，项点名称：{processName}";

                // 创建配置文件
                CreateJsonFileAsync(modelType, modelName, processName).Wait();

                // 更新配置
                _workflowState.UpdateConfiguration(modelType, modelName, processName);

                // 初始化变量
                InitializeVariables();

                // 加载已保存的步骤到DataGridView
                LoadStepsToGrid();

                // 加载工具箱
                LoadTreeViewData();

                // 使用服务创建DataGridView管理器
                _gridManager = new DataGridViewManager(ProcessDataGridView, _workflowState);

                // 设置事件处理程序
                RegisterEventHandlers();

                _logger.LogInformation("工作流配置窗体初始化完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化工作流配置窗体时发生错误");
                MessageHelper.MessageOK($"构造函数加载数据错误：{ex.Message}", TType.Error);
                throw; // 重新抛出异常，让调用方处理
            }
        }

        #endregion

        #region 初始化方法


        #endregion

        #region 初始化方法
        /// <summary>
        /// 注册事件处理程序
        /// </summary>
        private void RegisterEventHandlers()
        {
            try
            {
                // 订阅工作流状态变更事件
                _workflowState.StepNumChanged += OnStepNumChanged;
                _workflowState.VariableAdded += OnVariableAdded;
                _workflowState.VariableRemoved += OnVariableRemoved;
                _workflowState.StepAdded += OnStepAdded;
                _workflowState.StepRemoved += OnStepRemoved;

                // 订阅ToolTreeView的事件
                ToolTreeView.ItemDrag += ToolTreeView_ItemDrag;
                ToolTreeView.AfterSelect += TvTools_AfterSelect;

                // 订阅DataGridView的事件
                ProcessDataGridView.DragDrop += ProcessDataGridView_DragDrop;
                ProcessDataGridView.DragEnter += ProcessDataGridView_DragEnter;
                ProcessDataGridView.CellDoubleClick += ProcessDataGridView_CellDoubleClick;
                ProcessDataGridView.SelectionChanged += DgvProcess_SelectionChanged;

                // 按钮事件
                btnSave.Click += BtnSave_Click;
                btnExecute.Click += BtnExecute_Click;
                btnClose.Click += BtnClose_Click;
                btnVariableDefine.Click += BtnGeneral_Click;
                BtnPointDefine.Click += BtnGeneral_Click;
                BtnVariableMonitor.Click += BtnGeneral_Click;
                BtnSystemParams.Click += BtnGeneral_Click;

                _logger.LogDebug("事件处理程序注册完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "注册事件处理程序时发生错误");
                throw;
            }
        }

        // 加载已存在的Json数据到DataGridView控件中
        private async void LoadStepsToGrid()
        {
            try
            {
                var config = await JsonManager.GetOrCreateConfigAsync();
                // 找到当前项点的 Parent
                var parent = config.Form.FirstOrDefault(p =>
                    p.ModelTypeName == _workflowState.ModelTypeName &&
                    p.ModelName == _workflowState.ModelName &&
                    p.ItemName == _workflowState.ItemName);

                if (parent?.ChildSteps != null)
                {
                    // 清空临时数据和网格
                    _workflowState.ClearSteps();
                    _gridManager.Clears();

                    // 加载数据到临时存储和网格
                    foreach (var step in parent.ChildSteps)
                    {
                        _workflowState.AddStep(new ChildModel
                        {
                            StepName = step.StepName,
                            Status = step.Status,
                            StepNum = step.StepNum,
                            StepParameter = step.StepParameter
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载步骤数据错误", ex);
                MessageHelper.MessageOK($"加载步骤数据错误：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 工具箱初始化

        /// <summary>
        /// 加载示例数据
        /// </summary>
        private void LoadSampleData()
        {
            // 加载TreeView数据
            LoadTreeViewData();

            // 加载DataGridView数据
            LoadDataGridViewData();
        }

        /// <summary>
        /// 加载TreeView数据
        /// </summary>
        private void LoadTreeViewData()
        {
            ToolTreeView.Nodes.Clear();

            // 逻辑控制组
            TreeNode logicNode = new("逻辑控制") { Tag = "LogicControl", ImageKey = "文件夹.png" };
            logicNode.Nodes.Add(new TreeNode("延时等待") { Tag = "DelayWait", ImageKey = "延时等待.png" });
            logicNode.Nodes.Add(new TreeNode("条件判断") { Tag = "ConditionJudge", ImageKey = "条件判断.png" });
            logicNode.Nodes.Add(new TreeNode("循环开始") { Tag = "LoopControlStart", ImageKey = "循环开始.png" });
            logicNode.Nodes.Add(new TreeNode("循环结束") { Tag = "LoopControlStop", ImageKey = "循环结束.png" });
            ToolTreeView.Nodes.Add(logicNode);

            // 数据操作组
            TreeNode dataNode = new("数据操作") { Tag = "DataOperation", ImageKey = "文件夹.png" };
            dataNode.Nodes.Add(new TreeNode("变量赋值") { Tag = "VariableAssign", ImageKey = "变量赋值.png" });
            dataNode.Nodes.Add(new TreeNode("数据读取") { Tag = "DataRead", ImageKey = "数据读取.png" });
            dataNode.Nodes.Add(new TreeNode("数据计算") { Tag = "DataCalculate", ImageKey = "数据计算.png" });
            dataNode.Nodes.Add(new TreeNode("消息通知") { Tag = "", ImageKey = "消息通知.png" });
            ToolTreeView.Nodes.Add(dataNode);

            // PLC通信组
            TreeNode plcNode = new("通信操作") { Tag = "PLCCommunication", ImageKey = "文件夹.png" };
            plcNode.Nodes.Add(new TreeNode("读取PLC") { Tag = "PLCRead", ImageKey = "读取PLC.png" });
            plcNode.Nodes.Add(new TreeNode("写入PLC") { Tag = "PLCWrite", ImageKey = "写入PLC.png" });
            ToolTreeView.Nodes.Add(plcNode);

            // 报表操作组
            TreeNode reportNode = new("报表工具") { Tag = "PLCCommunication", ImageKey = "文件夹.png" };
            reportNode.Nodes.Add(new TreeNode("读取单元格") { Tag = "PLCRead", ImageKey = "报表读取.png" });
            reportNode.Nodes.Add(new TreeNode("写入单元格") { Tag = "PLCWrite", ImageKey = "报表写入.png" });
            ToolTreeView.Nodes.Add(reportNode);

            // 为不同节点设置颜色
            logicNode.ForeColor = Color.FromArgb(52, 58, 64);
            dataNode.ForeColor = Color.FromArgb(40, 167, 69);
            plcNode.ForeColor = Color.FromArgb(13, 110, 253);

            // 展开所有节点
            ToolTreeView.ExpandAll();
        }

        /// <summary>
        /// 加载DataGridView数据
        /// </summary>
        private void LoadDataGridViewData()
        {
            ProcessDataGridView.Rows.Clear();

            // 添加示例数据
            ProcessDataGridView.Rows.Add("1", "初始化变量", "变量赋值", "✓ 完成", "0.05s");
            ProcessDataGridView.Rows.Add("2", "读取传感器数据", "PLC读取", "▶ 执行中", "1.23s");
            ProcessDataGridView.Rows.Add("3", "条件判断", "逻辑判断", "⏳ 待执行", "-");
            ProcessDataGridView.Rows.Add("4", "输出控制信号", "PLC写入", "⏳ 待执行", "-");
            ProcessDataGridView.Rows.Add("5", "记录测试结果", "数据记录", "⏳ 待执行", "-");

            // 设置行颜色
            for (int i = 0; i < ProcessDataGridView.Rows.Count; i++)
            {
                DataGridViewRow row = ProcessDataGridView.Rows[i];
                string status = row.Cells["ColStatus"].Value?.ToString();

                if (status?.Contains("完成") == true)
                {
                    //row.DefaultCellStyle.BackColor = Color.FromArgb(240, 255, 240);
                    row.Cells["ColStatus"].Style.ForeColor = SuccessGreen;
                }
                else if (status?.Contains("执行中") == true)
                {
                    //row.DefaultCellStyle.BackColor = Color.FromArgb(227, 242, 253);
                    row.Cells["ColStatus"].Style.ForeColor = PrimaryBlue;
                }
                else if (status?.Contains("待执行") == true)
                {
                    row.Cells["ColStatus"].Style.ForeColor = Color.FromArgb(108, 117, 125);
                }
            }

            // 默认选中第一行
            if (ProcessDataGridView.Rows.Count > 0)
            {
                ProcessDataGridView.Rows[0].Selected = true;
                UpdateStepDetails(0);
            }
        }

        #endregion

        #region 变量初始化
        /// <summary>
        /// 初始化变量
        /// </summary>
        private async void InitializeVariables()
        {
            try
            {
                // 读取JSON文件中的变量项
                var VarItems = await JsonManager.ReadVarItemsAsync();

                // 将VarItem转换为VarItem_Enhanced
                var enhancedVarItems = VarItems.Select(v => new VarItem_Enhanced
                {
                    VarName = v.VarName,
                    VarType = v.VarType,
                    VarValue = v.VarValue,
                    VarText = v.VarText,
                    LastUpdated = DateTime.Now,
                    IsAssignedByStep = false,
                    AssignmentType = VariableAssignmentType.None
                }).Cast<object>().ToList();

                // 清空现有变量并添加新变量
                _workflowState.ClearVariables();
                foreach (var variable in enhancedVarItems)
                {
                    _workflowState.AddVariable(variable);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化变量失败", ex);
                MessageHelper.MessageOK("初始化变量失败：" + ex.Message, TType.Error);
            }
        }
        #endregion

        #region JSON文件处理
        // 创建JSON文件，如果不存在则创建并写入默认结构
        private static async Task CreateJsonFileAsync(string modelType, string modelName, string processName)
        {
            // 根据产品类型、产品型号中的试验项点生成存放JSON数据的路径
            string modelPath = Path.Combine(Application.StartupPath, "Procedure", modelType, modelName);
            string jsonPath = Path.Combine(modelPath, $"{processName}.json");

            if (!Directory.Exists(modelPath))
                Directory.CreateDirectory(modelPath);

            if (!File.Exists(jsonPath))
            {
                // 如果文件不存在，创建默认配置及格式
                var config = BuildDefaultConfig(modelType, modelName, processName);

                // 保存默认配置到JSON文件
                await JsonManager.UpdateConfigAsync(async c =>
                {
                    c.System = config.System;
                    c.Form = config.Form;
                    c.Variable = config.Variable;
                    await Task.CompletedTask;
                });
            }
        }

        /// <summary>
        /// 生成默认JSON配置结构
        /// </summary>
        /// <param name="modelType">产品类型</param>
        /// <param name="modelName">产品型号</param>
        /// <param name="processName">试验项点</param>
        /// <returns></returns>
        private static JsonManager.JsonConfig BuildDefaultConfig(string modelType, string modelName, string processName)
        {
            return new JsonManager.JsonConfig
            {
                // 初始化系统信息
                System = new JsonManager.JsonConfig.SystemInfo
                {
                    CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ProjectName = "软件通用平台"
                },
                // 初始化默认表单结构
                Form =
                [
                    new()
                     {
                         ModelTypeName = modelType,
                         ModelName = modelName,
                         ItemName = processName,
                         ChildSteps = []
                     }
                ],
                // 初始化默认变量列表（使用VarItem_Enhanced）
                Variable =
                [
                    new VarItem { VarName = "a", VarType = "int", VarText = "变量a" },
                    new VarItem { VarName = "b", VarType = "double", VarText = "变量b" },
                    new VarItem { VarName = "c", VarType = "int", VarText = "变量c" }
                ]
            };
        }

        #endregion

        #region 步骤操作

        /// <summary>
        /// 添加步骤到表单
        /// </summary>
        private void AddStepToForm(int stepNumber, string stepName)
        {
            try
            {
                _logger.LogDebug("添加步骤: {StepName}, 序号: {StepNumber}", stepName, stepNumber);

                var newStep = new ChildModel
                {
                    StepName = stepName,
                    Status = 0,
                    StepNum = stepNumber,
                    StepParameter = 0
                };

                // 添加步骤
                _workflowState.AddStep(newStep);

                _logger.LogInformation("步骤添加成功: {StepName}", stepName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加步骤时发生错误: {StepName}", stepName);
                MessageHelper.MessageOK($"添加步骤错误：{ex.Message}", TType.Error);
            }
        }


        // 删除步骤按钮点击事件处理
        private void toolDeleteStep_Click(object sender, EventArgs e)
        {
            int selectIndex = _gridManager.SelectedRows();
            if (selectIndex >= 0)
            {
                if (_workflowState.RemoveStepAt(selectIndex))
                {
                    // 重新排序工作流状态中的步骤号
                    var steps = _workflowState.GetSteps();
                    for (int i = 0; i < steps.Count; i++)
                    {
                        steps[i].StepNum = i + 1;
                    }
                }
            }
        }

        /// <summary>
        /// 更新步骤状态显示
        /// </summary>
        private void UpdateStepStatus(ChildModel step, int index)
        {
            try
            {
                if (ProcessDataGridView.InvokeRequired)
                {
                    ProcessDataGridView.Invoke(() => UpdateStepStatus(step, index));
                    return;
                }

                if (index >= ProcessDataGridView.Rows.Count) return;

                var row = ProcessDataGridView.Rows[index];

                // 更新状态显示（不同颜色表示不同状态）
                switch (step.Status)
                {
                    case 0: // 未执行
                        row.DefaultCellStyle.BackColor = Color.White;
                        break;
                    case 1: // 执行中
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    case 2: // 成功
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        break;
                    case 3: // 失败
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                        break;
                }

                // 刷新显示
                ProcessDataGridView.Refresh();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("更新步骤状态显示错误", ex);
            }
        }
        #endregion

        #region 事件处理方法

        /// <summary>
        /// 步骤序号变更事件处理
        /// </summary>
        private void OnStepNumChanged(int newStepNum)
        {
            try
            {
                _logger.LogDebug("步骤序号变更为: {StepNum}", newStepNum);

                // 在UI线程上更新界面
                if (InvokeRequired)
                {
                    Invoke(new Action<int>(OnStepNumChanged), newStepNum);
                    return;
                }

                // 更新界面显示
                UpdateStepDisplay(newStepNum);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理步骤序号变更事件时发生错误");
            }
        }

        /// <summary>
        /// 变量添加事件处理
        /// </summary>
        private void OnVariableAdded(object variable)
        {
            try
            {
                if (variable is VarItem_Enhanced varItem)
                {
                    _logger.LogDebug("变量已添加: {VarName}", varItem.VarName);
                    // 可以在这里更新相关UI
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理变量添加事件时发生错误");
            }
        }

        /// <summary>
        /// 变量移除事件处理
        /// </summary>
        private void OnVariableRemoved(object variable)
        {
            try
            {
                if (variable is VarItem_Enhanced varItem)
                {
                    _logger.LogDebug("变量已移除: {VarName}", varItem.VarName);
                    // 可以在这里更新相关UI
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理变量移除事件时发生错误");
            }
        }

        /// <summary>
        /// 步骤添加事件处理
        /// </summary>
        private void OnStepAdded(ChildModel step)
        {
            try
            {
                _logger.LogDebug("步骤已添加: {StepName}", step.StepName);

                // 在UI线程上更新DataGridView
                if (InvokeRequired)
                {
                    Invoke(new Action<ChildModel>(OnStepAdded), step);
                    return;
                }

                _gridManager.AddRow(step.StepName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理步骤添加事件时发生错误");
            }
        }

        /// <summary>
        /// 步骤移除事件处理
        /// </summary>
        private void OnStepRemoved(ChildModel step)
        {
            try
            {
                _logger.LogDebug("步骤已移除: {StepName}", step.StepName);

                // 在UI线程上更新DataGridView
                if (InvokeRequired)
                {
                    Invoke(new Action<ChildModel>(OnStepRemoved), step);
                    return;
                }

                _gridManager.DeleteSelectedRow();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理步骤移除事件时发生错误");
            }
        }

        // 工具箱拖放事件处理
        private void ToolTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode node && node.Parent != null)
            {
                ToolTreeView.DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        // DataGridView拖放事件处理
        private void ProcessDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        AddStepToForm(ProcessDataGridView.Rows.Count + 1, node.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("拖拽步骤错误", ex);
                MessageHelper.MessageOK($"拖拽步骤错误：{ex.Message}", TType.Error);
            }
        }

        // DataGridView拖放进入事件处理
        private void ProcessDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(TreeNode)) ?
                DragDropEffects.Copy : DragDropEffects.None;
        }

        /// <summary>
        /// 双击DataGridView单元格打开步骤配置界面
        /// </summary>
        private void ProcessDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var row = ProcessDataGridView.Rows[e.RowIndex];
                    string stepName = row.Cells[0].Value?.ToString();

                    if (!string.IsNullOrEmpty(stepName))
                    {
                        _logger.LogDebug("打开步骤配置: {StepName}, 行索引: {RowIndex}", stepName, e.RowIndex);

                        // 使用新的线程安全属性设置
                        _workflowState.StepNum = e.RowIndex;
                        _workflowState.StepName = stepName;

                        _formService.OpenFormByName(this, stepName, this);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "打开步骤参数配置界面时发生错误");
                MessageHelper.MessageOK($"打开步骤参数配置界面错误：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 按钮点击事件处理

        /// <summary>
        /// DataGridView选择改变事件
        /// </summary>
        private void DgvProcess_SelectionChanged(object sender, EventArgs e)
        {
            if (ProcessDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = ProcessDataGridView.SelectedRows[0].Index;
                UpdateStepDetails(selectedIndex);
            }
        }

        /// <summary>
        /// TreeView节点选择事件
        /// </summary>
        private void TvTools_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 这里可以添加工具选择后的逻辑
            string selectedTool = e.Node.Text;
            AppendLog($"[{DateTime.Now:HH:mm:ss}] 选择工具: {selectedTool}");
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation("开始保存工作流配置");

                await JsonManager.UpdateConfigAsync(async config =>
                {
                    // 确保配置中有表单
                    if (config.Form.Count == 0)
                    {
                        config.Form.Add(new Parent
                        {
                            ModelTypeName = _workflowState.ModelTypeName,
                            ModelName = _workflowState.ModelName,
                            ItemName = _workflowState.ItemName,
                            ChildSteps = []
                        });
                    }

                    // 使用新的线程安全方法获取变量
                    config.Variable.Clear();
                    var variables = _variableManager.GetAllVariables();
                    config.Variable.AddRange(variables.Cast<VarItem_Enhanced>());

                    // 使用线程安全方法获取步骤
                    config.Form[0].ChildSteps.Clear();
                    var steps = _workflowState.GetSteps();
                    config.Form[0].ChildSteps.AddRange(steps);

                    await Task.CompletedTask;
                });

                _logger.LogInformation("工作流配置保存成功");
                MessageHelper.MessageOK("保存成功！", TType.Success);
                Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存工作流配置时发生错误");
                MessageHelper.MessageOK($"保存错误：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 执行按钮点击事件
        /// </summary>
        private async void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isExecuting)
                {
                    // 停止执行
                    _executionManager?.Stop();
                    return;
                }

                _isExecuting = true;
                btnExecute.Text = "停止";
                btnExecute.Symbol = 61516;

                try
                {
                    // 取消选择
                    ProcessDataGridView.ClearSelection();

                    // 使用新的线程安全方法获取步骤
                    var steps = _workflowState.GetSteps();
                    var stepCount = _workflowState.GetStepCount();

                    _logger.LogInformation("开始执行步骤序列，共 {StepCount} 个步骤", stepCount);

                    var factory = Program.ServiceProvider.GetRequiredService<Func<List<ChildModel>, StepExecutionManager>>();
                    _executionManager = factory(steps);

                    _executionManager.StepStatusChanged += UpdateStepStatus;

                    // 开始执行
                    await _executionManager.StartExecutionAsync();

                    _logger.LogInformation("步骤序列执行完成");
                }
                finally
                {
                    _isExecuting = false;
                    btnExecute.Text = "执行";
                    btnExecute.Symbol = 61515;

                    if (_executionManager != null)
                    {
                        _executionManager.StepStatusChanged -= UpdateStepStatus;
                        _executionManager = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "执行步骤序列时发生错误");
                MessageHelper.MessageOK($"执行错误：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 关闭按钮点击事件
        /// </summary>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (_isExecuting)
            {
                DialogResult result = MessageBox.Show("流程正在执行中，确定要关闭吗？", "确认",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
            }

            this.Close();
        }


        private void BtnGeneral_Click(object sender, EventArgs e)
        {
            // 打开窗体
            var button = sender as UIButton;
            AppendLog($"[{DateTime.Now:HH:mm:ss}] 打开{button.Text}界面");
            _formService.OpenFormByName(this, button.Text, this);
        }


        #endregion

        #region 辅助方法

        /// <summary>
        /// 更新步骤显示
        /// </summary>
        private void UpdateStepDisplay(int stepNum)
        {
            try
            {
                // 更新界面显示当前步骤
                if (stepNum >= 0 && stepNum < ProcessDataGridView.Rows.Count)
                {
                    ProcessDataGridView.ClearSelection();
                    ProcessDataGridView.Rows[stepNum].Selected = true;
                    ProcessDataGridView.CurrentCell = ProcessDataGridView.Rows[stepNum].Cells[0];
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新步骤显示时发生错误");
            }
        }

        /// <summary>
        /// 更新步骤详情
        /// </summary>
        private void UpdateStepDetails(int stepIndex)
        {
            if (stepIndex < 0 || stepIndex >= ProcessDataGridView.Rows.Count)
                return;

            DataGridViewRow row = ProcessDataGridView.Rows[stepIndex];

            lblStepNumber.Text = row.Cells["ColStepNumber"].Value?.ToString() ?? "";
            lblStepName.Text = row.Cells["ColStepName"].Value?.ToString() ?? "";
            lblStepType.Text = row.Cells["ColStepType"].Value?.ToString() ?? "";
            lblStepStatus.Text = row.Cells["ColStatus"].Value?.ToString() ?? "";
            lblExecutionTime.Text = row.Cells["ColExecutionTime"].Value?.ToString() ?? "";

            // 设置状态颜色
            string status = lblStepStatus.Text;
            if (status.Contains("完成"))
                lblStepStatus.ForeColor = SuccessGreen;
            else if (status.Contains("执行中"))
                lblStepStatus.ForeColor = PrimaryBlue;
            else if (status.Contains("待执行"))
                lblStepStatus.ForeColor = Color.FromArgb(108, 117, 125);
            else
                lblStepStatus.ForeColor = ErrorRed;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        private void AppendLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(AppendLog), message);
                return;
            }

            txtLog.AppendText(message + Environment.NewLine);
            txtLog.ScrollToCaret();
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        private void StartExecution()
        {
            _isExecuting = true;
            btnExecute.Text = "停止执行";
            btnExecute.BackColor = ErrorRed;

            AppendLog($"[{DateTime.Now:HH:mm:ss}] 开始执行流程...");

            // 这里添加实际的执行逻辑
        }

        /// <summary>
        /// 停止执行
        /// </summary>
        private void StopExecution()
        {
            _isExecuting = false;
            btnExecute.Text = "执行流程";
            btnExecute.BackColor = PrimaryBlue;

            AppendLog($"[{DateTime.Now:HH:mm:ss}] 流程执行已停止");
        }

        #endregion

        #region 资源释放

        /// <summary>
        /// 窗体关闭时的清理工作
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                // 取消事件订阅
                if (_workflowState != null)
                {
                    _workflowState.StepNumChanged -= OnStepNumChanged;
                    _workflowState.VariableAdded -= OnVariableAdded;
                    _workflowState.VariableRemoved -= OnVariableRemoved;
                    _workflowState.StepAdded -= OnStepAdded;
                    _workflowState.StepRemoved -= OnStepRemoved;
                }

                _logger.LogInformation("工作流配置窗体已关闭");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "关闭窗体时发生错误");
            }
            finally
            {
                base.OnFormClosed(e);
            }
        }

        #endregion
    }
}

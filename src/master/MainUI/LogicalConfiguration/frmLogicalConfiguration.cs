using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;

namespace MainUI.LogicalConfiguration
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
        private StepExecutionManager _executionManager;
        private bool _isExecuting = false;

        // 添加菜单管理器
        private StepContextMenuManager _menuManager;

        // 使用自定义控件
        private ToolTreeViewControl _toolTreeControl;
        private ProcessDataGridViewControl _processGridControl;
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

                // 更新全局变量
                TestInfoVariableHelper.UpdateProductInfo(_variableManager, modelType, modelName);

                // 创建配置文件
                CreateJsonFileAsync(modelType, modelName, processName).Wait();

                // 更新配置
                _workflowState.UpdateConfiguration(modelType, modelName, processName);

                // 初始化变量
                InitializeVariables();

                // 使用自定义控件初始化
                InitializeCustomControls();

                // 设置事件处理程序，先注册事件，再加载数据
                RegisterEventHandlers();

                // 加载已保存的步骤到DataGridView
                LoadStepsToGrid();

                _logger.LogInformation("工作流配置窗体初始化完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化工作流配置窗体时发生错误");
                MessageHelper.MessageOK($"构造函数加载数据错误：{ex.Message}", TType.Error);
                throw; // 重新抛出异常，让调用方处理
            }
        }

        /// <summary>
        /// 初始化自定义控件
        /// </summary>
        private void InitializeCustomControls()
        {
            try
            {
                // 创建工具箱控件
                var toolLogger = Program.ServiceProvider?.GetService<ILogger<ToolTreeViewControl>>();
                _toolTreeControl = new ToolTreeViewControl(toolLogger)
                {
                    Dock = DockStyle.Fill,
                    Title = "工具箱"
                };

                // 订阅工具箱事件
                _toolTreeControl.ToolSelected += OnToolSelected;
                _toolTreeControl.AfterSelect += TvTools_AfterSelect;

                // 添加到容器
                panelToolBox.Controls.Clear();
                panelToolBox.Controls.Add(_toolTreeControl);

                _logger.LogDebug("工具箱控件已初始化");

                // 创建流程表格控件
                var gridLogger = Program.ServiceProvider?.GetService<ILogger<ProcessDataGridViewControl>>();
                _processGridControl = new ProcessDataGridViewControl(_workflowState, gridLogger)
                {
                    Dock = DockStyle.Fill
                };

                // 订阅流程表格事件
                _processGridControl.StepConfigRequested += OnStepConfigRequested;
                _processGridControl.DragDropEvent += OnProcessGridDragDrop;
                _processGridControl.SelectionChangedEvent += OnGridSelectionChanged;
                _processGridControl.CellEndEditEvent += ProcessDataGridView_CellEndEdit;
                _processGridControl.CellBeginEditEvent += ProcessDataGridView_CellBeginEdit;

                // 添加到容器
                panelProcess.Controls.Clear();
                panelProcess.Controls.Add(_processGridControl);

                _logger.LogDebug("流程表格控件已初始化");

                // 创建右键菜单管理器
                _menuManager = new StepContextMenuManager(
                    _processGridControl.DataGridView,
                    _workflowState,
                    new DataGridViewManager(_processGridControl.DataGridView),
                    _logger,
                    this);

                _logger.LogInformation("自定义控件初始化完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化自定义控件时发生错误");
                throw;
            }
        }
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
                _workflowState.StepsChanged += OnStepsChanged;

           
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

                // 先清空数据源
                _workflowState.ClearSteps();

                if (parent?.ChildSteps != null && parent.ChildSteps.Count > 0)
                {
                    // 批量加载到数据源（会触发事件）
                    foreach (var step in parent.ChildSteps)
                    {
                        _workflowState.AddStep(new ChildModel
                        {
                            StepName = step.StepName,
                            Status = step.Status,
                            StepNum = step.StepNum,
                            StepParameter = step.StepParameter,
                            Remark = step.Remark ?? ""
                        });
                    }

                    _logger.LogInformation("成功加载 {Count} 个步骤", parent.ChildSteps.Count);
                }
                else
                {
                    // 如果没有数据，手动刷新UI显示空表格
                    _processGridControl.RefreshGrid();
                    _logger.LogInformation("没有找到已保存的步骤数据");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载步骤数据错误", ex);
                MessageHelper.MessageOK($"加载步骤数据错误：{ex.Message}", TType.Error);
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

                // 将VarItem转换为VarItem_Enhanced并添加
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
                _workflowState.ClearUserVariables();
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
                // 初始化默认变量列表
                Variable =
                [
                    new VarItem { VarName = "开始气压", VarType = "double", VarText = "开始气压" },
                    new VarItem { VarName = "结束气压", VarType = "double", VarText = "结束气压" },
                    new VarItem { VarName = "泄漏量", VarType = "double", VarText = "泄漏量" },
                    new VarItem { VarName = "开始时间", VarType = "string", VarText = "开始时间" },
                    new VarItem { VarName = "结束时间", VarType = "string", VarText = "结束时间" },
                    new VarItem { VarName = "总用时", VarType = "double", VarText = "总用时" }
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
            var newStep = new ChildModel
            {
                StepName = stepName,
                Status = 0,
                StepNum = stepNumber,
                StepParameter = 0
            };

            // 可以通过控件添加
            _processGridControl.AddStep(newStep);
        }

        /// <summary>
        /// 更新步骤状态显示
        /// </summary>
        private void UpdateStepStatus(ChildModel step, int index)
        {
            try
            {
                // 使用 DataGridViewManager 更新UI
                _processGridControl.UpdateStepStatus(index, step.Status);

                _logger.LogDebug("更新步骤状态: Index={Index}, Status={Status}", index, step.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新步骤状态时发生错误");
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

                // 确保在UI线程上执行
                if (InvokeRequired)
                {
                    Invoke(new Action<ChildModel>(OnStepAdded), step);
                    return;
                }

                // 从数据源刷新整个表格，确保数据一致性
                _processGridControl.RefreshGrid();

                _logger.LogInformation("UI已更新：步骤 {StepName} 添加成功", step.StepName);
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

                // 确保在UI线程上执行
                if (InvokeRequired)
                {
                    Invoke(new Action<ChildModel>(OnStepRemoved), step);
                    return;
                }

                // 从数据源刷新整个表格，确保数据一致性
                _processGridControl.RefreshGrid();

                _logger.LogInformation("UI已更新：步骤 {StepName} 移除成功", step.StepName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理步骤移除事件时发生错误");
            }
        }

        /// <summary>
        /// 批量步骤变更事件处理 - UI自动刷新
        /// 用于处理 ClearSteps、批量插入等操作
        /// </summary>
        private void OnStepsChanged()
        {
            try
            {
                _logger.LogDebug("步骤集合已发生批量变更");

                // 确保在UI线程上执行
                if (InvokeRequired)
                {
                    Invoke(new Action(OnStepsChanged));
                    return;
                }

                // 从数据源刷新整个表格
                _processGridControl.RefreshGrid();

                _logger.LogInformation("UI已更新：步骤集合刷新完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理批量步骤变更事件时发生错误");
            }
        }

        // 只需要处理工具被选择后的逻辑
        private void OnToolSelected(object sender, ToolSelectedEventArgs e)
        {
            var stepNum = _processGridControl.StepCount + 1;
            AddStepToForm(stepNum, e.ToolName);
        }

        // 表格拖放事件
        private void OnProcessGridDragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        // 使用控件的属性
                        AddStepToForm(_processGridControl.StepCount + 1, node.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "拖拽步骤错误");
            }
        }


        private void OnStepConfigRequested(object sender, StepConfigEventArgs e)
        {
            try
            {
                // 步骤信息已经在事件参数中
                _workflowState.StepNum = e.RowIndex;
                _workflowState.StepName = e.Step.StepName;
                _formService.OpenFormByName(this, e.Step.StepName, this);
                _processGridControl.RefreshGrid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "打开步骤配置时发生错误");
            }
        }

        #endregion

        #region 点击事件处理

        /// <summary>
        /// 单元格开始编辑事件 - 只允许编辑备注列
        /// </summary>
        private void ProcessDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 只允许编辑备注列
            if (_processGridControl.DataGridView.Columns[e.ColumnIndex].Name != "ColRemark")
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 单元格编辑完成事件 - 保存备注到数据源
        /// </summary>
        private void ProcessDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 确保是备注列
                if (_processGridControl.DataGridView.Columns[e.ColumnIndex].Name != "ColRemark")
                    return;

                var row = _processGridControl.DataGridView.Rows[e.RowIndex];
                string newRemark = row.Cells["ColRemark"].Value?.ToString() ?? "";

                // 更新数据源
                var step = _workflowState.GetStep(e.RowIndex);
                if (step != null)
                {
                    step.Remark = newRemark;
                    _logger.LogDebug("步骤 {StepNum} 备注已更新: {Remark}", step.StepNum, newRemark);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新备注时发生错误");
            }
        }

        private void OnGridSelectionChanged(object sender, EventArgs e)
        {
            // 使用控件的属性
            if (_processGridControl.SelectedIndex >= 0)
            {
                UpdateStepDetails(_processGridControl.SelectedIndex);
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
                    config.Variable.AddRange(variables.Where(v => !v.IsSystemVariable).
                        Cast<VarItem_Enhanced>());

                    // 使用线程安全方法获取步骤
                    config.Form[0].ChildSteps.Clear();
                    var steps = _workflowState.GetSteps();
                    config.Form[0].ChildSteps.AddRange(steps);

                    await Task.CompletedTask;
                });

                // 保存后刷新表格显示,更新步骤详情列
                _processGridControl.RefreshGrid();

                _logger.LogInformation("工作流配置保存成功");
                MessageHelper.MessageOK(this, "保存成功！", TType.Success);
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
                    _processGridControl.DataGridView.ClearSelection();

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
                _processGridControl.SelectRow(stepNum);
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
            if (stepIndex < 0 || stepIndex >= _processGridControl.DataGridView.Rows.Count)
                return;

            DataGridViewRow row = _processGridControl.DataGridView.Rows[stepIndex];

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
        /// 处理键盘快捷键
        /// </summary>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            // 如果焦点在 DataGridView 上，让菜单管理器处理快捷键
            if (_processGridControl.DataGridView.Focused)
            {
                if (_menuManager.HandleKeyDown(keyData))
                {
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
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

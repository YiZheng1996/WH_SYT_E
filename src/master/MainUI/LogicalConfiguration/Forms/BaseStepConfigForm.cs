using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;
using ContextMenuStrip = System.Windows.Forms.ContextMenuStrip;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 步骤配置窗体基类 - 提供工具箱、拖拽、配置等通用功能
    /// </summary>
    public partial class BaseStepConfigForm : UIForm
    {
        #region 受保护字段

        /// <summary>
        /// 工具箱树形控件
        /// </summary>
        protected TreeView treeViewTools;

        /// <summary>
        /// 步骤列表表格
        /// </summary>
        protected DataGridView dgvSteps;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected ILogger _logger;

        /// <summary>
        /// 是否有未保存的更改
        /// </summary>
        protected bool _hasUnsavedChanges = false;

        /// <summary>
        /// DataGridView管理器
        /// </summary>
        protected DataGridViewManager _gridManager;

        #endregion

        #region 属性和方法

        private List<ChildModel> _defaultSteps = [];
        /// <summary>
        /// 获取当前步骤列表
        /// </summary>
        protected virtual List<ChildModel> GetStepsList()
        {
            return _defaultSteps;
        }


        /// <summary>
        /// 设置步骤列表
        /// </summary>
        protected virtual void SetStepsList(List<ChildModel> steps)
        {
            _defaultSteps = steps;
        }

        /// <summary>
        /// 获取是否允许循环控制（主流程允许，子步骤不允许）
        /// </summary>
        protected virtual bool AllowLoopControl { get; }

        /// <summary>
        /// 获取窗体标题
        /// </summary>
        protected virtual string FormTitle { get; }

        /// <summary>
        /// 保存配置（由子类实现具体保存逻辑）
        /// </summary>
        protected virtual void SaveConfiguration()
        {
            // 默认空实现，子类重写
            _logger?.LogWarning("SaveConfiguration未实现");
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 基类构造函数
        /// </summary>
        protected BaseStepConfigForm()
        {
            InitializeBaseComponents();
            InitializeCommonUI();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化基础组件
        /// </summary>
        private void InitializeBaseComponents()
        {
            // 创建工具箱
            treeViewTools = new TreeView
            {
                Name = "treeViewTools",
                Dock = DockStyle.Left,
                Width = 200,
                HideSelection = false,
                ItemHeight = 30,
                ShowLines = true,
                ShowPlusMinus = true,
                ShowRootLines = true
            };

            // 创建步骤列表
            dgvSteps = new DataGridView
            {
                Name = "dgvSteps",
                Dock = DockStyle.Fill,
                AllowDrop = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 35,
                GridColor = Color.FromArgb(224, 224, 224),
                ReadOnly = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ShowCellErrors = false,
                ShowRowErrors = false
            };

            // 初始化列
            InitializeGridColumns();

            // 设置事件处理
            RegisterBaseEventHandlers();
        }

        /// <summary>
        /// 初始化表格列
        /// </summary>
        private void InitializeGridColumns()
        {
            dgvSteps.Columns.Clear();

            // 步骤序号列
            var colIndex = new DataGridViewTextBoxColumn
            {
                Name = "ColIndex",
                HeaderText = "步骤",
                Width = 60,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };

            // 步骤名称列
            var colStepName = new DataGridViewTextBoxColumn
            {
                Name = "ColStepName",
                HeaderText = "步骤名称",
                Width = 150,
                ReadOnly = true
            };

            // 备注列
            var colRemark = new DataGridViewTextBoxColumn
            {
                Name = "ColRemark",
                HeaderText = "备注",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = false
            };

            // 操作列
            var colAction = new DataGridViewButtonColumn
            {
                Name = "ColAction",
                HeaderText = "操作",
                Text = "配置",
                UseColumnTextForButtonValue = true,
                Width = 80
            };

            dgvSteps.Columns.AddRange(colIndex, colStepName, colRemark, colAction);
        }

        /// <summary>
        /// 初始化通用UI样式
        /// </summary>
        private void InitializeCommonUI()
        {
            // 设置窗体基础属性
            this.ShowTitle = true;
            this.ShowIcon = true;
            this.TopMost = false;
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = FormTitle;

            // 设置主题颜色
            this.TitleColor = Color.FromArgb(65, 100, 204);
            this.RectColor = Color.FromArgb(65, 100, 204);

            // 设置字体
            var defaultFont = new Font("微软雅黑", 10F);
            treeViewTools.Font = defaultFont;
            dgvSteps.Font = defaultFont;
            dgvSteps.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
        }

        #endregion

        #region 工具箱初始化

        /// <summary>
        /// 初始化工具箱（供子类调用或重写）
        /// </summary>
        protected virtual void InitializeToolBox()
        {
            try
            {
                treeViewTools.Nodes.Clear();
                treeViewTools.ImageList = new ImageList { ImageSize = new Size(20, 20) };

                // 逻辑控制组
                var logicNode = new TreeNode("逻辑控制")
                {
                    Tag = "LogicControl",
                    ForeColor = Color.FromArgb(52, 58, 64)
                };
                logicNode.Nodes.Add(new TreeNode("延时等待") { Tag = "DelayWait" });
                logicNode.Nodes.Add(new TreeNode("条件判断") { Tag = "ConditionJudge" });
                logicNode.Nodes.Add(new TreeNode("等待稳定") { Tag = "Waitingforstability" });

                // 只在主流程中添加循环控制
                if (AllowLoopControl)
                {
                    logicNode.Nodes.Add(new TreeNode("循环开始") { Tag = "LoopControlStart" });
                    logicNode.Nodes.Add(new TreeNode("循环结束") { Tag = "LoopControlStop" });
                }

                treeViewTools.Nodes.Add(logicNode);

                // 数据操作组
                var dataNode = new TreeNode("数据操作")
                {
                    Tag = "DataOperation",
                    ForeColor = Color.FromArgb(40, 167, 69)
                };
                dataNode.Nodes.Add(new TreeNode("变量赋值") { Tag = "VariableAssign" });
                dataNode.Nodes.Add(new TreeNode("消息通知") { Tag = "MessageNotify" });
                treeViewTools.Nodes.Add(dataNode);

                // PLC通信组
                var plcNode = new TreeNode("通信操作")
                {
                    Tag = "PLCCommunication",
                    ForeColor = Color.FromArgb(13, 110, 253)
                };
                plcNode.Nodes.Add(new TreeNode("读取PLC") { Tag = "PLCRead" });
                plcNode.Nodes.Add(new TreeNode("写入PLC") { Tag = "PLCWrite" });
                treeViewTools.Nodes.Add(plcNode);

                // 报表操作组
                var reportNode = new TreeNode("报表工具")
                {
                    Tag = "ReportTools",
                    ForeColor = Color.FromArgb(255, 140, 0)
                };
                reportNode.Nodes.Add(new TreeNode("读取单元格") { Tag = "ReadCells" });
                reportNode.Nodes.Add(new TreeNode("写入单元格") { Tag = "WriteCells" });
                treeViewTools.Nodes.Add(reportNode);

                // 展开所有节点
                treeViewTools.ExpandAll();

                _logger?.LogDebug("工具箱初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化工具箱失败");
            }
        }

        #endregion

        #region 拖拽处理

        /// <summary>
        /// 注册基础事件处理器
        /// </summary>
        private void RegisterBaseEventHandlers()
        {
            // 工具箱拖拽
            treeViewTools.ItemDrag += TreeViewTools_ItemDrag;

            // DataGridView拖拽
            dgvSteps.DragEnter += DgvSteps_DragEnter;
            dgvSteps.DragDrop += DgvSteps_DragDrop;

            // 双击配置
            dgvSteps.CellDoubleClick += DgvSteps_CellDoubleClick;
            dgvSteps.CellContentClick += DgvSteps_CellContentClick;

            // 值改变事件
            dgvSteps.CellValueChanged += DgvSteps_CellValueChanged;
        }

        /// <summary>
        /// 工具箱拖拽开始
        /// </summary>
        protected virtual void TreeViewTools_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode node && node.Parent != null)
            {
                treeViewTools.DoDragDrop(node, DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// 拖拽进入表格
        /// </summary>
        protected virtual void DgvSteps_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(TreeNode))
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        /// <summary>
        /// 拖拽放置
        /// </summary>
        protected virtual void DgvSteps_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        AddStep(node.Text, node.Tag?.ToString());
                        _hasUnsavedChanges = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "拖拽添加步骤失败");
                MessageHelper.MessageOK($"添加步骤失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 步骤管理

        /// <summary>
        /// 添加步骤
        /// </summary>
        protected virtual void AddStep(string stepName, string stepTag)
        {
            try
            {
                var steps = GetStepsList();

                var newStep = new ChildModel
                {
                    StepNum = steps.Count + 1,
                    StepName = stepName,
                    Remark = $"{stepName}步骤",
                    StepParameter = null
                };

                steps.Add(newStep);

                // 刷新表格显示
                LoadStepsToGrid();

                _logger?.LogInformation("添加步骤: {StepName}", stepName);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "添加步骤失败");
                throw;
            }
        }

        /// <summary>
        /// 删除步骤
        /// </summary>
        protected virtual void RemoveStep(int stepIndex)
        {
            try
            {
                var steps = GetStepsList();

                if (stepIndex >= 0 && stepIndex < steps.Count)
                {
                    steps.RemoveAt(stepIndex);

                    // 重新编号
                    for (int i = stepIndex; i < steps.Count; i++)
                    {
                        steps[i].StepNum = i + 1;
                    }

                    // 刷新显示
                    LoadStepsToGrid();
                    _hasUnsavedChanges = true;

                    _logger?.LogInformation("删除步骤索引: {Index}", stepIndex);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除步骤失败");
                MessageHelper.MessageOK($"删除步骤失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 加载步骤到表格
        /// </summary>
        protected virtual void LoadStepsToGrid()
        {
            try
            {
                dgvSteps.Rows.Clear();
                var steps = GetStepsList();

                foreach (var step in steps)
                {
                    int rowIndex = dgvSteps.Rows.Add();
                    var row = dgvSteps.Rows[rowIndex];

                    row.Cells["ColIndex"].Value = step.StepNum;
                    row.Cells["ColStepName"].Value = step.StepName;
                    row.Cells["ColRemark"].Value = step.Remark ?? "";
                    row.Tag = step; // 保存完整的步骤对象
                }

                _logger?.LogDebug("加载了 {Count} 个步骤到表格", steps.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载步骤到表格失败");
            }
        }

        #endregion

        #region 步骤配置

        /// <summary>
        /// 双击配置步骤
        /// </summary>
        protected virtual void DgvSteps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                ConfigureStep(e.RowIndex);
            }
        }

        /// <summary>
        /// 点击配置按钮
        /// </summary>
        protected virtual void DgvSteps_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSteps.Columns[e.ColumnIndex].Name == "ColAction")
            {
                ConfigureStep(e.RowIndex);
            }
        }

        /// <summary>
        /// 配置指定步骤
        /// </summary>
        protected virtual void ConfigureStep(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= dgvSteps.Rows.Count)
                    return;

                if (dgvSteps.Rows[rowIndex].Tag is not ChildModel step)
                {
                    _logger?.LogWarning("步骤数据为空，无法配置");
                    return;
                }

                // 创建配置窗体
                var configForm = CreateStepConfigForm(step);
                if (configForm == null)
                {
                    MessageHelper.MessageOK($"步骤 {step.StepName} 暂不支持配置", TType.Warn);
                    return;
                }

                // 加载现有参数
                LoadParameterToForm(configForm, step.StepParameter);

                // 显示配置窗体
                if (configForm.ShowDialog(this) == DialogResult.OK)
                {
                    // 获取并保存参数
                    var updatedParameter = GetParameterFromForm(configForm);
                    if (updatedParameter != null)
                    {
                        step.StepParameter = JsonConvert.SerializeObject(updatedParameter);
                        _logger?.LogDebug("步骤 {StepName} 参数已更新", step.StepName);
                    }

                    // 更新备注
                    UpdateStepRemark(configForm, step, rowIndex);

                    _hasUnsavedChanges = true;
                    _logger?.LogInformation("步骤 {StepName} 配置完成", step.StepName);
                }

                configForm.Dispose();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "配置步骤失败");
                MessageHelper.MessageOK($"配置步骤失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 创建步骤配置窗体
        /// </summary>
        protected virtual Form CreateStepConfigForm(ChildModel step)
        {
            try
            {
                string formName = $"Form_{step.StepName}";

                var formType = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(t => t.Name == formName && t.IsSubclassOf(typeof(Form)));

                if (formType == null)
                {
                    _logger?.LogWarning("未找到窗体类型: {FormName}", formName);
                    return null;
                }

                // 尝试创建实例
                Form form = null;

                // 尝试无参构造函数
                var constructor = formType.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    form = (Form)Activator.CreateInstance(formType);
                }
                else
                {
                    // 尝试通过DI容器
                    form = Program.ServiceProvider?.GetService(formType) as Form;
                }

                return form;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "创建配置窗体失败");
                return null;
            }
        }

        /// <summary>
        /// 加载参数到配置窗体
        /// </summary>
        protected virtual void LoadParameterToForm(Form form, object stepParameter)
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

                // 如果是JSON字符串，反序列化
                if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
                {
                    var paramType = parameterProperty.PropertyType;
                    var deserializedParam = JsonConvert.DeserializeObject(jsonStr, paramType);
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
        protected virtual object GetParameterFromForm(Form form)
        {
            try
            {
                // 尝试多种方式获取参数

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

                // 3. 查找带下划线的私有字段
                var fields = form.GetType().GetFields(
                    BindingFlags.NonPublic | BindingFlags.Instance);

                var paramField = fields.FirstOrDefault(f =>
                    f.Name.Contains("parameter", StringComparison.OrdinalIgnoreCase) ||
                    f.Name.Contains("_param", StringComparison.OrdinalIgnoreCase));

                if (paramField != null)
                {
                    return paramField.GetValue(form);
                }

                _logger?.LogWarning("无法从窗体获取参数");
                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "从窗体获取参数失败");
                return null;
            }
        }

        /// <summary>
        /// 更新步骤备注
        /// </summary>
        protected virtual void UpdateStepRemark(Form form, ChildModel step, int rowIndex)
        {
            try
            {
                var remarkProperty = form.GetType().GetProperty("Remark");
                if (remarkProperty != null)
                {
                    step.Remark = remarkProperty.GetValue(form)?.ToString() ?? step.Remark;
                    dgvSteps.Rows[rowIndex].Cells["ColRemark"].Value = step.Remark ?? "";
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新备注失败");
            }
        }

        #endregion

        #region 单元格编辑

        /// <summary>
        /// 单元格值改变事件（用于备注编辑）
        /// </summary>
        protected virtual void DgvSteps_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvSteps.Columns[e.ColumnIndex].Name == "ColRemark")
                {
                    var step = dgvSteps.Rows[e.RowIndex].Tag as ChildModel;
                    if (step != null)
                    {
                        step.Remark = dgvSteps.Rows[e.RowIndex].Cells["ColRemark"].Value?.ToString() ?? "";
                        _hasUnsavedChanges = true;
                        _logger?.LogDebug("步骤 {StepNum} 备注已更新", step.StepNum);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新单元格值失败");
            }
        }

        #endregion

        #region 窗体关闭处理

        /// <summary>
        /// 窗体关闭前检查
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_hasUnsavedChanges && this.DialogResult != DialogResult.OK)
            {
                var result = MessageHelper.MessageYes(
                    "有未保存的更改，是否放弃？");

                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }

        #endregion

        #region 上下文菜单

        /// <summary>
        /// 创建右键菜单（供子类扩展）
        /// </summary>
        protected virtual ContextMenuStrip CreateContextMenu()
        {
            var menu = new ContextMenuStrip();

            var deleteItem = new ToolStripMenuItem("删除步骤", null, (s, e) =>
            {
                if (dgvSteps.SelectedRows.Count > 0)
                {
                    RemoveStep(dgvSteps.SelectedRows[0].Index);
                }
            });

            var clearItem = new ToolStripMenuItem("清空所有", null, (s, e) =>
            {
                if (MessageHelper.MessageYes("确定要清空所有步骤吗？") == DialogResult.OK)
                {
                    GetStepsList().Clear();
                    LoadStepsToGrid();
                    _hasUnsavedChanges = true;
                }
            });

            menu.Items.AddRange(
            [
                deleteItem,
                new ToolStripSeparator(),
                clearItem
            ]);

            return menu;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 显示日志信息（供子类使用）
        /// </summary>
        protected virtual void LogInfo(string message)
        {
            _logger?.LogInformation(message);
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        protected virtual void LogError(string message, Exception ex = null)
        {
            if (ex != null)
                _logger?.LogError(ex, message);
            else
                _logger?.LogError(message);
        }

        #endregion
    }
}
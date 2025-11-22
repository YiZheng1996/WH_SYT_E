using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 循环体子步骤配置窗体
    /// 用于配置循环内部要执行的步骤序列
    /// </summary>
    public partial class Form_ChildStepsConfig : Sunny.UI.UIForm
    {
        #region 私有字段

        /// <summary>
        /// 子步骤列表
        /// </summary>
        private List<ChildModel> _childSteps;

        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly ILogger<Form_ChildStepsConfig> _logger;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="childSteps">要编辑的子步骤列表</param>
        /// <param name="logger">日志服务</param>
        public Form_ChildStepsConfig(
            List<ChildModel> childSteps,
            ILogger<Form_ChildStepsConfig> logger = null)
        {
            InitializeComponent();

            // 深拷贝子步骤列表,避免直接修改原始数据
            _childSteps = childSteps != null
                ? JsonConvert.DeserializeObject<List<ChildModel>>(JsonConvert.SerializeObject(childSteps))
                : new List<ChildModel>();

            _logger = logger;

            InitializeToolBox();
            LoadStepsToGrid();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化工具箱
        /// </summary>
        private void InitializeToolBox()
        {
            try
            {
                treeViewTools.Nodes.Clear();
                treeViewTools.ImageList = new ImageList();

                // 逻辑控制组
                TreeNode logicNode = new TreeNode("逻辑控制") { Tag = "LogicControl" };
                logicNode.Nodes.Add(new TreeNode("延时等待") { Tag = "DelayWait" });
                logicNode.Nodes.Add(new TreeNode("条件判断") { Tag = "ConditionJudge" });
                logicNode.Nodes.Add(new TreeNode("等待稳定") { Tag = "Waitingforstability" });
                treeViewTools.Nodes.Add(logicNode);

                // 数据操作组
                TreeNode dataNode = new TreeNode("数据操作") { Tag = "DataOperation" };
                dataNode.Nodes.Add(new TreeNode("变量赋值") { Tag = "VariableAssign" });
                treeViewTools.Nodes.Add(dataNode);

                // PLC通信组
                TreeNode plcNode = new TreeNode("通信操作") { Tag = "PLCCommunication" };
                plcNode.Nodes.Add(new TreeNode("读取PLC") { Tag = "ReadPLC" });
                plcNode.Nodes.Add(new TreeNode("写入PLC") { Tag = "WritePLC" });
                treeViewTools.Nodes.Add(plcNode);

                // 气路控制组
                TreeNode airNode = new TreeNode("气路控制") { Tag = "AirControl" };
                airNode.Nodes.Add(new TreeNode("开关控制") { Tag = "SwitchControl" });
                airNode.Nodes.Add(new TreeNode("压力控制") { Tag = "PressureControl" });
                airNode.Nodes.Add(new TreeNode("压力排气") { Tag = "PressureExhaust" });
                treeViewTools.Nodes.Add(airNode);

                // 检测工具组
                TreeNode detectionNode = new TreeNode("检测工具") { Tag = "Detection" };
                detectionNode.Nodes.Add(new TreeNode("检测工具") { Tag = "Detection" });
                treeViewTools.Nodes.Add(detectionNode);

                // 报表操作组
                TreeNode reportNode = new TreeNode("报表操作") { Tag = "ReportOperation" };
                reportNode.Nodes.Add(new TreeNode("读取单元格") { Tag = "ReadCells" });
                reportNode.Nodes.Add(new TreeNode("写入单元格") { Tag = "WriteCells" });
                reportNode.Nodes.Add(new TreeNode("保存报表") { Tag = "SaveReport" });
                treeViewTools.Nodes.Add(reportNode);

                // 其他工具组
                TreeNode otherNode = new TreeNode("其他工具") { Tag = "Other" };
                otherNode.Nodes.Add(new TreeNode("系统提示") { Tag = "SystemPrompt" });
                treeViewTools.Nodes.Add(otherNode);

                // 展开所有节点
                treeViewTools.ExpandAll();

                _logger?.LogDebug("工具箱初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化工具箱失败");
                MessageHelper.MessageOK($"初始化工具箱失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 加载步骤到表格
        /// </summary>
        private void LoadStepsToGrid()
        {
            try
            {
                dgvSteps.Rows.Clear();

                if (_childSteps == null || _childSteps.Count == 0)
                {
                    _logger?.LogDebug("没有子步骤需要加载");
                    return;
                }

                foreach (var step in _childSteps)
                {
                    int rowIndex = dgvSteps.Rows.Add();
                    var row = dgvSteps.Rows[rowIndex];

                    row.Cells["ColIndex"].Value = step.StepNum;
                    row.Cells["ColStepName"].Value = step.StepName;
                    row.Cells["ColRemark"].Value = step.Remark ?? "";

                    // 存储完整的步骤对象
                    row.Tag = step;
                }

                _logger?.LogInformation("加载了 {Count} 个子步骤", _childSteps.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载步骤到表格失败");
                MessageHelper.MessageOK($"加载步骤失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 步骤操作方法

        /// <summary>
        /// 添加步骤
        /// </summary>
        private void AddStep(string stepName)
        {
            try
            {
                int stepNum = _childSteps.Count + 1;

                var newStep = new ChildModel
                {
                    StepName = stepName,
                    Status = 0,
                    StepNum = stepNum,
                    StepParameter = 0,
                    Remark = ""
                };

                _childSteps.Add(newStep);

                // 添加到表格
                int rowIndex = dgvSteps.Rows.Add();
                var row = dgvSteps.Rows[rowIndex];

                row.Cells["ColIndex"].Value = stepNum;
                row.Cells["ColStepName"].Value = stepName;
                row.Cells["ColRemark"].Value = "";
                row.Tag = newStep;

                _logger?.LogDebug("添加步骤: {StepName}, 序号: {StepNum}", stepName, stepNum);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "添加步骤失败");
                MessageHelper.MessageOK($"添加步骤失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 删除选中的步骤
        /// </summary>
        private void DeleteSelectedStep()
        {
            try
            {
                if (dgvSteps.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要删除的步骤！", TType.Warn);
                    return;
                }

                var result = MessageHelper.MessageYes("确定要删除选中的步骤吗?", TType.Warn);
                if (result!= DialogResult.OK)
                    return;

                int rowIndex = dgvSteps.SelectedRows[0].Index;
                var step = dgvSteps.Rows[rowIndex].Tag as ChildModel;

                if (step != null)
                {
                    _childSteps.Remove(step);
                }

                dgvSteps.Rows.RemoveAt(rowIndex);

                // 重新编号
                RenumberSteps();

                _logger?.LogDebug("删除步骤成功,当前剩余 {Count} 个步骤", _childSteps.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除步骤失败");
                MessageHelper.MessageOK($"删除步骤失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 编辑选中的步骤
        /// </summary>
        private void EditSelectedStep()
        {
            try
            {
                if (dgvSteps.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要编辑的步骤！", TType.Warn);
                    return;
                }

                int rowIndex = dgvSteps.SelectedRows[0].Index;
                var step = dgvSteps.Rows[rowIndex].Tag as ChildModel;

                if (step == null)
                {
                    MessageHelper.MessageOK("无法获取步骤信息！", TType.Error);
                    return;
                }

                OpenStepConfigForm(rowIndex, step);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "编辑步骤失败");
                MessageHelper.MessageOK($"编辑步骤失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 上移选中的步骤
        /// </summary>
        private void MoveUpSelectedStep()
        {
            try
            {
                if (dgvSteps.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的步骤！", TType.Warn);
                    return;
                }

                int rowIndex = dgvSteps.SelectedRows[0].Index;

                if (rowIndex == 0)
                {
                    MessageHelper.MessageOK("已经是第一个步骤,无法上移！", TType.Info);
                    return;
                }

                // 交换数据
                var temp = _childSteps[rowIndex];
                _childSteps[rowIndex] = _childSteps[rowIndex - 1];
                _childSteps[rowIndex - 1] = temp;

                // 重新编号
                RenumberSteps();

                // 刷新表格
                LoadStepsToGrid();

                // 保持选中
                if (rowIndex - 1 >= 0)
                    dgvSteps.Rows[rowIndex - 1].Selected = true;

                _logger?.LogDebug("步骤上移成功");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "上移步骤失败");
                MessageHelper.MessageOK($"上移步骤失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 下移选中的步骤
        /// </summary>
        private void MoveDownSelectedStep()
        {
            try
            {
                if (dgvSteps.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的步骤！", TType.Warn);
                    return;
                }

                int rowIndex = dgvSteps.SelectedRows[0].Index;

                if (rowIndex >= dgvSteps.Rows.Count - 1)
                {
                    MessageHelper.MessageOK("已经是最后一个步骤,无法下移！", TType.Info);
                    return;
                }

                // 交换数据
                var temp = _childSteps[rowIndex];
                _childSteps[rowIndex] = _childSteps[rowIndex + 1];
                _childSteps[rowIndex + 1] = temp;

                // 重新编号
                RenumberSteps();

                // 刷新表格
                LoadStepsToGrid();

                // 保持选中
                if (rowIndex + 1 < dgvSteps.Rows.Count)
                    dgvSteps.Rows[rowIndex + 1].Selected = true;

                _logger?.LogDebug("步骤下移成功");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "下移步骤失败");
                MessageHelper.MessageOK($"下移步骤失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 重新编号所有步骤
        /// </summary>
        private void RenumberSteps()
        {
            for (int i = 0; i < _childSteps.Count; i++)
            {
                _childSteps[i].StepNum = i + 1;
            }
        }

        /// <summary>
        /// 打开步骤配置窗体
        /// ⭐ 重新设计: 直接实例化窗体,不依赖 FormService 和 IWorkflowStateService
        /// </summary>
        private void OpenStepConfigForm(int rowIndex, ChildModel step)
        {
            try
            {
                _logger?.LogDebug("打开步骤配置: {StepName}, 行索引: {RowIndex}", step.StepName, rowIndex);

                // 根据步骤名称创建对应的配置窗体
                Form configForm = CreateStepConfigForm(step);

                if (configForm == null)
                {
                    MessageHelper.MessageOK($"步骤 {step.StepName} 暂不支持配置", TType.Warn);
                    return;
                }

                // 显示窗体
                var result = configForm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // 从窗体获取配置好的参数
                    object updatedParameter = GetParameterFromForm(configForm);

                    if (updatedParameter != null)
                    {
                        step.StepParameter = updatedParameter;

                        // 如果窗体有 Remark 属性,也更新备注
                        var remarkProperty = configForm.GetType().GetProperty("Remark");
                        if (remarkProperty != null)
                        {
                            step.Remark = remarkProperty.GetValue(configForm)?.ToString() ?? step.Remark;
                        }

                        // 更新表格显示
                        dgvSteps.Rows[rowIndex].Cells["ColRemark"].Value = step.Remark ?? "";

                        _logger?.LogDebug("步骤参数已更新");
                    }
                }

                configForm.Dispose();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "打开步骤配置窗体失败");
                MessageHelper.MessageOK($"打开配置窗体失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 根据步骤名称创建对应的配置窗体实例
        /// </summary>
        private Form CreateStepConfigForm(ChildModel step)
        {
            try
            {
                // 步骤名称到窗体类名的映射
                string formName = $"Form_{step.StepName}";

                // 通过反射创建窗体实例
                var formType = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(t => t.Name == formName && t.IsSubclassOf(typeof(Form)));

                if (formType == null)
                {
                    _logger?.LogWarning("未找到步骤 {StepName} 对应的窗体类型 {FormName}", step.StepName, formName);
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
                    // 尝试通过服务容器创建(如果有依赖注入)
                    form = Program.ServiceProvider?.GetService(formType) as Form;
                }

                if (form == null)
                {
                    _logger?.LogError("无法创建窗体实例: {FormName}", formName);
                    return null;
                }

                // 如果窗体实现了参数接口,加载参数
                LoadParameterToForm(form, step.StepParameter);

                return form;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "创建步骤配置窗体失败");
                return null;
            }
        }

        /// <summary>
        /// 将参数加载到窗体
        /// </summary>
        private void LoadParameterToForm(Form form, object stepParameter)
        {
            try
            {
                // 查找 Parameter 属性
                var parameterProperty = form.GetType().GetProperty("Parameter");
                if (parameterProperty != null && parameterProperty.CanWrite)
                {
                    // 如果参数是字符串(JSON),尝试反序列化
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
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载参数到窗体失败");
            }
        }

        /// <summary>
        /// 从窗体获取配置好的参数
        /// </summary>
        private object GetParameterFromForm(Form form)
        {
            try
            {
                // 查找 Parameter 属性
                var parameterProperty = form.GetType().GetProperty("Parameter");
                if (parameterProperty != null && parameterProperty.CanRead)
                {
                    var parameter = parameterProperty.GetValue(form);

                    // 序列化为JSON字符串
                    if (parameter != null)
                    {
                        return JsonConvert.SerializeObject(parameter);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "从窗体获取参数失败");
                return null;
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 工具箱拖拽事件
        /// </summary>
        private void TreeViewTools_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode node && node.Parent != null)
            {
                treeViewTools.DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// DataGridView 拖放进入事件
        /// </summary>
        private void DgvSteps_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(TreeNode))
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        /// <summary>
        /// DataGridView 拖放事件
        /// </summary>
        private void DgvSteps_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        AddStep(node.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "拖拽步骤失败");
                MessageHelper.MessageOK($"拖拽步骤失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// DataGridView 双击事件
        /// </summary>
        private void DgvSteps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedStep();
            }
        }

        /// <summary>
        /// DataGridView 单元格开始编辑事件 - 只允许编辑备注列
        /// </summary>
        private void DgvSteps_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvSteps.Columns[e.ColumnIndex].Name != "ColRemark")
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// DataGridView 单元格编辑完成事件
        /// </summary>
        private void DgvSteps_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSteps.Columns[e.ColumnIndex].Name != "ColRemark")
                    return;

                var row = dgvSteps.Rows[e.RowIndex];
                string newRemark = row.Cells["ColRemark"].Value?.ToString() ?? "";

                var step = row.Tag as ChildModel;
                if (step != null)
                {
                    step.Remark = newRemark;
                    _logger?.LogDebug("步骤 {StepNum} 备注已更新: {Remark}", step.StepNum, newRemark);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新备注失败");
            }
        }

        /// <summary>
        /// 添加按钮点击事件
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            MessageHelper.MessageOK("请从左侧工具箱拖拽步骤到列表中,或双击工具箱中的步骤进行添加。", TType.Info);
        }

        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedStep();
        }

        /// <summary>
        /// 上移按钮点击事件
        /// </summary>
        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            MoveUpSelectedStep();
        }

        /// <summary>
        /// 下移按钮点击事件
        /// </summary>
        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            MoveDownSelectedStep();
        }

        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedStep();
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 更新所有步骤的备注
                for (int i = 0; i < dgvSteps.Rows.Count; i++)
                {
                    var row = dgvSteps.Rows[i];
                    if (row.Tag is ChildModel step)
                    {
                        step.Remark = row.Cells["ColRemark"].Value?.ToString() ?? "";
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();

                _logger?.LogInformation("子步骤配置已保存,共 {Count} 个步骤", _childSteps.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存子步骤配置失败");
                MessageHelper.MessageOK($"保存失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取配置好的子步骤列表
        /// </summary>
        public List<ChildModel> GetChildSteps()
        {
            return _childSteps;
        }

        #endregion
    }
}
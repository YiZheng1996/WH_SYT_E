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
    /// ⭐ 修复版本 - 正确处理步骤参数的保存和加载
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
        /// <param name="logger">日志服务(可选)</param>
        public Form_ChildStepsConfig(
            List<ChildModel> childSteps,
            ILogger<Form_ChildStepsConfig> logger = null)
        {
            InitializeComponent();

            // ⭐ 深拷贝子步骤列表,避免直接修改原始数据
            _childSteps = childSteps != null
                ? JsonConvert.DeserializeObject<List<ChildModel>>(
                    JsonConvert.SerializeObject(childSteps))
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

                // 数据检测组
                TreeNode detectionNode = new TreeNode("数据检测") { Tag = "DataDetection" };
                detectionNode.Nodes.Add(new TreeNode("数据检测") { Tag = "Detection" });
                treeViewTools.Nodes.Add(detectionNode);

                // 设备控制组
                TreeNode deviceNode = new TreeNode("设备控制") { Tag = "DeviceControl" };
                deviceNode.Nodes.Add(new TreeNode("产品移入") { Tag = "ProductMoveIn" });
                deviceNode.Nodes.Add(new TreeNode("产品移出") { Tag = "ProductMoveOut" });
                treeViewTools.Nodes.Add(deviceNode);

                // Excel操作组
                TreeNode excelNode = new TreeNode("Excel操作") { Tag = "ExcelOperation" };
                excelNode.Nodes.Add(new TreeNode("读取单元格") { Tag = "ReadCells" });
                excelNode.Nodes.Add(new TreeNode("写入单元格") { Tag = "WriteCells" });
                treeViewTools.Nodes.Add(excelNode);

                // 展开所有节点
                treeViewTools.ExpandAll();

                _logger?.LogDebug("工具箱初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化工具箱失败");
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
                    DataGridViewRow row = dgvSteps.Rows[rowIndex];

                    row.Cells["ColStepNum"].Value = step.StepNum;
                    row.Cells["ColStepName"].Value = step.StepName;
                    row.Cells["ColRemark"].Value = step.Remark ?? "";

                    // ⭐ 重要:保存完整的步骤对象到Tag,包括参数
                    row.Tag = step;
                }

                _logger?.LogDebug("加载了 {Count} 个子步骤", _childSteps.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载步骤到表格失败");
            }
        }

        #endregion

        #region 工具箱拖放

        /// <summary>
        /// 工具箱节点鼠标按下事件 - 开始拖拽
        /// </summary>
        private void TreeViewTools_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode node && node.Tag != null)
            {
                DoDragDrop(node.Tag.ToString(), DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// DataGridView拖拽进入事件
        /// </summary>
        private void DgvSteps_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        /// <summary>
        /// DataGridView放置事件 - 添加步骤
        /// </summary>
        private void DgvSteps_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(string)))
                {
                    string stepName = e.Data.GetData(typeof(string)).ToString();

                    // 创建新步骤
                    var newStep = new ChildModel
                    {
                        StepNum = _childSteps.Count + 1,
                        StepName = stepName,
                        Remark = $"{stepName}步骤",
                        StepParameter = null // ⭐ 初始参数为空,需要配置后才有值
                    };

                    _childSteps.Add(newStep);

                    // 添加到表格
                    int rowIndex = dgvSteps.Rows.Add();
                    DataGridViewRow row = dgvSteps.Rows[rowIndex];

                    row.Cells["ColIndex"].Value = newStep.StepNum;
                    row.Cells["ColStepName"].Value = newStep.StepName;
                    row.Cells["ColRemark"].Value = newStep.Remark;
                    row.Tag = newStep; // ⭐ 保存步骤对象引用

                    _logger?.LogDebug("添加新步骤: {StepName}", stepName);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "添加步骤失败");
                MessageHelper.MessageOK($"添加步骤失败:{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 步骤编辑

        /// <summary>
        /// 双击表格行 - 打开步骤配置
        /// </summary>
        private void DgvSteps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedStep();
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
                    MessageHelper.MessageOK("请先选择要编辑的步骤!", TType.Warn);
                    return;
                }

                int rowIndex = dgvSteps.SelectedRows[0].Index;
                var step = dgvSteps.Rows[rowIndex].Tag as ChildModel;

                if (step == null)
                {
                    MessageHelper.MessageOK("无法获取步骤信息!", TType.Error);
                    return;
                }

                OpenStepConfigForm(rowIndex, step);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "编辑步骤失败");
                MessageHelper.MessageOK($"编辑步骤失败:{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 打开步骤配置窗体
        /// ⭐ 核心方法 - 使用反射创建配置窗体并处理参数
        /// </summary>
        private void OpenStepConfigForm(int rowIndex, ChildModel step)
        {
            try
            {
                _logger?.LogDebug("打开步骤配置: {StepName}, 行索引: {RowIndex}",
                    step.StepName, rowIndex);

                // 1. 创建配置窗体
                Form configForm = CreateStepConfigForm(step);

                if (configForm == null)
                {
                    MessageHelper.MessageOK($"步骤 {step.StepName} 暂不支持配置", TType.Warn);
                    return;
                }

                // 2. 加载现有参数到窗体(如果有)
                LoadParameterToForm(configForm, step.StepParameter);

                // 3. 显示配置窗体
                var result = configForm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // 4. ⭐ 从窗体获取配置好的参数并保存
                    object updatedParameter = GetParameterFromForm(configForm);

                    if (updatedParameter != null)
                    {
                        // ⭐ 序列化参数为JSON字符串保存(与主流程保持一致)
                        step.StepParameter = JsonConvert.SerializeObject(updatedParameter);

                        _logger?.LogDebug("步骤参数已更新并序列化: {StepName}", step.StepName);
                    }

                    // 5. 更新备注(如果窗体有Remark属性)
                    var remarkProperty = configForm.GetType().GetProperty("Remark");
                    if (remarkProperty != null)
                    {
                        step.Remark = remarkProperty.GetValue(configForm)?.ToString()
                            ?? step.Remark;
                    }

                    // 6. 更新表格显示
                    dgvSteps.Rows[rowIndex].Cells["ColRemark"].Value = step.Remark ?? "";

                    _logger?.LogInformation("步骤 {StepName} 配置完成", step.StepName);
                }

                configForm.Dispose();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "打开步骤配置窗体失败");
                MessageHelper.MessageOK($"打开配置窗体失败:{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// ⭐ 创建步骤配置窗体实例(使用反射)
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
                    _logger?.LogWarning("未找到步骤 {StepName} 对应的窗体类型 {FormName}",
                        step.StepName, formName);
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
                    // 尝试通过DI容器创建
                    form = Program.ServiceProvider?.GetService(formType) as Form;
                }

                if (form == null)
                {
                    _logger?.LogError("无法创建窗体实例: {FormName}", formName);
                    return null;
                }

                return form;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "创建步骤配置窗体失败");
                return null;
            }
        }

        /// <summary>
        /// ⭐ 将参数加载到窗体 - 支持JSON反序列化
        /// </summary>
        private void LoadParameterToForm(Form form, object stepParameter)
        {
            try
            {
                if (stepParameter == null)
                {
                    _logger?.LogDebug("步骤参数为空,使用默认值");
                    return;
                }

                // 查找窗体的Parameter属性
                var parameterProperty = form.GetType().GetProperty("Parameter");
                if (parameterProperty == null || !parameterProperty.CanWrite)
                {
                    _logger?.LogWarning("窗体 {FormType} 没有可写的Parameter属性",
                        form.GetType().Name);
                    return;
                }

                // ⭐ 如果参数是JSON字符串,需要反序列化
                if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
                {
                    var paramType = parameterProperty.PropertyType;
                    var deserializedParam = JsonConvert.DeserializeObject(jsonStr, paramType);
                    parameterProperty.SetValue(form, deserializedParam);

                    _logger?.LogDebug("从JSON反序列化参数: {ParamType}", paramType.Name);
                }
                else
                {
                    // 直接设置参数对象
                    parameterProperty.SetValue(form, stepParameter);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载参数到窗体失败");
            }
        }

        /// <summary>
        /// ⭐⭐⭐ 从窗体获取参数对象 - 这是关键的缺失方法!
        /// </summary>
        private object GetParameterFromForm(Form form)
        {
            try
            {
                // 方法1: 尝试获取Parameter属性
                var parameterProperty = form.GetType().GetProperty("Parameter");
                if (parameterProperty != null && parameterProperty.CanRead)
                {
                    var param = parameterProperty.GetValue(form);
                    if (param != null)
                    {
                        _logger?.LogDebug("从窗体获取Parameter属性: {ParamType}",
                            param.GetType().Name);
                        return param;
                    }
                }

                // 方法2: 尝试调用CollectParameters方法(如果窗体继承自BaseParameterForm)
                var collectMethod = form.GetType().GetMethod("CollectParameters",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (collectMethod != null)
                {
                    var param = collectMethod.Invoke(form, null);
                    if (param != null)
                    {
                        _logger?.LogDebug("从窗体调用CollectParameters方法: {ParamType}",
                            param.GetType().Name);
                        return param;
                    }
                }

                // 方法3: 尝试调用CollectTypedParameters方法(如果实现了IParameterForm接口)
                var collectTypedMethod = form.GetType().GetMethod("CollectTypedParameters",
                    BindingFlags.Instance | BindingFlags.Public);

                if (collectTypedMethod != null)
                {
                    var param = collectTypedMethod.Invoke(form, null);
                    if (param != null)
                    {
                        _logger?.LogDebug("从窗体调用CollectTypedParameters方法: {ParamType}",
                            param.GetType().Name);
                        return param;
                    }
                }

                _logger?.LogWarning("无法从窗体 {FormType} 获取参数", form.GetType().Name);
                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "从窗体获取参数失败");
                return null;
            }
        }

        #endregion

        #region 步骤管理

        /// <summary>
        /// 删除选中的步骤
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSteps.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要删除的步骤!", TType.Warn);
                    return;
                }

                var result = MessageHelper.MessageYes("确定要删除选中的步骤吗?", TType.Warn);
                if (result != DialogResult.OK)
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

                _logger?.LogDebug("删除步骤成功,剩余 {Count} 个", _childSteps.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除步骤失败");
                MessageHelper.MessageOK($"删除失败:{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 上移选中的步骤
        /// </summary>
        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSteps.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的步骤!", TType.Warn);
                    return;
                }

                int rowIndex = dgvSteps.SelectedRows[0].Index;
                if (rowIndex == 0)
                {
                    MessageHelper.MessageOK("已经是第一个步骤!", TType.Warn);
                    return;
                }

                // 交换数据源中的步骤
                var temp = _childSteps[rowIndex];
                _childSteps[rowIndex] = _childSteps[rowIndex - 1];
                _childSteps[rowIndex - 1] = temp;

                // 重新编号
                RenumberSteps();

                // 重新加载表格
                LoadStepsToGrid();

                // 选中移动后的行
                if (rowIndex - 1 >= 0)
                    dgvSteps.Rows[rowIndex - 1].Selected = true;

                _logger?.LogDebug("步骤上移成功");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "上移步骤失败");
                MessageHelper.MessageOK($"上移失败:{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 下移选中的步骤
        /// </summary>
        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSteps.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的步骤!", TType.Warn);
                    return;
                }

                int rowIndex = dgvSteps.SelectedRows[0].Index;
                if (rowIndex == dgvSteps.Rows.Count - 1)
                {
                    MessageHelper.MessageOK("已经是最后一个步骤!", TType.Warn);
                    return;
                }

                // 交换数据源中的步骤
                var temp = _childSteps[rowIndex];
                _childSteps[rowIndex] = _childSteps[rowIndex + 1];
                _childSteps[rowIndex + 1] = temp;

                // 重新编号
                RenumberSteps();

                // 重新加载表格
                LoadStepsToGrid();

                // 选中移动后的行
                if (rowIndex + 1 < dgvSteps.Rows.Count)
                    dgvSteps.Rows[rowIndex + 1].Selected = true;

                _logger?.LogDebug("步骤下移成功");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "下移步骤失败");
                MessageHelper.MessageOK($"下移失败:{ex.Message}", TType.Error);
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

        #endregion

        #region 保存和取消

        /// <summary>
        /// 保存按钮点击
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ⭐ 确保所有步骤的参数都已正确保存
                _logger?.LogInformation("保存子步骤配置,共 {Count} 个步骤", _childSteps.Count);

                // 记录每个步骤的参数状态(用于调试)
                for (int i = 0; i < _childSteps.Count; i++)
                {
                    var step = _childSteps[i];
                    bool hasParameter = step.StepParameter != null &&
                                       step.StepParameter.ToString().Length > 2;

                    _logger?.LogDebug("步骤 {Index}: {StepName}, 有参数: {HasParam}",
                        i + 1, step.StepName, hasParameter);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存子步骤配置失败");
                MessageHelper.MessageOK($"保存失败:{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        ///// <summary>
        ///// 添加按钮点击事件
        ///// </summary>
        //private void BtnAdd_Click(object sender, EventArgs e)
        //{
        //    MessageHelper.MessageOK("请从左侧工具箱拖拽步骤到列表中,或双击工具箱中的步骤进行添加。", TType.Info);
        //}


        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedStep();
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

                if (row.Tag is ChildModel step)
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


        #endregion

        #region 公共方法

        /// <summary>
        /// ⭐ 获取配置好的子步骤列表
        /// </summary>
        public List<ChildModel> GetChildSteps()
        {
            return _childSteps;
        }

        #endregion
    }
}
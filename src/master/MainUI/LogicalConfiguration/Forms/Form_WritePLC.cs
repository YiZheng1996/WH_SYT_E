using AntdUI;
using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// PLC写入参数配置表单
    /// 用于配置和管理工作流步骤中的PLC写入操作
    /// </summary>
    public partial class Form_WritePLC : BaseParameterForm, IParameterForm<Form_WritePLC>
    {
        #region 私有字段

        /// <summary>
        /// PLC管理器，用于PLC相关操作
        /// </summary>
        private readonly IPLCManager _pLCManager;

        /// <summary>
        /// 当前参数对象缓存
        /// </summary>
        private Parameter_WritePLC _currentParameter;

        #endregion

        #region 属性

        /// <summary>
        /// IParameterForm接口实现 - 参数对象
        /// TODO: 需要实现完整的get/set逻辑
        /// </summary>
        public Form_WritePLC Parameter
        {
            get => throw new NotImplementedException("需要实现Parameter属性的get逻辑");
            set => throw new NotImplementedException("需要实现Parameter属性的set逻辑");
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数 - 主要用于设计器
        /// </summary>
        public Form_WritePLC()
        {
            InitializeComponent();

            // 只有在非设计时模式才进行初始化，避免设计器错误
            if (!DesignMode)
            {
                try
                {
                    LoadWritePLCParameters();
                    InitializePointLocationPLC();
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "默认构造函数初始化失败");
                    MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        /// <param name="pLCManager">PLC管理器服务</param>
        public Form_WritePLC(IPLCManager pLCManager)
        {
            _pLCManager = pLCManager ?? throw new ArgumentNullException(nameof(pLCManager));

            InitializeComponent();

            try
            {
                LoadWritePLCParameters();
                InitializePointLocationPLC();

                _logger?.LogDebug("Form_WritePLC 依赖注入构造函数初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "依赖注入构造函数初始化失败");
                MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
            }
            finally
            {
            }
        }

        #endregion

        #region 初始化相关

        /// <summary>
        /// 加载写入PLC参数
        /// 从工作流状态中获取当前步骤的PLC写入参数并显示在界面上
        /// </summary>
        private void LoadWritePLCParameters()
        {
            try
            {
                // 检查服务可用性
                if (!IsServiceAvailable)
                {
                    _logger?.LogWarning("服务不可用，无法加载PLC参数");
                    return;
                }

                var steps = _workflowState.GetSteps();
                int idx = _workflowState.StepNum;

                // 验证步骤索引有效性
                if (steps == null || idx < 0 || idx >= steps.Count)
                {
                    _logger?.LogWarning("步骤索引无效: Index={Index}, Count={Count}", idx, steps?.Count ?? 0);
                    _currentParameter = new Parameter_WritePLC();
                    return;
                }

                var currentStep = steps[idx];
                var paramObj = currentStep.StepParameter;
                Parameter_WritePLC param = null;

                // 尝试多种方式解析参数对象
                if (paramObj is Parameter_WritePLC directParam)
                {
                    // 直接类型匹配
                    param = directParam;
                    _logger?.LogDebug("直接获取Parameter_WritePLC参数");
                }
                else if (paramObj != null)
                {
                    // 尝试JSON反序列化
                    try
                    {
                        string jsonString = paramObj is string s ? s : JsonConvert.SerializeObject(paramObj);
                        param = JsonConvert.DeserializeObject<Parameter_WritePLC>(jsonString);
                        _logger?.LogDebug("JSON反序列化获取Parameter_WritePLC参数");
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger?.LogWarning(jsonEx, "JSON反序列化失败，使用默认参数");
                        param = new Parameter_WritePLC();
                    }
                }
                else
                {
                    // 参数为空，创建默认参数
                    param = new Parameter_WritePLC();
                    _logger?.LogDebug("参数为空，创建默认Parameter_WritePLC参数");
                }

                _currentParameter = param ?? new Parameter_WritePLC();

                // 将参数数据绑定到界面控件
                BindParametersToUI();

                _logger?.LogInformation("成功加载PLC写入参数，包含{Count}个项目", _currentParameter.Items?.Count ?? 0);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载PLC写入参数时发生异常");
                _currentParameter = new Parameter_WritePLC();
                MessageHelper.MessageOK($"加载PLC参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 将参数数据绑定到UI控件
        /// </summary>
        private void BindParametersToUI()
        {
            try
            {
                if (_currentParameter?.Items == null) return;

                // 清空现有数据
                DataGridViewPLCList.Rows.Clear();

                // 添加参数项到DataGridView
                foreach (var item in _currentParameter.Items)
                {
                    var rowIndex = DataGridViewPLCList.Rows.Add();
                    var row = DataGridViewPLCList.Rows[rowIndex];

                    row.Cells["ColPCLModelName"].Value = item.PlcModuleName ?? "";
                    row.Cells["ColPCLKeyName"].Value = item.PlcKeyName ?? "";
                    row.Cells["ColConstant"].Value = item.PlcValue ?? "";
                }

                _logger?.LogDebug("UI数据绑定完成，共{Count}行", _currentParameter.Items.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "UI数据绑定失败");
            }
        }

        /// <summary>
        /// 初始化PLC点位信息
        /// 加载可用的PLC模块和键名供用户选择
        /// </summary>
        private async void InitializePointLocationPLC()
        {
            try
            {
                if (_pLCManager == null)
                {
                    _logger?.LogWarning("PLC管理器未初始化，无法加载PLC点位信息");
                    return;
                }

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

                _logger?.LogDebug("PLC点位信息初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化PLC点位信息失败");
            }
        }

        #endregion

        #region 事件处理
        private void TreeViewPLC_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeViewPLC.DoDragDrop(e.Item, DragDropEffects.Copy);
        }

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
                _logger.LogError(ex, "拖拽步骤错误");
                MessageHelper.MessageOK($"拖拽步骤错误：{ex.Message}", TType.Error);
            }
        }

        private void DataGridViewPLCList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(TreeNode)) ?
               DragDropEffects.Copy : DragDropEffects.None;
        }

        // 保存数据到当前步骤
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取当前步骤
                var currentStep = GetCurrentStepSafely();
                if (currentStep == null)
                {
                    MessageHelper.MessageOK("当前步骤无效，无法保存PLC数据。", TType.Warn);
                    return;
                }

                // 从界面收集PLC项目数据
                var plcItems = CollectPLCItemsFromUI();
                if (plcItems == null) return; // 验证失败

                // 检查是否有有效数据
                if (plcItems.Count == 0)
                {
                    MessageHelper.MessageOK("请至少添加一个有效的PLC操作。", TType.Warn);
                    return;
                }

                // 创建参数对象并保存
                var param = new Parameter_WritePLC { Items = plcItems };
                currentStep.StepParameter = JsonConvert.SerializeObject(param);

                _logger?.LogInformation("PLC写入参数保存成功，共{Count}个项目", plcItems.Count);
                MessageHelper.MessageOK("保存成功！PLC操作将在主界面保存时写入配置文件。", TType.Success);
                Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存PLC写入参数失败");
                MessageHelper.MessageOK($"保存PLC写入错误：{ex.Message}", TType.Error);
            }
        }

        // 删除选中的PLC行
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取选中的行索引
                int rowIndex = GetSelectedRowIndex();
                if (rowIndex < 0)
                {
                    MessageHelper.MessageOK("请选择要删除的PLC行！", TType.Warn);
                    return;
                }

                // 确认删除
                if (MessageHelper.MessageYes(this, "确定要删除选中的PLC行吗？") != DialogResult.OK)
                {
                    return;
                }

                // 执行删除操作
                if (DeletePLCItem(rowIndex))
                {
                    MessageHelper.MessageOK("删除成功！", TType.Success);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除PLC项目失败");
                MessageHelper.MessageOK($"删除失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 从UI界面收集PLC项目数据
        /// </summary>
        /// <returns>收集到的PLC项目列表，如果验证失败返回null</returns>
        private List<PlcWriteItem> CollectPLCItemsFromUI()
        {
            try
            {
                var plcItems = new List<PlcWriteItem>();
                bool hasEmptyConstant = false;

                // 遍历DataGridView中的所有行
                foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
                {
                    // 跳过新行（空行）
                    if (row.IsNewRow) continue;

                    // 提取单元格数据
                    string plcModelName = row.Cells["ColPCLModelName"].Value?.ToString()?.Trim() ?? "";
                    string plcKeyName = row.Cells["ColPCLKeyName"].Value?.ToString()?.Trim() ?? "";
                    string plcValue = row.Cells["ColConstant"].Value?.ToString()?.Trim() ?? "";

                    // 跳过空的模块名行
                    if (string.IsNullOrEmpty(plcModelName)) continue;

                    // 检查重复项
                    string fullPlcName = $"{plcModelName}.{plcKeyName}";
                    if (plcItems.Any(p => $"{p.PlcModuleName}.{p.PlcKeyName}".Equals(fullPlcName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageHelper.MessageOK($"PLC名称\"{fullPlcName}\"重复。", TType.Warn);
                        return null;
                    }

                    // 检查值是否为空
                    if (string.IsNullOrEmpty(plcValue))
                    {
                        hasEmptyConstant = true;
                    }

                    // 添加到列表
                    plcItems.Add(new PlcWriteItem
                    {
                        PlcModuleName = plcModelName,
                        PlcKeyName = plcKeyName,
                        PlcValue = plcValue
                    });
                }

                // 检查是否有空常数值
                if (hasEmptyConstant)
                {
                    MessageHelper.MessageOK("存在常数未填写，请补全所有常数值后再保存。", TType.Warn);
                    return null;
                }

                return plcItems;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "从UI收集PLC项目数据失败");
                throw;
            }
        }

        /// <summary>
        /// 获取选中的行索引
        /// </summary>
        /// <returns>选中的行索引，如果没有选中返回-1</returns>
        private int GetSelectedRowIndex()
        {
            // 优先使用整行选中
            if (DataGridViewPLCList.SelectedRows.Count > 0)
            {
                return DataGridViewPLCList.SelectedRows[0].Index;
            }
            // 如果没有整行选中，尝试使用当前单元格
            else if (DataGridViewPLCList.CurrentCell != null)
            {
                return DataGridViewPLCList.CurrentCell.RowIndex;
            }

            return -1;
        }

        /// <summary>
        /// 删除指定行的PLC项目
        /// </summary>
        /// <param name="rowIndex">要删除的行索引</param>
        /// <returns>删除是否成功</returns>
        private bool DeletePLCItem(int rowIndex)
        {
            try
            {
                // 验证行索引有效性
                if (rowIndex < 0 || rowIndex >= DataGridViewPLCList.Rows.Count ||
                    DataGridViewPLCList.Rows[rowIndex].IsNewRow)
                {
                    MessageHelper.MessageOK("选中的行无效！", TType.Warn);
                    return false;
                }

                // 获取当前步骤和参数
                var currentStep = GetCurrentStepSafely();
                if (currentStep == null)
                {
                    MessageHelper.MessageOK("当前步骤无效，无法删除。", TType.Warn);
                    return false;
                }

                var param = ParseCurrentParameter(currentStep);
                if (param?.Items == null)
                {
                    MessageHelper.MessageOK("参数数据异常，无法删除。", TType.Error);
                    return false;
                }

                // 获取要删除的项目信息
                var row = DataGridViewPLCList.Rows[rowIndex];
                string plcModelName = row.Cells["ColPCLModelName"].Value?.ToString() ?? "";
                string plcKeyName = row.Cells["ColPCLKeyName"].Value?.ToString() ?? "";
                string fullPlcName = $"{plcModelName}.{plcKeyName}";

                // 在集合中查找并移除对应项
                var toRemove = param.Items.FirstOrDefault(x =>
                    $"{x.PlcModuleName}.{x.PlcKeyName}".Equals(fullPlcName, StringComparison.OrdinalIgnoreCase));

                if (toRemove != null)
                {
                    param.Items.Remove(toRemove);
                    // 更新步骤参数
                    currentStep.StepParameter = JsonConvert.SerializeObject(param);
                    // 从DataGridView中删除行
                    DataGridViewPLCList.Rows.RemoveAt(rowIndex);

                    _logger?.LogInformation("成功删除PLC项目: {FullName}", fullPlcName);
                    return true;
                }
                else
                {
                    MessageHelper.MessageOK("集合中未找到对应数据。", TType.Warn);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除PLC项目时发生异常，行索引: {RowIndex}", rowIndex);
                throw;
            }
        }

        /// <summary>
        /// 解析当前步骤的参数对象
        /// </summary>
        /// <param name="currentStep">当前步骤</param>
        /// <returns>解析后的参数对象</returns>
        private Parameter_WritePLC ParseCurrentParameter(ChildModel currentStep)
        {
            try
            {
                if (currentStep.StepParameter is Parameter_WritePLC directParam)
                {
                    return directParam;
                }
                else if (currentStep.StepParameter != null)
                {
                    string jsonString = currentStep.StepParameter is string s
                        ? s : JsonConvert.SerializeObject(currentStep.StepParameter);

                    return JsonConvert.DeserializeObject<Parameter_WritePLC>(jsonString)
                           ?? new Parameter_WritePLC();
                }

                return new Parameter_WritePLC();
            }
            catch (JsonException ex)
            {
                _logger?.LogWarning(ex, "解析参数JSON失败，返回默认参数");
                return new Parameter_WritePLC();
            }
        }

        #endregion

        #region 接口实现
        public void PopulateControls(Form_WritePLC parameter)
        {
            Parameter = parameter;
        }

        void IParameterForm<Form_WritePLC>.SetDefaultValues()
        {
            SetDefaultValues();
        }

        public bool ValidateTypedParameters()
        {
            return ValidateParameters();
        }

        public Form_WritePLC CollectTypedParameters()
        {
            throw new NotImplementedException();
        }

        public Form_WritePLC ConvertParameter(object stepParameter)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 供BaseParameterForm使用

        /// <summary>
        /// 设置默认值（BaseParameterForm调用）
        /// </summary>
        protected override void SetDefaultValues()
        {
            try
            {
                // 清空现有数据
                DataGridViewPLCList.Rows.Clear();
                _currentParameter = new Parameter_WritePLC();

                _logger?.LogDebug("已设置默认值");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "设置默认值失败");
            }
        }

        #endregion
    }
}

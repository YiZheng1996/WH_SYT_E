using AntdUI;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
namespace MainUI.LogicalConfiguration.Forms
{
    public partial class Form_DefineVar : BaseParameterForm, IParameterForm<Parameter_DefineVar>
    {
        #region 构造函数
        private readonly GlobalVariableManager _variableManager;

        public Parameter_DefineVar Parameter
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// 使用依赖注入的构造函数
        /// </summary>
        public Form_DefineVar(GlobalVariableManager variableManager)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));

            InitializeComponent();
            LoadVariables();
        }

        #endregion

        #region 变量操作方法

        /// <summary>
        /// 加载变量列表
        /// </summary>
        private void LoadVariables()
        {
            try
            {
                _logger.LogDebug("开始加载变量列表");

                DataGridViewDefineVar.Rows.Clear();

                // 使用新的线程安全方法
                var variables = _workflowState.GetAllVariables();

                foreach (var variable in variables)
                {
                    DataGridViewDefineVar.Rows.Add(variable.VarName, variable.VarType, variable.VarText);
                }

                _logger.LogDebug("变量列表加载完成，共 {Count} 个变量", variables.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载变量列表时发生错误");
                MessageHelper.MessageOK("加载变量失败：" + ex.Message, TType.Error);
            }
        }

        /// <summary>
        /// 删除变量
        /// </summary>
        private void Clean_Click(object sender, EventArgs e)
        {
            int rowIndex = DataGridViewDefineVar.CurrentCell?.RowIndex ?? -1;
            if (rowIndex < 0 || DataGridViewDefineVar.Rows[rowIndex].IsNewRow)
            {
                MessageHelper.MessageOK("请选择要删除的变量！", TType.Warn);
                return;
            }

            if (MessageHelper.MessageYes(this, "确定要删除选中的变量吗？") == DialogResult.OK)
            {
                try
                {
                    string varName = DataGridViewDefineVar.Rows[rowIndex].Cells[0].Value?.ToString();

                    _logger.LogDebug("尝试删除变量: {VarName}", varName);

                    // 使用新的变量管理器方法
                    bool removed = _variableManager.RemoveVariable(varName);

                    if (removed)
                    {
                        LoadVariables(); // 重新加载列表
                        MessageHelper.MessageOK("删除成功！", TType.Success);
                        _logger.LogInformation("变量删除成功: {VarName}", varName);
                    }
                    else
                    {
                        MessageHelper.MessageOK("删除失败：变量不存在", TType.Warn);
                        _logger.LogWarning("尝试删除不存在的变量: {VarName}", varName);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "删除变量时发生错误");
                    MessageHelper.MessageOK($"删除失败：{ex.Message}", TType.Error);
                }
            }
        }

        /// <summary>
        /// 保存变量
        /// </summary>
        private async void Save_Click(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation("开始保存变量定义");

                // 先清空所有变量
                _workflowState.ClearVariables();

                var addedVariables = new List<string>();
                var errorMessages = new List<string>();

                // 遍历DataGridView所有有效行
                foreach (DataGridViewRow row in DataGridViewDefineVar.Rows)
                {
                    // 跳过新行或空行
                    if (row.IsNewRow) continue;

                    string varName = row.Cells["ColVarName"].Value?.ToString()?.Trim();
                    string varType = row.Cells["ColVarType"].Value?.ToString()?.Trim();
                    string varText = row.Cells["ColVarText"].Value?.ToString()?.Trim();

                    // 跳过变量名为空的行
                    if (string.IsNullOrEmpty(varName)) continue;

                    // 检查变量名是否重复
                    if (addedVariables.Contains(varName, StringComparer.OrdinalIgnoreCase))
                    {
                        errorMessages.Add($"变量名称\"{varName}\"重复");
                        continue;
                    }

                    try
                    {
                        // 创建新的增强型变量
                        var newVariable = new VarItem_Enhanced
                        {
                            VarName = varName,
                            VarType = varType ?? "string",
                            VarText = varText ?? "",
                            VarValue = "", // 默认值
                            LastUpdated = DateTime.Now,
                            IsAssignedByStep = false,
                            AssignedByStepIndex = -1,
                            AssignmentType = VariableAssignmentType.None
                        };

                        // 使用新的线程安全方法添加变量
                        _workflowState.AddVariable(newVariable);
                        addedVariables.Add(varName);

                        _logger.LogDebug("添加变量: {VarName}, 类型: {VarType}", varName, varType);
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add($"添加变量\"{varName}\"失败: {ex.Message}");
                        _logger.LogError(ex, "添加变量时发生错误: {VarName}", varName);
                    }
                }

                // 保存到JSON配置
                if (addedVariables.Count > 0)
                {
                    await JsonManager.UpdateConfigAsync(config =>
                    {
                        try
                        {
                            // 使用新的线程安全方法获取所有变量
                            config.Variable.Clear();
                            var allVariables = _workflowState.GetAllVariables();
                            config.Variable.AddRange(allVariables.Cast<VarItem>());

                            _logger.LogDebug("变量配置已保存到JSON文件");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "保存变量配置到JSON时发生错误");
                            throw;
                        }

                        return Task.CompletedTask;
                    });
                }

                // 显示结果
                LoadVariables(); // 重新加载显示

                if (errorMessages.Count > 0)
                {
                    var errorMsg = $"保存完成，但有以下错误:\n{string.Join("\n", errorMessages)}";
                    MessageHelper.MessageOK(errorMsg, TType.Warn);
                    _logger.LogWarning("变量保存完成但有错误: {Errors}", string.Join("; ", errorMessages));
                }
                else
                {
                    MessageHelper.MessageOK($"保存成功！共添加 {addedVariables.Count} 个变量", TType.Success);
                    _logger.LogInformation("变量保存成功，共添加 {Count} 个变量", addedVariables.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存变量时发生错误");
                MessageHelper.MessageOK($"保存失败：{ex.Message}", TType.Error);
            }
        }

        public void PopulateControls(Parameter_DefineVar parameter)
        {
            throw new NotImplementedException();
        }

        void IParameterForm<Parameter_DefineVar>.SetDefaultValues()
        {
            SetDefaultValues();
        }

        public bool ValidateTypedParameters()
        {
            throw new NotImplementedException();
        }

        public Parameter_DefineVar CollectTypedParameters()
        {
            throw new NotImplementedException();
        }

        public Parameter_DefineVar ConvertParameter(object stepParameter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}

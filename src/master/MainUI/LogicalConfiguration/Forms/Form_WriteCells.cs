using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 写入单元格参数配置表单 - 灵活版本
    /// 支持固定值、变量、表达式、系统属性等多种数据源
    /// </summary>
    public partial class Form_WriteCells : BaseParameterForm, IParameterForm<Parameter_WriteCells>
    {
        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private Parameter_WriteCells _currentParameter;
        private bool _isLoading = false;

        #endregion

        #region 属性

        public Parameter_WriteCells Parameter
        {
            get => GetCurrentParameters();
            set => LoadParameters(value);
        }

        #endregion

        #region 构造函数

        public Form_WriteCells()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                try
                {
                    _isLoading = true;
                    InitializeForm();
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Form_WriteCells 初始化失败");
                    MessageHelper.MessageOK($"初始化失败:{ex.Message}", TType.Error);
                }
                finally
                {
                    _isLoading = false;
                }
            }
        }

        public Form_WriteCells(GlobalVariableManager variableManager) : this()
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
        }

        #endregion

        #region 初始化方法

        private void InitializeForm()
        {
            _currentParameter = new Parameter_WriteCells();
            InitializeDataGridView();
            BindEvents();

            _logger?.LogInformation("Form_WriteCells 灵活版本初始化完成");
        }

        private void InitializeDataGridView()
        {
            try
            {
                DataGridViewDefineVar.AllowUserToAddRows = true;
                DataGridViewDefineVar.AllowUserToDeleteRows = true;
                DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DataGridViewDefineVar.MultiSelect = false;
                DataGridViewDefineVar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewDefineVar.EditMode = DataGridViewEditMode.EditOnEnter;

                // 更新ComboBox选项为新的数据源类型
                var typeColumn = DataGridViewDefineVar.Columns["ColVarType"] as DataGridViewComboBoxColumn;
                if (typeColumn != null)
                {
                    typeColumn.Items.Clear();
                    typeColumn.Items.AddRange([
                        "固定值",
                        "变量",
                        "表达式",
                        "系统属性"
                    ]);
                }

                _logger?.LogDebug("DataGridView 初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化DataGridView时发生错误");
                throw;
            }
        }

        private void BindEvents()
        {
            uiSymbolButton1.Click += BtnAdd_Click;
            BtnDelete.Click += BtnDelete_Click;
            DataGridViewDefineVar.CellValueChanged += DataGridViewDefineVar_CellValueChanged;
            DataGridViewDefineVar.CurrentCellDirtyStateChanged += DataGridViewDefineVar_CurrentCellDirtyStateChanged;
        }

        #endregion

        #region 参数操作方法

        private Parameter_WriteCells GetCurrentParameters()
        {
            try
            {
                var param = new Parameter_WriteCells
                {
                    Items = new List<WriteCellItem>()
                };

                foreach (DataGridViewRow row in DataGridViewDefineVar.Rows)
                {
                    if (row.IsNewRow) continue;

                    var cellAddress = row.Cells["ColVarName"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(cellAddress)) continue;

                    var sourceTypeStr = row.Cells["ColVarType"].Value?.ToString();
                    var content = row.Cells["ColVarText"].Value?.ToString();

                    var item = new WriteCellItem
                    {
                        CellAddress = cellAddress.Trim().ToUpper(),
                        SourceType = ParseSourceType(sourceTypeStr)
                    };

                    // 根据数据源类型设置对应的属性
                    switch (item.SourceType)
                    {
                        case CellsDataSourceType.FixedValue:
                            item.FixedValue = content;
                            break;
                        case CellsDataSourceType.Variable:
                            item.VariableName = content;
                            break;
                        case CellsDataSourceType.Expression:
                            item.Expression = content;
                            break;
                        case CellsDataSourceType.SystemProperty:
                            item.PropertyPath = content;
                            break;
                    }

                    param.Items.Add(item);
                }

                _logger?.LogDebug($"从界面获取参数，共 {param.Items.Count} 项");
                return param;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "获取当前参数时发生错误");
                return new Parameter_WriteCells();
            }
        }

        private void LoadParameters(Parameter_WriteCells param)
        {
            try
            {
                _isLoading = true;
                _currentParameter = param ?? new Parameter_WriteCells();

                DataGridViewDefineVar.Rows.Clear();

                if (_currentParameter.Items != null)
                {
                    foreach (var item in _currentParameter.Items)
                    {
                        var rowIndex = DataGridViewDefineVar.Rows.Add();
                        var row = DataGridViewDefineVar.Rows[rowIndex];

                        row.Cells["ColVarName"].Value = item.CellAddress ?? "";
                        row.Cells["ColVarType"].Value = GetSourceTypeDisplayName(item.SourceType);

                        // 根据数据源类型设置内容
                        var content = item.SourceType switch
                        {
                            CellsDataSourceType.FixedValue => item.FixedValue,
                            CellsDataSourceType.Variable => item.VariableName,
                            CellsDataSourceType.Expression => item.Expression,
                            CellsDataSourceType.SystemProperty => item.PropertyPath,
                            _ => string.Empty
                        };

                        row.Cells["ColVarText"].Value = content ?? "";
                    }
                }

                _logger?.LogInformation($"成功加载参数，包含 {_currentParameter.Items?.Count ?? 0} 项");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载参数到界面时发生错误");
                MessageHelper.MessageOK($"加载参数失败:{ex.Message}", TType.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        #endregion

        #region 事件处理器

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var rowIndex = DataGridViewDefineVar.Rows.Add();
                var row = DataGridViewDefineVar.Rows[rowIndex];

                row.Cells["ColVarName"].Value = "";
                row.Cells["ColVarType"].Value = "固定值";
                row.Cells["ColVarText"].Value = "";

                DataGridViewDefineVar.CurrentCell = row.Cells["ColVarName"];
                DataGridViewDefineVar.BeginEdit(true);

                _logger?.LogDebug("添加新的写入配置行");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "添加新行时发生错误");
                MessageHelper.MessageOK($"添加失败:{ex.Message}", TType.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewDefineVar.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要删除的行", TType.Warn);
                    return;
                }

                var result = MessageHelper.MessageYes("确定要删除选中的配置吗?", TType.Warn);
                if (result == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in DataGridViewDefineVar.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            DataGridViewDefineVar.Rows.Remove(row);
                        }
                    }

                    _logger?.LogDebug("删除选中的写入配置行");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除行时发生错误");
                MessageHelper.MessageOK($"删除失败:{ex.Message}", TType.Error);
            }
        }

        private void DataGridViewDefineVar_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading) return;
            if (e.RowIndex < 0) return;

            try
            {
                var row = DataGridViewDefineVar.Rows[e.RowIndex];
                var columnName = DataGridViewDefineVar.Columns[e.ColumnIndex].Name;

                // 当数据源类型改变时,更新提示信息
                if (columnName == "ColVarType")
                {
                    var sourceType = row.Cells["ColVarType"].Value?.ToString();
                    var contentCell = row.Cells["ColVarText"];

                    // 根据不同的数据源类型设置提示
                    contentCell.ReadOnly = false;
                    contentCell.Style.BackColor = Color.White;

                    // 设置占位提示文本(通过ToolTip或清空内容)
                    switch (sourceType)
                    {
                        case "固定值":
                            contentCell.Value = ""; // 用户直接输入
                            break;
                        case "变量":
                            contentCell.Value = ""; // 输入变量名
                            break;
                        case "表达式":
                            contentCell.Value = ""; // 输入表达式
                            break;
                        case "系统属性":
                            contentCell.Value = ""; // 输入属性路径
                            break;
                    }
                }

                _logger?.LogTrace($"单元格值改变: Row={e.RowIndex}, Col={columnName}");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "处理单元格值改变事件时发生错误");
            }
        }

        private void DataGridViewDefineVar_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            if (DataGridViewDefineVar.CurrentCell is DataGridViewComboBoxCell)
            {
                DataGridViewDefineVar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        #endregion

        #region 辅助方法

        private CellsDataSourceType ParseSourceType(string typeStr)
        {
            return typeStr switch
            {
                "固定值" => CellsDataSourceType.FixedValue,
                "变量" => CellsDataSourceType.Variable,
                "表达式" => CellsDataSourceType.Expression,
                "系统属性" => CellsDataSourceType.SystemProperty,
                _ => CellsDataSourceType.FixedValue
            };
        }

        private string GetSourceTypeDisplayName(CellsDataSourceType type)
        {
            return type switch
            {
                CellsDataSourceType.FixedValue => "固定值",
                CellsDataSourceType.Variable => "变量",
                CellsDataSourceType.Expression => "表达式",
                CellsDataSourceType.SystemProperty => "系统属性",
                _ => "固定值"
            };
        }

        private bool ValidateParameters()
        {
            try
            {
                var param = GetCurrentParameters();

                if (param.Items == null || param.Items.Count == 0)
                {
                    MessageHelper.MessageOK("请至少添加一个写入配置", TType.Warn);
                    return false;
                }

                // 验证每一项
                for (int i = 0; i < param.Items.Count; i++)
                {
                    var item = param.Items[i];

                    // 验证单元格地址
                    if (string.IsNullOrWhiteSpace(item.CellAddress))
                    {
                        MessageHelper.MessageOK($"第 {i + 1} 行:单元格地址不能为空", TType.Warn);
                        DataGridViewDefineVar.Rows[i].Selected = true;
                        return false;
                    }

                    // 验证单元格地址格式
                    if (!System.Text.RegularExpressions.Regex.IsMatch(item.CellAddress, @"^[A-Z]+\d+$",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {
                        MessageHelper.MessageOK($"第 {i + 1} 行:单元格地址格式不正确 (例如: A1, B2)", TType.Warn);
                        DataGridViewDefineVar.Rows[i].Selected = true;
                        return false;
                    }

                    // 验证数据源内容
                    var hasContent = item.SourceType switch
                    {
                        CellsDataSourceType.FixedValue => !string.IsNullOrWhiteSpace(item.FixedValue),
                        CellsDataSourceType.Variable => !string.IsNullOrWhiteSpace(item.VariableName),
                        CellsDataSourceType.Expression => !string.IsNullOrWhiteSpace(item.Expression),
                        CellsDataSourceType.SystemProperty => !string.IsNullOrWhiteSpace(item.PropertyPath),
                        _ => false
                    };

                    if (!hasContent)
                    {
                        var typeName = GetSourceTypeDisplayName(item.SourceType);
                        MessageHelper.MessageOK($"第 {i + 1} 行:{typeName}的内容不能为空", TType.Warn);
                        DataGridViewDefineVar.Rows[i].Selected = true;
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证参数时发生错误");
                MessageHelper.MessageOK($"参数验证失败:{ex.Message}", TType.Error);
                return false;
            }
        }

        #endregion

        #region 重写方法和接口实现

        protected void OnOKButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateParameters())
                {
                    return;
                }

                _currentParameter = GetCurrentParameters();

                _logger?.LogInformation($"保存写入单元格配置，共 {_currentParameter.Items.Count} 项");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存参数时发生错误");
                MessageHelper.MessageOK($"保存失败:{ex.Message}", TType.Error);
            }
        }

        protected void OnCancelButtonClick(object sender, EventArgs e)
        {
            _logger?.LogDebug("用户取消写入单元格配置");
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //  实现 IParameterForm 接口的所有方法
        
        public void PopulateControls(Parameter_WriteCells parameter)
        {
            LoadParameters(parameter);
        }

        void IParameterForm<Parameter_WriteCells>.SetDefaultValues()
        {
            // 设置默认值
            _currentParameter = new Parameter_WriteCells();
            DataGridViewDefineVar.Rows.Clear();
            _logger?.LogDebug("设置默认值");
        }

        public bool ValidateTypedParameters()
        {
            return ValidateParameters();
        }

        public Parameter_WriteCells CollectTypedParameters()
        {
            return GetCurrentParameters();
        }

        public Parameter_WriteCells ConvertParameter(object stepParameter)
        {
            try
            {
                // 如果已经是目标类型,直接返回
                if (stepParameter is Parameter_WriteCells typedParam)
                {
                    return typedParam;
                }

                // 尝试从JSON反序列化
                if (stepParameter != null)
                {
                    var jsonString = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                    return JsonConvert.DeserializeObject<Parameter_WriteCells>(jsonString);
                }

                return new Parameter_WriteCells();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "转换参数失败");
                return new Parameter_WriteCells();
            }
        }

        // 重写基类方法
        protected override void LoadParameterFromStep(object stepParameter)
        {
            try
            {
                var parameter = ConvertParameter(stepParameter);
                LoadParameters(parameter);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "从步骤加载参数失败");
            }
        }

        #endregion
    }
}
using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 写入单元格参数配置表单 - 方案2增强版
    /// 支持固定值、变量、表达式、系统属性等多种数据源
    /// 使用 ReportExpressionHelper 进行表达式验证和计算
    /// </summary>
    public partial class Form_WriteCells : BaseParameterForm, IParameterForm<Parameter_WriteCells>
    {
        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private readonly ReportExpressionHelper _expressionHelper;
        private Parameter_WriteCells _currentParameter;
        private bool _isLoading = false;

        // 数据源类型的使用说明
        private readonly Dictionary<string, string> _sourceTypeHints = new()
        {
            { "固定值",
                "💡 直接输入文本或数值\n" +
                "示例:\n" +
                "  • 测试报告\n" +
                "  • 123.45\n" +
                "  • 2025-01-01"
            },
            { "变量",
                "💡 输入全局变量名称\n" +
                "示例:\n" +
                "  • TestResult\n" +
                "  • Temperature\n" +
                "  • UserName\n" +
                "⚠️ 变量必须在工作流中已定义"
            },
            { "表达式",
                "💡 输入包含变量的表达式\n" +
                "格式: 使用 {变量名} 包裹变量\n" +
                "示例:\n" +
                "  • {Var1} + {Var2}\n" +
                "  • {Price} * 1.13\n" +
                "  • 结果:{Result}分\n" +
                "  • FORMAT(NOW(), \"yyyy-MM-dd\")\n" +
                "  • MAX({Val1}, {Val2}, {Val3})\n" +
                "⚠️ 支持函数: LEN, UPPER, LOWER, TRIM, NOW, FORMAT, ABS, MAX, MIN 等"
            },
            { "系统属性",
                "💡 输入系统对象属性路径\n" +
                "格式: 对象.属性.子属性\n" +
                "示例:\n" +
                "  • NewUsers.NewUserInfo.Username\n" +
                "  • VarHelper.TestViewModel.ModelName\n" +
                "  • DateTime.Now.ToString(\"yyyy-MM-dd\")\n" +
                "支持的根对象:\n" +
                "  • NewUsers - 用户信息\n" +
                "  • VarHelper - 系统变量\n" +
                "  • DateTime - 日期时间\n" +
                "  • BaseTest - 测试基类"
            }
        };

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

                    // 从全局服务提供者获取变量管理器
                    _variableManager = Program.ServiceProvider?.GetService<GlobalVariableManager>();

                    // 初始化表达式助手
                    if (_variableManager != null)
                    {
                        _expressionHelper = new ReportExpressionHelper(_variableManager, _logger);
                    }

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
            _expressionHelper = new ReportExpressionHelper(_variableManager, _logger);
        }

        #endregion

        #region 初始化方法

        private void InitializeForm()
        {
            _currentParameter = new Parameter_WriteCells();
            InitializeDataGridView();
            BindEvents();
            ShowQuickGuide(); // 显示快速指南

            _logger?.LogInformation("Form_WriteCells 方案2版本初始化完成");
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
                if (DataGridViewDefineVar.Columns["ColVarType"] is DataGridViewComboBoxColumn typeColumn)
                {
                    typeColumn.Items.Clear();
                    typeColumn.Items.AddRange(new object[]
                    {
                        "固定值",
                        "变量",
                        "表达式",
                        "系统属性"
                    });
                }

                // 为内容列添加提示文本
                if (DataGridViewDefineVar.Columns["ColVarText"] is DataGridViewTextBoxColumn textColumn)
                {
                    textColumn.HeaderText = "内容 (根据类型填写)";
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
            btnSave.Click += BtnSave_Click; ;
            btnAddRow.Click += BtnAdd_Click;
            BtnDelete.Click += BtnDelete_Click;
            btnBrowse.Click += BtnBrowse_Click;
            DataGridViewDefineVar.CellValueChanged += DataGridViewDefineVar_CellValueChanged;
            DataGridViewDefineVar.CurrentCellDirtyStateChanged += DataGridViewDefineVar_CurrentCellDirtyStateChanged;
            DataGridViewDefineVar.CellEnter += DataGridViewDefineVar_CellEnter; // 进入单元格时显示提示
            DataGridViewDefineVar.CellDoubleClick += DataGridViewDefineVar_CellDoubleClick; // 双击显示详细帮助
        }

        /// <summary>
        /// 显示快速指南
        /// </summary>
        private void ShowQuickGuide()
        {
            // 如果窗体上有提示标签,可以设置文本
            // 这里只记录日志
            _logger?.LogDebug("快速指南已准备");
        }

        #endregion

        #region 参数操作方法

        private Parameter_WriteCells GetCurrentParameters()
        {
            try
            {
                var param = new Parameter_WriteCells
                {
                    FilePath = txtFilePath.Text?.Trim(),
                    SheetName = txtSheetName.Text?.Trim(),
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

                _logger?.LogDebug($"从界面获取参数,共 {param.Items.Count} 项");
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

                txtFilePath.Text = _currentParameter.FilePath ?? "";
                txtSheetName.Text = _currentParameter.SheetName ?? "Sheet1";

                DataGridViewDefineVar.Rows.Clear();

                if (_currentParameter.Items != null)
                {
                    foreach (var item in _currentParameter.Items)
                    {
                        var rowIndex = DataGridViewDefineVar.Rows.Add();
                        var row = DataGridViewDefineVar.Rows[rowIndex];

                        row.Cells["ColVarName"].Value = item.CellAddress ?? "";
                        row.Cells["ColVarType"].Value = GetSourceTypeDisplayName(item.SourceType);

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

                _logger?.LogInformation($"成功加载参数,包含 {_currentParameter.Items?.Count ?? 0} 项");
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveParameters();
        }


        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel文件|*.xlsx;*.xls|所有文件|*.*";
                openFileDialog.Title = "选择Excel文件";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                    _logger?.LogDebug($"选择文件: {openFileDialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "选择文件时发生错误");
                MessageHelper.MessageOK($"选择文件失败:{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 进入单元格时显示提示
        /// </summary>
        private void DataGridViewDefineVar_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                var columnName = DataGridViewDefineVar.Columns[e.ColumnIndex].Name;
                var row = DataGridViewDefineVar.Rows[e.RowIndex];

                // 当进入"内容"列时,根据类型显示提示
                if (columnName == "ColVarText")
                {
                    var sourceType = row.Cells["ColVarType"].Value?.ToString() ?? "固定值";
                    if (_sourceTypeHints.TryGetValue(sourceType, out var hint))
                    {
                        var cell = row.Cells[e.ColumnIndex];
                        cell.ToolTipText = hint;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "显示单元格提示时发生错误");
            }
        }

        /// <summary>
        /// 双击单元格显示详细帮助
        /// </summary>
        private void DataGridViewDefineVar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                var columnName = DataGridViewDefineVar.Columns[e.ColumnIndex].Name;

                // 如果双击的是类型列,显示完整帮助
                if (columnName == "ColVarType")
                {
                    ShowDetailedHelp();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "显示帮助时发生错误");
            }
        }

        private void DataGridViewDefineVar_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0) return;

            try
            {
                var row = DataGridViewDefineVar.Rows[e.RowIndex];
                var columnName = DataGridViewDefineVar.Columns[e.ColumnIndex].Name;

                // 当数据源类型改变时,清空内容并设置提示
                if (columnName == "ColVarType")
                {
                    var sourceType = row.Cells["ColVarType"].Value?.ToString();
                    var contentCell = row.Cells["ColVarText"];

                    contentCell.ReadOnly = false;
                    contentCell.Style.BackColor = Color.White;
                    contentCell.Value = ""; // 清空内容

                    // 设置占位提示
                    if (_sourceTypeHints.TryGetValue(sourceType, out var hint))
                    {
                        contentCell.ToolTipText = hint;
                    }
                }

                // 当内容改变时,进行实时验证
                if (columnName == "ColVarText")
                {
                    ValidateCellContent(row);
                }
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

        #region 验证方法

        /// <summary>
        /// 验证单元格内容
        /// </summary>
        private void ValidateCellContent(DataGridViewRow row)
        {
            try
            {
                var sourceType = row.Cells["ColVarType"].Value?.ToString();
                var content = row.Cells["ColVarText"].Value?.ToString();
                var contentCell = row.Cells["ColVarText"];

                if (string.IsNullOrWhiteSpace(content))
                {
                    contentCell.Style.BackColor = Color.White;
                    return;
                }

                bool isValid = sourceType switch
                {
                    "固定值" => true,
                    "变量" => ValidateVariableName(content),
                    "表达式" => ValidateExpression(content),
                    "系统属性" => ValidateSystemProperty(content),
                    _ => true
                };

                // 根据验证结果设置背景色
                contentCell.Style.BackColor = isValid ? Color.LightGreen : Color.LightYellow;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证单元格内容时发生错误");
            }
        }

        /// <summary>
        /// 验证变量名
        /// </summary>
        private bool ValidateVariableName(string varName)
        {
            if (string.IsNullOrWhiteSpace(varName))
                return false;

            // 检查变量名是否存在
            if (_variableManager != null)
            {
                var variable = _variableManager.FindVariableByName(varName);
                return variable != null;
            }

            // 简单验证格式
            return System.Text.RegularExpressions.Regex.IsMatch(varName, @"^[a-zA-Z_][a-zA-Z0-9_]*$");
        }

        /// <summary>
        /// 验证表达式 - 使用 ReportExpressionHelper
        /// </summary>
        private bool ValidateExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return false;

            // 使用表达式助手验证
            if (_expressionHelper != null)
            {
                return _expressionHelper.ValidateExpression(expression);
            }

            // 简单验证:检查是否包含变量或运算符
            return expression.Contains('{') || expression.Contains('+') ||
                   expression.Contains('-') || expression.Contains('*') || expression.Contains('/');
        }

        /// <summary>
        /// 验证系统属性
        /// </summary>
        private bool ValidateSystemProperty(string propertyPath)
        {
            if (string.IsNullOrWhiteSpace(propertyPath))
                return false;

            var parts = propertyPath.Split('.');
            if (parts.Length < 2)
                return false;

            var validRoots = new[] { "NewUsers", "VarHelper", "DateTime", "BaseTest" };
            return validRoots.Contains(parts[0]);
        }

        #endregion

        #region 帮助方法

        /// <summary>
        /// 显示详细帮助对话框
        /// </summary>
        private void ShowDetailedHelp()
        {
            var helpText = new System.Text.StringBuilder();
            helpText.AppendLine("📖 数据源类型详细说明\n");
            helpText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            helpText.AppendLine("1️ 【固定值】");
            helpText.AppendLine("   直接输入要写入单元格的文本或数值");
            helpText.AppendLine("   📌 示例:");
            helpText.AppendLine("      • 测试报告");
            helpText.AppendLine("      • 123.45");
            helpText.AppendLine("      • 2025-01-01\n");

            helpText.AppendLine("2️ 【变量】");
            helpText.AppendLine("   从工作流全局变量中获取值");
            helpText.AppendLine("   📌 示例:");
            helpText.AppendLine("      • TestResult");
            helpText.AppendLine("      • Temperature");
            helpText.AppendLine("      • UserName\n");

            helpText.AppendLine("3️ 【表达式】 ⭐增强功能");
            helpText.AppendLine("   使用变量和函数进行计算或拼接");
            helpText.AppendLine("   📌 基础运算:");
            helpText.AppendLine("      • {Var1} + {Var2}");
            helpText.AppendLine("      • {Price} * 1.13");
            helpText.AppendLine("      • ({Max} + {Min}) / 2");
            helpText.AppendLine("   📌 字符串函数:");
            helpText.AppendLine("      • UPPER({Name})");
            helpText.AppendLine("      • LOWER({Text})");
            helpText.AppendLine("      • SUBSTRING({Text}, 0, 10)");
            helpText.AppendLine("   📌 日期函数:");
            helpText.AppendLine("      • FORMAT(NOW(), \"yyyy-MM-dd\")");
            helpText.AppendLine("      • FORMAT(NOW(), \"HH:mm:ss\")");
            helpText.AppendLine("   📌 数学函数:");
            helpText.AppendLine("      • ABS({Value})");
            helpText.AppendLine("      • MAX({Val1}, {Val2}, {Val3})");
            helpText.AppendLine("      • MIN({Val1}, {Val2}, {Val3})");
            helpText.AppendLine("   📌 逻辑运算:");
            helpText.AppendLine("      • {Score} >= 60");
            helpText.AppendLine("      • {Temp} > 20 && {Temp} < 30\n");

            helpText.AppendLine("4️⃣ 【系统属性】");
            helpText.AppendLine("   通过反射获取系统对象的属性值");
            helpText.AppendLine("   📌 用户信息:");
            helpText.AppendLine("      • NewUsers.NewUserInfo.Username");
            helpText.AppendLine("      • NewUsers.NewUserInfo.RoleName");
            helpText.AppendLine("   📌 系统变量:");
            helpText.AppendLine("      • VarHelper.TestViewModel.ModelName");
            helpText.AppendLine("      • VarHelper.TestViewModel.DrawingNo");
            helpText.AppendLine("   📌 日期时间:");
            helpText.AppendLine("      • DateTime.Now.ToString(\"yyyy-MM-dd\")");
            helpText.AppendLine("      • DateTime.Now.Year\n");

            helpText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            helpText.AppendLine("💡 使用技巧:");
            helpText.AppendLine("   • 内容单元格会显示ToolTip提示");
            helpText.AppendLine("   • 绿色背景 = 格式正确");
            helpText.AppendLine("   • 黄色背景 = 可能有问题");
            helpText.AppendLine("   • 表达式支持完整的函数库");
            helpText.AppendLine("   • 可以组合使用多种功能\n");

            helpText.AppendLine("🔍 更多函数请参考文档");

            MessageHelper.MessageOK(helpText.ToString(), TType.Info);
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

        #endregion

        #region 重写基类方法

        protected override object CollectParameters()
        {
            return GetCurrentParameters();
        }

        protected override bool ValidateParameters()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFilePath.Text))
                {
                    MessageHelper.MessageOK("请选择或输入Excel文件路径", TType.Warn);
                    txtFilePath.Focus();
                    return false;
                }

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

                    if (string.IsNullOrWhiteSpace(item.CellAddress))
                    {
                        MessageHelper.MessageOK($"第 {i + 1} 行:单元格地址不能为空", TType.Warn);
                        return false;
                    }

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

        protected override void SetDefaultValues()
        {
            _currentParameter = new Parameter_WriteCells();
            txtFilePath.Text = "";
            txtSheetName.Text = "Sheet1";
            DataGridViewDefineVar.Rows.Clear();
        }

        #endregion

        #region IParameterForm 接口实现

        public void PopulateControls(Parameter_WriteCells parameter)
        {
            LoadParameters(parameter);
        }

        void IParameterForm<Parameter_WriteCells>.SetDefaultValues()
        {
            SetDefaultValues();
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
                if (stepParameter is Parameter_WriteCells typedParam)
                {
                    return typedParam;
                }

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

        #endregion
    }
}
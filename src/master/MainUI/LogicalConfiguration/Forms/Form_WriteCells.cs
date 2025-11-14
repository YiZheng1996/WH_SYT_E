using AntdUI;
using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 写入单元格参数配置表单
    /// 新增功能:
    /// 1. 变量选择对话框
    /// 2. 表达式构建器
    /// 3. 实时预览面板
    /// 4. 智能提示
    /// 5. 操作按钮列
    /// </summary>
    public partial class Form_WriteCells : BaseParameterForm, IParameterForm<Parameter_WriteCells>
    {
        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private readonly ReportExpressionHelper _expressionHelper;
        private readonly ExpressionEngine _engine;
        private Parameter_WriteCells _currentParameter;
        private bool _isLoading = false;

        // 预览定时器 - 延迟更新预览,避免频繁计算
        private System.Windows.Forms.Timer _previewTimer;

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
                "💡 输入全局变量名称或点击'选择'按钮\n" +
                "示例:\n" +
                "  • TestResult\n" +
                "  • Temperature\n" +
                "  • UserName\n" +
                "⚠️ 变量必须在工作流中已定义"
            },
            { "表达式",
                "💡 输入包含变量的表达式或点击'构建'按钮\n" +
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
            set
            {
                _currentParameter = value ?? new Parameter_WriteCells();
                if (!DesignMode && !_isLoading && IsHandleCreated)
                {
                    LoadParametersToForm();
                }
            }
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
                    var plcManager = Program.ServiceProvider?.GetService<IPLCManager>();

                    // 初始化表达式助手和引擎
                    if (_variableManager != null)
                    {
                        _expressionHelper = new ReportExpressionHelper(_variableManager, _logger);
                        _engine = new ExpressionEngine(_variableManager, plcManager);
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
            var plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
            _expressionHelper = new ReportExpressionHelper(_variableManager, _logger);
            _engine = new ExpressionEngine(_variableManager, plcManager);
        }

        #endregion

        #region 初始化方法

        private void InitializeForm()
        {
            _currentParameter = new Parameter_WriteCells();
            InitializePreviewTimer();
            InitializeDataGridView();
            BindEvents();
            ShowQuickGuide();
            LoadParametersToForm();
            _logger?.LogInformation("Form_WriteCells 增强版初始化完成");
        }

        /// <summary>
        /// 从参数对象加载到表单
        /// </summary>
        private void LoadParametersToForm()
        {
            if (_currentParameter == null) return;

            _isLoading = true;

            try
            {
                _isLoading = true;
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
            }
            finally
            {
                _isLoading = false;
            }
        }

        /// <summary>
        /// 初始化预览定时器
        /// </summary>
        private void InitializePreviewTimer()
        {
            _previewTimer = new System.Windows.Forms.Timer
            {
                Interval = 500 // 500ms延迟
            };
            _previewTimer.Tick += PreviewTimer_Tick;
        }

        private void InitializeDataGridView()
        {
            try
            {
                DataGridViewDefineVar.AllowUserToAddRows = false;
                DataGridViewDefineVar.AllowUserToDeleteRows = true;
                DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DataGridViewDefineVar.MultiSelect = false;
                DataGridViewDefineVar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewDefineVar.EditMode = DataGridViewEditMode.EditOnEnter;

                // 更新ComboBox选项
                if (DataGridViewDefineVar.Columns["ColVarType"] is DataGridViewComboBoxColumn typeColumn)
                {
                    typeColumn.Items.Clear();
                    typeColumn.Items.AddRange(
                    [
                        "固定值",
                        "变量",
                        "表达式",
                        "系统属性"
                    ]);
                }

                // 为内容列添加提示文本
                if (DataGridViewDefineVar.Columns["ColVarText"] is DataGridViewTextBoxColumn textColumn)
                {
                    textColumn.HeaderText = "内容 (根据类型填写)";
                }

                // 添加操作按钮列
                AddOperationButtonColumn();

                _logger?.LogDebug("DataGridView 初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化DataGridView时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 添加操作按钮列
        /// </summary>
        private void AddOperationButtonColumn()
        {
            // 检查是否已存在操作列
            if (DataGridViewDefineVar.Columns.Contains("ColOperation"))
            {
                return;
            }

            var btnColumn = new DataGridViewButtonColumn
            {
                Name = "ColOperation",
                HeaderText = "操作",
                Text = "...",
                UseColumnTextForButtonValue = false, // 不使用列文本,我们会动态设置
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };

            DataGridViewDefineVar.Columns.Add(btnColumn);
        }

        private void BindEvents()
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnAddRow.Click += BtnAdd_Click;
            BtnDelete.Click += BtnDelete_Click;

            // DataGridView事件
            DataGridViewDefineVar.CellValueChanged += DataGridViewDefineVar_CellValueChanged;
            DataGridViewDefineVar.CurrentCellDirtyStateChanged += DataGridViewDefineVar_CurrentCellDirtyStateChanged;
            DataGridViewDefineVar.CellEnter += DataGridViewDefineVar_CellEnter;
            DataGridViewDefineVar.CellDoubleClick += DataGridViewDefineVar_CellDoubleClick;
            DataGridViewDefineVar.SelectionChanged += DataGridViewDefineVar_SelectionChanged;
            DataGridViewDefineVar.CellContentClick += DataGridViewDefineVar_CellContentClick;
            DataGridViewDefineVar.CellFormatting += DataGridViewDefineVar_CellFormatting;
        }

        private void ShowQuickGuide()
        {
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
                    SheetName = txtSheetName.Text?.Trim(),
                    Items = []
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

        #endregion

        #region DataGridView事件处理器

        private void DataGridViewDefineVar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0) return;

            try
            {
                var columnName = DataGridViewDefineVar.Columns[e.ColumnIndex].Name;

                // 动态设置操作按钮的文本
                if (columnName == "ColOperation")
                {
                    var row = DataGridViewDefineVar.Rows[e.RowIndex];
                    var sourceType = row.Cells["ColVarType"].Value?.ToString() ?? "固定值";

                    e.Value = sourceType switch
                    {
                        "变量" => "选择...",
                        "表达式" => "构建...",
                        "系统属性" => "浏览...",
                        _ => ""
                    };
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "格式化单元格时发生错误");
            }
        }

        private void DataGridViewDefineVar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                var columnName = DataGridViewDefineVar.Columns[e.ColumnIndex].Name;

                // 处理操作按钮点击
                if (columnName == "ColOperation")
                {
                    var row = DataGridViewDefineVar.Rows[e.RowIndex];
                    var sourceType = row.Cells["ColVarType"].Value?.ToString() ?? "固定值";

                    switch (sourceType)
                    {
                        case "变量":
                            ShowVariableSelector(row);
                            break;
                        case "表达式":
                            ShowExpressionBuilder(row);
                            break;
                        case "系统属性":
                            MessageHelper.MessageOK(this, "系统属性浏览功能开发中...", TType.Info);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "处理按钮点击时发生错误");
            }
        }

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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void DataGridViewDefineVar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                var columnName = DataGridViewDefineVar.Columns[e.ColumnIndex].Name;

                // ⭐ 添加这个判断: 如果是操作按钮列,直接返回
                if (columnName == "ColOperation")
                {
                    return; // 操作按钮已经在 CellContentClick 中处理
                }

                // 如果双击的是类型列,显示完整帮助
                if (columnName == "ColVarType")
                {
                    ShowDetailedHelp();
                }
                // 如果双击内容列,根据类型打开相应的辅助工具
                else if (columnName == "ColVarText")
                {
                    var row = DataGridViewDefineVar.Rows[e.RowIndex];
                    var sourceType = row.Cells["ColVarType"].Value?.ToString() ?? "固定值";

                    switch (sourceType)
                    {
                        case "变量":
                            ShowVariableSelector(row);
                            break;
                        case "表达式":
                            ShowExpressionBuilder(row);
                            break;
                    }
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
                var columnName = DataGridViewDefineVar.Columns[e.ColumnIndex].Name;

                // 当类型列改变时,清空内容列并更新预览
                if (columnName == "ColVarType")
                {
                    var row = DataGridViewDefineVar.Rows[e.RowIndex];
                    row.Cells["ColVarText"].Value = "";

                    // 刷新操作列按钮文本
                    DataGridViewDefineVar.InvalidateRow(e.RowIndex);
                }

                // 重启预览定时器
                RestartPreviewTimer();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "处理单元格值变化时发生错误");
            }
        }

        private void DataGridViewDefineVar_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            // 立即提交ComboBox的更改,以便CellValueChanged事件能立即触发
            if (DataGridViewDefineVar.CurrentCell is DataGridViewComboBoxCell)
            {
                DataGridViewDefineVar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridViewDefineVar_SelectionChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            // 行选择变化时更新预览
            RestartPreviewTimer();
        }

        #endregion

        #region 辅助对话框方法

        /// <summary>
        /// 显示变量选择对话框
        /// </summary>
        private void ShowVariableSelector(DataGridViewRow row)
        {
            try
            {
                if (_variableManager == null)
                {
                    MessageHelper.MessageOK(this, "变量管理器不可用", TType.Error);
                    return;
                }

                var selector = new VariableSelectionDialog(_variableManager)
                {
                    StartPosition = FormStartPosition.CenterParent
                };

                VarHelper.ShowDialogWithOverlay(this, selector);

                if (selector.DialogResult == DialogResult.OK && !string.IsNullOrEmpty(selector.SelectedVariableName))
                {
                    row.Cells["ColVarText"].Value = selector.SelectedVariableName;
                    _logger?.LogDebug($"选中变量: {selector.SelectedVariableName}");

                    // 更新预览
                    RestartPreviewTimer();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "显示变量选择对话框时发生错误");
                MessageHelper.MessageOK(this, $"打开变量选择器失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 显示表达式构建器对话框
        /// </summary>
        private void ShowExpressionBuilder(DataGridViewRow row)
        {
            try
            {
                if (_variableManager == null || _engine == null)
                {
                    MessageHelper.MessageOK(this, "表达式引擎不可用", TType.Error);
                    return;
                }

                var currentExpression = row.Cells["ColVarText"].Value?.ToString() ?? "";

                using var builder = new ExpressionBuilderDialog(_variableManager, _engine)
                {
                    InitialExpression = currentExpression,
                    StartPosition = FormStartPosition.CenterParent
                };

                VarHelper.ShowDialogWithOverlay(this, builder);

                if (builder.DialogResult == DialogResult.OK && !string.IsNullOrEmpty(builder.GeneratedExpression))
                {
                    row.Cells["ColVarText"].Value = builder.GeneratedExpression;
                    _logger?.LogDebug($"生成表达式: {builder.GeneratedExpression}");

                    // 更新预览
                    RestartPreviewTimer();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "显示表达式构建器时发生错误");
                MessageHelper.MessageOK(this, $"打开表达式构建器失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 预览功能

        /// <summary>
        /// 重启预览定时器
        /// </summary>
        private void RestartPreviewTimer()
        {
            _previewTimer?.Stop();
            _previewTimer?.Start();
        }

        /// <summary>
        /// 预览定时器触发事件
        /// </summary>
        private void PreviewTimer_Tick(object sender, EventArgs e)
        {
            _previewTimer.Stop();
            UpdatePreviewPanel();
        }

        /// <summary>
        /// 更新预览面板
        /// </summary>
        private void UpdatePreviewPanel()
        {
            try
            {
                _logger?.LogDebug("开始更新预览面板");

                // 直接查找预览面板中的控件
                if (Controls.Find("panelPreview", false).FirstOrDefault() is not System.Windows.Forms.Panel panelPreview)
                {
                    _logger?.LogWarning("未找到预览面板 panelPreview");
                    return;
                }

                // panelPreview 是预览标题,lblPreviewContent 是预览内容
                var lblPreviewTitle = panelPreview.Controls.Find("lblPreviewTitle", false).FirstOrDefault() as System.Windows.Forms.Label;
                var lblPreviewContent = panelPreview.Controls.Find("txtPreviewContent", false).FirstOrDefault() as RichTextBox;

                if (lblPreviewTitle == null || lblPreviewContent == null)
                {
                    _logger?.LogWarning($"预览面板控件未找到: lblPreviewTitle={0}, lblPreviewContent={1}",
                        lblPreviewTitle != null, lblPreviewContent != null);
                    return;
                }

                // 获取当前选中行
                if (DataGridViewDefineVar.SelectedRows.Count == 0)
                {
                    lblPreviewTitle.Text = "实时预览";
                    lblPreviewContent.Text = "请选择一行查看预览";
                    lblPreviewContent.ForeColor = Color.Gray;
                    _logger?.LogDebug("无选中行,显示默认提示");
                    return;
                }

                var row = DataGridViewDefineVar.SelectedRows[0];
                var cellAddress = row.Cells["ColVarName"].Value?.ToString() ?? "";
                var sourceType = row.Cells["ColVarType"].Value?.ToString() ?? "固定值";
                var content = row.Cells["ColVarText"].Value?.ToString() ?? "";

                _logger?.LogDebug($"选中行: 单元格={{0}}, 类型={{1}}, 内容={{2}}",
                    cellAddress, sourceType, content);

                lblPreviewTitle.Text = $"实时预览 - {sourceType}";

                if (string.IsNullOrWhiteSpace(content))
                {
                    lblPreviewContent.Text = $"单元格 {cellAddress}: (内容为空)";
                    lblPreviewContent.ForeColor = Color.Gray;
                    return;
                }

                // 根据数据源类型显示预览
                switch (sourceType)
                {
                    case "固定值":
                        lblPreviewContent.Text = $"单元格 {cellAddress} 将写入:\n{content}";
                        lblPreviewContent.ForeColor = Color.Black;
                        _logger?.LogDebug("预览固定值: {0}", content);
                        break;

                    case "变量":
                        PreviewVariable(content, lblPreviewContent);
                        break;

                    case "表达式":
                        PreviewExpression(content, lblPreviewContent);
                        break;

                    case "系统属性":
                        lblPreviewContent.Text = $"单元格 {cellAddress}:\n系统属性 {content}\n(运行时动态获取)";
                        lblPreviewContent.ForeColor = Color.DarkBlue;
                        break;

                    default:
                        lblPreviewContent.Text = $"单元格 {cellAddress}:\n{content}";
                        lblPreviewContent.ForeColor = Color.Black;
                        break;
                }

                _logger?.LogDebug("预览面板更新完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新预览面板时发生错误");

                // 可选: 在预览面板显示错误
                try
                {
                    var panelPreview = this.Controls.Find("uiGroupBox3", false).FirstOrDefault() as UIPanel;
                    if (panelPreview?.Controls.Find("lblPreviewContent", false).FirstOrDefault() is UILabel lblPreviewContent)
                    {
                        lblPreviewContent.Text = $"预览失败: {ex.Message}";
                        lblPreviewContent.ForeColor = Color.Red;
                    }
                }
                catch
                {
                    // 忽略二次错误
                }
            }
        }

        /// <summary>
        /// 预览变量值
        /// 配置时:允许变量未赋值,给出友好提示
        /// 运行时:期望变量已赋值
        /// </summary>
        private void PreviewVariable(string varName, RichTextBox lblPreview)
        {
            try
            {
                var variable = _variableManager?.FindVariableByName(varName);

                if (variable == null)
                {
                    // 配置时: 变量未定义是正常的
                    lblPreview.Text = $" 变量 '{varName}' 尚未赋值\n(运行时将从工作流变量中获取)";
                    lblPreview.ForeColor = Color.Gray;
                    return;
                }

                // 显示详细信息
                var valueStr = FormatVariableValue(variable);
                var typeInfo = $"[{variable.VarType}]";

                lblPreview.Text = $"✓ 变量 '{varName}' {typeInfo}\n当前值: {valueStr}";

                // 根据赋值状态设置颜色
                lblPreview.ForeColor = variable.IsAssignedByStep
                    ? Color.DarkGreen   // 已赋值
                    : Color.DarkOrange; // 声明但未赋值

                // 可选: 显示赋值来源
                if (variable.IsAssignedByStep && !string.IsNullOrWhiteSpace(variable.AssignedByStepInfo))
                {
                    lblPreview.Text += $"\n来源: {variable.AssignedByStepInfo}";
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "预览变量 {VarName} 失败", varName);
                lblPreview.Text = $"预览失败: {ex.Message}";
                lblPreview.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 格式化变量值显示
        /// </summary>
        private string FormatVariableValue(VarItem_Enhanced variable)
        {
            if (variable.VarValue == null)
                return "(null)";

            return variable.VarType?.ToLower() switch
            {
                "datetime" => variable.VarValue is DateTime dt
                    ? dt.ToString("yyyy-MM-dd HH:mm:ss")
                    : variable.VarValue.ToString(),
                "double" or "decimal" => string.Format("{0:F2}", variable.VarValue),
                "bool" => variable.VarValue.ToString(),
                _ => variable.VarValue.ToString()
            };
        }

        /// <summary>
        /// 预览表达式计算结果
        /// </summary>
        private void PreviewExpression(string expression, RichTextBox lblPreview)
        {
            try
            {
                if (_expressionHelper == null)
                {
                    lblPreview.Text = "表达式引擎不可用";
                    lblPreview.ForeColor = Color.Red;
                    return;
                }

                // 尝试计算表达式
                var result = _expressionHelper.EvaluateForReport(expression);

                if (result != null && !result.ToString().Contains("错误"))
                {
                    lblPreview.Text = $"表达式计算结果: {result}";
                    lblPreview.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblPreview.Text = $"表达式: {expression}\n{result}";
                    lblPreview.ForeColor = Color.OrangeRed;
                }
            }
            catch (Exception ex)
            {
                lblPreview.Text = $"表达式计算失败: {ex.Message}";
                lblPreview.ForeColor = Color.Red;
            }
        }

        #endregion

        #region 验证和保存

        protected override object CollectParameters()
        {
            return GetCurrentParameters();
        }

        protected override bool ValidateParameters()
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
                MessageHelper.MessageOK($"验证失败:{ex.Message}", TType.Error);
                return false;
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

        /// <summary>
        /// 显示详细帮助对话框
        /// </summary>
        private void ShowDetailedHelp()
        {
            var helpText = new System.Text.StringBuilder();
            helpText.AppendLine("数据源类型详细说明\n");
            helpText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            helpText.AppendLine("【固定值】");
            helpText.AppendLine(" 直接输入要写入单元格的文本或数值");
            helpText.AppendLine("   📌 示例:");
            helpText.AppendLine("      • 测试报告");
            helpText.AppendLine("      • 123.45");
            helpText.AppendLine("      • 2025-01-01\n");

            helpText.AppendLine("【变量】 ⭐可使用'选择'按钮");
            helpText.AppendLine("   从工作流全局变量中获取值");
            helpText.AppendLine("   示例:");
            helpText.AppendLine("   • TestResult");
            helpText.AppendLine("   • Temperature");
            helpText.AppendLine("   • UserName");
            helpText.AppendLine("   提示: 双击或点击'选择'按钮打开变量选择器\n");

            helpText.AppendLine("【表达式】 ⭐可使用'构建'按钮");
            helpText.AppendLine("   使用变量和函数进行计算或拼接");
            helpText.AppendLine("   基础运算:");
            helpText.AppendLine("   • {Var1} + {Var2}");
            helpText.AppendLine("   • {Price} * 1.13");
            helpText.AppendLine("   • ({Max} + {Min}) / 2");
            helpText.AppendLine("   字符串函数:");
            helpText.AppendLine("   • UPPER({Name})");
            helpText.AppendLine("   • LOWER({Text})");
            helpText.AppendLine("   • SUBSTRING({Text}, 0, 10)");
            helpText.AppendLine("   日期函数:");
            helpText.AppendLine("   • FORMAT(NOW(), \"yyyy-MM-dd\")");
            helpText.AppendLine("   • FORMAT(NOW(), \"HH:mm:ss\")");
            helpText.AppendLine("   提示: 双击或点击'构建'按钮打开表达式构建器\n");

            helpText.AppendLine("【系统属性】");
            helpText.AppendLine("   通过反射获取系统对象的属性值");
            helpText.AppendLine("   用户信息:");
            helpText.AppendLine("   • NewUsers.NewUserInfo.Username");
            helpText.AppendLine("   • NewUsers.NewUserInfo.RoleName");
            helpText.AppendLine("   系统变量:");
            helpText.AppendLine("   • VarHelper.TestViewModel.ModelName");
            helpText.AppendLine("   • VarHelper.TestViewModel.DrawingNo");
            helpText.AppendLine("   日期时间:");
            helpText.AppendLine("   • DateTime.Now.ToString(\"yyyy-MM-dd\")");
            helpText.AppendLine("   • DateTime.Now.Year\n");

            helpText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            helpText.AppendLine("新功能:");
            helpText.AppendLine("• 实时预览面板 - 查看当前值或计算结果");
            helpText.AppendLine("• 变量选择器 - 搜索和选择可用变量");
            helpText.AppendLine("• 表达式构建器 - 可视化构建复杂表达式");
            helpText.AppendLine("• 智能提示 - 输入时显示相关帮助\n");
            MessageHelper.MessageOK(helpText.ToString(), TType.Info);
        }

        #region 重写基类方法
        /// <summary>
        /// 从步骤参数加载 - 重写基类方法实现自动加载
        /// </summary>
        protected override void LoadParameterFromStep(object stepParameter)
        {
            try
            {
                Parameter_WriteCells loadedParameter = null;

                // 尝试直接类型转换
                if (stepParameter is Parameter_WriteCells directParam)
                {
                    loadedParameter = directParam;
                }
                // 尝试JSON反序列化
                else if (stepParameter != null)
                {
                    try
                    {
                        string jsonString = stepParameter is string s
                            ? s
                            : JsonConvert.SerializeObject(stepParameter);
                        loadedParameter = JsonConvert.DeserializeObject<Parameter_WriteCells>(jsonString);
                    }
                    catch (JsonException)
                    {
                        loadedParameter = null;
                    }
                }

                // 加载成功则更新参数并刷新界面
                if (loadedParameter != null)
                {
                    _currentParameter = loadedParameter;
                    LoadParametersToForm();  // 刷新界面控件
                }
                else
                {
                    SetDefaultValues();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "从步骤参数加载失败");
                SetDefaultValues();
            }
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            try
            {
                _currentParameter = new Parameter_WriteCells();
                LoadParametersToForm();
                _logger?.LogDebug("设置默认值");

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "设置默认值失败");
            }
        }
        #endregion

        #region IParameterForm<Parameter_WriteCells> 接口实现
        public void PopulateControls(Parameter_WriteCells parameter)
        {
            Parameter = parameter;
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

            if (stepParameter is Parameter_WriteCells param)
            {
                return param;
            }

            if (stepParameter is string json)
            {
                try
                {
                    return JsonConvert.DeserializeObject<Parameter_WriteCells>(json);
                }
                catch
                {
                    return new Parameter_WriteCells();
                }
            }

            return new Parameter_WriteCells();
        }
        #endregion

        #endregion

    }
}

using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 写入单元格参数配置表单 - 使用 ExpressionInputPanel
    /// 自动识别数据源类型（固定值/变量/表达式/系统属性）
    /// 参考 Form_WritePLC 的布局和配色
    /// </summary>
    public partial class Form_WriteCells : BaseParameterForm
    {
        #region 私有字段

        private Parameter_WriteCells _currentParameter;
        private bool _isLoading = false;

        /// <summary>
        /// 当前编辑行索引
        /// </summary>
        private int _editingRowIndex = -1;

        /// <summary>
        /// 临时文本框 - 用于 ExpressionInputPanel 附加
        /// </summary>
        private Sunny.UI.UITextBox _tempValueTextBox;

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
        #endregion

        #region 初始化方法

        private void InitializeForm()
        {
            _currentParameter = new Parameter_WriteCells();
            InitializeDataGridView();
            InitializeTempTextBox();
            BindEvents();
            _logger?.LogInformation("Form_WriteCells 初始化完成");
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
                _logger?.LogDebug("DataGridView 初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化 DataGridView 失败");
            }
        }

        private void InitializeTempTextBox()
        {
            try
            {
                _tempValueTextBox = new Sunny.UI.UITextBox
                {
                    Visible = false,
                    Size = new Size(1, 1),
                    Location = new Point(-100, -100)
                };

                ExpressionInputPanel.AttachTo(_tempValueTextBox, new InputPanelOptions
                {
                    Mode = InputMode.Expression,
                    EnabledModules = InputModules.All,
                    Title = "配置单元格内容",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true
                });

                // 监听文本变化 - 当面板确认后会更新_tempValueTextBox.Text
                _tempValueTextBox.TextChanged += (s, e) =>
                {
                    if (!_isLoading && _editingRowIndex >= 0 && _editingRowIndex < DataGridViewDefineVar.Rows.Count)
                    {
                        DataGridViewDefineVar.Rows[_editingRowIndex].Cells["ColVarText"].Value = _tempValueTextBox.Text;

                        // 转移焦点来强制刷新
                        var currentCell = DataGridViewDefineVar.CurrentCell;
                        DataGridViewDefineVar.CurrentCell = null;
                        DataGridViewDefineVar.CurrentCell = DataGridViewDefineVar.Rows[_editingRowIndex].Cells["ColCellAddress"];
                        DataGridViewDefineVar.Refresh();

                        _logger?.LogDebug("单元格内容已更新：行{Row}, 值={Value}", _editingRowIndex, _tempValueTextBox.Text);
                    }
                };

                this.Controls.Add(_tempValueTextBox);
                _logger?.LogDebug("临时文本框初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化临时文本框失败");
            }
        }

        private void BindEvents()
        {
            btnMoveUp.Click += BtnMoveUp_Click;
            btnMoveDown.Click += BtnMoveDown_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            DataGridViewDefineVar.CellClick += DataGridViewDefineVar_CellClick;
            DataGridViewDefineVar.RowsAdded += DataGridViewPLCList_RowsAdded;

            this.FormClosing += Form_WriteCells_FormClosing;
        }

        #endregion

        #region DataGridView 事件处理

        /// <summary>
        /// 行添加事件
        /// </summary>
        private void DataGridViewPLCList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (_isLoading) return;
            UpdateRowIndices();
        }

        /// <summary>
        /// 单元格点击事件 - 处理内容列的 ExpressionInputPanel
        /// </summary>
        private void DataGridViewDefineVar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == DataGridViewDefineVar.Columns["ColVarText"].Index)
            {
                _editingRowIndex = e.RowIndex;
                var currentValue = DataGridViewDefineVar.Rows[e.RowIndex].
                    Cells["ColVarText"].Value?.ToString() ?? "";

                _tempValueTextBox.Text = currentValue;

                // 临时移动_tempValueTextBox到正确位置以便面板正确定位
                var cellRect = DataGridViewDefineVar.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var dgvLocation = DataGridViewDefineVar.PointToScreen(new Point(cellRect.Left, cellRect.Bottom));
                var formLocation = this.PointToClient(dgvLocation);

                // 临时移动textbox到单元格位置
                _tempValueTextBox.Location = formLocation;
                _tempValueTextBox.Width = cellRect.Width;

                // 显示面板
                ExpressionInputPanel.Show(_tempValueTextBox, new InputPanelOptions
                {
                    Mode = InputMode.Expression,
                    EnabledModules = InputModules.All,  // 所有模块
                    Title = "配置单元格内容",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true,
                    InitialExpression = currentValue  // 传递当前值
                });


                // 显示后恢复隐藏位置
                _tempValueTextBox.Location = new Point(-100, -100);
                _tempValueTextBox.Width = 1;
            }
        }

        /// <summary>
        /// 更新所有行的序号
        /// </summary>
        private void UpdateRowIndices()
        {
            int index = 1;
            foreach (DataGridViewRow row in DataGridViewDefineVar.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["ColIndex"].Value = index++;
                }
            }
        }

        /// <summary>
        /// 上移按钮点击事件
        /// </summary>
        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewDefineVar.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的行！", TType.Warn);
                    return;
                }

                var selectedRow = DataGridViewDefineVar.SelectedRows[0];
                var rowIndex = selectedRow.Index;

                if (rowIndex > 0)
                {
                    DataGridViewDefineVar.Rows.RemoveAt(rowIndex);
                    DataGridViewDefineVar.Rows.Insert(rowIndex - 1, selectedRow);
                    DataGridViewDefineVar.ClearSelection();
                    selectedRow.Selected = true;
                    UpdateRowIndices();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "上移行失败");
                MessageHelper.MessageOK($"上移失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 下移按钮点击事件
        /// </summary>
        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewDefineVar.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的行！", TType.Warn);
                    return;
                }

                var selectedRow = DataGridViewDefineVar.SelectedRows[0];
                var rowIndex = selectedRow.Index;

                if (rowIndex < DataGridViewDefineVar.Rows.Count - 1)
                {
                    DataGridViewDefineVar.Rows.RemoveAt(rowIndex);
                    DataGridViewDefineVar.Rows.Insert(rowIndex + 1, selectedRow);
                    DataGridViewDefineVar.ClearSelection();
                    selectedRow.Selected = true;
                    UpdateRowIndices();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "下移行失败");
                MessageHelper.MessageOK($"下移失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 按钮事件

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var rowIndex = DataGridViewDefineVar.Rows.Add();
                var row = DataGridViewDefineVar.Rows[rowIndex];
                row.Cells["ColIndex"].Value = DataGridViewDefineVar.Rows.Count;
                row.Cells["ColCellAddress"].Value = "";
                row.Cells["ColVarText"].Value = "";
                DataGridViewDefineVar.CurrentCell = row.Cells["ColCellAddress"];
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
                    UpdateRowIndices();
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
            DialogResult = DialogResult.Cancel;
            Close();
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

                    var cellAddress = row.Cells["ColCellAddress"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(cellAddress)) continue;

                    var content = row.Cells["ColVarText"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(content)) continue;

                    // 自动识别数据源类型
                    var sourceType = DetectSourceType(content);

                    var item = new WriteCellItem
                    {
                        CellAddress = cellAddress.Trim().ToUpper(),
                        SourceType = sourceType
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
                txtSheetName.Text = _currentParameter.SheetName ?? "Sheet1";

                DataGridViewDefineVar.Rows.Clear();

                if (_currentParameter.Items != null)
                {
                    foreach (var item in _currentParameter.Items)
                    {
                        var rowIndex = DataGridViewDefineVar.Rows.Add();
                        var row = DataGridViewDefineVar.Rows[rowIndex];

                        // 设置序号列
                        row.Cells["ColIndex"].Value = rowIndex + 1;
                        row.Cells["ColCellAddress"].Value = item.CellAddress ?? "";

                        // 根据数据源类型获取对应的内容
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

                // 加载完成后更新所有序号
                UpdateRowIndices();

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

        #region 数据源类型自动识别

        /// <summary>
        /// 自动检测数据源类型
        /// 支持: 固定值、变量、表达式、系统属性、PLC引用
        /// </summary>
        private CellsDataSourceType DetectSourceType(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return CellsDataSourceType.FixedValue;

            // 1. 检查是否为PLC引用: {PLC.模块.地址} 或 {PLC.xxx.yyy}
            // 注意: 支持中文模块名和地址
            if (Regex.IsMatch(content, @"^\s*\{PLC\.[^\}]+\}\s*$", RegexOptions.IgnoreCase))
            {
                return CellsDataSourceType.Expression; // 作为表达式处理
            }

            // 2. 检查是否为表达式: 包含 {变量}、运算符或函数
            if (Regex.IsMatch(content, @"\{[\w\u4e00-\u9fa5.]+\}") ||
                Regex.IsMatch(content, @"[+\-*/()><=!&|]"))
            {
                return CellsDataSourceType.Expression;
            }

            // 3. 检查是否为系统属性路径（包含多个点号）
            if (Regex.IsMatch(content, @"^[A-Za-z_][\w\u4e00-\u9fa5]*(\.[A-Za-z_][\w\u4e00-\u9fa5]*)+"))
            {
                var systemRoots = new[] { "NewUsers", "VarHelper", "DateTime", "BaseTest", "Environment" };
                if (systemRoots.Any(root => content.StartsWith(root + ".")))
                {
                    return CellsDataSourceType.SystemProperty;
                }
            }

            // 4. 检查是否为单个变量名
            if (Regex.IsMatch(content, @"^[\w\u4e00-\u9fa5]+$") &&
                !Regex.IsMatch(content, @"^\d+(\.\d+)?$"))
            {
                if (_globalVariable?.FindVariableByName(content) != null)
                {
                    return CellsDataSourceType.Variable;
                }
            }

            // 5. 默认为固定值
            return CellsDataSourceType.FixedValue;
        }

        #endregion

        #region 窗体事件

        private void Form_WriteCells_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ExpressionInputPanel.CloseActivePanel();
                _logger?.LogDebug("写入单元格配置窗体正在关闭");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "窗体关闭事件处理失败");
            }
        }

        #endregion

    }
}
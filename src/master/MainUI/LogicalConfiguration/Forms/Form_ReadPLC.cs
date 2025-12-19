using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 读取PLC参数配置表单
    /// 只修改逻辑，保持界面不变
    /// </summary>
    public partial class Form_ReadPLC : BaseParameterForm
    {
        #region 私有字段
        private Parameter_ReadPLC _currentParameter;
        private bool _isLoading = false;
        private int _editingRowIndex = -1;
        private Sunny.UI.UITextBox _tempVarTextBox;

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象属性
        /// </summary>
        public Parameter_ReadPLC Parameter
        {
            get => GetCurrentParameters();
            set => LoadParameters(value);
        }

        #endregion

        #region 构造函数

        public Form_ReadPLC(ILogger<Form_ReadPLC> logger) : base()
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
                    _logger?.LogError(ex, "Form_ReadPLC 初始化失败");
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
            _currentParameter = new Parameter_ReadPLC();
            InitializeDataGridView();
            InitializeTempVarTextBox();
            BindEvents();
            LoadPLCModules();

            // 加载当前步骤参数
            LoadParametersFromStep();

            UpdatePreview();

            _logger?.LogInformation("Form_ReadPLC 初始化完成");
        }

        private void InitializeDataGridView()
        {
            // DataGridView 已在 Designer 中定义，这里只设置基本属性
            DataGridViewPLCList.AllowUserToAddRows = false;
            DataGridViewPLCList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewPLCList.MultiSelect = false;
        }

        private void InitializeTempVarTextBox()
        {
            // 初始化临时文本框用于 ExpressionInputPanel
            _tempVarTextBox = new Sunny.UI.UITextBox
            {
                Name = "tempVarTextBox",
                Location = new Point(-100, -100),
                Width = 1,
                Visible = true
            };

            _tempVarTextBox.TextChanged += (s, e) =>
            {
                if (!_isLoading && _editingRowIndex >= 0 && _editingRowIndex < DataGridViewPLCList.Rows.Count)
                {
                    DataGridViewPLCList.Rows[_editingRowIndex].Cells["ColTargetVar"].Value = _tempVarTextBox.Text;
                    // 转移焦点来强制刷新
                    var currentCell = DataGridViewPLCList.CurrentCell;
                    DataGridViewPLCList.CurrentCell = null;
                    DataGridViewPLCList.CurrentCell = DataGridViewPLCList.Rows[_editingRowIndex].Cells["ColPLCAddress"];
                    DataGridViewPLCList.Refresh();
                    _logger?.LogDebug("单元格内容已更新：行{Row}, 值={Value}", _editingRowIndex, _tempVarTextBox.Text);
                }
            };

            this.Controls.Add(_tempVarTextBox);
        }

        private void BindEvents()
        {
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnMoveUp.Click += BtnMoveUp_Click;
            btnMoveDown.Click += BtnMoveDown_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            DataGridViewPLCList.CellClick += DataGridViewPLCList_CellClick;
            DataGridViewPLCList.CellValueChanged += DataGridViewPLCList_CellValueChanged;
            DataGridViewPLCList.RowsAdded += (s, e) => UpdateRowIndices();
            DataGridViewPLCList.RowsRemoved += (s, e) => UpdateRowIndices();
        }

        /// <summary>
        /// 加载PLC模块和点位地址
        /// </summary>
        private async void LoadPLCModules()
        {
            try
            {
                var moduleTags = await _plcManager.GetModuleTagsAsync();

                // 填充模块下拉框
                if (DataGridViewPLCList.Columns["ColPlcModule"] is DataGridViewComboBoxColumn moduleColumn)
                {
                    moduleColumn.Items.Clear();
                    foreach (var moduleName in moduleTags.Keys)
                    {
                        moduleColumn.Items.Add(moduleName);
                    }
                }
                _logger?.LogDebug("已加载 {Count} 个PLC模块", moduleTags.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载PLC模块失败");
            }
        }

        #endregion

        #region 核心方法 - 参考 Form_WriteCells 模式

        /// <summary>
        /// 从界面收集参数 - 参考 Form_WriteCells.GetCurrentParameters()
        /// </summary>
        private Parameter_ReadPLC GetCurrentParameters()
        {
            try
            {
                var param = new Parameter_ReadPLC
                {
                    Items = new List<PlcReadItem>()
                };

                foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
                {
                    if (row.IsNewRow) continue;

                    string moduleName = row.Cells["ColPlcModule"].Value?.ToString() ?? "";
                    string address = row.Cells["ColPlcAddress"].Value?.ToString() ?? "";
                    string targetVar = row.Cells["ColTargetVar"].Value?.ToString() ?? "";

                    // 只添加有效的配置项
                    if (!string.IsNullOrEmpty(moduleName) && !string.IsNullOrEmpty(address))
                    {
                        param.Items.Add(new PlcReadItem
                        {
                            PlcModuleName = moduleName,
                            PlcKeyName = address,
                            TargetVarName = targetVar
                        });
                    }
                }

                _logger?.LogDebug("从界面获取参数，共 {Count} 项", param.Items.Count);
                return param;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "获取当前参数时发生错误");
                return new Parameter_ReadPLC();
            }
        }

        /// <summary>
        /// 加载参数到界面 - 参考 Form_WriteCells.LoadParameters()
        /// </summary>
        private void LoadParameters(Parameter_ReadPLC param)
        {
            try
            {
                _isLoading = true;
                _currentParameter = param ?? new Parameter_ReadPLC();

                DataGridViewPLCList.Rows.Clear();

                if (_currentParameter.Items != null && _currentParameter.Items.Count > 0)
                {
                    foreach (var item in _currentParameter.Items)
                    {
                        int rowIndex = DataGridViewPLCList.Rows.Add();
                        var row = DataGridViewPLCList.Rows[rowIndex];

                        row.Cells["ColIndex"].Value = (rowIndex + 1).ToString();
                        row.Cells["ColPlcModule"].Value = item.PlcModuleName ?? "";
                        row.Cells["ColPlcAddress"].Value = item.PlcKeyName ?? "";
                        row.Cells["ColTargetVar"].Value = item.TargetVarName ?? "";

                        // 异步加载该模块的地址列表
                        if (!string.IsNullOrEmpty(item.PlcModuleName))
                        {
                            LoadPLCAddressesForRow(item.PlcModuleName, rowIndex);
                        }
                    }
                }

                UpdateRowIndices();
                _logger?.LogInformation("成功加载参数，包含 {Count} 项", _currentParameter.Items?.Count ?? 0);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载参数时发生错误");
            }
            finally
            {
                _isLoading = false;
            }
        }

        /// <summary>
        /// 从当前步骤加载参数
        /// </summary>
        private void LoadParametersFromStep()
        {
            try
            {
                var currentStep = GetCurrentStepSafely();
                if (currentStep?.StepParameter == null)
                {
                    _logger?.LogDebug("当前步骤无参数，使用默认值");
                    LoadParameters(new Parameter_ReadPLC());
                    return;
                }

                Parameter_ReadPLC loadedParameter = null;

                // 尝试直接类型转换
                if (currentStep.StepParameter is Parameter_ReadPLC param)
                {
                    loadedParameter = param;
                }
                // 尝试 JSON 反序列化
                else
                {
                    try
                    {
                        string jsonString = currentStep.StepParameter is string s
                            ? s
                            : JsonConvert.SerializeObject(currentStep.StepParameter);
                        loadedParameter = JsonConvert.DeserializeObject<Parameter_ReadPLC>(jsonString);
                    }
                    catch (JsonException ex)
                    {
                        _logger?.LogWarning(ex, "JSON反序列化失败");
                        loadedParameter = null;
                    }
                }

                // 加载成功则更新参数并刷新界面
                if (loadedParameter != null)
                {
                    LoadParameters(loadedParameter);
                }
                else
                {
                    _logger?.LogWarning("参数加载失败，使用默认值");
                    LoadParameters(new Parameter_ReadPLC());
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载步骤参数失败");
                LoadParameters(new Parameter_ReadPLC());
            }
        }

        private async void LoadPLCAddressesForRow(string moduleName, int rowIndex)
        {
            try
            {
                if (string.IsNullOrEmpty(moduleName)) return;

                var addresses = await _plcManager.GetModuleTagsAsync(moduleName);

                if (rowIndex >= 0 && rowIndex < DataGridViewPLCList.Rows.Count)
                {
                    var row = DataGridViewPLCList.Rows[rowIndex];
                    if (row.Cells["ColPlcAddress"] is DataGridViewComboBoxCell addressCell)
                    {
                        // 保存当前值
                        var currentValue = addressCell.Value;

                        // 清空并填充
                        addressCell.Items.Clear();
                        foreach (var address in addresses)
                        {
                            addressCell.Items.Add(address);
                        }

                        // 恢复值（如果存在于列表中）
                        if (currentValue != null && addressCell.Items.Contains(currentValue))
                        {
                            addressCell.Value = currentValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载PLC地址失败");
            }
        }

        #endregion

        #region BaseParameterForm 重写方法

        /// <summary>
        /// 加载参数到表单 - BaseParameterForm 要求实现
        /// </summary>
        protected override void LoadParameterToForm()
        {
            LoadParametersFromStep();
        }

        /// <summary>
        /// 保存表单到参数
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                // 1. 从界面收集参数
                _currentParameter = GetCurrentParameters();

                // 2. 调用基类方法保存参数
                SetParameterValue(_currentParameter);

                _logger?.LogDebug("保存了 {Count} 个PLC读取项", _currentParameter.Items.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存参数失败");
                throw;
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        protected override bool ValidateInput()
        {
            if (DataGridViewPLCList.Rows.Count == 0)
            {
                MessageHelper.MessageOK("请至少添加一个PLC读取项！", TType.Warn);
                return false;
            }

            for (int i = 0; i < DataGridViewPLCList.Rows.Count; i++)
            {
                var row = DataGridViewPLCList.Rows[i];
                if (row.IsNewRow) continue;

                string moduleName = row.Cells["ColPlcModule"].Value?.ToString() ?? "";
                string address = row.Cells["ColPlcAddress"].Value?.ToString() ?? "";
                string targetVar = row.Cells["ColTargetVar"].Value?.ToString() ?? "";

                if (string.IsNullOrEmpty(moduleName))
                {
                    MessageHelper.MessageOK(this, $"第 {i + 1} 行：PLC模块不能为空！", TType.Warn);
                    return false;
                }

                if (string.IsNullOrEmpty(address))
                {
                    MessageHelper.MessageOK(this, $"第 {i + 1} 行：点位地址不能为空！", TType.Warn);
                    return false;
                }

                if (string.IsNullOrEmpty(targetVar))
                {
                    MessageHelper.MessageOK(this, $"第 {i + 1} 行：目标变量不能为空！", TType.Warn);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 事件处理

        private void DataGridViewPLCList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 点击目标变量列时，打开 ExpressionInputPanel
            if (e.ColumnIndex == DataGridViewPLCList.Columns["ColTargetVar"].Index)
            {
                _editingRowIndex = e.RowIndex;
                var currentValue = DataGridViewPLCList.Rows[e.RowIndex].Cells["ColTargetVar"].Value?.ToString() ?? "";

                _tempVarTextBox.Text = currentValue;

                // 计算textbox位置
                var cellRect = DataGridViewPLCList.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var dgvLocation = DataGridViewPLCList.PointToScreen(new Point(cellRect.Left, cellRect.Bottom));
                var formLocation = this.PointToClient(dgvLocation);

                _tempVarTextBox.Location = formLocation;
                _tempVarTextBox.Width = cellRect.Width;

                // 显示面板
                ExpressionInputPanel.Show(_tempVarTextBox, new InputPanelOptions
                {
                    Mode = InputMode.Expression,
                    EnabledModules = InputModules.Variable,
                    Title = "选择目标变量",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true,
                    InitialExpression = currentValue
                });

                // 恢复隐藏位置
                _tempVarTextBox.Location = new Point(-100, -100);
                _tempVarTextBox.Width = 1;
            }
        }

        private async void DataGridViewPLCList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0) return;

            // 当 PLC 模块变化时，加载对应的点位地址
            if (e.ColumnIndex == DataGridViewPLCList.Columns["ColPlcModule"].Index)
            {
                string moduleName = DataGridViewPLCList.Rows[e.RowIndex].Cells["ColPlcModule"].Value?.ToString();
                if (!string.IsNullOrEmpty(moduleName))
                {
                    await LoadPLCAddressesForRowAsync(e.RowIndex, moduleName);
                }
            }
        }

        private async Task LoadPLCAddressesForRowAsync(int rowIndex, string moduleName)
        {
            try
            {
                var addresses = await _plcManager.GetModuleTagsAsync();
                if (addresses == null || addresses.Count == 0)
                {
                    Logger?.LogWarning("模块 {ModuleName} 没有可用地址", moduleName);
                    return;
                }

                if (DataGridViewPLCList.Rows[rowIndex].Cells["ColPlcAddress"] is
                    DataGridViewComboBoxCell addressCell)
                {
                    // 保存当前值
                    var currentValue = addressCell.Value;

                    // 清空并填充 Items
                    addressCell.Items.Clear();
                    if (addresses.TryGetValue(moduleName, out List<string> addresse))
                    {
                        foreach (var item in addresse)
                        {
                            addressCell.Items.Add(item);
                        }
                    }

                    // 恢复或清空值
                    if (currentValue != null && addressCell.Items.Contains(currentValue))
                    {
                        addressCell.Value = currentValue;
                    }
                    else
                    {
                        addressCell.Value = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载地址列表失败");
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int rowIndex = DataGridViewPLCList.Rows.Add();
            UpdateRowIndices();
            _logger?.LogDebug("添加新的PLC读取项，行索引: {RowIndex}", rowIndex);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewPLCList.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK(this, "请选择要删除的行！", TType.Warn);
                    return;
                }

                DataGridViewPLCList.Rows.RemoveAt(DataGridViewPLCList.SelectedRows[0].Index);
                UpdateRowIndices();
                UpdatePreview();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除行失败");
            }
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewPLCList.SelectedRows.Count == 0) return;

                int rowIndex = DataGridViewPLCList.SelectedRows[0].Index;
                if (rowIndex <= 0) return;

                SwapRows(rowIndex, rowIndex - 1);
                DataGridViewPLCList.ClearSelection();
                DataGridViewPLCList.Rows[rowIndex - 1].Selected = true;
                UpdateRowIndices();
                UpdatePreview();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "上移行失败");
            }
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewPLCList.SelectedRows.Count == 0) return;

                int rowIndex = DataGridViewPLCList.SelectedRows[0].Index;
                if (rowIndex >= DataGridViewPLCList.Rows.Count - 1) return;

                SwapRows(rowIndex, rowIndex + 1);
                DataGridViewPLCList.ClearSelection();
                DataGridViewPLCList.Rows[rowIndex + 1].Selected = true;
                UpdateRowIndices();
                UpdatePreview();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "下移行失败");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                SaveParameters();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存失败");
                MessageHelper.MessageOK($"保存失败: {ex.Message}", TType.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region 辅助方法

        private void UpdateRowIndices()
        {
            for (int i = 0; i < DataGridViewPLCList.Rows.Count; i++)
            {
                if (!DataGridViewPLCList.Rows[i].IsNewRow)
                {
                    DataGridViewPLCList.Rows[i].Cells["ColIndex"].Value = (i + 1).ToString();
                }
            }
        }

        private void SwapRows(int index1, int index2)
        {
            if (index1 < 0 || index2 < 0 || index1 >= DataGridViewPLCList.Rows.Count || index2 >= DataGridViewPLCList.Rows.Count)
                return;

            var row1 = DataGridViewPLCList.Rows[index1];
            var row2 = DataGridViewPLCList.Rows[index2];

            // 交换除了序号列之外的所有列
            for (int i = 1; i < DataGridViewPLCList.Columns.Count; i++)
            {
                var temp = row1.Cells[i].Value;
                row1.Cells[i].Value = row2.Cells[i].Value;
                row2.Cells[i].Value = temp;
            }
        }

        private void UpdatePreview()
        {
            try
            {
                var preview = new System.Text.StringBuilder();
                preview.AppendLine($"共配置 {DataGridViewPLCList.Rows.Count} 个PLC读取项：\n");

                for (int i = 0; i < DataGridViewPLCList.Rows.Count; i++)
                {
                    var row = DataGridViewPLCList.Rows[i];
                    if (row.IsNewRow) continue;

                    string moduleName = row.Cells["ColPlcModule"].Value?.ToString() ?? "";
                    string address = row.Cells["ColPlcAddress"].Value?.ToString() ?? "";
                    string targetVar = row.Cells["ColTargetVar"].Value?.ToString() ?? "";

                    preview.AppendLine($"[{i + 1}] {moduleName}.{address} → {targetVar}");
                }

                _logger?.LogDebug("预览更新完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新预览失败");
            }
        }

        #endregion
    }
}
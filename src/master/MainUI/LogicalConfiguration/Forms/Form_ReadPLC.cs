using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 读取PLC参数配置表单 - 使用 ExpressionInputPanel
    /// </summary>
    public partial class Form_ReadPLC : BaseParameterForm
    {
        #region 私有字段
        private Parameter_ReadPLC _currentParameter;
        private bool _isLoading = false;
        private int _editingRowIndex = -1;
        private Sunny.UI.UITextBox _tempVarTextBox;

        #endregion

        #region 构造函数

        public Form_ReadPLC(ILogger<Form_ReadPLC> logger)
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
            LoadParametersFromStep();
            UpdatePreview();

            _logger?.LogInformation("Form_ReadPLC 增强版初始化完成");
        }

        private void InitializeDataGridView()
        {
            DataGridViewPLCList.AllowUserToAddRows = false;
            DataGridViewPLCList.AllowUserToDeleteRows = false;
            DataGridViewPLCList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewPLCList.MultiSelect = false;
            DataGridViewPLCList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewPLCList.EditMode = DataGridViewEditMode.EditOnEnter;
            DataGridViewPLCList.RowTemplate.Height = 32;
        }

        /// <summary>
        /// 初始化隐藏的临时文本框用于 ExpressionInputPanel 附加
        /// </summary>
        private void InitializeTempVarTextBox()
        {
            try
            {
                _tempVarTextBox = new Sunny.UI.UITextBox
                {
                    Name = "_tempVarTextBox",
                    Visible = false,
                    Size = new Size(1, 1),
                    Location = new Point(-100, -100)
                };

                // 附加表达式输入面板 - 仅用于选择变量
                ExpressionInputPanel.AttachTo(_tempVarTextBox, new InputPanelOptions
                {
                    Mode = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title = "选择目标变量",
                    ShowValidation = false,
                    ShowPreview = false,
                    CloseOnSubmit = true
                });

                // 监听文本变化
                _tempVarTextBox.TextChanged += (s, e) =>
                {
                    //if (!_isLoading && _editingRowIndex >= 0 && _editingRowIndex < DataGridViewPLCList.Rows.Count)
                    //{
                    //    DataGridViewPLCList.Rows[_editingRowIndex].Cells["ColTargetVar"].Value = _tempVarTextBox.Text;
                    //    UpdatePreview();
                    //    _logger?.LogDebug("目标变量已更新：行{Row}, 变量={Var}", _editingRowIndex, _tempVarTextBox.Text);
                    //}

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
                _logger?.LogDebug("临时文本框初始化完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化临时文本框失败");
            }
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

        #endregion

        #region PLC 数据加载

        /// <summary>
        /// 加载所有 PLC 模块
        /// </summary>
        private async void LoadPLCModules()
        {
            try
            {
                if (_plcManager == null) return;

                var modules = await _plcManager.GetModuleTagsAsync();
                var moduleNames = modules.Keys.ToArray();

                if (DataGridViewPLCList.Columns["ColPlcModule"] is DataGridViewComboBoxColumn moduleColumn)
                {
                    moduleColumn.Items.Clear();
                    moduleColumn.Items.AddRange(moduleNames);
                }

                _logger?.LogDebug("加载了 {Count} 个PLC模块", moduleNames.Length);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载PLC模块失败");
            }
        }

        /// <summary>
        /// 加载指定模块的点位地址
        /// </summary>
        private async void LoadPLCAddresses(string moduleName, int rowIndex)
        {
            try
            {
                if (string.IsNullOrEmpty(moduleName) || _plcManager == null) return;

                var modules = await _plcManager.GetModuleTagsAsync();
                if (modules.TryGetValue(moduleName, out var addresses))
                {
                    if (DataGridViewPLCList.Columns["ColPlcAddress"] is DataGridViewComboBoxColumn addressColumn)
                    {
                        // 临时保存所有行的地址值
                        var currentValues = new Dictionary<int, string>();
                        foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
                        {
                            if (row.Index != rowIndex)
                            {
                                currentValues[row.Index] = row.Cells["ColPlcAddress"].Value?.ToString() ?? "";
                            }
                        }

                        // 更新地址列表
                        addressColumn.Items.Clear();
                        addressColumn.Items.AddRange(addresses.ToArray());

                        // 恢复其他行的值
                        foreach (var kvp in currentValues)
                        {
                            if (kvp.Key < DataGridViewPLCList.Rows.Count)
                            {
                                DataGridViewPLCList.Rows[kvp.Key].Cells["ColPlcAddress"].Value = kvp.Value;
                            }
                        }
                    }

                    _logger?.LogDebug("加载模块 {Module} 的 {Count} 个点位", moduleName, addresses.Count);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载PLC地址失败");
            }
        }

        #endregion

        #region 参数加载和保存

        /// <summary>
        /// 从当前步骤加载参数
        /// </summary>
        private void LoadParametersFromStep()
        {
            try
            {
                _isLoading = true;

                var currentStep = GetCurrentStepSafely();
                if (currentStep?.StepParameter != null)
                {
                    if (currentStep.StepParameter is Parameter_ReadPLC param)
                    {
                        _currentParameter = param;
                    }
                    else
                    {
                        var jsonString = JsonConvert.SerializeObject(currentStep.StepParameter);
                        _currentParameter = JsonConvert.DeserializeObject<Parameter_ReadPLC>(jsonString) ?? new Parameter_ReadPLC();
                    }

                    // 加载到表格
                    DataGridViewPLCList.Rows.Clear();
                    foreach (var item in _currentParameter.Items ?? [])
                    {
                        int rowIndex = DataGridViewPLCList.Rows.Add(
                            "",
                            item.PlcModuleName,
                            item.PlcKeyName,
                            item.TargetVarName
                        );

                        // 异步加载该模块的地址
                        if (!string.IsNullOrEmpty(item.PlcModuleName))
                        {
                            LoadPLCAddresses(item.PlcModuleName, rowIndex);
                        }
                    }

                    UpdateRowIndices();
                    _logger?.LogDebug("加载了 {Count} 个PLC读取项", _currentParameter.Items?.Count ?? 0);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载参数失败");
            }
            finally
            {
                _isLoading = false;
            }
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                _currentParameter = new Parameter_ReadPLC
                {
                    Items = []
                };

                foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
                {
                    if (row.IsNewRow) continue;

                    string moduleName = row.Cells["ColPlcModule"].Value?.ToString() ?? "";
                    string address = row.Cells["ColPlcAddress"].Value?.ToString() ?? "";
                    string targetVar = row.Cells["ColTargetVar"].Value?.ToString() ?? "";

                    if (!string.IsNullOrEmpty(moduleName) && !string.IsNullOrEmpty(address))
                    {
                        _currentParameter.Items.Add(new PlcReadItem
                        {
                            PlcModuleName = moduleName,
                            PlcKeyName = address,
                            TargetVarName = targetVar
                        });
                    }
                }

                SetParameterValue(_currentParameter);
                _logger?.LogDebug("保存了 {Count} 个PLC读取项", _currentParameter.Items.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存参数失败");
                throw;
            }
        }

        protected override void LoadParameterToForm()
        {
            LoadParametersFromStep();
        }

        #endregion

        #region DataGridView 事件处理

        /// <summary>
        /// 单元格点击事件 - 处理目标变量列
        /// </summary>
        private void DataGridViewPLCList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 点击目标变量列时，打开 ExpressionInputPanel
            if (e.ColumnIndex == DataGridViewPLCList.Columns["ColTargetVar"].Index)
            {
                _editingRowIndex = e.RowIndex;
                var currentValue = DataGridViewPLCList.Rows[e.RowIndex].Cells["ColTargetVar"].Value?.ToString() ?? "";

                _tempVarTextBox.Text = currentValue;

                // 临时移动_tempVarTextBox到正确位置以便面板正确定位
                var cellRect = DataGridViewPLCList.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var dgvLocation = DataGridViewPLCList.PointToScreen(new Point(cellRect.Left, cellRect.Bottom));
                var formLocation = this.PointToClient(dgvLocation);

                // 临时移动textbox到单元格位置
                _tempVarTextBox.Location = formLocation;
                _tempVarTextBox.Width = cellRect.Width;

                // 显示面板
                ExpressionInputPanel.Show(_tempVarTextBox, new InputPanelOptions
                {
                    Mode = InputMode.Expression,
                    EnabledModules = InputModules.Variable,  // 所有模块
                    Title = "配置单元格内容",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true,
                    InitialExpression = currentValue  // 传递当前值
                });

                // 显示后恢复隐藏位置
                _tempVarTextBox.Location = new Point(-100, -100);
                _tempVarTextBox.Width = 1;
            }
        }

        /// <summary>
        /// 单元格值变化事件
        /// </summary>
        private void DataGridViewPLCList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0) return;

            // 当 PLC 模块变化时，加载对应的点位地址
            if (e.ColumnIndex == DataGridViewPLCList.Columns["ColPlcModule"].Index)
            {
                string moduleName = DataGridViewPLCList.Rows[e.RowIndex].Cells["ColPlcModule"].Value?.ToString();
                if (!string.IsNullOrEmpty(moduleName))
                {
                    // 清空当前行的地址选择
                    DataGridViewPLCList.Rows[e.RowIndex].Cells["ColPlcAddress"].Value = null;
                    LoadPLCAddresses(moduleName, e.RowIndex);
                }
            }

            UpdatePreview();
        }

        #endregion

        #region 按钮事件

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = DataGridViewPLCList.Rows.Add("", "", "", "");
                DataGridViewPLCList.ClearSelection();
                DataGridViewPLCList.Rows[rowIndex].Selected = true;
                UpdateRowIndices();
                UpdatePreview();
                _logger?.LogDebug("添加新行，索引: {Index}", rowIndex);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "添加行失败");
                MessageHelper.MessageOK($"添加行失败: {ex.Message}", TType.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewPLCList.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要删除的行！", TType.Warn);
                    return;
                }

                int rowIndex = DataGridViewPLCList.SelectedRows[0].Index;
                DataGridViewPLCList.Rows.RemoveAt(rowIndex);
                UpdateRowIndices();
                UpdatePreview();
                _logger?.LogDebug("删除行，索引: {Index}", rowIndex);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除行失败");
                MessageHelper.MessageOK($"删除行失败: {ex.Message}", TType.Error);
            }
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewPLCList.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的行！", TType.Warn);
                    return;
                }

                int rowIndex = DataGridViewPLCList.SelectedRows[0].Index;
                if (rowIndex == 0) return;

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
                if (DataGridViewPLCList.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的行！", TType.Warn);
                    return;
                }

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

        #region 验证和辅助方法

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

        /// <summary>
        /// 更新行号
        /// </summary>
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

        /// <summary>
        /// 交换两行
        /// </summary>
        private void SwapRows(int index1, int index2)
        {
            if (index1 < 0 || index2 < 0 || index1 >= DataGridViewPLCList.Rows.Count || index2 >= DataGridViewPLCList.Rows.Count)
                return;

            var row1 = DataGridViewPLCList.Rows[index1];
            var row2 = DataGridViewPLCList.Rows[index2];

            for (int i = 1; i < DataGridViewPLCList.Columns.Count; i++)
            {
                var temp = row1.Cells[i].Value;
                row1.Cells[i].Value = row2.Cells[i].Value;
                row2.Cells[i].Value = temp;
            }
        }

        /// <summary>
        /// 更新预览
        /// </summary>
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
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新预览失败");
            }
        }

        #endregion
    }
}
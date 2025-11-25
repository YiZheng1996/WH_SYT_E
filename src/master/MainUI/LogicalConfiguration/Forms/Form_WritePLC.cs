using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// PLC写入参数配置表单
    /// 用于配置和管理工作流步骤中的PLC写入操作
    /// </summary>
    public partial class Form_WritePLC : BaseParameterForm
    {
        #region 属性

        private Parameter_WritePLC _parameter;
        /// <summary>
        /// 参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_WritePLC Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_WritePLC();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }
        #endregion

        #region 私有字段
        /// <summary>
        /// 初始化状态标志 - 防止在窗体初始化过程中触发不必要的事件
        /// </summary>
        private bool _isInitializing = true;

        /// <summary>
        /// 未保存更改标志 - 跟踪用户是否做了未保存的修改
        /// </summary>
        private bool _hasUnsavedChanges = false;

        /// <summary>
        /// 验证定时器 - 延迟触发配置验证
        /// </summary>
        private System.Windows.Forms.Timer _validationTimer;

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
                InitializeForm();
            }
        }

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        /// <param name="workflowState">工作流状态服务</param>
        /// <param name="logger">日志服务</param>
        public Form_WritePLC(
            IWorkflowStateService workflowState,
            ILogger<Form_WritePLC> logger)
        {

            InitializeComponent();
            InitializeForm();

            Logger?.LogDebug("Form_WritePLC 依赖注入构造函数初始化完成");
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化窗体
        /// 按顺序执行各项初始化任务，确保窗体处于可用状态
        /// </summary>
        private async void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 初始化窗体样式（已在Designer中设置，这里做补充）
                InitializeFormStyle();

                // 初始化定时器
                InitializeTimers();

                // 初始化DataGridView额外配置
                InitializeDataGridViewExtras();

                // 加载PLC模块和点位
                await LoadPLCModulesAndAddresses();

                // 加载可用变量（用于支持变量引用）
                LoadAvailableVariables();

                // 绑定事件
                BindEvents();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化窗体失败");
                MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 初始化窗体样式（补充Designer中的设置）
        /// </summary>
        private void InitializeFormStyle()
        {
            // Designer中已设置基本样式，这里做运行时补充
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ShowInTaskbar = true;
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitializeTimers()
        {
            _validationTimer = new System.Windows.Forms.Timer
            {
                Interval = 500 // 0.5秒延迟
            };
            _validationTimer.Tick += ValidationTimer_Tick;
        }

        /// <summary>
        /// 初始化DataGridView额外配置（Designer已设置基础配置）
        /// </summary>
        private void InitializeDataGridViewExtras()
        {
            try
            {
                // Designer已设置基本属性，这里做运行时补充
                DataGridViewPLCList.AllowUserToAddRows = false;
                DataGridViewPLCList.AllowUserToDeleteRows = true;
                DataGridViewPLCList.AllowUserToOrderColumns = false;

                // 添加 DataError 事件处理
                DataGridViewPLCList.DataError += DataGridViewPLCList_DataError;

                Logger?.LogDebug("DataGridView额外配置完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化DataGridView额外配置失败");
            }
        }

        /// <summary>
        /// 加载PLC模块和点位地址
        /// </summary>
        private async Task LoadPLCModulesAndAddresses()
        {
            try
            {
                if (_plcManager == null) return;

                // 获取PLC模块及其点位信息
                var moduleTagsDict = await _plcManager.GetModuleTagsAsync();
                if (moduleTagsDict == null || moduleTagsDict.Count == 0)
                {
                    Logger?.LogWarning("未找到可用的PLC模块");
                    return;
                }

                // 只设置列级别的默认 Items（用于新添加的行）
                if (DataGridViewPLCList.Columns["ColPLCModule"] is DataGridViewComboBoxColumn moduleColumn)
                {
                    moduleColumn.Items.Clear();
                    foreach (var moduleName in moduleTagsDict.Keys)
                    {
                        moduleColumn.Items.Add(moduleName);
                    }
                }

                Logger?.LogInformation("成功加载 {Count} 个PLC模块", moduleTagsDict.Count);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC模块失败");
            }
        }

        /// <summary>
        /// 加载可用变量（用于值引用）
        /// </summary>
        private void LoadAvailableVariables()
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (globalVariableManager == null) return;

                var variables = globalVariableManager.GetAllVariables();

                Logger?.LogInformation("成功加载 {Count} 个可用变量", variables.Count);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载可用变量失败");
            }
        }

        /// <summary>
        /// 绑定事件处理器
        /// </summary>
        private void BindEvents()
        {
            try
            {
                // DataGridView事件
                if (DataGridViewPLCList != null)
                {
                    DataGridViewPLCList.CellValueChanged += DataGridViewPLCList_CellValueChanged;
                    DataGridViewPLCList.CurrentCellDirtyStateChanged += DataGridViewPLCList_CurrentCellDirtyStateChanged;
                    DataGridViewPLCList.RowsAdded += DataGridViewPLCList_RowsAdded;
                    DataGridViewPLCList.UserDeletingRow += DataGridViewPLCList_UserDeletingRow;
                    DataGridViewPLCList.DragDrop += DataGridViewPLCList_DragDrop;
                    DataGridViewPLCList.DragEnter += DataGridViewPLCList_DragEnter;
                }

                // 按钮事件
                if (btnAdd != null) btnAdd.Click += BtnAdd_Click;
                if (btnDelete != null) btnDelete.Click += BtnDelete_Click;
                if (btnMoveUp != null) btnMoveUp.Click += BtnMoveUp_Click;
                if (btnMoveDown != null) btnMoveDown.Click += BtnMoveDown_Click;
                if (btnSave != null) btnSave.Click += BtnSave_Click;
                if (btnCancel != null) btnCancel.Click += BtnCancel_Click;
                if (btnHelp != null) btnHelp.Click += BtnHelp_Click;

                // 窗体事件
                this.FormClosing += Form_WritePLC_FormClosing;

                Logger?.LogDebug("事件绑定完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "绑定事件失败");
            }
        }

        #endregion

        #region 基类方法重写

        /// <summary>
        /// 加载参数到界面（从参数对象到UI控件）
        /// </summary>
        protected override async void LoadParameterToForm()
        {
            try
            {
                _parameter ??= new Parameter_WritePLC();

                _isInitializing = true;

                // 清空DataGridView
                DataGridViewPLCList.Rows.Clear();

                // 加载描述信息
                if (txtDescription != null)
                {
                    txtDescription.Text = _parameter.Description ?? "";
                }

                if (chkEnabled != null)
                {
                    chkEnabled.Checked = _parameter.IsEnabled;
                }

                // 加载写入项
                if (_parameter.Items != null && _parameter.Items.Count > 0)
                {
                    // 获取所有PLC模块的地址信息（用于填充下拉框）
                    var moduleTagsDict = _plcManager != null ? await _plcManager.GetModuleTagsAsync() : null;

                    foreach (var item in _parameter.Items)
                    {
                        int rowIndex = DataGridViewPLCList.Rows.Add();
                        var row = DataGridViewPLCList.Rows[rowIndex];

                        // 序号
                        row.Cells["ColIndex"].Value = rowIndex + 1;

                        // PLC模块（ComboBox）
                        if (row.Cells["ColPLCModule"] is DataGridViewComboBoxCell moduleCell)
                        {
                            if (!string.IsNullOrEmpty(item.PlcModuleName))
                            {
                                if (!moduleCell.Items.Contains(item.PlcModuleName))
                                {
                                    moduleCell.Items.Add(item.PlcModuleName);
                                }
                                moduleCell.Value = item.PlcModuleName;
                            }
                        }

                        // PLC地址（ComboBox）- 根据模块动态填充
                        if (row.Cells["ColPLCAddress"] is DataGridViewComboBoxCell addressCell && moduleTagsDict != null &&
                            !string.IsNullOrEmpty(item.PlcModuleName) &&
                            moduleTagsDict.TryGetValue(item.PlcModuleName, out List<string> addresses))
                        {
                            // 先填充 Items
                            addressCell.Items.Clear();
                            foreach (var addr in addresses)
                            {
                                addressCell.Items.Add(addr);
                            }

                            // 再设置 Value
                            if (!string.IsNullOrEmpty(item.PlcKeyName) &&
                                addressCell.Items.Contains(item.PlcKeyName))
                            {
                                addressCell.Value = item.PlcKeyName;
                            }
                        }

                        // 写入值和描述（TextBox，直接设置）
                        row.Cells["ColWriteValue"].Value = item.PlcValue ?? "";
                        row.Cells["ColDescription"].Value = item.Description ?? "";
                    }
                }

                _hasUnsavedChanges = false;
                Logger?.LogDebug("参数已加载到界面");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数到界面失败");
                MessageHelper.MessageOK($"加载参数失败：{ex.Message}", TType.Error);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 保存界面数据到参数对象
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                _parameter ??= new Parameter_WritePLC();

                // 保存基本信息
                _parameter.Description = txtDescription?.Text ?? "";
                _parameter.IsEnabled = chkEnabled?.Checked ?? true;

                // 收集DataGridView中的数据
                var items = new List<Parameter_WritePLC.PLCWriteItem>();

                foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
                {
                    // 跳过空行和新增行
                    if (row.IsNewRow) continue;

                    var module = row.Cells["ColPLCModule"].Value?.ToString();
                    var address = row.Cells["ColPLCAddress"].Value?.ToString();
                    var value = row.Cells["ColWriteValue"].Value?.ToString();

                    // 必须至少有模块和地址
                    if (string.IsNullOrWhiteSpace(module) || string.IsNullOrWhiteSpace(address))
                    {
                        continue;
                    }

                    items.Add(new Parameter_WritePLC.PLCWriteItem
                    {
                        PlcModuleName = module,
                        PlcKeyName = address,
                        PlcValue = value ?? "",
                        Description = row.Cells["ColDescription"].Value?.ToString() ?? ""
                    });
                }

                _parameter.Items = items;

                Logger?.LogDebug("界面数据已保存到参数对象，共{Count}个项目", items.Count);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存界面数据到参数对象失败");
                throw;
            }
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            try
            {
                _parameter = new Parameter_WritePLC
                {
                    Description = $"PLC写入步骤 {(_workflowState?.StepNum ?? 0) + 1}",
                    IsEnabled = true,
                    Items = []
                };

                LoadParameterToForm();

                Logger?.LogDebug("已设置默认值");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "设置默认值失败");
            }
        }

        /// <summary>
        /// 验证输入数据的有效性
        /// </summary>
        protected override bool ValidateInput()
        {
            try
            {
                // 收集当前数据
                SaveFormToParameter();

                // 检查是否有有效数据
                if (_parameter.Items == null || _parameter.Items.Count == 0)
                {
                    MessageHelper.MessageOK("请至少添加一个PLC写入项！", TType.Warn);
                    return false;
                }

                // 验证每一项
                for (int i = 0; i < _parameter.Items.Count; i++)
                {
                    var item = _parameter.Items[i];

                    if (string.IsNullOrWhiteSpace(item.PlcModuleName))
                    {
                        MessageHelper.MessageOK($"第 {i + 1} 项：PLC模块不能为空！", TType.Warn);
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(item.PlcKeyName))
                    {
                        MessageHelper.MessageOK($"第 {i + 1} 项：PLC地址不能为空！", TType.Warn);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入失败");
                MessageHelper.MessageOK($"验证失败：{ex.Message}", TType.Error);
                return false;
            }
        }

        /// <summary>
        /// 为指定行加载PLC地址列表
        /// </summary>
        private async Task LoadAddressesForRow(int rowIndex, string moduleName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(moduleName) || _plcManager == null) return;
                if (rowIndex < 0 || rowIndex >= DataGridViewPLCList.Rows.Count) return;

                var addresses = await _plcManager.GetModuleTagsAsync();
                if (addresses == null || addresses.Count == 0)
                {
                    Logger?.LogWarning("模块 {ModuleName} 没有可用地址", moduleName);
                    return;
                }

                if (DataGridViewPLCList.Rows[rowIndex].Cells["ColPLCAddress"] is
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
                Logger?.LogError(ex, "加载地址列表失败");
            }
        }

        #endregion

        #region DataGridView事件

        /// <summary>
        /// 单元格值改变事件
        /// </summary>
        private async void DataGridViewPLCList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isInitializing) return;
            if (e.RowIndex < 0) return;

            try
            {
                // 当PLC模块改变时，刷新对应的地址列表
                if (e.ColumnIndex == DataGridViewPLCList.Columns["ColPLCModule"].Index)
                {
                    var moduleName = DataGridViewPLCList.Rows[e.RowIndex].Cells["ColPLCModule"].Value?.ToString();
                    if (!string.IsNullOrEmpty(moduleName))
                    {
                        await LoadAddressesForRow(e.RowIndex, moduleName);
                    }
                }

                _hasUnsavedChanges = true;
                RestartValidationTimer();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "处理单元格值改变事件失败");
            }
        }

        /// <summary>
        /// 当前单元格脏状态改变事件
        /// </summary>
        private void DataGridViewPLCList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DataGridViewPLCList.IsCurrentCellDirty)
            {
                DataGridViewPLCList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// 行添加事件
        /// </summary>
        private void DataGridViewPLCList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (_isInitializing) return;
            UpdateRowIndices();
        }

        /// <summary>
        /// 用户删除行事件
        /// </summary>
        private void DataGridViewPLCList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (_isInitializing) return;

            var result = MessageHelper.MessageYes(this, "确定要删除选中的PLC写入项吗？");
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }

            _hasUnsavedChanges = true;
        }

        /// <summary>
        /// DataGridView数据错误事件
        /// </summary>
        private void DataGridViewPLCList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Logger?.LogWarning("DataGridView数据错误: Row={Row}, Column={Column}, Error={Error}",
                e.RowIndex, e.ColumnIndex, e.Exception?.Message);
            e.ThrowException = false;
        }

        /// <summary>
        /// 拖拽进入事件
        /// </summary>
        private void DataGridViewPLCList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// 拖拽放下事件
        /// </summary>
        private void DataGridViewPLCList_DragDrop(object sender, DragEventArgs e)
        {
            // 实现行拖拽排序功能
            var clientPoint = DataGridViewPLCList.PointToClient(new System.Drawing.Point(e.X, e.Y));
            var targetRowIndex = DataGridViewPLCList.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (targetRowIndex >= 0 && DataGridViewPLCList.SelectedRows.Count > 0)
            {
                var selectedRow = DataGridViewPLCList.SelectedRows[0];
                DataGridViewPLCList.Rows.RemoveAt(selectedRow.Index);
                DataGridViewPLCList.Rows.Insert(targetRowIndex, selectedRow);
                UpdateRowIndices();
                _hasUnsavedChanges = true;
            }
        }

        /// <summary>
        /// 更新所有行的序号
        /// </summary>
        private void UpdateRowIndices()
        {
            int index = 1;
            foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["ColIndex"].Value = index++;
                }
            }
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 添加按钮点击事件
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // 添加新行
                int rowIndex = DataGridViewPLCList.Rows.Add();
                var row = DataGridViewPLCList.Rows[rowIndex];

                row.Cells["ColIndex"].Value = DataGridViewPLCList.Rows.Count;

                // 选中新行
                DataGridViewPLCList.ClearSelection();
                row.Selected = true;
                DataGridViewPLCList.CurrentCell = row.Cells["ColPLCModule"];

                _hasUnsavedChanges = true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "添加行失败");
                MessageHelper.MessageOK($"添加失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewPLCList.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要删除的行！", TType.Warn);
                    return;
                }

                var result = MessageHelper.MessageYes(this, "确定要删除选中的PLC写入项吗？");
                if (result == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in DataGridViewPLCList.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            DataGridViewPLCList.Rows.Remove(row);
                        }
                    }

                    UpdateRowIndices();
                    _hasUnsavedChanges = true;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "删除行失败");
                MessageHelper.MessageOK($"删除失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 上移按钮点击事件
        /// </summary>
        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewPLCList.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的行！", TType.Warn);
                    return;
                }

                var selectedRow = DataGridViewPLCList.SelectedRows[0];
                var rowIndex = selectedRow.Index;

                if (rowIndex > 0)
                {
                    DataGridViewPLCList.Rows.RemoveAt(rowIndex);
                    DataGridViewPLCList.Rows.Insert(rowIndex - 1, selectedRow);
                    DataGridViewPLCList.ClearSelection();
                    selectedRow.Selected = true;
                    UpdateRowIndices();
                    _hasUnsavedChanges = true;
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
                if (DataGridViewPLCList.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的行！", TType.Warn);
                    return;
                }

                var selectedRow = DataGridViewPLCList.SelectedRows[0];
                var rowIndex = selectedRow.Index;

                if (rowIndex < DataGridViewPLCList.Rows.Count - 1)
                {
                    DataGridViewPLCList.Rows.RemoveAt(rowIndex);
                    DataGridViewPLCList.Rows.Insert(rowIndex + 1, selectedRow);
                    DataGridViewPLCList.ClearSelection();
                    selectedRow.Selected = true;
                    UpdateRowIndices();
                    _hasUnsavedChanges = true;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "下移行失败");
                MessageHelper.MessageOK($"下移失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 基类统一处理
            SaveParameters();
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 帮助按钮点击事件
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string helpText = @"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📖 PLC写入配置 - 使用说明
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

🔹 基本操作
   • 添加: 点击【添加】按钮添加新的写入项
   • 删除: 选中行后点击【删除】按钮
   • 排序: 使用【上移】/【下移】调整执行顺序
   • 拖拽: 支持拖拽行来调整顺序

🔹 配置说明
   • PLC模块: 选择要写入的PLC模块
   • PLC地址: 选择或输入目标地址
   • 写入值: 支持常量或变量引用
   • 描述: 添加备注信息

🔹 变量引用
   • 使用 {变量名} 引用全局变量
   • 示例: {Temperature} 表示读取Temperature变量的值
   • 支持混合使用: 固定值100或{变量}

🔹 执行顺序
   • 按表格中的顺序依次执行
   • 可通过上移/下移或拖拽调整顺序
   • 序号列显示执行顺序

⚠️ 注意事项
   1. 确保PLC模块已正确配置并可连接
   2. 写入值的类型应与地址匹配
   3. 建议使用测试功能验证配置
   4. 修改后记得点击【保存】按钮

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

                MessageHelper.MessageOK(this, helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助失败");
            }
        }

        #endregion

        #region 验证方法

        /// <summary>
        /// 验证定时器触发事件
        /// </summary>
        private void ValidationTimer_Tick(object sender, EventArgs e)
        {
            _validationTimer?.Stop();
            // 可以在这里执行延迟验证逻辑
        }

        /// <summary>
        /// 重启验证定时器
        /// </summary>
        private void RestartValidationTimer()
        {
            _validationTimer?.Stop();
            _validationTimer?.Start();
        }

        #endregion

        #region 窗体事件

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_WritePLC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK) return;

            if (_hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes(this, "存在未保存的更改，确定要关闭吗？");
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion
    }
}
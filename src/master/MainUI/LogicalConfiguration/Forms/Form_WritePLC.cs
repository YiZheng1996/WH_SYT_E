using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// PLC写入参数配置表单（优化版）
    /// 用于配置和管理工作流步骤中的PLC写入操作
    /// 参考 Form_VariableAssignment 的设计模式进行优化
    /// </summary>
    public partial class Form_WritePLC : BaseParameterForm, IParameterForm<Parameter_WritePLC>
    {
        #region 私有字段
        /// <summary>
        /// 当前参数对象缓存
        /// </summary>
        private Parameter_WritePLC _parameter;

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

        #region 属性

        /// <summary>
        /// 参数对象属性（IParameterForm接口实现）
        /// </summary>
        public Parameter_WritePLC Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_WritePLC();

                // 只有在非设计模式、非基类加载状态且窗体句柄已创建时才加载到界面
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
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
                InitializeForm();
            }
        }

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        /// <param name="workflowState">工作流状态服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="plcManager">PLC管理器服务</param>
        public Form_WritePLC(
            IWorkflowStateService workflowState,
            ILogger<Form_WritePLC> logger)
            : base(workflowState, logger)
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
        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 初始化窗体样式
                InitializeFormStyle();

                // 初始化定时器
                InitializeTimers();

                // 初始化DataGridView
                InitializeDataGridView();

                // 加载PLC模块和点位
                LoadPLCModulesAndAddresses();

                // 加载可用变量（用于支持变量引用）
                LoadAvailableVariables();

                // 绑定事件
                BindEvents();

                // 从工作流状态加载参数
                LoadParameterFromWorkflowState();
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
        /// 初始化窗体样式
        /// </summary>
        private void InitializeFormStyle()
        {
            this.Text = "PLC写入配置";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Width = 900;
            this.Height = 600;

            // 设置SunnyUI主题（如果使用）
            if (this is Sunny.UI.UIForm uiForm)
            {
                uiForm.Style = Sunny.UI.UIStyle.Custom;
                uiForm.StyleCustomMode = true;
                uiForm.TitleColor = System.Drawing.Color.FromArgb(65, 100, 204);
                uiForm.TitleFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
                uiForm.RectColor = System.Drawing.Color.FromArgb(65, 100, 204);
            }
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitializeTimers()
        {
            _validationTimer = new System.Windows.Forms.Timer
            {
                Interval = 500 // 500ms延迟
            };
            _validationTimer.Tick += (s, e) =>
            {
                _validationTimer.Stop();
                ValidateConfigurationAsync();
            };
        }

        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        private void InitializeDataGridView()
        {
            try
            {
                if (DataGridViewPLCList == null) return;

                // 设置样式
                DataGridViewPLCList.AllowUserToAddRows = true;
                DataGridViewPLCList.AllowUserToDeleteRows = true;
                DataGridViewPLCList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DataGridViewPLCList.MultiSelect = false;
                DataGridViewPLCList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewPLCList.RowHeadersVisible = true;
                DataGridViewPLCList.AllowDrop = true;

                // 设置列
                DataGridViewPLCList.Columns.Clear();

                // 序号列
                DataGridViewPLCList.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ColIndex",
                    HeaderText = "序号",
                    ReadOnly = true,
                    Width = 60,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                });

                // PLC模块列（下拉框）
                var colModule = new DataGridViewComboBoxColumn
                {
                    Name = "ColPLCModule",
                    HeaderText = "PLC模块",
                    Width = 150,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FlatStyle = FlatStyle.Flat
                };
                DataGridViewPLCList.Columns.Add(colModule);

                // PLC地址列（下拉框）
                var colAddress = new DataGridViewComboBoxColumn
                {
                    Name = "ColPLCAddress",
                    HeaderText = "PLC地址",
                    Width = 200,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FlatStyle = FlatStyle.Flat
                };
                DataGridViewPLCList.Columns.Add(colAddress);

                // 写入值列（可输入变量引用）
                DataGridViewPLCList.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ColWriteValue",
                    HeaderText = "写入值（支持{变量名}）",
                    Width = 200,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                // 描述列
                DataGridViewPLCList.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ColDescription",
                    HeaderText = "描述",
                    Width = 150,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                // 添加 DataError 事件处理
                DataGridViewPLCList.DataError += DataGridViewPLCList_DataError;

                Logger?.LogDebug("DataGridView初始化完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化DataGridView失败");
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

                // ✅ 只设置列级别的默认 Items（用于新添加的行）
                var moduleColumn = DataGridViewPLCList.Columns["ColPLCModule"] as DataGridViewComboBoxColumn;
                if (moduleColumn != null)
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

                // 可以在界面上添加一个提示标签，告诉用户可以使用 {变量名} 引用变量
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
                if (btnTest != null) btnTest.Click += BtnTest_Click;
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

        #region 参数加载和保存

        /// <summary>
        /// 从工作流状态加载参数
        /// </summary>
        private void LoadParameterFromWorkflowState()
        {
            try
            {
                if (!IsServiceAvailable)
                {
                    Logger?.LogWarning("服务不可用，无法加载PLC参数");
                    SetDefaultValues();
                    return;
                }

                var steps = _workflowState.GetSteps();
                int idx = _workflowState.StepNum;

                if (steps == null || idx < 0 || idx >= steps.Count)
                {
                    Logger?.LogWarning("步骤索引无效: Index={Index}, Count={Count}", idx, steps?.Count ?? 0);
                    SetDefaultValues();
                    return;
                }

                var currentStep = steps[idx];
                var paramObj = currentStep.StepParameter;

                // 解析参数
                if (paramObj is Parameter_WritePLC directParam)
                {
                    _parameter = directParam;
                    Logger?.LogDebug("直接获取Parameter_WritePLC参数");
                }
                else if (paramObj != null)
                {
                    try
                    {
                        string jsonString = paramObj is string s ? s : JsonConvert.SerializeObject(paramObj);
                        _parameter = JsonConvert.DeserializeObject<Parameter_WritePLC>(jsonString);
                        Logger?.LogDebug("JSON反序列化获取Parameter_WritePLC参数");
                    }
                    catch (JsonException jsonEx)
                    {
                        Logger?.LogWarning(jsonEx, "JSON反序列化失败，使用默认参数");
                        _parameter = new Parameter_WritePLC();
                    }
                }
                else
                {
                    _parameter = new Parameter_WritePLC();
                    Logger?.LogDebug("参数为空，创建默认Parameter_WritePLC参数");
                }

                _parameter ??= new Parameter_WritePLC();

                // 加载到界面
                LoadParameterToForm();

                Logger?.LogInformation("成功加载PLC写入参数，包含{Count}个项目", _parameter.Items?.Count ?? 0);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC写入参数时发生异常");
                _parameter = new Parameter_WritePLC();
                MessageHelper.MessageOK($"加载PLC参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 加载参数到界面（从参数对象到UI控件）
        /// </summary>
        private async Task LoadParameterToForm()
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

                // 加载启用状态
                if (chkEnabled != null)
                {
                    chkEnabled.Checked = _parameter.IsEnabled;
                }

                // 先获取所有模块的点位信息
                Dictionary<string, List<string>> moduleTagsDict = null;
                if (_plcManager != null)
                {
                    try
                    {
                        moduleTagsDict = await _plcManager.GetModuleTagsAsync();
                    }
                    catch (Exception ex)
                    {
                        Logger?.LogError(ex, "获取模块点位信息失败");
                    }
                }

                // 加载PLC项目
                if (_parameter.Items != null && _parameter.Items.Any())
                {
                    int index = 1;
                    foreach (var item in _parameter.Items)
                    {
                        int rowIndex = DataGridViewPLCList.Rows.Add();
                        var row = DataGridViewPLCList.Rows[rowIndex];

                        // 设置序号（TextBox，直接设置）
                        row.Cells["ColIndex"].Value = index++;

                        // 处理 PLC模块 ComboBoxCell
                        if (row.Cells["ColPLCModule"] is DataGridViewComboBoxCell moduleCell && 
                            moduleTagsDict != null)
                        {
                            // 先填充 Items
                            moduleCell.Items.Clear();
                            foreach (var moduleName in moduleTagsDict.Keys)
                            {
                                moduleCell.Items.Add(moduleName);
                            }

                            // 再设置 Value
                            if (!string.IsNullOrEmpty(item.PlcModuleName) &&
                                moduleCell.Items.Contains(item.PlcModuleName))
                            {
                                moduleCell.Value = item.PlcModuleName;
                            }
                        }

                        //  处理 PLC地址 ComboBoxCell
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
        private void SaveFormToParameter()
        {
            try
            {
                if (_parameter == null)
                {
                    _parameter = new Parameter_WritePLC();
                }

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
        /// 为指定行加载PLC地址列表
        /// </summary>
        private async Task LoadAddressesForRow(int rowIndex, string moduleName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(moduleName) || _plcManager == null) return;
                if (rowIndex < 0 || rowIndex >= DataGridViewPLCList.Rows.Count) return;

                var addresses = await _plcManager.GetModuleTagsAsync(moduleName);
                if (addresses == null || !addresses.Any())
                {
                    Logger?.LogWarning("模块 {ModuleName} 没有可用地址", moduleName);
                    return;
                }

                if (DataGridViewPLCList.Rows[rowIndex].Cells["ColPLCAddress"] is DataGridViewComboBoxCell addressCell)
                {
                    // 保存当前值
                    var currentValue = addressCell.Value;

                    // 清空并填充 Items
                    addressCell.Items.Clear();
                    foreach (var addr in addresses)
                    {
                        addressCell.Items.Add(addr);
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

        #region 验证方法

        /// <summary>
        /// 验证输入数据的有效性
        /// </summary>
        private bool ValidateInput()
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

                // 验证每个项目
                int index = 1;
                foreach (var item in _parameter.Items)
                {
                    // 验证模块名
                    if (string.IsNullOrWhiteSpace(item.PlcModuleName))
                    {
                        MessageHelper.MessageOK($"第{index}项：PLC模块名不能为空！", TType.Warn);
                        return false;
                    }

                    // 验证地址
                    if (string.IsNullOrWhiteSpace(item.PlcKeyName))
                    {
                        MessageHelper.MessageOK($"第{index}项：PLC地址不能为空！", TType.Warn);
                        return false;
                    }

                    // 写入值可以为空（表示写0或false）

                    index++;
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
        /// 异步验证配置
        /// </summary>
        private async void ValidateConfigurationAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    // 这里可以添加异步验证逻辑
                    // 例如：检查PLC连接状态、验证地址有效性等
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "异步验证失败");
                }
            });
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// DataGridView单元格值改变事件
        /// </summary>
        private async void DataGridViewPLCList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isInitializing || e.RowIndex < 0) return;

            try
            {
                _hasUnsavedChanges = true;

                // 如果是模块列改变，更新对应的地址列
                if (e.ColumnIndex == DataGridViewPLCList.Columns["ColPLCModule"].Index)
                {
                    var moduleName = DataGridViewPLCList.Rows[e.RowIndex].Cells["ColPLCModule"].Value?.ToString();

                    if (!string.IsNullOrWhiteSpace(moduleName))
                    {
                        // 获取该模块的地址列表
                        var addresses = await _plcManager.GetModuleTagsAsync(moduleName);

                        // 更新地址单元格的 Items
                        if (DataGridViewPLCList.Rows[e.RowIndex].Cells["ColPLCAddress"] is DataGridViewComboBoxCell addressCell)
                        {
                            // 保存当前值
                            var currentValue = addressCell.Value;

                            // 清空并填充新的 Items
                            addressCell.Items.Clear();
                            if (addresses != null && addresses.Any())
                            {
                                foreach (var addr in addresses)
                                {
                                    addressCell.Items.Add(addr);
                                }
                            }

                            // 如果原来的值还在新列表中，恢复它；否则清空
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
                }

                // 更新序号
                UpdateRowIndices();

                // 重启验证定时器
                RestartValidationTimer();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "处理单元格值改变事件失败");
            }
        }

        // 添加事件处理方法
        private void DataGridViewPLCList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Logger?.LogWarning("DataGridView 数据错误: 行={Row}, 列={Column}, 错误={Error}",
                e.RowIndex, e.ColumnIndex, e.Exception?.Message);

            // 阻止默认的错误对话框显示
            e.ThrowException = false;
            e.Cancel = true;
        }

        /// <summary>
        /// 当前单元格脏状态改变事件（用于立即提交ComboBox的改变）
        /// </summary>
        private void DataGridViewPLCList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

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

        /// <summary>
        /// 重启验证定时器
        /// </summary>
        private void RestartValidationTimer()
        {
            _validationTimer?.Stop();
            _validationTimer?.Start();
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

                if (rowIndex < DataGridViewPLCList.Rows.Count - 2) // -2因为最后一行是新增行
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
            try
            {
                // 验证输入
                if (!ValidateInput())
                {
                    return;
                }

                // 获取当前步骤
                var currentStep = GetCurrentStepSafely();
                if (currentStep == null)
                {
                    MessageHelper.MessageOK("当前步骤无效，无法保存PLC数据。", TType.Warn);
                    return;
                }

                // 保存界面数据到参数对象
                SaveFormToParameter();

                // 序列化参数对象并保存到步骤
                currentStep.StepParameter = JsonConvert.SerializeObject(_parameter);

                _hasUnsavedChanges = false;

                Logger?.LogInformation("PLC写入参数保存成功，共{Count}个项目", _parameter.Items?.Count ?? 0);
                MessageHelper.MessageOK("保存成功！PLC操作将在主界面保存时写入配置文件。", TType.Success);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存PLC写入参数失败");
                MessageHelper.MessageOK($"保存失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = MessageHelper.MessageYes(this, "存在未保存的更改，确定要关闭吗？");
                if (result != DialogResult.OK)
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 测试按钮点击事件
        /// </summary>
        private void BtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证输入
                if (!ValidateInput())
                {
                    return;
                }

                // 保存当前数据
                SaveFormToParameter();

                MessageHelper.MessageOK("测试功能：\n\n" +
                    $"将执行 {_parameter.Items.Count} 个PLC写入操作\n\n" +
                    "注意：测试模式下不会实际写入PLC设备", TType.Info);

                // TODO: 实现实际的测试逻辑
                // 可以调用 PLC 管理器进行连接测试等
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "测试失败");
                MessageHelper.MessageOK($"测试失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 帮助按钮点击事件
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string helpText = @"PLC写入配置帮助：

1. 基本操作：
   - 点击【添加】按钮添加新的写入项
   - 点击【删除】按钮删除选中的项
   - 点击【上移】/【下移】调整执行顺序
   - 支持拖拽行来调整顺序

2. 配置说明：
   - PLC模块：选择要写入的PLC模块
   - PLC地址：选择或输入目标地址
   - 写入值：支持常量或变量引用（使用{变量名}）
   - 描述：添加备注信息

3. 变量引用：
   - 使用 {变量名} 引用全局变量
   - 例如：{Temperature} 表示读取Temperature变量的值

4. 执行顺序：
   - 按表格中的顺序依次执行
   - 可通过上移/下移或拖拽调整顺序

5. 注意事项：
   - 确保PLC模块已正确配置并可连接
   - 写入值的类型应与地址匹配
   - 建议使用测试功能验证配置";

                MessageHelper.MessageOK(this, helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助失败");
            }
        }

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

        #region BaseParameterForm重写和接口实现

        /// <summary>
        /// 设置默认值（BaseParameterForm调用）
        /// </summary>
        protected override void SetDefaultValues()
        {
            try
            {
                _parameter = new Parameter_WritePLC
                {
                    Description = $"PLC写入步骤 {_workflowState?.StepNum + 1}",
                    IsEnabled = true,
                    Items = new List<Parameter_WritePLC.PLCWriteItem>()
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
        /// 从步骤参数加载（基类方法重写）
        /// </summary>
        protected override void LoadParameterFromStep(object stepParameter)
        {
            try
            {
                Parameter_WritePLC loadedParameter = null;

                // 尝试直接类型转换
                if (stepParameter is Parameter_WritePLC directParam)
                {
                    loadedParameter = directParam;
                    Logger?.LogDebug("直接获取Parameter_WritePLC参数");
                }
                // 尝试JSON反序列化
                else if (stepParameter != null)
                {
                    try
                    {
                        string jsonString = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                        loadedParameter = JsonConvert.DeserializeObject<Parameter_WritePLC>(jsonString);
                        Logger?.LogDebug("JSON反序列化获取Parameter_WritePLC参数");
                    }
                    catch (JsonException jsonEx)
                    {
                        Logger?.LogWarning(jsonEx, "JSON反序列化失败，使用默认参数");
                        loadedParameter = null;
                    }
                }

                if (loadedParameter != null)
                {
                    _parameter = loadedParameter;
                    Logger?.LogInformation("成功加载PLC写入参数: {Description}", _parameter.Description);
                }
                else
                {
                    Logger?.LogWarning("加载参数失败，使用默认参数");
                    SetDefaultValues();
                    return;
                }

                LoadParameterToForm();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数时发生错误");
                MessageHelper.MessageOK($"加载参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 收集参数（基类方法重写）
        /// </summary>
        protected override object CollectParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        /// <summary>
        /// 验证参数（基类方法重写）
        /// </summary>
        protected override bool ValidateParameters()
        {
            return ValidateInput();
        }

        /// <summary>
        /// 填充控件（IParameterForm接口实现）
        /// </summary>
        public void PopulateControls(Parameter_WritePLC parameter)
        {
            Parameter = parameter;
        }

        /// <summary>
        /// 设置默认值（IParameterForm接口实现）
        /// </summary>
        void IParameterForm<Parameter_WritePLC>.SetDefaultValues()
        {
            SetDefaultValues();
        }

        /// <summary>
        /// 验证类型化参数（IParameterForm接口实现）
        /// </summary>
        public bool ValidateTypedParameters()
        {
            return ValidateInput();
        }

        /// <summary>
        /// 收集类型化参数（IParameterForm接口实现）
        /// </summary>
        public Parameter_WritePLC CollectTypedParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        /// <summary>
        /// 转换参数对象（IParameterForm接口实现）
        /// </summary>
        public Parameter_WritePLC ConvertParameter(object stepParameter)
        {
            if (stepParameter is Parameter_WritePLC paramObj)
                return paramObj;

            if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Parameter_WritePLC>(jsonStr)
                        ?? new Parameter_WritePLC();
                }
                catch (JsonException ex)
                {
                    Logger?.LogWarning(ex, "转换参数失败");
                    return new Parameter_WritePLC();
                }
            }

            return new Parameter_WritePLC();
        }

        #endregion
    }
}
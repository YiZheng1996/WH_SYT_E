using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 读取单元格参数配置表单
    /// 支持:
    /// 1. 多单元格批量读取
    /// 2. 读取结果保存到变量
    /// 3. 支持指定工作表
    /// 4. 单元格地址验证
    /// </summary>
    public partial class Form_ReadCells : BaseParameterForm
    {
        #region 属性

        private Parameter_ReadCells _parameter;
        /// <summary>
        /// 参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_ReadCells Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_ReadCells();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }
        #endregion


        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private bool _isLoading = false;

        // 单元格地址格式正则
        private readonly System.Text.RegularExpressions.Regex _cellAddressRegex =
            new(@"^[A-Z]+[0-9]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Form_ReadCells()
        {
            InitializeComponent();
            _variableManager = Program.ServiceProvider.GetService<GlobalVariableManager>();
            InitializeForm();
        }

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        public Form_ReadCells(GlobalVariableManager variableManager)
        {
            InitializeComponent();
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化表单
        /// </summary>
        private void InitializeForm()
        {
            if (DesignMode) return;

            _isLoading = true;

            try
            {
                // 初始化事件绑定
                BindEvents();

                // 设置默认值
                _parameter ??= new Parameter_ReadCells();

                // 加载全局变量
                LoadVariables();

                // 加载参数到界面
                LoadParametersFromWorkflow();
            }
            finally
            {
                _isLoading = false;
            }
        }


        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            // 添加行按钮
            btnAddRow.Click += BtnAddRow_Click;

            // 删除行按钮
            BtnDelete.Click += BtnDelete_Click;

            // 预览按钮
            btnPreview.Click += BtnPreview_Click;

            // 保存按钮
            btnSave.Click += BtnSave_Click;

            // 取消按钮
            btnCancel.Click += BtnCancel_Click;

            // 数据网格单元格值变化
            DataGridViewDefineVar.CellValueChanged += DataGridView_CellValueChanged;
            DataGridViewDefineVar.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
            DataGridViewDefineVar.DataError += DataGridView_DataError;

            // 单元格验证
            DataGridViewDefineVar.CellValidating += DataGridView_CellValidating;
        }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载全局变量到下拉框
        /// </summary>
        private void LoadVariables()
        {
            if (_variableManager == null) return;

            var variables = _variableManager.GetAllUserVariables()
                .Select(v => v.VarName)
                .OrderBy(n => n)
                .ToList();

            // 更新保存到变量列的下拉选项
            if (DataGridViewDefineVar.Columns["ColSaveToVar"] is DataGridViewComboBoxColumn colSaveToVar)
            {
                colSaveToVar.Items.Clear();
                colSaveToVar.Items.AddRange([.. variables]);
            }
        }

       
        /// <summary>
        /// 获取当前参数
        /// </summary>
        private Parameter_ReadCells GetCurrentParameters()
        {
            var param = new Parameter_ReadCells
            {
                SheetName = txtSheetName.Text?.Trim(),
                ReadItems = []
            };

            // 收集所有行数据
            foreach (DataGridViewRow row in DataGridViewDefineVar.Rows)
            {
                if (row.IsNewRow) continue;

                var cellAddress = row.Cells["ColCellAddress"].Value?.ToString()?.Trim();
                if (string.IsNullOrEmpty(cellAddress)) continue;

                param.ReadItems.Add(new ReadCellItem
                {
                    CellAddress = cellAddress,
                    SaveToVariable = row.Cells["ColSaveToVar"].Value?.ToString(),
                    DataType = GetDataTypeFromDisplayName(row.Cells["ColDataType"].Value?.ToString()),
                    DefaultValue = row.Cells["ColDefaultValue"].Value?.ToString()
                });
            }

            return param;
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 添加新行
        /// </summary>
        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        /// <summary>
        /// 添加新行的实现
        /// </summary>
        private void AddNewRow()
        {
            int rowIndex = DataGridViewDefineVar.Rows.Add();
            var row = DataGridViewDefineVar.Rows[rowIndex];

            // 设置默认值
            row.Cells["ColDataType"].Value = "字符串";
            row.Cells["ColCellAddress"].Value = $"A{rowIndex + 1}";
        }

        /// <summary>
        /// 删除选中行
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (DataGridViewDefineVar.SelectedRows.Count == 0)
            {
                MessageHelper.MessageOK(this, "请选择要删除的行", TType.Warn);
                return;
            }

            if (MessageHelper.MessageYes(this, "确定要删除选中的行吗?", TType.Info) == DialogResult.OK)
            {
                foreach (DataGridViewRow row in DataGridViewDefineVar.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        DataGridViewDefineVar.Rows.Remove(row);
                    }
                }
            }
        }

        /// <summary>
        /// 预览按钮点击 - 测试读取功能
        /// </summary>
        private async void BtnPreview_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                btnPreview.Enabled = false;
                btnPreview.Text = "读取中...";

                var param = GetCurrentParameters();

                var reportMethods = Program.ServiceProvider.GetService<ReportMethods>();
                if (reportMethods == null)
                {
                    MessageHelper.MessageOK("报表服务未初始化", TType.Error);
                    return;
                }

                //  逐个读取单元格
                foreach (var item in param.ReadItems)
                {
                    // 为每个单元格创建单独的参数
                    var readParam = new Parameter_ReadCells
                    {
                        SheetName = param.SheetName,
                        ReadItems = [item]
                    };

                    // 调用读取方法
                    var value = await reportMethods.ReadCells(readParam);

                    // 更新预览值
                    foreach (DataGridViewRow row in DataGridViewDefineVar.Rows)
                    {
                        if (row.Cells["ColCellAddress"].Value?.ToString() == item.CellAddress)
                        {
                            row.Cells["ColPreview"].Value = value?.ToString() ?? "(空值)";
                            break;
                        }
                    }
                }

                MessageHelper.MessageOK(this, "预览读取完成!", TType.Success);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"预览读取失败: {ex.Message}", ex);
                MessageHelper.MessageOK($"预览读取失败:\n{ex.Message}", TType.Error);
            }
            finally
            {
                btnPreview.Enabled = true;
                btnPreview.Text = "预览读取";
            }
        }

        /// <summary>
        /// 保存按钮点击
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                // 先更新 _parameter,再调用基类保存方法
                _parameter = GetCurrentParameters();

                // 调用基类的 SaveParameters() 方法
                // 基类会调用 CollectParameters() 来获取要保存的参数
                SaveParameters();

                // 设置对话框结果并关闭
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存参数失败", ex);
                MessageHelper.MessageOK(this, $"保存参数失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 数据网格单元格值变化
        /// </summary>
        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0) return;

            // 可以在这里添加值变化后的处理逻辑
        }

        /// <summary>
        /// 数据网格当前单元格脏状态变化
        /// </summary>
        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DataGridViewDefineVar.IsCurrentCellDirty)
            {
                DataGridViewDefineVar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is ArgumentException &&
                e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show($"输入的值无效，请从下拉列表中选择有效的选项。",
                              "数据错误",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                e.ThrowException = false;
            }
        }

        /// <summary>
        /// 单元格验证
        /// </summary>
        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_isLoading) return;

            // 验证单元格地址格式
            if (DataGridViewDefineVar.Columns[e.ColumnIndex].Name == "ColCellAddress")
            {
                var value = e.FormattedValue?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(value) && !_cellAddressRegex.IsMatch(value))
                {
                    e.Cancel = true;
                    MessageHelper.MessageOK(this, $"单元格地址格式不正确: {value}\n正确格式如: A1, B2, C3", TType.Warn);
                }
            }
        }

        #endregion

        #region 基类方法重写

        /// <summary>
        /// 从参数对象加载到表单
        /// </summary>
        protected override void LoadParametersFromWorkflow()
        {
            if (_parameter == null) return;

            _isLoading = true;

            try
            {
                txtSheetName.Text = _parameter.SheetName ?? "Sheet1";

                // 加载读取项
                DataGridViewDefineVar.Rows.Clear();

                if (_parameter.ReadItems != null && _parameter.ReadItems.Count > 0)
                {
                    foreach (var item in _parameter.ReadItems)
                    {
                        int rowIndex = DataGridViewDefineVar.Rows.Add();
                        var row = DataGridViewDefineVar.Rows[rowIndex];

                        row.Cells["ColCellAddress"].Value = item.CellAddress;
                        row.Cells["ColSaveToVar"].Value = item.SaveToVariable;
                        row.Cells["ColDataType"].Value = GetDataTypeDisplayName(item.DataType);
                        row.Cells["ColDefaultValue"].Value = item.DefaultValue;
                    }
                }
                else
                {
                    // 添加一个空行
                    AddNewRow();
                }
            }
            finally
            {
                _isLoading = false;
            }
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        protected override bool ValidateInput()
        {
            // 检查是否有读取项
            if (DataGridViewDefineVar.Rows.Count == 0 ||
                DataGridViewDefineVar.Rows.Cast<DataGridViewRow>().All(r => r.IsNewRow))
            {
                MessageHelper.MessageOK(this, "请至少添加一个读取项", TType.Warn);
                return false;
            }

            // 验证每一行的数据
            foreach (DataGridViewRow row in DataGridViewDefineVar.Rows)
            {
                if (row.IsNewRow) continue;

                // 检查单元格地址
                var cellAddress = row.Cells["ColCellAddress"].Value?.ToString()?.Trim();
                if (string.IsNullOrEmpty(cellAddress))
                {
                    MessageHelper.MessageOK(this, $"第 {row.Index + 1} 行的单元格地址不能为空", TType.Warn);
                    return false;
                }

                if (!_cellAddressRegex.IsMatch(cellAddress))
                {
                    MessageHelper.MessageOK(this, $"第 {row.Index + 1} 行的单元格地址格式不正确: {cellAddress}", TType.Warn);
                    return false;
                }

                // 检查保存到变量
                var saveToVar = row.Cells["ColSaveToVar"].Value?.ToString()?.Trim();
                if (string.IsNullOrEmpty(saveToVar))
                {
                    MessageHelper.MessageOK(this, $"第 {row.Index + 1} 行的目标变量不能为空", TType.Warn);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取数据类型的显示名称
        /// </summary>
        private string GetDataTypeDisplayName(CellDataType dataType)
        {
            return dataType switch
            {
                CellDataType.String => "字符串",
                CellDataType.Integer => "整数",
                CellDataType.Decimal => "小数",
                CellDataType.Boolean => "布尔",
                CellDataType.DateTime => "日期时间",
                _ => "字符串"
            };
        }

        /// <summary>
        /// 从显示名称获取数据类型
        /// </summary>
        private CellDataType GetDataTypeFromDisplayName(string displayName)
        {
            return displayName switch
            {
                "字符串" => CellDataType.String,
                "整数" => CellDataType.Integer,
                "小数" => CellDataType.Decimal,
                "布尔" => CellDataType.Boolean,
                "日期时间" => CellDataType.DateTime,
                _ => CellDataType.String
            };
        }

        #endregion

    }
}
using AntdUI;
using Google.Protobuf.WellKnownTypes;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 读取单元格参数配置表单 - 增强版
    /// 支持:
    /// 1. 多单元格批量读取
    /// 2. 读取结果保存到变量
    /// 3. 支持指定工作表
    /// 4. 实时预览功能
    /// 5. 单元格地址验证
    /// </summary>
    public partial class Form_ReadCells : BaseParameterForm, IParameterForm<Parameter_ReadCells>
    {
        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private Parameter_ReadCells _currentParameter;
        private bool _isLoading = false;

        // 单元格地址格式正则
        private readonly System.Text.RegularExpressions.Regex _cellAddressRegex =
            new(@"^[A-Z]+[0-9]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象属性
        /// </summary>
        public Parameter_ReadCells Parameter
        {
            get => GetCurrentParameters();
            set
            {
                _currentParameter = value ?? new Parameter_ReadCells();
                if (!DesignMode && !_isLoading && IsHandleCreated)
                {
                    LoadParametersToForm();
                }
            }
        }

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
                // 初始化数据网格
                InitializeDataGrid();

                // 初始化事件绑定
                BindEvents();

                // 设置默认值
                _currentParameter ??= new Parameter_ReadCells
                {
                    SheetName = "Sheet1",
                    ReadItems = []
                };

                // 加载已有变量到下拉框
                LoadVariables();

                // 添加默认行
                if (_currentParameter.ReadItems.Count == 0)
                {
                    AddNewRow();
                }
            }
            finally
            {
                _isLoading = false;
            }
        }

        /// <summary>
        /// 初始化数据网格
        /// </summary>
        private void InitializeDataGrid()
        {
            // 清空现有列
            DataGridViewDefineVar.Columns.Clear();

            // 设置网格样式
            DataGridViewDefineVar.AllowUserToAddRows = false;
            DataGridViewDefineVar.AllowUserToDeleteRows = false;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDefineVar.MultiSelect = false;
            DataGridViewDefineVar.RowHeadersVisible = false;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.GridColor = Color.FromArgb(224, 224, 224);
            DataGridViewDefineVar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 添加列
            // 1. 单元格地址列
            var colCellAddress = new DataGridViewTextBoxColumn
            {
                Name = "ColCellAddress",
                HeaderText = "单元格地址",
                DataPropertyName = "CellAddress",
                Width = 120,
                MinimumWidth = 100
            };
            DataGridViewDefineVar.Columns.Add(colCellAddress);

            // 2. 保存到变量列（下拉框）
            var colSaveToVar = new DataGridViewComboBoxColumn
            {
                Name = "ColSaveToVar",
                HeaderText = "保存到变量",
                DataPropertyName = "SaveToVariable",
                Width = 150,
                MinimumWidth = 120
            };
            DataGridViewDefineVar.Columns.Add(colSaveToVar);

            // 3. 数据类型列（下拉框）
            var colDataType = new DataGridViewComboBoxColumn
            {
                Name = "ColDataType",
                HeaderText = "数据类型",
                DataPropertyName = "DataType",
                Width = 100,
                MinimumWidth = 80
            };
            colDataType.Items.AddRange("字符串", "整数", "小数", "布尔", "日期时间");
            DataGridViewDefineVar.Columns.Add(colDataType);

            // 4. 默认值列
            var colDefaultValue = new DataGridViewTextBoxColumn
            {
                Name = "ColDefaultValue",
                HeaderText = "默认值(可选)",
                DataPropertyName = "DefaultValue",
                Width = 120,
                MinimumWidth = 100
            };
            DataGridViewDefineVar.Columns.Add(colDefaultValue);

            // 5. 描述列
            var colDescription = new DataGridViewTextBoxColumn
            {
                Name = "ColDescription",
                HeaderText = "说明",
                DataPropertyName = "Description",
                Width = 200,
                MinimumWidth = 150
            };
            DataGridViewDefineVar.Columns.Add(colDescription);

            // 6. 预览值列（只读）
            var colPreview = new DataGridViewTextBoxColumn
            {
                Name = "ColPreview",
                HeaderText = "预览值",
                DataPropertyName = "PreviewValue",
                Width = 150,
                MinimumWidth = 120,
                ReadOnly = true
            };
            colPreview.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            colPreview.DefaultCellStyle.ForeColor = Color.Gray;
            DataGridViewDefineVar.Columns.Add(colPreview);

            // 设置列头样式
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewDefineVar.ColumnHeadersHeight = 40;

            // 设置行样式
            DataGridViewDefineVar.RowTemplate.Height = 35;
            DataGridViewDefineVar.DefaultCellStyle.Font = new Font("微软雅黑", 9F);
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

            var variables = _variableManager.GetAllVariables()
                .Select(v => v.VarName)
                .OrderBy(n => n)
                .ToList();

            // 更新保存到变量列的下拉选项
            var colSaveToVar = DataGridViewDefineVar.Columns["ColSaveToVar"] as DataGridViewComboBoxColumn;
            if (colSaveToVar != null)
            {
                colSaveToVar.Items.Clear();
                colSaveToVar.Items.AddRange([.. variables]);
            }
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
                txtSheetName.Text = _currentParameter.SheetName ?? "Sheet1";

                // 加载读取项
                DataGridViewDefineVar.Rows.Clear();

                if (_currentParameter.ReadItems != null && _currentParameter.ReadItems.Count > 0)
                {
                    foreach (var item in _currentParameter.ReadItems)
                    {
                        int rowIndex = DataGridViewDefineVar.Rows.Add();
                        var row = DataGridViewDefineVar.Rows[rowIndex];

                        row.Cells["ColCellAddress"].Value = item.CellAddress;
                        row.Cells["ColSaveToVar"].Value = item.SaveToVariable;
                        row.Cells["ColDataType"].Value = GetDataTypeDisplayName(item.DataType);
                        row.Cells["ColDefaultValue"].Value = item.DefaultValue;
                        row.Cells["ColDescription"].Value = item.Description;
                        row.Cells["ColPreview"].Value = item.PreviewValue;
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
                    DefaultValue = row.Cells["ColDefaultValue"].Value?.ToString(),
                    Description = row.Cells["ColDescription"].Value?.ToString(),
                    PreviewValue = row.Cells["ColPreview"].Value?.ToString()
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
                MessageHelper.MessageOK("请选择要删除的行", TType.Warn);
                return;
            }

            if (MessageHelper.MessageYes(this, "确定要删除选中的行吗?") == DialogResult.OK)
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
            if (!ValidateParameters())
            {
                return;
            }

            try
            {
                btnPreview.Enabled = false;
                btnPreview.Text = "读取中...";

                var param = GetCurrentParameters();

                // 调用读取方法
                var reportMethods = Program.ServiceProvider.GetService<ReportMethods>();
                if (reportMethods == null)
                {
                    MessageHelper.MessageOK("报表服务未初始化", TType.Error);
                    return;
                }

                // 逐个读取并更新预览值
                foreach (var item in param.ReadItems)
                {
                    var readParam = new Parameter_ReadCells
                    {
                        SheetName = param.SheetName,
                    };

                    var value = await reportMethods.ReadCells(readParam);

                    // 更新对应行的预览值
                    foreach (DataGridViewRow row in DataGridViewDefineVar.Rows)
                    {
                        if (row.Cells["ColCellAddress"].Value?.ToString() == item.CellAddress)
                        {
                            row.Cells["ColPreview"].Value = value?.ToString() ?? "(空值)";
                            break;
                        }
                    }
                }

                MessageHelper.MessageOK("预览读取完成!", TType.Success);
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
            if (ValidateParameters())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// 取消按钮点击
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 单元格值变化时立即触发更新
        /// </summary>
        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DataGridViewDefineVar.IsCurrentCellDirty)
            {
                DataGridViewDefineVar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// 单元格值变化处理
        /// </summary>
        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0) return;

            // 如果是单元格地址列变化,验证格式
            if (e.ColumnIndex == DataGridViewDefineVar.Columns["ColCellAddress"].Index)
            {
                var cellValue = DataGridViewDefineVar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (!string.IsNullOrEmpty(cellValue) && !ValidateCellAddress(cellValue))
                {
                    DataGridViewDefineVar.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightPink;
                }
                else
                {
                    DataGridViewDefineVar.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// 单元格验证
        /// </summary>
        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_isLoading) return;

            // 验证单元格地址格式
            if (e.ColumnIndex == DataGridViewDefineVar.Columns["ColCellAddress"].Index)
            {
                var value = e.FormattedValue?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(value) && !ValidateCellAddress(value))
                {
                    MessageHelper.MessageOK($"单元格地址格式不正确!\n正确格式: A1, B2, C3...", TType.Warn);
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region 验证方法

        /// <summary>
        /// 验证参数
        /// </summary>
        protected override bool ValidateParameters()
        {
            // 验证至少有一个读取项
            if (DataGridViewDefineVar.Rows.Count == 0)
            {
                MessageHelper.MessageOK("请至少添加一个读取配置", TType.Warn);
                return false;
            }

            // 验证每一行
            for (int i = 0; i < DataGridViewDefineVar.Rows.Count; i++)
            {
                var row = DataGridViewDefineVar.Rows[i];
                if (row.IsNewRow) continue;

                // 验证单元格地址
                var cellAddress = row.Cells["ColCellAddress"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(cellAddress))
                {
                    MessageHelper.MessageOK($"第 {i + 1} 行:单元格地址不能为空", TType.Warn);
                    return false;
                }

                if (!ValidateCellAddress(cellAddress))
                {
                    MessageHelper.MessageOK($"第 {i + 1} 行:单元格地址格式不正确\n正确格式: A1, B2, C3...", TType.Warn);
                    return false;
                }

                // 验证保存到变量
                var saveToVar = row.Cells["ColSaveToVar"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(saveToVar))
                {
                    MessageHelper.MessageOK($"第 {i + 1} 行:请选择保存到的变量", TType.Warn);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 验证单元格地址格式
        /// </summary>
        private bool ValidateCellAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return false;

            return _cellAddressRegex.IsMatch(address.Trim());
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取数据类型显示名称
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

        /// <summary>
        /// 重写基类方法 - 收集参数
        /// </summary>
        protected override object CollectParameters()
        {
            return GetCurrentParameters();
        }

        public void PopulateControls(Parameter_ReadCells parameter)
        {
            throw new NotImplementedException();
        }

        void IParameterForm<Parameter_ReadCells>.SetDefaultValues()
        {
            SetDefaultValues();
        }

        public bool ValidateTypedParameters()
        {
            throw new NotImplementedException();
        }

        public Parameter_ReadCells CollectTypedParameters()
        {
            throw new NotImplementedException();
        }

        public Parameter_ReadCells ConvertParameter(object stepParameter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
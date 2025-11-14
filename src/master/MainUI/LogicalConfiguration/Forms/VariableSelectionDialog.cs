using MainUI.LogicalConfiguration.LogicalManager;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 变量选择对话框 - 现代化版本
    /// 提供友好的变量选择界面,支持搜索、过滤和详细信息显示
    /// </summary>
    public partial class VariableSelectionDialog : UIForm
    {
        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private List<VariableInfo> _allVariables = [];
        private List<VariableInfo> _filteredVariables = [];
        private BindingSource _bindingSource = [];

        #endregion

        #region 内部类

        /// <summary>
        /// 变量信息数据模型
        /// </summary>
        public class VariableInfo
        {
            /// <summary>
            /// 变量名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 变量值
            /// </summary>
            public object Value { get; set; }

            /// <summary>
            /// 数据类型
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// 值预览(用于表格显示)
            /// </summary>
            public string ValuePreview => GetValuePreview();

            /// <summary>
            /// 完整值描述(用于详情面板)
            /// </summary>
            public string FullValueDescription => GetFullValueDescription();

            /// <summary>
            /// 获取值预览字符串
            /// </summary>
            private string GetValuePreview()
            {
                if (Value == null) return "null";

                var str = Value.ToString();
                return str.Length > 50 ? str.Substring(0, 47) + "..." : str;
            }

            /// <summary>
            /// 获取完整值描述
            /// </summary>
            private string GetFullValueDescription()
            {
                if (Value == null) return "null";

                var str = Value.ToString();
                if (str.Length <= 200) return str;

                return str.Substring(0, 197) + "...";
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 选中的变量名称
        /// </summary>
        public string SelectedVariableName { get; private set; }

        /// <summary>
        /// 选中的变量信息
        /// </summary>
        public VariableInfo SelectedVariable { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="variableManager">全局变量管理器</param>
        public VariableSelectionDialog(GlobalVariableManager variableManager)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));

            InitializeComponent();
            InitializeDataGridView();
            LoadVariables();

            ApplyFilter();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化 DataGridView 设置
        /// </summary>
        private void InitializeDataGridView()
        {
            // 绑定数据源
            dgvVariables.AutoGenerateColumns = false;
            dgvVariables.DataSource = _bindingSource;

            // 设置交替行颜色
            dgvVariables.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }

        /// <summary>
        /// 加载所有变量
        /// </summary>
        private void LoadVariables()
        {
            _allVariables.Clear();

            var variables = _variableManager?.GetAllVariables();
            if (variables != null)
            {
                foreach (var variable in variables)
                {
                    var varInfo = new VariableInfo
                    {
                        Name = variable.VarName,
                        Value = variable.VarValue,
                        Type = variable.VarType
                    };
                    _allVariables.Add(varInfo);
                }
            }

            _allVariables = _allVariables.OrderBy(v => v.Name).ToList();
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        private string GetTypeName(object value)
        {
            if (value == null) return "null";

            var type = value.GetType();

            if (type == typeof(string)) return "字符串";
            if (type == typeof(int) || type == typeof(long) || type == typeof(short)) return "整数";
            if (type == typeof(double) || type == typeof(float) || type == typeof(decimal)) return "小数";
            if (type == typeof(bool)) return "布尔值";
            if (type == typeof(DateTime)) return "日期时间";

            return type.Name;
        }

        #endregion

        #region 过滤和搜索

        /// <summary>
        /// 应用过滤条件
        /// </summary>
        private void ApplyFilter()
        {
            var searchText = txtSearch.Text.Trim().ToLower();
            var filterType = cmbFilter.Text;

            _filteredVariables = _allVariables.Where(v =>
            {
                // 搜索过滤
                if (!string.IsNullOrEmpty(searchText) &&
                    !v.Name.ToLower().Contains(searchText))
                {
                    return false;
                }

                // 类型过滤
                if (filterType != "全部变量")
                {
                    if (filterType == "字符串" && v.Type != "字符串") return false;
                    if (filterType == "数字" && v.Type != "整数" && v.Type != "小数") return false;
                    if (filterType == "日期时间" && v.Type != "日期时间") return false;
                    if (filterType == "布尔值" && v.Type != "布尔值") return false;
                    if (filterType == "其他" && (v.Type == "字符串" || v.Type == "整数" ||
                        v.Type == "小数" || v.Type == "日期时间" || v.Type == "布尔值"))
                    {
                        return false;
                    }
                }

                return true;
            }).ToList();

            // 更新数据源
            _bindingSource.DataSource = _filteredVariables;
            _bindingSource.ResetBindings(false);

            // 更新统计信息
            UpdateStatistics();

            // 清除选择
            if (dgvVariables.Rows.Count > 0)
            {
                dgvVariables.ClearSelection();
            }
        }

        /// <summary>
        /// 更新统计信息
        /// </summary>
        private void UpdateStatistics()
        {
            var total = _allVariables.Count;
            var filtered = _filteredVariables.Count;

            if (filtered == total)
            {
                lblStats.Text = $"共 {total} 个变量";
            }
            else
            {
                lblStats.Text = $"显示 {filtered} 个变量 (共 {total} 个)";
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void VariableSelectionDialog_Load(object sender, EventArgs e)
        {
            // 聚焦搜索框
            txtSearch.Focus();

            // 初始化详情面板
            UpdateDetailsPanel(null);
        }

        /// <summary>
        /// 搜索文本变化事件
        /// </summary>
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        /// <summary>
        /// 过滤器选择变化事件
        /// </summary>
        private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        /// <summary>
        /// DataGridView 选择变化事件
        /// </summary>
        private void DgvVariables_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVariables.SelectedRows.Count > 0)
            {
                var selectedRow = dgvVariables.SelectedRows[0];
                var variableInfo = selectedRow.DataBoundItem as VariableInfo;
                UpdateDetailsPanel(variableInfo);
            }
            else
            {
                UpdateDetailsPanel(null);
            }
        }

        /// <summary>
        /// DataGridView 双击事件
        /// </summary>
        private void DgvVariables_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ConfirmSelection();
            }
        }

        /// <summary>
        /// DataGridView 键盘事件
        /// </summary>
        private void DgvVariables_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                ConfirmSelection();
            }
        }

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            ConfirmSelection();
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 更新详情面板
        /// </summary>
        private void UpdateDetailsPanel(VariableInfo variable)
        {
            if (variable == null)
            {
                txtDetails.Text = "请选择一个变量查看详细信息";
                txtDetails.ForeColor = Color.Gray;
                btnOK.Enabled = false;
                return;
            }

            // 构建详细信息
            var details = $"变量名称: {variable.Name}\r\n";
            details += $"数据类型: {variable.Type}\r\n";
            details += $"当前值: {variable.FullValueDescription}";

            txtDetails.Text = details;
            txtDetails.ForeColor = Color.FromArgb(80, 80, 80);
            btnOK.Enabled = true;
        }

        /// <summary>
        /// 确认选择
        /// </summary>
        private void ConfirmSelection()
        {
            if (dgvVariables.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择一个变量", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvVariables.SelectedRows[0];
            var variableInfo = selectedRow.DataBoundItem as VariableInfo;

            if (variableInfo != null)
            {
                SelectedVariable = variableInfo;
                SelectedVariableName = variableInfo.Name;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        #endregion
    }
}
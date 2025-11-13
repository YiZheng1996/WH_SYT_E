using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 变量选择对话框
    /// 提供友好的变量选择界面，支持搜索和过滤
    /// </summary>
    public partial class VariableSelectionDialog : UIForm
    {
        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private List<VariableInfo> _allVariables = new();
        private List<VariableInfo> _filteredVariables = new();

        #endregion

        #region 内部类

        /// <summary>
        /// 变量信息
        /// </summary>
        public class VariableInfo
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public string Type { get; set; }
            public string DisplayText => $"{Name,-30} [{Type}] = {GetValuePreview()}";

            private string GetValuePreview()
            {
                if (Value == null) return "null";
                var str = Value.ToString();
                return str.Length > 50 ? str.Substring(0, 47) + "..." : str;
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

        public VariableSelectionDialog(GlobalVariableManager variableManager)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            InitializeComponent();
            InitializeForm();
        }

        #endregion

        #region 初始化

        private void InitializeForm()
        {
            // 初始化下拉框选项
            cmbFilter.Items.Clear();
            cmbFilter.Items.AddRange(["全部", "整数", "小数", "文本", "布尔", "日期时间"]);
            cmbFilter.SelectedIndex = 0;

            LoadVariables();
            UpdateVariablesList();
        }

        #endregion

        #region 私有方法

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
                        Type = GetVariableType(variable.VarValue)
                    };
                    _allVariables.Add(varInfo);
                }
            }

            _allVariables = _allVariables.OrderBy(v => v.Name).ToList();
        }

        private string GetVariableType(object value)
        {
            if (value == null) return "未知";

            return value.GetType().Name switch
            {
                "Int32" or "Int64" or "Int16" => "整数",
                "Double" or "Single" or "Decimal" => "小数",
                "String" => "文本",
                "Boolean" => "布尔",
                "DateTime" => "日期时间",
                _ => value.GetType().Name
            };
        }

        private void UpdateVariablesList()
        {
            lstVariables.Items.Clear();
            _filteredVariables.Clear();

            var searchText = txtSearch.Text?.Trim().ToLower() ?? "";
            var filterType = cmbFilter.SelectedItem?.ToString() ?? "全部";

            // 过滤变量
            foreach (var varInfo in _allVariables)
            {
                // 搜索过滤
                if (!string.IsNullOrEmpty(searchText) &&
                    !varInfo.Name.ToLower().Contains(searchText))
                {
                    continue;
                }

                // 类型过滤
                if (filterType != "全部" && varInfo.Type != filterType)
                {
                    continue;
                }

                _filteredVariables.Add(varInfo);
                lstVariables.Items.Add(varInfo.DisplayText);
            }

            // 更新统计信息
            lblStats.Text = $"共 {_filteredVariables.Count} 个变量";
            if (_filteredVariables.Count != _allVariables.Count)
            {
                lblStats.Text += $" (总计 {_allVariables.Count} 个)";
            }

            // 如果有结果，默认选中第一个
            if (lstVariables.Items.Count > 0)
            {
                lstVariables.SelectedIndex = 0;
            }
            else
            {
                UpdateDetailsPanel(null);
            }
        }

        private void UpdateDetailsPanel(VariableInfo varInfo)
        {
            if (varInfo == null)
            {
                lblDetails.Text = "请选择一个变量";
                lblDetails.ForeColor = System.Drawing.Color.Gray;
                return;
            }

            var details = $"变量名: {varInfo.Name}\n类型: {varInfo.Type}\n当前值: {varInfo.Value ?? "null"}";
            lblDetails.Text = details;
            lblDetails.ForeColor = System.Drawing.Color.Black;
        }

        #endregion

        #region 事件处理

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateVariablesList();
        }

        private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVariablesList();
        }

        private void LstVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVariables.SelectedIndex >= 0 && lstVariables.SelectedIndex < _filteredVariables.Count)
            {
                UpdateDetailsPanel(_filteredVariables[lstVariables.SelectedIndex]);
            }
        }

        private void LstVariables_DoubleClick(object sender, EventArgs e)
        {
            if (lstVariables.SelectedIndex >= 0)
            {
                ConfirmSelection();
            }
        }

        private void LstVariables_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lstVariables.SelectedIndex >= 0)
            {
                ConfirmSelection();
                e.Handled = true;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            ConfirmSelection();
        }

        private void ConfirmSelection()
        {
            if (lstVariables.SelectedIndex < 0 || lstVariables.SelectedIndex >= _filteredVariables.Count)
            {
                MessageHelper.MessageOK(this, "请选择一个变量", TType.Warn);
                return;
            }

            SelectedVariable = _filteredVariables[lstVariables.SelectedIndex];
            SelectedVariableName = SelectedVariable.Name;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}

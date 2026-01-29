using MainUI.LogicalConfiguration.Instrument.Models;
using CommandType = MainUI.LogicalConfiguration.Instrument.Models.CommandType;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    /// <summary>
    /// 命令模板编辑窗体
    /// </summary>
    public partial class FrmCommandEditor
    {
        #region 属性

        /// <summary>
        /// 编辑后的命令对象
        /// </summary>
        public InstrumentCommand Command { get; private set; }

        #endregion

        #region 构造函数

        public FrmCommandEditor(InstrumentCommand command)
        {
            InitializeComponent();
            InitializeFormData();
            BindEvents();
            InitializeGridColumns();

            if (command != null)
            {
                Command = command.Clone();
                LoadCommandToForm(command);
                this.Text = "编辑命令模板";
            }
            else
            {
                Command = new InstrumentCommand();
                this.Text = "新建命令模板";
            }
        }

        #endregion

        #region 初始化

        private void InitializeFormData()
        {
            // 初始化命令类型下拉框 - 显示Description
            cboCommandType.DataSource = EnumExtensions.GetEnumItems<CommandType>();
            cboCommandType.DisplayMember = "DisplayName";
            cboCommandType.ValueMember = "Value";
            cboCommandType.SelectedValue = CommandType.Query;

            // 初始化数据类型下拉框 - 显示Description
            cboDataType.DataSource = EnumExtensions.GetEnumItems<DataType>();
            cboDataType.DisplayMember = "DisplayName";
            cboDataType.ValueMember = "Value";
            cboDataType.SelectedValue = DataType.String;
        }

        private void BindEvents()
        {
            // 参数操作
            btnAddParam.Click += BtnAddParam_Click;
            btnDeleteParam.Click += BtnDeleteParam_Click;
            dgvParameters.CellDoubleClick += DgvParameters_CellDoubleClick;

            // 解析规则操作
            btnAddRule.Click += BtnAddRule_Click;
            btnDeleteRule.Click += BtnDeleteRule_Click;
            dgvParseRules.CellDoubleClick += DgvParseRules_CellDoubleClick;

            // 确定取消
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void InitializeGridColumns()
        {
            // 参数表格列
            dgvParameters.Columns.Clear();
            dgvParameters.Columns.Add("Name", "参数名");
            dgvParameters.Columns.Add("DisplayName", "显示名称");
            dgvParameters.Columns.Add("DataType", "数据类型");
            dgvParameters.Columns.Add("DefaultValue", "默认值");
            dgvParameters.Columns.Add("Required", "必填");

            // 解析规则表格列
            dgvParseRules.Columns.Clear();
            dgvParseRules.Columns.Add("Name", "规则名称");
            dgvParseRules.Columns.Add("TargetVariable", "目标变量");
            dgvParseRules.Columns.Add("ParseType", "解析方式");
            dgvParseRules.Columns.Add("Pattern", "解析参数");
        }

        #endregion

        #region 数据加载

        private void LoadCommandToForm(InstrumentCommand command)
        {
            txtName.Text = command.Name;
            txtDisplayName.Text = command.DisplayName;
            cboCommandType.SelectedValue = command.CommandType;
            cboDataType.SelectedValue = command.RequestDataType;
            txtTimeout.Text = command.Timeout.ToString();
            chkWaitForResponse.Checked = command.WaitForResponse;
            txtRequestTemplate.Text = command.RequestTemplate;
            txtSuccessIndicator.Text = command.SuccessIndicator;
            txtFailureIndicator.Text = command.FailureIndicator;
            txtDescription.Text = command.Description;

            LoadParameters(command.Parameters);
            LoadParseRules(command.ParseRules);
        }

        private void LoadParameters(List<CommandParameter> parameters)
        {
            dgvParameters.Rows.Clear();

            if (parameters == null)
                return;

            foreach (var param in parameters)
            {
                var rowIndex = dgvParameters.Rows.Add(
                    param.Name,
                    param.DisplayName,
                    param.DataType.ToString(),
                    param.DefaultValue,
                    param.Required ? "是" : "否"
                );
                dgvParameters.Rows[rowIndex].Tag = param;
            }
        }

        private void LoadParseRules(List<ResponseParseRule> rules)
        {
            dgvParseRules.Rows.Clear();

            if (rules == null)
                return;

            foreach (var rule in rules)
            {
                var pattern = GetParseRulePattern(rule);
                var rowIndex = dgvParseRules.Rows.Add(
                    rule.Name,
                    rule.TargetVariable,
                    rule.ParseType,
                    pattern
                );
                dgvParseRules.Rows[rowIndex].Tag = rule;
            }
        }

        private string GetParseRulePattern(ResponseParseRule rule)
        {
            return rule.ParseType switch
            {
                "Position" => $"起始:{rule.StartPosition}, 长度:{rule.Length}",
                "Delimiter" => $"分隔符:{rule.Delimiter}, 索引:{rule.SegmentIndex}",
                "Regex" => rule.RegexPattern,
                "Json" => rule.JsonPath,
                _ => ""
            };
        }

        #endregion

        #region 数据收集

        private void CollectFormToCommand()
        {
            Command.Name = txtName.Text.Trim();
            Command.DisplayName = txtDisplayName.Text.Trim();
            Command.CommandType = (CommandType)(cboCommandType.SelectedValue ?? CommandType.Query);
            Command.RequestDataType = (DataType)(cboDataType.SelectedValue ?? DataType.String);
            int.TryParse(txtTimeout.Text, out var timeout);
            Command.Timeout = timeout > 0 ? timeout : 3000;
            Command.WaitForResponse = chkWaitForResponse.Checked;
            Command.RequestTemplate = txtRequestTemplate.Text;
            Command.SuccessIndicator = txtSuccessIndicator.Text.Trim();
            Command.FailureIndicator = txtFailureIndicator.Text.Trim();
            Command.Description = txtDescription.Text.Trim();

            // 收集参数
            Command.Parameters.Clear();
            foreach (DataGridViewRow row in dgvParameters.Rows)
            {
                if (row.Tag is CommandParameter param)
                {
                    Command.Parameters.Add(param);
                }
            }

            // 收集解析规则
            Command.ParseRules.Clear();
            foreach (DataGridViewRow row in dgvParseRules.Rows)
            {
                if (row.Tag is ResponseParseRule rule)
                {
                    Command.ParseRules.Add(rule);
                }
            }
        }

        #endregion

        #region 参数操作事件

        private void BtnAddParam_Click(object sender, EventArgs e)
        {
            var param = new CommandParameter
            {
                Name = $"Param{dgvParameters.Rows.Count + 1}",
                DisplayName = $"参数{dgvParameters.Rows.Count + 1}",
                DataType = DataType.String,
                Required = false
            };

            var rowIndex = dgvParameters.Rows.Add(
                param.Name,
                param.DisplayName,
                param.DataType.ToString(),
                param.DefaultValue,
                param.Required ? "是" : "否"
            );
            dgvParameters.Rows[rowIndex].Tag = param;
            dgvParameters.Rows[rowIndex].Selected = true;
        }

        private void BtnDeleteParam_Click(object sender, EventArgs e)
        {
            if (dgvParameters.SelectedRows.Count == 0)
            {
                UIMessageTip.ShowWarning("请选择要删除的参数");
                return;
            }

            if (MessageHelper.MessageYes("确定要删除选中的参数吗？") != DialogResult.OK)
                return;

            dgvParameters.Rows.Remove(dgvParameters.SelectedRows[0]);
        }

        private void DgvParameters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // 允许直接编辑单元格
                var cell = dgvParameters.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvParameters.BeginEdit(true);
            }
        }

        #endregion

        #region 解析规则操作事件

        private void BtnAddRule_Click(object sender, EventArgs e)
        {
            var rule = new ResponseParseRule
            {
                Name = $"Rule{dgvParseRules.Rows.Count + 1}",
                TargetVariable = $"Result{dgvParseRules.Rows.Count + 1}",
                ParseType = "Position",          
                TargetDataType = DataType.String,
                StartPosition = 0,               
                Length = -1                      
            };

            var rowIndex = dgvParseRules.Rows.Add(
                rule.Name,
                rule.TargetVariable,
                rule.ParseType.ToString(),
                GetParseRulePattern(rule)
            );
            dgvParseRules.Rows[rowIndex].Tag = rule;
            dgvParseRules.Rows[rowIndex].Selected = true;
        }

        private void BtnDeleteRule_Click(object sender, EventArgs e)
        {
            if (dgvParseRules.SelectedRows.Count == 0)
            {
                MessageHelper.MessageOK(this, "请选择要删除的规则");
                return;
            }

            if (MessageHelper.MessageYes("确定要删除选中的规则吗？") != DialogResult.OK)
                return;

            dgvParseRules.Rows.Remove(dgvParseRules.SelectedRows[0]);
        }

        private void DgvParseRules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // 允许直接编辑单元格
            var cell = dgvParseRules.Rows[e.RowIndex].Cells[e.ColumnIndex];
            dgvParseRules.BeginEdit(true);
        }

        #endregion

        #region 确定取消事件

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            CollectFormToCommand();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                UIMessageTip.ShowWarning("请输入命令名称");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                UIMessageTip.ShowWarning("请输入显示名称");
                txtDisplayName.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtRequestTemplate.Text)) return true;

            UIMessageTip.ShowWarning("请输入请求模板");
            txtRequestTemplate.Focus();
            return false;

        }

        #endregion
    }
}
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
            cboCommandType.SelectedValue = CommandType.Read;

            // 初始化数据类型下拉框 - 显示Description
            cboDataType.DataSource = EnumExtensions.GetEnumItems<DataType>();
            cboDataType.DisplayMember = "DisplayName";
            cboDataType.ValueMember = "Value";
            cboDataType.SelectedValue = DataType.String;
        }

        private void BindEvents()
        {
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
            // 解析规则表格列
            dgvParseRules.Columns.Clear();
            dgvParseRules.Columns.Add("Name", "规则名称");
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

            LoadParseRules(command.ParseRules);
        }
        private void LoadParseRules(List<ResponseParseRule> rules)
        {
            dgvParseRules.Rows.Clear();
            if (rules == null) return;

            foreach (var rule in rules)
            {
                var rowIndex = dgvParseRules.Rows.Add(
                    rule.Name,
                    rule.ParseType.GetDescription(),
                    GetParseRulePattern(rule)
                );
                dgvParseRules.Rows[rowIndex].Tag = rule;
            }
        }
        private string GetParseRulePattern(ResponseParseRule rule)
        {
            return rule.ParseType switch
            {
                ParseType.Position => $"起始:{rule.StartPosition}, 长度:{rule.Length}",
                ParseType.Delimiter => $"分隔符:{rule.Delimiter}, 索引:{rule.SegmentIndex}",
                ParseType.Regex => rule.RegexPattern,
                ParseType.Json => rule.JsonPath,
                _ => ""
            };
        }

        #endregion

        #region 数据收集

        private void CollectFormToCommand()
        {
            Command.Name = txtName.Text.Trim();
            Command.DisplayName = txtDisplayName.Text.Trim();
            Command.CommandType = (CommandType)(cboCommandType.SelectedValue ?? CommandType.Read);
            Command.RequestDataType = (DataType)(cboDataType.SelectedValue ?? DataType.String);

            // 0 表示继承驱动层 ReadTimeout
            int.TryParse(txtTimeout.Text, out var timeout);
            Command.Timeout = timeout > 0 ? timeout : 0;

            Command.WaitForResponse = chkWaitForResponse.Checked;
            Command.RequestTemplate = txtRequestTemplate.Text;
            Command.SuccessIndicator = txtSuccessIndicator.Text.Trim();
            Command.FailureIndicator = txtFailureIndicator.Text.Trim();
            Command.Description = txtDescription.Text.Trim();

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

        #region 解析规则操作事件

        private void BtnAddRule_Click(object sender, EventArgs e)
        {
            using var frm = new FrmParseRuleEditor();
            var frmResult = VarHelper.ShowDialogWithOverlayEx(this, frm);
            if (frmResult != DialogResult.OK) return;
            AddParseRuleRow(frm.Rule);
        }

        private void BtnDeleteRule_Click(object sender, EventArgs e)
        {
            if (dgvParseRules.SelectedRows.Count == 0)
            {
                MessageHelper.MessageOK(this, "请选择要删除的规则");
                return;
            }

            if (MessageHelper.MessageYes(this, "确定要删除选中的规则吗？") != DialogResult.OK)
                return;

            dgvParseRules.Rows.Remove(dgvParseRules.SelectedRows[0]);
        }

        private void DgvParseRules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvParseRules.Rows[e.RowIndex];
            if (row.Tag is not ResponseParseRule) return;

            using var frm = new FrmParseRuleEditor();
            var frmResult = VarHelper.ShowDialogWithOverlayEx(this, frm);
            if (frmResult != DialogResult.OK) return;

            row.Tag = frm.Rule;
            RefreshParseRuleRow(row, frm.Rule);
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
                MessageHelper.MessageOK(this, "请输入命令名称");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageHelper.MessageOK(this, "请输入显示名称");
                txtDisplayName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRequestTemplate.Text))
            {
                MessageHelper.MessageOK(this, "请输入请求模板");
                txtRequestTemplate.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region 辅助方法

        private void RefreshParseRuleRow(DataGridViewRow row, ResponseParseRule rule)
        {
            row.Cells["Name"].Value = rule.Name;
            row.Cells["ParseType"].Value = rule.ParseType.GetDescription();
            row.Cells["Pattern"].Value = GetParseRulePattern(rule);
        }

        /// <summary>
        /// 向解析规则表格新增一行
        /// </summary>
        private void AddParseRuleRow(ResponseParseRule rule)
        {
            var rowIndex = dgvParseRules.Rows.Add(
                rule.Name,
                rule.ParseType,
                GetParseRulePattern(rule)
            );
            dgvParseRules.Rows[rowIndex].Tag = rule;
            dgvParseRules.Rows[rowIndex].Selected = true;
        }

        #endregion
    }
}
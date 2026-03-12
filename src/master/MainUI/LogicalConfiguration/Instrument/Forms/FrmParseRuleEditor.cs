using System.Text.RegularExpressions;
using MainUI.LogicalConfiguration.Instrument.Models;
using Sunny.UI;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    public partial class FrmParseRuleEditor : UIForm
    {
        #region 正则预设模板

        private static readonly (string DisplayName, string Pattern, string Hint)[] RegexTemplates =
        {
            ("— 选择常用模板 —",         "",                         ""),
            ("提取第一个数字（含小数/科学计数）", @"([+-]?\d+\.?\d*(?:[eE][+-]?\d+)?)", "适用: +1.234E+01 / 12.5 / -0.3"),
            ("提取冒号后的值",            @":\s*([^\r\n,;]+)",         "适用: VOLT:12.34 / TEMP:25.3°C"),
            ("空格前的第一段",            @"^(\S+)",                   "适用: 12.34 V / 1.5 OK"),
            ("提取括号内的内容",          @"\(([^)]+)\)",              "适用: VALUE(12.34) / RESULT(OK)"),
            ("去掉单位取纯数字",          @"([+-]?\d+\.?\d*)",         "适用: 12.34V / 1.5A / 25℃"),
            ("提取引号内的字符串",        @"""([^""]+)""",             "适用: STATUS:\"OK\" / NAME:\"CH1\""),
            ("提取第N个逗号段（改索引）", @"(?:[^,]*,){1}([^,]*)",     "适用: A,12.34,C 取第2段，{1}改为段数-1"),
        };

        #endregion

        #region 属性

        public ResponseParseRule Rule { get; private set; }

        /// <summary>
        /// 是否处于高级模式（决定正则选项是否可见）
        /// </summary>
        private bool _isAdvancedMode = false;

        #endregion

        #region 构造函数

        public FrmParseRuleEditor(ResponseParseRule rule = null)
        {
            InitializeComponent();
            InitFormData();
            BindEvents();
            ApplyAdvancedMode(); // 初始化时应用一次，隐藏正则选项

            if (rule != null)
            {
                Rule = CloneRule(rule);

                // 如果已有规则是正则类型，自动打开高级模式
                if (rule.ParseType == ParseType.Regex)
                {
                    _isAdvancedMode = true;
                    chkAdvancedMode.Checked = true;
                    ApplyAdvancedMode();
                }

                LoadToForm(rule);
                this.Text = "编辑解析规则";
            }
            else
            {
                Rule = new ResponseParseRule();
                this.Text = "添加解析规则";
                cboParseType.SelectedValue = ParseType.Position;
            }
        }

        #endregion

        #region 初始化

        private void InitFormData()
        {
            // 解析方式下拉
            cboParseType.DataSource = EnumExtensions.GetEnumItems<ParseType>();
            cboParseType.DisplayMember = "DisplayName";
            cboParseType.ValueMember = "Value";
            cboParseType.SelectedValue = ParseType.Position;

            // 结果类型下拉
            cboTargetDataType.DataSource = EnumExtensions.GetEnumItems<DataType>();
            cboTargetDataType.DisplayMember = "DisplayName";
            cboTargetDataType.ValueMember = "Value";
            cboTargetDataType.SelectedValue = DataType.String;

            // 正则预设模板下拉
            cboRegexTemplate.Items.Clear();
            foreach (var t in RegexTemplates)
                cboRegexTemplate.Items.Add(t.DisplayName);
            cboRegexTemplate.SelectedIndex = 0;
        }

        private void BindEvents()
        {
            cboParseType.SelectedIndexChanged += CboParseType_SelectedIndexChanged;
            chkAdvancedMode.CheckedChanged += ChkAdvancedMode_CheckedChanged;
            cboRegexTemplate.SelectedIndexChanged += CboRegexTemplate_SelectedIndexChanged;
            btnTestRegex.Click += BtnTestRegex_Click;
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region 高级模式

        /// <summary>
        /// 根据 _isAdvancedMode 状态，控制正则选项在下拉列表中的可见性
        /// </summary>
        private void ApplyAdvancedMode()
        {
            // 记录当前选中值
            var currentValue = cboParseType.SelectedValue is ParseType pt ? pt : ParseType.Position;

            // 重新绑定数据源，根据模式决定是否包含 Regex
            var items = EnumExtensions.GetEnumItems<ParseType>()
                .Where(i => _isAdvancedMode || (ParseType)i.Value != ParseType.Regex)
                .ToList();

            cboParseType.DataSource = null;
            cboParseType.DataSource = items;
            cboParseType.DisplayMember = "DisplayName";
            cboParseType.ValueMember = "Value";

            // 恢复选中值；若当前是 Regex 但关闭了高级模式，回退到 Position
            if (!_isAdvancedMode && currentValue == ParseType.Regex)
                cboParseType.SelectedValue = ParseType.Position;
            else
                cboParseType.SelectedValue = currentValue;

            // 高级模式说明标签
            lblAdvancedHint.Visible = _isAdvancedMode;
        }

        private void ChkAdvancedMode_CheckedChanged(object sender, EventArgs e)
        {
            _isAdvancedMode = chkAdvancedMode.Checked;
            ApplyAdvancedMode();
        }

        #endregion

        #region 数据加载

        private void LoadToForm(ResponseParseRule rule)
        {
            txtName.Text = rule.Name;
            cboParseType.SelectedValue = rule.ParseType;

            if (cboTargetDataType.Items.Count > 0)
                cboTargetDataType.SelectedValue = rule.TargetDataType;

            numScaleFactor.Value = rule.ScaleFactor;
            numOffset.Value = rule.Offset;

            numStartPosition.Value = rule.StartPosition;
            numLength.Value = rule.Length < 0 ? -1 : rule.Length;
            txtDelimiter.Text = rule.Delimiter;
            numSegmentIndex.Value = rule.SegmentIndex;
            txtRegexPattern.Text = rule.RegexPattern;
            numRegexGroup.Value = rule.RegexGroupIndex;
            txtJsonPath.Text = rule.JsonPath;
        }

        #endregion

        #region 事件处理

        private void CboParseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboParseType.SelectedValue is not ParseType type) return;

            panelPosition.Visible = type == ParseType.Position;
            panelDelimiter.Visible = type == ParseType.Delimiter;
            panelRegex.Visible = type == ParseType.Regex;
            panelJson.Visible = type == ParseType.Json;
        }

        /// <summary>
        /// 选择预设模板后，自动填充正则框并显示说明
        /// </summary>
        private void CboRegexTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cboRegexTemplate.SelectedIndex;
            if (idx <= 0 || idx >= RegexTemplates.Length) return; // 0 是占位项

            var template = RegexTemplates[idx];
            txtRegexPattern.Text = template.Pattern;
            lblRegexHint.Text = template.Hint; // 更新说明文字
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageHelper.MessageOK(this, "规则名称不能为空");
                txtName.Focus();
                return;
            }

            if (cboParseType.SelectedValue is not ParseType type)
            {
                UIMessageTip.ShowWarning("请选择解析方式");
                return;
            }

            if (type == ParseType.Regex && string.IsNullOrWhiteSpace(txtRegexPattern.Text))
            {
                MessageHelper.MessageOK(this, "正则表达式不能为空");
                txtRegexPattern.Focus();
                return;
            }

            if (type == ParseType.Json && string.IsNullOrWhiteSpace(txtJsonPath.Text))
            {
                MessageHelper.MessageOK(this, "JSON路径不能为空");
                txtJsonPath.Focus();
                return;
            }

            if (type == ParseType.Regex)
            {
                try { _ = new Regex(txtRegexPattern.Text); }
                catch
                {
                    MessageHelper.MessageOK(this, "正则表达式语法错误，请检查");
                    txtRegexPattern.Focus();
                    return;
                }
            }

            Rule.Name = txtName.Text.Trim();
            Rule.ParseType = type;
            Rule.TargetDataType = (DataType)(cboTargetDataType.SelectedValue ?? DataType.String);
            Rule.ScaleFactor = (double)numScaleFactor.Value;
            Rule.Offset = (double)numOffset.Value;
            Rule.StartPosition = (int)numStartPosition.Value;
            Rule.Length = (int)numLength.Value;
            Rule.Delimiter = txtDelimiter.Text.Length > 0 ? txtDelimiter.Text : ",";
            Rule.SegmentIndex = (int)numSegmentIndex.Value;
            Rule.RegexPattern = txtRegexPattern.Text;
            Rule.RegexGroupIndex = (int)numRegexGroup.Value;
            Rule.JsonPath = txtJsonPath.Text.Trim();

            DialogResult = DialogResult.OK;
        }

        private void BtnTestRegex_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRegexPattern.Text))
            {
                UIMessageTip.ShowWarning("请先填写正则表达式");
                return;
            }

            try { _ = new Regex(txtRegexPattern.Text); }
            catch (Exception ex)
            {
                UIMessageBox.ShowWarning($"正则表达式语法错误：\r\n{ex.Message}");
                return;
            }

            //using var frm = new FrmRegexTester(txtRegexPattern.Text, (int)numRegexGroup.Value);
            //frm.ShowDialog(this);
        }

        #endregion

        #region 辅助方法

        private static ResponseParseRule CloneRule(ResponseParseRule r) => new ResponseParseRule
        {
            Name = r.Name,
            ParseType = r.ParseType,
            StartPosition = r.StartPosition,
            Length = r.Length,
            Delimiter = r.Delimiter,
            SegmentIndex = r.SegmentIndex,
            RegexPattern = r.RegexPattern,
            RegexGroupIndex = r.RegexGroupIndex,
            JsonPath = r.JsonPath,
            TargetDataType = r.TargetDataType,
            ScaleFactor = r.ScaleFactor,
            Offset = r.Offset
        };

        #endregion
    }
}
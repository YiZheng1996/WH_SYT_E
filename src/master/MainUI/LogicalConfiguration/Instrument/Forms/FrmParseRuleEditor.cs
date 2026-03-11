using System.Text.RegularExpressions;
using MainUI.LogicalConfiguration.Instrument.Models;
using Newtonsoft.Json.Linq;
using Sunny.UI;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    /// <summary>
    /// 响应解析规则编辑窗体
    /// 根据解析类型（Position / Delimiter / Regex / Json）动态显示对应输入面板
    /// </summary>
    public partial class FrmParseRuleEditor : UIForm
    {
        #region 解析类型定义

        private static readonly (string Value, string Text)[] ParseTypeItems =
        {
            ("Position",  "位置截取  —  指定起始位置和长度"),
            ("Delimiter", "分隔符分割 — 按字符拆分取指定段"),
            ("Regex",     "正则表达式 — 用正则匹配提取内容"),
            ("Json",      "JSON路径   — 按点路径读取JSON字段"),
        };

        #endregion

        #region 属性

        /// <summary>
        /// 编辑完成后的解析规则对象
        /// </summary>
        public ResponseParseRule Rule { get; private set; }

        #endregion

        #region 构造函数

        public FrmParseRuleEditor(ResponseParseRule rule = null)
        {
            InitializeComponent();
            InitFormData();
            BindEvents();

            if (rule != null)
            {
                Rule = CloneRule(rule);
                LoadToForm(rule);
                this.Text = "编辑解析规则";
            }
            else
            {
                Rule = new ResponseParseRule();
                this.Text = "添加解析规则";
                cboParseType.SelectedIndex = 0; // 默认 Position
            }
        }

        #endregion

        #region 初始化

        private void InitFormData()
        {
            // 解析方式下拉 - 用 string[] 方式绑定，保持简单
            foreach (var item in ParseTypeItems)
                cboParseType.Items.Add(item.Text);

            // 结果类型下拉
            cboTargetDataType.DataSource = EnumExtensions.GetEnumItems<DataType>();
            cboTargetDataType.DisplayMember = "DisplayName";
            cboTargetDataType.ValueMember = "Value";
            cboTargetDataType.SelectedValue = DataType.String;
        }

        private void BindEvents()
        {
            cboParseType.SelectedIndexChanged += CboParseType_SelectedIndexChanged;
            btnTestRegex.Click += BtnTestRegex_Click;
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region 数据加载

        private void LoadToForm(ResponseParseRule rule)
        {
            txtName.Text = rule.Name;
            txtTargetVariable.Text = rule.TargetVariable;

            if (cboTargetDataType.Items.Count > 0)
                cboTargetDataType.SelectedValue = rule.TargetDataType;

            numScaleFactor.Value = rule.ScaleFactor;
            numOffset.Value = rule.Offset;

            // 选中解析类型
            int idx = Array.FindIndex(ParseTypeItems, t => t.Value == rule.ParseType);
            cboParseType.SelectedIndex = idx >= 0 ? idx : 0;

            // 填充各类型字段
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
            int idx = cboParseType.SelectedIndex;
            if (idx < 0 || idx >= ParseTypeItems.Length) return;

            string type = ParseTypeItems[idx].Value;
            panelPosition.Visible = type == "Position";
            panelDelimiter.Visible = type == "Delimiter";
            panelRegex.Visible = type == "Regex";
            panelJson.Visible = type == "Json";
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // ── 基础验证
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageHelper.MessageOK(this, $"规则名称不能为空{txtName}");
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTargetVariable.Text))
            {
                MessageHelper.MessageOK(this, $"目标变量不能为空{txtTargetVariable}");
                txtTargetVariable.Focus();
                return;
            }

            int idx = cboParseType.SelectedIndex;
            if (idx < 0)
            {
                UIMessageTip.ShowWarning("请选择解析方式");
                return;
            }

            string type = ParseTypeItems[idx].Value;

            // ── 各类型特定验证
            if (type == "Regex" && string.IsNullOrWhiteSpace(txtRegexPattern.Text))
            {
                MessageHelper.MessageOK(this, $"正则表达式不能为空{txtRegexPattern}");
                txtRegexPattern.Focus();
                return;
            }

            if (type == "Json" && string.IsNullOrWhiteSpace(txtJsonPath.Text))
            {
                MessageHelper.MessageOK(this, $"JSON路径不能为空{txtJsonPath}");
                txtJsonPath.Focus();
                return;
            }

            if (type == "Regex")
            {
                try { _ = new Regex(txtRegexPattern.Text); }
                catch
                {
                    MessageHelper.MessageOK(this, $"正则表达式语法错误，请检查{txtRegexPattern}");
                    txtRegexPattern.Focus();
                    return;
                }
            }

            // ── 写回 Rule
            Rule.Name = txtName.Text.Trim();
            Rule.TargetVariable = txtTargetVariable.Text.Trim();
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

        /// <summary>
        /// 正则测试：输入示例响应文本，实时看匹配结果
        /// </summary>
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
            TargetVariable = r.TargetVariable,
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
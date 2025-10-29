using AntdUI;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.LogicalManager;
using System.Text;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 增强版表达式构建器对话框
    /// 提供可视化的表达式构建功能，支持：
    /// 1. 变量选择和插入
    /// 2. 函数库和智能提示
    /// 3. 运算符快速输入
    /// 4. 实时表达式验证
    /// 5. 表达式预览和计算
    /// 6. 表达式历史记录
    /// 7. 常用表达式模板
    /// </summary>
    public partial class ExpressionBuilderDialog : UIForm
    {
        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private readonly ExpressionEngine _engine;

        // 表达式历史记录
        private readonly List<string> _expressionHistory = [];
        private int _historyIndex = -1;

        // 支持的运算符
        private readonly Dictionary<string, string> _operators = new()
        {
            { "+", "加法" },
            { "-", "减法" },
            { "*", "乘法" },
            { "/", "除法" },
            { "%", "取余" },
            { "==", "等于" },
            { "!=", "不等于" },
            { ">", "大于" },
            { "<", "小于" },
            { ">=", "大于等于" },
            { "<=", "小于等于" },
            { "&&", "逻辑与" },
            { "||", "逻辑或" },
            { "!", "逻辑非" },
            { "(", "左括号" },
            { ")", "右括号" }
        };

        // 支持的函数分类
        private readonly Dictionary<string, List<FunctionInfo>> _functions = new()
        {
            {
                "数学函数", new List<FunctionInfo>
                {
                    new("Math.Abs", "x", "绝对值", "Math.Abs(-5) → 5"),
                    new("Math.Max", "x, y", "最大值", "Math.Max(10, 20) → 20"),
                    new("Math.Min", "x, y", "最小值", "Math.Min(10, 20) → 10"),
                    new("Math.Round", "x, [digits]", "四舍五入", "Math.Round(3.14159, 2) → 3.14"),
                    new("Math.Floor", "x", "向下取整", "Math.Floor(3.7) → 3"),
                    new("Math.Ceiling", "x", "向上取整", "Math.Ceiling(3.2) → 4"),
                    new("Math.Sqrt", "x", "平方根", "Math.Sqrt(16) → 4"),
                    new("Math.Pow", "x, y", "幂运算", "Math.Pow(2, 3) → 8"),
                    new("Math.Sin", "x", "正弦值(弧度)", "Math.Sin(Math.PI/2) → 1"),
                    new("Math.Cos", "x", "余弦值(弧度)", "Math.Cos(0) → 1"),
                    new("Math.Tan", "x", "正切值(弧度)", "Math.Tan(Math.PI/4) → 1")
                }
            },
            {
                "字符串函数", new List<FunctionInfo>
                {
                    new("String.Length", "str", "字符串长度", "String.Length(\"Hello\") → 5"),
                    new("String.Substring", "str, start, [length]", "截取字符串", "String.Substring(\"Hello\", 0, 2) → \"He\""),
                    new("String.Contains", "str, value", "包含判断", "String.Contains(\"Hello\", \"ell\") → true"),
                    new("String.Replace", "str, old, new", "替换字符串", "String.Replace(\"Hello\", \"H\", \"J\") → \"Jello\""),
                    new("String.ToUpper", "str", "转大写", "String.ToUpper(\"hello\") → \"HELLO\""),
                    new("String.ToLower", "str", "转小写", "String.ToLower(\"HELLO\") → \"hello\""),
                    new("String.Trim", "str", "去除首尾空格", "String.Trim(\" Hello \") → \"Hello\""),
                    new("String.StartsWith", "str, prefix", "是否以指定字符开头", "String.StartsWith(\"Hello\", \"He\") → true"),
                    new("String.EndsWith", "str, suffix", "是否以指定字符结尾", "String.EndsWith(\"Hello\", \"lo\") → true")
                }
            },
            {
                "类型转换", new List<FunctionInfo>
                {
                    new("Convert.ToInt32", "value", "转换为整数", "Convert.ToInt32(\"123\") → 123"),
                    new("Convert.ToDouble", "value", "转换为浮点数", "Convert.ToDouble(\"3.14\") → 3.14"),
                    new("Convert.ToBoolean", "value", "转换为布尔值", "Convert.ToBoolean(\"true\") → true"),
                    new("Convert.ToString", "value", "转换为字符串", "Convert.ToString(123) → \"123\"")
                }
            },
            {
                "日期时间", new List<FunctionInfo>
                {
                    new("DateTime.Now", "", "当前时间", "DateTime.Now → 2025-01-15 10:30:00"),
                    new("DateTime.Today", "", "今天日期", "DateTime.Today → 2025-01-15"),
                    new("DateTime.AddDays", "date, days", "增加天数", "DateTime.Now.AddDays(7)"),
                    new("DateTime.AddHours", "date, hours", "增加小时", "DateTime.Now.AddHours(2)"),
                    new("DateTime.ToString", "date, format", "格式化日期", "DateTime.Now.ToString(\"yyyy-MM-dd\")"),
                    new("DateTime.Year", "date", "获取年份", "DateTime.Now.Year → 2025"),
                    new("DateTime.Month", "date", "获取月份", "DateTime.Now.Month → 1"),
                    new("DateTime.Day", "date", "获取日期", "DateTime.Now.Day → 15")
                }
            },
            {
                "条件逻辑", new List<FunctionInfo>
                {
                    new("IF", "condition, trueValue, falseValue", "条件判断", "IF({Var1} > 10, \"大\", \"小\")"),
                    new("ISNULL", "value, defaultValue", "空值判断", "ISNULL({Var1}, 0)"),
                    new("ISEMPTY", "str", "空字符串判断", "ISEMPTY({Var1}) → true/false")
                }
            }
        };

        // 常用表达式模板
        private readonly Dictionary<string, string> _templates = new()
        {
            { "简单赋值", "{变量名}" },
            { "数值计算", "{Var1} + {Var2}" },
            { "百分比计算", "{Var1} / {Var2} * 100" },
            { "条件判断", "IF({Var1} > 10, {Var2}, {Var3})" },
            { "字符串拼接", "{Var1} + \" - \" + {Var2}" },
            { "当前日期", "DateTime.Now.ToString(\"yyyy-MM-dd\")" },
            { "当前时间", "DateTime.Now.ToString(\"HH:mm:ss\")" },
            { "四舍五入", "Math.Round({Var1}, 2)" },
            { "最大值", "Math.Max({Var1}, {Var2})" },
            { "最小值", "Math.Min({Var1}, {Var2})" },
            { "绝对值", "Math.Abs({Var1})" },
            { "类型转换", "Convert.ToDouble({Var1})" },
            { "空值处理", "ISNULL({Var1}, 0)" }
        };

        #endregion

        #region 属性

        /// <summary>
        /// 初始表达式
        /// </summary>
        public string InitialExpression { get; set; }

        /// <summary>
        /// 目标变量类型
        /// </summary>
        public string TargetVariableType { get; set; }

        /// <summary>
        /// 生成的表达式
        /// </summary>
        public string GeneratedExpression { get; private set; }

        #endregion

        #region 构造函数

        public ExpressionBuilderDialog(GlobalVariableManager variableManager, ExpressionEngine validator)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _engine = validator ?? throw new ArgumentNullException(nameof(validator));

            InitializeComponent();
            InitializeExpressionBuilder();
        }

        #endregion

        #region 界面初始化

        /// <summary>
        /// 初始化表达式构建器界面
        /// </summary>
        private void InitializeExpressionBuilder()
        {
            // 绑定事件
            BindEvents();

            // 初始化数据
            LoadVariables();
            LoadFunctionsList();
            LoadOperatorsList();
            LoadTemplates();

            // 设置初始表达式
            if (!string.IsNullOrWhiteSpace(InitialExpression))
            {
                txtExpression.Text = InitialExpression;
                ValidateExpression();
            }

            // 设置提示信息
            ShowWelcomeMessage();
        }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载变量列表
        /// </summary>
        private void LoadVariables()
        {
            try
            {
                lstVariables.Items.Clear();

                var variables = _variableManager.GetAllVariables();
                if (variables == null || variables.Count == 0)
                {
                    lstVariables.Items.Add("💡 暂无可用变量");
                    lstVariables.Items.Add("请先在工作流中定义变量");
                    return;
                }

                // 按类型分组显示
                var groupedVars = variables.GroupBy(v => v.VarType);
                foreach (var group in groupedVars.OrderBy(g => g.Key))
                {
                    lstVariables.Items.Add($"━━ {group.Key} 类型 ━━");
                    foreach (var variable in group)
                    {
                        var currentValue = variable.VarValue ?? "null";
                        var displayText = $"  {variable.VarName} = {currentValue}";
                        lstVariables.Items.Add(displayText);
                    }
                }
            }
            catch (Exception ex)
            {
                lstVariables.Items.Clear();
                lstVariables.Items.Add("❌ 加载变量失败");
                lstVariables.Items.Add($"错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 加载函数列表
        /// </summary>
        private void LoadFunctionsList()
        {
            try
            {
                lstFunctions.Items.Clear();

                foreach (var category in _functions)
                {
                    lstFunctions.Items.Add($"━━━ {category.Key} ━━━");
                    foreach (var func in category.Value)
                    {
                        var displayText = $"  {func.Name}({func.Parameters})";
                        lstFunctions.Items.Add(displayText);
                    }
                    lstFunctions.Items.Add(""); // 空行分隔
                }
            }
            catch (Exception ex)
            {

                lstFunctions.Items.Clear();
                lstFunctions.Items.Add("❌ 加载函数失败");
            }
        }

        /// <summary>
        /// 加载运算符列表
        /// </summary>
        private void LoadOperatorsList()
        {
            try
            {
                lstOperators.Items.Clear();

                // 算术运算符
                lstOperators.Items.Add("━━━ 算术运算 ━━━");
                lstOperators.Items.Add("  + (加法)");
                lstOperators.Items.Add("  - (减法)");
                lstOperators.Items.Add("  * (乘法)");
                lstOperators.Items.Add("  / (除法)");
                lstOperators.Items.Add("  % (取余)");
                lstOperators.Items.Add("");

                // 比较运算符
                lstOperators.Items.Add("━━━ 比较运算 ━━━");
                lstOperators.Items.Add("  == (等于)");
                lstOperators.Items.Add("  != (不等于)");
                lstOperators.Items.Add("  > (大于)");
                lstOperators.Items.Add("  < (小于)");
                lstOperators.Items.Add("  >= (大于等于)");
                lstOperators.Items.Add("  <= (小于等于)");
                lstOperators.Items.Add("");

                // 逻辑运算符
                lstOperators.Items.Add("━━━ 逻辑运算 ━━━");
                lstOperators.Items.Add("  && (逻辑与)");
                lstOperators.Items.Add("  || (逻辑或)");
                lstOperators.Items.Add("  ! (逻辑非)");
                lstOperators.Items.Add("");

                // 其他
                lstOperators.Items.Add("━━━ 其他 ━━━");
                lstOperators.Items.Add("  ( ) (括号)");
            }
            catch (Exception ex)
            {
                lstOperators.Items.Clear();
                lstOperators.Items.Add("❌ 加载运算符失败");
            }
        }

        /// <summary>
        /// 加载模板列表
        /// </summary>
        private void LoadTemplates()
        {
            if (cmbTemplates != null)
            {
                cmbTemplates.Items.Clear();
                cmbTemplates.Items.Add("选择模板...");
                foreach (var template in _templates)
                {
                    cmbTemplates.Items.Add($"{template.Key}: {template.Value}");
                }
                cmbTemplates.SelectedIndex = 0;
            }
        }

        #endregion

        #region 事件绑定

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            // 表达式文本变化事件
            txtExpression.TextChanged += TxtExpression_TextChanged;
            txtExpression.KeyDown += TxtExpression_KeyDown;

            // 列表双击事件
            lstVariables.DoubleClick += LstVariables_DoubleClick;
            lstFunctions.DoubleClick += LstFunctions_DoubleClick;
            lstOperators.DoubleClick += LstOperators_DoubleClick;

            // 列表键盘事件
            lstVariables.KeyDown += Lst_KeyDown;
            lstFunctions.KeyDown += Lst_KeyDown;
            lstOperators.KeyDown += Lst_KeyDown;

            // 按钮点击事件
            btnValidate.Click += (s, e) => ValidateExpression();
            btnClear.Click += BtnClear_Click;
            btnUndo.Click += BtnUndo_Click;
            btnRedo.Click += BtnRedo_Click;
            btnHelp.Click += BtnHelp_Click;
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            // 模板选择事件
            if (cmbTemplates != null)
            {
                cmbTemplates.SelectedIndexChanged += CmbTemplates_SelectedIndexChanged;
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 表达式文本变化事件
        /// </summary>
        private void TxtExpression_TextChanged(object sender, EventArgs e)
        {
            // 延迟验证,避免频繁验证
            if (_validationTimer != null)
            {
                _validationTimer.Stop();
                _validationTimer.Start();
            }
        }

        /// <summary>
        /// 表达式输入框键盘事件
        /// </summary>
        private void TxtExpression_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl+Z 撤销
            if (e.Control && e.KeyCode == Keys.Z)
            {
                BtnUndo_Click(sender, e);
                e.Handled = true;
            }
            // Ctrl+Y 重做
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                BtnRedo_Click(sender, e);
                e.Handled = true;
            }
            // Ctrl+Enter 验证
            else if (e.Control && e.KeyCode == Keys.Enter)
            {
                ValidateExpression();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 列表键盘事件
        /// </summary>
        private void Lst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender == lstVariables)
                    LstVariables_DoubleClick(sender, e);
                else if (sender == lstFunctions)
                    LstFunctions_DoubleClick(sender, e);
                else if (sender == lstOperators)
                    LstOperators_DoubleClick(sender, e);

                e.Handled = true;
            }
        }

        /// <summary>
        /// 变量列表双击事件
        /// </summary>
        private void LstVariables_DoubleClick(object sender, EventArgs e)
        {
            if (lstVariables.SelectedItem?.ToString() is string selectedText &&
                !selectedText.Contains("━") &&
                !selectedText.Contains("暂无") &&
                !selectedText.Contains("加载失败"))
            {
                // 提取变量名 (格式: "  VarName = value")
                var parts = selectedText.Trim().Split('=');
                if (parts.Length > 0)
                {
                    var varName = parts[0].Trim();
                    InsertTextAtCursor($"{{{varName}}}");
                }
            }
        }

        /// <summary>
        /// 函数列表双击事件
        /// </summary>
        private void LstFunctions_DoubleClick(object sender, EventArgs e)
        {
            if (lstFunctions.SelectedItem?.ToString() is string selectedText &&
                !selectedText.Contains("━") &&
                !string.IsNullOrWhiteSpace(selectedText))
            {
                // 提取函数名 (格式: "  FunctionName(params)")
                var text = selectedText.Trim();
                var openParenIndex = text.IndexOf('(');
                if (openParenIndex > 0)
                {
                    var funcName = text.Substring(0, openParenIndex);
                    InsertTextAtCursor($"{funcName}()");

                    // 将光标移到括号内
                    txtExpression.SelectionStart -= 1;
                }
            }
        }

        /// <summary>
        /// 运算符列表双击事件
        /// </summary>
        private void LstOperators_DoubleClick(object sender, EventArgs e)
        {
            if (lstOperators.SelectedItem?.ToString() is string selectedText &&
                !selectedText.Contains("━") &&
                !string.IsNullOrWhiteSpace(selectedText))
            {
                // 提取运算符 (格式: "  + (加法)")
                var text = selectedText.Trim();
                var spaceIndex = text.IndexOf(' ');
                if (spaceIndex > 0)
                {
                    var op = text.Substring(0, spaceIndex);
                    InsertTextAtCursor($" {op} ");
                }
            }
        }

        /// <summary>
        /// 模板选择变化事件
        /// </summary>
        private void CmbTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTemplates.SelectedIndex > 0)
            {
                var selectedTemplate = cmbTemplates.SelectedItem.ToString();
                var colonIndex = selectedTemplate.IndexOf(':');
                if (colonIndex > 0)
                {
                    var template = selectedTemplate.Substring(colonIndex + 1).Trim();
                    txtExpression.Text = template;
                    txtExpression.SelectionStart = txtExpression.Text.Length;
                    ValidateExpression();
                }
            }
        }

        /// <summary>
        /// 清除按钮点击
        /// </summary>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            var result = MessageHelper.MessageYes(this, "确定要清除当前表达式吗?");
            if (result == DialogResult.OK)
            {
                SaveToHistory();
                txtExpression.Clear();
                rtbPreview.Clear();
                lblValidationResult.Text = "验证结果: 等待输入";
                lblValidationResult.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// 撤销按钮点击
        /// </summary>
        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (_historyIndex > 0)
            {
                _historyIndex--;
                txtExpression.Text = _expressionHistory[_historyIndex];
                ValidateExpression();
            }
        }

        /// <summary>
        /// 重做按钮点击
        /// </summary>
        private void BtnRedo_Click(object sender, EventArgs e)
        {
            if (_historyIndex < _expressionHistory.Count - 1)
            {
                _historyIndex++;
                txtExpression.Text = _expressionHistory[_historyIndex];
                ValidateExpression();
            }
        }

        /// <summary>
        /// 帮助按钮点击
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            var helpText = new StringBuilder();
            helpText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            helpText.AppendLine("📖 表达式构建器 - 使用帮助");
            helpText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            helpText.AppendLine();
            helpText.AppendLine("🔹 基本操作:");
            helpText.AppendLine("  • 双击列表项插入到表达式");
            helpText.AppendLine("  • 或选中后按 Enter 键插入");
            helpText.AppendLine("  • 支持直接输入编辑");
            helpText.AppendLine();
            helpText.AppendLine("🔹 变量引用:");
            helpText.AppendLine("  • 使用 {变量名} 格式引用变量");
            helpText.AppendLine("  • 示例: {Var1} + {Var2}");
            helpText.AppendLine();
            helpText.AppendLine("🔹 常用表达式:");
            helpText.AppendLine("  • 数值计算: {Var1} * 2 + 10");
            helpText.AppendLine("  • 条件判断: IF({Var1} > 10, \"大\", \"小\")");
            helpText.AppendLine("  • 字符串: {Var1} + \" - \" + {Var2}");
            helpText.AppendLine("  • 日期时间: DateTime.Now.ToString(\"yyyy-MM-dd\")");
            helpText.AppendLine();
            helpText.AppendLine("🔹 快捷键:");
            helpText.AppendLine("  • Ctrl+Z: 撤销");
            helpText.AppendLine("  • Ctrl+Y: 重做");
            helpText.AppendLine("  • Ctrl+Enter: 验证表达式");
            helpText.AppendLine();
            helpText.AppendLine("🔹 提示:");
            helpText.AppendLine("  • 使用模板快速开始");
            helpText.AppendLine("  • 实时验证显示错误");
            helpText.AppendLine("  • 预览显示预期结果");
            helpText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            MessageHelper.MessageOK(this, helpText.ToString(), TType.Info);
        }

        /// <summary>
        /// 确定按钮点击
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            var expression = txtExpression.Text?.Trim();

            if (string.IsNullOrWhiteSpace(expression))
            {
                MessageHelper.MessageOK("表达式不能为空!", TType.Warn);
                return;
            }

            // 验证表达式
            var validationResult = _engine?.ValidateExpression(expression);

            if (validationResult?.IsValid != true)
            {
                var errors = validationResult?.Errors != null
                    ? string.Join("\n", validationResult.Errors.Select(e => e?.ToString() ?? ""))
                    : "验证失败";

                var result = MessageHelper.MessageYes($"表达式验证失败：\n{errors}\n\n是否仍要使用此表达式？");
                if (result != DialogResult.OK)
                {
                    return;
                }
            }

            SaveToHistory();
            GeneratedExpression = expression;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 在光标位置插入文本
        /// </summary>
        private void InsertTextAtCursor(string text)
        {
            SaveToHistory();

            var selectionStart = txtExpression.SelectionStart;
            var currentText = txtExpression.Text;

            txtExpression.Text = currentText.Insert(selectionStart, text);
            txtExpression.SelectionStart = selectionStart + text.Length;
            txtExpression.Focus();

            ValidateExpression();
        }

        /// <summary>
        /// 保存到历史记录
        /// </summary>
        private void SaveToHistory()
        {
            var current = txtExpression.Text;
            if (_historyIndex == -1 || _expressionHistory[_historyIndex] != current)
            {
                // 如果不在最后,删除后面的历史
                if (_historyIndex < _expressionHistory.Count - 1)
                {
                    _expressionHistory.RemoveRange(_historyIndex + 1, _expressionHistory.Count - _historyIndex - 1);
                }

                _expressionHistory.Add(current);
                _historyIndex = _expressionHistory.Count - 1;

                // 限制历史记录数量
                if (_expressionHistory.Count > 50)
                {
                    _expressionHistory.RemoveAt(0);
                    _historyIndex--;
                }
            }
        }

        /// <summary>
        /// 验证表达式
        /// </summary>
        private void ValidateExpression()
        {
            try
            {
                var expression = txtExpression.Text?.Trim();

                if (string.IsNullOrWhiteSpace(expression))
                {
                    lblValidationResult.Text = "验证结果: 等待输入";
                    lblValidationResult.ForeColor = Color.Gray;
                    rtbPreview.Text = "💡 请输入表达式或选择模板开始";
                    return;
                }

                // 执行验证
                var validationResult = _engine?.ValidateExpression(expression);

                if (validationResult?.IsValid == true)
                {
                    lblValidationResult.Text = "验证结果: ✅ 表达式语法正确";
                    lblValidationResult.ForeColor = Color.FromArgb(40, 167, 69);

                    // 尝试计算预期值
                    var calcResult = _engine.CalculateExpectedValue(expression);
                    if (calcResult?.Success == true)
                    {
                        var preview = new StringBuilder();
                        preview.AppendLine("✅ 验证成功!");
                        preview.AppendLine();
                        preview.AppendLine("📊 预期结果:");
                        preview.AppendLine($"  {calcResult.Value ?? "null"}");
                        preview.AppendLine();
                        preview.AppendLine("💡 提示:");
                        preview.AppendLine("  表达式将在工作流执行时计算实际值");

                        rtbPreview.Text = preview.ToString();
                    }
                    else
                    {
                        rtbPreview.Text = $"⚠️ 语法正确但无法计算预期值\n\n错误: {calcResult?.ErrorMessage}";
                    }
                }
                else
                {
                    var errors = validationResult?.Errors != null
                        ? string.Join("; ", validationResult.Errors.Select(e => e?.ToString() ?? ""))
                        : "未知错误";

                    lblValidationResult.Text = $"验证结果: ❌ {errors}";
                    lblValidationResult.ForeColor = Color.FromArgb(220, 53, 69);

                    var errorDetail = new StringBuilder();
                    errorDetail.AppendLine("❌ 验证失败!");
                    errorDetail.AppendLine();
                    errorDetail.AppendLine("错误详情:");
                    if (validationResult?.Errors != null)
                    {
                        foreach (var error in validationResult.Errors)
                        {
                            errorDetail.AppendLine($"  • {error}");
                        }
                    }
                    errorDetail.AppendLine();
                    errorDetail.AppendLine("💡 建议:");
                    errorDetail.AppendLine("  • 检查变量名是否正确");
                    errorDetail.AppendLine("  • 检查函数调用格式");
                    errorDetail.AppendLine("  • 检查括号是否匹配");
                    errorDetail.AppendLine("  • 参考右侧的函数列表");

                    rtbPreview.Text = errorDetail.ToString();
                }
            }
            catch (Exception ex)
            {
                lblValidationResult.Text = $"验证结果: ❌ 验证异常: {ex.Message}";
                lblValidationResult.ForeColor = Color.FromArgb(220, 53, 69);
                rtbPreview.Text = $"❌ 验证异常\n\n{ex.Message}";
            }
        }

        /// <summary>
        /// 显示欢迎信息
        /// </summary>
        private void ShowWelcomeMessage()
        {
            if (string.IsNullOrWhiteSpace(txtExpression.Text))
            {
                var welcome = new StringBuilder();
                welcome.AppendLine("快速开始:");
                welcome.AppendLine("1️.从模板下拉框选择常用表达式");
                welcome.AppendLine("2️.或双击左侧列表插入变量/函数");
                welcome.AppendLine("3️.或直接输入表达式");
                welcome.AppendLine();
                welcome.AppendLine("提示:");
                welcome.AppendLine("• 变量使用 {变量名} 格式");
                welcome.AppendLine("• 支持数学运算和函数调用");
                welcome.AppendLine("• 实时验证和预览结果");
                welcome.AppendLine("💡点击右下角 [?] 查看详细帮助");

                rtbPreview.Text = welcome.ToString();
            }
        }

        #endregion

        #region 内部类

        /// <summary>
        /// 函数信息
        /// </summary>
        private class FunctionInfo
        {
            public string Name { get; set; }
            public string Parameters { get; set; }
            public string Description { get; set; }
            public string Example { get; set; }

            public FunctionInfo(string name, string parameters, string description, string example)
            {
                Name = name;
                Parameters = parameters;
                Description = description;
                Example = example;
            }
        }

        #endregion
    }
}
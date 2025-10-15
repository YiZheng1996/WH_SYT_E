using AntdUI;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.LogicalManager;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 表达式构建器对话框
    /// 提供可视化的表达式构建功能，支持变量选择、函数插入、运算符操作
    /// </summary>
    public partial class ExpressionBuilderDialog : UIForm
    {
        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private readonly ExpressionValidator _validator;

        // 支持的运算符
        private readonly string[] _operators =
        [
            "+ (加法)", "- (减法)", "* (乘法)", "/ (除法)", "% (取余)",
            "== (等于)", "!= (不等于)", "> (大于)", "< (小于)",
            ">= (大于等于)", "<= (小于等于)",
            "&& (逻辑与)", "|| (逻辑或)", "! (逻辑非)"
        ];

        // 支持的函数
        private readonly string[] _functions =
        [
            "Math.Abs(x) - 绝对值",
            "Math.Max(x,y) - 最大值",
            "Math.Min(x,y) - 最小值",
            "Math.Round(x) - 四舍五入",
            "Math.Floor(x) - 向下取整",
            "Math.Ceiling(x) - 向上取整",
            "Math.Sqrt(x) - 平方根",
            "Math.Sin(x) - 正弦值",
            "Math.Cos(x) - 余弦值",
            "String.Length(str) - 字符串长度",
            "String.Substring(str,start,length) - 截取字符串",
            "String.Contains(str,value) - 包含判断",
            "DateTime.Now - 当前时间",
            "Convert.ToInt32(value) - 转换为整数",
            "Convert.ToDouble(value) - 转换为浮点数"
        ];

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

        public ExpressionBuilderDialog(GlobalVariableManager variableManager, ExpressionValidator validator)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));

            InitializeComponent();
            InitializeExpressionBuilder();
            LoadVariables();
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
            LoadFunctionsList();
            LoadOperatorsList();

            // 设置初始表达式
            if (!string.IsNullOrWhiteSpace(InitialExpression))
            {
                txtExpression.Text = InitialExpression;
                ValidateExpression();
            }
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
                foreach (var variable in variables)
                {
                    var displayText = $"{variable.VarName} ({variable.VarType})";
                    lstVariables.Items.Add(displayText);
                }

                if (lstVariables.Items.Count == 0)
                {
                    lstVariables.Items.Add("(暂无可用变量)");
                }
            }
            catch (Exception ex)
            {
                lstVariables.Items.Clear();
                lstVariables.Items.Add("(加载变量失败)");
                Debug.WriteLine($"加载变量失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 加载函数列表
        /// </summary>
        private void LoadFunctionsList()
        {
            lstFunctions.Items.Clear();
            foreach (var func in _functions)
            {
                lstFunctions.Items.Add(func);
            }
        }

        /// <summary>
        /// 加载运算符列表
        /// </summary>
        private void LoadOperatorsList()
        {
            lstOperators.Items.Clear();
            foreach (var op in _operators)
            {
                lstOperators.Items.Add(op);
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
            txtExpression.TextChanged += (s, e) => ValidateExpression();

            // 列表双击事件
            lstVariables.DoubleClick += LstVariables_DoubleClick;
            lstFunctions.DoubleClick += LstFunctions_DoubleClick;
            lstOperators.DoubleClick += LstOperators_DoubleClick;

            // 按钮点击事件
            btnValidate.Click += (s, e) => ValidateExpression();
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 变量列表双击事件
        /// </summary>
        private void LstVariables_DoubleClick(object sender, EventArgs e)
        {
            if (lstVariables.SelectedItem?.ToString() is string selectedText &&
                !selectedText.Contains("(暂无") && !selectedText.Contains("(加载"))
            {
                // 提取变量名（去掉类型部分）
                var variableName = selectedText.Split(' ')[0];
                InsertTextAtCursor($"{{{variableName}}}");
            }
        }

        /// <summary>
        /// 函数列表双击事件
        /// </summary>
        private void LstFunctions_DoubleClick(object sender, EventArgs e)
        {
            if (lstFunctions.SelectedItem?.ToString() is string selectedText)
            {
                // 提取函数调用部分（去掉说明部分）
                var functionCall = selectedText.Split(' ')[0];
                InsertTextAtCursor(functionCall);
            }
        }

        /// <summary>
        /// 运算符列表双击事件
        /// </summary>
        private void LstOperators_DoubleClick(object sender, EventArgs e)
        {
            if (lstOperators.SelectedItem?.ToString() is string selectedText)
            {
                // 提取运算符部分
                var operatorSymbol = selectedText.Split(' ')[0];
                InsertTextAtCursor($" {operatorSymbol} ");
            }
        }

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            var expression = txtExpression.Text?.Trim();

            if (string.IsNullOrWhiteSpace(expression))
            {
                MessageHelper.MessageOK("请输入表达式", TType.Warn);
                return;
            }

            // 最终验证
            var validationResult = _validator?.ValidateExpression(expression);
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
            var selectionStart = txtExpression.SelectionStart;
            var currentText = txtExpression.Text;

            txtExpression.Text = currentText.Insert(selectionStart, text);
            txtExpression.SelectionStart = selectionStart + text.Length;
            txtExpression.Focus();
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
                    rtbPreview.Text = "";
                    return;
                }

                // 执行验证
                var validationResult = _validator?.ValidateExpression(expression);

                if (validationResult?.IsValid == true)
                {
                    lblValidationResult.Text = "验证结果: ✓ 表达式语法正确";
                    lblValidationResult.ForeColor = Color.Green;

                    // 尝试计算预期值
                    var calcResult = _validator.CalculateExpectedValue(expression);
                    if (calcResult?.Success == true)
                    {
                        rtbPreview.Text = $"预期结果:\n{calcResult.Value ?? "null"}";
                    }
                    else
                    {
                        rtbPreview.Text = $"预期结果:\n计算失败 - {calcResult?.ErrorMessage}";
                    }
                }
                else
                {
                    var errors = validationResult?.Errors != null
                        ? string.Join("; ", validationResult.Errors.Select(e => e?.ToString() ?? ""))
                        : "未知错误";
                    lblValidationResult.Text = $"验证结果: ✗ {errors}";
                    lblValidationResult.ForeColor = Color.Red;
                    rtbPreview.Text = "预期结果:\n验证失败，无法计算";
                }
            }
            catch (Exception ex)
            {
                lblValidationResult.Text = $"验证结果: ✗ 验证异常: {ex.Message}";
                lblValidationResult.ForeColor = Color.Red;
                rtbPreview.Text = "预期结果:\n验证异常";
            }
        }

        #endregion
    }
}

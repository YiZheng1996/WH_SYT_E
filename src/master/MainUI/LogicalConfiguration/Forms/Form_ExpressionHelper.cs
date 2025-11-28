using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 表达式助手窗体
    /// 用于可视化构建退出条件表达式
    /// </summary>
    public partial class Form_ExpressionHelper : Sunny.UI.UIForm
    {
        #region 属性

        /// <summary>
        /// 生成的表达式
        /// </summary>
        public string GeneratedExpression { get; private set; } = "";

        #endregion

        #region 私有字段

        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger<Form_ExpressionHelper> _logger;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="variableManager">全局变量管理器</param>
        /// <param name="currentExpression">当前表达式</param>
        public Form_ExpressionHelper(
            GlobalVariableManager variableManager,
            string currentExpression = "")
        {
            InitializeComponent();

            _variableManager = variableManager;

            // 加载变量列表
            LoadVariables();

            // 加载函数列表
            LoadFunctions();

            // 绑定事件
            BindEvents();

            // 加载当前表达式
            if (!string.IsNullOrWhiteSpace(currentExpression))
            {
                txtPreview.Text = currentExpression;
            }

            // 更新预览
            UpdatePreview();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 加载变量列表
        /// </summary>
        private void LoadVariables()
        {
            try
            {
                var variables = _variableManager?.GetAllVariables() ?? [];

                cmbLeftVariable.Items.Clear();
                cmbRightVariable.Items.Clear();

                foreach (var variable in variables)
                {
                    cmbLeftVariable.Items.Add(variable.VarName);
                    cmbRightVariable.Items.Add(variable.VarName);
                }

                // 默认选中第一个
                if (cmbLeftVariable.Items.Count > 0)
                {
                    cmbLeftVariable.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载变量列表失败");
            }
        }

        /// <summary>
        /// 加载函数列表
        /// </summary>
        private void LoadFunctions()
        {
            cmbFunction.Items.Clear();
            cmbFunction.Items.AddRange(new object[]
            {
                "无",
                "ABS - 绝对值",
                "MAX - 最大值",
                "MIN - 最小值",
                "SQRT - 平方根",
                "POW - 幂运算",
                "ROUND - 四舍五入"
            });
            cmbFunction.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            // 左侧操作数类型切换
            rdoLeftVar.CheckedChanged += (s, e) => UpdateLeftControls();
            rdoLeftExpr.CheckedChanged += (s, e) => UpdateLeftControls();

            // 右侧操作数类型切换
            rdoRightValue.CheckedChanged += (s, e) => UpdateRightControls();
            rdoRightVar.CheckedChanged += (s, e) => UpdateRightControls();
            rdoRightExpr.CheckedChanged += (s, e) => UpdateRightControls();

            // 值改变时更新预览
            cmbLeftVariable.SelectedIndexChanged += (s, e) => UpdatePreview();
            txtLeftExpression.TextChanged += (s, e) => UpdatePreview();
            cmbFunction.SelectedIndexChanged += (s, e) => UpdatePreview();

            // 运算符改变
            rdoOpGreater.CheckedChanged += (s, e) => { if (rdoOpGreater.Checked) UpdatePreview(); };
            rdoOpLess.CheckedChanged += (s, e) => { if (rdoOpLess.Checked) UpdatePreview(); };
            rdoOpGreaterEqual.CheckedChanged += (s, e) => { if (rdoOpGreaterEqual.Checked) UpdatePreview(); };
            rdoOpLessEqual.CheckedChanged += (s, e) => { if (rdoOpLessEqual.Checked) UpdatePreview(); };
            rdoOpEqual.CheckedChanged += (s, e) => { if (rdoOpEqual.Checked) UpdatePreview(); };
            rdoOpNotEqual.CheckedChanged += (s, e) => { if (rdoOpNotEqual.Checked) UpdatePreview(); };
            rdoOpAnd.CheckedChanged += (s, e) => { if (rdoOpAnd.Checked) UpdatePreview(); };
            rdoOpOr.CheckedChanged += (s, e) => { if (rdoOpOr.Checked) UpdatePreview(); };

            // 右侧值改变
            txtRightValue.TextChanged += (s, e) => UpdatePreview();
            cmbRightVariable.SelectedIndexChanged += (s, e) => UpdatePreview();
            txtRightExpression.TextChanged += (s, e) => UpdatePreview();

            // 按钮事件
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            btnClear.Click += BtnClear_Click;

            // 模板按钮
            btnTemplateBasic.Click += (s, e) => ApplyTemplate("basic");
            btnTemplateRange.Click += (s, e) => ApplyTemplate("range");
            btnTemplateError.Click += (s, e) => ApplyTemplate("error");
            btnTemplateLogic.Click += (s, e) => ApplyTemplate("logic");
        }

        #endregion

        #region 控件状态更新

        /// <summary>
        /// 更新左侧控件状态
        /// </summary>
        private void UpdateLeftControls()
        {
            if (rdoLeftVar.Checked)
            {
                cmbLeftVariable.Enabled = true;
                txtLeftExpression.Enabled = false;
            }
            else if (rdoLeftExpr.Checked)
            {
                cmbLeftVariable.Enabled = false;
                txtLeftExpression.Enabled = true;
            }

            UpdatePreview();
        }

        /// <summary>
        /// 更新右侧控件状态
        /// </summary>
        private void UpdateRightControls()
        {
            txtRightValue.Enabled = rdoRightValue.Checked;
            cmbRightVariable.Enabled = rdoRightVar.Checked;
            txtRightExpression.Enabled = rdoRightExpr.Checked;

            UpdatePreview();
        }

        #endregion

        #region 表达式生成

        /// <summary>
        /// 更新预览
        /// </summary>
        private void UpdatePreview()
        {
            try
            {
                string left = GetLeftOperand();
                string op = GetOperator();
                string right = GetRightOperand();

                if (string.IsNullOrWhiteSpace(left) || string.IsNullOrWhiteSpace(right))
                {
                    return;
                }

                // 应用函数
                left = ApplyFunction(left);

                // 生成表达式
                string expression = $"{left} {op} {right}";

                txtPreview.Text = expression;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新预览失败");
            }
        }

        /// <summary>
        /// 获取左侧操作数
        /// </summary>
        private string GetLeftOperand()
        {
            if (rdoLeftVar.Checked)
            {
                string varName = cmbLeftVariable.Text;
                return string.IsNullOrWhiteSpace(varName) ? "" : $"{{{varName}}}";
            }
            else if (rdoLeftExpr.Checked)
            {
                return txtLeftExpression.Text?.Trim() ?? "";
            }

            return "";
        }

        /// <summary>
        /// 获取运算符
        /// </summary>
        private string GetOperator()
        {
            if (rdoOpGreater.Checked) return ">";
            if (rdoOpLess.Checked) return "<";
            if (rdoOpGreaterEqual.Checked) return ">=";
            if (rdoOpLessEqual.Checked) return "<=";
            if (rdoOpEqual.Checked) return "==";
            if (rdoOpNotEqual.Checked) return "!=";
            if (rdoOpAnd.Checked) return "AND";
            if (rdoOpOr.Checked) return "OR";

            return ">=";
        }

        /// <summary>
        /// 获取右侧操作数
        /// </summary>
        private string GetRightOperand()
        {
            if (rdoRightValue.Checked)
            {
                return txtRightValue.Text?.Trim() ?? "";
            }
            else if (rdoRightVar.Checked)
            {
                string varName = cmbRightVariable.Text;
                return string.IsNullOrWhiteSpace(varName) ? "" : $"{{{varName}}}";
            }
            else if (rdoRightExpr.Checked)
            {
                return txtRightExpression.Text?.Trim() ?? "";
            }

            return "";
        }

        /// <summary>
        /// 应用函数包装
        /// </summary>
        private string ApplyFunction(string operand)
        {
            if (cmbFunction.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(operand))
            {
                return operand;
            }

            string funcText = cmbFunction.Text;
            string funcName = funcText.Split('-')[0].Trim();

            // 特殊处理需要两个参数的函数
            if (funcName == "MAX" || funcName == "MIN" || funcName == "POW")
            {
                return $"{funcName}({operand}, ?)";
            }

            return $"{funcName}({operand})";
        }

        #endregion

        #region 模板应用

        /// <summary>
        /// 应用模板
        /// </summary>
        private void ApplyTemplate(string templateType)
        {
            try
            {
                switch (templateType)
                {
                    case "basic":
                        // 基本比较: {变量} >= 数值
                        rdoLeftVar.Checked = true;
                        rdoOpGreaterEqual.Checked = true;
                        rdoRightValue.Checked = true;
                        if (cmbLeftVariable.Items.Count > 0)
                            cmbLeftVariable.SelectedIndex = 0;
                        txtRightValue.Text = "6.0";
                        cmbFunction.SelectedIndex = 0;
                        break;

                    case "range":
                        // 范围判断: {变量} < 值1 OR {变量} > 值2
                        MessageHelper.MessageOK(this,
                            "范围判断示例:\n\n" +
                            "{变量} < 10 OR {变量} > 90\n\n" +
                            "请先生成第一个条件，然后手动添加 OR 和第二个条件",
                            TType.Info);
                        rdoLeftVar.Checked = true;
                        rdoOpLess.Checked = true;
                        rdoRightValue.Checked = true;
                        txtRightValue.Text = "10";
                        break;

                    case "error":
                        // 误差容忍: ABS({变量} - 目标值) < 容差
                        rdoLeftVar.Checked = true;
                        rdoOpLess.Checked = true;
                        rdoRightValue.Checked = true;
                        cmbFunction.SelectedIndex = 1; // ABS
                        if (cmbLeftVariable.Items.Count > 0)
                            cmbLeftVariable.SelectedIndex = 0;
                        txtRightValue.Text = "0.1";
                        MessageHelper.MessageOK(this,
                            "误差判断已设置为: ABS({变量}) < 0.1\n\n" +
                            "如需计算误差，请切换到表达式模式，输入:\n" +
                            "{测量值} - {目标值}\n\n" +
                            "完整表达式: ABS({测量值} - {目标值}) < 0.1",
                            TType.Info);
                        break;

                    case "logic":
                        // 逻辑组合: {A} > 值1 AND {B} < 值2
                        MessageHelper.MessageOK(this,
                            "逻辑组合示例:\n\n" +
                            "{压力} >= 6 AND {温度} < 80\n\n" +
                            "请先生成第一个条件，然后手动添加 AND 和第二个条件",
                            TType.Info);
                        rdoLeftVar.Checked = true;
                        rdoOpGreaterEqual.Checked = true;
                        rdoRightValue.Checked = true;
                        txtRightValue.Text = "6.0";
                        break;
                }

                UpdatePreview();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "应用模板失败");
            }
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 确定按钮
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string expression = txtPreview.Text?.Trim();

                if (string.IsNullOrWhiteSpace(expression))
                {
                    MessageHelper.MessageOK(this, "请先构建表达式！", TType.Warn);
                    return;
                }

                // 简单验证
                if (!expression.Contains("{") || !expression.Contains("}"))
                {
                    var result = MessageHelper.MessageYes(this,
                        "表达式中未检测到变量引用，确定使用此表达式？",
                        TType.Warn);

                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                }

                GeneratedExpression = expression;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "确定操作失败");
                MessageHelper.MessageOK(this, $"操作失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 清空按钮
        /// </summary>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                // 重置为默认状态
                rdoLeftVar.Checked = true;
                rdoOpGreaterEqual.Checked = true;
                rdoRightValue.Checked = true;
                cmbFunction.SelectedIndex = 0;

                if (cmbLeftVariable.Items.Count > 0)
                    cmbLeftVariable.SelectedIndex = 0;

                txtLeftExpression.Clear();
                txtRightValue.Clear();
                cmbRightVariable.SelectedIndex = -1;
                txtRightExpression.Clear();
                txtPreview.Clear();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "清空失败");
            }
        }

        #endregion
    }
}
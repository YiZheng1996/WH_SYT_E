using MainUI.LogicalConfiguration.Parameter;
using System.Globalization;

namespace MainUI.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 检测表达式工具类
    /// 提供表达式验证、生成和模板功能
    /// </summary>
    public static class DetectionExpressionHelper
    {
        #region 表达式生成方法

        /// <summary>
        /// 生成范围检测表达式
        /// </summary>
        public static string GenerateRangeExpression(double minValue, double maxValue)
        {
            return $"{{value}} >= {FormatNumber(minValue)} && {{value}} <= {FormatNumber(maxValue)}";
        }

        /// <summary>
        /// 生成阈值检测表达式
        /// </summary>
        public static string GenerateThresholdExpression(double threshold, string operatorSymbol = ">=")
        {
            return $"{{value}} {operatorSymbol} {FormatNumber(threshold)}";
        }

        /// <summary>
        /// 生成容差检测表达式
        /// </summary>
        public static string GenerateToleranceExpression(double targetValue, double tolerance)
        {
            return $"Math.Abs({{value}} - {FormatNumber(targetValue)}) <= {FormatNumber(tolerance)}";
        }

        /// <summary>
        /// 生成变量比较表达式
        /// </summary>
        public static string GenerateVariableCompareExpression(string variableName, string operatorSymbol = ">=")
        {
            return $"{{value}} {operatorSymbol} {{{variableName}}}";
        }

        /// <summary>
        /// 格式化数字（避免本地化问题）
        /// </summary>
        private static string FormatNumber(double value)
        {
            return value.ToString("G", CultureInfo.InvariantCulture);
        }

        #endregion

        #region 表达式验证

        /// <summary>
        /// 验证表达式是否为有效的检测条件
        /// </summary>
        public static (bool IsValid, string Message) ValidateConditionExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return (false, "表达式不能为空");
            }

            // 检查是否包含{value}占位符
            if (!expression.Contains("{value}"))
            {
                return (false, "表达式必须包含{value}占位符来引用数据源的值");
            }

            // 检查括号匹配
            int braceCount = 0;
            int parenCount = 0;
            foreach (char c in expression)
            {
                switch (c)
                {
                    case '{': braceCount++; break;
                    case '}': braceCount--; break;
                    case '(': parenCount++; break;
                    case ')': parenCount--; break;
                }

                if (braceCount < 0 || parenCount < 0)
                {
                    return (false, "括号不匹配");
                }
            }

            if (braceCount != 0)
            {
                return (false, "大括号不匹配");
            }

            if (parenCount != 0)
            {
                return (false, "小括号不匹配");
            }

            // 检查是否包含至少一个比较运算符
            string[] comparisonOps = { ">=", "<=", "==", "!=", ">", "<" };
            bool hasComparison = comparisonOps.Any(op => expression.Contains(op));

            if (!hasComparison)
            {
                return (false, "表达式必须包含比较运算符（>=, <=, ==, !=, >, <）");
            }

            return (true, "表达式有效");
        }

        /// <summary>
        /// 获取表达式类型描述
        /// </summary>
        public static string GetExpressionTypeDescription(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return "未配置";

            if (expression.Contains(">=") && expression.Contains("<=") && expression.Contains("&&"))
                return "范围检测";
            if (expression.Contains("Math.Abs"))
                return "容差检测";
            if (expression.Contains("&&"))
                return "多条件AND";
            if (expression.Contains("||"))
                return "多条件OR";
            if (expression.Contains(">="))
                return "大于等于";
            if (expression.Contains("<="))
                return "小于等于";
            if (expression.Contains(">") && !expression.Contains(">="))
                return "大于";
            if (expression.Contains("<") && !expression.Contains("<="))
                return "小于";
            if (expression.Contains("=="))
                return "相等";
            if (expression.Contains("!="))
                return "不等";

            return "自定义";
        }

        #endregion
    }

    /// <summary>
    /// 常用表达式模板
    /// </summary>
    public static class ExpressionTemplates
    {
        /// <summary>
        /// 获取所有模板
        /// </summary>
        public static List<ExpressionTemplate> GetAllTemplates()
        {
            return
            [
                // 常用模板
                new()
                {
                    Name = "阈值检测",
                    Description = "值大于等于阈值",
                    Expression = "{value} >= [阈值]",
                    Category = "常用",
                    Icon = "📊",
                    Placeholders = ["[阈值]"]
                },
                new()
                {
                    Name = "范围检测",
                    Description = "值在最小值和最大值之间",
                    Expression = "{value} >= [最小值] && {value} <= [最大值]",
                    Category = "常用",
                    Icon = "📏",
                    Placeholders = ["[最小值]", "[最大值]"]
                },
                new()
                {
                    Name = "容差检测",
                    Description = "值与目标值的差值在容差范围内",
                    Expression = "Math.Abs({value} - [目标值]) <= [容差]",
                    Category = "常用",
                    Icon = "🎯",
                    Placeholders = ["[目标值]", "[容差]"]
                },
                new()
                {
                    Name = "相等检测",
                    Description = "值等于目标值",
                    Expression = "{value} == [目标值]",
                    Category = "常用",
                    Icon = "⚖️",
                    Placeholders = ["[目标值]"]
                },
                new()
                {
                    Name = "不等检测",
                    Description = "值不等于目标值",
                    Expression = "{value} != [目标值]",
                    Category = "常用",
                    Icon = "≠",
                    Placeholders = ["[目标值]"]
                },
                new()
                {
                    Name = "小于检测",
                    Description = "值小于阈值",
                    Expression = "{value} < [阈值]",
                    Category = "常用",
                    Icon = "📉",
                    Placeholders = ["[阈值]"]
                },

                // 高级模板
                new()
                {
                    Name = "多条件AND",
                    Description = "同时满足多个条件",
                    Expression = "{value} > [下限] && {value} < [上限]",
                    Category = "高级",
                    Icon = "🔗",
                    Placeholders = ["[下限]", "[上限]"]
                },
                new()
                {
                    Name = "多条件OR",
                    Description = "满足任一条件",
                    Expression = "{value} < [阈值1] || {value} > [阈值2]",
                    Category = "高级",
                    Icon = "🔀",
                    Placeholders = ["[阈值1]", "[阈值2]"]
                },
                new()
                {
                    Name = "变量比较",
                    Description = "与其他变量比较",
                    Expression = "{value} >= {目标变量}",
                    Category = "高级",
                    Icon = "🔄",
                    Placeholders = ["{目标变量}"]
                },
                new()
                {
                    Name = "变量比例比较",
                    Description = "与其他变量的比例比较",
                    Expression = "{value} >= {目标变量} * [比例]",
                    Category = "高级",
                    Icon = "📐",
                    Placeholders = ["{目标变量}", "[比例]"]
                },
                new()
                {
                    Name = "布尔状态检测",
                    Description = "检测布尔值为真",
                    Expression = "{value} == true",
                    Category = "高级",
                    Icon = "✅",
                    Placeholders = []
                },
                new()
                {
                    Name = "取整比较",
                    Description = "取整后与目标值比较",
                    Expression = "Math.Round({value}) == [目标值]",
                    Category = "高级",
                    Icon = "🔢",
                    Placeholders = ["[目标值]"]
                },
                new()
                {
                    Name = "百分比变化检测",
                    Description = "检测值相对于基准的变化是否在范围内",
                    Expression = "Math.Abs(({value} - {基准值}) / {基准值}) <= [变化百分比]",
                    Category = "高级",
                    Icon = "📈",
                    Placeholders = ["{基准值}", "[变化百分比]"]
                }
            ];
        }

        /// <summary>
        /// 按分类获取模板
        /// </summary>
        public static Dictionary<string, List<ExpressionTemplate>> GetTemplatesByCategory()
        {
            return GetAllTemplates()
                .GroupBy(t => t.Category)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// 获取常用模板
        /// </summary>
        public static List<ExpressionTemplate> GetCommonTemplates()
        {
            return GetAllTemplates().Where(t => t.Category == "常用").ToList();
        }
    }

    /// <summary>
    /// 表达式模板
    /// </summary>
    public class ExpressionTemplate
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 模板描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 表达式内容
        /// </summary>
        public string Expression { get; set; } = "";

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; } = "常用";

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; } = "📊";

        /// <summary>
        /// 需要替换的占位符
        /// </summary>
        public string[] Placeholders { get; set; } = [];

        /// <summary>
        /// 显示名称（带图标）
        /// </summary>
        public string DisplayName => $"{Icon} {Name}";

        /// <summary>
        /// 替换占位符
        /// </summary>
        public string ApplyReplacements(Dictionary<string, string> replacements)
        {
            string result = Expression;
            foreach (var kvp in replacements)
            {
                result = result.Replace(kvp.Key, kvp.Value);
            }
            return result;
        }
    }
}
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 表达式引擎共享常量
    /// </summary>
    internal static class ExpressionConstants
    {
        #region 正则表达式模式 - 统一管理,避免在多个类中重复定义

        /// <summary>
        /// 变量引用模式: {变量名} - 支持所有 Unicode 字符
        /// </summary>
        public static readonly Regex VariablePattern = new(@"\{([^}]+)\}", RegexOptions.Compiled);

        /// <summary>
        /// 函数调用模式: 函数名(参数)
        /// </summary>
        public static readonly Regex FunctionPattern = new(@"([\w\.]+)\s*\(([^)]*)\)", RegexOptions.Compiled);

        /// <summary>
        /// 数字模式
        /// </summary>
        public static readonly Regex NumberPattern = new(@"\b\d+(\.\d+)?\b", RegexOptions.Compiled);

        /// <summary>
        /// 字符串字面量模式
        /// </summary>
        public static readonly Regex StringLiteralPattern = new(@"""([^""\\]*(\\.[^""\\]*)*)""", RegexOptions.Compiled);

        /// <summary>
        /// PLC读取模式: PLC.模块名.地址
        /// </summary>
        public static readonly Regex PlcReadPattern = new(@"PLC\.(\w+)\.(\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// DateTime.Now.ToString("format") 模式
        /// </summary>
        public static readonly Regex DateTimeNowFormatPattern = new(@"DateTime\.Now\.ToString\(""[^""]*""\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// 单独的 DateTime.Now 模式
        /// </summary>
        public static readonly Regex DateTimeNowPattern = new(@"DateTime\.Now\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region 运算符定义 - 统一管理

        /// <summary>
        /// 运算符优先级
        /// </summary>
        public static readonly Dictionary<string, int> OperatorPrecedence = new()
        {
            { "||", 1 },
            { "&&", 2 },
            { "==", 3 }, { "!=", 3 },
            { "<", 4 }, { "<=", 4 }, { ">", 4 }, { ">=", 4 },
            { "+", 5 }, { "-", 5 },
            { "*", 6 }, { "/", 6 }, { "%", 6 },
            { "!", 7 }  // 一元运算符
        };

        /// <summary>
        /// 支持的运算符列表
        /// </summary>
        public static readonly string[] SupportedOperators =
        [
            "+", "-", "*", "/", "%", "==", "!=", ">", "<", ">=", "<=", "&&", "||", "!"
        ];

        /// <summary>
        /// DataTable运算符映射 - 用于求值
        /// </summary>
        public static readonly Dictionary<string, string> DataTableOperatorMap = new()
        {
            { "&&", " AND " },
            { "||", " OR " },
            { "==", "=" },
            { "!=", "<>" }
        };

        #endregion

        #region 字符集定义

        /// <summary>
        /// 有效字符集
        /// </summary>
        public static readonly HashSet<char> ValidCharacters = new(
            "0123456789.+-*/%(){}\"<>=!&|, \t\r\n"
            + "abcdefghijklmnopqrstuvwxyz"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        );

        #endregion
    }

    /// <summary>
    /// 表达式工具类
    /// </summary>
    internal static class ExpressionUtils
    {
        #region 类型转换 - 统一处理

        /// <summary>
        /// 转换为double类型 - 统一的转换逻辑
        /// </summary>
        public static double ConvertToDouble(object value)
        {
            if (value == null) return 0;
            if (value is double d) return d;
            if (value is int i) return i;
            if (value is bool b) return b ? 1 : 0;
            if (value is string s && double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                return result;

            return Convert.ToDouble(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 转换为bool类型 - 统一的转换逻辑
        /// </summary>
        public static bool ConvertToBool(object value)
        {
            if (value == null) return false;
            if (value is bool b) return b;
            if (value is double d) return d != 0;
            if (value is int i) return i != 0;
            if (value is string s) return !string.IsNullOrEmpty(s) && !s.Equals("false", StringComparison.OrdinalIgnoreCase);
            return true;
        }

        /// <summary>
        /// 转换为DateTime类型 - 统一的转换逻辑
        /// </summary>
        public static DateTime ConvertToDateTime(object value)
        {
            if (value is DateTime dt) return dt;
            if (value is string s && DateTime.TryParse(s, out var result))
                return result;

            return Convert.ToDateTime(value);
        }

        #endregion

        #region 值格式化 - 统一处理

        /// <summary>
        /// 格式化值用于表达式 - 智能处理字符串类型的数值
        /// </summary>
        public static string FormatValueForExpression(object value)
        {
            if (value == null)
                return "null";

            return value switch
            {
                // 整数类型
                int i => i.ToString(),
                long l => l.ToString(),
                short sh => sh.ToString(),
                byte b => b.ToString(),

                // 浮点数类型
                double d => d.ToString(CultureInfo.InvariantCulture),
                float f => f.ToString(CultureInfo.InvariantCulture),
                decimal dec => dec.ToString(CultureInfo.InvariantCulture),

                // 布尔类型
                bool bo => bo.ToString().ToLower(),

                // 字符串类型 - 智能判断
                string s => FormatStringValue(s),

                // 日期时间类型
                DateTime dt => $"\"{dt:yyyy-MM-dd HH:mm:ss}\"",

                // 其他类型
                _ => FormatUnknownType(value)
            };
        }

        /// <summary>
        /// 智能格式化字符串值
        /// 如果字符串是纯数值,不加引号;否则加引号
        /// </summary>
        private static string FormatStringValue(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "\"\"";

            // 尝试解析为数值
            // 如果是纯数值字符串,返回不带引号的数值
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var numValue))
            {
                // 返回格式化的数值（保持精度）
                return numValue.ToString(CultureInfo.InvariantCulture);
            }

            // 尝试解析为布尔值
            if (bool.TryParse(s, out var boolValue))
            {
                return boolValue.ToString().ToLower();
            }

            // 不是数值或布尔值,作为字符串处理(加引号并转义)
            return $"\"{EscapeString(s)}\"";
        }

        /// <summary>
        /// 转义字符串中的特殊字符
        /// </summary>
        private static string EscapeString(string s)
        {
            return s.Replace("\\", "\\\\")  // 反斜杠
                    .Replace("\"", "\\\"")  // 引号
                    .Replace("\n", "\\n")   // 换行
                    .Replace("\r", "\\r")   // 回车
                    .Replace("\t", "\\t");  // 制表符
        }

        /// <summary>
        /// 格式化未知类型的值
        /// </summary>
        private static string FormatUnknownType(object value)
        {
            var str = value.ToString();

            // 尝试解析为数值
            if (double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var numValue))
            {
                return numValue.ToString(CultureInfo.InvariantCulture);
            }

            // 尝试解析为布尔值
            if (bool.TryParse(str, out var boolValue))
            {
                return boolValue.ToString().ToLower();
            }

            // 作为字符串处理
            return $"\"{EscapeString(str)}\"";
        }


        /// <summary>
        /// 反转义字符串
        /// </summary>
        public static string UnescapeString(string str)
        {
            return str?.Replace("\\\"", "\"") ?? string.Empty;
        }

        #endregion

        #region 表达式检查 - 统一处理

        /// <summary>
        /// 检查括号是否平衡 - 统一的检查逻辑
        /// </summary>
        public static bool IsParenthesesBalanced(string expression)
        {
            int count = 0;
            bool inQuotes = false;
            char prevChar = '\0';

            foreach (char c in expression)
            {
                if (c == '"' && prevChar != '\\')
                {
                    inQuotes = !inQuotes;
                }
                else if (!inQuotes)
                {
                    if (c == '(') count++;
                    if (c == ')') count--;
                    if (count < 0) return false;
                }
                prevChar = c;
            }
            return count == 0;
        }

        /// <summary>
        /// 获取无效字符列表 - 统一检查逻辑
        /// </summary>
        public static List<char> GetInvalidCharacters(string expression)
        {
            return [.. expression
                .Where(c => !ExpressionConstants.ValidCharacters.Contains(c) && !char.IsLetterOrDigit(c))
                .Distinct()];
        }

        /// <summary>
        /// 判断是否为字符串字面量
        /// </summary>
        public static bool IsStringLiteral(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return false;

            return str.StartsWith("\"") && str.EndsWith("\"") && !str.EndsWith("\\\"");
        }

        /// <summary>
        /// 判断是否为运算符
        /// </summary>
        public static bool IsOperator(string token)
        {
            return ExpressionConstants.SupportedOperators.Contains(token);
        }

        /// <summary>
        /// 判断字符是否为运算符的一部分
        /// </summary>
        public static bool IsOperatorChar(char c)
        {
            return c is '+' or '-' or '*' or '/' or '%' or '=' or '!' or '<' or '>' or '&' or '|';
        }

        #endregion

        #region 字符串分割 - 统一处理

        /// <summary>
        /// 移除字符串字面量(用于验证) - 统一的处理逻辑
        /// </summary>
        public static string RemoveStringLiterals(string expression)
        {
            return ExpressionConstants.StringLiteralPattern.Replace(expression, "\"\"");
        }

        /// <summary>
        /// 分割函数参数(考虑嵌套括号和引号) - 统一的分割逻辑
        /// </summary>
        public static List<string> SplitArguments(string argsStr)
        {
            var args = new List<string>();
            var current = new StringBuilder();
            int parentheses = 0;
            bool inQuotes = false;
            bool escaped = false;

            foreach (char c in argsStr)
            {
                if (escaped)
                {
                    current.Append(c);
                    escaped = false;
                    continue;
                }

                if (c == '\\' && inQuotes)
                {
                    escaped = true;
                    current.Append(c);
                    continue;
                }

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                    current.Append(c);
                }
                else if (c == '(' && !inQuotes)
                {
                    parentheses++;
                    current.Append(c);
                }
                else if (c == ')' && !inQuotes)
                {
                    parentheses--;
                    current.Append(c);
                }
                else if (c == ',' && parentheses == 0 && !inQuotes)
                {
                    var arg = current.ToString().Trim();
                    if (!string.IsNullOrEmpty(arg))
                    {
                        args.Add(arg);
                    }
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            // 添加最后一个参数
            var lastArg = current.ToString().Trim();
            if (!string.IsNullOrEmpty(lastArg))
            {
                args.Add(lastArg);
            }

            return args;
        }

        #endregion

        #region DateTime处理 - 统一处理

        /// <summary>
        /// 预处理DateTime.Now表达式 - 统一的处理逻辑
        /// </summary>
        public static string PreprocessDateTimeExpression(string expression)
        {
            // DateTime.Now.ToString("format")
            expression = ExpressionConstants.DateTimeNowFormatPattern.Replace(
                expression,
                "\"2024-01-01\"");

            // 单独的 DateTime.Now
            expression = ExpressionConstants.DateTimeNowPattern.Replace(
                expression,
                "\"2024-01-01\"");

            return expression;
        }

        /// <summary>
        /// 处理DateTime.Now为实际值 - 运行时使用
        /// </summary>
        public static string ProcessDateTimeNow(string expression)
        {
            var now = DateTime.Now;

            // 处理带格式的 DateTime.Now.ToString("format")
            expression = Regex.Replace(
                expression,
                @"DateTime\.Now\.ToString\(""([^""]*)""\)",
                match =>
                {
                    var format = match.Groups[1].Value;
                    try
                    {
                        return $"\"{now.ToString(format)}\"";
                    }
                    catch
                    {
                        return $"\"{now:yyyy-MM-dd HH:mm:ss}\"";
                    }
                },
                RegexOptions.IgnoreCase);

            // 处理单独的 DateTime.Now
            expression = Regex.Replace(
                expression,
                @"DateTime\.Now\b",
                $"\"{now:yyyy-MM-dd HH:mm:ss}\"",
                RegexOptions.IgnoreCase);

            return expression;
        }

        #endregion

        #region 运算符处理 - 统一处理

        /// <summary>
        /// 转换为DataTable可识别的运算符 - 统一的转换逻辑
        /// </summary>
        public static string ConvertToDataTableOperators(string expression)
        {
            var result = expression;
            foreach (var mapping in ExpressionConstants.DataTableOperatorMap)
            {
                result = result.Replace(mapping.Key, mapping.Value);
            }
            return result;
        }

        /// <summary>
        /// 获取运算符优先级
        /// </summary>
        public static int GetOperatorPrecedence(string op)
        {
            return ExpressionConstants.OperatorPrecedence.TryGetValue(op, out var precedence)
                ? precedence
                : 0;
        }

        /// <summary>
        /// 获取运算符(处理多字符运算符) - 统一的获取逻辑
        /// </summary>
        public static string GetOperator(string expression, ref int index)
        {
            if (index >= expression.Length)
                return string.Empty;

            var c = expression[index];

            // 尝试匹配双字符运算符
            if (index + 1 < expression.Length)
            {
                var twoChar = expression.Substring(index, 2);
                if (ExpressionConstants.SupportedOperators.Contains(twoChar))
                {
                    index++;
                    return twoChar;
                }
            }

            // 单字符运算符
            return c.ToString();
        }

        #endregion

        #region 变量提取 - 统一处理

        /// <summary>
        /// 获取表达式中引用的所有变量名 - 统一的提取逻辑
        /// </summary>
        public static List<string> GetReferencedVariables(string expression)
        {
            return [.. ExpressionConstants.VariablePattern.Matches(expression)
                .Cast<Match>()
                .Select(m => m.Groups[1].Value.Trim())
                .Distinct()];
        }

        /// <summary>
        /// 获取表达式中的所有函数调用
        /// </summary>
        public static List<string> GetReferencedFunctions(string expression)
        {
            return [.. ExpressionConstants.FunctionPattern.Matches(expression)
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .Distinct()];
        }

        #endregion

        #region PLC引用检测 - 区分变量和PLC引用

        /// <summary>
        /// 判断是否为PLC引用格式
        /// 支持格式: 
        /// - PLC.模块名.地址 (如: PLC.AO控制.EP01)
        /// - 模块名.地址 (如: AO控制.EP01)
        /// </summary>
        public static bool IsPLCReference(string varName)
        {
            if (string.IsNullOrWhiteSpace(varName))
                return false;

            // 检查是否包含点号(PLC引用的特征)
            if (!varName.Contains('.'))
                return false;

            var parts = varName.Split('.');

            // 格式1: PLC.模块名.地址 (至少3部分)
            if (parts.Length >= 3 && parts[0].Equals("PLC", StringComparison.OrdinalIgnoreCase))
                return true;

            // 格式2: 模块名.地址 (至少2部分,且不全是数字)
            // 排除纯数字的情况,如 "123.456" 不应该被识别为PLC引用
            if (parts.Length >= 2)
            {
                // 至少有一部分不是纯数字,才认为是PLC引用
                return parts.Any(p => !double.TryParse(p, out _));
            }

            return false;
        }

        /// <summary>
        /// 从PLC引用中提取模块名和地址
        /// </summary>
        public static (string ModuleName, string Address) ParsePLCReference(string plcRef)
        {
            if (string.IsNullOrWhiteSpace(plcRef))
                return (null, null);

            var parts = plcRef.Split('.');

            // PLC.模块名.地址 格式
            if (parts.Length >= 3 && parts[0].Equals("PLC", StringComparison.OrdinalIgnoreCase))
            {
                var moduleName = parts[1];
                var address = string.Join(".", parts.Skip(2));
                return (moduleName, address);
            }

            // 模块名.地址 格式
            if (parts.Length >= 2)
            {
                var moduleName = parts[0];
                var address = string.Join(".", parts.Skip(1));
                return (moduleName, address);
            }

            return (null, null);
        }

        #endregion
    }
}
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.RegularExpressions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 表达式求值器 - 负责计算表达式的值
    /// </summary>
    internal class ExpressionEvaluator(FunctionRegistry functionRegistry, ILogger logger = null)
    {
        private readonly FunctionRegistry _functionRegistry = functionRegistry ?? throw new ArgumentNullException(nameof(functionRegistry));
        private readonly ILogger _logger = logger;

        // 正则表达式模式
        private readonly Regex _functionPattern = new(@"([\w\.]+)\s*\(([^)]*)\)", RegexOptions.Compiled);

        /// <summary>
        /// 求值已预处理的表达式
        /// </summary>
        public object Evaluate(string processedExpression)
        {
            try
            {
                _logger?.LogDebug("开始求值表达式: {Expression}", processedExpression);

                // 先转换运算符（在处理函数前）
                processedExpression = processedExpression
                    .Replace("&&", " AND ")
                    .Replace("||", " OR ")
                    .Replace("==", "=")
                    .Replace("!=", "<>");

                // 处理函数调用
                var expressionWithFunctions = ProcessFunctions(processedExpression);

                // 检查是否是简单值
                var trimmed = expressionWithFunctions.Trim();

                if (trimmed.StartsWith("\"") && trimmed.EndsWith("\""))
                    return trimmed.Substring(1, trimmed.Length - 2).Replace("\\\"", "\"");

                if (trimmed.Equals("true", StringComparison.OrdinalIgnoreCase))
                    return true;
                if (trimmed.Equals("false", StringComparison.OrdinalIgnoreCase))
                    return false;
                if (trimmed.Equals("null", StringComparison.OrdinalIgnoreCase))
                    return null;

                // 使用 DataTable 计算
                using var dt = new DataTable();
                dt.Locale = CultureInfo.InvariantCulture;
                return dt.Compute(expressionWithFunctions, string.Empty);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "表达式求值失败: {Expression}", processedExpression);
                throw new InvalidOperationException($"求值失败: {ex.Message}", ex);
            }
        }

        #region 函数处理

        /// <summary>
        /// 处理表达式中的函数调用
        /// </summary>
        private string ProcessFunctions(string expression)
        {
            var result = expression;
            var matches = _functionPattern.Matches(expression);

            // 从内向外处理函数（处理嵌套）
            while (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    var funcName = match.Groups[1].Value;
                    var argsStr = match.Groups[2].Value;

                    // 执行函数
                    var funcResult = ExecuteFunction(funcName, argsStr);

                    // 替换函数调用为结果
                    result = result.Replace(match.Value, FormatValue(funcResult));
                }

                // 重新匹配（处理嵌套函数）
                matches = _functionPattern.Matches(result);
            }

            return result;
        }

        /// <summary>
        /// 执行单个函数
        /// </summary>
        private object ExecuteFunction(string funcName, string argsStr)
        {
            var func = _functionRegistry.GetFunction(funcName) ?? 
                throw new InvalidOperationException($"未找到函数: {funcName}");

            // 解析参数
            var args = ParseFunctionArguments(argsStr);

            // 执行函数
            try
            {
                return func(args);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"执行函数 {funcName} 失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 解析函数参数
        /// </summary>
        private List<object> ParseFunctionArguments(string argsStr)
        {
            if (string.IsNullOrWhiteSpace(argsStr)) return [];

            var args = new List<object>();
            var parts = SplitArguments(argsStr);

            foreach (var part in parts)
            {
                var trimmed = part.Trim();

                // 字符串字面量
                if (trimmed.StartsWith("\"") && trimmed.EndsWith("\""))
                {
                    args.Add(trimmed.Substring(1, trimmed.Length - 2).Replace("\\\"", "\""));
                }
                // 布尔值
                else if (trimmed.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    args.Add(true);
                }
                else if (trimmed.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    args.Add(false);
                }
                // null
                else if (trimmed.Equals("null", StringComparison.OrdinalIgnoreCase))
                {
                    args.Add(null);
                }
                // 数字
                else if (double.TryParse(trimmed, NumberStyles.Any, CultureInfo.InvariantCulture, out double numValue))
                {
                    args.Add(numValue);
                }
                // 其他表达式 - 递归求值
                else
                {
                    var subResult = EvaluateWithDataTable(trimmed);
                    args.Add(subResult);
                }
            }

            return args;
        }

        /// <summary>
        /// 分割函数参数（考虑嵌套括号和引号）
        /// </summary>
        private List<string> SplitArguments(string argsStr)
        {
            var args = new List<string>();
            var current = new System.Text.StringBuilder();
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
                    args.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            if (current.Length > 0)
            {
                args.Add(current.ToString());
            }

            return args;
        }

        #endregion

        #region DataTable 求值

        /// <summary>
        /// 使用 DataTable 计算表达式
        /// </summary>
        private object EvaluateWithDataTable(string expression)
        {
            try
            {
                using var dt = new DataTable();
                dt.Locale = CultureInfo.InvariantCulture;
                var result = dt.Compute(expression, string.Empty);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"计算表达式失败: {expression}", ex);
            }
        }

        #endregion

        #region 值格式化

        /// <summary>
        /// 将值格式化为字符串
        /// </summary>
        private string FormatValue(object value)
        {
            if (value == null)
                return "null";

            if (value is bool b)
                return b ? "true" : "false";

            if (value is string str)
                return $"\"{str.Replace("\"", "\\\"")}\"";

            if (value is DateTime dt)
                return $"\"{dt:yyyy-MM-dd HH:mm:ss}\"";

            // 数字类型使用 InvariantCulture
            if (value is double || value is float || value is decimal)
                return Convert.ToDouble(value).ToString(CultureInfo.InvariantCulture);

            if (value is int || value is long || value is short || value is byte ||
                value is uint || value is ulong || value is ushort || value is sbyte)
                return Convert.ToInt64(value).ToString(CultureInfo.InvariantCulture);

            return value.ToString();
        }

        #endregion
    }
}
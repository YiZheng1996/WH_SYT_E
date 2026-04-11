using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.RegularExpressions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 表达式求值器
    /// 负责计算表达式的值,使用共享工具消除重复代码
    /// </summary>
    internal class ExpressionEvaluator(FunctionRegistry functionRegistry, ILogger logger = null)
    {
        private readonly FunctionRegistry _functionRegistry = functionRegistry ?? throw new ArgumentNullException(nameof(functionRegistry));

        #region 公共方法 - 求值入口

        /// <summary>
        /// 求值已预处理的表达式
        /// </summary>
        public object Evaluate(string processedExpression)
        {
            try
            {
                logger?.LogDebug("开始求值表达式: {Expression}", processedExpression);

                // 转换运算符 - 使用共享工具
                processedExpression = ExpressionUtils.ConvertToDataTableOperators(processedExpression);

                // 处理函数调用
                var expressionWithFunctions = ProcessFunctions(processedExpression);

                // 检查是否是简单值
                var (IsSimple, Value) = TryEvaluateSimpleValue(expressionWithFunctions);
                if (IsSimple)
                {
                    return Value;
                }

                // 使用 DataTable 计算复杂表达式
                return EvaluateWithDataTable(expressionWithFunctions);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "表达式求值失败: {Expression}", processedExpression);
                throw new InvalidOperationException($"求值失败: {ex.Message}", ex);
            }
        }

        #endregion

        #region 私有方法 - 简单值处理

        /// <summary>
        /// 尝试求值简单值 - 统一的简单值处理
        /// </summary>
        private (bool IsSimple, object Value) TryEvaluateSimpleValue(string expression)
        {
            var trimmed = expression.Trim();

            // 字符串字面量 - 使用共享工具
            if (ExpressionUtils.IsStringLiteral(trimmed))
            {
                var content = trimmed.Substring(1, trimmed.Length - 2);
                return (true, ExpressionUtils.UnescapeString(content));
            }

            // 布尔值
            if (trimmed.Equals("true", StringComparison.OrdinalIgnoreCase))
                return (true, true);
            if (trimmed.Equals("false", StringComparison.OrdinalIgnoreCase))
                return (true, false);

            // null值
            if (trimmed.Equals("null", StringComparison.OrdinalIgnoreCase))
                return (true, null);

            // 数字
            if (double.TryParse(trimmed, NumberStyles.Any, CultureInfo.InvariantCulture, out var numValue))
                return (true, numValue);

            return (false, null);
        }

        /// <summary>
        /// 使用DataTable求值
        /// </summary>
        private object EvaluateWithDataTable(string expression)
        {
            using var dt = new DataTable();
            dt.Locale = CultureInfo.InvariantCulture;
            return dt.Compute(expression, string.Empty);
        }

        #endregion

        #region 私有方法 - 函数处理

        /// <summary>
        /// 处理表达式中的函数调用 - 从内向外处理嵌套，支持嵌套括号
        /// </summary>
        private string ProcessFunctions(string expression)
        {
            var result = expression;

            // 循环处理，直到没有函数调用为止
            while (true)
            {
                // 找到最内层的函数调用（参数中不再包含其他函数调用）
                var match = FindInnermostFunctionCall(result);
                if (match == null) break;

                var funcName = match.Value.FuncName;
                var argsStr = match.Value.ArgsStr;
                var fullMatch = match.Value.FullMatch;

                logger?.LogDebug($"[ProcessFunctions] 匹配函数: [{funcName}] 参数: [{argsStr}]");

                // 执行函数
                var funcResult = ExecuteFunction(funcName, argsStr);
                logger?.LogDebug($"[ProcessFunctions] 函数结果: {funcResult}");

                // 替换函数调用为结果
                var formattedResult = ExpressionUtils.FormatValueForExpression(funcResult);
                var index = result.IndexOf(fullMatch, StringComparison.Ordinal);
                if (index < 0) break; // 安全防护，避免死循环
                result = result.Substring(0, index) + formattedResult + result.Substring(index + fullMatch.Length);
            }

            return result;
        }

        /// <summary>
        /// 查找表达式中最内层的函数调用（参数不含嵌套函数）
        /// 通过手工括号配对，正确处理嵌套场景
        /// </summary>
        private (string FuncName, string ArgsStr, string FullMatch)? FindInnermostFunctionCall(string expression)
        {
            // 函数名正则：标识符（含 . 命名空间），后跟 (
            var funcNameRegex = new Regex(@"([A-Za-z_][\w\.]*)\s*\(", RegexOptions.Compiled);

            (string FuncName, string ArgsStr, string FullMatch)? lastInnermost = null;

            foreach (Match nameMatch in funcNameRegex.Matches(expression))
            {
                var funcName = nameMatch.Groups[1].Value;
                var openParenIndex = nameMatch.Index + nameMatch.Length - 1; // ( 的位置

                // 从 ( 开始做括号配对，找到对应的 )
                int depth = 0;
                int closeParenIndex = -1;
                bool inString = false;

                for (int i = openParenIndex; i < expression.Length; i++)
                {
                    char c = expression[i];

                    // 字符串字面量内的括号不参与配对
                    if (c == '"')
                    {
                        inString = !inString;
                        continue;
                    }
                    if (inString) continue;

                    if (c == '(') depth++;
                    else if (c == ')')
                    {
                        depth--;
                        if (depth == 0)
                        {
                            closeParenIndex = i;
                            break;
                        }
                    }
                }

                if (closeParenIndex < 0) continue; // 括号不匹配，跳过

                var argsStr = expression.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);
                var fullMatch = expression.Substring(nameMatch.Index, closeParenIndex - nameMatch.Index + 1);

                // 关键：检查参数中是否还有未求值的函数调用
                // 如果参数里还有 "标识符(" 模式，说明这不是最内层
                if (funcNameRegex.IsMatch(argsStr))
                {
                    // 不是最内层，记录但继续找更内层的
                    continue;
                }

                // 这是一个最内层函数调用
                lastInnermost = (funcName, argsStr, fullMatch);
                return lastInnermost; // 找到第一个就返回
            }

            return lastInnermost;
        }

        /// <summary>
        /// 执行单个函数
        /// </summary>
        private object ExecuteFunction(string funcName, string argsStr)
        {
            var func = _functionRegistry.GetFunction(funcName);

            if (func == null)
            {
                throw new InvalidOperationException($"不支持的函数: {funcName}");
            }

            // 解析参数 - 使用共享工具分割
            var args = ParseFunctionArguments(argsStr);

            // 执行函数
            try
            {
                return func(args);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"函数 '{funcName}' 执行失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 解析函数参数 - 统一的参数解析逻辑
        /// </summary>
        private List<object> ParseFunctionArguments(string argsStr)
        {
            if (string.IsNullOrWhiteSpace(argsStr))
                return [];

            // 使用共享工具分割参数
            var argStrings = ExpressionUtils.SplitArguments(argsStr);
            var args = new List<object>();

            foreach (var argStr in argStrings)
            {
                var trimmed = argStr.Trim();

                // 字符串字面量
                if (ExpressionUtils.IsStringLiteral(trimmed))
                {
                    var content = trimmed.Substring(1, trimmed.Length - 2);
                    args.Add(ExpressionUtils.UnescapeString(content));
                }
                // 布尔值
                else if (bool.TryParse(trimmed, out var boolValue))
                {
                    args.Add(boolValue);
                }
                // 数字
                else if (double.TryParse(trimmed, NumberStyles.Any, CultureInfo.InvariantCulture, out var numValue))
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

        #endregion
    }
}

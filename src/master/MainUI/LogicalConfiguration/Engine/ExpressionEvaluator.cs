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
        /// 处理表达式中的函数调用 - 从内向外处理嵌套
        /// </summary>
        private string ProcessFunctions(string expression)
        {
            var result = expression;
            var matches = ExpressionConstants.FunctionPattern.Matches(expression);

            // 从内向外处理函数(处理嵌套)
            while (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    var funcName = match.Groups[1].Value;
                    var argsStr = match.Groups[2].Value;

                    // 执行函数
                    var funcResult = ExecuteFunction(funcName, argsStr);

                    // 替换函数调用为结果 - 使用共享工具格式化
                    result = result.Replace(match.Value, ExpressionUtils.FormatValueForExpression(funcResult));
                }

                // 重新匹配(处理嵌套函数)
                matches = ExpressionConstants.FunctionPattern.Matches(result);
            }

            return result;
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

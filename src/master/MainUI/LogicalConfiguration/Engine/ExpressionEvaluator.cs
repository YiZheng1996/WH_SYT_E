using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.RegularExpressions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 表达式评估器
    /// 支持变量引用、数学运算、字符串操作、逻辑判断等
    /// </summary>
    public class ExpressionEvaluator
    {
        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger _logger;

        // 正则表达式模式
        private readonly Regex _variablePattern = new Regex(@"\{(\w+)\}", RegexOptions.Compiled);
        private readonly Regex _functionPattern = new Regex(@"\b(\w+)\s*\(([^)]*)\)", RegexOptions.Compiled);
        private readonly Regex _numberPattern = new Regex(@"\b\d+(\.\d+)?\b", RegexOptions.Compiled);
        private readonly Regex _stringPattern = new Regex(@"""([^""\\]*(\\.[^""\\]*)*)""", RegexOptions.Compiled);

        // 支持的运算符优先级
        private readonly Dictionary<string, int> _operatorPrecedence = new Dictionary<string, int>
        {
            { "||", 1 },
            { "&&", 2 },
            { "==", 3 }, { "!=", 3 },
            { "<", 4 }, { "<=", 4 }, { ">", 4 }, { ">=", 4 },
            { "+", 5 }, { "-", 5 },
            { "*", 6 }, { "/", 6 }, { "%", 6 },
            { "!", 7 }  // 一元运算符
        };

        // 支持的函数
        private readonly Dictionary<string, Func<List<object>, object>> _supportedFunctions;

        public ExpressionEvaluator(GlobalVariableManager variableManager, ILogger logger = null)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _logger = logger;

            // 初始化支持的函数
            _supportedFunctions = InitializeFunctions();
        }

        /// <summary>
        /// 异步评估表达式
        /// </summary>
        /// <param name="expression">要评估的表达式</param>
        /// <returns>评估结果</returns>
        public async Task<EvaluationResult> EvaluateAsync(string expression)
        {
            return await Task.Run(() => Evaluate(expression));
        }

        /// <summary>
        /// 评估表达式
        /// </summary>
        /// <param name="expression">要评估的表达式</param>
        /// <returns>评估结果</returns>
        public EvaluationResult Evaluate(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    return EvaluationResult.Error("表达式为空");
                }

                _logger?.LogDebug("开始评估表达式: {Expression}", expression);

                // 1. 预处理表达式
                var processedExpression = PreprocessExpression(expression);

                // 2. 解析和评估
                var result = EvaluateExpression(processedExpression);

                _logger?.LogDebug("表达式评估完成: {Expression} = {Result}", expression, result);

                return EvaluationResult.Successs(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "表达式评估失败: {Expression}", expression);
                return EvaluationResult.Error($"评估失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 预处理表达式
        /// </summary>
        /// <param name="expression">原始表达式</param>
        /// <returns>处理后的表达式</returns>
        private string PreprocessExpression(string expression)
        {
            var result = expression.Trim();

            // 1. 替换变量引用
            result = ReplaceVariableReferences(result);

            // 2. 处理函数调用
            result = ProcessFunctionCalls(result);

            return result;
        }

        /// <summary>
        /// 替换变量引用
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>替换后的表达式</returns>
        private string ReplaceVariableReferences(string expression)
        {
            return _variablePattern.Replace(expression, match =>
            {
                var variableName = match.Groups[1].Value;
                var variable = _variableManager.FindVariableByName(variableName);

                if (variable?.VarValue != null)
                {
                    var value = variable.VarValue.ToString();

                    // 根据变量类型决定如何处理值
                    switch (variable.VarType?.ToUpperInvariant())
                    {
                        case "STRING":
                            return $"\"{EscapeString(value)}\"";

                        case "BOOLEAN":
                        case "BOOL":
                            if (bool.TryParse(value, out var boolValue))
                                return boolValue.ToString().ToLowerInvariant();
                            return "false";

                        case "INTEGER":
                        case "DOUBLE":
                        case "DECIMAL":
                        case "FLOAT":
                            return value;

                        default:
                            // 尝试识别数值
                            if (IsNumeric(value))
                                return value;
                            else
                                return $"\"{EscapeString(value)}\"";
                    }
                }

                return "null";
            });
        }

        /// <summary>
        /// 处理函数调用
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>处理后的表达式</returns>
        private string ProcessFunctionCalls(string expression)
        {
            return _functionPattern.Replace(expression, match =>
            {
                var functionName = match.Groups[1].Value.ToUpperInvariant();
                var argumentsString = match.Groups[2].Value;

                if (_supportedFunctions.TryGetValue(functionName, out var function))
                {
                    try
                    {
                        var arguments = ParseFunctionArguments(argumentsString);
                        var result = function(arguments);

                        // 根据结果类型格式化返回值
                        return FormatFunctionResult(result);
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogWarning(ex, "函数调用失败: {FunctionName}({Arguments})", functionName, argumentsString);
                        return "null";
                    }
                }

                return match.Value; // 不支持的函数保持原样
            });
        }

        /// <summary>
        /// 评估处理后的表达式
        /// </summary>
        /// <param name="expression">处理后的表达式</param>
        /// <returns>评估结果</returns>
        private object EvaluateExpression(string expression)
        {
            expression = expression.Trim();

            // 处理简单情况
            if (string.IsNullOrEmpty(expression) || expression == "null")
                return null;

            // 字符串字面量
            if (expression.StartsWith("\"") && expression.EndsWith("\""))
                return expression.Substring(1, expression.Length - 2);

            // 布尔值
            if (bool.TryParse(expression, out var boolValue))
                return boolValue;

            // 数值
            if (IsNumeric(expression))
            {
                if (int.TryParse(expression, out var intValue))
                    return intValue;
                if (double.TryParse(expression, NumberStyles.Float, CultureInfo.InvariantCulture, out var doubleValue))
                    return doubleValue;
            }

            // 复杂表达式 - 使用递归下降解析器
            try
            {
                var tokens = Tokenize(expression);
                var postfix = ConvertToPostfix(tokens);
                return EvaluatePostfix(postfix);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "复杂表达式评估失败，尝试作为字符串处理: {Expression}", expression);
                return expression; // 作为字符串返回
            }
        }

        /// <summary>
        /// 标记化表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>标记列表</returns>
        private List<Token> Tokenize(string expression)
        {
            var tokens = new List<Token>();
            var i = 0;

            while (i < expression.Length)
            {
                var c = expression[i];

                // 跳过空白字符
                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                // 字符串字面量
                if (c == '"')
                {
                    var start = i;
                    i++; // 跳过开始的引号

                    while (i < expression.Length && expression[i] != '"')
                    {
                        if (expression[i] == '\\' && i + 1 < expression.Length)
                            i += 2; // 跳过转义字符
                        else
                            i++;
                    }

                    if (i < expression.Length)
                        i++; // 跳过结束的引号

                    var value = expression.Substring(start + 1, i - start - 2);
                    tokens.Add(new Token(TokenType.String, value));
                    continue;
                }

                // 数字
                if (char.IsDigit(c) || c == '.')
                {
                    var start = i;
                    while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                        i++;

                    var value = expression.Substring(start, i - start);
                    tokens.Add(new Token(TokenType.Number, value));
                    continue;
                }

                // 操作符
                if (i + 1 < expression.Length)
                {
                    var twoCharOp = expression.Substring(i, 2);
                    if (_operatorPrecedence.ContainsKey(twoCharOp))
                    {
                        tokens.Add(new Token(TokenType.Operator, twoCharOp));
                        i += 2;
                        continue;
                    }
                }

                var oneCharOp = c.ToString();
                if (_operatorPrecedence.ContainsKey(oneCharOp) || c == '(' || c == ')')
                {
                    var tokenType = c == '(' || c == ')' ? TokenType.Parenthesis : TokenType.Operator;
                    tokens.Add(new Token(tokenType, oneCharOp));
                    i++;
                    continue;
                }

                // 标识符（布尔值等）
                if (char.IsLetter(c))
                {
                    var start = i;
                    while (i < expression.Length && (char.IsLetterOrDigit(expression[i]) || expression[i] == '_'))
                        i++;

                    var value = expression.Substring(start, i - start);

                    // 检查是否是布尔值
                    if (value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                        value.Equals("false", StringComparison.OrdinalIgnoreCase))
                    {
                        tokens.Add(new Token(TokenType.Boolean, value.ToLowerInvariant()));
                    }
                    else
                    {
                        tokens.Add(new Token(TokenType.Identifier, value));
                    }
                    continue;
                }

                // 未识别的字符，跳过
                i++;
            }

            return tokens;
        }

        /// <summary>
        /// 将中缀表达式转换为后缀表达式（逆波兰表示法）
        /// </summary>
        /// <param name="tokens">标记列表</param>
        /// <returns>后缀表达式标记列表</returns>
        private List<Token> ConvertToPostfix(List<Token> tokens)
        {
            var output = new List<Token>();
            var operatorStack = new Stack<Token>();

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                    case TokenType.String:
                    case TokenType.Boolean:
                    case TokenType.Identifier:
                        output.Add(token);
                        break;

                    case TokenType.Operator:
                        while (operatorStack.Count > 0 &&
                               operatorStack.Peek().Type == TokenType.Operator &&
                               GetPrecedence(operatorStack.Peek().Value) >= GetPrecedence(token.Value))
                        {
                            output.Add(operatorStack.Pop());
                        }
                        operatorStack.Push(token);
                        break;

                    case TokenType.Parenthesis:
                        if (token.Value == "(")
                        {
                            operatorStack.Push(token);
                        }
                        else // ")"
                        {
                            while (operatorStack.Count > 0 && operatorStack.Peek().Value != "(")
                            {
                                output.Add(operatorStack.Pop());
                            }
                            if (operatorStack.Count > 0)
                                operatorStack.Pop(); // 移除左括号
                        }
                        break;
                }
            }

            while (operatorStack.Count > 0)
            {
                output.Add(operatorStack.Pop());
            }

            return output;
        }

        /// <summary>
        /// 评估后缀表达式
        /// </summary>
        /// <param name="postfixTokens">后缀表达式标记列表</param>
        /// <returns>评估结果</returns>
        private object EvaluatePostfix(List<Token> postfixTokens)
        {
            var stack = new Stack<object>();

            foreach (var token in postfixTokens)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                        if (int.TryParse(token.Value, out var intValue))
                            stack.Push(intValue);
                        else if (double.TryParse(token.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var doubleValue))
                            stack.Push(doubleValue);
                        else
                            stack.Push(token.Value);
                        break;

                    case TokenType.String:
                        stack.Push(token.Value);
                        break;

                    case TokenType.Boolean:
                        stack.Push(bool.Parse(token.Value));
                        break;

                    case TokenType.Identifier:
                        stack.Push(token.Value);
                        break;

                    case TokenType.Operator:
                        var result = ExecuteOperation(token.Value, stack);
                        stack.Push(result);
                        break;
                }
            }

            return stack.Count > 0 ? stack.Pop() : null;
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="operation">操作符</param>
        /// <param name="stack">操作数栈</param>
        /// <returns>操作结果</returns>
        private object ExecuteOperation(string operation, Stack<object> stack)
        {
            switch (operation)
            {
                case "+":
                    return ExecuteBinaryOperation((a, b) => AddValues(b, a), stack);
                case "-":
                    return ExecuteBinaryOperation((a, b) => SubtractValues(b, a), stack);
                case "*":
                    return ExecuteBinaryOperation((a, b) => MultiplyValues(b, a), stack);
                case "/":
                    return ExecuteBinaryOperation((a, b) => DivideValues(b, a), stack);
                case "%":
                    return ExecuteBinaryOperation((a, b) => ModuloValues(b, a), stack);
                case "==":
                    return ExecuteBinaryOperation((a, b) => CompareValues(b, a, "=="), stack);
                case "!=":
                    return ExecuteBinaryOperation((a, b) => CompareValues(b, a, "!="), stack);
                case "<":
                    return ExecuteBinaryOperation((a, b) => CompareValues(b, a, "<"), stack);
                case "<=":
                    return ExecuteBinaryOperation((a, b) => CompareValues(b, a, "<="), stack);
                case ">":
                    return ExecuteBinaryOperation((a, b) => CompareValues(b, a, ">"), stack);
                case ">=":
                    return ExecuteBinaryOperation((a, b) => CompareValues(b, a, ">="), stack);
                case "&&":
                    return ExecuteBinaryOperation((a, b) => LogicalAnd(b, a), stack);
                case "||":
                    return ExecuteBinaryOperation((a, b) => LogicalOr(b, a), stack);
                case "!":
                    return ExecuteUnaryOperation(a => LogicalNot(a), stack);
                default:
                    throw new InvalidOperationException($"不支持的操作符: {operation}");
            }
        }

        /// <summary>
        /// 执行二元操作
        /// </summary>
        private object ExecuteBinaryOperation(Func<object, object, object> operation, Stack<object> stack)
        {
            if (stack.Count < 2)
                throw new InvalidOperationException("操作数不足");

            var right = stack.Pop();
            var left = stack.Pop();
            return operation(left, right);
        }

        /// <summary>
        /// 执行一元操作
        /// </summary>
        private object ExecuteUnaryOperation(Func<object, object> operation, Stack<object> stack)
        {
            if (stack.Count < 1)
                throw new InvalidOperationException("操作数不足");

            var operand = stack.Pop();
            return operation(operand);
        }

        #region 运算操作实现

        /// <summary>
        /// 加法运算
        /// </summary>
        private object AddValues(object left, object right)
        {
            if (left is string || right is string)
            {
                return $"{left}{right}";
            }

            if (TryGetNumericValue(left, out var leftNum) && TryGetNumericValue(right, out var rightNum))
            {
                if (left is int && right is int)
                    return (int)leftNum + (int)rightNum;
                return leftNum + rightNum;
            }

            return $"{left}{right}"; // 字符串连接
        }

        /// <summary>
        /// 减法运算
        /// </summary>
        private object SubtractValues(object left, object right)
        {
            if (TryGetNumericValue(left, out var leftNum) && TryGetNumericValue(right, out var rightNum))
            {
                if (left is int && right is int)
                    return (int)leftNum - (int)rightNum;
                return leftNum - rightNum;
            }

            throw new InvalidOperationException($"无法对 '{left}' 和 '{right}' 执行减法运算");
        }

        /// <summary>
        /// 乘法运算
        /// </summary>
        private object MultiplyValues(object left, object right)
        {
            if (TryGetNumericValue(left, out var leftNum) && TryGetNumericValue(right, out var rightNum))
            {
                if (left is int && right is int)
                    return (int)leftNum * (int)rightNum;
                return leftNum * rightNum;
            }

            throw new InvalidOperationException($"无法对 '{left}' 和 '{right}' 执行乘法运算");
        }

        /// <summary>
        /// 除法运算
        /// </summary>
        private object DivideValues(object left, object right)
        {
            if (TryGetNumericValue(left, out var leftNum) && TryGetNumericValue(right, out var rightNum))
            {
                if (Math.Abs(rightNum) < double.Epsilon)
                    throw new DivideByZeroException("除数不能为零");

                return leftNum / rightNum;
            }

            throw new InvalidOperationException($"无法对 '{left}' 和 '{right}' 执行除法运算");
        }

        /// <summary>
        /// 模运算
        /// </summary>
        private object ModuloValues(object left, object right)
        {
            if (TryGetNumericValue(left, out var leftNum) && TryGetNumericValue(right, out var rightNum))
            {
                if (Math.Abs(rightNum) < double.Epsilon)
                    throw new DivideByZeroException("除数不能为零");

                return leftNum % rightNum;
            }

            throw new InvalidOperationException($"无法对 '{left}' 和 '{right}' 执行模运算");
        }

        /// <summary>
        /// 比较运算
        /// </summary>
        private object CompareValues(object left, object right, string comparison)
        {
            // 相等性比较
            if (comparison == "==")
            {
                return Equals(left?.ToString(), right?.ToString());
            }
            if (comparison == "!=")
            {
                return !Equals(left?.ToString(), right?.ToString());
            }

            // 数值比较
            if (TryGetNumericValue(left, out var leftNum) && TryGetNumericValue(right, out var rightNum))
            {
                return comparison switch
                {
                    "<" => leftNum < rightNum,
                    "<=" => leftNum <= rightNum,
                    ">" => leftNum > rightNum,
                    ">=" => leftNum >= rightNum,
                    _ => false
                };
            }

            // 字符串比较
            var leftStr = left?.ToString() ?? "";
            var rightStr = right?.ToString() ?? "";
            var compareResult = string.Compare(leftStr, rightStr, StringComparison.Ordinal);

            return comparison switch
            {
                "<" => compareResult < 0,
                "<=" => compareResult <= 0,
                ">" => compareResult > 0,
                ">=" => compareResult >= 0,
                _ => false
            };
        }

        /// <summary>
        /// 逻辑与运算
        /// </summary>
        private object LogicalAnd(object left, object right)
        {
            return ToBool(left) && ToBool(right);
        }

        /// <summary>
        /// 逻辑或运算
        /// </summary>
        private object LogicalOr(object left, object right)
        {
            return ToBool(left) || ToBool(right);
        }

        /// <summary>
        /// 逻辑非运算
        /// </summary>
        private object LogicalNot(object operand)
        {
            return !ToBool(operand);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取操作符优先级
        /// </summary>
        private int GetPrecedence(string op)
        {
            return _operatorPrecedence.TryGetValue(op, out var precedence) ? precedence : 0;
        }

        /// <summary>
        /// 检查字符串是否为数值
        /// </summary>
        private bool IsNumeric(string value)
        {
            return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
        }

        /// <summary>
        /// 尝试获取数值
        /// </summary>
        private bool TryGetNumericValue(object value, out double numericValue)
        {
            numericValue = 0;

            if (value is int intValue)
            {
                numericValue = intValue;
                return true;
            }
            if (value is double doubleValue)
            {
                numericValue = doubleValue;
                return true;
            }
            if (value is decimal decimalValue)
            {
                numericValue = (double)decimalValue;
                return true;
            }
            if (value is float floatValue)
            {
                numericValue = floatValue;
                return true;
            }

            return double.TryParse(value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out numericValue);
        }

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        private bool ToBool(object value)
        {
            if (value is bool boolValue)
                return boolValue;

            if (value == null)
                return false;

            var stringValue = value.ToString();
            if (bool.TryParse(stringValue, out var parsedBool))
                return parsedBool;

            if (TryGetNumericValue(value, out var numericValue))
                return Math.Abs(numericValue) > double.Epsilon;

            return !string.IsNullOrEmpty(stringValue);
        }

        /// <summary>
        /// 转义字符串
        /// </summary>
        private string EscapeString(string value)
        {
            return value?.Replace("\"", "\\\"") ?? "";
        }

        /// <summary>
        /// 解析函数参数
        /// </summary>
        private List<object> ParseFunctionArguments(string argumentsString)
        {
            var arguments = new List<object>();

            if (string.IsNullOrWhiteSpace(argumentsString))
                return arguments;

            // 简单的参数分割（不处理嵌套括号）
            var parts = argumentsString.Split(',');

            foreach (var part in parts)
            {
                var trimmedPart = part.Trim();
                var evaluatedArg = EvaluateExpression(trimmedPart);
                arguments.Add(evaluatedArg);
            }

            return arguments;
        }

        /// <summary>
        /// 格式化函数结果
        /// </summary>
        private string FormatFunctionResult(object result)
        {
            if (result == null)
                return "null";

            if (result is string stringResult)
                return $"\"{EscapeString(stringResult)}\"";

            if (result is bool boolResult)
                return boolResult.ToString().ToLowerInvariant();

            return result.ToString();
        }

        /// <summary>
        /// 初始化支持的函数
        /// </summary>
        private Dictionary<string, Func<List<object>, object>> InitializeFunctions()
        {
            return new Dictionary<string, Func<List<object>, object>>
            {
                ["LEN"] = args => args.FirstOrDefault()?.ToString()?.Length ?? 0,
                ["UPPER"] = args => args.FirstOrDefault()?.ToString()?.ToUpperInvariant() ?? "",
                ["LOWER"] = args => args.FirstOrDefault()?.ToString()?.ToLowerInvariant() ?? "",
                ["TRIM"] = args => args.FirstOrDefault()?.ToString()?.Trim() ?? "",
                ["SUBSTRING"] = args =>
                {
                    if (args.Count < 2) return "";
                    var str = args[0]?.ToString() ?? "";
                    if (TryGetNumericValue(args[1], out var start))
                    {
                        var startIndex = Math.Max(0, (int)start);
                        if (args.Count > 2 && TryGetNumericValue(args[2], out var length))
                        {
                            var len = Math.Max(0, (int)length);
                            return startIndex < str.Length ?
                                str.Substring(startIndex, Math.Min(len, str.Length - startIndex)) : "";
                        }
                        return startIndex < str.Length ? str.Substring(startIndex) : "";
                    }
                    return "";
                },
                ["REPLACE"] = args =>
                {
                    if (args.Count < 3) return args.FirstOrDefault()?.ToString() ?? "";
                    var str = args[0]?.ToString() ?? "";
                    var oldValue = args[1]?.ToString() ?? "";
                    var newValue = args[2]?.ToString() ?? "";
                    return str.Replace(oldValue, newValue);
                },
                ["NOW"] = args => DateTime.Now,
                ["TODAY"] = args => DateTime.Today,
                ["FORMAT"] = args =>
                {
                    if (args.Count < 2) return args.FirstOrDefault()?.ToString() ?? "";
                    var value = args[0];
                    var format = args[1]?.ToString() ?? "";

                    if (value is DateTime dateTime)
                        return dateTime.ToString(format);
                    if (TryGetNumericValue(value, out var number))
                        return number.ToString(format);

                    return value?.ToString() ?? "";
                },
                ["ABS"] = args =>
                {
                    if (TryGetNumericValue(args.FirstOrDefault(), out var value))
                        return Math.Abs(value);
                    return 0;
                },
                ["MAX"] = args =>
                {
                    var numbers = args.Where(a => TryGetNumericValue(a, out _)).ToList();
                    if (!numbers.Any()) return 0;
                    return numbers.Max(a => { TryGetNumericValue(a, out var v); return v; });
                },
                ["MIN"] = args =>
                {
                    var numbers = args.Where(a => TryGetNumericValue(a, out _)).ToList();
                    if (!numbers.Any()) return 0;
                    return numbers.Min(a => { TryGetNumericValue(a, out var v); return v; });
                }
            };
        }

        #endregion
    }

    #region 辅助类定义

    /// <summary>
    /// 评估结果
    /// </summary>
    public class EvaluationResult
    {
        public bool Success { get; set; }
        public object Value { get; set; }
        public string ErrorMessage { get; set; }

        public static EvaluationResult Successs(object value)
        {
            return new EvaluationResult { Success = true, Value = value };
        }

        public static EvaluationResult Error(string errorMessage)
        {
            return new EvaluationResult { Success = false, ErrorMessage = errorMessage };
        }
    }

    /// <summary>
    /// 标记类型
    /// </summary>
    public enum TokenType
    {
        Number,
        String,
        Boolean,
        Identifier,
        Operator,
        Parenthesis
    }

    /// <summary>
    /// 标记
    /// </summary>
    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Type}: {Value}";
        }
    }

    #endregion
}
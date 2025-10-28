using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.RegularExpressions;

namespace MainUI.LogicalConfiguration.Services
{
    /// <summary>
    /// 表达式验证引擎
    /// 提供表达式语法检查、变量存在性验证、类型兼容性检查等功能
    /// </summary>
    public class ExpressionValidator(GlobalVariableManager variableManager, ILogger<ExpressionValidator> logger = null)
    {
        private readonly GlobalVariableManager _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
        private readonly ILogger<ExpressionValidator> _logger = logger;

        // 支持的运算符
        private readonly string[] _supportedOperators = { "+", "-", "*", "/", "%", "==", "!=", ">", "<", ">=", "<=", "&&", "||", "!" };

        // 支持的函数 - 扩展版本
        // 包含原有的基础函数和 ExpressionBuilderDialog 中使用的所有函数
        private readonly string[] _supportedFunctions =
        { 
            // === 原有的基础字符串函数 ===
            "LEN", "SUBSTRING", "UPPER", "LOWER", "TRIM", "REPLACE", "NOW", "FORMAT",
            
            // === Math 数学函数 (支持 Math.XXX 形式) ===
            "MATH.ABS", "ABS",               // 绝对值
            "MATH.MAX", "MAX",               // 最大值
            "MATH.MIN", "MIN",               // 最小值
            "MATH.ROUND", "ROUND",           // 四舍五入
            "MATH.FLOOR", "FLOOR",           // 向下取整
            "MATH.CEILING", "CEILING",       // 向上取整
            "MATH.SQRT", "SQRT",             // 平方根
            "MATH.POW", "POW",               // 幂运算
            "MATH.SIN", "SIN",               // 正弦
            "MATH.COS", "COS",               // 余弦
            "MATH.TAN", "TAN",               // 正切
            
            // === String 字符串函数 (支持 String.XXX 形式) ===
            "STRING.LENGTH", "LENGTH",       // 字符串长度
            "STRING.SUBSTRING",              // 字符串截取
            "STRING.CONTAINS", "CONTAINS",   // 包含判断
            "STRING.REPLACE",                // 字符串替换
            "STRING.TOUPPER", "TOUPPER",     // 转大写
            "STRING.TOLOWER", "TOLOWER",     // 转小写
            "STRING.TRIM",                   // 去空格
            "STRING.STARTSWITH", "STARTSWITH", // 开始判断
            "STRING.ENDSWITH", "ENDSWITH",   // 结束判断
            "STRING.INDEXOF", "INDEXOF",     // 查找位置
            "STRING.SPLIT", "SPLIT",         // 字符串分割
            "STRING.JOIN", "JOIN",           // 字符串连接
            "STRING.PADLEFT", "PADLEFT",     // 左补齐
            "STRING.PADRIGHT", "PADRIGHT",   // 右补齐
            
            // === DateTime 日期时间函数 (支持 DateTime.XXX 形式) ===
            "DATETIME.NOW",                  // 当前时间
            "DATETIME.TODAY",                // 今天日期
            "DATETIME.TOSTRING",             // 日期格式化
            "DATETIME.PARSE",                // 日期解析
            "DATETIME.ADDDAYS", "ADDDAYS",   // 增加天数
            "DATETIME.ADDHOURS", "ADDHOURS", // 增加小时
            "DATETIME.ADDMINUTES", "ADDMINUTES", // 增加分钟
            
            // === Convert 类型转换函数 (支持 Convert.XXX 形式) ===
            "CONVERT.TODOUBLE", "TODOUBLE",  // 转换为 Double
            "CONVERT.TOINT32", "TOINT32", "TOINT",  // 转换为 Int
            "CONVERT.TOSTRING", "TOSTRING",  // 转换为 String
            "CONVERT.TOBOOLEAN", "TOBOOLEAN", "TOBOOL", // 转换为 Boolean
            "CONVERT.TODECIMAL", "TODECIMAL", // 转换为 Decimal
            
            // === 条件逻辑函数 ===
            "IF",                            // 条件判断
            "ISNULL",                        // 空值判断
            "ISEMPTY",                       // 空字符串判断
            "ISNULLOREMPTY",                 // 空值或空字符串判断
        };

        // 变量引用模式 {VariableName}
        private readonly Regex _variablePattern = new(@"\{(\w+)\}", RegexOptions.Compiled);

        // 字符串字面量模式 "string"
        private readonly Regex _stringLiteralPattern = new(@"""([^""\\]*(\\.[^""\\]*)*)""", RegexOptions.Compiled);

        // 数值字面量模式
        private readonly Regex _numberLiteralPattern = new(@"\b\d+(\.\d+)?\b", RegexOptions.Compiled);

        // 函数调用模式 FUNCTION(args) 或 Class.FUNCTION(args)
        private readonly Regex _functionPattern = new(@"\b(\w+(?:\.\w+)?)\s*\(([^)]*)\)", RegexOptions.Compiled);

        /// <summary>
        /// 验证表达式
        /// </summary>
        /// <param name="expression">要验证的表达式</param>
        /// <param name="context">验证上下文</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateExpression(string expression, ValidationContext context = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    return ValidationResult.Error("表达式不能为空");
                }

                var result = new ValidationResult { IsValid = true };
                var warnings = new List<string>();
                var errors = new List<string>();

                // 1. 基础语法检查
                var syntaxResult = ValidateSyntax(expression);
                if (!syntaxResult.IsValid)
                {
                    errors.AddRange(syntaxResult.Errors);
                }

                // 2. 变量存在性检查
                var variableResult = ValidateVariableReferences(expression);
                if (!variableResult.IsValid)
                {
                    errors.AddRange(variableResult.Errors);
                }
                warnings.AddRange(variableResult.Warnings);

                // 3. 类型兼容性检查 
                if (context?.TargetVariableType != null)
                {
                    var typeResult = ValidateTypeCompatibility(expression, context.TargetVariableType);
                    if (!typeResult.IsValid)
                    {
                        errors.AddRange(typeResult.Errors);
                    }
                    warnings.AddRange(typeResult.Warnings);
                }

                // 4. 循环依赖检查
                if (context?.TargetVariableName != null)
                {
                    var dependencyResult = ValidateCircularDependency(expression, context.TargetVariableName);
                    if (!dependencyResult.IsValid)
                    {
                        errors.AddRange(dependencyResult.Errors);
                    }
                }

                // 5. 安全性检查
                var securityResult = ValidateSecurity(expression);
                if (!securityResult.IsValid)
                {
                    errors.AddRange(securityResult.Errors);
                }

                // 6. 函数调用验证
                var functionResult = ValidateFunctionCalls(expression);
                if (!functionResult.IsValid)
                {
                    errors.AddRange(functionResult.Errors);
                }
                warnings.AddRange(functionResult.Warnings);

                result.IsValid = errors.Count == 0;
                result.Errors = errors;
                result.Warnings = warnings;
                result.Message = GenerateResultMessage(result);

                _logger?.LogDebug("表达式验证完成: {Expression}, 结果: {IsValid}", expression, result.IsValid);

                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证表达式时发生错误: {Expression}", expression);
                return ValidationResult.Error($"验证过程发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 计算表达式预期结果
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>计算结果</returns>
        public CalculationResult CalculateExpectedValue(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    return new CalculationResult { Success = false, ErrorMessage = "表达式为空" };
                }

                // 先验证表达式
                var validationResult = ValidateExpression(expression);
                if (!validationResult.IsValid)
                {
                    return new CalculationResult
                    {
                        Success = false,
                        ErrorMessage = "表达式验证失败: " + string.Join(", ", validationResult.Errors)
                    };
                }

                // 替换变量引用
                var processedExpression = ProcessVariableReferences(expression);

                // 处理字符串连接
                processedExpression = ProcessStringConcatenation(processedExpression);

                // 处理函数调用
                processedExpression = ProcessFunctionCalls(processedExpression);

                // 如果是纯字符串字面量，直接返回
                if (_stringLiteralPattern.IsMatch(processedExpression.Trim()) &&
                    _stringLiteralPattern.Match(processedExpression.Trim()).Value == processedExpression.Trim())
                {
                    return new CalculationResult
                    {
                        Success = true,
                        Value = processedExpression.Trim().Trim('"'),
                        ValueType = typeof(string)
                    };
                }

                // 尝试数值计算
                if (TryEvaluateNumericExpression(processedExpression, out var numericResult))
                {
                    return new CalculationResult
                    {
                        Success = true,
                        Value = numericResult,
                        ValueType = numericResult.GetType()
                    };
                }

                // 尝试布尔计算
                if (TryEvaluateBooleanExpression(processedExpression, out var boolResult))
                {
                    return new CalculationResult
                    {
                        Success = true,
                        Value = boolResult,
                        ValueType = typeof(bool)
                    };
                }

                // 作为字符串处理
                return new CalculationResult
                {
                    Success = true,
                    Value = processedExpression,
                    ValueType = typeof(string)
                };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "计算表达式时发生错误: {Expression}", expression);
                return new CalculationResult
                {
                    Success = false,
                    ErrorMessage = $"计算失败: {ex.Message}"
                };
            }
        }

        #region 私有验证方法

        /// <summary>
        /// 验证基础语法
        /// </summary>
        private ValidationResult ValidateSyntax(string expression)
        {
            var result = new ValidationResult { IsValid = true };
            var errors = new List<string>();

            try
            {
                // 检查括号匹配
                if (!IsParenthesesBalanced(expression))
                {
                    errors.Add("括号不匹配");
                }

                // 检查引号匹配
                if (!IsQuotesBalanced(expression))
                {
                    errors.Add("引号不匹配");
                }

                // 检查连续运算符
                if (HasConsecutiveOperators(expression))
                {
                    errors.Add("存在连续的运算符");
                }

                // 检查无效字符
                var invalidChars = GetInvalidCharacters(expression);
                if (invalidChars.Count != 0)
                {
                    errors.Add($"包含无效字符: {string.Join(", ", invalidChars)}");
                }

                result.IsValid = errors.Count == 0;
                result.Errors = errors;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证语法时发生错误");
                result.IsValid = false;
                result.Errors = ["语法验证过程发生错误"];
            }

            return result;
        }

        /// <summary>
        /// 验证变量引用
        /// </summary>
        private ValidationResult ValidateVariableReferences(string expression)
        {
            var result = new ValidationResult { IsValid = true };
            var errors = new List<string>();
            var warnings = new List<string>();

            try
            {
                var matches = _variablePattern.Matches(expression);
                var allVariables = _variableManager.GetAllVariables();

                foreach (Match match in matches)
                {
                    var variableName = match.Groups[1].Value;
                    var variable = allVariables.FirstOrDefault(v => v.VarName.Equals(variableName, StringComparison.OrdinalIgnoreCase));

                    if (variable == null)
                    {
                        errors.Add($"变量 '{variableName}' 不存在");
                    }
                    else
                    {
                        // 检查变量是否有值
                        if (variable.VarValue == null)
                        {
                            warnings.Add($"变量 '{variableName}' 当前值为空");
                        }
                    }
                }

                result.IsValid = errors.Count == 0;
                result.Errors = errors;
                result.Warnings = warnings;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证变量引用时发生错误");
                result.IsValid = false;
                result.Errors = ["变量引用验证过程发生错误"];
            }

            return result;
        }

        /// <summary>
        /// 验证类型兼容性
        /// </summary>
        private ValidationResult ValidateTypeCompatibility(string expression, string targetType)
        {
            var result = new ValidationResult { IsValid = true };
            var errors = new List<string>();
            var warnings = new List<string>();

            try
            {
                // 获取表达式的推断类型
                var inferredType = InferExpressionType(expression);

                if (inferredType != null && !IsTypeCompatible(inferredType, targetType))
                {
                    warnings.Add($"表达式类型 '{inferredType}' 与目标类型 '{targetType}' 可能不兼容");
                }

                result.Warnings = warnings;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证类型兼容性时发生错误");
                warnings.Add("类型兼容性检查失败");
                result.Warnings = warnings;
            }

            return result;
        }

        /// <summary>
        /// 验证循环依赖
        /// </summary>
        private ValidationResult ValidateCircularDependency(string expression, string targetVariableName)
        {
            var result = new ValidationResult { IsValid = true };
            var errors = new List<string>();

            try
            {
                var referencedVariables = GetReferencedVariables(expression);

                if (referencedVariables.Any(v => v.Equals(targetVariableName, StringComparison.OrdinalIgnoreCase)))
                {
                    errors.Add($"检测到循环依赖: 变量 '{targetVariableName}' 不能引用自身");
                }

                // TODO: 实现更深层的循环依赖检测
                // 这需要分析所有变量的依赖关系图

                result.IsValid = errors.Count == 0;
                result.Errors = errors;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证循环依赖时发生错误");
                result.IsValid = false;
                result.Errors = new List<string> { "循环依赖检查失败" };
            }

            return result;
        }

        /// <summary>
        /// 安全性验证
        /// </summary>
        private ValidationResult ValidateSecurity(string expression)
        {
            var result = new ValidationResult { IsValid = true };
            var errors = new List<string>();

            try
            {
                // 检查危险关键字
                var dangerousKeywords = new[] { "eval", "exec", "system", "cmd", "powershell", "script" };
                var lowerExpression = expression.ToLowerInvariant();

                foreach (var keyword in dangerousKeywords)
                {
                    if (lowerExpression.Contains(keyword))
                    {
                        errors.Add($"包含潜在危险关键字: '{keyword}'");
                    }
                }

                // 检查文件系统访问
                var fileSystemPatterns = new[] { @"[A-Za-z]:\\", @"\\\\", @"/[A-Za-z]+/" };
                foreach (var pattern in fileSystemPatterns)
                {
                    if (Regex.IsMatch(expression, pattern))
                    {
                        errors.Add("表达式包含可能的文件系统路径");
                        break;
                    }
                }

                result.IsValid = errors.Count == 0;
                result.Errors = errors;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "安全性验证时发生错误");
                result.IsValid = false;
                result.Errors = new List<string> { "安全性检查失败" };
            }

            return result;
        }

        /// <summary>
        /// 验证函数调用
        /// </summary>
        private ValidationResult ValidateFunctionCalls(string expression)
        {
            var result = new ValidationResult { IsValid = true };
            var errors = new List<string>();
            var warnings = new List<string>();

            try
            {
                var matches = _functionPattern.Matches(expression);

                foreach (Match match in matches)
                {
                    var functionName = match.Groups[1].Value.ToUpperInvariant();
                    var arguments = match.Groups[2].Value;

                    // 检查是否为支持的函数
                    if (!_supportedFunctions.Contains(functionName))
                    {
                        errors.Add($"不支持的函数: '{functionName}'");
                        continue;
                    }

                    // 验证函数参数
                    var argValidationResult = ValidateFunctionArguments(functionName, arguments);
                    if (!argValidationResult.IsValid)
                    {
                        errors.AddRange(argValidationResult.Errors);
                    }
                    warnings.AddRange(argValidationResult.Warnings);
                }

                result.IsValid = errors.Count == 0;
                result.Errors = errors;
                result.Warnings = warnings;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证函数调用时发生错误");
                result.IsValid = false;
                result.Errors = new List<string> { "函数调用验证失败" };
            }

            return result;
        }

        /// <summary>
        /// 验证函数参数
        /// </summary>
        private ValidationResult ValidateFunctionArguments(string functionName, string arguments)
        {
            var result = new ValidationResult { IsValid = true };
            // 这里可以实现具体的函数参数验证逻辑
            return result;
        }

        /// <summary>
        /// 生成验证结果消息
        /// </summary>
        private string GenerateResultMessage(ValidationResult result)
        {
            var messages = new List<string>();

            if (result.IsValid)
            {
                messages.Add("✅ 表达式验证通过");
            }
            else
            {
                messages.Add("❌ 表达式验证失败");
            }

            if (result.Errors?.Any() == true)
            {
                messages.AddRange(result.Errors.Select(e => $"❌ {e}"));
            }

            if (result.Warnings?.Any() == true)
            {
                messages.AddRange(result.Warnings.Select(w => $"⚠️ {w}"));
            }

            return string.Join(Environment.NewLine, messages);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查括号是否平衡
        /// </summary>
        private bool IsParenthesesBalanced(string expression)
        {
            int count = 0;
            foreach (char c in expression)
            {
                if (c == '(') count++;
                if (c == ')') count--;
                if (count < 0) return false;
            }
            return count == 0;
        }

        /// <summary>
        /// 检查引号是否平衡
        /// </summary>
        private bool IsQuotesBalanced(string expression)
        {
            int count = 0;
            bool escaped = false;
            foreach (char c in expression)
            {
                if (c == '\\' && !escaped)
                {
                    escaped = true;
                    continue;
                }
                if (c == '"' && !escaped)
                {
                    count++;
                }
                escaped = false;
            }
            return count % 2 == 0;
        }

        /// <summary>
        /// 检查是否存在连续运算符
        /// </summary>
        private bool HasConsecutiveOperators(string expression)
        {
            // 移除字符串字面量后检查
            var withoutStrings = RemoveStringLiterals(expression);

            // 检查连续的运算符（排除负号和逻辑非）
            var operatorPattern = new Regex(@"[+\-*/%][\s]*[+*/%]|[=!<>]{3,}");
            return operatorPattern.IsMatch(withoutStrings);
        }

        /// <summary>
        /// 获取无效字符
        /// </summary>
        private List<char> GetInvalidCharacters(string expression)
        {
            var validChars = new HashSet<char>(
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" +
                "+-*/%=!<>&|(){}[].,;:\"' \t\n\r_"
            );

            return expression.Where(c => !validChars.Contains(c)).Distinct().ToList();
        }

        /// <summary>
        /// 获取表达式中引用的变量
        /// </summary>
        private List<string> GetReferencedVariables(string expression)
        {
            var variables = new List<string>();
            var matches = _variablePattern.Matches(expression);

            foreach (Match match in matches)
            {
                variables.Add(match.Groups[1].Value);
            }

            return variables.Distinct().ToList();
        }

        /// <summary>
        /// 尝试计算数值表达式
        /// </summary>
        private bool TryEvaluateNumericExpression(string expression, out object result)
        {
            result = null;
            try
            {
                // 这里应该使用专业的表达式计算引擎
                // 简单示例：只处理基本的数值运算

                if (double.TryParse(expression, out var numValue))
                {
                    result = numValue;
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 尝试计算布尔表达式
        /// </summary>
        private bool TryEvaluateBooleanExpression(string expression, out bool result)
        {
            result = false;
            try
            {
                if (bool.TryParse(expression, out result))
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 移除字符串字面量
        /// </summary>
        private string RemoveStringLiterals(string expression)
        {
            return _stringLiteralPattern.Replace(expression, "\"\"");
        }

        /// <summary>
        /// 处理变量引用
        /// </summary>
        private string ProcessVariableReferences(string expression)
        {
            return _variablePattern.Replace(expression, match =>
            {
                var variableName = match.Groups[1].Value;
                var variable = _variableManager.GetAllVariables()
                    .FirstOrDefault(v => v.VarName.Equals(variableName, StringComparison.OrdinalIgnoreCase));

                if (variable?.VarValue != null)
                {
                    // 根据类型格式化值
                    if (variable.VarType == "String")
                        return $"\"{variable.VarValue}\"";
                    return variable.VarValue.ToString();
                }

                return "null";
            });
        }

        /// <summary>
        /// 处理字符串连接
        /// </summary>
        private string ProcessStringConcatenation(string expression)
        {
            // 简化的字符串连接处理
            // 实际应该使用更复杂的解析逻辑
            return expression;
        }

        /// <summary>
        /// 处理函数调用
        /// </summary>
        private string ProcessFunctionCalls(string expression)
        {
            return _functionPattern.Replace(expression, match =>
            {
                var functionName = match.Groups[1].Value.ToUpperInvariant();
                var arguments = match.Groups[2].Value;

                // 执行函数（如果支持）
                return ExecuteFunction(functionName, arguments);
            });
        }

        /// <summary>
        /// 执行函数
        /// </summary>
        private string ExecuteFunction(string functionName, string arguments)
        {
            var upperFunctionName = functionName.ToUpperInvariant();

            switch (upperFunctionName)
            {
                // ===== 日期时间函数 =====
                case "NOW":
                case "DATETIME.NOW":
                    return $"\"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\"";

                case "TODAY":
                case "DATETIME.TODAY":
                    return $"\"{DateTime.Today:yyyy-MM-dd}\"";

                // ===== 字符串函数 =====
                case "LEN":
                case "LENGTH":
                case "STRING.LENGTH":
                    var lenArg = arguments.Trim().Trim('"');
                    return lenArg.Length.ToString();

                case "UPPER":
                case "TOUPPER":
                case "STRING.TOUPPER":
                    var upperArg = arguments.Trim().Trim('"');
                    return $"\"{upperArg.ToUpperInvariant()}\"";

                case "LOWER":
                case "TOLOWER":
                case "STRING.TOLOWER":
                    var lowerArg = arguments.Trim().Trim('"');
                    return $"\"{lowerArg.ToLowerInvariant()}\"";

                case "TRIM":
                case "STRING.TRIM":
                    var trimArg = arguments.Trim().Trim('"');
                    return $"\"{trimArg.Trim()}\"";

                case "SUBSTRING":
                case "STRING.SUBSTRING":
                    // 简化处理,实际应该解析参数
                    return $"\"{arguments}\"";

                case "REPLACE":
                case "STRING.REPLACE":
                    // 简化处理,实际应该解析参数
                    return $"\"{arguments}\"";

                // ===== 数学函数 =====
                case "ROUND":
                case "MATH.ROUND":
                    var roundArgs = ParseFunctionArguments(arguments);
                    if (roundArgs.Count >= 1 && double.TryParse(roundArgs[0], out var roundValue))
                    {
                        int digits = 0;
                        if (roundArgs.Count >= 2)
                            int.TryParse(roundArgs[1], out digits);
                        return Math.Round(roundValue, digits).ToString();
                    }
                    return arguments;

                case "ABS":
                case "MATH.ABS":
                    if (double.TryParse(arguments.Trim(), out var absValue))
                        return Math.Abs(absValue).ToString();
                    return arguments;

                case "MAX":
                case "MATH.MAX":
                    var maxArgs = ParseFunctionArguments(arguments);
                    if (maxArgs.Count >= 2 &&
                        double.TryParse(maxArgs[0], out var max1) &&
                        double.TryParse(maxArgs[1], out var max2))
                    {
                        return Math.Max(max1, max2).ToString();
                    }
                    return arguments;

                case "MIN":
                case "MATH.MIN":
                    var minArgs = ParseFunctionArguments(arguments);
                    if (minArgs.Count >= 2 &&
                        double.TryParse(minArgs[0], out var min1) &&
                        double.TryParse(minArgs[1], out var min2))
                    {
                        return Math.Min(min1, min2).ToString();
                    }
                    return arguments;

                case "FLOOR":
                case "MATH.FLOOR":
                    if (double.TryParse(arguments.Trim(), out var floorValue))
                        return Math.Floor(floorValue).ToString();
                    return arguments;

                case "CEILING":
                case "MATH.CEILING":
                    if (double.TryParse(arguments.Trim(), out var ceilingValue))
                        return Math.Ceiling(ceilingValue).ToString();
                    return arguments;

                case "SQRT":
                case "MATH.SQRT":
                    if (double.TryParse(arguments.Trim(), out var sqrtValue) && sqrtValue >= 0)
                        return Math.Sqrt(sqrtValue).ToString();
                    return arguments;

                case "POW":
                case "MATH.POW":
                    var powArgs = ParseFunctionArguments(arguments);
                    if (powArgs.Count >= 2 &&
                        double.TryParse(powArgs[0], out var powBase) &&
                        double.TryParse(powArgs[1], out var powExp))
                    {
                        return Math.Pow(powBase, powExp).ToString();
                    }
                    return arguments;

                // ===== 类型转换函数 =====
                case "TOINT":
                case "TOINT32":
                case "CONVERT.TOINT32":
                    if (double.TryParse(arguments.Trim().Trim('"'), out var intValue))
                        return ((int)intValue).ToString();
                    return arguments;

                case "TODOUBLE":
                case "CONVERT.TODOUBLE":
                    if (double.TryParse(arguments.Trim().Trim('"'), out var dblValue))
                        return dblValue.ToString();
                    return arguments;

                case "TOSTRING":
                case "CONVERT.TOSTRING":
                    return $"\"{arguments.Trim().Trim('"')}\"";

                // ===== 条件逻辑函数 =====
                case "IF":
                    // IF 函数需要特殊处理,这里简化返回
                    return arguments;

                case "ISNULL":
                    var isnullArgs = ParseFunctionArguments(arguments);
                    if (isnullArgs.Count >= 2)
                    {
                        var value = isnullArgs[0].Trim();
                        var defaultValue = isnullArgs[1].Trim();
                        return string.IsNullOrEmpty(value) || value == "null" ? defaultValue : value;
                    }
                    return arguments;

                case "ISEMPTY":
                    var isemptyArg = arguments.Trim().Trim('"');
                    return string.IsNullOrEmpty(isemptyArg) ? "true" : "false";

                // ===== 默认处理 =====
                default:
                    _logger?.LogWarning("未实现的函数执行: {FunctionName}({Arguments})", functionName, arguments);
                    // 对于未实现的函数,返回原始函数调用字符串
                    return $"{functionName}({arguments})";
            }
        }

        /// <summary>
        /// 解析函数参数
        /// </summary>
        private List<string> ParseFunctionArguments(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
                return new List<string>();

            // 简单的参数分割 (不处理嵌套函数和字符串中的逗号)
            var args = new List<string>();
            var currentArg = new StringBuilder();
            int nestedLevel = 0;
            bool inString = false;

            foreach (char c in arguments)
            {
                if (c == '"')
                {
                    inString = !inString;
                    currentArg.Append(c);
                }
                else if (c == '(' && !inString)
                {
                    nestedLevel++;
                    currentArg.Append(c);
                }
                else if (c == ')' && !inString)
                {
                    nestedLevel--;
                    currentArg.Append(c);
                }
                else if (c == ',' && nestedLevel == 0 && !inString)
                {
                    args.Add(currentArg.ToString().Trim());
                    currentArg.Clear();
                }
                else
                {
                    currentArg.Append(c);
                }
            }

            if (currentArg.Length > 0)
            {
                args.Add(currentArg.ToString().Trim());
            }

            return args;
        }

        /// <summary>
        /// 推断表达式类型
        /// </summary>
        private string InferExpressionType(string expression)
        {
            try
            {
                // 如果是字符串字面量
                if (_stringLiteralPattern.IsMatch(expression))
                    return "String";

                // 如果是数值字面量
                if (_numberLiteralPattern.IsMatch(expression))
                    return expression.Contains(".") ? "Double" : "Int";

                // 如果包含逻辑运算符
                if (expression.Contains("==") || expression.Contains("!=") ||
                    expression.Contains("&&") || expression.Contains("||"))
                    return "Boolean";

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 检查类型兼容性
        /// </summary>
        private bool IsTypeCompatible(string sourceType, string targetType)
        {
            if (sourceType.Equals(targetType, StringComparison.OrdinalIgnoreCase))
                return true;

            // 数值类型之间可以转换
            var numericTypes = new[] { "Int", "Double", "Float", "Decimal", "Long" };
            if (numericTypes.Contains(sourceType) && numericTypes.Contains(targetType))
                return true;

            return false;
        }

        #endregion
    }

    #region 辅助类定义

    /// <summary>
    /// 验证结果
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// 错误信息列表
        /// </summary>
        public List<string> Errors { get; set; } = [];

        /// <summary>
        /// 提醒信息列表
        /// </summary>
        public List<string> Warnings { get; set; } = [];

        /// <summary>
        /// 消息摘要
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 创建错误结果
        /// </summary>
        public static ValidationResult Error(string message)
        {
            return new ValidationResult
            {
                IsValid = false,
                Errors = [message],
                Message = message
            };
        }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        public static ValidationResult Success(string message = "验证通过")
        {
            return new ValidationResult
            {
                IsValid = true,
                Message = message
            };
        }
    }

    /// <summary>
    /// 验证上下文
    /// </summary>
    public class ValidationContext
    {
        /// <summary>
        /// 目标变量名称（用于循环依赖检查）
        /// </summary>
        public string TargetVariableName { get; set; }

        /// <summary>
        /// 目标变量类型（用于类型兼容性检查）
        /// </summary>
        public string TargetVariableType { get; set; }
    }

    /// <summary>
    /// 计算结果
    /// </summary>
    public class CalculationResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 计算结果值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 结果类型
        /// </summary>
        public Type ValueType { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    #endregion
}
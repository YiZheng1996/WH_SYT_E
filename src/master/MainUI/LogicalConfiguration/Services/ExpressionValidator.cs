using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
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

        // 支持的函数
        private readonly string[] _supportedFunctions = { "LEN", "SUBSTRING", "UPPER", "LOWER", "TRIM", "REPLACE", "NOW", "FORMAT" };

        // 变量引用模式 {VariableName}
        private readonly Regex _variablePattern = new Regex(@"\{(\w+)\}", RegexOptions.Compiled);

        // 字符串字面量模式 "string"
        private readonly Regex _stringLiteralPattern = new Regex(@"""([^""\\]*(\\.[^""\\]*)*)""", RegexOptions.Compiled);

        // 数值字面量模式
        private readonly Regex _numberLiteralPattern = new Regex(@"\b\d+(\.\d+)?\b", RegexOptions.Compiled);

        // 函数调用模式 FUNCTION(args)
        private readonly Regex _functionPattern = new Regex(@"\b(\w+)\s*\(([^)]*)\)", RegexOptions.Compiled);

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

                // 预留类型检查，暂时不需要
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
                result.Errors = new List<string> { "语法验证过程发生错误" };
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

        #endregion

        #region 私有辅助方法

        /// <summary>
        /// 检查括号是否平衡
        /// </summary>
        private bool IsParenthesesBalanced(string expression)
        {
            int count = 0;
            bool inString = false;
            char stringChar = '\0';

            for (int i = 0; i < expression.Length; i++)
            {
                var c = expression[i];

                if (!inString)
                {
                    if (c == '"' || c == '\'')
                    {
                        inString = true;
                        stringChar = c;
                    }
                    else if (c == '(')
                    {
                        count++;
                    }
                    else if (c == ')')
                    {
                        count--;
                        if (count < 0) return false;
                    }
                }
                else
                {
                    if (c == stringChar && (i == 0 || expression[i - 1] != '\\'))
                    {
                        inString = false;
                    }
                }
            }

            return count == 0 && !inString;
        }

        /// <summary>
        /// 检查引号是否平衡
        /// </summary>
        private bool IsQuotesBalanced(string expression)
        {
            int doubleQuoteCount = 0;
            int singleQuoteCount = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '"' && (i == 0 || expression[i - 1] != '\\'))
                {
                    doubleQuoteCount++;
                }
                else if (expression[i] == '\'' && (i == 0 || expression[i - 1] != '\\'))
                {
                    singleQuoteCount++;
                }
            }

            return doubleQuoteCount % 2 == 0 && singleQuoteCount % 2 == 0;
        }

        /// <summary>
        /// 检查是否有连续运算符
        /// </summary>
        private bool HasConsecutiveOperators(string expression)
        {
            var cleanExpression = RemoveStringLiterals(expression);
            var operators = new[] { "+", "-", "*", "/", "=", "!", ">", "<", "&", "|" };

            for (int i = 0; i < cleanExpression.Length - 1; i++)
            {
                if (operators.Any(op => cleanExpression[i].ToString() == op) &&
                    operators.Any(op => cleanExpression[i + 1].ToString() == op))
                {
                    // 允许的组合：==, !=, >=, <=, &&, ||
                    var combo = cleanExpression.Substring(i, 2);
                    if (combo != "==" && combo != "!=" && combo != ">=" &&
                        combo != "<=" && combo != "&&" && combo != "||")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 获取无效字符
        /// </summary>
        private List<char> GetInvalidCharacters(string expression)
        {
            var validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789{}()[]\"'+-*/%=!<>&|.,_: \t\r\n";
            return expression.Where(c => !validChars.Contains(c)).Distinct().ToList();
        }

        /// <summary>
        /// 处理变量引用
        /// </summary>
        private string ProcessVariableReferences(string expression)
        {
            var allVariables = _variableManager.GetAllVariables();

            return _variablePattern.Replace(expression, match =>
            {
                var variableName = match.Groups[1].Value;
                var variable = allVariables.FirstOrDefault(v => v.VarName.Equals(variableName, StringComparison.OrdinalIgnoreCase));

                if (variable?.VarValue != null)
                {
                    // 如果是字符串值，需要添加引号
                    if (variable.VarType.Equals("String", StringComparison.OrdinalIgnoreCase))
                    {
                        return $"\"{variable.VarValue}\"";
                    }
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
            // 简单的字符串连接处理
            // 实际项目中可能需要更复杂的解析
            return expression.Replace(" + ", "");
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

                try
                {
                    return ExecuteFunction(functionName, arguments);
                }
                catch (Exception ex)
                {
                    _logger?.LogWarning(ex, "执行函数 {FunctionName} 时发生错误", functionName);
                    return match.Value; // 返回原始值
                }
            });
        }

        /// <summary>
        /// 执行函数
        /// </summary>
        private string ExecuteFunction(string functionName, string arguments)
        {
            switch (functionName)
            {
                case "NOW":
                    return $"\"{DateTime.Now}\"";

                case "LEN":
                    var lenArg = arguments.Trim().Trim('"');
                    return lenArg.Length.ToString();

                case "UPPER":
                    var upperArg = arguments.Trim().Trim('"');
                    return $"\"{upperArg.ToUpperInvariant()}\"";

                case "LOWER":
                    var lowerArg = arguments.Trim().Trim('"');
                    return $"\"{lowerArg.ToLowerInvariant()}\"";

                case "TRIM":
                    var trimArg = arguments.Trim().Trim('"');
                    return $"\"{trimArg.Trim()}\"";

                // 更多函数实现...

                default:
                    return arguments; // 不支持的函数返回参数
            }
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
                    return expression.Contains(".") ? "Double" : "Integer";

                // 如果包含布尔运算符
                if (expression.Contains("==") || expression.Contains("!=") ||
                    expression.Contains("&&") || expression.Contains("||"))
                    return "Boolean";

                // 基于变量引用推断
                var variables = GetReferencedVariables(expression);
                if (variables.Count == 1)
                {
                    var allVars = _variableManager.GetAllVariables();
                    var variable = allVars.FirstOrDefault(v => v.VarName == variables[0]);
                    return variable?.VarType;
                }

                return "String"; // 默认为字符串类型
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
            if (string.IsNullOrEmpty(sourceType) || string.IsNullOrEmpty(targetType))
                return true;

            // 字符串类型可以接受任何类型
            if (targetType.Equals("String", StringComparison.OrdinalIgnoreCase))
                return true;

            // 相同类型
            if (sourceType.Equals(targetType, StringComparison.OrdinalIgnoreCase))
                return true;

            // 数值类型兼容性
            var numericTypes = new[] { "Integer", "Double", "Decimal", "Float" };
            if (numericTypes.Contains(sourceType, StringComparer.OrdinalIgnoreCase) &&
                numericTypes.Contains(targetType, StringComparer.OrdinalIgnoreCase))
                return true;

            return false;
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

        public static ValidationResult Error(string message)
        {
            return new ValidationResult
            {
                IsValid = false,
                Errors = new List<string> { message },
                Message = message
            };
        }

        public static ValidationResult Success(string message = "验证成功")
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
        /// 目标变量名称
        /// </summary>
        public string TargetVariableName { get; set; }

        /// <summary>
        /// 目标变量类型
        /// </summary>
        public string TargetVariableType { get; set; }

        /// <summary>
        /// 附加上下文
        /// </summary>
        public Dictionary<string, object> AdditionalContext { get; set; } = [];
    }

    /// <summary>
    /// 计算结果
    /// </summary>
    public class CalculationResult
    {
        /// <summary>
        /// 计算结果
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 计算值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 计算值类型
        /// </summary>
        public Type ValueType { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            if (!Success)
                return $"计算失败: {ErrorMessage}";

            return Value?.ToString() ?? "null";
        }
    }

    #endregion
}
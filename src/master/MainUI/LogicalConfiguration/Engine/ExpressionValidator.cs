using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 表达式验证器 - 负责验证表达式的合法性
    /// </summary>
    internal class ExpressionValidator(
        GlobalVariableManager variableManager,
        FunctionRegistry functionRegistry,
        ILogger logger = null)
    {
        private readonly GlobalVariableManager _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
        private readonly FunctionRegistry _functionRegistry = functionRegistry ?? throw new ArgumentNullException(nameof(functionRegistry));

        // 正则表达式模式
        private readonly Regex _variablePattern = new(@"\{([^}]+)\}", RegexOptions.Compiled);
        private readonly Regex _functionPattern = new(@"([\w\.]+)\s*\(([^)]*)\)", RegexOptions.Compiled);
        private readonly Regex _stringLiteralPattern = new(@"""([^""\\]*(\\.[^""\\]*)*)""", RegexOptions.Compiled);

        // 支持的运算符
        private readonly string[] _supportedOperators =
        [
            "+", "-", "*", "/", "%", "==", "!=", ">", "<", ">=", "<=", "&&", "||", "!"
        ];

        /// <summary>
        /// 验证表达式的合法性
        /// </summary>
        public ValidationResult Validate(string expression, ValidationContext context = null)
        {
            var result = new ValidationResult { IsValid = true };

            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    result.IsValid = false;
                    result.Message = "表达式不能为空";
                    return result;
                }

                var label = context != null && !string.IsNullOrWhiteSpace(context.ValidationLabel)
                    ? $"{context.ValidationLabel}: {expression}"
                    : expression;

                logger?.LogDebug("开始验证表达式: {Expression}", label);

                // 1. 检查无效字符
                if (!ValidateCharacters(expression, result))
                    return result;

                // 2. 检查括号匹配
                if (!ValidateParentheses(expression, result))
                    return result;

                // 3. 检查变量存在性
                if (!ValidateVariables(expression, context, result))
                    return result;

                // 4. 检查函数调用
                if (!ValidateFunctions(expression, result))
                    return result;

                // 5. 检查运算符使用
                if (!ValidateOperators(expression, result))
                    return result;

                // 6. 类型兼容性检查（如果提供了上下文）
                if (context != null && !ValidateTypeCompatibility(expression, context, result))
                    return result;

                result.Message = "表达式验证通过";
                logger?.LogDebug("表达式验证成功: {Expression}", expression);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "验证表达式时发生错误: {Expression}", expression);
                result.IsValid = false;
                result.Message = $"验证失败: {ex.Message}";
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        #region 验证子方法

        /// <summary>
        /// 验证字符合法性
        /// </summary>
        private bool ValidateCharacters(string expression, ValidationResult result)
        {
            var invalidChars = GetInvalidCharacters(expression);
            if (invalidChars.Count != 0)
            {
                result.IsValid = false;
                result.Message = $"表达式包含无效字符: {string.Join(", ", invalidChars)}";
                result.Errors.Add($"无效字符: {string.Join(", ", invalidChars)}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证括号匹配
        /// </summary>
        private bool ValidateParentheses(string expression, ValidationResult result)
        {
            if (!CheckParenthesesBalance(expression))
            {
                result.IsValid = false;
                result.Message = "括号不匹配";
                result.Errors.Add("括号数量或顺序不正确");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证变量存在性
        /// </summary>
        private bool ValidateVariables(string expression, ValidationContext context, ValidationResult result)
        {
            var referencedVars = GetReferencedVariables(expression);

            // 如果有上下文，应用白名单过滤
            var varsToCheck = context != null
                ? FilterVariablesByWhitelist(referencedVars, context)
                : referencedVars;

            var missingVars = varsToCheck.Where(v =>
            {
                // 跳过 PLC 地址格式的检查
                if (v.StartsWith("PLC.", StringComparison.OrdinalIgnoreCase))
                    return false;

                var variable = _variableManager.TryFindVariableByName(v);
                return variable == null;
            }).ToList();

            if (missingVars.Count > 0)
            {
                result.IsValid = false;
                result.Message = $"以下变量不存在: {string.Join(", ", missingVars)}";
                result.Errors.AddRange(missingVars.Select(v => $"变量 '{v}' 未定义"));

                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证函数调用
        /// </summary>
        private bool ValidateFunctions(string expression, ValidationResult result)
        {
            var functionMatches = _functionPattern.Matches(expression);
            foreach (Match match in functionMatches)
            {
                var funcName = match.Groups[1].Value.ToUpper();
                if (!_functionRegistry.IsSupported(funcName))
                {
                    result.IsValid = false;
                    result.Message = $"不支持的函数: {funcName}";
                    result.Errors.Add($"函数 '{funcName}' 未定义");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 验证运算符使用
        /// </summary>
        private bool ValidateOperators(string expression, ValidationResult result)
        {
            var withoutStrings = RemoveStringLiterals(expression);
            if (!HasValidOperatorUsage(withoutStrings))
            {
                result.IsValid = false;
                result.Message = "运算符使用不当";
                result.Errors.Add("请检查运算符的位置和用法");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证类型兼容性
        /// </summary>
        private bool ValidateTypeCompatibility(string expression, ValidationContext context, ValidationResult result)
        {
            if (context.TargetVariableType == null)
                return true;

            // 检查目标变量是否存在
            if (!string.IsNullOrWhiteSpace(context.TargetVariableName))
            {
                var targetVar = _variableManager.TryFindVariableByName(context.TargetVariableName);
                if (targetVar == null)
                {
                    result.IsValid = false;
                    result.Message = $"目标变量 '{context.TargetVariableName}' 不存在";
                    result.Errors.Add($"目标变量 '{context.TargetVariableName}' 未定义");
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 根据白名单过滤变量
        /// </summary>
        private List<string> FilterVariablesByWhitelist(List<string> variables, ValidationContext context)
        {
            if (context.RuntimeVariableWhitelist == null || context.RuntimeVariableWhitelist.Count == 0)
                return variables;

            return [.. variables.Where(v =>
            {
                // 检查是否在白名单中（完全匹配 或 前缀匹配）
                bool isInWhitelist = context.RuntimeVariableWhitelist.Any(w =>
                    v.Equals(w, StringComparison.OrdinalIgnoreCase) ||
                    v.StartsWith(w + ".", StringComparison.OrdinalIgnoreCase)
                );

                return !isInWhitelist;  // 返回不在白名单中的变量
            })];
        }

        /// <summary>
        /// 获取表达式中引用的所有变量名
        /// </summary>
        public List<string> GetReferencedVariables(string expression)
        {
            var matches = _variablePattern.Matches(expression);
            return matches.Cast<Match>()
                         .Select(m => m.Groups[1].Value)
                         .Distinct()
                         .ToList();
        }

        /// <summary>
        /// 检查括号是否平衡
        /// </summary>
        private bool CheckParenthesesBalance(string expression)
        {
            int count = 0;
            foreach (char c in expression)
            {
                if (c == '(') count++;
                else if (c == ')') count--;
                if (count < 0) return false;
            }
            return count == 0;
        }

        /// <summary>
        /// 获取表达式中的无效字符
        /// </summary>
        private List<string> GetInvalidCharacters(string expression)
        {
            var invalidChars = new HashSet<string>();
            var validChars = new HashSet<char>(
                "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ+-*/%(){}.,<>=!&|\"' :\t\r\n"
            );  // 添加了冒号 :

            foreach (char c in expression)
            {
                // 允许 Unicode 字符（用于中文变量名）
                if (!validChars.Contains(c) && c < 128)
                {
                    invalidChars.Add(c.ToString());
                }
            }

            return invalidChars.ToList();
        }

        /// <summary>
        /// 移除字符串字面量
        /// </summary>
        private string RemoveStringLiterals(string expression)
        {
            return _stringLiteralPattern.Replace(expression, "\"\"");
        }

        /// <summary>
        /// 检查运算符使用是否有效
        /// </summary>
        private bool HasValidOperatorUsage(string expression)
        {
            // 移除空白字符
            var normalized = expression.Replace(" ", "").Replace("\t", "");

            // 检查是否有连续的运算符（除了 ==, !=, >=, <= 等合法组合）
            foreach (var op in _supportedOperators.Where(o => o.Length == 1))
            {
                if (normalized.Contains(op + op) && op != "=" && op != "&" && op != "|")
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
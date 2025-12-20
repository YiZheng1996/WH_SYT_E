using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 表达式验证器
    /// 负责验证表达式的合法性,使用共享工具消除所有重复代码
    /// 区分普通变量和PLC引用,避免将PLC点位当作变量验证
    /// </summary>
    internal class ExpressionValidator
    {
        private readonly GlobalVariableManager _variableManager;
        private readonly FunctionRegistry _functionRegistry;
        private readonly ILogger _logger;

        public ExpressionValidator(
            GlobalVariableManager variableManager,
            FunctionRegistry functionRegistry,
            ILogger logger = null)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _functionRegistry = functionRegistry ?? throw new ArgumentNullException(nameof(functionRegistry));
            _logger = logger;
        }

        #region 公共方法 - 验证入口

        /// <summary>
        /// 验证表达式的合法性
        /// </summary>
        public ValidationResult Validate(string expression, ValidationContext context = null)
        {
            var result = new ValidationResult { IsValid = true };
            context ??= new ValidationContext();

            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    return CreateError("表达式不能为空");
                }

                var label = !string.IsNullOrWhiteSpace(context.ValidationLabel)
                    ? $"{context.ValidationLabel}: {expression}"
                    : expression;

                _logger?.LogDebug("开始验证表达式: {Expression}", label);

                // 统一的验证流程 - 每个步骤独立,职责单一
                if (!ValidateCharacters(expression, result)) return result;
                if (!ValidateParentheses(expression, result)) return result;
                if (!ValidateVariables(expression, context, result)) return result;
                if (!ValidateFunctions(expression, result)) return result;
                if (!ValidateOperators(expression, result)) return result;
                if (!ValidateTypeCompatibility(expression, context, result)) return result;

                _logger?.LogDebug("表达式验证通过: {Expression}", expression);
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "表达式验证失败: {Expression}", expression);
                return CreateError($"验证过程发生错误: {ex.Message}");
            }
        }

        #endregion

        #region 私有方法 - 各项验证

        /// <summary>
        /// 验证字符有效性 - 使用共享工具
        /// </summary>
        private bool ValidateCharacters(string expression, ValidationResult result)
        {
            var invalidChars = ExpressionUtils.GetInvalidCharacters(expression);

            if (invalidChars.Count > 0)
            {
                result.IsValid = false;
                result.Message = $"表达式包含无效字符: {string.Join(", ", invalidChars)}";
                result.Errors.Add($"无效字符: {string.Join(", ", invalidChars)}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证括号匹配 - 使用共享工具
        /// </summary>
        private bool ValidateParentheses(string expression, ValidationResult result)
        {
            if (!ExpressionUtils.IsParenthesesBalanced(expression))
            {
                result.IsValid = false;
                result.Message = "括号不匹配";
                result.Errors.Add("括号数量或顺序不正确");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证变量存在性 -  区分普通变量和PLC引用
        /// </summary>
        private bool ValidateVariables(string expression, ValidationContext context, ValidationResult result)
        {
            var referencedVars = ExpressionUtils.GetReferencedVariables(expression);

            // 根据上下文白名单过滤
            var varsToCheck = FilterVariablesByWhitelist(referencedVars, context);

            // 分离普通变量和PLC引用 - 关键修复点
            var normalVariables = new List<string>();
            var plcReferences = new List<string>();

            foreach (var varName in varsToCheck)
            {
                if (ExpressionUtils.IsPLCReference(varName))
                {
                    plcReferences.Add(varName);
                }
                else
                {
                    normalVariables.Add(varName);
                }
            }

            // 只检查普通变量是否存在,PLC引用不检查
            var missingVars = normalVariables
                .Where(v => _variableManager.TryFindVariableByName(v) == null)
                .ToList();

            if (missingVars.Count > 0)
            {
                result.IsValid = false;
                result.Message = $"以下变量不存在: {string.Join(", ", missingVars)}";
                result.Errors.AddRange(missingVars.Select(v => $"变量 '{v}' 未定义"));
                return false;
            }

            // 可选: 验证PLC引用的格式是否正确
            if (context?.AllowPlcReferences == true && plcReferences.Count > 0)
            {
                var invalidPlcRefs = ValidatePLCReferences(plcReferences);
                if (invalidPlcRefs.Count > 0)
                {
                    result.AddWarning($"PLC引用格式可能不正确: {string.Join(", ", invalidPlcRefs)}");
                }
            }

            return true;
        }

        /// <summary>
        /// 验证函数调用
        /// </summary>
        private bool ValidateFunctions(string expression, ValidationResult result)
        {
            var funcNames = ExpressionUtils.GetReferencedFunctions(expression);
            var unsupportedFunctions = funcNames.Where(f => !_functionRegistry.IsSupported(f)).ToList();

            if (unsupportedFunctions.Count > 0)
            {
                result.IsValid = false;
                result.Message = $"不支持的函数: {string.Join(", ", unsupportedFunctions)}";
                result.Errors.AddRange(unsupportedFunctions.Select(f => $"函数 '{f}' 未定义"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证运算符使用
        /// </summary>
        private bool ValidateOperators(string expression, ValidationResult result)
        {
            var withoutStrings = ExpressionUtils.RemoveStringLiterals(expression);

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
            if (string.IsNullOrWhiteSpace(context?.TargetVariableName))
                return true;

            // 检查目标变量是否存在
            var targetVar = _variableManager.TryFindVariableByName(context.TargetVariableName);
            if (targetVar == null)
            {
                result.IsValid = false;
                result.Message = $"目标变量 '{context.TargetVariableName}' 不存在";
                result.Errors.Add($"目标变量 '{context.TargetVariableName}' 未定义");
                return false;
            }

            // 可以添加更多类型兼容性检查...

            return true;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 根据白名单过滤变量 - 简化的逻辑
        /// </summary>
        private List<string> FilterVariablesByWhitelist(List<string> variables, ValidationContext context)
        {
            if (context?.RuntimeVariableWhitelist == null || context.RuntimeVariableWhitelist.Count == 0)
                return variables;

            return [.. variables.Where(v =>
            {
                // 检查是否在白名单中(完全匹配或前缀匹配)
                return !context.RuntimeVariableWhitelist.Any(w =>
                    v.Equals(w, StringComparison.OrdinalIgnoreCase) ||
                    v.StartsWith(w + ".", StringComparison.OrdinalIgnoreCase));
            })];
        }

        /// <summary>
        /// 验证PLC引用格式 - 新增方法
        /// </summary>
        private List<string> ValidatePLCReferences(List<string> plcReferences)
        {
            var invalidRefs = new List<string>();

            foreach (var plcRef in plcReferences)
            {
                var parts = plcRef.Split('.');

                // PLC引用至少需要 模块名.地址 两部分
                if (parts.Length < 2)
                {
                    invalidRefs.Add(plcRef);
                    continue;
                }

                // 如果以PLC开头,至少需要三部分: PLC.模块名.地址
                if (parts[0].Equals("PLC", StringComparison.OrdinalIgnoreCase) && parts.Length < 3)
                {
                    invalidRefs.Add(plcRef);
                }
            }

            return invalidRefs;
        }

        /// <summary>
        /// 检查运算符使用是否有效
        /// </summary>
        private bool HasValidOperatorUsage(string expression)
        {
            // 简单的运算符使用检查
            // 避免连续的二元运算符(如 "+ +", "* /")
            foreach (var op in ExpressionConstants.SupportedOperators.Where(o => o.Length > 0 && o != "!"))
            {
                // 检查重复运算符
                if (expression.Contains(op + " " + op) || expression.Contains(op + op))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 创建错误结果 - 统一的工厂方法
        /// </summary>
        private ValidationResult CreateError(string message)
        {
            return new ValidationResult
            {
                IsValid = false,
                Message = message,
                Errors = [message]
            };
        }

        #endregion
    }
}
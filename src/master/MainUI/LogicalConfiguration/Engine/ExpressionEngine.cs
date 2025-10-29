using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 统一的表达式引擎
    /// 整合了表达式验证、解析、求值和变量赋值功能
    /// </summary>
    public class ExpressionEngine
    {
        private readonly GlobalVariableManager _variableManager;
        private readonly IPLCManager _plcManager;
        private readonly ILogger<ExpressionEngine> _logger;

        #region 正则表达式模式

        // 变量引用模式: {变量名}
        private readonly Regex _variablePattern = new(@"\{(\w+)\}", RegexOptions.Compiled);

        // 函数调用模式: 函数名(参数)
        private readonly Regex _functionPattern = new(@"\b(\w+)\s*\(([^)]*)\)", RegexOptions.Compiled);

        // 数字模式
        private readonly Regex _numberPattern = new(@"\b\d+(\.\d+)?\b", RegexOptions.Compiled);

        // 字符串字面量模式
        private readonly Regex _stringLiteralPattern = new(@"""([^""\\]*(\\.[^""\\]*)*)""", RegexOptions.Compiled);

        // PLC读取模式: PLC.模块名.地址
        private readonly Regex _plcReadPattern = new(@"PLC\.(\w+)\.(\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region 运算符和函数定义

        // 运算符优先级
        private readonly Dictionary<string, int> _operatorPrecedence = new()
        {
            { "||", 1 },
            { "&&", 2 },
            { "==", 3 }, { "!=", 3 },
            { "<", 4 }, { "<=", 4 }, { ">", 4 }, { ">=", 4 },
            { "+", 5 }, { "-", 5 },
            { "*", 6 }, { "/", 6 }, { "%", 6 },
            { "!", 7 }  // 一元运算符
        };

        // 支持的运算符
        private readonly string[] _supportedOperators =
        {
            "+", "-", "*", "/", "%", "==", "!=", ">", "<", ">=", "<=", "&&", "||", "!"
        };

        // 支持的函数
        private readonly Dictionary<string, Func<List<object>, object>> _supportedFunctions;

        #endregion

        #region 构造函数

        public ExpressionEngine(
            GlobalVariableManager variableManager,
            IPLCManager plcManager = null,
            ILogger<ExpressionEngine> logger = null)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _plcManager = plcManager;
            _logger = logger;

            // 初始化支持的函数
            _supportedFunctions = InitializeFunctions();
        }

        #endregion

        #region 公开方法 - 验证

        /// <summary>
        /// 验证表达式的合法性
        /// </summary>
        /// <param name="expression">要验证的表达式</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateExpression(string expression)
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

                _logger?.LogDebug("开始验证表达式: {Expression}", expression);

                // 1. 检查无效字符
                var invalidChars = GetInvalidCharacters(expression);
                if (invalidChars.Count != 0)
                {
                    result.IsValid = false;
                    result.Message = $"表达式包含无效字符: {string.Join(", ", invalidChars)}";
                    result.Errors.Add($"无效字符: {string.Join(", ", invalidChars)}");
                    return result;
                }

                // 2. 检查括号匹配
                if (!CheckParenthesesBalance(expression))
                {
                    result.IsValid = false;
                    result.Message = "括号不匹配";
                    result.Errors.Add("括号数量或顺序不正确");
                    return result;
                }

                // 3. 检查变量存在性
                var referencedVars = GetReferencedVariables(expression);
                var missingVars = referencedVars.Where(v => _variableManager.TryFindVariableByName(v) != null).ToList();
                if (missingVars.Count == 0)
                {
                    result.IsValid = false;
                    result.Message = $"以下变量不存在: {string.Join(", ", missingVars)}";
                    result.Errors.AddRange(missingVars.Select(v => $"变量 '{v}' 未定义"));
                    return result;
                }

                // 4. 检查函数调用
                var functionMatches = _functionPattern.Matches(expression);
                foreach (Match match in functionMatches)
                {
                    var funcName = match.Groups[1].Value.ToUpper();
                    if (!IsFunctionSupported(funcName))
                    {
                        result.IsValid = false;
                        result.Message = $"不支持的函数: {funcName}";
                        result.Errors.Add($"函数 '{funcName}' 未定义");
                        return result;
                    }
                }

                // 5. 检查运算符使用
                var withoutStrings = RemoveStringLiterals(expression);
                if (!HasValidOperatorUsage(withoutStrings))
                {
                    result.IsValid = false;
                    result.Message = "运算符使用不当";
                    result.Errors.Add("请检查运算符的位置和用法");
                    return result;
                }

                result.Message = "表达式验证通过";
                _logger?.LogDebug("表达式验证成功: {Expression}", expression);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证表达式时发生错误: {Expression}", expression);
                result.IsValid = false;
                result.Message = $"验证失败: {ex.Message}";
                result.Errors.Add(ex.Message);
            }

            return result;
        }


        /// <summary>
        /// 验证表达式的合法性（带验证上下文）
        /// </summary>
        /// <param name="expression">要验证的表达式</param>
        /// <param name="context">验证上下文，提供目标变量信息等</param>
        /// <returns>验证结果，包含错误和警告</returns>
        public ValidationResult ValidateExpression(string expression, ValidationContext context)
        {
            // 如果没有提供上下文，调用无参数版本
            if (context == null)
            {
                return ValidateExpression(expression);
            }

            var result = new ValidationResult { IsValid = true };

            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    result.IsValid = false;
                    result.Message = "表达式不能为空";
                    return result;
                }

                var label = string.IsNullOrWhiteSpace(context.ValidationLabel)
                    ? expression
                    : $"{context.ValidationLabel}: {expression}";

                _logger?.LogDebug("开始验证表达式: {Expression}", label);

                // 1. 检查无效字符
                var invalidChars = GetInvalidCharacters(expression);
                if (invalidChars.Any())
                {
                    result.IsValid = false;
                    result.Message = $"表达式包含无效字符: {string.Join(", ", invalidChars)}";
                    result.Errors.Add($"无效字符: {string.Join(", ", invalidChars)}");
                    return result;
                }

                // 2. 检查括号匹配
                if (!CheckParenthesesBalance(expression))
                {
                    result.IsValid = false;
                    result.Message = "括号不匹配";
                    result.Errors.Add("括号数量或顺序不正确");
                    return result;
                }

                // 3. 检查变量存在性
                var referencedVars = GetReferencedVariables(expression);
                var missingVars = referencedVars.Where(v =>
                {
                    var variable = _variableManager.TryFindVariableByName(v);
                    return variable == null;
                }).ToList();

                if (missingVars.Any())
                {
                    result.IsValid = false;
                    result.Message = $"以下变量不存在: {string.Join(", ", missingVars)}";
                    result.Errors.AddRange(missingVars.Select(v => $"变量 '{v}' 未定义"));
                    return result;
                }

                // 4. 检查函数调用（如果启用）
                if (context.AllowFunctionCalls)
                {
                    var functionMatches = _functionPattern.Matches(expression);
                    foreach (Match match in functionMatches)
                    {
                        var funcName = match.Groups[1].Value.ToUpper();
                        if (!IsFunctionSupported(funcName))
                        {
                            result.IsValid = false;
                            result.Message = $"不支持的函数: {funcName}";
                            result.Errors.Add($"函数 '{funcName}' 未定义");
                            return result;
                        }
                    }
                }
                else
                {
                    // 不允许函数调用时，检查是否包含函数
                    var functionMatches = _functionPattern.Matches(expression);
                    if (functionMatches.Count > 0)
                    {
                        var funcNames = functionMatches.Cast<Match>()
                            .Select(m => m.Groups[1].Value)
                            .Distinct();
                        result.IsValid = false;
                        result.Message = "此场景不允许使用函数调用";
                        result.Errors.Add($"检测到函数调用: {string.Join(", ", funcNames)}");
                        return result;
                    }
                }

                // 5. 检查PLC引用（如果启用）
                if (!context.AllowPlcReferences)
                {
                    var plcMatches = _plcReadPattern.Matches(expression);
                    if (plcMatches.Count > 0)
                    {
                        result.IsValid = false;
                        result.Message = "此场景不允许使用PLC引用";
                        result.Errors.Add("检测到PLC引用");
                        return result;
                    }
                }

                // 6. 检查运算符使用
                var withoutStrings = RemoveStringLiterals(expression);
                if (!HasValidOperatorUsage(withoutStrings))
                {
                    result.IsValid = false;
                    result.Message = "运算符使用不当";
                    result.Errors.Add("请检查运算符的位置和用法");
                    return result;
                }

                // 7. 类型兼容性检查（如果提供了目标变量类型）
                if (!string.IsNullOrWhiteSpace(context.TargetVariableType))
                {
                    CheckTypeCompatibility(expression, context.TargetVariableType, result, context.StrictMode);
                }

                // 8. 设置最终消息
                if (result.IsValid)
                {
                    result.Message = result.HasWarnings
                        ? $"表达式验证通过（有 {result.Warnings.Count} 个警告）"
                        : "表达式验证通过";
                }

                _logger?.LogDebug("表达式验证完成: {Expression}, 有效: {IsValid}, 警告数: {WarningCount}",
                    label, result.IsValid, result.Warnings.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "验证表达式时发生错误: {Expression}", expression);
                result.IsValid = false;
                result.Message = $"验证失败: {ex.Message}";
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 计算表达式的预期值(用于预览)
        /// 不修改任何变量,仅返回计算结果
        /// </summary>
        /// <param name="expression">要计算的表达式</param>
        /// <returns>计算结果</returns>
        public EvaluationResult CalculateExpectedValue(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    return EvaluationResult.Error("表达式为空");
                }

                _logger?.LogDebug("计算预期值: {Expression}", expression);

                // 1. 先验证表达式
                var validation = ValidateExpression(expression);
                if (!validation.IsValid)
                {
                    return EvaluationResult.Error(validation.Message);
                }

                // 2. 预处理表达式(替换变量引用)
                var processedExpression = PreprocessExpression(expression);

                // 3. 执行计算(不修改任何变量)
                var result = EvaluateProcessedExpression(processedExpression);

                _logger?.LogDebug("预期值计算成功: {Expression} = {Result}", expression, result);

                return EvaluationResult.Succes(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "计算预期值时发生错误: {Expression}", expression);
                return EvaluationResult.Error($"计算失败: {ex.Message}");
            }
        }

        #endregion

        #region 类型兼容性检查私有方法

        /// <summary>
        /// 检查表达式结果与目标类型的兼容性
        /// </summary>
        private void CheckTypeCompatibility(string expression, string targetType, ValidationResult result, bool strictMode)
        {
            try
            {
                // 尝试推断表达式的结果类型
                var inferredType = InferExpressionType(expression);

                if (string.IsNullOrWhiteSpace(inferredType))
                {
                    // 无法推断类型，添加警告
                    result.AddWarning("无法确定表达式的结果类型，可能需要运行时类型转换");
                    return;
                }

                // 检查类型是否兼容
                if (!AreTypesCompatible(inferredType, targetType))
                {
                    var message = $"表达式结果类型 '{inferredType}' 与目标类型 '{targetType}' 可能不兼容";

                    if (strictMode)
                    {
                        // 严格模式：类型不匹配是错误
                        result.AddError(message);
                    }
                    else
                    {
                        // 非严格模式：类型不匹配是警告
                        result.AddWarning(message + "，将尝试自动类型转换");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "类型兼容性检查失败: {Expression}", expression);
                result.AddWarning("无法执行类型兼容性检查");
            }
        }

        /// <summary>
        /// 推断表达式的结果类型
        /// </summary>
        private string InferExpressionType(string expression)
        {
            // 简单的类型推断逻辑
            // 更复杂的类型推断需要完整的表达式解析

            // 数字字面量
            if (_numberPattern.IsMatch(expression.Trim()))
            {
                return expression.Contains(".") ? "double" : "int";
            }

            // 字符串字面量
            if (_stringLiteralPattern.IsMatch(expression.Trim()))
            {
                return "string";
            }

            // 布尔字面量
            var lower = expression.Trim().ToLower();
            if (lower == "true" || lower == "false")
            {
                return "bool";
            }

            // 单个变量引用
            var varMatch = Regex.Match(expression.Trim(), @"^\{(\w+)\}$");
            if (varMatch.Success)
            {
                var varName = varMatch.Groups[1].Value;
                var variable = _variableManager.TryFindVariableByName(varName);
                if (variable != null)
                {
                    return variable.VarType;
                }
            }

            // 包含算术运算符，推断为数值类型
            if (expression.Contains("+") || expression.Contains("-") ||
                expression.Contains("*") || expression.Contains("/"))
            {
                return "double"; // 默认为 double 以支持小数
            }

            // 包含比较运算符或逻辑运算符，推断为布尔类型
            if (expression.Contains("==") || expression.Contains("!=") ||
                expression.Contains(">") || expression.Contains("<") ||
                expression.Contains("&&") || expression.Contains("||"))
            {
                return "bool";
            }

            // 无法推断
            return null;
        }

        /// <summary>
        /// 检查两种类型是否兼容
        /// </summary>
        private bool AreTypesCompatible(string sourceType, string targetType)
        {
            if (string.IsNullOrWhiteSpace(sourceType) || string.IsNullOrWhiteSpace(targetType))
            {
                return true; // 类型未知时认为兼容
            }

            var source = sourceType.ToLower();
            var target = targetType.ToLower();

            // 完全相同
            if (source == target)
            {
                return true;
            }

            // 数值类型之间可以转换
            var numericTypes = new[] { "int", "int32", "long", "int64", "float", "single", "double", "decimal" };
            if (numericTypes.Contains(source) && numericTypes.Contains(target))
            {
                return true;
            }

            // string 可以接受任何类型（通过 ToString()）
            if (target == "string")
            {
                return true;
            }

            // 其他情况认为不兼容
            return false;
        }

        #endregion

        #region 公开方法 - 求值

        /// <summary>
        /// 异步求值表达式
        /// </summary>
        /// <param name="expression">要求值的表达式</param>
        /// <returns>求值结果</returns>
        public async Task<EvaluationResult> EvaluateExpressionAsync(string expression)
        {
            return await Task.Run(() => EvaluateExpression(expression));
        }

        /// <summary>
        /// 同步求值表达式
        /// </summary>
        /// <param name="expression">要求值的表达式</param>
        /// <returns>求值结果</returns>
        public EvaluationResult EvaluateExpression(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    return EvaluationResult.Error("表达式为空");
                }

                _logger?.LogDebug("开始求值表达式: {Expression}", expression);

                // 1. 先验证表达式
                var validation = ValidateExpression(expression);
                if (!validation.IsValid)
                {
                    return EvaluationResult.Error(validation.Message);
                }

                // 2. 预处理表达式(替换变量引用)
                var processedExpression = PreprocessExpression(expression);

                // 3. 执行计算
                var result = EvaluateProcessedExpression(processedExpression);

                _logger?.LogDebug("表达式求值成功: {Expression} = {Result}", expression, result);
                return EvaluationResult.Succes(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "求值表达式时发生错误: {Expression}", expression);
                return EvaluationResult.Error($"求值失败: {ex.Message}");
            }
        }

        #endregion

        #region 公开方法 - 变量赋值

        /// <summary>
        /// 直接赋值（固定值）
        /// </summary>
        /// <param name="targetVarName">目标变量名</param>
        /// <param name="value">要赋的值</param>
        /// <returns>赋值结果</returns>
        public AssignmentResult AssignDirectValue(string targetVarName, object value)
        {
            try
            {
                _logger?.LogInformation("直接赋值: {VarName} = {Value}", targetVarName, value);

                // 验证目标变量存在
                if (_variableManager.TryFindVariableByName(targetVarName) == null)
                {
                    return AssignmentResult.Error($"目标变量 '{targetVarName}' 不存在");
                }

                // 获取变量信息
                var variable = _variableManager.TryFindVariableByName(targetVarName);

                // 类型转换
                var convertedValue = ConvertValueToType(value, variable.VarType);
                if (convertedValue == null)
                {
                    return AssignmentResult.Error($"无法将值 '{value}' 转换为类型 '{variable.VarType}'");
                }

                // 执行赋值
                var oldValue = variable.VarValue;
                variable.UpdateValue(convertedValue, "直接赋值");

                _logger?.LogInformation("赋值成功: {VarName} = {NewValue} (旧值: {OldValue})",
                    targetVarName, convertedValue, oldValue);

                return AssignmentResult.Succes(convertedValue, oldValue);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "直接赋值失败: {VarName}", targetVarName);
                return AssignmentResult.Error($"赋值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 表达式赋值
        /// </summary>
        /// <param name="targetVarName">目标变量名</param>
        /// <param name="expression">表达式</param>
        /// <returns>赋值结果</returns>
        public async Task<AssignmentResult> AssignExpressionAsync(string targetVarName, string expression)
        {
            try
            {
                _logger?.LogInformation("表达式赋值: {VarName} = {Expression}", targetVarName, expression);

                // 验证目标变量存在
                if (_variableManager.TryFindVariableByName(targetVarName) == null)
                {
                    return AssignmentResult.Error($"目标变量 '{targetVarName}' 不存在");
                }

                // 求值表达式
                var evalResult = await EvaluateExpressionAsync(expression);
                if (!evalResult.Success)
                {
                    return AssignmentResult.Error($"表达式求值失败: {evalResult.ErrorMessage}");
                }

                // 获取变量信息
                var variable = _variableManager.FindVariableByName(targetVarName);

                // 类型转换
                var convertedValue = ConvertValueToType(evalResult.Result, variable.VarType);
                if (convertedValue == null)
                {
                    return AssignmentResult.Error(
                        $"无法将表达式结果 '{evalResult.Result}' 转换为类型 '{variable.VarType}'");
                }

                // 执行赋值
                var oldValue = variable.VarValue;
                variable.UpdateValue(convertedValue, $"表达式计算: {expression}");

                _logger?.LogInformation("表达式赋值成功: {VarName} = {NewValue} (表达式: {Expression})",
                    targetVarName, convertedValue, expression);

                return AssignmentResult.Succes(convertedValue, oldValue);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "表达式赋值失败: {VarName} = {Expression}", targetVarName, expression);
                return AssignmentResult.Error($"赋值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 从变量复制赋值
        /// </summary>
        /// <param name="targetVarName">目标变量名</param>
        /// <param name="sourceVarName">源变量名</param>
        /// <returns>赋值结果</returns>
        public AssignmentResult AssignFromVariable(string targetVarName, string sourceVarName)
        {
            try
            {
                _logger?.LogInformation("变量复制: {TargetVar} = {SourceVar}", targetVarName, sourceVarName);

                // 验证变量存在
                if (_variableManager.FindVariableByName(targetVarName) == null)
                {
                    return AssignmentResult.Error($"目标变量 '{targetVarName}' 不存在");
                }
                if (_variableManager.TryFindVariableByName(sourceVarName) == null)
                {
                    return AssignmentResult.Error($"源变量 '{sourceVarName}' 不存在");
                }

                // 获取源变量值
                var sourceVar = _variableManager.TryFindVariableByName(sourceVarName);
                var sourceValue = sourceVar.VarValue;

                // 获取目标变量信息
                var targetVar = _variableManager.TryFindVariableByName(targetVarName);

                // 类型转换
                var convertedValue = ConvertValueToType(sourceValue, targetVar.VarType);
                if (convertedValue == null)
                {
                    return AssignmentResult.Error(
                        $"无法将源变量值 '{sourceValue}' (类型: {sourceVar.VarType}) 转换为目标类型 '{targetVar.VarType}'");
                }

                // 执行赋值
                var oldValue = targetVar.VarValue;
                targetVar.UpdateValue(convertedValue, $"从变量复制: {sourceVarName}");

                _logger?.LogInformation("变量复制成功: {TargetVar} = {NewValue} (来自: {SourceVar})",
                    targetVarName, convertedValue, sourceVarName);

                return AssignmentResult.Succes(convertedValue, oldValue);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "变量复制失败: {TargetVar} = {SourceVar}", targetVarName, sourceVarName);
                return AssignmentResult.Error($"赋值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 从PLC读取赋值
        /// </summary>
        /// <param name="targetVarName">目标变量名</param>
        /// <param name="plcModuleName">PLC模块名</param>
        /// <param name="plcAddress">PLC地址</param>
        /// <returns>赋值结果</returns>
        public async Task<AssignmentResult> AssignFromPlcAsync(
            string targetVarName,
            string plcModuleName,
            string plcAddress)
        {
            try
            {
                _logger?.LogInformation("PLC赋值: {VarName} = PLC.{Module}.{Address}",
                    targetVarName, plcModuleName, plcAddress);

                // 验证PLC管理器可用
                if (_plcManager == null)
                {
                    return AssignmentResult.Error("PLC管理器未初始化");
                }

                // 验证目标变量存在
                if (_variableManager.TryFindVariableByName(targetVarName) == null)
                {
                    return AssignmentResult.Error($"目标变量 '{targetVarName}' 不存在");
                }

                // 从PLC读取值
                var plcValue = await _plcManager.ReadPLCValueAsync(plcModuleName, plcAddress);
                if (plcValue == null)
                {
                    return AssignmentResult.Error($"无法从PLC读取值: {plcModuleName}.{plcAddress}");
                }

                // 获取目标变量信息
                var variable = _variableManager.TryFindVariableByName(targetVarName);

                // 类型转换
                var convertedValue = ConvertValueToType(plcValue, variable.VarType);
                if (convertedValue == null)
                {
                    return AssignmentResult.Error(
                        $"无法将PLC值 '{plcValue}' 转换为类型 '{variable.VarType}'");
                }

                // 执行赋值
                var oldValue = variable.VarValue;
                variable.UpdateValue(convertedValue, $"PLC读取: {plcModuleName}.{plcAddress}");

                _logger?.LogInformation("PLC赋值成功: {VarName} = {NewValue} (来自: PLC.{Module}.{Address})",
                    targetVarName, convertedValue, plcModuleName, plcAddress);

                return AssignmentResult.Succes(convertedValue, oldValue);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "PLC赋值失败: {VarName} = PLC.{Module}.{Address}",
                    targetVarName, plcModuleName, plcAddress);
                return AssignmentResult.Error($"赋值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 智能赋值 - 自动识别赋值类型
        /// </summary>
        /// <param name="targetVarName">目标变量名</param>
        /// <param name="valueExpression">值或表达式</param>
        /// <returns>赋值结果</returns>
        public async Task<AssignmentResult> AssignSmartAsync(string targetVarName, string valueExpression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(valueExpression))
                {
                    return AssignmentResult.Error("赋值表达式不能为空");
                }

                // 1. 检查是否是PLC读取: PLC.模块名.地址
                var plcMatch = _plcReadPattern.Match(valueExpression);
                if (plcMatch.Success)
                {
                    var moduleName = plcMatch.Groups[1].Value;
                    var address = plcMatch.Groups[2].Value;
                    return await AssignFromPlcAsync(targetVarName, moduleName, address);
                }

                // 2. 检查是否是单个变量引用: {变量名}
                var singleVarMatch = Regex.Match(valueExpression.Trim(), @"^\{(\w+)\}$");
                if (singleVarMatch.Success)
                {
                    var sourceVarName = singleVarMatch.Groups[1].Value;
                    return AssignFromVariable(targetVarName, sourceVarName);
                }

                // 3. 检查是否包含变量引用或运算符 - 表达式赋值
                if (_variablePattern.IsMatch(valueExpression) ||
                    _supportedOperators.Any(op => valueExpression.Contains(op)))
                {
                    return await AssignExpressionAsync(targetVarName, valueExpression);
                }

                // 4. 否则作为直接赋值处理
                return AssignDirectValue(targetVarName, valueExpression);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "智能赋值失败: {VarName} = {Expression}", targetVarName, valueExpression);
                return AssignmentResult.Error($"赋值失败: {ex.Message}");
            }
        }

        #endregion

        #region 私有方法 - 验证辅助

        /// <summary>
        /// 检查是否包含无效字符
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
        /// 检查括号是否匹配
        /// </summary>
        private bool CheckParenthesesBalance(string expression)
        {
            var stack = new Stack<char>();
            var pairs = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' } };

            foreach (var c in expression)
            {
                if (c is '(' or '[' or '{')
                {
                    stack.Push(c);
                }
                else if (c is ')' or ']' or '}')
                {
                    if (stack.Count == 0 || stack.Pop() != pairs[c])
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
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
        /// 检查函数是否支持
        /// </summary>
        private bool IsFunctionSupported(string functionName)
        {
            var normalizedName = functionName.ToUpper();
            return _supportedFunctions.ContainsKey(normalizedName) ||
                   _supportedFunctions.ContainsKey(normalizedName.Replace("MATH.", "")) ||
                   _supportedFunctions.ContainsKey(normalizedName.Replace("STRING.", "")) ||
                   _supportedFunctions.ContainsKey(normalizedName.Replace("DATETIME.", ""));
        }

        /// <summary>
        /// 检查运算符使用是否合理
        /// </summary>
        private bool HasValidOperatorUsage(string expression)
        {
            // 移除字符串后检查运算符
            var withoutStrings = RemoveStringLiterals(expression);

            // 简单检查：确保有运算符或者是纯数字/变量
            var hasOperator = _supportedOperators.Any(op => withoutStrings.Contains(op));
            var hasVariableOrNumber = _variablePattern.IsMatch(withoutStrings) ||
                                     _numberPattern.IsMatch(withoutStrings);

            return hasOperator || hasVariableOrNumber;
        }

        /// <summary>
        /// 移除字符串字面量
        /// </summary>
        private string RemoveStringLiterals(string expression)
        {
            return _stringLiteralPattern.Replace(expression, "\"\"");
        }

        #endregion

        #region 私有方法 - 表达式预处理

        /// <summary>
        /// 预处理表达式(替换变量引用为实际值)
        /// </summary>
        private string PreprocessExpression(string expression)
        {
            return _variablePattern.Replace(expression, match =>
            {
                var varName = match.Groups[1].Value;
                var variable = _variableManager.TryFindVariableByName(varName);

                if (variable == null)
                {
                    _logger?.LogWarning("预处理时发现未定义变量: {VarName}", varName);
                    return match.Value;
                }

                var value = variable.VarValue;

                if (value == null)
                {
                    return "null";
                }

                // 调用统一的格式化方法
                return FormatValueForExpression(value);
            });
        }

        #endregion

        #region 私有方法 - 表达式求值

        /// <summary>
        /// 求值已预处理的表达式
        /// </summary>
        private object EvaluateProcessedExpression(string expression)
        {
            try
            {
                // 1. 处理函数调用
                while (_functionPattern.IsMatch(expression))
                {
                    expression = _functionPattern.Replace(expression, match =>
                    {
                        var funcName = match.Groups[1].Value.ToUpper();
                        var argsString = match.Groups[2].Value;

                        // 解析参数
                        var args = ParseFunctionArguments(argsString);

                        // 调用函数
                        if (_supportedFunctions.TryGetValue(funcName, out var func))
                        {
                            var result = func(args);
                            return FormatValueForExpression(result);
                        }

                        return match.Value;
                    }, 1); // 每次只替换一个，从内层开始
                }

                // 2. 计算最终表达式
                return CalculateExpression(expression);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"表达式求值失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 解析函数参数
        /// </summary>
        private List<object> ParseFunctionArguments(string argsString)
        {
            if (string.IsNullOrWhiteSpace(argsString))
            {
                return new List<object>();
            }

            var args = new List<object>();
            var parts = SplitArguments(argsString);

            foreach (var part in parts)
            {
                var trimmed = part.Trim();

                // 尝试解析为不同类型
                if (trimmed.StartsWith("\"") && trimmed.EndsWith("\""))
                {
                    // 字符串
                    args.Add(trimmed.Substring(1, trimmed.Length - 2));
                }
                else if (bool.TryParse(trimmed, out var boolValue))
                {
                    // 布尔值
                    args.Add(boolValue);
                }
                else if (double.TryParse(trimmed, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
                {
                    // 数字
                    args.Add(doubleValue);
                }
                else
                {
                    // 可能是嵌套表达式，递归求值
                    var result = EvaluateProcessedExpression(trimmed);
                    args.Add(result);
                }
            }

            return args;
        }

        /// <summary>
        /// 分割函数参数（考虑嵌套）
        /// </summary>
        private List<string> SplitArguments(string argsString)
        {
            var args = new List<string>();
            var current = new StringBuilder();
            var depth = 0;
            var inString = false;

            foreach (var c in argsString)
            {
                if (c == '"' && (current.Length == 0 || current[^1] != '\\'))
                {
                    inString = !inString;
                }

                if (!inString)
                {
                    if (c is '(' or '[' or '{') depth++;
                    if (c is ')' or ']' or '}') depth--;
                }

                if (c == ',' && depth == 0 && !inString)
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

        /// <summary>
        /// 计算表达式（处理运算符）
        /// </summary>
        private object CalculateExpression(string expression)
        {
            expression = expression.Trim();

            // 1. 尝试解析为字面量
            // 检查是否是完整的字符串字面量（必须准确判断）
            if (IsStringLiteral(expression))
            {
                // 去掉首尾引号，返回字符串内容
                return expression.Substring(1, expression.Length - 2);
            }

            if (bool.TryParse(expression, out var boolValue))
            {
                return boolValue;
            }

            if (double.TryParse(expression, NumberStyles.Any, CultureInfo.InvariantCulture, out var numValue))
            {
                return numValue;
            }

            // 2. 检查是否包含字符串字面量和运算符
            if (expression.Contains("\""))
            {
                // 处理字符串拼接（+ 运算符）
                if (expression.Contains("+"))
                {
                    return HandleStringConcatenation(expression);
                }

                // 其他包含字符串的运算暂不支持
                throw new InvalidOperationException($"不支持该字符串运算: {expression}");
            }

            // 3. 纯数值表达式 - 使用 DataTable.Compute
            try
            {
                var table = new System.Data.DataTable();
                var result = table.Compute(expression, "");
                return result;
            }
            catch
            {
                // 如果 DataTable.Compute 失败，尝试逻辑表达式
                return EvaluateLogicalExpression(expression);
            }
        }

        /// <summary>
        /// 检查是否是完整的字符串字面量（单个字符串，不包含运算符）
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>如果是单个字符串字面量返回 true，否则返回 false</returns>
        private bool IsStringLiteral(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;

            // 必须以引号开头和结尾
            if (!expression.StartsWith("\"") || !expression.EndsWith("\""))
                return false;

            // 至少要有两个引号 (空字符串 "")
            if (expression.Length < 2)
                return false;

            // 检查是否是单个完整的字符串字面量
            // 策略：从第二个字符开始，找到第一个未转义的引号
            // 如果这个引号就是最后一个字符，说明是单个字符串字面量

            int i = 1; // 从第二个字符开始（跳过开头的引号）
            while (i < expression.Length)
            {
                char c = expression[i];

                if (c == '\\' && i + 1 < expression.Length)
                {
                    // 转义字符，跳过下一个字符
                    i += 2;
                    continue;
                }

                if (c == '"')
                {
                    // 找到未转义的引号
                    // 检查这是否是最后一个字符
                    if (i == expression.Length - 1)
                    {
                        // 是最后一个字符，说明是单个字符串字面量
                        return true;
                    }
                    else
                    {
                        // 不是最后一个字符，说明后面还有内容（如 + 运算符）
                        return false;
                    }
                }

                i++;
            }

            // 没有找到结束引号（不应该发生）
            return false;
        }

        /// <summary>
        /// 处理字符串拼接
        /// 示例: "11" + "12" → "1112"
        /// 示例: "Hello" + " " + "World" → "Hello World"
        /// 示例: "Value: " + 123 → "Value: 123"
        /// </summary>
        private object HandleStringConcatenation(string expression)
        {
            try
            {
                var parts = new List<string>();
                var currentPart = new StringBuilder();
                bool inString = false;

                for (int i = 0; i < expression.Length; i++)
                {
                    char c = expression[i];

                    // 处理转义字符
                    if (c == '\\' && i + 1 < expression.Length && inString)
                    {
                        currentPart.Append(c);
                        currentPart.Append(expression[i + 1]);
                        i++; // 跳过下一个字符
                        continue;
                    }

                    // 处理引号
                    if (c == '"')
                    {
                        inString = !inString;
                        currentPart.Append(c);
                        continue;
                    }

                    // 在字符串外检测 + 号
                    if (!inString && c == '+')
                    {
                        // 保存当前部分
                        string part = currentPart.ToString().Trim();
                        if (!string.IsNullOrEmpty(part))
                        {
                            parts.Add(part);
                        }
                        currentPart.Clear();
                        continue;
                    }

                    // 其他字符直接添加
                    currentPart.Append(c);
                }

                // 添加最后一部分
                string lastPart = currentPart.ToString().Trim();
                if (!string.IsNullOrEmpty(lastPart))
                {
                    parts.Add(lastPart);
                }

                // 如果没有有效的部分，返回空字符串
                if (parts.Count == 0)
                {
                    return string.Empty;
                }

                // 拼接所有部分
                var result = new StringBuilder();
                foreach (var part in parts)
                {
                    var value = EvaluatePart(part);
                    result.Append(value);
                }

                return result.ToString();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "字符串拼接处理失败: {Expression}", expression);
                throw new InvalidOperationException($"字符串拼接失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 计算表达式的一部分
        /// </summary>
        private string EvaluatePart(string part)
        {
            part = part.Trim();

            // 如果是字符串字面量，去掉引号
            if (IsStringLiteral(part))
            {
                return part.Substring(1, part.Length - 2);
            }

            // 如果是数字
            if (double.TryParse(part, NumberStyles.Any, CultureInfo.InvariantCulture, out var numValue))
            {
                return numValue.ToString(CultureInfo.InvariantCulture);
            }

            // 如果是布尔值
            if (bool.TryParse(part, out var boolValue))
            {
                return boolValue.ToString();
            }

            // 否则递归计算（可能是子表达式）
            try
            {
                var result = CalculateExpression(part);
                return result?.ToString() ?? string.Empty;
            }
            catch
            {
                // 如果无法计算，返回原始值
                return part;
            }
        }

        /// <summary>
        /// 求值逻辑表达式
        /// </summary>
        private object EvaluateLogicalExpression(string expression)
        {
            // 处理逻辑运算符: &&, ||, ==, !=, <, >, <=, >=

            // 优先级最低的是 ||
            if (expression.Contains("||"))
            {
                var parts = expression.Split(new[] { "||" }, 2, StringSplitOptions.None);
                var left = CalculateExpression(parts[0]);
                var right = CalculateExpression(parts[1]);
                return ConvertToBool(left) || ConvertToBool(right);
            }

            // 然后是 &&
            if (expression.Contains("&&"))
            {
                var parts = expression.Split(new[] { "&&" }, 2, StringSplitOptions.None);
                var left = CalculateExpression(parts[0]);
                var right = CalculateExpression(parts[1]);
                return ConvertToBool(left) && ConvertToBool(right);
            }

            // 比较运算符
            var comparisonOps = new[] { "==", "!=", "<=", ">=", "<", ">" };
            foreach (var op in comparisonOps)
            {
                if (expression.Contains(op))
                {
                    var parts = expression.Split(new[] { op }, 2, StringSplitOptions.None);
                    var left = CalculateExpression(parts[0]);
                    var right = CalculateExpression(parts[1]);

                    return op switch
                    {
                        "==" => Equals(left, right),
                        "!=" => !Equals(left, right),
                        "<" => CompareValues(left, right) < 0,
                        ">" => CompareValues(left, right) > 0,
                        "<=" => CompareValues(left, right) <= 0,
                        ">=" => CompareValues(left, right) >= 0,
                        _ => false
                    };
                }
            }

            throw new InvalidOperationException($"无法计算表达式: {expression}");
        }

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        private bool ConvertToBool(object value)
        {
            return value switch
            {
                bool b => b,
                string s => !string.IsNullOrEmpty(s) && s.ToLower() != "false" && s != "0",
                int i => i != 0,
                double d => d != 0,
                _ => value != null
            };
        }

        /// <summary>
        /// 比较两个值
        /// </summary>
        private int CompareValues(object left, object right)
        {
            if (left is IComparable leftComp && right is IComparable rightComp)
            {
                // 尝试转换为相同类型
                if (left is double || right is double)
                {
                    var leftNum = Convert.ToDouble(left);
                    var rightNum = Convert.ToDouble(right);
                    return leftNum.CompareTo(rightNum);
                }

                if (left.GetType() == right.GetType())
                {
                    return leftComp.CompareTo(rightComp);
                }
            }

            return string.Compare(left?.ToString(), right?.ToString(), StringComparison.Ordinal);
        }

        /// <summary>
        /// 格式化值用于表达式
        /// 重要：数值类型不加引号，字符串类型才加引号
        /// </summary>
        private string FormatValueForExpression(object value)
        {
            if (value == null)
                return "null";

            return value switch
            {
                // 字符串类型：需要转义并加引号
                string s => $"\"{s.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"",

                // 布尔类型：小写形式
                bool b => b.ToString().ToLower(),

                // 数值类型：不加引号，直接转字符串
                sbyte or byte or short or ushort or int or uint or long or ulong or
                float or double or decimal
                    => Convert.ToString(value, CultureInfo.InvariantCulture),

                // 其他类型：转字符串并加引号
                _ => $"\"{value.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"")}\""
            };
        }

        #endregion

        #region 私有方法 - 类型转换

        /// <summary>
        /// 将值转换为指定类型
        /// </summary>
        private object ConvertValueToType(object value, string targetType)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                return targetType.ToLower() switch
                {
                    "int" => Convert.ToInt32(value),
                    "double" => Convert.ToDouble(value),
                    "bool" => Convert.ToBoolean(value),
                    "string" => Convert.ToString(value),
                    _ => value
                };
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "类型转换失败: {Value} -> {TargetType}", value, targetType);
                return null;
            }
        }

        #endregion

        #region 支持的函数初始化

        /// <summary>
        /// 初始化所有支持的函数
        /// </summary>
        private Dictionary<string, Func<List<object>, object>> InitializeFunctions()
        {
            var functions = new Dictionary<string, Func<List<object>, object>>(StringComparer.OrdinalIgnoreCase);

            // === 字符串函数 ===
            functions["LEN"] = functions["LENGTH"] = args =>
                args[0]?.ToString()?.Length ?? 0;

            functions["SUBSTRING"] = args =>
            {
                var str = args[0]?.ToString() ?? "";
                var start = Convert.ToInt32(args[1]);
                var length = args.Count > 2 ? Convert.ToInt32(args[2]) : str.Length - start;
                return str.Substring(start, Math.Min(length, str.Length - start));
            };

            functions["UPPER"] = functions["TOUPPER"] = args =>
                args[0]?.ToString()?.ToUpper() ?? "";

            functions["LOWER"] = functions["TOLOWER"] = args =>
                args[0]?.ToString()?.ToLower() ?? "";

            functions["TRIM"] = args =>
                args[0]?.ToString()?.Trim() ?? "";

            functions["REPLACE"] = args =>
            {
                var str = args[0]?.ToString() ?? "";
                var oldValue = args[1]?.ToString() ?? "";
                var newValue = args[2]?.ToString() ?? "";
                return str.Replace(oldValue, newValue);
            };

            functions["CONTAINS"] = args =>
            {
                var str = args[0]?.ToString() ?? "";
                var search = args[1]?.ToString() ?? "";
                return str.Contains(search);
            };

            functions["STARTSWITH"] = args =>
            {
                var str = args[0]?.ToString() ?? "";
                var prefix = args[1]?.ToString() ?? "";
                return str.StartsWith(prefix);
            };

            functions["ENDSWITH"] = args =>
            {
                var str = args[0]?.ToString() ?? "";
                var suffix = args[1]?.ToString() ?? "";
                return str.EndsWith(suffix);
            };

            functions["INDEXOF"] = args =>
            {
                var str = args[0]?.ToString() ?? "";
                var search = args[1]?.ToString() ?? "";
                return str.IndexOf(search);
            };

            functions["SPLIT"] = args =>
            {
                var str = args[0]?.ToString() ?? "";
                var separator = args[1]?.ToString() ?? ",";
                return str.Split(new[] { separator }, StringSplitOptions.None);
            };

            functions["JOIN"] = args =>
            {
                var separator = args[0]?.ToString() ?? "";
                var items = args.Skip(1).Select(x => x?.ToString() ?? "");
                return string.Join(separator, items);
            };

            // === 数学函数 ===
            functions["ABS"] = args => Math.Abs(Convert.ToDouble(args[0]));
            functions["MAX"] = args => args.Max(x => Convert.ToDouble(x));
            functions["MIN"] = args => args.Min(x => Convert.ToDouble(x));
            functions["ROUND"] = args =>
            {
                var value = Convert.ToDouble(args[0]);
                var digits = args.Count > 1 ? Convert.ToInt32(args[1]) : 0;
                return Math.Round(value, digits);
            };
            functions["FLOOR"] = args => Math.Floor(Convert.ToDouble(args[0]));
            functions["CEILING"] = args => Math.Ceiling(Convert.ToDouble(args[0]));
            functions["SQRT"] = args => Math.Sqrt(Convert.ToDouble(args[0]));
            functions["POW"] = args => Math.Pow(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]));
            functions["SIN"] = args => Math.Sin(Convert.ToDouble(args[0]));
            functions["COS"] = args => Math.Cos(Convert.ToDouble(args[0]));
            functions["TAN"] = args => Math.Tan(Convert.ToDouble(args[0]));

            // === 日期时间函数 ===
            functions["NOW"] = args => DateTime.Now;
            functions["TODAY"] = args => DateTime.Today;
            functions["FORMAT"] = args =>
            {
                if (args[0] is DateTime dt)
                {
                    var format = args.Count > 1 ? args[1]?.ToString() : "yyyy-MM-dd HH:mm:ss";
                    return dt.ToString(format);
                }
                return args[0]?.ToString() ?? "";
            };

            functions["ADDDAYS"] = args =>
            {
                if (args[0] is DateTime dt)
                {
                    var days = Convert.ToInt32(args[1]);
                    return dt.AddDays(days);
                }
                return args[0];
            };

            functions["ADDHOURS"] = args =>
            {
                if (args[0] is DateTime dt)
                {
                    var hours = Convert.ToInt32(args[1]);
                    return dt.AddHours(hours);
                }
                return args[0];
            };

            functions["ADDMINUTES"] = args =>
            {
                if (args[0] is DateTime dt)
                {
                    var minutes = Convert.ToInt32(args[1]);
                    return dt.AddMinutes(minutes);
                }
                return args[0];
            };

            return functions;
        }

        #endregion
    }


}
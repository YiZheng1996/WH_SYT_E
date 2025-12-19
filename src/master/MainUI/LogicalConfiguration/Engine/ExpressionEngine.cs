using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;

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

        // 内部组件
        private readonly FunctionRegistry _functionRegistry;
        private readonly ExpressionValidator _validator;
        private readonly VariableResolver _variableResolver;
        private readonly ExpressionEvaluator _evaluator;

        #region 构造函数

        public ExpressionEngine(
            GlobalVariableManager variableManager,
            IPLCManager plcManager = null,
            ILogger<ExpressionEngine> logger = null)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _plcManager = plcManager;
            _logger = logger;

            // 初始化内部组件
            _functionRegistry = new FunctionRegistry();
            _validator = new ExpressionValidator(_variableManager, _functionRegistry, _logger);
            _variableResolver = new VariableResolver(_variableManager, _plcManager, _logger);
            _evaluator = new ExpressionEvaluator(_functionRegistry, _logger);
        }

        #endregion

        #region 公开方法 - 验证
        private string PreprocessDateTimeExpression(string expression)
        {
            // DateTime.Now.ToString("format")
            expression = System.Text.RegularExpressions.Regex.Replace(expression,
                @"DateTime\.Now\.ToString\(""[^""]*""\)",
                "\"2024-01-01\"",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // 单独的 DateTime.Now
            expression = System.Text.RegularExpressions.Regex.Replace(expression,
                @"DateTime\.Now\b",
                "\"2024-01-01\"",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return expression;
        }

        /// <summary>
        /// 验证表达式的合法性（带验证上下文）
        /// </summary>
        /// <param name="expression">要验证的表达式</param>
        /// <param name="context">验证上下文，提供目标变量信息等</param>
        /// <returns>验证结果，包含错误和警告</returns>
        public ValidationResult ValidateExpression(string expression, ValidationContext context)
        {
            // 先替换 DateTime.Now 表达式
            expression = PreprocessDateTimeExpression(expression);
            return _validator.Validate(expression, context);
        }

        public ValidationResult ValidateExpression(string expression)
        {
            // 先替换 DateTime.Now 表达式
            expression = PreprocessDateTimeExpression(expression);
            return _validator.Validate(expression);
        }

        #endregion

        #region 公开方法 - 求值

        /// <summary>
        /// 求值表达式（同步版本）
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

                // 1. 验证表达式
                var validation = ValidateExpression(expression);
                if (!validation.IsValid)
                {
                    return EvaluationResult.Error(validation.Message);
                }

                // 2. 预处理表达式（替换变量）
                var processedExpression = _variableResolver.PreprocessExpression(expression);

                // 3. 计算结果
                var result = _evaluator.Evaluate(processedExpression);

                _logger?.LogDebug("表达式求值成功: {Expression} = {Result}", expression, result);

                return EvaluationResult.Succes(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "表达式求值失败: {Expression}", expression);
                return EvaluationResult.Error($"求值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 真正的异步求值表达式（支持PLC异步读取）
        /// 
        /// 注意：建议将此方法重命名为 EvaluateExpressionAsync，
        /// 并将原来的伪异步方法标记为 [Obsolete]，但为了兼容性暂时保留
        /// </summary>
        public async Task<EvaluationResult> EvaluateExpressionAsync(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    return EvaluationResult.Error("表达式为空");
                }

                _logger?.LogDebug("开始异步求值表达式: {Expression}", expression);

                // 1. 验证表达式
                var validation = ValidateExpression(expression);
                if (!validation.IsValid)
                {
                    return EvaluationResult.Error(validation.Message);
                }

                // 2. 异步预处理表达式（支持PLC读取）
                var processedExpression = await _variableResolver.PreprocessExpressionAsync(expression);

                // 3. 计算结果
                var result = _evaluator.Evaluate(processedExpression);

                _logger?.LogDebug("表达式求值成功: {Expression} = {Result}", expression, result);

                return EvaluationResult.Succes(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "表达式求值失败: {Expression}", expression);
                return EvaluationResult.Error($"求值失败: {ex.Message}");
            }
        }

        #endregion

        #region 公开方法 - 变量赋值

        /// <summary>
        /// 将表达式的结果赋值给指定变量
        /// </summary>
        /// <param name="variableName">目标变量名</param>
        /// <param name="expression">表达式</param>
        /// <returns>赋值结果</returns>
        public AssignmentResult AssignVariable(string variableName, string expression)
        {
            try
            {
                _logger?.LogDebug("开始变量赋值: {Variable} = {Expression}", variableName, expression);

                // 1. 查找变量
                var variable = _variableManager.TryFindVariableByName(variableName);
                if (variable == null)
                {
                    return AssignmentResult.Error($"变量 '{variableName}' 不存在");
                }

                // 2. 求值表达式
                var evalResult = EvaluateExpression(expression);
                if (!evalResult.Success)
                {
                    return AssignmentResult.Error($"表达式求值失败: {evalResult.ErrorMessage}");
                }

                // 3. 类型转换和赋值
                try
                {
                    var oldValue = variable.VarValue;
                    object convertedValue = ConvertValueToType(evalResult.Result, variable.VarType);
                    variable.VarValue = convertedValue;

                    _logger?.LogDebug("变量赋值成功: {Variable} = {Value}", variableName, convertedValue);

                    return AssignmentResult.Succes(convertedValue, oldValue);
                }
                catch (Exception ex)
                {
                    return AssignmentResult.Error($"类型转换失败: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "变量赋值失败: {Variable} = {Expression}", variableName, expression);
                return AssignmentResult.Error($"赋值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 直接赋值（不使用表达式）
        /// </summary>
        public AssignmentResult AssignVariable(string variableName, object value)
        {
            try
            {
                var variable = _variableManager.TryFindVariableByName(variableName);
                if (variable == null)
                {
                    return AssignmentResult.Error($"变量 '{variableName}' 不存在");
                }

                var oldValue = variable.VarValue;
                object convertedValue = ConvertValueToType(value, variable.VarType);
                variable.VarValue = convertedValue;

                return AssignmentResult.Succes(convertedValue, oldValue);
            }
            catch (Exception ex)
            {
                return AssignmentResult.Error($"赋值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 直接赋值（固定值）
        /// </summary>
        public AssignmentResult AssignDirectValue(string targetVarName, object value)
        {
            return AssignVariable(targetVarName, value);
        }

        /// <summary>
        /// 表达式赋值
        /// </summary>
        public async Task<AssignmentResult> AssignExpressionAsync(string targetVarName, string expression)
        {
            // 求值表达式
            var evalResult = await EvaluateExpressionAsync(expression);
            if (!evalResult.Success)
                return AssignmentResult.Error($"表达式求值失败: {evalResult.ErrorMessage}");

            return AssignVariable(targetVarName, evalResult.Result);
        }

        /// <summary>
        /// 从变量复制
        /// </summary>
        public AssignmentResult AssignFromVariable(string targetVarName, string sourceVarName)
        {
            var sourceVar = _variableManager.TryFindVariableByName(sourceVarName);
            if (sourceVar == null)
                return AssignmentResult.Error($"源变量 '{sourceVarName}' 不存在");

            return AssignVariable(targetVarName, sourceVar.VarValue);
        }

        /// <summary>
        /// 从PLC读取赋值
        /// </summary>
        public async Task<AssignmentResult> AssignFromPlcAsync(string targetVarName, string moduleName, string address)
        {
            try
            {
                if (_plcManager == null)
                    return AssignmentResult.Error("PLCManager 未初始化");

                var plcValue = await _plcManager.ReadPLCValueAsync(moduleName, address);
                if (plcValue == null)
                    return AssignmentResult.Error($"无法读取PLC: {moduleName}.{address}");

                return AssignVariable(targetVarName, plcValue);
            }
            catch (Exception ex)
            {
                return AssignmentResult.Error($"PLC读取失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 智能赋值（自动识别类型）
        /// </summary>
        public async Task<AssignmentResult> AssignSmartAsync(string targetVarName, string expression)
        {
            // 如果是单变量引用 {变量名}，转为变量复制
            if (System.Text.RegularExpressions.Regex.IsMatch(expression, @"^\{[^}]+\}$"))
            {
                var varName = expression.Trim('{', '}');
                return AssignFromVariable(targetVarName, varName);
            }

            // 否则作为表达式求值
            return await AssignExpressionAsync(targetVarName, expression);
        }

        #endregion

        #region 公开方法 - 辅助

        /// <summary>
        /// 获取表达式中引用的所有变量名
        /// </summary>
        public List<string> GetReferencedVariables(string expression)
        {
            return _variableResolver.GetReferencedVariables(expression);
        }

        /// <summary>
        /// 检查函数是否受支持
        /// </summary>
        public bool IsFunctionSupported(string functionName)
        {
            return _functionRegistry.IsSupported(functionName);
        }

        /// <summary>
        /// 获取所有支持的函数名称
        /// </summary>
        public IEnumerable<string> GetSupportedFunctions()
        {
            return _functionRegistry.GetAllFunctionNames();
        }

        #endregion

        #region 私有辅助方法

        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        private object ConvertValueToType(object value, string targetTypeName)
        {
            if (value == null || string.IsNullOrEmpty(targetTypeName))
                return value;

            // 将字符串类型名转换为 Type
            Type targetType = targetTypeName switch
            {
                "System.String" or "String" => typeof(string),
                "System.Int32" or "Int32" => typeof(int),
                "System.Double" or "Double" => typeof(double),
                "System.Boolean" or "Boolean" => typeof(bool),
                "System.DateTime" or "DateTime" => typeof(DateTime),
                _ => Type.GetType(targetTypeName)
            };

            if (targetType == null)
                return value;

            // 原有的转换逻辑
            try
            {
                if (value.GetType() == targetType)
                    return value;

                if (targetType == typeof(bool))
                {
                    if (value is string str)
                    {
                        if (str.Equals("true", StringComparison.OrdinalIgnoreCase))
                            return true;
                        if (str.Equals("false", StringComparison.OrdinalIgnoreCase))
                            return false;
                    }
                    return Convert.ToBoolean(value);
                }

                if (targetType == typeof(DateTime))
                {
                    if (value is string dateStr)
                        return DateTime.Parse(dateStr);
                    return Convert.ToDateTime(value);
                }

                return Convert.ChangeType(value, targetType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"无法将值 '{value}' 转换为目标类型 {targetTypeName}", ex);
            }
        }

        #endregion
    }
}
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 统一的表达式引擎
    /// 整合了表达式验证、解析、求值和变量赋值功能
    /// 通过内部组件协作消除冗余,保持API完全兼容
    /// </summary>
    public class ExpressionEngine
    {
        private readonly GlobalVariableManager _variableManager;
        private readonly IPLCManager _plcManager;
        private readonly ILogger<ExpressionEngine> _logger;

        // 内部组件 - 职责分离
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

        /// <summary>
        /// 验证表达式的合法性(带验证上下文)
        /// </summary>
        public ValidationResult ValidateExpression(string expression, ValidationContext context)
        {
            // 预处理DateTime.Now - 使用共享工具
            expression = ExpressionUtils.PreprocessDateTimeExpression(expression);

            // 委托给验证器
            return _validator.Validate(expression, context);
        }

        /// <summary>
        /// 验证表达式的合法性(简化版本)
        /// </summary>
        public ValidationResult ValidateExpression(string expression)
        {
            // 预处理DateTime.Now - 使用共享工具
            expression = ExpressionUtils.PreprocessDateTimeExpression(expression);

            // 委托给验证器
            return _validator.Validate(expression);
        }

        #endregion

        #region 公开方法 - 求值

        /// <summary>
        /// 求值表达式(同步版本)
        /// </summary>
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

                // 2. 预处理表达式(替换变量)
                var processedExpression = _variableResolver.PreprocessExpression(expression);

                // 3. 求值
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
        /// 求值表达式(异步版本)
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

                // 2. 异步预处理表达式(替换变量,支持PLC异步读取)
                var processedExpression = await _variableResolver.PreprocessExpressionAsync(expression);

                // 3. 求值
                var result = _evaluator.Evaluate(processedExpression);

                _logger?.LogDebug("表达式异步求值成功: {Expression} = {Result}", expression, result);
                return EvaluationResult.Succes(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "表达式异步求值失败: {Expression}", expression);
                return EvaluationResult.Error($"求值失败: {ex.Message}");
            }
        }

        #endregion

        #region 公开方法 - 变量赋值

        /// <summary>
        /// 直接赋值(简单值)
        /// </summary>
        public AssignmentResult AssignVariable(string targetVarName, object value)
        {
            try
            {
                var targetVar = _variableManager.TryFindVariableByName(targetVarName);
                if (targetVar == null)
                {
                    return AssignmentResult.Error($"目标变量 '{targetVarName}' 不存在");
                }

                var oldValue = targetVar.VarValue;
                targetVar.VarValue = value;
                targetVar.LastUpdated = DateTime.Now;

                _logger?.LogInformation("变量赋值成功: {VarName} = {Value}", targetVarName, value);
                return AssignmentResult.Succes(value, oldValue);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "变量赋值失败: {VarName}", targetVarName);
                return AssignmentResult.Error($"赋值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 表达式赋值(同步)
        /// </summary>
        public AssignmentResult AssignExpression(string targetVarName, string expression)
        {
            var evalResult = EvaluateExpression(expression);

            if (!evalResult.Success)
            {
                return AssignmentResult.Error($"表达式求值失败: {evalResult.ErrorMessage}");
            }

            return AssignVariable(targetVarName, evalResult.Result);
        }

        /// <summary>
        /// 表达式赋值(异步)
        /// </summary>
        public async Task<AssignmentResult> AssignExpressionAsync(string targetVarName, string expression)
        {
            var evalResult = await EvaluateExpressionAsync(expression);

            if (!evalResult.Success)
            {
                return AssignmentResult.Error($"表达式求值失败: {evalResult.ErrorMessage}");
            }

            return AssignVariable(targetVarName, evalResult.Result);
        }

        /// <summary>
        /// 从变量复制
        /// </summary>
        public AssignmentResult AssignFromVariable(string targetVarName, string sourceVarName)
        {
            var sourceVar = _variableManager.TryFindVariableByName(sourceVarName);
            if (sourceVar == null)
            {
                return AssignmentResult.Error($"源变量 '{sourceVarName}' 不存在");
            }

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
                {
                    return AssignmentResult.Error("PLCManager 未初始化");
                }

                var plcValue = await _plcManager.ReadPLCValueAsync(moduleName, address);
                if (plcValue == null)
                {
                    return AssignmentResult.Error($"无法读取PLC: {moduleName}.{address}");
                }

                return AssignVariable(targetVarName, plcValue);
            }
            catch (Exception ex)
            {
                return AssignmentResult.Error($"PLC读取失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 智能赋值(自动识别类型)
        /// </summary>
        public async Task<AssignmentResult> AssignSmartAsync(string targetVarName, string expression)
        {
            // 如果是单变量引用 {变量名},转为变量复制
            if (System.Text.RegularExpressions.Regex.IsMatch(expression, @"^\{[^}]+\}$"))
            {
                var varName = expression.Trim('{', '}');
                return AssignFromVariable(targetVarName, varName);
            }

            // 否则作为表达式求值
            return await AssignExpressionAsync(targetVarName, expression);
        }

        #endregion

        #region 公开方法 - 辅助功能

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
    }
}

using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 变量赋值执行引擎
    /// 负责执行各种类型的变量赋值操作
    /// </summary>
    public class VariableAssignmentEngine
    {
        private readonly GlobalVariableManager _variableManager;
        private readonly IPLCManager _plcManager;
        private readonly ExpressionValidator _expressionValidator;
        private readonly ILogger<VariableAssignmentEngine> _logger;

        // 表达式计算引擎
        private readonly ExpressionEvaluator _evaluator;

        // 变量引用模式
        private readonly Regex _variablePattern = new(@"\{(\w+)\}", RegexOptions.Compiled);

        public VariableAssignmentEngine(
            GlobalVariableManager variableManager,
            IPLCManager plcManager,
            ExpressionValidator expressionValidator,
            ILogger<VariableAssignmentEngine> logger = null)
        {
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _plcManager = plcManager;
            _expressionValidator = expressionValidator ?? throw new ArgumentNullException(nameof(expressionValidator));
            _logger = logger;

            _evaluator = new ExpressionEvaluator(_variableManager, _logger);
        }

        /// <summary>
        /// 执行变量赋值
        /// </summary>
        /// <param name="parameter">赋值参数</param>
        /// <returns>执行结果</returns>
        public async Task<AssignmentExecutionResult> ExecuteAssignmentAsync(Parameter_VariableAssignment parameter)
        {
            var result = new AssignmentExecutionResult();
            var startTime = DateTime.Now;

            try
            {
                _logger?.LogInformation("开始执行变量赋值: {TargetVar} = {AssignmentType}",
                    parameter.TargetVarName, parameter.AssignmentType);

                // 1. 验证参数
                var validationResult = ValidateParameterAsync(parameter);
                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.ErrorMessage = validationResult.Message;
                    result.ValidationErrors = validationResult.Errors;
                    return result;
                }

                // 2. 获取目标变量
                var targetVariable = _variableManager.FindVariableByName(parameter.TargetVarName);
                if (targetVariable == null)
                {
                    result.Success = false;
                    result.ErrorMessage = $"目标变量 '{parameter.TargetVarName}' 不存在";
                    return result;
                }

                // 记录原始值
                result.OldValue = targetVariable.VarValue;
                result.TargetVariableName = parameter.TargetVarName;

                // 3. 检查执行条件
                if (!string.IsNullOrWhiteSpace(parameter.Condition))
                {
                    var conditionResult = await EvaluateConditionAsync(parameter.Condition);
                    if (!conditionResult.Success)
                    {
                        result.Success = false;
                        result.ErrorMessage = $"条件评估失败: {conditionResult.ErrorMessage}";
                        return result;
                    }

                    if (!conditionResult.ConditionMet)
                    {
                        result.Success = true;
                        result.Skipped = true;
                        result.SkipReason = "执行条件不满足";
                        result.NewValue = result.OldValue; // 值未改变
                        _logger?.LogInformation("变量赋值跳过: 条件不满足 - {Condition}", parameter.Condition);
                        return result;
                    }
                }

                // 4. 根据赋值方式执行相应的逻辑 - 使用枚举而不是字符串
                var assignmentResult = await ExecuteAssignmentByTypeAsync(parameter, targetVariable);
                if (!assignmentResult.Success)
                {
                    result.Success = false;
                    result.ErrorMessage = assignmentResult.ErrorMessage;
                    return result;
                }

                // 5. 执行赋值
                var oldValue = targetVariable.VarValue;
                targetVariable.UpdateValue(assignmentResult.Value, $"变量赋值引擎: {parameter.AssignmentType.GetDescription()}");

                result.Success = true;
                result.NewValue = assignmentResult.Value;
                result.AssignmentType = parameter.AssignmentType.GetDescription(); // 使用枚举描述
                result.ExecutionTime = DateTime.Now - startTime;

                _logger?.LogInformation("变量赋值完成: {TargetVar} 从 '{OldValue}' 变为 '{NewValue}'",
                    parameter.TargetVarName, oldValue, assignmentResult.Value);

                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "执行变量赋值时发生错误: {TargetVar}", parameter.TargetVarName);

                result.Success = false;
                result.ErrorMessage = $"执行过程发生异常: {ex.Message}";
                result.Exception = ex;
                result.ExecutionTime = DateTime.Now - startTime;

                return result;
            }
        }

        /// <summary>
        /// 根据赋值类型执行相应逻辑
        /// </summary>
        private async Task<ValueCalculationResult> ExecuteAssignmentByTypeAsync(
            Parameter_VariableAssignment parameter,
            VarItem_Enhanced targetVariable)
        {
            // 直接使用枚举，不需要字符串转换
            switch (parameter.AssignmentType)
            {
                case AssignmentTypeEnum.DirectAssignment:
                    return ExecuteDirectValueAssignment(parameter.Expression, targetVariable);

                case AssignmentTypeEnum.ExpressionCalculation:
                    return await ExecuteExpressionCalculation(parameter.Expression, targetVariable);

                case AssignmentTypeEnum.VariableCopy:
                    return ExecuteVariableCopy(parameter.Expression, targetVariable);

                case AssignmentTypeEnum.PLCRead:
                    return await ExecutePLCReadFromConfig(parameter.DataSource.PlcConfig, targetVariable);

                default:
                    return ValueCalculationResult.Error($"不支持的赋值类型: {parameter.AssignmentType}");
            }
        }

        /// <summary>
        /// 执行PLC读取（使用配置对象）
        /// </summary>
        private async Task<ValueCalculationResult> ExecutePLCReadFromConfig(PlcAddressConfig plcConfig, VarItem_Enhanced targetVariable)
        {
            try
            {
                _logger?.LogDebug("执行PLC读取: {ModuleName}.{Address}", plcConfig.ModuleName, plcConfig.Address);

                if (_plcManager == null)
                {
                    return ValueCalculationResult.Error("PLC管理器未初始化");
                }

                if (string.IsNullOrEmpty(plcConfig.ModuleName))
                {
                    return ValueCalculationResult.Error("PLC模块名不能为空");
                }

                if (string.IsNullOrEmpty(plcConfig.Address))
                {
                    return ValueCalculationResult.Error("PLC地址不能为空");
                }

                // 从PLC读取值
                var value = await _plcManager.ReadPLCValueAsync(plcConfig.ModuleName, plcConfig.Address);

                // 转换为目标类型
                var convertedValue = ConvertValueToTargetType(value?.ToString(), targetVariable.VarType);

                return ValueCalculationResult.Successs(convertedValue, $"从PLC读取: {plcConfig.ModuleName}.{plcConfig.Address}");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "PLC读取失败: {ModuleName}.{Address}", plcConfig.ModuleName, plcConfig.Address);
                return ValueCalculationResult.Error($"PLC读取失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 执行直接值赋值
        /// </summary>
        private ValueCalculationResult ExecuteDirectValueAssignment(string expression, VarItem_Enhanced targetVariable)
        {
            try
            {
                _logger?.LogDebug("执行直接值赋值: {Expression}", expression);

                // 移除外层引号（如果有）
                var value = expression?.Trim();
                if (value?.StartsWith("\"") == true && value.EndsWith("\""))
                {
                    value = value.Substring(1, value.Length - 2);
                }
                else if (value?.StartsWith("'") == true && value.EndsWith("'"))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                // 尝试转换为目标变量的类型
                var convertedValue = ConvertValueToTargetType(value, targetVariable.VarType);

                return ValueCalculationResult.Successs(convertedValue, "直接值赋值");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "直接值赋值失败");
                return ValueCalculationResult.Error($"直接值赋值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 执行表达式计算
        /// </summary>
        private async Task<ValueCalculationResult> ExecuteExpressionCalculation(string expression, VarItem_Enhanced targetVariable)
        {
            try
            {
                _logger?.LogDebug("执行表达式计算: {Expression}", expression);

                // 使用表达式评估器计算结果
                var result = await _evaluator.EvaluateAsync(expression);

                if (!result.Success)
                {
                    return ValueCalculationResult.Error($"表达式计算失败: {result.ErrorMessage}");
                }

                // 转换为目标类型
                var convertedValue = ConvertValueToTargetType(result.Value?.ToString(), targetVariable.VarType);

                return ValueCalculationResult.Successs(convertedValue, "表达式计算");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "表达式计算失败");
                return ValueCalculationResult.Error($"表达式计算失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 执行变量复制
        /// </summary>
        private ValueCalculationResult ExecuteVariableCopy(string expression, VarItem_Enhanced targetVariable)
        {
            try
            {
                _logger?.LogDebug("执行变量复制: {Expression}", expression);

                // 处理变量引用格式 {VariableName} 或直接变量名
                string sourceVariableName;

                var match = _variablePattern.Match(expression);
                if (match.Success)
                {
                    sourceVariableName = match.Groups[1].Value;
                }
                else
                {
                    sourceVariableName = expression.Trim();
                }

                // 查找源变量
                var sourceVariable = _variableManager.FindVariableByName(sourceVariableName);
                if (sourceVariable == null)
                {
                    return ValueCalculationResult.Error($"源变量 '{sourceVariableName}' 不存在");
                }

                // 复制值并转换类型
                var convertedValue = ConvertValueToTargetType(sourceVariable.VarValue?.ToString(), targetVariable.VarType);

                return ValueCalculationResult.Successs(convertedValue, $"从变量 '{sourceVariableName}' 复制");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "变量复制失败");
                return ValueCalculationResult.Error($"变量复制失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 验证赋值参数
        /// </summary>
        private ValidationResult ValidateParameterAsync(Parameter_VariableAssignment parameter)
        {
            try
            {
                var errors = new List<string>();

                // 基本参数检查
                if (string.IsNullOrWhiteSpace(parameter.TargetVarName))
                {
                    errors.Add("目标变量名不能为空");
                }

                // 根据赋值类型验证不同字段
                switch (parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.PLCRead:
                        // PLC读取验证DataSource
                        if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.ModuleName))
                        {
                            errors.Add("PLC模块名不能为空");
                        }
                        if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.Address))
                        {
                            errors.Add("PLC地址不能为空");
                        }
                        break;

                    default:
                        // 其他类型验证Expression
                        if (string.IsNullOrWhiteSpace(parameter.Expression))
                        {
                            errors.Add("赋值表达式不能为空");
                        }
                        break;
                }

                if (errors.Count != 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Errors = errors,
                        Message = string.Join("; ", errors)
                    };
                }

                // 检查目标变量是否存在
                var targetVariable = _variableManager.FindVariableByName(parameter.TargetVarName);
                if (targetVariable == null)
                {
                    errors.Add($"目标变量 '{parameter.TargetVarName}' 不存在");
                }

                // 表达式验证（非PLC读取类型）
                if (parameter.AssignmentType != AssignmentTypeEnum.PLCRead)
                {
                    var validationContext = new ValidationContext
                    {
                        TargetVariableName = parameter.TargetVarName,
                        TargetVariableType = targetVariable?.VarType
                    };

                    var expressionValidation = _expressionValidator.ValidateExpression(parameter.Expression, validationContext);
                    if (!expressionValidation.IsValid)
                    {
                        errors.AddRange(expressionValidation.Errors);
                    }
                }

                // 条件表达式验证（如果有）
                if (!string.IsNullOrWhiteSpace(parameter.Condition))
                {
                    var conditionValidation = _expressionValidator.ValidateExpression(parameter.Condition);
                    if (!conditionValidation.IsValid)
                    {
                        errors.AddRange(conditionValidation.Errors.Select(e => $"条件表达式错误: {e}"));
                    }
                }

                return new ValidationResult
                {
                    IsValid = errors.Count == 0,
                    Errors = errors,
                    Message = errors.Count != 0 ? string.Join("; ", errors) : "验证通过"
                };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "参数验证时发生错误");
                return ValidationResult.Error($"参数验证失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 评估执行条件
        /// </summary>
        private async Task<ConditionEvaluationResult> EvaluateConditionAsync(string condition)
        {
            try
            {
                var result = await _evaluator.EvaluateAsync(condition);
                if (!result.Success)
                {
                    return new ConditionEvaluationResult
                    {
                        Success = false,
                        ErrorMessage = result.ErrorMessage
                    };
                }

                // 尝试将结果转换为布尔值
                bool conditionMet = false;
                if (result.Value is bool boolValue)
                {
                    conditionMet = boolValue;
                }
                else if (result.Value != null)
                {
                    // 尝试解析字符串为布尔值
                    var stringValue = result.Value.ToString();
                    if (bool.TryParse(stringValue, out var parsedBool))
                    {
                        conditionMet = parsedBool;
                    }
                    else
                    {
                        // 非空值视为true
                        conditionMet = !string.IsNullOrEmpty(stringValue) && stringValue != "0";
                    }
                }

                return new ConditionEvaluationResult
                {
                    Success = true,
                    ConditionMet = conditionMet,
                    EvaluatedValue = result.Value
                };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "条件评估失败: {Condition}", condition);
                return new ConditionEvaluationResult
                {
                    Success = false,
                    ErrorMessage = $"条件评估异常: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        private object ConvertValueToTargetType(string value, string targetType)
        {
            if (value == null) return null;

            try
            {
                switch (targetType?.ToUpperInvariant())
                {
                    case "STRING":
                        return value;

                    case "INTEGER":
                    case "INT":
                        if (int.TryParse(value, out var intValue))
                            return intValue;
                        break;

                    case "DOUBLE":
                    case "FLOAT":
                        if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var doubleValue))
                            return doubleValue;
                        break;

                    case "DECIMAL":
                        if (decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var decimalValue))
                            return decimalValue;
                        break;

                    case "BOOLEAN":
                    case "BOOL":
                        if (bool.TryParse(value, out var boolValue))
                            return boolValue;
                        // 数值转换：0为false，非0为true
                        if (int.TryParse(value, out var numValue))
                            return numValue != 0;
                        break;

                    case "DATETIME":
                        if (DateTime.TryParse(value, out var dateValue))
                            return dateValue;
                        break;

                    default:
                        // 未知类型，返回字符串
                        return value;
                }

                // 如果转换失败，记录警告但仍返回原字符串值
                _logger?.LogWarning("类型转换失败: '{Value}' -> {TargetType}, 保持为字符串类型", value, targetType);
                return value;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "类型转换异常: '{Value}' -> {TargetType}", value, targetType);
                return value; // 转换失败时返回原值
            }
        }

    }

    #region 辅助类和枚举定义

    /// <summary>
    /// 赋值类型枚举
    /// </summary>
    public enum AssignmentType
    {
        DirectValue,            // 直接值赋值
        ExpressionCalculation,  // 表达式计算
        VariableCopy,          // 变量复制
        PLCRead                // PLC读取
    }

    /// <summary>
    /// 赋值执行结果
    /// </summary>
    public class AssignmentExecutionResult
    {

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public Exception Exception { get; set; }

        public string TargetVariableName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
        public string AssignmentType { get; set; }

        public bool Skipped { get; set; }
        public string SkipReason { get; set; }

        public TimeSpan ExecutionTime { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// 值计算结果
    /// </summary>
    public class ValueCalculationResult
    {
        public bool Success { get; set; }
        public object Value { get; set; }
        public string ErrorMessage { get; set; }
        public string CalculationMethod { get; set; }

        public static ValueCalculationResult Successs(object value, string method = "")
        {
            return new ValueCalculationResult
            {
                Success = true,
                Value = value,
                CalculationMethod = method
            };
        }

        public static ValueCalculationResult Error(string errorMessage)
        {
            return new ValueCalculationResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }

    /// <summary>
    /// 条件评估结果
    /// </summary>
    public class ConditionEvaluationResult
    {
        public bool Success { get; set; }
        public bool ConditionMet { get; set; }
        public object EvaluatedValue { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// PLC地址信息
    /// </summary>
    public class PLCAddressInfo
    {
        public string ModuleName { get; set; }
        public string Address { get; set; }
    }

    /// <summary>
    /// PLC读取结果
    /// </summary>
    public class PLCReadResult
    {
        public bool Success { get; set; }
        public object Value { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ReadTime { get; set; }
    }

    #endregion
}
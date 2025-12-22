using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 变量赋值执行引擎
    /// 现在作为 ExpressionEngine 的简化包装器
    /// 负责处理参数模型并委托给统一的表达式引擎
    /// </summary>
    public class VariableAssignmentEngine
    {
        private readonly ExpressionEngine _expressionEngine;
        private readonly ILogger<VariableAssignmentEngine> _logger;

        public VariableAssignmentEngine(
            GlobalVariableManager variableManager,
            IPLCManager plcManager,
            ILogger<VariableAssignmentEngine> logger = null)
        {
            ArgumentNullException.ThrowIfNull(variableManager);

            _logger = logger;

            // 创建统一的表达式引擎
            _expressionEngine = new ExpressionEngine(variableManager, plcManager, null);
        }

        /// <summary>
        /// 执行变量赋值
        /// </summary>
        /// <param name="parameter">赋值参数</param>
        /// <returns>执行结果</returns>
        public async Task<AssignmentResult> ExecuteAssignmentAsync(Parameter_VariableAssignment parameter)
        {
            var result = new AssignmentResult();
            var startTime = DateTime.Now;

            try
            {
                _logger?.LogInformation("开始执行变量赋值: {TargetVar} = {AssignmentType}",
                    parameter.TargetVarName, parameter.AssignmentType);

                // 1. 验证参数基本有效性
                var validationResult = ValidateParameter(parameter);
                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.ErrorMessage = validationResult.Message;
                    result.ValidationErrors = validationResult.Errors;
                    return result;
                }

                // 2. 根据赋值类型执行不同的赋值操作
                AssignmentResult assignResult = parameter.AssignmentType switch
                {
                    // 直接赋值
                    VariableAssignmentType.DirectAssignment =>
                        _expressionEngine.AssignVariable(parameter.TargetVarName, parameter.Expression),

                    // 表达式计算赋值
                    VariableAssignmentType.ExpressionCalculation =>
                        await _expressionEngine.AssignExpressionAsync(parameter.TargetVarName, parameter.Expression),
                        
                // 从其他变量复制
                VariableAssignmentType.VariableCopy =>
                        _expressionEngine.AssignFromVariable(parameter.TargetVarName, parameter.DataSource.VariableName),

                    // 从PLC读取
                    VariableAssignmentType.PLCRead =>
                        await _expressionEngine.AssignFromPlcAsync(
                            parameter.TargetVarName,
                            parameter.DataSource.PlcConfig.ModuleName,
                            parameter.DataSource.PlcConfig.Address),

                    // 智能赋值（自动识别）
                    _ => await _expressionEngine.AssignSmartAsync(parameter.TargetVarName, parameter.Expression)
                };

                // 3. 填充执行结果
                result.Success = assignResult.Success;
                result.ErrorMessage = assignResult.ErrorMessage;
                result.NewValue = assignResult.NewValue;
                result.OldValue = assignResult.OldValue;
                result.ExecutionTime = DateTime.Now - startTime;

                if (result.Success)
                {
                    _logger?.LogInformation("赋值执行成功: {TargetVar} = {NewValue} (耗时: {Duration}ms)",
                        parameter.TargetVarName, result.NewValue, result.ExecutionTime.TotalMilliseconds);
                }
                else
                {
                    _logger?.LogError("赋值执行失败: {TargetVar}, 错误: {Error}",
                        parameter.TargetVarName, result.ErrorMessage);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "执行变量赋值时发生异常: {TargetVar}", parameter.TargetVarName);

                result.Success = false;
                result.ErrorMessage = $"执行失败: {ex.Message}";
                result.ExecutionTime = DateTime.Now - startTime;

                return result;
            }
        }

        /// <summary>
        /// 验证赋值参数的基本有效性
        /// </summary>
        private ValidationResult ValidateParameter(Parameter_VariableAssignment parameter)
        {
            var result = new ValidationResult { IsValid = true };

            // 检查目标变量名
            if (string.IsNullOrWhiteSpace(parameter.TargetVarName))
            {
                result.IsValid = false;
                result.Message = "目标变量名不能为空";
                result.Errors.Add("TargetVarName is required");
                return result;
            }

            // 根据赋值类型检查必要参数
            switch (parameter.AssignmentType)
            {
                case VariableAssignmentType.DirectAssignment:
                    if (parameter.Expression == null)
                    {
                        result.IsValid = false;
                        result.Message = "直接赋值的值不能为空";
                        result.Errors.Add("Expression is required for Direct assignment");
                    }
                    break;

                case VariableAssignmentType.ExpressionCalculation:
                    if (string.IsNullOrWhiteSpace(parameter.Expression))
                    {
                        result.IsValid = false;
                        result.Message = "表达式不能为空";
                        result.Errors.Add("Expression is required for Expression calculation");
                    }
                    break;

                case VariableAssignmentType.VariableCopy:
                    if (string.IsNullOrWhiteSpace(parameter.DataSource.VariableName))
                    {
                        result.IsValid = false;
                        result.Message = "源变量名不能为空";
                        result.Errors.Add("DataSource.VariableName is required for Variable copy");
                    }
                    break;

                case VariableAssignmentType.PLCRead:
                    if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.ModuleName))
                    {
                        result.IsValid = false;
                        result.Message = "PLC模块名不能为空";
                        result.Errors.Add("DataSource.PlcConfig.ModuleName is required for PLC read");
                    }
                    if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.Address))
                    {
                        result.IsValid = false;
                        result.Message = "PLC地址不能为空";
                        result.Errors.Add("DataSource.PlcConfig.Address is required for PLC read");
                    }
                    break;
            }

            if (result.IsValid)
            {
                result.Message = "参数验证通过";
            }

            return result;
        }
    }





}
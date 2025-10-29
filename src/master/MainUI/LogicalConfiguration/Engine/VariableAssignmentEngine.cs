using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;
using Sunny.UI;
using System.ComponentModel;

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
            var expressionLogger = logger?.LoggerFactory?.CreateLogger<ExpressionEngine>();
            _expressionEngine = new ExpressionEngine(variableManager, plcManager, expressionLogger);
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
                        _expressionEngine.AssignDirectValue(parameter.TargetVarName, parameter.DirectValue),

                    // 表达式计算赋值
                    (AssignmentResult)VariableAssignmentType.Expression =>
                        await _expressionEngine.AssignExpressionAsync(parameter.TargetVarName, parameter.ExpressionValue),

                    // 从其他变量复制
                    VariableAssignmentType.FromVariable =>
                        _expressionEngine.AssignFromVariable(parameter.TargetVarName, parameter.SourceVariableName),

                    // 从PLC读取
                    VariableAssignmentType.FromPLC =>
                        await _expressionEngine.AssignFromPlcAsync(
                            parameter.TargetVarName,
                            parameter.PlcModuleName,
                            parameter.PlcKeyName),

                    // 智能赋值（自动识别）
                    _ => await _expressionEngine.AssignSmartAsync(parameter.TargetVarName, parameter.ValueExpression)
                };

                // 3. 填充执行结果
                result.Success = assignResult.Succes;
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
                        result.Errors.Add("DirectValue is required for Direct assignment");
                    }
                    break;

                case VariableAssignmentType.ExpressionCalculation:
                    if (string.IsNullOrWhiteSpace(parameter.Expression))
                    {
                        result.IsValid = false;
                        result.Message = "表达式不能为空";
                        result.Errors.Add("ExpressionValue is required for Expression assignment");
                    }
                    break;

                case VariableAssignmentType.VariableCopy:
                    if (string.IsNullOrWhiteSpace(parameter.DataSource.VariableName))
                    {
                        result.IsValid = false;
                        result.Message = "源变量名不能为空";
                        result.Errors.Add("SourceVariableName is required for FromVariable assignment");
                    }
                    break;

                case VariableAssignmentType.PLCRead:
                    if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.ModuleName))
                    {
                        result.IsValid = false;
                        result.Message = "PLC模块名不能为空";
                        result.Errors.Add("PlcModuleName is required for FromPLC assignment");
                    }
                    if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.Address))
                    {
                        result.IsValid = false;
                        result.Message = "PLC地址不能为空";
                        result.Errors.Add("PlcKeyName is required for FromPLC assignment");
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

    #region 赋值类型枚举

    ///// <summary>
    ///// 变量赋值类型
    ///// </summary>
    //public enum AssignmentTypeEnum
    //{
    //    /// <summary>
    //    /// 直接赋值 - 将固定值直接赋给目标变量
    //    /// </summary>
    //    [Description("直接赋值")]
    //    DirectAssignment,

    //    /// <summary>
    //    /// 表达式计算 - 通过数学表达式计算结果后赋值
    //    /// </summary>
    //    [Description("表达式计算")]
    //    ExpressionCalculation,

    //    /// <summary>
    //    /// 从其他变量复制 - 将其他变量的值复制到目标变量
    //    /// </summary>
    //    [Description("从其他变量复制")]
    //    VariableCopy,

    //    /// <summary>
    //    /// 从PLC读取 - 从指定的PLC模块和地址读取值
    //    /// </summary>
    //    [Description("从PLC读取")]
    //    PLCRead
    //}

    #endregion

    #region 执行结果类

    /// <summary>
    /// 赋值执行结果
    /// </summary>
    public class AssignmentExecutionResult
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public object NewValue { get; set; }

        /// <summary>
        /// 旧值
        /// </summary>
        public object OldValue { get; set; }

        /// <summary>
        /// 执行耗时
        /// </summary>
        public TimeSpan ExecutionTime { get; set; }

        /// <summary>
        /// 验证错误列表
        /// </summary>
        public List<string> ValidationErrors { get; set; } = new();
    }

    #endregion
}
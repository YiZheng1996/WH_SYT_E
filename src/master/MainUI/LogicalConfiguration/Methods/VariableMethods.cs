using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 变量管理方法集合
    /// </summary>
    public class VariableMethods(IWorkflowStateService workflowStateService,
        GlobalVariableManager globalVariableManager) : DSLMethodBase
    {
        public override string Category => "变量管理";

        public override string Description => "提供变量定义、赋值等变量管理功能";

        private readonly IWorkflowStateService _workflowStateService = workflowStateService;
        private readonly GlobalVariableManager _globalVariableManager = globalVariableManager;

        /// <summary>
        /// 变量定义方法
        /// </summary>
        public async Task<bool> DefineVar(Parameter_DefineVar param)
        {
            return await ExecuteWithLogging(param, () =>
            {
                var variables = _globalVariableManager.GetAllVariables();

                // 检查变量是否已存在
                var existingVar = variables.FirstOrDefault(v => v.VarName == param.VarName);
                if (existingVar != null)
                {
                    existingVar.VarName = param.VarName;
                    existingVar.VarType = param.VarType;
                    existingVar.UpdateValue(param.VarValue, "变量定义更新");
                    return Task.FromResult(true);
                }
                else
                {
                    var newVar = new VarItem_Enhanced
                    {
                        VarName = param.VarName,
                        VarValue = param.VarValue,
                        VarType = param.VarType,
                        LastUpdated = DateTime.Now,
                        IsAssignedByStep = false,
                        AssignmentType = VariableAssignmentType.None
                    };
                    _workflowStateService.AddVariable(newVar);
                    return Task.FromResult(true);
                }
            }, false); // 默认返回false
        }

        /// <summary>
        /// 变量赋值方法
        /// </summary>
        public async Task<bool> VariableAssignment(Parameter_VariableAssignment param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                var targetVar = _globalVariableManager.FindVariableByName(param.TargetVarName) ??
                    throw new ArgumentException($"目标变量不存在: {param.TargetVarName}");

                string newValue = await CalculateAssignmentValue(param);
                targetVar.UpdateValue(newValue, $"变量赋值: {param.TargetVarName}");

                return true;
            }, false);
        }

        /// <summary>
        /// 计算赋值值（私有方法不需要包装）
        /// </summary>
        private async Task<string> CalculateAssignmentValue(Parameter_VariableAssignment param)
        {
            // 实现赋值逻辑
            await Task.CompletedTask;
            return param.TargetVarName.ToString() ?? "";
        }
    }
}

using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 变量管理方法集合
    /// </summary>
    public class VariableMethods(
        GlobalVariableManager globalVariableManager,
        VariableAssignmentEngine assignmentEngine) : DSLMethodBase
    {
        public override string Category => "变量管理";
        public override string Description => "提供变量定义、赋值等变量管理功能";

        private readonly GlobalVariableManager _globalVariableManager = globalVariableManager ?? throw new ArgumentNullException(nameof(globalVariableManager));
        private readonly VariableAssignmentEngine _assignmentEngine = assignmentEngine ?? throw new ArgumentNullException(nameof(assignmentEngine));

        /// <summary>
        /// 变量定义方法
        /// </summary>
        public async Task<bool> DefineVar(Parameter_DefineVar param)
        {
            return await ExecuteWithLogging(param, () =>
            {
                // 统一使用 GlobalVariableManager
                _globalVariableManager.AddOrUpdateVariable(new VarItem_Enhanced
                {
                    VarName = param.VarName,
                    VarValue = param.VarValue,
                    VarType = param.VarType,
                    LastUpdated = DateTime.Now,
                    IsAssignedByStep = false,
                    AssignmentType = VariableAssignmentType.None
                });

                return Task.FromResult(true);
            }, false);
        }

        /// <summary>
        /// 变量赋值方法
        /// </summary>
        public async Task<bool> VariableAssignment(Parameter_VariableAssignment param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                // 1. 验证目标变量是否存在
                var targetVar = _globalVariableManager.FindVariableByName(param.TargetVarName) ??
                throw new ArgumentException($"目标变量不存在: {param.TargetVarName}");

                // 2. 使用赋值引擎执行赋值操作
                var result = await _assignmentEngine.ExecuteAssignmentAsync(param);

                // 3. 检查执行结果
                if (!result.Success)
                {
                    throw new InvalidOperationException($"变量赋值失败: {result.ErrorMessage}");
                }

                // 4. 记录日志
                NlogHelper.Default.Info(
                    $"变量赋值成功 - 变量: {param.TargetVarName}, " +
                    $"赋值类型: {param.AssignmentType}, " +
                    $"新值: {result.NewValue}, " +
                    $"旧值: {result.OldValue}, " +
                    $"耗时: {result.ExecutionTime.TotalMilliseconds}ms");

                return true;
            }, false);
        }
    }
}
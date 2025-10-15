using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 流程控制方法集合
    /// </summary>
    public class FlowControlMethods(IWorkflowStateService workflowStateService) : DSLMethodBase
    {
        public override string Category => "流程控制";
        public override string Description => "提供条件判断、循环等流程控制功能";

        private readonly IWorkflowStateService _workflowStateService = workflowStateService;

        /// <summary>
        /// 条件评估方法 - 返回下一步骤索引
        /// </summary>
        public async Task<int> EvaluateCondition(Parameter_Condition param)
        {
            // 错误处理，异常时返回False分支索引
            return await ExecuteWithLogging(param, async () =>
            {
                // 获取变量值
                var variables = _workflowStateService.GetVariables<VarItem_Enhanced>().ToList();
                var variable = variables.FirstOrDefault(v => v.VarName == param.VarName) ??
                    throw new ArgumentException($"变量 {param.VarName} 不存在");

                // 执行条件比较
                bool conditionResult = await EvaluateConditionInternal(variable.VarValue, param.Operator, param.Value);

                int nextStep = conditionResult ? param.TrueStepIndex : param.FalseStepIndex;

                // 可以添加额外的日志信息，但不是必须的
                NlogHelper.Default.Info($"条件判断结果: {param.VarName}({variable.VarValue}) {param.Operator} {param.Value} = {conditionResult}, 下一步: {nextStep}");

                return nextStep;
            }, param.FalseStepIndex); // 异常时返回False分支索引
        }

        ///// <summary>
        ///// 循环控制方法 - 新增功能示例
        ///// </summary>
        //public async Task<bool> LoopControl(Parameter_Loop param)
        //{
        //    return await ExecuteWithLogging(param, async () =>
        //    {
        //        // 获取循环计数变量
        //        var loopVar = GlobalVariableManager.FindVariableByName(param.CounterVarName);
        //        if (loopVar == null)
        //        {
        //            throw new ArgumentException($"循环计数变量不存在: {param.CounterVarName}");
        //        }

        //        // 解析当前计数值
        //        if (!int.TryParse(loopVar.VarValue?.ToString(), out int currentCount))
        //        {
        //            throw new InvalidOperationException($"循环计数变量值无效: {loopVar.VarValue}");
        //        }

        //        // 检查是否达到循环上限
        //        if (currentCount >= param.MaxIterations)
        //        {
        //            // 重置计数器
        //            loopVar.UpdateValue("0", "循环结束重置");
        //            return false; // 退出循环
        //        }

        //        // 增加计数器
        //        currentCount++;
        //        loopVar.UpdateValue(currentCount.ToString(), $"循环计数增加到 {currentCount}");

        //        return true; // 继续循环
        //    }, false);
        //}

        ///// <summary>
        ///// 跳转控制方法 - 无条件跳转
        ///// </summary>
        //public async Task<int> Jump(Parameter_Jump param)
        //{
        //    return await ExecuteWithLogging(param, async () =>
        //    {
        //        // 验证跳转目标的有效性
        //        if (param.TargetStepIndex < 0)
        //        {
        //            throw new ArgumentException($"无效的跳转目标索引: {param.TargetStepIndex}");
        //        }

        //        // 可以添加跳转前的准备工作
        //        await PrepareForJump(param);

        //        return param.TargetStepIndex;
        //    }, -1); // 异常时返回-1表示跳转失败
        //}

        ///// <summary>
        ///// 延时跳转方法 - 延时后跳转到指定步骤
        ///// </summary>
        //public async Task<int> DelayedJump(Parameter_DelayedJump param)
        //{
        //    return await ExecuteWithLogging(param, async () =>
        //    {
        //        // 验证参数
        //        if (param.DelayMs < 0)
        //        {
        //            throw new ArgumentException($"延时时间不能为负数: {param.DelayMs}");
        //        }

        //        if (param.TargetStepIndex < 0)
        //        {
        //            throw new ArgumentException($"无效的跳转目标索引: {param.TargetStepIndex}");
        //        }

        //        // 执行延时
        //        await Task.Delay(param.DelayMs);

        //        return param.TargetStepIndex;
        //    }, -1);
        //}

        ///// <summary>
        ///// 条件循环方法 - while循环的实现
        ///// </summary>
        //public async Task<bool> WhileLoop(Parameter_WhileLoop param)
        //{
        //    return await ExecuteWithLogging(param, async () =>
        //    {
        //        // 获取条件变量
        //        var conditionVar = GlobalVariableManager.FindVariableByName(param.ConditionVarName);
        //        if (conditionVar == null)
        //        {
        //            throw new ArgumentException($"条件变量不存在: {param.ConditionVarName}");
        //        }

        //        // 评估条件
        //        bool conditionResult = await EvaluateConditionInternal(
        //            conditionVar.VarValue?.ToString(),
        //            param.Operator,
        //            param.CompareValue);

        //        // 检查最大迭代次数防止死循环
        //        var iterationVar = GlobalVariableManager.FindVariableByName($"{param.ConditionVarName}_Iterations");
        //        if (iterationVar == null)
        //        {
        //            // 创建迭代计数器
        //            iterationVar = new VarItem_Enhanced
        //            {
        //                VarName = $"{param.ConditionVarName}_Iterations",
        //                VarValue = "0",
        //                VarType = "Int32"
        //            };
        //            SingletonStatus.Instance.Obj.Add(iterationVar);
        //        }

        //        int.TryParse(iterationVar.VarValue?.ToString(), out int iterations);

        //        if (iterations >= param.MaxIterations)
        //        {
        //            // 重置计数器并退出循环
        //            iterationVar.UpdateValue("0", "达到最大迭代次数，重置计数器");
        //            return false;
        //        }

        //        if (conditionResult)
        //        {
        //            // 增加迭代计数
        //            iterations++;
        //            iterationVar.UpdateValue(iterations.ToString(), $"循环迭代计数: {iterations}");
        //        }
        //        else
        //        {
        //            // 条件不满足，重置计数器
        //            iterationVar.UpdateValue("0", "循环条件不满足，重置计数器");
        //        }

        //        return conditionResult;
        //    }, false);
        //}

        #region 私有辅助方法（不需要包装，保持原样）

        /// <summary>
        /// 条件比较的具体实现（私有方法）
        /// </summary>
        private async Task<bool> EvaluateConditionInternal(string value, string operatorStr, string targetValue)
        {
            await Task.CompletedTask; // 异步操作占位符

            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(targetValue))
            {
                return false;
            }

            try
            {
                // 尝试数值比较
                if (double.TryParse(value, out double numValue) && double.TryParse(targetValue, out double numTarget))
                {
                    return operatorStr switch
                    {
                        "==" or "=" => Math.Abs(numValue - numTarget) < 0.0001, // 浮点数相等比较
                        "!=" or "<>" => Math.Abs(numValue - numTarget) >= 0.0001,
                        ">" => numValue > numTarget,
                        "<" => numValue < numTarget,
                        ">=" => numValue >= numTarget,
                        "<=" => numValue <= numTarget,
                        _ => throw new NotSupportedException($"不支持的数值比较操作符: {operatorStr}")
                    };
                }

                // 字符串比较
                return operatorStr switch
                {
                    "==" or "=" => string.Equals(value, targetValue, StringComparison.OrdinalIgnoreCase),
                    "!=" or "<>" => !string.Equals(value, targetValue, StringComparison.OrdinalIgnoreCase),
                    "Contains" => value.Contains(targetValue, StringComparison.OrdinalIgnoreCase),
                    "StartsWith" => value.StartsWith(targetValue, StringComparison.OrdinalIgnoreCase),
                    "EndsWith" => value.EndsWith(targetValue, StringComparison.OrdinalIgnoreCase),
                    _ => throw new NotSupportedException($"不支持的字符串比较操作符: {operatorStr}")
                };
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"条件比较执行失败: {value} {operatorStr} {targetValue}", ex);
                return false;
            }
        }

        /// <summary>
        /// 跳转前的准备工作（私有方法）
        /// </summary>
        //private async Task PrepareForJump(Parameter_Jump param)
        //{
        //    await Task.CompletedTask;

        //    // 记录跳转信息
        //    NlogHelper.Default.Info($"准备跳转到步骤: {param.TargetStepIndex}，原因: {param.JumpReason ?? "未指定"}");

        //    // 可以在这里添加跳转前的清理工作
        //    // 例如：保存状态、释放资源等
        //}

        #endregion

    }
}
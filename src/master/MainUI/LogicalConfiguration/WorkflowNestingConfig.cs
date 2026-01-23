using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration
{
    /// <summary>
    /// 工作流嵌套配置 - 定义嵌套层级控制的全局规则
    /// </summary>
    public static class WorkflowNestingConfig
    {
        #region 配置常量

        /// <summary>
        /// 最大嵌套层级
        /// </summary>
        public const int MaxNestingLevel = 3;

        /// <summary>
        /// 是否启用嵌套验证
        /// </summary>
        public static bool EnableNestingValidation { get; set; } = true;

        /// <summary>
        /// 复杂度警告阈值
        /// </summary>
        public const int ComplexityWarningLevel = 2;

        /// <summary>
        /// 需要进行嵌套限制的步骤类型列表
        /// </summary>
        public static readonly HashSet<string> RestrictedStepTypes = new()
        {
            "条件判断",
            "循环工具"
        };

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查指定步骤类型是否受到嵌套限制
        /// </summary>
        public static bool IsRestrictedStepType(string stepName)
        {
            return RestrictedStepTypes.Contains(stepName);
        }

        /// <summary>
        /// 判断指定层级是否达到复杂度警告阈值
        /// </summary>
        public static bool ShouldShowComplexityWarning(int level)
        {
            return level >= ComplexityWarningLevel && level < MaxNestingLevel;
        }

        /// <summary>
        /// 判断指定层级是否已达上限
        /// </summary>
        public static bool IsMaxLevelReached(int level)
        {
            return level >= MaxNestingLevel;
        }

        /// <summary>
        /// 获取层级显示文本
        /// </summary>
        public static string GetLevelDisplayText(int level)
        {
            return level switch
            {
                0 => "顶层",
                1 => "第1层嵌套",
                2 => "第2层嵌套",
                3 => "第3层嵌套",
                _ => $"第{level}层嵌套"
            };
        }

        /// <summary>
        /// 获取层级警告消息
        /// </summary>
        public static string GetLevelWarningMessage(int currentLevel)
        {
            if (IsMaxLevelReached(currentLevel))
            {
                return $"已达到最大嵌套层级({MaxNestingLevel}层),无法继续添加条件判断或循环工具";
            }

            return ShouldShowComplexityWarning(currentLevel) ? 
                $"当前已是第{currentLevel}层嵌套,工作流复杂度较高,建议使用组合条件或拆分子流程" : string.Empty;
        }

        #endregion

        // ========== 新增：嵌套层级计算核心方法 ==========

        #region 嵌套层级计算

        /// <summary>
        /// 递归计算并更新步骤的嵌套层级
        /// 用于处理从JSON加载的旧数据,确保所有步骤都有正确的层级信息
        /// </summary>
        /// <param name="steps">步骤列表</param>
        /// <param name="parentLevel">父级层级(默认-1表示顶层的父级)</param>
        /// <param name="logger">可选的日志记录器</param>
        public static void RecalculateNestingLevels(
            List<ChildModel> steps,
            int parentLevel = -1,
            Microsoft.Extensions.Logging.ILogger logger = null)
        {
            if (steps == null || steps.Count == 0) return;

            // 当前层级 = 父级层级 + 1
            int currentLevel = parentLevel + 1;

            foreach (var step in steps)
            {
                // 设置当前步骤的层级
                step.NestingLevel = currentLevel;

                // 设置步骤类型标识
                if (IsRestrictedStepType(step.StepName))
                {
                    step.StepType = step.StepName == "条件判断" ? "Condition" : "Loop";
                }
                else
                {
                    step.StepType = "Normal";
                }

                logger?.LogDebug(
                    "步骤 [{StepName}] 层级设置为: {Level}, 类型: {Type}",
                    step.StepName, step.NestingLevel, step.StepType);

                // 递归处理子步骤
                ProcessNestedSteps(step, currentLevel, logger);
            }

            logger?.LogInformation(
                "完成层级计算, 共处理 {Count} 个步骤, 当前层级: {Level}",
                steps.Count, currentLevel);
        }

        /// <summary>
        /// 处理步骤内部的嵌套子步骤
        /// </summary>
        /// <param name="step">当前步骤</param>
        /// <param name="currentLevel">当前层级</param>
        /// <param name="logger">日志记录器</param>
        private static void ProcessNestedSteps(
            ChildModel step,
            int currentLevel,
            Microsoft.Extensions.Logging.ILogger logger)
        {
            if (step.StepParameter == null) return;

            try
            {
                switch (step.StepName)
                {
                    // 处理条件判断步骤
                    case "条件判断":
                        ProcessConditionStep(step, currentLevel, logger);
                        break;
                    // 处理循环工具步骤
                    case "循环工具":
                        ProcessLoopStep(step, currentLevel, logger);
                        break;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex,
                    "处理步骤 [{StepName}] 的嵌套层级时出错", step.StepName);
            }
        }

        /// <summary>
        /// 处理条件判断步骤的子步骤
        /// </summary>
        private static void ProcessConditionStep(
            ChildModel step,
            int currentLevel,
            Microsoft.Extensions.Logging.ILogger logger)
        {
            Parameter.Parameter_Condition conditionParam = null;

            // 尝试获取参数
            if (step.StepParameter is Parameter.Parameter_Condition param)
            {
                conditionParam = param;
            }
            else
            {
                // 尝试从JSON反序列化
                string jsonStr = step.StepParameter is string str
                    ? str
                    : Newtonsoft.Json.JsonConvert.SerializeObject(step.StepParameter);
                conditionParam = Newtonsoft.Json.JsonConvert.DeserializeObject<Parameter.Parameter_Condition>(jsonStr);
            }

            if (conditionParam == null) return;

            // 递归处理满足条件的子步骤
            if (conditionParam.TrueSteps != null && conditionParam.TrueSteps.Count > 0)
            {
                logger?.LogDebug("处理条件判断的TrueSteps, 父级层级: {Level}", currentLevel);
                RecalculateNestingLevels(conditionParam.TrueSteps, currentLevel, logger);
            }

            // 递归处理不满足条件的子步骤
            if (conditionParam.FalseSteps == null || conditionParam.FalseSteps.Count <= 0) return;

            logger?.LogDebug("处理条件判断的FalseSteps, 父级层级: {Level}", currentLevel);
            RecalculateNestingLevels(conditionParam.FalseSteps, currentLevel, logger);
        }

        /// <summary>
        /// 处理循环工具步骤的子步骤
        /// </summary>
        private static void ProcessLoopStep(
            ChildModel step,
            int currentLevel,
            Microsoft.Extensions.Logging.ILogger logger)
        {
            Parameter.Parameter_Loop loopParam = null;

            // 尝试获取参数
            if (step.StepParameter is Parameter.Parameter_Loop param)
            {
                loopParam = param;
            }
            else
            {
                // 尝试从JSON反序列化
                string jsonStr = step.StepParameter is string str
                    ? str
                    : Newtonsoft.Json.JsonConvert.SerializeObject(step.StepParameter);
                loopParam = Newtonsoft.Json.JsonConvert.DeserializeObject<Parameter.Parameter_Loop>(jsonStr);
            }

            if (loopParam == null) return;

            // 递归处理循环的子步骤
            if (loopParam.ChildSteps != null && loopParam.ChildSteps.Count > 0)
            {
                logger?.LogDebug("处理循环工具的ChildSteps, 父级层级: {Level}", currentLevel);
                RecalculateNestingLevels(loopParam.ChildSteps, currentLevel, logger);
            }
        }

        /// <summary>
        /// 应用嵌套层级到子步骤列表
        /// 通常用于在配置界面中设置子步骤的层级
        /// </summary>
        /// <param name="childSteps">子步骤列表</param>
        /// <param name="childLevel">子步骤的层级</param>
        /// <param name="parentStepId">父步骤ID</param>
        /// <param name="logger">日志记录器</param>
        public static void ApplyNestingLevelToChildSteps(
            List<ChildModel> childSteps,
            int childLevel,
            string parentStepId = "",
            Microsoft.Extensions.Logging.ILogger logger = null)
        {
            if (childSteps == null || childSteps.Count == 0) return;

            foreach (var childStep in childSteps)
            {
                // 设置嵌套层级
                childStep.NestingLevel = childLevel;

                // 设置父步骤ID
                childStep.ParentStepId = parentStepId;

                // 设置步骤类型标识
                if (IsRestrictedStepType(childStep.StepName))
                {
                    childStep.StepType = childStep.StepName == "条件判断" ? "Condition" : "Loop";
                }
                else
                {
                    childStep.StepType = "Normal";
                }

                logger?.LogDebug(
                    "子步骤 [{StepName}] 层级设置: Level={Level}, Parent={Parent}, Type={Type}",
                    childStep.StepName, childStep.NestingLevel, childStep.ParentStepId, childStep.StepType);
            }
        }

        #endregion
    }
}
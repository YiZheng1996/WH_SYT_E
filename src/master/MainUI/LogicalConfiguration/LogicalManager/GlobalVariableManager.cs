using MainUI.LogicalConfiguration.Services;

namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 全局变量管理器
    /// </summary>
    public class GlobalVariableManager(IWorkflowStateService workflowState)
    {
        private readonly IWorkflowStateService _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));


        #region 实例方法
        /// <summary>
        /// 获取所有变量
        /// </summary>
        public List<VarItem_Enhanced> GetAllVariables()
        {
            return _workflowState.GetAllVariables();
        }

        /// <summary>
        /// 通过名称查找变量
        /// </summary>
        public VarItem_Enhanced FindVariableByName(string varName)
        {
            if (string.IsNullOrEmpty(varName))
                return null;

            return _workflowState.FindVariableByName(varName);
        }

        /// <summary>
        /// 安全查找变量（不抛异常）
        /// </summary>
        public VarItem_Enhanced TryFindVariableByName(string varName)
        {
            try
            {
                return FindVariableByName(varName);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 添加或更新变量
        /// </summary>
        public void AddOrUpdateVariable(VarItem_Enhanced variable)
        {
            ArgumentNullException.ThrowIfNull(variable);

            var existing = TryFindVariableByName(variable.VarName);
            if (existing != null)
            {
                // 更新现有变量
                existing.VarType = variable.VarType;
                existing.VarValue = variable.VarValue;
                existing.VarText = variable.VarText;
                existing.LastUpdated = DateTime.Now;
            }
            else
            {
                // 添加新变量
                _workflowState.AddVariable(variable);
            }
        }

        /// <summary>
        /// 删除变量
        /// </summary>
        public bool RemoveVariable(string varName)
        {
            var variable = TryFindVariableByName(varName);
            if (variable != null)
            {
                return _workflowState.RemoveVariable(variable);
            }
            return false;
        }

        /// <summary>
        /// 检查变量冲突
        /// </summary>
        public VariableConflictInfo CheckVariableConflict(string varname, int excludeStepIndex)
        {
            var variable = TryFindVariableByName(varname);
            if (variable == null)
            {
                return new VariableConflictInfo { HasConflict = false };
            }

            if (variable.IsAssignedByStep && variable.AssignedByStepIndex != excludeStepIndex)
            {
                return new VariableConflictInfo
                {
                    HasConflict = true,
                    ConflictStepIndex = variable.AssignedByStepIndex,
                    ConflictStepInfo = variable.AssignedByStepInfo ?? "",
                    ConflictAssignmentType = (VariableAssignmentType)variable.AssignmentType
                };
            }

            return new VariableConflictInfo { HasConflict = false };
        }

        /// <summary>
        /// 验证步骤索引
        /// </summary>
        public bool ValidateStepIndex(int stepIndex)
        {
            return _workflowState.ValidateStepIndex(stepIndex);
        }

        /// <summary>
        /// 获取未被赋值的变量
        /// </summary>
        public List<VarItem_Enhanced> GetUnassignedVariables()
        {
            return GetAllVariables().Where(v => !v.IsAssignedByStep).ToList();
        }

        /// <summary>
        /// 获取被赋值的变量
        /// </summary>
        public List<VarItem_Enhanced> GetAssignedVariables()
        {
            return GetAllVariables().Where(v => v.IsAssignedByStep).ToList();
        }

        /// <summary>
        /// 清空所有变量
        /// </summary>
        public void ClearAllVariables()
        {
            _workflowState.ClearVariables();
        }

        #endregion

        #region 辅助数据类

        /// <summary>
        /// 变量赋值信息
        /// </summary>
        public class VariableAssignment
        {
            /// <summary>
            /// 变量名称
            /// </summary>
            public string VariableName { get; set; }

            /// <summary>
            /// 赋值描述（如"PLC读取(Module1.Tag1)"）
            /// </summary>
            public string AssignmentDescription { get; set; }

            /// <summary>
            /// 额外信息（可选）
            /// </summary>
            public string ExtraInfo { get; set; }
        }

        /// <summary>
        /// 变量冲突信息类
        /// </summary>
        public class VariableConflictInfo
        {
            /// <summary>
            /// 是否存在冲突
            /// </summary>
            public bool HasConflict { get; set; }

            /// <summary>
            /// 冲突的步骤索引
            /// </summary>
            public int ConflictStepIndex { get; set; } = -1;

            /// <summary>
            /// 冲突步骤的信息描述
            /// </summary>
            public string ConflictStepInfo { get; set; } = "";

            /// <summary>
            /// 冲突的赋值类型
            /// </summary>
            public VariableAssignmentType ConflictAssignmentType { get; set; } = VariableAssignmentType.None;

            /// <summary>
            /// 冲突详细说明
            /// </summary>
            public string ConflictDescription => HasConflict
                ? $"变量已被步骤{ConflictStepIndex + 1}({ConflictStepInfo})以{ConflictAssignmentType}方式赋值"
                : "无冲突";
        }

        /// <summary>
        /// 变量赋值类型
        /// </summary>
        public enum VariableAssignmentType
        {
            /// <summary>
            /// 变量未被赋值
            /// </summary>
            None = 0,

            /// <summary>
            /// 通过PLC读取被赋值
            /// </summary>
            PLCRead = 1,

            /// <summary>
            /// 通过表达式计算被赋值
            /// </summary>
            Expression = 2
        }

        /// <summary>
        /// 当前步骤信息
        /// </summary>
        public class CurrentStepInfo
        {
            /// <summary>
            /// 是否有效
            /// </summary>
            public bool IsValid { get; set; }

            /// <summary>
            /// 步骤索引
            /// </summary>
            public int StepIndex { get; set; }

            /// <summary>
            /// 步骤对象
            /// </summary>
            public ChildModel Step { get; set; }

            /// <summary>
            /// 步骤名称
            /// </summary>
            public string StepName { get; set; }
        }

        #endregion

    }
}



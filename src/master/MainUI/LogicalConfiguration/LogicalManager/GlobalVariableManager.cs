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
        /// 获取所有变量,不带系统变量
        /// </summary>
        public List<VarItem_Enhanced> GetAllUserVariables()
        {
            return _workflowState.GetAllVariables().Where(v => !v.IsSystemVariable).ToList();
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
        /// 确保变量存储时类型正确
        /// </summary>
        /// <param name="varName">变量名称</param>
        /// <param name="value">当前值</param>
        /// <param name="varType">值类型</param>
        public void UpdateVariableValue(string varName, object value, string varType)
        {
            var variable = FindVariableByName(varName);
            if (variable == null) return;

            // 根据变量类型转换值
            object convertedValue = varType.ToLower() switch
            {
                "int" => value is string s ? int.Parse(s) : Convert.ToInt32(value),
                "double" => value is string s ? double.Parse(s) : Convert.ToDouble(value),
                "bool" => value is string s ? bool.Parse(s) : Convert.ToBoolean(value),
                "string" => value?.ToString() ?? "",
                _ => value
            };

            variable.VarValue = convertedValue;
            variable.LastUpdated = DateTime.Now;
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
            _workflowState.ClearAllVariables();
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



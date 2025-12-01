using System.ComponentModel;

namespace MainUI.LogicalConfiguration
{
    /// <summary>
    /// 增强的VarItem类 - 添加赋值状态管理
    /// </summary>
    public class VarItem_Enhanced : VarItem
    {
        /// <summary>
        /// 变量是否被某个步骤赋值
        /// </summary>
        public bool IsAssignedByStep { get; set; }

        /// <summary>
        /// 赋值步骤的信息
        /// </summary>
        public string AssignedByStepInfo { get; set; }

        /// <summary>
        /// 赋值步骤的索引
        /// </summary>
        public int AssignedByStepIndex { get; set; } = -1;

        /// <summary>
        /// 变量的赋值类型
        /// </summary>
        public VariableAssignmentType AssignmentType { get; set; } = VariableAssignmentType.None;

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否为系统变量
        /// 系统变量不应该被 ClearVariables 清空
        /// </summary>
        public bool IsSystemVariable { get; set; } = false;

        /// <summary>
        /// 变量的历史值（可选，用于调试）
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public List<VariableHistoryItem> ValueHistory { get; set; } = [];

        /// <summary>
        /// 更新变量值并记录历史
        /// </summary>
        public void UpdateValue(object newValue, string source = "")
        {
            var oldValue = VarValue;
            VarValue = newValue?.ToString() ?? "";
            LastUpdated = DateTime.Now;

            // 记录历史
            ValueHistory.Add(new VariableHistoryItem
            {
                OldValue = oldValue?.ToString(),
                NewValue = VarValue.ToString(),
                Timestamp = LastUpdated,
                Source = source
            });

            // 只保留最近10条历史记录
            if (ValueHistory.Count > 10)
            {
                ValueHistory.RemoveAt(0);
            }
        }

        /// <summary>
        /// 设置变量的赋值状态
        /// </summary>
        public void SetAssignmentStatus(int stepIndex, string stepInfo, VariableAssignmentType type)
        {
            IsAssignedByStep = true;
            AssignedByStepIndex = stepIndex;
            AssignedByStepInfo = stepInfo;
            AssignmentType = type;
        }

        /// <summary>
        /// 清除赋值状态
        /// </summary>
        public void ClearAssignmentStatus()
        {
            IsAssignedByStep = false;
            AssignedByStepIndex = -1;
            AssignedByStepInfo = "";
            AssignmentType = VariableAssignmentType.None;
        }

    }

    /// <summary>
    /// 变量赋值类型
    /// </summary>
    public enum VariableAssignmentType
    {
        [Description("未赋值")]
        None,           // 未赋值

        /// <summary>
        /// 直接赋值 - 将固定值直接赋给目标变量
        /// </summary>
        [Description("直接赋值")]
        DirectAssignment,

        /// <summary>
        /// 表达式计算 - 通过数学表达式计算结果后赋值
        /// </summary>
        [Description("表达式计算")]
        ExpressionCalculation,

        /// <summary>
        /// 从其他变量复制 - 将其他变量的值复制到目标变量
        /// </summary>
        [Description("从其他变量复制")]
        VariableCopy,

        /// <summary>
        /// 从PLC读取 - 从指定的PLC模块和地址读取值
        /// </summary>
        [Description("从PLC读取")]
        PLCRead,
    }

    /// <summary>
    /// 变量历史记录项
    /// </summary>
    public class VariableHistoryItem
    {
        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime Timestamp { get; set; }

        public string Source { get; set; }
    }

    public class VarItem
    {
        /// <summary>
        /// 自定义变量名称
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// 自定义变量类型
        /// </summary>
        public string VarType { get; set; }

        /// <summary>
        /// 自定义变量值
        /// </summary>
        public object VarValue { get; set; }

        /// <summary>
        /// 自定义变量文本
        /// </summary>
        public string VarText { get; set; }
    }
}

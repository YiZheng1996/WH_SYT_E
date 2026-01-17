namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 条件判断参数
    /// 使用通用表达式输入面板，支持完整的条件表达式
    /// </summary>
    [Serializable]
    public class Parameter_Condition
    {
        /// <summary>
        /// 条件表达式（支持完整的条件表达式）
        /// 示例：{Temperature} > 100
        /// 示例：{Pressure} >= 5.0 && {Pressure} <= 6.0
        /// 示例：{Status} == "OK"
        /// </summary>
        public string ConditionExpression { get; set; } = "";

        /// <summary>
        /// 满足条件时执行的子步骤
        /// </summary>
        public List<ChildModel> TrueSteps { get; set; } = [];

        /// <summary>
        /// 不满足条件时执行的子步骤
        /// </summary>
        public List<ChildModel> FalseSteps { get; set; } = [];

        /// <summary>
        /// 条件描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 是否启用此条件判断
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }
}
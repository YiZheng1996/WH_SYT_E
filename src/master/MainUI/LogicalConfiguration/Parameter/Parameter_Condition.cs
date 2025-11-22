namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 条件运算符
    /// </summary>
    public enum ConditionOperator
    {
        等于,
        不等于,
        大于,
        小于,
        大于等于,
        小于等于,
        在范围内,
        不在范围内
    }

    /// <summary>
    /// 条件判断参数
    /// </summary>
    [Serializable]
    public class Parameter_Condition
    {
        /// <summary>
        /// 左值表达式（支持变量，如：{Pressure}）
        /// </summary>
        public string LeftExpression { get; set; } = "";

        /// <summary>
        /// 运算符
        /// </summary>
        public ConditionOperator Operator { get; set; } = ConditionOperator.等于;

        /// <summary>
        /// 右值表达式（单值比较时使用）
        /// </summary>
        public string RightExpression { get; set; } = "";

        /// <summary>
        /// 范围最小值（范围判断时使用）
        /// </summary>
        public string RangeMin { get; set; } = "";

        /// <summary>
        /// 范围最大值（范围判断时使用）
        /// </summary>
        public string RangeMax { get; set; } = "";

        /// <summary>
        /// 满足条件时执行的子步骤
        /// </summary>
        public List<Parent> TrueSteps { get; set; } = [];

        /// <summary>
        /// 不满足条件时执行的子步骤
        /// </summary>
        public List<Parent> FalseSteps { get; set; } = new List<Parent>();

        /// <summary>
        /// 条件描述
        /// </summary>
        public string Description { get; set; } = "";
    }
}
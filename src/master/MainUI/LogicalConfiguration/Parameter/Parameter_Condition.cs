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
        public List<Parent> TrueSteps { get; set; } = [];

        /// <summary>
        /// 不满足条件时执行的子步骤
        /// </summary>
        public List<Parent> FalseSteps { get; set; } = [];

        /// <summary>
        /// 条件描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 是否启用此条件判断
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        #region 兼容旧版本属性（向后兼容）

        /// <summary>
        /// 左值表达式（已弃用，保留用于向后兼容）
        /// </summary>
        [Obsolete("请使用 ConditionExpression 替代")]
        public string LeftExpression { get; set; } = "";

        /// <summary>
        /// 运算符（已弃用，保留用于向后兼容）
        /// </summary>
        [Obsolete("请使用 ConditionExpression 替代")]
        public ConditionOperator Operator { get; set; } = ConditionOperator.等于;

        /// <summary>
        /// 右值表达式（已弃用，保留用于向后兼容）
        /// </summary>
        [Obsolete("请使用 ConditionExpression 替代")]
        public string RightExpression { get; set; } = "";

        /// <summary>
        /// 范围最小值（已弃用，保留用于向后兼容）
        /// </summary>
        [Obsolete("请使用 ConditionExpression 替代")]
        public string RangeMin { get; set; } = "";

        /// <summary>
        /// 范围最大值（已弃用，保留用于向后兼容）
        /// </summary>
        [Obsolete("请使用 ConditionExpression 替代")]
        public string RangeMax { get; set; } = "";

        #endregion

        /// <summary>
        /// 迁移旧版本参数到新格式
        /// 如果检测到旧版本格式（ConditionExpression为空但有Left/Right表达式），自动转换
        /// </summary>
        public void MigrateFromLegacy()
        {
            // 如果已有新格式的表达式，不需要迁移
            if (!string.IsNullOrWhiteSpace(ConditionExpression))
                return;

            // 检查是否有旧格式数据
#pragma warning disable CS0618
            if (string.IsNullOrWhiteSpace(LeftExpression))
                return;

            // 根据旧运算符生成新表达式
            string operatorStr = Operator switch
            {
                ConditionOperator.等于 => "==",
                ConditionOperator.不等于 => "!=",
                ConditionOperator.大于 => ">",
                ConditionOperator.小于 => "<",
                ConditionOperator.大于等于 => ">=",
                ConditionOperator.小于等于 => "<=",
                ConditionOperator.在范围内 => "RANGE",
                ConditionOperator.不在范围内 => "NOT_RANGE",
                _ => "=="
            };

            if (operatorStr == "RANGE")
            {
                // 范围判断转换为 AND 表达式
                ConditionExpression = $"{LeftExpression} >= {RangeMin} && {LeftExpression} <= {RangeMax}";
            }
            else if (operatorStr == "NOT_RANGE")
            {
                // 不在范围内转换为 OR 表达式
                ConditionExpression = $"{LeftExpression} < {RangeMin} || {LeftExpression} > {RangeMax}";
            }
            else
            {
                // 普通比较
                ConditionExpression = $"{LeftExpression} {operatorStr} {RightExpression}";
            }
#pragma warning restore CS0618
        }
    }

    /// <summary>
    /// 条件运算符（保留用于向后兼容）
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
}
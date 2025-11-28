namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 循环参数
    /// </summary>
    [Serializable]
    public class Parameter_Loop
    {
        /// <summary>
        /// 循环次数表达式(可以是数字或变量,如:10 或 {MaxRetryCount})
        /// </summary>
        public string LoopCountExpression { get; set; } = "10";

        /// <summary>
        /// 循环计数器变量名(在子步骤中可通过此变量获取当前循环索引,从1开始)
        /// </summary>
        public string CounterVariableName { get; set; } = "LoopIndex";

        /// <summary>
        /// 是否启用计数器变量
        /// </summary>
        public bool EnableCounter { get; set; } = true;

        /// <summary>
        /// 循环体子步骤列表
        /// </summary>
        public List<ChildModel> ChildSteps { get; set; } = [];

        /// <summary>
        /// 循环描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 是否启用提前退出
        /// </summary>
        public bool EnableEarlyExit { get; set; } = false;

        /// <summary>
        /// 提前退出条件表达式
        /// 例如: {压力值} >= 6.0
        /// 当表达式结果为 true 时，立即退出循环
        /// </summary>
        public string ExitConditionExpression { get; set; } = "";

        /// <summary>
        /// 退出条件说明（可选，用于界面提示）
        /// </summary>
        public string ExitConditionDescription { get; set; } = "";
    }


    /// <summary>
    /// 循环控制类型
    /// </summary>
    public enum LoopControlType
    {
        跳出循环,    // Break - 立即跳出当前循环
        继续循环     // Continue - 跳过本次循环剩余步骤,进入下一次循环
    }

    /// <summary>
    /// 循环控制参数(用于Break和Continue)
    /// </summary>
    [Serializable]
    public class Parameter_LoopControl
    {
        /// <summary>
        /// 控制类型
        /// </summary>
        public LoopControlType ControlType { get; set; } = LoopControlType.跳出循环;

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; } = "";
    }
}
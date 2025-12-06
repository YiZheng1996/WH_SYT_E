namespace MainUI.LogicalConfiguration
{
    /// <summary>
    /// 子节点模型类，用于描述每个步骤的详细信息
    /// </summary>
    public class ChildModel
    {
        /// <summary>
        /// 步骤号
        /// </summary>
        public int StepNum { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// 步骤状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 步骤参数
        /// </summary>
        public object StepParameter { get; set; }

        /// <summary>
        /// 步骤备注 - 用户自定义说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 错误信息 - 执行失败时记录的详细错误
        /// </summary>
        public string ErrorMessage { get; set; }
    }

}

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

        /// <summary>
        /// 嵌套层级 - 表示当前步骤在工作流中的层级深度
        /// 0 = 顶层步骤
        /// 1 = 第一层子步骤(如条件判断内的步骤)
        /// 2 = 第二层子步骤(嵌套条件内的步骤)
        /// 依此类推...
        /// </summary>
        public int NestingLevel { get; set; } = 0;

        /// <summary>
        /// 父步骤ID - 用于快速查找父级关系
        /// 格式: "StepNum-StepName" 例如: "5-条件判断"
        /// 顶层步骤此属性为空
        /// </summary>
        public string ParentStepId { get; set; } = "";

        /// <summary>
        /// 步骤类型标识 - 用于快速识别特殊步骤类型
        /// 例如: "Condition"(条件判断), "Loop"(循环), "Normal"(普通步骤)
        /// </summary>
        public string StepType { get; set; } = "Normal";
    }

}

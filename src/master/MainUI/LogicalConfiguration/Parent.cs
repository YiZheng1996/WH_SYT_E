namespace MainUI.LogicalConfiguration
{
    /// <summary>
    /// 父级模型类，用于描述产品类型、型号和项点名称
    /// </summary>
    public class Parent
    {
        /// <summary>
        /// 产品类型名称，用于区分不同的产品或模型
        /// </summary>
        public string ModelTypeName { get; set; }

        /// <summary>
        /// 产品型号名称，用于具体标识某一型号的产品
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 项点名称，用于标识具体的试验或操作步骤
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 用于存储和加载步骤配置
        /// </summary>
        public List<ChildModel> ChildSteps { get; set; }
    }
}

using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.Services;

namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// PLC读取参数
    /// </summary>
    public class Parameter_ReadPLC
    {
        public List<PlcReadItem> Items { get; set; } = [];
    }

    /// <summary>
    /// PLC读取项 - 增强版，支持数据转换
    /// </summary>
    public class PlcReadItem
    {
        /// <summary>
        /// PLC模块名称
        /// </summary>
        public string PlcModuleName { get; set; } = "";

        /// <summary>
        /// PLC键值对（地址）
        /// </summary>
        public string PlcKeyName { get; set; } = "";

        /// <summary>
        /// 目标变量名称（用于序列化）
        /// </summary>
        public string TargetVarName { get; set; } = "";

        /// <summary>
        /// 是否启用数据转换
        /// </summary>
        public bool EnableConversion { get; set; } = false;

        /// <summary>
        /// 数据转换表达式
        /// 支持使用 {value} 引用读取到的原始值
        /// 例如: {value} * 0.1  (单位转换)
        ///      {value} + 100   (偏移量)
        ///      ROUND({value}, 2) (保留2位小数)
        /// </summary>
        public string ConversionExpression { get; set; } = "";

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 是否启用此项
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        // 运行时解析的变量引用（不序列化）
        [Newtonsoft.Json.JsonIgnore]
        public VarItem_Enhanced TargetVariable { get; set; }

        /// <summary>
        /// 解析目标变量（运行时调用）
        /// </summary>
        /// <param name="workflowStateService">工作流状态服务</param>
        public void ResolveTargetVariable(IWorkflowStateService workflowStateService)
        {
            if (!string.IsNullOrEmpty(TargetVarName))
            {
                var variables = workflowStateService.GetVariables<VarItem_Enhanced>();
                TargetVariable = variables.FirstOrDefault(v => v.VarName == TargetVarName);
            }
        }

        /// <summary>
        /// 获取显示用的简短描述
        /// </summary>
        public string GetDisplayText()
        {
            var text = $"{PlcModuleName}.{PlcKeyName} → @{TargetVarName}";

            if (EnableConversion && !string.IsNullOrEmpty(ConversionExpression))
            {
                text += $" [转换]";
            }

            return text;
        }
    }
}
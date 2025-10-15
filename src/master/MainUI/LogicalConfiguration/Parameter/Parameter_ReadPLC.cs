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
    /// PLC读取项
    /// </summary>
    public class PlcReadItem
    {
        /// <summary>
        /// PLC模块名称
        /// </summary>
        public string PlcModuleName { get; set; }

        /// <summary>
        /// PLC键值对
        /// </summary>
        public string PlcKeyName { get; set; }

        // 运行时解析的变量引用（不序列化）
        [Newtonsoft.Json.JsonIgnore]
        public VarItem_Enhanced TargetVariable { get; set; }

        //private IWorkflowStateService _workflowStateService;

        // 这个属性需要重新设计，建议通过工厂模式或服务来解析
        public string TargetVarName
        {
            get => TargetVariable?.VarName ?? "";
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 延迟解析，在实际使用时通过服务解析
                    _targetVarName = value;
                }
            }
        }

        private string _targetVarName;

        // 添加解析方法，由使用方调用
        public void ResolveTargetVariable(IWorkflowStateService workflowStateService)
        {
            if (!string.IsNullOrEmpty(_targetVarName))
            {
                var variables = workflowStateService.GetVariables<VarItem_Enhanced>();
                TargetVariable = variables.FirstOrDefault(v => v.VarName == _targetVarName);
            }
        }


    }
}
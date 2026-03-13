using MainUI.LogicalConfiguration.Instrument.Models;
using Newtonsoft.Json;

namespace MainUI.LogicalConfiguration.Instrument.Parameter
{
    /// <summary>
    /// 仪器通讯工具参数
    /// </summary>
    public class Parameter_InstrumentCommunication
    {
        #region 基本配置

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 选择的仪器驱动ID
        /// </summary>
        public string DriverId { get; set; } = "";

        /// <summary>
        /// 选择的仪器名称(用于显示)
        /// </summary>
        public string InstrumentName { get; set; } = "";

        /// <summary>
        /// 选择的命令ID
        /// </summary>
        public string CommandId { get; set; } = "";

        /// <summary>
        /// 选择的命令名称(用于显示)
        /// </summary>
        public string CommandName { get; set; } = "";

        /// <summary>
        /// 是否使用自定义命令
        /// </summary>
        public bool UseCustomCommand { get; set; } = false;

        /// <summary>
        /// 自定义命令内容
        /// </summary>
        public string CustomCommand { get; set; } = "";

        /// <summary>
        /// 自定义命令数据类型
        /// </summary>
        public DataType CustomCommandDataType { get; set; } = DataType.String;

        #endregion

        #region 连接参数覆盖

        /// <summary>
        /// 是否覆盖默认连接参数
        /// </summary>
        public bool OverrideConnectionParams { get; set; } = false;

        /// <summary>
        /// 覆盖的连接参数(JSON格式)
        /// </summary>
        public string OverrideParamsJson { get; set; } = "";

        #endregion

        #region 响应处理

        /// <summary>
        /// 是否等待响应
        /// </summary>
        public bool WaitForResponse { get; set; } = true;

        /// <summary>
        /// 响应存储变量名(存储原始响应)
        /// </summary>
        public string ResponseVariable { get; set; } = "";

        #endregion

        #region 超时和重试

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; } = 0;

        /// <summary>
        /// 重试间隔(毫秒)
        /// </summary>
        public int RetryInterval { get; set; } = 500;

        #endregion

        #region 错误处理

        /// <summary>
        /// 失败处理策略
        /// </summary>
        public FailureStrategy FailureStrategy { get; set; } = FailureStrategy.Abort;

        /// <summary>
        /// 跳转到的步骤号(当FailureStrategy为JumpToStep时使用)
        /// </summary>
        public int JumpToStepNumber { get; set; } = 0;

        /// <summary>
        /// 错误信息存储变量
        /// </summary>
        public string ErrorVariable { get; set; } = "";

        /// <summary>
        /// 执行状态存储变量(存储true/false)
        /// </summary>
        public string StatusVariable { get; set; } = "";

        #endregion

        #region 高级选项

        /// <summary>
        /// 发送前延时(毫秒)
        /// </summary>
        public int DelayBeforeSend { get; set; } = 0;

        /// <summary>
        /// 发送后延时(毫秒)
        /// </summary>
        public int DelayAfterSend { get; set; } = 0;

        /// <summary>
        /// 是否记录通讯日志
        /// </summary>
        public bool EnableLogging { get; set; } = true;

        /// <summary>
        /// 执行条件表达式(为空表示始终执行)
        /// </summary>
        public string ExecuteCondition { get; set; } = "";

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取覆盖的协议配置
        /// </summary>
        public T GetOverrideConfig<T>() where T : ProtocolConfigBase
        {
            if (string.IsNullOrEmpty(OverrideParamsJson))
                return null;

            try
            {
                return JsonConvert.DeserializeObject<T>(OverrideParamsJson);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 设置覆盖的协议配置
        /// </summary>
        public void SetOverrideConfig(ProtocolConfigBase config)
        {
            if (config == null) return;

            OverrideParamsJson = JsonConvert.SerializeObject(config, Formatting.Indented);
            OverrideConnectionParams = true;
        }

        /// <summary>
        /// 获取显示摘要
        /// </summary>
        public string GetSummary()
        {
            return UseCustomCommand ? 
                $"[{InstrumentName}] 自定义命令" :
                $"[{InstrumentName}] {CommandName}";
        }

        #endregion
    }
}
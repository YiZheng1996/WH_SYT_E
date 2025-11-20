namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 等待变量稳定步骤的参数类
    /// 用于监测变量或PLC地址的值变化,当变化率小于阈值且持续一定次数后认为稳定
    /// </summary>
    public class Parameter_WaitForStable
    {
        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = "等待稳定";

        /// <summary>
        /// 监测源类型
        /// </summary>
        public MonitorSourceType MonitorSourceType { get; set; } = MonitorSourceType.Variable;

        /// <summary>
        /// 监测的变量名（当MonitorSourceType为Variable时使用）
        /// </summary>
        public string MonitorVariable { get; set; } = string.Empty;

        /// <summary>
        /// PLC模块名（当MonitorSourceType为PLC时使用）
        /// </summary>
        public string PlcModuleName { get; set; } = string.Empty;

        /// <summary>
        /// PLC地址（当MonitorSourceType为PLC时使用）
        /// </summary>
        public string PlcAddress { get; set; } = string.Empty;

        /// <summary>
        /// 稳定判据:变化率阈值(单位:变量单位/秒)
        /// 当|当前值 - 上次值| / 采样间隔 <= 此值时,认为该次采样是稳定的
        /// </summary>
        public double StabilityThreshold { get; set; } = 0.1;

        /// <summary>
        /// 采样间隔(秒)
        /// 每隔多少秒采样一次
        /// </summary>
        public int SamplingInterval { get; set; } = 1;

        /// <summary>
        /// 连续稳定次数
        /// 连续多少次采样都满足稳定条件才算真正稳定
        /// 用于过滤偶然的波动
        /// </summary>
        public int StableCount { get; set; } = 3;

        /// <summary>
        /// 超时时间(秒)
        /// 0表示无限等待
        /// </summary>
        public int TimeoutSeconds { get; set; } = 60;

        /// <summary>
        /// 稳定后将当前值赋值给指定变量(可选)
        /// 为空则不赋值
        /// </summary>
        public string AssignToVariable { get; set; } = string.Empty;

        /// <summary>
        /// 超时后的动作
        /// </summary>
        public TimeoutAction OnTimeout { get; set; } = TimeoutAction.ContinueAndLog;

        /// <summary>
        /// 超时后跳转的步骤号(-1表示下一步)
        /// 仅当OnTimeout为JumpToStep时有效
        /// </summary>
        public int TimeoutJumpToStep { get; set; } = -1;
    }

    /// <summary>
    /// 监测源类型枚举
    /// </summary>
    public enum MonitorSourceType
    {
        /// <summary>
        /// 监测全局变量
        /// </summary>
        Variable = 0,

        /// <summary>
        /// 监测PLC地址
        /// </summary>
        PLC = 1
    }

    /// <summary>
    /// 超时后的动作枚举
    /// </summary>
    public enum TimeoutAction
    {
        /// <summary>
        /// 继续执行并记录日志
        /// </summary>
        ContinueAndLog = 0,

        /// <summary>
        /// 停止整个流程
        /// </summary>
        StopProcedure = 1,

        /// <summary>
        /// 跳转到指定步骤
        /// </summary>
        JumpToStep = 2
    }
}
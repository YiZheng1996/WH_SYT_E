using System.ComponentModel;

namespace MainUI.LogicalConfiguration.Parameter
{
    public class Parameter_SystemPrompt
    {
        /// <summary>
        /// 提示标题
        /// </summary>
        public string Title { get; set; } = "提示";

        /// <summary>
        /// 提示内容（支持表达式）
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 对话框类型
        /// </summary>
        [DefaultValue(DialogType.OK)]
        public DialogType DialogType { get; set; } = DialogType.OK;

        /// <summary>
        /// 提示等级（图标类型）
        /// </summary>
        [DefaultValue(MessageLevel.Info)]
        public MessageLevel MessageLevel { get; set; } = MessageLevel.Info;

        /// <summary>
        /// 存储返回值的变量名（仅在YesNo/OKCancel类型时使用）
        /// 返回值：true表示"是/确认"，false表示"否/取消"
        /// </summary>
        public string ResultVariable { get; set; }

        /// <summary>
        /// 是否等待用户响应
        /// </summary>
        [DefaultValue(true)]
        public bool WaitForResponse { get; set; } = true;

        /// <summary>
        /// 用户选择结果（运行时使用）
        /// </summary>
        [Browsable(false)]
        public DialogResult? UserResponse { get; set; }
    }

    /// <summary>
    /// 对话框类型枚举（简化版）
    /// </summary>
    public enum DialogType
    {
        /// <summary>
        /// 仅确认按钮
        /// </summary>
        [Description("确认")]
        OK = 0,

        /// <summary>
        /// 是/否 选择（返回值保存到变量）
        /// </summary>
        [Description("是/否")]
        YesNo = 1,

        /// <summary>
        /// 确认/取消 选择（返回值保存到变量）
        /// </summary>
        [Description("确认/取消")]
        OKCancel = 2
    }

    /// <summary>
    /// 提示等级枚举
    /// </summary>
    public enum MessageLevel
    {
        /// <summary>
        /// 信息提示
        /// </summary>
        [Description("信息")]
        Info = 0,

        /// <summary>
        /// 警告提示
        /// </summary>
        [Description("警告")]
        Warning = 1,

        /// <summary>
        /// 错误提示
        /// </summary>
        [Description("错误")]
        Error = 2,

        /// <summary>
        /// 询问提示
        /// </summary>
        [Description("询问")]
        Question = 3
    }
}
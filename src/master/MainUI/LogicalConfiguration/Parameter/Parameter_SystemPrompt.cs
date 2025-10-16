using System.ComponentModel;

namespace MainUI.LogicalConfiguration.Parameter
{
    public class Parameter_SystemPrompt
    {
        /// <summary>
        /// 提示标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 提示内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 对话框类型
        /// </summary>
        [DefaultValue(DialogType.Message)]
        public DialogType DialogType { get; set; } = DialogType.Message;

        /// <summary>
        /// 是否等待用户响应
        /// </summary>
        [DefaultValue(value: true)]
        public bool WaitForResponse { get; set; } = true;

        /// <summary>
        /// 用户选择结果（用于条件判断）
        /// </summary>
        public DialogResult? UserResponse { get; set; }
    }

    /// <summary>
    /// 对话框类型枚举
    /// </summary>
    public enum DialogType
    {
        /// <summary>
        /// 仅消息提示
        /// </summary>
        Message,

        /// <summary>
        /// 是/否 确认
        /// </summary>
        YesNo,

        /// <summary>
        /// 是/否/取消 确认
        /// </summary>
        YesNoCancel,

        /// <summary>
        /// 确定/取消 确认
        /// </summary>
        OKCancel,

        /// <summary>
        /// 仅确定
        /// </summary>
        OK
    }

    public enum MessageType
    {
        Info,
        Warning,
        Error,
        Question
    }
}

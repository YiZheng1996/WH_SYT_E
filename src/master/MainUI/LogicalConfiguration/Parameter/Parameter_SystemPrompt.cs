using System.ComponentModel;

namespace MainUI.LogicalConfiguration.Parameter
{
    public class Parameter_SystemPrompt
    {
        /// <summary>
        /// 提示标题
        /// </summary>
        public string Title { get; set; }           // 提示标题

        /// <summary>
        /// 提示内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 提示类型
        /// </summary>
        //public MessageType MessageType { get; set; } // 提示类型

        /// <summary>
        /// 是否等待用户响应
        /// </summary>
        [DefaultValue(value: true)]
        public bool WaitForResponse { get; set; }
    }

    public enum MessageType
    {
        Info,
        Warning,
        Error,
        Question
    }
}

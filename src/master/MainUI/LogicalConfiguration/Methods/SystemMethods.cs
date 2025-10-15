using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 系统工具方法集合 - 使用新的统一错误处理
    /// </summary>
    public class SystemMethods : DSLMethodBase
    {
        public override string Category => "系统工具";
        public override string Description => "提供延时、提示等系统级工具方法";

        /// <summary>
        /// 延时方法
        /// </summary>
        public async Task<bool> DelayTime(Parameter_DelayTime param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(param.T));
                return true;
            }, false); // 默认返回false
        }

        /// <summary>
        /// 系统提示方法
        /// </summary>
        public async Task<bool> SystemPrompt(Parameter_SystemPrompt param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                // 解析提示内容中的变量引用
                string resolvedMessage = await ResolveVariablesInText(param.Message);

                // 显示提示信息
                var result = MessageHelper.MessageYes(resolvedMessage);

                // 如果需要等待用户响应
                if (param.WaitForResponse)
                {
                    return result == DialogResult.OK;
                }

                return true;
            }, false);
        }

        /// <summary>
        /// 解析文本中的变量引用
        /// </summary>
        private async Task<string> ResolveVariablesInText(string text)
        {
            // 实现变量解析逻辑
            await Task.CompletedTask;
            return text; // 简化实现
        }
    }
}

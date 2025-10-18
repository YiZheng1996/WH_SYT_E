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

        public Task<bool> SystemPrompt(Parameter_SystemPrompt param)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(param.Message))
                {
                    //_logger.LogWarning("提示消息为空");
                    return Task.FromResult(false);
                }

                DialogResult result = DialogResult.None;

                // 根据对话框类型显示不同的消息框
                switch (param.DialogType)
                {
                    case DialogType.Message:
                        MessageBox.Show(param.Message, param.Title ?? "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        result = DialogResult.OK;
                        break;

                    case DialogType.YesNo:
                        result = MessageBox.Show(param.Message, param.Title ?? "确认",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        break;

                    case DialogType.YesNoCancel:
                        result = MessageBox.Show(param.Message, param.Title ?? "确认",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        break;

                    case DialogType.OKCancel:
                        result = MessageBox.Show(param.Message, param.Title ?? "确认",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        break;

                    case DialogType.OK:
                        MessageBox.Show(param.Message, param.Title ?? "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        result = DialogResult.OK;
                        break;
                }

                // 保存用户响应结果
                param.UserResponse = result;

                // 记录日志
                //_logger.LogInformation($"系统提示已显示，类型: {param.DialogType}, 用户响应: {result}");

                // 根据等待响应设置返回值
                if (param.WaitForResponse)
                {
                    // 对于需要确认的对话框，根据用户选择返回结果
                    return Task.FromResult(result == DialogResult.OK || result == DialogResult.Yes);
                }

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                //_logger.LogError(ex, "显示系统提示失败");
                return Task.FromResult(false);
            }
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

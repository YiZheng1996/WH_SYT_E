using MainUI.LogicalConfiguration.LogicalManager;
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
        /// 延时等待 - 支持取消
        /// </summary>
        public async Task<bool> DelayTime(Parameter_DelayTime param, CancellationToken cancellationToken = default)
        {
            try
            {
                NlogHelper.Default.Info($"开始延时: {param.T} 秒");

                int delayMilliseconds = (int)(param.T /** 1000*/);

                // 使用支持取消的延时
                await Task.Delay(delayMilliseconds, cancellationToken);

                NlogHelper.Default.Info("延时完成");
                return true;
            }
            catch (OperationCanceledException)
            {
                NlogHelper.Default.Info("延时被取消");
                throw; // 向上传播取消异常
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("延时执行失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 系统提示方法 - 支持新的对话框类型和提示等级
        /// </summary>
        public Task<bool> SystemPrompt(Parameter_SystemPrompt param, GlobalVariableManager variableManager = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(param.Message))
                {
                    NlogHelper.Default.Warn("提示消息为空");
                    return Task.FromResult(false);
                }

                // 获取对应的 MessageBoxIcon
                var icon = param.MessageLevel switch
                {
                    MessageLevel.Info => MessageBoxIcon.Information,
                    MessageLevel.Warning => MessageBoxIcon.Warning,
                    MessageLevel.Error => MessageBoxIcon.Error,
                    MessageLevel.Question => MessageBoxIcon.Question,
                    _ => MessageBoxIcon.Information
                };

                DialogResult result = DialogResult.None;
                string title = param.Title ?? "提示";

                // 根据对话框类型显示不同的消息框
                switch (param.DialogType)
                {
                    case DialogType.OK:
                        // 仅确认按钮
                        MessageBox.Show(param.Message, title, MessageBoxButtons.OK, icon);
                        result = DialogResult.OK;
                        break;

                    case DialogType.YesNo:
                        // 是/否 选择
                        result = MessageBox.Show(param.Message, title, MessageBoxButtons.YesNo, icon);

                        // 保存结果到变量
                        if (variableManager != null && !string.IsNullOrEmpty(param.ResultVariable))
                        {
                            bool userChoice = result == DialogResult.Yes;
                            variableManager.UpdateVariableValue(param.ResultVariable, userChoice, "");
                            NlogHelper.Default.Info($"用户选择已保存到变量 [{param.ResultVariable}]: {userChoice}");
                        }
                        break;

                    case DialogType.OKCancel:
                        // 确认/取消 选择
                        result = MessageBox.Show(param.Message, title, MessageBoxButtons.OKCancel, icon);

                        // 保存结果到变量
                        if (variableManager != null && !string.IsNullOrEmpty(param.ResultVariable))
                        {
                            bool userChoice = result == DialogResult.OK;
                            variableManager.UpdateVariableValue(param.ResultVariable, userChoice, "");
                            NlogHelper.Default.Info($"用户选择已保存到变量 [{param.ResultVariable}]: {userChoice}");
                        }
                        break;
                }

                // 保存用户响应结果
                param.UserResponse = result;

                NlogHelper.Default.Info($"系统提示显示完成: Type={param.DialogType}, Level={param.MessageLevel}, Result={result}");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("系统提示执行失败", ex);
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

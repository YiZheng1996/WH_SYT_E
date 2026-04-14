using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.Helpers;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 系统工具方法集合 - 使用新的统一错误处理
    /// </summary>
    public class SystemMethods(ExpressionEngine expressionEngine,
        GlobalVariableManager variableManager) : DSLMethodBase
    {
        private readonly ExpressionEngine _expressionEngine = expressionEngine;
        private readonly GlobalVariableManager _variableManager = variableManager;

        public override string Category => "系统工具";
        public override string Description => "提供延时、提示等系统级工具方法";

        /// <summary>
        /// 延时等待 - 支持变量表达式和时间单位
        /// </summary>
        public async Task<bool> DelayTime(Parameter_DelayTime param, CancellationToken cancellationToken = default)
        {
            try
            {
                if (param == null)
                {
                    NlogHelper.Default.Error("延时参数为空");
                    return false;
                }

                double delayValue;

                // 1. 解析延时值
                if (string.IsNullOrWhiteSpace(param.DelayValue))
                {
                    // 兼容旧数据：DelayValue 为空时直接用 _legacyT（已是毫秒）
                    NlogHelper.Default.Info($"使用兼容模式延时: {param.T} 毫秒");
                    await Task.Delay((int)Math.Min(param.T, int.MaxValue), cancellationToken);
                    return true;
                }

                if (param.ContainsVariables())
                {
                    // 变量表达式：交给表达式引擎求值
                    NlogHelper.Default.Info($"解析延时表达式: {param.DelayValue}");
                    var evalResult = await _expressionEngine.EvaluateExpressionAsync(param.DelayValue);

                    if (!evalResult.Success)
                    {
                        NlogHelper.Default.Error($"延时表达式求值失败: {evalResult.ErrorMessage}");
                        return false;
                    }

                    if (evalResult.Result is double d)
                        delayValue = d;
                    else if (evalResult.Result is int i)
                        delayValue = i;
                    else if (double.TryParse(evalResult.Result?.ToString(), out double p))
                        delayValue = p;
                    else
                    {
                        NlogHelper.Default.Error($"延时表达式结果类型无法转换为数值: {evalResult.Result?.GetType().Name}");
                        return false;
                    }

                    NlogHelper.Default.Info($"表达式 {param.DelayValue} 解析结果: {delayValue}");
                }
                else
                {
                    // 固定数值：直接解析用户原始输入（如 "1"，单位由 param.Unit 决定）
                    if (!double.TryParse(param.DelayValue, out delayValue))
                    {
                        NlogHelper.Default.Error($"延时值格式错误: {param.DelayValue}");
                        return false;
                    }
                }

                // 2. 按单位换算为毫秒（只做一次）
                double msDouble = param.ConvertToMilliseconds(delayValue);
                string unitDisplay = Parameter_DelayTime.GetUnitDisplayName(param.Unit);
                NlogHelper.Default.Info($"延时时间: {delayValue} {unitDisplay} = {msDouble} 毫秒");

                // 3. 验证
                if (msDouble <= 0)
                {
                    NlogHelper.Default.Error($"延时值必须大于零: {msDouble}");
                    return false;
                }

                // 4. 执行延时（Task.Delay 接受 int，限制最大 24 小时）
                const double MaxDelayMs = 24.0 * 60 * 60 * 1000; // 86,400,000 ms
                int delayMilliseconds = (int)Math.Min(msDouble, MaxDelayMs);

                NlogHelper.Default.Info($"开始延时 {delayMilliseconds} 毫秒");
                await Task.Delay(delayMilliseconds, cancellationToken);
                NlogHelper.Default.Info("延时完成");

                return true;
            }
            catch (OperationCanceledException)
            {
                NlogHelper.Default.Info("延时被取消");
                throw;
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
        public async Task<bool> SystemPrompt(Parameter_SystemPrompt param, GlobalVariableManager variableManager = null)
        {
            try
            {
                if (param == null)
                {
                    NlogHelper.Default.Error("系统提示参数为空");
                    return false;
                }

                // 解析表达式中的变量引用
                var message = await ResolveVariablesInText(param.Message);
                var title = string.IsNullOrWhiteSpace(param.Title) ? "提示" : param.Title;

                // 根据提示等级确定图标
                var icon = param.MessageLevel switch
                {
                    MessageLevel.Info => MessageBoxIcon.Information,
                    MessageLevel.Warning => MessageBoxIcon.Warning,
                    MessageLevel.Error => MessageBoxIcon.Error,
                    MessageLevel.Question => MessageBoxIcon.Question,
                    _ => MessageBoxIcon.Information
                };

                DialogResult result = DialogResult.No;

                // 根据对话框类型显示不同的消息框
                switch (param.DialogType)
                {
                    case DialogType.OK:
                        // 仅确认按钮
                        MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
                        result = DialogResult.OK;
                        break;

                    case DialogType.YesNo:
                        // 是/否 选择
                        result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, icon);

                        // 保存结果到变量前，先规范化变量名
                        if (variableManager != null && !string.IsNullOrEmpty(param.ResultVariable))
                        {
                            // 使用 VariableNameHelper 规范化变量名
                            var normalizedVarName = VariableNameHelper.NormalizeVariableName(param.ResultVariable);

                            if (normalizedVarName != null)
                            {
                                bool userChoice = result == DialogResult.Yes;
                                variableManager.UpdateVariableValue(normalizedVarName, userChoice, "");

                                // 日志中显示规范化信息
                                if (param.ResultVariable.Trim() != normalizedVarName)
                                {
                                    NlogHelper.Default.Info(
                                        $"变量名已规范化: '{param.ResultVariable}' -> '{normalizedVarName}'");
                                }

                                NlogHelper.Default.Info(
                                    $"用户选择已保存到变量 [{normalizedVarName}]: {userChoice}");
                            }
                            else
                            {
                                NlogHelper.Default.Error(
                                    $"无效的变量名格式: {param.ResultVariable}");
                                return false;
                            }
                        }
                        break;

                    case DialogType.OKCancel:
                        // 确认/取消 选择
                        result = MessageBox.Show(message, title, MessageBoxButtons.OKCancel, icon);

                        // 保存结果到变量前，先规范化变量名
                        if (variableManager != null && !string.IsNullOrEmpty(param.ResultVariable))
                        {
                            var normalizedVarName = VariableNameHelper.NormalizeVariableName(param.ResultVariable);

                            if (normalizedVarName != null)
                            {
                                bool userChoice = result == DialogResult.OK;
                                variableManager.UpdateVariableValue(normalizedVarName, userChoice, "");

                                if (param.ResultVariable.Trim() != normalizedVarName)
                                {
                                    NlogHelper.Default.Info(
                                        $"变量名已规范化: '{param.ResultVariable}' -> '{normalizedVarName}'");
                                }

                                NlogHelper.Default.Info(
                                    $"用户选择已保存到变量 [{normalizedVarName}]: {userChoice}");
                            }
                            else
                            {
                                NlogHelper.Default.Error(
                                    $"无效的变量名格式: {param.ResultVariable}");
                                return false;
                            }
                        }
                        break;
                }

                // 保存用户响应结果
                param.UserResponse = result;

                NlogHelper.Default.Info(
                    $"系统提示显示完成: Type={param.DialogType}, Level={param.MessageLevel}, Result={result}");
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("系统提示执行失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 解析文本中的变量引用，将 {变量名} 替换为实际值
        /// </summary>
        private async Task<string> ResolveVariablesInText(string text)
        {
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(text)) return text ?? string.Empty;

            return PromptTextRenderer.Render(text, _variableManager);
        }
    }
}

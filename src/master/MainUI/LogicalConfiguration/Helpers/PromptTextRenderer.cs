using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace MainUI.LogicalConfiguration.Helpers
{
    /// <summary>
    /// 提示文本渲染器
    /// 将模板文本中的 {变量名} 替换为实际变量值，用于消息通知、用户输入、实时监控等提示文本
    /// </summary>
    public static class PromptTextRenderer
    {
        // 与 ExpressionEngine 保持一致的变量引用模式
        private static readonly Regex VariablePattern =
            new(@"\{([^}]+)\}", RegexOptions.Compiled);

        /// <summary>
        /// 渲染提示文本：将 {变量名} 替换为变量当前值
        /// </summary>
        /// <param name="template">原始模板文本，如 "请将压力调整到{范围上限}kPa"</param>
        /// <param name="variableManager">全局变量管理器</param>
        /// <param name="logger">可选日志</param>
        /// <returns>替换后的文本；变量不存在时保留原占位符</returns>
        public static string Render(
            string template,
            GlobalVariableManager variableManager,
            Microsoft.Extensions.Logging.ILogger logger = null)
        {
            if (string.IsNullOrEmpty(template) || variableManager == null)
                return template ?? string.Empty;

            return VariablePattern.Replace(template, match =>
            {
                var varName = match.Groups[1].Value.Trim();
                var variable = variableManager.TryFindVariableByName(varName);

                if (variable == null)
                {
                    logger?.LogWarning("提示文本中的变量未定义，保留原样: {VarName}", varName);
                    return match.Value; // 保留 {变量名} 原样，避免误导
                }

                return variable.VarValue?.ToString() ?? string.Empty;
            });
        }
    }
}
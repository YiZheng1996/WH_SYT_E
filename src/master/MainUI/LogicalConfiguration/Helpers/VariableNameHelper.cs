using System.Text.RegularExpressions;

namespace MainUI.LogicalConfiguration.Helpers
{
    /// <summary>
    /// 变量名处理辅助类
    /// 提供统一的变量名规范化、验证和格式转换功能
    /// 确保系统各个模块对变量名的处理保持一致
    /// </summary>
    public static class VariableNameHelper
    {
        // 匹配带花括号的变量名: {变量名} 或 { 变量名 }
        private static readonly Regex BracedVariablePattern =
            new Regex(@"^\s*\{([\w\u4e00-\u9fa5]+)\}\s*$", RegexOptions.Compiled);

        // 匹配有效的变量名格式（字母、数字、下划线、中文，不能以数字开头）
        private static readonly Regex ValidVariableNamePattern =
            new Regex(@"^[a-zA-Z_\u4e00-\u9fa5][\w\u4e00-\u9fa5]*$", RegexOptions.Compiled);

        /// <summary>
        /// 规范化变量名 - 核心方法
        /// 将任何格式的变量名转换为标准格式（不带花括号）
        /// </summary>
        /// <param name="variableName">原始变量名，可能带或不带花括号</param>
        /// <returns>规范化后的变量名（不带花括号），如果输入无效则返回null</returns>
        /// <example>
        /// NormalizeVariableName("{是否成功}") -> "是否成功"
        /// NormalizeVariableName("是否成功") -> "是否成功"
        /// NormalizeVariableName("{ 温度值 }") -> "温度值"
        /// NormalizeVariableName("123abc") -> null (无效变量名)
        /// </example>
        public static string NormalizeVariableName(string variableName)
        {
            if (string.IsNullOrWhiteSpace(variableName))
                return null;

            var trimmed = variableName.Trim();

            // 检查是否是带花括号格式
            var match = BracedVariablePattern.Match(trimmed);
            if (match.Success)
            {
                // 提取花括号内的变量名
                var cleanName = match.Groups[1].Value;
                return IsValidVariableName(cleanName) ? cleanName : null;
            }

            // 不带花括号，直接验证
            return IsValidVariableName(trimmed) ? trimmed : null;
        }

        /// <summary>
        /// 验证变量名是否有效
        /// </summary>
        /// <param name="variableName">要验证的变量名</param>
        /// <returns>true表示有效，false表示无效</returns>
        public static bool IsValidVariableName(string variableName)
        {
            if (string.IsNullOrWhiteSpace(variableName))
                return false;

            return ValidVariableNamePattern.IsMatch(variableName);
        }

        /// <summary>
        /// 将变量名转换为表达式格式（添加花括号）
        /// 用于在表达式中引用变量
        /// </summary>
        /// <param name="variableName">变量名（不带花括号）</param>
        /// <returns>表达式格式的变量引用 {变量名}</returns>
        /// <example>
        /// ToExpressionFormat("是否成功") -> "{是否成功}"
        /// </example>
        public static string ToExpressionFormat(string variableName)
        {
            var normalized = NormalizeVariableName(variableName);
            return normalized != null ? $"{{{normalized}}}" : null;
        }

        /// <summary>
        /// 批量规范化变量名列表
        /// </summary>
        /// <param name="variableNames">变量名列表</param>
        /// <returns>规范化后的变量名列表，跳过无效的变量名</returns>
        public static List<string> NormalizeVariableNames(IEnumerable<string> variableNames)
        {
            if (variableNames == null)
                return new List<string>();

            return variableNames
                .Select(NormalizeVariableName)
                .Where(name => name != null)
                .ToList();
        }

        /// <summary>
        /// 尝试从表达式中提取所有变量名
        /// 用于表达式解析和验证
        /// </summary>
        /// <param name="expression">表达式字符串</param>
        /// <returns>表达式中引用的所有变量名列表</returns>
        /// <example>
        /// ExtractVariableNames("{Var1} + {Var2} * 2") -> ["Var1", "Var2"]
        /// </example>
        public static List<string> ExtractVariableNames(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return new List<string>();

            var matches = Regex.Matches(expression, @"\{([\w\u4e00-\u9fa5]+)\}");
            return matches
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// 检查字符串是否是简单的变量引用（单个变量，带或不带花括号）
        /// </summary>
        /// <param name="text">要检查的文本</param>
        /// <returns>如果是简单变量引用返回true</returns>
        public static bool IsSimpleVariableReference(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            var normalized = NormalizeVariableName(text);
            if (normalized == null)
                return false;

            // 检查规范化后的名称是否就是原文本（不带花括号的情况）
            // 或者原文本是否是 {规范化名称} 格式
            var trimmed = text.Trim();
            return trimmed == normalized || trimmed == $"{{{normalized}}}";
        }

        /// <summary>
        /// 获取变量名的友好显示格式
        /// 用于错误消息和日志输出
        /// </summary>
        /// <param name="variableName">变量名</param>
        /// <param name="includeOriginal">是否包含原始输入</param>
        /// <returns>友好的显示文本</returns>
        public static string GetDisplayName(string variableName, bool includeOriginal = false)
        {
            if (string.IsNullOrWhiteSpace(variableName))
                return "(空)";

            var normalized = NormalizeVariableName(variableName);

            if (normalized == null)
                return $"(无效变量名: {variableName})";

            if (includeOriginal && variableName.Trim() != normalized)
                return $"{normalized} (原输入: {variableName.Trim()})";

            return normalized;
        }

        /// <summary>
        /// 比较两个变量名是否指向同一个变量
        /// 自动处理花括号格式差异
        /// </summary>
        /// <param name="name1">第一个变量名</param>
        /// <param name="name2">第二个变量名</param>
        /// <returns>如果指向同一变量返回true</returns>
        public static bool AreEqual(string name1, string name2)
        {
            var normalized1 = NormalizeVariableName(name1);
            var normalized2 = NormalizeVariableName(name2);

            if (normalized1 == null || normalized2 == null)
                return false;

            return string.Equals(normalized1, normalized2, StringComparison.Ordinal);
        }
    }
}
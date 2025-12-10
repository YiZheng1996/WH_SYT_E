using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.RegularExpressions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 变量解析器 - 负责解析和替换表达式中的变量引用
    /// </summary>
    internal class VariableResolver(
        GlobalVariableManager variableManager,
        IPLCManager plcManager = null,
        ILogger logger = null)
    {
        private readonly GlobalVariableManager _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
        private readonly IPLCManager _plcManager = plcManager;
        private readonly ILogger _logger = logger;

        // 正则表达式模式
        private readonly Regex _variablePattern = new(@"\{([^}]+)\}", RegexOptions.Compiled);

        /// <summary>
        /// 同步预处理表达式 - 替换变量引用
        /// </summary>
        public string PreprocessExpression(string expression)
        {
            // 先处理 DateTime.Now 表达式
            expression = ProcessDateTimeNow(expression);

            var result = expression;
            var matches = _variablePattern.Matches(expression);

            foreach (Match match in matches)
            {
                var varName = match.Groups[1].Value;

                // 检查是否是 PLC 地址格式
                if (varName.Contains('.'))
                {
                    var plcValue = ReplacePLCReference(varName);
                    result = result.Replace(match.Value, plcValue);
                }
                else
                {
                    // 普通变量
                    var variable = _variableManager.TryFindVariableByName(varName);
                    if (variable != null)
                    {
                        var formattedValue = FormatValueForExpression(variable.VarValue);
                        result = result.Replace(match.Value, formattedValue);
                    }
                    else
                    {
                        _logger?.LogWarning($"变量 '{varName}' 不存在");
                        throw new InvalidOperationException($"变量 '{varName}' 不存在");
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 异步预处理表达式 - 支持PLC异步读取
        /// </summary>
        public async Task<string> PreprocessExpressionAsync(string expression)
        {
            var result = expression;
            var matches = _variablePattern.Matches(expression);

            foreach (Match match in matches)
            {
                var varName = match.Groups[1].Value;

                // 检查是否是 PLC 地址格式
                if (varName.Contains('.'))
                {
                    var plcValue = await ReplacePLCReferenceAsync(varName);
                    result = result.Replace(match.Value, plcValue);
                }
                else
                {
                    // 普通变量
                    var variable = _variableManager.TryFindVariableByName(varName);
                    if (variable != null)
                    {
                        var formattedValue = FormatValueForExpression(variable.VarValue);
                        result = result.Replace(match.Value, formattedValue);
                    }
                    else
                    {
                        _logger?.LogWarning($"变量 '{varName}' 不存在");
                        throw new InvalidOperationException($"变量 '{varName}' 不存在");
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取表达式中引用的所有变量名
        /// </summary>
        public List<string> GetReferencedVariables(string expression)
        {
            var matches = _variablePattern.Matches(expression);
            return [.. matches.Cast<Match>()
                         .Select(m => m.Groups[1].Value)
                         .Distinct()];
        }

        #region PLC 处理

        /// <summary>
        /// 同步替换PLC引用
        /// </summary>
        private string ReplacePLCReference(string plcReference)
        {
            try
            {
                var parts = plcReference.Split('.');
                if (parts.Length < 3)
                {
                    throw new InvalidOperationException($"PLC引用格式错误: {plcReference}");
                }

                string moduleName = parts[1];
                string address = parts[2];

                if (_plcManager == null)
                {
                    throw new InvalidOperationException("PLCManager 未初始化");
                }

                var plcValue = _plcManager.ReadPLCValueAsync(moduleName, address) ?? 
                    throw new InvalidOperationException($"无法读取PLC: {moduleName}.{address}");

                _logger?.LogDebug($"PLC读取成功: {moduleName}.{address} = {plcValue}");
                return FormatValueForExpression(plcValue);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"处理PLC引用失败: {plcReference}");
                throw;
            }
        }

        /// <summary>
        /// 异步替换PLC引用
        /// </summary>
        private async Task<string> ReplacePLCReferenceAsync(string plcReference)
        {
            try
            {
                var parts = plcReference.Split('.');
                if (parts.Length < 3)
                {
                    throw new InvalidOperationException($"PLC引用格式错误: {plcReference}");
                }

                string moduleName = parts[1];
                string address = parts[2];

                if (_plcManager == null)
                {
                    throw new InvalidOperationException("PLCManager 未初始化");
                }

                var plcValue = await _plcManager.ReadPLCValueAsync(moduleName, address);

                if (plcValue == null)
                {
                    throw new InvalidOperationException($"无法读取PLC: {moduleName}.{address}");
                }

                _logger?.LogDebug($"PLC读取成功: {moduleName}.{address} = {plcValue}");
                return FormatValueForExpression(plcValue);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"处理PLC引用失败: {plcReference}");
                throw;
            }
        }

        #endregion

        #region 值格式化

        /// <summary>
        /// 将值格式化为适合表达式的字符串
        /// </summary>
        private string FormatValueForExpression(object value)
        {
            if (value == null)
                return "null";

            // 布尔值
            if (value is bool b)
                return b ? "true" : "false";

            // 数字类型 - 统一使用 InvariantCulture
            if (value is int || value is long || value is short || value is byte ||
                value is uint || value is ulong || value is ushort || value is sbyte)
            {
                return Convert.ToInt64(value).ToString(CultureInfo.InvariantCulture);
            }

            if (value is double || value is float || value is decimal)
            {
                return Convert.ToDouble(value).ToString(CultureInfo.InvariantCulture);
            }

            // 日期时间
            if (value is DateTime dt)
            {
                return $"\"{dt:yyyy-MM-dd HH:mm:ss}\"";
            }

            // 字符串 - 检查是否为数字字符串
            if (value is string str)
            {
                // 如果是数字字符串，不加引号
                if (double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out double numValue))
                {
                    return numValue.ToString(CultureInfo.InvariantCulture);
                }

                // 否则加引号
                return $"\"{str.Replace("\"", "\\\"")}\"";
            }

            // 其他类型 - 尝试转换为数字
            if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result.ToString(CultureInfo.InvariantCulture);
            }

            // 最后才加引号
            return $"\"{value}\"";
        }


        /// <summary>
        /// 处理 DateTime.Now 表达式
        /// </summary>
        private string ProcessDateTimeNow(string expression)
        {
            // 匹配 DateTime.Now.ToString("format")
            var pattern = @"DateTime\.Now\.ToString\(""([^""]+)""\)";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            expression = regex.Replace(expression, match =>
            {
                var format = match.Groups[1].Value;
                var formattedDate = DateTime.Now.ToString(format);
                return $"\"{formattedDate}\"";
            });

            // 匹配单独的 DateTime.Now
            expression = Regex.Replace(expression, @"DateTime\.Now\b",
                $"\"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\"",
                RegexOptions.IgnoreCase);

            return expression;
        }

        #endregion
    }
}
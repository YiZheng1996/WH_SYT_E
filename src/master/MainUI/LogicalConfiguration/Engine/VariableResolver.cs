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
            // 先处理 DateTime.Now 表达式（原代码缺少这行）
            expression = ProcessDateTimeNow(expression);

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
        /// 支持：
        /// 1. DateTime.Now.ToString("format")
        /// 2. DateTime.Today.ToString("format")  
        /// 3. DateTime.Now
        /// 4. DateTime.Today
        /// </summary>
        private string ProcessDateTimeNow(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return expression;

            _logger?.LogDebug($"[ProcessDateTimeNow] 输入: {expression}");

            // 1. 匹配 DateTime.Now.ToString("format") - 带引号的格式字符串
            var pattern1 = @"DateTime\.Now\.ToString\(""([^""]+)""\)";
            var regex1 = new Regex(pattern1, RegexOptions.IgnoreCase);

            expression = regex1.Replace(expression, match =>
            {
                var format = match.Groups[1].Value;
                var formattedDate = DateTime.Now.ToString(format);
                _logger?.LogDebug($"[ProcessDateTimeNow] 匹配DateTime.Now.ToString: 格式={format}, 结果={formattedDate}");
                return $"\"{formattedDate}\"";
            });

            // 2. 匹配 DateTime.Today.ToString("format")
            var pattern2 = @"DateTime\.Today\.ToString\(""([^""]+)""\)";
            var regex2 = new Regex(pattern2, RegexOptions.IgnoreCase);

            expression = regex2.Replace(expression, match =>
            {
                var format = match.Groups[1].Value;
                var formattedDate = DateTime.Today.ToString(format);
                _logger?.LogDebug($"[ProcessDateTimeNow] 匹配DateTime.Today.ToString: 格式={format}, 结果={formattedDate}");
                return $"\"{formattedDate}\"";
            });

            // 3. 匹配 DateTime.Now.ToString('format') - 单引号版本（兼容性）
            var pattern3 = @"DateTime\.Now\.ToString\('([^']+)'\)";
            var regex3 = new Regex(pattern3, RegexOptions.IgnoreCase);

            expression = regex3.Replace(expression, match =>
            {
                var format = match.Groups[1].Value;
                var formattedDate = DateTime.Now.ToString(format);
                _logger?.LogDebug($"[ProcessDateTimeNow] 匹配DateTime.Now.ToString(单引号): 格式={format}, 结果={formattedDate}");
                return $"\"{formattedDate}\"";
            });

            // 4. 匹配单独的 DateTime.Now（不带参数）
            expression = Regex.Replace(expression, @"\bDateTime\.Now\b(?!\s*\.)",
                match =>
                {
                    var formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    _logger?.LogDebug($"[ProcessDateTimeNow] 匹配DateTime.Now(无参数): 结果={formattedDate}");
                    return $"\"{formattedDate}\"";
                },
                RegexOptions.IgnoreCase);

            // 5. 匹配单独的 DateTime.Today
            expression = Regex.Replace(expression, @"\bDateTime\.Today\b(?!\s*\.)",
                match =>
                {
                    var formattedDate = DateTime.Today.ToString("yyyy-MM-dd");
                    _logger?.LogDebug($"[ProcessDateTimeNow] 匹配DateTime.Today(无参数): 结果={formattedDate}");
                    return $"\"{formattedDate}\"";
                },
                RegexOptions.IgnoreCase);

            _logger?.LogDebug($"[ProcessDateTimeNow] 输出: {expression}");
            return expression;
        }


        #endregion
    }
}
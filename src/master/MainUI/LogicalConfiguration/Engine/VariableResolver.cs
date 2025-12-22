using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 变量解析器 - 优化版 (修复PLC引用解析问题)
    /// 负责解析和替换表达式中的变量引用,使用共享工具消除重复代码
    /// 正确处理 PLC.模块名.地址 格式的PLC引用
    /// </summary>
    internal class VariableResolver(
        GlobalVariableManager variableManager,
        IPLCManager plcManager = null,
        ILogger logger = null)
    {
        private readonly GlobalVariableManager _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));

        #region 公共方法 - 表达式预处理

        /// <summary>
        /// 同步预处理表达式 - 替换变量引用
        /// </summary>
        public string PreprocessExpression(string expression)
        {
            // 先处理 DateTime.Now 表达式 - 使用共享工具
            expression = ExpressionUtils.ProcessDateTimeNow(expression);

            // 替换变量引用
            return ReplaceVariableReferences(expression, false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 异步预处理表达式 - 支持PLC异步读取
        /// </summary>
        public async Task<string> PreprocessExpressionAsync(string expression)
        {
            // 先处理 DateTime.Now 表达式 - 使用共享工具
            expression = ExpressionUtils.ProcessDateTimeNow(expression);

            // 替换变量引用
            return await ReplaceVariableReferences(expression, true);
        }

        /// <summary>
        /// 获取表达式中引用的所有变量名 - 使用共享工具
        /// </summary>
        public List<string> GetReferencedVariables(string expression)
        {
            return ExpressionUtils.GetReferencedVariables(expression);
        }

        #endregion

        #region 私有方法 - 变量替换

        /// <summary>
        /// 替换变量引用 - 统一的替换逻辑,支持同步和异步
        /// </summary>
        private async Task<string> ReplaceVariableReferences(string expression, bool async)
        {
            var result = expression;
            var matches = ExpressionConstants.VariablePattern.Matches(expression);

            foreach (Match match in matches)
            {
                var varName = match.Groups[1].Value;
                string replacement;

                // 使用共享工具检查是否是 PLC 引用格式 - 修复点
                if (ExpressionUtils.IsPLCReference(varName))
                {
                    replacement = async
                        ? await ReplacePLCReferenceAsync(varName)
                        : await ReplacePLCReference(varName);
                }
                else
                {
                    replacement = ReplaceVariableReference(varName);
                }

                result = result.Replace(match.Value, replacement);
            }

            return result;
        }

        /// <summary>
        /// 替换普通变量引用
        /// </summary>
        private string ReplaceVariableReference(string varName)
        {
            var variable = _variableManager.TryFindVariableByName(varName);

            if (variable == null)
            {
                logger?.LogWarning("变量 '{VarName}' 不存在", varName);
                throw new InvalidOperationException($"变量 '{varName}' 不存在");
            }

            // 使用共享工具格式化值
            return ExpressionUtils.FormatValueForExpression(variable.VarValue);
        }

        /// <summary>
        /// 替换PLC引用(同步版本) - 使用ParsePLCReference正确解析
        /// </summary>
        private async Task<string> ReplacePLCReference(string plcAddress)
        {
            if (plcManager == null)
            {
                logger?.LogWarning("PLCManager未初始化,无法读取PLC: {Address}", plcAddress);
                throw new InvalidOperationException($"PLCManager未初始化,无法读取PLC: {plcAddress}");
            }

            try
            {
                // 使用共享工具正确解析PLC引用 - 关键修复点
                var (moduleName, address) = ExpressionUtils.ParsePLCReference(plcAddress);

                if (string.IsNullOrEmpty(moduleName) || string.IsNullOrEmpty(address))
                {
                    throw new InvalidOperationException($"无效的PLC地址格式: {plcAddress}");
                }

                logger?.LogDebug("解析PLC引用: '{RawAddress}' -> 模块={Module}, 地址={Address}",
                    plcAddress, moduleName, address);

                var value = await plcManager.ReadPLCValueAsync(moduleName, address);
                logger?.LogDebug("读取PLC值: {Module}.{Address} = {Value}", moduleName, address, value);

                // 使用共享工具格式化值
                return ExpressionUtils.FormatValueForExpression(value);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "读取PLC失败: {Address}", plcAddress);
                throw new InvalidOperationException($"读取PLC失败: {plcAddress}", ex);
            }
        }

        /// <summary>
        /// 替换PLC引用(异步版本) - 使用ParsePLCReference正确解析
        /// </summary>
        private async Task<string> ReplacePLCReferenceAsync(string plcAddress)
        {
            if (plcManager == null)
            {
                logger?.LogWarning("PLCManager未初始化,无法读取PLC: {Address}", plcAddress);
                throw new InvalidOperationException($"PLCManager未初始化,无法读取PLC: {plcAddress}");
            }

            try
            {
                // 使用共享工具正确解析PLC引用 - 关键修复点
                var (moduleName, address) = ExpressionUtils.ParsePLCReference(plcAddress);

                if (string.IsNullOrEmpty(moduleName) || string.IsNullOrEmpty(address))
                {
                    throw new InvalidOperationException($"无效的PLC地址格式: {plcAddress}");
                }

                logger?.LogDebug("解析PLC引用: '{RawAddress}' -> 模块={Module}, 地址={Address}",
                    plcAddress, moduleName, address);

                var value = await plcManager.ReadPLCValueAsync(moduleName, address);
                logger?.LogDebug("读取PLC值: {Module}.{Address} = {Value}", moduleName, address, value);

                // 使用共享工具格式化值
                return ExpressionUtils.FormatValueForExpression(value);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "读取PLC失败: {Address}", plcAddress);
                throw new InvalidOperationException($"读取PLC失败: {plcAddress}", ex);
            }
        }

        #endregion
    }
}
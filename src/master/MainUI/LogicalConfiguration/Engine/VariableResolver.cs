using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 变量解析器 - 修复版本 (解决UI线程死锁问题)
    /// 负责解析和替换表达式中的变量引用
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
        /// 同步预处理表达式 - 仅处理普通变量引用
        /// ⚠️ 警告：如果表达式包含PLC引用，此方法会抛出异常
        /// 包含PLC引用时请使用 PreprocessExpressionAsync
        /// </summary>
        public string PreprocessExpression(string expression)
        {
            // 先处理 DateTime.Now 表达式
            expression = ExpressionUtils.ProcessDateTimeNow(expression);

            // 检查是否包含PLC引用
            var matches = ExpressionConstants.VariablePattern.Matches(expression);
            foreach (Match match in matches)
            {
                var varName = match.Groups[1].Value;
                if (ExpressionUtils.IsPLCReference(varName))
                {
                    // 使用 Task.Run 包装异步调用，避免UI线程死锁
                    // 在非UI线程上执行异步操作，然后同步等待结果
                    return Task.Run(async () => await PreprocessExpressionAsync(expression))
                        .GetAwaiter().GetResult();
                }
            }

            // 没有PLC引用，同步处理普通变量
            return ReplaceVariableReferencesSync(expression);
        }

        /// <summary>
        /// 异步预处理表达式 - 支持PLC异步读取（推荐使用）
        /// </summary>
        public async Task<string> PreprocessExpressionAsync(string expression)
        {
            // 先处理 DateTime.Now 表达式
            expression = ExpressionUtils.ProcessDateTimeNow(expression);

            // 异步替换变量引用
            return await ReplaceVariableReferencesAsync(expression);
        }

        /// <summary>
        /// 获取表达式中引用的所有变量名
        /// </summary>
        public List<string> GetReferencedVariables(string expression)
        {
            return ExpressionUtils.GetReferencedVariables(expression);
        }

        #endregion

        #region 私有方法 - 同步变量替换（仅普通变量）

        /// <summary>
        /// 同步替换变量引用 - 仅处理普通变量，不处理PLC引用
        /// </summary>
        private string ReplaceVariableReferencesSync(string expression)
        {
            var result = expression;
            var matches = ExpressionConstants.VariablePattern.Matches(expression);

            foreach (Match match in matches)
            {
                var varName = match.Groups[1].Value;

                // 跳过PLC引用（应该不会走到这里，因为上层已经检查过）
                if (ExpressionUtils.IsPLCReference(varName))
                {
                    throw new InvalidOperationException(
                        $"同步方法不支持PLC引用: {varName}，请使用 PreprocessExpressionAsync");
                }

                var replacement = ReplaceVariableReference(varName);
                result = result.Replace(match.Value, replacement);
            }

            return result;
        }

        #endregion

        #region 私有方法 - 异步变量替换

        /// <summary>
        /// 异步替换变量引用 - 支持普通变量和PLC引用
        /// </summary>
        private async Task<string> ReplaceVariableReferencesAsync(string expression)
        {
            var result = expression;
            var matches = ExpressionConstants.VariablePattern.Matches(expression);

            foreach (Match match in matches)
            {
                var varName = match.Groups[1].Value;

                // 检查是否是PLC引用格式
                var replacement = ExpressionUtils.IsPLCReference(varName)
                    ? await ReplacePLCReferenceAsync(varName)
                    : ReplaceVariableReference(varName);

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
        /// 异步替换PLC引用
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
                // 使用共享工具正确解析PLC引用
                var (moduleName, address) = ExpressionUtils.ParsePLCReference(plcAddress);

                if (string.IsNullOrEmpty(moduleName) || string.IsNullOrEmpty(address))
                {
                    throw new InvalidOperationException($"无效的PLC地址格式: {plcAddress}");
                }

                logger?.LogDebug("解析PLC引用: '{RawAddress}' -> 模块={Module}, 地址={Address}",
                    plcAddress, moduleName, address);

                // 异步读取PLC值
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
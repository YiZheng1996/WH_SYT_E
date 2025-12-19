using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;
using static MainUI.LogicalConfiguration.Parameter.Parameter_WritePLC;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// PLC通信方法集合
    /// </summary>
    public class PLCMethods(
        IWorkflowStateService workflowStateService,
        IPLCManager plcManager,
        ILogger<PLCMethods> logger) : DSLMethodBase
    {
        public override string Category => "PLC通信";
        public override string Description => "提供PLC读写等硬件交互功能";

        private readonly ILogger<PLCMethods> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IPLCManager _plcManager = plcManager ?? throw new ArgumentNullException(nameof(plcManager));
        private readonly IWorkflowStateService _workflowStateService = workflowStateService ?? throw new ArgumentNullException(nameof(workflowStateService));

        /// <summary>
        /// PLC读取方法
        /// </summary>
        /// <param name="param">读取参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>是否读取成功</returns>
        public async Task<DetailedResult> ReadPLC(Parameter_ReadPLC param, CancellationToken cancellationToken = default)
        {
            return await ExecuteWithDetailedResult(param, async () =>
            {
                if (param?.Items == null || param.Items.Count == 0)
                {
                    throw new ArgumentException("PLC读取参数为空");
                }

                _logger.LogInformation("开始执行PLC读取操作，共 {Count} 项", param.Items.Count);

                var variables = _workflowStateService.GetVariables<VarItem_Enhanced>().ToList();
                int successCount = 0;

                foreach (var plc in param.Items)
                {
                    try
                    {
                        // 检查取消令牌
                        cancellationToken.ThrowIfCancellationRequested();

                        // 先清理花括号
                        var cleanVarName = CleanVariableName(plc.TargetVarName);

                        var targetVariable = variables.FirstOrDefault(v => v.VarName == cleanVarName);
                        if (targetVariable == null)
                        {
                            _logger.LogWarning("目标变量不存在: {TargetVarName}", plc.TargetVarName);
                            continue;
                        }

                        // 使用依赖注入的PLC管理器
                        var plcValue = await _plcManager.ReadPLCValueAsync(
                            plc.PlcModuleName,
                            plc.PlcKeyName,
                            cancellationToken);

                        targetVariable.UpdateValue(plcValue, $"PLC读取: {plc.PlcModuleName}.{plc.PlcKeyName}");
                        successCount++;

                        _logger.LogDebug("PLC读取成功: {ModuleName}.{KeyName} -> {VarName} = {Value}",
                            plc.PlcModuleName, plc.PlcKeyName, plc.TargetVarName, plcValue);
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogWarning("PLC读取被取消: {ModuleName}.{KeyName}", plc.PlcModuleName, plc.PlcKeyName);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "PLC读取项失败: {ModuleName}.{KeyName}", plc.PlcModuleName, plc.PlcKeyName);
                    }
                }

                var isSuccess = successCount > 0;
                _logger.LogInformation("PLC读取操作完成: 成功 {SuccessCount}/{TotalCount} 项，结果: {Result}",
                    successCount, param.Items.Count, isSuccess ? "成功" : "失败");

            });
        }

        /// <summary>
        /// PLC写入方法 - 使用依赖注入的PLC管理器
        /// </summary>
        /// <param name="param">写入参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>是否写入成功</returns>
        public async Task<DetailedResult> WritePLC(Parameter_WritePLC param, CancellationToken cancellationToken = default)
        {
            return await ExecuteWithDetailedResult(param, async () =>
            {
                if (param?.Items == null || param.Items.Count == 0)
                {
                    throw new ArgumentException("PLC写入参数为空");
                }

                _logger.LogInformation("开始执行PLC写入操作，共 {Count} 项", param.Items.Count);

                int successCount = 0;

                foreach (var plc in param.Items)
                {
                    try
                    {
                        // 检查取消令牌
                        cancellationToken.ThrowIfCancellationRequested();

                        // 解析写入值
                        var writeValue = await ResolveWriteValue(plc.PlcValue, cancellationToken);

                        // 使用依赖注入的PLC管理器执行写入
                        var success = await _plcManager.WritePLCValueAsync(
                            plc.PlcModuleName,
                            plc.PlcKeyName,
                            writeValue,
                            cancellationToken);

                        if (success)
                        {
                            successCount++;
                            _logger.LogDebug("PLC写入成功: {ModuleName}.{KeyName} = {Value}",
                                plc.PlcModuleName, plc.PlcKeyName, writeValue);

                            // 添加写入间隔，避免PLC通信拥塞，50ms延迟，根据实际PLC调整
                            await Task.Delay(50, cancellationToken);
                        }
                        else
                        {
                            _logger.LogWarning("PLC写入失败: {ModuleName}.{KeyName}", plc.PlcModuleName, plc.PlcKeyName);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogWarning("PLC写入被取消: {ModuleName}.{KeyName}", plc.PlcModuleName, plc.PlcKeyName);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "PLC写入项失败: {ModuleName}.{KeyName}", plc.PlcModuleName, plc.PlcKeyName);
                    }
                }

                var isSuccess = successCount > 0;
                _logger.LogInformation("PLC写入操作完成: 成功 {SuccessCount}/{TotalCount} 项，结果: {Result}",
                    successCount, param.Items.Count, isSuccess ? "成功" : "失败");

            });
        }


        /// <summary>
        /// 批量PLC读取方法
        /// </summary>
        /// <param name="readItems">读取项列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>读取结果字典</returns>
        public async Task<Dictionary<string, object>> BatchReadPLC(
            IEnumerable<PlcReadItem> readItems,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("开始批量PLC读取操作");

                var results = await _plcManager.BatchReadPLCAsync(readItems, cancellationToken);

                _logger.LogInformation("批量PLC读取完成，成功读取 {Count} 项", results.Count);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "批量PLC读取操作失败");
                throw;
            }
        }

        /// <summary>
        /// 批量PLC写入方法 - 新增功能
        /// </summary>
        /// <param name="writeItems">写入项列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>成功写入的数量</returns>
        public async Task<int> BatchWritePLC(
            IEnumerable<PLCWriteItem> writeItems,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("开始批量PLC写入操作");

                var successCount = await _plcManager.BatchWritePLCAsync(writeItems, cancellationToken);

                _logger.LogInformation("批量PLC写入完成，成功写入 {Count} 项", successCount);
                return successCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "批量PLC写入操作失败");
                throw;
            }
        }

        /// <summary>
        /// 获取PLC状态信息 - 新增功能
        /// </summary>
        /// <returns>PLC操作统计信息</returns>
        public async Task<PLCOperationStats> GetPLCStatus()
        {
            try
            {
                _logger.LogDebug("获取PLC状态信息");
                return await _plcManager.GetOperationStatsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取PLC状态信息失败");
                throw;
            }
        }

        /// <summary>
        /// 解析写入值 - 支持变量引用和表达式
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>解析后的值</returns>
        private async Task<object> ResolveWriteValue(string value, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value ?? string.Empty;

            try
            {
                // 检查是否是变量引用（例如：${variableName}）
                if (value.StartsWith("${") && value.EndsWith("}"))
                {
                    var variableName = value[2..^1]; // 移除 ${ 和 }
                    var variables = _workflowStateService.GetVariables<VarItem_Enhanced>();
                    var variable = variables.FirstOrDefault(v => v.VarName == variableName);

                    if (variable != null)
                    {
                        _logger.LogDebug("解析变量引用: {VariableName} = {Value}", variableName, variable.VarValue);
                        return variable.VarValue;
                    }
                    else
                    {
                        _logger.LogWarning("未找到引用的变量: {VariableName}", variableName);
                        return value;
                    }
                }

                // 尝试类型转换
                // 布尔值
                if (bool.TryParse(value, out var boolValue))
                    return boolValue;

                // 整数
                if (int.TryParse(value, out var intValue))
                    return intValue;

                // 浮点数
                if (double.TryParse(value, out var doubleValue))
                    return doubleValue;

                // 默认返回字符串
                return value;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "解析写入值失败，使用原始值: {Value}", value);
                return value;
            }
            finally
            {
                await Task.CompletedTask; // 保持异步签名
            }
        }

        //  新增辅助方法
        private string CleanVariableName(string varName)
        {
            if (string.IsNullOrWhiteSpace(varName))
                return string.Empty;

            var cleanName = varName.Trim();

            if (cleanName.StartsWith('{') && cleanName.EndsWith('}'))
            {
                cleanName = cleanName[1..^1].Trim();
                _logger.LogDebug("清理变量名: {Original} → {Clean}", varName, cleanName);
            }

            return cleanName;
        }
    }
}

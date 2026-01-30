using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.Instrument.Communication;
using MainUI.LogicalConfiguration.Instrument.Models;
using MainUI.LogicalConfiguration.Instrument.Parameter;
using MainUI.LogicalConfiguration.Instrument.Services;
using MainUI.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CommandType = MainUI.LogicalConfiguration.Instrument.Models.CommandType;

namespace MainUI.LogicalConfiguration.Instrument.Methods
{
    /// <summary>
    /// 仪器通讯执行方法类
    /// </summary>
    public class InstrumentCommunicationMethods(
        ILogger<InstrumentCommunicationMethods> logger,
        IInstrumentDriverService driverService,
        GlobalVariableManager variableManager,
        ExpressionEngine expressionEngine)
    {
        #region 字段

        private readonly ILogger<InstrumentCommunicationMethods> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IInstrumentDriverService _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
        private readonly GlobalVariableManager _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
        private readonly ExpressionEngine _expressionEngine = expressionEngine ?? throw new ArgumentNullException(nameof(expressionEngine));
        private readonly CommunicationProviderFactory _providerFactory = new(logger);

        #endregion

        #region 主执行方法

        /// <summary>
        /// 执行仪器通讯
        /// </summary>
        public async Task<CommunicationResult> ExecuteAsync(
            Parameter_InstrumentCommunication parameter,
            CancellationToken cancellationToken = default)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                // 参数验证
                if (parameter == null)
                {
                    return CommunicationResult.Failed("参数不能为空");
                }

                // 检查执行条件
                if (!string.IsNullOrEmpty(parameter.ExecuteCondition))
                {
                    var conditionResult = await _expressionEngine.EvaluateExpressionAsync(parameter.ExecuteCondition);
                    if (!conditionResult.Success)
                    {
                        _logger.LogInformation("执行条件不满足，跳过仪器通讯: {Condition}", parameter.ExecuteCondition);
                        return CommunicationResult.Successful("条件不满足，跳过执行");
                    }
                }

                // 发送前延时
                if (parameter.DelayBeforeSend > 0)
                {
                    await Task.Delay(parameter.DelayBeforeSend, cancellationToken);
                }

                // 获取仪器驱动
                var driver = await _driverService.GetDriverByIdAsync(parameter.DriverId);
                if (driver == null)
                {
                    return CommunicationResult.Failed($"未找到仪器驱动: {parameter.InstrumentName}");
                }

                // 获取通讯配置
                var protocolConfig = GetEffectiveConfig(driver, parameter);

                // 获取或创建通讯提供者
                var connectionId = GetConnectionId(protocolConfig);
                var provider = _providerFactory.GetOrCreateProvider(driver.ProtocolType, connectionId);

                // 连接
                if (!provider.IsConnected)
                {
                    var connected = await provider.ConnectAsync(protocolConfig, cancellationToken);
                    if (!connected)
                    {
                        return CommunicationResult.Failed($"连接仪器失败: {driver.DisplayName}");
                    }
                }

                // 构建请求数据
                byte[] requestData;
                InstrumentCommand command = null;
                int timeout;
                bool waitForResponse;

                if (parameter.UseCustomCommand)
                {
                    // 使用自定义命令
                    requestData = BuildCustomRequest(parameter);
                    timeout = parameter.OverrideTimeout ? parameter.CustomTimeout : protocolConfig.ReadTimeout;
                    waitForResponse = parameter.WaitForResponse;
                }
                else
                {
                    // 使用预定义命令
                    command = driver.GetCommand(parameter.CommandName);
                    if (command == null)
                    {
                        command = driver.Commands.FirstOrDefault(c => c.CommandId == parameter.CommandId);
                    }

                    if (command == null)
                    {
                        return CommunicationResult.Failed($"未找到命令: {parameter.CommandName}");
                    }

                    requestData = BuildCommandRequest(command, parameter.CommandParameters);
                    timeout = command.Timeout > 0 ? command.Timeout :
                             parameter.OverrideTimeout ? parameter.CustomTimeout : protocolConfig.ReadTimeout;
                    waitForResponse = command.WaitForResponse;
                }

                // 执行通讯(带重试)
                CommunicationResult result = null;
                int retryCount = parameter.RetryCount;

                for (int attempt = 0; attempt <= retryCount; attempt++)
                {
                    if (attempt > 0)
                    {
                        _logger.LogWarning("第 {Attempt} 次重试仪器通讯", attempt);
                        await Task.Delay(parameter.RetryInterval, cancellationToken);
                    }

                    result = await provider.SendAndReceiveAsync(
                        requestData,
                        driver.FrameConfig,
                        timeout,
                        waitForResponse,
                        cancellationToken);

                    if (result.Success)
                    {
                        break;
                    }
                }

                // 发送后延时
                if (parameter.DelayAfterSend > 0)
                {
                    await Task.Delay(parameter.DelayAfterSend, cancellationToken);
                }

                // 处理响应
                if (result.Success && waitForResponse)
                {
                    // 解析响应数据
                    var parseRules = parameter.UseCustomParseRules && parameter.CustomParseRules?.Count > 0
                        ? parameter.CustomParseRules
                        : command?.ParseRules;

                    if (parseRules != null && parseRules.Count > 0)
                    {
                        ParseResponse(result, parseRules);
                    }

                    // 检查成功/失败标识
                    if (command != null)
                    {
                        if (!string.IsNullOrEmpty(command.FailureIndicator) &&
                            result.ResponseString.Contains(command.FailureIndicator))
                        {
                            result.Success = false;
                            result.ErrorMessage = $"仪器返回错误标识: {command.FailureIndicator}";
                        }
                        else if (!string.IsNullOrEmpty(command.SuccessIndicator) &&
                                 !result.ResponseString.Contains(command.SuccessIndicator))
                        {
                            result.Success = false;
                            result.ErrorMessage = "响应中未包含成功标识";
                        }
                    }
                }

                // 存储结果到变量
                StoreResults(result, parameter);

                // 记录日志
                if (parameter.EnableLogging)
                {
                    LogCommunication(parameter, result);
                }

                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                return result;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("仪器通讯被取消");
                var result = CommunicationResult.Failed("操作被取消");
                StoreResults(result, parameter);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "仪器通讯执行异常");
                var result = CommunicationResult.Failed($"执行异常: {ex.Message}");
                StoreResults(result, parameter);
                return result;
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取有效的协议配置(考虑覆盖参数)
        /// </summary>
        private ProtocolConfigBase GetEffectiveConfig(InstrumentDriver driver, Parameter_InstrumentCommunication parameter)
        {
            var baseConfig = driver.GetProtocolConfig();

            if (!parameter.OverrideConnectionParams || string.IsNullOrEmpty(parameter.OverrideParamsJson))
            {
                return baseConfig;
            }

            // 合并覆盖参数
            try
            {
                ProtocolConfigBase overrideConfig = driver.ProtocolType switch
                {
                    ProtocolType.TcpIp => parameter.GetOverrideConfig<TcpProtocolConfig>(),
                    ProtocolType.Serial => parameter.GetOverrideConfig<SerialProtocolConfig>(),
                    ProtocolType.ModbusTcp or ProtocolType.ModbusRtu => parameter.GetOverrideConfig<ModbusProtocolConfig>(),
                    ProtocolType.Http => parameter.GetOverrideConfig<HttpProtocolConfig>(),
                    ProtocolType.Udp => throw new NotImplementedException("UDP协议尚未实现"),
                    _ => null
                };

                return overrideConfig ?? baseConfig;
            }
            catch
            {
                return baseConfig;
            }
        }

        /// <summary>
        /// 获取连接标识
        /// </summary>
        private string GetConnectionId(ProtocolConfigBase config)
        {
            return config switch
            {
                TcpProtocolConfig tcp => $"{tcp.IpAddress}:{tcp.Port}",
                SerialProtocolConfig serial => serial.PortName,
                ModbusProtocolConfig modbus => modbus.ProtocolType == ProtocolType.ModbusTcp
                    ? $"{modbus.IpAddress}:{modbus.Port}"
                    : modbus.PortName,
                HttpProtocolConfig http => http.BaseUrl,
                _ => "default"
            };
        }

        /// <summary>
        /// 构建自定义命令请求
        /// </summary>
        private byte[] BuildCustomRequest(Parameter_InstrumentCommunication parameter)
        {
            var command = parameter.CustomCommand;

            // 替换变量引用
            command = ReplaceVariables(command);

            // 根据数据类型转换
            return parameter.CustomCommandDataType switch
            {
                DataType.Hex => HexStringToBytes(command),
                DataType.ByteArray => command.Split(',').Select(s => byte.Parse(s.Trim())).ToArray(),
                _ => Encoding.ASCII.GetBytes(command)
            };
        }

        /// <summary>
        /// 构建预定义命令请求
        /// </summary>
        private byte[] BuildCommandRequest(InstrumentCommand command, Dictionary<string, string> parameters)
        {
            var template = command.RequestTemplate;

            // 替换命令参数
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    var placeholder = $"{{{param.Key}}}";
                    var value = ReplaceVariables(param.Value);
                    template = template.Replace(placeholder, value);
                }
            }

            // 替换默认值(未提供的参数)
            foreach (var paramDef in command.Parameters)
            {
                var placeholder = $"{{{paramDef.Name}}}";
                if (template.Contains(placeholder) && !string.IsNullOrEmpty(paramDef.DefaultValue))
                {
                    template = template.Replace(placeholder, paramDef.DefaultValue);
                }
            }

            // 替换变量引用
            template = ReplaceVariables(template);

            // 根据数据类型转换
            return command.RequestDataType switch
            {
                DataType.Hex => HexStringToBytes(template),
                DataType.ByteArray => template.Split(',').Select(s => byte.Parse(s.Trim())).ToArray(),
                _ => Encoding.ASCII.GetBytes(template)
            };
        }

        /// <summary>
        /// 替换字符串中的变量引用
        /// </summary>
        private string ReplaceVariables(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // 匹配 {$变量名} 或 {变量名} 格式
            var pattern = @"\{\$?([^}]+)\}";
            return Regex.Replace(input, pattern, match =>
            {
                var varName = match.Groups[1].Value;
                if (varName.StartsWith("$"))
                {
                    varName = varName.Substring(1);
                }

                var value = _variableManager.FindVariableByName(varName);
                return value?.ToString() ?? match.Value;
            });
        }

        /// <summary>
        /// 解析响应数据
        /// </summary>
        private void ParseResponse(CommunicationResult result, List<ResponseParseRule> parseRules)
        {
            foreach (var rule in parseRules)
            {
                try
                {
                    object parsedValue = rule.ParseType switch
                    {
                        "Position" => ParseByPosition(result, rule),
                        "Delimiter" => ParseByDelimiter(result, rule),
                        "Regex" => ParseByRegex(result, rule),
                        "Json" => ParseByJson(result, rule),
                        _ => result.ResponseString
                    };

                    // 应用缩放和偏移
                    if (parsedValue is double numValue)
                    {
                        parsedValue = numValue * rule.ScaleFactor + rule.Offset;
                    }

                    // 类型转换
                    parsedValue = ConvertToTargetType(parsedValue, rule.TargetDataType);

                    // 存储到解析结果字典
                    result.ParsedData[rule.Name] = parsedValue;

                    // 存储到全局变量
                    if (!string.IsNullOrEmpty(rule.TargetVariable))
                    {
                        _variableManager.UpdateVariableValue(rule.TargetVariable, parsedValue, "");
                        _logger.LogDebug("解析数据存储到变量 {Variable}: {Value}", rule.TargetVariable, parsedValue);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "解析规则执行失败: {RuleName}", rule.Name);
                }
            }
        }

        /// <summary>
        /// 按位置解析
        /// </summary>
        private object ParseByPosition(CommunicationResult result, ResponseParseRule rule)
        {
            var data = result.ResponseString;
            if (rule.StartPosition >= data.Length)
                return null;

            var length = rule.Length < 0 ? data.Length - rule.StartPosition : rule.Length;
            length = Math.Min(length, data.Length - rule.StartPosition);

            return data.Substring(rule.StartPosition, length).Trim();
        }

        /// <summary>
        /// 按分隔符解析
        /// </summary>
        private object ParseByDelimiter(CommunicationResult result, ResponseParseRule rule)
        {
            var parts = result.ResponseString.Split(new[] { rule.Delimiter }, StringSplitOptions.None);
            if (rule.SegmentIndex >= 0 && rule.SegmentIndex < parts.Length)
            {
                return parts[rule.SegmentIndex].Trim();
            }
            return null;
        }

        /// <summary>
        /// 按正则表达式解析
        /// </summary>
        private object ParseByRegex(CommunicationResult result, ResponseParseRule rule)
        {
            var match = Regex.Match(result.ResponseString, rule.RegexPattern);
            if (match.Success && match.Groups.Count > rule.RegexGroupIndex)
            {
                return match.Groups[rule.RegexGroupIndex].Value.Trim();
            }
            return null;
        }

        /// <summary>
        /// 按JSON路径解析
        /// </summary>
        private object ParseByJson(CommunicationResult result, ResponseParseRule rule)
        {
            try
            {
                var json = Newtonsoft.Json.Linq.JObject.Parse(result.ResponseString);
                var token = json.SelectToken(rule.JsonPath);
                return token?.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换到目标类型
        /// </summary>
        private object ConvertToTargetType(object value, DataType targetType)
        {
            if (value == null)
                return null;

            var stringValue = value.ToString().Trim();

            return targetType switch
            {
                DataType.Integer => int.TryParse(stringValue, out var i) ? i : 0,
                DataType.Double => double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0.0,
                DataType.Boolean => bool.TryParse(stringValue, out var b) ? b : stringValue != "0",
                DataType.String => stringValue,
                _ => value
            };
        }

        /// <summary>
        /// 存储执行结果到变量
        /// </summary>
        private void StoreResults(CommunicationResult result, Parameter_InstrumentCommunication parameter)
        {
            // 存储原始响应
            if (!string.IsNullOrEmpty(parameter.ResponseVariable))
            {
                _variableManager.UpdateVariableValue(parameter.ResponseVariable, result.ResponseString, "");
            }

            // 存储错误信息
            if (!string.IsNullOrEmpty(parameter.ErrorVariable) && !result.Success)
            {
                _variableManager.UpdateVariableValue(parameter.ErrorVariable, result.ErrorMessage, "");
            }

            // 存储执行状态
            if (!string.IsNullOrEmpty(parameter.StatusVariable))
            {
                _variableManager.UpdateVariableValue(parameter.StatusVariable, result.Success, "");
            }
        }

        /// <summary>
        /// 记录通讯日志
        /// </summary>
        private void LogCommunication(Parameter_InstrumentCommunication parameter, CommunicationResult result)
        {
            if (result.Success)
            {
                _logger.LogInformation(
                    "仪器通讯成功 [{Instrument}] {Command} | 发送: {Sent} | 接收: {Response} | 耗时: {Elapsed}ms",
                    parameter.InstrumentName,
                    parameter.UseCustomCommand ? "自定义命令" : parameter.CommandName,
                    result.SentString,
                    result.ResponseString,
                    result.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogWarning(
                    "仪器通讯失败 [{Instrument}] {Command} | 发送: {Sent} | 错误: {Error} | 耗时: {Elapsed}ms",
                    parameter.InstrumentName,
                    parameter.UseCustomCommand ? "自定义命令" : parameter.CommandName,
                    result.SentString,
                    result.ErrorMessage,
                    result.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        private static byte[] HexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "").Replace("0x", "").Replace("0X", "");
            if (hex.Length % 2 != 0)
                hex = "0" + hex;

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        #endregion

        #region 测试连接

        /// <summary>
        /// 测试仪器连接
        /// </summary>
        public async Task<CommunicationResult> TestConnectionAsync(
            string driverId,
            ProtocolConfigBase overrideConfig = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var driver = await _driverService.GetDriverByIdAsync(driverId);
                if (driver == null)
                {
                    return CommunicationResult.Failed("未找到仪器驱动");
                }

                var config = overrideConfig ?? driver.GetProtocolConfig();
                var provider = _providerFactory.CreateProvider(driver.ProtocolType);

                var connected = await provider.ConnectAsync(config, cancellationToken);
                if (!connected)
                {
                    return CommunicationResult.Failed("连接失败");
                }

                // 尝试发送查询命令(如果有)
                var queryCommand = driver.Commands.FirstOrDefault(c =>
                    c.CommandType == CommandType.Query ||
                    c.Name.Contains("IDN", StringComparison.OrdinalIgnoreCase) ||
                    c.Name.Contains("Identity", StringComparison.OrdinalIgnoreCase));

                if (queryCommand != null)
                {
                    var requestData = BuildCommandRequest(queryCommand, new Dictionary<string, string>());
                    var result = await provider.SendAndReceiveAsync(
                        requestData,
                        driver.FrameConfig,
                        config.ReadTimeout,
                        true,
                        cancellationToken);

                    await provider.DisconnectAsync();
                    provider.Dispose();

                    return result;
                }

                await provider.DisconnectAsync();
                provider.Dispose();

                return CommunicationResult.Successful("连接测试成功");
            }
            catch (Exception ex)
            {
                return CommunicationResult.Failed($"测试连接异常: {ex.Message}");
            }
        }

        #endregion

        #region 释放资源

        /// <summary>
        /// 释放所有通讯连接
        /// </summary>
        public void DisposeConnections()
        {
            _providerFactory.DisposeAll();
        }

        #endregion
    }
}
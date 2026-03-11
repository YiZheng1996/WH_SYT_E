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
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace MainUI.LogicalConfiguration.Instrument.Methods
{
    /// <summary>
    /// 仪器通讯执行方法类 - 修复版
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
        private readonly CommunicationProviderFactory _providerFactory = new();

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
                    _logger.LogDebug("发送前延时: {Delay}ms", parameter.DelayBeforeSend);
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
                    _logger.LogInformation("正在连接仪器: {Instrument}", driver.DisplayName);
                    var connected = await provider.ConnectAsync(protocolConfig, cancellationToken);
                    if (!connected)
                    {
                        return CommunicationResult.Failed($"连接仪器失败: {driver.DisplayName}");
                    }
                    _logger.LogInformation("仪器连接成功: {Instrument}", driver.DisplayName);
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
                    timeout = parameter.OverrideTimeout
                        ? parameter.CustomTimeout
                        : protocolConfig.ReadTimeout;
                    waitForResponse = parameter.WaitForResponse;

                    _logger.LogDebug("使用自定义命令: {Command}", parameter.CustomCommand);
                }
                else
                {
                    // 使用预定义命令
                    command = driver.GetCommand(parameter.CommandName) ??
                              driver.Commands.FirstOrDefault(c => c.CommandId == parameter.CommandId);

                    if (command == null)
                    {
                        return CommunicationResult.Failed($"未找到命令: {parameter.CommandName}");
                    }

                    requestData = BuildCommandRequest(command, parameter.CommandParameters);
                    timeout = command.Timeout > 0
                        ? command.Timeout
                        : (parameter.OverrideTimeout ? parameter.CustomTimeout : protocolConfig.ReadTimeout);
                    waitForResponse = command.WaitForResponse;

                    _logger.LogDebug("使用预定义命令: {CommandName} (ID: {CommandId})",
                        command.DisplayName, command.CommandId);
                }

                // 执行通讯(带重试)
                CommunicationResult result = null;
                int retryCount = parameter.RetryCount;

                for (int attempt = 0; attempt <= retryCount; attempt++)
                {
                    if (attempt > 0)
                    {
                        _logger.LogWarning("第 {Attempt}/{Total} 次重试仪器通讯",
                            attempt, retryCount);
                        await Task.Delay(parameter.RetryInterval, cancellationToken);
                    }

                    result = await provider.SendAndReceiveAsync(
                        requestData,
                        driver.FrameConfig,
                        timeout,
                        waitForResponse,
                        cancellationToken);

                    // 如果成功或不等待响应，则跳出重试循环
                    if (result.Success || !waitForResponse)
                    {
                        break;
                    }
                }

                // 发送后延时
                if (parameter.DelayAfterSend > 0)
                {
                    _logger.LogDebug("发送后延时: {Delay}ms", parameter.DelayAfterSend);
                    await Task.Delay(parameter.DelayAfterSend, cancellationToken);
                }

                // 处理响应
                if (result.Success && waitForResponse)
                {
                    //先检查成功/失败标识，再解析数据
                    bool validationPassed = ValidateResponse(result, command, parameter);

                    if (!validationPassed)
                    {
                        // 验证失败，不执行解析
                        _logger.LogWarning("响应验证失败，跳过数据解析");
                    }
                    else
                    {
                        // 验证成功，执行数据解析
                        var parseRules = parameter.UseCustomParseRules && parameter.CustomParseRules?.Count > 0
                            ? parameter.CustomParseRules
                            : command?.ParseRules;

                        if (parseRules != null && parseRules.Count > 0)
                        {
                            _logger.LogDebug("开始解析响应数据，规则数量: {Count}", parseRules.Count);
                            ParseResponse(result, parseRules);
                        }
                    }
                }
                else if (!result.Success)
                {
                    _logger.LogWarning("通讯失败: {Error}", result.ErrorMessage);
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
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                StoreResults(result, parameter);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "仪器通讯执行异常");
                var result = CommunicationResult.Failed($"执行异常: {ex.Message}");
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                StoreResults(result, parameter);
                return result;
            }
        }

        #endregion

        #region 响应验证方法

        /// <summary>
        /// 验证响应是否符合成功/失败标识
        /// </summary>
        /// <returns>true=验证通过, false=验证失败</returns>
        private bool ValidateResponse(
            CommunicationResult result,
            InstrumentCommand command,
            Parameter_InstrumentCommunication parameter)
        {
            // 如果没有命令定义，默认通过验证
            if (command == null)
            {
                return true;
            }

            // 处理空响应
            if (string.IsNullOrEmpty(result.ResponseString))
            {
                _logger.LogWarning("响应为空");

                // 如果设置了成功标识，空响应视为失败
                if (string.IsNullOrEmpty(command.SuccessIndicator)) return true;

                result.Success = false;
                result.ErrorMessage = "响应为空，无法验证成功标识";
                return false;

            }

            // Trim并转换为统一格式
            var originalResponse = result.ResponseString;
            var cleanResponse = originalResponse.Trim();

            // 记录详细的响应信息用于调试
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var responseBytes = result.RawResponse ?? Array.Empty<byte>();
                _logger.LogDebug(
                    "响应验证开始\n" +
                    "  原始响应: [{Original}]\n" +
                    "  清理后: [{Clean}]\n" +
                    "  字节长度: {ByteLen}\n" +
                    "  字符长度: {StrLen}\n" +
                    "  十六进制: {Hex}\n" +
                    "  成功标识: [{Success}]\n" +
                    "  失败标识: [{Failure}]",
                    originalResponse,
                    cleanResponse,
                    responseBytes.Length,
                    cleanResponse.Length,
                    BitConverter.ToString(responseBytes),
                    command.SuccessIndicator ?? "(未设置)",
                    command.FailureIndicator ?? "(未设置)"
                );
            }

            // 先检查失败标识(优先级更高)
            if (!string.IsNullOrEmpty(command.FailureIndicator))
            {
                // 使用不区分大小写的IndexOf进行匹配
                bool containsFailure = cleanResponse.IndexOf(
                    command.FailureIndicator.Trim(),
                    StringComparison.OrdinalIgnoreCase) >= 0;

                if (containsFailure)
                {
                    result.Success = false;
                    result.ErrorMessage = $"仪器返回错误标识: {command.FailureIndicator}";

                    _logger.LogWarning(
                        "检测到失败标识\n" +
                        "  失败标识: [{Indicator}]\n" +
                        "  响应内容: [{Response}]",
                        command.FailureIndicator,
                        cleanResponse
                    );

                    return false; // 立即返回，不再检查成功标识
                }

                _logger.LogDebug("未检测到失败标识: [{Indicator}]", command.FailureIndicator);
            }

            // 检查成功标识(使用不区分大小写匹配)
            if (string.IsNullOrEmpty(command.SuccessIndicator)) return true;

            // 使用不区分大小写的IndexOf进行匹配
            bool containsSuccess = cleanResponse.IndexOf(
                command.SuccessIndicator.Trim(),
                StringComparison.OrdinalIgnoreCase) >= 0;

            if (!containsSuccess)
            {
                result.Success = false;
                result.ErrorMessage = $"响应中未包含成功标识。期望: {command.SuccessIndicator}, 实际: {cleanResponse}";

                // 详细的诊断日志
                _logger.LogWarning(
                    "成功标识匹配失败\n" +
                    "  期望包含: [{Expected}]\n" +
                    "  实际响应(原始): [{Original}]\n" +
                    "  实际响应(清理): [{Clean}]\n" +
                    "  大小写敏感匹配: {CaseSensitive}\n" +
                    "  不区分大小写匹配: {CaseInsensitive}\n" +
                    "  原始字节(Hex): {Hex}",
                    command.SuccessIndicator,
                    originalResponse,
                    cleanResponse,
                    cleanResponse.Contains(command.SuccessIndicator),
                    cleanResponse.IndexOf(command.SuccessIndicator, StringComparison.OrdinalIgnoreCase) >= 0,
                    BitConverter.ToString(result.RawResponse ?? Array.Empty<byte>())
                );

                return false;
            }
            else
            {
                _logger.LogDebug("成功标识匹配成功: [{Indicator}]", command.SuccessIndicator);
            }

            // 所有验证通过
            return true;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取有效的协议配置(考虑覆盖参数)
        /// </summary>
        private ProtocolConfigBase GetEffectiveConfig(
            InstrumentDriver driver,
            Parameter_InstrumentCommunication parameter)
        {
            var config = driver.GetProtocolConfig();

            // 如果参数中指定了覆盖配置
            if (!parameter.OverrideConnectionParams ||
                string.IsNullOrEmpty(parameter.OverrideParamsJson)) return config;

            try
            {
                // 这里需要根据协议类型反序列化配置
                // 简化处理，实际应该根据ProtocolType进行类型转换
                _logger.LogDebug("使用覆盖的连接参数");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "覆盖配置解析失败，使用默认配置");
            }

            return config;
        }

        /// <summary>
        /// 获取连接标识
        /// </summary>
        private string GetConnectionId(ProtocolConfigBase config)
        {
            return config switch
            {
                SerialProtocolConfig serial => $"Serial_{serial.PortName}",
                TcpProtocolConfig tcp => $"TCP_{tcp.IpAddress}_{tcp.Port}",
                _ => Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// 构建自定义请求数据
        /// </summary>
        private byte[] BuildCustomRequest(Parameter_InstrumentCommunication parameter)
        {
            var content = ResolveVariables(parameter.CustomCommand);

            return parameter.CustomCommandDataType switch
            {
                DataType.Hex => HexStringToBytes(content),
                DataType.ByteArray => Encoding.UTF8.GetBytes(content), // 字节数组使用UTF8编码
                _ => Encoding.UTF8.GetBytes(content) // 默认使用UTF8编码
            };
        }

        /// <summary>
        /// 构建命令请求数据
        /// </summary>
        private byte[] BuildCommandRequest(
        InstrumentCommand command,
        Dictionary<string, string> parameters)
        {
            var template = command.RequestTemplate;

            // 1. 替换参数占位符 {ParamName}
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    var placeholder = $"{{{param.Key}}}";
                    var value = ResolveVariables(param.Value);
                    template = template.Replace(placeholder, value);
                }
            }

            // 2. 替换变量引用 {$VarName}
            template = ResolveVariables(template);

            // 3. Modbus 协议特殊处理：生成 PDU bytes
            if (template.StartsWith("MODBUS:", StringComparison.OrdinalIgnoreCase))
            {
                return BuildModbusPdu(template[7..], command); // 去掉 "MODBUS:" 前缀
            }

            // 4. 普通协议按数据类型编码
            return command.RequestDataType switch
            {
                DataType.Hex => HexStringToBytes(template),
                DataType.ByteArray => Encoding.UTF8.GetBytes(template),
                _ => Encoding.UTF8.GetBytes(template)
            };
        }

        /// <summary>
        /// 从模板字符串构建 Modbus PDU
        /// 格式示例：
        ///   "FC=03,Addr=40001,Count=2"   → 读保持寄存器
        ///   "FC=06,Addr=40001,Value=100" → 写单寄存器
        ///   "FC=10,Addr=40001,Count=2,Values=100,200" → 写多寄存器
        /// </summary>
        private byte[] BuildModbusPdu(string template, InstrumentCommand command)
        {
            // 解析 key=value 对
            var kvPairs = template.Split(',')
                .Select(s => s.Trim().Split('='))
                .Where(a => a.Length == 2)
                .ToDictionary(
                    a => a[0].Trim().ToUpperInvariant(),
                    a => a[1].Trim(),
                    StringComparer.OrdinalIgnoreCase);

            if (!kvPairs.TryGetValue("FC", out var fcStr) || !byte.TryParse(fcStr, out var fc))
            {
                _logger.LogError("Modbus模板缺少有效的功能码FC: {Template}", template);
                return Array.Empty<byte>();
            }

            // 从驱动配置取从站地址（通过 command 找不到，从 _driverService 找）
            // 简化处理：SlaveAddress 在 ModbusProtocolConfig 里，这里约定默认为 1
            // 若需要精确，可在 Parameters 里加一个 SlaveAddr 参数
            byte slaveAddr = 1;
            if (kvPairs.TryGetValue("SLAVE", out var slaveStr))
                byte.TryParse(slaveStr, out slaveAddr);

            var pdu = new List<byte> { slaveAddr, fc };

            switch (fc)
            {
                case 0x03: // 读保持寄存器
                case 0x04: // 读输入寄存器
                    if (kvPairs.TryGetValue("ADDR", out var addrStr3) &&
                        kvPairs.TryGetValue("COUNT", out var countStr))
                    {
                        ushort addr = ParseRegisterAddress(addrStr3);
                        ushort count = ushort.Parse(countStr);
                        pdu.AddRange(BitConverter.GetBytes(addr).Reverse());
                        pdu.AddRange(BitConverter.GetBytes(count).Reverse());
                    }
                    break;

                case 0x06: // 写单寄存器
                    if (kvPairs.TryGetValue("ADDR", out var addrStr6) &&
                        kvPairs.TryGetValue("VALUE", out var valueStr6))
                    {
                        ushort addr = ParseRegisterAddress(addrStr6);
                        ushort value = ushort.Parse(valueStr6);
                        pdu.AddRange(BitConverter.GetBytes(addr).Reverse());
                        pdu.AddRange(BitConverter.GetBytes(value).Reverse());
                    }
                    break;

                case 0x10: // 写多寄存器
                    if (kvPairs.TryGetValue("ADDR", out var addrStr10) &&
                        kvPairs.TryGetValue("COUNT", out var countStr10) &&
                        kvPairs.TryGetValue("VALUES", out var valuesStr))
                    {
                        ushort addr = ParseRegisterAddress(addrStr10);
                        ushort count = ushort.Parse(countStr10);
                        var vals = valuesStr.Split(';').Select(ushort.Parse).ToArray();

                        pdu.AddRange(BitConverter.GetBytes(addr).Reverse());
                        pdu.AddRange(BitConverter.GetBytes(count).Reverse());
                        pdu.Add((byte)(count * 2)); // Byte Count
                        foreach (var v in vals)
                            pdu.AddRange(BitConverter.GetBytes(v).Reverse());
                    }
                    break;

                default:
                    _logger.LogWarning("不支持的Modbus功能码: 0x{FC:X2}", fc);
                    break;
            }

            return pdu.ToArray();
        }

        /// <summary>
        /// 解析寄存器地址（支持 40001 格式和 0 开始的直接地址）
        /// 40001 → 0, 30001 → 0 (去掉类型前缀)
        /// </summary>
        private static ushort ParseRegisterAddress(string addrStr)
        {
            if (!ushort.TryParse(addrStr, out ushort addr)) return 0;

            // Modbus 传统地址：40001 = 保持寄存器 1 → 实际地址 0
            if (addr >= 40001 && addr <= 49999) return (ushort)(addr - 40001);
            if (addr >= 30001 && addr <= 39999) return (ushort)(addr - 30001);
            if (addr >= 10001 && addr <= 19999) return (ushort)(addr - 10001);
            if (addr >= 1 && addr <= 9999) return (ushort)(addr - 1);

            return addr; // 直接地址
        }


        /// <summary>
        /// 解析变量引用
        /// </summary>
        private string ResolveVariables(string content)
        {
            if (string.IsNullOrEmpty(content))
                return content;

            // 匹配 {$变量名} 格式
            var regex = new Regex(@"\{\$(\w+)\}");
            return regex.Replace(content, match =>
            {
                var varName = match.Groups[1].Value;
                var variable = _variableManager.FindVariableByName(varName);
                return variable?.VarValue?.ToString() ?? match.Value;
            });
        }

        /// <summary>
        /// 解析响应数据
        /// </summary>
        private void ParseResponse(CommunicationResult result, List<ResponseParseRule> rules)
        {
            foreach (var rule in rules)
            {
                try
                {
                    object parsedValue = null;

                    switch (rule.ParseType?.ToLower())
                    {
                        case "regex":
                            parsedValue = ParseByRegex(result.ResponseString, rule);
                            break;
                        case "position":
                            parsedValue = ParseByPosition(result.ResponseString, rule);
                            break;
                        case "split":
                            parsedValue = ParseBySplit(result.ResponseString, rule);
                            break;
                        default:
                            _logger.LogWarning("未知的解析类型: {Type}", rule.ParseType);
                            continue;
                    }

                    if (parsedValue == null || string.IsNullOrEmpty(rule.TargetVariable)) continue;

                    // 类型转换
                    var convertedValue = ConvertToTargetType(parsedValue, rule.TargetDataType);

                    // 保存到变量
                    _variableManager.UpdateVariableValue(
                        rule.TargetVariable,
                        convertedValue,
                        $"解析自仪器响应");

                    _logger.LogDebug(
                        "解析成功: 规则[{Rule}] -> 变量[{Var}] = {Value}",
                        rule.Name,
                        rule.TargetVariable,
                        convertedValue);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "解析规则执行失败: {Rule}", rule.Name);
                }
            }
        }

        /// <summary>
        /// 正则表达式解析
        /// </summary>
        private object ParseByRegex(string response, ResponseParseRule rule)
        {
            var regex = new Regex(rule.RegexPattern);
            var match = regex.Match(response);

            if (match.Success)
            {
                return match.Groups.Count > 1 ? match.Groups[1].Value : match.Value;
            }

            return null;
        }

        /// <summary>
        /// 位置解析
        /// </summary>
        private object ParseByPosition(string response, ResponseParseRule rule)
        {
            if (rule.StartPosition < 0 || rule.StartPosition >= response.Length)
                return null;

            var length = rule.Length > 0 ? rule.Length : response.Length - rule.StartPosition;
            length = Math.Min(length, response.Length - rule.StartPosition);

            return response.Substring(rule.StartPosition, length);
        }

        /// <summary>
        /// 分隔符解析
        /// </summary>
        private object ParseBySplit(string response, ResponseParseRule rule)
        {
            var parts = response.Split([rule.Delimiter], StringSplitOptions.None);

            if (rule.SegmentIndex >= 0 && rule.SegmentIndex < parts.Length)
            {
                return parts[rule.SegmentIndex].Trim();
            }

            return null;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        private object ConvertToTargetType(object value, DataType targetType)
        {
            var stringValue = value?.ToString() ?? "";

            return targetType switch
            {
                DataType.Integer => int.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var i) ? i : 0,
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
                _variableManager.UpdateVariableValue(
                    parameter.ResponseVariable,
                    result.ResponseString ?? "",
                    "仪器响应");

                _logger.LogDebug("响应已保存到变量: {Var}", parameter.ResponseVariable);
            }

            // 存储错误信息
            if (!string.IsNullOrEmpty(parameter.ErrorVariable) && !result.Success)
            {
                _variableManager.UpdateVariableValue(
                    parameter.ErrorVariable,
                    result.ErrorMessage ?? "",
                    "错误信息");

                _logger.LogDebug("错误信息已保存到变量: {Var}", parameter.ErrorVariable);
            }

            // 存储执行状态
            if (string.IsNullOrEmpty(parameter.StatusVariable)) return;

            _variableManager.UpdateVariableValue(
                parameter.StatusVariable,
                result.Success,
                "执行状态");

            _logger.LogDebug("执行状态已保存到变量: {Var} = {Status}",
                parameter.StatusVariable, result.Success);
        }

        /// <summary>
        /// 记录通讯日志
        /// </summary>
        private void LogCommunication(Parameter_InstrumentCommunication parameter, CommunicationResult result)
        {
            if (result.Success)
            {
                _logger.LogInformation(
                    "仪器通讯成功 [{Instrument}] {Command}\n" +
                    "  发送: {Sent}\n" +
                    "  接收: {Response}\n" +
                    "  耗时: {Elapsed}ms",
                    parameter.InstrumentName,
                    parameter.UseCustomCommand ? "自定义命令" : parameter.CommandName,
                    result.SentString,
                    result.ResponseString,
                    result.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogWarning(
                    "仪器通讯失败 [{Instrument}] {Command}\n" +
                    "  发送: {Sent}\n" +
                    "  错误: {Error}\n" +
                    "  耗时: {Elapsed}ms",
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
                var connectionId = GetConnectionId(config);
                var provider = _providerFactory.GetOrCreateProvider(driver.ProtocolType, connectionId);

                var connected = await provider.ConnectAsync(config, cancellationToken);

                if (connected)
                {
                    await provider.DisconnectAsync();
                    return CommunicationResult.Successful("连接测试成功");
                }
                else
                {
                    return CommunicationResult.Failed("连接测试失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "测试连接异常");
                return CommunicationResult.Failed($"测试连接异常: {ex.Message}");
            }
        }

        #endregion
    }
}
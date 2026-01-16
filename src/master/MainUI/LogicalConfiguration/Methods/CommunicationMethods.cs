using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services.ServicesCommunication;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.RegularExpressions;
using static MainUI.LogicalConfiguration.Parameter.Parameter_EthernetSend;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 通信操作方法
    /// </summary>
    public class CommunicationMethods(
        GlobalVariableManager variableManager,
        ILogger<CommunicationMethods> logger)
    {
        private readonly GlobalVariableManager _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));

        /// <summary>
        /// 执行以太网发送
        /// </summary>
        public async Task<bool> ExecuteEthernetSend(Parameter_EthernetSend param, CancellationToken cancellationToken = default)
        {
            if (!param.IsEnabled)
            {
                logger?.LogDebug("以太网发送步骤已禁用，跳过执行");
                return true;
            }

            logger?.LogInformation("开始执行以太网发送: {IP}:{Port}", param.IPAddress, param.Port);

            try
            {
                var config = new EthernetConfig
                {
                    IPAddress = param.IPAddress,
                    Port = param.Port,
                    Protocol = param.Protocol,
                    ConnectTimeout = param.ConnectTimeout,
                    SendTimeout = param.SendTimeout,
                    ReceiveTimeout = param.ResponseTimeout
                };

                using var service = new EthernetService(config);

                // 准备数据
                byte[] data = PrepareData(param);

                logger?.LogDebug("准备发送数据: {ByteCount} 字节", data.Length);

                CommunicationResult result;
                if (param.WaitResponse)
                {
                    result = await service.SendAndReceiveAsync(data, param.ResponseTimeout, cancellationToken);

                    // 保存响应到变量
                    if (result.Success && !string.IsNullOrEmpty(param.ResponseVariableName))
                    {
                        _variableManager.UpdateVariableValue(param.ResponseVariableName, result.ResponseText ?? "", "");
                        logger?.LogDebug("响应已保存到变量: {VarName}", param.ResponseVariableName);
                    }
                }
                else
                {
                    result = await service.SendAsync(data, cancellationToken);
                }

                if (param.DisconnectAfterSend)
                {
                    await service.DisconnectAsync();
                }

                if (result.Success)
                {
                    logger?.LogInformation("以太网发送成功: {Message}", result.Message);
                }
                else
                {
                    logger?.LogWarning("以太网发送失败: {Message}", result.Message);
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "以太网发送执行异常");
                return false;
            }
        }

        /// <summary>
        /// 执行串口发送
        /// </summary>
        public async Task<bool> ExecuteSerialPortSend(Parameter_SerialPortSend param, CancellationToken cancellationToken = default)
        {
            if (!param.IsEnabled)
            {
                logger?.LogDebug("串口发送步骤已禁用，跳过执行");
                return true;
            }

            logger?.LogInformation("开始执行串口发送: {PortName}", param.PortName);

            try
            {
                var config = new SerialPortConfig
                {
                    PortName = param.PortName,
                    BaudRate = param.BaudRate,
                    Parity = param.Parity,
                    DataBits = param.DataBits,
                    StopBits = param.StopBits,
                    Handshake = param.Handshake,
                    ReadTimeout = param.ResponseTimeout,
                    WriteTimeout = 3000
                };

                using var service = new SerialPortService(config);

                // 准备数据
                byte[] data = PrepareSerialData(param);

                logger?.LogDebug("准备发送数据: {ByteCount} 字节", data.Length);

                CommunicationResult result;
                if (param.WaitResponse)
                {
                    result = await service.SendAndReceiveAsync(data, param.ResponseTimeout, cancellationToken);

                    // 保存响应到变量
                    if (result.Success && !string.IsNullOrEmpty(param.ResponseVariableName))
                    {
                        _variableManager.UpdateVariableValue(param.ResponseVariableName, result.ResponseText ?? "", "string");
                        logger?.LogDebug("响应已保存到变量: {VarName}", param.ResponseVariableName);
                    }
                }
                else
                {
                    result = await service.SendAsync(data, cancellationToken);
                }

                if (param.CloseAfterSend)
                {
                    await service.DisconnectAsync();
                }

                if (result.Success)
                {
                    logger?.LogInformation("串口发送成功: {Message}", result.Message);
                }
                else
                {
                    logger?.LogWarning("串口发送失败: {Message}", result.Message);
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "串口发送执行异常");
                return false;
            }
        }

        #region 私有方法

        private byte[] PrepareData(Parameter_EthernetSend param)
        {
            string content = ResolveVariables(param.SendContent);

            if (param.AppendNewLine)
            {
                content += param.NewLineType;
            }

            var encoding = GetEncoding(param.Encoding);

            return param.DataFormat switch
            {
                DataFormatType.Hex => HexStringToBytes(content),
                DataFormatType.Base64 => Convert.FromBase64String(content),
                _ => encoding.GetBytes(content)
            };
        }

        private byte[] PrepareSerialData(Parameter_SerialPortSend param)
        {
            string content = ResolveVariables(param.SendContent);

            if (param.AppendNewLine)
            {
                content += param.NewLineType;
            }

            var encoding = GetEncoding(param.Encoding);

            return param.DataFormat switch
            {
                DataFormatType.Hex => HexStringToBytes(content),
                DataFormatType.Base64 => Convert.FromBase64String(content),
                _ => encoding.GetBytes(content)
            };
        }

        private string ResolveVariables(string content)
        {
            if (string.IsNullOrEmpty(content))
                return content;

            var regex = new Regex(@"\{(\w+)\}");
            return regex.Replace(content, match =>
            {
                var varName = match.Groups[1].Value;
                var value = _variableManager.GetAllUserVariables();
                return value?.ToString() ?? match.Value;
            });
        }

        private static Encoding GetEncoding(EncodingType type)
        {
            return type switch
            {
                EncodingType.ASCII => Encoding.ASCII,
                EncodingType.GB2312 => Encoding.GetEncoding("GB2312"),
                EncodingType.Unicode => Encoding.Unicode,
                _ => Encoding.UTF8
            };
        }

        private static byte[] HexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "");
            return Enumerable.Range(0, hex.Length / 2)
                .Select(i => Convert.ToByte(hex.Substring(i * 2, 2), 16))
                .ToArray();
        }

        #endregion
    }
}
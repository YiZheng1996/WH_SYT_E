using MainUI.LogicalConfiguration.Instrument.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// Modbus通讯提供者(简化实现)
    /// 实际项目中建议使用NModbus等成熟库
    /// </summary>
    public class ModbusCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private ICommunicationProvider _underlyingProvider;
        private ModbusProtocolConfig _config;

        public ProtocolType ProtocolType => _config?.ProtocolType ?? ProtocolType.ModbusTcp;
        public bool IsConnected => _underlyingProvider?.IsConnected ?? false;
        public string ConnectionId => _underlyingProvider?.ConnectionId ?? "";

        public async Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not ModbusProtocolConfig modbusConfig)
            {
                logger?.LogError("无效的Modbus配置");
                return false;
            }

            _config = modbusConfig;

            // 根据Modbus类型创建底层通讯
            if (modbusConfig.ProtocolType == ProtocolType.ModbusTcp)
            {
                _underlyingProvider = new TcpCommunicationProvider(logger);
                var tcpConfig = new TcpProtocolConfig
                {
                    IpAddress = modbusConfig.IpAddress,
                    Port = modbusConfig.Port,
                    ConnectionTimeout = modbusConfig.ConnectionTimeout,
                    ReadTimeout = modbusConfig.ReadTimeout,
                    WriteTimeout = modbusConfig.WriteTimeout
                };
                return await _underlyingProvider.ConnectAsync(tcpConfig, cancellationToken);
            }
            else
            {
                _underlyingProvider = new SerialCommunicationProvider(logger);
                var serialConfig = new SerialProtocolConfig
                {
                    PortName = modbusConfig.PortName,
                    BaudRate = modbusConfig.BaudRate,
                    DataBits = modbusConfig.DataBits,
                    StopBits = modbusConfig.StopBits,
                    Parity = modbusConfig.Parity,
                    ReadTimeout = modbusConfig.ReadTimeout,
                    WriteTimeout = modbusConfig.WriteTimeout
                };
                return await _underlyingProvider.ConnectAsync(serialConfig, cancellationToken);
            }
        }

        public async Task DisconnectAsync()
        {
            if (_underlyingProvider != null)
            {
                await _underlyingProvider.DisconnectAsync();
            }
        }

        public async Task<CommunicationResult> SendAndReceiveAsync(
            byte[] data,
            FrameConfig frameConfig,
            int timeout,
            bool waitForResponse,
            CancellationToken cancellationToken = default)
        {
            // 简化实现：解析Modbus命令并构建报文
            // 格式: "功能码,参数1,参数2,..."
            try
            {
                var commandStr = Encoding.ASCII.GetString(data);
                var parts = commandStr.Split(',');

                if (parts.Length < 1)
                {
                    return CommunicationResult.Failed("无效的Modbus命令格式");
                }

                var functionCode = byte.Parse(parts[0]);
                var modbusRequest = BuildModbusRequest(functionCode, parts.Skip(1).ToArray());

                // 添加Modbus TCP头或RTU CRC
                byte[] finalRequest;
                if (_config.ProtocolType == ProtocolType.ModbusTcp)
                {
                    finalRequest = AddModbusTcpHeader(modbusRequest);
                }
                else
                {
                    finalRequest = AddModbusRtuCrc(modbusRequest);
                }

                // 设置Modbus特定的帧配置
                var modbusFrameConfig = new FrameConfig
                {
                    Enabled = true,
                    FixedResponseLength = CalculateExpectedResponseLength(functionCode, parts)
                };

                return await _underlyingProvider.SendAndReceiveAsync(finalRequest, modbusFrameConfig, timeout, waitForResponse, cancellationToken);
            }
            catch (Exception ex)
            {
                return CommunicationResult.Failed($"Modbus请求构建失败: {ex.Message}");
            }
        }

        private byte[] BuildModbusRequest(byte functionCode, string[] parameters)
        {
            var request = new List<byte> { _config.SlaveAddress, functionCode };

            switch (functionCode)
            {
                case 0x03: // 读取保持寄存器
                case 0x04: // 读取输入寄存器
                    if (parameters.Length >= 2)
                    {
                        var startAddress = ushort.Parse(parameters[0]);
                        var count = ushort.Parse(parameters[1]);
                        request.AddRange(BitConverter.GetBytes(startAddress).Reverse());
                        request.AddRange(BitConverter.GetBytes(count).Reverse());
                    }
                    break;

                case 0x06: // 写入单个寄存器
                    if (parameters.Length >= 2)
                    {
                        var address = ushort.Parse(parameters[0]);
                        var value = ushort.Parse(parameters[1]);
                        request.AddRange(BitConverter.GetBytes(address).Reverse());
                        request.AddRange(BitConverter.GetBytes(value).Reverse());
                    }
                    break;
            }

            return request.ToArray();
        }

        private byte[] AddModbusTcpHeader(byte[] pdu)
        {
            var mbap = new byte[7];
            // Transaction ID (2 bytes)
            mbap[0] = 0x00;
            mbap[1] = 0x01;
            // Protocol ID (2 bytes, always 0 for Modbus)
            mbap[2] = 0x00;
            mbap[3] = 0x00;
            // Length (2 bytes)
            var length = (ushort)(pdu.Length);
            mbap[4] = (byte)(length >> 8);
            mbap[5] = (byte)(length & 0xFF);
            // Unit ID
            mbap[6] = pdu[0];

            return mbap.Concat(pdu.Skip(1)).ToArray();
        }

        private byte[] AddModbusRtuCrc(byte[] pdu)
        {
            var crc = CalculateCrc16(pdu);
            return pdu.Concat(BitConverter.GetBytes(crc)).ToArray();
        }

        private ushort CalculateCrc16(byte[] data)
        {
            ushort crc = 0xFFFF;
            foreach (var b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }

        private int CalculateExpectedResponseLength(byte functionCode, string[] parts)
        {
            return functionCode switch
            {
                0x03 or 0x04 when parts.Length >= 2 => 5 + int.Parse(parts[1]) * 2 + (_config.ProtocolType == ProtocolType.ModbusRtu ? 2 : 0),
                0x06 => 8 + (_config.ProtocolType == ProtocolType.ModbusRtu ? 2 : 0),
                _ => 256
            };
        }

        public Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            return _underlyingProvider?.SendAsync(data, cancellationToken) ?? Task.FromResult(false);
        }

        public Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default)
        {
            return _underlyingProvider?.ReceiveAsync(frameConfig, timeout, cancellationToken) ?? Task.FromResult(Array.Empty<byte>());
        }

        public void Dispose()
        {
            _underlyingProvider?.Dispose();
        }
    }
}

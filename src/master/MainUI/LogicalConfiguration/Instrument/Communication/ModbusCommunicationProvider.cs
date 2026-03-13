using MainUI.LogicalConfiguration.Instrument.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// Modbus 通讯提供者
    ///
    /// 修复要点：
    /// 1. SendAndReceiveAsync 不再将 byte[] 解析为 ASCII 字符串命令
    ///    → 改为直接接收已构建好的 Modbus PDU（由 BuildCommandRequest 生成）
    /// 2. Transaction ID 使用原子递增，避免并发冲突
    /// 3. RTU 响应验证 CRC
    /// 4. 响应解析：将寄存器数据转为可读字符串供 ParseResponse 处理
    ///
    /// 与 BuildCommandRequest 的约定：
    ///   RequestTemplate = "FC:03,Addr:0,Count:2"  （模板格式）
    ///   运行时 BuildCommandRequest 替换参数后变为纯 PDU bytes
    ///   → 本类直接加 MBAP/CRC 头后发送，不做二次解析
    /// </summary>
    public class ModbusCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private ICommunicationProvider _underlyingProvider;
        private ModbusProtocolConfig _config;
        private int _transactionId = 0;   // 原子递增

        public ProtocolType ProtocolType => _config?.ProtocolType ?? ProtocolType.ModbusTcp;
        public bool IsConnected => _underlyingProvider?.IsConnected ?? false;
        public string ConnectionId => _underlyingProvider?.ConnectionId ?? "";

        // ── 连接 ─────────────────────────────────────────────────────────────

        public async Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not ModbusProtocolConfig modbusConfig)
            {
                logger?.LogError("无效的Modbus配置");
                return false;
            }
            _config = modbusConfig;

            if (modbusConfig.ProtocolType == ProtocolType.ModbusTcp)
            {
                _underlyingProvider = new TcpCommunicationProvider(logger);
                return await _underlyingProvider.ConnectAsync(new TcpProtocolConfig
                {
                    IpAddress = modbusConfig.IpAddress,
                    Port = modbusConfig.Port,
                    ConnectionTimeout = modbusConfig.ConnectionTimeout,
                    ReadTimeout = modbusConfig.ReadTimeout,
                }, cancellationToken);
            }
            else
            {
                _underlyingProvider = new SerialCommunicationProvider(logger);
                return await _underlyingProvider.ConnectAsync(new SerialProtocolConfig
                {
                    PortName = modbusConfig.PortName,
                    BaudRate = modbusConfig.BaudRate,
                    DataBits = modbusConfig.DataBits,
                    StopBits = modbusConfig.StopBits,
                    Parity = modbusConfig.Parity,
                    ReadTimeout = modbusConfig.ReadTimeout,
                }, cancellationToken);
            }
        }

        public async Task DisconnectAsync()
        {
            if (_underlyingProvider != null)
                await _underlyingProvider.DisconnectAsync();
        }

        // ── 发送并接收 ───────────────────────────────────────────────────────
        // data 参数约定：由 BuildCommandRequest 生成的原始 Modbus PDU（不含 MBAP/CRC）
        // 格式：[SlaveAddr(1), FunctionCode(1), ...参数字节...]

        public async Task<CommunicationResult> SendAndReceiveAsync(
            byte[] data,
            FrameConfig frameConfig,
            int timeout,
            bool waitForResponse,
            CancellationToken cancellationToken = default)
        {
            if (data == null || data.Length < 2)
                return CommunicationResult.Failed("Modbus PDU 数据无效");

            var sw = Stopwatch.StartNew();

            try
            {
                byte functionCode = data[1];
                byte[] finalRequest;
                FrameConfig modbusFrameConfig;
                int expectedLength;

                if (_config.ProtocolType == ProtocolType.ModbusTcp)
                {
                    // 生成唯一 Transaction ID（原子递增）
                    ushort txId = (ushort)(Interlocked.Increment(ref _transactionId) & 0xFFFF);
                    finalRequest = AddModbusTcpHeader(data, txId);
                    expectedLength = CalculateExpectedLength(functionCode, data);
                    modbusFrameConfig = new FrameConfig
                    {
                        Enabled = true,
                        FixedResponseLength = expectedLength
                    };
                }
                else
                {
                    finalRequest = AddModbusRtuCrc(data);
                    expectedLength = CalculateExpectedLength(functionCode, data) + 2; // +2 CRC
                    modbusFrameConfig = new FrameConfig
                    {
                        Enabled = true,
                        FixedResponseLength = expectedLength
                    };
                }

                var result = await _underlyingProvider.SendAndReceiveAsync(
                    finalRequest, modbusFrameConfig, timeout, waitForResponse, cancellationToken);

                if (!result.Success || !waitForResponse)
                    return result;

                // 验证并解析响应
                return ParseModbusResponse(result, functionCode, data);
            }
            catch (Exception ex)
            {
                var r = CommunicationResult.Failed($"Modbus通讯异常: {ex.Message}");
                r.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                return r;
            }
        }

        // ── 响应解析 ─────────────────────────────────────────────────────────

        /// <summary>
        /// 解析 Modbus 响应，将寄存器值转为 ResponseString
        /// 格式："值1,值2,..." 供上层 ParseResponse 规则使用
        /// </summary>
        private CommunicationResult ParseModbusResponse(
            CommunicationResult result, byte functionCode, byte[] requestPdu)
        {
            var raw = result.RawResponse;
            if (raw == null || raw.Length == 0) return result;

            try
            {
                // TCP 响应跳过 6 字节 MBAP 头；RTU 从第 0 字节开始
                int offset = _config.ProtocolType == ProtocolType.ModbusTcp ? 6 : 0;

                // RTU：验证 CRC
                if (_config.ProtocolType == ProtocolType.ModbusRtu)
                {
                    if (!ValidateRtuCrc(raw))
                    {
                        result.Success = false;
                        result.ErrorMessage = "Modbus RTU CRC 校验失败";
                        logger?.LogWarning("RTU CRC校验失败: {Hex}", BitConverter.ToString(raw));
                        return result;
                    }
                }

                // 检查异常响应（功能码 bit7 置位）
                if (offset + 1 < raw.Length && (raw[offset + 1] & 0x80) != 0)
                {
                    var exCode = offset + 2 < raw.Length ? raw[offset + 2] : 0;
                    result.Success = false;
                    result.ErrorMessage = $"Modbus异常响应，功能码=0x{raw[offset + 1]:X2}，异常码={exCode}";
                    return result;
                }

                // 解析数据：03/04 读寄存器
                if (functionCode is 0x03 or 0x04)
                {
                    int byteCount = raw[offset + 2];
                    var values = new List<string>();
                    bool bigEndian = _config.ByteOrder == ByteOrder.BigEndian;

                    for (int i = 0; i < byteCount; i += 2)
                    {
                        if (offset + 3 + i + 1 >= raw.Length) break;
                        byte hi = raw[offset + 3 + i];
                        byte lo = raw[offset + 3 + i + 1];

                        ushort regVal = bigEndian
                            ? (ushort)((hi << 8) | lo)
                            : (ushort)((lo << 8) | hi);

                        if (_config.SwapBytes) regVal = (ushort)((regVal >> 8) | (regVal << 8));

                        values.Add(regVal.ToString());
                    }

                    // 多寄存器以逗号分隔，供 Delimiter 解析规则使用
                    result.ResponseString = string.Join(",", values);
                    logger?.LogDebug("Modbus读取 {Count} 个寄存器: {Values}",
                        values.Count, result.ResponseString);
                }
                // 06 写单寄存器：响应回显地址和值
                else if (functionCode == 0x06)
                {
                    result.ResponseString = "OK";
                    logger?.LogDebug("Modbus写入成功");
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Modbus响应解析异常");
                // 解析失败不影响原始 ResponseString
            }

            return result;
        }

        // ── 帧构建 ───────────────────────────────────────────────────────────

        /// <summary>
        /// 添加 Modbus TCP MBAP 头（6字节）
        /// </summary>
        private static byte[] AddModbusTcpHeader(byte[] pdu, ushort transactionId)
        {
            // MBAP: TransactionID(2) + ProtocolID(2) + Length(2) + UnitID 已在 pdu[0]
            var mbap = new byte[6];
            mbap[0] = (byte)(transactionId >> 8);
            mbap[1] = (byte)(transactionId & 0xFF);
            mbap[2] = 0x00; // Protocol ID
            mbap[3] = 0x00;
            ushort length = (ushort)pdu.Length;
            mbap[4] = (byte)(length >> 8);
            mbap[5] = (byte)(length & 0xFF);
            return mbap.Concat(pdu).ToArray();
        }

        /// <summary>
        /// 添加 Modbus RTU CRC16（2字节，小端）
        /// </summary>
        private static byte[] AddModbusRtuCrc(byte[] pdu)
        {
            ushort crc = CalculateCrc16(pdu);
            return pdu.Concat(new[] { (byte)(crc & 0xFF), (byte)(crc >> 8) }).ToArray();
        }

        private static bool ValidateRtuCrc(byte[] frame)
        {
            if (frame.Length < 4) return false;
            var data = frame.Take(frame.Length - 2).ToArray();
            ushort calc = CalculateCrc16(data);
            ushort recv = (ushort)(frame[^2] | (frame[^1] << 8));
            return calc == recv;
        }

        private static ushort CalculateCrc16(byte[] data)
        {
            ushort crc = 0xFFFF;
            foreach (var b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                    crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
            }
            return crc;
        }

        // ── 期望响应长度计算 ─────────────────────────────────────────────────

        private int CalculateExpectedLength(byte functionCode, byte[] requestPdu)
        {
            // requestPdu: [SlaveAddr, FunctionCode, ...]
            int tcpHeader = _config.ProtocolType == ProtocolType.ModbusTcp ? 6 : 0;
            int crcBytes = _config.ProtocolType == ProtocolType.ModbusRtu ? 2 : 0;

            return functionCode switch
            {
                // 读保持/输入寄存器：响应 = MBAP(6) + SlaveAddr(1) + FC(1) + ByteCount(1) + N*2
                0x03 or 0x04 when requestPdu.Length >= 6 =>
                    tcpHeader + 3 + (requestPdu[5] * 2) + crcBytes,

                // 写单寄存器：响应 = MBAP(6) + SlaveAddr(1) + FC(1) + Addr(2) + Value(2)
                0x06 => tcpHeader + 6 + crcBytes,

                // 写多寄存器：响应 = MBAP(6) + SlaveAddr(1) + FC(1) + Addr(2) + Count(2)
                0x10 => tcpHeader + 6 + crcBytes,

                _ => tcpHeader + 256 + crcBytes  // 未知功能码，接收缓冲上限
            };
        }

        // ── 简单转发方法 ─────────────────────────────────────────────────────

        public Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
            => _underlyingProvider?.SendAsync(data, cancellationToken) ?? Task.FromResult(false);

        public Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default)
            => _underlyingProvider?.ReceiveAsync(frameConfig, timeout, cancellationToken) ?? Task.FromResult(Array.Empty<byte>());

        public void Dispose() => _underlyingProvider?.Dispose();
    }
}
using MainUI.LogicalConfiguration.Instrument.Models;
using System.Text;

namespace MainUI.LogicalConfiguration.Instrument.Utilities
{
    /// <summary>
    /// 校验算法工具类
    /// 提供各种常用的数据校验算法实现
    /// </summary>
    public static class ChecksumHelper
    {
        /// <summary>
        /// 计算校验值
        /// </summary>
        /// <param name="data">要计算校验的数据</param>
        /// <param name="checksumType">校验算法类型</param>
        /// <param name="startIndex">计算起始位置</param>
        /// <param name="length">计算长度(-1表示到末尾)</param>
        /// <returns>校验值字节数组</returns>
        public static byte[] Calculate(byte[] data, ChecksumType checksumType, int startIndex = 0, int length = -1)
        {
            if (data == null || data.Length == 0)
                return Array.Empty<byte>();

            // 确定计算范围
            int endIndex = length < 0 ? data.Length : Math.Min(startIndex + length, data.Length);
            var segment = data.Skip(startIndex).Take(endIndex - startIndex).ToArray();

            return checksumType switch
            {
                ChecksumType.CRC16 => CalculateCRC16(segment),
                ChecksumType.CRC32 => CalculateCRC32(segment),
                ChecksumType.LRC => CalculateLRC(segment),
                ChecksumType.XOR => CalculateXOR(segment),
                ChecksumType.Checksum => CalculateChecksum(segment),
                ChecksumType.ModbusCRC => CalculateModbusCRC(segment),
                ChecksumType.None => Array.Empty<byte>(),
                _ => Array.Empty<byte>()
            };
        }

        /// <summary>
        /// 验证校验值
        /// </summary>
        /// <param name="data">包含校验值的完整数据</param>
        /// <param name="checksumType">校验算法类型</param>
        /// <param name="checksumPosition">校验值位置(-1表示末尾)</param>
        /// <param name="checksumSize">校验值字节数</param>
        /// <returns>校验是否通过</returns>
        public static bool Verify(byte[] data, ChecksumType checksumType, int checksumPosition = -1, int checksumSize = 2)
        {
            if (data == null || data.Length < checksumSize)
                return false;

            if (checksumType == ChecksumType.None)
                return true;

            // 确定校验值位置
            int csPos = checksumPosition < 0 ? data.Length - checksumSize : checksumPosition;
            if (csPos < 0 || csPos + checksumSize > data.Length)
                return false;

            // 提取数据部分和校验值
            var dataSegment = data.Take(csPos).ToArray();
            var existingChecksum = data.Skip(csPos).Take(checksumSize).ToArray();

            // 计算校验值
            var calculatedChecksum = Calculate(dataSegment, checksumType);

            // 比较
            return calculatedChecksum.Length == existingChecksum.Length &&
                   calculatedChecksum.SequenceEqual(existingChecksum);
        }

        #region CRC16

        /// <summary>
        /// CRC16-CCITT 校验
        /// </summary>
        private static byte[] CalculateCRC16(byte[] data)
        {
            ushort crc = 0xFFFF;
            ushort polynomial = 0x1021;

            foreach (byte b in data)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ polynomial);
                    else
                        crc <<= 1;
                }
            }

            return new byte[] { (byte)(crc >> 8), (byte)(crc & 0xFF) };
        }

        #endregion

        #region CRC32

        private static readonly uint[] Crc32Table = GenerateCrc32Table();

        private static uint[] GenerateCrc32Table()
        {
            uint[] table = new uint[256];
            const uint polynomial = 0xEDB88320;

            for (uint i = 0; i < 256; i++)
            {
                uint crc = i;
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) != 0)
                        crc = (crc >> 1) ^ polynomial;
                    else
                        crc >>= 1;
                }
                table[i] = crc;
            }
            return table;
        }

        /// <summary>
        /// CRC32 校验
        /// </summary>
        private static byte[] CalculateCRC32(byte[] data)
        {
            uint crc = 0xFFFFFFFF;

            foreach (byte b in data)
            {
                byte index = (byte)((crc ^ b) & 0xFF);
                crc = (crc >> 8) ^ Crc32Table[index];
            }

            crc ^= 0xFFFFFFFF;
            return BitConverter.GetBytes(crc);
        }

        #endregion

        #region LRC

        /// <summary>
        /// LRC (纵向冗余校验)
        /// </summary>
        private static byte[] CalculateLRC(byte[] data)
        {
            byte lrc = 0;
            foreach (byte b in data)
            {
                lrc += b;
            }
            lrc = (byte)(~lrc + 1); // 取补码
            return new byte[] { lrc };
        }

        #endregion

        #region XOR

        /// <summary>
        /// 异或校验
        /// </summary>
        private static byte[] CalculateXOR(byte[] data)
        {
            byte xor = 0;
            foreach (byte b in data)
            {
                xor ^= b;
            }
            return new byte[] { xor };
        }

        #endregion

        #region Checksum

        /// <summary>
        /// 累加和校验
        /// </summary>
        private static byte[] CalculateChecksum(byte[] data)
        {
            ushort sum = 0;
            foreach (byte b in data)
            {
                sum += b;
            }
            return new byte[] { (byte)(sum & 0xFF) };
        }

        #endregion

        #region Modbus CRC

        /// <summary>
        /// Modbus CRC16 校验
        /// </summary>
        private static byte[] CalculateModbusCRC(byte[] data)
        {
            ushort crc = 0xFFFF;

            foreach (byte b in data)
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

            // Modbus CRC 是低字节在前
            return new byte[] { (byte)(crc & 0xFF), (byte)(crc >> 8) };
        }

        #endregion
    }

    /// <summary>
    /// 数据转换工具类
    /// </summary>
    public static class DataConversionHelper
    {
        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        /// <param name="hex">十六进制字符串，如 "AA55" 或 "AA 55" 或 "0xAA0x55"</param>
        /// <returns>字节数组</returns>
        public static byte[] HexStringToBytes(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return Array.Empty<byte>();

            // 移除常见的分隔符和前缀
            hex = hex.Replace(" ", "")
                     .Replace("-", "")
                     .Replace("0x", "")
                     .Replace("0X", "")
                     .Replace(",", "");

            // 确保长度为偶数
            if (hex.Length % 2 != 0)
                hex = "0" + hex;

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        /// <summary>
        /// 字节数组转十六进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="separator">分隔符</param>
        /// <returns>十六进制字符串</returns>
        public static string BytesToHexString(byte[] bytes, string separator = " ")
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            return string.Join(separator, bytes.Select(b => b.ToString("X2")));
        }

        /// <summary>
        /// 根据字节序转换整数
        /// </summary>
        public static int BytesToInt32(byte[] bytes, ByteOrder byteOrder, int startIndex = 0)
        {
            if (bytes == null || bytes.Length < startIndex + 4)
                return 0;

            var segment = bytes.Skip(startIndex).Take(4).ToArray();

            if (byteOrder == ByteOrder.LittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(segment);
            }

            return BitConverter.ToInt32(segment, 0);
        }

        /// <summary>
        /// 根据字节序转换16位整数
        /// </summary>
        public static short BytesToInt16(byte[] bytes, ByteOrder byteOrder, int startIndex = 0)
        {
            if (bytes == null || bytes.Length < startIndex + 2)
                return 0;

            var segment = bytes.Skip(startIndex).Take(2).ToArray();

            if (byteOrder == ByteOrder.LittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(segment);
            }

            return BitConverter.ToInt16(segment, 0);
        }

        /// <summary>
        /// 根据字节序转换无符号16位整数
        /// </summary>
        public static ushort BytesToUInt16(byte[] bytes, ByteOrder byteOrder, int startIndex = 0)
        {
            if (bytes == null || bytes.Length < startIndex + 2)
                return 0;

            var segment = bytes.Skip(startIndex).Take(2).ToArray();

            if (byteOrder == ByteOrder.LittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(segment);
            }

            return BitConverter.ToUInt16(segment, 0);
        }

        /// <summary>
        /// 根据字节序转换浮点数
        /// </summary>
        public static float BytesToFloat(byte[] bytes, ByteOrder byteOrder, int startIndex = 0)
        {
            if (bytes == null || bytes.Length < startIndex + 4)
                return 0;

            var segment = bytes.Skip(startIndex).Take(4).ToArray();

            if (byteOrder == ByteOrder.LittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(segment);
            }

            return BitConverter.ToSingle(segment, 0);
        }

        /// <summary>
        /// 整数转字节数组
        /// </summary>
        public static byte[] Int32ToBytes(int value, ByteOrder byteOrder)
        {
            var bytes = BitConverter.GetBytes(value);

            if (byteOrder == ByteOrder.LittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// 16位整数转字节数组
        /// </summary>
        public static byte[] Int16ToBytes(short value, ByteOrder byteOrder)
        {
            var bytes = BitConverter.GetBytes(value);

            if (byteOrder == ByteOrder.LittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// 浮点数转字节数组
        /// </summary>
        public static byte[] FloatToBytes(float value, ByteOrder byteOrder)
        {
            var bytes = BitConverter.GetBytes(value);

            if (byteOrder == ByteOrder.LittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }
    }

    /// <summary>
    /// 帧构建器
    /// 用于构建符合指定格式的数据帧
    /// </summary>
    public class FrameBuilder
    {
        private readonly FrameConfig _config;
        private readonly List<byte> _data = new();

        public FrameBuilder(FrameConfig config)
        {
            _config = config ?? new FrameConfig();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        public FrameBuilder AddData(byte[] data)
        {
            if (data != null)
            {
                _data.AddRange(data);
            }
            return this;
        }

        /// <summary>
        /// 添加字符串数据
        /// </summary>
        public FrameBuilder AddString(string data, Encoding encoding = null)
        {
            if (!string.IsNullOrEmpty(data))
            {
                encoding ??= Encoding.ASCII;
                _data.AddRange(encoding.GetBytes(data));
            }
            return this;
        }

        /// <summary>
        /// 添加整数(16位)
        /// </summary>
        public FrameBuilder AddInt16(short value)
        {
            var bytes = DataConversionHelper.Int16ToBytes(value, _config.ByteOrder);
            _data.AddRange(bytes);
            return this;
        }

        /// <summary>
        /// 添加整数(32位)
        /// </summary>
        public FrameBuilder AddInt32(int value)
        {
            var bytes = DataConversionHelper.Int32ToBytes(value, _config.ByteOrder);
            _data.AddRange(bytes);
            return this;
        }

        /// <summary>
        /// 添加浮点数
        /// </summary>
        public FrameBuilder AddFloat(float value)
        {
            var bytes = DataConversionHelper.FloatToBytes(value, _config.ByteOrder);
            _data.AddRange(bytes);
            return this;
        }

        /// <summary>
        /// 构建完整的数据帧
        /// </summary>
        public byte[] Build()
        {
            var frame = new List<byte>();

            // 添加帧头
            if (!string.IsNullOrEmpty(_config.FrameHeader))
            {
                frame.AddRange(DataConversionHelper.HexStringToBytes(_config.FrameHeader));
            }

            // 添加数据
            frame.AddRange(_data);

            // 计算并添加校验值
            if (_config.ChecksumType != ChecksumType.None)
            {
                var checksum = ChecksumHelper.Calculate(
                    frame.ToArray(),
                    _config.ChecksumType,
                    _config.ChecksumStartPosition,
                    _config.ChecksumEndPosition < 0 ? -1 : _config.ChecksumEndPosition - _config.ChecksumStartPosition);
                frame.AddRange(checksum);
            }

            // 添加帧尾
            if (!string.IsNullOrEmpty(_config.FrameFooter))
            {
                frame.AddRange(DataConversionHelper.HexStringToBytes(_config.FrameFooter));
            }

            return frame.ToArray();
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public FrameBuilder Clear()
        {
            _data.Clear();
            return this;
        }
    }
}
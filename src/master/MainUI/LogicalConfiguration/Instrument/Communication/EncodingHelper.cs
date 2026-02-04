using System.Text;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// 编码辅助类 - 智能识别和解码字节数据
    /// </summary>
    public static class EncodingHelper
    {
        /// <summary>
        /// 智能解码字节数组为字符串
        /// 自动检测编码类型: UTF-8, GB2312, GBK, ASCII
        /// </summary>
        public static string SmartDecode(byte[] data)
        {
            if (data == null || data.Length == 0)
                return "";

            // 1. 检测 BOM (Byte Order Mark)
            var bomResult = TryDecodeBOM(data);
            if (bomResult != null)
                return bomResult;

            // 2. 检查是否全是纯 ASCII (0-127)
            if (IsAscii(data))
                return Encoding.ASCII.GetString(data);

            // 3. 尝试 UTF-8 解码（检查是否有效）
            var utf8Result = TryDecodeUtf8(data);
            if (utf8Result != null)
                return utf8Result;

            // 4. 尝试 GB2312/GBK 解码（中文 Windows 常用）
            var gbResult = TryDecodeGB(data);
            if (gbResult != null)
                return gbResult;

            // 5. 默认使用 UTF-8（带替换字符）
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// 检测并解码 BOM
        /// </summary>
        private static string TryDecodeBOM(byte[] data)
        {
            if (data.Length < 2)
                return null;

            // UTF-8 BOM: EF BB BF
            if (data.Length >= 3 && data[0] == 0xEF && data[1] == 0xBB && data[2] == 0xBF)
            {
                return Encoding.UTF8.GetString(data, 3, data.Length - 3);
            }

            // UTF-16 LE BOM: FF FE
            if (data[0] == 0xFF && data[1] == 0xFE)
            {
                return Encoding.Unicode.GetString(data, 2, data.Length - 2);
            }

            // UTF-16 BE BOM: FE FF
            if (data[0] == 0xFE && data[1] == 0xFF)
            {
                return Encoding.BigEndianUnicode.GetString(data, 2, data.Length - 2);
            }

            return null;
        }

        /// <summary>
        /// 检查是否全是 ASCII 字符
        /// </summary>
        private static bool IsAscii(byte[] data)
        {
            return data.All(b => b < 128);
        }

        /// <summary>
        /// 尝试 UTF-8 解码，检查是否有效
        /// </summary>
        private static string TryDecodeUtf8(byte[] data)
        {
            try
            {
                // 使用 UTF-8 解码器（不抛出异常，使用替换字符）
                var utf8 = Encoding.UTF8.GetString(data);

                // 检查是否包含替换字符 (U+FFFD)
                // 如果有替换字符，说明不是有效的 UTF-8
                if (utf8.Contains('\uFFFD'))
                    return null;

                // 额外检查：UTF-8 的有效性
                if (IsValidUtf8(data))
                    return utf8;

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 验证 UTF-8 编码的有效性
        /// </summary>
        private static bool IsValidUtf8(byte[] data)
        {
            int i = 0;
            while (i < data.Length)
            {
                byte b = data[i];

                // 单字节字符 (0xxxxxxx)
                if ((b & 0x80) == 0)
                {
                    i++;
                    continue;
                }

                // 多字节字符
                int bytesToRead = 0;
                if ((b & 0xE0) == 0xC0) bytesToRead = 1;      // 110xxxxx (2字节)
                else if ((b & 0xF0) == 0xE0) bytesToRead = 2; // 1110xxxx (3字节)
                else if ((b & 0xF8) == 0xF0) bytesToRead = 3; // 11110xxx (4字节)
                else return false; // 无效的起始字节

                // 检查后续字节
                for (int j = 0; j < bytesToRead; j++)
                {
                    i++;
                    if (i >= data.Length || (data[i] & 0xC0) != 0x80)
                        return false; // 后续字节必须是 10xxxxxx
                }

                i++;
            }

            return true;
        }

        /// <summary>
        /// 尝试 GB2312/GBK 解码（中文编码）
        /// </summary>
        private static string TryDecodeGB(byte[] data)
        {
            try
            {
                // 先尝试 GBK (兼容 GB2312，支持更多字符)
                var gbk = Encoding.GetEncoding("GBK");
                var result = gbk.GetString(data);

                // 简单验证：检查是否有过多的无效字符
                // GBK 解码通常不会产生 U+FFFD
                if (!result.Contains('\uFFFD'))
                    return result;

                return null;
            }
            catch
            {
                // GBK 不可用时，尝试 GB2312
                try
                {
                    var gb2312 = Encoding.GetEncoding("GB2312");
                    return gb2312.GetString(data);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取数据的十六进制表示（用于调试）
        /// </summary>
        public static string ToHexString(byte[] data, int maxLength = 50)
        {
            if (data == null || data.Length == 0)
                return "";

            var length = Math.Min(data.Length, maxLength);
            var hex = BitConverter.ToString(data, 0, length).Replace("-", " ");

            if (data.Length > maxLength)
                hex += "...";

            return hex;
        }

        /// <summary>
        /// 诊断编码类型（用于调试）
        /// </summary>
        public static string DiagnoseEncoding(byte[] data)
        {
            if (data == null || data.Length == 0)
                return "空数据";

            var result = new StringBuilder();
            result.AppendLine($"数据长度: {data.Length} 字节");
            result.AppendLine($"HEX: {ToHexString(data)}");
            result.AppendLine();

            // 测试各种编码
            result.AppendLine("编码尝试:");

            // ASCII
            if (IsAscii(data))
            {
                result.AppendLine($"✓ ASCII: {Encoding.ASCII.GetString(data)}");
            }
            else
            {
                result.AppendLine("✗ ASCII: 包含非ASCII字符");
            }

            // UTF-8
            var utf8 = TryDecodeUtf8(data);
            if (utf8 != null)
            {
                result.AppendLine($"✓ UTF-8: {utf8}");
            }
            else
            {
                result.AppendLine($"✗ UTF-8: {Encoding.UTF8.GetString(data)} (含替换字符)");
            }

            // GB2312/GBK
            var gb = TryDecodeGB(data);
            if (gb != null)
            {
                result.AppendLine($"✓ GB2312/GBK: {gb}");
            }
            else
            {
                result.AppendLine("✗ GB2312/GBK: 解码失败");
            }

            // 推荐
            result.AppendLine();
            result.AppendLine($"推荐使用: {GetRecommendedEncoding(data)}");
            result.AppendLine($"解码结果: {SmartDecode(data)}");

            return result.ToString();
        }

        /// <summary>
        /// 获取推荐的编码名称
        /// </summary>
        private static string GetRecommendedEncoding(byte[] data)
        {
            if (IsAscii(data))
                return "ASCII";
            if (TryDecodeUtf8(data) != null)
                return "UTF-8";

            return TryDecodeGB(data) != null ? "GB2312/GBK" : "UTF-8 (默认)";
        }
    }
}
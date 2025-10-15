using System.Text;

namespace MainUI.CurrencyHelper
{
    /// <summary>
    /// 网络路径帮助类 - 用于跨工位访问共享文件
    /// </summary>
    public static class NetworkPathHelper
    {
        /// <summary>
        /// 根据工位信息和本地路径构建网络访问路径
        /// </summary>
        /// <param name="localPath">本地路径 (例如: D:\试验报告\2025\10\产品类型\文件.xls)</param>
        /// <param name="targetTestBench">目标工位信息</param>
        /// <param name="currentTestBench">当前工位信息</param>
        /// <returns>可访问的路径（本地或网络UNC路径）</returns>
        public static string ConvertToAccessiblePath(string localPath, TestBenchModel targetTestBench, TestBenchModel currentTestBench)
        {
            try
            {
                // 如果是当前工位的文件，直接返回本地路径
                if (targetTestBench.ID == currentTestBench.ID)
                {
                    return localPath;
                }

                // 如果目标工位没有配置IP，返回原路径并记录警告
                if (string.IsNullOrEmpty(targetTestBench.BenchIP))
                {
                    NlogHelper.Default.Warn($"目标工位 [{targetTestBench.BenchName}] 未配置IP地址");
                    return localPath;
                }

                // 构建 UNC 路径
                string uncPath = ConvertLocalPathToUNC(localPath, targetTestBench.BenchIP);

                NlogHelper.Default.Info($"转换路径: {localPath} -> {uncPath}");

                return uncPath;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"转换网络路径失败: {ex.Message}", ex);
                return localPath; // 失败时返回原路径
            }
        }

        /// <summary>
        /// 将本地路径转换为UNC路径
        /// 例如: D:\试验报告\2025\10\产品类型\文件.xls 
        ///   -> \\192.168.1.100\试验报告\2025\10\产品类型\文件.xls
        /// </summary>
        /// <param name="localPath">本地路径</param>
        /// <param name="targetIP">目标工位IP</param>
        /// <returns>UNC路径</returns>
        private static string ConvertLocalPathToUNC(string localPath, string targetIP)
        {
            try
            {
                // 提取盘符和路径
                // 例如: D:\试验报告\... -> 盘符: D, 相对路径: 试验报告\...

                if (!Path.IsPathRooted(localPath))
                {
                    return localPath; // 不是完整路径，直接返回
                }

                string driveLetter = Path.GetPathRoot(localPath); // 获取 "D:\"
                string relativePath = localPath.Substring(driveLetter.Length); // 获取 "试验报告\2025\..."

                // 获取共享名称（默认使用路径的第一级目录名）
                string shareName = GetShareName(relativePath);

                // 构建 UNC 路径: \\IP\共享名\剩余路径
                string remainingPath = GetRemainingPath(relativePath);
                string uncPath = $"\\\\{targetIP}\\{shareName}";

                if (!string.IsNullOrEmpty(remainingPath))
                {
                    uncPath = Path.Combine(uncPath, remainingPath);
                }

                return uncPath;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"转换UNC路径失败: {ex.Message}", ex);
                return localPath;
            }
        }

        /// <summary>
        /// 从相对路径中提取共享名称（第一级目录）
        /// 例如: 试验报告\2025\10\... -> 试验报告
        /// </summary>
        private static string GetShareName(string relativePath)
        {
            var parts = relativePath.Split(new[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[0] : "试验报告";
        }

        /// <summary>
        /// 获取除共享名称外的剩余路径
        /// 例如: 试验报告\2025\10\产品类型\文件.xls -> 2025\10\产品类型\文件.xls
        /// </summary>
        private static string GetRemainingPath(string relativePath)
        {
            var parts = relativePath.Split(['\\', '/'], StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length <= 1)
            {
                return string.Empty;
            }

            // 跳过第一个部分（共享名），组合剩余部分
            return string.Join("\\", parts.Skip(1));
        }

        /// <summary>
        /// 检查网络路径是否可访问
        /// </summary>
        /// <param name="uncPath">UNC路径</param>
        /// <param name="timeoutMs">超时时间（毫秒），默认3000ms</param>
        /// <returns>是否可访问</returns>
        public static bool IsNetworkPathAccessible(string uncPath, int timeoutMs = 3000)
        {
            try
            {
                // 使用Task超时控制
                var task = Task.Run(() => File.Exists(uncPath));

                if (task.Wait(timeoutMs))
                {
                    return task.Result;
                }
                else
                {
                    NlogHelper.Default.Warn($"检查网络路径超时: {uncPath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"检查网络路径失败: {uncPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 尝试打开网络文件，如果失败则提供友好的错误提示
        /// </summary>
        /// <param name="filePath">文件路径（可以是本地或UNC路径）</param>
        /// <returns>(是否成功, 错误消息)</returns>
        public static (bool Success, string Message) TryAccessFile(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return (false, "文件路径为空");
                }

                // 判断是否为网络路径
                bool isNetworkPath = filePath.StartsWith("\\\\");

                if (isNetworkPath)
                {
                    // 网络路径，先检查可访问性
                    if (!IsNetworkPathAccessible(filePath))
                    {
                        return (false, BuildNetworkErrorMessage(filePath));
                    }
                }
                else
                {
                    // 本地路径
                    if (!File.Exists(filePath))
                    {
                        return (false, "报表文件不存在或已被删除");
                    }
                }

                return (true, "文件可访问");
            }
            catch (UnauthorizedAccessException)
            {
                return (false, "没有访问权限，请检查网络共享权限设置");
            }
            catch (IOException ex)
            {
                return (false, $"文件访问错误: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"未知错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 构建网络访问失败的详细错误消息
        /// </summary>
        private static string BuildNetworkErrorMessage(string uncPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("无法访问网络文件，可能的原因：");
            sb.AppendLine("1. 目标工位未开机或网络连接异常");
            sb.AppendLine("2. 目标工位未设置文件夹共享");
            sb.AppendLine("3. 网络共享权限不足");
            sb.AppendLine("4. 防火墙阻止了文件共享");
            sb.AppendLine();
            sb.AppendLine($"访问路径: {uncPath}");
            sb.AppendLine();
            sb.AppendLine("解决方法：");
            sb.AppendLine("1. 确认目标工位已开机且网络正常");
            sb.AppendLine("2. 在目标工位上右键 D:\\试验报告 文件夹");
            sb.AppendLine("3. 选择 属性 -> 共享 -> 高级共享");
            sb.AppendLine("4. 勾选 共享此文件夹，共享名设为: 试验报告");
            sb.AppendLine("5. 点击 权限，添加 Everyone 用户，赋予读取权限");

            return sb.ToString();
        }

        /// <summary>
        /// 从UNC路径提取目标IP
        /// 例如: \\192.168.1.100\试验报告\... -> 192.168.1.100
        /// </summary>
        public static string ExtractIPFromUNC(string uncPath)
        {
            try
            {
                if (!uncPath.StartsWith("\\\\"))
                {
                    return null;
                }

                var parts = uncPath.Substring(2).Split('\\');
                return parts.Length > 0 ? parts[0] : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
namespace MainUI.Service
{
    /// <summary>
    /// 系统进程管理帮助类
    /// </summary>
    public static class ProcessHelper
    {
        /// <summary>
        /// 强制终止指定进程
        /// </summary>
        /// <param name="processName">进程名（不含.exe）</param>
        /// <param name="timeoutMs">超时时间（毫秒）</param>
        /// <returns>是否成功终止</returns>
        public static bool KillProcess(string processName, int timeoutMs = 5000)
        {
            try
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    NlogHelper.Default.Info($"未找到进程：{processName}");
                    return true; // 进程不存在，认为成功
                }

                bool allKilled = true;
                foreach (var process in processes)
                {
                    try
                    {
                        if (!process.HasExited)
                        {
                            // 先尝试优雅关闭
                            if (!TryGracefulClose(process, timeoutMs / 2))
                            {
                                // 优雅关闭失败，强制终止
                                ForceKillProcess(process);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"终止进程失败 {processName} (PID: {process.Id})", ex);
                        allKilled = false;
                    }
                    finally
                    {
                        process.Dispose();
                    }
                }

                return allKilled;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"终止进程操作失败：{processName}", ex);
                return false;
            }
        }

        /// <summary>
        /// 尝试优雅关闭进程
        /// </summary>
        /// <param name="process">进程对象</param>
        /// <param name="timeoutMs">超时时间</param>
        /// <returns>是否成功关闭</returns>
        private static bool TryGracefulClose(Process process, int timeoutMs)
        {
            try
            {
                // 尝试发送关闭消息
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    process.CloseMainWindow();
                    return process.WaitForExit(timeoutMs);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 强制终止进程
        /// </summary>
        /// <param name="process">进程对象</param>
        private static void ForceKillProcess(Process process)
        {
            try
            {
                if (!process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit(2000); // 等待2秒确保进程完全退出
                    NlogHelper.Default.Info($"强制终止进程：{process.ProcessName} (PID: {process.Id})");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"强制终止进程失败：{process.ProcessName}", ex);
                // 如果还是杀不掉，尝试使用 taskkill 命令
                TryTaskKill(process.Id);
            }
        }

        /// <summary>
        /// 使用 taskkill 命令强制终止进程
        /// </summary>
        /// <param name="processId">进程ID</param>
        private static void TryTaskKill(int processId)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = $"/F /PID {processId}",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var taskKillProcess = Process.Start(startInfo);
                taskKillProcess?.WaitForExit(3000);

                NlogHelper.Default.Info($"使用 taskkill 终止进程 PID: {processId}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"taskkill 命令执行失败，PID: {processId}", ex);
            }
        }

        /// <summary>
        /// 异步终止进程
        /// </summary>
        /// <param name="processName">进程名</param>
        /// <param name="timeoutMs">超时时间</param>
        /// <returns>是否成功终止</returns>
        public static async Task<bool> KillProcessAsync(string processName, int timeoutMs = 5000)
        {
            return await Task.Run(() => KillProcess(processName, timeoutMs));
        }

        /// <summary>
        /// 检查进程是否正在运行
        /// </summary>
        /// <param name="processName">进程名</param>
        /// <returns>是否正在运行</returns>
        public static bool IsProcessRunning(string processName)
        {
            try
            {
                var processes = Process.GetProcessesByName(processName);
                return processes.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 等待进程完全退出
        /// </summary>
        /// <param name="processName">进程名</param>
        /// <param name="maxWaitMs">最大等待时间</param>
        /// <returns>是否成功退出</returns>
        public static async Task<bool> WaitForProcessExit(string processName, int maxWaitMs = 10000)
        {
            var startTime = DateTime.Now;
            while (IsProcessRunning(processName))
            {
                if ((DateTime.Now - startTime).TotalMilliseconds > maxWaitMs)
                {
                    return false;
                }
                await Task.Delay(100);
            }
            return true;
        }
    }
}
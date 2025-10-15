using System.Management;

namespace MainUI.Service
{
    /// <summary>
    /// 处理系统正常运行时间和应用程序运行时间的时间跟踪
    /// </summary>
    public class TimeTrackingService
    {
        private readonly Stopwatch _applicationRuntime;
        private DateTime _systemBootTime;

        public TimeTrackingService()
        {
            _applicationRuntime = Stopwatch.StartNew();
            GetSystemBootTime();
        }

        // 获取系统启动时间
        private void GetSystemBootTime()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT LastBootUpTime FROM Win32_OperatingSystem");
                foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                {
                    var bootTime = ManagementDateTimeConverter.ToDateTime(obj["LastBootUpTime"].ToString());
                    _systemBootTime = bootTime.ToLocalTime();
                    break;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取系统启动时间失败", ex);
                _systemBootTime = DateTime.Now; // 如果获取失败，使用当前时间
            }
        }

        // 获取系统运行时间
        public TimeSpan GetSystemUptime()
        {
            return DateTime.Now - _systemBootTime;
        }

        // 获取系统开机时间
        public DateTime GetSystemOnTime()
        {
            return _systemBootTime;
        }

        // 获取应用程序运行时间
        public TimeSpan GetApplicationUptime()
        {
            return _applicationRuntime.Elapsed;
        }

        // 格式化时间显示
        public static string FormatTimeSpan(TimeSpan span)
        {
            return $"{(int)span.TotalHours:D2}:{span.Minutes:D2}:{span.Seconds:D2}";
        }
    }
}
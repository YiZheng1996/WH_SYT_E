using System.Management;

namespace MainUI.Service
{
    /// <summary>
    /// ����ϵͳ��������ʱ���Ӧ�ó�������ʱ���ʱ�����
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

        // ��ȡϵͳ����ʱ��
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
                NlogHelper.Default.Error("��ȡϵͳ����ʱ��ʧ��", ex);
                _systemBootTime = DateTime.Now; // �����ȡʧ�ܣ�ʹ�õ�ǰʱ��
            }
        }

        // ��ȡϵͳ����ʱ��
        public TimeSpan GetSystemUptime()
        {
            return DateTime.Now - _systemBootTime;
        }

        // ��ȡϵͳ����ʱ��
        public DateTime GetSystemOnTime()
        {
            return _systemBootTime;
        }

        // ��ȡӦ�ó�������ʱ��
        public TimeSpan GetApplicationUptime()
        {
            return _applicationRuntime.Elapsed;
        }

        // ��ʽ��ʱ����ʾ
        public static string FormatTimeSpan(TimeSpan span)
        {
            return $"{(int)span.TotalHours:D2}:{span.Minutes:D2}:{span.Seconds:D2}";
        }
    }
}
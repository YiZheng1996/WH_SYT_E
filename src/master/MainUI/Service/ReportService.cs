using RW.UI.Controls.Report;

namespace MainUI.Service
{
    /// <summary>
    /// ���������
    /// �ṩ������ص�ҵ���߼�����,��������ؼ������ļ���������ҳ�ȹ���
    /// </summary>
    public class ReportService
    {
        #region ˽���ֶ�

        private readonly string _reportPath;
        private readonly RWReport _report;
        private int _currentRows = 1;
        private const int MaxRows = 1000;

        // ===== ����: ����ؼ��������� =====
        private static RWReport _sharedReportControl;
        private static readonly object _lock = new object();

        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="reportPath">����·��</param>
        /// <param name="report">����ؼ�</param>
        public ReportService(string reportPath, RWReport report)
        {
            _reportPath = reportPath ?? throw new ArgumentNullException(nameof(reportPath));
            _report = report ?? throw new ArgumentNullException(nameof(report));

            // ע�ᱨ��ؼ�������ʵ��
            RegisterReportControl(report);
        }

        #endregion

        #region ===== ��������: ����ؼ��������� =====

        /// <summary>
        /// ע�ᱨ��ؼ�������ʵ��
        /// </summary>
        /// <param name="report">����ؼ�ʵ��</param>
        public static void RegisterReportControl(RWReport report)
        {
            lock (_lock)
            {
                if (_sharedReportControl != report)
                {
                    _sharedReportControl = report;

                    // ͬʱ���� BaseTest.Report (����������)
                    BaseTest.Report = report;

                    NlogHelper.Default.Info($"����ؼ���ע�ᵽȫ�ַ��� (ʵ��: {report?.GetHashCode()})");
                }
            }
        }

        /// <summary>
        /// ��ȡ����ı���ؼ�ʵ��
        /// </summary>
        /// <returns>����ؼ�ʵ��</returns>
        /// <exception cref="InvalidOperationException">����ؼ�δ��ʼ��ʱ�׳�</exception>
        public static RWReport GetReportControl()
        {
            lock (_lock)
            {
                if (_sharedReportControl == null || _sharedReportControl.IsDisposed)
                {
                    throw new InvalidOperationException(
                        "����ؼ�δ��ʼ��,��ȷ��:\n" +
                        "1. ����HMI������ر���\n" +
                        "2. UcHMI.Init() ����ȷִ��\n" +
                        "3. �����ļ�·����ȷ");
                }

                return _sharedReportControl;
            }
        }

        /// <summary>
        /// ��鱨��ؼ��Ƿ����
        /// </summary>
        public static bool IsReportAvailable
        {
            get
            {
                lock (_lock)
                {
                    return _sharedReportControl != null && !_sharedReportControl.IsDisposed;
                }
            }
        }

        /// <summary>
        /// ��UI�̰߳�ȫִ�б������(�з���ֵ)
        /// </summary>
        /// <typeparam name="T">����ֵ����</typeparam>
        /// <param name="action">Ҫִ�еĲ���</param>
        /// <returns>�������</returns>
        public static T InvokeOnReportControl<T>(Func<RWReport, T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var report = GetReportControl();

            if (report.InvokeRequired)
            {
                return (T)report.Invoke(action, report);
            }
            else
            {
                return action(report);
            }
        }

        /// <summary>
        /// ��UI�̰߳�ȫִ�б������(�޷���ֵ)
        /// </summary>
        /// <param name="action">Ҫִ�еĲ���</param>
        public static void InvokeOnReportControl(Action<RWReport> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var report = GetReportControl();

            if (report.InvokeRequired)
            {
                report.Invoke(action, report);
            }
            else
            {
                action(report);
            }
        }

        /// <summary>
        /// �������ı���ؼ�����
        /// (ͨ����Ӧ�ó���رջ�����ʱ����)
        /// </summary>
        public static void ClearReportControl()
        {
            lock (_lock)
            {
                _sharedReportControl = null;
                BaseTest.Report = null;
                NlogHelper.Default.Info("����ؼ����������");
            }
        }

        #endregion

        #region ===== ԭ�й��ܱ��ֲ��� =====

        /// <summary>
        /// ��ȡĬ�ϱ���·��
        /// </summary>
        public static string GetDefaultReportPath()
        {
            return Path.Combine(Application.StartupPath, "reports\\");
        }

        /// <summary>
        /// ��ȡ��������·��
        /// </summary>
        public static string GetWorkingReportPath()
        {
            return Path.Combine(GetDefaultReportPath(), "report.xls");
        }

        /// <summary>
        /// ���������ļ�·��
        /// </summary>
        public static string BuildSaveFilePath(string modelName)
        {
            string savePath = Path.Combine(GetDefaultReportPath(),
                DateTime.Now.ToString("yyyy"),
                DateTime.Now.ToString("MM"));

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            string fileName = $"{modelName}_{DateTime.Now:yyyyMMddHHmmss}.xls";
            return Path.Combine(savePath, fileName);
        }

        /// <summary>
        /// ����ļ��Ƿ����
        /// </summary>
        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        /// <summary>
        /// ���Ʊ����ļ�
        /// </summary>
        public void CopyReportFile(string sourceFile, string destFile)
        {
            try
            {
                if (File.Exists(destFile))
                {
                    File.Delete(destFile);
                }

                File.Copy(sourceFile, destFile, true);
                NlogHelper.Default.Info($"�����ļ��Ѹ���: {sourceFile} -> {destFile}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"���Ʊ����ļ�ʧ��: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        public static bool SaveTestRecord(TestRecordModel record)
        {
            try
            {
                if (record == null)
                    throw new ArgumentNullException(nameof(record));

                var result = VarHelper.fsql.Insert(record).ExecuteAffrows();

                if (result > 0)
                {
                    NlogHelper.Default.Info($"�����¼����ɹ�: {record.TestID}");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"���������¼ʧ��: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// ���Ϸ�ҳ
        /// </summary>
        public (int currentRows, bool upEnabled, bool downEnabled) PageUp(int pageSize)
        {
            _currentRows -= pageSize;
            if (_currentRows < 1)
            {
                _currentRows = 1;
            }

            _report?.ScrollIndex(_currentRows);

            return (_currentRows, _currentRows > 1, _currentRows < MaxRows);
        }

        /// <summary>
        /// ���·�ҳ
        /// </summary>
        public (int currentRows, bool upEnabled, bool downEnabled) PageDown(int pageSize)
        {
            _currentRows += pageSize;
            if (_currentRows > MaxRows)
            {
                _currentRows = 1; // ѭ������һҳ
            }

            _report?.ScrollIndex(_currentRows);

            return (_currentRows, _currentRows > 1, _currentRows < MaxRows);
        }

        #endregion
    }

    static class Constants
    {
        public const string ReportsPath = @"reports\report.xls";
    }
}
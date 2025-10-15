namespace MainUI.Service
{
    /// <summary>
    /// ��������� - �������ء�����ͷ�ҳ����
    /// </summary>
    public class ReportService(string reportsPath, RW.UI.Controls.Report.RWReport report = null)
    {
        // ��ǰ����
        public int CurrentRows { get; private set; } = 1;
        // ���������Ĭ��Ϊ1000��
        public int MaxRows { get; set; } = 1000;

        /// <summary>
        /// ��ȡ�����ļ�����·��
        /// </summary>
        /// <param name="fileName">�����ļ���</param>
        /// <returns>����·��</returns>
        public string GetReportFilePath(string fileName)
        {
            return Path.Combine(reportsPath, fileName);
        }

        /// <summary>
        /// ��ȡĬ�ϱ���·��
        /// </summary>
        /// <returns>��������·��</returns>
        public static string GetDefaultReportPath()
        {
            return Path.Combine(Application.StartupPath, "reports\\");
        }

        /// <summary>
        /// ��ȡ��������·��
        /// </summary>
        /// <returns>��������·��</returns>
        public static string GetWorkingReportPath()
        {
            return Path.Combine(Application.StartupPath, "reports", "report.xls");
        }

        /// <summary>
        /// ��֤�����ļ��Ƿ����
        /// </summary>
        /// <param name="fileName">�����ļ���</param>
        /// <returns>�Ƿ����</returns>
        public bool FileExists(string fileName)
        {
            string filePath = GetReportFilePath(fileName);
            return File.Exists(filePath);
        }

        /// <summary>
        /// ���Ʊ����ļ�������Ŀ¼
        /// </summary>
        /// <param name="fileName">Դ�ļ���</param>
        /// <param name="targetPath">Ŀ��·��</param>
        public void CopyReportFile(string fileName, string targetPath)
        {
            string sourceFile = GetReportFilePath(fileName);
            File.Copy(sourceFile, targetPath, true);
        }

        /// <summary>
        /// ������Լ�¼
        /// </summary>
        /// <param name="testRecord">���Լ�¼</param>
        /// <returns>�Ƿ񱣴�ɹ�</returns>
        public static bool SaveTestRecord(TestRecordModel testRecord)
        {
            try
            {
                TestRecordNewBLL testRecordBLL = new();
                return testRecordBLL.SaveTestRecord(testRecord);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("������Լ�¼ʧ��", ex);
                return false;
            }
        }

        /// <summary>
        /// �����������ļ�·��
        /// ·���ṹ: D:\���鱨��\{���}\{�·�}\{��Ʒ����}\{��Ʒ�ͺ�}_{yyyyMMddHHmmss}.xls
        /// </summary>
        /// <param name="modelName">��Ʒ�ͺ�����</param>
        /// <returns>�������ļ�����·��</returns>
        public static string BuildSaveFilePath(string modelName)
        {
            try
            {
                // ���������ļ��еĻ���·��
                SaveReportConfig saveConfig = new();
                saveConfig.Load();

                // ��ȡ����·��,�������Ϊ����ʹ��Ĭ�ϵ� D:\���鱨��
                string basePath = string.IsNullOrEmpty(saveConfig.RptSaveFile)
                    ? @"D:\���鱨��"
                    : saveConfig.RptSaveFile;

                // ��ȡ��ǰʱ��
                DateTime now = DateTime.Now;

                // ��ȡ��Ʒ��������
                string modelTypeName = VarHelper.TestViewModel?.ModelTypeName ?? "δ֪����";

                // �����ļ��������еķǷ��ַ�
                modelTypeName = CleanFolderName(modelTypeName);
                modelName = CleanFileName(modelName);

                // ����Ŀ¼�ṹ: ����·��\���\�·�\��Ʒ����
                string yearFolder = now.Year.ToString();
                string monthFolder = now.Month.ToString("D2"); // ��λ���·�,�� 01, 02, ..., 12

                string directoryPath = Path.Combine(basePath, yearFolder, monthFolder, modelTypeName);

                // ȷ��Ŀ¼����,����������򴴽�
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    NlogHelper.Default.Info($"����������Ŀ¼: {directoryPath}");
                }

                // �����ļ���: ��Ʒ�ͺ�_yyyyMMddHHmmss.xls
                string timestamp = now.ToString("yyyyMMddHHmmss");
                string fileName = $"{modelName}_{timestamp}.xls";

                // �������·��
                string fullPath = Path.Combine(directoryPath, fileName);

                NlogHelper.Default.Info($"������·��: {fullPath}");

                return fullPath;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("����������·��ʧ��", ex);

                // �������,����һ��Ӧ��·��
                string emergencyPath = Path.Combine(
                    @"D:\���鱨��",
                    DateTime.Now.ToString("yyyyMMdd"),
                    $"{modelName}_{DateTime.Now:yyyyMMddHHmmss}.xls"
                );

                // ȷ��Ӧ��Ŀ¼����
                string emergencyDir = Path.GetDirectoryName(emergencyPath);
                if (!Directory.Exists(emergencyDir))
                {
                    Directory.CreateDirectory(emergencyDir);
                }

                return emergencyPath;
            }
        }

        /// <summary>
        /// �����ļ��������еķǷ��ַ�
        /// </summary>
        /// <param name="folderName">�ļ�������</param>
        /// <returns>�������ļ�������</returns>
        private static string CleanFolderName(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                return "Ĭ��";

            // Windows�ļ������Ʋ�������ַ�
            char[] invalidChars = Path.GetInvalidFileNameChars();

            foreach (char c in invalidChars)
            {
                folderName = folderName.Replace(c, '_');
            }

            // �Ƴ�һЩ�����ַ�
            folderName = folderName.Replace('/', '_')
                                   .Replace('\\', '_')
                                   .Replace(':', '_')
                                   .Replace('*', '_')
                                   .Replace('?', '_')
                                   .Replace('"', '_')
                                   .Replace('<', '_')
                                   .Replace('>', '_')
                                   .Replace('|', '_');

            return folderName.Trim();
        }

        /// <summary>
        /// �����ļ����еķǷ��ַ�
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <returns>�������ļ���</returns>
        private static string CleanFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "δ����";

            // Windows�ļ�����������ַ�
            char[] invalidChars = Path.GetInvalidFileNameChars();

            foreach (char c in invalidChars)
            {
                fileName = fileName.Replace(c, '_');
            }

            return fileName.Trim();
        }

        #region ����ҳ����

        /// <summary>
        /// ���Ϸ�ҳ
        /// </summary>
        /// <param name="pageSize">��ҳ����</param>
        /// <returns>��ҳ����������Ͱ�ť״̬</returns>
        public (int currentRows, bool upEnabled, bool downEnabled) PageUp(int pageSize)
        {
            CurrentRows -= pageSize;

            if (CurrentRows < 1)
            {
                CurrentRows = 1;
            }

            // ִ�б������
            report?.ScrollIndex(CurrentRows);

            return GetPageButtonStates();
        }

        /// <summary>
        /// ���·�ҳ
        /// </summary>
        /// <param name="pageSize">��ҳ����</param>
        /// <returns>��ҳ����������Ͱ�ť״̬</returns>
        public (int currentRows, bool upEnabled, bool downEnabled) PageDown(int pageSize)
        {
            CurrentRows += pageSize;

            if (CurrentRows > MaxRows)
            {
                CurrentRows = 1; // ѭ������һҳ
            }

            // ִ�б������
            report?.ScrollIndex(CurrentRows);

            return GetPageButtonStates();
        }

        /// <summary>
        /// ��ת��ָ����
        /// </summary>
        /// <param name="targetRow">Ŀ����</param>
        /// <returns>��ת����������Ͱ�ť״̬</returns>
        public (int currentRows, bool upEnabled, bool downEnabled) ScrollToRow(int targetRow)
        {
            if (targetRow < 1) targetRow = 1;
            if (targetRow > MaxRows) targetRow = MaxRows;

            CurrentRows = targetRow;
            report?.ScrollIndex(CurrentRows);

            return GetPageButtonStates();
        }

        /// <summary>
        /// ���õ���һҳ
        /// </summary>
        /// <returns>���ú���������Ͱ�ť״̬</returns>
        public (int currentRows, bool upEnabled, bool downEnabled) ResetToFirstPage()
        {
            CurrentRows = 1;
            report?.ScrollIndex(CurrentRows);
            return GetPageButtonStates();
        }

        /// <summary>
        /// ��ȡ��ҳ��ť������״̬
        /// </summary>
        /// <returns>�Ϸ����·���ť������״̬</returns>
        private (int currentRows, bool upEnabled, bool downEnabled) GetPageButtonStates()
        {
            bool upEnabled = CurrentRows > 1;
            bool downEnabled = CurrentRows < MaxRows;

            return (CurrentRows, upEnabled, downEnabled);
        }

        #endregion
    }

    static class Constants
    {
        public const string ReportsPath = @"reports\report.xls";
    }
}
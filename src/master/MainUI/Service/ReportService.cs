namespace MainUI.Service
{
    /// <summary>
    /// 报表服务类 - 包含加载、保存和翻页功能
    /// </summary>
    public class ReportService(string reportsPath, RW.UI.Controls.Report.RWReport report = null)
    {
        // 当前行数
        public int CurrentRows { get; private set; } = 1;
        // 最大行数，默认为1000行
        public int MaxRows { get; set; } = 1000;

        /// <summary>
        /// 获取报表文件完整路径
        /// </summary>
        /// <param name="fileName">报表文件名</param>
        /// <returns>完整路径</returns>
        public string GetReportFilePath(string fileName)
        {
            return Path.Combine(reportsPath, fileName);
        }

        /// <summary>
        /// 获取默认报表路径
        /// </summary>
        /// <returns>工作报表路径</returns>
        public static string GetDefaultReportPath()
        {
            return Path.Combine(Application.StartupPath, "reports\\");
        }

        /// <summary>
        /// 获取工作报表路径
        /// </summary>
        /// <returns>工作报表路径</returns>
        public static string GetWorkingReportPath()
        {
            return Path.Combine(Application.StartupPath, "reports", "report.xls");
        }

        /// <summary>
        /// 验证报表文件是否存在
        /// </summary>
        /// <param name="fileName">报表文件名</param>
        /// <returns>是否存在</returns>
        public bool FileExists(string fileName)
        {
            string filePath = GetReportFilePath(fileName);
            return File.Exists(filePath);
        }

        /// <summary>
        /// 复制报表文件到工作目录
        /// </summary>
        /// <param name="fileName">源文件名</param>
        /// <param name="targetPath">目标路径</param>
        public void CopyReportFile(string fileName, string targetPath)
        {
            string sourceFile = GetReportFilePath(fileName);
            File.Copy(sourceFile, targetPath, true);
        }

        /// <summary>
        /// 保存测试记录
        /// </summary>
        /// <param name="testRecord">测试记录</param>
        /// <returns>是否保存成功</returns>
        public static bool SaveTestRecord(TestRecordModel testRecord)
        {
            try
            {
                TestRecordNewBLL testRecordBLL = new();
                return testRecordBLL.SaveTestRecord(testRecord);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存测试记录失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 构建报表保存文件路径
        /// 路径结构: D:\试验报告\{年份}\{月份}\{产品类型}\{产品型号}_{yyyyMMddHHmmss}.xls
        /// </summary>
        /// <param name="modelName">产品型号名称</param>
        /// <returns>完整的文件保存路径</returns>
        public static string BuildSaveFilePath(string modelName)
        {
            try
            {
                // 加载配置文件中的基础路径
                SaveReportConfig saveConfig = new();
                saveConfig.Load();

                // 获取基础路径,如果配置为空则使用默认的 D:\试验报告
                string basePath = string.IsNullOrEmpty(saveConfig.RptSaveFile)
                    ? @"D:\试验报告"
                    : saveConfig.RptSaveFile;

                // 获取当前时间
                DateTime now = DateTime.Now;

                // 获取产品类型名称
                string modelTypeName = VarHelper.TestViewModel?.ModelTypeName ?? "未知类型";

                // 清理文件夹名称中的非法字符
                modelTypeName = CleanFolderName(modelTypeName);
                modelName = CleanFileName(modelName);

                // 构建目录结构: 基础路径\年份\月份\产品类型
                string yearFolder = now.Year.ToString();
                string monthFolder = now.Month.ToString("D2"); // 两位数月份,如 01, 02, ..., 12

                string directoryPath = Path.Combine(basePath, yearFolder, monthFolder, modelTypeName);

                // 确保目录存在,如果不存在则创建
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    NlogHelper.Default.Info($"创建报表保存目录: {directoryPath}");
                }

                // 构建文件名: 产品型号_yyyyMMddHHmmss.xls
                string timestamp = now.ToString("yyyyMMddHHmmss");
                string fileName = $"{modelName}_{timestamp}.xls";

                // 组合完整路径
                string fullPath = Path.Combine(directoryPath, fileName);

                NlogHelper.Default.Info($"报表保存路径: {fullPath}");

                return fullPath;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("构建报表保存路径失败", ex);

                // 如果出错,返回一个应急路径
                string emergencyPath = Path.Combine(
                    @"D:\试验报告",
                    DateTime.Now.ToString("yyyyMMdd"),
                    $"{modelName}_{DateTime.Now:yyyyMMddHHmmss}.xls"
                );

                // 确保应急目录存在
                string emergencyDir = Path.GetDirectoryName(emergencyPath);
                if (!Directory.Exists(emergencyDir))
                {
                    Directory.CreateDirectory(emergencyDir);
                }

                return emergencyPath;
            }
        }

        /// <summary>
        /// 清理文件夹名称中的非法字符
        /// </summary>
        /// <param name="folderName">文件夹名称</param>
        /// <returns>清理后的文件夹名称</returns>
        private static string CleanFolderName(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                return "默认";

            // Windows文件夹名称不允许的字符
            char[] invalidChars = Path.GetInvalidFileNameChars();

            foreach (char c in invalidChars)
            {
                folderName = folderName.Replace(c, '_');
            }

            // 移除一些特殊字符
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
        /// 清理文件名中的非法字符
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>清理后的文件名</returns>
        private static string CleanFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "未命名";

            // Windows文件名不允许的字符
            char[] invalidChars = Path.GetInvalidFileNameChars();

            foreach (char c in invalidChars)
            {
                fileName = fileName.Replace(c, '_');
            }

            return fileName.Trim();
        }

        #region 报表翻页功能

        /// <summary>
        /// 向上翻页
        /// </summary>
        /// <param name="pageSize">翻页行数</param>
        /// <returns>翻页后的行索引和按钮状态</returns>
        public (int currentRows, bool upEnabled, bool downEnabled) PageUp(int pageSize)
        {
            CurrentRows -= pageSize;

            if (CurrentRows < 1)
            {
                CurrentRows = 1;
            }

            // 执行报表滚动
            report?.ScrollIndex(CurrentRows);

            return GetPageButtonStates();
        }

        /// <summary>
        /// 向下翻页
        /// </summary>
        /// <param name="pageSize">翻页行数</param>
        /// <returns>翻页后的行索引和按钮状态</returns>
        public (int currentRows, bool upEnabled, bool downEnabled) PageDown(int pageSize)
        {
            CurrentRows += pageSize;

            if (CurrentRows > MaxRows)
            {
                CurrentRows = 1; // 循环到第一页
            }

            // 执行报表滚动
            report?.ScrollIndex(CurrentRows);

            return GetPageButtonStates();
        }

        /// <summary>
        /// 跳转到指定行
        /// </summary>
        /// <param name="targetRow">目标行</param>
        /// <returns>跳转后的行索引和按钮状态</returns>
        public (int currentRows, bool upEnabled, bool downEnabled) ScrollToRow(int targetRow)
        {
            if (targetRow < 1) targetRow = 1;
            if (targetRow > MaxRows) targetRow = MaxRows;

            CurrentRows = targetRow;
            report?.ScrollIndex(CurrentRows);

            return GetPageButtonStates();
        }

        /// <summary>
        /// 重置到第一页
        /// </summary>
        /// <returns>重置后的行索引和按钮状态</returns>
        public (int currentRows, bool upEnabled, bool downEnabled) ResetToFirstPage()
        {
            CurrentRows = 1;
            report?.ScrollIndex(CurrentRows);
            return GetPageButtonStates();
        }

        /// <summary>
        /// 获取翻页按钮的启用状态
        /// </summary>
        /// <returns>上翻和下翻按钮的启用状态</returns>
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
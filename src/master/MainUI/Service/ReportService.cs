using RW.UI.Controls.Report;

namespace MainUI.Service
{
    /// <summary>
    /// 报表服务类
    /// 提供报表相关的业务逻辑处理,包括报表控件管理、文件操作、翻页等功能
    /// </summary>
    public class ReportService
    {
        #region 私有字段

        private readonly string _reportPath;
        private readonly RWReport _report;
        private int _currentRows = 1;
        private const int MaxRows = 1000;

        // ===== 报表控件单例管理 =====
        private static RWReport _sharedReportControl;
        private static readonly object _lock = new object();

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="reportPath">报表路径</param>
        /// <param name="report">报表控件</param>
        public ReportService(string reportPath, RWReport report)
        {
            _reportPath = reportPath ?? throw new ArgumentNullException(nameof(reportPath));
            _report = report ?? throw new ArgumentNullException(nameof(report));

            // 注册报表控件到共享实例
            RegisterReportControl(report);
        }

        #endregion

        #region ===== 报表控件单例管理 =====

        /// <summary>
        /// 注册报表控件到共享实例
        /// </summary>
        /// <param name="report">报表控件实例</param>
        public static void RegisterReportControl(RWReport report)
        {
            lock (_lock)
            {
                if (_sharedReportControl != report)
                {
                    _sharedReportControl = report;

                    // 同时更新 BaseTest.Report
                    BaseTest.Report = report;

                    NlogHelper.Default.Info($"报表控件已注册到全局服务 (实例: {report?.GetHashCode()})");
                }
            }
        }

        /// <summary>
        /// 获取共享的报表控件实例
        /// </summary>
        /// <returns>报表控件实例</returns>
        /// <exception cref="InvalidOperationException">报表控件未初始化时抛出</exception>
        public static RWReport GetReportControl()
        {
            lock (_lock)
            {
                if (_sharedReportControl == null || _sharedReportControl.IsDisposed)
                {
                    throw new InvalidOperationException(
                        "报表控件未初始化,请确保:\n" +
                        "1. 已在HMI界面加载报表\n" +
                        "2. UcHMI.Init() 已正确执行\n" +
                        "3. 报表文件路径正确");
                }

                return _sharedReportControl;
            }
        }

        /// <summary>
        /// 检查报表控件是否可用
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
        /// 在UI线程安全执行报表操作(有返回值)
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action">要执行的操作</param>
        /// <returns>操作结果</returns>
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
        /// 在UI线程安全执行报表操作(无返回值)
        /// </summary>
        /// <param name="action">要执行的操作</param>
        public static void InvokeOnReportControl(Action<RWReport> action)
        {
            ArgumentNullException.ThrowIfNull(action);

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
        /// 清除共享的报表控件引用
        /// (通常在应用程序关闭或重置时调用)
        /// </summary>
        public static void ClearReportControl()
        {
            lock (_lock)
            {
                _sharedReportControl = null;
                BaseTest.Report = null;
                NlogHelper.Default.Info("报表控件引用已清除");
            }
        }

        #endregion

        #region ===== 原有功能保持不变 =====

        /// <summary>
        /// 报表保存路径
        /// </summary>
        public static string SaveReportPath()
        {
            return Path.Combine(Application.StartupPath, "Save\\");
        }

        /// <summary>
        /// 获取默认报表路径
        /// </summary>
        public static string GetDefaultReportPath()
        {
            return Path.Combine(Application.StartupPath, "reports\\");
        }

        /// <summary>
        /// 获取工作报表路径
        /// </summary>
        public static string GetWorkingReportPath()
        {
            return Path.Combine(GetDefaultReportPath(), "report.xls");
        }

        /// <summary>
        /// 构建报表保存路径(智能选择基础目录)
        /// 优先使用用户设置的保存路径,未设置则使用默认路径
        /// 保存结构: [基础目录]\年\月\产品类型\文件名.xls
        /// </summary>
        /// <param name="modelTypeName">产品类型</param>
        /// <param name="modelName">产品型号</param>
        /// <returns>完整的保存路径</returns>
        public static string BuildSaveFilePath(string modelTypeName, string modelName)
        {
            try
            {
                DateTime now = DateTime.Now;

                // 智能选择基础目录
                string baseDirectory = GetBaseSaveDirectory();

                // 构建目录结构: [基础目录]\年\月\产品类型\
                string yearMonth = Path.Combine(now.Year.ToString(), now.Month.ToString("D2"));
                string directory = Path.Combine(baseDirectory, yearMonth, modelTypeName ?? "未知类型");

                // 确保目录存在
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    NlogHelper.Default.Info($"创建报表保存目录: {directory}");
                }

                // 构建文件名: 产品型号_图号_时间戳.xls
                string fileName = GenerateReportFileName(modelTypeName, modelName,
                    VarHelper.TestViewModel?.DrawingNo);

                string fullPath = Path.Combine(directory, fileName);
                NlogHelper.Default.Info($"构建报表保存路径: {fullPath}");

                return fullPath;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"构建报表路径失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取报表保存的基础目录
        /// 优先使用用户设置的路径,未设置则使用默认路径 D:\试验报告
        /// </summary>
        /// <returns>基础目录路径</returns>
        private static string GetBaseSaveDirectory()
        {
            try
            {
                // 加载用户配置
                SaveReportConfig saveReportConfig = new();
                saveReportConfig.Load();

                // 如果用户设置了保存路径
                if (!string.IsNullOrEmpty(saveReportConfig.RptSaveFile))
                {
                    string userPath = saveReportConfig.RptSaveFile;

                    // 如果用户设置的是目录路径,直接使用
                    if (Directory.Exists(userPath))
                    {
                        NlogHelper.Default.Info($"使用用户设置的保存目录: {userPath}");
                        return userPath;
                    }

                    // 如果用户设置的是文件路径,提取目录部分
                    string directory = Path.GetDirectoryName(userPath);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        // 确保目录存在
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                            NlogHelper.Default.Info($"创建用户设置目录: {directory}");
                        }
                        NlogHelper.Default.Info($"使用用户设置的保存目录: {directory}");
                        return directory;
                    }
                }

                // 未设置或设置无效,使用默认路径
                string defaultPath = SaveReportPath();
                NlogHelper.Default.Info($"使用默认保存目录: {defaultPath}");
                return defaultPath;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取基础保存目录失败,使用默认路径: {ex.Message}", ex);
                return "D:\\试验报告";
            }
        }

        /// <summary>
        /// 生成报表文件名
        /// 格式: 产品类型_产品型号_试验编号_时间戳.xls
        /// </summary>
        /// <param name="modelTypeName">产品类型</param>
        /// <param name="modelName">产品型号</param>
        /// <param name="testID">试验编号(可选)</param>
        /// <returns>文件名</returns>
        private static string GenerateReportFileName(string modelTypeName, string modelName, string testID = null)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // 清理文件名中的非法字符
            string safeModelType = CleanFileName(modelTypeName ?? "未知类型");
            string safeModelName = CleanFileName(modelName ?? "未知型号");
            string safeTestID = CleanFileName(testID ?? "");

            // 根据是否有试验编号生成文件名
            string fileName = string.IsNullOrEmpty(safeTestID)
                ? $"{safeModelType}_{safeModelName}_{timestamp}.xls"
                : $"{safeModelType}_{safeModelName}_{safeTestID}_{timestamp}.xls";

            return fileName;
        }

        /// <summary>
        /// 清理文件名中的非法字符
        /// </summary>
        /// <param name="fileName">原文件名</param>
        /// <returns>清理后的文件名</returns>
        private static string CleanFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return fileName;

            // Windows文件名非法字符
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                fileName = fileName.Replace(c, '_');
            }

            return fileName;
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        /// <summary>
        /// 复制报表文件
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
                NlogHelper.Default.Info($"报表文件已复制: {sourceFile} -> {destFile}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"复制报表文件失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 保存试验记录
        /// </summary>
        public static bool SaveTestRecord(TestRecordModel record)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(record);

                var result = VarHelper.fsql.Insert(record).ExecuteAffrows();

                if (result > 0)
                {
                    NlogHelper.Default.Info($"试验记录保存成功: {record.TestID}");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"保存试验记录失败: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 向上翻页
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
        /// 向下翻页
        /// </summary>
        public (int currentRows, bool upEnabled, bool downEnabled) PageDown(int pageSize)
        {
            _currentRows += pageSize;
            if (_currentRows > MaxRows)
            {
                _currentRows = 1; // 循环到第一页
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
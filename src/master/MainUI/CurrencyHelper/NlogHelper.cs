using MainUI.Service;
using Microsoft.VisualBasic.Logging;
using NLog;
using System.Runtime.CompilerServices;

namespace MainUI.CurrencyHelper
{
    /// <summary>
    /// 日志助手 - 优化版
    /// 
    /// 主要改进：
    /// 1. 线程安全：每次创建新LogEventInfo，避免多线程竞争
    /// 2. 简化异常处理：不手动拼接异常信息，让NLog.config自动处理
    /// 3. 自动获取调用源：使用CallerMemberName自动记录类名.方法名
    /// 4. 支持操作名称：messageName参数区分操作名称和详细信息
    /// 5. 完全向后兼容：保留原有API，现有代码无需修改
    /// 
    /// 日志级别说明：
    /// Trace - 最详细的信息，用于诊断问题
    /// Debug - 调试信息
    /// Info  - 一般信息，程序正常运行
    /// Warn  - 警告信息，潜在问题
    /// Error - 错误信息，功能受影响但程序可继续
    /// Fatal - 致命错误，程序无法继续
    /// </summary>
    public class NlogHelper
    {
        #region 字段和属性

        /// <summary>
        /// NLog日志记录器
        /// </summary>
        private readonly Logger _logger;

        /// <summary>
        /// 默认日志实例（单例模式，线程安全）
        /// </summary>
        public static NlogHelper Default { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private NlogHelper(Logger logger)
        {
            _logger = logger ?? LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// 命名构造函数
        /// </summary>
        /// <param name="name">日志记录器名称</param>
        public NlogHelper(string name) : this(LogManager.GetLogger(name)) { }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static NlogHelper()
        {
            try
            {
                Default = new NlogHelper("Logger");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"NlogHelper初始化失败: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region Trace 级别日志

        /// <summary>
        /// 记录Trace级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="messageName">操作名称（可选）</param>
        /// <param name="callerName">调用者名称（自动获取）</param>
        public void Trace(string message,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Trace, message, null, messageName, callerName);
        }

        /// <summary>
        /// 记录Trace级别日志（带异常）
        /// </summary>
        public void Trace(string message, Exception exception,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Trace, message, exception, messageName, callerName);
        }

        #endregion

        #region Debug 级别日志

        /// <summary>
        /// 记录Debug级别日志
        /// </summary>
        public void Debug(string message,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Debug, message, null, messageName, callerName);
        }

        /// <summary>
        /// 记录Debug级别日志（带异常）
        /// </summary>
        public void Debug(string message, Exception exception,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Debug, message, exception, messageName, callerName);
        }

        #endregion

        #region Info 级别日志

        /// <summary>
        /// 记录Info级别日志
        /// </summary>
        public void Info(string message,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Info, message, null, messageName, callerName);
        }

        /// <summary>
        /// 记录Info级别日志（带异常）
        /// </summary>
        public void Info(string message, Exception exception,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Info, message, exception, messageName, callerName);
        }

        #endregion

        #region Warn 级别日志

        /// <summary>
        /// 记录Warn级别日志
        /// </summary>
        public void Warn(string message,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Warn, message, null, messageName, callerName);
        }

        /// <summary>
        /// 记录Warn级别日志（带异常）
        /// </summary>
        public void Warn(string message, Exception exception,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Warn, message, exception, messageName, callerName);
        }

        #endregion

        #region Error 级别日志

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        public void Error(string message,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Error, message, null, messageName, callerName);
        }

        /// <summary>
        /// 记录Error级别日志（带异常）
        /// </summary>
        public void Error(string message, Exception exception,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Error, message, exception, messageName, callerName);
        }

        #endregion

        #region Fatal 级别日志

        /// <summary>
        /// 记录Fatal级别日志
        /// </summary>
        public void Fatal(string message,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Fatal, message, null, messageName, callerName);
        }

        /// <summary>
        /// 记录Fatal级别日志（带异常）
        /// </summary>
        public void Fatal(string message, Exception exception,
            string messageName = null,
            [CallerMemberName] string callerName = null)
        {
            WriteLog(NLog.LogLevel.Fatal, message, exception, messageName, callerName);
        }

        #endregion

        #region 核心写入方法

        /// <summary>
        /// 核心日志写入方法
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常对象（可选）</param>
        /// <param name="messageName">操作名称（可选，用于数据库MessageName字段）</param>
        /// <param name="callerName">调用者方法名（自动获取）</param>
        private void WriteLog(
            NLog.LogLevel level,
            string message,
            Exception exception,
            string messageName,
            string callerName)
        {
            // 检查日志级别是否启用
            if (_logger == null || !_logger.IsEnabled(level))
                return;

            try
            {
                // 每次创建新的LogEventInfo对象，避免多线程环境下的竞争条件
                var logEvent = new LogEventInfo(level, _logger.Name, message)
                {
                    Exception = exception,  // 设置异常对象
                    TimeStamp = DateTime.Now
                };

                // 不手动拼接异常信息
                // Message字段只包含传入的message
                // 异常信息由NLog.config的layout自动添加：
                // ${message}${onexception:${newline}${exception:format=tostring}}

                // 设置自定义属性（用于数据库存储）
                logEvent.Properties["UserName"] = GetCurrentUserName();
                logEvent.Properties["MessageName"] = messageName ?? message ?? "无";
                logEvent.Properties["Source"] = GetSourceInfo(callerName);
                logEvent.Properties["TestBenchID"] = GetCurrentTestBenchID();

                // 记录日志
                _logger.Log(logEvent);

#if DEBUG
                // Debug模式下额外输出到调试窗口
                var debugMessage = $"[{level}] {message}";
                if (exception != null)
                {
                    debugMessage += $" - {exception.Message}";
                }
                System.Diagnostics.Debug.WriteLine(debugMessage);
#endif
            }
            catch (Exception logEx)
            {
                // 日志记录失败时的降级处理
                // 避免因日志失败导致程序崩溃
                System.Diagnostics.Debug.WriteLine($"日志记录失败: {logEx.Message}");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取当前用户名
        /// </summary>
        /// <returns>用户名，失败时返回"系统"</returns>
        private static string GetCurrentUserName()
        {
            try
            {
                // 安全获取当前登录用户名
                // 如果已优化为CurrentUserManager，可以替换为：
                // return CurrentUserManager.CurrentUser?.Username ?? "系统";

                return NewUsers.NewUserInfo?.Username ?? "系统";
            }
            catch
            {
                return "系统";
            }
        }

        /// <summary>
        /// 自动获取调用源信息（类名.方法名）
        /// </summary>
        /// <param name="callerName">调用者方法名</param>
        /// <returns>格式: 类名.方法名</returns>
        private static string GetSourceInfo(string callerName)
        {
            try
            {
                if (!string.IsNullOrEmpty(callerName))
                {
                    // 通过堆栈跟踪获取调用类名
                    var stackTrace = new System.Diagnostics.StackTrace();

                    for (int i = 0; i < Math.Min(stackTrace.FrameCount, 10); i++) // 最多检查10层
                    {
                        var frame = stackTrace.GetFrame(i);
                        var method = frame?.GetMethod();

                        if (method != null)
                        {
                            var declaringType = method.DeclaringType;

                            // 跳过NlogHelper自身和系统类
                            if (declaringType != null &&
                                declaringType != typeof(NlogHelper) &&
                                !declaringType.FullName.StartsWith("System.") &&
                                !declaringType.FullName.StartsWith("Microsoft."))
                            {
                                // 返回格式: 类名.方法名
                                return $"{declaringType.Name}.{callerName}";
                            }
                        }
                    }
                }

                return callerName ?? "未知";
            }
            catch
            {
                return callerName ?? "未知";
            }
        }

        /// <summary>
        /// 获取当前试验台ID
        /// </summary>
        /// <returns>试验台ID，失败时返回0</returns>
        private static int GetCurrentTestBenchID()
        {
            try
            {
                return TestBenchService.CurrentTestBenchID;
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
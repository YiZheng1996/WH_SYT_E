using MainUI.Service;
using NLog;

namespace MainUI.CurrencyHelper
{
    /// <summary>
    /// 日志等级排序 自上而下，等级递增:
    /// Trace - 最常见的记录信息，一般用于普通输出
    /// Debug - 同样是记录信息，不过出现的频率要比Trace少一些，一般用来调试程序
    /// Info  - 信息类型的消息
    /// Warn  - 警告信息，一般用于比较重要的场合
    /// Error - 错误信息
    /// Fatal - 致命异常信息。一般来讲，发生致命异常之后程序将无法继续执行。
    /// </summary>
    public class NlogHelper
    {
        #region 初始化
        /// <summary>
        /// 日事件间类
        /// </summary>
        private readonly LogEventInfo _logEventInfo = new();

        /// <summary>
        /// 提供日志接口和实用程序功能
        /// </summary>
        public readonly Logger _logger = null;

        /// <summary>
        /// 自定义日志对象供外部使用(单例模式,只读)
        /// </summary>
        public static NlogHelper Default { get; private set; }

        private NlogHelper(Logger logger)
        {
            _logger = logger ?? LogManager.GetCurrentClassLogger();
        }

        public NlogHelper(string name) : this(LogManager.GetLogger(name)) { }

        static NlogHelper()
        {
            try
            {
                //获取具有当前类名称的日志程序。
                Default = new NlogHelper("Logger");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"NlogHelper初始化失败: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region 等级分类

        #region Trace 最常见的记录信息，一般用于普通输出
        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <param name="msg">跟踪信息</param>
        /// <param name="args">动态参数</param>
        public void Trace(string msg, params object[] args)
        {
            var message = string.Empty;
            if (args != null && args.Length > 0)
            {
                message = string.Format(msg, args);
            }
            else
            {
                message = msg;
            }
            InsLog(NLog.LogLevel.Trace, message);
        }

        public void Trace(string msg, Exception err)
        {
            InsLog(NLog.LogLevel.Trace, msg, err);
        }
        #endregion

        #region Debug 同样是记录信息，不过出现的频率要比Trace少一些，一般用来调试程序
        public void Debug(string msg, params object[] args)
        {
            var message = string.Empty;
            if (args != null && args.Length > 0)
            {
                message = string.Format(msg, args);
            }
            else
            {
                message = msg;
            }
            InsLog(NLog.LogLevel.Debug, message);
        }

        public void Debug(string msg, Exception err)
        {
            InsLog(NLog.LogLevel.Debug, msg, err);
        }
        #endregion

        #region Info 信息类型的消息
        public void Info(string msg, params object[] args)
        {
            var message = string.Empty;
            if (args != null && args.Length > 0)
            {
                message = string.Format(msg, args);
            }
            else
            {
                message = msg;
            }
            InsLog(NLog.LogLevel.Info, message);
        }

        public void Info(string msg, Exception err)
        {
            InsLog(NLog.LogLevel.Info, msg, err);
        }
        #endregion

        #region Warn 警告信息，一般用于比较重要的场合
        /// <summary>
        ///警告
        /// </summary>
        /// <param name="msg">警告信息</param>
        /// <param name="args">动态参数</param>
        public void Warn(string msg, params object[] args)
        {
            var message = string.Empty;
            if (args != null && args.Length > 0)
            {
                message = string.Format(msg, args);
            }
            else
            {
                message = msg;
            }
            InsLog(NLog.LogLevel.Warn, message);
        }

        public void Warn(string msg, Exception err)
        {
            InsLog(NLog.LogLevel.Warn, msg, err);
        }
        #endregion

        #region Error 错误信息
        /// <summary>
        /// 使用指定的参数在错误级别写入诊断消息。
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <param name="args">动态参数</param>
        public void Error(string msg, params object[] args)
        {
            var message = string.Empty;
            if (args != null && args.Length > 0)
            {
                message = string.Format(msg, args);
            }
            else
            {
                message = msg;
            }
            InsLog(NLog.LogLevel.Error, message);
        }

        /// <summary>
        /// 使用指定的参数在错误级别写入诊断消息。
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <param name="err">异常信息</param>
        public void Error(string msg, Exception err)
        {
            InsLog(NLog.LogLevel.Error, msg, err);
        }
        #endregion

        #region Fatal 致命异常信息。一般来讲，发生致命异常之后程序将无法继续执行。

        public void Fatal(string msg, params object[] args)
        {
            var message = string.Empty;
            if (args != null && args.Length > 0)
            {
                message = string.Format(msg, args);
            }
            else
            {
                message = msg;
            }
            InsLog(NLog.LogLevel.Fatal, message);
        }

        public void Fatal(string msg, Exception ex)
        {
            InsLog(NLog.LogLevel.Fatal, msg, ex);
        }
        #endregion

        #endregion

        #region 日志写入
        /// <summary>
        /// 写入日志信息
        /// </summary>
        private void InsLog(NLog.LogLevel level, string msg, Exception ex = null)
        {
            try
            {
                var stackTrace = string.Empty;
                if (ex != null)
                {
                    stackTrace = $"StackTrace:{ex.StackTrace},Message:";
                    var exception = ex;
                    do
                    {
                        stackTrace += exception.Message;
                        exception = exception.InnerException;
                        if (exception != null)
                        {
                            stackTrace += " -> ";
                        }
                    } while (exception != null);

                    stackTrace += " | ";
                }

                _logEventInfo.TimeStamp = DateTime.Now;

                // 安全获取用户名(可能为null)
                _logEventInfo.Properties["UserName"] = NewUsers.NewUserInfo?.Username ?? "系统";

                // 安全获取消息名称
                _logEventInfo.Properties["MessageName"] = string.IsNullOrEmpty(msg) ? "无" : msg;

                // 安全获取来源
                _logEventInfo.Properties["Source"] = _logger?.Name ?? "未知";

                // 安全获取试验台ID(可能为0)
                _logEventInfo.Properties["TestBenchID"] = TestBenchService.CurrentTestBenchID;

                _logEventInfo.Level = level;
                _logEventInfo.Message = stackTrace + msg;
                _logEventInfo.Exception = ex;

                _logger?.Log(_logEventInfo);

#if DEBUG
                System.Diagnostics.Debug.WriteLine($"[{level}] {msg}" + (ex != null ? $" - {ex.Message}" : ""));
#endif
            }
            catch (Exception logEx)
            {
                // 如果日志记录本身失败,输出到调试窗口
                System.Diagnostics.Debug.WriteLine($"日志记录失败: {logEx.Message}");
            }
        }
        #endregion 
    }
}
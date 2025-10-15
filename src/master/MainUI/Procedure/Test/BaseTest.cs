using AntdUI;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 测试基础类，提供测试流程控制和事件通知功能
    /// </summary>
    public class BaseTest
    {
        #region 事件定义
        /// <summary>
        /// 测试状态变更事件
        /// </summary>
        public static event Action<bool> TestStateChanged;

        /// <summary>
        /// 测试提示信息变更事件
        /// </summary>
        public static event EventHandler<object> TipsChanged;

        /// <summary>
        /// 测试计时变更事件
        /// </summary>
        public static event EventHandler<int> TimingChanged;

        /// <summary>
        /// 测试等待进度事件，使用等待的毫秒数
        /// </summary>
        public static event Action<int> WaitTick;

        /// <summary>
        /// 测试等待步骤进度事件，使用等待的秒数及步骤名称
        /// </summary>
        public static event Action<int, string> WaitStepTick;

        /// <summary>
        /// 测试状态变更事件触发
        /// </summary>
        /// <param name="state"></param>
        protected virtual void OnTestStateChanged(bool state) =>
            TestStateChanged?.Invoke(state);

        /// <summary>
        /// 测试提示信息变更事件触发
        /// </summary>
        /// <param name="info"></param>
        protected virtual void OnTipsChanged(object info) =>
            TipsChanged?.Invoke(this, info);

        /// <summary>
        /// 测试计时变更事件触发
        /// </summary>
        /// <param name="seconds"></param>
        protected virtual void OnTimingChanged(int seconds) =>
            TimingChanged?.Invoke(this, seconds);

        /// <summary>
        /// 测试等待进度变更事件触发
        /// </summary>
        /// <param name="tick"></param>
        protected virtual void OnWaitTick(int tick) =>
            WaitTick?.Invoke(tick);

        /// <summary>
        /// 测试等待步骤进度变更事件触发（传递秒数）
        /// </summary>
        protected virtual void OnWaitStepTick(int seconds, string stepName) =>
            WaitStepTick?.Invoke(seconds, stepName);
        #endregion

        #region 通用属性

        /// <summary>
        /// 当前测试项的取消令牌 - 由Execute方法自动设置
        /// </summary>
        protected CancellationToken CurrentCancellationToken { get; private set; }

        /// <summary>
        /// 系统参数配置
        /// </summary>
        private static ParaConfig _paraconfig = new();
        public static ParaConfig ParaConfig
        {
            get => _paraconfig;
            set => _paraconfig = value;
        }

        /// <summary>
        /// 系统报表控件
        /// </summary>
        private static RW.UI.Controls.Report.RWReport _report;
        public static RW.UI.Controls.Report.RWReport Report
        {
            get => _report;
            set => _report = value;
        }

        /// <summary>
        /// 菜单窗体
        /// </summary>
        private static frmMainMenu _frm;
        public static frmMainMenu Frm
        {
            get => _frm;
            set => _frm = value;
        }

        // 是否正在测试
        public static bool _IsTesting = false;
        #endregion

        #region 用户交互方法（带遮罩层的安全模态对话框）

        /// <summary>
        /// 安全地在UI线程上执行操作
        /// </summary>
        private void SafeInvoke(Action action)
        {
            // 1. 检查 Frm 是否为 null
            if (Frm == null)
            {
                string error = "Frm 为 null，无法显示对话框。请确保在测试项创建时已设置 Frm 属性。";
                NlogHelper.Default.Error(error);
                throw new InvalidOperationException(error);
            }

            // 2. 检查窗体句柄是否已创建
            if (!Frm.IsHandleCreated)
            {
                string error = "窗体句柄尚未创建，无法显示对话框。请确保在 UcHMI.Init() 中调用了 EnsureFrmHandleCreated()。";
                NlogHelper.Default.Error(error);
                throw new InvalidOperationException(error);
            }

            // 3. 检查窗体是否已释放
            if (Frm.IsDisposed)
            {
                string error = "窗体已释放，无法显示对话框。";
                NlogHelper.Default.Error(error);
                throw new ObjectDisposedException(error);
            }

            // 4. 安全调用
            try
            {
                if (Frm.InvokeRequired)
                {
                    Frm.Invoke(action);
                }
                else
                {
                    action();
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"SafeInvoke 执行失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 显示确认对话框（带遮罩层，阻止所有其他操作）
        /// </summary>
        /// <param name="message">确认消息</param>
        /// <param name="icon">图标类型</param>
        /// <returns>用户是否确认（true=确认，false=取消）</returns>
        public bool ShowConfirmDialog(string message, TType icon = TType.Warn)
        {
            bool result = false;

            try
            {
                TxtTips($"等待用户确认: {message}");

                SafeInvoke(() =>
                {
                    DialogResult dialogResult = MessageHelper.MessageYes(Frm, message, icon);
                    result = (dialogResult == DialogResult.OK);
                });

                TxtTips(result ? "用户已确认" : "用户已取消");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"显示确认对话框失败: {ex.Message}", ex);
                TxtTips($"对话框错误: {ex.Message}");
                throw;
            }

            return result;
        }

        /// <summary>
        /// 显示信息对话框（带遮罩层）
        /// </summary>
        /// <param name="message">信息内容</param>
        /// <param name="icon">图标类型</param>
        public void ShowInfoDialog(string message, TType icon = TType.Success)
        {
            try
            {
                TxtTips($"显示提示: {message}");

                SafeInvoke(() =>
                {
                    MessageHelper.MessageOK(Frm, message, icon);
                });
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"显示信息对话框失败: {ex.Message}", ex);
                TxtTips($"对话框错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 显示警告对话框（带遮罩层）
        /// </summary>
        /// <param name="message">警告信息</param>
        public void ShowWarningDialog(string message)
        {
            ShowInfoDialog(message, TType.Warn);
        }

        /// <summary>
        /// 显示错误对话框（带遮罩层）
        /// </summary>
        /// <param name="message">错误信息</param>
        public void ShowErrorDialog(string message)
        {
            TxtTips($"错误: {message}");
            ShowInfoDialog(message, TType.Error);
        }

        /// <summary>
        /// 显示成功对话框（带遮罩层）
        /// </summary>
        /// <param name="message">成功信息</param>
        public void ShowSuccessDialog(string message)
        {
            ShowInfoDialog(message, TType.Success);
        }

        /// <summary>
        /// 显示自定义对话框（带遮罩层）
        /// 用于需要更复杂交互的场景
        /// </summary>
        /// <typeparam name="T">对话框窗体类型</typeparam>
        /// <returns>对话框结果</returns>
        public DialogResult ShowCustomDialog<T>() where T : Form, new()
        {
            DialogResult result = DialogResult.Cancel;

            try
            {
                SafeInvoke(() =>
                {
                    using T dialog = new();
                    VarHelper.ShowDialogWithOverlay(Frm, dialog);
                    result = dialog.DialogResult;
                });
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"显示自定义对话框失败: {ex.Message}", ex);
                TxtTips($"对话框错误: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// 显示自定义对话框（带遮罩层，带参数）
        /// </summary>
        /// <typeparam name="T">对话框窗体类型</typeparam>
        /// <param name="configureDialog">配置对话框的委托</param>
        /// <returns>对话框结果</returns>
        public DialogResult ShowCustomDialog<T>(Action<T> configureDialog) where T : Form, new()
        {
            DialogResult result = DialogResult.Cancel;

            try
            {
                SafeInvoke(() =>
                {
                    using T dialog = new();
                    configureDialog?.Invoke(dialog);
                    VarHelper.ShowDialogWithOverlay(Frm, dialog);
                    result = dialog.DialogResult;
                });
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"显示自定义对话框失败: {ex.Message}", ex);
                TxtTips($"对话框错误: {ex.Message}");
            }

            return result;
        }

        #endregion

        #region 延时和等待操作

        /// <summary>
        /// 简单延时 - 使用当前项点的CancellationToken
        /// </summary>
        /// <param name="seconds">延时秒数</param>
        public void Delay(double seconds)
        {
            Delay(seconds, 100, () => false);
        }

        /// <summary>
        /// 简单延时 - 明确指定CancellationToken(向后兼容)
        /// </summary>
        /// <param name="seconds">延时秒数</param>
        /// <param name="cancellationToken">取消令牌</param>
        public void Delay(double seconds, CancellationToken cancellationToken)
        {
            Delay(seconds, 100, () => false, cancellationToken);
        }

        /// <summary>
        /// 带回调的延时 - 使用当前项点的CancellationToken
        /// </summary>
        /// <param name="seconds">延时秒数</param>
        /// <param name="interval">检查间隔(毫秒)</param>
        /// <param name="wait">等待条件(返回false时继续等待,返回true时退出)</param>
        /// <returns>是否超时(true=超时,false=条件满足提前退出)</returns>
        public bool Delay(double seconds, int interval, Func<bool> wait)
        {
            return Delay(seconds, interval, wait, CurrentCancellationToken);
        }

        /// <summary>
        /// 带回调的延时 - 明确指定CancellationToken(核心实现)
        /// </summary>
        /// <param name="seconds">延时秒数</param>
        /// <param name="interval">检查间隔(毫秒)</param>
        /// <param name="wait">等待条件(返回false时继续等待,返回true时退出)</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>是否超时(true=超时,false=条件满足提前退出)</returns>
        public bool Delay(double seconds, int interval, Func<bool> wait, CancellationToken cancellationToken)
        {
            var elapsed = 0;
            var timeout = seconds * 1000;

            while (elapsed <= timeout && !wait() && !cancellationToken.IsCancellationRequested)
            {
                Thread.Sleep(interval);
                elapsed += interval;
                OnWaitTick(elapsed);
                cancellationToken.ThrowIfCancellationRequested();
            }

            return elapsed > timeout;
        }

        /// <summary>
        /// 带条件的超时等待操作 - 使用当前项点的CancellationToken
        /// </summary>
        /// <param name="timeout">超时时间(秒)</param>
        /// <param name="breakTime">检查间隔(毫秒)</param>
        /// <param name="conditions">退出条件数组(任意一个为true时退出)</param>
        public void Delay(int timeout, int breakTime, params Func<bool>[] conditions)
        {
            Delay(timeout, breakTime, CurrentCancellationToken, conditions);
        }

        /// <summary>
        /// 带条件的超时等待操作 - 明确指定CancellationToken
        /// </summary>
        /// <param name="timeout">超时时间(秒)</param>
        /// <param name="breakTime">检查间隔(毫秒)</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <param name="conditions">退出条件数组(任意一个为true时退出)</param>
        public static void Delay(int timeout, int breakTime, CancellationToken cancellationToken, params Func<bool>[] conditions)
        {
            Stopwatch sw = Stopwatch.StartNew();
            conditions ??= [];

            while (sw.Elapsed.TotalSeconds < timeout && !cancellationToken.IsCancellationRequested)
            {
                Task.Delay(breakTime).Wait();
                if (conditions.Any(condition => condition()))
                {
                    return;
                }
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        /// <summary>
        /// 带步骤名称的延时 - 显示倒计时进度（使用当前项点的CancellationToken）
        /// </summary>
        /// <param name="seconds">延时秒数</param>
        /// <param name="interval">检查间隔(毫秒)</param>
        /// <param name="wait">等待条件(返回false时继续等待,返回true时退出)</param>
        /// <param name="waitName">步骤名称(显示在UI上)</param>
        /// <returns>是否完成(true=正常完成或条件满足,false=被取消或中断)</returns>
        public bool Delay(int seconds, int interval, Func<bool> wait, string waitName)
        {
            return Delay(seconds, interval, wait, waitName, CurrentCancellationToken);
        }

        /// <summary>
        /// 带步骤名称的延时 - 显示倒计时进度（明确指定CancellationToken）
        /// </summary>
        /// <param name="seconds">延时秒数</param>
        /// <param name="interval">检查间隔(毫秒)</param>
        /// <param name="wait">等待条件(返回false时继续等待,返回true时退出)</param>
        /// <param name="waitName">步骤名称(显示在UI上)</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>是否完成(true=正常完成或条件满足,false=被取消或中断)</returns>
        public bool Delay(int seconds, int interval, Func<bool> wait, string waitName, CancellationToken cancellationToken)
        {
            int remainingSeconds = seconds;
            int elapsedMilliseconds = 0;

            while (remainingSeconds > 0
                   && !wait()
                   && _IsTesting
                   && !cancellationToken.IsCancellationRequested)
            {
                Thread.Sleep(interval);
                elapsedMilliseconds += interval;

                // 每秒更新一次（当累计到1000毫秒时）
                if (elapsedMilliseconds >= 1000)
                {
                    remainingSeconds--;
                    elapsedMilliseconds = 0;

                    // 传递剩余秒数（而不是毫秒）
                    OnWaitStepTick(remainingSeconds, waitName);
                }

                // 检查取消令牌
                cancellationToken.ThrowIfCancellationRequested();
            }

            // 如果是因为取消或测试结束而退出，返回false
            // 如果是正常完成或条件满足，返回true
            return remainingSeconds <= 0 || wait();
        }
        #endregion

        #region 状态更新方法
        /// <summary>
        /// 更新已执行时间（秒数）
        /// </summary>
        public void LblTime(int seconds, string waitName) =>
            OnWaitStepTick(seconds, waitName);

        /// <summary>
        /// 显示提示信息
        /// </summary>
        public void TxtTips(object message) =>
            OnTipsChanged(message);

        /// <summary>
        /// 更新计时（秒数）
        /// </summary>
        public void TxtTiming(int seconds) =>
            OnTimingChanged(seconds);

        /// <summary>
        /// 更新测试状态
        /// </summary>
        public static void TestStatus(bool isTest)
        {
            _IsTesting = isTest;
            TestStateChanged?.Invoke(isTest);
        }
        #endregion

        #region 虚方法
        /// <summary>
        /// 在子类中执行测试过程
        /// </summary>
        public virtual Task<bool> Execute(CancellationToken cancellationToken)
        {
            // 设置当前测试项的取消令牌
            CurrentCancellationToken = cancellationToken;
            return Task.FromResult(true);
        }
        #endregion

    }
}

namespace MainUI.Service
{

    /// <summary>
    /// 计时服务 - 支持倒计时和正计时
    /// </summary>
    public class CountdownService(UIPanel timeLabel)
    {
        private CancellationTokenSource _cancellationTokenSource;
        private Task _timerTask;
        private readonly Stopwatch _stopwatch = new();
        private TimerMode _currentMode;

        /// <summary>
        /// 计时器模式
        /// </summary>
        public enum TimerMode
        {
            /// <summary>
            /// 倒计时模式
            /// </summary>
            Countdown,

            /// <summary>
            /// 正计时模式
            /// </summary>
            Countup
        }

        /// <summary>
        /// 计时更新事件 - 传递已过去的秒数（正计时）或剩余秒数（倒计时）
        /// </summary>
        public event Action<int> TimingUpdated;

        #region 倒计时模式

        /// <summary>
        /// 启动倒计时
        /// </summary>
        /// <param name="totalMinutes">总分钟数</param>
        /// <param name="externalToken">外部取消令牌</param>
        public async Task StartCountdown(int totalMinutes, CancellationToken externalToken)
        {
            await StartTimer(TimerMode.Countdown, totalMinutes * 60, externalToken);
        }

        /// <summary>
        /// 启动倒计时（秒为单位）
        /// </summary>
        /// <param name="totalSeconds">总秒数</param>
        /// <param name="externalToken">外部取消令牌</param>
        public async Task StartCountdownSeconds(int totalSeconds, CancellationToken externalToken)
        {
            await StartTimer(TimerMode.Countdown, totalSeconds, externalToken);
        }

        #endregion

        #region 正计时模式

        /// <summary>
        /// 启动正计时（从0开始计时）
        /// </summary>
        /// <param name="externalToken">外部取消令牌</param>
        public async Task StartCountup(CancellationToken externalToken)
        {
            await StartTimer(TimerMode.Countup, 0, externalToken);
        }

        /// <summary>
        /// 启动正计时（带最大时长限制）
        /// </summary>
        /// <param name="maxMinutes">最大分钟数，0表示无限制</param>
        /// <param name="externalToken">外部取消令牌</param>
        public async Task StartCountup(int maxMinutes, CancellationToken externalToken)
        {
            await StartTimer(TimerMode.Countup, maxMinutes * 60, externalToken);
        }

        #endregion

        #region 核心计时逻辑

        /// <summary>
        /// 启动计时器（核心方法）
        /// </summary>
        /// <param name="mode">计时模式</param>
        /// <param name="targetSeconds">目标秒数（倒计时=总时长，正计时=最大时长，0表示无限制）</param>
        /// <param name="externalToken">外部取消令牌</param>
        private async Task StartTimer(TimerMode mode, int targetSeconds, CancellationToken externalToken)
        {
            timeLabel.Invoke(() =>
            {
                timeLabel.Text = "00:00:00";
            });

            // 如果已有任务在运行，先停止
            StopTimer();

            _currentMode = mode;
            _cancellationTokenSource = new CancellationTokenSource();

            // 链接外部和内部token
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                externalToken,
                _cancellationTokenSource.Token
            );

            _stopwatch.Restart();

            _timerTask = Task.Run(async () =>
            {
                try
                {
                    TimeSpan elapsed = TimeSpan.Zero;
                    TimeSpan remaining = TimeSpan.FromSeconds(targetSeconds);

                    while (!linkedCts.Token.IsCancellationRequested)
                    {
                        elapsed = _stopwatch.Elapsed;

                        if (_currentMode == TimerMode.Countdown)
                        {
                            // 倒计时模式
                            remaining = TimeSpan.FromSeconds(targetSeconds) - elapsed;

                            if (remaining.TotalSeconds <= 0)
                            {
                                // 倒计时结束
                                UpdateDisplay(TimeSpan.Zero);
                                TimingUpdated?.Invoke(0);
                                break;
                            }

                            UpdateDisplay(remaining);
                            TimingUpdated?.Invoke((int)remaining.TotalSeconds);
                        }
                        else
                        {
                            // 正计时模式
                            UpdateDisplay(elapsed);
                            TimingUpdated?.Invoke((int)elapsed.TotalSeconds);

                            // 如果设置了最大时长限制
                            if (targetSeconds > 0 && elapsed.TotalSeconds >= targetSeconds)
                            {
                                break;
                            }
                        }

                        await Task.Delay(1000, linkedCts.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    // 正常取消
                    HandleCancellation();
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error("计时器运行错误", ex);
                    HandleCancellation();
                }
            }, linkedCts.Token);

            await _timerTask;
        }

        /// <summary>
        /// 更新显示
        /// </summary>
        private void UpdateDisplay(TimeSpan time)
        {
            timeLabel.Invoke(() =>
            {
                timeLabel.Text = $"{time.Hours:D2}:{time.Minutes:D2}:{time.Seconds:D2}";
            });
        }

        /// <summary>
        /// 处理取消操作
        /// </summary>
        private void HandleCancellation()
        {
            //timeLabel.Invoke(() =>
            //{
            //    timeLabel.Text = "00:00:00";
            //});
        }

        #endregion

        #region 控制方法

        /// <summary>
        /// 停止计时（原方法名保持兼容）
        /// </summary>
        public void StopCountdown()
        {
            StopTimer();
        }

        /// <summary>
        /// 停止计时器
        /// </summary>
        public void StopTimer()
        {
            try
            {
                _cancellationTokenSource?.Cancel();
                _stopwatch.Stop();
            }
            catch (ObjectDisposedException)
            {
                // 忽略已释放的对象
            }
        }

        /// <summary>
        /// 暂停计时器
        /// </summary>
        public void Pause()
        {
            _stopwatch.Stop();
        }

        /// <summary>
        /// 恢复计时器
        /// </summary>
        public void Resume()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void Reset()
        {
            StopTimer();
            _stopwatch.Reset();
            timeLabel.Invoke(() =>
            {
                timeLabel.Text = "00:00:00";
            });
            TimingUpdated?.Invoke(0);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取当前已经过的秒数
        /// </summary>
        public int ElapsedSeconds => (int)_stopwatch.Elapsed.TotalSeconds;

        /// <summary>
        /// 获取当前已经过的时间
        /// </summary>
        public TimeSpan ElapsedTime => _stopwatch.Elapsed;

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning => _stopwatch.IsRunning;

        /// <summary>
        /// 当前计时模式
        /// </summary>
        public TimerMode CurrentMode => _currentMode;

        #endregion

        #region 释放资源

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            StopTimer();
            _cancellationTokenSource?.Dispose();
        }

        #endregion
    }
}


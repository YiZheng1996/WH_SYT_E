namespace MainUI.Procedure
{
    /// <summary>
    /// 参数管理界面基类
    /// </summary>
    public partial class ucBaseManagerUI : UserControl
    {
        /// <summary>
        /// 标识是否已订阅数据变更事件
        /// </summary>
        private bool _isSubscribed = false;

        /// <summary>
        /// 是否自动订阅数据变更事件
        /// 子类可以通过设置此属性为false来禁用自动订阅
        /// </summary>
        protected bool AutoSubscribeDataChange { get; set; } = true;

        public ucBaseManagerUI()
        {
            InitializeComponent();

            // 在控件加载时自动订阅数据变更事件
            this.Load += UcBaseManagerUI_Load;
            // 在控件释放时取消订阅
            this.Disposed += UcBaseManagerUI_Disposed;
        }

        /// <summary>
        /// 控件加载事件处理
        /// </summary>
        private void UcBaseManagerUI_Load(object sender, EventArgs e)
        {
            // 如果启用自动订阅且尚未订阅，则订阅数据变更事件
            if (AutoSubscribeDataChange && !_isSubscribed)
            {
                SubscribeDataChange();
            }
        }

        /// <summary>
        /// 控件释放事件处理
        /// </summary>
        private void UcBaseManagerUI_Disposed(object sender, EventArgs e)
        {
            // 控件被释放时自动取消订阅，防止内存泄漏
            UnsubscribeDataChange();
        }

        /// <summary>
        /// 订阅数据变更事件
        /// </summary>
        protected void SubscribeDataChange()
        {
            if (!_isSubscribed)
            {
                // 订阅无参数事件
                DataChangedEventManager.DataChanged += OnDataChanged;

                // 订阅带参数事件（如果需要根据类型过滤）
                DataChangedEventManager.DataChangedWithType += OnDataChangedWithType;

                _isSubscribed = true;

                // 调试日志
                Debug.WriteLine($"{this.GetType().Name} 已订阅数据变更事件");
            }
        }

        /// <summary>
        /// 取消订阅数据变更事件
        /// </summary>
        protected void UnsubscribeDataChange()
        {
            if (_isSubscribed)
            {
                // 取消订阅
                DataChangedEventManager.DataChanged -= OnDataChanged;
                DataChangedEventManager.DataChangedWithType -= OnDataChangedWithType;

                _isSubscribed = false;

                // 调试日志
                Debug.WriteLine($"{this.GetType().Name} 已取消订阅数据变更事件");
            }
        }

        /// <summary>
        /// 数据变更事件处理（无参数版本）
        /// </summary>
        private void OnDataChanged()
        {
            try
            {
                // 如果控件不在设计模式且已创建句柄（已显示）
                if (!this.DesignMode && this.IsHandleCreated)
                {
                    // 使用Invoke确保在UI线程上执行
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() => Reload()));
                    }
                    else
                    {
                        Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                // 捕获异常，防止刷新错误影响其他控件
                NlogHelper.Default.Error($"{this.GetType().Name} 刷新数据时发生错误", ex);
            }
        }

        /// <summary>
        /// 数据变更事件处理（带类型参数版本）
        /// 子类可以重写此方法来实现按类型过滤刷新
        /// </summary>
        /// <param name="changeType">数据变更类型</param>
        protected virtual void OnDataChangedWithType(DataChangeType changeType)
        {
            // 默认行为：无论什么类型都刷新
            // 子类可以重写此方法，根据changeType决定是否刷新
            OnDataChanged();
        }

        /// <summary>
        /// 重新加载数据（虚方法）
        /// 子类必须重写此方法以实现具体的数据刷新逻辑
        /// </summary>
        public virtual void Reload()
        {
            // 子类重写此方法实现具体的数据加载逻辑
        }

        /// <summary>
        /// 手动刷新数据
        /// 当需要主动刷新时调用此方法
        /// </summary>
        public void ManualRefresh()
        {
            Reload();
        }

    }
}

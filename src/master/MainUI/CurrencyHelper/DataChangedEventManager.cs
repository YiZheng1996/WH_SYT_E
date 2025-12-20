namespace MainUI.CurrencyHelper
{
    /// <summary>
    /// 全局数据变更事件管理器
    /// 用于在数据修改后通知所有订阅的控件刷新数据
    /// </summary>
    public static class DataChangedEventManager
    {
        /// <summary>
        /// 数据变更事件
        /// 当任何数据发生变化时触发此事件
        /// </summary>
        public static event Action DataChanged;

        /// <summary>
        /// 带参数的数据变更事件
        /// 可以传递变更类型，让订阅者根据类型决定是否刷新
        /// </summary>
        public static event Action<DataChangeType> DataChangedWithType;

        /// <summary>
        /// 通知所有订阅者数据已变更（无参数）
        /// 在数据修改、删除、新增后调用此方法
        /// </summary>
        public static void NotifyDataChanged()
        {
            try
            {
                // 触发无参事件
                DataChanged?.Invoke();

                // 记录日志（可选）
                NlogHelper.Default.Info("数据变更通知已发送");
            }
            catch (Exception ex)
            {
                // 捕获异常，防止某个订阅者出错影响其他订阅者
                NlogHelper.Default.Error("通知数据变更时发生错误", ex);
            }
        }

        /// <summary>
        /// 通知所有订阅者数据已变更（带类型参数）
        /// 可以指定变更类型，让订阅者有选择地刷新
        /// </summary>
        /// <param name="changeType">数据变更类型</param>
        public static void NotifyDataChanged(DataChangeType changeType)
        {
            try
            {
                // 触发带参事件
                DataChangedWithType?.Invoke(changeType);

                // 同时触发无参事件（兼容不关心类型的订阅者）
                DataChanged?.Invoke();

                // 记录日志（可选）
                NlogHelper.Default.Info($"数据变更通知已发送，类型：{changeType}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("通知数据变更时发生错误", ex);
            }
        }

        /// <summary>
        /// 批量通知多个类型的数据变更
        /// </summary>
        public static void NotifyDataChanged(params DataChangeType[] changeTypes)
        {
            try
            {
                foreach (var changeType in changeTypes)
                {
                    DataChangedWithType?.Invoke(changeType);
                }

                DataChanged?.Invoke();

                NlogHelper.Default.Info($"数据变更通知已发送，类型：{string.Join(", ", changeTypes)}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("通知数据变更时发生错误", ex);
            }
        }

        /// <summary>
        /// 清除所有订阅（通常在应用程序关闭时调用）
        /// </summary>
        public static void ClearSubscriptions()
        {
            // 清除所有事件订阅
            DataChanged = null;
            DataChangedWithType = null;

            NlogHelper.Default.Info("已清除所有数据变更订阅");
        }
    }

    /// <summary>
    /// 数据变更类型枚举
    /// 用于区分不同类型的数据变更，让订阅者可以有选择地响应
    /// </summary>
    public enum DataChangeType
    {
        /// <summary>
        /// 所有数据（默认，刷新所有）
        /// </summary>
        All = 0,

        /// <summary>
        /// 用户数据变更
        /// </summary>
        User = 1,

        /// <summary>
        /// 角色数据变更
        /// </summary>
        Role = 2,

        /// <summary>
        /// 权限数据变更
        /// </summary>
        Permission = 3,

        /// <summary>
        /// 型号数据变更
        /// </summary>
        Model = 4,

        /// <summary>
        /// 型号类型数据变更
        /// </summary>
        ModelType = 5,

        /// <summary>
        /// 项点管理数据变更
        /// </summary>
        TestProcess = 6,

        /// <summary>
        /// 项点配置数据变更
        /// </summary>
        TestStep = 7,

        /// <summary>
        /// 报表模板数据变更
        /// </summary>
        ReportTemplate = 8,
    }
}

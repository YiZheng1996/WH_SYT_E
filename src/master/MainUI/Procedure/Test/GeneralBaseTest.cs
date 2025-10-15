namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 通用方法测试类，继承自BaseTest，提供具体的测试实现
    /// </summary>
    public class GeneralBaseTest : BaseTest
    {
        #region Execute 方法重写(确保设置 CurrentCancellationToken)

        /// <summary>
        /// 重写Execute方法,确保设置CurrentCancellationToken
        /// </summary>
        public override Task<bool> Execute(CancellationToken cancellationToken)
        {
            // 调用基类方法设置CurrentCancellationToken
            base.Execute(cancellationToken);

            // 子类可以继续重写此方法
            return Task.FromResult(true);
        }

        #endregion

        #region 全局初始化方法
        /// <summary>
        /// 全局初始化 - 整个测试序列开始前只调用一次
        /// </summary>
        public static void GlobalInit()
        {
            if (_IsTesting)
                return; // 已经初始化过，直接返回

            try
            {
                // 在这里添加你的全局初始化逻辑
                // 例如：硬件初始化、参数设置等

                TestStatus(true);
                NlogHelper.Default.Info("全局初始化完成");
            }
            catch (Exception ex)
            {
                _IsTesting = false;
                NlogHelper.Default.Error("全局初始化失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 全局清理 - 测试序列结束后调用
        /// </summary>
        public static void GlobalCleanup()
        {
            try
            {
                // 在这里添加你的全局清理逻辑
                TestStatus(false);
                NlogHelper.Default.Info("全局清理完成");
            }
            catch (Exception ex)
            {
                _IsTesting = false;
                NlogHelper.Default.Error("全局清理失败", ex);
            }
        }

        #endregion

        #region 通用方法编写处

        #endregion
    }
}

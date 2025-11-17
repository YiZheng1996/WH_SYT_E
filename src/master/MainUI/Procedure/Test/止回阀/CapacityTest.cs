namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 容量试验
    /// </summary>
    public class CapacityTest : GeneralBaseTest
    {
        public override async Task<bool> Execute(CancellationToken cancellationToken)
        {
            await base.Execute(cancellationToken);
            try
            {
                // 使用示例
                // 简单延时 2 秒
                Delay(2.0);

                // 等待条件满足（最多10秒）
                // wait() 返回 true 表示条件满足，退出等待
                bool timeout = Delay(10.0, 100, () => OPCHelper.AIgrp[0] > 100.0);
                if (timeout)
                {
                    TxtTips("等待超时");
                    return false;
                }

                if (!ShowConfirmDialog("是否继续?"))
                {
                    return false;
                }

                // 带图标类型的确认
                if (!ShowConfirmDialog("检测到异常，是否继续?", AntdUI.TType.Warn))
                {
                    return false;
                }

                // 显示各种提示
                ShowSuccessDialog("操作成功");
                ShowWarningDialog("注意检查");
                ShowErrorDialog("操作失败");
                ShowInfoDialog("提示信息");

                //  等待多个条件之一满足（最多5秒）
                Delay(5, 100,
                    () => OPCHelper.DIgrp[1],           // 条件1：DI[1] 为 true
                    () => OPCHelper.AIgrp[0] > 50.0     // 条件2：AI[0] > 50
                );

                //  带步骤名称的延时（显示倒计时）
                Delay(30, 100, () => false, "预热阶段");

                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"CapacityTest：{ex.Message}");
                throw new($"CapacityTest：{ex.Message}");
            }
            finally
            {
                // 试验结束后的清理操作
            }
        }
    }
}

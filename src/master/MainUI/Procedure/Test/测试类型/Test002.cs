namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 
    /// </summary>
    public class Test002 : GeneralBaseTest
    {
        public override async Task<bool> Execute(CancellationToken cancellationToken)
        {
            await base.Execute(cancellationToken);
            try
            {
                // 使用示例
                // 1. 简单延时 2 秒
                Delay(2.0);
                
                // 2. 等待条件满足（最多10秒）
                // wait() 返回 true 表示条件满足，退出等待
                bool timeout = Delay(10.0, 100, () => OPCHelper.AIgrp[0] > 100.0);
                if (timeout)
                {
                    TxtTips("等待超时");
                    return false;
                }
                
                // 3. 等待多个条件之一满足（最多5秒）
                Delay(5, 100, 
                    () => OPCHelper.DIgrp[1],           // 条件1：DI[1] 为 true
                    () => OPCHelper.AIgrp[0] > 50.0     // 条件2：AI[0] > 50
                );
                
                // 4. 带步骤名称的延时（显示倒计时）
                Delay(30, 100, () => false, "预热阶段");

                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"Test002：{ex.Message}");
                throw new($"Test002：{ex.Message}");
            }
            finally
            {
                // 试验结束后的清理操作
            }
        }
    }
}

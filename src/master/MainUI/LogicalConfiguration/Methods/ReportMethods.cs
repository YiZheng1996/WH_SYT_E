using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 报表工具方法集合
    /// </summary>
    public class ReportMethods : DSLMethodBase
    {
        public override string Category => "报表工具";
        public override string Description => "提供Excel报表读写等功能";

        /// <summary>
        /// 保存报表方法 - 使用新的统一错误处理
        /// </summary>
        public async Task<bool> SaveReport(Parameter_SaveReport param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                // TODO: 实现报表保存逻辑
                await Task.Delay(100); // 模拟保存操作

                return true;
            }, false);
        }

        /// <summary>
        /// 读取单元格方法 - 返回对象类型
        /// </summary>
        public async Task<object> ReadCells(Parameter_ReadCells param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                // TODO: 实现单元格读取逻辑
                await Task.Delay(100);
                return null;

            }, (object)null);
        }

        /// <summary>
        /// 写入单元格方法
        /// </summary>
        public async Task<bool> WriteCells(Parameter_WriteCells param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                // TODO: 实现单元格写入逻辑
                await Task.Delay(100); // 模拟写入操作

                return true;
            }, false);
        }
    }
}

using MainUI.Service;

namespace MainUI.BLL
{
    public class NlogsBLL
    {
        /// <summary>
        /// 查询日志列表 - 根据试验台过滤
        /// </summary>
        /// <param name="Level">日志级别(null或"All"表示查询所有)</param>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <returns></returns>
        public List<NlogsModel> FindList(string Level, DateTime from, DateTime to)
        {
            var query = VarHelper.fsql.Select<NlogsModel>()
                .Where(t => t.MessTime.Between(from, to));

            // 如果不是查询全部,则按级别过滤
            if (!string.IsNullOrEmpty(Level) && Level != "All")
            {
                query = query.Where(t => t.Level == Level);
            }

            // 数据权限过滤
            if (!TestBenchService.CanViewAllBenchData())
            {
                query = query.Where(t => t.TestBenchID == TestBenchService.CurrentTestBenchID);
            }

            return query
                .OrderByDescending(t => t.MessTime)
                .ToList();
        }
    }
}

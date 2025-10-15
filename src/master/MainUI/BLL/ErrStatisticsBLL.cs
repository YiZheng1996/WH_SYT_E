using MainUI.Service;

namespace MainUI.BLL
{
    public class ErrStatisticsBLL
    {
        /// <summary>
        /// 获取错误统计列表 - 根据试验台过滤
        /// </summary>
        public List<ErrStatisticsModel> GetErrStatistics()
        {
            var query = VarHelper.fsql.Select<ErrStatisticsModel>()
                .Where(m => m.IsDelete == 0);

            // 数据过滤
            if (!TestBenchService.CanViewAllBenchData())
            {
                query = query.Where(m => m.TestBenchID == TestBenchService.CurrentTestBenchID);
            }

            return query.ToList();
        }

        /// <summary>
        /// 软删除错误统计记录
        /// </summary>
        public bool DelErrStatistics(int ID) =>
            VarHelper.fsql.Update<ErrStatisticsModel>()
            .Set(m => m.IsDelete, 1)
            .Set(m => m.DeleteTime, DateTime.Now)
            .Where(m => m.ID == ID)
            .ExecuteAffrows() > 0;

        /// <summary>
        /// 更新错误统计记录
        /// </summary>
        public bool UpdateErrStatistics(ErrStatisticsModel model) =>
            VarHelper.fsql.Update<ErrStatisticsModel>()
            .SetSource(model)
            .Where(m => m.ID == model.ID)
            .ExecuteAffrows() > 0;

        /// <summary>
        /// 新增错误统计记录 - 自动关联当前试验台
        /// </summary>
        public bool AddErrStatistics(ErrStatisticsModel model)
        {
            // 自动设置试验台ID
            model.TestBenchID = TestBenchService.CurrentTestBenchID;

            return VarHelper.fsql.Insert(model)
                .ExecuteAffrows() > 0;
        }
    }
}

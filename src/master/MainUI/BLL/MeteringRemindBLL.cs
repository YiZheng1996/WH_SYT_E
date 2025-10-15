using MainUI.Service;

namespace MainUI.BLL
{
    internal class MeteringRemindBLL
    {
        /// <summary>
        /// 获取计量提醒列表 - 根据试验台过滤
        /// </summary>
        public List<MeteringRemindModel> GetMeteringReminds()
        {
            var query = VarHelper.fsql
                .Select<MeteringRemindModel>()
                .Where(m => m.IsDelete == 0);

            // 数据过滤
            if (!TestBenchService.CanViewAllBenchData())
            {
                query = query.Where(m => m.TestBenchID == TestBenchService.CurrentTestBenchID);
            }

            return query.ToList();
        }

        /// <summary>
        /// 软删除计量提醒记录
        /// </summary>
        public bool DelMeteringReminds(int ID)
            => VarHelper.fsql
            .Update<MeteringRemindModel>()
            .Set(m => m.IsDelete, 1)
            .Set(m => m.DeleteTime, DateTime.Now)
            .Where(m => m.ID == ID)
            .ExecuteAffrows() > 0;

        /// <summary>
        /// 更新计量提醒记录
        /// </summary>
        public bool UpdateMeteringReminds(MeteringRemindModel model)
            => VarHelper.fsql
            .Update<MeteringRemindModel>()
            .SetSource(model)
            .Where(m => m.ID == model.ID)
            .ExecuteAffrows() > 0;

        /// <summary>
        /// 新增计量提醒记录 - 自动关联当前试验台
        /// </summary>
        public bool AddMeteringReminds(MeteringRemindModel model)
        {
            // 自动设置试验台ID
            model.TestBenchID = TestBenchService.CurrentTestBenchID;

            return VarHelper.fsql
                .Insert(model)
                .ExecuteAffrows() > 0;
        }
    }
}

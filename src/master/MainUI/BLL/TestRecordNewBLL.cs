using MainUI.Service;

namespace MainUI.BLL
{
    internal class TestRecordNewBLL
    {
        /// <summary>
        /// 更新测试记录
        /// </summary>
        public bool UpdateTestRecord(TestRecordModel model) => VarHelper.fsql
                   .Update<TestRecordModel>()
                   .Set(a => a.KindID, model.KindID)
                   .Set(a => a.ModelID, model.ModelID)
                   .Set(a => a.TestID, model.TestID)
                   .Set(a => a.Tester, model.Tester)
                   .Set(a => a.TestTime, model.TestTime)
                   .Set(a => a.ReportPath, model.ReportPath)
                   .Where(a => a.ID == model.ID)
                   .ExecuteAffrows() > 0;

        /// <summary>
        /// 添加测试记录 - 自动关联当前试验台
        /// </summary>
        public bool SaveTestRecord(TestRecordModel model)
        {
            // 自动设置试验台ID
            model.TestBenchID = TestBenchService.CurrentTestBenchID;

            return VarHelper.fsql
                .Insert(model)
                .ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 删除测试记录
        /// </summary>
        public bool DeleteTestRecord(int id) => VarHelper.fsql
            .Delete<TestRecordModel>()
            .Where(a => a.ID == id)
            .ExecuteAffrows() > 0;

        /// <summary>
        /// 获取测试记录 - 根据试验台过滤
        /// </summary>
        /// <param name="model">查询条件模型</param>
        /// <param name="toTime">结束时间</param>
        /// <returns>测试记录列表</returns>
        public List<TestRecordModelDto> GetTestRecord(TestRecordModel model, DateTime toTime)
        {
            var query = VarHelper.fsql
                .Select<TestRecordModel, ModelsType, Models, TestBenchModel>()
                .LeftJoin((t, mt, m, tb) => t.KindID == mt.ID)
                .LeftJoin((t, mt, m, tb) => t.ModelID == m.ID)
                .LeftJoin((t, mt, m, tb) => t.TestBenchID == tb.ID) // 关联试验台表
                .WhereIf(model.KindID != -1, (t, mt, m, tb) => t.KindID == model.KindID)
                .WhereIf(model.ModelID != -1, (t, mt, m, tb) => t.ModelID == model.ModelID)
                .WhereIf(!string.IsNullOrEmpty(model.TestID), (t, mt, m, tb) => t.TestID.Contains(model.TestID))
                .WhereIf(!string.IsNullOrEmpty(model.Tester), (t, mt, m, tb) => t.Tester == model.Tester)
                .Where((t, mt, m, tb) => t.TestTime.Between(model.TestTime, toTime));

            // 关键修改：支持按指定工位查询
            // 如果model.TestBenchID > 0，则查询指定工位的数据
            // 如果model.TestBenchID == 0，则根据权限决定是否过滤
            if (model.TestBenchID > 0)
            {
                // 明确指定了工位ID，按指定工位查询
                query = query.Where((t, mt, m, tb) => t.TestBenchID == model.TestBenchID);
            }
            else if (model.TestBenchID == 0)
            {
                // TestBenchID为0表示查询所有工位
                // 但需要检查权限：如果没有全局查看权限，只能看自己的
                if (!TestBenchService.CanViewAllBenchData())
                {
                    query = query.Where((t, mt, m, tb) => t.TestBenchID == TestBenchService.CurrentTestBenchID);
                }
                // 如果有全局权限，则不添加过滤条件，查询所有工位
            }
            else
            {
                // TestBenchID < 0 或其他情况，按默认权限过滤
                if (!TestBenchService.CanViewAllBenchData())
                {
                    query = query.Where((t, mt, m, tb) => t.TestBenchID == TestBenchService.CurrentTestBenchID);
                }
            }

            return query
                .OrderByDescending((t, mt, m, tb) => t.TestTime) // 按时间倒序排列
                .ToList((t, mt, m, tb) => new TestRecordModelDto
                {
                    ID = t.ID,
                    KindID = t.KindID,
                    ModelID = t.ModelID,
                    TestID = t.TestID,
                    Tester = t.Tester,
                    TestTime = t.TestTime,
                    ReportPath = t.ReportPath,
                    TestBenchID = t.TestBenchID,
                    ModelTypeName = mt.ModelTypeName,
                    ModelName = m.ModelName,
                    TestBenchName = tb.BenchName ?? "未知" // 添加工位名称
                });
        }
    }
}

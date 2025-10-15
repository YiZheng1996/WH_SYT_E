
namespace MainUI.BLL
{
    public class TestBenchBLL
    {
        /// <summary>
        /// 获取所有启用的试验台
        /// </summary>
        public List<TestBenchModel> GetActiveTestBenches() =>
            VarHelper.fsql.Select<TestBenchModel>()
                .Where(t => t.Status)
                .OrderBy(t => t.BenchCode)
                .ToList();

        /// <summary>
        /// 根据ID获取试验台信息
        /// </summary>
        /// <param name="testBenchId">试验台ID</param>
        /// <returns>试验台信息，未找到返回null</returns>
        public TestBenchModel GetTestBench(int testBenchId)
        {
            try
            {
                return VarHelper.fsql.Select<TestBenchModel>()
                    .Where(t => t.ID == testBenchId)
                    .First();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"查询试验台失败: ID={testBenchId}", ex);
                return null;
            }
        }

        /// <summary>
        /// 新增试验台
        /// </summary>
        public bool AddTestBench(TestBenchModel model) =>
            VarHelper.fsql.Insert(model)
                .ExecuteAffrows() > 0;

        /// <summary>
        /// 更新试验台
        /// </summary>
        public bool UpdateTestBench(TestBenchModel model) =>
            VarHelper.fsql.Update<TestBenchModel>()
                .SetSource(model)
                .Where(t => t.ID == model.ID)
                .ExecuteAffrows() > 0;

        /// <summary>
        /// 启用/禁用试验台
        /// </summary>
        public bool ToggleStatus(int id, bool status) =>
            VarHelper.fsql.Update<TestBenchModel>()
                .Set(t => t.Status, status)
                .Where(t => t.ID == id)
                .ExecuteAffrows() > 0;

    }
}
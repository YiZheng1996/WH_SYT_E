using MainUI.Service;

namespace MainUI.BLL
{
    public class ModelBLL
    {
        /// <summary>
        /// 查找指定类型ID的所有型号 - 根据试验台过滤
        /// </summary>
        /// <param name="typeID">类型表ID</param>
        /// <param name="IsRelease">是否查看未发布型号 
        /// false只查看发布型号 true查看所有型号</param>
        /// <param name="filterByTestBench">是否按试验台过滤（默认true）。
        /// 在数据管理界面（FrmDataManager）查询时，应传入false以查看所有工位的数据</param>
        /// <param name="includePleaseSelect">是否包含"请选择"选项（默认false）。
        /// 在数据管理界面中传入true以便用户重置选择</param>
        /// <returns></returns>
        public static List<NewModels> GetNewModels(int typeID, bool IsRelease = false,
            bool filterByTestBench = true, bool includePleaseSelect = false)
        {
            var query = VarHelper.fsql.Select<Models, ModelsType>()
                .LeftJoin((m, t) => m.TypeID == t.ID)
                .Where((m, t) => m.TypeID == typeID)
                .WhereIf(!IsRelease, (m, t) => m.IsRelease);

            if (filterByTestBench && !TestBenchService.CanViewAllBenchData())
            {
                query = query.Where((m, t) => t.TestBenchID == TestBenchService.CurrentTestBenchID);
            }

            var result = query.ToList((m, t) => new NewModels
            {
                ID = m.ID,
                ModelName = m.ModelName,
                ModelTypeID = t.ID,
                ModelTypeName = t.ModelTypeName,
                Mark = m.Mark,
                DrawingNo = m.DrawingNo,
                CompanyProjectNo = m.CompanyProjectNo,
                CustomerProjectNo = m.CustomerProjectNo,
                IsRelease = m.IsRelease,
                ReleaseTime = m.ReleaseTime,
                CreateTime = m.CreateTime,
                UpdateTime = m.UpdateTime
            });

            if (includePleaseSelect && result.Count > 0)
            {
                result.Insert(0, new NewModels { ID = -1, ModelName = "请选择" });
            }

            return result;
        }

        /// <summary>
        /// 根据试验台ID获取指定类型的所有型号
        /// </summary>
        public static List<NewModels> GetNewModelsByTestBench(int typeID, int testBenchID,
            bool IsRelease = false, bool includePleaseSelect = false)
        {
            var query = VarHelper.fsql.Select<Models, ModelsType>()
                .LeftJoin((m, t) => m.TypeID == t.ID)
                .Where((m, t) => m.TypeID == typeID)
                .WhereIf(!IsRelease, (m, t) => m.IsRelease);

            if (testBenchID > 0)
            {
                query = query.Where((m, t) => t.TestBenchID == testBenchID);
            }

            var result = query.ToList((m, t) => new NewModels
            {
                ID = m.ID,
                ModelName = m.ModelName,
                ModelTypeID = t.ID,
                ModelID = m.ID,
                ModelTypeName = t.ModelTypeName,
                Mark = m.Mark,
                DrawingNo = m.DrawingNo,
                CompanyProjectNo = m.CompanyProjectNo,
                CustomerProjectNo = m.CustomerProjectNo,
                IsRelease = m.IsRelease,
                ReleaseTime = m.ReleaseTime,
                CreateTime = m.CreateTime,
                UpdateTime = m.UpdateTime
            });

            if (includePleaseSelect && result.Count > 0)
            {
                result.Insert(0, new NewModels { ID = -1, ModelName = "请选择" });
            }

            return result;
        }

        /// <summary>
        /// 根据型号ID获取单条完整记录
        /// </summary>
        public static NewModels GetModelById(int modelId)
        {
            return VarHelper.fsql.Select<Models, ModelsType>()
                .LeftJoin((m, t) => m.TypeID == t.ID)
                .Where((m, t) => m.ID == modelId)
                .ToList((m, t) => new NewModels
                {
                    ID = m.ID,
                    ModelName = m.ModelName,
                    ModelTypeID = t.ID,
                    ModelTypeName = t.ModelTypeName,
                    Mark = m.Mark,
                    DrawingNo = m.DrawingNo,
                    CompanyProjectNo = m.CompanyProjectNo,
                    CustomerProjectNo = m.CustomerProjectNo,
                    IsRelease = m.IsRelease,
                    ReleaseTime = m.ReleaseTime,
                    CreateTime = m.CreateTime,
                    UpdateTime = m.UpdateTime
                }).FirstOrDefault();
        }

        /// <summary>
        /// 检查型号是否存在
        /// </summary>
        public bool IsExist(int typeid, string modelname) =>
            VarHelper.fsql.Select<Models>()
            .Where(a => a.TypeID == typeid && a.ModelName == modelname)
            .ToList().Count > 0;

        /// <summary>
        /// 新增型号
        /// </summary>
        public static bool Add(Models models)
        {
            return VarHelper.fsql.Insert<Models>().AppendData(new Models
            {
                ModelName = models.ModelName,
                TypeID = models.TypeID,
                Mark = models.Mark,
                DrawingNo = models.DrawingNo,
                CompanyProjectNo = models.CompanyProjectNo,
                CustomerProjectNo = models.CustomerProjectNo,
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            }).ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 修改型号
        /// </summary>
        public bool Update(Models models)
        {
            return VarHelper.fsql.Update<Models>()
                .Set(a => a.ModelName, models.ModelName)
                .Set(a => a.TypeID, models.TypeID)
                .Set(a => a.Mark, models.Mark)
                .Set(a => a.DrawingNo, models.DrawingNo)
                .Set(a => a.CompanyProjectNo, models.CompanyProjectNo)
                .Set(a => a.CustomerProjectNo, models.CustomerProjectNo)
                .Set(a => a.UpdateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Where(a => a.ID == models.ID)
                .ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 发布型号
        /// </summary>
        public bool IsRelease(NewModels models)
        {
            return VarHelper.fsql.Update<Models>()
                .Set(a => a.IsRelease, true)
                .Set(a => a.ReleaseTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Where(a => a.ID == models.ID)
                .ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 删除型号
        /// </summary>
        public bool Delete(int modelID, string lxname = null, string xhname = null)
        {
            //deleteFile(SpecificSymbol(lxname), SpecificSymbol(xhname));
            return VarHelper.fsql.Delete<Models>()
            .Where(a => a.ID == modelID)
            .ExecuteAffrows() > 0;
        }
    }
}
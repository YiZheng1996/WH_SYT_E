using MainUI.Service;

namespace MainUI.BLL
{
    public class ModelTypeBLL
    {
        /// <summary>
        /// 查找所有类型表列表
        /// </summary>
        /// <returns></returns>
        public List<ModelsType> GetAllModelType()
        {
            var query = VarHelper.fsql.Select<ModelsType>().Where(m => m.TestBenchID == TestBenchService.CurrentTestBenchID);

            return query.ToList();
        }

        /// <summary>
        /// 添加类型表数据 - 自动关联当前试验台
        /// </summary>
        public bool Add(ModelsType models)
        {
            // 自动设置试验台ID
            models.TestBenchID = TestBenchService.CurrentTestBenchID;

            return VarHelper.fsql.Insert<ModelsType>()
                .AppendData(models)
                .ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 删除类型表数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Delete(int id, string name = null)
        {
            //deleteFile(SpecificSymbol(name));
            return VarHelper.fsql.Delete<ModelsType>()
              .Where(a => a.ID == id)
              .ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 修改类型表数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool Updata(ModelsType models)
        {
            //changeFileName(SpecificSymbol(name), SpecificSymbol(OldName));
            return VarHelper.fsql.Update<ModelsType>()
            .Set(a => a.ModelTypeName, models.ModelTypeName)
            .Set(a => a.Mark, models.Mark)
            .Where(a => a.ID == models.ID)
            .ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 查看所有类型列表
        /// </summary>
        /// <param name="filterByTestBench">是否按试验台过滤（默认true）。
        /// 在数据管理界面（FrmDataManager）查询时，应传入false以查看所有工位的数据</param>
        /// <param name="includePleaseSelect">是否包含"请选择"选项（默认false）。
        /// 在数据管理界面中传入true以便用户重置选择</param>
        /// <returns></returns>
        public List<ModelsType> GetModels(bool filterByTestBench = true, bool includePleaseSelect = false)
        {
            var query = VarHelper.fsql.Select<ModelsType>();

            // 只有在需要过滤且没有全局查看权限时才过滤
            if (filterByTestBench && !TestBenchService.CanViewAllBenchData())
            {
                query = query.Where(x => x.TestBenchID == TestBenchService.CurrentTestBenchID);
            }

            var result = query.ToList();

            // 如果没有数据，添加"无数据"提示
            if (result.Count < 1)
            {
                result.Add(new ModelsType()
                {
                    ID = 0,
                    ModelTypeName = "无数据"
                });
            }
            // 如果需要添加"请选择"选项，在开头插入
            else if (includePleaseSelect)
            {
                result.Insert(0, new ModelsType()
                {
                    ID = -1,
                    ModelTypeName = "请选择"
                });
            }

            return result;
        }

        /// <summary>
        /// 根据试验台ID获取类型列表
        /// </summary>
        /// <param name="testBenchID">试验台ID，0表示获取所有</param>
        /// <param name="includePleaseSelect">是否包含"请选择"选项</param>
        /// <returns></returns>
        public List<ModelsType> GetModelsByTestBench(int testBenchID, bool includePleaseSelect = false)
        {
            var query = VarHelper.fsql.Select<ModelsType>();

            if (testBenchID > 0)
            {
                query = query.Where(x => x.TestBenchID == testBenchID);
            }

            var result = query.ToList();

            if (result.Count < 1)
            {
                result.Add(new ModelsType()
                {
                    ID = 0,
                    ModelTypeName = "无数据"
                });
            }
            else if (includePleaseSelect)
            {
                result.Insert(0, new ModelsType()
                {
                    ID = -1,
                    ModelTypeName = "请选择"
                });
            }

            return result;
        }
    }
}

using MainUI.Service;

namespace MainUI.BLL
{
    internal class TestProcessBLL
    {
        //private readonly TestClassFileManager _fileManager = new();

        /// <summary>
        /// 获取所有测试流程 - 根据当前试验台过滤
        /// </summary>
        public static List<TestProcessModel> GetTestProcess(int typeID = 0, bool? isVisible = null)
        {
            var query = VarHelper.fsql.Select<TestProcessModel>()
                .WhereIf(typeID > 0, w => w.ModelTypeID == typeID)
                .WhereIf(isVisible.HasValue, w => w.IsVisible == isVisible.Value);

            // 数据过滤
            if (!TestBenchService.CanViewAllBenchData())
            {
                query = query.Where(t => t.TestBenchID == TestBenchService.CurrentTestBenchID);
            }

            return query.ToList(item => new TestProcessModel
            {
                ID = item.ID,
                ProcessName = item.ProcessName,
                ModelTypeID = item.ModelTypeID,
                EntityClassName = item.EntityClassName,
                IsVisible = item.IsVisible,
                TestBenchID = item.TestBenchID
            });
        }

        public bool AddTestProcess(TestProcessModel model)
        {
            try
            {
                // 自动设置试验台ID
                model.TestBenchID = TestBenchService.CurrentTestBenchID;

                bool result = VarHelper.fsql.Insert(model).ExecuteAffrows() > 0;

                //if (result && !string.IsNullOrEmpty(model.EntityClassName))
                //{
                //    _fileManager.CreateTestClassByEntityClassName(
                //        model.EntityClassName,
                //        model.ModelTypeID,
                //        model.ProcessName
                //    );
                //}
                return result;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("添加测试流程失败：", ex);
                return false;
            }
        }

        public bool DelTestProcess(int ID)
        {
            try
            {
                var model = VarHelper.fsql.Select<TestProcessModel>()
                    .Where(a => a.ID == ID)
                    .First();

                if (model == null) return false;

                bool result = VarHelper.fsql.Delete<TestProcessModel>()
                    .Where(a => a.ID == ID)
                    .ExecuteAffrows() > 0;

                //if (result && !string.IsNullOrEmpty(model.EntityClassName))
                //{
                //    _fileManager.DeleteTestClassByEntityClassName(model.EntityClassName, model.ModelTypeID);
                //}
                return result;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("删除测试流程失败：", ex);
                return false;
            }
        }

        public bool SaveTestProcess(TestProcessModel model)
        {
            try
            {
                // 获取原始记录
                var originalModel = VarHelper.fsql.Select<TestProcessModel>()
                    .Where(w => w.ID == model.ID)
                    .First();

                if (originalModel == null)
                    return false;

                string oldEntityClassName = originalModel.EntityClassName;
                int oldModelTypeID = originalModel.ModelTypeID;

                // 更新数据库
                bool result = VarHelper.fsql.Update<TestProcessModel>()
                    .Set(s => s.ProcessName, model.ProcessName)
                    .Set(s => s.IsVisible, model.IsVisible)
                    .Set(s => s.EntityClassName, model.EntityClassName)
                    .Where(w => w.ID == model.ID)
                    .ExecuteAffrows() > 0;

                if (result)
                {
                    // 处理文件操作 - 修改这里的逻辑
                    HandleFileOperationsWithModelType(oldEntityClassName, model.EntityClassName,
                        oldModelTypeID, model.ModelTypeID);
                }

                return result;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存测试流程失败：", ex);
                return false;
            }
        }

        // 新增或修改这个方法来处理ModelType变化的情况
        private void HandleFileOperationsWithModelType(string oldEntityClassName, string newEntityClassName,
            int oldModelTypeID, int newModelTypeID)
        {
            try
            {
                // 情况1：EntityClassName和ModelTypeID都没变，无需操作
                if (oldEntityClassName == newEntityClassName && oldModelTypeID == newModelTypeID)
                {
                    return;
                }

                // 情况2：只有EntityClassName发生变化，ModelTypeID没变
                if (!string.IsNullOrEmpty(oldEntityClassName) &&
                    !string.IsNullOrEmpty(newEntityClassName) &&
                    oldEntityClassName != newEntityClassName &&
                    oldModelTypeID == newModelTypeID)
                {
                    //_fileManager.RenameTestClassByEntityClassName(oldEntityClassName, newEntityClassName, newModelTypeID);
                    //NlogHelper.Default.Info($"重命名测试文件：{oldEntityClassName} -> {newEntityClassName}");
                }
                // 情况3：ModelTypeID发生变化（可能EntityClassName也变了）
                else if (oldModelTypeID != newModelTypeID)
                {
                    if (!string.IsNullOrEmpty(oldEntityClassName))
                    {
                        // 先移动文件到新的ModelType目录
                        //_fileManager.MoveTestClassToNewModelType(oldEntityClassName, oldModelTypeID, newModelTypeID);

                        // 如果EntityClassName也发生了变化，再进行重命名
                        if (!string.IsNullOrEmpty(newEntityClassName) && oldEntityClassName != newEntityClassName)
                        {
                            //_fileManager.RenameTestClassByEntityClassName(oldEntityClassName, newEntityClassName, newModelTypeID);
                        }

                        NlogHelper.Default.Info($"移动测试文件到新ModelType：{oldModelTypeID} -> {newModelTypeID}");
                    }
                    else if (!string.IsNullOrEmpty(newEntityClassName))
                    {
                        // 直接在新ModelType目录创建文件
                        //_fileManager.CreateTestClassByEntityClassName(newEntityClassName, newModelTypeID);
                    }
                }
                // 情况4：只是新增了EntityClassName
                else if (string.IsNullOrEmpty(oldEntityClassName) && !string.IsNullOrEmpty(newEntityClassName))
                {
                    //_fileManager.CreateTestClassByEntityClassName(newEntityClassName, newModelTypeID);
                    //NlogHelper.Default.Info($"创建新测试文件：{newEntityClassName}");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("处理测试文件操作失败", ex);
            }
        }
    }
}
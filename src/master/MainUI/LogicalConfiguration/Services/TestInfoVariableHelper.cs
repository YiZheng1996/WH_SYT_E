using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.Service;

namespace MainUI.LogicalConfiguration.Services
{
    /// <summary>
    /// 测试信息变量助手
    /// 用于管理试验员、产品型号、产品类型等系统级全局变量
    /// </summary>
    public static class TestInfoVariableHelper
    {
        // 预定义的变量名称常量
        public const string VAR_TESTER = "试验员";
        public const string VAR_MODEL_TYPE = "产品类型";
        public const string VAR_MODEL_NAME = "产品型号";
        public const string VAR_TEST_ID = "产品图号";
        public const string VAR_TEST_TIME = "测试时间";
        public const string VAR_TEST_BENCH = "试验台";

        /// <summary>
        /// 初始化测试信息相关的全局变量
        /// 在工作流启动时调用
        /// </summary>
        /// <param name="variableManager">全局变量管理器</param>
        public static void InitializeTestInfoVariables(GlobalVariableManager variableManager)
        {
            ArgumentNullException.ThrowIfNull(variableManager);

            // 1. 试验员
            AddOrUpdateVariable(variableManager, new VarItem_Enhanced
            {
                VarName = VAR_TESTER,
                VarType = "string",
                VarValue = GetCurrentTester(),
                IsSystemVariable = true,  // 标记为系统变量
                VarText = "当前试验员姓名"
            });

            // 2. 产品类型
            AddOrUpdateVariable(variableManager, new VarItem_Enhanced
            {
                VarName = VAR_MODEL_TYPE,
                VarType = "string",
                VarValue = GetCurrentModelType(),
                IsSystemVariable = true,  // 标记为系统变量
                VarText = "当前产品类型名称"
            });

            // 3. 产品型号
            AddOrUpdateVariable(variableManager, new VarItem_Enhanced
            {
                VarName = VAR_MODEL_NAME,
                VarType = "string",
                VarValue = GetCurrentModelName(),
                IsSystemVariable = true,  // 标记为系统变量
                VarText = "当前产品型号名称"
            });

            // 4. 产品图号
            AddOrUpdateVariable(variableManager, new VarItem_Enhanced
            {
                VarName = VAR_TEST_ID,
                VarType = "string",
                VarValue = GetCurrentTestID(),
                IsSystemVariable = true,  // 标记为系统变量
                VarText = "当前产品图号/测试ID"
            });

            // 5. 测试时间
            AddOrUpdateVariable(variableManager, new VarItem_Enhanced
            {
                VarName = VAR_TEST_TIME,
                VarType = "string",
                VarValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                IsSystemVariable = true,  // 标记为系统变量
                VarText = "当前测试时间"
            });

            // 6. 试验台
            AddOrUpdateVariable(variableManager, new VarItem_Enhanced
            {
                VarName = VAR_TEST_BENCH,
                VarType = "string",
                VarValue = GetCurrentTestBench(),
                IsSystemVariable = true,  // 标记为系统变量
                VarText = "当前试验台名称"
            });

            var aa = variableManager.GetAllVariables();
            NlogHelper.Default.Info("测试信息变量初始化完成");
        }

        /// <summary>
        /// 更新所有测试信息变量的值
        /// 在测试参数改变时调用
        /// </summary>
        /// <param name="variableManager">全局变量管理器</param>
        public static void UpdateTestInfoVariables(GlobalVariableManager variableManager)
        {
            if (variableManager == null) return;

            UpdateVariableValue(variableManager, VAR_TESTER, GetCurrentTester());
            UpdateVariableValue(variableManager, VAR_MODEL_TYPE, GetCurrentModelType());
            UpdateVariableValue(variableManager, VAR_MODEL_NAME, GetCurrentModelName());
            UpdateVariableValue(variableManager, VAR_TEST_ID, GetCurrentTestID());
            UpdateVariableValue(variableManager, VAR_TEST_TIME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            UpdateVariableValue(variableManager, VAR_TEST_BENCH, GetCurrentTestBench());

            NlogHelper.Default.Info("测试信息变量已更新");
        }

        /// <summary>
        /// 更新产品类型和型号
        /// 当用户切换产品时调用
        /// </summary>
        public static void UpdateProductInfo(
            GlobalVariableManager variableManager,
            string modelTypeName,
            string modelName)
        {
            if (variableManager == null) return;

            UpdateVariableValue(variableManager, VAR_MODEL_TYPE, modelTypeName ?? "");
            UpdateVariableValue(variableManager, VAR_MODEL_NAME, modelName ?? "");
            UpdateVariableValue(variableManager, VAR_TEST_TIME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            NlogHelper.Default.Info($"产品信息已更新: {modelTypeName} - {modelName}");
        }

        /// <summary>
        /// 更新试验员信息
        /// </summary>
        public static void UpdateTester(GlobalVariableManager variableManager, string testerName)
        {
            if (variableManager == null) return;

            UpdateVariableValue(variableManager, VAR_TESTER, testerName ?? "");
            NlogHelper.Default.Info($"试验员已更新: {testerName}");
        }

        /// <summary>
        /// 更新产品图号
        /// </summary>
        public static void UpdateTestID(GlobalVariableManager variableManager, string testID)
        {
            if (variableManager == null) return;

            UpdateVariableValue(variableManager, VAR_TEST_ID, testID ?? "");
            NlogHelper.Default.Info($"产品图号已更新: {testID}");
        }

        #region 私有辅助方法

        /// <summary>
        /// 添加或更新变量
        /// </summary>
        private static void AddOrUpdateVariable(GlobalVariableManager variableManager, VarItem_Enhanced variable)
        {
            try
            {
                // 检查变量是否已存在
                var existingVar = variableManager.FindVariableByName(variable.VarName);
                if (existingVar != null)
                {
                    // 变量已存在，只更新值
                    variableManager.AddOrUpdateVariable(variable);
                }
                else
                {
                    // 变量不存在，添加新变量
                    variableManager.AddOrUpdateVariable(variable);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"添加/更新变量失败: {variable.VarName}", ex);
            }
        }

        /// <summary>
        /// 更新变量值
        /// </summary>
        private static void UpdateVariableValue(GlobalVariableManager variableManager, string varName, object value)
        {
            try
            {
                var variable = variableManager.FindVariableByName(varName);
                if (variable != null)
                {
                    variableManager.AddOrUpdateVariable(new VarItem_Enhanced
                    {
                        VarName = varName,
                        VarValue = value,
                        VarType = "string",
                    });
                }
                else
                {
                    NlogHelper.Default.Warn($"尝试更新不存在的变量: {varName}");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"更新变量值失败: {varName}", ex);
            }
        }

        /// <summary>
        /// 获取当前试验员
        /// </summary>
        private static string GetCurrentTester()
        {
            try
            {
                return NewUsers.NewUserInfo?.Username ?? "未登录";
            }
            catch
            {
                return "未知";
            }
        }

        /// <summary>
        /// 获取当前产品类型
        /// </summary>
        private static string GetCurrentModelType()
        {
            try
            {
                return VarHelper.TestViewModel?.ModelTypeName ?? "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取当前产品型号
        /// </summary>
        private static string GetCurrentModelName()
        {
            try
            {
                return VarHelper.TestViewModel?.ModelName ?? "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取当前产品图号
        /// </summary>
        private static string GetCurrentTestID()
        {
            try
            {
                return VarHelper.TestViewModel?.DrawingNo ?? "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取当前试验台
        /// </summary>
        private static string GetCurrentTestBench()
        {
            try
            {
                var testBenchId = TestBenchService.CurrentTestBenchID;
                if (testBenchId > 0)
                {
                    var testBench = VarHelper.fsql.Select<TestBenchModel>()
                        .Where(x => x.ID == testBenchId)
                        .First();
                    return testBench?.BenchName ?? "未知试验台";
                }
                return "未选择";
            }
            catch
            {
                return "未知";
            }
        }

        #endregion
    }
}
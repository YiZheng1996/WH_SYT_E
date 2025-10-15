using System.Reflection;

namespace MainUI.Service
{
    /// <summary>
    /// 测试类工厂，用于动态创建测试实例
    /// </summary>
    public static class TestFactory
    {
        private static readonly Dictionary<string, Type> TestTypes = new();

        static TestFactory()
        {
            RegisterTestTypes();
        }

        /// <summary>
        /// 注册所有测试类型
        /// </summary>
        private static void RegisterTestTypes()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var baseType = typeof(BaseTest);

                var testTypes = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t))
                    .ToList();

                foreach (var type in testTypes)
                {
                    TestTypes[type.Name] = type;
                }

                NlogHelper.Default.Info($"已注册 {TestTypes.Count} 个测试类型");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("注册测试类型失败", ex);
            }
        }

        /// <summary>
        /// 根据类名创建测试实例
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>测试实例</returns>
        public static BaseTest CreateTest(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentException("类名不能为空", nameof(className));
            }

            if (!TestTypes.TryGetValue(className, out Type testType))
            {
                throw new ArgumentException($"未找到测试类：{className}", nameof(className));
            }

            try
            {
                return (BaseTest)Activator.CreateInstance(testType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"创建测试实例失败：{className}", ex);
            }
        }

        /// <summary>
        /// 获取所有已注册的测试类型名称
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredTestTypes()
        {
            return TestTypes.Keys.ToList();
        }

        /// <summary>
        /// 检查测试类型是否已注册
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns></returns>
        public static bool IsTestTypeRegistered(string className)
        {
            return TestTypes.ContainsKey(className);
        }
    }
}
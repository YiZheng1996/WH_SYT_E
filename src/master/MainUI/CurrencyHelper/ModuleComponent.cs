using RW.DSL.Modules;
using RW.Log;
using RW.Modules;
using System.Text;

namespace MainUI.CurrencyHelper
{
    /// <summary>
    /// 模块的操作类
    /// </summary>
    public class ModuleComponent
    {
        public static readonly ModuleComponent Instance = new();
        /// <summary>
        /// 获取或设置所有的模块对象，只含公共模块.严禁修改，可通过partial编写扩展方法
        /// </summary>
        public Dictionary<string, BaseModule> Modules { get; set; } = [];

        private static readonly string configPath = Application.StartupPath + @"Modules\MyModules.ini";

        /// <summary>
        /// 模块配置文件路径
        /// </summary>
        public string ConfigPath { get; set; }

        /// <summary>
        /// 初始化所有模块，请注意是否给定了ConfigPath
        /// </summary>
        public void Init()
        {
            try
            {
                // 注册自定义编码提供程序
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding("GB2312");

                if (string.IsNullOrEmpty(ConfigPath))
                    ConfigPath = configPath;
                var full = ConfigPath;
                LogHelper.WriteLine($"初始化模块文件[{full}]。");
                FileInfo f = new(full);
                var filename = f.FullName;
                if (!File.Exists(filename))
                    throw new FileNotFoundException($"模块文件不存在。path:[{filename}]");
                var modules = DSLModuleHelper.GetModulesWithFile<BaseModule>(filename);
                DSLModuleHelper.InitModules(modules.ToDictionary(x => x.Key, x => x.Value as DSLModule));
                Sets(modules);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("模板加载出错" + ex.Message.ToString());
                throw;
            }
        }

        /// <summary>
        /// 模块是否存在
        /// </summary>
        public bool Contains(string key)
        {
            return Modules.ContainsKey(key);
        }

        /// <summary>
        /// 获取指定模块，如果模块不存在将报错。
        /// </summary>
        public BaseModule Get(string moduleName, bool throwError = true)
        {
            if (Modules.TryGetValue(moduleName, out BaseModule value))
                return value;
            else if (!throwError)
                return null;
            else
                throw new Exception($@"找不到模块[{moduleName}]。可能的原因：
                1、模块未在ini文件中配置。
                2、ini文件编码格式不正确。
                3、模块未初始化。
                请联系开发者确认问题。");
        }

        /// <summary>
        /// 读取指定模块的指定点
        /// </summary>
        public T Read<T>(string moduleName, string key)
        {
            var module = Get(moduleName);
            return module.Read<T>(key);
        }

        /// <summary>
        /// 读取指定模块的指定点
        /// </summary>
        public T Read<T>(string fullTagName)
        {
            var arr = fullTagName.Split('.', ' ');
            if (arr.Length != 2)
                throw new ArgumentException($"点位格式不正确，应该是'moduleName.tagName'或'moduleName tagName'，实际值为[{fullTagName}]");
            return Read<T>(arr[0], arr[1]);
        }

        /// <summary>
        /// 获取当前设置的所有模块
        /// </summary>
        public Dictionary<string, BaseModule> GetList()
        {
            return Modules;
        }

        /// <summary>
        /// 设置模块
        /// </summary>
        public void Sets(Dictionary<string, BaseModule> ms)
        {
            foreach (var item in ms)
            {
                Set(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 单独设置一个模块
        /// </summary>
        public void Set(string key, BaseModule value)
        {
            Modules[key] = value;
        }

        /// <summary>
        /// 关闭OPC连接
        /// </summary>
        public void Close()
        {
            if (Modules.Count > 0)
            {
                foreach (var item in Modules)
                {
                    item.Value.Driver.Close();
                }
            }
        }

    }
}

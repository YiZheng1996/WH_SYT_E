using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.Services;
using Newtonsoft.Json;
using System.Text;

namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// JSON数据访问管理器
    /// </summary>
    public class JsonManager
    {
        // Json设置
        private static readonly JsonSerializerSettings _jsonSettings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        // Json文件路径
        public static string FilePath { get; set; }

        /// <summary>
        /// Json配置数据结构
        /// </summary>
        public class JsonConfig
        {
            public SystemInfo System { get; set; } = new();
            public List<Parent> Form { get; set; } = [];
            public List<VarItem> Variable { get; set; } = [];

            public class SystemInfo
            {
                public string CreateTime { get; set; } = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                public string ProjectName { get; set; } = string.Empty;
            }
        }

        /// <summary>
        /// 读取或创建Json配置
        /// </summary>
        public static async Task<JsonConfig> GetOrCreateConfigAsync()
        {
            if (!File.Exists(FilePath))
            {
                var config = new JsonConfig();
                await SaveConfigAsync(config);
                return config;
            }

            try
            {
                string jsonString = await File.ReadAllTextAsync(FilePath);
                var config = JsonConvert.DeserializeObject<JsonConfig>(jsonString, _jsonSettings);
                return config ?? new JsonConfig();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("读取Json配置失败", ex);
                return new JsonConfig();
            }
        }

        /// <summary>
        /// 保存Json配置
        /// </summary>
        private static async Task SaveConfigAsync(JsonConfig config)
        {
            try
            {
                // 注册自定义编码提供程序
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                
                string json = JsonConvert.SerializeObject(config, _jsonSettings);
                
                // 使用 StreamWriter 写入文件，确保正确的编码
                using var stream = new FileStream(FilePath, FileMode.Create, 
                    FileAccess.Write, FileShare.None);
                using var writer = new StreamWriter(stream, new UTF8Encoding(true)); 
                await writer.WriteAsync(json);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存Json配置失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 更新配置中的特定部分
        /// </summary>
        public static async Task UpdateConfigAsync(Func<JsonConfig, Task> updateAction)
        {
            var config = await GetOrCreateConfigAsync();
            await updateAction(config);
            await SaveConfigAsync(config);
        }

        #region Form操作
        public static async Task AddParentAsync(IWorkflowStateService singleton)
        {
            await UpdateConfigAsync(async config =>
            {
                var parent = new Parent
                {
                    ModelTypeName = singleton.ModelTypeName,
                    ModelName = singleton.ModelName,
                    ItemName = singleton.ItemName,
                    ChildSteps = []
                };
                config.Form.Add(parent);
                await Task.CompletedTask;
            });
        }

        public static async Task<List<Parent>> ReadParentAsync()
        {
            var config = await GetOrCreateConfigAsync();
            return config.Form;
        }

        public static async Task AddChildAsync(IWorkflowStateService singleton, ChildModel item)
        {
            await UpdateConfigAsync(async config =>
            {
                var parent = config.Form.FirstOrDefault(p =>
                    p.ModelTypeName == singleton.ModelTypeName &&
                    p.ModelName == singleton.ModelName &&
                    p.ItemName == singleton.ItemName);

                if (parent != null)
                {
                    parent.ChildSteps ??= [];
                    parent.ChildSteps.Add(item);
                }
                await Task.CompletedTask;
            });
        }
        #endregion

        #region 变量操作
        /// <summary>
        /// 读取所有自定义变量列表
        /// </summary>
        /// <returns></returns>
        public static async Task<List<VarItem>> ReadVarItemsAsync()
        {
            var config = await GetOrCreateConfigAsync();
            return config.Variable;
        }
        /// <summary>
        /// 添加自定义变量
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static async Task AddVarItemAsync(VarItem item)
        {
            await UpdateConfigAsync(async config =>
            {
                config.Variable.Add(item);
                await Task.CompletedTask;
            });
        }
        #endregion
    }
}
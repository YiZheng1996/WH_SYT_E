using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.Services;
using Newtonsoft.Json;
using System.Text;

namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// JSON���ݷ��ʹ�����
    /// </summary>
    public class JsonManager
    {
        // Json����
        private static readonly JsonSerializerSettings _jsonSettings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        // Json�ļ�·��
        public static string FilePath { get; set; }

        /// <summary>
        /// Json�������ݽṹ
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
        /// ��ȡ�򴴽�Json����
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
                NlogHelper.Default.Error("��ȡJson����ʧ��", ex);
                return new JsonConfig();
            }
        }

        /// <summary>
        /// ����Json����
        /// </summary>
        private static async Task SaveConfigAsync(JsonConfig config)
        {
            try
            {
                // ע���Զ�������ṩ����
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                
                string json = JsonConvert.SerializeObject(config, _jsonSettings);
                
                // ʹ�� StreamWriter д���ļ���ȷ����ȷ�ı���
                using var stream = new FileStream(FilePath, FileMode.Create, 
                    FileAccess.Write, FileShare.None);
                using var writer = new StreamWriter(stream, new UTF8Encoding(true)); 
                await writer.WriteAsync(json);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("����Json����ʧ��", ex);
                throw;
            }
        }

        /// <summary>
        /// ���������е��ض�����
        /// </summary>
        public static async Task UpdateConfigAsync(Func<JsonConfig, Task> updateAction)
        {
            var config = await GetOrCreateConfigAsync();
            await updateAction(config);
            await SaveConfigAsync(config);
        }

        #region Form����
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

        #region ��������
        /// <summary>
        /// ��ȡ�����Զ�������б�
        /// </summary>
        /// <returns></returns>
        public static async Task<List<VarItem>> ReadVarItemsAsync()
        {
            var config = await GetOrCreateConfigAsync();
            return config.Variable;
        }
        /// <summary>
        /// ����Զ������
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
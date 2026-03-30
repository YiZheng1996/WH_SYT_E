namespace MainUI.Service
{
    /// <summary>
    /// 产品路径工具类
    /// 统一管理工作流配置文件路径的构建逻辑
    /// 
    /// 路径规则: Procedure/{类型名}/{型号ID}_{型号名}/{项点名}.json
    /// 示例: Procedure/阀门类/5_ABC-100/气密性检测.json
    /// </summary>
    public static class ProductPathHelper
    {
        /// <summary>
        /// 构建产品文件夹名称
        /// 格式: {ID}_{ModelName}
        /// </summary>
        public static string BuildProductFolderName(int modelId, string modelName)
        {
            if (modelId <= 0)
                throw new ArgumentException("型号ID无效", nameof(modelId));
            if (string.IsNullOrWhiteSpace(modelName))
                throw new ArgumentException("型号名称不能为空", nameof(modelName));

            return $"{modelId}_{SanitizePath(modelName)}";
        }

        /// <summary>
        /// 从 NewModels 对象构建产品文件夹名称
        /// </summary>
        public static string BuildProductFolderName(NewModels model)
        {
            ArgumentNullException.ThrowIfNull(model);
            return BuildProductFolderName(model.ID, model.ModelName);
        }

        /// <summary>
        /// 构建工作流JSON配置文件的完整路径
        /// </summary>
        /// <param name="modelTypeName">产品类型名称</param>
        /// <param name="modelId">型号数据库ID</param>
        /// <param name="modelName">型号名称</param>
        /// <param name="processName">试验项点名称</param>
        /// <returns>完整的JSON文件路径</returns>
        public static string BuildJsonPath(string modelTypeName, int modelId,
            string modelName, string processName)
        {
            string modelPath = BuildModelPath(modelTypeName, modelId, modelName);
            return Path.Combine(modelPath, $"{SanitizePath(processName)}.json");
        }

        /// <summary>
        /// 从 NewModels 对象构建JSON路径
        /// </summary>
        public static string BuildJsonPath(NewModels model, string processName)
        {
            ArgumentNullException.ThrowIfNull(model);
            return BuildJsonPath(model.ModelTypeName, model.ID, model.ModelName, processName);
        }

        /// <summary>
        /// 根据当前选中的产品构建JSON路径
        /// </summary>
        public static string BuildJsonPathFromCurrent(string processName)
        {
            var vm = VarHelper.TestViewModel
                ?? throw new InvalidOperationException("未选择产品型号");

            return BuildJsonPath(vm.ModelTypeName, vm.ID, vm.ModelName, processName);
        }

        /// <summary>
        /// 构建工作流配置文件所在的目录路径
        /// </summary>
        public static string BuildModelPath(string modelTypeName, int modelId, string modelName)
        {
            return Path.Combine(
                Application.StartupPath, "Procedure",
                SanitizePath(modelTypeName),
                BuildProductFolderName(modelId, modelName));
        }

        /// <summary>
        /// 构建参数配置的 SectionName
        /// 使用 ID 确保唯一性
        /// </summary>
        public static string BuildParaConfigSectionName(string modelTypeName, int modelId)
        {
            return $"{modelTypeName}_{modelId}";
        }

        /// <summary>
        /// 从当前选中产品构建 SectionName
        /// </summary>
        public static string BuildParaConfigSectionNameFromCurrent()
        {
            var vm = VarHelper.TestViewModel;
            if (vm == null) return "";
            return BuildParaConfigSectionName(vm.ModelTypeName, vm.ID);
        }

        /// <summary>
        /// 清理路径中的非法字符
        /// </summary>
        private static string SanitizePath(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "_";

            string result = input.Trim();
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                result = result.Replace(c, '_');
            }
            return result;
        }
    }
}
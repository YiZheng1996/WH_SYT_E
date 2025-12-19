namespace MainUI.LogicalConfiguration.Services
{
    /// <summary>
    /// 窗体服务接口，负责窗体的创建和管理
    /// </summary>
    public interface IFormService
    {
        /// <summary>
        /// 根据名称打开窗体
        /// </summary>
        /// <param name="formName">窗体名称</param>
        /// <param name="parent">父窗体</param>
        void OpenFormByName(Form parentform, string formName, Form parent = null);

        /// <summary>
        /// 创建指定类型的窗体
        /// </summary>
        /// <typeparam name="T">窗体类型</typeparam>
        /// <returns>窗体实例</returns>
        T CreateForm<T>() where T : Form;

        /// <summary>
        /// 创建逻辑配置窗体
        /// </summary>
        FrmLogicalConfiguration CreateLogicalConfigurationForm(
            string path, string modelType, string modelName, string processName);
    }
}

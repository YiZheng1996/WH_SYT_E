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
        /// 根据名称打开窗体并返回配置结果(用于BaseStepConfigForm内部打开子步骤配置)
        /// </summary>
        /// <param name="parentForm">父窗体</param>
        /// <param name="formName">窗体名称(步骤名称)</param>
        /// <param name="currentParameter">当前参数(JSON字符串或对象)</param>
        /// <returns>对话框结果和更新后的参数</returns>
        (DialogResult result, object parameter) OpenFormByNameWithResult(
            Form parentForm, string formName, object currentParameter);

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

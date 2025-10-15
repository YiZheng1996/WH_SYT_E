namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 参数表单泛型接口
    /// </summary>
    public interface IParameterForm<T> where T : class, new()
    {
        /// <summary>
        /// 参数对象
        /// </summary>
        T Parameter { get; set; }

        /// <summary>
        /// 填充控件
        /// </summary>
        void PopulateControls(T parameter);

        /// <summary>
        /// 设置默认值
        /// </summary>
        void SetDefaultValues();

        /// <summary>
        /// 验证参数
        /// </summary>
        bool ValidateTypedParameters();

        /// <summary>
        /// 收集参数
        /// </summary>
        T CollectTypedParameters();

        /// <summary>
        /// 转换参数
        /// </summary>
        T ConvertParameter(object stepParameter);
    }
}
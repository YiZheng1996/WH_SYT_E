using MainUI.LogicalConfiguration.Instrument.Models;

namespace MainUI.LogicalConfiguration.Instrument.Services
{
    /// <summary>
    /// 仪器驱动管理服务接口
    /// </summary>
    public interface IInstrumentDriverService
    {
        /// <summary>
        /// 获取所有启用的仪器驱动（用于下拉选择等场景）
        /// </summary>
        Task<List<InstrumentDriver>> GetAllDriversAsync();

        /// <summary>
        /// 获取所有仪器驱动（包括禁用的，用于管理界面）
        /// </summary>
        Task<List<InstrumentDriver>> GetAllDriversIncludingDisabledAsync();

        /// <summary>
        /// 根据ID获取仪器驱动
        /// </summary>
        Task<InstrumentDriver> GetDriverByIdAsync(string driverId);

        /// <summary>
        /// 根据名称获取仪器驱动
        /// </summary>
        Task<InstrumentDriver> GetDriverByNameAsync(string name);

        /// <summary>
        /// 根据类别获取仪器驱动列表
        /// </summary>
        Task<List<InstrumentDriver>> GetDriversByCategoryAsync(InstrumentCategory category);

        /// <summary>
        /// 添加仪器驱动
        /// </summary>
        Task<bool> AddDriverAsync(InstrumentDriver driver);

        /// <summary>
        /// 更新仪器驱动
        /// </summary>
        Task<bool> UpdateDriverAsync(InstrumentDriver driver);

        /// <summary>
        /// 删除仪器驱动
        /// </summary>
        Task<bool> DeleteDriverAsync(string driverId);

        /// <summary>
        /// 导出仪器驱动到文件
        /// </summary>
        Task<bool> ExportDriverAsync(string driverId, string filePath);

        /// <summary>
        /// 导出所有驱动到文件
        /// </summary>
        Task<bool> ExportAllDriversAsync(string filePath);

        /// <summary>
        /// 从文件导入仪器驱动
        /// </summary>
        Task<InstrumentDriver> ImportDriverAsync(string filePath);

        /// <summary>
        /// 复制仪器驱动
        /// </summary>
        Task<InstrumentDriver> CloneDriverAsync(string driverId);

        /// <summary>
        /// 保存所有配置
        /// </summary>
        Task<bool> SaveAsync();

        /// <summary>
        /// 重新加载配置
        /// </summary>
        Task<bool> ReloadAsync();

        /// <summary>
        /// 驱动变更事件
        /// </summary>
        event Action DriversChanged;
    }
}
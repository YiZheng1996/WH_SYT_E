using static MainUI.LogicalConfiguration.LogicalManager.GlobalVariableManager;

namespace MainUI.LogicalConfiguration.Services
{
    /// <summary>
    /// 工作流状态管理服务接口
    /// 这个接口定义了工作流状态管理的所有核心功能，用于替代直接使用 SingletonStatus
    /// 优势：
    /// 1. 支持依赖注入，便于单元测试
    /// 2. 接口隔离，降低耦合度
    /// 3. 更好的线程安全保证
    /// 4. 清晰的职责边界
    /// </summary>
    public interface IWorkflowStateService
    {
        #region 基础配置属性

        /// <summary>
        /// 产品类型名称
        /// 用于标识当前工作流所属的产品类型
        /// </summary>
        string ModelTypeName { get; set; }

        /// <summary>
        /// 产品型号名称
        /// 用于标识具体的产品型号
        /// </summary>
        string ModelName { get; set; }

        /// <summary>
        /// 项目名称
        /// 用于标识当前的测试项目或工作项
        /// </summary>
        string ItemName { get; set; }

        /// <summary>
        /// 当前步骤名称
        /// 用于显示或记录当前正在执行的步骤
        /// </summary>
        string StepName { get; set; }

        /// <summary>
        /// 通用整数属性
        /// 可用于存储临时的数值数据
        /// </summary>
        int T { get; set; }

        /// <summary>
        /// 当前操作状态
        /// true 表示操作成功，false 表示操作失败
        /// </summary>
        bool Status { get; set; }

        /// <summary>
        /// 当前步骤序号
        /// 用于标识当前正在执行或选中的步骤索引
        /// 设置时会触发 StepNumChanged 事件
        /// </summary>
        int StepNum { get; set; }

        /// <summary>
        /// 是否应该跳出循环（Break）
        /// </summary>
        bool ShouldBreakLoop { get; set; }

        /// <summary>
        /// 是否应该继续下一次循环（Continue）
        /// </summary>
        bool ShouldContinueLoop { get; set; }
        #endregion

        #region 配置管理方法

        /// <summary>
        /// 批量更新工作流配置信息
        /// 这是一个原子操作，要么全部成功，要么全部失败
        /// </summary>
        /// <param name="modelType">产品类型</param>
        /// <param name="modelName">产品型号</param>
        /// <param name="itemName">项目名称</param>
        void UpdateConfiguration(string modelType, string modelName, string itemName);

        #endregion

        #region 步骤管理方法

        /// <summary>
        /// 添加新的工作流步骤
        /// 注意：这是线程安全的操作
        /// </summary>
        /// <param name="step">要添加的步骤对象</param>
        /// <exception cref="ArgumentNullException">当 step 为 null 时抛出</exception>
        void AddStep(ChildModel step);

        /// <summary>
        /// 移除指定的工作流步骤
        /// </summary>
        /// <param name="step">要移除的步骤对象</param>
        /// <returns>如果成功移除返回 true，否则返回 false</returns>
        bool RemoveStep(ChildModel step);

        /// <summary>
        /// 根据索引移除工作流步骤
        /// 这比 RemoveStep(ChildModel) 更高效，因为不需要遍历查找
        /// </summary>
        /// <param name="index">要移除的步骤索引</param>
        /// <returns>如果成功移除返回 true，如果索引无效返回 false</returns>
        bool RemoveStepAt(int index);

        /// <summary>
        /// 清空所有工作流步骤
        /// 谨慎使用：这会删除所有已配置的步骤
        /// </summary>
        void ClearSteps();

        /// <summary>
        /// 获取所有工作流步骤的副本
        /// 返回的是副本，因此可以安全地遍历而不担心并发修改
        /// </summary>
        /// <returns>步骤列表的副本</returns>
        List<ChildModel> GetSteps();

        /// <summary>
        /// 获取指定索引的工作流步骤
        /// </summary>
        /// <param name="index">步骤索引</param>
        /// <returns>指定的步骤对象，如果索引无效则返回 null</returns>
        ChildModel GetStep(int index);

        /// <summary>
        /// 获取工作流步骤总数
        /// 这是一个线程安全的操作
        /// </summary>
        /// <returns>步骤总数</returns>
        int GetStepCount();

        /// <summary>
        /// 验证步骤索引是否有效
        /// 用于在访问步骤前进行边界检查
        /// </summary>
        /// <param name="stepIndex">要验证的步骤索引</param>
        /// <returns>如果索引有效返回 true，否则返回 false</returns>
        bool ValidateStepIndex(int stepIndex);

        /// <summary>
        /// 更新指定步骤的参数
        /// </summary>
        /// <param name="stepIndex">步骤索引</param>
        /// <param name="parameter">新的参数对象</param>
        void UpdateStepParameter(int stepIndex, object parameter);
        #endregion

        #region 变量管理方法

        /// <summary>
        /// 添加变量到工作流状态
        /// 支持添加任何类型的变量对象
        /// </summary>
        /// <param name="variable">要添加的变量对象</param>
        /// <exception cref="ArgumentNullException">当 variable 为 null 时抛出</exception>
        void AddVariable(object variable);

        /// <summary>
        /// 从工作流状态中移除指定变量
        /// </summary>
        /// <param name="variable">要移除的变量对象</param>
        /// <returns>如果成功移除返回 true，否则返回 false</returns>
        bool RemoveVariable(object variable);

        /// <summary>
        /// 清空所有变量
        /// 谨慎使用：这会删除所有已定义的变量
        /// </summary>
        void ClearAllVariables();

        /// <summary>
        /// 清空所有用户变量(保留系统变量)
        /// </summary>
        void ClearUserVariables();

        /// <summary>
        /// 获取指定类型的所有变量
        /// 这是一个泛型方法，支持类型安全的变量查询
        /// </summary>
        /// <typeparam name="T">变量类型</typeparam>
        /// <returns>指定类型的变量列表</returns>
        List<T> GetVariables<T>() where T : class;

        /// <summary>
        /// 根据条件查找指定类型的变量
        /// 使用 Func 委托进行灵活的条件查询
        /// </summary>
        /// <typeparam name="T">变量类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <returns>符合条件的第一个变量，如果没有找到则返回 null</returns>
        T FindVariable<T>(Func<T, bool> predicate) where T : class;

        /// <summary>
        /// 获取所有 VarItem_Enhanced 类型的变量，排除了系统全局变量
        /// 这是一个便捷方法，等同于 GetVariables&lt;VarItem_Enhanced&gt;()
        /// </summary>
        /// <returns>所有增强型变量的列表</returns>
        List<VarItem_Enhanced> GetAllVariables();

        /// <summary>
        /// 根据变量名查找 VarItem_Enhanced 变量
        /// 这是最常用的变量查找方法
        /// </summary>
        /// <param name="varName">变量名</param>
        /// <returns>找到的变量对象，如果不存在则返回 null</returns>
        VarItem_Enhanced FindVariableByName(string varName);

        /// <summary>
        /// 标记变量被步骤赋值占用
        /// 用于跟踪变量的赋值状态和冲突检测
        /// </summary>
        /// <param name="varName">变量名</param>
        /// <param name="stepIndex">步骤索引</param>
        /// <param name="stepInfo">步骤信息描述</param>
        /// <param name="assignmentType">赋值类型</param>
        void MarkVariableAssignedByStep(string varName, int stepIndex, string stepInfo, VariableAssignmentType assignmentType);

        /// <summary>
        /// 清除变量的赋值标记
        /// 当步骤被删除或赋值配置被移除时调用
        /// </summary>
        /// <param name="varName">变量名</param>
        /// <param name="stepIndex">步骤索引，用于验证只能清除自己标记的变量</param>
        void ClearVariableAssignmentMark(string varName, int stepIndex);

        /// <summary>
        /// 检查变量赋值冲突
        /// 检测变量是否已被其他步骤赋值
        /// </summary>
        /// <param name="varName">变量名</param>
        /// <param name="excludeStepIndex">排除的步骤索引（通常是当前步骤）</param>
        /// <returns>冲突检查结果</returns>
        VariableConflictInfo CheckVariableAssignmentConflict(string varName, int excludeStepIndex);

        /// <summary>
        /// 批量检查多个变量的赋值冲突
        /// </summary>
        /// <param name="varNames">变量名列表</param>
        /// <param name="excludeStepIndex">排除的步骤索引</param>
        /// <returns>冲突信息字典，key为变量名</returns>
        Dictionary<string, VariableConflictInfo> CheckMultipleVariableConflicts(List<string> varNames, int excludeStepIndex);

        /// <summary>
        /// 获取被指定步骤赋值的所有变量
        /// </summary>
        /// <param name="stepIndex">步骤索引</param>
        /// <returns>被该步骤赋值的变量列表</returns>
        List<VarItem_Enhanced> GetVariablesAssignedByStep(int stepIndex);
        #endregion

        #region 事件通知

        /// <summary>
        /// 步骤序号变更事件
        /// 当 StepNum 属性发生变化时触发
        /// 参数为新的步骤序号
        /// </summary>
        event Action<int> StepNumChanged;

        /// <summary>
        /// 变量添加事件
        /// 当有新变量添加到工作流状态时触发
        /// 参数为被添加的变量对象
        /// </summary>
        event Action<object> VariableAdded;

        /// <summary>
        /// 变量移除事件
        /// 当变量从工作流状态中移除时触发
        /// 参数为被移除的变量对象
        /// </summary>
        event Action<object> VariableRemoved;

        /// <summary>
        /// 步骤添加事件
        /// 当有新步骤添加到工作流时触发
        /// 参数为被添加的步骤对象
        /// </summary>
        event Action<ChildModel> StepAdded;

        /// <summary>
        /// 步骤移除事件
        /// 当步骤从工作流中移除时触发
        /// 参数为被移除的步骤对象
        /// </summary>
        event Action<ChildModel> StepRemoved;

        /// <summary>
        /// 当步骤集合发生批量变更时触发（用于批量操作后刷新UI）
        /// 使用场景：ClearSteps、批量插入、批量排序等
        /// </summary>
        event Action StepsChanged;

        #endregion

        #region 状态管理和诊断

        /// <summary>
        /// 重置工作流状态到初始状态
        /// 这会清空所有数据并重置所有属性
        /// 谨慎使用：这是不可逆的操作
        /// </summary>
        void Reset();

        /// <summary>
        /// 获取当前工作流状态的诊断信息
        /// 用于调试和监控，返回包含各种状态信息的字符串
        /// </summary>
        /// <returns>诊断信息字符串</returns>
        string GetDiagnosticInfo();

        #endregion
    }
}

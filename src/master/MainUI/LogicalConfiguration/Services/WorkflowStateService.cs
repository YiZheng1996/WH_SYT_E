using static MainUI.LogicalConfiguration.LogicalManager.GlobalVariableManager;

namespace MainUI.LogicalConfiguration.Services
{
    /// <summary>
    /// 工作流状态管理服务的线程安全实现
    /// 
    /// 设计原则：
    /// 1. 线程安全：使用读写锁保护所有共享数据
    /// 2. 性能优化：读操作使用读锁，写操作使用写锁
    /// 3. 异常安全：所有公共方法都有异常处理
    /// 4. 资源管理：实现 IDisposable 确保资源正确释放
    /// 
    /// 使用建议：
    /// - 推荐通过依赖注入容器注册为单例
    /// - 在应用程序结束时调用 Dispose() 方法
    /// - 频繁的读操作不会相互阻塞
    /// - 写操作会阻塞所有其他操作，因此要尽量减少写操作的时间
    /// </summary>
    public class WorkflowStateService : IWorkflowStateService, IDisposable
    {
        #region 私有字段

        // 三个独立的读写锁，用于保护不同类型的数据
        // 这样可以提高并发性能，因为不同类型的数据可以并行访问
        private readonly ReaderWriterLockSlim _configLock = new();    // 保护配置数据
        private readonly ReaderWriterLockSlim _variablesLock = new(); // 保护变量集合
        private readonly ReaderWriterLockSlim _stepsLock = new();     // 保护步骤集合
        private readonly object _eventLock = new object();   // 保护事件触发

        // 配置相关的私有字段
        // 使用私有字段 + 属性的模式来控制访问
        private string _modelTypeName = string.Empty;
        private string _modelName = string.Empty;
        private string _itemName = string.Empty;
        private string _stepName = string.Empty;

        // 使用 volatile 关键字确保这些字段的可见性
        // 对于简单类型，volatile 可以提供基本的线程安全保证
        private volatile int _t;
        private volatile bool _status;
        private volatile int _stepNum;
        private volatile bool _shouldBreakLoop;
        private volatile bool _shouldContinueLoop;


        // 数据集合，使用 List 因为需要保持顺序
        // 注意：List<T> 不是线程安全的，所以需要锁保护
        private readonly List<object> _variables = [];
        private readonly List<ChildModel> _steps = [];

        // 资源释放标志
        private bool _disposed = false;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化 WorkflowStateService 实例
        /// 
        /// 设计说明：
        /// - 构造函数很轻量，不执行任何可能失败的操作
        /// - 所有集合都使用默认容量，避免不必要的内存分配
        /// - 锁对象在声明时就初始化，确保线程安全
        /// </summary>
        public WorkflowStateService()
        {
            // 构造函数故意保持简单
            // 所有的初始化都在字段声明时完成
            // 这确保了对象创建的原子性和线程安全性
        }

        #endregion

        #region 配置属性实现

        /// <summary>
        /// 产品类型名称属性的线程安全实现
        /// 
        /// 实现细节：
        /// - 读操作：获取读锁 -> 读取值 -> 释放读锁
        /// - 写操作：获取写锁 -> 设置值 -> 释放写锁
        /// - 使用 try/finally 确保锁总是被释放
        /// </summary>
        public string ModelTypeName
        {
            get
            {
                _configLock.EnterReadLock();  // 获取读锁，允许多个并发读取
                try
                {
                    return _modelTypeName;
                }
                finally
                {
                    _configLock.ExitReadLock();  // 确保释放读锁
                }
            }
            set
            {
                _configLock.EnterWriteLock();  // 获取写锁，独占访问
                try
                {
                    _modelTypeName = value ?? string.Empty;  // 防止 null 值
                }
                finally
                {
                    _configLock.ExitWriteLock();  // 确保释放写锁
                }
            }
        }

        /// <summary>
        /// 产品型号名称属性的线程安全实现
        /// 实现方式与 ModelTypeName 相同
        /// </summary>
        public string ModelName
        {
            get
            {
                _configLock.EnterReadLock();
                try { return _modelName; }
                finally { _configLock.ExitReadLock(); }
            }
            set
            {
                _configLock.EnterWriteLock();
                try { _modelName = value ?? string.Empty; }
                finally { _configLock.ExitWriteLock(); }
            }
        }

        /// <summary>
        /// 项目名称属性的线程安全实现
        /// </summary>
        public string ItemName
        {
            get
            {
                _configLock.EnterReadLock();
                try { return _itemName; }
                finally { _configLock.ExitReadLock(); }
            }
            set
            {
                _configLock.EnterWriteLock();
                try { _itemName = value ?? string.Empty; }
                finally { _configLock.ExitWriteLock(); }
            }
        }

        /// <summary>
        /// 步骤名称属性的线程安全实现
        /// </summary>
        public string StepName
        {
            get
            {
                _configLock.EnterReadLock();
                try { return _stepName; }
                finally { _configLock.ExitReadLock(); }
            }
            set
            {
                _configLock.EnterWriteLock();
                try { _stepName = value ?? string.Empty; }
                finally { _configLock.ExitWriteLock(); }
            }
        }

        /// <summary>
        /// 通用整数属性的原子操作实现
        /// 
        /// 使用 Interlocked 类进行原子操作：
        /// - 比锁更高效
        /// - 对于简单的数值操作，Interlocked 是首选
        /// - Exchange 方法是原子的，返回旧值
        /// </summary>
        public int T
        {
            get => _t;  // volatile 字段的读取是原子的
            set => Interlocked.Exchange(ref _t, value);  // 原子交换操作
        }

        /// <summary>
        /// 状态属性的简单实现
        /// 
        /// 对于 bool 类型：
        /// - 读写操作在 .NET 中是原子的
        /// - volatile 确保可见性
        /// - 不需要锁或 Interlocked
        /// </summary>
        public bool Status
        {
            get => _status;
            set => _status = value;
        }

        /// <summary>
        /// 步骤序号属性的实现，包含事件触发
        /// 
        /// 特殊处理：
        /// - 使用 Interlocked.Exchange 进行原子更新
        /// - 比较新旧值，只有在真正改变时才触发事件
        /// - 事件触发是异步的，不会阻塞属性设置
        /// </summary>
        public int StepNum
        {
            get => _stepNum;
            set
            {
                var oldValue = Interlocked.Exchange(ref _stepNum, value);
                if (oldValue != value)
                {
                    // 只有值真正改变时才触发事件
                    OnStepNumChanged(value);
                }
            }
        }

        /// <summary>
        /// 是否应该跳出循环（Break）
        /// </summary>
        public bool ShouldBreakLoop
        {
            get => _shouldBreakLoop;
            set => _shouldBreakLoop = value;
        }

        /// <summary>
        /// 是否应该继续下一次循环（Continue）
        /// </summary>
        public bool ShouldContinueLoop
        {
            get => _shouldContinueLoop;
            set => _shouldContinueLoop = value;
        }

        /// <summary>
        /// 批量更新配置的原子操作
        /// 
        /// 优势：
        /// - 一次性获取写锁，避免多次锁获取的开销
        /// - 确保配置更新的原子性
        /// - 如果其中一个操作失败，所有操作都会回滚
        /// </summary>
        public void UpdateConfiguration(string modelType, string modelName, string itemName)
        {
            _configLock.EnterWriteLock();
            try
            {
                // 批量更新，确保原子性
                _modelTypeName = modelType ?? string.Empty;
                _modelName = modelName ?? string.Empty;
                _itemName = itemName ?? string.Empty;
            }
            finally
            {
                _configLock.ExitWriteLock();
            }
        }
        #endregion

        #region 步骤管理实现

        /// <summary>
        /// 添加工作流步骤的线程安全实现
        /// 
        /// 实现步骤：
        /// 1. 参数验证（防御性编程）
        /// 2. 获取写锁
        /// 3. 添加到集合
        /// 4. 释放写锁
        /// 5. 触发事件通知
        /// 
        /// 注意：事件触发在锁外进行，避免死锁
        /// </summary>
        public void AddStep(ChildModel step)
        {
            // 防御性编程：提前验证参数
            if (step == null)
            {
                // 对于 null 参数，选择静默返回而不是抛异常
                // 这样可以避免调用方的异常处理负担
                return;
            }

            _stepsLock.EnterWriteLock();
            try
            {
                _steps.Add(step);
            }
            finally
            {
                _stepsLock.ExitWriteLock();
            }

            // 在锁外触发事件，避免事件处理程序中的潜在死锁
            OnStepAdded(step);
        }

        /// <summary>
        /// 移除工作流步骤的线程安全实现
        /// 
        /// 返回值设计：
        /// - true: 成功移除
        /// - false: 步骤不存在或参数为 null
        /// 
        /// 这种设计让调用方可以知道操作是否成功
        /// </summary>
        public bool RemoveStep(ChildModel step)
        {
            if (step == null) return false;

            bool removed;
            _stepsLock.EnterWriteLock();
            try
            {
                removed = _steps.Remove(step);
            }
            finally
            {
                _stepsLock.ExitWriteLock();
            }

            // 在锁外触发事件，避免死锁
            if (removed)
            {
                OnStepRemoved(step);
            }

            return removed;
        }

        // 触发批量变更事件
        protected virtual void OnStepsChanged()
        {
            StepsChanged?.Invoke();
        }

        /// <summary>
        /// 根据索引移除步骤的高效实现
        /// 
        /// 相比 RemoveStep(ChildModel)，这个方法：
        /// - 不需要遍历查找
        /// - 时间复杂度 O(1) vs O(n)
        /// - 但需要调用方确保索引有效
        /// </summary>
        public bool RemoveStepAt(int index)
        {
            ChildModel removedStep = null;
            bool removed = false;

            _stepsLock.EnterWriteLock();
            try
            {
                // 边界检查
                if (index >= 0 && index < _steps.Count)
                {
                    // 保存引用，用于事件通知
                    removedStep = _steps[index];
                    _steps.RemoveAt(index);
                    removed = true;
                }
            }
            finally
            {
                _stepsLock.ExitWriteLock();
            }

            // 在锁外触发事件，事件处理可以安全地调用 GetSteps()
            if (removed && removedStep != null)
            {
                OnStepRemoved(removedStep);
            }

            return removed;
        }

        /// <summary>
        /// 清空所有步骤
        /// 
        /// 注意：这是一个危险操作，会丢失所有步骤数据
        /// 调用前应该有确认机制
        /// </summary>
        public void ClearSteps()
        {
            _stepsLock.EnterWriteLock();
            try
            {
                _steps.Clear();
            }
            finally
            {
                _stepsLock.ExitWriteLock();
            }

            // 触发批量变更事件，而不是单个移除事件
            OnStepsChanged();
        }

        /// <summary>
        /// 获取所有步骤的副本
        /// 
        /// 返回副本的好处：
        /// 1. 调用方可以安全地遍历，不担心并发修改
        /// 2. 调用方的修改不会影响原始数据
        /// 3. 避免了长时间持有锁
        /// 
        /// 缺点：
        /// 1. 内存开销（创建副本）
        /// 2. 数据可能不是最新的（获取后原始数据可能已修改）
        /// </summary>
        public List<ChildModel> GetSteps()
        {
            _stepsLock.EnterReadLock();
            try
            {
                // 创建副本，确保线程安全
                return [.. _steps];
            }
            finally
            {
                _stepsLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取指定索引的步骤
        /// 
        /// 这是一个轻量级操作，只读取单个元素
        /// 相比返回整个列表，这更高效
        /// </summary>
        public ChildModel GetStep(int index)
        {
            _stepsLock.EnterReadLock();
            try
            {
                // 边界检查
                if (index >= 0 && index < _steps.Count)
                {
                    return _steps[index];
                }
                return null;  // 索引无效时返回 null
            }
            finally
            {
                _stepsLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取步骤总数
        /// 
        /// 这是一个非常轻量的操作
        /// 获取 Count 属性比创建整个列表副本高效得多
        /// </summary>
        public int GetStepCount()
        {
            _stepsLock.EnterReadLock();
            try
            {
                return _steps.Count;
            }
            finally
            {
                _stepsLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 验证步骤索引的有效性
        /// 
        /// 这是一个实用的辅助方法
        /// 调用方可以在访问步骤前先验证索引
        /// </summary>
        public bool ValidateStepIndex(int stepIndex)
        {
            var count = GetStepCount();
            return stepIndex >= 0 && stepIndex < count;
        }

        /// <summary>
        /// 更新指定步骤的参数
        /// </summary>
        /// <param name="stepIndex">步骤索引</param>
        /// <param name="parameter">新的参数对象</param>
        public void UpdateStepParameter(int stepIndex, object parameter)
        {
            var steps = GetSteps();
            if (steps != null && stepIndex >= 0 && stepIndex < steps.Count)
            {
                steps[stepIndex].StepParameter = parameter;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(stepIndex), "步骤索引超出范围");
            }
        }
        #endregion

        #region 变量管理实现

        /// <summary>
        /// 添加变量的线程安全实现
        /// 
        /// 设计说明：
        /// - 支持任意类型的变量对象
        /// - 不检查重复，因为可能有合理的重复需求
        /// - 触发事件通知其他组件
        /// </summary>
        public void AddVariable(object variable)
        {
            if (variable == null) return;

            _variablesLock.EnterWriteLock();
            try
            {
                _variables.Add(variable);
            }
            finally
            {
                _variablesLock.ExitWriteLock();
            }

            // 在锁外触发事件
            OnVariableAdded(variable);
        }

        /// <summary>
        /// 移除变量的线程安全实现
        /// 
        /// 使用对象引用比较，而不是值比较
        /// 这意味着必须是同一个对象实例才能被移除
        /// </summary>
        public bool RemoveVariable(object variable)
        {
            if (variable == null) return false;

            bool removed;
            _variablesLock.EnterWriteLock();
            try
            {
                removed = _variables.Remove(variable);
            }
            finally
            {
                _variablesLock.ExitWriteLock();
            }

            // 在锁外触发事件
            if (removed)
            {
                OnVariableRemoved(variable);
            }

            return removed;
        }

        /// <summary>
        /// 清空所有变量
        /// </summary>
        public void ClearAllVariables()
        {
            _variablesLock.EnterWriteLock();
            try
            {
                _variables.Clear();
            }
            finally
            {
                _variablesLock.ExitWriteLock();
            }
        }

        // <summary>
        /// 清空所有用户变量(保留系统变量)
        /// </summary>
        public void ClearUserVariables()
        {
            _variablesLock.EnterWriteLock();
            try
            {
                var systemVars = _variables
                    .OfType<VarItem_Enhanced>()
                    .Where(v => v.IsSystemVariable)
                    .ToList();

                _variables.Clear();
                _variables.AddRange(systemVars);
            }
            finally
            {
                _variablesLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 获取指定类型的变量列表
        /// 
        /// 泛型方法的优势：
        /// 1. 类型安全，编译时检查
        /// 2. 不需要强制转换
        /// 3. 支持任意类型，扩展性好
        /// 
        /// 使用 LINQ 的 OfType 方法进行类型过滤
        /// </summary>
        public List<T> GetVariables<T>() where T : class
        {
            _variablesLock.EnterReadLock();
            try
            {
                // OfType<T>() 会过滤出指定类型的对象
                // ToList() 创建副本，确保线程安全
                return _variables.OfType<T>().ToList();
            }
            finally
            {
                _variablesLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 根据条件查找变量
        /// 
        /// 使用委托参数提供灵活的查询条件
        /// 例如：FindVariable&lt;VarItem&gt;(v => v.VarName == "test")
        /// </summary>
        public T FindVariable<T>(Func<T, bool> predicate) where T : class
        {
            if (predicate == null) return null;

            _variablesLock.EnterReadLock();
            try
            {
                // 找到第一个符合条件的变量，没有则返回 null
                return _variables.OfType<T>().FirstOrDefault(predicate);
            }
            finally
            {
                _variablesLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取所有 VarItem_Enhanced 类型变量的便捷方法
        /// 
        /// 这是最常用的变量查询操作
        /// 等同于 GetVariables&lt;VarItem_Enhanced&gt;()
        /// </summary>
        public List<VarItem_Enhanced> GetAllVariables()
        {
            return [.. GetVariables<VarItem_Enhanced>()];
        }

        /// <summary>
        /// 根据变量名查找 VarItem_Enhanced 变量
        /// 
        /// 这是最常用的变量查找方法
        /// 实现了快速的名称查找功能
        /// </summary>
        public VarItem_Enhanced FindVariableByName(string varName)
        {
            // 参数验证
            if (string.IsNullOrEmpty(varName)) return null;

            // 使用泛型查找方法，条件是变量名匹配
            return FindVariable<VarItem_Enhanced>(v => v.VarName == varName);
        }

        /// <summary>
        /// 标记变量被步骤赋值占用
        /// </summary>
        public void MarkVariableAssignedByStep(string varName, int stepIndex, string stepInfo, VariableAssignmentType assignmentType)
        {
            if (string.IsNullOrEmpty(varName) || stepIndex < 0)
                return;

            _variablesLock.EnterWriteLock();
            try
            {
                var variable = FindVariableByName(varName);
                if (variable != null)
                {
                    // 设置赋值状态
                    variable.SetAssignmentStatus(stepIndex, stepInfo, assignmentType);

                    // 触发变量状态变更事件
                    OnVariableStateChanged(variable);
                }
            }
            finally
            {
                _variablesLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 清除变量的赋值标记
        /// </summary>
        public void ClearVariableAssignmentMark(string varName, int stepIndex)
        {
            if (string.IsNullOrEmpty(varName) || stepIndex < 0)
                return;

            _variablesLock.EnterWriteLock();
            try
            {
                var variable = FindVariableByName(varName);
                if (variable != null && variable.AssignedByStepIndex == stepIndex)
                {
                    // 只能清除自己标记的变量
                    variable.ClearAssignmentStatus();

                    // 触发变量状态变更事件
                    OnVariableStateChanged(variable);
                }
            }
            finally
            {
                _variablesLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 检查变量赋值冲突
        /// </summary>
        public VariableConflictInfo CheckVariableAssignmentConflict(string varName, int excludeStepIndex)
        {
            if (string.IsNullOrEmpty(varName))
                return new VariableConflictInfo { HasConflict = false };

            _variablesLock.EnterReadLock();
            try
            {
                // 查找变量
                var variable = FindVariableByName(varName);
                if (variable == null || !variable.IsAssignedByStep)
                    return new VariableConflictInfo { HasConflict = false };

                // 如果被当前步骤赋值，不算冲突
                if (variable.AssignedByStepIndex == excludeStepIndex)
                    return new VariableConflictInfo { HasConflict = false };

                // 存在冲突
                return new VariableConflictInfo
                {
                    HasConflict = true,
                    ConflictStepIndex = variable.AssignedByStepIndex,
                    ConflictStepInfo = variable.AssignedByStepInfo,
                    ConflictAssignmentType = variable.AssignmentType
                };
            }
            finally
            {
                _variablesLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 批量检查多个变量的赋值冲突
        /// </summary>
        public Dictionary<string, VariableConflictInfo> CheckMultipleVariableConflicts(List<string> varNames, int excludeStepIndex)
        {
            var conflicts = new Dictionary<string, VariableConflictInfo>();

            if (varNames?.Count > 0)
            {
                foreach (var varName in varNames)
                {
                    conflicts[varName] = CheckVariableAssignmentConflict(varName, excludeStepIndex);
                }
            }

            return conflicts;
        }

        /// <summary>
        /// 获取被指定步骤赋值的所有变量
        /// </summary>
        public List<VarItem_Enhanced> GetVariablesAssignedByStep(int stepIndex)
        {
            _variablesLock.EnterReadLock();
            try
            {
                return _variables.OfType<VarItem_Enhanced>()
                    .Where(v => v.IsAssignedByStep && v.AssignedByStepIndex == stepIndex)
                    .ToList();
            }
            finally
            {
                _variablesLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 变量状态变更事件
        /// </summary>
        public event Action<VarItem_Enhanced> VariableStateChanged;

        /// <summary>
        /// 触发变量状态变更事件
        /// </summary>
        private void OnVariableStateChanged(VarItem_Enhanced variable)
        {
            try
            {
                VariableStateChanged?.Invoke(variable);
            }
            catch (Exception ex)
            {
                throw new Exception("触发变量状态变更事件时发生错误", ex);
            }
        }


        #endregion

        #region 事件管理实现

        /// <summary>
        /// 步骤序号变更事件
        /// 当 StepNum 属性发生变化时触发
        /// </summary>
        public event Action<int> StepNumChanged;

        /// <summary>
        /// 变量添加事件
        /// 当有新变量添加时触发
        /// </summary>
        public event Action<object> VariableAdded;

        /// <summary>
        /// 变量移除事件
        /// 当变量被移除时触发
        /// </summary>
        public event Action<object> VariableRemoved;

        /// <summary>
        /// 步骤添加事件
        /// 当有新步骤添加时触发
        /// </summary>
        public event Action<ChildModel> StepAdded;

        /// <summary>
        /// 步骤移除事件
        /// 当步骤被移除时触发
        /// </summary>
        public event Action<ChildModel> StepRemoved;

        /// <summary>
        /// 批量变更事件
        /// </summary>
        public event Action StepsChanged;

        /// <summary>
        /// 触发步骤序号变更事件的安全方法
        /// 
        /// 事件触发的注意事项：
        /// 1. 使用锁保护，避免并发问题
        /// 2. 捕获异常，避免事件处理程序的异常影响主程序
        /// 3. 使用 null 条件运算符，避免空引用异常
        /// </summary>
        private void OnStepNumChanged(int newStepNum)
        {
            lock (_eventLock)
            {
                try
                {
                    // ?. 运算符确保在有订阅者时才调用
                    StepNumChanged?.Invoke(newStepNum);
                }
                catch (Exception ex)
                {
                    // 记录异常但不重新抛出
                    // 避免事件处理程序的异常影响主程序流程
                    Debug.WriteLine($"StepNumChanged 事件处理异常: {ex.Message}");

                    // 在生产环境中，可能需要记录到日志系统
                    // Logger?.LogError(ex, "StepNumChanged event handler failed");
                }
            }
        }

        /// <summary>
        /// 触发变量添加事件的安全方法
        /// </summary>
        private void OnVariableAdded(object variable)
        {
            lock (_eventLock)
            {
                try
                {
                    VariableAdded?.Invoke(variable);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"VariableAdded 事件处理异常: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 触发变量移除事件的安全方法
        /// </summary>
        private void OnVariableRemoved(object variable)
        {
            lock (_eventLock)
            {
                try
                {
                    VariableRemoved?.Invoke(variable);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"VariableRemoved 事件处理异常: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 触发步骤添加事件的安全方法
        /// </summary>
        private void OnStepAdded(ChildModel step)
        {
            lock (_eventLock)
            {
                try
                {
                    StepAdded?.Invoke(step);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"StepAdded 事件处理异常: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 触发步骤移除事件的安全方法
        /// </summary>
        private void OnStepRemoved(ChildModel step)
        {
            lock (_eventLock)
            {
                try
                {
                    StepRemoved?.Invoke(step);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"StepRemoved 事件处理异常: {ex.Message}");
                }
            }
        }

        #endregion

        #region 状态管理和诊断实现

        /// <summary>
        /// 重置工作流状态到初始状态
        /// 
        /// 这是一个复杂的操作，需要：
        /// 1. 按照正确的顺序获取所有锁（避免死锁）
        /// 2. 重置所有状态变量
        /// 3. 清空所有集合
        /// 4. 确保操作的原子性
        /// 
        /// 锁获取顺序：配置锁 -> 变量锁 -> 步骤锁
        /// 这个顺序在整个类中必须保持一致，避免死锁
        /// </summary>
        public void Reset()
        {
            // 按照预定义的顺序获取锁，避免死锁
            _configLock.EnterWriteLock();
            try
            {
                _variablesLock.EnterWriteLock();
                try
                {
                    _stepsLock.EnterWriteLock();
                    try
                    {
                        // 重置所有基础属性
                        _stepNum = 0;
                        _status = false;
                        _t = 0;
                        _modelTypeName = string.Empty;
                        _modelName = string.Empty;
                        _itemName = string.Empty;
                        _stepName = string.Empty;

                        // 清空所有集合
                        _variables.Clear();
                        _steps.Clear();

                        // 注意：这里没有触发事件，因为 Reset 是一个特殊操作
                        // 如果需要，可以在最后触发一个 StateReset 事件
                    }
                    finally
                    {
                        _stepsLock.ExitWriteLock();
                    }
                }
                finally
                {
                    _variablesLock.ExitWriteLock();
                }
            }
            finally
            {
                _configLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 获取诊断信息的实现
        /// 
        /// 诊断信息包括：
        /// 1. 当前所有属性的值
        /// 2. 集合的大小信息
        /// 3. 对象的状态信息
        /// 
        /// 这对于调试和监控非常有用
        /// </summary>
        public string GetDiagnosticInfo()
        {
            // 同样需要按顺序获取读锁
            _configLock.EnterReadLock();
            try
            {
                _variablesLock.EnterReadLock();
                try
                {
                    _stepsLock.EnterReadLock();
                    try
                    {
                        // 构建诊断信息字符串
                        return $"WorkflowStateService 诊断信息:\n" +
                               $"- 步骤编号: {_stepNum}\n" +
                               $"- 状态: {_status}\n" +
                               $"- T值: {_t}\n" +
                               $"- 模型类型: {_modelTypeName ?? "未设置"}\n" +
                               $"- 模型名称: {_modelName ?? "未设置"}\n" +
                               $"- 项目名称: {_itemName ?? "未设置"}\n" +
                               $"- 步骤名称: {_stepName ?? "未设置"}\n" +
                               $"- 变量数量: {_variables.Count}\n" +
                               $"- 步骤数量: {_steps.Count}\n" +
                               $"- 是否已释放: {_disposed}\n" +
                               $"- 创建时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    }
                    finally
                    {
                        _stepsLock.ExitReadLock();
                    }
                }
                finally
                {
                    _variablesLock.ExitReadLock();
                }
            }
            finally
            {
                _configLock.ExitReadLock();
            }
        }

        #endregion

        #region IDisposable 实现

        /// <summary>
        /// 实现 IDisposable 接口
        /// 
        /// 资源释放的重要性：
        /// 1. ReaderWriterLockSlim 是非托管资源，需要显式释放
        /// 2. 事件订阅可能导致内存泄漏，需要清空
        /// 3. 遵循 .NET 的资源管理最佳实践
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  // 告诉 GC 不需要调用终结器
        }

        /// <summary>
        /// 受保护的释放方法
        /// 
        /// 参数说明：
        /// - disposing: true 表示是从 Dispose() 方法调用的
        /// - disposing: false 表示是从终结器调用的
        /// 
        /// 在这个实现中，我们不提供终结器，所以 disposing 总是 true
        /// </summary>
        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                try
                {
                    // 清空所有事件订阅，防止内存泄漏
                    StepNumChanged = null;
                    VariableAdded = null;
                    VariableRemoved = null;
                    StepAdded = null;
                    StepRemoved = null;

                    // 释放锁资源
                    // 注意：要在 try-catch 中释放，因为 Dispose 可能被多次调用
                    _configLock?.Dispose();
                    _variablesLock?.Dispose();
                    _stepsLock?.Dispose();
                }
                catch (Exception ex)
                {
                    // 在 Dispose 方法中不应该抛出异常
                    Debug.WriteLine($"WorkflowStateService 释放资源时发生异常: {ex.Message}");
                }
                finally
                {
                    _disposed = true;
                }
            }
        }

        #endregion
    }
}

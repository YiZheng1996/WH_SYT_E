namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// PLC写入参数类（优化版）
    /// 用于存储PLC写入操作的配置参数
    /// </summary>
    [Serializable]
    public class Parameter_WritePLC
    {
        #region 属性

        /// <summary>
        /// 步骤描述信息
        /// </summary>
        public string Description { get; set; } = "PLC写入步骤";

        /// <summary>
        /// 是否启用此步骤
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// PLC写入项目列表
        /// </summary>
        public List<PLCWriteItem> Items { get; set; } = [];

        /// <summary>
        /// 执行条件（可选）
        /// 为空时总是执行
        /// </summary>
        public string Condition { get; set; } = "";

        /// <summary>
        /// 错误处理策略
        /// </summary>
        public ErrorHandlingStrategy ErrorHandling { get; set; } = ErrorHandlingStrategy.StopOnError;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; set; } = DateTime.Now;

        #endregion

        #region 嵌套类

        /// <summary>
        /// PLC写入项目
        /// </summary>
        [Serializable]
        public class PLCWriteItem
        {
            /// <summary>
            /// PLC模块名称
            /// </summary>
            public string PlcModuleName { get; set; } = "";

            /// <summary>
            /// PLC点位地址/键名
            /// </summary>
            public string PlcKeyName { get; set; } = "";

            /// <summary>
            /// 写入值（支持常量或变量引用 {变量名}）
            /// </summary>
            public string PlcValue { get; set; } = "";

            /// <summary>
            /// 描述信息
            /// </summary>
            public string Description { get; set; } = "";

            /// <summary>
            /// 是否启用此项
            /// </summary>
            public bool IsEnabled { get; set; } = true;

            /// <summary>
            /// 延迟时间（毫秒，写入后等待）
            /// </summary>
            public int DelayAfterWrite { get; set; } = 0;

            /// <summary>
            /// 重试次数（写入失败时）
            /// </summary>
            public int RetryCount { get; set; } = 0;

            /// <summary>
            /// 数据类型提示（可选）
            /// </summary>
            public string DataType { get; set; } = "Auto";

            /// <summary>
            /// 是否验证写入（读回验证）
            /// </summary>
            public bool VerifyAfterWrite { get; set; } = false;

            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreatedTime { get; set; } = DateTime.Now;

            /// <summary>
            /// 克隆对象
            /// </summary>
            public PLCWriteItem Clone()
            {
                return new PLCWriteItem
                {
                    PlcModuleName = this.PlcModuleName,
                    PlcKeyName = this.PlcKeyName,
                    PlcValue = this.PlcValue,
                    Description = this.Description,
                    IsEnabled = this.IsEnabled,
                    DelayAfterWrite = this.DelayAfterWrite,
                    RetryCount = this.RetryCount,
                    DataType = this.DataType,
                    VerifyAfterWrite = this.VerifyAfterWrite,
                    CreatedTime = this.CreatedTime
                };
            }

            /// <summary>
            /// 转换为字符串（用于日志）
            /// </summary>
            public override string ToString()
            {
                return $"[{PlcModuleName}] {PlcKeyName} = {PlcValue}";
            }
        }

        /// <summary>
        /// 错误处理策略枚举
        /// </summary>
        public enum ErrorHandlingStrategy
        {
            /// <summary>
            /// 发生错误时停止执行
            /// </summary>
            StopOnError = 0,

            /// <summary>
            /// 发生错误时继续执行下一项
            /// </summary>
            ContinueOnError = 1,

            /// <summary>
            /// 发生错误时重试
            /// </summary>
            RetryOnError = 2,

            /// <summary>
            /// 跳过当前项并记录错误
            /// </summary>
            SkipAndLog = 3
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Parameter_WritePLC()
        {
            Items = [];
            CreatedTime = DateTime.Now;
            LastModifiedTime = DateTime.Now;
        }

        /// <summary>
        /// 带描述的构造函数
        /// </summary>
        public Parameter_WritePLC(string description) : this()
        {
            Description = description;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 添加PLC写入项
        /// </summary>
        public void AddItem(PLCWriteItem item)
        {
            if (item != null)
            {
                Items.Add(item);
                LastModifiedTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 添加PLC写入项（简化方法）
        /// </summary>
        public void AddItem(string moduleName, string keyName, string value, string description = "")
        {
            AddItem(new PLCWriteItem
            {
                PlcModuleName = moduleName,
                PlcKeyName = keyName,
                PlcValue = value,
                Description = description
            });
        }

        /// <summary>
        /// 移除指定索引的项
        /// </summary>
        public bool RemoveItem(int index)
        {
            if (index >= 0 && index < Items.Count)
            {
                Items.RemoveAt(index);
                LastModifiedTime = DateTime.Now;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 清空所有项
        /// </summary>
        public void ClearItems()
        {
            Items.Clear();
            LastModifiedTime = DateTime.Now;
        }

        /// <summary>
        /// 获取已启用的项
        /// </summary>
        public List<PLCWriteItem> GetEnabledItems()
        {
            return Items.Where(item => item.IsEnabled).ToList();
        }

        /// <summary>
        /// 验证参数有效性
        /// </summary>
        public bool Validate(out string errorMessage)
        {
            errorMessage = "";

            if (Items == null || Items.Count == 0)
            {
                errorMessage = "至少需要添加一个PLC写入项";
                return false;
            }

            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];

                if (string.IsNullOrWhiteSpace(item.PlcModuleName))
                {
                    errorMessage = $"第{i + 1}项：PLC模块名不能为空";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(item.PlcKeyName))
                {
                    errorMessage = $"第{i + 1}项：PLC地址不能为空";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 克隆参数对象
        /// </summary>
        public Parameter_WritePLC Clone()
        {
            var cloned = new Parameter_WritePLC
            {
                Description = this.Description,
                IsEnabled = this.IsEnabled,
                Condition = this.Condition,
                ErrorHandling = this.ErrorHandling,
                CreatedTime = this.CreatedTime,
                LastModifiedTime = DateTime.Now
            };

            if (this.Items != null)
            {
                cloned.Items = new List<PLCWriteItem>();
                foreach (var item in this.Items)
                {
                    cloned.Items.Add(item.Clone());
                }
            }

            return cloned;
        }

        /// <summary>
        /// 获取摘要信息
        /// </summary>
        public string GetSummary()
        {
            int enabledCount = GetEnabledItems().Count;
            int totalCount = Items?.Count ?? 0;
            return $"{Description} (共{totalCount}项, 已启用{enabledCount}项)";
        }

        /// <summary>
        /// 转换为字符串（用于日志）
        /// </summary>
        public override string ToString()
        {
            return GetSummary();
        }

        #endregion

        #region 静态工厂方法

        /// <summary>
        /// 创建默认参数
        /// </summary>
        public static Parameter_WritePLC CreateDefault(string description = "PLC写入步骤")
        {
            return new Parameter_WritePLC(description);
        }

        /// <summary>
        /// 创建单个写入项的参数
        /// </summary>
        public static Parameter_WritePLC CreateSingle(
            string moduleName,
            string keyName,
            string value,
            string description = "")
        {
            var param = new Parameter_WritePLC(description);
            param.AddItem(moduleName, keyName, value, description);
            return param;
        }

        /// <summary>
        /// 从项目列表创建参数
        /// </summary>
        public static Parameter_WritePLC CreateFromItems(
            List<PLCWriteItem> items,
            string description = "PLC写入步骤")
        {
            var param = new Parameter_WritePLC(description);
            if (items != null && items.Any())
            {
                param.Items = new List<PLCWriteItem>(items);
            }
            return param;
        }

        #endregion
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MainUI.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 时间单位枚举
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TimeUnit
    {
        /// <summary>
        /// 毫秒
        /// </summary>
        Milliseconds,

        /// <summary>
        /// 秒
        /// </summary>
        Seconds,

        /// <summary>
        /// 分钟
        /// </summary>
        Minutes
    }

    /// <summary>
    /// 延时等待参数类
    /// 支持时间单位选择和变量/表达式输入
    /// </summary>
    public class Parameter_DelayTime
    {
        #region 属性

        /// <summary>
        /// 延时时间值（支持变量表达式，如 {DelayTime} 或 1000）
        /// </summary>
        public string DelayValue { get; set; } = "1000";

        /// <summary>
        /// 时间单位
        /// </summary>
        public TimeUnit Unit { get; set; } = TimeUnit.Milliseconds;

        /// <summary>
        /// 原有属性（毫秒）- 保持向后兼容
        /// 读取时：如果 DelayValue 为空则返回此值，否则尝试解析 DelayValue
        /// 写入时：同时设置 DelayValue
        /// </summary>
        [JsonIgnore]
        public double T
        {
            get
            {
                // 优先使用 DelayValue
                if (!string.IsNullOrEmpty(DelayValue))
                {
                    // 尝试直接解析为数值
                    if (double.TryParse(DelayValue, out double value))
                    {
                        return value;
                    }
                }
                return _legacyT;
            }
            set
            {
                _legacyT = value;
                // 同步到 DelayValue（仅在 DelayValue 为空或为数值时）
                if (string.IsNullOrEmpty(DelayValue) || double.TryParse(DelayValue, out _))
                {
                    DelayValue = value.ToString();
                }
            }
        }
        private double _legacyT = 1000;

        /// <summary>
        /// 描述信息（可选）
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取时间单位的显示名称
        /// </summary>
        public static string GetUnitDisplayName(TimeUnit unit)
        {
            return unit switch
            {
                TimeUnit.Milliseconds => "毫秒",
                TimeUnit.Seconds => "秒",
                TimeUnit.Minutes => "分钟",
                _ => "毫秒"
            };
        }

        /// <summary>
        /// 从显示名称获取时间单位
        /// </summary>
        public static TimeUnit GetUnitFromDisplayName(string displayName)
        {
            return displayName switch
            {
                "毫秒" => TimeUnit.Milliseconds,
                "秒" => TimeUnit.Seconds,
                "分钟" => TimeUnit.Minutes,
                _ => TimeUnit.Milliseconds
            };
        }

        /// <summary>
        /// 将指定值按当前单位转换为毫秒
        /// </summary>
        /// <param name="value">时间值</param>
        /// <returns>毫秒数</returns>
        public double ConvertToMilliseconds(double value)
        {
            return Unit switch
            {
                TimeUnit.Milliseconds => value,
                TimeUnit.Seconds => value * 1000,
                TimeUnit.Minutes => value * 60000,
                _ => value
            };
        }

        /// <summary>
        /// 获取当前配置的毫秒数（仅当 DelayValue 为纯数值时有效）
        /// </summary>
        /// <returns>毫秒数，如果 DelayValue 是表达式则返回 -1</returns>
        public double GetMillisecondsOrDefault()
        {
            if (double.TryParse(DelayValue, out double value))
            {
                return ConvertToMilliseconds(value);
            }
            return -1; // 表示是表达式，需要运行时解析
        }

        /// <summary>
        /// 检查 DelayValue 是否包含变量引用
        /// </summary>
        public bool ContainsVariables()
        {
            if (string.IsNullOrEmpty(DelayValue)) return false;
            return DelayValue.Contains("{") && DelayValue.Contains("}");
        }

        /// <summary>
        /// 获取显示用的预览文本
        /// </summary>
        public string GetPreviewText()
        {
            string unitName = GetUnitDisplayName(Unit);

            if (ContainsVariables())
            {
                return $"表达式: {DelayValue} {unitName}";
            }

            if (double.TryParse(DelayValue, out double value))
            {
                // 计算实际毫秒数用于显示
                double ms = ConvertToMilliseconds(value);
                if (ms >= 60000)
                {
                    return $"{ms / 60000:F1} 分钟";
                }
                else if (ms >= 1000)
                {
                    return $"{ms / 1000:F1} 秒";
                }
                else
                {
                    return $"{ms:F0} 毫秒";
                }
            }

            return $"{DelayValue} {unitName}";
        }

        #endregion

        #region 静态工厂方法

        /// <summary>
        /// 创建默认参数
        /// </summary>
        public static Parameter_DelayTime CreateDefault()
        {
            return new Parameter_DelayTime
            {
                DelayValue = "1000",
                Unit = TimeUnit.Milliseconds,
                Description = string.Empty
            };
        }

        /// <summary>
        /// 从毫秒数创建参数
        /// </summary>
        public static Parameter_DelayTime FromMilliseconds(double milliseconds)
        {
            return new Parameter_DelayTime
            {
                DelayValue = milliseconds.ToString(),
                Unit = TimeUnit.Milliseconds
            };
        }

        /// <summary>
        /// 从秒数创建参数
        /// </summary>
        public static Parameter_DelayTime FromSeconds(double seconds)
        {
            return new Parameter_DelayTime
            {
                DelayValue = seconds.ToString(),
                Unit = TimeUnit.Seconds
            };
        }

        #endregion
    }
}
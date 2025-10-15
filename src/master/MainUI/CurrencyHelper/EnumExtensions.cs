using System.ComponentModel;
using System.Reflection;

namespace MainUI.CurrencyHelper
{
    /// <summary>
    /// 枚举扩展方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举的Description特性值
        /// </summary>
        public static string GetDescription(this System.Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field == null)
                return value.ToString();

            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? value.ToString();
        }

        /// <summary>
        /// 获取枚举的所有描述和值的列表
        /// </summary>
        public static List<EnumItem> GetEnumItems<T>() where T : System.Enum
        {
            return System.Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new EnumItem
                {
                    Value = Convert.ToInt32(e),
                    Name = e.ToString(),
                    Description = e.GetDescription()
                })
                .ToList();
        }
    }

    /// <summary>
    /// 枚举项
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// 枚举值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 枚举名称(英文)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 枚举描述(中文)
        /// </summary>
        public string Description { get; set; }
    }
}

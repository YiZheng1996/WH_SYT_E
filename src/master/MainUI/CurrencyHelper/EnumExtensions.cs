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
            return [.. Enum.GetValues(typeof(T))
              .Cast<T>()
              .Select(e => new EnumItem
              {
                  Value = e,
                  DisplayName = e.GetDescription()
              })];
        }
    }

    /// <summary>
    /// 枚举项
    /// </summary>
    public class EnumItem
    {
        public object Value { get; set; }
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}

using System.ComponentModel;
using System.Reflection;

namespace Marisa.Domain.EnumHelper
{
    public static class EnumHelper
    {
        public static T GetEnumValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null && attribute.Description == description)
                    return (T)field.GetValue(null);
            }

            throw new ArgumentException($"'{description}' is not a valid description for enum '{typeof(T).Name}'.");
        }
    }
}

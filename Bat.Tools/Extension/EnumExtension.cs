using System.Reflection;

namespace Bat.Tools;

public static class EnumExtension
{
    public static string GetDisplayName(this Enum enumObj)
    {
        var fields = enumObj.GetType().GetField(enumObj.ToString());
        var attributes = (DisplayAttribute[])fields.GetCustomAttributes(typeof(DisplayAttribute), false);

        if (attributes.Length > 0) return attributes[0].Name;
        else return enumObj.ToString();
    }

    public static string GetDescription(this Enum enumObj)
    {
        var type = enumObj.GetType();
        var memberInfo = type.GetMember(enumObj.ToString());

        if (memberInfo != null && memberInfo.Length > 0)
        {
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0) return ((DescriptionAttribute)attributes[0]).Description;
        }

        return enumObj.ToString();
    }

    public static string GetLocalizeDescription(this Enum enumObj)
    {
        var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0) return attributes[0].Description;
        else return enumObj.ToString();
    }

    public static IEnumerable<Core.PropertyInfo> GetEnumElements<T>() where T : Enum
    {
        var result = new List<Core.PropertyInfo>();
        if (typeof(T).BaseType == typeof(Enum))
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                Enum val = Enum.Parse(typeof(T), item.ToString()) as Enum;
                result.Add(new Core.PropertyInfo
                {
                    Name = item.ToString(),
                    Type = item.GetType().Name,
                    Value = Convert.ToInt32(val),
                    DisplayName = item.GetDisplayName(),
                    DataType = item.GetType().BaseType.Name,
                    Description = item.GetLocalizeDescription()
                });
            }
        }
        return result;
    }

    public static IEnumerable<TEnum> FilterEnumWithAttributeOf<TEnum, TAttribute>() where TEnum : struct where TAttribute : class
    {
        foreach (var field in typeof(TEnum).GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static))
        {
            if (field.GetCustomAttributes(typeof(TAttribute), false).Length > 0)
                yield return (TEnum)field.GetValue(null);
        }
    }

}
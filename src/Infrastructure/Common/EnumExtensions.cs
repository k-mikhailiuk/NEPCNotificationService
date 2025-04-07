using Common.Enums;

namespace Common;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value, Language language)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null)
            return value.ToString();

        // Пытаемся получить наш атрибут MultiLanguageDescriptionAttribute
        var attribute = 
            (MultiLanguageDescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(MultiLanguageDescriptionAttribute));

        return attribute != null ? attribute.GetDescription(language) : value.ToString();
    }
}
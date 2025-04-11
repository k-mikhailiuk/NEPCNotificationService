using Common.Enums;

namespace Common;

/// <summary>
/// Расширения для перечислений (enum), позволяющие получать описания на разных языках.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Возвращает локализованное описание значения перечисления, если задан атрибут <see cref="MultiLanguageDescriptionAttribute"/>.
    /// В противном случае возвращает строковое представление значения.
    /// </summary>
    /// <param name="value">Значение перечисления.</param>
    /// <param name="language">Язык, на котором необходимо получить описание.</param>
    /// <returns>Локализованное описание или строковое представление значения перечисления.</returns>
    public static string GetDescription(this Enum value, Language language)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null)
            return value.ToString();

        var attribute = 
            (MultiLanguageDescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(MultiLanguageDescriptionAttribute));

        return attribute != null ? attribute.GetDescription(language) : value.ToString();
    }
}
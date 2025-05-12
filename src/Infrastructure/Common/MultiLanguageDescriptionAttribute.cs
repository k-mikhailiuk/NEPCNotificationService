using Common.Enums;

namespace Common;

/// <summary>
/// Атрибут для задания описания значения перечисления на нескольких языках.
/// Используется совместно с методом расширения <see cref="EnumExtensions.GetDescription"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class MultiLanguageDescriptionAttribute(string russian, string kyrgyz, string english) : Attribute
{
    /// <summary>
    /// Описание на русском языке.
    /// </summary>
    private string Russian { get; } = russian;
    
    /// <summary>
    /// Описание на английском языке.
    /// </summary>
    private string English { get; } = english;
    
    /// <summary>
    /// Описание на кыргызском языке.
    /// </summary>
    private string Kyrgyz { get; } = kyrgyz;

    /// <summary>
    /// Возвращает описание на выбранном языке.
    /// </summary>
    /// <param name="language">Язык, на котором необходимо получить описание.</param>
    /// <returns>Строка с описанием на указанном языке. Если язык не поддерживается — возвращается пустая строка.</returns>
    public string GetDescription(Language language)
    {
        return language switch
        {
            Language.Russian => Russian,
            Language.English => English,
            Language.Kyrgyz => Kyrgyz,
            _ => string.Empty
        };
    }
}
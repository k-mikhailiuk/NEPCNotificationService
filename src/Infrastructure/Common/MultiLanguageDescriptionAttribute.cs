using Common.Enums;

namespace Common;

[AttributeUsage(AttributeTargets.Field)]
public class MultiLanguageDescriptionAttribute(string russian, string kyrgyz, string english) : Attribute
{
    private string Russian { get; } = russian;
    private string English { get; } = english;
    private string Kyrgyz { get; } = kyrgyz;

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
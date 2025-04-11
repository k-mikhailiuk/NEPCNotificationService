using System.Text.RegularExpressions;

namespace Common;

/// <summary>
/// Утилита для замены плейсхолдеров (ключевых слов в фигурных скобках) в строке на заданные значения.
/// </summary>
public static partial class KeyWordReplacer
{
    /// <summary>
    /// Скомпилированное регулярное выражение для поиска плейсхолдеров в формате {PLACEHOLDER}.
    /// </summary>
    [GeneratedRegex(@"\{[^}]+\}")]
    private static partial Regex MyRegex();
    
    /// <summary>
    /// Заменяет все плейсхолдеры в строке на соответствующие значения из словаря.
    /// Если замена для найденного плейсхолдера отсутствует — он удаляется из строки.
    /// </summary>
    /// <param name="message">Исходная строка с плейсхолдерами.</param>
    /// <param name="replacements">Словарь замен, где ключ — это плейсхолдер (например, {NAME}), а значение — строка для подстановки.</param>
    /// <returns>Строка с заменёнными плейсхолдерами.</returns>
    public static string ReplacePlaceholders(string? message, IDictionary<string, string> replacements)
    {
        const string pattern = @"\{[^}]+\}";
        
        if(message is null)
            return string.Empty;

        return MyRegex().Replace(message, match => replacements.TryGetValue(match.Value, out var replacement)
            ? replacement 
            : string.Empty);
    }

}
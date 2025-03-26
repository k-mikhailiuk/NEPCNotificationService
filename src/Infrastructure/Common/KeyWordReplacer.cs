using System.Text.RegularExpressions;

namespace Common;

public static partial class KeyWordReplacer
{
    
    [GeneratedRegex(@"\{[^}]+\}")]
    private static partial Regex MyRegex();
    
    public static string ReplacePlaceholders(string message, IDictionary<string, string> replacements)
    {
        const string pattern = @"\{[^}]+\}";

        return MyRegex().Replace(message, match => replacements.TryGetValue(match.Value, out var replacement)
            ? replacement 
            : string.Empty);
    }

}
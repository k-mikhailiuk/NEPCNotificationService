using System.Text.RegularExpressions;

namespace Common;

public static partial class KeyWordReplacer
{
    
    [GeneratedRegex(@"\{[^}]+\}")]
    private static partial Regex MyRegex();
    
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
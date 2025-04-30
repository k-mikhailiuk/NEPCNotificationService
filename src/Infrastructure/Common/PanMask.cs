namespace Common;

/// <summary>
/// Утилитный класс для маскирования PAN (Primary Account Number).
/// </summary>
public static class PanMask
{
    /// <summary>
    /// Маскирует PAN, оставляя только последние 4 цифры.
    /// Все предыдущие символы заменяются символом '*'.
    /// </summary>
    /// <param name="pan">Полный номер карты (PAN).</param>
    /// <returns>
    /// Маскированный номер карты, где все символы кроме последних четырёх заменены на '*'.
    /// Если <paramref name="pan"/> пустой или короче 5 символов — возвращается исходное значение.
    /// </returns>
    public static string MaskPan(string pan)
    {
        if (string.IsNullOrEmpty(pan))
            return string.Empty;

        if (pan.Length <= 4)
            return pan;

        var last4 = pan[^4..];

        return '*' + last4;
    }

}
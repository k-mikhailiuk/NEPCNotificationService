namespace Common;

/// <summary>
/// Утилитный класс для преобразования чисел типа <see cref="decimal"/> в строку с учётом наличия дробной части.
/// </summary>
public static class NumberConverter
{
    /// <summary>
    /// Если дробная часть числа <see cref="decimal"/> равна нулю, возвращает число, приведённое к <see cref="int"/>.
    /// Иначе возвращает исходное значение типа <see cref="decimal"/>.
    /// </summary>
    /// <param name="number">Исходное число типа <see cref="decimal"/>.</param>
    /// <returns>Тип <see cref="int"/>, если число целое; иначе — <see cref="decimal"/>.</returns>
    private static object ConvertToIntIfWhole(decimal number)
    {
        var integerPart = decimal.Truncate(number);
        
        if (number == integerPart)
        {
            return (int)number;
        }
   
        return number;
    }

    /// <summary>
    /// Возвращает строковое представление числа <see cref="decimal"/>, преобразованное с учётом дробной части.
    /// Если число целое, возвращается строка, представляющая <see cref="int"/>.
    /// Если число содержит дробную часть — строка от <see cref="decimal"/>.
    /// Если значение <c>null</c>, возвращается пустая строка.
    /// </summary>
    /// <param name="number">Число типа <see cref="decimal"/>, которое нужно преобразовать.</param>
    /// <returns>Строковое представление числа или пустая строка, если значение <c>null</c>.</returns>
    public static string? GetConvertedString(decimal? number)
    {
        if(number == null)
            return string.Empty;
        
        var convertedNumber = ConvertToIntIfWhole(number.Value);
        
        return convertedNumber.ToString();
    }
}
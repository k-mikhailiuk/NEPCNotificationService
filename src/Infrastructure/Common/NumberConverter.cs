namespace Common;

public static class NumberConverter
{
    /// <summary>
    /// Если дробная часть числа decimal равна нулю, возвращает число, приведённое к int.
    /// Иначе возвращает исходное число типа decimal.
    /// </summary>
    /// <param name="number">Исходное число типа decimal</param>
    /// <returns>Либо число типа int, если дробная часть равна нулю, либо исходное число decimal</returns>
    private static object ConvertToIntIfWhole(decimal number)
    {
        var integerPart = decimal.Truncate(number);
        
        if (number == integerPart)
        {
            return (int)number;
        }
   
        return number;
    }

    public static string? GetConvertedString(decimal? number)
    {
        if(number == null)
            return string.Empty;
        
        var convertedNumber = ConvertToIntIfWhole(number.Value);
        
        return convertedNumber.ToString();
    }
}
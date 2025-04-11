using System.Globalization;

namespace Common;

/// <summary>
/// Утилитный класс для преобразования строковых представлений даты и времени в <see cref="DateTimeOffset"/> с учетом часового пояса.
/// </summary>
public static class TimeZoneConverter
{
    /// <summary>
    /// Преобразует строку в формате "yyyyMMddHHmmss" в UTC-дату, учитывая заданный часовой пояс.
    /// </summary>
    /// <param name="stringDateTime">Дата и время в строковом формате "yyyyMMddHHmmss".</param>
    /// <param name="timeZoneId">Идентификатор часового пояса Windows (по умолчанию: "N. Central Asia Standard Time").</param>
    /// <returns>Дата и время в формате <see cref="DateTimeOffset"/>, приведённые к UTC.</returns>
    /// <exception cref="FormatException">Выбрасывается, если входная строка имеет неверный формат.</exception>
    public static DateTimeOffset ConvertFromStringToUtc(string stringDateTime,
        string timeZoneId = "N. Central Asia Standard Time")
    {
        const string format = "yyyyMMddHHmmss";
        if (!DateTime.TryParseExact(stringDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var localDateTime))
        {
            throw new FormatException($"Input string '{stringDateTime}' is not in the correct format '{format}'.");
        }

        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

        DateTimeOffset localDateTimeWithOffset = TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZone);

        return localDateTimeWithOffset;
    }

    /// <summary>
    /// Преобразует строку в формате "yyyyMMddHHmmss", интерпретируя её как локальное время, и возвращает её в формате UTC.
    /// </summary>
    /// <param name="localDateTimeString">Дата и время в строковом формате "yyyyMMddHHmmss".</param>
    /// <returns>Дата и время, приведённые к UTC, в формате <see cref="DateTimeOffset"/>.</returns>
    /// <exception cref="FormatException">Выбрасывается, если входная строка имеет неверный формат.</exception>
    public static DateTimeOffset ConvertLocalToUtc(string localDateTimeString)
    {
        const string format = "yyyyMMddHHmmss";

        if (!DateTime.TryParseExact(localDateTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var localDateTime))
        {
            throw new FormatException($"Input string '{localDateTimeString}' is not in the correct format '{format}'.");
        }

        var unspecifiedDateTime = DateTime.SpecifyKind(localDateTime, DateTimeKind.Local);

        DateTimeOffset utcDateTime = unspecifiedDateTime.ToUniversalTime();

        return utcDateTime;
    }
}
using Common;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Services;

/// <summary>
/// Конвертер дат
/// </summary>
/// <param name="logger">Логгер для записи событий и ошибок</param>
public class DateTimeConverter(ILogger<DateTimeConverter> logger)
{
    /// <summary>
    /// Безопасно преобразует строку времени в значение <see cref="DateTimeOffset"/> в UTC.
    /// </summary>
    /// <param name="time">Строковое представление времени.</param>
    /// <returns>
    /// Преобразованное время в UTC; в случае неудачи возвращает <see cref="DateTimeOffset.MinValue"/>.
    /// </returns>
    public DateTimeOffset SafeConvertTime(string time)
    {
        try
        {
            return TimeZoneConverter.ConvertFromStringToUtc(time);
        }
        catch (Exception ex)
        {
            logger.LogError("SafeConvertTime: Failed to convert time={time}. Error: {ex.Message}", time, ex.Message);
            return DateTimeOffset.MinValue;
        }
    }

    /// <summary>
    /// Безопасно преобразует локальное строковое время в значение <see cref="DateTimeOffset"/> в UTC.
    /// </summary>
    /// <param name="time">Строковое представление локального времени.</param>
    /// <returns>
    /// Преобразованное время в UTC; в случае неудачи возвращает <see cref="DateTimeOffset.MinValue"/>.
    /// </returns>
    public DateTimeOffset SafeConvertFromLocalToUtc(string time)
    {
        try
        {
            return TimeZoneConverter.ConvertLocalToUtc(time);
        }
        catch (Exception ex)
        {
            logger.LogError("SafeConvertTime: Failed to convert time={time}. Error: {ex.Message}", time, ex.Message);
            return DateTimeOffset.MinValue;
        }
    }
}
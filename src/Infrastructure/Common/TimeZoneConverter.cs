using System.Globalization;

namespace Common;

public static class TimeZoneConverter
{
    public static DateTimeOffset ConvertFromStringToUtc(string stringDateTime,
        string timeZoneId = "N. Central Asia Standard Time")
    {
        const string format = "yyyyMMddHHmmss";
        if (!DateTime.TryParseExact(stringDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime localDateTime))
        {
            throw new FormatException($"Input string '{stringDateTime}' is not in the correct format '{format}'.");
        }

        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

        DateTimeOffset localDateTimeWithOffset = TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZone);

        return localDateTimeWithOffset;
    }

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
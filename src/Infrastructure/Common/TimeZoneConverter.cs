using System.Globalization;

namespace Common;

public static class TimeZoneConverter
{
    public static DateTimeOffset ConvertFromStringToUtc(string stringDateTime, string timeZoneId = "N. Central Asia Standard Time")
    {
        const string format = "yyyyMMddHHmmss";
        if (DateTime.TryParseExact(
                stringDateTime, 
                format, 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out var localDateTime))
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZone);
            return new DateTimeOffset(utcTime, TimeSpan.Zero);
        }

        throw new FormatException($"Input string '{stringDateTime}' is not in the correct format '{format}'.");
    }
    
    public static DateTimeOffset ConvertFromUtcToLocal(string stringDateTime, string timeZoneId = "N. Central Asia Standard Time")
    {
        return DateTimeOffset.Now;
    }
    
    
    
}
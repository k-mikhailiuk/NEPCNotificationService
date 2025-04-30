namespace Aggregator.DTOs.AcsOtp;

/// <summary>
/// Информация о разовом пароле
/// </summary>
public record AggregatorOtpInfo
{
    /// <summary>
    /// Разовый пароль
    /// </summary>
    public string Otp { get; init; }
    
    /// <summary>
    /// Срок действия разового пароля (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string ExpirationTime { get; init; }
}
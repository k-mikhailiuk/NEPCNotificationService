namespace Aggregator.DTOs.AcsOtp;

/// <summary>
/// Информация о разовом пароле
/// </summary>
public class AggregatorOtpInfo
{
    /// <summary>
    /// Разовый пароль
    /// </summary>
    public string Otp { get; set; }
    
    /// <summary>
    /// Срок действия разового пароля (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string ExpirationTime { get; set; }
}
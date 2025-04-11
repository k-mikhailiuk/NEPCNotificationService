using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Информация о разовом пароле
/// </summary>
[Owned]
public class OtpInfo
{
    /// <summary>
    /// Разовый пароль
    /// </summary>
    public string Otp { get; set; }
    
    /// <summary>
    /// Срок действия разового пароля (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public DateTimeOffset ExpirationTime { get; set; }
}
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

[Owned]
public class OtpInfo
{
    public string Otp { get; set; }
    
    public DateTimeOffset ExpirationTime { get; set; }
}
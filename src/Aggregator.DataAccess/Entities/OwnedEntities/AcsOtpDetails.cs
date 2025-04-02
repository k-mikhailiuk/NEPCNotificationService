using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

[Owned]
public class AcsOtpDetails
{
    public OtpInfo OtpInfo { get; set; }
    
    public AuthMoney? AuthMoney { get; set; }
    
    public DateTimeOffset TransactionTime { get; set; }
}
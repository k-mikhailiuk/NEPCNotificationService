using System.Linq.Expressions;
using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.AcsOtp;
using Common;

namespace Aggregator.Repositories.Repositories.AcsOtp;

public class AcsOtpRepository : Repository<DataAccess.Entities.AcsOtp.AcsOtp>, IAcsOtpRepository
{
    public AcsOtpRepository(AggregatorDbContext context) : base(context)
    {
    }

    public async Task<List<DataAccess.Entities.AcsOtp.AcsOtp>> GetListByIdsRawSqlWithDecryptionAsync(List<long> ids,
        CancellationToken cancellationToken,
        params Expression<Func<DataAccess.Entities.AcsOtp.AcsOtp, object>>[] includes)
    {
        var list = await GetListByIdsRawSqlAsync(ids, cancellationToken, includes);

        foreach (var item in list)
        {
            item.Details.OtpInfo.Otp = Encryptor.Decrypt(item.Details.OtpInfo.Otp);
        }
        
        return list.ToList();
    }

    public async Task AddWithEncriptionAsync(DataAccess.Entities.AcsOtp.AcsOtp entity, CancellationToken cancellationToken = default)
    {
        entity.Details.OtpInfo.Otp = Encryptor.Encrypt(entity.Details.OtpInfo.Otp);
        
        await AddAsync(entity, cancellationToken);
    }
}
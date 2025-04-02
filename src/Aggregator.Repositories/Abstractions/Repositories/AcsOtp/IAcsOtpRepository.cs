using System.Linq.Expressions;

namespace Aggregator.Repositories.Abstractions.Repositories.AcsOtp;

public interface IAcsOtpRepository : IRepository<DataAccess.Entities.AcsOtp.AcsOtp>
{
    Task<List<DataAccess.Entities.AcsOtp.AcsOtp>> GetListByIdsRawSqlWithDecryptionAsync(List<long> ids,
        CancellationToken cancellationToken,
        params Expression<Func<DataAccess.Entities.AcsOtp.AcsOtp, object>>[] includes);

    Task AddWithEncriptionAsync(DataAccess.Entities.AcsOtp.AcsOtp entity,
        CancellationToken cancellationToken = default);
}
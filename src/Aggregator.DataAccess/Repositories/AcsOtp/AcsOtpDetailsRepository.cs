using Aggregator.DataAccess.Abstractions.Repositories.AcsOtp;

namespace Aggregator.DataAccess.Repositories.AcsOtp;

public class AcsOtpDetailsRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.AcsOtp.AcsOtpDetails>(context), IAcsOtpDetailsRepository;
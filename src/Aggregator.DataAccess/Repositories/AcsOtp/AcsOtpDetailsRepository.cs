using Aggregator.DataAccess.Abstractions.Repositories.AcsOtp;

namespace Aggregator.DataAccess.Repositories.AcsOtp;

/// <summary>
/// Репозиторий для работы с AccountInfo.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAcsOtpDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="DataAccess.Entities.AcsOtp.AcsOtpDetails"/>.
/// </remarks>
public class AcsOtpDetailsRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.AcsOtp.AcsOtpDetails>(context), IAcsOtpDetailsRepository;
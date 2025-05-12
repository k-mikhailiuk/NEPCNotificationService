using Aggregator.DataAccess.Entities.AcsOtp;

namespace Aggregator.DataAccess.Abstractions.Repositories.AcsOtp;

/// <summary>
/// Репозиторий для работы с AcsOtpDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAcsOtpRepository"/> и наследует базовый класс 
/// </remarks>
public interface IAcsOtpDetailsRepository : IRepository<AcsOtpDetails>;
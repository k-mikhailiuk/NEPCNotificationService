using Aggregator.DataAccess.Abstractions.Repositories.AcsOtp;
using Common;

namespace Aggregator.DataAccess.Repositories.AcsOtp;

/// <summary>
/// Репозиторий для работы с AcsOtp.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAcsOtpRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AcsOtp"/>.
/// </remarks>
public class AcsOtpRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.AcsOtp.AcsOtp>(context), IAcsOtpRepository
{
    /// <inheritdoc/>
    public void AddWithEncryption(DataAccess.Entities.AcsOtp.AcsOtp entity)
    {
        entity.Details.OtpInfo.Otp = Encryptor.Encrypt(entity.Details.OtpInfo.Otp);

        Add(entity);
    }
}
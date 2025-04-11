using System.Linq.Expressions;
using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.AcsOtp;
using Common;

namespace Aggregator.Repositories.Repositories.AcsOtp;

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
    /// <summary>
    /// Асинхронно получает список объектов <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> по заданным идентификаторам с применением дешифрования.
    /// </summary>
    /// <param name="ids">Список идентификаторов объектов.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <param name="includes">Выражения для включения связанных сущностей.</param>
    /// <returns>Список объектов <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> с расшифрованными данными.</returns>
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

    /// <summary>
    /// Асинхронно добавляет объект <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> с применением шифрования.
    /// </summary>
    /// <param name="entity">Объект <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> для добавления.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию добавления объекта с шифрованием.</returns>
    public async Task AddWithEncriptionAsync(DataAccess.Entities.AcsOtp.AcsOtp entity, CancellationToken cancellationToken = default)
    {
        entity.Details.OtpInfo.Otp = Encryptor.Encrypt(entity.Details.OtpInfo.Otp);
        
        await AddAsync(entity, cancellationToken);
    }
}
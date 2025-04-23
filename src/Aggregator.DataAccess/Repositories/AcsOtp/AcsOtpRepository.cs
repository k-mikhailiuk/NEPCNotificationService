using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
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
public class AcsOtpRepository(AggregatorDbContext context, INotificationsRepository notifications)
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
        var list = await GetByIdsWithIncludesAsync(ids, cancellationToken, includes);

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
    /// <returns>Задача, представляющая асинхронную операцию добавления объекта с шифрованием.</returns>
    public void AddWithEncription(DataAccess.Entities.AcsOtp.AcsOtp entity)
    {
        entity.Details.OtpInfo.Otp = Encryptor.Encrypt(entity.Details.OtpInfo.Otp);

        Add(entity);
    }

    /// <inheritdoc/>
    public Task<List<DataAccess.Entities.AcsOtp.AcsOtp>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default)
    {
        return notifications.GetListByIdsAsync<DataAccess.Entities.AcsOtp.AcsOtp>(ids, ct);
    }

    /// <summary>
    /// Получает уведомления типа <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> по их идентификаторам
    /// с загрузкой указанных навигационных свойств.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> с подгруженными зависимостями.</returns>
    private async Task<List<DataAccess.Entities.AcsOtp.AcsOtp>> GetByIdsWithIncludesAsync(
        List<long> ids,
        CancellationToken ct = default,
        params Expression<Func<Entities.AcsOtp.AcsOtp, object>>[] includes)
    {
        return await notifications.GetListByIdsAsync(ids, ct, includes);
    }
}
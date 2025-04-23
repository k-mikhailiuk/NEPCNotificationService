using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories.AcsOtp;

/// <summary>
/// Интерфейс репозитория для работы с AcsOtp.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="AcsOtp"/>.
/// </remarks>
public interface IAcsOtpRepository : IRepository<DataAccess.Entities.AcsOtp.AcsOtp>
{
    /// <summary>
    /// Асинхронно получает список объектов <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> по их идентификаторам с применением дешифрования.
    /// </summary>
    /// <param name="ids">Список идентификаторов объектов.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <param name="includes">Выражения для включения связанных сущностей.</param>
    /// <returns>Список объектов <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> с применённым дешифрованием.</returns>
    Task<List<DataAccess.Entities.AcsOtp.AcsOtp>> GetListByIdsRawSqlWithDecryptionAsync(List<long> ids,
        CancellationToken cancellationToken,
        params Expression<Func<DataAccess.Entities.AcsOtp.AcsOtp, object>>[] includes);

    /// <summary>
    /// Асинхронно добавляет объект <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> с применением шифрования.
    /// </summary>
    /// <param name="entity">Объект <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> для добавления.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию добавления объекта с шифрованием.</returns>
    void AddWithEncription(DataAccess.Entities.AcsOtp.AcsOtp entity);
    
    Task<List<DataAccess.Entities.AcsOtp.AcsOtp>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default);
}
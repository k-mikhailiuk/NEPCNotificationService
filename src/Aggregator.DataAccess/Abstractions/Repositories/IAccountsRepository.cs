using Aggregator.DataAccess.Entities.ABSEntities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с Account.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="Account"/>.
/// </remarks>
public interface IAccountsRepository : IRepository<Account>
{
    /// <summary>
    /// Асинхронная операция по получения соответствия AccountNo CustomerId
    /// </summary>
    /// <param name="accountNos">Коллекция AccountNo</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Асинхронную операцию содержащую данные по соответствиям AccountNo CustomerId</returns>
    Task<Dictionary<string, int>> GetAccountCustomerMapAsync(IReadOnlyCollection<string> accountNos,
        CancellationToken cancellationToken);
}
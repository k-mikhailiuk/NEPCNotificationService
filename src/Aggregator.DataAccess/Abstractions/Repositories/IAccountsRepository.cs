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
    IQueryable<Account> GetByAccountNos(List<string?> accountNos);
}
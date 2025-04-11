using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.Repositories.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с AccountsInfo.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="AccountsInfo"/>.
/// </remarks>
public interface IAccountsInfoRepository : IRepository<AccountsInfo>
{
    
}
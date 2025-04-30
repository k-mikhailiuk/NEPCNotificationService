using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с AccountInfo.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="AccountInfo"/>.
/// </remarks>
public interface IAccountsInfoRepository : IRepository<AccountInfo>
{
    
}
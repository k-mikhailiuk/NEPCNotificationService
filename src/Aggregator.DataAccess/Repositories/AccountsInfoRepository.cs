using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с AccountsInfo.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAccountsInfoRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AccountsInfo"/>.
/// </remarks>
public class AccountsInfoRepository(AggregatorDbContext context)
    : Repository<AccountsInfo>(context), IAccountsInfoRepository;
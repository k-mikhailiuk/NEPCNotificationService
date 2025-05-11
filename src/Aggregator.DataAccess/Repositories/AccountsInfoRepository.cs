using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с AccountInfo.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAccountsInfoRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AccountInfo"/>.
/// </remarks>
public class AccountsInfoRepository(AggregatorDbContext context)
    : Repository<AccountInfo>(context), IAccountsInfoRepository;
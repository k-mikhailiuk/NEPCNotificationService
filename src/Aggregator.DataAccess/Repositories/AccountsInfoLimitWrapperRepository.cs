using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с AccountsInfoLimitWrapper.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAccountsInfoLimitWrapperRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AccountsInfoLimitWrapper"/>.
/// </remarks>
public class AccountsInfoLimitWrapperRepository(AggregatorDbContext context)
    : Repository<AccountsInfoLimitWrapper>(context), IAccountsInfoLimitWrapperRepository;
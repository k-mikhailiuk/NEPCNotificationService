using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;

namespace Aggregator.Repositories.Repositories.AcctBalChange;

/// <summary>
/// Репозиторий для работы с деталями изменения баланса счета.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAcctBalChangeDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AcctBalChangeDetails"/>.
/// </remarks>
public class AcctBalChangeDetailsRepository(AggregatorDbContext context)
    : Repository<AcctBalChangeDetails>(context), IAcctBalChangeDetailsRepository;
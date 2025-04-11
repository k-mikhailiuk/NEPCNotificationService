using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;

namespace Aggregator.Repositories.Repositories.AcctBalChange;

/// <summary>
/// Репозиторий для работы с AcctBalChange.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAcctBalChangeRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AcctBalChange"/>.
/// </remarks>
public class AcctBalChangeRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.AcctBalChange.AcctBalChange>(context), IAcctBalChangeRepository;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с FinTransaction.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IFinTransactionRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="FinTransaction"/>.
/// </remarks>
public class FinTransactionRepository(AggregatorDbContext context)
    : Repository<FinTransaction>(context), IFinTransactionRepository;
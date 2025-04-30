using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с FinTransaction.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="FinTransaction"/>.
/// </remarks>
public interface IFinTransactionRepository : IRepository<FinTransaction>
{
    
}
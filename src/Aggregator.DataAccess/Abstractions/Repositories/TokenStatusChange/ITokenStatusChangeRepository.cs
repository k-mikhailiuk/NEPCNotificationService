namespace Aggregator.DataAccess.Abstractions.Repositories.TokenStatusChange;

/// <summary>
/// Интерфейс репозитория для работы с TokenStatusChange.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="TokenStatusChange"/>.
/// </remarks>
public interface ITokenStatusChangeRepository : IRepository<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>
{
    
}
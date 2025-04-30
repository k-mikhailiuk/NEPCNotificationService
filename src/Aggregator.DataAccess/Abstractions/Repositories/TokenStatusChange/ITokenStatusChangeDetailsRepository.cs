using Aggregator.DataAccess.Entities.TokenChangeStatus;

namespace Aggregator.DataAccess.Abstractions.Repositories.TokenStatusChange;

/// <summary>
/// Интерфейс репозитория для работы с TokenStatusChangeDetails.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="TokenStatusChangeDetails"/>.
/// </remarks>
public interface ITokenStatusChangeDetailsRepository : IRepository<TokenStatusChangeDetails>
{
    
}
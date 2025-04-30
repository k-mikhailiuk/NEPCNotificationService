using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с CheckedLimit.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="CheckedLimit"/>.
/// </remarks>
public interface ICheckedLimitRepository : IRepository<CheckedLimit>
{
    
}
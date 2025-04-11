using Aggregator.DataAccess.Entities;

namespace Aggregator.Repositories.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с ExtensionParameter.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="ExtensionParameter"/>.
/// </remarks>
public interface IExtensionParameterRepository : IRepository<ExtensionParameter>
{
    
}
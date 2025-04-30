using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с InboxArchiveMessage.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="InboxArchiveMessage"/>.
/// </remarks>
public interface IInboxArchiveMessageRepository : IRepository<InboxArchiveMessage>
{
    
}
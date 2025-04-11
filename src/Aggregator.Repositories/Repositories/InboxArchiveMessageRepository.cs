using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с InboxArchiveMessage.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IInboxArchiveMessageRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="InboxArchiveMessage"/>.
/// </remarks>
public class InboxArchiveMessageRepository(AggregatorDbContext context)
    : Repository<InboxArchiveMessage>(context), IInboxArchiveMessageRepository;
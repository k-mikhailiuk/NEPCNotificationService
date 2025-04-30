using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с NotificationMessage.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="INotificationMessageRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="NotificationMessage"/>.
/// </remarks>
public class NotificationMessageRepository(AggregatorDbContext context)
    : Repository<NotificationMessage>(context), INotificationMessageRepository;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с NotificationMessage.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="INotificationMessageRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="NotificationMessage"/>.
/// </remarks>
public class NotificationMessageRepository(AggregatorDbContext context)
    : Repository<NotificationMessage>(context), INotificationMessageRepository;
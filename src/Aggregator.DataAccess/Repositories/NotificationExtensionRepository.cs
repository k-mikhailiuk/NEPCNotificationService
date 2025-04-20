using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с NotificationExtension.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="INotificationExtensionRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="NotificationExtension"/>.
/// </remarks>
public class NotificationExtensionRepository(AggregatorDbContext context)
    : Repository<NotificationExtension>(context), INotificationExtensionRepository;
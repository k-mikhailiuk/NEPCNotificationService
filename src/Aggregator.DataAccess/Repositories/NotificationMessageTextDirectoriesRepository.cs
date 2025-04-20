using Aggregator.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с NotificationMessageTextDirectory.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="INotificationMessageTextDirectoriesRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="NotificationMessageTextDirectory"/>.
/// </remarks>
public class NotificationMessageTextDirectoriesRepository(AggregatorDbContext context)
    : Repository<NotificationMessageTextDirectory>(context),
        INotificationMessageTextDirectoriesRepository;
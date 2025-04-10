using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace Aggregator.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с NotificationMessageKeyWord.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="INotificationMessageKeyWordsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="NotificationMessageKeyWord"/>.
/// </remarks>
public class NotificationMessageKeyWordsRepository(AggregatorDbContext context)
    : Repository<NotificationMessageKeyWord>(context),
        INotificationMessageKeyWordsRepository;
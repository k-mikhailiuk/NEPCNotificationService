using Aggregator.DataAccess.Entities;
using NotificationService.DataAccess.Abstractions;

namespace NotificationService.DataAccess;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="NotificationMessage"/>.
/// Наследуется от универсального <see cref="Repository{T}"/> и реализует <see cref="INotificationMessagesRepository"/>.
/// </summary>
/// <param name="context">Контекст базы данных уведомлений.</param>
public class NotificationMessagesRepository(NotificationServiceDbContext context)
    : Repository<NotificationMessage>(context), INotificationMessagesRepository;
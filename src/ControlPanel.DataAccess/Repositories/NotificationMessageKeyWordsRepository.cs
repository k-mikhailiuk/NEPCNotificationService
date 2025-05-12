using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с NotificationMessageKeyWord.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="INotificationMessageKeyWordsRepository"/> и наследует базовый класс <see cref="Repository{NotificationMessageKeyWord}"/>.
/// </remarks>
public class NotificationMessageKeyWordsRepository(ControlPanelDbContext context)
    : Repository<NotificationMessageKeyWord>(context),
        INotificationMessageKeyWordsRepository;
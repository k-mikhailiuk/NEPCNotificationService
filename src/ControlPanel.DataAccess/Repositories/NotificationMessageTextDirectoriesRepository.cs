using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с NotificationMessageTextDirectory.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="INotificationMessageTextDirectoriesRepository"/> и наследует базовый класс <see cref="Repository{NotificationMessageTextDirectory}"/>.
/// </remarks>
public class NotificationMessageTextDirectoriesRepository(ControlPanelDbContext context)
    : Repository<NotificationMessageTextDirectory>(context),
        INotificationMessageTextDirectoriesRepository;
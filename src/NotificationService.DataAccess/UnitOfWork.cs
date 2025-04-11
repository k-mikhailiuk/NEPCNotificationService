using Microsoft.Extensions.DependencyInjection;
using NotificationService.DataAccess.Abstractions;

namespace NotificationService.DataAccess;

/// <summary>
/// Реализация паттерна Unit of Work для работы с репозиториями и транзакциями.
/// Обеспечивает единое хранилище для работы с данными и управления жизненным циклом DbContext.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly NotificationServiceDbContext _context;

    /// <summary>
    /// Репозиторий для работы с сущностями уведомлений.
    /// </summary>
    public INotificationMessagesRepository NotificationMessages => _notificationMessages.Value;
    private readonly Lazy<INotificationMessagesRepository> _notificationMessages;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UnitOfWork"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <param name="serviceProvider">Провайдер сервисов для создания репозиториев.</param>
    public UnitOfWork(NotificationServiceDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;

        _notificationMessages = new Lazy<INotificationMessagesRepository>(() =>
            serviceProvider.GetService<INotificationMessagesRepository>() ?? throw new InvalidOperationException());
    }
    
    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    /// <summary>
    /// Освобождает ресурсы контекста базы данных.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }
}
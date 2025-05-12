using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ControlPanel.DataAccess;

/// <inheritdoc/>
public class ControlPanelUnitOfWork : IControlPanelUnitOfWork
{
    private readonly ControlPanelDbContext _context;

    /// <inheritdoc/>
    public INotificationMessageKeyWordsRepository NotificationMessageKeyWords => _notificationMessageKeyWords.Value;
    private readonly Lazy<INotificationMessageKeyWordsRepository> _notificationMessageKeyWords;

    /// <inheritdoc/>
    public INotificationMessageTextDirectoriesRepository NotificationMessageTextDirectories => _notificationMessageTextDirectories.Value;
    private readonly Lazy<INotificationMessageTextDirectoriesRepository> _notificationMessageTextDirectories;
    
    /// <inheritdoc/>
    public ICurrenciesRepository Currencies => _currencies.Value;
    private readonly Lazy<ICurrenciesRepository> _currencies;
    
    /// <inheritdoc/>
    public ILimitIdDescriptionDirectoriesRepository LimitIdDescriptionDirectories => _limitIdDescriptionDirectories.Value;
    private readonly Lazy<ILimitIdDescriptionDirectoriesRepository> _limitIdDescriptionDirectories;

    /// <summary>
    /// Конструктор, инициализирующий единицу работы.
    /// </summary>
    /// <param name="context">Контекст базы данных ControlPanel.</param>
    /// <param name="serviceProvider">Провайдер сервисов для разрешения зависимостей репозиториев.</param>
    public ControlPanelUnitOfWork(ControlPanelDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;

        _notificationMessageKeyWords = new Lazy<INotificationMessageKeyWordsRepository>(() =>
            serviceProvider.GetService<INotificationMessageKeyWordsRepository>() ?? throw new InvalidOperationException());
        
        _notificationMessageTextDirectories = new Lazy<INotificationMessageTextDirectoriesRepository>(() =>
            serviceProvider.GetService<INotificationMessageTextDirectoriesRepository>() ?? throw new InvalidOperationException());
        
        _currencies = new Lazy<ICurrenciesRepository>(() =>
            serviceProvider.GetService<ICurrenciesRepository>() ?? throw new InvalidOperationException());
        
        _limitIdDescriptionDirectories = new Lazy<ILimitIdDescriptionDirectoriesRepository>(() =>
            serviceProvider.GetService<ILimitIdDescriptionDirectoriesRepository>() ?? throw new InvalidOperationException());
    }

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    /// <summary>
    /// Освобождает ресурсы, используемые контекстом базы данных.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }
}
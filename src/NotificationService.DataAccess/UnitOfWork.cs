using NotificationService.DataAccess.Abstractions;

namespace NotificationService.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly NotificationServiceDbContext _context;

    public INotificationMessageTextDirectoriesRepository NotificationMessageTextDirectories => _notificationMessageTextDirectories.Value;
    private readonly Lazy<INotificationMessageTextDirectoriesRepository> _notificationMessageTextDirectories;

    public UnitOfWork(NotificationServiceDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;

        _notificationMessageTextDirectories = new Lazy<INotificationMessageTextDirectoriesRepository>(() =>
            serviceProvider.GetService<INotificationMessageTextDirectoriesRepository>() ?? throw new InvalidOperationException());
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _context.Dispose();
    }
}
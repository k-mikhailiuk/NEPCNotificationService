using Microsoft.Extensions.DependencyInjection;
using NotificationService.DataAccess.Abstractions;

namespace NotificationService.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly NotificationServiceDbContext _context;

    public INotificationMessagesRepository NotificationMessages => _notificationMessages.Value;
    private readonly Lazy<INotificationMessagesRepository> _notificationMessages;

    public UnitOfWork(NotificationServiceDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;

        _notificationMessages = new Lazy<INotificationMessagesRepository>(() =>
            serviceProvider.GetService<INotificationMessagesRepository>() ?? throw new InvalidOperationException());
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _context.Dispose();
    }
}
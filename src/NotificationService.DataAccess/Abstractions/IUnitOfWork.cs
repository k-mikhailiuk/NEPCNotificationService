namespace NotificationService.DataAccess.Abstractions;

public interface IUnitOfWork : IDisposable
{
    INotificationMessagesRepository NotificationMessages { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

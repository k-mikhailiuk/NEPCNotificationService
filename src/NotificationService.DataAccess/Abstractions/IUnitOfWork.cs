namespace NotificationService.DataAccess.Abstractions;

public interface IUnitOfWork : IDisposable
{
    INotificationMessageTextDirectoriesRepository NotificationMessageTextDirectories { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

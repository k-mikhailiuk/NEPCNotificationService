using ControlPanel.DataAccess.Abstractions.Repositories;

namespace ControlPanel.DataAccess.Abstractions;

public interface IUnitOfWork : IDisposable
{
    INotificationMessageKeyWordsRepository NotificationMessageKeyWords { get; }
    
    INotificationMessageTextDirectoriesRepository NotificationMessageTextDirectories { get; }
    ICurrenciesRepository Currencies { get; }
    ILimitIdDescriptionDirectoriesRepository LimitIdDescriptionDirectories { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : class;
    
    void BeginTransactionAsync();
    
    void CommitTransactionAsync();
    
    void RollbackTransactionAsync();
}
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControlPanel.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ControlPanelDbContext _context;

    public INotificationMessageKeyWordsRepository NotificationMessageKeyWords => _notificationMessageKeyWords.Value;
    private readonly Lazy<INotificationMessageKeyWordsRepository> _notificationMessageKeyWords;

    public INotificationMessageTextDirectoriesRepository NotificationMessageTextDirectories => _notificationMessageTextDirectories.Value;
    private readonly Lazy<INotificationMessageTextDirectoriesRepository> _notificationMessageTextDirectories;
    
    public ICurrenciesRepository Currencies => _currencies.Value;
    private readonly Lazy<ICurrenciesRepository> _currencies;

    public UnitOfWork(ControlPanelDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;

        _notificationMessageKeyWords = new Lazy<INotificationMessageKeyWordsRepository>(() =>
            serviceProvider.GetService<INotificationMessageKeyWordsRepository>() ?? throw new InvalidOperationException());
        
        _notificationMessageTextDirectories = new Lazy<INotificationMessageTextDirectoriesRepository>(() =>
            serviceProvider.GetService<INotificationMessageTextDirectoriesRepository>() ?? throw new InvalidOperationException());
        
        _currencies = new Lazy<ICurrenciesRepository>(() =>
            serviceProvider.GetService<ICurrenciesRepository>() ?? throw new InvalidOperationException());
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    public IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : class
    {
        return _context.Set<T>().FromSqlRaw(sql, parameters);
    }

    public void BeginTransactionAsync()
    {
        _context.Database.BeginTransactionAsync();
    }

    public void CommitTransactionAsync()
    {
        _context.Database.CommitTransactionAsync();
    }

    public void RollbackTransactionAsync()
    {
        _context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
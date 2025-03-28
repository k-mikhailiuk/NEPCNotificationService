using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NotificationService.DataAccess.Abstractions;

namespace NotificationService.DataAccess;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly NotificationServiceDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected Repository(NotificationServiceDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public Task<List<T>> GetAllWithConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
       return _dbSet.Where(expression).ToListAsync(cancellationToken);
    }
}
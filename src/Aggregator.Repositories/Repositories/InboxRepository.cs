using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using DataIngrestorApi.DataAccess.Entities;
using DataIngrestorApi.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Repositories.Repositories;

public class InboxRepository(AggregatorDbContext context) : Repository<InboxMessage>(context), IInboxRepository
{
    public async Task<List<InboxMessage>> GetUnprocessedMessagesAsync(int batchSize, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => x.Status == InboxMessageStatus.New).Take(batchSize).ToListAsync(cancellationToken);
    }

    public async Task<List<InboxMessage>> GetByIdsAsync(List<long> ids, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }
}
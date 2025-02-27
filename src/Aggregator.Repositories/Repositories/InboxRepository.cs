using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using DataIngrestorApi.DataAccess.Entities;
using DataIngrestorApi.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Repositories.Repositories;

public class InboxRepository : Repository<InboxMessage>, IInboxRepository
{

    public InboxRepository(AggregatorDbContext context) : base(context)
    {
    }

    public async Task<List<InboxMessage>> GetUnprocessedMessagesAsync(int batchSize)
    {
       return await _dbSet.Where(x => x.Status == InboxMessageStatus.New).Take(batchSize).ToListAsync();
    }
}
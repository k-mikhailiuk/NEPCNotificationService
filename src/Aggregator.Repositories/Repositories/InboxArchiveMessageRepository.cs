using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class InboxArchiveMessageRepository : Repository<InboxArchiveMessage> , IInboxArchiveMessageRepository
{
    public InboxArchiveMessageRepository(AggregatorDbContext context) : base(context)
    {
    }
}
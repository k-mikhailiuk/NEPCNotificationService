using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class InboxArchiveMessageRepository(AggregatorDbContext context)
    : Repository<InboxArchiveMessage>(context), IInboxArchiveMessageRepository;
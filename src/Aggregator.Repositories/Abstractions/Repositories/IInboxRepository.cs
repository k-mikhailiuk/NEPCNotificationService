using DataIngrestorApi.DataAccess.Entities;

namespace Aggregator.Repositories.Abstractions.Repositories;

public interface IInboxRepository : IRepository<InboxMessage>
{
    Task<List<InboxMessage>> GetUnprocessedMessagesAsync(int batchSize, CancellationToken cancellationToken);
    
    Task<List<InboxMessage>> GetByIdsAsync(List<long> ids, CancellationToken cancellationToken);
}
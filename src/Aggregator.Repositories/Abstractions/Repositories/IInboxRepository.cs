using DataIngrestorApi.DataAccess.Entities;

namespace Aggregator.Repositories.Abstractions.Repositories;

public interface IInboxRepository : IRepository<InboxMessage>
{
    Task<IEnumerable<InboxMessage>> GetUnprocessedMessagesAsync();
}
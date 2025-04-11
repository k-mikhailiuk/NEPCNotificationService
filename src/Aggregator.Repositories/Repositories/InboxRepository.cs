using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using DataIngrestorApi.DataAccess.Entities;
using DataIngrestorApi.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с InboxMessage.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IInboxRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="InboxMessage"/>.
/// </remarks>
public class InboxRepository(AggregatorDbContext context) : Repository<InboxMessage>(context), IInboxRepository
{
    /// <summary>
    /// Асинхронно получает пакет необработанных сообщений.
    /// </summary>
    /// <param name="batchSize">Максимальное количество сообщений для выборки.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Список входящих сообщений со статусом <see cref="InboxMessageStatus.New"/>.
    /// </returns>
    public async Task<List<InboxMessage>> GetUnprocessedMessagesAsync(int batchSize, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => x.Status == InboxMessageStatus.New).Take(batchSize).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Асинхронно получает сообщения по заданным идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов сообщений.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Список входящих сообщений, идентификаторы которых содержатся в указанном списке.
    /// </returns>
    public async Task<List<InboxMessage>> GetByIdsAsync(List<long> ids, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }
}
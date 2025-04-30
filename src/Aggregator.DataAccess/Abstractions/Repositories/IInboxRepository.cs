using DataIngrestorApi.DataAccess.Entities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с InboxMessage.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="InboxMessage"/>.
/// </remarks>
public interface IInboxRepository : IRepository<InboxMessage>
{
    /// <summary>
    /// Асинхронно получает необработанные сообщения входящих.
    /// </summary>
    /// <param name="batchSize">Размер пакета сообщений для выборки.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список необработанных сообщений входящих.</returns>
    Task<List<InboxMessage>> GetUnprocessedMessagesAsync(int batchSize, CancellationToken cancellationToken);
    
    /// <summary>
    /// Асинхронно получает сообщения входящих по заданным идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов сообщений.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список сообщений входящих, соответствующих указанным идентификаторам.</returns>
    Task<List<InboxMessage>> GetByIdsAsync(List<long> ids, CancellationToken cancellationToken);
}
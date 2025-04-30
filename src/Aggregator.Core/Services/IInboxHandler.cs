using DataIngrestorApi.DataAccess.Entities;

namespace Aggregator.Core.Services;

/// <summary>
/// Обработчик входящих сообщений.
/// </summary>
public interface IInboxHandler
{
    /// <summary>
    /// Обрабатывает входящие сообщения, обновляет их статус, создаёт команды уведомлений и архивирует сообщения.
    /// </summary>
    /// <param name="messages">Список входящих сообщений.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task HandleAsync(IEnumerable<InboxMessage> messages, CancellationToken cancellationToken);
}
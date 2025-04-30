namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Представляет архивированное сообщение входящих, сохранённое в базе данных.
/// </summary>
public class InboxArchiveMessage
{
    /// <summary>
    /// Получает или задаёт уникальный идентификатор сообщения.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Получает или задаёт временную метку создания сообщения.
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }
    
    /// <summary>
    /// Получает или задаёт полезную нагрузку (содержимое) сообщения.
    /// </summary>
    public string Payload { get; set; }
    
    /// <summary>
    /// Фабричный метод для создания экземпляра <see cref="InboxArchiveMessage"/>.
    /// </summary>
    /// <param name="payload">Тело сообщения.</param>
    /// <returns>Созданный объект <see cref="InboxArchiveMessage"/>.</returns>
    public static InboxArchiveMessage Create(string payload)
    {
        return new InboxArchiveMessage
        {
            Payload = payload,
            Timestamp = DateTimeOffset.UtcNow,
        };
    }
}
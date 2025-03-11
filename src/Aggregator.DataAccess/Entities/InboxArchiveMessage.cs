namespace Aggregator.DataAccess.Entities;

public class InboxArchiveMessage
{
    public long Id { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
    
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
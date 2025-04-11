using System.Text.Json;
using DataIngrestorApi.DataAccess.Entities.Enums;

namespace DataIngrestorApi.DataAccess.Entities;

/// <summary>
/// Модель для хранения сырых сообщений
/// </summary>
public class InboxMessage
{
    /// <summary>
    /// Уникальный идентификатор сообщения
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Время создания сообщения
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }
    
    /// <summary>
    /// Тело сообщения
    /// </summary>
    public string Payload { get; set; }
    
    /// <summary>
    /// Статус обработки сообщения
    /// </summary>
    public InboxMessageStatus Status { get; set; }

    /// <summary>
    /// Фабричный метод для создания экземпляра <see cref="InboxMessage"/>.
    /// </summary>
    /// <param name="batchItem">Элемент пакета уведомлений.</param>
    /// <param name="jsonOptions">Настройки JSON-сериализации.</param>
    /// <returns>Созданный объект <see cref="InboxMessage"/>.</returns>
    public static InboxMessage Create(object batchItem, JsonSerializerOptions jsonOptions)
    {
        return new InboxMessage
        {
            Payload = JsonSerializer.Serialize(batchItem, jsonOptions),
            Timestamp = DateTimeOffset.UtcNow,
            Status = InboxMessageStatus.New
        };
    }
}
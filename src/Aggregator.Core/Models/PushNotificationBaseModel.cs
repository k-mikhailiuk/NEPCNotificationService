namespace Aggregator.Core.Models;

/// <summary>
/// Базовая модель push-уведомления, содержащая основные поля для отправки сообщения.
/// </summary>
public class PushNotificationBaseModel
{
    /// <summary>
    /// Текст уведомления.
    /// </summary>
    public string MessageBody { get; set; }
    
    /// <summary>
    /// Заголовок уведомления.
    /// </summary>
    public string MessageTittle { get; set; }
    
    /// <summary>
    /// Идентификатор получателя (например, токен устройства или пользовательский идентификатор).
    /// </summary>
    public string Destination { get; set; }
}
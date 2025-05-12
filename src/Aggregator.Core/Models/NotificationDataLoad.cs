using ControlPanel.DataAccess.Entities;

namespace Aggregator.Core.Models;

/// <summary>
/// Загруженные данные уведомлений.
/// </summary>
public record NotificationDataLoad<T>
{
    /// <summary>
    /// Набор входящих сообщений.
    /// </summary>
    public IEnumerable<T> Messages { get; set; }
    
    /// <summary>
    /// Тексты уведомлений по их идентификатору.
    /// </summary>
    public Dictionary<long, NotificationMessageTextDirectory> NotificationTextById { get; init; }
    
    /// <summary>
    /// Отображение: идентификатор уведомления → идентификатор клиента.
    /// </summary>
    public Dictionary<long, int> NotificationToCustomer { get; init; }
    
    /// <summary>
    /// Отображение: идентификатор клиента → настройки клиента.
    /// </summary>
    public Dictionary<int, int> CustomerSettingsMap { get; init; }
}
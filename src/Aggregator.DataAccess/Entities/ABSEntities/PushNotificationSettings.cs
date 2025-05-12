namespace Aggregator.DataAccess.Entities.ABSEntities;

/// <summary>
/// Настройки push-уведомлений для конкретного логина/клиента.
/// </summary>
public class PushNotificationSettings
{
    /// <summary>
    /// Уникальный идентификатор логина (ключ записи).
    /// </summary>
    public int LoginID { get; set; }
    
    /// <summary>
    /// Идентификатор клиента, для которого заданы настройки.
    /// </summary>
    public int CustomerID { get; set; }
    
    /// <summary>
    /// Имя логина (например, username или e-mail).
    /// </summary>
    public string? LoginName { get; set; }

    /// <summary>
    /// Признак активности: <c>true</c> — настройки активны, <c>false</c> — неактивны.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Дата и время последнего запроса/проверки уведомлений для этого логина.
    /// </summary>
    public DateTime LastRequestDate { get; set; }

    /// <summary>
    /// Идентификатор языка для локализации push-уведомлений.
    /// </summary>
    public int LanguageID { get; set; }
}
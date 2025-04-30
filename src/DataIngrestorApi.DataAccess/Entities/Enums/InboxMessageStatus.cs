namespace DataIngrestorApi.DataAccess.Entities.Enums;

/// <summary>
/// Статусы обработки сообщений в inbox
/// </summary>
public enum InboxMessageStatus : byte
{
    /// <summary>
    /// Default
    /// </summary>
    Undefined = 0,
    
    /// <summary>
    /// Новое сообщение
    /// </summary>
    New = 1,
    
    /// <summary>
    /// В обработке
    /// </summary>
    InProgress = 2,
    
    /// <summary>
    /// Обработанное сообщение
    /// </summary>
    Completed = 3
}
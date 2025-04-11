namespace ControlPanel.Core.DTOs;

/// <summary>
/// DTO для обновления описания ключевых слов сообщений уведомлений.
/// </summary>
public class UpdateNotificationMessageKeyWordsDescriptionDto
{
    /// <summary>
    /// Идентификатор записи.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }
}
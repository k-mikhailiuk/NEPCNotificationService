namespace ControlPanel.Core.DTOs;

/// <summary>
/// DTO для обновления текстов сообщений справочника уведомлений.
/// </summary>
public class UpdateNotificationMessageDirectoriesTextDto
{
    /// <summary>
    /// Идентификатор записи.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Текст сообщения на русском языке.
    /// </summary>
    public string? MessageTextRu { get; set; }
    
    /// <summary>
    /// Текст сообщения на английском языке.
    /// </summary>
    public string? MessageTextEn { get; set; }
    
    /// <summary>
    /// Текст сообщения на кыргызском языке.
    /// </summary>
    public string? MessageTextKg { get; set; }
    
    /// <summary>
    /// Флаг, указывающий, требуется ли отправка сообщения.
    /// </summary>
    public bool IsNeedSend { get; set; }
}
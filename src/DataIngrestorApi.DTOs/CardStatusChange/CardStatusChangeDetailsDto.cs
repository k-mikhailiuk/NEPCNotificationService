namespace DataIngrestorApi.DTOs.CardStatusChange;

/// <summary>
/// Подробная информация об изменении статуса карты
/// </summary>
public class CardStatusChangeDetailsDto
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; set; }
    
    /// <summary>
    /// Старое значение статуса
    /// </summary>
    public int OldStatus { get; set; }
    
    /// <summary>
    /// Новое значение статуса
    /// </summary>
    public int NewStatus { get; set; }
    
    /// <summary>
    /// Дата изменения статуса карты в ПЦ в формате (YYYYMMDDHH24MISS)
    /// </summary>
    public string ChangeDate { get; set; }
    
    /// <summary>
    /// Сервис, изменивший статус карты
    /// </summary>
    public string? Service { get; set; }
    
    /// <summary>
    /// Пользователь сервиса, изменивший статус карты
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// Причина изменения статуса карты
    /// </summary>
    public string? Note { get; set; }
    
    /// <summary>
    /// Один из идентификаторов карты
    /// </summary>
    public CardIdentifierDto? CardIdentifier { get; set; }
}
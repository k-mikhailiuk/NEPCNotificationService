namespace Aggregator.DTOs.AcctBalChange;

/// <summary>
/// Уведомление об изменении лимита авторизации по факту финансовой обработки
/// </summary>
public class AggregatorAcctBalChangeDto
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; set; }
    
    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string Time { get; set; }
    
    /// <summary>
    /// Детали операции изменения лимита авторизации по факту фин. обработки
    /// </summary>
    public AggregatorAcctBalChangeDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; set; }
    
    /// <summary>
    /// Информация о счетах на момент формирования уведомления
    /// </summary>
    public List<AggregatorAccountInfoDto> AccountsInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; set; }
}
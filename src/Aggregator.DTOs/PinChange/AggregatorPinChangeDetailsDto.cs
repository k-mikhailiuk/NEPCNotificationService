namespace Aggregator.DTOs.PinChange;

/// <summary>
/// Подробная информация об изменении PIN-кода
/// </summary>
public record AggregatorPinChangeDetailsDto
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; init; }
    
    /// <summary>
    /// Время смены PIN-кода в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; init; }
    
    /// <summary>
    /// Статус операции изменения PIN-кода. OK - успешный, NOK - неуспешный
    /// </summary>
    public string Status { get; init; }
    
    /// <summary>
    /// Внутренний код ответа ПЦ
    /// </summary>
    public int? ResponseCode { get; init; }
    
    /// <summary>
    /// Сервис по изменению PIN-кода.
    /// </summary>
    public string Service { get; init; }
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public List<AggregatorCardIdentifierDto>? CardIdentifier { get; init; }
}
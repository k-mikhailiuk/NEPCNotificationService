using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.AcctBalChange;

/// <summary>
/// Уведомление об изменении лимита авторизации по факту финансовой обработки
/// </summary>
public record AggregatorAcctBalChangeDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; init; }
    
    /// <inheritdoc />
    public long EventId { get; init; }
    
    /// <inheritdoc />
    public string Time { get; init; }
    
    /// <summary>
    /// Детали операции изменения лимита авторизации по факту фин. обработки
    /// </summary>
    public AggregatorAcctBalChangeDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; init; }
    
    /// <summary>
    /// Информация о счетах на момент формирования уведомления
    /// </summary>
    public List<AggregatorAccountInfoDto> AccountsInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; init; }
}
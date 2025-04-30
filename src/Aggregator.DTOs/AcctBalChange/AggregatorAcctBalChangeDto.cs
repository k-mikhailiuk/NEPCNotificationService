using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.AcctBalChange;

/// <summary>
/// Уведомление об изменении лимита авторизации по факту финансовой обработки
/// </summary>
public record AggregatorAcctBalChangeDto : NotificationAggregatorDto<AggregatorAcctBalChangeDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; init; }
    
    /// <summary>
    /// Информация о счетах на момент формирования уведомления
    /// </summary>
    public IEnumerable<AggregatorAccountInfoDto> AccountsInfo { get; init; }
}
using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.IssFinAuth;

/// <summary>
/// Уведомление о финансовой авторизации по номеру счета банка-эмитента
/// </summary>
public record AggregatorIssFinAuthDto : NotificationAggregatorDto<AggregatorIssFinAuthDetailsDto>
{
    /// <summary>
    /// Информация о карте и ее лимитах на момент формирования уведомления
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; init; }
    
    /// <summary>
    /// Информация о счетах на момент формирования уведомления
    /// </summary>
    public IEnumerable<AggregatorAccountInfoDto> AccountsInfo { get; init; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorMerchantInfoDto MerchantInfo { get; init; }
}
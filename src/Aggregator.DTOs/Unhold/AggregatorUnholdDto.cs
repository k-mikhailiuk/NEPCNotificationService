using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.Unhold;

/// <summary>
/// Уведомление о снятии холда
/// </summary>
public record AggregatorUnholdDto : NotificationAggregatorDto<AggregatorUnholdDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; init; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorMerchantInfoDto MerchantInfo { get; init; }
}
using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.AcqFinAuth;

/// <summary>
/// Уведомление об онлайн эквайринговых финансовых авторизациях по картам
/// </summary>
public record AggregatorAcqFinAuthDto : NotificationAggregatorDto<AggregatorAcqFinAuthDetailsDto>
{
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorMerchantInfoDto MerchantInfo { get; init; }
}
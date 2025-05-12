using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.AcsOtp;

/// <summary>
/// Уведомление о разовых паролях, отправляемых ACS банка-эмитента карты
/// </summary>
public record AggregatorOtpDto : NotificationAggregatorDto<AggregatorAcsOtpDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; init; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorAcsOtpMerchantInfoDto MerchantInfo { get; init; }
}
using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.AcsOtp;

/// <summary>
/// Уведомление о разовых паролях, отправляемых ACS банка-эмитента карты
/// </summary>
public record AggregatorAcsOtpDto : INotificationAggregatorDto
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; init; }
    
    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string Time { get; init; }
    
    /// <summary>
    /// Детали разовых паролей, отправляемых ACS банка-эмитента карты
    /// </summary>
    public AggregatorAcsOtpDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; init; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorAcsOtpMerchantInfoDto MerchantInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; init; }
}
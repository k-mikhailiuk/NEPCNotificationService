using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.AcsOtp;

public class AggregatorAcsOtpDto : INotificationAggregatorDto
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
    /// Детали разовых паролей, отправляемых ACS банка-эмитента карты
    /// </summary>
    public AggregatorAcsOtpDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; set; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorAcsOtpMerchantInfoDto MerchantInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; set; }
}
using DataIngrestorApi.DTOs.Abstractions;

namespace DataIngrestorApi.DTOs.AcsOtp;

/// <summary>
/// Уведомление о разовых паролях, отправляемых ACS банка-эмитента карты
/// </summary>
public record AcsOtpDto : NotificationDto<AcsOtpDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto CardInfo { get; init; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AcsOtpMerchantInfoDto MerchantInfo { get; init; }
}
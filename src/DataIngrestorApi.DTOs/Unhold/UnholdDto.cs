using DataIngrestorApi.DTOs.Abstractions;

namespace DataIngrestorApi.DTOs.Unhold;

/// <summary>
/// Уведомление о снятии холда
/// </summary>
public record UnholdDto : NotificationDto<UnholdDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto CardInfo { get; init; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public MerchantInfoDto MerchantInfo { get; init; }
}
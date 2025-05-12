using DataIngrestorApi.DTOs.Abstractions;

namespace DataIngrestorApi.DTOs.AcqFinAuth;

/// <summary>
/// Уведомление об онлайн эквайринговых финансовых авторизациях по картам
/// </summary>
public record AcqFinAuthDto : NotificationDto<AcqFinAuthDetailsDto>
{
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public MerchantInfoDto MerchantInfo { get; init; }
}
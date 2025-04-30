using DataIngrestorApi.DTOs.Abstractions;

namespace DataIngrestorApi.DTOs.TokenStatusChange;

/// <summary>
/// Уведомление об изменении статуса токена
/// </summary>
public record TokenStatusChangeDto : NotificationDto<TokenStatusChangeDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto CardInfo { get; init; }
}
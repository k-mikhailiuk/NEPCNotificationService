using DataIngrestorApi.DTOs.Abstractions;

namespace DataIngrestorApi.DTOs.PinChange;

/// <summary>
/// Уведомление об изменении PIN-кода
/// </summary>
public record PinChangeDto : NotificationDto<PinChangeDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto CardInfo { get; init; }
}
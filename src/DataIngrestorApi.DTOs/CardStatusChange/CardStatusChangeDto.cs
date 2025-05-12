using DataIngrestorApi.DTOs.Abstractions;

namespace DataIngrestorApi.DTOs.CardStatusChange;

/// <summary>
/// Уведомление об изменении статуса карты
/// </summary>
public record CardStatusChangeDto : NotificationDto<CardStatusChangeDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto CardInfo { get; init; }
}
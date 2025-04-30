using DataIngrestorApi.DTOs.Abstractions;

namespace DataIngrestorApi.DTOs.OwiUserAction;

/// <summary>
/// Уведомление о действии пользователя в OWI
/// </summary>
public record OwiUserActionDto : NotificationDto<OwiUserActionDetailsDto>
{
    /// <summary>
    /// Информация о карте на момент формирования уведомления
    /// </summary>
    public CardInfoDto? CardInfo { get; init; }
}
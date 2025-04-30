using DataIngrestorApi.DTOs.Abstractions;

namespace DataIngrestorApi.DTOs.AcctBalChange;

/// <summary>
/// Уведомление об изменении лимита авторизации по факту финансовой обработки
/// </summary>
public record AcctBalChangeDto : NotificationDto<AcctBalChangeDetailsDto>
{ 
    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto? CardInfo { get; init; }
    
    /// <summary>
    /// Информация о счетах на момент формирования уведомления
    /// </summary>
    public IEnumerable<AccountInfoDto> AccountsInfo { get; init; }
}
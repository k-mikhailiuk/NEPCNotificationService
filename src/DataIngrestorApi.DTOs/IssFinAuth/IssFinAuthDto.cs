using DataIngrestorApi.DTOs.Abstractions;

namespace DataIngrestorApi.DTOs.IssFinAuth;

/// <summary>
/// Уведомление о финансовой авторизации по номеру счета банка-эмитента
/// </summary>
public record IssFinAuthDto : NotificationDto<IssFinAuthDetailsDto>
{
    /// <summary>
    /// Информация о карте и ее лимитах на момент формирования уведомления
    /// </summary>
    public CardInfoDto? CardInfo { get; init; }

    /// <summary>
    /// Информация о счетах на момент формирования уведомления
    /// </summary>
    public List<AccountInfoDto> AccountsInfo { get; init; }

    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public MerchantInfoDto MerchantInfo { get; init; }
}
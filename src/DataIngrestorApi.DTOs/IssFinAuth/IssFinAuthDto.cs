namespace DataIngrestorApi.DTOs.IssFinAuth;

/// <summary>
/// Уведомление о финансовой авторизации по номеру счета банка-эмитента
/// </summary>
public class IssFinAuthDto
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
    /// Детали финансовой авторизации по карте банка-эмитента
    /// </summary>
    public IssFinAuthDetailsDto Details { get; set; }

    /// <summary>
    /// Информация о карте и ее лимитах на момент формирования уведомления
    /// </summary>
    public CardInfoDto? CardInfo { get; set; }

    /// <summary>
    /// Информация о счетах на момент формирования уведомления
    /// </summary>
    public List<AccountInfoDto> AccountsInfo { get; set; }

    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public MerchantInfoDto MerchantInfo { get; set; }

    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; set; }
}
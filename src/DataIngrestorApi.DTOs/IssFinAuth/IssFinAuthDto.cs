namespace DataIngrestorApi.DTOs.IssFinAuth;

/// <summary>
/// Уведомление о финансовой авторизации по номеру счета банка-эмитента
/// </summary>
public record IssFinAuthDto
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; init; }

    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string Time { get; init; }

    /// <summary>
    /// Детали финансовой авторизации по карте банка-эмитента
    /// </summary>
    public IssFinAuthDetailsDto Details { get; init; }

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

    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; init; }
}
namespace DataIngrestorApi.DTOs.PinChange;

/// <summary>
/// Уведомление об изменении PIN-кода
/// </summary>
public record PinChangeDto
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
    /// Подробная информация об изменении PIN-кода
    /// </summary>
    public PinChangeDetailsDto Details { get; init; }

    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto CardInfo { get; init; }

    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; init; }
}
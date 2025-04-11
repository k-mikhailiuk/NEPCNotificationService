namespace DataIngrestorApi.DTOs.PinChange;

/// <summary>
/// Уведомление об изменении PIN-кода
/// </summary>
public class PinChangeDto
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
    /// Подробная информация об изменении PIN-кода
    /// </summary>
    public PinChangeDetailsDto Details { get; set; }

    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto CardInfo { get; set; }

    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; set; }
}
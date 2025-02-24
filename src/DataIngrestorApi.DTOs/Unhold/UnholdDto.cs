namespace DataIngrestorApi.DTOs.Unhold;

/// <summary>
/// Уведомление о снятии холда
/// </summary>
public class UnholdDto
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
    /// Подробная информация о снятии холда
    /// </summary>
    public UnholdDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto CardInfo { get; set; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public MerchantInfoDto MerchantInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; set; }
}
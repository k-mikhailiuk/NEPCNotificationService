namespace DataIngrestorApi.DTOs.AcqFinAuth;

/// <summary>
/// Уведомление об онлайн эквайринговых финансовых авторизациях по картам
/// </summary>
public class AcqFinAuthDto
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
    /// Детали онлайн эквайринговой финансовой авторизации по карте
    /// </summary>
    public AcqFinAuthDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public MerchantInfoDto MerchantInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; set; }
}
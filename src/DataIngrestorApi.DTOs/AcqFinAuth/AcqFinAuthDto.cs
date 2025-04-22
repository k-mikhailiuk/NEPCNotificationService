namespace DataIngrestorApi.DTOs.AcqFinAuth;

/// <summary>
/// Уведомление об онлайн эквайринговых финансовых авторизациях по картам
/// </summary>
public record AcqFinAuthDto
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
    /// Детали онлайн эквайринговой финансовой авторизации по карте
    /// </summary>
    public AcqFinAuthDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public MerchantInfoDto MerchantInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; init; }
}
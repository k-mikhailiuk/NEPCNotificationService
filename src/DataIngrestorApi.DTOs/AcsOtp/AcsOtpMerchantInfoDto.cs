namespace DataIngrestorApi.DTOs.AcsOtp;

/// <summary>
/// Информация о мерчанте
/// </summary>
public class AcsOtpMerchantInfoDto
{
    /// <summary>
    /// Идентификатор мерчанта
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Имя мерчанта
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Страна мерчанта. ISO-3166 (3 цифры)
    /// </summary>
    public string Country { get; set; }
    
    /// <summary>
    /// URL мерчанта
    /// </summary>
    public string Url { get; set; }
}
namespace Aggregator.DTOs.AcsOtp;

/// <summary>
/// Информация о мерчанте
/// </summary>
public record AggregatorAcsOtpMerchantInfoDto
{
    /// <summary>
    /// Идентификатор мерчанта
    /// </summary>
    public string Id { get; init; }
    
    /// <summary>
    /// Имя мерчанта
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Страна мерчанта. ISO-3166 (3 цифры)
    /// </summary>
    public string Country { get; init; }
    
    /// <summary>
    /// URL мерчанта
    /// </summary>
    public string Url { get; init; }
}
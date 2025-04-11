using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Информация о мерчанте
/// </summary>
[Owned]
public class AcsOtpMerchantInfo
{
    /// <summary>
    /// Идентификатор мерчанта
    /// </summary>
    public string MerchantId { get; set; }
    
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
namespace Aggregator.DTOs;

/// <summary>
/// Информация о мерчанте
/// </summary>
public class AggregatorMerchantInfoDto
{
    /// <summary>
    /// Идентификатор мерчанта
    /// </summary>
    public string? Id { get; set; }
    
    /// <summary>
    /// Код категории мерчанта
    /// </summary>
    public string MCC { get; set; }
    
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public string? TerminalId { get; set; }
    
    /// <summary>
    /// Идентификатор, присвоенный банку-эквайеру платежной системой
    /// </summary>
    public string? Aid { get; set; }
    
    /// <summary>
    /// Имя мерчанта
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Улица мерчанта
    /// </summary>
    public string? Street { get; set; }
    
    /// <summary>
    /// Город мерчанта
    /// </summary>
    public string? City { get; set; }
    
    /// <summary>
    /// Страна мерчанта. ISO-3166 (3 цифры)
    /// </summary>
    public string? Country { get; set; }
    
    /// <summary>
    /// Почтовый код мерчанта
    /// </summary>
    public string? ZipCode { get; set; }
}
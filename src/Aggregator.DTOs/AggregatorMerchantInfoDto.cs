namespace Aggregator.DTOs;

/// <summary>
/// Информация о мерчанте
/// </summary>
public record AggregatorMerchantInfoDto
{
    /// <summary>
    /// Идентификатор мерчанта
    /// </summary>
    public string? Id { get; init; }
    
    /// <summary>
    /// Код категории мерчанта
    /// </summary>
    public string MCC { get; init; }
    
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public string? TerminalId { get; init; }
    
    /// <summary>
    /// Идентификатор, присвоенный банку-эквайеру платежной системой
    /// </summary>
    public string? Aid { get; init; }
    
    /// <summary>
    /// Имя мерчанта
    /// </summary>
    public string? Name { get; init; }
    
    /// <summary>
    /// Улица мерчанта
    /// </summary>
    public string? Street { get; init; }
    
    /// <summary>
    /// Город мерчанта
    /// </summary>
    public string? City { get; init; }
    
    /// <summary>
    /// Страна мерчанта. ISO-3166 (3 цифры)
    /// </summary>
    public string? Country { get; init; }
    
    /// <summary>
    /// Почтовый код мерчанта
    /// </summary>
    public string? ZipCode { get; init; }
}
using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.IssFinAuth;

/// <summary>
/// Уведомление о финансовой авторизации по номеру счета банка-эмитента
/// </summary>
public class AggregatorIssFinAuthDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; set; }
    
    /// <inheritdoc />
    public long EventId { get; set; }
    
    /// <inheritdoc />
    public string Time { get; set; }
    
    /// <summary>
    /// Детали финансовой авторизации по карте банка-эмитента
    /// </summary>
    public AggregatorIssFinAuthDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о карте и ее лимитах на момент формирования уведомления
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; set; }
    
    /// <summary>
    /// Информация о счетах на момент формирования уведомления
    /// </summary>
    public List<AggregatorAccountInfoDto> AccountsInfo { get; set; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorMerchantInfoDto MerchantInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; set; }
}
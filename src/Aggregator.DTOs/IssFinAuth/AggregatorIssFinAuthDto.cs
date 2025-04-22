using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.IssFinAuth;

/// <summary>
/// Уведомление о финансовой авторизации по номеру счета банка-эмитента
/// </summary>
public record AggregatorIssFinAuthDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; init; }
    
    /// <inheritdoc />
    public long EventId { get; init; }
    
    /// <inheritdoc />
    public string Time { get; init; }
    
    /// <summary>
    /// Детали финансовой авторизации по карте банка-эмитента
    /// </summary>
    public AggregatorIssFinAuthDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о карте и ее лимитах на момент формирования уведомления
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; init; }
    
    /// <summary>
    /// Информация о счетах на момент формирования уведомления
    /// </summary>
    public List<AggregatorAccountInfoDto> AccountsInfo { get; init; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorMerchantInfoDto MerchantInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; init; }
}
using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.AcqFinAuth;

/// <summary>
/// Уведомление об онлайн эквайринговых финансовых авторизациях по картам
/// </summary>
public record AggregatorAcqFinAuthDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; init; }
    
    /// <inheritdoc />
    public long EventId { get; init; }
    
    /// <inheritdoc />
    public string Time { get; init; }
    
    /// <summary>
    /// Детали онлайн эквайринговой финансовой авторизации по карте
    /// </summary>
    public AggregatorAcqFinAuthDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorMerchantInfoDto MerchantInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; init; }
}
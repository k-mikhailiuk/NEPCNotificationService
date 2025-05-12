namespace Aggregator.DTOs;

/// <summary>
/// Тип - контейнер лимитов
/// </summary>
public record AggregatorLimitWrapperDto
{
    /// <summary>
    /// Суммовой лимит
    /// </summary>
    public AggregatorAmtLimitDto? AmtLimit { get; init; }
    
    /// <summary>
    /// Количественный лимит
    /// </summary>
    public AggregatorCntLimitDto? CntLimit { get; init; }
}
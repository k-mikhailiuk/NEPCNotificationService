namespace Aggregator.DTOs;

/// <summary>
/// Тип - контейнер лимитов
/// </summary>
public class AggregatorLimitWrapperDto
{
    /// <summary>
    /// Суммовой лимит
    /// </summary>
    public AggregatorAmtLimitDto? AmtLimit { get; set; }
    
    /// <summary>
    /// Количественный лимит
    /// </summary>
    public AggregatorCntLimitDto? CntLimit { get; set; }
}
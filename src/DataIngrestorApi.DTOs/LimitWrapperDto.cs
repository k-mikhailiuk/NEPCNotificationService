namespace DataIngrestorApi.DTOs;

/// <summary>
/// Тип - контейнер лимитов
/// </summary>
public record LimitWrapperDto
{
    /// <summary>
    /// Суммовой лимит
    /// </summary>
    public AmtLimitDto? AmtLimit { get; init; }
    
    /// <summary>
    /// Количественный лимит
    /// </summary>
    public CntLimitDto? CntLimit { get; init; }
}
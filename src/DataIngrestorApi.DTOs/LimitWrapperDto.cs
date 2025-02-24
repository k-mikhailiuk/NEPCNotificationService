namespace DataIngrestorApi.DTOs;

/// <summary>
/// Тип - контейнер лимитов
/// </summary>
public class LimitWrapperDto
{
    /// <summary>
    /// Суммовой лимит
    /// </summary>
    public AmtLimitDto? AmtLimit { get; set; }
    
    /// <summary>
    /// Количественный лимит
    /// </summary>
    public CntLimitDto? CntLimit { get; set; }
}
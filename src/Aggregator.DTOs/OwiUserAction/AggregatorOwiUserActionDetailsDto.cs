namespace Aggregator.DTOs.OwiUserAction;

/// <summary>
/// Подробная информация о действии пользователя в OWI
/// </summary>
public record AggregatorOwiUserActionDetailsDto
{
    /// <summary>
    /// Время совершения операции в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; init; }

    /// <summary>
    /// Login в owi (имя пользователя)
    /// </summary>
    public string Login { get; init; }

    /// <summary>
    /// Действие пользователя
    /// </summary>
    public string Action { get; init; }
}
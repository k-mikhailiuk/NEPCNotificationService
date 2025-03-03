using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.OwiUserAction;

/// <summary>
/// Уведомление о действии пользователя в OWI
/// </summary>
public class AggregatorOwiUserActionDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; set; }
    
    /// <inheritdoc />
    public long EventId { get; set; }
    
    /// <inheritdoc />
    public string Time { get; set; }
    
    /// <summary>
    /// Подробная информация о действии пользователя в OWI
    /// </summary>
    public AggregatorOwiUserActionDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о карте на момент формирования уведомления
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; set; }
}
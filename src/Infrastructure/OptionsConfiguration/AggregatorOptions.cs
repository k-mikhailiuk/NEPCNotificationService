namespace OptionsConfiguration;

/// <summary>
/// Класс конфигурации для агрегатора уведомлений.
/// Используется для задания параметров пакетной обработки и интервалов.
/// </summary>
public class AggregatorOptions
{
    /// <summary>
    /// Имя секции конфигурации.
    /// </summary>
    public const string Aggregator = nameof(Aggregator); 
    
    /// <summary>
    /// Размер пакета для обработки уведомлений.
    /// </summary>
    public int BatchSize { get; set; }
    
    /// <summary>
    /// Интервал между итерациями обработки, в секундах.
    /// </summary>
    public int IntervalInSeconds { get; set; }
}
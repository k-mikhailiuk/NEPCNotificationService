namespace ControlPanel.Core.DTOs.LimitIdDescription;

/// <summary>
/// DTO для добавления описания лимита.
/// </summary>
/// <remarks>
/// Содержит информацию о коде лимита, его наименовании и описаниях на русском, кыргызском и английском языках.
/// </remarks>
public class AddLimitIdDescriptionDto
{
    /// <summary>
    /// Получает или задаёт код лимита.
    /// </summary>
    public int LimitCode { get; set; }
    
    /// <summary>
    /// Получает или задаёт наименование лимита.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Получает или задаёт описание лимита на русском языке.
    /// </summary>
    public string DescriptionRu { get; set; }
    
    /// <summary>
    /// Получает или задаёт описание лимита на кыргызском языке.
    /// </summary>
    public string DescriptionKg { get; set; }
    
    /// <summary>
    /// Получает или задаёт описание лимита на английском языке.
    /// </summary>
    public string DescriptionEn { get; set; }
}
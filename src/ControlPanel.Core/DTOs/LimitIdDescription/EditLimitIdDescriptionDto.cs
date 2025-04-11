namespace ControlPanel.Core.DTOs.LimitIdDescription;

/// <summary>
/// DTO для редактирования описания лимита.
/// </summary>
/// <remarks>
/// Содержит идентификатор записи, код лимита, наименование и описания на русском, кыргызском и английском языках.
/// </remarks>
public class EditLimitIdDescriptionDto
{
    /// <summary>
    /// Идентификатор записи описания лимита.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Код лимита
    /// </summary>
    public int LimitCode { get; set; }
    
    /// <summary>
    /// Наименование лимита
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
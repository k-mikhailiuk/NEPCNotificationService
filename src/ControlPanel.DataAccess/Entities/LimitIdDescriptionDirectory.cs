using System.ComponentModel.DataAnnotations.Schema;

namespace ControlPanel.DataAccess.Entities;

/// <summary>
/// Представляет справочник описаний лимитов по идентификатору, включающий код лимита, название и описания на русском, кыргызском и английском языках.
/// </summary>
public class LimitIdDescriptionDirectory
{
    /// <summary>
    /// Уникальный идентификатор записи справочника. Значение генерируется автоматически базой данных.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Код лимита.
    /// </summary>
    public long LimitCode { get; set; }
    
    /// <summary>
    /// Название лимита.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание лимита на русском языке.
    /// </summary>
    public string DescriptionRu { get; set; }
    
    /// <summary>
    /// Описание лимита на кыргызском языке.
    /// </summary>
    public string DescriptionKg { get; set; }
    
    /// <summary>
    /// Описание лимита на английском языке.
    /// </summary>
    public string DescriptionEn { get; set; }
    
    /// <summary>
    /// Создает новый экземпляр <see cref="LimitIdDescriptionDirectory"/> с указанными параметрами.
    /// </summary>
    /// <param name="limitCode">Код лимита. Не должен быть равен 0.</param>
    /// <param name="name">Название лимита. Не должно быть пустым или состоять только из пробелов.</param>
    /// <param name="descriptionRu">Описание лимита на русском языке. Не должно быть пустым или состоять только из пробелов.</param>
    /// <param name="descriptionKg">Описание лимита на кыргызском языке. Не должно быть пустым или состоять только из пробелов.</param>
    /// <param name="descriptionEn">Описание лимита на английском языке. Не должно быть пустым или состоять только из пробелов.</param>
    /// <returns>Новый объект <see cref="LimitIdDescriptionDirectory"/> с заданными параметрами.</returns>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если любое из текстовых полей пустое или состоит только из пробелов, либо если limitCode равен 0.
    /// </exception>
    public static LimitIdDescriptionDirectory Create(long limitCode, string name, string descriptionRu, string descriptionKg, string descriptionEn)
    {
        if (string.IsNullOrWhiteSpace(descriptionRu))
            throw new ArgumentException("DescriptionRu cannot be null or whitespace.", nameof(descriptionRu));
        
        if (string.IsNullOrWhiteSpace(descriptionKg))
            throw new ArgumentException("DescriptionKg cannot be null or whitespace.", nameof(descriptionKg));
        
        if (string.IsNullOrWhiteSpace(descriptionEn))
            throw new ArgumentException("DescriptionEn cannot be null or whitespace.", nameof(descriptionEn));
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        
        if (limitCode == 0)
            throw new ArgumentException("LimitCode cannot be null or default value.", nameof(limitCode));
        
        return new LimitIdDescriptionDirectory
        {
            LimitCode = limitCode,
            Name = name,
            DescriptionRu = descriptionRu,
            DescriptionKg = descriptionKg,
            DescriptionEn = descriptionEn
        }; 
    }
}
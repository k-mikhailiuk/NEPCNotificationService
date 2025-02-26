using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Параметры расширения
/// </summary>
public class ExtensionParameter
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    /// Идентификатор расширения, которому принадлежит параметр
    /// </summary>
    [Required]
    public long ExtensionId { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    [ForeignKey(nameof(ExtensionId))]
    public NotificationExtension Extension { get; set; }
    
    /// <summary>
    /// Имя параметра
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Значение параметра
    /// </summary>
    [Required]
    public string Value { get; set; }
}